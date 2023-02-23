using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace search
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
            services.AddControllers()
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddScoped<OpensearchHandler>();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CheckEnvVariables();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void CheckEnvVariables()
        {
            var missingVariables = new List<string>();

            // Check each environment variable and add its name to the missingVariables list if it's not set
            foreach (var fieldName in typeof(EnvironmentVariables).GetFields())
            {
                var value = (string)fieldName.GetValue(null);
                var environmentVariable = Environment.GetEnvironmentVariable(value);

                if (string.IsNullOrEmpty(environmentVariable))
                {
                    missingVariables.Add(value);
                }
            }

            // If there are missing environment variables, stop the API
            if (missingVariables.Any())
            {
                var missingVariablesMessage = string.Join(", ", missingVariables);
                throw new Exception($"The following environment variables are not set: {missingVariablesMessage}");
            }
        }
    }
}

