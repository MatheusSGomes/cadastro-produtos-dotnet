namespace IWantApp.Endpoints.Orders;

public record OrderResponse(Guid Id, string ClientEmail, List<OrderProduct> Products, decimal Total, string DeliveryAddress);
