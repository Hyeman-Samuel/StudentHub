﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StudentHub.Infrastructure.Network;
using StudentHub.Services.Auth;
using StudentHub.Services.Comment;
using StudentHub.Services.DtoMapper.Interface;
using StudentHub.Services.DtoMapper.Service;
using StudentHub.Services.Question;
using StudentHub.Services.Solution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StudentHub
{
    public partial class Startup
    {
        public void ConfigureDIContainer(IServiceCollection services)
        {
            AddSwagger(services);
            AddMapper(services);
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ISolutionService, SolutionService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOcrScanner, OcrScanner>();
            
            
        }

        private void AddMapper(IServiceCollection services)
        {
            services.AddScoped<IQuestionMapper, QuestionMapper>();
            services.AddScoped<ICommentMapper, CommentMapper>();
            services.AddScoped<ISolutionMapper, SolutionMapper>();

        }
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Demia.io",
                    Description = "A ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Hyeman Samuel",
                        Email = "Hyeman1738@gmail.com"
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                //c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.Http,
                //    Scheme = "Bearer"
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            }
                //        },
                //        Array.Empty<string>()
                //    }
                //});

            });

        }
    }
}