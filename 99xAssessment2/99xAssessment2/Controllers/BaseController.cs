using System.Linq;
using System.Web;
using System.Web.Mvc;
using _99xAssessment2.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace _99xAssessment2.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Role = UserRole;
            base.OnActionExecuting(filterContext);
        }

        public string UserId => Request.IsAuthenticated ? Request.GetOwinContext().Authentication.User.Identity.GetUserId() : string.Empty;

        public long AdminId
        {
            get
            {
                if (Request.IsAuthenticated)
                {
                    if (Request.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(UserId).Contains(Utils.Constants.RoleSuperUser))
                    {
                        var matchedUser = DAL.GetUsers().FirstOrDefault(x => x.Username == Request.GetOwinContext().Authentication.User.Identity.Name);
                        if (matchedUser != null)
                            return matchedUser.Id;
                    }
                }

                return 0;
            }
        }

        public string UserRole
        {
            get
            {
                if (Request.IsAuthenticated)
                {
                    if (Request.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(UserId).Contains(Utils.Constants.RoleSuperUser))
                    {
                        return Utils.Constants.RoleSuperUser;
                    }
                    if (Request.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(UserId).Contains(Utils.Constants.RoleAdmin))
                    {
                        return Utils.Constants.RoleAdmin;
                    }
                }

                return null;
            }
        }
    }
}