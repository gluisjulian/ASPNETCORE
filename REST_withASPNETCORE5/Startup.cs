using REST_withASPNETCORE5.Business.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using REST_withASPNETCORE5.Model.Context;
using Microsoft.EntityFrameworkCore;
using REST_withASPNETCORE5.Repository.Implementations;
using Serilog;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace REST_withASPNETCORE5
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }
        //private System.Action<DbContextOptionsBuilder> options;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connection = Configuration["MysqlConnection:MysqlConnectionString"];
            services.AddDbContext<MysqlContext>(options => options.UseMySql(connection));

            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }



            //Injeção de Dependencia
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database Migration Failed", ex);

                throw;
            }
        }
    }
}
