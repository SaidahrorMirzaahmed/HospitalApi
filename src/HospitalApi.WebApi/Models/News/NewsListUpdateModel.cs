namespace HospitalApi.WebApi.Models.News;

public class NewsListUpdateModel
{
    public string Title { get; set; }
    public string TitleRu { get; set; }

    public string SubTitle { get; set; }
    public string SubTitleRu { get; set; }

    public IFormFile? Picture { get; set; } = null;
}