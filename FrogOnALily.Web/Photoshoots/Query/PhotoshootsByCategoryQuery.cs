using System.Collections.Generic;
using MediatR;
using FrogOnALily.Photoshoots.Model;

namespace FrogOnALily.Photoshoots.Query
{
    public class PhotoshootsByCategoryQuery : IRequest<IEnumerable<Photoshoot>>
    {
        public PhotoshootsByCategoryQuery(PhotoshootCategory? category)
        {
            Category = category;
        }

        public PhotoshootCategory? Category { get; }
    }
}
