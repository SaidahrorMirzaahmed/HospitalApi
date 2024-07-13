using FluentValidation;
using HospitalApi.WebApi.Models.News;

namespace HospitalApi.WebApi.Validations.News;

public class NewsListUpdateModelValidator : AbstractValidator<NewsListUpdateModel>
{
    public NewsListUpdateModelValidator()
    {
        
    }
}
