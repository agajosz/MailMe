using MailMe.Application.Extensions;
using MailMe.Backend.Helpers;
using MailMe.Data.Extensions;
using MailMe.Jobs.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MailMe.Backend
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MailMe.Backoffice", Version = "v1" });
            });
            services
                .AddAutoMapper(typeof(MappingProfile), typeof(MailMe.Data.Helpers.MappingProfile))
                .AddInfrastructure(Configuration)
                .AddDomain()
                .AddFixturesDataFeedJobs()
                .AddWeeklyLeagueNewsletterJobs()
                .AddCronJobHandler(Configuration);
            
                services.MigrateDatabase();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRecurringJobManager(Configuration);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MailMe.Backoffice v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.AddHangfireDashboard(Configuration);
            });
        }
    }
}