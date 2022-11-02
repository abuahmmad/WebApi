using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1.data;
using WebApi1.service;

namespace WebApi1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices employeeservice;

        public EmployeeController(IEmployeeServices _employeeservice)
        {
            employeeservice = _employeeservice;
        }

        [HttpPost]
        [Route("Insert")]
        public void Insert(Employee emp)
        {
            employeeservice.Insert(emp);
        }
        
        [HttpPost]
        [Route("Update")]
        public void Update(Employee emp)
        {
              
        }

        [HttpGet]
        [Route("load")]
        public string load(string name)
        {

            return "load calling";    
        }
    }
}
