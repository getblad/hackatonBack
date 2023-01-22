using System;
using DataAccessLibrary.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestApiASPNET.Helpers;


public static class ResponseHelper
{
    public static JsonResult HandleException(Exception e)
    {
        switch (e)
        {
            case NotFoundException ex:
                return ex.MyMessage != null
                    ? new JsonResult($"{ex.MyMessage}") { StatusCode = StatusCodes.Status404NotFound }
                    : new JsonResult("Not Found") { StatusCode = StatusCodes.Status404NotFound };
            case AlreadyExistingException ex:
                return new JsonResult("Already existing element"){StatusCode = StatusCodes.Status400BadRequest};;
            default:
                return new JsonResult("Error"){StatusCode = StatusCodes.Status400BadRequest};
        }
    }

    public static JsonResult HandleException(bool serverError, Exception? ex = null)
    {
        
        if (serverError)
        {
            return new JsonResult("Server Error") { StatusCode = StatusCodes.Status500InternalServerError };
        }
        return new JsonResult("") { StatusCode = StatusCodes.Status400BadRequest };
    }
}