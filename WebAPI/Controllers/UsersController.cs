using DataService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private IAuthService _service;

        public UsersController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {
            var token = _service.Authenticate(user.Login, user.Password);

            if (token == null)
                return BadRequest();

            return Ok(token);
        }
    }
}
