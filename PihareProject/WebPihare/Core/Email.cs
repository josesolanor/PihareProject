using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebPihare.Entities;

namespace WebPihare.Core
{
    public class Email
    {
        
        public void SendEmail(Commisioner commisioner, string password)
        {
            SmtpClient client = new SmtpClient("mail.pihareii.com")
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("postmaster@pihareii.com", "Gusley123*")
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("postmaster@pihareii.com")
                
            };
            mailMessage.To.Add(commisioner.Email);
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"<html><body><h2>Bienvenido nuevo comisionista!!</h2><div><h3>Credenciales del nuevo usuario</h3>" +
                $"<dl><dt><b>Usuario:</b> {commisioner.Nic}</dt><dt><b>Password:</b> {password}</dt></dl></div></body></html>";
            mailMessage.Subject = "Credenciales de Usuario";
            client.Send(mailMessage);
        }
    }
}
