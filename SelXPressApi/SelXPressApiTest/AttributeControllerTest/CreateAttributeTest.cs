using System.Net;
using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Controllers;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Exceptions;
namespace SelXPressApiTest.AttributeControllerTest;

/// <summary>
/// Class to test the /api/Attribute POST route
/// </summary>
public class CreateAttributeTest
{
    private AttributeController _attributeController;
    private IAttributeRepository _attributeRepository;
    private IMapper _mapper;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private HttpContext _httpContext;

    /// <summary>
    /// Initialize a new instance of the <see cref="CreateAttributeTest"/>
    /// </summary>
    public CreateAttributeTest()
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
    /// Test to check if the status of the request is equals to 201
    /// </summary>
    [Fact]
    public async void AttributeController_CreateAttribute_Status_201()
    {
        CreateAttributeDTO requestBody = new CreateAttributeDTO() {Name = "test",Type = "test"};
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Returns(true);
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(true);

        var result = await _attributeController.CreateAttribute(requestBody);
        Assert.IsType<StatusCodeResult>(result);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400
    /// </summary>
    [Fact]
    public async void AttributeController_CreateAttribute_Status_400()
    {
        CreateAttributeDTO requestbody = new CreateAttributeDTO();
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Returns(true);
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(true);
        var exception = await Assert.ThrowsAsync<BadRequestException>(() =>_attributeController.CreateAttribute(requestbody));
        
        Assert.Equal("Some fields are missing, please try again with complete data", exception.Message);
        Assert.Equal("ATT-1102",exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public async void AttributeController_CreateAttribute_Status_401_TokenIsMissing()
    {
        CreateAttributeDTO requestBody = new CreateAttributeDTO() {Name = "test",Type = "test"};

        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("The token is not valid, please try again with another token", "SRV-1702"));

        _attributeController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception =
            await Assert.ThrowsAsync<UnauthorizedException>(() => _attributeController.CreateAttribute(requestBody));
        Assert.Equal("The token is not valid, please try again with another token", exception.Message);
        Assert.Equal("SRV-1702", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public async void AttributeController_CreateAttribute_Status_401_TokenIsInvalid()
    {
        CreateAttributeDTO requestBody = new CreateAttributeDTO() {Name = "test",Type = "test"};

        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("An error occured while the decoding of the jwt token", "SRV-1701"));

        _attributeController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception =
            await Assert.ThrowsAsync<UnauthorizedException>(() => _attributeController.CreateAttribute(requestBody));
        Assert.Equal("An error occured while the decoding of the jwt token", exception.Message);
        Assert.Equal("SRV-1701", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public async void AttributeController_CreateAttribute_Status_401_EmailIsNotInTheDatabase()
    {
        CreateAttributeDTO requestBody = new CreateAttributeDTO() {Name = "test",Type = "test"};

        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("The email address is not in the database","SRV-1401"));

        _attributeController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception =
            await Assert.ThrowsAsync<UnauthorizedException>(() => _attributeController.CreateAttribute(requestBody));
        Assert.Equal("The email address is not in the database", exception.Message);
        Assert.Equal("SRV-1401", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public async void AttributeController_CreateAttribute_Status_403()
    {
        CreateAttributeDTO requestBody = new CreateAttributeDTO() {Name = "test",Type = "test"};
        
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(false);

        var exception = await Assert.ThrowsAsync<ForbiddenRequestException>(() => _attributeController.CreateAttribute(requestBody));
        
        Assert.Equal("You are not authorized to perform this operation", exception.Message);
        Assert.Equal("ATT-2001", exception.Code );
    }
    
    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public async void AttributeController_CreateAttribute_Status_500()
    {
        CreateAttributeDTO requestBody = new CreateAttributeDTO() {Name = "test",Type = "test"};
        A.CallTo(() => _attributeRepository.CreateAttribute(requestBody)).Throws(new Exception("SRV-1000"));
        var exception = await Assert.ThrowsAsync<Exception>(() => _attributeRepository.CreateAttribute(requestBody));
        
        Assert.Equal("SRV-1000",exception.Message);
    }
}