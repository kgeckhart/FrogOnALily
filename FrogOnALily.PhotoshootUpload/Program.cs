using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FrogOnALily.PhotoshootUpload
{
    class Program
    {
        
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No directory provided");
                return;
            }
            var sourceDirectory = args[0];

            if (Directory.Exists(sourceDirectory) == false)
            {
                Console.WriteLine($"Source directory {sourceDirectory} does not exist");
            }

            var photoshootDirectories = new List<(string SouceDirectory, string DestinationDirectory, string PhotoshootName)>();
            var subDirectories = Directory.GetDirectories(sourceDirectory);
            if (subDirectories.Any())
            {
                
                foreach(var directory in subDirectories)
                {
                    var splitDirectory = directory.Split(Path.DirectorySeparatorChar);
                    var photoshootName = splitDirectory[splitDirectory.Length - 1];
                    var destinationDirectory = sourceDirectory + @"\resized\" + photoshootName + Path.DirectorySeparatorChar;
                    photoshootDirectories.Add((directory, destinationDirectory, photoshootName));
                }
            }
            else
            {
                var splitSourceDirectory = sourceDirectory.Split(Path.DirectorySeparatorChar);
                var photoshootName = splitSourceDirectory[splitSourceDirectory.Length - 1];
                splitSourceDirectory[splitSourceDirectory.Length - 1] = @"resized\" + photoshootName + Path.DirectorySeparatorChar;
                var destinationDirectory = string.Join(Path.DirectorySeparatorChar, splitSourceDirectory);
                photoshootDirectories.Add((sourceDirectory, destinationDirectory, photoshootName));
            }

            foreach(var photoshootDirectory in photoshootDirectories)
            {
                ConvertAndUploadForPhotoshoot(photoshootDirectory).Wait();
            }
        }

        private static async Task ConvertAndUploadForPhotoshoot((string SourceDirectory, string DestinationDirectory, string PhotoshootName) photoshoot)
        {
            new ImageResize().ResizeImagesInDirectory(photoshoot.SourceDirectory, photoshoot.DestinationDirectory);
            var photoshootClient = new PhotoshootAwsClient();
            await photoshootClient.UploadPhotosFromPhotoshoot(photoshoot.PhotoshootName, photoshoot.DestinationDirectory);
        }
    }
}
