using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mes.Model;
using Mes.DataLayer.Sql;
using Mes.Datalayer.Sql.Tests;

namespace Mes.DataLayer.Sql.Tests
{
    [TestClass]
    public class UsersRepositoryTests
    {
        private readonly static string _connectionString = @"Server=localhost\SQLExpress;Initial Catalog=DBMes;
        Trusted_Connection=True;";

        UsersRepository repository = new UsersRepository(_connectionString);

        [TestMethod]
        public void ShouldCreateUser()
        {
            User user = Datalayer.Sql.Tests.Helper.NewUser();
            User result = Datalayer.Sql.Tests.Helper.CreateUser(_connectionString, user);

            Assert.AreEqual(user.Name, result.Name);
            Assert.AreEqual(user.Password, result.Password);
            Assert.AreEqual(user.Disabled, result.Disabled);
        }
        [TestMethod]
        public void ShouldDeleteUser()
        {
            User user = Datalayer.Sql.Tests.Helper.CreateUser(_connectionString, Datalayer.Sql.Tests.Helper.NewUser());
            repository.Delete(user.Id);
            var result = new UsersRepository(_connectionString).Get(user.Id);
            Assert.AreEqual(true, result.Disabled);
        }
        [TestMethod]
        public void ShouldGetUser()
        {
            User user = Datalayer.Sql.Tests.Helper.NewUser();
            User result = Datalayer.Sql.Tests.Helper.CreateUser(_connectionString, user);
            User gottenUser = new UsersRepository(_connectionString).Get(result.Id);

            Assert.AreEqual(user.Name, gottenUser.Name);
            Assert.AreEqual(user.Id, gottenUser.Id);
            Assert.AreEqual(user.Password, gottenUser.Password);
            Assert.AreEqual(user.Disabled, gottenUser.Disabled);
        }
        [TestMethod]
        public void ShouldChangeName()
        {
            User result = Datalayer.Sql.Tests.Helper.CreateUser(_connectionString, Datalayer.Sql.Tests.Helper.NewUser());
            repository.ChangeName(result.Id, "NewName");
            User gottenUser = new UsersRepository(_connectionString).Get(result.Id);

            Assert.AreEqual("NewName", gottenUser.Name);
        }
        [TestMethod]
        public void ShouldChangePassword()
        {
            User result = Datalayer.Sql.Tests.Helper.CreateUser(_connectionString, Datalayer.Sql.Tests.Helper.NewUser());
            repository.ChangePassword(result.Id, "NewPass");
            User gottenUser = new UsersRepository(_connectionString).Get(result.Id);

            Assert.AreEqual("NewPass", gottenUser.Password);
        }

        //[TestMethod]
        //public void ShouldChangeAvatar()
        //{
        //    User result = Datalayer.Sql.Tests.Helper.CreateUser(_connectionString, Datalayer.Sql.Tests.Helper.NewUser());
        //    Image image = new Bitmap(600, 600);
        //    //repository.ChangeAvatar(result.Id, image);
        //}
       
        [TestMethod]
        public void ShouldLoginTrue()
        {
            User user = Datalayer.Sql.Tests.Helper.NewUser();
            Datalayer.Sql.Tests.Helper.CreateUser(_connectionString, user);
            var result = repository.Login(user.Name, "password");
            Assert.AreEqual(user.Name, result.Name);
            Assert.AreEqual(user.Password, result.Password);
        }
        [TestMethod]
        public void ShouldSearchUser()
        {
            User user1 = Datalayer.Sql.Tests.Helper.CreateUser(_connectionString, Datalayer.Sql.Tests.Helper.NewUser());
            repository.ChangeName(user1.Id, "Pasha");
            User user2 = Datalayer.Sql.Tests.Helper.CreateUser(_connectionString, Datalayer.Sql.Tests.Helper.NewUser());
            List<User> users = new List<User>();
            users = (List<User>)new UsersRepository(_connectionString).SearchUsers(user2.Id, "Pasha");
            Assert.AreEqual(1, users.Count);

        }
    }
}
