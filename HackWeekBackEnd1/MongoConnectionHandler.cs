using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackWeekBackEnd1.Models;
using MongoDB.Driver;

namespace HackWeekBackEnd1
{
    public class MongoConnectionHandler<T> where T : IMongoEntity
    {
        public IMongoCollection<T> MongoCollection { get; set; }

        public MongoConnectionHandler()
        {
            const string connectionString = "mongodb://ec2-35-164-77-251.us-west-2.compute.amazonaws.com:27017/test";
            MongoClient mongoClient = new MongoClient(connectionString);

            const string databaseName = "test";
            IMongoDatabase db = mongoClient.GetDatabase(databaseName);

            MongoCollection = db.GetCollection<T>(typeof(T).Name.ToLower() + "s");
        }

    }
}