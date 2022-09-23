using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace _99xAssessment2.Controllers
{
    public class BaseController : Controller
    {
        public string UserId => Request.IsAuthenticated ? Request.GetOwinContext().Authentication.User.Identity.GetUserId() : string.Empty;
    }
}