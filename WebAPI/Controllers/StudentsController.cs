using DataService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class StudentsController : ApiController
    {
        private IStudentsService _service;

        public StudentsController(IStudentsService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var students = _service.Read();
            return Ok(students);
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public IActionResult Get(int id)
        {
            var student = _service.Read(id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Post(Student student)
        {
            student = _service.Create(student);
            return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Student student)
        {
            if (_service.Read(id) == null)
                return NotFound();

            _service.Update(id, student);
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
