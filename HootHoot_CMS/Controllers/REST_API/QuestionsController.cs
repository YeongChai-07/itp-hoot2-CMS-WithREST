/*using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Description;
*/

using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using HootHoot_CMS.DAL;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.Controllers.REST_API
{
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class QuestionsController : ApiController
    {
        private QuestionsDataGateway qnsGateway = new QuestionsDataGateway();

        // GET: api/Questions
        public IEnumerable<Questions> GetQuestions()
        {
            //return db.Questions;
            return qnsGateway.SelectAll();
        }

        // GET: api/Questions/5
        public IEnumerable<Questions> GetQuestions(string station_ID)
        {
            return qnsGateway.SelectByStationID(station_ID);
        }


        // COMMENTED OUT Codes (NOT IN USE CODES)

        /* private HootHootDbContext db = new HootHootDbContext();

        // GET: api/Questions/5
        /[ResponseType(typeof(Questions))]
        public IHttpActionResult GetQuestions(int id)
        {
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }

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

        */
    }
}