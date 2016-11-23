using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace HackWeekBackEnd1.Models
{
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}