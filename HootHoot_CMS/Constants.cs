using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HootHoot_CMS
{
    public class Constants
    {
        public static readonly string UPLOAD_FOLDER_PATH = HttpRuntime.AppDomainAppPath + @"Upload\";
        public static string AZURE_STORAGE_SETTINGS_KEYNAME = "StorageConnectionString";
        public static string HOOTSQ_IMAGE_BLOB_CONTAINER = "hootsq-image-options";
        public const string QNS_IMAGE_OPTION_TYPE = "IMAGE";
    }
}