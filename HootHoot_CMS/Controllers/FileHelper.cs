using System.Web;

namespace HootHoot_CMS.Controllers
{
    public class FileHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileToUpload"></param>
        /// <returns></returns>
        public static FileInfo uploadFileToServer_Result(HttpPostedFileBase fileToUpload)
        {
            bool uploadSuccess = false;
            int fileSize = fileToUpload.ContentLength;

            // Removes the absolute path with all the ("\") from the filename string, so that 
            // we will only end up with the file name (REQUIRED : for IE browsers)
            string fileName = fileName_Correction(fileToUpload.FileName);

            if (uploadSuccess = checksFileSize_ExceedsLimit(fileSize))
            {
                setupUploadFolder_IfNotAvailable();

                fileToUpload.SaveAs(Constants.UPLOAD_FOLDER_PATH + fileName);
                //uploadSuccess will be TRUE automatically if everything goes fine
            }

            return new FileInfo(fileName, fileSize, fileToUpload.ContentType, uploadSuccess);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        private static bool checksFileSize_ExceedsLimit(int fileSize)
        {
            return (fileSize <= Constants._4MB_IN_BYTES);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void setupUploadFolder_IfNotAvailable()
        {
            if (!System.IO.Directory.Exists(Constants.UPLOAD_FOLDER_PATH))
            {
                //This represent that the upload directory isn't created in the web server,
                //let's have it created first
                System.IO.Directory.CreateDirectory(Constants.UPLOAD_FOLDER_PATH);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool checkFileExists_Server(string fileName)
        {
            return System.IO.File.Exists(Constants.UPLOAD_FOLDER_PATH + fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool checkFileExt_Valid(string fileName)
        {
            string mimeType = MimeMapping.GetMimeMapping(Constants.UPLOAD_FOLDER_PATH + fileName).ToUpper();
            bool mimeValid = false;

            for(byte i=0;i<Constants.ACCEPTED_MIME_TYPE.Length;i++)
            {
                if (mimeType.Equals(Constants.ACCEPTED_MIME_TYPE[i]) )
                { mimeValid = true; break; }
            }

            return mimeValid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool checkImageDimension_Valid(string fileName)
        {
            bool dimens_Valid = false;

            using (System.Drawing.Image tempImage = System.Drawing.Image.FromFile(Constants.UPLOAD_FOLDER_PATH + fileName))
            {
                dimens_Valid = (tempImage.Width <= Constants.PIC_MAX_WIDTH) && (tempImage.Height <= Constants.PIC_MAX_HEIGHT);
            }

            return dimens_Valid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionValue"></param>
        /// <returns></returns>
        public static bool checkFileExists_Blob(string optionValue)
        {
            try
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    webClient.DownloadFile(optionValue, Constants.UPLOAD_FOLDER_PATH + 
                        fileName_Correction(optionValue) );

                    return true;
                }
            }
            catch(System.Net.WebException we)
            {
                return false;
            }
        } //End of checkFileExists_Blob function

        /// <summary>
        /// 
        /// </summary>
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

}