using System;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
