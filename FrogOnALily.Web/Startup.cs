using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.S3;
using FrogOnALily.Photoshoots.Query;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
using System.IO;
using System.Reflection;

namespace FrogOnALily
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            HostingEnvironment = env;
            Configuration = config;
        }

        private IHostingEnvironment HostingEnvironment { get; }
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddSingleton<IImageRepository, AwsImageRepository>();
            var awsOptions = Configuration.GetAWSOptions();
            awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();
            awsOptions.Region = RegionEndpoint.USEast1;
            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonS3>();
            services.AddAWSService<IAmazonDynamoDB>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, new SwaggerUiSettings()
                {
                    DefaultPropertyNameHandling = PropertyNameHandling.CamelCase
                });
            }
            
           app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
