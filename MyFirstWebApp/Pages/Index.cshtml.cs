using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyFirstWebApp.Models;
using MyFirstWebApp.Services;

namespace MyFirstWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        //hozzáadjuk a saját service-ünket
        public JsonFilePersonService PersonService;
        //hozzáadjuk a Person adatok listáját is (private set: csak ez az osztály állíthatja be a lista adatait)
        public IEnumerable<Person> Persons { get; private set; }

        //konstruktor
        //az ASP.Net-től kérünk egy loggert (ez benne volt)
        //plusz ide írjuk a saját service-einket is
        public IndexModel(
            ILogger<IndexModel> logger,
            JsonFilePersonService personService)
        {
            _logger = logger;
            PersonService = personService;
            
        }

        //a get html parancs esetén hajtja végre ezt
        public void OnGet()
        {
            //itt kérjük el az adatokat a personservice-től
            Persons = PersonService.GetPersons();
        }
    }
}
