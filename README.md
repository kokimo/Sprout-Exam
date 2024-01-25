# Sprout Developer Application Exam

This repository contains code and resources related to the Sprout Developer Exam. 

## Prerequisites

This project runs on .NET 5

## Configurations

Change the value of the default connection depending on your setup and local environment
``` bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(local);Initial Catalog=SproutExamDb;Integrated Security=true"
  },
}
```

Install dependencies on the client
``` bash
cd Sprout.Exam.WebApp/ClientApp
npm install
```

Restore updated database using the SproutDbExamUpdated.bak

## Task Accomplished

- Create Employee
- Get employees list
- Get employee by ID
- Update employee
- Soft Delete employee (setting employee isDelete flag)
- Calculate salary of regular and contractual employee
- Unit Testing
- Validation for the client and the server

## Improvements needed to deploy to production

- Update to the latest version of .NET
- Add table for salary. Right now salary is static but future update might require employee to have different salary grades and adding a table for Salary is a must.
- Add Employee Type services. This includes tables to hold employee type data. This is useful if we plan to have different employee types in the future. 
- Depending on what other feature, Consider adopting a microservices architecture for independent scaling of components.
- Improve UI, use modals instead of alerts.
- Add CreatedBy,CreatedDate, ModifiedBy, ModifiedDate on every table. The aforementioned data is necessary when an audit or responsibility assignment control must be carried out.
