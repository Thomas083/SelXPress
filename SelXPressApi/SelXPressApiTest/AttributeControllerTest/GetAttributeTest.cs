using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using Attribute = SelXPressApi.Models.Attribute;
using SelXPressApi.Exceptions;

namespace SelXPressApiTest.AttributeControllerTest;

/// <summary>
/// Class to test the /api/Attribute/{id} GET route
/// </summary>
public class GetAttributeTest
{
    private AttributeController _attributeController;
    private IAttributeRepository _attributeRepository;
    private IMapper _mapper;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private HttpContext _httpContext;

    /// <summary>
    /// Initialize a new instance of the <see cref="GetAttributeTest"/>
    /// </summary>
    public GetAttributeTest()
    {
        _attributeRepository = A.Fake<IAttributeRepository>();
        _mapper = A.Fake<IMapper>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _httpContext = A.Fake<HttpContext>();
        
        _attributeController = new AttributeController(_attributeRepository, _mapper, _authorizationMiddleware);
        
    }
    

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public async void AttributeController_GetAttribute_Status200()
    {
        var attribute = new Attribute() { Name = "testAttribute", Type = "type" };
        int id = 1;
        A.CallTo(() => _attributeRepository.AttributeExists(1)).Returns(true);
        A.CallTo(() => _attributeRepository.GetAttributeById(id)).Returns(attribute);

        var result = await _attributeController.GetAttribute(id);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<Attribute>(okResult.Value);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public async void AttributeController_GetAttribute_Status404()
    {
        var attribute = new Attribute() { Name = "testAttribute", Type = "type" };
        int id = 1;
        A.CallTo(() => _attributeRepository.AttributeExists(id)).Returns(false);
        A.CallTo(() => _attributeRepository.GetAttributeById(id)).Returns(attribute);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _attributeController.GetAttribute(id));
        Assert.Equal("The attribute with ID " + id + " doesn't exist", exception.Message);
        Assert.Equal("ATT-1402", exception.Code);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public async void AttributeController_GetAttribute_Status500()
    {
        A.CallTo(() => _attributeRepository.GetAttributeById(1)).Throws(new Exception("SRV-1000"));
        var exception = await Assert.ThrowsAsync<Exception>(() => _attributeRepository.GetAttributeById(1));
        
        Assert.Equal("SRV-1000",exception.Message);
    }
}