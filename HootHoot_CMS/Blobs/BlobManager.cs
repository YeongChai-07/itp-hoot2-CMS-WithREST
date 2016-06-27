using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HootHoot_CMS.Blobs
{
    public class BlobManager
    {
        public void boo()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //Setup the Cloud Blob Client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        }
    }
}