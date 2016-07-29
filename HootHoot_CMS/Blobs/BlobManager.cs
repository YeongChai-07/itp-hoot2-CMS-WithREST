using System;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HootHoot_CMS.Blobs
{
    public class BlobManager
    {
        private CloudStorageAccount storageAccount = null;
        private CloudBlobContainer newContainer = null;
        private CloudBlockBlob cloud_BlockBlob = null;

        public BlobManager()
        {
            storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting(Constants.AZURE_STORAGE_SETTINGS_KEYNAME));

            //Setup the Cloud Blob Client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //newContainer = blobClient.GetContainerReference("test-container");
            newContainer = blobClient.GetContainerReference(Constants.HOOTSQ_IMAGE_BLOB_CONTAINER);
            newContainer.CreateIfNotExists();

            newContainer.SetPermissions( new BlobContainerPermissions()
                                            { PublicAccess = BlobContainerPublicAccessType.Blob } );

            //cloud_BlockBlob = newContainer.GetBlockBlobReference("test-blob.jpg");

        }

        public string uploadPictureToBlob(string pictureFileName)
        {
            //Perform some checks whether the pictureFileName (and file extension) is correct

            // If pictureFileName and file extension is correct
            cloud_BlockBlob = newContainer.GetBlockBlobReference(pictureFileName);

            string pictureFullPath = Constants.UPLOAD_FOLDER_PATH + pictureFileName;
            string retUri = "";
            try
            {
                cloud_BlockBlob.UploadFromFile(pictureFullPath);
                retUri = cloud_BlockBlob.Uri.ToString();
            }
            catch(System.IO.FileNotFoundException fnf)
            {
                // Show an error to user probably there's problem uploading the file to the web server
                // for some reason the web server did not receive the file
            }
            catch(System.IO.DirectoryNotFoundException dnf)
            {
                // The path is invalid, perhaps the path is not complete ?
            }

            return retUri;
            
        } //End of UploadPictureToBlob Function

        public void listAllBlobs()
        {
            foreach(IListBlobItem listBlob in newContainer.ListBlobs())
            {
                if(listBlob.GetType() ==  typeof(CloudBlockBlob) )
                {
                    CloudBlockBlob bb = (CloudBlockBlob)listBlob;
                    Console.WriteLine("Found a Cloud Block Blob..." +
                        "Content Information:\n" + bb.Properties + "\nBlob stored at:\n" + bb.StorageUri);
                }
                
            }
        }
    }
}