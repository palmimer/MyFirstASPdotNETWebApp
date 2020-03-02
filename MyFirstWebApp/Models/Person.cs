using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyFirstWebApp.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Full_name { get
            {
                string fullName = First_name + " " + Last_name;
                return fullName;
            }
        }
        public string Email { get; set; }
        public string City { get; set; }
        public int[] Ratings { get; set; }

        //convert all the data back to a json format
        public override string ToString()
        {
            //serialize: to put one thing after the other
            //the type of properties to be serialized: Person
            return JsonSerializer.Serialize<Person>(this);
        }
    }
}
