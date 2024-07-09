using FluentValidation;
using HospitalApi.WebApi.Models.News;

namespace HospitalApi.WebApi.Validations.News;

public class NewsListUpdateModelValidator : AbstractValidator<NewsListUpdateModel>
{
    public NewsListUpdateModelValidator()
    {
        RuleFor(x => x.PublisherId).NotNull().NotEmpty()
            .WithMessage(x => $"{nameof(x.PublisherId)} should not be null or empty");
    }
}
