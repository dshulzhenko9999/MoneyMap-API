using MoneyMap.API.Data.Models;
using MoneyMap.API.Data.Repositories.UserRepository;
using MoneyMap.API.Domain.DTOs;
using MoneyMap.API.Domain.Services.PasswordService;
using MoneyMap.API.Domain.UserDomain.Implementations;
using MoneyMap.UnitTests.DomainTests.Mocks;
using Moq;

namespace MoneyMap.UnitTests.DomainTests;

public class UserDomainTests
{
    private readonly Mock<IPasswordService> _passwordServiceMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserDomain _userDomain;

    public UserDomainTests()
    {
        _passwordServiceMock = new Mock<IPasswordService>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _userDomain = new UserDomain(_passwordServiceMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateUserAsync_UserIsNew_Creates()
    {
        _userRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as User);
        _userRepositoryMock.Setup(x => x.SaveUserAsync(It.IsAny<User>())).ReturnsAsync(UserDomainTestsMocks.User);
        _passwordServiceMock
            .Setup(x => x.CreatePasswordHash(It.IsAny<string>(), out It.Ref<byte[]>.IsAny, out It.Ref<byte[]>.IsAny))
            .Callback((string _, out byte[] hash, out byte[] salt) =>
            {
                hash = UserDomainTestsMocks.expectedHash;
                salt = UserDomainTestsMocks.expectedSalt;
            });


        var result = await _userDomain.CreateUserAsync(UserDomainTestsMocks.UserCreationDto);

        Assert.IsType<UserDto>(result);
        Assert.Equal(UserDomainTestsMocks.UserCreationDto.Email, result.Email);
        Assert.Equal(UserDomainTestsMocks.UserCreationDto.UserName, result.UserName);
        Assert.Equal(UserDomainTestsMocks.UserCreationDto.UserName, result.UserName);
    }

    [Fact]
    public async Task CreateUserAsync_UserIsNotNew_ThrowsInvalidOperationException()
    {
        _userRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(UserDomainTestsMocks.User);

        await Assert.ThrowsAnyAsync<InvalidOperationException>(async () =>
            await _userDomain.CreateUserAsync(UserDomainTestsMocks.UserCreationDto));
    }

    [Fact]
    public async Task GetUserByIdAsync_UserExists_ReturnsUserDto()
    {
        _userRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(UserDomainTestsMocks.User);

        var result = await _userDomain.GetUserByIdAsync(UserDomainTestsMocks.User.Id);

        Assert.IsType<UserDto>(result);
        Assert.Equal(UserDomainTestsMocks.User.Email, result.Email);
        Assert.Equal(UserDomainTestsMocks.User.UserName, result.UserName);
        Assert.Equal(UserDomainTestsMocks.User.Id, result.Id);
    }

    [Fact]
    public async Task GetUserByIdAsync_UserDoesNotExist_ThrowsInvalidOperationException()
    {
        _userRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as User);

        await Assert.ThrowsAnyAsync<InvalidOperationException>(async () =>
            await _userDomain.GetUserByIdAsync(UserDomainTestsMocks.User.Id));
    }

    [Fact]
    public async Task AuthenticateAsync_ValidCredentials_ReturnsUserDto()
    {
        _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(UserDomainTestsMocks.User);
        _passwordServiceMock
            .Setup(x => x.VerifyPasswordHash(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(true);

        var result = await _userDomain.AuthenticateAsync(UserDomainTestsMocks.AuthenticateUserDto);

        Assert.IsType<UserDto>(result);
        Assert.Equal(UserDomainTestsMocks.User.Email, result.Email);
        Assert.Equal(UserDomainTestsMocks.User.UserName, result.UserName);
        Assert.Equal(UserDomainTestsMocks.User.Id, result.Id);
    }

    [Fact]
    public async Task AuthenticateAsync_InvalidCredentials_ThrowsUnauthorizedAccessException()
    {
        _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(null as User);
        _passwordServiceMock
            .Setup(x => x.VerifyPasswordHash(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(false);

        await Assert.ThrowsAnyAsync<UnauthorizedAccessException>(async () =>
            await _userDomain.AuthenticateAsync(UserDomainTestsMocks.AuthenticateUserDto));
    }
}
