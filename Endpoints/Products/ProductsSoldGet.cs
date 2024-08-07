namespace IWantApp.Endpoints.Products;

/**
 * Desafio: trazer os produtos mais vendidos
 * Resposta deve trazer: id, nome e quantidade do produto.
 * Ordenado pelos mais vendidos.
 */
public class ProductsSoldGet
{
    public static string Template => "/products/sold";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(QueryAllProductsSold query)
    {
        var productsMostSold = await query.Execute();
        return Results.Ok(productsMostSold);
    }
}
