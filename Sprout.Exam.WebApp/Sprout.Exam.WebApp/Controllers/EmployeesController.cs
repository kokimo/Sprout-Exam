using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common;
using System.Threading;
using Sprout.Exam.WebApp.Data.Entities;
using Sprout.Exam.Business.Factory.SalaryCalculator;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository) {
            _employeeRepository = employeeRepository;
        }



        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeRepository.GetListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeeRepository.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var employee = await _employeeRepository.UpdateAsync(input);
                if (employee == null) return NotFound("Employee not found");
                employee.FullName = input.FullName;
                employee.Tin = input.Tin;
                employee.Birthdate = input.Birthdate;
                employee.EmployeeTypeId = input.TypeId;
                return Ok(employee);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
          
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {

            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
               var id = await _employeeRepository.CreateAsync(input);

                return Created($"/api/employees/{id}", id);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _employeeRepository.DeleteAsync(id);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

          
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(CalculateDto data)
        {
            try
            {
                if (data.Id <= 0)
                {
                    ModelState.AddModelError("InvalidInput", "Id must be non-negative.");
                    return BadRequest(ModelState);
                }

                var employee = await _employeeRepository.GetByIdAsync(data.Id);

                if (employee == null) return NotFound("Employee not found");

                if(employee.EmployeeTypeId == (int)EmployeeType.Regular && data.AbsentDays < 0) {
                    ModelState.AddModelError("InvalidInput", "Worked Days must be non-negative.");
                    return BadRequest(ModelState);
                }

                if (employee.EmployeeTypeId == (int)EmployeeType.Contractual && data.WorkedDays < 0)
                {
                    ModelState.AddModelError("InvalidInput", "Absent Days must be non-negative.");
                    return BadRequest(ModelState);
                }

          
                var salaryCalculator = SalaryCalculator.CreateCalculator(employee.EmployeeTypeId);

                var salary = salaryCalculator.Calculate(employee, data);
                return Created($"/api/employees/{employee.Id}/calculate", salary.ToString());

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
            

           

        }

    }
}
