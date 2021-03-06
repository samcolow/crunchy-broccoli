using Fulfillment.Models;
using NUnit.Framework;
using PowerAssert;
using System;
using System.Linq;

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
            var item = OrderInputItem(id: "123", dimensionInCm: 10, orderItemType: OrderItemType.Parcel);
            var orderInput = OrderInput(item);
            var result = _service.Calculate(orderInput);

            PAssert.IsTrue(() => result != null);
        }

        [TestCase("SmallLow", -1, OrderItemType.Parcel, 3)]
        [TestCase("SmallLow", 0, OrderItemType.Parcel, 3)]
        [TestCase("SmallLow", 1, OrderItemType.Parcel, 3)]
        [TestCase("SmallHigh", 9, OrderItemType.Parcel, 3)]
        [TestCase("MediumLow", 10, OrderItemType.Parcel, 8)]
        [TestCase("MediumHigh", 49, OrderItemType.Parcel, 8)]
        [TestCase("LargeLow", 50, OrderItemType.Parcel, 15)]
        [TestCase("LargeHigh", 99, OrderItemType.Parcel, 15)]
        [TestCase("XLargeLow", 100, OrderItemType.Parcel, 25)]
        [TestCase("XLargeHigh", 1000, OrderItemType.Parcel, 25)]
        public void Calculate_GivenOrderItemWithDimension_ReturnsExpectedShipmentCost(string id, int dimensionInCm, OrderItemType type, int expectedCost)
        {
            var item = OrderInputItem(id: id, dimensionInCm: dimensionInCm, orderItemType: type);
            var orderInput = OrderInput(item);
            var result = _service.Calculate(orderInput);

            PAssert.IsTrue(() => result.Items[0].Cost == expectedCost);
        }

        [Test]
        public void Calculate_GivenMultipleOrderItem_ReturnsExpectedTotalCost()
        {
            var item1 = OrderInputItem(id: "Small", dimensionInCm: 1, orderItemType: OrderItemType.Parcel);
            var item2 = OrderInputItem(id: "Medium", dimensionInCm: 10, orderItemType: OrderItemType.Parcel);
            var item3 = OrderInputItem(id: "Large", dimensionInCm: 50, orderItemType: OrderItemType.Parcel);
            var orderInput = OrderInput(item1, item2, item3);
            var result = _service.Calculate(orderInput);

            var expectedTotalCost = ShipmentCostCalculatorService.SmallSizeCost + ShipmentCostCalculatorService.MediumSizeCost + ShipmentCostCalculatorService.LargeSizeCost;

            PAssert.IsTrue(() => result.TotalCost == expectedTotalCost);
        }

        [Test]
        public void Calculate_GivenSpeedyShippingSelected_ReturnsDoubledCostOfEntireOrder()
        {
            var item1 = OrderInputItem(id: "Small", dimensionInCm: 1, orderItemType: OrderItemType.Parcel);
            var item2 = OrderInputItem(id: "Medium", dimensionInCm: 10, orderItemType: OrderItemType.Parcel);
            var item3 = OrderInputItem(id: "Large", dimensionInCm: 50, orderItemType: OrderItemType.Parcel);
            var orderInput = OrderInputWithSpeedyShipping(item1, item2, item3);
            var result = _service.Calculate(orderInput);

            var expectedTotalCost = ShipmentCostCalculatorService.SmallSizeCost + ShipmentCostCalculatorService.MediumSizeCost + ShipmentCostCalculatorService.LargeSizeCost;
            var expectedSpeedyTotalCost = expectedTotalCost * 2;

            PAssert.IsTrue(() => result.TotalCost == expectedSpeedyTotalCost);
        }

        [Test]
        public void Calculate_GivenSpeedyShippingSelected_ReturnsResultWithSpeedyShippingItem()
        {
            var item1 = OrderInputItem(id: "Small", dimensionInCm: 1, orderItemType: OrderItemType.Parcel);
            var item2 = OrderInputItem(id: "Medium", dimensionInCm: 10, orderItemType: OrderItemType.Parcel);
            var item3 = OrderInputItem(id: "Large", dimensionInCm: 50, orderItemType: OrderItemType.Parcel);
            var orderInput = OrderInputWithSpeedyShipping(item1, item2, item3);
            var result = _service.Calculate(orderInput);

            PAssert.IsTrue(() => result.Items.Any(i => i.ShipmentPriorityType == ShipmentPriorityType.Speedy));
        }

        OrderInput OrderInput(params OrderInputItem[] item)
        {
            return new OrderInput(item.ToList());
        }

        OrderInput OrderInputWithSpeedyShipping(params OrderInputItem[] item)
        {
            return new OrderInput(item.ToList(), isSpeedyShipping: true);
        }

        OrderInputItem OrderInputItem(string id, int dimensionInCm, OrderItemType orderItemType)
        {
            return new OrderInputItem(id: id, dimensionInCm: dimensionInCm, orderItemType: orderItemType);
        }

        ShipmentCostItem SpeedyShipmentCostItem()
        {
            return new ShipmentCostItem(id: "", cost: 0, orderItemType: OrderItemType.NonItem, ShipmentPriorityType.Speedy);
        }
    }
}