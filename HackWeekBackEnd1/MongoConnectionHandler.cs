using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackWeekBackEnd1.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HackWeekBackEnd1
{
    public class MongoConnectionHandler<T> where T : IMongoEntity
    {
        public IMongoCollection<T> MongoCollection { get; set; }

        public MongoConnectionHandler()
        {
            // Connect to the given Mongo Database
            const string connectionString = "mongodb://ec2-35-164-77-251.us-west-2.compute.amazonaws.com:27017/test";
            MongoClient mongoClient = new MongoClient(connectionString);

            // Use the test database
            const string databaseName = "test";
            IMongoDatabase db = mongoClient.GetDatabase(databaseName);

            // Provides a MongoCollection of the given type, and automatically
            // assumes a collection name of the plural version of the type.
            MongoCollection = db.GetCollection<T>(typeof(T).Name.ToLower() + "s");
        }

    }
}