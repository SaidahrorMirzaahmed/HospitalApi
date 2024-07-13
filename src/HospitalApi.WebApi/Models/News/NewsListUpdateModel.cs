namespace HospitalApi.WebApi.Models.News;

public class NewsListUpdateModel
{
    public string Text { get; set; }
    public IFormFile Picture { get; set; } = null;
}
