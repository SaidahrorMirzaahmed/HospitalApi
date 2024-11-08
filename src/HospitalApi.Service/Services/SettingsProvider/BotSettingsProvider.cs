
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HospitalApi.Service.Services.SettingsProvider;

public class BotSettingsProvider(IConfiguration configuration) : IBotSettingsProvider
{
    public async Task<(string token, string chatId)> GetBotSettingsAsync()
    {
        var dbConnectionString = configuration.GetConnectionString("DefaultDbConnection");

        using(var connection = new SqlConnection(dbConnectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("SELECT Token, ChatId FROM BotSettings WHERE Name = 'CodeSenderBot'", connection);
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    var token = reader["Token"].ToString();
                    var chatId = reader["ChatId"].ToString();

                    if (token.IsNullOrEmpty() || chatId.IsNullOrEmpty())
                        throw new InvalidOperationException("Bot settings not found");

                    return (token, chatId);
                }
            }
        }

        throw new InvalidOperationException("Bot settings not found in the configuration database.");
    }
}