using HospitalApi.Service.Services.SettingsProvider;
using Telegram.Bot;

namespace HospitalApi.Service.Services.Notifications;

public class CodeSenderService : ICodeSenderService
{
    private readonly (string token, string chatId) _botSettings;
    private readonly TelegramBotClient _botClient;

    public CodeSenderService(IBotSettingsProvider settingsProvider)
    {
        _botSettings = new Lazy<Task<(string token, string chatId)>>(async () => await settingsProvider.GetBotSettingsAsync()).Value.Result;
        _botClient = new TelegramBotClient(_botSettings.token);
    }

    public async Task<long> SendCodeToPhone(string phoneNumber)
    {
        var code = GenerateCode();

        await _botClient
            .SendTextMessageAsync(_botSettings.chatId, CreateBotMessage(phoneNumber, code));

        return code;
    }

    private long GenerateCode() =>
        new Random().Next(100000, 999999);

    private string CreateBotMessage(string phoneNumber, long code) =>
        $"Phone number: {phoneNumber}, Code: {code}";
}