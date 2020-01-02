using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;


namespace SmCty.Framework.Common.APIGateway.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this._corsPolicyName = Configuration.GetSection("Cors").GetSection("DefaultPolicyName").Value;
        }

        public IConfiguration Configuration { get; }
        private readonly string _corsPolicyName;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var corsPolicy = Configuration.GetSection("Cors").GetSection(_corsPolicyName);
            var allowedOrigins = corsPolicy.GetSection("AllowedOrigins").Value.Split(",");
            var allowedHeaders = corsPolicy.GetSection("AllowedHeaders").Value.Split(",");
            var allowedMethods = corsPolicy.GetSection("AllowedMethods").Value.Split(",");

            services.AddControllers();
            services.AddOcelot();

            //Default policy allows calls from any origin with any header and method. Use a specific policy to tighten the cors relaxation
            services.AddCors(options =>
            {
                options.AddPolicy(_corsPolicyName, builder =>
                {
                    builder.WithOrigins(allowedOrigins)
                            .WithHeaders(allowedHeaders).WithMethods(allowedMethods);
                });

                //default cors policy
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                logger.LogInformation("Running Gateway in Development Setup");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors(); // without parameter uses default cors policy
            app.UseCors(_corsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot().Wait();
        }

    }

}