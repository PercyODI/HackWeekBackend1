using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace HackWeekBackEnd1.Models
{
    public class Project
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Difficulty { get; set; }
        public List<Person> PeopleOnProject { get; set; }
    }
}