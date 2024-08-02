namespace IWantApp.Endpoints.Products;

public class ProductGetShowcase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(int? page, int? row, string? orderBy, ApplicationDbContext context)
    {
        if (page == null)
            page = 1;

        if (row == null)
            row = 10;

        if (string.IsNullOrEmpty(orderBy))
            orderBy = "name";

        var queryBase = context.Products.Include(product => product.Category)
            .Where(product => product.HasStock && product.Category.Active);

        if (orderBy == "name")
            queryBase.OrderBy(product => product.Name);
        else
            queryBase.OrderBy(product => product.Price);

        var queryFilter = queryBase.Skip((page.Value - 1) * row.Value).Take(row.Value);

        var products = queryFilter.ToList();

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
