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
            return View();
        }

        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Questions questions)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(questions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.option_type = new SelectList(db.OptionTypes, "optiontype", "optiontype", questions.option_type);
            ViewBag.question_type = new SelectList(db.QuestionTypes, "questiontype", "questiontype", questions.question_type);
            ViewBag.station_id = new SelectList(db.Stations, "station_id", "station_name", questions.station_id);
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
