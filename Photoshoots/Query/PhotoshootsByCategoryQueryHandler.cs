using System.Collections.Generic;
using System;
using MediatR;
using System.Threading.Tasks;
using System.Linq;
using FrogOnALily.Photoshoots.Model;

namespace FrogOnALily.Photoshoots.Query
{
    public class PhotoshootsByCategoryQueryHandler : IAsyncRequestHandler<PhotoshootsByCategoryQuery, IEnumerable<Photoshoot>>
    {
        private List<Photoshoot> _photoshoots;

        public PhotoshootsByCategoryQueryHandler()
        {
            _photoshoots = new List<Photoshoot> {
                new Photoshoot(1, "Kenton Robert", PhotoshootCategory.Newborn, new DateTime(2017, 9, 20), new Uri("http://d1h83yq8rkrddi.cloudfront.net/wp-content/uploads/2017/09/IMG_8376.jpg"))
            };
        }

        public Task<IEnumerable<Photoshoot>> Handle(PhotoshootsByCategoryQuery message)
        {
            IEnumerable<Photoshoot> result = _photoshoots;

            if (message.Category != null)
            {
                result = result.Where(photoshoot => photoshoot.Category == message.Category);
            }

            return Task.FromResult(result);
        }
    }
}
