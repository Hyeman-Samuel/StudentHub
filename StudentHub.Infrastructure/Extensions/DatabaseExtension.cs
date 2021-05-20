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
        const string ProdConnectionString = "Server=tcp:student-hub.database.windows.net,1433;Initial Catalog=student-hub;Persist Security Info=False;User ID=skujy;Password=MyDatabase123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static IWebHostBuilder ConfigureDatabase(this IWebHostBuilder builder)
        {
            return builder.ConfigureServices((context, services) =>
            {
               services.AddDbContextPool<ApplicationDBContext>(option => option.UseSqlServer(ProdConnectionString));
            });
        }

    }
}
