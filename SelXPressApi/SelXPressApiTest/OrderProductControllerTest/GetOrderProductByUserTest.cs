using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.OrderProductControllerTest;

public class GetOrderProductByUserTest
{
    private OrderProductController _orderProductController;
    private IOrderProductRepository _orderProductRepository;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private IMapper _mapper;

    public GetOrderProductByUserTest()
    {
        _orderProductRepository = A.Fake<IOrderProductRepository>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _mapper = A.Fake<IMapper>();

        _orderProductController =
            new OrderProductController(_authorizationMiddleware, _orderProductRepository, _mapper);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void OrderProductController_GetOrderProductByUser_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void OrderProductController_GetOrderProductByUser_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void OrderProductController_GetOrderProductByUser_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void OrderProductController_GetOrderProductByUser_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void OrderProductController_GetOrderProductByUser_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public void OrderProductController_GetOrderProductByUser_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public void OrderProductController_GetOrderProductByUser_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void OrderProductController_GetOrderProductByUser_Status_500()
    {
        //todo
    }
}