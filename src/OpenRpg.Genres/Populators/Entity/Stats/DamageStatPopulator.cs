using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Stats.Entity;
using OpenRpg.Core.Variables;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Populators.Entity.Stats
{
    public class DamageStatPopulator : IEntityPartialStatPopulator
    {
        public int Priority => 10;

        public float ComputeTotal(IReadOnlyCollection<Effect> effects)
        {
            var amount = effects.GetPotencyFor(EffectTypes.DamageBonusAmount);
            var percentage = effects.GetPotencyFor(EffectTypes.DamageBonusPercentage);

            if (percentage == 0)
            { return amount; }

            var addition = amount * (percentage/100);
            return amount + addition;
        }
        
        public void Populate(IEntityStatsVariables stats, IReadOnlyCollection<Effect> activeEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var totalValue = ComputeTotal(activeEffects);
            stats.Damage(totalValue);
        }
    }
}