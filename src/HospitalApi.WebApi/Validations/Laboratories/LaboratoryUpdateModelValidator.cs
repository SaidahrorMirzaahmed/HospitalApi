using FluentValidation;
using HospitalApi.WebApi.Models.Laboratories;

namespace HospitalApi.WebApi.Validations.Laboratories;

public class LaboratoryUpdateModelValidator : AbstractValidator<LaboratoryUpdateModel>
{
    public LaboratoryUpdateModelValidator()
    {
        RuleFor(entity => entity.ClientId)
            .GreaterThan(0)
            .WithMessage(entity => $"{nameof(entity.ClientId)} cant be 0");
    }
}
