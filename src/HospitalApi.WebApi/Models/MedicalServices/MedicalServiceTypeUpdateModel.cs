namespace HospitalApi.WebApi.Models.MedicalServices;

public class MedicalServiceTypeUpdateModel
{
    public string ServiceTypeTitle { get; set; }
    public string ServiceTypeTitleRu { get; set; }

    public double Price { get; set; }

    public long StaffId { get; set; }
}