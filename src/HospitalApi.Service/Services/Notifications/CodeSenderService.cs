using Telegram.Bot;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Notifications;

public class CodeSenderService : ICodeSenderService
{
    private readonly TelegramBotClient _botClient;

    public CodeSenderService()
    {
        _botClient = new TelegramBotClient(EnvironmentHelper.CodeSenderBotToken);
    }

    public async Task<long> SendCodeToPhone(string phoneNumber)
    {
        var code = GenerateCode();

        await _botClient
            .SendTextMessageAsync(EnvironmentHelper.CodeSenderBotReceiverChatId,
            CreateBotMessage(phoneNumber, code));

        return code;
    }

    private long GenerateCode() =>
        new Random().Next(100000, 999999);

    private string CreateBotMessage(string phoneNumber, long code) =>
        $"Phone number: {phoneNumber}, Code: {code}";
}