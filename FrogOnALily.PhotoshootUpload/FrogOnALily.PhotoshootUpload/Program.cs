using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FrogOnALily.PhotoshootUpload
{
    class Program
    {
        private static List<(int Width, string Identifier)> _sizes = 
            new List<(int Width, string Identifier)> { (1400, "Large"), (800, "Medium"), (200, "Thumbnail") };

        static void Main(string[] args)
        {
            //if (args.Length == 0)
            //{
            //    Console.WriteLine("No directory provided");
            //    return;
            //}
            //var sourceDirectory = args[0];
            var sourceDirectory = @"D:\Users\Eckhart\Pictures\Industrial Strength";

            var splitSourceDirectory = sourceDirectory.Split(Path.DirectorySeparatorChar);
            var photoshootName = splitSourceDirectory[splitSourceDirectory.Length - 1];
            splitSourceDirectory[splitSourceDirectory.Length - 1] = @"resized\" + photoshootName + Path.DirectorySeparatorChar;
            var destinationDirectory = string.Join(Path.DirectorySeparatorChar, splitSourceDirectory);

            ResizeImagesInDirectory(sourceDirectory, destinationDirectory);
            UploadToS3(destinationDirectory);
        }

        private static void UploadToS3(string destinationDirectory)
        {
            throw new NotImplementedException();
        }

        private static void ResizeImagesInDirectory(string directory, string destinationDirectory)
        {
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            var images = Directory.GetFiles(directory).Where(file => file.EndsWith("JPG", StringComparison.InvariantCultureIgnoreCase));

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
