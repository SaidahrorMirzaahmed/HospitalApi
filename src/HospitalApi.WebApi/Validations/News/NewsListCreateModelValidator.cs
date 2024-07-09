using FluentValidation;
using HospitalApi.WebApi.Models.News;

namespace HospitalApi.WebApi.Validations.News;

public class NewsListCreateModelValidator : AbstractValidator<NewsListCreateModel>
{
    public NewsListCreateModelValidator()
    {
        RuleFor(x=> x.PublisherId).NotNull().NotEmpty()
            .WithMessage(x=> $"{nameof(x.PublisherId)} should not be null or empty");
    }
}
