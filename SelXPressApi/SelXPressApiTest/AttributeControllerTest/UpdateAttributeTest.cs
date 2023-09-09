using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Controllers;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.AttributeControllerTest;

/// <summary>
/// Class to test the /api/Attribute/{id} PUT route
/// </summary>
public class UpdateAttributeTest
{
    private AttributeController _attributeController;
    private IAttributeRepository _attributeRepository;
    private IMapper _mapper;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private HttpContext _httpContext;

    /// <summary>
    /// Initialize a new instance of the <see cref="UpdateAttributeTest"/>
    /// </summary>
    public UpdateAttributeTest()
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
    public async void AttributeController_UpdateAttribute_Status_200()
    {
        UpdateAttributeDTO requestBody = new UpdateAttributeDTO() { Name = "test", Type = "test" };
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Returns(true);
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(true);
        A.CallTo(() => _attributeRepository.AttributeExists(1)).Returns(true);

        var result = await _attributeController.UpdateAttribute(1, requestBody);

        Assert.IsType<OkResult>(result);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400
    /// </summary>
    [Fact]
    public async void AttributeController_UpdateAttribute_Status_400()
    {
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Returns(true);
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(true);
        A.CallTo(() => _attributeRepository.AttributeExists(1)).Returns(true);
        var exception = await Assert.ThrowsAsync<BadRequestException>(() =>_attributeController.UpdateAttribute(1,null));
        
        Assert.Equal("Some fields are missing, please try again with complete data", exception.Message);
        Assert.Equal("ATT-1102",exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public async void AttributeController_UpdateAttribute_Status_401_TokenIsMissing()
    {
        UpdateAttributeDTO requestBody = new UpdateAttributeDTO() {Name = "test",Type = "test"};

        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("The token is not valid, please try again with another token", "SRV-1702"));

        _attributeController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception =
            await Assert.ThrowsAsync<UnauthorizedException>(() => _attributeController.UpdateAttribute(1,requestBody));
        Assert.Equal("The token is not valid, please try again with another token", exception.Message);
        Assert.Equal("SRV-1702", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public async void AttributeController_UpdateAttribute_Status_401_TokenIsInvalid()
    {
        UpdateAttributeDTO requestBody = new UpdateAttributeDTO() {Name = "test",Type = "test"};

        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("An error occured while the decoding of the jwt token", "SRV-1701"));

        _attributeController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception =
            await Assert.ThrowsAsync<UnauthorizedException>(() => _attributeController.UpdateAttribute(1,requestBody));
        Assert.Equal("An error occured while the decoding of the jwt token", exception.Message);
        Assert.Equal("SRV-1701", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public async void AttributeController_UpdateAttribute_Status_401_EmailIsNotInTheDatabase()
    {
        UpdateAttributeDTO requestBody = new UpdateAttributeDTO() {Name = "test",Type = "test"};

        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Throws(
            new UnauthorizedException("The email address is not in the database","SRV-1401"));

        _attributeController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

        var exception =
            await Assert.ThrowsAsync<UnauthorizedException>(() => _attributeController.UpdateAttribute(1,requestBody));
        Assert.Equal("The email address is not in the database", exception.Message);
        Assert.Equal("SRV-1401", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public async void AttributeController_UpdateAttribute_Status_403()
    {
        UpdateAttributeDTO requestBody = new UpdateAttributeDTO() {Name = "test",Type = "test"};
        
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(false);

        var exception = await Assert.ThrowsAsync<ForbiddenRequestException>(() => _attributeController.UpdateAttribute(1,requestBody));
        
        Assert.Equal("You are not authorized to perform this operation", exception.Message);
        Assert.Equal("ATT-2001", exception.Code );
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public async void AttributeController_UpdateAttribute_Status_404()
    {
        int id = 1;
        UpdateAttributeDTO requestBody = new UpdateAttributeDTO() { Name = "test", Type = "test" };
        A.CallTo(() => _authorizationMiddleware.CheckIfTokenExists(_httpContext)).Returns(true);
        A.CallTo(() => _authorizationMiddleware.CheckRoleIfAdmin(_httpContext)).Returns(true);
        A.CallTo(() => _attributeRepository.AttributeExists(id)).Returns(false);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _attributeController.UpdateAttribute(id,requestBody));
        Assert.Equal("The attribute with ID " + id + " doesn't exist", exception.Message);
        Assert.Equal("ATT-1402", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500
    /// </summary>
    [Fact]
    public async void AttributeController_UpdateAttribute_Status_500()
    {
        UpdateAttributeDTO requestBody = new UpdateAttributeDTO() {Name = "test",Type = "test"};
        A.CallTo(() => _attributeRepository.UpdateAttribute(1,requestBody)).Throws(new Exception("SRV-1000"));
        var exception = await Assert.ThrowsAsync<Exception>(() => _attributeRepository.UpdateAttribute(1,requestBody));
        
        Assert.Equal("SRV-1000",exception.Message);
    }
}