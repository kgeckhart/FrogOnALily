using System;

namespace FrogOnALily.Photoshoots.Model
{
    public class Photoshoot
    {
        public Photoshoot(string name, PhotoshootCategory category, DateTime shootDate, string thumbnailUri)
        {
            Name = name;
            ThumbnailUri = thumbnailUri;
            Category = category;
            ShootDate = shootDate;
        }

        public string Name { get; }
        public PhotoshootCategory Category {get;}
        public DateTime ShootDate { get; set; }
        public string ThumbnailUri { get; }
    }
}
