using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
//using Vfi.Client.Module.Production.Interfaces;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
using System.Threading;
using System.Globalization;


namespace Vfi.Ui.Mvc.Vfi.Controllers.Production {
    public class CurrencyController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ICurrencyService _currencyService;
        [InjectionConstructor]
        public CurrencyController(IUnitOfWork unitOfWork
            //,                                    ICurrencyService currencyService
            ) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            //if (currencyService == null) throw new ArgumentNullException("currencyService");

            _unitOfWork = unitOfWork;
            //_currencyService = currencyService;
        }

        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }

            return ViewData;
        }
        // View
        public ActionResult CurrencyManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        // GetData
        List<CurrencyModel> GetCurrencyByModels() {
            var model = new List<CurrencyModel>();
            using (var vfi = new tammaContext()) {
                model.AddRange(vfi.Currencies.Select(x => new CurrencyModel {
                    CurrencyCode = x.CurrencyCode,
                    CurrencyName = x.CurrencyName,
                    Active = x.Active,
                    ModifiedUser = x.ModifiedUser,
                    ModifiedDate = x.ModifiedDate
                }));
            }
            return model.OrderByDescending(x => x.Active).ThenBy(x => x.CurrencyCode).ToList();
        }

        #region Currency

        [GridAction]
        public ActionResult SelectCurrency() {
            return View(new GridModel(GetCurrencyByModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertCurrency(CurrencyModel insert) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("InsertCurrency",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(GetCurrencyByModels()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var entity = vfi.Currencies.FirstOrDefault(x => x.CurrencyCode.Equals(insert.CurrencyCode));
                    if (entity != null) {
                        throw new AggregateException("Lỗi! Mã giá trị đã tồn tại");
                    }
                    entity = new Currency {
                        CurrencyCode = insert.CurrencyCode,
                        CurrencyName = insert.CurrencyName,
                        Active = true,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                    };
                    vfi.Currencies.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception) {
                ModelState.AddModelError("CurrencyCode", @"Lỗi giá trị nhập. (try-catch)");
            }
            return View(new GridModel(GetCurrencyByModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateCurrency(CurrencyModel update) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("InsertCurrency",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(GetCurrencyByModels()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var entity = vfi.Currencies.FirstOrDefault(x => x.CurrencyCode.Equals(update.CurrencyCode));
                    if (entity != null) {
                        entity.CurrencyName = update.CurrencyName;
                        entity.Active = update.Active;
                        entity.ModifiedUser = HttpContext.User.Identity.Name;
                        entity.ModifiedDate = DateTime.Now;
                        vfi.SaveChanges();
                    }
                    else {
                        entity = new Currency {
                            CurrencyCode = update.CurrencyCode,
                            CurrencyName = update.CurrencyName,
                            Active = true,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                        };
                        vfi.Currencies.Add(entity);
                        vfi.SaveChanges();
                    }
                }

            }
            catch (Exception) {
                ModelState.AddModelError("CurrencyCode", @"Lỗi giá trị nhập. (try-catch)");
            }
            return View(new GridModel(GetCurrencyByModels()));
        }

        public ActionResult SelectComboBoxCurrency() {
            return new JsonResult {
                Data = new SelectList(GetCurrencyByModels().Where(f => f.Active), "CurrencyCode", "CurrencyCodeName")
            };
        }

        #endregion
    }
}
