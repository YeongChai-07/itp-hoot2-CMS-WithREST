using System.Web.Mvc;

namespace HootHoot_CMS.Controllers
{
    public class FileUploadController : Controller
    {
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