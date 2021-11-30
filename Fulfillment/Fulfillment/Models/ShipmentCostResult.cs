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
                var total = Items.Sum(i => i.Cost);

                if (Items.Any(i => i.ShipmentPriorityType == ShipmentPriorityType.Speedy))
                {
                    total *= 2;
                }

                return total;
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
        public OrderItemType OrderItemType { get; }
        public ShipmentPriorityType ShipmentPriorityType { get; set; }

        public ShipmentCostItem(
            string id, 
            int cost, 
            OrderItemType orderItemType, 
            ShipmentPriorityType shipmentPriorityType = ShipmentPriorityType.Normal)
        {
            Id = id;
            Cost = cost;
            OrderItemType = orderItemType;
            ShipmentPriorityType = shipmentPriorityType;
        }
    }
}
