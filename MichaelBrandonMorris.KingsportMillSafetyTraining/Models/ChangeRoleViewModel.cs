using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class ChangeRoleViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for ChangeRoleViewModel
    public class ChangeRoleViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ChangeRoleViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public ChangeRoleViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ChangeRoleViewModel" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roles"></param>
        /// TODO Edit XML Comment Template for #ctor
        public ChangeRoleViewModel(User user, string roleName, IList<string> roleNames)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Email = user.Email;

            Name = user.MiddleName.IsNullOrWhiteSpace()
                ? $"{user.FirstName} {user.LastName}"
                : $"{user.FirstName} {user.MiddleName} {user.LastName}";

            RoleName = roleName;           
            UserId = user.Id;
            RoleNames = roleNames;
        }

        /// <summary>
        ///     Gets the roles.
        /// </summary>
        /// <value>The roles.</value>
        /// TODO Edit XML Comment Template for Groups
        public IList<string> RoleNames
        {
            get;
        }

        /// <summary>
        ///     Gets the role select list.
        /// </summary>
        /// <value>The role select list.</value>
        /// TODO Edit XML Comment Template for RoleSelectList
        public IEnumerable<SelectListItem> RoleSelectList => RoleNames.Select(
            role => new SelectListItem
            {
                Value = role,
                Text = role
            });

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
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        /// TODO Edit XML Comment Template for Name
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the role name.
        /// </summary>
        /// <value>The role identifier.</value>
        /// TODO Edit XML Comment Template for RoleId
        [DisplayName("Role")]
        public string RoleName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        /// TODO Edit XML Comment Template for UserId
        public string UserId
        {
            get;
            set;
        }
    }
}