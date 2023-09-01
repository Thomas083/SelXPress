using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.ProductControllerTest;

public class DeleteProductTest
{
    private ProductController _productController;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private IProductRepository _productRepository;
    private IMapper _mapper;
    private DataContext _context;

    public DeleteProductTest()
    {
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _productRepository = A.Fake<IProductRepository>();
        _mapper = A.Fake<IMapper>();
        _context = A.Fake<DataContext>();

        _productController = new ProductController(_authorizationMiddleware, _productRepository, _mapper, _context);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    public void ProductController_DeleteProduct_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    public void ProductController_DeleteProduct_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    public void ProductController_DeleteProduct_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    public void ProductController_DeleteProduct_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    public void ProductController_DeleteProduct_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    public void ProductController_DeleteProduct_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    public void ProductController_DeleteProduct_Status_500()
    {
        //todo
    }
}