using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.ProductAttributeControllerTest;

public class GetProductAttributesTest
{
    private ProductAttributeController _productAttributeController;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private IProductAttributeRepository _productAttributeRepository;
    private IMapper _mapper;

    public GetProductAttributesTest()
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
    public void ProductAttributeController_GetProductAttributes_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    public void ProductAttributeController_GetProductAttributes_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    public void ProductAttributeController_GetProductAttributes_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    public void ProductController_GetProductAttributes_Status_500()
    {
        //todo
    }
    
}