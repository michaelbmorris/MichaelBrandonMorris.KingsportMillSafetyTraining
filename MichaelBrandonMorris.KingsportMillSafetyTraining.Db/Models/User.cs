using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    /// <summary>
    ///     Class User.
    /// </summary>
    /// <seealso cref="IdentityUser" />
    /// TODO Edit XML Comment Template for User
    public class User : IdentityUser
    {
        /// <summary>
        ///     Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        /// TODO Edit XML Comment Template for Company
        public virtual Company Company
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
        ///     Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        /// TODO Edit XML Comment Template for Group
        public virtual Group Group
        {
            get;
            set;
        }

        public DateTime? LastLogonDateTime
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
        ///     Gets or sets the latest quiz start date time.
        /// </summary>
        /// <value>The latest quiz start date time.</value>
        /// TODO Edit XML Comment Template for LatestQuizStartDateTime
        [DisplayName("Latest Quiz Started On")]
        public DateTime? LatestQuizStartDateTime
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the latest training start date time.
        /// </summary>
        /// <value>The latest training start date time.</value>
        /// TODO Edit XML Comment Template for LatestTrainingStartDateTime
        [DisplayName("Latest Training Started On")]
        public DateTime? LatestTrainingStartDateTime
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
        ///     Gets or sets the name of the other company.
        /// </summary>
        /// <value>The name of the other company.</value>
        /// TODO Edit XML Comment Template for OtherCompanyName
        [DisplayName("Other Company")]
        public string OtherCompanyName
        {
            get;
            set;
        }


        /// <summary>
        ///     PhoneNumber for the user
        /// </summary>
        /// <value>The phone number.</value>
        /// TODO Edit XML Comment Template for PhoneNumber
        public new string PhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the training results.
        /// </summary>
        /// <value>The training results.</value>
        /// TODO Edit XML Comment Template for TrainingResults
        [DisplayName("Training Results")]
        public virtual IList<TrainingResult> TrainingResults
        {
            get;
            set;
        } = new List<TrainingResult>();

        /// <summary>
        ///     generate user identity as an asynchronous operation.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <returns>Task&lt;ClaimsIdentity&gt;.</returns>
        /// TODO Edit XML Comment Template for GenerateUserIdentityAsync
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<User, string> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(
                this,
                DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}