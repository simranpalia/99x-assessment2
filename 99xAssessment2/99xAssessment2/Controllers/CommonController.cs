using System.Web.Mvc;
using _99xAssessment2.Repository;

namespace _99xAssessment2.Controllers
{
    [Authorize]
    public class CommonController : BaseController
    {
        // GET: Common
        [HttpGet]
        public ActionResult BalanceReport()
        {
            return View(DAL.GetAccounts());
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}