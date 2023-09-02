using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.OrderProductControllerTest;

public class GetOrderProductTest
{
    private OrderProductController _orderProductController;
    private IOrderProductRepository _orderProductRepository;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private IMapper _mapper;

    public GetOrderProductTest()
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
    public void OrderProductController_GetOrderProduct_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    public void OrderProductController_GetOrderProduct_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    public void OrderProductController_GetOrderProduct_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    public void OrderProductController_GetOrderProduct_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    public void OrderProductController_GetOrderProduct_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    public void OrderProductController_GetOrderProduct_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    public void OrderProductController_GetOrderProduct_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500
    /// </summary>
    public void OrderProductController_GetOrderProduct_Status_500()
    {
        //todo
    }
}