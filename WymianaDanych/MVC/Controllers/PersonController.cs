using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string firstName, string lastName)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:5001");

                var xml = $"<Person><FirstName>{firstName}</FirstName><LastName>{lastName}</LastName></Person>";
                using (var content = new StringContent(xml, Encoding.UTF8, "application/xml"))
                {
                    var response = await httpClient.PostAsync("/api/People", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return View(int.Parse(responseContent));
                    }
                }
            }
            return View(null);
        }
    }
}
