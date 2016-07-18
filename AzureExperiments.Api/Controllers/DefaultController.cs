using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AzureExperiments.Api.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureExperiments.Controllers
{
    [RoutePrefix("api/default")]
    public class DefaultController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("You have reached the default controller successfully");
        }

        [Route("GetBlobs")]
        [HttpGet]
        public IHttpActionResult GetBlobs()
        {
            var blobConnection =
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var blobClient = blobConnection.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(CloudConfigurationManager.GetSetting("BlobContainerName"));

            if (!container.Exists())
                throw new Exception("Container does not exist");

            var blobs = container.ListBlobs().Select(x => x.Uri);

            return Ok(blobs);
        }

        [Route("AddBlob")]
        [HttpPost]
        public IHttpActionResult AddBlob(AddBlob args)
        {
            var blobConnection =
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var blobClient = blobConnection.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(CloudConfigurationManager.GetSetting("BlobContainerName"));

            if(!container.Exists())
                CreateBlobContainer(ref container);

            var blob = container.GetBlockBlobReference(args.Name);

            using (var stream = new MemoryStream(args.BlobObject))
            {
                blob.UploadFromStream(stream);
            }
            
            return Ok();
        }

        private void CreateBlobContainer(ref CloudBlobContainer container)
        {
            container.CreateIfNotExists();

            container.SetPermissions(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });
        }
    }
}
