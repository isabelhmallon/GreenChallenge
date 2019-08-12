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
using GreenChallenge.Models;

namespace GreenChallenge.Controllers
{
    public class ChallengeTasksController : ApiController
    {
        private GreenChallengeContext db = new GreenChallengeContext();

        // GET: api/ChallengeTasks
        public IQueryable<ChallengeTask> GetChallengeTasks()
        {
            return db.ChallengeTasks;
        }

        // GET: api/ChallengeTasks/5
        [ResponseType(typeof(ChallengeTask))]
        public IHttpActionResult GetChallengeTask(int id)
        {
            ChallengeTask challengeTask = db.ChallengeTasks.Find(id);
            if (challengeTask == null)
            {
                return NotFound();
            }

            return Ok(challengeTask);
        }

        // PUT: api/ChallengeTasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChallengeTask(int id, ChallengeTask challengeTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != challengeTask.id)
            {
                return BadRequest();
            }

            db.Entry(challengeTask).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeTaskExists(id))
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

        // POST: api/ChallengeTasks
        [ResponseType(typeof(ChallengeTask))]
        public IHttpActionResult PostChallengeTask(ChallengeTask challengeTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChallengeTasks.Add(challengeTask);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = challengeTask.id }, challengeTask);
        }

        // DELETE: api/ChallengeTasks/5
        [ResponseType(typeof(ChallengeTask))]
        public IHttpActionResult DeleteChallengeTask(int id)
        {
            ChallengeTask challengeTask = db.ChallengeTasks.Find(id);
            if (challengeTask == null)
            {
                return NotFound();
            }

            db.ChallengeTasks.Remove(challengeTask);
            db.SaveChanges();

            return Ok(challengeTask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChallengeTaskExists(int id)
        {
            return db.ChallengeTasks.Count(e => e.id == id) > 0;
        }
    }
}