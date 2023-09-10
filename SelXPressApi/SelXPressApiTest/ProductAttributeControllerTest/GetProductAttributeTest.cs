using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.ProductAttributeControllerTest;

public class GetProductAttributeTest
{
    private ProductAttributeController _productAttributeController;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private IProductAttributeRepository _productAttributeRepository;
    private IMapper _mapper;

    public GetProductAttributeTest()
    {
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _productAttributeRepository = A.Fake<IProductAttributeRepository>();
        _mapper = A.Fake<IMapper>();
        _productAttributeController =
            new ProductAttributeController(_authorizationMiddleware, _productAttributeRepository, _mapper);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void ProductAttributeController_GetProductAttribute_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void ProductAttributeController_GetProductAttribute_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public void ProductAttributeController_GetProductAttribute_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void ProductAttributeController_GetProductAttribute_Status_500()
    {
        //todo
    }
}