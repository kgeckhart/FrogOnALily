namespace FrogOnALily.Photoshoots.Model
{
    public class PhotoshootImage
    {
        public PhotoshootImage(string imageUri, string mediumUri, string thumbnailUri)
        {
            ImageUri = imageUri;
            MediumUri = mediumUri;
            ThumbnailUri = thumbnailUri;
        }

        public string ImageUri { get; }
        public string MediumUri { get; }
        public string ThumbnailUri { get; }
    }
}
