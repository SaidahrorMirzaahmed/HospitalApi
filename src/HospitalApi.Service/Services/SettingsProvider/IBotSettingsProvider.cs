namespace HospitalApi.Service.Services.SettingsProvider;

public interface IBotSettingsProvider
{
    Task<(string token, string chatId)> GetBotSettingsAsync();
}