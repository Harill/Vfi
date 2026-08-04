using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Sales.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
using System.Threading;

namespace Vfi.Ui.Mvc.Vfi.Areas.Sales.Controllers {
    public class CustomerTypeController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ICustomerService _customerTypeService;
        [InjectionConstructor]
        public CustomerTypeController(IUnitOfWork unitOfWork
            //, ICustomerService customerTypeService
            ) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            //if (customerTypeService == null) throw new ArgumentNullException("customerTypeService");

            _unitOfWork = unitOfWork;
            //_customerTypeService = customerTypeService;
        }

        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }
            // 04/08/2026
            Session["CurrentCulture"] = "vi-VN";
            string culture = (string)Session["CurrentCulture"] ?? "en-US";
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return ViewData;
        }
        // View
        public ActionResult CustomerTypeManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }


        #region CustomerType

        [GridAction]
        public ActionResult SelectCustomerType() {
            return View(new GridModel(GetAllCustomerTypes()));
        }

        List<CustomerTypeModel> GetAllCustomerTypes() {
            var model = new List<CustomerTypeModel>();
            using (var vfi = new tammaContext()) {
                model.AddRange(vfi.CustomerTypes.Select(x => new CustomerTypeModel {
                    CustomerTypeId = x.CustomerTypeId,
                    CustomerTypeName = x.CustomerTypeName,
                    Active = x.Active,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedUser = x.ModifiedUser,
                    Note = x.Note
                }));
            }
            return model;
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertCustomerType(CustomerTypeModel insert) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("InsertCustomerType",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerType>()));
            }

            var entity = new CustomerType {
                CustomerTypeName = insert.CustomerTypeName,
                Note = insert.Note,
                Active = true,
                ModifiedDate = DateTime.Now,
                ModifiedUser = HttpContext.User.Identity.Name
            };
            using (var vfi = new tammaContext()) {
                vfi.CustomerTypes.Add(entity);
                vfi.SaveChanges();
            }
            return View(new GridModel(GetAllCustomerTypes()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateCustomerType(CustomerTypeModel update) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateCustomerType",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerType>()));
            }
            using (var vfi = new tammaContext()) {
                var customerType = vfi.CustomerTypes.FirstOrDefault(x => x.CustomerTypeId == update.CustomerTypeId);
                if (customerType == null) {
                    throw new AggregateException("Lỗi! Không tìm thấy loại khách hàng");
                }
                customerType.Note = update.Note.Trim();
                customerType.CustomerTypeName = update.CustomerTypeName.Trim();
                customerType.ModifiedUser = HttpContext.User.Identity.Name;
                customerType.ModifiedDate = DateTime.Now;
                vfi.SaveChanges();
            }

            return View(new GridModel(GetAllCustomerTypes()));
        }

        public ActionResult SelectComboBoxCustomerType() {
            var model = GetAllCustomerTypes().Where(x => x.Active).ToList();
            return new JsonResult {
                Data = new SelectList(model, "CustomerTypeId", "CustomerTypeName")
            };
        }
        #endregion


        #region customer area

        [GridAction]
        public ActionResult SelectCustomerArea() {
            using (var vfi = new tammaContext()) {
                return View(new GridModel(vfi.Areas.ToList().OrderBy(a => a.AreaName)));
            }
        }

        public ActionResult SelectComboBoxCustomerArea() {
            using (var vfi = new tammaContext()) {
                return new JsonResult {
                    Data = new SelectList(vfi.Areas.ToList(), "AreaId", "AreaName")
                };

            }
        }

        public ActionResult SelectComboBoxCustomerPayType() {
            using (var vfi = new tammaContext()) {
                return new JsonResult {
                    Data = new SelectList(vfi.CustomerPayTypes.ToList(), "Id", "TypeName")
                };

            }
        }
        #endregion

    }
}
