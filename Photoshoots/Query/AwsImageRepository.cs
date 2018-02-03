using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.S3;
using FrogOnALily.Photoshoots.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FrogOnALily.Photoshoots.Query
{
    public class AwsImageRepository : IImageRepository
    {
        private readonly IAmazonDynamoDB _dynamoClient;
        private readonly IAmazonS3 _s3Client;

        public AwsImageRepository(IAmazonDynamoDB dynamoClient, IAmazonS3 s3Client)
        {
            _dynamoClient = dynamoClient;
            _s3Client = s3Client;
        }

        public async Task<List<Photoshoot>> PhotoshootByCategory(PhotoshootCategory? category = null)
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
                var imageUri = (AwsConstants.S3BaseUri + "/" + AwsConstants.S3BucketName +
                    "/" + imagePath   + "/" + AwsConstants.CoverPhotoName);
                photoshoots.Add(new Photoshoot(photoshootName, (PhotoshootCategory)Enum.Parse(typeof(PhotoshootCategory),categoryString),
                    DateTime.Parse(shootDateString), imageUri));
            }
            return photoshoots;
        }
    }
}
