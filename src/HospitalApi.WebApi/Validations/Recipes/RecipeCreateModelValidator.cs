using FluentValidation;
using HospitalApi.WebApi.Models.Recipes;

namespace HospitalApi.WebApi.Validations.Recipes;

public class RecipeCreateModelValidator : AbstractValidator<RecipeCreateModel>
{
    public RecipeCreateModelValidator()
    {
        RuleFor(b => b.ClientId)
            .GreaterThan(0)
            .WithMessage(a => $"{nameof(a.ClientId)} cant be null or 0");
    }
}