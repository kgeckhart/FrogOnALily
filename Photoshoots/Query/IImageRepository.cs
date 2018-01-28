using FrogOnALily.Photoshoots.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrogOnALily.Photoshoots.Query
{
    public interface IImageRepository
    {
        Task<List<Photoshoot>> PhotoshootByCategory(PhotoshootCategory? category = null);
    }
}
