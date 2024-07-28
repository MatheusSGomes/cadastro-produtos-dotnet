using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration _configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    {
        var db = new SqlConnection(_configuration["ConnectionStrings:IWantDb"]);

        var query = @"
                SELECT users.Id, claims.ClaimValue as Name, users.Email
                  FROM AspNetUsers users
            INNER JOIN AspNetUserClaims claims on users.Id = claims.UserId and claims.ClaimType = 'Name' ORDER BY Name
                OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY
        ";

        return db.Query<EmployeeResponse>(query, new { page, rows});
    }
}
