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
        public IHttpActionResult Post(Project value)
        {
            var projectService = new ProjectService();
            

            try
            {
                if (ModelState.IsValid)
                {
                    Project newProject = projectService.Create(value);
                    return Ok(newProject);
                }
                else
                {
                    return BadRequest("Project model is not valid...");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Project/5
        // NOTE: id in url MUST match _id from the project object given. Don't use id in url to bypass this
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult UpdateProject(string id, [FromBody]Project value)
        {
            var projectService = new ProjectService();

            try
            {
                if (ModelState.IsValid && id.Equals(value._id))
                {
                    Project updatedProject = projectService.Update(value);
                    return Ok(updatedProject);
                }
                else
                {
                    return BadRequest("Project model is not valid...");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Overloaded update project if no id is in route
        // Requires an array of project objects to update
        [Route("")]
        [HttpPut]
        public IHttpActionResult UpdateProject([FromBody] List<Project> values)
        {
            var projectService = new ProjectService();

            try
            {
                if (ModelState.IsValid)
                {
                    List<Project> updatedProjects = new List<Project>();
                    foreach (var value in values)
                    {
                        Project updatedProject = projectService.Update(value);
                        updatedProjects.Add(updatedProject);
                    }
                    return Ok(updatedProjects);
                }
                else
                { 
                    return BadRequest("Expecting Array of Project Objects to update");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Project/583da92708767075382a4a63
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

        
    }


}
