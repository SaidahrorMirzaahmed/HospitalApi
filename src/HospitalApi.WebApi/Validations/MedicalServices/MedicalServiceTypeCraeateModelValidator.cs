using FluentValidation;
using HospitalApi.WebApi.Models.MedicalServices;

namespace HospitalApi.WebApi.Validations.MedicalServices;

public class MedicalServiceTypeCreateModelValidator : AbstractValidator<MedicalServiceTypeCreateModel>
{
    public MedicalServiceTypeCreateModelValidator()
    {
        RuleFor(b => b.ServiceTypeTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage($"{nameof(MedicalServiceTypeCreateModel)} cant be null or empty");

        RuleFor(entity => entity.ServiceTypeTitleRu)
            .NotEmpty()
            .NotNull()
            .WithMessage($"{nameof(MedicalServiceTypeCreateModel.ServiceTypeTitleRu)} cant be null or empty");

        RuleFor(entity => entity.StaffId)
            .GreaterThan(0)
            .WithMessage(a => $"{nameof(a.StaffId)} cant be null or 0");
    }
}