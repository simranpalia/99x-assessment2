using System.Web.Mvc;
using _99xAssessment2.Repository;
using _99xAssessment2.Utils;

namespace _99xAssessment2.Controllers
{
    [Authorize(Roles = Constants.RoleSuperUser)]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index(string message=null)
        {
            ViewBag.Message= message;
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
            if (Request.Files == null)
                return RedirectToAction("Index");

            var file = Request.Files[0];

            if (file != null)
            {
                if (file.FileName.Split('.')[1].Contains("xlsx"))
                {
                    DAL.ProcessAndUpdateAccountInfo(file, Request.Form["ddlYear"].ToInt(), Request.Form["ddlMonth"].ToInt(),
                        AdminId);
                }
                else
                    return RedirectToAction("Index", new { message = "File uploaded must be in .xlsx format." });
            }

            return RedirectToAction("Index");
        }

    }
}