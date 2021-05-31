using System.Web.Mvc;

namespace NationAssists.Areas.CardPrinters
{
    public class CardPrintersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CardPrinters";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CardPrinters_default",
                "CardPrinters/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}