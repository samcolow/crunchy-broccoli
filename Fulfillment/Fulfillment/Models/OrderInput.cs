using System.Collections.Generic;

namespace Fulfillment.Models
{
    public enum OrderItemType
    {
        Unknown = 0,
        Parcel = 1
    }
    public enum ItemSize
    {
        Unknown = 0,
        Small = 1,
        Medium = 2,
        Large = 3,
        XLarge = 4
    }

    public class OrderInput
    {
        public List<OrderInputItem> Items { get; }

        public OrderInput(List<OrderInputItem> items)
        {
            Items = items;
        }
    }

    public class OrderInputItem
    {
        public string Id { get; }
        public int DimensionInCm { get; }
        public OrderItemType Type { get; }
        public ItemSize Size { get; }

        public OrderInputItem(string id, int dimensionInCm, OrderItemType type)
        {
            Id = id;
            DimensionInCm = dimensionInCm;
            Type = type;
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
