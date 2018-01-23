using System;

namespace FrogOnALily.Photoshoots.Model
{
    public class PhotoshootImage
    {
        public PhotoshootImage(int imageNumber, Uri imageUri, Uri thumbnailUri)
        {
            ImageNumber = imageNumber;
            ImageUri = imageUri;
            ThumbnailUri = thumbnailUri;
        }

        public int ImageNumber { get; }
        public Uri ImageUri { get; }
        public Uri ThumbnailUri { get; }
    }
}
