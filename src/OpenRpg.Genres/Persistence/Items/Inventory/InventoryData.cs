using System.Collections.Generic;

namespace OpenRpg.Genres.Persistence.Items.Inventory
{
    public class InventoryData
    {
        public IReadOnlyCollection<ItemData> Items { get; }
        public IReadOnlyDictionary<int, object> Variables { get; }

        public InventoryData(IReadOnlyCollection<ItemData> items, IReadOnlyDictionary<int, object> variables)
        {
            Items = items;
            Variables = variables;
        }
    }
}