using DataService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Filters;
using WebAPI.Hubs;

namespace WebAPI.Controllers
{
    [ServiceFilter(typeof(SampleActionFilter))]
    public class StudentsController : CrudApiController<Student>
    {
        IHubContext<StudentsHub> _hub;

        public StudentsController(IService<Student> service, IHubContext<StudentsHub> hub) : base(service)
        {
            _hub = hub;
        }

        [HttpGet("{id}", Name = "StudentsController_Get")]
        public override IActionResult Get(int id)
        {
            return base.Get(id);
        }

        public override IActionResult Post(Student entity)
        {
            entity = _service.Create(entity);

            //StudentsHub.NotifyCreated(_hub.Clients, entity).Wait();
            StudentsHub.NotifyWhenGroupYear2Created(_hub.Clients, entity).Wait();

            return CreatedAtRoute(GetType().Name + "_Get", new { id = entity.Id }, entity);
        }
    }
}
