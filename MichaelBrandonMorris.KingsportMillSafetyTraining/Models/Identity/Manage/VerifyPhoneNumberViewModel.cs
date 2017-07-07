using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Manage
{
    /// <summary>
    ///     Class VerifyPhoneNumberViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for VerifyPhoneNumberViewModel
    public class VerifyPhoneNumberViewModel
    {
        /// <summary>
        ///     Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        /// TODO Edit XML Comment Template for Code
        [Required]
        [Display(Name = "Code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        /// TODO Edit XML Comment Template for PhoneNumber
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get;
            set;
        }
    }
}