using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackWeekBackEnd1.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HackWeekBackEnd1.Services
{
    public class UserService : EntityService<User>
    {
        public IEnumerable<User> GetUsersDetails(int limit, int skip)
        {

            BsonDocument emtpyFilter = new BsonDocument();
            var sort = Builders<User>.Sort.Descending("first_name");
            var userCursor = MongoConnectionHandler.MongoCollection.Find(emtpyFilter)
                .Sort(sort)
                .Limit(limit)
                .Skip(skip)
                .ToList();
            return userCursor;

        }

        public override User Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}