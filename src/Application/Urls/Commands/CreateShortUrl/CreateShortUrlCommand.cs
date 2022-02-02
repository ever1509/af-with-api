using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Urls.Commands.CreateShortUrl;
public class CreateShortUrlCommand : IRequest<bool>
{
    public string Url { get; set; }
}

public class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, bool>
{
    private readonly IContext _context;
    private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public CreateShortUrlCommandHandler(IContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.Url) && IsValidUrl(request.Url) && !ExistUrl(request.Url))
        {
            var newUrl = new Url()
            {
                Id = Guid.NewGuid(),
                OriginalUrl = request.Url,
                ShortUrl = generateShortLink(),
                CreatedDateUrl = DateTime.Now,
                Count = 0
            };
            _context.Urls.Add(newUrl);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }

    private bool IsValidUrl(string url)
    {
        Uri uriResult;
        bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                      && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        return result;
    }

    private string generateShortLink()
    {
        string ShortLink = GenerateShortString(5);
        while (GetOriginUrl(ShortLink) != null)
        {
            ShortLink = GenerateShortString(5);
        }
        return ShortLink;
    }

    private string GenerateShortString(int length)
    {
        Random random = new Random();
        var newString = new string(Enumerable.Repeat(CHARS, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        if (ExistShortUrl(newString))
        {
            GenerateShortString(5);
        }

        return newString;
    }

    private bool ExistShortUrl(string newString)
    {
        var urls = _context.Urls.Where(x => x.ShortUrl == newString).ToList();
        return urls.Count > 0;
    }

    private string GetOriginUrl(string shortUrl)
    {
        return _context.Urls.SingleOrDefault(x => x.ShortUrl == shortUrl)?.OriginalUrl;
    }

    private bool ExistUrl(string url)
    {
        var alreadyUrl = _context.Urls.SingleOrDefault(x => x.OriginalUrl == url);

        return alreadyUrl != null;
    }
}
