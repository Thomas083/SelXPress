using FakeItEasy;
using SelXPressApi.Controllers;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApiTest.MarkControllerTest;

public class CreateMarkTest
{
    private MarkController _markController;
    private IMarkRepository _markRepository;
    private IAuthorizationMiddleware _authorizationMiddleware;

    public CreateMarkTest()
    {
        _markRepository = A.Fake<IMarkRepository>();
        _authorizationMiddleware = A.Fake<IAuthorizationMiddleware>();

        _markController = new MarkController(_markRepository, _authorizationMiddleware);
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 201
    /// </summary>
    public void MarkController_CreateMark_Status_201()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 400 (BadRequest)
    /// </summary>
    public void MarkController_CreateMark_Status_400()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is missing
    /// </summary>
    public void MarkController_CreateMark_Status_401_TokenIsMissing()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the token is invalid
    /// </summary>
    public void MarkController_CreateMark_Status_401_TokenIsInvalid()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 401 because the email is not in the database
    /// </summary>
    public void MarkController_CreateMark_Status_401_EmailIsNOtInTheDatabase()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 403
    /// </summary>
    public void MarkController_CreateMark_Status_403()
    {
        //todo
    }

    /// <summary>
    /// Test to check if the status of the request is equals to 500 due to an internal server error
    /// </summary>
    public void MarkController_CreateMark_Status_500()
    {
        //todo
    }
}