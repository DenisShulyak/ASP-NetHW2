using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using System.Reactive.Subjects;

namespace WebApplicationHW2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<RedirectToRouteResult> SendRequest(string name, string email, string message)
        {
            var compossedData = $"{name} - {email} - {message}";
            // Отправка сообщения на почту
            //AIzaSyA9TCTQ3sR8ZuLzhCIWel1aH9zcvyqPBWE
            var messageForUser = "Спасибо за оставленные данные! В ближайшее время с вами свяжется наша организация.";
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "sendmailtest@inbox.ru"));
            emailMessage.To.Add(new MailboxAddress(name,email));
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = messageForUser
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.inbox.ru", 25, false);
                client.Authenticate("sendmailtest@inbox.ru", "test1234mail");
                client.Send(emailMessage);
                client.Disconnect(true);
            }

            return RedirectToAction("Index");
        }
    }

    
}