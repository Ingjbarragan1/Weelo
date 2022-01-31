using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weelo.API.Common;
using Weelo.API.Controllers;
using Weelo.API.DTOs;
using Weelo.API.Models;
using Weelo.API.Service;

namespace Weelo.API.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class OwnerControllerTests
    {
        private OwnerController _controller;
        private Mock<IOwnerService> _service;

        [SetUp]
        public void Setup()
        {
            this._service = new Mock<IOwnerService>();
            this._controller = new OwnerController(this._service.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this._service = null;
            this._controller = null;
        }

        [Test]
        public void Constructor_GivenNullService_ThrownArgumentNullException()
        {
            // arrange
            OwnerController controller = null;
            IOwnerService service = null;
            var expectedParamName = "service";

            // act
            // assert

            var result = Assert.Throws<ArgumentNullException>(() =>
            {
                controller = new OwnerController(service);
            });

            Assert.IsNull(controller);
            Assert.AreEqual(expectedParamName, result.ParamName);
        }

        [Test]
        public async Task AddOwnerAsync_GivenStatusResult200_ReturnOk()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 200,
                StatusMessage = "Ok"
            };
            this._service.Setup(x => x.AddOwnerAsync(It.IsAny<AddOwnerDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Owner created";

            // act
            var result = await this._controller.AddOwnerAsync(It.IsAny<AddOwnerDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task AddOwnerAsync_GivenStatusResult400_ReturnBadRequest()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 400,
                StatusMessage = "Bad Request"
            };
            this._service.Setup(x => x.AddOwnerAsync(It.IsAny<AddOwnerDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Invalid parameters";

            // act
            var result = await this._controller.AddOwnerAsync(It.IsAny<AddOwnerDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
            var okObjectResult = result as BadRequestObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task AddOwnerAsync_GivenStatusResultDifferent200_ReturnStatusCode500()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 500,
                StatusMessage = "Error"
            };
            this._service.Setup(x => x.AddOwnerAsync(It.IsAny<AddOwnerDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Server Error";
            var expectedStatusCode = 500;

            // act
            var result = await this._controller.AddOwnerAsync(It.IsAny<AddOwnerDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            var okObjectResult = result as ObjectResult;
            Assert.AreEqual(expectedStatusCode, okObjectResult.StatusCode);
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task GetOwnersAsync_GivenStatusResult200_ReturnOk()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 200,
                StatusMessage = "Ok",
                Data = new List<Owner>()
            };
            this._service.Setup(x => x.GetOwnersAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedResult);

            // act
            var result = await this._controller.GetOwnersAsync(It.IsAny<string>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(expectedResult.Data, okObjectResult.Value);
        }

        [Test]
        public async Task GetOwnersAsync_GivenStatusResultDifferent200_ReturnStatusCode500()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 500,
                StatusMessage = "Error"
            };
            this._service.Setup(x => x.GetOwnersAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Server Error";
            var expectedStatusCode = 500;

            // act
            var result = await this._controller.GetOwnersAsync(It.IsAny<string>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            var okObjectResult = result as ObjectResult;
            Assert.AreEqual(expectedStatusCode, okObjectResult.StatusCode);
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task UpdateOwnerAsync_GivenStatusResult200_ReturnOk()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 200,
                StatusMessage = "Ok"
            };
            this._service.Setup(x => x.UpdateOwnerAsync(It.IsAny<UpdateOwnerDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Owner updated";

            // act
            var result = await this._controller.UpdateOwnerAsync(It.IsAny<UpdateOwnerDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task UpdateOwnerAsync_GivenStatusResult400_ReturnBadRequest()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 400,
                StatusMessage = "Bad Request"
            };
            this._service.Setup(x => x.UpdateOwnerAsync(It.IsAny<UpdateOwnerDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Invalid parameters";

            // act
            var result = await this._controller.UpdateOwnerAsync(It.IsAny<UpdateOwnerDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
            var okObjectResult = result as BadRequestObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task UpdateOwnerAsync_GivenStatusResultDifferent200_ReturnStatusCode500()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 500,
                StatusMessage = "Error"
            };
            this._service.Setup(x => x.UpdateOwnerAsync(It.IsAny<UpdateOwnerDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Server Error";
            var expectedStatusCode = 500;

            // act
            var result = await this._controller.UpdateOwnerAsync(It.IsAny<UpdateOwnerDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            var okObjectResult = result as ObjectResult;
            Assert.AreEqual(expectedStatusCode, okObjectResult.StatusCode);
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task DeleteOwnerAsync_GivenStatusResult200_ReturnOk()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 200,
                StatusMessage = "Ok"
            };
            this._service.Setup(x => x.DeleteOwnerAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Owner deleted";

            // act
            var result = await this._controller.DeleteOwnerAsync(It.IsAny<int>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task DeleteOwnerAsync_GivenStatusResult404_ReturnNotFound()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 404,
                StatusMessage = "Not Found"
            };
            this._service.Setup(x => x.DeleteOwnerAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Owner does not exist";

            // act
            var result = await this._controller.DeleteOwnerAsync(It.IsAny<int>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);
            var okObjectResult = result as NotFoundObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task DeleteOwnerAsync_GivenStatusResultDifferent200_ReturnStatusCode500()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 500,
                StatusMessage = "Error"
            };
            this._service.Setup(x => x.DeleteOwnerAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Server Error";
            var expectedStatusCode = 500;

            // act
            var result = await this._controller.DeleteOwnerAsync(It.IsAny<int>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            var okObjectResult = result as ObjectResult;
            Assert.AreEqual(expectedStatusCode, okObjectResult.StatusCode);
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }


    }
}
