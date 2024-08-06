using System.Reflection.Metadata;

namespace IWantApp.Endpoints.Orders;

public class OrderGetById
{
    public static string Template => "/order/{id}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    private static async Task<IResult> Action(Guid id, ApplicationDbContext context, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var clientClaim = http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        var employeeClaim = http.User.Claims.FirstOrDefault(c => c.Type == "EmployeeCode");

        // var order = await context.Orders.Where(order => order.Id == id).FirstOrDefaultAsync();
        var order = await context.Orders.Include(o => o.Products).Where(order => order.Id == id).FirstOrDefaultAsync();

        if (order.ClientId != clientClaim.Value && employeeClaim == null)
            return Results.Forbid();

        // var client = await context.Users.Where(u => u.Id == clientClaim.Value).FirstAsync();
        var client = await userManager.FindByIdAsync(order.ClientId);

        var productsResponse = order.Products.Select(p => new OrderProduct(p.Id, p.Name)).ToList();

        var orderResponse = new OrderResponse(order.Id, client.Email, productsResponse, order.Total, order.DeliveryAddress);

        return Results.Ok(orderResponse);
    }
}
