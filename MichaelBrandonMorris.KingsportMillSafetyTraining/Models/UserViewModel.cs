﻿using System;
using System.ComponentModel;
using System.Linq;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class UserViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for UserViewModel
    public class UserViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="UserViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public UserViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="UserViewModel" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// TODO Edit XML Comment Template for #ctor
        public UserViewModel(User user)
        {
            CompanyName = user.CompanyName;
            Email = user.Email;
            FirstName = user.FirstName;
            Id = user.Id;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            PhoneNumber = user.PhoneNumber;

            var lastTrainingResult = user.TrainingResults.LastOrDefault();

            if (lastTrainingResult == null)
            {
                return;
            }

            LastTrainingResultDateTime = lastTrainingResult.CompletionDateTime;

            LastTrainingResultId = lastTrainingResult.Id;
        }

        /// <summary>
        ///     Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        /// TODO Edit XML Comment Template for CompanyName
        [DisplayName("Company")]
        public string CompanyName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        /// TODO Edit XML Comment Template for Email
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        /// TODO Edit XML Comment Template for FirstName
        [DisplayName("First Name")]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// TODO Edit XML Comment Template for Id
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        /// TODO Edit XML Comment Template for LastName
        [DisplayName("Last Name")]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the last training result date time.
        /// </summary>
        /// <value>The last training result date time.</value>
        /// TODO Edit XML Comment Template for LastTrainingResultDateTime
        [DisplayName("Last Training Completed On")]
        public DateTime? LastTrainingResultDateTime
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the last training result identifier.
        /// </summary>
        /// <value>The last training result identifier.</value>
        /// TODO Edit XML Comment Template for LastTrainingResultId
        public int LastTrainingResultId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        /// TODO Edit XML Comment Template for MiddleName
        [DisplayName("Middle Name")]
        public string MiddleName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        /// TODO Edit XML Comment Template for PhoneNumber
        [DisplayName("Phone Number")]
        public string PhoneNumber
        {
            get;
            set;
        }
    }
}