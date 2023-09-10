using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Stats.Populators;
using OpenRpg.Core.Stats.Variables;
using OpenRpg.Core.Variables;
using OpenRpg.Genres.Fantasy.Effects;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Stats.Populators.Conventions;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Stats.Populators
{
    public class FantasyElementalDefenseStatPopulator : CompositeStatPopulator, IPartialStatPopulator
    {
        public int Priority => 10;
        
        public FantasyElementalDefenseStatPopulator()
        {
            PartialPopulators = new[]
            {
                new DamageOrDefenseStatPartialPopulator(EffectTypes.AllElementDefenseBonusAmount, EffectTypes.AllElementDefenseBonusPercentage, 
                    IGenreStatsVariableTypes.IceDefense, EffectRelationships.IceDefenseRelationship, GetElementalModBonus, Priority),
                
                new DamageOrDefenseStatPartialPopulator(EffectTypes.AllElementDefenseBonusAmount, EffectTypes.AllElementDefenseBonusPercentage, 
                    IGenreStatsVariableTypes.FireDefense, EffectRelationships.FireDefenseRelationship, GetElementalModBonus, Priority),
                
                new DamageOrDefenseStatPartialPopulator(EffectTypes.AllElementDefenseBonusAmount, EffectTypes.AllElementDefenseBonusPercentage, 
                    IGenreStatsVariableTypes.WindDefense, EffectRelationships.WindDefenseRelationship, GetElementalModBonus, Priority),
                
                new DamageOrDefenseStatPartialPopulator(EffectTypes.AllElementDefenseBonusAmount, EffectTypes.AllElementDefenseBonusPercentage, 
                    IGenreStatsVariableTypes.EarthDefense, EffectRelationships.EarthDefenseRelationship, GetElementalModBonus, Priority),
                
                new DamageOrDefenseStatPartialPopulator(EffectTypes.AllElementDefenseBonusAmount, EffectTypes.AllElementDefenseBonusPercentage, 
                    IGenreStatsVariableTypes.LightDefense, EffectRelationships.LightDefenseRelationship, GetElementalModBonus, Priority),
                
                new DamageOrDefenseStatPartialPopulator(EffectTypes.AllElementDefenseBonusAmount, EffectTypes.AllElementDefenseBonusPercentage, 
                    IGenreStatsVariableTypes.DarkDefense, EffectRelationships.DarkDefenseRelationship, GetElementalModBonus, Priority),
            };
        }

        public static float GetElementalModBonus(IStatsVariables stats, IReadOnlyCollection<Effect> activeEffects,
            IReadOnlyCollection<IVariables> relatedVars)
        { return stats.Intelligence() / 100.0f;}
    }
}