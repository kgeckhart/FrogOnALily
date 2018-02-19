using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using System.Linq;
using FrogOnALily.Photoshoots.Model;
using System.Threading;

namespace FrogOnALily.Photoshoots.Query
{
    public class PhotoshootsByCategoryQueryHandler : IRequestHandler<PhotoshootsByCategoryQuery, IEnumerable<Photoshoot>>
    {
        private readonly IImageRepository _repository;

        public PhotoshootsByCategoryQueryHandler(IImageRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<Photoshoot>> Handle(PhotoshootsByCategoryQuery request, CancellationToken cancellationToken)
        {
            return (await _repository.PhotoshootByCategory(request.Category)).
                OrderByDescending(photoshoot => photoshoot.ShootDate);
        }
    }
}
