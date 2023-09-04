﻿using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.OrderControllerTest;

public class DeleteOrderTest
{
    private OrderController _orderController;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private IOrderRepository _orderRepository;
    private IMapper _mapper;

    public DeleteOrderTest()
    {
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _orderRepository = A.Fake<IOrderRepository>();
        _mapper = A.Fake<IMapper>();

        _orderController = new OrderController(_authorizationMiddleware, _orderRepository, _mapper);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    public void OrderController_DeleteOrder_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    public void OrderController_DeleteOrder_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    public void OrderController_DeleteOrder_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    public void OrderController_DeleteOrder_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    public void OrderController_DeleteOrder_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    public void OrderController_DeleteOrder_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    public void OrderController_DeleteOrder_Status_500()
    {
        //todo
    }
}