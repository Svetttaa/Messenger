using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mes.Model;
using Mes.DataLayer.Sql;
using System.Linq;
using Mes.DataLayer;
using System.Collections.Generic;

namespace Mes.Datalayer.Sql.Tests
{
    [TestClass]
    public class MessagesRepositoryTests
    {
        private readonly static string _connectionString = @"Server=localhost\SQLExpress;Initial Catalog=DBMes;
        Trusted_Connection=True;";
        // private readonly List<Guid> _tempUsers = new List<Guid>();
        // private readonly List<User> _tempUsers1 = new List<User>();
        [TestMethod]
        public void ShouldSend()
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

            var resultCreate = chatRepository.Create(new[] { user1.Id, user2.Id }, "HellowChat");

            Message message = new Message
            {
                Text = "HelloWorld",
                UserId = user1.Id,
                ChatId = resultCreate.Id,
            };
            MessagesRepository messagesRepository = new MessagesRepository(_connectionString, userRepository, chatRepository);
            var result = messagesRepository.Send(message.UserId, message.ChatId, message.Text, null);
            Assert.AreEqual(message.Text, result.Text);
            Assert.IsNotNull(result.Id);
            Assert.IsNotNull(result.Date);
        }
        [TestMethod]
        public void SholdGetAmount()
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

            Message message1 = new Message
            {
                Text = "message",
                UserId = user1.Id,
                ChatId = resultCreate.Id,
            };
            Message message2 = new Message
            {
                Text = "message2",
                UserId = user2.Id,
                ChatId = resultCreate.Id,
            };
            Message message3 = new Message
            {
                Text = "message3",
                UserId = user1.Id,
                ChatId = resultCreate.Id,
            };
            MessagesRepository messagesRepository = new MessagesRepository(_connectionString, userRepository, chatRepository);
            message1 = messagesRepository.Send(message1.UserId, message1.ChatId, message1.Text, null);
            message2 = messagesRepository.Send(message2.UserId, message2.ChatId, message2.Text, null);
            message3 = messagesRepository.Send(message3.UserId, message3.ChatId, message3.Text, null);
            var result=messagesRepository.GetAmount(resultCreate.Id,1,1);
            Assert.AreEqual(1, result.Count());
        }

    }
}
