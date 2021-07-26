using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Scalable_Web.Controllers;
using Scalable_Web.Data;
using Scalable_Web.DataManager;
using Scalable_Web.DTO.Request;
using Scalable_Web.DTO.Response;
using Scalable_Web.Models;
using Scalable_Web.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Scalable_Web.Test
{
    public class DifferenceUnitTestController
    {
        private DifferenceManager repository;
        private static IMapper _mapper;

        public static DbContextOptions<Scaleble_Web_Context> dbContextOptions { get; }
        public static string connectionString = "server=databases-dev.database.windows.net;database=scalable_web_db;User ID=arlind;password=h(YB9Wa\\;";

        public DifferenceUnitTestController()
        {
            var context = new Scaleble_Web_Context(dbContextOptions);
            //DummyDataDBInitializer db = new DummyDataDBInitializer();
            //db.Seed(context);


            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ReturnDifferenceReponse());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            repository = new DifferenceManager(context, _mapper);

        }

        static DifferenceUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<Scaleble_Web_Context>()
                .UseSqlServer(connectionString)
                .Options;
        }

        [Fact]
        public async void Task_DifferenceById_Return_OkResult()
        {
            //Arrange  
            var controller = new DifferenceController(repository);
            var id = 1;

            //Act  
            var data = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_DifferenceById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new DifferenceController(repository);
            var id = 99;

            //Act  
            var data = await controller.Get(id);

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data);
        }

        [Fact]
        public async void Task_DifferenceById_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new DifferenceController(repository);
            int id = 0;

            //Act  
            var data = await controller.Get(id);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }


        [Fact]
        public async void Task_DifferenceById_MatchResult()
        {
            //Arrange  
            var controller = new DifferenceController(repository);
            int id = 1;

            //Act  
            var data = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var post = okResult.Value.Should().BeAssignableTo<DifferenceResponseDTO>().Subject;

            Assert.Equal(Encoding.ASCII.GetBytes("Hello there"), post.Result);
        }

        [Fact]
        public async void Task_Add_ValidData_Left_Return_OkResult()
        {
            //Arrange  
            var controller = new DifferenceController(repository);
            var post = new DifferencePostLeft() { Id = 1, Left = Encoding.ASCII.GetBytes("") };

            //Act  
            var data = await controller.PostLeftAsync(post);

            //Assert  
            Assert.IsType<CreatedAtActionResult>(data);
        }
        [Fact]
        public async void Task_Add_ValidData_Right_Return_OkResult()
        {
            //Arrange  
            var controller = new DifferenceController(repository);
            var post = new DifferencePostRight() { Id = 1, Right = Encoding.ASCII.GetBytes("") };

            //Act  
            var data = await controller.PostRightAsync(post);

            //Assert  
            Assert.IsType<CreatedAtActionResult>(data);
        }


        [Fact]
        public async void Task_Add_InvalidData_Left_Return_BadRequest()
        {
            //Arrange  
            var controller = new DifferenceController(repository);
            DifferencePostLeft post = new DifferencePostLeft() { Id = 1, Left = Encoding.ASCII.GetBytes("------------") };

            //Act              
            var data = await controller.PostLeftAsync(post);

            //Assert  
            Assert.IsType<CreatedAtActionResult>(data);
        }

        [Fact]
        public async void Task_Add_InvalidData_Right_Return_BadRequest()
        {
            //Arrange  
            var controller = new DifferenceController(repository);
            DifferencePostRight post = new DifferencePostRight() { Id = 1, Right = Encoding.ASCII.GetBytes("--------------") };

            //Act              
            var data = await controller.PostRightAsync(post);

            //Assert  
            Assert.IsType<CreatedAtActionResult>(data);
        }

    }
}
