using Microsoft.AspNetCore.Mvc;
using MoneyMap.API.Controllers;
using MoneyMap.API.Domain.DTOs;
using MoneyMap.API.Domain.UserDomain;
using MoneyMap.UnitTests.ControllerTests.Mocks;
using Moq;

namespace MoneyMap.UnitTests.ControllerTests;

public class UserControllerTests
{
    private readonly Mock<IUserDomain> _userDomainMock;
    private readonly UserController userController;

    public UserControllerTests()
    {
        _userDomainMock = new Mock<IUserDomain>();
        userController = new UserController(_userDomainMock.Object);
    }

    [Fact]
    public async Task GetUserByIdAsync_UserFound_ReturnsOkResult()
    {
        _userDomainMock.Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(UserControllerTestsMocks.UserDto);

        var result = await userController.GetUserByIdAsync(UserControllerTestsMocks.UserDto.Id);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetUserByIdAsync_UserNotFound_Throws()
    {
        _userDomainMock.Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new InvalidOperationException());

        var result = await userController.GetUserByIdAsync(UserControllerTestsMocks.UserDto.Id);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task CreateUserAsync_ValidUser_ReturnsOkResult()
    {
        _userDomainMock.Setup(x => x.CreateUserAsync(It.IsAny<UserCreationDto>()))
            .ReturnsAsync(UserControllerTestsMocks.UserDto);

        var result = await userController.CreateUserAsync(UserControllerTestsMocks.UserCreationDto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task CreateUserAsync_InvalidUser_ReturnsBadRequest()
    {
        _userDomainMock.Setup(x => x.CreateUserAsync(It.IsAny<UserCreationDto>()))
            .ThrowsAsync(new InvalidOperationException());

        var result = await userController.CreateUserAsync(UserControllerTestsMocks.UserCreationDto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsOkResult()
    {
        _userDomainMock.Setup(x => x.AuthenticateAsync(It.IsAny<AuthenticateUserDto>()))
            .ReturnsAsync(UserControllerTestsMocks.UserDto);

        var result = await userController.LoginAsync(UserControllerTestsMocks.AuthenticateUserDto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task LoginAsync_InvalidCredentials_ReturnsUnauthorized()
    {
        _userDomainMock.Setup(x => x.AuthenticateAsync(It.IsAny<AuthenticateUserDto>()))
            .ThrowsAsync(new UnauthorizedAccessException());

        var result = await userController.LoginAsync(UserControllerTestsMocks.AuthenticateUserDto);
        
        Assert.IsType<UnauthorizedObjectResult>(result);
    }
}
