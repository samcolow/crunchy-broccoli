using Fulfillment.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fulfillment
{
    public class ShipmentCostCalculatorService : IShipmentCostCalculatorService
    {
        public const int SmallSizeCost = 3;
        public const int MediumSizeCost = 8;
        public const int LargeSizeCost = 15;
        public const int XLargeSizeCost = 25;

        public ShipmentCostResult Calculate(OrderInput input)
        {
            var items = ProcessInputItems(input).ToList();

            return new ShipmentCostResult(items);
        }

        IEnumerable<ShipmentCostItem> ProcessInputItems(OrderInput input)
        {
            foreach (var item in input.Items)
            {
                var shippingCostItem = new ShipmentCostItem(
                    id: item.Id,
                    cost: GetCostBySize(item.Size),
                    orderItemType: item.OrderItemType);

                yield return shippingCostItem;
            }

            if (input.IsSpeedyShipping)
            {
                var priorityLineItem = new ShipmentCostItem(
                    id: "ShipmentPriorityType",
                    cost: 0,
                    orderItemType: OrderItemType.NonItem,
                    shipmentPriorityType: ShipmentPriorityType.Speedy);

                yield return priorityLineItem;
            }
        }

        static int GetCostBySize(ItemSize size)
        {
            return size switch
            {
                ItemSize.Small => SmallSizeCost,
                ItemSize.Medium => MediumSizeCost,
                ItemSize.Large => LargeSizeCost,
                ItemSize.XLarge => XLargeSizeCost,
                _ => throw new NotSupportedException(),
            };
        }
    }
}
