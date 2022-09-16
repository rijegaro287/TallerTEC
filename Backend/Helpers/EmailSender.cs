using SendGrid;
using SendGrid.Helpers.Mail;


namespace Backend.Helpers;

public class EmailSender
{
    public static async Task SendEmailAsync(string clientName,
                                            string clientEmail,
                                            string emailSubject,
                                            string emailBody)
    {
        DotNetEnv.Env.TraversePath().Load();
        string apiKey = DotNetEnv.Env.GetString("SENDGRID_API_KEY");

        var client = new SendGridClient(apiKey);

        var fromEmail = new EmailAddress("gatgens27@gmail.com", "TallerTEC");
        var toEmail = new EmailAddress(clientEmail, clientName);

        var plainTextContent = emailBody;
        var htmlContent = $"<h1>{emailBody}</h1>";
        var msg = MailHelper.CreateSingleEmail(fromEmail,
                                               toEmail,
                                               emailSubject,
                                               plainTextContent,
                                               htmlContent);

        var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
    }
}