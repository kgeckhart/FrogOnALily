using FrogOnALily.Photoshoots.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrogOnALily.Photoshoots.Query
{
    public interface IImageRepository
    {
        Task<IEnumerable<Photoshoot>> PhotoshootByCategory(PhotoshootCategory? category = null);

        Task<IEnumerable<PhotoshootImage>> PhotoshootImagesByName(string photoshootName);
    }
}
