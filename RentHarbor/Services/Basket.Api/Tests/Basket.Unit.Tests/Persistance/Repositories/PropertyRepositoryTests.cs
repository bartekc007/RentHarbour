using Moq;
using MongoDB.Driver;
using MongoDB.Bson;
using RentHarbor.MongoDb.Data;
using RentHarbor.MongoDb.Entities;
using Basket.Persistance.Repositories;

namespace Basket.Unit.Tests.Persistance.Repositories
{
    public class PropertyRepositoryTests
    {
        private readonly Mock<IMongoContext> _contextMock;
        private readonly Mock<IMongoCollection<Property>> _propertyCollectionMock;
        private readonly Mock<IMongoCollection<UserProperty>> _userPropertyCollectionMock;
        private readonly PropertyRepository _repository;

        public PropertyRepositoryTests()
        {
            _contextMock = new Mock<IMongoContext>();
            _propertyCollectionMock = new Mock<IMongoCollection<Property>>();
            _userPropertyCollectionMock = new Mock<IMongoCollection<UserProperty>>();

            _contextMock.SetupGet(ctx => ctx.Properties).Returns(_propertyCollectionMock.Object);
            _contextMock.SetupGet(ctx => ctx.FollowedProperties).Returns(_userPropertyCollectionMock.Object);

            _repository = new PropertyRepository(_contextMock.Object);
        }

        [Fact]
        public async Task AddPropertyAsync_ShouldInsertProperty()
        {
            // Arrange
            var property = new Property { Id = ObjectId.GenerateNewId().ToString(), UserId = "user123" };

            // Act
            await _repository.AddPropertyAsync(property);

            // Assert
            _propertyCollectionMock.Verify(coll => coll.InsertOneAsync(property, null, default), Times.Once);
        }

        [Fact]
        public async Task UpdatePropertyAsync_ShouldUpdateProperty()
        {
            // Arrange
            var property = new Property { Id = ObjectId.GenerateNewId().ToString(), UserId = "user123" };
            var updateResultMock = new Mock<ReplaceOneResult>();
            updateResultMock.SetupGet(ur => ur.IsAcknowledged).Returns(true);
            updateResultMock.SetupGet(ur => ur.ModifiedCount).Returns(1);

            _propertyCollectionMock.Setup(coll => coll.ReplaceOneAsync(It.IsAny<FilterDefinition<Property>>(), property, It.IsAny<ReplaceOptions>(), default))
                .ReturnsAsync(updateResultMock.Object);

            // Act
            var result = await _repository.UpdatePropertyAsync(property);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddUserPropertyAsync_ShouldInsertUserProperty()
        {
            // Arrange
            var userProperty = new UserProperty { Id = ObjectId.GenerateNewId().ToString(), UserId = "user123", PropertyId = "property123" };

            // Act
            await _repository.AddUserPropertyAsync(userProperty);

            // Assert
            _userPropertyCollectionMock.Verify(coll => coll.InsertOneAsync(userProperty, null, default), Times.Once);
        }

        [Fact]
        public async Task RemoveUserPropertyAsync_ShouldDeleteUserProperty()
        {
            // Arrange
            var deleteResultMock = new Mock<DeleteResult>();
            deleteResultMock.SetupGet(dr => dr.IsAcknowledged).Returns(true);
            deleteResultMock.SetupGet(dr => dr.DeletedCount).Returns(1);

            _userPropertyCollectionMock.Setup(coll => coll.DeleteOneAsync(It.IsAny<FilterDefinition<UserProperty>>(), default))
                .ReturnsAsync(deleteResultMock.Object);

            // Act
            var result = await _repository.RemoveUserPropertyAsync("property123", "user123");

            // Assert
            Assert.True(result);
        }
    }
}


