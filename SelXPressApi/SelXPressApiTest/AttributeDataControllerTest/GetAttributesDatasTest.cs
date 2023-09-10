using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.AttributeDataControllerTest;

/// <summary>
/// Class to test the /api/AttributeData GET route
/// </summary>
public class GetAttributesDatasTest
{
    private AttributeDataController _attributeDataController;
    private IAttributeDataRepository _attributeDataRepository;
    private IMapper _mapper;
    private IAuthorizationMiddleware _authorizationMiddleware;

    /// <summary>
    /// Initialize a new instance of the <see cref="GetAttributesDatasTest"/>
    /// </summary>
    public GetAttributesDatasTest()
    {
        _attributeDataRepository = A.Fake<IAttributeDataRepository>();
        _mapper = A.Fake<IMapper>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _attributeDataController = new AttributeDataController(_attributeDataRepository,_mapper,_authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void AttributeDataController_GetAttributesDatas_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void AttributeDataController_GetAttributesDatas_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public void AttributeDataController_GetAttributesDatas_Status_404()
    {
        //todo 
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500
    /// </summary>
    [Fact]
    public void AttributeDataController_GetAttributesDatas_Status_500()
    {
        //todo
    }
}