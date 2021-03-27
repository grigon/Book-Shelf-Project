﻿using bookshelf.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookshelf.Middlewere
{
    public class ErrorHandligMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandligMiddleware> _logger;

        public ErrorHandligMiddleware(ILogger<ErrorHandligMiddleware> logger)
        {
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequest)
            {
                _logger.LogInformation(badRequest.Message);
                context.Response.StatusCode = 400;

                await context.Response.WriteAsync(badRequest.Message);
                //await context.Response.CompleteAsync();
            }
            catch (UnauthorizedAccess unauthorize)
            {
                _logger.LogWarning(unauthorize.Message);

                context.Response.StatusCode = 401;

                await context.Response.WriteAsync(unauthorize.Message);
            }
            catch (ForbiddenException forbidden)
            {
                _logger.LogInformation(forbidden.Message);
                context.Response.StatusCode = 403;

                await context.Response.WriteAsync(forbidden.Message);
            }
            catch (NotFoundException notFoundEx)
            {
                _logger.LogInformation(notFoundEx.Message);

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundEx.Message);
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(error.Message);
                
            }
        }
    }
}
