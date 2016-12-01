﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace HackWeekBackEnd1.Models
{
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