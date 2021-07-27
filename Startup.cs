using System;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using colonist_extension.Database;
using colonist_extension.Models.Configuration;
using colonist_extension.Repositories;
using colonist_extension.Repositories.Database;

namespace colonist_extension
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
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "COLONIST API", Version = "v1" });
            });

            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddSwaggerGenNewtonsoftSupport();

            // Register Repositories
            services.AddSingleton<IEventRepository, EventRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IGameRepository, GameRepository>();
            services.AddSingleton<IDatabaseConnection, MySQLConnection>();

            // Register Configuration
            services.Configure<MySqlSettings>(Configuration.GetSection("MySqlSettings"));

            // Configure FluentMigrator for database migrations
            services.AddFluentMigratorCore()
                .ConfigureRunner(builder => builder
                    .AddMySql5()
                    .WithGlobalConnectionString(new MySQLConnection(Configuration.GetSection("MySqlSettings").Get<MySqlSettings>()).GetConnectionString())
                    .ScanIn(typeof(_001_SetupDatabase).Assembly).For.Migrations());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin();
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });

            // Database setup
            MySQLDatabaseSetup.TryCreateDatabase(Configuration.GetSection("MySqlSettings").Get<MySqlSettings>());

            // Start database migrations
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
