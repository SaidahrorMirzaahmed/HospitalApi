using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.News;

public class NewsListViewModel
{
    public long Id {  get; set; }
    public UserViewModel Publisher { get; set; }
    public string Text { get; set; }
    public UserViewModel Picture { get; set; }
}