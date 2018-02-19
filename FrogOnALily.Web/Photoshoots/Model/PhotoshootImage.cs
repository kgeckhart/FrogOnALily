namespace FrogOnALily.Photoshoots.Model
{
    public class PhotoshootImage
    {
        public PhotoshootImage(string imageUri, string thumbnailUri)
        {
            ImageUri = imageUri;
            ThumbnailUri = thumbnailUri;
        }

        public string ImageUri { get; }
        public string ThumbnailUri { get; }
    }
}
