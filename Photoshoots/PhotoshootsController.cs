using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using FrogOnALily.Photoshoots.Model;
using FrogOnALily.Photoshoots.Query;
using NSwag.Annotations;
using System.Collections.Generic;

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
        [SwaggerResponse(typeof(IEnumerable<Photoshoot>))]
        public async Task<IActionResult> GetPhotoshoots(PhotoshootCategory? category = null)
        {
            return Ok(await _mediator.Send(new PhotoshootsByCategoryQuery(category)));
        }

        [HttpGet("{photoshootName}/images")]
        [SwaggerResponse(typeof(IEnumerable<PhotoshootImage>))]
        public async Task<IActionResult> GetImagesForPhotoshoots(string photoshootName)
        {
            return Ok(await _mediator.Send(new ImagesByPhotoshootNameQuery(photoshootName)));
        }
    }
}
