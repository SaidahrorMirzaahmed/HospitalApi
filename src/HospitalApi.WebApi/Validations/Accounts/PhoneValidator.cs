using FluentValidation;

namespace HospitalApi.WebApi.Validations.Accounts;

public class PhoneValidator : AbstractValidator<string>
{
    public PhoneValidator()
    {
        RuleFor(x => x);
    }
}