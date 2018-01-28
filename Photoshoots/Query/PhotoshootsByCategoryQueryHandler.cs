using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using System.Linq;
using FrogOnALily.Photoshoots.Model;

namespace FrogOnALily.Photoshoots.Query
{
    public class PhotoshootsByCategoryQueryHandler : IAsyncRequestHandler<PhotoshootsByCategoryQuery, IEnumerable<Photoshoot>>
    {
        private readonly IImageRepository _repository;

        public PhotoshootsByCategoryQueryHandler(IImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Photoshoot>> Handle(PhotoshootsByCategoryQuery message)
        {
            return (await _repository.PhotoshootByCategory(message.Category)).
                OrderByDescending(photoshoot => photoshoot.ShootDate);
        }
    }
}
