using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class EmailService.
    /// </summary>
    /// <seealso
    ///     cref="Microsoft.AspNet.Identity.IIdentityMessageService" />
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
            // TODO: Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}