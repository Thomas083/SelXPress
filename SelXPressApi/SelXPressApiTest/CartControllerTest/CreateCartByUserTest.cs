using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.CartControllerTest;

public class CreateCartByUserTest
{
    private CartController _cartController;
    private ICartRepository _cartRepository;
    private IAuthorizationMiddleware _authorizationMiddleware;

    /// <summary>
    /// Initialize a new instance of the <see cref="CreateCartByAdminTest"/>
    /// </summary>
    public CreateCartByUserTest()
    {
        _cartRepository = A.Fake<ICartRepository>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();

        _cartController = new CartController(_cartRepository, _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void CartController_CreateCartByUser_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void CartController_CreateCartByUser_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void CartController_CreateCartByUser_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void CartController_CreateCartByUser_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void CartController_CreateCartByUser_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public void CartController_CreateCartByUser_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void CartController_CreateCartByUser_Status_500()
    {
        //todo
    }
}