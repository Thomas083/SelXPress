using System.Net;
using AutoFixture;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Controllers;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using User = SelXPressApi.Models.User;
namespace SelXPressApiTest.UserControllerTest;

/// <summary>
/// Class to test the /api/Users GET route
/// </summary>
public class GetUsersTest
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
    public GetUsersTest()
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
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public async void UserController_GetUsers_Status200()
    {
        var users = new List<User>()
        {
            new User() { Id = 1, Email = "maxence.leroy@epitech.eu" },
            new User() { Id = 2, Email = "ugo.bertrand@epitech.eu" }
        };

        var usersMapped = new List<UserDto>()
        {
            new UserDto() { Id = 1, Email = "maxence.leroy@epitech.eu" },
            new UserDto() { Id = 2, Email = "ugo.bertrand@epitech.eu" }
        };
        A.CallTo(() => _userRepository.GetAllUsers()).Returns(users);
        A.CallTo(() => _mapper.Map<List<UserDto>>(A<List<User>>._)).Returns(usersMapped);
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(true);

        var result = await _userController.GetUsers();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<List<UserDto>>(okResult.Value);
        Assert.Equal(2, model.Count);
    }
    
    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public async void UserController_GetUsers_Status401_TokenIsInvalid()
    {

        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("The token is not valid, please try again with another token", "SRV-1702"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _userController.GetUsers());
        
        Assert.Equal("The token is not valid, please try again with another token", exception.Message);
        Assert.Equal("SRV-1702", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public async void UserController_GetUsers_Status401_TokenIsMissing()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("An error occured while the decoding of the jwt token", "SRV-1701"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _userController.GetUsers());
        
        Assert.Equal("An error occured while the decoding of the jwt token", exception.Message);
        Assert.Equal("SRV-1701", exception.Code);

    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email in the token is not in the database
    /// </summary>
    [Fact]
    public async void UserController_GetUsers_Status401_EmailIsNotInTheDatabase()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new NotFoundException("The email address is not in the database","SRV-1401"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userController.GetUsers());
        
        Assert.Equal("The email address is not in the database", exception.Message);
        Assert.Equal("SRV-1401", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403 because the user is not authorized to do this operation
    /// </summary>
    [Fact]
    public async void UserController_GetUsers_Status403()
    {
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(false);

        var exception = await Assert.ThrowsAsync<ForbiddenRequestException>(() => _userController.GetUsers());
        
        Assert.Equal("You are not authorized to access this data", exception.Message);
        Assert.Equal("USR-2001", exception.Code );
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404 because there is no users in the database
    /// </summary>
    [Fact]
    public async void UserController_GetUsers_Status404()
    {
        var users = new List<User>()
        {
            new User() { Id = 1, Email = "maxence.leroy@epitech.eu" },
            new User() { Id = 2, Email = "ugo.bertrand@epitech.eu" }
        };
        A.CallTo(() => _userRepository.GetAllUsers()).Returns(users);
        A.CallTo(() => _mapper.Map<List<UserDto>>(A<List<User>>._)).Returns(new List<UserDto>());
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(true);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userController.GetUsers());
        
        Assert.Equal("There are no users in the database, please try again", exception.Message);
        Assert.Equal("USR-1401",exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public async void UserController_GetUsers_Status500()
    {
        A.CallTo(() => _userRepository.GetAllUsers()).Throws(new Exception("SRV-1000"));
        var exception = await Assert.ThrowsAsync<Exception>(() => _userRepository.GetAllUsers());
        
        Assert.Equal("SRV-1000",exception.Message);
    }
    
}