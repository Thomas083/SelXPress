using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.OrderControllerTest;

public class UpdateOrderTest
{
    private OrderController _orderController;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private IOrderRepository _orderRepository;
    private IMapper _mapper;

    public UpdateOrderTest()
    {
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _orderRepository = A.Fake<IOrderRepository>();
        _mapper = A.Fake<IMapper>();

        _orderController = new OrderController(_authorizationMiddleware, _orderRepository, _mapper);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void OrderController_UpdateOrder_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400
    /// </summary>
    [Fact]
    public void OrderController_UpdateOrder_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because 
    /// </summary>
    [Fact]
    public void OrderController_UpdateOrder_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void OrderController_UpdateOrder_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void OrderController_UpdateOrder_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public void OrderController_UpdateOrder_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public void OrderController_UpdateOrder_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500
    /// </summary>
    [Fact]
    public void OrderController_UpdateOrder_Status_500()
    {
        //todo
    }
}