using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PitchMatch.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchMatchUnitTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public IConfiguration Configuration { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) => { Configuration = config.Build(); });

            builder.ConfigureServices(services => {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PitchMatchDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add ApplicationDbContext using an Azure SQL Database.
                services.AddDbContext<PitchMatchDbContext>(options => {
                    options.UseSqlServer(Configuration.GetConnectionString("PitchMatchConnection"));
                });
            });
        }

    
    }
}
