using System.Reflection.Metadata;

namespace IWantApp.Endpoints.Orders;

public class OrderGetById
{
    public static string Template => "/order/{id}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    private static async Task<IResult> Action(string id)
    {
        return Results.Ok("Deu certo: " + id);
    }
}
