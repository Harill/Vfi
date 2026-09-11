
using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using Vfi.Ui.Mvc.Vfi.IoC;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using System.Web;


namespace Vfi.Ui.Mvc.Vfi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "MenuRoute", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Menu", action = "MainMenu", id = UrlParameter.Optional } // Parameter defaults
            );


            routes.MapRoute(
               "excel_file_upload_o_day_ne_dcm", // Route name
               "{controller}/{action}", // URL with parameters
               new { controller = "User", action = "Save" } // Parameter defaults //UserController
           );

        }

        protected void Application_Start()
        {
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
            AreaRegistration.RegisterAllAreas();

            // Add custom validation attributes.
            //DataAnnotationsModelValidatorProvider.RegisterAdapter(
            //    typeof(EqualToPropertyAttribute),
            //    typeof(EqualToPropertyValidator));

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            IUnityContainer unityContainer = new UnityContainer();
            var container = IoCComponents.ConfigureUnity(unityContainer);
            //var factory = new UnityControllerFactory(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("vi-VN");
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            //const string culture = "vi-VN";
            //CultureInfo ci = CultureInfo.GetCultureInfo(culture);

            //Thread.CurrentThread.CurrentCulture = ci;
            //Thread.CurrentThread.CurrentUICulture = ci;

            // 08/08/20026
            var ci = new CultureInfo("vi-VN");

            //HttpCookie langCookie = HttpContext.Current.Request.Cookies["CurrentCulture"];
            //string cultureName = langCookie != null ? langCookie.Value : "en-US";
            //var ci = new CultureInfo(cultureName);


            Thread.CurrentThread.CurrentUICulture = ci;

            var ci2 = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci2;
        }

        // 10/08/2026
        //protected void Application_AcquireRequestState(object sender, EventArgs e) {
        //    string cultureName = (string)Session["CurrentCulture"] ?? "en-US";
        //    var ci = new CultureInfo(cultureName);
        //    Thread.CurrentThread.CurrentUICulture = ci;

        //    var ci2 = new CultureInfo("en-US");
        //    Thread.CurrentThread.CurrentCulture = ci2;
        //}


    }
}