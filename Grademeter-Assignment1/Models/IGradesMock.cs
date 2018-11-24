using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grademeter_Assignment1.Models
{
    public interface IGradesMock
    {
        IQueryable<Grade> Grades { get;}

        Grade Save(Grade grade);
        void Delete(Grade grade);



    }
}
