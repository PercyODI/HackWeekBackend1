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

        // To be removed!! Bad Bad Bad!
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
            if (entity.Contains("remove-personName-from-project"))
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

        public void AddPersonToProject(string projectId, Person person)
        {
            var collection = MongoConnectionHandler.MongoCollection;
            var filterBuilder = Builders<Project>.Filter;
            var filter = filterBuilder.Eq("_id", new ObjectId(projectId)) & filterBuilder.Ne("people_on_project.name", person.name);
            var update = Builders<Project>.Update
                .Push(x => x.people_on_project, person);
            collection.UpdateOne(filter, update);
        }

        // Use FindOneAndUpdate if we want to return the new object
        public void RemovePersonFromProject(string projectId, string personName)
        {
            var personToRemove = new Person {name=personName};
            var collection = MongoConnectionHandler.MongoCollection;
            var filter = Builders<Project>.Filter.Eq("_id", new ObjectId(projectId));
            var update = Builders<Project>.Update
                .PullFilter("people_on_project", Builders<Person>.Filter.Eq("name", personName));
            collection.UpdateOne(filter, update);
        }

        public void AddExpertiseToPerson(string projectId, Person person)
        {
            var collection = MongoConnectionHandler.MongoCollection;
            var filterBuilder = Builders<Project>.Filter;
            var filter = filterBuilder.Eq("_id", new ObjectId(projectId)) & filterBuilder.Eq("people_on_project.name", person.name);
            var update = Builders<Project>.Update
                .AddToSetEach("people_on_project.$.expertise", person.expertise);
            collection.UpdateOne(filter, update);
        }
    }
}