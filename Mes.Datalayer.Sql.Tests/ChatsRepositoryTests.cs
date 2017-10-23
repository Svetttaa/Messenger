using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mes.Model;
using Mes.DataLayer.Sql;
using System.Linq;
using Mes.DataLayer;
using System.Collections.Generic;


namespace Mes.DataLayer.Sql.Tests
{
    [TestClass]
    public class ChatsRepositoryTests
    {
        private readonly static string _connectionString = @"Server=localhost\SQLExpress;Initial Catalog=DBMes;
        Trusted_Connection=True;";
        private readonly List<Guid> _tempUsers = new List<Guid>();
        private readonly List<User> _tempUsers1 = new List<User>();
        [TestMethod]
        public void ShouldCreateChat()
        {
            var user1 = new User
            {
                Name = "testUser1",
                Password = "password"
            };
            var user2 = new User
            {
                Name = "testUser2",
                Password = "password"
            };
            UsersRepository userRepository = new UsersRepository(_connectionString);
            var result1 = userRepository.Create(user1);
            var result2 = userRepository.Create(user2);
            _tempUsers.Add(result1.Id);
            _tempUsers.Add(result2.Id);
            ChatsRepository chatRepository = new ChatsRepository(_connectionString, userRepository);
            var result = chatRepository.Create(_tempUsers, "NewChat");
            _tempUsers1.Add(userRepository.Get(_tempUsers[0]));
            _tempUsers1.Add(userRepository.Get(_tempUsers[1]));
            Assert.AreEqual("NewChat", result.Name);
            //Assert.AreEqual(_tempUsers1, result.Members.ToList());
            //Assert.IsTrue(result.Members.ToList().SequenceEqual(_tempUsers1.ToList()));
            Assert.AreEqual(_tempUsers1.OrderBy(u=>u.Id).First().Id, result.Members.OrderBy(u => u.Id).First().Id);
            Assert.AreEqual(_tempUsers1.OrderBy(u => u.Id).Last().Id, result.Members.OrderBy(u => u.Id).Last().Id);

        }
    }
}
