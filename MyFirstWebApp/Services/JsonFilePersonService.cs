using Microsoft.AspNetCore.Hosting;
using MyFirstWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyFirstWebApp.Services
{
    //ez segít a json file-ból beolvasni az adatokat Person objektumokba
    //a Startup class ConfigureServices metódusába be kell tenni, hogy használni tudjuk
    public class JsonFilePersonService
    {
        //konstruktorban megkapja a webhostenviron..-et (weboldalak egy hostban élnek)
        public JsonFilePersonService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        //az adatokat tartalmazó file elérési útvonala (ez egy property)
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "person_mock_data.json"); }
        }

        //ez egy sorozatot ad vissza a Person objektumokkal, ami mindenféle Collectionbe beolvasható
        //a file-ból olvassa ki és szétszedi egy-egy objektumra (deserialize)
        public IEnumerable<Person> GetPersons()
        {
            //a jsonFileReaderbe betesszük az adatos file-t
            using( var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Person[]>(
                    jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        /*ez opcionális beállítás a kiolvasáshoz,
                        a property nevek és a json file-ban talált nevek összehasonlításakor
                        figyelmen kívül hagyja a kis- és nagybetűket*/
                        PropertyNameCaseInsensitive = true
                    }) ;
            }
        }

        public void AddRating(int personId, int rating)
        {

        }

    }
}
