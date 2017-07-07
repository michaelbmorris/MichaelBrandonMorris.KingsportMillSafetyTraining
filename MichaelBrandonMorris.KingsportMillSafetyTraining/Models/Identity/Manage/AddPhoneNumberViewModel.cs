using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Manage
{
    /// <summary>
    ///     Class AddPhoneNumberViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for AddPhoneNumberViewModel
    public class AddPhoneNumberViewModel
    {
        /// <summary>
        ///     Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        /// TODO Edit XML Comment Template for Number
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number
        {
            get;
            set;
        }
    }
}