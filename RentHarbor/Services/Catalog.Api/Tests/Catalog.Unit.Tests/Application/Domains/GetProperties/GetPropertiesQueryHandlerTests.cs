using AutoMapper;
using Moq;
using MongoDB.Driver;
using RentHarbor.MongoDb.Data;
using RentHarbor.MongoDb.Entities;
using Catalog.Application.Domains.Property.GetProperties;
using Catalog.Application.Domains.Property.GetProperties.Dto;

namespace Catalog.Unit.Tests.Application.Domains.GetProperties
{
    public class GetPropertiesQueryHandlerTests
    {
        private readonly Mock<IMongoContext> _contextMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetPropertiesQueryHandler _handler;
        private readonly List<Property> _properties;

        public GetPropertiesQueryHandlerTests()
        {
            _contextMock = new Mock<IMongoContext>();
            _mapperMock = new Mock<IMapper>();

            _handler = new GetPropertiesQueryHandler(_contextMock.Object, _mapperMock.Object);

            _properties = new List<Property>
        {
            new Property { Id = "1", Name = "Property1", Address = new Address { City = "SampleCity" } },
            new Property { Id = "2", Name = "Property2", Address = new Address { City = "SampleCity" } }
        };
        }

        [Fact]
        public async Task Handle_ShouldReturnProperties_WhenPropertiesExist()
        {
            var query = new GetPropertiesQuery { City = "SampleCity" };

            var mockCollection = new Mock<IMongoCollection<Property>>();
            var mockCursor = new Mock<IAsyncCursor<Property>>();
            mockCursor.Setup(_ => _.Current).Returns(_properties);
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

            var propertyDtos = new List<PropertyDto>
        {
            new PropertyDto { Id = "1", Name = "Property1", Address = new AddressDto { City = "SampleCity" } },
            new PropertyDto { Id = "2", Name = "Property2", Address = new AddressDto { City = "SampleCity" } }
        };

            _mapperMock.Setup(m => m.Map<List<PropertyDto>>(_properties)).Returns(propertyDtos);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Property1", result.Data[0].Name);
            Assert.Equal("Property2", result.Data[1].Name);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoPropertiesExist()
        {
            var query = new GetPropertiesQuery { City = "NonExistingCity" };
            var emptyProperties = new List<Property>();

            var mockCollection = new Mock<IMongoCollection<Property>>();
            var mockCursor = new Mock<IAsyncCursor<Property>>();
            mockCursor.Setup(_ => _.Current).Returns(emptyProperties);
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

            var propertyDtos = new List<PropertyDto>();

            _mapperMock.Setup(m => m.Map<List<PropertyDto>>(emptyProperties)).Returns(propertyDtos);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
        }
    }
}

