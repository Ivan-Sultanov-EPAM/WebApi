using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Northwind.Configuration;
using Northwind.Data;

namespace Northwind
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
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);

            services.AddControllers();
            services.AddSingleton(appSettings);
            services.AddDbContext<NorthwindContext>(options =>
                options.UseSqlServer(appSettings.ConnectionString));

            services
                .AddSwaggerGen()
                .AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, NorthwindContext dbContext)
        {
            dbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Northwind");
                c.DisplayOperationId();
                c.RoutePrefix = string.Empty;
            });
            
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
