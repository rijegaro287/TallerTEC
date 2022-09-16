// using MailKit.Net.Smtp;
// using MimeKit;
using System;
using System.Threading.Tasks;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace Backend.Helpers;

public class EmailSender
{
    private static string apiKey = "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";
    public static async Task SendEmailAsync(string clientName,
                                            string clientEmail,
                                            string emailSubject,
                                            string emailBody)
    {
        var client = new SendGridClient(apiKey);

        var fromEmail = new EmailAddress("gatgens48@gmail.com", "TallerTEC");
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