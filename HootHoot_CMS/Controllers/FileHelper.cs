using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HootHoot_CMS.Controllers
{
    public class FileHelper
    {
        public static FileInfo uploadFileToServer_Result(HttpPostedFileBase fileToUpload)
        {
            bool uploadSuccess = false;
            int fileSize = fileToUpload.ContentLength;

            // Removes the absolute path with all the ("\") from the filename string, so that 
            // we will only end up with the file name (REQUIRED : for IE browsers)
            string fileName = fileName_Correction(fileToUpload.FileName);

            if (uploadSuccess = checksFileSize_ExceedsLimit(fileSize))
            {
                fileToUpload.SaveAs(Constants.UPLOAD_FOLDER_PATH + fileName);
                //uploadSuccess will be TRUE automatically if everything goes fine
            }

            return new FileInfo(fileName, fileSize, fileToUpload.ContentType, uploadSuccess);
        }

        private static bool checksFileSize_ExceedsLimit(int fileSize)
        {
            return (fileSize <= Constants._4MB_IN_BYTES);
        }

        private static string fileName_Correction(string fileName)
        {
            //Check and replace each of the path pattern
            for (byte i = 0; i < Constants.FILEPATH_PATTERN.Length; i++)
            {
                fileName = fileName.Replace(Constants.FILEPATH_PATTERN[i], '>');
            }

            int actualStartIdx = -1;

            //Now we will check the index of the last occurence of '>' within the fileName
            if ((actualStartIdx = fileName.LastIndexOf('>')) > -1)
            {
                //Now increment the startIndex to 1 as the start of 
                // the filename is 1 position next to it
                actualStartIdx++;

                fileName = fileName.Substring(actualStartIdx);
            }

            return fileName;
        }

        public static bool checkFileExists_Server(string fileName)
        {
            return System.IO.File.Exists(Constants.UPLOAD_FOLDER_PATH + fileName);
        }

        public static void checkFileExt_Valid()
        {

        }

        public static bool checkFileExists_Blob()
        {
            using (System.Net.WebClient webClient = new System.Net.WebClient())
            {
                webClient.DownloadFile("https://hootsqpicturestorage.blob.core.windows.net/hootsq-image-options/Lighthouse.jpg",
                    Constants.UPLOAD_FOLDER_PATH + "moo.jpg");

            }
        }

        public static void deletesALLUploadFiles()
        {
            //Gets a list of files in the upload folder FIRST
            string[] filesInFolder = System.IO.Directory.GetFiles(Constants.UPLOAD_FOLDER_PATH);

            for (byte i = 0; i < filesInFolder.Length; i++)
            { System.IO.File.Delete(filesInFolder[i]); }

        }


        public class FileInfo
        {
            public string m_FileName { get; set; }
            public int m_FileSize { get; set; }
            public string m_FileExt { get; set; }
            public bool m_FileStats { get; set; }

            public FileInfo(string inFileName, int inFileSize, string inFileExt, bool inFileStats)
            {
                m_FileName = inFileName;
                m_FileSize = inFileSize;
                m_FileExt = inFileExt;
                m_FileStats = inFileStats;

            }

        }
    }



    /*public class JSONDataFormatter
    {

    }*/

}