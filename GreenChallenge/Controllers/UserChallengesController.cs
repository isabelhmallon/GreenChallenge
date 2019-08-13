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
    public class UserChallengesController : ApiController
    {
        private GreenChallengeContext db = new GreenChallengeContext();

        // GET: api/UserChallenges
        public IQueryable<UserChallenge> GetUserChallenges()
        {
            return db.UserChallenges;
        }

        // GET: api/UserChallenges/5
        [ResponseType(typeof(UserChallenge))]
        public IHttpActionResult GetUserChallenge(int id)
        {
            UserChallenge userChallenge = db.UserChallenges.Find(id);
            if (userChallenge == null)
            {
                return NotFound();
            }

            return Ok(userChallenge);
        }

        // PUT: api/UserChallenges/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserChallenge(int id, UserChallenge userChallenge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userChallenge.Id)
            {
                return BadRequest();
            }

            db.Entry(userChallenge).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserChallengeExists(id))
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

        // POST: api/UserChallenges
        [ResponseType(typeof(UserChallenge))]
        public IHttpActionResult PostUserChallenge(UserChallenge userChallenge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserChallenges.Add(userChallenge);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userChallenge.Id }, userChallenge);
        }

        // DELETE: api/UserChallenges/5
        [ResponseType(typeof(UserChallenge))]
        public IHttpActionResult DeleteUserChallenge(int id)
        {
            UserChallenge userChallenge = db.UserChallenges.Find(id);
            if (userChallenge == null)
            {
                return NotFound();
            }

            db.UserChallenges.Remove(userChallenge);
            db.SaveChanges();

            return Ok(userChallenge);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserChallengeExists(int id)
        {
            return db.UserChallenges.Count(e => e.Id == id) > 0;
        }
    }
}