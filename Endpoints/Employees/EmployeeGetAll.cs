using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    // [Authorize(Policy = "Employee008Policy")]
    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(QueryAllUsersWithClaimName query, int? page, int? rows)
    {
        if (page == null)
            return Results.BadRequest("O parâmetro page não pode ser nulo");

        if (rows == null)
            rows = 10;

        var allUsers = query.Execute(page.Value, rows.Value);
        return Results.Ok(allUsers);
    }
}
