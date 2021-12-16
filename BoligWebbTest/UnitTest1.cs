using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoligWebApi.Controllers;
using BoligWebApi.Exeptions;
using BoligWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace BoligWebbTest
{
    public class UnitTest1 
    {
        private static DbContextOptions<BoligWebContext> options;
        private static BoligWebContext context;
        private static RolesController roleController;
        private static List<Role> listRole;

        public UnitTest1()
        {
            options = new DbContextOptionsBuilder<BoligWebContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging().Options;
            context = new BoligWebContext(options);
            roleController = new RolesController(context);
            listRole = new List<Role>
            {
                new Role() {Id = 4, RoleName = "RoleName4"},
                new Role() {Id = 5, RoleName = "RoleName5"},
                new Role() {Id = 6, RoleName = "RoleName6"},
                new Role() {Id = 7, RoleName = "RoleName7"},
            };
        }

        //Check of the GetRoles method
        [Fact]
        public async Task CheckIfAnyRoleExists()
        {                       
            foreach (Role role in listRole)
            {
                await roleController.PostRole(role);
            }

            var result = roleController.GetRoles().Result.Value;
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void CheckIfNORolesExist()
        {
            

            var result = roleController.GetRoles().Result.Value;
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        //Check the GetRole method
        [Fact]
        public async Task CheckIfThrowsNotPossitiveNumberExeptionOnGet()
        {
            //arrange
            foreach (Role role in listRole)
            {
                await roleController.PostRole(role);
            }

            _ = Assert.ThrowsAsync<NotPosstiveNumberExeption>(async () => await roleController.GetRole(-1));

        }
        [Fact]
        public async Task CheckIfRoleWitheThisIdDosentExist()
        {

            //arrange
            foreach (Role role in listRole)
            {
                await roleController.PostRole(role);
            }

            var result = await roleController.GetRole(5464);
            var status = result.Result as StatusCodeResult;
            Console.WriteLine(status);
            Console.WriteLine(result);
            Assert.Equal(404, status.StatusCode);
        }



        [Fact]
        public async Task CheckIfRoleWitheThisIdExist()
        {
            //arrange
            foreach (Role role in listRole)
            {
                await roleController.PostRole(role);
            }

            var result = roleController.GetRole(4);
            var valu = result.Result.Value;
           
            Assert.NotNull(valu);
        }

        [Fact]
        public async Task CheckIfThrowsNotPossitiveNumberExeptionOnDelete()
        {
            //arrange
            foreach (Role role in listRole)
            {
                await roleController.PostRole(role);
            }

            _ = Assert.ThrowsAsync<NotPosstiveNumberExeption>(async () => await roleController.DeleteRole(-1));  //arrange
            
        }
        [Fact]
        public async Task CheckIfRoleWitheThisIdDosentExistOndelete()
        {

            //arrange
            foreach (Role role in listRole)
            {
                await roleController.PostRole(role);
            }

            var result = await roleController.DeleteRole(5464);
            var status = result.Result as StatusCodeResult;
            Assert.Equal(404, status.StatusCode);
        }

        [Fact]
        public async Task CheckIfRoleWitheThisIdExistonDelete()
        {

            //arrange
            foreach (Role role in listRole)
            {
                await roleController.PostRole(role);
            }

            var result = await roleController.DeleteRole(5);
            var status = result.Result as StatusCodeResult;
            Assert.Equal(200,status.StatusCode);
        }




        //act

        // Assert.NotNull(result.Result.Value);
        //Mock<PostsController> postControllerMock = new Mock<PostsController>();

        //var result =await postControllerMock.Object.GetPosts();


        //var status = result.Result as StatusCodeResult;

        //Assert.Equal(200, status.StatusCode);



    }
}   

