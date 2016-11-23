using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HackWeekBackEnd1.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HackWeekBackEnd1.Controllers
{
    public class ProjectsController : ApiController
    {
        // GET: api/Project
        public IEnumerable<Project> Get()
        {
            var client = new MongoClient("mongodb://ec2-35-164-77-251.us-west-2.compute.amazonaws.com:27017/test");
            var db = client.GetDatabase("test");
            var col = db.GetCollection<Project>("projects");
            List<Project> projects = col.Find(x => true).ToList();

            return projects;
        }

        // GET: api/Project/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Project
        public HttpResponseMessage Post(Project value)
        {
            var client = new MongoClient("mongodb://ec2-35-164-77-251.us-west-2.compute.amazonaws.com:27017/test");
            var db = client.GetDatabase("test");
            var collection = db.GetCollection<Project>("projects");

            try
            {
                if (ModelState.IsValid)
                {
                    collection.InsertOne(value);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Model");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    "Error inserting document into database: " + ex.Message);
            }

            

        }

        // PUT: api/Project/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Project/5
        public void Delete(int id)
        {
        }
    }
}
