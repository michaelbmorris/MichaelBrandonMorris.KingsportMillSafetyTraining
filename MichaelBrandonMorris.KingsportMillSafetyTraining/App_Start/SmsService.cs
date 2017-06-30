using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class SmsService.
    /// </summary>
    /// <seealso cref="IIdentityMessageService" />
    /// TODO Edit XML Comment Template for SmsService
    public class SmsService : IIdentityMessageService
    {
        /// <summary>
        ///     This method should send the message
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for SendAsync
        public Task SendAsync(IdentityMessage message)
        {
            // TODO: Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}