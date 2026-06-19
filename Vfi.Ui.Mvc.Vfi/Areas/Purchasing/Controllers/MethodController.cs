using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Controllers {
    public class MethodController : Controller {
        //
        // GET: /Purchasing/Method/
        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }
            return ViewData;
        }
        public ActionResult MethodManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        #region method

        [HttpPost]
        [GridAction]
        public ActionResult SelectAllMethod() {
            try {
                return View(new GridModel(GetAllMethod()));
            }
            catch (Exception ex) {
                ModelState.AddModelError("AllMethod", ex.Message);
            }
            return View(new GridModel(new List<MethodModel>()));
        }

        private IOrderedEnumerable<MethodModel> GetAllMethod() {
            var model = new List<MethodModel>();
            using (var vfi = new tammaContext()) {
                model.AddRange(vfi.Methods.Select(method => new MethodModel {
                    MethodId = method.MethodId,
                    MethodName = method.MethodName,
                    MethodName_EN = method.MethodName_EN,
                    TypeId = method.MethodTypeId ?? 0,
                    TypeName = method.MethodType.TypeName,
                    Description = method.Description,
                    Active = method.Active ?? false,
                    ModifiedDate = method.ModifiedDate ?? DateTime.Now,
                    ModifiedUser = method.ModifiedUser ?? "",
                    BuyMethod = method.BuyMethod ?? false,
                    SaleMethod = method.SaleMethod ?? false,
                }));
            }
            return model.OrderBy(m => m.TypeId).ThenBy(m => m.MethodName);
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertMethod(MethodModel newMethodModel) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateWarehouse",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<MethodModel>()));
            }
            try {
                var method = new Method {
                    Active = newMethodModel.Active,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name,
                    MethodName = newMethodModel.MethodName,
                    MethodName_EN = newMethodModel.MethodName_EN + "",
                    Description = "",
                    BuyMethod = newMethodModel.BuyMethod,
                    SaleMethod = newMethodModel.SaleMethod
                };
                using (var vfi = new tammaContext()) {
                    int methodType = 1;
                    try {
                        methodType = Convert.ToInt32(newMethodModel.TypeName);
                    }
                    catch (Exception) {
                        methodType =
                            vfi.MethodTypes.FirstOrDefault(a => a.TypeName.Equals(newMethodModel.TypeName)).TypeId;
                    }
                    method.MethodTypeId = methodType;
                    vfi.Methods.Add(method);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("AllMethod", ex.Message);
            }
            return View(new GridModel(GetAllMethod()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateMethod(MethodModel updateMethodModel) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateWarehouse",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<MethodModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var method = vfi.Methods.FirstOrDefault(m => m.MethodId == updateMethodModel.MethodId);
                    if (method == null)
                        throw new AggregateException("Lỗi phương thức ! Tìm không thấy.");
                    method.Active = updateMethodModel.Active;
                    method.ModifiedDate = DateTime.Now;
                    method.ModifiedUser = HttpContext.User.Identity.Name;
                    method.MethodName = updateMethodModel.MethodName;
                    method.MethodName_EN = updateMethodModel.MethodName_EN;
                    method.BuyMethod = updateMethodModel.BuyMethod;
                    method.SaleMethod = updateMethodModel.SaleMethod;
                    method.Description = "";
                    int methodType = 1;
                    try {
                        methodType = Convert.ToInt32(updateMethodModel.TypeName);
                    }
                    catch (Exception) {
                        methodType =
                            vfi.MethodTypes.FirstOrDefault(a => a.TypeName.Equals(updateMethodModel.TypeName)).TypeId;
                    }
                    method.MethodTypeId = methodType;
                    //vfi.Methods.Add(method);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("AllMethod", ex.Message);
            }
            return View(new GridModel(GetAllMethod()));
        }

        public ActionResult SelectComboBoxMethodType() {
            using (var vfi = new tammaContext()) {
                return new JsonResult {
                    Data =
                        new SelectList(vfi.MethodTypes.ToList(), "TypeId",
                                       "TypeName")
                };
            }
        }

        public ActionResult SelectComboBoxMethodByType(int typeId, bool? sale, bool? buy) {
            using (var vfi = new tammaContext()) {
                var model = from m in vfi.Methods
                            where m.Active.Value && m.MethodTypeId == typeId
                                  && (sale != true || m.SaleMethod.Value)
                                  && (buy != true || m.BuyMethod.Value)
                            select new {
                                m.MethodId,
                                m.MethodName,
                                //m.SaleMethod,
                                //m.BuyMethod
                            };
                //if (sale)
                //    model = model.Where(m => m.SaleMethod.Value);
                //if (buy)
                //    model = model.Where(m => m.BuyMethod.Value);
                return new JsonResult {
                    Data =
                        new SelectList(model.ToList(), "MethodId", "MethodName")
                };
            }
        }

        public ActionResult SelectComboBoxMethodByType2(int? typeId, bool? buy) {
            using (var vfi = new tammaContext()) {
                var model = from m in vfi.Methods
                            where m.Active.Value && m.MethodTypeId == typeId
                                  //&& (sale != true || m.SaleMethod.Value)
                                  && (buy != true || m.BuyMethod.Value)
                            select new {
                                MethodId = m.MethodId,
                                MethodName = m.MethodName_EN,

                            };
                return new JsonResult {
                    Data =
                        new SelectList(model.ToList(), "MethodId", "MethodName")
                };
            }
        }


        public ActionResult SelectComboBoxPaymentMethod(int? typeId, bool? buy) {
            using (var vfi = new tammaContext()) {
                var model = from m in vfi.Methods
                            where m.Active.Value && m.MethodTypeId == typeId
                                //&& (sale != true || m.SaleMethod.Value)
                                  && (buy != true || m.BuyMethod.Value)
                            select new {
                                PaymentMethodId = m.MethodId,
                                PaymentMethodName = m.MethodName_EN,

                            };
                return new JsonResult {
                    Data =
                        new SelectList(model.ToList(), "PaymentMethodId", "PaymentMethodName")
                };
            }
        }

        public ActionResult SelectComboBoxShipMethodBy(int? typeId, bool? buy) {
            using (var vfi = new tammaContext()) {
                var model = from m in vfi.Methods
                            where m.Active.Value && m.MethodTypeId == typeId
                                //&& (sale != true || m.SaleMethod.Value)
                                  && (buy != true || m.BuyMethod.Value)
                            select new {
                                ShipMethodId = m.MethodId,
                                ShipMethodName = m.MethodName_EN,

                            };
                return new JsonResult {
                    Data =
                        new SelectList(model.ToList(), "ShipMethodId", "ShipMethodName")
                };
                //return Json(model.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SelectComboBoxConditionMethod(int? typeId, bool? buy) {
            using (var vfi = new tammaContext()) {
                var model = from m in vfi.Methods
                            where m.Active.Value && m.MethodTypeId == typeId
                                //&& (sale != true || m.SaleMethod.Value)
                                  && (buy != true || m.BuyMethod.Value)
                            select new {
                                DeliveryMethodId = m.MethodId,
                                DeliveryMethodName = m.MethodName_EN,

                            };
                return new JsonResult {
                    Data =
                        new SelectList(model.ToList(), "DeliveryMethodId", "DeliveryMethodName")
                };
            }
        }
        #endregion
    }
}
