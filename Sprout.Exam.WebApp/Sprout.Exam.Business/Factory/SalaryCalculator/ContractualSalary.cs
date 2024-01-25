using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Factory.SalaryCalculator
{
    public class ContractualSalaryCalculator : ISalaryCalculator
    {
        public decimal Calculate(EmployeeDto employee, CalculateDto calculateDto)
        {
            decimal ratePerDay = 500M;
            decimal salary = ratePerDay * calculateDto.WorkedDays;
            salary = decimal.Round(salary, 2, MidpointRounding.AwayFromZero);
            return salary;
        }
    }
}
