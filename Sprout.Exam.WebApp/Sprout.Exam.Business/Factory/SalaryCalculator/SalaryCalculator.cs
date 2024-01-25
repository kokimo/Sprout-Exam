using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Factory.SalaryCalculator
{
    public static class SalaryCalculator
    {
        public static ISalaryCalculator CreateCalculator(int employeeType) {

            // add other salary type here

            ISalaryCalculator calculatorDetails = employeeType switch
            {
                (int)EmployeeType.Regular => new RegularSalaryCalculator(),
                (int)EmployeeType.Contractual => new ContractualSalaryCalculator(),
                _ => throw new ArgumentException("Invalid Employee Type", nameof(employeeType)),
            };
            return calculatorDetails;
        }
    }
}
