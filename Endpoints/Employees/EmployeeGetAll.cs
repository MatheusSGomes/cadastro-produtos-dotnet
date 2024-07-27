using Dapper;
using Microsoft.Data.SqlClient;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int? page, int? rows, IConfiguration configuration)
    {
        var db = new SqlConnection(configuration["ConnectionStrings:IWantDb"]);
        var employees = db.Query<EmployeeResponse>(@"
                select users.Id, claims.ClaimValue as Name, users.Email
                  from AspNetUsers users
            inner join AspNetUserClaims claims on users.Id = claims.UserId and claims.ClaimType = 'Name'
              order by Name
        ");

        return Results.Ok(employees);
    }
}
