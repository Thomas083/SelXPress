using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using SelXPressApi.Controllers;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DTO.UserDTO;
using User = SelXPressApi.Models.User;
using SelXPressApi.Exceptions;

namespace SelXPressApiTest.UserControllerTest;

/// <summary>
/// Class to test the /api/Users/{id} GET route
/// </summary>
public class GetUserTest
{
    private UserController _userController;
    private IUserRepository _userRepository;
    private IMapper _mapper;
    private FirebaseAuthManager _authManager;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private HttpContext _httpContext;

    /// <summary>
    /// Initialize a nex instance of the <see cref="GetUserTest"/> class
    /// </summary>
    public GetUserTest()
    {
        //Inject the dependencies of the UserController
        _userRepository = A.Fake<IUserRepository>();
        _mapper = A.Fake<IMapper>();
        _authManager = A.Fake<FirebaseAuthManager>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _httpContext = A.Fake<HttpContext>();
        
        
        //Inject the user controller
        _userController = new UserController(_userRepository,_mapper,_authorizationMiddleware);
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
    public async void UserController_GetUser_Status200()
    {
        var user = new User() { Id = 1, Email = "maxence.leroy@epitech.eu" };
        var userMapped = new UserDto() { Id = 1, Email = "maxence.leroy@epitech.eu" };
        A.CallTo(() => _userRepository.UserExistsEmail(user.Email)).Returns(true);
        
        A.CallTo(() => _mapper.Map<UserDto>(A<User>._)).Returns(userMapped);

        var httpContext = new DefaultHttpContext();
        httpContext.Response.Headers["EmailHeader"] = user.Email;
        _userController.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };
        
        var result = await _userController.GetUser();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<UserDto>(okResult.Value);
    }
    
    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public async void UserController_GetUser_Status401_TokenIsInvalid()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("An error occured while the decoding of the jwt token", "SRV-1701"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _userController.GetUser());
        
        Assert.Equal("An error occured while the decoding of the jwt token", exception.Message);
        Assert.Equal("SRV-1701", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public async void UserController_GetUser_Status401_TokenIsMissing()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("The token is not valid, please try again with another token", "SRV-1702"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _userController.GetUser());
        
        Assert.Equal("The token is not valid, please try again with another token", exception.Message);
        Assert.Equal("SRV-1702", exception.Code);

        
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email in the token is not in the database
    /// </summary>
    [Fact]
    public async void UserController_GetUser_Status401_EmailIsNotInTheDatabase()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new NotFoundException("The email address is not in the database","SRV-1401"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userController.GetUser());
        
        Assert.Equal("The email address is not in the database", exception.Message);
        Assert.Equal("SRV-1401", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403 because the user is not authorized to do this operation
    /// </summary>
    [Fact]
    public async void UserController_GetUser_Status403()
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
    public async void UserController_GetUser_Status404()
    {
        var user = new User() { Id = 1, Email = "maxence.leroy@epitech.eu" };
        var userMapped = new UserDto() { Id = 1, Email = "maxence.leroy@epitech.eu" };
        A.CallTo(() => _userRepository.UserExistsEmail(user.Email)).Returns(false);
        
        A.CallTo(() => _mapper.Map<UserDto>(A<User>._)).Returns(userMapped);

        var httpContext = new DefaultHttpContext();
        httpContext.Response.Headers["EmailHeader"] = user.Email;
        _userController.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };
        
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userController.GetUser());
        
        Assert.Equal("The user with the email: " + user.Email + " doesn't exist", exception.Message);
        Assert.Equal("USR-1403",exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public async void UserController_GetUser_Status500()
    {
        A.CallTo(() => _userRepository.GetAllUsers()).Throws(new Exception("SRV-1000"));
        var exception = await Assert.ThrowsAsync<Exception>(() => _userRepository.GetAllUsers());
        
        Assert.Equal("SRV-1000",exception.Message);
    }
}