using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.RoleControllerTest;

public class DeleteRoleTest
{
    private RoleController _roleController;
    private IRoleRepository _roleRepository;
    private IAuthorizationMiddleware _authorizationMiddleware;

    public DeleteRoleTest()
    {
        _roleRepository = A.Fake<IRoleRepository>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();

        _roleController = new RoleController(_roleRepository, _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    [Fact]
    public void RoleController_DeleteRole_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400
    /// </summary>
    [Fact]
    public void RoleController_DeleteRole_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    [Fact]
    public void RoleController_DeleteRole_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    [Fact]
    public void RoleController_DeleteRole_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    [Fact]
    public void RoleController_DeleteRole_Status_401_EmailIsNotInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    [Fact]
    public void RoleController_DeleteRole_Status_403()
    {
        //todo
    }
    
    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    [Fact]
    public void RoleController_DeleteRole_Status_404()
    {
        //todo
    }
    
    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    [Fact]
    public void RoleController_DeleteRole_Status_500()
    {
        //todo
    }
}