using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc.Extensions;
using System.Drawing;
using System.Drawing.Imaging;

using Telerik.Web.Mvc;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Server.Core.DataModel.Models.Inv;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Sales.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Controllers;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Models;
using System.Threading;


namespace Vfi.Ui.Mvc.Vfi.Areas.Sales.Controllers {
    public class OrdersController : Controller {

        private readonly IUnitOfWork _unitOfWork;
        private readonly SalesOrderController _salesOrderController;
        private readonly WorkOrderController _workOrderController;
        [InjectionConstructor]
        public OrdersController(IUnitOfWork unitOfWork, SalesOrderController salesOrderController, WorkOrderController workOrderController) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
            _salesOrderController = salesOrderController;
            _workOrderController = workOrderController;
        }

        #region view
        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }

            return ViewData;
        }
        public ActionResult CreateOrderForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult OrderManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult InvoicesManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult TaxInvoicesManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult EditOrderForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ApproveInvoicesForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ApproveOrderForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ChangeOrderStatus() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult SendBackQualityOrder() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ApproveTaxInvoiceForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ApproveTaxInvoiceDetailForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult CancelQualityOrder() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult OrderNoteManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult AddTaxInvoiceDetails() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionTaxInvoice"] = new List<TaxInvoiceDetailModel>();
            return View();
        }
        public ActionResult ReduceTaxInvoiceDetails() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionTaxInvoice"] = new List<TaxInvoiceDetailModel>();
            return View();
        }
        public ActionResult AddInvoiceDetail() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["SessionInvoiceDetail"] = new List<OrderDetailModel>();
            return View();
        }
        public ActionResult DeptReport() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ConfirmOrderProgress() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult WaitingOrderBalancingPlan() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        #endregion
        
        #region order
        [GridAction]
        public ActionResult SelectOrderDetail() {
            return View(new GridModel(new List<OrderDetailModel>()));
        }

        [GridAction]
        public ActionResult SelectOrderDetailByExcel(string fileName, int productIndex, int quantityIndex,
                                                     int priceIndex, string sheetName, int deliveryIndex) {
            var model = new List<OrderDetailModel>();
            if (string.IsNullOrWhiteSpace(fileName))
                return View(new GridModel(model));
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            var dt = new DataTable();
            try {
                using (var conn = new OleDbConnection()) {
                    var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload"), fileName);
                    string fileExtension = Path.GetExtension(destinationPath);
                    if (fileExtension == ".xls")
                        conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destinationPath + ";" +
                                                "Extended Properties='Excel 8.0;HDR=YES;'";
                    else if (fileExtension == ".xlsx")
                        conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destinationPath + ";" +
                                                "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    MyUtilities.Function.SaveLog(actionName, "destinationPath: " + destinationPath);
                    try {
                        using (var comm = new OleDbCommand()) {
                            //var sheetName = "Sheet1";
                            if (string.IsNullOrWhiteSpace(sheetName))
                                comm.CommandText = "Select * from [Sheet1$]";
                            else
                                comm.CommandText = "Select * from [" + sheetName.Trim() + "$]";
                            comm.Connection = conn;
                            comm.CommandType = CommandType.Text;
                            using (var da = new OleDbDataAdapter()) {
                                da.SelectCommand = comm;
                                da.Fill(dt);
                            }
                            //conn.Open();
                            //using (OleDbDataReader _dr = comm.ExecuteReader()) {
                            //    dt.Load(_dr);
                            //}
                        }
                    }
                    catch (Exception ex) {
                        MyUtilities.Function.SaveLog(actionName, "OleDbCommandError: " + ex.Message);
                        throw ex;
                    }
                }
                using (var vfi = new tammaContext()) {
                    errorIndex = "";
                    oke = error = duplicate = 0;
                    dt.Rows.RemoveAt(0);
                    //dt.Rows.RemoveAt(0);
                    MyUtilities.Function.SaveLog(actionName, "Row count: " + dt.Rows.Count);

                    Session["SessionOrderDetailModel"] = new List<OrderDetailModel>();
                    foreach (DataRow row in dt.Rows) {
                        //MyUtilities.Function.SaveLog(contentPath(), actionName, "Row count: " + row.row);
                        var productCode = "";
                        if (productIndex == 0)
                            productCode = row[3].ToString().Trim();
                        else
                            productCode = row[productIndex - 1].ToString().Trim();
                        if (string.IsNullOrWhiteSpace(productCode)) {
                            if (string.IsNullOrWhiteSpace(row[1].ToString())) break;
                            continue;
                        }
                        var product = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(productCode));
                        if (product == null) {
                            error++;
                            errorIndex += "Error: Không tìm thấy: " + productCode + "\n";
                            continue;
                        }
                        //var entity = model.FirstOrDefault(m => m.ProductId == product.ProductId);
                        //if (entity != null)
                        //{
                        //    duplicate++;
                        //    errorIndex += "Error: Bị trùng mã: " + product.ProductCode + "\n";
                        //    continue;
                        //}
                        var entity = new OrderDetailModel {
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            ProductDesignNo = product.DesignNo,
                            ProductId = product.ProductId,
                            TotalInventory = 0,
                            OrderQty = 0,
                            LineTotal = 0,
                            UnitPrice = 0,
                            PONumber = row[1].ToString()
                        };
                        if (quantityIndex == 0) {
                            if (!string.IsNullOrWhiteSpace(row[4].ToString()))
                                try {
                                    entity.OrderQty = Convert.ToDouble(row[4]);
                                }
                                catch (FormatException) {
                                }
                        }
                        else {
                            if (!string.IsNullOrWhiteSpace(row[quantityIndex - 1].ToString()))
                                try {
                                    entity.OrderQty = Convert.ToDouble(row[quantityIndex - 1]);
                                }
                                catch (FormatException) {
                                }
                        }
                        if (entity.OrderQty == 0) continue;
                        if (priceIndex == 0) {
                            if (!string.IsNullOrWhiteSpace(row[5].ToString()))
                                try {
                                    entity.OrderQty = Math.Round(Convert.ToDouble(row[5]), 4);
                                }
                                catch (FormatException) {
                                }
                        }
                        else {
                            if (!string.IsNullOrWhiteSpace(row[priceIndex - 1].ToString()))
                                try {
                                    entity.UnitPrice = Math.Round(Convert.ToDouble(row[priceIndex - 1]), 4);
                                }
                                catch (FormatException) {
                                }
                        }
                        entity.LineTotal = entity.UnitPrice * entity.OrderQty;
                        var ci = new CultureInfo("vi-VN");
                        var deliverDate = DateTime.Now;
                        if (deliveryIndex == 0) {
                            if (!string.IsNullOrWhiteSpace(row[7].ToString()))
                                try {
                                    deliverDate = Convert.ToDateTime(row[7], ci);
                                }
                                catch (FormatException) {
                                }
                        }
                        else {
                            if (!string.IsNullOrWhiteSpace(row[deliveryIndex - 1].ToString()))
                                try {
                                    deliverDate = Convert.ToDateTime(row[deliveryIndex - 1], ci);
                                }
                                catch (FormatException) {
                                }
                        }
                        entity.CustomerDueDate = deliverDate;
                        oke++;
                        model.Add(entity);
                    }
                }
            }
            catch (OleDbException oledbEx) {
                MyUtilities.Function.SaveLog(actionName, "OleDbException: " + oledbEx.Message);
                ModelState.AddModelError("OleDbException", oledbEx.Message);
            }
            catch (Exception ex) {
                MyUtilities.Function.SaveLog(actionName, "Exception: " + ex.Message);
                ModelState.AddModelError("SelectOrderDetailByExcel", ex.Message);
            }
            Session["SessionOrderDetailModel"] = model;

            var data = new object[] { oke + error + duplicate, oke, error, duplicate, errorIndex };
            MyUtilities.Function.SaveLog(actionName, "Finish: " + data.ToString());
            return View(new GridModel(model));


            //return View(new GridModel(model));
        }

        public ActionResult SaveAllOrderDetail(
            string customerId, string orderDate, int? paymentTermId
            , int? shipMethodId, int? employeeId, string currencyCode
            , string billToAddress, string shipToAddress, string note) {


            if (string.IsNullOrEmpty(customerId) || string.IsNullOrWhiteSpace(customerId)) {
                ModelState.AddModelError("ProductCode", "Please pick Customer up!");
            }
            if (paymentTermId == null || shipMethodId == null || employeeId == null)
                throw new AggregateException("Vui lòng chọn đầy đủ các điều kiện");

            using (var vfi = new tammaContext()) {
                var orderDetails = new List<OrderDetail>();
                try {
                    if (!Request.IsAuthenticated) {
                        if (!Request.IsAuthenticated)
                            throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                        //return View(new GridModel(new List<SalesOrderDetailModel>()));
                    }
                    // check existed
                    var cid = Convert.ToInt32(customerId);
                    var customer = vfi.Customers.FirstOrDefault(f => f.CustomerId == cid && f.State == (byte)MyUtilities.Sales.CustomerState.Active);
                    if (customer == null)
                        throw new AggregateException("Customer is not existed");

                    var ci = new CultureInfo("vi-VN");

                    var tDate = string.IsNullOrWhiteSpace(orderDate)
                                    ? DateTime.Today
                                    : Convert.ToDateTime(orderDate, ci);
                    var model = (List<OrderDetailModel>)Session["SessionOrderDetailModel"];
                    if (ModelState.IsValid) {
                        if (model != null) {
                            if (paymentTermId == 0 || shipMethodId == 0 || employeeId == 0 ||
                                string.IsNullOrWhiteSpace(currencyCode) || cid == 0) {
                                throw new AggregateException("Vui lòng chọn đầy đủ các điều kiện");
                            }

                            var order = new Order {
                                CustomerId = cid,
                                SalesPersonId = employeeId,
                                CreatedDate = DateTime.Now,
                                OrderDate = tDate,
                                OrderNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Order, 1),
                                BillToAddress = billToAddress,
                                ShipToAddress = shipToAddress,
                                ShipMethodId = shipMethodId,
                                CurrencyCode = currencyCode,
                                Note = note,
                                PaymentTermId = paymentTermId,
                                //DueDate = Convert.ToDateTime(dueDate),
                                Active = true,
                                Status = 0,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                            };
                            //var i = 0;
                            //var flag = false;
                            foreach (var entity in model) {
                                if (entity.OrderQty == 0) continue;
                                //if (i >= 310) 
                                //    flag = true;
                                //    i++;
                                var detail =
                                    orderDetails.FirstOrDefault(
                                        od =>
                                        od.ProductId == entity.ProductId &&
                                        od.PONumber.Equals(entity.PONumber) &&
                                        od.UnitPrice == Math.Round(entity.UnitPrice, 4) &&
                                        od.CustomerDueDate == entity.CustomerDueDate);
                                if (detail == null) {
                                    detail = new OrderDetail {
                                        OrderId = order.OrderId,
                                        ProductId = entity.ProductId,
                                        OrderQty = MyUtilities.Function.RoundUp(entity.OrderQty),
                                        //RequiedNumber = Convert.ToInt32(entity.OrderQty + ""),
                                        UnitPrice = Math.Round(entity.UnitPrice, 4),
                                        UnitPriceDiscount = Math.Round(entity.UnitPrice, 4),
                                        LineTotal = entity.UnitPrice * (1 - entity.UnitPriceDiscount) * entity.OrderQty,
                                        CustomerDueDate = entity.CustomerDueDate ?? DateTime.Now,
                                        //VFIDueDate =  entity.VFIDueDate,
                                        LotNumber = entity.LotNumber,
                                        ModelNumber = entity.ModelNumber,
                                        Active = true,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        ModifiedDate = DateTime.Now,

                                        PONumber = entity.PONumber,
                                        IsComplete = false,
                                    };
                                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == entity.ProductId && p.UnitPrice == 0);
                                    if (product != null)
                                        product.UnitPrice = detail.UnitPrice;
                                    detail.RequiedNumber = detail.OrderQty ?? 0;
                                    orderDetails.Add(detail);
                                }
                                else {
                                    detail.OrderQty += MyUtilities.Function.RoundUp(entity.OrderQty);
                                    detail.RequiedNumber = detail.OrderQty ?? 0;
                                }
                            }
                            if (orderDetails.Count > 0) {
                                vfi.Orders.Add(order);
                                vfi.OrderDetails.AddRange(orderDetails);
                                vfi.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    return Json(ex);
                }
            }
            Session["SessionOrderDetailModel"] = new List<OrderDetailModel>();
            return Json("Ok All");
        }

        private static int oke;
        private static int error;
        private static int duplicate;
        private static string errorIndex;
        public ActionResult GetExcelParseStatus() {
            var data = new object[] { oke + error + duplicate, oke, error, duplicate, errorIndex };
            return Json(data);
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateOrderDetail(
            [Bind(Prefix = "inserted")]IEnumerable<OrderDetailModel> insertedOrderDetails,
            [Bind(Prefix = "updated")]IEnumerable<SalesOrderDetailModel> updatedSalesOrderDetails,
            [Bind(Prefix = "deleted")]IEnumerable<SalesOrderDetailModel> deletedSalesOrderDetails,
             string customerId, string orderNumber, string orderDate, string dueDate
            , string poNumber, string lotNumber, string modelNumber, int paymentTermId
            , int shipMethodId, int employeeId, string currencyCode
            , string billToAddress, string shipToAddress, string note) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SalesOrderDetailModel>()));
            }

            if (string.IsNullOrEmpty(customerId) || string.IsNullOrWhiteSpace(customerId)) {
                ModelState.AddModelError("ProductCode", "Please pick Customer up!");
            }

            using (var vfi = new tammaContext()) {
                try {
                    // check existed
                    var cid = Convert.ToInt32(customerId);
                    var customer = vfi.Customers.FirstOrDefault(f => f.CustomerId == cid && f.State == (byte)MyUtilities.Sales.CustomerState.Active);
                    if (customer == null)
                        throw new AggregateException("Customer is not existed");

                    if (ModelState.IsValid) {
                        if (insertedOrderDetails != null && insertedOrderDetails.Count() > 0) {
                            if (paymentTermId == 0 || shipMethodId == 0 || employeeId == 0 ||
                                string.IsNullOrWhiteSpace(currencyCode) || cid == 0) {
                                throw new AggregateException("Vui lòng chọn đầy đủ các điều kiện");
                            }

                            var order = new Order {
                                CustomerId = cid,
                                SalesPersonId = employeeId,
                                CreatedDate = DateTime.Now,
                                OrderDate = Convert.ToDateTime(orderDate),
                                OrderNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Order, 1),
                                //PoNumber = poNumber,
                                LotNumber = lotNumber,
                                ModelNumber = modelNumber,
                                BillToAddress = billToAddress,
                                ShipToAddress = shipToAddress,
                                ShipMethodId = shipMethodId,
                                CurrencyCode = currencyCode,
                                Note = note,
                                PaymentTermId = paymentTermId,
                                //DueDate = Convert.ToDateTime(dueDate),
                                Active = true,
                                Status = 0,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                            };
                            var orderDetails = new List<OrderDetail>();
                            foreach (var entity in insertedOrderDetails) {
                                if (entity.OrderQty == 0) continue;
                                if (entity.CustomerDueDate == null) {
                                    throw new AggregateException("Lỗi! Ngày yêu cầu khách hàng không được để trống " + entity.ProductCode);
                                }
                                var detail =
                                    orderDetails.FirstOrDefault(
                                        od =>
                                        od.ProductId == entity.ProductId &&
                                        od.PONumber.Equals(entity.PONumber) &&
                                        od.UnitPrice == Math.Round(entity.UnitPrice, 4) &&
                                        od.CustomerDueDate == entity.CustomerDueDate);
                                if (detail == null) {
                                    detail = new OrderDetail {
                                        OrderId = order.OrderId,
                                        ProductId = entity.ProductId,
                                        OrderQty = MyUtilities.Function.RoundUp(entity.OrderQty),
                                        RequiedNumber = MyUtilities.Function.RoundUp(entity.OrderQty),
                                        LineTotal = entity.UnitPrice * (1 - entity.UnitPriceDiscount) * entity.OrderQty,
                                        CustomerDueDate = entity.CustomerDueDate ?? DateTime.Now,
                                        //VFIDueDate =  entity.VFIDueDate,
                                        LotNumber = entity.LotNumber,
                                        ModelNumber = entity.ModelNumber,
                                        Active = true,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                        ModifiedDate = DateTime.Now,
                                        PONumber = entity.PONumber,
                                        IsComplete = false,
                                        Status = (int)MyUtilities.Sales.Status.Waiting,
                                        IsAlert = entity.IsAlert,
                                        OrderNote = entity.OrderNote
                                    };
                                    if (currencyCode.Equals("VND")) {
                                        detail.UnitPrice = Math.Round(entity.UnitPrice, 0);
                                        detail.UnitPriceDiscount = Math.Round(entity.UnitPrice, 0);
                                    }
                                    else {
                                        detail.UnitPrice = Math.Round(entity.UnitPrice, 4);
                                        detail.UnitPriceDiscount = Math.Round(entity.UnitPrice, 4);
                                    }
                                    var product =
                                        vfi.Products.FirstOrDefault(
                                            p => p.ProductId == entity.ProductId && p.UnitPrice == 0);
                                    if (product != null)
                                        product.UnitPrice = detail.UnitPrice;

                                    orderDetails.Add(detail);
                                }
                                else {
                                    detail.OrderQty += MyUtilities.Function.RoundUp(entity.OrderQty);
                                }
                            }
                            if (orderDetails.Count > 0) {
                                vfi.Orders.Add(order);
                                vfi.OrderDetails.AddRange(orderDetails);
                                vfi.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCode", "" + exception.Message);
                }
            }
            return View(new GridModel(new List<SalesOrderDetailModel>()));
        }

        public ActionResult PrintPreparationTools(int orderDetailId) {
            var model = new List<PreparationToolModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var orderDetail = vfi.OrderDetails.FirstOrDefault(x => x.OrderDetailId == orderDetailId);
                    if (orderDetail == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy thông tin");
                    }
                    var productionTools = (from pt in vfi.ProductionTools
                                          where pt.Active && pt.ProductId == orderDetail.ProductId
                                          select pt).ToList();
                    var toolIds = productionTools.Select(x => x.ToolId).Distinct().ToList();
                    var toolInvs = vfi.ToolInventories.Where(x => x.TotalQuantity > 0 && toolIds.Contains(x.ToolId)).ToList();
                    var allProductionTools = vfi.SelectCurrentProductionTools;
                    var materialInv = 0.0;
                    var material = orderDetail.Product.ProductionMaterials.Where(x => x.Active).OrderBy(x => x.Priority).FirstOrDefault();
                    if (material != null) {
                        materialInv = vfi.MaterialInventories.Where(mi => mi.TotalQty > 0 && mi.MaterialId == material.MaterialId)
                                                            .ToList().Sum(x => x.TotalQty);
                    }
                    var index = 1;
                    var employeeName = orderDetail.Order.SalesPersonId != null ? orderDetail.Order.Employee.EmployeeName : "";
                    var machineName = orderDetail.Product.ProcessingDesign != null ? orderDetail.Product.ProcessingType.TypeName : "";
                    foreach (var productionTool in productionTools) {
                        var detail = new PreparationToolDetail {
                            ToolId = productionTool.ToolId,
                            ToolName = productionTool.Tool.ToolFullCode,
                            ToolUse = productionTool.Note,
                            TypeId = productionTool.Tool.MaterialTypeId,
                            TypeName = productionTool.Tool.MaterialType.MaterialTypeName,

                            ToolIndex = productionTool.ToolIndex ?? 0,
                            ExportQuantity = productionTool.UseNumber,
                            PrepareQuantity = 0,
                            TotalInv = 0,
                        };
                        var sameProductionTools = allProductionTools.Where(pt => pt.ToolId == detail.ToolId);
                        foreach (var sameProductionTool in sameProductionTools) {
                            var same = detail.SameMachines.FirstOrDefault(s => s.MachineId == sameProductionTool.MachineId);
                            if (same == null) {
                                same = new SamePreparation {
                                    MachineId = sameProductionTool.MachineId,
                                    MachineName = sameProductionTool.MachineName,
                                };
                                detail.SameMachines.Add(same);
                            }
                        }

                        var toolInvsById = toolInvs.Where(ti => ti.ToolId == detail.ToolId);
                        if (toolInvsById.Any()) {
                            detail.TotalInv = toolInvsById.Sum(ti => ti.TotalQuantity);
                        }

                        var entity = model.FirstOrDefault(m => m.ToolTypeId == detail.TypeId);
                        if (entity == null) {
                            entity = new PreparationToolModel {
                                Index = index,
                                TrackId = orderDetailId,
                                Quantity = orderDetail.OrderQty ?? 0,
                                DeliveryDate = orderDetail.CustomerDueDate ?? DateTime.Now,

                                DeliveryEmployee = employeeName,
                                ReceiveEmployee = employeeName,

                                ProductId = orderDetail.ProductId,
                                ProductCode = orderDetail.Product.ProductCode,
                                Productivity = orderDetail.Product.Productivity ?? 0,
                                ProductionRate = orderDetail.Product.ProductionRate ?? 0,

                                MaterialId =  material!=null ? material.MaterialId : 0,
                                MaterialCode = material!=null ?  material.Material.MaterialCode :"",
                                MaterialTypeName = material!=null ? material.Material.MaterialType.MaterialTypeName: "",

                                //MachineId = track.MachineId,
                                MachineName = machineName,
                                ToolTypeId = detail.TypeId,
                                ToolTypeName = detail.TypeName,
                                MaterialInv = materialInv
                            };
                            var sameMaterialProductions = vfi.SelectCurrentTrackUpMachines
                                .Where(t => t.MaterialId == entity.MaterialId);
                            foreach (var sameMaterialProduction in sameMaterialProductions) {
                                var same = new SamePreparation {
                                    MachineId = sameMaterialProduction.MachineId,
                                    MachineName = sameMaterialProduction.MachineName
                                };
                                entity.SameMachines.Add(same);
                            }
                            index++;
                            model.Add(entity);

                        }
                        entity.Details.Add(detail);
                    }
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return PartialView("PagePreparationTools", model);
        }
        #endregion

        #region tâm ma


        [GridAction]
        public ActionResult SelectAllOrders(string poNumber) {
            using (var vfi = new tammaContext()) {
                var models = new List<OrderModel>();
                var entities = vfi.Orders.Where(o => o.DueDate >= DateTime.Now).ToList();
                if (!String.IsNullOrWhiteSpace(poNumber))
                    entities = entities.Where(o => o.PoNumber == poNumber).ToList();
                foreach (var entity in entities) {
                    var model = new OrderModel {
                        OrderId = entity.OrderId,
                        DueDate = entity.DueDate,
                        ModifiedDate = entity.ModifiedDate,
                        OrderDate = entity.OrderDate,
                        PoNumber = entity.PoNumber,
                        OrderNumber = entity.OrderNumber,
                        CurrencyCode = entity.CurrencyCode,

                    };
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == entity.CustomerId);
                    model.CustomerCode = customer.CustomerCode;
                    model.CustomerName = customer.CustomerName;
                    model.ShipMethodName = vfi.ShipMethods.FirstOrDefault(sm => sm.ShipMethodId == entity.ShipMethodId).Name;
                    model.SalesPersonName = vfi.Employees.FirstOrDefault(sm => sm.EmployeeId == entity.SalesPersonId).EmployeeName;

                    model.CurrencyCode = vfi.Currencies.FirstOrDefault(cc => cc.CurrencyCode == entity.CurrencyCode).CurrencyName;

                    model.TotalQuality =
                        vfi.OrderDetails.Where(od => od.OrderId == entity.OrderId).Sum(od => od.OrderQty) ?? 0;
                    models.Add(model);
                }
                return View(new GridModel(models.OrderByDescending(o => o.OrderDate)));
            }
        }

        [GridAction]
        public ActionResult SelecOrderDetailByOrderId(long orderId) {
            if (orderId == 0)
                return View(new GridModel(new List<OrderDetail>()));
            using (var vfi = new tammaContext()) {
                var entities = vfi.OrderDetails.Where(od => od.OrderId == orderId);
                var models = new List<OrderDetailModel>();
                foreach (var orderDetail in entities) {
                    var model = new OrderDetailModel {
                        VFIDueDate = orderDetail.VFIDueDate,
                        CustomerDueDate = orderDetail.CustomerDueDate,
                        OrderQty = orderDetail.OrderQty ?? 0,
                        LotNumber = orderDetail.LotNumber,
                        ModelNumber = orderDetail.ModelNumber,
                        UnitPrice = orderDetail.UnitPrice,

                    };

                    model.ProductCode = vfi.Products.FirstOrDefault(p => p.ProductId == orderDetail.ProductId).ProductCode;
                    model.LineTotal = model.OrderQty * model.UnitPrice;
                    models.Add(model);
                }
                return View(new GridModel(models));

            }
        }

        // approve order
        List<OrderModel> GetOrderNeedApprove() {
            var model = new List<OrderModel>();
            using (var vfi = new tammaContext()) {
                var orders = (from x in vfi.Orders
                              where x.DueDate == null && x.Active && x.OrderDetails.Any()
                              select new {
                                  x.OrderId,
                                  x.CustomerId,
                                  x.OrderNumber,
                                  x.PoNumber,
                                  x.Customer.CustomerCode,
                                  x.Customer.IsNotRequireApproveOrder,
                                  x.ModifiedDate,
                                  x.ModifiedUser,
                                  x.CurrencyCode,
                                  x.Note,
                                  x.OrderDate,
                                  x.OrderDetails,
                                  x.Customer.Area.AreaName,
                                  SalesPersonName = x.SalesPersonId != null ? x.Employee.EmployeeName : ""
                              }).ToList();
                var isSalesManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.SaleManagement);
                foreach (var order in orders) {
                    var entity = new OrderModel {
                        CustomerId = order.CustomerId,
                        OrderId = order.OrderId,
                        OrderNumber = order.OrderNumber,
                        PoNumber = order.PoNumber ?? "",
                        CustomerCode = order.CustomerCode,
                        TotalQuality = order.OrderDetails.Sum(od => od.OrderQty).Value,
                        ModifiedDate = order.ModifiedDate,
                        Area = order.AreaName,
                        CurrencyCode = order.CurrencyCode,
                        Note = order.Note,
                        OrderDate = order.OrderDate,
                        SalesPersonName = order.SalesPersonName,
                        SalesManager = isSalesManager,
                        CanApprove = order.IsNotRequireApproveOrder
                    };
                    var detail = order.OrderDetails.FirstOrDefault(od => od.Status != (int)MyUtilities.Sales.Status.Completed);
                    if (detail != null) {
                        entity.DetailStatus = 1;
                    }
                    else {
                        entity.DueDate = order.OrderDetails.OrderByDescending(od => od.VFIDueDate).FirstOrDefault().VFIDueDate;
                        if (isSalesManager) {
                            entity.CanApprove = true;
                        }
                    }
                    model.Add(entity);
                }
                return model;
            }
        }

        [GridAction]
        public ActionResult SelectOrder() {
            return View(new GridModel(GetOrderNeedApprove().OrderByDescending(o => o.ModifiedDate)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult ApproveOrder(int orderId, string dueDate) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            try {
                var orderDetailIds = new List<long>();
                var isWorkOrder = false;
                using (var vfi = new tammaContext()) {
                    // save approve order
                    var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                    var productIds = order.OrderDetails.Select(od => od.ProductId).Distinct().ToList();
                    if (order.Customer.IsWorkOrder) {
                        isWorkOrder = order.Customer.IsWorkOrder;
                        var checkProduct = _workOrderController.CheckValidProductValue(productIds);
                        if (checkProduct.Code != (int)MyUtilities.Monitor.ErrorCode.NoError) {
                            throw new AggregateException(checkProduct.Message);
                        }
                    }
                    var orderDetail = order.OrderDetails.OrderByDescending(od => od.VFIDueDate).FirstOrDefault();
                    if (Convert.ToDateTime(dueDate) < orderDetail.VFIDueDate)
                        throw new AggregateException("Lỗi! Không thể duyệt ngày nhỏ hơn chi tiết");
                    order.DueDate = Convert.ToDateTime(dueDate);
                    order.Status = (byte)MyUtilities.Sales.Status.Waiting;
                    order.Active = true;
                    order.ApprovedDate = DateTime.Now;
                    order.ApprovedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();

                    // update forecast order
                    var allOrderDetails = vfi.OrderDetails.Where(od => productIds.Contains(od.ProductId) &&
                        od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                        od.Order.DueDate != null &&
                       od.Order.DueDate.Value.Month == order.DueDate.Value.Month &&
                       od.Order.DueDate.Value.Year == order.DueDate.Value.Year &&
                       od.OrderId != order.OrderId)
                       .Select(x => new { x.ProductId, x.OrderQty }).ToList();
                    foreach (var productId in productIds) {
                        var forecast = vfi.ForecastOrders.FirstOrDefault(f => f.ProductId == productId &&
                            f.ForecastDate.Month == order.DueDate.Value.Month &&
                            f.ForecastDate.Year == order.DueDate.Value.Year);
                        if (forecast == null) {
                            forecast = new ForecastOrder {
                                ProductId = productId,
                                ForecastDate = order.DueDate.Value,
                                Quantity = 0,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name + "-Auto",
                                IsSelling = true,
                                Status = (byte)MyUtilities.Transaction.Status.Approved,
                            };
                            vfi.ForecastOrders.Add(forecast);
                        }
                        var orderDetailsById = allOrderDetails.Where(od => od.ProductId == productId).ToList();
                        var orderDetailsInOrder = order.OrderDetails.Where(od => od.ProductId == productId).ToList();
                        var totalQuantity = orderDetailsById.Sum(od => od.OrderQty.Value) +
                            orderDetailsInOrder.Sum(od => od.OrderQty.Value);
                        if (forecast.Quantity < totalQuantity) {
                            forecast.Quantity = totalQuantity;
                            forecast.ModifiedDate = DateTime.Now;
                        }
                    }
                    vfi.SaveChanges();

                    // save order progress auto
                    orderDetailIds = order.OrderDetails.Select(x => x.OrderDetailId).ToList();
                }
                // save order progress auto
                foreach (var detailId in orderDetailIds) {
                    var orderProgresses = _salesOrderController.GetProductionExpectedByOrderDetail(detailId).Where(x => x.ModifiedUser.Equals("Auto") && x.StartDate != null).ToList();
                    foreach (var progress in orderProgresses) {
                        _salesOrderController.SaveOrderProgess(progress, HttpContext.User.Identity.Name);
                    }
                }

                // save workorder
                if (isWorkOrder) {
                    _workOrderController.SaveWorkOrders(orderId, 0);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ApproveOrder", ex.Message);
            }
            return View(new GridModel(GetOrderNeedApprove().OrderByDescending(o => o.ModifiedDate)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult CancelOrder(int orderId) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            using (var vfi = new tammaContext()) {
                var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                order.Active = false;
                order.Status = (byte)MyUtilities.Sales.Status.Cancel;
                vfi.SaveChanges();
            }
            return View(new GridModel(GetOrderNeedApprove().OrderByDescending(o => o.ModifiedDate)));
        }

        public ActionResult CreateWorkOrder(int orderId) {
            try {
                using (var vfi = new tammaContext()) {
                    var orderDetails = vfi.OrderDetails.Where(x => x.OrderId == orderId).ToList();
                    var orderDetailIds = orderDetails.Select(x => x.OrderDetailId).ToList();
                    var isWorkOrder = vfi.WorkOrders.Any(x => orderDetailIds.Contains(x.OrderDetailId)
                        && x.Status != (byte)MyUtilities.WorkOrder.Status.Cancel);
                    if (isWorkOrder) {
                        return Json(new MyUtilities.Monitor.MyJsonResult(
                            (int)MyUtilities.Monitor.ErrorCode.StatusChanged,
                            "Đã có workorder, không thể dùng chức năng này",
                            0));

                    }
                    var productIds = orderDetails.Select(x => x.ProductId).Distinct().ToList();
                    var checkProduct = _workOrderController.CheckValidProductValue(productIds);
                    if (checkProduct.Code != (int)MyUtilities.Monitor.ErrorCode.NoError) {
                        return Json(new MyUtilities.Monitor.MyJsonResult(
                            (int)MyUtilities.Monitor.ErrorCode.StatusChanged,
                            checkProduct.Message,
                            0));
                    }

                    _workOrderController.SaveWorkOrders(orderId, 0);
                    return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.NoError,
                        "Hoàn thành",
                        0));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult(
                    (int)MyUtilities.Monitor.ErrorCode.Exception,
                    ex.Message,
                    0));
            }
            //return Json(new MyUtilities.Monitor.MyJsonResult(
            //    (int)MyUtilities.Monitor.ErrorCode.NotImplement,
            //    "",
            //    0));
        }
        public ActionResult SelectOrderInProcess() {
            using (var vfi = new tammaContext()) {
                var orderModels =
                   (from o in vfi.Orders
                    where
                         o.DueDate != null && o.Active &&
                         (o.Status == (byte)MyUtilities.Sales.Status.InProcess || o.Status == (byte)MyUtilities.Sales.Status.Waiting)
                    select new OrderModel {
                        OrderId = o.OrderId,
                        OrderNumber = o.OrderNumber + " - " + o.Customer.CustomerCode
                    }).ToList();
                return new JsonResult {
                    Data = new SelectList(orderModels, "OrderId", "OrderNumber")
                };
            }
        }

        public ActionResult SelectOrderApprovedDueDate() {
            using (var vfi = new tammaContext()) {
                var orderModels =
                   (from o in vfi.Orders
                    where
                         o.DueDate != null && o.Active &&
                         (o.Status == (byte)MyUtilities.Sales.Status.InProcess || o.Status == (byte)MyUtilities.Sales.Status.Waiting)
                    select new OrderModel {
                        OrderId = o.OrderId,
                        OrderNumber = o.OrderNumber + " - " + o.Customer.CustomerCode
                    }).ToList();
                return new JsonResult {
                    Data = new SelectList(orderModels, "OrderId", "OrderNumber")
                };
            }
        }
        public ActionResult SelectOrderNotApprovedDueDate() {
            using (var vfi = new tammaContext()) {
                var orderModels =
                   (from o in vfi.Orders
                    where
                        o.DueDate == null && o.Active &&
                        o.Status != (byte)MyUtilities.Sales.Status.Cancel
                    select new OrderModel {
                        OrderId = o.OrderId,
                        OrderNumber = o.OrderNumber + " - " + o.Customer.CustomerCode
                    }).ToList();
                return new JsonResult {
                    Data = new SelectList(orderModels, "OrderId", "OrderNumber")
                };
            }
        }

        #endregion

        #region invoice

        public ActionResult SelectStatus() {
            var val = from MyUtilities.Sales.Status stt in Enum.GetValues(typeof(MyUtilities.Sales.Status))
                      select new {
                          Value = (int)Enum.Parse(typeof(MyUtilities.Sales.Status), stt.ToString()),
                          Text = stt.ToString()
                      };

            return new JsonResult {
                Data = new SelectList(val, "Value", "Text")
            };
        }

        // in invoice tam
        public List<InvoiceTempModel> PrepareInvoiceTemp(string currency) {
            var model = new List<InvoiceTempModel>();

            using (var vfi = new tammaContext()) {
                var startTaxInvoiceDate = new DateTime(2014, 12, 31, 10, 0, 0);
                //vfi.Configuration.LazyLoadingEnabled = false;
                var invoices = from i in vfi.Invoices
                               where
                                   (i.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                    i.Status == (byte)MyUtilities.Sales.Status.InProcess)
                                    && i.ShipmentDate > startTaxInvoiceDate
                               //&& i.InvoiceId == 1558
                               select new {
                                   i.InvoiceId,
                                   i.InvoiceNumber,
                                   i.ExportId,
                                   i.OrderId,
                                   i.Note,
                                   i.Active,
                                   i.ExchangeRate,
                                   i.TaxPercent,
                                   i.ModifiedDate,
                                   i.ModifiedUser,
                               };
                foreach (var invoice in invoices) {
                    //int a = 0;
                    //if (invoice.InvoiceNumber.Contains("14200"))
                    //    a = 5;
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                    if (export == null) continue;
                    //if (export.DateTransporter < new DateTime(2014, 7, 1, 0, 0, 0)) continue;
                    var order =
                        vfi.Orders.FirstOrDefault(
                            o => o.OrderId == invoice.OrderId);
                    if (!string.IsNullOrWhiteSpace(currency))
                        if (!order.CurrencyCode.Equals(currency)) continue;
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
                    var entity = new InvoiceTempModel {
                        InvoiceId = invoice.InvoiceId,
                        InvoiceNumber = invoice.InvoiceNumber,
                        ShiftmentDate = export.DateTransporter != null ? export.DateTransporter.Value : DateTime.Now,
                        ExportId = invoice.ExportId.Value,
                        OrderId = invoice.OrderId.Value,
                        OrderNumber = order.OrderNumber ?? "",
                        PoNumber = order.PoNumber ?? "",
                        BillToAddress = order.BillToAddress ?? "",
                        CurrencyCode = order.CurrencyCode ?? "",
                        ShipToAddress = order.ShipToAddress ?? "",
                        CustomerId = customer.CustomerId,
                        CustomerCode = customer.CustomerCode,
                        ModifiedDate = invoice.ModifiedDate ?? DateTime.Now,
                        ModifiedUser = invoice.ModifiedUser ?? "",
                        TaxPercent = invoice.TaxPercent,
                        Note = invoice.Note,
                        Active = invoice.Active,
                        ExchangeRate = invoice.ExchangeRate,
                        TotalQuantity = 0,
                        TaxInvoice = "",
                        EmployeeSale = order.Employee.EmployeeName,
                    };
                    var orderNotes = from orderNote in vfi.OrderNotes
                                     where orderNote.InvoiceId == invoice.InvoiceId && orderNote.NoteType == 1
                                     select new {
                                         orderNote.OrderNoteDetails,
                                         orderNote.Transaction,
                                         orderNote.NoteId,
                                     };
                    var total = 0.0;

                    foreach (var exportFormTpKdDetail in export.ExportFormTP_KDDetail) {
                        var orderDetail =
                            order.OrderDetails.FirstOrDefault(od => od.ProductId == exportFormTpKdDetail.ProductId);
                        if (orderDetail == null) {
                            ModelState.AddModelError("invoice error!",
                                                     "orderdetail null " + invoice.InvoiceNumber);
                        }
                        else {
                            total += ((exportFormTpKdDetail.Quality) * orderDetail.UnitPrice);
                            entity.TotalQuantity += exportFormTpKdDetail.Quality;
                            var orderNotesByProductId =
                                orderNotes.Where(
                                    on =>
                                    on.OrderNoteDetails.FirstOrDefault(
                                        ond => ond.ProductId == exportFormTpKdDetail.ProductId) != null);
                            if (orderNotesByProductId.Any()) {
                                foreach (var orderNote in orderNotesByProductId) {
                                    if (orderNote.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved) {
                                        var orderNoteDetail =
                                            vfi.OrderNoteDetails.FirstOrDefault(
                                                ond =>
                                                ond.NoteId == orderNote.NoteId &&
                                                ond.ProductId == exportFormTpKdDetail.ProductId);
                                        if (orderNoteDetail != null) {
                                            total -= ((orderNoteDetail.Quantity.Value) * orderDetail.UnitPrice);
                                            entity.TotalQuantity -= orderNoteDetail.Quantity.Value;
                                        }
                                    }
                                }
                            }
                        }
                        if (exportFormTpKdDetail.IsInvoiced ?? false) {
                            var taxInvoiceProduct =
                                vfi.TaxInvoiceProductDetails.FirstOrDefault(
                                    tipd => tipd.ExportDetailId == exportFormTpKdDetail.DetailId && tipd.Active == true);
                            if (taxInvoiceProduct != null)
                                if (!entity.TaxInvoice.Contains(taxInvoiceProduct.TaxInvoice.TaxInvoiceList))
                                    entity.TaxInvoice += (taxInvoiceProduct.TaxInvoice.TaxInvoiceList + " ");
                        }
                    }
                    entity.TotalAmount = total;
                    entity.TotalAmountAfterTax = ((total * entity.TaxPercent / 100) + total);
                    entity.TotalAmountAfterTax = (double)Math.Round((decimal)entity.TotalAmountAfterTax, 2);
                    entity.TotalAmountAfterTaxVND = entity.TotalAmountAfterTax * entity.ExchangeRate;
                    entity.TotalAmountAfterTaxVND = (double)Math.Round((decimal)entity.TotalAmountAfterTaxVND);
                    model.Add(entity);
                }
            }
            return model;
        }


        // in invoice tam
        public List<InvoiceTempModel> PrepareInvoiceTemp_New(string currency, int month, int year) {
            var model = new List<InvoiceTempModel>();

            using (var vfi = new tammaContext()) {
                var startTaxInvoiceDate = new DateTime(2014, 12, 31, 10, 0, 0);
                //vfi.Configuration.LazyLoadingEnabled = false;
                var invoices = (from i in vfi.Invoices
                               where
                                   (i.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                    i.Status == (byte)MyUtilities.Sales.Status.InProcess)
                                   //&& i.ShipmentDate > startTaxInvoiceDate
                                   && i.ShipmentDate.Value.Month == month
                                   && i.ShipmentDate.Value.Year == year
                                   && i.Active
                               select new {
                                   i.InvoiceId,
                                   i.InvoiceNumber,
                                   i.ExportId,
                                   i.OrderId,
                                   i.Note,
                                   i.Active,
                                   i.ExchangeRate,
                                   i.TaxPercent,
                                   i.ModifiedDate,
                                   i.ModifiedUser,
                                   i.Customer.CustomerCode,
                                   i.CustomerId,
                               }).ToList();
                foreach (var invoice in invoices) {
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                    if (export == null) continue;
                    var entity = new InvoiceTempModel {
                        InvoiceId = invoice.InvoiceId,
                        InvoiceNumber = invoice.InvoiceNumber,
                        ShiftmentDate = export.DateTransporter != null ? export.DateTransporter.Value : DateTime.Now,
                        ExportId = invoice.ExportId.Value,
                        //OrderId = invoice.OrderId.Value,
                        //OrderNumber = order.OrderNumber ?? "",
                        //PoNumber = order.PoNumber ?? "",
                        //BillToAddress = order.BillToAddress ?? "",
                        //CurrencyCode = order.CurrencyCode ?? "",
                        //ShipToAddress = order.ShipToAddress ?? "",
                        CustomerId = invoice.CustomerId,
                        CustomerCode = invoice.CustomerCode,
                        ModifiedDate = invoice.ModifiedDate ?? DateTime.Now,
                        ModifiedUser = invoice.ModifiedUser ?? "",
                        TaxPercent = invoice.TaxPercent,
                        Note = invoice.Note,
                        Active = invoice.Active,
                        ExchangeRate = invoice.ExchangeRate,
                        TotalQuantity = 0,
                        TaxInvoice = "",
                        //EmployeeSale = order.Employee.EmployeeName,
                    };
                    foreach (var exportDetail in export.ExportFormTP_KDDetail) {
                        if (exportDetail.InvoiceDetails != null && exportDetail.InvoiceDetails.Any(id => id.Active)) {
                            entity.TotalQuantity += exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece);
                            entity.TotalAmount += (exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece) *
                                                   exportDetail.InvoiceDetails.FirstOrDefault(id => id.Active).Price.Value);
                        }
                        else {
                            entity.TotalQuantity += (exportDetail.Quality);
                        }

                        if (exportDetail.IsInvoiced ?? false) {
                            var taxInvoiceProduct =
                                vfi.TaxInvoiceProductDetails.FirstOrDefault(
                                    tipd => tipd.ExportDetailId == exportDetail.DetailId && tipd.Active == true);
                            if (taxInvoiceProduct != null)
                                if (!entity.TaxInvoice.Contains(taxInvoiceProduct.TaxInvoice.TaxInvoiceList))
                                    entity.TaxInvoice += (taxInvoiceProduct.TaxInvoice.TaxInvoiceList + " ");
                        }
                    }
                    //entity.TotalAmount = total;
                    entity.TotalAmountAfterTax = entity.TotalAmount + (entity.TotalAmount * entity.TaxPercent / 100);
                    entity.TotalAmountAfterTax = (double)Math.Round((decimal)entity.TotalAmountAfterTax, 2);
                    entity.TotalAmountAfterTaxVND = entity.TotalAmountAfterTax * entity.ExchangeRate;
                    entity.TotalAmountAfterTaxVND = (double)Math.Round((decimal)entity.TotalAmountAfterTaxVND);
                    model.Add(entity);
                }
            }
            return model;
        }

        List<InvoiceDetailTempModel> InvoiceDetailListByExportId(int exportId) {
            var model = new List<InvoiceDetailTempModel>();
            using (var vfi = new tammaContext()) {
                var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == exportId);
                if (export == null)
                    throw new AggregateException("Lỗi không tìm thấy phiếu xuất");
                //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == export.OrderId);
                //if (order == null)
                //    throw new AggregateException("Lỗi không tìm hóa đơn");
                var orderNotes = vfi.OrderNotes.Where(on => on.ExportId == export.ExportId && on.NoteType == 1);
                var invoice = vfi.Invoices.FirstOrDefault(i => i.ExportId == exportId);
                foreach (var detail in export.ExportFormTP_KDDetail) {
                    //var orderDetail = order.OrderDetails.FirstOrDefault(od => od.ProductId == detail.ProductId);
                    var entity = new InvoiceDetailTempModel {
                        ExportId = exportId,
                        DetailId = detail.DetailId,
                        ProductCode = detail.Product.ProductCode,
                        Quantity = detail.Quality,
                        //UnitPrice = orderDetail.UnitPrice,
                        //PONumber = orderDetail.PONumber,
                        TaxPercent = invoice.TaxPercent,
                        ExchangeRate = invoice.ExchangeRate,
                        ExportDetailId = detail.DetailId
                    };
                    var orderNotesByProductId =
                        orderNotes.Where(
                            on =>
                            on.OrderNoteDetails.FirstOrDefault(ond => ond.ProductId == detail.ProductId) != null &&
                            on.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);

                    if (orderNotes.Any()) {
                        foreach (var orderNote in orderNotesByProductId) {
                            //var transaction =
                            //    vfi.Transactions.FirstOrDefault(
                            //        t =>
                            //        t.TransactionId == orderNote.TransactionId &&
                            //        t.Status == (byte) MyUtilities.Transaction.Status.Approved);
                            //if (transaction != null)
                            //{
                            var orderNoteDetail =
                                orderNote.OrderNoteDetails.FirstOrDefault(
                                    ond => ond.ProductId == detail.ProductId);
                            entity.Quantity -= orderNoteDetail.Quantity.Value;
                            entity.Note += "! Trả hàng:" + orderNoteDetail.Quantity.Value;
                            //}
                        }
                    }
                    if (entity.Quantity == 0) {
                        if (detail.IsInvoiced == null || !detail.IsInvoiced.Value) {
                            detail.IsInvoiced = true;
                            vfi.SaveChanges();
                        }
                    }
                    if (export.ExportFormTP_KDDetail.Count(ed => ed.IsInvoiced == true) ==
                                export.ExportFormTP_KDDetail.Count()) {
                        invoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                        vfi.SaveChanges();
                    }
                    //if (order.CurrencyCode.Equals("USD"))
                    //    entity.UnitPriceCurrency = string.Format("{0:N4}", entity.UnitPrice);
                    //if (order.CurrencyCode.Equals("VND"))
                    //    entity.UnitPriceCurrency = string.Format("{0:N0}", entity.UnitPrice);
                    var taxInvoiceProduct =
                        vfi.TaxInvoiceProductDetails.FirstOrDefault(tip => tip.ExportDetailId == entity.DetailId && tip.Active == true);
                    if (taxInvoiceProduct != null) {
                        entity.TaxInvoiceList = taxInvoiceProduct.TaxInvoice.TaxInvoiceList;
                        entity.TaxPercent = taxInvoiceProduct.TaxInvoice.TaxPercent;
                        entity.ExchangeRate = taxInvoiceProduct.TaxInvoice.ExchangeRate;
                    }
                    var invoiceDetails = vfi.InvoiceDetails.Where(id => id.ExportDetailId == entity.ExportDetailId && id.Active);
                    if (invoiceDetails.Any()) {
                        entity.Amount = (invoiceDetails.Sum(id => id.Piece * id.Price).Value * (1 + entity.TaxPercent / 100));
                        entity.AmountVnd = entity.Amount * entity.ExchangeRate;
                    }
                    else entity.Note += "Chưa phân đơn hàng";
                    model.Add(entity);
                }
            }

            return model;
        }


        List<InvoiceDetailTempModel> GetDetailByExportId(int exportId) {
            var model = new List<InvoiceDetailTempModel>();
            using (var vfi = new tammaContext()) {
                var export = vfi.ExportFormTP_KD.FirstOrDefault(t => t.ExportId == exportId);
                if (export == null) {
                    throw new AggregateException("Loi chi tiet");
                }
                var transactionList = vfi.Transactions.Where(t => t.TransactionCode == export.TransactionCode).Select(t => t.TransactionId).FirstOrDefault();
                //var transactionList2 = vfi.Transactions.FirstOrDefault(t => t.TransactionCode == export.TransactionCode);

                var transactionDetailList = vfi.TransactionDetails.Where(t => t.TransactionId == transactionList)
                                                                  .Select(t => new {
                                                                      LotNumber = t.LotNumber,
                                                                      Quantity = t.Quantity,
                                                                      ProductCode = t.Product.ProductCode,
                                                                      Note = t.Note,
                                                                  }).OrderBy(t => t.ProductCode)
                                                                  .ThenBy(t => t.LotNumber).ToList();
                var index = 1;

                foreach (var transaction in transactionDetailList) {
                    var entity = new InvoiceDetailTempModel {
                        LotNumber = transaction.LotNumber,
                        Number = transaction.Quantity,
                        ProductCodeDetail = transaction.ProductCode,
                        NoteDetail = transaction.Note,
                        Index = index,
                    };
                    var serialNumber = entity.LotNumber.Split('-').Length > 0
                           ? entity.LotNumber.Split('-')[0]
                           //? entity.LotNumber.Substring(0, entity.LotNumber.LastIndexOf("-"))
                           : entity.LotNumber;
                    var workOrderId = vfi.WorkOrders.Where(t => t.SerialNumber == serialNumber).Select(t => t.WorkOrderId).FirstOrDefault();
                    var ProductId  = vfi.WorkOrders.Where(t => t.SerialNumber == serialNumber).Select(t => t.ProductId).FirstOrDefault();
                    var entityProductId = vfi.Products.Where(t => t.ProductCode == entity.ProductCodeDetail).Select(t => t.ProductId).FirstOrDefault();
                    if (workOrderId != 0 && ProductId == entityProductId) {
                        var machineId = vfi.WorkOrderRoutings.Where(t => t.WorkOrderId == workOrderId && t.RoutingIndex == 1).Select(t => t.MachineId).FirstOrDefault();
                        if (machineId != null) {
                            entity.MachineCode = vfi.Machines.Where(t => t.MachineId == machineId).Select(t => t.MachineName).FirstOrDefault();
                        }
                        else {
                            entity.MachineCode = ""; 
                        }
                        var serialDate = vfi.WorkOrders.Where(t => t.SerialNumber == serialNumber).Select(t => new { StartDate = t.StartDate, EndDate = t.EndDate, }).ToList();
                        entity.StartDate = serialDate.Select(t => t.StartDate).FirstOrDefault();
                        entity.EndDate = serialDate.Select(t => t.EndDate).FirstOrDefault();
                    }
                    model.Add(entity);
                    index ++;
                }
            }
            return model.OrderBy(t => t.Index).ToList();
        }


        List<InvoiceDetailTempModel> GetMaterialCertificateByExportId(int exportId) {
            var model = new List<InvoiceDetailTempModel>();
            using (var vfi = new tammaContext()) {
                var export = vfi.ExportFormTP_KD.FirstOrDefault(t => t.ExportId == exportId);
                if (export == null) {
                    throw new AggregateException("Loi chi tiet");
                }
                var transactionList = vfi.Transactions.Where(t => t.TransactionCode == export.TransactionCode).Select(t => t.TransactionId).FirstOrDefault();
                //var transactionList2 = vfi.Transactions.FirstOrDefault(t => t.TransactionCode == export.TransactionCode);

                var transactionDetailList = vfi.TransactionDetails.Where(t => t.TransactionId == transactionList)
                                                                  .Select(t => new {
                                                                      LotNumber = t.LotNumber,
                                                                      Quantity = t.Quantity,
                                                                      ProductCode = t.Product.ProductCode,
                                                                  }).ToList();
                var bigIndex = 1;

                var results = (
                    from td in transactionDetailList
                    let lotNumber = td.LotNumber.Split('-').Length > 0
                        ? td.LotNumber.Split('-').Last()
                        : td.LotNumber
                    let serialNumber = td.LotNumber.Split('-').Length > 0           
                        ? td.LotNumber.Split('-')[0]
                        //? td.LotNumber.Substring(0, td.LotNumber.LastIndexOf("-"))
                        : td.LotNumber
                    let workOrderId = vfi.WorkOrders
                        .Where(w => w.SerialNumber == serialNumber)
                        .Select(w => w.WorkOrderId)
                        .FirstOrDefault()
                    let materialInvId = vfi.WorkOrderRoutings
                        .Where(r => r.WorkOrderId == workOrderId)
                        .Select(r => r.MaterialInvId)
                        .FirstOrDefault()
                    let materialId = vfi.MaterialInventories
                        .Where(mi => mi.MaterialInventoryId == materialInvId)
                        .Select(mi => mi.MaterialId)
                        .FirstOrDefault()
                    select new {
                        td.ProductCode,
                        materialId,
                        LotNumber = lotNumber,
                        td.Quantity
                    }
                )
                .GroupBy(x => new { x.ProductCode, x.materialId, x.LotNumber })
                .Select(g => new InvoiceDetailTempModel {
                    ProductCode = g.Key.ProductCode,
                    MaterialId = g.Key.materialId,
                    LotNumber = g.Key.LotNumber,
                    // cộng dồn Quantity
                    Quantity = g.Sum(x => x.Quantity)
                }).OrderBy(t => t.ProductCode)
                .ThenBy(t => t.LotNumber)
                .ToList();

                foreach (var result in results) {
                    var entity = new InvoiceDetailTempModel() {
                        ProductCodeDetail = result.ProductCode,
                        Number = result.Quantity,
                        MaterialId = result.MaterialId,
                        LotNumber = result.LotNumber,
                        Index = bigIndex,
                    };
                    entity.MaterialCode = vfi.Materials.Where(t => t.MaterialId == entity.MaterialId).Select(t => t.MaterialCode).FirstOrDefault();
                    entity.InfoImg = vfi.MaterialInventories.Where(t => t.MaterialId == entity.MaterialId && t.LotNumber == entity.LotNumber).Select(t => t.InfoImg).FirstOrDefault();
                    entity.InfoImg2 = vfi.MaterialInventories.Where(t => t.MaterialId == entity.MaterialId && t.LotNumber == entity.LotNumber).Select(t => t.InfoImg2).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(entity.InfoImg))
                        entity.InfoImg = "askquestion.jpg";
                    if (string.IsNullOrWhiteSpace(entity.InfoImg2))
                        entity.InfoImg2 = "askquestion.jpg";
                    if (!string.IsNullOrWhiteSpace(entity.MaterialCode)){
                    entity.ImportDate = vfi.MaterialInventories.Where(t => t.LotNumber == entity.LotNumber
                                                                      && t.MaterialId == entity.MaterialId).Select(t => t.ImportDate).FirstOrDefault().ToString("dd/MM/yyyy") == "01/01/0001"
                                            ? ""
                                            : vfi.MaterialInventories.Where(t => t.LotNumber == entity.LotNumber
                                                                            && t.MaterialId == entity.MaterialId).Select(t => t.ImportDate).FirstOrDefault().ToString("dd/MM/yyyy");

                    entity.LastUsedDate = vfi.MaterialInventories.Where(t => t.LotNumber == entity.LotNumber
                                                                        && t.MaterialId == entity.MaterialId).Select(t => t.EndDate).FirstOrDefault() == null
                                          ? ""
                                          : vfi.MaterialInventories.Where(t => t.LotNumber == entity.LotNumber
                                                                          && t.MaterialId == entity.MaterialId).Select(t => t.EndDate).FirstOrDefault().Value.ToString("dd/MM/yyyy");
                    }
                    model.Add(entity);
                    bigIndex++;
                }


                // 1) Lấy phần trước dấu “-” đầu tiên
                //var serialNumberList = transactionDetailList
                //    .Select(t => {
                //        var index = t.LotNumber.LastIndexOf("-");
                //        return index > 0 ? t.LotNumber.Substring(0, index) : t.LotNumber;
                //    })
                //    .Distinct()
                //    .ToList();


                // 2) Lấy phần trước dấu “-” cuối cùng
                //var serialNumberList = transactionDetailList
                //    .Select(t => {
                //        var index = t.LotNumber.LastIndexOf("-");
                //        return index > 0 ? t.LotNumber.Substring(0, index) : t.LotNumber;
                //    })
                //    .Distinct()
                //    .ToList();


                //// 3) Dùng Split: lay phan sau "-" -------- có thể chọn parts[0], parts[1], hoặc parts.Last().
                //var trueLotNumber = transactionDetailList
                //    .Select(t => {
                //        var parts = t.LotNumber.Split('-');
                //        return parts.Length > 0 ? parts.Last() : t.LotNumber;
                //    })
                //    .Distinct()
                //    .ToList();

            }
            return model;
        }

         [GridAction]
        public ActionResult SelectTransactionImgById(int exportId) {
            var model = new List<TransactionImgModel>();
            try {
                model = GetTransactionImgById(exportId);
            }
            catch (Exception ex) {
                {
                      Console.WriteLine(ex.InnerException.Message);
                }
            }   
            return View(new GridModel(model));
        }


         List<TransactionImgModel> GetTransactionImgById(int exportId) {
             var model = new List<TransactionImgModel>();
             //var technicalManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.TechicalManagerLv2);

             using (var vfi = new tammaContext()) {
                 var index = 1;
                 var transactionCode = vfi.ExportFormTP_KD.Where(t => t.ExportId == exportId).Select(t => t.TransactionCode).FirstOrDefault();
                 var transactionId = vfi.Transactions.Where(t => t.TransactionCode == transactionCode).Select(t => t.TransactionId).FirstOrDefault();
                 var transactionImgs = vfi.TransactionImgs.Where(t => t.TransactionId == transactionId).OrderBy(t => t.ModifiedDate);
                 foreach (var transactionImg in transactionImgs) {
                     var entity = new TransactionImgModel {
                         ImgId = transactionImg.ImgId,
                         TransactionId = (long)transactionId,
                         ImgUrl = transactionImg.ImgUrl,
                         TransactionNumber = transactionImg.TransactionNumber,
                         ModifiedDate = transactionImg.ModifiedDate,
                         ModifiedUser = transactionImg.ModifiedUser,
                         //CanModify = technicalManager,
                         Description = transactionImg.Description,
                         Name = transactionImg.Name,
                         Index = index,
                     };
                     model.Add(entity);
                     index++;
                 }
             }
             return model;
         }


        List<InvoiceDetailTempModel> InvoiceDetailListByExportId_New(int exportId, long invoiceId) {
            var model = new List<InvoiceDetailTempModel>();
            using (var vfi = new tammaContext()) {
                var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == exportId);
                if (export == null)
                    throw new AggregateException("Lỗi không tìm thấy phiếu xuất");
                //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == export.OrderId);
                //if (order == null)
                //    throw new AggregateException("Lỗi không tìm hóa đơn");
                var orderNoteDetails = vfi.OrderNoteDetails.Where(x => x.OrderNote.ExportId == export.ExportId 
                    && x.OrderNote.NoteType == 1
                    && x.OrderNote.Transaction.Status ==(byte) MyUtilities.Transaction.Status.Approved).ToList();
                var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
                foreach (var detail in export.ExportFormTP_KDDetail) {
                    //var orderDetail = order.OrderDetails.FirstOrDefault(od => od.ProductId == detail.ProductId);
                    var entity = new InvoiceDetailTempModel {
                        ExportId = exportId,
                        DetailId = detail.DetailId,
                        ProductCode = detail.Product.ProductCode,
                        Quantity = detail.Quality,
                        //UnitPrice = orderDetail.UnitPrice,
                        //PONumber = orderDetail.PONumber,
                        TaxPercent = invoice.TaxPercent,
                        ExchangeRate = invoice.ExchangeRate,
                        ExportDetailId = detail.DetailId,
                        ProductId = detail.ProductId ?? 0,
                        TransactionCode = export.TransactionCode,
                    };


                    var invoiceDetails = vfi.InvoiceDetails.Where(id => id.ExportDetailId == entity.ExportDetailId && id.Active);
                    if (invoiceDetails.Any()) {
                        entity.Quantity = invoiceDetails.Sum(id => id.Piece);
                        entity.UnitPrice = invoiceDetails.FirstOrDefault().Price.Value;
                        if (invoiceDetails.FirstOrDefault().OrderDetailId != null) {
                            entity.CurrencyCode = invoiceDetails.FirstOrDefault().OrderDetail.Order.CurrencyCode;
                        }
                    }
                    else {
                        entity.Note += "Chưa phân đơn hàng";
                    }

                    var orderNotesByProductId = orderNoteDetails.Where(on => on.ProductId == entity.ProductId);

                    if (orderNotesByProductId.Any()) {
                        entity.Note += "! Trả hàng:" + orderNotesByProductId.Sum(x => x.Quantity ?? 0);
                    }
                    // re check quantity to complete invoice
                    {
                        if (entity.Quantity == 0) {
                            if (detail.IsInvoiced == null || !detail.IsInvoiced.Value) {
                                detail.IsInvoiced = true;
                                vfi.SaveChanges();
                            }
                        }
                        if (export.ExportFormTP_KDDetail.Count(ed => ed.IsInvoiced == true) ==
                                    export.ExportFormTP_KDDetail.Count()) {
                            invoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                            vfi.SaveChanges();
                        }
                    }

                    var taxInvoiceProduct =
                        vfi.TaxInvoiceProductDetails.FirstOrDefault(tip => tip.ExportDetailId == entity.DetailId && tip.Active == true);
                    if (taxInvoiceProduct != null) {
                        entity.TaxInvoiceList = taxInvoiceProduct.TaxInvoice.TaxInvoiceList;
                        entity.TaxPercent = taxInvoiceProduct.TaxInvoice.TaxPercent;
                        entity.ExchangeRate = taxInvoiceProduct.TaxInvoice.ExchangeRate;
                    }
                    entity.Amount = (entity.Quantity * entity.UnitPrice) * (1 + entity.TaxPercent / 100);
                    entity.AmountVnd = entity.Amount * entity.ExchangeRate;
                    model.Add(entity);
                }
            }

            return model;
        }




        List<InvoiceDetailTempModel> InvoiceDetailListByExportDetailId(int exportDetailId) {
            var model = new List<InvoiceDetailTempModel>();
            using (var vfi = new tammaContext()) {
                var invoiceDetails = vfi.InvoiceDetails.Where(id => id.ExportDetailId == exportDetailId && id.Active);
                foreach (var invoiceDetail in invoiceDetails) {
                    var orderDetail =
                        vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == invoiceDetail.OrderDetailId);
                    if (orderDetail != null) {
                        var entity = new InvoiceDetailTempModel {
                            OrderDetailId = orderDetail.OrderDetailId,
                            VFIDueDate = orderDetail.Order.DueDate.Value,
                            OrderNumber = orderDetail.Order.OrderNumber,
                            ProductId = orderDetail.ProductId,
                            ProductCode = orderDetail.Product.ProductCode,
                            UnitPrice = orderDetail.UnitPrice,
                            Note = orderDetail.Note,
                            Quantity = invoiceDetail.Piece,
                            ExportDetailId = exportDetailId,
                            CurrencyCode = orderDetail.Order.CurrencyCode,
                            Amount = invoiceDetail.Piece * orderDetail.UnitPrice,
                        };
                        model.Add(entity);
                    }
                    else {
                        var entity = new InvoiceDetailTempModel {
                            //VFIDueDate = orderDetail.Order.DueDate.Value,
                            //OrderNumber = orderDetail.Order.OrderNumber,
                            ProductId = invoiceDetail.ProductId.Value,
                            ProductCode = invoiceDetail.Product.ProductCode,
                            UnitPrice = invoiceDetail.Price.Value,
                            Note = invoiceDetail.Note,
                            Quantity = invoiceDetail.Piece,
                            ExportDetailId = exportDetailId,
                            CurrencyCode = "",
                            Amount = invoiceDetail.Piece * invoiceDetail.Price.Value,
                        };
                        model.Add(entity);
                    }
                }
            }

            return model;
        }


        List<DeptReportModel> GetDeptReportList(int month, int year, int customerId, string customerCode) {
            var model = new List<DeptReportModel>();

            var inMonth = new DateTime(year, month, 1).AddMonths(1).AddSeconds(-1);
            var lastMonth = new DateTime(year, month, 1).AddSeconds(-1);
            try {
                using (var vfi = new tammaContext()) {
                    //StartTaxInvoiceDate = new DateTime(2016, 1, 1, 0, 0, 0).AddSeconds(-1)
                    //var customerId = 0;
                    var startTaxInvoice = MyUtilities.Sales.StartTaxInvoiceDate;
                    //if (month == 1 && year == 2018)
                    //{
                    //    startTaxInvoice = MyUtilities.Sales.StartTaxInvoiceDate;
                    //    //lastMonth = startTaxInvoice;
                    //}
                    var customers = (from c in vfi.Customers
                                     where c.State == (byte)MyUtilities.Sales.CustomerState.Active &&
                                           (customerId == 0 || c.CustomerId == customerId)
                                     //c.CustomerPayTypeId == payType.Id
                                     orderby c.ShortName
                                     select c).ToList();
                    if (customerId == 0 && !string.IsNullOrWhiteSpace(customerCode)) {
                        customers =
                            customers.Where(
                                    c => c.CustomerCode.Contains(customerCode) ||
                                        c.CustomerName.ToUpper().Contains(customerCode))
                                .ToList();
                    }
                    var customerIds = customers.Select(c => c.CustomerId).ToList();
                    var exportDetails = from epd in vfi.ExportFormTP_KDDetail
                                        where epd.InvoiceDetails.Any(id => id.Active)
                                              && customerIds.Contains(epd.ExportFormTP_KD.CustomerId.Value)
                                              && epd.ExportFormTP_KD.DateTransporter < inMonth
                                              && epd.ExportFormTP_KD.DateTransporter >= startTaxInvoice
                                              && epd
                                                  .ExportFormTP_KD
                                                  .Invoices
                                                  .FirstOrDefault(
                                                      i => i.Status != (byte)MyUtilities.Sales.Status.Cancel) != null
                                        select new {
                                            epd.DetailId,
                                            epd.ExportId,
                                            epd.ExportFormTP_KD.CustomerId,
                                            epd.InvoiceDetails,
                                            UnitPrice =
                                            epd.InvoiceDetails.FirstOrDefault(id => id.Active).Price,
                                            epd.InvoiceDetails.FirstOrDefault(id => id.Active)
                                                           .OrderDetail.Order.CurrencyCode,
                                            epd.Quality,
                                            epd.Product.ProductCode,
                                            epd.IsInvoiced,
                                            //epd.ExportFormTP_KD.Invoices.FirstOrDefault(/).TaxPercent,
                                            //epd.ExportFormTP_KD.Invoices.FirstOrDefault(/).ExchangeRate,
                                            epd.ExportFormTP_KD.DateTransporter,
                                            ExchangeRate =
                                            epd.InvoiceDetails.FirstOrDefault(id => id.Active)
                                                .Invoice.ExchangeRate,
                                            TaxPercent =
                                            epd.InvoiceDetails.FirstOrDefault(id => id.Active)
                                                .Invoice.TaxPercent,
                                            epd.TaxInvoiceProductDetails,
                                            TaxDate = epd.TaxInvoiceProductDetails.Any()
                                                ? epd.TaxInvoiceProductDetails.FirstOrDefault().TaxInvoice.SetupDate
                                                : lastMonth
                                        };
                    var taxInvoices = (from ti in vfi.TaxInvoices
                                       where ti.SetupDate != null &&
                                       customerIds.Contains(ti.CustomerId.Value) &&
                                           //(customerId == 0 || ti.CustomerId == customerId) &&
                                             ti.SetupDate < inMonth &&
                                             ti.SetupDate >= startTaxInvoice &&
                                             ti.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                             (ti.FinishDate == null || ti.FinishDate >= lastMonth)
                                       select new {
                                           ti.Id,
                                           ti.CustomerId,
                                           SetupDate = ti.SetupDate.Value,
                                           TotalAmount =
                                           ti.TaxInvoiceProducts.Sum(tid => tid.Quantity * tid.UnitPrice),
                                           TaxPercent = ti.TaxPercent,
                                           Currency = ti.Currency.Trim(),
                                           ExchangeRate = ti.ExchangeRate,
                                       }).ToList();
                    var taxInvoiceDetails = (from tid in vfi.TaxInvoiceDetails
                                             where tid.ImportDate < inMonth &&
                                             customerIds.Contains(tid.TaxInvoice.CustomerId.Value) &&
                                                 //(customerId == 0 || tid.TaxInvoice.CustomerId == customerId) &&
                                                   tid.ImportDate >= startTaxInvoice &&
                                                   tid.Status == (byte)MyUtilities.Sales.Status.Completed &&
                                                   (tid.TaxInvoice.FinishDate == null || tid.TaxInvoice.FinishDate >= lastMonth)
                                             select new {
                                                 tid.TaxInvoice.CustomerId,
                                                 MoneyValue = Math.Abs(tid.Money.Value),
                                                 tid.Money,
                                                 Currency = tid.TaxInvoice.Currency.Trim(),
                                                 tid.ImportDate,
                                                 ExchangeRate = tid.TaxInvoice.ExchangeRate
                                             }).ToList();
                    //var customerIds = taxInvoices.Select(ti => ti.CustomerId).Distinct().ToList();
                    //var index = 1;
                    foreach (var payType in vfi.CustomerPayTypes) {
                        var group = new DeptReportModel {
                            Details = new List<DeptReportDetailModel>(),
                            GroupCode = "",
                            GroupName = payType.TypeName,
                            ReportDate = inMonth.ToString("MM/yyyy")
                        };
                        var customersByType = from c in customers
                                              where c.CustomerPayTypeId == payType.Id
                                              orderby c.ShortName
                                              select c;
                        foreach (var customer in customersByType) {
                            var entity = group.Details
                                .FirstOrDefault(g => g.CustomerName.Equals(customer.ShortName));
                            if (entity == null) {
                                entity = new DeptReportDetailModel {
                                    CustomerName = customer.ShortName,
                                    CustomerId = customer.CustomerId,
                                    Month = month,
                                    Year = year,
                                    PayType = group.GroupName
                                };
                                group.Details.Add(entity);
                            }
                            bool isUsd = (customer.CustomerTypeId == 7 || customer.CustomerTypeId == 6);
                            #region xuat ban 4 5 6 7
                            var exportDetailsById =
                                exportDetails.Where(
                                    ed =>
                                    ed.CustomerId == customer.CustomerId &&
                                    ed.DateTransporter.Value.Month == month &&
                                    ed.DateTransporter.Value.Year == year);
                            foreach (var exportDetail in exportDetailsById) {
                                var detail = new InvoiceDetailTempModel {
                                    ProductCode = exportDetail.ProductCode,
                                    Quantity = exportDetail.Quality,
                                    UnitPrice = exportDetail.UnitPrice ?? 0,
                                    //CurrencyCode = exportDetail.CurrencyCode,
                                    DetailId = exportDetail.DetailId,
                                    ExportId = exportDetail.ExportId.Value,
                                    TaxPercent = exportDetail.TaxPercent,
                                    ExchangeRate = 1,
                                    Tax = 0,
                                    ExportedDate = exportDetail.DateTransporter.Value
                                };
                                if (isUsd) detail.CurrencyCode = "USD";
                                var taxInvoiceProduct =
                                    vfi.TaxInvoiceProductDetails.FirstOrDefault(
                                        tip => tip.ExportDetailId == exportDetail.DetailId && tip.Active == true);
                                // da xuat hoa don
                                if (taxInvoiceProduct != null) {
                                    detail.TaxPercent = taxInvoiceProduct.TaxInvoice.TaxPercent;
                                    detail.ExchangeRate = taxInvoiceProduct.TaxInvoice.ExchangeRate;
                                    detail.UnitPrice = taxInvoiceProduct.UnitPrice;
                                    detail.CurrencyCode = taxInvoiceProduct.TaxInvoice.Currency;
                                }
                                //xb tien usd - xhd tien vnd
                                else {
                                    detail.ExchangeRate = exportDetail.ExchangeRate;
                                    detail.TaxPercent = exportDetail.TaxPercent;
                                }
                                if (detail.CurrencyCode.Equals("VND")) {
                                    if (taxInvoiceProduct == null)
                                        detail.UnitPrice = Math.Round(detail.UnitPrice * detail.ExchangeRate, 0);
                                    detail.AmountVnd = detail.Quantity * detail.UnitPrice;
                                    detail.Tax = detail.AmountVnd * (((double)detail.TaxPercent) / 100);
                                }
                                else if (detail.CurrencyCode.Equals("USD")) {
                                    detail.Amount = detail.Quantity * detail.UnitPrice;
                                }
                                else {
                                    detail.Amount = detail.Quantity * detail.UnitPrice;
                                    detail.Note += " " + detail.CurrencyCode;
                                }
                                entity.ExportInMonths.Add(detail);
                            }
                            //5 xuat hoa don va chung tu trong thang
                            var taxInvoiceInMonth = taxInvoices.Where(ti =>
                                                                      ti.SetupDate.Month == month &&
                                                                      ti.SetupDate.Year == year &&
                                                                      ti.CustomerId == customer.CustomerId &&
                                                                      ti.Currency.Equals("VND")).ToList();
                            if (taxInvoiceInMonth.Any()) {
                                entity.InMonthTaxInvoiceVND +=
                                    taxInvoiceInMonth.Sum(ti => ti.TotalAmount * (1 + (((double)ti.TaxPercent) / 100)));
                            }
                            taxInvoiceInMonth = taxInvoices.Where(ti =>
                                                                  ti.SetupDate.Month == month &&
                                                                  ti.SetupDate.Year == year &&
                                                                  ti.CustomerId == customer.CustomerId &&
                                                                  ti.Currency.Equals("USD")).ToList();
                            if (taxInvoiceInMonth.Any()) {
                                //if (isUsd)
                                entity.InMonthTaxInvoiceUSD +=
                                    taxInvoiceInMonth.Sum(ti => ti.TotalAmount * (1 + (((double)ti.TaxPercent) / 100)));
                                //else
                                //    entity.InMonthTaxInvoiceVND +=
                                //        taxInvoiceInMonth.Sum(
                                //ti => ti.TotalAmount * ti.ExchangeRate * (1 + (((double)ti.TaxPercent) / 100)));
                            }
                            //6 thu tien trong thang
                            var receiveInMonth =
                                taxInvoiceDetails.Where(
                                    tid =>
                                    tid.CustomerId == customer.CustomerId && tid.Currency.Equals("VND") && tid.Money > 0 &&
                                    tid.ImportDate.Value.Month == month && tid.ImportDate.Value.Year == year)
                                                        .ToList();
                            if (receiveInMonth.Any())
                                entity.ReceiveInMonthVND += receiveInMonth.Sum(tid => tid.MoneyValue);

                            receiveInMonth =
                                taxInvoiceDetails.Where(
                                    tid =>
                                    tid.CustomerId == customer.CustomerId && tid.Currency.Equals("USD") && tid.Money > 0 &&
                                    tid.ImportDate.Value.Month == month && tid.ImportDate.Value.Year == year)
                                                        .ToList();
                            if (receiveInMonth.Any())
                                entity.ReceiveInMonthUSD += receiveInMonth.Sum(tid => tid.MoneyValue);
                            //7 giam tru doanh thu
                            receiveInMonth =
                                taxInvoiceDetails.Where(
                                    tid =>
                                    tid.CustomerId == customer.CustomerId && tid.Currency.Equals("VND") && tid.Money < 0 &&
                                    tid.ImportDate.Value.Month == month && tid.ImportDate.Value.Year == year)
                                                        .ToList();
                            if (receiveInMonth.Any())
                                entity.InMonthReduceVND += receiveInMonth.Sum(tid => tid.MoneyValue);
                            receiveInMonth =
                                taxInvoiceDetails.Where(
                                    tid =>
                                    tid.CustomerId == customer.CustomerId && tid.Currency.Equals("USD") && tid.Money < 0 &&
                                    tid.ImportDate.Value.Month == month && tid.ImportDate.Value.Year == year)
                                                        .ToList();
                            if (receiveInMonth.Any())
                                entity.InMonthReduceUSD += receiveInMonth.Sum(tid => tid.MoneyValue);
                            #endregion
                            //2 xuat hoa don va chung tu thang truoc
                            var taxInvoiceLastMonth =
                                taxInvoices.Where(ti =>
                                    ti.SetupDate <= lastMonth &&
                                    ti.CustomerId == customer.CustomerId &&
                                    ti.Currency.Equals("VND")).ToList();
                            if (taxInvoiceLastMonth.Any())
                                entity.LastMonthRequireVND +=
                                    taxInvoiceLastMonth.Sum(ti => ti.TotalAmount * (1 + (((double)ti.TaxPercent) / 100)));

                            //2 thu tien va giam tru thang truoc
                            receiveInMonth =
                                taxInvoiceDetails.Where(
                                    tid =>
                                        tid.CustomerId == customer.CustomerId &&
                                        tid.Currency.Equals("VND") &&
                                        tid.ImportDate <= lastMonth).ToList();
                            if (receiveInMonth.Any())
                                entity.LastMonthRequireVND -= receiveInMonth.Sum(tid => tid.MoneyValue);
                            //
                            taxInvoiceLastMonth = taxInvoices.Where(ti =>
                                ti.SetupDate <= lastMonth &&
                                ti.CustomerId == customer.CustomerId &&
                                ti.Currency.Equals("USD")).ToList();
                            if (taxInvoiceLastMonth.Any())
                                entity.LastMonthRequireUSD +=
                                    taxInvoiceLastMonth.Sum(ti => ti.TotalAmount * (1 + ((double)ti.TaxPercent) / 100));
                            //
                            receiveInMonth =
                                taxInvoiceDetails.Where(
                                    tid =>
                                    tid.CustomerId == customer.CustomerId &&
                                    tid.Currency.Equals("USD") &&
                                    tid.ImportDate <= lastMonth).ToList();
                            if (receiveInMonth.Any())
                                entity.LastMonthRequireUSD -= receiveInMonth.Sum(tid => tid.MoneyValue);
                            //1 so du dau thang chua xuat hoa don
                            exportDetailsById =
                                exportDetails.Where(
                                    ed =>
                                        ed.CustomerId == customer.CustomerId &&
                                        ed.DateTransporter <= lastMonth &&
                                        ed.TaxDate >= lastMonth);
                            foreach (var exportDetail in exportDetailsById) {
                                var detail = new InvoiceDetailTempModel {
                                    ProductCode = exportDetail.ProductCode,
                                    Quantity = exportDetail.Quality,
                                    UnitPrice = exportDetail.UnitPrice ?? 0,
                                    //CurrencyCode = exportDetail.CurrencyCode,
                                    DetailId = exportDetail.DetailId,
                                    ExportId = exportDetail.ExportId.Value,
                                    TaxPercent = exportDetail.TaxPercent,
                                    ExchangeRate = 1,
                                    Tax = 0,
                                    ExportedDate = exportDetail.DateTransporter.Value
                                };
                                if (isUsd) detail.CurrencyCode = "USD";
                                var taxInvoiceProduct =
                                    vfi.TaxInvoiceProductDetails.FirstOrDefault(
                                        tip => tip.ExportDetailId == exportDetail.DetailId && tip.Active == true);
                                // da xuat hoa don
                                if (taxInvoiceProduct != null) {
                                    detail.TaxPercent = taxInvoiceProduct.TaxInvoice.TaxPercent;
                                    detail.ExchangeRate = taxInvoiceProduct.TaxInvoice.ExchangeRate;
                                    detail.UnitPrice = taxInvoiceProduct.UnitPrice;
                                }
                                //xb tien usd - xhd tien vnd
                                else if (!exportDetail.CurrencyCode.Equals("VND")) {
                                    detail.ExchangeRate = exportDetail.ExchangeRate;
                                    detail.TaxPercent = exportDetail.TaxPercent;
                                }
                                if (detail.CurrencyCode.Equals("VND")) {
                                    if (taxInvoiceProduct == null)
                                        detail.UnitPrice = Math.Round(detail.UnitPrice * detail.ExchangeRate, 0);
                                    detail.AmountVnd = detail.Quantity * detail.UnitPrice;
                                    detail.Tax = detail.AmountVnd * (((double)detail.TaxPercent) / 100);
                                }
                                else if (detail.CurrencyCode.Equals("USD")) {
                                    detail.Amount = detail.Quantity * detail.UnitPrice;
                                }
                                else {
                                    detail.Amount = detail.Quantity * detail.UnitPrice;
                                    detail.Note += " " + detail.CurrencyCode;
                                }
                                entity.ExportLastMonths.Add(detail);
                            }
                            //2
                            if (!entity.Show)
                                group.Details.Remove(entity);
                        }

                        model.Add(group);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model;
        }




        [HttpPost]
        public ActionResult PrintDeptReport(
            int month, int year) {
            var model = new List<DeptReportModel>();
            try {
                model = GetDeptReportList(month, year, 0, "");
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return PartialView("PageDeptReport", model);
        }

        [GridAction]
        public ActionResult SelectDeptReport(int month, int year, string customerId, string customerCode) {
            var model = new List<DeptReportDetailModel>();
            try {
                var customer = 0;
                try {
                    customer = Convert.ToInt32(customerId);
                }
                catch (FormatException) { }
                var group = GetDeptReportList(month, year, customer, customerCode.ToUpper());
                foreach (var reportModel in group) {
                    model.AddRange(reportModel.Details.Where(d => d.Show));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDeptReport", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectExportDetailByCustomerId(
            int month, int year, int customerId) {

            var model = new List<InvoiceDetailTempModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    var customers =
                        vfi.Customers.Where(g => g.ShortName.Equals(customer.ShortName))
                           .Select(c => c.CustomerId)
                           .ToList();
                    bool isUsd = (customer.CustomerTypeId == 7 || customer.CustomerTypeId == 6);

                    var exportDetailsInMonth = (from epd in vfi.ExportFormTP_KDDetail
                                                where epd.InvoiceDetails.Any(id => id.Active)
                                                      &&
                                                      customers.Contains(epd.ExportFormTP_KD.CustomerId.Value)
                                                      && epd.ExportFormTP_KD.DateTransporter.Value.Month == month
                                                      && epd.ExportFormTP_KD.DateTransporter.Value.Year == year
                                                      && epd
                                                             .ExportFormTP_KD
                                                             .Invoices
                                                             .FirstOrDefault(
                                                                 i =>
                                                                 i.Status != (byte)MyUtilities.Sales.Status.Cancel) != null
                                                select new {
                                                    epd.DetailId,
                                                    epd.ExportId,
                                                    epd.ExportFormTP_KD.CustomerId,
                                                    epd.InvoiceDetails,
                                                    UnitPrice =
                                                epd.InvoiceDetails.FirstOrDefault(id => id.Active).Price,
                                                    epd.InvoiceDetails.FirstOrDefault(id => id.Active)
                                                                  .OrderDetail.Order.CurrencyCode,
                                                    epd.Quality,
                                                    epd.Product.ProductCode,
                                                    epd.IsInvoiced,
                                                    //epd.ExportFormTP_KD.Invoices.FirstOrDefault(/).TaxPercent,
                                                    //epd.ExportFormTP_KD.Invoices.FirstOrDefault(/).ExchangeRate,
                                                    epd.ExportFormTP_KD.DateTransporter,
                                                    epd.InvoiceDetails.FirstOrDefault(id => id.Active).Invoice.TaxPercent,
                                                    epd.InvoiceDetails.FirstOrDefault(id => id.Active).Invoice.ExchangeRate,
                                                }).ToList();
                    foreach (var exportDetail in exportDetailsInMonth) {
                        var detail = new InvoiceDetailTempModel {
                            ProductCode = exportDetail.ProductCode,
                            Quantity = exportDetail.Quality,
                            UnitPrice = exportDetail.UnitPrice ?? 0,
                            //CurrencyCode = exportDetail.CurrencyCode,
                            DetailId = exportDetail.DetailId,
                            ExportId = exportDetail.ExportId.Value,
                            TaxPercent = exportDetail.TaxPercent,
                            ExchangeRate = 1,
                            Tax = 0,
                            ExportedDate = exportDetail.DateTransporter.Value
                        };
                        if (isUsd) detail.CurrencyCode = "USD";
                        var taxInvoiceProduct =
                            vfi.TaxInvoiceProductDetails.FirstOrDefault(
                                tip => tip.ExportDetailId == exportDetail.DetailId && tip.Active == true);
                        // da xuat hoa don
                        if (taxInvoiceProduct != null) {
                            detail.TaxPercent = taxInvoiceProduct.TaxInvoice.TaxPercent;
                            detail.ExchangeRate = taxInvoiceProduct.TaxInvoice.ExchangeRate;
                            detail.UnitPrice = taxInvoiceProduct.UnitPrice;
                            detail.CurrencyCode = taxInvoiceProduct.TaxInvoice.Currency;
                        }
                        //xb tien usd - xhd tien vnd
                        else if (!exportDetail.CurrencyCode.Equals("VND")) {
                            detail.ExchangeRate = exportDetail.ExchangeRate;
                        }
                        if (detail.CurrencyCode.Equals("VND")) {
                            detail.UnitPrice = Math.Round(detail.UnitPrice * detail.ExchangeRate, 0);
                            detail.AmountVnd = detail.Quantity * detail.UnitPrice;
                            detail.Tax = detail.AmountVnd * (((double)detail.TaxPercent) / 100);
                        }
                        else if (detail.CurrencyCode.Equals("USD")) {
                            detail.Amount = detail.Quantity * detail.UnitPrice;
                        }
                        else {
                            detail.Amount = detail.Quantity * detail.UnitPrice;
                            detail.Note += " " + detail.CurrencyCode;
                        }
                        model.Add(detail);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectExportDetailByCustomerId", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.ExportedDate).ThenBy(m => m.ProductCode)));
        }
        [GridAction]
        public ActionResult SelectTaxInvoicesDetailByCustomerId(int month, int year, int customerId) {
            var model = new List<TaxInvoiceDetailModel>();
            try {
                using (var vfi = new tammaContext()) {

                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    var customers =
                        vfi.Customers.Where(g => g.ShortName.Equals(customer.ShortName))
                           .Select(c => c.CustomerId)
                           .ToList();
                    var taxInvoiceDetail =
                        vfi.TaxInvoiceDetails.Where(tid =>
                                                    customers.Contains(tid.TaxInvoice.CustomerId.Value)
                                                    && tid.Status == (byte)MyUtilities.Transaction.Status.Approved
                                                    && tid.ImportDate.Value.Month == month
                                                    && tid.ImportDate.Value.Year == year);
                    foreach (var detail in taxInvoiceDetail) {
                        var entity = new TaxInvoiceDetailModel {
                            Money = detail.Money,
                            TaxInvoiceList = detail.TaxInvoice.TaxInvoiceList,
                            ModifiedDate = detail.ModifiedDate.Value,
                            ModifiedUser = detail.ModifiedUser,
                            Times = detail.Times,
                            StatusName = CastTaxInvoiceStatusEnumDomain.GetText(detail.Status ?? 1),
                            Note = detail.Note,
                            ImportDate = detail.ImportDate.Value,
                            CurrencyCode = detail.TaxInvoice.Currency
                        };
                        entity.TypeDetail = entity.Money > 0 ? "Thanh toán" : "Giảm trừ";
                        entity.Money = Math.Abs(entity.Money ?? 0);
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ProductCode", "" + ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectTaxInvoicesByCustomerId(int month, int year, int customerId) {
            var model = new List<TaxInvoiceModel>();
            try {
                using (var vfi = new tammaContext()) {

                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    var customers =
                        vfi.Customers.Where(g => g.ShortName.Equals(customer.ShortName))
                           .Select(c => c.CustomerId)
                           .ToList();
                    var taxInvoices =
                        vfi.TaxInvoices.Where(
                            ti =>
                            customers.Contains(ti.CustomerId.Value) &&
                            ti.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                            ti.SetupDate.Value.Month == month &&
                            ti.SetupDate.Value.Year == year).ToList();
                    foreach (var taxInvoice in taxInvoices) {
                        //if (!taxInvoice.TaxInvoiceProductDetails.Any(tip => tip.Active == true)) continue;
                        //mới
                        var entity = new TaxInvoiceModel {
                            TaxInvoiceList = taxInvoice.TaxInvoiceList,
                            TaxInvoiceId = taxInvoice.Id,
                            ModifiedDate = taxInvoice.ModifiedDate ?? DateTime.Now,
                            Tax = taxInvoice.TaxPercent,
                            ExchangeRate = taxInvoice.ExchangeRate,
                            CurrencyCode = taxInvoice.Currency,
                            SetupDate = taxInvoice.SetupDate,
                            CustomerCode = taxInvoice.Customer.CustomerCode,
                            TotalQuantity = taxInvoice.TaxInvoiceProducts.Sum(tip => tip.Quantity),
                            TotalAmount = taxInvoice.TaxInvoiceProducts.Sum(tip => Math.Round(tip.Quantity * tip.UnitPrice, 2))
                        };
                        entity.StatusName = CastTaxInvoiceStatusEnumDomain2.GetText(taxInvoice.Status.Value);
                        entity.TotalAmount += (entity.TotalAmount * taxInvoice.TaxPercent / 100);
                        //}
                        entity.RequiredAmount = entity.TotalAmount ?? 0;
                        var taxInvoiceDetailAdd =
                            vfi.TaxInvoiceDetails.Where(
                                tid =>
                                tid.TaxInvoiceId == taxInvoice.Id && tid.Money > 0 &&
                                tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        if (taxInvoiceDetailAdd.Any())
                            entity.RequiredAmount -= (taxInvoiceDetailAdd.Sum(tid => tid.Money) ?? 0.0);
                        var taxInvoiceDetailReduce =
                            vfi.TaxInvoiceDetails.Where(
                                tid =>
                                tid.TaxInvoiceId == taxInvoice.Id && tid.Money < 0 &&
                                tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        if (taxInvoiceDetailReduce.Any())
                            entity.RequiredAmount += (taxInvoiceDetailReduce.Sum(tid => tid.Money) ?? 0.0);
                        //entity.RequiredAmount -= total;
                        entity.TotalAmountVND = (entity.TotalAmount ?? 0) * entity.ExchangeRate;
                        entity.TotalAmountVND =
                            (double)Math.Round((decimal)(entity.TotalAmountVND));
                        entity.RequiredAmountVND = entity.RequiredAmount * entity.ExchangeRate;
                        entity.RequiredAmountVND =
                            (double)Math.Round((decimal)(entity.RequiredAmountVND));
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ProductCode", "" + ex.Message);
            }
            return View(new GridModel(model));
        }
        [GridAction]
        public ActionResult SelectManageOrders(
            string productCode
            //, int warehouseIssue, int warehouseReceipt
            , int? employeeId
            , string fromDate, string toDate, byte? active, byte? status, bool chkAll) {
            //var warehouseId = eoi ? warehouseIssue : warehouseReceipt;
            var ci = new CultureInfo("vi-VN");
            DateTime toDay = DateTime.Now;

            var fDate = string.IsNullOrWhiteSpace(fromDate)
                            ? new DateTime(toDay.Year, toDay.Month, 1)
                            : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                            ? toDay
                            : Convert.ToDateTime(toDate, ci);

            // return View(new GridModel(new List<ManageOrderModel>()));

            var models = new List<ManageOrderModel>();
            if (active != null && active != 0) {
                using (var vfi = new tammaContext()) {
                    //var orders = vfi.Orders.Where(o=> o.OrderDetails.Any()).ToList();
                    var orders = (from x in vfi.Orders
                                  where x.OrderDetails.Any()
                                  select new {
                                      x.CustomerId,
                                      x.Customer.CustomerCode,
                                      x.Customer.Area.AreaName,
                                      x.OrderId,
                                      x.OrderNumber,
                                      x.OrderDate,
                                      x.CurrencyCode,
                                      x.Employee.EmployeeName,
                                      x.Active,
                                      x.DueDate,
                                      x.Status,
                                      x.ModifiedDate,
                                      x.ModifiedUser,
                                      OrderDetails = x.OrderDetails.Select(y => new { 
                                          y.OrderDetailId,
                                          y.RequiedNumber, y.OrderQty, y.UnitPrice, y.ProductId }),
                                      x.Note,
                                      //ProductCodes = x.OrderDetails.Select(y => y.Product.ProductCode).Distinct().ToList()
                                  }).ToList();
                    if (!chkAll) {
                        switch (active) {
                            case 1:
                                orders = orders.Where(o => o.Active && o.DueDate == null).ToList();
                                break;
                            case 2:
                                orders =
                                    orders.Where(o => o.Active && o.DueDate >= fDate && o.DueDate <= tDate).ToList();
                                if (status == 0 || status == 1)
                                    orders =
                                        orders.Where(
                                            o =>
                                            (o.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                             o.Status == (byte)MyUtilities.Sales.Status.InProcess)).ToList();
                                else {
                                    orders =
                                        orders.Where(
                                            o => o.Status == status && o.DueDate.Value >= fDate && o.DueDate.Value <= tDate)
                                              .ToList();
                                }
                                break;
                            case 3:
                                orders =
                                    orders.Where(
                                        o =>
                                        o.Active == false && o.DueDate == null &&
                                        o.ModifiedDate.Value >= fDate && o.ModifiedDate.Value <= tDate).ToList();
                                break;

                        }
                    }
                    else {
                        switch (active) {
                            case 1:
                                orders = orders.Where(o => o.Active && o.DueDate == null).ToList();
                                break;
                            case 2:
                                orders =
                                    orders.Where(o => o.Active && o.DueDate != null).ToList();
                                if (status == 0 || status == 1)
                                    orders =
                                        orders.Where(
                                            o =>
                                            (o.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                             o.Status == (byte)MyUtilities.Sales.Status.InProcess)).ToList();
                                else {
                                    orders =
                                        orders.Where(
                                            o => o.Status == status && o.DueDate.Value >= fDate && o.DueDate.Value <= tDate)
                                              .ToList();
                                }
                                break;
                            case 3:
                                orders =
                                    orders.Where(
                                        o =>
                                        o.Active == false && o.DueDate == null &&
                                        o.ModifiedDate.Value >= fDate && o.ModifiedDate.Value <= tDate).ToList();
                                break;

                        }
                    }
                    if (!string.IsNullOrWhiteSpace(productCode)) {
                        var product = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(productCode));
                        if (product != null) {
                            orders = orders.Where(o => o.OrderDetails.Any(od => od.ProductId == product.ProductId))
                                            .ToList();
                        }
                    }
                    var isProductionManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.ProductionManagement);
                    foreach (var order in orders) {
                        var entity = new ManageOrderModel {
                            OrderId = order.OrderId,
                            SalesPersonName = order.EmployeeName,
                            DueDate = order.DueDate,
                            OrderNumber = order.OrderNumber,
                            ModifiedDate = order.ModifiedDate,
                            Status = order.Status,
                            StatusName = MyUtilities.Sales.GetText(order.Status),
                            Note = order.Note,
                            CurrencyCode = order.CurrencyCode,
                            CustomerCode = order.CustomerCode,
                            Area = order.AreaName,
                            OrderDate = order.OrderDate,
                            CanCreateWorkOrder = false
                        };
                        entity.TotalQuality = order.OrderDetails.Sum(od => od.OrderQty) ?? 0;
                        entity.TotalPrice = order.OrderDetails.Sum(od => od.OrderQty * od.UnitPrice) ?? 0.0;
                        entity.TotalRequired = order.OrderDetails.Sum(od => od.RequiedNumber);
                        entity.TotalRequiredPrice = order.OrderDetails.Sum(od => od.RequiedNumber * od.UnitPrice);
                        if (entity.Status != (byte)MyUtilities.Sales.Status.Cancel
                            && entity.Status != (byte)MyUtilities.Sales.Status.Completed
                            && entity.DueDate != null) {
                            var orderDetailIds = order.OrderDetails.Select(x => x.OrderDetailId).ToList();
                            if (vfi.WorkOrders.Any(x => orderDetailIds.Contains(x.OrderDetailId)&&
                                x.Status!= (byte) MyUtilities.WorkOrder.Status.Cancel)) { }
                            else {
                                entity.CanCreateWorkOrder = isProductionManager;
                            }
                        }
                        models.Add(entity);
                    }
                }
            }
            return View(new GridModel(models.OrderBy(o => o.DueDate)));
        }


        [GridAction]
        public ActionResult SelectOrderInfo(
          int orderId) {

            var model = new List<OrderModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                    if (order != null) {
                        var shipMethod = vfi.ShipMethods.FirstOrDefault(sm => sm.ShipMethodId == order.ShipMethodId);
                        var payment = vfi.PaymentTerms.FirstOrDefault(pt => pt.Id == order.PaymentTermId);
                        var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
                        var entity = new OrderModel {
                            OrderId = orderId,
                            BillToAddress = customer.Address,
                            //ShipToAddress = order.ShipToAddress,
                            ShipMethodName = shipMethod.Name,
                            PaymentMethodName = payment.TermName,
                            Note = order.Note
                        };
                        model.Add(entity);
                    }
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectOrderInfo", ex.Message);
            }
            return View(new GridModel(model));
        }


        [GridAction]
        public ActionResult SelectInvoiceDetailByExportId(int exportId) {
            var model = new List<InvoiceDetailTempModel>();
            try {
                model = InvoiceDetailListByExportId(exportId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDetailExport", ex.Message);
            }
            return View(new GridModel(model));
        }


        [GridAction]
        public ActionResult SelectInvoiceDetailByExportId_New(int exportId, long invoiceId) {
            var model = new List<InvoiceDetailTempModel>();
            try {
                model = InvoiceDetailListByExportId_New(exportId, invoiceId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDetailExport_New", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectDetailByExportId(int exportId) {
            var model = new List<InvoiceDetailTempModel>();
            try {
                model = GetDetailByExportId(exportId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDetailByTransactionCode", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectMaterialCertificateByExportId(int exportId) {
            var model = new List<InvoiceDetailTempModel>();
            try {
                model = GetMaterialCertificateByExportId(exportId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDetailByTransactionCode", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectInvoiceDetailByExportDetailId(int exportDetailId) {
            var model = new List<InvoiceDetailTempModel>();
            try {
                model = InvoiceDetailListByExportDetailId(exportDetailId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDetailExport", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectProductDetailInTaxInvoice(int taxInvoiceId) {
            var model = new List<InvoiceDetailTempModel>();
            try {
                model = TaxInvoiceProductDetailByExportId(taxInvoiceId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectDetailExport", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<InvoiceDetailTempModel> TaxInvoiceProductDetailByExportId(int taxInvoiceId) {
            var model = new List<InvoiceDetailTempModel>();
            using (var vfi = new tammaContext()) {
                var productDetails =
                    vfi.TaxInvoiceProductDetails.Where(e => e.TaxInvoiceId == taxInvoiceId && e.Active == true);
                var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == taxInvoiceId);
                foreach (var detail in productDetails) {
                    var exportDetail =
                        vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == detail.ExportDetailId);
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.ExportId == exportDetail.ExportId);
                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == invoice.OrderId);
                    var entity = new InvoiceDetailTempModel {
                        DetailId = detail.PDetailId,
                        ProductCode = exportDetail.Product.ProductCode,
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice,
                        ExportedDate = detail.ExportDate ?? DateTime.Now,
                        InvoiceNumber = invoice.InvoiceNumber,
                        CurrencyCode = taxInvoice.Currency
                    };
                    entity.Amount = entity.Quantity * entity.UnitPrice;

                    var taxInvoiceProduct =
                        vfi.TaxInvoiceProductDetails.FirstOrDefault(tipd => tipd.ExportDetailId == entity.DetailId && tipd.Active == true);
                    if (taxInvoiceProduct != null) {
                        entity.TaxInvoiceList = taxInvoiceProduct.TaxInvoice.TaxInvoiceList;
                    }
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.ProductCode)
                        .ThenBy(m => m.ExportedDate)
                        .ThenBy(m => m.InvoiceNumber)
                        .ToList();
        }



        [GridAction]
        public ActionResult CancelProductTaxInvoiceDetailByExportDetailId(int detailId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var productDetail = vfi.TaxInvoiceProductDetails.FirstOrDefault(tip => tip.PDetailId == detailId);
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == productDetail.TaxInvoiceId);
                    if (taxInvoice.Status != (byte)MyUtilities.Sales.Status.Waiting)
                        throw new AggregateException(
                            "Chứng từ này đã được thay đổi ! Vui lòng F5 Refesh lại để có danh sách mới nhất !");
                    productDetail.Active = false;
                    if (!taxInvoice.TaxInvoiceProductDetails.Any(ti => ti.Active == true))
                        taxInvoice.Status = (byte)MyUtilities.Sales.Status.Cancel;
                    var exportDetail =
                        vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == productDetail.ExportDetailId);
                    exportDetail.IsInvoiced = false;
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.ExportId == exportDetail.ExportId);
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == exportDetail.ExportId);
                    if (!export.ExportFormTP_KDDetail.Any(ti => ti.IsInvoiced == true))
                        invoice.Status = (byte)MyUtilities.Sales.Status.Waiting;
                    else
                        invoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                    vfi.SaveChanges();
                    return View(new GridModel(TaxInvoiceProductDetailByExportId(productDetail.TaxInvoiceId.Value)));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CancelTaxInvoice", ex.Message);
            }
            return View(new GridModel(new List<InvoiceDetailTempModel>()));
        }

        [GridAction]
        public ActionResult SelectPrepareInvoice() {
            return View(new GridModel(PrepareInvoiceTemp("").Where(i => i.Active).OrderByDescending(o => o.ModifiedDate)));
        }

        [GridAction]
        public ActionResult SelectPrepareInvoice_New(int month, int year) {
            return View(new GridModel(PrepareInvoiceTemp_New("", month, year).OrderByDescending(o => o.ModifiedDate)));
        }

        [GridAction]
        public ActionResult SelectApprovedInvoice(string fromDate, string toDate, int? status, string currency) {
            var ci = new CultureInfo("vi-VN");
            DateTime toDay = DateTime.Now;

            var fDate = string.IsNullOrWhiteSpace(fromDate)
                            ? new DateTime(toDay.Year, toDay.Month, 1, 0, 0, 0)
                            : Convert.ToDateTime(fromDate, ci);
            //fDate = new DateTime(fDate.Year, fDate.Month, fDate.Day, 0, 0, 0);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                            ? toDay
                            : Convert.ToDateTime(toDate, ci);

            var model = new List<InvoiceTempModel>();
            if (status == 0 || status == null)
                return View(new GridModel(model));
            using (var vfi = new tammaContext()) {
                var invoices = new List<Invoice>();
                if (status != 1)
                    invoices = vfi.Invoices.Where(i => i.Status == (byte)status).ToList();
                else
                    invoices = vfi.Invoices.Where(i => i.Status == 1 || i.Status == 4).ToList();
                foreach (var invoice in invoices) {
                    //var a = 5;
                    //if (invoice.InvoiceId == 247)
                    //    a = 6;
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                    if (export.DateTransporter < fDate || export.DateTransporter > tDate) continue;
                    var order = vfi.Orders.FirstOrDefault(o => o.OrderId == invoice.OrderId);
                    if (!order.CurrencyCode.Equals(currency)) continue;
                    //var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
                    //var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == invoice.TaxInvoiceId);
                    var entity = new InvoiceTempModel {
                        InvoiceNumber = invoice.InvoiceNumber,
                        InvoiceId = invoice.InvoiceId,
                        ShiftmentDate = export.DateTransporter != null ? export.DateTransporter.Value : DateTime.Now,
                        ExportId = invoice.ExportId.Value,
                        OrderId = invoice.OrderId.Value,
                        OrderNumber = order.OrderNumber ?? "",
                        PoNumber = order.PoNumber ?? "",
                        BillToAddress = order.BillToAddress ?? "",
                        CurrencyCode = order.CurrencyCode ?? "",
                        ShipToAddress = order.ShipToAddress ?? "",
                        CustomerId = order.CustomerId,
                        CustomerCode = order.Customer.CustomerCode,
                        ModifiedDate = invoice.ModifiedDate ?? DateTime.Now,
                        ModifiedUser = invoice.ModifiedUser ?? "",
                        TaxPercent = invoice.TaxPercent,
                        TaxInvoice = "",
                        Note = invoice.Note,
                        ExchangeRate = invoice.ExchangeRate,
                        EmployeeSale = order.Employee.EmployeeName,
                    };
                    var orderNotes = vfi.OrderNotes.Where(on => on.InvoiceId == invoice.InvoiceId && on.NoteType == 1);
                    var total = 0.0;
                    foreach (var exportFormTpKdDetail in export.ExportFormTP_KDDetail) {
                        var orderDetail =
                            order.OrderDetails.FirstOrDefault(od => od.ProductId == exportFormTpKdDetail.ProductId);
                        if (orderDetail == null) {
                            ModelState.AddModelError("invoice error!",
                                                     "orderdetail null " + invoice.InvoiceNumber);
                        }
                        else {
                            total += ((exportFormTpKdDetail.Quality) * orderDetail.UnitPrice);
                            entity.TotalQuantity += (exportFormTpKdDetail.Quality);
                            var orderNotesByProductId =
                                orderNotes.Where(
                                    on =>
                                    on.OrderNoteDetails.FirstOrDefault(
                                        ond => ond.ProductId == exportFormTpKdDetail.ProductId) != null);
                            if (orderNotesByProductId.Any()) {
                                foreach (var orderNote in orderNotesByProductId) {
                                    var transaction =
                                        vfi.Transactions.FirstOrDefault(
                                            t =>
                                            t.TransactionId == orderNote.TransactionId &&
                                            t.Status == (byte)MyUtilities.Transaction.Status.Approved);
                                    if (transaction != null) {
                                        var orderNoteDetail =
                                            vfi.OrderNoteDetails.FirstOrDefault(
                                                ond =>
                                                ond.NoteId == orderNote.NoteId &&
                                                ond.ProductId == exportFormTpKdDetail.ProductId);
                                        if (orderNoteDetail != null) {
                                            total -= ((orderNoteDetail.Quantity.Value) * orderDetail.UnitPrice);
                                            entity.TotalQuantity -= orderNoteDetail.Quantity.Value;
                                        }
                                    }
                                }
                            }
                        }
                        if (exportFormTpKdDetail.IsInvoiced ?? false) {
                            var taxInvoiceProduct =
                                vfi.TaxInvoiceProductDetails.FirstOrDefault(
                                    tipd => tipd.ExportDetailId == exportFormTpKdDetail.DetailId && tipd.Active == true);
                            if (taxInvoiceProduct != null)
                                if (!entity.TaxInvoice.Contains(taxInvoiceProduct.TaxInvoice.TaxInvoiceList))
                                    entity.TaxInvoice += (taxInvoiceProduct.TaxInvoice.TaxInvoiceList + " ");
                        }
                    }

                    entity.TotalAmount = total;
                    entity.TotalAmountAfterTax = ((total * entity.TaxPercent / 100) + total);
                    entity.TotalAmountAfterTax = (double)Math.Round((decimal)entity.TotalAmountAfterTax, 2);
                    entity.TotalAmountAfterTaxVND = entity.TotalAmountAfterTax * entity.ExchangeRate;
                    entity.TotalAmountAfterTaxVND = (double)Math.Round((decimal)entity.TotalAmountAfterTaxVND);
                    model.Add(entity);
                }
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectApprovedInvoice_New(string fromDate, string toDate, int? status, string currency) {
            var ci = new CultureInfo("vi-VN");
            DateTime toDay = DateTime.Now;

            var fDate = string.IsNullOrWhiteSpace(fromDate)
                            ? new DateTime(toDay.Year, toDay.Month, 1, 0, 0, 0)
                            : Convert.ToDateTime(fromDate, ci);
            //fDate = new DateTime(fDate.Year, fDate.Month, fDate.Day, 0, 0, 0);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                            ? toDay
                            : Convert.ToDateTime(toDate, ci);

            var model = new List<InvoiceTempModel>();
            if (status == 0 || status == null)
                return View(new GridModel(model));
            using (var vfi = new tammaContext()) {
                var invoices = vfi.Invoices.Where(i => i.ShipmentDate >= fDate && i.ShipmentDate <= tDate).ToList();
                if (status != 1)
                    invoices = invoices.Where(i => i.Status == (byte)status).ToList();
                else
                    invoices = invoices.Where(i => i.Status == 1 || i.Status == 4).ToList();
                foreach (var invoice in invoices) {
                    //var a = 5;
                    //if (invoice.InvoiceId == 247)
                    //    a = 6;
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                    //if (export.DateTransporter < fDate || export.DateTransporter > tDate) continue;
                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == invoice.OrderId);
                    //if (!order.CurrencyCode.Equals(currency)) continue;
                    //var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
                    //var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == invoice.TaxInvoiceId);
                    var entity = new InvoiceTempModel {
                        InvoiceNumber = invoice.InvoiceNumber,
                        InvoiceId = invoice.InvoiceId,
                        ShiftmentDate = export.DateTransporter != null ? export.DateTransporter.Value : DateTime.Now,
                        ExportId = invoice.ExportId.Value,
                        //OrderId = invoice.OrderId.Value,
                        //OrderNumber = order.OrderNumber ?? "",
                        //PoNumber = order.PoNumber ?? "",
                        //BillToAddress = order.BillToAddress ?? "",
                        //CurrencyCode = order.CurrencyCode ?? "",
                        //ShipToAddress = order.ShipToAddress ?? "",
                        CustomerId = export.CustomerId.Value,
                        CustomerCode = export.Customer.CustomerCode,
                        ModifiedDate = invoice.ModifiedDate ?? DateTime.Now,
                        ModifiedUser = invoice.ModifiedUser ?? "",
                        TaxPercent = invoice.TaxPercent,
                        TaxInvoice = "",
                        Note = invoice.Note,
                        ExchangeRate = invoice.ExchangeRate,
                        TransactionCode = export.TransactionCode,
                        //EmployeeSale = order.Employee.EmployeeName,
                    };
                    foreach (var exportDetail in export.ExportFormTP_KDDetail) {
                        if (exportDetail.InvoiceDetails != null && exportDetail.InvoiceDetails.Any(id => id.Active)) {
                            entity.TotalQuantity += exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece);
                            entity.TotalAmount += (exportDetail.InvoiceDetails.Where(id => id.Active).Sum(id => id.Piece) *
                                                   exportDetail.InvoiceDetails.FirstOrDefault(id => id.Active).Price.Value);
                        }
                        else {
                            entity.TotalQuantity += (exportDetail.Quality);
                        }

                        if (exportDetail.IsInvoiced ?? false) {
                            var taxInvoiceProduct =
                                vfi.TaxInvoiceProductDetails.FirstOrDefault(
                                    tipd => tipd.ExportDetailId == exportDetail.DetailId && tipd.Active == true);
                            if (taxInvoiceProduct != null) {
                                if (!entity.TaxInvoice.Contains(taxInvoiceProduct.TaxInvoice.TaxInvoiceList))
                                    entity.TaxInvoice += (taxInvoiceProduct.TaxInvoice.TaxInvoiceList + " ");
                                entity.ExchangeRate = taxInvoiceProduct.TaxInvoice.ExchangeRate;
                                entity.TaxPercent = taxInvoiceProduct.TaxInvoice.TaxPercent;
                                entity.CurrencyCode = taxInvoiceProduct.TaxInvoice.Currency;
                            }
                        }
                    }
                    entity.TotalAmountAfterTax = ((entity.TotalAmount * entity.TaxPercent / 100) + entity.TotalAmount);
                    entity.TotalAmountAfterTax = (double)Math.Round((decimal)entity.TotalAmountAfterTax, 2);
                    entity.TotalAmountAfterTaxVND = entity.TotalAmountAfterTax * entity.ExchangeRate;
                    entity.TotalAmountAfterTaxVND = (double)Math.Round((decimal)entity.TotalAmountAfterTaxVND);
                    model.Add(entity);
                }
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult UpdateInvoice(int invoiceId, int taxPercent, string note, int exchangeRate) {

            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
                    if (invoice == null) throw new AggregateException("Lỗi Invoice");
                    invoice.Note = note;
                    //if (invoice.TaxPercent != taxPercent || invoice.ExchangeRate != exchangeRate)
                    //{

                    //    if (invoice.Status == (byte) MyUtilities.Sales.Status.Waiting)
                    //    {
                    invoice.TaxPercent = taxPercent;
                    invoice.ExchangeRate = exchangeRate;
                    //    }
                    //    else
                    //    {
                    //        ModelState.AddModelError("ApproveInvoice", "Invoice đã có xuất chứng từ không thể thay đổi");
                    //    }
                    //}
                    vfi.SaveChanges();
                    return
                           View(
                               new GridModel(
                                   PrepareInvoiceTemp_New("", invoice.ShipmentDate.Value.Month,
                                                          invoice.ShipmentDate.Value.Year)
                                       .OrderByDescending(o => o.ModifiedDate)));

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateInvoice", ex.Message);
            }
            return
                View(
                    new GridModel(
                        PrepareInvoiceTemp_New("", DateTime.Now.Month, DateTime.Now.Year)
                            .OrderByDescending(o => o.ModifiedDate)));
        }

//        [GridAction]
//        public ActionResult DeleteInvoice(int invoiceId, string note) {
//            try {
//                if (!Request.IsAuthenticated) {
//                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
//                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
//                                         Xin vui lòng đăng nhập lại hệ thống.");
//                }
//                using (var vfi = new tammaContext()) {
//                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
//                    if (invoice == null || invoice.Active == false)
//                        return View(new GridModel(PrepareInvoiceTemp("").OrderByDescending(o => o.ModifiedDate)));
//                    if (invoice.Status == (byte)MyUtilities.Sales.Status.InProcess) {
//                        throw new AggregateException("Invoice đã có xuất hóa đơn không thể hủy !");
//                    }
//                    if (invoice.Status != (byte)MyUtilities.Sales.Status.Waiting) {
//                        throw new AggregateException("Lỗi lệnh ! Vui lòng F5 để Refresh");
//                    }
//                    invoice.Note = note;
//                    invoice.Active = false;
//                    invoice.Status = (byte)MyUtilities.Sales.Status.Cancel;
//                    //roll back
//                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
//                    var order = vfi.Orders.FirstOrDefault(o => o.OrderId == export.OrderId);
//                    var transaction =
//                        vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(export.TransactionCode));
//                    if (transaction != null) {
//                        //giao dich
//                        transaction.Status = (byte)MyUtilities.Transaction.Status.Open;
//                        foreach (var exportDetail in export.ExportFormTP_KDDetail) {
//                            //ton kho
//                            var productInventoryIssue =
//                                vfi.ProductInventories.FirstOrDefault(
//                                    pi => pi.ProductId == exportDetail.ProductId && pi.WarehouseId == MyUtilities.Warehouse.Finish);
//                            var productInventoryReceipt =
//                                vfi.ProductInventories.FirstOrDefault(
//                                    pi => pi.ProductId == exportDetail.ProductId && pi.WarehouseId == MyUtilities.Warehouse.Business);

//                            productInventoryIssue.TotalQty += exportDetail.Quality;
//                            productInventoryReceipt.TotalQty -= exportDetail.Quality;
//                            // luan chuyen
//                            var productInventoryPeriods =
//                                vfi.ProductInventoryPeriods.Where(pip => pip.TransactionId == transaction.TransactionId);
//                            vfi.ProductInventoryPeriods.RemoveRange(productInventoryPeriods);
//                            //don hang
//                            var orderDetail =
//                                order.OrderDetails.FirstOrDefault(od => od.ProductId == exportDetail.ProductId);
//                            orderDetail.RequiedNumber += MyUtilities.Function.RoundUp(exportDetail.Quality);
//                            orderDetail.IsComplete = false;
//                        }
//                    }
//                    if (order.OrderDetails.Count(od => od.OrderQty == od.RequiedNumber) == order.OrderDetails.Count()) {
//                        order.Status = (byte)MyUtilities.Sales.Status.Waiting;
//                    }
//                    else {
//                        order.Status = (byte)MyUtilities.Sales.Status.InProcess;
//                    }
//                    vfi.SaveChanges();
//                }
//            }
//            catch (Exception ex) {
//                ModelState.AddModelError("DeleteInvocie", ex.Message);
//            }
//            return View(new GridModel(PrepareInvoiceTemp("").Where(i => i.Active).OrderByDescending(o => o.ModifiedDate)));
//        }

//        [GridAction]
//        public ActionResult DeleteInvoice_New(int invoiceId, string note) {
//            try {
//                if (!Request.IsAuthenticated) {
//                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
//                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
//                                         Xin vui lòng đăng nhập lại hệ thống.");
//                }
//                using (var vfi = new tammaContext()) {
//                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
//                    if (invoice == null || invoice.Active == false)
//                        return View(new GridModel(PrepareInvoiceTemp("").OrderByDescending(o => o.ModifiedDate)));
//                    if (invoice.Status == (byte)MyUtilities.Sales.Status.InProcess) {
//                        throw new AggregateException("Invoice đã có xuất hóa đơn không thể hủy !");
//                    }
//                    if (invoice.Status != (byte)MyUtilities.Sales.Status.Waiting) {
//                        throw new AggregateException("Lỗi lệnh ! Vui lòng F5 để Refresh");
//                    }
//                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, invoice.ShipmentDate.Value)) {
//                        throw new AggregateException(
//                            @"Không có quyền huỷ phiếu xuất tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
//                    }
//                    //roll back
//                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
//                    var exportDetailCheck = export.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.InvoiceDetails.Any(id => id.Active));
//                    if (exportDetailCheck != null)
//                        throw new AggregateException("Lỗi! Vui lòng hủy hết lệnh phân đơn hàng trước khi hủy lệnh xuất");
//                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == export.OrderId);
//                    var transaction =
//                        vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(export.TransactionCode));
//                    if (transaction != null) {
//                        //giao dich
//                        transaction.Status = (byte)MyUtilities.Transaction.Status.Open;
//                        foreach (var exportDetail in export.ExportFormTP_KDDetail) {
//                            //ton kho
//                            var productInventoryIssue =
//                                vfi.ProductInventories.FirstOrDefault(
//                                    pi => pi.ProductId == exportDetail.ProductId && pi.WarehouseId == MyUtilities.Warehouse.Finish);
//                            var productInventoryReceipt =
//                                vfi.ProductInventories.FirstOrDefault(
//                                    pi => pi.ProductId == exportDetail.ProductId && pi.WarehouseId == MyUtilities.Warehouse.Business);

//                            productInventoryIssue.TotalQty += exportDetail.Quality;
//                            productInventoryReceipt.TotalQty -= exportDetail.Quality;
//                        }
//                        // tra lai luan chuyen
//                        var productInventoryPeriods =
//                            vfi.ProductInventoryPeriods.Where(pip => pip.TransactionId == transaction.TransactionId);
//                        vfi.ProductInventoryPeriods.RemoveRange(productInventoryPeriods);
//                        var orderNoteDetails =
//                            vfi.OrderNoteDetails.Where(
//                                ond =>
//                                ond.OrderNote.InvoiceId == invoiceId &&
//                                ond.OrderNote.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
//                        if (orderNoteDetails.Any()) {
//                            var transactionIds = orderNoteDetails.Select(ond => ond.OrderNote.TransactionId).ToList();
//                            var productInventoryPeriods2 =
//                                vfi.ProductInventoryPeriods.Where(pip => transactionIds.Contains(pip.TransactionId));
//                            vfi.ProductInventoryPeriods.RemoveRange(productInventoryPeriods2);
//                        }
//                    }
//                    invoice.Note = note;
//                    invoice.Active = false;
//                    invoice.Status = (byte)MyUtilities.Sales.Status.Cancel;
//                    //foreach(var
//                    vfi.SaveChanges();
//                    return
//                        View(
//                            new GridModel(
//                                PrepareInvoiceTemp_New("", invoice.ShipmentDate.Value.Month,
//                                                       invoice.ShipmentDate.Value.Year)
//                                    .OrderByDescending(o => o.ModifiedDate)));

//                }
//            }
//            catch (Exception ex) {
//                ModelState.AddModelError("DeleteInvoice_New", ex.Message);
//            }
//            return
//                View(
//                    new GridModel(
//                        PrepareInvoiceTemp_New("", DateTime.Now.Month, DateTime.Now.Year)
//                            .OrderByDescending(o => o.ModifiedDate)));
//        }

        [GridAction]
        public ActionResult DeleteInvoice_3(int invoiceId, string note) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
                    if (invoice == null || invoice.Active == false)
                        return View(new GridModel(PrepareInvoiceTemp("").OrderByDescending(o => o.ModifiedDate)));
                    if (invoice.Status == (byte)MyUtilities.Sales.Status.InProcess) {
                        throw new AggregateException("Invoice đã có xuất hóa đơn không thể hủy !");
                    }
                    if (invoice.Status != (byte)MyUtilities.Sales.Status.Waiting) {
                        throw new AggregateException("Lỗi lệnh ! Vui lòng F5 để Refresh");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, invoice.ShipmentDate.Value)) {
                        throw new AggregateException(
                            @"Không có quyền huỷ phiếu xuất tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    //roll back
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                    var exportDetailCheck = export.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.InvoiceDetails.Any(id => id.Active));
                    if (exportDetailCheck != null)
                        throw new AggregateException("Lỗi! Vui lòng hủy hết lệnh phân đơn hàng trước khi hủy lệnh xuất");
                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == export.OrderId);
                    var transaction =
                        vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(export.TransactionCode));
                    if (transaction != null) {
                        //giao dich
                        transaction.Status = (byte)MyUtilities.Transaction.Status.Open;
                        //foreach (var exportDetail in export.ExportFormTP_KDDetail) {
                        foreach (var detail in transaction.TransactionDetails) {
                            var periodExport = vfi.ProductInventoryPeriods.FirstOrDefault(x => x.TransactionId == transaction.TransactionId
                                                                                    && x.ProductInvId == detail.ProductInvId
                                                                                    && x.WarehouseId == MyUtilities.Warehouse.Finish);
                            if (periodExport == null) {
                                throw new AggregateException("Lỗi data! Liên hệ admin!");
                            }
                            var periodImport = vfi.ProductInventoryPeriods.FirstOrDefault(x => x.TransactionId == transaction.TransactionId
                                                                                && x.WarehouseId == MyUtilities.Warehouse.Business
                                                                                && x.Quantity == periodExport.Quantity);

                            //ton kho
                            var productInventoryIssue =
                                vfi.ProductInventories.FirstOrDefault(pi => pi.ProductInventoryId == periodExport.ProductInvId);
                            var productInventoryReceipt =
                                vfi.ProductInventories.FirstOrDefault(pi => pi.ProductInventoryId == periodImport.ProductInvId);

                            productInventoryIssue.TotalQty += periodExport.Quantity;
                            productInventoryReceipt.TotalQty -= periodExport.Quantity;
                        }
                        // tra lai luan chuyen
                        var productInventoryPeriods =
                            vfi.ProductInventoryPeriods.Where(pip => pip.TransactionId == transaction.TransactionId);
                        vfi.ProductInventoryPeriods.RemoveRange(productInventoryPeriods);
                        var orderNoteDetails =
                            vfi.OrderNoteDetails.Where(
                                ond =>
                                ond.OrderNote.InvoiceId == invoiceId &&
                                ond.OrderNote.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        if (orderNoteDetails.Any()) {
                            var transactionIds = orderNoteDetails.Select(ond => ond.OrderNote.TransactionId).ToList();
                            var productInventoryPeriods2 =
                                vfi.ProductInventoryPeriods.Where(pip => transactionIds.Contains(pip.TransactionId));
                            vfi.ProductInventoryPeriods.RemoveRange(productInventoryPeriods2);
                        }
                        var orderNotes =
                            vfi.OrderNotes.Where(
                                ond =>
                                ond.InvoiceId == invoiceId &&
                                ond.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open);
                        foreach (var orderNote in orderNotes) {
                            orderNote.Transaction.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                        }
                    }
                    invoice.Note = note;
                    invoice.Active = false;
                    invoice.Status = (byte)MyUtilities.Sales.Status.Cancel;
                    //foreach(var
                    vfi.SaveChanges();
                    return
                        View(
                            new GridModel(
                                PrepareInvoiceTemp_New("", invoice.ShipmentDate.Value.Month,
                                                       invoice.ShipmentDate.Value.Year)
                                    .OrderByDescending(o => o.ModifiedDate)));

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteInvoice_New", ex.Message);
            }
            return
                View(
                    new GridModel(
                        PrepareInvoiceTemp_New("", DateTime.Now.Month, DateTime.Now.Year)
                            .OrderByDescending(o => o.ModifiedDate)));
        }

        [GridAction]
        public ActionResult DeleteInvoice_4(int invoiceId, string note) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
                    if (invoice == null || invoice.Active == false)
                        return View(new GridModel(PrepareInvoiceTemp("").OrderByDescending(o => o.ModifiedDate)));
                    if (invoice.Status == (byte)MyUtilities.Sales.Status.InProcess) {
                        throw new AggregateException("Invoice đã có xuất hóa đơn không thể hủy !");
                    }
                    if (invoice.Status != (byte)MyUtilities.Sales.Status.Waiting) {
                        throw new AggregateException("Lỗi lệnh ! Vui lòng F5 để Refresh");
                    }
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, invoice.ShipmentDate.Value)) {
                        throw new AggregateException(
                            @"Không có quyền huỷ phiếu xuất tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    //roll back
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                    var exportDetailCheck = export.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.InvoiceDetails.Any(id => id.Active));
                    if (exportDetailCheck != null)
                        throw new AggregateException("Lỗi! Vui lòng hủy hết lệnh phân đơn hàng trước khi hủy lệnh xuất");
                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == export.OrderId);
                    var transaction =
                        vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(export.TransactionCode));
                    if (transaction != null) {
                        //giao dich
                        transaction.Status = (byte)MyUtilities.Transaction.Status.Open;
                        //foreach (var exportDetail in export.ExportFormTP_KDDetail) {
                        foreach (var detail in transaction.TransactionDetails) {
                            var periodExport = vfi.ProductInventoryPeriods.FirstOrDefault(x => x.TransactionId == transaction.TransactionId
                                                                                    && x.ProductInvId == detail.ProductInvId
                                                                                    && x.WarehouseId == MyUtilities.Warehouse.Finish);
                            if (periodExport == null) {
                                throw new AggregateException("Lỗi data! Liên hệ admin!");
                            }
                            var periodImport = vfi.ProductInventoryPeriods.FirstOrDefault(x => x.TransactionId == transaction.TransactionId
                                                                                && x.WarehouseId == MyUtilities.Warehouse.Business
                                                                                && x.Quantity == periodExport.Quantity);

                            //ton kho
                            var productInventoryIssue =
                                vfi.ProductInventories.FirstOrDefault(pi => pi.ProductInventoryId == periodExport.ProductInvId);
                            var productInventoryReceipt =
                                vfi.ProductInventories.FirstOrDefault(pi => pi.ProductInventoryId == periodImport.ProductInvId);

                            productInventoryIssue.TotalQty += periodExport.Quantity;
                            productInventoryReceipt.TotalQty -= periodExport.Quantity;
                        }
                        // tra lai luan chuyen
                        vfi.ProductInventoryPeriods.RemoveRange(transaction.ProductInventoryPeriods);

                        var orderNotes =
                            vfi.OrderNotes.Where(
                                ond =>
                                ond.InvoiceId == invoiceId &&
                                ond.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        foreach (var orderNote in orderNotes) {
                            vfi.ProductInventoryPeriods.RemoveRange(orderNote.Transaction.ProductInventoryPeriods);
                        }
                        orderNotes =
                           vfi.OrderNotes.Where(
                               ond =>
                               ond.InvoiceId == invoiceId &&
                               ond.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open);
                        foreach (var orderNote in orderNotes) {
                            orderNote.Transaction.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                        }
                    }
                    invoice.Note = note;
                    invoice.Active = false;
                    invoice.Status = (byte)MyUtilities.Sales.Status.Cancel;
                    //foreach(var
                    vfi.SaveChanges();
                    return
                        View(
                            new GridModel(
                                PrepareInvoiceTemp_New("", invoice.ShipmentDate.Value.Month,
                                                       invoice.ShipmentDate.Value.Year)
                                    .OrderByDescending(o => o.ModifiedDate)));

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteInvoice_New", ex.Message);
            }
            return
                View(
                    new GridModel(
                        PrepareInvoiceTemp_New("", DateTime.Now.Month, DateTime.Now.Year)
                            .OrderByDescending(o => o.ModifiedDate)));
        }

        [GridAction]
        public ActionResult DeleteInvoiceDetail(int detailId, int exportId) {
            try {
                using (var vfi = new tammaContext()) {
                    var exportDetail = vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == detailId);
                    if (exportDetail.IsInvoiced.Value)
                        throw new AggregateException(
                            "Lỗi! Chi tiết đã được xuất hóa đơn ! Hủy hóa đơn trước khi hủy phân đơn hàng!");

                    var invoice = vfi.Invoices.FirstOrDefault(e => e.ExportId == exportId);
                    var invoiceDetails =
                        vfi.InvoiceDetails.Where(id => id.ExportDetailId == detailId && id.Active && id.Piece > 0);
                    var invoiceDetailSentBacks =
                        vfi.InvoiceDetails.Where(id => id.ExportDetailId == detailId && id.Active && id.Piece < 0).ToList();
                    var totalSentBack = invoiceDetailSentBacks.Sum(id => id.Piece * -1);
                    foreach (var invoiceDetail in invoiceDetails) {
                        var orderDetail =
                            vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == invoiceDetail.OrderDetailId);
                        var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderDetail.OrderId);
                        if (totalSentBack > 0) {
                            if (totalSentBack < invoiceDetail.Piece) {
                                orderDetail.RequiedNumber += (invoiceDetail.Piece - totalSentBack);
                                //orderDetail.OrderQty -= totalSentBack;
                                totalSentBack = 0;
                                if (order.Status == (byte)MyUtilities.Sales.Status.Completed)
                                    order.Status = (byte)MyUtilities.Sales.Status.InProcess;
                            }
                            else {
                                orderDetail.RequiedNumber += invoiceDetail.Piece;
                                //orderDetail.Note += (" !Trả hàng" + (totalSentBack - invoiceDetail.Piece));
                                totalSentBack -= invoiceDetail.Piece;
                            }
                        }
                        else {
                            if (order.Status == (byte)MyUtilities.Sales.Status.Completed)
                                order.Status = (byte)MyUtilities.Sales.Status.InProcess;
                            orderDetail.RequiedNumber += invoiceDetail.Piece;
                        }
                        if (orderDetail.RequiedNumber > 0)
                            orderDetail.IsComplete = false;
                        //invoiceDetail.Active = false;
                    }
                    //foreach (var sentBack in invoiceDetailSentBacks)
                    //{
                    //    sentBack.Active = false;
                    //}
                    vfi.InvoiceDetails.RemoveRange(invoiceDetails);
                    vfi.InvoiceDetails.RemoveRange(invoiceDetailSentBacks);
                    vfi.SaveChanges();
                    return View(new GridModel(InvoiceDetailListByExportId_New(exportId, invoice.InvoiceId)));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteInvocie", ex.Message);
            }
            return View(new GridModel(InvoiceDetailListByExportId(exportId)));
        }


        [GridAction]
        public ActionResult UpdateProductTaxInvoiceDetailByExportDetailId(int detailId, string taxInvoiceList) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var exportDetail = vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == detailId);
                    if (exportDetail == null || exportDetail.IsInvoiced == true)
                        throw new AggregateException("Lỗi chi tiết sản phẩm xuất !! Vui lòng F5 để làm mới !!");
                    if (string.IsNullOrWhiteSpace(taxInvoiceList))
                        throw new AggregateException("Chứng từ không được để trống !!");
                    taxInvoiceList = taxInvoiceList.Trim();
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.ExportId == exportDetail.ExportId);
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == exportDetail.ExportId);
                    var orderNotes =
                        vfi.OrderNotes.Where(on => on.InvoiceId == invoice.InvoiceId && on.NoteType == 1)
                           .ToList();
                    var orderDetail =
                           vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == exportDetail.OrderDetailId);
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.TaxInvoiceList.Equals(taxInvoiceList));
                    if (taxInvoice == null) {
                        taxInvoice = new TaxInvoice {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Currency = orderDetail.Order.CurrencyCode,
                            TaxInvoiceList = taxInvoiceList,
                            Status = (byte)MyUtilities.Sales.Status.Waiting,
                            TaxPercent = invoice.TaxPercent,
                            ExchangeRate = invoice.ExchangeRate,
                            CustomerId = orderDetail.Order.CustomerId,
                        };
                        vfi.TaxInvoices.Add(taxInvoice);
                    }
                    else {
                        if (taxInvoice.Status == (byte)MyUtilities.Sales.Status.InProcess) {
                            throw new AggregateException("Lỗi Chứng từ: chứng từ đã duyệt vui lòng nhập chứng từ khác !"
                                                         + taxInvoiceList + " !");
                        }
                        if (taxInvoice.Status == (byte)MyUtilities.Sales.Status.Cancel) {
                            taxInvoice.ModifiedDate = DateTime.Now;
                            taxInvoice.ModifiedUser = HttpContext.User.Identity.Name;
                            taxInvoice.Currency = orderDetail.Order.CurrencyCode;
                            taxInvoice.TaxInvoiceList = taxInvoiceList;
                            taxInvoice.Status = (byte)MyUtilities.Sales.Status.Waiting;
                            taxInvoice.TaxPercent = invoice.TaxPercent;
                            taxInvoice.ExchangeRate = invoice.ExchangeRate;
                            taxInvoice.CustomerId = orderDetail.Order.CustomerId;
                        }
                        else if (taxInvoice.Status == (byte)MyUtilities.Sales.Status.Completed) {
                            throw new AggregateException("Lỗi Chứng từ: chứng từ này đã khóa sổ " + taxInvoiceList +
                                                         " !");
                        }
                        else {

                            if (taxInvoice.Currency != orderDetail.Order.CurrencyCode)
                                throw new AggregateException("Lỗi tiền tệ ! (Chứng từ: " + taxInvoice.Currency +
                                                             " - Invoice: " + orderDetail.Order.CurrencyCode + ")");
                            if (taxInvoice.TaxPercent != (invoice.TaxPercent))
                                throw new AggregateException("Lỗi thuế ! (Chứng từ: " + taxInvoice.TaxPercent +
                                                             " - Invoice: " + (invoice.TaxPercent) + ")");
                            if (taxInvoice.ExchangeRate != (invoice.ExchangeRate))
                                throw new AggregateException("Lỗi tỉ giá ! (Chứng từ: " + taxInvoice.ExchangeRate +
                                                             " - Invoice: " + (invoice.ExchangeRate) + ")");
                            if (taxInvoice.CustomerId != orderDetail.Order.CustomerId) {
                                var customerCodeTaxInvoice = taxInvoice.Customer.CustomerCode;
                                var customerCodeOrder = orderDetail.Order.Customer.CustomerCode;
                                var lastChar = customerCodeTaxInvoice[customerCodeTaxInvoice.Length - 1];
                                if ('A' <= lastChar && lastChar <= 'Z') {
                                    var codeTaxInvoice = customerCodeTaxInvoice.Remove(customerCodeTaxInvoice.Length - 1);
                                    var codeOrder = customerCodeOrder.Remove(customerCodeOrder.Length - 1);
                                    if (!codeTaxInvoice.Equals(codeOrder)) {
                                        throw new AggregateException("Lỗi khách hàng ! (Chứng từ: " +
                                                                     taxInvoice.Customer.CustomerCode +
                                                                     " - Invoice: " +
                                                                     orderDetail.Order.Customer.CustomerCode +
                                                                     ")");
                                    }
                                }
                                else {
                                    throw new AggregateException("Lỗi khách hàng ! (Chứng từ: " +
                                                                 taxInvoice.Customer.CustomerCode +
                                                                 " - Invoice: " +
                                                                 orderDetail.Order.Customer.CustomerCode + ")");
                                }
                            }
                        }
                    }
                    var sendBackQuantity = 0.0;
                    var orderNotesByProductId =
                         orderNotes.Where(
                             on =>
                             on.OrderNoteDetails.FirstOrDefault(ond => ond.ProductId == exportDetail.ProductId) != null &&
                             on.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);

                    if (orderNotes.Any()) {
                        foreach (var orderNote in orderNotesByProductId) {
                            //var transaction =
                            //    vfi.Transactions.FirstOrDefault(
                            //        t =>
                            //        t.TransactionId == orderNote.TransactionId &&
                            //        t.Status == (byte) MyUtilities.Transaction.Status.Approved);
                            //if (transaction != null)
                            //{
                            var orderNoteDetail =
                                orderNote.OrderNoteDetails.FirstOrDefault(
                                    ond => ond.ProductId == exportDetail.ProductId);
                            sendBackQuantity += orderNoteDetail.Quantity.Value;
                            //}
                        }
                    }
                    if (exportDetail.Quality - sendBackQuantity == 0)
                        throw new AggregateException("Lỗi sản phẩm xuất = 0! Vui lòng F5 làm lại");
                    var detail = new TaxInvoiceProductDetail {
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ExportDate = export.DateTransporter,
                        ExportDetailId = detailId,
                        UnitPrice = orderDetail.UnitPrice,
                        Quantity = exportDetail.Quality - sendBackQuantity,
                        TaxInvoice = taxInvoice,
                        TaxInvoiceId = taxInvoice.Id,
                        ExportFormTP_KDDetail = exportDetail,
                        Active = true,
                    };
                    vfi.TaxInvoiceProductDetails.Add(detail);
                    exportDetail.IsInvoiced = true;
                    vfi.SaveChanges();
                    var exportUpdate = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == exportDetail.ExportId);
                    if (exportUpdate.ExportFormTP_KDDetail.Count(od => od.IsInvoiced == false) == 0)
                        invoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                    else
                        invoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                    vfi.SaveChanges();

                    return View(new GridModel(InvoiceDetailListByExportId(exportDetail.ExportId.Value)));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateTaxInvoiceDetail", ex.Message);
            }
            return View(new GridModel(new List<InvoiceDetailTempModel>()));
        }

        [GridAction]
        public ActionResult UpdateProductTaxInvoiceDetailByExportDetailId_New(int invoiceId, int detailId, string taxInvoiceList) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var exportDetail = vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == detailId);
                    if (exportDetail == null || exportDetail.IsInvoiced == true)
                        throw new AggregateException("Lỗi chi tiết sản phẩm xuất !! Vui lòng F5 để làm mới !!");
                    var invoiceDetails =
                        vfi.InvoiceDetails.Where(id => id.Active && id.ExportDetailId == exportDetail.DetailId).ToList();
                    if (!invoiceDetails.Any())
                        throw new AggregateException("Lỗi chi tiết sản phẩm xuất !! Chưa phân đơn hàng !!");
                    if (string.IsNullOrWhiteSpace(taxInvoiceList))
                        throw new AggregateException("Chứng từ không được để trống !!");
                    taxInvoiceList = taxInvoiceList.Trim();
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
                    var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == exportDetail.ExportId);
                    var currencyCode = " ";
                    var order = invoiceDetails.FirstOrDefault().OrderDetail.Order;
                    if (order != null) currencyCode = order.CurrencyCode;
                    if (string.IsNullOrWhiteSpace(currencyCode))
                        throw new AggregateException("Lỗi chi tiết sản phẩm xuất !! Đơn hàng lỗi tiền tệ !!");
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.TaxInvoiceList.Equals(taxInvoiceList));
                    if (taxInvoice == null) {
                        taxInvoice = new TaxInvoice {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Currency = currencyCode,
                            TaxInvoiceList = taxInvoiceList,
                            Status = (byte)MyUtilities.Sales.Status.Waiting,
                            TaxPercent = invoice.TaxPercent,
                            ExchangeRate = invoice.ExchangeRate,
                            CustomerId = export.CustomerId,

                        };
                        vfi.TaxInvoices.Add(taxInvoice);
                    }
                    else {
                        if (taxInvoice.Status == (byte)MyUtilities.Sales.Status.InProcess) {
                            throw new AggregateException("Lỗi Chứng từ: chứng từ đã duyệt vui lòng nhập chứng từ khác !"
                                                         + taxInvoiceList + " !");
                        }
                        if (taxInvoice.Status == (byte)MyUtilities.Sales.Status.Cancel) {
                            taxInvoice.ModifiedDate = DateTime.Now;
                            taxInvoice.ModifiedUser = HttpContext.User.Identity.Name;
                            taxInvoice.Currency = currencyCode;
                            taxInvoice.TaxInvoiceList = taxInvoiceList;
                            taxInvoice.Status = (byte)MyUtilities.Sales.Status.Waiting;
                            taxInvoice.TaxPercent = invoice.TaxPercent;
                            taxInvoice.ExchangeRate = invoice.ExchangeRate;
                            taxInvoice.CustomerId = export.CustomerId;
                        }
                        else if (taxInvoice.Status == (byte)MyUtilities.Sales.Status.Completed) {
                            throw new AggregateException("Lỗi Chứng từ: chứng từ này đã khóa sổ " + taxInvoiceList +
                                                         " !");
                        }
                        else {
                            if (taxInvoice.Currency != currencyCode)
                                throw new AggregateException("Lỗi tiền tệ ! (Chứng từ: " + taxInvoice.Currency +
                                                             " - Invoice: " + currencyCode + ")");
                            if (taxInvoice.TaxPercent != (invoice.TaxPercent))
                                throw new AggregateException("Lỗi thuế ! (Chứng từ: " + taxInvoice.TaxPercent +
                                                             "% - Invoice: " + (invoice.TaxPercent) + "%)");
                            if (taxInvoice.ExchangeRate != (invoice.ExchangeRate))
                                throw new AggregateException("Lỗi tỉ giá ! (Chứng từ: " + taxInvoice.ExchangeRate +
                                                             " - Invoice: " + (invoice.ExchangeRate) + ")");
                            if (taxInvoice.CustomerId != export.CustomerId) {
                                var customerCodeTaxInvoice = taxInvoice.Customer.CustomerCode;
                                var customerCodeOrder = export.Customer.CustomerCode;
                                var lastChar = customerCodeTaxInvoice[customerCodeTaxInvoice.Length - 1];
                                if ('A' <= lastChar && lastChar <= 'Z') {
                                    var codeTaxInvoice = customerCodeTaxInvoice.Remove(customerCodeTaxInvoice.Length - 1);
                                    var codeOrder = customerCodeOrder.Remove(customerCodeOrder.Length - 1);
                                    if (!codeTaxInvoice.Equals(codeOrder)) {
                                        throw new AggregateException("Lỗi khách hàng ! (Chứng từ: " +
                                                                     taxInvoice.Customer.CustomerCode +
                                                                     " - Invoice: " +
                                                                     export.Customer.CustomerCode +
                                                                     ")");
                                    }
                                }
                                else {
                                    throw new AggregateException("Lỗi khách hàng ! (Chứng từ: " +
                                                                 taxInvoice.Customer.CustomerCode +
                                                                 " - Invoice: " +
                                                                export.Customer.CustomerCode + ")");
                                }
                            }
                        }
                    }
                    var detail = new TaxInvoiceProductDetail {
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ExportDate = export.DateTransporter,
                        ExportDetailId = detailId,
                        UnitPrice = invoiceDetails.FirstOrDefault().OrderDetail.UnitPrice,
                        Quantity = invoiceDetails.Sum(id => id.Piece),
                        TaxInvoice = taxInvoice,
                        TaxInvoiceId = taxInvoice.Id,
                        ExportFormTP_KDDetail = exportDetail,
                        Active = true,
                    };
                    vfi.TaxInvoiceProductDetails.Add(detail);

                    exportDetail.IsInvoiced = true;
                    vfi.SaveChanges();
                    if (export.ExportFormTP_KDDetail.Count(od => od.IsInvoiced == false) == 0)
                        invoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                    else
                        invoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                    vfi.SaveChanges();

                    return View(new GridModel(InvoiceDetailListByExportId(export.ExportId)));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateTaxInvoiceDetail", ex.Message);
            }
            return View(new GridModel(new List<InvoiceDetailTempModel>()));
        }
        
        [GridAction]
        public ActionResult CancelQuantityProduct(
            [Bind(Prefix = "inserted")] IEnumerable<ExportFormTP_KDDetailsModel> insertedProductExportTermDetails,
            [Bind(Prefix = "updated")] IEnumerable<ExportFormTP_KDDetailsModel> updatedProductExportTermDetails,
            [Bind(Prefix = "deleted")] IEnumerable<ExportFormTP_KDDetailsModel> deletedProductExportTermDetails
            , int? orderId
            ) {

            if (updatedProductExportTermDetails != null) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    using (var vfi = new tammaContext()) {
                        foreach (var detail in updatedProductExportTermDetails) {
                            if (detail.Quality == 0) continue;
                            var orderDetail =
                                vfi.OrderDetails.FirstOrDefault(
                                    od => od.OrderId == orderId && od.OrderDetailId == detail.DetailId);
                            if (orderDetail != null) {
                                orderDetail.RequiedNumber = orderDetail.RequiedNumber - MyUtilities.Function.RoundUp(detail.Quality);
                                if (orderDetail.RequiedNumber < 0) {
                                    throw new AggregateException("Không thể huỷ nhiều hơn số lượng còn lại");
                                }
                                if (orderDetail.RequiedNumber == 0) {
                                    orderDetail.IsComplete = true;
                                }
                                orderDetail.Note = ("Huỷ: " + detail.Quality + ":" +
                                                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")) + ":" +
                                                    detail.Note + ":" +
                                                    HttpContext.User.Identity.Name;
                                orderDetail.OrderQty -= MyUtilities.Function.RoundUp(detail.Quality);
                                vfi.SaveChanges();
                            }
                        }
                        var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                        if (!order.OrderDetails.Any(od => od.IsComplete == false)) {
                            order.Status = (byte)MyUtilities.Sales.Status.Completed;
                            vfi.SaveChanges();
                        }

                        return View(new GridModel(new List<ProductInventoryModel>()));
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCodeName", "" + exception.Message);
                }
            }
            return View(new GridModel(new List<ProductInventoryModel>()));
        }

        public ActionResult SelectComboBoxOrderStatus() {
            var val = from MyUtilities.Sales.Status stt in Enum.GetValues(typeof(MyUtilities.Sales.Status))
                      select new {
                          Value = (int)Enum.Parse(typeof(MyUtilities.Sales.Status), stt.ToString()),
                          Text =
                      MyUtilities.Sales.GetText(
                          (int)Enum.Parse(typeof(MyUtilities.Sales.Status), stt.ToString()))
                      };

            return new JsonResult {
                Data = new SelectList(val, "Value", "Text")
            };
        }

        public ActionResult SelectComboBoxExportTpTransaction() {
            var model = new List<InvoiceTempModel>();
            using (var vfi = new tammaContext()) {
                var startTaxInvoiceDate = new DateTime(2014, 12, 31, 10, 0, 0);
                //vfi.Configuration.LazyLoadingEnabled = false;
                var invoices = (from i in vfi.Invoices
                                where
                                    (i.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                     i.Status == (byte)MyUtilities.Sales.Status.InProcess)
                                     && i.ShipmentDate > startTaxInvoiceDate
                                     && i.Active
                                     && i.ExportFormTP_KD.ExportFormTP_KDDetail.Any(ed => !ed.InvoiceDetails.Any(id => id.Active))
                                orderby i.ShipmentDate
                                //&& i.InvoiceId == 1558
                                select new {
                                    i.InvoiceId,
                                    i.InvoiceNumber,
                                    i.Customer.CustomerCode,
                                    i.Customer.ShortName,
                                    //i.ExportId,
                                    ShipmentDate = i.ShipmentDate.Value,
                                    //i.OrderId,
                                    //i.Note,
                                    //i.Active,
                                    //i.ExchangeRate,
                                    //i.TaxPercent,
                                    //i.ModifiedDate,
                                    //i.ModifiedUser,

                                }).ToList();
                foreach (var invoice in invoices) {
                    //var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                    //if (export == null) continue;
                    //var exportDetail = export.ExportFormTP_KDDetail.FirstOrDefault(ed => !ed.InvoiceDetails.Any(id => id.Active));
                    //if (exportDetail == null) continue;
                    var entity = new InvoiceTempModel {
                        InvoiceId = invoice.InvoiceId,
                        InvoiceNumber = invoice.InvoiceNumber + " - " + invoice.ShipmentDate.ToString("dd/MM/yyyy")
                                        + " - " + invoice.CustomerCode + " - " + invoice.ShortName,
                    };
                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "InvoiceId", "InvoiceNumber"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxPaymentTerm() {
            using (var vfi = new tammaContext()) {
                return new JsonResult {
                    Data = new SelectList(vfi.PaymentTerms.ToList(), "Id", "TermName")
                };
            }
        }

        public ActionResult SelectComboBoxPaymentMethod() {
            using (var vfi = new tammaContext()) {
                var model = (from m in vfi.Methods
                            where m.MethodTypeId == MethodEnumTypeId.PaymentMethod && m.Active.Value
                            select new {
                                m.MethodId,
                                m.MethodName
                            }).ToList();
                return new JsonResult {
                    Data = new SelectList(model, "MethodId", "MethodName")
                };
            }
        }

        public ActionResult SelectOrderInProcessAndComplete() {
            using (var vfi = new tammaContext()) {
                var orderModels = vfi.Orders.Where(o => o.DueDate != null && o.Active
                    && (o.Status == (byte)MyUtilities.Sales.Status.InProcess || o.Status == (byte)MyUtilities.Sales.Status.Completed))
                    .Select(x => new { x.OrderId, x.OrderNumber })
                    .ToList();
                return new JsonResult {
                    Data = new SelectList(orderModels, "OrderId", "OrderNumber")
                };
            }
        }

        [HttpPost]
        public ActionResult ExportOrderDetailSaveDefault() {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

            var modifiedUser = HttpContext.User.Identity.Name;
            if (string.IsNullOrWhiteSpace(modifiedUser))
                return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
            var listOrderDetails = (List<OrderDetailModel>)Session["SessionInvoiceDetail"];
            if (!listOrderDetails.Any())
                return Json("Error! List null", JsonRequestBehavior.AllowGet);
            //return View(new GridModel(new List<OrderDetailModel>()));
            try {
                listOrderDetails = listOrderDetails.Where(l => l.AvailableQty > 0).ToList();
                UpdateAddInvoiceDetail(listOrderDetails);
                return Json("Hoàn tất lưu !", JsonRequestBehavior.AllowGet);
                //return View(new GridModel(new List<OrderDetailModel>()));
            }
            catch (Exception exception) {
                ModelState.AddModelError("ExportOrderDetailSaveDefault", "" + exception.Message);
                return Json("Error! " + exception.Message, JsonRequestBehavior.AllowGet);

            }
            return Json("Error! UnKnow", JsonRequestBehavior.AllowGet);
            //return View(new GridModel(new List<OrderDetailModel>()));
        }


        [GridAction]
        public ActionResult AddExportInvoiceDetail(
            [Bind(Prefix = "inserted")] IEnumerable<OrderDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<OrderDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<OrderDetailModel> deletedDetails
            //, int? id
            ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<OrderDetailModel>()));
            }
            var listOrderDetails = (List<OrderDetailModel>)Session["SessionInvoiceDetail"];
            if (!listOrderDetails.Any())
                return View(new GridModel(new List<OrderDetailModel>()));
            //if (updatedDetails != null)
            try {
                if (updatedDetails != null) {
                    foreach (var updateDetail in updatedDetails) {
                        var detail = listOrderDetails.FirstOrDefault(od => od.OrderDetailId == updateDetail.OrderDetailId);
                        detail.AvailableQty = updateDetail.AvailableQty;
                    }
                    listOrderDetails = listOrderDetails.Where(l => l.AvailableQty > 0).ToList();
                    UpdateAddInvoiceDetail(listOrderDetails);
                }

            }
            catch (Exception exception) {
                ModelState.AddModelError("AddExportInvoiceDetail", "" + exception.Message);
            }
            Session["SessionInvoiceDetail"] = new List<OrderDetailModel>();
            return View(new GridModel(new List<OrderDetailModel>()));
        }

        private int UpdateAddInvoiceDetail(List<OrderDetailModel> list) {
            using (var vfi = new tammaContext()) {
                //var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == id);
                //var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.InvoiceId);
                if (list.Select(l => l.Currency).Distinct().Count() > 1)
                    throw new AggregateException("Lỗi! Có 2 đơn hàng khác tiền tệ !");
                //if (list.Select(l => l.UnitPrice).Distinct().Count() > 1)
                //    throw new AggregateException("Lỗi! Có 2 đơn hàng khác giá tiền !");
                var orderDetailFirst = list.FirstOrDefault();
                var exportDetail =
                    vfi.ExportFormTP_KDDetail.FirstOrDefault(
                        ed => ed.DetailId == orderDetailFirst.ExportDetailId);
                if (exportDetail == null)
                    throw new AggregateException("Lỗi! Không tìm được chi tiết xuất kho!");
                if (exportDetail.InvoiceDetails.Any(id => id.Active))
                    throw new AggregateException("Lỗi! Chi tiết này đã phân đơn hàng!");
                var invoice = vfi.Invoices.FirstOrDefault(i => i.ExportId == exportDetail.ExportId && i.Active);
                if (invoice == null)
                    throw new AggregateException("Lỗi! Không tìm được phiếu xuất kho!");
                if (exportDetail.Quality != list.Sum(od => od.AvailableQty))
                    throw new AggregateException("Lỗi! Phân chưa đủ số lượng xuất kho");
                var orders = new List<Order>();
                foreach (var orderDetailModel in list) {
                    var orderDetail =
                        vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailModel.OrderDetailId);
                    if (orderDetail == null) throw new AggregateException("Lỗi! Không tìm thấy chi tiết đơn hàng");
                    if (orderDetail.RequiedNumber < orderDetailModel.AvailableQty)
                        throw new AggregateException("Lỗi! Số lượng phân lớn hơn số yêu cầu " +
                                                     orderDetail.Order.OrderNumber);
                    if (orderDetailModel.AvailableQty > 0) {
                        var entity = new InvoiceDetail {
                            Piece = MyUtilities.Function.RoundUp(orderDetailModel.AvailableQty),
                            OrderDetailId = orderDetail.OrderDetailId,
                            Price = orderDetail.UnitPrice,
                            ProductId = orderDetailModel.ProductId,
                            Active = true,
                            ExportDetailId = orderDetailModel.ExportDetailId,
                            InvoiceId = invoice.InvoiceId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                        };
                        vfi.InvoiceDetails.Add(entity);
                        orderDetail.RequiedNumber -= entity.Piece;
                        if (orderDetail.RequiedNumber == 0) {
                            orderDetail.IsComplete = true;
                            if (!orders.Contains(orderDetail.Order))
                                orders.Add(orderDetail.Order);
                        }
                    }
                }
                foreach (var order in orders) {
                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                    if (order.OrderDetails.Any(od => od.RequiedNumber != 0))
                        continue;
                    order.Status = (byte)MyUtilities.Sales.Status.Completed;
                }
                var a = vfi.SaveChanges();
                Session["SessionInvoiceDetail"] = new List<OrderDetailModel>();
                return a;
            }
        }


        [GridAction]
        public ActionResult SendBackQuantityProduct(
            [Bind(Prefix = "inserted")] IEnumerable<ExportFormTP_KDDetailsModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<ExportFormTP_KDDetailsModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<ExportFormTP_KDDetailsModel> deletedDetails
            , int? invoiceId, int? type, string createdDate
            ) {
            if (updatedDetails != null) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    if (invoiceId == 0 || invoiceId == null)
                        throw new ArgumentException("Vui lòng chọn 1 invoice");
                    if (type == 0 || type == null)
                        throw new ArgumentException("Vui lòng chọn loại trả hàng");
                    var ci = new CultureInfo("vi-VN");
                    var tDate = string.IsNullOrWhiteSpace(createdDate)
                                    ? DateTime.Now
                                    : Convert.ToDateTime(createdDate, ci);
                    if (MyUtilities.UserRole.CheckTransaction(HttpContext.User.Identity.Name, tDate)) {
                        throw new AggregateException(
                            @"Không có quyền tạo phiếu tháng trước! \n Hạn chót ngày: 05! \n Vui lòng liên hệ quản lý !");
                    }
                    using (var vfi = new tammaContext()) {
                        var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
                        var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                        if (export != null && tDate < export.DateTransporter)
                            throw new AggregateException("Lỗi! Không thể trả hàng trước khi xuất hàng\n" +
                                                         "Ngày trả hàng nhỏ hơn ngày xuất hàng");
                        var transaction =
                            vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(export.TransactionCode));
                        //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == export.OrderId);
                        var orderNote = new OrderNote {
                            NoteNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.OrderNote, 1),
                            CreatedDate = tDate,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            NoteType = (byte)type,
                            Status = (byte)MyUtilities.Sales.Status.Waiting,
                            InvoiceId = invoiceId,
                            ExportId = invoice.ExportId,
                            IsComplete = false,
                        };
                        var transactionNote = new Transaction {
                            WarehouseIssueId = MyUtilities.Warehouse.Business,
                            WarehouseReceiptId = MyUtilities.Warehouse.Finish,
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            EoI = "2",
                            MoP = false,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = orderNote.CreatedDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now
                        };
                        foreach (var detail in updatedDetails) {
                            if (detail.Quality == 0) continue;
                            if (string.IsNullOrWhiteSpace(detail.Note)) { 
                                throw new ArgumentException("Chưa nhập lý do đổi trả " + detail.ProductCode); 
                            }
                            var exportDetail =
                                export.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == detail.DetailId);
                            if (exportDetail.IsInvoiced == true) { 
                                throw new ArgumentException("Sản phẩm đã xuất chứng từ " + detail.ProductCode); 
                            }
                            var transactionDetail =
                                transaction.TransactionDetails.FirstOrDefault(
                                    td => td.TransactionDetailId == detail.TransactionDetailId);
                            if (detail.Quality > transactionDetail.Quantity) {
                                throw new ArgumentException("Không được trả quá số lượng đã giao " +
                                                               detail.ProductCode);
                            }
                            var productInvFinish =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi =>
                                        pi.ProductInventoryId == transactionDetail.ProductInvId.Value);
                            if (productInvFinish == null) { 
                                throw new AggregateException("Lỗi! Không tìm thấy tồn kho KD, thông báo admin."); 
                            }
                            var productNoteInv =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi =>
                                        pi.ProductId == productInvFinish.ProductId &&
                                        pi.LotNumber.Equals(productInvFinish.LotNumber) &&
                                        pi.WarehouseId == MyUtilities.Warehouse.Business);
                            var noteDetail = new OrderNoteDetail {
                                NoteId = orderNote.NoteId,
                                ProductId = detail.ProductId,
                                Quantity = detail.Quality,
                                Note = detail.Note,
                                Weight = detail.Weight,
                            };
                            orderNote.OrderNoteDetails.Add(noteDetail);
                            var transactionDetailNote = new TransactionDetail {
                                Transaction = transactionNote,
                                TransactionId = transactionNote.TransactionId,
                                ReferenceId = detail.ProductId,
                                MoP = false,
                                Quantity = detail.Quality,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                QuantityKg = detail.Weight,
                                Note = detail.Note,
                                ProductInvId = productNoteInv.ProductInventoryId,
                                LotNumber = productNoteInv.LotNumber,
                            };
                            if (type == 1)
                                transactionDetailNote.Note = "Trả hàng";
                            else if (type == 2)
                                transactionDetailNote.Note = "Đổi hàng";
                            transactionNote.TransactionDetails.Add(transactionDetailNote);
                        }

                        if (orderNote.OrderNoteDetails.Any()) {
                            orderNote.Transaction = transactionNote;
                            vfi.OrderNotes.Add(orderNote);
                            vfi.Transactions.Add(transactionNote);
                            //vfi.Transactions.Add(transactionReturn);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCodeName", "" + exception.Message);
                }
            }
            return View(new GridModel(new List<ProductInventoryModel>()));
        }

        [GridAction]
        public ActionResult ChangeOrderDetailProduct(
            [Bind(Prefix = "inserted")] IEnumerable<ExportFormTP_KDDetailsModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<ExportFormTP_KDDetailsModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<ExportFormTP_KDDetailsModel> deletedDetails
            , int? noteId, string exportDate
            ) {
            if (updatedDetails != null) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    if (noteId == 0 || noteId == null)
                        throw new ArgumentException("Vui lòng chọn 1 phiếu");
                    var ci = new CultureInfo("vi-VN");
                    var tDate = string.IsNullOrWhiteSpace(exportDate)
                                    ? DateTime.Now
                                    : Convert.ToDateTime(exportDate, ci);
                    using (var vfi = new tammaContext()) {
                        var orderNote = vfi.OrderNotes.FirstOrDefault(on => on.NoteId == noteId);
                        var transaction = new Vfi.Models.Transaction {
                            WarehouseIssueId = 11,
                            WarehouseReceiptId = 14,
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            EoI = "2",
                            MoP = false,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = orderNote.CreatedDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                        };
                        var export = new ExportChangeProduct {
                            NoteId = noteId,
                            ExportDate = tDate,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            TransactionId = transaction.TransactionId,
                        };
                        foreach (var detail in updatedDetails) {
                            var orderNoteDetail =
                                orderNote.OrderNoteDetails.FirstOrDefault(ond => ond.ProductId == detail.ProductId);
                            if (orderNoteDetail == null) continue;
                            if (detail.Quality > orderNoteDetail.Quantity)
                                throw new ArgumentException("Không được đổi nhiều hơn số lượng được yêu cầu (" + detail.ProductCode + ")");
                            var transactionDetail = new Vfi.Models.TransactionDetail {
                                Transaction = transaction,
                                ReferenceId = detail.ProductId,
                                MoP = false,
                                Quantity = detail.Quality,
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                QuantityKg = detail.Weight,
                                Note = "Đổi hàng " + detail.Note,
                            };
                            transaction.TransactionDetails.Add(transactionDetail);
                        }
                        if (transaction.TransactionDetails.Any()) {
                            vfi.Transactions.Add(transaction);
                            vfi.ExportChangeProducts.Add(export);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCodeName", "" + exception.Message);
                }
            }
            return View(new GridModel(new List<ProductInventoryModel>()));
        }

        List<TaxInvoiceModel> PrepareTaxInvoice() {
            var model = new List<TaxInvoiceModel>();
            try {
                using (var vfi = new tammaContext()) {
                    //var startTaxInvoiceDate = new DateTime(2014, 12,31,10,0,0);
                    var taxInvoices = vfi.TaxInvoices.Where(ti => ti.Status == (byte)MyUtilities.Sales.Status.Waiting
                        //&& ti.TaxInvoiceList.Equals("VP/14P-0055")
                        );
                    foreach (var taxInvoice in taxInvoices) {
                        //var taxInvoiceProductDetails =
                        //    taxInvoice.TaxInvoiceProductDetails.Where(tip => tip.Active == true
                        //        //&& tip.ExportDate > MyUtilities.Sales.StartTaxInvoiceDate
                        //    ).ToList();
                        //if (!taxInvoiceProductDetails.Any()) continue;
                        //var taxInvoiceProduct =
                        //    taxInvoiceProductDetails.FirstOrDefault(
                        //        tip => tip.ExportDate >= MyUtilities.Sales.StartTaxInvoiceDateCheat && tip.Active == true);
                        //if (taxInvoiceProduct == null) continue;
                        //mới
                        var entity = new TaxInvoiceModel();
                        entity.TaxInvoiceList = taxInvoice.TaxInvoiceList;
                        entity.TaxInvoiceId = taxInvoice.Id;
                        entity.ModifiedDate = taxInvoice.ModifiedDate ?? DateTime.Now;
                        entity.Tax = taxInvoice.TaxPercent;
                        entity.ExchangeRate = taxInvoice.ExchangeRate;
                        entity.CurrencyCode = taxInvoice.Currency;
                        entity.CustomerCode = taxInvoice.Customer.CustomerCode;
                        entity.TotalQuantity =
                            taxInvoice.TaxInvoiceProducts.Sum(tip => tip.Quantity);
                        entity.TotalAmount =
                            taxInvoice.TaxInvoiceProducts.Sum(tip => Math.Round(tip.Quantity * tip.UnitPrice, 2));
                        entity.TotalAmount += (entity.TotalAmount * entity.Tax / 100);

                        entity.RequiredAmount = (entity.TotalAmount ?? 0);
                        //entity.RequiredAmount -= total;
                        entity.TotalAmountVND = (entity.TotalAmount ?? 0) * entity.ExchangeRate;
                        entity.TotalAmountVND =
                            (double)Math.Round((decimal)(entity.TotalAmountVND));
                        entity.RequiredAmountVND = entity.TotalAmountVND;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex.Message);
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectPrepareTaxInvoice() {
            try {
                return View(new GridModel(PrepareTaxInvoice().OrderByDescending(o => o.ModifiedDate)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("ApproveTaxInvoice", ex.Message);
                return View(new GridModel(new List<TaxInvoiceModel>()));
            }
        }

        [GridAction]
        public ActionResult UpdateTaxInvoice(int taxInvoiceId, string setupDate) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(setupDate))
                    throw new AggregateException("Ngày lập không được để trống");
                using (var vfi = new tammaContext()) {
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == taxInvoiceId);
                    if (taxInvoice == null)
                        throw new AggregateException("Lỗi chứng từ ! Không tìm thấy chứng từ hợp lệ !");
                    taxInvoice.SetupDate = Convert.ToDateTime(setupDate);
                    taxInvoice.TotalAmount =
                        Math.Round(taxInvoice.TaxInvoiceProductDetails.Where(tip => tip.Active == true)
                                             .Sum(tip => tip.Quantity * tip.UnitPrice), 2);
                    taxInvoice.TotalAmount += Math.Round((taxInvoice.TotalAmount * taxInvoice.TaxPercent / 100).Value, 2);
                    if (taxInvoice.TotalAmount == 0)
                        taxInvoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                    else
                        taxInvoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                    vfi.SaveChanges();
                }
                return View(new GridModel(PrepareTaxInvoice().OrderByDescending(o => o.ModifiedDate)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("ApproveTaxInvoice", ex.Message);
                return View(new GridModel(new List<TaxInvoiceModel>()));
            }
        }

        [GridAction]
        public ActionResult CancelTaxInvoice(int taxInvoiceId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == taxInvoiceId);
                    if (taxInvoice.Status == (byte)MyUtilities.Sales.Status.Waiting)
                        taxInvoice.Status = (byte)MyUtilities.Sales.Status.Cancel;
                    else
                        throw new AggregateException("Chứng từ này đã được duyệt rồi ! Vui lòng F5 Refresh lại để có danh sách mới nhất");
                    foreach (var productDetail in taxInvoice.TaxInvoiceProductDetails.Where(tip => tip.Active == true)) {
                        productDetail.Active = false;
                        var exportDetail =
                            vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == productDetail.ExportDetailId);
                        exportDetail.IsInvoiced = false;
                        var invoice = vfi.Invoices.FirstOrDefault(i => i.ExportId == exportDetail.ExportId);
                        var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == exportDetail.ExportId);
                        if (!export.ExportFormTP_KDDetail.Any(ti => ti.IsInvoiced == true))
                            invoice.Status = (byte)MyUtilities.Sales.Status.Waiting;
                        else
                            invoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                    }
                    vfi.SaveChanges();
                }
                return View(new GridModel(PrepareTaxInvoice().OrderByDescending(o => o.ModifiedDate)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("ApproveTaxInvoice", ex.Message);
                return View(new GridModel(new List<TaxInvoiceModel>()));
            }
        }
        public ActionResult SelectTaxInvoiceNotComplete() {
            using (var vfi = new tammaContext()) {
                var taxInvoices = vfi.TaxInvoices.Where(ti => ti.Status == (byte)MyUtilities.Sales.Status.InProcess);
                return new JsonResult {
                    Data = new SelectList(taxInvoices.ToList(), "Id", "TaxInvoiceList"),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet

                };
            }
        }

        [GridAction]
        public ActionResult SelectTaxInvoice(int customerId) {
            try {
                var model = new List<TaxInvoiceModel>();
                if (customerId == 0)
                    return View(new GridModel(model));
                //if (string.IsNullOrWhiteSpace(taxInvoiceText))
                //       return View(new GridModel(model));
                using (var vfi = new tammaContext()) {
                    var taxInvoices =
                        vfi.TaxInvoices.Where(
                            ti =>
                            (ti.CustomerId == customerId) &&
                            ti.SetupDate != null &&
                            ti.SetupDate >= MyUtilities.Sales.StartTaxInvoiceDate &&
                            (ti.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                             ti.Status != (byte)MyUtilities.Sales.Status.Completed));
                    foreach (var taxInvoice in taxInvoices) {
                        var entity = new TaxInvoiceModel {
                            TaxInvoiceList = taxInvoice.TaxInvoiceList,
                            TaxInvoiceId = taxInvoice.Id,
                            ModifiedDate = taxInvoice.ModifiedDate ?? DateTime.Now,
                            Tax = taxInvoice.TaxPercent,
                            ExchangeRate = taxInvoice.ExchangeRate,
                            CurrencyCode = taxInvoice.Currency,
                            SetupDate = taxInvoice.SetupDate,
                            CustomerCode = taxInvoice.Customer.CustomerCode,
                            TotalQuantity = taxInvoice.TaxInvoiceProducts.Sum(tip => tip.Quantity),
                            TotalAmount = taxInvoice.TaxInvoiceProducts.Sum(tip => Math.Round(tip.Quantity * tip.UnitPrice, 2))
                        };
                        entity.TotalAmount += (entity.TotalAmount * taxInvoice.TaxPercent / 100);
                        entity.RequiredAmount = entity.TotalAmount ?? 0;
                        var taxInvoiceDetailAdd =
                            vfi.TaxInvoiceDetails.Where(
                                tid =>
                                tid.TaxInvoiceId == taxInvoice.Id && tid.Money > 0 &&
                                tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        if (taxInvoiceDetailAdd.Any())
                            entity.RequiredAmount -= (taxInvoiceDetailAdd.Sum(tid => tid.Money) ?? 0.0);
                        var taxInvoiceDetailReduce =
                            vfi.TaxInvoiceDetails.Where(
                                tid =>
                                tid.TaxInvoiceId == taxInvoice.Id && tid.Money < 0 &&
                                tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        if (taxInvoiceDetailReduce.Any())
                            entity.RequiredAmount += (taxInvoiceDetailReduce.Sum(tid => tid.Money) ?? 0.0);
                        if (entity.CurrencyCode.Equals("VND")) {
                            entity.TotalAmountVND = (entity.TotalAmount ?? 0);
                            entity.RequiredAmountVND = entity.RequiredAmount;
                        }
                        else {
                            entity.TotalAmountVND = (entity.TotalAmount ?? 0) * entity.ExchangeRate;
                            entity.RequiredAmountVND = entity.RequiredAmount * entity.ExchangeRate;
                        }
                        model.Add(entity);
                    }
                }
                return View(new GridModel(model));
            }
            catch (Exception ex) {
                ModelState.AddModelError("ApproveTaxInvoice", ex.Message);
                return View(new GridModel(new List<TaxInvoiceModel>()));
            }
        }

        [GridAction]
        public ActionResult SelectTaxInvoiceDetail(int[] ids) {
            // int[] arr_ids = ids.Split(':'); 
            using (var vfi = new tammaContext()) {
                var importTaxInvoice =
                    (List<TaxInvoiceDetailModel>)Session["SessionTaxInvoice"];
                if (importTaxInvoice == null)
                    importTaxInvoice = new List<TaxInvoiceDetailModel>();
                //var model = new List<TaxInvoiceDetailModel>();
                if (ids == null)
                    return View(new GridModel(importTaxInvoice));
                foreach (var id in ids) {
                    TaxInvoiceDetailModel entity = null;
                    if (importTaxInvoice.Any())
                        entity = importTaxInvoice.FirstOrDefault(ti => ti.TaxInvoiceId == id);
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == id);
                    if (taxInvoice == null)
                        throw new AggregateException("Lỗi không tìm thấy chứng từ");
                    if (entity == null) {
                        entity = new TaxInvoiceDetailModel {
                            TaxInvoiceId = taxInvoice.Id,
                            ModifiedDate = taxInvoice.ModifiedDate.Value,
                            CurrencyCode = taxInvoice.Currency,
                            Money = 0,
                            TotalAmount =
                               taxInvoice.TaxInvoiceProducts.Sum(tip => Math.Round(tip.Quantity * tip.UnitPrice, 2))
                        };
                        entity.TotalAmount += (entity.TotalAmount * taxInvoice.TaxPercent / 100);
                        entity.RequiredAmount = entity.TotalAmount;
                        importTaxInvoice.Add(entity);
                    }
                    entity.TaxInvoiceList = taxInvoice.TaxInvoiceList;
                    var taxInvoiceDetailAdd =
                             vfi.TaxInvoiceDetails.Where(
                                 tid =>
                                 tid.TaxInvoiceId == taxInvoice.Id && tid.Money > 0 &&
                                 tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                    if (taxInvoiceDetailAdd.Any())
                        entity.RequiredAmount -= (taxInvoiceDetailAdd.Sum(tid => tid.Money) ?? 0.0);
                    var taxInvoiceDetailReduce =
                        vfi.TaxInvoiceDetails.Where(
                            tid =>
                            tid.TaxInvoiceId == taxInvoice.Id && tid.Money < 0 &&
                            tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                    if (taxInvoiceDetailReduce.Any())
                        entity.RequiredAmount += (taxInvoiceDetailReduce.Sum(tid => tid.Money) ?? 0.0);
                }
                Session["SessionTaxInvoice"] = importTaxInvoice;

                return View(new GridModel(importTaxInvoice));
            }
        }

        [GridAction]
        public ActionResult PushTaxInvoiceDetail(
            [Bind(Prefix = "inserted")] IEnumerable<TaxInvoiceDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<TaxInvoiceDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<TaxInvoiceDetailModel> deletedDetails,
            string importDate
            ) {
            if (updatedDetails != null) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    using (var vfi = new tammaContext()) {
                        var ci = new CultureInfo("vi-VN");
                        var iDate = string.IsNullOrWhiteSpace(importDate)
                                        ? DateTime.Now
                                        : Convert.ToDateTime(importDate, ci);

                        foreach (var detail in updatedDetails) {
                            var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == detail.TaxInvoiceId);
                            if (taxInvoice == null)
                                throw new ArgumentException("Error liên hệ admin!" + detail.TaxInvoiceList);

                            if (detail.Money <= 0) continue;
                            var moneyReceived = 0.0;
                            var taxInvoiceDetailAdd =
                                taxInvoice.TaxInvoiceDetails.Where(
                                    tid =>
                                    tid.TaxInvoiceId == detail.TaxInvoiceId &&
                                    tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                            if (taxInvoiceDetailAdd.Any())
                                moneyReceived = Math.Round(taxInvoiceDetailAdd.Sum(tid => Math.Abs(tid.Money.Value)), 2);
                            var total = taxInvoice.TaxInvoiceProducts.Sum(tip => Math.Round(tip.Quantity * tip.UnitPrice, 2));
                            total += Math.Round((total * taxInvoice.TaxPercent / 100));
                            if (Math.Round((total - moneyReceived - detail.Money).Value, 2) < 0)
                                throw new ArgumentException("Số tiền không được lớn hơn số nợ." + detail.TaxInvoiceList);
                            var entity = new TaxInvoiceDetail {
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                Money = Math.Round(detail.Money.Value, 2),
                                TaxInvoiceId = detail.TaxInvoiceId,
                                Note = detail.Note,
                                Times = taxInvoiceDetailAdd.Count() + 1,
                                Status = (byte)MyUtilities.Sales.Status.Waiting,
                                ImportDate = iDate,
                            };
                            vfi.TaxInvoiceDetails.Add(entity);
                        }
                        vfi.SaveChanges();
                        Session["SessionTaxInvoice"] = new List<TaxInvoiceDetailModel>();
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("PushTaxInvoice", ex.Message);
                }
            }
            return View(new GridModel(new List<TaxInvoiceModel>()));
        }

        [GridAction]
        public ActionResult ReduceTaxInvoiceDetail(
            [Bind(Prefix = "inserted")] IEnumerable<TaxInvoiceDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<TaxInvoiceDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<TaxInvoiceDetailModel> deletedDetails,
            string importDate
            ) {
            if (updatedDetails != null) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    using (var vfi = new tammaContext()) {
                        var ci = new CultureInfo("vi-VN");
                        var iDate = string.IsNullOrWhiteSpace(importDate)
                                        ? DateTime.Now
                                        : Convert.ToDateTime(importDate, ci);
                        foreach (var detail in updatedDetails) {
                            var taxInvoiceDetailReduce =
                                vfi.TaxInvoiceDetails.Where(
                                    tid => tid.TaxInvoiceId == detail.TaxInvoiceId && tid.Money < 0);
                            var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == detail.TaxInvoiceId);
                            if (taxInvoice == null)
                                throw new ArgumentException("Error liên hệ admin!" + detail.TaxInvoiceList);
                            var max = ((taxInvoice.TotalAmount * taxInvoice.TaxPercent) + taxInvoice.TotalAmount) ?? 0;

                            if (detail.Money <= 0) continue;
                            var moneyReceive = 0.0;
                            var taxInvoiceDetailAdd =
                                taxInvoice.TaxInvoiceDetails.Where(
                                    tid =>
                                    tid.TaxInvoiceId == detail.TaxInvoiceId &&
                                    tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                            if (taxInvoiceDetailAdd.Any())
                                moneyReceive = Math.Round(taxInvoiceDetailAdd.Sum(tid => Math.Abs(tid.Money.Value)), 2);
                            if (detail.Money + moneyReceive > Math.Round(max, 2))
                                throw new ArgumentException("Số tiền không được lớn hơn số nợ." + detail.TaxInvoiceList);
                            var entity = new TaxInvoiceDetail {
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                Money = (-detail.Money),
                                TaxInvoiceId = detail.TaxInvoiceId,
                                Note = detail.Note,
                                Times = taxInvoiceDetailReduce.Count() + 1,
                                Status = (byte)MyUtilities.Sales.Status.Waiting,
                                ImportDate = iDate,
                            };
                            taxInvoice.TaxInvoiceDetails.Add(entity);
                            var details = taxInvoice.TaxInvoiceDetails;
                        }
                        vfi.SaveChanges();
                        Session["SessionTaxInvoice"] = new List<TaxInvoiceDetailModel>();
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("ReduceTaxInvoice", ex.Message);
                }
            }
            return View(new GridModel(new List<TaxInvoiceModel>()));
        }
        List<TaxInvoiceDetailModel> TaxInvoideDetailNeedApproves() {
            var model = new List<TaxInvoiceDetailModel>();
            using (var vfi = new tammaContext()) {
                var taxInvoiceDetails = vfi.TaxInvoiceDetails.Where(tid => tid.Status == (byte)MyUtilities.Sales.Status.Waiting);
                foreach (var taxInvoiceDetail in taxInvoiceDetails) {
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == taxInvoiceDetail.TaxInvoiceId);
                    //var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == taxInvoice.InvoiceId);
                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == invoice.OrderId);
                    var entity = new TaxInvoiceDetailModel {
                        TaxInvoiceList = taxInvoice.TaxInvoiceList,
                        TaxInvoiceId = taxInvoice.Id,
                        Tax = taxInvoice.TaxPercent,
                        CurrencyCode = taxInvoice.Currency,
                        TotalAmount = taxInvoice.TaxInvoiceProducts.Sum(tip => Math.Round(tip.Quantity * tip.UnitPrice, 2)),
                        Money = taxInvoiceDetail.Money,
                        ImportDate = taxInvoiceDetail.ImportDate.Value,
                        Times = taxInvoiceDetail.Times,
                        Note = taxInvoiceDetail.Note,
                        ModifiedUser = taxInvoiceDetail.ModifiedUser,
                        ModifiedDate = taxInvoiceDetail.ModifiedDate ?? DateTime.Now,
                        DetailId = taxInvoiceDetail.Id,
                    };
                    entity.TotalAmount += (entity.TotalAmount * taxInvoice.TaxPercent / 100);
                    entity.RequiredAmount = entity.TotalAmount;
                    var taxInvoiceDetailAdd =
                        vfi.TaxInvoiceDetails.Where(
                            tid =>
                            tid.TaxInvoiceId == taxInvoice.Id && tid.Money > 0 &&
                            tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                    if (taxInvoiceDetailAdd.Any())
                        entity.RequiredAmount -= (taxInvoiceDetailAdd.Sum(tid => tid.Money) ?? 0.0);
                    var taxInvoiceDetailReduce =
                        vfi.TaxInvoiceDetails.Where(
                            tid =>
                            tid.TaxInvoiceId == taxInvoice.Id && tid.Money < 0 &&
                            tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                    if (taxInvoiceDetailReduce.Any())
                        entity.RequiredAmount += (taxInvoiceDetailReduce.Sum(tid => tid.Money) ?? 0.0);
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectTaxInvoideDetailNeedApprove() {
            return View(new GridModel(TaxInvoideDetailNeedApproves()));
        }

        [HttpPost]
        public ActionResult ApproveTaxInvoice(long[] checkedRecords) {
            try {

                if (!Request.IsAuthenticated)
                    return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                var modifiedUser = HttpContext.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(modifiedUser))
                    return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
                if (checkedRecords.Count() <= 0)
                    return Json(@"Vui lòng chọn ít nhất 1 lệnh. (checkedRecords <= 0). ");

                //tam ma
                using (var vfi = new tammaContext()) {
                    foreach (var detailId in checkedRecords) {
                        var taxInvoiceDetail = vfi.TaxInvoiceDetails.FirstOrDefault(tid => tid.Id == detailId);
                        if (taxInvoiceDetail.Status != (byte)MyUtilities.Transaction.Status.Open) continue;
                        var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == taxInvoiceDetail.TaxInvoiceId);
                        var taxInvoideDetails =
                            vfi.TaxInvoiceDetails.Where(
                                tid =>
                                tid.TaxInvoiceId == taxInvoice.Id &&
                                tid.Status == (byte)MyUtilities.Sales.Status.Completed);
                        var totalMoneyHasAdd = Math.Round(Math.Abs(taxInvoiceDetail.Money.Value), 2);
                        if (taxInvoideDetails.Any())
                            totalMoneyHasAdd += Math.Round(taxInvoideDetails.Sum(tid => Math.Abs(tid.Money.Value)), 2);
                        var total = taxInvoice.TaxInvoiceProducts.Sum(tip => Math.Round(tip.Quantity * tip.UnitPrice, 2));
                        total = total + (total * taxInvoice.TaxPercent / 100) - totalMoneyHasAdd;
                        if (taxInvoice.Currency.Equals("VND")) {
                            if ((Math.Round(total, 0) < 0))
                                return Json(@"Tien can duyet vuot qua so tien can thiet ");
                        }
                        else {
                            if ((Math.Round(total, 2) < 0))
                                return Json(@"Tien can duyet vuot qua so tien can thiet ");
                        }

                        taxInvoiceDetail.Status = (byte)MyUtilities.Sales.Status.Completed;

                        vfi.SaveChanges();
                        if (Math.Round(total, 2) == 0) {
                            if (taxInvoice.TaxInvoiceProducts.FirstOrDefault(tip => !tip.IsFinish) == null) {
                                taxInvoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                                taxInvoice.FinishDate =
                                    taxInvoice.TaxInvoiceDetails.OrderByDescending(od => od.ImportDate)
                                        .FirstOrDefault()
                                        .ImportDate;
                            }
                            else {
                                taxInvoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                            }
                            vfi.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception exception) {
                return Json(@"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }
            return Json("okie");
        }

        [GridAction]
        public ActionResult CancelTaxInvoideDetailNeedApprove(int detailId) {
            using (var vfi = new tammaContext()) {
                var taxInvoideDetail = vfi.TaxInvoiceDetails.FirstOrDefault(tid => tid.Id == detailId);
                taxInvoideDetail.Status = (byte)MyUtilities.Sales.Status.Cancel;
                vfi.SaveChanges();
            }
            return View(new GridModel(TaxInvoideDetailNeedApproves()));
        }

        public ActionResult SelectInvoiceByType(int type) {
            using (var vfi = new tammaContext()) {
                var models = vfi.Invoices.Where(i => i.Active).ToList();
                if (type == 1)
                    models =
                        models.Where(
                            i =>
                            i.Status == (byte)MyUtilities.Sales.Status.Waiting || i.Status == (byte)MyUtilities.Sales.Status.InProcess)
                              .ToList();
                return new JsonResult {
                    Data = new SelectList(models, "InvoiceId", "InvoiceNumber")
                };
            }
        }

        public ActionResult SelectAutofillExportTp(string transporter) {
            using (var vfi = new tammaContext()) {
                var export = vfi.ExportFormTP_KD.Where(e => e.Transporter.Equals(transporter)).ToList().LastOrDefault();
                if (export == null)
                    return new JsonResult();
                var data = new List<string>
                    {
                        export.CompanyTransporter + "",
                        export.CarNumber + "", 
                    };
                return new JsonResult {
                    Data = data
                };
            }
        }

        public ActionResult SelectOrderOrOrderNote(int type) {
            using (var vfi = new tammaContext()) {
                if (type == 1) {
                    var orderModels =
                        vfi.Orders.Where(
                            o =>
                            o.DueDate != null && o.Active &&
                            (o.Status == (byte)MyUtilities.Sales.Status.Waiting || o.Status == (byte)MyUtilities.Sales.Status.InProcess))
                           .ToList();
                    return new JsonResult {
                        Data = new SelectList(orderModels, "OrderId", "OrderNumber")
                    };
                }
                else if (type == 2) {
                    var models =
                        vfi.OrderNotes.Where(
                            on =>
                            on.NoteType == 2 && on.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                            !(on.IsComplete ?? false))
                           .ToList();
                    return new JsonResult {
                        Data = new SelectList(models, "NoteId", "NoteNumber")
                    };
                }
            }
            return new JsonResult {
                Data = new SelectList(null)
            };

        }
        public ActionResult SelectOrderNoteToChange() {
            using (var vfi = new tammaContext()) {
                var models =
                    vfi.OrderNotes.Where(
                        on => on.NoteType == 2 && on.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved).ToList();
                return new JsonResult {
                    Data = new SelectList(models, "NoteId", "NoteNumber")
                };
            }
        }
        List<OrderNoteModel> PrepareOrderNote() {
            var model = new List<OrderNoteModel>();
            using (var vfi = new tammaContext()) {
                var orderNotes = vfi.OrderNotes.Where(on => on.Status == (byte)MyUtilities.Sales.Status.Waiting);
                foreach (var orderNote in orderNotes) {
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceId == orderNote.InvoiceId);
                    var entity = new OrderNoteModel {
                        NoteId = orderNote.NoteId,
                        CreatedDate = orderNote.CreatedDate,
                        ModifiedDate = orderNote.ModifiedDate,
                        ModifiedUser = orderNote.ModifiedUser,
                        NoteNumber = orderNote.NoteNumber,
                        StatusName = MyUtilities.Sales.GetText(Convert.ToInt16(orderNote.Status)),
                        TypeName = orderNote.NoteType == 1 ? "Trả hàng" : "Đổi hàng",
                        InvoiceNumber = invoice.InvoiceNumber
                    };
                    entity.TotalNumber = orderNote.OrderNoteDetails.Sum(ond => ond.Quantity).Value;
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectOrderNoteByStatus(string fromDate, string toDate, int? type, int? status) {
            var model = new List<OrderNoteModel>();
            if (type == 0 || type == null)
                return View(new GridModel(model));
            var ci = new CultureInfo("vi-VN");
            DateTime toDay = DateTime.Now;

            var fDate = string.IsNullOrWhiteSpace(fromDate)
                            ? new DateTime(toDay.Year, toDay.Month, 1)
                            : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                            ? toDay
                            : Convert.ToDateTime(toDate, ci);

            using (var vfi = new tammaContext()) {
                var orderNotes = vfi.OrderNotes.ToList();
                orderNotes = orderNotes.Where(on => on.NoteType == type).ToList();
                orderNotes = orderNotes.Where(on => on.CreatedDate <= tDate).ToList();
                orderNotes = orderNotes.Where(on => on.CreatedDate >= fDate).ToList();
                if (status != 0 && status != null)
                    orderNotes = orderNotes.Where(on => on.Transaction.Status == status).ToList();
                foreach (var orderNote in orderNotes) {
                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderNote.Invoice.OrderId);

                    var entity = new OrderNoteModel {
                        InvoiceNumber = orderNote.Invoice.InvoiceNumber,
                        CreatedDate = orderNote.CreatedDate,
                        ModifiedDate = orderNote.ModifiedDate,
                        ModifiedUser = orderNote.ModifiedUser,
                        NoteNumber = orderNote.NoteNumber,
                        TypeName = orderNote.NoteType == 1 ? "Trả hàng" : "Đổi hàng",
                        StatusName =
                            MyUtilities.Transaction.CastText.GetTextStatus(
                                orderNote.Transaction.Status),
                        TotalNumber = orderNote.Transaction.TransactionDetails.Sum(td => td.Quantity),
                        NoteId = orderNote.NoteId,
                        //OrderNumber = order.OrderNumber,
                        CustomerCode = orderNote.Invoice.Customer.CustomerCode
                    };
                    model.Add(entity);
                }
            }
            return View(new GridModel(model.OrderByDescending(m => m.CreatedDate)));
        }

        [GridAction]
        public ActionResult SelectOrderNoteDetailDetail(int noteId) {
            var model = new List<OrderNoteDetailModel>();
            using (var vfi = new tammaContext()) {
                var orderNote = vfi.OrderNotes.FirstOrDefault(on => on.NoteId == noteId);
                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == orderNote.TransactionId);
                if (transaction != null)
                    foreach (var detail in orderNote.OrderNoteDetails) {
                        var product = vfi.Products.FirstOrDefault(p => p.ProductId == detail.ProductId);
                        var entity = new OrderNoteDetailModel {
                            ProductCode = product.ProductCode,
                            Quantity = detail.Quantity.Value,
                            CustomerCode = product.Customer.CustomerCode,
                            //Weight = detail.QuantityKg.Value,
                            Note = detail.Note,
                        };
                        model.Add(entity);
                    }
            }
            return View(new GridModel(model));
        }
        #endregion
    }
}
