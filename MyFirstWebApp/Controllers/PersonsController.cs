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
    [Route("[controller]")]
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

    }
}