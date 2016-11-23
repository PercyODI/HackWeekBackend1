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
        public Project()
        {
            PeopleOnProject = new List<Person>();
        }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("difficulty")]
        public int Difficulty { get; set; }

        [BsonElement("people_on_project")]
        public List<Person> PeopleOnProject { get; set; }

        [BsonExtraElements]
        public BsonDocument CatchAll { get; set; }
    }
}