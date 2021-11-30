using Fulfillment.Models;
using System.Collections.Generic;

namespace Fulfillment
{
    public class ShipmentCostCalculatorService : IShipmentCostCalculatorService
    {
        public ShipmentCostResult Calculate(OrderInput input)
        {
            var shippingCostItem = new ShipmentCostItem("", 1, "");
            
            var items = new List<ShipmentCostItem>();
            items.Add(shippingCostItem);

            return new ShipmentCostResult(items);
        }
    }
}
