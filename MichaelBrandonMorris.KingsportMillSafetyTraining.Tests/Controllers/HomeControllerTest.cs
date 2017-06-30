using System.Diagnostics;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Tests.Controllers
{
    /// <summary>
    ///     Class HomeControllerTest.
    /// </summary>
    /// TODO Edit XML Comment Template for HomeControllerTest
    [TestClass]
    public class HomeControllerTest
    {
        /// <summary>
        ///     Abouts this instance.
        /// </summary>
        /// TODO Edit XML Comment Template for About
        [TestMethod]
        public void About()
        {
            var controller = new HomeController();
            var result = controller.About() as ViewResult;
            Debug.Assert(result != null, "result != null");

            Assert.AreEqual(
                "Your application description page.",
                result.ViewBag.Message);
        }

        /// <summary>
        ///     Contacts this instance.
        /// </summary>
        /// TODO Edit XML Comment Template for Contact
        [TestMethod]
        public void Contact()
        {
            var controller = new HomeController();
            var result = controller.Contact() as ViewResult;
            Assert.IsNotNull(result);
        }

        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// TODO Edit XML Comment Template for Index
        [TestMethod]
        public void Index()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}