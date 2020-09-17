using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using tasks.domain.Entities;

namespace tasks.api.Controllers
{
    public abstract class BaseController<TController> : ControllerBase
    {
        protected readonly string _requestId;

        public BaseController()
        {
            _requestId = Guid.NewGuid().ToString();
        }

        private IActionResult MakeResponse(IActionResult result)
        {
            return result;
        }
        protected IActionResult CreateResponse<T>(T dto)
        {
            IActionResult response = null;
            if (dto != null)
            {
                response = Ok(dto);
            }
            else
            {
                response = NotFound();
            }
            return MakeResponse(response);
        }
        protected IActionResult CreateServerErrorResponse(Exception ex, int? statusCode)
        {
            int status = statusCode != null ? statusCode.GetHashCode() : HttpStatusCode.InternalServerError.GetHashCode();

            return MakeResponse(StatusCode(
                status,
                new ErrorResponse()
                {
                    Status = status,
                    Message = $"{_requestId} :: {ex.Message}"
                }
            ));
        }
        protected IActionResult CreateValidationErrorResponse(IList<ValidationFailure> errors)
        {
            int status = HttpStatusCode.UnprocessableEntity.GetHashCode();

            return MakeResponse(StatusCode(
                status,
                new ErrorResponse()
                {
                    Status = status,
                    Message = $"{_requestId}",
                    Validation = errors
                }
            ));
        }
        protected IActionResult CreateErrorResponse(string message, int statusCode)
        {
            var status = HttpStatusCode.Unauthorized.GetHashCode();
            return MakeResponse(StatusCode(
                status,
                new ErrorResponse()
                {
                    Status = status,
                    Message = $"{_requestId} :: {message}"
                }
            ));
        }
    }
}