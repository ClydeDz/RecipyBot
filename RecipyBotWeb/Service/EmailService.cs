using RecipyBotWeb.Constants;
using RecipyBotWeb.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RecipyBotWeb.Service
{
    public class EmailService
    {
        public static void SendContactForm(ContactUsDataModel c)
        {
            SendGridMessage msg = new SendGridMessage();
            msg.From = new EmailAddress(SiteSettingsConstant.GeneralFromEmail);
            msg.Subject = SiteSettingsConstant.ContactUsSubjectLine;
            msg.AddTo(SiteSettingsConstant.GeneralToEmail);

            StringBuilder sbtext = new StringBuilder();
            StringBuilder sbhtml = new StringBuilder();

            // Can also use an HTML email template but I am keeping it simple
            sbhtml.Append("<h2>The Recipy Bot</h2><hr/><p>You have a new message from the Recipy Bot.</p><br/>");

            foreach (var property in c.GetType().GetProperties())
            {
                if (property.Name == "Email")
                {
                    // Set the reply to field of the email to the contact persons email address for ease of use
                    msg.ReplyTo = new EmailAddress(property.GetValue(c, null).ToString());
                }
                sbtext.Append(property.Name + ": " + property.GetValue(c, null) + "\r\n");
                sbhtml.Append("<p><span style='font-weight:bold;'>" + property.Name + ":</span>  " + property.GetValue(c, null) + "<p>");
            }

            // Email footer
            sbhtml.Append("<br /><p>Thanks,</p><p><b>The Recipy Bot</b></p>");

            msg.PlainTextContent = sbtext.ToString();
            msg.HtmlContent = sbhtml.ToString();
            SendMessage(msg).ConfigureAwait(false);
        }

        public static async Task SendMessage(SendGridMessage myMessage)
        {
            var client = new SendGridClient(SiteSettingsConstant.SendGridApiKey);
            var response = await client.SendEmailAsync(myMessage);
        }
    }
}