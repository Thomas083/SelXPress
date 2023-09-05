using AutoMapper;
using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.CategoryControllerTest;

public class UpdateCategoryTest
{
    private CategoryController _categoryController;
    private ICategoryRepository _categoryRepository;
    private IMapper _mapper;
    private IAuthorizationMiddleware _authorizationMiddleware;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCategoryTest"/>
    /// </summary>
    public UpdateCategoryTest()
    {
        _categoryRepository = A.Fake<ICategoryRepository>();
        _mapper = A.Fake<IMapper>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();

        _categoryController = new CategoryController(_categoryRepository, _mapper, _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void CategoryController_UpdateCategory_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    [Fact]
    public void CategoryController_UpdateCategory_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void CategoryController_UpdateCategory_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void CategoryController_UpdateCategory_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void CategoryController_UpdateCategory_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public void CategoryController_UpdateCategory_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public void CategoryController_UpdateCategory_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void CategoryController_UpdateCategory_Status_500()
    {
        //todo
    }
}