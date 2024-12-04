namespace HospitalApi.Service.Models;

public class TicketCreateDto
{
    public long MedicalServiceId { get; set; }

    public DateOnly BookingDate { get; set; }
}