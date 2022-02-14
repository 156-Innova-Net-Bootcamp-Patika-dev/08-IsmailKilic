﻿using System.Reflection;
using Application.Exceptions;
using FluentValidation.AspNetCore;
using Infrastructure.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    static public class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
                options.Filters.Add(typeof(ValidationFilter));
            }).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            )
            .AddFluentValidation(
                opt =>
                {
                    opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });
            serviceCollection.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

        }
    }
}
