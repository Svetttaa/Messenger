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
        private readonly static string _connectionString= @"Server=sveta-lt\SQLExpress;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DBMes.mdf;Database=DBMes;
Trusted_Connection=Yes;";

        UsersRepository repository = new UsersRepository(_connectionString);

        [TestMethod]
        public void ShouldCreateUser()
        {
            //arrange
            var user = new User
            {
                Name = "testUser",
                
                Password = "password"
            };

           //act
            var result = repository.Create(user);

            //asserts
            Assert.AreEqual(user.Name, result.Name);
            
            Assert.AreEqual(user.Password, result.Password);
            Assert.AreEqual(user.Disabled,result.Disabled);

        }
        [TestMethod]
        public void ShouldDeleteUser()
        {
            //arrange
            var user = new User
            {
                Name = "testUser",
               
                Password = "password"
            };

            //act
            repository.Create(user);
            repository.Delete(user.Id);
            var result=new UsersRepository(_connectionString).Get(user.Id);

            //asserts 
            Assert.AreEqual(true, result.Disabled);  
        }
        [TestMethod]
        public void ShouldGetUser()
        {

            //arrange
            var user = new User
            {
                Name = "testUser",
                Password = "password"
            };

            //act
            var result=repository.Create(user);
            User gottenUser = new UsersRepository(_connectionString).Get(result.Id);
            //var gottenUser = repository.Get(result.Id);
           
            //asserts 
            Assert.AreEqual(user.Name, gottenUser.Name);
            Assert.AreEqual(user.Id, gottenUser.Id);
            Assert.AreEqual(user.Password, gottenUser.Password);
            
            Assert.AreEqual(user.Disabled, gottenUser.Disabled);
        }
        //[TestCleanup]
        //public void Clean()
        //{
        //    foreach (var id in _tempUsers)
        //        new UsersRepository(_connectionString).Delete(id);
        //}
    }
}
