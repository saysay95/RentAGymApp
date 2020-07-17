using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using static System.Console;
using Shared;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using RentAGymService.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;

namespace RentAGymService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            string databasePath = Path.Combine("..", "RentAGym.db");

            services.AddDbContext<RentAGym>(options =>
                options.UseSqlite($"Data Source={databasePath}"));

            services.AddControllers(options =>
                    {
                        WriteLine("Default output formatters:");
                        foreach (IOutputFormatter formatter in options.OutputFormatters)
                        {
                            var mediaFormatter = formatter as OutputFormatter;

                            if (mediaFormatter == null)
                            {
                                WriteLine($"  {formatter.GetType().Name}");
                            }
                            else // OutputFormatter class has SupportedMediaTypes
                            {
                                WriteLine("  {0}, Media types: {1}",
                      arg0: mediaFormatter.GetType().Name,
                      arg1: string.Join(", ", mediaFormatter.SupportedMediaTypes));
                            }
                        }
                    })
                    .AddXmlDataContractSerializerFormatters()
                    .AddXmlSerializerFormatters()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped<IAddressRepository, AddressRepository>();

            // Register the Swagger generator and define a Swagger document // for Northwind service
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(name: "v1", info: new OpenApiInfo
                { Title = "Northwind Service API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind Service API Version 1");
                options.SupportedSubmitMethods(new[] { SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete });
            });
        }
    }
}
