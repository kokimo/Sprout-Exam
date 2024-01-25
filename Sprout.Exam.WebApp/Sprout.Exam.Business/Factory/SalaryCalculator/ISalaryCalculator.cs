using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Factory.SalaryCalculator
{
    public interface ISalaryCalculator
    {

        //EmployeeDto - Salary amount might be coming from employee data, as future enhancement
        // CalculateDto - calculation might be different per employee type, this data object will hold those data (future enhancement)
        decimal Calculate(EmployeeDto employee, CalculateDto calculateDto); 
    }
}
