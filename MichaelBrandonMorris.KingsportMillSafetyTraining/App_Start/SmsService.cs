using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // TODO: Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}