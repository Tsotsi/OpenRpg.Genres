using System;
using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Stats.Populators;
using OpenRpg.Core.Stats.Variables;
using OpenRpg.Core.Variables;
using OpenRpg.Genres.Extensions;

namespace OpenRpg.Genres.Stats.Populators.Conventions
{
    public class BasicStatPartialPopulator : IPartialStatPopulator
    {
        public int Priority { get; }
        public int EffectBonusAmountType { get; }
        public int EffectBonusPercentageType { get; }
        public int StatType { get; }
        public Func<IStatsVariables, IReadOnlyCollection<Effect>, IReadOnlyCollection<IVariables>, int> MiscGetter { get; } = null;

        public BasicStatPartialPopulator(int effectBonusAmountType, int effectBonusPercentageType, int statType, int priority = 100)
        {
            Priority = priority;
            EffectBonusAmountType = effectBonusAmountType;
            EffectBonusPercentageType = effectBonusPercentageType;
            StatType = statType;
        }
        
        public BasicStatPartialPopulator(int effectBonusAmountType, int effectBonusPercentageType, int statType,
            Func<IStatsVariables, IReadOnlyCollection<Effect>, IReadOnlyCollection<IVariables>, int> miscGetter,  
            int priority = 100)
            : this(effectBonusAmountType, effectBonusPercentageType, statType)
        {
            MiscGetter = miscGetter;
        }

        public void Populate(IStatsVariables stats, IReadOnlyCollection<Effect> activeEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var miscBonus = MiscGetter?.Invoke(stats, activeEffects, relatedVars) ?? 0;
            var attributeValue = (int)activeEffects.CalculateBonusFor(EffectBonusAmountType, EffectBonusPercentageType, miscBonus);
            stats[StatType] = attributeValue;
        }
    }
}