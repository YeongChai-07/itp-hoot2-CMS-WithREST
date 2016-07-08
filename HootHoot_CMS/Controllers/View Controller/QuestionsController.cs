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
            string[] optionValues_Arr = null;
            bool[] containsBlob_Val = null;

            if (modelState_FirstPass)
            {
                is_ImageOptionType = questions.option_type == Constants.QNS_IMAGE_OPTION_TYPE;
                optionValues_Arr = new string[] { questions.option_1, questions.option_2, questions.option_3, questions.option_4 };
                containsBlob_Val = new bool[] 
                {
                    //Check and initialize each of the option field whether it has blob storage address format
                    optionValues_Arr[0].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    optionValues_Arr[1].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    optionValues_Arr[2].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    optionValues_Arr[3].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER)
                };

                checkHasBlobValue_Option(containsBlob_Val, optionValues_Arr, is_ImageOptionType);

            }

            // Perform second pass ModelState check ensure that model state is still valid 
            // NOTE: This is required as it is possible for picture option type to FAIL during the first check, 
            // this has no impact to TEXT option type checks, it will still remain TRUE
            if (ModelState.IsValid)
            {
                if (is_ImageOptionType)
                {
                    BlobManager picBlob = new BlobManager();
                    questions.option_1 = !containsBlob_Val[0] ? picBlob.uploadPictureToBlob(optionValues_Arr[0]) : optionValues_Arr[0];
                    questions.option_2 = !containsBlob_Val[1] ? picBlob.uploadPictureToBlob(optionValues_Arr[1]) : optionValues_Arr[1];
                    questions.option_3 = !containsBlob_Val[2] ? picBlob.uploadPictureToBlob(optionValues_Arr[2]) : optionValues_Arr[2];
                    questions.option_4 = !containsBlob_Val[3] ? picBlob.uploadPictureToBlob(optionValues_Arr[3]) : optionValues_Arr[3];
                }

                questionsGateway.Update(questions);
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

        private void checkHasBlobValue_Option(bool[] containsBlob_Val, string[] optionValues_Arr, bool isPict_Option )
        {
            
            for (byte i = 0; i < Constants.OPTIONS_PER_QNS; i++)
            {
                if (containsBlob_Val[i])
                {
                    if (isPict_Option && !FileHelper.checkFileExists_Blob(optionValues_Arr[i]))
                    {
                        ModelState.AddModelError(
                                    ModelState.Keys.Single(field => field == "option_" + (i + 1)),
                                    Constants.BLOB_PIC_NOT_FOUND);
                        //passCheck = false;
                    }
                    else if (!isPict_Option)
                    {
                        ModelState.AddModelError(
                                ModelState.Keys.Single(field => field == "option_" + (i + 1)),
                                Constants.TEXT_OPTION_HAS_BLOB_VALUE);
                        //passCheck = false;
                    } //End Else-If

                }
            }
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
