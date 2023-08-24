using System.Runtime.CompilerServices;
using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.AttributeDataControllerTest;

/// <summary>
/// Class to test the /api/AttributeData/{id} PUT route
/// </summary>
public class UpdateAttributeDataTest
{
    private AttributeDataController _attributeDataController;
    private IAttributeDataRepository _attributeDataRepository;
    private IMapper _mapper;
    private IAuthorizationMiddleware _authorizationMiddleware;

    /// <summary>
    /// Initialize a new instance of the <see cref="UpdateAttributeDataTest"/>
    /// </summary>
    public UpdateAttributeDataTest()
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
    public void AttributeDataController_UpdateAttributeData_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void AttributeDataController_UpdateAttributeData_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void AttributeDataController_UpdateAttributeData_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void AttributeDataController_UpdateAttributeData_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void AttributeDataController_UpdateAttributeData_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public void AttributeDataController_UpdateAttributeData_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public void AttributeDataController_UpdateAttributeData_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500
    /// </summary>
    [Fact]
    public void AttributeDataController_UpdateAttributeData_Status_500()
    {
        //todo
    }
}