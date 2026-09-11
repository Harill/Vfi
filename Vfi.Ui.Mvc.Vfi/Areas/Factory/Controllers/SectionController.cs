using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Utilities;

namespace Vfi.Ui.Mvc.Vfi.Areas.Factory.Controllers {
    public class SectionController : Controller {
        //
        // GET: /Factory/Section/
        #region view

        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }

            return ViewData;
        }
        public ActionResult SectionManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ExportToProduction2() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionProductId"] = new List<int>();
            return View();
        }

        public ActionResult RotateSection() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionSectionInvId"] = new List<int>();
            return View();
        }

        public ActionResult ApproveTransactionProduction2() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult TransactionProduction2Management() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult SectionInventory() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ManagementProduction2() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ExportSection() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ImportSection() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult SectionPeriodDetail() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult Production2Plan() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult SectionProcessManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        #endregion

        public ActionResult AutoCompletedProductCodeSection(string text) {
            var model = new List<string>();
            using (var vfi = new tammaContext()) {
                var products = (from p in vfi.Products
                                where p.ProductCode.ToUpper().Contains(text.ToUpper())
                                      && p.ProductionSections.Any(ps => ps.Active)
                                orderby p.ProductCode
                                select p.ProductCode).ToList();
                model.AddRange(products);
            }
            return new JsonResult {
                Data = model,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult AutoCompletedProductCodeSectionInv(string text) {
            var model = new List<string>();
            using (var vfi = new tammaContext()) {
                //var products = (from p in vfi.Products
                //                where p.ProductCode.ToUpper().Contains(text.ToUpper())
                //                      && p.ProductionSections.Any(ps => ps.Active)
                //                orderby p.ProductCode
                //                select p.ProductCode).ToList();
                var sectionInvs = (from si in vfi.Production2Inventory
                                   where si.ProductionSection.Product.ProductCode.ToUpper().Contains(text.ToUpper())
                                         && si.TotalQuantity > 0
                                   orderby si.ProductionSection.Product.ProductCode
                                   select si.ProductionSection.Product.ProductCode).ToList();
                model.AddRange(sectionInvs);
            }
            return new JsonResult {
                Data = model,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxMachineProduction2() {
            using (var vfi = new tammaContext()) {
                //var model = vfi.Machines.Where(m => m.Active).ToList();
                var model = from m in vfi.Machines
                            where m.Production2
                            select new {
                                m.MachineId,
                                m.MachineName
                            };
                return new JsonResult {
                    Data = new SelectList(model.ToList(), "MachineId", "MachineName")
                };
            }
        }

        [GridAction]
        public ActionResult CancelProduction2Transaction(long transactionId) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

            var modifiedUser = HttpContext.User.Identity.Name;
            if (string.IsNullOrWhiteSpace(modifiedUser))
                return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
            try {
                using (var vfi = new tammaContext()) {
                    var transaction = vfi.Production2Transaction.FirstOrDefault(t => t.TransactionId == transactionId);
                    if (transaction == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu!");
                    transaction.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("SelectProduction2Status", "" + exception.Message);
            }
            return View(new GridModel(GetProduction2Transactions((byte)MyUtilities.Transaction.Status.Open, "", "")));
        }

        private List<Production2TransactionModel> GetProduction2Transactions(byte? status, string fromDate,
                                                                             string toDate) {
            var model = new List<Production2TransactionModel>();
            var ci = new CultureInfo("vi-VN");
            DateTime? fDate = null;
            if (!string.IsNullOrWhiteSpace(fromDate))
                fDate = Convert.ToDateTime(fromDate, ci);
            DateTime? tDate = null;
            if (!string.IsNullOrWhiteSpace(toDate))
                tDate = Convert.ToDateTime(toDate, ci);
            try {
                using (var vfi = new tammaContext()) {
                    var transactions = from t in vfi.Production2Transaction
                                       where t.Status == status
                                             && (fDate == null || (t.CreateDate >= fDate && t.CreateDate <= tDate))
                                       select t;
                    foreach (var transaction in transactions) {
                        var entity = new Production2TransactionModel {
                            TransactionId = transaction.TransactionId,
                            CreateDate = transaction.CreateDate,
                            ModifiedDate = transaction.ModifiedDate,
                            ModifiedUser = transaction.ModifiedUser,
                            TransactionCode = transaction.TransactionCode,
                            EoIName = MyUtilities.Transaction.CastText.GetTextEoI(transaction.EoI),
                            StatusName = MyUtilities.Transaction.CastText.GetTextStatus(transaction.Status),
                            TotalQuantity = transaction.Production2TransactionDetail.Sum(td => td.QuantityKg),
                            TotalLost = transaction.Production2TransactionDetail.Sum(td => td.QuantityLost),
                            TotalQuantityDefect =
                                transaction.Production2TransactionDetail.Sum(td => td.QuantityDefect),
                            Time = transaction.Production2TransactionDetail.Sum(td => td.Time),
                            OverTime = transaction.Production2TransactionDetail.Sum(td => td.OverTime),
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("SelectProduction2Status", "" + exception.Message);
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectProduction2Transaction(byte? status, string fromDate, string toDate) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

            var modifiedUser = HttpContext.User.Identity.Name;
            if (string.IsNullOrWhiteSpace(modifiedUser))
                return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
            var model = new List<Production2TransactionModel>();
            try {
                model =
                    GetProduction2Transactions(status, fromDate, toDate)
                        .OrderBy(m => m.CreateDate)
                        .ThenBy(m => m.ModifiedDate)
                        .ToList();
            }
            catch (Exception exception) {
                ModelState.AddModelError("SelectProduction2Status", "" + exception.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectTransactionDetail(long transactionId) {
            var model = new List<Production2TransactionDetailModel>();
            if (transactionId == 0)
                return View(new GridModel(model));
            using (var vfi = new tammaContext()) {
                var transaction = vfi.Production2Transaction.FirstOrDefault(t => t.TransactionId == transactionId);
                foreach (var detail in transaction.Production2TransactionDetail) {
                    var entity = new Production2TransactionDetailModel {
                        ProductCode = detail.Product.ProductCode,
                        Note = detail.Note,
                        Quantity = detail.Quantity,
                        QuantityKg = detail.QuantityKg,
                        UnitMeasure = "Kg",
                        SectionIssueName =
                            detail.ProductionSection != null
                                ? detail.ProductionSection.SectionIndex + "." +
                                  detail.ProductionSection.Section.SectionName
                                : "*.Kho SX 2",
                        SectionReceiptName =
                            detail.ProductionSection1 != null
                                ? detail.ProductionSection1.SectionIndex + "." +
                                  detail.ProductionSection1.Section.SectionName
                                : "*.Hoàn thành",
                        QuantityDefect = detail.QuantityDefect,
                        QuantityLost = detail.QuantityLost,
                        SectionIndex = 0,
                        Time = detail.Time,
                        OverTime = detail.OverTime,
                    };
                    if (detail.ProductionSection != null) {
                        var sectionInv =
                            vfi.Production2Inventory.FirstOrDefault(
                                si => si.ProductionSectionId == detail.SectionIssueId);
                        if (sectionInv != null)
                            entity.SectionIssueInv = sectionInv.TotalQuantity;
                        entity.SectionIndex = detail.ProductionSection.SectionIndex;
                    }
                    if (transaction.EoI == Convert.ToChar(MyUtilities.Transaction.EoIEnum.Export).ToString()) {
                        if (detail.Machine != null && detail.Employee != null)
                            entity.Note += ("! " + detail.Machine.MachineName + "-" + detail.Employee.EmployeeName +
                                            " ! ");
                    }
                    model.Add(entity);
                }
            }
            return View(new GridModel(model.OrderBy(m => m.ProductCode).ThenBy(m => m.SectionIndex)));
        }

        [HttpPost]
        public ActionResult ApproveTransactionSection(long[] checkedRecords) {
            var j = 1;
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
                        vfi.Production2Transaction.Where(
                            f =>
                            checkedRecords.Contains(f.TransactionId) &&
                            f.Status != (byte)MyUtilities.Transaction.Status.Approved &&
                            f.Status != (byte)MyUtilities.Transaction.Status.Cancel).ToList();
                    foreach (var transaction in transactions) {

                        var listPeriod = new List<Production2InventoryPeriod>();
                        //var listProduction2Inventory = new List<Production2Inventory>();
                        foreach (var detail in transaction.Production2TransactionDetail.OrderBy(td => td.SectionIndex)) {
                            if (detail.SectionReceiptId != null) {
                                var invReceipt =
                                    vfi.Production2Inventory.FirstOrDefault(
                                        pi =>
                                        pi.ProductionSectionId == detail.SectionReceiptId);
                                if (invReceipt == null) {
                                    invReceipt = new Production2Inventory {
                                        ProductionSectionId = detail.SectionReceiptId,
                                        TotalQuantity = 0,
                                        Weight = detail.ProductionSection1.Weight,
                                    };
                                    if (detail.SectionIssueId == null)
                                        invReceipt.Weight = detail.Product.ProductionWeight ?? 0;
                                    else {
                                        var sectionnReceipt =
                                            vfi.ProductionSections.FirstOrDefault(
                                                ps =>
                                                ps.ProductionSectionId == detail.SectionReceiptId);
                                        if (sectionnReceipt == null)
                                            throw new AggregateException("Lỗi! Không tìm thấy công đoạn nhập " +
                                                                         detail.Product.ProductCode + " ! Liên hệ admin");
                                        invReceipt.Weight = sectionnReceipt.Weight;
                                    }
                                    vfi.Production2Inventory.Add(invReceipt);
                                    vfi.SaveChanges();
                                    //listProduction2Inventory.Add(invReceipt);
                                }
                                var periodReceipt = new Production2InventoryPeriod {
                                    EarlyQuantity = invReceipt.TotalQuantity,
                                    Quantity = detail.QuantityKg,
                                    LastQuantity = Math.Round(invReceipt.TotalQuantity + detail.QuantityKg, 3),
                                    PeriodDate = transaction.CreateDate,
                                    TransactionId = transaction.TransactionId,
                                    Production2Inventory = invReceipt,
                                    Weight = invReceipt.Weight,
                                };
                                invReceipt.TotalQuantity = Math.Round(invReceipt.TotalQuantity + detail.QuantityKg, 3);
                                listPeriod.Add(periodReceipt);
                            }
                            if (detail.SectionIssueId != null) {
                                var sectionIssue =
                                    vfi.Production2Inventory.FirstOrDefault(
                                        pi => pi.ProductionSectionId == detail.SectionIssueId);
                                //if (sectionIssue == null)
                                //    sectionIssue = listProduction2Inventory.FirstOrDefault(
                                //        pi => pi.ProductionSectionId == detail.SectionIssueId);
                                if (sectionIssue == null) {
                                    sectionIssue = new Production2Inventory {
                                        ProductionSectionId = detail.SectionIssueId,
                                        TotalQuantity = 0,
                                        Weight = detail.ProductionSection.Weight,
                                    };
                                    vfi.Production2Inventory.Add(sectionIssue);
                                    vfi.SaveChanges();
                                    //throw new AggregateException("Lỗi! Không tìm thấy công đoạn xuất " +
                                    //                             detail.Product.ProductCode + " ! Liên hệ admin");
                                }
                                if (detail.QuantityKg > 0) {
                                    var periodIssue = new Production2InventoryPeriod {
                                        EarlyQuantity = sectionIssue.TotalQuantity,
                                        Quantity = detail.QuantityKg,
                                        LastQuantity = Math.Round(sectionIssue.TotalQuantity - detail.QuantityKg, 3),
                                        PeriodDate = transaction.CreateDate,
                                        TransactionId = transaction.TransactionId,
                                        InvId = sectionIssue.InvId,
                                        Weight = sectionIssue.Weight,
                                    };
                                    sectionIssue.TotalQuantity =
                                        Math.Round(sectionIssue.TotalQuantity - detail.QuantityKg, 3);
                                    listPeriod.Add(periodIssue);
                                }
                                if (detail.QuantityDefect > 0) {
                                    var periodIssueDefect = new Production2InventoryPeriod {
                                        EarlyQuantity = sectionIssue.TotalQuantity,
                                        Quantity = detail.QuantityDefect,
                                        LastQuantity =
                                            Math.Round(sectionIssue.TotalQuantity - detail.QuantityDefect, 3),
                                        PeriodDate = transaction.CreateDate,
                                        TransactionId = transaction.TransactionId,
                                        InvId = sectionIssue.InvId,
                                        Weight = sectionIssue.Weight,
                                    };
                                    sectionIssue.TotalQuantity =
                                        Math.Round(sectionIssue.TotalQuantity - detail.QuantityDefect, 3);
                                    listPeriod.Add(periodIssueDefect);
                                }
                                if (detail.QuantityLost > 0) {
                                    var periodIssueLost = new Production2InventoryPeriod {
                                        EarlyQuantity = sectionIssue.TotalQuantity,
                                        Quantity = detail.QuantityLost,
                                        LastQuantity =
                                            Math.Round(sectionIssue.TotalQuantity - detail.QuantityLost, 3),
                                        PeriodDate = transaction.CreateDate,
                                        TransactionId = transaction.TransactionId,
                                        InvId = sectionIssue.InvId,
                                        Weight = sectionIssue.Weight,
                                    };
                                    sectionIssue.TotalQuantity =
                                        Math.Round(sectionIssue.TotalQuantity - detail.QuantityLost, 3);
                                    listPeriod.Add(periodIssueLost);
                                }
                            }
                            j++;
                        }
                        transaction.Status = (byte)MyUtilities.Transaction.Status.Approved;
                        vfi.Production2InventoryPeriod.AddRange(listPeriod);
                        //if (listProduction2Inventory.Any())
                        //    vfi.Production2Inventory.AddRange(listProduction2Inventory);
                        vfi.SaveChanges();
                        return Json("Ok all");
                    }
                }
            }
            catch (Exception exception) {
                //ModelState.AddModelError("SelectProduction2Status", "" + exception.Message);
                return Json(exception.Message);
            }
            return Json("null");
        }


        public ActionResult SelectComboBoxProductProduction2() {
            using (var vfi = new tammaContext()) {
                //var model = vfi.Machines.Where(m => m.Active).ToList();
                var model = from m in vfi.Products
                            where m.ProductionSections.Any(ps => ps.Active)
                            orderby m.ProductCode
                            select new {
                                m.ProductId,
                                m.ProductCode,
                            };
                return new JsonResult {
                    Data = new SelectList(model.ToList(), "ProductId", "ProductCode")
                };
            }
        }

        [HttpPost]
        public ActionResult GetProductSection(int productId, int productionSectionId, int employeeType) {
            try {

                using (var vfi = new tammaContext()) {
                    var section = (from pp in vfi.ProductionSections
                                   where pp.ProductId == productId
                                         && pp.Active
                                         && (productionSectionId == 0 || pp.ProductionSectionId == productionSectionId)
                                   orderby pp.SectionIndex
                                   select pp).FirstOrDefault();
                    if (section == null)
                        return Json("8");
                    //var productionSection =
                    //    vfi.ProductionSections.FirstOrDefault(ps => ps.ProductionSectionId == productionSectionId);
                    var entity = new Production2Model();
                    var lastTransactionDetail = (from td in vfi.Production2TransactionDetail
                                                 orderby td.Production2Transaction.CreateDate descending
                                                 where
                                                     td.Production2Transaction.Status ==
                                                     (byte)MyUtilities.Transaction.Status.Approved
                                                     && td.ProductId == productId
                                                     &&
                                                     (productionSectionId == 0
                                                          ? td.SectionIssueId == section.ProductionSectionId
                                                          : td.SectionIssueId == productionSectionId)
                                                     && td.Production2Transaction.EoI == "1"
                                                 select td).FirstOrDefault();
                    if (lastTransactionDetail != null) {
                        entity.ProductionSectionId = lastTransactionDetail.SectionIssueId.Value;
                        entity.SectionName = lastTransactionDetail.ProductionSection.SectionIndex + "." +
                                             lastTransactionDetail.ProductionSection.Section.SectionName;
                        entity.SectionProductivity = lastTransactionDetail.ProductionSection.Productivity;
                        entity.Weight = lastTransactionDetail.ProductionSection.Weight;
                        var inv =
                            vfi.Production2Inventory.FirstOrDefault(
                                pi => pi.ProductionSectionId == productionSectionId);
                        if (inv != null)
                            entity.TotalInventory = inv.TotalQuantity;
                        if (lastTransactionDetail.ProductionSection1 == null) {
                            entity.SectionNext = 0;
                            entity.SectionNextName = "*.Hoàn thành";
                        }
                        else {
                            entity.SectionNext = lastTransactionDetail.SectionReceiptId.Value;
                            entity.SectionNextName = lastTransactionDetail.ProductionSection1.SectionIndex + "." +
                                                     lastTransactionDetail.ProductionSection1.Section.SectionName;
                        }
                        if (lastTransactionDetail.Machine == null) {
                            entity.MachineId = 0;
                            entity.MachineName = "";
                        }
                        else {
                            entity.MachineId = lastTransactionDetail.MachineId.Value;
                            entity.MachineName = lastTransactionDetail.Machine.MachineName;

                        }
                        if (lastTransactionDetail.Employee == null) {
                            entity.EmployeeId = 0;
                            entity.EmployeeName = "";
                        }
                        else {
                            entity.EmployeeId = lastTransactionDetail.EmployeeId.Value;
                            entity.EmployeeName = lastTransactionDetail.Employee.EmployeeCode + "-" +
                                                  lastTransactionDetail.Employee.EmployeeName;
                        }
                    }
                    else {
                        entity.ProductionSectionId = section.ProductionSectionId;
                        entity.SectionName = section.SectionIndex + "." + section.Section.SectionName;
                        entity.SectionProductivity = section.Productivity;
                        entity.Weight = section.Weight;
                    }
                    entity.Time = 8;
                    switch (employeeType) {
                        case 2:
                            break;
                        case 3:
                            entity.Time = 12;
                            break;
                        default:
                            break;

                    }
                    entity.Quantity = entity.SectionProductivityKgInHour * entity.Time;
                    //return Json(sectionInv);
                    return Json(entity);
                }
            }
            catch (Exception) {
                return Json("0");
            }
            return Json("0");
        }

        public ActionResult SelectComboBoxActiveSection() {
            var model = new List<SectionModel>();
            using (var vfi = new tammaContext()) {
                model = (from pp in vfi.Sections
                         where pp.Active
                         orderby pp.SectionName
                         select new SectionModel {
                             SectionId = pp.SectionId,
                             SectionName = pp.SectionName,
                         }).ToList();
            }
            return new JsonResult {
                Data =
                    new SelectList(model, "SectionId", "SectionName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxSection(int productId) {
            var model = new List<ProductionSectionModel>();
            if (productId != 0)
                using (var vfi = new tammaContext()) {
                    var productSections = from pp in vfi.ProductionSections
                                          where pp.Active && (productId == 0 || pp.ProductId == productId)
                                          orderby pp.SectionIndex
                                          select new {
                                              pp.ProductionSectionId,
                                              pp.Section.SectionName,
                                              pp.SectionIndex,
                                          };
                    foreach (var productSection in productSections) {
                        var entity = new ProductionSectionModel {
                            ProductionSectionId = productSection.ProductionSectionId,
                            SectionName = productSection.SectionIndex + "." + productSection.SectionName,
                        };
                        model.Add(entity);
                    }
                }
            return new JsonResult {
                Data =
                    new SelectList(model, "ProductionSectionId", "SectionName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxNextSection(int productId) {
            var model = new List<ProductionSectionModel>();
            if (productId != 0)
                using (var vfi = new tammaContext()) {
                    var productSections = from pp in vfi.ProductionSections
                                          where pp.Active && (productId == 0 || pp.ProductId == productId)
                                          orderby pp.SectionIndex
                                          select new {
                                              pp.ProductionSectionId,
                                              pp.Section.SectionName,
                                              pp.SectionIndex,
                                          };
                    foreach (var productSection in productSections) {
                        var entity = new ProductionSectionModel {
                            ProductionSectionId = productSection.ProductionSectionId,
                            SectionName = productSection.SectionIndex + "." + productSection.SectionName,
                        };
                        model.Add(entity);
                    }
                    var finish = new ProductionSectionModel {
                        ProductionSectionId = 0,
                        SectionName = "*.Hoàn thành",
                    };
                    model.Add(finish);
                }
            return new JsonResult {
                Data =
                    new SelectList(model, "ProductionSectionId", "SectionName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxSectionByProductId(int productId) {
            var model = new List<Section>();
            try {
                using (var vfi = new tammaContext()) {
                    var sections = from ps in vfi.ProductionSections
                                   where ps.Active && ps.ProductId == productId
                                   orderby ps.SectionIndex
                                   select new {
                                       ps.SectionId,
                                       ps.Section.SectionName
                                   };
                    foreach (var section in sections) {
                        model.Add(new Section() { SectionId = section.SectionId, SectionName = section.SectionName });
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectComboBoxSectionByProductId", ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(model, "SectionId", "SectionName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxSectionNextProcess(int processId) {
            var model = new List<SectionProcessModel>();
            if (processId != 0) {
                using (var vfi = new tammaContext()) {
                    var sectionProcess = vfi.SectionProcesses.FirstOrDefault(sp => sp.ProcessId == processId);
                    if (sectionProcess == null)
                        throw new AggregateException("Lỗi! Không tìm thấy công đoạn gia công");
                    var sectionProcesses = (from sp in vfi.SectionProcesses
                                            where sp.Active &&
                                                 sp.ProductId == sectionProcess.ProductId &&
                                                 sp.ProcessId != processId
                                            orderby sp.StartProcess descending, sp.EndProcess, sp.ProcessName
                                            select new {
                                                sp.ProcessId,
                                                sp.ProcessName
                                            }).ToList();
                    foreach (var section in sectionProcesses) {
                        var entity = new SectionProcessModel {
                            ProcessId = section.ProcessId,
                            ProcessName = section.ProcessName,
                        };
                        model.Add(entity);
                    }
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "ProcessId", "ProcessName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [GridAction]
        public ActionResult SelectProduction2Rotate(int employeeType) {
            var model = new List<Production2Model>();
            try {
                using (var vfi = new tammaContext()) {
                    var smartProduction2s = vfi.SmartProduction2.ToList();
                    switch (employeeType) {
                        case 2:
                            smartProduction2s = smartProduction2s.Where(sp => sp.Employee.Production2).ToList();
                            break;
                        case 3:
                            smartProduction2s = smartProduction2s.Where(sp => sp.Employee.Production2B).ToList();
                            break;
                        default:
                            break;
                    }
                    foreach (var production2 in smartProduction2s) {
                        var entity = new Production2Model {
                            EmployeeId = production2.EmployeeId,
                            EmployeeName = production2.Employee.EmployeeName,
                            ProductId = production2.ProductId,
                            ProductCode = production2.Product.ProductCode,
                            Quantity = production2.QuantityKg,
                            ProductionSectionId = production2.SectionIssueId,
                            SectionName = production2.ProductionSection.SectionIndex + "." +
                                          production2.ProductionSection.Section.SectionName,
                            MachineId = production2.MachineId,
                            MachineName = production2.Machine.MachineName,
                            SectionProductivity = production2.ProductionSection.Productivity,
                            Weight = production2.ProductionSection.Weight
                        };
                        if (production2.SectionReceiptId != null) {
                            entity.SectionNext = production2.SectionReceiptId.Value;
                            entity.SectionNextName = production2.ProductionSection1.SectionIndex + "." +
                                                     production2.ProductionSection1.Section.SectionName;
                        }
                        else {
                            entity.SectionNext = 0;
                            entity.SectionNextName = "*.Hoàn thành";
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProduction2Rotate", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.EmployeeName)));
        }

        [GridAction]
        public ActionResult UpdateProduction2Rotate(
            [Bind(Prefix = "inserted")] IEnumerable<Production2Model> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<Production2Model> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<Production2Model> deleteds,
            string date) {
            if (inserteds != null || updateds != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var modifiedUser = HttpContext.User.Identity.Name;
                    if (string.IsNullOrWhiteSpace(modifiedUser))
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
                    ModelState.Clear();
                    var ci = new CultureInfo("vi-VN");
                    var createdDate = string.IsNullOrWhiteSpace(date)
                                          ? DateTime.Today
                                          : Convert.ToDateTime(date, ci);
                    var list = new List<Production2Model>();
                    if (updateds != null && updateds.Any())
                        list.AddRange(updateds.Where(i => i.Quantity + i.QuantityDefect + i.QuantityLost > 0).ToList());
                    if (inserteds != null && inserteds.Any())
                        list.AddRange(inserteds.Where(i => i.Quantity + i.QuantityDefect + i.QuantityLost > 0).ToList());
                    var transaction = new Production2Transaction {
                        TransactionCode =
                            MyUtilities.AutoIncrease.GetParam(
                                (int)MyUtilities.AutoIncrease.IncreaseNum.Production2, 1),
                        EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Export).ToString(),
                        CreateDate = createdDate,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Production2TransactionDetail = new List<Production2TransactionDetail>()
                    };
                    using (var vfi = new tammaContext()) {
                        // kiem tra thong tin
                        var msg = "";
                        var checks = list.Where(i => i.ProductionSectionId == 0);
                        if (checks.Any()) {
                            foreach (var detail in checks) {
                                msg += "Vui lòng chọn công đoạn ! " + detail.ProductCode + "\n";
                            }
                        }
                        checks = list.Where(i => i.EmployeeId == 0);
                        if (checks.Any()) {
                            foreach (var detail in checks) {
                                msg += "Vui lòng chọn nhân viên ! " + detail.ProductCode
                                       + "-" + detail.SectionName + "\n";
                            }
                        }
                        checks = list.Where(i => i.MachineId == 0);
                        if (checks.Any()) {
                            foreach (var detail in checks) {
                                msg += "Vui lòng chọn máy ! " + detail.ProductCode
                                       + "-" + detail.SectionName + "\n";
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(msg))
                            throw new AggregateException(msg);

                        foreach (var detail in list) {
                            var sectionIssue =
                                vfi.ProductionSections.FirstOrDefault(
                                    ps =>
                                    ps.ProductId == detail.ProductId &&
                                    ps.ProductionSectionId == detail.ProductionSectionId && ps.Active);
                            if (sectionIssue == null)
                                throw new AggregateException("Vui lòng cập nhật lại công đoạn cho sản phẩm " +
                                                             detail.ProductCode + "(công đoạn " + detail.SectionName +
                                                             " không tìm thấy)");
                            var entity = new Production2TransactionDetail {
                                Production2Transaction = transaction,
                                TransactionId = transaction.TransactionId,
                                ProductId = detail.ProductId,
                                SectionIssueId = detail.ProductionSectionId,
                                UnitMeasure = "Kg",
                                QuantityKg = detail.Quantity,
                                Quantity = 0,
                                Note = detail.Note + "",
                                MachineId = detail.MachineId,
                                EmployeeId = detail.EmployeeId,
                                QuantityDefect = detail.QuantityDefect,
                                QuantityLost = detail.QuantityLost,
                                Time = detail.Time,
                                OverTime = detail.OverTime,
                                SectionIndex = sectionIssue.SectionIndex,

                            };
                            //entity.SectionIndex = section.SectionIndex;
                            if (detail.SectionNext != 0) {
                                var section =
                                    vfi.ProductionSections.FirstOrDefault(
                                        ps =>
                                        ps.ProductId == detail.ProductId &&
                                        ps.ProductionSectionId == detail.SectionNext && ps.Active);
                                if (section == null)
                                    throw new AggregateException(
                                        "Lỗi! Công đoạn kế không tìm thấy! Vui lòng cập nhật lại");
                                entity.SectionReceiptId = detail.SectionNext;
                            }
                            transaction.Production2TransactionDetail.Add(entity);
                        }
                        if (transaction.Production2TransactionDetail.Any()) {
                            vfi.Production2Transaction.Add(transaction);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProduction2Rotate", ex.Message);
                }
            }


            return View(new GridModel(new List<Production2Model>()));
        }

        [GridAction]
        public ActionResult UpdateProduction2Import(
            [Bind(Prefix = "inserted")] IEnumerable<Production2Model> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<Production2Model> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<Production2Model> deleteds,
            string date) {
            if (inserteds != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var modifiedUser = HttpContext.User.Identity.Name;
                    if (string.IsNullOrWhiteSpace(modifiedUser))
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
                    ModelState.Clear();
                    var ci = new CultureInfo("vi-VN");
                    var createdDate = string.IsNullOrWhiteSpace(date)
                                          ? DateTime.Today
                                          : Convert.ToDateTime(date, ci);
                    var transaction = new Production2Transaction {
                        TransactionCode =
                            MyUtilities.AutoIncrease.GetParam(
                                (int)MyUtilities.AutoIncrease.IncreaseNum.Production2, 1),
                        EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Import).ToString(),
                        CreateDate = createdDate,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Production2TransactionDetail = new List<Production2TransactionDetail>()
                    };
                    using (var vfi = new tammaContext()) {
                        // kiem tra thong tin
                        var msg = "";
                        var checks = inserteds.Where(i => i.ProductionSectionId == 0);
                        if (checks.Any()) {
                            foreach (var detail in checks) {
                                msg += "Vui lòng chọn công đoạn ! " + detail.ProductCode + "\n";
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(msg))
                            throw new AggregateException(msg);

                        // them vao co so du lieu
                        foreach (var detail in inserteds) {
                            var entity = new Production2TransactionDetail {
                                Production2Transaction = transaction,
                                TransactionId = transaction.TransactionId,
                                ProductId = detail.ProductId,
                                SectionIssueId = null,
                                SectionReceiptId = detail.ProductionSectionId,
                                UnitMeasure = "Kg",
                                QuantityKg = detail.Quantity,
                                Quantity = 0,
                                Note = detail.Note + "",
                                MachineId = null,
                                EmployeeId = null,
                                QuantityDefect = 0,
                                Time = 0,
                                OverTime = 0,
                                QuantityLost = 0,
                                SectionIndex = 0,

                            };
                            transaction.Production2TransactionDetail.Add(entity);
                        }
                        if (transaction.Production2TransactionDetail.Any()) {
                            vfi.Production2Transaction.Add(transaction);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProduction2Import", ex.Message);
                }
            }
            return View(new GridModel(new List<Production2Model>()));
        }


        [GridAction]
        public ActionResult UpdateProduction2Export(
            [Bind(Prefix = "inserted")] IEnumerable<Production2Model> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<Production2Model> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<Production2Model> deleteds,
            string date) {
            if (inserteds != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var modifiedUser = HttpContext.User.Identity.Name;
                    if (string.IsNullOrWhiteSpace(modifiedUser))
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
                    ModelState.Clear();
                    var ci = new CultureInfo("vi-VN");
                    var createdDate = string.IsNullOrWhiteSpace(date)
                                          ? DateTime.Today
                                          : Convert.ToDateTime(date, ci);
                    var transaction = new Production2Transaction {
                        TransactionCode =
                            MyUtilities.AutoIncrease.GetParam(
                                (int)MyUtilities.AutoIncrease.IncreaseNum.Production2, 1),
                        EoI = Convert.ToChar(MyUtilities.Transaction.EoIEnum.Export).ToString(),
                        CreateDate = createdDate,
                        Status = (byte)MyUtilities.Transaction.Status.Open,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        Production2TransactionDetail = new List<Production2TransactionDetail>()
                    };
                    using (var vfi = new tammaContext()) {
                        // kiem tra thong tin
                        var msg = "";
                        var checks = inserteds.Where(i => i.ProductionSectionId == 0);
                        if (checks.Any()) {
                            foreach (var detail in checks) {
                                msg += "Vui lòng chọn công đoạn ! " + detail.ProductCode + "\n";
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(msg))
                            throw new AggregateException(msg);

                        // kiem tra ton kho cua cong doan
                        var productIds = inserteds.Select(i => i.ProductId).Distinct();
                        //var exportSection = new Production2Model();
                        foreach (var productId in productIds) {
                            var productionSections =
                                vfi.ProductionSections.Where(ps => ps.ProductId == productId && ps.Active)
                                   .OrderBy(ps => ps.SectionIndex);

                            var sectionInvQuantity = 0.0;
                            var exportSection = 0.0;
                            foreach (var section in productionSections) {
                                var insertModels =
                                    inserteds.Where(i => i.ProductionSectionId == section.ProductionSectionId);
                                if (insertModels.Any()) {
                                    var firstModel = insertModels.FirstOrDefault();
                                    var sectionInv =
                                        vfi.Production2Inventory.FirstOrDefault(
                                            pi => pi.ProductionSectionId == section.ProductionSectionId);
                                    if (sectionInv != null)
                                        sectionInvQuantity = sectionInv.TotalQuantity;
                                    var totalExport = Math.Round(insertModels.Sum(i => i.Quantity + i.QuantityDefect), 3);
                                    if (totalExport > sectionInvQuantity + exportSection) {
                                        msg += "Lỗi! Công đoạn " + firstModel.SectionName +
                                               " của sản phẩm " + firstModel.ProductCode +
                                               " không đủ tồn kho ! Thiếu " +
                                               Math.Round(totalExport - sectionInvQuantity + exportSection, 3) + "(kg)";
                                    }
                                    exportSection = totalExport;
                                }
                                else {
                                    exportSection = 0;
                                }
                            }


                        }
                        if (!string.IsNullOrWhiteSpace(msg))
                            throw new AggregateException(msg);
                        // them vao co so du lieu
                        foreach (var detail in inserteds) {
                            var entity = new Production2TransactionDetail {
                                Production2Transaction = transaction,
                                TransactionId = transaction.TransactionId,
                                ProductId = detail.ProductId,
                                SectionIssueId = detail.ProductionSectionId,
                                SectionReceiptId = null,
                                UnitMeasure = "Kg",
                                QuantityKg = 0,
                                Quantity = 0,
                                Note = detail.Note + "",
                                MachineId = null,
                                EmployeeId = null,
                                QuantityDefect = detail.QuantityDefect,
                                Time = 0,
                                OverTime = 0,
                                QuantityLost = 0,

                            };
                            transaction.Production2TransactionDetail.Add(entity);
                        }
                        if (transaction.Production2TransactionDetail.Any()) {
                            vfi.Production2Transaction.Add(transaction);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProduction2Export", ex.Message);
                }
            }


            return View(new GridModel(new List<Production2Model>()));
        }


        [GridAction]
        public ActionResult SelectProduction2Plan(int employeeType) {
            var model = new List<Production2Model>();
            try {
                using (var vfi = new tammaContext()) {
                    var employees = vfi.Employees.ToList();
                    var time = 8;
                    switch (employeeType) {
                        case 2:
                            employees = employees.Where(e => e.Production2).ToList();
                            break;
                        case 3:
                            employees = employees.Where(e => e.Production2B).ToList();
                            time = 12;
                            break;
                        default:
                            employees = new List<Employee>();
                            break;
                    }
                    foreach (var employee in employees) {
                        var entity = new Production2Model {
                            EmployeeId = employee.EmployeeId,
                            EmployeeName = employee.EmployeeName,

                        };
                        var smartProducion2 =
                            vfi.SmartProduction2.FirstOrDefault(sp => sp.EmployeeId == employee.EmployeeId);
                        if (smartProducion2 != null) {
                            entity.ProductId = smartProducion2.ProductId;
                            entity.ProductCode = smartProducion2.Product.ProductCode;
                            entity.MachineId = smartProducion2.MachineId;
                            entity.MachineName = smartProducion2.Machine.MachineName;
                            entity.Time = time;
                            entity.ProductionSectionId = smartProducion2.SectionIssueId;
                            entity.SectionName = smartProducion2.ProductionSection.SectionIndex + "." +
                                                 smartProducion2.ProductionSection.Section.SectionName;
                            entity.SectionProductivity = smartProducion2.ProductionSection.Productivity;
                            entity.Weight = smartProducion2.ProductionSection.Weight;
                            entity.Quantity = entity.SectionProductivityKgInHour * entity.Time;
                            //entity.IsAdd = smartProducion2.Active;
                            if (smartProducion2.SectionReceiptId != null) {
                                entity.SectionNext = smartProducion2.SectionReceiptId.Value;
                                entity.SectionNextName = smartProducion2.ProductionSection1.SectionIndex + "." +
                                                         smartProducion2.ProductionSection1.Section.SectionName;
                            }
                            else {
                                entity.SectionNext = 0;
                                entity.SectionNextName = "*.Hoàn thành";
                            }
                        }
                        else {
                            var lastTransactionDetail = (from td in vfi.Production2TransactionDetail
                                                         orderby td.Production2Transaction.CreateDate descending
                                                         where
                                                             td.Production2Transaction.Status ==
                                                             (byte)MyUtilities.Transaction.Status.Approved
                                                             && td.EmployeeId == entity.EmployeeId
                                                             && td.Production2Transaction.EoI == "1"
                                                         select td).FirstOrDefault();
                            if (lastTransactionDetail != null) {

                                entity.ProductId = lastTransactionDetail.ProductId;
                                entity.ProductCode = lastTransactionDetail.Product.ProductCode;
                                entity.MachineId = lastTransactionDetail.MachineId.Value;
                                entity.MachineName = lastTransactionDetail.Machine.MachineName;
                                entity.Time = time;
                                entity.IsAdd = false;
                                if (lastTransactionDetail.SectionIssueId != null) {
                                    entity.ProductionSectionId = lastTransactionDetail.SectionIssueId.Value;
                                    entity.SectionName = lastTransactionDetail.ProductionSection.SectionIndex + "." +
                                                         lastTransactionDetail.ProductionSection.Section.SectionName;
                                    entity.SectionProductivity = lastTransactionDetail.ProductionSection.Productivity;
                                    entity.Weight = lastTransactionDetail.ProductionSection.Weight;
                                    entity.Quantity = entity.SectionProductivityKgInHour * entity.Time;
                                }
                                if (lastTransactionDetail.SectionReceiptId != null) {
                                    entity.SectionNext = lastTransactionDetail.SectionReceiptId.Value;
                                    entity.SectionNextName = lastTransactionDetail.ProductionSection1.SectionIndex + "." +
                                                             lastTransactionDetail.ProductionSection1.Section
                                                                                  .SectionName;
                                }
                                else {
                                    entity.SectionNext = 0;
                                    entity.SectionNextName = "*.Hoàn thành";
                                }
                            }
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProduction2Plan", ex.Message);

            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult UpdateProduction2Plan(
            [Bind(Prefix = "inserted")] IEnumerable<Production2Model> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<Production2Model> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<Production2Model> deleteds,
            int employeeType) {
            if (inserteds != null || updateds != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var modifiedUser = HttpContext.User.Identity.Name;
                    if (string.IsNullOrWhiteSpace(modifiedUser))
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
                    ModelState.Clear();
                    var list = new List<Production2Model>();
                    if (updateds != null && updateds.Any())
                        list.AddRange(updateds.Where(u => u.IsAdd).ToList());
                    if (inserteds != null && inserteds.Any())
                        list.AddRange(inserteds.Where(u => u.Quantity > 0).ToList());
                    using (var vfi = new tammaContext()) {
                        // kiem tra thong tin
                        var msg = "";
                        var checks = list.Where(i => i.ProductionSectionId == 0);
                        if (checks.Any()) {
                            foreach (var detail in checks) {
                                msg += "Vui lòng chọn công đoạn ! " + detail.ProductCode + "\n";
                            }
                        }
                        checks = list.Where(i => i.EmployeeId == 0);
                        if (checks.Any()) {
                            foreach (var detail in checks) {
                                msg += "Vui lòng chọn nhân viên ! " + detail.ProductCode
                                       + "-" + detail.SectionName + "\n";
                            }
                        }
                        checks = list.Where(i => i.MachineId == 0);
                        if (checks.Any()) {
                            foreach (var detail in checks) {
                                msg += "Vui lòng chọn máy ! " + detail.ProductCode
                                       + "-" + detail.SectionName + "\n";
                            }
                        }
                        checks = list.Where(i => i.Quantity == 0);
                        if (checks.Any()) {
                            foreach (var detail in checks) {
                                msg += "Vui lòng cập nhật số lượng ! " + detail.ProductCode + "\n"
                                       + detail.EmployeeName;
                            }
                        }
                        //checks = list.Where(i => i.Time == 0);
                        //if (checks.Any())
                        //{
                        //    foreach (var detail in checks)
                        //    {
                        //        msg += "Vui lòng cập nhật giờ làm ! " + detail.ProductCode + "\n"
                        //            + detail.EmployeeName;
                        //    }
                        //}
                        if (!string.IsNullOrWhiteSpace(msg))
                            throw new AggregateException(msg);
                        switch (employeeType) {
                            case 2:
                                vfi.SmartProduction2.RemoveRange(
                                    vfi.SmartProduction2.Where(sp => sp.Employee.Production2).ToList());
                                break;
                            case 3:
                                vfi.SmartProduction2.RemoveRange(
                                    vfi.SmartProduction2.Where(sp => sp.Employee.Production2B).ToList());
                                break;
                            default:
                                break;
                        }
                        foreach (var detail in list) {
                            var sectionIssue =
                                vfi.ProductionSections.FirstOrDefault(
                                    ps =>
                                    ps.ProductId == detail.ProductId &&
                                    ps.ProductionSectionId == detail.ProductionSectionId && ps.Active);
                            if (sectionIssue == null)
                                throw new AggregateException("Vui lòng cập nhật lại công đoạn cho sản phẩm " +
                                                             detail.ProductCode + "(công đoạn " + detail.SectionName +
                                                             " không tìm thấy)");
                            var entity = new SmartProduction2 {
                                ProductId = detail.ProductId,
                                SectionIssueId = detail.ProductionSectionId,
                                QuantityKg = detail.Quantity,
                                MachineId = detail.MachineId,
                                EmployeeId = detail.EmployeeId,
                                //Time = detail.Time,
                                //Active = detail.IsAdd,
                            };
                            if (detail.SectionNext != 0) {
                                var sectionNext =
                                    vfi.ProductionSections.FirstOrDefault(
                                        ps =>
                                        ps.ProductId == detail.ProductId &&
                                        ps.ProductionSectionId == detail.SectionNext && ps.Active);
                                if (sectionNext == null)
                                    throw new AggregateException("Vui lòng cập nhật lại công đoạn cho sản phẩm " +
                                                                 detail.ProductCode + "(công đoạn " + detail.SectionName +
                                                                 " không tìm thấy)");
                                entity.SectionReceiptId = detail.SectionNext;
                            }
                            vfi.SmartProduction2.Add(entity);
                        }
                        vfi.SaveChanges();
                    }

                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProduction2Plan", ex.Message);
                }
            }


            return View(new GridModel(new List<Production2Model>()));
        }

        [GridAction]
        public ActionResult SelectSectionInventory(string productCode, string fromDate, string toDate, bool getAll) {
            var model = new List<Production2PeriodModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fDate = string.IsNullOrWhiteSpace(fromDate)
                                ? DateTime.Today
                                : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                                ? DateTime.Today
                                : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    var production2Periods = from pip in vfi.Production2InventoryPeriod
                                             where pip.PeriodDate <= tDate
                                             select pip;
                    var production2PeriodInMonths = from pip in production2Periods
                                                    where pip.PeriodDate >= fDate
                                                    select pip;
                    var productIds =
                        production2Periods.Select(pip => pip.Production2Inventory.ProductionSection.ProductId);
                    var products =
                        vfi.Products.Where(p => productIds.Contains(p.ProductId) && p.Active)
                           .OrderBy(p => p.ProductCode);
                    var index = 1;
                    foreach (var product in products) {
                        if (!getAll) {
                            var check =
                                production2PeriodInMonths.Where(
                                    pp => pp.Production2Inventory.ProductionSection.ProductId == product.ProductId);
                            if (!check.Any())
                                continue;
                        }
                        var productionSections =
                            vfi.ProductionSections.Where(ps => ps.ProductId == product.ProductId && ps.Active)
                               .OrderBy(ps => ps.SectionIndex);
                        foreach (var productionSection in productionSections) {
                            var period = new Production2PeriodModel {
                                GlobalIndex = index,
                                ProductId = product.ProductId,
                                ProductCode = product.ProductCode,
                                SectionId = productionSection.SectionId,
                                SectionIndex = productionSection.SectionIndex,
                                SectionName = productionSection.Section.SectionName,
                                SectionProductivity = productionSection.Productivity,
                                Import = 0,
                                ImportPeriod = 0,
                                Export = 0,
                                ExportPeriod = 0
                            };
                            var productionSectionInv =
                                vfi.Production2Inventory.FirstOrDefault(
                                    pi => pi.ProductionSectionId == productionSection.ProductionSectionId);
                            if (productionSectionInv == null) {
                                index++;
                                model.Add(period);
                                continue;
                            }
                            var earlyPeriod =
                                production2Periods.Where(
                                    pp =>
                                    pp.Production2Inventory.InvId == productionSectionInv.InvId &&
                                    pp.PeriodDate < fDate);
                            if (earlyPeriod.Any()) {
                                period.EarlyQuantity = earlyPeriod.Sum(pp => pp.LastQuantity - pp.EarlyQuantity);
                            }
                            var sectionImportPeriod =
                                production2PeriodInMonths.Where(
                                    pp =>
                                    pp.Production2Inventory.InvId == productionSectionInv.InvId &&
                                    pp.LastQuantity > pp.EarlyQuantity);
                            if (sectionImportPeriod.Any()) {
                                period.ImportPeriod = sectionImportPeriod.Sum(pp => pp.Quantity);
                                var sectionImport = sectionImportPeriod.Where(pp => pp.PeriodDate == tDate);
                                if (sectionImport.Any())
                                    period.Import = sectionImport.Sum(pp => pp.Quantity);
                            }
                            var sectionExportPeriod =
                                production2PeriodInMonths.Where(
                                    pp =>
                                    pp.Production2Inventory.InvId == productionSectionInv.InvId &&
                                    pp.LastQuantity < pp.EarlyQuantity);
                            if (sectionExportPeriod.Any()) {
                                period.ExportPeriod = sectionExportPeriod.Sum(pp => pp.Quantity);
                                var sectionExport = sectionExportPeriod.Where(pp => pp.PeriodDate == tDate);
                                if (sectionExport.Any())
                                    period.Export = sectionExport.Sum(pp => pp.Quantity);
                            }
                            var lastPeriod =
                                production2Periods.Where(
                                    pp => pp.Production2Inventory.InvId == productionSectionInv.InvId);
                            if (lastPeriod.Any()) {
                                period.LastQuantity = lastPeriod.Sum(pp => pp.LastQuantity - pp.EarlyQuantity);
                            }
                            index++;
                            model.Add(period);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectSectionInventory", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<Production2Model> GetProduction2Model(string productCode, string fromDate, string toDate, int employeeType) {

            var model = new List<Production2Model>();
            var ci = new CultureInfo("vi-VN");
            var fDate = string.IsNullOrWhiteSpace(fromDate)
                            ? DateTime.Today
                            : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                            ? DateTime.Today
                            : Convert.ToDateTime(toDate, ci);

            using (var vfi = new tammaContext()) {
                var transactionDetails = (from td in vfi.Production2TransactionDetail
                                          orderby td.Production2Transaction.CreateDate, td.Product.ProductCode,
                                              td.SectionIndex
                                          where
                                              td.Production2Transaction.Status ==
                                              (byte)MyUtilities.Transaction.Status.Approved
                                              && td.Production2Transaction.CreateDate >= fDate
                                              && td.Production2Transaction.CreateDate <= tDate
                                              && td.MachineId != null && td.EmployeeId != null
                                              && td.SectionIssueId != null
                                          select td).ToList();
                var productIds = transactionDetails.Select(td => td.ProductId).Distinct().ToList();
                var productionPricings = vfi.ProductionPricings.Where(pp => productIds.Contains(pp.ProductId)).ToList();
                if (employeeType == 1) {
                    transactionDetails = transactionDetails.Where(td => td.Employee.Production2).ToList();
                }
                else if (employeeType == 2) {
                    transactionDetails = transactionDetails.Where(td => td.Employee.Production2B).ToList();
                }
                if (!string.IsNullOrWhiteSpace(productCode))
                    transactionDetails =
                        transactionDetails.Where(td => td.Product.ProductCode.Contains(productCode.Trim().ToUpper()))
                                          .ToList();
                var isProductionManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.Production2Management);
                foreach (var detail in transactionDetails) {
                    //var entity =
                    //    model.FirstOrDefault(
                    //        m =>
                    //        m.PeriodDate == detail.Production2Transaction.CreateDate &&
                    //        m.ProductId == detail.ProductId &&
                    //        m.MachineId == detail.MachineId &&
                    //        m.EmployeeId == detail.EmployeeId &&
                    //        m.ProductionSectionId == detail.SectionIssueId);

                    //if (entity != null)
                    //    if (detail.SectionReceiptId == null && entity.SectionNext != 0)
                    //        entity = null;
                    //    else if (detail.SectionReceiptId != null && entity.SectionNext == 0)
                    //        entity = null;
                    //    else if (detail.SectionReceiptId != null && entity.SectionNext == 0 &&
                    //             detail.SectionReceiptId != entity.SectionNext)
                    //        entity = null;
                    //    else if (!(detail.Note.Equals(entity.Note)))
                    //        entity = null;
                    //if (entity == null)
                    //{
                    var entity = new Production2Model {
                        PeriodDate = detail.Production2Transaction.CreateDate,
                        ProductId = detail.ProductId,
                        ProductCode = detail.Product.ProductCode,
                        ProductionSectionId = detail.SectionIssueId.Value,
                        SectionName =
                            detail.ProductionSection.SectionIndex + "." +
                            detail.ProductionSection.Section.SectionName,
                        SectionIndex = detail.ProductionSection.SectionIndex,
                        Weight = detail.ProductionSection.Weight,
                        EmployeeId = detail.EmployeeId.Value,
                        EmployeeName = detail.Employee.EmployeeName,
                        MachineId = detail.MachineId.Value,
                        MachineName = detail.Machine.MachineName,
                        Quantity = detail.QuantityKg,
                        QuantityDefect = detail.QuantityDefect,
                        QuantityLost = detail.QuantityLost,
                        Note = detail.Note,
                        Time = detail.Time,
                        OverTime = detail.OverTime,
                        SectionProductivity = detail.ProductionSection.Productivity,
                        Type = detail.Employee.Production2 ? "A.Văn" : "A.Mẫn",
                        IsProductionManager = isProductionManager,
                        TransactionDetailId = detail.DetailId

                    };
                    if (detail.SectionReceiptId != null) {
                        entity.SectionNext = detail.SectionReceiptId ?? 0;
                        entity.SectionNextName = detail.ProductionSection1.SectionIndex + "." +
                                                 detail.ProductionSection1.Section.SectionName;
                        entity.SectionNextIndex = detail.ProductionSection1.SectionIndex;
                    }
                    else {
                        entity.SectionNext = 0;
                        entity.SectionNextName = "*.Hoàn thành";
                        entity.SectionNextIndex = 99;
                    }
                    var productionPricingsById = productionPricings.FirstOrDefault(pp => pp.ProductId == entity.ProductId);
                    if (productionPricingsById != null) {
                        entity.ProductionPrice = productionPricingsById.Production2Pricing;
                    }
                    model.Add(entity);
                    //}
                    //else
                    //{
                    //    entity.Quantity += detail.QuantityKg;
                    //    entity.QuantityDefect += detail.QuantityDefect;
                    //    entity.QuantityLost += detail.QuantityLost;
                    //    entity.Time += (detail.Time);
                    //    entity.OverTime += (detail.OverTime);
                    //    if (!string.IsNullOrWhiteSpace(detail.Note))
                    //        entity.Note += " | " + detail.Note;
                    //}
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult UpdateSectionPeriodDetail(Production2Model update) {
            try {
                using (var vfi = new tammaContext()) {

                    var detail = vfi.Production2TransactionDetail.FirstOrDefault(td =>
                        td.DetailId == update.TransactionDetailId);
                    if (detail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết cần sửa");

                    var employeeId = 0;
                    try {
                        employeeId = Convert.ToInt32(update.EmployeeName);
                    }
                    catch (FormatException) {
                        employeeId = vfi.Employees.FirstOrDefault(p => p.EmployeeName.Equals(update.EmployeeName))
                                       .EmployeeId;
                    }
                    detail.EmployeeId = employeeId;
                    detail.Time = update.Time;
                    detail.OverTime = update.OverTime;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateSectionPeriodDetail", ex.Message);
            }
            return View(new GridModel(new List<Production2Model>()));
        }

        [GridAction]
        public ActionResult SelectSectionPeriodDetail(string productCode, string fromDate, string toDate, int employeeType) {
            var model = new List<Production2Model>();
            if (string.IsNullOrWhiteSpace(fromDate))
                return View(new GridModel(model));
            try {
                model = GetProduction2Model(productCode, fromDate, toDate, employeeType);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectSectionPeriodDetail", ex.Message);
            }
            return
                View(
                    new GridModel(
                        model.OrderByDescending(m => m.Type)
                             .ThenBy(m => m.PeriodDate)
                             .ThenBy(m => m.ProductCode)
                             .ThenBy(m => m.SectionNextIndex)
                             .ThenBy(m => m.SectionIndex)));
        }

        #region


        public ActionResult SelectComboBoxSectionProcess(int productId, int processId) {
            using (var vfi = new tammaContext()) {
                var model = from sp in vfi.SectionProcesses
                            where sp.Active &&
                            sp.ProductId == productId &&
                            (processId == 0 || sp.ProcessId != processId)
                            orderby sp.StartProcess descending, sp.EndProcess, sp.ProcessName
                            select new {
                                sp.ProcessId,
                                sp.ProcessName
                            };
                return new JsonResult {
                    Data = new SelectList(model.ToList(), "ProcessId", "ProcessName")
                };
            }
        }

        [GridAction]
        public ActionResult SelectSectionProcessProduct(int customerId, string productCode) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            if (customerId < 0)
                return View(new GridModel(new List<ProductModel>()));
            var model = new List<ProductModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var production2Warehouses = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    var production2Processes = from pp in vfi.ProductionProcesses
                                               where (customerId == 0 || pp.Product.CustomerId == customerId) &&
                                                      pp.Product.Active &&
                                                      pp.IsNecessary &&
                                                      production2Warehouses.Contains(pp.WarehouseId)
                                               select new {
                                                   pp.Product.CustomerId,
                                                   pp.Product.Customer.CustomerCode,
                                                   pp.ProductId,
                                                   pp.Product.ProductCode
                                               };
                    if (!string.IsNullOrWhiteSpace(productCode)) {
                        production2Processes = production2Processes.Where(pp => pp.ProductCode.Contains(productCode));
                    }
                    var productIds = production2Processes.Select(pp => pp.ProductId).Distinct().ToList();
                    var productionSections = vfi.ProductionSections.Where(ps => productIds.Contains(ps.ProductId) && ps.Active);
                    foreach (var productId in productIds) {
                        var production2Process = production2Processes.FirstOrDefault(pp => pp.ProductId == productId);
                        var entity = new ProductModel {
                            ProductId = productId,
                            ProductCode = production2Process.ProductCode,
                            CustomerId = production2Process.CustomerId,
                            CustomerCode = production2Process.CustomerCode,
                            SectionCount = 0
                        };
                        var productionSectionsById = productionSections.Where(ps => ps.ProductId == entity.ProductId);
                        if (productionSectionsById.Any()) entity.SectionCount = productionSectionsById.Count();
                        model.Add(entity);
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("SelectSectionProcessProduct", exception.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }

        [GridAction]
        public ActionResult SelectSectionProcessManagement(int productId) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            if (productId < 0)
                return View(new GridModel(new List<SectionProcessModel>()));
            var model = new List<SectionProcessModel>();
            try {
                model = GetSectionProcessManagement(productId);
            }
            catch (Exception exception) {
                ModelState.AddModelError("SelectSectionProcessManagement", exception.Message);
            }
            return View(new GridModel(model));
        }

        List<SectionProcessModel> GetSectionProcessManagement(int productId) {
            var model = new List<SectionProcessModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var sectionProcesses = vfi.SectionProcesses.Where(sp => sp.ProductId == productId);
                    foreach (var sectionProcess in sectionProcesses) {
                        var entity = new SectionProcessModel {
                            ProductId = productId,
                            ProcessId = sectionProcess.ProcessId,
                            ProcessName = sectionProcess.ProcessName,
                            Active = sectionProcess.Active,
                            StartProcess = sectionProcess.StartProcess,
                            EndProcess = sectionProcess.EndProcess,
                            ModifiedDate = sectionProcess.ModifiedDate,
                            ModifiedUser = sectionProcess.ModifiedUser
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception exception) {
                throw exception;
            }
            return model.OrderByDescending(m => m.StartProcess).ThenBy(m => m.EndProcess).ThenByDescending(m => m.Active).ThenBy(m => m.ProcessName).ToList();
        }

        [GridAction]
        public ActionResult InsertSectionProcess(int productId, SectionProcessModel insert) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            try {
                insert.ProcessName = insert.ProcessName.Trim();
                using (var vfi = new tammaContext()) {
                    var entity = vfi.SectionProcesses.FirstOrDefault(sp => sp.ProcessName.Equals(insert.ProcessName) && sp.ProductId == productId);
                    var existedStart = vfi.SectionProcesses.Any(sp => sp.ProductId == productId && sp.Active && sp.StartProcess);
                    var existedEnd = vfi.SectionProcesses.Any(sp => sp.ProductId == productId && sp.Active && sp.EndProcess);
                    if (entity != null) {
                        if (entity.Active)
                            throw new AggregateException("Lỗi! Bị trùng tên.");
                        entity.ProcessName = insert.ProcessName;
                        entity.StartProcess = existedStart ? insert.StartProcess : true;
                        entity.EndProcess = existedEnd ? insert.EndProcess : true;
                        entity.Active = true;
                        entity.ModifiedDate = DateTime.Now;
                        entity.ModifiedUser = HttpContext.User.Identity.Name;
                        vfi.SaveChanges();
                    }
                    else {
                        entity = new SectionProcess {
                            ProcessName = insert.ProcessName,
                            ProductId = insert.ProductId,
                            StartProcess = existedStart ? insert.StartProcess : true,
                            EndProcess = existedEnd ? insert.EndProcess : true,
                            Active = true,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name
                        };
                        vfi.SectionProcesses.Add(entity);
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("InsertSectionProcess", exception.Message);
            }
            return View(new GridModel(GetSectionProcessManagement(insert.ProductId)));
        }

        [GridAction]
        public ActionResult UpdateSectionProcess(SectionProcessModel update) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            try {
                using (var vfi = new tammaContext()) {
                    var entity = vfi.SectionProcesses.FirstOrDefault(sp => sp.ProcessId != update.ProcessId &&
                                                                            sp.ProcessName.Equals(update.ProcessName) &&
                                                                            sp.ProductId == update.ProductId);
                    var existedStart = vfi.SectionProcesses.Any(sp => sp.ProcessId != update.ProcessId && sp.ProductId == update.ProductId && sp.Active && sp.StartProcess);
                    var existedEnd = vfi.SectionProcesses.Any(sp => sp.ProcessId != update.ProcessId && sp.ProductId == update.ProductId && sp.Active && sp.EndProcess);
                    if (entity != null) {
                        throw new AggregateException("Lỗi! Bị trùng tên.");
                    }
                    entity = vfi.SectionProcesses.FirstOrDefault(sp => sp.ProcessId == update.ProcessId);
                    if (entity == null)
                        throw new AggregateException("Lỗi! Không tìm thấy công đoạn.");
                    if (!existedStart && (!update.StartProcess || !update.Active))
                        throw new AggregateException("Lỗi! Thiếu công đoạn bắt đầu! Thiết lập công đoạn khác bắt đầu");
                    if (!existedEnd && (!update.EndProcess || !update.Active))
                        throw new AggregateException("Lỗi! Thiếu công đoạn kết thúc! Thiết lập công đoạn khác kết thúc");
                    entity.ProcessName = update.ProcessName;
                    entity.StartProcess = update.StartProcess;
                    entity.EndProcess = update.EndProcess;
                    entity.Active = update.Active;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateSectionProcess", exception.Message);
            }
            return View(new GridModel(GetSectionProcessManagement(update.ProductId)));
        }


        [GridAction]
        public ActionResult SelectSectionProcessDetailManagement(int processId) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            if (processId < 0)
                return View(new GridModel(new List<SectionProcessDetailModel>()));
            var model = new List<SectionProcessDetailModel>();
            try {
                model = GetSectionProcessDetailManagement(processId);
            }
            catch (Exception exception) {
                ModelState.AddModelError("SelectSectionProcessDetailManagement", exception.Message);
            }
            return View(new GridModel(model));
        }

        List<SectionProcessDetailModel> GetSectionProcessDetailManagement(int processId) {
            var model = new List<SectionProcessDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var sectionProcessDetails = vfi.SectionProcessDetails.Where(sp => sp.ProcessId == processId);
                    foreach (var detail in sectionProcessDetails) {
                        var entity = new SectionProcessDetailModel {
                            ProcessId = processId,
                            DetailId = detail.DetailId,
                            ProductId = detail.ProductId,
                            SectionId = detail.SectionId,
                            SectionName = detail.Section.SectionName,
                            DetailIndex = detail.DetailIndex,
                            Active = detail.Active,
                            ModifiedDate = detail.ModifiedDate,
                            ModifiedUser = detail.ModifiedUser,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception exception) {
                throw exception;
            }
            return model.OrderByDescending(m => m.Active).ThenBy(m => m.DetailIndex).ToList();
        }

        [GridAction]
        public ActionResult InsertSectionProcessDetail(int processId, int productId, SectionProcessDetailModel insert) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            try {
                using (var vfi = new tammaContext()) {
                    int sectionId = 1;
                    try {
                        sectionId = Convert.ToInt32(insert.SectionName);
                    }
                    catch (Exception) {
                        var section = vfi.Sections.FirstOrDefault(w => w.SectionName.Equals(insert.SectionName));
                        if (section == null)
                            throw new AggregateException("Lỗi công đoạn ! Chọn lại công đoạn");
                        sectionId = section.SectionId;
                    }
                    var entity = new SectionProcessDetail {
                        ProcessId = processId,
                        ProductId = productId,
                        SectionId = sectionId,
                        DetailIndex = insert.DetailIndex,
                        Active = true,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                    };
                    vfi.SectionProcessDetails.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("InsertSectionProcessDetail", exception.Message);
            }
            return View(new GridModel(GetSectionProcessDetailManagement(insert.ProcessId)));
        }

        [GridAction]
        public ActionResult UpdateSectionProcessDetail(SectionProcessDetailModel update) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            try {
                using (var vfi = new tammaContext()) {
                    var process = vfi.SectionProcessDetails.FirstOrDefault(sp => sp.DetailId == update.DetailId);
                    if (process == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết công đoạn !");
                    update.ProcessId = process.ProcessId;
                    int sectionId = process.SectionId;
                    try {
                        sectionId = Convert.ToInt32(update.SectionName);
                    }
                    catch (Exception) {
                        try {
                            sectionId = vfi.Sections.FirstOrDefault(w => w.SectionName.Equals(update.SectionName)).SectionId;
                        }
                        catch (NullReferenceException) { }
                    }
                    process.Active = update.Active;
                    process.ModifiedDate = DateTime.Now;
                    process.ModifiedUser = HttpContext.User.Identity.Name;
                    process.DetailIndex = update.DetailIndex;
                    process.SectionId = sectionId;
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateSectionProcessDetail", exception.Message);
            }
            return View(new GridModel(GetSectionProcessDetailManagement(update.ProcessId)));
        }

        [GridAction]
        public ActionResult SelectProductionSectionProcessManagement(int processId) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            if (processId < 0)
                return View(new GridModel(new List<ProductionSectionProcessModel>()));
            var model = new List<ProductionSectionProcessModel>();
            try {
                model = GetProductionSectionProcessManagement(processId);
            }
            catch (Exception exception) {
                ModelState.AddModelError("SelectProductionSectionProcessManagement", exception.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionSectionProcessModel> GetProductionSectionProcessManagement(int processId) {
            var model = new List<ProductionSectionProcessModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var psps = vfi.ProductionSectionProcesses.Where(sp => sp.ProcessId == processId);
                    foreach (var psp in psps) {
                        var entity = new ProductionSectionProcessModel {
                            PSPId = psp.PSPId,
                            ProcessId = processId,
                            ProcessName = psp.SectionProcess.ProcessName,
                            NextProcessId = psp.NextProcessId,
                            NextProcessName = psp.SectionProcess1.ProcessName,
                            Productivity = psp.Productivity,
                            UnitMeasure = psp.UnitMeasure,
                            UnitPrice = psp.UnitPrice,
                            UnitWeight = psp.UnitWeight,
                            ModifiedDate = psp.ModifiedDate,
                            ModifiedUser = psp.ModifiedUser,
                            Active = psp.Active
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception exception) {
                throw exception;
            }
            return model.OrderByDescending(m => m.Active).ToList();
        }

        [GridAction]
        public ActionResult InsertProductionSectionProcess(int processId, int productId, ProductionSectionProcessModel insert) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            try {
                using (var vfi = new tammaContext()) {
                    int nextProcessId = 1;
                    try {
                        nextProcessId = Convert.ToInt32(insert.NextProcessName);
                    }
                    catch (Exception) {
                        var nextProcess = vfi.SectionProcesses.FirstOrDefault(w => w.ProcessName.Equals(insert.NextProcessName));
                        if (nextProcess == null)
                            throw new AggregateException("Lỗi công đoạn ! Chọn lại công đoạn");
                        nextProcessId = nextProcess.ProcessId;
                    }
                    var existedNextProcess = vfi.ProductionSectionProcesses.FirstOrDefault(pp => pp.ProcessId == processId && pp.NextProcessId == nextProcessId);
                    if (existedNextProcess == null) {
                        var configProcess = new ProductionSectionProcess {
                            Active = true,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            NextProcessId = nextProcessId,
                            ProcessId = processId,
                            Productivity = insert.Productivity,
                            UnitMeasure = "pcs",
                            UnitPrice = insert.UnitPrice,
                            UnitWeight = insert.UnitWeight,
                        };
                        vfi.ProductionSectionProcesses.Add(configProcess);
                        vfi.SaveChanges();
                    }
                    else {
                        if (!existedNextProcess.Active) {
                            existedNextProcess.Active = true;
                            existedNextProcess.Productivity = insert.Productivity;
                            existedNextProcess.UnitWeight = insert.UnitWeight;
                            existedNextProcess.UnitPrice = insert.UnitPrice;
                            existedNextProcess.ModifiedDate = DateTime.Now;
                            existedNextProcess.ModifiedUser = HttpContext.User.Identity.Name;
                            vfi.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("InsertProductionSectionProcess", exception.Message);
            }
            return View(new GridModel(GetProductionSectionProcessManagement(insert.ProcessId)));
        }

        [GridAction]
        public ActionResult UpdateProductionSectionProcess(ProductionSectionProcessModel update) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            try {
                using (var vfi = new tammaContext()) {
                    var nextProcess = vfi.ProductionSectionProcesses.FirstOrDefault(pp => pp.PSPId == update.PSPId);
                    if (nextProcess == null)
                        throw new AggregateException("Lỗi! Không tìm thấy công đoạn kế !");
                    update.ProcessId = nextProcess.ProcessId;
                    int nextProcessId = nextProcess.NextProcessId;
                    try {
                        nextProcessId = Convert.ToInt32(update.NextProcessName);
                    }
                    catch (Exception) {
                        try {
                            nextProcessId = vfi.SectionProcesses
                                .FirstOrDefault(w => w.ProcessName.Equals(update.NextProcessName)).ProcessId;
                        }
                        catch (NullReferenceException) { }
                    }
                    if (nextProcessId != nextProcess.NextProcessId) {
                        var existedNextProcess = vfi.ProductionSectionProcesses
                            .FirstOrDefault(pp => pp.ProcessId == update.ProcessId && pp.NextProcessId == nextProcessId);
                        if (existedNextProcess == null) {
                            nextProcess.NextProcessId = nextProcessId;
                            nextProcess.Active = update.Active;
                            nextProcess.Productivity = update.Productivity;
                            nextProcess.UnitWeight = update.UnitWeight;
                            nextProcess.UnitPrice = update.UnitPrice;
                            nextProcess.ModifiedDate = DateTime.Now;
                            nextProcess.ModifiedUser = HttpContext.User.Identity.Name;
                            vfi.SaveChanges();
                        }
                        else {
                            throw new AggregateException(
                                "Lỗi! Đã tồn tại công đoạn gia công này " + nextProcess.SectionProcess1.ProcessName);
                        }
                    }
                    else {
                        nextProcess.Active = update.Active;
                        nextProcess.Productivity = update.Productivity;
                        nextProcess.UnitWeight = update.UnitWeight;
                        nextProcess.UnitPrice = update.UnitPrice;
                        nextProcess.ModifiedDate = DateTime.Now;
                        nextProcess.ModifiedUser = HttpContext.User.Identity.Name;
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateProductionSectionProcess", exception.Message);
            }
            return View(new GridModel(GetProductionSectionProcessManagement(update.ProcessId)));
        }
        #endregion
    }
}
