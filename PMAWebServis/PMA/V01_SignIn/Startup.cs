using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Services;

namespace V01_SignIn
{
    /*
     Scaffold-DbContext "Server=localhost;Database=TEST;User ID=sa;Password=Local123;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data\Models -Force 
     */
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
            // CORS = Cross Origin Resource Sharing
            // osiguranje servera od hakerskih napada
            /*
            services.AddCors(options =>
            {
                options.AddPolicy("Policy1",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:54580/",
                                            "http://www.contoso.com")
                                                .WithMethods("POST", "PUT", "DELETE", "GET");
                    });

                options.AddPolicy("Policy2",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44308")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            */
            // Cross Origin Resoucr Sharing
            
            services.AddCors(c =>
            {
                c.AddPolicy("Policy3", options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
            
            services.AddControllers();
            services.AddScoped<IUserSignInManager, UserSignInManager>();
            services.AddScoped<IAccountManager, AccountManager>();
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

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
