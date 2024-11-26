namespace HospitalApi.WebApi.Models.MedicalServices;

public class MedicalServiceTypeUpdateModel
{
    public string ServiceType { get; set; }

    public double Price { get; set; }

    public long StaffId { get; set; }
}