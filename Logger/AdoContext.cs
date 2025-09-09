using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Abstractions;
using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace Logger
{
    public class AdoContext
    {
        private SqlDataAdapter DataAdapter { get; set; }
        private readonly IConfiguration _configuration;
        private readonly SqlConnection connection;
        public AdoContext(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            DataAdapter = new SqlDataAdapter();
        }
        private SqlConnection connectionState()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            return connection;
        }
        public async Task InsertLogs(Logs logEntry)
        {
            try
            {
                const string insertQuery = @"
        INSERT INTO Logs 
        (LogId, Controller, ActionName, RequestBody, QueryString, IsAjax, IsFormPost, StartTime, EndTime, RequestDurationMs, IsException, ExceptionDetails, ResponseBody, HttpMethod, IpAddress, StatusCode, RequestHeaders, ResponseHeaders) 
        VALUES 
        (@LogId, @Controller, @ActionName, @RequestBody, @QueryString, @IsAjax, @IsFormPost, @StartTime, @EndTime, @RequestDurationMs, @IsException, @ExceptionDetails, @ResponseBody, @HttpMethod, @IpAddress, @StatusCode, @RequestHeaders, @ResponseHeaders)";

                using var connection = connectionState();
                using var command = new SqlCommand(insertQuery, connection);

                command.Parameters.AddWithValue("@LogId", logEntry.LogId);
                command.Parameters.AddWithValue("@Controller", logEntry.Controller ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ActionName", logEntry.ActionName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RequestBody", logEntry.RequestBody ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@QueryString", logEntry.QueryString ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IsAjax", logEntry.IsAjax);
                command.Parameters.AddWithValue("@IsFormPost", logEntry.IsFormPost);
                command.Parameters.AddWithValue("@StartTime", logEntry.StartTime);
                command.Parameters.AddWithValue("@EndTime", logEntry.EndTime);
                command.Parameters.AddWithValue("@RequestDurationMs", logEntry.RequestDurationMs);
                command.Parameters.AddWithValue("@IsException", logEntry.IsException);
                command.Parameters.AddWithValue("@ExceptionDetails", logEntry.ExceptionDetails ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ResponseBody", logEntry.ResponseBody ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@HttpMethod", logEntry.HttpMethod ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IpAddress", logEntry.IpAddress ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@StatusCode", logEntry.StatusCode);
                command.Parameters.AddWithValue("@RequestHeaders", logEntry.RequestHeaders ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ResponseHeaders", logEntry.ResponseHeaders ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
