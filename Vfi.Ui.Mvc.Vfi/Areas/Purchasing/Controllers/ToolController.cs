using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Sales.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Utilities;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Controllers {
    public class ToolController : Controller {
        //
        // GET: /Purchasing/Tool/
        #region view
        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }

            // 08/08/2026
            //var ci = new CultureInfo("vi-VN");
            //var ci2 = new CultureInfo("en-US");
            //ci.NumberFormat.NumberDecimalSeparator = ".";
            //ci.NumberFormat.NumberGroupSeparator = ",";
            //Thread.CurrentThread.CurrentCulture = ci2;
            //Thread.CurrentThread.CurrentUICulture = ci;

            return ViewData;
        }

        public ActionResult ToolTransactionManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ToolTransactionDetail() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ApproveTransactionTool() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult AssignTool() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult AssignToolProduction2() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["ListExportToolInvIds"] = new List<int>();
            return View();
        }

        public ActionResult ExportToolInventory() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["ListExportToolInvIds"] = new List<int>();
            return View();
        }

        public ActionResult ImportToolInventory() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ImportToolInventoryB() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ImportTransactionTool() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ImportInternalTransactionTool() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ExportInternalTransactionTool() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ToolInventoryCard() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ToolImportExportReport() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ChestManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ToolUseManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ToolUseCreate() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ToolUseRetrieve() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ToolUseApprovement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ToolUseInvManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ToolExportReport() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        #endregion

        #region tool management

        [GridAction]
        public ActionResult SelectProductionTool(int productId) {
            var model = new List<ProductionToolModel>();
            try {
                model = GetProductionToolByProductId(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionTool", ex.Message);
            }
            return View(new GridModel(model));
        }
        List<ProductionToolModel> GetProductionToolByProductId(int productId) {
            var model = new List<ProductionToolModel>();
            using (var vfi = new tammaContext()) {
                var productionTools = vfi.ProductionTools.Where(pt => pt.Active && pt.ProductId == productId);
                model.AddRange(productionTools.Select(productionTool => new ProductionToolModel {
                    RealToolId = productionTool.RealToolId,
                    ToolId = productionTool.ToolId,
                    ProductId = productionTool.ProductId,
                    UseNumber = productionTool.UseNumber,
                    Active = productionTool.Active,
                    ModifiedDate = productionTool.ModifiedDate,
                    ModifiedUser = productionTool.ModifiedUser,
                    ToolFullCode = productionTool.Tool.ToolFullCode,
                    ToolCode = productionTool.Tool.ToolCode,
                    ToolName = productionTool.Tool.ToolName,
                    ToolDesign = productionTool.Tool.ToolDesignNo,
                    ToolMaterial = productionTool.Tool.ToolMaterial,
                    ToolProduction = productionTool.Tool.ToolProduction,
                    Note = productionTool.Note,
                    Quota = productionTool.Quota,
                    Description = productionTool.Description,
                    ToolIndex = productionTool.ToolIndex ?? 0,
                    ToolLocation = productionTool.ToolLocation,
                    ToolActive = productionTool.Tool.Active,
                    NamingToolId = productionTool.NamingToolId,
                    NamingToolName = productionTool.Tool1.ToolName,
                    ProcessWarehouseId = productionTool.ProcessWarehouseId,
                    ProcessWarehouseName = productionTool.ProcessWarehouseId != null
                                                    ? productionTool.Warehouse.WarehouseName
                                                    : ""
                }));
            }
            return model.OrderBy(m => m.ToolIndex).ToList();

        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionTool(ProductionToolModel insert, int productId) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null)
                        throw new AggregateException("Vui lòng đăng nhập lại");
                    int toolId = 0;
                    try {
                        toolId = Convert.ToInt32(insert.ToolFullCode);
                    }
                    catch (Exception) { }
                    int namingToolId = 0;
                    try {
                        namingToolId = Convert.ToInt32(insert.NamingToolName);
                    }
                    catch (Exception) { }
                    if (toolId <= 0 || namingToolId <= 0) {
                        throw new AggregateException("Vui lòng chọn lại công cụ");
                    }
                    Nullable<int> processWarehouseId = null;
                    try {
                        processWarehouseId = Convert.ToInt32(insert.ProcessWarehouseName);
                    }
                    catch (Exception) { }
                    var entity =
                        vfi.ProductionTools.FirstOrDefault(
                            pt => pt.ProductId == productId && pt.ToolId == toolId && !pt.Active);
                    if (entity == null) {
                        entity = new ProductionTool {
                            ToolId = toolId,
                            ProductId = productId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Active = true,
                            //UseNumber = insert.UseNumber,
                            UseNumber = 1,
                            Note = insert.Note,
                            Quota = insert.Quota,
                            ToolIndex = insert.ToolIndex,
                            Description = insert.Description,
                            ToolLocation = insert.ToolLocation,
                            NamingToolId = namingToolId,
                            ProcessWarehouseId = processWarehouseId
                        };
                        vfi.ProductionTools.Add(entity);
                    }
                    else {
                        entity.ModifiedDate = DateTime.Now;
                        entity.ModifiedUser = HttpContext.User.Identity.Name;
                        entity.Active = true;
                        //entity.UseNumber = insert.UseNumber;
                        entity.UseNumber = 1;
                        entity.Note = insert.Note;
                        entity.Quota = insert.Quota;
                        entity.ToolIndex = insert.ToolIndex;
                        entity.Description = insert.Description;
                        entity.ToolLocation = insert.ToolLocation;
                        entity.NamingToolId = namingToolId;
                        entity.ProcessWarehouseId = processWarehouseId;
                    }
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionTool", ex.Message);
            }

            return View(new GridModel(GetProductionToolByProductId(productId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionTool(ProductionToolModel update, int productId) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null)
                        throw new AggregateException("Vui lòng đăng nhập lại");
                    var entity = vfi.ProductionTools.FirstOrDefault(pt => pt.RealToolId == update.RealToolId);
                    if (entity == null)
                        throw new AggregateException("Lỗi! Không tìm thấy công cụ trong sản phấm! Liên hệ admin");
                    int toolId = 1;
                    try {
                        toolId = Convert.ToInt32(update.ToolFullCode);
                    }
                    catch (Exception) {
                        toolId = entity.ToolId;
                    }
                    int namingToolId = 0;
                    try {
                        namingToolId = Convert.ToInt32(update.NamingToolName);
                    }
                    catch (Exception) {
                        namingToolId = entity.NamingToolId;
                    }
                    if (toolId <= 0 || namingToolId <= 0) {
                        throw new AggregateException("Vui lòng chọn lại công cụ");
                    }
                    Nullable<int> processWarehouseId = null;
                    try {
                        if (!string.IsNullOrWhiteSpace(update.ProcessWarehouseName))
                            processWarehouseId = Convert.ToInt32(update.ProcessWarehouseName);
                    }
                    catch (Exception) {
                        processWarehouseId = entity.ProcessWarehouseId;
                    }
                    entity.ToolId = toolId;
                    //entity.ProductId = productId;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    entity.Active = update.Active;
                    //entity.UseNumber = update.UseNumber;
                    entity.UseNumber = 1;
                    entity.Note = update.Note;
                    entity.Quota = update.Quota;
                    entity.ToolIndex = update.ToolIndex;
                    entity.Description = update.Description;
                    entity.ToolLocation = update.ToolLocation;
                    entity.NamingToolId = namingToolId;
                    entity.ProcessWarehouseId = processWarehouseId;
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionTool", ex.Message);
            }

            return View(new GridModel(GetProductionToolByProductId(productId)));
        }


        [GridAction]
        public ActionResult SelectProductionToolReplacement(int trackId) {
            var model = new List<ProductionToolReplacementModel>();
            try {
                model = GetProductionToolReplacement(trackId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionTool", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionToolReplacementModel> GetProductionToolReplacement(int trackId) {
            var model = new List<ProductionToolReplacementModel>();
            using (var vfi = new tammaContext()) {
                var track = vfi.TrackUpMachines.FirstOrDefault(t => t.TrackId == trackId);
                if (track == null)
                    throw new AggregateException("Lỗi!");
                var replaceTools = vfi.ProductionToolReplacements.Where(pt => pt.TrackId == trackId ||
                    (pt.TrackUpMachine.MachineId == track.MachineId &&
                    pt.TrackUpMachine.ProductId == track.ProductId));
                var productionTools = vfi.ProductionTools.Where(pt => pt.Active && pt.ProductId == track.ProductId);
                //var requirementTools = vfi.ToolInventoryRequirements;
                foreach (var productionTool in productionTools) {
                    var entity = new ProductionToolReplacementModel {
                        //ReplaceId = replaceTool.ReplaceId,
                        ProductionToolId = productionTool.RealToolId,
                        ProductionToolFullCode = productionTool.Tool.ToolFullCode,
                        ToolImg = productionTool.Tool.Img,
                        UploadDate = productionTool.Tool.ModifiedDate.ToString("yyyyMMddhhmmss"),
                        TrackId = trackId,
                        Note = productionTool.Note,
                        ModifiedDate = productionTool.ModifiedDate,
                        ModifiedUser = productionTool.ModifiedUser,
                        //ReplaceToolId = replaceTool.ToolId,
                        //AutoPrepareQuantity = track.Quantity / 
                    };
                    if (productionTool.Quota > 0) {
                        entity.AutoPrepareQuantity = MyUtilities.Function.RoundUp(track.Quantity / productionTool.Quota);
                    }

                    var toolId = productionTool.ToolId;
                    if (string.IsNullOrWhiteSpace(entity.ToolImg))
                        entity.ToolImg = "askquestion.jpg";

                    var replaceTool = replaceTools.FirstOrDefault(rt => rt.ProductionToolId == entity.ProductionToolId);
                    if (replaceTool != null) {
                        if (replaceTool.ToolId != null) {
                            entity.ReplaceToolId = replaceTool.ToolId.Value;
                            entity.ReplaceToolFullCode = replaceTool.Tool.ToolFullCode;
                            toolId = entity.ReplaceToolId;
                            entity.ReplaceImg = replaceTool.Tool.Img;
                        }
                        if (string.IsNullOrWhiteSpace(entity.ReplaceImg))
                            entity.ReplaceId = replaceTool.ReplaceId;
                        entity.PrepareQuantity = replaceTool.PrepareQuantity;
                        entity.Note = replaceTool.Note;
                        entity.ModifiedDate = replaceTool.ModifiedDate;
                        entity.ModifiedUser = replaceTool.ModifiedUser;
                    }
                    entity.ReplaceImg = "askquestion.jpg";
                    var toolInvs = vfi.ToolInventories.Where(ti => ti.ToolId == toolId);
                    if (toolInvs.Any()) {
                        entity.TotalInv = toolInvs.Sum(ti => ti.TotalQuantity);
                    }

                    var requirementTools = vfi.ToolInventoryRequirements.Where(tir => tir.ToolId == toolId).ToList();
                    if (requirementTools.Any()) {
                        entity.RequireInventory = requirementTools.LastOrDefault().RequireQuantity;
                    }
                    model.Add(entity);
                }
            }
            return model.ToList();

        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionToolReplacement(ProductionToolReplacementModel update, int trackId) {
            using (var vfi = new tammaContext()) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                                 "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                                 "Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    var replaceTool =
                        vfi.ProductionToolReplacements.FirstOrDefault(tr => tr.ReplaceId == update.ReplaceId);
                    int toolId = 0;
                    try {
                        toolId = Convert.ToInt32(update.ReplaceToolFullCode);
                    }
                    catch (Exception) {
                        toolId = vfi.Tools.FirstOrDefault(w => w.ToolFullCode.Equals(update.ReplaceToolFullCode)).ToolId;
                    }
                    if (replaceTool == null) {
                        replaceTool = new ProductionToolReplacement {
                            //ToolId = toolId != 0 ? toolId :  null,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            Note = update.Note,
                            TrackId = trackId,
                            ProductionToolId = update.ProductionToolId,
                            PrepareQuantity = update.PrepareQuantity
                        };
                        if (toolId != 0)
                            replaceTool.ToolId = toolId;
                        vfi.ProductionToolReplacements.Add(replaceTool);
                    }
                    else {
                        replaceTool.TrackId = trackId;
                        replaceTool.ModifiedUser = HttpContext.User.Identity.Name;
                        replaceTool.PrepareQuantity = update.PrepareQuantity;
                        replaceTool.ModifiedDate = DateTime.Now;
                        replaceTool.Note = update.Note;
                        if (toolId != 0)
                            replaceTool.ToolId = toolId;
                        else
                            replaceTool.ToolId = null;
                    }
                    vfi.SaveChanges();
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProductionTool", ex.Message);
                }
            }
            return View(new GridModel(GetProductionToolReplacement(trackId)));
        }


        #endregion

        [GridAction]
        public ActionResult SelectTransactionTool(string toolCode, byte status, string fromDate, string toDate) {

            var model = new List<TransactionFptModel>();
            try {
                var fdate = MyUtilities.Function.ParseDate(fromDate);
                var tdate = MyUtilities.Function.ParseDate(toDate);
                using (var vfi = new tammaContext()) {

                    var purchasing = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.PurchasingManagement);

                    var transactions = (from i in vfi.TransactionFpts
                                        where
                                        //i.TransactionCode == "VFMT-269753" &&
                                        i.Status == status && i.TransactionDate >= fdate &&
                                        i.TransactionDate <= tdate && i.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool
                                        select i).ToList();
                    if (!string.IsNullOrWhiteSpace(toolCode)) {
                        var toolIds = vfi.Tools.Where(t => t.ToolFullCode.Contains(toolCode)).Select(t => t.ToolId).ToList();
                        if (toolIds.Any()) {
                            transactions = transactions.Where(t => t.TransactionFptDetails.Any(td => toolIds.Contains(td.FptId))).ToList();
                        }
                    }


                    foreach (var transaction in transactions) {
                        var entity = new TransactionFptModel {
                            TransactionId = transaction.TransactionId,
                            TransactionCode = transaction.TransactionCode,
                            ModifiedDate = transaction.ModifiedDate,
                            ModifiedUser = transaction.ModifiedUser,
                            TransactionDate = transaction.TransactionDate,
                            PoId = transaction.PoId ?? 0,
                            Status = transaction.Status,
                            EoI = transaction.EoI,
                            Fpt = transaction.Fpt,
                            Type = transaction.Type,
                            InventorySignature = transaction.InventorySignature,
                            AccountantSignature = transaction.AccountantSignature,
                            QcSignature = transaction.QcSignature,
                            PurchasingSignature = transaction.PurchasingSignature,
                            CanUpdate = false,
                        };
                       
                // get machineName
                        var transactionList = entity.TransactionId;
                        var machineMap = vfi.TransactionFptDetails
                                            .Where(t => transactionList== t.TransactionId)
                                            .Select(t => t.MachineId)
                                            .ToList();
                        var machineName = string.Join(", ",
                            vfi.Machines
                               .Where(t => machineMap.Contains(t.MachineId))
                               .Select(t => t.MachineName)
                               .ToList());
                        entity.MachineName = machineName;



                        if (entity.PoId != 0)
                            entity.PoCode = transaction.PurchaseOrder.RevisionNumber;
                        entity.StatusName = MyUtilities.Transaction.CastText.GetTextStatus(entity.Status);
                        if (entity.EoI != 0) {
                            entity.EoIName = MyUtilities.PurchaseOrder.GetEoIName(entity.EoI, entity.Type);
                            if (entity.EoI == (byte)MyUtilities.PurchaseOrder.EoILot.Export) {
                                var exportTool = transaction.ExportTools.FirstOrDefault();
                                if (exportTool != null) {
                                    entity.Department = exportTool.Department;
                                    entity.Note = exportTool.Description;
                                }
                            }
                        }
                        //if (transaction.TransactionFptDetails.Any(td => td.MachineId == null))
                        //{
                        //    entity.EoIName += " huỷ";
                        //}
                        if (entity.Fpt != 0)
                            entity.FptName = MyUtilities.PurchaseOrder.GetFptName(entity.Fpt);
                        if (entity.Status == (byte)MyUtilities.Transaction.Status.Approved) {
                            if (purchasing) {
                                entity.PurchasingSignatureType = 1;
                                entity.CanUpdate = purchasing;
                            }
                        }
                        foreach (var detail in transaction.TransactionFptDetails) {
                            entity.TotalQuantity += detail.Quantity;
                            entity.TotalPrice += (detail.Quantity * detail.UnitPrice);
                        }
                        if (transaction.IsInternal == true) { entity.EoIName += " nội bộ"; }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTransactionTool", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.TransactionDate)));
        }

        [GridAction]
        public ActionResult SelectTransactionToolDetail(int transactionId) {
            var model = new List<TransactionFptDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = (from tf in vfi.TransactionFpts
                                       where tf.TransactionId == transactionId
                                       select tf).FirstOrDefault();
                    if (transaction == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu");
                    var export = vfi.ExportTools.FirstOrDefault(et => et.TransactionId == transaction.TransactionId);
                    foreach (var detail in transaction.TransactionFptDetails) {
                        var tool = vfi.Tools.FirstOrDefault(t => t.ToolId == detail.FptId);
                        var vendor = vfi.Vendors.FirstOrDefault(v => v.VendorId == detail.VendorId);
                        if (tool == null || vendor == null) throw new AggregateException("Lỗi! Liên hệ admin");
                        var entity = new TransactionFptDetailModel {
                            LotNumber = detail.LotNumber,
                            Note = detail.Note,
                            Quantity = detail.Quantity,
                            UnitPrice = detail.UnitPrice,
                            ToolCode = tool.ToolCode,
                            ToolName = tool.ToolName,
                            ToolDesignNo = tool.ToolDesignNo,
                            ToolFullCodeName = tool.ToolFullCode,
                            UnitMeasure = detail.UnitMeasure,
                            VendorId = vendor.VendorId,
                            VendorName = vendor.VendorName,
                            TotalInv = 0,
                            ToolMaterial = tool.ToolMaterial,
                            PoDetailId = detail.PoDetailId ?? 0
                        };
                        if (!string.IsNullOrWhiteSpace(detail.LotNumber)) {
                            var toolInv =
                                vfi.ToolInventories.FirstOrDefault(
                                    ti =>
                                    ti.ToolId == tool.ToolId && ti.VendorId == vendor.VendorId &&
                                    ti.LotNumber.Equals(detail.LotNumber) && ti.UnitPrice == detail.UnitPrice);
                            entity.TotalInv = (toolInv == null ? 0 : toolInv.TotalQuantity);
                        }
                        if (transaction.EoI == (byte)MyUtilities.PurchaseOrder.EoILot.Export && export != null) {
                            var exportDetail =
                                vfi.ExportToolDetails.FirstOrDefault(
                                    ed => ed.ExportId == export.ExportId && ed.TransactionDetailId == detail.DetailId);
                            if (exportDetail != null) {
                                entity.UnitPrice = exportDetail.ToolInventory.UnitPrice;
                                var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == exportDetail.MachineId);
                                if (machine != null)
                                    entity.Note += "| " + machine.MachineName;
                                var product = vfi.Products.FirstOrDefault(m => m.ProductId == exportDetail.ProductId);
                                if (product != null)
                                    entity.Note += ("-" + product.ProductCode);
                            }
                        }
                        entity.Price = entity.UnitPrice * entity.Quantity;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTransactionToolDetail", ex.Message);
            }
            return View(new GridModel(model));
        }


        [GridAction]
        public ActionResult SelectDetailTransactionTool(int status, string fromdate, string todate) {
            var model = new List<TransactionFptDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var fdate = MyUtilities.Function.ParseDate(fromdate);
                    var tdate = MyUtilities.Function.ParseDate(todate);
                    var transactions = (from tf in vfi.TransactionFpts
                                        where tf.Status == status
                                        && tf.TransactionDate >= fdate
                                        && tf.TransactionDate <= tdate
                                        && tf.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool
                                        select tf.TransactionId).ToList();
                    if (transactions.Count == 0)
                        throw new AggregateException("Loi 01: Khong co ket qua");
                    var detailTransactions = vfi.TransactionFptDetails.Where(t => transactions.Contains(t.TransactionId)).Select(t => t).ToList();
                    if (detailTransactions.Count == 0) {
                        throw new AggregateException("Loi 02");
                    }
                    var index = 1;
                    foreach (var detail in detailTransactions) {
                        var tool = vfi.Tools.FirstOrDefault(t => t.ToolId == detail.FptId);
                        if (tool == null) {
                            throw new AggregateException("Loi 03");
                        }
                        var entity = new TransactionFptDetailModel {
                            Quantity = detail.Quantity,
                            VendorId = detail.VendorId,
                            Status = detail.TransactionFpt.Status,
                            ToolName = tool.ToolName,
                            //ToolCode = tool.ToolCode,
                            ToolFullCodeName = tool.ToolFullCode,
                            ToolId = detail.FptId,
                            PoId = detail.TransactionFpt.PoId ?? 0,
                            TransactionCode = detail.TransactionFpt.TransactionCode,
                            TransactionDate = detail.TransactionFpt.TransactionDate,
                            LotNumber = detail.LotNumber,
                            UnitPrice = detail.UnitPrice,
                            Note = detail.Note,
                            UnitMeasure = detail.UnitMeasure,
                            EoI = detail.TransactionFpt.EoI,
                            Index = index, 


                        };
                        var toolInvs = vfi.ToolInventories.Where(t => t.ToolId == entity.ToolId && t.TotalQuantity > 0).ToList();
                        entity.ToolInv = toolInvs.Sum(t => t.TotalQuantity);
                        entity.Price = entity.UnitPrice * entity.Quantity;
                        if (entity.PoId != 0)
                            entity.PoCode = detail.TransactionFpt.PurchaseOrder.RevisionNumber;
                        entity.StatusName = MyUtilities.Transaction.CastText.GetTextStatus(entity.Status); entity.VendorName = vfi.Vendors.Where(t => t.VendorId == entity.VendorId).Select(t => t.VendorName).FirstOrDefault();
                        if (entity.EoI != 0) {
                            entity.EoIName = MyUtilities.PurchaseOrder.GetEoIName(entity.EoI, entity.Type);
                        }
                        if (detail.TransactionFpt.IsInternal == true) { entity.EoIName += " nội bộ"; }


                        model.Add(entity);
                        index++;




                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDetailTransactionTool", ex.Message);
            }
            return View(new GridModel(model));
        }





        [GridAction]
        public ActionResult SelectToolInPo(string ids, int poId) {
            if (string.IsNullOrWhiteSpace(ids))
                return View(new GridModel(new List<TransactionFptDetailModel>()));

            if (poId == 0)
                return View(new GridModel(new List<TransactionFptDetailModel>()));

            //int[] checkedRecords;
            var checkedRecords = new List<long>();
            try {
                var lst = ids.Split(':');
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords.Add(Convert.ToInt64(lst.ElementAt(i)));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<TransactionFptDetailModel>()));
            }

            var model = new List<TransactionFptDetailModel>();

            try {
                using (var vfi = new tammaContext()) {
                    //var poDetails = (from pod in vfi.PurchaseOrderDetails
                    //                 where checkedRecords.Contains(pod.PurchaseOrderDetailId)
                    //                       && !pod.IsComplete.Value
                    //                 select new
                    //                     {
                    //                         pod.ReferenceId,
                    //                         pod.PurchaseOrderDetailId,
                    //                         pod.PurchaseOrder.Vendor,
                    //                         pod.OrderQty,
                    //                         pod.ReceivedQty,
                    //                         pod.RejectedQty,
                    //                         pod.UnitPrice,
                    //                         pod.Unit
                    //                     }).ToList();
                    //var toolIds = poDetails.Select(pod => pod.ReferenceId).ToList();
                    //var tools = from f in vfi.Tools
                    //            where toolIds.Contains(f.ToolId)
                    //            select f;
                    foreach (var id in checkedRecords) {
                        var poDetail =
                            vfi.PurchaseOrderDetails.FirstOrDefault(
                                pod => pod.PurchaseOrderDetailId == id);
                        var tool = vfi.Tools.FirstOrDefault(m => m.ToolId == poDetail.ReferenceId);
                        if (poDetail.IsComplete.Value)
                            throw new AggregateException("Lỗi!" + tool.ToolFullCode + " đã nhập đủ số lượng!");
                        var entity = new TransactionFptDetailModel() {
                            ToolId = tool.ToolId,
                            ToolName = tool.ToolName,
                            ToolFullCodeName = tool.ToolFullCode,
                            RequiredQuantity = poDetail.OrderQty - poDetail.ReceivedQty,
                            VendorName = poDetail.PurchaseOrder.Vendor.ShortName,
                            UnitMeasure = poDetail.Unit,
                            UnitPrice = poDetail.UnitPrice,
                            DetailId = poDetail.PurchaseOrderDetailId,
                            PoDetailId = poDetail.PurchaseOrderDetailId
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectToolInPo", ex.Message);
            }
            return
                View(
                    new GridModel(
                        model.OrderBy(m => m.ToolFullCodeName)));
        }

        [GridAction]
        public ActionResult UpdateImportTool(
            [Bind(Prefix = "inserted")] IEnumerable<TransactionFptDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<TransactionFptDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<TransactionFptDetailModel> deletedDetails,
            int vendor, string importDate, int poId, int exchangeRate) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ImportWorkpiece",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<TransactionFptDetailModel>()));
            }
            try {
                var date = MyUtilities.Function.ParseDate(importDate);
                if (date > DateTime.Now.AddDays(1)) {
                    throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                }
                if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                    throw new AggregateException(
                        @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                }
                try {
                    if (!insertedDetails.Any() || insertedDetails == null)
                        return View(new GridModel(new List<TransactionFptDetailModel>()));
                }
                catch {
                    if (!updatedDetails.Any() || updatedDetails == null)
                        return View(new GridModel(new List<TransactionFptDetailModel>()));
                }
                using (var vfi = new tammaContext()) {
                    var transaction = new TransactionFpt {
                        TransactionDate = date,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        PoId = null,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        TransactionFptDetails = new List<TransactionFptDetail>(),
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Tool, 1),
                        EoI = Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Import),
                        Type = 1,
                        Fpt = Convert.ToInt16(MyUtilities.PurchaseOrder.FptLot.Tool),
                        AccountantSignature = 0,
                        InventorySignature = 0,
                        PurchasingSignature = 0,
                        QcSignature = 0,
                        ExchangeRate = exchangeRate,
                    };
                    var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == poId);
                    if (purchaseOrder != null) {
                        vendor = purchaseOrder.VendorId;
                        transaction.PoId = purchaseOrder.PurchaseOrderId;
                        if (!purchaseOrder.CurrencyCode.Trim().Equals("VND"))
                            if (transaction.ExchangeRate <= 1)
                                throw new AggregateException("Lỗi tiền tệ ! Chưa nhập tỉ giá.");

                    }
                    else if (transaction.ExchangeRate <= 0)
                        throw new AggregateException("Lỗi tiền tệ ! Chưa nhập tỉ giá.");
                    if (vendor == 0)
                        throw new AggregateException("Lỗi nhà cung cấp ! Vui lòng kiểm tra lại.");
                    var list = insertedDetails == null
                                   ? updatedDetails
                                   : insertedDetails;
                    foreach (var detailModel in list) {
                        if (detailModel.ToolId == 0)
                            throw new AggregateException("Lỗi công cụ ! Vui lòng kiểm tra lại.\n" +
                                                         detailModel.ToolFullCodeName);
                        if (detailModel.Quantity <= 0) continue;
                        var toolInv =
                            vfi.ToolInventories.FirstOrDefault(
                                ti =>
                                ti.ToolId == detailModel.ToolId && ti.LotNumber.Equals(detailModel.LotNumber.Trim()));
                        if (toolInv != null) {
                            if (toolInv.ToolInventoryPeriods.Any() && purchaseOrder == null) {
                                detailModel.UnitPrice = toolInv.UnitPrice;
                            }
                            else {
                                toolInv.UnitPrice = detailModel.UnitPrice;
                            }
                        }
                        if (detailModel.UnitPrice <= 0 && poId == 0)
                            throw new AggregateException("Lỗi đơn giá ! Vui lòng kiểm tra lại.\n" +
                                                         detailModel.ToolFullCodeName);
                        var detail = new TransactionFptDetail {
                            FptId = detailModel.ToolId,
                            Quantity = detailModel.Quantity,
                            UnitPrice = Math.Round(detailModel.UnitPrice * transaction.ExchangeRate, 0),
                            LotNumber = (detailModel.LotNumber + "").Trim(),
                            Note = detailModel.Note,
                            UnitMeasure = detailModel.UnitMeasure,
                            TransactionFpt = transaction,
                            TransactionId = transaction.TransactionId,
                            VendorId = vendor,
                            PoDetailId = detailModel.PoDetailId,
                        };
                        //if (purchaseOrder != null)
                        //{
                        //    detail.PoReferenceDetailId
                        //}
                        transaction.TransactionFptDetails.Add(detail);
                    }
                    if (transaction.TransactionFptDetails.Any()) {
                        vfi.TransactionFpts.Add(transaction);
                        vfi.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException dbEx) {
                foreach (var validationErrors in dbEx.EntityValidationErrors) {
                    foreach (var validationError in validationErrors.ValidationErrors) {
                        ModelState.AddModelError("UpdateImportTool",
                                                 "Property: " + validationError.PropertyName + " Error: " +
                                                 validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateImportTool", "" + exception.Message);
            }
            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        [GridAction]
        public ActionResult CreateImportInternalTool(
            [Bind(Prefix = "inserted")] IEnumerable<TransactionFptDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<TransactionFptDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<TransactionFptDetailModel> deletedDetails,
            int vendor, string importDate, int poId, int exchangeRate) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ImportWorkpiece",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<TransactionFptDetailModel>()));
            }
            try {
                var ci = new CultureInfo("vi-VN");
                var date = string.IsNullOrWhiteSpace(importDate)
                               ? DateTime.Today
                               : Convert.ToDateTime(importDate, ci);
                if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                    throw new AggregateException(
                        @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                }
                try {
                    if (!insertedDetails.Any() || insertedDetails == null)
                        return View(new GridModel(new List<TransactionFptDetailModel>()));
                }
                catch {
                    if (!updatedDetails.Any() || updatedDetails == null)
                        return View(new GridModel(new List<TransactionFptDetailModel>()));
                }
                using (var vfi = new tammaContext()) {
                    var transaction = new TransactionFpt {
                        TransactionDate = date,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        PoId = null,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        TransactionFptDetails = new List<TransactionFptDetail>(),
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Tool, 1),
                        EoI = Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Import),
                        Type = 1,
                        Fpt = Convert.ToInt16(MyUtilities.PurchaseOrder.FptLot.Tool),
                        AccountantSignature = 0,
                        InventorySignature = 0,
                        PurchasingSignature = 0,
                        QcSignature = 0,
                        ExchangeRate = exchangeRate,
                        IsInternal = true,
                    };
                    var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == poId);
                    if (purchaseOrder != null) {
                        vendor = purchaseOrder.VendorId;
                        transaction.PoId = purchaseOrder.PurchaseOrderId;
                        if (!purchaseOrder.CurrencyCode.Trim().Equals("VND"))
                            if (transaction.ExchangeRate <= 1)
                                throw new AggregateException("Lỗi tiền tệ ! Chưa nhập tỉ giá.");

                    }
                    else if (transaction.ExchangeRate <= 0)
                        throw new AggregateException("Lỗi tiền tệ ! Chưa nhập tỉ giá.");
                    if (vendor == 0)
                        throw new AggregateException("Lỗi nhà cung cấp ! Vui lòng kiểm tra lại.");
                    var list = insertedDetails == null
                                   ? updatedDetails
                                   : insertedDetails;
                    foreach (var detailModel in list) {
                        if (detailModel.ToolId == 0)
                            throw new AggregateException("Lỗi công cụ ! Vui lòng kiểm tra lại.\n" +
                                                         detailModel.ToolFullCodeName);
                        if (detailModel.Quantity <= 0) continue;
                        var toolInv =
                            vfi.ToolInventories.FirstOrDefault(
                                ti =>
                                ti.ToolId == detailModel.ToolId && ti.LotNumber.Equals(detailModel.LotNumber.Trim()));
                        if (toolInv != null) {
                            if (toolInv.ToolInventoryPeriods.Any()) {
                                detailModel.UnitPrice = toolInv.UnitPrice;
                            }
                            else {
                                toolInv.UnitPrice = detailModel.UnitPrice;
                            }
                        }
                        //if (detailModel.UnitPrice <= 0 && poId == 0)
                        //    throw new AggregateException("Lỗi đơn giá ! Vui lòng kiểm tra lại.\n" +
                        //                                 detailModel.ToolFullCodeName);
                        var detail = new TransactionFptDetail {
                            FptId = detailModel.ToolId,
                            Quantity = detailModel.Quantity,
                            UnitPrice = Math.Round(detailModel.UnitPrice * transaction.ExchangeRate, 0),
                            LotNumber = (detailModel.LotNumber + "").Trim(),
                            Note = detailModel.Note,
                            UnitMeasure = detailModel.UnitMeasure,
                            TransactionFpt = transaction,
                            TransactionId = transaction.TransactionId,
                            VendorId = vendor,
                            PoDetailId = detailModel.PoDetailId,
                            IsInternal = transaction.IsInternal
                        };
                        //if (purchaseOrder != null)
                        //{
                        //    detail.PoReferenceDetailId
                        //}
                        transaction.TransactionFptDetails.Add(detail);
                    }
                    if (transaction.TransactionFptDetails.Any()) {
                        vfi.TransactionFpts.Add(transaction);
                        vfi.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException dbEx) {
                foreach (var validationErrors in dbEx.EntityValidationErrors) {
                    foreach (var validationError in validationErrors.ValidationErrors) {
                        ModelState.AddModelError("UpdateImportTool",
                                                 "Property: " + validationError.PropertyName + " Error: " +
                                                 validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateImportTool", "" + exception.Message);
            }
            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        [GridAction]
        public ActionResult UpdateImportToolB(
            [Bind(Prefix = "inserted")] IEnumerable<TransactionFptDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<TransactionFptDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<TransactionFptDetailModel> deletedDetails,
            int vendor, string importDate) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ImportWorkpiece",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<TransactionFptDetailModel>()));
            }
            try {
                var date = MyUtilities.Function.ParseDate(importDate);
                if (date > DateTime.Now.AddDays(1)) {
                    throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                }
                if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                    throw new AggregateException(
                        @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                }
                try {
                    if (!insertedDetails.Any() || insertedDetails == null)
                        return View(new GridModel(new List<TransactionFptDetailModel>()));
                }
                catch {
                    if (!updatedDetails.Any() || updatedDetails == null)
                        return View(new GridModel(new List<TransactionFptDetailModel>()));
                }
                using (var vfi = new tammaContext()) {
                    var transaction = new TransactionFpt {
                        TransactionDate = date,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        PoId = null,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        TransactionFptDetails = new List<TransactionFptDetail>(),
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Tool, 1),
                        EoI = Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Import),
                        Type = 1,
                        Fpt = Convert.ToInt16(MyUtilities.PurchaseOrder.FptLot.Tool),
                        AccountantSignature = 0,
                        InventorySignature = 0,
                        PurchasingSignature = 0,
                        QcSignature = 0,
                        ExchangeRate = 1,
                    };
                    if (vendor == 0)
                        throw new AggregateException("Lỗi nhà cung cấp ! Vui lòng kiểm tra lại.");
                    var list = insertedDetails == null
                                   ? updatedDetails
                                   : insertedDetails;
                    foreach (var detailModel in list) {
                        if (detailModel.ToolId == 0)
                            throw new AggregateException("Lỗi công cụ ! Vui lòng kiểm tra lại.\n" +
                                                         detailModel.ToolFullCodeName);
                        if (detailModel.Quantity <= 0) continue;
                        var detail = new TransactionFptDetail {
                            FptId = detailModel.ToolId,
                            Quantity = detailModel.Quantity,
                            UnitPrice = 0,
                            LotNumber = "-B",
                            Note = detailModel.Note,
                            UnitMeasure = detailModel.UnitMeasure,
                            TransactionFpt = transaction,
                            TransactionId = transaction.TransactionId,
                            VendorId = vendor,
                        };
                        //if (purchaseOrder != null)
                        //{
                        //    detail.PoReferenceDetailId
                        //}
                        transaction.TransactionFptDetails.Add(detail);
                    }
                    if (transaction.TransactionFptDetails.Any()) {
                        vfi.TransactionFpts.Add(transaction);
                        vfi.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException dbEx) {
                foreach (var validationErrors in dbEx.EntityValidationErrors) {
                    foreach (var validationError in validationErrors.ValidationErrors) {
                        ModelState.AddModelError("UpdateImportTool",
                                                 "Property: " + validationError.PropertyName + " Error: " +
                                                 validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateImportTool", "" + exception.Message);
            }
            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        [GridAction]
        public ActionResult SelectWaitingTransactionTool() {
            try {
                return View(new GridModel(GetTransactionToolModel().OrderByDescending(m => m.TransactionDate)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectWaitingTransactionTool", "" + ex.Message);
            }
            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        private List<TransactionFptModel> GetTransactionToolModel() {
            var model = new List<TransactionFptModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var purchasing = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.PurchasingManagement);

                    var transactions = from import in vfi.TransactionFpts
                                       where import.Status == (byte)MyUtilities.Transaction.Status.Open
                                             && import.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool
                                       select import;
                    foreach (var transaction in transactions) {
                        var entity = new TransactionFptModel {
                            TransactionId = transaction.TransactionId,
                            TransactionCode = transaction.TransactionCode,
                            ModifiedDate = transaction.ModifiedDate,
                            ModifiedUser = transaction.ModifiedUser,
                            TransactionDate = transaction.TransactionDate,
                            PoId = transaction.PoId ?? 0,
                            Status = transaction.Status,
                            EoI = transaction.EoI,
                            Fpt = transaction.Fpt,
                            FptName = MyUtilities.PurchaseOrder.GetFptName(transaction.Fpt),
                            Type = transaction.Type,
                            PurchasingSignature = transaction.PurchasingSignature,
                            PurchasingSignatureType = 0,
                            AccountantSignatureType = 0,
                            IsInternal = transaction.IsInternal ?? false,
                            TotalQuantity = transaction.TransactionFptDetails.Sum(td => td.Quantity),
                            TotalPrice = transaction.TransactionFptDetails.Sum(td => td.Quantity * td.UnitPrice)
                        };
                        var exportTool = transaction.ExportTools.FirstOrDefault();
                        if (exportTool != null) {
                            entity.Department = exportTool.Department;
                            entity.Note = exportTool.Description;
                        }
                        if (entity.PoId > 0) {
                            entity.PoCode = transaction.PurchaseOrder.RevisionNumber;
                            entity.Note = transaction.PurchaseOrder.RevisionNumber + "-" +
                                          transaction.PurchaseOrder.CurrencyCode.Trim() + "-" +
                                          transaction.ExchangeRate;
                        }
                        entity.StatusName = MyUtilities.Transaction.CastText.GetTextStatus(entity.Status);
                        if (entity.EoI != 0)
                            entity.EoIName = MyUtilities.PurchaseOrder.GetEoIName(entity.EoI, 0);
                        if (entity.IsInternal) {
                            entity.EoIName += " nội bộ";
                        }
                        else if (entity.EoI == (byte)MyUtilities.PurchaseOrder.EoILot.Import) {
                        }
                        else if (entity.EoI == (byte)MyUtilities.PurchaseOrder.EoILot.Export) {
                            if (entity.Type == (byte)MyUtilities.Tool.ExportType.Destroy) {
                                entity.EoIName += " huỷ";
                                entity.AccountantSignatureType = 1;
                            }
                        }

                        if (entity.PurchasingSignature != 1) {
                            if (purchasing) {
                                entity.PurchasingSignatureType = 1;
                            }
                        }

                        entity.AlertColor = 1;
                        if (entity.TransactionDate > DateTime.Now.AddDays(4) ||
                            entity.TransactionDate < DateTime.Now.AddDays(-4))
                            entity.AlertColor = 2;
                        else if (entity.TransactionDate > DateTime.Now.AddDays(1) ||
                                 entity.TransactionDate < DateTime.Now.AddDays(-1))
                            entity.AlertColor = 0;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetTransactionToolModel", ex.Message);
            }
            return model.OrderByDescending(m => m.TransactionDate).ToList();
        }

        private ToolInventory GetToolInventory(int toolId, string lotNumber, int vendorId, double unitPrice,
                                               DateTime date, string unitMeasure) {
            using (var vfi = new tammaContext()) {
                var toolInv = vfi.ToolInventories.FirstOrDefault(
                    ti =>
                    ti.ToolId == toolId && ti.LotNumber.Equals(lotNumber) &&
                    ti.VendorId == vendorId && ti.UnitPrice == unitPrice);
                if (toolInv == null) {
                    toolInv = new ToolInventory {
                        ToolId = toolId,
                        CreateDate = date,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        LotNumber =
                            MyUtilities.PurchaseOrder.GetFptLotParam((int)MyUtilities.PurchaseOrder.FptLot.Tool, toolId,
                                                       date, 0),
                        UnitMeasure = unitMeasure,
                        UnitPrice = unitPrice,
                        TotalQuantity = 0,
                        VendorId = vendorId,
                    };
                    if (!string.IsNullOrWhiteSpace(lotNumber))
                        toolInv.LotNumber = lotNumber;
                    vfi.ToolInventories.Add(toolInv);
                    //vfi.SaveChanges();
                }
                return toolInv;
            }

        }

        [HttpPost]
        public ActionResult UpdateApproveTransactionTool(long[] checkedRecords) {
            try {
                if (!Request.IsAuthenticated)
                    return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                var modifiedUser = HttpContext.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(modifiedUser))
                    return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
                if (checkedRecords.Count() <= 0)
                    return Json(@"Vui lòng chọn ít nhất 1 lệnh. (checkedRecords <= 0). ");
                using (var vfi = new tammaContext()) {
                    var transactions =
                        vfi.TransactionFpts.Where(f => checkedRecords.Contains(f.TransactionId));
                    var transaction = transactions.FirstOrDefault();
                    //foreach (var transaction in transactions) {
                        if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, transaction.TransactionDate)) {
                            throw new AggregateException(
                                @"Không có quyền duyệt phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                        }
                        var purchaseOrder =
                            vfi.PurchaseOrders.FirstOrDefault(
                                po => po.PurchaseOrderId == transaction.PoId);

                        if (transaction.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import) {// nhap
                            var list = new List<ToolInventory>();
                            foreach (var detail in transaction.TransactionFptDetails) {
                                var period = new ToolInventoryPeriod {
                                    //ToolInventory = toolInv,
                                    ToolId = detail.FptId,
                                    TransactionId = transaction.TransactionId,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    Quantity = detail.Quantity,
                                    PeriodDate = transaction.TransactionDate,
                                };
                                //var toolInv = GetToolInventory(detail.FptId, detail.LotNumber, detail.VendorId,
                                //                               detail.UnitPrice,
                                //                               transaction.TransactionDate, detail.UnitMeasure);
                                var toolInv =
                                    vfi.ToolInventories.FirstOrDefault(
                                        ti =>
                                        ti.ToolId == detail.FptId && ti.LotNumber.Equals(detail.LotNumber) &&
                                        ti.VendorId == detail.VendorId && ti.UnitPrice == detail.UnitPrice);
                                if (toolInv == null) {
                                    if (string.IsNullOrWhiteSpace(detail.LotNumber))
                                        toolInv = list.FirstOrDefault(
                                            ti =>
                                                ti.ToolId == detail.FptId && //ti.LotNumber.Equals(detail.LotNumber) &&
                                                ti.VendorId == detail.VendorId && ti.UnitPrice == detail.UnitPrice);
                                    if (toolInv == null) {
                                        var count = list.Count(l => l.ToolId == detail.FptId);
                                        toolInv = new ToolInventory {
                                            ToolId = detail.FptId,
                                            CreateDate = transaction.TransactionDate,
                                            ModifiedDate = DateTime.Now,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            //LotNumber =
                                            //    MyUtilities.PurchaseOrder.GetFptLotParam(
                                            //        (int)MyUtilities.PurchaseOrder.FptLot.Tool, detail.FptId,
                                            //        transaction.TransactionDate, count),
                                            UnitMeasure = detail.UnitMeasure,
                                            UnitPrice = detail.UnitPrice,
                                            TotalQuantity = 0,
                                            VendorId = detail.VendorId,
                                            ImportDate = transaction.TransactionDate,
                                            ImportQuantity = detail.Quantity,
                                        };
                                        if (!string.IsNullOrWhiteSpace(detail.LotNumber)) {
                                            if (detail.LotNumber.Trim().Equals("-B")) {
                                                toolInv.LotNumber =
                                                    MyUtilities.PurchaseOrder.GetFptLotParam(
                                                        (int)MyUtilities.PurchaseOrder.FptLot.Tool, detail.FptId,
                                                        transaction.TransactionDate, count) + "-B";
                                            }
                                            else {
                                                toolInv.LotNumber = detail.LotNumber;
                                            }
                                        }
                                        else {
                                            toolInv.LotNumber =
                                                MyUtilities.PurchaseOrder.GetFptLotParam(
                                                    (int)MyUtilities.PurchaseOrder.FptLot.Tool, detail.FptId,
                                                    transaction.TransactionDate, count);
                                        }
                                        list.Add(toolInv);
                                    }
                                }
                                toolInv.EndDate = null;

                                period.EarlyQuantity = toolInv.TotalQuantity;
                                period.LastQuantity = toolInv.TotalQuantity + detail.Quantity;
                                toolInv.TotalQuantity = period.LastQuantity;

                                period.ToolInventory = toolInv;
                                //period.ToolInvId = toolInv.ToolInvId;
                                detail.LotNumber = toolInv.LotNumber;
                                if (purchaseOrder != null) {
                                    //var poDetails =
                                    //    vfi.PurchaseOrderDetails.Where(
                                    //        pod =>
                                    //        pod.PurchaseOrderId == purchaseOrder.PurchaseOrderId &&
                                    //        pod.ReferenceId == detail.FptId &&
                                    //        Math.Round(pod.UnitPrice * transaction.ExchangeRate - detail.UnitPrice, 0) == 0);
                                    // dm cai cc gi the nay
                                    //var poDetails = purchaseOrder.PurchaseOrderDetails.Where(pod => pod.ReferenceId == detail.FptId);
                                    //if (poDetails.Any()) {
                                    //var poDetail = poDetails.FirstOrDefault(pod => Math.Round(pod.UnitPrice * transaction.ExchangeRate, 0) == detail.UnitPrice);
                                    // sua lan 2
                                    var poDetail = purchaseOrder.PurchaseOrderDetails.FirstOrDefault(pod => pod.PurchaseOrderDetailId == detail.PoDetailId);
                                    if (poDetail != null) {
                                        poDetail.ReceivedQty += period.Quantity;
                                        if (poDetail.ReceivedQty >= poDetail.OrderQty)
                                            poDetail.IsComplete = true;
                                        var tool =
                                            vfi.Tools.FirstOrDefault(m => m.ToolId == poDetail.ReferenceId);
                                        if (tool.UnitPrice < toolInv.UnitPrice)
                                            tool.UnitPrice = toolInv.UnitPrice;
                                    }
                                    else {
                                        // giu lai cai lan 1
                                        var poDetails = purchaseOrder.PurchaseOrderDetails.Where(pod => pod.ReferenceId == detail.FptId);
                                        if (poDetails.Any()) {
                                            poDetail = poDetails.FirstOrDefault(pod => Math.Round(pod.UnitPrice * transaction.ExchangeRate, 0) == detail.UnitPrice);
                                            if (poDetail != null) {
                                                poDetail.ReceivedQty += period.Quantity;
                                                if (poDetail.ReceivedQty >= poDetail.OrderQty)
                                                    poDetail.IsComplete = true;
                                                var tool =
                                                    vfi.Tools.FirstOrDefault(m => m.ToolId == poDetail.ReferenceId);
                                                if (tool.UnitPrice < toolInv.UnitPrice)
                                                    tool.UnitPrice = toolInv.UnitPrice;
                                            }
                                        }
                                    }
                                    //}
                                }
                                vfi.ToolInventories.AddRange(list);
                                vfi.ToolInventoryPeriods.Add(period);
                            }
                            vfi.SaveChanges();
                        }
                        else {// la xuat
                            foreach (var detail in transaction.TransactionFptDetails) {
                                var toolInv =
                                    vfi.ToolInventories.FirstOrDefault(
                                        ti =>
                                        ti.ToolId == detail.FptId && ti.LotNumber.Equals(detail.LotNumber) &&
                                        ti.VendorId == detail.VendorId && ti.UnitPrice == detail.UnitPrice);
                                if (toolInv == null) {
                                    throw new AggregateException("Lỗi! Không tìm thấy tồn kho công cụ");
                                }
                                if (Math.Round(detail.Quantity, 2) > Math.Round(toolInv.TotalQuantity, 2))
                                    throw new AggregateException("Lỗi! Tồn kho công cụ không đủ " +
                                                                 toolInv.Tool.ToolFullCode);
                                var period = new ToolInventoryPeriod {
                                    ToolInventory = toolInv,
                                    ToolInvId = toolInv.ToolInvId,
                                    ToolId = toolInv.ToolId,
                                    TransactionId = transaction.TransactionId,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    EarlyQuantity = Math.Round(toolInv.TotalQuantity, 2),
                                    Quantity = Math.Round(detail.Quantity, 2),
                                    LastQuantity = Math.Round(toolInv.TotalQuantity - detail.Quantity, 2),
                                    PeriodDate = transaction.TransactionDate
                                };
                                toolInv.TotalQuantity = period.LastQuantity;
                                if (toolInv.FirstUseDate == null) {
                                    toolInv.FirstUseDate = transaction.TransactionDate;
                                }
                                if (Math.Round(toolInv.TotalQuantity, 2) == 0) {
                                    toolInv.EndDate = DateTime.Now;
                                }
                                else {
                                    toolInv.EndDate = null;
                                }
                                vfi.ToolInventoryPeriods.Add(period);
                                //if (transaction.Type == (byte)MyUtilities.Tool.ExportType.Production) {

                                //}
                            }
                        }
                        transaction.Status = (byte)MyUtilities.Transaction.Status.Approved;
                        if (purchaseOrder != null) {
                            var allcomplete =
                                purchaseOrder.PurchaseOrderDetails.FirstOrDefault(po => po.IsComplete == false);
                            if (allcomplete == null)
                                purchaseOrder.Status = (byte)MyUtilities.Sales.Status.Completed;
                        }
                        vfi.SaveChanges();
                    }
                //}
            }
            catch (Exception exception) {
                return Json("Lỗi! " + exception.Message);
            }
            return Json("okie");
        }

        [GridAction]
        public ActionResult CancelTransactionTool(long transactionId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CancelTransactionTool",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<TransactionFptModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var import = vfi.TransactionFpts.FirstOrDefault(i => i.TransactionId == transactionId);
                    if (import == null) throw new AggregateException("Lỗi phiếu nhập ! Không tìm thấy phiếu nhập");
                    import.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CancelTransactionTool ", ex.Message);

            }
            return View(new GridModel(GetTransactionToolModel().OrderBy(m => m.TransactionDate)));
        }

        [HttpPost]
        public ActionResult PrintTransactionTool(int transactionId) {
            var model = new List<TransactionFptDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.TransactionFpts.FirstOrDefault(i => i.TransactionId == transactionId);
                    if (transaction == null) throw new AggregateException("Lỗi phiếu nhập ! Không tìm thấy phiếu nhập");
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
                    if (transaction.EoI == Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Import)) {
                        foreach (var detail in transaction.TransactionFptDetails) {
                            var tool = vfi.Tools.FirstOrDefault(f => f.ToolId == detail.FptId);
                            var entity = new TransactionFptDetailModel {
                                LotNumber = detail.LotNumber,
                                Note = detail.Note,
                                Quantity = detail.Quantity,
                                UnitPrice = detail.UnitPrice,
                                ToolCode = tool.ToolCode,
                                ToolName = tool.ToolName,
                                ToolDesignNo = tool.ToolCode + tool.ToolDesignNo + "-" + tool.ToolMaterial,
                                ToolFullCodeName = tool.ToolFullCode,
                                TransactionDate = transaction.TransactionDate,
                                TransactionCode = transaction.TransactionCode,
                                ModifiedDate = transaction.ModifiedDate,
                                ModifiedUser = transaction.ModifiedUser,
                                UnitMeasure = detail.UnitMeasure ?? "",
                                EoI = transaction.EoI,
                                Type = transaction.Type,
                                FptType = transaction.Fpt,
                                PurchasingSignature = transaction.PurchasingSignature,
                                InventorySignature = transaction.InventorySignature,
                                ToolMaterial = tool.ToolMaterial,
                                TotalInv = 0,
                                ToolId = tool.ToolId,
                                FptTypeName = tool.MaterialType.MaterialTypeName,
                                Info = info
                            };
                            if (transaction.PoId != 0 && transaction.PoId != null)
                                entity.PoNumber = transaction.PurchaseOrder.RevisionNumber;
                            if (detail.VendorId > 0) {
                                var vendor = vfi.Vendors.FirstOrDefault(v => v.VendorId == detail.VendorId);
                                entity.VendorCode = vendor.VendorCode;
                                entity.VendorName = vendor.ShortName;
                            }
                            entity.StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status);
                            if (entity.FptType != 0)
                                entity.FptTypeName = MyUtilities.PurchaseOrder.GetFptName(entity.FptType);
                            if (entity.EoI != 0)
                                entity.FptTypeName = MyUtilities.PurchaseOrder.GetEoIName(entity.EoI, entity.Type);
                            entity.Price = entity.UnitPrice * entity.Quantity;
                            var totalInvs =
                                vfi.ToolInventoryPeriods.Where(
                                    ti => ti.ToolId == entity.ToolId &&
                                          ti.PeriodDate <= transaction.TransactionDate);
                            if (totalInvs.Any()) {
                                entity.TotalInv = totalInvs.Sum(ti => ti.LastQuantity - ti.EarlyQuantity);
                            }
                            if (transaction.Status == (byte)MyUtilities.Transaction.Status.Open)
                                entity.TotalInv += entity.Quantity;
                            model.Add(entity);
                        }
                        if (transaction.IsInternal == true) {
                            model.First().TransactionTitle = "PHIẾU NHẬP KHO NỘI BỘ";
                            return PartialView("PagePrintToolTransactionInternal", model);
                        }
                        return PartialView("PagePrintImportTool", model);
                    }
                    else if (transaction.EoI == Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Export)) {
                        var exportDetails = (from x in vfi.ExportToolDetails
                                            where x.ExportTool.TransactionId == transactionId
                                            select new {
                                                x.TransactionDetailId,
                                                x.ToolInventory,
                                                x.ToolInvId,
                                                x.ExportTool.Department,
                                                x.ExportTool.Description,
                                                MachineName = x.MachineId != null ? x.Machine.MachineName : "",
                                                ProductCode = x.ProductId != null ? x.Product.ProductCode : "",
                                            }).ToList();
                        foreach (var detail in transaction.TransactionFptDetails) {
                            //var tool = vfi.Tools.FirstOrDefault(f => f.ToolId == detail.FptId);
                            var tool = vfi.ToolInventories.FirstOrDefault(f => f.ToolId == detail.FptId && f.LotNumber.Equals(detail.LotNumber));
                            var entity = new TransactionFptDetailModel {
                                LotNumber = detail.LotNumber,
                                Note = detail.Note,
                                Quantity = detail.Quantity,
                                UnitPrice = tool.UnitPrice,
                                Price = detail.Quantity * tool.UnitPrice,
                                ToolCode = tool.Tool.ToolCode,
                                ToolName = tool.Tool.ToolName,
                                ToolDesignNo = tool.Tool.ToolDesignNo + "-" + tool.Tool.ToolMaterial,
                                ToolFullCodeName = tool.Tool.ToolFullCode,
                                TransactionDate = transaction.TransactionDate,
                                TransactionCode = transaction.TransactionCode,
                                ModifiedDate = transaction.ModifiedDate,
                                ModifiedUser = transaction.ModifiedUser,
                                UnitMeasure = detail.UnitMeasure ?? "",
                                Status = transaction.Status,
                                StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                                EoI = transaction.EoI,
                                EoIName = MyUtilities.PurchaseOrder.GetEoIName(transaction.EoI, transaction.Type),
                                Type = transaction.Type,
                                TypeName = tool.Tool.MaterialType.MaterialTypeName,
                                FptType = transaction.Fpt,
                                FptTypeName = MyUtilities.PurchaseOrder.GetFptName(transaction.Fpt),
                                PurchasingSignature = transaction.PurchasingSignature,
                                InventorySignature = transaction.InventorySignature,
                                ToolMaterial = tool.Tool.ToolMaterial,
                                Info = info
                            };
                            if (transaction.PoId > 0) {
                                entity.PoNumber = transaction.PurchaseOrder.RevisionNumber;
                            }
                            if (detail.VendorId > 0) {
                                var vendor = vfi.Vendors.FirstOrDefault(v => v.VendorId == detail.VendorId);
                                entity.VendorCode = vendor.VendorCode;
                                entity.VendorName = vendor.ShortName;
                            }
                            var exportDetail = exportDetails.FirstOrDefault(ed => ed.TransactionDetailId == detail.DetailId);
                            if (exportDetail != null) {
                                entity.MachineName = exportDetail.MachineName;
                                entity.ProductCode = exportDetail.ProductCode;
                                entity.Department = exportDetail.Department;
                                entity.Description = exportDetail.Description;
                            }
                            model.Add(entity);
                        }
                        if (transaction.IsInternal == true) {
                            model.First().TransactionTitle = "PHIẾU XUẤT KHO NỘI BỘ";
                            return PartialView("PagePrintToolTransactionInternal", model);
                        }
                        return PartialView("PagePrintExportTool", model);
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PagePrintImportExportTool", ex.Message);
            }
            return PartialView(null);
        }

        public ActionResult CreateRollbackTransaction(long transactionId) {
            var saved = 0;
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.TransactionFpts.FirstOrDefault(x => x.TransactionId == transactionId);
                    if (transaction == null) {
                        return Json(new MyUtilities.Monitor.MyJsonResult(
                            (int)MyUtilities.Monitor.ErrorCode.NotFound,
                            "Không tìm thấy phiếu",
                            0));
                    }
                    if (transaction.Status != (byte)MyUtilities.Transaction.Status.Approved) {
                        return Json(new MyUtilities.Monitor.MyJsonResult(
                            (int)MyUtilities.Monitor.ErrorCode.StatusChanged,
                            "Phiếu chưa duyệt không thể trả phiếu",
                            0));
                    }
                    if (transaction.EoI == (byte)MyUtilities.PurchaseOrder.EoILot.Import) {
                        if (transaction.PoId > 0) {
                            //var taxInvoice = vfi.PoTaxInvoiceReferenceDetails.Where(x=> x.ImportId
                            var isTaxInvoice = transaction.TransactionFptDetails.Any(x => x.PoReferenceDetailId != null);
                            if (isTaxInvoice) {
                                return Json(new MyUtilities.Monitor.MyJsonResult(
                                    (int)MyUtilities.Monitor.ErrorCode.NotFound,
                                    "Phiếu có xuất hóa đơn ! Không thể trả phiếu",
                                    0));
                            }
                        }
                    }
                    else if (transaction.EoI == (byte)MyUtilities.PurchaseOrder.EoILot.Export) { }
                    else { return Json((int)MyUtilities.Monitor.ErrorCode.NotImplement); }

                    var result = RollbackProductInventory(transactionId);
                    if (result.Code != (int)MyUtilities.Monitor.ErrorCode.NoError) {
                        return Json(result);
                    }
                    saved += (int)result.Data;

                    if (transaction.PoId > 0) {
                        result = RollbackPurchasing(transactionId);
                        if (result.Code != (int)MyUtilities.Monitor.ErrorCode.NoError) {
                            return Json(result);
                        }
                    }
                    saved += (int)result.Data;

                    transaction.Status = (byte)MyUtilities.Transaction.Status.Open;
                    saved += vfi.SaveChanges();
                    return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.NoError,
                        "",
                        saved));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult(
                    (int)MyUtilities.Monitor.ErrorCode.Exception,
                    ex.Message,
                    0));
            }
        }

        MyUtilities.Monitor.MyJsonResult RollbackProductInventory(long transactionId) {
            using (var vfi = new tammaContext()) {
                var periods = vfi.ToolInventoryPeriods.Where(x => x.TransactionId == transactionId);
                if (!periods.Any()) {
                    return new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "", 0);
                }
                var invIds = periods.Select(x => x.ToolInvId).Distinct().ToList();
                var invs = vfi.ToolInventories.Where(x => invIds.Contains(x.ToolInvId));
                foreach (var period in periods) {
                    var inv = invs.FirstOrDefault(x => x.ToolInvId == period.ToolInvId);
                    if (period.LastQuantity > period.EarlyQuantity) { // import case
                        if (inv.TotalQuantity < period.Quantity) {
                            return new MyUtilities.Monitor.MyJsonResult(
                                (int)MyUtilities.Monitor.ErrorCode.ReferenceError,
                                "Tồn kho không đủ xử lý",
                                0);
                        }
                        inv.TotalQuantity -= period.Quantity;
                    }
                    else { // export case
                        inv.TotalQuantity += period.Quantity;
                        inv.EndDate = null;
                    }
                }
                vfi.ToolInventoryPeriods.RemoveRange(periods);
                var saved = vfi.SaveChanges();
                return new MyUtilities.Monitor.MyJsonResult(
                    (int)MyUtilities.Monitor.ErrorCode.NoError,
                    "",
                    saved);
            }
        }

        MyUtilities.Monitor.MyJsonResult RollbackPurchasing(long transactionId) {
            using (var vfi = new tammaContext()) {
                var transactionDetails = vfi.TransactionFptDetails.Where(x => x.TransactionId == transactionId);
                foreach (var detail in transactionDetails) {
                    var poDetail = vfi.PurchaseOrderDetails.FirstOrDefault(x => x.PurchaseOrderDetailId == detail.PoDetailId);
                    if (poDetail == null) {
                        new MyUtilities.Monitor.MyJsonResult(
                            (int)MyUtilities.Monitor.ErrorCode.NotFound,
                            "Không tìm thấy phiếu mua",
                            0);                      
                    }
                    poDetail.ReceivedQty -= detail.Quantity;
                    if (poDetail.ReceivedQty < 0) { poDetail.ReceivedQty = 0; }
                    poDetail.IsComplete = false;
                    poDetail.PurchaseOrder.Status = (byte)MyUtilities.Sales.Status.InProcess;
                }
                var saved = vfi.SaveChanges();
                return new MyUtilities.Monitor.MyJsonResult(
                    (int)MyUtilities.Monitor.ErrorCode.NoError,
                    "",
                    saved);
            }
        }

        [GridAction]
        public ActionResult SelectImportTool() {
            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        public ActionResult SelectComboBoxToolInventory() {
            using (var vfi = new tammaContext()) {
                var toolInvs = vfi.ToolInventories.Where(m => m.TotalQuantity > 0);
                var model = toolInvs.Select(m => new ToolInventoryModel {
                    ToolInvId = m.ToolInvId,
                    ToolCode = m.Tool.ToolFullCode,
                    LotNumber = m.LotNumber,
                    VendorCode = m.Vendor.VendorCode
                });
                return new JsonResult {
                    Data = new SelectList(model.ToList(), "ToolInvId", "ToolFullCode")
                };
            }
        }

        public ActionResult SelectComboBoxToolType() {
            using (var vfi = new tammaContext()) {
                var materialTypes = vfi.MaterialTypes.Where(mt => mt.MaterialClassifiedId == 3 && mt.Active);
                var model = materialTypes.Select(m => new ToolModel {
                    MaterialTypeId = m.MaterialTypeId,
                    MaterialTypeName = m.MaterialTypeName
                });
                return new JsonResult {
                    Data = new SelectList(model.ToList(), "MaterialTypeId", "MaterialTypeName")
                };
            }
        }

        public ActionResult SelectComboBoxToolIdentity() {
            using (var vfi = new tammaContext()) {
                var materialTypes = vfi.MaterialTypes.Where(mt => mt.MaterialClassifiedId == 3 && mt.Active);
                var model = materialTypes.Select(mt => mt.IdentityCode).Distinct().ToList();
                return new JsonResult {
                    Data = new SelectList(model)
                };
            }
        }

        public ActionResult SelectComboBoxDepartment() {
            using (var vfi = new tammaContext()) {
                var toolInvs = vfi.Departments.Where(mt => mt.Active);
                var model = toolInvs.Select(m => new DepartmentModel() {
                    DepartmentId = m.DepartmentId,
                    DepartmentName = m.DepartmentName
                });
                return new JsonResult {
                    Data = new SelectList(model.ToList(), "DepartmentId", "DepartmentName")
                };
            }
        }
        public ActionResult SelectComboBoxTool() {
            using (var vfi = new tammaContext()) {
                var tools = vfi.Tools.Where(t => t.Active).OrderBy(x => x.ToolFullCode);
                return new JsonResult {
                    Data = new SelectList(tools.ToList(), "ToolId", "ToolFullCode")
                };
            }
        }

        public ActionResult SelectComboBoxToolName() {
            using (var vfi = new tammaContext()) {
                var tools = vfi.Tools.Where(t => t.Active)
                                    .OrderBy(x => x.ToolName);
                return new JsonResult {
                    Data = new SelectList(tools.ToList(), "ToolId", "ToolName")
                };
            }
        }

        public ActionResult SelectComboBoxProductionToolById(int productId) {
            var model = new List<ProductionToolModel>();
            using (var vfi = new tammaContext()) {
                var productionTools = vfi.ProductionTools.Where(pt => pt.ProductId == productId && pt.Active);
                foreach (var productionTool in productionTools) {
                    var entity = new ProductionToolModel {
                        RealToolId = productionTool.RealToolId,
                        ToolFullCode = productionTool.Tool.ToolFullCode,
                    };
                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "RealToolId", "ToolFullCode")
            };
        }

        [HttpPost]
        public ActionResult GetProductOnMachine(int machineId) {
            try {
                using (var vfi = new tammaContext()) {
                    var products = vfi.Products.Where(f => f.Active)
                                               .Select(f => new {
                                                   f.ProductId,
                                                   f.ProductCode
                                               })
                                               .ToList();         // select producId, product code o giua where voi tolist

                    
                    var approvedStatus = (byte)MyUtilities.Transaction.Status.Approved;

                    var productId = vfi.TrackUpMachines             //lay productId cua lastrack o code cu.
                                     .Where(t => t.MachineId == machineId
                                            && t.Status == approvedStatus
                                            && t.Product.Active
                                            && t.DeliveryDate != null)
                                     .GroupBy(t => t.MachineId)
                                     .Select(g => g.OrderByDescending(t => t.DeliveryDate).FirstOrDefault())
                                     .Select(t => t.ProductId)
                                     .Distinct()
                                     .FirstOrDefault();
                                     


                    return new JsonResult {
                        Data =
                            new SelectList(products, "ProductId", "ProductCode",            // load products combobox
                                            productId == null ? -1 : productId),                   // loc product co' productId trong combobox o tren)
                    };
                }
            }
            catch (Exception) {
            }
            return Json("0");
        }


        public ActionResult GetToolInvTotal(int toolInvId) {
            try {
                //var intRevisionNumber = Convert.ToInt32(revisionNumber);
                using (var vfi = new tammaContext()) {
                    var toolInv =
                        vfi.ToolInventories.FirstOrDefault(mi => mi.ToolInvId == toolInvId);
                    return Json(toolInv != null ? toolInv.TotalQuantity : 0);
                }
            }
            catch (FormatException) {
                return Json(0);
            }
        }

        [GridAction]
        public ActionResult SelectExportToolInv(string toolCode, int vendorId, int typeId, int type, int productId) {
            var model = new List<AssignToolModel>();
            try {
                using (var vfi = new tammaContext()) {
                    if (string.IsNullOrWhiteSpace(toolCode) && vendorId == 0 && typeId == 0 && productId == 0)
                        return View(new GridModel(model));

                    var nullCode = string.IsNullOrWhiteSpace(toolCode);
                    var toolInvs = (from ti in vfi.ToolInventories
                                    where ti.Tool.Active
                                    && (productId == 0 || ti.Tool.ProductionTools.Any(pt => pt.ProductId == productId && pt.Active))
                                    && (typeId == 0 || ti.Tool.MaterialTypeId == typeId)
                                    && (nullCode || ti.Tool.ToolFullCode.Contains(toolCode))
                                    && ti.TotalQuantity > 0
                                    && (vendorId == 0 || ti.VendorId == vendorId)
                                    && (type == 1 ? ti.UnitPrice > 0 : type == 2 ? ti.UnitPrice == 0 : true)
                                    //&& ti.ToolId == 5769
                                    select
                                    new {
                                        ti.ToolId,
                                        ti.ToolInvId,
                                        ti.TotalQuantity,
                                        ti.LotNumber,
                                        ToolCode = ti.Tool.ToolCode,
                                        ToolName = ti.Tool.ToolName,
                                        ToolDesignNo = ti.Tool.ToolDesignNo,
                                        ToolFullCode = ti.Tool.ToolFullCode,
                                        ti.VendorId,
                                        VendorCode = ti.Vendor.VendorCode,
                                        VendorName = ti.Vendor.VendorName,
                                        ti.UnitMeasure,
                                        ti.NG,
                                        ti.Lock,

                                    }
                                    //ti
                                    ).ToList();
                    var toolIds = toolInvs.Select(x => x.ToolId).Distinct();
                    //var toolIds = new List<int>();
                    //if (productId != 0) {
                    //    toolIds =
                    //        vfi.ProductionTools.Where(
                    //            pt =>
                    //            pt.ProductId == productId && pt.Active
                    //        //&& (nullCode || pt.Tool.ToolFullCode.Contains(toolCode))
                    //            )
                    //           .Select(pt => pt.ToolId)
                    //           .ToList();
                    //}
                    //else {
                    //    toolIds =
                    //        vfi.Tools.Where(
                    //            t =>
                    //            t.Active && (typeId == 0 || t.MaterialTypeId == typeId) &&
                    //            (nullCode || t.ToolFullCode.Contains(toolCode)))
                    //           .Select(t => t.ToolId)
                    //           .ToList();
                    //}
                    //foreach (var toolId in toolIds) {
                    //        var tool = vfi.Tools.FirstOrDefault(t => t.ToolId == toolId);
                    //        var toolInvs = vfi.ToolInventories.Where(fi => fi.ToolId == tool.ToolId && fi.TotalQuantity > 0);
                    //        if (vendorId != 0)
                    //            toolInvs = toolInvs.Where(fi => fi.VendorId == vendorId);
                    //        if (type == 1) {
                    //            toolInvs = toolInvs.Where(ti => ti.UnitPrice > 0);
                    //        }
                    //        else if (type == 2) {
                    //            toolInvs = toolInvs.Where(ti => ti.UnitPrice == 0);
                    //        }
                    //        if (toolInvs.Any()) {
                    var openTransactions = (from td in vfi.TransactionFptDetails
                                            where td.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                            td.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                            td.TransactionFpt.EoI == (byte)MyUtilities.PurchaseOrder.EoILot.Export &&
                                           toolIds.Contains(td.FptId)
                                            select
                                            new {
                                                td.FptId,
                                                td.LotNumber,
                                                td.Quantity,
                                            }
                                            //td
                                            ).ToList();
                                foreach (var toolInv in toolInvs) {
                                    var entity = new AssignToolModel {
                                        ToolId = toolInv.ToolId,
                                        TotalInv = toolInv.TotalQuantity,
                                        ToolCode = toolInv.ToolCode,
                                        ToolName = toolInv.ToolName,
                                        ToolDesignNo = toolInv.ToolDesignNo,
                                        ToolFullCodeName = toolInv.ToolFullCode,
                                        LotNumber = toolInv.LotNumber,
                                        ToolInvId = toolInv.ToolInvId,
                                        VendorId = toolInv.VendorId,
                                        VendorCode = toolInv.VendorCode,
                                        VendorName = toolInv.VendorName,
                                        UnitMeasure = toolInv.UnitMeasure,
                                        AvailInv = toolInv.TotalQuantity,
                                        NG = toolInv.NG,
                                        Lock = toolInv.Lock,
                                        
                                    };
                                    //var checkList = vfi.TransactionFptDetails
                                    //    .Where(t => t.LotNumber == entity.LotNumber && t.FptId == entity.ToolId)
                                    //    .Select(t => t.PoDetailId).FirstOrDefault();
                                    //var poNumber = vfi.PurchaseOrderDetails
                                    //    .Where(t => t.PurchaseOrderDetailId == checkList)
                                    //    .Select(t => new {
                                    //    PoNumber = t.PurchaseOrder.RevisionNumber,
                                    //    Importdate = t.PurchaseOrder.ShipDate,
                                    //}).ToList();
                                    var openTransactionsById = openTransactions
                                        .Where(ot => ot.FptId == toolInv.ToolId && ot.LotNumber.Equals(toolInv.LotNumber));
                                    if (openTransactionsById.Any()) {
                                        entity.AvailInv = entity.TotalInv - openTransactionsById.Sum(ot => ot.Quantity);
                                    }
                                    if (entity.AvailInv <= 0) continue;
                                    model.Add(entity);
                                }
                            }
                        }

                    //}
            //    }
            //}
            catch (Exception ex) {
                ModelState.AddModelError("SelectExportToolInv", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.ToolCode)
                .ThenBy(m => m.ToolFullCodeName)
                .ThenBy(m => m.VendorCode)
                .ThenBy(m => m.LotNumber)));
        }

        [GridAction]
        public ActionResult SelectExportToolInventory(string invIds, int machineId, int productId) {
            if (string.IsNullOrWhiteSpace(invIds))
                return View(new GridModel(new List<TransactionFptDetailModel>()));

            //int[] checkedRecords;
            List<int> checkedRecords;
            try {
                var lst = invIds.Split(':');
                checkedRecords = new List<int>();
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords.Add(Convert.ToInt32(lst.ElementAt(i)));
                }
            }
            catch (FormatException) {
                return View(new GridModel(new List<TransactionFptDetailModel>()));
            }
            var lstIds = (List<int>)Session["ListExportToolInvIds"];
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
            var model = new List<TransactionFptDetailModel>();
            lstIds = lstIds.Distinct().ToList();
            using (var vfi = new tammaContext()) {
                try {
                    var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == machineId);
                    var product = vfi.Products.FirstOrDefault(m => m.ProductId == productId);
                    foreach (var id in lstIds) {
                        var toolInv = vfi.ToolInventories.FirstOrDefault(mi => mi.ToolInvId == id);

                        if ((toolInv.TotalQuantity) == 0) continue;
                        var entity = new TransactionFptDetailModel {
                            ToolId = toolInv.ToolId,
                            TotalInv = toolInv.TotalQuantity,
                            ToolCode = toolInv.Tool.ToolCode,
                            ToolName = toolInv.Tool.ToolName,
                            ToolDesignNo = toolInv.Tool.ToolDesignNo,
                            ToolFullCodeName = toolInv.Tool.ToolFullCode,
                            LotNumber = toolInv.LotNumber,
                            ToolInvId = toolInv.ToolInvId,
                            VendorId = toolInv.VendorId,
                            VendorCode = toolInv.Vendor.VendorCode,
                            VendorName = toolInv.Vendor.VendorName,
                            UnitMeasure = toolInv.UnitMeasure,
                            AvailInv = toolInv.TotalQuantity,
                            NG = toolInv.NG,
                            Lock = toolInv.Lock,
                        };
                        if (toolInv.ImportDate != null)
                            entity.TaxInvoiceDate = toolInv.ImportDate.Value;
                        if (machine != null) {
                            entity.MachineId = machine.MachineId;
                            entity.MachineName = machine.MachineName;
                        }
                        if (product != null) {
                            entity.ProductId = product.ProductId;
                            entity.ProductCode = product.ProductCode;
                        }

                        var openTransactions = from td in vfi.TransactionFptDetails
                                               where td.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                               td.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                             td.FptId == entity.ToolId && td.LotNumber.Equals(entity.LotNumber)
                                               select td;
                        if (openTransactions.Any()) {
                            entity.AvailInv = entity.TotalInv - openTransactions.Sum(ot => ot.Quantity);
                        }
                        model.Add(entity);

                    }
                    Session["ListExportToolInvIds"] = lstIds;
                    return View(new GridModel(model));
                }
                catch (Exception ex) {
                    throw ex;
                }
            }                   

        }

        [GridAction]
        public ActionResult SelectAssignToolProduction(int machineId, int productId) {
            if (productId == 0)
                return View(new GridModel(new List<TransactionFptDetailModel>()));
            var model = new List<TransactionFptDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == machineId && m.Active);
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId && p.Active);
                    var productionTools = vfi.ProductionTools.Where(pt => pt.ProductId == productId && pt.Active)
                        .OrderBy(pt => pt.ToolIndex);
                    var track =
                        vfi.TrackUpMachines.Where(
                                tm => tm.ProductId == productId && tm.MachineId == machineId && tm.Status == 2)
                            .OrderByDescending(m => m.DeliveryDate).FirstOrDefault();
                    var trackId = track != null ? track.TrackId : 0;
                    var toolIds = productionTools.Select(ti => ti.ToolId).Distinct().ToList();
                    var openTransactions = from td in vfi.TransactionFptDetails
                                           where td.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                           td.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                          toolIds.Contains(td.FptId)
                                           select td;
                    foreach (var productionTool in productionTools) {
                        var toolId = productionTool.ToolId;                             //replaceTool != null ? replaceTool.ToolId :
                        var tool = vfi.Tools.FirstOrDefault(t => t.ToolId == toolId);
                        if (tool == null) continue;
                        var toolInvs =
                            vfi.ToolInventories.Where(ti => ti.ToolId == toolId && ti.TotalQuantity > 0)
                            .OrderBy(ti => ti.LotNumber);
                        foreach (var toolInv in toolInvs) {
                            var entity = new TransactionFptDetailModel {
                                ToolId = toolId,
                                TotalInv = toolInv.TotalQuantity,
                                ToolCode = tool.ToolCode,
                                ToolName = tool.ToolName,
                                ToolDesignNo = tool.ToolDesignNo,
                                ToolFullCodeName = tool.ToolFullCode,
                                LotNumber = toolInv.LotNumber,
                                ToolInvId = toolInv.ToolInvId,
                                VendorId = toolInv.VendorId,
                                VendorCode = toolInv.Vendor.VendorCode,
                                VendorName = toolInv.Vendor.VendorName,
                                UnitMeasure = toolInv.UnitMeasure,
                                FptTypeName = tool.MaterialType.MaterialTypeName,   
                                StatusName =
                                    productionTool.ToolIndex +
                                    ".QCSD: " + productionTool.Description +
                                    " - Vị trí:" + productionTool.ToolLocation + " - " +
                                    productionTool.Note,
                                AvailInv = toolInv.TotalQuantity,
                                ProductionToolId = productionTool.RealToolId,                        // 20/01/2026
                                NG = toolInv.NG,
                                Lock = toolInv.Lock,
                            };
                            // Kiếm phiếu mua lô Công cụ
                            //var poDetailId = vfi.TransactionFptDetails.Where(t => t.FptId == entity.ToolId && t.LotNumber == entity.LotNumber && t.PoDetailId != null).Select(t => t.PoDetailId).ToList();
                            //var poNumber = vfi.PurchaseOrderDetails.Where(t => poDetailId.Contains(t.PurchaseOrderDetailId)).Select(t => new {
                            //                                                                                                        ReivisionNumber = t.PurchaseOrder.RevisionNumber,
                            //                                                                                                        ImportDate = t.PurchaseOrder.ShipDate,
                            //                                                                                                        }).ToList();
                            if (toolInv.ImportDate != null)
                                entity.TaxInvoiceDate = toolInv.ImportDate.Value;
                            if (machine != null) {
                                entity.MachineId = machine.MachineId;
                                entity.MachineName = machine.MachineName;
                            }
                            if (product != null) {
                                entity.ProductId = product.ProductId;
                                entity.ProductCode = product.ProductCode;
                            }
                            var openTransactionsById = openTransactions.Where(ot => ot.FptId == entity.ToolId && ot.LotNumber.Equals(entity.LotNumber));
                            if (openTransactionsById.Any()) {
                                entity.AvailInv -= openTransactionsById.Sum(ot => ot.Quantity);
                            }
                            //if (replaceTool != null)
                            //    entity.Note = replaceTool.Note;
                            model.Add(entity);
                        }
                        if (!toolInvs.Any()) {
                            var entity = new TransactionFptDetailModel {
                                ToolId = toolId,
                                TotalInv = 0,
                                ToolCode = tool.ToolCode,
                                ToolName = tool.ToolName,
                                ToolDesignNo = tool.ToolDesignNo,
                                ToolFullCodeName = tool.ToolFullCode,
                                LotNumber = "",
                                ToolInvId = 0,
                                VendorId = 0,
                                VendorCode = "",
                                VendorName = "",
                                UnitMeasure = "",
                                FptTypeName = tool.MaterialType.MaterialTypeName,
                                StatusName =
                                    productionTool.ToolIndex +
                                    ".QCSD: " + productionTool.Description +
                                    " - Vị trí:" + productionTool.ToolLocation + " - " +
                                    productionTool.Note,
                                AvailInv = 0,
                                ProductionToolId = productionTool.RealToolId                        //20/01/2026

                            };
                            if (machine != null) {
                                entity.MachineId = machine.MachineId;
                                entity.MachineName = machine.MachineName;
                            }
                            if (product != null) {
                                entity.ProductId = product.ProductId;
                                entity.ProductCode = product.ProductCode;
                            }
                            //if (replaceTool != null)
                            //    entity.Note = replaceTool.Note;
                            model.Add(entity);
                        }

                    }

                    var replaceTools = vfi.ProductionToolReplacements.Where(pt => pt.ToolId != null &&
                        (pt.TrackId == trackId ||
                        (pt.TrackUpMachine.MachineId == track.MachineId &&
                        pt.TrackUpMachine.ProductId == track.ProductId)));

                    toolIds = replaceTools.Select(ti => ti.ToolId.Value).Distinct().ToList();
                    openTransactions = from td in vfi.TransactionFptDetails
                                       where td.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                            td.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                            toolIds.Contains(td.FptId)
                                       select td;
                    foreach (var replaceTool in replaceTools) {
                        var toolId = //replaceTool != null ? replaceTool.ToolId : 
                             replaceTool.ToolId.Value;
                        var tool = vfi.Tools.FirstOrDefault(t => t.ToolId == toolId);
                        if (tool == null) continue;
                        var toolInvs =
                            vfi.ToolInventories.Where(ti => ti.ToolId == toolId && ti.TotalQuantity > 0)
                            .OrderBy(ti => ti.LotNumber);
                        foreach (var toolInv in toolInvs) {
                            var entity = new TransactionFptDetailModel {
                                ToolId = toolId,
                                TotalInv = toolInv.TotalQuantity,
                                ToolCode = tool.ToolCode,
                                ToolName = tool.ToolName,
                                ToolDesignNo = tool.ToolDesignNo,
                                ToolFullCodeName = tool.ToolFullCode,
                                LotNumber = toolInv.LotNumber,
                                ToolInvId = toolInv.ToolInvId,
                                VendorId = toolInv.VendorId,
                                VendorCode = toolInv.Vendor.VendorCode,
                                VendorName = toolInv.Vendor.VendorName,
                                UnitMeasure = toolInv.UnitMeasure,
                                FptTypeName = tool.MaterialType.MaterialTypeName,
                                //StatusName = "CCTT: " ,
                                AvailInv = toolInv.TotalQuantity,
                                ProductionToolId = replaceTool.ProductionToolId                            // 21/01/2026

                            };
                            var note = productionTools.Where(pt => pt.RealToolId == entity.ProductionToolId).FirstOrDefault();
                            entity.StatusName = "CCTT - Vị trí: " + note.ToolLocation;
                            if (machine != null) {
                                entity.MachineId = machine.MachineId;
                                entity.MachineName = machine.MachineName;
                            }
                            if (product != null) {
                                entity.ProductId = product.ProductId;
                                entity.ProductCode = product.ProductCode;
                            }
                            var openTransactionsById = openTransactions.Where(ot => ot.FptId == entity.FptId && ot.LotNumber.Equals(entity.LotNumber));
                            if (openTransactionsById.Any()) {
                                entity.AvailInv -= openTransactionsById.Sum(ot => ot.Quantity);
                            }
                            //if (replaceTool != null)
                            //    entity.Note = replaceTool.Note;
                            model.Add(entity);
                        }
                        if (!toolInvs.Any()) {
                            var entity = new TransactionFptDetailModel {
                                ToolId = toolId,
                                TotalInv = 0,
                                ToolCode = tool.ToolCode,
                                ToolName = tool.ToolName,
                                ToolDesignNo = tool.ToolDesignNo,
                                ToolFullCodeName = tool.ToolFullCode,
                                LotNumber = "",
                                ToolInvId = 0,
                                VendorId = 0,
                                VendorCode = "",
                                VendorName = "",
                                UnitMeasure = "",
                                FptTypeName = tool.MaterialType.MaterialTypeName,
                                //StatusName = "CCTT",
                                AvailInv = 0,
                                ProductionToolId = replaceTool.ProductionToolId                            // 21/01/2026

                            };

                            var note = productionTools.Where(pt => pt.RealToolId == entity.ProductionToolId).FirstOrDefault();
                            entity.StatusName = "CCTT - Vị trí: " + note.ToolLocation;
                            if (machine != null) {
                                entity.MachineId = machine.MachineId;
                                entity.MachineName = machine.MachineName;
                            }
                            if (product != null) {
                                entity.ProductId = product.ProductId;
                                entity.ProductCode = product.ProductCode;
                            }
                            //if (replaceTool != null)
                            //    entity.Note = replaceTool.Note;
                            model.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectAssignToolProduction", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult UpdateAssignToolProduction_2(
            [Bind(Prefix = "inserted")] IEnumerable<TransactionFptDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<TransactionFptDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<TransactionFptDetailModel> deletedDetails,
            string exportDate, int machineId, int productId, string description) {
            try {
                if (!Request.IsAuthenticated)
                    throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                updatedDetails = updatedDetails.Where(ud => ud.Quantity > 0 || ud.MachineUse > 0 || ud.MachineReturn > 0);
                if (!updatedDetails.Any())
                    throw new AggregateException(@"Lỗi không có số lượng.");
                var modifiedUser = HttpContext.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(modifiedUser))
                    throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
                if (machineId == 0 || productId == 0)
                    throw new AggregateException("Lỗi! Vui lòng chọn máy và sản phẩm cần phát !");
                var ci = new CultureInfo("vi-VN");
                var date = string.IsNullOrWhiteSpace(exportDate)
                               ? DateTime.Today
                               : Convert.ToDateTime(exportDate, ci);
                if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                    throw new AggregateException(
                        @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                }

                using (var vfi = new tammaContext()) {

                    var assignUpdates = updatedDetails.Where(ud => ud.Quantity > 0);
                    var transaction = new TransactionFpt {
                        TransactionDate = date,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        PoId = null,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        TransactionFptDetails = new List<TransactionFptDetail>(),
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Tool, 1),
                        EoI = Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Export),
                        Type = (byte)(MyUtilities.Tool.ExportType.Production),
                        Fpt = Convert.ToInt16(MyUtilities.PurchaseOrder.FptLot.Tool),
                        AccountantSignature = 0,
                        InventorySignature = 0,
                        PurchasingSignature = 0,
                        QcSignature = 0,
                        ExchangeRate = 1,
                    };

                                                                            
                    var exportTool = new ExportTool {
                        TransactionFpt = transaction,
                        TransactionId = transaction.TransactionId,

                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ExportDate = date,
                        Description = description,
                        Department = "SX1",
                        MachineId = machineId,
                        ProductId = productId
                    };
                    if (!assignUpdates.Any())
                        goto part2;
                    
                    foreach (var detail in assignUpdates) {
                        if (detail.Quantity == 0 || detail.ToolInvId == 0) continue;
                        var toolInv = vfi.ToolInventories.FirstOrDefault(m => m.ToolInvId == detail.ToolInvId);
                        if (toolInv == null) continue;
                        if (detail.Quantity > toolInv.TotalQuantity)
                            throw new AggregateException("Lỗi! Lô công cụ " + detail.FuelFullCodeName + " " +
                                                         toolInv.LotNumber + " xuất vượt quá tồn kho");
                        if (toolInv.ImportDate != null && toolInv.ImportDate > transaction.TransactionDate)
                            throw new AggregateException("Lỗi! Lô công cụ " + detail.FuelFullCodeName + " " +
                                                         toolInv.LotNumber + " xuất sai thời gian! (nhập kho: "
                                                         + toolInv.ImportDate.Value.ToString("dd/MM/yyyy"));
                        var transactionDetail = new TransactionFptDetail {
                            FptId = toolInv.ToolId,
                            Quantity = detail.Quantity,
                            UnitPrice = toolInv.UnitPrice,
                            LotNumber = toolInv.LotNumber,
                            Note = detail.Note,
                            UnitMeasure = toolInv.UnitMeasure,
                            TransactionFpt = transaction,
                            TransactionId = transaction.TransactionId,
                            VendorId = toolInv.VendorId,
                            MachineId = exportTool.MachineId
                        };
                        transaction.TransactionFptDetails.Add(transactionDetail);

                        var exportDetail = new ExportToolDetail {
                            Quantity = detail.Quantity,
                            ToolInvId = toolInv.ToolInvId,
                            ExportId = exportTool.ExportId,
                            ExportTool = exportTool,
                            TransactionFptDetail = transactionDetail,
                            TransactionDetailId = transactionDetail.DetailId,
                            ProductId = exportTool.ProductId,
                            MachineId = exportTool.MachineId,
                            
                        };
                        exportTool.ExportToolDetails.Add(exportDetail);
                    }
                    if (transaction.TransactionFptDetails.Any()) {
                        vfi.TransactionFpts.Add(transaction);
                        vfi.ExportTools.Add(exportTool);
                    }

                part2: {
                        assignUpdates = updatedDetails.Where(ud => ud.MachineUse > 0);
                        if (!assignUpdates.Any())
                            goto part4;
                        var toolUse = new ToolUse {
                            Department = exportTool.Department,
                            ModifiedDate = exportTool.ModifiedDate.Value,
                            ModifiedUser = exportTool.ModifiedUser,
                            UsedCode = transaction.TransactionCode,
                            Status = transaction.Status,
                            UsedDate = transaction.TransactionDate,
                            Type = (byte)MyUtilities.Tool.ExportType.Production,
                        };

                        foreach (var detail in assignUpdates) {
                            if (detail.MachineUse == 0 || detail.ToolInvId == 0) continue;
                            var toolInv = vfi.ToolInvOnMachines.FirstOrDefault(m => m.ToolInvId == detail.ToolInvId &&
                                m.MachineId == machineId);
                            if (toolInv == null) continue;
                            if (detail.MachineUse > toolInv.TotalQuantity)
                                throw new AggregateException("Lỗi! Lô công cụ " + detail.FuelFullCodeName + " " +
                                                             detail.LotNumber + " xuất vượt quá tồn kho trên máy");

                            var useDetail = new ToolUseDetail {
                                ToolInvMachineId = toolInv.ToolInvMachineId,
                                Quantity = detail.MachineUse,
                                Note = detail.Note,
                            };
                            toolUse.ToolUseDetails.Add(useDetail);
                        }
                        if (toolUse.ToolUseDetails.Any()) {
                            vfi.ToolUses.Add(toolUse);
                        }
                    }
                part3: {

                        assignUpdates = updatedDetails.Where(ud => ud.MachineReturn > 0);
                        if (!assignUpdates.Any())
                            goto part4;
                        var toolReturn = new ToolUse {
                            Department = exportTool.Department,
                            ModifiedDate = exportTool.ModifiedDate.Value,
                            ModifiedUser = exportTool.ModifiedUser,
                            UsedCode = transaction.TransactionCode,
                            Status = transaction.Status,
                            UsedDate = transaction.TransactionDate,
                            Type = (byte)MyUtilities.Tool.ExportType.Return,
                        };

                        foreach (var detail in assignUpdates) {
                            if (detail.MachineReturn == 0 || detail.ToolInvId == 0) continue;
                            var toolInv = vfi.ToolInvOnMachines.FirstOrDefault(m => m.ToolInvId == detail.ToolInvId &&
                                m.MachineId == machineId);
                            if (toolInv == null) continue;
                            if (detail.MachineReturn > toolInv.TotalQuantity)
                                throw new AggregateException("Lỗi! Lô công cụ " + detail.FuelFullCodeName + " " +
                                                             detail.LotNumber + " xuất vượt quá tồn kho trên máy");

                            var returnDetail = new ToolUseDetail {
                                ToolInvMachineId = toolInv.ToolInvMachineId,
                                Quantity = detail.MachineReturn,
                                Note = detail.Note,
                            };
                            toolReturn.ToolUseDetails.Add(returnDetail);
                        }
                        if (toolReturn.ToolUseDetails.Any()) {
                            vfi.ToolUses.Add(toolReturn);
                        }
                    }
                part4: {
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateAssignToolProduction", "" + exception.Message);
            }

            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        [GridAction]
        public ActionResult UpdateAssignToolProduction(
            [Bind(Prefix = "inserted")] IEnumerable<TransactionFptDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<TransactionFptDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<TransactionFptDetailModel> deletedDetails,
            string exportDate, int machineId, int productId, string department, string description) {
            if (updatedDetails != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var modifiedUser = HttpContext.User.Identity.Name;
                    if (string.IsNullOrWhiteSpace(modifiedUser))
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
                    if (machineId == 0 || productId == 0)
                        throw new AggregateException("Lỗi! Vui lòng chọn máy và sản phẩm cần phát !");
                    var date = MyUtilities.Function.ParseDate(exportDate);
                    if (date > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    updatedDetails = updatedDetails.Where(ud => ud.Quantity != 0 && ud.ToolInvId != 0);
                    if (!updatedDetails.Any()) {
                        throw new AggregateException("Cập nhật không thành công !");
                    }


                    using (var vfi = new tammaContext()) {

                        var transaction = new TransactionFpt {
                            TransactionDate = date,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            PoId = null,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            TransactionFptDetails = new List<TransactionFptDetail>(),
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Tool, 1),
                            EoI = Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Export),
                            Type = (byte)(MyUtilities.Tool.ExportType.Production),
                            Fpt = Convert.ToInt16(MyUtilities.PurchaseOrder.FptLot.Tool),
                            AccountantSignature = 0,
                            InventorySignature = 0,
                            PurchasingSignature = 0,
                            QcSignature = 0,
                            ExchangeRate = 1,
                        };
                        var exportTool = new ExportTool {
                            TransactionFpt = transaction,
                            TransactionId = transaction.TransactionId,

                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ExportDate = date,
                            Description = description,
                            Department = department,
                            MachineId = machineId,
                            ProductId = productId
                        };
                        //
                        var toolInvIds = updatedDetails.Select(ud => ud.ToolInvId).Distinct().ToList();
                        foreach (var toolInvId in toolInvIds) {
                            var details = updatedDetails.Where(ud => ud.ToolInvId == toolInvId);
                            if (!details.Any()) continue;
                            var toolInv = vfi.ToolInventories.FirstOrDefault(m => m.ToolInvId == toolInvId);
                            if (toolInv == null) continue;
                            var quantity = toolInv.TotalQuantity - details.Sum(d => d.Quantity);
                            if (quantity < 0)
                                throw new AggregateException("Lỗi! Lô công cụ " + toolInv.Tool.ToolFullCode + " " +
                                                             toolInv.LotNumber + " xuất vượt quá tồn kho");
                            var openTransactions = from td in vfi.TransactionFptDetails
                                                   where td.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                                   td.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                                 td.FptId == toolInv.ToolId && td.LotNumber.Equals(toolInv.LotNumber)
                                                   select td;
                            if (openTransactions.Any()) {
                                quantity -= openTransactions.Sum(ot => ot.Quantity);
                                if (quantity < 0)
                                    throw new AggregateException("Lỗi! Lô công cụ " + toolInv.Tool.ToolFullCode + " " +
                                                                 toolInv.LotNumber + " xuất vượt quá tồn kho còn lại");
                            }
                            if (toolInv.ImportDate != null && toolInv.ImportDate > transaction.TransactionDate)
                                throw new AggregateException("Lỗi! Lô công cụ " + toolInv.Tool.ToolFullCode + " " +
                                                             toolInv.LotNumber + " xuất sai thời gian! (nhập kho: "
                                                             + toolInv.ImportDate.Value.ToString("dd/MM/yyyy"));
                        }
                        foreach (var detail in updatedDetails) {
                            //if (detail.Quantity == 0 || detail.ToolInvId == 0) continue;
                            var toolInv = vfi.ToolInventories.FirstOrDefault(m => m.ToolInvId == detail.ToolInvId);
                            if (toolInv == null) continue;
                            //var quantity = toolInv.TotalQuantity - detail.Quantity;
                            //if (quantity < 0)
                            //    throw new AggregateException("Lỗi! Lô công cụ " + detail.FuelFullCodeName + " " +
                            //                                 toolInv.LotNumber + " xuất vượt quá tồn kho");

                            //var openTransactions = from td in vfi.TransactionFptDetails
                            //                       where td.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Open &&
                            //                       td.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool &&
                            //                     td.FptId == entity.ToolId && td.LotNumber.Equals(entity.LotNumber)
                            //                       select td;
                            //if (openTransactions.Any()) {
                            //    quantity -= openTransactions.Sum(ot => ot.Quantity);
                            //    if (quantity < 0)
                            //        throw new AggregateException("Lỗi! Lô công cụ " + detail.FuelFullCodeName + " " +
                            //                                     toolInv.LotNumber + " xuất vượt quá tồn kho còn lại");
                            //}
                            //if (toolInv.ImportDate != null && toolInv.ImportDate > transaction.TransactionDate)
                            //    throw new AggregateException("Lỗi! Lô công cụ " + detail.FuelFullCodeName + " " +
                            //                                 toolInv.LotNumber + " xuất sai thời gian! (nhập kho: "
                            //                                 + toolInv.ImportDate.Value.ToString("dd/MM/yyyy"));
                            var transactionDetail = new TransactionFptDetail {
                                FptId = toolInv.ToolId,
                                Quantity = detail.Quantity,
                                UnitPrice = toolInv.UnitPrice,
                                LotNumber = toolInv.LotNumber,
                                Note = detail.Note,
                                UnitMeasure = toolInv.UnitMeasure,
                                TransactionFpt = transaction,
                                TransactionId = transaction.TransactionId,
                                VendorId = toolInv.VendorId,
                                MachineId = exportTool.MachineId,
                                NG = toolInv.NG,
                                Lock= toolInv.Lock,
                            };
                            if (transactionDetail.Lock == true) {
                                throw new AggregateException("Lô công cụ này đã bị khóa do NG, không thể phát ra máy! Vui lòng chọn lô khác hoặc liên hệ quản lý để xử lý.");
                            };
                            transaction.TransactionFptDetails.Add(transactionDetail);

                            var exportDetail = new ExportToolDetail {
                                Quantity = detail.Quantity,
                                ToolInvId = toolInv.ToolInvId,
                                ExportId = exportTool.ExportId,
                                ExportTool = exportTool,
                                TransactionFptDetail = transactionDetail,
                                TransactionDetailId = transactionDetail.DetailId,
                                ProductId = exportTool.ProductId,
                                MachineId = exportTool.MachineId,
                                RealToolId = detail.ProductionToolId,                                //20/01/20026
                            };
                            exportTool.ExportToolDetails.Add(exportDetail);
                        }
                        if (transaction.TransactionFptDetails.Any()) {
                            vfi.TransactionFpts.Add(transaction);
                            vfi.ExportTools.Add(exportTool);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("UpdateAssignToolProduction", "" + exception.Message);
                }
            }
            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }
        [GridAction]
        public ActionResult UpdateAssignTool2(
            [Bind(Prefix = "inserted")] IEnumerable<TransactionFptDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<TransactionFptDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<TransactionFptDetailModel> deletedDetails,
            string exportDate, string department, string description) {
            if (updatedDetails != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var modifiedUser = HttpContext.User.Identity.Name;
                    if (string.IsNullOrWhiteSpace(modifiedUser))
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
                    ModelState.Clear();
                    var date = MyUtilities.Function.ParseDate(exportDate);
                    if (date > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var transaction = new TransactionFpt {
                            TransactionDate = date,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            PoId = null,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            TransactionFptDetails = new List<TransactionFptDetail>(),
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Tool, 1),
                            EoI = Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Export),
                            Type = (byte)MyUtilities.Tool.ExportType.Production2,
                            Fpt = Convert.ToInt16(MyUtilities.PurchaseOrder.FptLot.Tool),
                            AccountantSignature = 0,
                            InventorySignature = 0,
                            PurchasingSignature = 0,
                            QcSignature = 0,
                            ExchangeRate = 1
                        };
                        var exportTool = new ExportTool {
                            TransactionFpt = transaction,
                            TransactionId = transaction.TransactionId,

                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ExportDate = date,
                            Description = description,
                            Department = department,
                        };
                        //
                        foreach (var detail in updatedDetails) {
                            if (detail.Quantity == 0) continue;
                            var toolInv = vfi.ToolInventories.FirstOrDefault(m => m.ToolInvId == detail.ToolInvId);
                            if (toolInv == null) continue;
                            var quantity = toolInv.TotalQuantity - detail.Quantity;
                            if (quantity < 0)
                                throw new AggregateException("Lỗi! Lô công cụ " + detail.FuelFullCodeName + " " +
                                                             toolInv.LotNumber + " xuất vượt quá tồn kho");
                            var openTransactions = from td in vfi.TransactionFptDetails
                                                   where td.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                                   td.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                                 td.FptId == detail.ToolId && td.LotNumber.Equals(detail.LotNumber)
                                                   select td;
                            if (openTransactions.Any()) {
                                quantity -= openTransactions.Sum(ot => ot.Quantity);
                                if (quantity < 0)
                                    throw new AggregateException("Lỗi! Lô công cụ " + detail.FuelFullCodeName + " " +
                                                                 toolInv.LotNumber + " xuất vượt quá tồn kho còn lại");
                            }
                            if (toolInv.ImportDate != null && toolInv.ImportDate > transaction.TransactionDate)
                                throw new AggregateException("Lỗi! Lô công cụ " + detail.FuelFullCodeName + " " +
                                                             toolInv.LotNumber + " xuất sai thời gian! (nhập kho: "
                                                             + toolInv.ImportDate.Value.ToString("dd/MM/yyyy"));
                            if (toolInv.Lock == true) {
                                throw new AggregateException("Lô Công cụ đang Khóa do bị NG, vui lòng chọn lô khác!");
                            }
                            if (detail.MachineId == 0) {
                                throw new AggregateException("Vui lòng nhập số Máy");
                            }
                            var transactionDetail = new TransactionFptDetail {
                                FptId = toolInv.ToolId,
                                Quantity = detail.Quantity,
                                UnitPrice = toolInv.UnitPrice,
                                LotNumber = toolInv.LotNumber,
                                Note = detail.Note,
                                UnitMeasure = toolInv.UnitMeasure,
                                TransactionFpt = transaction,
                                TransactionId = transaction.TransactionId,
                                VendorId = toolInv.VendorId,
                                NG = toolInv.NG,
                            };
                            transaction.TransactionFptDetails.Add(transactionDetail);

                            var exportDetail = new ExportToolDetail {
                                Quantity = detail.Quantity,
                                ToolInvId = toolInv.ToolInvId,
                                ExportId = exportTool.ExportId,
                                ExportTool = exportTool,
                                TransactionFptDetail = transactionDetail,
                                TransactionDetailId = transactionDetail.DetailId,
                            };
                            exportTool.ExportToolDetails.Add(exportDetail);
                            if (detail.ProductId != 0)
                                exportDetail.ProductId = detail.ProductId;
                            if (detail.MachineId != 0) {
                                exportDetail.MachineId = detail.MachineId;
                                transactionDetail.MachineId = detail.MachineId;
                            }
                        }
                        if (transaction.TransactionFptDetails.Any()) {
                            vfi.TransactionFpts.Add(transaction);
                            vfi.ExportTools.Add(exportTool);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    var errorMessage = exception.Message;
                    var innerException = exception.InnerException;

                    while (innerException != null) {
                        errorMessage += " | InnerException: " + innerException.Message;
                        innerException = innerException.InnerException;
                    }

                    ModelState.AddModelError("UpdateAssignTool2", errorMessage);
                }
            }

            Session["ListExportToolInvIds"] = new List<int>();

            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        [GridAction]
        public ActionResult ExportTransactionToolInventory(
            [Bind(Prefix = "inserted")] IEnumerable<TransactionFptDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<TransactionFptDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<TransactionFptDetailModel> deletedDetails,
            string exportDate) {
            if (updatedDetails != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var modifiedUser = HttpContext.User.Identity.Name;
                    if (string.IsNullOrWhiteSpace(modifiedUser))
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
                    ModelState.Clear();
                    var date = MyUtilities.Function.ParseDate(exportDate);
                    if (date > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var transaction = new TransactionFpt {
                            TransactionDate = date,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            PoId = null,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            TransactionFptDetails = new List<TransactionFptDetail>(),
                            TransactionCode =
                                MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Tool, 1),
                            EoI = Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Export),
                            Type = (byte)MyUtilities.Tool.ExportType.Destroy,
                            Fpt = Convert.ToInt16(MyUtilities.PurchaseOrder.FptLot.Tool),
                            AccountantSignature = 0,
                            InventorySignature = 0,
                            PurchasingSignature = 0,
                            QcSignature = 0,
                            ExchangeRate = 1
                        };
                        //
                        foreach (var detail in updatedDetails) {
                            var toolInv = vfi.ToolInventories.FirstOrDefault(m => m.ToolInvId == detail.ToolInvId);
                            if (toolInv == null) continue;
                            if (detail.IsDestroy) detail.Quantity = toolInv.TotalQuantity;
                            if (detail.Quantity == 0) continue;
                            var quantity = toolInv.TotalQuantity - detail.Quantity;
                            if (quantity < 0)
                                throw new AggregateException("Lỗi! Lô công cụ " + detail.ToolFullCodeName + " " +
                                                             toolInv.LotNumber + " xuất vượt quá tồn kho");
                            var openTransactions = from td in vfi.TransactionFptDetails
                                                   where td.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                                   td.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                                 td.FptId == detail.ToolId && td.LotNumber.Equals(detail.LotNumber)
                                                   select td;
                            if (openTransactions.Any()) {
                                quantity -= openTransactions.Sum(ot => ot.Quantity);
                                if (quantity < 0)
                                    throw new AggregateException("Lỗi! Lô công cụ " + detail.ToolFullCodeName + " " +
                                                                 toolInv.LotNumber + " xuất vượt quá tồn kho còn lại");
                            }
                            if (toolInv.ImportDate != null && toolInv.ImportDate > transaction.TransactionDate)
                                throw new AggregateException("Lỗi! Lô công cụ " + detail.ToolFullCodeName + " " +
                                                             toolInv.LotNumber + " xuất sai thời gian! (nhập kho: "
                                                             + toolInv.ImportDate.Value.ToString("dd/MM/yyyy"));
                            var transactionDetail = new TransactionFptDetail {
                                FptId = toolInv.ToolId,
                                Quantity = detail.Quantity,
                                UnitPrice = toolInv.UnitPrice,
                                LotNumber = toolInv.LotNumber,
                                Note = detail.Note,
                                UnitMeasure = toolInv.UnitMeasure,
                                TransactionFpt = transaction,
                                TransactionId = transaction.TransactionId,
                                VendorId = toolInv.VendorId,
                            };
                            transaction.TransactionFptDetails.Add(transactionDetail);
                        }
                        if (transaction.TransactionFptDetails.Any()) {
                            vfi.TransactionFpts.Add(transaction);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ExportTransactionToolInventory", "" + exception.Message);
                }
            }

            Session["ListExportToolInvIds"] = new List<int>();

            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        [GridAction]
        public ActionResult CreateExportInternalTool(
            [Bind(Prefix = "inserted")] IEnumerable<TransactionFptDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<TransactionFptDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<TransactionFptDetailModel> deletedDetails,
            string exportDate) {
            if (updatedDetails != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var modifiedUser = HttpContext.User.Identity.Name;
                    if (string.IsNullOrWhiteSpace(modifiedUser))
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
                    ModelState.Clear();
                    var date = MyUtilities.Function.ParseDate(exportDate);
                    if (date > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var transaction = new TransactionFpt {
                            TransactionDate = date,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            PoId = null,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            TransactionFptDetails = new List<TransactionFptDetail>(),
                            TransactionCode =
                                MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Tool, 1),
                            EoI = Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Export),
                            Type = (byte)MyUtilities.Tool.ExportType.Destroy,
                            Fpt = Convert.ToInt16(MyUtilities.PurchaseOrder.FptLot.Tool),
                            AccountantSignature = 0,
                            InventorySignature = 0,
                            PurchasingSignature = 0,
                            QcSignature = 0,
                            ExchangeRate = 1,
                            IsInternal = true
                        };
                        //
                        foreach (var detail in updatedDetails) {
                            var toolInv = vfi.ToolInventories.FirstOrDefault(m => m.ToolInvId == detail.ToolInvId);
                            if (toolInv == null) continue;
                            if (detail.IsDestroy) detail.Quantity = toolInv.TotalQuantity;
                            if (detail.Quantity == 0) continue;
                            var quantity = toolInv.TotalQuantity - detail.Quantity;
                            if (quantity < 0)
                                throw new AggregateException("Lỗi! Lô công cụ " + detail.ToolFullCodeName + " " +
                                                             toolInv.LotNumber + " xuất vượt quá tồn kho");
                            var openTransactions = from td in vfi.TransactionFptDetails
                                                   where td.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                                   td.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                                 td.FptId == detail.ToolId && td.LotNumber.Equals(detail.LotNumber)
                                                   select td;
                            if (openTransactions.Any()) {
                                quantity -= openTransactions.Sum(ot => ot.Quantity);
                                if (quantity < 0)
                                    throw new AggregateException("Lỗi! Lô công cụ " + detail.ToolFullCodeName + " " +
                                                                 toolInv.LotNumber + " xuất vượt quá tồn kho còn lại");
                            }
                            if (toolInv.ImportDate != null && toolInv.ImportDate > transaction.TransactionDate)
                                throw new AggregateException("Lỗi! Lô công cụ " + detail.ToolFullCodeName + " " +
                                                             toolInv.LotNumber + " xuất sai thời gian! (nhập kho: "
                                                             + toolInv.ImportDate.Value.ToString("dd/MM/yyyy"));
                            var transactionDetail = new TransactionFptDetail {
                                FptId = toolInv.ToolId,
                                Quantity = detail.Quantity,
                                UnitPrice = toolInv.UnitPrice,
                                LotNumber = toolInv.LotNumber,
                                Note = detail.Note,
                                UnitMeasure = toolInv.UnitMeasure,
                                TransactionFpt = transaction,
                                TransactionId = transaction.TransactionId,
                                VendorId = toolInv.VendorId,
                                IsInternal = transaction.IsInternal
                            };
                            transaction.TransactionFptDetails.Add(transactionDetail);
                        }
                        if (transaction.TransactionFptDetails.Any()) {
                            vfi.TransactionFpts.Add(transaction);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ExportTransactionToolInventory", "" + exception.Message);
                }
            }

            Session["ListExportToolInvIds"] = new List<int>();

            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        [GridAction]
        public ActionResult SelectSmartProductionForTool() {
            var model = new List<SmartProductionModel>();
            using (var vfi = new tammaContext()) {
                var machines =
                    vfi.Machines.Where(m => m.Active || m.Production2)
                       .OrderBy(m => m.Active)
                       .ThenBy(m => m.Production2)
                       .ThenBy(m => m.MachineName);
                foreach (var machine in machines) {
                    var smartProduct = vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == machine.MachineId);
                    var entity = new SmartProductionModel();
                    if (machine.Active)
                        entity.SmartType = "SX1";
                    if (machine.Production2)
                        entity.SmartType = "SX2";
                    if (machine.MachineName.Equals("Cơ khí"))
                        entity.SmartType = "Cơ khí";
                    entity.MachineName = machine.MachineName;
                    entity.MachineId = machine.MachineId;
                    if (smartProduct == null) {
                        entity.ProductId = 0;
                        entity.ToolInvId = 0;
                    }
                    else {
                        entity.ProductId = smartProduct.ProductId ?? 0;
                        entity.ToolInvId = smartProduct.ToolInvId ?? 0;
                    }
                    if (entity.ProductId != 0) {
                        entity.ProductCode = smartProduct.Product.ProductCode;
                    }
                    if (entity.ToolInvId != 0) {
                        var toolInv =
                            vfi.ToolInventories.FirstOrDefault(
                                mi => mi.ToolInvId == entity.ToolInvId);
                        if (toolInv != null) {
                            entity.ToolFullCode = toolInv.Vendor.VendorCode +
                                                  toolInv.Tool.ToolFullCode + "-" +
                                                  toolInv.LotNumber;
                            entity.ToolInvTotal = toolInv.TotalQuantity;
                        }
                    }
                    model.Add(entity);
                }
                //var machines2 = vfi.Machines.Where(m => m.Production2).OrderBy(m => m.MachineName);
                //foreach (var machine in machines2)
                //{
                //    var smartProduct = vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == machine.MachineId);
                //    var entity = new SmartProductionModel();
                //    entity.SmartType = "SX2";
                //    entity.MachineName = machine.MachineName;
                //    entity.MachineId = machine.MachineId;
                //    if (smartProduct == null)
                //    {
                //        entity.ProductId = 0;
                //        entity.MaterialId = 0;
                //        entity.WarehouseExportId = 0;
                //        entity.MaterialInventoryId = 0;
                //        entity.FuelInvId = 0;
                //        entity.ToolInvId = 0;
                //    }
                //    else
                //    {
                //        entity.ProductId = smartProduct.ProductId ?? 0;
                //        entity.MaterialId = 0;
                //        entity.ToolInvId = smartProduct.ToolInvId ?? 0;
                //    }
                //    if (entity.ProductId != 0)
                //    {
                //        entity.ProductCode = smartProduct.Product.ProductCode;
                //    }
                //    if (entity.ToolInvId != 0)
                //    {
                //        var toolInv =
                //            vfi.ToolInventories.FirstOrDefault(
                //                mi => mi.ToolInvId == entity.ToolInvId);
                //        if (toolInv != null)
                //        {
                //            entity.ToolFullCode = toolInv.Vendor.VendorCode +
                //                                  toolInv.Tool.ToolFullCode + "-" +
                //                                  toolInv.LotNumber;
                //            entity.ToolInvTotal = toolInv.TotalQuantity;
                //        }
                //    }
                //    model.Add(entity);
                //}
            }
            return View(new GridModel(model));
        }

        public ActionResult PrintToolInvTotalInMonth(int vendorId, string fromDate, string toDate, int type, int toolTypeId, string identity) {
            var model = new List<GroupToolInventory>();
            var ci = new CultureInfo("vi-VN");
            var lastDate = string.IsNullOrWhiteSpace(toDate)
                           ? DateTime.Today
                           : Convert.ToDateTime(toDate, ci);
            try {
                var list = GetPrintToolInvTotal(vendorId, "", toolTypeId, type, fromDate, toDate, identity, false, (int)MyUtilities.Report.Calculate.All, (int)MyUtilities.Report.Calculate.All);
                var materialTypeIds = list.Select(l => l.MaterialTypeId).Distinct().ToList();

                foreach (var materialTypeId in materialTypeIds) {
                    var listById = list.Where(l => l.MaterialTypeId == materialTypeId).ToList();
                    var group = new GroupToolInventory {
                        MaterialTypeId = listById.FirstOrDefault().MaterialTypeId,
                        GroupName = listById.FirstOrDefault().MaterialTypeName,
                        LastPeriodDateString = lastDate.ToString("dd/MM/yyyy"),
                        ReportDateString = lastDate.ToString("MM/yyyy")
                    };
                    if (type == 1) group.MaterialTypeName = " KHO A";
                    else if (type == 2) group.MaterialTypeName = " KHO B";

                    var toolIds = listById.Select(l => l.ToolId).Distinct().ToList();
                    foreach (var toolId in toolIds) {
                        var listDetailById = listById.Where(x => x.ToolId == toolId).ToList();
                        var tool = listDetailById.FirstOrDefault();
                        var entity = new ReportToolInventory {
                            MaterialTypeId = tool.MaterialTypeId,
                            MaterialTypeName = tool.MaterialTypeName,
                            ToolCode = tool.ToolCode,
                            ToolName = tool.ToolName,
                            ToolFullCode = tool.ToolFullCode,
                            ToolDesign = tool.ToolDesign,
                            ToolMaterial = tool.ToolMaterial,
                            ToolProduction = tool.ToolProduction,
                            ToolTypeId = tool.ToolTypeId,
                        };
                        entity.Export3Month = listDetailById.Sum(x => x.Export3Month);
                        var showList = listDetailById.Where(x => x.IsShow).ToList();
                        if (showList.Any() || entity.Export3Month > 0) {
                            entity.Details.AddRange(showList);
                            group.TotalInv += entity.TotalInv;
                            group.List.Add(entity);
                        }
                    }
                    //group.TotalInv = listById.Sum(l => l.TotalQuantity);
                    //group.List.AddRange(listById);
                    //if (group.List.Any())
                    model.Add(group);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PageToolTotal", ex.Message);
            }
            return PartialView("PageToolTotal", model.OrderBy(m => m.GroupName).ToList());
        }

        [GridAction]
        public ActionResult UpdateToolInvTotal(ToolInventoryModel updated, int toolTypeId, string fromDate, string toDate) {
            try {
                using (var vfi = new tammaContext()) {
                    var toolInv = vfi.ToolInventories.FirstOrDefault(ti => ti.ToolInvId == updated.ToolInvId);
                    if (toolInv == null)
                        throw new AggregateException("Lỗi! Không tìm thấy công cụ tồn kho");
                    toolInv.StoreCode = updated.StoreCode;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateToolInvTotal", ex.Message);
            }
            return View(new GridModel(GetPrintToolInvTotal(0, "", toolTypeId, 0, fromDate, toDate, "", true,(int)MyUtilities.Report.Calculate.InPeriod, (int)MyUtilities.Report.Calculate.InPeriod)));
        }

        [GridAction]
        public ActionResult SelectToolExportTotal(string toolName, int toolTypeId, string fromDate, string toDate) {
            if (toolTypeId == -1)
                return View(new GridModel(new List<TransactionFptDetailModel>()));
            var model = new List<TransactionFptDetailModel>();
            try {
                model = GetPrintToolExportTotal(toolName, toolTypeId, fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectToolExportTotal", ex.Message);

            }
            return View(new GridModel(model));
        }

        private List<TransactionFptDetailModel> GetPrintToolExportTotal(string toolName, int toolTypeId, string fromDate,
            string toDate) {
            var model = new List<TransactionFptDetailModel>();
            var ci = new CultureInfo("vi-VN");
            var startDate = MyUtilities.Function.ParseDate(fromDate);
            var lastDate = MyUtilities.Function.ParseLastDateTime(toDate);
            using (var vfi = new tammaContext()) {
                var tools = from t in vfi.Tools
                            where (toolTypeId == 0 || t.MaterialTypeId == toolTypeId)
                            select t;
                if (!string.IsNullOrWhiteSpace(toolName))
                    tools = tools.Where(t => t.ToolFullCode.Contains(toolName));
                var toolIds = tools.Select(t => t.ToolId).ToList();
                var exportTools = (from ed in vfi.ExportToolDetails
                                  where ed.ExportTool.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                        ed.ExportTool.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                        ed.ExportTool.TransactionFpt.TransactionDate >= startDate &&
                                        ed.ExportTool.TransactionFpt.TransactionDate <= lastDate &&
                                        toolIds.Contains(ed.ToolInventory.ToolId)
                                  orderby ed.ExportTool.ExportDate
                                  select ed).ToList();
                var productIds =
                    exportTools.Where(ed => ed.ProductId != null).Select(ed => ed.ProductId.Value).Distinct().ToList();
                var productions = (from id in vfi.ProductInventoryPeriods
                                  where productIds.Contains(id.ProductId) &&
                                        id.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                        id.Transaction.WarehouseIssueId == null
                                  select id).ToList();
                foreach (var exportTool in exportTools) {
                    var exportToolId = exportTool.ExportDetailId;
                    var entity = new TransactionFptDetailModel() {
                        TransactionDate = exportTool.ExportTool.ExportDate.Value,
                        ToolId = exportTool.ToolInventory.ToolId,
                        ToolName = exportTool.ToolInventory.Tool.ToolName,
                        ToolFullCodeName = exportTool.ToolInventory.Tool.ToolFullCode,
                        ToolInvId = exportTool.ToolInvId,
                        LotNumber = exportTool.ToolInventory.LotNumber,
                        VendorCode = exportTool.ToolInventory.Vendor.VendorCode,
                        Quantity = exportTool.Quantity,
                        TransactionCode = exportTool.ExportTool.TransactionFpt.TransactionCode,
                        Note = exportTool.TransactionFptDetail.Note,
                        TaxInvoiceNumber = exportTool.ExportTool.Department,
                        EoIName =
                            MyUtilities.PurchaseOrder.GetEoIName(exportTool.TransactionFptDetail.TransactionFpt.EoI,
                                exportTool.TransactionFptDetail.TransactionFpt.Type),
                        Department = exportTool.ExportTool.Department,
                    };
                    if (exportTool.MachineId != null) {
                        entity.MachineId = exportTool.MachineId.Value;
                        entity.MachineName = exportTool.Machine.MachineName;
                    }
                    if (exportTool.ProductId != null) {
                        entity.ProductId = exportTool.ProductId.Value;
                        entity.ProductCode = exportTool.Product.ProductCode;
                        if (exportTool.ExportTool.TransactionFpt.Type ==
                            (byte)MyUtilities.Tool.ExportType.Production) {
                            var lastExport =
                                vfi.ExportToolDetails.Where(
                                        ed =>
                                            ed.ExportTool.TransactionFpt.Status ==
                                            (byte)MyUtilities.Transaction.Status.Approved &&
                                            ed.ExportTool.ExportDate > entity.TransactionDate &&
                                            ed.MachineId == entity.MachineId &&
                                            ed.ProductId == entity.ProductId &&
                                            ed.ToolInventory.ToolId == entity.ToolId)
                                    .OrderBy(ed => ed.ExportTool.ExportDate)
                                    .FirstOrDefault();
                            if (lastExport != null) {
                                var productionsById =
                                    productions.Where(p => p.ProductId == entity.ProductId &&
                                                           p.PeriodDate >= entity.TransactionDate &&
                                                           p.PeriodDate < lastExport.ExportTool.ExportDate.Value)
                                        .ToList();
                                entity.RequiredQuantity = productionsById.Sum(p => p.Quantity);
                            }
                            else {
                                var productionsById =
                                    productions.Where(p => p.ProductId == entity.ProductId &&
                                                           p.PeriodDate >= entity.TransactionDate)
                                        .ToList();
                                entity.RequiredQuantity = productionsById.Sum(p => p.Quantity);
                            }
                        }
                    }
                    var productionTool =
                        vfi.ProductionTools.FirstOrDefault(
                            pt => pt.ToolId == entity.ToolId && pt.ProductId == entity.ProductId);
                    if (productionTool != null) {
                        entity.QuotaQuantity = productionTool.Quota;
                    }
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectToolInvTotal(int vendorId, string toolName, int toolTypeId, int type, int reportType, string fromDate, string toDate, string identity) {
            if (toolTypeId == -1)
                return View(new GridModel(new List<ToolInventoryModel>()));
            var model = new List<ToolInventoryModel>();
            try {
                model = GetPrintToolInvTotal(vendorId, toolName, toolTypeId, type, fromDate, toDate, identity, true, reportType, reportType);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectToolInvTotal", ex.Message);

            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectToolInvDetailTotal(int toolInvId, string fromDate, string toDate) {

            var model = new List<TransactionFptDetailModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fDate = string.IsNullOrWhiteSpace(fromDate)
                                   ? DateTime.Today
                                   : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                                   ? DateTime.Today
                                   : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    var toolPeriods = (from tip in vfi.ToolInventoryPeriods
                                       where tip.ToolInvId == toolInvId
                                             && tip.PeriodDate >= fDate
                                             && tip.PeriodDate <= tDate
                                       select tip).ToList();
                    foreach (var period in toolPeriods) {
                        var entity = new TransactionFptDetailModel {
                            TransactionDate = period.PeriodDate,
                            ToolCode = period.Tool.ToolCode,
                            Quantity = period.Quantity,
                            TransactionCode = period.TransactionFpt.TransactionCode,
                            EoI = period.TransactionFpt.EoI,
                            Type = period.TransactionFpt.Type,
                            ModifiedDate = period.ModifiedDate,
                            ModifiedUser = period.ModifiedUser,
                        };
                        if (entity.EoI != 0)
                            entity.EoIName = MyUtilities.PurchaseOrder.GetEoIName(entity.EoI, entity.Type);
                        if (period.TransactionFpt.PoId != null)
                            entity.EoIName += " " + period.TransactionFpt.PurchaseOrder.RevisionNumber;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectToolInvDetailTotal", ex.Message);

            }
            return View(new GridModel(model.OrderBy(x => x.TransactionDate).ToList()));
        }

        private List<ToolInventoryModel> GetPrintToolInvTotal(
            int vendorId, 
            string toolName, int toolTypeId, int type,
            string fromDate, string toDate,
            string identity, bool productPoNote,
            int reportType, int calculateMode) {
            var model = new List<ToolInventoryModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var startDate = string.IsNullOrWhiteSpace(fromDate)
                                    ? DateTime.Today
                                    : Convert.ToDateTime(fromDate, ci);
                //var startDate = new DateTime(year, month, 1);
                //var lastDate = date;
                var lastDate = string.IsNullOrWhiteSpace(toDate)
                                   ? DateTime.Today
                                   : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var tools = (from t in vfi.Tools
                                 where t.Active &&
                                       (toolTypeId == 0 || t.MaterialTypeId == toolTypeId)
                                 //&& t.ToolId == 1337
                                 orderby t.ToolFullCode
                                 select new {
                                     t.ToolId,
                                     t.ToolCode,
                                     t.ToolFullCode,
                                     t.ToolName,
                                     t.ToolMaterial,
                                     t.ToolDesignNo,
                                     t.MaterialTypeId,
                                     t.MaterialType.IdentityCode,
                                     t.MaterialType.MaterialTypeName,
                                     t.ToolProduction,
                                 }).ToList();
                    if (!string.IsNullOrWhiteSpace(toolName))
                        tools = tools.Where(t => t.ToolFullCode.Contains(toolName)).ToList();
                    if (!string.IsNullOrWhiteSpace(identity))
                        tools = tools.Where(t => t.IdentityCode.Contains(identity)).ToList();
                    var toolIds = tools.Select(t => t.ToolId).ToList();

                    var last3Month = startDate.AddMonths(-3);
                    var toolInvs = (from ti in vfi.ToolInventories
                                    where toolIds.Contains(ti.ToolId) &&
                                   (ti.EndDate == null || ti.EndDate >= last3Month) &&
                                   (vendorId == 0 || ti.VendorId == vendorId)
                                    select new {
                                        ti.ToolId,
                                        ti.ToolInvId,
                                        ti.Vendor.VendorCode,
                                        ti.VendorId,
                                        ti.LotNumber,
                                        ti.UnitPrice,
                                        ti.UnitMeasure,
                                        ti.ImportDate,
                                        ImportQuantity = ti.ImportQuantity ?? 0,
                                        ti.FirstUseDate,
                                        ti.EndDate,
                                        ti.StoreCode,
                                        ti.TotalQuantity,
                                    }).ToList();

                    if (type == 1) {
                        toolInvs = toolInvs.Where(ti => ti.UnitPrice > 0).ToList();
                        toolIds = toolInvs.Select(t => t.ToolId).Distinct().ToList();
                        tools = tools.Where(t => toolIds.Contains(t.ToolId)).ToList();
                    }
                    else if (type == 2) {
                        toolInvs = toolInvs.Where(ti => ti.UnitPrice == 0).ToList();
                        toolIds = toolInvs.Select(t => t.ToolId).Distinct().ToList();
                        tools = tools.Where(t => toolIds.Contains(t.ToolId)).ToList();
                    }
                    var toolInvIds = toolInvs.Select(x => x.ToolInvId).ToList();
                    var toolPeriods = (from tip in vfi.ToolInventoryPeriods
                                       where toolInvIds.Contains(tip.ToolInvId)
                                             && tip.PeriodDate <= lastDate
                                       select new {
                                           tip.ToolId,
                                           tip.ToolInvId,
                                           tip.PeriodDate,
                                           tip.EarlyQuantity,
                                           tip.LastQuantity,
                                           tip.Quantity,
                                           IsInternal = tip.TransactionFpt.IsInternal ?? false,
                                           IsPurchase = tip.TransactionFpt.PoId != null,
                                       }).ToList();
                    if (calculateMode == (int)MyUtilities.Report.Calculate.InPeriod) {
                        var inPeriods = toolPeriods.Where(x => x.PeriodDate >= startDate).ToList();
                        if (!inPeriods.Any()) {
                            return model;
                        }
                        toolIds = inPeriods.Select(x => x.ToolId).Distinct().ToList();
                        tools = tools.Where(x => toolIds.Contains(x.ToolId)).ToList();
                    }
                    //var purchaseTools = (from x in vfi.TransactionFptDetails
                    //                   where x.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved
                    //                   && x.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool
                    //                    && toolIds.Contains(x.FptId)
                    //                    && x.TransactionFpt.TransactionDate >= startDate
                    //                    && x.TransactionFpt.TransactionDate <= lastDate
                    //                   select new {
                    //                       x.LotNumber,
                    //                       x.FptId,
                    //                       x.Quantity
                    //                   }).ToList();
                    var exportTools = (from x in vfi.ExportToolDetails
                                       where x.ExportTool.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved
                                        && toolIds.Contains(x.ToolInventory.ToolId)
                                        && x.ExportTool.ExportDate >= startDate
                                        && x.ExportTool.ExportDate <= lastDate
                                       select new {
                                           x.ToolInventory.ToolInvId,
                                           x.Quantity
                                       }).ToList();
                    var productionTools = new List<ProductionToolModel>();
                    var productIds = new List<int>();
                    var orderDetails = new List<OrderDetailModel>();
                    if (productPoNote) {
                        productionTools = (from pt in vfi.ProductionTools
                                           where pt.Active && toolIds.Contains(pt.ToolId)
                                           select new ProductionToolModel {
                                               ProductCode = pt.Product.ProductCode,
                                               ProductId = pt.ProductId,
                                               ToolId = pt.ToolId
                                           }).ToList();
                        productIds = productionTools.Select(pt => pt.ProductId).Distinct().ToList();
                        orderDetails = (from od in vfi.OrderDetails
                                        where (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                               od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                              od.Order.DueDate != null &&
                                              productIds.Contains(od.ProductId) &&
                                              od.RequiedNumber > 0
                                        select new OrderDetailModel {
                                            ProductId = od.ProductId,
                                            //VFIDueDate = od.Order.DueDate,
                                            OrderQty = od.OrderQty ?? 0,
                                            RequiredNumber = od.RequiedNumber,
                                        }).ToList();
                    }
                    var listProductionTool = new List<ProductionToolModel>();
                    foreach (var tool in tools) {
                        var listProductionToolById = new List<ProductionToolModel>();

                        if (productPoNote) {
                            var productionToolsById = productionTools.Where(pt => pt.ToolId == tool.ToolId);
                            foreach (var productionTool in productionToolsById) {
                                var orderDetailsById = orderDetails.Where(od => od.ProductId == productionTool.ProductId);
                                if (!orderDetailsById.Any())
                                    continue;
                                var productionToolModel =
                                    listProductionTool.FirstOrDefault(
                                        pt => pt.ProductId == productionTool.ProductId && pt.ToolId == productionTool.ToolId);
                                if (productionToolModel != null)
                                    continue;

                                productionToolModel = new ProductionToolModel {
                                    ToolId = productionTool.ToolId,
                                    ProductCode = productionTool.ProductCode,
                                    ProductId = productionTool.ProductId,
                                    ProductOrder = orderDetailsById.Sum(od => od.RequiredNumber)
                                };
                                listProductionToolById.Add(productionToolModel);
                                listProductionTool.Add(productionToolModel);
                            }
                        }

                        var toolInvsById = toolInvs.Where(ti => ti.ToolId == tool.ToolId).ToList();
                        foreach (var toolInventory in toolInvsById) {
                            var entity = new ToolInventoryModel {
                                MaterialTypeId = tool.MaterialTypeId,
                                MaterialTypeName = tool.IdentityCode + "-" + tool.MaterialTypeName,
                                LotNumber = toolInventory.LotNumber,
                                ToolId = tool.ToolId,
                                ToolCode = tool.ToolCode,
                                ToolName = tool.ToolName,
                                ToolFullCode = MyUtilities.Tool.GetFullToolDesign(tool.ToolDesignNo, tool.ToolMaterial, tool.ToolProduction),
                                VendorCode = toolInventory.VendorCode,
                                UnitPrice = toolInventory.UnitPrice,
                                UnitMeasure = toolInventory.UnitMeasure,
                                ToolDesign = tool.ToolDesignNo,
                                ToolMaterial = tool.ToolMaterial,
                                ToolProduction = tool.ToolProduction,
                                ImportDate = toolInventory.ImportDate,
                                FirstImportQuantity = toolInventory.ImportQuantity,
                                FirstUseDate = toolInventory.FirstUseDate,
                                EndDate = toolInventory.EndDate,
                                StoreCode = toolInventory.StoreCode,
                                FromDate = fromDate,
                                ToDate = toDate,
                                ToolTypeId = toolTypeId,
                                ToolInvId = toolInventory.ToolInvId,
                                TotalQuantity = toolInventory.TotalQuantity,
                            };
                            entity.ProductionTools = listProductionToolById;
                            var toolPeriodsById = toolPeriods.Where(tip => tip.ToolInvId == entity.ToolInvId).ToList();
                            var periods = toolPeriodsById.Where(tip => tip.PeriodDate < startDate).ToList();

                            entity.Early = periods.Sum(tip => tip.LastQuantity - tip.EarlyQuantity);
                            periods = toolPeriodsById.Where(tip => tip.PeriodDate >= startDate && tip.LastQuantity > tip.EarlyQuantity)
                                                    .ToList();
                            entity.Import = periods.Where(x => x.IsPurchase).Sum(tip => tip.Quantity);
                            entity.ImportMore = periods.Where(x => !x.IsInternal && !x.IsPurchase).Sum(tip => tip.Quantity);
                            //var purchaseToolsById = purchaseTools.Where(x => x.FptId == entity.ToolId && x.LotNumber.Equals(entity.LotNumber));
                            //if (purchaseToolsById.Any()) {
                            //    entity.Import = purchaseToolsById.Sum(x => x.Quantity);
                            //    entity.ImportMore -= entity.Import;                            
                            //}

                            entity.ImportInternal = periods.Where(x => x.IsInternal).Sum(tip => tip.Quantity);

                            periods = toolPeriodsById.Where(tip => tip.PeriodDate >= startDate && tip.LastQuantity < tip.EarlyQuantity)
                                                .ToList();
                            entity.ExportDestroy = periods.Where(x => !x.IsInternal).Sum(tip => tip.Quantity);
                            entity.ExportInternal = periods.Where(x => x.IsInternal).Sum(tip => tip.Quantity);
                            var exportToolsById = exportTools.Where(x => x.ToolInvId == toolInventory.ToolInvId);
                            if (exportToolsById.Any()) {
                                entity.Export = exportToolsById.Sum(x => x.Quantity);
                                entity.ExportDestroy -= entity.Export;
                            }

                            periods = toolPeriodsById.Where(tip => tip.PeriodDate >= last3Month && tip.LastQuantity < tip.EarlyQuantity)
                                                .ToList();
                            entity.Export3Month = periods.Sum(tip => tip.Quantity);
                            if (entity.IsShow) {
                                model.Add(entity);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PageToolTotal", ex.Message);
            }
            return
                model.OrderBy(m => m.ToolName)
                     .ThenBy(m => m.ToolFullCode)
                     .ThenBy(m => m.LotNumber)
                     .ToList();
        }


            // code mau moi

        public ActionResult PrintAssignProductionTool() {
            var model = new List<ProductionToolModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var toolInvs = vfi.ToolInventories.Where(ti => ti.Tool.Active && ti.TotalQuantity > 0);

                    var machines = from m in vfi.Machines
                                   where m.Active || m.Production2
                                   select m;
                    foreach (var machine in machines) {
                        var lastTrack =
                            (from t in vfi.TrackUpMachines
                             where
                             t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                             t.MachineId == machine.MachineId &&
                             t.Product.Active &&
                             t.DeliveryDate != null
                             select new {
                                 t.MachineId,
                                 t.TrackId,
                                 t.Machine.MachineName,
                                 t.ProductId,
                                 t.Product.ProductCode,
                                 Date = t.DeliveryDate.Value
                             })
                            .OrderByDescending(t => t.Date)
                            .FirstOrDefault();
                        if (lastTrack != null) {
                            var productionToolsById =
                                vfi.ProductionTools.Where(pt => pt.ProductId == lastTrack.ProductId && pt.Active);
                            //var replaceTools = vfi.ProductionToolReplacements.Where(ptr => ptr.TrackId == lastTrack.TrackId);
                            foreach (var productionTool in productionToolsById) {
                                var toolId = productionTool.ToolId;
                                var replaceTool = vfi.ProductionToolReplacements.FirstOrDefault(rt => rt.ProductionToolId == productionTool.RealToolId 
                                    &&rt.TrackId == lastTrack.TrackId);

                                if (replaceTool != null) {
                                    toolId = replaceTool.ReplaceId;
                                }
                                var tool = vfi.Tools.FirstOrDefault(t => t.ToolId == toolId);

                                var entity = model.FirstOrDefault(m => m.ToolId == toolId);
                                if (entity == null) {
                                    entity = new ProductionToolModel {
                                        ToolId = toolId,
                                        ToolCode = tool.ToolCode,
                                        ToolDesign = tool.ToolDesignNo,
                                        ToolMaterial = tool.ToolMaterial,
                                        ToolProduction = tool.ToolProduction,
                                        ToolName = tool.ToolName,
                                        //ToolFullCode = tool.ToolFullCode,
                                        ToolFullCode = MyUtilities.Tool.GetFullToolDesign(tool.ToolDesignNo, tool.ToolMaterial, tool.ToolProduction),
                                        ToolTypeName = tool.MaterialType.MaterialTypeName,
                                        ProductCode = lastTrack.ProductCode,
                                        MachineName = lastTrack.MachineName,
                                        PrepareQuantity = 0,
                                        ToolInv = 0
                                    };
                                    var toolInvsById = toolInvs.Where(ti => ti.ToolId == entity.ToolId).ToList();
                                    entity.ToolInv = toolInvsById.Sum(ti => ti.TotalQuantity);
                                    if (replaceTool != null) {
                                        entity.PrepareQuantity = replaceTool.PrepareQuantity;
                                        entity.Note = replaceTool.Note + "-" + lastTrack.MachineName + "| ";
                                    }
                                    model.Add(entity);
                                }
                                else {
                                    if (!entity.ProductCode.Contains(lastTrack.ProductCode))
                                        entity.ProductCode += (", " + lastTrack.ProductCode);
                                    if (!entity.MachineName.Contains(lastTrack.MachineName))
                                        entity.MachineName += (", " + lastTrack.MachineName);
                                    if (replaceTool != null) {
                                        entity.PrepareQuantity += replaceTool.PrepareQuantity;
                                        entity.Note += replaceTool.Note + "-" + lastTrack.MachineName + "| ";
                                    }
                                }
                            }


                        }
                    }
                }
            }
            catch (Exception ex) {
                return Json("Lỗi" + ex.Message);
            }
            return PartialView("PageAssignProductionTool",
                model.OrderBy(m => m.ToolTypeName)
                    .ThenBy(m => m.ToolName)
                    .ThenBy(m => m.ToolDesign)
                    .ThenBy(m => m.ToolMaterial)
                    .ThenBy(m => m.ToolProduction)
                    .ToList());
        }







        public ActionResult PrintProductionToolByDay(string monthlyDate) {
            var model = new List<ProductionToolModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var approvedStatus = (byte)MyUtilities.Transaction.Status.Approved;
                    var ci = new CultureInfo("vi-VN");
                    var ReportDate = string.IsNullOrWhiteSpace(monthlyDate)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(monthlyDate, ci);
                    ReportDate = ReportDate.AddDays(1).AddSeconds(-1);


                    // Step 1: Load all active machines
                    var machines = vfi.Machines
                        .Where(m => (m.Active || m.Production2)
                        //&& m.MachineName.Equals("C24")
                        && m.ProcessingType.ForWarehouseId == MyUtilities.Warehouse.Production1
                        && m.StateId != MyUtilities.Machine.State.Done
                        && m.StateId != MyUtilities.Machine.State.OutOfMaterial
                        && m.StateId != MyUtilities.Machine.State.BadState
                        )
                        .ToList();
                    var machineIds = machines.Select(m => m.MachineId).ToList();
                    // Step 2: Get latest approved TrackUpMachine per machine
                    var lastTracks = vfi.TrackUpMachines        //phieu len may
                        .Where(t => machineIds.Contains(t.MachineId)
                            && t.Status == approvedStatus
                            && t.Product.Active
                            && t.DeliveryDate <= ReportDate)     //ngay ban giao
                         .GroupBy(t => t.MachineId)
                         .Select(g => g.OrderByDescending(t => t.DeliveryDate).FirstOrDefault())
                         .Select(t => new {
                                 t.MachineId,
                                 MachineName = t.Machine.MachineName,
                                 t.ProductId,
                                 ProductCode = t.Product.ProductCode,
                                 Date = t.DeliveryDate.Value,
                                 TrackId = t.TrackId,
                            }).ToList();

                    // Step 3: Get all relevant product IDs
                    var productIds = lastTracks.Select(t => t.ProductId).Distinct().ToList();

                    // Step 4: Load all active production tools for those products
                    var productionTools = vfi.ProductionTools
                        .Where(pt => productIds.Contains(pt.ProductId) && pt.Active && pt.Tool.Active && pt.Active)
                        .ToList();

                    var replaceTools = vfi.ProductionToolReplacements.Where(rp => rp.ToolId != null
                                                                                    && productIds.Contains(rp.TrackUpMachine.ProductId)
                                                                                    && machineIds.Contains(rp.TrackUpMachine.MachineId))
                                                                       .Select(rp => new {
                                                                           toolCode = rp.Tool.ToolCode,
                                                                           toolDesign = rp.Tool.ToolDesignNo,
                                                                           toolFullCode = rp.Tool.ToolFullCode,
                                                                           toolMaterial = rp.Tool.ToolMaterial,
                                                                           toolProduction = rp.Tool.ToolProduction,
                                                                           toolName = rp.Tool.ToolName,
                                                                           toolTypeName = rp.Tool.MaterialType.MaterialTypeName,
                                                                           note = rp.Note,
                                                                           ProductId = rp.TrackUpMachine.ProductId,
                                                                           ToolId = rp.ToolId.Value,
                                                                           Active = true,
                                                                           trackId = rp.TrackId,
                                                                           RealToolId = rp.ProductionToolId,
                                                                           MachineId = rp.TrackUpMachine.MachineId,
                                                                       })
                                                                       .ToList();

                    var listRealToolId = productionTools.Select(rp => rp.RealToolId).ToList();
                    var listReplaceRealToolId = replaceTools.Select(rp => rp.RealToolId).ToList();

                    listRealToolId.AddRange(listReplaceRealToolId);
                    var new_listRealToolId = listRealToolId.Distinct().ToList();

                    // Step 5: Load all active tools with inventory
                    var toolIds = productionTools.Select(x => x.ToolId).Distinct().ToList();
                    var replaceToolIds = replaceTools.Select(x => x.ToolId).Distinct().ToList();
                    toolIds.AddRange(replaceToolIds);

                    var new_toolIds = toolIds.ToList();
                    var toolInvs = vfi.ToolInventoryPeriods                                 // bat dau o day
                        .Where(ti => new_toolIds.Contains(ti.ToolId))
                        .ToList();

                    var warehouseIds = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity2();
                    var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);     // gioi han. ngay bat dau
                    var endDate = startDate.AddMonths(3);                       // gioi han. ngay ket thuc du doan' ton` kho
                    var products = vfi.Products
                                    .Where(p => productIds.Contains(p.ProductId))
                                    .Select(p => new {
                                        ProductId = p.ProductId,
                                        Forecasts = vfi.ForecastOrders
                                           .Where(f => f.ProductId == p.ProductId && f.ForecastDate >= startDate && f.ForecastDate < endDate)
                                           .Select(f => (double?)f.Quantity)
                                           .DefaultIfEmpty(0)
                                           .Sum(),
                                        Inventories = vfi.ProductInventories
                                            .Where(inv => inv.ProductId == p.ProductId && warehouseIds.Contains(inv.WarehouseId))
                                            .Select(inv => (double?)inv.TotalQty)
                                            .DefaultIfEmpty(0)
                                            .Sum()
                                    })
                                    .ToList();




                    var lastExportTools = vfi.ExportToolDetails
                        .Where(ed =>
                            ed.ExportTool.TransactionFpt.Type == (byte)MyUtilities.Tool.ExportType.Production
                            && ed.ExportTool.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved
                            && ed.ExportTool.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool
                            && ed.ExportTool.ExportDate <= ReportDate
                            && ed.RealToolId != null && new_listRealToolId.Contains(ed.RealToolId.Value)
                            && new_toolIds.Contains(ed.ToolInventory.ToolId)
                            && ed.MachineId != null && machineIds.Contains(ed.MachineId.Value)
                            && ed.ProductId != null && productIds.Contains(ed.ProductId.Value))
                        .GroupBy(ed => new {
                            MachineId = ed.MachineId.Value,
                            ToolId = ed.ToolInventory.ToolId,
                            ProductId = ed.ProductId.Value,
                            RealtoolId = ed.RealToolId.Value,
                        })
                        .Select(g => g.OrderByDescending(t => t.ExportTool.ExportDate).FirstOrDefault())
                        .Select(ed => new {
                            MachineId = ed.MachineId.Value,
                            ToolId = ed.ToolInventory.ToolId,
                            ProductId = ed.ProductId.Value,
                            ExportDate = ed.ExportTool.ExportDate,
                            RealToolId = ed.RealToolId.Value,
                        })
                        .ToList();
                    var lastExportTools_productIds = lastExportTools.Select(ed => ed.ProductId).Distinct().ToList();
                    var startExportTool = lastExportTools.Min(ed => ed.ExportDate);
                    var productions = (from id in vfi.ProductInventoryPeriods   //vet ton kho san pham 
                                       where lastExportTools_productIds.Contains(id.ProductId) &&
                                             id.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                             id.Transaction.WarehouseIssueId == null &&
                                             id.PeriodDate >= startExportTool &&
                                             id.PeriodDate <= ReportDate
                                       select new {
                                           id.PeriodDate,
                                           id.ProductId,
                                           id.Quantity
                                       }).ToList();

                    // Step 6: Build model efficiently
                    
                        foreach (var machine in machines) {
                        var lastTrack = lastTracks.FirstOrDefault(x => x.MachineId == machine.MachineId);

                        if (lastTrack != null) {
                            var productionToolsById =
                                vfi.ProductionTools.Where(pt => pt.ProductId == lastTrack.ProductId && pt.Active).ToList();
                            foreach (var productionTool in productionToolsById) {

                                var entity = new ProductionToolModel {  
                                    ToolId = productionTool.ToolId,
                                    ToolCode = productionTool.Tool.ToolCode,
                                    ToolDesign = productionTool.Tool.ToolDesignNo,
                                    ToolFullCode = productionTool.Tool.ToolFullCode,
                                    ToolMaterial = productionTool.Tool.ToolMaterial,
                                    ToolProduction = productionTool.Tool.ToolProduction,
                                    ToolName = productionTool.Tool.ToolName,
                                    ToolTypeName = productionTool.Tool.MaterialType.MaterialTypeName,
                                    ProductId = lastTrack.ProductId,
                                    ProductCode = lastTrack.ProductCode,
                                    MachineName = lastTrack.MachineName,
                                    ToolQuota = productionTool.Quota,
                                    MachineId = lastTrack.MachineId,
                                    RealToolId = productionTool.RealToolId,                                                               // 19/01/2026
                                    Note = "Vị trí:" + productionTool.ToolLocation + " - " +                                                // 16/01/2026
                                    productionTool.Note,

                                };

                                    //ToolInv
                                var toolInvsById = toolInvs.Where(ti => ti.ToolId == entity.ToolId).ToList();
                                entity.ToolInv = toolInvsById.Sum(ti => ti.LastQuantity-ti.EarlyQuantity);


                                    //ProductionQuantity
                                var lastExport = lastExportTools.FirstOrDefault(t => t.ProductId == entity.ProductId 
                                                                                && t.MachineId == entity.MachineId
                                                                                && t.ToolId == entity.ToolId
                                                                                && t.RealToolId == entity.RealToolId);

                                if (lastExport != null) {
                                    var ProductQuantity = productions.Where(t => t.ProductId == entity.ProductId
                                                                        && t.PeriodDate > lastExport.ExportDate)
                                                                        .Select(x => (double?)x.Quantity)
                                                                        .DefaultIfEmpty(0)
                                                                        .Sum();
                                    if (lastExport.ExportDate.HasValue) {
                                        entity.ExportDate = lastExport.ExportDate.Value;
                                    }
                                    entity.ProductionQuantity = ProductQuantity ?? 0;
                                    entity.ToolRequiredColor = entity.ToolQuota > 0
                                          ? entity.ProductionQuantity / entity.ToolQuota > 1.1
                                              ? 1
                                              : entity.ProductionQuantity / entity.ToolQuota > 1.0
                                                  ? 2
                                                  : entity.ProductionQuantity / entity.ToolQuota > 0.9
                                                    ? 3
                                                    : entity.ProductionQuantity / entity.ToolQuota >= 0.75
                                                      ? 4
                                                    : 0
                                    : 1;
                                }

                                     //entity.ProductRequired
                                var product = products.FirstOrDefault(x => x.ProductId == entity.ProductId);

                                if (product != null) {
                                    entity.ProductInventory = product.Inventories ?? 0;
                                    entity.ProductForcast = product.Forecasts ?? 0;

                                }

                                entity.TheDay = ReportDate;


                                model.Add(entity);
                            }



                            //replaceTool
                            var replaceToolById = replaceTools.Where(pt => pt.ProductId == lastTrack.ProductId
                                            && pt.MachineId == lastTrack.MachineId).ToList();              //22/01/2026
                            foreach (var replacement in replaceToolById) {
                                var entity2 = new ProductionToolModel {
                                    ToolId = replacement.ToolId,
                                    ToolCode = replacement.toolCode,
                                    ToolDesign = replacement.toolDesign,
                                    ToolFullCode = replacement.toolFullCode,
                                    ToolMaterial = replacement.toolMaterial,
                                    ToolProduction = replacement.toolProduction,
                                    ToolName = replacement.toolName,
                                    ToolTypeName = replacement.toolTypeName,
                                    ProductId = lastTrack.ProductId,
                                    ProductCode = lastTrack.ProductCode,
                                    MachineName = lastTrack.MachineName,
                                    MachineId = lastTrack.MachineId,
                                    RealToolId = replacement.RealToolId,                                                        //21/01/2026
                                    Note = "CCTT - Vị trí: "                                                                // 13/01/2026
                                    ,

                                };
                                var productionTool = productionToolsById.FirstOrDefault(x => x.RealToolId == replacement.RealToolId);   // 13/01/2026
                                entity2.Quota = productionTool.Quota;
                                entity2.ToolLocation = productionTool.ToolLocation;

                                entity2.Note = entity2.Note + entity2.ToolLocation;

                                var toolInvsById = toolInvs.Where(ti => ti.ToolId == entity2.ToolId).ToList();
                                entity2.ToolInv = toolInvsById.Sum(ti => ti.LastQuantity - ti.EarlyQuantity);

                                var lastExport = lastExportTools.FirstOrDefault(x =>
                                                                                x.ProductId == entity2.ProductId
                                                                                && x.MachineId == entity2.MachineId
                                                                                && x.ToolId == entity2.ToolId                       //22/01/2026
                                                                                && x.RealToolId == entity2.RealToolId               //19/01/2026
                                                                                );

                                if (lastExport != null) {
                                    var ExportProductionQty = productions.Where(p => p.ProductId == entity2.ProductId
                                                                                && p.PeriodDate > lastExport.ExportDate)
                                                                         .Select(x => (double?)x.Quantity)
                                                                         .DefaultIfEmpty(0)
                                                                         .Sum();
                                    entity2.ProductionQuantity = ExportProductionQty ?? 0;


                                    entity2.ExportDate = lastExport.ExportDate;


                                    entity2.ToolRequiredColor = entity2.ToolQuota > 0
                                                   ? entity2.ProductionQuantity / entity2.ToolQuota > 1.1
                                                       ? 1
                                                       : entity2.ProductionQuantity / entity2.ToolQuota >= 1.0
                                                           ? 2
                                                           : entity2.ProductionQuantity / entity2.ToolQuota >= 0.9
                                                               ? 3
                                                               : entity2.ProductionQuantity / entity2.ToolQuota >= 0.75
                                                                     ? 4
                                                               : 0
                                                   : 1;
                                }


                                var product = products.FirstOrDefault(pt => pt.ProductId == entity2.ProductId);
                                if (product != null) {
                                    entity2.ProductForcast = product.Forecasts ?? 0;
                                    entity2.ProductInventory = product.Inventories ?? 0;
                                }
                                entity2.TheDay = ReportDate;                                     // 16/01/2026

                                model.Add(entity2);
                            }
                            if (productionToolsById.Count ==0) {
                                var entity = new ProductionToolModel {
                                    ProductId = lastTrack.ProductId,
                                    ProductCode = lastTrack.ProductCode,
                                    MachineName = lastTrack.MachineName,
                                };
                                model.Add(entity);
                            }
                        }
                        else {
                            var entity = new ProductionToolModel {
                                MachineName = machine.MachineName,
                            };
                            model.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                return Json("Lỗi" + ex.Message);
            }
            return PartialView("PageAssignProductionTool",
                model.OrderBy(m => m.MachineName)
                    .ThenBy(m => m.ProductCode)
                    .ThenBy(m => m.ToolTypeName)
                    .ThenBy(m => m.ToolName)
                    .ThenBy(m => m.ToolDesign)
                    .ThenBy(m => m.ToolMaterial)
                    .ThenBy(m => m.ToolProduction)
                    .ToList());
        }







        // code goc


        public ActionResult PrintProductionTool() {
            var model = new List<ProductionToolModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var approvedStatus = (byte)MyUtilities.Transaction.Status.Approved;

                    var ReportDate = DateTime.Today;

                    // Step 1: Load all active machines
                    var machines = vfi.Machines
                        .Where(m => (m.Active || m.Production2)
                        //&& m.MachineName.Equals("C11")
                        && m.ProcessingType.ForWarehouseId == MyUtilities.Warehouse.Production1
                        && m.StateId != MyUtilities.Machine.State.Done
                        && m.StateId != MyUtilities.Machine.State.OutOfMaterial
                        && m.StateId != MyUtilities.Machine.State.BadState
                        )
                        .ToList();
                    var machineIds = machines.Select(m => m.MachineId).ToList();

                    // Step 2: Get latest approved TrackUpMachine per machine
                    var lastTracks = vfi.TrackUpMachines
                        .Where(t => machineIds.Contains(t.MachineId) &&
                                    t.Status == approvedStatus &&
                                    t.Product.Active &&
                                    t.DeliveryDate != null)
                        .GroupBy(t => t.MachineId)
                        .Select(g => g.OrderByDescending(t => t.DeliveryDate).FirstOrDefault())
                        .Select(t => new {
                            t.MachineId,
                            MachineName = t.Machine.MachineName,
                            t.ProductId,
                            ProductCode = t.Product.ProductCode,
                            Date = t.DeliveryDate.Value,
                            TrackId = t.TrackId,
                        })
                        .ToList();
                    //var machine_ids_track = lastTracks.Select(x => x.MachineId).Distinct().ToList();                  //22/01/2026 tat

                    // Step 3: Get all relevant product IDs
                    var productIds = lastTracks.Select(t => t.ProductId).Distinct().ToList();

                    // Step 4: Load all active production tools for those products
                    var productionTools = vfi.ProductionTools
                        .Where(pt => productIds.Contains(pt.ProductId) && pt.Tool.Active && pt.Active && pt.Active)
                        //.GroupBy(pt => new { pt.ProductId, pt.ToolId })
                        //.Select(pt => new {

                        //    ProductId = pt.Key.ProductId,
                        //    ToolId = pt.Key.ToolId,
                        //    Quota = pt.Sum(x => x.Quota)
                        //})
                        .ToList();


                    //var trackIds = lastTracks.Select(t => t.TrackId).Distinct().ToList();                      // 13/01/2026 (bo do khong can`)

                    var replaceTools = vfi.ProductionToolReplacements                                           // 13/01/2026
                        .Where(rp => rp.ToolId != null
                        && productIds.Contains(rp.TrackUpMachine.ProductId)
                        && machineIds.Contains(rp.TrackUpMachine.MachineId)
                        )
                        .Select(rp => new {
                            toolCode = rp.Tool.ToolCode,
                            toolDesign = rp.Tool.ToolDesignNo,
                            toolFullCode = rp.Tool.ToolFullCode,
                            toolMaterial = rp.Tool.ToolMaterial,
                            toolProduction = rp.Tool.ToolProduction,
                            toolName = rp.Tool.ToolName,
                            toolTypeName = rp.Tool.MaterialType.MaterialTypeName,
                            note = rp.Note,
                            ProductId = rp.TrackUpMachine.ProductId,
                            ToolId = rp.ToolId.Value,
                            Active = true,
                            trackId = rp.TrackId,
                            RealToolId = rp.ProductionToolId,
                            MachineId = rp.TrackUpMachine.MachineId,

                        })
                        .ToList();



                    var realToolIdList = productionTools.Select(rt => rt.RealToolId).ToList();                    // 19/01/2026
                    var replaceRealToolId = replaceTools.Select(rt => rt.RealToolId).ToList();

                    realToolIdList.AddRange(replaceRealToolId);
                    var new_realToolIdList = realToolIdList.ToList();





                    // Step 5: Load all active tools with inventory
                    var replaceToolIds = replaceTools.Select(x => x.ToolId).Distinct().ToList();                 // 13/01/2026
                    var toolIds = productionTools.Select(x => x.ToolId).Distinct().ToList();

                    toolIds.AddRange(replaceToolIds);
                    var new_toolIds = toolIds.Distinct().ToList();                                                     // danh sach co ca tool + replace tool   13/01/2026

                    var toolInvs = vfi.ToolInventories                                                              // danh sach ton` kho cua new_toolId    13/01/2026
                        .Where(ti => new_toolIds.Contains(ti.ToolId) && ti.TotalQuantity > 0)
                        .ToList();

                    var warehouseIds = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity2();
                    var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    var endDate = startDate.AddMonths(3);
                    var products = vfi.Products
                                    .Where(p => productIds.Contains(p.ProductId))
                                    .Select(p => new {
                                        ProductId = p.ProductId,
                                        Forecasts = vfi.ForecastOrders
                                           .Where(f => f.ProductId == p.ProductId && f.ForecastDate >= startDate && f.ForecastDate < endDate)
                                           .Select(f => (double?)f.Quantity)
                                           .DefaultIfEmpty(0)
                                           .Sum(),
                                        Inventories = vfi.ProductInventories
                                            .Where(inv => inv.ProductId == p.ProductId && warehouseIds.Contains(inv.WarehouseId))
                                            .Select(inv => (double?)(inv.TotalQty))
                                            .DefaultIfEmpty(0)
                                            .Sum()
                                    })
                                    .ToList();


                    //var lastExportReplaceTools = vfi.ExportToolDetails
                    //    .Where(ed =>
                    //           ed.ExportTool.TransactionFpt.Type == (byte)MyUtilities.Tool.ExportType.Production
                    //           && ed.ExportTool.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved
                    //           && ed.ExportTool.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool
                    //           && replaceToolIds.Contains(ed.ToolInventory.ToolId)
                    //           && ed.MachineId != null && machineIds.Contains(ed.MachineId.Value)
                    //           && ed.ProductId != null && productIds.Contains(ed.ProductId.Value))
                    //    .GroupBy(ed => new {
                    //             MachineId = ed.MachineId.Value,
                    //             ToolId = ed.ToolInventory.ToolId,
                    //             ProductId = ed.ProductId.Value,
                    //           })
                    //    .Select(g => g.OrderByDescending(t => t.ExportTool.ExportDate).FirstOrDefault())
                    //    .Select(ed => new {
                    //            MachineId = ed.MachineId.Value,
                    //            ToolId = ed.ToolInventory.ToolId,
                    //            ProductId = ed.ProductId.Value,
                    //            ExportDate = ed.ExportTool.ExportDate,
                    //            })
                    //   .ToList();








                    var lastExportTools = vfi.ExportToolDetails
                        .Where(ed =>
                            ed.ExportTool.TransactionFpt.Type == (byte)MyUtilities.Tool.ExportType.Production
                            && ed.ExportTool.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved
                            && ed.ExportTool.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Tool
                            && ed.ExportTool.ExportDate < DateTime.Now                                                                      // 22/01/2026
                            && new_toolIds.Contains(ed.ToolInventory.ToolId)                                                                // thay toolIds = new_toolIds (13/01/2026)
                            && ed.RealToolId != null && new_realToolIdList.Contains(ed.RealToolId.Value)                                    // 19/01/2026
                            && ed.MachineId != null && machineIds.Contains(ed.MachineId.Value)
                            && ed.ProductId != null && productIds.Contains(ed.ProductId.Value))
                        .GroupBy(ed => new {
                            MachineId = ed.MachineId.Value,
                            ToolId = ed.ToolInventory.ToolId,
                            ProductId = ed.ProductId.Value,
                            RealToolId = ed.RealToolId.Value,                                                                                //22/01/2026
                        })
                        .Select(g => g.OrderByDescending(t => t.ExportTool.ExportDate).FirstOrDefault())
                        .Select(ed => new {
                            MachineId = ed.MachineId.Value,
                            ToolId = ed.ToolInventory.ToolId,
                            ProductId = ed.ProductId.Value,
                            ExportDate = ed.ExportTool.ExportDate,
                            RealToolId = ed.RealToolId.Value,                                                                                // 19/01/2026
                        })
                        .ToList();






                    var lastExportTools_productIds = lastExportTools.Select(ed => ed.ProductId).Distinct().ToList();
                    var startExportTool = lastExportTools.Min(ed => ed.ExportDate);
                    var productions = (from id in vfi.ProductInventoryPeriods
                                       where lastExportTools_productIds.Contains(id.ProductId) &&
                                             id.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                             id.Transaction.WarehouseIssueId == null &&
                                             id.PeriodDate >= startExportTool
                                       select new {
                                           id.PeriodDate,
                                           id.ProductId,
                                           id.Quantity
                                       }).ToList();


                    // Step 6: Build model efficiently

                    //model = (from pt in productionTools
                    //         join track in lastTracks on pt.ProductId equals track.ProductId
                    //         let toolInvQty = toolInvs.Where(ti => ti.ToolId == pt.ToolId).Sum(ti => ti.TotalQuantity)
                    //         let ForecastQuantity = .FirstOrDefault(x => x.ProductId == pt.ProductId).Forecasts
                    //         let ProductInvQuantity = products.FirstOrDefault(x => x.ProductId == pt.ProductId).Inventories
                    //join lastExport in lastExportTools on new { pt.ProductId, pt.ToolId, track.MachineId } equals new { lastExport.ProductId, lastExport.ToolId, lastExport.MachineId }
                    //let ExportProductionQty = productions.Where(p => p.ProductId == pt.ProductId && p.PeriodDate > lastExport.ExportDate)
                    //                                     .Select(x => (double?)x.Quantity).DefaultIfEmpty(0).Sum()
                    //         select new ProductionToolModel {
                    //             ToolId = pt.ToolId,
                    //             ToolCode = pt.Tool.ToolCode,
                    //             ToolDesign = pt.Tool.ToolDesignNo,
                    //             ToolFullCode = pt.Tool.ToolFullCode,
                    //             ToolMaterial = pt.Tool.ToolMaterial,
                    //             ToolProduction = pt.Tool.ToolProduction,
                    //             ToolName = pt.Tool.ToolName,
                    //             ToolTypeName = pt.Tool.MaterialType.MaterialTypeName,
                    //             ProductId = track.ProductId,
                    //             ProductCode = track.ProductCode,
                    //             MachineName = track.MachineName,
                    //             ToolInv = toolInvQty,
                    //             ToolQuota = pt.Quota,
                    //             Note = pt.Description,
                    //             ProductForcast = ForecastQuantity ?? 0.0,
                    //             ProductInventory = ProductInvQuantity ?? 0.0,
                    //             ProductionQuantity = ExportProductionQty ?? 0.0,
                    //             ToolRequiredColor = pt.Quota > 0
                    //                         ? ExportProductionQty / pt.Quota > 1.2
                    //                             ? 1 
                    //                             : ExportProductionQty / pt.Quota > 1.1
                    //                                 ? 2
                    //                                 : ExportProductionQty / pt.Quota > 1
                    //                                     ? 3
                    //                                     : 0
                    //                         : 1,
                    //         }).ToList();


                    //var machine_ids_model = model.Select(x => x.MachineName).Distinct().ToList();

                    //var toolInvs = vfi.ToolInventories.Where(ti => ti.Tool.Active && ti.TotalQuantity > 0);

                    //var machines = from m in vfi.Machines
                    //               where m.Active || m.Production2
                    //               select m;
                    foreach (var machine in machines) {
                        var lastTrack = lastTracks.FirstOrDefault(x => x.MachineId == machine.MachineId);
                        //(from t in vfi.TrackUpMachines
                        // where
                        // t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                        // t.MachineId == machine.MachineId &&
                        // t.Product.Active &&
                        // t.DeliveryDate != null
                        // select new {
                        //     t.MachineId,
                        //     t.Machine.MachineName,
                        //     t.ProductId,
                        //     t.Product.ProductCode,
                        //     Date = t.DeliveryDate.Value
                        // })
                        //.OrderByDescending(t => t.Date)
                        //.FirstOrDefault();

                        if (lastTrack != null) {

                            var productionToolsById = productionTools.Where(pt => pt.ProductId == lastTrack.ProductId).ToList();

                            foreach (var productionTool in productionToolsById) {
                                //var entity =
                                //    model.FirstOrDefault(
                                //        m =>
                                //            m.ToolId == productionTool.ToolId && m.ProductId == productionTool.ProductId);
                                //if (entity == null) {
                                var entity = new ProductionToolModel {
                                    ToolId = productionTool.ToolId,
                                    ToolCode = productionTool.Tool.ToolCode,
                                    ToolDesign = productionTool.Tool.ToolDesignNo,
                                    ToolFullCode = productionTool.Tool.ToolFullCode,
                                    ToolMaterial = productionTool.Tool.ToolMaterial,
                                    ToolProduction = productionTool.Tool.ToolProduction,
                                    ToolName = productionTool.Tool.ToolName,
                                    ToolTypeName = productionTool.Tool.MaterialType.MaterialTypeName,
                                    ProductId = lastTrack.ProductId,
                                    ProductCode = lastTrack.ProductCode,
                                    MachineName = lastTrack.MachineName,
                                    MachineId = lastTrack.MachineId,
                                    ToolQuota = productionTool.Quota,
                                    RealToolId = productionTool.RealToolId,                                                               // 19/01/2026
                                    Note = "Vị trí:" + productionTool.ToolLocation + " - " +                                                // 16/01/2026
                                    productionTool.Note,

                                };
                                var toolInvsById = toolInvs.Where(ti => ti.ToolId == entity.ToolId).ToList();
                                entity.ToolInv = toolInvsById.Sum(ti => ti.TotalQuantity);




                                var lastExport = lastExportTools.FirstOrDefault(x => x.ProductId == entity.ProductId
                                                                                  && x.MachineId == entity.MachineId
                                                                                  && x.ToolId == entity.ToolId                               //22/01/2026
                                                                                  && x.RealToolId == entity.RealToolId                       //19/01/2026
                                                                                );

                                if (lastExport != null) {
                                    var ExportProductionQty = productions.Where(p => p.ProductId == entity.ProductId
                                                                                && p.PeriodDate > lastExport.ExportDate)
                                                                         .Select(x => (double?)x.Quantity)
                                                                         .DefaultIfEmpty(0)
                                                                         .Sum();
                                    entity.ProductionQuantity = ExportProductionQty ?? 0;


                                    entity.ExportDate = lastExport.ExportDate;


                                    // to^ mau`
                                    entity.ToolRequiredColor = entity.ToolQuota > 0
                                                   ? entity.ProductionQuantity / entity.ToolQuota > 1.1
                                                       ? 1
                                                       : entity.ProductionQuantity / entity.ToolQuota > 1.0
                                                           ? 2
                                                           : entity.ProductionQuantity / entity.ToolQuota > 0.9
                                                               ? 3
                                                               : entity.ProductionQuantity / entity.ToolQuota >= 0.75
                                                                     ? 4
                                                               : 0
                                                   : 1;
                                }

                                //entity.ProductRequired = productForcast - productInventory
                                //entity.ProductForcast = products.FirstOrDefault(pt => pt.ProductId == entity.ProductId.Forecasts ?? 0;
                                var product = products.FirstOrDefault(pt => pt.ProductId == entity.ProductId);
                                if (product != null) {
                                    entity.ProductForcast = product.Forecasts ?? 0;
                                    entity.ProductInventory = product.Inventories ?? 0;
                                }

                                entity.TheDay = DateTime.Now;                                     // 16/01/2026

                                model.Add(entity);
                                //else {
                                //    //if (!entity.ProductCode.Contains(lastTrack.ProductCode))
                                //    //    entity.ProductCode += lastTrack.ProductCode + ",";
                                //    if (!entity.MachineName.Contains(lastTrack.MachineName))
                                //        entity.MachineName += lastTrack.MachineName + ",";
                                //}
                            }
                            //var productionToolsById = productionTools.Where(pt => pt.ProductId == lastTrack.ProductId).ToList();         
                            //foreach (var productionTool in productionToolsById) {


                            // replacementTool

                            

                            var replaceToolById = replaceTools.Where(pt => pt.ProductId == lastTrack.ProductId
                                                                        && pt.MachineId == lastTrack.MachineId).ToList();              //22/01/2026
                            foreach (var replacement in replaceToolById) {
                                var entity2 = new ProductionToolModel {
                                    ToolId = replacement.ToolId,
                                    ToolCode = replacement.toolCode,
                                    ToolDesign = replacement.toolDesign,
                                    ToolFullCode = replacement.toolFullCode,
                                    ToolMaterial = replacement.toolMaterial,
                                    ToolProduction = replacement.toolProduction,
                                    ToolName = replacement.toolName,
                                    ToolTypeName = replacement.toolTypeName,
                                    ProductId = lastTrack.ProductId,
                                    ProductCode = lastTrack.ProductCode,
                                    MachineName = lastTrack.MachineName,
                                    MachineId = lastTrack.MachineId,
                                    RealToolId = replacement.RealToolId,                                                        //21/01/2026

                                    Note = "CCTT - Vị trí: "                                                                // 13/01/2026

                                    ,

                                };
                                var productionTool = productionToolsById.FirstOrDefault(x => x.RealToolId == replacement.RealToolId);   // 13/01/2026
                                entity2.Quota = productionTool.Quota;
                                entity2.ToolLocation = productionTool.ToolLocation;

                                entity2.Note = entity2.Note + entity2.ToolLocation;

                                var toolInvsById = toolInvs.Where(ti => ti.ToolId == entity2.ToolId).ToList();
                                entity2.ToolInv = toolInvsById.Sum(ti => ti.TotalQuantity);

                                var lastExport = lastExportTools.FirstOrDefault(x =>
                                                                                x.ProductId == entity2.ProductId
                                                                                && x.MachineId == entity2.MachineId
                                                                                && x.ToolId == entity2.ToolId                       //22/01/2026
                                                                                && x.RealToolId == entity2.RealToolId               //19/01/2026
                                                                                );

                                if (lastExport != null) {
                                    var ExportProductionQty = productions.Where(p => p.ProductId == entity2.ProductId
                                                                                && p.PeriodDate > lastExport.ExportDate)
                                                                         .Select(x => (double?)x.Quantity)
                                                                         .DefaultIfEmpty(0)
                                                                         .Sum();
                                    entity2.ProductionQuantity = ExportProductionQty ?? 0;


                                    entity2.ExportDate = lastExport.ExportDate;


                                    entity2.ToolRequiredColor = entity2.ToolQuota > 0
                                                   ? entity2.ProductionQuantity / entity2.ToolQuota > 1.1
                                                       ? 1
                                                       : entity2.ProductionQuantity / entity2.ToolQuota >= 1.0
                                                           ? 2
                                                           : entity2.ProductionQuantity / entity2.ToolQuota >= 0.9
                                                               ? 3
                                                               : entity2.ProductionQuantity / entity2.ToolQuota >= 0.75
                                                                     ? 4
                                                               : 0
                                                   : 1;
                                }


                                var product = products.FirstOrDefault(pt => pt.ProductId == entity2.ProductId);
                                if (product != null) {
                                    entity2.ProductForcast = product.Forecasts ?? 0;
                                    entity2.ProductInventory = product.Inventories ?? 0;
                                }
                                entity2.TheDay = DateTime.Now;                                     // 16/01/2026

                                model.Add(entity2);
                            }


                            if (productionToolsById.Count == 0) {
                                var entity = new ProductionToolModel {
                                    ProductId = lastTrack.ProductId,
                                    ProductCode = lastTrack.ProductCode,
                                    MachineName = lastTrack.MachineName,
                                };
                                model.Add(entity);
                            }
                        }
                        else {
                            var entity = new ProductionToolModel {
                                MachineName = machine.MachineName,
                            };
                            model.Add(entity);
                        }
                    }

                    //var productids = model.select(x => x.productid).distinct().tolist();
                    //var toolids = model.select(x => x.toolid).distinct().tolist();
                    //var startdate = new datetime(datetime.today.year, datetime.today.month, 1);
                    //var enddate = startdate.addmonths(3);
                    ////var forecasts = vfi.forecastorders.where(x => productids.contains(x.productid) && x.forecastdate >= startdate && x.forecastdate < enddate).select(x => new { x.productid, x.quantity }); ;
                    //var warehouseids = myutilities.warehouse.getwarehouseid_sumtotalquantity2();
                    ////var productinvs = vfi.productinventories.where(x => productids.contains(x.productid) && warehouseids.contains(x.warehouseid)).select(x => new { x.productid, x.totalqty });
                    ////var productiontoolds = vfi.productiontools.where(x => productids.contains(x.productid) && toolids.contains(x.toolid)).tolist();
                    ////var getinonecall = vfi.products.where(x => productids.contains(x.productid));

                }
            }
            catch (Exception ex) {
                return Json("Lỗi" + ex.Message);
            }
            return PartialView("PageAssignProductionTool",
                model.OrderBy(m => m.MachineName)
                    .ThenBy(m => m.ProductCode)
                    .ThenBy(m => m.ToolTypeName)
                    .ThenBy(m => m.ToolName)
                    .ThenBy(m => m.ToolDesign)
                    .ThenBy(m => m.ToolMaterial)
                    .ThenBy(m => m.ToolProduction)
                    .ToList());
        }

        [GridAction]
        public ActionResult SelectToolInventory(int toolId) {
            var model = new List<TransactionFptDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var tool = vfi.Tools.FirstOrDefault(t => t.ToolId == toolId);
                    if (tool == null)
                        throw new AggregateException("Không tìm thấy công cụ!");
                    var toolInvs = vfi.ToolInventories.Where(ti => ti.ToolId == toolId && ti.TotalQuantity > 0);
                    foreach (var toolInventory in toolInvs) {
                        var entity = new TransactionFptDetailModel {
                            LotNumber = toolInventory.LotNumber,
                            VendorName = toolInventory.Vendor.VendorName,
                            TotalInv = toolInventory.TotalQuantity,
                            UnitMeasure = toolInventory.UnitMeasure
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectToolInventory", ex.Message);
            }

            return View(new GridModel(model));
        }


        public Product ProductRequired { get; set; }

        public DateTime startExportToolproduct { get; set; }

        public int ProductId { get; set; }

        public int MachineId { get; set; }

        public long trackId { get; set; }
    }
}
