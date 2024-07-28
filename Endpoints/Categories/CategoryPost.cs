using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category(categoryRequest.Name, "UsuarioTeste", "UsuarioTeste");

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        context.Categories.Add(category);
        context.SaveChanges();
        
        return Results.Created($"/categories/{category.Id}", category);
    }
}
