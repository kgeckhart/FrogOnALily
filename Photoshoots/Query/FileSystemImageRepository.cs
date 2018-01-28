using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using FrogOnALily.Photoshoots.Model;
using System.IO;
using System.Globalization;

namespace FrogOnALily.Photoshoots.Query
{
    public class FileSystemImageRepository : IImageRepository
    {
        private const string ImageBasePath = @"\assets\website pictures medium\";

        public Task<List<Photoshoot>> PhotoshootByCategory(PhotoshootCategory? category = null)
        {
            var photoshoots = new List<Photoshoot>();

            foreach (var directoryIndex in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\src\assets\website pictures medium").Select((directory, index) => ( Directory: directory, Id: index)))
            {
                var leafDirectory = new DirectoryInfo(directoryIndex.Directory).Name;
                var splitDirectory = leafDirectory.Split(' ');
                var dateString = splitDirectory[splitDirectory.Length - 3] + splitDirectory[splitDirectory.Length - 2] + splitDirectory[splitDirectory.Length - 1];
                DateTime shootDate = DateTime.ParseExact(dateString, "MMMMd,yyyy", CultureInfo.InvariantCulture);
                var fileName = Path.GetFileName(Directory.GetFiles(directoryIndex.Directory).SingleOrDefault());
                var imageName = string.Join(' ', splitDirectory.Take(splitDirectory.Length - 3));
                photoshoots.Add(new Photoshoot(directoryIndex.Id, imageName.ToString(), PhotoshootCategory.Unknown,
                    shootDate, ImageBasePath + leafDirectory + @"\" + fileName));
            }

            return Task.FromResult(photoshoots);
        }
    }
}
