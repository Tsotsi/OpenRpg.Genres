using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Characters;
using OpenRpg.Quests;
using OpenRpg.Quests.Variables;

namespace OpenRpg.Genres.Requirements
{
    public interface ICharacterRequirementChecker : IRequirementChecker<ICharacter>
    {
        bool IsRequirementMet(IQuestState state, Requirement requirement);
        bool IsRequirementMet(ITriggerStateVariables state, Requirement requirement);
    }
}