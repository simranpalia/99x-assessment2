using System.Web.Mvc;
using _99xAssessment2.Repository;
using _99xAssessment2.Utils;

namespace _99xAssessment2.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult UploadBalance()
        {
            var file = Request.Files[0];

            if (file != null)
            {
                DAL.ProcessAndUpdateAccountInfo(file, Request.Form["ddlYear"].ToInt(), Request.Form["ddlMonth"].ToInt(),
                    AdminId);
            }

            return RedirectToAction("Index");
        }

    }
}