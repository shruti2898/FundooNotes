using FundooRepository.CustomException;
using System;
using System.Collections.Generic;
using System.Net;

namespace FundooNotes.ExceptionMiddleware
{
    public class ExceptionStatusCode
    {
        private static Dictionary<Type, HttpStatusCode> statusCodes = new Dictionary<Type, HttpStatusCode>{
            {typeof(CustomArgumentException), HttpStatusCode.BadRequest},
            {typeof(CustomExistingDataException), HttpStatusCode.Conflict},
            {typeof(CustomNotFoundException),HttpStatusCode.NotFound},
            {typeof(CustomUnauthorizedException),HttpStatusCode.Unauthorized}
        };

        public static HttpStatusCode GetExceptionStatusCode(Exception ex)
        {
            bool exceptionFound = statusCodes.TryGetValue(ex.GetType(), out HttpStatusCode code);
            return exceptionFound ? code : HttpStatusCode.InternalServerError;
        }
    }
}
