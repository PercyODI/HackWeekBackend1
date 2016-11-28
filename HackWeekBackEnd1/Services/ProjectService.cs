using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackWeekBackEnd1.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HackWeekBackEnd1.Services
{
    public class ProjectService : EntityService<Project>
    {
        public IEnumerable<Project> GetProjectsDetails(int limit, int skip)
        {
            BsonDocument emptyFilter = new BsonDocument();
            var sort = Builders<Project>.Sort.Descending("name");
            var projectsCursor = MongoConnectionHandler.MongoCollection.Find(emptyFilter)
                .Sort(sort)
                .Limit(limit)
                .Skip(skip)
                .ToList();
            return projectsCursor;
        }

        public override void Update(Project entity)
        {
            throw new NotImplementedException();
        }

        public void Update(BsonDocument entity)
        {
            // Hopefully the generic project won't interfere...
            IMongoCollection<Project> collection = MongoConnectionHandler.MongoCollection;
            var filter = Builders<Project>.Filter.Eq("_id", new ObjectId(entity.GetValue("_id").ToString()));
            bool set = false;
            bool push = false;
            bool pull = false;
            var setFields = new BsonDocument();
            var pushFields = new BsonDocument();
            var pullFields = new BsonDocument();
            if (entity.Contains("name"))
            {
                setFields.Add("name", entity.GetValue("name"));
                set = true;
            }
            if (entity.Contains("difficulty"))
            {
                setFields.Add("difficulty", entity.GetValue("difficulty"));
                set = true;
            }
            if (entity.Contains("add_person_to_project"))
            {
                pushFields.Add("people_on_project", new BsonDocument().Add("name", entity.GetValue("add_person_to_project")));
                push = true;
            }
            if (entity.Contains("remove-person-from-project"))
            {
                pullFields.Add("people_on_project", entity.GetValue("remove_person_from_project"));
                pull = true;
            }
            var update = new BsonDocument();
            if (set)
            {
                update.Add("$set", setFields);
            }
            if (push)
            {
                update.Add("$push", pushFields);
            }
            if (pull)
            {
                update.Add("$pull", pullFields);
            }
        
            
            var result = collection.UpdateMany(filter, update);
        }
    }
}