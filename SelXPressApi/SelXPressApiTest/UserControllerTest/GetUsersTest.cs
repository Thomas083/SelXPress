using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Controllers;
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
        
        //inject the user controller
        _userController = new UserController(_userRepository, _mapper, _authorizationMiddleware);
    }
    
    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void UserController_GetUsers_Status200()
    {
        var users = A.Fake<List<User>>();
        A.CallTo(() => _userRepository.GetAllUsers()).Returns(users);

        var result = _userController.GetUsers();

        result.Should().BeOfType<Task<IActionResult>>();
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void UserController_GetUsers_Status400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void UserController_GetUsers_Status401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void UserController_GetUsers_Status401_TokenIsMissing()
    {
        //todo
        
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email in the token is not in the database
    /// </summary>
    [Fact]
    public void UserController_GetUsers_Status401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403 because the user is not authorized to do this operation
    /// </summary>
    [Fact]
    public void UserController_GetUsers_Status403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404 because there is no users in the database
    /// </summary>
    [Fact]
    public void UserController_GetUsers_Status404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void UserController_GetUsers_Status500()
    {
        //todo
    }
}