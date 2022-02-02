using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Urls.Queries.GetAllUrls;
public class GetAllUrlsQuery : IRequest<List<UrlDto>>
{
}

public class GetAllUrlQueryHandler : IRequestHandler<GetAllUrlsQuery, List<UrlDto>>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;

    public GetAllUrlQueryHandler(IContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UrlDto>> Handle(GetAllUrlsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Urls
            .ProjectTo<UrlDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

