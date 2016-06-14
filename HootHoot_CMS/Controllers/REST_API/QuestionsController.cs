using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HootHoot_CMS.DAL;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.Controllers.REST_API
{
    public class QuestionsController : ApiController
    {
        private HootHootDbContext db = new HootHootDbContext();
        private QuestionsDataGateway qdg = new QuestionsDataGateway();

        // GET: api/Questions
        public IEnumerable<Questions> GetQuestions()
        {
            //return db.Questions;
            return qdg.SelectAll();
        }

        // GET: api/Questions/5
        public IEnumerable<Questions> GetQuestions(int station_ID)
        {
            /*List<Questions> stationQns = db.Questions
                .Where(qns => qns.station_ID.Equals(station_ID)).Take(10)
                .ToList();


            return stationQns;*/
            return qdg.SelectByStationID(station_ID);
        }

        // GET: api/Questions/5
        /*[ResponseType(typeof(Questions))]
        public IHttpActionResult GetQuestions(int id)
        {
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }*/

        // PUT: api/Questions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestions(int id, Questions questions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questions.question_id)
            {
                return BadRequest();
            }

            db.Entry(questions).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Questions
        [ResponseType(typeof(Questions))]
        public IHttpActionResult PostQuestions(Questions questions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Questions.Add(questions);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = questions.question_id }, questions);
        }

        // DELETE: api/Questions/5
        [ResponseType(typeof(Questions))]
        public IHttpActionResult DeleteQuestions(int id)
        {
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return NotFound();
            }

            db.Questions.Remove(questions);
            db.SaveChanges();

            return Ok(questions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionsExists(int id)
        {
            return db.Questions.Count(e => e.question_id == id) > 0;
        }
    }
}