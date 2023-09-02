using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.TagControllerTest;

public class CreateTagTest
{
    private TagController _tagController;
    private ITagRepository _tagRepository;
    private IMapper _mapper;
    private IAuthorizationMiddleware _authorizationMiddleware;

    public CreateTagTest()
    {
        _tagRepository = A.Fake<ITagRepository>();
        _mapper = A.Fake<IMapper>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _tagController = new TagController(_tagRepository, _mapper, _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 201
    /// </summary>
    public void TagController_CreateTag_Status_201()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400
    /// </summary>
    public void TagController_CreateTag_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    public void TagController_CreateTag_Status_401_TokenIsMissing()
    {
        //Todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    public void TagController_CreateTag_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    public void TagController_CreateTag_Status_401_EmailIsNotInTheDatabase()
    {
        //Todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    public void TagController_CreateTag_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    public void TagController_CreateTag_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500
    /// </summary>
    public void TagController_CreateTag_Status_500()
    {
        //todo
    }
}