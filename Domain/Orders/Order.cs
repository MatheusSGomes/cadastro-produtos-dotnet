namespace IWantApp.Domain.Orders;

public class Order: Entity
{
    public string ClientId { get; private set; }
    public List<Product> Products { get; private set; }
    public decimal Total { get; private set; }
    public string DeliveryAddress { get; private set; }
    
    private Order() {}

    public Order(string clientId, string clientName, List<Product> products, string deliveryAddress)
    {
        ClientId = clientId;
        Products = products;
        DeliveryAddress = deliveryAddress;

        CreatedBy = clientName;
        CreatedOn = DateTime.Now;
        EditedBy = clientName;
        EditedOn = DateTime.Now;

        Total = 0;

        foreach (var item in Products)
        {
            Total += item.Price;
        }

        Validate();
    }

    public void Validate()
    {
        var contract = new Contract<Order>()
            .IsNotNull(ClientId, "Client")
            .IsNotNull(Products, "Products")
            .IsNotNull(DeliveryAddress, "DeliveryAddress");

        AddNotifications(contract);
    }
}
