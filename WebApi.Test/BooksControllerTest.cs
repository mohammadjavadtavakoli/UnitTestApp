using ASPCoreMVC.App.Controllers;
using ASPCoreMVC.App.Data.MockData;
using ASPCoreMVC.App.Data.Models;
using ASPCoreMVC.App.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebApi.Test
{
    public class BooksControllerTest
    {
        [Fact]
        public void IndexUnitTest()
        {
            //arrange 
            var mockRepo = new Mock<IBookService>();
            mockRepo.Setup(n => n.GetAll()).Returns(MockData.GetTestBookItems());
            var controller = new HomeController(mockRepo.Object);
            // act
            var result = controller.Index();
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);

            //var viewResult = result as ViewResult;

            var viewResultBooks = Assert.IsAssignableFrom<List<Book>>(viewResult.ViewData.Model);

            //var viewResultBooks = viewResult.ViewData.Model as List<Book>;

            Assert.Equal(5, viewResultBooks.Count);

        }
    }
}
