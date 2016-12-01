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
        public List<Skill> skills { get; set; }

        public Person()
        {
            name = "";
            skills = new List<Skill>();
        }
    }
}