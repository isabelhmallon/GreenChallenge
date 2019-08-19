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
    public class UserTaskLogsController : ApiController
    {
        private GreenChallengeContext db = new GreenChallengeContext();

        // GET: api/UserTaskLogs
        public IQueryable<UserTaskLog> GetUserTaskLogs()
        {
            return db.UserTaskLogs;//.Include("challengeTask");
        }

        // GET: api/UserTaskLogs/5
        [ResponseType(typeof(UserTaskLog))]
        public IHttpActionResult GetUserTaskLog(int id)
        {
            UserTaskLog userTaskLog = db.UserTaskLogs.Find(id);
            if (userTaskLog == null)
            {
                return NotFound();
            }

            return Ok(userTaskLog);
        }

        // PUT: api/UserTaskLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserTaskLog(int id, UserTaskLog userTaskLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userTaskLog.id)
            {
                return BadRequest();
            }

            db.Entry(userTaskLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTaskLogExists(id))
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

        // POST: api/UserTaskLogs
        [ResponseType(typeof(UserTaskLog))]
        public IHttpActionResult PostUserTaskLog(UserTaskLog userTaskLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserTaskLogs.Add(userTaskLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userTaskLog.id }, userTaskLog);
        }

        // DELETE: api/UserTaskLogs/5
        [ResponseType(typeof(UserTaskLog))]
        public IHttpActionResult DeleteUserTaskLog(int id)
        {
            UserTaskLog userTaskLog = db.UserTaskLogs.Find(id);
            if (userTaskLog == null)
            {
                return NotFound();
            }

            db.UserTaskLogs.Remove(userTaskLog);
            db.SaveChanges();

            return Ok(userTaskLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTaskLogExists(int id)
        {
            return db.UserTaskLogs.Count(e => e.id == id) > 0;
        }
    }
}