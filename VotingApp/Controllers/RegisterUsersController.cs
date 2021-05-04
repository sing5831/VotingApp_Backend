using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RegisterDataAccess;

namespace VotingApp.Controllers
{
    public class RegisterUsersController : Controller
    {
        private RegisterDataAccess.votingdbEntities db = new RegisterDataAccess.votingdbEntities();

        // GET: RegisterUsers
        public ActionResult Index()
        {
            return View(db.RegisterUsers.ToList());
        }

        // GET: RegisterUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterUser registerUser = db.RegisterUsers.Find(id);
            if (registerUser == null)
            {
                return HttpNotFound();
            }
            return View(registerUser);
        }

        // GET: RegisterUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_Id,Email,User_Password,VoterId,PIN,DeviceId,Voting_Status")] RegisterUser registerUser)
        {
            if (ModelState.IsValid)
            {
                db.RegisterUsers.Add(registerUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registerUser);
        }

        // GET: RegisterUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterUser registerUser = db.RegisterUsers.Find(id);
            if (registerUser == null)
            {
                return HttpNotFound();
            }
            return View(registerUser);
        }

      
        // GET: RegisterUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterUser registerUser = db.RegisterUsers.Find(id);
            if (registerUser == null)
            {
                return HttpNotFound();
            }
            return View(registerUser);
        }

        // POST: RegisterUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterUser registerUser = db.RegisterUsers.Find(id);
            db.RegisterUsers.Remove(registerUser);
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
