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
/// Class to test the /api/User/auth/login POST
/// </summary>
public class LoginTest
{
    private UserController _userController;
    private IUserRepository _userRepository;
    private IMapper _mapper;
    private FirebaseAuthManager _authManager;
    private IAuthorizationMiddleware _authorizationMiddleware;

    /// <summary>
    /// Initialize a new instance of the <see cref="GetUsersTest"/> class.
    /// </summary>
    public LoginTest()
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
    public void UserController_Login_Status200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void UserController_Login_Status400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void UserController_Login_Status500()
    {
        //todo
    }
}