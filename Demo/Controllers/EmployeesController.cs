using Demo.DAL.Repositories;
using Demo.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [Produces("application/json")]
    public class EmployeesController : ODataController
    {
        private readonly IEmployeeRepository repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(404)]

        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var entity = await repository.FindOneAsync(emp => emp.Id == key);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await repository.InsertAsync(employee);
            return Ok(await repository.FindOneAsync(emp => emp.Id == employee.Id));
        }
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Employee employee)
        {
            if (employee == null)
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!key.Equals(employee.Id))
            {
                return BadRequest();
            }
            try
            {
                await repository.UpdateAsync(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await repository.FindOneAsync(emp => emp.Id == key) == null)
                {
                    return NotFound();
                }
                throw;
            }
            return Ok(await repository.FindOneAsync(emp => emp.Id == key));
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Delete([FromODataUri] int key, [FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return Unauthorized();
                }
                if (!key.Equals(employee.Id))
                {
                    return BadRequest();
                }
                await repository.DeleteAsync(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await repository.FindOneAsync(emp => emp.Id == key) == null)
                {
                    return NotFound();
                }
                throw;
            }
            return Ok(await repository.FindOneAsync(emp => emp.Id == key));
        }
    }
}
