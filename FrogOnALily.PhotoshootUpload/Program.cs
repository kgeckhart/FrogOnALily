using System.IO;
using System.Threading.Tasks;

namespace FrogOnALily.PhotoshootUpload
{
    class Program
    {
        
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

            //new ImageResize().ResizeImagesInDirectory(sourceDirectory, destinationDirectory);

            var photoshootClient = new PhotoshootAwsClient();
            photoshootClient.UploadPhotosFromPhotoshoot(photoshootName, destinationDirectory).Wait();
        }
    }
}
