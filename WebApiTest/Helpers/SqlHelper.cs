using Microsoft.Data.SqlClient; 
using System.Data;
using System.Text;

namespace BackendExam.Api.Helpers;

public static class SqlHelper
{
    public static async Task<string> ExecuteStoredProcedureAsync(string connectionString, string spName, string json)
    {
        using var conn = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(spName, conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = json });

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        var result = new StringBuilder();
        while (await reader.ReadAsync())
        {
            result.Append(reader[0].ToString());
        }

        return result.ToString();
    }
}
