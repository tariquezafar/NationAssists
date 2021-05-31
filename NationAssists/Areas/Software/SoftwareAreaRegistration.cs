using System.Web.Mvc;

namespace NationAssists.Areas.Software
{
    public class SoftwareAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Software";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Software_default",
                "Software/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}