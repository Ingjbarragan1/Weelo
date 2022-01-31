using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weelo.API.Database;
using Weelo.API.Service;

namespace Weelo.API.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class PropertyServiceTests
    {
        private PropertyService _service;
        private Mock<DatabaseContext> _context;

        [SetUp]
        public void Setup()
        {
            this._context = new Mock<DatabaseContext>();
            this._service = new PropertyService(this._context.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this._context = null;
            this._service = null;
        }
    }
}
