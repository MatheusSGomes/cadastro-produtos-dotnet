using Dapper;
using Microsoft.Data.SqlClient;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(IConfiguration configuration, int? page = 1, int? rows = 10)
    {
        var db = new SqlConnection(configuration["ConnectionStrings:IWantDb"]);

        var query = @"
                SELECT users.Id, claims.ClaimValue as Name, users.Email
                  FROM AspNetUsers users
            INNER JOIN AspNetUserClaims claims on users.Id = claims.UserId and claims.ClaimType = 'Name' ORDER BY Name
                OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY
        ";
        
        var employees = db.Query<EmployeeResponse>(query, new { page, rows});

        return Results.Ok(employees);
    }
}
