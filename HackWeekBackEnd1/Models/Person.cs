using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace HackWeekBackEnd1.Models
{
    public class Person
    {
        public string name { get; set; }
        public List<string> expertise { get; set; }
    }
}