using System;
using System.Net.Mail;
namespace proyectoPrograAvanz.Helpers
{
    
        public class EmailNotification
        {

            public bool notificarAUsuario(string body, string toEmail, string toName, string asunto)
            {
                try
                {
                    SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
                    // set smtp-client with basicAuthentication
                    mySmtpClient.UseDefaultCredentials = false;
                    System.Net.NetworkCredential basicAuthenticationInfo = new
                    System.Net.NetworkCredential("healthexperts479@gmail.com", "adHE2031");
                    mySmtpClient.Credentials = basicAuthenticationInfo;

                    // add from,to mailaddresses
                    MailAddress from = new MailAddress("healthexperts479@gmail.com", "Health Experts");
                    MailAddress to = new MailAddress(toEmail, toName);
                    MailMessage myMail = new System.Net.Mail.MailMessage(from, to);                 

                    // set subject and encoding
                    myMail.Subject = asunto;
                    myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                    // set body-message and encoding
                    myMail.Body = body;
                    myMail.BodyEncoding = System.Text.Encoding.UTF8;
                    // text or html
                    myMail.IsBodyHtml = true;
                    mySmtpClient.Port = 587;
                    mySmtpClient.EnableSsl = true;
                    mySmtpClient.Send(myMail);

                }

                catch (SmtpException ex)
                {
                    throw new ApplicationException
                      ("SmtpException has occured: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                return true;
            }
        }
    }
