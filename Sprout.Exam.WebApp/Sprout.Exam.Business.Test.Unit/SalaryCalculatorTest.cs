using FluentAssertions;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Factory.SalaryCalculator;
using Sprout.Exam.Common;
using System;
using System.Collections.Generic;
using Xunit;

namespace Sprout.Exam.Business.Test.Unit
{
    public class SalaryCalculatorTest
    {
        [Fact]
        public void RegularSalaryCalculator_ReturnsSalaryForNoAbsence()
        {
            // Arrange
            var calculator = new RegularSalaryCalculator();
            EmployeeDto employee = new() { Id = 1, FullName = "John Doe", Birthdate = new DateTime(1990, 5, 15), Tin = "123456789", EmployeeTypeId = 1 };
            CalculateDto calculateDto = new() { AbsentDays = 0.0M };

            // Act
            decimal result = calculator.Calculate(employee, calculateDto);

            // Assert
            result.Should().BePositive();
            result.Should().Be(17600M);
        }

        [Fact]
        public void RegularSalaryCalculator_ReturnsSalaryForDayAbsence()
        {
            // Arrange
            var calculator = new RegularSalaryCalculator();
            EmployeeDto employee = new() { Id = 1, FullName = "John Doe", Birthdate = new DateTime(1990, 5, 15), Tin = "123456789", EmployeeTypeId = 1 };
            CalculateDto calculateDto = new() { AbsentDays = 1.0M };

            // Act
            decimal result = calculator.Calculate(employee, calculateDto);

            // Assert
            result.Should().BePositive();
            result.Should().Be(16690.91M);
        }

        [Fact]
        public void RegularSalaryCalculator_ReturnsSalaryForHalfDayAbsence()
        {
            // Arrange
            var calculator = new RegularSalaryCalculator();
            EmployeeDto employee = new() { Id = 1, FullName = "John Doe", Birthdate = new DateTime(1990, 5, 15), Tin = "123456789", EmployeeTypeId = 1 };
            CalculateDto calculateDto = new() { AbsentDays = 1.5M };
           
            // Act
            decimal result = calculator.Calculate(employee, calculateDto);

            // Assert
            result.Should().BePositive();
            result.Should().Be(16236.36M);
        }

        [Fact]
        public void ContractualSalaryCalculator_ReturnsSalaryForNoWorkdays()
        {
            // Arrange
            var calculator = new ContractualSalaryCalculator();
            EmployeeDto employee = new() { Id = 1, FullName = "John Doe", Birthdate = new DateTime(1990, 5, 15), Tin = "123456789", EmployeeTypeId = 1 };
            CalculateDto calculateDto = new() { WorkedDays = 0.0M };

            // Act
            decimal result = calculator.Calculate(employee, calculateDto);

            // Assert
       
            result.Should().Be(0.0M);
        }

        [Fact]
        public void ContractualSalaryCalculator_ReturnsSalaryForHalfWorkDay()
        {
            // Arrange
            var calculator = new ContractualSalaryCalculator();
            EmployeeDto employee = new() { Id = 1, FullName = "John Doe", Birthdate = new DateTime(1990, 5, 15), Tin = "123456789", EmployeeTypeId = 1 };
            CalculateDto calculateDto = new() { WorkedDays = 0.5M };

            // Act
            decimal result = calculator.Calculate(employee, calculateDto);

            // Assert
            result.Should().BePositive();
            result.Should().Be(250.0M);
        }

        [Fact]
        public void ContractualSalaryCalculator_ReturnsSalaryForOneWorkDay()
        {
            // Arrange
            var calculator = new ContractualSalaryCalculator();
            EmployeeDto employee = new() { Id = 1, FullName = "John Doe", Birthdate = new DateTime(1990, 5, 15), Tin = "123456789", EmployeeTypeId = 1 };
            CalculateDto calculateDto = new() { WorkedDays = 1.0M };

            // Act
            decimal result = calculator.Calculate(employee, calculateDto);

            // Assert
            result.Should().BePositive();
            result.Should().Be(500.0M);
        }
    }
}
