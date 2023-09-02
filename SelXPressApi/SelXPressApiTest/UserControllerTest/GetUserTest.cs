using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

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
        
        
        //Inject the user controller
        _userController = new UserController(_userRepository,_mapper,_authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void UserController_GetUser_Status200()
    {
        //todo
    }
    
    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void UserController_GetUser_Status400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void UserController_GetUser_Status401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void UserController_GetUser_Status401_TokenIsMissing()
    {
        //todo
        
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email in the token is not in the database
    /// </summary>
    [Fact]
    public void UserController_GetUser_Status401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403 because the user is not authorized to do this operation
    /// </summary>
    [Fact]
    public void UserController_GetUser_Status403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404 because there is no users in the database
    /// </summary>
    [Fact]
    public void UserController_GetUser_Status404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void UserController_GetUser_Status500()
    {
        //todo
    }
}