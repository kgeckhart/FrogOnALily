using FrogOnALily.Photoshoots.Model;
using MediatR;
using System.Collections.Generic;

namespace FrogOnALily.Photoshoots.Query
{
    public class ImagesByPhotoshootIdQuery : IRequest<IEnumerable<PhotoshootImage>>
    {
        public ImagesByPhotoshootIdQuery(int photoshootId)
        {
            PhotoshootId = photoshootId;
        }

        public int PhotoshootId { get; }
    }
}
