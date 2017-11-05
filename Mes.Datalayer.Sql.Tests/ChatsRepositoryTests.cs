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
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            var user2 = new User
            {
                Name = Guid.NewGuid().ToString(),
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
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldDeleteChat()
        {
            var user1 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            var user2 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            UsersRepository userRepository = new UsersRepository(_connectionString);
            user1 = userRepository.Create(user1);
            user2 = userRepository.Create(user2);
            _tempUsers.Add(user1.Id);
            _tempUsers.Add(user2.Id);
            ChatsRepository chatRepository = new ChatsRepository(_connectionString, userRepository);
            var result =chatRepository.Create(_tempUsers, "NewChat");
            chatRepository.Delete(result.Id,user1.Id);
            chatRepository.GetChat(result.Id);
        }
        [TestMethod]
        public void ShouldGetChat()
        {
            var user1 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            var user2 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            UsersRepository userRepository = new UsersRepository(_connectionString);
            user1 = userRepository.Create(user1);
            user2 = userRepository.Create(user2);
            ChatsRepository chatRepository = new ChatsRepository(_connectionString, userRepository);

            var resultCreate=chatRepository.Create(new[] {user1.Id,user2.Id}, "NewChat");
            var resultGet = chatRepository.GetChat(resultCreate.Id);
            Assert.AreEqual("NewChat", resultGet.Name);
        }
        [TestMethod]
        public void ShouldGetUserChats()
        {
            var user1 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            var user2 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            UsersRepository userRepository = new UsersRepository(_connectionString);
            user1 = userRepository.Create(user1);
            user2 = userRepository.Create(user2);
            ChatsRepository chatRepository = new ChatsRepository(_connectionString, userRepository);

            var resultCreate = chatRepository.Create(new[] { user1.Id, user2.Id }, "NewChat");
            var resultChats = chatRepository.GetUserChats(user1.Id);
            Assert.AreEqual("NewChat",resultChats.First().Name);
        }
        [TestMethod]
        public void ShouldGetChatMembers()
        {
            _tempUsers1.Clear();
            var user1 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            var user2 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            UsersRepository userRepository = new UsersRepository(_connectionString);
            user1 = userRepository.Create(user1);
            user2 = userRepository.Create(user2);
            _tempUsers1.Add(user1);
            _tempUsers1.Add(user2);
            ChatsRepository chatRepository = new ChatsRepository(_connectionString, userRepository);

            var resultCreate = chatRepository.Create(new[] { user1.Id, user2.Id }, "NewChat");
            List<User> resultUsers = chatRepository.GetChatMembers(resultCreate.Id).ToList();
            Assert.AreEqual(_tempUsers1.OrderBy(u => u.Id).First().Id, resultCreate.Members.OrderBy(u => u.Id).First().Id);
        }
        [TestMethod]
        public void ShouldDeleteMembers()
        {
            var user1 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            var user2 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            UsersRepository userRepository = new UsersRepository(_connectionString);
            user1 = userRepository.Create(user1);
            user2 = userRepository.Create(user2);
            _tempUsers.Add(user1.Id);
            
            ChatsRepository chatRepository = new ChatsRepository(_connectionString, userRepository);

            var resultCreate = chatRepository.Create(new[] { user1.Id, user2.Id }, "NewChat");
            chatRepository.DeleteMembers(_tempUsers, resultCreate.Id);
            var result = chatRepository.GetChatMembers(resultCreate.Id);
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void ShouldAddMembers()
        {

            var user1 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            var user2 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            var user3 = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            UsersRepository userRepository = new UsersRepository(_connectionString);
            user1 = userRepository.Create(user1);
            user2 = userRepository.Create(user2);
            user3 = userRepository.Create(user3);
            _tempUsers.Add(user3.Id);

            ChatsRepository chatRepository = new ChatsRepository(_connectionString, userRepository);

            var resultCreate = chatRepository.Create(new[] { user1.Id, user2.Id }, "NewChat");
            chatRepository.AddMembers(_tempUsers, resultCreate.Id);
            var result = chatRepository.GetChatMembers(resultCreate.Id);
            Assert.AreEqual(3, result.Count());
        }
    }
}
