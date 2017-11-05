using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mes.Model;
using Mes.DataLayer.Sql;

namespace Mes.DataLayer.Sql.Tests
{
    [TestClass]
    public class UsersRepositoryTests
    {
        private readonly static string _connectionString = @"Server=sveta-lt\SQLExpress;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DBMes.mdf;Database=DBMes;
Trusted_Connection=Yes;";

        UsersRepository repository = new UsersRepository(_connectionString);

        [TestMethod]
        public void ShouldCreateUser()
        {
            //arrange
            var user = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };

            //act
            var result = repository.Create(user);

            //asserts
            Assert.AreEqual(user.Name, result.Name);
            Assert.AreEqual(user.Password, result.Password);
            Assert.AreEqual(user.Disabled, result.Disabled);

        }
        [TestMethod]
        public void ShouldDeleteUser()
        {
            //arrange
            var user = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };

            //act
            repository.Create(user);
            repository.Delete(user.Id);
            var result = new UsersRepository(_connectionString).Get(user.Id);

            //asserts 
            Assert.AreEqual(true, result.Disabled);
        }
        [TestMethod]
        public void ShouldGetUser()
        {
            //arrange
            var user = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };

            //act
            var result = repository.Create(user);
            User gottenUser = new UsersRepository(_connectionString).Get(result.Id);
            //var gottenUser = repository.Get(result.Id);

            //asserts 
            Assert.AreEqual(user.Name, gottenUser.Name);
            Assert.AreEqual(user.Id, gottenUser.Id);
            Assert.AreEqual(user.Password, gottenUser.Password);
            Assert.AreEqual(user.Disabled, gottenUser.Disabled);
        }
        [TestMethod]
        public void ShouldChangeName()
        {
            //arrange
            var user = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            //act
            var result = repository.Create(user);
            repository.ChangeName(result.Id, "NewName");
            User gottenUser = new UsersRepository(_connectionString).Get(result.Id);
            //asserts 
            Assert.AreEqual("NewName", gottenUser.Name);
        }
        [TestMethod]
        public void ShouldChangePassword()
        {
            //arrange
            var user = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            //act
            var result = repository.Create(user);
            repository.ChangePassword(result.Id, "NewPass");
            User gottenUser = new UsersRepository(_connectionString).Get(result.Id);
            //asserts 
            Assert.AreEqual("NewPass", gottenUser.Password);
        }

        [TestMethod]
        public void ShouldChangeAvatar()
        {            //arrange
            var user = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            //act
            var result = repository.Create(user);

            Image image = new Bitmap(600, 600);

            repository.ChangeAvatar(result.Id, image);
        }
        [TestMethod]
        public void ShouldLoginFalse()
        {            
            try
            {
                var result = repository.Login("login", "password1");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Неверное имя или пароль", e.Message);
            }
        }
        [TestMethod]
        public void ShouldLoginTrue()
        {            //arrange
            var user = new User
            {
                Name = Guid.NewGuid().ToString(),
                Password = "password"
            };
            //act
            repository.Create(user);
            var result = repository.Login(user.Name, "password");
            Assert.AreEqual(user.Name, result.Name);
            Assert.AreEqual(user.Password, result.Password);
        }
        //[TestCleanup]
        //public void Clean()
        //{
        //    foreach (var id in _tempUsers)
        //        new UsersRepository(_connectionString).Delete(id);
        //}
    }
}
