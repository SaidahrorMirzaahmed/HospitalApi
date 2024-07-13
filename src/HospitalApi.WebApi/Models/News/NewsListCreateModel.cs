namespace HospitalApi.WebApi.Models.News;

public class NewsListCreateModel
{
    public string Text { get; set; }
    public IFormFile Picture { get; set; } = null;
}
