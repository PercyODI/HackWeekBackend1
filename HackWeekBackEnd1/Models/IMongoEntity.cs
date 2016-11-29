using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace HackWeekBackEnd1.Models
{
    public interface IMongoEntity
    {
        string _id { get; set; }
    }
}