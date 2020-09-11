using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items;

using OpenRpg.Items.Equipment;

namespace OpenRpg.Genres.Fantasy.Equipment.Slots
{
    public class OffHandSlot : DefaultEquipmentSlot<IItem>
    {
        public override int SlotType => ItemTypes.OffhandItem;
    }
}