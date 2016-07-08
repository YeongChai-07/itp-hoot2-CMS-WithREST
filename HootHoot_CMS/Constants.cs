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
        public static string AZURE_BLOB_STORAGE_FOLDER = "https://hootsqpicturestorage.blob.core.windows.net/hootsq-image-options/";
        public static string HOOTSQ_IMAGE_BLOB_CONTAINER = "hootsq-image-options";
        public const string QNS_IMAGE_OPTION_TYPE = "IMAGE";
        public const int _4MB_IN_BYTES = 4194304;
        public static readonly char[] FILEPATH_PATTERN = { '/', '\\', ':' };
        public const string FILE_UPLOAD_NOT_FOUND = "The specified file is not found. Perhaps the file " +
                       "isn't uploaded correctly ?";

        public static readonly List<SelectListItem> CORRECTOPTION_LIST = customCorrectOption_List(string.Empty);


        public static List<SelectListItem> customCorrectOption_List(string selectedValue)
        {
            List<SelectListItem> ddl_correctOption = new List<SelectListItem>();
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 1", Value = "option_1" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 2", Value = "option_2" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 3", Value = "option_3" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 4", Value = "option_4" });

            if (!selectedValue.Equals(string.Empty))
            {
                ddl_correctOption.Single(option => option.Value == selectedValue).Selected = true;
            }

            return ddl_correctOption;
        }
    }
}