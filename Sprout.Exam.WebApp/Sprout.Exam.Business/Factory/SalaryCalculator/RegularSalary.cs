using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Factory.SalaryCalculator
{
    public class RegularSalaryCalculator : ISalaryCalculator
    {
        public decimal Calculate(EmployeeDto employee, CalculateDto calculateDto)
        {
            decimal baseSalary = 20000M; // Data can be in EmployeeDto
            decimal tax = 0.12m; // Tax amount can be in CalculateDto
            int numberOfDays = 22; // number of days can be in CalculateDto 

            decimal absentSalaryDeduction = baseSalary / numberOfDays * calculateDto.AbsentDays;
            decimal taxSalaryDeduction = baseSalary * tax;
            decimal salary = baseSalary - absentSalaryDeduction - taxSalaryDeduction;
            salary = decimal.Round(salary, 2, MidpointRounding.AwayFromZero);
            return salary;
        }
    }
}
