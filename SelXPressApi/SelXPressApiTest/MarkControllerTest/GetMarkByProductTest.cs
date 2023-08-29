using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.MarkControllerTest;

public class GetMarkByProductTest
{
    private MarkController _markController;
    private IMarkRepository _markRepository;
    private IAuthorizationMiddleware _authorizationMiddleware;

    public GetMarkByProductTest()
    {
        _markRepository = A.Fake<IMarkRepository>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();

        _markController = new MarkController(_markRepository, _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 200
    /// </summary>
    public void MarkController_GetMarkByProduct_Status_200()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    public void MarkController_GetMarkByProduct_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 404
    /// </summary>
    public void MarkController_GetMarkByProduct_Status_404()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    public void MarkController_GetMarkByProduct_Status_500()
    {
        //todo
    }
}