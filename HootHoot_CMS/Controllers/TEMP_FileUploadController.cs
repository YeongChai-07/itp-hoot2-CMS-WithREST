﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace HootHoot_CMS.Controllers
{
    public class TEMP_FileUploadController : Controller
    {
        TEMP_FilesHelper filesHelper;
        String tempPath = "~/somefiles/";
        String serverMapPath = "~/Upload";
        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }
        private string UrlBase = "/Upload/";
        String DeleteURL = "/Upload/DeleteFile/?file=";
        String DeleteType = "GET";
        public TEMP_FileUploadController()
        {
           filesHelper = new TEMP_FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, serverMapPath, serverMapPath);
        }
      
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Show()
        {
            JsonFiles ListOfFiles = filesHelper.GetFileList();
            var model = new FilesViewModel()
            {
                Files = ListOfFiles.files
            };
          
            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var resultList = new List<ViewDataUploadFilesResult>();
           
            var CurrentContext = HttpContext;

            filesHelper.UploadAndShowResults(CurrentContext, resultList);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            else
            {
                return Json(files);
            }
        }
        public JsonResult GetFileList()
        {
            var list=filesHelper.GetFileList();
            return Json(list,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteFile(string file)
        {
            filesHelper.DeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
       
    }

    class FilesViewModel
    {
        public ViewDataUploadFilesResult[] Files { get; set; }
    }
}