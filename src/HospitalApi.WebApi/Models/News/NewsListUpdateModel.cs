namespace HospitalApi.WebApi.Models.News;

public class NewsListUpdateModel
{
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public IFormFile? Picture { get; set; } = null;
}
