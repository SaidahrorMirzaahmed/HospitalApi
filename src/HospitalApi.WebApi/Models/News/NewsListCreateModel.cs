namespace HospitalApi.WebApi.Models.News;

public class NewsListCreateModel
{
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public IFormFile? Picture { get; set; } = null;
}