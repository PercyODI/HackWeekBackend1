using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HackWeekBackEnd1.Models
{
    [BsonIgnoreExtraElements]
    public class Project : MongoEntity
    {
        public string name { get; set; }
        public int difficulty { get; set; }
        public List<Person> people_on_project { get; set; }

        //[BsonExtraElements]
        //public BsonDocument CatchAll { get; set; }

        public Project()
        {
            name = "";
            difficulty = -1;
            people_on_project = new List<Person>();
        }
    }
}