using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElectionStartEndDataAccess;

namespace VotingApp.Controllers
{
    public class ElectionTimeController : Controller
    {
        private startenddbEntities db = new startenddbEntities();

        // GET: ElectionTime
        public ActionResult Index()
        {
            return View(db.ElectionStartEnds.ToList());
        }

        // GET: ElectionTime/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectionStartEnd electionStartEnd = db.ElectionStartEnds.Find(id);
            if (electionStartEnd == null)
            {
                return HttpNotFound();
            }
            return View(electionStartEnd);
        }

        // GET: ElectionTime/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ElectionTime/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StartDate,EndDate")] ElectionStartEnd electionStartEnd)
        {
            if (ModelState.IsValid)
            {
                db.ElectionStartEnds.Add(electionStartEnd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(electionStartEnd);
        }

        // GET: ElectionTime/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectionStartEnd electionStartEnd = db.ElectionStartEnds.Find(id);
            if (electionStartEnd == null)
            {
                return HttpNotFound();
            }
            return View(electionStartEnd);
        }


        // GET: ElectionTime/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectionStartEnd electionStartEnd = db.ElectionStartEnds.Find(id);
            if (electionStartEnd == null)
            {
                return HttpNotFound();
            }
            return View(electionStartEnd);
        }

        // POST: ElectionTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ElectionStartEnd electionStartEnd = db.ElectionStartEnds.Find(id);
            db.ElectionStartEnds.Remove(electionStartEnd);
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
