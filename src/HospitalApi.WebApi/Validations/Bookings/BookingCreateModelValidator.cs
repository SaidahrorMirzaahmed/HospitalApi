using FluentValidation;
using HospitalApi.WebApi.Models.Bookings;

namespace HospitalApi.WebApi.Validations.Bookings;

public class BookingCreateModelValidator : AbstractValidator<BookingCreateModel>
{
    public BookingCreateModelValidator()
    {
        RuleFor(b => b.StaffId).NotNull().NotEqual(0)
            .WithMessage(a => $"{nameof(a.StaffId)} cant be null or 0");
        RuleFor(b => b.ClientId).NotNull().NotEqual(0)
            .WithMessage(a => $"{nameof(a.StaffId)} cant be null or 0");
        RuleFor(b => b.Time).NotNull();
    }
}