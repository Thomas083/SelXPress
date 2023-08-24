using AutoMapper;
using FakeItEasy;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

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

    /// <summary>
    /// Initialize a new instance of the <see cref="GetAttributesTest"/>
    /// </summary>
    public GetAttributesTest()
    {
        _attributeRepository = A.Fake<IAttributeRepository>();
        _mapper = A.Fake<IMapper>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _attributeController = new AttributeController(_attributeRepository, _mapper, _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void AttributeController_GetAttributes_Status200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400
    /// </summary>
    [Fact]
    public void AttributeController_GetAttributes_Status400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public void AttributeController_GetAttributes_Status404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void AttributeController_GetAttributes_Status500()
    {
        //todo
    }
    
    
    
    
    
    
    
    
    
    
}