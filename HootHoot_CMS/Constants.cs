using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HootHoot_CMS
{
    public class Constants
    {
        public static readonly string UPLOAD_FOLDER_PATH = HttpRuntime.AppDomainAppPath + @"Upload\";
        public static string AZURE_STORAGE_SETTINGS_KEYNAME = "StorageConnectionString";
        public static string HOOTSQ_IMAGE_BLOB_CONTAINER = "hootsq-image-options";
        public const string QNS_IMAGE_OPTION_TYPE = "IMAGE";
        public const int _4MB_IN_BYTES = 4194304;
        public static readonly char[] FILEPATH_PATTERN = { '/', '\\', ':' };
        public const string FILE_UPLOAD_NOT_FOUND = "The specified file is not found. Perhaps the file " +
                       "isn't uploaded correctly ?";

        public static readonly List<SelectListItem> CORRECTOPTION_LIST = initCorrectOption_List();


        private static List<SelectListItem> initCorrectOption_List()
        {
            List<SelectListItem> ddl_correctOption = new List<SelectListItem>();
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 1", Value = "option_1" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 2", Value = "option_2" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 3", Value = "option_3" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 4", Value = "option_4" });

            return ddl_correctOption;
        }
    }
}