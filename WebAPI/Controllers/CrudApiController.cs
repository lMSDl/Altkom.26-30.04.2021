using DataService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public abstract class CrudApiController<T> : ApiController where T : Entity
    {
        private IService<T> _service;

        public CrudApiController(IService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRoles.Read))]
        public IActionResult Get()
        {
            var entities = _service.Read();
            return Ok(entities);
        }

        [Authorize(Roles = nameof(UserRoles.Read))]
        public virtual IActionResult Get(int id)
        {
            var entity = _service.Read(id);
            if (entity == null)
                return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRoles.Create))]
        [Authorize(Roles = nameof(UserRoles.Read))]
        public IActionResult Post(T entity)
        {
            /*if (!ModelState.IsValid)
                return BadRequest(ModelState);*/

            entity = _service.Create(entity);
            return CreatedAtRoute(GetType().Name + "_Get", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(UserRoles.Update))]
        public IActionResult Put(int id, T entity)
        {
            if (_service.Read(id) == null)
                return NotFound();

            _service.Update(id, entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(UserRoles.Delete))]
        public IActionResult Delete(int id)
        {
            if (_service.Read(id) == null)
                return NotFound();

            _service.Delete(id);
            
            //return NoContent();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
