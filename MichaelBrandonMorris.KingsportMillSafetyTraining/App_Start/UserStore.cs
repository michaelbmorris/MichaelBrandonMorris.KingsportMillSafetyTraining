using System.Data.Entity;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class UserStore.
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.Identity.EntityFramework.UserStore{T}" />
    /// TODO Edit XML Comment Template for UserStore
    public class UserStore
        : UserStore<User, Role, string, IdentityUserLogin, IdentityUserRole,
            IdentityUserClaim>
    {
        /// <summary>
        ///     Constructor which takes a db context and wires up the
        ///     stores with default instances using the context
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public UserStore(DbContext context)
            : base(context)
        {
        }
    }
}