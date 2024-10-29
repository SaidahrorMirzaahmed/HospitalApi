using FluentValidation;
using HospitalApi.Service.Helpers;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Validations.Users;

public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
{
    public UserUpdateModelValidator()
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

        RuleFor(user => user.Phone)
            .Must(ValidationHelper.IsPhoneValid);
    }
}