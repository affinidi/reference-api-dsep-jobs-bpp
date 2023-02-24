
using System;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using bpp.Filters;
using bpp.Security;
using bpp.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace bpp
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnv;

        private IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _hostingEnv = env;
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services
                .AddMvc(options =>
                {
                    options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
                    options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));

                })
                .AddXmlSerializerFormatters();
            services.AddSingleton<STDCodeHelper>()
                .AddSingleton<NetworkParticipantCache>()
                .AddScoped<ConfirmHandler>()
                .AddScoped<SelectHandler>()
                .AddScoped<SearchHandler>()
                .AddScoped<StausHandler>()
                .AddScoped<InitHandler>()
                .AddScoped<XinputHandler>();


            //services.AddAuthentication(ApiKeyAuthenticationHandler.SchemeName)
            //    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationHandler.SchemeName, null);

            //services.AddAuthentication(ApiKeyAuthenticationHandler.SchemeName)
            //    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationHandler.SchemeName, null);

            //services.AddAuthentication(ApiKeyAuthenticationHandler.SchemeName)
            //    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationHandler.SchemeName, null);


            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("DSEP-bpp", new OpenApiInfo
                    {

                        Contact = new OpenApiContact()


                    });
                    c.CustomSchemaIds(type => type.FullName);
                    c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");

                    // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
                    // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                    c.OperationFilter<GeneratePathParamsValidationFilter>();
                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            CheckEnvVariables();

            app.UseRouting();
            app.UseMiddleware<DSEPSigVerification>();

            app.UseAuthorization();

            app.UseSwagger(c => { c.SerializeAsV2 = true; });
            app.UseSwaggerUI(c =>
            {
                //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
                c.SwaggerEndpoint("./DSEP-bpp/swagger.json", "");


            });

            //TODO: Use Https Redirection
            // app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //TODO: Enable production exception handling (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling)
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }
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
