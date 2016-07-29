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
        public const int MAX_QUESTION_ITEMS = 10;
        public const byte OPTIONS_PER_QNS = 4;
        public const int _4MB_IN_BYTES = 4194304;
        public const byte MAX_RANDOM_FUNFACTS = 1;
        public const string STATION_TYPE_FOR_QNS = "HH";

        //Image Option type picture file MAX dimensions properties
        public static int PIC_MAX_WIDTH = 500;
        public static int PIC_MAX_HEIGHT = 500;

        public const string IMG_NO_PREVIEW_SRC = "../../Images/NoPreview.png";
        public const string IMG_PIC_UPLOAD_SRC = "../../Upload/";

        public static readonly char[] FILEPATH_PATTERN = { '/', '\\', ':' };
        public static readonly string[] INTERNET_ADDRESS_PATTERN = { "HTTPS://", "HTTP://", "FTP://", "FTPS://" };
        public static readonly string[] ACCEPTED_MIME_TYPE = { "IMAGE/BMP", "IMAGE/GIF", "IMAGE/JPEG", "IMAGE/PNG" };

        //Constant Literal string for validation error messages.
        public const string FILE_UPLOAD_NOT_FOUND = "The specified file is not found. Perhaps the file " +
                       "isn't uploaded correctly ?";
        public const string BLOB_PIC_NOT_FOUND = "The specified file location doesn't exists in the picture store. Please try again.";
        public const string TEXT_OPTION_HAS_BLOB_VALUE = "This option should not assign with any value that is an internet address. Please try again.";
        public const string BLOB_OPTION_HAS_INERNET_ADDR = "Internet address is not allowed for the picture opion type. Only the picture store resource " +
            "identifier is permitted.";
        public const string FILE_TYPE_NOT_ACCEPTED = "The specified file that you've uploaded is not accepted. Only .gif, .png, .bmp, .jpg, "
                                                     + ".jpeg are the accepted image file formats for image option.";
        public const string PIC_FILE_EXCEEDS_DIMENSION = "The specified picture file exceeds the maximum width of 500px or height of 500px or both. " 
                                                         + "Please try again. ";
        public const string ENTERED_USERNAME_INVALID = "The username that you've entered is invalid. Please try again. ";
        public const string ENTERED_PASSWORD_INVALID = "The password that you've entered is invalid. Please try again. ";

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

        public static readonly string[] QNS_OPTIONS_MODEL_KEYS = { "option_1", "option_2", "option_3", "option_4" };
        public static readonly string[] ACCOUNTS_MODEL_KEYS = { "user_name", "password" };
    }
}