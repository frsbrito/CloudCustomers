using CloudCustomers.API.Models;
using CloudCustomers.API.Models.Config;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            await sut.GetAllUsers();

            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse, endpoint);

            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = endpoint
                }
            );

            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            var uri = new Uri(endpoint);

            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get 
                    && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
