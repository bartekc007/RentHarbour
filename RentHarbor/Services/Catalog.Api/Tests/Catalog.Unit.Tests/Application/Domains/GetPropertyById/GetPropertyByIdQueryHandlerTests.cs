using AutoMapper;
using Moq;
using Catalog.Application.Domains.Property.GetPropertyById;
using Catalog.Application.Domains.Property.GetPropertyById.Dto;
using MongoDB.Driver;
using RentHarbor.MongoDb.Data;
using RentHarbor.MongoDb.Entities;

namespace Catalog.Unit.Tests.Application.Domains.GetPropertyById
{
    public class GetPropertyByIdQueryHandlerTests
    {
        private readonly Mock<IMongoContext> _contextMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetPropertyByIdQueryHandler _handler;

        public GetPropertyByIdQueryHandlerTests()
        {
            _contextMock = new Mock<IMongoContext>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetPropertyByIdQueryHandler(_contextMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnProperty_WhenPropertyExists()
        {
            // Arrange
            var query = new GetPropertyByIdQuery("1");
            var property = new Property { Id = "1", Name = "Property1", Address = new Address { City = "SampleCity" } };
            var propertyDto = new GetPropertyByIdDto { Id = "1", Name = "Property1", Address = new Address { City = "SampleCity" } };

            var mockCollection = new Mock<IMongoCollection<Property>>();
            var mockCursor = new Mock<IAsyncCursor<Property>>();
            mockCursor.Setup(_ => _.Current).Returns(new List<Property> { property });
            mockCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

            mockCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Property>>(), It.IsAny<FindOptions<Property>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            _contextMock.SetupGet(ctx => ctx.Properties).Returns(mockCollection.Object);
            _mapperMock.Setup(m => m.Map<GetPropertyByIdDto>(property)).Returns(propertyDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("Property1", result.Name);
            Assert.Equal("SampleCity", result.Address.City);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenPropertyDoesNotExist()
        {
            // Arrange
            var query = new GetPropertyByIdQuery("2");
            var mockCollection = new Mock<IMongoCollection<Property>>();
            var mockCursor = new Mock<IAsyncCursor<Property>>();
            mockCursor.Setup(_ => _.Current).Returns(new List<Property>());
            mockCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

            mockCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Property>>(), It.IsAny<FindOptions<Property>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            _contextMock.SetupGet(ctx => ctx.Properties).Returns(mockCollection.Object);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}

