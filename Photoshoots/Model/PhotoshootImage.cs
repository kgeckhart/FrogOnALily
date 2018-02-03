using System;

namespace FrogOnALily.Photoshoots.Model
{
    public class PhotoshootImage
    {
        public PhotoshootImage(string imageName, Uri imageUri, Uri thumbnailUri)
        {
            ImageName = imageName;
            ImageUri = imageUri;
            ThumbnailUri = thumbnailUri;
        }

        public string ImageName { get; }
        public Uri ImageUri { get; }
        public Uri ThumbnailUri { get; }
    }
}
