using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(UserManager<IdentityUser> userManager, int page, int rows = 10)
    {
        var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();
        var employees = new List<EmployeeResponse>();
        
        foreach (var user in users)
        {
            var claims = userManager.GetClaimsAsync(user).Result;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name");
            var userName = (claimName != null) ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse(Id: user.Id, Name: userName, Email: user.Email));
        }

        return Results.Ok(employees);
    }
}
