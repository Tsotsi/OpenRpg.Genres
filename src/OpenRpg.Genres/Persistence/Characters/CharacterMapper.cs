using System.Linq;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Races;
using OpenRpg.Core.State.Entity;
using OpenRpg.Core.Variables.Entity;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Persistence.Classes;
using OpenRpg.Genres.Persistence.Items;
using OpenRpg.Genres.Persistence.Items.Equipment;
using OpenRpg.Genres.Persistence.Items.Inventory;
using OpenRpg.Genres.Types;
using OpenRpg.Items.Extensions;

namespace OpenRpg.Genres.Persistence.Characters
{
    public abstract class CharacterMapper : ICharacterMapper
    {
        public IItemMapper ItemMapper { get; }
        public IClassMapper ClassMapper { get; }
        public IEquipmentMapper EquipmentMapper { get; }
        public IInventoryMapper InventoryMapper { get; }

        protected CharacterMapper(IItemMapper itemMapper, IClassMapper classMapper, IEquipmentMapper equipmentMapper, IInventoryMapper inventoryMapper)
        {
            ItemMapper = itemMapper;
            ClassMapper = classMapper;
            EquipmentMapper = equipmentMapper;
            InventoryMapper = inventoryMapper;
        }

        public ICharacter Map(CharacterData data)
        {
            var entityVariables = new DefaultEntityVariables();

            if (data.Variables.ContainsKey(GenreEntityVariableTypes.Equipment))
            {
                var equipment = EquipmentMapper.Map(data.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData);
                entityVariables.Equipment(equipment);
            }
            
            if (data.Variables.ContainsKey(GenreEntityVariableTypes.Inventory))
            {
                var inventory = InventoryMapper.Map(data.Variables[GenreEntityVariableTypes.Inventory] as InventoryData);
                entityVariables.Inventory(inventory);
            }
            
            var characterState = new DefaultEntityStateVariables(data.StateVariables
                .ToDictionary(x => x.Key, x => x.Value));

            var raceTemplate = GetRaceTemplateFor(data.RaceTemplateId);
            var characterClass = ClassMapper.Map(data.ClassData);
            
            return InitializeCharacter(data, characterState, entityVariables, raceTemplate, characterClass);
        }

        public virtual ICharacter InitializeCharacter(CharacterData data, IEntityStateVariables state, IEntityVariables variables, IRaceTemplate raceTemplate, IClass @class)
        {
            return new DefaultCharacter
            {
                NameLocaleId = data.NameLocaleId,
                DescriptionLocaleId = data.DescriptionLocaleId,
                UniqueId = data.Id,
                Variables = variables,
                Class = @class,
                Race = raceTemplate,
                State = state,
                GenderType = data.GenderType
            };
        }
        
        public abstract IRaceTemplate GetRaceTemplateFor(int raceTemplateId);
    }
}