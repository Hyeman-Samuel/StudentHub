using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StudentHub.Infrastructure.Network.Email;
using StudentHub.Infrastructure.Network.Ocr;
using StudentHub.Services.Auth;
using StudentHub.Services.Comment;
using StudentHub.Services.DtoMapper.Interface;
using StudentHub.Services.DtoMapper.Service;
using StudentHub.Services.Question;
using StudentHub.Services.Reply;
using StudentHub.Services.Solution;
using StudentHub.Services.Tag;
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
            AddSingletonServices(services);
            AddSwagger(services);
            AddMapper(services);
            AddScopedServices(services);           
        }

        private void AddScopedServices(IServiceCollection services)
        {
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ISolutionService, SolutionService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOcrScanner, OcrScanner>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IReplyService, ReplyService>();
        }
        private void AddSingletonServices(IServiceCollection services)
        {
            var emailConfig = Configuration
             .GetSection("EmailConfiguration")
             .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            var ocrConfig = Configuration
            .GetSection("OcrConfiguration")
            .Get<OcrConfiguration>();
            services.AddSingleton(ocrConfig);
        }
        private void AddMapper(IServiceCollection services)
        {
            services.AddScoped<IQuestionMapper, QuestionMapper>();
            services.AddScoped<ICommentMapper, CommentMapper>();
            services.AddScoped<ISolutionMapper, SolutionMapper>();
            services.AddScoped<ITagMapper, TagMapper>();
            services.AddScoped<IReplyMapper, ReplyMapper>();

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
