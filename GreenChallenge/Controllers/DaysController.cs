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
    public class DaysController : ApiController
    {
        private GreenChallengeContext db = new GreenChallengeContext();

        // GET: api/Days
        public IQueryable<Day> GetDays()
        {
            return db.Days.Include(d => d.tasksCompleted);
        }

        // GET: api/Days/5
        [ResponseType(typeof(Day))]
        public IHttpActionResult GetDay(int id)
        {
            Day day = db.Days.Include(d => d.tasksCompleted).FirstOrDefault(d => d.id == id);
           
            if (day == null)
            {
                return NotFound();
            }

            return Ok(day);
        }

        // PUT: api/Days/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDay(int id, Day day)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != day.id)
            {
                return BadRequest();
            }

            //day.dayCompleted = DayCompleted(day);

            db.Entry(day).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayExists(id))
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

        // POST: api/Days
        [ResponseType(typeof(Day))]
        public IHttpActionResult PostDay(Day day)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //day.dayCompleted = DayCompleted(day);
            db.Days.Add(day);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = day.id }, day);
        }

        // DELETE: api/Days/5
        [ResponseType(typeof(Day))]
        public IHttpActionResult DeleteDay(int id)
        {
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return NotFound();
            }

            db.Days.Remove(day);
            db.SaveChanges();

            return Ok(day);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DayExists(int id)
        {
            return db.Days.Count(e => e.id == id) > 0;
        }

        //private bool DayCompleted(Day day)
        //{
        //    UserChallenge userChallenge = db.UserChallenges.Include(uc => uc.challenge.tasks)
        //           .FirstOrDefault(uc => uc.id == day.userChallengeId);

        //    Challenge challenge = userChallenge.challenge;

        //    List<ChallengeTask> tasks = challenge.tasks.ToList();

        //    var completedChallengeTaskIds = new List<int>();

        //    day.tasksCompleted.ToList().ForEach(task => completedChallengeTaskIds.Add(task.challengeTaskId));

        //    var dayCompleted = true;
        //    tasks.ForEach(task =>
        //    {
        //        if (!completedChallengeTaskIds.Contains(task.id))
        //        {
        //            dayCompleted = false;
        //        }
        //    });


        //    return dayCompleted;
        //}
    }
}