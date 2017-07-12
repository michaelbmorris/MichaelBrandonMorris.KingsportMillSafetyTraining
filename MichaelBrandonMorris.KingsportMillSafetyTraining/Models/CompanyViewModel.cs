using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class CompanyViewModel
    {
        public CompanyViewModel(Company company)
        {
            Employees = company.GetEmployees().AsViewModels();
            Id = company.Id;
            Name = company.Name;
        }

        public IList<UserViewModel> Employees
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}