using System.Data.Entity;
using System.Linq;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    public class TrainingResultStore : EntityStore<TrainingResult, int>
    {
        public TrainingResultStore(DbContext context)
            : base(context)
        {
        }

        public IQueryable<TrainingResult> TrainingResults => Entities;
    }
}