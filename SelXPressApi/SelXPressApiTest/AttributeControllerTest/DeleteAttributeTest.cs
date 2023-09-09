using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Exceptions;

namespace SelXPressApiTest.AttributeControllerTest;

public class DeleteAttributeTest
{
    private AttributeController _attributeController;
    private IAttributeRepository _attributeRepository;
    private IMapper _mapper;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private HttpContext _httpContext;

    /// <summary>
    /// Initialize a new instance of the <see cref="DeleteAttributeTest"/>
    /// </summary>
    public DeleteAttributeTest()
    {
        _attributeRepository = A.Fake<IAttributeRepository>();
        _mapper = A.Fake<IMapper>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _httpContext = A.Fake<HttpContext>();
        
        _attributeController = new AttributeController(_attributeRepository, _mapper, _authorizationMiddleware);
        var controllerContext = new ControllerContext()
        {
            HttpContext = _httpContext
        };
        _attributeController.ControllerContext = controllerContext;
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public async void AttributeController_DeleteAttribute_Status_200()
    {
        int id = 1;
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Returns(true);
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(true);
        A.CallTo(() => _attributeRepository.AttributeExists(id)).Returns(true);

        var result = await _attributeController.DeleteAttribute(id);
        Assert.IsType<OkResult>(result);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public async void AttributeController_DeleteAttribute_Status_401_TokenIsMissing()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("The token is not valid, please try again with another token", "SRV-1702"));

        _attributeController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception =
            await Assert.ThrowsAsync<UnauthorizedException>(() => _attributeController.DeleteAttribute(1));
        Assert.Equal("The token is not valid, please try again with another token", exception.Message);
        Assert.Equal("SRV-1702", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public async void AttributeController_DeleteAttribute_Status_401_TokenIsInvalid()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("An error occured while the decoding of the jwt token", "SRV-1701"));

        _attributeController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception =
            await Assert.ThrowsAsync<UnauthorizedException>(() => _attributeController.DeleteAttribute(1));
        Assert.Equal("An error occured while the decoding of the jwt token", exception.Message);
        Assert.Equal("SRV-1701", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public async void AttributeController_DeleteAttribute_Status_401_EmailIsNotInTheDatabase()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("The email address is not in the database","SRV-1401"));

        _attributeController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception =
            await Assert.ThrowsAsync<UnauthorizedException>(() => _attributeController.DeleteAttribute(1));
        Assert.Equal("The email address is not in the database", exception.Message);
        Assert.Equal("SRV-1401", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public async void AttributeController_DeleteAttribute_Status_403()
    {
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(false);

        var exception = await Assert.ThrowsAsync<ForbiddenRequestException>(() => _attributeController.DeleteAttribute(1));
        
        Assert.Equal("You are not authorized to perform this operation", exception.Message);
        Assert.Equal("ATT-2001", exception.Code );
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public async void AttributeController_DeleteAttribute_Status_404()
    {
        int id = 1;
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Returns(true);
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(true);
        A.CallTo(() => _attributeRepository.AttributeExists(id)).Returns(false);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _attributeController.DeleteAttribute(1));
        Assert.Equal("The attribute with ID " + id + " doesn't exist", exception.Message);
        Assert.Equal("ATT-1402", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public async void AttributeController_DeleteAttribute_Status_500()
    {
        A.CallTo(() => _attributeRepository.DeleteAttribute(1)).Throws(new Exception("SRV-1000"));
        var exception = await Assert.ThrowsAsync<Exception>(() => _attributeRepository.DeleteAttribute(1));
        
        Assert.Equal("SRV-1000",exception.Message);
    }
}