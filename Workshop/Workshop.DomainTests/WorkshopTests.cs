using System;
using NodaTime;
using NUnit.Framework;

namespace Workshop.DomainTests
{
    public class WorkshopTests
    {
        [Test]
        public void CustomerStartedShopping_when_StartShopping()
        {
            var cartId = "B21FF5DE-8568-483E-BFC5-77B4BFF19E90";
            var customerId = "C9F9DFC0-3FDE-4C54-A729-5E82E0EB4D4D";
            var startTime = new LocalDateTime(2018, 1, 30, 14, 23);

            new WorkshopTestFixture()
                .When(new StartShoppingCommand(customerId, cartId, startTime))
                .Then(new CustomerStartedShoppingEvent(cartId));
            
        }

        [Test]
        public void NotCustomerStartedShopping_when_StartShoppingAndAlreadyStartedShopping()
        {
            var cartId = "25E4E7DC-C968-420A-AD27-B9A9999F43CF";
            var customerId = "C9F9DFC0-3FDE-4C54-A729-5E82E0EB4D4D";
            var startTime = new LocalDateTime(2018, 1, 30, 14, 23);

            new WorkshopTestFixture()
                .Given(new CustomerStartedShoppingEvent(cartId))
                .When(new StartShoppingCommand(customerId, cartId, startTime))
                .ThenNothing();
        }

        [Test]
        public void RemoveProduct_ProductAdded_ProductRemoved()
        {
            var cartId = "25E4E7DC-C968-420A-AD27-B9A9999F43CF";
            var customerId = "C9F9DFC0-3FDE-4C54-A729-5E82E0EB4D4D";
            var startTime = new LocalDateTime(2018, 1, 30, 14, 23);

            new WorkshopTestFixture()
                .Given(new CustomerStartedShoppingEvent(cartId))
                .Given(new ProductWasAddedToCartEvent(cartId, "my-sku", 2d, startTime))
                .When(new RemoveProductFromCartCommand(cartId, "my-sku", startTime))
                .Then(new ProductWasRemovedFromCartEvent(cartId, "my-sku", startTime));
        }

        [Test]
        public void RemoveProduct_ProductNotAdded_NothingHappens()
        {
            var cartId = "25E4E7DC-C968-420A-AD27-B9A9999F43CF";
            var customerId = "C9F9DFC0-3FDE-4C54-A729-5E82E0EB4D4D";
            var startTime = new LocalDateTime(2018, 1, 30, 14, 23);

            new WorkshopTestFixture()
                .Given(new CustomerStartedShoppingEvent(cartId))
                .When(new RemoveProductFromCartCommand(cartId, "my-sku", startTime))
                .ThenNothing();
        }

        [Test]
        public void RemoveProduct_ProductNotAddedButOtherIs_NothingHappens()
        {
            var cartId = "25E4E7DC-C968-420A-AD27-B9A9999F43CF";
            var customerId = "C9F9DFC0-3FDE-4C54-A729-5E82E0EB4D4D";
            var startTime = new LocalDateTime(2018, 1, 30, 14, 23);

            new WorkshopTestFixture()
                .Given(new CustomerStartedShoppingEvent(cartId))
                .Given(new ProductWasAddedToCartEvent(cartId, "my-other-sku", 2d, startTime))
                .When(new RemoveProductFromCartCommand(cartId, "my-sku", startTime))
                .ThenNothing();
        }

        [Test]
        public void RemoveProduct_ShoppingNotStarted_NothingHappens()
        {
            var cartId = "25E4E7DC-C968-420A-AD27-B9A9999F43CF";
            var customerId = "C9F9DFC0-3FDE-4C54-A729-5E82E0EB4D4D";
            var startTime = new LocalDateTime(2018, 1, 30, 14, 23);

            new WorkshopTestFixture()
                .When(new RemoveProductFromCartCommand(cartId, "my-sku", startTime))
                .ThenNothing();
        }

