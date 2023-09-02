using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.AttributeDataControllerTest;

/// <summary>
/// Class to test /api/AttributeData POST route
/// </summary>
public class CreateAttributeDataTest
{
    private AttributeDataController _attributeDataController;
    private IAttributeDataRepository _attributeDataRepository;
    private IMapper _mapper;
    private IAuthorizationMiddleware _authorizationMiddleware;

    /// <summary>
    /// Initialize a new instance of the <see cref="CreateAttributeDataTest"/>
    /// </summary>
    public CreateAttributeDataTest()
    {
        _attributeDataRepository = A.Fake<IAttributeDataRepository>();
        _mapper = A.Fake<IMapper>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _attributeDataController = new AttributeDataController(_attributeDataRepository,_mapper,_authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 201 
    /// </summary>
    [Fact]
    public void AttributeDataController_CreateAttributeData_Status_201()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void AttributeDataController_CreateAttributeData_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void AttributeDataController_CreateAttributeData_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void AttributeDataController_CreateAttributeData_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void AttributeDataController_CreateAttributeData_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403 
    /// </summary>
    [Fact]
    public void AttributeDataController_CreateAttributeData_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void AttributeDataController_CreateAttributeData_Status_500()
    {
        //todo
    }
}