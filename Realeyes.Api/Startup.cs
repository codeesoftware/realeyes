using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Realeyes.Infrastructure.IOCs;
using Serilog;

namespace Realeyes.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Realeyes.Api", Version = "v1" });
            });

            Boostrap.Populate(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(configure => configure.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                Exception exception = exceptionHandlerPathFeature.Error;
                var logger =context.RequestServices.GetService<ILogger<Exception>>();
                logger.LogError(exception, "Server error!");
                await context.Response.WriteAsJsonAsync(new { error = exception.Message, data = exception.Data });
            }));
            app.UseSerilogRequestLogging();
            app.UseRouting();

            //   app.UseHttpsRedirection();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger()
          .UseSwaggerUI(c =>
          {
              c.ConfigObject.DisplayRequestDuration = true;
              c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
          });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            Boostrap.RegisterAssemblyModulesen(builder);
        }

    }
}
