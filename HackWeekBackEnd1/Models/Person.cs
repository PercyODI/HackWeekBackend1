using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace HackWeekBackEnd1.Models
{
    // Defines a person that can be added to a project
    public class Person
    {
        public string name { get; set; }
<<<<<<< HEAD
        public List<Skill> skills { get; set; }
=======
        public List<string> skills { get; set; }
>>>>>>> Work on comments

        public Person()
        {
            name = "";
<<<<<<< HEAD
            skills = new List<Skill>();
=======
            skills = new List<string>();
>>>>>>> Work on comments
        }
    }
}