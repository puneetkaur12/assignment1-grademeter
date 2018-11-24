using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grademeter_Assignment1.Models
{
    public class EFGrades : IGradesMock
    {
        private GrademeterModel db = new GrademeterModel();

        public IQueryable<Grade> Grades { get { return db.Grades;  } }

       public  void Delete(Grade grade)
        {
            db.Grades.Remove(grade);
            db.SaveChanges();

        }

        public Grade Save(Grade grade)
        {
            if (grade.GradeID == 0)
            {
                db.Grades.Add(grade);
            }
            else
            {
                db.Entry(grade).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return grade;
        }
    }
}