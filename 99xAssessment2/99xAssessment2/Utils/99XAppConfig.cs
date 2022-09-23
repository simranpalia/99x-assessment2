using System.Configuration;

namespace _99xAssessment2.Utils
{
    public static class _99XAppConfig
    {
        
        public static string SuperAdminPwd => ConfigurationManager.AppSettings["SuperAdminPwd"];
        
    }
}