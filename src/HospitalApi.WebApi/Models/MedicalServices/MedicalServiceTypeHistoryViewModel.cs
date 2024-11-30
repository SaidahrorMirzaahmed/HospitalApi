using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.MedicalServices;

public class MedicalServiceTypeHistoryViewModel
{
    public int Queue { get; set; }

    public DateOnly QueueDate { get; set; }

    public long TicketId { get; set; }

    public long ClientId { get; set; }
    public UserViewModel Client { get; set; }

    public long MedicalServiceTypeId { get; set; }
    public MedicalServiceTypeViewModel MedicalServiceType { get; set; }
}