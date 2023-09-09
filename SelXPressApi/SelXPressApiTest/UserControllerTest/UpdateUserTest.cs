using System.Runtime.Serialization;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Controllers;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Exceptions;
using User = SelXPressApi.Models.User;

namespace SelXPressApiTest.UserControllerTest;

/// <summary>
/// Class to test the /api/User/email PUT route
/// </summary>
public class UpdateUserTest
{
    private UserController _userController;
    private IUserRepository _userRepository;
    private IMapper _mapper;
    private FirebaseAuthManager _authManager;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private HttpContext _httpContext;

    /// <summary>
    /// Initialize a new instance of the <see cref="UpdateUserTest"/> class.
    /// </summary>
    public UpdateUserTest()
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
    public async void UserController_UpdateUser_Status200()
    {
        string email = "maxence.leroy@epitech.eu";
        UpdateUserDTO requestBody = new UpdateUserDTO() { Username = "testunit" };
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Returns(true);
        A.CallTo(() => _userRepository.UserExistsEmail(email)).Returns(true);
        var httpContext = new DefaultHttpContext();
        httpContext.Response.Headers["EmailHeader"] = email;
        _userController.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var result = await _userController.UpdateUser(requestBody);
        
        Assert.IsType<OkResult>(result);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (Bad Request)
    /// </summary>
    [Fact]
    public async void UserController_UpdateUser_Status400()
    {
        string email = "maxence.leroy@epitech.eu";
        
        A.CallTo(() => _userRepository.UserExistsEmail(email)).Returns(true);
        var httpContext = new DefaultHttpContext();
        httpContext.Response.Headers["EmailHeader"] = email;
        _userController.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };
        var exception = await Assert.ThrowsAsync<BadRequestException>(() => _userController.UpdateUser(null));
        
        
        Assert.Equal("There are missing fields, please try again with some data", exception.Message);
        Assert.Equal("USR-1102",exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public async void UserController_UpdateUser_Status401_TokenIsMissing()
    {
        UpdateUserDTO requestBody = new UpdateUserDTO() { Username = "testunit" };
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("The token is not valid, please try again with another token", "SRV-1702"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _userController.UpdateUser(requestBody));
        
        Assert.Equal("The token is not valid, please try again with another token", exception.Message);
        Assert.Equal("SRV-1702", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public async void UserController_UpdateUser_Status401_TokenIsInvalid()
    {
        UpdateUserDTO requestBody = new UpdateUserDTO() { Username = "testunit" };
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("An error occured while the decoding of the jwt token", "SRV-1701"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _userController.UpdateUser(requestBody));
        
        Assert.Equal("An error occured while the decoding of the jwt token", exception.Message);
        Assert.Equal("SRV-1701", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not the database
    /// </summary>
    [Fact]
    public async void UserController_UpdateUser_Status401_EmailIsNotInTheDatabase()
    {
        UpdateUserDTO requestBody = new UpdateUserDTO() { Username = "testunit" };
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new NotFoundException("The email address is not in the database","SRV-1401"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userController.UpdateUser(requestBody));
        
        Assert.Equal("The email address is not in the database", exception.Message);
        Assert.Equal("SRV-1401", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public async void UserController_UpdateUser_Status404()
    {
        string email = "maxence.leroy@epitech.eu";
        UpdateUserDTO requestBody = new UpdateUserDTO() { Username = "testunit" };
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Returns(true);
        A.CallTo(() => _userRepository.UserExistsEmail(email)).Returns(false);
        var httpContext = new DefaultHttpContext();
        httpContext.Response.Headers["EmailHeader"] = email;
        _userController.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        var exeption = await Assert.ThrowsAsync<NotFoundException>(() =>_userController.UpdateUser(requestBody));
        Assert.Equal("The user with the email : " + email + " doesn't exist", exeption.Message);
        Assert.Equal("USR-1403", exeption.Code);
        
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public async void UserController_UpdateUser_Status500()
    {
        string email = "maxence.leroy@epitech.eu";
        UpdateUserDTO requestBody = new UpdateUserDTO() { Username = "testunit" };
        A.CallTo(() => _userRepository.UpdateUser(requestBody, email)).Throws(new Exception("SRV-1000"));
        var exception = await Assert.ThrowsAsync<Exception>(() => _userRepository.UpdateUser(requestBody, email));
        
        Assert.Equal("SRV-1000",exception.Message);
    }
}