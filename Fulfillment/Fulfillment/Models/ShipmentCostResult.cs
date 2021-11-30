using System.Collections.Generic;
using System.Linq;

namespace Fulfillment.Models
{
    public class ShipmentCostResult
    {
        public List<ShipmentCostItem> Items { get; private set; }
        public int TotalCost
        {
            get
            {
                return Items.Sum(i => i.Cost);
            }
        }

        public ShipmentCostResult(List<ShipmentCostItem> items)
        {
            Items = items;
        }
    }

    public class ShipmentCostItem
    {
        public string Id { get; private set; }
        public int Cost { get; private set; }
        public string Type { get; private set; }

        public ShipmentCostItem(string id, int cost, string type)
        {
            Id = id;
            Cost = cost;
            Type = type;
        }
    }
}
