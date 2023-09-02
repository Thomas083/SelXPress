using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.OrderControllerTest;

public class CreateOrderTest
{
    private OrderController _orderController;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private IOrderRepository _orderRepository;
    private IMapper _mapper;

    public CreateOrderTest()
    {
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _orderRepository = A.Fake<IOrderRepository>();
        _mapper = A.Fake<IMapper>();

        _orderController = new OrderController(_authorizationMiddleware, _orderRepository, _mapper);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 201
    /// </summary>
    public void OrderController_CreateOrder_Status_201()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    public void OrderController_CreateOrder_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    public void OrderController_CreateOrder_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    public void OrderController_CreateOrder_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    public void OrderController_CreateOrder_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    public void OrderController_CreateOrder_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    public void OrderController_CreateOrder_Status_500()
    {
        //todo
    }
}