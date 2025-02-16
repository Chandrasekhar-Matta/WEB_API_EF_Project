using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEB_API_EF_Project.Models;

namespace WEB_API_EF_Project.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        [Route("api/Users/GetAllUsers")]
        public HttpResponseMessage GetAllUsers()
        {
            using (UserDBContext db = new UserDBContext())
            {
                var users = db.Users.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
        }
        // GET api/<controller>/5
        [Route("api/Users/GetUserById")]
        public HttpResponseMessage Get(int userId)
        {
            using (UserDBContext db = new UserDBContext())
            {
                var userByID = db.Users.FirstOrDefault(e => e.UserID == userId);
                return Request.CreateResponse(HttpStatusCode.OK, userByID);
            }
        }

        [Route("api/Users/AddNewUser")]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] User user)
        {
            try
            {
                using (UserDBContext db = new UserDBContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Request.RequestUri +
                   user.UserID.ToString());
                    return message;
                }
            }
            catch(Exception ex) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

        // PUT api/<controller>/5
        [Route("api/Users/UpDateUser")]
        public HttpResponseMessage Put(int userId, [FromBody] User value)
        {
            try
            {
                using (UserDBContext dbContext = new UserDBContext())
                {
                    var entity = dbContext.Users.FirstOrDefault(e => e.UserID == userId);
                    if(entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with Id" + userId.ToString() + " not found to  update");
                    }
                    else
                    {
                        entity.FirstName = value.FirstName;
                        entity.LastName = value.LastName;
                        entity.Email = value.Email;
                        entity.Password = value.Password;
                        entity.PhoneNumber = value.PhoneNumber;
                        entity.DateOfBrith = value.DateOfBrith;
                        entity.Gender = value.Gender;
                        entity.MaritalStatus = value.MaritalStatus;
                        entity.Address = value.Address;
                        entity.Country = value.Country;
                        entity.State = value.State;
                        entity.City = value.City;
                        entity.PinCode = value.PinCode;
                        entity.ImagePath = value.ImagePath;
                        entity.ImageName = value.ImageName;
                        entity.ResumeName = value.ResumeName;
                        entity.ResumePath = value.ResumePath;
                        
                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }

        // DELETE api/<controller>/5
        [Route("api/Users/DeleteUser")]
        public HttpResponseMessage Delete(int userId)
        {
            try
            {
                using (UserDBContext dbContext = new UserDBContext())
                {
                    var entity = dbContext.Users.FirstOrDefault(e => e.UserID == userId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with Id = " + userId.ToString() + " not found to delete");
                    }
                    else
                    {
                        dbContext.Users.Remove(entity);
                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}