using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.S3;
using Amazon.S3.Model;
using FrogOnALily.Photoshoots.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FrogOnALily.Photoshoots.Query
{
    public partial class AwsImageRepository : IImageRepository
    {
        private readonly IAmazonDynamoDB _dynamoClient;
        private readonly IAmazonS3 _s3Client;

        public AwsImageRepository(IAmazonDynamoDB dynamoClient, IAmazonS3 s3Client)
        {
            _dynamoClient = dynamoClient;
            _s3Client = s3Client;
        }

        public async Task<IEnumerable<Photoshoot>> PhotoshootByCategory(PhotoshootCategory? category = null)
        {
            var dynamoQuery = await _dynamoClient.ScanAsync(new ScanRequest("Photoshoots"));
            if (dynamoQuery.HttpStatusCode >= HttpStatusCode.BadRequest)
            {
                throw new Exception($"Http Status Code was {dynamoQuery.HttpStatusCode})");
            }
            var photoshoots = new List<Photoshoot>();
            foreach (Dictionary<string, AttributeValue> item in dynamoQuery.Items)
            {
                var photoshootName = item["PhotoshootName"].S;
                var categoryString = item["Category"].S;
                var shootDateString = item["ShootDate"].S;
                var imagePath = photoshootName.Replace(" ", "+");
                var imageUri = (AwsConstants.CloudfrontBaseUri + imagePath   + "/" + AwsConstants.CoverPhotoName);
                photoshoots.Add(new Photoshoot(photoshootName, (PhotoshootCategory)Enum.Parse(typeof(PhotoshootCategory), categoryString),
                    DateTime.Parse(shootDateString), imageUri));
            }
            return photoshoots;
        }

        public async Task<IEnumerable<PhotoshootImage>> PhotoshootImagesByName(string photoshootName)
        {
            ListObjectsV2Request request = new ListObjectsV2Request
            {
                BucketName = AwsConstants.S3BucketName,
                Prefix = $"{photoshootName}/"
            };

            var objectsInShoot = (await _s3Client.ListObjectsV2Async(request)).S3Objects
                .Where(s3Object => !s3Object.Key.Contains(AwsConstants.CoverPhotoName));

            return objectsInShoot.Select(objectInShoot =>
            {
                var imagePath = objectInShoot.Key;
                var imageKey = imagePath.Remove(imagePath.LastIndexOf("_"));
                var imageType = imagePath.Contains("Thumbnail") ? ImageType.Thumbnail : ImageType.Large;
                return (imagePath, imageKey, imageType);
            })
            .GroupBy(image => image.imageKey).OrderBy(imageSet => imageSet.Key)
            .Select(imageSet =>
            {
                var thumbnailPath = imageSet.Where(image => image.imageType == ImageType.Thumbnail)
                    .FirstOrDefault().imagePath.Replace(" ", "+");
                var imagePath = imageSet.Where(image => image.imageType == ImageType.Large)
                    .FirstOrDefault().imagePath.Replace(" ", "+");
                return new PhotoshootImage(AwsConstants.CloudfrontBaseUri + imagePath,
                    AwsConstants.CloudfrontBaseUri + thumbnailPath);
            });
        }
    }
}
