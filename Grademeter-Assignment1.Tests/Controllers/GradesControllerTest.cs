using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//Added Reference To Web Project Controllers
using Grademeter_Assignment1.Controllers;
using System.Web.Mvc;
using Moq;
using Grademeter_Assignment1.Models;
using System.Collections.Generic;
using System.Linq;

namespace Grademeter_Assignment1.Tests.Controllers
{
    [TestClass]
    public class GradesControllerTest
    {
        Mock<IGradesMock> mock;
        List<Grade> grades;
        GradesController controller;
        [TestInitialize]
        public void TestInitialize()
        {

            mock = new Mock<IGradesMock>();

            grades = new List<Grade>
            {
                new Grade{ GradeID=1001, GradeName="A", Section="B-21", Remarks="Good Work"  },
                new Grade{ GradeID=1002, GradeName="A+", Section="B-21", Remarks="Great Work"  }
            };
            mock.Setup(m => m.Grades).Returns(grades.AsQueryable());
            controller = new GradesController(mock.Object);
        }
        #region Unit Test Cases For /Grades/ Index Views 
        [TestMethod]
        public void IndexReturnsView()
        {
         
            ViewResult result=controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }
        [TestMethod]
        public void IndexReturnsGrades()
        {
            var actual= (List<Grade>)((ViewResult)controller.Index()).Model;
            CollectionAssert.AreEqual(grades, actual);
        }
        #endregion

        #region Unit Test Cases For /Grades/Details 
        [TestMethod]
        public void Details_NoId_LoadsError()
        {
            ViewResult result = (ViewResult)controller.Details(null);
            Assert.AreEqual("Error", result.ViewName);
        }
        [TestMethod]
        public void Details_InvalidId_LoadsError()
        {
            ViewResult result = (ViewResult)controller.Details(2000);
            Assert.AreEqual("Error", result.ViewName);
        }
        [TestMethod]
        public void Details_ValidId_LoadsView()
        {
            ViewResult result = (ViewResult)controller.Details(1001);
            Assert.AreEqual("Details", result.ViewName);
        }
        [TestMethod]
        public void Details_ValidId_LoadsGrades()
        {
            Grade result = (Grade)((ViewResult)controller.Details(1001)).Model;
            Assert.AreEqual(grades[0], result);

        }
        #endregion

        [TestMethod]
        public void Edit_NoId()
        {
            int? id = null;
            var result = (ViewResult)controller.Edit(id);
            Assert.AreEqual("Error", result.ViewName);
        }
        [TestMethod]
        public void Edit_ViewLoads()
        {
            ViewResult actual = (ViewResult)controller.Edit(1001);
            Assert.AreEqual("Edit", actual.ViewName);
        }
        [TestMethod]
        public void Edit_LoadsGrades()
        {
            Grade actual = (Grade)((ViewResult)controller.Edit(1001)).Model;
            Assert.AreEqual(grades[0], actual);

        }





        [TestMethod]
        public void Create_ViewLoads()
        {
            var result=(ViewResult)controller.Create();
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void Delete_NoId()
        {
            var result = (ViewResult)controller.Delete(null);
            Assert.AreEqual("Error", result.ViewName);

        }

        [TestMethod]
        public void Delete_InvalidId()
        {
            var result = (ViewResult)controller.Delete(2000);
            Assert.AreEqual("Error", result.ViewName);

        }
        [TestMethod]
        public void Delete_ValidId_LoadsView()
        {
            var result = (ViewResult)controller.Delete(1001);
            Assert.AreEqual("Delete", result.ViewName);

        }
        [TestMethod]
        public void Delete_ValidId_LoadsModel()
        {
            Grade actual = (Grade)((ViewResult)controller.Delete(1001)).Model;
            Assert.AreEqual(grades[0],actual);

        }

        [TestMethod]
        public void EditPostGrade_LoadsIndex()
        {
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(grades[0]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EditPost_InvalidLoadView()
        {
            Grade id = new Grade { GradeID = 2001 };
            controller.ModelState.AddModelError("Error", "It'll Not Save");
            ViewResult result = (ViewResult)controller.Edit(id);
            Assert.AreEqual("Edit", result.ViewName);

        }

        [TestMethod]
        public void EditPost_InvalidLoadGrade()
        {
            Grade id = new Grade { GradeID = 1001 };
            controller.ModelState.AddModelError("Error", "It'll Not Save");
            Grade result = (Grade)((ViewResult)controller.Edit(id)).Model;
            Assert.AreEqual(id, result);

        }

        [TestMethod]
        public void Create_ValidGrade()
        {
            Grade newGrade = new Grade
            {
                GradeID = 1003,
                GradeName = "B",
                Section = "B21",
                Remarks = "Average"
            };
             RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(newGrade);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Create_InValidGrade()
        {
            Grade newGrade = new Grade();
            controller.ModelState.AddModelError("Unable To Create Grade", "Grade Creation Exception");
            ViewResult result = (ViewResult)controller.Create(newGrade);
            Assert.AreEqual("Create", result.ViewName);
        }
    }
}
