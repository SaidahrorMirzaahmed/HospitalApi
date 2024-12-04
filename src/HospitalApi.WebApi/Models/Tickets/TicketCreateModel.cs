namespace HospitalApi.WebApi.Models.Tickets;

public class TicketCreateModel
{
    public long MedicalServiceId { get; set; }
    
    public DateOnly BookingDate { get; set; }
}