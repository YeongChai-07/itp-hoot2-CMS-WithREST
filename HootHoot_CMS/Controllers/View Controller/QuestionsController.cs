using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HootHoot_CMS.DAL;
using HootHoot_CMS.Models;
using HootHoot_CMS.Blobs;

namespace HootHoot_CMS.Controllers.View_Controller
{
    public class QuestionsController : Controller
    {
        private HootHootDbContext db = new HootHootDbContext();
        private QuestionsDataGateway questionsGateway = new QuestionsDataGateway();

        // GET: Questions
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.optionType).Include(q => q.questionType).Include(q => q.station);
            return View(questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Questions questions)
        {

            bool modelState_FirstPass = ModelState.IsValid; // First-Pass check of the ModelState Valid property
            bool is_ImageOptionType = false; // Initialized to false to avoid complications
            string[] listOfFiles = null;

            if (modelState_FirstPass)
            {
                is_ImageOptionType = questions.option_type == Constants.QNS_IMAGE_OPTION_TYPE;
                if (is_ImageOptionType)
                {
                    //Checks whether the specified file for each option exists in server
                    listOfFiles = new string[] { questions.option_1, questions.option_2, questions.option_3, questions.option_4 };

                    //iterate each item in listOfFiles to check whether the file exists in web server
                    for (byte i = 0; i < listOfFiles.Length; i++)
                    {

                        if (!FileHelper.checkFileExists_Server(listOfFiles[i]))
                        {
                            //DO NOT LET THE SUBMIT/UPLOAD GO THROUGH, STOP the upload IMMEDIATELY
                            ModelState.AddModelError(
                                    ModelState.Keys.Single(field => field == "option_" + (i + 1)),
                                    Constants.FILE_UPLOAD_NOT_FOUND);

                        }

                        //TODO: Include checks on the file type (extension) to ensure that it is an image


                    } // End FOR-Loop

                } // End question.option_type IF-Block

                // Do nothing if question.option_type != "IMAGE"


            } // End Model.IsValid() IF-Block

            // Perform second pass ModelState check ensure that model state is still valid 
            // NOTE: This is required as it is possible for picture option type to FAIL during the first check, 
            // this has no impact to TEXT option type checks, it will still remain TRUE
            if (ModelState.IsValid)
            {
                if (is_ImageOptionType)
                {
                    //Uploads and gets the blob URI string to each image option
                    BlobManager picBlob = new BlobManager();
                    questions.option_1 = picBlob.uploadPictureToBlob(listOfFiles[0]);
                    questions.option_2 = picBlob.uploadPictureToBlob(listOfFiles[1]);
                    questions.option_3 = picBlob.uploadPictureToBlob(listOfFiles[2]);
                    questions.option_4 = picBlob.uploadPictureToBlob(listOfFiles[3]);
                }

                questionsGateway.Insert(questions);

                //Assume : All image options files were uploaded to Azure Blob SUCCESSFULLY.
                FileHelper.deletesALLUploadFiles(); //Deletes all pictures from the server upload folder

                return RedirectToAction("Index");
            }

            return View(questions);

        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }

            ViewBag.correct_option = Constants.customCorrectOption_List(questions.correct_option);

            return View(questions);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Questions questions)
        {
            bool modelState_FirstPass = ModelState.IsValid; // First-Pass check of the ModelState Valid property
            bool is_ImageOptionType = false; // Initialized to false to avoid complications

            if (modelState_FirstPass)
            {
                is_ImageOptionType = questions.option_type == Constants.QNS_IMAGE_OPTION_TYPE;
                string[] listOfFiles = new string[] { questions.option_1, questions.option_2, questions.option_3, questions.option_4 };
                bool[] containsBlob_Val = new bool[] 
                {
                    //Check and initialize each of the option field whether it has blob storage address format
                    listOfFiles[0].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    listOfFiles[1].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    listOfFiles[2].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    listOfFiles[3].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER)
                };

                
                if(is_ImageOptionType)
                {
                    for (byte i = 0; i < Constants.OPTIONS_PER_QNS; i++)
                    {
                        if (containsBlob_Val[i])
                        {
                            if (!FileHelper.checkFileExists_Blob(listOfFiles[i]))
                            {

                            }

                        }
                    }
                } // End of is_ImageOptionType IF-Block
                

                db.Entry(questions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.correct_option = Constants.customCorrectOption_List(questions.correct_option);
            return View(questions);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Questions questions = db.Questions.Find(id);
            db.Questions.Remove(questions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
