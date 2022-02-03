using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMedia.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SocialMedia.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //Validar si la excepción es de tipo BusinessException
            if (context.Exception.GetType() == typeof(BusinessException))
            {
                //Capturar objeto exception convirtiendolo a tipo BusinessException
                var exception = (BusinessException)context.Exception;
                //Crear objeto con los datos a devolver de la excepción
                var validation = new
                {
                    Status = 400,
                    Title = "Bad Request",
                    Detail = exception.Message
                };
                //Crear objeto con un listado de los objetos de excepción
                var json = new
                {
                    errors = new[] { validation }
                };

                //Asignar objeto con las excepciones al contexto
                context.Result = new BadRequestObjectResult(json);
                //Indicar que el status code es un bad request
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                //Indicar que es una excepción controlada
                context.ExceptionHandled = true;
            }
        }
    }
}
