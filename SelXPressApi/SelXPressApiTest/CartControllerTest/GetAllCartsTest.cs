﻿using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.CartControllerTest;

/// <summary>
/// Class to test the /api/Cart GET route
/// </summary>
public class GetAllCartsTest
{
    private CartController _cartController;
    private ICartRepository _cartRepository;
    private IAuthorizationMiddleware _authorizationMiddleware;

    /// <summary>
    /// Initialize a new instance of the <see cref="GetAllCartsTest"/> class
    /// </summary>
    public GetAllCartsTest()
    {
        _cartRepository = A.Fake<ICartRepository>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();

        _cartController = new CartController(_cartRepository, _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void CartController_GetAllCarts_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void CartController_GetAllCarts_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void CartController_GetAllCarts_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void CartController_GetAllCarts_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void CartController_GetAllCarts_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public void CartController_GetAllCarts_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public void CartController_GetAllCarts_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500
    /// </summary>
    [Fact]
    public void CartController_GetAllCarts_Status_500()
    {
        //todo
    }
}