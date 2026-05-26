using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace esapi_browser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();

            // Register EsapiSettings from server-side configuration
            var esapiSettings = new EsapiSettings();
            builder.Configuration.GetSection("EsapiSettings").Bind(esapiSettings);
            builder.Services.AddSingleton(esapiSettings);

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapRazorPages();
            app.MapControllers();

            app.Run();
        }
    }
}
