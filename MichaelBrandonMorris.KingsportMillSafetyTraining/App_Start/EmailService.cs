using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class EmailService.
    /// </summary>
    /// <seealso cref="IIdentityMessageService" />
    /// TODO Edit XML Comment Template for EmailService
    public class EmailService : IIdentityMessageService
    {
        /// <summary>
        ///     This method should send the message
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for SendAsync
        public Task SendAsync(IdentityMessage message)
        {
            using (var smtpClient = new SmtpClient())
            {
                var mailMessage = new MailMessage
                {
                    Body = message.Body,
                    IsBodyHtml = true,
                    Subject = message.Subject,
                    To = { message.Destination }
                };

                return smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}