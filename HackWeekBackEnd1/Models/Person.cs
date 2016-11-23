﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace HackWeekBackEnd1.Models
{
    public class Person
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("expertise")]
        public List<String> Expertise { get; set; }
    }
}