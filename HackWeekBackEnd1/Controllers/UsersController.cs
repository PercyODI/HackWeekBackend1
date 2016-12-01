using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HackWeekBackEnd1.Models;
using HackWeekBackEnd1.Services;

// Currently Unused controller. Could be used for user authentication
namespace HackWeekBackEnd1.Controllers
{
    public class UsersController : ApiController
    {
        // GET: api/Users
        public IEnumerable<User> Get()
        {
            var usersService = new UserService();
            var usersList = usersService.GetUsersDetails(100, 0);

            return usersList;
        }

        // GET: api/Users/5
        public IHttpActionResult Get(string id)
        {
            try
            {
                UserService userService = new UserService();
                User user = userService.GetById(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: api/Users
        public IHttpActionResult Post(User value)
        {
            UserService userService = new UserService();

            try
            {
                if (ModelState.IsValid)
                {
                    userService.Create(value);
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public IHttpActionResult Delete(string id)
        {
            try
            {
                UserService userServer = new UserService();
                userServer.Delete(id);

                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
