﻿using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;


namespace SelXPressApiTest.ProductAttributeControllerTest;

public class CreateProductAttributeTest
{
    private ProductAttributeController _productAttributeController;
    private IAuthorizationMiddleware _authorizationMiddleware;
    private IProductAttributeRepository _productAttributeRepository;
    private IMapper _mapper;

    public CreateProductAttributeTest()
    {
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();
        _productAttributeRepository = A.Fake<IProductAttributeRepository>();
        _mapper = A.Fake<IMapper>();
        _productAttributeController =
            new ProductAttributeController(_authorizationMiddleware, _productAttributeRepository, _mapper);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 201
    /// </summary>
    public void ProductAttributeController_CreateProductAttribute_Status_201()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void ProductAttributeController_CreateProductAttribute_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void ProductAttributeController_CreateProductAttribute_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void ProductAttributeController_CreateProductAttribute_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void ProductAttributeController_CreateProductAttribute_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public void ProductAttributeController_CreateProductAttribute_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void ProductAttributeController_CreateProductAttribute_Status_500()
    {
        //todo
    }
}