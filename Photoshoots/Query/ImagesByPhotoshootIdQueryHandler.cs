using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using FrogOnALily.Photoshoots.Model;
using System.Threading;

namespace FrogOnALily.Photoshoots.Query
{
    public class ImagesByPhotoshootIdQueryHandler : IRequestHandler<ImagesByPhotoshootNameQuery, IEnumerable<PhotoshootImage>>
    {
        public Task<IEnumerable<PhotoshootImage>> Handle(ImagesByPhotoshootNameQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Enumerable.Empty<PhotoshootImage>());
        }
    }
}
