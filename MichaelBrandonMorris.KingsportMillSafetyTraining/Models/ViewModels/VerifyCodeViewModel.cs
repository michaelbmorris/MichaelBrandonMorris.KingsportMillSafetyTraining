﻿using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.ViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code
        {
            get;
            set;
        }

        [Required]
        public string Provider
        {
            get;
            set;
        }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser
        {
            get;
            set;
        }

        public bool RememberMe
        {
            get;
            set;
        }

        public string ReturnUrl
        {
            get;
            set;
        }
    }
}