using DataService.Interfaces;
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
        public IActionResult Get()
        {
            var entities = _service.Read();
            return Ok(entities);
        }

        public virtual IActionResult Get(int id)
        {
            var entity = _service.Read(id);
            if (entity == null)
                return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Post(T entity)
        {
            /*if (!ModelState.IsValid)
                return BadRequest(ModelState);*/

            entity = _service.Create(entity);
            return CreatedAtRoute(GetType().Name + "_Get", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, T entity)
        {
            if (_service.Read(id) == null)
                return NotFound();

            _service.Update(id, entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
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
