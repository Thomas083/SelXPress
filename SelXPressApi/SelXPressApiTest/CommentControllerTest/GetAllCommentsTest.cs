using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.CommentControllerTest;

/// <summary>
/// Class to test the /api/Comment/ GET route
/// </summary>
public class GetAllCommentsTest
{
    private CommentController _commentController;
    private ICommentRepository _commentRepository;
    private IMapper _mapper;
    private IUserRepository _userRepository;
    private IProductRepository _productRepository;
    private IAuthorizationMiddleware _authorizationMiddleware;


    public GetAllCommentsTest()
    {
        _commentRepository = A.Fake<ICommentRepository>();
        _mapper = A.Fake<IMapper>();
        _userRepository = A.Fake<IUserRepository>();
        _productRepository = A.Fake<IProductRepository>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();

        _commentController = new CommentController(_commentRepository, _mapper, _userRepository, _productRepository,
            _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    public void CommentController_GetAllComments_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    public void CommentController_GetAllComments_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    public void CommentController_GetAllComments_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    public void CommentController_GetAllComments_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    public void CommentController_GetAllComments_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    public void CommentController_GetAllComments_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    public void CommentController_GetAllComments_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    public void CommentController_GetAllComments_Status_500()
    {
        //todo
    }
}