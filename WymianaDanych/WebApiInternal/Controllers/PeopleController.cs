using DAL.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.IO;
using System.Threading.Tasks;
using WfcExternal;

namespace WebApiInternal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _wcfService;
        private readonly DbService<Request> _requestDbService;
        private readonly DbService<RequestRaw>  _requestRawDbService;
        private readonly DbService<Error> _errorDbService;

        public PeopleController(IPersonService wcfService, DbService<Request> requestDbService, DbService<RequestRaw> requestRawDbService, DbService<Error> errorDbService)
        {
            _wcfService = wcfService;
            _requestDbService = requestDbService;
            _requestRawDbService = requestRawDbService;
            _errorDbService = errorDbService;
        }

        [HttpPost]
        public async Task<IActionResult> GetPoints(Person person)
        {
            RequestRaw requestRaw = await CreateRequestRaw();
            Request request = await CreateRequest(person, requestRaw);

            try
            {
                request.Status = await _wcfService.CheckPointsAsync(person);
            }
            catch(Exception e)
            {
                Error error = await CreateError(e);
                request.Error = error;
            }
            await _requestDbService.UpdateAsync(request.Id, request);

            if (request.Error != null)
                return StatusCode(500);
            return Ok(request.Status);
        }

        private async Task<Error> CreateError(Exception e)
        {
            var error = new Error { Message = e.Message };
            error = await _errorDbService.CreateAsync(error);
            return error;
        }

        private async Task<Request> CreateRequest(Person person, RequestRaw requestRaw)
        {
            var request = new Request { FirstName = person.FirstName, LastName = person.LastName, RequestRawId = requestRaw.Id, Status = 0 };
            request = await _requestDbService.CreateAsync(request);
            return request;
        }

        private async Task<RequestRaw> CreateRequestRaw()
        {
            var requestRaw = new RequestRaw();
            requestRaw.Body = await GetRequestBodyAsString();
            requestRaw = await _requestRawDbService.CreateAsync(requestRaw);
            return requestRaw;
        }

        private async Task<string> GetRequestBodyAsString()
        {
            Request.Body.Position = 0;
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
