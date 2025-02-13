using HospitalApi.WebApi.Configurations;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace HospitalApi.Service.Services.Notifications;

public class SmsCodeSenderService : ICodeSenderService
{
    public async Task<long> SendCodeToPhone(string phoneNumber)
    {
        HttpClient client = new HttpClient();
        var code = GenerateCode();
        var message = CreateMessage(phoneNumber, code);

        var request = new HttpRequestMessage(HttpMethod.Post, $"");

        return await Task.FromResult(code);
    }

    private long GenerateCode() =>
        new Random().Next(100000, 999999);

    private string CreateMessage(string phoneNumber, long code) =>
        $"Phone number: {phoneNumber}, Code: {code}";
}