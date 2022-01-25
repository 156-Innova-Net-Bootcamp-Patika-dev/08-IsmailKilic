using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (AuthUser)context.HttpContext.Items["User"];

        if (user == null)
        {
            // not logged in
            context.Result = new JsonResult(new
            {
                errors = new List<string> { "You are unauthorized" },
                status = 401
            })
            { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}