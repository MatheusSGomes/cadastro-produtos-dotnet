namespace IWantApp.Endpoints.Orders;

public record OrderResponse(Guid Id, string ClientId, decimal Total, string DeliveryAddress);
