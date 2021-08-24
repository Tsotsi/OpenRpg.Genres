using System.Linq;
using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Types;
using OpenRpg.Quests.States;

namespace OpenRpg.Genres.Requirements
{
    public class DefaultCharacterRequirementChecker : ICharacterRequirementChecker
    {
        public virtual bool IsRequirementMet(ICharacter character, Requirement requirement)
        {
            if (requirement.RequirementType == RequirementTypes.RaceRequirement)
            { return character.Race.Id == requirement.AssociatedId; }
            
            if (requirement.RequirementType == RequirementTypes.ClassRequirement)
            { return character.Class.ClassTemplate.Id == requirement.AssociatedId; }
            
            if (requirement.RequirementType == RequirementTypes.LevelRequirement)
            { return character.Class.Level >= requirement.AssociatedValue; }
            
            if(requirement.RequirementType == RequirementTypes.GenderRequirement)
            { return character.GenderType == requirement.AssociatedId; }
            
            if (requirement.RequirementType == RequirementTypes.EquipmentItemRequirement)
            {
                return character.Equipment.Slots.Values.Any(x =>
                    x.SlotType == requirement.AssociatedId &&
                    x.SlottedItem.ItemTemplate.Id == requirement.AssociatedValue);
            }
            
            return true;
        }

        public virtual bool IsRequirementMet(IGameState state, Requirement requirement)
        {
            if (requirement.RequirementType == RequirementTypes.TriggerRequirement)
            {
                var hasTrigger = state.Triggers.ContainsKey(requirement.AssociatedId);
                var triggerState = (requirement.AssociatedValue == 1);
                if(requirement.AssociatedValue == 0 && !hasTrigger) { return true; }
                
                return state.Triggers[requirement.AssociatedId] == triggerState;
            }

            if (requirement.RequirementType == RequirementTypes.QuestStateRequirement)
            {
                var hasQuestState = state.QuestStates.ContainsKey(requirement.AssociatedId);
                if(requirement.AssociatedValue == QuestStateTypes.QuestNotStarted && !hasQuestState) { return true; }

                return state.QuestStates[requirement.AssociatedId] == requirement.AssociatedValue;
            }
            
            return true;
        }
    }
}