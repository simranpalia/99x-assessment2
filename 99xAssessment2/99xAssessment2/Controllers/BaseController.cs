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
    }
}