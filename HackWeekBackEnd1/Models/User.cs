using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace HackWeekBackEnd1.Models
{
    // Unused user model. Could be used for user authentication
    [BsonIgnoreExtraElements]
    public class User : MongoEntity
    {
        public string first_name { get; set; }
        public string last_name { get; set; }   
        public string password { get; set; }
    }
}