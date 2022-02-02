using Application.Urls.Queries.GetAllUrls;

namespace Application.Urls.Queries.GetUrlStats;
public class ShowStatsDto
{
    public UrlDto? Url { get; set; }
    public Dictionary<string, int> DailyClicks { get; set; }
    public Dictionary<string, int> BrowseClicks { get; set; }
    public Dictionary<string, int> PlatformClicks { get; set; }
}

