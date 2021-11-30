using System;
using System.Collections.Generic;

namespace Fulfillment.Models
{
    public enum OrderItemType
    {
        Unknown = 0,
        Parcel = 1,
        NonItem = 2
    }

    public enum ItemSize
    {
        Unknown = 0,
        Small = 1,
        Medium = 2,
        Large = 3,
        XLarge = 4,
        Heavy = 5
    }

    public enum ShipmentPriorityType
    {
        Unknown = 0,
        Normal = 1,
        Speedy = 2,
    }

    public class OrderInput
    {
        public List<OrderInputItem> Items { get; }
        public bool IsSpeedyShipping { get; }

        public OrderInput(List<OrderInputItem> items, bool isSpeedyShipping = false)
        {
            Items = items;
            IsSpeedyShipping = isSpeedyShipping;
        }
    }

    public class OrderInputItem
    {
        public string Id { get; }
        public int DimensionInCm { get; }
        public OrderItemType OrderItemType { get; }
        public ItemSize Size { get; }
        public int WeightLimitInKg
        {
            get
            {
                return Size switch
                {
                    ItemSize.Small => 1,
                    ItemSize.Medium => 3,
                    ItemSize.Large => 6,
                    ItemSize.XLarge => 10,
                    _ => throw new NotSupportedException(),
                };
            }
        }

        public OrderInputItem(string id, int dimensionInCm, OrderItemType orderItemType)
        {
            Id = id;
            DimensionInCm = dimensionInCm;
            OrderItemType = orderItemType;
            Size = ConvertToSize(dimensionInCm);
        }

        ItemSize ConvertToSize(int dimensionInCm)
        {
            if (dimensionInCm < 10)
            {
                return ItemSize.Small;
            }
            else if (dimensionInCm < 50)
            {
                return ItemSize.Medium;
            }
            else if (dimensionInCm < 100)
            {
                return ItemSize.Large;
            }
            else
            {
                return ItemSize.XLarge;
            }
        }
    }
}
