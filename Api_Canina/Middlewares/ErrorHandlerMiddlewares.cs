﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APLICATION;
using APLICATION.Wrappers;
using System.Net;
using System.Text.Json;

namespace Api_Canina.Middlewares
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
                    case APLICATION.Exceptions.ApiException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case APLICATION.Exceptions.ValidationException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
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
