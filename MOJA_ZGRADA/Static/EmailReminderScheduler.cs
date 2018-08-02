using MOJA_ZGRADA.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Static
{
    public class EmailReminderScheduler
    {
        private readonly MyDbContext _context;

        public EmailReminderScheduler(MyDbContext context)
        {
            _context = context;
        }
        
        public Task SchedulerStart()
        {
            foreach (var CreatedCleaningPlan in _context.Created_Cleaning_Plans.ToList())
            {

                if (CreatedCleaningPlan.Cleaning_Reminder == true /*&& (CreatedCleaningPlan.Cleaning_Reminder_DateTime >= DateTime.Now.ToLocalTime())*/)
                {
                    var Tenant = _context.Tenants.Where(i => i.Id == CreatedCleaningPlan.Tenant_Id).FirstOrDefault();

                    var TimeOfCleaning = _context.Created_Cleaning_Plans.Where(k => k.Tenant_Id == Tenant.Id).Select(t => t.Cleaning_DateTime).FirstOrDefault();
                    
                    SmtpClient smtpClient = new SmtpClient("smtp.live.com");
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("mail1@hotmail.com");
                    mailMessage.To.Add("mail2@hotmail.com");
                    mailMessage.Body = "Dragi "+ Tenant.UserName +" vreme ciscenja je: "+ TimeOfCleaning;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Subject = "Cleaning date reminder";
                    //smtpClient.Host = "localhost";

                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("my@hotmail.com", "pass");
                    smtpClient.EnableSsl = true;

                    smtpClient.Send(mailMessage);


                    CreatedCleaningPlan.Cleaning_Reminder = false;

                }

            }
            return Console.Out.WriteLineAsync("Emails sent!"); ;
        }
    }
}
