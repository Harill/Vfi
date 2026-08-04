using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;

namespace Vfi.Ui.Mvc.Vfi.Areas.Factory.Controllers {
    public class DefectController : Controller {
        //
        // GET: /Factory/Defect/
        #region view

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
        public ActionResult TransactionDefectApprove() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult TransactionDefectDetailAdd() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult TransactionDefectManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductionDefectTypeManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductionDefectManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductionDefectRemedyManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult TransactionDefectReport() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        #endregion

        #region data item

        #region defect type
        
        [GridAction]
        public ActionResult SelectProductionDefectType() {
            var model = new List<ProductionDefectTypeModel>();
            try {
                model = GetProductionDefectTypeModels();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionDefectType", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionDefectType(ProductionDefectTypeModel insertModel) {
            try {
                using (var vfi = new vfiContext()) {
                    var newM = new Vfi.Models.ProductionDefectType {
                        Active = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        DefectTypeCode = insertModel.DefectTypeCode.Trim(),
                        DefectTypeName = insertModel.DefectTypeName.Trim(),
                        Description = (insertModel.Description + "").Trim(),
                    };
                    vfi.ProductionDefectTypes.Add(newM);
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("InsertProductionDefectType", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }

            return View(new GridModel(GetProductionDefectTypeModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionDefectType(ProductionDefectTypeModel updateModel) {
            try {
                using (var vfi = new vfiContext()) {
                    var update = vfi.ProductionDefectTypes.FirstOrDefault(mt => mt.DefectTypeId == updateModel.DefectTypeId);
                    update.Active = updateModel.Active;
                    update.ModifiedUser = HttpContext.User.Identity.Name;
                    update.ModifiedDate = DateTime.Now;
                    update.DefectTypeCode = updateModel.DefectTypeCode.Trim();
                    update.DefectTypeName = updateModel.DefectTypeName.Trim();
                    update.Description = (updateModel.Description + "").Trim();
                    vfi.SaveChanges();
                }

            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateProductionDefectType", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }
            return View(new GridModel(GetProductionDefectTypeModels()));
        }

        public List<ProductionDefectTypeModel> GetProductionDefectTypeModels() {
            var model = new List<ProductionDefectTypeModel>();
            try {
                using (var vfi = new vfiContext()) {
                    model = vfi.ProductionDefectTypes.Select(
                        entity => new ProductionDefectTypeModel {
                            DefectTypeId = entity.DefectTypeId,
                            DefectTypeCode = entity.DefectTypeCode,
                            DefectTypeName = entity.DefectTypeName,
                            Description = entity.Description,
                            ModifiedUser = entity.ModifiedUser,
                            ModifiedDate = entity.ModifiedDate,
                            Active = entity.Active,
                        }).ToList();

                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex);
            }
            return model.OrderBy(x => x.DefectTypeCode).ToList();
        }

        public ActionResult SelectComboBoxDefectType() {
            var model = new List<ProductionDefectTypeModel>();
            using (var vfi = new vfiContext()) {
                var entities = vfi.ProductionDefectTypes.Where(x => x.Active).OrderBy(x => x.DefectTypeCode);
                model = entities.Select(
                        entity => new ProductionDefectTypeModel {
                            DefectTypeId = entity.DefectTypeId,
                            DefectTypeCode = entity.DefectTypeCode,
                            DefectTypeName = entity.DefectTypeName,
                        }).ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "DefectTypeId", "DefectTypeName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #region defect

        [GridAction]
        public ActionResult SelectProductionDefect(int customerId, string productCode) {
            var model = new List<ProductionDefectModel>();
            try {
                model = GetProductionDefectModels(customerId, productCode);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionDefect", ex.Message);
            }
            return View(new GridModel(model));
        }


        public List<ProductionDefectModel> GetProductionDefectModels(int customerId, string productCode) {
            var model = new List<ProductionDefectModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var productIds = new List<int>();
                    if (customerId != 0 || !string.IsNullOrWhiteSpace(productCode)) {
                        var products = vfi.Products.Where(x => customerId == 0 || x.CustomerId == customerId);
                        if (!string.IsNullOrWhiteSpace(productCode)) {
                            products = products.Where(x => x.ProductCode.Contains(productCode));
                        }
                        productIds = products.Select(x => x.ProductId).Distinct().ToList();
                    }
                    else return model;
                    var productionDefects = vfi.ProductionDefects.ToList();
                    if (productIds.Count > 0) {
                        productionDefects = productionDefects.Where(x => productIds.Contains(x.ProductId)).ToList();
                    }

                    model = productionDefects.Select(
                        entity => new ProductionDefectModel {
                            DefectId = entity.DefectId,
                            DefectCode = entity.DefectCode,
                            DefectName = entity.DefectName,
                            ProductId = entity.ProductId,
                            ProductCode = entity.Product.ProductCode,
                            DefectTypeId = entity.DefectTypeId,
                            DefectTypeCode = entity.ProductionDefectType.DefectTypeCode,
                            DefectTypeName = entity.ProductionDefectType.DefectTypeName,
                            Description = entity.Description,
                            ModifiedUser = entity.ModifiedUser,
                            ModifiedDate = entity.ModifiedDate,
                            Active = entity.Active,
                            IsUsing = entity.DefectTransactionDetails.Any(),
                            DefaultRemedyId = entity.DefaultRemedyId ?? 0,
                            RemedyName = entity.DefaultRemedyId != null ? entity.ProductionDefectRemedy.RemedyName : "",
                        }).ToList();
                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex);
            }
            return model.OrderBy(x => x.ProductCode).ThenBy(x => x.DefectName).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionDefect(ProductionDefectModel insertModel) {
            try {
                using (var vfi = new vfiContext()) {

                    int typeId = 1;
                    try {
                        typeId = Convert.ToInt32(insertModel.DefectTypeCode);
                    }
                    catch (FormatException) {
                        typeId =
                            vfi.ProductionDefectTypes.FirstOrDefault(c => c.DefectTypeCode.Equals(insertModel.DefectTypeCode)).DefectTypeId;
                    }
                    int productId = 1;
                    try {
                        productId = Convert.ToInt32(insertModel.ProductCode);
                    }
                    catch (FormatException) {
                        productId =
                            vfi.Products.FirstOrDefault(c => c.ProductCode.Equals(insertModel.ProductCode)).ProductId;
                    }
                    if (string.IsNullOrWhiteSpace(insertModel.DefectCode) || string.IsNullOrWhiteSpace(insertModel.DefectName) || typeId == 0 || productId == 0) {
                        throw new AggregateException("Lỗi giá trị nhập | DefectCode | DefectName | Type | Product");
                    }
                    var entity = vfi.ProductionDefects.FirstOrDefault(x => x.ProductId == productId
                                                                        && x.DefectTypeId == typeId
                                                                        && x.DefectName.Equals(insertModel.DefectName.Trim()));
                    if (entity != null) {
                        throw new AggregateException("Giá trị nhập đã tồn tại !" + insertModel.DefectCode.Trim());
                    }
                    var newM = new ProductionDefect {
                        Active = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        DefectCode = insertModel.DefectCode.Trim(),
                        DefectName = insertModel.DefectName.Trim(),
                        Description = (insertModel.Description + "").Trim(),
                        DefectTypeId = typeId,
                        ProductId = productId,
                    };
                    if (!string.IsNullOrWhiteSpace(insertModel.RemedyName)) {
                        try {
                            newM.DefaultRemedyId = Convert.ToInt32(insertModel.RemedyName);
                        }
                        catch (FormatException) { }
                    }
                    vfi.ProductionDefects.Add(newM);
                    vfi.SaveChanges();
                }

            }
            catch (Exception exception) {
                ModelState.AddModelError("InsertProductionDefect", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }

            return View(new GridModel(GetProductionDefectModels(0,"")));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionDefect(ProductionDefectModel updateModel) {
            try {
                using (var vfi = new vfiContext()) {
                    var update = vfi.ProductionDefects.FirstOrDefault(mt => mt.DefectId == updateModel.DefectId);

                    int typeId = update.DefectTypeId;
                    try {
                        typeId = Convert.ToInt32(updateModel.DefectTypeCode);
                    }
                    catch (FormatException) { }
                    int productId = update.ProductId;
                    if (!string.IsNullOrWhiteSpace(updateModel.ProductCode)) {
                        try {
                            productId = Convert.ToInt32(updateModel.ProductCode);
                        }
                        catch (FormatException) { }
                    }
                    var entity = vfi.ProductionDefects.FirstOrDefault(x => x.DefectId != updateModel.DefectId
                                                                        && x.ProductId == productId
                                                                        && x.DefectTypeId == typeId
                                                                        && x.DefectName.Equals(updateModel.DefectName.Trim()));
                    if (entity != null) {
                        throw new AggregateException("Giá trị nhập đã tồn tại !" + updateModel.DefectCode.Trim());
                    }
                    var isUse = vfi.DefectTransactionDetails.Any(x => x.DefectId == update.DefectId);
                    var isManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.TechicalManagerLv1);
                    if (!isManager && isUse && update.ProductId != productId) {
                        throw new AggregateException("Lỗi! Không thể cập nhật do lỗi này đã được sử dụng.");
                    }
                    update.Active = updateModel.Active;
                    update.ModifiedUser = HttpContext.User.Identity.Name;
                    update.ModifiedDate = DateTime.Now;
                    update.DefectCode = updateModel.DefectCode.Trim();
                    update.DefectName = updateModel.DefectName.Trim();
                    update.Description = (updateModel.Description + "").Trim();
                    update.ProductId = productId;
                    update.DefectTypeId = typeId;
                    if (!string.IsNullOrWhiteSpace(updateModel.RemedyName)) {
                        try {
                            update.DefaultRemedyId = Convert.ToInt32(updateModel.RemedyName);
                        }
                        catch (FormatException) { }
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateProductionDefect", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }
            return View(new GridModel(GetProductionDefectModels(0, "")));
        }

        public ActionResult SelectComboBoxProductionDefect() {
            var model = new List<ProductionDefectModel>();
            using (var vfi = new vfiContext()) {
                var entities = vfi.ProductionDefects.Where(x => x.Active).OrderBy(x => x.DefectName);
                model = entities.Select(
                        entity => new ProductionDefectModel {
                            DefectId = entity.DefectId,
                            DefectCode = entity.DefectCode,
                            DefectName = entity.DefectName,
                        }).ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "DefectId", "DefectCode"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxProductionDefectNameRecommend() {
            var model = new List<string>();
            using (var vfi = new vfiContext()) {
                model = vfi.ProductionDefects
                    .Where(x => x.Active)
                    .OrderBy(x => x.DefectName)
                    .Select(x=> x.DefectName)
                    .Distinct()
                    .ToList();
                    
            }
            return new JsonResult {
                Data = new SelectList(model),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #region defect remedy
        [GridAction]
        public ActionResult SelectProductionDefectRemedy() {
            var model = new List<ProductionDefectRemedyModel>();
            try {
                model = GetProductionDefectRemedyModels();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionDefectRemedy", ex.Message);
            }
            return View(new GridModel(model));
        }

        public List<ProductionDefectRemedyModel> GetProductionDefectRemedyModels() {
            var model = new List<ProductionDefectRemedyModel>();
            try {
                using (var vfi = new vfiContext()) {
                    foreach (var item in vfi.ProductionDefectRemedies) {
                        var entity = new ProductionDefectRemedyModel {
                            RemedyId = item.RemedyId,
                            RemedyCode = item.RemedyCode,
                            RemedyName = item.RemedyName,
                            Description = item.Description,
                            ModifiedUser = item.ModifiedUser,
                            ModifiedDate = item.ModifiedDate,
                            Active = item.Active,
                            ProcessWarehouseId = item.ProcessWarehouseId ?? 0
                        };
                        if (entity.ProcessWarehouseId > 0) {
                            var warehouse = vfi.Warehouses.FirstOrDefault(x => x.WarehouseId == entity.ProcessWarehouseId);
                            entity.ProcessWarehouseName = warehouse.WarehouseName;
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex);
            }
            return model.OrderBy(x => x.RemedyCode).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionDefectRemedy(ProductionDefectRemedyModel insertModel) {
            try {
                using (var vfi = new vfiContext()) {
                    var newM = new ProductionDefectRemedy {
                        Active = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        RemedyCode = insertModel.RemedyCode.Trim(),
                        RemedyName = insertModel.RemedyName.Trim(),
                        Description = (insertModel.Description + "").Trim(),
                    };
                    if (!string.IsNullOrWhiteSpace(insertModel.ProcessWarehouseName)) {
                        try {
                            newM.ProcessWarehouseId = Convert.ToInt32(insertModel.ProcessWarehouseName);
                        }
                        catch (FormatException) { }
                    }
                    vfi.ProductionDefectRemedies.Add(newM);
                    vfi.SaveChanges();
                }

            }
            catch (Exception exception) {
                ModelState.AddModelError("InsertProductionDefectRemedy", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }

            return View(new GridModel(GetProductionDefectRemedyModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionDefectRemedy(ProductionDefectRemedyModel updateModel) {
            try {
                using (var vfi = new vfiContext()) {
                    var update = vfi.ProductionDefectRemedies.FirstOrDefault(mt => mt.RemedyId == updateModel.RemedyId);
                    update.Active = updateModel.Active;
                    update.ModifiedUser = HttpContext.User.Identity.Name;
                    update.ModifiedDate = DateTime.Now;
                    update.RemedyCode = updateModel.RemedyCode.Trim();
                    update.RemedyName = updateModel.RemedyName.Trim();
                    update.Description = (updateModel.Description + "").Trim();
                    if (!string.IsNullOrWhiteSpace(updateModel.ProcessWarehouseName)) {
                        try {
                            update.ProcessWarehouseId = Convert.ToInt32(updateModel.ProcessWarehouseName);
                        }
                        catch (FormatException) { }
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateProductionDefectRemedy", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }
            return View(new GridModel(GetProductionDefectRemedyModels()));
        }

        public ActionResult SelectComboBoxDefectRemedy() {
            var model = new List<ProductionDefectRemedyModel>();
            using (var vfi = new vfiContext()) {
                var entities = vfi.ProductionDefectRemedies.Where(x => x.Active).OrderBy(x => x.RemedyCode);
                model = entities.Select(
                        entity => new ProductionDefectRemedyModel {
                            RemedyId = entity.RemedyId,
                            RemedyCode = entity.RemedyCode,
                            RemedyName = entity.RemedyName,
                        }).ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "RemedyId", "RemedyName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #endregion

        #region data business

        public ActionResult SelectComboBoxImportTransaction() {
            var model = new List<TransactionModel>();
            using (var vfi = new vfiContext()) {
                var transactions =
                    vfi.Transactions.Where(
                        t =>
                        t.Status == (byte)MyUtilities.Transaction.Status.Open &&
                        t.WarehouseReceiptId == MyUtilities.Warehouse.Processing)
                        .OrderBy(x=> x.CreatedDate)
                        .ToList();
                var transactionIds = transactions.Select(x => x.TransactionId).ToList();
                var defectTransactions = vfi.DefectTransactions.Where(x => transactionIds.Contains(x.ImportTransactionId.Value));
                foreach (var transaction in transactions) {
                    if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1) {
                            if (transaction.ReferenceId == null || transaction.ReferenceId == 0) continue;
                    }
                    var entity = new TransactionModel {
                        TransactionId = transaction.TransactionId,
                        TransactionCode = transaction.TransactionCode + "-" + transaction.CreatedDate.ToString("dd/MM")
                    };
                    if (transaction.WarehouseIssueId != null) {
                        entity.TransactionCode += (" - " + transaction.Warehouse.ShortName);
                    }
                    else {
                        entity.TransactionCode += (" - nhập thêm!");
                    }
                    var defectTransaction = defectTransactions.FirstOrDefault(x => x.ImportTransactionId == transaction.TransactionId);
                    if (defectTransaction != null) {
                        entity.TransactionCode += (" - " + MyUtilities.Transaction.CastText.GetDefectTransactionTextStatus(defectTransaction.Status));
                    }
                    else {
                        entity.TransactionCode += (" - Chưa phân lỗi!");
                    }

                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "TransactionId", "TransactionCode"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [GridAction]
        public ActionResult SelectImportTransactionDetail(long transactionId) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            var model = new List<TransactionDetailModel>();
            try {
                model = GetImportTransactionDetail(transactionId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTransactionDefect", ex.Message);
            }
            return View(new GridModel(model.OrderByDescending(f => f.ModifiedDate)));
        }

        private List<TransactionDetailModel> GetImportTransactionDetail(long transactionId) {
            var model = new List<TransactionDetailModel>();
            using (var vfi = new vfiContext()) {
                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                var transactionDefect =
                    vfi.DefectTransactions.FirstOrDefault(
                        dt =>
                        dt.ImportTransactionId == transactionId && dt.Status != (byte)MyUtilities.Transaction.Status.Cancel);
                //var transactionDefectDetails = transactionDefect != null ? transactionDefect.DefectTransactionDetails : new List<DefectTransactionDetail>();
                var productIds = transaction.TransactionDetails.Select(x => x.ReferenceId.Value).Distinct().ToList();
                var invs =
                    vfi.ProductInventories.Where(
                        pi =>
                        pi.WarehouseId == transaction.WarehouseIssueId && productIds.Contains(pi.ProductId));
                if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1 && transaction.ReferenceId != null) // nhập từ sx1
                {
                    var production = vfi.ImportFormSX1.FirstOrDefault(i => i.ImportId == transaction.ReferenceId);
                    if (production == null) return model;
                    var sx1Details = production.ImportFormSX1Detail.Where(id => id.Processing1 + id.Processing2 > 0);
                    foreach (var detail in sx1Details) {
                        var entity = new TransactionDetailModel {
                            TransactionId = transactionId,
                            TransactionDetailId = detail.DetailId,
                            ReferenceId = detail.ProductId,
                            ProductCode = detail.Product.ProductCode,
                            Quantity = detail.Processing1 + detail.Processing2,
                            Note = detail.Machine1.MachineName,
                            Status = 3,
                            AvailableQuantity = 0,
                            Shift = detail.Processing1 > 0 ? 1 : 2,
                        };
                        if (!detail.Product.ProductionDefects.Any(pd => pd.Active))
                            entity.Status = 1;
                        else if (transactionDefect != null) {
                            entity.Status =
                                transactionDefect.DefectTransactionDetails.Any(
                                    dtd => dtd.ProductId == entity.ReferenceId && detail.MachineId == dtd.MachineId)
                                    ? 2
                                    : 3;
                        }
                        entity.UnitWeight = MyUtilities.Product.GetProductInvWeight(detail.ProductId, transaction.WarehouseIssueId.Value);
                        entity.QuantityKg = entity.Quantity * entity.UnitWeight;
                        var inv = invs.FirstOrDefault(pi => pi.ProductId == entity.ReferenceId);
                        if (inv != null)
                            entity.AvailableQuantity = inv.TotalQty;
                        model.Add(entity);
                    }
                }
                else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Cnc && transaction.ReferenceId != null) {
                    var production = vfi.ImportFormCncs.FirstOrDefault(i => i.ImportId == transaction.ReferenceId);
                    if (production == null) return model;
                    var sx1Details = production.ImportFormCncDetails.Where(id => id.Processing1 + id.Processing2 > 0);
                    foreach (var detail in sx1Details) {
                        var unitWeight = MyUtilities.Product.GetProductInvWeight(detail.ProductId, transaction.WarehouseIssueId.Value);
                        if (detail.Processing1 > 0) {
                            var entity = new TransactionDetailModel {
                                TransactionId = transactionId,
                                TransactionDetailId = detail.DetailId,
                                ReferenceId = detail.ProductId,
                                ProductCode = detail.Product.ProductCode,
                                Quantity = detail.Processing1,
                                Note = detail.Machine.MachineName,
                                Status = 3,
                                AvailableQuantity = 0,
                                Shift = 1,
                            };
                            if (!detail.Product.ProductionDefects.Any(pd => pd.Active))
                                entity.Status = 1;
                            else if (transactionDefect != null) {
                                entity.Status =
                                    transactionDefect.DefectTransactionDetails.Any(
                                        dtd => dtd.ProductId == entity.ReferenceId && dtd.MachineId == detail.MachineId && dtd.Shift == 1)
                                        ? 2
                                        : 3;
                            }
                            entity.UnitWeight = unitWeight;
                            entity.QuantityKg = entity.Quantity * entity.UnitWeight;
                            var inv = invs.FirstOrDefault(pi => pi.ProductId == entity.ReferenceId);
                            if (inv != null)
                                entity.AvailableQuantity = inv.TotalQty;
                            model.Add(entity);
                        }
                        if (detail.Processing2 > 0) {
                            var entity = new TransactionDetailModel {
                                TransactionId = transactionId,
                                TransactionDetailId = detail.DetailId,
                                ReferenceId = detail.ProductId,
                                ProductCode = detail.Product.ProductCode,
                                Quantity = detail.Processing2,
                                Note = detail.Machine.MachineName,
                                Status = 3,
                                AvailableQuantity = 0,
                                Shift = 2,
                            };
                            if (!detail.Product.ProductionDefects.Any(pd => pd.Active))
                                entity.Status = 1;
                            else if (transactionDefect != null) {
                                entity.Status =
                                    transactionDefect.DefectTransactionDetails.Any(
                                        dtd => dtd.ProductId == entity.ReferenceId && dtd.MachineId == detail.MachineId && dtd.Shift == 2)
                                        ? 2
                                        : 3;
                            }
                            entity.UnitWeight = unitWeight;
                            entity.QuantityKg = entity.Quantity * entity.UnitWeight;
                            var inv = invs.FirstOrDefault(pi => pi.ProductId == entity.ReferenceId);
                            if (inv != null)
                                entity.AvailableQuantity = inv.TotalQty;
                            model.Add(entity);
                        }
                    }
                }
                else // nhập từ nguồn khác
                {
                    foreach (var detail in transaction.TransactionDetails) {
                        var entity = model.FirstOrDefault(x => x.ReferenceId == detail.ReferenceId);
                        if (entity == null) {
                            entity = new TransactionDetailModel {
                                TransactionId = transactionId,
                                TransactionDetailId = detail.TransactionDetailId,
                                ReferenceId = detail.ReferenceId,
                                ProductCode = detail.Product.ProductCode,
                                Quantity = detail.Quantity,
                                Note = detail.Note,
                                Status = 3,
                                AvailableQuantity = 0,
                                Shift = 0
                            };
                            if (!detail.Product.ProductionDefects.Any(pd => pd.Active))
                                entity.Status = 1;
                            else if (transactionDefect != null) {
                                entity.Status =
                                    transactionDefect.DefectTransactionDetails.Any(
                                        dtd => dtd.ProductId == entity.ReferenceId)
                                        ? 2
                                        : 3;
                            }
                            entity.UnitWeight = MyUtilities.Product.GetProductInvWeight(detail.ReferenceId.Value, transaction.WarehouseIssueId.Value);
                            entity.QuantityKg = entity.Quantity * entity.UnitWeight;
                            var inv = invs.FirstOrDefault(pi => pi.ProductId == entity.ReferenceId);
                            if (inv != null)
                                entity.AvailableQuantity = inv.TotalQty;
                            model.Add(entity);
                        }
                        else {
                            entity.Quantity += detail.Quantity;
                            entity.QuantityKg = entity.Quantity * entity.UnitWeight;
                        }
                    }
                }
            }
            return model.OrderByDescending(f => f.ModifiedDate).ToList();
        }

        [GridAction]
        public ActionResult SelectDefectTransaction(byte status, string fromDate, string toDate) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            var model = new List<DefectTransactionModel>();
            try {
                if (!string.IsNullOrWhiteSpace(fromDate))
                    model = GetDefectTransactionsList(status, fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDefectTransaction", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<DefectTransactionModel> GetDefectTransactionsList(byte status, string fromDate, string toDate) {
            var model = new List<DefectTransactionModel>();

            var ci = new CultureInfo("vi-VN");
            var fDate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(fromDate))
                fDate = Convert.ToDateTime(fromDate, ci);
            var tDate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(toDate))
                tDate = Convert.ToDateTime(toDate, ci);
            using (var vfi = new vfiContext()) {
                var transactions = vfi.DefectTransactions.Where(x => x.Status == status && x.CreateDate >= fDate && x.CreateDate <= tDate).ToList();

                var transactionIds = transactions.Select(x => x.TransactionId).Distinct().ToList();
                var transactionDetails = vfi.DefectTransactionDetails.Where(td => transactionIds.Contains(td.TransactionId));

                var imporTransactionIds = transactions.Select(x => x.ImportTransactionId).Distinct().ToList();
                var importTransactions = vfi.Transactions.Where(x => imporTransactionIds.Contains(x.TransactionId));
                foreach (var transaction in transactions) {
                    var transactionDetailsById = transactionDetails.Where(x => x.TransactionId == transaction.TransactionId);

                    if (!transactionDetails.Any()) continue;
                    var entity = new DefectTransactionModel {
                        TransactionId = transaction.TransactionId,
                        TransactionCode = transaction.TransactionCode,
                        CreateDate = transaction.CreateDate,
                        ModifiedDate = transaction.ModifiedDate,
                        ModifiedUser = transaction.ModifiedUser,
                        Status = transaction.Status,
                        StatusName = MyUtilities.Transaction.CastText.GetDefectTransactionTextStatus(transaction.Status),
                        Quantity = transactionDetailsById.Sum(x => x.QuantityDefect),
                        EoI = transaction.EoI,
                        ImportTransactionId = transaction.ImportTransactionId ?? 0
                    };
                    var importTransaction = importTransactions.FirstOrDefault(x => x.TransactionId == transaction.ImportTransactionId);
                    if (importTransaction != null) {
                        if (importTransaction.WarehouseIssueId != null) {
                            entity.ImportWarehouseName = importTransaction.Warehouse.WarehouseName;
                        }
                    }
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.CreateDate).ToList();
        }

        [GridAction]
        public ActionResult SelectDefectTransactionDetail(long transactionId) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            var model = new List<DefectTransactionDetailModel>();
            try {
                model = GetDefectTransactionDetails(transactionId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDefectTransactionDetail", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<DefectTransactionDetailModel> GetDefectTransactionDetails(long transactionId) {
            var model = new List<DefectTransactionDetailModel>();
            using (var vfi = new vfiContext()) {
                var transaction = vfi.DefectTransactions.FirstOrDefault(t => t.TransactionId == transactionId);
                if (transaction == null) { throw new AggregateException("Không tìm thấy phiếu"); }

                var importTransaction = vfi.DefectTransactions.FirstOrDefault(t => t.TransactionId == transaction.ImportTransactionId);

                foreach (var detail in transaction.DefectTransactionDetails) {
                    var entity = new DefectTransactionDetailModel {
                        TransactionId = detail.TransactionId,
                        DetailId = detail.DetailId,
                        DefectId = detail.DefectId,
                        DefectName = detail.ProductionDefect.DefectName,
                        ProductId = detail.ProductId,
                        ProductCode = detail.Product.ProductCode,
                        QuantityDefect = detail.QuantityDefect,
                        Destroy = detail.Destroy,
                        Recheck = detail.Recheck,
                        Reprocess = detail.Reprocess,
                    };
                    if (detail.MachineId != null) {
                        entity.MachineId = detail.MachineId;
                        entity.MachineName = detail.Machine.MachineName;
                    }
                    if (detail.NextWarehouseProcessId != null) {
                        entity.NextWarehouseProcessId = detail.NextWarehouseProcessId;
                        entity.NextWarehouseProcessName = detail.Warehouse.WarehouseName;
                    }
                    if (detail.RemedyId != null) {
                        entity.RemedyId = detail.RemedyId;
                        entity.RemedyCode = detail.ProductionDefectRemedy.RemedyName;
                    }
                    entity.UnitWeight = MyUtilities.Product.GetProductInvWeight(entity.ProductId, MyUtilities.Warehouse.QcA);
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.MachineName).OrderBy(m => m.ProductCode).ThenBy(x => x.DefectName).ToList();
        }

        [GridAction]
        public ActionResult SelectImportTransactionDefectDetail(long transactionId, string shift, string detailId, string pId) {
            var model = new List<DefectTransactionDetailModel>();
            if (string.IsNullOrWhiteSpace(detailId)) {
                return View(new GridModel(model));
            }
            // material
            try {
                var sh = Convert.ToInt32(shift);
                var convertId = Convert.ToInt64(detailId);
                var productId = Convert.ToInt32(pId);
                using (var vfi = new vfiContext()) {
                    var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);

                    var productionDefects = (from x in vfi.ProductionDefects
                                             where x.ProductId == productId && x.Active
                                             select new {
                                                 x.DefectId,
                                                 x.DefectName,
                                                 x.ProductId,
                                                 x.Product.ProductCode,
                                                 x.Product.ProductionWeight,
                                                 x.Product.CncWeight,
                                                 DefaultRemedyId = x.DefaultRemedyId ?? 0,
                                                 DefaultRemedyName = x.DefaultRemedyId != null
                                                                 ? x.ProductionDefectRemedy.RemedyName
                                                                 : "",
                                                 ProcessWarehouseId = x.DefaultRemedyId != null
                                                                 ? (x.ProductionDefectRemedy.ProcessWarehouseId ?? 0)
                                                                 : 0,
                                                 ProcessWarehouseName =
                                                     x.DefaultRemedyId != null && x.ProductionDefectRemedy.ProcessWarehouseId != null
                                                        ? x.ProductionDefectRemedy.Warehouse.WarehouseName
                                                        : "",
                                             })
                                            .OrderBy(x => x.DefectName)
                                            .ToList();
                    if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1 && transaction.ReferenceId != null) {
                        var importSx1Detail = vfi.ImportFormSX1Detail.FirstOrDefault(id => id.DetailId == convertId);
                        if (importSx1Detail == null)
                            throw new AggregateException("Lỗi! không tìm thấy chi tiết !");

                        var existDefectTransactionDetails =
                            vfi.DefectTransactionDetails.Where(
                                dtd =>
                                dtd.DefectTransaction.ImportTransactionId == transaction.TransactionId &&
                                dtd.ProductId == importSx1Detail.ProductId &&
                                dtd.MachineId == importSx1Detail.MachineId &&
                                (dtd.ReferenceDetailId == null || dtd.ReferenceDetailId == convertId));
                        foreach (var defect in productionDefects) {
                            var entity = new DefectTransactionDetailModel {
                                DefectId = defect.DefectId,
                                DefectName = defect.DefectName,
                                ProductId = defect.ProductId,
                                ProductCode = defect.ProductCode,
                                UnitWeight = defect.ProductionWeight ?? 0,
                                MachineId = importSx1Detail.MachineId,
                                MachineName = importSx1Detail.Machine1.MachineName,
                                QuantityDefect = 0,
                                Shift = sh,
                                ReferenceDetailId = importSx1Detail.DetailId,
                                DetailId = 0,
                                RequireQuantityDefect = importSx1Detail.Processing1 + importSx1Detail.Processing2,
                                DefaultRemedyId = defect.DefaultRemedyId,
                                RemedyName = defect.DefaultRemedyName,
                                ProcessWarehouseId = defect.ProcessWarehouseId,
                                ProcessWarehouseName = defect.ProcessWarehouseName,
                            };
                            entity.RequireQuantityDefectKg = entity.RequireQuantityDefect * entity.UnitWeight;
                            if (existDefectTransactionDetails.Any()) {
                                var exist = existDefectTransactionDetails.FirstOrDefault(x => x.ProductId == entity.ProductId && x.DefectId == entity.DefectId);
                                if (exist != null) {
                                    entity.DetailId = exist.DetailId;
                                    entity.QuantityDefect = exist.QuantityDefect;
                                    entity.DefectExpand = exist.DefectExpand;
                                    entity.Note = exist.Note;
                                    entity.StoreCode = exist.StoreCode;
                                    entity.QuantityDefectKg = entity.QuantityDefect * entity.UnitWeight;
                                    if (exist.RemedyId != null) {
                                        entity.RemedyId = exist.RemedyId;
                                        entity.RemedyCode = exist.ProductionDefectRemedy.RemedyName;
                                    }
                                    if (exist.NextWarehouseProcessId != null) {
                                        entity.NextWarehouseProcessId = exist.NextWarehouseProcessId;
                                        entity.NextWarehouseProcessName = exist.Warehouse.WarehouseName;
                                    }
                                }
                            }
                            model.Add(entity);
                        }
                    }
                    else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Cnc && transaction.ReferenceId != null) {
                        //var import = vfi.ImportFormCncs.FirstOrDefault(i => i.ImportId == transaction.ReferenceId);
                        //if (import == null)
                        //    return View(new GridModel(model));
                        var importDetail = vfi.ImportFormCncDetails.FirstOrDefault(id => id.DetailId == convertId);
                        if (importDetail == null)
                            throw new AggregateException("Lỗi! không tìm thấy chi tiết !");

                        var existDefectTransactionDetails =
                            vfi.DefectTransactionDetails.Where(
                                dtd =>
                                dtd.DefectTransaction.ImportTransactionId == transaction.TransactionId &&
                                dtd.ProductId == importDetail.ProductId &&
                                dtd.MachineId == importDetail.MachineId &&
                                (dtd.ReferenceDetailId == null || dtd.ReferenceDetailId == convertId) &&
                                dtd.Shift == sh);
                        foreach (var defect in productionDefects) {
                            var entity = new DefectTransactionDetailModel {
                                DefectId = defect.DefectId,
                                DefectName = defect.DefectName,
                                ProductId = defect.ProductId,
                                ProductCode = defect.ProductCode,
                                MachineId = importDetail.MachineId,
                                MachineName = importDetail.Machine.MachineName,
                                QuantityDefect = 0,
                                Shift = sh,
                                ReferenceDetailId = importDetail.DetailId,
                                DetailId = 0,
                                UnitWeight = defect.CncWeight ?? 0,
                                RequireQuantityDefect = importDetail.Processing1 + importDetail.Processing2,
                                DefaultRemedyId = defect.DefaultRemedyId,
                                RemedyName = defect.DefaultRemedyName,
                                ProcessWarehouseId = defect.ProcessWarehouseId,
                                ProcessWarehouseName = defect.ProcessWarehouseName
                            };
                            entity.RequireQuantityDefectKg = entity.RequireQuantityDefect * entity.UnitWeight;
                            if (existDefectTransactionDetails.Any()) {
                                var exist = existDefectTransactionDetails.FirstOrDefault(x => x.ProductId == entity.ProductId && x.DefectId == entity.DefectId);
                                if (exist != null) {
                                    entity.DetailId = exist.DetailId;
                                    entity.QuantityDefect = exist.QuantityDefect;
                                    entity.DefectExpand = exist.DefectExpand;
                                    entity.Note = exist.Note;
                                    entity.StoreCode = exist.StoreCode;
                                    entity.QuantityDefectKg = entity.QuantityDefect * entity.UnitWeight;
                                    if (exist.RemedyId != null) {
                                        entity.RemedyId = exist.RemedyId;
                                        entity.RemedyCode = exist.ProductionDefectRemedy.RemedyName;
                                    }
                                    if (exist.NextWarehouseProcessId != null) {
                                        entity.NextWarehouseProcessId = exist.NextWarehouseProcessId;
                                        entity.NextWarehouseProcessName = exist.Warehouse.WarehouseName;
                                    }
                                }
                            }
                            model.Add(entity);
                        }
                    }
                    else {

                        var transactionDetail =
                            vfi.TransactionDetails.FirstOrDefault(td => td.TransactionDetailId == convertId);
                        if (transactionDetail == null)
                            throw new AggregateException("Lỗi! không tìm thấy chi tiết !");
                        var transactionDetails =
                            transaction.TransactionDetails.Where(td => td.ReferenceId == transactionDetail.ReferenceId).ToList();

                        var unitWeight = 0.0;
                        if (transaction.WarehouseIssueId != null) {
                            unitWeight = MyUtilities.Product.GetProductInvWeight(productId, transaction.WarehouseIssueId.Value);
                        }
                        else {
                            unitWeight = MyUtilities.Product.GetProductInvWeight(productId, MyUtilities.Warehouse.QcA);
                        }

                        var existDefectTransactionDetails =
                           vfi.DefectTransactionDetails.Where(
                               dtd =>
                               dtd.DefectTransaction.ImportTransactionId == transaction.TransactionId &&
                               dtd.ProductId == transactionDetail.ReferenceId);
                        foreach (var defect in productionDefects) {
                            var entity = new DefectTransactionDetailModel {
                                DefectId = defect.DefectId,
                                DefectName = defect.DefectName,
                                ProductId = transactionDetail.ReferenceId.Value,
                                ProductCode = defect.ProductCode,
                                MachineName = "",
                                QuantityDefect = 0,
                                Shift = sh,
                                ReferenceDetailId = null,
                                DetailId = 0,
                                DefectExpand = "",
                                UnitWeight = unitWeight,
                                RequireQuantityDefect = transactionDetails.Sum(x => x.Quantity),
                                DefaultRemedyId = defect.DefaultRemedyId,
                                RemedyName = defect.DefaultRemedyName,
                                ProcessWarehouseId = defect.ProcessWarehouseId,
                                ProcessWarehouseName = defect.ProcessWarehouseName
                            };
                            entity.RequireQuantityDefectKg = entity.RequireQuantityDefect * entity.UnitWeight;
                            if (existDefectTransactionDetails.Any()) {
                                var exist = existDefectTransactionDetails.FirstOrDefault(x => x.ProductId == entity.ProductId && x.DefectId == entity.DefectId);
                                if (exist != null) {
                                    entity.DetailId = exist.DetailId;
                                    entity.QuantityDefect = exist.QuantityDefect;
                                    entity.DefectExpand = exist.DefectExpand;
                                    entity.Note = exist.Note;
                                    entity.QuantityDefectKg = entity.QuantityDefect * entity.UnitWeight;
                                    if (exist.RemedyId != null) {
                                        entity.RemedyId = exist.RemedyId;
                                        entity.RemedyCode = exist.ProductionDefectRemedy.RemedyName;
                                    }
                                    if (exist.NextWarehouseProcessId != null) {
                                        entity.NextWarehouseProcessId = exist.NextWarehouseProcessId;
                                        entity.NextWarehouseProcessName = exist.Warehouse.WarehouseName;
                                    }
                                }
                            }
                            model.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportTransactionDefectDetail", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        public ActionResult GetRemedyDefaultByDefect(int defectId) {
            try {
                var defaultRemedy = new ProductionDefectModel() {
                    DefectId = defectId,
                    DefaultRemedyId = 0,
                    RemedyName = "",
                    WarehouseProcessId = 0,
                    WarehouseProcessName = ""
                };
                using (var vfi = new vfiContext()) {
                    var defect = vfi.ProductionDefects.FirstOrDefault(x => x.DefectId == defectId);
                    if (defect != null && defect.DefaultRemedyId > 0) {
                        defaultRemedy.DefaultRemedyId = defect.DefaultRemedyId.Value;
                        defaultRemedy.RemedyName = defect.ProductionDefectRemedy.RemedyName;
                        if (defect.ProductionDefectRemedy.ProcessWarehouseId > 0) {
                            defaultRemedy.WarehouseProcessId = defect.ProductionDefectRemedy.ProcessWarehouseId.Value;
                            defaultRemedy.WarehouseProcessName = defect.ProductionDefectRemedy.Warehouse.WarehouseName;
                        }
                    }
                }
                return Json(new object[]
                    {
                        defaultRemedy.DefaultRemedyId,
                        defaultRemedy.RemedyName,
                        defaultRemedy.WarehouseProcessId,
                        defaultRemedy.WarehouseProcessName,
                    });
            }
            catch (Exception) {
                return Json("e");
            }
        }

        [HttpPost]
        public ActionResult GetProcessWarehouseIdByRemedy(int remedyId) {
            try {
                var processWarehouse = new WarehouseModel() {
                    WarehouseId = 0,
                    WarehouseName = ""
                };
                using (var vfi = new vfiContext()) {
                    var remedy = vfi.ProductionDefectRemedies.FirstOrDefault(x => x.RemedyId == remedyId);
                    if (remedy != null && remedy.ProcessWarehouseId > 0) {
                        processWarehouse.WarehouseId = remedy.Warehouse.WarehouseId;
                        processWarehouse.WarehouseName = remedy.Warehouse.WarehouseName;
                    }
                }
                return Json(new object[]
                    {
                        processWarehouse.WarehouseId,
                        processWarehouse.WarehouseName,
                    });
            }
            catch (Exception) {
                return Json("e");
            }
        }

        #endregion

        #region business

        [GridAction]
        public ActionResult CreateDefectTransactionDetail(
         [Bind(Prefix = "inserted")] IEnumerable<DefectTransactionDetailModel> insertedDetails,
         [Bind(Prefix = "updated")] IEnumerable<DefectTransactionDetailModel> updatedDetails,
         [Bind(Prefix = "deleted")] IEnumerable<DefectTransactionDetailModel> deletedDetails
         , long transactionId, string date
         ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<DefectTransactionDetailModel>()));
            }
            try {
                if (!updatedDetails.Any()) { return View(new GridModel(new List<DefectTransactionDetailModel>())); }
                var ci = new CultureInfo("vi-VN");
                var fDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(date)) {
                    fDate = MyUtilities.Function.ParseDate(date);
                }
                using (var vfi = new vfiContext()) {
                    var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                    var shift = updatedDetails.FirstOrDefault().Shift;
                    var processQuantity = 0.0;
                    var pId = Convert.ToInt32(updatedDetails.FirstOrDefault().ProductId);

                    var defectTransactionDetails = new List<DefectTransactionDetail>();
                    var transactionDefect = vfi.DefectTransactions.FirstOrDefault(dt => dt.ImportTransactionId == transactionId);
                    var referenceDetailId = updatedDetails.FirstOrDefault().ReferenceDetailId;
                    if (transactionDefect == null) {
                        transactionDefect = new DefectTransaction {
                            ImportTransactionId = transactionId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Status = (byte)MyUtilities.Transaction.Status.Processing,
                            EoI = (byte)MyUtilities.Transaction.EoIEnum.Import,
                            DefectTransactionDetails = new Collection<DefectTransactionDetail>(),
                            TransactionCode = transaction.TransactionCode,
                            CreateDate = transaction.CreatedDate,
                        };
                        vfi.DefectTransactions.Add(transactionDefect);
                    }
                    else {
                        if (transactionDefect.Status == (byte)MyUtilities.Transaction.Status.Cancel) {
                            transactionDefect.Status = (byte)MyUtilities.Transaction.Status.Processing;
                        }
                    }

                    if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1 && transaction.ReferenceId != null) {
                        var importSx1 = vfi.ImportFormSX1.FirstOrDefault(i => i.ImportId == transaction.ReferenceId);
                        if (importSx1 != null) {
                            var importDetail = importSx1.ImportFormSX1Detail.FirstOrDefault(
                                                            id => id.DetailId == referenceDetailId);
                            if (importDetail == null) {
                                throw new AggregateException("Lỗi! Không tìm thấy sản xuất cần phân lỗi !");
                            }
                            processQuantity = importDetail.Processing1 + importDetail.Processing2;
                            defectTransactionDetails = transactionDefect.DefectTransactionDetails.Where(x => x.ProductId == pId
                                                                                    && x.MachineId == importDetail.MachineId
                                                                                    && (referenceDetailId == null || referenceDetailId == x.ReferenceDetailId)).ToList();
                            transactionDefect.CreateDate = importSx1.MaterialUseDate;
                        }
                    }
                    else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Cnc && transaction.ReferenceId != null) {
                        var importSx1 = vfi.ImportFormCncs.FirstOrDefault(i => i.ImportId == transaction.ReferenceId);
                        if (importSx1 != null) {
                            var importDetail = importSx1.ImportFormCncDetails.FirstOrDefault(
                                                            id => id.DetailId == referenceDetailId);
                            if (importDetail == null) {
                                throw new AggregateException("Lỗi! Không tìm thấy sản xuất cần phân lỗi !");
                            }
                            processQuantity = shift == 1 ? importDetail.Processing1 : importDetail.Processing2;
                            defectTransactionDetails = transactionDefect.DefectTransactionDetails.Where(x => x.ProductId == pId
                                                                                    && x.MachineId == importDetail.MachineId
                                                                                    && (referenceDetailId == null || referenceDetailId == x.ReferenceDetailId)).ToList();
                            transactionDefect.CreateDate = importSx1.MaterialUseDate;
                        }
                    }
                    else {
                        var transactionDetails = transaction.TransactionDetails.Where(td => td.ReferenceId == pId);
                        if (!transactionDetails.Any())
                            throw new AggregateException("Lỗi! không tìm thấy chi tiết !");
                        processQuantity = transactionDetails.Sum(x => x.Quantity);
                        defectTransactionDetails = transactionDefect.DefectTransactionDetails.Where(x => x.ProductId == pId).ToList();
                    }
                    var remedyIds = updatedDetails.Select(x => x.RemedyId).Distinct().ToList();
                    var remedies = vfi.ProductionDefectRemedies.Where(x => remedyIds.Any() && remedyIds.Contains(x.RemedyId));
                    var defectIds = updatedDetails.Select(x => x.DefectId).Distinct().ToList();
                    var defects = vfi.ProductionDefects.Where(x => defectIds.Contains(x.DefectId));
                    foreach (var updatedDetail in updatedDetails) {
                        var defect = defects.FirstOrDefault(x => x.DefectId == updatedDetail.DefectId);
                        if (updatedDetail.DetailId != 0) {
                            var defectTransactionDetail = defectTransactionDetails.FirstOrDefault(x => x.DetailId == updatedDetail.DetailId);
                            if (defectTransactionDetail == null) continue;
                            var checkQuantity = defectTransactionDetails.FirstOrDefault(x => x.DetailId == updatedDetail.DetailId);
                            checkQuantity.QuantityDefect = updatedDetail.QuantityDefect;
                            if (updatedDetail.QuantityDefect == 0) {
                                vfi.DefectTransactionDetails.Remove(defectTransactionDetail);
                                transactionDefect.DefectTransactionDetails.Remove(defectTransactionDetail);
                            }
                            else {
                                defectTransactionDetail.QuantityDefect = updatedDetail.QuantityDefect;
                                defectTransactionDetail.Note = updatedDetail.Note;
                                defectTransactionDetail.NextWarehouseProcessId = updatedDetail.NextWarehouseProcessId;
                                defectTransactionDetail.DefectExpand = updatedDetail.DefectExpand;
                                defectTransactionDetail.ReferenceDetailId = referenceDetailId;
                                defectTransactionDetail.StoreCode = updatedDetail.StoreCode;
                                if (updatedDetail.RemedyId > 0) {
                                    defectTransactionDetail.RemedyId = updatedDetail.RemedyId;
                                    defect.DefaultRemedyId = defectTransactionDetail.RemedyId;
                                }
                                if (updatedDetail.NextWarehouseProcessId == MyUtilities.Warehouse.Defect) {
                                    defectTransactionDetail.Destroy = true;
                                }
                                else {
                                    defectTransactionDetail.Destroy = false;
                                }
                            }
                        }
                        else {
                            if (updatedDetail.QuantityDefect <= 0) continue;
                            var detail = new DefectTransactionDetail {
                                DefectId = updatedDetail.DefectId,
                                MachineId = updatedDetail.MachineId,
                                Note = updatedDetail.Note,
                                ProductId = updatedDetail.ProductId,
                                TransactionId = transactionDefect.TransactionId,
                                DefectTransaction = transactionDefect,
                                QuantityDefect = updatedDetail.QuantityDefect,
                                Destroy = false,
                                Recheck = false,
                                Reprocess = false,
                                NextWarehouseProcessId = updatedDetail.NextWarehouseProcessId,
                                RemedyId = updatedDetail.RemedyId,
                                Shift = updatedDetail.Shift,
                                DefectExpand = updatedDetail.DefectExpand,
                                ReferenceDetailId = referenceDetailId,
                                StoreCode = updatedDetail.StoreCode
                            };
                            if (detail.RemedyId > 0 && detail.NextWarehouseProcessId == null) {
                                var remedy = remedies.FirstOrDefault(x => x.RemedyId == detail.RemedyId);
                                detail.NextWarehouseProcessId = remedy.ProcessWarehouseId;
                                defect.DefaultRemedyId = detail.RemedyId;
                            }
                            if (detail.NextWarehouseProcessId == MyUtilities.Warehouse.Defect) {
                                detail.Destroy = true;
                            }
                            defectTransactionDetails.Add(detail);
                            transactionDefect.DefectTransactionDetails.Add(detail);
                        }
                    }

                    var diff = defectTransactionDetails.Where(x => x.Shift == shift).Sum(ud => ud.QuantityDefect) - processQuantity;
                    if (diff != 0) {
                        throw new AggregateException("Lỗi! Chi tiết phân lỗi lệch SL !" + diff);
                    }
                    vfi.SaveChanges();

                    if (transactionDefect.Status != (byte)MyUtilities.Transaction.Status.Approved) {
                        if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Production1 && transaction.ReferenceId != null) {
                            var import = vfi.ImportFormSX1.FirstOrDefault(i => i.ImportId == transaction.ReferenceId);
                            var details = import.ImportFormSX1Detail.Where(x => x.Processing1 + x.Processing2 > 0).ToList();
                            //var defectDetails = transactionDefect.DefectTransactionDetails.Where(x => x.Shift == shift);
                            var isFinish = true;
                            foreach (var detail in details) {
                                var defectDetail = transactionDefect.DefectTransactionDetails.Any(x => x.ProductId == detail.ProductId && x.MachineId == detail.MachineId);
                                if (defectDetail) continue;
                                isFinish = false;
                                break;
                            }
                            if (isFinish) {
                                transactionDefect.Status = (byte)MyUtilities.Transaction.Status.Approved;
                            }
                            else {
                                return View(new GridModel(new List<DefectTransactionDetailModel>()));
                            }
                        }
                        else if (transaction.WarehouseIssueId == MyUtilities.Warehouse.Cnc && transaction.ReferenceId != null) {
                            var import = vfi.ImportFormCncs.FirstOrDefault(i => i.ImportId == transaction.ReferenceId);
                            var details = import.ImportFormCncDetails.Where(x => x.Processing1 > 0);
                            var defectDetails = transactionDefect.DefectTransactionDetails.Where(x => x.Shift == 1);
                            var isFinish = true;
                            foreach (var detail in details) {
                                var defectDetail = defectDetails.Any(x => x.ProductId == detail.ProductId && x.MachineId == detail.MachineId);
                                if (defectDetail) continue;
                                isFinish = false;
                                break;
                            }
                            if (isFinish) {
                                details = import.ImportFormCncDetails.Where(x => x.Processing2 > 0);
                                defectDetails = transactionDefect.DefectTransactionDetails.Where(x => x.Shift == 2);
                                foreach (var detail in details) {
                                    var defectDetail = defectDetails.Any(x => x.ProductId == detail.ProductId && x.MachineId == detail.MachineId);
                                    if (defectDetail) continue;
                                    isFinish = false;
                                    break;
                                }
                                if (isFinish) {
                                    transactionDefect.Status = (byte)MyUtilities.Transaction.Status.Approved;
                                }
                                else {
                                    return View(new GridModel(new List<DefectTransactionDetailModel>()));
                                }
                            }
                            else {
                                return View(new GridModel(new List<DefectTransactionDetailModel>()));
                            }
                        }
                        else {
                            var productIds = transactionDefect.DefectTransactionDetails.Select(x => x.ProductId).Distinct().ToList();
                            if (transaction.TransactionDetails.Any(x => !productIds.Contains(x.ReferenceId.Value))) {
                                return View(new GridModel(new List<DefectTransactionDetailModel>()));
                            }
                            else {
                                transactionDefect.Status = (byte)MyUtilities.Transaction.Status.Approved;
                            }
                        }

                        //var defectIds = updatedDetails.Select(x => x.DefectId).Distinct().ToList();
                        //var defects = vfi.ProductionDefects.Where(x => defectIds.Contains(x.DefectId) && x.DefaultRemedyId == null);
                        //foreach (var defect in defects) {
                        //    var updateDetail = updatedDetails.FirstOrDefault(x => x.DefectId == defect.DefectId);
                        //    defect.DefaultRemedyId = updateDetail.RemedyId;
                        //}

                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CreateDefectTransactionDetail", ex.Message);
            }
            return View(new GridModel(new List<DefectTransactionDetailModel>()));
        }

        [HttpPost]
        public ActionResult ApproveDefectTransaction(long[] checkedRecords) {
            return Json("Không hoàn thành");
        }
        
        //[GridAction]
        public ActionResult UpdateDefectTransactionDetail(DefectTransactionDetailModel update) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            return View(new GridModel(new List<DefectTransactionDetailModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult CancelTransactionDefect(long transactionId) {
            return View(new GridModel(new List<TransactionModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult CancelTransactionDefectDetail(long transactionId, long transactionDetailId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<TransactionModel>()));
            }
            return View(new GridModel(new List<TransactionModel>()));
        }

        #endregion

        #region report 

        
        [HttpPost]
        public ActionResult PrintDefectReport(string fromDate, string toDate, int customerId, string productCode, int productId) {
            var model = new List<DefectGroupReportModel>();
            try {
                model = GetDefectReportGroup(fromDate, toDate, customerId, productCode, productId);
                //return PartialView("PageDefectTransactionListing", model);
            }
            catch (Exception ex) {
                return PartialView(Json(ex.Message));
            }
            return PartialView("PageDefectTransactionListing", model);
        }

        [GridAction]
        public ActionResult SelectTransactionDefectReport(string fromDate, string toDate, int customerId, string productCode) {
            var model = new List<DefectReportModel>();
            try {
                var groups = GetDefectReportGroup(fromDate, toDate, customerId, productCode, 0);

                foreach (var group in groups) {
                    group.Details.ForEach(x => {
                        x.Shift = group.Shift;
                        x.GroupName = group.GroupName;
                        x.ReportDateStr = group.ReportDateStr;
                    });
                    model.AddRange(group.Details);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDefectReport", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<DefectGroupReportModel> GetDefectReportGroup(string fromDate, string toDate, int customerId, string productCode, int productId) {
            var model = new List<DefectGroupReportModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var fDate = MyUtilities.Function.ParseDate(fromDate);
                    var tDate = MyUtilities.Function.ParseDateTime(toDate);

                    var transactions = (from x in vfi.DefectTransactions
                                        where x.Status != (byte)MyUtilities.Transaction.Status.Cancel
                                            && x.CreateDate >= fDate
                                            && x.CreateDate <= tDate
                                        select new {
                                            x.TransactionId,
                                            x.CreateDate,
                                            x.ImportTransactionId,
                                        })
                                        .OrderBy(x => x.CreateDate)
                                        .ToList();
                    if (!transactions.Any()) return model;
                    var isRangeDate = fDate.Date == tDate.Date ? false : true;
                    var dates = transactions.Select(x => x.CreateDate).Distinct().ToList();
                    var transactionIds = transactions.Select(x => x.TransactionId).ToList();

                    var defectDetails = (from x in vfi.DefectTransactionDetails
                                         where transactionIds.Contains(x.TransactionId) && x.QuantityDefect > 0
                                         select new {
                                             x.TransactionId,
                                             x.ProductId,
                                             x.Product.ProductCode,
                                             x.DefectId,
                                             x.ProductionDefect.DefectName,
                                             x.DefectExpand,
                                             x.QuantityDefect,
                                             x.MachineId,
                                             MachineName = x.MachineId != null ? x.Machine.MachineName : "",
                                             x.Destroy,
                                             x.RemedyId,
                                             RemedyName = x.RemedyId != null ? x.ProductionDefectRemedy.RemedyName : "",
                                             x.NextWarehouseProcessId,
                                             NextWarehouseName = x.NextWarehouseProcessId != null ? x.Warehouse.WarehouseName : "",
                                             x.DefectTransaction.ImportTransactionId,
                                             x.ProductionDefect.DefectTypeId,
                                             x.Shift,
                                             x.ReferenceDetailId,
                                         }).ToList();
                    if (customerId > 0 || !string.IsNullOrWhiteSpace(productCode) || productId > 0) {
                        var productIds = (from x in vfi.Products
                                          where (customerId == 0 || x.CustomerId == customerId) 
                                              && (productId == 0 || x.ProductId == productId) 
                                              && x.ProductCode.Contains(productCode)
                                          select x.ProductId).ToList();
                        if (!productIds.Any()) return model;
                        defectDetails = defectDetails.Where(x => productIds.Contains(x.ProductId)).ToList();
                    }
                    var importTransactionIds = transactions.Where(x => x.ImportTransactionId != null).Select(x => x.ImportTransactionId.Value).Distinct().ToList();
                    var importTransactions = (from x in vfi.Transactions
                                              where importTransactionIds.Contains(x.TransactionId)
                                              select new {
                                                  x.TransactionId,
                                                  x.WarehouseIssueId,
                                                  WarehouseIssueName = x.WarehouseIssueId != null ? x.Warehouse.WarehouseName : "",
                                                  x.WarehouseReceiptId,
                                                  WarehouseReceiptName = x.WarehouseReceiptId != null ? x.Warehouse1.WarehouseName : "",
                                                  x.TransactionCode,
                                                  x.ReferenceId,
                                              }).ToList();
                    var shiftNames = vfi.ProductionLocks.Where(x => x.LockDate >= fDate && x.LockDate <= tDate).ToList();

                    var importFromProductions = importTransactions.Where(x => x.WarehouseIssueId == MyUtilities.Warehouse.Production1 && x.ReferenceId != null).ToList();
                    var referenceIds = importFromProductions.Select(x => x.ReferenceId.Value).Distinct().ToList();
                    if (importFromProductions.Any()) {
                        foreach (var date in dates) {
                            var importProductions = vfi.ImportFormSX1.Where(x => x.MaterialUseDate == date && referenceIds.Contains(x.ImportId)).ToList();
                            //var importProductions = vfi.ImportFormSX1.Where(x => importTransactionIds.Contains(x.ImportId));
                            if (!importProductions.Any()) continue;
                            var firstImport = importProductions.FirstOrDefault();
                            var shiftName = shiftNames.FirstOrDefault(x => x.LockDate == firstImport.MaterialUseDate);
                            var group1 = new DefectGroupReportModel {
                                GroupName = shiftName != null ? shiftName.Shift1Name : "Chưa tạo ca 1",
                                Details = new List<DefectReportModel>(),
                                Shift = 1,
                                ReportDate = date,
                                ReportDateStr = date.ToString("dd/MM/yyyy")
                            };
                            var group2 = new DefectGroupReportModel {
                                GroupName = shiftName != null ? shiftName.Shift2Name : "Chưa tạo ca 2",
                                Details = new List<DefectReportModel>(),
                                Shift = 2,
                                ReportDate = date,
                                ReportDateStr = date.ToString("dd/MM/yyyy")
                            };
                            if (isRangeDate) {
                                group1.GroupName += " " + group1.ReportDateStr;
                                group2.GroupName += " " + group2.ReportDateStr;
                                group1.ReportDateStr = fDate.ToString("dd/MM/yyyy") + " - " + tDate.ToString("dd/MM/yyyy");
                                group2.ReportDateStr = group1.ReportDateStr;
                            }
                            model.Add(group1);
                            model.Add(group2);

                            foreach (var import in importProductions) {
                                var importTransaction = importTransactions.FirstOrDefault(x => x.ReferenceId == import.ImportId);
                                var transaction = transactions.FirstOrDefault(x => x.ImportTransactionId == importTransaction.TransactionId);
                                var importDetails = import.ImportFormSX1Detail.Where(x => x.Processing1 + x.Processing2 > 0).ToList();
                                foreach (var detail in importDetails) {
                                    var shift = detail.Processing1 > 0 ? 1 : 2;
                                    //var defectDetailsById = defectDetails.Where(x => x.TransactionId == transaction.TransactionId
                                    //                                                && x.MachineId == detail.MachineId
                                    //                                                && x.ProductId == detail.ProductId
                                    //                                                && x.Shift == shift);
                                    var defectDetailsById = defectDetails.Where(x => x.ReferenceDetailId == detail.DetailId);
                                    foreach (var defectDetail in defectDetailsById) {
                                        var entity = new DefectReportModel {
                                            ProductId = defectDetail.ProductId,
                                            ProductCode = defectDetail.ProductCode,
                                            DefectId = defectDetail.DefectId,
                                            DefectName = defectDetail.DefectName + " " + defectDetail.DefectExpand,
                                            Quantity = defectDetail.QuantityDefect,
                                            IsDetroy = defectDetail.Destroy,
                                            TransactionCode = importTransaction.TransactionCode,
                                            IssueWarehouseName = importTransaction.WarehouseReceiptName
                                        };
                                        if (defectDetail.MachineId != null) {
                                            entity.MachineId = defectDetail.MachineId.Value;
                                            entity.MachineName = defectDetail.MachineName;
                                        }
                                        if (defectDetail.RemedyId != null) {
                                            entity.RemedyId = defectDetail.RemedyId.Value;
                                            entity.RemedyName = defectDetail.RemedyName;
                                        }
                                        if (defectDetail.NextWarehouseProcessId != null) {
                                            entity.NextWarehouseProcessId = defectDetail.NextWarehouseProcessId.Value;
                                            entity.NextWarehouseProcessName = defectDetail.NextWarehouseName;
                                        }
                                        if (detail.Processing1 > 0) {
                                            group1.Details.Add(entity);
                                        }
                                        if (detail.Processing2 > 0) {
                                            group2.Details.Add(entity);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    var importFromCncs = importTransactions.Where(x => x.WarehouseIssueId == MyUtilities.Warehouse.Cnc && x.ReferenceId != null).ToList();
                    referenceIds = importFromCncs.Select(x => x.ReferenceId.Value).Distinct().ToList();
                    if (importFromCncs.Any()) {
                        //var dates = importFromCncs.Select(x => x.CreatedDate).Distinct().ToList();
                        foreach (var date in dates) {
                            
                            //var referenceIds = importFromCncs.Where(x => x.CreatedDate == date).Select(x => x.ReferenceId.Value).Distinct().ToList();
                            var importProductions = vfi.ImportFormCncs.Where(x => referenceIds.Contains(x.ImportId) && x.MaterialUseDate == date);
                            //var importProductions = vfi.ImportFormCncs.Where(x => importTransactionIds.Contains(x.ImportId));
                            if (!importProductions.Any()) continue;
                            var firstImport = importProductions.FirstOrDefault();
                            var shiftName = shiftNames.FirstOrDefault(x => x.LockDate == firstImport.MaterialUseDate);
                            var group1 = model.FirstOrDefault(x => x.ReportDate == date && x.Shift == 1);
                            if (group1 == null) {
                                group1 = new DefectGroupReportModel {
                                    GroupName = shiftName != null ? shiftName.Shift1Name : "Chưa tạo ca 1",
                                    Details = new List<DefectReportModel>(),
                                    Shift = 1,
                                    ReportDate = date,
                                    ReportDateStr = date.ToString("dd/MM/yyyy")
                                };
                                if (isRangeDate) {
                                    group1.GroupName += " " + group1.ReportDateStr;
                                    group1.ReportDateStr = fDate.ToString("dd/MM/yyyy") + " - " + tDate.ToString("dd/MM/yyyy");
                                }
                                model.Add(group1);
                            }
                            var group2 = model.FirstOrDefault(x => x.ReportDate == date && x.Shift == 2);
                            if (group2 == null) {
                                group2 = new DefectGroupReportModel {
                                    GroupName = shiftName != null ? shiftName.Shift2Name : "Chưa tạo ca 2",
                                    Details = new List<DefectReportModel>(),
                                    Shift = 2,
                                    ReportDate = date,
                                    ReportDateStr = date.ToString("dd/MM/yyyy")
                                };
                                if (isRangeDate) {
                                    group2.GroupName += " " + group2.ReportDateStr;
                                    group2.ReportDateStr = fDate.ToString("dd/MM/yyyy") + " - " + tDate.ToString("dd/MM/yyyy");
                                }
                                model.Add(group2);
                            }
                            foreach (var import in importProductions) {
                                var importTransaction = importTransactions.FirstOrDefault(x => x.ReferenceId == import.ImportId);
                                var transaction = transactions.FirstOrDefault(x => x.ImportTransactionId == importTransaction.TransactionId);
                                var importDetails = import.ImportFormCncDetails.Where(x => x.Processing1 + x.Processing2 > 0).ToList();
                                foreach (var detail in importDetails) {
                                    //var shift = detail.Processing1 > 0 ? 1 : 2;
                                    if (detail.Processing1 > 0) {
                                        var defectDetailsById = defectDetails.Where(x => x.TransactionId == transaction.TransactionId
                                                                                        && x.MachineId == detail.MachineId
                                                                                        && x.ProductId == detail.ProductId
                                                                                        && x.Shift == 1);
                                        foreach (var defectDetail in defectDetailsById) {
                                            var entity = new DefectReportModel {
                                                ProductId = defectDetail.ProductId,
                                                ProductCode = defectDetail.ProductCode,
                                                DefectId = defectDetail.DefectId,
                                                DefectName = defectDetail.DefectName + " " + defectDetail.DefectExpand,
                                                Quantity = defectDetail.QuantityDefect,
                                                IsDetroy = defectDetail.Destroy,
                                                TransactionCode = importTransaction.TransactionCode,
                                                IssueWarehouseName = importTransaction.WarehouseIssueName
                                            };
                                            if (defectDetail.MachineId != null) {
                                                entity.MachineId = defectDetail.MachineId.Value;
                                                entity.MachineName = defectDetail.MachineName;
                                            }
                                            if (defectDetail.RemedyId != null) {
                                                entity.RemedyId = defectDetail.RemedyId.Value;
                                                entity.RemedyName = defectDetail.RemedyName;
                                            }
                                            if (defectDetail.NextWarehouseProcessId != null) {
                                                entity.NextWarehouseProcessId = defectDetail.NextWarehouseProcessId.Value;
                                                entity.NextWarehouseProcessName = defectDetail.NextWarehouseName;
                                            }
                                            group1.Details.Add(entity);
                                        }
                                    }
                                    if (detail.Processing2 > 0) {
                                        var defectDetailsById = defectDetails.Where(x => x.TransactionId == transaction.TransactionId
                                                                                        && x.MachineId == detail.MachineId
                                                                                        && x.ProductId == detail.ProductId
                                                                                        && x.Shift == 2);
                                        foreach (var defectDetail in defectDetailsById) {
                                            var entity = new DefectReportModel {
                                                ProductId = defectDetail.ProductId,
                                                ProductCode = defectDetail.ProductCode,
                                                DefectId = defectDetail.DefectId,
                                                DefectName = defectDetail.DefectName + " " + defectDetail.DefectExpand,
                                                Quantity = defectDetail.QuantityDefect,
                                                IsDetroy = defectDetail.Destroy,
                                                TransactionCode = importTransaction.TransactionCode,
                                                IssueWarehouseName = importTransaction.WarehouseIssueName
                                            };
                                            if (defectDetail.MachineId != null) {
                                                entity.MachineId = defectDetail.MachineId.Value;
                                                entity.MachineName = defectDetail.MachineName;
                                            }
                                            if (defectDetail.RemedyId != null) {
                                                entity.RemedyId = defectDetail.RemedyId.Value;
                                                entity.RemedyName = defectDetail.RemedyName;
                                            }
                                            if (defectDetail.NextWarehouseProcessId != null) {
                                                entity.NextWarehouseProcessId = defectDetail.NextWarehouseProcessId.Value;
                                                entity.NextWarehouseProcessName = defectDetail.NextWarehouseName;
                                            }
                                            group2.Details.Add(entity);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    var importElses = importTransactions.Where(x => x.ReferenceId == null);
                    //var importElses = importTransactions.Where(x => x);
                    if (importElses.Any()) {
                        //var dates = importElses.Select(x => x.CreatedDate).Distinct().ToList();
                        var importElseIds = importElses.Select(x => x.TransactionId).ToList();
                        var transactionElses = transactions.Where(x => x.ImportTransactionId != null && importElseIds.Contains(x.ImportTransactionId.Value));
                        var transactionElseIds = transactionElses.Select(x => x.TransactionId).Distinct().ToList();
                        var defectTypes = vfi.ProductionDefectTypes.Where(x => x.Active).ToList();
                        foreach (var defectType in defectTypes) {
                            var group = new DefectGroupReportModel {
                                GroupName = defectType.DefectTypeName,
                                Details = new List<DefectReportModel>(),
                                ReportDate = tDate,
                                ReportDateStr = tDate.ToString("dd/MM/yyyy")
                            };
                            if (isRangeDate) {
                                group.ReportDateStr = fDate.ToString("dd/MM/yyyy") + " - " + tDate.ToString("dd/MM/yyyy");
                            }
                            model.Add(group);
                            var defectDetailsByType = defectDetails.Where(x => transactionElseIds.Contains(x.TransactionId)
                                && x.DefectTypeId == defectType.DefectTypeId).ToList();
                            foreach (var defectDetail in defectDetailsByType) {
                                var entity = new DefectReportModel {
                                    ProductId = defectDetail.ProductId,
                                    ProductCode = defectDetail.ProductCode,
                                    DefectId = defectDetail.DefectId,
                                    DefectName = defectDetail.DefectName + " " + defectDetail.DefectExpand,
                                    Quantity = defectDetail.QuantityDefect,
                                    MachineId = 0,
                                    MachineName = "",
                                    IsDetroy = defectDetail.Destroy,
                                    TransactionCode = "",
                                    IssueWarehouseName = ""
                                };
                                if (defectDetail.MachineId != null) {
                                    entity.MachineId = defectDetail.MachineId.Value;
                                    entity.MachineName = defectDetail.MachineName;
                                }
                                if (defectDetail.RemedyId != null) {
                                    entity.RemedyId = defectDetail.RemedyId.Value;
                                    entity.RemedyName = defectDetail.RemedyName;
                                }
                                if (defectDetail.NextWarehouseProcessId != null) {
                                    entity.NextWarehouseProcessId = defectDetail.NextWarehouseProcessId.Value;
                                    entity.NextWarehouseProcessName = defectDetail.NextWarehouseName;
                                }
                                var importTransaction = importTransactions.FirstOrDefault(x => x.TransactionId == defectDetail.ImportTransactionId);
                                if (importTransaction != null) {
                                    entity.TransactionCode = importTransaction.TransactionCode;
                                    entity.IssueWarehouseName = importTransaction.WarehouseIssueName;
                                }
                                group.Details.Add(entity);
                            }
                        }
                    }
                    //model.Add(report);
                    model = model.Where(x => x.Details.Count > 0).ToList();
                    return model;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            //return model.OrderBy(x => x.ReportDate).ToList();
        }


        #endregion
    }
}
