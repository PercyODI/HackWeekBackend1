using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace HackWeekBackEnd1.Models
{
    public class MongoEntity : IMongoEntity
    {
        // Matches the _id from the database. Is stored as an ObjectId on the
        // database, but is represented by a string in C#.
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
    }
}