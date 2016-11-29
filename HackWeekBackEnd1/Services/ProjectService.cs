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
        
        public override void Update(Project project)
        { 
            // Hopefully the generic project won't interfere...
            IMongoCollection<Project> collection = MongoConnectionHandler.MongoCollection;
            var filter = Builders<Project>.Filter.Eq("_id", project._id);
            collection.ReplaceOne(filter, project);
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