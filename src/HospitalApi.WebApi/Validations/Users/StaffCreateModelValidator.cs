using FluentValidation;
using HospitalApi.WebApi.Models.Users;
using HospitalApi.Service.Helpers;

namespace HospitalApi.WebApi.Validations.Users;

public class StaffCreateModelValidator : AbstractValidator<StaffCreateModel>
{
    public StaffCreateModelValidator()
    {
        RuleFor(user => user.FirstName)
            .NotNull()
            .WithMessage(user => $"{nameof(user.FirstName)} is not specified");

        RuleFor(user => user.FirstName)
            .NotNull()
            .WithMessage(user => $"{nameof(user.LastName)} is not specified");

        RuleFor(user => user.Phone)
            .NotNull()
            .WithMessage(user => $"{nameof(user.Phone)} is not specified");

        RuleFor(user => user.MedicalSpecialists)
            .NotNull()
            .WithMessage(user => $"{nameof(user.MedicalSpecialists)} is not specified");

        RuleFor(user => user.Phone)
            .Must(ValidationHelper.IsPhoneValid);
    }
}