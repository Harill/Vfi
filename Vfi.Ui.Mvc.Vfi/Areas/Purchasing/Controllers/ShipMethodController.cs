using System;
using System.Collections.Generic;
//using System.Linq;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
//using Vfi.Client.Module.Purchasing.Interfaces;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
//using Vfi.Server.Core.DataModel.BaseEntities;
using Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
using Vfi.Ui.Mvc.Vfi.Models;
using System.Threading;
using System.Globalization;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Controllers {
    public class ShipMethodController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IShipMethodService _shipMethodService;
        [InjectionConstructor]
        public ShipMethodController(IUnitOfWork unitOfWork
            //, IShipMethodService shipMethodService
            ) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            //if (shipMethodService == null) throw new ArgumentNullException("shipMethodService");

            _unitOfWork = unitOfWork;
            //_shipMethodService = shipMethodService;
        }
        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }

            return ViewData;
        }
        // View
        public ActionResult ShipMethodManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        // Data
        public IEnumerable<ShipMethodModel> GetShipMethodByModels() {
            try {
                using (var vfi = new tammaContext() ){
                    return vfi.ShipMethods.Select(x => new ShipMethodModel {
                        ShipMethodId = x.ShipMethodId,
                        Name = x.Name,
                        ShipBase = x.ShipBase ?? 0,
                        ShipRate = x.ShipRate ?? 0,

                        Active = x.Active,
                        ModifiedUser = x.ModifiedUser,
                        ModifiedDate = x.ModifiedDate
                    })
                    .OrderBy(x=> x.Name)
                    .ToList();
                }
            }
            catch (Exception) {
                return null;
            }
        }

        #region ShipMethod

        [GridAction]
        public ActionResult SelectShipMethod() {
            return View(new GridModel(GetShipMethodByModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertShipMethod(ShipMethodModel insert) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ShipMethodModel>()));
            }

            var method = new ShipMethod { 
                Name = insert.Name,
                ShipBase = insert.ShipBase,
                ModifiedDate = DateTime.Now,
                ModifiedUser = HttpContext.User.Identity.Name,
                Active = true,
                ShipRate = 0
            };
            using (var vfi = new tammaContext()) {
                vfi.ShipMethods.Add(method);
                vfi.SaveChanges();
            }
            return View(new GridModel(GetShipMethodByModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateShipMethod(ShipMethodModel update) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ShipMethodModel>()));
            }

            using (var vfi = new tammaContext()) {
                var method = vfi.ShipMethods.FirstOrDefault(x => x.ShipMethodId == update.ShipMethodId);
                if (method == null) { throw new AggregateException("Lỗi! Không tìm thấy phương thức"); }
                method.Name = update.Name;
                method.ShipBase = update.ShipBase;
                method.ShipRate = update.ShipRate;
                method.Active = update.Active;
                method.ModifiedDate = DateTime.Now;
                method.ModifiedUser = HttpContext.User.Identity.Name;
                vfi.SaveChanges();
            }
            return View(new GridModel(GetShipMethodByModels()));
        }

        public ActionResult SelectComboBoxShipMethod() {
            return new JsonResult {
                Data = new SelectList(GetShipMethodByModels().Where(f => f.Active), "ShipMethodId", "Name")
            };
        }

        [GridAction]
        public ActionResult SelectShipMethodPrice(int productId) {
            var model = new List<ShipMethodModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(x => x.ProductId == productId);
                    if (product == null) { throw new AggregateException("Lỗi! Không tìm thấy sản phẩm"); }
                    var shipMethod = product.Customer.ShipMethod;
                    if (shipMethod != null) {
                        var entity = new ShipMethodModel {
                            ShipMethodId = shipMethod.ShipMethodId,
                            Name = shipMethod.Name,
                            ShipBase = shipMethod.ShipBase ?? 0,
                            UnitWeight = product.QcWeight ?? 0,
                        };
                        entity.ShipPrice = Math.Round(entity.ShipBase * entity.UnitWeight / 1000, 4);
                        model.Add(entity);
                    }
                    else {
                        var entity = new ShipMethodModel {
                            ShipMethodId = 0,
                            Name = "Chưa thiết lập",
                            ShipBase =  0,
                            UnitWeight = product.QcWeight ?? 0,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectShipMethodPrice", ex.Message);
            }
            return View(new GridModel(model));
        }
        #endregion


    }
}
