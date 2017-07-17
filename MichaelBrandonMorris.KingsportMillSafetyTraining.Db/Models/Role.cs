using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    public class Role : IdentityRole
    {
        public string Description
        {
            get;
            set;
        }
    }
}
