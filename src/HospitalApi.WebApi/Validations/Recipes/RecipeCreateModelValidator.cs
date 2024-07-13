using FluentValidation;
using HospitalApi.WebApi.Models.Recipes;

namespace HospitalApi.WebApi.Validations.Recipes;

public class RecipeCreateModelValidator : AbstractValidator<RecipeCreateModel>
{
    public RecipeCreateModelValidator()
    {
        
        RuleFor(b => b.ClientId).NotNull().NotEqual(0)
            .WithMessage(a => $"{nameof(a.ClientId)} cant be null or 0");
        RuleFor(b => b.Picture).NotNull()
            .WithMessage(a => $"{nameof(a.Picture)} cant be null");
    }
}
