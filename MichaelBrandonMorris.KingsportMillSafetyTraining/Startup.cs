using System.Configuration;
using System.Net.Mail;
using System.Web.Security;
using MichaelBrandonMorris.KingsportMillSafetyTraining;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using MichaelBrandonMorris.Extensions.CollectionExtensions;

[assembly: OwinStartup(typeof(Startup))]

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class Startup.
    /// </summary>
    /// TODO Edit XML Comment Template for Startup
    public partial class Startup
    {
        /// <summary>
        ///     Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// TODO Edit XML Comment Template for Configuration
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateOwner();
            UpdateCurrentIndices();
            UpdateUserRoles();
        }

        /// <summary>
        ///     Updates the current indices.
        /// </summary>
        /// TODO Edit XML Comment Template for UpdateCurrentIndices
        private static void UpdateCurrentIndices()
        {
            using (var context = new KingsportMillSafetyTrainingDbContext())
            using (var answerStore = new AnswerStore(context))
            using (var answerManager = new AnswerManager(answerStore))
            using (var categoryStore = new CategoryStore(context))
            using (var categoryManager = new CategoryManager(categoryStore))
            using (var groupStore = new GroupStore(context))
            using (var groupManager = new GroupManager(groupStore))
            {
                answerManager.UpdateCurrentIndex();
                categoryManager.UpdateCurrentIndex();
                groupManager.UpdateCurrentIndex();
            }
        }

        /// <summary>
        ///     Creates the owner account.
        /// </summary>
        /// TODO Edit XML Comment Template for CreateRoles
        private async void CreateOwner()
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            using (var store = new UserStore(db))
            using (var manager = new UserManager(store))
            {
                var ownerUserName =
                    ConfigurationManager.AppSettings["OwnerUserName"];

                var user = await manager.FindByNameAsync(ownerUserName);

                if (user != null)
                {
                    return;
                }

                user = new User
                {
                    UserName = ownerUserName,
                    Email = ownerUserName
                };

                var password = Membership.GeneratePassword(8, 1);
                await manager.CreateAsync(user, password);
                user = await manager.FindByEmailAsync(user.Email);
                await manager.AddToRoleAsync(user.Id, "Owner");

                using (var smtpClient = new SmtpClient())
                {
                    using (var mailMessage = new MailMessage
                    {
                        Body = password,
                        Subject = "Kingsport Mill Safety Training Owner Password"
                    })
                    {
                        var toAddress = new MailAddress(user.Email);
                        mailMessage.To.Add(toAddress);
                        smtpClient.Send(mailMessage);
                    }
                }
            }
        }

        /// <summary>
        ///     Updates the user roles.
        /// </summary>
        /// TODO Edit XML Comment Template for UpdateUserRoles
        private static async void UpdateUserRoles()
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            using (var store = new UserStore(db))
            using (var manager = new UserManager(store))
            {
                foreach (var user in await manager.Users.ToListAsync())
                {
                    if (user.Roles.IsNullOrEmpty())
                    {
                        await manager.AddToRoleAsync(user.Id, "User");
                    }
                }
            }
        }
    }
}