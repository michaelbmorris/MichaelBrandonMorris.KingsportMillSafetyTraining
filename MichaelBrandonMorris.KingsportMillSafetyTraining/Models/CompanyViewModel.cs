using System.Collections.Generic;
using System.Linq;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class CompanyViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for CompanyViewModel
    public class CompanyViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="CompanyViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public CompanyViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="CompanyViewModel" /> class.
        /// </summary>
        /// <param name="company">The company.</param>
        /// TODO Edit XML Comment Template for #ctor
        public CompanyViewModel(Company company)
        {
            Employees = company.Employees
                .Select(
                    e => new UserViewModel(
                        e,
                        null,
                        new List<Company>(),
                        new List<Role>(),
                        new List<Group>()))
                .ToList();
            Id = company.Id;
            Name = company.Name;
        }

        /// <summary>
        ///     Gets the supervisors.
        /// </summary>
        /// <value>The supervisors.</value>
        /// TODO Edit XML Comment Template for Supervisors
        public IList<UserViewModel> Supervisors => Employees
            .Where(employee => employee.Role?.Name == "Supervisor")
            .ToList();

        /// <summary>
        ///     Gets or sets the employees.
        /// </summary>
        /// <value>The employees.</value>
        /// TODO Edit XML Comment Template for Employees
        public IList<UserViewModel> Employees
        {
            get;
            set;
        } = new List<UserViewModel>();

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// TODO Edit XML Comment Template for Id
        public int Id
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
    }
}