using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HackWeekBackEnd1.Models
{
    // Defines a project.

    // Any extra elements are ignored by WebAPI. If update is applied to the
    // object, the extra elements stored in the database will be overwritten
    // Missing elements are given default values
    [BsonIgnoreExtraElements]
    public class Project : MongoEntity
    {
        public string name { get; set; }
        public int difficulty { get; set; }
        public string description { get; set; }
        public List<Skill> needed_skills { get; set; }
        public List<Person> people_on_project { get; set; }

        // Set default values for any missing
        public Project()
        {
            name = "";
            difficulty = -1;
            description = "";
            people_on_project = new List<Person>();
            needed_skills = new List<Skill>();
        }
    }
}