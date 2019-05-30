using System;
using System.Web.Mvc;
using AteliwareGitHub2.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AteliwareGitHubUnitTeste
{
    [TestClass]
    public class GitHubControllerTest
    {
        [TestMethod]
        public void IndexView()
        {
            var controller = new GitHubController();
            var result = controller.Index() as ActionResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DetalhesView()
        {
            var controller = new GitHubController();
            var result = controller.Detalhes(1) as ActionResult;
            Assert.IsNotNull(result);
        }
    }
}
