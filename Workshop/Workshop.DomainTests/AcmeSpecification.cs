using System;
using NodaTime;
using NUnit.Framework;

namespace Workshop.DomainTests
{
    public class AcmeSpecification
    {
        [Test]
        public void Generate()
        {
            var model = new DomainSpecification()
                .Command("StartShopping", new
                {
                    CustomerId = "C1AB47AB-C2CC-4ED4-AD7A-4EF5E6865F1D",
                    CartId = "2533DAD1-C661-4891-8A11-99E39F41916A",
                    StartTime = new LocalDateTime(2018, 1, 30, 12, 09)

                })
                .Command("AddProductToCart", new
                {
                    CartId = "2533DAD1-C661-4891-8A11-99E39F41916A",
                    Sku = "some-product",
                    Price = 2d,
                    AddTime = new LocalDateTime(2018, 1, 30, 12, 11)
                })
                .Command("RemoveProductFromCart", new
                {
                    CartId = "2533DAD1-C661-4891-8A11-99E39F41916A",
                    Sku = "some-product",
                    AddTime = new LocalDateTime(2018, 1, 30, 12, 11)
                })
                .Command("PlaceOrder", new
                {
                    CartId = "2533DAD1-C661-4891-8A11-99E39F41916A",
                    OrderTime = new LocalDateTime(2018, 1, 30, 12, 33)
                })
                .Event("CustomerStartedShopping", new
                {
                    CartId = "2533DAD1-C661-4891-8A11-99E39F41916A",
                })
                .Event("ProductWasAddedToCart", new
                {
                    CartId = "2533DAD1-C661-4891-8A11-99E39F41916A",
                    Sku = "some-product",
                    Price = 2d,
                    AddedAt = new LocalDateTime(2018, 1, 30, 12, 11)
                })
                .Event("ProductWasRemovedFromCart", new
                {
                    CartId = "2533DAD1-C661-4891-8A11-99E39F41916A",
                    Sku = "some-product",
                    RemovedAt = new LocalDateTime(2018, 1, 30, 12, 11)
                })
                .Event("CustomerPlacedOrder", new 
                {
                    CartId = "2533DAD1-C661-4891-8A11-99E39F41916A",
                    CustomerId = "C1AB47AB-C2CC-4ED4-AD7A-4EF5E6865F1D",
                    Products = new []
                    {
                        new
                        {
                            Sku = "some-product",
                            Quantity = 5,
                            PriceInCents = 200,
                            Currency = "€"
                        }
                    },
                    OrderedAt = new LocalDateTime(2018, 1, 30, 12, 35)
                })
                .Event("CustomerAbandonedCart", new
                {
                    CartId = "2533DAD1-C661-4891-8A11-99E39F41916A",
                })
                .Build();

            foreach (var m in model)
            {
                //Console.WriteLine($"----- File: {m.Key} -----");
                Console.WriteLine(m.Value);
            }
        }
    }

}
