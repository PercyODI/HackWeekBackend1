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
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
    }
}