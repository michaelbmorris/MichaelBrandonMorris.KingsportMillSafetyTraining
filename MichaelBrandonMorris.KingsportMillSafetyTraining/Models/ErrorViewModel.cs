using System.Net;
using System.Text.RegularExpressions;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class ErrorViewModel
    {
        public string CodeName => Regex.Replace(
            Code.ToString(),
            @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))",
            " $0");

        public int CodeNumber => (int) Code;

        public HttpStatusCode Code
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
}