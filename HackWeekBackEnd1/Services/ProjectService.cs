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
        // Returns a list of the projects in the database. Provides parameters to
        // allow pagination of the list.
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
        
<<<<<<< HEAD
        public override Project Update(Project project)
=======
        // Replaces the existing project in the database with the one provided.
        public override void Update(Project project)
>>>>>>> Work on comments
        { 
            IMongoCollection<Project> collection = MongoConnectionHandler.MongoCollection;
            var filter = Builders<Project>.Filter.Eq("_id", project._id);
<<<<<<< HEAD
            var findAndReplaceOptions = new FindOneAndReplaceOptions<Project>()
            {
                ReturnDocument = ReturnDocument.After
            };
            return collection.FindOneAndReplace(filter, project, findAndReplaceOptions);
            //collection.ReplaceOne(filter, project);
        }
        
=======
            collection.ReplaceOne(filter, project);
        }
>>>>>>> Work on comments
    }
}