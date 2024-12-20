using HospitalApi.WebApi.Models.Assets;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.News;

public class NewsListViewModel
{
    public long Id { get; set; }
    public UserViewModel Publisher { get; set; }

    public string Title { get; set; }
    public string TitleRu { get; set; }

    public string SubTitle { get; set; }
    public string SubTitleRu { get; set; }

    public AssetViewModel? Picture { get; set; }
}