using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Grademeter_Assignment1.Models;

namespace Grademeter_Assignment1.Controllers
{
    public class GradesController : Controller
    {
        //private GrademeterModel db = new GrademeterModel();
        private IGradesMock db;

        public GradesController()
        {
            this.db = new EFGrades();
        }
        public GradesController(IGradesMock gradesmock)
        {
            this.db = gradesmock;
        }

        [AllowAnonymous]
        // GET: Grades
        public ActionResult Index()
        {
            var grades = db.Grades.ToList();

            return View("Index",grades);
        }

        // GET: Grades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
            }
            Grade grade = db.Grades.SingleOrDefault(x=>x.GradeID==id);
            if (grade == null)
            {
                return View("Error");
            }
            return View("Details",grade);
        }

        [Authorize]
        // GET: Grades/Create
        public ActionResult Create()
        {
            return View("Create");
        }


        // POST: Grades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GradeID,GradeName,Section,Remarks")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Save(grade);
                return RedirectToAction("Index");
            }

            return View("Create",grade);
        }

        [Authorize]
        // GET: Grades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Grade grade = db.Grades.SingleOrDefault(x=>x.GradeID==id);
            if (grade == null)
            {
                return View("Error");
            }
            return View("Edit",grade);
        }

        //POST: Grades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradeID,GradeName,Section,Remarks")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Save(grade);
                return RedirectToAction("Index");
            }
            return View("Edit",grade);
        }

        [Authorize]
        // GET: Grades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Grade grade = db.Grades.SingleOrDefault(x=>x.GradeID==id);
            if (grade == null)
            {
                return View("Error");
            }
            return View("Delete",grade);
        }

        //// POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return View("Error");

            }
            var grade = db.Grades.SingleOrDefault(x => x.GradeID == id);
            if (grade == null)
            {
                return View("Error");

            }
            db.Delete(grade);

            return RedirectToAction("Index");
        }

       
    }
}
