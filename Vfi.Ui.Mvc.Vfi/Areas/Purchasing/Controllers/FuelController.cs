using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Utilities;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Controllers {
    public class FuelController : Controller {

        //
        // GET: /Purchasing/Fuel/
        #region View
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
        public ActionResult FuelTransactionManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ImportFuelInventory() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ExportFuelInventory() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["ListExportFuelInvIds"] = new List<int>();
            return View();
        }

        public ActionResult AssignFuel() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ApproveImportFuel() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ImportTransactionFuel() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ImportInternalTransactionFuel() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ExportInternalTransactionFuel() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        #endregion

        [GridAction]
        public ActionResult SelectFuelInPo(string ids, int poId) {
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
                var fuelIds = poDetails.Select(pod => pod.ReferenceId).ToList();
                var fuels = from f in vfi.Fuels
                            where fuelIds.Contains(f.FuelId)
                            select f;
                foreach (var id in checkedRecords) {
                    var poDetail =
                        poDetails.FirstOrDefault(
                            pod => pod.PurchaseOrderDetailId == id);
                    var fuel = fuels.FirstOrDefault(m => m.FuelId == poDetail.ReferenceId);
                    var entity = new TransactionFptDetailModel() {
                        FuelId = fuel.FuelId,
                        FuelFullCodeName = fuel.FuelFullCode,
                        RequiredQuantity = poDetail.OrderQty,
                        VendorName = poDetail.Vendor.ShortName,
                        UnitMeasure = poDetail.Unit,
                        UnitPrice = poDetail.UnitPrice,
                        PoDetailId = poDetail.PurchaseOrderDetailId
                    };
                    model.Add(entity);
                }
            }
            return
                View(
                    new GridModel(
                        model.OrderBy(m => m.FuelCode)));
        }

        List<TransactionFptModel> GetTrasactionFuel(string fuelCode, byte status, string fromDate, string toDate) {
            var model = new List<TransactionFptModel>();

            var invManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, 
                MyUtilities.UserRole.InvManagementLv2);
                
            var ci = new CultureInfo("vi-VN");
            var fdate = string.IsNullOrWhiteSpace(fromDate)
                            ? DateTime.Today
                            : Convert.ToDateTime(fromDate, ci);
            var tdate = string.IsNullOrWhiteSpace(toDate)
                            ? DateTime.Today
                            : Convert.ToDateTime(toDate, ci);
            using (var vfi = new tammaContext()) {
                var purchasing = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.PurchasingManagement);

                var transactions = (from i in vfi.TransactionFpts
                                   where
                                       i.Status == status && i.TransactionDate >= fdate &&
                                       i.TransactionDate <= tdate && i.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Fuel
                                       orderby  i.TransactionDate
                                   select i).ToList();

                if (!string.IsNullOrWhiteSpace(fuelCode)) {
                    var fuelIds = vfi.Fuels.Where(t => t.FuelFullCode.Contains(fuelCode)).Select(t => t.FuelId).ToList();
                    if (fuelIds.Any()) {
                        transactions = transactions.Where(t => t.TransactionFptDetails.Any(td => fuelIds.Contains(td.FptId))).ToList();
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
                        InvManager = invManager ? 2 : 0,
                        IsInternal = transaction.IsInternal ??  false,
                        CanUpdate = false
                    };
                    if (entity.PoId != 0)
                        entity.PoCode = transaction.PurchaseOrder.RevisionNumber;
                    entity.StatusName =
                        MyUtilities.Transaction.CastText.GetTextStatus(entity.Status);
                    if (entity.EoI != 0)
                        entity.EoIName = MyUtilities.PurchaseOrder.GetEoIName(entity.EoI, entity.Type);
                    if (entity.Fpt != 0)
                        entity.FptName = MyUtilities.PurchaseOrder.GetFptName(entity.Fpt);
                    if (entity.Status == (byte)MyUtilities.Transaction.Status.Approved) {
                        if (purchasing) {
                            entity.PurchasingSignatureType = 1;
                        }
                    }
                    if (entity.Status == (byte)MyUtilities.Transaction.Status.Approved) {
                        entity.CanUpdate = invManager;
                    }
                    foreach (var detail in transaction.TransactionFptDetails) {
                        entity.TotalQuantity += detail.Quantity;
                        entity.TotalPrice += (detail.Quantity * detail.UnitPrice);
                    }
                    if (entity.IsInternal) {
                        entity.EoIName += " (nội bộ)";
                    }
                    model.Add(entity);
                }
            }
            return model;
        }


        [GridAction]
        public ActionResult SelectTransactionFuel(string fuelCode, byte status, string fromDate, string toDate) {

            var model = new List<TransactionFptModel>();
            try {
                model = GetTrasactionFuel(fuelCode,status, fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectManageImportFuel", "\n" + ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult CancelTransactionFuel(TransactionFptModel update,string fuelCode, byte status, string fromDate, string toDate) {

            try {
                var invManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagementLv2);
                if (!invManager)
                    throw new AggregateException("Lỗi! Không có quyền làm điều này."); // You can't do this.

                using (var vfi = new tammaContext()) {
                    var transaction = vfi.TransactionFpts.FirstOrDefault(t => t.TransactionId == update.TransactionId);
                    if (transaction == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu nhiên liệu");

                    if (transaction.Fpt != (byte)MyUtilities.PurchaseOrder.FptLot.Fuel)
                        throw new AggregateException("Lỗi! Chỉ giải quyết phiếu nhiên liệu");

                    if (transaction.EoI == (byte)MyUtilities.PurchaseOrder.EoILot.Import)
                        throw new AggregateException("Lỗi! Không giải quyết nhập kho");

                    if (transaction.Status != (byte)MyUtilities.Transaction.Status.Approved)
                        throw new AggregateException("Lỗi! Chỉ giải quyết tình trạng đã duyệt");

                    foreach (var detail in transaction.TransactionFptDetails) {
                        var fuelInv =
                            vfi.FuelInventories.FirstOrDefault(
                                fi =>
                                fi.FuelId == detail.FptId && 
                                fi.LotNumber.Equals(detail.LotNumber) &&
                                fi.VendorId == detail.VendorId);
                        fuelInv.TotalQuantity += detail.Quantity;
                    }
                    var periods = vfi.FuelInventoryPeriods.Where(fip => fip.TransactionId == update.TransactionId);
                    if (periods.Any())
                        vfi.FuelInventoryPeriods.RemoveRange(periods);

                    transaction.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateTransactionFuel", "\n" + ex.Message);
            }

            return View(new GridModel(GetTrasactionFuel(fuelCode,status, fromDate, toDate)));
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

                    var result = RollbackFuelInventory(transactionId);
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

        MyUtilities.Monitor.MyJsonResult RollbackFuelInventory(long transactionId) {
            using (var vfi = new tammaContext()) {
                var periods = vfi.FuelInventoryPeriods.Where(x => x.TransactionId == transactionId);
                if (!periods.Any()) {
                    return new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "", 0);
                }
                var invIds = periods.Select(x => x.FuelInvId).Distinct().ToList();
                var invs = vfi.FuelInventories.Where(x => invIds.Contains(x.FuelInvId));
                foreach (var period in periods) {
                    var inv = invs.FirstOrDefault(x => x.FuelInvId == period.FuelInvId);
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
                vfi.FuelInventoryPeriods.RemoveRange(periods);
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
        public ActionResult SelectWaitingImportFuel() {
            //var model = new List<ImportFuelModel>();
            try {
                return View(new GridModel(GetImportFuelModel().OrderBy(m => m.TransactionDate)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportFuelByStatus", "" + ex.Message);
            }
            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        private IEnumerable<TransactionFptModel> GetImportFuelModel() {
            var model = new List<TransactionFptModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Contains(HttpContext.User.Identity.Name));
                    if (user == null)
                        return model;
                    var purchasing = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.PurchasingManagement);

                    var transactions = from import in vfi.TransactionFpts
                                       where import.Status == (byte)MyUtilities.Transaction.Status.Open
                                             && import.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Fuel
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
                            Type = transaction.Type,
                            PurchasingSignature = transaction.PurchasingSignature,
                            PurchasingSignatureType = transaction.PurchasingSignature,
                            IsInternal = transaction.IsInternal ?? false
                        };
                        if (entity.PoId != 0)
                            entity.PoCode = transaction.PurchaseOrder.RevisionNumber;
                        entity.StatusName =
                            MyUtilities.Transaction.CastText.GetTextStatus(entity.Status);
                        if (entity.EoI != 0)
                            entity.EoIName = MyUtilities.PurchaseOrder.GetEoIName(entity.EoI, entity.Type);
                        if (entity.Fpt != 0)
                            entity.FptName = MyUtilities.PurchaseOrder.GetFptName(entity.Fpt);
                        foreach (var detail in transaction.TransactionFptDetails) {
                            entity.TotalQuantity += detail.Quantity;
                            entity.TotalPrice += (detail.Quantity * detail.UnitPrice);
                        }
                        if (entity.PurchasingSignature != 1) {
                            if (purchasing) {
                                entity.PurchasingSignatureType = 2;
                            }
                        }
                        //if (transaction.PurchasingSignature != null) entity.PurchasingSignatureType = 1;
                        //else if (purchasing != null && purchasing.Execution.Value == true)
                        //{
                        //    entity.PurchasingSignatureType = 2;
                        //}
                        entity.AlertColor = 1;
                        if (entity.TransactionDate > DateTime.Now.AddDays(4) ||
                            entity.TransactionDate < DateTime.Now.AddDays(-4))
                            entity.AlertColor = 2;
                        else if (entity.TransactionDate > DateTime.Now.AddDays(1) ||
                                 entity.TransactionDate < DateTime.Now.AddDays(-1))
                            entity.AlertColor = 0;
                        if (entity.IsInternal) {
                            entity.EoIName += " (nội bộ)";
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportFuelByStatus", ex.Message);
            }
            return model.OrderByDescending(m => m.TransactionDate).ToList();
        }

        [GridAction]
        public ActionResult SelectImportFuelDetail(int transactionId) {
            var model = new List<TransactionFptDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = (from tf in vfi.TransactionFpts
                                       where tf.TransactionId == transactionId
                                       select tf).FirstOrDefault();
                    if (transaction == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu");
                    foreach (var detail in transaction.TransactionFptDetails) {
                        var fuel = vfi.Fuels.FirstOrDefault(f => f.FuelId == detail.FptId);
                        var vendor = vfi.Vendors.FirstOrDefault(v => v.VendorId == detail.VendorId);
                        var entity = new TransactionFptDetailModel {
                            LotNumber = detail.LotNumber,
                            Note = detail.Note,
                            Quantity = detail.Quantity,
                            UnitPrice = detail.UnitPrice,
                            FuelCode = fuel.FuelCode,
                            FuelName = fuel.FuelName,
                            FuelDesignNo = fuel.FuelFullCode,
                            UnitMeasure = detail.UnitMeasure,
                            VendorId = vendor.VendorId,
                            VendorName = vendor.VendorName,
                            TotalInv = 0,
                        };
                        entity.Price = entity.UnitPrice * entity.Quantity;
                        if (!string.IsNullOrWhiteSpace(detail.LotNumber)) {
                            detail.LotNumber = detail.LotNumber.Trim();
                            var fuelInv =
                                vfi.FuelInventories.FirstOrDefault(
                                    fi => fi.FuelId == fuel.FuelId
                                        && fi.VendorId == vendor.VendorId 
                                        && fi.LotNumber.Equals(detail.LotNumber));
                            entity.TotalInv = fuelInv == null ? 0 : fuelInv.TotalQuantity;
                        }
                        if (transaction.EoI == (byte)MyUtilities.PurchaseOrder.EoILot.Export) {
                            var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == detail.MachineId);
                            if (machine != null) {
                                entity.Note += machine.MachineName;
                            }
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportFuelByStatus", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        public ActionResult UpdateApproveImportFuel(long[] checkedRecords) {
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
                        vfi.TransactionFpts.Where(f => checkedRecords.Contains(f.TransactionId) && 
                            f.Status == (byte)MyUtilities.Transaction.Status.Open);
                    foreach (var transaction in transactions) {
                        if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, transaction.TransactionDate)) {
                            throw new AggregateException(
                                @"Không có quyền duyệt phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                        }
                        var purchaseOrder =
                            vfi.PurchaseOrders.FirstOrDefault(
                                po => po.PurchaseOrderId == transaction.PoId);
                        if (transaction.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import) {// nhap nhien lieu
                            foreach (var detail in transaction.TransactionFptDetails) {
                                var fuelInv =
                                    vfi.FuelInventories.FirstOrDefault(
                                        fi =>
                                        fi.FuelId == detail.FptId && fi.LotNumber.Equals(detail.LotNumber) &&
                                        fi.VendorId == detail.VendorId);
                                var period = new FuelInventoryPeriod {
                                    //FuelInventory = fuelInv,
                                    FuelId = detail.FptId,
                                    TransactionId = transaction.TransactionId,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    Quantity = detail.Quantity,
                                    PeriodDate = transaction.TransactionDate,
                                };
                                if (fuelInv == null) {
                                    fuelInv = new FuelInventory {
                                        FuelId = detail.FptId,
                                        CreateDate = transaction.TransactionDate,
                                        ModifiedDate = DateTime.Now,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        LotNumber =
                                            MyUtilities.PurchaseOrder.GetFptLotParam((int)MyUtilities.PurchaseOrder.FptLot.Fuel, detail.FptId,
                                                                       transaction.TransactionDate, 0),
                                        UnitMeasure = detail.UnitMeasure,
                                        UnitPrice = detail.UnitPrice,
                                        TotalQuantity = detail.Quantity,
                                        VendorId = detail.VendorId,
                                    };
                                    vfi.FuelInventories.Add(fuelInv);
                                    period.EarlyQuantity = 0;
                                    period.LastQuantity = detail.Quantity;
                                }
                                else {
                                    period.EarlyQuantity = fuelInv.TotalQuantity;
                                    fuelInv.TotalQuantity += detail.Quantity;
                                    period.LastQuantity = fuelInv.TotalQuantity;
                                }
                                fuelInv.EndDate = null;
                                period.FuelInventory = fuelInv;
                                period.FuelInvId = fuelInv.FuelInvId;
                                detail.LotNumber = fuelInv.LotNumber;
                                if (purchaseOrder != null) {
                                    //var poDetail =
                                    //    vfi.PurchaseOrderDetails.FirstOrDefault(
                                    //        pod =>
                                    //        pod.PurchaseOrderId == purchaseOrder.PurchaseOrderId &&
                                    //        pod.ReferenceId == detail.FptId);
                                    var poDetail = purchaseOrder.PurchaseOrderDetails.FirstOrDefault(x => x.PurchaseOrderDetailId == detail.PoDetailId);
                                    if (poDetail != null) {
                                        poDetail.ReceivedQty += period.Quantity;
                                        if (poDetail.ReceivedQty >= poDetail.OrderQty)
                                            poDetail.IsComplete = true;

                                        var fuel = vfi.Fuels.FirstOrDefault(m => m.FuelId == poDetail.ReferenceId);
                                        if (fuel.UnitPrice < fuelInv.UnitPrice)
                                            fuel.UnitPrice = fuelInv.UnitPrice;
                                    }
                                }
                                vfi.FuelInventoryPeriods.Add(period);
                            }
                            if (purchaseOrder != null) {
                                var allcomplete =
                                    purchaseOrder.PurchaseOrderDetails.FirstOrDefault(po => po.IsComplete == false);
                                if (allcomplete == null)
                                    purchaseOrder.Status = (byte)MyUtilities.Sales.Status.Completed;
                            }
                        }
                        else {// la xuat nhien lieu
                            foreach (var detail in transaction.TransactionFptDetails) {
                                var fuelInv =
                                    vfi.FuelInventories.FirstOrDefault(
                                        fi =>
                                        fi.FuelId == detail.FptId &&
                                        fi.VendorId == detail.VendorId &&
                                        fi.LotNumber.Equals(detail.LotNumber));
                                if (fuelInv == null) {
                                    throw new AggregateException("Lỗi! Không tìm thấy tồn kho nhiên liệu");
                                }
                                if (fuelInv.TotalQuantity < detail.Quantity) {
                                    throw new AggregateException("Lỗi! Không đủ tồn kho nhiên liệu");
                                }
                                if (detail.MachineId != null && detail.MachineId != 0) {
                                    var smartProduction =
                                        vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == detail.MachineId);
                                    if (smartProduction == null) {
                                        smartProduction = new SmartProduction {
                                            MachineId = detail.MachineId,
                                            FuelInvId = fuelInv.FuelInvId,
                                            ModifiedDate = DateTime.Now,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                        };
                                        vfi.SmartProductions.Add(smartProduction);
                                    }
                                    else {
                                        smartProduction.FuelInvId = fuelInv.FuelInvId;
                                    }
                                }
                                var period = new FuelInventoryPeriod {
                                    FuelInventory = fuelInv,
                                    FuelInvId = fuelInv.FuelInvId,
                                    FuelId = fuelInv.FuelId,
                                    TransactionId = transaction.TransactionId,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    EarlyQuantity = fuelInv.TotalQuantity,
                                    Quantity = detail.Quantity,
                                    LastQuantity = fuelInv.TotalQuantity - detail.Quantity,
                                    PeriodDate = transaction.TransactionDate,
                                };
                                fuelInv.TotalQuantity = Math.Round(fuelInv.TotalQuantity - detail.Quantity, 2);
                                if (fuelInv.TotalQuantity == 0) {
                                    fuelInv.TotalQuantity = 0;
                                    fuelInv.EndDate = DateTime.Now;
                                }
                                else {
                                    fuelInv.EndDate = null;
                                }
                                vfi.FuelInventoryPeriods.Add(period);
                            }
                        }
                        transaction.Status = (byte)MyUtilities.Transaction.Status.Approved;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                return
                    Json("Lỗi! " + exception.Message);
            }
            return Json("okie");
        }

        [GridAction]
        public ActionResult CancelImportFuel(long transactionId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ImportWorkpiece",
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
                ModelState.AddModelError("CancelImportFuel ", ex.Message);

            }
            return View(new GridModel(GetImportFuelModel().OrderBy(m => m.TransactionDate)));
        }

        [HttpPost]
        public ActionResult PrintImportFuel(int importId) {
            var model = new List<TransactionFptDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.TransactionFpts.FirstOrDefault(i => i.TransactionId == importId);
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
                    foreach (var detail in transaction.TransactionFptDetails) {
                        var fuel = vfi.Fuels.FirstOrDefault(f => f.FuelId == detail.FptId);
                        var entity = new TransactionFptDetailModel {
                            LotNumber = detail.LotNumber,
                            Note = detail.Note,
                            Quantity = detail.Quantity,
                            UnitPrice = detail.UnitPrice,
                            FuelCode = fuel.FuelCode,
                            FuelName = fuel.FuelName,
                            FuelDesignNo = fuel.FuelFullCode,
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
                            FuelId = fuel.FuelId,
                            Info = info,
                        };
                        if (transaction.PoId != 0 && transaction.PoId != null)
                            entity.PoNumber = transaction.PurchaseOrder.RevisionNumber;
                        if (detail.VendorId > 0) {
                            var vendor = vfi.Vendors.FirstOrDefault(v => v.VendorId == detail.VendorId);
                            entity.VendorName = vendor.CompanyName;
                        }
                        var invs =
                            vfi.FuelInventoryPeriods.Where(
                                ti => ti.FuelId == entity.FuelId &&
                                      ti.PeriodDate <= transaction.TransactionDate);
                        if (invs.Any()) {
                            entity.TotalInv = invs.Sum(ti => ti.LastQuantity - ti.EarlyQuantity);
                        }
                        entity.StatusName =
                            MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status);
                        if (entity.FptType != 0)
                            entity.FptTypeName = MyUtilities.PurchaseOrder.GetFptName(entity.FptType);
                        if (entity.EoI != 0)
                            entity.EoIName = MyUtilities.PurchaseOrder.GetEoIName(entity.EoI, entity.Type);
                        if (entity.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import) {
                            entity.TransactionTitle = "PHIẾU NHẬP KHO";
                            if (transaction.Status == (byte)MyUtilities.Transaction.Status.Open) {
                                entity.TotalInv += entity.Quantity;
                            }
                        }
                        else if (entity.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Export) {
                            entity.TransactionTitle = "PHIẾU XUẤT KHO";
                            if (transaction.Status == (byte)MyUtilities.Transaction.Status.Open) {
                                entity.TotalInv -= entity.Quantity;
                            }
                        }
                        entity.Price = entity.UnitPrice * entity.Quantity;
                        model.Add(entity);
                    }
                    return PartialView("PagePrintImportFuel", model);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PagePrintImportFuel", "\n" + ex.Message);
            }
            return PartialView(null);
        }

        [GridAction]
        public ActionResult SelectImportFuel() {
            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateAssignFuelProduction(
            [Bind(Prefix = "inserted")] IEnumerable<SmartProductionModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<SmartProductionModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<SmartProductionModel> deletedDetails,
            string importDate) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateAssignFuelProduction",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SmartProductionModel>()));
            }
            if (updatedDetails.Any()) {
                try {
                    var date = MyUtilities.Function.ParseDate(importDate);
                    if (date > DateTime.Now.AddDays(1)) {
                        throw new AggregateException("Lỗi! Xem lại ngày ( lớn hơn hiện tại) !");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, date)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    if (insertedDetails != null)
                        updatedDetails = updatedDetails.Union(insertedDetails);
                    using (var vfi = new tammaContext()) {
                        // kiem tra ton kho nguyen lieu theo lo
                        var fuelInvIds = updatedDetails.Select(u => u.FuelInvId).Distinct();
                        foreach (var fuelInvId in fuelInvIds) {
                            var updateds = updatedDetails.Where(u => u.FuelInvId == fuelInvId);
                            if (string.IsNullOrWhiteSpace(updateds.FirstOrDefault().FuelFullCode)) continue;
                            var fuelInv =
                                vfi.FuelInventories.FirstOrDefault(
                                    fi => fi.FuelInvId == fuelInvId || fi.TotalQuantity > 0);
                            if (fuelInv == null) {
                                throw new AggregateException("Nhiên liệu " +
                                                             updateds.FirstOrDefault().FuelFullCode +
                                                             " đã hết!" + updateds.ToList().Select(u => u.MachineName));
                            }
                            var exportFuel = Math.Round(updateds.Sum(u => u.MaterialUse1 + u.MaterialUse2),
                                                                    2);
                            if (exportFuel > fuelInv.TotalQuantity) {
                                throw new AggregateException("Nhiên liệu " +
                                                             updateds.FirstOrDefault().FuelFullCode +
                                                             " không đủ!" + updateds.Select(u => u.MachineName).FirstOrDefault() +
                                                             "\n" +
                                                             (fuelInv.TotalQuantity - exportFuel));
                            }
                        }
                        var transaction = new TransactionFpt {
                            TransactionDate = date,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            PoId = null,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            TransactionFptDetails = new List<TransactionFptDetail>(),
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Fuel, 1),
                            EoI = Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Export),
                            Type = 1,
                            Fpt = Convert.ToInt16(MyUtilities.PurchaseOrder.FptLot.Fuel),
                            AccountantSignature = 0,
                            InventorySignature = 0,
                            PurchasingSignature = 0,
                            QcSignature = 0,
                            ExchangeRate = 1
                        };
                        //
                        foreach (var detail in updatedDetails) {
                            var quantity = Math.Round(detail.MaterialUse1 + detail.MaterialUse2, 2);
                            if (quantity == 0) continue;
                            var smartProduction =
                                   vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == detail.MachineId);
                            if (smartProduction == null) {
                                smartProduction = new SmartProduction();
                                smartProduction.MachineId = detail.MachineId;
                                smartProduction.FuelInvId = detail.FuelInvId;
                                vfi.SmartProductions.Add(smartProduction);
                            }
                            else {
                                smartProduction.FuelInvId = detail.FuelInvId;
                            }
                            var fuelInv =
                                vfi.FuelInventories.FirstOrDefault(
                                    m => m.FuelInvId == detail.FuelInvId);
                            var transactionDetail = new TransactionFptDetail {
                                FptId = fuelInv.FuelId,
                                Quantity = quantity,
                                UnitPrice = fuelInv.UnitPrice,
                                LotNumber = fuelInv.LotNumber,
                                Note = detail.Note,
                                UnitMeasure = fuelInv.UnitMeasure,
                                TransactionFpt = transaction,
                                TransactionId = transaction.TransactionId,
                                VendorId = fuelInv.VendorId,
                                MachineId = detail.MachineId,
                            };
                            transaction.TransactionFptDetails.Add(transactionDetail);
                        }
                        if (transaction.TransactionFptDetails.Any()) {
                            vfi.TransactionFpts.Add(transaction);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (DbEntityValidationException ex) {
                    foreach (var validationErrors in ex.EntityValidationErrors) {
                        foreach (var validationError in validationErrors.ValidationErrors) {
                            ModelState.AddModelError("db sx1",
                                                     string.Format("Property: {0} Error: {1}",
                                                                   validationError.PropertyName,
                                                                   validationError.ErrorMessage));
                        }
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("AssignFuel", ex.Message);
                }
            }
            
            return View(new GridModel(new List<SmartProductionModel>()));
        }

        [GridAction]
        public ActionResult UpdateImportFuel(
            [Bind(Prefix = "inserted")] IEnumerable<TransactionFptDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<TransactionFptDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<TransactionFptDetailModel> deletedDetails,
            int fuelVendor, string importDate, int poId, int exchangeRate, bool isInternal) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ImportWorkpiece",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<TransactionFptDetailModel>()));
            }
            try {
                try {
                    if (!insertedDetails.Any() || insertedDetails == null)
                        return View(new GridModel(new List<TransactionFptDetailModel>()));
                }
                catch {
                    if (!updatedDetails.Any() || updatedDetails == null)
                        return View(new GridModel(new List<TransactionFptDetailModel>()));
                }
                var date = MyUtilities.Function.ParseDate(importDate);
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
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Fuel, 1),
                        EoI = Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Import),
                        Type = 1,
                        Fpt = Convert.ToInt16(MyUtilities.PurchaseOrder.FptLot.Fuel),
                        AccountantSignature = 0,
                        InventorySignature = 0,
                        PurchasingSignature = 0,
                        QcSignature = 0,
                        ExchangeRate = exchangeRate,
                        IsInternal = isInternal
                    };
                    var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == poId);
                    if (purchaseOrder != null) {
                        fuelVendor = purchaseOrder.VendorId;
                        transaction.PoId = purchaseOrder.PurchaseOrderId;
                        if (!purchaseOrder.CurrencyCode.Trim().Equals("VND"))
                            if (transaction.ExchangeRate <= 1)
                                throw new AggregateException("Lỗi tiền tệ ! Chưa nhập tỉ giá.");
                    }
                    if (fuelVendor == 0)
                        throw new AggregateException("Lỗi nhà cung cấp ! Vui lòng kiểm tra lại.");
                    var list = insertedDetails == null
                                   ? updatedDetails
                                   : insertedDetails;
                    foreach (var detailModel in list) {
                        if (detailModel.FuelId == 0)
                            throw new AggregateException("Lỗi nhiên liệu ! Vui lòng kiểm tra lại.\n" +
                                                         detailModel.FuelFullCodeName);
                        if (detailModel.Quantity <= 0) continue;
                        if (detailModel.UnitPrice <= 0 && poId == 0)
                            throw new AggregateException("Lỗi đơn giá ! Vui lòng kiểm tra lại.\n" +
                                                         detailModel.FuelFullCodeName);
                        var detail = new TransactionFptDetail {
                            FptId = detailModel.FuelId,
                            Quantity = detailModel.Quantity,
                            UnitPrice = Math.Round(detailModel.UnitPrice * transaction.ExchangeRate, 0),
                            LotNumber = (detailModel.LotNumber + "").Trim(),
                            Note = detailModel.Note,
                            UnitMeasure = detailModel.UnitMeasure,
                            TransactionFpt = transaction,
                            TransactionId = transaction.TransactionId,
                            VendorId = fuelVendor,
                            PoDetailId = detailModel.PoDetailId,
                            IsInternal = transaction.IsInternal
                        };
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
                        ModelState.AddModelError("ExportWorkpieceMaterials",
                                                 "Property: " + validationError.PropertyName + " Error: " +
                                                 validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("ExportWorkpieceMaterials", "" + exception.Message);
            }
            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        public ActionResult GetFuelInvTotal(int fuelInvId) {
            try {
                //var intRevisionNumber = Convert.ToInt32(revisionNumber);
                using (var vfi = new tammaContext()) {
                    var fuelInv =
                        vfi.FuelInventories.FirstOrDefault(mi => mi.FuelInvId == fuelInvId);
                    return Json(fuelInv != null ? fuelInv.TotalQuantity : 0);
                }
            }
            catch (FormatException) {
                return Json(0);
            }
        }

        public ActionResult SelectComboBoxFuelInventory() {
            using (var vfi = new tammaContext()) {
                var fuelInvs = vfi.FuelInventories.Where(m => m.TotalQuantity > 0);
                var model = fuelInvs.Select(m => new FuelInventoryModel {
                    FuelInvId = m.FuelInvId,
                    FuelCode = m.Fuel.FuelFullCode,
                    LotNumber = m.LotNumber,
                    VendorCode = m.Vendor.VendorCode
                });
                return new JsonResult {
                    Data = new SelectList(model.ToList(), "FuelInvId", "FuelFullCode")
                };
            }
        }


        public ActionResult SelectComboBoxFuel() {
            using (var vfi = new tammaContext()) {
                var fuelInvs = vfi.Fuels.Where(m => m.Active && m.FuelName.Equals("Bao bì"));
                var model = fuelInvs.Select(m => new FuelModel() {
                    FuelId = m.FuelId,
                    FuelFullCode = m.FuelFullCode,
                    FuelCode = m.FuelCode,
                });
                return new JsonResult {
                    Data = new SelectList(model.ToList(), "FuelId", "FuelFullCode"),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        [GridAction]
        public ActionResult SelectExportFuelInv(string fuelCode, int vendorId) {
            var model = new List<TransactionFptDetailModel>();
            using (var vfi = new tammaContext()) {
                if (string.IsNullOrWhiteSpace(fuelCode) && vendorId == 0)
                    return View(new GridModel(model));
                var fuels = vfi.Fuels.Where(f => f.Active);
                if (!string.IsNullOrWhiteSpace(fuelCode))
                    fuels = fuels.Where(m => m.FuelFullCode.Contains(fuelCode));
                foreach (var fuel in fuels) {
                    var fuelInvs =
                        vfi.FuelInventories.Where(
                            fi =>
                            fi.FuelId == fuel.FuelId && fi.TotalQuantity > 0 &&
                            (vendorId == 0 || fi.VendorId == vendorId));
                    if (fuelInvs.Any()) {
                        foreach (var fuelInv in fuelInvs) {
                            var entity = new TransactionFptDetailModel {
                                FuelId = fuelInv.FuelId,
                                TotalInv = fuelInv.TotalQuantity,
                                FuelCode = fuel.FuelCode,
                                FuelName = fuel.FuelName,
                                FuelDesignNo = fuel.FuelDesignNo,
                                FuelFullCodeName = fuel.FuelFullCode,
                                LotNumber = fuelInv.LotNumber,
                                FuelInvId = fuelInv.FuelInvId,
                                VendorId = fuelInv.VendorId,
                                VendorCode = fuelInv.Vendor.VendorCode,
                                VendorName = fuelInv.Vendor.VendorName,
                                UnitMeasure = fuelInv.UnitMeasure,
                                TransactionDate = fuelInv.CreateDate
                            };
                            model.Add(entity);
                        }
                    }
                }
            }
            return View(new GridModel(model.OrderBy(m => m.FuelCode)));
        }

        [GridAction]
        public ActionResult SelectExportInputFuelInventory(string invIds) {
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
            var lstIds = (List<int>)Session["ListExportFuelInvIds"];
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
                foreach (var id in lstIds) {
                    var fuelInv = vfi.FuelInventories.FirstOrDefault(mi => mi.FuelInvId == id);
                    if ((fuelInv.TotalQuantity) == 0) continue;
                    var entity = new TransactionFptDetailModel {
                        FuelId = fuelInv.FuelId,
                        TotalInv = fuelInv.TotalQuantity,
                        FuelCode = fuelInv.Fuel.FuelCode,
                        FuelName = fuelInv.Fuel.FuelName,
                        FuelDesignNo = fuelInv.Fuel.FuelDesignNo,
                        FuelFullCodeName = fuelInv.Fuel.FuelFullCode,
                        LotNumber = fuelInv.LotNumber,
                        FuelInvId = fuelInv.FuelInvId,
                        VendorId = fuelInv.VendorId,
                        VendorCode = fuelInv.Vendor.VendorCode,
                        VendorName = fuelInv.Vendor.VendorName,
                        UnitMeasure = fuelInv.UnitMeasure,
                        TransactionDate = fuelInv.CreateDate
                    };
                    model.Add(entity);
                }
                Session["ListExportFuelInvIds"] = lstIds;
                return View(new GridModel(model));
            }
        }


        [GridAction]
        public ActionResult ExportTransactionFuelInventory(
            [Bind(Prefix = "inserted")]IEnumerable<TransactionFptDetailModel> insertedDetails,
            [Bind(Prefix = "updated")]IEnumerable<TransactionFptDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")]IEnumerable<TransactionFptDetailModel> deletedDetails,
            string exportDate, bool isInternal
            ) {
            if (updatedDetails != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

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
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Fuel, 1),
                            EoI = Convert.ToInt16(MyUtilities.PurchaseOrder.EoILot.Export),
                            Type = 1,
                            Fpt = Convert.ToInt16(MyUtilities.PurchaseOrder.FptLot.Fuel),
                            AccountantSignature = 0,
                            InventorySignature = 0,
                            PurchasingSignature = 0,
                            QcSignature = 0,
                            ExchangeRate = 1,
                            IsInternal = isInternal
                        };
                        //
                        foreach (var detail in updatedDetails) {
                            var fuelInv =
                                vfi.FuelInventories.FirstOrDefault(
                                    m => m.FuelInvId == detail.FuelInvId);
                            if (fuelInv == null)
                                throw new AggregateException("Lỗi không tìm thấy tồn kho! " + detail.FuelFullCodeName);
                            if (detail.IsDestroy) detail.Quantity = fuelInv.TotalQuantity;
                            if (detail.Quantity == 0) continue;
                            if (fuelInv.TotalQuantity < detail.Quantity)
                                throw new AggregateException("Lỗi không đủ tồn kho! " + detail.FuelFullCodeName);
                            if (fuelInv.CreateDate > date)
                                throw new AggregateException("Lỗi ngày xuất trước ngày nhập kho! " + detail.FuelFullCodeName);
                            var transactionDetail = new TransactionFptDetail {
                                FptId = fuelInv.FuelId,
                                Quantity = detail.Quantity,
                                UnitPrice = fuelInv.UnitPrice,
                                LotNumber = fuelInv.LotNumber,
                                Note = detail.Note,
                                UnitMeasure = fuelInv.UnitMeasure,
                                TransactionFpt = transaction,
                                TransactionId = transaction.TransactionId,
                                VendorId = fuelInv.VendorId,
                                MachineId = detail.MachineId,
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
                    ModelState.AddModelError("MaterialCodeName", "" + exception.Message);
                }
            }

            Session["ListExportFuelInvIds"] = new List<int>();

            return View(new GridModel(new List<TransactionFptDetailModel>()));
        }

        [HttpPost]
        public ActionResult PrintFuelInvTotalInMonth(int vendorId, int month, int year) {
            var model = new List<GroupFuelInventory>();
            var date = new DateTime(year, month, 1).AddMonths(1).AddSeconds(-1);
            var startDate = new DateTime(year, month, 1).AddSeconds(-1);
            var lastDate = date;
            try {
                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var fuels = (from t in vfi.Fuels
                                 where t.Active
                                 orderby t.FuelFullCode
                                 select new {
                                     t.FuelId,
                                     t.FuelCode,
                                     t.FuelDesignNo,
                                     t.FuelName,
                                     t.FuelFullCode,
                                 }).ToList();
                    var fuelIds = fuels.Select(t => t.FuelId).ToList();
                    var fuelInvs = (from ti in vfi.FuelInventories
                                    where fuelIds.Contains(ti.FuelId)
                                    && (ti.EndDate == null || ti.EndDate >= startDate)
                                    && (vendorId == 0 || ti.VendorId == vendorId)
                                    select new {
                                        ti.FuelId,
                                        ti.FuelInvId,
                                        ti.Vendor.VendorCode,
                                        ti.VendorId,
                                        ti.LotNumber,
                                        ti.UnitPrice,
                                        ti.UnitMeasure
                                    }).ToList();
                    var fuelPeriods = (from tip in vfi.FuelInventoryPeriods
                                       where fuelIds.Contains(tip.FuelId)
                                             && tip.PeriodDate < date
                                       orderby tip.PeriodDate
                                       select new {
                                           tip.PeriodDate,
                                           tip.FuelInvId,
                                           tip.LastQuantity,
                                           tip.EarlyQuantity,
                                           tip.Quantity,
                                           IsInternal = tip.TransactionFpt.IsInternal == true,
                                           IsPurchase = tip.TransactionFpt.PoId != null
                                       }).ToList();
                    if (fuelPeriods.Any()) {
                        lastDate = fuelPeriods.LastOrDefault().PeriodDate;
                    }
                    var vendorIds = fuelInvs.Select(fi => fi.VendorId).Distinct().ToList();
                    var vendors = (from v in vfi.Vendors
                                   where vendorIds.Contains(v.VendorId) && v.Active
                                   orderby v.ShortName
                                   select new { 
                                       v.VendorId,
                                       v.ShortName
                                   }).ToList();

                    var exportFuels = (from x in vfi.TransactionFptDetails
                                       where x.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved
                                        && x.TransactionFpt.Fpt == (byte)MyUtilities.PurchaseOrder.FptLot.Fuel
                                        && x.TransactionFpt.EoI == (byte)MyUtilities.PurchaseOrder.EoILot.Export
                                        && fuelIds.Contains(x.FptId)
                                        && x.TransactionFpt.TransactionDate >= startDate
                                        && x.TransactionFpt.TransactionDate <= lastDate
                                        && x.IsInternal != true
                                        && x.MachineId > 0
                                       select new {
                                           x.FptId,
                                           x.LotNumber,
                                           x.Quantity
                                       }).ToList();
                    foreach (var vendor in vendors) {
                        var group = new GroupFuelInventory {
                            List = new List<FuelInventoryModel>(),
                            MaterialTypeId = vendor.VendorId,
                            GroupName = vendor.ShortName,
                            LastPeriodDateString = lastDate.ToString("dd/MM/yyyy"),
                            ReportDateString = date.ToString("MM/yyyy")
                        };
                        //foreach (var fuelInv in fuelInvByIds)
                        //{
                        //var toolInvsById = toolInvs.Where(ti => ti.ToolId == tool.ToolId);
                        var fuelInvByIds = fuelInvs.Where(t => t.VendorId == vendor.VendorId).ToList();
                        foreach (var fuelInv in fuelInvByIds) {
                            var fuel = fuels.FirstOrDefault(f => f.FuelId == fuelInv.FuelId);
                            var entity = new FuelInventoryModel {
                                FuelInvId =  fuelInv.FuelInvId,
                                FuelId = fuelInv.FuelId,
                                LotNumber = fuelInv.LotNumber,
                                FuelCode = fuel.FuelCode,
                                VendorCode = fuelInv.VendorCode,
                                UnitPrice = fuelInv.UnitPrice,
                                UnitMeasure = fuelInv.UnitMeasure,
                                FuelDesignNo = fuel.FuelDesignNo,
                                FuelName = fuel.FuelName,
                                FuelFullCode = fuel.FuelFullCode,
                            };
                            var periodsById = fuelPeriods.Where(tip => tip.FuelInvId == entity.FuelInvId).ToList();
                            var periods = periodsById.Where(tip => tip.PeriodDate < startDate).ToList();

                            entity.Early = periods.Sum(tip => tip.LastQuantity - tip.EarlyQuantity);
                            periods = periodsById.Where(tip => tip.PeriodDate >= startDate && tip.LastQuantity > tip.EarlyQuantity)
                                                    .ToList();
                            entity.Import = periods.Where(x => x.IsPurchase).Sum(tip => tip.Quantity);
                            entity.ImportMore = periods.Where(x => !x.IsInternal && !x.IsPurchase).Sum(tip => tip.Quantity);

                            entity.ImportInternal = periods.Where(x => x.IsInternal).Sum(tip => tip.Quantity);

                            periods = periodsById.Where(tip => tip.PeriodDate >= startDate && tip.LastQuantity < tip.EarlyQuantity)
                                                .ToList();
                            entity.ExportDestroy = periods.Where(x => !x.IsInternal).Sum(tip => tip.Quantity);
                            entity.ExportInternal = periods.Where(x => x.IsInternal).Sum(tip => tip.Quantity);
                            var exportsById = exportFuels.Where(x => x.FptId == entity.FuelId && x.LotNumber.Equals(entity.LotNumber));
                            if (exportsById.Any()) {
                                entity.Export = exportsById.Sum(x => x.Quantity);
                                entity.ExportDestroy -= entity.Export;
                            }
                            if (entity.IsShow) {
                                group.List.Add(entity);
                            }
                        }
                        if (group.List.Any()) {
                            group.List = group.List.OrderBy(l => l.FuelCode)
                                              .ThenBy(l => l.FuelDesignNo)
                                              .ThenBy(l => l.LotNumber)
                                              .ToList();
                            model.Add(group);
                        }
                    }
                }
            }
            catch (Exception ex) {
                return PartialView("PageFuelTotal", ex.Message);
            }
            return PartialView("PageFuelTotal", model);
        }

    }
}
