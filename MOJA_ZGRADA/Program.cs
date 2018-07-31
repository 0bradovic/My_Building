using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOJA_ZGRADA.Static;
using Quartz;
using Quartz.Impl;

namespace MOJA_ZGRADA
{
    public class Program
    {
        private static IScheduler _scheduler;
        
        public static void Main(string[] args)
        {

            //CreateWebHostBuilder(args).Build().Run();


            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            StartSchedulerAsync(); // add this line

            host.Run();

            //StartScheduler();


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


        private static async Task StartSchedulerAsync()
        {
            var properties = new NameValueCollection
            {
                // json serialization is the one supported under .NET Core (binary isn't)
                ["quartz.serializer.type"] = "json",

                // the following setup of job store is just for example and it didn't change from v2
                // according to your usage scenario though, you definitely need 
                // the ADO.NET job store and not the RAMJobStore.
                ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
                ["quartz.jobStore.useProperties"] = "false",
                ["quartz.jobStore.dataSource"] = "default",
                ["quartz.jobStore.tablePrefix"] = "QRTZ_",
                ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
                ["quartz.dataSource.default.provider"] = "SqlServer-41", // SqlServer-41 is the new provider for .NET Core
                ["quartz.dataSource.default.connectionString"] = @"Server=(localdb)\MSSQLLocalDB;Database=MOJA_ZGRADA;Integrated Security=true"
            };

            //var schedulerFactory = new StdSchedulerFactory(properties);
            //_scheduler = schedulerFactory.GetScheduler().Result;
            //_scheduler.Start().Wait();

            
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = await sf.GetScheduler();
            await sched.Start();

            DateTime utcTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            DateTimeOffset targetTime = new DateTimeOffset(utcTime);

   
      

            var userEmailsJob = JobBuilder.Create<EmailReminderScheduler>()
                .WithIdentity("SendUserEmails")
                .Build();
            var userEmailsTrigger = TriggerBuilder.Create()
                .WithIdentity("UserEmailsCron")
                .StartAt(targetTime)
                //.StartNow()
                //.WithCronSchedule("0 0 17 ? * MON,TUE,WED")
                .Build();

            _scheduler.ScheduleJob(userEmailsJob, userEmailsTrigger).Wait();

            //var adminEmailsJob = JobBuilder.Create<SendAdminEmailsJob>()
            //    .WithIdentity("SendAdminEmails")
            //    .Build();
            //var adminEmailsTrigger = TriggerBuilder.Create()
            //    .WithIdentity("AdminEmailsCron")
            //    .StartNow()
            //    .WithCronSchedule("0 0 9 ? * THU,FRI")
            //    .Build();

            //_scheduler.ScheduleJob(adminEmailsJob, adminEmailsTrigger).Wait();
        }
    }
}
