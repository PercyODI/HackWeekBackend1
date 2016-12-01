using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackWeekBackEnd1.Models
{
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