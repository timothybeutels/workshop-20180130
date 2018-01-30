namespace Workshop.Tests
{
    public class StartShoppingCommand
    {
        public System.String CustomerId { get; private set; }
        public string CartId { get; private set; }
        public NodaTime.LocalDateTime StartTime { get; private set; }
        public StartShoppingCommand(System.String customerid, string cartid, NodaTime.LocalDateTime starttime)
        {
            CustomerId = customerid;
            CartId = cartid;
            StartTime = starttime;
        }
    }

    public class AddProductToCartCommand
    {
        public System.String CartId { get; private set; }
        public System.String Sku { get; private set; }
        public System.Double Price { get; private set; }
        public NodaTime.LocalDateTime AddTime { get; private set; }
        public AddProductToCartCommand(System.String cartid, System.String sku, System.Double price, NodaTime.LocalDateTime addtime)
        {
            CartId = cartid;
            Sku = sku;
            Price = price;
            AddTime = addtime;
        }
    }

    public class RemoveProductFromCartCommand
    {
        public System.String CartId { get; private set; }
        public System.String Sku { get; private set; }
        public NodaTime.LocalDateTime AddTime { get; private set; }
        public RemoveProductFromCartCommand(System.String cartid, System.String sku, NodaTime.LocalDateTime addtime)
        {
            CartId = cartid;
            Sku = sku;
            AddTime = addtime;
        }
    }

    public class PlaceOrderCommand
    {
        public System.String CartId { get; private set; }
        public NodaTime.LocalDateTime OrderTime { get; private set; }
        public PlaceOrderCommand(System.String cartid, NodaTime.LocalDateTime ordertime)
        {
            CartId = cartid;
            OrderTime = ordertime;
        }
    }
    

    public class CustomerStartedShoppingEvent
    {
        public string CartId { get; }

        public CustomerStartedShoppingEvent(string cartId)
        {
            CartId = cartId;
        }
    }

    public class ProductWasAddedToCartEvent
    {
        public System.String CartId { get; private set; }
        public System.String Sku { get; private set; }
        public System.Double Price { get; private set; }
        public NodaTime.LocalDateTime AddedAt { get; private set; }
        public ProductWasAddedToCartEvent(System.String cartid, System.String sku, System.Double price, NodaTime.LocalDateTime addedat)
        {
            CartId = cartid;
            Sku = sku;
            Price = price;
            AddedAt = addedat;
        }
    }

    public class ProductWasRemovedFromCartEvent
    {
        public System.String CartId { get; private set; }
        public System.String Sku { get; private set; }
        public NodaTime.LocalDateTime RemovedAt { get; private set; }
        public ProductWasRemovedFromCartEvent(System.String cartid, System.String sku, NodaTime.LocalDateTime removedat)
        {
            CartId = cartid;
            Sku = sku;
            RemovedAt = removedat;
        }
    }

    public class CustomerPlacedOrderEvent
    {
        public System.String CartId { get; private set; }
        public System.String CustomerId { get; private set; }
        public ProductModel[] Products { get; private set; }
        public NodaTime.LocalDateTime OrderedAt { get; private set; }
        public CustomerPlacedOrderEvent(System.String cartid, System.String customerid, ProductModel[] products, NodaTime.LocalDateTime orderedat)
        {
            CartId = cartid;
            CustomerId = customerid;
            Products = products;
            OrderedAt = orderedat;
        }
    }

    public class CustomerAbandonedCartEvent
    {
        public System.String CartId { get; private set; }
        public CustomerAbandonedCartEvent(System.String cartid)
        {
            CartId = cartid;
        }
    }

    public class ProductModel
    {
        public System.String Sku { get; private set; }
        public System.Int32 Quantity { get; private set; }
        public System.Int32 PriceInCents { get; private set; }
        public System.String Currency { get; private set; }
        public ProductModel(System.String sku, System.Int32 quantity, System.Int32 priceincents, System.String currency)
        {
            Sku = sku;
            Quantity = quantity;
            PriceInCents = priceincents;
            Currency = currency;
        }
    }


}
