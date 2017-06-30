using System.Net;
using System.Text.RegularExpressions;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class ErrorViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for ErrorViewModel
    public class ErrorViewModel
    {
        /// <summary>
        ///     Gets the name of the code.
        /// </summary>
        /// <value>The name of the code.</value>
        /// TODO Edit XML Comment Template for CodeName
        public string CodeName => Regex.Replace(
            Code.ToString(),
            @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))",
            " $0");

        /// <summary>
        ///     Gets the code number.
        /// </summary>
        /// <value>The code number.</value>
        /// TODO Edit XML Comment Template for CodeNumber
        public int CodeNumber => (int) Code;

        /// <summary>
        ///     Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        /// TODO Edit XML Comment Template for Code
        public HttpStatusCode Code
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        /// TODO Edit XML Comment Template for Message
        public string Message
        {
            get;
            set;
        }
    }
}