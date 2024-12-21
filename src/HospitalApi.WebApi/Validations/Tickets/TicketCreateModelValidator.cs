using FluentValidation;
using HospitalApi.WebApi.Models.Tickets;

namespace HospitalApi.WebApi.Validations.Tickets;

public class TicketCreateModelValidator : AbstractValidator<TicketCreateModel>
{
    public TicketCreateModelValidator()
    {
        RuleFor(entity => entity.MedicalServiceId)
            .GreaterThan(0)
            .WithMessage(entity => $"{nameof(entity.MedicalServiceId)} cant be 0");

        RuleFor(entity => entity.BookingDate)
            .NotNull()
            .WithMessage(entity => $"{nameof(entity.BookingDate)} cant be null or empty");
    }
}