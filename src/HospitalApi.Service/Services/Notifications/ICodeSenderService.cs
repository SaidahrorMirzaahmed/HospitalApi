namespace Tenge.Service.Services.Notifications;

public interface ICodeSenderService
{
    Task<long> SendCodeToPhone(string phoneNumber);
}