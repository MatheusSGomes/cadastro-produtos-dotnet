using System.Reflection.Metadata;

namespace IWantApp.Endpoints.Orders;

public class OrderGetById
{
    public static string Template => "/order/{id}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    private static async Task<IResult> Action(Guid id, ApplicationDbContext context, HttpContext http)
    {
        var userId = http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var order = await context.Orders.Where(order => order.Id == id).FirstOrDefaultAsync();

        var isNotClient = order.ClientId != userId;
        var isNotEmployee = http.User.Claims.FirstOrDefault(c => c.Type == "EmployeeCode").Value != null;

        if (isNotClient || isNotEmployee)
            return Results.BadRequest("Apenas cliente pode ter acesso ao seu pedido");

        var orderResponse = new OrderResponse(order.Id, order.ClientId, order.Total, order.DeliveryAddress);
        return Results.Ok(orderResponse);
    }
}
