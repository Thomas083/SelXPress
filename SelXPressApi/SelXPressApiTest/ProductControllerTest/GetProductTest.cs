﻿using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.ProductControllerTest;

public class GetProductTest
{
    private ProductController _productController;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private IProductRepository _productRepository;
    private IMapper _mapper;

    public GetProductTest()
    {
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _productRepository = A.Fake<IProductRepository>();
        _mapper = A.Fake<IMapper>();

        _productController = new ProductController(_authorizationMiddleware, _productRepository, _mapper);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void ProductController_GetProduct_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void ProductController_GetProduct_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public void ProductController_GetProduct_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void ProductController_GetProduct_Status_500()
    {
        //todo
    }
}