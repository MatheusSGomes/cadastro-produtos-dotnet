using IWantApp.Endpoints.Products;

namespace IWantApp.Infra.Data;

public class QueryAllProductsSold
{
    private readonly IConfiguration _configuration;

    public QueryAllProductsSold(IConfiguration _configuration)
    {
        this._configuration = _configuration;
    }

    public async Task<IEnumerable<ProductsSoldReportResponse>> Execute()
    {
        var db = new SqlConnection(_configuration["ConnectionStrings:IWantDb"]);

        var query = @"
                SELECT P.Id, P.Name, COUNT(OP.OrdersId) as QuantityOrdered
                  FROM Orders O
            INNER JOIN OrderProducts OP ON OP.OrdersId = O.Id
            INNER JOIN Products P ON P.Id = OP.ProductsId
              GROUP BY P.Id, P.Name
              ORDER BY QuantityOrdered DESC;
        ";

        return await db.QueryAsync<ProductsSoldReportResponse>(query);
    }
}
