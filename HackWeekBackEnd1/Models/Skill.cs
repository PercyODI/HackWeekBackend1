using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackWeekBackEnd1.Models
{
    // Defines a skill that can be added to a person in a project
    public class Skill
    {
        public string name { get; set; }
        public int level { get; set; }

        public Skill()
        {
            name = "";
            level = 0;
        }
    }
}