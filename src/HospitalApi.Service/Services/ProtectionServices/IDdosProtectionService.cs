namespace HospitalApi.Service.Services.ProtectionServices;

public interface IDdosProtectionService
{
    Task<bool> IsRequestAllowedAsync(string ipAddress);
}