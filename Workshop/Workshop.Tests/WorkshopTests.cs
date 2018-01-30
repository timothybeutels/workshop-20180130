using NodaTime;
using NUnit.Framework;

namespace Workshop.Tests
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
    }

    
}
