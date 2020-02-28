using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.Models;
using MyFirstWebApp.Services;

namespace MyFirstWebApp.Controllers
{
    //[Route("[controller]")]
    //this will call the name of the controller: persons, localhost.../persons
    //this can be changed if we want to use another route, like [Route("[/userdata]")]
    [Route("userdata")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        //we add this to Startup class in ConfigureServices method
        //it needs a personService object, we give it in the constructor
        public PersonsController(JsonFilePersonService personService)
        {
            this.PersonService = personService;
        }

        public JsonFilePersonService PersonService { get; }

        //a method that returns some information on a http get request
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return PersonService.GetPersons();
        }

        //a method to transfer the rating to the database with the help of the service, returns an OK
        //a subroute userdata/rate
        [Route("rate")]
        //[HttpPatch] "[FromBody]"
        [HttpGet]
        public ActionResult Get([FromQuery] int personId, [FromQuery] int rating)
        {
            PersonService.AddRating(personId, rating);
            return Ok();
        }
    }
}