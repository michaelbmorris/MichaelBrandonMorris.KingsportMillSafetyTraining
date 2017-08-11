using System;
using System.Collections.Generic;
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
        /// TODO Edit XML Comment Template for #ctor
        public ChangeRoleViewModel(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Email = user.Email;

            Name = user.MiddleName.IsNullOrWhiteSpace()
                ? $"{user.FirstName} {user.LastName}"
                : $"{user.FirstName} {user.MiddleName} {user.LastName}";

            RoleId = user.Roles.Single().RoleId;           
            UserId = user.Id;
        }

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
        ///     Gets the role select list.
        /// </summary>
        /// <value>The role select list.</value>
        /// TODO Edit XML Comment Template for RoleSelectList
        public IEnumerable<SelectListItem> RoleSelectList => Roles.Select(
            role => new SelectListItem
            {
                Value = role.Id,
                Text = role.Name
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
        ///     Gets or sets the role identifier.
        /// </summary>
        /// <value>The role identifier.</value>
        /// TODO Edit XML Comment Template for RoleId
        public string RoleId
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

        /// <summary>
        ///     Gets the roles.
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
    }
}