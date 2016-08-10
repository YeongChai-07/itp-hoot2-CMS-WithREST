using System.Web.Mvc;

namespace HootHoot_CMS.Controllers
{
    /* FileUploadController class : Codes were REFERENCED and RE-WORKED for the requirements 
    *  to handle HTTP request from ajax for the uploading of images (questions options) of both the
    *  "Create" and "Edit" question views for the CMS.
    *  
    * Referenced Source: Simple implementation of Blueimp jQuery File Upload Plugin using ASP.NET MVC5
    * Original Author: Daniel Kalinowski 
    * Available via GitHub:  https://github.com/TheKalin/jQuery-File-Upload.MVC5
    * 
    * All Credits goes to the Original Author.
    */
    public class FileUploadController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload()
        {
            var fileToUpload = HttpContext.Request.Files[0];

            FileHelper.FileInfo fileInfo_Obj = FileHelper.uploadFileToServer_Result(fileToUpload);

            if(!fileInfo_Obj.m_FileStats)
            {
                return Json("Error ");
            }
            return Json(new FileHelper.FileInfo[] { fileInfo_Obj });
        }
    }
}