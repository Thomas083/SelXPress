﻿using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.CartControllerTest;

/// <summary>
/// Class to test the /api/Cart/{id} GET route
/// </summary>
public class GetCartByIdTest
{
    private CartController _cartController;
    private ICartRepository _cartRepository;
    private IAuthorizationMiddleware _authorizationMiddleware;

    /// <summary>
    /// Initialize a new instance of the <see cref="GetAllCartsTest"/>
    /// </summary>
    public GetCartByIdTest()
    {
        _cartRepository = A.Fake<ICartRepository>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();

        _cartController = new CartController(_cartRepository, _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    public void CartController_GetCartById_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    public void CartController_GetCartById_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    public void CartController_GetCartById_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    public void CartController_GetCartById_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the datase
    /// </summary>
    public void CartController_GetCartById_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    public void CartController_GetCartById_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    public void CartController_GetCartById_Status_500()
    {
        //todo
    }
}