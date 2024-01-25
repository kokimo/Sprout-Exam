using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using System.Linq;

namespace Sprout.Exam.Business.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await _context.Employee.Where(m => m.Id == id).FirstOrDefaultAsync();
            return employee;
        }

        public async Task<List<EmployeeDto>> GetListAsync()
        {
            var employees = await _context.Employee.Where(m => m.IsDeleted == false).ToListAsync();
            return employees;
        }

        public async Task<int> CreateAsync(CreateEmployeeDto input)
        {
            var newEmployee = new EmployeeDto
            {
                FullName = input.FullName,
                Birthdate = input.Birthdate,
                Tin = input.Tin,
                EmployeeTypeId = input.TypeId
            };

            _context.Employee.Add(newEmployee);

            await _context.SaveChangesAsync();

            return newEmployee.Id;
        }

        public async Task<EmployeeDto> UpdateAsync(EditEmployeeDto input)
        {
            var existingEmployee = await _context.Employee
                .Where(e => e.Id == input.Id)
                .FirstOrDefaultAsync();

            if (existingEmployee is null)
            {
                throw new ArgumentNullException(nameof(existingEmployee), "Employee not found");
            }

            existingEmployee.FullName = input.FullName;
            existingEmployee.Birthdate = input.Birthdate;
            existingEmployee.Tin = input.Tin;
            existingEmployee.EmployeeTypeId = input.TypeId;
            await _context.SaveChangesAsync();

            return existingEmployee;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var existingEmployee = await _context.Employee
               .Where(e => e.Id == id)
               .FirstOrDefaultAsync();

            if (existingEmployee is null)
            {
                throw new ArgumentNullException(nameof(existingEmployee), "Employee not found");
            }

            existingEmployee.IsDeleted = true;

            await _context.SaveChangesAsync();
            return existingEmployee.Id;

        }
    }
}
