using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Controllers;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Exceptions;

namespace SelXPressApiTest.UserControllerTest;

/// <summary>
/// Class to test the /api/User/{id} route
/// </summary>
public class DeleteUserTest
{
    private UserController _userController;
    private IUserRepository _userRepository;
    private IMapper _mapper;
    private FirebaseAuthManager _authManager;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private HttpContext _httpContext;

    /// <summary>
    /// Initialize a new instance of the <see cref="DeleteUserTest"/>
    /// </summary>
    public DeleteUserTest()
    {
        //Inject the dependencies of the UserController
        _userRepository = A.Fake<IUserRepository>();
        _mapper = A.Fake<IMapper>();
        _authManager = A.Fake<FirebaseAuthManager>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _httpContext = A.Fake<HttpContext>();
        
        //Inject the user controller
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
    public async void UserController_DeleteUser_Status200()
    {
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(true);
        A.CallTo(() => _userRepository.UserExists(1)).Returns(true);
        A.CallTo(() => _userRepository.DeleteUser(1)).Returns(true);

        var result = await _userController.DeleteUser(1);

        Assert.IsType<OkResult>(result);
    }
    
    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public async void UserController_DeleteUser_Status401_TokenIsMissing()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("The token is not valid, please try again with another token", "SRV-1702"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _userController.DeleteUser(1));
        
        Assert.Equal("The token is not valid, please try again with another token", exception.Message);
        Assert.Equal("SRV-1702", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public async void UserController_DeleteUser_Status401_TokenIsInvalid()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("An error occured while the decoding of the jwt token", "SRV-1701"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _userController.DeleteUser(1));
        
        Assert.Equal("An error occured while the decoding of the jwt token", exception.Message);
        Assert.Equal("SRV-1701", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public async void UserController_DeleteUser_Status401_EmailIsNotInTheDatabase()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new NotFoundException("The email address is not in the database","SRV-1401"));
        _userController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userController.DeleteUser(1));
        
        Assert.Equal("The email address is not in the database", exception.Message);
        Assert.Equal("SRV-1401", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403 because the user is not authorized to do this operation
    /// </summary>
    [Fact]
    public async void UserController_DeleteUser_Status403()
    {
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(false);

        var exception = await Assert.ThrowsAsync<ForbiddenRequestException>(() => _userController.DeleteUser(1));
        
        Assert.Equal("You are not authorized to access this data", exception.Message);
        Assert.Equal("USR-2001", exception.Code );
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public async void UserController_DeleteUser_Status404()
    {
        int id = 1;
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Returns(true);
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(true);
        A.CallTo(() => _userRepository.UserExists(id)).Returns(false);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userController.DeleteUser(id));
        
        Assert.Equal("The user with the ID: " + id + " doesn't exist", exception.Message);
        Assert.Equal("USR-1402", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public async void UserController_DeleteUser_Status500()
    {
        A.CallTo(() => _userRepository.DeleteUser(1)).Throws(new Exception("SRV-1000"));
        var exception = await Assert.ThrowsAsync<Exception>(() => _userRepository.DeleteUser(1));
        
        Assert.Equal("SRV-1000",exception.Message);
    }
}