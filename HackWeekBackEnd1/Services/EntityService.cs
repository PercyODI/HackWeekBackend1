using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackWeekBackEnd1.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HackWeekBackEnd1.Services
{
    // Provides a default set of services that can be called to modify a document in the database.
    public abstract class EntityService<T> : IEntityService<T> where T : IMongoEntity
    {
        // Creates a property for services to access the ConnecitonHandler
        protected readonly MongoConnectionHandler<T> MongoConnectionHandler = new MongoConnectionHandler<T>();

        public virtual T Create(T entity)
        {
            MongoConnectionHandler.MongoCollection.InsertOne(entity);
            return entity;
        }

        public virtual void Delete(string id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            MongoConnectionHandler.MongoCollection.FindOneAndDelete(filter);
        }

        public virtual T GetById(string id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            return MongoConnectionHandler.MongoCollection.Find(filter).First();
        }

        public abstract T Update(T entity);
    }
}