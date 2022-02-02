using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Clicks.Commands.TrackClick;
public class CreateClickCommand : IRequest<string>
{
    public string ShortUrl { get; set; }
    public string UserPlatform { get; set; }
    public string Browser { get; set; }
}

public class CreateClickCommandHandler : IRequestHandler<CreateClickCommand, string>
{
    private readonly IContext _context;

    public CreateClickCommandHandler(IContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateClickCommand request, CancellationToken cancellationToken)
    {
        var url = _context.Urls.SingleOrDefault(x => x.ShortUrl == request.ShortUrl);
        if (url != null)
        {
            url.Count++;
            _context.Clicks.Add(new Click() { Browser = request.Browser, Platform = request.UserPlatform, UrlId = url.Id, CreatedDateClick = DateTime.Now });
            await _context.SaveChangesAsync(cancellationToken);

            return GetOriginUrl(request.ShortUrl);
        }

        return string.Empty;
    }

    private string GetOriginUrl(string shortUrl)
    {
        return _context.Urls.SingleOrDefault(x => x.ShortUrl == shortUrl)?.OriginalUrl;
    }
}
