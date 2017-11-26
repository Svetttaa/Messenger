using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mes.Model;
using Mes.DataLayer.Sql;

namespace Mes.Datalayer.Sql.Tests
{
    public static class Helper
    {
        public static User NewUser()
        {
            return new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
        }
        public static User CreateUser(string connectionString,User user)
        {
            UsersRepository repository = new UsersRepository(connectionString);
            return repository.Create(user);
        }
    }
}
