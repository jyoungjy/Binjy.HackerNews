using Binjy.HackerNews.Core.Interface;
using Binjy.HackerNews.Core.Model;
using Binjy.HackerNews.Core.Service;
using Binjy.HackerNews.Core.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Binjy.HackerNews.WebClient.Angular
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
            services.AddHttpClient();
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // wiring up application specific dependencies
            var restApiConfig = new RestApiConfig();
            services.AddSingleton(typeof(IApiConfig), restApiConfig);

            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            services.AddSingleton(typeof(JsonSerializerSettings), serializerSettings);

            var storyMapper = new StoryMapper(serializerSettings);
            services.AddSingleton(typeof(IItemMapper<Story>), storyMapper);

            var commentMapper = new CommentMapper(serializerSettings);
            services.AddSingleton(typeof(IItemMapper<Comment>), commentMapper);

            IServiceProvider provider = services.BuildServiceProvider(false);
            services.AddSingleton<IHackerNewsClient<Story>>(c =>
            {
                return new HackerNewsRestClient<Story>(restApiConfig,
                    provider.GetService<IHttpClientFactory>(),
                    provider.GetService<ILogger<HackerNewsRestClient<Story>>>(),
                    storyMapper);
            }
            );

            services.AddSingleton<IHackerNewsClient<Comment>>(c =>
            {
                return new HackerNewsRestClient<Comment>(restApiConfig,
                    provider.GetService<IHttpClientFactory>(),
                    provider.GetService<ILogger<HackerNewsRestClient<Comment>>>(),
                    commentMapper);
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
