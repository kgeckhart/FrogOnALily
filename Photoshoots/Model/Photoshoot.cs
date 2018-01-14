using System;

namespace FrogOnALily.Photoshoots.Model
{
    public class Photoshoot
    {
        public Photoshoot(int id, string name, PhotoshootCategory category, DateTime shootDate, Uri thumbnailUri)
        {
            Id = id;
            Name = name;
            ThumbnailUri = thumbnailUri;
            Category = category;
            ShootDate = shootDate;
        }

        public int Id { get; }
        public string Name { get; }
        public PhotoshootCategory Category {get;}
        public Uri ThumbnailUri { get; }
        public DateTime ShootDate { get; set; }
    }
}
