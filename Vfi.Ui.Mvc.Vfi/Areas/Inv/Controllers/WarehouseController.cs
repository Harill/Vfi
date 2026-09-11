using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
using System.Threading;
using System.Globalization;


//using Warehouse = Vfi.Server.Core.DataModel.BaseEntities.Warehouse;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Controllers {
    public class WarehouseController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IWarehouseService _warehouseService;
        [InjectionConstructor]
        public WarehouseController(IUnitOfWork unitOfWork
            //, IWarehouseService warehouseService
            ) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            //if (warehouseService == null) throw new ArgumentNullException("warehouseService");

            _unitOfWork = unitOfWork;
            //_warehouseService = warehouseService;
        }
        #region View
        // View
        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }

            return ViewData;
        }
        public ActionResult WarehouseManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult InventoryShelfManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProcessErrorManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        #endregion

        #region Warehouse

        [GridAction]
        public ActionResult SelectWarehouse() {
            return View(new GridModel(GetWarehouseModels()));
        }

        // Data
        public List<WarehouseModel> GetWarehouseModels() {
            var model = new List<WarehouseModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.Warehouses.Select(x => new WarehouseModel {
                    Idx = x.Idx,
                    WarehouseId = x.WarehouseId,
                    WarehouseName = x.WarehouseName,
                    ShortName = x.ShortName,
                    ModifiedUser = x.ModifiedUser,
                    ModifiedDate = x.ModifiedDate,

                    Active = x.Active,
                    IsMainProcess = x.IsMainProcess,
                    IsHeatTreatment = x.IsHeatTreatment,
                    IsPolish = x.IsPolish,
                    IsProduction = x.IsProduction,
                    IsProduction2 = x.IsProduction2,
                    IsProduction2Process = x.IsProduction2Process,
                    IsReprocessing = x.IsReprocessing,
                    IsPlating = x.IsPlating,
                    IsQC = x.IsQC,
                    IsPacking = x.IsPacking,
                    IsFinish = x.IsFinish,
                    CanInternal = x.CanInternal,
                    CanPurchase = x.CanPurchase,
                    CanStock = x.CanStock,
                    CanWeighing = x.CanWeighing,
                    IsOutOfProcess = x.IsOutOfProcess,
                    IsCncMilling = x.IsCncMilling,
                    AutoGenerateProcess = x.AutoGenerateProcess ?? false,
                    IsDefect = x.IsDefect ?? false,
                    IsDestroy = x.IsDestroy ?? false,
                    IsTransfer = x.IsTransfer ?? false,
                }).ToList();
            }
            return model.OrderByDescending(m => m.Active).ThenBy(m => m.Idx).ThenBy(m => m.WarehouseName).ToList();
        }
        [GridAction]
        public ActionResult SelectActiveWarehouse() {
            var model = new List<WarehouseModel>();
            try {
                model = GetWarehouseModels().Where(x => x.Active).ToList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectActiveWarehouse", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectWarehouse_user() {
            var model = new List<WarehouseModel>();
            try {
                model = GetWarehouseModels().Where(x => x.Active && x.WarehouseTypeId != 1).ToList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectWarehouse_user", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertWarehouse(WarehouseModel inserted) {

            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("InsertWarehouse",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<WarehouseModel>()));
            }
            //var model = new Warehouse();
            //if (TryUpdateModel(model))
            //{
            try {
                using (var vfi = new tammaContext()) {
                    if (string.IsNullOrWhiteSpace(inserted.WarehouseName))
                        throw new AggregateException("Lỗi! Tên kho phải có");

                    var entity = new Mvc.Vfi.Models.Warehouse {
                        WarehouseName = inserted.WarehouseName.Trim(),
                        ShortName = inserted.ShortName.Trim(),
                        Idx = inserted.Idx,
                        Active = true,

                        CanInternal = inserted.CanInternal,
                        CanPurchase = inserted.CanPurchase,
                        CanStock = inserted.CanStock,
                        IsHeatTreatment = inserted.IsHeatTreatment,
                        IsPolish = inserted.IsPolish,
                        IsProduction = inserted.IsProduction,
                        IsCncMilling = inserted.IsCncMilling,
                        IsProduction2 = inserted.IsProduction2,
                        IsProduction2Process = inserted.IsProduction2Process,
                        IsReprocessing = inserted.IsReprocessing,
                        IsMainProcess = inserted.IsMainProcess,
                        IsQC = inserted.IsQC,
                        IsPlating = inserted.IsPlating,
                        IsPacking = inserted.IsPacking,
                        IsFinish = inserted.IsFinish,
                        IsOutOfProcess = inserted.IsOutOfProcess,
                        CanWeighing = inserted.CanWeighing,
                        AutoGenerateProcess = inserted.AutoGenerateProcess,
                        
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    };
                    if (string.IsNullOrWhiteSpace(entity.ShortName))
                        entity.ShortName = entity.WarehouseName;
                    vfi.Warehouses.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("WarehouseName", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }
            return View(new GridModel(GetWarehouseModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateWarehouse(WarehouseModel updated) {

            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateWarehouse",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<WarehouseModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    if (string.IsNullOrWhiteSpace(updated.WarehouseName))
                        throw new AggregateException("Lỗi! Tên kho phải có");

                    var entity = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == updated.WarehouseId);
                    if (entity == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy kho cần cập nhật");
                    }
                    entity.WarehouseName = updated.WarehouseName.Trim();
                    entity.ShortName = updated.ShortName.Trim();
                    entity.Idx = updated.Idx;
                    entity.Active = updated.Active;
                    entity.IsMainProcess = updated.IsMainProcess;
                    entity.IsOutOfProcess = updated.IsOutOfProcess;
                    entity.AutoGenerateProcess = updated.AutoGenerateProcess;
                    //entity.CanInternal = updated.CanInternal;
                    //entity.CanPurchase = updated.CanPurchase;
                    //entity.CanStock = updated.CanStock;
                    //entity.IsHeatTreatment = updated.IsHeatTreatment;
                    //entity.IsPolish = updated.IsPolish;
                    //entity.IsProduction = updated.IsProduction;
                    //entity.IsCncMilling = updated.IsCncMilling;
                    //entity.IsProduction2 = updated.IsProduction2;
                    //entity.IsProduction2Process = updated.IsProduction2Process;
                    //entity.IsReprocessing = updated.IsReprocessing;
                    //entity.IsQC = updated.IsQC;
                    //entity.IsPlating = updated.IsPlating;
                    //entity.IsPacking = updated.IsPacking;
                    //entity.IsFinish = updated.IsFinish;
                    //entity.CanWeighing = updated.CanWeighing;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    entity.ModifiedDate = DateTime.Now;
                    if (string.IsNullOrWhiteSpace(entity.ShortName))
                        entity.ShortName = entity.WarehouseName;
                    //vfi.Warehouses.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("WarehouseName", @"Lỗi giá trị nhập.\r\n(try-catch)\r\n" + exception.Message);
            }
            return View(new GridModel(GetWarehouseModels()));
        }


        [GridAction]
        public ActionResult SelectWarehouseProcessConfiguration(int warehouseId) {
            var model = new List<WarehouseModel>();
            try {
                model = GetWarehouseModels().Where(x => x.WarehouseId == warehouseId).ToList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectWarehouseProcessConfiguration", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateWarehouseProcessConfiguration(WarehouseModel updated) {

            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateWarehouseProcessConfiguration",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<WarehouseModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {

                    var entity = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == updated.WarehouseId);
                    if (entity == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy kho cần cập nhật");
                    }
                    entity.IsHeatTreatment = updated.IsHeatTreatment;
                    entity.IsPolish = updated.IsPolish;
                    entity.IsProduction = updated.IsProduction;
                    entity.IsCncMilling = updated.IsCncMilling;
                    entity.IsProduction2 = updated.IsProduction2;
                    entity.IsProduction2Process = updated.IsProduction2Process;
                    entity.IsReprocessing = updated.IsReprocessing;
                    entity.IsQC = updated.IsQC;
                    entity.IsPacking = updated.IsPacking;
                    entity.IsPlating = updated.IsPlating;
                    entity.IsFinish = updated.IsFinish;
                    entity.IsDefect = updated.IsDefect;

                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateWarehouseProcessConfiguration", @"Lỗi giá trị nhập.\r\n(try-catch)\r\n" + exception.Message);
            }
            return View(new GridModel(GetWarehouseModels().Where(x => x.WarehouseId == updated.WarehouseId)));
        }

        [GridAction]
        public ActionResult SelectWarehouseFunctionConfiguration(int warehouseId) {
            var model = new List<WarehouseModel>();
            try {
                model = GetWarehouseModels().Where(x => x.WarehouseId == warehouseId).ToList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectWarehouseFunctionConfiguration", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateWarehouseFunctionConfiguration(WarehouseModel updated) {

            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateWarehouseFunctionConfiguration",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<WarehouseModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {

                    var entity = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == updated.WarehouseId);
                    if (entity == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy kho cần cập nhật");
                    }
                    entity.CanInternal = updated.CanInternal;
                    entity.CanPurchase = updated.CanPurchase;
                    entity.CanStock = updated.CanStock;
                    entity.CanWeighing = updated.CanWeighing;
                    entity.IsDestroy = updated.IsDestroy;
                    entity.IsTransfer = updated.IsTransfer;

                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateWarehouseFunctionConfiguration", @"Lỗi giá trị nhập.\r\n(try-catch)\r\n" + exception.Message);
            }
            return View(new GridModel(GetWarehouseModels().Where(x => x.WarehouseId == updated.WarehouseId)));
        }

        public List<WarehouseCboModel> GetActiveWarehouseModels(WarehouseConfiguration config) {
            var model = new List<WarehouseCboModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.Warehouses
                         where x.Active
                             && (config.IsMainProcess == null || x.IsMainProcess == config.IsMainProcess)
                             && (config.IsProduction == null || x.IsProduction == config.IsProduction)
                             && (config.IsCncMilling == null || x.IsCncMilling == config.IsCncMilling)
                             && (config.IsProduction2 == null || x.IsProduction2 == config.IsProduction2)
                             && (config.IsProduction2Process == null || x.IsProduction2Process == config.IsProduction2Process)
                             && (config.IsHeatTreatment == null || x.IsHeatTreatment == config.IsHeatTreatment)
                             && (config.IsPolish == null || x.IsPolish == config.IsPolish)
                             && (config.IsQC == null || x.IsQC == config.IsQC)
                             && (config.IsPlating == null || x.IsPlating == config.IsPlating)
                             && (config.IsPacking == null || x.IsPacking == config.IsPacking)
                             && (config.IsFinish == null || x.IsFinish == config.IsFinish)
                             && (config.IsDefect == null || x.IsDefect == config.IsDefect)
                             && (config.IsDestroy == null || x.IsDestroy == config.IsDestroy)
                             && (config.IsTransfer == null || x.IsTransfer == config.IsTransfer)
                             && (config.IsReprocessing == null || x.IsReprocessing == config.IsReprocessing)
                             && (config.IsOutOfProcess == null || x.IsOutOfProcess == config.IsOutOfProcess)
                             && (config.CanInternal == null || x.CanInternal == config.CanInternal)
                             && (config.CanPurchase == null || x.CanPurchase == config.CanPurchase)
                             && (config.CanStock == null || x.CanStock == config.CanStock)
                             && (config.CanWeighing == null || x.CanWeighing == config.CanWeighing)
                             && (!config.Ids.Any() || config.Ids.Contains(x.WarehouseId))
                         orderby x.Idx, x.WarehouseName
                         select new WarehouseCboModel {
                             WarehouseId = x.WarehouseId,
                             WarehouseName = x.WarehouseName,
                             Idx = x.Idx,
                         }).ToList();
            }
            if (!model.Any()) {
                model.Insert(0, new WarehouseCboModel {
                    WarehouseId = 0,
                    WarehouseName = "Chưa có thiết lập",
                    Idx = 0
                });
            }
            else if (config.AddFirstAll == true) {
                model.Insert(0, new WarehouseCboModel {
                    WarehouseId = 0,
                    WarehouseName = "Tất cả",
                    Idx = 0
                });
            }
            return model;
        }

        public List<int> GetActiveWarehouseIds(WarehouseConfiguration config) {
            return GetActiveWarehouseModels(config).Where(x => x.WarehouseId > 0).Select(x => x.WarehouseId).ToList();
        }

        public ActionResult SelectComboBoxWarehouseMainProcess() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsMainProcess = true }), "WarehouseId", "WarehouseName")
            };
        }

        public ActionResult SelectComboBoxWarehouseSameType(int warehouseId) {
            var model = new List<WarehouseCboModel>();
            using (var vfi = new tammaContext()) {
                var warehouse = vfi.Warehouses.FirstOrDefault(x => x.WarehouseId == warehouseId);
                if (warehouse != null) {
                    var config = new WarehouseConfiguration { };
                    if (warehouse.IsProduction) { config.IsProduction = warehouse.IsProduction; }
                    if (warehouse.IsCncMilling) { config.IsCncMilling = warehouse.IsCncMilling; }
                    model = GetActiveWarehouseModels(config);
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "WarehouseId", "WarehouseName")
            };
        }

        public ActionResult SelectComboBoxWarehouseProductionTesting() {
            var warehouseIds = new List<int>();
            warehouseIds.Add(GetActiveWarehouseIds(new WarehouseConfiguration { IsProduction = true }).FirstOrDefault());
            warehouseIds.Add(GetActiveWarehouseIds(new WarehouseConfiguration { IsQC = true }).FirstOrDefault());
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { Ids = warehouseIds }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehouseProduction() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsProduction = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehouseCncMilling() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsCncMilling = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehouseProduction2All() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsProduction2 = true, AddFirstAll = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehouseProduction2() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsProduction2 = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehouseProduction2Processing() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsProduction2Process = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehouseHeatTreatment() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsHeatTreatment = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehousePolish() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsPolish = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehouseReprocessing() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsReprocessing = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehouseQc() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsQC = true, AddFirstAll = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehousePacking() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsPacking = true, AddFirstAll = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehouseFinish() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsFinish = true, AddFirstAll = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWarehousePlating() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { IsPlating = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxInternalWarehouse() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { CanInternal = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxPurchaseWarehouse() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { CanPurchase = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxWeighingWarehouse() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { CanWeighing = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectComboBoxStockWarehouse() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { CanStock = true }), "WarehouseId", "WarehouseName")
            };
        }
        public ActionResult SelectAllComboBoxWarehouse() {
            return new JsonResult {
                Data = new SelectList(GetActiveWarehouseModels(new WarehouseConfiguration { AddFirstAll = true }), "WarehouseId", "WarehouseName")
            };
        }

        public ActionResult SelectComboBoxImportWarehouse() {
            var warehouseIds = new List<int>();
            using (var vfi = new tammaContext()) {
                var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                if (user == null)
                    throw new AggregateException("Vui lòng đăng nhập lại");
                warehouseIds =
                    vfi.WarehousePermissions.Where(wp => wp.UserId == user.UserId && wp.Import == true)
                       .Select(wp => wp.WarehouseId.Value)
                       .ToList();
            }
            var model = new List<WarehouseCboModel>();
            if (warehouseIds.Any()) {
                model = GetActiveWarehouseModels(new WarehouseConfiguration { Ids = warehouseIds });
            }
            return new JsonResult {
                Data = new SelectList(model, "WarehouseId", "WarehouseName")
            };
        }


        public ActionResult SelectComboBoxWarehouseRotateById(int warehouseId) {
            var warehouseIds = new List<int>();
            using (var vfi = new tammaContext()) {
                var isInvManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagement);
                if (isInvManager) {
                    warehouseIds = vfi.Warehouses.Select(x => x.WarehouseId).ToList();
                }
                else {
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null) {
                        return new JsonResult {
                            Data = new SelectList(new List<WarehouseCboModel>(), "WarehouseId", "WarehouseName")
                        };
                    }
                    warehouseIds = vfi.WarehouseRotates.Where(x => x.Active && x.WarehouseId == warehouseId)
                                                        .Select(x => x.ToWarehouseId)
                                                        .ToList();
                    //warehouseIds.AddRange(MyUtilities.Warehouse.GetWarehouseId_ExceptionRotate());
                    warehouseIds = vfi.WarehousePermissions.Where(x => x.UserId == user.UserId
                                                                    && x.Rotate == true
                                                                    && warehouseIds.Contains(x.WarehouseId.Value))
                                                            .Select(x => x.WarehouseId.Value).ToList();
                }
            }
            var model = new List<WarehouseCboModel>();
            if (warehouseIds.Any()) {
                model = GetActiveWarehouseModels(new WarehouseConfiguration { Ids = warehouseIds });
            }
            return new JsonResult {
                Data = new SelectList(model, "WarehouseId", "WarehouseName")
            };
        }

        public ActionResult SelectComboBoxCncRotateWarehouse() {
            return SelectComboBoxWarehouseRotateById(MyUtilities.Warehouse.Cnc);
        }

        public ActionResult SelectComboBoxRotateWarehouse() {
            var model = new List<WarehouseCboModel>();
            using (var vfi = new tammaContext()) {
                var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                if (user == null)
                    throw new AggregateException("Vui lòng đăng nhập lại");
                var warehouseIds =
                    vfi.WarehousePermissions.Where(wp => wp.UserId == user.UserId && wp.Rotate == true)
                       .Select(wp => wp.WarehouseId.Value)
                       .ToList();
                model = GetActiveWarehouseModels(new WarehouseConfiguration { Ids = warehouseIds });
            }
            return new JsonResult {
                Data = new SelectList(model, "WarehouseId", "WarehouseName")
            };
        }

        public ActionResult SelectComboBoxProgressWarehouseByUser() {
            var model = new List<WarehouseCboModel>();
            using (var vfi = new tammaContext()) {
                var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                if (user == null)
                    throw new AggregateException("Vui lòng đăng nhập lại");
                var warehouseIds =
                    vfi.WarehousePermissions.Where(wp => wp.UserId == user.UserId && wp.OrderProgress == true)
                       .Select(wp => wp.WarehouseId.Value)
                       .ToList();
                model = GetActiveWarehouseModels(new WarehouseConfiguration { Ids = warehouseIds });
            }
            return new JsonResult {
                Data = new SelectList(model, "WarehouseId", "WarehouseName")
            };
        }

        #endregion

        #region shelf - drawer

        [GridAction]
        public ActionResult SelectInventoryShelf() {
            var model = new List<InventoryShelfModel>();
            try {
                model = GetInventoryShelves();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectInventoryShelf", @"Lỗi giá trị nhập. (try-catch). " + ex.Message);
            }
            return View(new GridModel(model));
        }

        // Data
        public List<InventoryShelfModel> GetInventoryShelves() {
            var model = new List<InventoryShelfModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.InventoryShelves
                         select new InventoryShelfModel {
                             ShelfId = x.ShelfId,
                             ShelfName = x.ShelfName,
                             ClassifiedId = x.ClassifiedId,
                             ClassifiedName = x.MaterialClassified.MaterialClassifiedName,
                             WarehouseId = x.WarehouseId,
                             WarehouseName = x.WarehouseId != null ? x.Warehouse.WarehouseName : "",
                             MaxColumn = x.MaxColumn,
                             MaxRow = x.MaxRow,
                             Active = x.Active,
                             ModifiedDate = x.ModifiedDate,
                             ModifiedUser = x.ModifiedUser,
                         }).ToList();
            }
            return model;
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertInventoryShelf(InventoryShelfModel inserted) {

            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("InsertInventoryShelf",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<InventoryShelfModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    if (string.IsNullOrWhiteSpace(inserted.ShelfName)) {
                        throw new AggregateException("Lỗi! Vui lòng nhập tên kệ");
                    }
                    else { inserted.ShelfName = inserted.ShelfName.Trim(); }
                    if (inserted.MaxRow <= 0 || inserted.MaxColumn <= 0) {
                        throw new AggregateException("Lỗi! Số hàng hoặc số cột lỗi");
                    }
                    var shelf = vfi.InventoryShelves.FirstOrDefault(x => x.ShelfName.Equals(inserted.ShelfName) 
                                                                      && x.ClassifiedId == inserted.ClassifiedId);
                    if (shelf != null) { throw new AggregateException("Lỗi! Kệ trùng tên"); }
                    var classifiedId = 0;
                    try { classifiedId = Convert.ToInt32(inserted.ClassifiedName); }
                    catch (FormatException) { }
                    if (classifiedId <= 0) { throw new AggregateException("Lỗi! Phân loại kệ lỗi"); }

                    shelf = new InventoryShelf {
                        ShelfId = inserted.ShelfId,
                        ShelfName = inserted.ShelfName,
                        ClassifiedId = classifiedId,
                        MaxColumn = inserted.MaxColumn,
                        MaxRow = inserted.MaxRow,
                        Active = true,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                    };
                    //if (inserted.WarehouseId > 0) {
                    //    shelf.WarehouseId = inserted.WarehouseId;
                    //}
                    vfi.InventoryShelves.Add(shelf);
                    var drawers = new List<InventoryDrawer>();
                    for (var i = 1; i <= inserted.MaxColumn; i++) {
                        for (var j = 0; j < inserted.MaxRow; j++) {
                            var drawer = new InventoryDrawer {
                                InventoryShelf = shelf,
                                Active = shelf.Active,
                                ModifiedDate = shelf.ModifiedDate,
                                ModifiedUser = shelf.ModifiedUser,
                                ColumnName = string.Format("{0:00}", i),
                                RowName = Convert.ToChar(65 + j) + "",
                            };
                            drawers.Add(drawer);
                        } 
                    }
                    vfi.InventoryDrawers.AddRange(drawers);
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("InsertInventoryShelf", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }
            return View(new GridModel(GetInventoryShelves()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateInventoryShelf(InventoryShelfModel updated) {

            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateInventoryShelf",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<InventoryShelfModel>()));
            }
            try {
                //throw new AggregateException("Lỗi! Chưa làm chức năng này");
                using (var vfi = new tammaContext()) {
                    if (string.IsNullOrWhiteSpace(updated.ShelfName)) {
                        throw new AggregateException("Lỗi! Vui lòng nhập tên kệ");
                    }
                    else { updated.ShelfName = updated.ShelfName.Trim(); }

                    if (updated.MaxRow <= 0 || updated.MaxColumn <= 0) {
                        throw new AggregateException("Lỗi! Số hàng hoặc số cột lỗi");
                    }

                    var shelf = vfi.InventoryShelves.FirstOrDefault(x => x.ShelfName.Equals(updated.ShelfName)
                                                                        && x.ClassifiedId == updated.ClassifiedId
                                                                        && x.ShelfId != updated.ShelfId);
                    if (shelf != null) { throw new AggregateException("Lỗi! Kệ trùng tên"); }

                    shelf = vfi.InventoryShelves.FirstOrDefault(x => x.ShelfId == updated.ShelfId);
                    if (shelf == null) { throw new AggregateException("Lỗi! Không tìm thấy kệ"); }
                    if (shelf.MaxRow > updated.MaxRow || shelf.MaxColumn > updated.MaxColumn) { 
                        throw new AggregateException("Lỗi! Không thể giảm số cột và dòng"); 
                    }

                    shelf.ShelfName = updated.ShelfName;
                    //shelf.ClassifiedId = updated.ClassifiedId;
                    shelf.Active = updated.Active;
                    shelf.ModifiedDate = DateTime.Now;
                    shelf.ModifiedUser = HttpContext.User.Identity.Name;

                    var drawers = new List<InventoryDrawer>();
                    if (updated.MaxColumn > shelf.MaxColumn) {
                        for (var i = shelf.MaxColumn + 1; i <= updated.MaxColumn; i++) {
                            for (var j = 0; j < updated.MaxRow; j++) {
                                var drawer = new InventoryDrawer {
                                    InventoryShelf = shelf,
                                    Active = shelf.Active,
                                    ModifiedDate = shelf.ModifiedDate,
                                    ModifiedUser = shelf.ModifiedUser,
                                    ColumnName = string.Format("{0:00}", i),
                                    RowName = Convert.ToChar(65 + j) + "",
                                };
                                drawers.Add(drawer);
                            }
                        }
                        shelf.MaxColumn = updated.MaxColumn;
                    }
                    if (updated.MaxRow > shelf.MaxRow) {
                        for (var i = 1; i <= updated.MaxColumn; i++) {
                            for (var j = shelf.MaxRow; j < updated.MaxRow; j++) {
                                var drawer = new InventoryDrawer {
                                    InventoryShelf = shelf,
                                    Active = shelf.Active,
                                    ModifiedDate = shelf.ModifiedDate,
                                    ModifiedUser = shelf.ModifiedUser,
                                    ColumnName = string.Format("{0:00}", i),
                                    RowName = Convert.ToChar(65 + j) + "",
                                };
                                drawers.Add(drawer);
                            }
                        }
                        shelf.MaxRow = updated.MaxRow;
                    }
                    vfi.InventoryDrawers.AddRange(drawers);

                    //if (shelf.MaxRow < updated.MaxRow) { shelf.MaxRow = updated.MaxRow; }
                    //if (shelf.MaxColumn < updated.MaxColumn) { shelf.MaxColumn = updated.MaxColumn; }

                    //if (updated.WarehouseId > 0) {
                    //    if (updated.WarehouseId != shelf.WarehouseId) {
                    //        shelf.WarehouseId = updated.WarehouseId;
                    //    }
                    //}
                    //else { shelf.WarehouseId = null; }
                    //var drawers = new List<InventoryDrawer>();
                    //for (var i = 1; i <= inserted.MaxColumn; i++) {
                    //    for (var j = 0; j < inserted.MaxRow; j++) {
                    //        var drawer = new InventoryDrawer {
                    //            InventoryShelf = shelf,
                    //            Active = shelf.Active,
                    //            ModifiedDate = shelf.ModifiedDate,
                    //            ModifiedUser = shelf.ModifiedUser,
                    //            ColumnName = string.Format("{0:00}", i),
                    //            RowName = Convert.ToChar(65 + j) + "",
                    //        };
                    //        drawers.Add(drawer);
                    //    }
                    //}
                    //vfi.InventoryDrawers.AddRange(drawers);
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateInventoryShelf", @"Lỗi giá trị nhập.\r\n(try-catch)\r\n" + exception.Message);
            }
            return View(new GridModel(GetInventoryShelves()));
        }

        [GridAction]
        public ActionResult SelectInventoryDrawer(int shelfId) {
            var model = new List<InventoryDrawerModel>();
            try {
                model = GetInventoryDrawers(shelfId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectInventoryDrawer", @"Lỗi giá trị nhập. (try-catch). " + ex.Message);
            }
            return View(new GridModel(model));
        }

        // Data
        public List<InventoryDrawerModel> GetInventoryDrawers(int shelfId) {
            var model = new List<InventoryDrawerModel>();
            using (var vfi = new tammaContext()) {
                //var shelf = vfi.InventoryShelves.FirstOrDefault(x=> x.ShelfId);
                //if (shelf == null) { throw new AggregateException("Lỗi! Không tìm thấy kệ"); }
                var drawers = (from x in vfi.InventoryDrawers
                               where x.ShelfId == shelfId
                               orderby x.RowName, x.ColumnName
                               select x).ToList();
                //select new InventoryDrawerModel {
                //    ShelfId = shelfId,
                //    DrawerId = x.DrawerId,
                //    ColumnName = x.ColumnName,
                //    RowName = x.RowName,
                //    Active = x.Active,
                //    ModifiedDate = x.ModifiedDate,
                //    ModifiedUser = x.ModifiedUser,
                //});
                var drawerIds = drawers.Select(x => x.DrawerId).ToList();
                var onshelves = vfi.OnShelves.Where(x => x.Active && drawerIds.Contains(x.DrawerId)).ToList();
                var materialInvIds = onshelves.Where(x=> x.InventoryDrawer.InventoryShelf.ClassifiedId== 1).Select(x => x.ReferenceInvId).Distinct().ToList();
                var materialInv = (from x in vfi.MaterialInventories
                                  where materialInvIds.Contains(x.MaterialInventoryId)
                                  select x).ToList();
                foreach (var drawer in drawers) {
                    var entity = new InventoryDrawerModel {
                        ShelfId = shelfId,
                        ShelfName = drawer.InventoryShelf.ShelfName,
                        DrawerId = drawer.DrawerId,
                        ColumnName = drawer.ColumnName,
                        RowName = drawer.RowName,
                        AdditionName = drawer.AdditionName,
                        Active = drawer.Active,
                        ModifiedDate = drawer.ModifiedDate,
                        ModifiedUser = drawer.ModifiedUser,
                        ClassifiedId = drawer.InventoryShelf.ClassifiedId
                    };
                    var onshelfs = onshelves.Where(x => x.DrawerId == drawer.DrawerId);
                    var listCodes = new List<string>();
                    foreach (var onshelf in onshelfs) {
                        switch (drawer.InventoryShelf.ClassifiedId) {
                            case 1: // nguyen lieu
                                var inv = materialInv.FirstOrDefault(x => x.MaterialInventoryId == onshelf.ReferenceInvId);
                                if (inv != null) {
                                    listCodes.Add(MyUtilities.Material.GetMaterialInvDesignNo(inv));
                                }
                                break;
                            default:
                                listCodes.Add("Không xác định");
                                break;
                        }
                    }
                    entity.ReferenceInvCode = string.Join(" | ", listCodes);
                    //if (drawer.ReferenceInvId > 0) {
                    //    switch (drawer.InventoryShelf.ClassifiedId) {
                    //        case 1: // nguyen lieu
                    //            var inv = vfi.MaterialInventories.FirstOrDefault(x => x.MaterialInventoryId == drawer.ReferenceInvId);
                    //            if (inv != null) {
                    //                entity.ReferenceCode = inv.Material.MaterialCode;
                    //                entity.LotNumber = inv.LotNumber;
                    //                entity.OwnerName = inv.Vendor.VendorName;
                    //                entity.UnitWeight = inv.UnitWeight;
                    //                entity.Unit = "Kg";
                    //                entity.Quantity = inv.TotalQty * inv.UnitWeight;
                    //            }
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}
                    //else if (drawer.ReferenceId > 0) {
                    //    switch (drawer.InventoryShelf.ClassifiedId) {
                    //        case 1: // nguyen lieu
                    //            var material = vfi.Materials.FirstOrDefault(x => x.MaterialId == drawer.ReferenceId);
                    //            if (material != null) {
                    //                entity.ReferenceName = material.MaterialCode;
                    //            }
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}

                    model.Add(entity);
                }
            }
            return model;
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertInventoryDrawer(InventoryDrawerModel inserted, int shelfId) {

            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("InsertInventoryDrawer",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<InventoryDrawerModel>()));
            }
            try {
                throw new AggregateException("Lỗi! Chưa làm chức năng này");
                //using (var vfi = new tammaContext()) {
                //}
            }
            catch (Exception exception) {
                ModelState.AddModelError("InsertInventoryDrawer", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }
            return View(new GridModel(GetInventoryDrawers(shelfId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateInventoryDrawer(InventoryDrawerModel updated, int shelfId) {

            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateInventoryDrawer",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<InventoryDrawerModel>()));
            }
            try {
                throw new AggregateException("Lỗi! Chưa làm chức năng này");
                //using (var vfi = new tammaContext()) {
                //}
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateInventoryDrawer", @"Lỗi giá trị nhập.\r\n(try-catch)\r\n" + exception.Message);
            }
            return View(new GridModel(GetInventoryDrawers(shelfId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult DeleteInventoryDrawer(int drawerId, int shelfId) {

            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateInventoryDrawer",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<InventoryDrawerModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var drawer = vfi.InventoryDrawers.FirstOrDefault(x => x.DrawerId == drawerId);
                    if (drawer != null) {
                        drawer.ReferenceId = null;
                        drawer.ReferenceInvId = null;
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Lỗi! Không tìm thấy kệ");
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("DeleteInventoryDrawer", @"Lỗi giá trị nhập.\r\n(try-catch)\r\n" + exception.Message);
            }
            return View(new GridModel(GetInventoryDrawers(shelfId)));
        }


        public ActionResult DuplicateInventoryDrawer(int drawerId) {
            using (var vfi = new tammaContext()) {
                var drawer = vfi.InventoryDrawers.FirstOrDefault(x => x.DrawerId == drawerId);
                if (drawer == null) {
                    return Json(MyUtilities.Monitor.ErrorCode.NotFound);
                }
                var newDrawer = new InventoryDrawer {
                    Active = true,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name,
                    RowName = drawer.RowName,
                    ColumnName = drawer.ColumnName,
                    ShelfId = drawer.ShelfId,
                };
                var drawerCounts = vfi.InventoryDrawers.Where(x => x.ColumnName.Equals(drawer.ColumnName)
                                                                && x.RowName.Equals(drawer.RowName)
                                                                && x.ShelfId == drawer.ShelfId)
                                                        .ToList()
                                                        .Count;
                var additionChar = '\'';
                newDrawer.AdditionName = new String(additionChar, drawerCounts);
                vfi.InventoryDrawers.Add(newDrawer);
                vfi.SaveChanges();
            }
            return Json(MyUtilities.Monitor.ErrorCode.NoError);
        }

        public ActionResult SelectComboBoxInventoryDrawer(int classifiedId) {
            var model = new List<InventoryDrawerModel>();
            try {
                using (var vfi = new tammaContext()) {
                    model = (from x in vfi.InventoryDrawers
                             where x.Active
                                  && x.InventoryShelf.ClassifiedId == classifiedId
                             select new InventoryDrawerModel {
                                 DrawerId = x.DrawerId,
                                 ColumnName = x.ColumnName,
                                 RowName = x.RowName,
                                 AdditionName = x.AdditionName,
                                 ShelfName = x.InventoryShelf.ShelfName
                             }).ToList();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectComboBoxInventoryDrawer", ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(model.OrderBy(x => x.DrawerCode), "DrawerId", "DrawerCode")
            };
        }


        [HttpPost]
        public ActionResult PrintInventoryOnShelfCards(string checkedRecords) {
            var model = new List<MaterialInvShelfDiagramModel>();
            try {
                int[] ids;
                try {
                    var lst = checkedRecords.Split(':');
                    ids = new int[lst.Count()];
                    for (var i = 0; i < lst.Count(); i++) {
                        ids[i] = Convert.ToInt32(lst.ElementAt(i));
                    }
                }
                catch (FormatException) {
                    return View(new GridModel(new List<ProductInventoryRotateModel>()));
                }
                if (ids.Count() <= 0)
                    return View(new GridModel(model));
                if (ids[0] == 0) {
                    return View(new GridModel(model));
                }
                var over2YearColor = "FF6666";
                var over1YearColor = "FFC061";
                var below1YearColor = "";
                using (var vfi = new tammaContext()) {
                    var drawers = (from x in vfi.InventoryDrawers
                                   where ids.Contains(x.DrawerId)
                                   select new {
                                       x.DrawerId,
                                       AdditionName = x.AdditionName,
                                       ColumnName = x.ColumnName,
                                       RowName = x.RowName,
                                       ShelfName = x.InventoryShelf.ShelfName,
                                       x.InventoryShelf.ClassifiedId,
                                   }).ToList();

                    var onShelves = (from x in vfi.OnShelves
                                     where x.Active && ids.Contains(x.DrawerId)
                                     select x).ToList();
                    var invIds = onShelves.Select(x => x.ReferenceInvId).Distinct().ToList();
                    var classified = drawers.FirstOrDefault().ClassifiedId;
                    switch (classified) {
                        case 1: {
                            var materialInvs = (from x in vfi.MaterialInventories
                                                where invIds.Contains(x.MaterialInventoryId)
                                                select new MaterialInventoryModel {
                                                    MaterialInventoryId = x.MaterialInventoryId,
                                                    LotNumber = x.LotNumber,
                                                    ImportDate = x.ImportDate,
                                                    TotalImportKg = x.ImportQuantityKg,
                                                    QuantityKg = x.TotalQty * x.UnitWeight,
                                                    MaterialName = x.Material.MaterialName,
                                                    OutDiameter = x.Material.OutDiameter,
                                                    InDiameter = x.Material.InDiameter,
                                                    DiameterType = x.Material.DiameterType,
                                                    Shape = x.Material.Shape,
                                                    Length = x.Length,
                                                    VendorId = x.VendorId ?? 0,
                                                    VendorCode = x.Vendor.VendorCode,
                                                    VendorName = x.Vendor.VendorName,
                                                    MaterialTypeId = x.Material.MaterialTypeId,
                                                    MaterialCode = x.Material.MaterialType.DiagramColor
                                                }).ToList();
                                foreach (var drawer in drawers) {
                                    var entity = new MaterialInvShelfDiagramModel {
                                        ShelfName = drawer.ShelfName,
                                        ColumnName = drawer.ColumnName,
                                        RowName = drawer.RowName,
                                        AdditionName = drawer.AdditionName,
                                    };
                                    var onShelvesById = onShelves.Where(x => x.DrawerId == drawer.DrawerId).ToList();
                                    if (!onShelvesById.Any()) continue;
                                    foreach (var onShelf in onShelvesById) {
                                        var detail = new MaterialInvShelfDiagramDetailModel { };
                                        var materialInvsById = materialInvs.FirstOrDefault(x => x.MaterialInventoryId == onShelf.ReferenceInvId);
                                        if (materialInvsById != null) {
                                            detail.ShelfBarcode = materialInvsById.ShelfBarcode;
                                            detail.ReferenceInvId = materialInvsById.MaterialInventoryId;
                                            detail.ReferenceCode = materialInvsById.MaterialName;
                                            detail.ReferenceInvCode = MyUtilities.Material.GetMaterialDesignNo(materialInvsById.OutDiameter,
                                                materialInvsById.InDiameter,
                                                materialInvsById.DiameterType,
                                                materialInvsById.Shape,
                                                materialInvsById.Length);
                                            detail.LotNumber = materialInvsById.LotNumber;
                                            detail.Quantity = materialInvsById.QuantityKg;
                                            detail.ImportQuantity = materialInvsById.TotalImportKg;
                                            if (materialInvsById.ImportDate != null) {
                                                detail.ImportDateStr = materialInvsById.ImportDate.Value.ToString("dd/MM/yy");
                                                var yearOld = (DateTime.Now - materialInvsById.ImportDate.Value).TotalDays / 365;
                                                if (yearOld > 2) {
                                                    detail.MaterialStateColor = over2YearColor;
                                                    detail.MaterialStateCode = "2";
                                                }
                                                else if (yearOld > 1) {
                                                    detail.MaterialStateColor = over1YearColor;
                                                    detail.MaterialStateCode = "1";
                                                }
                                                else {
                                                    detail.MaterialStateColor = below1YearColor;
                                                    detail.MaterialStateCode = "0";
                                                }
                                            }
                                            else {
                                                detail.MaterialStateColor = over2YearColor;
                                                detail.MaterialStateCode = "2";
                                            }
                                            detail.VendorId = materialInvsById.VendorId;
                                            detail.VendorName = materialInvsById.VendorName;
                                            detail.MaterialTypeId = materialInvsById.MaterialTypeId;
                                            detail.MaterialTypeColor = materialInvsById.MaterialCode;
                                        }
                                        entity.Details.Add(detail);
                                    }
                                    model.Add(entity);
                                }
                            }
                            return PartialView("PageMaterialInvOnShelfCard", model);
                        default:
                            break;
                    }
                }


            }
            catch (Exception exception) {
                throw new Exception(exception.Message);
            }
            return PartialView("PageMaterialInvOnShelfCard", null);
        }

        #endregion

        #region process error

        [GridAction]
        public ActionResult SelectProcessError() {
            var model = new List<ProcessErrorModel>();
            try {
                model = GetProcessErrorModel();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProcessError", ex.Message);
            }
            return View(new GridModel(model));

        }

        private List<ProcessErrorModel> GetProcessErrorModel() {
            var model = new List<ProcessErrorModel>();
            using (var vfi = new tammaContext()) {
                var processErrors = vfi.ProcessErrors
                                       .OrderByDescending(pe => pe.Active)
                                       .ThenBy(pe => pe.Description);
                foreach (var processError in processErrors) {
                    var entity = new ProcessErrorModel {
                        Description = processError.Description,
                        Active = processError.Active,
                        ErrorId = processError.ErrorId,
                        ModifiedDate = processError.ModifiedDate,
                        ModifiedUser = processError.ModifiedUser
                    };
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertProcessError(ProcessErrorModel insert) {
            try {

                using (var vfi = new tammaContext()) {
                    var error = vfi.ProcessErrors.FirstOrDefault(pe => pe.Description.Equals(insert.Description.Trim()));
                    if (error == null) {
                        error = new ProcessError {
                            Active = true,
                            Description = insert.Description,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                        };
                        vfi.ProcessErrors.Add(error);
                        vfi.SaveChanges();
                    }
                    else {
                        if (!error.Active) {
                            error.Active = true;
                            vfi.SaveChanges();
                        }
                        else throw new AggregateException("Lỗi! Mô tả lỗi bị trùng.");
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProcessError", ex.Message);
            }
            return View(new GridModel(GetProcessErrorModel()));
        }

        [GridAction]
        public ActionResult UpdateProcessError(ProcessErrorModel update) {
            try {

                using (var vfi = new tammaContext()) {
                    var error =
                        vfi.ProcessErrors.FirstOrDefault(
                            pe => pe.Description.Equals(update.Description.Trim()) && pe.ErrorId != update.ErrorId);
                    if (error == null) {
                        error = vfi.ProcessErrors.FirstOrDefault(pe => pe.ErrorId == update.ErrorId);
                        if (error == null)
                            throw new AggregateException("Lỗi! Không tìm thấy dữ liệu cần cập nhật!");
                        error.Active = update.Active;
                        error.Description = update.Description;
                        error.ModifiedDate = DateTime.Now;
                        error.ModifiedUser = HttpContext.User.Identity.Name;
                        vfi.SaveChanges();
                    }
                    else {
                        if (!error.Active) {
                            error.Active = true;
                            vfi.SaveChanges();
                        }
                        else throw new AggregateException("Lỗi! Mô tả lỗi bị trùng.");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProcessError", ex.Message);
            }
            return View(new GridModel(GetProcessErrorModel()));
        }

        public ActionResult SelectComboBoxProductError() {
            var model = new List<ProcessErrorModel>();
            using (var vfi = new tammaContext()) {
                var errors = vfi.ProcessErrors.Where(pe => pe.Active).OrderBy(pe => pe.Description);
                foreach (var error in errors) {
                    var entity = new ProcessErrorModel {
                        ErrorId = error.ErrorId,
                        Description = error.Description
                    };
                    model.Add(entity);
                }
                return new JsonResult {
                    Data =
                        new SelectList(model, "ErrorId", "Description")
                };
            }
        }
        #endregion

    }
}
