using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Server.Core.DataModel.Models.Inv;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Areas.Sales.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models;
using System.Threading;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Controllers {
    public class InvController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        private readonly WarehouseController _warehouseController;

        [InjectionConstructor]
        public InvController(IUnitOfWork unitOfWork, WarehouseController warehouseController
            ) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            _warehouseController = warehouseController;
        }

        #region View
        // View
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
        public ActionResult MaterialInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductionLockManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult MaterialOnMachineManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult InventoryCard() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MaterialInventoryCard() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult CncInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult DefectInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult HeatTreatmentInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult PlatingInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProcessingInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult Production2InvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult Production2InvManagementSplit() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult QcInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult SurfaceTreatmentInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult WaitingPlatingInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult FinishInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult PackingInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ICForCustomer() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult Production2WaitingInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductInvStatistic() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MaterialOnShelf() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult MaterialInventoryShelfDiagram() {

            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            var groups = new List<MaterialInvShelfDiagramGroupModel>();
            var model = new MaterialInvShelfDiagramViewModel { };
            try {
                using (var vfi = new tammaContext()) {
                    var materialTypes = (from x in vfi.MaterialTypes
                                         where x.MaterialClassifiedId == 1 && x.Active
                                         orderby x.IdentityCode
                                         select new MaterialTypeModel {
                                             MaterialTypeId = x.MaterialTypeId,
                                             MaterialTypeName = x.MaterialTypeName,
                                             Count = 0,
                                             DiagramColor = x.DiagramColor,
                                         }).ToList();
                    var shelves = (from x in vfi.InventoryShelves
                                   where x.Active && x.ClassifiedId == 1
                                   orderby x.ShelfName
                                   select new {
                                       x.ShelfId,
                                       x.ShelfName,
                                       x.MaxColumn,
                                       x.MaxRow,
                                   }).ToList();
                    var shelfIds = shelves.Select(x => x.ShelfId).ToList();
                    var drawers = (from x in vfi.InventoryDrawers
                                   where shelfIds.Contains(x.ShelfId) && x.Active
                                   orderby x.AdditionName, x.RowName, x.ColumnName
                                   select new InventoryDrawerModel {
                                       DrawerId = x.DrawerId,
                                       ShelfId = x.ShelfId,
                                       ColumnName = x.ColumnName,
                                       RowName = x.RowName,
                                       ShelfName = x.InventoryShelf.ShelfName,
                                       AdditionName = x.AdditionName,
                                       ReferenceInvId = x.ReferenceInvId ?? 0
                                   }).ToList();
                    var onShelves = (from x in vfi.OnShelves
                                     where x.Active
                                     select x).ToList();
                    var invIds = onShelves.Select(x => x.ReferenceInvId).Distinct().ToList();
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
                    model.Types = materialTypes;
                    var over2YearColor = "FF6666";
                    var over1YearColor = "FFC061";
                    var below1YearColor = "";
                    var states = new List<MaterialInvStateModel>();
                    states.Add(new MaterialInvStateModel {
                        Code = "0",
                        Name = "Dưới 1 năm",
                        Color = below1YearColor,
                    });
                    states.Add(new MaterialInvStateModel {
                        Code = "1",
                        Name = "1-2 năm",
                        Color = over1YearColor,
                    });
                    states.Add(new MaterialInvStateModel {
                        Code = "2",
                        Name = "Trên 2 năm",
                        Color = over2YearColor,
                    });
                    model.States = states;
                    var onShelfts = new List<MaterialInvShelfDiagramDetailModel>();
                    foreach (var shelf in shelves) {
                        var group = new MaterialInvShelfDiagramGroupModel {
                            ShelfName = shelf.ShelfName,
                            ColumnCount = shelf.MaxColumn,
                            RowCount = shelf.MaxRow,
                        };
                        //var columns = drawers.Where(x => x.ShelfId == shelf.ShelfId).Select(x => x.RowName).OrderBy(x => x).Distinct().ToList();
                        //foreach (var column in columns) {
                        //    var entity = new MaterialInvShelfDiagramColumnModel {
                        //        ColumnName = column,
                        //    };
                        var drawersById = drawers.Where(x => x.ShelfId == shelf.ShelfId).ToList();
                        foreach (var drawer in drawersById) {
                            var entity = new MaterialInvShelfDiagramModel {
                                ShelfName = drawer.ShelfName,
                                ColumnName = drawer.ColumnName,
                                RowName = drawer.RowName,
                                AdditionName = drawer.AdditionName,
                            };
                            var onShelvesById = onShelves.Where(x => x.DrawerId == drawer.DrawerId).ToList();
                            if (!onShelvesById.Any()) {
                                var detail = new MaterialInvShelfDiagramDetailModel {
                                    ReferenceInvId = 0,
                                    ReferenceCode = "Chỗ trống",
                                    MaterialStateCode = "2",
                                    MaterialStateColor = over2YearColor
                                };
                                entity.Details.Add(detail);
                            }
                            else {
                                foreach (var onShelf in onShelvesById) {
                                    var detail = new MaterialInvShelfDiagramDetailModel { };
                                    var materialInvsById = materialInvs.FirstOrDefault(x => x.MaterialInventoryId == onShelf.ReferenceInvId);
                                    if (materialInvsById != null) {
                                        detail.ReferenceInvId = materialInvsById.MaterialInventoryId;
                                        detail.ReferenceCode = materialInvsById.MaterialName +
                                            MyUtilities.Material.GetMaterialDesignNo(materialInvsById.OutDiameter,
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
                                    onShelfts.Add(detail);
                                }
                            }

                            group.Drawers.Add(entity);
                        }

                        groups.Add(group);
                    }

                    var notOnShelfInvs = (from x in vfi.MaterialInventories
                                          where !invIds.Contains(x.MaterialInventoryId) && x.TotalQty > 0
                                          orderby x.Material.MaterialType.IdentityCode
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
                    if (notOnShelfInvs.Any()) {
                        var notOnShelf = new MaterialInvShelfDiagramModel { };
                        foreach (var materialInvsById in notOnShelfInvs) {
                            var detail = new MaterialInvShelfDiagramDetailModel {
                                ReferenceInvId = materialInvsById.MaterialInventoryId,
                                ReferenceCode = materialInvsById.MaterialName +
                                MyUtilities.Material.GetMaterialDesignNo(materialInvsById.OutDiameter,
                                materialInvsById.InDiameter,
                                materialInvsById.DiameterType,
                                materialInvsById.Shape,
                                materialInvsById.Length),
                                LotNumber = materialInvsById.LotNumber,
                                Quantity = materialInvsById.QuantityKg,
                                ImportQuantity = materialInvsById.TotalImportKg,
                                VendorId = materialInvsById.VendorId,
                                VendorName = materialInvsById.VendorName,
                                MaterialTypeId = materialInvsById.MaterialTypeId,
                                MaterialTypeColor = materialInvsById.MaterialCode
                            };
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
                            notOnShelf.Details.Add(detail);
                            onShelfts.Add(detail);
                        }
                        model.NotOnShelf = notOnShelf;
                    }
                    foreach (var state in model.States) {
                        state.Count = onShelfts.Where(x => x.MaterialStateCode.Equals(state.Code))
                            .Select(x => x.ReferenceInvId).Distinct().Count();
                    }
                    foreach (var type in model.Types) {
                        type.Count = onShelfts.Where(x => x.MaterialTypeId == type.MaterialTypeId)
                            .Select(x => x.ReferenceInvId).Distinct().Count();
                    }
                    var vendorIds = onShelfts.Select(x => x.VendorId).Distinct().ToList();
                    var vendors = (from x in vfi.Vendors
                                   where vendorIds.Contains(x.VendorId)
                                   orderby x.VendorName
                                   select new VendorModel {
                                       VendorId = x.VendorId,
                                       VendorCode = x.VendorCode,
                                       VendorName = x.VendorName,
                                       Count = 0
                                   }).ToList();
                    model.Vendors = vendors;
                    foreach (var vendor in model.Vendors) {
                        vendor.Count = onShelfts.Where(x => x.VendorId == vendor.VendorId)
                            .Select(x => x.ReferenceInvId).Distinct().Count();
                    }
                }
                model.Groups = groups;
            }
            catch (Exception) {

            }
            ViewData = GetPageConfigData();
            ViewData["BackgroundImage"] = "";
            return View(model);
        }
        #endregion

        #region MaterialInventory

        public ActionResult AutoCompletedMaterialInventory(string text, int? materialTypeId) {
            List<string> model = new List<string>();
            using (var vfi = new tammaContext()) {
                if (materialTypeId != null && materialTypeId != 0)
                    model = vfi.Materials.Where(m => m.MaterialTypeId == materialTypeId)
                        .OrderBy(x => new { x.MaterialTypeId, x.MaterialName, x.Shape, x.DiameterType, x.OutDiameter, x.InDiameter })
                        .Select(m => m.MaterialCode)
                        .ToList();
                if (!string.IsNullOrWhiteSpace(text)) {
                    if (model.Any())
                        model = model.Where(m => m.Contains(text)).ToList();
                    else
                        model = vfi.Materials.Where(m => m.MaterialCode.Contains(text)).Select(m => m.MaterialCode).ToList();
                }
            }
            return new JsonResult {
                Data = model,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult CheckExportQuantity(MaterialInventoryImportModel importModel) {

            if (importModel.Quantity <= importModel.TotalQty)
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [GridAction]
        public ActionResult SelectPrepareMaterialInventory(
            int materialTypeId, string materialCode, string lotNumber) {
            var model = new List<MaterialInventoryDomainModel>();
            if (materialTypeId == 0 && string.IsNullOrWhiteSpace(materialCode) && string.IsNullOrWhiteSpace(lotNumber))
                return View(new GridModel(model));

            using (var vfi = new tammaContext()) {
                var materials = vfi.Materials.Where(m => m.Active);
                if (!materials.Any())
                    return View(new GridModel(model));
                if (materialTypeId != 0)
                    materials = materials.Where(m => m.MaterialTypeId == materialTypeId);
                if (!string.IsNullOrWhiteSpace(materialCode))
                    materials = materials.Where(m => m.MaterialCode.Contains(materialCode));
                if (!string.IsNullOrWhiteSpace(lotNumber))
                    materials = materials.Where(m => m.MaterialCode.Contains(lotNumber));
                foreach (var material in materials) {
                    var materialInvs = vfi.MaterialInventories.Where(mi => mi.MaterialId == material.MaterialId);
                    if (materialInvs.Any()) {
                        foreach (var materialInventory in materialInvs) {
                            var entity = new MaterialInventoryDomainModel();
                            entity.MaterialCode = material.MaterialCode;
                            entity.MaterialId = material.MaterialId;
                            entity.TotalQty = 0;
                            entity.LotNumber = materialInventory.LotNumber;
                            entity.TotalQty = materialInventory.TotalQty;
                            entity.MaterialInventoryId = materialInventory.MaterialInventoryId;
                            model.Add(entity);
                        }
                    }
                    else {
                        var entity = new MaterialInventoryDomainModel();
                        entity.MaterialCode = material.MaterialCode;
                        entity.MaterialId = material.MaterialId;
                        entity.TotalQty = 0;
                        model.Add(entity);
                    }
                }
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectMaterialInPurchaseOrder(int? poId) {
            var model = new List<PurchaseOrderDetailModel>();
            if (poId == 0 || poId == null)
                return View(new GridModel(model));

            using (var vfi = new tammaContext()) {
                var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == poId);
                if (purchaseOrder != null) {
                    switch (purchaseOrder.MaterialClassifiedId) {
                        case 1:
                            foreach (var detail in purchaseOrder.PurchaseOrderDetails) {
                                var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == detail.ReferenceId);
                                var entity = new PurchaseOrderDetailModel {
                                    PurchaseOrderDetailId = detail.PurchaseOrderDetailId,
                                    MaterialId = detail.ReferenceId,
                                    Code = material.MaterialCode,
                                    VendorName = purchaseOrder.Vendor.VendorName,
                                    VendorCode = purchaseOrder.Vendor.VendorCode,
                                    OrderQty = detail.OrderQty,
                                    ReceivedQty = detail.ReceivedQty,
                                    RejectedQty = detail.RejectedQty,
                                    TotalQtyKg = detail.OrderQty - detail.ReceivedQty - detail.RejectedQty,
                                    Unit = detail.Unit
                                };
                                model.Add(entity);
                            }
                            break;
                        case 2:
                            foreach (var detail in purchaseOrder.PurchaseOrderDetails) {
                                var fuel = vfi.Fuels.FirstOrDefault(m => m.FuelId == detail.ReferenceId);
                                var entity = new PurchaseOrderDetailModel {
                                    PurchaseOrderDetailId = detail.PurchaseOrderDetailId,
                                    FuelId = detail.ReferenceId,
                                    Code = fuel.FuelCode,
                                    OrderQty = detail.OrderQty,
                                    ReceivedQty = detail.ReceivedQty,
                                    RejectedQty = detail.RejectedQty,
                                    TotalQtyKg = detail.OrderQty - detail.ReceivedQty - detail.RejectedQty,
                                    Unit = detail.Unit
                                };
                                model.Add(entity);
                            }
                            break;
                        case 3:
                            foreach (var detail in purchaseOrder.PurchaseOrderDetails) {
                                var tool = vfi.Tools.FirstOrDefault(m => m.ToolId == detail.ReferenceId);
                                var entity = new PurchaseOrderDetailModel {
                                    PurchaseOrderDetailId = detail.PurchaseOrderDetailId,
                                    ToolId = detail.ReferenceId,
                                    Code = tool.ToolCode,
                                    OrderQty = detail.OrderQty,
                                    ReceivedQty = detail.ReceivedQty,
                                    RejectedQty = detail.RejectedQty,
                                    TotalQtyKg = detail.OrderQty - detail.ReceivedQty - detail.RejectedQty,
                                    Unit = detail.Unit
                                };
                                model.Add(entity);
                            }
                            break;
                    }
                }

            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectMaterialInventoryByRotate(
            int materialTypeId
            , int warehouseIssue, int warehouseReceipt
            , string eoi) {
            //var warehouseId = eoi ? warehouseIssue : warehouseReceipt;

            return View(new GridModel(new List<MaterialInventoryRotateModel>()));
        }

        [GridAction]
        public ActionResult SelectInputMaterialInventory(string materialIds) {
            if (string.IsNullOrWhiteSpace(materialIds))
                return View(new GridModel(new List<MaterialInventoryModel>()));

            //int[] checkedRecords;
            List<int> checkedRecords;
            try {

                //checkedRecords = Convert.ToInt32(ids.Split(':'));
                //checkedRecords = Convert.ToInt32(ids.Split(':'));
                var lst = materialIds.Split(':');
                //checkedRecords = new int[lst.Count()];
                checkedRecords = new List<int>();
                for (var i = 0; i < lst.Count(); i++) {
                    //checkedRecords[i] = Convert.ToInt32(lst.ElementAt(i));
                    checkedRecords.Add(Convert.ToInt32(lst.ElementAt(i)));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<MaterialInventoryModel>()));
            }
            var lstIds = (List<int>)Session["SessionImportTransactionMaterialIds"];
            if (lstIds == null)
                lstIds = new List<int>();
            if (!lstIds.Any()) {
                lstIds = checkedRecords;
            }
            else {
                lstIds.AddRange(checkedRecords);
                //checkedRecords = new int[lstImportTransactionMaterialIds.Count()];
                //checkedRecords.AddRange(lstImportTransactionMaterialIds);
            }
            var model = new List<MaterialInventoryModel>();
            lstIds = lstIds.Distinct().ToList();
            using (var vfi = new tammaContext()) {
                foreach (var id in lstIds) {
                    var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == id);
                    var materialInvs = vfi.MaterialInventories.Where(mi => mi.MaterialId == id && mi.TotalQty > 0);
                    if (materialInvs.Any()) {
                        foreach (var materialInventory in materialInvs) {
                            if (materialInventory.TotalQty == 0) continue;
                            var entity = new MaterialInventoryModel {
                                MaterialCode = material.MaterialCode,
                                MaterialId = id,
                                LotNumber = materialInventory.LotNumber,
                                TotalQty = materialInventory.TotalQty,
                                TotalQtyKg = materialInventory.TotalQty * materialInventory.UnitWeight,
                                UnitWeight = materialInventory.UnitWeight,
                                UnitPrice = materialInventory.UnitPrice,
                                VendorId = materialInventory.VendorId.Value,
                                VendorName = materialInventory.Vendor.VendorName,
                                VendorCode = materialInventory.Vendor.VendorCode,
                                MaterialInvOnMachine = 0,
                                StoreCode = materialInventory.StoreCode,
                                Length = materialInventory.Length
                            };
                            var materialInvOnMachine =
                                vfi.MaterialInvOnMachines.Where(
                                    mi =>
                                    materialInventory.MaterialInventoryId == mi.MaterialInvId);
                            if (materialInvOnMachine.Any()) {
                                entity.MaterialInvOnMachine = materialInvOnMachine.Sum(mi => mi.TotalQuantity);
                            }
                            model.Add(entity);
                        }
                    }
                    else {
                        var entity = new MaterialInventoryModel {
                            MaterialCode = material.MaterialCode,
                            MaterialId = id,
                            LotNumber = "",
                            TotalQty = 0,
                            UnitWeight = 0,
                            UnitPrice = 0,
                            VendorId = 0,
                            VendorName = "",
                            VendorCode = "",
                            MaterialInvOnMachine = 0,
                        };
                        model.Add(entity);
                    }
                }
            }
            Session["SessionImportTransactionMaterialIds"] = lstIds;
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectExportInputMaterialInventory(string materialInvIds) {
            if (string.IsNullOrWhiteSpace(materialInvIds))
                return View(new GridModel(new List<MaterialInventoryModel>()));

            //int[] checkedRecords;
            List<int> checkedRecords;
            try {

                //checkedRecords = Convert.ToInt32(ids.Split(':'));
                //checkedRecords = Convert.ToInt32(ids.Split(':'));
                var lst = materialInvIds.Split(':');
                //checkedRecords = new int[lst.Count()];
                checkedRecords = new List<int>();
                for (var i = 0; i < lst.Count(); i++) {
                    //checkedRecords[i] = Convert.ToInt32(lst.ElementAt(i));
                    checkedRecords.Add(Convert.ToInt32(lst.ElementAt(i)));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<MaterialInventoryModel>()));
            }
            var lstImportTransactionMaterialIds = (List<int>)Session["SessionExportTransactionMaterialIds"];
            if (lstImportTransactionMaterialIds == null)
                lstImportTransactionMaterialIds = new List<int>();
            if (!lstImportTransactionMaterialIds.Any()) {
                lstImportTransactionMaterialIds = checkedRecords;
            }
            else {
                lstImportTransactionMaterialIds.AddRange(checkedRecords);
                //checkedRecords = new int[lstImportTransactionMaterialIds.Count()];
                //checkedRecords.AddRange(lstImportTransactionMaterialIds);
            }
            var model = new List<MaterialInventoryModel>();
            lstImportTransactionMaterialIds = lstImportTransactionMaterialIds.Distinct().ToList();
            using (var vfi = new tammaContext()) {
                foreach (var id in lstImportTransactionMaterialIds) {
                    var materialInv = vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == id);
                    if (materialInv.TotalQty == 0) continue;
                    var entity = new MaterialInventoryModel {
                        MaterialCode = materialInv.Material.MaterialCode,
                        MaterialInventoryId = id,
                        TotalQty = materialInv.TotalQty,
                        TotalQtyKg = materialInv.TotalQty * materialInv.UnitWeight,
                        MaterialId = materialInv.Material.MaterialId,
                        VendorId = materialInv.VendorId ?? 0,
                        VendorCode = materialInv.Vendor.VendorCode,
                        VendorName = materialInv.Vendor.VendorName,
                        MaterialInvOnMachine = 0,
                        LotNumber = materialInv.LotNumber,
                        Length = materialInv.Length,
                        UnitWeight = materialInv.UnitWeight,
                    };
                    var materialInvOnMachine =
                        vfi.MaterialInvOnMachines.Where(
                            mi => id == mi.MaterialInvId);
                    if (materialInvOnMachine.Any()) {
                        entity.MaterialInvOnMachine = materialInvOnMachine.Sum(mi => mi.TotalQuantity);
                    }
                    model.Add(entity);
                }
                Session["SessionExportTransactionMaterialIds"] = lstImportTransactionMaterialIds;
                return View(new GridModel(model));
            }
        }

        [GridAction]
        public ActionResult SelectMaterialInPO(string ids, int poId) {
            if (string.IsNullOrWhiteSpace(ids))
                return View(new GridModel(new List<MaterialInventoryModel>()));

            if (poId == 0)
                return View(new GridModel(new List<MaterialInventoryModel>()));

            //int[] checkedRecords;
            var checkedRecords = new List<long>();
            try {
                var lst = ids.Split(':');
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords.Add(Convert.ToInt64(lst.ElementAt(i)));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<MaterialInventoryModel>()));
            }
            var model = new List<MaterialInventoryModel>();

            using (var vfi = new tammaContext()) {
                var poDetails = (from pod in vfi.PurchaseOrderDetails
                                 where checkedRecords.Contains(pod.PurchaseOrderDetailId)
                                 select new {
                                     pod.ReferenceId,
                                     pod.PurchaseOrderDetailId,
                                     pod.PurchaseOrder.Vendor,
                                     pod.OrderQty,
                                     pod.ReceivedQty,
                                     pod.RejectedQty,
                                     pod.UnitPrice,
                                     pod.Unit
                                 }).ToList();
                var materialIds = poDetails.Select(pod => pod.ReferenceId).ToList();
                var materials = from m in vfi.Materials
                                where materialIds.Contains(m.MaterialId)
                                select new {
                                    m.MaterialId,
                                    m.MaterialCode,
                                    m.MaterialName,
                                    m.OutDiameter,
                                    m.InDiameter,
                                    m.Shape,
                                    m.DiameterType
                                };
                foreach (var id in checkedRecords) {
                    var poDetail =
                        poDetails.FirstOrDefault(
                            pod => pod.PurchaseOrderDetailId == id);
                    var material = materials.FirstOrDefault(m => m.MaterialId == poDetail.ReferenceId);
                    var entity = new MaterialInventoryModel {
                        PurchaseOrderDetailId = poDetail.PurchaseOrderDetailId,
                        MaterialCode = material.MaterialCode,
                        VendorId = poDetail.Vendor.VendorId,
                        VendorCode = poDetail.Vendor.VendorCode,
                        VendorName = poDetail.Vendor.VendorName,
                        MaterialId = material.MaterialId,
                        TotalQtyKg = poDetail.OrderQty - poDetail.ReceivedQty - poDetail.RejectedQty,
                        UnitPrice = poDetail.UnitPrice,
                        Unit = poDetail.Unit,
                        MaterialName = material.MaterialName,
                        Shape = material.Shape,
                        DiameterType = material.DiameterType,
                        OutDiameter = material.OutDiameter,
                        InDiameter = material.InDiameter,
                        Length = 0
                    };
                    if (entity.TotalQtyKg < 0)
                        entity.TotalQtyKg = 0;
                    model.Add(entity);
                }
            }
            return
                View(
                    new GridModel(
                        model.OrderBy(m => m.MaterialName)
                             .ThenBy(m => m.Shape)
                             .ThenBy(m => m.DiameterType)
                             .ThenBy(m => m.OutDiameter)
                             .ThenBy(m => m.InDiameter)));
        }

        [GridAction]
        public ActionResult SelectMaterialInventory(string materialIds, int warehouseId) {
            return View(new GridModel(new List<MaterialInventoryModel>()));

        }

        [GridAction]
        public ActionResult SelectMaterialInventoryByProductId(int productId) {
            var model = new List<ProductionMaterialModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null)
                        throw new AggregateException("Lỗi! Mất đăng nhập! Vui lòng đăng nhập lại!");
                    var materials = vfi.ProductionMaterials.Where(ps => ps.ProductId == productId).OrderBy(m => m.Priority);
                    if (!materials.Any())
                        return View(new GridModel(model));
                    var materialIds = materials.Select(m => m.MaterialId).ToList();
                    var materialInvs = from mi in vfi.MaterialInventories
                                       where materialIds.Contains(mi.MaterialId) && Math.Round(mi.TotalQty) != 0
                                       select new {
                                           mi.MaterialId,
                                           mi.TotalQty,
                                           mi.UnitWeight
                                       };
                    foreach (var material in materials) {
                        var entity = new ProductionMaterialModel {
                            RealMaterialId = material.RealMaterialId,
                            MaterialId = material.MaterialId,
                            MaterialCode = material.Material.MaterialCode,
                            ProductId = material.ProductId,
                            Priority = material.Priority,
                            Note = material.Note
                        };
                        var materialInvsById = materialInvs.Where(mi => mi.MaterialId == material.MaterialId);
                        if (materialInvsById.Any()) {
                            foreach (var materialInv in materialInvsById) {
                                entity.MaterialInv = materialInv.TotalQty;
                                entity.MaterialInvKg = materialInv.TotalQty * materialInv.UnitWeight;
                            }
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionSectionById", ex.Message);
            }
            return View(new GridModel(model));
        }
        [GridAction]
        public ActionResult InputMaterialInventoryByRotate(string materialIds
            //public ActionResult SelectStockOrderMaterialInventory(int[] checkedRecords
            //, int warehouseId, bool eoi
            , int warehouseIssue, int warehouseReceipt
            ) {
            return View(new GridModel(new List<MaterialInventoryRotateModel>()));
        }

        #endregion

        #region MaterailInventoryPeriod

        private List<MaterialInvOnMachineModel> GetProductionMaterialOnMachine(string fromDate, string toDate) {
            var model = new List<MaterialInvOnMachineModel>();
            if (string.IsNullOrWhiteSpace(fromDate))
                return model;
            try {
                var ci = new CultureInfo("vi-VN");
                var fDate = Convert.ToDateTime(fromDate, ci);
                var tDate = Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    var exportMaterials = (from ed in vfi.ExportMaterialDetails
                                           where
                                               ed.ExportMaterial.ExportDate >= fDate &&
                                               ed.ExportMaterial.ExportDate <= tDate &&
                                               ed.ExportMaterial.Transaction.Status ==
                                               (byte)MyUtilities.Transaction.Status.Approved &&
                                               ed.MachineId != null
                                           select new {
                                               MachineId = ed.MachineId.Value,
                                               ed.Machine.MachineName,
                                               MaterialInvId = ed.MaterialInvId,
                                               ed.MaterialInventory,
                                               Quantity = ed.Quantity,
                                           });
                    var muInShift = from mud in vfi.MaterialUseDetails
                                    where
                                        mud.MaterialUseInShift.UsedDate >= fDate &&
                                        mud.MaterialUseInShift.UsedDate <= tDate &&
                                        mud.MaterialUseInShift.Status == (byte)MyUtilities.Transaction.Status.Approved
                                    select new {
                                        mud.DetailId,
                                        mud.MachineId,
                                        mud.Machine.MachineName,
                                        mud.MaterialInvId,
                                        mud.MaterialInventory,
                                        mud.IsDetroy,
                                        mud.EditQuantity,
                                        mud.EditQuantity2,
                                        mud.MaterialUseInShift.Type
                                    };
                    var importSxDetails = from id in vfi.ImportFormSX1Detail
                                          where
                                              id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                              id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                              id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                                .Transaction.Status ==
                                              (byte)MyUtilities.Transaction.Status.Approved &&
                                              id.ImportFormSX1.MaterialUseDate >= fDate &&
                                              id.ImportFormSX1.MaterialUseDate <= tDate
                                          select new {
                                              id.Machine1.MachineName,
                                              id.Number1,
                                              id.Number2,
                                              id.Processing1,
                                              id.Processing2,
                                              id.DefectProduct1,
                                              id.DefectProduct2,
                                              id.MaterialInventory,
                                              id.ProductId,
                                              id.Product,
                                              id.ProductionRate,
                                              id.ProductWeight,
                                              id.MaterialUse1,
                                              id.MaterialUse2,
                                          };
                    var machines = vfi.Machines.Where(m => m.Active && m.MachineName.Contains("C"));
                    //foreach (var machine in machines)
                    //{
                    //    var entity = new MaterialInvOnMachineModel
                    //        {
                    //            MachineId = machine.MachineId,
                    //            MachineName = machine.MachineName,
                    //            MaterialId = period.MaterialInventory.MaterialId,
                    //            MaterialInvId = period.MaterialInvId,
                    //            MaterialDesign = MyUtilities.Material.GetMaterialInvDesignNo(period.MaterialInventory),
                    //            Length = period.MaterialInventory.Length / 1000,
                    //            LotNumber = period.MaterialInventory.LotNumber,
                    //            MaterialName = period.MaterialInventory.Material.MaterialName,
                    //            Shape = period.MaterialInventory.Material.Shape,
                    //            DiameterType = period.MaterialInventory.Material.DiameterType,
                    //            OutDiameter = period.MaterialInventory.Material.OutDiameter,
                    //            InDiameter = period.MaterialInventory.Material.InDiameter,
                    //            DateString = toDate,
                    //            VendorCode = period.MaterialInventory.Vendor.VendorCode,
                    //        };
                    //    model.Add(entity);
                    //}

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetProductionMaterialOnMachine", ex.Message);
            }
            return model;
        }

        List<MaterialInvOnMachineModel> GetMaterialInvOnMachine(string fromDate, string toDate, string factory) {
            var model = new List<MaterialInvOnMachineModel>();
            if (string.IsNullOrWhiteSpace(fromDate))
                return model;
            try {
                var ci = new CultureInfo("vi-VN");
                var fDate = Convert.ToDateTime(fromDate, ci);
                var tDate = Convert.ToDateTime(toDate, ci).AddDays(1).AddSeconds(-1);
                using (var vfi = new tammaContext()) {
                    var machines = vfi.Machines.Where(x => x.ProcessingType.Warehouse.IsProduction).ToList();
                    //if (!string.IsNullOrWhiteSpace(factory)) {
                    //    if (factory.Equals(MyUtilities.Machine.FactoryVF2)) {
                    //        machines = machines.Where(x => x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //    else {
                    //        machines = machines.Where(x => !x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //}
                    var machineIds = vfi.Machines.Select(x => x.MachineId).ToList();
                    var exportMaterials = (from ed in vfi.ExportMaterialDetails
                                           where
                                               ed.ExportMaterial.ExportDate >= fDate &&
                                               ed.ExportMaterial.ExportDate <= tDate &&
                                               ed.ExportMaterial.Transaction.Status ==
                                               (byte)MyUtilities.Transaction.Status.Approved &&
                                               ed.MachineId != null &&
                                               machineIds.Contains(ed.MachineId.Value)
                                           select new {
                                               MachineId = ed.MachineId.Value,
                                               MaterialInvId = ed.MaterialInvId,
                                               Quantity = ed.Quantity,
                                           }).ToList();
                    var muInShift = (from mud in vfi.MaterialUseDetails
                                     where
                                         mud.MaterialUseInShift.UsedDate >= fDate &&
                                         mud.MaterialUseInShift.UsedDate <= tDate &&
                                         mud.MaterialUseInShift.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                         machineIds.Contains(mud.MachineId)
                                     select new {
                                         mud.DetailId,
                                         mud.MachineId,
                                         mud.MaterialInvId,
                                         mud.IsDetroy,
                                         mud.EditQuantity,
                                         mud.EditQuantity2,
                                         mud.MaterialUseInShift.Type
                                     }).ToList();
                    machineIds = (from mim in vfi.MaterialInvOnMachines
                                  where mim.TotalQuantity > 0 &&
                                        machineIds.Contains(mim.MachineId.Value)
                                  select mim.MachineId.Value).ToList();
                    var machineIds2 = exportMaterials.Select(ed => ed.MachineId).ToList();
                    machineIds.AddRange(machineIds2);
                    machineIds2 = muInShift.Select(ed => ed.MachineId).ToList();
                    machineIds.AddRange(machineIds2);
                    machineIds = machineIds.Distinct().ToList();
                    var mimPeriods = (from p in vfi.MaterialInvOnMachinePeriods
                                      where
                                          p.PeriodDate <= tDate &&
                                          machineIds.Contains(p.MachineId.Value)
                                      select new {
                                          MachineId = p.MachineId.Value,
                                          p.Machine.MachineName,
                                          MaterialInvId = p.MaterialInvId.Value,
                                          p.MaterialInventory,
                                          p.EarlyQuantity,
                                          p.LastQuantity,
                                          p.PeriodDate
                                      }).ToList();
                    //var mimPeriodsByDate = mimPeriods.Where(mim => mim.PeriodDate >= fDate);
                    var loops = (from mim in mimPeriods
                                 select new {
                                     mim.MachineId,
                                     mim.MachineName,
                                     mim.MaterialInvId,
                                     mim.MaterialInventory,
                                 }).Distinct().OrderBy(mim => mim.MachineName).ToList();
                    foreach (var period in loops) {
                        var entity = new MaterialInvOnMachineModel {
                            MachineId = period.MachineId,
                            MachineName = period.MachineName,
                            MaterialId = period.MaterialInventory.MaterialId,
                            MaterialInvId = period.MaterialInvId,
                            MaterialDesign = MyUtilities.Material.GetMaterialInvDesignNo(period.MaterialInventory),
                            Length = period.MaterialInventory.Length / 1000,
                            LotNumber = period.MaterialInventory.LotNumber,
                            MaterialName = period.MaterialInventory.Material.MaterialName,
                            Shape = period.MaterialInventory.Material.Shape,
                            DiameterType = period.MaterialInventory.Material.DiameterType,
                            OutDiameter = period.MaterialInventory.Material.OutDiameter,
                            InDiameter = period.MaterialInventory.Material.InDiameter,
                            DateString = toDate,
                            VendorCode = period.MaterialInventory.Vendor.VendorCode,
                        };
                        var periods =
                            mimPeriods.Where(
                                mim => mim.MachineId == entity.MachineId && mim.MaterialInvId == entity.MaterialInvId)
                                      .ToList();
                        entity.LastQuantity = periods.Sum(mim => mim.LastQuantity - mim.EarlyQuantity);
                        periods = periods.Where(mim => mim.PeriodDate < fDate).ToList();
                        entity.EarlyQuantity = periods.Sum(mim => mim.LastQuantity - mim.EarlyQuantity);

                        var assign =
                            exportMaterials.Where(
                                ed => ed.MachineId == entity.MachineId && ed.MaterialInvId == entity.MaterialInvId)
                                           .ToList();
                        entity.AssignQuantity = assign.Sum(ed => ed.Quantity);
                        var useById = muInShift.Where(
                            mud => mud.MachineId == entity.MachineId &&
                                   mud.MaterialInvId == entity.MaterialInvId)
                                               .ToList();
                        if (useById.Any()) {
                            var use = useById.Where(mud => mud.Type == (int)MyUtilities.Material.UseType.Using).ToList();
                            entity.QuantityUse1 = use.Sum(mud => mud.EditQuantity);
                            entity.QuantityUse2 = use.Sum(mud => mud.EditQuantity2);
                            use = useById.Where(mud => mud.Type == (int)MyUtilities.Material.UseType.SendBack && mud.IsDetroy).ToList();
                            entity.Destroy = use.Sum(mud => mud.EditQuantity);
                            use = useById.Where(mud => mud.Type == (int)MyUtilities.Material.UseType.SendBack && !mud.IsDetroy).ToList();
                            entity.SendBack = use.Sum(mud => mud.EditQuantity);
                        }
                        if (!entity.Show) continue;
                        var tracks =
                            vfi.TrackUpMachines.Where(
                                t =>
                                t.MachineId == entity.MachineId && t.MaterialId == entity.MaterialId &&
                                t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                t.DeliveryDate != null)
                               .OrderBy(t => t.DeliveryDate).ToList();
                        var lastTrack = MyUtilities.Machine.LastTrackUpMachine(entity.MachineId, entity.MaterialId, null, DateTime.Now);
                        if (lastTrack != null) {
                            entity.ProductId = lastTrack.ProductId;
                            entity.ProductCode = lastTrack.ProductCode;
                            entity.Productivity = lastTrack.RealProductivity;
                            //entity.ProductionRate = lastTrack.RealRate;
                            entity.ProductionRate = MyUtilities.Product
                                .GetProductRate(3000, lastTrack.WorkPiece, lastTrack.ProductLength, lastTrack.KnifeCut);
                        }
                        if (entity.Destroy > 0)
                            entity.Note += "| Hủy " + entity.Destroy;
                        if (entity.SendBack > 0)
                            entity.Note += "| Trả NL " + entity.SendBack;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("MaterialInvPeriod", ex.Message);
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectMaterialInvOnMachineTotal(string fromDate, string toDate, string factory) {
            var model = new List<MaterialInvOnMachineModel>();
            try {
                model = GetMaterialInvOnMachine(fromDate, toDate, factory);
            }
            catch (Exception ex) {
                ModelState.AddModelError("MaterialInvPeriod", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<InventoryCardModel> GetInventoryCardPeriod(
            int periodType, int productId,
            DateTime fromDate, DateTime toDate,
            string lotNumber) {
            var model = new List<InventoryCardModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId && p.Active);
                    if (product == null)
                        throw new AggregateException("Lỗi! Sản phẩm không tìm thấy");
                    var productInvPeriods = (from pip in vfi.ProductInventoryPeriods
                                             where pip.ProductId == productId && pip.PeriodDate <= toDate
                                             select new {
                                                 Date = pip.PeriodDate,
                                                 pip.WarehouseId,
                                                 pip.EarlyPeriodQuantity,
                                                 pip.LastPeriodQuantity,
                                                 pip.Quantity,
                                                 pip.LotNumber,
                                                 pip.Transaction.WarehouseIssueId,
                                                 pip.Transaction.WarehouseReceiptId,
                                                 IsInternal = pip.Transaction.IsInternal ?? false,
                                             }).ToList();
                    if (!string.IsNullOrWhiteSpace(lotNumber) && !lotNumber.ToLower().Equals("all")) {
                        productInvPeriods = productInvPeriods.Where(pip => pip.LotNumber.Contains(lotNumber)).ToList();
                    }
                    //if (!productInvPeriods.Any()) return model;
                    var inTimePeriods = productInvPeriods.Where(x => x.Date >= fromDate).ToList();

                    var productions = (from sx in vfi.ImportFormSX1Detail
                                       where
                                           sx.ProductId == productId &&
                                           sx.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                           //sx.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                           sx.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                             .Transaction.Status ==
                                           (byte)MyUtilities.Transaction.Status.Approved &&
                                           sx.ImportFormSX1.ImportDate >= fromDate &&
                                           sx.ImportFormSX1.ImportDate <= toDate
                                           && sx.ImportFormSX1.Status == (byte)MyUtilities.Transaction.Status.Approved
                                       select new {
                                           Date = sx.ImportFormSX1.ImportDate,
                                           Quantity = (sx.Number1 + sx.Number2),
                                           Processing = (sx.Processing1 + sx.Processing2),
                                           LotNumber = sx.LotNumber + "",
                                       }).ToList();
                    if (!string.IsNullOrWhiteSpace(lotNumber) && !lotNumber.ToLower().Equals("all")) {
                        productions = productions.Where(pip => pip.LotNumber.Contains(lotNumber)).ToList();
                    }

                    var warehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                    //
                    var importsPeriod = inTimePeriods.Where(x => x.WarehouseReceiptId != null
                                                                    && warehouses.Contains(x.WarehouseReceiptId.Value))
                                                        .ToList();
                    var importProductions = inTimePeriods.Where(x => x.WarehouseIssueId == MyUtilities.Warehouse.Production1
                                                    && x.WarehouseId == MyUtilities.Warehouse.Production1
                                                    && x.WarehouseReceiptId != null).ToList();
                    var listHangTra = (from p in importsPeriod
                                       where p.WarehouseId == MyUtilities.Warehouse.Business &&
                                             p.WarehouseIssueId == MyUtilities.Warehouse.Business &&
                                             p.WarehouseReceiptId == MyUtilities.Warehouse.Finish
                                       select p).ToList();
                    var importsMore = (from p in importsPeriod
                                       where p.WarehouseIssueId == null && !p.IsInternal
                                       select p).ToList();
                    var importsInternal = (from p in importsPeriod
                                           where p.WarehouseIssueId == null && p.IsInternal
                                           select p).ToList();

                    //
                    var exportsPeriod = inTimePeriods.Where(x => x.WarehouseIssueId != null
                                                                    && warehouses.Contains(x.WarehouseIssueId.Value))
                                                         .ToList();
                    var exports = (from p in exportsPeriod
                                   where p.WarehouseId == MyUtilities.Warehouse.Business &&
                                         p.WarehouseIssueId == MyUtilities.Warehouse.Finish &&
                                         p.WarehouseReceiptId == MyUtilities.Warehouse.Business
                                   select p).ToList();
                    var exportsDestroy = (from p in exportsPeriod
                                          where
                                              p.WarehouseId == MyUtilities.Warehouse.Destroy &&
                                              p.WarehouseReceiptId == MyUtilities.Warehouse.Destroy
                                          select p).ToList();
                    var exportsInternal = (from p in exportsPeriod
                                           where p.WarehouseReceiptId == null &&
                                                 p.IsInternal
                                           select p).ToList();
                    var exportsDefect = (from p in exportsPeriod
                                         where
                                             p.WarehouseId == MyUtilities.Warehouse.Defect &&
                                             p.WarehouseReceiptId == MyUtilities.Warehouse.Defect
                                         select p).ToList();

                    var orderDetails = (from od in vfi.OrderDetails
                                        where
                                            (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                             od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess ||
                                             od.Order.Status == (byte)MyUtilities.Sales.Status.Completed) &&
                                            od.Order.DueDate != null &&
                                            od.Order.DueDate >= fromDate && od.Order.DueDate <= toDate &&
                                            od.ProductId == productId
                                        select new {
                                            Quantity = od.OrderQty.Value,
                                            Date = od.Order.DueDate.Value,
                                        }).ToList();
                    var forecasts = (from fo in vfi.ForecastOrders
                                     where
                                         fo.ProductId == productId &&
                                         fo.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                         fo.ForecastDate >= fromDate && fo.ForecastDate <= toDate
                                     select new {
                                         fo.Quantity,
                                         Date = fo.ForecastDate,
                                     }).ToList();
                    var minDates = new List<DateTime>();
                    var maxDates = new List<DateTime>();
                    if (productions.Any()) {
                        minDates.Add(productions.Min(x => x.Date));
                        maxDates.Add(productions.Max(x => x.Date));
                    }
                    if (inTimePeriods.Any()) {
                        minDates.Add(inTimePeriods.Min(x => x.Date));
                        maxDates.Add(inTimePeriods.Max(x => x.Date));
                    }
                    if (orderDetails.Any()) {
                        minDates.Add(orderDetails.Min(x => x.Date));
                        maxDates.Add(orderDetails.Max(x => x.Date));
                    }
                    if (forecasts.Any()) {
                        minDates.Add(forecasts.Min(x => x.Date));
                        maxDates.Add(forecasts.Max(x => x.Date));
                    }
                    fromDate = minDates.Min(x => x);
                    toDate = maxDates.Max(x => x);
                    var cncsProcess = GetCncRotateCardModel(productId, fromDate, toDate, lotNumber);
                    var production2sProcess = GetProduction2RotateCardModel(productId, fromDate, toDate, lotNumber);
                    var startDate = fromDate;
                    switch (periodType) {
                        case (int)MyUtilities.Transaction.PeriodType.Year:
                            startDate = new DateTime(fromDate.Year, 1, 1);
                            toDate = new DateTime(toDate.Year, 1, 1).AddYears(1).AddSeconds(-1);
                            break;
                        case (int)MyUtilities.Transaction.PeriodType.Month:
                            startDate = new DateTime(fromDate.Year, fromDate.Month, 1);
                            toDate = new DateTime(toDate.Year, toDate.Month, 1).AddMonths(1).AddSeconds(-1);
                            break;
                        case (int)MyUtilities.Transaction.PeriodType.Day:
                            startDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);
                            toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day).AddDays(1).AddSeconds(-1);
                            break;
                        default:
                            throw new AggregateException("Lỗi! Liên hệ admin");
                    }
                    var endDate = startDate;
                    while (startDate < toDate) {
                        var entity = new InventoryCardModel {
                            ProductId = productId,
                            Year = startDate.Year,
                            Month = startDate.Month,
                            Day = startDate.Day,
                            InventoryStart = 0,
                            ImportProduction = 0,
                            ImportVirtual = 0,
                            ExportSell = 0,
                            ExportDefect = 0,
                            ExportDestroy = 0,
                            LotNumber = lotNumber,
                        };
                        entity.InventoryStart = productInvPeriods.Where(pip => pip.Date < startDate
                                                                            && warehouses.Contains(pip.WarehouseId))
                                                                .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);

                        switch (periodType) {
                            case (int)MyUtilities.Transaction.PeriodType.Year:
                                endDate = startDate.AddYears(1);
                                break;
                            case (int)MyUtilities.Transaction.PeriodType.Month:
                                endDate = startDate.AddMonths(1);
                                break;
                            case (int)MyUtilities.Transaction.PeriodType.Day:
                                endDate = startDate.AddDays(1);
                                break;
                            default:
                                throw new AggregateException("Lỗi! Liên hệ admin");
                        }

                        var import = productions.Where(pip => pip.Date >= startDate && pip.Date < endDate);
                        if (import.Any()) {
                            entity.ProcessProduction = import.Sum(p => p.Quantity);
                            entity.ProductionProcessing = import.Sum(p => p.Processing);
                        }
                        entity.ImportProduction = importProductions.Where(x => x.Date >= startDate && x.Date < endDate)
                            .Sum(x => x.Quantity);
                        entity.ImportVirtual = importsMore.Where(pip => pip.Date >= startDate && pip.Date < endDate)
                                                            .Sum(p => p.Quantity);
                        entity.ImportInternal = importsInternal.Where(pip => pip.Date >= startDate && pip.Date < endDate)
                                                            .Sum(p => p.Quantity);
                        entity.SendBack = listHangTra.Where(pip => pip.Date >= startDate && pip.Date < endDate)
                                                        .Sum(p => p.Quantity);

                        //
                        entity.ExportSell = exports.Where(pip => pip.Date >= startDate && pip.Date < endDate)
                                                    .Sum(p => p.Quantity);
                        entity.ExportDefect = exportsDefect.Where(pip => pip.Date >= startDate && pip.Date < endDate)
                                                            .Sum(p => p.Quantity);
                        entity.ExportDestroy = exportsDestroy.Where(pip => pip.Date >= startDate && pip.Date < endDate)
                                                                .Sum(p => p.Quantity);
                        entity.ExportInternal = exportsInternal.Where(pip => pip.Date >= startDate && pip.Date < endDate)
                                                                .Sum(p => p.Quantity);

                        //
                        var cncsInDay = cncsProcess.Where(x => x.Date >= startDate && x.Date < endDate);
                        if (cncsInDay.Any()) {
                            entity.ProcessCNC = cncsInDay.Sum(x => x.Number);
                            entity.ProcessProcessing += cncsInDay.Sum(x => x.Processing);
                        }
                        var production2InDay = production2sProcess.Where(x => x.Date >= startDate && x.Date < endDate);
                        if (production2InDay.Any()) {
                            entity.ProcessProduction2 = production2InDay.Sum(x => x.Number);
                            entity.ProcessProcessing += production2InDay.Sum(x => x.Processing);
                        }
                        entity.Order = orderDetails.Where(x => x.Date >= startDate && x.Date < endDate)
                                                    .Sum(p => p.Quantity);
                        entity.Forecast = forecasts.Where(x => x.Date >= startDate && x.Date < endDate)
                                                    .Sum(p => p.Quantity);
                        if (entity.Show)
                            model.Add(entity);

                        startDate = endDate;
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectInventoryCardYear(int productId, string lotNumber) {
            var model = new List<InventoryCardModel>();
            if (productId == 0)
                return View(new GridModel(model));
            try {
                var fromDate = new DateTime(2000, 1, 1);
                var toDate = DateTime.Today.AddYears(1);
                model = GetInventoryCardPeriod(
                    (int)MyUtilities.Transaction.PeriodType.Year, productId,
                    fromDate, toDate,
                    lotNumber);
            }
            catch (Exception ex) {
                ModelState.AddModelError("MaterialInvPeriod", ex.Message);
            }
            return View(new GridModel(model));
        }


        [GridAction]
        public ActionResult SelectInventoryCardMonth(int productId, int year, string lotNumber) {
            var model = new List<InventoryCardModel>();
            if (productId == 0)
                return View(new GridModel(model));
            try {
                var fromDate = new DateTime(year, 1, 1);
                var toDate = fromDate.AddYears(1).AddSeconds(-1);
                model = GetInventoryCardPeriod(
                    (int)MyUtilities.Transaction.PeriodType.Month, productId,
                    fromDate, toDate,
                    lotNumber);
            }
            catch (Exception ex) {
                ModelState.AddModelError("MaterialInvPeriod", ex.Message);

            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectInventoryCardDay(int productId, int year, int month, string lotNumber) {
            var model = new List<InventoryCardModel>();
            if (productId == 0)
                return View(new GridModel(model));
            try {

                var fromDate = new DateTime(year, month, 1);
                var toDate = fromDate.AddMonths(1).AddSeconds(-1);
                model = GetInventoryCardPeriod(
                    (int)MyUtilities.Transaction.PeriodType.Day, productId,
                    fromDate, toDate,
                    lotNumber);

            }
            catch (Exception ex) {
                ModelState.AddModelError("MaterialInvPeriod", ex.Message);

            }
            return View(new GridModel(model));
        }

        private List<CncFormDetailModel> GetCncRotateCardModel(int productId, DateTime fromDate, DateTime toDate, string lotNumber) {
            var model = new List<CncFormDetailModel>();
            using (var vfi = new tammaContext()) {
                var transactions = (from td in vfi.TransactionDetails
                                    where td.ReferenceId == productId &&
                                            td.Transaction.WarehouseIssueId != null &&
                                            td.Transaction.Warehouse.IsCncMilling &&
                                            td.Transaction.WarehouseReceiptId != null &&
                                            !td.Transaction.Warehouse1.IsCncMilling &&
                                        //warehouseProduction2.Contains(td.Transaction.WarehouseIssueId.Value) &&
                                            td.Transaction.CreatedDate >= fromDate &&
                                            td.Transaction.CreatedDate <= toDate &&
                                            td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                            td.IsInternal != true
                                    orderby td.Transaction.CreatedDate
                                    select new {
                                        td.Transaction.CreatedDate,
                                        td.Quantity,
                                        td.QuantityKg,
                                        Warehouse = td.Transaction.Warehouse1,
                                        TransactionCode = td.Transaction.TransactionCode,
                                        ProductInvCode = td.ProductInvId != null ? td.ProductInventory.LotNumber : ""
                                    }).ToList();
                if (transactions == null || !transactions.Any()) return model;
                if (!string.IsNullOrWhiteSpace(lotNumber) && !lotNumber.ToLower().Equals("all")) {
                    transactions = transactions.Where(pip => pip.ProductInvCode.Contains(lotNumber)).ToList();
                }

                foreach (var transaction in transactions) {
                    var entity = new CncFormDetailModel {
                        Date = transaction.CreatedDate,
                        MachineName = "Phay CNC",
                        TransactionCode = transaction.TransactionCode,
                        ProductInvCode = transaction.ProductInvCode
                    };
                    model.Add(entity);
                    if (transaction.Warehouse.IsReprocessing) {
                        entity.Processing += transaction.Quantity;
                    }
                    else if (transaction.Warehouse.IsDefect == true) {
                        entity.DefectProduct += transaction.Quantity;
                    }
                    else {
                        entity.Number = transaction.Quantity;

                    }
                }
            }
            return model.Where(x => x.Number + x.Processing + x.DefectProduct > 0).ToList();
        }

        private List<CncFormDetailModel> GetCncFormDetailCardModel(int productId, DateTime fromDate, DateTime toDate, string lotNumber) {
            var model = new List<CncFormDetailModel>();
            using (var vfi = new tammaContext()) {
                var importCncs = (from sx in vfi.ImportFormCncDetails
                                  where
                                     sx.ProductId == productId &&
                                      sx.ImportFormCnc.ImportDate >= fromDate &&
                                      sx.ImportFormCnc.ImportDate <= toDate
                                  orderby sx.ImportFormCnc.ImportDate
                                  select new {
                                      Date = sx.ImportFormCnc.ImportDate,
                                      sx.MachineId,
                                      sx.Machine.MachineName,
                                      sx.Machine.ProcessingType.TypeName,
                                      sx.Number1,
                                      sx.Number2,
                                      sx.Processing1,
                                      sx.Processing2,
                                      sx.DefectProduct1,
                                      sx.DefectProduct2,
                                      sx.ImportFormCnc.Shift1Name,
                                      sx.ImportFormCnc.Shift2Name,
                                      ProductInvCode = sx.ProductInventory.LotNumber + "",
                                      TransactionCode = sx.ImportFormCnc.TransactionCode
                                  }).ToList();
                if (importCncs == null || !importCncs.Any()) return model;
                if (!string.IsNullOrWhiteSpace(lotNumber) && !lotNumber.ToLower().Equals("all")) {
                    importCncs = importCncs.Where(pip => pip.ProductInvCode.Contains(lotNumber)).ToList();
                }
                var transactionCodes = importCncs.Select(x => x.TransactionCode).Distinct().ToList();
                var transactions = vfi.Transactions.Where(x => transactionCodes.Contains(x.TransactionCode)).ToList();
                foreach (var importCnc in importCncs) {
                    var transaction = transactions.FirstOrDefault(x => x.TransactionCode == importCnc.TransactionCode);
                    if (transaction == null) continue;

                    var entity = new CncFormDetailModel {
                        Date = importCnc.Date,
                        MachineId = importCnc.MachineId,
                        MachineName = importCnc.MachineName,
                        MachineType = importCnc.TypeName,
                        Number1 = importCnc.Number1,
                        Number2 = importCnc.Number2,
                        Number = importCnc.Number1 + importCnc.Number2,
                        Processing1 = importCnc.Processing1,
                        Processing2 = importCnc.Processing2,
                        Processing = importCnc.Processing1 + importCnc.Processing2,
                        DefectProduct1 = importCnc.DefectProduct1,
                        DefectProduct2 = importCnc.DefectProduct2,
                        DefectProduct = importCnc.DefectProduct1 + importCnc.DefectProduct2,
                        ProductInvCode = importCnc.ProductInvCode,
                        TransactionCode = importCnc.TransactionCode,
                        Shift1Name = importCnc.Shift1Name,
                        Shift2Name = importCnc.Shift2Name,
                    };
                    model.Add(entity);
                    if (transaction.Status == (byte)MyUtilities.Transaction.Status.Approved) continue;

                    if (transaction.Warehouse1.IsReprocessing) {
                        entity.Processing1 = 0;
                        entity.Processing2 = 0;
                        entity.Processing = 0;
                    }
                    else if (transaction.Warehouse1.IsDefect == true) {
                        entity.DefectProduct1 = 0;
                        entity.DefectProduct2 = 0;
                        entity.DefectProduct = 0;
                    }
                    else {
                        entity.Number1 = 0;
                        entity.Number2 = 0;
                        entity.Number = 0;
                    }
                }
            }
            return model.Where(x => x.Number + x.Processing + x.DefectProduct > 0).ToList();
        }

        private List<CncFormDetailModel> GetProduction2RotateCardModel(int productId, DateTime fromDate, DateTime toDate, string lotNumber) {
            var model = new List<CncFormDetailModel>();
            //var warehouseProduction2 = MyUtilities.Warehouse.GetWarehouseIdProduction2_PROCESS();
            using (var vfi = new tammaContext()) {
                var transactions = (from td in vfi.TransactionDetails
                                    where td.ReferenceId == productId &&
                                            td.Transaction.WarehouseIssueId != null &&
                                            td.Transaction.Warehouse.IsProduction2 &&
                                            td.Transaction.WarehouseReceiptId != null &&
                                            !td.Transaction.Warehouse1.IsProduction2 &&
                                        //warehouseProduction2.Contains(td.Transaction.WarehouseIssueId.Value) &&
                                            td.Transaction.CreatedDate >= fromDate &&
                                            td.Transaction.CreatedDate <= toDate &&
                                            td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                            td.IsInternal != true
                                    orderby td.Transaction.CreatedDate
                                    select new {
                                        td.Transaction.CreatedDate,
                                        td.Quantity,
                                        td.QuantityKg,
                                        Warehouse = td.Transaction.Warehouse1,
                                        TransactionCode = td.Transaction.TransactionCode,
                                        ProductInvCode = td.ProductInvId != null ? td.ProductInventory.LotNumber : ""
                                    }).ToList();
                if (transactions == null || !transactions.Any()) return model;
                if (!string.IsNullOrWhiteSpace(lotNumber) && !lotNumber.ToLower().Equals("all")) {
                    transactions = transactions.Where(pip => pip.ProductInvCode.Contains(lotNumber)).ToList();
                }

                foreach (var transaction in transactions) {
                    var entity = new CncFormDetailModel {
                        Date = transaction.CreatedDate,
                        MachineName = "SX2",
                        TransactionCode = transaction.TransactionCode,
                        ProductInvCode = transaction.ProductInvCode
                    };
                    model.Add(entity);
                    if (transaction.Warehouse.IsReprocessing) {
                        entity.Processing += transaction.Quantity;
                    }
                    else if (transaction.Warehouse.IsDefect == true) {
                        entity.DefectProduct += transaction.Quantity;
                    }
                    else {
                        entity.Number = transaction.Quantity;

                    }
                }
            }
            return model.Where(x => x.Number + x.Processing + x.DefectProduct > 0).ToList();
        }

        private List<CncFormDetailModel> GetImportInternalCardModel(int productId, DateTime fromDate, DateTime toDate, string lotNumber) {
            var model = new List<CncFormDetailModel>();
            using (var vfi = new tammaContext()) {
                var transactions = (from td in vfi.TransactionDetails
                                    where td.ReferenceId == productId &&
                                           td.Transaction.WarehouseIssueId == null &&
                                            td.Transaction.WarehouseReceiptId != null &&
                                            td.Transaction.CreatedDate >= fromDate &&
                                            td.Transaction.CreatedDate <= toDate &&
                                            td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                            td.IsInternal == true
                                    select new {
                                        td.Transaction.CreatedDate,
                                        td.Quantity,
                                        td.QuantityKg,
                                        WarehouseName = td.Transaction.Warehouse1.WarehouseName,
                                        TransactionCode = td.Transaction.TransactionCode,
                                        ProductInvCode = td.LotNumber,
                                        //ProductInvCode = td.ProductInvId != null ? td.ProductInventory.LotNumber : "",
                                    }).ToList();
                if (transactions == null || !transactions.Any()) return model;
                if (!string.IsNullOrWhiteSpace(lotNumber) && !lotNumber.ToLower().Equals("all")) {
                    transactions = transactions.Where(pip => pip.ProductInvCode.Contains(lotNumber)).ToList();
                }
                foreach (var transaction in transactions) {
                    var entity = new CncFormDetailModel {
                        Date = transaction.CreatedDate,
                        MachineName = "Nhập nội bộ " + transaction.WarehouseName,
                        TransactionCode = transaction.TransactionCode,
                        ProductInvCode = transaction.ProductInvCode,
                        Number1 = transaction.Quantity
                    };
                    model.Add(entity);

                }
            }
            return model.Where(x => x.Number1 + x.Processing1 + x.DefectProduct1 > 0).ToList();
        }

        private List<CncFormDetailModel> GetExportInternalCardModel(int productId, DateTime fromDate, DateTime toDate, string lotNumber) {
            var model = new List<CncFormDetailModel>();
            using (var vfi = new tammaContext()) {
                var transactions = (from td in vfi.TransactionDetails
                                    where td.ReferenceId == productId &&
                                           td.Transaction.WarehouseIssueId != null &&
                                            td.Transaction.WarehouseReceiptId == null &&
                                            td.Transaction.CreatedDate >= fromDate &&
                                            td.Transaction.CreatedDate <= toDate &&
                                            td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                            td.IsInternal == true
                                    select new {
                                        td.Transaction.CreatedDate,
                                        td.Quantity,
                                        td.QuantityKg,
                                        WarehouseName = td.Transaction.Warehouse.WarehouseName,
                                        TransactionCode = td.Transaction.TransactionCode,
                                        ProductInvCode = td.LotNumber,
                                        //ProductInvCode = td.ProductInvId != null ? td.ProductInventory.LotNumber : "",
                                    }).ToList();
                if (transactions == null || !transactions.Any()) return model;
                if (!string.IsNullOrWhiteSpace(lotNumber) && !lotNumber.ToLower().Equals("all")) {
                    transactions = transactions.Where(pip => pip.ProductInvCode.Contains(lotNumber)).ToList();
                }
                foreach (var transaction in transactions) {
                    var entity = new CncFormDetailModel {
                        Date = transaction.CreatedDate,
                        MachineName = "Xuất nội bộ " + transaction.WarehouseName,
                        TransactionCode = transaction.TransactionCode,
                        ProductInvCode = transaction.ProductInvCode,
                        Number1 = transaction.Quantity
                    };
                    model.Add(entity);

                }
            }
            return model.Where(x => x.Number1 + x.Processing1 + x.DefectProduct1 > 0).ToList();
        }


        [GridAction]
        public ActionResult SelectMaterialInventoryCard(int materialId) {
            var model = new List<MaterialInventoryModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var materialInvs = vfi.MaterialInventories.Where(mi => mi.MaterialId == materialId)
                        .Select(x => new {
                            x.MaterialInventoryId,
                            x.MaterialId,
                            x.LotNumber,
                            x.TotalQty,
                            x.UnitWeight,
                            x.UnitPrice,
                            x.Length,
                            x.ImportQuantity,
                            x.ImportQuantityKg,
                            x.ImportDate,
                            x.EndDate,
                            x.VendorId,
                            x.Vendor.VendorCode,
                            x.Vendor.VendorName,
                            x.InfoImg,
                            x.InfoImg2,
                            x.ModifiedDate,
                        })
                        .ToList();
                    var materialInvIds = materialInvs.Select(mi => mi.MaterialInventoryId);
                    var importMores = (from id in vfi.ImportPurchaseOrderDetails
                                       where
                                           id.ImportPurchaseOrder.Transaction.Status ==
                                           (byte)MyUtilities.Transaction.Status.Approved
                                           && id.MaterialId == materialId
                                           && id.ImportPurchaseOrder.PurchaseOrderId == null
                                       select new {
                                           id.LotNumber,
                                           id.MaterialId,
                                           id.Length,
                                           id.VendorId,
                                           id.Quantity,
                                       }).ToList();
                    var destroys = (from em in vfi.ExportMaterialDetails
                                    where
                                        em.ExportMaterial.Transaction.Status ==
                                        (byte)MyUtilities.Transaction.Status.Approved &&
                                        em.MachineId == null &&
                                        em.MaterialId == materialId
                                    select new {
                                        em.MaterialInvId,
                                        Quantity = em.Quantity,
                                    }).ToList();
                    var productions = (from i in vfi.ImportFormSX1Detail
                                       where
                                           materialInvIds.Contains(i.MaterialInvId.Value) &&
                                           //i.MachineId == machineId &&
                                           i.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                           i.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                           i.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                            .Transaction.Status ==
                                           (byte)MyUtilities.Transaction.Status.Approved
                                       //&& i.Product.ProductCode.Contains(productCode)
                                       //&& i.MachineId == 68
                                       select new {
                                           i.MachineId,
                                           i.MaterialInvId,
                                           i.Product.ProductCode,
                                           MaterialUse = i.MaterialUse1 + i.MaterialUse2,
                                           Production = i.Number1 + i.Number2,
                                           Process = i.Processing1 + i.Processing2,
                                           Defect = i.DefectProduct1 + i.DefectProduct2,
                                           i.ImportFormSX1.ImportDate,
                                           i.ProductionRate,
                                           i.ProductWeight,
                                       }).ToList();
                    var destroysOnMachine = (from md in vfi.MaterialUseDetails
                                             where
                                                 md.MaterialUseInShift.Status ==
                                                 (byte)MyUtilities.Transaction.Status.Approved
                                                 && md.IsDetroy
                                                 && materialInvIds.Contains(md.MaterialInvId)
                                             select new {
                                                 md.MaterialInvId,
                                                 Quantity = md.EditQuantity + md.EditQuantity2
                                             }).ToList();
                    foreach (var materialInventory in materialInvs) {
                        var entity = new MaterialInventoryModel {
                            Length = materialInventory.Length,
                            MaterialInventoryId = materialInventory.MaterialInventoryId,
                            LotNumber = materialInventory.LotNumber,
                            TotalQty = materialInventory.TotalQty,
                            TotalQtyKg = materialInventory.TotalQty * materialInventory.UnitWeight,
                            UnitWeight = materialInventory.UnitWeight,
                            UnitPrice = materialInventory.UnitPrice,
                            EarlyQuantity = materialInventory.ImportQuantity,
                            EarlyQuantityKg = materialInventory.ImportQuantityKg,
                            ImportDate = materialInventory.ImportDate,
                            //EarlyQuantity = 
                            EndDate = materialInventory.EndDate ?? DateTime.Now,
                            VendorCode = materialInventory.VendorCode,
                            VendorName = materialInventory.VendorName,
                            VendorId = materialInventory.VendorId ?? 0,
                            InfoImg = materialInventory.InfoImg,
                            InfoImg2 = materialInventory.InfoImg2,
                            UploadDate = materialInventory.ModifiedDate.ToString("yyyyMMddhhmmss"),
                        };
                        if (string.IsNullOrWhiteSpace(entity.InfoImg)) {
                            entity.InfoImg = "askquestion.jpg";
                        }
                        if (string.IsNullOrWhiteSpace(entity.InfoImg2)) {
                            entity.InfoImg2 = "askquestion.jpg";
                        }
                        var importMoreDetails =
                            importMores.Where(
                                id =>
                                id.LotNumber.Equals(entity.LotNumber) &&
                                id.Length == entity.Length &&
                                id.VendorId == entity.VendorId).ToList();
                        if (importMoreDetails.Any()) {
                            entity.TotalImport = importMoreDetails.Sum(id => id.Quantity);
                        }
                        entity.TotalImportKg = entity.TotalImport * entity.UnitWeight;
                        var destroy = destroys.Where(ed => ed.MaterialInvId == entity.MaterialInventoryId).ToList();
                        if (destroy.Any()) {
                            entity.ExportDestroy = destroy.Sum(ed => ed.Quantity);
                        }
                        var productionsById = productions.Where(id => id.MaterialInvId == entity.MaterialInventoryId).ToList();
                        if (productionsById.Any()) {
                            entity.ExportUse = productionsById.Sum(id => id.MaterialUse);
                        }
                        var destroysById = destroysOnMachine.Where(md => md.MaterialInvId == entity.MaterialInventoryId).ToList();
                        if (destroysById.Any()) {
                            entity.DestroyOnMachine = destroysById.Sum(ed => ed.Quantity);
                        }
                        var materialOnMachines =
                            vfi.MaterialInvOnMachines.Where(mim => mim.MaterialInvId == entity.MaterialInventoryId).ToList();
                        if (materialOnMachines.Any()) {
                            entity.MaterialInvOnMachine = materialOnMachines.Sum(mim => mim.TotalQuantity);
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterialInventoryCard", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.VendorName).ThenBy(m => m.ImportDate)));
        }

        [GridAction]
        public ActionResult SelectMaterialInventoryCardYear(int materialInvId) {
            var model = new List<InventoryCardModel>();
            if (materialInvId == 0)
                return View(new GridModel(model));
            try {
                using (var vfi = new tammaContext()) {
                    var materialInv =
                        vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == materialInvId);
                    var materialPeriods = from mip in vfi.MaterialInventoryPeriods
                                          where mip.MaterialInventoryId == materialInvId
                                          select new {
                                              mip.PeriodDate,
                                              Quantity = mip.LastPeriodQuantity - mip.EarlyPeriodQuantity
                                          };
                    var onMachinePeriods = from mimp in vfi.MaterialInvOnMachinePeriods
                                           where mimp.MaterialInvId == materialInvId
                                           select new {
                                               mimp.PeriodDate,
                                               Quantity = mimp.LastQuantity - mimp.LastQuantity
                                           };
                    var imports = from id in vfi.ImportPurchaseOrderDetails
                                  where
                                      id.ImportPurchaseOrder.Transaction.Status ==
                                      (byte)MyUtilities.Transaction.Status.Approved
                                      && id.MaterialId == materialInv.MaterialId
                                      && id.LotNumber.Equals(materialInv.LotNumber)
                                      && id.VendorId == materialInv.VendorId
                                      && id.Length == materialInv.Length
                                  //&& id.ImportPurchaseOrder.PurchaseOrderId == null
                                  select new {
                                      id.ImportPurchaseOrder.ImportDate,
                                      id.ImportPurchaseOrder.PurchaseOrderId,
                                      id.Quantity,
                                  };
                    var exports = (from em in vfi.ExportMaterialDetails
                                   where
                                       em.ExportMaterial.Transaction.Status ==
                                       (byte)MyUtilities.Transaction.Status.Approved &&
                                       materialInvId == em.MaterialInvId
                                   select new {
                                       em.ExportMaterial.ExportDate,
                                       em.MachineId,
                                       Quantity = em.Quantity,
                                   }).ToList();
                    var uses = from md in vfi.MaterialUseDetails
                               where
                                   md.MaterialUseInShift.Status ==
                                   (byte)MyUtilities.Transaction.Status.Approved
                                   //&& dom.IsDetroy
                                   && materialInvId == md.MaterialInvId
                               select new {
                                   md.MaterialUseInShift.UsedDate,
                                   md.IsDetroy,
                                   md.MaterialUseInShift.Type,
                                   Quantity = md.EditQuantity + md.EditQuantity2
                               };
                    var years = materialPeriods.Select(mip => mip.PeriodDate.Year).ToList();
                    var year2 = imports.Select(u => u.ImportDate.Year).Distinct().ToList();
                    years.AddRange(year2);
                    year2 = exports.Select(u => u.ExportDate.Year).Distinct().ToList();
                    years.AddRange(year2);
                    year2 = uses.Select(u => u.UsedDate.Year).Distinct().ToList();
                    years.AddRange(year2);
                    years = years.Distinct().OrderBy(y => y).ToList();
                    foreach (var year in years) {
                        var entity = new InventoryCardModel {
                            Year = year,
                            InventoryStart = 0,
                            MaterialInvId = materialInvId,
                        };
                        var import =
                            imports.Where(id => id.ImportDate.Year == year && id.PurchaseOrderId != null).ToList();
                        entity.ImportProduction = import.Sum(id => id.Quantity); // nhap mua
                        import = imports.Where(id => id.ImportDate.Year == year && id.PurchaseOrderId == null).ToList();
                        entity.ImportVirtual = import.Sum(id => id.Quantity); // nhap them

                        var export = exports.Where(ed => ed.ExportDate.Year == year && ed.MachineId != null).ToList();
                        entity.ExportSell = export.Sum(ed => ed.Quantity); // phat NL
                        export = exports.Where(ed => ed.ExportDate.Year == year && ed.MachineId == null).ToList();
                        entity.ExportDestroy = export.Sum(ed => ed.Quantity); // huy NL

                        var use = uses.Where(md => md.UsedDate.Year == year && md.Type == (int)MyUtilities.Material.UseType.Using && !md.IsDetroy).ToList();
                        entity.ExportProcessing = use.Sum(mu => mu.Quantity); // su dung NL
                        use = uses.Where(md => md.UsedDate.Year == year && md.Type == (int)MyUtilities.Material.UseType.SendBack).ToList();
                        entity.SendBack = use.Sum(mu => mu.Quantity); // trả NL
                        use = uses.Where(md => md.UsedDate.Year == year && md.Type == (int)MyUtilities.Material.UseType.SendBack && md.IsDetroy).ToList();
                        entity.ExportDefect = use.Sum(mu => mu.Quantity); // huy NL tren may

                        var startDate = new DateTime(year, 1, 1).AddSeconds(-1);
                        var periods = materialPeriods.Where(mip => mip.PeriodDate <= startDate).ToList();
                        var periods2 = onMachinePeriods.Where(mip => mip.PeriodDate <= startDate).ToList();
                        entity.InventoryStart = periods.Sum(mip => mip.Quantity) + periods2.Sum(mip => mip.Quantity);
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterialInventoryCardYear", ex.Message);

            }
            return View(new GridModel(model));
        }
        [GridAction]
        public ActionResult SelectMaterialInventoryCardMonth(int materialInvId, int year) {
            var model = new List<InventoryCardModel>();
            if (materialInvId == 0)
                return View(new GridModel(model));
            try {
                using (var vfi = new tammaContext()) {
                    var materialInv =
                        vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == materialInvId);
                    var materialPeriods = from mip in vfi.MaterialInventoryPeriods
                                          where mip.MaterialInventoryId == materialInvId
                                          //&&mip.PeriodYear == year
                                          select new {
                                              mip.PeriodDate,
                                              Quantity = mip.LastPeriodQuantity - mip.EarlyPeriodQuantity
                                          };
                    var onMachinePeriods = from mimp in vfi.MaterialInvOnMachinePeriods
                                           where mimp.MaterialInvId == materialInvId
                                           // &&mimp.PeriodDate.Year == year
                                           select new {
                                               mimp.PeriodDate,
                                               Quantity = mimp.LastQuantity - mimp.LastQuantity
                                           };
                    var imports = from id in vfi.ImportPurchaseOrderDetails
                                  where
                                      id.ImportPurchaseOrder.Transaction.Status ==
                                      (byte)MyUtilities.Transaction.Status.Approved
                                      && id.MaterialId == materialInv.MaterialId
                                      && id.LotNumber.Equals(materialInv.LotNumber)
                                      && id.VendorId == materialInv.VendorId
                                      && id.Length == materialInv.Length
                                      && id.ImportPurchaseOrder.ImportDate.Year == year
                                  //&& id.ImportPurchaseOrder.PurchaseOrderId == null
                                  select new {
                                      id.ImportPurchaseOrder.ImportDate,
                                      id.ImportPurchaseOrder.PurchaseOrderId,
                                      id.Quantity,
                                  };
                    var exports = (from ed in vfi.ExportMaterialDetails
                                   where
                                       ed.ExportMaterial.Transaction.Status ==
                                       (byte)MyUtilities.Transaction.Status.Approved &&
                                       materialInvId == ed.MaterialInvId
                                      && ed.ExportMaterial.ExportDate.Year == year
                                   select new {
                                       ed.ExportMaterial.ExportDate,
                                       ed.MachineId,
                                       ed.Quantity,
                                   }).ToList();
                    var uses = from md in vfi.MaterialUseDetails
                               where
                                   md.MaterialUseInShift.Status ==
                                   (byte)MyUtilities.Transaction.Status.Approved
                                   //&& dom.IsDetroy
                                   && materialInvId == md.MaterialInvId
                                   && md.MaterialUseInShift.UsedDate.Year == year
                               select new {
                                   md.MaterialUseInShift.UsedDate,
                                   md.IsDetroy,
                                   md.MaterialUseInShift.Type,
                                   Quantity = md.EditQuantity + md.EditQuantity2
                               };
                    var months =
                        materialPeriods.Where(mip => mip.PeriodDate.Year == year)
                                       .Select(mip => mip.PeriodDate.Month)
                                       .ToList();
                    var months2 = imports.Select(u => u.ImportDate.Month).Distinct().ToList();
                    months.AddRange(months2);
                    months2 = exports.Select(u => u.ExportDate.Month).Distinct().ToList();
                    months.AddRange(months2);
                    months2 = uses.Select(u => u.UsedDate.Month).Distinct().ToList();
                    months.AddRange(months2);
                    months = months.Distinct().OrderBy(y => y).ToList();
                    foreach (var month in months) {
                        var entity = new InventoryCardModel {
                            Year = year,
                            Month = month,
                            InventoryStart = 0,
                            MaterialInvId = materialInvId,
                        };
                        var import = imports.Where(id => id.ImportDate.Month == month && id.PurchaseOrderId != null).ToList();
                        entity.ImportProduction = import.Sum(id => id.Quantity); // nhap mua
                        import = imports.Where(id => id.ImportDate.Month == month && id.PurchaseOrderId == null).ToList();
                        entity.ImportVirtual = import.Sum(id => id.Quantity); // nhap them

                        var export = exports.Where(ed => ed.ExportDate.Month == month && ed.MachineId != null).ToList();
                        entity.ExportSell = export.Sum(ed => ed.Quantity); // phat NL
                        export = exports.Where(ed => ed.ExportDate.Month == month && ed.MachineId == null).ToList();
                        entity.ExportDestroy = export.Sum(ed => ed.Quantity); // huy NL

                        var use = uses.Where(md => md.UsedDate.Month == month && md.Type == (int)MyUtilities.Material.UseType.Using && !md.IsDetroy).ToList();
                        entity.ExportProcessing = use.Sum(mu => mu.Quantity); // su dung NL
                        use = uses.Where(md => md.UsedDate.Month == month && md.Type == (int)MyUtilities.Material.UseType.SendBack).ToList();
                        entity.SendBack = use.Sum(mu => mu.Quantity); // trả NL
                        use = uses.Where(md => md.UsedDate.Month == month && md.Type == (int)MyUtilities.Material.UseType.SendBack && md.IsDetroy).ToList();
                        entity.ExportDefect = use.Sum(mu => mu.Quantity); // huy NL tren may

                        var startDate = new DateTime(year, month, 1).AddSeconds(-1);
                        var periods = materialPeriods.Where(mip => mip.PeriodDate <= startDate).ToList();
                        var periods2 = onMachinePeriods.Where(mip => mip.PeriodDate <= startDate).ToList();
                        entity.InventoryStart = periods.Sum(mip => mip.Quantity) + periods2.Sum(mip => mip.Quantity);
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterialInventoryCardMonth", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectMaterialInventoryCardDay(int materialInvId, int year, int month) {
            var model = new List<InventoryCardModel>();
            if (materialInvId == 0)
                return View(new GridModel(model));
            try {
                using (var vfi = new tammaContext()) {
                    var materialInv =
                        vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == materialInvId);
                    //var materialPeriods =
                    //    vfi.MaterialInventoryPeriods.Where(
                    //        mip => mip.MaterialInventoryId == materialInvId &&
                    //               mip.PeriodYear == year &&
                    //               mip.PeriodMonth == month);
                    var materialPeriods = from mip in vfi.MaterialInventoryPeriods
                                          where mip.MaterialInventoryId == materialInvId
                                          //mip.PeriodYear == year &&
                                          //mip.PeriodMonth == month
                                          select new {
                                              mip.PeriodDate,
                                              Quantity = mip.LastPeriodQuantity - mip.EarlyPeriodQuantity
                                          };
                    var onMachinePeriods = from mimp in vfi.MaterialInvOnMachinePeriods
                                           where mimp.MaterialInvId == materialInvId
                                           //mimp.PeriodDate.Year == year &&
                                           //mimp.PeriodDate.Month == month
                                           select new {
                                               mimp.PeriodDate,
                                               Quantity = mimp.LastQuantity - mimp.LastQuantity
                                           };
                    var imports = from id in vfi.ImportPurchaseOrderDetails
                                  where
                                      id.ImportPurchaseOrder.Transaction.Status ==
                                      (byte)MyUtilities.Transaction.Status.Approved
                                      && id.MaterialId == materialInv.MaterialId
                                      && id.LotNumber.Equals(materialInv.LotNumber)
                                      && id.VendorId == materialInv.VendorId
                                      && id.Length == materialInv.Length
                                      && id.ImportPurchaseOrder.ImportDate.Year == year
                                      && id.ImportPurchaseOrder.ImportDate.Month == month
                                  //&& id.ImportPurchaseOrder.PurchaseOrderId == null
                                  select new {
                                      id.ImportPurchaseOrder.ImportDate,
                                      id.ImportPurchaseOrder.PurchaseOrderId,
                                      id.Quantity,
                                  };
                    var exports = (from ed in vfi.ExportMaterialDetails
                                   where
                                       ed.ExportMaterial.Transaction.Status ==
                                       (byte)MyUtilities.Transaction.Status.Approved &&
                                       materialInvId == ed.MaterialInvId
                                       && ed.ExportMaterial.ExportDate.Year == year
                                       && ed.ExportMaterial.ExportDate.Month == month
                                   select new {
                                       ed.ExportMaterial.ExportDate,
                                       ed.MachineId,
                                       ed.Quantity,
                                   }).ToList();
                    var uses = from md in vfi.MaterialUseDetails
                               where
                                   md.MaterialUseInShift.Status ==
                                   (byte)MyUtilities.Transaction.Status.Approved
                                   //&& dom.IsDetroy
                                   && materialInvId == md.MaterialInvId
                                   && md.MaterialUseInShift.UsedDate.Year == year
                                   && md.MaterialUseInShift.UsedDate.Month == month
                               select new {
                                   md.MaterialUseInShift.UsedDate,
                                   md.IsDetroy,
                                   md.MaterialUseInShift.Type,
                                   Quantity = md.EditQuantity + md.EditQuantity2
                               };
                    var days =
                        materialPeriods.Where(mip => mip.PeriodDate.Year == year && mip.PeriodDate.Month == month)
                                       .Select(mip => mip.PeriodDate.Day)
                                       .ToList();
                    var days2 = imports.Select(u => u.ImportDate.Day).Distinct().ToList();
                    days.AddRange(days2);
                    days2 = exports.Select(u => u.ExportDate.Day).Distinct().ToList();
                    days.AddRange(days2);
                    days2 = uses.Select(u => u.UsedDate.Day).Distinct().ToList();
                    days.AddRange(days2);
                    days = days.Distinct().OrderBy(y => y).ToList();
                    foreach (var day in days) {
                        var entity = new InventoryCardModel {
                            Year = year,
                            Month = month,
                            Day = day,
                            InventoryStart = 0,
                            MaterialInvId = materialInvId,
                        };
                        var import =
                            imports.Where(id => id.ImportDate.Day == day && id.PurchaseOrderId != null).ToList();
                        entity.ImportProduction = import.Sum(id => id.Quantity); // nhap mua
                        import = imports.Where(id => id.ImportDate.Day == day && id.PurchaseOrderId == null).ToList();
                        entity.ImportVirtual = import.Sum(id => id.Quantity); // nhap them

                        var export = exports.Where(ed => ed.ExportDate.Day == day && ed.MachineId != null).ToList();
                        entity.ExportSell = export.Sum(ed => ed.Quantity); // phat NL
                        export = exports.Where(ed => ed.ExportDate.Day == day && ed.MachineId == null).ToList();
                        entity.ExportDestroy = export.Sum(ed => ed.Quantity); // huy NL

                        var use = uses.Where(md => md.UsedDate.Day == day && md.Type == (int)MyUtilities.Material.UseType.Using && !md.IsDetroy).ToList();
                        entity.ExportProcessing = use.Sum(mu => mu.Quantity); // su dung NL
                        use = uses.Where(md => md.UsedDate.Day == day && md.Type == (int)MyUtilities.Material.UseType.SendBack).ToList();
                        entity.SendBack = use.Sum(mu => mu.Quantity); // trả NL
                        use = uses.Where(md => md.UsedDate.Day == day && md.Type == (int)MyUtilities.Material.UseType.SendBack && md.IsDetroy).ToList();
                        entity.ExportDefect = use.Sum(mu => mu.Quantity); // huy NL tren may

                        var startDate = new DateTime(year, month, day).AddSeconds(-1);
                        var periods = materialPeriods.Where(mip => mip.PeriodDate <= startDate).ToList();
                        var periods2 = onMachinePeriods.Where(mip => mip.PeriodDate <= startDate).ToList();
                        entity.InventoryStart = periods.Sum(mip => mip.Quantity) + periods2.Sum(mip => mip.Quantity);
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterialInventoryCardDay", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectProductionInDay(int productId, int materialInvId, int year, int month, int day, string lotNumber) {
            var model = new List<ProductionDailyReport>();
            try {
                using (var vfi = new tammaContext()) {
                    var importSx1s = (from sxd in vfi.ImportFormSX1Detail
                                      orderby sxd.Machine1.MachineName
                                      where
                                      (productId == 0 ||
                                          (sxd.ProductId == productId &&
                                          sxd.ImportFormSX1.ImportDate.Day == day &&
                                          sxd.ImportFormSX1.ImportDate.Month == month &&
                                          sxd.ImportFormSX1.ImportDate.Year == year)) &&
                                      (materialInvId == 0 ||
                                          (sxd.MaterialInvId == materialInvId &&
                                            sxd.ImportFormSX1.MaterialUseDate.Day == day &&
                                            sxd.ImportFormSX1.MaterialUseDate.Month == month &&
                                            sxd.ImportFormSX1.MaterialUseDate.Year == year)) &&

                                            sxd.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                            sxd.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                          //sxd.MachineId == 26 &&
                                            sxd.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault().Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved 
                                        select sxd).ToList();
                    //var machines = vfi.Machines.Where(m => m.Active).OrderBy(m => m.MachineName);
                    if (!string.IsNullOrWhiteSpace(lotNumber) && !lotNumber.ToLower().Equals("all")) {
                        importSx1s = importSx1s.Where(id => id.LotNumber.Contains(lotNumber)).ToList();
                    }
                    var machines = importSx1s.Select(id => id.Machine1).Distinct();
                    var i = 1;
                    foreach (var machine in machines) {
                        var sx1DetailByMachines = importSx1s.Where(id => id.MachineId == machine.MachineId).ToList();
                        foreach (var sx1Detail in sx1DetailByMachines) {
                            var entity =
                                model.FirstOrDefault(
                                    m =>
                                    m.MachineId == sx1Detail.MachineId
                                    && m.MaterialInvId == sx1Detail.MaterialInvId
                                    && m.LotNumber.Equals(sx1Detail.LotNumber));
                            if (entity == null) {
                                var product = vfi.Products.FirstOrDefault(p => p.ProductId == sx1Detail.ProductId);
                                entity = new ProductionDailyReport {
                                    Index = i,
                                    MachineId = sx1Detail.MachineId.Value,
                                    MachineName = sx1Detail.Machine1.MachineName,
                                    MachineType = sx1Detail.Machine1.ProcessingType.TypeName,
                                    MaterialInvId = sx1Detail.MaterialInvId.Value,
                                    MaterialWeight = sx1Detail.MaterialInventory.UnitWeight,
                                    ProductId = sx1Detail.ProductId,
                                    ProductCode = product.ProductCode,
                                    ProductionRate = sx1Detail.ProductionRate,
                                    UnitWeight = product.ProductionWeight ?? 0,
                                    Productivity = product.Productivity ?? 0,
                                    //ProductivityInShift = 0,
                                    //ProductivityInDay = 0,
                                    Shift1Name = sx1Detail.ImportFormSX1.Shift1Name + "",
                                    Shift2Name = sx1Detail.ImportFormSX1.Shift2Name + "",
                                    VendorCode = sx1Detail.MaterialInventory.Vendor.VendorCode,
                                    LotNumber = sx1Detail.LotNumber
                                };
                                entity.MaterialCode =
                                    MyUtilities.Material.GetMaterialInvDesignNo(sx1Detail.MaterialInventory);
                                i++;
                                //if (entity.Productivity != 0) {
                                //    //10h
                                //    entity.ProductivityInShift = MyUtilities.Product.Second/10h / entity.Productivity;
                                //    //20h
                                //    entity.ProductivityInDay = MyUtilities.Product.Second/20h / entity.Productivity;
                                //}
                                var shift1 = new Shift {
                                    Quantity = sx1Detail.Number1,
                                    ProcessingQuantity = sx1Detail.Processing1,
                                    DefectQuantity = sx1Detail.DefectProduct1,
                                    MaterialUse = sx1Detail.MaterialUse1,
                                    ProductionRate = entity.ProductionRate
                                };
                                var shift2 = new Shift {
                                    Quantity = sx1Detail.Number2,
                                    ProcessingQuantity = sx1Detail.Processing2,
                                    DefectQuantity = sx1Detail.DefectProduct2,
                                    MaterialUse = sx1Detail.MaterialUse2,
                                    ProductionRate = entity.ProductionRate
                                };
                                entity.Shifts.Add(shift1);
                                entity.Shifts.Add(shift2);
                                model.Add(entity);
                            }
                            else {
                                if (entity.ProductId != sx1Detail.ProductId) {
                                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == sx1Detail.ProductId);
                                    var newEntity = new ProductionDailyReport {
                                        Index = i,
                                        MachineId = sx1Detail.MachineId.Value,
                                        MachineName = sx1Detail.Machine1.MachineName,
                                        MachineType = sx1Detail.Machine1.ProcessingType.TypeName,
                                        MaterialInvId = sx1Detail.MaterialInvId.Value,
                                        MaterialWeight = sx1Detail.MaterialInventory.UnitWeight,
                                        ProductId = sx1Detail.ProductId,
                                        ProductCode = product.ProductCode,
                                        ProductionRate = sx1Detail.ProductionRate,
                                        UnitWeight = product.ProductionWeight ?? 0,
                                        //Shift = new List<Shift>(),
                                        Productivity = product.Productivity ?? 0,
                                        //ProductivityInShift = 0,
                                        //ProductivityInDay = 0,
                                        VendorCode = sx1Detail.MaterialInventory.Vendor.VendorCode,
                                        LotNumber = sx1Detail.MaterialInventory.LotNumber,
                                        Shift1Name = sx1Detail.ImportFormSX1.Shift1Name + "",
                                        Shift2Name = sx1Detail.ImportFormSX1.Shift2Name + "",
                                    };
                                    entity.MaterialCode = MyUtilities.Material.GetMaterialInvDesignNo(sx1Detail.MaterialInventory);
                                    i++;
                                    //if (newEntity.Productivity != 0) {
                                    //    //10h
                                    //    newEntity.ProductivityInShift = MyUtilities.Product.Second/10h / newEntity.Productivity;
                                    //    //20h
                                    //    newEntity.ProductivityInDay = MyUtilities.Product.Second/20h / newEntity.Productivity;
                                    //}
                                    var shift1 = new Shift {
                                        Quantity = sx1Detail.Number1,
                                        ProcessingQuantity = sx1Detail.Processing1,
                                        DefectQuantity = sx1Detail.DefectProduct1,
                                        MaterialUse = sx1Detail.MaterialUse1,
                                        ProductionRate = newEntity.ProductionRate
                                    };
                                    var shift2 = new Shift {
                                        Quantity = sx1Detail.Number2,
                                        ProcessingQuantity = sx1Detail.Processing2,
                                        DefectQuantity = sx1Detail.DefectProduct2,
                                        MaterialUse = sx1Detail.MaterialUse2,
                                        ProductionRate = newEntity.ProductionRate
                                    };
                                    newEntity.Shifts.Add(shift1);
                                    newEntity.Shifts.Add(shift2);
                                    model.Add(newEntity);
                                }
                                else {
                                    if (string.IsNullOrWhiteSpace(entity.Shift1Name))
                                        entity.Shift1Name = sx1Detail.ImportFormSX1.Shift1Name + "";
                                    if (string.IsNullOrWhiteSpace(entity.Shift2Name))
                                        entity.Shift2Name = sx1Detail.ImportFormSX1.Shift2Name + "";
                                    entity.Shifts[0].Quantity += sx1Detail.Number1;
                                    entity.Shifts[1].Quantity += sx1Detail.Number2;
                                    entity.Shifts[0].ProcessingQuantity += sx1Detail.Processing1;
                                    entity.Shifts[1].ProcessingQuantity += sx1Detail.Processing2;
                                    entity.Shifts[0].MaterialUse += sx1Detail.MaterialUse1;
                                    entity.Shifts[1].MaterialUse += sx1Detail.MaterialUse2;
                                    entity.Shifts[0].DefectQuantity += sx1Detail.DefectProduct1;
                                    entity.Shifts[1].DefectQuantity += sx1Detail.DefectProduct2;

                                }
                            }
                        }
                        if (!sx1DetailByMachines.Any()) {
                            var smartProduction =
                                vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == machine.MachineId);
                            if (smartProduction != null) {
                                var product = vfi.Products.FirstOrDefault(p => p.ProductId == smartProduction.ProductId);
                                var entity = new ProductionDailyReport {
                                    Index = i,
                                    MachineId = machine.MachineId,
                                    MachineName = machine.MachineName,
                                    MachineType = machine.ProcessingType.TypeName,
                                    ProductCode = product.ProductCode,
                                    ProductionRate = product.ProductionRate ?? 0,
                                    UnitWeight = product.ProductionWeight ?? 0,
                                    //Shift = new List<Shift>(),
                                    Productivity = product.Productivity ?? 0,
                                    //ProductivityInShift = 0,
                                    //ProductivityInDay = 0,
                                };
                                i++;
                                //if (entity.Productivity != 0) {
                                //    //10h
                                //    entity.ProductivityInShift = MyUtilities.Product.Second/10h / entity.Productivity;
                                //    //20h
                                //    entity.ProductivityInDay = MyUtilities.Product.Second/20h / entity.Productivity;
                                //}
                                var shift1 = new Shift {
                                };
                                entity.Shifts.Add(shift1);
                                entity.Shifts.Add(shift1);
                                model.Add(entity);
                            }
                            else {
                                var entity = new ProductionDailyReport {
                                    Index = i,
                                    MachineId = machine.MachineId,
                                    MachineName = machine.MachineName,
                                    ProductCode = "",
                                    ProductionRate = 0,
                                    UnitWeight = 0,
                                    Productivity = 0,
                                    //ProductivityInShift = 0,
                                    //ProductivityInDay = 0,
                                };
                                i++;
                                var shift1 = new Shift {
                                };
                                entity.Shifts.Add(shift1);
                                entity.Shifts.Add(shift1);
                                model.Add(entity);
                            }
                        }

                    }
                    if (productId != 0) {
                        var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                        var fromDate = new DateTime(year, month, day);
                        var toDate = new DateTime(year, month, day).AddDays(1).AddSeconds(-1);

                        //var cncsProcess = GetCncFormDetailCardModel(productId, fromDate, toDate, lotNumber);
                        //foreach (var process in cncsProcess) {
                        //    var entity = model.FirstOrDefault(x => x.MachineId == process.MachineId);
                        //    if (entity == null) {
                        //        entity = new ProductionDailyReport {
                        //            Index = i,
                        //            MachineId = process.MachineId,
                        //            MachineName = process.MachineName,
                        //            MachineType = process.MachineType,
                        //            MaterialWeight = 0,
                        //            ProductId = productId,
                        //            ProductCode = product.ProductCode,
                        //            ProductionRate = 0,
                        //            UnitWeight = 0,
                        //            Productivity = 0,
                        //            Shift1Name = process.Shift1Name + "",
                        //            Shift2Name = process.Shift2Name + "",
                        //            LotNumber = process.ProductInvCode
                        //        };

                        //        var shift1 = new Shift {
                        //            Quantity = process.Number1,
                        //            ProcessingQuantity = process.Processing1,
                        //            DefectQuantity = process.DefectProduct1,
                        //        };
                        //        var shift2 = new Shift {
                        //            Quantity = process.Number2,
                        //            ProcessingQuantity = process.Processing2,
                        //            DefectQuantity = process.DefectProduct2,
                        //        };
                        //        entity.Shifts.Add(shift1);
                        //        entity.Shifts.Add(shift2);
                        //        model.Add(entity);
                        //        i++;
                        //    }
                        //    else {
                        //        if (string.IsNullOrWhiteSpace(entity.Shift1Name))
                        //            entity.Shift1Name = process.Shift1Name + "";
                        //        if (string.IsNullOrWhiteSpace(entity.Shift2Name))
                        //            entity.Shift2Name = process.Shift2Name + "";
                        //        entity.Shifts[0].Quantity += process.Number1;
                        //        entity.Shifts[1].Quantity += process.Number2;
                        //        entity.Shifts[0].ProcessingQuantity += process.Processing1;
                        //        entity.Shifts[1].ProcessingQuantity += process.Processing2;
                        //        entity.Shifts[0].DefectQuantity += process.DefectProduct1;
                        //        entity.Shifts[1].DefectQuantity += process.DefectProduct2;
                        //    }
                        //}

                        var cncsProcess = GetCncRotateCardModel(productId, fromDate, toDate, lotNumber);
                        foreach (var process in cncsProcess) {
                            var entity = new ProductionDailyReport {
                                Index = i++,
                                MachineName = "",
                                MachineType = process.MachineName,
                                MaterialWeight = 0,
                                ProductId = productId,
                                ProductCode = product.ProductCode,
                                ProductionRate = 0,
                                UnitWeight = 0,
                                Productivity = 0,
                                LotNumber = process.ProductInvCode
                            };

                            var shift = new Shift {
                                Quantity = process.Number,
                                ProcessingQuantity = process.Processing,
                                DefectQuantity = process.DefectProduct,
                            };
                            entity.Shifts.Add(shift);
                            entity.Shifts.Add(new Shift());
                            model.Add(entity);
                        }

                        var production2sProcess = GetProduction2RotateCardModel(productId, fromDate, toDate, lotNumber);

                        foreach (var process in production2sProcess) {
                            var entity = new ProductionDailyReport {
                                Index = i++,
                                MachineName = "",
                                MachineType = process.MachineName,
                                MaterialWeight = 0,
                                ProductId = productId,
                                ProductCode = product.ProductCode,
                                ProductionRate = 0,
                                UnitWeight = 0,
                                Productivity = 0,
                                LotNumber = process.ProductInvCode
                            };

                            var shift = new Shift {
                                Quantity = process.Number,
                                ProcessingQuantity = process.Processing,
                                DefectQuantity = process.DefectProduct,
                            };
                            entity.Shifts.Add(shift);
                            entity.Shifts.Add(new Shift());
                            model.Add(entity);
                        }
                        //if (production2sProcess.Any()) {
                        //    var production2 = new ProductionDailyReport {
                        //        Index = i,
                        //        MachineName = "",
                        //        MachineType = "Sản xuất 2",
                        //        MaterialWeight = 0,
                        //        ProductId = productId,
                        //        ProductCode = product.ProductCode,
                        //        ProductionRate = 0,
                        //        UnitWeight = 0,
                        //        Productivity = 0,
                        //    };

                        //    var shift = new Shift {
                        //        Quantity = production2sProcess.Sum(x => x.Number1),
                        //        ProcessingQuantity = production2sProcess.Sum(x => x.Processing1),
                        //        DefectQuantity = production2sProcess.Sum(x => x.DefectProduct1),
                        //    };
                        //    production2.Shifts.Add(shift);
                        //    production2.Shifts.Add(new Shift());
                        //    model.Add(production2);
                        //}

                        var processes = GetImportInternalCardModel(productId, fromDate, toDate, lotNumber);
                        if (processes.Any()) {
                            foreach (var process in processes) {
                                var production2 = new ProductionDailyReport {
                                    Index = i++,
                                    MachineName = "",
                                    MachineType = process.MachineName,
                                    MaterialWeight = 0,
                                    ProductId = productId,
                                    ProductCode = product.ProductCode,
                                    ProductionRate = 0,
                                    UnitWeight = 0,
                                    Productivity = 0,
                                    LotNumber = process.ProductInvCode
                                };

                                var shift = new Shift {
                                    Quantity = process.Number,
                                    ProcessingQuantity = 0,
                                    DefectQuantity = 0,
                                };
                                production2.Shifts.Add(shift);
                                production2.Shifts.Add(new Shift());
                                model.Add(production2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ProductionDailyReport", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        [GridAction]
        public ActionResult SelectPrepareMaterialInv(string materialCode,
                                                     int? materialTypeId) {
            var model = new List<MaterialInventoryModel>();
            using (var vfi = new tammaContext()) {
                if (string.IsNullOrWhiteSpace(materialCode) && (materialTypeId == null || materialTypeId == 0))
                    return View(new GridModel(model));
                var materials = vfi.Materials.Where(m => m.Active);
                if (materialTypeId != null && materialTypeId != 0)
                    materials = materials.Where(m => m.MaterialTypeId == materialTypeId);
                if (!string.IsNullOrWhiteSpace(materialCode))
                    materials = materials.Where(m => m.MaterialCode.Contains(materialCode));
                foreach (var material in materials) {
                    var materialInvs =
                        vfi.MaterialInventories.Where(mi => mi.MaterialId == material.MaterialId);
                    if (materialInvs.Any()) {
                        foreach (var materialInventory in materialInvs) {
                            //if(materialInventory.TotalQty == 0) continue;
                            var entity = new MaterialInventoryModel {
                                MaterialId = material.MaterialId,
                                TotalQty = materialInventory.TotalQty,
                                MaterialCode = material.MaterialCode,
                                MaterialName = material.MaterialName,
                                LotNumber = materialInventory.LotNumber,
                                UnitPrice = materialInventory.UnitPrice,
                                UnitWeight = materialInventory.UnitWeight,
                                VendorId = materialInventory.VendorId ?? 0,
                                VendorName = materialInventory.Vendor.VendorName,
                                VendorCode = materialInventory.Vendor.VendorCode,
                                MaterialInvOnMachine = 0,
                                MaterialInventoryId = materialInventory.MaterialInventoryId,
                                Length = materialInventory.Length,
                                DesignNo = MyUtilities.Material.GetMaterialDesignNo(material),

                            };
                            var materialInvOnMachine =
                                vfi.MaterialInvOnMachines.Where(
                                    mi =>
                                    materialInventory.MaterialInventoryId == mi.MaterialInvId);
                            if (materialInvOnMachine.Any()) {
                                entity.MaterialInvOnMachine = materialInvOnMachine.Sum(mi => mi.TotalQuantity);
                            }
                            if (Math.Round(entity.TotalQty + entity.MaterialInvOnMachine, 2) > 0)
                                model.Add(entity);
                        }
                    }
                    if (model.FirstOrDefault(m => m.MaterialId == material.MaterialId) == null) {
                        var entity = new MaterialInventoryModel {
                            MaterialId = material.MaterialId,
                            DesignNo = MyUtilities.Material.GetMaterialDesignNo(material),
                            TotalQty = 0,
                            MaterialInventoryId = 0,
                            MaterialCode = material.MaterialCode,
                            MaterialName = material.MaterialName,
                            LotNumber = "",
                            UnitPrice = 0,
                            UnitWeight = 0,
                            VendorId = 0,
                            MaterialInvOnMachine = 0,
                            VendorName = "",
                            VendorCode = "",
                        };
                        model.Add(entity);
                    }
                }
            }
            return View(new GridModel(model.OrderBy(m => m.MaterialCode)));
        }

        [GridAction]
        public ActionResult SelectExportMaterialInv(string materialCode,
                                                     int materialTypeId) {
            var model = new List<MaterialInventoryModel>();
            using (var vfi = new tammaContext()) {
                if (string.IsNullOrWhiteSpace(materialCode) && materialTypeId == 0)
                    return View(new GridModel(model));
                var materials = vfi.Materials.Where(m => m.Active);
                if (!string.IsNullOrWhiteSpace(materialCode))
                    materials = materials.Where(m => m.MaterialCode.Contains(materialCode));
                if (materialTypeId != 0)
                    materials = materials.Where(m => m.MaterialTypeId == materialTypeId);
                foreach (var material in materials) {
                    var materialInvs =
                        vfi.MaterialInventories.Where(m => m.MaterialId == material.MaterialId && Math.Round(m.TotalQty, 2) > 0);
                    if (materialInvs.Any()) {
                        foreach (var materialInventory in materialInvs) {
                            var entity = new MaterialInventoryModel {
                                MaterialId = material.MaterialId,
                                TotalQty = materialInventory.TotalQty,
                                TotalQtyKg = materialInventory.TotalQty * materialInventory.UnitWeight,
                                MaterialCode = material.MaterialCode,
                                MaterialName = material.MaterialName,
                                MaterialInventoryId = materialInventory.MaterialInventoryId,
                                VendorId = materialInventory.VendorId ?? 0,
                                VendorCode = materialInventory.Vendor.VendorCode,
                                VendorName = materialInventory.Vendor.VendorName,
                                MaterialInvOnMachine = 0,
                                LotNumber = materialInventory.LotNumber,
                                Length = materialInventory.Length,
                                UnitWeight = materialInventory.UnitWeight,
                            };
                            var materialInvOnMachine =
                              vfi.MaterialInvOnMachines.Where(
                                  mi =>
                                  materialInventory.MaterialInventoryId == mi.MaterialInvId);
                            if (materialInvOnMachine.Any()) {
                                entity.MaterialInvOnMachine = materialInvOnMachine.Sum(mi => mi.TotalQuantity);
                            }
                            model.Add(entity);
                        }
                    }
                }
            }
            return View(new GridModel(model.OrderBy(m => m.MaterialCode)));
        }


        [GridAction]
        public ActionResult SelectMaterialInventoryPeriod(
            int? materialTypeId
            , string materialName
            , string fromDate, string toDate) {
            var model = new List<MaterialInventoryPeriodModel>();
            if ((materialTypeId == null || materialTypeId == 0) && !string.IsNullOrWhiteSpace(materialName))
                return View(new GridModel(model));
            var ci = new CultureInfo("vi-VN");
            DateTime fDate;
            DateTime tDate;
            if (string.IsNullOrWhiteSpace(fromDate) || string.IsNullOrWhiteSpace(toDate)) {
                fDate = DateTime.Today;
                tDate = DateTime.Today;
            }
            else {
                fDate = Convert.ToDateTime(fromDate, ci);
                tDate = Convert.ToDateTime(toDate, ci);
            }
            using (var vfi = new tammaContext()) {

                var materialIds = new List<int>();
                if (materialTypeId != null && materialTypeId != 0) {
                    materialIds =
                        vfi.Materials.Where(m => m.MaterialTypeId == materialTypeId).Select(m => m.MaterialId).ToList();
                }
                if (!string.IsNullOrWhiteSpace(materialName)) {
                    if (materialIds.Any()) {
                        var materialIdsByName =
                            vfi.Materials.Where(m => m.MaterialCode.Contains(materialName))
                               .Select(m => m.MaterialId)
                               .ToList();
                        materialIds = materialIds.Intersect(materialIdsByName).ToList();
                    }
                    else {
                        materialIds =
                            vfi.Materials.Where(m => m.MaterialCode.Contains(materialName))
                               .Select(m => m.MaterialId)
                               .ToList();
                    }
                }
                var materialPeriod = from mp in vfi.MaterialInventoryPeriods
                                     where
                                         mp.PeriodDate < tDate &&
                                         mp.PeriodDate > fDate &&
                                         materialIds.Contains(mp.MaterialId)
                                     select mp;
                foreach (var materialId in materialIds) {

                }
            }

            return View(new GridModel(model));
        }

        #endregion

        #region ProductInventory


        public ActionResult CheckProductInventoryInfo(int warehouseId, int productId) {
            // valueCode, weight, invQ, invG
            var result = new double[] { 0.0, 0.0, 0.0, 0.0 };
            if (warehouseId == 0 || productId == 0)
                result[0] = (int)MyUtilities.Monitor.ErrorCode.ReferenceError;
            else {
                result[1] = MyUtilities.Product.GetProductInvWeight(productId, warehouseId);
                using (var vfi = new tammaContext()) {
                    var invs =
                        vfi.ProductInventories.Where(
                            pi => pi.WarehouseId == warehouseId & pi.ProductId == productId).ToList();
                    if (!invs.Any()) { }
                    else {
                        result[2] = invs.Sum(x => x.TotalQty);

                        var transactionDetails = vfi.TransactionDetails.Where(x => x.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open
                            && x.Transaction.WarehouseIssueId == warehouseId
                            && x.ReferenceId == productId).ToList();
                        var waitingQuantity = transactionDetails.Sum(x => x.Quantity);

                        result[2] -= waitingQuantity;
                        if (result[2] < 0) {
                            result[2] = 0;
                        }
                        result[3] = result[2] * result[1];

                        result[0] = (int)MyUtilities.Monitor.ErrorCode.NoError;

                    }
                }
            }
            return Json(result);
        }

        public ActionResult SelectComboBoxProductInv(int warehouseId) {
            var model = new List<ProductInventoryModel>();
            using (var vfi = new tammaContext()) {
                var productInvs =
                    vfi.ProductInventories.Where(pi => pi.TotalQty > 0 && pi.WarehouseId == warehouseId);
                foreach (var productInv in productInvs) {
                    var entity = new ProductInventoryModel {
                        ProductInventoryId = productInv.ProductInventoryId,
                        ProductCode = productInv.Product.ProductCode,
                        CustomerCode = productInv.Product.Customer.CustomerCode,
                        LotNumber = productInv.LotNumber,
                    };
                    model.Add(entity);
                }
            }
            model = model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ThenBy(m => m.LotNumber).ToList();
            return new JsonResult {
                Data = new SelectList(model, "ProductInventoryId", "ProductCodeInv")
            };
        }

        public ActionResult AutoCompletedProductInventoryByCode(string text) {
            var model = new List<string>();
            if (text.HasValue()) {
                try {
                    using (var vfi = new tammaContext()) {
                        model =
                           (from p in vfi.Products
                            orderby p.ProductCode
                            where p.Active
                            select p.ProductCode).ToList();
                        model = model.Where(p => p.StartsWith(text, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
            return new JsonResult {
                Data = model,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult AutoCompletedProductRecipeByCode(string text) {
            var model = new List<string>();
            if (text.HasValue()) {
                try {
                    using (var vfi = new tammaContext()) {
                        model =
                           (from p in vfi.Products
                            orderby p.ProductCode
                            where p.Active && p.ProductCombinationRecipes.Any(x => x.Active)
                            select p.ProductCode).ToList();
                        model = model.Where(p => p.StartsWith(text, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
            return new JsonResult {
                Data = model,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [GridAction]
        public ActionResult SelectProductInventoryByMaterialAndWarehouse(
            int materialId,
            //, int warehouseIssue, int warehouseReceipt
            int? warehouseId
            , string eoi, string productName) {
            return View(new GridModel(new List<ProductInventoryModel>()));

        }

        [GridAction]
        public ActionResult SelectProductInventoryById(int customerId, int warehouseId, string productCode) {
            var models = new List<ProductInventoryModel>();
            if (warehouseId == 0 ||
                (customerId == 0 && string.IsNullOrWhiteSpace(productCode))) {
                return View(new GridModel(models));
            }
            var isManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagementLv2);
            using (var vfi = new tammaContext()) {
                var products = (from p in vfi.Products
                                where p.Active && (customerId == 0 || p.CustomerId == customerId)
                                select new {
                                    p.ProductId,
                                    p.ProductCode,
                                    p.DrawingFinish,
                                    UploadDate = p.UploadDate ?? DateTime.Now
                                }).ToList();
                if (!String.IsNullOrWhiteSpace(productCode)) {
                    products = products.Where(p => p.ProductCode.ToUpper().Contains(productCode.ToUpper())).ToList();
                }
                if (!products.Any()) {
                    return View(new GridModel(models));
                }
                var productIds = products.Select(x => x.ProductId).ToList();
                var productInvs = (from pi in vfi.ProductInventories
                                   where productIds.Contains(pi.ProductId) &&
                                           pi.WarehouseId == warehouseId &&
                                           pi.TotalQty > 0
                                   select new {
                                       pi.ProductInventoryId,
                                       pi.ProductId,
                                       pi.WarehouseId,
                                       pi.TotalQty,
                                       pi.LotNumber,
                                       pi.StoreCode,
                                       pi.ByProcessMachineId,
                                   }).ToList();
                var productionProcessByMachines = (from x in vfi.ProductionProcessByMachines
                                                   where productIds.Contains(x.ProductId) &&
                                                        x.Active
                                                   select new {
                                                       x.DetailId,
                                                       x.ProductId,
                                                       x.MachineId,
                                                       x.WarehouseId,
                                                       x.Warehouse.WarehouseName,
                                                       x.ProcessIndex
                                                   }).ToList();
                var productionProcesses = (from x in vfi.ProductionProcesses
                                           where productIds.Contains(x.ProductId) &&
                                                 x.IsNecessary
                                           select new {
                                               x.ProductId,
                                               x.WarehouseId,
                                               x.Warehouse.WarehouseName,
                                               x.ProcessIndex
                                           }).ToList();

                var warehouse = vfi.Warehouses.FirstOrDefault(x => x.WarehouseId == warehouseId);
                if (warehouse == null) {
                    throw new AggregateException("Lỗi! Không tìm thấy kho");
                }
                var warehouses = vfi.Warehouses.Where(x => x.Active).ToList();
                var p2Warehouses = warehouses.Where(x => x.IsProduction2).Select(x => x.WarehouseId).ToList();
                var qcWarehouses = warehouses.Where(x => x.IsQC).Select(x => x.WarehouseId).ToList();
                var plWarehouses = warehouses.Where(x => x.IsPlating).Select(x => x.WarehouseId).ToList();
                var rpWarehouses = warehouses.Where(x => x.IsReprocessing).Select(x => x.WarehouseId).ToList();
                var opWarehouses = warehouses.Where(x => x.IsOutOfProcess).Select(x => x.WarehouseId).ToList();
                var warehouseIds = new List<int> { warehouseId };
                var receiptWarehouseIds = new List<int> { };
                if (isManager) {
                    receiptWarehouseIds = warehouses.Select(x => x.WarehouseId).ToList();
                }
                else if (!warehouse.IsMainProcess) {
                    var rotateWarehouses = vfi.WarehouseRotates.Where(x => x.WarehouseId == warehouseId && x.Active)
                                                                .Select(x => x.ToWarehouseId)
                                                                .ToList();
                    receiptWarehouseIds = vfi.WarehousePermissions.Where(x => x.User.Username.Equals(HttpContext.User.Identity.Name)
                                                                            && x.Rotate == true
                                                                            && rotateWarehouses.Contains(x.WarehouseId.Value))
                                                                    .Select(x => x.WarehouseId.Value)
                                                                    .ToList();
                }
                else if (warehouse.IsProduction2) {
                    warehouseIds.AddRange(p2Warehouses);
                    receiptWarehouseIds.AddRange(p2Warehouses);
                }
                else if (warehouse.IsQC) {
                    warehouseIds.AddRange(qcWarehouses);
                    receiptWarehouseIds.AddRange(qcWarehouses);
                }
                //else if (warehouse.IsPlating) {
                //    warehouseIds = plWarehouses;
                //}
                else if (warehouse.IsReprocessing) {
                    warehouseIds.AddRange(rpWarehouses);
                    receiptWarehouseIds.AddRange(rpWarehouses);
                }
                receiptWarehouseIds.AddRange(opWarehouses);
                receiptWarehouseIds.Remove(warehouseId);
                warehouseIds = warehouseIds.Distinct().ToList();
                receiptWarehouseIds = receiptWarehouseIds.Distinct().ToList();
                //warehouseIds.Remove(warehouseId);
                var avaiableWorkOrders = (from x in vfi.WorkOrderRoutings
                                          where productIds.Contains(x.ProductId) && x.WarehouseId == warehouseId &&
                                                (x.Status == (byte)MyUtilities.WorkOrder.Status.InProcess
                                                || x.Status == (byte)MyUtilities.WorkOrder.Status.Actived
                                                || x.WorkOrderRouting2.Status == (byte)MyUtilities.WorkOrder.Status.Actived)
                                          select new { x.ProductId, x.RoutingLot }).ToList();
                foreach (var product in products) {
                    var productInvsById = productInvs.Where(pi => pi.ProductId == product.ProductId).ToList();
                    var weight = MyUtilities.Product.GetProductInvWeight(product.ProductId, warehouseId);
                    if (!productInvsById.Any()) {
                        var entity = new ProductInventoryModel {
                            ProductCode = product.ProductCode,
                            ProductId = product.ProductId,
                            TotalQty = 0,
                            AvailableQty = 0,
                            ProductWeight = weight,
                            LotNumber = "",
                            NextWarehouseId = -1,
                            NextWarehouseIds = "",
                            ProductImg = product.DrawingFinish,
                            UploadDate = product.UploadDate.ToString("yyyyMMddhhmmss"),
                            CanRotate = false
                        };
                        if (string.IsNullOrWhiteSpace(entity.ProductImg))
                            entity.ProductImg = "askquestion.jpg";
                        models.Add(entity);
                    }
                    else {
                        foreach (var productInventory in productInvsById) {
                            var entity = new ProductInventoryModel {
                                ProductCode = product.ProductCode,
                                ProductId = product.ProductId,
                                TotalQty = productInventory.TotalQty,
                                ProductInventoryId = productInventory.ProductInventoryId,
                                ProductWeight = weight,
                                LotNumber = productInventory.LotNumber + "",
                                NextWarehouseId = warehouse.IsMainProcess ? 0 : -1,
                                NextWarehouseIds = "",
                                ProductImg = product.DrawingFinish,
                                UploadDate = product.UploadDate.ToString("yyyyMMddhhmmss"),
                                StoreCode = productInventory.StoreCode + "",
                            };
                            if (string.IsNullOrWhiteSpace(entity.ProductImg))
                                entity.ProductImg = "askquestion.jpg";
                            entity.AvailableQty = GetWarehouseInvPeriod(productInventory.ProductInventoryId);

                            var avaiableWOById =
                                (avaiableWorkOrders != null && avaiableWorkOrders.Any())
                                ? avaiableWorkOrders.FirstOrDefault(x => x.ProductId == entity.ProductId
                                                                    && x.RoutingLot.Equals(entity.LotNumber))
                                : null;

                            if (isManager) {
                                foreach (var wid in receiptWarehouseIds) {
                                    entity.NextWarehouseIds += "|" + wid + "|";
                                }
                            }
                            else if (avaiableWOById != null) {
                                entity.NextWarehouseIds += "";
                            }
                            else {
                                if (productInventory.ByProcessMachineId != null) {
                                    var ppmById = productionProcessByMachines.FirstOrDefault(x => x.DetailId == productInventory.ByProcessMachineId);
                                    if (ppmById != null) {
                                        var nextProcess = productionProcessByMachines
                                            .Where(pp => pp.MachineId == ppmById.MachineId &&
                                                         pp.ProductId == ppmById.ProductId &&
                                                         pp.ProcessIndex > ppmById.ProcessIndex)
                                            .OrderBy(pp => pp.ProcessIndex)
                                            .FirstOrDefault();
                                        if (nextProcess != null) {
                                            receiptWarehouseIds.Add(nextProcess.WarehouseId);
                                            entity.NextProcess = nextProcess.WarehouseName;
                                            entity.NextWarehouseId = nextProcess.WarehouseId;
                                        }
                                    }
                                }
                                var process =
                                    productionProcesses.FirstOrDefault(
                                        pp => pp.ProductId == entity.ProductId &&
                                              warehouseIds.Contains(pp.WarehouseId));
                                if (process != null) {
                                    if (entity.NextWarehouseId == 0) {

                                        var nextProcess = productionProcesses
                                            .Where(pp => pp.ProductId == process.ProductId &&
                                                         pp.ProcessIndex > process.ProcessIndex)
                                            .OrderBy(pp => pp.ProcessIndex)
                                            .FirstOrDefault();
                                        if (nextProcess != null) {
                                            receiptWarehouseIds.Add(nextProcess.WarehouseId);
                                            entity.NextProcess = nextProcess.WarehouseName;
                                            entity.NextWarehouseId = nextProcess.WarehouseId;
                                        }
                                    }
                                    var previousProcess = productionProcesses
                                                .Where(pp => pp.ProductId == process.ProductId &&
                                                             pp.ProcessIndex < process.ProcessIndex)
                                                .OrderByDescending(pp => pp.ProcessIndex)
                                                .FirstOrDefault();
                                    if (previousProcess != null) {
                                        receiptWarehouseIds.Add(previousProcess.WarehouseId);
                                    }
                                }

                                if (receiptWarehouseIds.Any(x => qcWarehouses.Contains(x))) {
                                    receiptWarehouseIds.AddRange(qcWarehouses);
                                }
                                else if (receiptWarehouseIds.Any(x => p2Warehouses.Contains(x))) {
                                    receiptWarehouseIds.AddRange(p2Warehouses);
                                }
                                else if (receiptWarehouseIds.Any(x => rpWarehouses.Contains(x))) {
                                    receiptWarehouseIds.AddRange(rpWarehouses);
                                }
                                receiptWarehouseIds = receiptWarehouseIds.Distinct().ToList();
                                foreach (var id in receiptWarehouseIds) {
                                    entity.NextWarehouseIds += "|" + id + "|";
                                }
                            }
                            models.Add(entity);
                        }
                    }

                }
            }
            return View(new GridModel(models.OrderBy(f => f.ProductCode).ThenBy(f => f.LotNumber)));
        }

        [GridAction]
        public ActionResult SelectProductInventoryByCustomerAndWarehouse(
            int customerId,
            int? warehouseId
            , string eoi, string productName) {
            //tam ma
            var models = new List<ProductInventoryModel>();
            if (customerId == 0 && string.IsNullOrWhiteSpace(productName)) {
                return View(new GridModel(models));
            }
            using (var vfi = new tammaContext()) {
                var products = vfi.Products.Where(p => p.Active).ToList();
                if (customerId != 0) {
                    products = products.Where(p => p.CustomerId == customerId).ToList();
                }
                if (!String.IsNullOrWhiteSpace(productName)) {
                    products = products.Where(p => p.ProductCode.ToUpper().Contains(productName.ToUpper())).ToList();
                }
                foreach (var product in products) {
                    var productInventorys =
                        vfi.ProductInventories.Where(
                            pi => pi.ProductId == product.ProductId && pi.WarehouseId == warehouseId && pi.TotalQty > 0);
                    var weight = 0.0;
                    switch (warehouseId.Value) {
                        case 1:
                            weight = product.ProductionWeight ?? 0;
                            break;
                        //2	NULL	Kho SX 2/ CNC
                        case 2:
                            weight = product.CncWeight ?? 0;
                            break;
                        case 12:
                        case 18:
                        case 19:
                        case 20:
                            weight = product.Production2Weight ?? 0;
                            break;
                        //3	NULL	Chờ nhiệt luyện
                        case 3:
                            weight = product.HeatTreatmentWeight ?? 0;
                            break;
                        //4	NULL	Chờ rung bóng
                        case 4:
                            weight = product.SurfaceTreatmentWeight ?? 0;
                            break;
                        //5	NULL	Chờ GCN
                        case 5:
                            weight = product.WaitingPlatingWeight ?? 0;
                            break;
                        //6	NULL	Kho nhà cung ứng
                        case 6:
                            weight = product.PlatingWeight ?? 0;
                            break;
                        default:
                            weight = product.QcWeight ?? 0;
                            break;
                    }
                    if (!productInventorys.Any()) {
                        var entity = new ProductInventoryModel {
                            ProductCode = product.ProductCode,
                            ProductId = product.ProductId,
                            TotalQty = 0,
                            AvailableQty = 0,
                            ProductWeight = weight,
                        };
                        models.Add(entity);
                    }
                    else {
                        foreach (var productInventory in productInventorys) {
                            var entity = new ProductInventoryModel {
                                ProductCode = product.ProductCode,
                                ProductId = product.ProductId,
                                TotalQty = productInventory.TotalQty,
                                ProductInventoryId = productInventory.ProductInventoryId,
                                ProductWeight = weight
                            };
                            entity.AvailableQty = GetWarehouseInvPeriod(productInventory.ProductInventoryId);
                            if (productInventory.ErrorId != null) {
                                entity.Note += productInventory.ProcessError.Description +
                                               (productInventory.ImportDate != null
                                                    ? " - Nhập:" + productInventory.ImportDate.Value.ToString("dd/MM/yyyy")
                                                    : "");
                            }
                            models.Add(entity);

                        }
                    }

                }
            }
            return View(new GridModel(models.OrderBy(f => f.ProductCode)));
        }

        [GridAction]
        public ActionResult SelectProductInventoryByRotate(
            int materialId
            , int warehouseIssue, int warehouseReceipt
            , string eoi) {
            return View(new GridModel(new List<ProductInventoryRotateModel>()));
        }

        [GridAction]
        public ActionResult SelectProductInventoryRotateByCustomer(
            int customerId
            , int warehouseIssue, int warehouseReceipt
            , string eoi, string productName) {
            //var warehouseId = eoi ? warehouseIssue : warehouseReceipt;

            //if (customerId == 0 || warehouseIssue == 0 || warehouseReceipt == 0)
            //    return View(new GridModel(new List<ProductInventoryRotateModel>()));
            try {
                if (warehouseIssue == 0 || warehouseReceipt == 0)
                    return View(new GridModel(new List<ProductInventoryRotateModel>()));
                if (customerId == 0 && productName == "")
                    return View(new GridModel(new List<ProductInventoryRotateModel>()));

                using (var vfi = new tammaContext()) {
                    var products = vfi.Products.Where(p => p.Active);

                    if (customerId != 0)
                        products = products.Where(f => f.CustomerId == customerId);
                    if (productName != "")
                        products = products.Where(f => f.ProductCode.Contains(productName.ToUpper()));

                    var models = (from p in products
                                  select new ProductInventoryRotateModel {
                                      ProductId = p.ProductId,
                                      ProductCode = p.ProductCode,
                                      CustomerCode = p.Customer.CustomerCode,
                                      ProductName = p.ProductName,
                                      TotalQtyExport = vfi.ProductInventories.FirstOrDefault(pi => pi.ProductId == p.ProductId).TotalQty,
                                  }).ToList();

                    return View(new GridModel(models));
                }

            }
            catch (FormatException) {
                return View(new GridModel(new List<ProductInventoryRotateModel>()));
            }
            catch (Exception exception) {
                throw new Exception(exception.Message);
            }
        }

        [GridAction]
        public ActionResult SelectProductInventory(string productIds, int warehouseId) {

            var model = (List<ProductInventoryRotateModel>)Session["SessionRotateTransactionProduct"];
            if (model == null || !model.Any()) model = new List<ProductInventoryRotateModel>();
            if (string.IsNullOrWhiteSpace(productIds))
                return View(new GridModel(model));

            int[] checkedRecords;
            try {
                var lst = productIds.Split(':');
                checkedRecords = new int[lst.Count()];
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords[i] = Convert.ToInt32(lst.ElementAt(i));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<ProductInventoryRotateModel>()));
            }
            if (checkedRecords.Count() <= 0)
                return View(new GridModel(model));
            if (checkedRecords[0] == 0) {
                return View(new GridModel(model));
            }
            using (var vfi = new tammaContext()) {
                var productInvs = from pi in vfi.ProductInventories
                                  where checkedRecords.Contains(pi.ProductId) &&
                                        pi.WarehouseId == warehouseId &&
                                        pi.TotalQty > 0
                                  select pi;
                foreach (var productId in checkedRecords) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null)
                        continue;
                    var productWeight = 0.0;
                    switch (warehouseId) {
                        case 1:
                            productWeight = product.ProductionWeight ?? 0;
                            break;
                        //2	NULL	Kho SX 2/ CNC
                        case 2:
                            productWeight = product.CncWeight ?? 0;
                            break;
                        case 12:
                        case 18:
                        case 19:
                        case 20:
                            productWeight = product.Production2Weight ?? 0;
                            break;
                        //3	NULL	Chờ nhiệt luyện
                        case 3:
                            productWeight = product.HeatTreatmentWeight ?? 0;
                            break;
                        //4	NULL	Chờ rung bóng
                        case 4:
                            productWeight = product.SurfaceTreatmentWeight ?? 0;
                            break;
                        //5	NULL	Chờ GCN 
                        case 5:
                            productWeight = product.WaitingPlatingWeight ?? 0;
                            break;
                        //6	NULL	Kho nhà cung ứng
                        case 6:
                            productWeight = product.PlatingWeight ?? 0;
                            break;
                        default:
                            productWeight = product.QcWeight ?? 0;
                            break;
                    }
                    var productInvsById = productInvs.Where(m => m.ProductId == productId);
                    foreach (var productInv in productInvsById) {
                        var entity = new ProductInventoryRotateModel {
                            ProductInventoryId = productInv.ProductInventoryId,
                            ProductId = productId,
                            CustomerCode = product.Customer.CustomerCode,
                            ProductCode = product.ProductCode,
                            ProductInventoryReceiptId = warehouseId,
                            TotalQtyExport = productInv.TotalQty,
                            TotalQtyImport = 0,
                            ProductWeight = productWeight,
                            LotNumber = (productInv.LotNumber + "").Trim(),
                            TotalQty = productInv.TotalQty,
                        };
                        entity.AvailableQty = GetWarehouseInvPeriod(productInv.ProductInventoryId);
                        model.Add(entity);
                    }
                    if (!productInvsById.Any()) {
                        var entity = new ProductInventoryRotateModel {
                            ProductInventoryId = 0,
                            ProductId = productId,
                            CustomerCode = product.Customer.CustomerCode,
                            ProductCode = product.ProductCode,
                            ProductInventoryReceiptId = warehouseId,
                            TotalQtyExport = 0,
                            TotalQtyImport = 0,
                            AvailableQty = 0,
                            ProductWeight = productWeight,
                            LotNumber = "",
                            TotalQty = 0,
                        };
                        model.Add(entity);
                    }
                }
            }
            model = model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ToList();
            Session["SessionRotateTransactionProduct"] = model;
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectProductRecipeById(int customerId, int warehouseId, string productCode) {
            var models = new List<ProductCombinationRecipeModel>();
            if (warehouseId == 0 || (customerId == 0 && string.IsNullOrWhiteSpace(productCode))) {
                return View(new GridModel(models));
            }
            models = GetProductCombinationRecipeModel(customerId, productCode, warehouseId, new List<int>());

            return View(new GridModel(models));
        }

        List<ProductCombinationRecipeModel> GetProductCombinationRecipeModel(int customerId, string productCode, int warehouseId, List<int> recipeIds) {
            var model = new List<ProductCombinationRecipeModel>();
            using (var vfi = new tammaContext()) {
                var recipes = (from x in vfi.ProductCombinationRecipes
                               where x.Active &&
                               (customerId == 0 || x.Product.CustomerId == customerId) &&
                               (!recipeIds.Any() || recipeIds.Contains(x.RecipeId)) &&
                               x.ProductCombinationRecipeDetails.Any()
                               select new {
                                   x.ProductId,
                                   x.RecipeId,
                                   x.RecipeCode,
                                   x.RecipeName,
                                   x.Product.ProductCode,
                                   x.Product.DrawingFinish,
                                   UploadDate = x.Product.UploadDate ?? DateTime.Now,
                                   Details = x.ProductCombinationRecipeDetails.Select(y =>
                                                 new ProductCombinationRecipeDetailModel {
                                                     FromProductId = y.FromProductId,
                                                     FromProductCode = y.Product.ProductCode,
                                                     RequireNumber = y.RequireNumber,
                                                 }).ToList(),
                                   FromProductIds = x.ProductCombinationRecipeDetails.Select(y => y.FromProductId)
                               }).ToList();
                if (!String.IsNullOrWhiteSpace(productCode)) {
                    recipes = recipes.Where(x => x.ProductCode.ToUpper().Contains(productCode.ToUpper())).ToList();
                }
                if (!recipes.Any()) {
                    return model;
                }

                var productIds = recipes.Select(x => x.ProductId).Distinct().ToList();
                var productInvs = (from pi in vfi.ProductInventories
                                   where
                                       productIds.Contains(pi.ProductId) &&
                                       pi.WarehouseId == warehouseId &&
                                       pi.TotalQty > 0
                                   select new {
                                       pi.ProductId,
                                       pi.TotalQty
                                   }).ToList();
                var fromProductIds = new List<int>();
                recipes.ForEach(x => fromProductIds.AddRange(x.FromProductIds));
                var fromProductInvs = (from pi in vfi.ProductInventories
                                       where
                                           fromProductIds.Contains(pi.ProductId) &&
                                           pi.WarehouseId == warehouseId &&
                                           pi.TotalQty > 0
                                       select new {
                                           pi.ProductId,
                                           pi.ProductInventoryId,
                                           pi.TotalQty
                                       }).ToList();
                var productInvIds = fromProductInvs.Select(x => x.ProductInventoryId).Distinct().ToList();
                var waitingApproves = vfi.TransactionDetails.Where(x => x.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                                                        x.ProductInvId != null &&
                                                                        productInvIds.Contains(x.ProductInvId.Value))
                                                            .ToList();
                foreach (var recipe in recipes) {
                    var entity = new ProductCombinationRecipeModel {
                        RecipeId = recipe.RecipeId,
                        RecipeName = recipe.RecipeName,
                        RecipeCode = recipe.RecipeCode,
                        ProductCode = recipe.ProductCode,
                        ProductImg = recipe.DrawingFinish,
                        UploadDate = recipe.UploadDate.ToString("yyyyMMddhhmmss"),
                        DetailDescription = ProductCombinationRecipeNote.GetDetailDescription(recipe.Details),
                        ProductWeight = MyUtilities.Product.GetProductInvWeight(recipe.ProductId, warehouseId)
                        //InvQuantity = recipe.Invs.Any() ? recipe.Invs.Sum() : 0,
                        //AvailableQuantity = recipe.Details.Min(x => MyUtilities.Function.RoundDown(x.InvQuantity / x.RequireNumber))
                    };
                    entity.InvQuantity = productInvs.Where(x => x.ProductId == entity.ProductId).Sum(x => x.TotalQty);
                    foreach (var detail in recipe.Details) {
                        detail.InvQuantity = fromProductInvs.Where(x => x.ProductId == detail.FromProductId).Sum(x => x.TotalQty);
                        detail.InvQuantity -= waitingApproves.Where(x => x.ReferenceId == detail.FromProductId).Sum(x => x.Quantity);
                        detail.CombineQuantity = MyUtilities.Function.RoundDown(detail.InvQuantity / detail.RequireNumber);
                    }
                    entity.AvailableQuantity = recipe.Details.Min(x => x.CombineQuantity);

                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectProductRecipePicked(string ids, int warehouseId) {
            var model = new List<ProductCombinationRecipeModel>();
            //var model = (List<ProductCombinationRecipeModel>)Session["SessionRecipeTransactionProduct"];
            //if (model == null || !model.Any()) model = new List<ProductCombinationRecipeModel>();
            if (string.IsNullOrWhiteSpace(ids))
                return View(new GridModel(model));

            int[] checkedRecords;
            try {
                var lst = ids.Split(':');
                checkedRecords = new int[lst.Count()];
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords[i] = Convert.ToInt32(lst.ElementAt(i));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<ProductCombinationRecipeModel>()));
            }
            if (checkedRecords.Count() <= 0)
                return View(new GridModel(model));
            if (checkedRecords[0] == 0) {
                return View(new GridModel(model));
            }
            model = GetProductCombinationRecipeModel(0, "", warehouseId, checkedRecords.ToList());
            //model = model.Where(x => !checkedRecords.Contains(x.RecipeId)).ToList();
            //model.AddRange(GetProductCombinationRecipeModel(0, "", warehouseId, checkedRecords.ToList()));

            //model = model.OrderBy(x => x.CustomerCode).ThenBy(m => m.ProductCode).ToList();
            //var groupIndex = 1;
            //var pIds = model.Select(x => x.ProductId).Distinct().ToList();
            //foreach (var productId in pIds) {
            //    model.Where(x => x.ProductId == productId).ToList().ForEach(x => x.GroupIndex = groupIndex);
            //    groupIndex++;
            //}
            //Session["SessionRecipeTransactionProduct"] = model;
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult InputProductInventoryByRotate(string productIds, int warehouseIssue, int warehouseReceipt) {
            var model = (List<ProductInventoryRotateModel>)Session["SessionRotateTransactionProduct"];
            if (model == null || !model.Any()) model = new List<ProductInventoryRotateModel>();
            if (string.IsNullOrWhiteSpace(productIds))
                return View(new GridModel(model));

            int[] checkedRecords;
            try {
                var lst = productIds.Split(':');
                checkedRecords = new int[lst.Count()];
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords[i] = Convert.ToInt32(lst.ElementAt(i));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<ProductInventoryRotateModel>()));
            }
            if (checkedRecords.Count() <= 0)
                return View(new GridModel(model));
            if (checkedRecords[0] == 0) {
                return View(new GridModel(model));
            }
            using (var vfi = new tammaContext()) {
                var productInvs = (from pi in vfi.ProductInventories
                                   where
                                       checkedRecords.Contains(pi.ProductInventoryId) &&
                                       pi.WarehouseId == warehouseIssue &&
                                       pi.TotalQty > 0
                                   select new {
                                       pi.ProductInventoryId,
                                       pi.ProductId,
                                       pi.Product.Customer.CustomerCode,
                                       pi.Product.ProductCode,
                                       pi.TotalQty,
                                       pi.LotNumber,
                                       pi.StoreCode
                                   }).ToList();
                foreach (var productInv in productInvs) {
                    var entity = model.FirstOrDefault(m => m.ProductInventoryId == productInv.ProductInventoryId);
                    if (entity == null) {
                        entity = new ProductInventoryRotateModel {
                            ProductInventoryId = productInv.ProductInventoryId,
                            ProductId = productInv.ProductId,
                            CustomerCode = productInv.CustomerCode,
                            ProductCode = productInv.ProductCode,
                            ProductInventoryIssueId = warehouseIssue,
                            ProductInventoryReceiptId = warehouseReceipt,
                            TotalQtyExport = productInv.TotalQty,
                            TotalQtyImport = 0,
                            ProductWeight = 0,
                            LotNumber = (productInv.LotNumber + "").Trim(),
                            StoreCode = (productInv.StoreCode + "").Trim(),
                        };
                        model.Add(entity);
                    }
                    entity.AvailableQty = GetWarehouseInvPeriod(productInv.ProductInventoryId);
                    if (entity.AvailableQty <= 0) {
                        model.Remove(entity);
                        continue;
                    }
                    entity.ProductWeight = MyUtilities.Product.GetProductInvWeight(productInv.ProductId, warehouseIssue);
                }
            }
            model = model.OrderBy(m => m.ProductCode).ThenBy(m => m.LotNumber).ToList();
            var groupIndex = 1;
            var pIds = model.Select(x => x.ProductId).Distinct().ToList();
            foreach (var productId in pIds) {
                model.Where(x => x.ProductId == productId).ToList().ForEach(x => x.GroupIndex = groupIndex);
                groupIndex++;
            }
            Session["SessionRotateTransactionProduct"] = model;
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult InputProductInventoryByImportInternal(string productIds, int warehouseId) {
            var model = (List<ProductInventoryRotateModel>)Session["SessionImportInternalTransactionProduct"];
            if (model == null || !model.Any()) model = new List<ProductInventoryRotateModel>();
            if (string.IsNullOrWhiteSpace(productIds))
                return View(new GridModel(model));

            int[] checkedRecords;
            try {
                var lst = productIds.Split(':');
                checkedRecords = new int[lst.Count()];
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords[i] = Convert.ToInt32(lst.ElementAt(i));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<ProductInventoryRotateModel>()));
            }
            if (checkedRecords.Count() <= 0)
                return View(new GridModel(model));
            if (checkedRecords[0] == 0) {
                return View(new GridModel(model));
            }
            using (var vfi = new tammaContext()) {
                var products = (from x in vfi.Products
                                where checkedRecords.Contains(x.ProductId)
                                select new {
                                    x.ProductId,
                                    x.Customer.CustomerCode,
                                    x.ProductCode,
                                }).ToList();
                var productInvs = (from x in vfi.ProductInventories
                                   where checkedRecords.Contains(x.ProductId) &&
                                       x.WarehouseId == warehouseId &&
                                       x.TotalQty > 0
                                   select new {
                                       x.ProductId,
                                       x.TotalQty,
                                       x.ProductInventoryId,
                                       x.LotNumber
                                   }).ToList();
                foreach (var productId in checkedRecords) {
                    var product = products.FirstOrDefault(x => x.ProductId == productId);
                    var productInvsById = productInvs.Where(x => x.ProductId == productId);
                    if (productInvsById.Any()) {
                        foreach (var productInv in productInvs) {
                            var entity = model.FirstOrDefault(m => m.ProductInventoryId == productInv.ProductInventoryId);
                            if (entity == null) {
                                entity = new ProductInventoryRotateModel {
                                    ProductInventoryId = productInv.ProductInventoryId,
                                    ProductId = productId,
                                    CustomerCode = product.CustomerCode,
                                    ProductCode = product.ProductCode,
                                    ProductInventoryIssueId = 0,
                                    ProductInventoryReceiptId = 0,
                                    TotalQtyExport = productInv.TotalQty,
                                    TotalQtyImport = 0,
                                    ProductWeight = 0,
                                    LotNumber = (productInv.LotNumber + "").Trim(),
                                };
                                model.Add(entity);
                            }
                            entity.AvailableQty = GetWarehouseInvPeriod(productInv.ProductInventoryId);
                            entity.ProductWeight = MyUtilities.Product.GetProductInvWeight(productId, warehouseId);
                        }
                    }
                    else {
                        var entity = model.FirstOrDefault(m => m.ProductId == productId);
                        if (entity == null) {
                            if (product == null) continue;
                            entity = new ProductInventoryRotateModel {
                                ProductInventoryId = 0,
                                ProductId = productId,
                                CustomerCode = product.CustomerCode,
                                ProductCode = product.ProductCode,
                                ProductInventoryIssueId = 0,
                                ProductInventoryReceiptId = 0,
                                TotalQtyExport = 0,
                                TotalQtyImport = 0,
                                ProductWeight = 0,
                                LotNumber = "",
                                AvailableQty = 0
                            };
                            entity.ProductWeight = MyUtilities.Product.GetProductInvWeight(productId, warehouseId);
                            model.Add(entity);
                        }
                    }
                }
            }
            model = model.OrderBy(m => m.ProductCode).ThenBy(m => m.LotNumber).ToList();
            var groupIndex = 1;
            var pIds = model.Select(x => x.ProductId).Distinct().ToList();
            foreach (var productId in pIds) {
                model.Where(x => x.ProductId == productId).ToList().ForEach(x => x.GroupIndex = groupIndex);
                groupIndex++;
            }
            Session["SessionImportInternalTransactionProduct"] = model;
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult InputProductInventoryByExportInternal(string productIds, int warehouseIssue) {
            var model = (List<ProductInventoryRotateModel>)Session["SessionExportInternalTransactionProduct"];
            if (model == null || !model.Any()) model = new List<ProductInventoryRotateModel>();
            if (string.IsNullOrWhiteSpace(productIds))
                return View(new GridModel(model));

            int[] checkedRecords;
            try {
                var lst = productIds.Split(':');
                checkedRecords = new int[lst.Count()];
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords[i] = Convert.ToInt32(lst.ElementAt(i));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<ProductInventoryRotateModel>()));
            }
            if (checkedRecords.Count() <= 0)
                return View(new GridModel(model));
            if (checkedRecords[0] == 0) {
                return View(new GridModel(model));
            }
            using (var vfi = new tammaContext()) {
                var productInvs = (from x in vfi.ProductInventories
                                   where checkedRecords.Contains(x.ProductInventoryId) &&
                                       x.WarehouseId == warehouseIssue &&
                                       x.TotalQty > 0
                                   select new {
                                       x.ProductInventoryId,
                                       x.ProductId,
                                       x.Product.Customer.CustomerCode,
                                       x.Product.ProductCode,
                                       x.TotalQty,
                                       x.LotNumber
                                   }).ToList();
                foreach (var productInv in productInvs) {
                    var entity = model.FirstOrDefault(m => m.ProductInventoryId == productInv.ProductInventoryId);
                    if (entity == null) {
                        entity = new ProductInventoryRotateModel {
                            ProductInventoryId = productInv.ProductInventoryId,
                            ProductId = productInv.ProductId,
                            CustomerCode = productInv.CustomerCode,
                            ProductCode = productInv.ProductCode,
                            ProductInventoryIssueId = warehouseIssue,
                            ProductInventoryReceiptId = 0,
                            TotalQtyExport = productInv.TotalQty,
                            TotalQtyImport = 0,
                            ProductWeight = 0,
                            LotNumber = (productInv.LotNumber + "").Trim(),
                        };
                        model.Add(entity);
                    }
                    entity.AvailableQty = GetWarehouseInvPeriod(productInv.ProductInventoryId);
                    if (entity.AvailableQty <= 0) {
                        model.Remove(entity);
                        continue;
                    }
                    entity.ProductWeight = MyUtilities.Product.GetProductInvWeight(productInv.ProductId, warehouseIssue);
                }
            }
            model = model.OrderBy(m => m.ProductCode).ThenBy(m => m.LotNumber).ToList();
            var groupIndex = 1;
            var pIds = model.Select(x => x.ProductId).Distinct().ToList();
            foreach (var productId in pIds) {
                model.Where(x => x.ProductId == productId).ToList().ForEach(x => x.GroupIndex = groupIndex);
                groupIndex++;
            }
            Session["SessionExportInternalTransactionProduct"] = model;
            return View(new GridModel(model));
        }

        public double GetWarehouseInvPeriod(int productInvId) {
            var inv = 0.0;
            using (var vfi = new tammaContext()) {

                //var ci = new CultureInfo("vi-VN");
                //DateTime from;
                //DateTime to;
                var warehouseInv =
                    vfi.ProductInventories.FirstOrDefault(
                        pi => pi.ProductInventoryId == productInvId);
                if (warehouseInv != null)
                    inv = warehouseInv.TotalQty;
                var transactionDetails =
                    vfi.TransactionDetails.Where(
                        td =>
                        td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open &&
                        td.ProductInvId == productInvId).ToList();
                inv -= transactionDetails.Sum(td => td.Quantity);
            }
            return inv;
        }

        [GridAction]
        public ActionResult InputProductInOrder(string productIds, int? id, int type) {
            if (string.IsNullOrWhiteSpace(productIds))
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));

            long[] checkedRecords;
            try {

                //checkedRecords = Convert.ToInt32(ids.Split(':'));
                //checkedRecords = Convert.ToInt32(ids.Split(':'));
                var lst = productIds.Split(':');
                checkedRecords = new long[lst.Count()];
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords[i] = Convert.ToInt32(lst.ElementAt(i));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            }

            if (checkedRecords.Count() <= 0)
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            if (checkedRecords[0] == 0) {
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            }
            if (id == 0 || id == null)
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));

            // material
            var model = new List<ExportFormTP_KDDetailsModel>();
            using (var vfi = new tammaContext()) {
                if (type == 1) {
                    var entityProducts =
                        vfi.OrderDetails.Where(od => od.OrderId == id && checkedRecords.Contains(od.OrderDetailId));
                    foreach (var detail in entityProducts) {
                        var entity = new ExportFormTP_KDDetailsModel {
                            DetailId = detail.OrderDetailId,
                            ProductId = detail.ProductId,
                            ProductCode = detail.Product.ProductCode,
                            RequiredNumber = detail.RequiedNumber,
                            Weight = detail.Product.QcWeight ?? 0
                        };
                        var inventory = vfi.ProductInventories.FirstOrDefault(
                            pi => pi.ProductId == entity.ProductId && pi.WarehouseId == MyUtilities.Warehouse.Finish);
                        entity.TotalQuantity = inventory != null ? inventory.TotalQty : 0.0;
                        model.Add(entity);
                    }
                }
                else if (type == 2) {
                    var entityProducts =
                        vfi.OrderNoteDetails.Where(ond => ond.NoteId == id && checkedRecords.Contains(ond.NoteDetailId));
                    var exportChanges =
                        vfi.ExportChangeProducts.Where(
                            ec => ec.NoteId == id && ec.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
                    foreach (var detail in entityProducts) {
                        var product = vfi.Products.FirstOrDefault(p => p.ProductId == detail.ProductId);
                        var entity = new ExportFormTP_KDDetailsModel {
                            DetailId = detail.NoteDetailId,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            RequiredNumber = Convert.ToInt32(detail.Quantity ?? 0),
                        };
                        var inventory = vfi.ProductInventories.FirstOrDefault(
                            pi => pi.ProductId == entity.ProductId && pi.WarehouseId == MyUtilities.Warehouse.Finish);
                        entity.TotalQuantity = inventory != null ? inventory.TotalQty : 0.0;
                        if (exportChanges.Any()) {
                            foreach (var exportChange in exportChanges) {
                                var transactionDetail =
                                    vfi.TransactionDetails.FirstOrDefault(
                                        td =>
                                        td.ReferenceId == product.ProductId &&
                                        td.TransactionId == exportChange.TransactionId);
                                if (transactionDetail != null)
                                    entity.RequiredNumber -= Convert.ToInt32(transactionDetail.Quantity);
                            }
                        }
                        model.Add(entity);
                    }
                }
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult InputProductOrderDetail(string exportDetailId) {
            if (string.IsNullOrWhiteSpace(exportDetailId)) {
                Session["SessionInvoiceDetail"] = new List<OrderDetailModel>();
                return View(new GridModel(new List<OrderDetailModel>()));
            }
            // material
            var model = new List<OrderDetailModel>();
            try {

                using (var vfi = new tammaContext()) {
                    var eDetail = Convert.ToInt32(exportDetailId);
                    var exportDetail = vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == eDetail);
                    if (exportDetail == null)
                        return View(new GridModel(new List<OrderDetailModel>()));
                    if (exportDetail.InvoiceDetails != null && exportDetail.InvoiceDetails.Any(id => id.Active))
                        throw new AggregateException("Sản phẩm đã chọn đơn hàng tương ứng");
                    var orderDetails = from od in vfi.OrderDetails
                                       where od.Order.CustomerId == exportDetail.ExportFormTP_KD.CustomerId
                                             && od.RequiedNumber > 0
                                             && od.ProductId == exportDetail.ProductId
                                             && od.Order.CustomerId == exportDetail.ExportFormTP_KD.CustomerId
                                             && od.Order.DueDate != null
                                             && (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                                 od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess)
                                       orderby od.Order.DueDate, od.CustomerDueDate
                                       select od;
                    var quantity = exportDetail.Quality;
                    foreach (var orderDetail in orderDetails) {
                        var entity = new OrderDetailModel {
                            OrderDetailId = orderDetail.OrderDetailId,
                            VFIDueDate = orderDetail.Order.DueDate,
                            OrderNumber = orderDetail.Order.OrderNumber,
                            ProductId = orderDetail.ProductId,
                            ProductCode = orderDetail.Product.ProductCode,
                            Note = orderDetail.Note,
                            RequiredNumber = orderDetail.RequiedNumber,
                            AvailableQty = orderDetail.RequiedNumber,
                            ExportDetailId = eDetail,
                            Currency = orderDetail.Order.CurrencyCode,
                            UnitPrice = orderDetail.UnitPrice,
                            PONumber = orderDetail.PONumber,
                            CustomerDueDate = orderDetail.CustomerDueDate
                        };
                        if (quantity == 0)
                            entity.AvailableQty = 0;
                        else if (orderDetail.RequiedNumber > quantity) {
                            entity.AvailableQty = quantity;
                            quantity = 0;
                        }
                        else {
                            quantity -= entity.AvailableQty;
                            if (quantity < 0)
                                quantity = 0;
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InputProductOrderDetail", ex.Message);
            }
            Session["SessionInvoiceDetail"] = model;
            return View(new GridModel(model));
        }


        #endregion

        #region ProductInventoryPeriod

        [GridAction]
        public ActionResult SelectProductInventoryPeriodByCustomerAndWarehouse(
            int customerId
            //, int warehouseIssue, int warehouseReceipt
            , int warehouseId
            , string productCode
            , string lotnumber
            , string fromDate, string toDate,
            bool isGroup) {
            //var warehouseId = eoi ? warehouseIssue : warehouseReceipt;
            var ci = new CultureInfo("vi-VN");
            DateTime toDay = DateTime.Today;

            var fDate = string.IsNullOrWhiteSpace(fromDate)
                            ? new DateTime(toDay.Year, toDay.Month, 1)
                            : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                            ? toDay
                            : Convert.ToDateTime(toDate, ci);
            var model = new List<TransactionDetailModel>();
            using (var vfi = new tammaContext()) {
                var productsShow = new List<int>();
                if (warehouseId == 0 && string.IsNullOrWhiteSpace(productCode) && customerId == 0)
                    return View(new GridModel(model));
                var products = vfi.Products.Where(x => x.Active && (customerId == 0 || x.CustomerId == customerId));
                if (!string.IsNullOrWhiteSpace(productCode)) {
                    products = products.Where(p => p.ProductCode.Contains(productCode));
                    if (!products.Any())
                        return View(new GridModel(model));
                }
                var productIds = products.Select(x => x.ProductId).ToList();
                var periods = (from pip in vfi.ProductInventoryPeriods
                               where
                                   pip.PeriodDate >= fDate && pip.PeriodDate <= tDate &&
                                   (productIds.Contains(pip.ProductId)) &&
                                   (warehouseId == 0 || pip.WarehouseId == warehouseId)
                               select new {
                                   pip.Product,
                                   pip.ProductId,
                                   pip.Product.ProductCode,
                                   pip.Quantity,
                                   pip.LastPeriodQuantity,
                                   pip.EarlyPeriodQuantity,
                                   pip.PeriodDate,
                                   pip.WarehouseId,
                                   pip.TransactionId,
                                   pip.Transaction,
                                   pip.ModifiedDate,
                                   pip.ModifiedUser,
                                   pip.ProductInventory.LotNumber,
                                   WarehouseReceiptName = pip.Transaction.WarehouseReceiptId != null
                                   ? pip.Transaction.Warehouse1.WarehouseName
                                   : "",
                                   WarehouseIssueName = pip.Transaction.WarehouseIssueId != null
                                   ? pip.Transaction.Warehouse.WarehouseName
                                   : "",
                               }).ToList();
                if (!string.IsNullOrWhiteSpace(lotnumber)) {
                    periods = periods.Where(x => x.LotNumber.Contains(lotnumber)).ToList();
                }
                if (isGroup) {
                    var groups = periods.GroupBy(x => new {
                        x.ProductId,
                        x.ProductCode,
                        x.WarehouseIssueName,
                        x.WarehouseReceiptName,
                    })
                    .Select(x => new {
                        x.Key.ProductId,
                        x.Key.ProductCode,
                        x.Key.WarehouseIssueName,
                        x.Key.WarehouseReceiptName,
                        Quantity = x.Sum(y => y.Quantity),
                        EarlyPeriodQuantity = x.Sum(y => y.EarlyPeriodQuantity),
                        LastPeriodQuantity = x.Sum(y => y.LastPeriodQuantity),
                    })
                    .ToList();
                    foreach (var group in groups) {
                        var entity = new TransactionDetailModel {
                            ReferenceId = group.ProductId,
                            ProductCode = group.ProductCode,
                            Quantity = group.Quantity,
                            EarlyQuantity = group.EarlyPeriodQuantity,
                            LastQuantity = group.LastPeriodQuantity,
                            WarehouseReceiptName = group.WarehouseReceiptName,
                            WarehouseIssueName = group.WarehouseIssueName,
                        };
                        if (entity.EarlyQuantity > entity.LastQuantity)
                            entity.EoIName = "Xuất";
                        else
                            entity.EoIName = "Nhập";
                        model.Add(entity);
                    }
                }
                else {
                    foreach (var pp in periods) {
                        var transactionDetail =
                            vfi.TransactionDetails.FirstOrDefault(
                                td => td.TransactionId == pp.TransactionId
                                    && td.ReferenceId == pp.ProductId
                                    && td.LotNumber.Equals(pp.LotNumber));
                        if (transactionDetail == null) continue;
                        var entity = new TransactionDetailModel {
                            TransactionDetailId = transactionDetail.TransactionDetailId,
                            PeriodDate = pp.PeriodDate,
                            ProductCode = pp.Product.ProductCode,
                            Quantity = pp.Quantity,
                            EarlyQuantity = pp.EarlyPeriodQuantity,
                            LastQuantity = pp.LastPeriodQuantity,
                            WarehouseReceiptName = pp.WarehouseReceiptName,
                            WarehouseIssueName = pp.WarehouseIssueName,
                            Note = transactionDetail.Note,
                            ModifiedDate = pp.ModifiedDate,
                            ModifiedUser = pp.ModifiedUser,
                            LotNumber = pp.LotNumber,
                            ReferenceId = pp.ProductId
                        };
                        if (entity.EarlyQuantity > entity.LastQuantity)
                            entity.EoIName = "Xuất";
                        else
                            entity.EoIName = "Nhập";
                        //}
                        if ((pp.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Plating ||
                            pp.Transaction.WarehouseIssueId == MyUtilities.Warehouse.PlatingTest) &&
                                pp.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcB) {
                            var import =
                                vfi.ImportNCU_QCB.FirstOrDefault(
                                    id =>
                                    id.TransactionId == pp.Transaction.TransactionId);
                            if (import != null) {
                                entity.Note = import.PlatingForm.PlatingFormNumber + "-" + import.PlatingForm.Vendor.VendorCode;
                            }
                        }
                        if (pp.Transaction.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating &&
                            (pp.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.PlatingTest ||
                                pp.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Plating)) {
                            var export =
                                vfi.ExportGCN_NCU.FirstOrDefault(
                                    id =>
                                    id.TransactionId == pp.Transaction.TransactionId);
                            if (export != null) {
                                entity.Note = export.PlatingForm.PlatingFormNumber + "-" + export.PlatingForm.Vendor.VendorCode;
                            }
                        }
                        if (pp.WarehouseId == MyUtilities.Warehouse.Business || pp.WarehouseId == MyUtilities.Warehouse.Destroy) {
                            entity.LastQuantity = 0;
                            entity.EarlyQuantity = 0;
                        }
                        entity.UnitWeight = MyUtilities.Product.GetProductInvWeight(entity.ReferenceId.Value, pp.WarehouseId);
                        entity.QuantityKg = entity.Quantity * entity.UnitWeight / 1000;
                        model.Add(entity);
                    }
                }
            }
            return View(new GridModel(model.OrderByDescending(m => m.PeriodDate).ThenByDescending(m => m.ModifiedDate)));

        }

        [GridAction]
        public ActionResult SelectAllProductInventory(
            int customerId
            , string productName
            , string monthlyDate) {
            try {
                using (var vfi = new tammaContext()) {
                    if (string.IsNullOrWhiteSpace(monthlyDate))
                        return View(new GridModel(new List<SoLieuTongHopSanPham>()));
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var ci = new CultureInfo("vi-VN");

                    var monthly = string.IsNullOrWhiteSpace(monthlyDate)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(monthlyDate, ci);
                    monthly = new DateTime(monthly.Year, monthly.Month, monthly.Day);
                    monthly = monthly.AddDays(1);
                    monthly = monthly.AddSeconds(-1);
                    var productsShow = vfi.Products.Where(p => p.Active).Select(p => p.ProductId).Distinct().ToList();
                    if (customerId != 0 && !string.IsNullOrWhiteSpace(productName)) {
                        productsShow =
                            vfi.Products.Where(
                                p => p.CustomerId == customerId && p.ProductCode.Contains(productName) && p.Active)
                               .Select(p => p.ProductId)
                               .ToList();
                        if (!productsShow.Any())
                            return View(new GridModel(new List<SoLieuTongHopSanPham>()));
                    }
                    else if (customerId != 0) {
                        productsShow =
                            vfi.Products.Where(p => p.CustomerId == customerId && p.Active)
                               .Select(p => p.ProductId)
                               .ToList();
                        if (!productsShow.Any())
                            return View(new GridModel(new List<SoLieuTongHopSanPham>()));
                    }
                    else if (!string.IsNullOrWhiteSpace(productName)) {
                        productsShow =
                            vfi.Products.Where(p => p.ProductCode.Contains(productName) && p.Active)
                               .Select(p => p.ProductId)
                               .ToList();
                        if (!productsShow.Any())
                            return View(new GridModel(new List<SoLieuTongHopSanPham>()));
                    }

                    var allProductInventoryPeriodsByDate = productsShow.Any()
                        ? (from pip in vfi.ProductInventoryPeriods
                           where
                           pip.PeriodDate < monthly &&
                           productsShow.Contains(pip.ProductId)
                           select new {
                               pip.Product.CustomerId,
                               pip.ProductId,
                               pip.Quantity,
                               pip.LastPeriodQuantity,
                               pip.EarlyPeriodQuantity,
                               pip.PeriodDate,
                               pip.WarehouseId,
                               pip.Transaction,
                           }).ToList()
                        : (from pip in vfi.ProductInventoryPeriods
                           where pip.PeriodDate < monthly
                           select new {
                               pip.Product.CustomerId,
                               pip.ProductId,
                               pip.Quantity,
                               pip.LastPeriodQuantity,
                               pip.EarlyPeriodQuantity,
                               pip.PeriodDate,
                               pip.WarehouseId,
                               pip.Transaction,
                           }).ToList();

                    var giaodichKho1 = (from p in allProductInventoryPeriodsByDate
                                        where p.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Production1 &&
                                              p.Transaction.WarehouseIssueId == null &&
                                              p.PeriodDate.Month == monthly.Month &&
                                              p.PeriodDate.Year == monthly.Year
                                        select p).ToList();

                    var sxKho1 = (from p in giaodichKho1
                                  where p.PeriodDate.Day == monthly.Day
                                  select p).ToList();
                    var ppInMonth = (from p in allProductInventoryPeriodsByDate
                                     where p.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Defect &&
                                           p.Transaction.WarehouseIssueId != null &&
                                           p.WarehouseId == MyUtilities.Warehouse.Defect &&
                                           p.PeriodDate.Month == monthly.Month &&
                                           p.PeriodDate.Year == monthly.Year
                                     select p).ToList();
                    var giaodichKho1Huy = (from p in allProductInventoryPeriodsByDate
                                           where p.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Destroy &&
                                                 p.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1 &&
                                                 p.PeriodDate.Month == monthly.Month &&
                                                 p.PeriodDate.Year == monthly.Year &&
                                                 p.WarehouseId == MyUtilities.Warehouse.Production1
                                           select p).ToList();

                    var sxKho1Huy = (from p in giaodichKho1Huy
                                     where p.PeriodDate.Day == monthly.Day
                                     select p).ToList();
                    //////////////////////
                    var giaodichKD = (from p in allProductInventoryPeriodsByDate
                                      where p.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Finish &&
                                            p.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Business &&
                                            p.WarehouseId == MyUtilities.Warehouse.Business &&
                                            p.PeriodDate.Month == monthly.Month &&
                                            p.PeriodDate.Year == monthly.Year
                                      select p).ToList();
                    var xuatTP = (from p in giaodichKD
                                  where p.PeriodDate.Day == monthly.Day
                                  select p).ToList();
                    var listHangTra = (from ond in vfi.OrderNoteDetails
                                       where ond.OrderNote.CreatedDate.Month == monthly.Month &&
                                             ond.OrderNote.CreatedDate.Year == monthly.Year &&
                                             ond.OrderNote.Transaction.Status ==
                                             (byte)MyUtilities.Transaction.Status.Approved
                                       && productsShow.Contains(ond.ProductId.Value)
                                       select new {
                                           ond.ProductId,
                                           Quantity = ond.Quantity ?? 0
                                       }).ToList();
                    // 0 : SX1,1 : SX2/CNC,2 : NhietLuyen,3 : RungBong,4 : GCN,5 : NCU,6 : QCA,7 : CXL,8 : PP,9 : TPA,10 : KD,11 : SX2/SX2,12 : QCB,13 : TPB,14 : LuyKeSX,15 : LuyKeXuat

                    var tatcaSP = productsShow.Any()
                                      ? productsShow
                                      : vfi.Products.Where(p => p.Active).Select(p => p.ProductId).Distinct().ToList();

                    var tonkho = new List<SoLieuTongHopSanPham>();
                    int index = 1;

                    //foreach (var customer in customers)
                    //{
                    //    var products = from p in vfi.Products
                    //                   where p.CustomerId == customer.CustomerId &&
                    //                         productsShow.Contains(p.ProductId)
                    //                   orderby p.ProductCode
                    //                   select new
                    //                       {
                    //                           p.ProductId,
                    //                           p.ProductCode,
                    //                           ForecastsQuality = p.ForecastsQuality ?? 0,
                    //                       };
                    var production2Ids = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    var products = vfi.Products.Where(x => tatcaSP.Contains(x.ProductId)).ToList();
                    foreach (var product in products) {
                        var sp = new SoLieuTongHopSanPham();
                        //don hang
                        sp.LuyKeSX = giaodichKho1.Where(p => p.ProductId == product.ProductId).Sum(p => p.Quantity) -
                                     giaodichKho1Huy.Where(p => p.ProductId == product.ProductId).Sum(p => p.Quantity);
                        sp.LuyKeXuat = giaodichKD.Where(p => p.ProductId == product.ProductId).Sum(p => p.Quantity);
                        var hangTra =
                            listHangTra.Where(ond => ond.ProductId == product.ProductId).Sum(ond => ond.Quantity);
                        sp.LuyKeXuat -= hangTra;

                        //info
                        sp.ProductId = product.ProductId;
                        sp.ProductCode = product.ProductCode;
                        var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == product.CustomerId);
                        sp.CustomerCode = customer.CustomerCode;
                        //sp.ToDate = monthly;
                        //if (importSx1 != null)
                        //{
                        //    sp.DayOfSX1 = importSx1.MaterialUseDate.ToString("dd/MM");
                        //}
                        //else if (monthly.DayOfWeek == DayOfWeek.Monday)
                        //    sp.DayOfSX1 = monthly.AddDays(-2).ToString("dd/MM");
                        //else
                        //    sp.DayOfSX1 = monthly.AddDays(-1).ToString("dd/MM");
                        //var smartProduction = vfi.SmartProductions.FirstOrDefault(sm => sm.ProductId == product.ProductId);
                        //sp.OnMachine = (smartProduction != null ? 1 : 0);
                        var productPeriodByIds =
                            allProductInventoryPeriodsByDate.Where(pip => pip.ProductId == product.ProductId).ToList();
                        //ton kho 
                        sp.SanXuat1 = sxKho1.Where(p => p.ProductId == product.ProductId).Sum(p => p.Quantity) -
                                      sxKho1Huy.Where(p => p.ProductId == product.ProductId).Sum(p => p.Quantity);
                        sp.TonKhoSX2CNC =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.Cnc)
                                               .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoSX2SX2 =
                            productPeriodByIds.Where(p => production2Ids.Contains(p.WarehouseId))
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoNhietLuyen =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.HeatTreatment)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoRungBong =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoGCN =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.WaitingPlating)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoNCU =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.Plating)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoNCUKiemTra =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.PlatingTest)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoQCA =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.QcA)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoQCB =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.QcB)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoCXL =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.Processing)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoCXL2 =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.Processing2)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.ReProcessing =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.ReProcessing)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoPP = ppInMonth.Where(p => p.ProductId == product.ProductId).Sum(p => p.Quantity);
                        sp.TonKhoTPA =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.Finish)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        sp.TonKhoTPB =
                            productPeriodByIds.Where(p => p.WarehouseId == MyUtilities.Warehouse.Return)
                                              .Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity);
                        // xuat ban
                        sp.XuatThanhPham = xuatTP.Where(p => p.ProductId == product.ProductId).Sum(p => p.Quantity);
                        // cong thuc tinh toan
                        //sp.TonTong = sp.TonKhoSX2CNC + sp.TonKhoSX2SX2 + sp.TonKhoNhietLuyen + sp.TonKhoRungBong +
                        //             sp.TonKhoGCN + sp.TonKhoNCU + sp.TonKhoQCA + sp.TonKhoQCB +
                        //             sp.TonKhoCXL + sp.TonKhoTPA;
                        sp.DuBaoSX = product.ForecastsQuality ?? 0;
                        sp.SLCanSX = (sp.DuBaoSX - sp.TonTong) > 0
                                         ? (sp.DuBaoSX - sp.TonTong)
                                         : 0;
                        if (sp.HienThi)
                            sp.Index = index++;
                        tonkho.Add(sp);
                    }
                    //}

                    return View(new GridModel(tonkho.OrderBy(c => c.CustomerCode).ThenBy(p => p.ProductCode)));
                }
            }
            catch (Exception exception) {
                throw new Exception(exception.Message);
            }
        }

        private void UpdateProductInventoryTotal(DateTime toDate, int i) {
            if (i < 2) {
                var lastDate = new DateTime(toDate.Year, toDate.Month, 1);
                lastDate = lastDate.AddDays(-1);
                using (var vfi = new tammaContext()) {
                    var allProductTotalByDate =
                        vfi.ProductTotalByMonths.Where(p => p.Month == lastDate.Month && p.Year == lastDate.Year)
                           .ToList();
                    // chua co gia tri
                    if (allProductTotalByDate.Count == 0) {
                        var allProductInventoryPeriodsByDate =
                            vfi.ProductInventoryPeriods.Where(
                                pip =>
                                pip.PeriodDate < lastDate
                                ).ToList();
                        // kho trong thang do + nam khong co gia tri
                        if (allProductInventoryPeriodsByDate.Count == 0) {
                        }
                        // co gia tri se dc luu lai
                        else {
                            var giaodichKho1 = (from p in allProductInventoryPeriodsByDate
                                                where
                                                    p.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Production1 &&
                                                    p.Transaction.WarehouseIssueId == null
                                                select p).ToList();
                            var giaodichKD = (from p in allProductInventoryPeriodsByDate
                                              where
                                                  p.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Business
                                              select p).ToList();
                            var productTotals = vfi.ProductTotalByMonths.ToList();

                            allProductInventoryPeriodsByDate =
                                allProductInventoryPeriodsByDate.Where(
                                    pip => pip.WarehouseId != 1 && pip.WarehouseId != 11).ToList();
                            //1 : SX1,2 : SX2/CNC,3 : NhietLuyen,4 : RungBong,5 : GCN,6 : NCU,7 : QCA,
                            //8 : CXL,9 : PP,10 : TPA,11 : KD,12 : SX2/SX2,13 : QCB,14 : TPB
                            foreach (var productInventory in allProductInventoryPeriodsByDate) {
                                var sp = productTotals.FirstOrDefault(s => s.ProductId == productInventory.ProductId);
                                if (sp == null) {
                                    sp = new ProductTotalByMonth();
                                    sp.ProductId = productInventory.ProductId;

                                    switch (productInventory.WarehouseId) {
                                        case 1:
                                            break;
                                        case 2:
                                            sp.KhoSX2_CNC =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 3:
                                            sp.KhoNhietLuyen =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 4:
                                            sp.KhoChoRungBong =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 5:
                                            sp.KhoChoGCN =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 6:
                                            sp.KhoNhaCungUng =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 7:
                                            sp.KhoQC_A =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 8:
                                            sp.ChoXuLy =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 9:
                                            sp.PhePham =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 10:
                                            sp.ThanhPham_A =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 11:
                                            break;
                                        case 12:
                                            sp.KhoSX2_SX2 =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 13:
                                            sp.KhoQC_B =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 14:
                                            sp.ThanhPham_B =
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                    }
                                    sp.Month = lastDate.Month;
                                    sp.Year = lastDate.Year;
                                    sp.LuyKeSanXuat = GiaiQuyetVanDe(giaodichKho1, productInventory.ProductId);
                                    sp.LuyKeXuat = GiaiQuyetVanDe(giaodichKD, productInventory.ProductId);
                                    productTotals.Add(sp);
                                }
                                else {
                                    switch (productInventory.WarehouseId) {
                                        case 1:
                                            break;
                                        case 2:
                                            sp.KhoSX2_CNC +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 3:
                                            sp.KhoNhietLuyen +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 4:
                                            sp.KhoChoRungBong +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 5:
                                            sp.KhoChoGCN +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 6:
                                            sp.KhoNhaCungUng +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 7:
                                            sp.KhoQC_A +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 8:
                                            sp.ChoXuLy +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 9:
                                            sp.PhePham +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 10:
                                            sp.ThanhPham_A +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 11:
                                            break;
                                        case 12:
                                            sp.KhoSX2_SX2 +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 13:
                                            sp.KhoQC_B +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                        case 14:
                                            sp.ThanhPham_B +=
                                                (productInventory.LastPeriodQuantity) -
                                                (productInventory.EarlyPeriodQuantity);
                                            break;
                                    }
                                }
                            }
                            vfi.ProductTotalByMonths.AddRange(productTotals);
                            vfi.SaveChanges();
                        }
                    }
                    else {
                        i++;
                        UpdateProductInventoryTotal(lastDate, i);
                    }
                }
            }
        }

        private double? GiaiQuyetVanDe(IEnumerable<Vfi.Models.ProductInventoryPeriod> list, int productId) {
            var xuat = (from p in list
                        where p.ProductId == productId
                        select p).ToList();
            if (xuat.FirstOrDefault() == null)
                return 0.0;
            if (xuat.Count() == 1)
                return xuat.FirstOrDefault().Quantity;
            return xuat.Sum(p => p.Quantity);
        }
        #endregion

        #region Rpt

        public ActionResult PrintNewAssignMaterial(string printDate, string factory) {
            var model = new List<SmartProductionModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var ci = new CultureInfo("vi-VN");
                    var date = string.IsNullOrWhiteSpace(printDate)
                                          ? DateTime.Today
                                          : Convert.ToDateTime(printDate, ci);
                    var useTemp = (from mud in vfi.MaterialUseDetails
                                   where mud.MaterialUseInShift.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                         mud.MaterialUseInShift.UsedDate < date
                                   select new {
                                       mud.MachineId,
                                       mud.MaterialInventory.MaterialId,
                                       mud.MaterialInvId,
                                       Quantity = mud.EditQuantity + mud.EditQuantity2
                                   }).ToList();
                    var checkDate = date.AddMonths(-1);
                    var lastDate = new DateTime();
                    var lastUse = (from mud in vfi.MaterialUseDetails
                                   where mud.MaterialUseInShift.Status != (byte)MyUtilities.Transaction.Status.Cancel &&
                                         mud.MaterialUseInShift.UsedDate < date &&
                                         mud.MaterialUseInShift.Type == 1
                                   orderby mud.MaterialUseInShift.UsedDate
                                   select mud).ToList();
                    if (lastUse.Any()) {
                        lastDate = lastUse.LastOrDefault().MaterialUseInShift.UsedDate;
                    }
                    lastUse = (from mud in vfi.MaterialUseDetails
                               where mud.MaterialUseInShift.Status != (byte)MyUtilities.Transaction.Status.Cancel &&
                                     mud.MaterialUseInShift.UsedDate == lastDate &&
                                          mud.MaterialUseInShift.Type == 1
                               select mud).ToList();
                    var lastDateString = lastDate.ToString("dd/MM");
                    var machines = (from m in vfi.Machines
                                    where m.Active && m.ProcessingType.Warehouse.IsProduction
                                    orderby m.MachineName
                                    select new {
                                        m.MachineId,
                                        m.MachineName
                                    }).ToList();
                    //if (!string.IsNullOrWhiteSpace(factory)) {
                    //    if (factory.Equals(MyUtilities.Machine.FactoryVF2)) {
                    //        machines = machines.Where(x => x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //    else {
                    //        machines = machines.Where(x => !x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //}
                    var onShelves = vfi.OnShelves.Where(x => x.InventoryDrawer.InventoryShelf.ClassifiedId == 1 && x.Active)
                                                .Select(x => new InventoryDrawerModel {
                                                    ColumnName = x.InventoryDrawer.ColumnName,
                                                    RowName = x.InventoryDrawer.RowName,
                                                    AdditionName = x.InventoryDrawer.AdditionName,
                                                    ShelfName = x.InventoryDrawer.InventoryShelf.ShelfName,
                                                    ReferenceInvId = x.ReferenceInvId
                                                }).ToList();
                    foreach (var machine in machines) {
                        var lastTrack =
                            (from t in vfi.TrackUpMachines
                             where
                                 t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                 t.MachineId == machine.MachineId &&
                            ((t.DeliveryDate != null ? t.DeliveryDate.Value <= date : t.StartDate <= date) ||
                             (t.StartDate <= date))
                             select new {
                                 t.MachineId,
                                 t.ProductId,
                                 t.Product.ProductCode,
                                 t.RealProductivity,
                                 t.RealRate,
                                 t.TrackUpMaterials,
                                 t.MaterialId,
                                 t.Material,
                                 t.WorkPiece,
                                 t.KnifeCut,
                                 Length = t.Product.Length ?? 0,
                                 Date = t.DeliveryDate != null ? t.DeliveryDate.Value : t.StartDate
                             })
                                .OrderByDescending(t => t.Date)
                                .FirstOrDefault();
                        //var lastTrack = MyUtilities.Machine.LastTrackUpMachine(machine.MachineId, null, null, date);
                        if (lastTrack != null) {
                            if (lastTrack.Date < checkDate) {
                                if (!lastTrack.TrackUpMaterials.Any()) continue;
                                if (!lastTrack.TrackUpMaterials.Any(tm => tm.ModifiedDate > checkDate.AddMonths(-2))) continue;
                            }
                            var entity = new SmartProductionModel {
                                MachineName = machine.MachineName,
                                MachineId = machine.MachineId,
                                ProductId = lastTrack.ProductId,
                                ProductCode = lastTrack.ProductCode,
                                UseDateString = date.ToString("dd/MM"),
                                UseDateString2 = lastDateString,
                                Productivity = lastTrack.RealProductivity,
                                ProductionRate = lastTrack.RealRate,
                                DiffProduction =
                                    MyUtilities.Product.GetMaterialRateInFactoryFullShiftTime(lastTrack.RealProductivity, lastTrack.RealRate),
                                MaterialName = lastTrack.Material.MaterialName,
                                OutDiameter = lastTrack.Material.OutDiameter,
                                InDiameter = lastTrack.Material.InDiameter,
                                Shape = lastTrack.Material.Shape,
                                DiameterType = lastTrack.Material.DiameterType,
                                ProductAlert = 0,
                                MaterialId = lastTrack.MaterialId.Value,
                                KnifeCut = lastTrack.KnifeCut,
                                WorkPiece = lastTrack.WorkPiece,
                                LimitColor = 1
                            };
                            if (lastTrack.TrackUpMaterials.Any()) {
                                var materiaInv = lastTrack.TrackUpMaterials.LastOrDefault().MaterialInventory;
                                entity.MaterialInventoryId = materiaInv.MaterialInventoryId;
                                entity.MaterialInventoryCode = materiaInv.Material.MaterialCode;
                                //entity.StoreCode = materiaInv.StoreCode;
                                entity.LotNumber = materiaInv.LotNumber;
                                entity.Length = materiaInv.Length / 1000;
                                entity.VendorCode = materiaInv.Vendor.VendorCode;
                                entity.MaterialInvTotal = lastTrack.TrackUpMaterials.LastOrDefault().MaterialInventory.TotalQty;

                                entity.ProductionRate = MyUtilities.Product.GetProductRate(entity.Length * 1000,
                                                                                           lastTrack.WorkPiece,
                                                                                           lastTrack.Length,
                                                                                           lastTrack.KnifeCut);

                                var materialInvs = vfi.MaterialInventories.Where(pi => pi.MaterialId == materiaInv.MaterialId && pi.TotalQty > 0);
                                if (materialInvs.Any()) {
                                    entity.MaterialAllInvTotal = materialInvs.Sum(mi => mi.TotalQty);
                                }

                                var onShelvesById = onShelves.Where(x => x.ReferenceInvId == materiaInv.MaterialInventoryId).ToList();
                                if (onShelvesById.Any()) {
                                    entity.StoreCode = string.Join("+", onShelvesById.Select(x => x.DrawerCode).Distinct().OrderBy(x => x));
                                }
                                else {
                                    entity.StoreCode = "Ngoài kệ";
                                }
                            }
                            else {
                                entity.Require = 0;
                                var materialInvs =
                                    vfi.MaterialInventories.Where(
                                        mi => mi.MaterialId == entity.MaterialId && Math.Round(mi.TotalQty) > 0).ToList();
                                if (materialInvs.Any()) {
                                    entity.MaterialAllInvTotal = materialInvs.Sum(mi => mi.TotalQty);

                                    var materialInvIds = materialInvs.Select(x => x.MaterialInventoryId).ToList();
                                    var onShelvesById = onShelves.Where(x => materialInvIds.Contains(x.ReferenceInvId)).ToList();
                                    if (onShelvesById.Any()) {
                                        entity.StoreCode = string.Join("+", onShelvesById.Select(x => x.DrawerCode).Distinct().OrderBy(x => x));
                                    }
                                }
                            }
                            entity.MaterialLimitQuantity =
                                MyUtilities.Product.GetMaterialRateInFactoryFullShiftTime(entity.Productivity, entity.ProductionRate);
                            if (lastTrack.Material.MaterialType.IdentityCode.Equals("A")) {
                                entity.IsChange = true;
                            }
                            var materialInvOnmachines =
                                vfi.MaterialInvOnMachines.Where(
                                    mim =>
                                    mim.MachineId == entity.MachineId &&
                                    mim.MaterialInventory.MaterialId == entity.MaterialId).Select(x => x.TotalQuantity).ToList();
                            if (materialInvOnmachines.Any())
                                entity.MaterialInvOnMachine = materialInvOnmachines.Sum(mim => mim);
                            var waitingUse =
                                useTemp.Where(
                                    mud =>
                                    mud.MachineId == entity.MachineId && mud.MaterialId == entity.MaterialId);
                            if (waitingUse.Any()) {
                                entity.MaterialInvOnMachine -= waitingUse.Sum(mud => mud.Quantity);
                            }
                            var lastUseBy =
                                lastUse.Where(
                                    mud =>
                                    mud.MachineId == entity.MachineId &&
                                    mud.MaterialInventory.MaterialId == entity.MaterialId);
                            if (lastUseBy.Any()) {
                                entity.DiffMaterial = lastUseBy.Sum(mud => mud.EditQuantity + mud.EditQuantity2);
                            }

                            entity.MaxAssign = MyUtilities.Function.Round5(entity.MaterialLimitQuantity * 2.5);
                            //if (entity.MaxAssign == 0)
                            //    entity.MaxAssign = 5;
                            //else
                            if (!string.IsNullOrWhiteSpace(entity.VendorCode))
                                entity.Require = Math.Round(entity.MaxAssign - entity.MaterialInvOnMachine);
                            if (entity.Require < 0)
                                entity.Require = 0;
                            if (entity.Require * 6 > entity.MaterialInvTotal) {
                                entity.MaterialAlert = 1;
                            }
                            model.Add(entity);
                        }
                        else {
                            var entity = new SmartProductionModel {
                                MachineName = machine.MachineName,
                                MachineId = machine.MachineId,
                                ProductId = 0,
                                ProductCode = "",
                                KnifeCut = 0,
                                WorkPiece = 0
                            };
                            model.Add(entity);
                        }
                    }
                    var groupSx = (from sx in model
                                   group sx by
                                       new {
                                           sx.MaterialInventoryId
                                       }
                                       into sxsx1
                                       where (sxsx1.Count() > 1)
                                       select new {
                                           sxsx1.Key.MaterialInventoryId,
                                           sxsx1.FirstOrDefault().MaterialInvTotal,
                                           Require = sxsx1.Sum(s => s.Require)
                                       }).ToList();
                    foreach (var sx in groupSx) {
                        if (sx.Require * 6 > sx.MaterialInvTotal) {
                            var entities =
                                model.Where(
                                    m => m.MaterialInventoryId == sx.MaterialInventoryId
                                         && m.MaterialAlert != 1);
                            foreach (var entity in entities) {
                                entity.MaterialAlert = 1;
                            }
                        }
                    }
                    var first = model.FirstOrDefault();
                    first.TotalCames = model.Where(m => !m.MachineName.Contains("CNC") && m.DiffMaterial > 0).Count();
                    first.TotalCnc = model.Where(m => m.MachineName.Contains("CNC") && m.DiffMaterial > 0).Count();

                    // no one update
                    //var materialIds = model.Select(m => m.MaterialId).Distinct().ToList();
                    //var productIds = model.Select(m => m.ProductId).Distinct().ToList();
                    //var materialLimitPlans = (from mlp in vfi.MaterialLimitPlans
                    //                          where materialIds.Contains(mlp.MaterialId) &&
                    //                                 productIds.Contains(mlp.ProductId) &&
                    //                                 mlp.IsLock &&
                    //                                 mlp.ApplyDate <= date
                    //                          orderby mlp.ApplyDate descending
                    //                          group mlp by
                    //                              new {
                    //                                  mlp.MaterialId,
                    //                                  mlp.ProductId
                    //                              }
                    //                              into gmlp
                    //                              select new {
                    //                                  gmlp.Key.MaterialId,
                    //                                  gmlp.Key.ProductId,
                    //                                  RawProductionWeight = gmlp.FirstOrDefault().RawProductionWeight,
                    //                                  Plan = gmlp.FirstOrDefault(),
                    //                              }).ToList();
                    //var fromDate = new DateTime(DateTime.Now.Year, 1, 1);
                    //var oldestLimit = materialLimitPlans.OrderBy(o => o.Plan.ApplyDate).FirstOrDefault();
                    //if (oldestLimit != null)
                    //    fromDate = oldestLimit.Plan.ApplyDate;
                    //var sx1Details = (from id in vfi.ImportFormSX1Detail
                    //                  where
                    //                      id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                    //                      id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                    //                      id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                    //                        .Transaction.Status ==
                    //                      (byte)MyUtilities.Transaction.Status.Approved &&
                    //                      id.ImportFormSX1.ImportDate >= fromDate &&
                    //                      materialIds.Contains(id.MaterialInventory.MaterialId) &&
                    //                      productIds.Contains(id.ProductId)
                    //                  select new {

                    //                      MaterialId = id.MaterialInventory.MaterialId,
                    //                      id.ProductId,
                    //                      MaterialUse = (id.MaterialUse1) + (id.MaterialUse2),
                    //                      id.ProductionRate,
                    //                      MaterialWeight = id.MaterialInventory.UnitWeight,
                    //                      Production = (id.Number1) + (id.Number2) +
                    //                                   (id.Processing1) + (id.Processing2) +
                    //                                   (id.DefectProduct1) + (id.DefectProduct2),
                    //                      id.ImportFormSX1.MaterialUseDate,
                    //                      id.ImportFormSX1.ImportDate,
                    //                  }).ToList();
                    //foreach (var groupPlan in materialLimitPlans) {
                    //    var limitPlan = groupPlan.Plan;
                    //    var entities = model.Where(m => m.ProductId == limitPlan.ProductId &&
                    //                                m.MaterialId == limitPlan.MaterialId);
                    //    var productions = sx1Details
                    //        .Where(sd => sd.MaterialId == limitPlan.MaterialId &&
                    //                        sd.ProductId == limitPlan.ProductId &&
                    //                        sd.ImportDate >= limitPlan.ApplyDate);
                    //    var productionQuantity = productions.Sum(p => p.Production) * groupPlan.RawProductionWeight / 1000;
                    //    var materialUsedQuantity = productions.Sum(p => p.MaterialUse * p.MaterialWeight);
                    //    var limitColor = 1;
                    //    if (limitPlan.ProductLimitQuantity > productionQuantity &&
                    //        limitPlan.MaterialLimitQuantity > materialUsedQuantity) {
                    //        // nothing happen
                    //        var maxUse = entities.Sum(e => e.MaterialLimitQuantity * 2);
                    //        var minLimitLeft = Math.Min(limitPlan.ProductLimitQuantity - productionQuantity, limitPlan.MaterialLimitQuantity - materialUsedQuantity);
                    //        var day = maxUse > 0 ? minLimitLeft / maxUse : 0;
                    //        if (day > 7) {
                    //            limitColor = 0;
                    //        }
                    //        else if (day > 5) {
                    //            limitColor = 3;
                    //        }
                    //        else if (day > 3) {
                    //            limitColor = 2;
                    //        }
                    //        else {

                    //        }
                    //    }
                    //    if (limitColor != 1) {
                    //        foreach (var entity in entities) {
                    //            entity.LimitColor = limitColor;
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex) {
                return PartialView(ex.Message);
            }
            return PartialView("PageNewAssignMaterial", model);
        }


        public ActionResult PrintMaterialUseForm(string printDate, string factory) {
            var model = new List<MaterialInvOnMachineModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var ci = new CultureInfo("vi-VN");
                    var date = Convert.ToDateTime(printDate, ci);

                    var materialUse = (from mud in vfi.MaterialUseDetails
                                       where mud.MaterialUseInShift.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                             mud.MaterialUseInShift.UsedDate < date
                                       select new {
                                           mud.MachineId,
                                           mud.MaterialInventory.MaterialId,
                                           mud.MaterialInvId,
                                           Quantity = mud.EditQuantity + mud.EditQuantity2
                                       }).ToList();
                    var sendBackMaterials = (from mud in vfi.MaterialUseDetails
                                             where
                                                 mud.MaterialUseInShift.Status ==
                                                 (byte)MyUtilities.Transaction.Status.Approved &&
                                                 mud.MaterialUseInShift.UsedDate == date &&
                                                 mud.MaterialUseInShift.Type == (int)MyUtilities.Material.UseType.SendBack
                                             select new {
                                                 mud.MachineId,
                                                 mud.MaterialInventory.MaterialId,
                                                 mud.MaterialInvId,
                                                 Quantity = mud.EditQuantity + mud.EditQuantity2
                                             }).ToList();
                    var exportMaterials = (from ed in vfi.ExportMaterialDetails
                                           where
                                               ed.ExportMaterial.Transaction.Status ==
                                               (byte)MyUtilities.Transaction.Status.Approved &&
                                               ed.ExportMaterial.ExportDate.Year == date.Year &&
                                               ed.ExportMaterial.ExportDate.Month == date.Month &&
                                               ed.ExportMaterial.ExportDate.Day == date.Day &&
                                               ed.MachineId != null
                                           select new {
                                               ed.MachineId,
                                               ed.Material.MaterialId,
                                               ed.MaterialInvId,
                                               ed.Quantity
                                           }).ToList();
                    var checkDate = date.AddMonths(-1);
                    var checkDate2 = date.AddMonths(-3);
                    var tracks = (from t in vfi.TrackUpMachines
                                  where
                                  t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                      //t.MachineId == 10 &&
                                  (t.DeliveryDate != null ? t.DeliveryDate.Value <= date : t.StartDate <= date) &&
                                  ((t.DeliveryDate != null ? t.DeliveryDate.Value >= checkDate : t.StartDate >= checkDate) ||
                                   t.TrackUpMaterials.Any(tm => tm.ModifiedDate > checkDate2))
                                  select new {
                                      t.MachineId,
                                      t.ProductId,
                                      t.Product.ProductCode,
                                      t.RealProductivity,
                                      t.RealRate,
                                      t.TrackUpMaterials,
                                      t.MaterialId,
                                      t.Material,
                                      t.WorkPiece,
                                      t.KnifeCut,
                                      Length = t.Product.Length ?? 0,
                                      Date = t.DeliveryDate != null ? t.DeliveryDate.Value : t.StartDate
                                  })
                                  .OrderByDescending(t => t.Date).ToList();
                    var machineIds = tracks.Select(t => t.MachineId).Distinct().ToList();
                    //.FirstOrDefault();
                    //if (lastTrack != null)
                    //{
                    //    if (lastTrack.Date < checkDate)
                    //    {
                    //        if (!lastTrack.TrackUpMaterials.Any()) continue;
                    //        if (!lastTrack.TrackUpMaterials.Any(tm => tm.ModifiedDate > checkDate)) continue;
                    //    }
                    //}
                    var machines = (from m in vfi.Machines
                                    where m.Active && m.MachineName.Contains("C") &&
                                          machineIds.Contains(m.MachineId)
                                    orderby m.MachineName
                                    //&& m.MachineId == 55
                                    select new {
                                        m.MachineId,
                                        m.MachineName
                                    }).ToList();

                    //if (!string.IsNullOrWhiteSpace(factory)) {
                    //    if (factory.Equals(MyUtilities.Machine.FactoryVF2)) {
                    //        machines = machines.Where(x => x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //    else {
                    //        machines = machines.Where(x => !x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //}
                    foreach (var machine in machines) {
                        var groupInvs = (from mim in vfi.MaterialInvOnMachines
                                         where mim.MachineId == machine.MachineId &&
                                               Math.Round(mim.TotalQuantity) > 0
                                         group mim by
                                             new {
                                                 mim.MachineId,
                                                 mim.MaterialInventory.MaterialId,
                                                 mim.MaterialInventory.Material,
                                             }
                                             into gmim
                                             select new {
                                                 gmim.Key.MachineId,
                                                 gmim.Key.Material,
                                                 gmim.Key.MaterialId,
                                                 TotalQuantity = gmim.Sum(s => s.TotalQuantity)
                                             }).ToList();
                        //var allInvsOnMachine = from mim in vfi.MaterialInvOnMachines
                        //                       select new
                        //                           {
                        //                           };
                        foreach (var groupInv in groupInvs) {
                            var entity = new MaterialInvOnMachineModel {
                                MachineId = machine.MachineId,
                                MachineName = machine.MachineName,
                                MaterialId = groupInv.MaterialId,
                                MaterialName = groupInv.Material.MaterialName,
                                OutDiameter = groupInv.Material.OutDiameter,
                                InDiameter = groupInv.Material.InDiameter,
                                Shape = groupInv.Material.Shape,
                                DiameterType = groupInv.Material.DiameterType,
                                DateString = printDate,
                                //ProductId = lastTrack.ProductId,
                                //ProductCode = lastTrack.ProductCode,
                                EarlyQuantity = groupInv.TotalQuantity,
                            };
                            var waitingUse =
                                materialUse.Where(
                                    mud =>
                                    mud.MachineId == entity.MachineId && mud.MaterialId == entity.MaterialId);
                            if (waitingUse.Any()) {
                                entity.EarlyQuantity -= waitingUse.Sum(mud => mud.Quantity);
                            }
                            var lastTrack =
                                tracks.FirstOrDefault(
                                    t => t.MachineId == entity.MachineId && t.MaterialId == entity.MaterialId);
                            if (lastTrack != null) {
                                entity.ProductId = lastTrack.ProductId;
                                entity.ProductCode = lastTrack.ProductCode;
                                var rate = lastTrack.RealRate;
                                var lastMaterialInv =
                                    lastTrack.TrackUpMaterials.OrderByDescending(tm => tm.ModifiedDate)
                                        .ToList()
                                        .FirstOrDefault();
                                if (lastMaterialInv != null) {
                                    entity.MaterialInvId = lastMaterialInv.MaterialInvId;
                                    entity.Length = lastMaterialInv.MaterialInventory.Length / 1000;
                                    entity.LotNumber = lastMaterialInv.MaterialInventory.LotNumber;
                                    entity.VendorCode = lastMaterialInv.MaterialInventory.Vendor.VendorCode;
                                    //if (lastTrack.Length != 0)
                                    rate = MyUtilities.Product.GetProductRate(lastMaterialInv.MaterialInventory.Length,
                                                                              lastTrack.WorkPiece,
                                                                              lastTrack.Length,
                                                                              lastTrack.KnifeCut);
                                }
                                entity.WaitingNumber = MyUtilities.Product.GetMaterialRateInFactoryFullShiftTime(
                                                                 lastTrack.RealProductivity,
                                                                 rate);
                            }
                            var exportById =
                                exportMaterials.Where(
                                    ed => ed.MachineId == entity.MachineId && ed.MaterialId == entity.MaterialId);
                            if (exportById.Any()) {
                                entity.AssignQuantity = exportById.Sum(ed => ed.Quantity);
                            }
                            waitingUse = sendBackMaterials.Where(
                                mud =>
                                mud.MachineId == entity.MachineId && mud.MaterialId == entity.MaterialId);
                            if (waitingUse.Any()) {
                                entity.SendBack = waitingUse.Sum(mud => mud.Quantity);
                            }
                            entity.EarlyQuantity = Math.Round(entity.EarlyQuantity, 1);
                            model.Add(entity);
                        }
                    }
                }

                var groups = (from m in model
                              group m by
                                  new {
                                      m.MachineId,
                                      //m.MaterialId,
                                  }
                                  into gm
                                  where gm.Count() > 1
                                  select new {
                                      gm.Key.MachineId,
                                      //gm.Key.MaterialId
                                  }).ToList();
                foreach (var group in groups) {
                    var m = model.FirstOrDefault(
                        g =>
                        g.MachineId == group.MachineId &&
                        g.EarlyQuantity == 0);
                    if (m != null)
                        model.Remove(m);
                }

            }
            catch (Exception ex) {
                return PartialView(ex.Message);
            }
            return PartialView("PageMaterialUseForm", model);
        }

        public ActionResult PrintProduction(string printDate, string factory) {
            var model = new List<SmartProductionModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var ci = new CultureInfo("vi-VN");
                    var date = Convert.ToDateTime(printDate, ci);
                    var useDetails = (from md in vfi.MaterialUseDetails
                                      where
                                          md.MaterialUseInShift.Status != (byte)MyUtilities.Transaction.Status.Cancel &&
                                          md.MaterialUseInShift.UsedDate.Day == date.Day &&
                                          md.MaterialUseInShift.UsedDate.Month == date.Month &&
                                          md.MaterialUseInShift.UsedDate.Year == date.Year &&
                                          md.MaterialUseInShift.Type == 1 &&
                                          md.EditQuantity + md.EditQuantity2 > 0
                                      select md).ToList();
                    var shift1Name = "";
                    try {
                        shift1Name = useDetails.FirstOrDefault(id => id.MaterialUseInShift.Shift1.Trim().Length > 0)
                                               .MaterialUseInShift.Shift1;
                    }
                    catch { }
                    var shift2Name = "";
                    try {
                        shift2Name = useDetails.FirstOrDefault(id => id.MaterialUseInShift.Shift2.Trim().Length > 0)
                            .MaterialUseInShift.Shift2;
                    }
                    catch { }
                    var loops = (from md in useDetails
                                 orderby md.Machine.MachineName
                                 select new {
                                     md.MachineId,
                                     md.Machine.MachineName,
                                     md.MaterialInvId,
                                     md.MaterialInventory
                                 }).Distinct().ToList();
                    //if (!string.IsNullOrWhiteSpace(factory)) {
                    //    if (factory.Equals(MyUtilities.Machine.FactoryVF2)) {
                    //        loops = loops.Where(x => x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //    else {
                    //        loops = loops.Where(x => !x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //}
                    var sameProduct = 1;
                    var useDetailIds = useDetails.Select(ud => ud.DetailId).Distinct().ToList();
                    var importDetails = (from id in vfi.ImportFormSX1Detail
                                         where id.ImportFormSX1.Status != (byte)MyUtilities.Transaction.Status.Cancel &&
                                         id.UseDetailId != null &&
                                         useDetailIds.Contains(id.UseDetailId.Value)
                                         select new {
                                             id.ProductId,
                                             id.MachineId,
                                             id.MaterialInvId,
                                             // shift 1 info
                                             id.Shift1,
                                             id.MaterialUse1,
                                             id.Number1,
                                             id.Processing1,
                                             id.DefectProduct1,
                                             ProductionMaterial1 = id.ProductionRate > 0 ?
                                     Math.Round((id.Number1 + id.Processing1 + id.DefectProduct1) / id.ProductionRate, 2) : 0,
                                             // shift 2 info
                                             id.Shift2,
                                             id.MaterialUse2,
                                             id.Number2,
                                             id.Processing2,
                                             id.DefectProduct2,
                                             ProductionMaterial2 = id.ProductionRate > 0 ?
                                             Math.Round((id.Number2 + id.Processing2 + id.DefectProduct2) / id.ProductionRate, 2) : 0,
                                         }).ToList();

                    foreach (var detail in loops) {
                        var entity = new SmartProductionModel {
                            MachineId = detail.MachineId,
                            MachineName = detail.MachineName,
                            MaterialInventoryId = detail.MaterialInvId,
                            MaterialId = detail.MaterialInventory.MaterialId,
                            UseDateString = printDate,
                            Shift1Name = shift1Name,
                            Shift2Name = shift2Name,
                            ProductAlert = 0,
                        };
                        var useDetailsById =
                            useDetails.Where(
                                md => md.MachineId == entity.MachineId &&
                                    md.MaterialInvId == entity.MaterialInventoryId).ToList();
                        entity.MaterialUse1 = useDetailsById.Sum(md => md.EditQuantity);
                        entity.MaterialUse2 = useDetailsById.Sum(md => md.EditQuantity2);
                        var tracks =
                            vfi.TrackUpMachines.Where(
                                t =>
                                t.MachineId == entity.MachineId && t.MaterialId == entity.MaterialId &&
                                t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                t.DeliveryDate <= date)
                               .OrderBy(t => t.DeliveryDate).ToList();
                        if (tracks.Any()) {
                            var lastTrack = tracks.LastOrDefault();
                            entity.ProductId = lastTrack.ProductId;
                            entity.ProductCode = lastTrack.Product.ProductCode;
                            entity.ProductWeight = lastTrack.Product.ProductionWeight ?? 0;
                            entity.Length = lastTrack.Product.Length ?? 0;
                            entity.Productivity = lastTrack.RealProductivity;
                            //if (entity.Length != 0)
                            entity.ProductionRate =
                                MyUtilities.Product.GetProductRate(detail.MaterialInventory.Length,
                                                                   lastTrack.WorkPiece,
                                                                   entity.Length,
                                                                   lastTrack.KnifeCut);
                            //else
                            //    entity.ProductionRate = lastTrack.RealRate;

                            var lastProduction =
                                vfi.ImportFormSX1Detail.Where(
                                    id => id.MachineId == entity.MachineId && id.ProductId == entity.ProductId
                                        && id.ImportFormSX1.MaterialUseDate.Day == date.Day
                                        && id.ImportFormSX1.MaterialUseDate.Month == date.Month
                                        && id.ImportFormSX1.MaterialUseDate.Year == date.Year)
                                   .ToList()
                                   .LastOrDefault();
                            if (lastProduction != null) {
                                entity.ProductWeight = lastProduction.ProductWeight;
                            }
                            entity.QuantityDiff1 = entity.MaterialUse1 * entity.ProductionRate * entity.ProductWeight;
                            entity.QuantityDiff2 = entity.MaterialUse2 * entity.ProductionRate * entity.ProductWeight;
                        }

                        if (model.Any(m => m.ProductId == entity.ProductId && m.MachineId != entity.MachineId)) {
                            var first =
                                model.FirstOrDefault(
                                    m => m.ProductId == entity.ProductId && m.MachineId != entity.MachineId);
                            if (first.ProductAlert == 0) {
                                first.ProductAlert = sameProduct;
                                sameProduct++;
                            }
                            entity.ProductAlert = first.ProductAlert;
                        }

                        var nextProcess =
                            vfi.ProductionProcessByMachines
                                .Where(p => p.ProductId == entity.ProductId &&
                                            p.MachineId == entity.MachineId &&
                                            p.WarehouseId != MyUtilities.Warehouse.Production1 &&
                                            p.Active)
                                .OrderBy(p => p.ProcessIndex)
                                .FirstOrDefault();
                        if (nextProcess != null) {
                            entity.WarehouseExportId = nextProcess.WarehouseId;
                            entity.WarehouseExportName = nextProcess.Warehouse.ShortName;
                        }
                        else {
                            var productionProcess =
                                vfi.ProductionProcesses.Where(pp => pp.ProductId == entity.ProductId &&
                                                                    pp.IsNecessary &&
                                                                    pp.WarehouseId !=
                                                                    MyUtilities.Warehouse.Production1)
                                    .OrderBy(pp => pp.ProcessIndex)
                                    .FirstOrDefault();
                            if (productionProcess != null) {
                                entity.WarehouseExportId = productionProcess.WarehouseId;
                                entity.WarehouseExportName = productionProcess.Warehouse.ShortName;
                            }
                        }
                        var isReplace = false;
                        if (importDetails.Any()) {
                            //if (!string.IsNullOrWhiteSpace(entity.ProductCode)) {
                            //    var importDetailsById = importDetails.Where(id => id.ProductId == entity.ProductId &&
                            //        id.MachineId == entity.MachineId &&
                            //        id.MaterialInvId == entity.MaterialInventoryId);
                            //    if (importDetailsById.Any()) {
                            //        var importDetailsByIdShift = importDetailsById.Where(id => id.Shift1.Equals(entity.Shift1Name)).ToList();
                            //        entity.QuantityExport1 = importDetailsByIdShift.Sum(id => id.Number1) * entity.ProductWeight;
                            //        entity.QuantityPending1 = importDetailsByIdShift.Sum(id => id.Processing1) * entity.ProductWeight;
                            //        entity.QuantityDefect1 = importDetailsByIdShift.Sum(id => id.DefectProduct1) * entity.ProductWeight;
                            //        entity.DiffMaterial1 = importDetailsByIdShift.Sum(id => id.ProductionMaterial1) - entity.MaterialUse1;

                            //        importDetailsByIdShift = importDetailsById.Where(id => id.Shift2.Equals(entity.Shift2Name)).ToList();
                            //        entity.QuantityExport2 = importDetailsByIdShift.Sum(id => id.Number2) * entity.ProductWeight;
                            //        entity.QuantityPending2 = importDetailsByIdShift.Sum(id => id.Processing2) * entity.ProductWeight;
                            //        entity.QuantityDefect2 = importDetailsByIdShift.Sum(id => id.DefectProduct2) * entity.ProductWeight;
                            //        entity.DiffMaterial2 = importDetailsByIdShift.Sum(id => id.ProductionMaterial2) - entity.MaterialUse2;
                            //    }
                            //}
                            //else {
                            var importDetailsById = importDetails.Where(id => id.MachineId == entity.MachineId &&
                                id.MaterialInvId == entity.MaterialInventoryId);
                            if (importDetailsById.Any()) {
                                isReplace = true; // ?
                                var productIds = importDetailsById.Select(id => id.ProductId).Distinct().ToList();
                                foreach (var productId in productIds) {
                                    var newEntity = entity;
                                    var importDetailsByIdShift = importDetailsById.Where(id => id.Shift1.Equals(newEntity.Shift1Name) &&
                                        id.ProductId == productId).ToList();
                                    newEntity.QuantityExport1 = importDetailsByIdShift.Sum(id => id.Number1) * newEntity.ProductWeight;
                                    newEntity.QuantityPending1 = importDetailsByIdShift.Sum(id => id.Processing1) * newEntity.ProductWeight;
                                    newEntity.QuantityDefect1 = importDetailsByIdShift.Sum(id => id.DefectProduct1) * newEntity.ProductWeight;
                                    newEntity.DiffMaterial1 = importDetailsByIdShift.Sum(id => id.ProductionMaterial1) - newEntity.MaterialUse1;

                                    importDetailsByIdShift = importDetailsById.Where(id => id.Shift2.Equals(newEntity.Shift2Name) &&
                                        id.ProductId == productId).ToList();
                                    newEntity.QuantityExport2 = importDetailsByIdShift.Sum(id => id.Number2) * newEntity.ProductWeight;
                                    newEntity.QuantityPending2 = importDetailsByIdShift.Sum(id => id.Processing2) * newEntity.ProductWeight;
                                    newEntity.QuantityDefect2 = importDetailsByIdShift.Sum(id => id.DefectProduct2) * newEntity.ProductWeight;
                                    newEntity.DiffMaterial2 = importDetailsByIdShift.Sum(id => id.ProductionMaterial2) - newEntity.MaterialUse2;
                                    model.Add(newEntity);
                                }
                                //}
                            }
                            else {
                                entity.DiffMaterial1 = -entity.MaterialUse1;
                                entity.DiffMaterial2 = -entity.MaterialUse2;
                            }

                        }
                        if (!isReplace)
                            model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                return PartialView(ex.Message);
            }
            model = model.OrderBy(m => m.MachineName).ToList();
            return PartialView("PagePrintProduction", model);
        }

        public ActionResult PrintProduction2(string printDate, string factory) {
            var model = new List<SmartProductionModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var ci = new CultureInfo("vi-VN");
                    var date = Convert.ToDateTime(printDate, ci);
                    var useDetails = from md in vfi.MaterialUseDetails
                                     where
                                         md.MaterialUseInShift.Status != (byte)MyUtilities.Transaction.Status.Cancel &&
                                         md.MaterialUseInShift.UsedDate == date &&
                                         md.MaterialUseInShift.Type == 1 &&
                                         md.EditQuantity + md.EditQuantity2 > 0
                                     select md;
                    var shift1Name = "";
                    try {
                        shift1Name = useDetails.FirstOrDefault(id => id.MaterialUseInShift.Shift1.Trim().Length > 0)
                                               .MaterialUseInShift.Shift1;
                    }
                    catch { }
                    var shift2Name = "";
                    try {
                        shift2Name = useDetails.FirstOrDefault(id => id.MaterialUseInShift.Shift2.Trim().Length > 0)
                            .MaterialUseInShift.Shift2;
                    }
                    catch { }
                    var loops = (from md in useDetails
                                 select new {
                                     md.MachineId,
                                     md.Machine.MachineName,
                                     md.MaterialInvId,
                                     md.MaterialInventory
                                 }).Distinct().ToList();
                    //if (!string.IsNullOrWhiteSpace(factory)) {
                    //    if (factory.Equals(MyUtilities.Machine.FactoryVF2)) {
                    //        loops = loops.Where(x => x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //    else {
                    //        loops = loops.Where(x => !x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //}
                    var sameProduct = 1;
                    foreach (var detail in useDetails) {
                        var entity = new SmartProductionModel {
                            MachineId = detail.MachineId,
                            MachineName = detail.Machine.MachineName,
                            MaterialInventoryId = detail.MaterialInvId,
                            MaterialId = detail.MaterialInventory.MaterialId,
                            UseDateString = printDate,
                            Shift1Name = shift1Name,
                            Shift2Name = shift2Name,
                            ProductAlert = 0,
                            MaterialUse1 = detail.EditQuantity,
                            MaterialUse2 = detail.EditQuantity2,
                            MaterialCode = MyUtilities.Material.GetMaterialInvDesignNo(detail.MaterialInventory),
                            SmartId = detail.DetailId,
                        };
                        //var useDetailsById =
                        //    useDetails.Where(
                        //        md => md.MachineId == entity.MachineId && md.MaterialInvId == entity.MaterialInventoryId).ToList();
                        //entity.MaterialUse1 = useDetailsById.Sum(md => md.EditQuantity);
                        //entity.MaterialUse2 = useDetailsById.Sum(md => md.EditQuantity2);
                        var tracks =
                            vfi.TrackUpMachines.Where(
                                t =>
                                t.MachineId == entity.MachineId && t.MaterialId == entity.MaterialId &&
                                t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                t.DeliveryDate <= date)
                               .OrderBy(t => t.DeliveryDate).ToList();
                        if (tracks.Any()) {
                            var lastTrack = tracks.LastOrDefault();
                            entity.ProductId = lastTrack.ProductId;
                            entity.ProductCode = lastTrack.Product.ProductCode;
                            entity.Length = lastTrack.Product.Length ?? 0;
                            entity.Productivity = lastTrack.RealProductivity;
                            //if (entity.Length != 0)
                            entity.ProductionRate =
                                MyUtilities.Product.GetProductRate(detail.MaterialInventory.Length,
                                                                   lastTrack.WorkPiece,
                                                                   entity.Length,
                                                                   lastTrack.KnifeCut);
                            //else
                            //    entity.ProductionRate = lastTrack.RealRate;

                            var lastProduction =
                                vfi.ImportFormSX1Detail.Where(
                                    id => id.MachineId == entity.MachineId && id.ProductId == entity.ProductId)
                                   .ToList()
                                   .LastOrDefault();
                            if (lastProduction != null) {
                                entity.ProductWeight = lastProduction.ProductWeight;
                            }
                            entity.QuantityDiff1 = entity.MaterialUse1 * entity.ProductionRate * entity.ProductWeight;
                            entity.QuantityDiff2 = entity.MaterialUse2 * entity.ProductionRate * entity.ProductWeight;
                        }

                        if (model.Any(m => m.ProductId == entity.ProductId && m.MachineId != entity.MachineId)) {
                            var first =
                                model.FirstOrDefault(
                                    m => m.ProductId == entity.ProductId && m.MachineId != entity.MachineId);
                            if (first.ProductAlert == 0) {
                                first.ProductAlert = sameProduct;
                                sameProduct++;
                            }
                            entity.ProductAlert = first.ProductAlert;
                        }

                        var nextProcess =
                            vfi.ProductionProcessByMachines
                                .Where(p => p.ProductId == entity.ProductId &&
                                            p.MachineId == entity.MachineId &&
                                            p.WarehouseId != MyUtilities.Warehouse.Production1 &&
                                            p.Active)
                                .OrderBy(p => p.ProcessIndex)
                                .FirstOrDefault();
                        if (nextProcess != null) {
                            entity.WarehouseExportId = nextProcess.WarehouseId;
                            entity.WarehouseExportName = nextProcess.Warehouse.ShortName;
                        }
                        else {
                            var productionProcess =
                                vfi.ProductionProcesses.Where(pp => pp.ProductId == entity.ProductId &&
                                                                    pp.IsNecessary &&
                                                                    pp.WarehouseId !=
                                                                    MyUtilities.Warehouse.Production1)
                                    .OrderBy(pp => pp.ProcessIndex)
                                    .FirstOrDefault();
                            if (productionProcess != null) {
                                entity.WarehouseExportId = productionProcess.WarehouseId;
                                entity.WarehouseExportName = productionProcess.Warehouse.ShortName;
                            }
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                return PartialView(ex.Message);
            }
            return PartialView("PagePrintProduction2", model.OrderBy(m => m.MachineName).ToList());
        }

        public ActionResult PrintMaterialUseFormByDate(string fromDate, string toDate, string factory) {
            var model = new List<MaterialInvOnMachineModel>();
            try {
                model = GetMaterialInvOnMachine(fromDate, toDate, factory);
            }
            catch (Exception ex) {
                return PartialView(ex.Message);
            }
            return PartialView("PageMaterialUseForm", model);
        }

        #endregion

        #region Tâm ma


        [HttpPost]
        public ActionResult PrintTransactionForm(int transactionId) {
            var model = new List<TransactionDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                    //if (transaction.WarehouseIssueId != null && transaction.Warehouse.CanWeighing) {
                    //    return PrintTransactionFormGroup(transactionId);
                    //}
                    var info = new WorkGroupInfo();
                    var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.Active);
                    if (workgroup != null) {
                        info = new WorkGroupInfo {
                            Logo = workgroup.ImagePath + "/Logo/" + workgroup.LogoImage,
                            //CompanyFullName = workgroup.CompanyFullName,
                            //CompanyShortName = workgroup.CompanyShortName,
                            //Address = workgroup.Address,
                            //TelNumber = "Tel : " + workgroup.TelNumber,
                            //FaxNumber = "Fax : " + workgroup.FaxNumber,
                            //Email = "Email: " + workgroup.Email,
                            //Website = "Website: " + workgroup.Website
                        };
                    }
                    var list = GetTransactionProductDetailByTransactionId(transactionId);
                    var isPurchase = false;
                    foreach (var entity in list) {
                        entity.WarehouseIssueName = transaction.WarehouseIssueId != null
                                                  ? transaction.Warehouse.WarehouseName
                                                  : "";
                        entity.WarehouseReceiptName = transaction.WarehouseReceiptId != null
                                                  ? transaction.Warehouse1.WarehouseName
                                                  : "";
                        entity.WarehouseReceiptName = transaction.Warehouse1.WarehouseName;
                        entity.Status = transaction.Status;
                        entity.PeriodDate = transaction.CreatedDate;
                        entity.ModifiedDate = transaction.ModifiedDate;
                        entity.ModifiedUser = transaction.ModifiedUser;
                        entity.TransactionCode = transaction.TransactionCode;
                        entity.QuantityKg /= 1000;
                        entity.Info = info;
                        isPurchase = entity.PoDetailId > 0;
                    }
                    model = list;
                    if (isPurchase) {
                        return PartialView("PageImportProductForm", model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ToList());
                    }
                    return PartialView("PageTransactionForm", model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ToList());
                }
            }
            catch (Exception exception) {
                return PartialView("PageTransactionForm", exception.Message);
            }
            //return PartialView("PageTransactionForm", model.ToList());
        }

        [HttpPost]
        public ActionResult PrintTransactionFormGroup(int transactionId) {
            var model = new List<TransactionProductModel>();
            try {
                var list = GetTransactionProductDetailByTransactionId(transactionId);
                var productIds = list.Select(x => x.ReferenceId.Value).Distinct().ToList();
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                    var info = new WorkGroupInfo();
                    var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.Active);
                    if (workgroup != null) {
                        info = new WorkGroupInfo {
                            Logo = workgroup.ImagePath + "/Logo/" + workgroup.LogoImage,
                            CompanyFullName = workgroup.CompanyFullName,
                            CompanyShortName = workgroup.CompanyShortName,
                            Address = workgroup.Address,
                            TelNumber = "Tel : " + workgroup.TelNumber,
                            FaxNumber = "Fax : " + workgroup.FaxNumber,
                            Email = "Email: " + workgroup.Email,
                            Website = "Website: " + workgroup.Website
                        };
                    }
                    foreach (var productId in productIds) {
                        var details = list.Where(x => x.ReferenceId == productId).ToList();
                        if (!details.Any()) continue;
                        var detail = details.FirstOrDefault();
                        details.ForEach(x => x.QuantityKg = x.QuantityKg / 1000);
                        var lots = details.Where(x => !string.IsNullOrEmpty(x.LotNumber)).Select(x => x.LotNumber).Distinct().ToArray();
                        var entity = new TransactionProductModel() {
                            CustomerCode = detail.CustomerCode,
                            ProductId = productId,
                            ProductCode = detail.ProductCode,
                            WarehouseIssueName = transaction.WarehouseIssueId != null
                                                      ? transaction.Warehouse.WarehouseName
                                                      : "",
                            WarehouseReceiptName = transaction.WarehouseReceiptId != null
                                                      ? transaction.Warehouse1.WarehouseName
                                                      : "",
                            Status = transaction.Status,
                            PeriodDate = transaction.CreatedDate,
                            ModifiedDate = transaction.ModifiedDate,
                            ModifiedUser = transaction.ModifiedUser,
                            TransactionCode = transaction.TransactionCode,
                            ProductWeight = detail.UnitWeight,
                            Quantity = details.Sum(x => x.Quantity.Value),
                            QuantityKg = details.Sum(x => x.QuantityKg.Value),
                            Note = String.Join(", ", lots),
                            //Details = details
                            Info = info
                        };
                        model.Add(entity);
                    }
                    //model = list;
                }
            }
            catch (Exception exception) {
                return PartialView("PageTransactionFormGroup", exception.Message);
            }
            return PartialView("PageTransactionFormGroup", model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ToList());
        }


        List<TransactionDetailModel> GetTransactionProductDetailByTransactionId(long transactionId) {
            var model = new List<TransactionDetailModel>();
            var invManagerLv2 = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                MyUtilities.UserRole.InvManagementLv2);
            using (var vfi = new tammaContext()) {
                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                PurchaseOrder po = null;
                if (transaction.PoId > 0) {
                    po = vfi.PurchaseOrders.FirstOrDefault(x => x.PurchaseOrderId == transaction.PoId);
                }
                var transactionDetails = (from x in vfi.TransactionDetails
                                          where x.TransactionId == transactionId
                                          select new {
                                              x.ReferenceId,
                                              x.TransactionDetailId,
                                              x.LotNumber,
                                              x.Product.DesignNo,
                                              x.Product.ProductCode,
                                              x.Product.Customer.CustomerCode,
                                              x.Product.DrawingFinish,
                                              x.Quantity,
                                              x.PoDetailId,
                                              x.Note,
                                              x.ProductInvId,
                                              ProductionMaterial = x.Product.ProductionMaterials.FirstOrDefault(y => y.Active)

                                          });
                var productIds = transactionDetails.Select(td => td.ReferenceId).Distinct().ToList();
                //var productWeights = from pw in vfi.Products
                //                     where productIds.Contains(pw.ProductId)
                //                     select new {
                //                         pw.ProductId,
                //                         pw.ProductionWeight,
                //                         pw.CncWeight,
                //                         pw.Production2Weight,
                //                         pw.HeatTreatmentWeight,
                //                         pw.SurfaceTreatmentWeight,
                //                         pw.WaitingPlatingWeight,
                //                         pw.PlatingWeight,
                //                         pw.QcWeight,
                //                         //pw.DrawingFinish,
                //                     };
                var exportTpDetails = (from ed in vfi.ExportFormTP_KDDetail
                                       where ed.ExportFormTP_KD.TransactionCode.Equals(transaction.TransactionCode)
                                       select ed).ToList();
                var importSx1 =
                    vfi.ImportFormSX1.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));

                foreach (var transactionDetail in transactionDetails) {
                    var entity = new TransactionDetailModel {
                        TransactionDetailId = transactionDetail.TransactionDetailId,
                        LotNumber = transactionDetail.LotNumber,
                        ReferenceId = transactionDetail.ReferenceId,
                        UnitWeight =
                            MyUtilities.Product.GetProductInvWeight(transactionDetail.ReferenceId.Value,
                                transaction.WarehouseIssueId ?? transaction.WarehouseReceiptId.Value),
                        ProductImg = transactionDetail.DrawingFinish,
                        ProductCode = transactionDetail.ProductCode,
                        CustomerCode = transactionDetail.CustomerCode,
                        Quantity = transactionDetail.Quantity,
                        Note = transactionDetail.Note,
                        IsManagerLv2 = invManagerLv2,
                        PoDetailId = transactionDetail.PoDetailId ?? 0,
                        DesignCode = transactionDetail.DesignNo,
                        //VendorName = transactionDetail.VendorId
                    };
                    if (po != null) {
                        entity.VendorName = po.Vendor.VendorName;
                        entity.PoNumber = po.RevisionNumber;
                        var detail = po.PurchaseOrderDetails.FirstOrDefault(x => transactionDetail.PoDetailId == x.PurchaseOrderDetailId);
                        if (detail != null) {
                            entity.UnitMeasure = detail.Unit;
                            entity.UnitPrice = detail.UnitPrice;
                            entity.Price = entity.UnitPrice * entity.Quantity;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(entity.ProductImg))
                        entity.ProductImg = "askquestion.jpg";
                    if (transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Business) {
                        entity.AvailableQuantity = 0;
                    }
                    else {
                        var availableQuality =
                            vfi.ProductInventories.FirstOrDefault(
                                pi =>
                                pi.ProductInventoryId == transactionDetail.ProductInvId);
                        entity.AvailableQuantity = availableQuality != null ? availableQuality.TotalQty : 0;
                    }
                    if ((transaction.WarehouseIssueId == MyUtilities.Warehouse.Plating ||
                     transaction.WarehouseIssueId == MyUtilities.Warehouse.PlatingTest) &&
                         transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcB) {
                        var import =
                            vfi.ImportNCU_QCB.FirstOrDefault(
                                id =>
                                id.TransactionId == transaction.TransactionId);
                        if (import != null) {
                            entity.Note = import.PlatingForm.PlatingFormNumber;
                        }
                    }
                    if (transaction.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating &&
                        (transaction.WarehouseReceiptId == MyUtilities.Warehouse.PlatingTest ||
                            transaction.WarehouseReceiptId == MyUtilities.Warehouse.Plating)) {
                        var export =
                            vfi.ExportGCN_NCU.FirstOrDefault(
                                id =>
                                id.TransactionId == transaction.TransactionId);
                        if (export != null) {
                            entity.Note = export.PlatingForm.PlatingFormNumber;
                        }
                    }
                    //if (transaction.WarehouseReceiptId != null && transaction.Warehouse1.IsReprocessing) {
                    //    if (transactionDetail.ErrorId != null)
                    //        entity.Note += transactionDetail.ProcessError.Description;
                    //}
                    entity.QuantityKg = entity.UnitWeight * entity.Quantity;
                    if (exportTpDetails.Any()) {
                        var exportDetail =
                            exportTpDetails.FirstOrDefault(
                                ed => ed.TransactionDetailId == transactionId);
                        if (exportDetail != null)
                            entity.QuantityKg = exportDetail.Weight;
                    }
                    if (importSx1 == null) {
                        if (transactionDetail.ProductionMaterial != null) {
                            entity.MaterialCode = transactionDetail.ProductionMaterial.Material.MaterialCode;
                            entity.MaterialTypeName = transactionDetail.ProductionMaterial.Material.MaterialType.MaterialTypeName;
                            entity.Identity = transactionDetail.ProductionMaterial.Material.MaterialType.IdentityCode.Trim();
                        }
                    }
                    else {
                        var importDetail =
                            importSx1.ImportFormSX1Detail.FirstOrDefault(
                                id => id.ProductId == transactionDetail.ReferenceId);
                        if (importDetail != null) {
                            entity.MaterialCode = importDetail.MaterialInventory.Material.MaterialCode;
                            entity.MaterialTypeName =
                                importDetail.MaterialInventory.Material.MaterialType.MaterialTypeName;
                            entity.Identity =
                                importDetail.MaterialInventory.Material.MaterialType.IdentityCode.Trim();
                        }
                    }
                    model.Add(entity);
                }
                if (!transactionDetails.Any()) {
                    var transactionProducts = transaction.TransactionProducts;
                    foreach (var detail in transactionProducts) {
                        var entity = new TransactionDetailModel {
                            TransactionDetailId = detail.DetailId,
                            ProductCode = detail.Product.ProductCode,
                            CustomerCode = detail.Product.Customer.CustomerCode,
                            Quantity = detail.Quantity,
                            Note = detail.Note,
                            LotNumber = "",
                            ReferenceId = detail.ProductId,
                            UnitWeight =
                                MyUtilities.Product.GetProductInvWeight(detail.ProductId,
                                    transaction.WarehouseIssueId ?? transaction.WarehouseReceiptId.Value),
                            ProductImg = detail.Product.DrawingFinish,
                        };
                        if (string.IsNullOrWhiteSpace(entity.ProductImg))
                            entity.ProductImg = "askquestion.jpg";
                        if (transaction.WarehouseIssueId == (byte)MyUtilities.Warehouse.Business) {
                            entity.AvailableQuantity = 0;
                        }
                        else {
                            var invs =
                                vfi.ProductInventories.Where(
                                    pi =>
                                        pi.ProductId == detail.ProductId &&
                                        pi.WarehouseId == (transaction.WarehouseIssueId ?? 0)).ToList();
                            entity.AvailableQuantity = invs.Sum(pi => pi.TotalQty);
                        }
                        if ((transaction.WarehouseIssueId == MyUtilities.Warehouse.Plating ||
                             transaction.WarehouseIssueId == MyUtilities.Warehouse.PlatingTest) &&
                            transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcB) {
                            var import =
                                vfi.ImportNCU_QCB.FirstOrDefault(
                                    id => id.TransactionId == transaction.TransactionId);
                            if (import != null) {
                                entity.Note = import.PlatingForm.PlatingFormNumber;
                            }
                        }
                        if (transaction.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating &&
                            (transaction.WarehouseReceiptId == MyUtilities.Warehouse.PlatingTest ||
                             transaction.WarehouseReceiptId == MyUtilities.Warehouse.Plating)) {
                            var export =
                                vfi.ExportGCN_NCU.FirstOrDefault(
                                    id => id.TransactionId == transaction.TransactionId);
                            if (export != null) {
                                entity.Note = export.PlatingForm.PlatingFormNumber;
                            }
                        }
                        if (transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing) {
                            //if (transactionDetail.ErrorId != null)
                            //    entity.Note = detail.ProcessError.Description;
                        }
                        var productWeight = MyUtilities.Product.GetProductInvWeight(detail.ProductId,
                            transaction.WarehouseIssueId != null
                                ? transaction.WarehouseIssueId.Value
                                : transaction.WarehouseReceiptId.Value);
                        entity.QuantityKg = entity.UnitWeight * entity.Quantity;
                        if (exportTpDetails.Any()) {
                            var exportDetail =
                                exportTpDetails.FirstOrDefault(
                                    ed => ed.ProductId == detail.ProductId);
                            if (exportDetail != null)
                                entity.QuantityKg = exportDetail.Weight;
                        }
                        var material = detail.Product.ProductionMaterials.FirstOrDefault(pm => pm.Active);
                        if (material != null) {
                            entity.MaterialCode = material.Material.MaterialCode;
                            entity.MaterialTypeName = material.Material.MaterialType.MaterialTypeName;
                            entity.Identity = material.Material.MaterialType.IdentityCode.Trim();
                        }

                        model.Add(entity);
                    }

                }
            }

            return model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ThenBy(m => m.LotNumber).ToList();
        }

        [HttpPost]
        public ActionResult PrintImportExportForm(
            int transactionId,
            int formType) {
            try {
                switch (formType) {
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ImportSX1:
                        return ImportSX1FormView(transactionId);
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ExportGCN_NCU:
                        return ExportGCN_NCUFormView(transactionId);
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ImportNCU_QCB:
                        return ImportNCU_QCBFormView(transactionId);
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ExportTP:
                        //return ExportTPFormView(transactionId);
                        return PrintPackingList(transactionId);
                    case (byte)MyUtilities.Transaction.FormTypeEnum.ExportChange:
                        return ExportChangeFormView(transactionId);

                    default:
                        return PartialView(null);
                }
            }
            catch (Exception exception) {
                throw new Exception(exception.Message);
            }
        }

        private PartialViewResult ImportSX1FormView(int transactionId) {
            var model = new List<PrintImportSX1Model>();
            using (var vfi = new tammaContext()) {
                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                var importSX1 =
                    vfi.ImportFormSX1.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));

                foreach (var tDetai in importSX1.ImportFormSX1Detail) {
                    var entity = new PrintImportSX1Model() {
                        TransactionCode = transaction.TransactionCode,
                        Shift1Name = importSX1.Shift1Name,
                        Shift2Name = importSX1.Shift2Name,
                        MaterialUse1 = tDetai.MaterialUse1,
                        MaterialUse2 = tDetai.MaterialUse2,
                        Number1 = tDetai.Number1,
                        Number2 = tDetai.Number2,
                        Processing1 = tDetai.Processing1,
                        Processing2 = tDetai.Processing2,
                        DefectProduct1 = tDetai.DefectProduct1,
                        DefectProduct2 = tDetai.DefectProduct2,
                        Machine = tDetai.Machine ?? "",
                        ProductCode = vfi.Products.FirstOrDefault(p => p.ProductId == tDetai.ProductId).ProductCode,
                        ImportDateString = importSX1.ImportDate != null
                                               ? importSX1.ImportDate.ToString("dd/MM/yyyy")
                                               : "",
                        ModifiedDateString = importSX1.ModifiedDate != null
                                                 ? importSX1.ModifiedDate.ToString("dd/MM/yyyy")
                                                 : "",
                        ModifiedUser = importSX1.ModifiedUser,
                        StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                        TotalNumber = (tDetai.Number1 + tDetai.Number2),
                        TotalDefect = (tDetai.DefectProduct1 + tDetai.DefectProduct2),

                    };
                    model.Add(entity);
                }
            }
            return PartialView("PageImportSX1Form", model);
        }

        private PartialViewResult ExportGCN_NCUFormView(int transactionId) {
            var model = new List<PrintExportGCN_NCUModel>();
            using (var vfi = new tammaContext()) {
                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                if (transaction != null) {
                    var exportGCN_NCU =
                        vfi.ExportGCN_NCU.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));
                    var info = new WorkGroupInfo();
                    var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.Active);
                    if (workgroup != null) {
                        info = new WorkGroupInfo {
                            Logo = workgroup.ImagePath + "/Logo/" + workgroup.LogoImage,
                            //CompanyFullName = workgroup.CompanyFullName,
                            //CompanyShortName = workgroup.CompanyShortName,
                            //Address = workgroup.Address,
                            //TelNumber = "Tel : " + workgroup.TelNumber,
                            //FaxNumber = "Fax : " + workgroup.FaxNumber,
                            //Email = "Email: " + workgroup.Email,
                            //Website = "Website: " + workgroup.Website
                        };
                    }
                    foreach (var tDetail in exportGCN_NCU.ExportGCN_NCUDetail) {
                        var entity = model.FirstOrDefault(m => m.PlatingDetailId == tDetail.PlatingDetailId);
                        var platingForm =
                            vfi.PlatingForms.FirstOrDefault(pf => pf.FormId == exportGCN_NCU.PlatingFormId);
                        if (entity == null) {
                            entity = new PrintExportGCN_NCUModel() {
                                PlatingDetailId = tDetail.PlatingDetailId ?? 0,
                                TransactionCode = transaction.TransactionCode,
                                ProductId = tDetail.ProductId,
                                ProductCode = tDetail.Product.ProductCode,
                                ExportDateString = exportGCN_NCU.ExportDate != null
                                    ? exportGCN_NCU.ExportDate.Value.ToString("dd/MM/yyyy")
                                    : "",
                                ModifiedDateString = exportGCN_NCU.ModifiedDate != null
                                    ? exportGCN_NCU.ModifiedDate.Value.ToString("dd/MM/yyyy")
                                    : "",
                                ModifiedUser = exportGCN_NCU.ModifiedUser,
                                StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                                BoxNumber = exportGCN_NCU.BoxNumber,
                                BlockNumber = exportGCN_NCU.BlockNumber,
                                Note = tDetail.Note,
                                RealNumber = tDetail.RealNumber,
                                RequestNumber = tDetail.RequestNumber,
                                RequestNumberString = tDetail.RequestNumber,
                                Weight = (tDetail.Weight ?? 0) / 1000,
                                ProviderName = exportGCN_NCU.ProviderName,
                                CustomerCode = tDetail.Product.Customer.CustomerCode,
                                Package = "",
                                Info = info,
                            };
                            if (platingForm != null) {
                                entity.ProviderName = platingForm.Vendor.VendorName;
                                if (tDetail.PlatingFormDetail.Unit.Contains("Kg")) {
                                    var requestString = string.Format("{0:n3}", entity.RequestNumber);
                                    var str = requestString;
                                    int i = requestString.Length - 1;
                                    while (i > -1) {
                                        if (!str[i].Equals('0')) break;
                                        i--;
                                    }
                                    if (i != requestString.Length - 1)
                                        requestString =
                                            str.Remove(requestString.Length - i == 4 ? i : i + 1);
                                    entity.RequestNumberString = requestString + " " +
                                                                 tDetail.PlatingFormDetail.Unit;
                                }
                                else {
                                    entity.RequestNumberString = string.Format("{0:n0}", entity.RequestNumber) + " " +
                                                                 tDetail.PlatingFormDetail.Unit;
                                }
                            }
                            entity.WeightString = string.Format("{0:n3}", entity.Weight);
                            var str2 = entity.WeightString;
                            int j = entity.WeightString.Length - 1;
                            while (j > -1) {
                                if (!str2[j].Equals('0')) break;
                                j--;
                            }
                            if (j != entity.WeightString.Length - 1)
                                entity.WeightString =
                                    str2.Remove(entity.WeightString.Length - j == 4 ? j : j + 1);
                            entity.Package = tDetail.Package;
                            try {
                                entity.PackageNumber = Convert.ToInt32(entity.Package);
                            }
                            catch (Exception) { }
                            model.Add(entity);
                        }
                        else {
                            entity.RealNumber += tDetail.RealNumber;
                            //entity.RequestNumber += tDetail.RequestNumber;
                            entity.Weight += (tDetail.Weight ?? 0) / 1000;
                            if (platingForm != null) {
                                if (tDetail.PlatingFormDetail.Unit.Contains("Kg")) {
                                    var requestString = string.Format("{0:n3}", entity.RequestNumber);
                                    var str = requestString;
                                    int i = requestString.Length - 1;
                                    while (i > -1) {
                                        if (!str[i].Equals('0')) break;
                                        i--;
                                    }
                                    if (i != requestString.Length - 1)
                                        requestString =
                                            str.Remove(requestString.Length - i == 4 ? i : i + 1);
                                    entity.RequestNumberString = requestString + " " +
                                                                 tDetail.PlatingFormDetail.Unit;
                                }
                                else {
                                    entity.RequestNumberString = string.Format("{0:n0}", entity.RequestNumber) + " " +
                                                                 tDetail.PlatingFormDetail.Unit;
                                }
                                //entity.Package = tDetail.Package;
                            }
                            entity.WeightString = string.Format("{0:n3}", entity.Weight);
                            var str2 = entity.WeightString;
                            int j = entity.WeightString.Length - 1;
                            while (j > -1) {
                                if (!str2[j].Equals('0')) break;
                                j--;
                            }
                            if (j != entity.WeightString.Length - 1)
                                entity.WeightString =
                                    str2.Remove(entity.WeightString.Length - j == 4 ? j : j + 1);

                        }
                    }
                }
            }


            return PartialView("PageExportGCN_NCUForm", model.OrderBy(m => m.CustomerCode).ToList());
        }
        private PartialViewResult ImportNCU_QCBFormView(int transactionId) {
            var model = new List<PrintImportNCU_QCBModel>();
            using (var vfi = new tammaContext()) {
                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                if (transaction != null) {
                    var importNCU_QCB =
                        vfi.ImportNCU_QCB.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));
                    var info = new WorkGroupInfo();
                    var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.Active);
                    if (workgroup != null) {
                        info = new WorkGroupInfo {
                            Logo = workgroup.ImagePath + "/Logo/" + workgroup.LogoImage,
                            //CompanyFullName = workgroup.CompanyFullName,
                            //CompanyShortName = workgroup.CompanyShortName,
                            //Address = workgroup.Address,
                            //TelNumber = "Tel : " + workgroup.TelNumber,
                            //FaxNumber = "Fax : " + workgroup.FaxNumber,
                            //Email = "Email: " + workgroup.Email,
                            //Website = "Website: " + workgroup.Website
                        };
                    }
                    foreach (var tDetail in importNCU_QCB.ImportNCU_QCBDetail) {
                        var entity = new PrintImportNCU_QCBModel() {
                            TransactionCode = transaction.TransactionCode,

                            ProductCode =
                                vfi.Products.FirstOrDefault(p => p.ProductId == tDetail.ProductId).ProductCode,
                            ImportDateString = importNCU_QCB.ImportDate != null
                                                   ? importNCU_QCB.ImportDate.ToString("dd/MM/yyyy")
                                                   : "",
                            ModifiedDateString = importNCU_QCB.ModifiedDate != null
                                                     ? importNCU_QCB.ModifiedDate.ToString("dd/MM/yyyy")
                                                     : "",
                            ModifiedUser = importNCU_QCB.ModifiedUser,
                            StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                            BoxNumber = importNCU_QCB.BoxNumber,
                            Note = tDetail.Note,
                            RealNumber = tDetail.RealNumber,
                            RequestNumber = tDetail.RequestNumber,
                            RequestNumberString = tDetail.RequestNumber,
                            Weight = tDetail.Weight,
                            ProviderName = importNCU_QCB.ProviderName,
                            CustomerCode = vfi.Customers.FirstOrDefault(c => c.CustomerId == tDetail.Product.CustomerId).CustomerCode ?? "",
                            Info = info
                        };
                        var platingForm = vfi.PlatingForms.FirstOrDefault(pf => pf.FormId == importNCU_QCB.PlatingFormId);
                        if (platingForm != null) {
                            entity.ProviderName = platingForm.Vendor.VendorName;
                            if (tDetail.ExportGCN_NCUDetail.PlatingFormDetail.Unit.Contains("Kg")) {
                                var requestString = string.Format("{0:n3}", entity.RequestNumber);
                                var str = requestString;
                                int i = requestString.Length - 1;
                                while (i > -1) {
                                    if (!str[i].Equals('0')) break;
                                    i--;
                                }
                                if (i != requestString.Length - 1)
                                    requestString =
                                        str.Remove(requestString.Length - i == 4 ? i : i + 1);
                                entity.RequestNumberString = requestString + " " +
                                                             tDetail.ExportGCN_NCUDetail.PlatingFormDetail.Unit;
                            }
                            else {
                                entity.RequestNumberString = string.Format("{0:n0}", entity.RequestNumber) + " " +
                                                             tDetail.ExportGCN_NCUDetail.PlatingFormDetail.Unit;
                            }
                            entity.Package = tDetail.Package;
                        }
                        entity.Weight = (tDetail.Weight) / 1000;
                        entity.WeightString = string.Format("{0:n3}", entity.Weight);
                        var str2 = entity.WeightString;
                        int j = entity.WeightString.Length - 1;
                        while (j > -1) {
                            if (!str2[j].Equals('0')) break;
                            j--;
                        }
                        if (j != entity.WeightString.Length - 1)
                            entity.WeightString =
                                str2.Remove(entity.WeightString.Length - j == 4 ? j : j + 1);
                        entity.PurchasingSignature = importNCU_QCB.PurchasingSignature;
                        model.Add(entity);
                    }
                }
            }

            return PartialView("PageImportNCU_QCBForm", model.OrderBy(m => m.CustomerCode).ToList());
        }

        private PartialViewResult ExportTPFormView(int transactionId) {
            var model = new List<PrintExportTP_KDModel>();
            using (var vfi = new tammaContext()) {
                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                if (transaction != null) {
                    var export =
                        vfi.ExportFormTP_KD.FirstOrDefault(i => i.TransactionCode.Equals(transaction.TransactionCode));
                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == export.OrderId);
                    var info = new WorkGroupInfo();
                    var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.Active);
                    if (workgroup != null) {
                        info = new WorkGroupInfo {
                            Logo = workgroup.ImagePath + "/Logo/" + workgroup.LogoImage,
                            //CompanyFullName = workgroup.CompanyFullName,
                            //CompanyShortName = workgroup.CompanyShortName,
                            //Address = workgroup.Address,
                            //TelNumber = "Tel : " + workgroup.TelNumber,
                            //FaxNumber = "Fax : " + workgroup.FaxNumber,
                            //Email = "Email: " + workgroup.Email,
                            //Website = "Website: " + workgroup.Website
                        };
                    }
                    foreach (var tDetail in export.ExportFormTP_KDDetail) {
                        var entity = new PrintExportTP_KDModel() {
                            TransactionCode = transaction.TransactionCode,

                            ProductCode =
                                vfi.Products.FirstOrDefault(p => p.ProductId == tDetail.ProductId).ProductCode,
                            DateTransporterString = export.DateTransporter != null
                                                   ? export.DateTransporter.Value.ToString("dd/MM/yyyy")
                                                   : "",
                            ModifiedDateString = export.ModifiedDate != null
                                                     ? export.ModifiedDate.Value.ToString("dd/MM/yyyy")
                                                     : "",
                            ModifiedUser = export.ModifiedUser,
                            StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                            TotalBox = export.TotalBox,
                            Note = tDetail.Note,
                            Quality = tDetail.Quality,
                            Weight = tDetail.Weight ?? 0.0,
                            // Weight = tDetail.Weight ?? 0.0,
                            Transporter = export.Transporter ?? "",
                            CompanyTransporter = export.CompanyTransporter ?? "",
                            CarNumber = export.CarNumber ?? "",
                            //OrderNumber = order.OrderNumber ?? "",
                            //EmployeeName = vfi.Employees.FirstOrDefault(e => e.EmployeeId == order.SalesPersonId).EmployeeName ?? "",
                            Info = info
                        };
                        var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == export.CustomerId);
                        entity.CustomerCodeName = customer.CustomerCode + " - " + customer.CustomerName;

                        model.Add(entity);
                    }
                }
            }

            return PartialView("PageExportTP_KDForm", model.OrderBy(o => o.ProductCode).ToList());
        }

        private PartialViewResult ExportChangeFormView(int transactionId) {
            var model = new List<PrintExportTP_KDModel>();
            using (var vfi = new tammaContext()) {
                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                if (transaction != null) {
                    var exportChange = vfi.ExportChangeProducts.FirstOrDefault(ec => ec.TransactionId == transactionId);
                    var orderNote = vfi.OrderNotes.FirstOrDefault(o => o.NoteId == exportChange.NoteId);
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == orderNote.InvoiceId);
                    foreach (var tDetail in transaction.TransactionDetails) {
                        var entity = new PrintExportTP_KDModel() {
                            TransactionCode = transaction.TransactionCode,

                            ProductCode =
                                vfi.Products.FirstOrDefault(p => p.ProductId == tDetail.ReferenceId).ProductCode,
                            DateTransporterString = exportChange.ExportDate != null
                                                   ? exportChange.ExportDate.Value.ToString("dd/MM/yyyy")
                                                   : "",
                            ModifiedDateString = exportChange.ModifiedDate != null
                                                     ? exportChange.ModifiedDate.Value.ToString("dd/MM/yyyy")
                                                     : "",
                            ModifiedUser = exportChange.ModifiedUser,
                            StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                            TotalBox = exportChange.Totalbox ?? 0,
                            Note = tDetail.Note,
                            Quality = tDetail.Quantity,
                            Weight = tDetail.QuantityKg ?? 0.0,
                            // Weight = tDetail.Weight ?? 0.0,
                            Transporter = exportChange.Transporter ?? "",
                            CompanyTransporter = exportChange.CompanyTransport ?? "",
                            CarNumber = exportChange.CarNumber ?? "",
                            NoteNumber = orderNote.NoteNumber ?? "",
                            InvoiceNumber = invoice.InvoiceNumber ?? "",
                        };
                        var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == invoice.CustomerId);
                        entity.CustomerCodeName = customer.CustomerCode + " - " + customer.CustomerName;

                        model.Add(entity);
                    }
                }
            }

            return PartialView("PageExportChangeProductForm", model.OrderBy(o => o.ProductCode).ToList());
        }


        [GridAction]
        public ActionResult SelectProductInOrder(int id, int type) {
            var model = new List<SelectProductInOrderModel>();
            try {
                if (id == 0)
                    return View(new GridModel(model));
                using (var vfi = new tammaContext()) {
                    // xuat ban
                    if (type == 1) {
                        var order = vfi.Orders.FirstOrDefault(o => o.OrderId == id);
                        if (order == null)
                            return View(new GridModel());
                        var productIds = order.OrderDetails.Select(od => od.ProductId).ToList();
                        var productInvs =
                            vfi.ProductInventories.Where(
                                pi => productIds.Contains(pi.ProductId) && pi.WarehouseId == MyUtilities.Warehouse.Finish);
                        foreach (var tDetail in order.OrderDetails) {
                            var entity = new SelectProductInOrderModel {
                                DetailId = tDetail.OrderDetailId,
                                ProductCode = tDetail.Product.ProductCode,
                                ProductId = tDetail.Product.ProductId,
                                RequiredNumber = (tDetail.RequiedNumber),
                            };
                            var productInv = productInvs.FirstOrDefault(pi => pi.ProductId == tDetail.ProductId);
                            if (productInv != null)
                                entity.TotalQuanlity = productInv.TotalQty;
                            model.Add(entity);
                        }
                    }
                    // tra hang
                    if (type == 2) {
                        var orderNote =
                            vfi.OrderNotes.FirstOrDefault(
                                on => on.NoteId == id && on.NoteType == 2 && !(on.IsComplete ?? false));
                        if (orderNote == null)
                            return View(new GridModel());
                        var exportChanges =
                            vfi.ExportChangeProducts.Where(
                                ec =>
                                ec.NoteId == id && ec.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        foreach (var detail in orderNote.OrderNoteDetails) {
                            var product = vfi.Products.FirstOrDefault(p => p.ProductId == detail.ProductId);
                            if (product == null) throw new AggregateException("Lỗi SP!");
                            var entity = new SelectProductInOrderModel {
                                DetailId = detail.NoteDetailId,
                                ProductCode = product.ProductCode,
                                ProductId = product.ProductId,
                                RequiredNumber = (detail.Quantity ?? 0)
                            };
                            var productInv =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi => pi.ProductId == detail.ProductId && pi.WarehouseId == MyUtilities.Warehouse.Finish);
                            if (productInv != null)
                                entity.TotalQuanlity = productInv.TotalQty;
                            if (exportChanges.Any()) {
                                foreach (var exportChange in exportChanges) {
                                    var transactionDetail =
                                        vfi.TransactionDetails.FirstOrDefault(
                                            td =>
                                            td.ReferenceId == detail.ProductId &&
                                            td.TransactionId == exportChange.TransactionId);
                                    if (transactionDetail != null)
                                        entity.RequiredNumber -= (transactionDetail.Quantity);
                                }
                            }
                            model.Add(entity);
                        }
                    }
                    return View(new GridModel(model));
                }
            }
            catch (Exception) {
                return View(new GridModel(model));
            }
        }

        [GridAction]
        public ActionResult SelectProductInExportTp(int id) {
            var model = new List<ExportFormTP_KDDetailsModel>();
            if (id == 0)
                return View(new GridModel(model));
            try {
                using (var vfi = new tammaContext()) {
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == id);
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);

                    foreach (var detail in export.ExportFormTP_KDDetail) {
                        var entity = new ExportFormTP_KDDetailsModel {
                            DetailId = detail.DetailId,
                            ProductCode = detail.Product.ProductCode,
                            ProductId = detail.Product.ProductId,
                            Quality = detail.Quality,
                        };
                        var invoiceDetails =
                            vfi.InvoiceDetails.Where(
                                invoiceDetail => invoiceDetail.ExportDetailId == detail.DetailId && invoiceDetail.Active);
                        if (invoiceDetails.Any())
                            entity.TotalQuantity = invoiceDetails.Sum(invoicedetail => invoicedetail.Piece);
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductInExportTp", ex.Message);
            }
            return View(new GridModel(model));
        }

        public List<CustomerIncomeModel> GetCustomerIncomeReport(int customerById,
                  int fromMonth, int fromYear,
                  int toMonth, int toYear) {
            var model = new List<CustomerIncomeModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var fromMonthly = new DateTime(fromYear, fromMonth, 1).AddSeconds(-1);
                    var toMonthly = new DateTime(toYear, toMonth, 1).AddMonths(1).AddSeconds(-1);
                    var invoices = (from x in vfi.Invoices
                                    where
                                         (customerById == 0 || x.CustomerId == customerById) &&
                                         //x.CustomerId == 13 || x.CustomerId == 117 &&
                                         x.ShipmentDate > fromMonthly
                                         && x.ShipmentDate < toMonthly
                                         && x.Active
                                         //&& x.InvoiceId == 1647
                                         //&& x.InvoiceNumber == "VFI-251789"
                                         && x.Status != (byte)MyUtilities.Sales.Status.Cancel
                                    select new {
                                        x.InvoiceDetails,
                                        x.InvoiceNumber,
                                        x.ExchangeRate,
                                        x.TaxPercent,
                                        x.ExportFormTP_KD,
                                        x.CustomerId,
                                        x.Customer.CustomerCode,
                                        x.Customer.CustomerName,
                                        x.Customer.CustomerTypeId,
                                    }).ToList();
                    //if (customerById != 0)
                    //    invoices = invoices.Where(i => i.CustomerId == customerById);
                    var customers = invoices.Select(e => new {
                        e.CustomerId,
                        e.CustomerCode,
                        e.CustomerName,
                        e.CustomerTypeId
                    }).Distinct().ToList();
                    
                    var exchangeRate = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.ExchangeToVndRate);
                    //var orderIds = invoices.Select(e => e.OrderId).Distinct();
                    foreach (var customer in customers) {
                        //var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                        var entity = new CustomerIncomeModel {
                            CustomerCodeName = customer.CustomerCode + "-" + customer.CustomerName,
                            ReportDateString = toMonthly.ToString("MM/yyyy"),
                        };
                        var invoicesByCustomer = invoices.Where(i => i.CustomerId == customer.CustomerId).ToList();
                        foreach (var invoice in invoicesByCustomer) {
                            var export = invoice.ExportFormTP_KD;
                            foreach (var exportDetail in export.ExportFormTP_KDDetail) {
                                var product = vfi.Products.FirstOrDefault(p => p.ProductId == exportDetail.ProductId);
                                var producName = vfi.Products.Where(t => t.ProductId == exportDetail.ProductId).Select(p => p.ProductCode).ToList();
                                var invoiceDetails = exportDetail.InvoiceDetails.Where(id => id.Active).ToList();
                                if (invoiceDetails.Any()) {
                                    var invoiceDetail = invoiceDetails.FirstOrDefault(x => x.OrderDetailId != null);
                                    var detail = new CustomerIncomeDetailModel {
                                        ProductCodeName = product.ProductCode,
                                        ProductId = product.ProductId,
                                        Quatity = invoiceDetails.Sum(id => id.Piece),
                                        UnitPrice = invoiceDetail != null ? invoiceDetail.Price.Value : 0,
                                        DueDateString = export.DateTransporter != null
                                                            ? export.DateTransporter.Value.ToString("dd/MM/yyyy")
                                                            : "",
                                        CurrencyCode = invoiceDetail != null ? invoiceDetail.OrderDetail.Order.CurrencyCode : "",
                                        //CurrencyCode = invoiceDetail.OrderDetail.Order.CurrencyCode,
                                        TaxInvoiceNumber = "",
                                        InvoiceNumber = invoice.InvoiceNumber,
                                        ExchangeRate = invoice.ExchangeRate,
                                        TaxPercent = invoice.TaxPercent,
                                        
                                    };
                                    if (detail.Quatity == 0) continue;
                                    var taxInvoiceProductDetail =
                                        vfi.TaxInvoiceProductDetails.FirstOrDefault(
                                            tip => tip.ExportDetailId == exportDetail.DetailId && tip.Active.Value);
                                    if (taxInvoiceProductDetail != null) {              
                                        detail.TaxInvoiceNumber = taxInvoiceProductDetail.TaxInvoice.TaxInvoiceList;
                                        detail.TaxPercent = taxInvoiceProductDetail.TaxInvoice.TaxPercent;
                                        detail.CurrencyCode = taxInvoiceProductDetail.TaxInvoice.Currency;
                                        detail.TaxInvoiceDateString =
                                            taxInvoiceProductDetail.TaxInvoice.SetupDate.Value.ToString("dd/MM/yyyy");
                                        if (taxInvoiceProductDetail.TaxInvoice.Currency.Equals("VND")) {
                                            detail.ExchangeRate = 1;
                                        }
                                        else {
                                            detail.ExchangeRate = taxInvoiceProductDetail.TaxInvoice.ExchangeRate;
                                        }

                                        var exchangerate = detail.ExchangeRate;
                                        var taxInvoiceProduct = vfi.TaxInvoiceProducts
                                                                   .FirstOrDefault(
                                                                       tip =>
                                                                       tip.ProductId == detail.ProductId &&
                                                                       tip.TaxInvoiceId ==
                                                                       taxInvoiceProductDetail.TaxInvoiceId);
                                        if (taxInvoiceProduct != null)
                                            detail.UnitPrice = taxInvoiceProduct.UnitPrice;
                                    }
                                    if (customer.CustomerTypeId != MyUtilities.Sales.CustomerKcx &&
                                        customer.CustomerTypeId != MyUtilities.Sales.CustomerForeign) {
                                        detail.CurrencyCode = "VND";
                                        detail.UnitPrice = detail.UnitPrice * detail.ExchangeRate;
                                        detail.ExchangeRate = 1;
                                    }

                                    if (detail.CurrencyCode != "VND" && detail.ExchangeRate == 1) {
                                        detail.ExchangeRate = exchangeRate;
                                    }

                                    entity.Details.Add(detail);
                                }
                                else {
                                    var detail = new CustomerIncomeDetailModel {
                                        ProductCodeName = product.ProductCode + "-" + product.ProductName,
                                        Quatity = exportDetail.Quality,
                                        UnitPrice = 0,
                                        DueDateString = export.DateTransporter != null
                                                            ? export.DateTransporter.Value.ToString("dd/MM/yyyy")
                                                            : "",
                                        CurrencyCode = "",
                                        TaxInvoiceNumber = "",
                                        InvoiceNumber = invoice.InvoiceNumber,
                                    };
                                    entity.Details.Add(detail);
                                }
                            }
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetCustomerIncomeReport", "" + ex.Message);
            }
            return model;
        }

        [HttpPost]
        public ActionResult PrintCustomerIncome(
            int customerById,
                  int fromMonth,
                   int fromYear,
                  int toMonth,
                  int toYear) {
            var model = new List<CustomerIncomeModel>();
            try {
                model = GetCustomerIncomeReport(customerById, fromMonth, fromYear, toMonth, toYear);
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintCustomerIncome", "" + ex.Message);
            }
            return PartialView("PageCustomerIncome", model);
        }

        [HttpPost]
        public ActionResult PrintCustomerIncome2(
            int customerById,
                  int fromMonth,
                   int fromYear,
                  int toMonth,
                  int toYear) {
            var model = new List<CustomerIncomeModel>();
            try {
                model = GetCustomerIncomeReport(customerById, fromMonth, fromYear, toMonth, toYear);
                foreach (var entity in model) {
                    var detailGroups = entity.Details.Select(d => new { d.ProductId, d.UnitPrice, d.HasTaxInvoice, d.CurrencyCode }).Distinct().ToList();
                    var newDetails = new List<CustomerIncomeDetailModel>();
                    foreach (var detailGroup in detailGroups) {
                        var details = entity.Details.Where(d => d.ProductId == detailGroup.ProductId &&
                            d.UnitPrice == detailGroup.UnitPrice &&
                            d.CurrencyCode.Equals(detailGroup.CurrencyCode) &&
                            d.HasTaxInvoice == detailGroup.HasTaxInvoice);
                        var detail = details.FirstOrDefault();
                        var newDetail = new CustomerIncomeDetailModel {
                            ProductCodeName = detail.ProductCodeName,
                            ProductId = detail.ProductId,
                            Quatity = details.Sum(id => id.Quatity),
                            UnitPrice = detail.UnitPrice,
                            DueDateString = "",
                            CurrencyCode = detail.CurrencyCode,
                            TaxInvoiceNumber = detailGroup.HasTaxInvoice ? "Đã XHĐ" : "",
                            InvoiceNumber = "",
                            ExchangeRate = detail.ExchangeRate,
                            TaxPercent = detail.TaxPercent,
                        };

                        newDetails.Add(newDetail);
                    }
                    entity.Details = newDetails.OrderBy(d => d.ProductCodeName).ToList();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintCustomerIncome", "" + ex.Message);
            }
            return PartialView("PageCustomerIncome", model);
        }

        [HttpPost]
        public ActionResult PrintCustomerIncome3(
            int customerById,
                  int fromMonth,
                   int fromYear,
                  int toMonth,
                  int toYear) {
            var model = new List<CustomerIncomeModel>();
            try {
                model = GetCustomerIncomeReport(customerById, fromMonth, fromYear, toMonth, toYear);
                foreach (var entity in model) {
                    var detailGroups = entity.Details.Select(d => new { d.ProductId, d.UnitPrice }).Distinct().ToList();
                    var newDetails = new List<CustomerIncomeDetailModel>();
                    foreach (var detailGroup in detailGroups) {
                        var details = entity.Details.Where(d =>
                            d.ProductId == detailGroup.ProductId &&
                            d.UnitPrice == detailGroup.UnitPrice);
                        var detail = details.FirstOrDefault();
                        var newDetail = new CustomerIncomeDetailModel {
                            ProductCodeName = detail.ProductCodeName,
                            ProductId = detail.ProductId,
                            Quatity = details.Sum(id => id.Quatity),
                            UnitPrice = detail.UnitPrice,
                            DueDateString = "",
                            CurrencyCode = detail.CurrencyCode,
                            TaxInvoiceNumber = "",
                            InvoiceNumber = "",
                            ExchangeRate = detail.ExchangeRate,
                            TaxPercent = detail.TaxPercent,
                        };
                        newDetails.Add(newDetail);
                    }
                    entity.Details = newDetails.OrderBy(d => d.ProductCodeName).ToList();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintCustomerIncome", "" + ex.Message);
            }
            return PartialView("PageCustomerIncome", model);
        }

        [HttpPost]
        public ActionResult PrintCustomerIncome2_1(
            int customerById,
                  int fromMonth,
                   int fromYear,
                  int toMonth,
                  int toYear) {
            try {
                var model = new List<CustomerIncomeModel>();

                using (var vfi = new tammaContext()) {
                    var fromMonthly = new DateTime(fromYear, fromMonth, 1).AddSeconds(-1);
                    var toMonthly = new DateTime(toYear, toMonth, 1).AddMonths(1).AddSeconds(-1);
                    var invoices =
                        vfi.Invoices.Where(
                            e =>
                            e.ShipmentDate > fromMonthly &&
                            e.ShipmentDate < toMonthly &&
                            e.Active &&
                                //e.InvoiceId == 7138 &&
                            (customerById == 0 || e.CustomerId == customerById) &&
                            e.Status != (byte)MyUtilities.Sales.Status.Cancel);
                    var customerIds = invoices.Select(e => e.CustomerId).Distinct();
                    foreach (var customerId in customerIds) {
                        var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                        var entity = new CustomerIncomeModel {
                            CustomerCodeName = customer.CustomerCode + "-" + customer.CustomerName,
                            ReportDateString = toMonthly.ToString("MM/yyyy")
                        };
                        var invoicesByCustomer = invoices.Where(i => i.CustomerId == customerId);
                        var details = new List<CustomerIncomeDetailModel>();
                        foreach (var invoice in invoicesByCustomer) {
                            var export = invoice.ExportFormTP_KD;
                            foreach (var exportDetail in export.ExportFormTP_KDDetail) {
                                var product = vfi.Products.FirstOrDefault(p => p.ProductId == exportDetail.ProductId);
                                var invoiceDetails = exportDetail.InvoiceDetails.Where(id => id.Active).ToList();

                                if (invoiceDetails.Any()) {
                                    if (invoiceDetails.Sum(id => id.Piece) > 0) {
                                        var unitPrice = invoiceDetails.FirstOrDefault().Price.Value;
                                        var taxInvoiceProductDetail =
                                            vfi.TaxInvoiceProductDetails.FirstOrDefault(
                                                tip => tip.ExportDetailId == exportDetail.DetailId && tip.Active.Value);
                                        if (taxInvoiceProductDetail != null) {
                                            var taxInvoiceProduct = vfi.TaxInvoiceProducts
                                                                       .FirstOrDefault(
                                                                           tip =>
                                                                           tip.ProductId == product.ProductId &&
                                                                           tip.TaxInvoiceId ==
                                                                           taxInvoiceProductDetail.TaxInvoiceId);
                                            if (taxInvoiceProduct != null)
                                                unitPrice = taxInvoiceProduct.UnitPrice;
                                        }
                                        var detail =
                                            details.FirstOrDefault(
                                                d =>
                                                d.ProductId == product.ProductId &&
                                                d.UnitPrice == unitPrice);
                                        if (detail == null) {
                                            detail = new CustomerIncomeDetailModel {
                                                ProductCodeName = product.ProductCode,
                                                ProductId = product.ProductId,
                                                UnitPrice = unitPrice,
                                                CurrencyCode =
                                                    invoiceDetails.FirstOrDefault().OrderDetail.Order.CurrencyCode,
                                                TaxInvoiceNumber = "",
                                                ExchangeRate = invoice.ExchangeRate,
                                                TaxPercent = invoice.TaxPercent,
                                            };
                                            details.Add(detail);
                                        }
                                        detail.Quatity += invoiceDetails.Sum(id => id.Piece);
                                        if (taxInvoiceProductDetail != null) {
                                            detail.TaxPercent = taxInvoiceProductDetail.TaxInvoice.TaxPercent;
                                            detail.CurrencyCode = taxInvoiceProductDetail.TaxInvoice.Currency;
                                            if (taxInvoiceProductDetail.TaxInvoice.Currency.Equals("VND"))
                                                detail.ExchangeRate = 1;
                                            else
                                                detail.ExchangeRate = taxInvoiceProductDetail.TaxInvoice.ExchangeRate;
                                        }
                                        detail.InvoiceNumber = "";
                                        detail.DueDateString = "";
                                        detail.TaxInvoiceNumber = "";
                                    }
                                }
                                else {
                                    var detail = new CustomerIncomeDetailModel {
                                        ProductCodeName = product.ProductCode + "-" + product.ProductName,
                                        Quatity = exportDetail.Quality,
                                        UnitPrice = 0,
                                        DueDateString = export.DateTransporter != null
                                                            ? export.DateTransporter.Value.ToString("dd/MM/yyyy")
                                                            : "",
                                        CurrencyCode = "",
                                        TaxInvoiceNumber = "",
                                        //InvoiceNumber = invoice.InvoiceNumber,
                                    };
                                    details.Add(detail);
                                }

                            }
                        }
                        entity.Details.AddRange(details.OrderBy(d => d.ProductCodeName));
                        model.Add(entity);
                    }

                }
                return PartialView("PageCustomerIncome", model);
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintCustomerIncome", "" + ex.Message);
            }
            return PartialView("PageCustomerIncome");
        }


        [HttpPost]
        public ActionResult PrintOrderPlan(
            int month, int year) {
            var model = new List<PrintProductionPlanModel>();

            using (var vfi = new tammaContext()) {
                //int i = 0;
                var lastMonth = new DateTime(year, month, 1).AddSeconds(-1);
                var monthlyDate = lastMonth.AddMonths(1);
                var orderDetails = (from od in vfi.OrderDetails
                                    where
                                         od.Order.DueDate != null &&
                                         od.Order.DueDate.Value.Month == month &&
                                         od.Order.DueDate.Value.Year == year &&
                                         od.RequiedNumber > 0 &&
                                         (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                          od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess)
                                    select new {
                                        od.ProductId,
                                        od.Product.ProductCode,
                                        od.Product.DesignNo,
                                        od.Order.Customer.CustomerCode,
                                        od.Order.OrderNumber,
                                        DueDate = od.Order.DueDate.Value,
                                        od.RequiedNumber,
                                    }).ToList();
                var warehouseIds = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                //var qcInvs = MyUtilities.Warehouse.GetWarehouseIdQc();
                foreach (var orderDetail in orderDetails) {
                    //var details = order.OrderDetails;
                    //foreach (var orderDetail in details)
                    //{

                    var entity = model.FirstOrDefault(m => m.ProductId == orderDetail.ProductId);
                    if (entity == null) {
                        entity = new PrintProductionPlanModel();
                        entity.ProductId = orderDetail.ProductId;
                        entity.ProductCode = orderDetail.ProductCode;
                        entity.AreaName = orderDetail.DesignNo;
                        entity.CustomerCode = orderDetail.CustomerCode;
                        entity.OrderNumberList = orderDetail.OrderNumber;
                        entity.Month = month;
                        entity.RequiedNumber[orderDetail.DueDate.Day - 1] += (orderDetail.RequiedNumber);
                        entity.RequiedNumber[33] += (orderDetail.RequiedNumber);
                        //var area = vfi.Areas.FirstOrDefault(a => a.AreaId == orderDetail.Order.Customer.AreaId);
                        //if (area == null)
                        //{
                        //    area = vfi.Areas.FirstOrDefault(a => a.AreaId == 1);
                        //}
                        //entity.AreaName = area.AreaName;

                        var productInvs =
                            vfi.ProductInventories.Where(
                                pi => pi.ProductId == entity.ProductId &&
                                    warehouseIds.Contains(pi.WarehouseId) &&
                                    pi.TotalQty > 0).Select(x => new {
                                        x.ProductId,
                                        x.TotalQty,
                                        x.Warehouse.IsPacking,
                                        x.Warehouse.IsFinish,
                                        x.Warehouse.IsQC
                                    }).ToList();

                        entity.TotalInventory = productInvs.Sum(pi => pi.TotalQty);
                        entity.FinishInventory = productInvs.Where(pi => pi.IsFinish).Sum(pi => pi.TotalQty);
                        entity.PackingInv = productInvs.Where(pi => pi.IsPacking).Sum(pi => pi.TotalQty);
                        entity.QcInventory = productInvs.Where(pi => pi.IsQC).Sum(pi => pi.TotalQty);
                        model.Add(entity);
                    }
                    else {
                        entity.OrderNumberList += ("," + orderDetail.OrderNumber);
                        entity.RequiedNumber[orderDetail.DueDate.Day - 1] += (orderDetail.RequiedNumber);
                        entity.RequiedNumber[33] += (orderDetail.RequiedNumber);
                    }
                    //}
                }
                var ordersInLastMonth = (from od in vfi.OrderDetails
                                         where
                                              od.Order.DueDate != null &&
                                              od.Order.DueDate < lastMonth &&
                                              od.RequiedNumber > 0 &&
                                              (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                               od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess)
                                         select new {
                                             od.ProductId,
                                             od.Product.ProductCode,
                                             od.Product.DesignNo,
                                             od.Order.Customer.CustomerCode,
                                             od.Order.OrderNumber,
                                             DueDate = od.Order.DueDate.Value,
                                             od.RequiedNumber,
                                         }).ToList();
                //foreach (var order in ordersInLastMonth)
                //{
                //    var details = order.OrderDetails;
                foreach (var orderDetail in ordersInLastMonth) {
                    //if (orderDetail.Product.ProductCode.Equals("F3251B"))
                    //    i = 5;
                    //if ((orderDetail.IsComplete ?? false)) continue;

                    var entity = model.FirstOrDefault(m => m.ProductId == orderDetail.ProductId);
                    if (entity == null) {
                        entity = new PrintProductionPlanModel();
                        entity.ProductId = orderDetail.ProductId;
                        entity.ProductCode = orderDetail.ProductCode;
                        entity.AreaName = orderDetail.DesignNo;
                        entity.CustomerCode = orderDetail.CustomerCode;
                        entity.OrderNumberList = orderDetail.OrderNumber;
                        entity.Month = month;
                        entity.RequiedNumber[31] += (orderDetail.RequiedNumber);
                        entity.RequiedNumber[33] += (orderDetail.RequiedNumber);
                        //var area = vfi.Areas.FirstOrDefault(a => a.AreaId == orderDetail.Order.Customer.AreaId);
                        //if (area == null)
                        //{
                        //    area = vfi.Areas.FirstOrDefault(a => a.AreaId == 1);
                        //}
                        //entity.AreaName = area.AreaName;

                        var productInvs =
                            vfi.ProductInventories.Where(
                                pi => pi.ProductId == entity.ProductId &&
                                    warehouseIds.Contains(pi.WarehouseId) &&
                                    pi.TotalQty > 0).Select(x => new {
                                        x.ProductId,
                                        x.TotalQty,
                                        x.Warehouse.IsPacking,
                                        x.Warehouse.IsFinish,
                                        x.Warehouse.IsQC
                                    }).ToList();

                        entity.TotalInventory = productInvs.Sum(pi => pi.TotalQty);
                        entity.FinishInventory = productInvs.Where(pi => pi.IsFinish).Sum(pi => pi.TotalQty);
                        entity.PackingInv = productInvs.Where(pi => pi.IsPacking).Sum(pi => pi.TotalQty);
                        entity.QcInventory = productInvs.Where(pi => pi.IsQC).Sum(pi => pi.TotalQty);

                        model.Add(entity);
                    }
                    else {
                        entity.OrderNumberList += ("," + orderDetail.OrderNumber);
                        entity.RequiedNumber[31] += (orderDetail.RequiedNumber);
                        entity.RequiedNumber[33] += (orderDetail.RequiedNumber);
                    }
                }
            }
            return PartialView("PageOrderPlan", model
                //.OrderBy(c => c.AreaName)
                .OrderBy(p => p.CustomerCode)
                .ThenBy(p => p.ProductCode)
                .ToList());
        }


        [HttpPost]
        public ActionResult PrintTaxInvoiceProductDetail(
            string monthlyDate) {
            var model = new List<InvoiceDetailTempModel>();
            try {

                var month = Convert.ToInt32(monthlyDate);
                var montlyDate = new DateTime(DateTime.Now.Year, month, 1);
                using (var vfi = new tammaContext()) {
                    var taxInvoices =
                        vfi.TaxInvoices.Where(
                            ti =>
                            ti.SetupDate != null && ti.SetupDate.Value.Month == montlyDate.Month &&
                            (ti.Status == (byte)MyUtilities.Sales.Status.Completed || ti.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                             ti.Status == (byte)MyUtilities.Sales.Status.InProcess));

                    foreach (var taxInvoice in taxInvoices) {
                        //var a = 5;
                        //if (taxInvoice.TaxInvoiceList.Equals("113"))
                        //    a = 6;
                        foreach (var productDetail in taxInvoice.TaxInvoiceProductDetails) {
                            if (productDetail.Active == false) continue;
                            var entity =
                                model.FirstOrDefault(
                                    m =>
                                    m.TaxInvoiceList.Equals(taxInvoice.TaxInvoiceList) &&
                                    m.SetupDate == taxInvoice.SetupDate.Value &&
                                    m.ProductId == productDetail.ExportFormTP_KDDetail.ProductId.Value &&
                                    m.CurrencyCode.Equals(taxInvoice.Currency) &&
                                    m.UnitPrice == productDetail.UnitPrice);
                            //  var entityList =
                            //      model.Where(m => m.TaxInvoiceList.Equals(taxInvoice.TaxInvoiceList) &&
                            //                       m.SetupDate == taxInvoice.SetupDate.Value &&
                            //                       m.CurrencyCode.Equals(taxInvoice.Currency));
                            //entity =  entityList.FirstOrDefault(el =>
                            //                            el.ProductId ==
                            //                            productDetail.ExportFormTP_KDDetail.ProductId.Value &&
                            //                            el.UnitPrice == productDetail.UnitPrice);
                            if (entity == null) {
                                entity = new Sales.Models.InvoiceDetailTempModel {
                                    TaxInvoiceList = taxInvoice.TaxInvoiceList,
                                    SetupDate = taxInvoice.SetupDate.Value,
                                    CustomerName = taxInvoice.Customer.CustomerName,
                                    ProductName = productDetail.ExportFormTP_KDDetail.Product.ProductName,
                                    ProductId = productDetail.ExportFormTP_KDDetail.ProductId.Value,
                                    Quantity = productDetail.Quantity,
                                    UnitPrice = productDetail.UnitPrice,
                                };
                                entity.CurrencyCode = taxInvoice.Currency;
                                entity.Amount = entity.Quantity * entity.UnitPrice;
                                if (taxInvoice.ExchangeRate != 1) {
                                    entity.ProductName = entity.ProductName + "\nTỉ giá: " + taxInvoice.ExchangeRate + "";
                                }
                                if (taxInvoice.TaxPercent != 0) {
                                    entity.ProductName = entity.ProductName + "\nThuế: " + taxInvoice.TaxPercent + "%";
                                }
                                model.Add(entity);
                            }
                            else {
                                entity.Quantity += productDetail.Quantity;
                                entity.Amount = entity.Quantity * entity.UnitPrice;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintInvoice", ex.Message);
            }
            return PartialView("PageTaxInvoiceProductDetail", model.OrderBy(m => m.SetupDate).ToList());
        }

        public ActionResult PrintInvoice_Cu(int invoiceId) {
            var model = new List<PrintInvoiceModel>();
            try {
                if (invoiceId != 0) {
                    using (var vfi = new tammaContext()) {
                        var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
                        var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                        var order = vfi.Orders.FirstOrDefault(o => o.OrderId == export.OrderId);
                        var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
                        if (order.PaymentTermId == null) order.PaymentTermId = 1;
                        var paymentTerm = vfi.PaymentTerms.FirstOrDefault(pt => pt.Id == order.PaymentTermId);
                        //var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == invoice.TaxInvoiceId);
                        foreach (var exportDetail in export.ExportFormTP_KDDetail) {
                            var product = vfi.Products.FirstOrDefault(p => p.ProductId == exportDetail.ProductId);
                            var orderDetail =
                                order.OrderDetails.FirstOrDefault(od => od.ProductId == exportDetail.ProductId);
                            var entity = new PrintInvoiceModel();
                            entity.InvoiceNumber = invoice.InvoiceNumber;
                            entity.Today = DateTime.Today;
                            entity.CustomerName = customer.CompanyName;
                            entity.CustomerInfo = customer.Address;
                            entity.CustomerContact = customer.Address + "\nContact: " + customer.ContactName;
                            entity.CustomerShipTo = customer.Address + "\nPhone: " + customer.Phone + "\nFax: " +
                                                    customer.Fax;
                            entity.OrderNumber = order.OrderNumber;
                            entity.CurrencyCode = order.CurrencyCode;
                            entity.ProductName = "PO: " + orderDetail.PONumber;
                            entity.ProductInfo = "Name:" + product.ProductName + "\nCode: " + product.DesignNo;
                            entity.Quantity = exportDetail.Quality;
                            entity.UM = "pcs";
                            entity.UnitPrice = orderDetail.UnitPrice;
                            entity.Note = invoice.Note;
                            entity.Weight = (exportDetail.Weight ?? 0.0) / 1000;
                            entity.Package = export.TotalBox;
                            entity.PaymentTerm = paymentTerm.TermName;
                            entity.ShiftmentDateString = export.DateTransporter.Value.ToString("dd/MM/yyyy");
                            entity.TaxPercent = invoice.TaxPercent;
                            //entity.TaxInvoice = taxInvoice == null ? "" : taxInvoice.TaxInvoiceList;
                            entity.Amount = exportDetail.Quality * orderDetail.UnitPrice;
                            entity.TaxAmount += (entity.Amount * (((double)entity.TaxPercent) / 100));
                            model.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
                //ModelState.AddModelError("PrintInvoice", ex.Message);
            }
            return PartialView("PageInvoice", model);
        }


        public ActionResult PrintInvoice(int invoiceId, bool? showPrice) {
            var model = new List<PrintInvoiceModel>();
            try {
                if (invoiceId != 0) {
                    using (var vfi = new tammaContext()) {
                        var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
                        if (invoice == null) { return PartialView("PageInvoice", model); }
                        var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                        if (export == null) { return PartialView("PageInvoice", model); }
                        var transaction = vfi.Transactions.FirstOrDefault(x => x.TransactionCode.Equals(export.TransactionCode));
                        if (transaction == null) { return PartialView("PageInvoice", model); }
                        if (showPrice != true) {
                            return PrintPackingList(transaction.TransactionId);
                        }

                        var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == export.CustomerId);
                        var paymentTerm = vfi.PaymentTerms.FirstOrDefault(pt => pt.Id == 1);
                        var isInvoiceDetail = export.ExportFormTP_KDDetail.FirstOrDefault(ed => !ed.InvoiceDetails.Any(id => id.Active));
                        if (isInvoiceDetail != null)
                            throw new AggregateException("Lỗi! Trong phiếu có chi tiết chưa phân đơn hàng!");
                        var info = new WorkGroupInfo();
                        var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.Active);
                        if (workgroup != null) {
                            info = new WorkGroupInfo {
                                Logo = workgroup.ImagePath + "/Logo/" + workgroup.LogoImage,
                                CompanyFullName = workgroup.CompanyFullName,
                                CompanyShortName = workgroup.CompanyShortName,
                                Address = workgroup.Address,
                                TelNumber = "Tel : " + workgroup.TelNumber,
                                FaxNumber = "Fax : " + workgroup.FaxNumber,
                                Email = "Email: " + workgroup.Email,
                                Website = "Website: " + workgroup.Website
                            };
                        }
                        foreach (var exportDetail in export.ExportFormTP_KDDetail) {
                            var entity = model.FirstOrDefault(m => m.ProductId == exportDetail.ProductId);
                            if (entity == null) {
                                entity = new PrintInvoiceModel {
                                    ProductId = exportDetail.ProductId ?? 0,
                                    InvoiceNumber = invoice.InvoiceNumber,
                                    Today = DateTime.Today,
                                    CustomerCode = customer.CustomerCode,
                                    CustomerName = customer.CompanyName,
                                    CustomerInfo = customer.Address,
                                    CustomerContact = customer.Address + "\nContact: " + customer.ContactName,
                                    CustomerShipTo = customer.Address + "\nPhone: " + customer.Phone + "\nFax: " +
                                                     customer.Fax,
                                    ProductInfo =
                                        "Name:" + exportDetail.Product.ProductName + "\nCode: " +
                                        exportDetail.Product.DesignNo,
                                    Quantity =
                                        exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece),
                                    UM = "pcs",
                                    Note = invoice.Note,
                                    Weight = (exportDetail.Weight ?? 0.0) / 1000,
                                    Package = export.TotalBox,
                                    PaymentTerm = paymentTerm.TermName,
                                    ShiftmentDateString = export.DateTransporter.Value.ToString("dd/MM/yyyy"),
                                    TaxPercent = invoice.TaxPercent,
                                    TaxAmount = 0,
                                    TaxInvoice = "",
                                    Info = info
                                };
                                if (entity.Quantity > 0)
                                    model.Add(entity);
                            }
                            else {
                                entity.Quantity +=
                                    exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece);
                                entity.Weight += (exportDetail.Weight ?? 0.0) / 1000;
                            }
                            if (exportDetail.IsInvoiced ?? false) {
                                var taxInvoiceProduct =
                                    vfi.TaxInvoiceProductDetails.FirstOrDefault(
                                        tipd => tipd.ExportDetailId == exportDetail.DetailId && tipd.Active == true);
                                if (taxInvoiceProduct != null) {
                                    if (!entity.TaxInvoice.Contains(taxInvoiceProduct.TaxInvoice.TaxInvoiceList))
                                        entity.TaxInvoice += (taxInvoiceProduct.TaxInvoice.TaxInvoiceList + " ");
                                    entity.TaxPercent = taxInvoiceProduct.TaxInvoice.TaxPercent;
                                }
                            }
                            entity.UnitPrice =
                                exportDetail.InvoiceDetails.FirstOrDefault(id => id.Active).Price.Value;
                            entity.Amount = entity.Quantity * entity.UnitPrice;
                            entity.TaxAmount = (entity.Amount * (((double)entity.TaxPercent) / 100));
                            entity.CurrencyCode =
                                exportDetail.InvoiceDetails.FirstOrDefault(id => id.Active)
                                    .OrderDetail.Order.CurrencyCode;
                            entity.ProductName = "PO:\n";
                            foreach (var invoiceDetail in
                                exportDetail.InvoiceDetails.Where(id => id.Active && id.Piece > 0)) {
                                if (!string.IsNullOrWhiteSpace(invoiceDetail.OrderDetail.PONumber))
                                    entity.ProductName += (invoiceDetail.OrderDetail.PONumber + "\n");
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            if (showPrice == true)
                return PartialView("PageInvoice", model);
            else
                return PartialView("PageInvoice_ExportForm", model);
        }

        public PartialViewResult PrintPackingList(long transactionId) {
            var model = new List<PrintInvoiceModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);
                    if (transaction == null) { return PartialView("PageInvoice_ExportForm", model); }
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.TransactionCode.Equals(transaction.TransactionCode));
                    if (export == null) { return PartialView("PageInvoice_ExportForm", model); }

                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == export.CustomerId);
                    var paymentTerm = vfi.PaymentTerms.FirstOrDefault(pt => pt.Id == 1);
                    var info = new WorkGroupInfo();
                    var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.Active);
                    if (workgroup != null) {
                        info = new WorkGroupInfo {
                            Logo = workgroup.ImagePath + "/Logo/" + workgroup.LogoImage,
                            CompanyFullName = workgroup.CompanyFullName,
                            CompanyShortName = workgroup.CompanyShortName,
                            Address = workgroup.Address,
                            TelNumber = "Tel : " + workgroup.TelNumber,
                            FaxNumber = "Fax : " + workgroup.FaxNumber,
                            Email = "Email: " + workgroup.Email,
                            Website = "Website: " + workgroup.Website
                        };
                    }
                    foreach (var exportDetail in export.ExportFormTP_KDDetail) {
                        var entity = model.FirstOrDefault(m => m.ProductId == exportDetail.ProductId);
                        if (entity == null) {
                            entity = new PrintInvoiceModel {
                                ProductId = exportDetail.ProductId ?? 0,
                                InvoiceNumber = transaction.TransactionCode,
                                Today = DateTime.Today,
                                CustomerName = customer.CompanyName,
                                CustomerInfo = customer.Address,
                                CustomerContact = customer.Address + "\nContact: " + customer.ContactName,
                                CustomerShipTo = customer.Address + "\nPhone: " + customer.Phone + "\nFax: " +
                                                 customer.Fax,
                                ProductName = exportDetail.Product.ProductCode,
                                ProductInfo =
                                    "Name:" + exportDetail.Product.ProductName +
                                    "\nCode: " + exportDetail.Product.DesignNo,
                                Quantity = exportDetail.Quality,
                                UM = "pcs",
                                Note = "",
                                Weight = (exportDetail.Weight ?? 0.0) / 1000,
                                Package = export.TotalBox,
                                PaymentTerm = paymentTerm.TermName,
                                ShiftmentDateString = export.DateTransporter.Value.ToString("dd/MM/yyyy"),
                                TaxPercent = 0,
                                TaxAmount = 0,
                                TaxInvoice = "",
                                Info = info
                            };
                            if (entity.Quantity > 0)
                                model.Add(entity);
                        }
                        else {
                            entity.Quantity += exportDetail.Quality;
                            entity.Weight += (exportDetail.Weight ?? 0.0) / 1000;
                        }

                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintPackingList", ex.Message);
            }
            return PartialView("PageInvoice_ExportForm", model);
        }

        [GridAction]
        public ActionResult SelectProductInventoryInDay(
            int warehouseView
            , int warehouseReceiptInDay1
            , int warehouseReceiptInDay2
            , int warehouseReceiptInDay3
            , int warehouseReceiptInDay4
            , int warehouseReceiptInDay5
            , int warehouseIssueInDay1
            , int warehouseIssueInDay2
            , int warehouseIssueInDay3
            , int warehouseIssueInDay4
            , int warehouseIssueInDay5
            , string dateView) {
            var model = new List<ProductInventoryInDayModel>();
            var ci = new CultureInfo("vi-VN");

            var monthly = string.IsNullOrWhiteSpace(dateView)
                              ? DateTime.Today
                              : Convert.ToDateTime(dateView, ci);
            using (var vfi = new tammaContext()) {
                var productInventoryPeriods =
                    vfi.ProductInventoryPeriods.Where(
                        pip => pip.PeriodDate <= monthly && pip.WarehouseId == warehouseView);
                //if (customerId != 0) ;
                //    //productInventoryPeriods =
                //    //    productInventoryPeriods.Where(pip => pip.Product.CustomerId == customerId);

                //if (string.IsNullOrWhiteSpace(productName)) ;
                //    //productInventoryPeriods =
                //    //    productInventoryPeriods.Where(
                //    //        pip => pip.Product.ProductCode.ToUpper().Contains(productName.ToUpper()));
                var productInventoryPeriodInDay = productInventoryPeriods.Where(pip => pip.PeriodDate == monthly);

                var lastDay = monthly.AddDays(-1);
                var productIventoryInLast = productInventoryPeriods.Where(pip => pip.PeriodDate <= lastDay);

                var productIdPeriodInDay = productInventoryPeriodInDay.Select(pip => pip.ProductId).Distinct();
                foreach (var productId in productIdPeriodInDay) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == product.CustomerId);
                    var productInvetoryByProduct = productInventoryPeriodInDay.Where(pip => pip.ProductId == productId).ToList();
                    var entity = new ProductInventoryInDayModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        CustomerCode = customer.CustomerCode,
                        FirstTotal =
                            (productIventoryInLast.Where(pip => pip.ProductId == productId)
                                                  .Sum(pip => pip.LastPeriodQuantity)) -
                            (productIventoryInLast.Where(pip => pip.ProductId == productId)
                                                  .Sum(pip => pip.EarlyPeriodQuantity)),

                        Receipt1 = GetTotalQuatity(productInvetoryByProduct, 1, warehouseReceiptInDay1),
                        Receipt2 = GetTotalQuatity(productInvetoryByProduct, 1, warehouseReceiptInDay2),
                        Receipt3 = GetTotalQuatity(productInvetoryByProduct, 1, warehouseReceiptInDay3),
                        Receipt4 = GetTotalQuatity(productInvetoryByProduct, 1, warehouseReceiptInDay4),
                        Receipt5 = GetTotalQuatity(productInvetoryByProduct, 1, warehouseReceiptInDay5),
                        Issue1 = GetTotalQuatity(productInvetoryByProduct, 2, warehouseIssueInDay1),
                        Issue2 = GetTotalQuatity(productInvetoryByProduct, 2, warehouseIssueInDay2),
                        Issue3 = GetTotalQuatity(productInvetoryByProduct, 2, warehouseIssueInDay3),
                        Issue4 = GetTotalQuatity(productInvetoryByProduct, 2, warehouseIssueInDay4),
                        Issue5 = GetTotalQuatity(productInvetoryByProduct, 2, warehouseIssueInDay5),
                        TotalReceipt =
                            productInvetoryByProduct.Where(
                                pr => pr.LastPeriodQuantity > pr.EarlyPeriodQuantity)
                                                    .Sum(pr => pr.Quantity),
                        TotalIssue =
                            productInvetoryByProduct.Where(
                                pr => pr.LastPeriodQuantity < pr.EarlyPeriodQuantity)
                                                    .Sum(pr => pr.Quantity),
                        LastTotal =
                            (productInventoryPeriods.Where(pip => pip.ProductId == productId)
                                                    .Sum(pip => pip.LastPeriodQuantity)) -
                            (productInventoryPeriods.Where(pip => pip.ProductId == productId)
                                                    .Sum(pip => pip.EarlyPeriodQuantity)),
                    };

                    model.Add(entity);
                }

            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }

        public ActionResult GetProductPicture(int productId) {
            var entity = new ProductModel();
            try {
                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (string.IsNullOrWhiteSpace(product.Drawing2D))
                        entity.Upload2D = "askquestion.jpg";
                    else entity.Upload2D = product.Drawing2D;
                    if (string.IsNullOrWhiteSpace(product.DrawingFinish))
                        entity.UploadReal = "askquestion.jpg";
                    else entity.UploadReal = product.DrawingFinish;

                    entity.UploadDate = product.UploadDate != null
                                        ? product.UploadDate.Value.ToString("yyyyMMddhhmmss")
                                        : DateTime.Now.ToString("yyyyMMddhhmmss");
                }
                return PartialView("InventoryCardPicture", entity);
            }
            catch (Exception) {
                return null;
            }

        }

        [GridAction]
        public ActionResult SelectProductInventoryForCustomer(int productId) {
            var model = new List<SoLieuTongHopSanPham>();
            if (productId == 0)
                return View(new GridModel(model));
            try {
                var customerIds = MyUtilities.Sales.GetCustomerAccessList(HttpContext.User.Identity.Name);
                using (var vfi = new tammaContext()) {

                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId && customerIds.Contains(p.CustomerId));
                    if (product == null) throw new AggregateException("Không tìm thấy SP ! Vui lòng chọn chính xác SP muốn tìm!");
                    //var production2Ids = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    model = GetProductInventoryInfo(productId, 0, "All");
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductInventoryForCustomer", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<SoLieuTongHopSanPham> GetProductInventoryInfo(int productId, int quantity, string lotNumber) {
            var model = new List<SoLieuTongHopSanPham>();
            if (productId == 0)
                return model;
            try {
                using (var vfi = new tammaContext()) {

                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null) throw new AggregateException("Không tìm thấy SP ! Vui lòng chọn chính xác SP muốn tìm!");
                    var productInvs = (from pi in vfi.ProductInventories
                                       where pi.ProductId == productId
                                       select new {
                                           pi.ProductId,
                                           pi.WarehouseId,
                                           TotalQty = pi.TotalQty,
                                           pi.LotNumber
                                       }).ToList();
                    if (!string.IsNullOrWhiteSpace(lotNumber) && !lotNumber.ToUpper().Equals("ALL")) {
                        productInvs = productInvs.Where(pi => pi.LotNumber.Contains(lotNumber)).ToList();
                    }
                    var entity = new SoLieuTongHopSanPham {
                        ProductCode = "Tồn kho(pcs)",
                        SanXuat1 = product.Productivity ?? 0,
                    };
                    foreach (var productInv in productInvs) {
                        if (productInv.WarehouseId == MyUtilities.Warehouse.Cnc) {
                            entity.TonKhoSX2CNC += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.Production2) {
                            entity.TonKhoSX2SX2 += productInv.TotalQty;
                        }
                        else if (MyUtilities.Warehouse.GetWarehouseIdProduction2_PROCESS().Contains(productInv.WarehouseId)) {
                            entity.TonKhoSX2SX2B += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.HeatTreatment) {
                            entity.TonKhoNhietLuyen += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment) {
                            entity.TonKhoRungBong += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.WaitingPlating) {
                            entity.TonKhoGCN += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.Plating) {
                            entity.TonKhoNCU += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.PlatingTest) {
                            entity.TonKhoNCUKiemTra += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.QcA) {
                            entity.TonKhoQCA += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.QcB) {
                            entity.TonKhoQCB += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.QcC) {
                            entity.TonKhoQCC += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.Processing ||
                                 productInv.WarehouseId == MyUtilities.Warehouse.Processing2) {
                            entity.TonKhoCXL += productInv.TotalQty;
                        }
                        else if (MyUtilities.Warehouse.GetWarehouseId_ReProcessing().Contains(productInv.WarehouseId)) {
                            entity.ReProcessing += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.Finish) {
                            entity.TonKhoTPA += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.Packing) {
                            entity.Packing += productInv.TotalQty;
                        }
                        else if (productInv.WarehouseId == MyUtilities.Warehouse.Tranfer) {
                            entity.Tranfer += productInv.TotalQty;
                        }
                    }
                    model.Add(entity);
                    var entity2 = new SoLieuTongHopSanPham {
                        ProductCode = "TL(g/pc)",
                        SanXuat1 = product.ProductionWeight ?? 0,
                        TonKhoSX2CNC = product.CncWeight ?? 0,
                        TonKhoSX2SX2 = product.Production2Weight ?? 0,
                        TonKhoSX2SX2B = product.Production2Weight ?? 0,
                        TonKhoNhietLuyen = product.HeatTreatmentWeight ?? 0,
                        TonKhoRungBong = product.SurfaceTreatmentWeight ?? 0,
                        TonKhoGCN = product.WaitingPlatingWeight ?? 0,
                        TonKhoNCU = product.PlatingWeight ?? 0,
                        TonKhoNCUKiemTra = product.PlatingWeight ?? 0,
                        TonKhoQCA = product.QcWeight ?? 0,
                        TonKhoQCB = product.QcWeight ?? 0,
                        TonKhoQCC = product.QcWeight ?? 0,
                        TonKhoCXL = product.QcWeight ?? 0,
                        ReProcessing = product.QcWeight ?? 0,
                        TonKhoTPA = product.QcWeight ?? 0,
                        TonKhoTPB = product.QcWeight ?? 0,
                        Packing = product.QcWeight ?? 0,
                        Tranfer = product.QcWeight ?? 0,
                    };
                    model.Add(entity2);
                    var entity3 = new SoLieuTongHopSanPham {
                        ProductCode = "Tồn kho(g)",
                        SanXuat1 = entity.SanXuat1 * entity2.SanXuat1,
                        TonKhoSX2CNC = entity.TonKhoSX2CNC * entity2.TonKhoSX2CNC,
                        TonKhoSX2SX2 = entity.TonKhoSX2SX2 * entity2.TonKhoSX2SX2,
                        TonKhoSX2SX2B = entity.TonKhoSX2SX2B * entity2.TonKhoSX2SX2B,
                        TonKhoNhietLuyen = entity.TonKhoNhietLuyen * entity2.TonKhoNhietLuyen,
                        TonKhoRungBong = entity.TonKhoRungBong * entity2.TonKhoRungBong,
                        TonKhoGCN = entity.TonKhoGCN * entity2.TonKhoGCN,
                        TonKhoNCU = entity.TonKhoNCU * entity2.TonKhoNCU,
                        TonKhoNCUKiemTra = entity.TonKhoNCUKiemTra * entity2.TonKhoNCUKiemTra,
                        TonKhoQCA = entity.TonKhoQCA * entity2.TonKhoQCA,
                        TonKhoQCB = entity.TonKhoQCB * entity2.TonKhoQCB,
                        TonKhoQCC = entity.TonKhoQCC * entity2.TonKhoQCC,
                        TonKhoCXL = entity.TonKhoCXL * entity2.TonKhoCXL,
                        ReProcessing = entity.ReProcessing * entity2.ReProcessing,
                        TonKhoTPA = entity.TonKhoTPA * entity2.TonKhoTPA,
                        TonKhoTPB = entity.TonKhoTPB * entity2.TonKhoTPB,
                        TonKhoPP = entity.TonKhoTPB * entity2.TonKhoTPB,
                        Packing = entity.Packing * entity2.Packing,
                        Tranfer = entity.Tranfer * entity2.Tranfer,
                    };
                    model.Add(entity3);
                    var processes =
                        vfi.ProductionProcesses.Where(pp => pp.ProductId == productId && pp.IsNecessary)
                           .OrderBy(pp => pp.ProcessIndex);
                    var entity4 = new SoLieuTongHopSanPham {
                        ProductCode = "Thời gian(d)",
                    };
                    foreach (var process in processes) {
                        if (process.WarehouseId == MyUtilities.Warehouse.Production1) {
                            if (entity.SanXuat1 == 0)
                                entity4.SanXuat1 = 0;
                            else if (quantity != 0)
                                entity4.SanXuat1 =
                                    MyUtilities.Function.RoundUp(quantity / (72000 / entity.SanXuat1), 0);
                            else
                                entity4.SanXuat1 = 1;
                        }
                        else if (process.WarehouseId == MyUtilities.Warehouse.Cnc) {
                            entity4.TonKhoSX2CNC = 1;
                        }
                        else if (process.WarehouseId == MyUtilities.Warehouse.Production2) {
                            var sections =
                                vfi.ProductionSections.Where(ps => ps.ProductId == productId && ps.Active);
                            if (sections.Any()) {
                                if (quantity != 0) {
                                    var smallestSection = sections.OrderBy(ps => ps.Productivity).FirstOrDefault();
                                    entity4.TonKhoSX2SX2 =
                                        MyUtilities.Function.RoundUp(quantity / (28800 / smallestSection.Productivity),
                                            0);
                                }
                                entity4.TonKhoSX2SX2 += (sections.Count());
                            }
                            else {
                                entity4.TonKhoSX2SX2 = 1;
                            }
                        }
                        else if (process.WarehouseId == MyUtilities.Warehouse.HeatTreatment) {
                            entity4.TonKhoNhietLuyen = 1;
                        }
                        else if (process.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment) {
                            entity4.TonKhoRungBong = 1;
                        }
                        else if (process.WarehouseId == MyUtilities.Warehouse.WaitingPlating) {
                            var platings = vfi.ProductionPlatings.Where(ps => ps.ProductId == productId && ps.Active);
                            if (platings.Any()) {
                                entity4.TonKhoGCN = platings.Sum(ps => ps.PlatingDay);
                            }
                            else {
                                entity4.TonKhoGCN = 1;
                            }
                        }
                        else if (process.WarehouseId == MyUtilities.Warehouse.QcA ||
                                 process.WarehouseId == MyUtilities.Warehouse.QcB) {
                            entity4.TonKhoQCA = 3;
                        }
                        else {
                            entity4.Packing = 1;
                        }
                    }
                    model.Add(entity4);

                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("GetProductInventoryInfo", ex.Message);
            }
            return model;

        }
        [GridAction]
        public ActionResult SelectProductInventoryNew(int productId, int quantity, string lotNumber) {
            var model = new List<SoLieuTongHopSanPham>();
            if (productId == 0)
                return View(new GridModel(model));
            try {
                model = GetProductInventoryInfo(productId, quantity, lotNumber);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductInventoryNew", ex.Message);
            }
            return View(new GridModel(model));
        }

        private double GetTotalQuatity(List<Vfi.Models.ProductInventoryPeriod> list, int style, int warehouseId) {
            double total = 0.0;
            if (warehouseId == 0) return total;
            using (var vfi = new tammaContext()) {
                // 1 nhap ; 2 xuat
                switch (style) {
                    case 1:
                        foreach (var productInventoryPeriod in list) {
                            var transaction =
                                vfi.Transactions.FirstOrDefault(
                                    pip => pip.TransactionId == productInventoryPeriod.TransactionId);
                            if (transaction.WarehouseIssueId == warehouseId)
                                total += (productInventoryPeriod.Quantity);
                        }
                        break;
                    case 2:
                        foreach (var productInventoryPeriod in list) {
                            var transaction =
                                vfi.Transactions.FirstOrDefault(
                                    pip => pip.TransactionId == productInventoryPeriod.TransactionId);
                            if (transaction.WarehouseReceiptId == warehouseId)
                                total += (productInventoryPeriod.Quantity);
                        }
                        break;
                }
            }
            return total;
        }

        [GridAction]
        public ActionResult SelectProductInInvoice(int invoiceId) {
            var model = new List<ExportFormTP_KDDetailsModel>();
            try {
                if (invoiceId == 0)
                    return View(new GridModel(model));
                using (var vfi = new tammaContext()) {
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
                    if (invoice == null)
                        return View(new GridModel());
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                    //var orderNoteDetails =
                    //    vfi.OrderNoteDetails.Where(
                    //        ond =>
                    //        ond.OrderNote.ExportId == export.ExportId && ond.OrderNote.NoteType == 1 &&
                    //        ond.OrderNote.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
                    //var orderNotes = vfi.OrderNotes.Where(on => on.ExportId == export.ExportId && on.NoteType == 1);
                    if (export == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu xuất!");
                    foreach (var exportDetail in export.ExportFormTP_KDDetail) {
                        var entity = new ExportFormTP_KDDetailsModel {
                            ProductCode = exportDetail.Product.ProductCode,
                            ProductId = exportDetail.Product.ProductId,
                            SentNumber = (exportDetail.Quality),
                            SentWeight = (exportDetail.Weight ?? 0),
                            StatusInvoice = (exportDetail.IsInvoiced ?? false) ? "Đã xuất" : "Chưa xuất",
                        };
                        if (exportDetail.InvoiceDetails.Count > 0)
                            entity.SentNumber = exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece);

                        model.Add(entity);

                    }
                    return View(new GridModel(model.OrderBy(m => m.ProductCode)));
                }
            }
            catch (Exception) {
                return View(new GridModel(model));
            }
            //return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectProductInOrderNote(
            int noteId) {
            var model = new List<ExportFormTP_KDDetailsModel>();
            try {
                if (noteId == 0)
                    return View(new GridModel(model));
                using (var vfi = new tammaContext()) {
                    var orderNote = vfi.OrderNotes.FirstOrDefault(on => on.NoteId == noteId);
                    if (orderNote == null)
                        return View(new GridModel());
                    foreach (var detail in orderNote.OrderNoteDetails) {
                        var product = vfi.Products.FirstOrDefault(p => p.ProductId == detail.ProductId);
                        var entity = new ExportFormTP_KDDetailsModel {
                            ProductCode = product.ProductCode,
                            ProductId = product.ProductId,
                            SentNumber = (detail.Quantity ?? 0),
                            SentWeight = (detail.Weight ?? 0),
                        };
                        model.Add(entity);
                    }
                    return View(new GridModel(model));
                }
            }
            catch (Exception) {
                return View(new GridModel(model));
            }
        }


        [GridAction]
        public ActionResult InputProductInInvoice(string productIds, int? invoiceId, int? type) {
            if (string.IsNullOrWhiteSpace(productIds))
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));

            int[] checkedRecords;
            try {

                //checkedRecords = Convert.ToInt32(ids.Split(':'));
                //checkedRecords = Convert.ToInt32(ids.Split(':'));
                var lst = productIds.Split(':');
                checkedRecords = new int[lst.Count()];
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords[i] = Convert.ToInt32(lst.ElementAt(i));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            }

            if (checkedRecords.Count() <= 0)
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            if (checkedRecords[0] == 0) {
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            }
            if (invoiceId == 0 || invoiceId == null)
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            if (type == 0 || type == null)
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));

            var model = new List<ExportFormTP_KDDetailsModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                    var transaction =
                        vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(export.TransactionCode));
                    var transactionDetails =
                        transaction.TransactionDetails.Where(td => checkedRecords.Contains(td.ReferenceId.Value));
                    //var orderNotes =
                    //    vfi.OrderNotes.Where(on => on.ExportId == export.ExportId && on.NoteType == 1);
                    foreach (var detail in transactionDetails) {
                        if (detail.ProductInvId == null) continue;
                        var exportDetail =
                            vfi.ExportFormTP_KDDetail.FirstOrDefault(
                                ed => ed.ProductId == detail.ReferenceId && ed.ExportId == export.ExportId);
                        if (type == 1 && (exportDetail.IsInvoiced ?? false))
                            throw new AggregateException("Sản phẩm đã xuất hóa đơn không thể trả hàng");
                        var entity = new ExportFormTP_KDDetailsModel {
                            DetailId = exportDetail.DetailId,
                            ProductId = detail.ReferenceId.Value,
                            ProductCode = exportDetail.Product.ProductCode,
                            SentNumber = detail.Quantity,
                            SentWeight = detail.QuantityKg ?? 0.0,
                            IsInvoiced = exportDetail.IsInvoiced ?? false,
                            ProductInvId = detail.ProductInvId.Value,
                            TransactionDetailId = detail.TransactionDetailId,
                            LotNumber = detail.ProductInventory.LotNumber,
                        };
                        var inventory = vfi.ProductInventories.FirstOrDefault(
                            pi => pi.ProductId == exportDetail.ProductId && pi.WarehouseId == MyUtilities.Warehouse.Finish);
                        entity.TotalQuantity = inventory != null ? inventory.TotalQty : 0;
                        //entity.SentNumber = exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece);
                        //var orderNotesByProductId =
                        //    orderNotes.Where(
                        //        on =>
                        //        on.OrderNoteDetails.FirstOrDefault(ond => ond.ProductId == product.ProductId) != null &&
                        //        on.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);

                        //if (orderNotes.Any())
                        //{
                        //    foreach (var orderNote in orderNotesByProductId)
                        //    {
                        //        //var transaction =
                        //        //    vfi.Transactions.FirstOrDefault(
                        //        //        t =>
                        //        //        t.TransactionId == orderNote.TransactionId &&
                        //        //        t.Status == (byte) MyUtilities.Transaction.Status.Approved);
                        //        //if (transaction != null)
                        //        //{
                        //        var orderNoteDetail =
                        //            orderNote.OrderNoteDetails.FirstOrDefault(
                        //                ond => ond.ProductId == product.ProductId);
                        //        entity.SentNumber -= orderNoteDetail.Quantity.Value;
                        //        //}
                        //    }
                        //}
                        model.Add(entity);
                    }
                }
                return View(new GridModel(model.OrderBy(m => m.ProductCode)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("InvoiceDetail", ex.Message);
            }
            return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
        }

        [GridAction]
        public ActionResult InputProductInOrderNote(string productIds, int? noteId) {
            if (string.IsNullOrWhiteSpace(productIds))
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));

            int[] checkedRecords;
            try {
                //checkedRecords = Convert.ToInt32(ids.Split(':'));
                //checkedRecords = Convert.ToInt32(ids.Split(':'));
                var lst = productIds.Split(':');
                checkedRecords = new int[lst.Count()];
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords[i] = Convert.ToInt32(lst.ElementAt(i));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            }

            if (checkedRecords.Count() <= 0)
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            if (checkedRecords[0] == 0) {
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));
            }
            if (noteId == 0 || noteId == null)
                return View(new GridModel(new List<ExportFormTP_KDDetailsModel>()));

            var model = new List<ExportFormTP_KDDetailsModel>();
            using (var vfi = new tammaContext()) {
                var entityProducts = vfi.Products.Where(f => checkedRecords.Contains(f.ProductId));
                var orderNote = vfi.OrderNotes.FirstOrDefault(on => on.NoteId == noteId);
                foreach (var product in entityProducts) {
                    var noteDetail = orderNote.OrderNoteDetails.FirstOrDefault(ond => ond.ProductId == product.ProductId);
                    var entity = new ExportFormTP_KDDetailsModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        SentNumber = noteDetail.Quantity ?? 0.0,
                        SentWeight = noteDetail.Weight ?? 0.0,
                    };
                    var inventory = vfi.ProductInventories.FirstOrDefault(
                        pi => pi.ProductId == product.ProductId && pi.WarehouseId == MyUtilities.Warehouse.Finish);
                    entity.TotalQuantity = inventory != null ? inventory.TotalQty : 0.0;
                    model.Add(entity);
                }
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        public ActionResult PrintOrderNote(int noteId) {
            var model = new List<PrintOrderNoteModel>();
            if (noteId != 0) {
                using (var vfi = new tammaContext()) {
                    var orderNote = vfi.OrderNotes.FirstOrDefault(on => on.NoteId == noteId);
                    var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == orderNote.TransactionId);
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == orderNote.InvoiceId);
                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == invoice.OrderId);
                    foreach (var detail in transaction.TransactionDetails) {
                        var product = vfi.Products.FirstOrDefault(p => p.ProductId == detail.ReferenceId);
                        var entity = new PrintOrderNoteModel {
                            DateCreateString = orderNote.CreatedDate.ToString("dd/MM/yyyy"),
                            CustomerCode = invoice.Customer.CustomerCode + " " + invoice.Customer.CustomerName,
                            ModifiedDateString = orderNote.ModifiedDate.ToString("dd/MM/yyyy"),
                            Note = detail.Note,
                            Weight = detail.Quantity * (product.QcWeight ?? 0),
                            Quality = detail.Quantity,
                            StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                            //EmployeeName = order.Employee.EmployeeName,
                            Title = "PHIẾU " + (orderNote.NoteType == 1 ? "TRẢ HÀNG" : "ĐỔI HÀNG"),
                            NoteNumber = orderNote.NoteNumber,
                            ModifiedUser = detail.ModifiedUser,
                            //OrderNumber = order.OrderNumber,
                            ProductCode = product.ProductCode,
                        };
                        model.Add(entity);
                    }
                }
            }
            return PartialView("PageOrderNoteForm", model);
        }

        [HttpPost]
        public ActionResult PrintTransactionMaterial(int transactionId) {
            var model = new List<PrintTransactionMaterialModel>();
            using (var vfi = new tammaContext()) {
                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                if (transaction != null) {
                    var info = new WorkGroupInfo();
                    var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.Active);
                    if (workgroup != null) {
                        info = new WorkGroupInfo {
                            Logo = workgroup.ImagePath + "/Logo/" + workgroup.LogoImage,
                            //CompanyFullName = workgroup.CompanyFullName,
                            //CompanyShortName = workgroup.CompanyShortName,
                            //Address = workgroup.Address,
                            //TelNumber = "Tel : " + workgroup.TelNumber,
                            //FaxNumber = "Fax : " + workgroup.FaxNumber,
                            //Email = "Email: " + workgroup.Email,
                            //Website = "Website: " + workgroup.Website
                        };
                    }
                    var transactionDetails =
                                vfi.TransactionDetails.Where(td => td.TransactionId == transactionId);
                    if (transaction.EoI.Equals("0")) {
                        var importPo = vfi.ImportPurchaseOrders.FirstOrDefault(i => i.TransactionId == transactionId);
                        if (importPo == null) throw new AggregateException("Lỗi! Không tìm thấy phiếu nhập mua nguyên liệu");
                        foreach (var transactionDetail in transactionDetails) {
                            var importDetail =
                                importPo.ImportPurchaseOrderDetails.FirstOrDefault(
                                    id => id.MaterialId == transactionDetail.ReferenceId &&
                                    id.PoDetailId == transactionDetail.PoDetailId);
                            var purchaseDetail =
                                vfi.PurchaseOrderDetails.FirstOrDefault(
                                    pod => pod.PurchaseOrderDetailId == transactionDetail.PoDetailId);
                            if (purchaseDetail == null) {
                                purchaseDetail =
                                    vfi.PurchaseOrderDetails.FirstOrDefault(
                                        pod => pod.PurchaseOrderId == importPo.PurchaseOrderId
                                               && pod.ReferenceId == transactionDetail.ReferenceId);
                            }
                            var entity = new PrintTransactionMaterialModel {
                                MaterialCode = transactionDetail.Material.MaterialCode,
                                MaterialName = importDetail.Material.MaterialName,
                                MaterialDesignNo =
                                    MyUtilities.Material.GetMaterialDesignNo(transactionDetail.Material,
                                        importDetail.Length),
                                VendorCode = importDetail.Vendor.VendorCode,
                                VendorName = importDetail.Vendor.VendorName,
                                Quantity = importPo.PurchaseOrderId == null
                                    ? transactionDetail.Quantity
                                    : importDetail.Quantity,
                                QuantityKg = importPo.PurchaseOrderId == null
                                    ? transactionDetail.QuantityKg
                                    : importDetail.QuantityKg,
                                LotNumber = importDetail.LotNumber,
                                StoreCode = importDetail.StoreCode,
                                Note = transactionDetail.Note,
                                PONumber = importPo.PurchaseOrderId == null
                                    ? ""
                                    : importPo.PurchaseOrder.RevisionNumber,
                                UnitPrice = importDetail.UnitPrice,
                                UnitWeight = importDetail.UnitWeight,
                                Currency = importPo.PurchaseOrderId == null
                                    ? ""
                                    : importPo.PurchaseOrder.CurrencyCode,
                                ExchangeRate = importPo.ExchangeRate,
                                CreatedDate = transaction.CreatedDate,
                                StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                                TransactionCode = transaction.TransactionCode,
                                ModifiedUser = transaction.ModifiedUser,
                                ModifiedDate = transaction.ModifiedDate,
                                PurchasingSignature = importPo.PurchasingSignature,
                                Info = info,
                            };
                            if (purchaseDetail != null) {
                                if (purchaseDetail.Unit.Contains("Kg")) {
                                    entity.Price = entity.QuantityKg.Value * entity.UnitPrice;
                                }
                                else {
                                    entity.Price = entity.Quantity.Value * entity.UnitPrice;
                                }
                            }
                            else {
                                entity.Price = entity.QuantityKg.Value * entity.UnitPrice;
                            }
                            var totalInvs =
                                vfi.MaterialInventoryPeriods.Where(
                                    mip =>
                                        mip.MaterialId == transactionDetail.ReferenceId &&
                                        mip.PeriodDate <= transaction.CreatedDate);
                            if (totalInvs.Any()) {
                                entity.TotalInv =
                                    totalInvs.Sum(
                                        mip =>
                                            (mip.LastPeriodQuantity - mip.EarlyPeriodQuantity) *
                                            mip.MaterialInventory.UnitWeight);
                            }
                            if (transaction.Status == (byte)MyUtilities.Transaction.Status.Open) {
                                entity.TotalInv += entity.QuantityKg ?? 0;
                            }
                            model.Add(entity);
                        }
                        //return PartialView("PageMaterialLotCard", model);
                        return PartialView("PageImportMaterial", model.OrderBy(x => x.MachineName)
                                                                    .ThenBy(x => x.MaterialDesignNo)
                                                                    .ThenBy(x => x.LotNumber
                                            ).ToList());
                    }
                    //xuat
                    else if (transaction.EoI.Equals("1")) {
                        var exportMaterial =
                            vfi.ExportMaterials.FirstOrDefault(em => em.TransactionId == transaction.TransactionId);
                        foreach (var exportDetail in exportMaterial.ExportMaterialDetails) {
                            var materialInv =
                                vfi.MaterialInventories.FirstOrDefault(
                                    mi => mi.MaterialInventoryId == exportDetail.MaterialInvId);
                            //var transactionDetail =
                            //    vfi.TransactionDetails.FirstOrDefault(
                            //        td =>
                            //        td.TransactionId == transaction.TransactionId &&
                            //        td.ReferenceId == materialInv.MaterialId);
                            var entity = new PrintTransactionMaterialModel();
                            entity.Info = info;
                            entity.MaterialCode = materialInv.Material.MaterialCode;
                            entity.MaterialName = materialInv.Material.MaterialName;
                            entity.MaterialDesignNo = MyUtilities.Material.GetMaterialDesignNo(materialInv.Material, materialInv.Length);
                            if (exportDetail.MachineId != null)
                                entity.MachineName = exportDetail.Machine.MachineName;
                            else {
                                entity.MachineName = "";
                                if (transaction.IsInternal == true) {
                                    entity.Note = "Xuất NB :" + exportDetail.TransactionDetail.Note;
                                }
                                else {
                                    entity.Note = "Hủy :" + exportDetail.TransactionDetail.Note;
                                }
                            }
                            entity.VendorCode = materialInv.Vendor.VendorCode;
                            entity.VendorName = materialInv.Vendor.VendorName;
                            entity.Quantity = exportDetail.Quantity;
                            entity.LotNumber = materialInv.LotNumber;
                            //entity.UnitPrice = transactionDetail.Price ?? 0;
                            //entity.AvailableQuantity = materialInv.TotalQty;
                            entity.UnitPrice = materialInv.UnitPrice;
                            entity.UnitWeight = materialInv.UnitWeight;
                            entity.QuantityKg = entity.UnitWeight * entity.Quantity;
                            entity.CreatedDate = transaction.CreatedDate;
                            entity.StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status);
                            entity.TransactionCode = transaction.TransactionCode;
                            //if (exportDetail.MachineId != null)
                            //    entity.Note = "Máy: " + exportDetail.Machine.MachineName;
                            model.Add(entity);
                        }
                        return PartialView("PageExportMaterial", model.OrderBy(x => x.MachineName)
                                                                    .ThenBy(x => x.MaterialDesignNo)
                                                                    .ThenBy(x => x.LotNumber
                                                                    ).ToList());
                    }
                }
            }
            return PartialView(null);
        }

        [HttpPost]
        public ActionResult PrintTransactionMaterialCard(int transactionId) {
            var model = new List<PrintTransactionMaterialModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                    if (transaction != null) {
                        var transactionDetails =
                                    vfi.TransactionDetails.Where(td => td.TransactionId == transactionId);
                        if (transaction.EoI.Equals("0")) {
                            var importPo = vfi.ImportPurchaseOrders.FirstOrDefault(i => i.TransactionId == transactionId);
                            if (importPo == null) throw new AggregateException("Lỗi phiếu nhập nguyên liệu");
                            foreach (var transactionDetail in transactionDetails) {
                                var importDetail =
                                    importPo.ImportPurchaseOrderDetails.FirstOrDefault(
                                        id => id.MaterialId == transactionDetail.ReferenceId);
                                var materialByLot =
                                    vfi.MaterialInventories.FirstOrDefault(
                                        mi =>
                                            mi.LotNumber.Equals(importDetail.LotNumber) &&
                                            mi.MaterialId == transactionDetail.ReferenceId &&
                                            mi.VendorId == importDetail.VendorId &&
                                            mi.Length == importDetail.Length);
                                var entity = new PrintTransactionMaterialModel {
                                    MaterialCode = transactionDetail.Material.MaterialCode,
                                    MaterialName = importDetail.Material.MaterialName,
                                    MaterialDesignNo =
                                        MyUtilities.Material.GetMaterialDesignNo(transactionDetail.Material,
                                            importDetail.Length),
                                    VendorCode = importDetail.Vendor.VendorCode,
                                    VendorName = importDetail.Vendor.VendorName,
                                    Quantity = importPo.PurchaseOrderId == null
                                        ? transactionDetail.Quantity
                                        : importDetail.Quantity,
                                    QuantityKg = importPo.PurchaseOrderId == null
                                        ? transactionDetail.QuantityKg
                                        : importDetail.QuantityKg,
                                    LotNumber = importDetail.LotNumber,
                                    StoreCode = materialByLot == null
                                        ? importDetail.StoreCode
                                        : materialByLot.StoreCode,
                                    Note = transactionDetail.Note,
                                    PONumber = importPo.PurchaseOrderId == null
                                        ? ""
                                        : importPo.PurchaseOrder.RevisionNumber,
                                    UnitPrice = importDetail.UnitPrice,
                                    UnitWeight = importDetail.UnitWeight,
                                    Currency = importPo.PurchaseOrderId == null
                                        ? ""
                                        : importPo.PurchaseOrder.CurrencyCode,
                                    ExchangeRate = importPo.ExchangeRate,
                                    CreatedDate = transaction.CreatedDate,
                                    StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                                    TransactionCode = transaction.TransactionCode,
                                    ModifiedUser = transaction.ModifiedUser,
                                    ModifiedDate = transaction.ModifiedDate,
                                    PurchasingSignature = importPo.PurchasingSignature,
                                };
                                entity.Price = entity.QuantityKg.Value * entity.UnitPrice;
                                entity.ModifiedDate = transaction.CreatedDate.AddDays(2);
                                model.Add(entity);
                            }
                            return PartialView("PageMaterialLotCard", model);
                            //return PartialView("PageImportMaterial", model);
                        }
                    }
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return PartialView(null);
        }
        [GridAction]
        public ActionResult SelectMaterialDailyPeriod(int materialType, string materialCode, string toDate) {
            var ci = new CultureInfo("vi-VN");

            var monthly = string.IsNullOrWhiteSpace(toDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(toDate, ci);
            monthly = new DateTime(monthly.Year, monthly.Month, monthly.Day, 23, 59, 59);
            var earlyDate = monthly.AddDays(-1);
            var model = new List<MaterialInventoryPeriodModel>();
            var materialIds = new List<int>();
            try {
                using (var vfi = new tammaContext()) {
                    materialIds = vfi.Materials.Where(m => m.Active).Select(m => m.MaterialId).ToList();
                    if (materialType != 0) {
                        materialIds =
                            vfi.Materials.Where(m => m.MaterialTypeId == materialType)
                               .Select(m => m.MaterialId)
                               .ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(materialCode)) {
                        var materials =
                            vfi.Materials.Where(m => m.MaterialCode.Contains(materialCode)).Select(m => m.MaterialId);
                        materialIds = materialIds.Intersect(materials).ToList();
                    }
                    //var assginMaterials =
                    //    vfi.ExportMaterials.Where(
                    //        em =>
                    //        em.ExportDate.Value.Day == monthly.Day &&
                    //        em.ExportDate.Value.Month == monthly.Month &&
                    //        em.ExportDate.Value.Year == monthly.Year);
                    var materialIdsExport =
                        vfi.ExportMaterialDetails.Where(
                            emd =>
                            emd.ExportMaterial.ExportDate.Day == monthly.Day &&
                            emd.ExportMaterial.ExportDate.Month == monthly.Month &&
                            emd.ExportMaterial.ExportDate.Year == monthly.Year &&
                            emd.ExportMaterial.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
                    materialIds = materialIds.Intersect(materialIdsExport.Select(ed => ed.MaterialId).ToList()).ToList();
                    if (!materialIds.Any())
                        return View(new GridModel(model));

                    var materialLastPeriods =
                        vfi.MaterialInventoryPeriods.Where(
                            mip =>
                            materialIds.Contains(mip.MaterialId) &&
                            mip.PeriodDate < monthly);
                    var materialEarlyPeriods = materialLastPeriods.Where(mip => mip.PeriodDate < earlyDate);

                    var materialInDayPeriods =
                        materialLastPeriods.Where(
                            mip =>
                            mip.PeriodDay == monthly.Day &&
                            mip.PeriodMonth == monthly.Month &&
                            mip.PeriodYear == monthly.Year);
                    foreach (var materialId in materialIds) {
                        var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == materialId);
                        var materialEarlyPeriodsById = materialEarlyPeriods.Where(mp => mp.MaterialId == materialId);
                        var materialInDayPeriodsById = materialInDayPeriods.Where(mp => mp.MaterialId == materialId);
                        var materialExportDetails = materialIdsExport.Where(ed => ed.MaterialId == materialId && ed.MachineId != null);
                        var entity = new MaterialInventoryPeriodModel {
                            MaterialId = materialId,
                            MaterialCode = material.MaterialCode,
                            EarlyPeriodQuantity = 0,
                            LastPeriodQuantity = 0,
                            QtyExport = 0,
                            QtyImport = 0,
                            //HowStupidIam = materialId + "@" + toDate,
                            ToDateString = toDate,
                        };
                        entity.EarlyPeriodQuantity = materialEarlyPeriodsById.Sum(mp => mp.LastPeriodQuantity) -
                                                     materialEarlyPeriodsById.Sum(mp => mp.EarlyPeriodQuantity);
                        entity.QtyImport =
                            materialInDayPeriodsById.Where(mp => mp.LastPeriodQuantity > mp.EarlyPeriodQuantity)
                                                    .Sum(mp => mp.Quantity);
                        entity.QtyExport =
                            materialInDayPeriodsById.Where(mp => mp.LastPeriodQuantity < mp.EarlyPeriodQuantity)
                                                    .Sum(mp => mp.Quantity);
                        entity.LastPeriodQuantity = (entity.EarlyPeriodQuantity ?? 0) + (entity.QtyImport ?? 0) -
                                                    (entity.QtyExport ?? 0);
                        entity.MaterialForecasts = Convert.ToInt32(
                            (entity.LastPeriodQuantity ?? 0) / (materialExportDetails.Sum(ed => ed.Quantity)));

                        var materialInvById = vfi.MaterialInventories.Where(mi => mi.MaterialId == materialId);




                        model.Add(entity);
                    }

                    //foreach (var assginMaterial in assginMaterials)
                    //{
                    //    foreach (var detail in assginMaterial.ExportMaterialDetails)
                    //    {

                    //        var entity = model.FirstOrDefault(m => m.MaterialId == detail.MaterialId);
                    //        var materialEarlyPeriodsById = materialEarlyPeriods.Where(mp => mp.MaterialId == detail.MaterialId);
                    //        var materialInDayPeriodsById = materialInDayPeriods.Where(mp => mp.MaterialId == detail.MaterialId);
                    //        if (entity == null)
                    //        {
                    //            entity = new MaterialInventoryPeriodModel
                    //                {
                    //                    MaterialId = detail.MaterialId.Value,
                    //                    MaterialCode = detail.Material.MaterialCode,
                    //                    EarlyPeriodQuantity =0,
                    //                    LastPeriodQuantity =  0,
                    //                    QtyExport =  0,
                    //                    QtyImport = 0,
                    //                };
                    //            entity.EarlyPeriodQuantity = materialEarlyPeriodsById.Sum(mp => mp.LastPeriodQuantity) -
                    //                                         materialEarlyPeriodsById.Sum(mp => mp.EarlyPeriodQuantity);
                    //            entity.QtyExport = detail.Quantity;
                    //            entity.QtyImport =
                    //                materialInDayPeriodsById.Where(mp => mp.LastPeriodQuantity > mp.EarlyPeriodQuantity)
                    //                                        .Sum(mp => mp.Quantity);
                    //            entity.LastPeriodQuantity = entity.EarlyPeriodQuantity + entity.QtyImport -
                    //                                        entity.QtyExport;
                    //            model.Add(entity);
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("materialperiod", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<WorkpieceMaterialPeriodModel> GetWorkpieceMaterialModel(DateTime fromD, DateTime toD) {
            var model = new List<WorkpieceMaterialPeriodModel>();
            var ci = new CultureInfo("vi-VN");
            //var to = string.IsNullOrWhiteSpace(toDate)
            //                  ? DateTime.Today
            //                  : Convert.ToDateTime(toDate, ci);
            //var fromD = new DateTime(to.Year, to.Month, 1).AddDays(-1);
            try {
                using (var vfi = new tammaContext()) {
                    var periods = vfi.WorkpieceMaterialPeriods.Where(wmp => wmp.PeriodDate <= toD
                        && wmp.Type != (int)MyUtilities.Material.WorkpieceType.Defect).ToList();

                    var identityCodes = MaterialIdentityCode.GetMaterialIdentityCodes(0);
                    foreach (var identity in identityCodes) {
                        foreach (var type in MyUtilities.Material.GetMaterialIdentityType()) {
                            var entity = new WorkpieceMaterialPeriodModel {
                                IdentityCode = identity.IdentityCode.Trim(),
                                MaterialTypeName = identity.MaterialTypeShortName,
                                Type = type,
                                Weight = 0,
                                EoI = (int)MyUtilities.Transaction.EoIEnum.Import,
                                IsDestroy = false,
                                ToDate = toD.ToString("dd/MM/yyyy")
                            };
                            var periodByTypes =
                                periods.Where(wmp => wmp.IdentityCode.Equals(identity.IdentityCode) && wmp.Type == type)
                                       .ToList();
                            if (periodByTypes.Any()) {
                                entity.LastQuantity = periodByTypes.Sum(wmp => wmp.LastQuantity - wmp.EarlyQuantity);
                                var periodByDates =
                                      periodByTypes.Where(
                                          wmp =>
                                          wmp.PeriodDate >= fromD &&
                                          wmp.EoI == (byte)MyUtilities.Transaction.EoIEnum.Import).ToList();
                                entity.Import =
                                    periodByDates.Where(wmp => wmp.ImportWorkpieceMaterial.TransactionId != null)
                                                 .Sum(wmp => wmp.Weight);
                            }

                            model.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintWorkpieceMaterial", ex.Message);
            }
            return model;
        }

        List<PrintGroupMaterial> GetMaterialDailyPeriod(int materialType, string materialCode, string monthly) {
            var model = new List<PrintGroupMaterial>();
            var ci = new CultureInfo("vi-VN");
            var reportDate = string.IsNullOrWhiteSpace(monthly)
                              ? DateTime.Today
                              : Convert.ToDateTime(monthly, ci);
            using (var vfi = new tammaContext()) {
                vfi.Configuration.LazyLoadingEnabled = false;
                var importSx1 = vfi.ImportFormSX1.FirstOrDefault(i => i.MaterialUseDate.Day == reportDate.Day
                    && i.MaterialUseDate.Month == reportDate.Month
                    && i.MaterialUseDate.Year == reportDate.Year);
                if (importSx1 == null) throw new AggregateException("Không có gì để báo cáo!");
                //var startMonth = new DateTime(importSx1.ImportDate.Year, importSx1.ImportDate.Month, 1);
                var importSx1Details = (from id in vfi.ImportFormSX1Detail
                                        where
                                            //id.ImportFormSX1.ImportDate <= importSx1.ImportDate &&
                                            //id.ImportFormSX1.ImportDate >= startMonth &&
                                            id.ImportFormSX1.MaterialUseDate.Month == importSx1.MaterialUseDate.Month &&
                                            id.ImportFormSX1.MaterialUseDate.Year == importSx1.MaterialUseDate.Year &&
                                            id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                            id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                              .Transaction.Status ==
                                            (byte)MyUtilities.Transaction.Status.Approved &&
                                            (materialType == 0 || id.MaterialInventory.Material.MaterialTypeId == materialType)
                                        orderby id.ImportFormSX1.MaterialUseDate
                                        select new {
                                            ProductId = id.ProductId,
                                            MachineId = id.MachineId.Value,
                                            id.Machine1.MachineName,
                                            id.MaterialInventory.Material,
                                            MaterialInvId = id.MaterialInvId.Value,
                                            id.Product,
                                            MaterialUse = (id.MaterialUse1) + (id.MaterialUse2),
                                            Production = (id.Number1) + (id.Number2),
                                            Processing = (id.Processing1) + (id.Processing2),
                                            DefectProduct = (id.DefectProduct1) + (id.DefectProduct2),
                                            ProductUnitWeight = id.ProductWeight,
                                            id.ImportFormSX1.MaterialUseDate,
                                            ProductionRate = id.ProductionRate,
                                            MaterialUnitWeight = id.MaterialInvId != null
                                                                    ? id.MaterialInventory.UnitWeight
                                                                    : 0,
                                            DifferenceQuantity =
                                                (id.Number1 + id.Processing1 + id.DefectProduct1 +
                                                 id.Number2 + id.Processing2 + id.DefectProduct2) -
                                                ((id.MaterialUse1 + id.MaterialUse2) * id.ProductionRate),
                                            MaterialLength = id.MaterialInventory.Length,
                                            ProductLength = id.Product.Length ?? 0,
                                            KnifeCut = id.Product.KnifeCut ?? 0,
                                            //ProductionUnitWeight = id.Product.ProductionMaterials.Any(x => x.Active)
                                            //? id.Product.ProductionMaterials.OrderBy(x => x.Priority).FirstOrDefault(x => x.Active).UnitWeightByMaterial
                                            //: 0,
                                        }).ToList();
                var startMonth = new DateTime(reportDate.Year, reportDate.Month, 1);
                var endMonth = reportDate;
                if (importSx1Details.Any()) {
                    startMonth = importSx1Details.Min(x => x.MaterialUseDate);

                    endMonth = importSx1Details.Max(x => x.MaterialUseDate).Date.AddDays(1).AddSeconds(-1);
                }
                var groupMaterials = (from mt in vfi.MaterialTypes
                                      where mt.Active && mt.MaterialClassifiedId == 1
                                      && (materialType == 0 || mt.MaterialTypeId == materialType)
                                      //&& mt.MaterialTypeId == 3
                                      orderby mt.IdentityCode
                                      select new {
                                          mt.MaterialTypeName,
                                          mt.MaterialTypeId,
                                          mt.IdentityCode
                                      }).ToList();
                var groupIds = groupMaterials.Select(g => g.MaterialTypeId).ToList();
                var materials = (from m in vfi.Materials
                                 where m.Active && groupIds.Contains(m.MaterialTypeId)
                                 //&& m.MaterialId == 476
                                 select new {
                                     m.MaterialId,
                                     m.MaterialName,
                                     m.MaterialCode,
                                     m.Shape,
                                     m.InDiameter,
                                     m.OutDiameter,
                                     m.DiameterType,
                                     m.MaterialTypeId
                                 }).ToList();
                if (!string.IsNullOrWhiteSpace(materialCode)) {
                    materials = materials.Where(x => x.MaterialCode.Contains(materialCode)).ToList();
                }
                var materialIds = materials.Select(m => m.MaterialId).ToList();
                var materialInvs = (from mi in vfi.MaterialInventories
                                    where materialIds.Contains(mi.MaterialId)
                                    && (mi.EndDate == null || mi.EndDate >= startMonth)
                                    //&& (mi.ModifiedDate > deathMonth || mi.TotalQty > 0)
                                    //&& mi.MaterialInventoryId == 716
                                    select new {
                                        mi.MaterialId,
                                        mi.MaterialInventoryId,
                                        mi.LotNumber,
                                        mi.Length,
                                        VendorId = mi.VendorId ?? 0,
                                        mi.Vendor.VendorCode,
                                        UnitWeight = mi.UnitWeight,
                                        UnitPrice = mi.UnitPrice,
                                    }).ToList();
                var materialInvIds = materialInvs.Select(mi => mi.MaterialInventoryId).ToList();
                materialIds = materialInvs.Select(m => m.MaterialId).ToList();
                var mip = (from period in vfi.MaterialInventoryPeriods
                           where
                               period.PeriodDate <= endMonth &&
                               materialInvIds.Contains(period.MaterialInventoryId)
                           select new {
                               period.MaterialInventoryId,
                               period.PeriodDate,
                               period.EarlyPeriodQuantity,
                               period.LastPeriodQuantity,
                               period.Quantity,
                               IsDestroyed = period.IsDestroyed ?? false
                           }).ToList();
                var mipStartMonth = (from period in mip
                                     where period.PeriodDate < startMonth
                                     select period).ToList();
                var mipInMonth = (from period in mip
                                  where period.PeriodDate >= startMonth
                                  orderby period.PeriodDate
                                  select period).ToList();
                var mimp = (from period in vfi.MaterialInvOnMachinePeriods
                            where
                                period.PeriodDate <= endMonth
                            select new {
                                period.MaterialInvId,
                                period.PeriodDate,
                                period.EarlyQuantity,
                                period.LastQuantity,
                                period.Quantity,
                            }).ToList();
                var mimpStartMonth = (from period in mimp
                                      where period.PeriodDate < startMonth
                                      select period).ToList();
                var emInMonth = (from em in vfi.ExportMaterialDetails
                                 where
                                     materialInvIds.Contains(em.MaterialInvId) &&
                                     em.ExportMaterial.ExportDate >= startMonth &&
                                     em.ExportMaterial.ExportDate <= endMonth &&
                                     em.ExportMaterial.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved
                                 select new {
                                     em.MachineId,
                                     em.Machine.MachineName,
                                     em.MaterialInvId,
                                     Quantity = em.Quantity,
                                     ExportDate = em.ExportMaterial.ExportDate,
                                 }).ToList();
                var retrieveInMonth = (from ms in vfi.MaterialUseDetails
                                       where ms.MaterialUseInShift.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                             ms.MaterialUseInShift.UsedDate >= startMonth &&
                                             ms.MaterialUseInShift.UsedDate <= endMonth &
                                             ms.MaterialUseInShift.Type == (int)MyUtilities.Material.UseType.SendBack &&
                                             !ms.IsDetroy &&
                                             materialInvIds.Contains(ms.MaterialInvId)
                                       select new {
                                           ms.MaterialInvId,
                                           Quantity = ms.EditQuantity + ms.EditQuantity2,
                                           ms.MaterialUseInShift.UsedDate,
                                       }).ToList();
                var productIds = importSx1Details.Select(id => id.ProductId).Distinct().ToList();
                var warehouseList = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                //var warehouseList = _warehouseController.GetActiveWarehouseIds(new WarehouseConfiguration { CanStock = true });
                var productInvPeriods = (from pip in vfi.ProductInventoryPeriods
                                         where productIds.Contains(pip.ProductId) &&
                                               warehouseList.Contains(pip.WarehouseId)
                                         select new {
                                             pip.ProductId,
                                             PeriodQuantity =
                                         (pip.LastPeriodQuantity - pip.EarlyPeriodQuantity),
                                         }).ToList();
                DateTime fromDate;
                DateTime toDate;
                int quater;
                MyUtilities.Function.GetQuaterDateTime(out fromDate, out  toDate, out quater, reportDate);
                var saleProduct = (from pip in vfi.ProductInventoryPeriods
                                   where productIds.Contains(pip.ProductId) &&
                                         pip.WarehouseId == MyUtilities.Warehouse.Business &&
                                         pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Finish &&
                                         pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Business &&
                                         pip.PeriodDate > fromDate &&
                                         pip.PeriodDate < toDate
                                   select new {
                                       pip.ProductId,
                                       PeriodQuantity =
                                   (pip.LastPeriodQuantity - pip.EarlyPeriodQuantity),
                                   }).ToList();
                var productOrders = (from od in vfi.OrderDetails
                                     where productIds.Contains(od.ProductId) && od.RequiedNumber != 0 &&
                                           (od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess ||
                                           od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting) &&
                                           od.Order.DueDate != null
                                     select new {
                                         od.ProductId,
                                         od.RequiedNumber,
                                     }).ToList();
                var productionMaterials = (from pm in vfi.ProductionMaterials
                                           where productIds.Contains(pm.ProductId)
                                                 && pm.Active
                                           select new {
                                               pm.ProductId,
                                               pm.MaterialId
                                           }).ToList();
                var realProductions = (from rp in vfi.RealProductions
                                       where productIds.Contains(rp.ProductId) &&
                                             rp.TrackUpMachine.Status == (byte)MyUtilities.Transaction.Status.Approved
                                             && rp.TrackUpMachine.RealProductivity != 0
                                             && rp.TrackUpMachine.RealRate != 0
                                       select new {
                                           rp.ProductId,
                                           rp.MachineId,
                                           rp.TrackUpMachine,
                                           rp.TrackUpMachine.MaterialId,
                                           rp.TrackUpMachine.RealProductivity,
                                           rp.TrackUpMachine.RealRate,
                                           rp.TrackUpMachine.Quantity,
                                           Length = rp.Product.Length ?? 0,
                                           rp.TrackUpMachine.KnifeCut,
                                           rp.TrackUpMachine.WorkPiece,
                                           StartDate =
                                       rp.TrackUpMachine.DeliveryDate ??
                                       rp.TrackUpMachine.ForecastDate,
                                       }).ToList();
                var tracks = realProductions.Select(rp => rp.TrackUpMachine).OrderBy(t => t.StartDate).ToList();
                var startTrackDate = tracks.Any() ? tracks.FirstOrDefault().StartDate : reportDate;
                var importProductions = (from i in vfi.ImportFormSX1Detail
                                         where
                                             i.ImportFormSX1.MaterialUseDate >= startTrackDate &&
                                             i.ImportFormSX1.MaterialUseDate <= reportDate &&
                                             i.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                             i.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                             i.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                              .Transaction.Status ==
                                             (byte)MyUtilities.Transaction.Status.Approved &&
                                             productIds.Contains(i.ProductId)
                                         select new {
                                             i.MachineId,
                                             i.ProductId,
                                             i.MaterialInventory.MaterialId,
                                             ProductionMaterialQuantity = i.ProductionRate == 0 ? 0 : (i.Number1 + i.Number2 + i.Processing1 + i.Processing2) / i.ProductionRate,
                                             MaterialUse = (i.MaterialUse1 + i.MaterialUse2),
                                             i.ImportFormSX1.MaterialUseDate,
                                         }).ToList();
                var workPieces = GetWorkpieceMaterialModel(startMonth, endMonth);
                //var check = false;
                var _list = new List<PrintMaterialDailyPeriod>();
                var planDate = reportDate.AddDays(-5);
                foreach (var group in groupMaterials) {
                    var printGroup = new PrintGroupMaterial {
                        GroupName = group.MaterialTypeName,
                        MaterialTyleId = group.MaterialTypeId,
                        ReportDate = reportDate,
                        List = new List<PrintMaterialDailyPeriod>(),
                        StartDate = startMonth.ToString("dd/MM"),
                        EndDate = endMonth.ToString("dd/MM"),
                        Quater = quater + "",
                    };
                    var workPiece = workPieces.Where(wp => wp.IdentityCode.Equals(group.IdentityCode)).ToList();
                    if (workPiece.Any()) {
                        printGroup.WorkPiece = workPiece.Sum(wp => wp.LastQuantity);
                        printGroup.WorkPieceInMonth = workPiece.Sum(wp => wp.Import);
                    }
                    foreach (var item in workPiece) {
                        if (item.Type == (int)MyUtilities.Material.WorkpieceType.Piece) {
                            printGroup.PieceWorkpiece = item.Import;
                        }
                        else if (item.Type == (int)MyUtilities.Material.WorkpieceType.Scrap) {
                            printGroup.ScrapWorkpiece = item.Import;
                        }
                    }
                    _list = new List<PrintMaterialDailyPeriod>();
                    var materialsByType = materials.Where(m => m.MaterialTypeId == group.MaterialTypeId).ToList();
                    foreach (var material in materialsByType) {
                        //var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == materialId.MaterialId);
                        var entity = new PrintMaterialDailyPeriod {
                            MaterialId = material.MaterialId,
                            MaterialName = material.MaterialName,
                            MaterialDesignNo =
                                MyUtilities.Material.GetMaterialDesignNo(material.OutDiameter, material.InDiameter, material.DiameterType, material.Shape),
                            Details = new List<PrintMaterialDailyPeriodDetail>(),
                            Show = true,
                            Shape = material.Shape,
                            OutDiameter = material.OutDiameter,
                            InDiameter = material.InDiameter,
                            DiameterType = material.DiameterType,
                            Status = 0,
                            ForecastStatus = 0,
                            ReportDate = reportDate,
                            PlanDate = planDate
                        };
                        var materialInvsById = materialInvs.Where(mi => mi.MaterialId == material.MaterialId);
                        var _details = new List<PrintMaterialDailyPeriodDetail>();
                        var lastExports = new List<MachineExport>();
                        foreach (var materialInv in materialInvsById) {
                            var detail = new PrintMaterialDailyPeriodDetail {
                                MaterialId = materialInv.MaterialId,
                                MaterialLot = materialInv.LotNumber,
                                VendorId = materialInv.VendorId,
                                VendorCode = materialInv.VendorCode,
                                MaterialUnitWeight = materialInv.UnitWeight,
                                MaterialInvId = materialInv.MaterialInventoryId,
                                OnMachines = new List<PrintMaterialDailyPeriodDetailOnMachine>(),
                                MaterialLenght = materialInv.Length,
                            };
                            var earlyPeriodsById =
                                mipStartMonth.Where(
                                    period => period.MaterialInventoryId == materialInv.MaterialInventoryId).ToList();
                            if (earlyPeriodsById.Any()) {
                                detail.EarlyQuantity =
                                    Math.Round(
                                        earlyPeriodsById.Sum(
                                            e => e.LastPeriodQuantity - e.EarlyPeriodQuantity), 2);
                            }
                            var startOnMachine = mimpStartMonth.Where(
                                   period => period.MaterialInvId == materialInv.MaterialInventoryId).ToList();
                            if (startOnMachine.Any()) {
                                detail.EarlyQuantity +=
                                    Math.Round(startOnMachine.Sum(
                                        period => period.LastQuantity - period.EarlyQuantity), 2);
                            }
                            var importInMonth =
                                 mipInMonth.Where(
                                     period =>
                                     period.MaterialInventoryId == materialInv.MaterialInventoryId &&
                                     period.LastPeriodQuantity > period.EarlyPeriodQuantity);
                            if (importInMonth.Any()) {
                                detail.ImportInMonth = Math.Round(importInMonth.Sum(period => period.Quantity), 2);
                                var importInDay = importInMonth.Where(ipo => ipo.PeriodDate.Day == importSx1.MaterialUseDate.Day 
                                    && ipo.PeriodDate.Month == importSx1.MaterialUseDate.Month 
                                    && ipo.PeriodDate.Year == importSx1.MaterialUseDate.Year);
                                if (importInDay.Any()) {
                                    detail.Import = Math.Round(importInDay.Sum(e => e.Quantity), 2);
                                }
                                var retrieve =
                                    retrieveInMonth.Where(
                                        ms => ms.MaterialInvId == materialInv.MaterialInventoryId);
                                if (retrieve.Any()) {
                                    detail.ImportInMonth -= Math.Round(retrieve.Sum(period => period.Quantity), 2);
                                    var retrieveInDay = retrieve.Where(ipo => ipo.UsedDate == importSx1.MaterialUseDate);
                                    if (retrieveInDay.Any()) {
                                        detail.Import -= Math.Round(retrieveInDay.Sum(e => e.Quantity), 2);
                                    }
                                }
                            }
                            var exportsInMonth = (from ed in emInMonth
                                                  where
                                                      ed.MaterialInvId == materialInv.MaterialInventoryId &&
                                                      ed.MachineId != null
                                                  select ed).ToList();
                            if (exportsInMonth.Any()) {
                                detail.ExportInMonth = Math.Round(exportsInMonth.Sum(e => e.Quantity), 2);
                                var exportInDay = exportsInMonth.Where(ipo => ipo.ExportDate.Day == importSx1.MaterialUseDate.Day
                                    && ipo.ExportDate.Month == importSx1.MaterialUseDate.Month
                                    && ipo.ExportDate.Year == importSx1.MaterialUseDate.Year);
                                if (exportInDay.Any()) {
                                    detail.Export = Math.Round(exportInDay.Sum(e => e.Quantity), 2);
                                }
                            }
                            var exportsDestroyMonth = (from ed in emInMonth
                                                       where
                                                           ed.MaterialInvId == materialInv.MaterialInventoryId &&
                                                           ed.MachineId == null
                                                       select ed).ToList();
                            if (exportsDestroyMonth.Any()) {
                                detail.ExportDestroyInMonth = Math.Round(exportsDestroyMonth.Sum(e => e.Quantity), 2);
                            }
                            var lastPeriodsById =
                                mip.Where(
                                    period => period.MaterialInventoryId == materialInv.MaterialInventoryId).ToList();
                            if (lastPeriodsById.Any()) {
                                detail.LastQuantity =
                                    Math.Round(
                                        lastPeriodsById.Sum(
                                            e => e.LastPeriodQuantity - e.EarlyPeriodQuantity), 2);
                            }
                            var materialInvsOnMachineById = mimp.Where(
                                period => period.MaterialInvId == materialInv.MaterialInventoryId).ToList();
                            if (materialInvsOnMachineById.Any()) {
                                detail.MaterialOnMachine =
                                    Math.Round(
                                        materialInvsOnMachineById.Sum(
                                            e => e.LastQuantity - e.EarlyQuantity), 2);
                            }
                            var useInMonth = (from sx in importSx1Details
                                              where
                                                  //sx.MachineId == 44 &&
                                                  sx.MaterialInvId == materialInv.MaterialInventoryId
                                              select sx).ToList();

                            var _onMachines = new List<PrintMaterialDailyPeriodDetailOnMachine>();

                            var groupSx = (from sx in useInMonth
                                           group sx by
                                               new {
                                                   sx.MachineId,
                                                   sx.MaterialInvId,
                                                   sx.ProductId
                                               }
                                               into sxsx1
                                               select new {
                                                   sxsx1.Key.MachineId,
                                                   sxsx1.Key.MaterialInvId,
                                                   sxsx1.Key.ProductId,
                                                   sxsx1.FirstOrDefault().Product,
                                                   sxsx1.FirstOrDefault().MachineName,
                                                   //DifferenceQuantity = sxsx1.Sum(sx => sx.DifferenceQuantity),
                                                   MaterialUse = sxsx1.Sum(sx => sx.MaterialUse),
                                                   //DayCount = sxsx1.Where(sx => sx.MaterialUse > 0).Select(sx => sx.MaterialUseDate).Distinct().Count(),
                                                   //IsPlan = sxsx1.Any(sx => sx.MaterialUseDate > planDate && sx.MaterialUse > 0),
                                                   StartDate = sxsx1.FirstOrDefault().MaterialUseDate,
                                                   DayProduct = sxsx1.Count(),
                                                   ProductUnitWeight = sxsx1.ToList().LastOrDefault().ProductUnitWeight,
                                                   //ProductionUnitWeight = sxsx1.ToList().LastOrDefault().ProductionUnitWeight,
                                                   ProductionRate = sxsx1.ToList().LastOrDefault().ProductionRate,
                                                   ProductLength = sxsx1.ToList().LastOrDefault().ProductLength,
                                                   KnifeCut = sxsx1.ToList().LastOrDefault().KnifeCut,
                                               }).ToList();
                            //neu duy nhat chi co 1 may chay 1 san pham
                            if (groupSx.Count() == 1) {
                                var sx1 = groupSx.FirstOrDefault();
                                var onMachine = new PrintMaterialDailyPeriodDetailOnMachine {
                                    ProductId = sx1.ProductId,
                                    ProductCode = sx1.Product.ProductCode,
                                    MachineId = sx1.MachineId,
                                    MachineName = sx1.MachineName,
                                    ProductForecastsQuality = sx1.Product.ForecastsQuality ?? 0,
                                    ProductUnitWeight = sx1.ProductUnitWeight,
                                    MaterialUse = 0,
                                    ExportInMonth = 0,
                                    Export = 0,
                                    DayProducted = 1,
                                    ProductInventory = 0,
                                    LastExport = 0,
                                    DefectQuantity = 0,
                                    ProductQuantity = 0,
                                    ProductOrder = 0,
                                    OrderStatus = 1,
                                    Productivity = 0,
                                    ProductionRate = sx1.ProductionRate,
                                    MaterialUnitWeight = detail.MaterialUnitWeight,
                                    MaterialId = detail.MaterialId,
                                    MaterialLenght = detail.MaterialLenght,
                                    MaterialUseInPlan = detail.MaterialUse,
                                    ProductionUnitWeight =
                                    MyUtilities.Product.GetProductWeight(material.MaterialName,
                                        material.OutDiameter,
                                        material.InDiameter,
                                        sx1.ProductLength,
                                        sx1.KnifeCut,
                                        material.Shape),
                                };
                                onMachine.ExportInMonth = detail.ExportInMonth;
                                onMachine.Export = detail.Export;
                                var lastExport =
                                    exportsInMonth.OrderByDescending(em => em.ExportDate).FirstOrDefault();
                                //1
                                if (lastExport != null) {
                                    var exportMachine =
                                        lastExports.FirstOrDefault(
                                            me => me.MachineId == sx1.MachineId && me.MaterialId == material.MaterialId);
                                    if (exportMachine == null) {
                                        exportMachine = new MachineExport {
                                            MachineId = sx1.MachineId,
                                            MaterialId = material.MaterialId,
                                            LastExport = Math.Round(lastExport.Quantity, 2),
                                            ExportDate = sx1.StartDate
                                        };
                                        lastExports.Add(exportMachine);
                                    }
                                    else {
                                        if (exportMachine.ExportDate < sx1.StartDate)
                                            exportMachine.LastExport = Math.Round(lastExport.Quantity, 2);
                                        else if (exportMachine.ExportDate == sx1.StartDate) {
                                            exportMachine.LastExport +=
                                                Math.Round(lastExport.Quantity, 2);
                                        }
                                    }
                                }

                                var productInvById =
                                    productInvPeriods.Where(pip => pip.ProductId == sx1.ProductId).ToList();
                                if (productInvById.Any()) {
                                    onMachine.ProductInventory = productInvById.Sum(pip => pip.PeriodQuantity);
                                }
                                var saleById =
                                   saleProduct.Where(pip => pip.ProductId == sx1.ProductId).ToList();
                                if (saleById.Any()) {
                                    onMachine.SaleQuater = saleById.Sum(pip => pip.PeriodQuantity);
                                }
                                var pOrders =
                                    productOrders.Where(po => po.ProductId == onMachine.ProductId).ToList();
                                if (pOrders.Any()) {
                                    onMachine.ProductOrder = pOrders.Sum(po => po.RequiedNumber);
                                    if (onMachine.ProductInventory < onMachine.ProductOrder)
                                        onMachine.OrderStatus = 2;
                                }

                                onMachine.DayProducted = useInMonth.Count();
                                var useInMonthById =
                                             useInMonth.Where(
                                                 ipo =>
                                                 ipo.MachineId == sx1.MachineId &&
                                                 ipo.ProductId == sx1.ProductId).ToList();
                                if (useInMonthById.Any()) {
                                    onMachine.MaterialUseInMonth = Math.Round(useInMonthById.Sum(e => e.MaterialUse), 2);
                                    onMachine.ProductQuantityInMonth = useInMonthById.Sum(e => e.Production);
                                    onMachine.ProcessQuantityInMonth = useInMonthById.Sum(e => e.Processing);
                                    onMachine.DefectQuantityInMonth = useInMonthById.Sum(e => e.DefectProduct);
                                    onMachine.DifferentQuantityInMonth = useInMonthById.Sum(e => e.DifferenceQuantity);
                                    var useInDay =
                                        useInMonthById.Where(
                                            ipo =>
                                            ipo.MaterialUseDate.Day == importSx1.MaterialUseDate.Day
                                            && ipo.MaterialUseDate.Month == importSx1.MaterialUseDate.Month
                                            && ipo.MaterialUseDate.Year == importSx1.MaterialUseDate.Year).ToList();
                                    if (useInDay.Any()) {
                                        onMachine.MaterialUse = Math.Round(useInDay.Sum(e => e.MaterialUse), 2);
                                        onMachine.ProductQuantity = useInDay.Sum(e => e.Production);
                                        onMachine.ProcessQuantity = useInDay.Sum(e => e.Processing);
                                        onMachine.DefectQuantity = useInDay.Sum(e => e.DefectProduct);
                                        onMachine.DifferentQuantity = useInDay.Sum(e => e.DifferenceQuantity);
                                    }
                                    if (useInMonthById.Any(e => e.MaterialUseDate >= planDate)) {
                                        var dayCount = useInMonthById.Select(e => e.MaterialUseDate).Distinct().Count();
                                        onMachine.MaterialUseInPlan = Math.Max(onMachine.MaterialUse, onMachine.MaterialUseInMonth / dayCount);
                                    }
                                }
                                if ((onMachine.ProductQuantity + onMachine.ProcessQuantity + onMachine.DefectQuantity) > 0) {
                                    var lastTrack =
                                        MyUtilities.Machine.LastTrackUpMachine(
                                        onMachine.MachineId,
                                        onMachine.MaterialId,
                                        onMachine.ProductId,
                                        printGroup.ReportDate
                                        );
                                    if (lastTrack != null) {
                                        onMachine.ProductionRate =
                                            MyUtilities.Product.GetProductRate(
                                            detail.MaterialLenght,
                                            lastTrack.WorkPiece,
                                            lastTrack.ProductLength,
                                            lastTrack.KnifeCut);
                                        //onMachine.ProductionUnitWeight =
                                        //    MyUtilities.Product.GetProductWeight(material.MaterialName,
                                        //        material.OutDiameter,
                                        //        material.InDiameter,
                                        //        sx1.ProductLength,
                                        //        lastTrack.KnifeCut,
                                        //        material.Shape);
                                        onMachine.Productivity = lastTrack.RealProductivity;
                                        onMachine.ProductForecastsQuality = lastTrack.Quantity;
                                        onMachine.WorkPieceLenght = lastTrack.WorkPiece;
                                        var productionQuantity =
                                            importProductions.Where(
                                                id =>
                                                id.ProductId == onMachine.ProductId && id.MachineId == onMachine.MachineId &&
                                                id.MaterialId == onMachine.MaterialId &&
                                                id.MaterialUseDate >= lastTrack.StartDate)
                                                             .ToList().Sum(i => i.ProductionMaterialQuantity);
                                        var trackMaterialUse = onMachine.ProductForecastsQuality / onMachine.ProductionRate;
                                        onMachine.RequiredPerProduct = Math.Round((trackMaterialUse - productionQuantity), 2);
                                        if (onMachine.RequiredPerProduct <= 0) {
                                            var materialUse =
                                                importProductions.Where(
                                                    id =>
                                                    id.ProductId == onMachine.ProductId && id.MachineId == onMachine.MachineId &&
                                                    id.MaterialId == onMachine.MaterialId &&
                                                    id.MaterialUseDate >= lastTrack.StartDate)
                                                                 .ToList().Sum(i => i.MaterialUse);
                                            if (materialUse > trackMaterialUse * 1.1)
                                                onMachine.RequiredStatus = 2;
                                            else onMachine.RequiredStatus = 1;
                                        }
                                    }
                                    //else {
                                    //    onMachine.ProductionUnitWeight =
                                    //        MyUtilities.Product.GetProductWeight(material.MaterialName,
                                    //            material.OutDiameter,
                                    //            material.InDiameter,
                                    //            sx1.ProductLength,
                                    //            0,
                                    //            material.Shape);
                                    //}
                                }
                                onMachine.DifferentQuantity = (onMachine.ProductQuantity + onMachine.ProcessQuantity + onMachine.DefectQuantity) -
                                                              (onMachine.ProductionRate *
                                                               onMachine.MaterialUse);
                                var productionMaterial =
                                    productionMaterials.FirstOrDefault(
                                        pm =>
                                        pm.ProductId == onMachine.ProductId &&
                                        pm.MaterialId == material.MaterialId);
                                if (productionMaterial == null)
                                    onMachine.ProductionMaterialStatus = 1;
                                printGroup.CountRow++;
                                _onMachines.Add(onMachine);
                            }
                            else {
                                // group machine lai
                                var machineIds = groupSx.Select(g => g.MachineId).Distinct().ToList();
                                foreach (var machineId in machineIds) {
                                    var onMachines =
                                        groupSx.Where(
                                            om =>
                                            om.MachineId == machineId).OrderBy(om => om.StartDate).ToList();
                                    var index = 1;
                                    foreach (var sx1 in onMachines) {
                                        var sx1s =
                                            useInMonth.Where(
                                                sx =>
                                                sx.MachineId == sx1.MachineId &&
                                                sx.ProductId == sx1.ProductId).ToList();
                                        var onMachine = new PrintMaterialDailyPeriodDetailOnMachine {
                                            ProductId = sx1.ProductId,
                                            ProductCode = sx1.Product.ProductCode,
                                            MachineId = sx1.MachineId,
                                            MachineName = sx1.MachineName,
                                            MaterialUseInMonth = 0,
                                            ProductionRate = sx1.ProductionRate,
                                            ProductForecastsQuality =
                                                MyUtilities.Function.RoundUp((sx1.Product.ForecastsQuality ?? 0) / 4, 3),
                                            MaterialUse = 0,
                                            ExportInMonth = 0,
                                            Export = 0,
                                            DayProducted = 1,
                                            ProductInventory = 0,
                                            LastExport = 0,
                                            DefectQuantity = 0,
                                            ProductQuantity = 0,
                                            ProcessQuantity = 0,
                                            ProductOrder = 0,
                                            OrderStatus = 1,
                                            Productivity = 0,
                                            RequiredPerProduct = 0,
                                            MaterialUnitWeight = detail.MaterialUnitWeight,
                                            ProductUnitWeight = sx1.ProductUnitWeight,
                                            MaterialId = detail.MaterialId,
                                            MaterialLenght = detail.MaterialLenght,
                                            ProductionUnitWeight =
                                                MyUtilities.Product.GetProductWeight(material.MaterialName,
                                                    material.OutDiameter,
                                                    material.InDiameter,
                                                    sx1.ProductLength,
                                                    sx1.KnifeCut,
                                                    material.Shape),
                                        };
                                        if (index == 1)
                                            onMachine.StartDate = startMonth;
                                        else
                                            onMachine.StartDate = sx1s.FirstOrDefault().MaterialUseDate;
                                        if (index == onMachines.Count())
                                            onMachine.EndDate = importSx1.MaterialUseDate;
                                        else
                                            onMachine.EndDate = sx1s.LastOrDefault().MaterialUseDate;
                                        var onMachineExist =
                                            _onMachines.Where(om => om.MachineId == sx1.MachineId)
                                                       .ToList()
                                                       .LastOrDefault();
                                        if (onMachineExist != null) {
                                            onMachineExist.EndDate = onMachine.StartDate.AddDays(-1);
                                            var exportByMachines2 =
                                                exportsInMonth.Where(
                                                    ed =>
                                                    ed.MachineId == onMachineExist.MachineId &&
                                                    ed.ExportDate >= onMachineExist.StartDate &&
                                                    ed.ExportDate <= onMachineExist.EndDate).ToList();
                                            if (exportByMachines2.Any()) {
                                                onMachineExist.ExportInMonth =
                                                    Math.Round(exportByMachines2.Sum(e => e.Quantity), 2);
                                                var lastExport =
                                                    exportByMachines2.OrderByDescending(em => em.ExportDate)
                                                                     .FirstOrDefault();
                                                onMachineExist.LastExport = Math.Round(lastExport.Quantity, 2);
                                                onMachineExist.Export = 0;
                                            }
                                        }
                                        onMachine.DayProducted = sx1.DayProduct;
                                        index++;
                                        var exportByMachines =
                                            exportsInMonth.Where(
                                                ed =>
                                                ed.MachineId == sx1.MachineId &&
                                                ed.ExportDate >= onMachine.StartDate &&
                                                ed.ExportDate <= onMachine.EndDate).ToList();
                                        if (exportByMachines.Any()) {
                                            onMachine.ExportInMonth =
                                                Math.Round(exportByMachines.Sum(e => e.Quantity), 2);
                                            var lastExport =
                                                exportByMachines.OrderByDescending(em => em.ExportDate)
                                                                .FirstOrDefault();
                                            //2
                                            if (lastExport != null) {
                                                var exportMachine =
                                                    lastExports.FirstOrDefault(
                                                        me => me.MachineId == sx1.MachineId && me.MaterialId == material.MaterialId);
                                                if (exportMachine == null) {
                                                    exportMachine = new MachineExport {
                                                        MachineId = sx1.MachineId,
                                                        MaterialId = material.MaterialId,
                                                        LastExport = Math.Round(lastExport.Quantity, 2),
                                                        ExportDate = sx1.StartDate
                                                    };
                                                    lastExports.Add(exportMachine);
                                                }
                                                else {
                                                    if (exportMachine.ExportDate < sx1.StartDate)
                                                        exportMachine.LastExport =
                                                            Math.Round(lastExport.Quantity, 2);
                                                    else if (exportMachine.ExportDate == sx1.StartDate) {
                                                        exportMachine.LastExport +=
                                                            Math.Round(lastExport.Quantity, 2);
                                                    }
                                                }
                                                //onMachine.LastExport = Math.Round(lastExport.Quantity, 2);
                                            }
                                            var exportInDay =
                                                exportByMachines.Where(
                                                    ipo => ipo.ExportDate == importSx1.MaterialUseDate);
                                            if (exportInDay.Any()) {
                                                onMachine.Export =
                                                    Math.Round(exportInDay.Sum(e => e.Quantity),
                                                               2);
                                            }
                                        }
                                        var productInvById =
                                            productInvPeriods.Where(pip => pip.ProductId == sx1.ProductId)
                                                             .ToList();
                                        if (productInvById.Any()) {
                                            onMachine.ProductInventory =
                                                productInvById.Sum(pip => pip.PeriodQuantity);
                                        }

                                        var saleById = saleProduct.Where(pip => pip.ProductId == sx1.ProductId).ToList();
                                        if (saleById.Any()) {
                                            onMachine.SaleQuater = saleById.Sum(pip => pip.PeriodQuantity);
                                        }
                                        var pOrders =
                                            productOrders.Where(po => po.ProductId == onMachine.ProductId)
                                                         .ToList();
                                        if (pOrders.Any()) {
                                            onMachine.ProductOrder = pOrders.Sum(po => po.RequiedNumber);
                                            if (onMachine.ProductInventory < onMachine.ProductOrder)
                                                onMachine.OrderStatus = 2;
                                        }
                                        onMachine.MaterialUseInMonth = sx1.MaterialUse;
                                        var useInMonthById =
                                            useInMonth.Where(
                                                ipo =>
                                                ipo.MachineId == sx1.MachineId &&
                                                ipo.ProductId == sx1.ProductId).ToList();
                                        if (useInMonthById.Any()) {
                                            //onMachine.MaterialUse = Math.Round(useInMonthById.Sum(e => e.MaterialUse), 2);
                                            onMachine.ProductQuantityInMonth = useInMonthById.Sum(e => e.Production);
                                            onMachine.ProcessQuantityInMonth = useInMonthById.Sum(e => e.Processing);
                                            onMachine.DefectQuantityInMonth = useInMonthById.Sum(e => e.DefectProduct);
                                            onMachine.DifferentQuantityInMonth = useInMonthById.Sum(e => e.DifferenceQuantity);
                                            var useInDay =
                                                useInMonthById.Where(
                                                    ipo =>
                                                    ipo.MaterialUseDate == importSx1.MaterialUseDate).ToList();
                                            if (useInDay.Any()) {
                                                onMachine.MaterialUse = Math.Round(useInDay.Sum(e => e.MaterialUse), 2);
                                                onMachine.MaterialUseInPlan = onMachine.MaterialUse;
                                                onMachine.ProductQuantity = useInDay.Sum(e => e.Production);
                                                onMachine.ProcessQuantity = useInDay.Sum(e => e.Processing);
                                                onMachine.DefectQuantity = useInDay.Sum(e => e.DefectProduct);
                                                //onMachine.ProductWeight = useInDay.Sum(e => e.ProductWeight);
                                                onMachine.DifferentQuantity = useInDay.Sum(e => e.DifferenceQuantity);
                                            }
                                            if (useInMonthById.Any(e => e.MaterialUseDate >= planDate)) {
                                                var dayCount = useInMonthById.Select(e => e.MaterialUseDate).Distinct().Count();
                                                onMachine.MaterialUseInPlan = Math.Max(onMachine.MaterialUse, onMachine.MaterialUseInMonth / dayCount);
                                            }
                                        }

                                        if (onMachine.ProductQuantity + onMachine.ProcessQuantity + onMachine.DefectQuantity > 0) {
                                            var lastTrack =
                                                MyUtilities.Machine.LastTrackUpMachine(
                                                onMachine.MachineId,
                                                onMachine.MaterialId,
                                                onMachine.ProductId,
                                                printGroup.ReportDate
                                                );
                                            if (lastTrack != null) {
                                                onMachine.ProductionRate =
                                                    MyUtilities.Product.GetProductRate(detail.MaterialLenght,
                                                        lastTrack.WorkPiece,
                                                        lastTrack.ProductLength,
                                                        lastTrack.KnifeCut);
                                                //onMachine.ProductionUnitWeight =
                                                //    MyUtilities.Product.GetProductWeight(material.MaterialName,
                                                //        material.OutDiameter,
                                                //        material.InDiameter,
                                                //        sx1.ProductLength,
                                                //        lastTrack.KnifeCut,
                                                //        material.Shape);
                                                onMachine.Productivity = lastTrack.RealProductivity;
                                                onMachine.ProductForecastsQuality = lastTrack.Quantity;
                                                onMachine.WorkPieceLenght = lastTrack.WorkPiece;
                                                var productionQuantity =
                                                    importProductions.Where(
                                                            id =>
                                                                id.ProductId == onMachine.ProductId &&
                                                                id.MachineId == onMachine.MachineId &&
                                                                id.MaterialId == onMachine.MaterialId &&
                                                                id.MaterialUseDate >= lastTrack.StartDate)
                                                        .ToList().Sum(i => i.ProductionMaterialQuantity);
                                                var trackMaterialUse = onMachine.ProductForecastsQuality /
                                                                       onMachine.ProductionRate;
                                                onMachine.RequiredPerProduct =
                                                    Math.Round((trackMaterialUse - productionQuantity), 2);
                                                if (onMachine.RequiredPerProduct <= 0) {
                                                    var materialUse =
                                                        importProductions.Where(
                                                                id =>
                                                                    id.ProductId == onMachine.ProductId &&
                                                                    id.MachineId == onMachine.MachineId &&
                                                                    id.MaterialId == onMachine.MaterialId &&
                                                                    id.MaterialUseDate >= lastTrack.StartDate)
                                                            .ToList().Sum(i => i.MaterialUse);
                                                    if (materialUse > trackMaterialUse * 1.1)
                                                        onMachine.RequiredStatus = 2;
                                                    else onMachine.RequiredStatus = 1;
                                                }
                                            }
                                            //else {
                                            //    onMachine.ProductionUnitWeight =
                                            //        MyUtilities.Product.GetProductWeight(material.MaterialName,
                                            //            material.OutDiameter,
                                            //            material.InDiameter,
                                            //            sx1.ProductLength,
                                            //            0,
                                            //            material.Shape);
                                            //}
                                        }
                                        //if (onMachine.ProductForecastsQuality != 0)
                                        //    onMachine.RequiredPerProduct =
                                        //        Math.Round((onMachine.ProductForecastsQuality -
                                        //                    onMachine.ProductInventory - onMachine.SaleQuater) /
                                        //                   onMachine.ProductionRate, 2);
                                        //if (onMachine.RequiredPerProduct <= 0)
                                        //    onMachine.RequiredStatus = 1;
                                        var productionMaterial =
                                            productionMaterials.FirstOrDefault(
                                                pm =>
                                                pm.ProductId == onMachine.ProductId &&
                                                pm.MaterialId == material.MaterialId);
                                        if (productionMaterial == null)
                                            onMachine.ProductionMaterialStatus = 1;
                                        printGroup.CountRow++;
                                        _onMachines.Add(onMachine);
                                    }
                                }
                                //}
                            }
                            var machineUseIds = _onMachines.Select(om => om.MachineId).Distinct().ToList();
                            var exportsMachineDoNotUse =
                                exportsInMonth.Where(
                                    em => em.MachineId != null && !machineUseIds.Contains(em.MachineId.Value))
                                              .Select(em => em.MachineId).Distinct().ToList();
                            foreach (var machineId in exportsMachineDoNotUse) {
                                var exportByMachines =
                                    exportsInMonth.Where(
                                        ed =>
                                        ed.MachineId == machineId).ToList();
                                var onMachine = new PrintMaterialDailyPeriodDetailOnMachine {
                                    ProductId = 0,
                                    ProductCode = "",
                                    MachineId = machineId.Value,
                                    MachineName = exportByMachines.FirstOrDefault().MachineName,
                                    MaterialUseInMonth = 0,
                                    ProductionRate = 1,
                                    ProductForecastsQuality = 0,
                                    MaterialUse = 0,
                                    ExportInMonth = 0,
                                    Export = 0,
                                    DayProducted = 0,
                                    ProductInventory = 0,
                                    LastExport = 0,
                                    DefectQuantity = 0,
                                    ProductQuantity = 0,
                                    ProcessQuantity = 0,
                                    DifferentQuantity = 0,
                                    ProductOrder = 0,
                                    OrderStatus = 1,
                                    Productivity = 0,
                                    RequiredPerProduct = 0,
                                    RequiredStatus = 0,
                                    MaterialUnitWeight = detail.MaterialUnitWeight,
                                    ProductUnitWeight = 0,
                                    MaterialId = detail.MaterialId,
                                    MaterialLenght = detail.MaterialLenght,
                                };

                                if (exportByMachines.Any()) {
                                    onMachine.ExportInMonth =
                                        Math.Round(exportByMachines.Sum(e => e.Quantity), 2);
                                    var lastExport =
                                        exportByMachines.OrderByDescending(em => em.ExportDate)
                                                        .FirstOrDefault();
                                    onMachine.LastExport = Math.Round(lastExport.Quantity, 2);
                                    var exportInDay =
                                        exportByMachines.Where(
                                            ipo => ipo.ExportDate == importSx1.MaterialUseDate);
                                    if (exportInDay.Any()) {
                                        onMachine.Export =
                                            Math.Round(exportInDay.Sum(e => e.Quantity),
                                                       2);
                                    }
                                }
                                printGroup.CountRow++;
                                _onMachines.Add(onMachine);
                            }
                            //}

                            if (_onMachines.Any()) {
                                detail.LastExport = _onMachines.Sum(om => om.LastExport);
                                detail.MaterialRate = _onMachines.Sum(om => om.MaterialRate);
                                detail.ProductivityInDay = _onMachines.Sum(om => om.ProductivityInDay);
                                detail.OnMachines.AddRange(
                                    _onMachines.OrderBy(om => om.MachineName).ThenBy(om => om.ProductCode));
                                var productOnMachines = _onMachines
                                    .Where(om => om.ProductionRate != 0)
                                    .GroupBy(p => p.ProductId,
                                             (key, g) =>
                                             new {
                                                 ProductId = key,
                                                 g.FirstOrDefault().ProductInventory,
                                                 g.FirstOrDefault().SaleQuater,
                                                 ForecastsQuality = g.FirstOrDefault().ProductForecastsQuality,
                                                 g.FirstOrDefault().ProductionRate,
                                             });
                                foreach (var pOnMachine in productOnMachines) {
                                    detail.Required += Math.Round((pOnMachine.ForecastsQuality -
                                                                     pOnMachine.ProductInventory - pOnMachine.SaleQuater) /
                                                                     pOnMachine.ProductionRate, 2);
                                }
                            }
                            detail.LastQuantity = detail.LastQuantity + detail.MaterialOnMachine;
                            _details.Add(detail);
                        }
                        entity.EarlyQuantity = _details.Sum(d => d.EarlyQuantity);
                        entity.EarlyQuantityKg = _details.Sum(d => d.EarlyQuantityKg);
                        entity.LastQuantity = _details.Sum(d => d.LastQuantity);
                        entity.LastQuantityKg = _details.Sum(d => d.LastQuantityKg);
                        entity.MaterialOnMachine = _details.Sum(d => d.MaterialOnMachine);
                        entity.Required = _details.Sum(d => d.Required);
                        entity.LastExport = lastExports.Sum(d => d.LastExport);
                        entity.MaterialRate = _details.Sum(om => om.MaterialRate);
                        entity.ProductivityInDay = _details.Sum(om => om.ProductivityInDay);

                        if (entity.Required < 0) {
                            var standard = 100 / entity.OutDiameter;
                            if ((entity.Required * -1) < standard)
                                entity.Status = 2;
                            entity.Required = 0;
                        }
                        else if (entity.Required > entity.LastQuantity) {
                            entity.Status = 1;
                        }
                        if (entity.MaterialRate != 0)
                            entity.MaterialForecasts = Math.Round(entity.LastQuantity / entity.MaterialRate, 0);
                        if (entity.MaterialForecasts < 45 && entity.Status == 1)
                            entity.ForecastStatus = 1;
                        entity.Details.AddRange(_details.Where(d => d.Show));
                        if (!entity.Details.Any() && entity.ImportInMonth == 0 && entity.ExportDestroyInMonth == 0)
                            entity.Show = false;
                        else if (entity.ImportInMonth > 0 && !entity.Details.Any()) {
                            printGroup.CountRow++;
                        }

                        var importSx1DetailsByMaterialId = from sx in importSx1Details
                                                           where sx.Material.MaterialId == entity.MaterialId
                                                           select sx;
                        foreach (var sxDetail in importSx1DetailsByMaterialId) {
                            var useInDay = entity.MaterialUseInDays.FirstOrDefault(x => x.MachineId == sxDetail.MachineId
                                                                        && x.ProductId == sxDetail.ProductId
                                                                        && x.DayUse == sxDetail.MaterialUseDate);
                            if (useInDay == null) {
                                useInDay = new MaterialUseInDay {
                                    MachineId = sxDetail.MachineId,
                                    ProductId = sxDetail.ProductId,
                                    DayUse = sxDetail.MaterialUseDate,
                                    MaterialUse = sxDetail.MaterialUse,
                                    MaterialUseKg = sxDetail.MaterialUse * sxDetail.MaterialUnitWeight
                                };
                                entity.MaterialUseInDays.Add(useInDay);
                            }
                            else {
                                useInDay.MaterialUse += sxDetail.MaterialUse;
                                useInDay.MaterialUseKg += sxDetail.MaterialUse * sxDetail.MaterialUnitWeight;
                            }
                        }
                        _list.Add(entity);
                    }
                    var showList = _list.Where(l => l.Show);
                    foreach (var entityShow in showList) {
                        var entitySames =
                            _list.Where(
                                l =>
                                l.MaterialName.Equals(entityShow.MaterialName) &&
                                l.Shape.Equals(entityShow.Shape) &&
                                l.OutDiameter == entityShow.OutDiameter &&
                                l.InDiameter == entityShow.InDiameter &&
                                l.Show == false &&
                                l.EarlyQuantity != 0 &&
                                l.LastQuantity != 0
                                );
                        foreach (var entityUnshow in entitySames) {
                            entityUnshow.Show = true;
                            printGroup.CountRow++;
                        }
                    }
                    printGroup.EarlyQuantity = _list.Sum(l => l.EarlyQuantity);
                    printGroup.EarlyQuantityKg = _list.Sum(l => l.EarlyQuantityKg);
                    printGroup.MaterialOnMachine = _list.Sum(l => l.MaterialOnMachine);
                    printGroup.LastQuantity = _list.Sum(l => l.LastQuantity);
                    printGroup.LastQuantityKg = _list.Sum(l => l.LastQuantityKg);
                    printGroup.MaterialRate = _list.Sum(om => om.MaterialRate);
                    printGroup.ProductivityInDay = _list.Sum(om => om.ProductivityInDay);
                    printGroup.WorkPieceKg = _list.Sum(l => l.WorkPieceKg);

                    printGroup.List.AddRange(_list.Where(l => l.Show)
                             .OrderBy(l => l.MaterialName)
                             .ThenBy(l => l.Shape)
                             .ThenBy(l => l.OutDiameter)
                             .ThenBy(l => l.InDiameter)
                             .ThenBy(l => l.DiameterType)
                             .ThenBy(l => l.Length));
                    if (printGroup.List.Any()) {
                        printGroup.SmallestMaterialForecasts = printGroup.List
                                                                         .OrderBy(l => l.MaterialForecasts)
                                                                         .First()
                                                                         .MaterialForecasts;
                    }
                    model.Add(printGroup);
                }
            }
            var groupWorkPiece = new List<PrintGroupWorkPiece>();
            var statisticMaterialTypes = new List<PrintStatisticMaterialType>();
            foreach (var group in model) {
                var newEntity = new PrintGroupWorkPiece() {
                    GroupName = group.GroupName,
                    PlanWorkPiece = group.WorkPieceKg,
                    ScrapWorkPiece = group.ScrapWorkpiece,
                    PieceWorkPiece = group.PieceWorkpiece,
                    WorkPieceInMonth = group.MaterialWorkPieceInMonth,
                    DefectWorkPiece = 0
                };
                if (group.PieceWorkpiece + group.ScrapWorkpiece > 0) {
                    newEntity.DefectWorkPiece = group.MaterialWorkPieceInMonth - group.PieceWorkpiece - group.ScrapWorkpiece;
                }
                if (newEntity.DefectWorkPiece < 0) newEntity.DefectWorkPiece = 0;
                groupWorkPiece.Add(newEntity);
                foreach (var element in group.List) {
                    var productsOnMachine = element.Details.SelectMany(x => x.OnMachines.Select(y => y.ProductId)).Distinct().Count();
                    foreach (var detail in element.Details) {
                        if (group.MaterialTyleId == 4) {
                            var statistic = statisticMaterialTypes.FirstOrDefault(x => x.VendorId == detail.VendorId);
                            if (statistic == null) {
                                statistic = new PrintStatisticMaterialType {
                                    VendorId = detail.VendorId,
                                    VendorCode = detail.VendorCode,
                                    MaterialUseKg = detail.MaterialUseKg,
                                    MaterialUseKgInMonth = detail.MaterialUseInMonthKg,
                                    Percentage = 0
                                };
                                statisticMaterialTypes.Add(statistic);
                            }
                            else {
                                statistic.MaterialUseKg += detail.MaterialUseKg;
                                statistic.MaterialUseKgInMonth += detail.MaterialUseInMonthKg;
                            }
                        }
                        if (productsOnMachine == 0) continue;
                        foreach (var onMachine in detail.OnMachines) {
                            if (onMachine.ProductUnitWeight <= 0) continue;
                            onMachine.ProductionPlan = ((element.LastQuantityKg / productsOnMachine) / onMachine.ProductionUnitWeight) * 1000;
                        }
                    }
                }
            }
            var totalMaterialUseKgInMonth = statisticMaterialTypes.Sum(x => x.MaterialUseKgInMonth);
            var totalMaterialUseKg = statisticMaterialTypes.Sum(x => x.MaterialUseKg);
            statisticMaterialTypes = statisticMaterialTypes.OrderBy(x => x.VendorCode).ToList();
            var idx = 1;
            foreach (var item in statisticMaterialTypes) {
                item.Index = idx;
                item.Percentage = Math.Round(item.MaterialUseKgInMonth / totalMaterialUseKgInMonth * 100, 0);
                idx++;
            }
            model[0].StatisticMaterialTypes = statisticMaterialTypes;
            model[0].StatisticMaterialUseKg = totalMaterialUseKg;
            model[0].StatisticMaterialUseKgInMonth = totalMaterialUseKgInMonth;

            model[0].GroupWorkPiece = groupWorkPiece;
            model[0].SumPlanWorkPiece = groupWorkPiece.Sum(x => x.PlanWorkPiece);
            model[0].SumPieceWorkPiece = groupWorkPiece.Sum(x => x.PieceWorkPiece);
            model[0].SumScrapWorkPiece = groupWorkPiece.Sum(x => x.ScrapWorkPiece);
            model[0].SumDefectWorkPiece = groupWorkPiece.Sum(x => x.DefectWorkPiece);
            model[0].SumWorkPieceInMonth = groupWorkPiece.Sum(x => x.WorkPieceInMonth);




            return model;
        }

        [HttpPost]
        public ActionResult PrintMaterialDailyPeriod(int materialType, string materialCode, string monthly) {
            var model = new List<PrintGroupMaterial>();
            try {
                model = GetMaterialDailyPeriod(materialType, materialCode, monthly);
            }
            catch (Exception ex) {
                //ModelState.AddModelError("MaterialDailyPeriod", ex.Message);
                return Json(ex.Message);
            }
            return PartialView("PageMaterialDailyPeriod", model);
        }

        [HttpPost]
        public ActionResult PrintAllMaterialDailyPeriod(int materialType, string materialCode, string monthly) {
            var model = new List<PrintGroupMaterial>();
            try {
                model = GetMaterialDailyPeriod(materialType, materialCode, monthly);
            }
            catch (Exception ex) {
                //ModelState.AddModelError("MaterialDailyPeriod", ex.Message);
                return Json(ex.Message);
            }
            return PartialView("PageAllMaterialDailyPeriod", model);
        }

        [GridAction]
        public ActionResult SelectMaterialPeriodDetailByMaterialId(int materialId, string toDate) {
            var model = new List<MaterialPeriodDetail>();
            //try
            //{
            //    var ci = new CultureInfo("vi-VN");
            //    var monthly = string.IsNullOrWhiteSpace(toDate)
            //                      ? DateTime.Today
            //                      : Convert.ToDateTime(toDate, ci);
            //    monthly = new DateTime(monthly.Year, monthly.Month, monthly.Day, 23, 59, 59);
            //    using (var vfi = new tammaContext())
            //    {
            //        var materialInvIds = vfi.MaterialInventories
            //                              .Where(mi => mi.MaterialId == materialId)
            //                              .Select(mi => mi.MaterialInventoryId);
            //        var materialExports =
            //            vfi.ExportMaterials.Where(
            //                em =>
            //                em.ExportDate.Day == monthly.Day &&
            //                em.ExportDate.Month == monthly.Month &&
            //                em.ExportDate.Year == monthly.Year &&
            //                em.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
            //        var materialExportsIds = new List<long>();
            //        if (materialExports.Any())
            //            materialExportsIds = materialExports.Select(i => i.ExportId).ToList();
            //        // bui nhui
            //        var importSX1s = vfi.ImportFormSX1.Where(
            //            i =>
            //            i.MaterialUseDate.Day == monthly.Day &&
            //            i.MaterialUseDate.Month == monthly.Month &&
            //            i.MaterialUseDate.Year == monthly.Year);
            //        var importSX1Ids = new List<int>();
            //        if (importSX1s.Any())
            //            importSX1Ids = importSX1s.Select(i => i.ImportId).ToList();

            //        var warehouseIds = new List<int> { 2, 12, 3, 4, 5, 6, 7, 13, 8, 10, 14 };
            //        foreach (var materialInvId in materialInvIds)
            //        {
            //            var materialInv =
            //                vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == materialInvId);
            //            var importSX1Details =
            //                vfi.ImportFormSX1Detail.Where(
            //                    id =>
            //                    id.MaterialInvId == materialInvId &&
            //                    importSX1Ids.Contains(id.ImportId));
            //            foreach (var pImportSx1Detail in importSX1Details)
            //            {
            //                var importSX1 =
            //                    vfi.ImportFormSX1.FirstOrDefault(i => i.ImportId == pImportSx1Detail.ImportId);
            //                var transaction =
            //                    vfi.Transactions.FirstOrDefault(
            //                        t =>
            //                        t.TransactionCode.Equals(importSX1.TransactionCode)
            //                        && t.WarehouseIssueId == null &&
            //                        t.WarehouseReceiptId == MyUtilities.Warehouse.Production1 &&
            //                        t.Status != (byte)MyUtilities.Transaction.Status.Cancel);
            //                if (transaction == null) continue;
            //                var entity =
            //                    model.FirstOrDefault(
            //                        m => m.MachineId == pImportSx1Detail.MachineId &&
            //                             m.Status == transaction.Status);
            //                if (entity == null)
            //                {
            //                    var productPeriods =
            //                        vfi.ProductInventoryPeriods.Where(
            //                            pip =>
            //                            pip.ProductId == pImportSx1Detail.ProductId &&
            //                            pip.PeriodDate <= monthly &&
            //                            warehouseIds.Contains(pip.WarehouseId));
            //                    entity = new MaterialPeriodDetail
            //                        {
            //                            MachineId = pImportSx1Detail.MachineId.Value,
            //                            MachineName = pImportSx1Detail.Machine1.MachineName,
            //                            MaterialId = materialInv.MaterialId,
            //                            MaterialInvId = materialInvId,
            //                            MaterialLotNumber = materialInv.LotNumber,
            //                            MaterialExportQuantity = 0,
            //                            ProductCode = pImportSx1Detail.Product.ProductCode,
            //                            ProductForecastsQuality = pImportSx1Detail.Product.ForecastsQuality ?? 0,
            //                            ProductQuantity = (pImportSx1Detail.Number1) +
            //                                              (pImportSx1Detail.Number2 ),
            //                            ProductId = pImportSx1Detail.ProductId,
            //                            ProductWeight = pImportSx1Detail.ProductWeight,
            //                            ProductionRate = pImportSx1Detail.ProductionRate,
            //                            Status = transaction.Status,
            //                            Note =
            //                                transaction.Status == (byte)MyUtilities.Transaction.Status.Open
            //                                    ? "Chưa duyệt"
            //                                    : "",
            //                            VendorName = materialInv.Vendor.VendorCode,
            //                        };
            //                    entity.TotalProductQuantity = productPeriods.Sum(pp => pp.LastPeriodQuantity) -
            //                                                  productPeriods.Sum(pp => pp.EarlyPeriodQuantity);
            //                    if (entity.ProductionRate != 0)
            //                        entity.MaterialRequired = Math.Round((entity.ProductForecastsQuality -
            //                                                   entity.TotalProductQuantity)
            //                                                  / entity.ProductionRate, 2);
            //                    model.Add(entity);
            //                }
            //                else
            //                {
            //                    entity.ProductQuantity += (pImportSx1Detail.Number1) +
            //                                              (pImportSx1Detail.Number2);
            //                }
            //            }
            //            var exportDetails =
            //                vfi.ExportMaterialDetails.Where(
            //                    emd =>
            //                    emd.MaterialInvId == materialInvId &&
            //                    materialExportsIds.Contains(emd.ExportId.Value));
            //            foreach (var mExportDetail in exportDetails)
            //            {
            //                var entity = model.FirstOrDefault(m => m.MachineId == mExportDetail.MachineId);
            //                if (entity == null)
            //                {
            //                    entity = new MaterialPeriodDetail
            //                        {
            //                            MachineId = 0,
            //                            MachineName = "",
            //                            MaterialId = materialInv.MaterialId,
            //                            MaterialInvId = materialInvId,
            //                            MaterialLotNumber = materialInv.LotNumber,
            //                            MaterialExportQuantity = mExportDetail.Quantity,
            //                            ProductCode = "",
            //                            ProductionRate = 0,
            //                            ProductForecastsQuality = 0,
            //                            ProductQuantity = 0,
            //                            ProductId = 0,
            //                            ProductWeight = 0,
            //                            Note = "Hủy",
            //                        };
            //                    if (mExportDetail.MachineId != null)
            //                    {
            //                        var smartProduction =
            //                            vfi.SmartProductions.FirstOrDefault(
            //                                sp => sp.MachineId == mExportDetail.MachineId);
            //                        entity.MachineId = mExportDetail.MachineId.Value;
            //                        entity.MachineName = mExportDetail.Machine.MachineName;
            //                        if (smartProduction != null)
            //                            if (smartProduction.ProductId != null)
            //                            {
            //                                var productPeriods =
            //                                    vfi.ProductInventoryPeriods.Where(
            //                                        pip =>
            //                                        pip.ProductId == smartProduction.ProductId &&
            //                                        pip.PeriodDate <= monthly &&
            //                                        warehouseIds.Contains(pip.WarehouseId));
            //                                entity.ProductId = smartProduction.ProductId.Value;
            //                                entity.ProductCode = smartProduction.Product.ProductCode;
            //                                entity.ProductForecastsQuality =
            //                                    smartProduction.Product.ForecastsQuality ?? 0;
            //                                entity.ProductWeight = smartProduction.Product.Weight ?? 0;
            //                                entity.TotalProductQuantity =
            //                                    productPeriods.Sum(pp => pp.LastPeriodQuantity) -
            //                                    productPeriods.Sum(pp => pp.EarlyPeriodQuantity);
            //                                entity.ProductionRate = smartProduction.Product.ProductionRate ?? 0;
            //                                entity.Note = "Chưa sản xuất";
            //                            }
            //                    }
            //                    if (entity.ProductionRate != 0)
            //                        entity.MaterialRequired = Math.Round((entity.ProductForecastsQuality -
            //                                                   entity.TotalProductQuantity)
            //                                                  / entity.ProductionRate, 2);
            //                    model.Add(entity);
            //                }
            //                else
            //                {
            //                    entity.MaterialExportQuantity += (mExportDetail.Quantity);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("materialperiod", ex.Message);
            //}
            return View(new GridModel(model.OrderBy(m => m.MaterialLotNumber)));
        }

        [GridAction]
        public ActionResult SelectProductionWeeklyReport(
            ////bool chkRemainingOrder,
            ////bool chkNextOder,
            ////bool chkDonHangThangKe,
            ////bool chkDonHangConLai,
            int customerId
            , string productName
            , string monthlyDate) {
            var model = new List<ProductionWeeklyModel>();
            try {
                //using (var vfi = new tammaContext())
                //{
                //    vfi.Configuration.LazyLoadingEnabled = false;
                //    var ci = new CultureInfo("vi-VN");
                //    var reportDate = string.IsNullOrWhiteSpace(monthlyDate)
                //                      ? DateTime.Today
                //                      : Convert.ToDateTime(monthlyDate, ci);
                //    var a = (int)(reportDate.DayOfWeek);
                //    if (a > 4)
                //        a = 4 - a + 7;
                //    else a = 4 - a;
                //    var monthly = reportDate.AddDays(a);
                //    var nextMonth = new DateTime(reportDate.Year, reportDate.Month, 1).AddMonths(1);
                //    var currentMonth = nextMonth.AddDays(-1);
                //    var startDate = OrderOption.GetStartOrderDate();
                //    var productIds = vfi.Products.Select(p => p.ProductId).ToList();
                //    var orderDetailNextMonth = (from od in vfi.OrderDetails
                //                                where (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                //                                      od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                //                                      od.Order.DueDate != null &&
                //                                      od.Order.DueDate > startDate &&
                //                                      od.Order.DueDate.Value.Month == nextMonth.Month &&
                //                                      od.Order.DueDate.Value.Year == nextMonth.Year
                //                                select new
                //                                {
                //                                    od.ProductId,
                //                                    od.Order.DueDate,
                //                                    od.OrderQty,
                //                                    od.RequiedNumber,
                //                                }).ToList();
                //    var orderDetailRemaining = (from od in vfi.OrderDetails
                //                                where (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                //                                      od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                //                                      od.Order.DueDate > startDate &&
                //                                      od.Order.DueDate != null &&
                //                                      od.Order.DueDate < currentMonth
                //                                select new
                //                                {
                //                                    od.ProductId,
                //                                    od.Order.DueDate,
                //                                    od.OrderQty,
                //                                    od.RequiedNumber,
                //                                }).ToList();
                //    var totalOrderDetails = orderDetailNextMonth.Union(orderDetailRemaining).ToList();
                //    productIds = totalOrderDetails.Select(od => od.ProductId).Distinct().ToList();
                //    var productInventorys = (from pi in vfi.ProductInventoryPeriods
                //                             where
                //                                 pi.WarehouseId != 1 &&
                //                                 pi.WarehouseId != 9 &&
                //                                 pi.WarehouseId != 11 &&
                //                                 pi.WarehouseId != 15 &&
                //                                 pi.PeriodDate < reportDate &&
                //                                 productIds.Contains(pi.ProductId)
                //                             select new
                //                             {
                //                                 pi.ProductId,
                //                                 pi.PeriodDate,
                //                                 pi.EarlyPeriodQuantity,
                //                                 pi.LastPeriodQuantity,
                //                             }).ToList();

                //    var dayZero = monthly.AddDays(-7);
                //    var dayOne = monthly.AddDays(-6);
                //    var dayTwo = monthly.AddDays(-5);
                //    var dayThree = monthly.AddDays(-3);
                //    var dayFour = monthly.AddDays(-2);
                //    var dayFive = monthly.AddDays(-1);
                //    var daySix = monthly;
                //    var productSX1Periods = (from pi in vfi.ProductInventoryPeriods
                //                             where pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                //                                   pi.PeriodDate > dayZero &&
                //                                   pi.PeriodDate <= reportDate &&
                //                                   pi.EarlyPeriodQuantity < pi.LastPeriodQuantity &&
                //                                   productIds.Contains(pi.ProductId)
                //                             select new
                //                             {
                //                                 pi.ProductId,
                //                                 pi.PeriodDate,
                //                                 pi.EarlyPeriodQuantity,
                //                                 pi.LastPeriodQuantity,
                //                                 pi.Quantity,
                //                             }).ToList();
                //    foreach (var productId in productIds)
                //    {
                //        var b = 5;
                //        if (model.Count == 375)
                //            b = 6;
                //        var entity = new ProductionWeeklyModel
                //        {
                //            OrderNext = 0,
                //            OrderLast = 0,
                //            OrderNextDate = null
                //        };
                //        var orderDetailByProductId = totalOrderDetails.Where(od => od.ProductId == productId);
                //        foreach (var orderDetail in orderDetailByProductId)
                //        {
                //            if (orderDetail.DueDate < currentMonth)
                //                entity.OrderLast += (orderDetail.RequiedNumber);
                //            else
                //                entity.OrderNext += (orderDetail.RequiedNumber);
                //            if (entity.OrderNextDate > orderDetail.DueDate || entity.OrderNextDate == null)
                //                entity.OrderNextDate = orderDetail.DueDate.Value;
                //        }
                //        var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                //        if (product == null)
                //            throw new AggregateException("Lỗi ! Không tìm thấy sản phẩm! " + productId);
                //        var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == product.MaterialId);
                //        if (material == null)
                //            throw new AggregateException("Lỗi ! Không tìm thấy nguyên liệu! " + product.MaterialId);
                //        entity.ProductId = productId;
                //        entity.ProductCode = product.ProductCode;
                //        var customer = vfi.Customers.FirstOrDefault(p => p.CustomerId == product.CustomerId);
                //        entity.CustomerCode = customer.CustomerCode;
                //        entity.MaterialId = product.MaterialId;
                //        entity.MaterialName = material.MaterialName;
                //        entity.MaterialDesignNo =MyUtilities.Material.GetMaterialDesignNo(material);
                //        entity.TotalProductInv =
                //            productInventorys.Where(pi => pi.ProductId == productId)
                //                             .Sum(pi => pi.LastPeriodQuantity) -
                //            productInventorys.Where(pi => pi.ProductId == productId)
                //                             .Sum(pi => pi.EarlyPeriodQuantity);
                //        entity.OrderLastRemaining = entity.TotalProductInv - entity.OrderLast;
                //        if (entity.OrderLastRemaining > 0)
                //        {
                //            entity.OrderNextRemaining = entity.OrderLastRemaining - entity.OrderNext;
                //            if (entity.OrderNextRemaining > 0)
                //                entity.OrderNextRemaining = 0;
                //            entity.OrderLastRemaining = 0;
                //        }
                //        else
                //            entity.OrderNextRemaining -= entity.OrderNext;
                //        var productSX1PeriodsByProductId = productSX1Periods.Where(p => p.ProductId == productId).ToList();
                //        entity.DayOne = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate == dayOne)
                //            .Sum(pip => pip.Quantity);
                //        entity.DayTwo = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate== dayTwo)
                //            .Sum(pip => pip.Quantity);
                //        entity.DayThree = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate == dayThree)
                //            .Sum(pip => pip.Quantity);
                //        entity.DayFour = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate == dayFour)
                //            .Sum(pip => pip.Quantity);
                //        entity.DayFive = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate == dayFive)
                //            .Sum(pip => pip.Quantity);
                //        entity.DaySix = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate == daySix)
                //            .Sum(pip => pip.Quantity);
                //        model.Add(entity);
                //    }
                //}
            }
            catch (Exception ex) {
                ModelState.AddModelError("ProductionWeeklyReport", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }

        [HttpPost]
        public ActionResult PrintProductionWeeklyReport(
            //bool chkRemainingOrder,
            //bool chkNextOder,
            //bool chkDonHangThangKe,
            //bool chkDonHangConLai,
            string monthlyDate) {
            var model = new List<ProductionWeeklyModel>();
            try {
                //using (var vfi = new tammaContext())
                //{
                //    vfi.Configuration.LazyLoadingEnabled = false;
                //    var ci = new CultureInfo("vi-VN");
                //    var reportDate = string.IsNullOrWhiteSpace(monthlyDate)
                //                      ? DateTime.Today
                //                      : Convert.ToDateTime(monthlyDate, ci);
                //    var a = (int)(reportDate.DayOfWeek);
                //    if (a > 4)
                //        a = 4 - a + 7;
                //    else a = 4 - a;
                //    var monthly = reportDate.AddDays(a);
                //    var nextMonth = new DateTime(reportDate.Year, reportDate.Month, 1).AddMonths(1);
                //    var currentMonth = nextMonth.AddSeconds(-1);
                //    var startDate = OrderOption.GetStartOrderDate();
                //    var productIds = vfi.Products.Select(p => p.ProductId).ToList();
                //    var orderDetailNextMonth = (from od in vfi.OrderDetails
                //                                where (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                //                                      od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                //                                      od.Order.DueDate != null &&
                //                                      od.Order.DueDate > startDate &&
                //                                      od.Order.DueDate.Value.Month == nextMonth.Month &&
                //                                      od.Order.DueDate.Value.Year == nextMonth.Year
                //                                select new
                //                                    {
                //                                        od.ProductId,
                //                                        od.Order.DueDate,
                //                                        od.OrderQty,
                //                                        od.RequiedNumber,
                //                                    }).ToList();
                //    var orderDetailRemaining = (from od in vfi.OrderDetails
                //                                where (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                //                                      od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                //                                      od.Order.DueDate > startDate &&
                //                                      od.Order.DueDate != null &&
                //                                      od.Order.DueDate < currentMonth
                //                                select new
                //                                {
                //                                    od.ProductId,
                //                                    od.Order.DueDate,
                //                                    od.OrderQty,
                //                                    od.RequiedNumber,
                //                                }).ToList();
                //    var totalOrderDetails = orderDetailNextMonth.Union(orderDetailRemaining).ToList();
                //    productIds = totalOrderDetails.Select(od => od.ProductId).Distinct().ToList();
                //    var productInventorys = (from pi in vfi.ProductInventoryPeriods
                //                             where
                //                                 pi.WarehouseId != 1 &&
                //                                 pi.WarehouseId != 9 &&
                //                                 pi.WarehouseId != 11 &&
                //                                 pi.WarehouseId != 15 &&
                //                                 pi.PeriodDate < reportDate &&
                //                                 productIds.Contains(pi.ProductId)
                //                             select new
                //                             {
                //                                 pi.ProductId,
                //                                 pi.PeriodDate,
                //                                 pi.EarlyPeriodQuantity,
                //                                 pi.LastPeriodQuantity,
                //                             }).ToList();

                //    var dayZero = monthly.AddDays(-7);
                //    var dayOne = monthly.AddDays(-6);
                //    var dayTwo = monthly.AddDays(-5);
                //    var dayThree = monthly.AddDays(-3);
                //    var dayFour = monthly.AddDays(-2);
                //    var dayFive = monthly.AddDays(-1);
                //    var daySix = monthly;
                //    var productSX1Periods = (from pi in vfi.ProductInventoryPeriods
                //                             where pi.WarehouseId == MyUtilities.Warehouse.Production1 &&
                //                                   pi.PeriodDate > dayZero &&
                //                                   pi.PeriodDate <= reportDate &&
                //                                   pi.EarlyPeriodQuantity < pi.LastPeriodQuantity &&
                //                                   productIds.Contains(pi.ProductId)
                //                             select new
                //                             {
                //                                 pi.ProductId,
                //                                 pi.PeriodDate,
                //                                 pi.EarlyPeriodQuantity,
                //                                 pi.LastPeriodQuantity,
                //                                 pi.Quantity,
                //                             }).ToList();
                //    foreach (var productId in productIds)
                //    {
                //        var b = 5;
                //        if (model.Count == 375)
                //            b = 6;
                //        var entity = new ProductionWeeklyModel
                //        {
                //            OrderNext = 0,
                //            OrderLast = 0,
                //            OrderNextDate = null
                //        };
                //        var orderDetailByProductId = totalOrderDetails.Where(od => od.ProductId == productId);
                //        foreach (var orderDetail in orderDetailByProductId)
                //        {
                //            if (orderDetail.DueDate < currentMonth)
                //                entity.OrderLast += (orderDetail.RequiedNumber);
                //            else
                //                entity.OrderNext += (orderDetail.RequiedNumber);
                //            if (entity.OrderNextDate > orderDetail.DueDate || entity.OrderNextDate == null)
                //                entity.OrderNextDate = orderDetail.DueDate.Value;
                //        }
                //        //if (chkNextOder || chkRemainingOrder)
                //        //{
                //        //    if (chkNextOder && entity.OrderNext != 0)
                //        //    {
                //        //        goto part2;
                //        //    }
                //        //    if (chkRemainingOrder && entity.OrderLast != 0)
                //        //    {
                //        //        goto part2;
                //        //    }
                //        //    continue;
                //        //}
                //        //part2:
                //        var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                //        if (product == null)
                //            throw new AggregateException("Lỗi ! Không tìm thấy sản phẩm! " + productId);
                //        var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == product.MaterialId);
                //        if (material == null)
                //            throw new AggregateException("Lỗi ! Không tìm thấy nguyên liệu! " + product.MaterialId);
                //        entity.ProductId = productId;
                //        entity.ProductCode = product.ProductCode;
                //        var customer = vfi.Customers.FirstOrDefault(p => p.CustomerId == product.CustomerId);
                //        entity.CustomerCode = customer.CustomerCode;
                //        entity.MaterialId = product.MaterialId;
                //        entity.MaterialName = material.MaterialName;
                //        entity.MaterialDesignNo =MyUtilities.Material.GetMaterialDesignNo(material);
                //        entity.TotalProductInv =
                //            productInventorys.Where(pi => pi.ProductId == productId)
                //                             .Sum(pi => pi.LastPeriodQuantity) -
                //            productInventorys.Where(pi => pi.ProductId == productId)
                //                             .Sum(pi => pi.EarlyPeriodQuantity);
                //        entity.OrderLastRemaining = entity.TotalProductInv - entity.OrderLast;
                //        if (entity.OrderLastRemaining > 0)
                //        {
                //            entity.OrderNextRemaining = entity.OrderLastRemaining - entity.OrderNext;
                //            if (entity.OrderNextRemaining > 0)
                //                entity.OrderNextRemaining = 0;
                //            entity.OrderLastRemaining = 0;
                //        }
                //        else
                //            entity.OrderNextRemaining -= entity.OrderNext;
                //        var productSX1PeriodsByProductId = productSX1Periods.Where(p => p.ProductId == productId).ToList();
                //        entity.DayOne = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate == dayOne)
                //            .Sum(pip => pip.Quantity);
                //        entity.DayTwo = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate == dayTwo)
                //            .Sum(pip => pip.Quantity);
                //        entity.DayThree = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate == dayThree)
                //            .Sum(pip => pip.Quantity);
                //        entity.DayFour = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate == dayFour)
                //            .Sum(pip => pip.Quantity);
                //        entity.DayFive = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate == dayFive)
                //            .Sum(pip => pip.Quantity);
                //        entity.DaySix = productSX1PeriodsByProductId
                //            .Where(
                //                pip => pip.PeriodDate == daySix)
                //            .Sum(pip => pip.Quantity);
                //        model.Add(entity);
                //    }
                //}
            }
            catch (Exception ex) {
                ModelState.AddModelError("ProductionWeeklyReport", ex.Message);
            }
            return PartialView("PageProductionWeeklySX1", model.OrderBy(p => p.ProductCode).ToList());
        }
        #endregion

        #region Inventory management

        #region Kho CNC
        [GridAction]
        public ActionResult SelectCncInvManagement(
            int customerId,
            //, int warehouseIssue, int warehouseReceipt
            string productCode,
            string fromDate,
            string toDate) {
            var model = new List<CncInvModel>();
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseLastDateTime(toDate);
            try {
                //var warehouseId = MyUtilities.Warehouse.Cnc;
                using (var vfi = new tammaContext()) {
                    //lay DS san pham da tung luu tru trong kho
                    var products = (from pi in vfi.ProductInventories
                                    where pi.Warehouse.IsCncMilling && pi.Product.Active
                                    orderby pi.Product.Customer.CustomerCode, pi.Product.ProductCode
                                    select new {
                                        pi.ProductId,
                                        pi.Product.ProductCode,
                                        pi.Product.CncWeight,
                                        pi.Product.CustomerId,
                                        pi.Product.Customer.CustomerCode,
                                    }).ToList();
                    if (customerId != 0)
                        products = products.Where(p => p.CustomerId == customerId).ToList();
                    if (!string.IsNullOrWhiteSpace(productCode))
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    var productIds = products.Select(p => p.ProductId).Distinct();
                    // lay thong tin luan chuyen toi ngay bao cao
                    var periods = (from pip in vfi.ProductInventoryPeriods
                                   where productIds.Contains(pip.ProductId)
                                         && pip.PeriodDate <= tDate &&
                                pip.Warehouse.IsCncMilling
                                   select new {
                                       pip.ProductId,
                                       PeriodQuantity = pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                       IoE = pip.LastPeriodQuantity > pip.EarlyPeriodQuantity,
                                       pip.PeriodDate,
                                       WarehouseIssue = pip.Transaction.Warehouse,
                                       WarehouseReceipt = pip.Transaction.Warehouse1,
                                       pip.Transaction.WarehouseIssueId,
                                       pip.Transaction.WarehouseReceiptId,
                                       pip.Quantity
                                   }).ToList();

                    var imports = periods.Where(pip => pip.IoE && pip.PeriodDate >= fDate)
                                         .ToList();
                    var exports = periods.Where(pip => !pip.IoE && pip.PeriodDate >= fDate)
                                         .ToList();

                    //var production2Ids = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    //var processingInvIds = MyUtilities.Warehouse.GetWarehouseId_ReProcessing();
                    var i = 0;
                    foreach (var productId in productIds) {
                        var product = products.FirstOrDefault(p => p.ProductId == productId);
                        var entity = new CncInvModel {
                            GlobalIndex = ++i,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.CncWeight ?? 0,
                        };

                        entity.EarlyInventory = periods.Where(pip => pip.PeriodDate < fDate && pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        entity.LastInventory = periods.Where(pip => pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        //
                        var importsById = imports.Where(x => x.ProductId == entity.ProductId).ToList();
                        entity.ImportSx1 = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsProduction)
                             .Sum(pip => pip.Quantity);
                        entity.ImportSx2 = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsProduction2)
                             .Sum(pip => pip.Quantity);
                        entity.TotalImport = importsById.Sum(x => x.Quantity);
                        //
                        var exportsById = exports.Where(x => x.ProductId == entity.ProductId).ToList();
                        entity.ExportSx2 = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsProduction2)
                             .Sum(pip => pip.Quantity);
                        entity.ExportQcA = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsQC)
                            .Sum(pip => pip.Quantity);
                        entity.ExportCxl1 = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsReprocessing)
                            .Sum(pip => pip.Quantity);
                        entity.ExportPp = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsDefect == true)
                             .Sum(pip => pip.Quantity);
                        entity.TotalExport = exportsById.Sum(pip => pip.Quantity);

                        if (entity.Show)
                            model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectCncInvManagement", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }
        #endregion

        #region Kho SX2

        [GridAction]
        public ActionResult SelectProduction2WaitingInvManagement(
            int customerId, string productCode,
           string fromDate, string toDate) {
            if (customerId < 0) {
                return View(new GridModel(new List<Production2WaitingInvModel>()));
            }

            var model = new List<Production2WaitingInvModel>();

            try {
                var fDate = MyUtilities.Function.ParseDate(fromDate);
                var tDate = MyUtilities.Function.ParseLastDateTime(toDate);
                using (var vfi = new tammaContext()) {
                    var products = (from pi in vfi.ProductInventories
                                    where
                                        pi.WarehouseId == MyUtilities.Warehouse.Production2 &&
                                        (customerId == 0 || pi.Product.CustomerId == customerId) &&
                                        (pi.TotalQty > 0 || pi.ExportDate <= tDate || pi.ExportDate == null)
                                    select new {
                                        pi.ProductId,
                                        pi.Product.ProductCode,
                                        pi.Product.Production2Weight,
                                        pi.Product.CustomerId,
                                        pi.Product.Customer.CustomerCode,
                                    }).Distinct().ToList();
                    if (!string.IsNullOrWhiteSpace(productCode))
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    var productIds = products.Select(p => p.ProductId).Distinct().ToList();
                    // lay thong tin luan chuyen toi ngay bao cao
                    var productInvPeriods = (from pip in vfi.ProductInventoryPeriods
                                             where productIds.Contains(pip.ProductId)
                                                   && pip.PeriodDate <= tDate
                                                   && pip.WarehouseId == MyUtilities.Warehouse.Production2
                                             select new {
                                                 pip.ProductId,
                                                 PeriodQuantity = pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                                 pip.PeriodDate,
                                                 //pip.Transaction,
                                                 pip.Transaction.WarehouseIssueId,
                                                 pip.Transaction.WarehouseReceiptId,
                                                 pip.TransactionId,
                                                 pip.WarehouseId,
                                                 pip.Quantity
                                             }).ToList();
                    var importPeriods =
                        productInvPeriods.Where(pip => pip.WarehouseId == MyUtilities.Warehouse.Production2
                                                       && pip.WarehouseReceiptId == MyUtilities.Warehouse.Production2
                                                       && pip.PeriodDate >= fDate)
                                         .ToList();
                    var exportPeriods =
                        productInvPeriods.Where(pip => pip.WarehouseId == MyUtilities.Warehouse.Production2
                                                       && pip.WarehouseIssueId == MyUtilities.Warehouse.Production2
                                                       && pip.PeriodDate >= fDate)
                                         .ToList();
                    var production2Warehouses = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    foreach (var product in products) {
                        var entity = new Production2WaitingInvModel {
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.Production2Weight ?? 0,
                            ReportDate = tDate
                        };
                        entity.EarlyInventory = productInvPeriods.Where(pip => pip.PeriodDate < fDate && pip.ProductId == entity.ProductId)
                                                                 .Sum(pip => pip.PeriodQuantity);
                        var importPeriodsById = importPeriods.Where(pip => pip.ProductId == entity.ProductId);
                        entity.ImportSx1 = importPeriodsById
                            .Where(pip => pip.WarehouseIssueId == MyUtilities.Warehouse.Production1)
                            .Sum(pip => pip.Quantity);
                        entity.ImportCnc = importPeriodsById
                            .Where(pip => pip.WarehouseIssueId == MyUtilities.Warehouse.Cnc)
                            .Sum(pip => pip.Quantity);
                        entity.ImportSx2Process = importPeriodsById
                            .Where(pip => pip.WarehouseIssueId != null && production2Warehouses.Contains(pip.WarehouseIssueId.Value))
                            .Sum(pip => pip.Quantity);
                        entity.TotalImport = importPeriodsById.Sum(pip => pip.Quantity);
                        entity.ImportElse = entity.TotalImport - entity.ImportSx1 - entity.ImportCnc - entity.ImportSx2Process;
                        //
                        var exportPeriodsById = exportPeriods.Where(pip => pip.ProductId == entity.ProductId);
                        entity.ExportSx2B = exportPeriodsById
                            .Where(pip => pip.WarehouseReceiptId == MyUtilities.Warehouse.Production2B)
                            .Sum(pip => pip.Quantity);
                        entity.ExportSx2C = exportPeriodsById
                            .Where(pip => pip.WarehouseReceiptId == MyUtilities.Warehouse.Production2C)
                            .Sum(pip => pip.Quantity);
                        entity.ExportSx2D = exportPeriodsById
                            .Where(pip => pip.WarehouseReceiptId == MyUtilities.Warehouse.Production2D)
                            .Sum(pip => pip.Quantity);

                        entity.ExportCxl1 = exportPeriodsById
                            .Where(pip => (pip.WarehouseReceiptId == MyUtilities.Warehouse.Processing ||
                                           pip.WarehouseReceiptId == MyUtilities.Warehouse.Processing2))
                            .Sum(pip => pip.Quantity);
                        entity.TotalExport = exportPeriodsById.Sum(pip => pip.Quantity);
                        entity.ExportElse = entity.TotalExport - entity.ExportSx2B - entity.ExportSx2C - entity.ExportSx2D - entity.ExportCxl1;

                        entity.LastInventory = productInvPeriods.Where(pip => pip.ProductId == entity.ProductId)
                                                                .Sum(pip => pip.PeriodQuantity);
                        if (entity.Show)
                            model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProduction2WaitingInvManagement", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }

        [GridAction]
        public ActionResult SelectProduction2InvManagement(
            int customerId,
            //, int warehouseIssue, int warehouseReceipt
            string productCode,
            int warehouseId,
           string fromDate, string toDate) {

            if (warehouseId == -1)
                return View(new GridModel(new List<Production2InvModel>()));
            var model = new List<Production2InvModel>();
            try {
                model =
                    GetProduction2Period(customerId, productCode, warehouseId, fromDate, toDate)
                    .Where(m => m.Show)
                    .ToList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProduction2InvManagement", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }

        List<Production2InvModel> GetProduction2Period(int customerId, string productCode,
            int warehouseId, string fromDate, string toDate) {
            var model = new List<Production2InvModel>();
            if (warehouseId == -1) return model;
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseLastDateTime(toDate);
            //var startMonth = new DateTime(reportDate.Year, reportDate.Month, 1);
            using (var vfi = new tammaContext()) {
                //var production2Ids = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                string name = "";
                if (warehouseId > 0) {
                    //production2Ids = new List<int> { warehouseId };
                    name = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == warehouseId).WarehouseName;
                }
                else {
                    name = vfi.Warehouses.Where(w => w.IsProduction2).OrderBy(x => x.Idx).FirstOrDefault().WarehouseName;
                }
                //if (warehouseId != MyUtilities.Warehouse.Production2) {
                //    production2Ids = new List<int> { warehouseId };
                //    name = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == warehouseId).WarehouseName;
                //}
                //else {
                //    name = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == MyUtilities.Warehouse.Production2).WarehouseName;
                //}
                //lay DS san pham da tung luu tru trong kho
                var products = (from pi in vfi.ProductInventories
                                where
                                    pi.Warehouse.IsProduction2
                                    && (customerId == 0 || pi.Product.CustomerId == customerId)
                                select new {
                                    pi.ProductId,
                                    pi.Product.ProductCode,
                                    pi.Product.Production2Weight,
                                    pi.Product.CustomerId,
                                    pi.Product.Customer.CustomerCode,
                                }).Distinct().ToList();
                if (!string.IsNullOrWhiteSpace(productCode))
                    products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                var productIds = products.Select(p => p.ProductId).Distinct();
                // lay thong tin luan chuyen toi ngay bao cao
                //var allWarehouseProduction2 = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                var periods = (from pip in vfi.ProductInventoryPeriods
                               where productIds.Contains(pip.ProductId)
                                     && pip.PeriodDate <= tDate
                                     && (warehouseId == 0 || pip.Warehouse.IsProduction2)
                               select new {
                                   pip.ProductId,
                                   PeriodQuantity = pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                   IoE = pip.LastPeriodQuantity > pip.EarlyPeriodQuantity,
                                   pip.PeriodDate,
                                   WarehouseIssue = pip.Transaction.Warehouse,
                                   WarehouseReceipt = pip.Transaction.Warehouse1,
                                   pip.Transaction.WarehouseIssueId,
                                   pip.Transaction.WarehouseReceiptId,
                                   pip.Quantity
                               }).ToList();
                var imports = periods.Where(pip => pip.IoE && pip.PeriodDate >= fDate)
                                     .ToList();
                var exports = periods.Where(pip => !pip.IoE && pip.PeriodDate >= fDate)
                                     .ToList();
                //productIds =
                //    periods.Where(pip =>
                //                            production2Ids.Contains(pip.WarehouseId) &&
                //                            pip.PeriodDate.Month == tDate.Month &&
                //                            pip.PeriodDate.Year == tDate.Year)
                //                     .Select(pip => pip.ProductId)
                //                     .Distinct()
                //                     .ToList();

                products = products.OrderBy(p => p.CustomerCode).ThenBy(p => p.ProductCode).ToList();
                foreach (var product in products) {
                    var entity = new Production2InvModel {
                        //GlobalIndex = i,
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        CustomerCode = product.CustomerCode,
                        Weight = product.Production2Weight ?? 0,
                        Name = name,
                        ReportDate = tDate,
                        Show = false
                    };
                    //if (entity.ProductCode.Equals("HA6"))
                    //    i = 1;
                    entity.EarlyInventory = periods.Where(pip => pip.PeriodDate < fDate && pip.ProductId == entity.ProductId)
                                                    .Sum(pip => pip.PeriodQuantity);
                    entity.LastInventory = periods.Where(pip => pip.ProductId == entity.ProductId)
                                                    .Sum(pip => pip.PeriodQuantity);

                    var importsById = imports.Where(x => x.ProductId == entity.ProductId).ToList();
                    entity.TotalImport = importsById.Sum(x => x.Quantity);
                    entity.ImportSx1 = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsProduction)
                                                .Sum(pip => pip.Quantity);
                    entity.ImportSx2 = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsProduction2)
                                                .Sum(pip => pip.Quantity);
                    entity.ImportCnc = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsCncMilling)
                                                .Sum(pip => pip.Quantity);
                    entity.ImportCxl1 = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsReprocessing)
                                                .Sum(pip => pip.Quantity);
                    //entity.TotalImport = entity.ImportSx1 + entity.ImportCnc + entity.ImportCxl1 + entity.ImportCxl2;
                    //
                    var exportsById = exports.Where(x => x.ProductId == entity.ProductId).ToList();
                    entity.TotalExport = exportsById.Sum(pip => pip.Quantity);
                    entity.ExportSx2 = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsProduction2)
                                                .Sum(pip => pip.Quantity);
                    entity.ExportQcA = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsQC)
                                                .Sum(pip => pip.Quantity);
                    entity.ExportCxl1 = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsReprocessing)
                                                .Sum(pip => pip.Quantity);
                    entity.ExportGcn = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsPlating)
                                                .Sum(pip => pip.Quantity);
                    entity.ExportRb = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsPolish)
                                                .Sum(pip => pip.Quantity);
                    entity.ExportDefect = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsDefect == true)
                                                .Sum(pip => pip.Quantity);

                    if (entity.TotalExport == 0 && entity.TotalImport == 0 &&
                        entity.EarlyInventory == 0 && entity.LastInventory == 0 &&
                        entity.DiffQuantity == 0) {
                    }
                    else {
                        entity.Show = true;
                    }
                    //if (entity.Show) {
                    //    var transactionDetailsById = transactionDetails.Where(td => td.ReferenceId == entity.ProductId);
                    //    foreach (var transactionDetail in transactionDetailsById) {
                    //        if (!string.IsNullOrWhiteSpace(transactionDetail.Note))
                    //            entity.Note += transactionDetail.Note + "(" + string.Format("{0:n0}", transactionDetail.Quantity) + ")! ";
                    //    }
                    //}
                    model.Add(entity);
                }
            }
            return model;

        }


        [GridAction]
        public ActionResult SelectProduction2ProcessingInvManagement(
            int customerId,
            string productCode,
            int warehouseId,
           string fromDate, string toDate) {
            var model = new List<Production2InvModel>();

            try {
                model =
                    GetProduction2ProcessingPeriod(customerId, productCode, warehouseId, fromDate, toDate).ToList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProduction2ProcessingInvManagement", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }

        List<Production2InvModel> GetProduction2ProcessingPeriod(int customerId, string productCode,
            int warehouseId, string fromDate, string toDate) {
            var model = new List<Production2InvModel>();
            if (warehouseId == -1)
                return model;
            var ci = new CultureInfo("vi-VN");
            var fDate = string.IsNullOrWhiteSpace(fromDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(toDate, ci);
            //var startMonth = new DateTime(reportDate.Year, reportDate.Month, 1);

            using (var vfi = new tammaContext()) {
                var production2Ids = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                string name = "";
                if (warehouseId != MyUtilities.Warehouse.Production2) {
                    production2Ids = new List<int> { warehouseId };
                    name = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == warehouseId).WarehouseName;
                }
                else {
                    name = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == MyUtilities.Warehouse.Production2).WarehouseName;
                }
                //lay DS san pham da tung luu tru trong kho
                var products = (from pi in vfi.ProductInventories
                                where
                                    //production2Ids.Contains(pi.WarehouseId) ||
                                    pi.WarehouseId == MyUtilities.Warehouse.Production2
                                select new {
                                    pi.ProductId,
                                    pi.Product.ProductCode,
                                    pi.Product.Production2Weight,
                                    pi.Product.CustomerId,
                                    pi.Product.Customer.CustomerCode,
                                }).Distinct().ToList();
                if (customerId > 0)
                    products = products.Where(p => p.CustomerId == customerId).ToList();
                if (!string.IsNullOrWhiteSpace(productCode))
                    products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                var productIds = products.Select(p => p.ProductId).Distinct();
                // lay thong tin luan chuyen toi ngay bao cao
                var allWarehouseProduction2 = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                var productInvPeriods = (from pip in vfi.ProductInventoryPeriods
                                         where productIds.Contains(pip.ProductId)
                                               && pip.PeriodDate <= tDate
                                               && pip.WarehouseId == warehouseId
                                         select new {
                                             pip.ProductId,
                                             PeriodQuantity =
                                         pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                             pip.PeriodDate,
                                             pip.Transaction,
                                             pip.TransactionId,
                                             pip.WarehouseId,
                                             pip.Quantity
                                         }).ToList();
                var importPeriodInMonths =
                    productInvPeriods.Where(pip => pip.Transaction.WarehouseReceiptId == warehouseId &&
                                                    pip.PeriodDate >= fDate)
                                     .ToList();
                var exportPeriodInMonths =
                    productInvPeriods.Where(pip => pip.Transaction.WarehouseIssueId == warehouseId &&
                                                    pip.PeriodDate >= fDate)
                                     .ToList();
                //var exportPeriodInDays =
                //    exportPeriodInMonths.Where(pip => pip.PeriodDate == tDate).ToList();
                //var transactionIds = exportPeriodInDays.Select(t => t.TransactionId).Distinct().ToList();
                //var transactionDetails = from td in vfi.TransactionDetails
                //                         where transactionIds.Contains(td.TransactionId.Value)
                //                         select td;
                //productIds =
                //    productInvPeriods.Where(pip =>
                //                            production2Ids.Contains(pip.WarehouseId) &&
                //                            pip.PeriodDate.Month == tDate.Month &&
                //                            pip.PeriodDate.Year == tDate.Year)
                //                     .Select(pip => pip.ProductId)
                //                     .Distinct()
                //                     .ToList();

                var i = 1;
                products = products.OrderBy(p => p.CustomerCode).ThenBy(p => p.ProductCode).ToList();
                foreach (var product in products) {
                    var entity = new Production2InvModel {
                        GlobalIndex = i,
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        CustomerCode = product.CustomerCode,
                        Weight = product.Production2Weight ?? 0,
                        Name = name,
                        ReportDate = tDate,
                        Show = false
                    };
                    //
                    entity.EarlyInventory = productInvPeriods.Where(
                        pip => pip.PeriodDate < fDate && pip.ProductId == entity.ProductId)
                                                             .Sum(pip => pip.PeriodQuantity);
                    entity.LastInventory = productInvPeriods.Where(
                        pip => pip.ProductId == entity.ProductId)
                                                            .Sum(pip => pip.PeriodQuantity);
                    // nhap tu sx2
                    entity.ImportSx2 = importPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId &&
                                    pip.Transaction.WarehouseIssueId != null &&
                                    allWarehouseProduction2.Contains(pip.Transaction.WarehouseIssueId.Value))
                        .Sum(pip => pip.Quantity);
                    // nhap thêm
                    entity.TotalImport = importPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId).Sum(pip => pip.Quantity);
                    //entity.ImportElse = importPeriodInMonths
                    //    .Where(pip => pip.ProductId == entity.ProductId &&
                    //                pip.Transaction.WarehouseIssueId == null)
                    //    .Sum(pip => pip.Quantity);
                    // kiem tra nguoc - tổng nhập ghi nhận
                    entity.DiffImport = importPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId)
                        .Sum(pip => pip.Quantity);
                    entity.DiffImport -= entity.TotalImport;
                    //
                    entity.ExportSx2 = exportPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId &&
                                    pip.Transaction.WarehouseReceiptId != null &&
                                    allWarehouseProduction2.Contains(pip.Transaction.WarehouseReceiptId.Value))
                        .Sum(pip => pip.Quantity);
                    entity.ExportCnc = exportPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId &&
                                      (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Cnc))
                        .Sum(pip => pip.Quantity);
                    entity.ExportNl = exportPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId &&
                                      (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.HeatTreatment))
                        .Sum(pip => pip.Quantity);
                    entity.ExportRb = exportPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId &&
                                      (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.SurfaceTreatment))
                        .Sum(pip => pip.Quantity);
                    entity.ExportGcn = exportPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId &&
                                      (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.WaitingPlating))
                        .Sum(pip => pip.Quantity);
                    entity.ExportQcA = exportPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId &&
                                      (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcA ||
                                      pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcB))
                        .Sum(pip => pip.Quantity);
                    entity.ExportCxl1 = exportPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId &&
                                      (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing ||
                                       pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing2))
                        .Sum(pip => pip.Quantity);
                    entity.ExportDefect = exportPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId &&
                                      (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Defect))
                        .Sum(pip => pip.Quantity);
                    entity.TotalExport = exportPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId)
                        .Sum(pip => pip.Quantity);
                    //entity.ExportElse = exportPeriodInMonths
                    //    .Where(pip => pip.ProductId == entity.ProductId &&
                    //                  (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Tranfer ||
                    //                  pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Destroy))
                    //    .Sum(pip => pip.Quantity);
                    // kiem tra nguoc - tổng xuất ghi nhận
                    entity.DiffExport = exportPeriodInMonths
                        .Where(pip => pip.ProductId == entity.ProductId)
                        .Sum(pip => pip.Quantity);
                    entity.DiffExport -= entity.TotalExport;

                    if (entity.EarlyInventory + entity.TotalImport + entity.TotalExport + entity.LastInventory + entity.DiffImport + entity.DiffExport > 0)
                        model.Add(entity);
                }
            }
            return model;

        }

        [HttpPost]
        public ActionResult PrintProduction2Period(int warehouseId, string fromDate, string toDate) {
            var model = new List<Production2InvModel>();
            try {
                model = GetProduction2Period(-1, "", warehouseId, fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintProduction2Period", ex.Message);
            }
            return PartialView("PageProduction2Period", model.OrderBy(p => p.CustomerCode).ThenBy(p => p.ProductCode).ToList());
        }
        [HttpPost]
        public ActionResult PrintProduction2List(string from, string to, bool isSales, bool isForecast) {
            var model = new List<ProductModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fromDate = string.IsNullOrWhiteSpace(from)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(from, ci);
                var toDate = string.IsNullOrWhiteSpace(to)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(to, ci);
                using (var vfi = new tammaContext()) {
                    var production2Ids = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    var productIds = (from pi in vfi.ProductInventories
                                      where production2Ids.Contains(pi.WarehouseId)
                                      select pi.ProductId).ToList();

                    var productionSections = from ps in vfi.ProductionSections
                                             where
                                                 //productIds.Contains(ps.ProductId.Value)&&
                                             ps.Active
                                             select new {
                                                 ps.ProductId,
                                                 ps.Section.SectionName,
                                                 //SectionCost = ps.Productivity * ps.Section.SaleFactor,
                                                 ps.Productivity,
                                             };
                    productIds = productIds.Union(productionSections.Select(od => od.ProductId))
                                           .Distinct()
                                           .ToList();

                    var products = from p in vfi.Products
                                   where p.Active &&
                                   productIds.Contains(p.ProductId)
                                   //&& p.ProductCode.Contains("301")
                                   //&&(customerId == 0 || p.CustomerId == customerId) &&
                                   //      (flag || p.ProductCode.Contains(productName))
                                   select p;
                    productIds = products.Select(p => p.ProductId).Distinct().ToList();
                    var allProductInventoryPeriodsByDate = (from pip in vfi.ProductInventoryPeriods
                                                            where
                                                                pip.PeriodDate >= fromDate &&
                                                                pip.PeriodDate <= toDate &&
                                                                productIds.Contains(pip.ProductId)
                                                            select new {
                                                                pip.Product.CustomerId,
                                                                pip.ProductId,
                                                                pip.Quantity,
                                                                pip.LastPeriodQuantity,
                                                                pip.EarlyPeriodQuantity,
                                                                pip.PeriodDate,
                                                                pip.WarehouseId,
                                                                pip.Transaction,
                                                            }).ToList();
                    //var productIds = allProductInventoryPeriodsByDate.Select(p => p.ProductId).ToList();
                    var giaodichKd = (from p in allProductInventoryPeriodsByDate
                                      where
                                          p.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Finish &&
                                          p.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Business &&
                                          p.WarehouseId == MyUtilities.Warehouse.Business &&
                                          (p.PeriodDate.Month >= fromDate.Month &&
                                          p.PeriodDate.Year >= fromDate.Year)
                                      select p).ToList();
                    var inMonth = new DateTime(toDate.Year, toDate.Month, 1).AddSeconds(-1);
                    if (inMonth < fromDate)
                        throw new AggregateException("Lỗi!");
                    var orderDetails = (from od in vfi.OrderDetails
                                        where od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                        od.Order.Status != (byte)MyUtilities.Sales.Status.Completed &&
                                            //od.Order.DueDate >= fromDate &&
                                              od.Order.DueDate <= inMonth &&
                                              od.Order.DueDate != null &&
                                              od.RequiedNumber != 0 &&
                                              productIds.Contains(od.ProductId)
                                        select new {
                                            od.ProductId,
                                            od.Order.DueDate,
                                            od.Order.ModifiedDate,
                                            od.OrderQty,
                                            od.RequiedNumber,
                                        }).ToList();
                    var orderDetailsInMonth = (from od in vfi.OrderDetails
                                               where od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                        od.Order.Status != (byte)MyUtilities.Sales.Status.Completed &&
                                                     od.Order.DueDate.Value.Month == toDate.Month &&
                                                     od.Order.DueDate.Value.Year == toDate.Year &&
                                                     od.Order.DueDate != null &&
                                              od.RequiedNumber != 0 &&
                                                     productIds.Contains(od.ProductId)
                                               select new {
                                                   od.ProductId,
                                                   od.Order.DueDate,
                                                   od.OrderQty,
                                                   od.RequiedNumber,
                                               }).ToList();
                    var forecasts = from f in vfi.ForecastOrders
                                    where productIds.Contains(f.ProductId)
                                    && f.ForecastDate.Month == toDate.Month
                                    && f.ForecastDate.Year == toDate.Year
                                    && f.IsSelling
                                    select f;
                    var nextMonth = toDate.AddMonths(1);
                    var forecastNextMonths = from f in vfi.ForecastOrders
                                             where productIds.Contains(f.ProductId)
                                             && f.ForecastDate.Month == nextMonth.Month
                                             && f.ForecastDate.Year == nextMonth.Year
                                             && f.IsSelling
                                             select f;
                    if (isSales && isForecast) {
                        productIds =
                            giaodichKd.Select(od => od.ProductId)
                                      .Union(forecasts.Select(od => od.ProductId))
                                      .Union(forecastNextMonths.Select(od => od.ProductId))
                                      .Union(orderDetails.Select(od => od.ProductId))
                                      .Union(orderDetailsInMonth.Select(od => od.ProductId))
                                      .Distinct()
                                      .ToList();
                    }
                    else if (isSales)
                        productIds = giaodichKd.Select(od => od.ProductId)
                                               .Union(orderDetails.Select(od => od.ProductId))
                                               .Union(orderDetailsInMonth.Select(od => od.ProductId))
                                               .Distinct()
                                               .ToList();
                    else if (isForecast)
                        productIds = forecasts.Select(od => od.ProductId)
                                              .Union(forecastNextMonths.Select(od => od.ProductId))
                                              .Distinct()
                                              .ToList();
                    var productInvs = from pi in vfi.ProductInventories
                                      where productIds.Contains(pi.ProductId)
                                      && (
                                      pi.WarehouseId == MyUtilities.Warehouse.HeatTreatment ||
                                      pi.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment ||
                                      pi.WarehouseId == MyUtilities.Warehouse.WaitingPlating ||
                                      pi.WarehouseId == MyUtilities.Warehouse.Plating ||
                                      pi.WarehouseId == MyUtilities.Warehouse.QcA ||
                                      pi.WarehouseId == MyUtilities.Warehouse.QcB ||
                                      pi.WarehouseId == MyUtilities.Warehouse.Processing ||
                                      pi.WarehouseId == MyUtilities.Warehouse.Processing2 ||
                                      pi.WarehouseId == MyUtilities.Warehouse.Finish
                                      )
                                      select new {
                                          pi.ProductId,
                                          TotalQty = pi.TotalQty
                                      };

                    foreach (var productId in productIds) {
                        var product = products.FirstOrDefault(p => p.ProductId == productId);
                        //if (product.ProductCode.Equals("S34975"))
                        //    product.ProductCode = product.ProductCode;
                        var entity = new ProductModel {
                            ProductId = product.ProductId,
                            CustomerCode = product.Customer.CustomerCode,
                            ProductCode = product.ProductCode,
                            SectionList = new List<ProductionSectionModel>(),
                            OrderList = new List<OrderDetailModel>(),
                            SectionCount = 0,
                            ToDate = toDate.ToString("MM/yyyy"),
                            NextMonth = nextMonth.ToString("MM/yyyy"),
                            Production2Weight = product.Production2Weight ?? 0,
                        };
                        var orderDetailsById = orderDetails.Where(od => od.ProductId == productId);
                        foreach (var orderDetailById in orderDetailsById) {
                            var odModel =
                                entity.OrderList.FirstOrDefault(
                                    od => od.VfiDueDateString.Equals(orderDetailById.DueDate.Value.ToString("dd/MM")));
                            if (odModel == null) {

                                odModel = new OrderDetailModel {
                                    RequiredNumber = orderDetailById.RequiedNumber,
                                    CreateDateString = orderDetailById.ModifiedDate.Value.ToString("dd/MM"),
                                    VfiDueDateString = orderDetailById.DueDate.Value.ToString("dd/MM")
                                };
                                entity.OrderList.Add(odModel);
                            }
                            else odModel.RequiredNumber += (orderDetailById.RequiedNumber);
                        }
                        entity.OrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                        var productionSectionById = productionSections.Where(ps => ps.ProductId == product.ProductId);

                        if (productionSectionById.Any()) {
                            foreach (var section in productionSectionById) {
                                var sectionModel = new ProductionSectionModel {
                                    SectionName = section.SectionName,
                                    //SectionCost = section.SectionCost,
                                    Productivity = section.Productivity,
                                };
                                sectionModel.SectionCost = sectionModel.Productivity * 5;
                                sectionModel.SectionCostKg = sectionModel.SectionCost * (1000 / entity.Production2Weight);

                                entity.SectionList.Add(sectionModel);
                                entity.SectionProductivity += section.Productivity;
                            }
                        }
                        var orderDetailsInMonthById = orderDetailsInMonth.Where(od => od.ProductId == productId);
                        if (orderDetailsInMonthById.Any()) {
                            entity.ForecastsQuality = orderDetailsInMonthById.Sum(od => od.RequiedNumber);
                            entity.IsOrderInMonth = 1;
                        }
                        else {
                            var forecast = forecasts.FirstOrDefault(f => f.ProductId == productId);
                            if (forecast != null) {
                                entity.ForecastsQuality = forecast.Quantity;
                            }
                        }
                        var forecastNextMonth = forecastNextMonths.FirstOrDefault(f => f.ProductId == productId);
                        if (forecastNextMonth != null) {
                            entity.ForecastNextMonth = forecastNextMonth.Quantity;
                        }
                        var productInvsById = productInvs.Where(pi => pi.ProductId == productId);
                        if (productInvsById.Any()) {
                            entity.TotalQuantity = productInvsById.Sum(pi => pi.TotalQty);
                        }
                        entity.OrderQuantityNeed = entity.OrderQuantity - entity.TotalQuantity;
                        if (entity.OrderQuantityNeed < 0) {
                            entity.OrderQuantityNeed = 0;
                            entity.ForecastQuantityNeed = (entity.ForecastsQuality ?? 0 + entity.OrderQuantity) -
                                                          entity.TotalQuantity;
                            if (entity.ForecastQuantityNeed < 0) {
                                entity.ForecastQuantityNeed = 0;
                                entity.ForecastNextMonthNeed = (entity.ForecastsQuality ?? 0 + entity.ForecastNextMonth
                                                                + entity.OrderQuantity) - entity.TotalQuantity;
                                if (entity.ForecastNextMonthNeed < 0) {
                                    entity.ForecastNextMonthNeed = 0;
                                }
                            }
                            else {
                                entity.ForecastNextMonthNeed = entity.ForecastNextMonth;
                            }
                        }
                        else if (entity.OrderQuantityNeed > 0) {
                            entity.ForecastQuantityNeed = (entity.ForecastsQuality ?? 0);
                            entity.ForecastNextMonthNeed = entity.ForecastNextMonth;
                        }
                        entity.OrderProcessTime = entity.OrderQuantityNeed * entity.SectionProductivity;
                        entity.ForecastProcessTime = entity.ForecastQuantityNeed * entity.SectionProductivity;
                        entity.NextMonthProcessTime = entity.ForecastNextMonthNeed * entity.SectionProductivity;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("Production2List", ex.Message);
            }
            return PartialView("PageProduction2List", model.OrderBy(p => p.CustomerCode).ThenBy(p => p.ProductCode).ToList());
        }

        [HttpPost]
        public ActionResult PrintProduction2Plan(string from, string to, bool isForecast) {
            var model = new List<Production2PlanModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fromDate = string.IsNullOrWhiteSpace(from)
                                 ? DateTime.Today
                                 : Convert.ToDateTime(from, ci);
                var toDate = string.IsNullOrWhiteSpace(to)
                                 ? DateTime.Today
                                 : Convert.ToDateTime(to, ci);
                var toDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                using (var vfi = new tammaContext()) {
                    var production2Ids = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    var products = from p in vfi.Products
                                   where p.Active
                                   //&& p.ProductCode.Equals("N3")
                                   select p;
                    var productIds = products.Select(p => p.ProductId).Distinct().ToList();
                    var production2Invs = (from pi in vfi.ProductInventories
                                           where productIds.Contains(pi.ProductId) &&
                                                 production2Ids.Contains(pi.WarehouseId)
                                           select pi).ToList();
                    var productionSections = from ps in vfi.ProductionSections
                                             where
                                                 productIds.Contains(ps.ProductId) &&
                                                 ps.Active
                                             select new {
                                                 ps.ProductionSectionId,
                                                 ps.ProductId,
                                                 SectionId = ps.SectionId,
                                                 ps.Section.SectionName,
                                                 ps.Productivity,
                                                 SectionIndex = ps.SectionIndex
                                             };
                    productIds = production2Invs.Select(pi => pi.ProductId)
                                            .Union(productionSections.Select(od => od.ProductId))
                                            .Distinct()
                                            .ToList();


                    var orderDetails = (from od in vfi.OrderDetails
                                        where od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                              od.Order.Status != (byte)MyUtilities.Sales.Status.Completed &&
                                            //od.Order.DueDate >= fromDate &&
                                              od.Order.DueDate <= toDate &&
                                              od.Order.DueDate != null &&
                                              od.RequiedNumber != 0 &&
                                              productIds.Contains(od.ProductId)
                                        orderby od.Order.DueDate, od.Order.ModifiedDate
                                        select new {
                                            od.ProductId,
                                            od.Order.DueDate,
                                            od.Order.ModifiedDate,
                                            od.OrderQty,
                                            od.RequiedNumber,
                                        }).ToList();
                    //var orderDetailsInMonth = (from od in vfi.OrderDetails
                    //                           where od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                    //                    od.Order.Status != (byte)MyUtilities.Sales.Status.Completed &&
                    //                                 od.Order.DueDate.Value.Month == toDate.Month &&
                    //                                 od.Order.DueDate.Value.Year == toDate.Year &&
                    //                                 od.Order.DueDate != null &&
                    //                          od.RequiedNumber != 0 &&
                    //                                 productIds.Contains(od.ProductId)
                    //                           select new
                    //                           {
                    //                               od.ProductId,
                    //                               od.Order.DueDate,
                    //                               od.OrderQty,
                    //                               od.RequiedNumber,
                    //                           }).ToList();
                    var forecasts = from f in vfi.ForecastOrders
                                    where productIds.Contains(f.ProductId)
                                          && f.ForecastDate.Month == toDate.Month
                                          && f.ForecastDate.Year == toDate.Year
                                          && f.IsSelling
                                    select f;
                    var nextMonth = toDate.AddMonths(1);
                    var forecastNextMonths = from f in vfi.ForecastOrders
                                             where productIds.Contains(f.ProductId)
                                                   && f.ForecastDate.Month == nextMonth.Month
                                                   && f.ForecastDate.Year == nextMonth.Year
                                                   && f.IsSelling
                                             select f;
                    //productIds =
                    //    forecasts.Select(od => od.ProductId)
                    //             .Union(forecastNextMonths.Select(od => od.ProductId))
                    //             .Union(orderDetails.Select(od => od.ProductId))
                    //    ////.Union(orderDetailsInMonth.Select(od => od.ProductId))
                    //             .Distinct()
                    //             .ToList();
                    var productInvs = from pi in vfi.ProductInventories
                                      where productIds.Contains(pi.ProductId)
                                            && (
                                                   pi.WarehouseId == MyUtilities.Warehouse.HeatTreatment ||
                                                   pi.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment ||
                                                   pi.WarehouseId == MyUtilities.Warehouse.WaitingPlating ||
                                                   pi.WarehouseId == MyUtilities.Warehouse.Plating ||
                                                   pi.WarehouseId == MyUtilities.Warehouse.QcA ||
                                                   pi.WarehouseId == MyUtilities.Warehouse.QcB ||
                                          //pi.WarehouseId == MyUtilities.Warehouse.Processing ||
                                          //pi.WarehouseId == MyUtilities.Warehouse.Processing2 ||
                                                   pi.WarehouseId == MyUtilities.Warehouse.Finish ||
                                                   production2Ids.Contains(pi.WarehouseId)
                                               )
                                      select new {
                                          pi.ProductId,
                                          pi.WarehouseId,
                                          TotalQty = pi.TotalQty
                                      };
                    var production2Exports = from pip in vfi.ProductInventoryPeriods
                                             where
                                                 production2Ids.Contains(pip.WarehouseId) &&
                                                 productIds.Contains(pip.ProductId) &&
                                                 production2Ids.Contains(pip.Transaction.WarehouseIssueId.Value) &&
                                                (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.SurfaceTreatment ||
                                                 pip.Transaction.WarehouseReceiptId != MyUtilities.Warehouse.WaitingPlating ||
                                                 pip.Transaction.WarehouseReceiptId != MyUtilities.Warehouse.QcA ||
                                                 pip.Transaction.WarehouseReceiptId != MyUtilities.Warehouse.Processing) &&
                                                 pip.PeriodDate >= fromDate && pip.PeriodDate <= toDate
                                             select pip;
                    var productionProcesses = from ps in vfi.ProductionProcesses
                                              where productIds.Contains(ps.ProductId)
                                                    && ps.IsNecessary
                                                    && ps.WarehouseId != MyUtilities.Warehouse.Production1
                                              select ps;
                    //var a = 0;
                    foreach (var productId in productIds) {
                        var product = products.FirstOrDefault(p => p.ProductId == productId);
                        //if (product.ProductCode.Equals("N3"))
                        //a = 1;
                        var entity = new Production2PlanModel {
                            ProductId = product.ProductId,
                            CustomerCode = product.Customer.CustomerCode,
                            ProductCode = product.ProductCode,
                            Production2Weight = product.Production2Weight ?? 0,
                            Details = new List<Production2PlanDetailModel>(),
                            StartDate = DateTime.Now,
                            ExportInMonth = 0
                        };
                        var productionSectionById =
                            productionSections.Where(ps => ps.ProductId == product.ProductId)
                                              .OrderBy(ps => ps.SectionIndex);
                        var orderDetailsById = orderDetails.Where(od => od.ProductId == product.ProductId);
                        var productInvsById = productInvs.Where(pi => pi.ProductId == product.ProductId);
                        var production2InvById =
                            productInvsById.Where(pi => production2Ids.Contains(pi.WarehouseId));
                        var productionProcessesById = productionProcesses.Where(ps => ps.ProductId == product.ProductId);
                        var prepareDay = productionProcessesById.Count();
                        if (productionProcessesById.FirstOrDefault(
                            pp => pp.WarehouseId == MyUtilities.Warehouse.QcA || pp.WarehouseId == MyUtilities.Warehouse.QcB) !=
                            null)
                            prepareDay++;
                        if (productionProcessesById.FirstOrDefault(
                            pp => pp.WarehouseId == MyUtilities.Warehouse.WaitingPlating) != null)
                            prepareDay += 6;
                        var exports = production2Exports.Where(pi => pi.ProductId == entity.ProductId);
                        if (exports.Any())
                            entity.ExportInMonth =
                                exports.Sum(pip => pip.EarlyPeriodQuantity - pip.LastPeriodQuantity);
                        var production2Inv = 0.0;
                        var afterProduction2Inv = 0.0;
                        if (production2InvById.Any()) {
                            production2Inv = production2InvById.Sum(pi => pi.TotalQty);
                        }
                        if (productInvsById.Any()) {
                            afterProduction2Inv = productInvsById.Sum(pi => pi.TotalQty) - production2Inv;
                        }
                        var forecast = forecasts.FirstOrDefault(f => f.ProductId == productId);
                        #region make section 2
                        if (orderDetailsById.Any()) {
                            foreach (var orderDetailById in orderDetailsById) {
                                if (productionSectionById.Any()) {
                                    foreach (var section in productionSectionById) {
                                        var detail = new Production2PlanDetailModel {
                                            SectionId = section.ProductionSectionId,
                                            SectionName = section.SectionName,
                                            SectionProductivity = section.Productivity,
                                            OrderQuantity = orderDetailById.RequiedNumber,
                                            CreateDate = orderDetailById.ModifiedDate.Value.ToString("dd/MM"),
                                            DueDate = orderDetailById.DueDate.Value.ToString("dd/MM"),
                                            ForecastNextMonth = 0,
                                            RequirementQuantity = 0,
                                            SectionProductivityInDay = 0,
                                            PrepareDay = prepareDay,
                                            FinishDateString =
                                                orderDetailById.DueDate.Value.AddDays(prepareDay * -1)
                                                               .ToString("dd/MM"),
                                            Production2Inv = production2Inv,
                                            AfterProduction2Inv = afterProduction2Inv,
                                            ExportInMonth = entity.ExportInMonth
                                        };

                                        if (detail.SectionProductivity != 0) {
                                            detail.SectionProductivityInDay = Math.Round(
                                                8 * 60 * 60 / detail.SectionProductivity, 0);
                                            detail.SectionProductivityInDayKg =
                                                Math.Round(detail.SectionProductivityInDay *
                                                           entity.Production2Weight);
                                            var temp = detail.SectionProductivityInDayKg % 100;
                                            if (temp > 0)
                                                detail.SectionProductivityInDayKg =
                                                    detail.SectionProductivityInDayKg - temp +
                                                    100;
                                            detail.RequirementDay = detail.RequirementQuantity /
                                                                    detail.SectionProductivityInDay;
                                        }
                                        detail.RequirementQuantity = detail.OrderQuantity -
                                                                     detail.AfterProduction2Inv;
                                        if (detail.RequirementQuantity < 0)
                                            detail.RequirementQuantity = 0;
                                        detail.RequirementQuantityKg = detail.RequirementQuantity *
                                                                       entity.Production2Weight;
                                        var temp2 = detail.RequirementDay % 1;
                                        if (temp2 > 0)
                                            detail.RequirementDay = detail.RequirementDay - temp2 + 1;
                                        if (
                                            orderDetailById.DueDate.Value.AddDays((prepareDay +
                                                                                   detail.RequirementDay) * -1) >
                                            toDay) {

                                        }
                                        else
                                            detail.DueDateStatus =
                                                orderDetailById.DueDate.Value.AddDays(((prepareDay +
                                                                                        detail.RequirementDay) * -1) +
                                                                                      7) < toDay
                                                    ? 1
                                                    : 2;
                                        entity.Details.Add(detail);
                                    }
                                    afterProduction2Inv -= (orderDetailById.RequiedNumber);
                                    if (afterProduction2Inv < 0) {
                                        production2Inv += afterProduction2Inv;
                                        if (production2Inv < 0)
                                            production2Inv = 0;
                                        afterProduction2Inv = 0;
                                    }
                                    //sectionModel.SectionCostKg = sectionModel.SectionCost * (1000 / entity.Production2Weight);
                                    if (entity.Details.LastOrDefault() != null)
                                        entity.Details.LastOrDefault().BorderLast = 1;
                                }
                                else {
                                    //var odModel =
                                    //    entity.Details.FirstOrDefault(
                                    //        od => od.DueDate.Equals(orderDetailById.DueDate.Value.ToString("dd/MM")));
                                    //if (odModel == null)
                                    //{

                                    var detail = new Production2PlanDetailModel {
                                        SectionName = "",
                                        SectionProductivity = 0,
                                        OrderQuantity = orderDetailById.RequiedNumber,
                                        CreateDate = orderDetailById.ModifiedDate.Value.ToString("dd/MM"),
                                        DueDate = orderDetailById.DueDate.Value.ToString("dd/MM"),
                                        AfterProduction2Inv = 0,
                                        ForecastNextMonth = 0,
                                        Production2Inv = 0,
                                        RequirementQuantity = 0,
                                        SectionProductivityInDay = 0,
                                        PrepareDay = prepareDay,
                                        ExportInMonth = entity.ExportInMonth
                                    };
                                    if (production2InvById != null)
                                        detail.Production2Inv = production2InvById.Sum(pi => pi.TotalQty);
                                    if (productInvsById.Any()) {
                                        detail.AfterProduction2Inv = productInvsById.Sum(pi => pi.TotalQty) -
                                                                     detail.Production2Inv;
                                    }
                                    detail.RequirementQuantity = detail.OrderQuantity - detail.AfterProduction2Inv;
                                    if (detail.RequirementQuantity < 0)
                                        detail.RequirementQuantity = 0;
                                    detail.RequirementQuantityKg = detail.RequirementQuantity *
                                                                   entity.Production2Weight;
                                    entity.Details.Add(detail);

                                }
                            }
                            if (forecast != null && isForecast) {
                                if (productionSectionById.Any()) {
                                    foreach (var section in productionSectionById) {
                                        var detail = new Production2PlanDetailModel {
                                            SectionId = section.ProductionSectionId,
                                            SectionName = section.SectionName,
                                            SectionProductivity = section.Productivity,
                                            OrderQuantity = forecast.Quantity,
                                            CreateDate = "DB",
                                            DueDate = "T" + toDate.ToString("MM"),
                                            ForecastNextMonth = 0,
                                            RequirementQuantity = 0,
                                            SectionProductivityInDay = 0,
                                            PrepareDay = prepareDay,
                                            Production2Inv = production2Inv,
                                            AfterProduction2Inv = afterProduction2Inv,
                                            ExportInMonth = entity.ExportInMonth
                                        };
                                        if (detail.SectionProductivity != 0) {
                                            detail.SectionProductivityInDay = Math.Round(
                                                8 * 60 * 60 / detail.SectionProductivity, 0);
                                            detail.SectionProductivityInDayKg =
                                                Math.Round(detail.SectionProductivityInDay *
                                                           entity.Production2Weight);
                                            var temp = detail.SectionProductivityInDayKg % 100;
                                            if (temp > 0)
                                                detail.SectionProductivityInDayKg =
                                                    detail.SectionProductivityInDayKg - temp +
                                                    100;
                                            detail.RequirementDay = detail.RequirementQuantity /
                                                                    detail.SectionProductivityInDay;
                                        }
                                        detail.RequirementQuantity = detail.OrderQuantity -
                                                                     detail.AfterProduction2Inv;
                                        if (detail.RequirementQuantity < 0)
                                            detail.RequirementQuantity = 0;
                                        detail.RequirementQuantityKg = detail.RequirementQuantity *
                                                                       entity.Production2Weight;
                                        var temp2 = detail.RequirementDay % 1;
                                        if (temp2 > 0)
                                            detail.RequirementDay = detail.RequirementDay - temp2 + 1;

                                        entity.Details.Add(detail);
                                    }

                                    if (entity.Details.LastOrDefault() != null)
                                        entity.Details.LastOrDefault().BorderLast = 1;
                                }
                            }
                        }
                        else if (productionSectionById.Any()) {
                            foreach (var section in productionSectionById) {

                                var detail = new Production2PlanDetailModel {
                                    SectionId = section.ProductionSectionId,
                                    SectionName = section.SectionName,
                                    SectionProductivity = section.Productivity,
                                    OrderQuantity = 0,
                                    CreateDate = "",
                                    DueDate = "",
                                    AfterProduction2Inv = 0,
                                    ForecastNextMonth = 0,
                                    Production2Inv = 0,
                                    RequirementQuantity = 0,
                                    SectionProductivityInDay = 0,
                                    RequirementDay = 0,
                                    RequirementQuantityKg = 0,
                                    SectionProductivityInDayKg = 0,
                                    PrepareDay = prepareDay,
                                    ExportInMonth = entity.ExportInMonth
                                };
                                if (forecast != null && isForecast) {
                                    detail.OrderQuantity = forecast.Quantity;
                                    detail.CreateDate = "DB";
                                    detail.DueDate = "T" + toDate.ToString("MM");
                                }
                                if (production2InvById != null)
                                    detail.Production2Inv = production2InvById.Sum(pi => pi.TotalQty);
                                if (productInvsById.Any()) {
                                    detail.AfterProduction2Inv = productInvsById.Sum(pi => pi.TotalQty) -
                                                                 detail.Production2Inv;
                                }
                                if (detail.SectionProductivity != 0) {
                                    detail.SectionProductivityInDay = Math.Round(
                                        8 * 60 * 60 / detail.SectionProductivity, 0);
                                    detail.SectionProductivityInDayKg =
                                        Math.Round(detail.SectionProductivityInDay *
                                                   entity.Production2Weight);
                                    var temp = detail.SectionProductivityInDayKg % 100;
                                    if (temp > 0)
                                        detail.SectionProductivityInDayKg =
                                            detail.SectionProductivityInDayKg - temp +
                                            100;
                                    detail.RequirementDay = detail.RequirementQuantity /
                                                            detail.SectionProductivityInDay;
                                }
                                detail.RequirementQuantity = detail.OrderQuantity -
                                                             detail.AfterProduction2Inv;
                                if (detail.RequirementQuantity < 0)
                                    detail.RequirementQuantity = 0;
                                detail.RequirementQuantityKg = detail.RequirementQuantity *
                                                               entity.Production2Weight;
                                var temp2 = detail.RequirementDay % 1;
                                if (temp2 > 0)
                                    detail.RequirementDay = detail.RequirementDay - temp2 + 1;
                                entity.Details.Add(detail);
                            }
                            if (entity.Details.LastOrDefault() != null)
                                entity.Details.LastOrDefault().BorderLast = 1;
                        }
                        else if (forecast != null && isForecast) {
                            if (productionSectionById.Any()) {
                                foreach (var section in productionSectionById) {
                                    var detail = new Production2PlanDetailModel {
                                        SectionId = section.ProductionSectionId,
                                        SectionName = section.SectionName,
                                        SectionProductivity = section.Productivity,
                                        OrderQuantity = forecast.Quantity,
                                        CreateDate = "DB",
                                        DueDate = "T" + toDate.ToString("MM"),
                                        ForecastNextMonth = 0,
                                        RequirementQuantity = 0,
                                        SectionProductivityInDay = 0,
                                        PrepareDay = prepareDay,
                                        Production2Inv = production2Inv,
                                        AfterProduction2Inv = afterProduction2Inv,
                                        ExportInMonth = entity.ExportInMonth
                                    };

                                    if (detail.SectionProductivity != 0) {
                                        detail.SectionProductivityInDay = Math.Round(
                                            8 * 60 * 60 / detail.SectionProductivity, 0);
                                        detail.SectionProductivityInDayKg =
                                            Math.Round(detail.SectionProductivityInDay *
                                                       entity.Production2Weight);
                                        var temp = detail.SectionProductivityInDayKg % 100;
                                        if (temp > 0)
                                            detail.SectionProductivityInDayKg =
                                                detail.SectionProductivityInDayKg - temp +
                                                100;
                                        detail.RequirementDay = detail.RequirementQuantity /
                                                                detail.SectionProductivityInDay;
                                    }
                                    detail.RequirementQuantity = detail.OrderQuantity -
                                                                 detail.AfterProduction2Inv;
                                    if (detail.RequirementQuantity < 0)
                                        detail.RequirementQuantity = 0;
                                    detail.RequirementQuantityKg = detail.RequirementQuantity *
                                                                   entity.Production2Weight;
                                    var temp2 = detail.RequirementDay % 1;
                                    if (temp2 > 0)
                                        detail.RequirementDay = detail.RequirementDay - temp2 + 1;

                                    entity.Details.Add(detail);
                                }

                                if (entity.Details.LastOrDefault() != null)
                                    entity.Details.LastOrDefault().BorderLast = 1;
                            }
                        }
                        #endregion
                        // no more
                        #region make late production 2

                        #endregion

                        #region make next production 2

                        #endregion

                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("Production2Plan", ex.Message);
            }

            return PartialView("PageProduction2Plan", model.OrderBy(p => p.CustomerCode).ThenBy(p => p.ProductCode).ToList());
        }


        [HttpPost]
        public ActionResult PrintProduction2PriceList(string from, string to, bool isSales, bool isForecast) {
            var model = new List<ProductModel>();
            //var customerId = -1;
            //var productCode = "";
            //if (customerId == 0 && string.IsNullOrWhiteSpace(productCode) && status == 0)
            //    return PartialView("PageProductPriceList", model);

            var ci = new CultureInfo("vi-VN");
            var fromDate = string.IsNullOrWhiteSpace(from)
                              ? DateTime.Today
                              : Convert.ToDateTime(from, ci);
            var toDate = string.IsNullOrWhiteSpace(to)
                              ? DateTime.Today
                              : Convert.ToDateTime(to, ci);
            try {
                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var production2Ids = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    var productIds = (from pi in vfi.ProductInventories
                                      where production2Ids.Contains(pi.WarehouseId)
                                      select pi.ProductId).ToList();

                    var productionSections = from ps in vfi.ProductionSections
                                             where
                                                 //productIds.Contains(ps.ProductId.Value)&&
                                             ps.Active
                                             select new {
                                                 ps.ProductId,
                                                 ps.Section.SectionName,
                                                 SectionCost = ps.Productivity * ps.Section.SaleFactor,
                                                 ps.Productivity,
                                             };
                    productIds = productIds.Union(productionSections.Select(od => od.ProductId))
                                           .Distinct()
                                           .ToList();
                    var products = from p in vfi.Products
                                   where p.Active
                                         && productIds.Contains(p.ProductId)
                                   //&& p.ProductCode.Contains("HA6")
                                   select new {
                                       p.ProductId,
                                       p.ProductCode,
                                       p.ProductName,
                                       p.CustomerId,
                                       p.Customer.CustomerCode,
                                       p.Customer.CustomerName,
                                       p.MaterialId,
                                       p.Material,
                                       //p.Material.MaterialName,
                                       p.DesignNo,
                                       Diameter = p.Diameter ?? 0,
                                       Length = p.Length ?? 0,
                                       Weight = p.Weight ?? 0,
                                       p.ForecastsQuality,
                                       p.ModifiedUser,
                                       p.ModifiedDate,
                                       ProductionWeight = p.ProductionWeight ?? 0,
                                       CncWeight = p.CncWeight ?? 0,
                                       Production2Weight = p.Production2Weight ?? 0,
                                       HeatTreatmentWeight = p.HeatTreatmentWeight ?? 0,
                                       SurfaceTreatmentWeight = p.SurfaceTreatmentWeight ?? 0,
                                       WaitingPlatingWeight = p.WaitingPlatingWeight ?? 0,
                                       PlatingWeight = p.PlatingWeight ?? 0,
                                       QcWeight = p.QcWeight ?? 0,
                                       FinishWeight = p.FinishWeight ?? 0,
                                       ProductionRate = p.ProductionRate ?? 0,
                                       Productivity = p.Productivity ?? 0,
                                       UnitPrice = p.UnitPrice ?? 0,
                                       SaleFactor = p.SaleFactor ?? 3,
                                       ProductionFactor = p.ProductionFactor ?? 3,
                                       p.Active,
                                       IsSelling = p.IsSelling ?? false,
                                       p.MaterialNameDesign,
                                       OutDiameterDesign = p.OutDiameterDesign ?? 0,
                                       p.OutDiameterTolerance,
                                       InDiameterDesign = p.InDiameterDesign ?? 0,
                                       p.InDiameterTolerance,
                                       ShapeDesign = p.ShapeDesign ?? "",
                                       p.Drawing2D,
                                       Status = p.Status.Value,
                                       p.ProcessingType,
                                       p.DrawingFinish,
                                       p.DiameterTypeDesign,
                                       KnifeCut = p.KnifeCut ?? 0,
                                       p.ProductionSections,
                                       MillProductivity = p.MillProductivity ?? 0,
                                       MaterialCost = p.MaterialCost ?? 0,
                                       p.QcProductivity,
                                       p.ProductShape
                                   };
                    productIds = products.Select(p => p.ProductId).ToList();
                    var allProductInventoryPeriodsByDate = (from pip in vfi.ProductInventoryPeriods
                                                            where
                                                                pip.PeriodDate >= fromDate &&
                                                                pip.PeriodDate <= toDate &&
                                                                productIds.Contains(pip.ProductId)
                                                            select new {
                                                                pip.Product.CustomerId,
                                                                pip.ProductId,
                                                                pip.Quantity,
                                                                pip.LastPeriodQuantity,
                                                                pip.EarlyPeriodQuantity,
                                                                pip.PeriodDate,
                                                                pip.WarehouseId,
                                                                pip.Transaction,
                                                            }).ToList();
                    //var productIds = allProductInventoryPeriodsByDate.Select(p => p.ProductId).ToList();
                    var giaodichKd = (from p in allProductInventoryPeriodsByDate
                                      where
                                          p.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Finish &&
                                          p.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Business &&
                                          p.WarehouseId == MyUtilities.Warehouse.Business &&
                                          (p.PeriodDate.Month >= fromDate.Month &&
                                          p.PeriodDate.Month >= fromDate.Year)
                                      select p).ToList();
                    var orderDetails = (from od in vfi.OrderDetails
                                        where od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                              od.Order.DueDate >= fromDate &&
                                              od.Order.DueDate <= toDate &&
                                              od.Order.DueDate != null &&
                                              productIds.Contains(od.ProductId)
                                        select new {
                                            od.ProductId,
                                            od.Order.DueDate,
                                            od.OrderQty,
                                            od.RequiedNumber,
                                        }).ToList();
                    var forecasts = from f in vfi.ForecastOrders
                                    where productIds.Contains(f.ProductId)
                                    && f.ForecastDate.Month == toDate.Month
                                    && f.ForecastDate.Year == toDate.Year
                                    && f.IsSelling
                                    select f;
                    if (isSales && isForecast) {
                        productIds =
                            giaodichKd.Select(od => od.ProductId)
                                      .Union(forecasts.Select(od => od.ProductId))
                                      .Union(orderDetails.Select(od => od.ProductId))
                                      .Distinct()
                                      .ToList();
                    }
                    else if (isSales)
                        productIds = giaodichKd.Select(od => od.ProductId)
                                               .Union(orderDetails.Select(od => od.ProductId))
                                               .Distinct()
                                               .ToList();
                    else if (isForecast)
                        productIds = forecasts.Select(od => od.ProductId).Distinct().ToList();
                    var productPlatings = from pp in vfi.ProductionPlatings
                                          where productIds.Contains(pp.ProductId)
                                                && pp.Active
                                          select new {
                                              pp.ProductId,
                                              pp.PlatingName,
                                              pp.PlatingCost,
                                          };
                    var realProductions = from rp in vfi.RealProductions
                                          where productIds.Contains(rp.ProductId) &&
                                                rp.TrackUpMachine.Status == (byte)MyUtilities.Transaction.Status.Approved
                                                && rp.TrackUpMachine.RealProductivity != 0
                                                && rp.TrackUpMachine.RealRate != 0
                                          select new {
                                              rp.Machine.MachineName,
                                              rp.ProductId,
                                              rp.TrackUpMachine.RealProductivity,
                                              rp.TrackUpMachine.RealRate,
                                          };
                    var realMaterials = from rm in vfi.ProductionMaterials
                                        where productIds.Contains(rm.ProductId)
                                        && rm.Active
                                        select new {
                                            rm.ProductId,
                                            rm.Material,
                                        };
                    //if (active)
                    //    products = products.Where(p => p.Active);
                    //if (status != 0)
                    //    products = products.Where(p => p.Status == status);
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null)
                        throw new AggregateException("Vui lòng đăng nhập lại");
                    var processingType = vfi.ProcessingTypes.FirstOrDefault(m => m.TypeId == 6);
                    var exchangeRate = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.ExchangeToVndRate);
                    foreach (var entity in products) {
                        //if (entity.ProductCode.Equals("HA6"))
                        //    entity.ProductCode = entity.ProductCode;
                        var product = new ProductModel {
                            ProductId = entity.ProductId,
                            ProductCode = entity.ProductCode,
                            ProductName = entity.ProductName,
                            CustomerId = entity.CustomerId,
                            CustomerCode = entity.CustomerCode,
                            CustomerName = entity.CustomerName,
                            //MaterialId = entity.MaterialId,
                            //MaterialCode = entity.MaterialCode,
                            //MaterialName = entity.MaterialName,
                            DesignNo = entity.DesignNo,
                            Diameter = entity.Diameter,
                            Length = entity.Length,
                            //Weight = entity.Weight,
                            Active = entity.Active,
                            ForecastsQuality = entity.ForecastsQuality,
                            ModifiedUser = entity.ModifiedUser,
                            ModifiedDate = entity.ModifiedDate,
                            ProductionWeight = entity.ProductionWeight,
                            CncWeight = entity.CncWeight,
                            Production2Weight = entity.Production2Weight,
                            HeatTreatmentWeight = entity.HeatTreatmentWeight,
                            SurfaceTreatmentWeight = entity.SurfaceTreatmentWeight,
                            WaitingPlatingWeight = entity.WaitingPlatingWeight,
                            PlatingWeight = entity.PlatingWeight,
                            QcWeight = entity.QcWeight,
                            FinishWeight = entity.FinishWeight,
                            ProductionRate = entity.ProductionRate,
                            Productivity = entity.Productivity,
                            UnitPrice = entity.UnitPrice,
                            ProductionFactor = entity.ProductionFactor,
                            SalesFactor = entity.SaleFactor,
                            IsSelling = entity.IsSelling,
                            MaterialNameDesign = entity.MaterialNameDesign + "",
                            OutDiameterDesign = entity.OutDiameterDesign,
                            OutDiameterTolerance = entity.OutDiameterTolerance,
                            InDiameterDesign = entity.InDiameterDesign,
                            InDiameterTolerance = entity.InDiameterTolerance,
                            ShapeDesign = entity.ShapeDesign.Trim(),
                            Upload2D = entity.Drawing2D,
                            UploadReal = entity.DrawingFinish,
                            ProcessingTypeId = entity.ProcessingType.TypeId,
                            ProcessingTypeName = entity.ProcessingType.TypeName,
                            DiameterTypeDesign = entity.DiameterTypeDesign,
                            KnifeCut = entity.KnifeCut,
                            StatusFilter = entity.Status,
                            Status = entity.Status,
                            StatusName = MyUtilities.Product.GetText(entity.Status),
                            MillProductivity = entity.MillProductivity,
                            MillCost = entity.MillProductivity * processingType.ProcessingSaleFactor.Value,
                            ProcessingSalesCost =
                                entity.ProcessingType.ProcessingSaleFactor.Value * entity.Productivity,
                            ProcessingCost = entity.ProcessingType.ProcessingFactor.Value * entity.Productivity,
                            QcProductivity = entity.QcProductivity,
                            ProductShape = entity.ProductShape + entity.Diameter,
                            SectionList = new List<ProductionSectionModel>(),
                            SectionCost = 0,
                            Weight = 0,
                        };
                        if (entity.MaterialId != null) {
                            product.MaterialId = entity.MaterialId.Value;
                            product.MaterialName = entity.Material.MaterialName;
                            product.MaterialCode = entity.Material.MaterialCode;
                        }
                        var realMaterialByProductId =
                            realMaterials.FirstOrDefault(rm => rm.ProductId == product.ProductId);
                        if (realMaterialByProductId != null) {
                            product.MaterialNameDesign = realMaterialByProductId.Material.MaterialName;
                            product.OutDiameterDesign = realMaterialByProductId.Material.OutDiameter;
                            product.ShapeDesign = (realMaterialByProductId.Material.Shape + "").Trim();
                            product.InDiameterDesign = realMaterialByProductId.Material.InDiameter;
                            product.DiameterTypeDesign = (realMaterialByProductId.Material.DiameterType + "").Trim();
                            product.MaterialCost = realMaterialByProductId.Material.UnitPrice;
                            product.MaterialCodeDesign =
                                MyUtilities.Material.GetMaterialDesignNo(realMaterialByProductId.Material);
                            product.Weight = MyUtilities.Product
                                .GetProductWeight(realMaterialByProductId.Material.MaterialName,
                                    realMaterialByProductId.Material.OutDiameter,
                                    realMaterialByProductId.Material.InDiameter,
                                    product.Length ?? 0,
                                    product.KnifeCut,
                                    realMaterialByProductId.Material.Shape + "");
                        }
                        var productionSectionById = productionSections.Where(ps => ps.ProductId == product.ProductId);

                        if (productionSectionById.Any()) {
                            foreach (var section in productionSectionById) {
                                var sectionModel = new ProductionSectionModel {
                                    SectionName = section.SectionName,
                                    SectionCost = section.SectionCost,
                                    Productivity = section.Productivity,
                                };
                                product.SectionCost += section.SectionCost;
                                product.SectionList.Add(sectionModel);
                            }
                        }
                        var productPlatingById = productPlatings.Where(ps => ps.ProductId == product.ProductId);
                        if (productPlatingById.Any()) {
                            var sectionModel = new ProductionSectionModel {
                                SectionName = productPlatingById.FirstOrDefault().PlatingName,
                                SectionCost = productPlatingById.FirstOrDefault().PlatingCost * 1.3,
                                Productivity = 0,
                            };
                            product.PlatingCost = sectionModel.SectionCost;
                            product.SectionList.Add(sectionModel);
                        }
                        if (!product.SectionList.Any()) {
                            var sectionModel = new ProductionSectionModel {
                                SectionName = "Without",
                                SectionCost = 0,
                                Productivity = 0
                            };
                            product.SectionList.Add(sectionModel);
                        }
                        product.MaterialUnitPrice = ((product.Weight ?? 0) / 1000) * product.MaterialCost;
                        product.ProductBaseCost = product.MaterialUnitPrice +
                                                  product.MillCost + product.ProcessingSalesCost +
                                                  product.SectionList.Sum(sl => sl.SectionCost);
                        //if (product.UnitPrice != null || product.UnitPrice != 0) {
                        //    var productPrice = Math.Round(product.UnitPrice ?? 0, 4);
                        //    var temp = Convert.ToInt32(productPrice);
                        //    if (productPrice - temp != 0)
                        //        productPrice = Math.Round(productPrice * MyUtilities.Product.ExchangeRateDesign, 0);
                        //    product.UnitPrice = productPrice; //* MyUtilities.Product.DesignPrice;
                        //}
                        product.UnitPrice = MyUtilities.Product.ProductVndPrice(entity.UnitPrice, 1, exchangeRate);
                        var realProductionsByProductId = realProductions.Where(rp => rp.ProductId == product.ProductId);
                        if (realProductionsByProductId.Any()) {
                            var realProductivity =
                                realProductionsByProductId.Where(rp => !rp.MachineName.Contains("P"))
                                                          .Select(rp => rp.RealProductivity)
                                                          .Max();
                            product.RealProductivity = realProductivity;
                            var realMillProductivity =
                                realProductionsByProductId.Where(rp => rp.MachineName.Contains("P"))
                                                          .Select(rp => rp.RealProductivity / rp.RealRate)
                                                          .Max();
                            product.RealMillProductivity = realMillProductivity;
                        }
                        model.Add(product);
                    }
                }
                return PartialView("PageProductPriceList", model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ToList());
            }
            catch (Exception ex) {
                ModelState.AddModelError("PageProductPriceList", "" + ex.Message);
            }
            return PartialView("PageProductPriceList", model);

        }
        #endregion

        #region Kho Nhiet Luyen
        [GridAction]
        public ActionResult SelectHeatTreatmentInvManagement(
            int customerId,
            //, int warehouseIssue, int warehouseReceipt
            string productCode,
            string fromDate, string toDate) {
            var model = new List<HeatTreatmentInvModel>();
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseLastDateTime(toDate);
            //reportDate = reportDate.AddDays(1).AddSeconds(-1); 
            try {
                //var warehouseId = MyUtilities.Warehouse.HeatTreatment;
                using (var vfi = new tammaContext()) {
                    //lay DS san pham da tung luu tru trong kho
                    var products = (from pi in vfi.ProductInventories
                                    where pi.Warehouse.IsHeatTreatment
                                    orderby pi.Product.Customer.CustomerCode, pi.Product.ProductCode
                                    select new {
                                        pi.ProductId,
                                        pi.Product.ProductCode,
                                        pi.Product.HeatTreatmentWeight,
                                        pi.Product.CustomerId,
                                        pi.Product.Customer.CustomerCode,
                                    }).ToList();
                    if (customerId != 0)
                        products = products.Where(p => p.CustomerId == customerId).ToList();
                    if (!string.IsNullOrWhiteSpace(productCode))
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    var productIds = products.Select(p => p.ProductId).Distinct();
                    // lay thong tin luan chuyen toi ngay bao cao
                    var periods = (from pip in vfi.ProductInventoryPeriods
                                   where productIds.Contains(pip.ProductId)
                                         && pip.PeriodDate <= tDate &&
                                pip.Warehouse.IsHeatTreatment
                                   select new {
                                       pip.ProductId,
                                       PeriodQuantity = pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                       IoE = pip.LastPeriodQuantity > pip.EarlyPeriodQuantity,
                                       pip.PeriodDate,
                                       WarehouseIssue = pip.Transaction.Warehouse,
                                       WarehouseReceipt = pip.Transaction.Warehouse1,
                                       pip.Transaction.WarehouseIssueId,
                                       pip.Transaction.WarehouseReceiptId,
                                       pip.Quantity
                                   }).ToList();
                    var imports = periods.Where(pip => pip.IoE && pip.PeriodDate >= fDate)
                                         .ToList();
                    var exports = periods.Where(pip => !pip.IoE && pip.PeriodDate >= fDate)
                                         .ToList();

                    //var production2Ids = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    //var platingInvIds = MyUtilities.Warehouse.GetWarehouseIds_Plating();
                    var i = 1;
                    foreach (var productId in productIds) {
                        var product = products.FirstOrDefault(p => p.ProductId == productId);
                        if (product == null) continue;
                        var entity = new HeatTreatmentInvModel {
                            GlobalIndex = ++i,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.HeatTreatmentWeight ?? 0
                        };
                        entity.EarlyInventory = periods.Where(pip => pip.PeriodDate < fDate && pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        entity.LastInventory = periods.Where(pip => pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        //
                        var importsById = imports.Where(x => x.ProductId == entity.ProductId).ToList();
                        entity.TotalImport = importsById.Sum(x => x.Quantity);
                        entity.ImportSx1 = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsProduction)
                             .Sum(pip => pip.Quantity);
                        entity.ImportSx2 = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsProduction2)
                             .Sum(pip => pip.Quantity);
                        //
                        var exportsById = exports.Where(x => x.ProductId == entity.ProductId).ToList();
                        entity.TotalExport = exportsById.Sum(pip => pip.Quantity);
                        entity.ExportQc = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsQC)
                            .Sum(pip => pip.Quantity);
                        entity.ExportGcn = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsPlating)
                            .Sum(pip => pip.Quantity);
                        entity.ExportRb = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsPolish)
                             .Sum(pip => pip.Quantity);
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectHeatTreatmentInvManagement", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }
        #endregion

        #region Kho Rung bong
        [GridAction]
        public ActionResult SelectSurfaceTreatmentInvManagement(
            int customerId,
            //, int warehouseIssue, int warehouseReceipt
            string productCode,
            string fromDate, string toDate) {
            var model = new List<SurfaceTreatmentInvModel>();
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseLastDateTime(toDate);
            try {
                using (var vfi = new tammaContext()) {
                    //lay DS san pham da tung luu tru trong kho
                    var products = (from pi in vfi.ProductInventories
                                    where pi.Warehouse.IsPolish
                                    orderby pi.Product.Customer.CustomerCode, pi.Product.ProductCode
                                    select new {
                                        pi.ProductId,
                                        pi.Product.ProductCode,
                                        pi.Product.SurfaceTreatmentWeight,
                                        pi.Product.CustomerId,
                                        pi.Product.Customer.CustomerCode,
                                    }).ToList();
                    if (customerId != 0)
                        products = products.Where(p => p.CustomerId == customerId).ToList();
                    if (!string.IsNullOrWhiteSpace(productCode))
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    var productIds = products.Select(p => p.ProductId).Distinct();
                    // lay thong tin luan chuyen toi ngay bao cao
                    var periods = (from pip in vfi.ProductInventoryPeriods
                                   where productIds.Contains(pip.ProductId)
                                         && pip.PeriodDate <= tDate &&
                                pip.Warehouse.IsPolish
                                   select new {
                                       pip.ProductId,
                                       PeriodQuantity = pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                       IoE = pip.LastPeriodQuantity > pip.EarlyPeriodQuantity,
                                       pip.PeriodDate,
                                       WarehouseIssue = pip.Transaction.Warehouse,
                                       WarehouseReceipt = pip.Transaction.Warehouse1,
                                       pip.Transaction.WarehouseIssueId,
                                       pip.Transaction.WarehouseReceiptId,
                                       pip.Quantity
                                   }).ToList();
                    var imports = periods.Where(pip => pip.IoE && pip.PeriodDate >= fDate)
                                         .ToList();
                    var exports = periods.Where(pip => !pip.IoE && pip.PeriodDate >= fDate)
                                         .ToList();
                    var i = 0;
                    foreach (var productId in productIds) {
                        var product = products.FirstOrDefault(p => p.ProductId == productId);
                        if (product == null) continue;
                        var entity = new SurfaceTreatmentInvModel {
                            GlobalIndex = ++i,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.SurfaceTreatmentWeight ?? 0
                        };
                        entity.EarlyInventory = periods.Where(pip => pip.PeriodDate < fDate && pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        entity.LastInventory = periods.Where(pip => pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        //
                        var importsById = imports.Where(x => x.ProductId == entity.ProductId).ToList();
                        entity.TotalImport = importsById.Sum(x => x.Quantity);
                        entity.ImportSx1 = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsProduction)
                             .Sum(pip => pip.Quantity);
                        entity.ImportSx2 = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsProduction2)
                             .Sum(pip => pip.Quantity);
                        entity.ImportNl = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsHeatTreatment)
                             .Sum(pip => pip.Quantity);
                        entity.ImportCxl1 = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsReprocessing)
                            .Sum(pip => pip.Quantity);
                        //
                        var exportsById = exports.Where(x => x.ProductId == entity.ProductId).ToList();
                        entity.TotalExport = exportsById.Sum(pip => pip.Quantity);
                        entity.ExportQcA = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsQC)
                            .Sum(pip => pip.Quantity);
                        entity.ExportGcn = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsPlating)
                            .Sum(pip => pip.Quantity);

                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectSurfaceTreatmentInvManagement", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }
        #endregion

        #region Kho Dong goi
        [GridAction]
        public ActionResult SelectPackingInvManagement(
            int customerId,
            //, int warehouseIssue, int warehouseReceipt
            string productCode,
            string fromDate,
            string toDate) {
            var model = new List<PackingInvModel>();
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseLastDateTime(toDate);
            var warehouseId = MyUtilities.Warehouse.Packing;
            try {

                using (var vfi = new tammaContext()) {
                    var products = (from pi in vfi.ProductInventories
                                    where pi.Warehouse.IsPacking &&
                                    (customerId == 0 || pi.Product.CustomerId == customerId)
                                    orderby pi.Product.Customer.CustomerCode, pi.Product.ProductCode
                                    select new {
                                        pi.ProductInventoryId,
                                        pi.ProductId,
                                        pi.Product.ProductCode,
                                        pi.Product.CustomerId,
                                        pi.Product.Customer.CustomerCode,
                                        pi.Product.QcWeight,
                                        pi.LotNumber,
                                    }).ToList();
                    if (!string.IsNullOrWhiteSpace(productCode))
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    var productIds = products.Select(p => p.ProductId).Distinct().ToList();
                    // lay thong tin luan chuyen toi ngay bao cao
                    var periods = (from pip in vfi.ProductInventoryPeriods
                                   where productIds.Contains(pip.ProductId) &&
                                   pip.PeriodDate <= tDate &&
                                pip.Warehouse.IsPacking
                                   select new {
                                       pip.ProductId,
                                       PeriodQuantity = pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                       IoE = pip.LastPeriodQuantity > pip.EarlyPeriodQuantity,
                                       pip.PeriodDate,
                                       WarehouseIssue = pip.Transaction.Warehouse,
                                       WarehouseReceipt = pip.Transaction.Warehouse1,
                                       pip.Transaction.WarehouseIssueId,
                                       pip.Transaction.WarehouseReceiptId,
                                       pip.Quantity
                                   }).ToList();
                    var imports = periods.Where(pip => pip.IoE && pip.PeriodDate >= fDate)
                                         .ToList();
                    var exports = periods.Where(pip => !pip.IoE && pip.PeriodDate >= fDate)
                                         .ToList();
                    var i = 1;
                    //var processingInvIds = MyUtilities.Warehouse.GetWarehouseId_ReProcessing();
                    foreach (var productId in productIds) {
                        var product = products.FirstOrDefault(p => p.ProductId == productId);
                        if (product == null) continue;
                        var entity = new PackingInvModel {
                            GlobalIndex = ++i,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.QcWeight ?? 0
                        };
                        entity.EarlyInventory = periods.Where(pip => pip.ProductId == entity.ProductId && pip.PeriodDate < fDate)
                            .Sum(pip => pip.PeriodQuantity);
                        entity.LastInventory = periods.Where(pip => pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        //
                        var importsById = imports.Where(x => x.ProductId == entity.ProductId).ToList();
                        entity.TotalImport = importsById.Sum(x => x.Quantity);
                        entity.ImportQc = importsById.Where(pip => pip.WarehouseIssueId != null && pip.WarehouseIssue.IsQC)
                             .Sum(pip => pip.Quantity);
                        //
                        var exportsById = exports.Where(x => x.ProductId == entity.ProductId).ToList();
                        entity.TotalExport = exportsById.Sum(pip => pip.Quantity);
                        entity.ExportQc = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsQC)
                             .Sum(pip => pip.Quantity);
                        entity.ExportFinish = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsFinish)
                             .Sum(pip => pip.Quantity);
                        entity.ExportCxl1 = exportsById.Where(pip => pip.WarehouseReceiptId != null && pip.WarehouseReceipt.IsReprocessing)
                            .Sum(pip => pip.Quantity);
                        //
                        if (entity.Show) {
                            model.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectPackingInvManagement", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }
        #endregion

        #region Kho Cho GCN
        [GridAction]
        public ActionResult SelectWaitingPlatingInvManagement(
            int customerId,
            string productCode,
            string monthlyDate) {
            var model = new List<WaitingPlatingInvModel>();
            var ci = new CultureInfo("vi-VN");
            var reportDate = MyUtilities.Function.ParseLastDateTime(monthlyDate);
            //reportDate = reportDate.AddDays(1).AddSeconds(-1);
            try {

                using (var vfi = new tammaContext()) {
                    //lay DS san pham da tung luu tru trong kho
                    var productInvs = (from pi in vfi.ProductInventories
                                       where pi.WarehouseId == MyUtilities.Warehouse.WaitingPlating
                                       orderby pi.Product.Customer.CustomerCode, pi.Product.ProductCode
                                       select new {
                                           pi.ProductId,
                                           pi.Product.ProductCode,
                                           pi.Product.WaitingPlatingWeight,
                                           pi.Product.CustomerId,
                                           pi.Product.Customer.CustomerCode,
                                       }).ToList();

                    if (customerId != 0)
                        productInvs = productInvs.Where(p => p.CustomerId == customerId).ToList();
                    if (!string.IsNullOrWhiteSpace(productCode))
                        productInvs = productInvs.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    var productIds = productInvs.Select(p => p.ProductId).Distinct();
                    // lay thong tin luan chuyen toi ngay bao cao
                    var productInvPeriods = (from pip in vfi.ProductInventoryPeriods
                                             where productIds.Contains(pip.ProductId)
                                                   && pip.PeriodDate <= reportDate
                                                   && pip.WarehouseId == MyUtilities.Warehouse.WaitingPlating
                                             select new {
                                                 pip.ProductId,
                                                 PeriodQuantity =
                                             pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                                 pip.PeriodDate,
                                                 pip.Transaction,
                                                 pip.WarehouseId,
                                                 pip.Quantity
                                             }).ToList();
                    // lay thong tin luan chuyen trong ngay
                    var periodInDay = productInvPeriods.Where(pip => pip.PeriodDate == reportDate).ToList();
                    var startMonth = new DateTime(reportDate.Year, reportDate.Month, 1);
                    var periodInMonth =
                        productInvPeriods.Where(pip =>
                                                pip.PeriodDate >= startMonth && pip.PeriodDate <= reportDate).ToList();
                    var earlyDate = reportDate.AddDays(-1);
                    var i = 1;
                    var products = from p in vfi.Products
                                   where productIds.Contains(p.ProductId) && p.Active
                                   select new {
                                       p.ProductId,
                                       p.ProductCode,
                                       p.Customer.CustomerCode,
                                       p.WaitingPlatingWeight
                                   };
                    var production2Ids = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    foreach (var product in products) {
                        var entity = new WaitingPlatingInvModel {
                            GlobalIndex = i,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.WaitingPlatingWeight ?? 0
                        };
                        entity.EarlyInventory = productInvPeriods
                            .Where(pip => pip.PeriodDate <= earlyDate && pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        entity.EarlyInventoryKg = entity.EarlyInventory * entity.Weight;
                        // Phan Nhap
                        entity.ImportSx1 = periodInDay
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1)
                            .Sum(pip => pip.Quantity);
                        entity.ImportSx1InMonth = periodInMonth
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1)
                            .Sum(pip => pip.Quantity);
                        entity.ImportCxl1 = periodInDay
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          (pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Processing ||
                                           pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Processing2))
                            .Sum(pip => pip.Quantity);
                        entity.ImportCxl1InMonth = periodInMonth
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          (pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Processing ||
                                           pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Processing2))
                            .Sum(pip => pip.Quantity);
                        entity.ImportSx2 = periodInDay
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                        production2Ids.Contains(pip.Transaction.WarehouseIssueId.Value))
                            .Sum(pip => pip.Quantity);
                        entity.ImportSx2InMonth = periodInMonth
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                        production2Ids.Contains(pip.Transaction.WarehouseIssueId.Value))
                            .Sum(pip => pip.Quantity);
                        entity.TotalImport = entity.ImportSx1 + entity.ImportSx2 + entity.ImportCxl1 + entity.ImportCxl2;
                        // Phan Xuat
                        entity.ExportNcu = periodInDay
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Production1)
                            .Sum(pip => pip.Quantity);
                        entity.ExportNcuInMonth = periodInMonth
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                        pip.Transaction.WarehouseReceiptId != null &&
                                         production2Ids.Contains(pip.Transaction.WarehouseReceiptId.Value))
                            .Sum(pip => pip.Quantity);
                        entity.ExportCxl2 = periodInDay
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing ||
                                           pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing2))
                            .Sum(pip => pip.Quantity);
                        entity.ExportCxl2InMonth = periodInMonth
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing ||
                                           pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing2))
                            .Sum(pip => pip.Quantity);
                        entity.TotalExport = entity.ExportNcu + entity.ExportCxl1 + entity.ExportCxl2;

                        entity.LastInventory = productInvPeriods
                            .Where(pip => pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        entity.LastInventoryKg = entity.LastInventory * entity.Weight;
                        i++;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectWaitingPlatingInvManagement", ex.Message);
            }
            return View(new GridModel(model));
        }
        #endregion

        #region Kho NCU
        [GridAction]
        public ActionResult SelectPlatingInvManagement(
            int customerId,
            //, int warehouseIssue, int warehouseReceipt
            string productCode,
            int platingType,
            string fromDate,
            string toDate) {
            var model = new List<PlatingInvModel>();
            if (platingType == 0)
                return View(new GridModel(model));
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseLastDateTime(toDate);
            //reportDate = reportDate.AddDays(1).AddSeconds(-1); 
            try {

                using (var vfi = new tammaContext()) {
                    //lay DS san pham da tung luu tru trong kho
                    var productInvs = (from pi in vfi.ProductInventories
                                       where pi.WarehouseId == platingType
                                       && (customerId == 0 || pi.Product.CustomerId == customerId)
                                       orderby pi.Product.Customer.CustomerCode, pi.Product.ProductCode
                                       select new {
                                           pi.ProductId,
                                           pi.Product.ProductCode,
                                           pi.Product.PlatingWeight,
                                           pi.Product.CustomerId,
                                           pi.Product.Customer.CustomerCode,
                                       }).ToList();
                    if (!string.IsNullOrWhiteSpace(productCode))
                        productInvs = productInvs.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    var productIds = productInvs.Select(p => p.ProductId).Distinct();
                    // lay thong tin luan chuyen toi ngay bao cao
                    var productInvPeriods = (from pip in vfi.ProductInventoryPeriods
                                             where productIds.Contains(pip.ProductId)
                                                   && pip.PeriodDate <= tDate &&
                                          pip.WarehouseId == platingType
                                             select new {
                                                 pip.ProductId,
                                                 PeriodQuantity =
                                             pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                                 pip.PeriodDate,
                                                 pip.Transaction,
                                                 pip.WarehouseId,
                                                 pip.Quantity
                                             }).ToList();
                    // lay thong tin luan chuyen trong ngay
                    //var periodInDay =
                    //    productInvPeriods.Where(pip =>
                    //                            pip.PeriodMonth == reportDate.Month &&
                    //                            pip.PeriodYear == reportDate.Year &&
                    //                            pip.PeriodDay == reportDate.Day).ToList();
                    //var startMonth = new DateTime(fDate.Year, fDate.Month, 1);
                    var periodInMonth =
                        productInvPeriods.Where(pip =>
                                                pip.PeriodDate >= fDate && pip.PeriodDate <= tDate).ToList();
                    //var earlyDate = reportDate.AddDays(-1);
                    var i = 1;
                    var products = from p in vfi.Products
                                   where productIds.Contains(p.ProductId) && p.Active
                                   select new {
                                       p.ProductId,
                                       p.ProductCode,
                                       p.Customer.CustomerCode,
                                       p.PlatingWeight
                                   };
                    foreach (var product in products) {
                        var entity = new PlatingInvModel {
                            GlobalIndex = i,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.PlatingWeight ?? 0
                        };
                        entity.EarlyInventory = productInvPeriods
                            .Where(pip => pip.PeriodDate <= fDate && pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        entity.ImportWaiting = periodInMonth
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating)
                            .Sum(pip => pip.Quantity);
                        entity.ExportQcB = periodInMonth
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcB)
                            .Sum(pip => pip.Quantity);
                        entity.LastInventory = productInvPeriods
                            .Where(pip => pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        if (entity.ImportWaiting + entity.ExportQcB + entity.LastInventory > 0) {
                            entity.GlobalIndex = i++;
                            model.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectPlatingInvManagement", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }
        #endregion

        #region Kho QC
        [GridAction]
        public ActionResult SelectQcInvManagement(
            int customerId, int warehouseId, string productCode,
            string fromDate, string toDate) {
            var model = new List<QcInvModel>();
            if (warehouseId == -1)
                return View(new GridModel(model));
            var ci = new CultureInfo("vi-VN");
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseLastDateTime(toDate);
            try {
                using (var vfi = new tammaContext()) {
                    //var qcIds = warehouseId == 0
                    //    ? MyUtilities.Warehouse.GetWarehouseIdQc()
                    //    : new List<int> { warehouseId };
                    //var products = (from p in vfi.Products
                    //               where p.Active
                    //               select new {
                    //                   p.ProductId,
                    //                   p.ProductCode,
                    //                   p.Customer.CustomerCode,
                    //                   p.QcWeight
                    //               }).ToList();
                    var products = (from pi in vfi.ProductInventories
                                    where (customerId == 0 || pi.Product.CustomerId == customerId) &&
                                        //qcIds.Contains(pi.WarehouseId)
                                          (warehouseId == 0
                                            ? pi.Warehouse.IsQC
                                            : pi.WarehouseId == warehouseId)
                                            && (pi.TotalQty > 0 || pi.ExportDate <= tDate || pi.ExportDate == null)
                                    select new {
                                        pi.ProductId,
                                        pi.Product.ProductCode,
                                        pi.Product.QcWeight,
                                        pi.Product.CustomerId,
                                        pi.Product.Customer.CustomerCode,
                                    })
                                    .GroupBy(x => x.ProductId)
                                    .Select(x => new {
                                        ProductId = x.Key,
                                        x.FirstOrDefault().ProductCode,
                                        x.FirstOrDefault().QcWeight,
                                        x.FirstOrDefault().CustomerId,
                                        x.FirstOrDefault().CustomerCode,
                                    })
                                    .ToList();
                    if (!string.IsNullOrWhiteSpace(productCode)) {
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    }
                    var productIds = products.Select(p => p.ProductId).Distinct().ToList();
                    // lay thong tin luan chuyen toi ngay bao cao
                    var productInvPeriods = (from pip in vfi.ProductInventoryPeriods
                                             where productIds.Contains(pip.ProductId)
                                                   && pip.PeriodDate <= tDate &&
                                                 //qcIds.Contains(pip.WarehouseId)
                                                  (warehouseId == 0
                                                    ? pip.Warehouse.IsQC
                                                    : pip.WarehouseId == warehouseId)
                                             select new {
                                                 pip.ProductId,
                                                 PeriodQuantity =
                                                 pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                                 EoI = pip.LastPeriodQuantity > pip.EarlyPeriodQuantity,
                                                 pip.PeriodDate,
                                                 //pip.Transaction,
                                                 pip.WarehouseId,
                                                 pip.Quantity,
                                                 pip.Transaction.WarehouseIssueId,
                                                 pip.Transaction.WarehouseReceiptId,
                                                 WarehouseIssue = pip.Transaction.Warehouse,
                                                 WarehouseReceipt = pip.Transaction.Warehouse1,
                                             })
                        //.GroupBy(x => new { })
                                             .ToList();
                    var importInMonths = productInvPeriods.Where(pip => pip.EoI && pip.PeriodDate >= fDate)
                                         .ToList();
                    var exportInMonths = productInvPeriods.Where(pip => !pip.EoI && pip.PeriodDate >= fDate)
                                         .ToList();
                    var i = 1;
                    foreach (var productId in productIds) {
                        var product = products.FirstOrDefault(p => p.ProductId == productId);
                        if (product == null) continue;
                        var entity = new QcInvModel {
                            GlobalIndex = ++i,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.QcWeight ?? 0
                        };
                        entity.EarlyInventory = productInvPeriods
                            .Where(pip => pip.PeriodDate < fDate && pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        entity.LastInventory = productInvPeriods
                            .Where(pip => pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);

                        var importInMonthsById = importInMonths.Where(pip => pip.ProductId == entity.ProductId).ToList();
                        entity.ImportSx1 = importInMonthsById.Where(pip => pip.WarehouseIssueId != null
                                                                        && pip.WarehouseIssue.IsProduction)
                             .Sum(pip => pip.Quantity);
                        entity.ImportCnc = importInMonthsById.Where(pip => pip.WarehouseIssueId != null
                                                                        && pip.WarehouseIssue.IsCncMilling)
                             .Sum(pip => pip.Quantity);
                        entity.ImportSx2 = importInMonthsById.Where(pip => pip.WarehouseIssueId != null
                                                                        && pip.WarehouseIssue.IsProduction2)
                             .Sum(pip => pip.Quantity);
                        entity.ImportRb = importInMonthsById.Where(pip => pip.WarehouseIssueId != null
                                                                       && pip.WarehouseIssue.IsPolish)
                             .Sum(pip => pip.Quantity);
                        entity.ImportNcu = importInMonthsById.Where(pip => pip.WarehouseIssueId != null
                                                                        && pip.WarehouseIssue.IsPlating)
                             .Sum(pip => pip.Quantity);
                        entity.ImportCxl1 = importInMonthsById.Where(pip => pip.WarehouseIssueId != null
                                                                         && pip.WarehouseIssue.IsReprocessing)
                             .Sum(pip => pip.Quantity);
                        entity.ImportQc = importInMonthsById.Where(pip => pip.WarehouseIssue.IsQC)
                             .Sum(pip => pip.Quantity);
                        entity.TotalImport = importInMonthsById.Sum(x => x.Quantity);
                        //
                        var exportInMonthsById = exportInMonths.Where(pip => pip.ProductId == entity.ProductId).ToList();
                        entity.ExportPacking = exportInMonthsById.Where(pip => pip.WarehouseReceiptId != null
                                                                            && pip.WarehouseReceipt.IsPacking)
                             .Sum(pip => pip.Quantity);
                        entity.ExportFinish = exportInMonthsById.Where(pip => pip.WarehouseReceiptId != null
                                                                           && pip.WarehouseReceipt.IsFinish)
                             .Sum(pip => pip.Quantity);
                        entity.ExportSx2 = exportInMonthsById.Where(pip => pip.WarehouseReceiptId != null
                                                                        && pip.WarehouseReceipt.IsProduction2)
                             .Sum(pip => pip.Quantity);
                        entity.ExportGcn = exportInMonthsById.Where(pip => pip.WarehouseReceiptId != null
                                                                        && pip.WarehouseReceipt.IsPlating)
                             .Sum(pip => pip.Quantity);
                        entity.ExportCxl1 = exportInMonthsById.Where(pip => pip.WarehouseReceiptId != null
                                                                         && pip.WarehouseReceipt.IsReprocessing)
                            .Sum(pip => pip.Quantity);
                        entity.ExportQc = exportInMonthsById.Where(pip => pip.WarehouseReceiptId != null
                                                                       && pip.WarehouseReceipt.IsQC)
                            .Sum(pip => pip.Quantity);
                        entity.ExportDefect = exportInMonthsById.Where(pip => pip.WarehouseReceiptId != null
                                                                           && pip.WarehouseReceipt.IsDefect == true)
                            .Sum(pip => pip.Quantity);
                        entity.TotalExport = exportInMonthsById.Sum(x => x.Quantity);
                        if (entity.Show) {
                            model.Add(entity);
                        }
                    }
                }
                return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectQcInvManagement", ex.Message);
            }
            return View(new GridModel(new List<QcInvModel>()));
        }
        #endregion

        #region Kho CXL

        [GridAction]
        public ActionResult SelectProcessingInvManagement(
            int customerId, string productCode,
            string fromDate, string toDate) {
            if (string.IsNullOrWhiteSpace(fromDate)) {
                return View(new GridModel(new List<ProcessingInvModel>()));
            }
            var model = new List<ProcessingInvModel>();
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseLastDateTime(toDate);
            //reportDate = reportDate.AddDays(1).AddSeconds(-1); 
            try {

                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    //lay DS san pham da tung luu tru trong kho
                    var products = from p in vfi.Products
                                   where p.Active && p.ProductCode.Contains(productCode) &&
                                         (customerId == 0 || p.CustomerId == customerId)
                                   select new {
                                       p.ProductId,
                                       p.ProductCode,
                                       p.Customer.CustomerCode,
                                       p.QcWeight
                                   };
                    var productIds = products.Select(p => p.ProductId).ToList();
                    var warehouseIds = MyUtilities.Warehouse.GetWarehouseId_ReProcessing();
                    var productInvs = from pi in vfi.ProductInventories
                                      where warehouseIds.Contains(pi.WarehouseId) &&
                                            productIds.Contains(pi.ProductId) &&
                                            pi.TotalQty > 0
                                      select new {
                                          pi.ProductInventoryId,
                                          pi.WarehouseId,
                                          pi.ProductId,
                                          pi.ErrorId,
                                          pi.ProcessError,
                                          pi.ImportDate,
                                          pi.DefectId,
                                          pi.ProductionDefect,
                                          pi.LotNumber,
                                      };
                    var productInvIds = productInvs.Select(pi => pi.ProductInventoryId).ToList();
                    // lay thong tin luan chuyen toi ngay bao cao
                    var productInvPeriods = (from pip in vfi.ProductInventoryPeriods
                                             where productInvIds.Contains(pip.ProductInvId) &&
                                                   pip.PeriodDate < tDate
                                             select new {
                                                 pip.ProductId,
                                                 PeriodQuantity =
                                             pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                                 pip.PeriodDate,
                                                 pip.Transaction,
                                                 pip.WarehouseId,
                                                 pip.Quantity,
                                                 pip.LastPeriodQuantity,
                                                 pip.EarlyPeriodQuantity,
                                                 pip.Transaction.WarehouseIssueId,
                                                 pip.Transaction.WarehouseReceiptId,
                                                 pip.ProductInvId
                                             }).ToList();
                    //var periodInMonth = productInvPeriods
                    //    .Where(pip => pip.PeriodDate >= from && pip.PeriodDate <= to)
                    //    .ToList();
                    foreach (var productInv in productInvs) {
                        var product = products.FirstOrDefault(p => p.ProductId == productInv.ProductId);
                        var entity = new ProcessingInvModel {
                            //GlobalIndex = i,
                            ProductInvId = productInv.ProductInventoryId,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.QcWeight ?? 0,
                            LotNumber = productInv.LotNumber,
                        };
                        if (productInv.ErrorId != null) {
                            entity.ErrorId = productInv.ErrorId.Value;
                            entity.ImportDate = productInv.ImportDate.Value.ToString("dd/MM/yyyy");
                            entity.ErrorDescription = productInv.ProcessError.Description;
                        }
                        if (productInv.DefectId != null) {
                            entity.DefectId = productInv.DefectId.Value;
                            entity.DefectName = productInv.ProductionDefect.DefectName;
                        }
                        var periodById =
                            productInvPeriods.Where(pip => pip.ProductInvId == entity.ProductInvId).ToList();

                        var periods =
                            periodById.Where(pip => pip.WarehouseId == MyUtilities.Warehouse.Processing).ToList();
                        entity.LastQuantity = periods.Sum(pip => pip.PeriodQuantity);

                        periods = periods.Where(pip => pip.PeriodDate < fDate).ToList();
                        entity.EarlyQuantity = periods.Sum(pip => pip.PeriodQuantity);

                        periodById =
                            periodById.Where(pip => pip.PeriodDate >= fDate).ToList();
                        periods =
                            periodById.Where(
                                pip =>
                                pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1 &&
                                pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing &&
                                pip.LastPeriodQuantity > pip.EarlyPeriodQuantity).ToList();
                        entity.ImportProduction1 = periods.Sum(pip => pip.Quantity);

                        periods =
                            periodById.Where(
                                pip =>
                                MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL()
                                           .Contains(pip.Transaction.WarehouseIssueId.Value) &&
                                pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing &&
                                pip.LastPeriodQuantity > pip.EarlyPeriodQuantity).ToList();
                        entity.ImportProduction2 = periods.Sum(pip => pip.Quantity);
                        periods =
                            periodById.Where(
                                pip =>
                                (pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.QcA ||
                                 pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.QcB) &&
                                pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing &&
                                pip.LastPeriodQuantity > pip.EarlyPeriodQuantity).ToList();
                        entity.ImportProduction2 = periods.Sum(pip => pip.Quantity);

                        periods =
                            periodById.Where(
                                pip =>
                                pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing &&
                                pip.LastPeriodQuantity > pip.EarlyPeriodQuantity).ToList();
                        entity.TotalImport = periods.Sum(pip => pip.Quantity);
                        entity.ImportElse = entity.TotalImport - entity.ImportProduction1 - entity.ImportProduction2 -
                                            entity.ImportQc;

                        periods =
                            periodById.Where(
                                pip =>
                                pip.WarehouseReceiptId == MyUtilities.Warehouse.Defect &&
                                pip.EarlyPeriodQuantity > pip.LastPeriodQuantity).ToList();
                        entity.ExportDefect = periods.Sum(pip => pip.Quantity);

                        periods =
                            periodById.Where(
                                pip =>
                                pip.WarehouseReceiptId == MyUtilities.Warehouse.ReProcessing &&
                                pip.EarlyPeriodQuantity > pip.LastPeriodQuantity).ToList();
                        entity.ExportReProcess = periods.Sum(pip => pip.Quantity);
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProcessingInvManagement", ex.Message);
            }
            return View(new GridModel(model.Where(m => m.Show).OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }

        [GridAction]
        public ActionResult SelectProcessingInvQuantity(
            int customerId, string productCode,
            string fromDate, string toDate) {
            //if (string.IsNullOrWhiteSpace(fromDate)) {
            //    return View(new GridModel(new List<ProcessingInvModel>()));
            //}
            var model = new List<ProcessingInvModel>();
            //var ci = new CultureInfo("vi-VN");
            //var from = string.IsNullOrWhiteSpace(fromDate)
            //                  ? DateTime.Today
            //                  : Convert.ToDateTime(fromDate, ci);
            //var to = string.IsNullOrWhiteSpace(toDate)
            //                  ? DateTime.Today
            //                  : Convert.ToDateTime(toDate, ci);
            //reportDate = reportDate.AddDays(1).AddSeconds(-1); 
            try {

                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    //lay DS san pham da tung luu tru trong kho
                    var products = (from p in vfi.Products
                                    where p.Active && p.ProductCode.Contains(productCode) &&
                                          (customerId == 0 || p.CustomerId == customerId)
                                    select new {
                                        p.ProductId,
                                        p.ProductCode,
                                        p.Customer.CustomerCode,
                                        p.QcWeight
                                    }).ToList();
                    var productIds = products.Select(p => p.ProductId).ToList();
                    var warehouseIds = MyUtilities.Warehouse.GetWarehouseId_ReProcessing();
                    var productInvs = (from pi in vfi.ProductInventories
                                       where warehouseIds.Contains(pi.WarehouseId) &&
                                             productIds.Contains(pi.ProductId) &&
                                             pi.TotalQty > 0
                                       select new {
                                           pi.ProductInventoryId,
                                           pi.WarehouseId,
                                           pi.ProductId,
                                           pi.ErrorId,
                                           pi.ProcessError,
                                           pi.ImportDate,
                                           pi.DefectId,
                                           pi.ProductionDefect,
                                           pi.LotNumber,
                                           pi.TotalQty,
                                           pi.Warehouse.WarehouseName,
                                           pi.StoreCode
                                       }).ToList();
                    var productInvIds = productInvs.Select(pi => pi.ProductInventoryId).ToList();

                    foreach (var productInv in productInvs) {
                        var product = products.FirstOrDefault(p => p.ProductId == productInv.ProductId);
                        var entity = new ProcessingInvModel {
                            //GlobalIndex = i,
                            ProductInvId = productInv.ProductInventoryId,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.QcWeight ?? 0,
                            LotNumber = productInv.LotNumber,
                            ReprocessingInv = productInv.TotalQty,
                            WarehouseName = productInv.WarehouseName,
                            StoreCode = productInv.StoreCode
                        };
                        if (productInv.ErrorId != null) {
                            entity.ErrorId = productInv.ErrorId.Value;
                            entity.ImportDate = productInv.ImportDate.Value.ToString("dd/MM/yyyy");
                            entity.ErrorDescription = productInv.ProcessError.Description;
                        }
                        if (productInv.DefectId != null) {
                            entity.DefectId = productInv.DefectId.Value;
                            entity.DefectName = productInv.ProductionDefect.DefectName;
                        }

                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProcessingInvManagement", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ThenBy(m => m.WarehouseName)));
        }
        #endregion

        #region Kho Phe Pham
        [GridAction]
        public ActionResult SelectDefectInvManagement(
            int customerId,
            //, int warehouseIssue, int warehouseReceipt
            string productCode,
            string monthlyDate) {
            var model = new List<DefectInvModel>();
            var ci = new CultureInfo("vi-VN");
            var reportDate = string.IsNullOrWhiteSpace(monthlyDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(monthlyDate, ci);
            //reportDate = reportDate.AddDays(1).AddSeconds(-1);
            var startMonth = new DateTime(reportDate.Year, reportDate.Month, 1);
            try {
                using (var vfi = new tammaContext()) {
                    //lay DS san pham da tung luu tru trong kho
                    var products = (from pi in vfi.ProductInventories
                                    where pi.WarehouseId == MyUtilities.Warehouse.Defect
                                    orderby pi.Product.Customer.CustomerCode, pi.Product.ProductCode
                                    select new {
                                        pi.ProductId,
                                        pi.Product.ProductCode,
                                        pi.Product.QcWeight,
                                        pi.Product.CustomerId,
                                        pi.Product.Customer.CustomerCode,
                                    }).ToList();
                    if (customerId != 0)
                        products = products.Where(p => p.CustomerId == customerId).ToList();
                    if (!string.IsNullOrWhiteSpace(productCode))
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    var productIds = products.Select(p => p.ProductId).Distinct();
                    // lay thong tin luan chuyen toi ngay bao cao
                    var productInvPeriods = (from pip in vfi.ProductInventoryPeriods
                                             where productIds.Contains(pip.ProductId)
                                                && pip.PeriodDate <= reportDate && pip.PeriodDate >= startMonth &&
                                          pip.WarehouseId == MyUtilities.Warehouse.Defect
                                             select new {
                                                 pip.ProductId,
                                                 PeriodQuantity =
                                             pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                                 pip.PeriodDate,
                                                 pip.Transaction,
                                                 pip.WarehouseId,
                                                 pip.Quantity
                                             }).ToList();
                    // lay thong tin luan chuyen trong ngay
                    var periodInDay =
                        productInvPeriods.Where(pip =>
                                                pip.PeriodDate == reportDate).ToList();
                    var periodInMonth =
                        productInvPeriods.Where(pip =>
                                                pip.PeriodDate >= startMonth && pip.PeriodDate <= reportDate).ToList();
                    var earlyDate = reportDate.AddDays(-1);
                    var i = 1;
                    foreach (var product in products) {
                        var entity = new DefectInvModel {
                            GlobalIndex = i,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.QcWeight ?? 0
                        };
                        entity.EarlyInventory = productInvPeriods
                            .Where(pip => pip.PeriodDate <= earlyDate && pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        entity.ImportSx1 = periodInDay
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1)
                            .Sum(pip => pip.Quantity);
                        entity.ImportSx1InMonth = periodInMonth
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1)
                            .Sum(pip => pip.Quantity);
                        entity.ImportCxl1 = periodInDay
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          (pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Processing ||
                                           pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Processing2))
                            .Sum(pip => pip.Quantity);
                        entity.ImportCxl1InMonth = periodInDay
                            .Where(pip => pip.ProductId == entity.ProductId &&
                                          (pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Processing ||
                                           pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Processing2))
                            .Sum(pip => pip.Quantity);
                        entity.TotalImport = entity.ImportSx1 + entity.ImportCxl1 + entity.ImportCxl2;
                        entity.LastInventory = productInvPeriods
                            .Where(pip => pip.ProductId == entity.ProductId)
                            .Sum(pip => pip.PeriodQuantity);
                        i++;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDefectInvManagement", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }
        #endregion

        #region Kho TP
        [GridAction]
        public ActionResult SelectFinishInvManagement(
            int customerId,
            //, int warehouseIssue, int warehouseReceipt
            string productCode,
            string monthlyDate) {
            var model = new List<FinishInvModel>();
            var reportDate = MyUtilities.Function.ParseLastDateTime(monthlyDate);
            //reportDate = reportDate.AddDays(1).AddSeconds(-1);
            try {

                using (var vfi = new tammaContext()) {
                    //lay DS san pham da tung luu tru trong kho
                    var products = (from pi in vfi.ProductInventories
                                    where pi.WarehouseId == MyUtilities.Warehouse.Finish
                                    orderby pi.Product.Customer.CustomerCode, pi.Product.ProductCode
                                    select new {
                                        pi.ProductId,
                                        pi.Product.ProductCode,
                                        pi.Product.FinishWeight,
                                        pi.Product.CustomerId,
                                        pi.Product.Customer.CustomerCode,
                                    }).ToList();
                    if (customerId != 0)
                        products = products.Where(p => p.CustomerId == customerId).ToList();
                    if (!string.IsNullOrWhiteSpace(productCode))
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    var productIds = products.Select(p => p.ProductId).Distinct();
                    // lay thong tin luan chuyen toi ngay bao cao
                    var productInvPeriods = (from pip in vfi.ProductInventoryPeriods
                                             where productIds.Contains(pip.ProductId)
                                                   && pip.PeriodDate <= reportDate &&
                                    (pip.WarehouseId == MyUtilities.Warehouse.Finish)
                                             select new {
                                                 pip.ProductId,
                                                 PeriodQuantity =
                                             pip.LastPeriodQuantity - pip.EarlyPeriodQuantity,
                                                 pip.PeriodDate,
                                                 pip.Transaction,
                                                 pip.WarehouseId,
                                                 pip.Quantity
                                             }).ToList();
                    // lay thong tin luan chuyen trong ngay
                    var periodInDay = productInvPeriods.Where(pip => pip.PeriodDate == reportDate).ToList();
                    var startMonth = new DateTime(reportDate.Year, reportDate.Month, 1);
                    var periodInMonth =
                        productInvPeriods.Where(pip =>
                        pip.PeriodDate >= startMonth && pip.PeriodDate <= reportDate).ToList();
                    //nhập
                    var earlyDate = reportDate.AddDays(-1);
                    var i = 1;
                    foreach (var productId in productIds) {
                        var product = products.FirstOrDefault(p => p.ProductId == productId);
                        var entity = new FinishInvModel {
                            GlobalIndex = i,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            Weight = product.FinishWeight ?? 0
                        };
                        entity.EarlyInventory = productInvPeriods.Where(
                            pip => pip.PeriodDate <= earlyDate && pip.ProductId == productId).Sum(pip => pip.PeriodQuantity);

                        entity.ImportQc = periodInDay.Where(
                            pip =>
                            pip.ProductId == entity.ProductId &&
                            (pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.QcA ||
                             pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.QcB))
                                                     .Sum(pip => pip.Quantity);
                        entity.ImportQcInMonth = periodInMonth.Where(
                            pip =>
                            pip.ProductId == entity.ProductId &&
                            (pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.QcA ||
                             pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.QcB))
                                                     .Sum(pip => pip.Quantity);
                        entity.ExportCxl = periodInDay.Where(
                            pip =>
                            pip.ProductId == entity.ProductId &&
                            (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing ||
                             pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing2))
                                                     .Sum(pip => pip.Quantity);
                        entity.ExportCxlInMonth = periodInMonth
                            .Where(pip =>
                                   pip.ProductId == entity.ProductId &&
                                   (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing ||
                                    pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Processing2))
                            .Sum(pip => pip.Quantity);
                        entity.ExportKd = periodInDay.Where(
                            pip =>
                            pip.ProductId == entity.ProductId &&
                            (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Business))
                                                     .Sum(pip => pip.Quantity);
                        entity.ExportKdInMonth = periodInMonth.Where(
                            pip =>
                            pip.ProductId == entity.ProductId &&
                            (pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Business))
                                                     .Sum(pip => pip.Quantity);
                        entity.TotalExport = entity.ExportCxl + entity.ExportKd;
                        entity.LastInventory = productInvPeriods.Where(
                            pip => pip.ProductId == entity.ProductId).Sum(pip => pip.PeriodQuantity);
                        i++;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectFinishInvManagement", ex.Message);
            }
            return View(new GridModel(model));
        }
        #endregion

        #region XNT kho
        [GridAction]
        public ActionResult SelectProductInvStatistic(
            int customerId, string productCode,
            string fromDate, string toDate,
            int warehouse1, int warehouse2, int warehouse3, int warehouse4, int warehouse5,
            int warehouse6, int warehouse7, int warehouse8, int warehouse9, int warehouse10
            ) {
            var model = new List<ProductInvStatisticModel>();
            try {
                var fDate = MyUtilities.Function.ParseDate(fromDate);
                var tDate = MyUtilities.Function.ParseLastDateTime(toDate);
                using (var vfi = new tammaContext()) {
                    var warehouseIds = new List<int>();
                    if (warehouse1 != 0) warehouseIds.Add(warehouse1);
                    if (warehouse2 != 0) warehouseIds.Add(warehouse2);
                    if (warehouse3 != 0) warehouseIds.Add(warehouse3);
                    if (warehouse4 != 0) warehouseIds.Add(warehouse4);
                    if (warehouse5 != 0) warehouseIds.Add(warehouse5);
                    if (warehouse6 != 0) warehouseIds.Add(warehouse6);
                    if (warehouse7 != 0) warehouseIds.Add(warehouse7);
                    if (warehouse8 != 0) warehouseIds.Add(warehouse8);
                    if (warehouse9 != 0) warehouseIds.Add(warehouse9);
                    if (warehouse10 != 0) warehouseIds.Add(warehouse10);
                    if (!warehouseIds.Any())
                        return View(new GridModel(model));
                    var productInvPeriods = (from pip in vfi.ProductInventoryPeriods
                                             where warehouseIds.Contains(pip.WarehouseId) &&
                                             pip.PeriodDate >= fDate && pip.PeriodDate <= tDate
                                             select new {
                                                 pip.WarehouseId,
                                                 pip.Warehouse.ShortName,
                                                 pip.ProductId,
                                                 pip.Product.ProductCode,
                                                 pip.Product.Customer.CustomerCode,
                                                 pip.Product.CustomerId,
                                                 pip.LastPeriodQuantity,
                                                 pip.EarlyPeriodQuantity,
                                                 pip.Quantity
                                             }).ToList();
                    var productIds = productInvPeriods.Select(pip => pip.ProductId).Distinct().ToList();
                    foreach (var productId in productIds) {
                        var productInvPeriodsById = productInvPeriods.Where(pip => pip.ProductId == productId).ToList();
                        if (!productInvPeriodsById.Any()) continue;
                        var product = productInvPeriodsById.FirstOrDefault();
                        var entity = new ProductInvStatisticModel {
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.CustomerCode,
                            CustomerId = product.CustomerId
                        };
                        model.Add(entity);
                        if (warehouse1 != 0) {
                            var productInvPeriodsById2 = productInvPeriodsById.Where(pip => pip.WarehouseId == warehouse1);
                            entity.Detail1.Weight = MyUtilities.Product.GetProductInvWeight(entity.ProductId, warehouse1);
                            entity.Detail1.Import = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity > pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                            entity.Detail1.Export = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity < pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                        }
                        if (warehouse2 != 0) {
                            var productInvPeriodsById2 = productInvPeriodsById.Where(pip => pip.WarehouseId == warehouse2);
                            entity.Detail2.Weight = MyUtilities.Product.GetProductInvWeight(entity.ProductId, warehouse2);
                            entity.Detail2.Import = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity > pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                            entity.Detail2.Export = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity < pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                        }
                        if (warehouse3 != 0) {
                            var productInvPeriodsById2 = productInvPeriodsById.Where(pip => pip.WarehouseId == warehouse3);
                            entity.Detail3.Weight = MyUtilities.Product.GetProductInvWeight(entity.ProductId, warehouse3);
                            entity.Detail3.Import = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity > pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                            entity.Detail3.Export = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity < pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                        }
                        if (warehouse4 != 0) {
                            var productInvPeriodsById2 = productInvPeriodsById.Where(pip => pip.WarehouseId == warehouse4);
                            entity.Detail4.Weight = MyUtilities.Product.GetProductInvWeight(entity.ProductId, warehouse4);
                            entity.Detail4.Import = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity > pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                            entity.Detail4.Export = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity < pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                        }
                        if (warehouse5 != 0) {
                            var productInvPeriodsById2 = productInvPeriodsById.Where(pip => pip.WarehouseId == warehouse5);
                            entity.Detail5.Weight = MyUtilities.Product.GetProductInvWeight(entity.ProductId, warehouse5);
                            entity.Detail5.Import = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity > pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                            entity.Detail5.Export = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity < pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                        }
                        if (warehouse6 != 0) {
                            var productInvPeriodsById2 = productInvPeriodsById.Where(pip => pip.WarehouseId == warehouse6);
                            entity.Detail6.Weight = MyUtilities.Product.GetProductInvWeight(entity.ProductId, warehouse6);
                            entity.Detail6.Import = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity > pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                            entity.Detail6.Export = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity < pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                        }
                        if (warehouse7 != 0) {
                            var productInvPeriodsById2 = productInvPeriodsById.Where(pip => pip.WarehouseId == warehouse7);
                            entity.Detail7.Weight = MyUtilities.Product.GetProductInvWeight(entity.ProductId, warehouse7);
                            entity.Detail7.Import = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity > pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                            entity.Detail7.Export = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity < pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                        }
                        if (warehouse8 != 0) {
                            var productInvPeriodsById2 = productInvPeriodsById.Where(pip => pip.WarehouseId == warehouse8);
                            entity.Detail8.Weight = MyUtilities.Product.GetProductInvWeight(entity.ProductId, warehouse8);
                            entity.Detail8.Import = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity > pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                            entity.Detail8.Export = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity < pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                        }
                        if (warehouse9 != 0) {
                            var productInvPeriodsById2 = productInvPeriodsById.Where(pip => pip.WarehouseId == warehouse9);
                            entity.Detail9.Weight = MyUtilities.Product.GetProductInvWeight(entity.ProductId, warehouse9);
                            entity.Detail9.Import = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity > pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                            entity.Detail9.Export = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity < pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                        }
                        if (warehouse10 != 0) {
                            var productInvPeriodsById2 = productInvPeriodsById.Where(pip => pip.WarehouseId == warehouse10);
                            entity.Detail10.Weight = MyUtilities.Product.GetProductInvWeight(entity.ProductId, warehouse10);
                            entity.Detail10.Import = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity > pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                            entity.Detail10.Export = productInvPeriodsById2
                                .Where(pip => pip.LastPeriodQuantity < pip.EarlyPeriodQuantity)
                                .ToList()
                                .Sum(pip => pip.Quantity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductInvStatistic", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }
        #endregion

        #endregion

        #region Production

        [GridAction]
        public ActionResult SelectProductionLock(string fromDate, string toDate) {
            var model = new List<ProductionLockModel>();
            try {
                model = GetProductionLock(fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionLock", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionLockModel> GetProductionLock(string fromDate, string toDate) {
            var model = new List<ProductionLockModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fDate = string.IsNullOrWhiteSpace(fromDate)
                                ? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
                                : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                                ? DateTime.Today
                                : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    var productionLocks = from pl in vfi.ProductionLocks
                                          where pl.LockDate >= fDate && pl.LockDate <= tDate
                                          orderby pl.LockDate descending
                                          select pl;
                    foreach (var productionLock in productionLocks) {
                        var entity = new ProductionLockModel() {
                            LockId = productionLock.LockId,
                            LockDate = productionLock.LockDate,
                            Production1Lock = productionLock.Production1Lock,
                            Production2Lock = productionLock.Production2Lock,
                            CNCLock = productionLock.CNCLock,
                            ModifiedDate = productionLock.ModifiedDate,
                            ModifiedUser = productionLock.ModifiedUser,
                            Shift1Name = productionLock.Shift1Name,
                            Shift2Name = productionLock.Shift2Name
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model;
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionLock(ProductionLockModel insert, string fromDate, string toDate) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    if (string.IsNullOrWhiteSpace(insert.Shift1Name) || string.IsNullOrWhiteSpace(insert.Shift2Name))
                        throw new AggregateException("Lỗi! Chưa nhập ca sản xuất");
                    var locked = vfi.ProductionLocks.FirstOrDefault(pl => pl.LockDate == insert.LockDate);
                    if (locked == null) {
                        var entity = new ProductionLock {
                            LockDate = insert.LockDate,
                            Production1Lock = insert.Production1Lock,
                            Production2Lock = insert.Production2Lock,
                            CNCLock = insert.CNCLock,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Shift1Name = insert.Shift1Name,
                            Shift2Name = insert.Shift2Name
                        };
                        vfi.ProductionLocks.Add(entity);
                    }
                    else {
                        locked.ModifiedDate = DateTime.Now;
                        locked.ModifiedUser = HttpContext.User.Identity.Name;
                        locked.CNCLock = insert.CNCLock;
                        locked.Production1Lock = insert.Production1Lock;
                        locked.Production2Lock = insert.Production2Lock;
                        locked.Shift1Name = insert.Shift1Name;
                        locked.Shift2Name = insert.Shift2Name;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionLock", ex.Message);
            }
            return View(new GridModel(GetProductionLock(fromDate, toDate)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionLock(ProductionLockModel update, string fromDate, string toDate) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    if (string.IsNullOrWhiteSpace(update.Shift1Name) || string.IsNullOrWhiteSpace(update.Shift2Name))
                        throw new AggregateException("Lỗi! Chưa nhập ca sản xuất");
                    var locked = vfi.ProductionLocks.FirstOrDefault(pl => pl.LockDate == update.LockDate);
                    if (locked == null) {
                        locked = new ProductionLock {
                            LockDate = update.LockDate,
                            Production1Lock = update.Production1Lock,
                            Production2Lock = update.Production2Lock,
                            CNCLock = update.CNCLock,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Shift1Name = update.Shift1Name,
                            Shift2Name = update.Shift2Name
                        };
                        vfi.ProductionLocks.Add(locked);
                    }
                    else {
                        locked.ModifiedDate = DateTime.Now;
                        locked.ModifiedUser = HttpContext.User.Identity.Name;
                        locked.CNCLock = update.CNCLock;
                        locked.Production1Lock = update.Production1Lock;
                        locked.Production2Lock = update.Production2Lock;
                        locked.Shift1Name = update.Shift1Name;
                        locked.Shift2Name = update.Shift2Name;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionLock", ex.Message);
            }
            return View(new GridModel(GetProductionLock(fromDate, toDate)));
        }
        #endregion

        #region shelf - drawer

        [GridAction]
        public ActionResult SelectInventoryOnShelf(int classifiedId, string fromDate, string toDate, int drawerId) {
            var model = new List<OnShelfModel>();
            try {
                model = GetInventoryOnShelf(classifiedId, fromDate, toDate, drawerId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectInventoryOnShelf", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<OnShelfModel> GetInventoryOnShelf(int classifiedId, string fromDate, string toDate, int drawerId) {
            var model = new List<OnShelfModel>();
            try {
                var fDate = MyUtilities.Function.ParseDate(fromDate);
                var tDate = MyUtilities.Function.ParseDate(toDate);
                using (var vfi = new tammaContext()) {
                    model = (from x in vfi.OnShelves
                             where (drawerId == 0
                                     ? x.OnDate >= fDate && x.OnDate <= tDate
                                     : x.DrawerId == drawerId)
                                    && x.Active
                             select new OnShelfModel {
                                 OnShelfId = x.OnShelfId,
                                 OnDate = x.OnDate,
                                 ModifiedDate = x.ModifiedDate,
                                 ModifiedUser = x.ModifiedUser,
                                 DrawerId = x.DrawerId,
                                 ReferenceId = x.ReferenceId,
                                 ReferenceInvId = x.ReferenceInvId,
                                 ClassifiedId = classifiedId
                             }).ToList();
                    var referenceInvIds = model.Select(x => x.ReferenceInvId).Distinct().ToList();
                    var referenceIds = model.Select(x => x.ReferenceId).Distinct().ToList();
                    var drawerIds = model.Select(x => x.DrawerId).Distinct().ToList();
                    var drawers = (from x in vfi.InventoryDrawers
                                   where drawerIds.Contains(x.DrawerId)
                                   select new InventoryDrawerModel {
                                       DrawerId = x.DrawerId,
                                       ColumnName = x.ColumnName,
                                       RowName = x.RowName,
                                       ShelfName = x.InventoryShelf.ShelfName
                                   }).ToList();
                    switch (classifiedId) {
                        case 1:
                            var materialInvs = (from x in vfi.MaterialInventories
                                                where referenceInvIds.Contains(x.MaterialInventoryId)
                                                select new MaterialInventoryModel {
                                                    MaterialInventoryId = x.MaterialInventoryId,
                                                    VendorCode = x.Vendor.VendorCode,
                                                    VendorName = x.Vendor.VendorName,
                                                    MaterialId = x.MaterialId,
                                                    MaterialName = x.Material.MaterialName,
                                                    OutDiameter = x.Material.OutDiameter,
                                                    InDiameter = x.Material.InDiameter,
                                                    Shape = x.Material.Shape,
                                                    DiameterType = x.Material.DiameterType,
                                                    LotNumber = x.LotNumber,
                                                    Length = x.Length,
                                                    Quantity = x.TotalQty,
                                                    UnitWeight = x.UnitWeight,
                                                }).ToList();
                            var materials = (from x in vfi.Materials
                                             where referenceIds.Contains(x.MaterialId)
                                             select new {
                                                 Id = x.MaterialId,
                                                 Code = x.MaterialCode
                                             }).ToList();
                            foreach (var onshelve in model) {
                                var drawer = drawers.FirstOrDefault(x => x.DrawerId == onshelve.DrawerId);
                                if (drawer != null) {
                                    onshelve.DrawerCode = drawer.DrawerCode;
                                }
                                if (onshelve.ReferenceInvId > 0) {
                                    var materialInv = materialInvs.FirstOrDefault(x => x.MaterialInventoryId == onshelve.ReferenceInvId);
                                    onshelve.ReferenceInvCode = materialInv.MaterialCodeLotNumber;
                                    onshelve.Unit = "Kg";
                                    onshelve.Quantity = materialInv.Quantity * materialInv.UnitWeight;
                                    onshelve.UnitWeight = materialInv.UnitWeight;
                                    onshelve.LotNumber = materialInv.LotNumber;
                                    onshelve.OwnerName = materialInv.VendorName;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model.OrderByDescending(x => x.OnDate).ThenBy(x => x.DrawerCode).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertInventoryOnShelf(OnShelfModel insert, int classifiedId, string fromDate, string toDate) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var drawerId = 0;
                    try { drawerId = Convert.ToInt32(insert.DrawerCode); }
                    catch (FormatException) { }
                    var invId = 0;
                    try { invId = Convert.ToInt32(insert.ReferenceInvCode); }
                    catch (FormatException) { }
                    if (drawerId == 0 || invId == 0) {
                        throw new AggregateException("Lỗi! Không tìm thấy data, vui lòng chọn lại vị trí kho và tồn");
                    }
                    var materialInv = vfi.MaterialInventories.FirstOrDefault(x => x.MaterialInventoryId == invId);
                    var onshelf = new OnShelf() {
                        DrawerId = drawerId,
                        ReferenceInvId = invId,
                        OnDate = insert.OnDate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ReferenceId = materialInv.MaterialId,
                        Active = true,
                    };
                    vfi.OnShelves.Add(onshelf);
                    vfi.SaveChanges();
                    //}
                    //else {
                    //    onshelf.ModifiedUser = HttpContext.User.Identity.Name;
                    //    onshelf.ModifiedDate = DateTime.Now;
                    //}
                    //var drawer = vfi.InventoryDrawers.FirstOrDefault(x => x.DrawerId == drawerId);
                    //drawer.ReferenceInvId = onshelf.ReferenceInvId;
                    //drawer.ReferenceId = onshelf.ReferenceId;
                    //vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertInventoryOnShelf", ex.Message);
            }
            return View(new GridModel(GetInventoryOnShelf(classifiedId, fromDate, toDate, 0)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateInventoryOnShelf(OnShelfModel update, int classifiedId, string fromDate, string toDate) {
            try {
                throw new AggregateException("Lỗi! Chưa hổ trợ cập nhật");
                //if (!Request.IsAuthenticated) {
                //    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                //                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                //                             "Xin vui lòng đăng nhập lại hệ thống.");
                //}
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateInventoryOnShelf", ex.Message);
            }
            return View(new GridModel(GetInventoryOnShelf(classifiedId, fromDate, toDate, 0)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult DeleteInventoryOnShelf(int onShelfId, int classifiedId, string fromDate, string toDate, int drawerId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var onShelf = vfi.OnShelves.FirstOrDefault(x => x.OnShelfId == onShelfId);
                    if (onShelf == null) { throw new AggregateException("Lỗi! Không tìm thấy kệ chứa NL"); }
                    onShelf.Active = false;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteInventoryOnShelf", ex.Message);
            }
            return View(new GridModel(GetInventoryOnShelf(classifiedId, fromDate, toDate, drawerId)));
        }
        #endregion
    }
}
