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
    public class PropertyControllerTest
    {
        private PropertyController _controller;
        private Mock<IPropertyService> _service;

        [SetUp]
        public void Setup()
        {
            this._service = new Mock<IPropertyService>();
            this._controller = new PropertyController(this._service.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this._service = null;
            this._controller = null;
        }

        [Test]
        public async Task AddPropertyAsync_GivenStatusResult200_ReturnOk()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 200,
                StatusMessage = "Ok"
            };
            this._service.Setup(x => x.AddPropertyAsync(It.IsAny<AddPropertyDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Property created";

            // act
            var result = await this._controller.AddPropertyAsync(It.IsAny<AddPropertyDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task AddPropertyAsync_GivenStatusResult400_ReturnBadRequest()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 400,
                StatusMessage = "Bad Request"
            };
            this._service.Setup(x => x.AddPropertyAsync(It.IsAny<AddPropertyDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Invalid parameters";

            // act
            var result = await this._controller.AddPropertyAsync(It.IsAny<AddPropertyDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
            var okObjectResult = result as BadRequestObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task AddPropertyAsync_GivenStatusResultDifferent200_ReturnStatusCode500()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 500,
                StatusMessage = "Error"
            };
            this._service.Setup(x => x.AddPropertyAsync(It.IsAny<AddPropertyDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Server Error";
            var expectedStatusCode = 500;

            // act
            var result = await this._controller.AddPropertyAsync(It.IsAny<AddPropertyDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            var okObjectResult = result as ObjectResult;
            Assert.AreEqual(expectedStatusCode, okObjectResult.StatusCode);
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task GetPropertiesAsync_GivenStatusResult200_ReturnOk()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 200,
                StatusMessage = "Ok",
                Data = new List<Property>()
            };
            this._service.Setup(x => x.GetPropertiesAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedResult);

            // act
            var result = await this._controller.GetPropertiesAsync(It.IsAny<string>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(expectedResult.Data, okObjectResult.Value);
        }

        [Test]
        public async Task GetPropertiesAsync_GivenStatusResultDifferent200_ReturnStatusCode500()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 500,
                StatusMessage = "Error"
            };
            this._service.Setup(x => x.GetPropertiesAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Server Error";
            var expectedStatusCode = 500;

            // act
            var result = await this._controller.GetPropertiesAsync(It.IsAny<string>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            var okObjectResult = result as ObjectResult;
            Assert.AreEqual(expectedStatusCode, okObjectResult.StatusCode);
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task UpdatePropertyAsync_GivenStatusResult200_ReturnOk()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 200,
                StatusMessage = "Ok"
            };
            this._service.Setup(x => x.UpdatePropertiesAsync(It.IsAny<UpdatePropertyDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Property updated";

            // act
            var result = await this._controller.UpdatePropertyAsync(It.IsAny<UpdatePropertyDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task UpdatePropertyAsync_GivenStatusResult400_ReturnBadRequest()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 400,
                StatusMessage = "Bad Request"
            };
            this._service.Setup(x => x.UpdatePropertiesAsync(It.IsAny<UpdatePropertyDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Invalid parameters";

            // act
            var result = await this._controller.UpdatePropertyAsync(It.IsAny<UpdatePropertyDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
            var okObjectResult = result as BadRequestObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task UpdatePropertyAsync_GivenStatusResultDifferent200_ReturnStatusCode500()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 500,
                StatusMessage = "Error"
            };
            this._service.Setup(x => x.UpdatePropertiesAsync(It.IsAny<UpdatePropertyDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Server Error";
            var expectedStatusCode = 500;

            // act
            var result = await this._controller.UpdatePropertyAsync(It.IsAny<UpdatePropertyDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            var okObjectResult = result as ObjectResult;
            Assert.AreEqual(expectedStatusCode, okObjectResult.StatusCode);
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task DeletePropertyAsync_GivenStatusResult200_ReturnOk()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 200,
                StatusMessage = "Ok"
            };
            this._service.Setup(x => x.DeletePropertyAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Property deleted";

            // act
            var result = await this._controller.DeletePropertyAsync(It.IsAny<int>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task DeletePropertyAsync_GivenStatusResult404_ReturnNotFound()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 404,
                StatusMessage = "Not Found"
            };
            this._service.Setup(x => x.DeletePropertyAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Property does not exist";

            // act
            var result = await this._controller.DeletePropertyAsync(It.IsAny<int>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);
            var okObjectResult = result as NotFoundObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task DeletePropertyAsync_GivenStatusResultDifferent200_ReturnStatusCode500()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 500,
                StatusMessage = "Error"
            };
            this._service.Setup(x => x.DeletePropertyAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Server Error";
            var expectedStatusCode = 500;

            // act
            var result = await this._controller.DeletePropertyAsync(It.IsAny<int>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            var okObjectResult = result as ObjectResult;
            Assert.AreEqual(expectedStatusCode, okObjectResult.StatusCode);
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task ChangePricePropertyAsync_GivenStatusResult200_ReturnOk()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 200,
                StatusMessage = "Ok"
            };
            this._service.Setup(x => x.UpdatePropertiesAsync(It.IsAny<UpdatePropertyDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Property updated";

            // act
            var result = await this._controller.UpdatePropertyAsync(It.IsAny<UpdatePropertyDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task ChangePricePropertyAsync_GivenStatusResult400_ReturnBadRequest()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 400,
                StatusMessage = "Bad Request"
            };
            this._service.Setup(x => x.UpdatePropertiesAsync(It.IsAny<UpdatePropertyDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Invalid parameters";

            // act
            var result = await this._controller.UpdatePropertyAsync(It.IsAny<UpdatePropertyDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
            var okObjectResult = result as BadRequestObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task ChangePricePropertyAsync_GivenStatusResultDifferent200_ReturnStatusCode500()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 500,
                StatusMessage = "Error"
            };
            this._service.Setup(x => x.UpdatePropertiesAsync(It.IsAny<UpdatePropertyDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Server Error";
            var expectedStatusCode = 500;

            // act
            var result = await this._controller.UpdatePropertyAsync(It.IsAny<UpdatePropertyDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            var okObjectResult = result as ObjectResult;
            Assert.AreEqual(expectedStatusCode, okObjectResult.StatusCode);
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task AddImageFromPropertyAsync_GivenStatusResult200_ReturnOk()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 200,
                StatusMessage = "Ok"
            };
            this._service.Setup(x => x.AddImageFromPropertiesAsync(It.IsAny<AddPropertyImageDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Property image added";

            // act
            var result = await this._controller.AddImageFromPropertyAsync(It.IsAny<AddPropertyImageDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task AddImageFromPropertyAsync_GivenStatusResult400_ReturnBadRequest()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 400,
                StatusMessage = "Bad Request"
            };
            this._service.Setup(x => x.AddImageFromPropertiesAsync(It.IsAny<AddPropertyImageDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Invalid parameters";

            // act
            var result = await this._controller.AddImageFromPropertyAsync(It.IsAny<AddPropertyImageDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
            var okObjectResult = result as BadRequestObjectResult;
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

        [Test]
        public async Task AddImageFromPropertyAsync_GivenStatusResultDifferent200_ReturnStatusCode500()
        {
            // arrange
            var expectedResult = new Result()
            {
                StatusResult = 500,
                StatusMessage = "Error"
            };
            this._service.Setup(x => x.AddImageFromPropertiesAsync(It.IsAny<AddPropertyImageDTO>()))
                .ReturnsAsync(expectedResult);
            var expectedMessage = "Server Error";
            var expectedStatusCode = 500;

            // act
            var result = await this._controller.AddImageFromPropertyAsync(It.IsAny<AddPropertyImageDTO>());

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            var okObjectResult = result as ObjectResult;
            Assert.AreEqual(expectedStatusCode, okObjectResult.StatusCode);
            Assert.AreEqual(expectedMessage, okObjectResult.Value);
        }

    }

}
