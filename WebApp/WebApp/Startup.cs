﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace WebApp
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup( IHostingEnvironment env )
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath( env.ContentRootPath )
                .AddJsonFile( "appsettings.json", optional: true, reloadOnChange: true )
                .AddJsonFile( $"appsettings.{env.EnvironmentName}.json", optional: true )
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices( IServiceCollection services )
        {
            services.AddOptions();
            services.Configure<WinOrLooseOptions>( Configuration.GetSection( "WinOrLoose" ) );
            services.AddScoped<ILooseOrWinService, ConstantLoseOrWinService>();
        }

        public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
        {
            loggerFactory.AddConsole();

            if( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            app.Map( "/loto", lotoApp => 
            {
                lotoApp.UseMiddleware<WinOrLooseMiddleware>();

                lotoApp.Run( async ( context ) =>
                {
                    await context.Response.WriteAsync( "Hello World!" );
                } );
            } );

            app.Run( async ( context ) =>
             {
                 await context.Response.WriteAsync( "Hello World!" );
             } );
        }
    }
}
