using Moq;
using AutoMapper;
using Basket.Persistance.Repositories;
using Basket.Application.Domains.Basket.GetFollowedProperties;
using RentHarbor.MongoDb.Entities;
using Basket.Application.Domains.Basket.GetFollowedProperties.Dto;

namespace Basket.Unit.Tests.Application.Domains.Basket.GetFollowedProperties
{
    public class GetFollowedPropertiesQueryHandlerTests
    {
        private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetFollowedPropertiesQueryHandler _handler;

        public GetFollowedPropertiesQueryHandlerTests()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetFollowedPropertiesQueryHandler(_mapperMock.Object, _propertyRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPropertiesExist()
        {
            var userId = "test_user";
            var UserPropertiesroperties = new List<UserProperty>
            {
                new UserProperty { Id = "property1" },
                new UserProperty { Id = "property2" }
            };
            var properties = new List<Property>
            {
                new Property { Id = "property1" },
                new Property { Id = "property2" }
            };
            var propertyDtos = new List<PropertyDto>
            {
                new PropertyDto { Id = "property1" },
                new PropertyDto { Id = "property2" }
            };

            _propertyRepositoryMock.Setup(repo => repo.GetUserPropertiesByUserIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(UserPropertiesroperties));
            _propertyRepositoryMock.Setup(repo => repo.GetPropertiesByIdsAsync(It.IsAny<List<string>>()))
                .ReturnsAsync(properties);
            _mapperMock.Setup(mapper => mapper.Map<List<PropertyDto>>(It.IsAny<List<Property>>()))
                .Returns(propertyDtos);

            var query = new GetFollowedPropertiesQuery { UserId = userId };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenExceptionIsThrown()
        {
            var userId = "test_user";

            _propertyRepositoryMock.Setup(repo => repo.GetUserPropertiesByUserIdAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Database error"));

            var query = new GetFollowedPropertiesQuery { UserId = userId };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("Database error", result.ErrorMessage);
        }
    }
}

