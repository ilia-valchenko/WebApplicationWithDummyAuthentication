using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationWithDummyAuthentication.Services.Interfaces;

namespace WebApplicationWithDummyAuthentication.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IPersonService service;

        public PersonController(IPersonService personService)
        {
            service = personService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "testAuthScheme")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await service.GetAsync());
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "tokenBasedAuthScheme")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var person = await service.GetAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
    }
}