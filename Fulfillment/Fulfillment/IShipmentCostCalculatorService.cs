using Fulfillment.Models;

namespace Fulfillment
{
    public interface IShipmentCostCalculatorService
    {
        ShipmentCostResult Calculate(OrderInput input);
    }
}
