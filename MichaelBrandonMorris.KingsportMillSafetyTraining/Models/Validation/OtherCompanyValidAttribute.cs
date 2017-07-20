using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Foolproof;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.Account;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Validation
{
    public class OtherCompanyValidAttribute : ModelAwareValidationAttribute
    {
        public override bool IsValid(object value, object container)
        {
            var model = container as RegisterViewModel;

            if (model == null)
            {
                throw new ArgumentException($"Attribute must be applied within a type of {typeof(RegisterViewModel)}");
            }

            // TODO Implement other company valid attribute
            throw new NotImplementedException();
        }
    }
}