using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using REST_withASPNETCORE5.Model;
using REST_withASPNETCORE5.Business.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_withASPNETCORE5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        

        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        //Construtor abaixo
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
            
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if(person == null)
            {
                return NotFound();
            }
            return Ok(person);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            
            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_personBusiness.Create(person));

        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {

            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_personBusiness.Update(person));

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long Id)
        {

            _personBusiness.Delete(Id);
            return NoContent();

        }


    }
}
