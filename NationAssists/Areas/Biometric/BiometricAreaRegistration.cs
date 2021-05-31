using System.Web.Mvc;

namespace NationAssists.Areas.Biometric
{
    public class BiometricAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Biometric";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Biometric_default",
                "Biometric/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}