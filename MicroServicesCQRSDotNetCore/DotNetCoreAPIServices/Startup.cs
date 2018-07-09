using Dot.Net.Core.Common.Settings;
using Dot.Net.Core.Interfaces.Repository;
using Dot.Net.Core.Interfaces.Service;
using Dot.Net.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreAPIServices
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
            services.AddMvcCore()
                .AddJsonFormatters();

            services.Configure<AppSettingsConfig>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<DatabaseSettingsConfig>(Configuration.GetSection("DatabaseSettings"));

            services.AddOptions();

            //depedency injection configuration
            services.AddTransient<IConnectToDatabase, ConnectDB>();
            services.AddTransient<IPalidromeRepository, PalindromeRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
