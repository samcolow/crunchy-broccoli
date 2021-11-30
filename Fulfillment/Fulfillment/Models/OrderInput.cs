using System.Collections.Generic;

namespace Fulfillment.Models
{
    public class OrderInput
    {
        public List<OrderInputItem> Items { get; private set; }

        public OrderInput(List<OrderInputItem> items)
        {
            Items = items;
        }
    }

    public class OrderInputItem
    {
        public string Id { get; private set; }

        public OrderInputItem(string id)
        {
            Id = id;
        }
    }
}
