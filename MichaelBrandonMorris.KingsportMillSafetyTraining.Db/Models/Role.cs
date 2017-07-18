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
        public Role()
        {
            Index = ++CurrentIndex;
        }

        public static int CurrentIndex
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }
    }
}
