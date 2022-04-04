using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NextTech.Core.Exceptions;
using NextTech.Core.MediatR.Config;
using NextTech.Core.WebApi.Extensions;
using NextTech.Core.WebApi.Middlewares.ExceptionHandling;
using NextTech.PayHub.Accounting.Application;
using NextTech.PayHub.Accounting.Application.Helper;
using NextTech.PayHub.Accounting.Infrastructure.EF.Config;
using System.Net;

namespace NextTech.PayHub.Accounting.Api
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
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NextTech.PayHub.Accounting.Api", Version = "v1" });
            });
            services.AddMediatRServices();
            services.AddApplicationModule(Configuration);
            services.AddEFModule(Configuration);
            services.AddApiVersioningExtension();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NextTech.PayHub.Accounting.Api v1"));
            }


            app.UseExceptionHandlingMiddleware(exception => exception switch
            {
                CardInvalidException _ => HttpStatusCode.BadRequest,
                AccountNotFound _ => HttpStatusCode.BadRequest,
                AggregateNotFoundException _ => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            });

            app.UseDataInitializer();
            app.UseRequestLogMiddleware();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
