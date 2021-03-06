﻿using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Manage
{
    /// <summary>
    ///     Class SetPasswordViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for SetPasswordViewModel
    public class SetPasswordViewModel
    {
        /// <summary>
        ///     Gets or sets the confirm password.
        /// </summary>
        /// <value>The confirm password.</value>
        /// TODO Edit XML Comment Template for ConfirmPassword
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare(
            "NewPassword",
            ErrorMessage =
                "The new password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the new password.
        /// </summary>
        /// <value>The new password.</value>
        /// TODO Edit XML Comment Template for NewPassword
        [Required]
        [StringLength(
            100,
            ErrorMessage = "The {0} must be at least {2} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword
        {
            get;
            set;
        }
    }
}