using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class SelectGroupViewModel
    {
        public IList<GroupViewModel> Groups
        {
            get;
            set;
        }

        public int DefaultGroupIndex
        {
            get;
            set;
        }
    }
}