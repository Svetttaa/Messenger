using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mes.Model;
using Mes.DataLayer.Sql;

namespace Mes.Datalayer.Sql.Tests
{
    [TestClass]
    public class UsersReposytoryTests
    {
        private readonly string _connectionString= @"Server=sveta-lt\SQLExpress;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DBMes.mdf;Database=DBMes;
Trusted_Connection=Yes;";
        private readonly List<Guid> _tempUsers = new List<Guid>();//список с id юзеров
        [TestMethod]
        public void ShouldCreateUser()
        {
            //arrange подготовка
            var user = new User
            {
                Name = "testUser",
                Avatar = Encoding.UTF8.GetBytes("ava"),
                Password = "password",
                Disabled=false
            };

            //act действия
            var repository = new UsersRepository(_connectionString);
            var result = repository.Create(user);

            _tempUsers.Add(result.Id);

            //asserts проверка результатов
            Assert.AreEqual(user.Name, result.Name);
            Assert.AreEqual(user.Avatar, result.Avatar);
            Assert.AreEqual(user.Password, result.Password);
            Assert.AreEqual(user.Disabled, result.Disabled);
        }
        [TestMethod]
        public void ShouldDeleteUser()
        {
            //arrange подготовка
            var user = new User
            {
                Name = "testUser",
                Avatar = Encoding.UTF8.GetBytes("ava"),
                Password = "password",
                Disabled = false
            };

            //act действия
            var repository = new UsersRepository(_connectionString);
            var newuser=repository.Create(user);
            _tempUsers.Add(newuser.Id);
            repository.Delete(newuser.Id);

            //asserts проверка результатов
            //Assert.AreEqual(user.Disabled, false);
            Assert.AreEqual(true, newuser.Disabled);
            
        }
        //[TestCleanup]
        //public void Clean()
        //{
        //    foreach (var id in _tempUsers)
        //        new UsersRepository(_connectionString).Delete(id);
        //}
    }
}
