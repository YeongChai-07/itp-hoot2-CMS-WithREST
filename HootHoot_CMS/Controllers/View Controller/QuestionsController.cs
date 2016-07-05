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

namespace HootHoot_CMS.Controllers.View_Controller
{
    public class QuestionsController : Controller
    {
        private HootHootDbContext db = new HootHootDbContext();

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
            ViewBag.option_type = new SelectList(db.OptionTypes, "optiontype", "optiontype");
            ViewBag.question_type = new SelectList(db.QuestionTypes, "questiontype", "questiontype");
            ViewBag.station_id = new SelectList(db.Stations, "station_id", "station_name");

            List<SelectListItem> ddl_correctOption = new List<SelectListItem>();
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 1", Value = "option_1" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 2", Value = "option_2" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 3", Value = "option_3" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 4", Value = "option_4" });

            ViewBag.correct_option = ddl_correctOption;

            return View();
        }

        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Questions questions)
        {
            ViewBag.option_type = new SelectList(db.OptionTypes, "optiontype", "optiontype", questions.option_type);
            ViewBag.question_type = new SelectList(db.QuestionTypes, "questiontype", "questiontype", questions.question_type);
            ViewBag.station_id = new SelectList(db.Stations, "station_id", "station_name", questions.station_id);

            List<SelectListItem> ddl_correctOption = new List<SelectListItem>();
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 1", Value = "option_1" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 2", Value = "option_2" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 3", Value = "option_3" });
            ddl_correctOption.Add(new SelectListItem() { Text = "Option 4", Value = "option_4" });

            ViewBag.correct_option = ddl_correctOption;

            bool cpy_ModelStateValid = ModelState.IsValid; // Makes a copy of the ModelState Valid property

            if (ModelState.IsValid)
            {
                if(questions.option_type == Constants.QNS_IMAGE_OPTION_TYPE)
                {
                    //Checks whether the specified file for each option exists in server
                    string[] listOfFiles = { questions.option_1, questions.option_2, questions.option_3, questions.option_4 };

                    string faultyOptionNo = "";
                    //iterate each item in listOfFiles to check whether the file exists in web server
                    for(byte i=0;i < listOfFiles.Length; i++)
                    {
                        if ( !System.IO.File.Exists(Constants.UPLOAD_FOLDER_PATH + listOfFiles[i]) )
                        {
                            faultyOptionNo = "option_" + (i + 1);
                            //DO NOT LET THE SUBMIT/UPLOAD GO THROUGH, STOP the upload IMMEDIATELY
                            ModelState.AddModelError(
                                    ModelState.Keys.Single(field => field == faultyOptionNo),
                                    "The specified file for " + faultyOptionNo + " is not found. Perhaps the file " +
                                   "isn't uploaded correctly ?"
                                   );
                        }
                        
                    } // End FOR-Loop

                } // End question.option_type IF-Block

                //Check again to ensure that model state is still valid 
               // NOTE: This is required as it is possible for picture option type to FAIL during the first check, 
               // this has no impact to TEXT option type checks, it will still remain TRUE
                if (cpy_ModelStateValid && ModelState.IsValid)
                {
                    db.Questions.Add(questions);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            } // End Model.IsValid() IF-Block

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
            ViewBag.option_type = new SelectList(db.OptionTypes, "optiontype", "optiontype", questions.option_type);
            ViewBag.question_type = new SelectList(db.QuestionTypes, "questiontype", "questiontype", questions.question_type);
            ViewBag.station_id = new SelectList(db.Stations, "station_id", "station_name", questions.station_id);
            return View(questions);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Questions questions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.option_type = new SelectList(db.OptionTypes, "optiontype", "optiontype", questions.option_type);
            ViewBag.question_type = new SelectList(db.QuestionTypes, "questiontype", "questiontype", questions.question_type);
            ViewBag.station_id = new SelectList(db.Stations, "station_id", "station_name", questions.station_id);
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
