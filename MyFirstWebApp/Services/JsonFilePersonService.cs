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
            IEnumerable<Person> users = GetPersons();
            Person query = users.First(x => x.Id == personId);
            if (query.Ratings == null)
            {
                //létrehozzuk az új tömböt az első adattal
                int[] ratings = new int[] { rating };
                //átírjuk a személy Ratings tulajdonságát
                query.Ratings = ratings;
            }
            else
            {
                //kivesszük az adott személy ratings tömbjét egy változóba
                List<int> ratings = query.Ratings.ToList();
                //hozzáadjuk az új rating-et
                ratings.Add(rating);
                //a személy régi Ratings tömbjét átírjuk az új ratings tömbbel
                query.Ratings = ratings.ToArray();
            }
            //az összes adatot visszaírjuk a json file-ba
            //megnyitjuk a file-t, create = mindent kitöröl, és újraír mindent
            using(FileStream outputStream = File.Open(JsonFileName, FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.Serialize<IEnumerable<Person>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                        {
                            SkipValidation = true,
                            Indented = true
                        }),
                        users
                    );
            }
        }


    }
}
