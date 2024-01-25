﻿namespace Sprout.Exam.WebApp.Data.Entities
{
    public partial class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string TIN { get; set; }

        public int EmployeeTypeId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
