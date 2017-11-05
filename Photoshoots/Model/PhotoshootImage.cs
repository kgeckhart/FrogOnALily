using System;

namespace FrogOnALily.Photoshoots.Model
{
    public class PhotoshootImage
    {
        public PhotoshootImage(int imageNumber, Uri imageUri)
        {
            ImageNumber = imageNumber;
            ImageUri = imageUri;
        }

        public int ImageNumber { get; }
        public Uri ImageUri { get; }
    }
}
