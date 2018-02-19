using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FrogOnALily.PhotoshootUpload
{
    public class ImageResize
    {
        private readonly List<(int Width, string Identifier)> _sizes;

        public ImageResize(List<(int Width, string Identifer)> sizes = null)
        {
            _sizes = sizes ?? new List<(int Width, string Identifier)> { (1400, "Large"), (800, "Medium"), (200, "Thumbnail") };
        }

        public void ResizeImagesInDirectory(string sourceDirectory, string destinationDirectory)
        {
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);    
            }

            var images = Directory.GetFiles(sourceDirectory).Where(file => file.EndsWith("JPG", StringComparison.InvariantCultureIgnoreCase));

            if (!images.Any())
                return;

            ResizeAndSaveImage(images.First(), destinationDirectory + "CoverPhoto.JPG", 400);

            foreach (var imagePath in images)
            {
                foreach (var (Width, Identifier) in _sizes)
                {
                    var fileName = Path.GetFileNameWithoutExtension(imagePath) + "_" + Identifier + ".JPG";
                    ResizeAndSaveImage(imagePath, destinationDirectory + fileName, Width);
                }
            }
        }

        private static void ResizeAndSaveImage(string sourceImagePath, string destinationImagePath, int width)
        {
            using (var image = Image.Load(sourceImagePath))
            {
                Console.WriteLine($"Writing {destinationImagePath}");
                using (var fileStream = File.Create(destinationImagePath))
                {
                    image.Clone(x => x.Resize(width, 0)).SaveAsJpeg(fileStream);
                }
            }
        }
    }
}
