using System.Collections.Generic;
using System.Linq;

namespace Fulfillment.Models
{
    public class ShipmentCostResult
    {
        public List<ShipmentCostItem> Items { get; }
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
        public string Id { get; }
        public int Cost { get; }
        public OrderItemType Type { get; }

        public ShipmentCostItem(string id, int cost, OrderItemType type)
        {
            Id = id;
            Cost = cost;
            Type = type;
        }
    }
}
