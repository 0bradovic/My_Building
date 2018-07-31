using MOJA_ZGRADA.Context;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Static
{
    public class EmailReminderScheduler : IJob
    {
        private readonly MyDbContext _context;

        public EmailReminderScheduler(MyDbContext context)
        {
            _context = context;
        }

        public Task Execute(IJobExecutionContext context)
        {
            //throw new NotImplementedException();

            return SchedulerStart();
        }

        public Task SchedulerStart()
        {
            foreach (var CreatedCleaningPlan in _context.Created_Cleaning_Plans.ToList())
            {

                if (CreatedCleaningPlan.Cleaning_Reminder == true /*&& (CreatedCleaningPlan.Cleaning_Reminder_DateTime >= DateTime.Now)*/)
                {
                    var Tenant = _context.Tenants.Where(i => i.Id == CreatedCleaningPlan.Tenant_Id).FirstOrDefault();

                    var TimeOfCleaning = _context.Created_Cleaning_Plans.Where(k => k.Tenant_Id == Tenant.Id).Select(t => t.Cleaning_DateTime).FirstOrDefault();

                    SmtpClient client = new SmtpClient("mysmtpserver");
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("username", "password");

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("SENDER@EMAIL.com");
                    mailMessage.To.Add("RECIEVER@EMAIL.com");
                    mailMessage.Body = "Dragi "+ Tenant.UserName +" vreme ciscenja je: "+ TimeOfCleaning;
                    mailMessage.Subject = "Cleaning date reminder";
                    client.Send(mailMessage);
                }

            }
            return Console.Out.WriteLineAsync("Emails sent!"); ;
        }
    }
}
