using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MOJA_ZGRADA.Context;
using MOJA_ZGRADA.Data;
using MOJA_ZGRADA.Static;
using Quartz;
using Quartz.Impl;

using Hangfire;
using Hangfire.AspNetCore;
using Hangfire.SqlServer;

namespace MOJA_ZGRADA
{
    public class Startup
    {
        private readonly MyDbContext _context;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                 .SetBasePath(hostingEnvironment.ContentRootPath);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<RoleManager<MyRoleManager>>();

            services.AddIdentity<Account, MyRoleManager>()
                .AddEntityFrameworkStores<MyDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;   //***Turn TRUE for publish!***
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "http://www.boo.com",
                    ValidIssuer = "http://www.boo.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"))
                };
            });


            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAuthorization();

            HangfireService(services);
            //ConfigureQuartz(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MyDbContext context, UserManager<Account> userManager, RoleManager<MyRoleManager> roleManager)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"));

            app.UseHangfireServer();
            app.UseHangfireDashboard();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            Seed.Initialize(context, userManager, roleManager).Wait();



            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        void HangfireService(IServiceCollection services)
        {
            JobStorage.Current = new SqlServerStorage(Configuration.GetConnectionString("DefaultConnection"));

            EmailReminderScheduler x = new EmailReminderScheduler(_context);

            RecurringJob.AddOrUpdate(() => x.SchedulerStart() , Cron.Minutely);
        }

        void ConfigureQuartz(IServiceCollection services)
        {
            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler scheduler = sf.GetScheduler().Result;

            
            DateTime utcTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            DateTimeOffset targetTime = new DateTimeOffset(utcTime.ToLocalTime());
            

            var userEmailsJob = JobBuilder.Create<EmailReminderScheduler>()
                //.WithIdentity("SendUserEmails")
                .Build();

            var userEmailsTrigger = TriggerBuilder.Create()
                //.WithIdentity("UserEmailsCron")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .StartAt(targetTime)
                //.StartNow()
                //.WithCronSchedule("0 0 17 ? * MON,TUE,WED")
                .Build();
            
            scheduler.ScheduleJob(userEmailsJob, userEmailsTrigger).Wait();
            scheduler.Start();

            services.AddSingleton<IScheduler>(scheduler);

        }




    }
}