        [Test]
        public void PlaceOrder_ProductsAdded_OrderPlaced()
        {
            var cartId = "25E4E7DC-C968-420A-AD27-B9A9999F43CF";
            var customerId = "C9F9DFC0-3FDE-4C54-A729-5E82E0EB4D4D";
            var startTime = new LocalDateTime(2018, 1, 30, 14, 23);

            new WorkshopTestFixture()
                .Given(new CustomerStartedShoppingEvent(cartId))
                .Given(new ProductWasAddedToCartEvent(cartId, "my-sku", 2d, startTime))
                .When(new PlaceOrderCommand(cartId, startTime))
                .Then(new CustomerPlacedOrderEvent(cartId, customerId, new ProductModel[]
                {
                    new ProductModel("my-sku", 1, Convert.ToInt32(2d * 100), "€"), 
                }, startTime));
        }

        [Test]
        public void PlaceOrder_SameProductMultipleTimesAdded_OrderPlaced()
        {
            var cartId = "25E4E7DC-C968-420A-AD27-B9A9999F43CF";
            var customerId = "C9F9DFC0-3FDE-4C54-A729-5E82E0EB4D4D";
            var startTime = new LocalDateTime(2018, 1, 30, 14, 23);

            new WorkshopTestFixture()
                .Given(new CustomerStartedShoppingEvent(cartId))
                .Given(new ProductWasAddedToCartEvent(cartId, "my-sku", 2d, startTime))
                .Given(new ProductWasAddedToCartEvent(cartId, "my-sku", 2d, startTime))
                .When(new PlaceOrderCommand(cartId, startTime))
                .Then(new CustomerPlacedOrderEvent(cartId, customerId, new ProductModel[]
                {
                    new ProductModel("my-sku", 2, Convert.ToInt32(2d * 100), "€"),
                }, startTime));
        }

        [Test]
        public void PlaceOrder_MultipleProductsAdded_OrderPlaced()
        {
            var cartId = "25E4E7DC-C968-420A-AD27-B9A9999F43CF";
            var customerId = "C9F9DFC0-3FDE-4C54-A729-5E82E0EB4D4D";
            var startTime = new LocalDateTime(2018, 1, 30, 14, 23);

            new WorkshopTestFixture()
                .Given(new CustomerStartedShoppingEvent(cartId))
                .Given(new ProductWasAddedToCartEvent(cartId, "my-sku", 2d, startTime))
                .Given(new ProductWasAddedToCartEvent(cartId, "my-sku-2", 15d, startTime))
                .When(new PlaceOrderCommand(cartId, startTime))
                .Then(new CustomerPlacedOrderEvent(cartId, customerId, new ProductModel[]
                {
                    new ProductModel("my-sku", 1, Convert.ToInt32(2d * 100), "€"),
                    new ProductModel("my-sku-2", 1, Convert.ToInt32(15d * 100), "€"),
                }, startTime));
        }

        [Test]
        public void PlaceOrder_NoProductsAdded_NothingHappens()
        {
            var cartId = "25E4E7DC-C968-420A-AD27-B9A9999F43CF";
            var customerId = "C9F9DFC0-3FDE-4C54-A729-5E82E0EB4D4D";
            var startTime = new LocalDateTime(2018, 1, 30, 14, 23);

            new WorkshopTestFixture()
                .Given(new CustomerStartedShoppingEvent(cartId))
                .When(new PlaceOrderCommand(cartId, startTime))
                .ThenNothing();
        }

        [Test]
        public void PlaceOrder_NotStarted_NothingHappens()
        {
            var cartId = "25E4E7DC-C968-420A-AD27-B9A9999F43CF";
            var customerId = "C9F9DFC0-3FDE-4C54-A729-5E82E0EB4D4D";
            var startTime = new LocalDateTime(2018, 1, 30, 14, 23);

            new WorkshopTestFixture()
                .When(new PlaceOrderCommand(cartId, startTime))
                .ThenNothing();
        }
    }

    
}
