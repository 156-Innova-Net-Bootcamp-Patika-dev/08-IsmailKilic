using System;
using Data;
using Data.EfCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Business.Concrete;
using Business.Abstract;
using API.Filters;
using Business.Helpers.Jwt;
using Business.Helpers;
using Business.Helpers.Middleware;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter))).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddDbContext<BlogContext>(options =>
                    options.UseSqlServer(_configuration.GetConnectionString("Connection"), b => b.MigrationsAssembly("API")));

            services.AddAutoMapper(typeof(Startup));
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog API", Version = "v1" });
            });

            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));

            // di injections
            services.AddScoped<EfCoreUserRepository>();
            services.AddScoped<IUserService,UserService>();

            services.AddScoped<EfCoreCategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<EfCorePostRepository>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<EfCoreCommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IJwtUtils, JwtUtils>();
        }
            
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            // Auth middleware
            app.UseMiddleware<AuthMiddleware>();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
