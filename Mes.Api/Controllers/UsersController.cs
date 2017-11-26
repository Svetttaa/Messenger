using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mes.Model;
using Mes.DataLayer.Sql;

namespace Mes.Api.Controllers
{
    public class UsersController : ApiController
    {
        private static string  connectionString= @"Server=localhost\SQLExpress;Initial Catalog=DBMes;
        Trusted_Connection=True;";
        UsersRepository usersRepository = new UsersRepository(connectionString);

        [HttpGet, Route("api/users/{id}")]
        public object GetUser(Guid id)
        {
            return usersRepository.Get(id);
        }

        [HttpPost,Route("api/users/register")]
        public object Register([FromBody]User user)
        {
            return usersRepository.Create(user);
        }

        [HttpDelete, Route("api/users/{id}")]
        public void Delete(Guid id)
        {
            usersRepository.Delete(id);
        }

        [HttpPost,Route("api/users/changeName")]
        public void ChangeName([FromBody] User user)
        {
           usersRepository.ChangeName(user.Id, user.Name);
        }

        [HttpPost, Route("api/users/changePassword")]
        public void ChangePassword([FromBody] User user)
        {
             usersRepository.ChangePassword(user.Id, user.Password);
        }

        [HttpPost, Route("api/users/login")]
        public object Login([FromBody] User user)
        {
            return usersRepository.Login(user.Name, user.Password);
        }

        [HttpPost, Route("api/users/avatar")]
        public void Ava([FromBody] User user)
        { 
            usersRepository.ChangeAvatar(user.Id,user.Ava);
        }

        [HttpGet, Route("api/users/searchUsers/{id}/{name}")]
        public IEnumerable<User> SearchUsers(Guid id, string name)
        {
            return usersRepository.SearchUsers(id,name);
        }

    }
}
