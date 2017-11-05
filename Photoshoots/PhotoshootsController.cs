using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using FrogOnALily.Photoshoots.Model;
using FrogOnALily.Photoshoots.Query;

namespace FrogOnALily.Photoshoots
{
    [Route("api/photoshoots")]
    public class PhotoshootsController : Controller
    {
        private readonly IMediator _mediator;

        public PhotoshootsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetPhotoshoots(PhotoshootCategory? category = null)
        {
            return Ok(await _mediator.Send(new PhotoshootsByCategoryQuery(category)));
        }

        [HttpGet("{photoshootId}/images")]
        public async Task<IActionResult> GetImagesForPhotoshoots(int photoshootId)
        {
            return Ok(await _mediator.Send(new ImagesByPhotoshootIdQuery(photoshootId)));
        }
    }
}
