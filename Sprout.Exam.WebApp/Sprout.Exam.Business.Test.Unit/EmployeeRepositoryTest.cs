using FluentAssertions;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Factory.SalaryCalculator;
using Sprout.Exam.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using NSubstitute;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Business.Repository;

namespace Sprout.Exam.Business.Test.Unit
{
    public class EmployeeRepositoryTest
    {
        [Fact]
        public async Task GetEmployees_ReturnsEmployeesList()
        {
            // Arrange
            var _context = Substitute.For<IEmployeeRepository>();
            List<EmployeeDto> employees = new()
            {
                new() { Id = 1, FullName = "Kevin Lee", Birthdate = new DateTime(1999, 1, 25), Tin = "41238823", EmployeeTypeId = 1 },
                new() { Id = 2, FullName = "Jasmine Smith", Birthdate = new DateTime(1980, 3, 12), Tin = "12390220", EmployeeTypeId = 2 },
            };
            _ = _context.GetListAsync().Returns(employees);

            // Act
            var result = await _context.GetListAsync();

            // Assert
            _ = result.Should().NotBeNull();
            _ = result.Should().BeOfType<List<EmployeeDto>>();
            _ = result.Count.Should().Be(2);
            _ = result[1].FullName.Should().Be("Jasmine Smith");
        }

        [Fact]
        public async Task GetEmployeeById_ReturnsEmployee()
        {
            // Arrange
            var _context = Substitute.For<IEmployeeRepository>();
            EmployeeDto expectedEmployee = new() { Id = 1, FullName = "Kevin Lee", Birthdate = new DateTime(1999, 1, 25), Tin = "41238823", EmployeeTypeId = 1 };
            int inputEmployeeId = 1;
            _context.GetByIdAsync(inputEmployeeId).Returns(expectedEmployee);

            // Act
            var result = await _context.GetByIdAsync(inputEmployeeId);

            // Assert
           
            _ = result.Id.Should().Be(expectedEmployee.Id);
            _ = result.FullName.Should().Be(expectedEmployee.FullName);
            _ = result.Birthdate.Should().Be(expectedEmployee.Birthdate);
            _ = result.Tin.Should().Be(expectedEmployee.Tin);
            _ = result.EmployeeTypeId.Should().Be(expectedEmployee.EmployeeTypeId);
            _ = result.Should().NotBeNull();
            _ = result.Should().BeOfType<EmployeeDto>();
        }

        [Fact]
        public async Task CreateEmployee_ReturnsCreatedEmployee()
        {
            // Arrange
            var _context = Substitute.For<IEmployeeRepository>();
            int expectedEmployeeId = 3;
            CreateEmployeeDto createEmployeeInput = new() { FullName = "Kevin Lee", Birthdate = new DateTime(1999, 1, 25), Tin = "41238823", TypeId = 1 };
            _context.CreateAsync(createEmployeeInput).Returns(expectedEmployeeId);

            // Arrange
            var result = await _context.CreateAsync(createEmployeeInput);

            // Assert
            _ = result.Should().BeGreaterThan(0);
            _ = result.Should().Be(expectedEmployeeId);
        }

        [Fact]
        public async Task EditEmployee_ReturnsEditedEmployee()
        {
            // Arrange
            var _context = Substitute.For<IEmployeeRepository>();
            EmployeeDto expectedEmployee = new() { Id = 1, FullName = "Kevin Lee", Birthdate = new DateTime(1999, 1, 25), Tin = "41238823", EmployeeTypeId = 1 };
            EditEmployeeDto editEmployee = new() { Id = 1, FullName = "Kevin Lee", Birthdate = new DateTime(1999, 1, 25), Tin = "41238823", TypeId = 1 };

            _context.UpdateAsync(editEmployee).Returns(expectedEmployee);

            // Act
            var result = await _context.UpdateAsync(editEmployee);

            // Assert
            _ = result.Should().NotBeNull();
            _ = result.Should().BeOfType<EmployeeDto>();
            _ = result.Id.Should().Be(expectedEmployee.Id);
            _ = result.FullName.Should().Be(expectedEmployee.FullName);
            _ = result.Birthdate.Should().Be(expectedEmployee.Birthdate);
            _ = result.Tin.Should().Be(expectedEmployee.Tin);
            _ = result.EmployeeTypeId.Should().Be(expectedEmployee.EmployeeTypeId);
        }

        [Fact]
        public async Task DeleteEmployee_ReturnsDeletedEmployeeId()
        {
            // Arrange
            var _context = Substitute.For<IEmployeeRepository>();
            int expectedEmployeeId =1;
            int inputEmployeeId = 1;
            _context.DeleteAsync(inputEmployeeId).Returns(expectedEmployeeId);

            // Act
            var result = await _context.DeleteAsync(inputEmployeeId);

            // Assert
            _ = result.Should().BeGreaterThan(0);
            _ = result.Should().Be(expectedEmployeeId);
        }

    }
}
