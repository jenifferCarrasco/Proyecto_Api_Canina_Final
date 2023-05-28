using APLICATION.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Canina.Middlewares
{
	public class ErrorHandlerMiddlewares
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddlewares(RequestDelegate next) {
            _next = next;
        }
        public async Task Invoke(HttpContext context) {

            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };
                switch (error)
                {
                    case APLICATION.Exceptions.ApiException _:
						//custom application error
						response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case APLICATION.Exceptions.ValidationException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException _:
                        //no found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        //unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
