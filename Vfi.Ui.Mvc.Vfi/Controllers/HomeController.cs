using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Vfi.Client.Module.Authentication.Interfaces;
//using Vfi.Server.Core.DataModel.Models.System;
using Microsoft.Practices.Unity;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
using LogInUserModel = Vfi.Server.Core.DataModel.Models.System.LogInUserModel;
using System.Threading;

namespace Vfi.Ui.Mvc.Vfi.Controllers {
    public class HomeController : Controller {
        private readonly IFormAuthenticationService _formAuthenticationService;
        private readonly IUserService _userService;
        [InjectionConstructor]
        public HomeController(IFormAuthenticationService formAuthenticationService, IUserService userService) {
            if (_userService == null) _userService = userService;
            if (_formAuthenticationService == null) _formAuthenticationService = formAuthenticationService;
        }
        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }
            return ViewData;
        }

        // 06/07/2026
        [HttpPost]
        public ActionResult SetLanguage(string lang) {
            Session["CurrentCulture"] = lang;
            return RedirectToAction("Index");
        }

        // 06/07/2026
        public ActionResult Index() {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            ViewData = GetPageConfigData();
            Session["CurrentCulture"] = "vi-VN";
            //Session["CurrentCulture"] = "en-US";
            string culture = (string)Session["CurrentCulture"] ?? "vi-VN";
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            //ViewData["BackgroundImage"] = Path.Combine(Server.MapPath("~/Content/Images"), "bg_body-2.jpg");
            return View();
        }

        [HttpPost]
        public ActionResult Index(LogInUserModel userModel, string startingDate, string returnUrl) {
            try {
                if (ModelState.IsValid) {
                    if (string.IsNullOrWhiteSpace(userModel.Username)) {
                        return Json(new LoginResultDto { Success = false, Message = "Username Not Empty" });
                    }
                    if (string.IsNullOrWhiteSpace(userModel.Password)) {
                        return Json(new LoginResultDto { Success = false, Message = "Password Not Empty" });
                    }

                    // ReSharper disable CSharpWarnings::CS0612
                    //var value = ConfigurationSettings.AppSettings["gkn"];
                    var value = ConfigurationManager.AppSettings["gkn"];
                    // ReSharper restore CSharpWarnings::CS0612
                    var loginStatus = _userService.ValidateUser(userModel.Username, userModel.Password, value);

                    if (loginStatus == "0") {
                        _formAuthenticationService.SignIn(userModel.Username, userModel.RememberMe);
                        //if (!String.IsNullOrEmpty(returnUrl))
                        //{
                        //    return Redirect(returnUrl);
                        //}

                        //return RedirectToAction("About", "Home");

                        if (Request.IsAjaxRequest()) {
                            return Json(new LoginResultDto { Success = true, Message = "Successfully logged in" }, JsonRequestBehavior.DenyGet);

                        }
                        return Json(new LoginResultDto { Success = false, Message = "Error - Wrong Username or Password" });
                        //return View(userModel);
                    }

                    switch (loginStatus) {
                        case "1":
                            return Json(new LoginResultDto { Success = false, Message = "Tài khoản chưa được kích hoạt hoặc không tồn tại." });
                        case "2":
                            return Json(new LoginResultDto { Success = false, Message = "Tên đăng nhập hoặc mật khẩu không đúng." });
                        case "3":
                            return Json(new LoginResultDto { Success = false, Message = "Lỗi kết nối hệ thống mạng.!" });
                        default:
                            return Json(new LoginResultDto { Success = false, Message = loginStatus });
                    }
                }

                // If we got this far, something failed, redisplay form
                return View(userModel);
            }
            catch (Exception exception) {
                return Json(new LoginResultDto { Success = false, Message = exception.Message });
                //return View();
            }
        }

        [Authorize]
        public ActionResult About() {
            return View();
        }

        public ActionResult NoAccess() {
            return View();
        }

        [Authorize]
        public ActionResult WindowLogOff() {
            _formAuthenticationService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOnUserControl() {
            var configData = GetPageConfigData();
            return PartialView("_LogOnUserControl", configData);
        }

        [GridAction]
        public ActionResult SelectMessage() {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SystemMessage>()));
            }
            var model = new List<SystemMessage>();
            try {
                using (var vfi = new tammaContext()) {
                    var production = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.Production2Management);
                    if (production) {
                        var productCount = vfi.Products.Count(p => (p.Length == 0 || p.Length == null) && p.Active);
                        if (productCount > 0) {
                            var message = new SystemMessage {
                                MessageType = "Kỹ thuật",
                                Message = "Chưa cập nhật chiều dài thiết kế",
                                Number = productCount
                            };
                            model.Add(message);
                        }
                        productCount = vfi.Products.Count(p => (p.KnifeCut == 0 || p.KnifeCut == null) && p.Active);
                        if (productCount > 0) {
                            var message = new SystemMessage {
                                MessageType = "Kỹ thuật",
                                Message = "Chưa cập nhật dao cắt thiết kế",
                                Number = productCount
                            };
                            model.Add(message);
                        }
                        productCount =
                            vfi.Products.Count(p => (p.Productivity == 0 || p.Productivity == null) && p.Active);
                        if (productCount > 0) {
                            var message = new SystemMessage {
                                MessageType = "Kỹ thuật",
                                Message = "Chưa cập nhật năng suất thiết kế",
                                Number = productCount
                            };
                            model.Add(message);
                        }
                        productCount =
                            vfi.Products.Count(p => (p.ProductionRate == 0 || p.ProductionRate == null) && p.Active);
                        if (productCount > 0) {
                            var message = new SystemMessage {
                                MessageType = "Kỹ thuật",
                                Message = "Chưa cập nhật định mức thiết kế",
                                Number = productCount
                            };
                            model.Add(message);
                        }
                        productCount =
                            vfi.Products.Count(p => (p.ProductionTools.Any(pt => pt.Active)) && p.Active);
                        if (productCount > 0) {
                            var message = new SystemMessage {
                                MessageType = "Kỹ thuật",
                                Message = "Chưa cập nhật công cụ thiết kế",
                                Number = productCount
                            };
                            model.Add(message);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMessage", ex.Message);
            }
            return View(new GridModel(model));
        }

    }

    internal class LoginResultDto {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ReturnUrl { get; set; }
    }
}
