using System;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    /// <summary>
    ///     Interface IEntity
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// TODO Edit XML Comment Template for IEntity`1
    public interface IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// TODO Edit XML Comment Template for Id
        TKey Id
        {
            get;
            set;
        }
    }
}