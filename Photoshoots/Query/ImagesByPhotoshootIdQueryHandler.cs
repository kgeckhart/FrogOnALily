using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using FrogOnALily.Photoshoots.Model;

namespace FrogOnALily.Photoshoots.Query
{
    public class ImagesByPhotoshootIdQueryHandler : IAsyncRequestHandler<ImagesByPhotoshootIdQuery, IEnumerable<PhotoshootImage>>
    {
        public Task<IEnumerable<PhotoshootImage>> Handle(ImagesByPhotoshootIdQuery message)
        {
            return Task.FromResult(Enumerable.Empty<PhotoshootImage>());
        }
    }
}
