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
    [RoutePrefix("api/projects")]
    public class ProjectsController : ApiController
    {
        // GET: api/Project
        [Route("")]
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            var projectsList = new ProjectService();
            var projects = projectsList.GetProjectsDetails(100, 0);

            return projects;
        }

        // GET: api/Project/5
        [Route("{id}")]
        [HttpGet]
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
        [Route("")]
        [HttpPost]
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
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(string id, [FromBody]JToken value)
        {
            var projectService = new ProjectService();

            try
            {
                if (ModelState.IsValid)
                {
                    var bson = BsonDocument.Parse(value.ToString());
                    bson.Add("_id", id);
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
        [Route("{id}")]
        [HttpDelete]
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

        // POST: New person to project
        // api/projects/{projectId}/people
        [Route("{projectId}/people/")]
        [HttpPost]
        public IHttpActionResult PostPersonToProject(string projectId, [FromBody]Person value)
        {
            try
            {
                var projectServer = new ProjectService();
                projectServer.AddPersonToProject(projectId, value);

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // DELETE: Remove person from project
        // api/projects/{projectId}/people/{personName}
        [Route("{projectId}/people/{personName}")]
        [HttpDelete]
        public IHttpActionResult DeletePersonFromProject(string projectId, string personName)
        {
            try
            {
                var projectServer = new ProjectService();
                projectServer.RemovePersonFromProject(projectId, personName);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        // POST: Add expertise to person in project
        // api/projects/5836373085abf4ff08955dc4/people/Hayley%20Hutson/expertise
        [Route("{projectId}/people/{personName}/expertise")]
        [HttpPost]
        public IHttpActionResult PostExpertiseToPerson(string projectId, string personName, [FromBody]Person value)
        {
            try
            {
                //Add name to person
                value.name = personName;
                var projectServer = new ProjectService();
                projectServer.AddExpertiseToPerson(projectId, value);

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }


}
