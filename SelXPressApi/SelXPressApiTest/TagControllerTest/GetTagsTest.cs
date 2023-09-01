using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.TagControllerTest;

public class GetTagsTest
{
    private TagController _tagController;
    private ITagRepository _tagRepository;
    private IMapper _mapper;
    private IAuthorizationMiddleware _authorizationMiddleware;

    public GetTagsTest()
    {
        _tagRepository = A.Fake<ITagRepository>();
        _mapper = A.Fake<IMapper>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _tagController = new TagController(_tagRepository, _mapper, _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    public void TagController_GetTags_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400
    /// </summary>
    public void TagController_GetTags_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    public void TagController_GetTags_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500
    /// </summary>
    public void TagController_GetTags_Status_500()
    {
        //todo
    }
}