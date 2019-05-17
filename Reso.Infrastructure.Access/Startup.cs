using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DataService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Reso.Infrastructure.Access
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
            RootConfig.Entry(services, Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            #region Swagger Config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("usecaseapi"
                    , new Info
                    {
                        Title = "Uni Access"
                        ,
                        Version = "v1.0"
                        ,
                        Description = "ASP.NET Core",
                    });
                var xmlFile = System.AppDomain.CurrentDomain.BaseDirectory + $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlFile);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();

            #region Swagger Config
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/usecaseapi/swagger.json", "Uni Log API V2.0");
                c.RoutePrefix = string.Empty;
                c.DefaultModelExpandDepth(0);
                c.DefaultModelsExpandDepth(-1);
            });
            #endregion

            app.UseMvc();
        }
    }
}
