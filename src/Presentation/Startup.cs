using apollo.Application;
using apollo.Application.Common.Interfaces;
using apollo.Infrastructure;
using apollo.Infrastructure.HangFire;
using apollo.Infrastructure.Persistence;
using apollo.Infrastructure.Swagger;
using apollo.Presentation.Middlewares;
using apollo.Presentation.Options;
using apollo.Presentation.Utils;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Presentation.Services;
using System;
using System.Text.Json;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            OfferSlideHelper.Configuration = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureAppOptions(Configuration);
            services.AddApplication();

            services.AddInfrastructure(Configuration);

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfireServer();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddRouting(x => x.LowercaseUrls = true);

            services.AddControllers(x => x.Filters.Add<GlobalExceptionFilter>());

            services.ConfigureSwaggerForBearer();

            services.AddMvc()
                .AddMvcOptions(opt => opt.EnableEndpointRouting = false)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    opt.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddHttpClient();

            services.AddCors();

            services.AddResponseCaching();
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Apollo API v1"));

            app.UseHealthChecks("/health");

            app.UseHangfireDashboard("/ws-jobs", new DashboardOptions
            {
                AppPath = "https://stag-ws.azurewebsites.net/swagger",
                Authorization = new[] { new AuthFilter() }
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
               .SetIsOriginAllowed(origin => true)
               .WithExposedHeaders("Content-Disposition")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(async (context, next) =>
            {
                // For GetTypedHeaders, add: using Microsoft.AspNetCore.Http;
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(10)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding", };
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                await next();
            });
        }
    }
}
