using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackWeekBackEnd1.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HackWeekBackEnd1.Services
{
    public abstract class EntityService<T> : IEntityService<T> where T : IMongoEntity
    {
        protected readonly MongoConnectionHandler<T> MongoConnectionHandler = new MongoConnectionHandler<T>();

        public virtual void Create(T entity)
        {
            MongoConnectionHandler.MongoCollection.InsertOne(entity);
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

        public abstract void Update(T entity);
    }
}