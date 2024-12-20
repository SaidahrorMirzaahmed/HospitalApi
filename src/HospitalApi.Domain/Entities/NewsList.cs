using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class NewsList : Auditable
{
    public long PublisherId { get; set; }
    public User Publisher { get; set; }

    public string Title { get; set; }
    public string TitleRu { get; set; }

    public string SubTitle { get; set; }
    public string SubTitleRu { get; set; }
    
    public long? PictureId { get; set; }
    public Asset? Picture { get; set; }
}