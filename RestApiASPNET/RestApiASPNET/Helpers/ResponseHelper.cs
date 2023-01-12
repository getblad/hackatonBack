using DataAccessLibrary.CustomExceptions;
using Microsoft.AspNetCore.Mvc;

namespace RestApiASPNET.Helpers;


public static class ResponseHelper
{
    public static JsonResult HandleException(Exception e)
    {
        switch (e)
        {
            case NotFoundException ex:
                return new JsonResult (ex.Message){StatusCode = StatusCodes.Status404NotFound};
            case AlreadyExistingException ex:
                return new JsonResult(ex.Message){StatusCode = StatusCodes.Status400BadRequest};;
            default:
                return new JsonResult("Error"){StatusCode = StatusCodes.Status400BadRequest};
        }
    }
    
}