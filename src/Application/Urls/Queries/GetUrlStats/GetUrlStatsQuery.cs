using Application.Common.Interfaces;
using Application.Urls.Queries.GetAllUrls;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;

namespace Application.Urls.Queries.GetUrlStats;
public class GetUrlStatsQuery : IRequest<ShowStatsDto>
{
    public string Url { get; set; }
}

public class GetUrlStatsQueryHandler : IRequestHandler<GetUrlStatsQuery, ShowStatsDto>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;

    public GetUrlStatsQueryHandler(IContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ShowStatsDto> Handle(GetUrlStatsQuery request, CancellationToken cancellationToken)
    {
        var showViewModel = new ShowStatsDto();
        var savedUrl = _context
            .Urls
            .ProjectTo<UrlDto>(_mapper.ConfigurationProvider)
            .SingleOrDefault(x => x.ShortUrl == request.Url);

        var clicksByDay = _context.Clicks.Where(x => x.UrlId == savedUrl.Id).Select(x => new
        {
            Date = x.CreatedDateClick.Value.ToString("M"),
            Id = x.ClickId
        }).ToList();

        showViewModel.Url = savedUrl;
        showViewModel.DailyClicks = clicksByDay
            .GroupBy(x => x.Date)
            .Select(x => new
            {
                Day = x.Key,
                Total = x.Count()
            }).ToDictionary(t => t.Day, t => t.Total);

        showViewModel.BrowseClicks = _context.Clicks.Where(x => x.UrlId == savedUrl.Id).GroupBy(x => x.Browser).Select(x => new
        {
            Browser = x.Key,
            Total = x.Count()
        }).ToDictionary(t => t.Browser, t => t.Total);

        showViewModel.PlatformClicks = _context.Clicks.Where(x => x.UrlId == savedUrl.Id).GroupBy(x => x.Platform).Select(x => new
        {
            Platform = x.Key,
            Total = x.Count()
        }).ToDictionary(t => t.Platform, t => t.Total);

        return showViewModel;
    }
}

