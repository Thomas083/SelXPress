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
    /// Test to check if the status of the request is equals to 201
    /// </summary>
    [Fact]
    public void UserController_CreateUser_Status201()
    {
        // set the body of the request 
        CreateUserDto requestBody = new CreateUserDto()
        {
            Email = "test@gmail.com",
            Password = "password",
            RoleId = 1,
            Username = "testUsername"
        };
        _authorizationMiddleware.CheckIfTokenExists(_httpContext);
        
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void UserController_CreateUser_Status400()
    {
        //todo
    }

    /// <summary>
    /// Test  to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void UserController_CreateUser_Status401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void UserController_CreateUser_Status401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void UserController_CreateUser_Status401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403 because the user is not authorized to do this operation
    /// </summary>
    [Fact]
    public void UserController_CreateUser_Status403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server errro
    /// </summary>
    [Fact]
    public void UserController_CreateUser_Status500()
    {
        //todo
    }
}