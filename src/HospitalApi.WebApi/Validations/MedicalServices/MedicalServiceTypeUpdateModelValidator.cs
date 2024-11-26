using FluentValidation;
using HospitalApi.WebApi.Models.MedicalServices;

namespace HospitalApi.WebApi.Validations.MedicalServices;

public class MedicalServiceTypeUpdateModelValidator : AbstractValidator<MedicalServiceTypeUpdateModel>
{
    public MedicalServiceTypeUpdateModelValidator()
    {
        RuleFor(b => b.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage($"{nameof(MedicalServiceTypeUpdateModelValidator)}  cant be null or empty");
    }
}