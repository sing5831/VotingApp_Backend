using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CandidateDataAccess;

namespace VotingApp.Controllers
{
    public class EditCandidatesController : Controller
    {
        private candidatedbEntities db = new candidatedbEntities();

        // GET: EditCandidates
        public ActionResult Index()
        {
            return View(db.Candidates.ToList());
        }

        // GET: EditCandidates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidates candidates = db.Candidates.Find(id);
            if (candidates == null)
            {
                return HttpNotFound();
            }
            return View(candidates);
        }

        // GET: EditCandidates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EditCandidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CandidateId,CandidateName")] Candidates candidates)
        {
            if (ModelState.IsValid)
            {
                db.Candidates.Add(candidates);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(candidates);
        }

        // GET: EditCandidates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidates candidates = db.Candidates.Find(id);
            if (candidates == null)
            {
                return HttpNotFound();
            }
            return View(candidates);
        }

        // GET: EditCandidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidates candidates = db.Candidates.Find(id);
            if (candidates == null)
            {
                return HttpNotFound();
            }
            return View(candidates);
        }

        // POST: EditCandidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidates candidates = db.Candidates.Find(id);
            db.Candidates.Remove(candidates);
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
