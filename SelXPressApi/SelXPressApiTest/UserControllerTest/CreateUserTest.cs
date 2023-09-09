using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Controllers;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using User = SelXPressApi.Models.User;
namespace SelXPressApiTest.UserControllerTest;

/// <summary>
/// Class to test the /api/User POST route
/// </summary>
public class CreateUserTest
{
    private UserController _userController;
    private IUserRepository _userRepository;
    private IMapper _mapper;
    private FirebaseAuthManager _authManager;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private HttpContext _httpContext;

    /// <summary>
    /// Initialize a new instance of the <see cref="GetUsersTest"/> class.
    /// </summary>
    public CreateUserTest()
    {
        //inject the dependencies of the UserController
        _userRepository = A.Fake<IUserRepository>();
        _mapper = A.Fake<IMapper>();
        _authManager = A.Fake<FirebaseAuthManager>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _httpContext = A.Fake<HttpContext>();
        //inject the user controller
        _userController = new UserController(_userRepository, _mapper, _authorizationMiddleware);

        var controllerContext = new ControllerContext()
        {
            HttpContext = _httpContext
        };
        _userController.ControllerContext = controllerContext;
    }
    
    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public async void UserController_CreateUser_Status400()
    {
        CreateUserDto requestBody = new CreateUserDto();
        var exception = await Assert.ThrowsAsync<BadRequestException>(() =>_userController.CreateUser(requestBody));
        
        Assert.Equal("There are missing fields, please try again with some data", exception.Message);
        Assert.Equal("USR-1102",exception.Code);
    }

    /// <summary>
    /// Test  to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public async void UserController_CreateUser_Status401_TokenIsMissing()
    {
        CreateUserDto requestBody = new CreateUserDto()
            { Email = "test@gmail.com", Password = "motdepasse", RoleId = 2, Username = "username" };
        
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("The token is not valid, please try again with another token", "SRV-1702"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _userController.CreateUser(requestBody));
        
        Assert.Equal("The token is not valid, please try again with another token", exception.Message);
        Assert.Equal("SRV-1702", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public async void UserController_CreateUser_Status401_TokenIsInvalid()
    {
        CreateUserDto requestBody = new CreateUserDto()
            { Email = "test@gmail.com", Password = "motdepasse", RoleId = 2, Username = "username" };
        
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("An error occured while the decoding of the jwt token", "SRV-1701"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _userController.CreateUser(requestBody));
        
        Assert.Equal("An error occured while the decoding of the jwt token", exception.Message);
        Assert.Equal("SRV-1701", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public async void UserController_CreateUser_Status401_EmailIsNotInTheDatabase()
    {
        CreateUserDto requestBody = new CreateUserDto()
            { Email = "test@gmail.com", Password = "motdepasse", RoleId = 2, Username = "username" };
        
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new NotFoundException("The email address is not in the database","SRV-1401"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userController.CreateUser(requestBody));
        
        Assert.Equal("The email address is not in the database", exception.Message);
        Assert.Equal("SRV-1401", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403 because the user is not authorized to do this operation
    /// </summary>
    [Fact]
    public async void UserController_CreateUser_Status403()
    {
        CreateUserDto requestBody = new CreateUserDto()
            { Email = "test@gmail.com", Password = "motdepasse", RoleId = 2, Username = "username" };
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(false);

        var exception = await Assert.ThrowsAsync<ForbiddenRequestException>(() => _userController.CreateUser(requestBody));
        
        Assert.Equal("You are not authorized to access this data", exception.Message);
        Assert.Equal("USR-2001", exception.Code );
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public async void UserController_CreateUser_Status500()
    {
        CreateUserDto requestBody = new CreateUserDto()
            { Email = "test@gmail.com", Password = "motdepasse", RoleId = 2, Username = "username" };
        A.CallTo(() => _userRepository.CreateUser(requestBody)).Throws(new Exception("SRV-1000"));
        var exception = await Assert.ThrowsAsync<Exception>(() => _userRepository.CreateUser(requestBody));
        
        Assert.Equal("SRV-1000",exception.Message);
    }
}