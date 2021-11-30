using Fulfillment.Models;
using NUnit.Framework;
using PowerAssert;
using System.Collections.Generic;

namespace Fulfillment.Tests
{
    public class ShipmentCostCalculatorServiceTests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        ShipmentCostCalculatorService _service;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void Setup()
        {
            _service = new ShipmentCostCalculatorService();
        }

        [Test]
        public void Calculate_GivenOrderInput_ReturnsShipmentCostResult()
        {
            var orderInput = OrderInput();
            var result = _service.Calculate(orderInput);
            PAssert.IsTrue(() => result != null);
        }

        OrderInput OrderInput()
        {
            var orderInputItem = new OrderInputItem("123");
            return new OrderInput(new List<OrderInputItem> { orderInputItem });
        }
    }
}