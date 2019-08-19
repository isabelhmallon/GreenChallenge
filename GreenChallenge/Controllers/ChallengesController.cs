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
    public class ChallengesController : ApiController
    {
        private GreenChallengeContext db = new GreenChallengeContext();

        // GET: api/Challenges
        public IQueryable<Challenge> GetChallenges()
        {
            return db.Challenges.Include("tasks");
        }

        // GET: api/Challenges/5
        [ResponseType(typeof(Challenge))]
        public IHttpActionResult GetChallenge(int id)
        {
            Challenge challenge = db.Challenges.Find(id);
            if (challenge == null)
            {
                return NotFound();
            }

            return Ok(challenge);
        }

        // PUT: api/Challenges/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChallenge(int id, Challenge challenge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != challenge.id)
            {
                return BadRequest();
            }

            db.Entry(challenge).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeExists(id))
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

        // POST: api/Challenges
        [ResponseType(typeof(Challenge))]
        public IHttpActionResult PostChallenge(Challenge challenge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Challenges.Add(challenge);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = challenge.id }, challenge);
        }

        // DELETE: api/Challenges/5
        [ResponseType(typeof(Challenge))]
        public IHttpActionResult DeleteChallenge(int id)
        {
            Challenge challenge = db.Challenges.Find(id);
            if (challenge == null)
            {
                return NotFound();
            }

            db.Challenges.Remove(challenge);
            db.SaveChanges();

            return Ok(challenge);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChallengeExists(int id)
        {
            return db.Challenges.Count(e => e.id == id) > 0;
        }
    }
}