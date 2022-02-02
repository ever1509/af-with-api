using Application.Clicks.Commands.TrackClick;
using Application.Urls.Commands.CreateShortUrl;
using Application.Urls.Queries.GetAllUrls;
using Application.Urls.Queries.GetUrlStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/url/")]
    [ApiController]
    public class UrlController : ControllerBase
    {     
        private readonly IMediator _mediator;

        public UrlController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UrlDto>>> GetAllUrls([FromQuery] GetAllUrlsQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("stats")]
        public async Task<ActionResult<ShowStatsDto>> GetUrlStats([FromQuery] GetUrlStatsQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("track-click")]
        public async Task<ActionResult<string>> TrackClick([FromBody] CreateClickCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("create-short-url")]
        public async Task<ActionResult<bool>> CreateShortUrl([FromBody] CreateShortUrlCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
