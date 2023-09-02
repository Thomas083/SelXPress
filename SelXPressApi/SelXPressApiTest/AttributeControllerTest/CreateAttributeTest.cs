using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

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

    /// <summary>
    /// Initialize a new instance of the <see cref="CreateAttributeTest"/>
    /// </summary>
    public CreateAttributeTest()
    {
        _attributeRepository = A.Fake<IAttributeRepository>();
        _mapper = A.Fake<IMapper>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _attributeController = new AttributeController(_attributeRepository, _mapper, _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 201
    /// </summary>
    [Fact]
    public void AttributeController_CreateAttribute_Status_201()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400
    /// </summary>
    [Fact]
    public void AttributeController_CreateAttribute_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void AttributeController_CreateAttribute_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void AttributeController_CreateAttribute_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void AttributeController_CreateAttribute_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public void AttributeController_CreateAttribute_Status_403()
    {
        //todo
    }
    
    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void AttributeController_CreateAttribute_Status_500()
    {
        //todo
    }
}