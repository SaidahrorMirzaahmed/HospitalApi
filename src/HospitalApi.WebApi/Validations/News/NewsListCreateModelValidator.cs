using FluentValidation;
using HospitalApi.WebApi.Models.News;

namespace HospitalApi.WebApi.Validations.News;

public class NewsListCreateModelValidator : AbstractValidator<NewsListCreateModel>
{
    public NewsListCreateModelValidator()
    {

    }
}