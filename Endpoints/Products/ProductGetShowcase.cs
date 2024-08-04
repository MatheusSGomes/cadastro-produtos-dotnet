namespace IWantApp.Endpoints.Products;

public class ProductGetShowcase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ApplicationDbContext context, int page = 1, int row = 10, string orderBy = "name")
    {
        if (row > 10)
            return Results.Problem(title: "Row with max 10", statusCode: 400);
        
        var queryBase = context.Products
            .AsNoTracking()
            .Include(product => product.Category)
            .Where(product => product.HasStock && product.Category.Active);

        if (orderBy == "name")
            queryBase.OrderBy(product => product.Name);
        else if (orderBy == "price")
            queryBase.OrderBy(product => product.Price);
        else
            return Results.Problem(title: "OrderBy only by price or name", statusCode: 400);

        var queryFilter = queryBase.Skip((page - 1) * row).Take(row);

        var products = queryFilter.ToList();

        var results = products.Select(product => new ProductResponse(
            product.Id,
            product.Name,
            product.Category.Name,
            product.Description,
            product.HasStock,
            product.Price,
            product.Active));

        return Results.Ok(results);
    }
}
