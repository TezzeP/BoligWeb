using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoligWebApi.Controllers;
using BoligWebApi.Models;
using Xunit;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;

using Moq;


namespace BoligWebTest
{
    public class UnitTest1
    {
        Mock<RolesController> boliMock = new();

        [Fact]
        public async Task _GetPostById_Return_OkResult()
        {
            //Arrange  
            boliMock.Setup(repo => repo.GetRoles());
            var fakeRoles = A.CollectionOfDummy<Role>(5).AsEnumerable();
            var ds = A.Fake<RolesController>();
            A.CallTo(() => ds.GetRole(5).Result);
            //var postId = 2;
            int testSessionId = 123;
            string testName = "test name";
            string testDescription = "test description";
            var testSession = GetTestSession();
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
                .ReturnsAsync(testSession);
            var controller = new IdeasController(mockRepo.Object);

            var newIdea = new NewIdeaModel()
            {
                Description = testDescription,
                Name = testName,
                SessionId = testSessionId
            };

            //Act
            var result = await controller.Create(newIdea);


            //var data = await boliMock.Object.GetRole(postId);

            //Assert
            //var okResult = Assert.IsType<OkObjectResult>(result);
            var returnSession = Assert.IsType<BrainstormSession>(okResult.Value);
            mockRepo.Verify();
            Assert.Equal(2, returnSession.Ideas.Count());
            Assert.Equal(testName, returnSession.Ideas.LastOrDefault().Name);
            Assert.Equal(testDescription, returnSession.Ideas.LastOrDefault().Description);
            Assert.IsType<OkObjectResult>(data);
        }


        [Fact]
        public async Task RoleTest()
        {
            //arrange 
            boliMock.Setup(repo => repo.GetRole(5));
            
            //Act
            var result = await boliMock.Object.GetRole(4)
                ;
            //Assert
            //Assert.Equal(boliMock. );
            
            
            //boliMock.Setup(m => m.GetRoles(It.))
            //    .ReturnsAsync(204);
            //var _controller = new RolesController(boliMock.Object);
            //_controller.GetRoles();
            //boliMock.Verify(x => x.Roles, Times.Once);


        }


        //[]
        //public void Setup()
        //{
        //    _person = new Person
        //    {
        //        //Id=1,If Id is not designed by using IDENTITY (1, 1),you need to add this line
        //        FirstName = "David",
        //        LastName = "Johnson",
        //        Email = "dj@dj.com"
        //    };
        //}
        //[Test]
        //public void Test1()
        //{
        //    var context = new WebApi2Context(dbContextOptions);
        //    PeopleController person = new PeopleController(context);
        //    var data = person.PostPerson(_person);
        //    var response = data.Result as CreatedAtActionResult;
        //    var item = response.Value as Person;
        //    Assert.AreEqual("David", item.FirstName);
        //    Assert.AreEqual("Johnson", item.LastName);
        //    Assert.AreEqual("dj@dj.com", item.Email);

        //}
        //[Fact]
        //public void Test1()
        //{


            //arrange
           // var controller = new RolesController(context);
           // var dataStore = A.Fake<DokumentsController>();
           // var controller = new DokumentsController(dataStore);
           // act
           //assert

        
        //public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        //{
        //    RolesController rolesController = new RolesController();
        //    // Arrange
        //    v
            
        //    mockRepo.Setup(repo => repo.ListAsync())
        //        .ReturnsAsync(GetTestSessions());
        //    var controller = new HomeController(mockRepo.Object);

        //    // Act
        //    var result = await controller.Index();

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
        //        viewResult.ViewData.Model);
        //    Assert.Equal(2, model.Count());
        //}

        //RolesController _controller;


        //[Fact]
        //public async Task GetAllTest()
        //{

        //    //Arrange
        //    var mockRepo = new Mock<ILogger>();
        //    mockRepo.Setup(repo => repo.())
        //        .ReturnsAsync(GetTestSessions());
        //    var controller = new HomeController(mockRepo.Object);

        //    //Act
        //    var result = _controller.GetRole(2);
        //    //Assert
        //    Assert.IsType<List<Role>>(result.Result);

        //    var list = result.Result as List<Role>;

        //    Assert.IsType<List<Role>>(list.Count);



        //    var listBooks = list.Value as List<Role>;

        //    Assert.Equal(5, listBooks.Count);
        //}
    }
}

