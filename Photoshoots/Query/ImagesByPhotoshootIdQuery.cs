using FrogOnALily.Photoshoots.Model;
using MediatR;
using System.Collections.Generic;

namespace FrogOnALily.Photoshoots.Query
{
    public class ImagesByPhotoshootNameQuery : IRequest<IEnumerable<PhotoshootImage>>
    {
        public ImagesByPhotoshootNameQuery(string photoshootName)
        {
            PhotoshootName = photoshootName;
        }

        public string PhotoshootName { get; }
    }
}
