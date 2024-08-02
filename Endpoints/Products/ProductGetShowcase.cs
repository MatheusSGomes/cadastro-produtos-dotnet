namespace IWantApp.Endpoints.Products;

public class ProductGetShowcase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ApplicationDbContext context)
    {
        var products = context.Products.Include(product => product.Category)
            .Where(product => product.HasStock && product.Category.Active)
            .OrderBy(product => product.Name)
            .ToList();

        var results = products.Select(product => new ProductResponse(
            product.Name,
            product.Category.Name,
            product.Description,
            product.HasStock,
            product.Price,
            product.Active));

        return Results.Ok(results);
    }
}
