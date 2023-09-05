﻿using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.CommentControllerTest;

/// <summary>
/// Class to test the api/Comment/{id}/user GET route
/// </summary>
public class GetCommentsByUserTest
{
    private CommentController _commentController;
    private ICommentRepository _commentRepository;
    private IMapper _mapper;
    private IUserRepository _userRepository;
    private IProductRepository _productRepository;
    private IAuthorizationMiddleware _authorizationMiddleware;


    public GetCommentsByUserTest()
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
    [Fact]
    public void CommentController_GetCommentsByUser_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void CommentController_GetCommentsByUser_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void CommentController_GetCommentsByUser_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void CommentController_GetCommentsByUser_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void CommentController_GetCommentsByUser_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public void CommentController_GetCommentsByUser_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public void CommentController_GetCommentsByUser_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void CommentController_GetCommentsByUser_Status_500()
    {
        //todo
    }
    
}