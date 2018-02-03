using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using FrogOnALily.Photoshoots.Model;
using System.Threading;

namespace FrogOnALily.Photoshoots.Query
{
    public class ImagesByPhotoshootNameQueryHandler : IRequestHandler<ImagesByPhotoshootNameQuery, IEnumerable<PhotoshootImage>>
    {
        public readonly IImageRepository _imageRepository;

        public ImagesByPhotoshootNameQueryHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<PhotoshootImage>> Handle(ImagesByPhotoshootNameQuery request, CancellationToken cancellationToken)
        {
            return await _imageRepository.PhotoshootImagesByName(request.PhotoshootName);
        }
    }
}
