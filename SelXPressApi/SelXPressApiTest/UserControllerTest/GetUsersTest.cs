using System.Net;
using AutoFixture;
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
            new User {Id = 1, Email = "maxenceLeroy@epitech.eu"},
            new User {Id = 2}
        }
        var usersMapped = new List<UserDto>()
        {
            new UserDto { Id = 1, Email = "maxence.leroy@epitech.eu" },
            new UserDto { Id = 2, Email = "testmoica@gmail.com" }
        };

        A.CallTo(() => _userRepository.GetAllUsers()).Returns(users);
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

    public List<User> CreateUser()
    {
        List<User> listUser = new List<User>();
        User userCustomer1 = new User
        {
            Username = "LeBirz",
            Email = "ugo.bertrand@epitech.eu",
        };

        User userCustomer2 = new User
        {
            Username = "Elsharion",
            Email = "david.vacossin@epitech.eu",
        };

        User userSeller = new User
        {
            Username = "Aliak",
            Email = "thomas.debray@epitech.eu",
        };

        User userOperator = new User
        {
            Username = "Maxence_Leroy",
            Email = "maxence.leroy@epitech.eu",
        };
        listUser.Add(userCustomer1);
        listUser.Add(userCustomer2);
        listUser.Add(userOperator);
        listUser.Add(userSeller);

        return listUser;
    }
}