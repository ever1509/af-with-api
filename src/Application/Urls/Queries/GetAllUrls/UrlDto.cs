using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Urls.Queries.GetAllUrls;
public class UrlDto : IMapFrom<Url>

{
    public Guid Id { get; set; }
    public string ShortUrl { get; set; }
    public int Count { get; set; }
    public DateTime CreatedDateUrl { get; set; }
    public string OriginalUrl { get; set; }
}
