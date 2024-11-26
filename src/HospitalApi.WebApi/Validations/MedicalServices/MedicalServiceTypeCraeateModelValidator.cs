using FluentValidation;
using HospitalApi.WebApi.Models.MedicalServices;

namespace HospitalApi.WebApi.Validations.MedicalServices;

public class MedicalServiceTypeCreateModelValidator : AbstractValidator<MedicalServiceTypeCreateModel>
{
    public MedicalServiceTypeCreateModelValidator()
    {
        RuleFor(b => b.ServiceType)
            .NotEmpty()
            .NotEmpty()
            .WithMessage($"{nameof(MedicalServiceTypeCreateModel)} cant be null or empty");
    }
}