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
            msg.Subject = "Contact us from The Recipy Bot";
            msg.AddTo(SiteSettingsConstant.GeneralToEmail);

            StringBuilder sbtext = new StringBuilder();
            StringBuilder sbhtml = new StringBuilder();
            
            foreach (var property in c.GetType().GetProperties())
            {
                sbtext.Append(property.Name + ": " + property.GetValue(c, null) + "\r\n");
                sbhtml.Append("<p><span style='font-weight:bold;'>" + property.Name + ":</span>  " + property.GetValue(c, null) + "<p>");
            }
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