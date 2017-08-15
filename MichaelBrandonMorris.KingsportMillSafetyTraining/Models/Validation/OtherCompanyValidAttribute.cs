using System;
using Foolproof;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.Account;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Validation
{
    /// <summary>
    ///     Class OtherCompanyValidAttribute.
    /// </summary>
    /// <seealso cref="ModelAwareValidationAttribute" />
    /// TODO Edit XML Comment Template for OtherCompanyValidAttribute
    public sealed class OtherCompanyValidAttribute : ModelAwareValidationAttribute
    {
        /// <summary>
        ///     Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="container">The container.</param>
        /// <returns>
        ///     <c>true</c> if the specified value is valid;
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        /// TODO Edit XML Comment Template for IsValid
        public override bool IsValid(object value, object container)
        {
            var model = container as RegisterViewModel;

            if (model == null)
            {
                throw new ArgumentException(
                    $"Attribute must be applied within a type of {typeof(RegisterViewModel)}");
            }

            Company company;

            using (var context = new KingsportMillSafetyTrainingDbContext())
            using (var store = new CompanyStore(context))
            using (var companyManager = new CompanyManager(store))
            {
                try
                {
                    company = companyManager.FindByIdAsync(model.CompanyId).Result;
                }
                catch (Exception)
                {
                    return true;
                }
            }

            if (!company.Name.EqualsOrdinalIgnoreCase("Other"))
            {
                return true;
            }

            var otherCompanyName = value as string;

            if (!otherCompanyName.IsNullOrWhiteSpace())
            {
                return true;
            }

            ErrorMessage =
                "Other Company must not be blank if 'Other' is selected in the Company drop down.";

            return false;
        }
    }
}