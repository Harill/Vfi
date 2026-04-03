using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
//using Vfi.Server.Core.DataModel.Models.Inv;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Sales.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
//using Order = Vfi.Server.Core.DataModel.BaseEntities.Order;
//using OrderDetail = Vfi.Server.Core.DataModel.BaseEntities.OrderDetail;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Controllers;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Models;
using Vfi.Ui.Mvc.Vfi.Controllers.Production;

namespace Vfi.Ui.Mvc.Vfi.Areas.Sales.Controllers {
    public class SalesOrderController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        private readonly WarehouseController _warehouseController;
        private readonly ProductController _productController;
        [InjectionConstructor]
        public SalesOrderController(IUnitOfWork unitOfWork, WarehouseController warehouseController, ProductController productController) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
            _warehouseController = warehouseController;
            _productController = productController;
        }

        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }
            return ViewData;
        }
        #region view
        public ActionResult CreateSalesOrder() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult CreateQuoteForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ApproveQuoteForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult QuoteFormManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult CreateTaxInvoice() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult CreateTaxInvoiceAuto() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult AddTaxInvoiceProductDetail() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ForecastOrderManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ForecastOrderApprove() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ForecastOrderCreate() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult SalesOrderProgress() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ForecastOrderProduction() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ForecastOrderCnc() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ForecastOrderProduction2() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ForecastOrderPlating() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ForecastOrderQc() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ForecastOrderTotal() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ForecastOrderProgress() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult TrackingOrderProgress() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MOQTemplateManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        #endregion

        #region order progress
        public ActionResult GetSalesOrderProgressDiv(int customerId) {
            var model = new List<ProductState>();
            try {
                using (var vfi = new tammaContext()) {
                    //var products = vfi.Products.Where(p => p.Active);
                    var orderDetails = from od in vfi.OrderDetails
                                       orderby od.Order.DueDate
                                       where
                                       (customerId == 0 || customerId == od.Order.CustomerId) &&
                                       od.RequiedNumber > 0 &&
                                       (od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess ||
                                        od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting) &&
                                       od.Order.DueDate != null
                                       select od;
                    //var customers = orderDetails.Select(od => od.Order.Customer).Distinct();
                    var warehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                    //foreach (var customer in customers)
                    //{
                    //var orderDetailsById = orderDetails.Where(od => od.Order.CustomerId == customer.CustomerId);
                    //if (!orderDetails.Any())
                    //    continue;
                    var products = orderDetails.Select(od => od.Product).Distinct();
                    foreach (var product in products) {
                        var entity = new ProductState {
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.Customer.CustomerCode,
                            Details = new List<OrderProgressDetailModel>(),
                        };
                        var orderDetailsByProductId = orderDetails.Where(od => od.ProductId == product.ProductId);
                        entity.ToDate = orderDetailsByProductId.FirstOrDefault().Order.DueDate.Value;
                        var totalInvQty = 0.0;
                        var finishInvQty = 0.0;
                        var totalInv =
                            vfi.ProductInventories.Where(
                                pi =>
                                    pi.ProductId == entity.ProductId &&
                                    warehouses.Contains(pi.WarehouseId));
                        if (totalInv.Any()) {
                            totalInvQty = totalInv.Sum(pi => pi.TotalQty);
                            var finishInv = totalInv.FirstOrDefault(pi => pi.WarehouseId == MyUtilities.Warehouse.Finish);
                            if (finishInv != null)
                                finishInvQty = finishInv.TotalQty;
                        }
                        foreach (var orderDetail in orderDetailsByProductId) {
                            var detail = new OrderProgressDetailModel() {
                                OrderDetailId = orderDetail.OrderDetailId,
                                OrderDueDate = orderDetail.Order.DueDate.Value,
                                StartDate = orderDetail.Order.ModifiedDate.Value,
                                StartDateString = orderDetail.Order.ModifiedDate.Value.ToString("dd/MM"),
                                OrderDueDateString = orderDetail.Order.DueDate.Value.ToString("dd/MM"),
                                OrderQuantity = orderDetail.RequiedNumber,
                                WarehouseInv = finishInvQty,
                                TotalInv = totalInvQty,
                            };
                            if (detail.StartDate < entity.ToDate)
                                entity.ToDate = detail.StartDate.Value;
                            finishInvQty -= detail.OrderQuantity;
                            if (finishInvQty < 0)
                                finishInvQty = 0;
                            if (detail.WarehouseInv > detail.OrderQuantity)
                                detail.Percent = 100;
                            else
                                detail.Percent = Convert.ToInt32((detail.WarehouseInv / detail.OrderQuantity) * 100);
                            if (detail.Type > entity.Type)
                                entity.Type = detail.Type;
                            if (detail.OrderDueDate > entity.ToDate)
                                entity.ToDate = detail.OrderDueDate;
                            entity.Details.Add(detail);
                        }
                        var processes =
                            vfi.ProductionProcesses.Where(pp => pp.IsNecessary && pp.ProductId == entity.ProductId);
                        entity.ProductionStatus = processes.Count();
                        model.Add(entity);
                    }
                }
                return PartialView("SalesOrderProgressDiv",
                    //model.OrderByDescending(m => m.Type)
                    //    .ThenBy(m => m.ToDate)
                    //    .ThenBy(m => m.CustomerCode)
                    //    .ThenBy(m => m.ProductCode)
                    //    .ToList());  

                    model.OrderByDescending(m => m.ToDate)
                        .ThenByDescending(m => m.Type)
                        .ThenBy(m => m.CustomerCode)
                        .ThenBy(m => m.ProductCode)
                        .ToList()); 


            }
            catch (Exception) {
                return null;
            }

        }

        public ActionResult GetForecastOrderProgressDiv(int customerId, int? month, int? year, int status) {
            if (month == null || year == null) {
                month = DateTime.Now.Month;
                year = DateTime.Now.Year;
            }
            var startDate = new DateTime(year.Value, month.Value, 1);
            var model = new List<ForecastModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var forecastDetails = from od in vfi.ForecastOrders
                                          orderby od.ForecastDate
                                          where
                                          od.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                          od.ForecastDate.Month == month &&
                                          od.ForecastDate.Year == year &&
                                          (customerId == 0 || od.Product.CustomerId == customerId)
                                          select od;
                    var orderDetailsLate = from od in vfi.OrderDetails
                                           where
                                           od.RequiedNumber > 0 &&
                                           od.Order.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                           od.Order.DueDate < startDate &&
                                           (customerId == 0 || od.Product.CustomerId == customerId)
                                           select od;
                    var orderDetails = from od in vfi.OrderDetails
                                       where
                                       od.RequiedNumber > 0 &&
                                       od.Order.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                       od.Order.DueDate.Value.Month == month &&
                                       od.Order.DueDate.Value.Year == year &&
                                       (customerId == 0 || od.Product.CustomerId == customerId)
                                       select od;
                    var products = forecastDetails.Select(fo => fo.Product).Distinct();
                    var warehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                    foreach (var product in products) {
                        var entity = new ForecastModel {
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.Customer.CustomerCode,
                            Details = new List<ForecastDetailModel>(),
                        };
                        var forecastByIds = forecastDetails.Where(fod => fod.ProductId == entity.ProductId);
                        entity.ForecastDate = forecastByIds.FirstOrDefault().ForecastDate;
                        var totalInvQty = 0.0;
                        var finishInvQty = 0.0;
                        var orderByIds = orderDetailsLate.Where(od => od.ProductId == entity.ProductId);
                        if (orderByIds.Any()) {
                            entity.LastMonthQuantity = orderByIds.Sum(od => od.RequiedNumber);
                        }
                        orderByIds = orderDetails.Where(od => od.ProductId == entity.ProductId);
                        if (orderByIds.Any()) {
                            entity.Quantity = orderByIds.Sum(od => od.RequiedNumber);
                        }
                        var totalInv =
                            vfi.ProductInventories.Where(
                                pi =>
                                    pi.ProductId == entity.ProductId &&
                                    warehouses.Contains(pi.WarehouseId));
                        if (totalInv.Any()) {
                            totalInvQty = totalInv.Sum(pi => pi.TotalQty);
                            var finishInv = totalInv.FirstOrDefault(pi => pi.WarehouseId == MyUtilities.Warehouse.Finish);
                            if (finishInv != null)
                                finishInvQty = finishInv.TotalQty;
                        }
                        foreach (var forecast in forecastByIds) {
                            var detail = new ForecastDetailModel() {
                                ForecastOrderId = forecast.ForecastOrderId,
                                EndDate = forecast.ForecastDate,
                                StartDate = forecast.ModifiedDate,
                                ForecastQuantity = forecast.Quantity,
                                WarehouseInv = finishInvQty,
                                TotalInv = totalInvQty,
                                ForecastDate = forecast.ForecastDate
                            };
                            var exportQuantity = 0.0;
                            var export = vfi.ProductInventoryPeriods.Where(
                                pip =>
                                    pip.WarehouseId == (byte)MyUtilities.Warehouse.Business &&
                                    pip.ProductId == product.ProductId &&
                                    pip.PeriodDate >= startDate && pip.PeriodDate < detail.ForecastDate);
                            if (export.Any())
                                exportQuantity = export.Sum(e => e.LastPeriodQuantity - e.EarlyPeriodQuantity);
                            // SL phân tích
                            var processDetails =
                                GetForecastProcessDetail(forecast.ForecastOrderId, true);
                            // SL lưu
                            var processDetails2 =
                                GetForecastProcessDetail(forecast.ForecastOrderId, false);
                            foreach (var processDetail2 in processDetails2) {
                                var processDetail =
                                    processDetails.FirstOrDefault(pd => pd.ProcessId == processDetail2.ProcessId);
                                if (processDetail != null) {
                                    // sl phân tích trừ bớt phần đã xuất kho trong tháng
                                    if (processDetail.ForecastQuantity - exportQuantity >
                                        processDetail2.ForecastQuantity) {
                                        detail.IsWorking = true;
                                        break;
                                    }
                                }
                            }
                            finishInvQty -= detail.ForecastQuantity;
                            if (finishInvQty < 0)
                                finishInvQty = 0;
                            if (detail.WarehouseInv > detail.ForecastQuantity)
                                detail.Percent = 100;
                            else
                                detail.Percent = Convert.ToInt32((detail.WarehouseInv / detail.ForecastQuantity) * 100);
                            if (detail.Type > entity.Type)
                                entity.Type = detail.Type;
                            if (status != -9 && entity.Type != status)
                                break;
                            entity.Details.Add(detail);
                            startDate = detail.ForecastDate.Value;
                        }
                        if (status != -9 && entity.Type != status)
                            continue;
                        model.Add(entity);
                    }
                }
                return PartialView("ForecastOrderProgressDiv",
                    model.OrderByDescending(m => m.Type)
                        .ThenBy(m => m.ForecastDate)
                        .ThenBy(m => m.CustomerCode)
                        .ThenBy(m => m.ProductCode)
                        .ToList());
            }
            catch (Exception) {
                return null;
            }
        }

        [GridAction]
        public ActionResult SelectSalesOrderProgressDetail(int orderDetailId) {
            var model = new List<OrderProgressDetailModel>();

            try {
                model = GetSalesOrderProgressDetail(orderDetailId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectSalesOrderProgressDetail", "" + ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<OrderProgressDetailModel> GetSalesOrderProgressDetail(int orderDetailId) {
            var model = new List<OrderProgressDetailModel>();
            using (var vfi = new tammaContext()) {
                var orderDetail = vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailId);
                var required = 0.0;
                // don hang da tre
                if (orderDetail.Order.DueDate < DateTime.Now)
                    return model;
                var endDate = orderDetail.Order.DueDate.Value;
                var processes =
                    vfi.ProductionProcesses.Where(pp => pp.IsNecessary && pp.ProductId == orderDetail.ProductId);
                var warehouseInvs =
                    vfi.ProductInventories.Where(
                        pi => pi.ProductId == orderDetail.ProductId);
                required = orderDetail.RequiedNumber;
                var olderOrderDetails =
                    vfi.OrderDetails.Where(od => od.ProductId == orderDetail.ProductId && od.RequiedNumber > 0 &&
                                                 od.OrderDetailId != orderDetailId &&
                                                 (od.Order.DueDate < orderDetail.Order.DueDate ||
                                                  (od.Order.DueDate == orderDetail.Order.DueDate &&
                                                   od.Order.ModifiedDate < orderDetail.Order.ModifiedDate))
                                                 &&
                                                 (od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess ||
                                                  od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting));
                var required2 = 0;
                if (olderOrderDetails.Any())
                    required2 = olderOrderDetails.Sum(od => od.RequiedNumber);
                //tp
                var process = processes.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.Finish);
                var warehouseInv = warehouseInvs.FirstOrDefault(pi => pi.WarehouseId == MyUtilities.Warehouse.Finish);
                if (process != null) {
                    var detail = new OrderProgressDetailModel {
                        Index = process.ProcessIndex,
                        OrderDetailId = orderDetailId,
                        ProcessName = process.Warehouse.ShortName,
                        OrderRequired = orderDetail.RequiedNumber,
                        //StartDate = endDate.AddDays(-1),
                        CompleteDate = endDate,
                    };
                    endDate = endDate.AddDays(-1);
                    detail.StartDate = endDate;
                    if (warehouseInv != null)
                        detail.WarehouseInv = warehouseInv.TotalQty - required2;
                    if (detail.WarehouseInv < 0)
                        detail.WarehouseInv = 0;
                    required -= detail.WarehouseInv;
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                process =
                    processes.FirstOrDefault(
                        pp => pp.WarehouseId == MyUtilities.Warehouse.QcA || pp.WarehouseId == MyUtilities.Warehouse.QcB);
                var warehouseInvss =
                    warehouseInvs.Where(
                        pi => pi.WarehouseId == MyUtilities.Warehouse.QcA || pi.WarehouseId == MyUtilities.Warehouse.QcB);
                if (required > 0 && process != null) {
                    var detail = new OrderProgressDetailModel {
                        Index = process.ProcessIndex,
                        OrderDetailId = orderDetailId,
                        ProcessName = process.Warehouse.ShortName,
                        OrderRequired = required,
                        //StartDate = endDate.AddDays(-1),
                        CompleteDate = endDate,
                    };
                    endDate = endDate.AddDays(-1);
                    detail.StartDate = endDate;
                    if (warehouseInvss.Any())
                        detail.WarehouseInv = warehouseInvss.Sum(pi => pi.TotalQty) - required2;
                    if (detail.WarehouseInv < 0)
                        detail.WarehouseInv = 0;
                    required -= detail.WarehouseInv;
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                process =
                    processes.FirstOrDefault(
                        pp => pp.WarehouseId == MyUtilities.Warehouse.WaitingPlating);
                if (required > 0 && process != null) {
                    var detail = new OrderProgressDetailModel {
                        Index = process.ProcessIndex,
                        OrderDetailId = orderDetailId,
                        ProcessName = process.Warehouse.ShortName,
                        OrderRequired = required,
                        //StartDate = endDate.AddDays(-7),
                        CompleteDate = endDate,
                    };
                    endDate = endDate.AddDays(-7);
                    detail.StartDate = endDate;
                    warehouseInv =
                        warehouseInvs.FirstOrDefault(pi => pi.WarehouseId == MyUtilities.Warehouse.WaitingPlating);
                    if (warehouseInv != null)
                        detail.WarehouseInv = warehouseInv.TotalQty - required2;
                    if (detail.WarehouseInv < 0)
                        detail.WarehouseInv = 0;
                    required -= detail.WarehouseInv;
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                process =
                    processes.FirstOrDefault(
                        pp => pp.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment);
                if (required > 0 && process != null) {
                    var detail = new OrderProgressDetailModel {
                        Index = process.ProcessIndex,
                        OrderDetailId = orderDetailId,
                        ProcessName = process.Warehouse.ShortName,
                        OrderRequired = required,
                        //StartDate = endDate.AddDays(-7),
                        CompleteDate = endDate,
                    };
                    endDate = endDate.AddDays(-1);
                    detail.StartDate = endDate;
                    warehouseInv =
                        warehouseInvs.FirstOrDefault(pi => pi.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment);
                    if (warehouseInv != null)
                        detail.WarehouseInv = warehouseInv.TotalQty - required2;
                    if (detail.WarehouseInv < 0)
                        detail.WarehouseInv = 0;
                    required -= detail.WarehouseInv;
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                process =
                    processes.FirstOrDefault(
                        pp => pp.WarehouseId == MyUtilities.Warehouse.HeatTreatment);
                if (required > 0 && process != null) {
                    var detail = new OrderProgressDetailModel {
                        Index = process.ProcessIndex,
                        OrderDetailId = orderDetailId,
                        ProcessName = process.Warehouse.ShortName,
                        OrderRequired = required,
                        //StartDate = endDate.AddDays(-7),
                        CompleteDate = endDate,
                    };
                    endDate = endDate.AddDays(-1);
                    detail.StartDate = endDate;
                    warehouseInv =
                        warehouseInvs.FirstOrDefault(pi => pi.WarehouseId == MyUtilities.Warehouse.HeatTreatment);
                    if (warehouseInv != null)
                        detail.WarehouseInv = warehouseInv.TotalQty - required2;
                    if (detail.WarehouseInv < 0)
                        detail.WarehouseInv = 0;
                    required -= detail.WarehouseInv;
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                process =
                    processes.FirstOrDefault(
                        pp => pp.WarehouseId == MyUtilities.Warehouse.Production2);
                warehouseInvss =
                    warehouseInvs.Where(
                        pi =>
                            pi.WarehouseId == MyUtilities.Warehouse.Production2 ||
                            pi.WarehouseId == MyUtilities.Warehouse.Production2B ||
                            pi.WarehouseId == MyUtilities.Warehouse.Production2C ||
                            pi.WarehouseId == MyUtilities.Warehouse.Production2D);
                if (required > 0 && process != null) {
                    var detail = new OrderProgressDetailModel {
                        Index = process.ProcessIndex,
                        OrderDetailId = orderDetailId,
                        ProcessName = process.Warehouse.ShortName,
                        OrderRequired = required,
                        //StartDate = endDate.AddDays(-7),
                        CompleteDate = endDate,
                    };
                    var day = MyUtilities.Function.RoundUp(
                        required / (72000 /
                                    orderDetail.Product.ProductionSections
                                        .Where(ps => ps.Active && ps.Productivity > 0)
                                        .Max(ps => ps.Productivity)), 0);
                    endDate = endDate.AddDays(day * -1);
                    detail.StartDate = endDate;
                    if (warehouseInvss.Any())
                        detail.WarehouseInv = warehouseInvss.Sum(pi => pi.TotalQty) - required2;
                    if (detail.WarehouseInv < 0)
                        detail.WarehouseInv = 0;
                    required -= detail.WarehouseInv;
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                process = processes.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.Cnc);
                if (required > 0 && process != null) {
                    var detail = new OrderProgressDetailModel {
                        Index = process.ProcessIndex,
                        OrderDetailId = orderDetailId,
                        ProcessName = process.Warehouse.ShortName,
                        OrderRequired = required,
                        //StartDate = endDate.AddDays(-7),
                        CompleteDate = endDate,
                    };
                    endDate = endDate.AddDays(-1);
                    detail.StartDate = endDate;
                    warehouseInv = warehouseInvs.FirstOrDefault(pi => pi.WarehouseId == MyUtilities.Warehouse.Cnc);
                    if (warehouseInv != null)
                        detail.WarehouseInv = warehouseInv.TotalQty - required2;
                    if (detail.WarehouseInv < 0)
                        detail.WarehouseInv = 0;
                    required -= detail.WarehouseInv;
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                process = processes.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.Production1);
                if (required > 0 && process != null) {
                    var detail = new OrderProgressDetailModel {
                        Index = process.ProcessIndex,
                        OrderDetailId = orderDetailId,
                        ProcessName = process.Warehouse.ShortName,
                        OrderRequired = required,
                        //StartDate = endDate.AddDays(-7),
                        CompleteDate = endDate,
                    };
                    var day = 1.0;
                    if (orderDetail.Product.Productivity != 0)
                        day = MyUtilities.Function.RoundUp(72000 / orderDetail.Product.Productivity.Value, 0);
                    endDate = endDate.AddDays(day * -1);
                    detail.StartDate = endDate;
                    model.Add(detail);
                }
            }
            return model.OrderBy(m => m.Index).ToList();
        }

        [GridAction]
        public ActionResult UpdateSalesOrderProgressDetail(int orderDetailId) {
            var model = new List<OrderProgressDetailModel>();
            try {
                using (var vfi = new tammaContext()) {

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateSalesOrderProgressDetail", "" + ex.Message);
            }
            return View(new GridModel(GetSalesOrderProgressDetail(orderDetailId)));

        }

        [GridAction]
        public ActionResult SelectProductionExpectedByOrderDetail(int orderDetailId) {
            var model = new List<OrderProgressModel>();
            try {
                model = GetProductionExpectedByOrderDetail(orderDetailId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionExpectedByOrderDetail", ex.Message);
            }
            return View(new GridModel(model));
        }

        public List<OrderProgressModel> GetProductionExpectedByOrderDetail(long orderDetailId) {
            var model = new List<OrderProgressModel>();
            using (var vfi = new tammaContext()) {
                var orderDetail = vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailId);

                var processes = (from pp in vfi.ProductionProcesses
                                 where pp.ProductId == orderDetail.ProductId &&
                                 pp.IsNecessary
                                 orderby pp.ProcessIndex descending
                                 select new {
                                     pp.ProductId,
                                     pp.Product.ProductCode,
                                     pp.ProcessIndex,
                                     Productivity = pp.Product.Productivity ?? 1,
                                     MillProductivity = pp.Product.MillProductivity ?? 1,
                                     pp.Product.ProcessingType,
                                     pp.WarehouseId,
                                     pp.Warehouse.WarehouseName,
                                     pp.Warehouse.IsQC,
                                     pp.Warehouse.IsProduction2,
                                     pp.Warehouse.IsHeatTreatment,
                                     pp.Warehouse.IsPolish,
                                     pp.Warehouse.IsPlating,
                                 }).ToList();
                var product = (from x in vfi.Products
                               where x.ProductId == orderDetail.ProductId
                               select new {
                                   x.ProductId,
                                   Sections = x.ProductionSections.Where(y => y.Active).OrderBy(y => y.SectionIndex).ToList(),
                                   HeatTreatment = x.ProductionHeatTreatments.OrderBy(y => y.Section).FirstOrDefault(y => y.Active),
                                   Polish = x.ProductionPolishes.OrderBy(y => y.Section).FirstOrDefault(y => y.Active),
                                   Platings = x.ProductionPlatings.OrderBy(y => y.PlatingIndex).ToList(),
                               }).FirstOrDefault();

                var warehouses = processes.Select(p => p.WarehouseId).ToList();
                var processSpecial = processes.FirstOrDefault(x => x.IsQC);
                var qcWarehouses = _warehouseController.GetActiveWarehouseIds(new WarehouseConfiguration { IsQC = true });
                var production2Warehouses = _warehouseController.GetActiveWarehouseIds(new WarehouseConfiguration { IsProduction2 = true });
                var platingWarehouses = _warehouseController.GetActiveWarehouseIds(new WarehouseConfiguration { IsPlating = true });
                if (processes.Any(x => x.IsQC)) {
                    warehouses.AddRange(qcWarehouses);
                }
                if (processes.Any(x => x.IsProduction2)) {
                    warehouses.AddRange(production2Warehouses);
                }
                if (processes.Any(x => x.IsPlating)) {
                    warehouses.AddRange(platingWarehouses);
                }
                warehouses = warehouses.Distinct().ToList();
                var totalInvs = (from pi in vfi.ProductInventories
                                 where pi.ProductId == orderDetail.ProductId &&
                                       warehouses.Contains(pi.WarehouseId) &&
                                       pi.TotalQty > 0
                                 select new {
                                     pi.ProductId,
                                     pi.WarehouseId,
                                     pi.TotalQty,
                                 }).ToList();
                var cames = (from x in vfi.SelectDiagram1
                             where x.ProductId == orderDetail.ProductId
                             orderby x.MachineName
                             select new TrackUpMachineModel {
                                 ProductId = x.ProductId,
                                 MachineName = x.MachineName,
                                 RealProductivity = x.RealProductivity
                             }).ToList();
                var cncs = (from x in vfi.SelectDiagram2
                            where x.ProductId == orderDetail.ProductId
                            orderby x.MachineName
                            select new TrackUpMachineModel {
                                ProductId = x.ProductId,
                                MachineName = x.MachineName,
                                RealProductivity = x.RealProductivity
                            }).ToList();
                var phayCncs = cncs.Where(x => x.MachineName.Contains("P")).ToList();
                cncs = cncs.Where(x => x.MachineName.Contains("CNC")).ToList();
                var track = MyUtilities.Machine.LastTrackUpProduct(0, "C", 0, orderDetail.ProductId, DateTime.Now);
                var trackCnc = MyUtilities.Machine.LastTrackUpProduct(0, "P", 0, orderDetail.ProductId, DateTime.Now);

                var approvedOrders = (from od in vfi.OrderDetails
                                      where
                                          od.Order.DueDate != null &&
                                          od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                         od.ProductId == orderDetail.ProductId &&
                                          (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                           od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                          od.RequiedNumber > 0
                                      orderby od.Order.DueDate
                                      select new {
                                          od.ProductId,
                                          od.Order.OrderNumber,
                                          DueDate = od.Order.DueDate.Value,
                                          od.RequiedNumber,
                                          od.Note,
                                      }).ToList();
                var approvedOrderQuantity = approvedOrders.Sum(od => od.RequiedNumber);
                var numberProcess = orderDetail.RequiedNumber + approvedOrderQuantity;

                var savedProgresses = (from x in vfi.OrderProgresses
                                       where x.OrderDetailId == orderDetailId
                                       select new OrderProgressModel {
                                           ProgressId = x.ProgressId,
                                           ProgressIndex = x.ProcessIndex,
                                           ExpectedFactor = x.ExpectedFactor,
                                           ExpectedDay = x.ExpectedDay,
                                           StartDate = x.StartDate,
                                           PendingProcessDay = 0,
                                           WarehouseId = x.WarehouseId,
                                           WarehouseName = x.Warehouse.WarehouseName,
                                           ProductId = x.ProductId,
                                           ProductCode = x.Product.ProductCode,
                                           DesignProductivityInDay = x.ProductivityInDay,
                                           NumberProcess = x.NumberProcess,
                                           IsProductionManager = false,
                                           ModifiedDate = x.ModifiedDate,
                                           ModifiedUser = x.ModifiedUser,
                                           Active = x.Active,
                                           Note = x.Note,
                                           OrderDetailId = orderDetailId,
                                       }).ToList();

                var finishDate = orderDetail.VFIDueDate != null ? orderDetail.VFIDueDate : orderDetail.CustomerDueDate;
                var afterProcessWarehouseId = new List<int>();
                OrderProgressModel nextProcess = null;
                foreach (var process in processes) {
                    // xu ly cong doan co nhieu kho
                    var processWarehouseIds = new List<int> { process.WarehouseId };
                    if (process.IsQC) { processWarehouseIds.AddRange(qcWarehouses); }
                    else if (process.IsProduction2) { processWarehouseIds.AddRange(production2Warehouses); }
                    else if (process.IsPlating) { processWarehouseIds.AddRange(platingWarehouses); }
                    processWarehouseIds = processWarehouseIds.Distinct().ToList();

                    var entity = savedProgresses.FirstOrDefault(x => x.WarehouseId == process.WarehouseId);
                    if (entity != null && entity.Active) {
                        entity.InvQuantity = totalInvs.Where(pi => processWarehouseIds.Contains(pi.WarehouseId)).Sum(pi => pi.TotalQty);
                        numberProcess -= MyUtilities.Function.RoundUp(entity.InvQuantity);
                        if (numberProcess < 0) { numberProcess = 0; }
                        entity.RequiredProductivityInDay = entity.DesignProductivityInDay;
                        entity.FinishDate = MyUtilities.Function.ToDate(entity.StartDate.Value, entity.ExpectedDay - 1);
                        finishDate = MyUtilities.Function.FromDate(entity.FinishDate.Value, 1);
                        if (entity.IsProduction) nextProcess = entity;
                        else { nextProcess = null; }
                        model.Add(entity);
                    }
                    else {
                        entity = new OrderProgressModel {
                            ProgressId = 0,
                            ProgressIndex = process.ProcessIndex,
                            ExpectedFactor = 1,
                            ExpectedDay = 0,
                            PendingProcessDay = 0,
                            WarehouseId = process.WarehouseId,
                            WarehouseName = process.WarehouseName,
                            ProductId = process.ProductId,
                            ProductCode = process.ProductCode,
                            DesignProductivityInDay = 0,
                            NumberProcess = numberProcess,
                            IsProductionManager = false,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = "Auto",
                            OrderDetailId = orderDetailId,
                        };
                        entity.InvQuantity = totalInvs.Where(pi => processWarehouseIds.Contains(pi.WarehouseId)).Sum(pi => pi.TotalQty);

                        var afterProcess = processes.Where(p => p.ProcessIndex > process.ProcessIndex && p.WarehouseId != process.WarehouseId).ToList();
                        if (afterProcess.Any()) {
                            entity.AfterInvQuantity = totalInvs.Where(pi => afterProcessWarehouseId.Contains(pi.WarehouseId)).Sum(pi => pi.TotalQty);
                        }

                        // numberProcess < 0 ko thuc hien tinh toan process
                        if (numberProcess <= 0) { goto add; }
                        switch (process.WarehouseId) {
                            case (int)MyUtilities.Warehouse.Id.Production1:
                                if (track != null) {
                                    entity.IsProduction = true;
                                    entity.DesignProductivityInDay = MyUtilities.Product.GetProductionRateInFactoryDayTime(track.RealProductivity);
                                }
                                else {
                                    entity.DesignProductivityInDay = MyUtilities.Product.GetProductionRateInFactoryDayTime(process.Productivity);
                                }
                                entity.ProgressNote = OrderProgressNote.GetTrackUpProductionNote(cames);
                                entity.ProgressNote += OrderProgressNote.GetTrackUpProductionNote(cncs);
                                break;

                            case (int)MyUtilities.Warehouse.Id.Cnc:
                                if (trackCnc != null) {
                                    entity.IsProduction = true;
                                    entity.DesignProductivityInDay = MyUtilities.Product.GetCncProductionRateInFactoryDayTime(trackCnc.RealProductivity, trackCnc.RealRate);
                                }
                                else {
                                    //entity.DesignProductivityInDay = MyUtilities.Product.GetDesignCncProductionRateInTime(MyUtilities.Product.Second20h, process.MillProductivity);
                                    entity.DesignProductivityInDay = MyUtilities.Product.GetProductionRateInFactoryDayTime(process.MillProductivity);
                                }
                                entity.ProgressNote = OrderProgressNote.GetTrackUpProductionNote(phayCncs);
                                break;

                            case (int)MyUtilities.Warehouse.Id.Production2:
                                var production2Productivity = product.Sections.Sum(x => x.Productivity);
                                if (production2Productivity > 0) {
                                    entity.DesignProductivityInDay = MyUtilities.Product.GetProductionRateInDayTime(production2Productivity);
                                }
                                entity.ProgressNote = OrderProgressNote.GetProduction2Note(product.Sections);
                                break;

                            case (int)MyUtilities.Warehouse.Id.HeatTreatment:
                                if (product.HeatTreatment != null) {
                                    if (product.HeatTreatment.Rate > 0) {
                                        entity.DesignProductivityInDay = MyUtilities.Product.GetProductionRateInDayTime(product.HeatTreatment.Timing / product.HeatTreatment.Rate);
                                    }
                                    entity.ProgressNote = OrderProgressNote.GetHeatTreatmentNote(product.HeatTreatment);
                                }
                                break;

                            case (int)MyUtilities.Warehouse.Id.SurfaceTreatment:
                                if (product.Polish != null) {
                                    if (product.Polish.Rate > 0) {
                                        entity.DesignProductivityInDay = MyUtilities.Product.GetProductionRateInDayTime(product.Polish.Timing / product.Polish.Rate);
                                    }
                                    entity.ProgressNote = OrderProgressNote.GetPolishNote(product.Polish);
                                }
                                break;

                            case (int)MyUtilities.Warehouse.Id.WaitingPlating:
                                if (product.Platings.Any()) {
                                    entity.ProgressNote = OrderProgressNote.GetPlatingNote(product.Platings);
                                    entity.ExpectedDay = product.Platings.Sum(ps => ps.PlatingDay);
                                }
                                break;

                            case (int)MyUtilities.Warehouse.Id.QcA:
                            case (int)MyUtilities.Warehouse.Id.QcB:
                            case (int)MyUtilities.Warehouse.Id.QcC:
                                entity.ExpectedDay = 3;
                                break;
                            default:
                                entity.ExpectedDay = 1;
                                break;
                        }
                        //}
                        entity.FinishDate = finishDate;
                        if (entity.DesignProductivityInDay > 0) {
                            entity.ExpectedDay = MyUtilities.Function.RoundUp(entity.NumberProcess / entity.DesignProductivityInDay);
                        }
                        if (entity.ExpectedDay <= 0) entity.ExpectedDay = 1;
                        entity.StartDate = MyUtilities.Function.FromDate(entity.FinishDate.Value, entity.ExpectedDay - 1);
                        entity.RequiredProductivityInDay = MyUtilities.Function.RoundUp(entity.NumberProcess / entity.ExpectedDay);
                        numberProcess -= MyUtilities.Function.RoundUp(entity.InvQuantity);
                        if (numberProcess < 0) { numberProcess = 0; }

                    add:
                        if (entity.StartDate != null) {
                            finishDate = MyUtilities.Function.FromDate(entity.StartDate.Value, 1);
                        }
                        if (entity.IsProduction) nextProcess = entity;
                        else { nextProcess = null; }
                        model.Add(entity);
                    }
                    afterProcessWarehouseId.AddRange(processWarehouseIds);
                }
                var today = DateTime.Today.AddDays(1);
                model = model.OrderBy(m => m.ProgressIndex).ToList();

                // re-calculate date for auto
                finishDate = null;
                foreach (var entity in model) {
                    if (entity.NumberProcess <= 0) continue; // bo qua cac ko yeu cau
                    // bo qua da luu
                    if (!entity.ModifiedUser.Equals("Auto")) {
                        finishDate = entity.FinishDate;
                        continue;
                    }

                    if (entity.DesignProductivityInDay > 0) {
                        if (entity.FinishDate < today) { entity.FinishDate = today; }
                        if (entity.StartDate < today) { entity.StartDate = today; }
                        entity.ExpectedDay = MyUtilities.Function.DaysNoSunDay(entity.StartDate.Value, entity.FinishDate.Value);
                        if (entity.ExpectedDay <= 0) entity.ExpectedDay = 1;
                        entity.RequiredProductivityInDay = MyUtilities.Function.RoundUp(entity.NumberProcess / entity.ExpectedDay);
                        entity.ExpectedFactor = entity.RequiredProductivityInDay / entity.DesignProductivityInDay;
                    }
                    else {
                        if (finishDate != null) { entity.StartDate = MyUtilities.Function.ToDate(finishDate.Value, 1); }
                        else if (entity.StartDate < today) { entity.StartDate = today; }
                        entity.FinishDate = MyUtilities.Function.ToDate(entity.StartDate.Value, entity.ExpectedDay - 1);
                    }
                    // next move
                    finishDate = entity.FinishDate;
                }
            }

            return model;
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionExpectedByOrderDetail(OrderProgressModel update) {
            try {
                SaveOrderProgess(update, HttpContext.User.Identity.Name);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionExpectedByOrderDetail", ex.Message);
            }
            return View(new GridModel(GetProductionExpectedByOrderDetail(update.OrderDetailId)));
        }

        public void SaveOrderProgess(OrderProgressModel update, string saveName) {
            using (var vfi = new tammaContext()) {
                var entity = vfi.OrderProgresses.FirstOrDefault(op => op.ProgressId == update.ProgressId);
                if (entity == null) {
                    entity = new OrderProgress {
                        ProductId = update.ProductId,
                        WarehouseId = update.WarehouseId,
                        OrderDetailId = update.OrderDetailId,
                        ProcessIndex = update.ProgressIndex,

                        NumberProcess = update.NumberProcess,
                        ProductivityInDay = update.DesignProductivityInDay,
                        ExpectedFactor = update.ExpectedFactor,
                        ExpectedDay = update.ExpectedDay,
                        StartDate = update.StartDate.Value,
                        Note = update.Note,

                        Month = update.StartDate.Value.Month,
                        Year = update.StartDate.Value.Year,
                        Active = true,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = saveName,
                    };
                    vfi.OrderProgresses.Add(entity);
                }
                else {
                    entity.NumberProcess = update.NumberProcess;
                    entity.ProductivityInDay = update.DesignProductivityInDay;
                    entity.ExpectedFactor = update.ExpectedFactor;
                    entity.StartDate = update.StartDate.Value;
                    entity.ExpectedDay = update.ExpectedDay;
                    entity.ProcessIndex = update.ProgressIndex;
                    entity.Month = entity.StartDate.Month;
                    entity.Year = entity.StartDate.Year;
                    entity.Active = true;
                    entity.Note = update.Note;

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = saveName;
                }
                vfi.SaveChanges();
            }
        }


        [HttpPost]
        public ActionResult PrintApproveOrderDetailForm(int orderId) {
            var model = new List<OrderDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                    //model.CustomerCode = order.Customer.CustomerCode;
                    //model.OrderNumber = order.OrderNumber;
                    //model.OrderDate = order.OrderDate;
                    //model.ModifiedDate = order.ModifiedDate;
                    //model.ModifiedUser = order.ModifiedUser;
                    var productIds = order.OrderDetails.Select(od => od.ProductId).Distinct().ToList();
                    var approvedOrders = (from od in vfi.OrderDetails
                                          where
                                              od.Order.DueDate != null &&
                                              od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                            productIds.Contains(od.ProductId) &&
                                              (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                               od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                              od.RequiedNumber > 0
                                          orderby od.Order.DueDate
                                          select new {
                                              od.ProductId,
                                              od.Order.OrderNumber,
                                              DueDate = od.Order.DueDate.Value,
                                              od.RequiedNumber,
                                              od.Note,
                                          }).ToList();
                    var totalQuality = order.OrderDetails.Sum(od => od.RequiedNumber);
                    var fckProcess = new List<OrderProgressModel> { };
                    fckProcess.Add(new OrderProgressModel {
                        WarehouseName = "Nguyên liệu"
                    });
                    fckProcess.Add(new OrderProgressModel {
                        WarehouseName = "Công cụ LM"
                    });
                    fckProcess.Add(new OrderProgressModel {
                        WarehouseName = "Công cụ KT"
                    });
                    foreach (var detail in order.OrderDetails) {
                        var detailEntity = new OrderDetailModel {
                            CustomerCode = order.Customer.CustomerCode,
                            OrderNumber = order.OrderNumber,
                            CreateDateString = order.OrderDate.ToString("dd/MM/yyyy"),
                            AvailableQty = totalQuality,
                            ProductId = detail.ProductId,
                            ProductCode = detail.Product.ProductCode,
                            RequiredNumber = detail.RequiedNumber,
                            CustomerDueDate = detail.CustomerDueDate,
                        };

                        var approvedOrdersById = approvedOrders.Where(od => od.ProductId == detail.ProductId).ToList();
                        foreach (var approvedDetail in approvedOrdersById) {
                            detailEntity.ApprovedOrderDetails.Add(new ApprovedOrderDetail {
                                OrderNumber = approvedDetail.OrderNumber,
                                DueDate = approvedDetail.DueDate,
                                RequireNumber = approvedDetail.RequiedNumber,
                                Note = approvedDetail.Note
                            });
                        }
                        detailEntity.AvailableQty = detailEntity.ApprovedOrderDetails.Sum(od => od.RequireNumber);
                        detailEntity.OrderProcessDetails = GetProductionExpectedByOrderDetail(detail.OrderDetailId);
                        detailEntity.OrderProcessDetails.InsertRange(0, fckProcess);
                        model.Add(detailEntity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintApproveOrderDetailForm", ex.Message);
            }
            return PartialView("PageApproveOrderDetail", model);
        }

        [GridAction]
        public ActionResult SelectConfirmOrderDetail() {
            var model = new List<OrderDetailModel>();
            try {
                model = GetSelectConfirmOrderDetail();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectConfirmOrderDetail", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<OrderDetailModel> GetSelectConfirmOrderDetail() {
            var model = new List<OrderDetailModel>();
            using (var vfi = new tammaContext()) {
                var orderDetails = (from x in vfi.OrderDetails
                                    where x.Order.DueDate == null &&
                                         x.Order.Status != (byte)MyUtilities.Sales.Status.Cancel
                                    orderby x.CustomerDueDate
                                    select x).ToList();
                var productIds = orderDetails.Select(x => x.ProductId).Distinct().ToList();
                var approvedOrders = (from od in vfi.OrderDetails
                                      where
                                          od.Order.DueDate != null &&
                                          od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                            productIds.Contains(od.ProductId) &&
                                          (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                           od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                          od.RequiedNumber > 0
                                      orderby od.Order.DueDate
                                      select new ApprovedOrderDetail {
                                          ProductId = od.ProductId,
                                          RequireNumber = od.RequiedNumber,
                                          DueDate = od.Order.DueDate.Value,
                                          OrderNumber = od.Order.OrderNumber,
                                      }).ToList();
                var processes = (from pp in vfi.ProductionProcesses
                                 where productIds.Contains(pp.ProductId) &&
                                 pp.IsNecessary
                                 orderby pp.ProcessIndex descending
                                 select new {
                                     pp.ProductId,
                                     pp.WarehouseId,
                                     pp.ProcessIndex,
                                     pp.Warehouse.WarehouseName,
                                     pp.Warehouse.IsQC,
                                     pp.Warehouse.IsProduction2,
                                     pp.Warehouse.IsHeatTreatment,
                                     pp.Warehouse.IsPolish,
                                     pp.Warehouse.IsPlating,
                                 }).ToList();
                var orderDetailIds = orderDetails.Select(x => x.OrderDetailId).ToList();
                var savedProgresses = (from x in vfi.OrderProgresses
                                       where orderDetailIds.Contains(x.OrderDetailId) && x.Active
                                       select new {
                                           x.OrderDetailId,
                                           x.WarehouseId
                                       }).ToList();
                var productInvs = (from x in vfi.ProductInventories
                                   where x.TotalQty > 0 &&
                                   productIds.Contains(x.ProductId)
                                   select new {
                                       x.ProductId,
                                       x.WarehouseId,
                                       x.TotalQty,
                                   });
                var qcs = _warehouseController.GetActiveWarehouseIds(new WarehouseConfiguration { IsQC = true });
                var platings = _warehouseController.GetActiveWarehouseIds(new WarehouseConfiguration { IsPlating = true });
                var production2s = _warehouseController.GetActiveWarehouseIds(new WarehouseConfiguration { IsProduction2 = true });

                var user = vfi.Users.FirstOrDefault(x => x.Username.Equals(HttpContext.User.Identity.Name));
                var userProgressPermissions = (from x in vfi.WarehousePermissions
                                               where x.UserId == user.UserId && x.OrderProgress == true
                                               select x.WarehouseId.Value).ToList();

                foreach (var detail in orderDetails) {
                    var entity = model.FirstOrDefault(x => x.ProductId == detail.ProductId);
                    if (entity != null) continue;
                    entity = new OrderDetailModel {
                        ProductCode = detail.Product.ProductCode,
                        CustomerCode = detail.Order.Customer.CustomerCode,
                        OrderNumber = detail.Order.OrderNumber,
                        OrderDetailId = detail.OrderDetailId,
                        OrderQty = detail.OrderQty.Value,
                        OrderDate = detail.ModifiedDate ?? DateTime.Now,
                        ProductId = detail.ProductId,
                        CustomerDueDate = detail.CustomerDueDate,
                        Note = detail.Note
                    };
                    var approvedOrdersById = approvedOrders.Where(od => od.ProductId == entity.ProductId).ToList();
                    entity.Note = OrderAutoNote.GetOrderNote(approvedOrdersById);
                    var processQuantity = approvedOrdersById.Sum(x => x.RequireNumber) + entity.OrderQty;
                    var processesById = processes.Where(x => x.ProductId == entity.ProductId).ToList();
                    var noProcesswarehouseIds = new List<int>();
                    if (processesById.Any()) {
                        for (int i = 0; i < processesById.Count; i++) {
                            var process = processesById[i];

                            var processWarehouseIds = new List<int> { process.WarehouseId };
                            if (process.IsQC) { processWarehouseIds = qcs; }
                            else if (process.IsPlating) { processWarehouseIds = platings; }
                            else if (process.IsProduction2) { processWarehouseIds = production2s; }
                            var invQuantity = productInvs.Where(x => processWarehouseIds.Contains(x.WarehouseId) && x.ProductId == entity.ProductId)
                                                        .ToList()
                                                        .Sum(x => x.TotalQty);
                            if (invQuantity >= processQuantity) {
                                noProcesswarehouseIds = processesById.Where(x => x.ProcessIndex < process.ProcessIndex).Select(x => x.WarehouseId).ToList();
                                break; // thanh pham du ton
                            }
                            else {
                                processQuantity -= invQuantity;
                            }
                        }
                        processesById = processesById.Where(x => !noProcesswarehouseIds.Contains(x.WarehouseId)).ToList();
                        if (!processesById.Any(x => userProgressPermissions.Contains(x.WarehouseId))) continue;
                        var saved = savedProgresses.Where(x => x.OrderDetailId == entity.OrderDetailId).Select(y => y.WarehouseId).ToList();
                        if (saved.Any()) {
                            processesById = processesById.Where(x => !saved.Contains(x.WarehouseId)).ToList();
                        }
                        var warehouses = processesById.OrderBy(x => x.ProcessIndex).Select(x => x.WarehouseName).ToList();
                        entity.ProcessNote = OrderAutoNote.GetProcessNote(warehouses);
                    }
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectTrackingOrderProgress(int customerId, string productCode, int warehouseId, string fromDate, string toDate) {
            var model = new List<TrackingOrderProgressModel>();
            try {
                model = GetSavedOrderProgressModel(customerId, productCode, warehouseId, fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTrackingOrderProgress", "" + ex.Message);
            }
            return View(new GridModel(model));
        }

        List<TrackingOrderProgressModel> GetSavedOrderProgressModel(
            int customerId, string productCode, int warehouseId,
            string fromDate, string toDate) {
            var model = new List<TrackingOrderProgressModel>();
            var ci = new CultureInfo("vi-VN");
            var fDate = string.IsNullOrWhiteSpace(fromDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(toDate, ci);
            using (var vfi = new tammaContext()) {
                var warehouse = vfi.Warehouses.FirstOrDefault(x => x.WarehouseId == warehouseId);
                if (warehouse == null) {
                    throw new AggregateException("Lỗi! Không tìm thấy công đoạn! Vui lòng chọn lại công đoạn");
                }
                var qcWarehouseIds = _warehouseController.GetActiveWarehouseIds(new WarehouseConfiguration { IsQC = true, CanStock = true });
                var production2WarehouseIds = _warehouseController.GetActiveWarehouseIds(new WarehouseConfiguration { IsProduction2 = true, CanStock = true });
                var platingWarehouseIds = _warehouseController.GetActiveWarehouseIds(new WarehouseConfiguration { IsPlating = true, CanStock = true });
                var warehouseIds = new List<int>();
                if (warehouse.IsQC) { warehouseIds = qcWarehouseIds; }
                else if (warehouse.IsProduction2) { warehouseIds = production2WarehouseIds; }
                else if (warehouse.IsPlating) { warehouseIds = platingWarehouseIds; }
                else if (warehouse.CanStock) { warehouseIds.Add(warehouseId); }

                var orderProgresses = (from x in vfi.OrderProgresses
                                       where x.StartDate <= tDate &&
                                           x.WarehouseId == warehouseId &&
                                           x.OrderDetail.Order.DueDate != null &&
                                           x.OrderDetail.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                           x.OrderDetail.RequiedNumber > 0
                                       orderby x.StartDate
                                       select new {
                                           x.ProductId,
                                           x.StartDate,
                                           x.ExpectedDay,
                                           x.ProductivityInDay,
                                           x.OrderDetailId,
                                           x.WarehouseId,
                                           x.ExpectedFactor,
                                           x.NumberProcess,
                                           x.ProcessIndex,
                                           x.OrderDetail.Order.DueDate,
                                           x.OrderDetail.OrderQty,
                                           x.OrderDetail.RequiedNumber,
                                       }).ToList();
                var productIds = orderProgresses.Select(x => x.ProductId).Distinct().ToList();
                var products = (from x in vfi.Products
                                where productIds.Contains(x.ProductId) &&
                                    x.Active &&
                                    (customerId == 0 || x.CustomerId == customerId)
                                select new {
                                    x.ProductId,
                                    x.ProductCode,
                                    x.Customer.CustomerCode
                                }).ToList();
                if (!string.IsNullOrWhiteSpace(productCode)) {
                    products = products.Where(x => x.ProductCode.Contains(productCode)).ToList();
                }
                productIds = products.Select(x => x.ProductId).ToList();
                var orderDetails = (from x in vfi.OrderDetails
                                    where x.RequiedNumber > 0 &&
                                        x.Order.DueDate <= tDate &&
                                        x.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                        productIds.Contains(x.ProductId)
                                    select new {
                                        x.ProductId,
                                        x.RequiedNumber,
                                        x.OrderQty,
                                        x.Order.DueDate
                                    }).ToList();
                var productionProcesses = (from x in vfi.ProductionProcesses
                                           where productIds.Contains(x.ProductId) && x.IsAlert && x.IsNecessary
                                           orderby x.ProcessIndex
                                           select new {
                                               x.ProductId,
                                               x.WarehouseId,
                                               x.ProcessIndex,
                                               x.Warehouse.IsQC,
                                               x.Warehouse.IsProduction2,
                                               x.Warehouse.IsPlating,
                                           }).ToList();
                var productInvs = (from x in vfi.ProductInventories
                                   where productIds.Contains(x.ProductId) &&
                                       //warehouseIds.Contains(x.WarehouseId) &&
                                       x.TotalQty > 0
                                   select new {
                                       x.ProductId,
                                       x.WarehouseId,
                                       x.TotalQty,
                                   }).ToList();
                var minDate = orderProgresses.Min(x => x.StartDate);
                var trackingProgressings = new List<TrackingProgressingProcessModel>();
                if (warehouseId == MyUtilities.Warehouse.Production1) {
                    trackingProgressings = (from x in vfi.ImportFormSX1Detail
                                            where x.ImportFormSX1.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                productIds.Contains(x.ProductId) &&
                                                x.ImportFormSX1.MaterialUseDate >= minDate
                                            select new TrackingProgressingProcessModel {
                                                ProductId = x.ProductId,
                                                Date = x.ImportFormSX1.MaterialUseDate,
                                                Quantity = x.Number1 + x.Number2,
                                                ToWarehouseId = 0
                                            }).ToList();
                }
                else if (warehouseId == MyUtilities.Warehouse.Finish) {
                    trackingProgressings = (from x in vfi.InvoiceDetails
                                            where x.Active &&
                                                productIds.Contains(x.ProductId.Value) &&
                                                x.Invoice.ShipmentDate >= minDate
                                            select new TrackingProgressingProcessModel {
                                                ProductId = x.ProductId.Value,
                                                Date = x.Invoice.ShipmentDate.Value,
                                                Quantity = x.Piece,
                                                ToWarehouseId = 0
                                            }).ToList();
                }
                else if (warehouse.IsPlating) {
                    trackingProgressings = (from x in vfi.ImportNCU_QCBDetail
                                            where x.ImportNCU_QCB.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                productIds.Contains(x.ProductId) &&
                                                x.ImportNCU_QCB.ImportDate >= minDate
                                            select new TrackingProgressingProcessModel {
                                                ProductId = x.ProductId,
                                                Date = x.ImportNCU_QCB.ImportDate,
                                                Quantity = x.RealNumber,
                                                ToWarehouseId = 0
                                            }).ToList();
                }
                else {
                    trackingProgressings = (from x in vfi.ProductInventoryPeriods
                                            where productIds.Contains(x.ProductId) &&
                                                warehouseIds.Contains(x.WarehouseId) &&
                                                x.LastPeriodQuantity < x.EarlyPeriodQuantity &&
                                                x.Transaction.WarehouseReceiptId > 0 &&
                                                x.PeriodDate >= minDate
                                            select new TrackingProgressingProcessModel {
                                                ProductId = x.ProductId,
                                                Date = x.PeriodDate,
                                                Quantity = x.Quantity,
                                                ToWarehouseId = x.Transaction.WarehouseReceiptId.Value
                                            }).ToList();
                }

                var rangesDate = new List<DateTime>();
                foreach (var product in products) {
                    var entity = new TrackingOrderProgressModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        CustomerCode = product.CustomerCode,
                        ProgressIndex = 0,
                    };
                    var orderDetailsById = orderDetails.Where(x => x.ProductId == entity.ProductId).ToList();
                    entity.OrderQuantity = orderDetailsById.Sum(x => x.OrderQty.Value);
                    entity.OrderRequired = orderDetailsById.Sum(x => x.RequiedNumber);
                    entity.OrderCount = orderDetailsById.Count;

                    var orderProgressById = orderProgresses.Where(x => x.ProductId == product.ProductId).ToList();
                    foreach (var progress in orderProgressById) {
                        if (entity.ProgressIndex < progress.ProcessIndex) {
                            entity.ProgressIndex = progress.ProcessIndex;
                        }
                        if (entity.NumberProcess < progress.NumberProcess) {
                            entity.NumberProcess = progress.NumberProcess;
                        }
                        if (entity.DesignProductivityInDay < progress.ProductivityInDay) {
                            entity.DesignProductivityInDay = progress.ProductivityInDay;
                        }
                        var finishDate = MyUtilities.Function.ToDate(progress.StartDate, progress.ExpectedDay - 1);
                        if (entity.FinishDate == null || entity.FinishDate < finishDate) {
                            entity.FinishDate = finishDate;
                        }
                        if (entity.StartDate == null || entity.StartDate > progress.StartDate) {
                            entity.StartDate = progress.StartDate;
                        }

                        var quantityPerDay = progress.ExpectedDay > 0 ? MyUtilities.Function.RoundUp(progress.NumberProcess / progress.ExpectedDay) : 0;

                        var progressDate = progress.StartDate;
                        while (progressDate <= finishDate) {
                            var processDay = entity.Days.FirstOrDefault(x => x.Date == progressDate);
                            if (processDay == null) {
                                processDay = new OrderProgressDay {
                                    Date = progressDate,
                                    Quantity = quantityPerDay
                                };
                                entity.Days.Add(processDay);
                            }
                            else if (quantityPerDay > processDay.Quantity) {
                                processDay.Quantity = quantityPerDay;
                            }
                            rangesDate.Add(progressDate);
                            progressDate = MyUtilities.Function.ToDate(progressDate, 1);
                        }
                    }
                    entity.ExpectedDay = entity.Days.Count;
                    entity.RequiredProductivityInDay = entity.ExpectedDay > 0 ? MyUtilities.Function.RoundUp(entity.NumberProcess / entity.ExpectedDay) : 0;

                    if (warehouse.CanStock) {
                        entity.InvQuantity = productInvs.Where(x => x.ProductId == entity.ProductId && warehouseIds.Contains(x.WarehouseId)).Sum(x => x.TotalQty);
                    }
                    var afterProgress = productionProcesses.Where(x => x.ProcessIndex > entity.ProgressIndex).ToList();
                    var afterWarehouseIds = afterProgress.Select(x => x.WarehouseId).ToList();
                    if (afterProgress.Any(x => x.IsQC)) { afterWarehouseIds.AddRange(qcWarehouseIds); }
                    if (afterProgress.Any(x => x.IsProduction2)) { afterWarehouseIds.AddRange(production2WarehouseIds); }
                    if (afterProgress.Any(x => x.IsPlating)) { afterWarehouseIds.AddRange(platingWarehouseIds); }
                    entity.AfterInvQuantity = productInvs.Where(x => x.ProductId == entity.ProductId && afterWarehouseIds.Contains(x.WarehouseId)).Sum(x => x.TotalQty);

                    var nextProcess = afterProgress.FirstOrDefault() != null ? afterProgress.FirstOrDefault().WarehouseId : 0;
                    var trackingProgressingsById = trackingProgressings.Where(x => x.ProductId == entity.ProductId &&
                        x.Date >= entity.StartDate &&
                        x.Date <= entity.FinishDate &&
                        (x.ToWarehouseId == 0 || x.ToWarehouseId == nextProcess)).ToList();
                    if (trackingProgressingsById.Any()) {
                        entity.ProductionQuantity = trackingProgressingsById.Sum(x => x.Quantity);
                    }

                    model.Add(entity);
                }

                rangesDate = rangesDate.Distinct().OrderBy(x => x).ToList();
                var rangesDateCount = rangesDate.Count;
                var maxScheduleDateCount = 31;
                foreach (var entity in model) {
                    foreach (var progressDate in rangesDate) {
                        var processDay = entity.Days.FirstOrDefault(x => x.Date == progressDate);
                        if (processDay == null) {
                            entity.Days.Add(new OrderProgressDay { Date = progressDate, Quantity = 0 });
                        }
                    }

                    entity.Days = entity.Days.OrderBy(x => x.Date).ToList();
                    for (int i = 0; i < maxScheduleDateCount - rangesDateCount; i++) {
                        entity.Days.Add(new OrderProgressDay { Quantity = 0 });
                    }
                }
            }
            return model.OrderBy(x => x.CustomerCode).ThenBy(x => x.ProductCode).ToList();
        }

        #endregion

        #region SalesOrder

        //[HttpPost]
        //public ActionResult CheckSalesOrderNumber(string orderNumber)
        //{
        //    try
        //    {
        //        var models = _salesOrderService.GetOrderByOrderNumber(orderNumber);

        //        return Json(models != null ? 1 : 0);
        //    }
        //    catch (FormatException)
        //    {
        //        return Json(2);
        //    }
        //}

        #endregion

        #region SalesOrderDetail

        [HttpPost]
        [GridAction]
        public ActionResult UpdateOrder(int orderId, string dueDate) {
            return View(new GridModel(new List<OrderDetailModel>()));
        }

        [GridAction]
        public ActionResult SelectSalesOrderDetail() {
            return View(new GridModel(new List<SalesOrderDetailModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateSalesOrderDetail(
            [Bind(Prefix = "inserted")] IEnumerable<SalesOrderDetailModel> insertedSalesOrderDetails,
            [Bind(Prefix = "updated")] IEnumerable<SalesOrderDetailModel> updatedSalesOrderDetails,
            [Bind(Prefix = "deleted")] IEnumerable<SalesOrderDetailModel> deletedSalesOrderDetails,
            string salesOrderNumber, string orderDate, string shipDate
            , int vendorId, int shipMethodId, int employeeId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                    "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<SalesOrderDetailModel>()));
            }

            if (insertedSalesOrderDetails != null) {
                try {
                    var salesOrder = new Order {

                        Active = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now
                    };
                    foreach (var soDetail in insertedSalesOrderDetails.Select(
                        models => new OrderDetail {

                        })) {


                        salesOrder.OrderDetails.Add(soDetail);
                    }

                    //var rs = _salesOrderService.CreateSalesOrder(salesOrder);
                    //if (rs == "1")
                    //{
                    //    if (_unitOfWork.SaveChanges() <= 0)
                    //        ModelState.AddModelError("ProductCode", @"Không thể tạo giá trị mới. Xin vui lòng nhập lại. (savechanges). ");
                    //}
                    //else
                    //    ModelState.AddModelError("ProductCode", @"Không thể tạo giá trị mới. Xin vui lòng nhập lại. (create). " + rs);
                }
                catch (Exception exception) {
                    ModelState.AddModelError("ProductCode", "" + exception.Message);
                }
            }

            return View(new GridModel(new List<SalesOrderDetailModel>()));
        }


        [GridAction]
        public ActionResult SelectExportByOrderId(long orderId) {
            var model = new List<ExportFormTP_KDModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var exports = vfi.ExportFormTP_KD.Where(e => e.OrderId == orderId);
                    if (!exports.Any())
                        return View(new GridModel(model));
                    foreach (var export in exports) {
                        var transaction =
                            vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(export.TransactionCode));
                        if (transaction == null || transaction.Status != (int)MyUtilities.Transaction.Status.Approved)
                            continue;
                        var entity = new ExportFormTP_KDModel {
                            TransactionCode = transaction.TransactionCode,
                            ExportFormId = export.ExportId,
                            Transporter = export.Transporter,
                            TotalBox = export.TotalBox,
                            ModifiedDate = export.ModifiedDate.Value,
                            ModifiedUser = export.ModifiedUser,
                            DateTransporter = export.DateTransporter.Value,
                            DateCreate = export.DateCreate.Value,
                            CarNumber = export.CarNumber,
                            CompanyTransporter = export.CompanyTransporter,
                            TransactionId = transaction.TransactionId
                        };
                        entity.TotalQuality = export.ExportFormTP_KDDetail.Sum(ed => ed.Quality);
                        model.Add(entity);

                    }
                    return View(new GridModel(model.OrderBy(m => m.DateCreate)));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectExportByOrderId", ex.Message);
            }
            return View(new GridModel(model));
        }


        [GridAction]
        public ActionResult SelectExportDetailById(long orderDetailId) {
            var model = new List<ExportFormTP_KDDetailsModel>();
            //return View(new GridModel(model));
            try {
                using (var vfi = new tammaContext()) {
                    var orderDetail = vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailId);
                    var exportDetails = from ed in vfi.ExportFormTP_KDDetail
                                        where ed.ProductId == orderDetail.ProductId
                                              && ed.ExportFormTP_KD.DateTransporter.Value.Month == DateTime.Now.Month
                                              && ed.ExportFormTP_KD.DateTransporter.Value.Year == DateTime.Now.Year
                                        select ed;
                    if (!exportDetails.Any())
                        return View(new GridModel(model));
                    foreach (var exportDetail in exportDetails) {
                        var transaction =
                            vfi.Transactions.FirstOrDefault(
                                t => t.TransactionCode.Equals(exportDetail.ExportFormTP_KD.TransactionCode));
                        if (transaction == null || transaction.Status != (byte)MyUtilities.Transaction.Status.Approved)
                            continue;
                        var entity = new ExportFormTP_KDDetailsModel {
                            TransactionCode = exportDetail.ExportFormTP_KD.TransactionCode,
                            ExportDate = exportDetail.ExportFormTP_KD.DateTransporter.Value,
                            Quality = exportDetail.Quality,
                            ProductCode = exportDetail.Product.ProductCode,
                            Weight = exportDetail.Weight.Value,
                            DetailId = exportDetail.DetailId,
                        };
                        var invoiceDetail =
                            vfi.InvoiceDetails.Where(id => id.ExportDetailId == exportDetail.DetailId && id.Active);
                        foreach (var detail in invoiceDetail) {
                            entity.Note += ("(" + detail.OrderDetail.Order.OrderNumber + "-" +
                                            detail.OrderDetail.Order.DueDate.Value.ToString("dd/MM/yy") + "-" +
                                            detail.Piece + ")");
                        }
                        model.Add(entity);
                    }
                    return View(new GridModel(model.OrderBy(m => m.ExportDate)));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectExportByOrderId", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectExportDetailByOrderInMonthId(long forecastOrderId) {
            var model = new List<ExportFormTP_KDDetailsModel>();
            //return View(new GridModel(model));
            try {
                using (var vfi = new tammaContext()) {
                    var forecast = vfi.ForecastOrders.FirstOrDefault(od => od.ForecastOrderId == forecastOrderId);

                    var invoiceDetails = from id in vfi.InvoiceDetails
                                         where id.Active &&
                                               id.Invoice.ShipmentDate.Value.Month == forecast.ForecastDate.Month &&
                                               id.Invoice.ShipmentDate.Value.Year == forecast.ForecastDate.Year &&
                                               id.OrderDetail.Order.DueDate.Value.Month == forecast.ForecastDate.Month &&
                                               id.OrderDetail.Order.DueDate.Value.Year == forecast.ForecastDate.Year &&
                                               id.ProductId == forecast.ProductId
                                         select id;
                    //var exportDetails = from ed in vfi.ExportFormTP_KDDetail
                    //                    where ed.ProductId == forecast.ProductId
                    //                          && ed.ExportFormTP_KD.DateTransporter.Value.Month == DateTime.Now.Month
                    //                          && ed.ExportFormTP_KD.DateTransporter.Value.Year == DateTime.Now.Year
                    //                    select ed;
                    if (!invoiceDetails.Any())
                        return View(new GridModel(model));
                    foreach (var invoiceDetail in invoiceDetails) {
                        //var transaction =
                        //    vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(exportDetail.ExportFormTP_KD.TransactionCode));
                        var entity = new ExportFormTP_KDDetailsModel {
                            TransactionCode = invoiceDetail.ExportFormTP_KDDetail.ExportFormTP_KD.TransactionCode,
                            ExportDate = invoiceDetail.Invoice.ShipmentDate.Value,
                            Quality = invoiceDetail.Piece,
                            ProductCode = invoiceDetail.Product.ProductCode,
                            //Weight = invoiceDetail.Weight.Value,
                            DetailId = invoiceDetail.InvoiceDetailId,
                            Note =
                                invoiceDetail.OrderDetail.Order.OrderNumber + "-" +
                                invoiceDetail.OrderDetail.Order.DueDate.Value.ToString("dd/MM/yyyy")
                        };
                        //var invoiceDetail =
                        //    vfi.InvoiceDetails.Where(id => id.ExportDetailId == exportDetail.DetailId && id.Active);
                        //foreach (var detail in invoiceDetail)
                        //{
                        //    entity.Note += ("(" + detail.OrderDetail.Order.OrderNumber + "-" +
                        //                    detail.OrderDetail.Order.DueDate.Value.ToString("dd/MM/yy") + "-" +
                        //                    detail.Piece + ")");
                        //}
                        model.Add(entity);
                    }
                    return View(new GridModel(model.OrderBy(m => m.ExportDate)));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectExportByOrderId", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectForecastDetailByOrderDetailId(long orderDetailId) {
            var model = new List<ForecastDetailModel>();
            //return View(new GridModel(model));
            try {
                using (var vfi = new tammaContext()) {
                    var orderDetail = vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailId);
                    var date = orderDetail.VFIDueDate != null
                        ? orderDetail.VFIDueDate
                        : orderDetail.CustomerDueDate;
                    if (date == null)
                        return View(new GridModel(model));
                    var forecasts = from f in vfi.ForecastOrders
                                    where f.ProductId == orderDetail.ProductId &&
                                          f.ForecastDate.Month == date.Value.Month &&
                                          f.ForecastDate.Year == date.Value.Year &&
                                          f.Status == (byte)MyUtilities.Transaction.Status.Approved
                                    select f;
                    foreach (var forecast in forecasts) {
                        var entity = new ForecastDetailModel {
                            ForecastOrderId = forecast.ProductId,
                            ForecastDate = forecast.ForecastDate,
                            ForecastQuantity = forecast.Quantity,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectExportByOrderId", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectMaterialByProductId(long orderDetailId) {
            var model = new List<ExpectedProductionMaterialPlanModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var orderDetail = vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailId);
                    if (orderDetail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết đơn hàng");
                    var product = vfi.Products.FirstOrDefault(x => x.ProductId == orderDetail.ProductId);
                    if (product == null) {
                        return View(new GridModel(model));
                    }
                    if (product.MaterialId == null) {
                        model.Add(new ExpectedProductionMaterialPlanModel {
                            Index = 0,
                            MaterialId = 0,
                            MaterialCode = "Chưa có nguyên liệu thiết kế",
                        });
                        return View(new GridModel(model));
                    }
                    var material = product.Material;
                    //var productionMaterials = from pm in vfi.ProductionMaterials
                    //                          where pm.ProductId == orderDetail.ProductId && pm.Active
                    //                          select new {
                    //                              pm.Material.MaterialId,
                    //                              pm.Material.MaterialCode,
                    //                              pm.Material.MaterialName,
                    //                              Shape = (pm.Material.Shape + "").Trim(),
                    //                              pm.Material.OutDiameter,
                    //                              pm.Material.InDiameter,
                    //                              DiameterType = pm.Material.DiameterType + "",
                    //                              ForecastsQuality = pm.Product.ForecastsQuality ?? 0,
                    //                              pm.UnitWeightByMaterial
                    //                          };
                    //var materialIds = productionMaterials.Select(m => m.MaterialId).ToList();
                    var materialInvs = (from mi in vfi.MaterialInventories
                                        where mi.TotalQty != null && mi.TotalQty > 0 &&
                                              mi.UnitWeight != null && mi.UnitWeight > 0 &&
                                              material.MaterialId == mi.MaterialId
                                        select new {
                                            //mi.MaterialId,
                                            //mi.MaterialInventoryId,
                                            TotalQtyKg = mi.TotalQty * mi.UnitWeight,
                                        }).ToList();
                    //var materialInvIds = materialInvs.Select(mi => mi.MaterialInventoryId).ToList();
                    var materialInvOnMachines = (from mim in vfi.MaterialInvOnMachines
                                                 where mim.MaterialInventory.MaterialId == material.MaterialId
                                                     //materialInvIds.Contains(mim.MaterialInvId.Value)
                                                       && mim.TotalQuantity > 0
                                                 select new {
                                                     //mim.MaterialInvId,
                                                     //mim.MaterialInventory.MaterialId,
                                                     TotalQuantityKg = mim.TotalQuantity * mim.MaterialInventory.UnitWeight,
                                                 }).ToList();
                    //var forecastsInMonth = (from f in vfi.ForecastOrders
                    //                        where
                    //                        f.ForecastDate.Month == orderDetail.CustomerDueDate.Value.Month &&
                    //                        f.ForecastDate.Year == orderDetail.CustomerDueDate.Value.Year &&
                    //                        f.ProductId == orderDetail.ProductId &&
                    //                        f.Status == (byte)MyUtilities.Transaction.Status.Approved
                    //                        select f);
                    var orderDetailsElse = (from od in vfi.OrderDetails
                                            where
                                            od.Order.DueDate != null &&
                                            (od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                             od.Order.Status != (byte)MyUtilities.Sales.Status.Completed) &&
                                            od.ProductId == orderDetail.ProductId &&
                                            od.RequiedNumber > 0 &&
                                            od.OrderId != orderDetail.OrderId
                                            orderby od.Order.DueDate
                                            select new {
                                                //od.ProductId,
                                                RequiedNumber = od.RequiedNumber,
                                            }).ToList();

                    var orderDetailsElseNotApprove = (from od in vfi.OrderDetails
                                                      where
                                                          od.Order.DueDate == null &&
                                                          od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                                           od.Order.Status != (byte)MyUtilities.Sales.Status.Completed &&
                                                          od.ProductId == orderDetail.ProductId &&
                                                          od.RequiedNumber > 0 &&
                                                          od.OrderId != orderDetail.OrderId
                                                      orderby od.Order.DueDate
                                                      select new {
                                                          //od.ProductId,
                                                          RequiedNumber = od.RequiedNumber,
                                                      }).ToList();
                    var lastMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddSeconds(-1);
                    var last3Month = lastMonth.AddMonths(-3);
                    var materialUses = (from x in vfi.MaterialUseDetails
                                        where x.MaterialUseInShift.UsedDate > last3Month &&
                                         x.MaterialUseInShift.UsedDate <= lastMonth &&
                                        x.MaterialUseInShift.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                        x.MaterialInventory.MaterialId == material.MaterialId
                                        select new {
                                            x.MaterialUseInShift.UsedDate.Month,
                                            UseQuantityKg = (x.EditQuantity + x.EditQuantity2) * x.MaterialInventory.UnitWeight
                                        }).ToList();
                    var monthUse = materialUses.Select(x => x.Month).Distinct().Count();
                    var poMaterials = (from pod in vfi.PurchaseOrderDetails
                                       where pod.MaterialClassifiedId == material.MaterialType.MaterialClassifiedId &&
                                       pod.ReferenceId == material.MaterialId &&
                                      (pod.PurchaseOrder.Status == (byte)MyUtilities.Transaction.Status.Open ||
                                       pod.PurchaseOrder.Status == (byte)MyUtilities.Transaction.Status.Processing) &&
                                       pod.OrderQty > pod.ReceivedQty
                                       select new {
                                           //pod.ReferenceId,
                                           PoQuantity = pod.OrderQty - pod.ReceivedQty
                                       }).ToList();
                    var index = 1;
                    //foreach (var productionMaterial in productionMaterials) {
                    //    var entity =
                    //        model.FirstOrDefault(
                    //            m =>
                    //                m.MaterialName.Equals(productionMaterial.MaterialName) &&
                    //                m.DiameterType.Equals(productionMaterial.DiameterType) &&
                    //                m.OutDiameter == productionMaterial.OutDiameter &&
                    //                m.InDiameter == productionMaterial.InDiameter &&
                    //                m.Shape.Equals(productionMaterial.Shape));
                    //    if (entity == null) {
                    var entity = new ExpectedProductionMaterialPlanModel {
                        Index = index,
                        MaterialId = material.MaterialId,
                        MaterialName = material.MaterialName,
                        MaterialCode = material.MaterialCode,
                        DiameterType = material.DiameterType,
                        InDiameter = material.InDiameter,
                        OutDiameter = material.OutDiameter,
                        Shape = material.Shape.Trim(),
                        TotalInv = 0,
                        TotalInvKg = 0,
                        ProductWeight = product.QcWeight ?? 0,
                        UnitWeightByMaterial = product.ProductionWeight ?? 0,
                        RequireInYear = orderDetail.RequiedNumber,
                        MonthCount = 3,
                    };
                    entity.UnitWeightByMaterial = MyUtilities.Product.GetProductWeight(
                        entity.MaterialName,
                        entity.OutDiameter,
                        entity.InDiameter,
                        product.Length ?? 0,
                        product.KnifeCut ?? 0,
                        entity.Shape);
                    index++;
                    //entity.ForecastInYear = productionMaterial.ForecastsQuality;
                    //entity.ForecastByMonth = entity.ForecastInYear * entity.MonthCount / 12;
                    //entity.ForecastInMonth = forecastsInMonth.Any()
                    //    ? 0
                    //    : forecastsInMonth.Sum(f => f.Quantity);
                    //if (orderDetailsElse.Any())
                    entity.RequireInMonth = orderDetailsElse.Sum(od => od.RequiedNumber);
                    //if (orderDetailsElseNotApprove.Any())
                    entity.RequireByMonth = orderDetailsElseNotApprove.Sum(od => od.RequiedNumber);
                    //var materialInvsById =
                    //    materialInvs.Where(mi => mi.MaterialId == productionMaterial.MaterialId).ToList();
                    //if (materialInvsById.Any())
                    entity.TotalInvKg = materialInvs.Sum(mi => mi.TotalQtyKg);
                    entity.ForecastInMonth = monthUse > 0 ? materialUses.Sum(x => x.UseQuantityKg) / monthUse : 0;
                    //var materialInvOnMachineById =
                    //    materialInvOnMachines.Where(mi => mi.MaterialId == productionMaterial.MaterialId).ToList();
                    //if (materialInvOnMachineById.Any())
                    entity.TotalInvKg += materialInvOnMachines.Sum(mi => mi.TotalQuantityKg);
                    entity.TotalRequire = entity.RequireInMonth + entity.RequireByMonth + entity.RequireInYear;
                    entity.OnPoKg = poMaterials.Sum(x => x.PoQuantity);
                    entity.RequirePoKg = entity.TotalRequireKg > entity.TotalInvKg + entity.OnPoKg
                        ? entity.TotalRequireKg - entity.OnPoKg - entity.TotalInvKg
                        : 0;
                    model.Add(entity);
                    //}
                    //}
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterialByProductId", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MaterialName)
                .ThenBy(m => m.Shape)
                .ThenBy(m => m.OutDiameter)
                .ThenBy(m => m.InDiameter)));
        }

        [GridAction]
        public ActionResult SelectToolOrderByOrderDetailId(long orderDetailId) {
            var model = new List<ExpectedProductionMaterialPlanModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var orderDetail = vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailId);
                    if (orderDetail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết đơn hàng");
                    var product = vfi.Products.FirstOrDefault(x => x.ProductId == orderDetail.ProductId);
                    if (product == null) {
                        return View(new GridModel(model));
                    }
                    var productionTools = (from x in product.ProductionTools
                                           where x.Active
                                           select new {
                                               x.Quota,
                                               x.ToolId,
                                               x.Tool.ToolFullCode,
                                               x.Tool.MaterialType.MaterialTypeName,
                                               x.Tool.MaterialType.MaterialClassifiedId
                                           }
                                           ).ToList();
                    if (!productionTools.Any()) {
                        model.Add(new ExpectedProductionMaterialPlanModel {
                            Index = 0,
                            MaterialId = 0,
                            MaterialCode = "Chưa có công cụ thiết kế",
                        });
                        return View(new GridModel(model));
                    }
                    var toolIds = product.ProductionTools.Select(x => x.ToolId).Distinct().ToList();
                    var invs = (from x in vfi.ToolInventories
                                where x.TotalQuantity > 0 &&
                                toolIds.Contains(x.ToolId)
                                select new {
                                    x.ToolId,
                                    x.TotalQuantity
                                }).ToList();

                    var orderDetailsElse = (from od in vfi.OrderDetails
                                            where
                                            od.Order.DueDate != null &&
                                            (od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                             od.Order.Status != (byte)MyUtilities.Sales.Status.Completed) &&
                                            od.ProductId == orderDetail.ProductId &&
                                            od.RequiedNumber > 0 &&
                                            od.OrderId != orderDetail.OrderId
                                            orderby od.Order.DueDate
                                            select new {
                                                RequiedNumber = od.RequiedNumber,
                                            }).ToList();

                    var orderDetailsElseNotApprove = (from od in vfi.OrderDetails
                                                      where
                                                          od.Order.DueDate == null &&
                                                          od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                                           od.Order.Status != (byte)MyUtilities.Sales.Status.Completed &&
                                                          od.ProductId == orderDetail.ProductId &&
                                                          od.RequiedNumber > 0 &&
                                                          od.OrderId != orderDetail.OrderId
                                                      orderby od.Order.DueDate
                                                      select new {
                                                          //od.ProductId,
                                                          RequiedNumber = od.RequiedNumber,
                                                      }).ToList();
                    var materialClassifiedId = productionTools.FirstOrDefault().MaterialClassifiedId;
                    var poMaterials = (from pod in vfi.PurchaseOrderDetails
                                       where pod.MaterialClassifiedId == materialClassifiedId &&
                                           toolIds.Contains(pod.ReferenceId) &&
                                          (pod.PurchaseOrder.Status == (byte)MyUtilities.Transaction.Status.Open ||
                                           pod.PurchaseOrder.Status == (byte)MyUtilities.Transaction.Status.Processing) &&
                                           pod.OrderQty > pod.ReceivedQty
                                       select new {
                                           pod.ReferenceId,
                                           PoQuantity = pod.OrderQty - pod.ReceivedQty
                                       }).ToList();
                    var lastMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddSeconds(-1);
                    var last3Month = lastMonth.AddMonths(-3);
                    var exportTools = (from x in vfi.ExportToolDetails
                                       where toolIds.Contains(x.ToolInventory.ToolId) &&
                                       x.ExportTool.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                       x.ExportTool.ExportDate > last3Month &&
                                       x.ExportTool.ExportDate <= lastMonth &&
                                       x.ExportTool.TransactionFpt.Type != (int)MyUtilities.Tool.ExportType.Destroy
                                       select new {
                                           x.ToolInventory.ToolId,
                                           x.ExportTool.ExportDate.Value.Month,
                                           x.Quantity
                                       }).ToList();
                    var orderQuantity = orderDetail.RequiedNumber;
                    var ordersElseQuantity = orderDetailsElse.Sum(od => od.RequiedNumber);
                    var ordersElseNotApproveQuantity = orderDetailsElseNotApprove.Sum(od => od.RequiedNumber);
                    var index = 1;
                    foreach (var toolId in toolIds) {
                        var productionToolsById = productionTools.Where(x => x.ToolId == toolId);
                        var entity = new ExpectedProductionMaterialPlanModel {
                            Index = index,
                            MaterialId = toolId,
                            MaterialName = productionToolsById.FirstOrDefault().MaterialTypeName,
                            MaterialCode = productionToolsById.FirstOrDefault().ToolFullCode,
                            TotalInv = 0,
                            TotalInvKg = 0,
                            ProductWeight = product.QcWeight ?? 0,
                            UnitWeightByMaterial = product.ProductionWeight ?? 0,
                            RequireInYear = 0,
                            RequireInMonth = 0,
                            RequireByMonth = 0,
                            MonthCount = 3,
                        };
                        entity.TotalInv = invs.Where(x => x.ToolId == toolId).Sum(x => x.TotalQuantity);
                        var exportToolsById = exportTools.Where(x => x.ToolId == toolId).ToList();
                        if (exportToolsById.Any()) {
                            var monthCount = exportToolsById.Select(x => x.Month).Distinct().Count();
                            entity.ForecastInMonth = exportToolsById.Sum(x => x.Quantity) / monthCount;
                        }
                        foreach (var productionTool in productionToolsById) {
                            if (productionTool.Quota <= 0) continue;
                            entity.RequireInYear += MyUtilities.Function.RoundUp(orderQuantity / productionTool.Quota);
                            entity.RequireInMonth += MyUtilities.Function.RoundUp(ordersElseQuantity / productionTool.Quota);
                            entity.RequireByMonth += MyUtilities.Function.RoundUp(ordersElseNotApproveQuantity / productionTool.Quota);
                        }
                        entity.TotalRequire = entity.RequireInMonth + entity.RequireByMonth + entity.RequireInYear;
                        entity.OnPoKg = poMaterials.Sum(x => x.PoQuantity);
                        entity.RequirePoKg = entity.TotalRequire > entity.TotalInv + entity.OnPoKg
                            ? entity.TotalRequire - entity.OnPoKg - entity.TotalInv
                            : 0;
                        index++;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectToolOrderByOrderDetailId", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MaterialName)
                .ThenBy(m => m.MaterialCode)));
        }

        [GridAction]
        public ActionResult SelectExportByProductId(long orderDetailId) {
            var model = new List<ExportFormQC_TPDetailModel>();

            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectOrderDetailByOrderId(long orderId) {
            var models = new List<OrderDetailModel>();
            try {
                models = GetOrderDetailsByOrderId(orderId).OrderBy(o => o.CustomerDueDate).ToList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectOrderDetailByOrderId", ex.Message);
            }
            return View(new GridModel(models));
        }

        [GridAction]
        public ActionResult SelectOrderDetailByOrderProductId(long orderId, int productId) {
            var models = new List<OrderDetailModel>();
            try {
                models = GetOrderDetailsByOrderProductId(orderId, productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectOrderDetailByOrderProductId", ex.Message);
            }
            return View(new GridModel(models));
        }

        [GridAction]
        public ActionResult UpdateOrderDetailByOrderProductId(OrderDetailModel update) {
            try {
                using (var vfi = new tammaContext()) {
                    var orderDetail = vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == update.OrderDetailId);
                    if (orderDetail == null)
                        throw new AggregateException(
                            "Không tìm thấy chi tiết đơn hàng");
                    if (orderDetail.RequiedNumber != orderDetail.OrderQty.Value)
                        throw new AggregateException(
                            "Đơn hàng đã xuất có xuất kho! \nKhông khả năng đổi giá! \nLỗi nặng!!!");
                    update.OrderId = orderDetail.OrderId;
                    update.ProductId = orderDetail.ProductId;
                    //var invoiceDetails = vfi.InvoiceDetails.Where(id => id.OrderDetailId == update.OrderDetailId);
                    //foreach (var invoiceDetail in invoiceDetails)
                    //{
                    //    var taxInvoiceDetails =
                    //        vfi.TaxInvoiceProductDetails.Where(tipd => tipd.ExportDetailId == invoiceDetail.ExportDetailId);
                    //    if (taxInvoiceDetails.Any())
                    //    {
                    //        return Json("Đơn hàng đã xuất hoá đơn! \nKhông khả năng đổi giá! \nLỗi nặng!!!");
                    //    }
                    //    invoiceDetail.Price = update.UnitPrice;
                    //}
                    orderDetail.UnitPrice = Math.Round(update.UnitPrice, 4);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateOrderDetailByOrderProductId", ex.Message);
            }
            return View(new GridModel(GetOrderDetailsByOrderProductId(update.OrderId, update.ProductId)));
        }
        public List<OrderDetailModel> GetOrderDetailsByOrderProductId(long orderId, int productId) {
            if (orderId == 0)
                return new List<OrderDetailModel>();
            var models = new List<OrderDetailModel>();

            var seePrice = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.SeePrice);
            var isSalesManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.SaleManagementLv2);

            using (var vfi = new tammaContext()) {
                var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                var orderDetails = vfi.OrderDetails.Where(td => td.OrderId == orderId && td.ProductId == productId)
                    .OrderBy(od => od.CustomerDueDate);

                foreach (var orderDetail in orderDetails) {
                    var entity = new OrderDetailModel {
                        ProductId = orderDetail.ProductId,
                        ProductCode = orderDetail.Product.ProductCode,
                        OrderDetailId = orderDetail.OrderDetailId,
                        OrderId = orderId,
                        VFIDueDate = orderDetail.VFIDueDate,
                        CustomerDueDate = orderDetail.CustomerDueDate,
                        OrderQty = orderDetail.OrderQty ?? 0,
                        PONumber = orderDetail.PONumber,
                        AvailableQty = 0,
                        TotalInventory = 0,
                        Note = orderDetail.Note,
                        ApproveType = false,
                        IsApprove = orderDetail.VFIDueDate != null,
                        DetailApproved = (orderDetail.Status == (int)MyUtilities.Sales.Status.Completed),
                        RequiredNumber = orderDetail.RequiedNumber,
                        InForcast = false,
                        UnitPrice = seePrice ? orderDetail.UnitPrice : 0.0,
                        //IsInvManager = isInvManager ? 1 : 0,
                        //IsProductionManager = isProductionManager ? 1 : 0,
                        IsSaleManager = isSalesManager ? 1 : 0,
                        OrderNote = orderDetail.OrderNote
                    };

                    models.Add(entity);
                }
            }

            return models.ToList();
        }


        [GridAction]
        public ActionResult SelectOrderDetailByOrderId2(long orderId) {
            var models = new List<OrderDetailModel>();
            try {
                models = GetOrderDetailsByOrderId2(orderId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectOrderDetailByOrderId2", ex.Message);
            }
            return View(new GridModel(models));
        }

        public List<OrderDetailModel> GetOrderDetailsByOrderId2(long orderId) {
            if (orderId == 0)
                return new List<OrderDetailModel>();
            var salesManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                MyUtilities.UserRole.SaleManagement);
            var model = new List<OrderDetailModel>();
            using (var vfi = new tammaContext()) {
                var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                var orderDetails = (from x in vfi.OrderDetails
                                    where x.OrderId == orderId
                                    select new {
                                        x.ProductId,
                                        x.OrderQty,
                                        x.RequiedNumber,
                                        x.UnitPrice,
                                        x.Order.DueDate,
                                    }).ToList();
                var productIds = orderDetails.Select(od => od.ProductId).Distinct().ToList();
                var warehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                var productInvs = (from x in vfi.ProductInventories
                                   where warehouses.Contains(x.WarehouseId) &&
                                         productIds.Contains(x.ProductId)
                                   select new {
                                       x.TotalQty,
                                       x.ProductId,
                                       x.WarehouseId,
                                   }).ToList();
                //var orderDetailsElse = from od in vfi.OrderDetails
                //    where
                //    od.Order.DueDate != null &&
                //    (od.Order.Status == (byte) MyUtilities.Sales.Status.Waiting ||
                //     od.Order.Status == (byte) MyUtilities.Sales.Status.InProcess) &&
                //    productIds.Contains(od.ProductId) &&
                //    od.RequiedNumber != 0 &&
                //    od.OrderId != orderId
                //    orderby od.Order.DueDate
                //    select new
                //    {
                //        od.ProductId,
                //        od.RequiedNumber,
                //        od.OrderQty,
                //        od.Order.DueDate,
                //        od.Order.OrderNumber,
                //        od.Order.Status,
                //    };
                var orderDetailsElse = (from od in vfi.OrderDetails
                                        where
                                        od.Order.DueDate != null &&
                                        (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                         od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                        productIds.Contains(od.ProductId) &&
                                        od.RequiedNumber > 0 &&
                                        od.OrderId != orderId
                                        group od by new { od.ProductId, od.Order.DueDate }
                                            into odd
                                            select new {
                                                odd.Key.ProductId,
                                                odd.Key.DueDate,
                                                OrderQty = odd.Sum(od => od.OrderQty.Value),
                                                RequiedNumber = odd.Sum(od => od.RequiedNumber),
                                            }).ToList();

                var forecast = (from f in vfi.ForecastOrders
                                where productIds.Contains(f.ProductId) &&
                                     f.Status == (byte)MyUtilities.Transaction.Status.Approved
                                select new {
                                    f.ProductId,
                                    f.ForecastDate,
                                    f.Quantity,
                                }).ToList();
                var products = (from x in vfi.Products
                                where productIds.Contains(x.ProductId)
                                select new {
                                    x.ProductId,
                                    x.ProductCode,
                                }).ToList();
                foreach (var product in products) {
                    var entity = new OrderDetailModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        //OrderDetailId = orderDetail.OrderDetailId,
                        OrderId = orderId,
                        //VFIDueDate = orderDetail.VFIDueDate,
                        //CustomerDueDate = orderDetail.CustomerDueDate,
                        //OrderQty = orderDetail.OrderQty ?? 0,
                        //PONumber = orderDetail.PONumber,
                        AvailableQty = 0,
                        TotalInventory = 0,
                        //Note = orderDetail.Note,
                        //ApproveType = false,
                        //IsApprove = orderDetail.VFIDueDate != null,
                        //DetailApproved = (orderDetail.Status == (int)MyUtilities.Sales.Status.Completed),
                        //RequiredNumber = orderDetail.RequiedNumber,
                        //InForcast = false,
                        //UnitPrice = seePrice ? orderDetail.UnitPrice : 0.0,
                        //IsInvManager = isInvManager ? 1 : 0,
                        //IsProductionManager = isProductionManager ? 1 : 0,
                        IsSaleManager = salesManager ? 1 : 0,
                        //VFIDueDate = order.due,
                        //OrderNote = 
                    };

                    var orderDetailsById = orderDetails.Where(od => od.ProductId == entity.ProductId).ToList();
                    entity.OrderQty = orderDetailsById.Sum(od => od.OrderQty.Value);
                    entity.RequiredNumber = orderDetailsById.Sum(od => od.RequiedNumber);
                    if (order.DueDate == null || order.Status == (byte)MyUtilities.Sales.Status.InProcess ||
                        order.Status == (byte)MyUtilities.Sales.Status.Waiting) {
                        var productInvsById = productInvs.Where(pi => pi.ProductId == entity.ProductId);

                        if (productInvsById.Any()) {
                            entity.TotalInventory = productInvsById.Sum(pi => pi.TotalQty);
                            productInvsById = productInvsById.Where(pi => pi.WarehouseId == MyUtilities.Warehouse.Finish);
                            if (productInvsById.Any())
                                entity.AvailableQty = productInvsById.Sum(pi => pi.TotalQty);
                        }
                    }
                    else {
                        entity.RequiredNumber = 0;
                    }
                    if (entity.RequiredNumber > 0) {
                        var detailsElse = orderDetailsElse.Where(od => od.ProductId == entity.ProductId).ToList();
                        var total = 0;
                        foreach (var detailElse in detailsElse) {
                            entity.Note += "\n(" + detailElse.DueDate.Value.ToString("dd/MM") + "-" +
                                           string.Format("{0:N0}", detailElse.RequiedNumber) + ")";
                            total += detailElse.RequiedNumber;
                        }
                        entity.Note = "Tổng ĐH khác: " + string.Format("{0:N0}", total) + " | " + entity.Note;
                    }
                    if (order.DueDate != null) {
                        var forecastById =
                            forecast.Where(
                                f =>
                                    f.ProductId == entity.ProductId &&
                                    f.ForecastDate.Month == order.DueDate.Value.Month &&
                                    f.ForecastDate.Year == order.DueDate.Value.Year).ToList();
                        entity.ForecastInMonth = forecastById.Sum(f => f.Quantity);
                    }

                    model.Add(entity);
                }
            }

            return model;
        }

        public List<OrderDetailModel> GetOrderDetailsByOrderId(long orderId) {
            if (orderId == 0)
                return new List<OrderDetailModel>();
            var models = new List<OrderDetailModel>();

            var seePrice = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.SeePrice);
            var isProductionManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ProductionManagement);
            var isSalesManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.SaleManagement);
            var isInvManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagement);

            using (var vfi = new tammaContext()) {
                //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                var orderDetails = (from od in vfi.OrderDetails
                                    where od.OrderId == orderId
                                    orderby od.Order.DueDate
                                    select new {
                                        od.OrderDetailId,
                                        od.ProductId,
                                        od.RequiedNumber,
                                        od.OrderQty,
                                        od.Order.DueDate,
                                        od.Order.OrderNumber,
                                        od.Status,
                                        od.Product.ProductCode,
                                        od.VFIDueDate,
                                        od.CustomerDueDate,
                                        od.PONumber,
                                        od.Note,
                                        od.UnitPrice,
                                        od.Product.Productivity,
                                        od.OrderNote,
                                    }).ToList();

                var productIds = orderDetails.Select(od => od.ProductId).Distinct().ToList();
                var warehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                var productInvs = (from x in vfi.ProductInventories
                                   where warehouses.Contains(x.WarehouseId) &&
                                         productIds.Contains(x.ProductId)
                                   select new {
                                       x.TotalQty,
                                       x.WarehouseId,
                                       x.ProductId
                                   }).ToList();
                var orderDetailsElse = (from od in vfi.OrderDetails
                                        where
                                            od.Order.DueDate != null &&
                                            (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                             od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                            productIds.Contains(od.ProductId) &&
                                            od.RequiedNumber > 0 &&
                                            od.OrderId != orderId
                                        orderby od.Order.DueDate descending
                                        select new ApprovedOrderDetail {
                                            ProductId = od.ProductId,
                                            RequireNumber = od.RequiedNumber,
                                            DueDate = od.Order.DueDate.Value,
                                            OrderNumber = od.Order.OrderNumber,
                                        }).ToList();
                var orderDetailsElseNotApprove = (from od in vfi.OrderDetails
                                                  where
                                                      od.Order.DueDate == null &&
                                                      od.VFIDueDate != null &&
                                                      productIds.Contains(od.ProductId) &&
                                                      od.RequiedNumber != 0 &&
                                                      od.OrderId != orderId
                                                  orderby od.Order.DueDate
                                                  select new ApprovedOrderDetail {
                                                      ProductId = od.ProductId,
                                                      RequireNumber = od.OrderQty.Value,
                                                      DueDate = od.VFIDueDate.Value,
                                                      OrderNumber = od.Order.OrderNumber,
                                                  }).ToList();
                //var orderProgresses = from op in vfi.OrderProgresses
                //                      where orderDetailIds.Contains(op.OrderDetailId.Value)
                //                      select op;

                var lastImportSx1Date = (from i in vfi.ImportFormSX1
                                         orderby i.MaterialUseDate descending
                                         select i.MaterialUseDate).FirstOrDefault();
                var lastImportDetail = (from id in vfi.ImportFormSX1Detail
                                        where id.ImportFormSX1.MaterialUseDate == lastImportSx1Date
                                              && productIds.Contains(id.ProductId)
                                        select id.ProductId).ToList();
                var forecast = (from f in vfi.ForecastOrders
                                where productIds.Contains(f.ProductId) &&
                                     f.Status == (byte)MyUtilities.Transaction.Status.Approved
                                select new {
                                    f.ProductId,
                                    f.ForecastDate,
                                    f.Quantity,
                                }).ToList();
                foreach (var orderDetail in orderDetails) {
                    var entity = new OrderDetailModel {
                        ProductId = orderDetail.ProductId,
                        ProductCode = orderDetail.ProductCode,
                        OrderDetailId = orderDetail.OrderDetailId,
                        OrderId = orderId,
                        VFIDueDate = orderDetail.VFIDueDate,
                        CustomerDueDate = orderDetail.CustomerDueDate,
                        OrderQty = orderDetail.OrderQty ?? 0,
                        PONumber = orderDetail.PONumber,
                        AvailableQty = 0,
                        TotalInventory = 0,
                        Note = orderDetail.Note,
                        ApproveType = false,
                        IsApprove = orderDetail.VFIDueDate != null,
                        DetailApproved = (orderDetail.Status == (int)MyUtilities.Sales.Status.Completed),
                        RequiredNumber = orderDetail.RequiedNumber,
                        InForcast = false,
                        UnitPrice = seePrice ? orderDetail.UnitPrice : 0.0,
                        IsInvManager = isInvManager ? 1 : 0,
                        IsProductionManager = isProductionManager ? 1 : 0,
                        IsSaleManager = isSalesManager ? 1 : 0,
                        OrderNote = orderDetail.OrderNote
                    };
                    var dueDate = orderDetail.VFIDueDate != null ? orderDetail.VFIDueDate : orderDetail.CustomerDueDate;

                    if (orderDetail.DueDate == null || orderDetail.Status == (byte)MyUtilities.Sales.Status.InProcess ||
                        orderDetail.Status == (byte)MyUtilities.Sales.Status.Waiting) {
                        var productInvsById = productInvs.Where(pi => pi.ProductId == orderDetail.ProductId);
                        if (productInvsById.Any()) {
                            entity.TotalInventory = productInvsById.Sum(pi => pi.TotalQty);
                            productInvsById = productInvsById.Where(pi => pi.WarehouseId == MyUtilities.Warehouse.Finish);
                            if (productInvsById.Any())
                                entity.AvailableQty = productInvsById.Sum(pi => pi.TotalQty);
                        }
                    }
                    //if (orderDetail.VFIDueDate == null)
                    //    if (entity.IsApprove)
                    //        entity.VFIDueDate = entity.CustomerDueDate;
                    if (entity.RequiredNumber > 0) {
                        var detailsElse = orderDetailsElse.Where(od => od.ProductId == entity.ProductId).ToList();
                        entity.Note = OrderAutoNote.GetOrderNote(detailsElse);
                        //var total = 0;
                        //foreach (var detailElse in detailsElse) {
                        //    entity.Note += "\n(" + detailElse.OrderNumber + "-" +
                        //                   detailElse.DueDate.Value.ToString("dd/MM") + "-" +
                        //                   string.Format("{0:N0}", detailElse.RequiedNumber) + ")";
                        //    total += detailElse.RequiedNumber;
                        //}
                        //entity.Note = "Tổng ĐH khác: " + string.Format("{0:N0}", total) + " | " + entity.Note;
                    }
                    var forecastById =
                        forecast.Where(
                            f =>
                            f.ProductId == entity.ProductId &&
                            (orderDetail.VFIDueDate == null
                                 ? (f.ForecastDate.Month == entity.CustomerDueDate.Value.Month &&
                                    f.ForecastDate.Year == entity.CustomerDueDate.Value.Year)
                                 : (f.ForecastDate.Month == entity.VFIDueDate.Value.Month &&
                                    f.ForecastDate.Year == entity.VFIDueDate.Value.Year))).ToList();
                    if (forecastById.Any()) {
                        entity.ForecastInMonth = forecastById.Sum(f => f.Quantity);
                    }
                    //Vfi.Models.OrderDetail detail = orderDetail;
                    // so sanh du bao - doi voi don hang chua duyet
                    if (orderDetail.DueDate == null) {
                        if (forecastById.Any()) {
                            var detailsElse = orderDetailsElse.Where(od => od.ProductId == entity.ProductId).ToList();
                            var detailsElseNotApprove =
                                orderDetailsElseNotApprove.Where(
                                    od => od.ProductId == entity.ProductId).ToList();
                            if (orderDetail.VFIDueDate != null) {
                                detailsElse =
                                    detailsElse.Where(
                                        od => od.DueDate.Month == orderDetail.VFIDueDate.Value.Month &&
                                              od.DueDate.Year == orderDetail.VFIDueDate.Value.Year).ToList();
                                detailsElseNotApprove =
                                    detailsElseNotApprove.Where(
                                        od => od.DueDate.Month == orderDetail.VFIDueDate.Value.Month &&
                                              od.DueDate.Year == orderDetail.VFIDueDate.Value.Year).ToList();
                            }
                            else if (orderDetail.CustomerDueDate != null) {
                                detailsElse =
                                    detailsElse.Where(
                                        od => od.DueDate.Month == orderDetail.CustomerDueDate.Value.Month &&
                                              od.DueDate.Year == orderDetail.CustomerDueDate.Value.Year).ToList();
                                detailsElseNotApprove =
                                    detailsElseNotApprove.Where(
                                        od => od.DueDate.Month == orderDetail.CustomerDueDate.Value.Month &&
                                              od.DueDate.Year == orderDetail.CustomerDueDate.Value.Year).ToList();
                            }
                            entity.InForcast = !((detailsElse.Sum(od => od.RequireNumber)) +
                                                 (detailsElseNotApprove.Sum(od => od.RequireNumber))
                                                 + entity.RequiredNumber > entity.ForecastInMonth);
                        }

                        // so sanh ton kho
                        if (orderDetail.Productivity > 0) {
                            entity.ProductivityInDay = (72000 / orderDetail.Productivity.Value);
                        }
                        else {
                            entity.ProductivityInDay = 1;
                        }
                        var detailsElseBefore = orderDetailsElse.Where(od => od.ProductId == entity.ProductId).ToList();
                        var detailsElseNotApproveBefore =
                            orderDetailsElseNotApprove.Where(od => od.ProductId == entity.ProductId).ToList();
                        if (orderDetail.VFIDueDate != null) {
                            detailsElseBefore = detailsElseBefore.Where(od => od.DueDate <= orderDetail.VFIDueDate).ToList();
                            detailsElseNotApproveBefore =
                                detailsElseNotApproveBefore.Where(od => od.DueDate <= orderDetail.VFIDueDate).ToList();
                        }
                        else if (orderDetail.CustomerDueDate != null) {
                            detailsElseBefore = detailsElseBefore.Where(od => od.DueDate <= orderDetail.CustomerDueDate).ToList();
                            detailsElseNotApproveBefore =
                                detailsElseNotApproveBefore.Where(od => od.DueDate <= orderDetail.CustomerDueDate)
                                                           .ToList();
                        }
                        var requiredNumber = (detailsElseBefore.Sum(od => od.RequireNumber)) +
                                             (detailsElseNotApproveBefore.Sum(od => od.RequireNumber)) +
                                             entity.RequiredNumber;
                        if (!entity.IsApprove)
                            entity.IsApprove = (entity.AvailableQty - requiredNumber) >= 0;
                        entity.ProductionApprove = (entity.TotalInventory - requiredNumber) >= 0;
                        if (entity.IsApprove) {
                            if (orderDetail.VFIDueDate == null)
                                entity.VFIDueDate = orderDetail.CustomerDueDate;
                        }
                    }

                    var lastImport = lastImportDetail.FirstOrDefault(id => id == entity.ProductId);
                    if (lastImport > 0) entity.IsProduction = true;

                    var lastOrder = orderDetailsElse.FirstOrDefault(x => x.ProductId == entity.ProductId);
                    if (lastOrder != null) {
                        entity.ProductionDate = lastOrder.DueDate;
                        entity.MachineRun = lastOrder.RequireNumber;
                    }
                    if (isInvManager)
                        entity.ApproveType = true;
                    else if (orderDetail.VFIDueDate != null && seePrice)
                        entity.ApproveType = true;
                    if (orderDetail.VFIDueDate == null)
                        entity.IsApprove = false;
                    models.Add(entity);
                }
            }


            //var entities = _transactionService.GetTransactionDetailByTransactionId(transactionId);
            //models = _transactionService.ConvertTransactionDetailToModels(entities).ToList();

            return models.OrderBy(m => m.ProductCode).ToList();
        }

        public ActionResult SplitOrderDetails(long orderId, long[] orderDetailIds) {
            try {
                if (!Request.IsAuthenticated) {
                    return Json(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                    if (order == null) return Json(0);
                    if (order.OrderDetails.Count == orderDetailIds.Length) return Json(2);
                    var orderDetails =
                        order.OrderDetails.Where(detail => orderDetailIds.Contains(detail.OrderDetailId)).ToList();
                    if (orderDetails.Count > 0) {
                        var newOrder = new Vfi.Models.Order {
                            OrderNumber =
                                MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Order,
                                                                  1),
                            CustomerId = order.CustomerId,
                            SalesPersonId = order.Employee.EmployeeId,
                            CreatedDate = DateTime.Now,
                            OrderDate = order.OrderDate,
                            Status = 0,
                            PoNumber = order.PoNumber,
                            LotNumber = order.LotNumber,
                            ModelNumber = order.ModelNumber,
                            BillToAddress = order.BillToAddress,
                            ShipToAddress = order.ShipToAddress,
                            ShipMethodId = order.ShipMethodId,
                            CurrencyCode = order.CurrencyCode,
                            Note = order.Note,
                            //DueDate = Convert.ToDateTime(dueDate),
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                        };
                        vfi.Orders.Add(newOrder);
                        vfi.SaveChanges();
                        foreach (var orderDetail in orderDetails) {
                            orderDetail.OrderId = newOrder.OrderId;
                        }
                        vfi.SaveChanges();
                    }
                    return Json(1);
                }
            }
            catch (Exception) {
                return Json(0);
            }
        }

        [HttpPost]
        [GridAction]
        public ActionResult SplitOrderDetailByOrderId(long orderId, long orderDetailId) {
            using (var vfi = new tammaContext()) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    var orderDetail = vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailId);
                    if (orderDetail != null) {
                        var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                        if ((order.OrderDetails.Count - 1) > 0) {
                            var newOrder = new Vfi.Models.Order {
                                OrderNumber =
                                    MyUtilities.AutoIncrease.GetParam(
                                        (int)MyUtilities.AutoIncrease.IncreaseNum.Order, 1),
                                CustomerId = order.CustomerId,
                                SalesPersonId = order.Employee.EmployeeId,
                                CreatedDate = DateTime.Now,
                                OrderDate = order.OrderDate,
                                Status = 1,
                                PoNumber = order.PoNumber,
                                LotNumber = order.LotNumber,
                                ModelNumber = order.ModelNumber,
                                BillToAddress = order.BillToAddress,
                                ShipToAddress = order.ShipToAddress,
                                ShipMethodId = order.ShipMethodId,
                                CurrencyCode = order.CurrencyCode,
                                Note = order.Note,
                                //DueDate = Convert.ToDateTime(dueDate),
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                            };
                            vfi.Orders.Add(newOrder);
                            vfi.SaveChanges();
                            orderDetail.OrderId = newOrder.OrderId;
                            vfi.SaveChanges();
                        }
                        else {
                            ModelState.AddModelError("Order detail",
                                                     @"Đơn hàng chỉ có 1 chi tiết không được tách. (Try Split)");
                        }
                    }

                    return View(new GridModel(
                                    GetOrderDetailsByOrderId(orderId).OrderByDescending(o => o.CustomerDueDate)));
                }
                catch (Exception) {
                    return View(new GridModel(
                                    GetOrderDetailsByOrderId(orderId).OrderByDescending(o => o.CustomerDueDate)));
                }
            }
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateOrderDetailByOrderId(OrderDetailModel update, string vfiDueDate) {
            try {
                var saleManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.SaleManagement);
                var invManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagement);
                using (var vfi = new tammaContext()) {
                    // quyen xem don gia trong don hang
                    var orderDetail = vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == update.OrderDetailId);
                    if (orderDetail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết cập nhật !");
                    if (saleManager && orderDetail.VFIDueDate != null)
                        orderDetail.Status = (int)MyUtilities.Sales.Status.Completed;
                    else if (invManager)
                        orderDetail.VFIDueDate = Convert.ToDateTime(vfiDueDate);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateOrderDetailByOrderId", ex.Message);
            }
            return View(new GridModel(
                GetOrderDetailsByOrderId(update.OrderId).OrderByDescending(o => o.CustomerDueDate)));
        }


        //[HttpPost]
        //public ActionResult OrderSent(long orderId)
        //{
        //    using (var vfi = new tammaContext())
        //    {
        //        var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
        //        if (order != null &&
        //            order.Status == (byte)MyUtilities.Sales.Status.Waiting &&
        //            order.Status == (byte)MyUtilities.Sales.Status.InProcess)
        //        {
        //            order.Status = (byte)MyUtilities.Transaction.Status.Approved;
        //            vfi.SaveChanges();
        //        }
        //    }
        //    return Json("");
        //}

        //[HttpPost]
        //public ActionResult OrderCancel(long orderId)
        //{
        //    using (var vfi = new tammaContext())
        //    {
        //        var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
        //        if (order != null &&
        //            (order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
        //             order.Status == (byte)MyUtilities.Sales.Status.InProcess))
        //        {
        //            order.Status = (byte)MyUtilities.Transaction.Status.Cancel;
        //            vfi.SaveChanges();
        //        }
        //    }
        //    return Json("");
        //}

        [HttpPost]
        [GridAction]
        public ActionResult UpdateOrderStatus(ManageOrderModel updated) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var order = vfi.Orders.FirstOrDefault(o => o.OrderId == updated.OrderId);
                    if (order == null)
                        throw new AggregateException("Lỗi Đơn Hàng !");
                    //if (order.Status != (byte) MyUtilities.Sales.Status.Waiting)
                    //{
                    //    throw new AggregateException("Đơn hàng " + order.OrderNumber +
                    //                                 " đã có giao không thể hủy ! Vui lòng hủy số lượng trong đơn hàng !");
                    //}
                    byte status = 0;
                    try {
                        status = Convert.ToByte(updated.StatusName);
                    }
                    catch (FormatException) { }
                    Order newOrder = null;
                    var monthChange = false;
                    if (status > 0) {
                        if (status == (byte)MyUtilities.Sales.Status.Cancel) {
                            if (order.OrderDetails.Any(od => od.RequiedNumber != od.OrderQty)) {
                                throw new AggregateException("Đơn hàng " + order.OrderNumber +
                                                             " đã có giao không thể hủy ! Vui lòng hủy số lượng trong đơn hàng !");
                            }
                        }
                        else if (status == (byte)MyUtilities.Sales.Status.Completed) {
                            if (order.OrderDetails.Any(x => x.RequiedNumber > 0)) {
                                throw new AggregateException("Đơn hàng " + order.OrderNumber +
                                                             " chưa giao đủ không thể hoàn thành ! Vui lòng hủy số lượng trong đơn hàng !");
                            }
                        }
                        order.Status = status;
                    }
                    else if (updated.DueDate != order.DueDate) {
                        if (updated.DueDate <= DateTime.Now) {
                            throw new AggregateException("Lỗi! Không thể dời ngày giao hàng về trước hiện tại");
                        }
                        if (updated.DueDate.Value.Month != order.DueDate.Value.Month 
                            || updated.DueDate.Value.Year != order.DueDate.Value.Year) {
                            monthChange = true;
                        }
                        if (order.OrderDetails.Any(x => x.RequiedNumber != x.OrderQty)) {
                            newOrder = new Order {
                                ParentOrderId = order.ParentOrderId ?? order.OrderId,
                                CustomerId = order.CustomerId,
                                CurrencyCode = order.CurrencyCode,
                                SalesPersonId = order.SalesPersonId,
                                BillToAddress = order.BillToAddress,
                                ShipToAddress = order.ShipToAddress,
                                ShipMethodId = order.ShipMethodId,
                                ShipmentDay = order.ShipmentDay,
                                Status = (byte)MyUtilities.Sales.Status.Waiting,
                                Active = true,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                LotNumber = order.LotNumber,
                                ModelNumber = order.ModelNumber,
                                Note = order.Note + "! Đổi ngày giao hàng:" + order.DueDate.Value.ToString("dd/MM")
                                                                    + "->" + updated.DueDate.Value.ToString("dd/MM"),
                                PoNumber = order.PoNumber,
                                ShippedDate = order.ShippedDate,
                                DueDate = updated.DueDate,
                                OrderDate = order.OrderDate,
                                OrderNumber = order.OrderNumber + "-" + string.Format("{0:00}", (order.Orders1.Count + 1))
                            };
                            foreach (var detail in order.OrderDetails) {
                                if (detail.RequiedNumber == 0) continue;
                                detail.OrderQty -= detail.RequiedNumber;
                                var newDetail = new OrderDetail {
                                    Active = true,
                                    CarrierTrackingNumber = detail.CarrierTrackingNumber,
                                    CustomerDueDate = detail.CustomerDueDate,
                                    LineTotal = detail.LineTotal,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ProductId = detail.ProductId,
                                    UnitPrice = detail.UnitPrice,
                                    UnitPriceDiscount = detail.UnitPriceDiscount,
                                    VFIDueDate = detail.VFIDueDate,
                                    OrderQty = detail.RequiedNumber,
                                    RequiedNumber = detail.RequiedNumber,
                                };
                                detail.RequiedNumber = 0;
                                detail.IsComplete = true;
                                newOrder.OrderDetails.Add(newDetail);
                            }
                            vfi.Orders.Add(newOrder);
                            order.Status = (byte)MyUtilities.Sales.Status.Completed;
                        }
                        else {
                            order.Note = order.Note + "! Đổi ngày giao hàng:" + order.DueDate.Value.ToString("dd/MM")
                                                                + "->" + updated.DueDate.Value.ToString("dd/MM");
                            order.DueDate = updated.DueDate;
                        }
                    }

                    order.ModifiedDate = DateTime.Now;
                    order.ModifiedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();

                    if (monthChange) {
                        if (newOrder != null) {
                            CreateForecaseOrder(newOrder.OrderId);
                        }
                        else {
                            CreateForecaseOrder(order.OrderId);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateOrderStatus", "" + ex.Message);
            }
            return View(new GridModel(GetOrderNeedCancel().OrderByDescending(o => o.ModifiedDate)));
        }

        public int CreateForecaseOrder(long orderId) {
            var saved = 0;
            using (var vfi = new tammaContext()) {
                var order = vfi.Orders.FirstOrDefault(o => o.OrderId == orderId);
                var productIds = order.OrderDetails.Select(x => x.ProductId).Distinct().ToList();
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
                saved += vfi.SaveChanges();
            }
            return saved;
        }

        [GridAction]
        public ActionResult SelectApprovedOrder() {
            return View(new GridModel(GetOrderNeedCancel().OrderByDescending(o => o.ModifiedDate)));
        }

        private List<ManageOrderModel> GetOrderNeedCancel() {
            var models = new List<ManageOrderModel>();
            using (var vfi = new tammaContext()) {
                var orders = from o in vfi.Orders
                             where
                                 o.DueDate != null && o.Active &&
                                 (o.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                  o.Status == (byte)MyUtilities.Sales.Status.InProcess)
                             select o;

                foreach (var order in orders) {
                    var model = new ManageOrderModel();
                    model.OrderId = order.OrderId;
                    model.OrderNumber = order.OrderNumber;
                    model.PoNumber = order.PoNumber ?? "";
                    model.CustomerCode = order.Customer.CustomerCode;
                    model.SalesPersonName = order.Employee.EmployeeName;
                    model.TotalQuality = order.OrderDetails.Sum(od => od.OrderQty).Value;
                    model.TotalRequired = order.OrderDetails.Sum(od => od.RequiedNumber);
                    model.ModifiedDate = order.ModifiedDate;
                    model.StatusName = MyUtilities.Sales.GetText(order.Status);
                    model.DueDate = order.DueDate;
                    model.Note = order.Note;
                    //var area =
                    //    vfi.Areas.FirstOrDefault(
                    //        a => a.AreaId == (order.Customer.Area != null ? order.Customer.Area.AreaId : 1));
                    //model.Area = area.AreaName;
                    models.Add(model);
                }


                return models;
            }
        }

        [GridAction]
        public ActionResult SelectTaxInvoices(string fromDate, string toDate, byte? status) {
            var ci = new CultureInfo("vi-VN");
            DateTime toDay = DateTime.Now;

            var fDate = string.IsNullOrWhiteSpace(fromDate)
                            ? new DateTime(toDay.Year, toDay.Month, 1)
                            : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                            ? toDay
                            : Convert.ToDateTime(toDate, ci);

            var model = new List<TaxInvoiceModel>();
            if (status == 0)
                return View(new GridModel(model));
            try {
                using (var vfi = new tammaContext()) {
                    var taxInvoices =
                        vfi.TaxInvoices.Where(
                            ti => ti.Status == status && ti.SetupDate >= fDate && ti.SetupDate <= tDate).ToList();
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
                            TotalAmount = taxInvoice.TaxInvoiceProducts.Sum(tip => tip.Quantity * tip.UnitPrice)
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
            }
            catch (Exception ex) {
                ModelState.AddModelError("TaxInvoiceManagement", "" + ex.Message);
            }
            return View(new GridModel(model.OrderByDescending(m => m.SetupDate).ThenBy(m => m.CustomerCode)));
        }

        [GridAction]
        public ActionResult SelectTaxInvoicesDetail(int taxInvoiceId) {
            var model = new List<TaxInvoiceDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    // var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == taxInvoiceId);
                    var taxInvoiceDetail = vfi.TaxInvoiceDetails.Where(tid => tid.TaxInvoiceId == taxInvoiceId);
                    foreach (var detail in taxInvoiceDetail) {
                        var entity = new TaxInvoiceDetailModel {
                            Money = detail.Money,
                            ModifiedDate = detail.ModifiedDate.Value,
                            ModifiedUser = detail.ModifiedUser,
                            Times = detail.Times,
                            StatusName = MyUtilities.Accounting.GetStatusText(detail.Status ?? 1),
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
        public ActionResult SelectTaxInvoiceProductAuto(int customerId, string currencyCode, int exchangeRate, int tax) {
            var model = new List<InvoiceDetailTempModel>();
            if (customerId <= 0)
                return View(new GridModel(model));
            try {
                if (string.IsNullOrWhiteSpace(currencyCode))
                    throw new AggregateException("Lỗi! Loại tiền tệ lỗi");
                if (exchangeRate <= 0 || (currencyCode.Equals("VND") && exchangeRate > 1)) {
                    exchangeRate = 1;
                }
                if (tax < 0) { throw new AggregateException("Lỗi! Thuế < 0"); }
                using (var vfi = new tammaContext()) {
                    var exportDetails = (from ed in vfi.ExportFormTP_KDDetail
                                         where
                                             ed.ExportFormTP_KD.DateTransporter >= MyUtilities.Sales.StartTaxInvoiceDate
                                             &&
                                             ed.ExportFormTP_KD.CustomerId == customerId
                                             && ed.InvoiceDetails.Any(id => id.Active && id.Invoice.Active)
                                             && ed.IsInvoiced == false
                                         select new {
                                             ed.DetailId,
                                             ed.ExportFormTP_KD,
                                             ed.ProductId,
                                             ed.Product.ProductCode,
                                             ed.Quality,
                                             ExportDate = ed.ExportFormTP_KD.DateTransporter.Value,
                                             VFIDueDate = ed.ExportFormTP_KD.DateTransporter.Value,
                                             InvoiceDetails = ed.InvoiceDetails.Where(id => id.Active && id.Invoice.Active),
                                         }).ToList();
                    foreach (var detail in exportDetails) {
                        var export = detail.ExportFormTP_KD;
                        if (export == null)
                            throw new AggregateException("Lỗi! Không tìm thấy phiếu xuất!");
                        var transaction =
                            vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(export.TransactionCode) &&
                                                                 t.Status ==
                                                                 (byte)MyUtilities.Transaction.Status.Approved);
                        if (transaction == null) continue;
                        var invoiceDetails = detail.InvoiceDetails;
                        if (!invoiceDetails.Any()) continue;
                        var entity = new InvoiceDetailTempModel {
                            DetailId = detail.DetailId,
                            ProductId = detail.ProductId ?? 0,
                            ProductCode = detail.ProductCode,
                            ExportedDateStr = detail.ExportDate.ToString("dd/MM/yyyy"),
                            ExportDetailId = detail.DetailId,
                            VFIDueDate = detail.VFIDueDate,
                            CurrencyCode = currencyCode,

                            InvoiceNumber = invoiceDetails.FirstOrDefault().Invoice.InvoiceNumber,
                            ExchangeRate = exchangeRate,
                            UnitPrice = invoiceDetails.FirstOrDefault().Price ?? 0.0,
                            //Quantity = detail.Quality,
                            Quantity = invoiceDetails.Sum(id => id.Piece),
                            Tax = tax
                        };
                        if (entity.Quantity <= 0) continue;
                        if (entity.UnitPrice <= 0) continue;

                        if (detail.ExportFormTP_KD.Customer.CustomerTypeId != 7 &&
                            detail.ExportFormTP_KD.Customer.CustomerTypeId != 6) {
                            entity.CurrencyCode = "VND";
                            entity.UnitPrice = Math.Round(entity.UnitPrice * entity.ExchangeRate, 0);
                            entity.ExchangeRate = 1;
                        }
                        entity.Amount = entity.UnitPrice * entity.Quantity;
                        entity.AmountVnd = Math.Round(entity.Amount * entity.ExchangeRate * (tax / 100 + 1), 0);
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTaxInvoiceProductAuto", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.ProductCode).ThenBy(m => m.VFIDueDate)));
        }


        [GridAction]
        public ActionResult UpdateTaxInvoiceProductAuto(
            [Bind(Prefix = "inserted")] IEnumerable<InvoiceDetailTempModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<InvoiceDetailTempModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<InvoiceDetailTempModel> deletedDetails,
            int customerId, string currencyCode, int exchangeRate, int tax,
            string taxInvoiceCode, string taxInvoiceDate
            ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<InvoiceDetailTempModel>()));
            }
            if (!updatedDetails.Any(ud => ud.IsAdd))
                return View(new GridModel(new List<InvoiceDetailTempModel>()));
            updatedDetails = updatedDetails.Where(ud => ud.IsAdd).ToList();
            try {
                var ci = new CultureInfo("vi-VN");
                var date = string.IsNullOrWhiteSpace(taxInvoiceDate)
                               ? DateTime.Now
                               : Convert.ToDateTime(taxInvoiceDate, ci);
                if (string.IsNullOrWhiteSpace(currencyCode))
                    throw new AggregateException("Lỗi! Loại tiền tệ lỗi");
                if (exchangeRate <= 0 || (currencyCode.Equals("VND") && exchangeRate > 1))
                    throw new AggregateException("Lỗi! Tỉ giá lỗi " + currencyCode + "-" + exchangeRate);
                if (tax < 0)
                    throw new AggregateException("Lỗi! Thuế < 0");
                if (string.IsNullOrWhiteSpace(taxInvoiceCode))
                    throw new AggregateException("Lỗi! chưa nhập số hóa đơn!");
                using (var vfi = new tammaContext()) {
                    var taxInvoice =
                        vfi.TaxInvoices.FirstOrDefault(
                            ti => ti.TaxInvoiceList.Equals(taxInvoiceCode) && ti.Status != (byte)MyUtilities.Sales.Status.Cancel);
                    if (taxInvoice != null)
                        throw new AggregateException("Lỗi! Hóa đơn đã tồn tại");
                    taxInvoice = new TaxInvoice {
                        CustomerId = customerId,
                        TaxPercent = tax,
                        ExchangeRate = exchangeRate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        Currency = currencyCode,
                        SetupDate = date,
                        Status = (byte)MyUtilities.Sales.Status.InProcess,
                        TaxInvoiceList = taxInvoiceCode
                    };
                    var detailId = updatedDetails.FirstOrDefault().DetailId;
                    var groups = updatedDetails.Select(ud => new { ud.ProductId, ud.UnitPrice }).Distinct().ToList();
                    foreach (var group in groups) {
                        var updatedDetailsById = updatedDetails.Where(ud => ud.ProductId == group.ProductId &&
                                                                            ud.UnitPrice == group.UnitPrice).ToList();
                        foreach (var detail in updatedDetailsById) {
                            var exportDetail =
                                vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == detail.ExportDetailId);
                            if (exportDetail.IsInvoiced == true) {
                                throw new AggregateException("Vui lòng chọn lại sản phẩm để làm mới xuất kho");
                            }
                            var entity = new TaxInvoiceProductDetail {
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ExportDate = exportDetail.ExportFormTP_KD.DateTransporter,
                                ExportDetailId = detail.ExportDetailId,
                                UnitPrice = detail.UnitPrice,
                                Quantity = detail.Quantity,
                                TaxInvoiceId = taxInvoice.Id,
                                Active = true,
                            };
                            exportDetail.IsInvoiced = true;
                            var invoice = vfi.Invoices.FirstOrDefault(i => i.ExportId == exportDetail.ExportId);
                            if (exportDetail.ExportFormTP_KD.ExportFormTP_KDDetail.Count(od => od.IsInvoiced == false) == 0)
                                invoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                            else
                                invoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                            vfi.TaxInvoiceProductDetails.Add(entity);
                        }
                        var updatedDetail = updatedDetailsById.FirstOrDefault();
                        var taxInvoiceProduct = new TaxInvoiceProduct {
                            ProductId = updatedDetail.ProductId,
                            UnitPrice = updatedDetail.UnitPrice,
                            Quantity = updatedDetailsById.Sum(ud => ud.Quantity),
                            TaxInvoiceId = taxInvoice.Id,
                            TaxInvoice = taxInvoice,
                            IsFinish = true,
                        };
                        vfi.TaxInvoiceProducts.Add(taxInvoiceProduct);
                    }
                    vfi.TaxInvoices.Add(taxInvoice);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("AddExportInvoiceDetail", ex.Message);
            }
            return View(new GridModel(new List<InvoiceDetailTempModel>()));
        }

        int UpdateInvoiceStatus(List<long> invoiceIds) {
            var saved = 0;
            try {
                using (var vfi = new tammaContext()) {
                    var invoices = vfi.Invoices.Where(i => invoiceIds.Contains(i.InvoiceId));
                    foreach (var invoice in invoices) {
                        if (invoice.ExportFormTP_KD.ExportFormTP_KDDetail.Count(od => od.IsInvoiced == false) == 0)
                            invoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                        else
                            invoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                    }
                    saved += vfi.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
            return saved;
        }

        [GridAction]
        public ActionResult SelectTaxInvoiceProduct() {
            return View(new GridModel(new List<InvoiceDetailTempModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateTaxInvoiceProduct(
            [Bind(Prefix = "inserted")] IEnumerable<InvoiceDetailTempModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<InvoiceDetailTempModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<InvoiceDetailTempModel> deleteds,
            int customerId, string taxInvoiceDate, string taxInvoiceCode, string currencyCode
            , int tax, int exchangeRate) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
                }
                var ci = new CultureInfo("vi-VN");
                var date = string.IsNullOrWhiteSpace(taxInvoiceDate)
                               ? DateTime.Now
                               : Convert.ToDateTime(taxInvoiceDate, ci);
                using (var vfi = new tammaContext()) {

                    if (string.IsNullOrWhiteSpace(taxInvoiceCode))
                        throw new AggregateException("Lỗi! chưa nhập số hóa đơn!");
                    if (exchangeRate <= 0)
                        throw new AggregateException("Lỗi! Tỉ giá lỗi. (<= 0)");
                    var taxInvoice =
                        vfi.TaxInvoices.FirstOrDefault(
                            ti => ti.TaxInvoiceList.Equals(taxInvoiceCode) && ti.Status != (byte)MyUtilities.Sales.Status.Cancel);
                    if (taxInvoice != null)
                        throw new AggregateException("Lỗi! Hóa đơn đã tồn tại");
                    taxInvoice = new TaxInvoice {
                        CustomerId = customerId,
                        TaxPercent = tax,
                        ExchangeRate = exchangeRate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        Currency = currencyCode,
                        SetupDate = date,
                        Status = (byte)MyUtilities.Sales.Status.Waiting,
                        TaxInvoiceList = taxInvoiceCode
                    };
                    var list = new List<TaxInvoiceProduct>();
                    foreach (var inserted in inserteds) {
                        var taxInvoiceProduct = new TaxInvoiceProduct {
                            ProductId = inserted.ProductId,
                            UnitPrice = inserted.UnitPrice,
                            Quantity = inserted.Quantity,
                            TaxInvoiceId = taxInvoice.Id,
                            TaxInvoice = taxInvoice,
                            IsFinish = false,
                        };
                        list.Add(taxInvoiceProduct);
                    }
                    taxInvoice.TotalAmount = Math.Round(list.Sum(l => l.Quantity * l.UnitPrice), 2);
                    vfi.TaxInvoices.Add(taxInvoice);
                    vfi.TaxInvoiceProducts.AddRange(list);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateTaxInvoiceProduct", ex.Message);
            }
            return View(new GridModel(new List<InvoiceDetailTempModel>()));
        }


        public ActionResult SelectComboBoxAddTaxInvoiceProductDetail(int customerId) {
            var model = new List<TaxInvoiceModel>();
            using (var vfi = new tammaContext()) {
                var taxInvoiceList =
                    vfi.TaxInvoices.Where(
                        ti =>
                            ti.CustomerId == customerId &&
                            ti.SetupDate != null &&
                            ti.SetupDate > MyUtilities.Sales.StartTaxInvoiceDate &&
                            (ti.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                             ti.Status == (byte)MyUtilities.Sales.Status.InProcess));
                foreach (var taxInvoice in taxInvoiceList) {
                    var entity = new TaxInvoiceModel {
                        TaxInvoiceList = taxInvoice.TaxInvoiceList,
                        CustomerCode = taxInvoice.Customer.CustomerCode,
                        TotalAmount = taxInvoice.TotalAmount ?? 0.0,
                        TaxInvoiceId = taxInvoice.Id,
                        RequiredAmount = taxInvoice.TotalAmount ?? 0.0,
                        ExchangeRate = taxInvoice.ExchangeRate,
                        CurrencyCode = taxInvoice.Currency,
                        TotalAmountVND = (taxInvoice.TotalAmount ?? 0) * (taxInvoice.ExchangeRate),
                        SetupDate = taxInvoice.SetupDate.Value
                    };

                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "TaxInvoiceId", "TaxInvoiceList"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [GridAction]
        public ActionResult SelectInvoiceDetailById(string detailId) {
            if (string.IsNullOrWhiteSpace(detailId)) {
                return View(new GridModel(new List<InvoiceDetailTempModel>()));
            }
            // material
            var model = new List<InvoiceDetailTempModel>();
            try {

                using (var vfi = new tammaContext()) {
                    var eDetail = Convert.ToInt32(detailId);
                    var taxInvoiceProduct = vfi.TaxInvoiceProducts.FirstOrDefault(ed => ed.DetailId == eDetail);
                    if (taxInvoiceProduct == null)
                        return View(new GridModel(new List<OrderDetailModel>()));
                    var taxInvoiceProductDetails =
                        vfi.TaxInvoiceProductDetails.Where(
                            tipd =>
                            tipd.TaxInvoiceId == taxInvoiceProduct.TaxInvoiceId &&
                            tipd.ExportFormTP_KDDetail.ProductId == taxInvoiceProduct.ProductId
                            && tipd.UnitPrice == taxInvoiceProduct.UnitPrice
                            && tipd.Active == true).ToList();
                    if (taxInvoiceProduct.Quantity == taxInvoiceProductDetails.Sum(tipd => tipd.Quantity))
                        throw new AggregateException("Sản phẩm đã đủ số lượng tương ứng");
                    var currencyCode = taxInvoiceProduct.TaxInvoice.Currency;
                    var exportDetails = (from ed in vfi.ExportFormTP_KDDetail
                                         where
                                             ed.ExportFormTP_KD.DateTransporter >= MyUtilities.Sales.StartTaxInvoiceDate
                                             &&
                                             ed.ExportFormTP_KD.CustomerId == taxInvoiceProduct.TaxInvoice.CustomerId
                                             //&&
                                             //ed.TransactionDetail.Transaction.Status ==
                                             //(byte)MyUtilities.Transaction.Status.Approved
                                             && ed.ProductId == taxInvoiceProduct.ProductId
                                             && ed.InvoiceDetails.Any(id => id.Active && id.Invoice.Active)
                                             && ed.IsInvoiced == false
                                         select new {
                                             ed.DetailId,
                                             ed.ExportFormTP_KD,
                                             ed.ProductId,
                                             ed.Product.ProductCode,
                                             ed.Quality,
                                             ExportDate = ed.ExportFormTP_KD.DateTransporter.Value,
                                             VFIDueDate = ed.ExportFormTP_KD.DateTransporter.Value,
                                             InvoiceDetails = ed.InvoiceDetails.Where(id => id.Active && id.Invoice.Active),

                                         }).ToList();
                    foreach (var detail in exportDetails) {
                        var export = detail.ExportFormTP_KD;
                        if (export == null)
                            throw new AggregateException("Lỗi! Không tìm thấy phiếu xuất!");
                        var transaction =
                            vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(export.TransactionCode) &&
                                                                 t.Status ==
                                                                 (byte)MyUtilities.Transaction.Status.Approved);
                        if (transaction == null) continue;
                        var invoiceDetails = detail.InvoiceDetails;
                        if (!invoiceDetails.Any()) continue;
                        var entity = new InvoiceDetailTempModel {
                            DetailId = eDetail,
                            ProductCode = detail.ProductCode,
                            ExportedDateStr = detail.ExportDate.ToString("dd/MM/yyyy"),
                            ExportDetailId = detail.DetailId,
                            VFIDueDate = detail.VFIDueDate,
                            CurrencyCode = currencyCode,

                            InvoiceNumber = invoiceDetails.FirstOrDefault().Invoice.InvoiceNumber,
                            ExchangeRate = invoiceDetails.FirstOrDefault().Invoice.ExchangeRate,
                            UnitPrice = invoiceDetails.FirstOrDefault().Price.Value,
                            //Quantity = detail.Quality,
                            Quantity = invoiceDetails.Sum(id => id.Piece)

                        };
                        if (entity.Quantity <= 0) continue;
                        //entity.CurrencyCode = invoiceDetails.FirstOrDefault().OrderDetail.Order.CurrencyCode;
                        //entity.Quantity = invoiceDetails.Sum(id => id.Piece);
                        if (detail.ExportFormTP_KD.Customer.CustomerTypeId != 7 &&
                            detail.ExportFormTP_KD.Customer.CustomerTypeId != 6) {
                            entity.ExchangeRate = invoiceDetails.FirstOrDefault().Invoice.ExchangeRate;
                            entity.CurrencyCode = "VND";
                        }
                        if (entity.CurrencyCode.Equals("VND"))
                            entity.UnitPrice = Math.Round(entity.UnitPrice * entity.ExchangeRate, 0);
                        //if(entity.CurrencyCode.Equals("VND")){
                        //    entity.UnitPrice = Math.Round(entity.UnitPrice,0);
                        //}else{
                        //    entity.UnitPrice = Math.Round(entity.UnitPrice);
                        //}

                        if (Math.Round(entity.UnitPrice - taxInvoiceProduct.UnitPrice, 0) != 0)
                            continue;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectInvoiceDetailById", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult UpdateProductTaxInvoiceDetail(
            [Bind(Prefix = "inserted")] IEnumerable<InvoiceDetailTempModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<InvoiceDetailTempModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<InvoiceDetailTempModel> deletedDetails
            //, int? id
            ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<InvoiceDetailTempModel>()));
            }
            if (!updatedDetails.Any(ud => ud.IsAdd))
                return View(new GridModel(new List<InvoiceDetailTempModel>()));
            updatedDetails = updatedDetails.Where(ud => ud.IsAdd).ToList();
            try {
                using (var vfi = new tammaContext()) {
                    var detailId = updatedDetails.FirstOrDefault().DetailId;
                    var taxInvoiceProduct = vfi.TaxInvoiceProducts.FirstOrDefault(tip => tip.DetailId == detailId);
                    var taxInvoiceProductDetails =
                        vfi.TaxInvoiceProductDetails.Where(
                            tipd =>
                                tipd.TaxInvoiceId == taxInvoiceProduct.TaxInvoiceId &&
                                tipd.ExportFormTP_KDDetail.ProductId == taxInvoiceProduct.ProductId
                                && tipd.UnitPrice == taxInvoiceProduct.UnitPrice
                                && tipd.Active == true).ToList();
                    if (taxInvoiceProduct.Quantity <
                        updatedDetails.Sum(ud => ud.Quantity) + taxInvoiceProductDetails.Sum(tipd => tipd.Quantity))
                        throw new AggregateException("Sản phẩm đã quá số lượng tương ứng" +
                                                     (taxInvoiceProduct.Quantity -
                                                      updatedDetails.Sum(ud => ud.Quantity) +
                                                      taxInvoiceProductDetails.Sum(tipd => tipd.Quantity)));
                    if (taxInvoiceProduct.TaxInvoice.Customer.CustomerTypeId == 7 ||
                        taxInvoiceProduct.TaxInvoice.Customer.CustomerTypeId == 6) {
                        var check = updatedDetails.Where(ud => ud.UnitPrice != taxInvoiceProduct.UnitPrice);
                        if (check.Any()) {
                            throw new AggregateException("Đơn giá sai so với hóa đơn!");
                        }
                    }
                    foreach (var detail in updatedDetails) {
                        var exportDetail =
                            vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == detail.ExportDetailId);
                        if (exportDetail.IsInvoiced == true) {
                            throw new AggregateException("Vui lòng chọn lại sản phẩm để làm mới xuất kho");
                        }
                        //var entity =
                        //    vfi.TaxInvoiceProductDetails.FirstOrDefault(
                        //        tip => tip.ExportDetailId == exportDetail.DetailId);
                        //if (entity == null)
                        //{
                        //    ///
                        //}
                        var entity = new TaxInvoiceProductDetail {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ExportDate = exportDetail.ExportFormTP_KD.DateTransporter,
                            ExportDetailId = exportDetail.DetailId,
                            UnitPrice = taxInvoiceProduct.UnitPrice,
                            Quantity = detail.Quantity,
                            TaxInvoiceId = taxInvoiceProduct.TaxInvoiceId,
                            Active = true,
                        };
                        exportDetail.IsInvoiced = true;
                        var invoice = vfi.Invoices.FirstOrDefault(i => i.ExportId == exportDetail.ExportId);
                        if (exportDetail.ExportFormTP_KD.ExportFormTP_KDDetail.Count(od => od.IsInvoiced == false) == 0)
                            invoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                        else
                            invoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                        vfi.TaxInvoiceProductDetails.Add(entity);
                    }
                    if (taxInvoiceProduct.Quantity ==
                        updatedDetails.Sum(ud => ud.Quantity) + taxInvoiceProductDetails.Sum(tipd => tipd.Quantity))
                        taxInvoiceProduct.IsFinish = true;
                    vfi.SaveChanges();
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == taxInvoiceProduct.TaxInvoiceId);
                    if (taxInvoice.TaxInvoiceProducts.FirstOrDefault(tip => !tip.IsFinish) == null) {
                        var taxInvoideDetails =
                            vfi.TaxInvoiceDetails.Where(
                                tid =>
                                    tid.TaxInvoiceId == taxInvoice.Id &&
                                    tid.Status == (byte)MyUtilities.Sales.Status.Completed).ToList();
                        var moneyAdded = taxInvoideDetails.Sum(tid => Math.Abs(tid.Money.Value));
                        if (Math.Round(
                                (moneyAdded - taxInvoice.TaxInvoiceProducts.Sum(tip => tip.UnitPrice * tip.Quantity)), 2)
                            == 0) {
                            taxInvoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                        }
                        else {
                            taxInvoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                        }
                        vfi.SaveChanges();
                        //taxInvoice.Status=(byte)
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("AddExportInvoiceDetail", ex.Message);
            }
            return View(new GridModel(new List<InvoiceDetailTempModel>()));
        }

        [HttpPost]
        public ActionResult GetProductUnitPrice(int productId) {
            try {

                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null)
                        return Json("-9");
                    return Json(new object[]
                        {
                            product.UnitPrice ?? 0
                        });
                }
            }
            catch (Exception) {
                return Json("-1");
            }
        }

        [HttpPost]
        public ActionResult GetCustomerTaxInvoiceById(int customerId) {
            try {

                using (var vfi = new tammaContext()) {
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    if (customer == null)
                        return Json("-9");
                    var last = (from o in vfi.TaxInvoices
                                orderby o.SetupDate descending
                                where o.SetupDate != null &&
                                      o.Status != (byte)MyUtilities.Sales.Status.Cancel
                                      && o.CustomerId == customerId
                                select o).FirstOrDefault();
                    var info = new TaxInvoiceModel {
                        CurrencyCode = "VND",
                        ExchangeRate = 1
                    };
                    if (last != null) {
                        info.CurrencyCode = last.Currency;
                        info.ExchangeRate = last.ExchangeRate;
                        info.Tax = last.TaxPercent;
                    }
                    return Json(info);
                }
            }
            catch (Exception) {
                return Json("-1");
            }
        }

        [GridAction]
        public ActionResult SelectTaxInvoiceWaiting(int month,int year) {
            try {
                return View(new GridModel(TaxInvoiceWaiting(month, year).OrderByDescending(o => o.ModifiedDate)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectPrepareTaxInvoice", ex.Message);
                return View(new GridModel(new List<TaxInvoiceModel>()));
            }
        }


        private List<TaxInvoiceModel> TaxInvoiceWaiting(int month, int year) {
            var model = new List<TaxInvoiceModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var startTaxInvoiceDate = MyUtilities.Sales.StartTaxInvoiceDate;
                    var taxInvoices = vfi.TaxInvoices.Where(ti => (ti.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                                                   ti.Status == (byte)MyUtilities.Sales.Status.InProcess)
                                                                  && ti.SetupDate > startTaxInvoiceDate
                                                                  && ti.SetupDate.Value.Month == month
                                                                  && ti.SetupDate.Value.Year == year
                        //&& ti.TaxInvoiceList.Equals("VP/14P-0055")
                        ).ToList();
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
                        var entity = new TaxInvoiceModel {
                            TaxInvoiceList = taxInvoice.TaxInvoiceList,
                            TaxInvoiceId = taxInvoice.Id,
                            SetupDate = taxInvoice.SetupDate,
                            ModifiedDate = taxInvoice.ModifiedDate ?? DateTime.Now,
                            Tax = taxInvoice.TaxPercent,
                            ExchangeRate = taxInvoice.ExchangeRate,
                            CurrencyCode = taxInvoice.Currency,
                            CustomerCode = taxInvoice.Customer.CustomerCode,
                            TotalQuantity = taxInvoice.TaxInvoiceProducts.Sum(tip => tip.Quantity),
                            TotalAmount = taxInvoice.TaxInvoiceProducts.Sum(tip => tip.Quantity * tip.UnitPrice)
                        };
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
        public ActionResult CancelTaxInvoice(int taxInvoiceId, int month, int year) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == taxInvoiceId);
                    var taxInvoiceProductDetail =
                        vfi.TaxInvoiceProductDetails.FirstOrDefault(
                            tipd => tipd.TaxInvoiceId == taxInvoice.Id && tipd.Active == true);
                    if (taxInvoiceProductDetail != null)
                        throw new AggregateException("Vui lòng hủy chi tiết xuất kho trước!");
                    if (taxInvoice.Status == (byte)MyUtilities.Sales.Status.Waiting)
                        taxInvoice.Status = (byte)MyUtilities.Sales.Status.Cancel;
                    else
                        throw new AggregateException("Vui lòng F5 Refresh lại để có danh sách mới nhất");

                    vfi.SaveChanges();
                }
                return View(new GridModel(TaxInvoiceWaiting(month, year).OrderByDescending(o => o.ModifiedDate)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("ApproveTaxInvoice", ex.Message);
                return View(new GridModel(new List<TaxInvoiceModel>()));
            }
        }

        [GridAction]
        public ActionResult SelectTaxInvoiceProductById(int taxInvoiceId) {
            var model = new List<InvoiceDetailTempModel>();
            try {
                model = TaxInvoiceProductById(taxInvoiceId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductInTaxInvoice", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<InvoiceDetailTempModel> TaxInvoiceProductById(int taxInvoiceId) {
            var model = new List<InvoiceDetailTempModel>();
            if (taxInvoiceId == 0)
                return model;
            try {
                using (var vfi = new tammaContext()) {
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == taxInvoiceId);

                    foreach (var detail in taxInvoice.TaxInvoiceProducts) {
                        var entity = new InvoiceDetailTempModel {
                            DetailId = detail.DetailId,
                            ProductCode = detail.Product.ProductCode,
                            ProductId = detail.Product.ProductId,
                            Quantity = detail.Quantity,
                            UnitPrice = detail.UnitPrice,
                            CurrencyCode = taxInvoice.Currency,
                            TaxPercent = taxInvoice.TaxPercent,
                            ExchangeRate = taxInvoice.ExchangeRate,
                            Amount = detail.Quantity * detail.UnitPrice,
                        };
                        var taxInvoiceProductDetails =
                            vfi.TaxInvoiceProductDetails.Where(
                                tipd =>
                                tipd.TaxInvoiceId == detail.TaxInvoiceId &&
                                tipd.ExportFormTP_KDDetail.ProductId == detail.ProductId &&
                                tipd.UnitPrice == detail.UnitPrice &&
                                tipd.Active == true)
                               .ToList();
                        entity.Receive = taxInvoiceProductDetails.Sum(tipd => tipd.Quantity);
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductInTaxInvoice", ex.Message);
            }
            return model.OrderBy(m => m.ProductCode)
                        .ThenBy(m => m.ExportedDate)
                        .ThenBy(m => m.InvoiceNumber)
                        .ToList();
        }

        [GridAction]
        public ActionResult CancelTaxInvoiceProduct(long detailId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var taxInvoiceProduct = vfi.TaxInvoiceProducts.FirstOrDefault(tip => tip.DetailId == detailId);

                    var productDetails =
                        vfi.TaxInvoiceProductDetails.Where(
                            tipd =>
                            tipd.ExportFormTP_KDDetail.ProductId == taxInvoiceProduct.ProductId &&
                            tipd.UnitPrice == taxInvoiceProduct.UnitPrice &&
                            tipd.Active == true &&
                            tipd.TaxInvoiceId == taxInvoiceProduct.TaxInvoiceId);
                    foreach (var productDetail in productDetails) {
                        //productDetail.Active = false;
                        var exportDetail =
                            vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == productDetail.ExportDetailId);
                        exportDetail.IsInvoiced = false;
                        var invoice = vfi.Invoices.FirstOrDefault(i => i.ExportId == exportDetail.ExportId);
                        //var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == exportDetail.ExportId);
                        //if (!export.ExportFormTP_KDDetail.Any(ti => ti.IsInvoiced == true))
                        invoice.Status = (byte)MyUtilities.Sales.Status.Waiting;
                        //else
                        //    invoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                    }
                    taxInvoiceProduct.IsFinish = false;
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == taxInvoiceProduct.TaxInvoiceId);
                    taxInvoice.Status = (byte)MyUtilities.Sales.Status.Waiting;
                    vfi.TaxInvoiceProductDetails.RemoveRange(productDetails);
                    vfi.SaveChanges();
                    return View(new GridModel(TaxInvoiceProductById(taxInvoiceProduct.TaxInvoiceId)));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CancelTaxInvoiceProduct", ex.Message);
            }
            return View(new GridModel(new List<InvoiceDetailTempModel>()));
        }

        [GridAction]
        public ActionResult SelectTaxInvoiceProductDetailById(long detailId) {
            var model = new List<InvoiceDetailTempModel>();
            try {
                model = TaxInvoiceProductDetailByDetailId(detailId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductDetailInTaxInvoiceById", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<InvoiceDetailTempModel> TaxInvoiceProductDetailByDetailId(long detailId) {
            var model = new List<InvoiceDetailTempModel>();
            using (var vfi = new tammaContext()) {
                var taxInvoiceProduct = vfi.TaxInvoiceProducts.FirstOrDefault(tip => tip.DetailId == detailId);

                var productDetails =
                    vfi.TaxInvoiceProductDetails.Where(
                        e =>
                        e.TaxInvoiceId == taxInvoiceProduct.TaxInvoiceId &&
                        e.UnitPrice == taxInvoiceProduct.UnitPrice &&
                        e.ExportFormTP_KDDetail.ProductId == taxInvoiceProduct.ProductId
                        && e.Active == true);
                foreach (var detail in productDetails) {
                    //var exportDetail =
                    //    vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == detail.ExportDetailId);
                    var invoiceDetail = vfi.InvoiceDetails.FirstOrDefault(i => i.ExportDetailId == detail.ExportDetailId);
                    //var order = vfi.Orders.FirstOrDefault(o => o.OrderId == invoice.OrderId);
                    var entity = new InvoiceDetailTempModel {
                        DetailId = detail.PDetailId,
                        ProductCode = detail.ExportFormTP_KDDetail.Product.ProductCode,
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice,
                        ExportedDate = detail.ExportDate ?? DateTime.Now,
                        CurrencyCode = taxInvoiceProduct.TaxInvoice.Currency,
                        Amount = detail.Quantity * detail.UnitPrice,
                        InvoiceNumber = invoiceDetail.Invoice.InvoiceNumber,
                        OrderNumber = invoiceDetail.OrderDetail.Order.OrderNumber
                    };
                    //entity.Amount = entity.Quantity * entity.UnitPrice;

                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.ProductCode)
                        .ThenBy(m => m.ExportedDate)
                        .ThenBy(m => m.InvoiceNumber)
                        .ToList();
        }

        [GridAction]
        public ActionResult CancelTaxInvoiceProductDetail(int detailId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var productDetail = vfi.TaxInvoiceProductDetails.FirstOrDefault(tip => tip.PDetailId == detailId);
                    //productDetail.Active = false;
                    var exportDetail =
                        vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == productDetail.ExportDetailId);
                    exportDetail.IsInvoiced = false;
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.ExportId == exportDetail.ExportId);
                    invoice.Status = (byte)MyUtilities.Sales.Status.Waiting;
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(ti => ti.Id == productDetail.TaxInvoiceId);
                    taxInvoice.Status = (byte)MyUtilities.Sales.Status.Waiting;
                    var taxInvoiceProduct =
                        vfi.TaxInvoiceProducts.FirstOrDefault(
                            tip => tip.ProductId == productDetail.ExportFormTP_KDDetail.ProductId &&
                                   tip.TaxInvoiceId == productDetail.TaxInvoiceId);
                    vfi.TaxInvoiceProductDetails.Remove(productDetail);
                    vfi.SaveChanges();
                    return View(new GridModel(TaxInvoiceProductDetailByDetailId(taxInvoiceProduct.DetailId)));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CancelTaxInvoiceProductDetail", ex.Message);
            }
            return View(new GridModel(new List<InvoiceDetailTempModel>()));
        }

        #region New Approve Order Formula

        #endregion

        #endregion

        #region ForecastOrder

        [GridAction]
        public ActionResult SelectForecast(string fromDate, string toDate, int status, int employeeId, int customerId) {
            var model = new List<ForecastModel>();
            try {
                model =
                    GetListForecast(fromDate, toDate, status, employeeId, customerId)
                        .OrderBy(m => m.ForecastDate)
                        .ToList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectForecast", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<ForecastModel> GetListForecast(string fromDate, string toDate, int status, int employeeId, int customerId) {
            var model = new List<ForecastModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                DateTime toDay = DateTime.Now;

                var fDate = string.IsNullOrWhiteSpace(fromDate)
                                ? new DateTime(toDay.Year, toDay.Month, 1)
                                : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                                ? toDay
                                : Convert.ToDateTime(toDate, ci);
                var saleManagement = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.SaleManagement);

                using (var vfi = new tammaContext()) {
                    var products = (from x in vfi.Products
                                    where x.Active &&
                                        (customerId == 0 || x.CustomerId == customerId) &&
                                        (employeeId == 0 || x.Customer.EmployeeId == employeeId)
                                    //orderby x.Customer.CustomerCode, x.ProductCode
                                    select new {
                                        x.ProductId,
                                        x.ProductCode,
                                        x.Customer.CustomerCode,
                                        x.Customer.Employee.EmployeeName,
                                    }).ToList();
                    var productIds = products.Select(x => x.ProductId).ToList();

                    var forecasts = (from fo in vfi.ForecastOrders
                                     where ((status == 0 && fo.Status != (byte)MyUtilities.Transaction.Status.Cancel)
                                                || fo.Status == status) &&
                                            productIds.Contains(fo.ProductId) &&
                                            fo.Quantity > 0 &&
                                           (fromDate == null || fo.ForecastDate >= fDate) &&
                                           (toDate == null || fo.ForecastDate <= tDate)
                                     orderby fo.ForecastDate
                                     select new {
                                         fo.ForecastOrderId,
                                         fo.ProductId,
                                         fo.ForecastDate,
                                         fo.Quantity,
                                         fo.ModifiedDate,
                                         fo.ModifiedUser,
                                         fo.Status,
                                     }).ToList();
                    //var forecasts2 = vfi.ForecastOrders.Where(x => x.Status == status).ToList();
                    //var forecasts3 = vfi.ForecastOrders.Where(x => x.Status == (byte)status).ToList();
                    //var forecasts4 = vfi.ForecastOrders.Where(x => x.Status == 1).ToList();
                    //var forecasts5 = vfi.ForecastOrders.Where(x => x.Status != 2 && x.Status != 4 && x.Status != 3).ToList();
                    //var forecasts = (from fo in vfi.ForecastOrders
                    //                 where productIds.Contains(fo.ProductId) &&
                    //                        fo.Quantity > 0 &&
                    //                        fo.Status == (byte)status
                    //                 orderby fo.ForecastDate
                    //                 select fo).ToList();
                    // //forecasts = forecasts.Where(x=> (status == 0 && x.Status == (byte)MyUtilities.Transaction.Status.Approved) || x.Status == status).ToList();
                    // forecasts = forecasts.Where(x => ((fromDate == null || x.ForecastDate >= fDate) && (toDate == null || x.ForecastDate <= tDate))).ToList();
                    //if (!string.IsNullOrWhiteSpace(toDate))
                    //    forecasts = forecasts.Where(f => f.ForecastDate >= fDate && f.ForecastDate <= tDate).ToList();
                    if (status != 0) {
                        if (!forecasts.Any()) return model;
                        productIds = forecasts.Select(x => x.ProductId).Distinct().ToList();
                        products = products.Where(x => productIds.Contains(x.ProductId)).ToList();
                    }
                    var lastOrderMonth = fDate.AddMonths(-1);
                    var lastPeriodYear = new DateTime(fDate.Year - 1, 1, 1);
                    if (forecasts.Any()) {
                        var lastForecast = forecasts.FirstOrDefault().ForecastDate;
                        lastOrderMonth = new DateTime(lastForecast.Year, lastForecast.Month, 1).AddMonths(-1);
                        lastPeriodYear = new DateTime(lastForecast.Year - 1, 1, 1);
                    }
                    var periods = (from pip in vfi.ProductInventoryPeriods
                                   where productIds.Contains(pip.ProductId) &&
                                        pip.WarehouseId == MyUtilities.Warehouse.Business &&
                                        pip.PeriodDate >= lastPeriodYear &&
                                        pip.PeriodDate < tDate
                                   //pip.PeriodDate >= fromDate
                                   select new {
                                       pip.ProductId,
                                       pip.PeriodDate,
                                       Quantity = pip.LastPeriodQuantity - pip.EarlyPeriodQuantity
                                   }).ToList();

                    var lastMonthOrders = (from od in vfi.OrderDetails
                                           where od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                                 od.Order.DueDate != null &&
                                                 od.Order.DueDate >= lastOrderMonth &&
                                               //od.Order.DueDate.Value.Month == lastMonth.Month &&
                                               //od.Order.DueDate.Value.Year == lastMonth.Year &&
                                                 productIds.Contains(od.ProductId) &&
                                                 od.OrderQty > 0
                                           select new {
                                               od.ProductId,
                                               DueDate = od.Order.DueDate.Value,
                                               OrderQty = od.OrderQty ?? 0,
                                           }).ToList();
                    var orders = (from od in vfi.OrderDetails
                                  where od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                        od.Order.Status != (byte)MyUtilities.Sales.Status.Completed &&
                                        od.Order.DueDate != null &&
                                        od.RequiedNumber > 0 &&
                                        productIds.Contains(od.ProductId)
                                  select new {
                                      od.ProductId,
                                      od.RequiedNumber,
                                  }).ToList();

                    var warehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                    var invs = (from pi in vfi.ProductInventories
                                where productIds.Contains(pi.ProductId) &&
                                       warehouses.Contains(pi.WarehouseId) &&
                                       pi.TotalQty > 0
                                select new {
                                    pi.ProductId,
                                    pi.TotalQty,
                                }).ToList();

                    foreach (var product in products) {
                        var forecastsById = forecasts.Where(x => x.ProductId == product.ProductId).ToList();
                        if (forecastsById.Any()) {
                            foreach (var forecast in forecastsById) {
                                var date = forecast.ForecastDate;
                                var entity = new ForecastModel {
                                    ProductId = product.ProductId,
                                    ProductCode = product.ProductCode,
                                    CustomerCode = product.CustomerCode,
                                    EmployeeName = product.EmployeeName,
                                    ForecastDate = date,
                                    Quantity = forecast.Quantity,
                                    ForecastOrderId = forecast.ForecastOrderId,
                                    ModifiedUser = forecast.ModifiedUser,
                                    ModifiedDate = forecast.ModifiedDate,
                                    SaleManagement = saleManagement,
                                    Status = forecast.Status,
                                    StatusName = MyUtilities.Transaction.CastText.GetTextStatus(forecast.Status)
                                };
                                var startOfMonth = new DateTime(date.Year, date.Month, 1);
                                var startOfLastYear = new DateTime(date.Year - 1, 1, 1);
                                var periodsById = periods.Where(x => x.ProductId == entity.ProductId &&
                                                                     x.PeriodDate < startOfMonth &&
                                                                     x.PeriodDate >= startOfLastYear)
                                                        .ToList();

                                // tong xuat nam truoc
                                var startOfYear = new DateTime(date.Year, 1, 1);
                                var period = periodsById.Where(p => p.PeriodDate <= startOfYear).ToList();
                                entity.LastYearTotalQuantity = MyUtilities.Function.RoundUp(period.Sum(p => p.Quantity), -3);

                                //binh quan trong nam
                                var monthCount = period.Select(p => p.PeriodDate.Month).Distinct().Count();
                                if (monthCount > 0) {
                                    entity.LastYearAvgQuantity =
                                        MyUtilities.Function.RoundUp(entity.LastYearTotalQuantity / monthCount, -3);
                                }
                                // cung ky nam truoc
                                var lastYear1 = startOfMonth.AddYears(-1);
                                period = periodsById.Where(p => p.PeriodDate.Month == lastYear1.Month &&
                                                                p.PeriodDate.Year == lastYear1.Year).ToList();
                                entity.LastYearQuantity = MyUtilities.Function.RoundUp(period.Sum(p => p.Quantity), -3);

                                // binh quan 3 thang
                                var lastMonth3 = startOfMonth.AddMonths(-3);
                                period = periodsById.Where(p => p.PeriodDate >= lastMonth3).ToList();
                                monthCount = period.Select(p => p.PeriodDate.Month).Distinct().Count();
                                if (monthCount > 0) {
                                    entity.LastMonthAvgQuantity =
                                        MyUtilities.Function.RoundUp(period.Sum(p => p.Quantity) / monthCount, -3);
                                }
                                // don hang thang truoc
                                var lastMonth = startOfMonth.AddMonths(-1);
                                entity.LastMonthOrderQuantity = lastMonthOrders.Where(x => x.ProductId == entity.ProductId &&
                                                                                            x.DueDate.Month == lastMonth.Month &&
                                                                                            x.DueDate.Year == lastMonth.Year)
                                                                                .Sum(x => x.OrderQty);

                                // don hang con lai
                                entity.TotalOrder = orders.Where(x => x.ProductId == entity.ProductId).Sum(x => x.RequiedNumber);

                                // ton tong
                                entity.TotalInv = invs.Where(x => x.ProductId == entity.ProductId).Sum(x => x.TotalQty);

                                model.Add(entity);
                            }
                        }
                        else {
                            var date = fDate;
                            var entity = new ForecastModel {
                                ProductId = product.ProductId,
                                ProductCode = product.ProductCode,
                                CustomerCode = product.CustomerCode,
                                EmployeeName = product.EmployeeName,
                                ForecastDate = date,
                                Quantity = 0,
                                ForecastOrderId = 0,
                                ModifiedUser = "",
                                ModifiedDate = DateTime.Now,
                                SaleManagement = saleManagement,
                            };
                            var startOfMonth = new DateTime(date.Year, date.Month, 1);
                            var startOfLastYear = new DateTime(date.Year - 1, 1, 1);
                            var periodsById = periods.Where(x => x.ProductId == entity.ProductId &&
                                                                 x.PeriodDate < startOfMonth &&
                                                                 x.PeriodDate >= startOfLastYear)
                                                    .ToList();

                            // tong xuat nam truoc
                            var startOfYear = new DateTime(date.Year, 1, 1);
                            var period = periodsById.Where(p => p.PeriodDate <= startOfYear).ToList();
                            entity.LastYearTotalQuantity = MyUtilities.Function.RoundUp(period.Sum(p => p.Quantity), -3);

                            //binh quan trong nam
                            var monthCount = period.Select(p => p.PeriodDate.Month).Distinct().Count();
                            if (monthCount > 0) {
                                entity.LastYearAvgQuantity =
                                    MyUtilities.Function.RoundUp(entity.LastYearTotalQuantity / monthCount, -3);
                            }
                            // cung ky nam truoc
                            var lastYear1 = startOfMonth.AddYears(-1);
                            period = periodsById.Where(p => p.PeriodDate.Month == lastYear1.Month &&
                                                            p.PeriodDate.Year == lastYear1.Year).ToList();
                            entity.LastYearQuantity = MyUtilities.Function.RoundUp(period.Sum(p => p.Quantity), -3);

                            // binh quan 3 thang
                            var lastMonth3 = startOfMonth.AddMonths(-3);
                            period = periodsById.Where(p => p.PeriodDate >= lastMonth3).ToList();
                            monthCount = period.Select(p => p.PeriodDate.Month).Distinct().Count();
                            if (monthCount > 0) {
                                entity.LastMonthAvgQuantity =
                                    MyUtilities.Function.RoundUp(period.Sum(p => p.Quantity) / monthCount, -3);
                            }
                            // don hang thang truoc
                            var lastMonth = startOfMonth.AddMonths(-1);
                            entity.LastMonthOrderQuantity = lastMonthOrders.Where(x => x.ProductId == entity.ProductId &&
                                                                                        x.DueDate.Month == lastMonth.Month &&
                                                                                        x.DueDate.Year == lastMonth.Year)
                                                                            .Sum(x => x.OrderQty);

                            // don hang con lai
                            entity.TotalOrder = orders.Where(x => x.ProductId == entity.ProductId).Sum(x => x.RequiedNumber);

                            // ton tong
                            entity.TotalInv = invs.Where(x => x.ProductId == entity.ProductId).Sum(x => x.TotalQty);

                            model.Add(entity);
                        }

                    }
                }
                //return model;
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetListForecast", ex.Message);
            }
            return model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ThenBy(x => x.ForecastDate).ToList();
            //return model;
        }

        ForecastModel GetForecastInfo(int productId, int month, int year) {
            var entity = new ForecastModel();
            try {
                using (var vfi = new tammaContext()) {
                    var product = (from p in vfi.Products
                                   where p.ProductId == productId && p.Active
                                   select new {
                                       p.ProductId,
                                       p.ProductCode,
                                   }).FirstOrDefault();
                    if (product == null)
                        return entity;
                    var toDate = new DateTime(year, month, 1);
                    var fromDate = new DateTime(year - 1, 1, 1);
                    var lastMonth = toDate.AddMonths(-1);
                    var lastMonth3 = toDate.AddMonths(-3);
                    var lastYear1 = toDate.AddYears(-1);
                    var endYear = new DateTime(year, 1, 1);
                    entity = new ForecastModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        Quantity = 0,
                        ForecastDate = new DateTime(year, month, 1)
                    };

                    var periodsById = (from pip in vfi.ProductInventoryPeriods
                                       where productId == pip.ProductId &&
                                             pip.WarehouseId == MyUtilities.Warehouse.Business &&
                                             pip.PeriodDate < toDate &&
                                             pip.PeriodDate >= fromDate
                                       select new {
                                           pip.PeriodDate,
                                           pip.LastPeriodQuantity,
                                           pip.EarlyPeriodQuantity
                                       }).ToList();
                    // tong xuat nam truoc
                    var period = periodsById.Where(p => p.PeriodDate <= endYear).ToList();
                    entity.LastYearTotalQuantity =
                        MyUtilities.Function.RoundUp(period.Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity), -3);
                    //binh quan nam truoc
                    var countMonth = period.Select(p => p.PeriodDate.Month).Distinct().Count();
                    if (countMonth > 0)
                        entity.LastYearAvgQuantity =
                            MyUtilities.Function.RoundUp(entity.LastYearTotalQuantity / countMonth, -3);
                    // cung ky nam truoc
                    period = periodsById.Where(p => p.PeriodDate.Month == lastYear1.Month &&
                                                    p.PeriodDate.Year == lastYear1.Year).ToList();
                    entity.LastYearQuantity =
                        MyUtilities.Function.RoundUp(period.Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity), -3);
                    // binh quan 3 thang
                    period = periodsById.Where(p => p.PeriodDate >= lastMonth3).ToList();
                    countMonth = period.Select(p => p.PeriodDate.Month).Distinct().Count();
                    if (countMonth > 0)
                        entity.LastMonthAvgQuantity =
                            MyUtilities.Function.RoundUp(period.Sum(p => p.LastPeriodQuantity - p.EarlyPeriodQuantity) /
                                                         countMonth, -3);

                    // don hang thang truoc
                    entity.LastMonthOrderQuantity = (from od in vfi.OrderDetails
                                                     where (od.Status == (byte)MyUtilities.Sales.Status.InProcess ||
                                                            od.Status == (byte)MyUtilities.Sales.Status.Completed) &&
                                                           od.Order.DueDate != null &&
                                                           od.Order.DueDate.Value.Month == lastMonth.Month &&
                                                           od.Order.DueDate.Value.Year == lastMonth.Year &&
                                                           productId == od.ProductId
                                                     select od.OrderQty.Value).Sum();
                    //entity.LastMonthOrderQuantity = orders.Sum(o => o);
                    // don hang con lai
                    entity.TotalOrder = (from od in vfi.OrderDetails
                                         where (od.Status == (byte)MyUtilities.Sales.Status.InProcess ||
                                                od.Status == (byte)MyUtilities.Sales.Status.Completed) &&
                                               od.Order.DueDate != null &&
                                               od.RequiedNumber > 0 &&
                                               productId == od.ProductId
                                         select od.RequiedNumber).Sum();
                    //entity.TotalOrder = orders.Sum(o => o.RequiedNumber);

                    // ton tong
                    var warehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                    entity.TotalInv = (from pi in vfi.ProductInventories
                                       where pi.ProductId == productId && warehouses.Contains(pi.WarehouseId) &&
                                       pi.TotalQty > 0
                                       select pi.TotalQty).Sum();
                    //entity.TotalInv = productInvs.Sum(pi => pi.TotalQty);

                    // da co du bao
                    var forecastOrder = (from fo in vfi.ForecastOrders
                                         where productId == fo.ProductId &&
                                               fo.ForecastDate.Month == month &&
                                               fo.ForecastDate.Year == year
                                         select new {
                                             fo.Quantity,
                                             fo.ForecastDate,
                                             fo.Status
                                         }).FirstOrDefault();
                    if (forecastOrder != null) {
                        entity.Quantity = forecastOrder.Quantity;
                        entity.ForecastDate = forecastOrder.ForecastDate;
                        entity.StatusName = MyUtilities.Transaction.CastText.GetTextStatus(forecastOrder.Status);
                    }
                    else {
                        entity.StatusName = "Chưa tạo";
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetForecastInfo", ex.Message);
            }
            return entity;
        }

        [GridAction]
        public ActionResult SelectCreateForecast(int customerId, int month, int year) {
            var model = new List<ForecastModel>();
            if (customerId == 0)
                return View(new GridModel(model));
            using (var vfi = new tammaContext()) {
                var fromDate = new DateTime(year, month, 1).ToString("dd/MM/yyyy");
                var toDate = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
                model = GetListForecast(fromDate, toDate, 0, 0, customerId);
                //var products = vfi.Products.Where(p => p.CustomerId == customerId && p.Active).OrderBy(p => p.ProductCode);
                //var productIds = products.Select(p => p.ProductId).ToList();
                //foreach (var productId in productIds)
                //{
                //    var entity = GetForecastInfo(productId, month, year);
                //    model.Add(entity);
                //}
            }
            return View(new GridModel(model));
        }

        //[GridAction]
        //public ActionResult InsertForecast(ForecastModel inserted, int month, int year, int status, int employeeId,
        //                                   string date)
        //{
        //    try
        //    {
        //        if (!Request.IsAuthenticated)
        //            ModelState.AddModelError("UpdateExpectedSales",
        //                                     @"Vui lòng đang nhập lại hệ thống. (IsAuthenticated) ");
        //        ModelState.Clear();
        //        if (inserted.Quantity <= 0)
        //            throw new AggregateException("Lỗi! Số lượng phải lớn hơn 0");
        //        using (var vfi = new tammaContext())
        //        {
        //            //var product = vfi.Products.FirstOrDefault(p => p.ProductId == inserted.ProductId);
        //            //if (product == null)
        //            //    throw new AggregateException("Không tìm thấy sản phẩm ! Liên hệ admin " +
        //            //                                 inserted.ProductCode);
        //            var productId = 0;
        //            try
        //            {
        //                productId = Convert.ToInt32(inserted.ProductCode);
        //            }
        //            catch (FormatException)
        //            {
        //                productId = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(inserted.ProductCode))
        //                               .ProductId;
        //            }

        //            var ci = new CultureInfo("vi-VN");
        //            var qDate = Convert.ToDateTime(date, ci);
        //            var forecastOrder =
        //                vfi.ForecastOrders.FirstOrDefault(
        //                    fo =>
        //                    fo.ProductId == productId &&
        //                    fo.ForecastDate == qDate);
        //            if (forecastOrder == null)
        //            {
        //                //if (!expectedNew.IsSelling) continue;
        //                forecastOrder = new ForecastOrder
        //                    {
        //                        ProductId = productId,
        //                        ForecastDate = qDate,
        //                        Quantity = inserted.Quantity,
        //                        ModifiedDate = DateTime.Now,
        //                        ModifiedUser = HttpContext.User.Identity.Name,
        //                        IsSelling = true,
        //                        Status = (byte)MyUtilities.Transaction.Status.Open,
        //                    };
        //                vfi.ForecastOrders.Add(forecastOrder);
        //            }
        //            else
        //            {
        //                if (forecastOrder.Status == (byte)MyUtilities.Transaction.Status.Approved)
        //                {
        //                    forecastOrder = new ForecastOrder
        //                        {
        //                            ProductId = productId,
        //                            ForecastDate = qDate,
        //                            Quantity = inserted.Quantity,
        //                            ModifiedDate = DateTime.Now,
        //                            ModifiedUser = HttpContext.User.Identity.Name,
        //                            IsSelling = true,
        //                            Status = (byte)MyUtilities.Transaction.Status.Open,
        //                        };
        //                    vfi.ForecastOrders.Add(forecastOrder);
        //                }
        //                else
        //                {
        //                    forecastOrder.Quantity = inserted.Quantity;
        //                    forecastOrder.IsSelling = true;
        //                    forecastOrder.Status = (byte)MyUtilities.Transaction.Status.Open;
        //                }
        //                //product.IsSelling = expectedNew.IsSelling;
        //            }
        //            vfi.SaveChanges();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("InsertExpectedSales2", ex.Message);
        //    }
        //    return View(new GridModel(GetListForecast(month, year, status, employeeId)));
        //}

        [GridAction]
        public ActionResult UpdateForecast2(
            [Bind(Prefix = "inserted")] IEnumerable<ForecastModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<ForecastModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ForecastModel> deleteds) {
            try {
                if (!Request.IsAuthenticated)
                    throw new AggregateException(@"Vui lòng đang nhập lại hệ thống. (IsAuthenticated) ");
                using (var vfi = new tammaContext()) {
                    foreach (var updated in updateds) {
                        var forecastOrder =
                            vfi.ForecastOrders.FirstOrDefault(
                                fo =>
                                    fo.ProductId == updated.ProductId &&
                                    fo.ForecastDate.Year == updated.ForecastDate.Year &&
                                    fo.ForecastDate.Month == updated.ForecastDate.Month);
                        if (forecastOrder == null) {
                            forecastOrder = new ForecastOrder {
                                ProductId = updated.ProductId,
                                ForecastDate = updated.ForecastDate,
                                Quantity = updated.Quantity,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                IsSelling = true,
                                Status = (byte)MyUtilities.Transaction.Status.Open,
                            };
                            vfi.ForecastOrders.Add(forecastOrder);
                        }
                        else {
                            forecastOrder.ForecastDate = updated.ForecastDate;
                            forecastOrder.Quantity = updated.Quantity;
                            forecastOrder.ModifiedDate = DateTime.Now;
                            forecastOrder.ModifiedUser = HttpContext.User.Identity.Name;
                            forecastOrder.Status = (byte)MyUtilities.Transaction.Status.Open;
                        }
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateForecast2", ex.Message);
            }
            return View(new GridModel(new List<ForecastModel>()));
        }

        //[GridAction]
        //public ActionResult UpdateForecast(ForecastModel updated, int month, int year, int status, int employeeId,
        //                                   string date)
        //{
        //    try
        //    {
        //        if (!Request.IsAuthenticated)
        //            throw new AggregateException(@"Vui lòng đang nhập lại hệ thống. (IsAuthenticated) ");
        //        ModelState.Clear();
        //        using (var vfi = new tammaContext())
        //        {

        //            //try
        //            //{
        //            //    productId = Convert.ToInt32(updated.ProductCode);
        //            //}
        //            //catch (FormatException)
        //            //{
        //            //    productId = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(updated.ProductCode))
        //            //                   .ProductId;
        //            //}
        //            var ci = new CultureInfo("vi-VN");
        //            var qDate = Convert.ToDateTime(date, ci);
        //            var forecastOrder =
        //                vfi.ForecastOrders.FirstOrDefault(
        //                    fo => fo.ForecastOrderId == updated.ForecastOrderId);
        //            if (forecastOrder == null)
        //            {
        //                //if (!expectedNew.IsSelling) continue;
        //                //forecastOrder = new ForecastOrder
        //                //    {
        //                //        ProductId = updated.ProductId,
        //                //        ForecastDate = updated.ForecastDate,
        //                //        Quantity = updated.Quantity,
        //                //        ModifiedDate = DateTime.Now,
        //                //        ModifiedUser = HttpContext.User.Identity.Name,
        //                //        Status = (byte) MyUtilities.Transaction.Status.Open,
        //                //        IsSelling = true
        //                //    };
        //                //vfi.ForecastOrders.Add(forecastOrder);
        //            }
        //            else
        //            {
        //                if (forecastOrder.ForecastDate != qDate)
        //                {
        //                    var productId = forecastOrder.ProductId;
        //                    forecastOrder = new ForecastOrder
        //                        {
        //                            ProductId = productId,
        //                            ForecastDate = qDate,
        //                            Quantity = updated.Quantity,
        //                            ModifiedDate = DateTime.Now,
        //                            ModifiedUser = HttpContext.User.Identity.Name,
        //                            IsSelling = true,
        //                            Status = (byte)MyUtilities.Transaction.Status.Open,
        //                        };
        //                    vfi.ForecastOrders.Add(forecastOrder);
        //                }
        //                else
        //                {
        //                    //if (forecastOrder.Status == (byte)MyUtilities.Transaction.Status.Cancel)
        //                    //{
        //                    forecastOrder.IsSelling = updated.IsSelling;
        //                    forecastOrder.Quantity = updated.Quantity;
        //                    forecastOrder.Status = (byte)MyUtilities.Transaction.Status.Open;
        //                    forecastOrder.ModifiedDate = DateTime.Now;
        //                    forecastOrder.ModifiedUser = HttpContext.User.Identity.Name;
        //                    //}
        //                }
        //            }
        //            vfi.SaveChanges();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("UpdateExpectedSales2", ex.Message);
        //    }
        //    return
        //        View(
        //            new GridModel(GetListForecast(month, year, status, employeeId).OrderBy(m => m.ForecastDate).ToList()));
        //}

        [GridAction]
        public ActionResult ApproveForecast(ForecastModel approved, int customerId) {
            try {
                if (!Request.IsAuthenticated)
                    ModelState.AddModelError("UpdateExpectedSales",
                        @"Vui lòng đang nhập lại hệ thống. (IsAuthenticated) ");
                ModelState.Clear();
                using (var vfi = new tammaContext()) {
                    var forecastOrder =
                        vfi.ForecastOrders.FirstOrDefault(
                            fo => fo.ForecastOrderId == approved.ForecastOrderId);
                    if (forecastOrder != null) {
                        approved.CustomerId = forecastOrder.Product.CustomerId;
                        forecastOrder.Status = (byte)MyUtilities.Transaction.Status.Approved;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ApproveForecast", ex.Message);
            }
            return View(new GridModel(GetListForecast(null, null, 1, 0, customerId)
                    .OrderBy(m => m.ForecastDate)));
        }

        [GridAction]
        public ActionResult DeleteForecast(ForecastModel deleted) {
            try {
                if (!Request.IsAuthenticated)
                    ModelState.AddModelError("UpdateExpectedSales",
                        @"Vui lòng đang nhập lại hệ thống. (IsAuthenticated) ");
                ModelState.Clear();
                using (var vfi = new tammaContext()) {
                    var forecastOrder =
                        vfi.ForecastOrders.FirstOrDefault(
                            fo => fo.ForecastOrderId == deleted.ForecastOrderId);
                    if (forecastOrder != null) {
                        deleted.CustomerId = forecastOrder.Product.CustomerId;
                        forecastOrder.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    }
                    vfi.SaveChanges();
                    //if (deleted.Status != (byte) MyUtilities.Transaction.Status.Open)
                    //return View(new GridModel(new List<ForecastModel>()));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteForecast", ex.Message);
            }
            return View(new GridModel(
                GetListForecast(deleted.ForecastDate.ToString("dd/MM/yyyy"), "", 1, 0, 0)
                    .OrderBy(m => m.ForecastDate)));
        }


        [GridAction]
        public ActionResult SelectForecastProcessDetail(int forecastOrderId, bool isAnalyze) {
            var model = new List<ForecastDetailModel>();

            try {
                model = GetForecastProcessDetail(forecastOrderId, isAnalyze);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectForecastProcessDetail", "" + ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<ForecastDetailModel> GetForecastProcessDetail(int forecastOrderId, bool isAnalyze) {
            var model = new List<ForecastDetailModel>();
            using (var vfi = new tammaContext()) {
                //var orderDetail = vfi.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailId);
                var forecastOrder = vfi.ForecastOrders.FirstOrDefault(od => od.ForecastOrderId == forecastOrderId);
                // don hang da tre
                if (forecastOrder.ForecastDate < DateTime.Now)
                    return model;
                var required = forecastOrder.Quantity;
                //required = forecastOrder.Quantity;
                var endDate = forecastOrder.ForecastDate;
                var processes =
                    vfi.ProductionProcesses.Where(pp => pp.IsNecessary && pp.ProductId == forecastOrder.ProductId);
                var warehouseInvs =
                    vfi.ProductInventories.Where(
                        pi => pi.ProductId == forecastOrder.ProductId);
                var olderOrderDetails =
                    vfi.OrderDetails.Where(od => od.ProductId == forecastOrder.ProductId && od.RequiedNumber > 0 &&
                                                 od.ProductId == forecastOrder.ProductId &&
                                                 od.Order.DueDate <= forecastOrder.ForecastDate &&
                                                 (od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess ||
                                                  od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting));
                var orderQuantity = 0;
                if (olderOrderDetails.Any())
                    orderQuantity = olderOrderDetails.Sum(od => od.RequiedNumber);
                //tp
                var process = processes.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.Finish);
                var warehouseInv = warehouseInvs.FirstOrDefault(pi => pi.WarehouseId == MyUtilities.Warehouse.Finish);
                if (process != null) {
                    var detail = new ForecastDetailModel {
                        Index = process.ProcessIndex,
                        //OrderDetailId = orderDetailId,
                        ForecastOrderId = forecastOrderId,
                        WarehouseId = process.WarehouseId,
                        WarehouseName = process.Warehouse.ShortName,
                        ForecastQuantity = required + orderQuantity,
                        //StartDate = endDate.AddDays(-1),
                        EndDate = endDate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "Auto",
                        IsWorking = false,
                        ProcessId = process.ProcessId,
                        OrderQuantity = orderQuantity,
                    };
                    endDate = endDate.AddDays(-1);
                    detail.StartDate = endDate;
                    if (detail.StartDate < DateTime.Now) {
                        detail.StartDate = DateTime.Now;
                        detail.EndDate = DateTime.Now.AddDays(1);
                    }
                    var foreacastDetail =
                        vfi.ForecastOrderDetails.FirstOrDefault(
                            fod =>
                            fod.WarehouseId == detail.WarehouseId && fod.ForecastOrderId == detail.ForecastOrderId);
                    if (foreacastDetail != null) {
                        if (!isAnalyze) {
                            detail.DetailId = foreacastDetail.DetailId;
                            detail.StartDate = foreacastDetail.StartDate;
                            detail.EndDate = foreacastDetail.EndDate;
                            detail.ForecastQuantity = foreacastDetail.ForecastQuantity;
                            detail.ModifiedDate = foreacastDetail.ModifiedDate;
                            detail.ModifiedUser = foreacastDetail.ModifiedUser;
                            detail.IsWorking = foreacastDetail.IsWorking;
                        }
                        else {
                            detail.ProcessQuantity = foreacastDetail.ForecastQuantity;
                        }
                    }
                    if (warehouseInv != null) {
                        detail.WarehouseInv = warehouseInv.TotalQty;
                        required -= detail.WarehouseInv;
                    }
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                if (required == 0) goto back;

                process =
                    processes.FirstOrDefault(
                        pp => pp.WarehouseId == MyUtilities.Warehouse.QcA || pp.WarehouseId == MyUtilities.Warehouse.QcB);
                var warehouseInvss =
                    warehouseInvs.Where(
                        pi => pi.WarehouseId == MyUtilities.Warehouse.QcA || pi.WarehouseId == MyUtilities.Warehouse.QcB);
                if (process != null) {
                    var detail = new ForecastDetailModel {
                        Index = process.ProcessIndex,
                        //OrderDetailId = orderDetailId,
                        ForecastOrderId = forecastOrderId,
                        WarehouseId = process.WarehouseId,
                        WarehouseName = process.Warehouse.ShortName,
                        ForecastQuantity = required + orderQuantity,
                        //StartDate = endDate.AddDays(-1),
                        EndDate = endDate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "Auto",
                        IsWorking = false,
                        ProcessId = process.ProcessId,
                        OrderQuantity = orderQuantity,
                    };
                    endDate = endDate.AddDays(-1);
                    detail.StartDate = endDate;
                    if (detail.StartDate < DateTime.Now) {
                        detail.StartDate = DateTime.Now;
                        detail.EndDate = DateTime.Now.AddDays(1);
                    }
                    var foreacastDetail =
                        vfi.ForecastOrderDetails.FirstOrDefault(
                            fod =>
                            fod.WarehouseId == detail.WarehouseId && fod.ForecastOrderId == detail.ForecastOrderId);
                    if (foreacastDetail != null) {
                        if (!isAnalyze) {
                            detail.DetailId = foreacastDetail.DetailId;
                            detail.StartDate = foreacastDetail.StartDate;
                            detail.EndDate = foreacastDetail.EndDate;
                            detail.ForecastQuantity = foreacastDetail.ForecastQuantity;
                            detail.ModifiedDate = foreacastDetail.ModifiedDate;
                            detail.ModifiedUser = foreacastDetail.ModifiedUser;
                            detail.IsWorking = foreacastDetail.IsWorking;
                        }
                        else {
                            detail.ProcessQuantity = foreacastDetail.ForecastQuantity;
                        }
                    }
                    if (warehouseInvss.Any()) {
                        detail.WarehouseInv = warehouseInvss.Sum(pi => pi.TotalQty);
                        required -= detail.WarehouseInv;
                    }
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                if (required == 0) goto back;

                process = processes.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.WaitingPlating);
                if (process != null) {
                    var detail = new ForecastDetailModel {
                        Index = process.ProcessIndex,
                        //OrderDetailId = orderDetailId,
                        ForecastOrderId = forecastOrderId,
                        WarehouseId = process.WarehouseId,
                        WarehouseName = process.Warehouse.ShortName,
                        ForecastQuantity = required + orderQuantity,
                        //StartDate = endDate.AddDays(-1),
                        EndDate = endDate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "Auto",
                        IsWorking = false,
                        ProcessId = process.ProcessId,
                        OrderQuantity = orderQuantity,
                    };
                    var productionPlatings = vfi.ProductionPlatings.Where(pp => pp.ProductId == forecastOrder.ProductId);
                    var day = 7;
                    if (productionPlatings.Any()) {
                        day = productionPlatings.Sum(pp => pp.PlatingDay);
                    }
                    endDate = endDate.AddDays(day * -1);
                    detail.StartDate = endDate;
                    if (detail.StartDate < DateTime.Now) {
                        detail.StartDate = DateTime.Now;
                        detail.EndDate = DateTime.Now.AddDays(7);
                    }
                    var foreacastDetail =
                        vfi.ForecastOrderDetails.FirstOrDefault(
                            fod =>
                            fod.WarehouseId == detail.WarehouseId && fod.ForecastOrderId == detail.ForecastOrderId);
                    if (foreacastDetail != null) {
                        if (!isAnalyze) {
                            detail.DetailId = foreacastDetail.DetailId;
                            detail.StartDate = foreacastDetail.StartDate;
                            detail.EndDate = foreacastDetail.EndDate;
                            detail.ForecastQuantity = foreacastDetail.ForecastQuantity;
                            detail.ModifiedDate = foreacastDetail.ModifiedDate;
                            detail.ModifiedUser = foreacastDetail.ModifiedUser;
                            detail.IsWorking = foreacastDetail.IsWorking;
                        }
                        else {
                            detail.ProcessQuantity = foreacastDetail.ForecastQuantity;
                        }
                    }
                    warehouseInvss =
                        warehouseInvs.Where(
                            pi =>
                            pi.WarehouseId == MyUtilities.Warehouse.WaitingPlating ||
                            pi.WarehouseId == MyUtilities.Warehouse.Plating ||
                            pi.WarehouseId == MyUtilities.Warehouse.PlatingTest);
                    if (warehouseInvss.Any()) {
                        detail.WarehouseInv = warehouseInvss.Sum(pi => pi.TotalQty);
                        required -= detail.WarehouseInv;
                    }
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                if (required == 0) goto back;

                process = processes.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment);
                if (process != null) {
                    var detail = new ForecastDetailModel {
                        Index = process.ProcessIndex,
                        //OrderDetailId = orderDetailId,
                        ForecastOrderId = forecastOrderId,
                        WarehouseId = process.WarehouseId,
                        WarehouseName = process.Warehouse.ShortName,
                        ForecastQuantity = required + orderQuantity,
                        //StartDate = endDate.AddDays(-1),
                        EndDate = endDate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "Auto",
                        IsWorking = false,
                        ProcessId = process.ProcessId,
                        OrderQuantity = orderQuantity,
                    };
                    endDate = endDate.AddDays(-1);
                    detail.StartDate = endDate;
                    if (detail.StartDate < DateTime.Now) {
                        detail.StartDate = DateTime.Now;
                        detail.EndDate = DateTime.Now.AddDays(1);
                    }
                    var foreacastDetail =
                        vfi.ForecastOrderDetails.FirstOrDefault(
                            fod =>
                            fod.WarehouseId == detail.WarehouseId && fod.ForecastOrderId == detail.ForecastOrderId);
                    if (foreacastDetail != null) {
                        if (!isAnalyze) {
                            detail.DetailId = foreacastDetail.DetailId;
                            detail.StartDate = foreacastDetail.StartDate;
                            detail.EndDate = foreacastDetail.EndDate;
                            detail.ForecastQuantity = foreacastDetail.ForecastQuantity;
                            detail.ModifiedDate = foreacastDetail.ModifiedDate;
                            detail.ModifiedUser = foreacastDetail.ModifiedUser;
                            detail.IsWorking = foreacastDetail.IsWorking;
                        }
                        else {
                            detail.ProcessQuantity = foreacastDetail.ForecastQuantity;
                        }
                    }
                    warehouseInv =
                        warehouseInvs.FirstOrDefault(pi => pi.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment);
                    if (warehouseInv != null) {
                        detail.WarehouseInv = warehouseInv.TotalQty;
                        required -= detail.WarehouseInv;
                    }
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                if (required == 0) goto back;

                process = processes.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.HeatTreatment);
                if (process != null) {
                    var detail = new ForecastDetailModel {
                        Index = process.ProcessIndex,
                        //OrderDetailId = orderDetailId,
                        ForecastOrderId = forecastOrderId,
                        WarehouseId = process.WarehouseId,
                        WarehouseName = process.Warehouse.ShortName,
                        ForecastQuantity = required + orderQuantity,
                        //StartDate = endDate.AddDays(-1),
                        EndDate = endDate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "Auto",
                        IsWorking = false,
                        ProcessId = process.ProcessId,
                        OrderQuantity = orderQuantity,
                    };
                    endDate = endDate.AddDays(-1);
                    detail.StartDate = endDate;
                    if (detail.StartDate < DateTime.Now) {
                        detail.StartDate = DateTime.Now;
                        detail.EndDate = DateTime.Now.AddDays(1);
                    }
                    var foreacastDetail =
                        vfi.ForecastOrderDetails.FirstOrDefault(
                            fod =>
                            fod.WarehouseId == detail.WarehouseId && fod.ForecastOrderId == detail.ForecastOrderId);
                    if (foreacastDetail != null) {
                        if (!isAnalyze) {
                            detail.DetailId = foreacastDetail.DetailId;
                            detail.StartDate = foreacastDetail.StartDate;
                            detail.EndDate = foreacastDetail.EndDate;
                            detail.ForecastQuantity = foreacastDetail.ForecastQuantity;
                            detail.ModifiedDate = foreacastDetail.ModifiedDate;
                            detail.ModifiedUser = foreacastDetail.ModifiedUser;
                            detail.IsWorking = foreacastDetail.IsWorking;
                        }
                        else {
                            detail.ProcessQuantity = foreacastDetail.ForecastQuantity;
                        }
                    }
                    warehouseInv =
                        warehouseInvs.FirstOrDefault(pi => pi.WarehouseId == MyUtilities.Warehouse.HeatTreatment);
                    if (warehouseInv != null) {
                        detail.WarehouseInv = warehouseInv.TotalQty;
                        required -= detail.WarehouseInv;
                    }
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                if (required == 0) goto back;
                process = processes.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.Production2);
                if (process != null) {
                    var detail = new ForecastDetailModel {
                        Index = process.ProcessIndex,
                        //OrderDetailId = orderDetailId,
                        ForecastOrderId = forecastOrderId,
                        WarehouseId = process.WarehouseId,
                        WarehouseName = process.Warehouse.ShortName,
                        ForecastQuantity = required + orderQuantity,
                        //StartDate = endDate.AddDays(-1),
                        EndDate = endDate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "Auto",
                        IsWorking = false,
                        ProcessId = process.ProcessId,
                        OrderQuantity = orderQuantity,
                    };
                    var productivity = 0.0;
                    if (forecastOrder.Product.ProductionSections.Any(ps => ps.Active && ps.Productivity > 0)) {
                        productivity =
                            forecastOrder.Product.ProductionSections.Where(ps => ps.Active && ps.Productivity > 0)
                                         .Max(ps => ps.Productivity);
                    }
                    var day = 0;
                    if (productivity > 0)
                        day = MyUtilities.Function.RoundUp(required / (72000 / productivity));
                    endDate = endDate.AddDays(day * -1);
                    detail.StartDate = endDate;
                    if (detail.StartDate < DateTime.Now) {
                        detail.StartDate = DateTime.Now;
                        detail.EndDate = DateTime.Now.AddDays(day);
                    }
                    var foreacastDetail =
                        vfi.ForecastOrderDetails.FirstOrDefault(
                            fod =>
                            fod.WarehouseId == detail.WarehouseId && fod.ForecastOrderId == detail.ForecastOrderId);
                    if (foreacastDetail != null) {
                        if (!isAnalyze) {
                            detail.DetailId = foreacastDetail.DetailId;
                            detail.StartDate = foreacastDetail.StartDate;
                            detail.EndDate = foreacastDetail.EndDate;
                            detail.ForecastQuantity = foreacastDetail.ForecastQuantity;
                            detail.ModifiedDate = foreacastDetail.ModifiedDate;
                            detail.ModifiedUser = foreacastDetail.ModifiedUser;
                            detail.IsWorking = foreacastDetail.IsWorking;
                        }
                        else {
                            detail.ProcessQuantity = foreacastDetail.ForecastQuantity;
                        }
                    }
                    warehouseInvss =
                        warehouseInvs.Where(
                            pi =>
                            pi.WarehouseId == MyUtilities.Warehouse.Processing2 ||
                            pi.WarehouseId == MyUtilities.Warehouse.Production2B ||
                            pi.WarehouseId == MyUtilities.Warehouse.Production2C ||
                            pi.WarehouseId == MyUtilities.Warehouse.Production2D);
                    if (warehouseInvss.Any()) {
                        detail.WarehouseInv = warehouseInvss.Sum(pi => pi.TotalQty);
                        required -= detail.WarehouseInv;
                    }
                    if (warehouseInv != null) {
                        detail.WarehouseInv = warehouseInv.TotalQty;
                        required -= detail.WarehouseInv;
                    }
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                if (required == 0) goto back;
                process = processes.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.Cnc);
                if (process != null) {
                    var detail = new ForecastDetailModel {
                        Index = process.ProcessIndex,
                        //OrderDetailId = orderDetailId,
                        ForecastOrderId = forecastOrderId,
                        WarehouseId = process.WarehouseId,
                        WarehouseName = process.Warehouse.ShortName,
                        ForecastQuantity = required + orderQuantity,
                        //StartDate = endDate.AddDays(-1),
                        EndDate = endDate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "Auto",
                        IsWorking = false,
                        ProcessId = process.ProcessId,
                        OrderQuantity = orderQuantity,
                    };
                    endDate = endDate.AddDays(-1);
                    detail.StartDate = endDate;
                    if (detail.StartDate < DateTime.Now) {
                        detail.StartDate = DateTime.Now;
                        detail.EndDate = DateTime.Now.AddDays(1);
                    }
                    var foreacastDetail =
                        vfi.ForecastOrderDetails.FirstOrDefault(
                            fod =>
                            fod.WarehouseId == detail.WarehouseId && fod.ForecastOrderId == detail.ForecastOrderId);
                    if (foreacastDetail != null) {
                        if (!isAnalyze) {
                            detail.DetailId = foreacastDetail.DetailId;
                            detail.StartDate = foreacastDetail.StartDate;
                            detail.EndDate = foreacastDetail.EndDate;
                            detail.ForecastQuantity = foreacastDetail.ForecastQuantity;
                            detail.ModifiedDate = foreacastDetail.ModifiedDate;
                            detail.ModifiedUser = foreacastDetail.ModifiedUser;
                            detail.IsWorking = foreacastDetail.IsWorking;
                        }
                        else {
                            detail.ProcessQuantity = foreacastDetail.ForecastQuantity;
                        }
                    }
                    warehouseInv = warehouseInvs.FirstOrDefault(pi => pi.WarehouseId == MyUtilities.Warehouse.Cnc);
                    if (warehouseInv != null) {
                        detail.WarehouseInv = warehouseInv.TotalQty;
                        required -= detail.WarehouseInv;
                    }
                    if (required < 0)
                        required = 0;
                    model.Add(detail);
                }
                if (required == 0) goto back;
                process = processes.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.Production1);
                if (process != null) {
                    var detail = new ForecastDetailModel {
                        Index = process.ProcessIndex,
                        //OrderDetailId = orderDetailId,
                        ForecastOrderId = forecastOrderId,
                        WarehouseId = process.WarehouseId,
                        WarehouseName = process.Warehouse.ShortName,
                        ForecastQuantity = required + orderQuantity,
                        //StartDate = endDate.AddDays(-1),
                        EndDate = endDate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "Auto",
                        IsWorking = false,
                        ProcessId = process.ProcessId,
                        OrderQuantity = orderQuantity,
                    };
                    var day = 1.0;
                    if (forecastOrder.Product.Productivity != 0)
                        day = MyUtilities.Function.RoundUp(required / (72000 / forecastOrder.Product.Productivity.Value), 0);
                    endDate = endDate.AddDays(day * -1);
                    detail.StartDate = endDate;
                    if (detail.StartDate < DateTime.Now) {
                        detail.StartDate = DateTime.Now;
                        detail.EndDate = DateTime.Now.AddDays(day);
                    }
                    var foreacastDetail =
                        vfi.ForecastOrderDetails.FirstOrDefault(
                            fod =>
                            fod.WarehouseId == detail.WarehouseId && fod.ForecastOrderId == detail.ForecastOrderId);
                    if (foreacastDetail != null) {
                        if (!isAnalyze) {
                            detail.DetailId = foreacastDetail.DetailId;
                            detail.StartDate = foreacastDetail.StartDate;
                            detail.EndDate = foreacastDetail.EndDate;
                            detail.ForecastQuantity = foreacastDetail.ForecastQuantity;
                            detail.ModifiedDate = foreacastDetail.ModifiedDate;
                            detail.ModifiedUser = foreacastDetail.ModifiedUser;
                            detail.IsWorking = foreacastDetail.IsWorking;
                        }
                        else {
                            detail.ProcessQuantity = foreacastDetail.ForecastQuantity;
                        }
                    }
                    model.Add(detail);
                }
                //foreach (var process in processes)
                //{
                //}
            }
        back:
            model = model.OrderBy(m => m.Index).ToList();
            if (!isAnalyze && model.Any()) {
                var endDate = model.FirstOrDefault().StartDate;
                foreach (var forecastDetailModel in model) {
                    if (forecastDetailModel.DetailId != 0) {
                        endDate = forecastDetailModel.EndDate;
                    }
                    else {
                        var day = (forecastDetailModel.EndDate - forecastDetailModel.StartDate).Value.TotalDays;
                        forecastDetailModel.StartDate = endDate;
                        forecastDetailModel.EndDate = endDate.Value.AddDays(day);
                        endDate = forecastDetailModel.EndDate;
                    }
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectForecastDetailInfo(int detailId) {
            var model = new List<ForecastDetailInfo>();

            try {
                model = GetForecastDetailInfo(detailId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectForecastDetailInfo", "" + ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<ForecastDetailInfo> GetForecastDetailInfo(int detailId) {
            var model = new List<ForecastDetailInfo>();
            //return model;
            if (detailId == 0)
                return model;
            using (var vfi = new tammaContext()) {
                var forecastDetail = vfi.ForecastOrderDetails.FirstOrDefault(fod => fod.DetailId == detailId);
                if (forecastDetail.WarehouseId != MyUtilities.Warehouse.Production2) {
                    var startDate = forecastDetail.StartDate;
                    while (startDate <= forecastDetail.EndDate) {
                        var entity = new ForecastDetailInfo {
                            Date = startDate,
                            Note = "Đã duyệt",
                            ProductCode = "",
                            Count = 0,
                            WarehouseId = forecastDetail.WarehouseId,
                            DateString = startDate.ToString("dd/MM")
                        };
                        var approvedDetails =
                            vfi.ForecastOrderDetails.Where(
                                fod =>
                                fod.ForecastOrder.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                fod.WarehouseId == forecastDetail.WarehouseId &&
                                startDate >= fod.StartDate && startDate <= fod.EndDate &&
                                !fod.IsWorking && fod.DetailId != detailId).ToList();
                        entity.List.AddRange(GetForecastDetailInfoList(approvedDetails, entity.WarehouseId));
                        if (entity.List.Any()) {
                            entity.Count = entity.List.Count;
                            entity.Description = entity.Production1Note;
                            model.Add(entity);
                        }

                        var entity2 = new ForecastDetailInfo {
                            Date = startDate,
                            Note = "Đợi duyệt",
                            ProductCode = "",
                            Count = 0,
                            WarehouseId = forecastDetail.WarehouseId,
                            DateString = startDate.ToString("dd/MM")
                        };
                        approvedDetails =
                            vfi.ForecastOrderDetails.Where(
                                fod =>
                                fod.ForecastOrder.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                fod.WarehouseId == forecastDetail.WarehouseId &&
                                startDate >= fod.StartDate && startDate <= fod.EndDate &&
                                !fod.IsWorking && fod.DetailId != detailId).ToList();
                        entity2.List.AddRange(GetForecastDetailInfoList(approvedDetails, entity2.WarehouseId));
                        if (entity2.List.Any()) {
                            entity.Count = entity.List.Count;
                            entity.Description = entity.Production1Note;
                            model.Add(entity2);
                        }

                        startDate = startDate.AddDays(1);
                    }
                }
                else {
                    var sections =
                        vfi.ProductionSections.Where(
                            ps => ps.Active && ps.ProductId == forecastDetail.ForecastOrder.ProductId).ToList();
                    var time = sections.Sum(ps => ps.Productivity * forecastDetail.ForecastQuantity);
                    var day = (forecastDetail.EndDate - forecastDetail.StartDate).TotalDays + 1;
                    var startDateMonth = new DateTime(forecastDetail.StartDate.Year, forecastDetail.StartDate.Month, 1);
                    var endDateMonth = new DateTime(forecastDetail.EndDate.Year, forecastDetail.EndDate.Month, 1)
                        .AddMonths(1).AddDays(-1);
                    for (DateTime date = startDateMonth; date < endDateMonth; date = date.AddMonths(1)) {
                        var eDate = date.AddMonths(1).AddDays(-1);
                        var time2 = time;
                        if (forecastDetail.StartDate.Month != forecastDetail.EndDate.Month) {
                            //dự báo trước
                            if (forecastDetail.StartDate > date) {
                                var checkDay = (eDate - forecastDetail.StartDate).TotalDays + 1;
                                time2 = checkDay / day * time;
                            }
                            // dự báo sau
                            else if (forecastDetail.EndDate < eDate) {
                                var checkDay = (forecastDetail.EndDate - date).TotalDays + 1;
                                time2 = checkDay / day * time;
                            }
                            else {
                                var checkDay = (eDate - date).TotalDays + 1;
                                time2 = checkDay / day * time;
                            }
                        }
                        var approvedDetails =
                            vfi.ForecastOrderDetails.Where(
                                fod =>
                                fod.ForecastOrder.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                fod.WarehouseId == forecastDetail.WarehouseId &&
                                ((fod.StartDate >= date && fod.StartDate <= eDate) ||
                                 (fod.EndDate >= date && fod.EndDate <= eDate)) &&
                                !fod.IsWorking && fod.DetailId != detailId)
                               .OrderBy(fod => fod.ForecastOrder.Product.ProductCode)
                               .ToList();

                        var entity = new ForecastDetailInfo {
                            Date = date,
                            Note = "Đã duyệt",
                            ProductCode = "",
                            Count = 0,
                            WarehouseId = forecastDetail.WarehouseId,
                            DateString = startDateMonth.ToString("MM/yyyy"),
                            Sum = Math.Round(time2 * 100 / MyUtilities.Section.Total, 2),
                        };
                        entity.List.AddRange(GetForecastDetailInfoList(approvedDetails, date, eDate));
                        if (entity.List.Any()) {
                            entity.Count =
                                Math.Round(entity.List.Sum(l => l.ForecastQuantity) * 100 / MyUtilities.Section.Total, 2);
                            entity.Description = entity.Production2Note;
                            model.Add(entity);
                        }
                        approvedDetails =
                            vfi.ForecastOrderDetails.Where(
                                fod =>
                                fod.ForecastOrder.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                fod.WarehouseId == forecastDetail.WarehouseId &&
                                ((fod.StartDate >= date && fod.StartDate <= eDate) ||
                                 (fod.EndDate >= date && fod.EndDate <= eDate)) &&
                                !fod.IsWorking && fod.DetailId != detailId)
                               .OrderBy(fod => fod.ForecastOrder.Product.ProductCode)
                               .ToList();

                        var entity2 = new ForecastDetailInfo {
                            Date = date,
                            Note = "Đợi duyệt",
                            ProductCode = "",
                            Count = 0,
                            WarehouseId = forecastDetail.WarehouseId,
                            DateString = startDateMonth.ToString("MM/yyyy"),
                            Sum = Math.Round(time2 * 100 / MyUtilities.Section.Total, 2),
                        };
                        entity2.List.AddRange(GetForecastDetailInfoList(approvedDetails, date, eDate));
                        if (entity2.List.Any()) {
                            entity2.Count =
                                Math.Round(entity2.List.Sum(l => l.ForecastQuantity) * 100 / MyUtilities.Section.Total, 2);
                            entity2.Description = entity2.Production2Note;
                            model.Add(entity2);
                        }
                    }
                }

            }
            return model;
        }

        private List<ForecastDetailInfoList> GetForecastDetailInfoList(List<ForecastOrderDetail> list, int warehouseId) {
            var model = new List<ForecastDetailInfoList>();
            using (var vfi = new tammaContext()) {
                foreach (var detail in list) {
                    var entity = new ForecastDetailInfoList {
                        ProductId = detail.ForecastOrder.ProductId,
                        ProductCode = detail.ForecastOrder.Product.ProductCode,
                        ForecastQuantity = detail.ForecastQuantity,
                    };
                    model.Add(entity);
                }
            }
            return model;
        }

        private List<ForecastDetailInfoList> GetForecastDetailInfoList(List<ForecastOrderDetail> list,
                                                                       DateTime startDateMonth, DateTime endDateMonth) {
            var model = new List<ForecastDetailInfoList>();
            using (var vfi = new tammaContext()) {
                foreach (var approvedDetail in list) {
                    var sections =
                        vfi.ProductionSections.Where(
                            ps => ps.Active && ps.ProductId == approvedDetail.ForecastOrder.ProductId)
                           .ToList();
                    if (!sections.Any()) continue;
                    var approvedTime = sections.Sum(ps => ps.Productivity * approvedDetail.ForecastQuantity);
                    var approvedDay = (approvedDetail.EndDate - approvedDetail.StartDate).TotalDays + 1;
                    var detail = new ForecastDetailInfoList {
                        ProductId = approvedDetail.ForecastOrder.ProductId,
                        ProductCode = approvedDetail.ForecastOrder.Product.ProductCode,
                        ForecastQuantity = approvedTime
                    };
                    model.Add(detail);
                    if (approvedDetail.StartDate.Month != approvedDetail.EndDate.Month) {
                        //dự báo trước
                        if (approvedDetail.StartDate > startDateMonth) {
                            var checkDay = (endDateMonth - approvedDetail.StartDate).TotalDays + 1;
                            detail.ForecastQuantity = checkDay / approvedDay * approvedTime;
                        }
                        // dự báo sau
                        else if (approvedDetail.EndDate < endDateMonth) {
                            var checkDay = (approvedDetail.EndDate - startDateMonth).TotalDays + 1;
                            detail.ForecastQuantity = checkDay / approvedDay * approvedTime;
                        }
                        else {
                            var checkDay = (endDateMonth - startDateMonth).TotalDays + 1;
                            detail.ForecastQuantity = checkDay / approvedDay * approvedTime;

                        }
                    }
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectProductionHistory(int productId) {
            var model = new List<ForecastDetailInfo>();

            try {
                using (var vfi = new tammaContext()) {
                    var historyDate = DateTime.Now.AddDays(-15);
                    var transactionDetails =
                        vfi.TransactionDetails.Where(
                            td => td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                  td.ReferenceId == productId &&
                                  td.Transaction.WarehouseIssueId == null &&
                                  td.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Production1 &&
                                  td.Transaction.CreatedDate > historyDate);
                    foreach (var transactionDetail in transactionDetails) {
                        var entity = new ForecastDetailInfo {
                            Date = transactionDetail.Transaction.CreatedDate,
                            DateString = transactionDetail.Transaction.CreatedDate.ToString("dd/MM"),
                            Count = transactionDetail.Quantity
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectForecastDetailInfo", "" + ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectOrderDetailBeforeForecast(int forecastOrderId) {
            var model = new List<OrderDetailModel>();

            try {
                using (var vfi = new tammaContext()) {
                    var forecast = vfi.ForecastOrders.FirstOrDefault(fo => fo.ForecastOrderId == forecastOrderId);
                    var orderDetails = from od in vfi.OrderDetails
                                       where od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                             od.Order.Status != (byte)MyUtilities.Sales.Status.Completed &&
                                             od.Order.DueDate != null &&
                                             od.Order.DueDate.Value.Month == forecast.ForecastDate.Month &&
                                             od.Order.DueDate.Value.Year == forecast.ForecastDate.Year &&
                                             od.ProductId == forecast.ProductId &&
                                             od.RequiedNumber > 0
                                       select od;
                    foreach (var orderDetail in orderDetails) {
                        var entity = new OrderDetailModel {
                            OrderDetailId = orderDetail.OrderDetailId,
                            VFIDueDate = orderDetail.Order.DueDate,
                            OrderQty = orderDetail.RequiedNumber,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectOrderDetail", "" + ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectLastForecast(int forecastOrderId) {
            var model = new List<OrderDetailModel>();

            try {
                using (var vfi = new tammaContext()) {
                    var forecast = vfi.ForecastOrders.FirstOrDefault(fo => fo.ForecastOrderId == forecastOrderId);
                    var lastDate = forecast.ForecastDate.AddMonths(-1);
                    var lastForecasts = from f in vfi.ForecastOrders
                                        where f.ProductId == forecast.ProductId &&
                                              f.ForecastDate.Month == lastDate.Month &&
                                              f.ForecastDate.Year == lastDate.Year &&
                                              f.Status == (byte)MyUtilities.Transaction.Status.Approved
                                        select f;
                    foreach (var lastForecast in lastForecasts) {
                        var entity = new OrderDetailModel {
                            OrderDetailId = lastForecast.ForecastOrderId,
                            VFIDueDate = lastForecast.ForecastDate,
                            OrderQty = lastForecast.Quantity,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectLastForecast", "" + ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateForecastProcessDetails(
            [Bind(Prefix = "inserted")] IEnumerable<ForecastDetailModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<ForecastDetailModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<ForecastDetailModel> deleteds,
            int forecastOrderId) {
            try {
                using (var vfi = new tammaContext()) {
                    //var ci = new CultureInfo("vi-VN");
                    foreach (var updated in updateds) {
                        //var sDate = Convert.ToDateTime(startDate, ci);
                        //var eDate = Convert.ToDateTime(endDate, ci);
                        if (updated.EndDate < updated.StartDate)
                            throw new AggregateException("Lỗi! Ngày hoàn thành phải sau ngày bắt đầu.");
                        //var forecastDetail = new ForecastOrderDetail();
                        //if (updated.DetailId != 0)
                        var forecastDetail =
                            vfi.ForecastOrderDetails.FirstOrDefault(fod => fod.DetailId == updated.DetailId);
                        //else
                        //    forecastDetail =
                        //        vfi.ForecastOrderDetails.FirstOrDefault(
                        //            fod =>
                        //            fod.ForecastOrderId == forecastOrderId && fod.WarehouseId == warehouseId);
                        if (forecastDetail == null) {
                            forecastDetail = new ForecastOrderDetail {
                                ForecastOrderId = forecastOrderId,
                                ForecastQuantity = updated.ForecastQuantity,
                                StartDate = updated.StartDate.Value,
                                EndDate = updated.EndDate.Value,
                                WarehouseId = updated.WarehouseId,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                IsWorking = updated.IsWorking,
                            };
                            vfi.ForecastOrderDetails.Add(forecastDetail);
                        }
                        else {
                            forecastDetail.ForecastQuantity = updated.ForecastQuantity;
                            forecastDetail.StartDate = updated.StartDate.Value;
                            forecastDetail.EndDate = updated.EndDate.Value;
                            forecastDetail.ModifiedDate = DateTime.Now;
                            forecastDetail.ModifiedUser = HttpContext.User.Identity.Name;
                            forecastDetail.IsWorking = updated.IsWorking;
                        }
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateForecastProcessDetails", "" + ex.Message);
            }
            return View(new GridModel(GetForecastProcessDetail(forecastOrderId, false)));
        }

        [GridAction]
        public ActionResult UpdateForecastProcessDetail(ForecastDetailModel updated, int forecastOrderId,
                                                        int warehouseId, string startDate, string endDate) {
            try {
                using (var vfi = new tammaContext()) {
                    var ci = new CultureInfo("vi-VN");
                    var sDate = Convert.ToDateTime(startDate, ci);
                    var eDate = Convert.ToDateTime(endDate, ci);
                    if (eDate < sDate)
                        throw new AggregateException("Lỗi! Ngày hoàn thành phải sau ngày bắt đầu.");
                    //var forecastDetail = new ForecastOrderDetail();
                    //if (updated.DetailId != 0)
                    var forecastDetail = vfi.ForecastOrderDetails.FirstOrDefault(fod => fod.DetailId == updated.DetailId);
                    //else
                    //    forecastDetail =
                    //        vfi.ForecastOrderDetails.FirstOrDefault(
                    //            fod =>
                    //            fod.ForecastOrderId == forecastOrderId && fod.WarehouseId == warehouseId);
                    if (forecastDetail == null) {
                        forecastDetail = new ForecastOrderDetail {
                            ForecastOrderId = forecastOrderId,
                            ForecastQuantity = updated.ForecastQuantity,
                            StartDate = sDate,
                            EndDate = eDate,
                            WarehouseId = warehouseId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                        };
                        vfi.ForecastOrderDetails.Add(forecastDetail);
                    }
                    else {
                        forecastDetail.ForecastQuantity = updated.ForecastQuantity;
                        forecastDetail.StartDate = sDate;
                        forecastDetail.EndDate = eDate;
                        forecastDetail.ModifiedDate = DateTime.Now;
                        forecastDetail.ModifiedUser = HttpContext.User.Identity.Name;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateForecastProcessDetail", "" + ex.Message);
            }
            return View(new GridModel(GetForecastProcessDetail(updated.ForecastOrderId, false)));
        }

        [HttpPost]
        public ActionResult PrintForecastPlan(string fromDate, string toDate, int warehouseId) {
            var model = new List<ForecastModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                DateTime toDay = DateTime.Now;

                var fDate = string.IsNullOrWhiteSpace(fromDate)
                    ? new DateTime(toDay.Year, toDay.Month, 1)
                    : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                    ? toDay
                    : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    var forecastDetails = (from fod in vfi.ForecastOrderDetails
                                           where fod.ForecastOrder.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                 fod.ForecastOrder.ForecastDate >= fDate &&
                                                 fod.ForecastOrder.ForecastDate <= tDate &&
                                                 (warehouseId == 0 || fod.WarehouseId == warehouseId)
                                           orderby fod.ForecastOrder.ForecastDate, fod.ForecastOrderId, fod.Warehouse.Idx
                                           select fod).ToList();
                    int i = 0;
                    foreach (var forecastDetail in forecastDetails) {
                        var entity = model.FirstOrDefault(m => m.ForecastOrderId == forecastDetail.ForecastOrderId);
                        if (entity == null) {
                            var forecast = forecastDetail.ForecastOrder;
                            entity = new ForecastModel {
                                Index = ++i,
                                ProductId = forecast.ProductId,
                                ProductCode = forecast.Product.ProductCode,
                                ForecastDate = forecast.ForecastDate,
                                CustomerId = forecast.Product.CustomerId,
                                CustomerCode = forecast.Product.Customer.CustomerCode,
                                ModifiedDate = forecast.ModifiedDate,
                                ModifiedUser = forecast.ModifiedUser,
                                Quantity = forecast.Quantity,
                                Status = forecast.Status,
                                IsSelling = forecast.IsSelling,
                                ForecastOrderId = forecast.ForecastOrderId,
                                EmployeeId = forecast.Product.Customer.EmployeeId,
                                EmployeeName = forecast.Product.Customer.Employee.EmployeeName,
                                MaterialEnoughDate = forecast.ForecastDate,
                                Details = new List<ForecastDetailModel>()
                            };
                            model.Add(entity);
                        }
                        var detail = new ForecastDetailModel {
                            StartDate = forecastDetail.StartDate,
                            EndDate = forecastDetail.EndDate,
                            ForecastQuantity = forecastDetail.ForecastQuantity,
                            WarehouseId = forecastDetail.WarehouseId,
                            WarehouseName = forecastDetail.Warehouse.WarehouseName,
                        };
                        entity.Details.Add(detail);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintForecastPlan", ex.Message);
            }
            //return PartialView("PageProductionPlan", tonkho.OrderBy(c => c.CustomerCode).ThenBy(p => p.ProductCode));
            return PartialView("PageForecastPlan", model);
        }

        //forecast sx1
        [GridAction]
        public ActionResult SelectForecastDetail(int customerId, string productCode,
            string fromDate, string toDate, bool all, int warehouseId) {
            var model = new List<ForecastDetailModel>();
            if (string.IsNullOrWhiteSpace(fromDate))
                return View(new GridModel(model));
            try {
                switch (warehouseId) {
                    case (int)MyUtilities.Warehouse.Id.Production1:
                        model = GetForecastOrderProduction_2(customerId, productCode, fromDate, toDate, all);
                        break;
                    case (int)MyUtilities.Warehouse.Id.Cnc:
                        model = GetForecastOrderCnc2(customerId, productCode, fromDate, toDate, all);
                        break;
                    case (int)MyUtilities.Warehouse.Id.Production2:
                        model = GetForecastOrderProduction2_2(customerId, productCode, fromDate, toDate, all);
                        break;
                    case (int)MyUtilities.Warehouse.Id.Plating:
                        model = GetForecastOrderPlating(customerId, productCode, fromDate, toDate, all);
                        break;
                    case (int)MyUtilities.Warehouse.Id.QcA:
                        model = GetForecastOrderQc_2(customerId, productCode, fromDate, toDate, all);
                        break;
                    case (int)MyUtilities.Warehouse.Id.Business:
                        model = GetForecastOrderTotal(customerId, productCode, fromDate, toDate, all);
                        break;
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectForecastDetail", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }

        private List<ForecastDetailModel> GetForecastOrderTotal(int customerId, string productCode,
            string fromDate, string toDate, bool all) {
            var model = new List<ForecastDetailModel>();
            var ci = new CultureInfo("vi-VN");
            var fDate = string.IsNullOrWhiteSpace(fromDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(toDate, ci);
            using (var vfi = new tammaContext()) {
                var products = (from p in vfi.Products
                                where p.Active
                                orderby p.Customer.CustomerCode, p.ProductCode
                                select new {
                                    p.ProductId,
                                    p.ProductCode,
                                    p.CustomerId,
                                    p.Customer.CustomerCode,
                                    MillProductivity = p.MillProductivity ?? 0,
                                    Productivity = p.Productivity ?? 0,
                                }).ToList();
                if (!string.IsNullOrWhiteSpace(productCode))
                    products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                var productIds = products.Select(p => p.ProductId).ToList();
                // lay don hang chua giao
                var orderDetails = (from od in vfi.OrderDetails
                                    where
                                    od.Order.DueDate != null &&
                                    od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                    od.Order.DueDate <= tDate &&
                                    (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                     od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                    od.RequiedNumber != 0 &&
                                    (customerId == 0 || od.Order.CustomerId == customerId) &&
                                    productIds.Contains(od.ProductId)
                                    orderby od.Order.DueDate
                                    select new {
                                        od.OrderDetailId,
                                        od.ProductId,
                                        od.RequiedNumber,
                                        DueDate = od.Order.DueDate.Value,
                                    }).ToList();
                // lay du bao trong thang
                var forecasts = from f in vfi.ForecastOrders
                                orderby f.ForecastDate
                                where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                      f.ForecastDate.Month == fDate.Month &&
                                      f.ForecastDate.Year == fDate.Year &&
                                      (customerId == 0 || f.Product.CustomerId == customerId) &&
                                      productIds.Contains(f.ProductId)
                                select f;

                //du bao thang ke
                var nextMonth = fDate.AddMonths(1);
                var orderDetailsNextMonth = (from od in vfi.OrderDetails
                                             where
                                             od.Order.DueDate != null &&
                                             od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                             od.Order.DueDate.Value.Month == nextMonth.Month &&
                                             od.Order.DueDate.Value.Year == nextMonth.Year &&
                                             (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                              od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                             od.RequiedNumber != 0 &&
                                             (customerId == 0 || od.Order.CustomerId == customerId) &&
                                             productIds.Contains(od.ProductId)
                                             orderby od.Order.DueDate
                                             select new {
                                                 od.OrderDetailId,
                                                 od.ProductId,
                                                 od.RequiedNumber,
                                                 DueDate = od.Order.DueDate.Value,
                                             }).ToList();
                var forecastsNextMonth = from f in vfi.ForecastOrders
                                         orderby f.ForecastDate
                                         where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                               f.ForecastDate.Month == nextMonth.Month &&
                                               f.ForecastDate.Year == nextMonth.Year &&
                                               (customerId == 0 || f.Product.CustomerId == customerId) &&
                                               productIds.Contains(f.ProductId)
                                         select f;

                // gop id giua don hang va du bao
                productIds = forecasts.Select(fo => fo.ProductId).Distinct().ToList();
                productIds.AddRange(forecastsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                productIds.AddRange(orderDetails.Select(o => o.ProductId).Distinct().ToList());
                productIds.AddRange(orderDetailsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                productIds = productIds.Distinct().ToList();

                // xuat ban trong thang
                var exports = from id in vfi.InvoiceDetails
                              where id.Active &&
                                    id.Invoice.ShipmentDate.Value.Month == fDate.Month &&
                                    id.Invoice.ShipmentDate.Value.Year == fDate.Year &&
                                    productIds.Contains(id.ProductId.Value)
                              //(customerId == 0 || id.Invoice.CustomerId == customerId)
                              select new {
                                  id.Piece,
                                  id.ProductId
                              };
                // tat ca ton kho
                var allWarehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity2();
                var totalInvs = from pi in vfi.ProductInventories
                                where productIds.Contains(pi.ProductId) &&
                                allWarehouses.Contains(pi.WarehouseId) &&
                                      pi.TotalQty > 0
                                select new {
                                    pi.ProductId,
                                    pi.TotalQty,
                                    pi.WarehouseId
                                };
                var productionProcesses = from pp in vfi.ProductionProcesses
                                          where productIds.Contains(pp.ProductId) &&
                                                pp.WarehouseId != MyUtilities.Warehouse.Finish &&
                                                pp.IsNecessary
                                          orderby pp.ProcessIndex descending
                                          select pp;
                var productionSections = from ps in vfi.ProductionSections
                                         where productIds.Contains(ps.ProductId) &&
                                               ps.Active && ps.SectionId != MyUtilities.Section.ReProcessSection
                                         select ps;
                // lay san pham co tien trinh cua qc
                var qcs = MyUtilities.Warehouse.GetWarehouseIdQc();
                var sx2s = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                var platings = MyUtilities.Warehouse.GetWarehouseIds_Plating();
                foreach (var productId in productIds) {
                    var product = products.FirstOrDefault(p => p.ProductId == productId);
                    var entity = new ForecastDetailModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        CustomerId = product.CustomerId,
                        CustomerCode = product.CustomerCode,
                        ForecastDate = DateTime.Now,
                        Days = 0,
                        ProductivityInDay = 0,
                        Productivity = product.Productivity,
                        RealProduction1 = product.Productivity,
                    };
                    if (entity.Productivity > 0) {
                        entity.ProductivityInDay = MyUtilities.Product.GetProductionRateInFactoryDayTime(entity.Productivity);
                        entity.ProductionDay =
                            MyUtilities.Function.RoundUp(entity.RequireProduction / entity.ProductivityInDay);
                    }
                    // forecast
                    var forecast = forecasts.FirstOrDefault(f => f.ProductId == entity.ProductId);
                    if (forecast != null) {
                        entity.ForecastQuantity = forecast.Quantity;
                        entity.ForecastDate = forecast.ForecastDate;
                    }
                    forecast = forecastsNextMonth.FirstOrDefault(f => f.ProductId == entity.ProductId);
                    if (forecast != null) {
                        entity.ForecastNextMonth = forecast.Quantity;
                        entity.NextForecastDate = forecast.ForecastDate;
                    }

                    // order
                    var orderDetailsById = orderDetails.Where(od => od.ProductId == entity.ProductId).ToList();
                    if (orderDetailsById.Any()) {
                        entity.LastOrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                        entity.OrderDate = orderDetailsById.FirstOrDefault().DueDate;
                        foreach (var orderDetail in orderDetailsById) {
                            var detailProcess = entity.Orders.FirstOrDefault(od => od.Date == orderDetail.DueDate);
                            if (detailProcess == null) {
                                detailProcess = new OrderDetailProcess() {
                                    DateStr = orderDetail.DueDate.ToString("dd/MM"),
                                    Date = orderDetail.DueDate,
                                    OrderQuantity = orderDetail.RequiedNumber
                                };
                                entity.Orders.Add(detailProcess);
                            }
                            else {
                                detailProcess.OrderQuantity += orderDetail.RequiedNumber;
                            }
                        }
                    }
                    orderDetailsById = orderDetailsNextMonth.Where(od => od.ProductId == entity.ProductId).ToList();
                    if (orderDetailsById.Any()) {
                        entity.OrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                        entity.NextOrderDate = orderDetailsById.FirstOrDefault().DueDate;
                        foreach (var orderDetail in orderDetailsById) {
                            var detailProcess = entity.Orders.FirstOrDefault(od => od.Date == orderDetail.DueDate);
                            if (detailProcess == null) {
                                detailProcess = new OrderDetailProcess() {
                                    DateStr = orderDetail.DueDate.ToString("dd/MM"),
                                    Date = orderDetail.DueDate,
                                    OrderQuantity = orderDetail.RequiedNumber
                                };
                                entity.Orders.Add(detailProcess);
                            }
                            else {
                                detailProcess.OrderQuantity += orderDetail.RequiedNumber;
                            }
                        }
                    }

                    // export
                    var exportsById = exports.Where(e => e.ProductId == entity.ProductId).ToList();
                    entity.ExportQuantity = exportsById.Sum(e => e.Piece);

                    // total quantity need process
                    entity.ProcessQuantity =
                        Math.Max(entity.ForecastQuantity + entity.ForecastNextMonth - entity.ExportQuantity,
                            entity.LastOrderQuantity + entity.OrderQuantity);
                    var productionProcess = productionProcesses.Where(pp => pp.ProductId == entity.ProductId);
                    var invsById = totalInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                    entity.TotalInv = invsById.Sum(pi => pi.TotalQty);
                    var invs = invsById.Where(pi => pi.WarehouseId == MyUtilities.Warehouse.Finish).ToList();
                    entity.WarehouseInv = invs.Sum(pi => pi.TotalQty);
                    entity.RequireProduction = entity.ProcessQuantity - entity.WarehouseInv;
                    if (entity.RequireProduction < 0)
                        entity.RequireProduction = 0;
                    if (!all) if (entity.RequireProduction == 0) continue;
                    var requireQuantity = entity.RequireProduction;
                    foreach (var process in productionProcess) {
                        if (requireQuantity == 0) break;
                        //var invsById = totalInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                        if (qcs.Contains(process.WarehouseId))
                            invs = invsById.Where(pi => qcs.Contains(pi.WarehouseId)).ToList();
                        else if (platings.Contains(process.WarehouseId))
                            invs = invsById.Where(pi => platings.Contains(pi.WarehouseId)).ToList();
                        else if (sx2s.Contains(process.WarehouseId)) {
                            if (!process.IsAlert) continue;
                            var sectionsById = productionSections.Where(ps => ps.ProductId == entity.ProductId).ToList();
                            if (sectionsById.Any())
                                entity.RealProduction3 = sectionsById.Max(ps => ps.Productivity);
                            invs = invsById.Where(pi => sx2s.Contains(pi.WarehouseId)).ToList();
                        }
                        else if (process.WarehouseId == MyUtilities.Warehouse.Cnc) {
                            entity.RealProduction2 = product.MillProductivity;
                            invs = invsById.Where(pi => pi.WarehouseId == process.WarehouseId).ToList();
                        }
                        else
                            invs = invsById.Where(pi => pi.WarehouseId == process.WarehouseId).ToList();

                        var detailProcess = new ForecastDetailProcess() {
                            WarehouseId = process.WarehouseId,
                            WarehouseName = process.Warehouse.ShortName,
                            TotalInv = invs.Sum(pi => pi.TotalQty),
                        };
                        detailProcess.RequireQuantity = requireQuantity - detailProcess.TotalInv;
                        if (detailProcess.RequireQuantity < 0)
                            detailProcess.RequireQuantity = 0;
                        requireQuantity = detailProcess.RequireQuantity;
                        entity.Processes.Add(detailProcess);
                    }

                    model.Add(entity);
                }
            }
            return model;
        }
        private List<ForecastDetailModel> GetForecastOrderQc_2(int customerId, string productCode,
            string fromDate, string toDate, bool all) {
            var model = new List<ForecastDetailModel>();
            if (string.IsNullOrWhiteSpace(productCode))
                productCode = "";
            var ci = new CultureInfo("vi-VN");
            var fDate = string.IsNullOrWhiteSpace(fromDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(toDate, ci);
            using (var vfi = new tammaContext()) {
                var products = (from p in vfi.Products
                                where p.Active
                                orderby p.Customer.CustomerCode, p.ProductCode
                                select new {
                                    p.ProductId,
                                    p.ProductCode,
                                    p.CustomerId,
                                    p.Customer.CustomerCode,
                                }).ToList();
                if (!string.IsNullOrWhiteSpace(productCode))
                    products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                var productIds = products.Select(p => p.ProductId).ToList();
                // lay don hang chua giao
                var orderDetails = (from od in vfi.OrderDetails
                                    where
                                    od.Order.DueDate != null &&
                                    od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                    od.Order.DueDate <= tDate &&
                                    (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                     od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                    od.RequiedNumber != 0 &&
                                    (customerId == 0 || od.Order.CustomerId == customerId) &&
                                    productIds.Contains(od.ProductId)
                                    orderby od.Order.DueDate
                                    select new {
                                        od.OrderDetailId,
                                        od.ProductId,
                                        od.RequiedNumber,
                                        DueDate = od.Order.DueDate.Value,
                                    }).ToList();
                // lay du bao trong thang
                var forecasts = from f in vfi.ForecastOrders
                                orderby f.ForecastDate
                                where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                      f.ForecastDate.Month == fDate.Month &&
                                      f.ForecastDate.Year == fDate.Year &&
                                      (customerId == 0 || f.Product.CustomerId == customerId) &&
                                      productIds.Contains(f.ProductId)
                                select f;

                //du bao thang ke
                var nextMonth = fDate.AddMonths(1);
                var orderDetailsNextMonth = (from od in vfi.OrderDetails
                                             where
                                             od.Order.DueDate != null &&
                                             od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                             od.Order.DueDate.Value.Month == nextMonth.Month &&
                                             od.Order.DueDate.Value.Year == nextMonth.Year &&
                                             (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                              od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                             od.RequiedNumber != 0 &&
                                             (customerId == 0 || od.Order.CustomerId == customerId) &&
                                             productIds.Contains(od.ProductId)
                                             orderby od.Order.DueDate
                                             select new {
                                                 od.OrderDetailId,
                                                 od.ProductId,
                                                 od.RequiedNumber,
                                                 DueDate = od.Order.DueDate.Value,
                                             }).ToList();
                var forecastsNextMonth = from f in vfi.ForecastOrders
                                         orderby f.ForecastDate
                                         where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                               f.ForecastDate.Month == nextMonth.Month &&
                                               f.ForecastDate.Year == nextMonth.Year &&
                                               (customerId == 0 || f.Product.CustomerId == customerId) &&
                                               productIds.Contains(f.ProductId)
                                         select f;

                // gop id giua don hang va du bao
                productIds = forecasts.Select(fo => fo.ProductId).Distinct().ToList();
                productIds.AddRange(forecastsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                productIds.AddRange(orderDetails.Select(o => o.ProductId).Distinct().ToList());
                productIds.AddRange(orderDetailsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                productIds = productIds.Distinct().ToList();


                // xuat ban trong thang
                var exports = from id in vfi.InvoiceDetails
                              where id.Active &&
                                    id.Invoice.ShipmentDate.Value.Month == fDate.Month &&
                                    id.Invoice.ShipmentDate.Value.Year == fDate.Year &&
                                    productIds.Contains(id.ProductId.Value)
                              //(customerId == 0 || id.Invoice.CustomerId == customerId)
                              select new {
                                  id.Piece,
                                  id.ProductId
                              };
                // tat ca ton kho
                var allWarehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity2();
                var totalInvs = from pi in vfi.ProductInventories
                                where productIds.Contains(pi.ProductId) &&
                                      allWarehouses.Contains(pi.WarehouseId) &&
                                      pi.TotalQty > 0
                                select new {
                                    pi.ProductId,
                                    pi.TotalQty,
                                    pi.WarehouseId
                                };
                var afterInvs = from pi in totalInvs
                                where productIds.Contains(pi.ProductId) &&
                                      pi.WarehouseId == MyUtilities.Warehouse.Finish &&
                                      pi.TotalQty > 0
                                select pi;
                // lay san pham co tien trinh cua qc
                var qcs = MyUtilities.Warehouse.GetWarehouseIdQc();
                var productInvs = from pi in totalInvs
                                  where productIds.Contains(pi.ProductId) &&
                                        qcs.Contains(pi.WarehouseId) &&
                                        pi.TotalQty > 0
                                  select pi;
                var createdDate = (from t in vfi.Transactions
                                   where ((t.WarehouseReceiptId == MyUtilities.Warehouse.Finish &&
                                           qcs.Contains(t.WarehouseIssueId.Value)) ||
                                          (t.WarehouseReceiptId == MyUtilities.Warehouse.QcC &&
                                           qcs.Contains(t.WarehouseIssueId.Value))) &&
                                         t.Status == (byte)MyUtilities.Transaction.Status.Approved
                                   orderby t.CreatedDate descending
                                   select t.CreatedDate).FirstOrDefault();
                var transactionDetails = from td in vfi.TransactionDetails
                                         where ((td.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Finish &&
                                                 qcs.Contains(td.Transaction.WarehouseIssueId.Value)) ||
                                                (td.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.QcC &&
                                                 qcs.Contains(td.Transaction.WarehouseIssueId.Value))) &&
                                               td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                               td.Transaction.CreatedDate == createdDate &&
                                               productIds.Contains(td.ReferenceId.Value)
                                         select new {
                                             td.ReferenceId,
                                             WarehouseIssueId = td.Transaction.WarehouseIssueId.Value,
                                             WarehouseReceiptId = td.Transaction.WarehouseReceiptId.Value,
                                             td.Quantity
                                         };
                foreach (var productId in productIds) {
                    var product = products.FirstOrDefault(p => p.ProductId == productId);
                    var entity = new ForecastDetailModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        CustomerId = product.CustomerId,
                        CustomerCode = product.CustomerCode,
                        ForecastDate = DateTime.Now,
                        Days = 0
                    };

                    // forecast
                    var forecast = forecasts.FirstOrDefault(f => f.ProductId == entity.ProductId);
                    if (forecast != null) {
                        entity.ForecastQuantity = forecast.Quantity;
                        entity.ForecastDate = forecast.ForecastDate;
                    }
                    forecast = forecastsNextMonth.FirstOrDefault(f => f.ProductId == entity.ProductId);
                    if (forecast != null) {
                        entity.ForecastNextMonth = forecast.Quantity;
                        entity.NextForecastDate = forecast.ForecastDate;
                    }

                    // order
                    var orderDetailsById = orderDetails.Where(od => od.ProductId == entity.ProductId).ToList();
                    if (orderDetailsById.Any()) {
                        entity.LastOrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                        entity.OrderDate = orderDetailsById.FirstOrDefault().DueDate;
                    }
                    orderDetailsById = orderDetailsNextMonth.Where(od => od.ProductId == entity.ProductId).ToList();
                    if (orderDetailsById.Any()) {
                        entity.OrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                        entity.NextOrderDate = orderDetailsById.FirstOrDefault().DueDate;
                    }

                    // export
                    var exportsById = exports.Where(e => e.ProductId == entity.ProductId).ToList();
                    entity.ExportQuantity = exportsById.Sum(e => e.Piece);

                    // total quantity need process
                    entity.ProcessQuantity =
                        Math.Max(entity.ForecastQuantity + entity.ForecastNextMonth - entity.ExportQuantity,
                            entity.LastOrderQuantity + entity.OrderQuantity);

                    // inventory finish and take require process
                    var productInvsById = totalInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                    entity.TotalInv = productInvsById.Sum(pi => pi.TotalQty);
                    productInvsById = afterInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                    entity.AfterInv = productInvsById.Sum(pi => pi.TotalQty);

                    entity.RequireProduction = entity.ProcessQuantity - entity.AfterInv;
                    if (entity.RequireProduction <= 0) {
                        if (!all)
                            continue;
                        entity.RequireProduction = 0;
                    }
                    else {
                        entity.Days = 3;
                        entity.StartDate = entity.ForecastDate.Value.AddDays(entity.Days * -1);
                        if (entity.StartDate < DateTime.Now)
                            entity.StartDate = DateTime.Now;
                        if (entity.WarehouseInv > 0)
                            entity.EndDate = entity.StartDate.Value.AddDays(entity.Days);
                        entity.EndDate3 = entity.StartDate.Value.AddDays(entity.Days);
                    }
                    // inventory qc
                    productInvsById = productInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                    entity.WarehouseInv = productInvsById.Sum(pi => pi.TotalQty);

                    // qc transaction by lastDate
                    var transactionDetailsById = transactionDetails
                        .Where(td => td.ReferenceId == entity.ProductId &&
                                     td.WarehouseReceiptId == MyUtilities.Warehouse.QcC).ToList();
                    entity.RealProduction = transactionDetailsById.Sum(td => td.Quantity);
                    transactionDetailsById = transactionDetails
                        .Where(td => td.ReferenceId == entity.ProductId &&
                                     td.WarehouseReceiptId == MyUtilities.Warehouse.Finish).ToList();
                    entity.RealProduction1 = transactionDetailsById.Sum(td => td.Quantity);
                    model.Add(entity);
                }
            }
            return model;
        }

        private List<ForecastDetailModel> GetForecastOrderProduction_2(int customerId, string productCode,
            string fromDate, string toDate, bool all) {
            var model = new List<ForecastDetailModel>();
            var ci = new CultureInfo("vi-VN");
            var fDate = string.IsNullOrWhiteSpace(fromDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(toDate, ci);
            using (var vfi = new tammaContext()) {
                //var productIds = productionProcesses.Select(fo => fo.ProductId).Distinct().ToList();
                var products = (from p in vfi.Products
                                where p.Active
                                orderby p.Customer.CustomerCode, p.ProductCode
                                select new {
                                    p.ProductId,
                                    p.ProductCode,
                                    p.CustomerId,
                                    p.Customer.CustomerCode,
                                    Productivity = p.Productivity ?? 0
                                }).ToList();
                if (!string.IsNullOrWhiteSpace(productCode))
                    products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                var productIds = products.Select(p => p.ProductId).ToList();

                // lay don hang chua giao
                var orderDetails = (from od in vfi.OrderDetails
                                    where
                                    od.Order.DueDate != null &&
                                    od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                    od.Order.DueDate <= tDate &&
                                    (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                     od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                    od.RequiedNumber != 0 &&
                                    (customerId == 0 || od.Order.CustomerId == customerId) &&
                                    productIds.Contains(od.ProductId)
                                    orderby od.Order.DueDate
                                    select new {
                                        od.OrderDetailId,
                                        od.ProductId,
                                        od.RequiedNumber,
                                        DueDate = od.Order.DueDate.Value,
                                    }).ToList();
                // lay du bao trong thang
                var forecasts = from f in vfi.ForecastOrders
                                orderby f.ForecastDate
                                where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                      f.ForecastDate.Month == fDate.Month &&
                                      f.ForecastDate.Year == fDate.Year &&
                                      (customerId == 0 || f.Product.CustomerId == customerId) &&
                                      productIds.Contains(f.ProductId)
                                select f;

                //du bao thang ke
                var nextMonth = fDate.AddMonths(1);
                var orderDetailsNextMonth = (from od in vfi.OrderDetails
                                             where
                                             od.Order.DueDate != null &&
                                             od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                             od.Order.DueDate.Value.Month == nextMonth.Month &&
                                             od.Order.DueDate.Value.Year == nextMonth.Year &&
                                             (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                              od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                             od.RequiedNumber != 0 &&
                                             (customerId == 0 || od.Order.CustomerId == customerId) &&
                                             productIds.Contains(od.ProductId)
                                             orderby od.Order.DueDate
                                             select new {
                                                 od.OrderDetailId,
                                                 od.ProductId,
                                                 od.RequiedNumber,
                                                 DueDate = od.Order.DueDate.Value,
                                             }).ToList();
                var forecastsNextMonth = from f in vfi.ForecastOrders
                                         orderby f.ForecastDate
                                         where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                               f.ForecastDate.Month == nextMonth.Month &&
                                               f.ForecastDate.Year == nextMonth.Year &&
                                               (customerId == 0 || f.Product.CustomerId == customerId) &&
                                               productIds.Contains(f.ProductId)
                                         select f;

                // gop id giua don hang va du bao
                productIds = forecasts.Select(fo => fo.ProductId).Distinct().ToList();
                productIds.AddRange(forecastsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                productIds.AddRange(orderDetails.Select(o => o.ProductId).Distinct().ToList());
                productIds.AddRange(orderDetailsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                productIds = productIds.Distinct().ToList();

                // xuat ban trong thang
                var exports = from id in vfi.InvoiceDetails
                              where id.Active &&
                                    id.Invoice.ShipmentDate.Value.Month == fDate.Month &&
                                    id.Invoice.ShipmentDate.Value.Year == fDate.Year &&
                                    productIds.Contains(id.ProductId.Value)
                              //(customerId == 0 || id.Invoice.CustomerId == customerId)
                              select new {
                                  id.Piece,
                                  id.ProductId
                              };
                // tat ca ton kho
                var allWarehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity2();
                var totalInvs = from pi in vfi.ProductInventories
                                where productIds.Contains(pi.ProductId) &&
                                      allWarehouses.Contains(pi.WarehouseId) &&
                                      pi.TotalQty > 0
                                select new {
                                    pi.ProductId,
                                    pi.TotalQty,
                                };

                var materialUseDate = (from sx in vfi.ImportFormSX1
                                       where sx.MaterialUseDate <= tDate &&
                                             sx.ImportWorkpieceMaterials.Any() &&
                                             sx.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                             sx.ImportWorkpieceMaterials.FirstOrDefault()
                                                 .Transaction.Status ==
                                             (byte)MyUtilities.Transaction.Status.Approved
                                       orderby sx.MaterialUseDate descending
                                       select sx.MaterialUseDate).FirstOrDefault();
                var importSx1Details = (from sx in vfi.ImportFormSX1Detail
                                        where sx.ImportFormSX1.MaterialUseDate == materialUseDate &&
                                             sx.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                             sx.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                             sx.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                                 .Transaction.Status ==
                                             (byte)MyUtilities.Transaction.Status.Approved
                                             && sx.ImportFormSX1.Status == (byte) MyUtilities.Transaction.Status.Approved
                                        select new {
                                            sx.MachineId,
                                            sx.ProductId,
                                            Quantity = sx.Number1 + sx.Number2 + sx.Processing1 + sx.Processing2,
                                        }).ToList();
                foreach (var productId in productIds) {
                    var product = products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null) continue;
                    var entity = new ForecastDetailModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        CustomerId = product.CustomerId,
                        CustomerCode = product.CustomerCode,
                        ForecastDate = DateTime.Now,
                        Days = 0,
                        ProductivityInDay = 0,
                        Productivity = product.Productivity,
                    };

                    // forecast
                    var forecast = forecasts.FirstOrDefault(f => f.ProductId == entity.ProductId);
                    if (forecast != null) {
                        entity.ForecastQuantity = forecast.Quantity;
                        entity.ForecastDate = forecast.ForecastDate;
                    }
                    forecast = forecastsNextMonth.FirstOrDefault(f => f.ProductId == entity.ProductId);
                    if (forecast != null) {
                        entity.ForecastNextMonth = forecast.Quantity;
                        entity.NextForecastDate = forecast.ForecastDate;
                    }

                    var orderDetailsById = orderDetails.Where(od => od.ProductId == entity.ProductId).ToList();
                    if (orderDetailsById.Any()) {
                        entity.LastOrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                        entity.OrderDate = orderDetailsById.FirstOrDefault().DueDate;
                    }

                    // order
                    orderDetailsById = orderDetailsNextMonth.Where(od => od.ProductId == entity.ProductId).ToList();
                    if (orderDetailsById.Any()) {
                        entity.OrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                        entity.NextOrderDate = orderDetailsById.FirstOrDefault().DueDate;
                    }

                    // export
                    var exportsById = exports.Where(e => e.ProductId == entity.ProductId).ToList();
                    entity.ExportQuantity = exportsById.Sum(e => e.Piece);

                    // total quantity need process
                    entity.ProcessQuantity =
                        Math.Max(entity.ForecastQuantity + entity.ForecastNextMonth - entity.ExportQuantity,
                            entity.LastOrderQuantity + entity.OrderQuantity);

                    // inventory total and take require process
                    var productInvsById = totalInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                    entity.TotalInv = productInvsById.Sum(pi => pi.TotalQty);

                    entity.RequireProduction = entity.ProcessQuantity - entity.TotalInv;
                    if (entity.RequireProduction <= 0) {
                        if (!all)
                            continue;
                        entity.RequireProduction = 0;
                    }
                    // calculate start date and end date
                    if (entity.Productivity > 0) {
                        entity.ProductivityInDay = MyUtilities.Product.GetProductionRateInFactoryDayTime(entity.Productivity);
                        entity.ProductionDay =
                            MyUtilities.Function.RoundUp(entity.RequireProduction / entity.ProductivityInDay);
                    }
                    if (entity.RequireProduction > 0) {
                        entity.Days = MyUtilities.Product.GetProductionDay(entity.ProductId,
                            entity.ProcessQuantity);
                        entity.StartDate = entity.ForecastDate.Value.AddDays(entity.Days * -1);
                        if (entity.StartDate < DateTime.Now)
                            entity.StartDate = DateTime.Now;

                        entity.EndDate = entity.StartDate.Value.AddDays(entity.ProductionDay);
                        entity.EndDate3 = entity.StartDate.Value.AddDays(entity.Days);
                    }
                    // production by materialUseDate = toDate
                    var importSx1DetailsById = importSx1Details
                        .Where(td => td.ProductId == entity.ProductId)
                        .ToList();
                    if (importSx1DetailsById.Any()) {
                        entity.MachineCount = importSx1DetailsById.Select(id => id.MachineId).Distinct().Count();
                        entity.RealProduction = importSx1DetailsById.Sum(td => td.Quantity);
                    }
                    model.Add(entity);
                }
            }
            return model;

        }

        private List<ForecastDetailModel> GetForecastOrderProduction(int customerId, string productCode,
            string fromDate, string toDate, bool all) {
            var model = new List<ForecastDetailModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fDate = string.IsNullOrWhiteSpace(fromDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    var forecasts = from f in vfi.ForecastOrders
                                    orderby f.ForecastDate
                                    where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                          f.ForecastDate >= fDate &&
                                          f.ForecastDate <= tDate &&
                                          (customerId == 0 || f.Product.CustomerId == customerId) &&
                                          f.Product.ProductCode.Contains(productCode)
                                    select f;
                    var productIds = forecasts.Select(fo => fo.ProductId).Distinct().ToList();
                    //var nextMonth = new DateTime(year, month, 1).AddMonths(1);
                    var orders = (from od in vfi.OrderDetails
                                  where
                                      od.Order.DueDate != null &&
                                      od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                      od.Order.DueDate < tDate &&
                                      (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                       od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                      od.RequiedNumber != 0 &&
                                      (customerId == 0 || od.Product.CustomerId == customerId) &&
                                      od.Product.ProductCode.Contains(productCode)
                                  select new {
                                      od.OrderDetailId,
                                      od.ProductId,
                                      od.RequiedNumber,
                                      DueDate = od.Order.DueDate.Value,
                                  }).ToList();
                    var productIds2 = orders.Select(o => o.ProductId).Distinct().ToList();
                    productIds.AddRange(productIds2);
                    productIds = productIds.Distinct().ToList();
                    var lastOrders = (from od in orders
                                      where od.DueDate < fDate
                                      select od).ToList();
                    var ordersInMonth = (from od in orders
                                         where od.DueDate >= fDate
                                         select od).ToList();
                    var lastProduction = (from sx in vfi.ImportFormSX1
                                          where
                                              sx.ImportWorkpieceMaterials.Any() &&
                                              sx.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                              sx.ImportWorkpieceMaterials.FirstOrDefault()
                                                .Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved
                                          orderby sx.MaterialUseDate descending
                                          select sx).FirstOrDefault();
                    var lastProductions = (from sx in vfi.ImportFormSX1Detail
                                           where productIds.Contains(sx.ProductId) &&
                                                 sx.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                                 sx.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                                 sx.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                                   .Transaction.Status ==
                                                 (byte)MyUtilities.Transaction.Status.Approved &&
                                                 lastProduction.MaterialUseDate == sx.ImportFormSX1.MaterialUseDate
                                             && sx.ImportFormSX1.Status == (byte)MyUtilities.Transaction.Status.Approved
                                           select new {
                                               sx.ProductId,
                                               sx.ImportFormSX1.MaterialUseDate,
                                               Quantity =
                                           (sx.Number1 + sx.Number2 + sx.Processing1 + sx.Processing2),
                                               sx.MachineId,
                                               sx.Machine1.MachineName
                                           }).ToList();
                    var warehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                    var productInvs = vfi.ProductInventories.Where(
                        pi => productIds.Contains(pi.ProductId) &&
                              warehouses.Contains(pi.WarehouseId));

                    var exports = from id in vfi.InvoiceDetails
                                  where id.Active &&
                                        id.Invoice.ShipmentDate >= fDate &&
                                        id.Invoice.ShipmentDate <= tDate &&
                                        id.OrderDetail.Order.DueDate >= fDate &&
                                        id.OrderDetail.Order.DueDate <= tDate &&
                                        productIds.Contains(id.ProductId.Value)
                                  select id;
                    var exportInMonth = from pip in vfi.ProductInventoryPeriods
                                        where productIds.Contains(pip.ProductId) &&
                                              pip.WarehouseId == MyUtilities.Warehouse.Business &&
                                              pip.PeriodDate >= fDate &&
                                              pip.PeriodDate <= tDate
                                        select pip;

                    var products = vfi.Products.Where(p => productIds.Contains(p.ProductId) && p.Active)
                                      .OrderBy(p => p.Customer.CustomerCode)
                                      .ThenBy(p => p.ProductCode)
                                      .ToList();
                    var warehouse =
                        vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == MyUtilities.Warehouse.Production1);
                    var i = 0;
                    foreach (var product in products) {
                        var forecastsById = forecasts.Where(f => f.ProductId == product.ProductId).ToList();
                        var lastOrderQuantity = lastOrders.Where(o => o.ProductId == product.ProductId)
                                                          .ToList().Sum(o => o.RequiedNumber);
                        var ordersById = ordersInMonth.Where(o => o.ProductId == product.ProductId).ToList();
                        var exportQuantity = (double)exports.Where(e => e.ProductId == product.ProductId)
                                                    .ToList().Sum(e => e.Piece);
                        var exportInMonthQuantity = exportInMonth.Where(e => e.ProductId == product.ProductId)
                                                                 .ToList()
                                                                 .Sum(e => e.LastPeriodQuantity - e.EarlyPeriodQuantity);
                        var invQuantity = productInvs.Where(pi => pi.ProductId == product.ProductId)
                                                     .ToList().Sum(pi => pi.TotalQty);
                        //invQuantity -= lastOrderQuantity;
                        //if (invQuantity < 0)
                        //    invQuantity = 0;
                        if (!all &&
                            invQuantity >= (forecastsById.Sum(f => f.Quantity) + lastOrderQuantity) - exportQuantity)
                            continue;
                        //var forecast = forecastsById.FirstOrDefault();
                        var entity = new ForecastDetailModel {
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.Customer.CustomerCode,
                            WarehouseId = MyUtilities.Warehouse.Production1,
                            WarehouseName = warehouse.WarehouseName,
                            //ForecastQuantity = forecastsById.Sum(f => f.Quantity),
                            TotalInv = invQuantity,
                            LastOrderQuantity = lastOrderQuantity,
                            ExportQuantity = exportQuantity,
                            //ForecastDate = forecast.ForecastDate,
                            Productivity = product.Productivity ?? 0,
                            ProductivityInDay = 0,
                            //ForecastOrderId = forecast.ForecastOrderId,
                            IsWorking = false,
                            ExportInMonthQuantity = exportInMonthQuantity
                        };
                        if (forecastsById.Any()) {
                            foreach (var forecastOrder in forecastsById) {
                                if (exportQuantity > forecastOrder.Quantity) {
                                    exportQuantity -= forecastOrder.Quantity;
                                    continue;
                                }
                                entity.ForecastDate = forecastOrder.ForecastDate;
                                break;
                            }
                            entity.ForecastQuantity = forecastsById.Sum(f => f.Quantity);
                            //entity.ForecastDate = forecastsById.FirstOrDefault().ForecastDate;
                            entity.ForecastOrderId = forecastsById.FirstOrDefault().ForecastOrderId;
                        }
                        if (ordersById.Any()) {
                            entity.OrderQuantity = ordersById.Sum(o => o.RequiedNumber);
                            entity.OrderDate = ordersById.FirstOrDefault().DueDate;
                        }
                        var productionsById = lastProductions.Where(lp => lp.ProductId == entity.ProductId);
                        if (productionsById.Any()) {
                            entity.RealProduction = productionsById.Sum(p => p.Quantity);
                            entity.MachineCount = productionsById.Select(p => p.MachineId).Distinct().Count();
                        }
                        else {
                            var date = DateTime.Now.AddDays(-4);
                            var smart = vfi
                                .ImportFormSX1Detail
                                .FirstOrDefault(sx =>
                                                sx.ProductId == entity.ProductId &&
                                                sx.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                                sx.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                                sx.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                                  .Transaction.Status ==
                                                (byte)MyUtilities.Transaction.Status.Approved &&
                                                sx.ImportFormSX1.MaterialUseDate > date
                                             && sx.ImportFormSX1.Status == (byte)MyUtilities.Transaction.Status.Approved);
                            if (smart != null)
                                entity.MachineCount = 1;
                        }
                        if (entity.Productivity > 0)
                            entity.ProductivityInDay = MyUtilities.Product.GetProductionRateInFactoryDayTime(entity.Productivity);
                        entity.ProcessQuantity = MyUtilities.Function.RoundUp((entity.ForecastQuantity + lastOrderQuantity) -
                                                 exportQuantity - invQuantity, 2);
                        if (entity.ProcessQuantity < 0)
                            entity.ProcessQuantity = 0;
                        var forecastDetail =
                            vfi.ForecastOrderDetails.FirstOrDefault(
                                f =>
                                f.WarehouseId == MyUtilities.Warehouse.Production1 &&
                                f.ForecastOrderId == entity.ForecastOrderId);
                        if (forecastDetail != null) {
                            entity.StartDate = forecastDetail.StartDate;
                            entity.EndDate = forecastDetail.EndDate;
                            entity.ModifiedDate = forecastDetail.ModifiedDate;
                            entity.ModifiedUser = forecastDetail.ModifiedUser;

                            var days = MyUtilities.Function.Days(entity.StartDate.Value, entity.EndDate.Value);
                            if (days > 0) {
                                entity.RequireProduction = MyUtilities.Function.RoundUp(entity.ProcessQuantity / days, 2);
                            }
                            entity.IsWorking = true;
                        }
                        else if (entity.ForecastDate != null || entity.OrderDate != null) {
                            var pendingDay = 3;// qc + tp
                            if (product.ProductionPlatings.Any(pp => pp.Active)) {
                                pendingDay += product.ProductionPlatings.Where(pp => pp.Active).Sum(pp => pp.PlatingDay);
                            }
                            if (product.OrderProgresses.Any(
                                op => op.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment))
                                pendingDay++;
                            if (product.OrderProgresses.Any(
                                op => op.WarehouseId == MyUtilities.Warehouse.HeatTreatment))
                                pendingDay++;
                            if (product.OrderProgresses.Any(
                                op => op.WarehouseId == MyUtilities.Warehouse.Production2))
                                pendingDay += (product.ProductionSections.Count(ps => ps.Active) + 1);
                            if (product.OrderProgresses.Any(
                                op => op.WarehouseId == MyUtilities.Warehouse.Cnc))
                                pendingDay += 1;
                            if (entity.LastOrderQuantity > 0) {
                                entity.RequireProduction = MyUtilities.Function.RoundUp(entity.ProcessQuantity / 25 * 1.2, 2);
                            }
                            else {
                                var days = entity.ForecastDate != null
                                               ? MyUtilities.Function.Days(DateTime.Now,
                                                                           entity.ForecastDate.Value.AddDays(pendingDay *
                                                                                                             -1))
                                               : MyUtilities.Function.Days(DateTime.Now,
                                                                           entity.OrderDate.Value.AddDays(pendingDay * -1));
                                if (days > 0) {
                                    entity.RequireProduction = MyUtilities.Function.RoundUp(
                                        entity.ProcessQuantity / days, 2);
                                }
                            }
                            if (entity.MachineCount > 0)
                                entity.StartDate = DateTime.Now.Date;
                            if (entity.ProductivityInDay + entity.RealProduction > 0) {
                                var productivity = entity.RealProduction > 0
                                                       ? entity.RealProduction
                                                       : entity.ProductivityInDay;
                                if (entity.LastOrderQuantity > 0 && entity.StartDate != null) {
                                    entity.EndDate = MyUtilities.Function
                                                                .ToDate(entity.StartDate.Value,
                                                                        MyUtilities.Function.RoundUp(
                                                                            entity.LastOrderQuantity /
                                                                            productivity));
                                }
                                if (entity.OrderQuantity > 0 && entity.StartDate != null) {
                                    if (entity.EndDate != null)
                                        entity.EndDate2 = MyUtilities.Function
                                                                     .ToDate(entity.EndDate.Value,
                                                                             MyUtilities.Function.RoundUp(
                                                                                 entity.OrderQuantity /
                                                                                 productivity));
                                    else
                                        entity.EndDate2 = MyUtilities.Function
                                                                     .ToDate(entity.StartDate.Value,
                                                                             MyUtilities.Function.RoundUp(
                                                                                 entity.OrderQuantity /
                                                                                 productivity));
                                }
                                if ((entity.ForecastQuantity - entity.ExportQuantity) > entity.OrderQuantity &&
                                    entity.StartDate != null) {
                                    if (entity.EndDate2 != null)
                                        entity.EndDate3 = MyUtilities.Function
                                                                     .ToDate(entity.EndDate2.Value,
                                                                             MyUtilities.Function.RoundUp(
                                                                                 (entity.ForecastQuantity -
                                                                                  entity.ExportQuantity -
                                                                                  entity.OrderQuantity) /
                                                                                 productivity));
                                    else if (entity.EndDate != null)
                                        entity.EndDate3 = MyUtilities.Function
                                                                     .ToDate(entity.EndDate.Value,
                                                                             MyUtilities.Function.RoundUp(
                                                                                 (entity.ForecastQuantity -
                                                                                  entity.ExportQuantity -
                                                                                  entity.OrderQuantity) /
                                                                                 productivity));
                                    else
                                        entity.EndDate3 = MyUtilities.Function
                                                                     .ToDate(entity.StartDate.Value,
                                                                             MyUtilities.Function.RoundUp(
                                                                                 (entity.ForecastQuantity -
                                                                                  entity.ExportQuantity -
                                                                                  entity.OrderQuantity) /
                                                                                 productivity));
                                }
                            }
                        }
                        if (entity.ForecastQuantity > entity.ProcessQuantity) {
                            entity.RequireProduction = MyUtilities.Function.RoundUp(entity.ForecastQuantity / 25 * 1.2, 2);
                        }
                        else if (entity.ProcessQuantity > 0 && entity.RequireProduction == 0) {
                            entity.RequireProduction = MyUtilities.Function.RoundUp(entity.ProcessQuantity / 25 * 1.2, 2);
                        }
                        if (entity.RequireProduction < entity.ProductivityInDay && entity.ProcessQuantity > 0)
                            entity.RequireProduction = entity.ProductivityInDay;
                        entity.Index = ++i;
                        if (entity.LastOrderQuantity > 0)
                            entity.Status = 0;
                        else if (entity.OrderQuantity > 0)
                            entity.Status = 1;
                        else if (entity.ProcessQuantity > 0)
                            entity.Status = 2;
                        else entity.Status = 3;
                        model.Add(entity);
                        //exportQuantity = 0;
                        //}
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model.OrderBy(m => m.Status).ToList();
        }

        private List<ForecastDetailModel> GetForecastOrderCnc2(int customerId, string productCode,
            string fromDate, string toDate, bool all) {
            var model = new List<ForecastDetailModel>();

            var ci = new CultureInfo("vi-VN");
            var fDate = string.IsNullOrWhiteSpace(fromDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(toDate, ci);
            using (var vfi = new tammaContext()) {
                // lay nhung san pham co process sx2
                var productionProcesses = from pp in vfi.ProductionProcesses
                                          where //productIds.Contains(pp.ProductId) &&
                                                pp.IsNecessary &&
                                                pp.WarehouseId == MyUtilities.Warehouse.Cnc
                                          select pp;
                var productIds = productionProcesses.Select(fo => fo.ProductId).Distinct().ToList();
                var products = (from p in vfi.Products
                                where productIds.Contains(p.ProductId)
                                orderby p.Customer.CustomerCode, p.ProductCode
                                select new {
                                    p.ProductId,
                                    p.ProductCode,
                                    p.CustomerId,
                                    p.Customer.CustomerCode,
                                    MillProductivity = p.MillProductivity ?? 0
                                }).ToList();
                if (!string.IsNullOrWhiteSpace(productCode))
                    products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                productIds = products.Select(p => p.ProductId).ToList();
                // lay don hang chua giao
                var orderDetails = (from od in vfi.OrderDetails
                                    where
                                    od.Order.DueDate != null &&
                                    od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                    od.Order.DueDate <= tDate &&
                                    (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                     od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                    od.RequiedNumber != 0 &&
                                    (customerId == 0 || od.Order.CustomerId == customerId) &&
                                    productIds.Contains(od.ProductId)
                                    orderby od.Order.DueDate
                                    select new {
                                        od.OrderDetailId,
                                        od.ProductId,
                                        od.RequiedNumber,
                                        DueDate = od.Order.DueDate.Value,
                                    }).ToList();
                // lay du bao trong thang
                var forecasts = from f in vfi.ForecastOrders
                                orderby f.ForecastDate
                                where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                      f.ForecastDate.Month == fDate.Month &&
                                      f.ForecastDate.Year == fDate.Year &&
                                      (customerId == 0 || f.Product.CustomerId == customerId) &&
                                      productIds.Contains(f.ProductId)
                                select f;

                //du bao thang ke
                var nextMonth = fDate.AddMonths(1);
                var orderDetailsNextMonth = (from od in vfi.OrderDetails
                                             where
                                             od.Order.DueDate != null &&
                                             od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                             od.Order.DueDate.Value.Month == nextMonth.Month &&
                                             od.Order.DueDate.Value.Year == nextMonth.Year &&
                                             (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                              od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                             od.RequiedNumber != 0 &&
                                             (customerId == 0 || od.Order.CustomerId == customerId) &&
                                             productIds.Contains(od.ProductId)
                                             orderby od.Order.DueDate
                                             select new {
                                                 od.OrderDetailId,
                                                 od.ProductId,
                                                 od.RequiedNumber,
                                                 DueDate = od.Order.DueDate.Value,
                                             }).ToList();
                var forecastsNextMonth = from f in vfi.ForecastOrders
                                         orderby f.ForecastDate
                                         where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                               f.ForecastDate.Month == nextMonth.Month &&
                                               f.ForecastDate.Year == nextMonth.Year &&
                                               (customerId == 0 || f.Product.CustomerId == customerId) &&
                                               productIds.Contains(f.ProductId)
                                         select f;

                // gop id giua don hang va du bao
                productIds = forecasts.Select(fo => fo.ProductId).Distinct().ToList();
                productIds.AddRange(forecastsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                productIds.AddRange(orderDetails.Select(o => o.ProductId).Distinct().ToList());
                productIds.AddRange(orderDetailsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                productIds = productIds.Distinct().ToList();

                // xuat ban trong thang
                var exports = from id in vfi.InvoiceDetails
                              where id.Active &&
                                    id.Invoice.ShipmentDate.Value.Month == fDate.Month &&
                                    id.Invoice.ShipmentDate.Value.Year == fDate.Year &&
                                    productIds.Contains(id.ProductId.Value)
                              //(customerId == 0 || id.Invoice.CustomerId == customerId)
                              select new {
                                  id.Piece,
                                  id.ProductId
                              };
                // tat ca ton kho trừ CNC
                var allWarehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity2();
                var totalInvs = from pi in vfi.ProductInventories
                                where productIds.Contains(pi.ProductId) &&
                                      allWarehouses.Contains(pi.WarehouseId) &&
                                      pi.TotalQty > 0
                                select new {
                                    pi.ProductId,
                                    pi.TotalQty,
                                    pi.WarehouseId
                                };
                //var productInvs = from pi in totalInvs
                //                  where productIds.Contains(pi.ProductId) &&
                //                      pi.WarehouseId == MyUtilities.Warehouse.Cnc &&
                //                        pi.TotalQty > 0
                //                  select pi;

                var transaction = (from t in vfi.Transactions
                                   where t.WarehouseIssueId == MyUtilities.Warehouse.Cnc &&
                                         allWarehouses.Contains(t.WarehouseReceiptId.Value) &&
                                         t.Status == (byte)MyUtilities.Transaction.Status.Approved
                                   orderby t.CreatedDate descending
                                   select t).FirstOrDefault();
                var transactionDetails = from td in vfi.TransactionDetails
                                         where td.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Cnc &&
                                               allWarehouses.Contains(td.Transaction.WarehouseReceiptId.Value) &&
                                               td.Transaction.CreatedDate == transaction.CreatedDate &&
                                               productIds.Contains(td.ReferenceId.Value) &&
                                               td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved
                                         select new {
                                             td.ReferenceId,
                                             //WarehouseIssueId = td.Transaction.WarehouseIssueId.Value,
                                             //WarehouseReceiptId = td.Transaction.WarehouseReceiptId.Value,
                                             td.Quantity
                                         };

                //var qcWarehouses = MyUtilities.Warehouse.GetWarehouseIdQc();
                //var platingWarehouses = MyUtilities.Warehouse.GetWarehouseIds_Plating();
                foreach (var productId in productIds) {
                    var product = products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null) continue;
                    var entity = new ForecastDetailModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        CustomerId = product.CustomerId,
                        CustomerCode = product.CustomerCode,
                        ForecastDate = DateTime.Now,
                        Days = 0,
                        Productivity = product.MillProductivity
                    };

                    // forecast
                    var forecast = forecasts.FirstOrDefault(f => f.ProductId == entity.ProductId);
                    if (forecast != null) {
                        entity.ForecastQuantity = forecast.Quantity;
                        entity.ForecastDate = forecast.ForecastDate;
                    }
                    forecast = forecastsNextMonth.FirstOrDefault(f => f.ProductId == entity.ProductId);
                    if (forecast != null) {
                        entity.ForecastNextMonth = forecast.Quantity;
                        entity.NextForecastDate = forecast.ForecastDate;
                    }

                    // order
                    var orderDetailsById = orderDetails.Where(od => od.ProductId == entity.ProductId).ToList();
                    if (orderDetailsById.Any()) {
                        entity.LastOrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                        entity.OrderDate = orderDetailsById.FirstOrDefault().DueDate;
                    }
                    orderDetailsById = orderDetailsNextMonth.Where(od => od.ProductId == entity.ProductId).ToList();
                    if (orderDetailsById.Any()) {
                        entity.OrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                        entity.NextOrderDate = orderDetailsById.FirstOrDefault().DueDate;
                    }

                    // export
                    var exportsById = exports.Where(e => e.ProductId == entity.ProductId).ToList();
                    entity.ExportQuantity = exportsById.Sum(e => e.Piece);

                    // total quantity need process
                    entity.ProcessQuantity =
                        Math.Max(entity.ForecastQuantity + entity.ForecastNextMonth - entity.ExportQuantity,
                            entity.LastOrderQuantity + entity.OrderQuantity);

                    // inventory after production 2 and take require process
                    var productInvsById = totalInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                    entity.TotalInv = productInvsById.Sum(pi => pi.TotalQty);
                    // production 2 inventory
                    productInvsById = productInvsById.Where(pi => pi.WarehouseId == MyUtilities.Warehouse.Cnc).ToList();
                    entity.WarehouseInv = productInvsById.Sum(pi => pi.TotalQty);
                    entity.AfterInv = entity.TotalInv - entity.WarehouseInv;

                    entity.RequireProduction = entity.ProcessQuantity - entity.AfterInv;
                    if (entity.RequireProduction <= 0) {
                        if (!all)
                            continue;
                        entity.RequireProduction = 0;
                    }

                    // calculate start date and end date
                    if (entity.Productivity > 0) {
                        entity.ProductivityInDay = MyUtilities.Product.GetProductionRateInFactoryDayTime(entity.Productivity);
                        entity.ProductionDay =
                            MyUtilities.Function.RoundUp(entity.RequireProduction / entity.ProductivityInDay);
                    }
                    if (entity.RequireProduction > 0) {
                        entity.Days = MyUtilities.Product.GetPlanDay(entity.ProductId,
                            entity.ProcessQuantity, MyUtilities.Warehouse.Cnc);
                        entity.StartDate = entity.ForecastDate.Value.AddDays(entity.Days * -1);
                        if (entity.StartDate < DateTime.Now)
                            entity.StartDate = DateTime.Now;

                        entity.EndDate = entity.StartDate.Value.AddDays(entity.ProductionDay);
                        entity.EndDate3 = entity.StartDate.Value.AddDays(entity.Days);
                    }

                    var transactionDetailsById = transactionDetails.Where(td => td.ReferenceId == entity.ProductId)
                        .ToList();
                    entity.RealProduction = transactionDetailsById.Sum(td => td.Quantity);
                    model.Add(entity);
                }
            }
            return model;
        }


        private List<ForecastDetailModel> GetForecastOrderCnc(int customerId, string productCode,
            string fromDate, string toDate, bool all) {

            var model = new List<ForecastDetailModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fDate = string.IsNullOrWhiteSpace(fromDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    var products = (from p in vfi.Products
                                    where p.Active &&
                                          (customerId == 0 || p.CustomerId == customerId) &&
                                          p.ProductCode.Contains(productCode) &&
                                          p.ProductionProcesses
                                           .FirstOrDefault(
                                               pp => pp.WarehouseId == MyUtilities.Warehouse.Cnc &&
                                                     pp.IsNecessary) != null
                                    select p).ToList();
                    var productIds = products.Select(p => p.ProductId).ToList();
                    var forecasts = from f in vfi.ForecastOrders
                                    orderby f.ForecastDate
                                    where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                          f.ForecastDate >= fDate &&
                                          f.ForecastDate <= tDate &&
                                          productIds.Contains(f.ProductId)
                                    select f;

                    //var nextMonth = new DateTime(year, month, 1).AddMonths(1);
                    var orders = (from od in vfi.OrderDetails
                                  where
                                      od.Order.DueDate != null &&
                                      od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                      od.Order.DueDate <= tDate &&
                                      (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                       od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                      od.RequiedNumber != 0 &&
                                      productIds.Contains(od.ProductId)
                                  select new {
                                      od.OrderDetailId,
                                      od.ProductId,
                                      od.RequiedNumber,
                                      DueDate = od.Order.DueDate.Value,
                                  }).ToList();


                    productIds = forecasts.Select(fo => fo.ProductId).Distinct().ToList();
                    var productIds2 = orders.Select(o => o.ProductId).Distinct().ToList();
                    productIds.AddRange(productIds2);
                    productIds = productIds.Distinct().ToList();

                    var warehouses = MyUtilities.Warehouse.GetWarehouseId_Process();
                    var productInvs = vfi.ProductInventories.Where(
                        pi => productIds.Contains(pi.ProductId) &&
                              warehouses.Contains(pi.WarehouseId) &&
                              pi.WarehouseId != MyUtilities.Warehouse.Cnc);
                    var production2Invs = vfi.ProductInventories.Where(
                        pi => productIds.Contains(pi.ProductId) &&
                               pi.WarehouseId == MyUtilities.Warehouse.Cnc);
                    var lastOrders = (from od in orders
                                      where
                                          od.DueDate < fDate
                                      select od).ToList();
                    var ordersInMonth = (from od in orders
                                         where
                                             od.DueDate >= fDate
                                         select od).ToList();
                    var exports = from id in vfi.InvoiceDetails
                                  where id.Active &&
                                        id.Invoice.ShipmentDate >= fDate &&
                                        id.Invoice.ShipmentDate <= tDate &&
                                        id.OrderDetail.Order.DueDate >= fDate &&
                                        id.OrderDetail.Order.DueDate <= tDate &&
                                        productIds.Contains(id.ProductId.Value)
                                  select id;
                    var lastProduction = (from sx in vfi.ImportFormCncs
                                          join t in vfi.Transactions
                                              on sx.TransactionCode equals t.TransactionCode
                                              into sxt
                                          where
                                              sxt.FirstOrDefault().Status == (byte)MyUtilities.Transaction.Status.Approved
                                          orderby sx.ImportDate descending
                                          select sx).FirstOrDefault();
                    var lastProductions = (from sx in vfi.ImportFormCncDetails
                                           join t in vfi.Transactions
                                               on sx.ImportFormCnc.TransactionCode equals t.TransactionCode
                                               into sxt
                                           where
                                               !sxt.Any(t2 => t2.Status != (byte)MyUtilities.Transaction.Status.Approved) &&
                                               sx.ImportFormCnc.ImportDate == lastProduction.ImportDate &&
                                               productIds.Contains(sx.ProductId)
                                           select new {
                                               sx.ProductId,
                                               Quantity =
                                           (sx.Number1 + sx.Number2 + sx.Processing1 + sx.Processing2),
                                               sx.MachineId,
                                           }).ToList();
                    products = products.Where(p => productIds.Contains(p.ProductId) && p.Active)
                                      .OrderBy(p => p.Customer.CustomerCode)
                                      .ThenBy(p => p.ProductCode)
                                      .ToList();
                    //var warehouse =
                    //    vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == MyUtilities.Warehouse.Production2);
                    var i = 0;
                    foreach (var product in products) {
                        var forecastsById = forecasts.Where(f => f.ProductId == product.ProductId).ToList();
                        var lastOrderQuantity = lastOrders.Where(o => o.ProductId == product.ProductId)
                                                          .ToList().Sum(o => o.RequiedNumber);
                        var ordersById = ordersInMonth.Where(o => o.ProductId == product.ProductId).ToList();
                        var exportQuantity = (double)exports.Where(e => e.ProductId == product.ProductId)
                                                    .ToList().Sum(e => e.Piece);
                        var invQuantity = productInvs.Where(pi => pi.ProductId == product.ProductId)
                                                     .ToList().Sum(pi => pi.TotalQty);
                        var cnc2Inv = production2Invs.Where(pi => pi.ProductId == product.ProductId)
                                                     .ToList().Sum(pi => pi.TotalQty);
                        //var smallestProductivity = product.CncProductivity ?? 0;
                        //invQuantity -= lastOrderQuantity;
                        //if (invQuantity < 0)
                        //    invQuantity = 0;
                        if (!all &&
                            invQuantity >= (forecastsById.Sum(f => f.Quantity) + lastOrderQuantity) - exportQuantity)
                            continue;
                        var entity = new ForecastDetailModel {
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerCode = product.Customer.CustomerCode,
                            //WarehouseId = MyUtilities.Warehouse.Production1,
                            //WarehouseName = warehouse.WarehouseName,
                            //ForecastQuantity = forecastsById.Sum(f => f.Quantity),
                            TotalInv = invQuantity,
                            WarehouseInv = cnc2Inv,
                            LastOrderQuantity = lastOrderQuantity,
                            ExportQuantity = exportQuantity,
                            //ForecastDate = forecast.ForecastDate,
                            Productivity = product.CncProductivity ?? 0,
                            ProductivityInDay = 0,
                            //ForecastOrderId = forecast.ForecastOrderId,
                            IsWorking = false
                        };
                        if (forecastsById.Any()) {
                            foreach (var forecastOrder in forecastsById) {
                                if (exportQuantity > forecastOrder.Quantity) {
                                    exportQuantity -= forecastOrder.Quantity;
                                    continue;
                                }
                                entity.ForecastDate = forecastOrder.ForecastDate;
                                break;
                            }
                            entity.ForecastQuantity = forecastsById.Sum(f => f.Quantity);
                            //entity.ForecastDate = forecastsById.FirstOrDefault().ForecastDate;
                            entity.ForecastOrderId = forecastsById.FirstOrDefault().ForecastOrderId;
                        }
                        if (ordersById.Any()) {
                            entity.OrderQuantity = ordersById.Sum(o => o.RequiedNumber);
                            entity.OrderDate = ordersById.FirstOrDefault().DueDate;
                        }
                        var productionsById = lastProductions.Where(lp => lp.ProductId == entity.ProductId);
                        if (productionsById.Any()) {
                            entity.RealProduction = productionsById.Sum(p => p.Quantity);
                            entity.MachineCount = productionsById.Select(p => p.MachineId).Distinct().Count();
                        }
                        else {
                            var date = DateTime.Now.AddDays(-4);
                            var smart = (from sx in vfi.ImportFormCncDetails
                                         join t in vfi.Transactions
                                             on sx.ImportFormCnc.TransactionCode equals t.TransactionCode
                                             into sxt
                                         where
                                             !sxt.Any(t2 => t2.Status != (byte)MyUtilities.Transaction.Status.Approved) &&
                                             sx.ImportFormCnc.ImportDate > date &&
                                             sx.ProductId == entity.ProductId
                                         select new {
                                             sx.ProductId,
                                             Quantity =
                                         (sx.Number1 + sx.Number2 + sx.Processing1 + sx.Processing2),
                                             sx.MachineId,
                                         }).FirstOrDefault();
                            if (smart != null)
                                entity.MachineCount = 1;
                        }
                        if (entity.Productivity > 0)
                            entity.ProductivityInDay = MyUtilities.Product.GetProductionRateInFactoryDayTime(entity.Productivity);
                        entity.ProcessQuantity = MyUtilities.Function.RoundUp((entity.ForecastQuantity + lastOrderQuantity) -
                                                 exportQuantity - invQuantity, 2);
                        if (entity.ProcessQuantity < 0)
                            entity.ProcessQuantity = 0;
                        var forecastDetail =
                            vfi.ForecastOrderDetails.FirstOrDefault(
                                f =>
                                f.WarehouseId == MyUtilities.Warehouse.Cnc &&
                                f.ForecastOrderId == entity.ForecastOrderId);
                        var day = 25;
                        if (forecastDetail != null) {
                            entity.StartDate = forecastDetail.StartDate;
                            entity.EndDate = forecastDetail.EndDate;
                            entity.ModifiedDate = forecastDetail.ModifiedDate;
                            entity.ModifiedUser = forecastDetail.ModifiedUser;

                            day = MyUtilities.Function.Days(entity.StartDate.Value, entity.EndDate.Value);
                            //if (day > 0)
                            //{
                            //    entity.RequireProduction = MyUtilities.Function.RoundUp(entity.ProcessQuantity / day, 2);
                            //}
                            entity.IsWorking = true;
                        }
                        else if (entity.ForecastDate != null || entity.OrderDate != null) {
                            var pendingDay = 3;// qc + tp
                            if (product.ProductionPlatings.Any(pp => pp.Active)) {
                                pendingDay += product.ProductionPlatings.Where(pp => pp.Active).Sum(pp => pp.PlatingDay);
                            }
                            if (product.OrderProgresses.Any(
                                op => op.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment))
                                pendingDay++;
                            if (product.OrderProgresses.Any(
                                op => op.WarehouseId == MyUtilities.Warehouse.HeatTreatment))
                                pendingDay++;
                            if (product.OrderProgresses.Any(
                                op => op.WarehouseId == MyUtilities.Warehouse.Production2))
                                pendingDay += (product.ProductionSections.Count(ps => ps.Active) + 1);
                            if (entity.LastOrderQuantity > 0) {
                                //entity.RequireProduction = MyUtilities.Function.RoundUp(entity.ProcessQuantity / 25 * 1.2, 2);
                            }
                            else {
                                day = entity.ForecastDate != null
                                              ? MyUtilities.Function.Days(DateTime.Now,
                                                                          entity.ForecastDate.Value.AddDays(pendingDay *
                                                                                                            -1))
                                              : MyUtilities.Function.Days(DateTime.Now,
                                                                          entity.OrderDate.Value.AddDays(pendingDay * -1));
                                //if (days > 0)
                                //{
                                //    entity.RequireProduction = MyUtilities.Function.RoundUp(
                                //        entity.ProcessQuantity / days, 2);
                                //}
                            }
                            if (entity.MachineCount > 0)
                                entity.StartDate = DateTime.Now.Date;
                            if (entity.ProductivityInDay + entity.RealProduction > 0) {
                                var productivity = entity.RealProduction > 0
                                                       ? entity.RealProduction
                                                       : entity.ProductivityInDay;
                                if (entity.LastOrderQuantity > 0 && entity.StartDate != null) {
                                    entity.EndDate = MyUtilities.Function
                                                                .ToDate(entity.StartDate.Value,
                                                                        MyUtilities.Function.RoundUp(
                                                                            entity.LastOrderQuantity /
                                                                            productivity));
                                }
                                if (entity.OrderQuantity > 0 && entity.StartDate != null) {
                                    if (entity.EndDate != null)
                                        entity.EndDate2 = MyUtilities.Function
                                                                     .ToDate(entity.EndDate.Value,
                                                                             MyUtilities.Function.RoundUp(
                                                                                 entity.OrderQuantity /
                                                                                 productivity));
                                    else
                                        entity.EndDate2 = MyUtilities.Function
                                                                     .ToDate(entity.StartDate.Value,
                                                                             MyUtilities.Function.RoundUp(
                                                                                 entity.OrderQuantity /
                                                                                 productivity));
                                }
                                if ((entity.ForecastQuantity - entity.ExportQuantity) > entity.OrderQuantity &&
                                    entity.StartDate != null) {
                                    if (entity.EndDate2 != null)
                                        entity.EndDate3 = MyUtilities.Function
                                                                     .ToDate(entity.EndDate2.Value,
                                                                             MyUtilities.Function.RoundUp(
                                                                                 (entity.ForecastQuantity -
                                                                                  entity.ExportQuantity -
                                                                                  entity.OrderQuantity) /
                                                                                 productivity));
                                    else if (entity.EndDate != null)
                                        entity.EndDate3 = MyUtilities.Function
                                                                     .ToDate(entity.EndDate.Value,
                                                                             MyUtilities.Function.RoundUp(
                                                                                 (entity.ForecastQuantity -
                                                                                  entity.ExportQuantity -
                                                                                  entity.OrderQuantity) /
                                                                                 productivity));
                                    else
                                        entity.EndDate3 = MyUtilities.Function
                                                                     .ToDate(entity.StartDate.Value,
                                                                             MyUtilities.Function.RoundUp(
                                                                                 (entity.ForecastQuantity -
                                                                                  entity.ExportQuantity -
                                                                                  entity.OrderQuantity) /
                                                                                 productivity));
                                }
                            }
                        }

                        if (entity.ProcessQuantity > 0 && entity.RequireProduction == 0) {
                            entity.RequireProduction = MyUtilities.Function.RoundUp(entity.ProcessQuantity / day * 1.2, 2);
                        }
                        if (entity.RequireProduction < entity.ProductivityInDay && entity.ProcessQuantity > 0)
                            entity.RequireProduction = entity.ProductivityInDay;
                        entity.Index = ++i;
                        if (entity.LastOrderQuantity > 0)
                            entity.Status = 0;
                        else if (entity.OrderQuantity > 0)
                            entity.Status = 1;
                        else if (entity.ProcessQuantity > 0)
                            entity.Status = 2;
                        else entity.Status = 3;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model;
        }

        private List<ForecastDetailModel> GetForecastOrderPlating(int customerId, string productCode,
            string fromDate, string toDate, bool all) {
            var model = new List<ForecastDetailModel>();

            var ci = new CultureInfo("vi-VN");
            var fDate = string.IsNullOrWhiteSpace(fromDate)
                ? DateTime.Today
                : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                ? DateTime.Today
                : Convert.ToDateTime(toDate, ci);
            using (var vfi = new tammaContext()) {
                var productionProcesses = from pp in vfi.ProductionProcesses
                                          where //productIds.Contains(pp.ProductId) &&
                                                pp.IsNecessary &&
                                                pp.WarehouseId == MyUtilities.Warehouse.WaitingPlating
                                          select pp;
                var productIds = productionProcesses.Select(fo => fo.ProductId).Distinct().ToList();
                var products = (from p in vfi.Products
                                where productIds.Contains(p.ProductId)
                                orderby p.Customer.CustomerCode, p.ProductCode
                                select new {
                                    p.ProductId,
                                    p.ProductCode,
                                    p.CustomerId,
                                    p.Customer.CustomerCode,
                                }).ToList();
                if (!string.IsNullOrWhiteSpace(productCode))
                    products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                productIds = products.Select(p => p.ProductId).ToList();
                // lay don hang chua giao
                var orderDetails = (from od in vfi.OrderDetails
                                    where
                                    od.Order.DueDate != null &&
                                    od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                    od.Order.DueDate <= tDate &&
                                    (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                     od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                    od.RequiedNumber != 0 &&
                                    (customerId == 0 || od.Order.CustomerId == customerId) &&
                                    productIds.Contains(od.ProductId)
                                    orderby od.Order.DueDate
                                    select new {
                                        od.OrderDetailId,
                                        od.ProductId,
                                        od.RequiedNumber,
                                        DueDate = od.Order.DueDate.Value,
                                    }).ToList();
                // lay du bao trong thang
                var forecasts = from f in vfi.ForecastOrders
                                orderby f.ForecastDate
                                where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                      f.ForecastDate.Month == fDate.Month &&
                                      f.ForecastDate.Year == fDate.Year &&
                                      (customerId == 0 || f.Product.CustomerId == customerId) &&
                                      productIds.Contains(f.ProductId)
                                select f;

                //du bao thang ke
                var nextMonth = fDate.AddMonths(1);
                var orderDetailsNextMonth = (from od in vfi.OrderDetails
                                             where
                                             od.Order.DueDate != null &&
                                             od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                             od.Order.DueDate.Value.Month == nextMonth.Month &&
                                             od.Order.DueDate.Value.Year == nextMonth.Year &&
                                             (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                              od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                             od.RequiedNumber != 0 &&
                                             (customerId == 0 || od.Order.CustomerId == customerId) &&
                                             productIds.Contains(od.ProductId)
                                             orderby od.Order.DueDate
                                             select new {
                                                 od.OrderDetailId,
                                                 od.ProductId,
                                                 od.RequiedNumber,
                                                 DueDate = od.Order.DueDate.Value,
                                             }).ToList();
                var forecastsNextMonth = from f in vfi.ForecastOrders
                                         orderby f.ForecastDate
                                         where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                               f.ForecastDate.Month == nextMonth.Month &&
                                               f.ForecastDate.Year == nextMonth.Year &&
                                               (customerId == 0 || f.Product.CustomerId == customerId) &&
                                               productIds.Contains(f.ProductId)
                                         select f;

                // gop id giua don hang va du bao
                productIds = forecasts.Select(fo => fo.ProductId).Distinct().ToList();
                productIds.AddRange(forecastsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                productIds.AddRange(orderDetails.Select(o => o.ProductId).Distinct().ToList());
                productIds.AddRange(orderDetailsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                productIds = productIds.Distinct().ToList();

                // lay lai tat ca tien trinh cua san pham
                productionProcesses = from pp in vfi.ProductionProcesses
                                      where productIds.Contains(pp.ProductId) &&
                                            pp.IsNecessary
                                      select pp;
                // xuat ban trong thang
                var exports = from id in vfi.InvoiceDetails
                              where id.Active &&
                                    id.Invoice.ShipmentDate.Value.Month == fDate.Month &&
                                    id.Invoice.ShipmentDate.Value.Year == fDate.Year &&
                                    productIds.Contains(id.ProductId.Value)
                              //(customerId == 0 || id.Invoice.CustomerId == customerId)
                              select new {
                                  id.Piece,
                                  id.ProductId
                              };
                // tat ca ton kho
                var allWarehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity2();
                var totalInvs = from pi in vfi.ProductInventories
                                where productIds.Contains(pi.ProductId) &&
                                      allWarehouses.Contains(pi.WarehouseId) &&
                                      pi.TotalQty > 0
                                select new {
                                    pi.ProductId,
                                    pi.TotalQty,
                                    pi.WarehouseId
                                };
                var qcs = MyUtilities.Warehouse.GetWarehouseIdQc();
                qcs.Add(MyUtilities.Warehouse.Finish);
                var afterInvs = from pi in totalInvs
                                where productIds.Contains(pi.ProductId) &&
                                      qcs.Contains(pi.WarehouseId) &&
                                      pi.TotalQty > 0
                                select pi;
                var platings = MyUtilities.Warehouse.GetWarehouseIds_Plating();
                var productInvs = from pi in totalInvs
                                  where productIds.Contains(pi.ProductId) &&
                                        platings.Contains(pi.WarehouseId) &&
                                        pi.TotalQty > 0
                                  select pi;

                var transaction = (from t in vfi.Transactions
                                   where t.WarehouseReceiptId != null &&
                                         t.WarehouseIssueId != null &&
                                         ((t.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating &&
                                           platings.Contains(t.WarehouseReceiptId.Value)) ||
                                          (platings.Contains(t.WarehouseIssueId.Value) &&
                                           t.WarehouseReceiptId == MyUtilities.Warehouse.QcB)) &&
                                         t.Status == (byte)MyUtilities.Transaction.Status.Approved
                                   orderby t.CreatedDate descending
                                   select t).FirstOrDefault();
                var transactionDetails = from td in vfi.TransactionDetails
                                         where td.Transaction.WarehouseReceiptId != null &&
                                               td.Transaction.WarehouseIssueId != null &&
                                               ((td.Transaction.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating &&
                                                 platings.Contains(td.Transaction.WarehouseReceiptId.Value)) ||
                                                (platings.Contains(td.Transaction.WarehouseIssueId.Value) &&
                                                 td.Transaction.WarehouseIssueId == MyUtilities.Warehouse.QcB)) &&
                                               td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                               td.Transaction.CreatedDate == transaction.CreatedDate &&
                                               productIds.Contains(td.ReferenceId.Value)
                                         select new {
                                             td.ReferenceId,
                                             WarehouseIssueId = td.Transaction.WarehouseIssueId.Value,
                                             WarehouseReceiptId = td.Transaction.WarehouseReceiptId.Value,
                                             td.Quantity
                                         };

                foreach (var productId in productIds) {
                    var product = products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null) continue;
                    var entity = new ForecastDetailModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        CustomerId = product.CustomerId,
                        CustomerCode = product.CustomerCode,
                        ForecastDate = DateTime.Now,
                        Days = 0
                    };

                    // forecast
                    var forecast = forecasts.FirstOrDefault(f => f.ProductId == entity.ProductId);
                    if (forecast != null) {
                        entity.ForecastQuantity = forecast.Quantity;
                        entity.ForecastDate = forecast.ForecastDate;
                    }
                    forecast = forecastsNextMonth.FirstOrDefault(f => f.ProductId == entity.ProductId);
                    if (forecast != null) {
                        entity.ForecastNextMonth = forecast.Quantity;
                        entity.NextForecastDate = forecast.ForecastDate;
                    }

                    // order
                    var orderDetailsById = orderDetails.Where(od => od.ProductId == entity.ProductId).ToList();
                    if (orderDetailsById.Any()) {
                        entity.LastOrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                        entity.OrderDate = orderDetailsById.FirstOrDefault().DueDate;
                    }
                    orderDetailsById = orderDetailsNextMonth.Where(od => od.ProductId == entity.ProductId).ToList();
                    if (orderDetailsById.Any()) {
                        entity.OrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                        entity.NextOrderDate = orderDetailsById.FirstOrDefault().DueDate;
                    }

                    // export
                    var exportsById = exports.Where(e => e.ProductId == entity.ProductId).ToList();
                    entity.ExportQuantity = exportsById.Sum(e => e.Piece);

                    // total quantity need process
                    entity.ProcessQuantity =
                        Math.Max(entity.ForecastQuantity + entity.ForecastNextMonth - entity.ExportQuantity,
                            entity.LastOrderQuantity + entity.OrderQuantity);

                    // inventory after plating (qc + finish) and take require process
                    var productInvsById = totalInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                    entity.TotalInv = productInvsById.Sum(pi => pi.TotalQty);
                    productInvsById = afterInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                    entity.AfterInv = productInvsById.Sum(pi => pi.TotalQty);

                    entity.RequireProduction = entity.ProcessQuantity - entity.AfterInv;
                    if (entity.RequireProduction <= 0) {
                        if (!all)
                            continue;
                        entity.RequireProduction = 0;
                    }
                    else {
                        entity.Days = MyUtilities.Product.GetPlanDay(entity.ProductId,
                            entity.ProcessQuantity, MyUtilities.Warehouse.WaitingPlating);

                        entity.StartDate = entity.ForecastDate.Value.AddDays(entity.Days * -1);
                        if (entity.StartDate < DateTime.Now)
                            entity.StartDate = DateTime.Now;

                        var productionPlatings =
                            vfi.ProductionPlatings.Where(ps => ps.ProductId == productId && ps.Active);
                        if (productionPlatings.Any())
                            entity.ProductionDay += productionPlatings.Sum(ps => ps.PlatingDay);
                        else
                            entity.ProductionDay += 1;

                        if (entity.WarehouseInv > 0)
                            entity.EndDate = entity.StartDate.Value.AddDays(entity.ProductionDay);

                        entity.EndDate3 = entity.StartDate.Value.AddDays(entity.Days);
                    }
                    productInvsById = productInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                    entity.WarehouseInv = productInvsById.Sum(pi => pi.TotalQty);
                    //xuat gcn
                    var transactionDetailsById = transactionDetails
                        .Where(td => td.ReferenceId == entity.ProductId &&
                                     td.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating &&
                                     td.WarehouseReceiptId == MyUtilities.Warehouse.Plating).ToList();
                    entity.RealProduction = transactionDetailsById.Sum(td => td.Quantity);
                    transactionDetailsById = transactionDetails
                        .Where(td => td.ReferenceId == entity.ProductId &&
                                     td.WarehouseIssueId == MyUtilities.Warehouse.WaitingPlating &&
                                     td.WarehouseReceiptId == MyUtilities.Warehouse.PlatingTest).ToList();
                    entity.RealProduction1 = transactionDetailsById.Sum(td => td.Quantity);
                    transactionDetailsById = transactionDetails
                        .Where(td => td.ReferenceId == entity.ProductId &&
                                     td.WarehouseIssueId == MyUtilities.Warehouse.Plating &&
                                     td.WarehouseReceiptId == MyUtilities.Warehouse.QcB).ToList();
                    entity.RealProduction2 = transactionDetailsById.Sum(td => td.Quantity);
                    transactionDetailsById = transactionDetails
                        .Where(td => td.ReferenceId == entity.ProductId &&
                                     td.WarehouseIssueId == MyUtilities.Warehouse.PlatingTest &&
                                     td.WarehouseReceiptId == MyUtilities.Warehouse.QcB).ToList();
                    entity.RealProduction3 = transactionDetailsById.Sum(td => td.Quantity);
                    model.Add(entity);
                }
            }
            return model;
        }

        private List<ForecastDetailModel> GetForecastOrderProduction2_2(int customerId, string productCode,
            string fromDate, string toDate, bool all) {
            var model = new List<ForecastDetailModel>();
            try {

                var ci = new CultureInfo("vi-VN");
                var fDate = string.IsNullOrWhiteSpace(fromDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    var sx2s = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    // lay nhung san pham co process sx2
                    var productionProcesses = from pp in vfi.ProductionProcesses
                                              where //productIds.Contains(pp.ProductId) &&
                                                    pp.IsNecessary && pp.IsAlert &&
                                                    pp.WarehouseId == MyUtilities.Warehouse.Production2
                                              select pp;
                    var productIds = productionProcesses.Select(fo => fo.ProductId).Distinct().ToList();
                    var products = (from p in vfi.Products
                                    where productIds.Contains(p.ProductId)
                                    orderby p.Customer.CustomerCode, p.ProductCode
                                    select new {
                                        p.ProductId,
                                        p.ProductCode,
                                        p.CustomerId,
                                        p.Customer.CustomerCode,
                                    }).ToList();
                    if (!string.IsNullOrWhiteSpace(productCode))
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    productIds = products.Select(p => p.ProductId).ToList();
                    // lay don hang chua giao
                    var orderDetails = (from od in vfi.OrderDetails
                                        where
                                        od.Order.DueDate != null &&
                                        od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                        od.Order.DueDate <= tDate &&
                                        (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                         od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                        od.RequiedNumber != 0 &&
                                        (customerId == 0 || od.Order.CustomerId == customerId) &&
                                        productIds.Contains(od.ProductId)
                                        orderby od.Order.DueDate
                                        select new {
                                            od.OrderDetailId,
                                            od.ProductId,
                                            od.RequiedNumber,
                                            DueDate = od.Order.DueDate.Value,
                                        }).ToList();
                    // lay du bao trong thang
                    var forecasts = from f in vfi.ForecastOrders
                                    orderby f.ForecastDate
                                    where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                          f.ForecastDate.Month == fDate.Month &&
                                          f.ForecastDate.Year == fDate.Year &&
                                          (customerId == 0 || f.Product.CustomerId == customerId) &&
                                          productIds.Contains(f.ProductId)
                                    select f;

                    //du bao thang ke
                    var nextMonth = fDate.AddMonths(1);
                    var orderDetailsNextMonth = (from od in vfi.OrderDetails
                                                 where
                                                 od.Order.DueDate != null &&
                                                 od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                                 od.Order.DueDate.Value.Month == nextMonth.Month &&
                                                 od.Order.DueDate.Value.Year == nextMonth.Year &&
                                                 (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                                  od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                                 od.RequiedNumber != 0 &&
                                                 (customerId == 0 || od.Order.CustomerId == customerId) &&
                                                 productIds.Contains(od.ProductId)
                                                 orderby od.Order.DueDate
                                                 select new {
                                                     od.OrderDetailId,
                                                     od.ProductId,
                                                     od.RequiedNumber,
                                                     DueDate = od.Order.DueDate.Value,
                                                 }).ToList();
                    var forecastsNextMonth = from f in vfi.ForecastOrders
                                             orderby f.ForecastDate
                                             where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                   f.ForecastDate.Month == nextMonth.Month &&
                                                   f.ForecastDate.Year == nextMonth.Year &&
                                                   (customerId == 0 || f.Product.CustomerId == customerId) &&
                                                   productIds.Contains(f.ProductId)
                                             select f;

                    // gop id giua don hang va du bao
                    productIds = forecasts.Select(fo => fo.ProductId).Distinct().ToList();
                    productIds.AddRange(forecastsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                    productIds.AddRange(orderDetails.Select(o => o.ProductId).Distinct().ToList());
                    productIds.AddRange(orderDetailsNextMonth.Select(o => o.ProductId).Distinct().ToList());
                    productIds = productIds.Distinct().ToList();

                    // lay san pham co tien trinh cua sx2

                    // lay lai tat ca tien trinh cua san pham
                    productionProcesses = from pp in vfi.ProductionProcesses
                                          where productIds.Contains(pp.ProductId) &&
                                                pp.IsNecessary
                                          select pp;
                    // xuat ban trong thang
                    var exports = from id in vfi.InvoiceDetails
                                  where id.Active &&
                                        id.Invoice.ShipmentDate.Value.Month == fDate.Month &&
                                        id.Invoice.ShipmentDate.Value.Year == fDate.Year &&
                                        productIds.Contains(id.ProductId.Value)
                                  //(customerId == 0 || id.Invoice.CustomerId == customerId)
                                  select new {
                                      id.Piece,
                                      id.ProductId
                                  };
                    // tat ca ton kho
                    var allWarehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity2();
                    var totalInvs = from pi in vfi.ProductInventories
                                    where productIds.Contains(pi.ProductId) &&
                                          allWarehouses.Contains(pi.WarehouseId) &&
                                          pi.TotalQty > 0
                                    select new {
                                        pi.ProductId,
                                        pi.TotalQty,
                                        pi.WarehouseId
                                    };
                    //var productInvs = from pi in totalInvs
                    //                  where productIds.Contains(pi.ProductId) &&
                    //                        sx2s.Contains(pi.WarehouseId) &&
                    //                        pi.TotalQty > 0
                    //                  select pi;
                    // lay tat ca section 
                    var productionSections = from ps in vfi.ProductionSections
                                             where productIds.Contains(ps.ProductId) &&
                                                   ps.Active && ps.SectionId != MyUtilities.Section.ReProcessSection
                                             select ps;

                    var transaction = (from t in vfi.Transactions
                                       where t.WarehouseReceiptId != null &&
                                             t.WarehouseIssueId != null &&
                                             ((t.WarehouseIssueId == MyUtilities.Warehouse.Production2 &&
                                               sx2s.Contains(t.WarehouseReceiptId.Value)) ||
                                              (sx2s.Contains(t.WarehouseIssueId.Value) &&
                                               allWarehouses.Contains(t.WarehouseReceiptId.Value))) &&
                                             t.Status == (byte)MyUtilities.Transaction.Status.Approved
                                       orderby t.CreatedDate descending
                                       select t).FirstOrDefault();

                    var transactionDetails = from td in vfi.TransactionDetails
                                             where td.Transaction.WarehouseReceiptId != null &&
                                                   td.Transaction.WarehouseIssueId != null &&
                                                   ((td.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Production2 &&
                                                     sx2s.Contains(td.Transaction.WarehouseReceiptId.Value)) ||
                                                    (sx2s.Contains(td.Transaction.WarehouseIssueId.Value) &&
                                                     allWarehouses.Contains(td.Transaction.WarehouseReceiptId.Value))) &&
                                                   td.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                   td.Transaction.CreatedDate == transaction.CreatedDate &&
                                                   productIds.Contains(td.ReferenceId.Value)
                                             select new {
                                                 td.ReferenceId,
                                                 WarehouseIssueId = td.Transaction.WarehouseIssueId.Value,
                                                 WarehouseReceiptId = td.Transaction.WarehouseReceiptId.Value,
                                                 td.Quantity
                                             };

                    var qcWarehouses = MyUtilities.Warehouse.GetWarehouseIdQc();
                    var platingWarehouses = MyUtilities.Warehouse.GetWarehouseIds_Plating();
                    var warehouse2_process = MyUtilities.Warehouse.GetWarehouseIdProduction2_PROCESS();
                    foreach (var productId in productIds) {
                        var product = products.FirstOrDefault(p => p.ProductId == productId);
                        if (product == null) continue;
                        var entity = new ForecastDetailModel {
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerId = product.CustomerId,
                            CustomerCode = product.CustomerCode,
                            ForecastDate = DateTime.Now,
                            Days = 0
                        };

                        // forecast
                        var forecast = forecasts.FirstOrDefault(f => f.ProductId == entity.ProductId);
                        if (forecast != null) {
                            entity.ForecastQuantity = forecast.Quantity;
                            entity.ForecastDate = forecast.ForecastDate;
                        }
                        forecast = forecastsNextMonth.FirstOrDefault(f => f.ProductId == entity.ProductId);
                        if (forecast != null) {
                            entity.ForecastNextMonth = forecast.Quantity;
                            entity.NextForecastDate = forecast.ForecastDate;
                        }

                        // order
                        var orderDetailsById = orderDetails.Where(od => od.ProductId == entity.ProductId).ToList();
                        if (orderDetailsById.Any()) {
                            entity.LastOrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                            entity.OrderDate = orderDetailsById.FirstOrDefault().DueDate;
                        }
                        orderDetailsById = orderDetailsNextMonth.Where(od => od.ProductId == entity.ProductId).ToList();
                        if (orderDetailsById.Any()) {
                            entity.OrderQuantity = orderDetailsById.Sum(od => od.RequiedNumber);
                            entity.NextOrderDate = orderDetailsById.FirstOrDefault().DueDate;
                        }

                        // export
                        var exportsById = exports.Where(e => e.ProductId == entity.ProductId).ToList();
                        entity.ExportQuantity = exportsById.Sum(e => e.Piece);



                        // total quantity need process
                        entity.ProcessQuantity =
                            Math.Max(entity.ForecastQuantity + entity.ForecastNextMonth - entity.ExportQuantity,
                                entity.LastOrderQuantity + entity.OrderQuantity);
                        entity.ProcessQuantity1 = Math.Max(entity.ForecastQuantity - entity.ExportQuantity, entity.LastOrderQuantity);
                        entity.ProcessQuantity2 = Math.Max(entity.ForecastNextMonth, entity.OrderQuantity);

                        // inventory after production 2 and take require process
                        var processById =
                            productionProcesses.FirstOrDefault(
                                pp => pp.ProductId == entity.ProductId && pp.WarehouseId == MyUtilities.Warehouse.Production2);
                        if (processById == null) continue;
                        var productionProcess = productionProcesses.Where(pp => pp.ProductId == entity.ProductId &&
                                                                                pp.ProcessIndex > processById.ProcessIndex);
                        foreach (var process in productionProcess) {
                            var invsById = totalInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                            if (qcWarehouses.Contains(process.WarehouseId))
                                invsById = invsById.Where(pi => qcWarehouses.Contains(pi.WarehouseId)).ToList();
                            else if (platingWarehouses.Contains(process.WarehouseId))
                                invsById = invsById.Where(pi => platingWarehouses.Contains(pi.WarehouseId)).ToList();
                            else
                                invsById = invsById.Where(pi => pi.WarehouseId == process.WarehouseId).ToList();

                            entity.AfterInv += invsById.Sum(pi => pi.TotalQty);
                        }
                        entity.RequireProduction = entity.ProcessQuantity - entity.AfterInv;
                        if (entity.RequireProduction <= 0) {
                            if (!all)
                                continue;
                            entity.RequireProduction = 0;
                        }

                        var inv = entity.AfterInv;
                        var require = entity.ProcessQuantity1 - inv;
                        if (require < 0) {
                            inv -= entity.ProcessQuantity1;
                            require = entity.ProcessQuantity2 - inv;
                            if (require < 0) {
                                entity.ProcessQuantity2 = 0;
                            }
                            else {
                                entity.ProcessQuantity2 -= inv;
                            }
                            entity.ProcessQuantity1 = 0;
                        }
                        else {
                            entity.ProcessQuantity1 -= inv;
                        }

                        // production 2 inventory
                        var productInvsById = totalInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                        entity.TotalInv = productInvsById.Sum(pi => pi.TotalQty);
                        productInvsById = productInvsById.Where(pi => sx2s.Contains(pi.WarehouseId)).ToList();
                        entity.WarehouseInv = productInvsById.Sum(pi => pi.TotalQty);

                        //xuat sx2 -> sx2BCD
                        var transactionDetailsById = transactionDetails
                            .Where(td => td.ReferenceId == entity.ProductId &&
                                         td.WarehouseIssueId == MyUtilities.Warehouse.Production2 &&
                                         sx2s.Contains(td.WarehouseReceiptId)).ToList();
                        entity.RealProduction = transactionDetailsById.Sum(td => td.Quantity);

                        transactionDetailsById = transactionDetails
                            .Where(td => td.ReferenceId == entity.ProductId &&
                                         warehouse2_process.Contains(td.WarehouseIssueId) &&
                                         allWarehouses.Contains(td.WarehouseReceiptId)).ToList();
                        entity.RealProduction1 = transactionDetailsById.Sum(td => td.Quantity);

                        // productivity
                        var productionSectionsById = productionSections.Where(ps => ps.ProductId == entity.ProductId).ToList();
                        if (productionSectionsById.Any()) {
                            entity.MachineCount = productionSectionsById.Count;
                            entity.Productivity = productionSectionsById.Max(ps => ps.Productivity);
                            entity.ProductivityInDay1 = productionSectionsById.Sum(ps => ps.Productivity);

                            entity.RealProduction2 = entity.ProductivityInDay1 * entity.ProcessQuantity1;
                            entity.RealProduction3 = entity.ProductivityInDay1 * entity.ProcessQuantity2;
                        }
                        if (entity.Productivity > 0) {
                            entity.ProductivityInDay = MyUtilities.Product.GetProductionRateInDayTime(entity.Productivity);
                            entity.ProductionDay =
                                MyUtilities.Function.RoundUp(entity.RequireProduction / entity.ProductivityInDay);
                        }

                        // calculate start date and end date
                        if (entity.RequireProduction > 0) {
                            entity.Days = MyUtilities.Product.GetPlanDay(entity.ProductId,
                                entity.ProcessQuantity, MyUtilities.Warehouse.Production2);

                            entity.StartDate = entity.ForecastDate.Value.AddDays(entity.Days * -1);
                            if (entity.StartDate < DateTime.Now)
                                entity.StartDate = DateTime.Now;
                            if (entity.WarehouseInv > 0 && entity.ProductionDay > 0)
                                entity.EndDate = entity.StartDate.Value.AddDays(entity.ProductionDay);

                            entity.EndDate3 = entity.StartDate.Value.AddDays(entity.Days);
                        }

                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model;
        }

        private List<ForecastDetailModel> GetForecastOrderProduction2(int customerId, string productCode,
            string fromDate, string toDate, bool all) {
            var model = new List<ForecastDetailModel>();
            var ci = new CultureInfo("vi-VN");
            var fDate = string.IsNullOrWhiteSpace(fromDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                              ? DateTime.Today
                              : Convert.ToDateTime(toDate, ci);
            using (var vfi = new tammaContext()) {
                var products = (from p in vfi.Products
                                where p.Active &&
                                      (customerId == 0 || p.CustomerId == customerId) &&
                                      p.ProductCode.Contains(productCode) &&
                                      p.ProductionProcesses
                                       .FirstOrDefault(
                                           pp => pp.WarehouseId == MyUtilities.Warehouse.Production2 &&
                                                 pp.IsNecessary) != null
                                select p).ToList();
                var productIds = products.Select(p => p.ProductId).ToList();
                var forecasts = from f in vfi.ForecastOrders
                                orderby f.ForecastDate
                                where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                      f.ForecastDate >= fDate &&
                                      f.ForecastDate <= tDate &&
                                      productIds.Contains(f.ProductId)
                                select f;

                //var nextMonth = new DateTime(year, month, 1).AddMonths(1);
                var orders = (from od in vfi.OrderDetails
                              where
                                  od.Order.DueDate != null &&
                                  od.Order.DueDate > MyUtilities.Sales.StartOrderDate &&
                                  od.Order.DueDate < tDate &&
                                  (od.Order.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                   od.Order.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                  od.RequiedNumber != 0 &&
                                  productIds.Contains(od.ProductId)
                              select new {
                                  od.OrderDetailId,
                                  od.ProductId,
                                  od.RequiedNumber,
                                  DueDate = od.Order.DueDate.Value,
                              }).ToList();
                productIds = forecasts.Select(fo => fo.ProductId).Distinct().ToList();
                var productIds2 = orders.Select(o => o.ProductId).Distinct().ToList();
                productIds.AddRange(productIds2);
                productIds = productIds.Distinct().ToList();
                var lastOrders = (from od in orders
                                  where
                                      od.DueDate < fDate
                                  select od).ToList();
                var ordersInMonth = (from od in orders
                                     where
                                         od.DueDate >= fDate
                                     select od).ToList();
                var warehouses = MyUtilities.Warehouse.GetWarehouseId_Process();
                var warehouses2 = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                var productInvs = vfi.ProductInventories.Where(
                    pi => productIds.Contains(pi.ProductId) &&
                          warehouses.Contains(pi.WarehouseId) &&
                          !warehouses2.Contains(pi.WarehouseId) &&
                          pi.WarehouseId != MyUtilities.Warehouse.Cnc);
                var production2Invs = vfi.ProductInventories.Where(
                    pi => productIds.Contains(pi.ProductId) &&
                          warehouses2.Contains(pi.WarehouseId));

                var exports = from id in vfi.InvoiceDetails
                              where id.Active &&
                                    id.Invoice.ShipmentDate >= fDate &&
                                    id.Invoice.ShipmentDate <= tDate &&
                                    id.OrderDetail.Order.DueDate >= fDate &&
                                    id.OrderDetail.Order.DueDate <= tDate &&
                                    productIds.Contains(id.ProductId.Value)
                              select id;
                var lastProduction = (from sx in vfi.Production2Transaction
                                      orderby sx.CreateDate descending
                                      select sx).FirstOrDefault();
                var lastProductions = (from sx in vfi.Production2TransactionDetail
                                       where productIds.Contains(sx.ProductId) &&
                                             sx.Production2Transaction.CreateDate == lastProduction.CreateDate
                                       select sx).ToList();
                var lastProduction1 = (from sx in vfi.ImportFormSX1
                                       where
                                           sx.ImportWorkpieceMaterials.Any() &&
                                           sx.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                           sx.ImportWorkpieceMaterials.FirstOrDefault()
                                             .Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved
                                       orderby sx.MaterialUseDate descending
                                       select sx).FirstOrDefault();
                var lastProduction1s = (from sx in vfi.ImportFormSX1Detail
                                        where productIds.Contains(sx.ProductId) &&
                                              sx.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                              sx.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                              sx.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                                .Transaction.Status ==
                                              (byte)MyUtilities.Transaction.Status.Approved &&
                                              lastProduction1.MaterialUseDate == sx.ImportFormSX1.MaterialUseDate
                                             && sx.ImportFormSX1.Status == (byte)MyUtilities.Transaction.Status.Approved
                                        select new {
                                            sx.ProductId,
                                            sx.ImportFormSX1.MaterialUseDate,
                                            Quantity =
                                        (sx.Number1 + sx.Number2 + sx.Processing1 + sx.Processing2),
                                            sx.MachineId,
                                            sx.Machine1.MachineName
                                        }).ToList();
                var production2Ids = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                var production2Exports = (from pip in vfi.ProductInventoryPeriods
                                          where
                                          production2Ids.Contains(pip.WarehouseId) &&
                                          production2Ids.Contains(pip.Transaction.WarehouseIssueId.Value) &&
                                          pip.Transaction.WarehouseReceiptId != MyUtilities.Warehouse.Defect &&
                                          pip.Transaction.WarehouseReceiptId != MyUtilities.Warehouse.Processing &&
                                          pip.Transaction.WarehouseReceiptId != MyUtilities.Warehouse.Processing2 &&
                                          pip.Transaction.WarehouseReceiptId != MyUtilities.Warehouse.Destroy &&
                                          pip.PeriodDate == lastProduction.CreateDate
                                          select pip).ToList();
                products = vfi.Products.Where(p => productIds.Contains(p.ProductId) && p.Active)
                              .OrderBy(p => p.Customer.CustomerCode)
                              .ThenBy(p => p.ProductCode)
                              .ToList();
                //var warehouse =
                //    vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == MyUtilities.Warehouse.Production2);
                var i = 0;
                foreach (var product in products) {
                    var forecastsById = forecasts.Where(f => f.ProductId == product.ProductId).ToList();
                    var lastOrderQuantity = lastOrders.Where(o => o.ProductId == product.ProductId)
                                                      .ToList().Sum(o => o.RequiedNumber);
                    var ordersById = ordersInMonth.Where(o => o.ProductId == product.ProductId).ToList();
                    var exportQuantity = (double)exports.Where(e => e.ProductId == product.ProductId)
                                                         .ToList().Sum(e => e.Piece);
                    var invQuantity = productInvs.Where(pi => pi.ProductId == product.ProductId)
                                                 .ToList().Sum(pi => pi.TotalQty);
                    var production2Inv = production2Invs.Where(pi => pi.ProductId == product.ProductId)
                                                        .ToList().Sum(pi => pi.TotalQty);
                    var lastSection =
                        product.ProductionSections.Where(ps => ps.ProductId == product.ProductId && ps.Active == true)
                       .OrderByDescending(ps => ps.SectionIndex);
                    //invQuantity -= lastOrderQuantity;
                    //if (invQuantity < 0)
                    //    invQuantity = 0;
                    if (!all &&
                        invQuantity >= (forecastsById.Sum(f => f.Quantity) + lastOrderQuantity) - exportQuantity)
                        continue;
                    var entity = new ForecastDetailModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        CustomerCode = product.Customer.CustomerCode,
                        //WarehouseId = MyUtilities.Warehouse.Production1,
                        //WarehouseName = warehouse.WarehouseName,
                        //ForecastQuantity = forecastsById.Sum(f => f.Quantity),
                        TotalInv = invQuantity,
                        WarehouseInv = production2Inv,
                        LastOrderQuantity = lastOrderQuantity,
                        ExportQuantity = exportQuantity,
                        //ForecastDate = forecast.ForecastDate,
                        //Productivity = smallestProductivity,
                        ProductivityInDay = 0,
                        //ForecastOrderId = forecast.ForecastOrderId,
                        IsWorking = false
                    };
                    if (forecastsById.Any()) {
                        foreach (var forecastOrder in forecastsById) {
                            if (exportQuantity > forecastOrder.Quantity) {
                                exportQuantity -= forecastOrder.Quantity;
                                continue;
                            }
                            entity.ForecastDate = forecastOrder.ForecastDate;
                            break;
                        }
                        entity.ForecastQuantity = forecastsById.Sum(f => f.Quantity);
                        //entity.ForecastDate = forecastsById.FirstOrDefault().ForecastDate;
                        entity.ForecastOrderId = forecastsById.FirstOrDefault().ForecastOrderId;
                    }
                    if (ordersById.Any()) {
                        entity.OrderQuantity = ordersById.Sum(o => o.RequiedNumber);
                        entity.OrderDate = ordersById.FirstOrDefault().DueDate;
                    }
                    var productionsById = lastProductions.Where(lp => lp.ProductId == entity.ProductId);
                    if (productionsById.Any()) {
                        //entity.RealProduction = productionsById.Sum(p => p.Quantity);
                        entity.MachineCount = 1;
                    }
                    else {
                        var date = DateTime.Now.AddDays(-4);
                        var smart = (from sx in vfi.Production2TransactionDetail
                                     where sx.Production2Transaction.CreateDate > date &&
                                           sx.ProductId == entity.ProductId
                                     select sx).FirstOrDefault();
                        if (smart != null)
                            entity.MachineCount = 1;
                    }

                    entity.RealProduction =
                        production2Exports.Where(pip => pip.ProductId == entity.ProductId)
                                          .ToList()
                                          .Sum(pip => pip.EarlyPeriodQuantity - pip.LastPeriodQuantity);
                    if (lastSection.Any())
                        entity.Productivity = lastSection.FirstOrDefault().Productivity;
                    if (entity.Productivity > 0)
                        entity.ProductivityInDay = MyUtilities.Product.GetProductionRateInDayTime(entity.Productivity);
                    entity.ProcessQuantity =
                        MyUtilities.Function.RoundUp((entity.ForecastQuantity + lastOrderQuantity) -
                                                     exportQuantity - invQuantity, 2);
                    if (entity.ProcessQuantity < 0)
                        entity.ProcessQuantity = 0;
                    var forecastDetail =
                        vfi.ForecastOrderDetails.FirstOrDefault(
                            f =>
                            f.WarehouseId == MyUtilities.Warehouse.Cnc &&
                            f.ForecastOrderId == entity.ForecastOrderId);
                    if (forecastDetail != null) {
                        entity.StartDate = forecastDetail.StartDate;
                        entity.EndDate = forecastDetail.EndDate;
                        entity.ModifiedDate = forecastDetail.ModifiedDate;
                        entity.ModifiedUser = forecastDetail.ModifiedUser;

                        var days = MyUtilities.Function.Days(entity.StartDate.Value, entity.EndDate.Value);
                        if (days > 0) {
                            entity.RequireProduction = MyUtilities.Function.RoundUp(entity.ProcessQuantity / days, 2);
                        }
                        entity.IsWorking = true;
                    }
                    else if (entity.ForecastDate != null || entity.OrderDate != null) {
                        var pendingDay = 3; // qc + tp
                        if (product.ProductionPlatings.Any(pp => pp.Active)) {
                            pendingDay += product.ProductionPlatings.Where(pp => pp.Active).Sum(pp => pp.PlatingDay);
                        }
                        if (product.OrderProgresses.Any(
                            op => op.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment))
                            pendingDay++;
                        if (product.OrderProgresses.Any(
                            op => op.WarehouseId == MyUtilities.Warehouse.HeatTreatment))
                            pendingDay++;
                        if (product.OrderProgresses.Any(
                            op => op.WarehouseId == MyUtilities.Warehouse.Production2))
                            pendingDay += (product.ProductionSections.Count(ps => ps.Active) + 1);
                        if (entity.LastOrderQuantity > 0) {
                            entity.RequireProduction = MyUtilities.Function.RoundUp(entity.ProcessQuantity / 25 * 1.2, 2);
                        }
                        else {
                            var days = entity.ForecastDate != null
                                           ? MyUtilities.Function.Days(DateTime.Now,
                                                                       entity.ForecastDate.Value.AddDays(pendingDay *
                                                                                                         -1))
                                           : MyUtilities.Function.Days(DateTime.Now,
                                                                       entity.OrderDate.Value.AddDays(pendingDay * -1));
                            if (days > 0) {
                                entity.RequireProduction = MyUtilities.Function.RoundUp(
                                    entity.ProcessQuantity / days, 2);
                            }
                        }
                        if (entity.MachineCount > 0)
                            entity.StartDate = DateTime.Now.Date;
                        if (entity.ProductivityInDay + entity.RealProduction > 0) {
                            var productivity = entity.RealProduction > 0
                                                   ? entity.RealProduction
                                                   : entity.ProductivityInDay;
                            if (entity.LastOrderQuantity > 0 && entity.StartDate != null) {
                                entity.EndDate = MyUtilities.Function
                                                            .ToDate(entity.StartDate.Value,
                                                                    MyUtilities.Function.RoundUp(
                                                                        entity.LastOrderQuantity /
                                                                        productivity));
                            }
                            if (entity.OrderQuantity > 0 && entity.StartDate != null) {
                                if (entity.EndDate != null)
                                    entity.EndDate2 = MyUtilities.Function
                                                                 .ToDate(entity.EndDate.Value,
                                                                         MyUtilities.Function.RoundUp(
                                                                             entity.OrderQuantity /
                                                                             productivity));
                                else
                                    entity.EndDate2 = MyUtilities.Function
                                                                 .ToDate(entity.StartDate.Value,
                                                                         MyUtilities.Function.RoundUp(
                                                                             entity.OrderQuantity /
                                                                             productivity));
                            }
                            if ((entity.ForecastQuantity - entity.ExportQuantity) > entity.OrderQuantity &&
                                entity.StartDate != null) {
                                if (entity.EndDate2 != null)
                                    entity.EndDate3 = MyUtilities.Function
                                                                 .ToDate(entity.EndDate2.Value,
                                                                         MyUtilities.Function.RoundUp(
                                                                             (entity.ForecastQuantity -
                                                                              entity.ExportQuantity -
                                                                              entity.OrderQuantity) /
                                                                             productivity));
                                else if (entity.EndDate != null)
                                    entity.EndDate3 = MyUtilities.Function
                                                                 .ToDate(entity.EndDate.Value,
                                                                         MyUtilities.Function.RoundUp(
                                                                             (entity.ForecastQuantity -
                                                                              entity.ExportQuantity -
                                                                              entity.OrderQuantity) /
                                                                             productivity));
                                else
                                    entity.EndDate3 = MyUtilities.Function
                                                                 .ToDate(entity.StartDate.Value,
                                                                         MyUtilities.Function.RoundUp(
                                                                             (entity.ForecastQuantity -
                                                                              entity.ExportQuantity -
                                                                              entity.OrderQuantity) /
                                                                             productivity));
                            }
                        }
                    }

                    if (entity.ProcessQuantity > 0 && entity.RequireProduction == 0) {
                        entity.RequireProduction = MyUtilities.Function.RoundUp(entity.ProcessQuantity / 25 * 1.2, 2);
                    }
                    if (entity.RequireProduction < entity.ProductivityInDay && entity.ProcessQuantity > 0)
                        entity.RequireProduction = entity.ProductivityInDay;
                    entity.Index = ++i;
                    if (entity.LastOrderQuantity > 0)
                        entity.Status = 0;
                    else if (entity.OrderQuantity > 0)
                        entity.Status = 1;
                    else if (entity.ProcessQuantity > 0)
                        entity.Status = 2;
                    else entity.Status = 3;
                    var production1sById = lastProduction1s.Where(lp => lp.ProductId == entity.ProductId);
                    if (production1sById.Any()) {
                        entity.RealProduction1 = production1sById.Sum(p => p.Quantity);
                        if (product.Productivity != null && product.Productivity > 0) {
                            var machineCount = production1sById.Select(p => p.MachineId).Distinct().Count();
                            entity.ProductivityInDay1 =
                                MyUtilities.Product.GetProductionRateInFactoryDayTime(product.Productivity.Value) * machineCount;
                        }
                    }
                    entity.RequireProductionOrder =
                        MyUtilities.Function.RoundUp((entity.OrderQuantity + lastOrderQuantity) - invQuantity, 2);
                    if (entity.RequireProductionOrder < 0)
                        entity.RequireProductionOrder = 0;
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.Status).ToList();
        }

        private List<ForecastDetailModel> GetForecastOrderQc(int customerId, string productCode,
            string fromDate, string toDate, bool all) {
            var model = new List<ForecastDetailModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fDate = string.IsNullOrWhiteSpace(fromDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {

                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model.OrderBy(m => m.Status).ToList();
        }

        [GridAction]
        public ActionResult UpdateForecastDetail(ForecastDetailModel update, int customerId, string productCode,
                                               string fromDate, string toDate, bool all, int warehouseId) {
            try {
                if (update.EndDate == null) {
                    throw new AggregateException("Lỗi! Chọn ngày kết thúc lỗi");
                }
                //if (update.StartDate <= DateTime.Now)
                //{
                //    throw new AggregateException("Lỗi! Ngày bắt đầu ko đc nhỏ hơn hiện tại");
                //}
                if (update.StartDate != null && update.StartDate > update.EndDate) {
                    throw new AggregateException("Lỗi! Ngày kết thúc phải lớn hơn ngày bắt đầu");
                }
                using (var vfi = new tammaContext()) {
                    var detail =
                        vfi.ForecastOrderDetails.FirstOrDefault(
                            fo => fo.ForecastOrderId == update.ForecastOrderId && fo.WarehouseId == warehouseId);
                    if (detail == null) {
                        detail = new ForecastOrderDetail {
                            ForecastOrderId = update.ForecastOrderId,
                        };
                        vfi.ForecastOrderDetails.Add(detail);
                    }
                    detail.ForecastQuantity = update.ProcessQuantity;
                    detail.StartDate = update.StartDate.Value;
                    detail.EndDate = update.EndDate.Value;
                    detail.ModifiedDate = DateTime.Now;
                    detail.ModifiedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateForecastDetail", ex.Message);
            }
            switch (warehouseId) {
                case (int)MyUtilities.Warehouse.Id.Production1:
                    return View(new GridModel(GetForecastOrderProduction(customerId, productCode, fromDate, toDate, all)));
                case (int)MyUtilities.Warehouse.Id.Production2:
                    return
                        View(new GridModel(GetForecastOrderProduction2(customerId, productCode, fromDate, toDate, all)));
                case (int)MyUtilities.Warehouse.Id.QcA:
                    return View(new GridModel(GetForecastOrderQc(customerId, productCode, fromDate, toDate, all)));
                default:
                    return View(new GridModel());

            }
        }

        #endregion

        #region quotation


        [GridAction]
        public ActionResult SelectMOQTemplate(bool isQuote = false) {
            var model = new List<MOQTemplateModel>();
            try{
                model = GetMOQTemplate(isQuote);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMOQTemplate", "" + ex.Message);
            }
            return View(new GridModel(model));
        }

        List<MOQTemplateModel> GetMOQTemplate(bool isQuote) {
            var model = new List<MOQTemplateModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.MOQTemplates.Where(x => isQuote == false || x.Active)
                    .Select(x => new MOQTemplateModel {
                        FromQuantity = x.FromQuantity,
                        ToQuantity = x.ToQuantity,
                        Active = x.Active,
                        ModifiedDate = x.ModifiedDate,
                        ModifiedUser = x.ModifiedUser,
                        TemplateId = x.TemplateId,
                        FactorDefault = x.FactorDefault
                    }).ToList();
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertMOQTemplate(MOQTemplateModel insert) {
            try {
                var template = new MOQTemplate {
                    FromQuantity = insert.FromQuantity,
                    ToQuantity = insert.ToQuantity,
                    FactorDefault = insert.FactorDefault,
                    Active = true,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name,
                };
                using (var vfi = new tammaContext()) {
                    vfi.MOQTemplates.Add(template);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMOQTemplate", "" + ex.Message);
            }
            return View(new GridModel(GetMOQTemplate(false)));
        }


        [GridAction]
        public ActionResult UpdateMOQTemplate(MOQTemplateModel update) {
            try {
                using (var vfi = new tammaContext()) {
                    var template = vfi.MOQTemplates.FirstOrDefault(x => x.TemplateId == update.TemplateId);
                    template.FromQuantity = update.FromQuantity;
                    template.ToQuantity = update.ToQuantity;
                    template.FactorDefault = update.FactorDefault;
                    template.Active = update.Active;
                    template.ModifiedDate = DateTime.Now;
                    template.ModifiedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMOQTemplate", "" + ex.Message);
            }
            return View(new GridModel(GetMOQTemplate(false)));
        }

        [GridAction]
        public ActionResult SelectQuotationProducts(int customerId, int quoteId, int exchangeRate) {
            var model = new List<ProductQuotationModel>();
            if (customerId == 0 && quoteId == 0)
                return View(new GridModel(model));
            try {
                using (var vfi = new tammaContext()) {
                    var millProcessing =
                        vfi.ProcessingTypes.FirstOrDefault(pt => pt.TypeId == 6).ProcessingSaleFactor.Value;
                    if (customerId != 0) {
                        var products = from p in vfi.Products
                                       where
                                           //p.Status >= (byte)MyUtilities.Product.ProductStatusEnum.Quoting && 
                                           p.Active &&
                                           p.CustomerId == customerId
                                       select new {
                                           p.ProductId,
                                           p.ProductCode,
                                           p.ProductName,
                                           p.DesignNo,
                                           p.MaterialNameDesign,
                                           p.Diameter,
                                           Productivity = p.Productivity ?? 0,
                                           MillProductivity = p.MillProductivity ?? 0,
                                           Diff = p.Diff ?? 0,
                                           MaterialCost = p.MaterialCost ?? 0,
                                           p.ProcessingDesign,
                                           p.Customer,
                                           OutDiameterDesign = p.OutDiameterDesign ?? 0,
                                           InDiameterDesign = p.InDiameterDesign ?? 0,
                                           p.ShapeDesign,
                                           Length = p.Length ?? 0,
                                           KnifeCut = p.KnifeCut ?? 0,
                                           p.ProductionSections,
                                           p.ProcessingType,
                                           p.ProductionPlatings
                                       };
                        //var productIds = products.Select(p => p.ProductId);
                        foreach (var product in products) {
                            var entity = new ProductQuotationModel {
                                CustomerCodeName =
                                    product.Customer.CustomerCode + " - " + product.Customer.CompanyName,
                                ProductId = product.ProductId,
                                ProductCustomerCode = product.DesignNo,
                                ProductName = product.ProductName,
                                MaterialDesign = product.MaterialNameDesign,
                                Dimension = product.Diameter + "x" + product.Length,
                                LastDiff = product.Diff,
                                MaterialPrice = product.MaterialCost,
                                ProcessingPrice =
                                    (product.MillProductivity * millProcessing),
                                Quantity = 0
                            };
                            //entity.ProcessingPrice += product.ProductionSections.Sum(ps => ps.SectionCost ?? 0);
                            entity.ProcessingPrice += product.ProductionPlatings.Sum(ps => ps.PlatingCost);
                            entity.ProcessingPrice += ((product.Productivity) *
                                                       (product.ProcessingType.ProcessingSaleFactor ?? 0));
                            entity.ProcessingPrice = entity.ProcessingPrice / exchangeRate;

                            entity.ProductWeight =
                                MyUtilities.Product.GetProductWeight(product.MaterialNameDesign,
                                                                     product.OutDiameterDesign, product.InDiameterDesign,
                                                                     product.Length, product.KnifeCut,
                                                                     product.ShapeDesign);
                            entity.MaterialUnitPrice = entity.ProductWeight / 1000 * entity.MaterialPrice;
                            entity.QuotationCost = (entity.ProcessingPrice + entity.LastDiff + entity.MaterialUnitPrice);
                            model.Add(entity);
                        }
                    }
                    else {
                        var quotation = vfi.QuoteForms.FirstOrDefault(q => q.QuoteId == quoteId);
                        if (quotation == null)
                            throw new AggregateException("");
                        var quoteDetails = quotation.QuoteDetails;
                        foreach (var detail in quoteDetails) {
                            var entity = new ProductQuotationModel {
                                ProductId = detail.ProductId,
                                ProductCustomerCode = detail.Product.DesignNo,
                                ProductName = detail.Product.ProductName,
                                MaterialDesign = detail.Product.MaterialNameDesign,
                                Dimension = detail.Product.Diameter + "x" + detail.Product.Length,
                                LastDiff = detail.Diff,
                                MaterialPrice = detail.MaterialPrice,
                                ProcessingPrice = (detail.Product.MillProductivity ?? 0) * millProcessing,
                                QuoteDetailId = detail.DetailId,
                                Quantity = detail.Quantity
                            };
                            //entity.ProcessingPrice += detail.Product.ProductionSections.Sum(ps => ps.SectionCost ?? 0);
                            entity.ProcessingPrice += detail.Product.ProductionPlatings.Sum(ps => ps.PlatingCost);
                            entity.ProcessingPrice += ((detail.Product.Productivity ?? 0) *
                                                       (detail.Product.ProcessingType.ProcessingSaleFactor ?? 0));
                            entity.ProcessingPrice = entity.ProcessingPrice / exchangeRate;

                            entity.ProductWeight =
                                MyUtilities.Product.GetProductWeight(detail.Product.MaterialNameDesign,
                                                                     detail.Product.OutDiameterDesign ?? 0,
                                                                     detail.Product.InDiameterDesign ?? 0,
                                                                     detail.Product.Length ?? 0,
                                                                     detail.Product.KnifeCut ?? 0,
                                                                     detail.Product.ShapeDesign);
                            entity.MaterialUnitPrice = entity.ProductWeight / 1000 * entity.MaterialPrice;
                            entity.QuotationCost = (entity.ProcessingPrice + entity.LastDiff + entity.MaterialUnitPrice);
                            model.Add(entity);
                        }

                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectQuotationProducts", "" + ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateQuotationProductDetail(
            [Bind(Prefix = "inserted")] IEnumerable<ProductQuotationModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<ProductQuotationModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<ProductQuotationModel> deletedDetails,
            int customerId, int quoteId, string productIds, int salesPersonId,
            string quoteDate, string endDate, string currencyCode, double exchangeRate,
            int deliveryTerm, string portName, int paymentMethod, int deliveryPeriod, int paymentCondition, string note) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ProductQuotationModel>()));
            }
            if (customerId == 0) {
                ModelState.AddModelError("SelectQuotationProductDetail", "Please pick Customer up!");
            }
            var insertList = new List<ProductQuotationModel>();
            if (insertedDetails != null && insertedDetails.Any()) insertList.AddRange(insertedDetails);
            if (updatedDetails != null && updatedDetails.Any()) insertList.AddRange(updatedDetails);
            using (var vfi = new tammaContext()) {
                try {
                    if (string.IsNullOrWhiteSpace(productIds))
                        throw new AggregateException("Vui lòng chọn sản phẩm để báo giá!");

                    var customer = vfi.Customers.FirstOrDefault(f => f.CustomerId == customerId && f.State == (byte)MyUtilities.Sales.CustomerState.Active);
                    if (customer == null)
                        throw new AggregateException("Customer is not existed");
                    // bao gia moi
                    if (quoteId == 0) {
                        var ci = new CultureInfo("vi-VN");
                        var qDate = string.IsNullOrWhiteSpace(quoteDate)
                                        ? DateTime.Now
                                        : Convert.ToDateTime(quoteDate, ci);
                        var eDate = string.IsNullOrWhiteSpace(endDate)
                                        ? DateTime.Now
                                        : Convert.ToDateTime(endDate, ci);
                        var quotation = new QuoteForm {
                            CustomerId = customerId,
                            QuoteNumber =
                                MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Quote,
                                                                  1),
                            QuoteDate = qDate,
                            OutOfDate = eDate,
                            QuoteCount = 1,
                            QuoteDetails = new List<QuoteDetail>(),
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            DeliveryTerm = deliveryTerm,
                            PaymentMethodId = paymentMethod,
                            DeliveryPeriodId = deliveryPeriod,
                            PaymentCondition = paymentCondition,
                            Note = note + "",
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            CurrencyCode = currencyCode,
                            ExchangeRate = exchangeRate,
                            PortName = portName,
                            SalesPersonId = salesPersonId,
                        };
                        int[] checkedRecords;
                        var lst = productIds.Split(':');
                        checkedRecords = new int[lst.Count()];
                        for (var i = 0; i < lst.Count(); i++) {
                            checkedRecords[i] = Convert.ToInt32(lst.ElementAt(i));
                        }
                        foreach (var productId in checkedRecords) {
                            var insert = insertList.FirstOrDefault(il => il.ProductId == productId);
                            var detail = new QuoteDetail {
                                QuoteForm = quotation,
                                QuoteId = quotation.QuoteId,
                                ProductId = productId,
                                ProcessingCost = 0,
                                ProductWeight = 0,
                                QuoteCost = 0,
                                MaterialDesign = "",
                                Dimension = "",
                                Note = "",
                                Quantity = 0
                            };
                            if (insert == null) {
                                var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                                detail.Diff = product.Diff ?? 0;
                                detail.MaterialPrice = product.MaterialCost ?? 0;
                            }
                            else {
                                detail.MaterialDesign += insert.MaterialDesign;
                                detail.Dimension += insert.Dimension;
                                detail.Diff = insert.LastDiff;
                                detail.MaterialPrice = insert.MaterialPrice;
                                detail.Quantity = insert.Quantity;
                                detail.Note = insert.Note + "";
                            }
                            quotation.QuoteDetails.Add(detail);
                        }
                        vfi.QuoteForms.Add(quotation);
                    }
                    // bao gia lai
                    else {
                        var quotation = vfi.QuoteForms.FirstOrDefault(q => q.QuoteId == quoteId);
                        if (quotation == null)
                            throw new AggregateException("Lỗi! Không tìm thấy form báo giá cũ");
                        quotation.Status = (byte)MyUtilities.Sales.Status.Waiting;
                        foreach (var insert in insertList) {
                            var detail = vfi.QuoteDetails.FirstOrDefault(qd => qd.DetailId == insert.QuoteDetailId);
                            if (detail == null) continue;
                            detail.Diff = insert.LastDiff;
                            detail.MaterialPrice = insert.MaterialPrice;
                            detail.Quantity = insert.Quantity;
                            detail.Note = insert.Note;
                        }
                    }
                    vfi.SaveChanges();
                }
                catch (Exception exception) {
                    ModelState.AddModelError("SelectQuotationProductDetail", "" + exception.Message);
                }
            }
            return View(new GridModel(new List<ProductQuotationModel>()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult CreateProductQuoteForm(
            int customerId, int quoteId, string productIds, int salesPersonId,
            string quoteDate, string endDate, string currencyCode, double exchangeRate,
            int deliveryTerm, string portName, int paymentMethod, int deliveryPeriod, int paymentCondition, string note) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreateProductQuoteForm",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<ProductQuotationModel>()));
            }
            if (customerId == 0) {
                ModelState.AddModelError("SelectQuotationProductDetail", "Please pick Customer up!");
            }
            var checkedrecords = MyUtilities.Function.StringToIds(productIds);
            var list = _productController.GetProductQuoteCalculate(0, "", 0, checkedrecords, false);
            try {
                var qDate = MyUtilities.Function.ParseDate(quoteDate);
                var eDate = MyUtilities.Function.ParseDate(endDate);
                var quotation = new QuoteForm {
                    CustomerId = customerId,
                    QuoteNumber =
                        MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Quote,
                                                          1),
                    QuoteDate = qDate,
                    OutOfDate = eDate,
                    QuoteCount = 1,
                    QuoteDetails = new List<QuoteDetail>(),
                    Status = (byte)MyUtilities.Transaction.Status.Open,
                    DeliveryTerm = deliveryTerm,
                    PaymentMethodId = paymentMethod,
                    DeliveryPeriodId = deliveryPeriod,
                    PaymentCondition = paymentCondition,
                    Note = note + "",
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name,
                    CurrencyCode = currencyCode,
                    ExchangeRate = exchangeRate,
                    PortName = portName,
                    SalesPersonId = salesPersonId,
                };
                foreach (var entity in list) {
                    var detail = new QuoteDetail {
                        QuoteForm = quotation,
                        QuoteId = quotation.QuoteId,
                        ProductId = entity.ProductId,
                        ProcessingCost = 0,
                        ProductWeight = entity.UnitWeight,
                        QuoteCost = entity.QuotePrice,
                        MaterialDesign = "",
                        Dimension = "",
                        Note = "",
                        Quantity = 0,
                        MaterialPrice = entity.MaterialPrice,
                    };
                    quotation.QuoteDetails.Add(detail);
                }
                using (var vfi = new tammaContext()) {
                    vfi.QuoteForms.Add(quotation);
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("CreateProductQuoteForm", "" + exception.Message);
            }

            return View(new GridModel(new List<ProductQuotationModel>()));
        }

        [GridAction]
        public ActionResult SelectQuoteApprove() {
            return
                View(
                    new GridModel(GetQuoteList((int)MyUtilities.Sales.Status.Waiting, "", "")
                                      .OrderByDescending(o => o.QuoteDate)));
        }

        [GridAction]
        public ActionResult SelectQuoteByStatus(int status, string fromDate, string toDate) {
            return View(new GridModel(GetQuoteList(status, fromDate, toDate).OrderByDescending(o => o.QuoteDate)));
        }

        private List<QuoteFormModel> GetQuoteList(int status, string fromDate, string toDate) {
            var models = new List<QuoteFormModel>();

            var ci = new CultureInfo("vi-VN");
            //var fDate = string.IsNullOrWhiteSpace(fromDate)
            //                ? DateTime.Now
            //                : Convert.ToDateTime(fromDate, ci);
            //var tDate = string.IsNullOrWhiteSpace(toDate)
            //                ? null
            //                : Convert.ToDateTime(toDate, ci);
            using (var vfi = new tammaContext()) {
                var quotes = from q in vfi.QuoteForms
                             where (status == 0 ? q.Status != (byte)MyUtilities.Sales.Status.Cancel : q.Status == status)
                             select q;
                if (!string.IsNullOrWhiteSpace(fromDate) && !string.IsNullOrWhiteSpace(toDate)) {
                    var fDate = Convert.ToDateTime(fromDate, ci);
                    var tDate = Convert.ToDateTime(toDate, ci);
                    quotes = quotes.Where(q => q.QuoteDate >= fDate && q.QuoteDate <= tDate);
                }
                foreach (var quote in quotes) {
                    var entity = new QuoteFormModel {
                        CustomerId = quote.CustomerId,
                        CustomerCodeName = quote.Customer.CustomerCode + "-" + quote.Customer.CustomerName,
                        QuoteDate = quote.QuoteDate,
                        OutOfDate = quote.OutOfDate,
                        ModifiedDate = quote.ModifiedDate ?? DateTime.Now,
                        ModifiedUser = quote.ModifiedUser,
                        QuoteCount = quote.QuoteCount ?? 0,
                        QuoteNumber = quote.QuoteNumber,
                        PortName = quote.PortName,
                        CurrencyCode = quote.CurrencyCode,
                        QuoteId = quote.QuoteId,
                        ExchangeRate = quote.ExchangeRate ?? 1,
                        Note = quote.Note,
                        ProductCount = quote.QuoteDetails.Count
                    };
                    //entity.DeliveryTermName =
                    //    methods.FirstOrDefault(m => m.MethodId == quote.DeliveryTerm).MethodName;
                    //entity.PaymentMethodName =
                    //    methods.FirstOrDefault(m => m.MethodId == quote.PaymentMethodId).MethodName;
                    //entity.DeliveryPeriodName =
                    //    methods.FirstOrDefault(m => m.MethodId == quote.DeliveryPeriodId).MethodName;
                    //entity.PaymentConditionName =
                    //    methods.FirstOrDefault(m => m.MethodId == quote.PaymentCondition).MethodName;
                    models.Add(entity);
                }
                return models;
            }
        }

        [HttpPost]
        public ActionResult ChangeQuoteStatus(int quoteId, int status) {
            try {
                using (var vfi = new tammaContext()) {
                    var quote = vfi.QuoteForms.FirstOrDefault(q => q.QuoteId == quoteId);
                    if (quote == null)
                        throw new AggregateException("Lỗi! Không tìm thấy form báo giá!");
                    if (quote.Status == (byte)MyUtilities.Sales.Status.Waiting) {
                        quote.Status = (byte)status;
                        switch (status) {
                            case (byte)MyUtilities.Sales.Status.Completed:
                                var millProcessing =
                                    vfi.ProcessingTypes.FirstOrDefault(pt => pt.TypeId == 6).ProcessingSaleFactor.Value;
                                foreach (var detail in quote.QuoteDetails) {
                                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == detail.ProductId);
                                    if (product == null) continue;
                                    product.MaterialCost = detail.MaterialPrice;
                                    product.Diff = detail.Diff;
                                    //product.Status = (byte)MyUtilities.Product.ProductStatusEnum.Sampling;
                                    detail.ProcessingCost = (product.MillProductivity ?? 0) * millProcessing;
                                    //detail.ProcessingCost += detail.Product.ProductionSections.Sum(ps => ps.SectionCost ?? 0);
                                    detail.ProcessingCost += detail.Product.ProductionPlatings.Sum(ps => ps.PlatingCost);
                                    detail.ProcessingCost += ((detail.Product.Productivity ?? 0) *
                                                              (detail.Product.ProcessingType.ProcessingSaleFactor ?? 0));
                                    detail.ProcessingCost = detail.ProcessingCost / quote.ExchangeRate.Value;
                                    detail.ProductWeight =
                                        MyUtilities.Product.GetProductWeight(product.MaterialNameDesign,
                                                                             product.OutDiameterDesign ?? 0,
                                                                             product.InDiameterDesign ?? 0,
                                                                             product.Length ?? 0, product.KnifeCut ?? 0,
                                                                             product.ShapeDesign);
                                    detail.QuoteCost = detail.ProcessingCost + detail.Diff +
                                                       (detail.ProductWeight / 1000 * detail.MaterialPrice);
                                    product.UnitPrice = detail.QuoteCost;
                                }
                                break;
                            case (byte)MyUtilities.Sales.Status.InProcess:
                                quote.QuoteCount++;
                                break;
                        }
                        vfi.SaveChanges();
                        return Json("Thành công!");
                    }
                }
            }
            catch (Exception ex) {
                return Json("Lỗi! " + ex.Message);
            }
            return Json("");
        }

        [GridAction]
        public ActionResult UpdateQuodeDetailById(ProductQuotationModel update) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var quoteDetail = vfi.QuoteDetails.FirstOrDefault(qd => qd.DetailId == update.QuoteDetailId);
                    if (quoteDetail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết báo giá!");
                    quoteDetail.Diff = update.LastDiff;
                    quoteDetail.Quantity = update.Quantity;
                    quoteDetail.Note = update.Note;
                    vfi.SaveChanges();
                }
                return View(new GridModel(QuodeDetailById(update.QuoteId)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateQuodeDetailById", ex.Message);
            }
            return View(new GridModel(QuodeDetailById(update.QuoteId)));
        }

        [GridAction]
        public ActionResult SelectQuodeDetailById(int quoteId) {
            var model = new List<ProductQuotationModel>();
            try {
                model = QuodeDetailById(quoteId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectQuodeDetailById", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectQuodeInfoById(int quoteId) {
            var model = new List<ProductQuotationModel>();
            if (quoteId == 0)
                return View(new GridModel(model));
            try {

                using (var vfi = new tammaContext()) {
                    var quotation = vfi.QuoteForms.FirstOrDefault(q => q.QuoteId == quoteId);
                    if (quotation == null)
                        throw new AggregateException("Lỗi! Không tìm thấy form báo giá");
                    var entity = new ProductQuotationModel();
                    entity.DeliveryTermName =
                        vfi.Methods.FirstOrDefault(m => m.MethodId == quotation.DeliveryTerm).MethodName
                        + "-" + quotation.PortName;
                    entity.PaymentMethodName =
                        vfi.Methods.FirstOrDefault(m => m.MethodId == quotation.PaymentMethodId).MethodName;
                    entity.DeliveryPeriodName =
                        vfi.Methods.FirstOrDefault(m => m.MethodId == quotation.DeliveryPeriodId).MethodName;
                    entity.PaymentConditionName =
                        vfi.Methods.FirstOrDefault(m => m.MethodId == quotation.PaymentCondition).MethodName;
                    entity.SalesPerson =
                        vfi.Employees.FirstOrDefault(m => m.EmployeeId == quotation.SalesPersonId).EmployeeName;
                    model.Add(entity);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectQuodeDetailById", ex.Message);
            }
            return View(new GridModel(model));
        }

        public List<ProductQuotationModel> QuodeDetailById(int quoteId) {
            if (quoteId == 0)
                return new List<ProductQuotationModel>();
            var model = new List<ProductQuotationModel>();

            using (var vfi = new tammaContext()) {
                var quotation = vfi.QuoteForms.FirstOrDefault(q => q.QuoteId == quoteId);
                if (quotation == null)
                    throw new AggregateException("Lỗi! Không tìm thấy form báo giá");
                var millProcessing = vfi.ProcessingTypes.FirstOrDefault(pt => pt.TypeId == 6).ProcessingSaleFactor.Value;
                if (quotation.Status != (byte)MyUtilities.Sales.Status.Completed) {
                    foreach (var detail in quotation.QuoteDetails) {
                        var product = detail.Product;
                        var entity = new ProductQuotationModel {
                            QuoteDetailId = detail.DetailId,
                            ProductId = product.ProductId,
                            ProductCustomerCode = product.DesignNo,
                            ProductName = product.ProductName,
                            MaterialDesign = product.MaterialNameDesign,
                            Dimension = product.Diameter + "x" + product.Length,
                            LastDiff = detail.Diff,
                            MaterialPrice = detail.MaterialPrice,
                            ProcessingPrice = (product.MillProductivity ?? 0) * millProcessing,
                            Quantity = detail.Quantity,
                            QuoteId = quoteId
                        };
                        //entity.ProcessingPrice += detail.Product.ProductionSections.Sum(ps => ps.SectionCost ?? 0);
                        entity.ProcessingPrice += detail.Product.ProductionPlatings.Sum(ps => ps.PlatingCost);
                        entity.ProcessingPrice += ((detail.Product.Productivity ?? 0) *
                                                   (detail.Product.ProcessingType.ProcessingSaleFactor ?? 0));
                        entity.ProcessingPrice = entity.ProcessingPrice / quotation.ExchangeRate.Value;

                        entity.ProductWeight =
                            MyUtilities.Product.GetProductWeight(detail.Product.MaterialNameDesign,
                                                                 detail.Product.OutDiameterDesign ?? 0,
                                                                 detail.Product.InDiameterDesign ?? 0,
                                                                 detail.Product.Length ?? 0,
                                                                 detail.Product.KnifeCut ?? 0,
                                                                 detail.Product.ShapeDesign);
                        entity.MaterialUnitPrice = entity.ProductWeight / 1000 * entity.MaterialPrice;
                        entity.QuotationCost = (entity.ProcessingPrice + entity.LastDiff + entity.MaterialUnitPrice);
                        model.Add(entity);
                    }
                }
                else {
                    foreach (var detail in quotation.QuoteDetails) {
                        var entity = new ProductQuotationModel {
                            QuoteDetailId = detail.DetailId,
                            ProductId = detail.ProductId,
                            ProductCustomerCode = detail.Product.DesignNo,
                            ProductName = detail.Product.ProductName,
                            MaterialDesign = detail.MaterialDesign,
                            Dimension = detail.Dimension,
                            LastDiff = detail.Diff,
                            MaterialPrice = detail.MaterialPrice,
                            ProcessingPrice = detail.ProcessingCost,
                            Quantity = detail.Quantity,
                            QuotationCost = detail.QuoteCost,
                            ProductWeight = detail.ProductWeight,
                            MaterialUnitPrice = detail.MaterialPrice / detail.ProductWeight,
                        };
                        model.Add(entity);
                    }
                }
            }
            return model.OrderBy(m => m.ProductName).ToList();
        }

        [HttpPost]
        public ActionResult PrintQuotation(int quoteId) {
            if (quoteId == 0)
                return PartialView(null);
            var model = new List<ProductQuotationModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var quotation = vfi.QuoteForms.FirstOrDefault(q => q.QuoteId == quoteId);
                    if (quotation == null)
                        throw new AggregateException("Lỗi! Không tìm thấy form báo giá");
                    var millProcessing =
                        vfi.ProcessingTypes.FirstOrDefault(pt => pt.TypeId == 6).ProcessingSaleFactor.Value;
                    var customer = quotation.Customer;
                    foreach (var detail in quotation.QuoteDetails) {
                        var product = detail.Product;
                        var entity = new ProductQuotationModel {
                            QuoteDetailId = detail.DetailId,
                            ProductId = product.ProductId,
                            ProductCustomerCode = product.DesignNo,
                            ProductName = product.ProductName,
                            MaterialDesign = product.MaterialNameDesign,
                            Dimension = product.Diameter + "x" + product.Length,
                            ProcessingPrice = (product.MillProductivity ?? 0 * millProcessing),
                            CustomerAddress = customer.Address,
                            CustomerEmail = customer.Email,
                            CustomerFax = customer.Fax,
                            CustomerName = customer.CustomerName,
                            CustomerPhone = customer.Phone,
                            CompanyName = customer.CompanyName,
                            ContactName = customer.ContactName,
                            QuoteDate = quotation.QuoteDate,
                            EndDate = quotation.OutOfDate,
                            QuoteNumber = quotation.QuoteNumber,
                            QuoteRev = quotation.QuoteCount ?? 0,
                            CurrencyCode = quotation.CurrencyCode,
                            Quantity = detail.Quantity,
                            LastDiff = detail.Diff,
                            MaterialPrice = detail.MaterialPrice,
                            Note = detail.Note,
                        };
                        foreach (var plating in product.ProductionPlatings) {
                            entity.ProductPlating += plating.PlatingName + ";\n";
                        }
                        if (string.IsNullOrWhiteSpace(entity.ProductPlating))
                            entity.ProductPlating = "Without";
                        else
                            entity.ProductPlating = entity.ProductPlating.Substring(entity.ProductPlating.Length - 1);
                        //entity.ProcessingPrice += detail.Product.ProductionSections.Sum(ps => ps.SectionCost ?? 0);
                        entity.ProcessingPrice += detail.Product.ProductionPlatings.Sum(ps => ps.PlatingCost);
                        entity.ProcessingPrice += ((detail.Product.Productivity ?? 0) *
                                                   (detail.Product.ProcessingType.ProcessingSaleFactor ?? 0));
                        entity.ProcessingPrice = entity.ProcessingPrice / quotation.ExchangeRate.Value;

                        entity.ProductWeight =
                            MyUtilities.Product.GetProductWeight(detail.Product.MaterialNameDesign,
                                                                 detail.Product.OutDiameterDesign ?? 0,
                                                                 detail.Product.InDiameterDesign ?? 0,
                                                                 detail.Product.Length ?? 0,
                                                                 detail.Product.KnifeCut ?? 0,
                                                                 detail.Product.ShapeDesign);
                        entity.MaterialUnitPrice = entity.ProductWeight / 1000 * entity.MaterialPrice;
                        entity.QuotationCost = (entity.ProcessingPrice + entity.LastDiff + entity.MaterialUnitPrice);
                        entity.DeliveryTermName =
                            vfi.Methods.FirstOrDefault(m => m.MethodId == quotation.DeliveryTerm).MethodName_EN
                            + "-" + quotation.PortName;
                        entity.PaymentMethodName =
                            vfi.Methods.FirstOrDefault(m => m.MethodId == quotation.PaymentMethodId).MethodName_EN;
                        entity.DeliveryPeriodName =
                            vfi.Methods.FirstOrDefault(m => m.MethodId == quotation.DeliveryPeriodId).MethodName_EN;
                        entity.PaymentConditionName =
                            vfi.Methods.FirstOrDefault(m => m.MethodId == quotation.PaymentCondition).MethodName_EN;
                        entity.CustomerCodeName =
                            vfi.Customers.FirstOrDefault(m => m.CustomerId == quotation.CustomerId).CustomerCode;
                        entity.SalesPerson =
                            vfi.Employees.FirstOrDefault(m => m.EmployeeId == quotation.SalesPersonId).EmployeeName;
                        entity.QuotationCost = entity.QuotationCost;
                        model.Add(entity);
                    }
                    return PartialView("PageQuotation", model);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintImportFuel", "\n" + ex.Message);
            }
            return PartialView(null);
        }

        public ActionResult SplitQuoteDetails(int quoteId, int[] detailIds) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var quotation = vfi.QuoteForms.FirstOrDefault(q => q.QuoteId == quoteId);
                    if (quotation == null)
                        return Json(9);
                    if (quotation.QuoteDetails.Count == detailIds.Length) return Json(8);

                    var newQuotation = new QuoteForm {
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        Status = (byte)MyUtilities.Sales.Status.Waiting,
                        ExchangeRate = quotation.ExchangeRate,
                        OutOfDate = quotation.OutOfDate,
                        PaymentMethodId = quotation.PaymentMethodId,
                        DeliveryTerm = quotation.DeliveryTerm,
                        PaymentCondition = quotation.PaymentCondition,
                        DeliveryPeriodId = quotation.DeliveryPeriodId,
                        CustomerId = quotation.CustomerId,
                        QuoteCount = quotation.QuoteCount,
                        QuoteNumber = quotation.QuoteNumber + "B",
                        QuoteDate = quotation.QuoteDate,
                        PortName = quotation.PortName,
                        CurrencyCode = quotation.CurrencyCode,
                        Note = quotation.Note + "! Đã được tách",
                    };
                    quotation.QuoteNumber += "A";
                    vfi.QuoteForms.Add(newQuotation);
                    vfi.SaveChanges();
                    var quotationDetails =
                        quotation.QuoteDetails.Where(detail => detailIds.Contains(detail.DetailId)).ToList();
                    foreach (var detail in quotationDetails) {
                        detail.QuoteId = newQuotation.QuoteId;
                    }
                    vfi.SaveChanges();
                    return Json(1);
                }
            }
            catch (Exception) {
                return Json(0);
            }
        }

        public ActionResult DuplicateQuoteDetails(int quoteId, int[] detailIds) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n 
                                         1 trong các nguyên nhân như mất thời gian chờ. \r\n 
                                         Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var quotation = vfi.QuoteForms.FirstOrDefault(q => q.QuoteId == quoteId);
                    if (quotation == null)
                        return Json(9);
                    //if (quotation.QuoteDetails.Count == detailIds.Length) return Json(8);
                    foreach (var detailId in detailIds) {
                        var quoteDetail = quotation.QuoteDetails.FirstOrDefault(qd => qd.DetailId == detailId);
                        if (quoteDetail == null) continue;
                        var newDetail = new QuoteDetail {
                            Note = quoteDetail.Note,
                            MaterialDesign = quoteDetail.MaterialDesign,
                            Diff = quoteDetail.Diff,
                            Dimension = quoteDetail.Dimension,
                            ProcessingCost = quoteDetail.ProcessingCost,
                            ProductId = quoteDetail.ProductId,
                            ProductWeight = quoteDetail.ProductWeight,
                            Quantity = quoteDetail.Quantity,
                            QuoteCost = quoteDetail.QuoteCost,
                            QuoteId = quoteDetail.QuoteId,
                            MaterialPrice = quoteDetail.MaterialPrice,
                            QuoteForm = quoteDetail.QuoteForm,
                        };
                        quotation.QuoteDetails.Add(newDetail);
                    }
                    vfi.SaveChanges();
                    return Json(1);
                }
            }
            catch (Exception) {
                return Json(0);
            }
        }

        public ActionResult SelectReQuotation() {
            using (var vfi = new tammaContext()) {
                var quotations =
                    vfi.QuoteForms.Where(q => q.Status == (byte)MyUtilities.Sales.Status.InProcess).ToList();
                return new JsonResult {
                    Data = new SelectList(quotations, "QuoteId", "QuoteNumber")
                };
            }
        }

        public ActionResult SelectReQuotationInfo(int quoteId) {
            using (var vfi = new tammaContext()) {
                var quotation =
                    vfi.QuoteForms.FirstOrDefault(
                        q => q.Status == (byte)MyUtilities.Sales.Status.InProcess && q.QuoteId == quoteId);
                if (quotation == null)
                    return Json(9);
                var quotationModel = new QuoteFormModel {
                    DeliveryTermId = quotation.DeliveryTerm ?? 0,
                    DeliveryPeriod = quotation.DeliveryPeriodId,
                    ExchangeRate = quotation.ExchangeRate ?? 1,
                    PaymentMethodId = quotation.PaymentMethodId,
                    CustomerId = quotation.CustomerId,
                    OutOfDate = quotation.OutOfDate,
                    QuoteDate = quotation.QuoteDate,
                    PortName = quotation.PortName,
                    Note = quotation.Note,
                    CurrencyCode = quotation.CurrencyCode,
                    PaymentCondition = quotation.PaymentCondition ?? 1,
                };
                quotationModel.DeliveryTermName =
                    vfi.Methods.FirstOrDefault(m => m.MethodId == quotationModel.DeliveryTermId).MethodName;
                quotationModel.PaymentMethodName =
                    vfi.Methods.FirstOrDefault(m => m.MethodId == quotationModel.PaymentMethodId).MethodName;
                quotationModel.DeliveryPeriodName =
                    vfi.Methods.FirstOrDefault(m => m.MethodId == quotationModel.DeliveryPeriod).MethodName;
                quotationModel.PaymentConditionName =
                    vfi.Methods.FirstOrDefault(m => m.MethodId == quotationModel.PaymentCondition).MethodName;
                quotationModel.CustomerCodeName =
                    vfi.Customers.FirstOrDefault(m => m.CustomerId == quotationModel.CustomerId).CustomerCode;
                quotationModel.FilterProduct = quotation.QuoteDetails.FirstOrDefault().Product.CustomerId;
                quotationModel.FilterProductCode =
                    vfi.Customers.FirstOrDefault(m => m.CustomerId == quotationModel.FilterProduct).CustomerCode;
                return Json(quotationModel);
            }
        }

        #endregion
    }
}



