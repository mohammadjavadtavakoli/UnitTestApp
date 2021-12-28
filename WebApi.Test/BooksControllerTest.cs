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
        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "117366b8-3541-4ac5-8732-860d698e1111")]
        public void DetailsUnitTest(string validguid, string invalidguid)
        {
            //arrange 
            var mockRepo = new Mock<IBookService>();
            var validItemguid = new Guid(validguid);
            mockRepo.Setup(g => g.GetByID(validItemguid)).Returns(MockData.GetTestBookById());
            var controller = new HomeController(mockRepo.Object);

            //act 
            var result = controller.Details(validItemguid);
            //assert

            var viewresult = Assert.IsType<ViewResult>(result);
            var viewresultValue = Assert.IsAssignableFrom<Book>(viewresult.ViewData.Model);
            Assert.Equal("Managing Oneself", viewresultValue.Title);
            Assert.Equal("Peter Drucker", viewresultValue.Author);
            Assert.Equal(validItemguid, viewresultValue.Id);

            //arrange 
            var invalidItemguid = new Guid(invalidguid);
            mockRepo.Setup(n => n.GetByID(invalidItemguid)).Returns(MockData.InvalidGetTestBookById());

            //act 

            var notFound = controller.Details(invalidItemguid);
            //assert

            var notFoundresult = Assert.IsType<NotFoundResult>(notFound);
        }


        [Fact]
        public void CreateTest()
        {
            //arrange
            var mockRepo = new Mock<IBookService>();
            var controller = new HomeController(mockRepo.Object);
            var newValidItem = new Book()
            {
                Author = "Author",
                Title = "Title",
                Description = "Description"

            };
            //act 

            var result = controller.Create(newValidItem);


            //assert

            var RedirectToActionResultView = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", RedirectToActionResultView.ActionName);
            Assert.Null(RedirectToActionResultView.ControllerName);

            //arrange
            var newInValidItem = new Book()
            {
                Title = "Title",
                Description = "Description"

            };
            controller.ModelState.AddModelError("Author", "The Author Value is Required");

            //act 

            var Invalidresult = controller.Create(newInValidItem);


            //assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(Invalidresult);
            Assert.IsType<SerializableError>(badRequestResult.Value);

        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200")]
        public void DeleteTest(string ValidGuid)
        {
            var mockRepo = new Mock<IBookService>();
            var GUID = new Guid(ValidGuid);
            mockRepo.Setup(c => c.GetByID(GUID)).Returns(MockData.GetTestBookById);
            var controller = new HomeController(mockRepo.Object);
            var result = controller.Delete(GUID, null);

          var RedirectToActionResultView=  Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", RedirectToActionResultView.ActionName);
            Assert.Null(RedirectToActionResultView.ControllerName);

        }

    }
}
