using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//Added Reference To Web Project Controllers
using Grademeter_Assignment1.Controllers;
using System.Web.Mvc;

namespace Grademeter_Assignment1.Tests.Controllers
{
    [TestClass]
    public class GradesControllerTest
    {
        [TestMethod]
        public void IndexReturnsView()
        {
            GradesController controller = new GradesController();
            ViewResult result=controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
