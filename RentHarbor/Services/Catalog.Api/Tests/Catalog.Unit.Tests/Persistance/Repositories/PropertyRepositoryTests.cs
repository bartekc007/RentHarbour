using MongoDB.Driver;
using Moq;
using Catalog.Persistance.Repositories.MongoDb;
using RentHarbor.MongoDb.Data;
using RentHarbor.MongoDb.Entities;

public class PropertyRepositoryTests
{
    private readonly Mock<IMongoContext> _contextMock;
    private readonly Mock<IMongoCollection<Property>> _propertyCollectionMock;
    private readonly PropertyRepository _repository;

    public PropertyRepositoryTests()
    {
        _contextMock = new Mock<IMongoContext>();
        _propertyCollectionMock = new Mock<IMongoCollection<Property>>();
        _contextMock.SetupGet(ctx => ctx.Properties).Returns(_propertyCollectionMock.Object);
        _repository = new PropertyRepository(_contextMock.Object);
    }

    [Fact]
    public async Task GetPropertyByIdAsync_ShouldReturnProperty_WhenPropertyExists()
    {
        // Arrange
        var propertyId = "1";
        var property = new Property { Id = propertyId, Name = "Property1" };

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

        _propertyCollectionMock.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Property>>(), It.IsAny<FindOptions<Property>>(), default))
            .ReturnsAsync(mockCursor.Object);

        // Act
        var result = await _repository.GetPropertyByIdAsync(propertyId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(propertyId, result.Id);
        Assert.Equal("Property1", result.Name);
    }

    [Fact]
    public async Task GetPropertyByIdAsync_ShouldReturnNull_WhenPropertyDoesNotExist()
    {
        // Arrange
        var propertyId = "1";

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

        _propertyCollectionMock.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Property>>(), It.IsAny<FindOptions<Property>>(), default))
            .ReturnsAsync(mockCursor.Object);

        // Act
        var result = await _repository.GetPropertyByIdAsync(propertyId);

        // Assert
        Assert.Null(result);
    }
}
