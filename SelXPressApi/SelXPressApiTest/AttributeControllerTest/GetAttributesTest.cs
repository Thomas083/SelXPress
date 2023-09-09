using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SelXPressApi.Controllers;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApiTest.AttributeControllerTest;

/// <summary>
/// Class to test the /api/Attribute GET route
/// </summary>
public class GetAttributesTest
{
    private AttributeController _attributeController;
    private IAttributeRepository _attributeRepository;
    private IMapper _mapper;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private HttpContext _httpContext;

    /// <summary>
    /// Initialize a new instance of the <see cref="GetAttributesTest"/>
    /// </summary>
    public GetAttributesTest()
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
    public async void AttributeController_GetAttributes_Status200()
    {
        List<Attribute> attributes = new List<Attribute>()
        {
            new Attribute() { Name = "testAttribute", Type = "type" },
            new Attribute() { Name = "test2Attribute", Type = "type" }
        };

        A.CallTo(() => _attributeRepository.GetAllAttributes()).Returns(attributes);

        var result = await _attributeController.GetAttributes();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<List<Attribute>>(okResult.Value);
        Assert.Equal(2,model.Count);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public async void AttributeController_GetAttributes_Status404()
    {
        List<Attribute> attributes = new List<Attribute>();

        A.CallTo(() => _attributeRepository.GetAllAttributes()).Returns(attributes);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _attributeController.GetAttributes());
        
        Assert.Equal("There are no attributes in the database, please try again", exception.Message);
        Assert.Equal("ATT-1401", exception.Code);

    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public async void AttributeController_GetAttributes_Status500()
    {
        A.CallTo(() => _attributeRepository.GetAllAttributes()).Throws(new Exception("SRV-1000"));
        var exception = await Assert.ThrowsAsync<Exception>(() => _attributeRepository.GetAllAttributes());
        
        Assert.Equal("SRV-1000",exception.Message);
    }
    
    
    
    
    
    
    
    
    
    
}