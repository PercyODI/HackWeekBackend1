using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HackWeekBackEnd1.Models;
using HackWeekBackEnd1.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace HackWeekBackEnd1.Controllers
{
    public class ProjectsController : ApiController
    {
        // GET: api/Project
        public IEnumerable<Project> Get()
        {
            var projectsList = new ProjectService();
            var projects = projectsList.GetProjectsDetails(100, 0);

            return projects;
        }

        // GET: api/Project/5
        public IHttpActionResult Get(string id)
        {
            try
            {
                var projectServer = new ProjectService();
                var project = projectServer.GetById(id.ToString());

                if (project == null)
                {
                    return NotFound();
                }

                return Ok(project);
            }
            catch (Exception)
            {
                return NotFound();
            }

            
        }

        // POST: api/Project
        public HttpResponseMessage Post(Project value)
        {
            var projectService = new ProjectService();
            

            try
            {
                if (ModelState.IsValid)
                {
                    projectService.Create(value);
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
        [HttpPut]
        public IHttpActionResult Put(string id, [FromBody]JToken value)
        {
            var projectService = new ProjectService();

            try
            {
                if (ModelState.IsValid)
                {
                    var bson = BsonDocument.Parse(value.ToJson());
                    bson.Add("_id", new ObjectId(id));
                    projectService.Update(bson);
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Project/5
        public IHttpActionResult Delete(string id)
        {
            try
            {
                var projectServer = new ProjectService();
                projectServer.Delete(id);

                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
