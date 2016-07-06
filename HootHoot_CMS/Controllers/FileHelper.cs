using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HootHoot_CMS.Controllers
{
    public class FileHelper
    {

        public FileInfo uploadFileToServer_Result(HttpPostedFileBase fileToUpload)
        {
            bool uploadSuccess = false;
            int fileSize = fileToUpload.ContentLength;
            string fileName = fileToUpload.FileName;

            if (uploadSuccess = checksFileSize_ExceedsLimit(fileSize) )
            {
                fileToUpload.SaveAs(Constants.UPLOAD_FOLDER_PATH + fileName);
                //uploadSuccess will be TRUE automatically if everything goes fine
            }

            return new FileInfo(fileName, fileSize, fileToUpload.ContentType, uploadSuccess);
        }

        public bool checksFileSize_ExceedsLimit(int fileSize)
        {
            return (fileSize <= Constants._4MB_IN_BYTES);
        }

        public void checkFileExt_Valid()
        {

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