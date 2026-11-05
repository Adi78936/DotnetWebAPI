
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        // Temporary in-memory database
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Rahul", Age = 22, Department = "IT" },
            new Employee { Id = 2, Name = "Amit", Age = 23, Department = "HR" },
            new Employee { Id = 3, Name = "Rohan", Age = 21, Department = "Finance" }
        };

        

        // 1. GET - Get all employees
        [HttpGet("GetAllEmployees")]
        public ActionResult<List<Employee>> GetAllEmployees()
        {
            return Ok(employees);
        }

        // 2. POST - Add a new employee
        [HttpPost("AddEmployee")]
        public ActionResult<Employee> AddEmployee(Employee employee)
        {
           // employee.Id = nextId++;

            employees.Add(employee);

            return CreatedAtAction(
                nameof(GetAllEmployees),
                employee
            );
        }

        // 3. PUT - Update employee
        [HttpPut("UpdateEmployee/{id}")]
        public IActionResult UpdateEmployee(int id, Employee updatedEmployee)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            employee.Name = updatedEmployee.Name;
            employee.Age = updatedEmployee.Age;
            employee.Department = updatedEmployee.Department;

            return Ok(employee);
        }

        // 4. DELETE - Delete employee
        [HttpDelete("DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            employees.Remove(employee);

            return Ok("Employee deleted successfully");
        }
    }

    // Employee Model
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Department { get; set; } = string.Empty;
    }
}
