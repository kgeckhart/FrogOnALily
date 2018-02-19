using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FrogOnALily.PhotoshootUpload
{
    internal class PhotoshootAwsClient
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IAmazonDynamoDB _dyanmoClient;
        private enum Action { Update, Delete, DoNothing }

        public PhotoshootAwsClient(IAmazonS3 s3Client = null, IAmazonDynamoDB dyanmoClient = null)
        {
            _s3Client = s3Client ?? new AmazonS3Client(new EnvironmentVariablesAWSCredentials(), RegionEndpoint.USEast1);
            _dyanmoClient = dyanmoClient ?? new AmazonDynamoDBClient(new EnvironmentVariablesAWSCredentials(), RegionEndpoint.USEast1);
        }

        public async Task UploadPhotosFromPhotoshoot(string photoshootName, string destinationDirectory)
        {
            var s3Prefix = $"{photoshootName}/";
            var request = new ListObjectsV2Request
            {
                BucketName = AwsConstants.S3BucketName,
                Prefix = s3Prefix
            };

            var currentPhotos = (await _s3Client.ListObjectsV2Async(request)).S3Objects;

            var newPhotos = Directory.GetFiles(destinationDirectory)
                .Select(file => new FileInfo(file));
            
            var diffResults = newPhotos.FullJoin(currentPhotos, 
                first => (FileName: first.Name, Size: first.Length),
                second => (FileName: second.Key.Replace(s3Prefix, "", StringComparison.InvariantCultureIgnoreCase), Size: second.Size),
                first => (Action : Action.Update, FileInfo : first, S3Object: default(S3Object)),
                second => (Action: Action.Delete, FileInfo: default(FileInfo), S3Object: second),
                (first, second) => (Action: Action.DoNothing, FileInfo: default(FileInfo), S3Object: default(S3Object)));

            var s3ObjectsToDelete = diffResults.Where(diffResult => diffResult.Action == Action.Delete).Select(delete => delete.S3Object);
            await DeleteObjectsFromS3(s3ObjectsToDelete);

            var filesToUpdate = diffResults.Where(diffResult => diffResult.Action == Action.Update).Select(update => update.FileInfo);
            await UpdateS3Objects(filesToUpdate, photoshootName);
        }

        private async Task DeleteObjectsFromS3(IEnumerable<S3Object> s3objectsToDelete)
        {
            var deleteKeys = s3objectsToDelete.Select(s3Object => new KeyVersion { Key = s3Object.Key }).ToList();
            if (!deleteKeys.Any())
                return;

            var deleteRequest = new DeleteObjectsRequest
            {
                BucketName = AwsConstants.S3BucketName,
                Objects = deleteKeys
            };
            var result = await _s3Client.DeleteObjectsAsync(deleteRequest);

            var errorText = new StringBuilder();
            if (result.HttpStatusCode >= HttpStatusCode.BadRequest)
            {
                errorText.AppendLine($"Error Status Code Retuned From Delete Request, Status Code = {result.HttpStatusCode}");
            }
            if (result.DeleteErrors.Any())
            {
                result.DeleteErrors.ForEach(error => errorText.AppendLine($"Delete Error from request, Key:{error.Key}, ErrorCode:{error.Code}, Message:{error.Message}"));
            }

            if (errorText.Length != 0)
            {
                throw new Exception(errorText.ToString());
            }
        }

        private async Task UpdateS3Objects(IEnumerable<FileInfo> filesToUpdate, string photoshootName)
        {
            var errorText = new StringBuilder();
            foreach(var file in filesToUpdate)
            {
                var request = new PutObjectRequest
                {
                    BucketName = AwsConstants.S3BucketName,
                    Key = photoshootName + "/" + file.Name,
                    FilePath = file.FullName
                };
                var result = await _s3Client.PutObjectAsync(request);

                if (result.HttpStatusCode >= HttpStatusCode.BadRequest)
                {
                    errorText.AppendLine($"File {file.Name} generated a unsuccessful status code of {result.HttpStatusCode}");
                }
            }

            if (errorText.Length > 0)
            {
                throw new Exception(errorText.ToString());
            }
        }
    }
}