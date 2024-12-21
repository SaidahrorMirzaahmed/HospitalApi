using FluentValidation;
using HospitalApi.WebApi.Models.News;

namespace HospitalApi.WebApi.Validations.News;

public class NewsListUpdateModelValidator : AbstractValidator<NewsListUpdateModel>
{
    public NewsListUpdateModelValidator()
    {
        RuleFor(entity => entity.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage($"{nameof(NewsListCreateModel.Title)} cant be null or empty");

        RuleFor(entity => entity.SubTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage($"{nameof(NewsListCreateModel.SubTitle)} cant be null or empty");

        RuleFor(entity => entity.TitleRu)
            .NotEmpty()
            .NotNull()
            .WithMessage($"{nameof(NewsListCreateModel.TitleRu)} cant be null or empty");

        RuleFor(entity => entity.SubTitleRu)
            .NotEmpty()
            .NotNull()
            .WithMessage($"{nameof(NewsListCreateModel.SubTitleRu)} cant be null or empty");
    }
}