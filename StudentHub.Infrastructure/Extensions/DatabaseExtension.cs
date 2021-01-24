using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Infrastructure.Extensions
{
    public static class DatabseExtension
    {
        const string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=StudentHub;Trusted_Connection=True;MultipleActiveResultSets=True";


        public static IWebHostBuilder ConfigureDatabase(this IWebHostBuilder builder)
        {
            return builder.ConfigureServices((context, services) =>
            {
               services.AddDbContextPool<ApplicationDBContext>(option => option.UseSqlServer(ConnectionString));
            });
        }

    }
}
