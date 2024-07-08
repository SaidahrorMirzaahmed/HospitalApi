namespace HospitalApi.WebApi.Models.News;

public class NewsListUpdateModel
{
    public long PublisherId { get; set; }
    public string Text { get; set; }
    public long PictureId { get; set; }
}
