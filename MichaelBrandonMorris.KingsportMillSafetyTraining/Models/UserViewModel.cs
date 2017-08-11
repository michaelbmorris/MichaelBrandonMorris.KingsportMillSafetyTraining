using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
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
            Email = user.Email;
            FirstName = user.FirstName;
            Id = user.Id;
            LastName = user.LastName;
            LastLogOnDateTime = user.LastLogOnDateTime;
            LastTrainingStartDateTime = user.LatestTrainingStartDateTime;
            MiddleName = user.MiddleName;
            OtherCompanyName = user.OtherCompanyName;
            PhoneNumber = user.PhoneNumber;
            Role = GetRole(user.Roles.Single().RoleId);

            if (user.Company != null)
            {
                CompanyId = user.Company.Id;
                CompanyName = user.Company.Name;
            }

            var lastTrainingResult = user.TrainingResults.LastOrDefault();

            if (lastTrainingResult == null)
            {
                return;
            }

            LastTrainingResultDateTime = lastTrainingResult.CompletionDateTime;
            LastTrainingResultId = lastTrainingResult.Id;
        }

        /// <summary>
        ///     Gets the companies.
        /// </summary>
        /// <value>The companies.</value>
        /// TODO Edit XML Comment Template for Companies
        public IList<Company> Companies
        {
            get;
        } = GetCompanies();

        /// <summary>
        ///     Gets the company identifier select list.
        /// </summary>
        /// <value>The company identifier select list.</value>
        /// TODO Edit XML Comment Template for CompanyIdSelectList
        public IList<SelectListItem> CompanyIdSelectList => Companies.Select(
            company => new SelectListItem
            {
                Value = company.Id.ToString(CultureInfo.InvariantCulture),
                Text = company.Name
            });

        /// <summary>
        ///     Gets the company name select list.
        /// </summary>
        /// <value>The company name select list.</value>
        /// TODO Edit XML Comment Template for CompanyNameSelectList
        public IList<SelectListItem> CompanyNameSelectList => Companies.Select(
            company => new SelectListItem
            {
                Value = company.Name,
                Text = company.Name
            });

        /// <summary>
        ///     Gets the groups.
        /// </summary>
        /// <value>The groups.</value>
        /// TODO Edit XML Comment Template for Groups
        public IList<Group> Groups
        {
            get;
        } = GetGroups();

        /// <summary>
        ///     Gets the group title select list.
        /// </summary>
        /// <value>The group title select list.</value>
        /// TODO Edit XML Comment Template for GroupTitleSelectList
        public IList<SelectListItem> GroupTitleSelectList => Groups.Select(
            group => new SelectListItem
            {
                Value = group.Title,
                Text = group.Title
            });

        /// <summary>
        /// Gets the last logon date time string.
        /// </summary>
        /// <value>The last logon date time string.</value>
        /// TODO Edit XML Comment Template for LastLogonDateTimeString
        [DisplayName("Last Logged On")]
        public string LastLogonDateTimeString => LastLogOnDateTime == null
            ? "User has not logged on."
            : LastLogOnDateTime.ToString();

        /// <summary>
        ///     Gets the last training start date time string.
        /// </summary>
        /// <value>The last training start date time string.</value>
        /// TODO Edit XML Comment Template for LastTrainingStartDateTimeString
        [DisplayName("Last Training Started On")]
        public string LastTrainingStartDateTimeString =>
            LastTrainingStartDateTime == null
                ? "Training not started"
                : LastTrainingStartDateTime.ToString();

        /// <summary>
        /// Gets the role name select list.
        /// </summary>
        /// <value>The role name select list.</value>
        /// TODO Edit XML Comment Template for RoleNameSelectList
        public IList<SelectListItem> RoleNameSelectList => Roles.Select(
            role => new SelectListItem
            {
                Value = role.Name,
                Text = role.Name
            });

        /// <summary>
        ///     Gets the roles.
        /// </summary>
        /// <value>The roles.</value>
        /// TODO Edit XML Comment Template for Groups
        public IList<Role> Roles
        {
            get;
        } = GetRoles();

        /// <summary>
        ///     Gets or sets the company identifier.
        /// </summary>
        /// <value>The identifier of the company.</value>
        /// TODO Edit XML Comment Template for CompanyName
        [Display(Name = "Company")]
        [Range(1, int.MaxValue, ErrorMessage = "This field is required.")]
        public int CompanyId
        {
            get;
            set;
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
        /// Gets or sets the last logon date time.
        /// </summary>
        /// <value>The last logon date time.</value>
        /// TODO Edit XML Comment Template for LastLogOnDateTime
        public DateTime? LastLogOnDateTime
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
        ///     Gets or sets the last training start date time.
        /// </summary>
        /// <value>The last training start date time.</value>
        /// TODO Edit XML Comment Template for LastTrainingStartDateTime
        [DisplayName("Last Training Started On")]
        public DateTime? LastTrainingStartDateTime
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
        [DisplayName("Other Company Name")]
        public string OtherCompanyName
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

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        /// TODO Edit XML Comment Template for Role
        public Role Role
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the companies.
        /// </summary>
        /// <returns>IList&lt;Company&gt;.</returns>
        /// TODO Edit XML Comment Template for GetCompanies
        private static IList<Company> GetCompanies()
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                return db.GetCompanies(company => company.Name);
            }
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <returns>IList&lt;Group&gt;.</returns>
        /// TODO Edit XML Comment Template for GetGroups
        private static IList<Group> GetGroups()
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                return db.GetGroups(group => group.Index);
            }
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns>IList&lt;Role&gt;.</returns>
        /// TODO Edit XML Comment Template for GetRoles
        private static IList<Role> GetRoles()
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                return db.GetRoles(role => role.Index);
            }
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Role.</returns>
        /// TODO Edit XML Comment Template for GetRole
        private static Role GetRole(string id)
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                return db.GetRole(id);
            }
        }
    }
}