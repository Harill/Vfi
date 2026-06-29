using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Utilities;
using PurchaseOrder = Vfi.Ui.Mvc.Vfi.Models.PurchaseOrder;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Controllers {
    public class PurchaseOrderController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        [InjectionConstructor]
        public PurchaseOrderController(IUnitOfWork unitOfWork) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        #region View
        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }
            return ViewData;
        }
        public ActionResult CreateMaterialPurchaseOrder() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult CreateToolPurchaseOrder() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult CreateFuelPurchaseOrder() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult UpdatePurchaseOrderStatus() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ApprovePurchaseOrder() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult PurchaseOrderManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ChangePurchaseOrderStatus() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult CancelQualityPuchaseOrder() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult CreatePlatingForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ApprovePlatingForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult PlatingManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ChangePlatingFormStatus() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult PurchaseTransactionProduct() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ImportPOManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult PurchasingProgress() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }


        public ActionResult AddPoTaxInvoiceReference() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult CreatePoTaxInvoice() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult PoTaxInvoicesManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ApprovePoTaxInvoice() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ApprovePoTaxInvoiceMoney() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult AddPoTaxInvoiceMoney() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult VendorInDept() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult InquiryPoCreate() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult InquiryPoApprove() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult InquiryPoManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult CreateEvaluationForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ApprovedEvaluationForm() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult EvaluationFormManager() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        #endregion
        // GetData

        #region EvaluationForm

        [GridAction]
        public ActionResult SelectEvaluationFormBegin(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<EvaluationFormModel>();
            try {
                if (vendorId != null && materialClassifiedId != null) {
                    model = GetEvaluationForm(vendorId, fromDate, toDate, materialClassifiedId);
                }
                else {
                    throw new AggregateException("Hãy nhập đầy đủ thông tin Loại và NCC!");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectEvaluationFormBegin", ex.Message);
            }
            return View(new GridModel(model));

        }

        List<EvaluationFormModel> GetEvaluationForm(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<EvaluationFormModel>();
            using (var vfi = new tammaContext()) {
                try {
                    if (vendorId != 0 && materialClassifiedId != 0) {
                        var fDate = MyUtilities.Function.ParseDate(fromDate);
                        var tDate = MyUtilities.Function.ParseDate(toDate);
                        if (fDate >= DateTime.Today) {
                            throw new Exception("'Từ ngày' không thể lớn hơn hoặc bằng ngày hiện tại");
                        }
                        if (tDate > DateTime.Today) {
                            tDate = DateTime.Today;
                        }


                        var entity = new EvaluationFormModel {
                            FromDate = fDate,
                            ToDate = tDate,
                            VendorId = vendorId ?? 0,
                            
                        };
                        var poDetailList = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrder.ShipDate >= fDate
                                                                          && t.PurchaseOrder.ShipDate <= tDate
                                                                          && t.PurchaseOrder.Status != (byte)MyUtilities.Sales.Status.Cancel
                                                                          && t.PurchaseOrder.VendorId == entity.VendorId
                                                                          && t.MaterialClassifiedId == materialClassifiedId)
                                                                   .Select(t => new {
                                                                       t.PurchaseOrderDetailId,
                                                                       t.OrderQty,
                                                                       t.ReceivedQty,
                                                                       OrderDate = t.PurchaseOrder.ShipDate,
                                                                   }).ToList();
                        if (poDetailList.Count == 0) {
                            throw new AggregateException("NCC không có lịch sử mua hàng trong mốc thời gian đã chọn.");
                        }
                        int lateDeliveryTimes = 0;
                        int deliveryLessThenOrder = 0;
                        int NGTimes = 0;
                        int totalNGQuantity = 0;
                        int totalRecied = 0;

                        switch (materialClassifiedId) {
                            case 1:
                                foreach (var poDetail in poDetailList) {
                                    if (poDetail.ReceivedQty == 0) {
                                        lateDeliveryTimes++;
                                        deliveryLessThenOrder++;
                                    }
                                    else {
                                        var importInfo = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber))
                                                                                       .Select(t => new {
                                                                                           t.NG,
                                                                                           t.ImportPurchaseOrder.ImportDate,
                                                                                           t.Quantity,
                                                                                           t.QuantityKg,
                                                                                           t.UnitWeight,
                                                                                       }).FirstOrDefault();

                                        // 1. Giao hàng trễ
                                        if (importInfo.ImportDate > poDetail.OrderDate) {
                                            lateDeliveryTimes++;
                                        }

                                        // 2. Giao hàng thiếu
                                        if (importInfo.QuantityKg == 0) {
                                            var importKgs = (importInfo.Quantity * importInfo.UnitWeight);
                                            if ((poDetail.OrderQty * 0.9) > importKgs)
                                                deliveryLessThenOrder++;
                                        }
                                        else if ((poDetail.OrderQty * 0.9) > importInfo.QuantityKg) {
                                            deliveryLessThenOrder++;
                                        }
                                        // 3,4. NG
                                        if (importInfo.NG == true) {
                                            NGTimes++;
                                            totalNGQuantity += (int)poDetail.ReceivedQty;
                                        }
                                        //totalRecied += (int)poDetail.ReceivedQty;
                                        totalRecied += (int)poDetail.OrderQty;
                                    }
                                }
                                break;

                            case 2:
                                foreach (var poDetail in poDetailList) {
                                    if (poDetail.ReceivedQty == 0) {
                                        lateDeliveryTimes++;
                                        deliveryLessThenOrder++;
                                    }
                                    else {
                                        var importInfo = vfi.TransactionFptDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                                                         && !string.IsNullOrEmpty(t.LotNumber))
                                                                                  .Select(t => new {
                                                                                      t.NG,
                                                                                      ImportDate = t.TransactionFpt.TransactionDate,
                                                                                      t.Quantity,
                                                                                  }).FirstOrDefault();
                                        // 1. Giao hàng trễ
                                        if (importInfo.ImportDate > poDetail.OrderDate) {
                                            lateDeliveryTimes++;
                                            if ((poDetail.OrderQty) > poDetail.ReceivedQty) {
                                                deliveryLessThenOrder++;
                                            }
                                        }
                                        // 2. Giao hàng thiếu
                                        if ((poDetail.OrderQty) > importInfo.Quantity)
                                            deliveryLessThenOrder++;

                                        // 3,4. NG
                                        if (importInfo.NG == true) {
                                            NGTimes++;
                                            totalNGQuantity += (int)poDetail.ReceivedQty;
                                        }
                                        //totalRecied += (int)poDetail.ReceivedQty;
                                        totalRecied += (int)poDetail.OrderQty;
                                    }
                                }
                                break;


                            case 3:
                                foreach (var poDetail in poDetailList) {
                                    if (poDetail.ReceivedQty == 0) {
                                        lateDeliveryTimes++;
                                        deliveryLessThenOrder++;
                                    }
                                    else {
                                        var importInfo = vfi.TransactionFptDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                                                         && !string.IsNullOrEmpty(t.LotNumber))
                                                                                  .Select(t => new {
                                                                                      t.NG,
                                                                                      ImportDate = t.TransactionFpt.TransactionDate,
                                                                                      t.Quantity,
                                                                                  }).FirstOrDefault();
                                        // 1. Giao hàng trễ
                                        if (importInfo.ImportDate > poDetail.OrderDate) {
                                            lateDeliveryTimes++;
                                            if ((poDetail.OrderQty) > poDetail.ReceivedQty) {
                                                deliveryLessThenOrder++;
                                            }
                                        }

                                        // 2. Giao hàng thiếu
                                        if ((poDetail.OrderQty) > importInfo.Quantity)
                                            deliveryLessThenOrder++;

                                        // 3,4. NG
                                        if (importInfo.NG == true) {
                                            NGTimes++;
                                            totalNGQuantity += (int)poDetail.ReceivedQty;
                                        }
                                        //totalRecied += (int)poDetail.ReceivedQty;
                                        totalRecied += (int)poDetail.OrderQty;
                                    }
                                }
                                break;

                        }
                        var zeroPointCount = 0;
                        // 1. Giao hàng trễ
                        if (lateDeliveryTimes > 5) {
                            entity.LogisticsPoint = 0;
                            zeroPointCount++;
                        }
                        else if (lateDeliveryTimes >= 3 && lateDeliveryTimes <= 5) {
                            entity.LogisticsPoint = 10;
                        }
                        else if (lateDeliveryTimes == 1 || lateDeliveryTimes == 2) {
                            entity.LogisticsPoint = 15;
                        }
                        else if (lateDeliveryTimes == 0) {
                            entity.LogisticsPoint = 20;
                        };


                        // 2. Giao hàng thiếu
                        if (deliveryLessThenOrder > 5) {
                            entity.QuantityDeliveryPoint = 0;
                            zeroPointCount++;
                        }
                        else if (deliveryLessThenOrder >= 3 && deliveryLessThenOrder <= 5) {
                            entity.QuantityDeliveryPoint = 10;
                        }
                        else if (deliveryLessThenOrder == 1 || deliveryLessThenOrder == 2) {
                            entity.QuantityDeliveryPoint = 15;
                        }
                        else if (deliveryLessThenOrder == 0) {
                            entity.QuantityDeliveryPoint = 20;
                        };

                        // 3. So lan bi NG
                        if (NGTimes > 4) {
                            entity.NGTimePoint = 0;
                            zeroPointCount++;
                        }
                        else if (NGTimes == 3 || NGTimes == 4) {
                            entity.NGTimePoint = 10;
                        }
                        else if (NGTimes == 1 || NGTimes == 2) {
                            entity.NGTimePoint = 15;
                        }
                        else if (NGTimes == 0) {
                            entity.NGTimePoint = 20;
                        };

                        // 4. So luong. NG
                        double percentNG = (totalNGQuantity * 100) / totalRecied;
                        if (totalNGQuantity == 0 || percentNG <= 2) {
                            entity.NGNumberPoint = 20;
                        }
                        else if (percentNG > 2 && percentNG <= 3) {
                            entity.NGNumberPoint = 15;
                        }
                        else if (percentNG > 3 && percentNG <= 4) {
                            entity.NGNumberPoint = 10;
                        }
                        else if (percentNG > 4) {
                            entity.NGNumberPoint = 0;
                            zeroPointCount++;
                        }


                        model.Add(entity);
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("GetEvaluationForm", ex.Message);
                }
            }
            return model;
        }






        [GridAction]
        public ActionResult SelectEvaluationForm() {
            return View(new GridModel(new List<EvaluationFormModel>()));
        }


        [GridAction]
        public ActionResult CreateNewEvaluationForm(
            [Bind(Prefix = "inserted")] IEnumerable<EvaluationFormModel> insertedForms,
            [Bind(Prefix = "updated")] IEnumerable<EvaluationFormModel> updatedForms,
            [Bind(Prefix = "deleted")] IEnumerable<EvaluationFormModel> deletedForms,
            int vendorId, string fromDate, string toDate, int materialClassified, string note) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreateNewInquiryPo",
                    @"Bạn đã bị mất quyền đăng nhập. 
              \r\n 1 trong các nguyên nhân như mất thời gian chờ. 
              \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<EvaluationFormModel>()));
            }

            try {
                var fDate = MyUtilities.Function.ParseDate(fromDate);
                var tDate = MyUtilities.Function.ParseDate(toDate);
                if (insertedForms.Count() >= 2) {
                    throw new AggregateException("Tạo mỗi lần tối đa 1 phiếu đánh giá! ");
                }
                if (insertedForms != null) {
                    using (var vfi = new tammaContext()) {
                        foreach (var inserted in insertedForms) {
                            if (fDate == null || fDate >= DateTime.Now)
                                throw new AggregateException("Lỗi! Từ ngày không thể lớn hơn hoặc bằng ngày hiện tại");

                            if (tDate == null || tDate > DateTime.Now)
                                throw new AggregateException("Lỗi!Đến ngày không thể lớn hơn ngày hiện tại");
                            if (vendorId == 0 || vendorId == null) {
                                throw new AggregateException("Lỗi! Chưa nhập tên Nhà cung cấp.");
                            }

                            var entity = new EvaluationForm {
                                VendorId = vendorId,
                                MaterialClassifiedId = materialClassified,
                                FromDate = fDate,
                                ToDate = tDate,
                                Status = (byte)MyUtilities.PurchaseOrder.EvaluationEnum.Pending,
                                Note = note,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                            };

                            switch (inserted.TechSpPoint) {
                                case 0:
                                case 5:
                                case 10:
                                    entity.TechSpPoint = inserted.TechSpPoint;
                                    break;
                                default:
                                    throw new AggregateException("Lỗi! Hãy nhập 'Technical SP' trong thang điểm (0 - 5 - 10) ");
                            }

                            switch (inserted.PricePoint) {
                                case 0:
                                case 5:
                                case 10:
                                    entity.PricePoint = inserted.PricePoint;
                                    break;
                                default:
                                    throw new AggregateException("Lỗi! Hãy nhập 'Competitivity' trong thang điểm (0 - 5 - 10) ");
                            }

                            var poDetailList = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrder.ShipDate >= fDate
                                                                              && t.PurchaseOrder.ShipDate <= tDate 
                                                                              && t.PurchaseOrder.Status != (byte)MyUtilities.Sales.Status.Cancel
                                                                              && t.PurchaseOrder.VendorId == entity.VendorId
                                                                              && t.MaterialClassifiedId == materialClassified)
                                                                       .Select(t => new {
                                                                            t.PurchaseOrderDetailId,
                                                                            t.OrderQty,
                                                                            t.ReceivedQty,
                                                                            OrderDate = t.PurchaseOrder.ShipDate,
                                                                       }).ToList();
                            if (poDetailList.Count == 0) {
                                throw new AggregateException("NCC không có lịch sử mua hàng trong mốc thời gian đã chọn.");
                            }

                            int lateDeliveryTimes = 0;
                            int deliveryLessThenOrder = 0;
                            int NGTimes = 0;
                            int totalNGQuantity = 0;
                            int totalRecied = 0;

                            switch (materialClassified) {
                                case 1:
                                    foreach (var poDetail in poDetailList) {
                                        if (poDetail.ReceivedQty == 0) {
                                            lateDeliveryTimes++;
                                            deliveryLessThenOrder++;
                                        }
                                        else {
                                            var importInfo = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                                                                  && !string.IsNullOrEmpty(t.LotNumber))
                                                                                           .Select(t => new {
                                                                                               t.NG,
                                                                                               t.ImportPurchaseOrder.ImportDate,
                                                                                               t.Quantity,
                                                                                               t.QuantityKg,
                                                                                               t.UnitWeight,
                                                                                           }).FirstOrDefault();

                                            // 1. Giao hàng trễ
                                            if (importInfo.ImportDate > poDetail.OrderDate) {
                                                lateDeliveryTimes++;
                                            }
                                            // 2. Giao hàng thiếu
                                            if (importInfo.QuantityKg != 0) {
                                                if ((poDetail.OrderQty * 0.9) > importInfo.QuantityKg)
                                                    deliveryLessThenOrder++;
                                            }
                                            else if (importInfo.QuantityKg == 0) {
                                                var importKg = importInfo.Quantity * importInfo.UnitWeight;
                                                if ((poDetail.OrderQty * 0.9) > importKg) {
                                                    deliveryLessThenOrder++;
                                                }
                                            }

                                            // 3,4. NG
                                            if (importInfo.NG == true) {
                                                NGTimes++;
                                                totalNGQuantity += (int)poDetail.OrderQty;
                                            }
                                            //totalRecied += (int)poDetail.ReceivedQty;
                                            totalRecied += (int)poDetail.OrderQty;
                                        }
                                    }
                                    break;

                                case 2:
                                    foreach (var poDetail in poDetailList) {
                                        if (poDetail.ReceivedQty == 0) {
                                            lateDeliveryTimes++;
                                            deliveryLessThenOrder++;
                                        }
                                        else {
                                            var importInfo = vfi.TransactionFptDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                                                             && !string.IsNullOrEmpty(t.LotNumber))
                                                                                      .Select(t => new {
                                                                                          t.NG,
                                                                                          ImportDate = t.TransactionFpt.TransactionDate,
                                                                                          t.Quantity,
                                                                                      }).FirstOrDefault();
                                            // 1. Giao hàng trễ
                                            if (importInfo.ImportDate > poDetail.OrderDate) {
                                                lateDeliveryTimes++;
                                                if (poDetail.OrderQty > poDetail.ReceivedQty) {
                                                    deliveryLessThenOrder++;
                                                }
                                            }

                                            // 2. Giao hàng thiếu
                                            if ((poDetail.OrderQty) > importInfo.Quantity)
                                                deliveryLessThenOrder++;

                                            // 3,4. NG
                                            if (importInfo.NG == true) {
                                                NGTimes++;
                                                totalNGQuantity += (int)poDetail.OrderQty;
                                            }
                                            //totalRecied += (int)poDetail.ReceivedQty;
                                            totalRecied += (int)poDetail.OrderQty;
                                        }
                                    }
                                    break;


                                case 3:
                                    foreach (var poDetail in poDetailList) {
                                        if (poDetail.ReceivedQty == 0) {
                                            lateDeliveryTimes++;
                                            deliveryLessThenOrder++;
                                        }
                                        else {
                                            var importInfo = vfi.TransactionFptDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                                                             && !string.IsNullOrEmpty(t.LotNumber))
                                                                                      .Select(t => new {
                                                                                          t.NG,
                                                                                          ImportDate = t.TransactionFpt.TransactionDate,
                                                                                          t.Quantity,
                                                                                      }).FirstOrDefault();
                                            // 1. Giao hàng trễ
                                            if (importInfo.ImportDate > poDetail.OrderDate) {
                                                lateDeliveryTimes++;
                                                if (poDetail.OrderQty > poDetail.ReceivedQty) {
                                                    deliveryLessThenOrder++;
                                                }
                                            }

                                            // 2. Giao hàng thiếu
                                            if ((poDetail.OrderQty) > importInfo.Quantity)
                                                deliveryLessThenOrder++;

                                            // 3,4. NG
                                            if (importInfo.NG == true) {
                                                NGTimes++;
                                                totalNGQuantity += (int)poDetail.OrderQty;
                                            }
                                            //totalRecied += (int)poDetail.ReceivedQty;
                                            totalRecied += (int)poDetail.OrderQty;
                                        }
                                    }
                                    break;

                            }
                            var zeroPointCount = 0;
                            // 1. Giao hàng trễ
                            if (lateDeliveryTimes > 5) {
                                entity.LogisticsPoint = 0;
                                zeroPointCount++;
                            }
                            else if (lateDeliveryTimes >= 3 && lateDeliveryTimes <= 5) {
                                entity.LogisticsPoint = 10;
                            }
                            else if (lateDeliveryTimes == 1 || lateDeliveryTimes == 2) {
                                entity.LogisticsPoint = 15;
                            }
                            else if (lateDeliveryTimes == 0) {
                                entity.LogisticsPoint = 20;
                            };


                            // 2. Giao hàng thiếu
                            if (deliveryLessThenOrder > 5) {
                                entity.QuantityDeliveryPoint = 0;
                                zeroPointCount++;
                            }
                            else if (deliveryLessThenOrder >= 3 && deliveryLessThenOrder <= 5) {
                                entity.QuantityDeliveryPoint = 10;
                            }
                            else if (deliveryLessThenOrder == 1 || deliveryLessThenOrder == 2) {
                                entity.QuantityDeliveryPoint = 15;
                            }
                            else if (deliveryLessThenOrder == 0) {
                                entity.QuantityDeliveryPoint = 20;
                            };

                            // 3. So lan bi NG
                            if (NGTimes > 4) {
                                entity.NGTimePoint = 0;
                                zeroPointCount++;
                            }
                            else if (NGTimes == 3 || NGTimes == 4) {
                                entity.NGTimePoint = 10;
                            }
                            else if (NGTimes == 1 || NGTimes == 2) {
                                entity.NGTimePoint = 15;
                            }
                            else if (NGTimes == 0) {
                                entity.NGTimePoint = 20;
                            };

                            // 4. So luong. NG
                            double percentNG = (totalNGQuantity * 100 )/ totalRecied;
                            if (totalNGQuantity == 0 || percentNG <= 2) {
                                entity.NGNumberPoint = 20;
                            }
                            else if (percentNG > 2 && percentNG <= 3) {
                                entity.NGNumberPoint = 15;
                            }
                            else if (percentNG > 3 && percentNG <= 4) {
                                entity.NGNumberPoint = 10;
                            }
                            else if(percentNG > 4){
                                entity.NGNumberPoint = 0;
                                zeroPointCount++;
                            }


                            //5. DIem gia ca
                            if (entity.PricePoint == 0) {
                                zeroPointCount++;
                            }

                            //6. DIem ho tro KT
                            if (entity.TechSpPoint == 0) {
                                zeroPointCount++;
                            }

                            entity.ZeroPointCount = zeroPointCount;

                            var totalPoint = (entity.TechSpPoint ?? 0) +
                                                (entity.LogisticsPoint ?? 0) +
                                                (entity.QuantityDeliveryPoint ?? 0) +
                                                (entity.NGNumberPoint ?? 0) +
                                                (entity.NGTimePoint ?? 0) +
                                                (entity.PricePoint ?? 0);
                            entity.TotalPoint = totalPoint;
                            
                            if (entity.TotalPoint == null || entity.ZeroPointCount == null) {
                                entity.Grade = "E";
                            }
                            else if (entity.TotalPoint < 70 || entity.ZeroPointCount >= 2) {
                                entity.Grade = "D";
                            }
                            else {
                                if (entity.ZeroPointCount == 1) {
                                    entity.Grade = "C";
                                }
                                else if (entity.ZeroPointCount == 0) {
                                    if (entity.TotalPoint <= 90) {
                                        entity.Grade = "B";
                                    }
                                    else {
                                        entity.Grade = "A";
                                    }
                                }
                            }
                            entity.OrderQuantity = totalRecied;
                            entity.NGQuantity = totalNGQuantity;
                            var number = vfi.EvaluationForms.Where(t => t.ModifiedDate.Year == DateTime.Now.Year 
                                                                   && t.VendorId == vendorId)
                                                            .Count() +1;
                            var vendorcode = vfi.Vendors.Where(t => t.VendorId == vendorId).Select(t => t.VendorCode).FirstOrDefault();
                            entity.Name = vendorcode + "-" + entity.ModifiedDate.Year + "." + number;

                            vfi.EvaluationForms.Add(entity);
                        }

                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CreateNewEvaluationForm", ex.Message);
            }

            return View(new GridModel(new List<EvaluationFormModel>()));
        }

        [GridAction]
        public ActionResult SelectEvationFormNotApproved(int classified, int vendorId, int status, string toDate, string fromDate) {
            var model = new List<EvaluationFormModel>();
            try {
                model = GetEvationFormNotApproved(classified, vendorId, status, toDate, fromDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectEvationFormNotApproved", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<EvaluationFormModel> GetEvationFormNotApproved(int classified, int vendorId, int status, string toDate, string fromDate) {
            var model = new List<EvaluationFormModel>();
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseDate(toDate);
            //var ManagementApproved = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ManagementApprovedPurchase);
            using (var vfi = new tammaContext()) {
                try {
                    var evaluationForms = vfi.EvaluationForms.Where(t => (status == 0 || t.Status == status)
                                                                    && (classified == 0 || t.MaterialClassifiedId == classified)
                                                                    && (vendorId == 0 || t.VendorId == vendorId)
                                                                    && (string.IsNullOrEmpty(fromDate) || t.ModifiedDate >= fDate)
                                                                    && (string.IsNullOrEmpty(toDate) || t.ModifiedDate <= tDate)
                                                                    )
                                                                    .Select(t => t)
                                                                    .OrderBy(t => t.MaterialClassifiedId)
                                                                    .ThenBy(t => t.ModifiedDate)
                                                                    .ToList();
                    int index = 1;
                    foreach (var form in evaluationForms) {
                        var entity = new EvaluationFormModel {
                            Name = form.Name,
                            ClassifiedName = form.MaterialClassified.MaterialClassifiedName,
                            VendorName = form.Vendor.VendorName,

                            FromDate = form.FromDate,
                            ToDate = form.ToDate,

                            PricePoint = form.PricePoint,
                            TechSpPoint = form.TechSpPoint,
                            LogisticsPoint = form.LogisticsPoint,
                            QuantityDeliveryPoint = form.QuantityDeliveryPoint,
                            NGNumberPoint = form.NGNumberPoint,
                            NGTimePoint = form.NGTimePoint,
                            TotalPoint = form.TotalPoint,
                            ZeroPointCount = form.ZeroPointCount,
                            Grade = form.Grade,

                            Note = form.Note,
                            ModifiedUser = form.ModifiedUser,
                            ModifiedDate = form.ModifiedDate,
                            Index = index,
                            FormId = form.FormId,

                            ApprovedDate = form.ApprovedDate,
                            ApprovedUser = form.ApprovedUser,

                        };
                        if (entity.Grade == null) {
                            entity.Grade = "E";
                        }



                        index++;
                        model.Add(entity);
                    }
                }
                catch (Exception ex) {
                    // Lấy thông tin chi tiết nhất từ Exception
                    var errorMessage = ex.Message;
                    if (ex.InnerException != null) {
                        errorMessage += " | InnerException: " + ex.InnerException.Message;
                    }

                    // Nếu muốn thêm stack trace để debug
                    errorMessage += " | StackTrace: " + ex.StackTrace;

                    ModelState.AddModelError("GetEvationFormNotApproved", errorMessage);
                }

            }
            return model;
        }


        [GridAction]
            public ActionResult CancelEvationForm(int formId, int classified, int vendorId, int status, string toDate, string fromDate){
            try {
                using (var vfi = new tammaContext()) {
                    var evationForm = vfi.EvaluationForms.FirstOrDefault(ip => ip.FormId == formId);
                    if (evationForm == null)
                        throw new AggregateException("Không tìm thấy phiếu đánh giá");
                    evationForm.Status = (byte)MyUtilities.PurchaseOrder.EvaluationEnum.Cancel;
                    evationForm.ApprovedDate = DateTime.Now;
                    evationForm.ApprovedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CancelEvationForm", ex.Message);
            }
            return View(new GridModel(GetEvationFormNotApproved(classified, vendorId, status, toDate, fromDate)));
        }

        public ActionResult ImportSignature(int formId) {
            using (var vfi = new tammaContext()) {
                var import = vfi.EvaluationForms.FirstOrDefault(i => i.FormId == formId);
                vfi.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                if (import != null) {
                    import.Status = (byte)MyUtilities.PurchaseOrder.EvaluationEnum.Approved;
                    import.ApprovedDate = DateTime.Now;
                    import.ApprovedUser = HttpContext.User.Identity.Name;
                }
                else return Json(9);
                vfi.SaveChanges();
            }
            return null;
        }



        [GridAction]
        public ActionResult SelectGetEvationDetail(int formId) {
            var model = new List<EvaluationFormModel>();
            try {
                model = GetEvationDetail(formId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetEvationDetail", ex.Message);
            }
            return View(new GridModel(model));
        }


        List<EvaluationFormModel> GetEvationDetail(int formId) {
            var model = new List<EvaluationFormModel>();
            //var ManagementApproved = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ManagementApprovedPurchase);
            using (var vfi = new tammaContext()) {
                try {
                    var evaluationForm = vfi.EvaluationForms.Where(t => t.FormId == formId)
                                                             .Select(t => new {
                                                                 t.FromDate,
                                                                 t.ToDate,
                                                                 t.VendorId,
                                                                 t.MaterialClassifiedId,
                                                             }).FirstOrDefault();
                    var poDetaiList = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrder.ShipDate >= evaluationForm.FromDate
                                                                     && t.PurchaseOrder.ShipDate <= evaluationForm.ToDate
                                                                     && t.MaterialClassifiedId == evaluationForm.MaterialClassifiedId
                                                                     && t.PurchaseOrder.VendorId == evaluationForm.VendorId
                                                                     && t.PurchaseOrder.Status != (byte)MyUtilities.Sales.Status.Cancel
                                                                     ).Select(t => new { 
                                                                         t.PurchaseOrderDetailId,
                                                                         t.OrderQty,
                                                                         t.ReceivedQty,
                                                                         OrderDate = t.PurchaseOrder.ShipDate,
                                                                         PurchaseOrderNumber = t.PurchaseOrder.RevisionNumber,
                                                                         t.ReferenceId,
                                                                         UnitMeasure = t.Unit,
                                                                     }).ToList();
                    int index = 1;
                    switch (evaluationForm.MaterialClassifiedId) {
                        case 1:
                            foreach (var poDetail in poDetaiList) {
                                var entity = new EvaluationFormModel {
                                    OrderDate = poDetail.OrderDate,
                                    OrderQuantity = poDetail.OrderQty,
                                    ReceivedQuantity = poDetail.ReceivedQty,
                                    PurchaseOrderCode = poDetail.PurchaseOrderNumber,
                                    UnitMeasure = poDetail.UnitMeasure,
                                    Index = index,

                                };
                                entity.MaterialCode = vfi.Materials.Where(t => t.MaterialId == poDetail.ReferenceId).Select(t => t.MaterialCode).FirstOrDefault();
                                if (poDetail.ReceivedQty == 0) {
                                    entity.CountLateDelivery = 1;
                                    entity.CountLessThanOrder = 1;
                                    entity.DeliveryDate = DateTime.MinValue;
                                    entity.RemainingQuantity = entity.OrderQuantity;
                                }
                                else {
                                    var import = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                                                      && t.VendorId == evaluationForm.VendorId
                                                                                      && !string.IsNullOrEmpty(t.LotNumber)
                                                                                ).Select(t => new {
                                                                                         ImportDate = t.ImportPurchaseOrder.ImportDate,
                                                                                         TransactionCode = t.ImportPurchaseOrder.Transaction.TransactionCode,
                                                                                         t.NG,
                                                                                         t.LotNumber,
                                                                                         t.Quantity,
                                                                                         t.QuantityKg,
                                                                                         t.UnitWeight,
                                                                                }).FirstOrDefault();
                                    entity.DeliveryDate = import.ImportDate;
                                    entity.ImportCode = import.TransactionCode;
                                    entity.LotNumber = import.LotNumber;
                                    if (import.NG == true) {
                                        entity.CountNG = 1;
                                        entity.NGQuantity = entity.OrderQuantity;
                                    }
                                    if (entity.DeliveryDate > entity.OrderDate) {
                                        entity.CountLateDelivery = 1;
                                    }
                                    if (import.QuantityKg != 0) {
                                        entity.ReceivedQuantity = import.QuantityKg;
                                        if ((entity.OrderQuantity * 0.9) > import.QuantityKg) {
                                            entity.CountLessThanOrder = 1;
                                            entity.RemainingQuantity = entity.OrderQuantity - import.QuantityKg;
                                        }
                                    }
                                    else if (import.QuantityKg == 0) {
                                        var importKg = import.Quantity * import.UnitWeight;
                                        entity.ReceivedQuantity = importKg;
                                        if ((entity.OrderQuantity * 0.9) > importKg) {
                                            entity.CountLessThanOrder = 1;
                                            entity.ReceivedQuantity = entity.OrderQuantity - importKg;
                                        }
                                    }
                                }
                                index++;
                                model.Add(entity);
                            }
                            
                            break;

                        case 2:
                            foreach (var poDetail in poDetaiList) {
                                var entity = new EvaluationFormModel {
                                    OrderDate = poDetail.OrderDate,
                                    OrderQuantity = poDetail.OrderQty,
                                    ReceivedQuantity = poDetail.ReceivedQty,
                                    PurchaseOrderCode = poDetail.PurchaseOrderNumber,
                                    UnitMeasure = poDetail.UnitMeasure,
                                    Index = index,
                                };
                                entity.MaterialCode = vfi.Fuels.Where(t => t.FuelId == poDetail.ReferenceId).Select(t => t.FuelFullCode).FirstOrDefault();
                                if (poDetail.ReceivedQty == 0) {
                                    entity.CountLateDelivery = 1;
                                    entity.CountLessThanOrder = 1;
                                    entity.DeliveryDate = DateTime.MinValue;
                                    entity.RemainingQuantity = entity.OrderQuantity;
                                }
                                else {
                                    var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                 && t.VendorId == evaluationForm.VendorId
                                                 && !string.IsNullOrEmpty(t.LotNumber))
                                                 .Select(t => new {
                                                     t.TransactionFpt.TransactionDate,
                                                     t.TransactionFpt.TransactionCode,
                                                     t.NG,
                                                     t.LotNumber,
                                                     t.Quantity,
                                                 })
                                                 .FirstOrDefault();
                                    entity.DeliveryDate = import.TransactionDate;
                                    entity.ImportCode = import.TransactionCode;
                                    entity.LotNumber = import.LotNumber;
                                    entity.ReceivedQuantity = import.Quantity;

                                    if (import.NG == true) {
                                        entity.CountNG = 1;
                                        entity.NGQuantity = entity.OrderQuantity;
                                    }

                                    if (entity.DeliveryDate > entity.OrderDate) {
                                        entity.CountLateDelivery = 1;
                                    }

                                    if ((entity.OrderQuantity) > import.Quantity) {
                                        entity.CountLessThanOrder = 1;
                                        entity.RemainingQuantity = entity.OrderQuantity - import.Quantity;
                                    }
                                }
                                index++;
                                model.Add(entity);
                            }

                            break;


                        case 3:
                            foreach (var poDetail in poDetaiList) {

                                var entity = new EvaluationFormModel {
                                    OrderDate = poDetail.OrderDate,
                                    OrderQuantity = poDetail.OrderQty,
                                    ReceivedQuantity = poDetail.ReceivedQty,
                                    PurchaseOrderCode = poDetail.PurchaseOrderNumber,
                                    UnitMeasure = poDetail.UnitMeasure,
                                    Index = index,
                                };
                                entity.MaterialCode = vfi.Tools.Where(t => t.ToolId == poDetail.ReferenceId).Select(t => t.ToolFullCode).FirstOrDefault();
                                if (poDetail.ReceivedQty == 0) {
                                    entity.CountLateDelivery = 1;
                                    entity.CountLessThanOrder = 1;
                                    entity.DeliveryDate = DateTime.MinValue;
                                    entity.RemainingQuantity = entity.OrderQuantity;
                                }
                                else {
                                    var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                 && t.VendorId == evaluationForm.VendorId
                                                 && !string.IsNullOrEmpty(t.LotNumber))
                                                 .Select(t => new {
                                                     t.TransactionFpt.TransactionDate,
                                                     t.TransactionFpt.TransactionCode,
                                                     t.NG,
                                                     t.LotNumber,
                                                     t.Quantity,
                                                 }).FirstOrDefault();

                                    entity.DeliveryDate = import.TransactionDate;
                                    entity.ImportCode = import.TransactionCode;
                                    entity.LotNumber = import.LotNumber;
                                    entity.ReceivedQuantity = import.Quantity;

                                    if (import.NG == true) {
                                        entity.CountNG = 1;
                                        entity.NGQuantity = entity.OrderQuantity;
                                    }

                                    if (entity.DeliveryDate > entity.OrderDate) {
                                        entity.CountLateDelivery = 1;
                                    }

                                    if ((entity.OrderQuantity) > import.Quantity) {
                                        entity.CountLessThanOrder = 1;
                                        entity.RemainingQuantity = entity.OrderQuantity - import.Quantity;

                                    }
                                }
                                index++;
                                model.Add(entity);
                            }

                            break;
                    }

                }
                catch (Exception ex) {
                    // Lấy thông tin chi tiết nhất từ Exception
                    var errorMessage = ex.Message;
                    if (ex.InnerException != null) {
                        errorMessage += " | InnerException: " + ex.InnerException.Message;
                    }

                    // Nếu muốn thêm stack trace để debug
                    errorMessage += " | StackTrace: " + ex.StackTrace;

                    ModelState.AddModelError("GetEvationFormNotApproved", errorMessage);
                }

            }
            return model;
        }



        [GridAction]
        public ActionResult SelectEvaluationDetail(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<EvaluationFormModel>();
            try {
                model = GetEvaluationDetail(vendorId, fromDate, toDate, materialClassifiedId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetEvaluationDetail", ex.Message);
            }
            return View(new GridModel(model));
        }


        List<EvaluationFormModel> GetEvaluationDetail(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<EvaluationFormModel>();
            //var ManagementApproved = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ManagementApprovedPurchase);
            using (var vfi = new tammaContext()) {
                try {
                    var fDate = MyUtilities.Function.ParseDate(fromDate);
                    var tDate = MyUtilities.Function.ParseDate(toDate);
                    var poDetaiList = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrder.ShipDate >= fDate
                                                                     && t.PurchaseOrder.ShipDate <= tDate
                                                                     && t.MaterialClassifiedId == materialClassifiedId
                                                                     && t.PurchaseOrder.VendorId == vendorId
                                                                     && t.PurchaseOrder.Status != (byte)MyUtilities.Sales.Status.Cancel
                                                                     ).Select(t => new {
                                                                         t.PurchaseOrderDetailId,
                                                                         t.OrderQty,
                                                                         t.ReceivedQty,
                                                                         OrderDate = t.PurchaseOrder.ShipDate,
                                                                         PurchaseOrderNumber = t.PurchaseOrder.RevisionNumber,
                                                                         t.ReferenceId,
                                                                         UnitMeasure = t.Unit,
                                                                     }).ToList();
                    int index = 1;
                    switch (materialClassifiedId) {
                        case 1:
                            foreach (var poDetail in poDetaiList) {
                                var entity = new EvaluationFormModel {
                                    OrderDate = poDetail.OrderDate,
                                    OrderQuantity = poDetail.OrderQty,
                                    ReceivedQuantity = poDetail.ReceivedQty,
                                    PurchaseOrderCode = poDetail.PurchaseOrderNumber,
                                    UnitMeasure = poDetail.UnitMeasure,
                                    Index = index,

                                };
                                entity.MaterialCode = vfi.Materials.Where(t => t.MaterialId == poDetail.ReferenceId).Select(t => t.MaterialCode).FirstOrDefault();
                                if (poDetail.ReceivedQty == 0) {
                                    entity.CountLateDelivery = 1;
                                    entity.CountLessThanOrder = 1;
                                    entity.DeliveryDate = DateTime.MinValue;
                                    entity.RemainingQuantity = entity.OrderQuantity;
                                }
                                else {
                                    var import = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                                                      && t.VendorId == vendorId
                                                                                      && !string.IsNullOrEmpty(t.LotNumber)
                                                                                ).Select(t => new {
                                                                                    ImportDate = t.ImportPurchaseOrder.ImportDate,
                                                                                    TransactionCode = t.ImportPurchaseOrder.Transaction.TransactionCode,
                                                                                    t.NG,
                                                                                    t.LotNumber,
                                                                                    t.Quantity,
                                                                                    t.QuantityKg,
                                                                                    t.UnitWeight,
                                                                                }).FirstOrDefault();
                                    entity.DeliveryDate = import.ImportDate;
                                    entity.ImportCode = import.TransactionCode;
                                    entity.LotNumber = import.LotNumber;
                                    if (import.NG == true) {
                                        entity.CountNG = 1;
                                        entity.NGQuantity = entity.OrderQuantity;
                                    }
                                    if (entity.DeliveryDate > entity.OrderDate) {
                                        entity.CountLateDelivery = 1;
                                    }
                                    if (import.QuantityKg != 0) {
                                        entity.ReceivedQuantity = import.QuantityKg;
                                        if ((entity.OrderQuantity * 0.9) > import.QuantityKg) {
                                            entity.CountLessThanOrder = 1;
                                            entity.RemainingQuantity = entity.OrderQuantity - import.QuantityKg;
                                        }
                                    }
                                    else if (import.QuantityKg == 0) {
                                        var importKg = import.Quantity * import.UnitWeight;
                                        entity.ReceivedQuantity = importKg;
                                        if ((entity.OrderQuantity * 0.9) > importKg) {
                                            entity.CountLessThanOrder = 1;
                                            entity.RemainingQuantity = entity.OrderQuantity - importKg;
                                        }
                                    }
                                }
                                index++;
                                model.Add(entity);
                            }

                            break;

                        case 2:
                            foreach (var poDetail in poDetaiList) {
                                var entity = new EvaluationFormModel {
                                    OrderDate = poDetail.OrderDate,
                                    OrderQuantity = poDetail.OrderQty,
                                    ReceivedQuantity = poDetail.ReceivedQty,
                                    PurchaseOrderCode = poDetail.PurchaseOrderNumber,
                                    UnitMeasure = poDetail.UnitMeasure,
                                    Index = index,
                                };
                                entity.MaterialCode = vfi.Fuels.Where(t => t.FuelId == poDetail.ReferenceId).Select(t => t.FuelFullCode).FirstOrDefault();
                                if (poDetail.ReceivedQty == 0) {
                                    entity.CountLateDelivery = 1;
                                    entity.CountLessThanOrder = 1;
                                    entity.DeliveryDate = DateTime.MinValue;
                                    entity.RemainingQuantity = entity.OrderQuantity;
                                }
                                else {
                                    var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                 && t.VendorId == vendorId
                                                 && !string.IsNullOrEmpty(t.LotNumber))
                                                 .Select(t => new {
                                                     t.TransactionFpt.TransactionDate,
                                                     t.TransactionFpt.TransactionCode,
                                                     t.NG,
                                                     t.LotNumber,
                                                     t.Quantity,
                                                 })
                                                 .FirstOrDefault();
                                    entity.DeliveryDate = import.TransactionDate;
                                    entity.ImportCode = import.TransactionCode;
                                    entity.LotNumber = import.LotNumber;
                                    entity.ReceivedQuantity = import.Quantity;

                                    if (import.NG == true) {
                                        entity.CountNG = 1;
                                        entity.NGQuantity = entity.OrderQuantity;
                                    }

                                    if (entity.DeliveryDate > entity.OrderDate) {
                                        entity.CountLateDelivery = 1;
                                    }
                                    if ((entity.OrderQuantity) > import.Quantity) {
                                        entity.CountLessThanOrder = 1;
                                        entity.RemainingQuantity = entity.OrderQuantity - import.Quantity;
                                    }
                                    
                                }
                                index++;
                                model.Add(entity);
                            }

                            break;


                        case 3:
                            foreach (var poDetail in poDetaiList) {

                                var entity = new EvaluationFormModel {
                                    OrderDate = poDetail.OrderDate,
                                    OrderQuantity = poDetail.OrderQty,
                                    ReceivedQuantity = poDetail.ReceivedQty,
                                    PurchaseOrderCode = poDetail.PurchaseOrderNumber,
                                    UnitMeasure = poDetail.UnitMeasure,
                                    Index = index,
                                };
                                entity.MaterialCode = vfi.Tools.Where(t => t.ToolId == poDetail.ReferenceId).Select(t => t.ToolFullCode).FirstOrDefault();
                                if (poDetail.ReceivedQty == 0) {
                                    entity.CountLateDelivery = 1;
                                    entity.CountLessThanOrder = 1;
                                    entity.DeliveryDate = DateTime.MinValue;
                                    entity.RemainingQuantity = entity.OrderQuantity;
                                }
                                else {
                                    var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId
                                                 && t.VendorId == vendorId
                                                 && !string.IsNullOrEmpty(t.LotNumber))
                                                 .Select(t => new {
                                                     t.TransactionFpt.TransactionDate,
                                                     t.TransactionFpt.TransactionCode,
                                                     t.NG,
                                                     t.LotNumber,
                                                     t.Quantity,
                                                 }).FirstOrDefault();

                                    entity.DeliveryDate = import.TransactionDate;
                                    entity.ImportCode = import.TransactionCode;
                                    entity.LotNumber = import.LotNumber;
                                    entity.ReceivedQuantity = import.Quantity;

                                    if (import.NG == true) {
                                        entity.CountNG = 1;
                                        entity.NGQuantity = entity.OrderQuantity;
                                    }

                                    if (entity.DeliveryDate > entity.OrderDate) {
                                        entity.CountLateDelivery = 1;
                                    }

                                    if ((entity.OrderQuantity) > import.Quantity) {
                                        entity.CountLessThanOrder = 1;
                                        entity.RemainingQuantity = entity.OrderQuantity - import.Quantity;
                                    }
                                }
                                index++;
                                model.Add(entity);
                            }

                            break;
                    }

                }
                catch (Exception ex) {
                    // Lấy thông tin chi tiết nhất từ Exception
                    var errorMessage = ex.Message;
                    if (ex.InnerException != null) {
                        errorMessage += " | InnerException: " + ex.InnerException.Message;
                    }

                    // Nếu muốn thêm stack trace để debug
                    errorMessage += " | StackTrace: " + ex.StackTrace;

                    ModelState.AddModelError("GetEvaluationDetail", errorMessage);
                }

            }
            return model;
        }

        // Late
        [GridAction]
        public ActionResult SelectLateDelivery(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<LogisticsModel>();
            try {
                model = GetLateDelivery(vendorId, fromDate, toDate, materialClassifiedId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetEvaluationDetail", ex.Message);
            }
            return View(new GridModel(model));
        }


        List<LogisticsModel> GetLateDelivery(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<LogisticsModel>();
            //var ManagementApproved = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ManagementApprovedPurchase);
            using (var vfi = new tammaContext()) {
                try {
                    var fDate = MyUtilities.Function.ParseDate(fromDate);
                    var tDate = MyUtilities.Function.ParseDate(toDate);

                    var entity = new LogisticsModel {
                    };
                    var poDetaiList = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrder.ShipDate >= fDate
                                                                     && t.PurchaseOrder.ShipDate <= tDate
                                                                     && t.MaterialClassifiedId == materialClassifiedId
                                                                     && t.PurchaseOrder.VendorId == vendorId
                                                                     && t.PurchaseOrder.Status != (byte)MyUtilities.Sales.Status.Cancel
                                                                     ).Select(t => new {
                                                                         t.PurchaseOrderDetailId,
                                                                         t.ReceivedQty,
                                                                         OrderDate = t.PurchaseOrder.ShipDate,
                                                                     }).ToList();
                    var countLateDelivery = 0;

                    switch (materialClassifiedId) {
                        case 1: {
                            foreach (var detail in poDetaiList){
                                if (detail.ReceivedQty == 0) {
                                    countLateDelivery++;
                                    continue;
                                }
                                else if (detail.ReceivedQty != 0) {
                                    var importDate = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                          && t.VendorId == vendorId
                                                                                          && !string.IsNullOrEmpty(t.LotNumber)
                                                                                          ).Select(t => t.ImportPurchaseOrder.ImportDate)
                                                                                          .FirstOrDefault();
                                    if (detail.OrderDate < importDate) {
                                        countLateDelivery++;
                                    }
                                }
                                };
                            break;
                            }
                        case 2: {
                                foreach (var detail in poDetaiList) {
                                    if (detail.ReceivedQty == 0) {
                                        countLateDelivery++;
                                        continue;
                                    }
                                    else if (detail.ReceivedQty != 0) {
                                        var importDate = vfi.TransactionFptDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                              && t.VendorId == vendorId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber)
                                                                                              ).Select(t => t.TransactionFpt.TransactionDate)
                                                                                              .FirstOrDefault();
                                        if (detail.OrderDate < importDate) {
                                            countLateDelivery++;
                                        }
                                    }
                                };
                                break;
                            }
                        case 3: {
                                foreach (var detail in poDetaiList) {
                                    if (detail.ReceivedQty == 0) {
                                        countLateDelivery++;
                                        continue;
                                    }
                                    else if (detail.ReceivedQty != 0) {
                                        var importDate = vfi.TransactionFptDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                              && t.VendorId == vendorId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber)
                                                                                              ).Select(t => t.TransactionFpt.TransactionDate)
                                                                                              .FirstOrDefault();
                                        if (detail.OrderDate < importDate) {
                                            countLateDelivery++;
                                        }
                                    }
                                };
                                break;
                            }
                    }
                    if (countLateDelivery == 0) {
                        entity.PlaceA = true;
                    }
                    else if (countLateDelivery == 1 || countLateDelivery == 2) {
                        entity.PlaceB = true;
                    }
                    else if (countLateDelivery >= 3 && countLateDelivery <= 5) {
                        entity.PlaceC = true;
                    }
                    else if (countLateDelivery > 5) {
                        entity.PlaceD = true;
                    };

                    entity.Count = countLateDelivery;

                    model.Add(entity);
                    }
                catch (Exception ex) {
                    // Lấy thông tin chi tiết nhất từ Exception
                    var errorMessage = ex.Message;
                    if (ex.InnerException != null) {
                        errorMessage += " | InnerException: " + ex.InnerException.Message;
                    }

                    // Nếu muốn thêm stack trace để debug
                    errorMessage += " | StackTrace: " + ex.StackTrace;

                    ModelState.AddModelError("GetLateDelivery", errorMessage);
                }

            }
            return model;
        }


        // Quantity
        [GridAction]
        public ActionResult SelectQuantityDelivery(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<LogisticsModel>();
            try {
                model = GetQuantityDelivery(vendorId, fromDate, toDate, materialClassifiedId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetEvaluationDetail", ex.Message);
            }
            return View(new GridModel(model));
        }


        List<LogisticsModel> GetQuantityDelivery(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<LogisticsModel>();
            //var ManagementApproved = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ManagementApprovedPurchase);
            using (var vfi = new tammaContext()) {
                try {
                    var fDate = MyUtilities.Function.ParseDate(fromDate);
                    var tDate = MyUtilities.Function.ParseDate(toDate);

                    var entity = new LogisticsModel {
                    };
                    var poDetaiList = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrder.ShipDate >= fDate
                                                                     && t.PurchaseOrder.ShipDate <= tDate
                                                                     && t.MaterialClassifiedId == materialClassifiedId
                                                                     && t.PurchaseOrder.VendorId == vendorId
                                                                     && t.PurchaseOrder.Status != (byte)MyUtilities.Sales.Status.Cancel
                                                                     ).Select(t => new {
                                                                         t.PurchaseOrderDetailId,
                                                                         t.OrderQty,
                                                                         t.ReceivedQty,
                                                                     }).ToList();
                    var countQuantity = 0;

                    switch (materialClassifiedId) {
                        case 1: {
                                foreach (var detail in poDetaiList) {
                                    if (detail.ReceivedQty == 0) {
                                        countQuantity++;
                                        continue;
                                    }
                                    else if (detail.ReceivedQty != 0) {
                                        var import = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                              && t.VendorId == vendorId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber)
                                                                                              ).Select(t => new { t.Quantity, t.QuantityKg, t.UnitWeight })
                                                                                              .FirstOrDefault();
                                        if (import.QuantityKg != 0) {
                                            if ((detail.OrderQty * 0.9) > import.QuantityKg) {
                                                countQuantity++;
                                            }
                                        }
                                        else if (import.QuantityKg == 0) {
                                            var importQuantity = import.Quantity * import.UnitWeight;
                                            if ((detail.OrderQty * 0.9) > importQuantity) {
                                                countQuantity++;
                                            }
                                        }
                                    }
                                };
                                break;
                            }
                        case 2: {
                                foreach (var detail in poDetaiList) {
                                    if (detail.ReceivedQty == 0) {
                                        countQuantity++;
                                        continue;
                                    }
                                    else if (detail.ReceivedQty != 0) {
                                        var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                              && t.VendorId == vendorId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber)
                                                                                              ).Select(t => t.Quantity)
                                                                                              .FirstOrDefault();
                                        if (detail.OrderQty > import) {
                                            countQuantity++;
                                        }
                                    }
                                };
                                break;
                            }
                        case 3: {
                                foreach (var detail in poDetaiList) {
                                    if (detail.ReceivedQty == 0) {
                                        countQuantity++;
                                        continue;
                                    }
                                    else if (detail.ReceivedQty != 0) {
                                        var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                              && t.VendorId == vendorId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber)
                                                                                              ).Select(t => t.Quantity)
                                                                                              .FirstOrDefault();
                                        if (detail.OrderQty > import) {
                                            countQuantity++;
                                        }
                                    }
                                };
                                break;
                            }
                    }
                    if (countQuantity == 0) {
                        entity.PlaceA = true;
                    }
                    else if (countQuantity == 1 || countQuantity == 2) {
                        entity.PlaceB = true;
                    }
                    else if (countQuantity >= 3 && countQuantity <= 5) {
                        entity.PlaceC = true;
                    }
                    else if (countQuantity > 5) {
                        entity.PlaceD = true;
                    };

                    entity.Count = countQuantity;

                    model.Add(entity);
                }
                catch (Exception ex) {
                    // Lấy thông tin chi tiết nhất từ Exception
                    var errorMessage = ex.Message;
                    if (ex.InnerException != null) {
                        errorMessage += " | InnerException: " + ex.InnerException.Message;
                    }

                    // Nếu muốn thêm stack trace để debug
                    errorMessage += " | StackTrace: " + ex.StackTrace;

                    ModelState.AddModelError("GetQuantityDelivery", errorMessage);
                }

            }
            return model;
        }


        // NG times
        [GridAction]
        public ActionResult SelectNGDelivery(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<LogisticsModel>();
            try {
                model = GetNGDelivery(vendorId, fromDate, toDate, materialClassifiedId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetEvaluationDetail", ex.Message);
            }
            return View(new GridModel(model));
        }


        List<LogisticsModel> GetNGDelivery(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<LogisticsModel>();
            //var ManagementApproved = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ManagementApprovedPurchase);
            using (var vfi = new tammaContext()) {
                try {
                    var fDate = MyUtilities.Function.ParseDate(fromDate);
                    var tDate = MyUtilities.Function.ParseDate(toDate);

                    var entity = new LogisticsModel {
                    };
                    var poDetaiList = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrder.ShipDate >= fDate
                                                                     && t.PurchaseOrder.ShipDate <= tDate
                                                                     && t.MaterialClassifiedId == materialClassifiedId
                                                                     && t.PurchaseOrder.VendorId == vendorId
                                                                     && t.PurchaseOrder.Status != (byte)MyUtilities.Sales.Status.Cancel
                                                                     ).Select(t => new {
                                                                         t.PurchaseOrderDetailId,
                                                                         t.OrderQty,
                                                                         t.ReceivedQty,
                                                                     }).ToList();
                    var countNG = 0;

                    switch (materialClassifiedId) {
                        case 1: {
                                foreach (var detail in poDetaiList) {
                                    if (detail.ReceivedQty == 0) {
                                        continue;
                                    }
                                    else if (detail.ReceivedQty != 0) {
                                        var import = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                              && t.VendorId == vendorId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber)
                                                                                              ).Select(t =>  t.NG)
                                                                                              .FirstOrDefault();

                                        if (import == true) {
                                            countNG++;
                                        }
                                    }
                                };
                                break;
                            }
                        case 2: {
                                foreach (var detail in poDetaiList) {
                                    if (detail.ReceivedQty == 0) {
                                        continue;
                                    }
                                    else if (detail.ReceivedQty != 0) {
                                        var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                              && t.VendorId == vendorId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber)
                                                                                              ).Select(t => t.NG)
                                                                                              .FirstOrDefault();
                                        if (import == true) {
                                            countNG++;
                                        }
                                    }
                                };
                                break;
                            }
                        case 3: {
                                foreach (var detail in poDetaiList) {
                                    if (detail.ReceivedQty == 0) {
                                        continue;
                                    }
                                    else if (detail.ReceivedQty != 0) {
                                        var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                              && t.VendorId == vendorId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber)
                                                                                              ).Select(t => t.NG)
                                                                                              .FirstOrDefault();
                                        if (import == true) {
                                            countNG++;
                                        }
                                    }
                                };
                                break;
                            }
                    }
                    if (countNG == 0) {
                        entity.PlaceA = true;
                    }
                    else if (countNG == 1 || countNG == 2) {
                        entity.PlaceB = true;
                    }
                    else if (countNG == 3 || countNG == 4) {
                        entity.PlaceC = true;
                    }
                    else if (countNG > 4) {
                        entity.PlaceD = true;
                    };

                    entity.Count = countNG;

                    model.Add(entity);
                }
                catch (Exception ex) {
                    // Lấy thông tin chi tiết nhất từ Exception
                    var errorMessage = ex.Message;
                    if (ex.InnerException != null) {
                        errorMessage += " | InnerException: " + ex.InnerException.Message;
                    }

                    // Nếu muốn thêm stack trace để debug
                    errorMessage += " | StackTrace: " + ex.StackTrace;

                    ModelState.AddModelError("GetNGDelivery", errorMessage);
                }

            }
            return model;
        }


        // NG Quanitty
        [GridAction]
        public ActionResult SelectNGQuantity(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<LogisticsModel>();
            try {
                model = GetNGQuantity(vendorId, fromDate, toDate, materialClassifiedId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetEvaluationDetail", ex.Message);
            }
            return View(new GridModel(model));
        }


        List<LogisticsModel> GetNGQuantity(int? vendorId, string fromDate, string toDate, int? materialClassifiedId) {
            var model = new List<LogisticsModel>();
            //var ManagementApproved = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ManagementApprovedPurchase);
            using (var vfi = new tammaContext()) {
                try {
                    var fDate = MyUtilities.Function.ParseDate(fromDate);
                    var tDate = MyUtilities.Function.ParseDate(toDate);

                    var entity = new LogisticsModel {
                    };
                    var poDetaiList = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrder.ShipDate >= fDate
                                                                     && t.PurchaseOrder.ShipDate <= tDate
                                                                     && t.MaterialClassifiedId == materialClassifiedId
                                                                     && t.PurchaseOrder.VendorId == vendorId
                                                                     && t.PurchaseOrder.Status != (byte)MyUtilities.Sales.Status.Cancel
                                                                     ).Select(t => new {
                                                                         t.PurchaseOrderDetailId,
                                                                         t.OrderQty,
                                                                         t.ReceivedQty,
                                                                     }).ToList();
                    //var countNG = 0;
                    int totalOderquantity = 0;
                    var NGQuantity = 0;

                    switch (materialClassifiedId) {
                        case 1: {
                                foreach (var detail in poDetaiList) {
                                    totalOderquantity += (int)detail.OrderQty;
                                    if (detail.ReceivedQty == 0) {
                                        continue;
                                    }
                                    else if (detail.ReceivedQty != 0) {
                                        var import = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                              && t.VendorId == vendorId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber)
                                                                                              ).Select(t => t.NG)
                                                                                              .FirstOrDefault();

                                        if (import == true) {
                                            NGQuantity += (int)detail.OrderQty;
                                        }
                                    }
                                };
                                break;
                            }
                        case 2: {
                                foreach (var detail in poDetaiList) {
                                    totalOderquantity += (int)detail.OrderQty;
                                    if (detail.ReceivedQty == 0) {
                                        continue;
                                    }
                                    else if (detail.ReceivedQty != 0) {
                                        var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                              && t.VendorId == vendorId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber)
                                                                                              ).Select(t => t.NG)
                                                                                              .FirstOrDefault();
                                        if (import == true) {
                                            NGQuantity += (int)detail.OrderQty;
                                        }
                                    }
                                };
                                break;
                            }
                        case 3: {
                                foreach (var detail in poDetaiList) {
                                    totalOderquantity += (int)detail.OrderQty;
                                    if (detail.ReceivedQty == 0) {
                                        continue;
                                    }
                                    else if (detail.ReceivedQty != 0) {
                                        var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                              && t.VendorId == vendorId
                                                                                              && !string.IsNullOrEmpty(t.LotNumber)
                                                                                              ).Select(t => t.NG)
                                                                                              .FirstOrDefault();
                                        if (import == true) {
                                            NGQuantity += (int)detail.OrderQty;
                                        }
                                    }
                                };
                                break;
                            }
                    }
                    double percent = (NGQuantity * 100 / totalOderquantity);
                    if (percent < 2) {
                        entity.PlaceA = true;
                    }
                    else if (percent >= 2 && percent <= 3) {
                        entity.PlaceB = true;
                    }
                    else if (percent > 3 && percent <= 4) {
                        entity.PlaceC = true;
                    }
                    else if (percent > 4) {
                        entity.PlaceD = true;
                    }
                    entity.Count = (int)percent;

                    model.Add(entity);
                }
                catch (Exception ex) {
                    // Lấy thông tin chi tiết nhất từ Exception
                    var errorMessage = ex.Message;
                    if (ex.InnerException != null) {
                        errorMessage += " | InnerException: " + ex.InnerException.Message;
                    }

                    // Nếu muốn thêm stack trace để debug
                    errorMessage += " | StackTrace: " + ex.StackTrace;

                    ModelState.AddModelError("GetNGQuantity", errorMessage);
                }

            }
            return model;
        }

        List<PrintEvaluationForm> GetEvaluationFormModel(int formId) {
            var model = new List<PrintEvaluationForm>();
            if (formId == 0) return model;
            try {
                using (var vfi = new tammaContext()) {
                    var evaluation = vfi.EvaluationForms.FirstOrDefault(t => t.FormId == formId 
                                                                        && t.Status != (byte)MyUtilities.PurchaseOrder.EvaluationEnum.Cancel);
                    if (evaluation != null) {
                        var info = new WorkGroupInfo();
                        var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.Active);
                        if (workgroup != null) {
                            info = new WorkGroupInfo {
                                //Logo = workgroup.ImagePath + "/Logo/" + workgroup.LogoImage,
                                //CompanyFullName = workgroup.CompanyFullName,
                                //CompanyShortName = workgroup.CompanyShortName,
                                //Address = workgroup.Address,
                                //TelNumber = "Tel : " + workgroup.TelNumber,
                                //FaxNumber = "Fax : " + workgroup.FaxNumber,
                                //Email = "Email: " + workgroup.Email,
                                //Website = "Website: " + workgroup.Website,
                                Logo = workgroup.ImagePath + "/Logo/" +
                                            (workgroup.LogoImage == "Logo-AVF.png"
                                                ? "Logo-AVF-New.jpg"
                                                : workgroup.LogoImage
                                            ),
                            };
                        }
                        var entity = new PrintEvaluationForm {
                            FormId = formId,
                            VendorId = evaluation.VendorId,
                            LogisticsPoint = evaluation.LogisticsPoint,
                            QuantityDeliveryPoint = evaluation.QuantityDeliveryPoint,
                            NGNumberPoint = evaluation.NGNumberPoint,
                            NGTimePoint = evaluation.NGTimePoint,
                            FromDate = evaluation.FromDate,
                            ToDate = evaluation.ToDate,
                            TechSpPoint = evaluation.TechSpPoint,
                            PricePoint = evaluation.PricePoint,
                            TotalPoint = evaluation.TotalPoint,
                            Grade = evaluation.Grade,
                            Info = info,
                            Status = evaluation.Status,
                            ModifiedDate = evaluation.ModifiedDate,
                            NGQuantity = evaluation.NGQuantity,
                            OrderQuantity = evaluation.OrderQuantity,
                            MaterialClassifiedId = evaluation.MaterialClassifiedId
                        };
                        if (entity.NGQuantity == 0) {
                            entity.PercentNGQuantity = 0;
                        }
                        else if (entity.NGQuantity != 0) {
                            entity.PercentNGQuantity = (entity.NGQuantity * 100) / (double)entity.OrderQuantity;
                        }
                        entity.VendorName = vfi.Vendors.Where(t => t.VendorId == entity.VendorId).Select(t => t.CompanyName).FirstOrDefault();
                        var detailList = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrder.ShipDate >= entity.FromDate
                                                                        && t.PurchaseOrder.ShipDate <= entity.ToDate
                                                                        && t.PurchaseOrder.VendorId == entity.VendorId
                                                                        && t.MaterialClassifiedId == entity.MaterialClassifiedId
                                                                        && t.PurchaseOrder.Status != (byte)MyUtilities.Sales.Status.Cancel
                                                                        ).Select(t => new {
                                                                            t.PurchaseOrderDetailId,
                                                                            t.OrderQty,
                                                                            t.ReceivedQty,
                                                                            OrderDate = t.PurchaseOrder.ShipDate,

                                                                        }).ToList();
                        var lateDeliveryCount = 0;
                        var lessDeliveryCount = 0;
                        var NGTimesCount = 0;

                        entity.TotalOrder = detailList.Count;

                        switch (entity.MaterialClassifiedId) {
                            case 1: {
                                foreach (var detail in detailList) {
                                    var import = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                      && t.VendorId == entity.VendorId
                                                                                      && !string.IsNullOrEmpty(t.LotNumber)
                                                                                      ).Select(t => new{
                                                                                          t.Quantity,
                                                                                          t.QuantityKg,
                                                                                          t.UnitWeight,
                                                                                          t.ImportPurchaseOrder.ImportDate,
                                                                                          t.NG,

                                                                                      }).FirstOrDefault();

                                    if (detail.ReceivedQty == 0) {
                                        lateDeliveryCount++;
                                        lessDeliveryCount++;
                                    }

                                    else if (detail.ReceivedQty != 0) {
                                        // 1. Giao hang tre
                                        if (detail.OrderDate < import.ImportDate) {
                                            lateDeliveryCount++;
                                        }

                                        // 2. Giao thieu
                                        if (import.QuantityKg != 0) {
                                            if ((detail.OrderQty * 0.9) > import.QuantityKg) {
                                                lessDeliveryCount++;
                                            }
                                        }
                                        else if (import.QuantityKg == 0) {
                                            var importQuantity = import.Quantity * import.UnitWeight;
                                            if ((detail.OrderQty * 0.9) > importQuantity) {
                                                lessDeliveryCount++;
                                            }
                                        }

                                        // 3, NG 
                                        if (import.NG == true) {
                                            NGTimesCount++;
                                        }

                                    }
                                }
                                break;
                                }


                            case 2: {
                                    foreach (var detail in detailList) {
                                        var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                     && t.VendorId == entity.VendorId
                                                                                     && !string.IsNullOrEmpty(t.LotNumber)
                                                                                     ).Select(t => new {
                                                                                         t.Quantity,
                                                                                         t.TransactionFpt.TransactionDate,
                                                                                         t.NG,
                                                                                     }).FirstOrDefault();
                                        if (detail.ReceivedQty == 0) {
                                            lateDeliveryCount++;
                                            lessDeliveryCount++;
                                        }
                                        else if (detail.ReceivedQty != 0) {
                                            // 1.Late
                                            if (detail.OrderDate < import.TransactionDate) {
                                                lateDeliveryCount++;
                                            }
                                            // 2.Less
                                            if (detail.OrderQty > import.Quantity){
                                                lessDeliveryCount++;
                                            }
                                            // 3. NG
                                            if (import.NG == true){
                                                NGTimesCount++;
                                            }
                                        }
                                    }
                                break;
                            }
                            case 3: {
                                    foreach (var detail in detailList) {
                                        var import = vfi.TransactionFptDetails.Where(t => t.PoDetailId == detail.PurchaseOrderDetailId
                                                                                     && t.VendorId == entity.VendorId
                                                                                     && !string.IsNullOrEmpty(t.LotNumber)
                                                                                     ).Select(t => new {
                                                                                         t.Quantity,
                                                                                         t.TransactionFpt.TransactionDate,
                                                                                         t.NG,
                                                                                     }).FirstOrDefault();
                                        if (detail.ReceivedQty == 0) {
                                            lateDeliveryCount++;
                                            lessDeliveryCount++;
                                        }
                                        else if (detail.ReceivedQty != 0) {
                                            // 1.Late
                                            if (detail.OrderDate < import.TransactionDate) {
                                                lateDeliveryCount++;
                                            }
                                            // 2.Less
                                            if (detail.OrderQty > import.Quantity) {
                                                lessDeliveryCount++;
                                            }
                                            // 3.4. NG
                                            if (import.NG == true) {
                                                NGTimesCount++;
                                            }
                                        }
                                    }
                                    break;
                                }
                        }
                        //1.
                        entity.LateTimes = lateDeliveryCount;

                        // 2.
                        entity.LessTimes = lessDeliveryCount;
 
                        // 3.
                        entity.NGTimes = NGTimesCount;
                        entity.Year = entity.ToDate.Year;
                        entity.NextYear = entity.ToDate.Year + 1;


                        // Commitment
                        var commitmentPerformance = vfi.VendorObjectives.Where(t => t.VendorId == entity.VendorId
                                                                               && t.Year == entity.Year
                                                                               ).Select(t => new {
                                                                                   t.PricePoint,
                                                                                   t.TechSpPoint,

                                                                                   t.LateTimes,
                                                                                   t.LessTimes,
                                                                                   t.NGTimes,
                                                                                   t.PercentNGNumber,
                                                                                   t.TotalPoint,
                                                                               }).FirstOrDefault();
                        if (commitmentPerformance == null) {
                            entity.CommitmentPricePoint = 0;
                            entity.CommitmentTechSpPoint = 0;

                            entity.CommitmentLateTimes = 0;
                            entity.CommitmentLessTimes = 0;
                            entity.CommitmentNGTimes = 0;
                            entity.CommitmentPercentNGQuanity = 0;
                            entity.CommitmentTotalPoint = 0;
                        }
                        else if (commitmentPerformance != null) {
                            entity.CommitmentPricePoint = commitmentPerformance.PricePoint;
                            entity.CommitmentTechSpPoint = commitmentPerformance.TechSpPoint;

                            entity.CommitmentLateTimes = commitmentPerformance.LateTimes;
                            entity.CommitmentLessTimes = commitmentPerformance.LessTimes;
                            entity.CommitmentNGTimes = commitmentPerformance.NGTimes;
                            entity.CommitmentPercentNGQuanity = commitmentPerformance.PercentNGNumber;
                            entity.CommitmentTotalPoint = commitmentPerformance.TotalPoint;
                        }

                        // Objective
                        var objectivePerformance = vfi.VendorObjectives.Where(t => t.VendorId == entity.VendorId
                                                       && t.Year == entity.NextYear
                                                       ).Select(t => new {
                                                           t.PricePoint,
                                                           t.TechSpPoint,

                                                           t.LateTimes,
                                                           t.LessTimes,
                                                           t.NGTimes,
                                                           t.PercentNGNumber,
                                                           t.TotalPoint,
                                                       }).FirstOrDefault();
                        if (objectivePerformance == null) {
                            entity.ObjectivePricePoint = 0;
                            entity.ObjectiveTechSpPoint = 0;

                            entity.ObjectiveLateTimes = 0;
                            entity.ObjectiveLessTimes = 0;
                            entity.ObjectiveNGTimes = 0;
                            entity.ObjectivePercentNGQuanity = 0;
                            entity.ObjectiveTotalPoint = 0;
                        }
                        else if (objectivePerformance != null) {
                            entity.ObjectivePricePoint = objectivePerformance.PricePoint;
                            entity.ObjectiveTechSpPoint = objectivePerformance.TechSpPoint;

                            entity.ObjectiveLateTimes = objectivePerformance.LateTimes;
                            entity.ObjectiveLessTimes = objectivePerformance.LessTimes;
                            entity.ObjectiveNGTimes = objectivePerformance.NGTimes;
                            entity.ObjectivePercentNGQuanity = objectivePerformance.PercentNGNumber;
                            entity.ObjectiveTotalPoint = objectivePerformance.TotalPoint;
                        }

                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                var error = ex.ToString(); // QUAN TRỌNG
                ModelState.AddModelError("PObyId", error);
            }
            return model;
        }


        #endregion



        #region Po
        [GridAction]
        public ActionResult SelectPurchaseOrderDetail() {
            return View(new GridModel(new List<PurchaseOrderDetailModel>()));
        }

        public ActionResult SelectComboBoxReferenceItem(int classified) {
            return SelectComboBoxMaterialByTypeId(classified, 0);
        }

        public ActionResult SelectComboBoxReferenceItemInventory(int classified, int referenceId) {
            try {
                using (var vfi = new tammaContext()) {
                    switch (classified) {
                        case 1:
                            var materialInvs = (from x in vfi.MaterialInventories
                                                where (referenceId == 0 || x.MaterialId == referenceId)
                                                    && x.TotalQty > 0
                                                select new MaterialInventoryModel {
                                                    MaterialInventoryId = x.MaterialInventoryId,
                                                    MaterialName = x.Material.MaterialName,
                                                    OutDiameter = x.Material.OutDiameter,
                                                    InDiameter = x.Material.InDiameter,
                                                    Shape = x.Material.Shape,
                                                    DiameterType = x.Material.DiameterType,
                                                    LotNumber = x.LotNumber,
                                                    Length = x.Length,
                                                }).ToList();
                            return new JsonResult {
                                Data = new SelectList(materialInvs.OrderBy(x => x.MaterialCodeLotNumber), "MaterialInventoryId", "MaterialCodeLotNumber")
                            };
                        default:
                            return new JsonResult {
                                Data = new SelectList(new List<object>())
                            };
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectComboBoxMaterialByVendorId", ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(new List<object>())
            };
        }

        public ActionResult SelectComboBoxMaterialByTypeId(int classified, int typeId) {
            try {
                using (var vfi = new tammaContext()) {
                    switch (classified) {
                        case 1:
                            var materials = (from x in vfi.Materials
                                             where x.Active && (typeId == 0 || x.MaterialTypeId == typeId)
                                             orderby x.MaterialName, x.Shape, x.DiameterType, x.OutDiameter, x.InDiameter
                                             select new { x.MaterialId, x.MaterialCode }).ToList();
                            return new JsonResult {
                                Data = new SelectList(materials, "MaterialId", "MaterialCode")
                            };
                        case 2:
                            var fuels = (from x in vfi.Fuels
                                         where x.Active
                                         orderby x.FuelFullCode
                                         select new { x.FuelId, x.FuelFullCode }
                                        ).ToList();
                            return new JsonResult {
                                Data = new SelectList(fuels, "FuelId", "FuelFullCode")
                            };
                        case 3:
                            var tools = (from x in vfi.Tools
                                         where x.Active && (typeId == 0 || x.MaterialTypeId == typeId)
                                         orderby x.ToolFullCode
                                         select new { x.ToolId, x.ToolFullCode }
                                        ).ToList();
                            return new JsonResult {
                                Data = new SelectList(tools, "ToolId", "ToolFullCode")
                            };
                        case 4:
                            var productionPlatings = (from x in vfi.ProductionPlatings
                                                      where x.Active
                                                      orderby x.Product.ProductCode
                                                      select new ProductionPlatingModel {
                                                          PlatingId = x.PlatingId,
                                                          ProductCode = x.Product.ProductCode,
                                                          PlatingName = x.PlatingName
                                                      }).ToList();
                            return new JsonResult {
                                Data = new SelectList(productionPlatings, "PlatingId", "ProductPlatingCode")
                            };
                        case 6:
                            var products = (from x in vfi.Products
                                            where x.Active
                                            select new { x.ProductId, x.ProductCode }).ToList();

                            return new JsonResult {
                                Data = new SelectList(products, "ProductId", "ProductCode")
                            };
                        default:
                            return new JsonResult {
                                Data = new SelectList(new List<object>())
                            };
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectComboBoxMaterialByVendorId", ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(new List<object>())
            };
        }

        // ban fix lai cho phu hop
        public ActionResult SelectComboBoxVendorByClassified(int classified) {
            try {

                using (var vfi = new tammaContext()) {
                    var vendors =
                        vfi.Vendors.Where(
                        v => v.Active && v.MaterialClassifiedId == classified)
                            .OrderBy(x => x.VendorCode)
                            .Select(x => new VendorModel {
                                VendorId = x.VendorId,
                                VendorName = x.VendorCode + " - " + x.VendorName
                            })
                           .ToList();
                    return new JsonResult {
                        Data = new SelectList(vendors, "VendorId", "VendorName")
                    };

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectComboBoxVendorByClassified", ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(null)
            };
        }


        [GridAction]
        public ActionResult SelectPurchaseOrderByStatus(byte? status, bool? active) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("SelectPurchaseOrderByStatus",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<PurchaseOrderModel>()));
            }
            if (status == null || status == 0) {
                return View(new GridModel(new List<PurchaseOrderModel>()));
            }
            if (active == null) {
                return View(new GridModel(new List<PurchaseOrderModel>()));
            }
            return View(new GridModel(SelectPurchaseOrderView(status, active, "", "", "")));
        }

        private List<PurchaseOrderModel> SelectPurchaseOrderView(byte? status, bool? active, string fromDate,
                                                                 string toDate, string inquiryNumber) {
            var model = new List<PurchaseOrderModel>();
            bool byDate = !string.IsNullOrWhiteSpace(fromDate) && !string.IsNullOrWhiteSpace(toDate);
            var poManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,MyUtilities.UserRole.PurchasingManagement);
            using (var vfi = new tammaContext()) {
                List<PurchaseOrder> list;
                if (byDate) {
                    var toDay = DateTime.Now;
                    var ci = new CultureInfo("vi-VN");
                    var fDate = string.IsNullOrWhiteSpace(fromDate)
                                    ? new DateTime(toDay.Year, toDay.Month, 1)
                                    : Convert.ToDateTime(fromDate, ci);
                    var tDate = string.IsNullOrWhiteSpace(toDate)
                                    ? toDay
                                    : Convert.ToDateTime(toDate, ci);
                    if (active == true) {
                        if (status == (byte)MyUtilities.Sales.Status.Waiting)
                            list = (from po in vfi.PurchaseOrders
                                    where
                                        (po.Status == (byte)MyUtilities.Sales.Status.Waiting
                                        || po.Status == (byte)MyUtilities.Sales.Status.InProcess)
                                        && po.Active == active
                                    select po).ToList();
                        else if (status == (byte)MyUtilities.Sales.Status.Cancel)
                            list = (from po in vfi.PurchaseOrders
                                    where
                                        po.Status == (byte)MyUtilities.Sales.Status.Cancel
                                        && po.Active == active
                                        && ((po.ShipDate != null && po.ShipDate.Value >= fDate && po.ShipDate.Value <= tDate)
                                        || (po.OrderDate >= fDate && po.OrderDate <= tDate))
                                    select po).ToList();
                        else
                            list = (from po in vfi.PurchaseOrders
                                    where
                                        (po.Status == status && po.ShipDate.Value >= fDate && po.ShipDate.Value <= tDate)
                                        && po.Active == active
                                    select po).ToList();
                    }
                    else
                        list = (from po in vfi.PurchaseOrders
                                where po.ShipDate == null && po.Active == active
                                select po).ToList();
                }
                else {
                    list = (from po in vfi.PurchaseOrders
                            where po.Status == status && po.Active == active
                            select po).ToList();
                }

                if (!string.IsNullOrWhiteSpace(inquiryNumber)) {
                    list = list.Where(x => x.PurchaseOrderDetails
                                            .Any(y => y.InquiryPoes
                                                .Any(z => z.InquiryNumber.Contains(inquiryNumber))))
                                            .ToList();
                }
                /*
                 * DeliveryMethod :Method
                 * PackagedMethod:Method1
                 * PaymentMethod:Method2
                 * ShipMethod:Method3
                 */
                model.AddRange(list.Select(po => {
                    var entity = new PurchaseOrderModel {
                        PurchaseOrderId = po.PurchaseOrderId,
                        ModifiedUser = po.ModifiedUser,
                        ModifiedDate = po.ModifiedDate,
                        //EmployeeName = po.EmployeeName.ToUpper(),
                        TotalQuality = po.PurchaseOrderDetails.Sum(pod => pod.OrderQty),
                        ShipDate = po.ShipDate,
                        Note = po.Note,
                        StatusName = MyUtilities.Sales.GetText(po.Status),
                        VendorCode = po.Vendor.VendorCode,
                        OrderDate = po.OrderDate,
                        VendorName = po.Vendor.VendorName,
                        RevisionNumber = po.RevisionNumber,
                        CurrencyCode = po.CurrencyCode,
                        Tolerance = po.Tolerance,
                        MaterialClasstifiedId = po.MaterialClassifiedId,
                        MaterialClasstifiedName = po.MaterialClassified.MaterialClassifiedName,
                        ContractNumber = po.ContractNumber,
                        Active = poManager,
                        AddressId = po.AddressId,
                        Address = po.DeliveryAddress.Address,
                        AddressName = po.DeliveryAddress.AddressShortName,
                        AddressFullName = po.DeliveryAddress.AddressName,
                        PaymentMethodId = po.PaymentId ?? 0,
                        DeliveryMethodId = po.ConditionDeliveryId ?? 0,
                        ShipMethodId = po.DeliveryById ?? 0,
                        PaymentMethodName = vfi.Methods.Where(t => t.MethodId == po.PaymentId).Select(t => t.MethodName_EN).FirstOrDefault(),
                        ShipMethodName = vfi.Methods.Where(t => t.MethodId == po.DeliveryById).Select(t => t.MethodName_EN).FirstOrDefault(),
                        DeliveryMethodName = vfi.Methods.Where(t => t.MethodId == po.ConditionDeliveryId).Select(t => t.MethodName_EN).FirstOrDefault(),
                        Approve = false,
                        BillToId = po.BillToId,
                        BillOfLanding = po.BillOfLanding,

                    };
                    if (entity.BillToId == 0) {
                        entity.BillToId = 1;
                    }
                    var billToList = vfi.DeliveryAddresses.FirstOrDefault(t => t.AddressId == entity.BillToId);
                    entity.BillTo = billToList.AddressShortName;
                    entity.BillToFullName = billToList.AddressName;
                    entity.BillToAddress = billToList.Address;
                    entity.ReceiptBill = billToList.Recipient;
                    entity.ReceiptBillPhoneNumber = billToList.Telephone;
                    
                    


                    if (!string.IsNullOrWhiteSpace(po.EmployeeName)) {
                        entity.EmployeeName = po.EmployeeName.ToUpper();
                    }
                    if (!string.IsNullOrWhiteSpace(entity.DeliveryMethodName)
                        && !string.IsNullOrWhiteSpace(entity.ShipMethodName)
                        && !string.IsNullOrWhiteSpace(entity.PaymentMethodName)
                        && !string.IsNullOrWhiteSpace(entity.AddressName)
                        //&& entity.ShipDate > DateTime.Now.Date
                        && !string.IsNullOrWhiteSpace(entity.EmployeeName)
                        ) {
                        entity.Approve = true;
                    }
                    return entity;
                }));

            }
            return model;
        }

        List<PurchaseOrderDetailModel> GetPurchaseOrderDetailModel(long purchaseOrderId) {
            var model = new List<PurchaseOrderDetailModel>();
            if (purchaseOrderId == 0) return model;
            try {
                var manager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.PurchasingManagement);
                using (var vfi = new tammaContext()) {
                    var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == purchaseOrderId);
                    if (purchaseOrder != null) {
                        var info = new WorkGroupInfo();
                        var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.Active);
                        if (workgroup != null) {
                            info = new WorkGroupInfo {
                                //Logo = workgroup.ImagePath + "/Logo/" + workgroup.LogoImage
                                //CompanyFullName = workgroup.CompanyFullName,
                                //CompanyShortName = workgroup.CompanyShortName,
                                //Address = workgroup.Address,
                                //TelNumber = "Tel : " + workgroup.TelNumber,
                                //FaxNumber = "Fax : " + workgroup.FaxNumber,
                                //Email = "Email: " + workgroup.Email,
                                //Website = "Website: " + workgroup.Website
                                Logo = workgroup.ImagePath + "/Logo/" +
                                            (workgroup.LogoImage == "Logo-AVF.png"
                                                ? "Logo-AVF-New.jpg"
                                                : workgroup.LogoImage
                                            ),
                            };

                        }
                        foreach (var poDetail in purchaseOrder.PurchaseOrderDetails) {
                            var entity = new PurchaseOrderDetailModel {
                                PurchaseOrderDetailId = poDetail.PurchaseOrderDetailId,
                                PurchaseOrderId = poDetail.PurchaseOrderId,
                                OrderQty = poDetail.OrderQty,
                                UnitPrice = poDetail.UnitPrice,
                                LineTotal = poDetail.OrderQty * poDetail.UnitPrice,
                                ReceivedQty = poDetail.ReceivedQty,
                                RejectedQty = poDetail.RejectedQty,
                                DueDate = poDetail.DueDate,
                                Unit = poDetail.Unit,
                                VendorCode = purchaseOrder.Vendor.VendorCode,
                                VendorName = purchaseOrder.Vendor.ShortName,
                                CurrencyCode = purchaseOrder.CurrencyCode.Trim(),
                                PurchaseOrderNumber = purchaseOrder.RevisionNumber,
                                ClassifiedName = purchaseOrder.MaterialClassified.MaterialClassifiedName.ToUpper(),
                                ModifiedUser = purchaseOrder.ModifiedUser,
                                StatusName = MyUtilities.Transaction.CastText.GetTextStatus(purchaseOrder.Status),
                                PurchaseDateTime = purchaseOrder.ShipDate ?? purchaseOrder.OrderDate,
                                IsPurchaseManager = manager,
                                Info = info,
                                Address = purchaseOrder.DeliveryAddress.Address,
                                AddressFullName = purchaseOrder.DeliveryAddress.AddressName,
                                Standard = poDetail.Standard,
                                OrderDate = purchaseOrder.ShipDate,
                                VendorNo = purchaseOrder.VendorId,
                                Tel = purchaseOrder.DeliveryAddress.Telephone,
                                Recipient = purchaseOrder.DeliveryAddress.Recipient,
                                QuotationNumber = purchaseOrder.ContractNumber,
                                ManagerNote = poDetail.ManagerNote,
                                
                                
                            };
                            // Vendor 
                            if (purchaseOrder.VendorId == 0) {
                                throw new AggregateException("VendorID");
                            }
                            var vendor = vfi.Vendors.Where(t => t.VendorId == entity.VendorNo).Select(t => new {
                                t.Address,
                                t.Phone,
                                t.CompanyName,
                                t.Email,
                                t.ContactName,
                            }).FirstOrDefault();

                            entity.VendorAddress = vendor.Address;
                            entity.VendorPhone = vendor.Phone;
                            entity.VendorCompanyName = vendor.CompanyName;
                            entity.VendorContactName = vendor.ContactName;

                            if (purchaseOrder.PaymentId != null) {
                                entity.PaymentMethodName = vfi.Methods.Where(t => t.MethodId == purchaseOrder.PaymentId).Select(t => t.MethodName_EN).FirstOrDefault().ToUpper();
                            }
                            if (purchaseOrder.DeliveryById != null) {
                                entity.ShipMethodName = vfi.Methods.Where(t => t.MethodId == purchaseOrder.DeliveryById).Select(t => t.MethodName_EN).FirstOrDefault().ToUpper();
                            }
                            if (purchaseOrder.ConditionDeliveryId != null) {
                                entity.DeliveryMethodName = vfi.Methods.Where(t => t.MethodId == purchaseOrder.ConditionDeliveryId).Select(t => t.MethodName_EN).FirstOrDefault().ToUpper();
                            }

                            if (!string.IsNullOrWhiteSpace(purchaseOrder.EmployeeName)){
                                entity.Employee = purchaseOrder.EmployeeName.ToUpper();
                            }


                            entity.Year = entity.PurchaseOrderNumber.Substring(7,4) + "-20" + entity.PurchaseOrderNumber.Substring(5, 2);

                            entity.Active = purchaseOrder.Active;

                            if (purchaseOrder.BillToId == 0) {
                                purchaseOrder.BillToId = 1;
                            }
                            entity.BillToId = purchaseOrder.BillToId;
                            var billToList = vfi.DeliveryAddresses.FirstOrDefault(t => t.AddressId == entity.BillToId);
                            entity.BillToFullName = billToList.AddressName;
                            entity.BillToAddress = billToList.Address;
                            entity.ReceiptBill = billToList.Recipient;
                            entity.ReceiptBillPhoneNumber = billToList.Telephone;


                            entity.RequireQty = entity.OrderQty - (entity.ReceivedQty + entity.RejectedQty);
                            if (poDetail.InquiryPoes.Any(x => x.Status == (byte)MyUtilities.PurchaseOrder.InquiryEnum.MakePo)) {
                                entity.Inquiry = poDetail.InquiryPoes.Where(x => x.Status == (byte)MyUtilities.PurchaseOrder.InquiryEnum.MakePo)
                                    .Select(x => new InquiryPoModel {
                                        InquiryNumber = x.InquiryNumber,
                                        DueDate = x.DueDate,
                                    })
                                    .FirstOrDefault();
                            }
                                                                                        
                            if (poDetail.InquiryPoes.Any(t => t.Status == (byte)MyUtilities.PurchaseOrder.InquiryEnum.MakePo)) {
                                
                                var inquiryPo = poDetail.InquiryPoes.Where(t => t.Status == (byte)MyUtilities.PurchaseOrder.InquiryEnum.MakePo)
                                                                          .Select(t => new {
                                                                              t.Standard,
                                                                              t.ModifiedUser,
                                                                            })
                                                                          .FirstOrDefault();
                                entity.Creater = vfi.Users.Where(t => t.Username == inquiryPo.ModifiedUser).Select(t => t.FullName).FirstOrDefault().ToUpper();
                                if (poDetail.Standard == "") {

                                    entity.Standard = inquiryPo.Standard;
                                }
                                else {
                                    entity.Standard = poDetail.Standard;
                                }
                            }

                            else {
                                entity.Standard = poDetail.Standard;
                                entity.Creater = vfi.Users.Where(t => t.Username == poDetail.ModifiedUser).Select(t => t.FullName).FirstOrDefault().ToUpper() ;
                            }

                            
                            entity.CurrencyCode = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrderDetailId == entity.PurchaseOrderDetailId).Select(t => t.PurchaseOrder.CurrencyCode).FirstOrDefault();
                            if (entity.RequireQty < 0)
                                entity.RequireQty = 0;
                            switch (purchaseOrder.MaterialClassifiedId) {
                                case 1:
                                    var material =
                                        vfi.Materials.FirstOrDefault(m => m.MaterialId == poDetail.ReferenceId);
                                    if (material == null)
                                        throw new AggregateException("Lỗi! Không tìm thấy nguyên liệu");
                                    entity.Name = material.MaterialName;
                                    entity.Code = material.MaterialCode;
                                    entity.MaterialModel = new MaterialModel {
                                        MaterialId = material.MaterialId,
                                        MaterialName = material.MaterialName,
                                        MaterialCode = material.MaterialCode,
                                        OutDiameter = material.OutDiameter,
                                        InDiameter = material.InDiameter,
                                        Shape = material.Shape,
                                        DiameterType = material.DiameterType,
                                    };
                                    entity.Note = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrderDetailId == entity.PurchaseOrderDetailId).Select(t => t.Note).FirstOrDefault();
                                    break;
                                case 2:
                                    var fuel =
                                        vfi.Fuels.FirstOrDefault(m => m.FuelId == poDetail.ReferenceId);
                                    if (fuel == null)
                                        throw new AggregateException("Lỗi! Không tìm thấy nhiên liệu");
                                    entity.Name = fuel.FuelName;
                                    entity.Code = fuel.FuelFullCode;
                                    entity.FuelId = fuel.FuelId;
                                    entity.Note = vfi.TransactionFptDetails.Where(t => t.PoDetailId == entity.PurchaseOrderDetailId).Select(t => t.Note).FirstOrDefault();
                                    break;
                                case 3:
                                    var tool =
                                        vfi.Tools.FirstOrDefault(m => m.ToolId == poDetail.ReferenceId);
                                    if (tool == null)
                                        throw new AggregateException("Lỗi! Không tìm thấy công cụ");
                                    entity.Name = tool.ToolName;
                                    entity.Code = tool.ToolFullCode;
                                    entity.ToolId = tool.ToolId;
                                    entity.Note = vfi.TransactionFptDetails.Where(t => t.PoDetailId == entity.PurchaseOrderDetailId).Select(t => t.Note).FirstOrDefault();

                                    break;
                                case 6:
                                    var product =
                                        vfi.Products.FirstOrDefault(m => m.ProductId == poDetail.ReferenceId);
                                    if (product == null)
                                        throw new AggregateException("Lỗi! Không tìm thấy sản phẩm");
                                    entity.Name = product.ProductName;
                                    entity.Code = product.ProductCode;
                                    break;
                            }
                            if (purchaseOrder.ShipDate == null) {
                                var lastPo = (from pod in vfi.PurchaseOrderDetails
                                              where pod.PurchaseOrder.ShipDate != null &&
                                              pod.PurchaseOrder.ShipDate < DateTime.Now &&
                                              pod.PurchaseOrder.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                              pod.ReferenceId == poDetail.ReferenceId &&
                                              pod.MaterialClassifiedId == poDetail.MaterialClassifiedId &&
                                              pod.UnitPrice != 0
                                              orderby pod.PurchaseOrder.ShipDate descending
                                              select pod).FirstOrDefault();
                                if (lastPo != null) {
                                    entity.LastPrice = lastPo.UnitPrice;
                                    entity.LastPoDate = lastPo.PurchaseOrder.ShipDate.Value;
                                }
                                var smallestPo = (from pod in vfi.PurchaseOrderDetails
                                                  where pod.PurchaseOrder.ShipDate != null &&
                                                  pod.PurchaseOrder.ShipDate < DateTime.Now &&
                                                  pod.PurchaseOrder.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                                  pod.ReferenceId == poDetail.ReferenceId &&
                                                  pod.MaterialClassifiedId == poDetail.MaterialClassifiedId &&
                                                  pod.UnitPrice != 0
                                                  orderby pod.UnitPrice, pod.PurchaseOrder.ShipDate descending
                                                  select pod).FirstOrDefault();
                                if (smallestPo != null) {
                                    entity.SmallestPrice = smallestPo.UnitPrice;
                                    entity.SmallestPricePoDate = smallestPo.PurchaseOrder.ShipDate.Value;
                                }


                                //entity.SpecificNote = entity.LastPrice + entity.SmallestPrice > 0
                                //    ? entity.LastPrice == entity.SmallestPrice
                                //        ? "ĐG gần nhất nhỏ nhất:" + entity.LastPrice + "-" + entity.LastPoDate.ToString("dd/MM/yyyy")
                                //        : entity.LastPrice > 0 && entity.SmallestPrice > 0
                                //            ? "ĐG gần nhất:" + entity.LastPrice + "-" + entity.LastPoDate.ToString("dd/MM/yyyy")
                                //                + "| ĐG nhỏ nhất:" + entity.SmallestPrice + "-" + entity.SmallestPricePoDate.ToString("dd/MM/yyyy")
                                //            : entity.LastPrice > 0
                                //                ? "ĐG gần nhất:" + entity.LastPrice + "-" + entity.LastPoDate.ToString("dd/MM/yyyy")
                                //                : "ĐG nhỏ nhất:" + entity.SmallestPrice + "-" + entity.SmallestPricePoDate.ToString("dd/MM/yyyy")
                                //    : "";
                            }

                            if (string.IsNullOrWhiteSpace(entity.Note) && !string.IsNullOrWhiteSpace(poDetail.Note)) {
                                entity.Note = poDetail.Note;
                            }
                            else if (!string.IsNullOrWhiteSpace(entity.Note) && !string.IsNullOrWhiteSpace(poDetail.Note)) {
                                entity.Note = entity.Note + " - " + poDetail.Note;
                            }
                            model.Add(entity);
                        }
                        model = purchaseOrder.MaterialClassifiedId == 1
                                    ? model.OrderBy(m => m.MaterialModel.MaterialName)
                                           .ThenBy(m => m.MaterialModel.Shape)
                                           .ThenBy(m => m.MaterialModel.DiameterType)
                                           .ThenBy(m => m.MaterialModel.OutDiameter)
                                           .ThenBy(m => m.MaterialModel.InDiameter)
                                           .ToList()
                                    : model.OrderBy(m => m.Code).ToList();
                    }
                }
            }
            catch (Exception ex) {
                var error = ex.ToString(); // QUAN TRỌNG
                ModelState.AddModelError("PObyId", error);
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectPurchaseOrderDetailByPOId(int purchaseOrderId) {
            var model = new List<PurchaseOrderDetailModel>();
            if (purchaseOrderId == 0) return View(new GridModel(model));
            try {
                model = GetPurchaseOrderDetailModel(purchaseOrderId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("PObyId", ex.Message);
            }
            return View(new GridModel(model));
        }


        //UpdateApprovedPo
        [GridAction]
        public ActionResult UpdatePurchaseOrderDetail(PurchaseOrderDetailModel update) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdatePurchaseOrderDetail",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<PurchaseOrderDetailModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var poDetail =
                        vfi.PurchaseOrderDetails.FirstOrDefault(
                            pod => pod.PurchaseOrderDetailId == update.PurchaseOrderDetailId);
                    if (poDetail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết cần sửa");
                    update.PurchaseOrderId = poDetail.PurchaseOrderId;
                    var po = poDetail.PurchaseOrder;
                    if (poDetail.ReceivedQty + poDetail.RejectedQty > 0) {
                        switch (po.MaterialClassifiedId) {
                            case 1: {
                                    var importDetails1 =
                                        vfi.ImportPurchaseOrderDetails
                                            .Where(td => td.PoDetailId == update.PurchaseOrderDetailId);
                                    foreach (var importDetail in importDetails1) {
                                        if (importDetail.PoReferenceDetailId != null)
                                            throw new AggregateException("Lỗi! Chi tiết đã có xuất hoá đơn!");
                                        if (importDetail.ImportPurchaseOrder.Transaction.Status
                                            == (byte)MyUtilities.Transaction.Status.Approved) {
                                            var materialInv = vfi.MaterialInventories
                                                .FirstOrDefault(ti => ti.MaterialId == importDetail.MaterialId &&
                                                                      ti.LotNumber.Equals(importDetail.LotNumber) &&
                                                                      ti.VendorId == importDetail.VendorId);
                                            if (materialInv != null)
                                                materialInv.UnitPrice = update.UnitPrice;
                                        }
                                        importDetail.UnitPrice = update.UnitPrice;

                                    }
                                    break;
                                }
                            case 2: {
                                    var importDetails2 =
                                        vfi.TransactionFptDetails
                                            .Where(td => td.PoDetailId == update.PurchaseOrderDetailId);
                                    foreach (var importDetail in importDetails2) {
                                        if (importDetail.PoReferenceDetailId != null)
                                            throw new AggregateException("Lỗi! Chi tiết đã có xuất hoá đơn!");
                                        if (importDetail.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved) {
                                            var fuelInv = vfi.FuelInventories
                                                .FirstOrDefault(ti => ti.FuelId == importDetail.FptId &&
                                                                      ti.LotNumber.Equals(importDetail.LotNumber) &&
                                                                      ti.VendorId == importDetail.VendorId);
                                            if (fuelInv != null)
                                                fuelInv.UnitPrice = update.UnitPrice;
                                        }
                                        importDetail.UnitPrice = update.UnitPrice;
                                    }
                                    break;
                                }
                            case 3: {
                                    var importDetails3 =
                                        vfi.TransactionFptDetails
                                            .Where(td => td.PoDetailId == update.PurchaseOrderDetailId);
                                    foreach (var importDetail in importDetails3) {
                                        if (importDetail.PoReferenceDetailId != null)
                                            throw new AggregateException("Lỗi! Chi tiết đã có xuất hoá đơn!");
                                        if (importDetail.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved) {
                                            var toolInv = vfi.ToolInventories
                                                .FirstOrDefault(ti => ti.ToolId == importDetail.FptId &&
                                                                      ti.LotNumber.Equals(importDetail.LotNumber) &&
                                                                      ti.VendorId == importDetail.VendorId);
                                            if (toolInv != null)
                                                toolInv.UnitPrice = update.UnitPrice;
                                        }
                                        importDetail.UnitPrice = update.UnitPrice;
                                    }
                                    break;
                                }
                        }
                    }

                    if (poDetail.Standard != update.Standard && update.Standard != null) {
                        poDetail.Standard = update.Standard;
                    }
                    //if (update.Note != poDetail.Note && update.Note != null) {
                    //    poDetail.Note = update.Note;
                    //}
                    if (update.OrderQty != poDetail.OrderQty && update.OrderQty != 0) {
                        poDetail.OrderQty = update.OrderQty;
                    }

                    if (update.ManagerNote != poDetail.ManagerNote && update.ManagerNote != null) {
                        poDetail.ManagerNote = update.ManagerNote;
                    }
                    if (update.UnitPrice != 0 && update.UnitPrice != poDetail.UnitPrice) {
                        poDetail.UnitPrice = update.UnitPrice;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdatePurchaseOrderDetail", ex.Message);
            }
            return View(new GridModel(GetPurchaseOrderDetailModel(update.PurchaseOrderId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateApprovePurchaseOrder(int purchaseOrderId, string shipDate, string addressName,string billTo, string contractNumber, int? paymentMethodId, int? shipMethodId, int? deliveryMethodId, string note, string employeeName) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateApprovePurchaseOrder",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(SelectPurchaseOrderView(1, false, "", "", "")));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var po = vfi.PurchaseOrders.FirstOrDefault(p => p.PurchaseOrderId == purchaseOrderId);
                    if (po != null) {
                        //if (po.Active){
                            //throw new AggregateException("Phiếu này đã được duyệt rồi !");
                        //}
                        po.ShipDate = Convert.ToDateTime(shipDate);
                        //po.Active = true;
                        if (addressName != po.DeliveryAddress.AddressShortName) {
                            var addressId = Convert.ToInt32(addressName);
                            po.AddressId = addressId;
                        }
                        if (billTo != po.DeliveryAddress.AddressShortName && !string.IsNullOrWhiteSpace(billTo)) {
                            var billToId = Convert.ToInt32(billTo);
                            po.BillToId = billToId;
                        }

                        if (!string.IsNullOrWhiteSpace(note)){
                            po.Note = note;
                        }

                        if (!string.IsNullOrWhiteSpace(employeeName)){
                            if (po.EmployeeName != employeeName)
                            po.EmployeeName = employeeName.ToUpper();
                        }


                        if (!string.IsNullOrWhiteSpace(contractNumber)) {
                            if (po.ContractNumber != contractNumber) {
                                po.ContractNumber = contractNumber;
                            }
                        }

                        if (paymentMethodId != null) {
                            if (po.PaymentId != paymentMethodId) {
                                po.PaymentId = paymentMethodId;
                            }
                            else if(po.PaymentId == paymentMethodId) {
                                po.PaymentId = po.PaymentId;
                            }
                        }

                        if (shipMethodId != null) {
                            if (po.DeliveryById != shipMethodId) {
                                po.DeliveryById = shipMethodId;
                            }
                        }

                        if (deliveryMethodId != null) {
                            if (po.ConditionDeliveryId != deliveryMethodId) {
                                po.ConditionDeliveryId = deliveryMethodId;
                            }
                        }

                        vfi.SaveChanges();
                        ModelState.AddModelError("UpdateApprovePurchaseOrder", "Đã cập nhật dữ liệu. Vui lòng làm mới lại bảng dữ liệu!");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateApprovePurchaseOrder", ex.Message);

            }
            return View(new GridModel(SelectPurchaseOrderView(1, false, "", "", "")));

        }

        // Duyet Phieu Mua
        [HttpPost]
        [GridAction]
        public ActionResult ApprovePO(int purchaseOrderId) {
            try {
                using (var vfi = new tammaContext()) {
                    var po = vfi.PurchaseOrders.FirstOrDefault(p => p.PurchaseOrderId == purchaseOrderId);
                    if (po != null) {
                        if (po.Active)
                            throw new AggregateException("Phiếu này đã được duyệt rồi !");
                        if (po.ShipDate == null)
                            throw new AggregateException("Cập nhật ngày giao.");
                        if (po.AddressId == 0 || po.PaymentId == 0 || po.DeliveryById == 0 || po.ConditionDeliveryId == 0)
                            throw new AggregateException("Hãy cập nhật đầy đủ thông tin trước khi duyệt");
                        po.Active = true;
                        vfi.SaveChanges();
                        ModelState.AddModelError("ApprovePO", "Đã duyệt PO, vui lòng làm mới lại dữ liệu. ");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ApprovePO", ex.Message);
            }
            return View(new GridModel(SelectPurchaseOrderView(1, false, "", "", "")));
        }


        public ActionResult SplitPurchaseOrderDetails(long purchaseOrderId, long[] detailIds) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("SplitPurchaseOrderDetails",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                //return View(new GridModel(new List<PurchaseOrderDetailModel>()));
                return Json(0);
            }
            try {
                using (var vfi = new tammaContext()) {
                    var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == purchaseOrderId);
                    if (purchaseOrder == null) return Json(0);
                    if (purchaseOrder.PurchaseOrderDetails.Count == detailIds.Length) return Json(2);
                    var details =
                        purchaseOrder.PurchaseOrderDetails.Where(
                            detail => detailIds.Contains(detail.PurchaseOrderDetailId)).ToList();
                    if (details.Count > 0) {
                        var newPO = new PurchaseOrder {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            EmployeeName = purchaseOrder.EmployeeName,
                            VendorId = purchaseOrder.VendorId,
                            ShipDate = null,
                            OrderDate = DateTime.Now,
                            RevisionNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.PurchaseOrder, 1),
                            Active = false,
                            Status = (byte)MyUtilities.Sales.Status.Waiting,
                            Tolerance = purchaseOrder.Tolerance,
                            Note = purchaseOrder.Note,
                            CurrencyCode = purchaseOrder.CurrencyCode,
                        };
                        vfi.PurchaseOrders.Add(newPO);
                        vfi.SaveChanges();
                        foreach (var detail in details) {
                            detail.PurchaseOrderId = newPO.PurchaseOrderId;
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
        public ActionResult CancelPurchaseOrder(int purchaseOrderId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("SplitPurchaseOrderDetails",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(SelectPurchaseOrderView(1, false, "", "","")));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var po = vfi.PurchaseOrders.FirstOrDefault(p => p.PurchaseOrderId == purchaseOrderId);
                    if (po != null) {
                        if (po.Active)
                            throw new AggregateException("Phiếu này đã được duyệt rồi !");
                        po.Status = (byte)MyUtilities.Sales.Status.Cancel;
                        po.Active = true;
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ApprovePObyId", ex.Message);

            }
            return View(new GridModel(SelectPurchaseOrderView(1, false, "", "","")));
        }


        [HttpPost]
        public ActionResult PrintPurchaseOrderTracking(int classifiedId, string fromDate, string toDate, string inquiryNumber) {
            var model = new List<GroupPoTracking>();
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseDate(toDate);
            try {
                using (var vfi = new tammaContext()) {
                    var purchaseOrderIds = (from po in vfi.PurchaseOrders
                                            where po.Active && po.ShipDate != null
                                                  && (classifiedId == 0 || po.MaterialClassifiedId == classifiedId)
                                                  && po.ShipDate >= fDate
                                                  && po.ShipDate <= tDate
                                                  //&& po.ShipDate <= tDate
                                                  //&& po.Status != (byte)MyUtilities.Sales.Status.Completed
                                                  && po.Status != (byte)MyUtilities.Sales.Status.Cancel
                                            select po.PurchaseOrderId).ToList();
                    //var purchaseOrderIds = purchaseOrders.Select(po => po.PurchaseOrderId).ToList();
                    var importPoInMonth = (from pod in vfi.ImportPurchaseOrders
                                           where pod.PurchaseOrderId != null &&
                                                 pod.Transaction.Status ==
                                                 (byte)MyUtilities.Transaction.Status.Approved &&
                                                 pod.Transaction.CreatedDate >= fDate &&
                                                 pod.Transaction.CreatedDate <= tDate
                                                  && (classifiedId == 0 || pod.PurchaseOrder.MaterialClassifiedId == classifiedId)
                                           select pod.PurchaseOrderId.Value).ToList();
                    var transactionInMonth = (from td in vfi.TransactionFpts
                                              where
                                                  td.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                  td.PoId != null &&
                                                  td.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import &&
                                                  td.TransactionDate >= fDate &&
                                                  td.TransactionDate <= tDate
                                                  && (classifiedId == 0 || td.PurchaseOrder.MaterialClassifiedId == classifiedId)
                                              select td.PoId.Value).ToList();
                    purchaseOrderIds = purchaseOrderIds.Union(importPoInMonth).Union(transactionInMonth).Distinct().ToList();

                    var purchaseOrders = (from x in vfi.PurchaseOrders
                                          where purchaseOrderIds.Contains(x.PurchaseOrderId)
                                          select new {
                                              x.PurchaseOrderId,
                                              x.Status,
                                              x.MaterialClassifiedId,
                                              x.MaterialClassified.MaterialClassifiedName,
                                              x.VendorId,
                                              x.Vendor.VendorName,
                                              x.CurrencyCode,
                                              x.ShipDate,
                                              x.OrderDate,
                                              x.PurchaseOrderDetails,
                                          }).ToList();
                    var importMaterials = (from x in vfi.ImportPurchaseOrderDetails
                                           where x.ImportPurchaseOrder.PurchaseOrderId != null &&
                                                 purchaseOrderIds.Contains(x.ImportPurchaseOrder.PurchaseOrderId.Value) &&
                                                 x.ImportPurchaseOrder.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                               //x.ImportPurchaseOrder.Transaction.CreatedDate >= fDate &&
                                                 x.ImportPurchaseOrder.Transaction.CreatedDate <= tDate
                                           select new {
                                               x.ImportPurchaseOrder.PurchaseOrderId,
                                               x.MaterialId,
                                               x.Quantity,
                                               x.QuantityKg,
                                               x.ImportPurchaseOrder.ImportDate,
                                               x.ImportPurchaseOrder.ExchangeRate,
                                           }).ToList();
                    var imports = (from x in vfi.TransactionFptDetails
                                   where
                                       x.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                       x.TransactionFpt.PoId != null &&
                                       purchaseOrderIds.Contains(x.TransactionFpt.PoId.Value) &&
                                       x.TransactionFpt.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import &&
                                       x.TransactionFpt.TransactionDate <= tDate
                                   select new {
                                       x.TransactionFpt.PoId,
                                       x.FptId,
                                       x.Quantity,
                                       x.TransactionFpt.TransactionDate,
                                       x.TransactionFpt.ExchangeRate,
                                   }).ToList();
                    var exchangeRate = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.ExchangeToVndRate);
                    var classifieds = purchaseOrders.Select(x => new { x.MaterialClassifiedId, x.MaterialClassifiedName }).Distinct().ToList();
                    foreach (var classified in classifieds) {
                        var group = new GroupPoTracking {
                            TypeName = classified.MaterialClassifiedName,
                            ReportDate = tDate,
                            //ReportDateStr = fDate.ToString("dd/MM/yy") + "-" + tDate.ToString("dd/MM/yy"),
                            ReportDateStr = tDate.ToString("dd/MM"),
                        };
                        var purchaseOrdersById = purchaseOrders.Where(po => po.MaterialClassifiedId == classified.MaterialClassifiedId);
                        var vendorIds = purchaseOrdersById.Select(po => po.VendorId).Distinct().ToList();
                        var index = 1;
                        foreach (var vendorId in vendorIds) {
                            //var vendor = vfi.Vendors.FirstOrDefault(v => v.VendorId == vendorId);
                            var purchaseOrderByVendor = purchaseOrdersById.Where(po => po.VendorId == vendorId);
                            foreach (var purchaseOrder in purchaseOrderByVendor) {
                                var entity = new PoTracking {
                                    VendorId = vendorId,
                                    VendorName = purchaseOrder.VendorName,
                                    PoDate = purchaseOrder.ShipDate != null ? purchaseOrder.ShipDate.Value : purchaseOrder.OrderDate,
                                    CreateDate = purchaseOrder.OrderDate
                                };
                                foreach (var poDetail in purchaseOrder.PurchaseOrderDetails) {
                                    var detail = new PoTrackingDetail {
                                        Currency = purchaseOrder.CurrencyCode.Trim(),
                                        Quantity = poDetail.OrderQty,
                                        UnitPrice = poDetail.UnitPrice,
                                        ExchangeRate = 1,
                                        UnitMeasure = poDetail.Unit,
                                        Index = index++,
                                        //PoDate = entity.PoDate,
                                        //CreateDate = entity.CreateDate,
                                        PoDateStr = entity.PoDate.ToString("dd/MM/yyyy"),
                                        CreateDateStr = entity.CreateDate.ToString("dd/MM/yyyy"),  
                                    };
                                    if (detail.Currency.Equals("USD"))
                                        detail.ExchangeRate = exchangeRate;
                                    else if (detail.Currency.Equals("EUR"))
                                        detail.ExchangeRate = 25000;
                                    switch (classified.MaterialClassifiedId) {
                                        case 1:
                                            var material =
                                                vfi.Materials.FirstOrDefault(m => m.MaterialId == poDetail.ReferenceId);
                                            if (material == null)
                                                throw new AggregateException("Lỗi! Không tìm thấy nguyên liệu");
                                            detail.PoDetailName = material.MaterialName;
                                            detail.PoDetailDesign =
                                                MyUtilities.Material.GetMaterialDesignNo(material.OutDiameter,
                                                                                         material.InDiameter,
                                                                                         material.DiameterType,
                                                                                         material.Shape);
                                            var importPoFirst =
                                                importMaterials.Where(
                                                    i =>
                                                    i.PurchaseOrderId == purchaseOrder.PurchaseOrderId &&
                                                    i.ImportDate < fDate &&
                                                    i.MaterialId == poDetail.ReferenceId).ToList();
                                            if (importPoFirst.Any())
                                                detail.Quantity -= importPoFirst.Sum(id => id.QuantityKg);
                                            if (detail.Quantity < 0) {
                                                detail.Quantity = 0;
                                                continue;
                                            }

                                            var importPos =
                                                importMaterials.Where(
                                                    i =>
                                                    i.PurchaseOrderId == purchaseOrder.PurchaseOrderId &&
                                                    i.ImportDate >= fDate &&
                                                    i.ImportDate <= tDate &&
                                                    i.MaterialId == poDetail.ReferenceId).ToList();
                                            foreach (var importPoDetail in importPos) {
                                                var importDetail = new PoTrackingImportDetail {
                                                    ImportDateString = importPoDetail.ImportDate.ToString("dd/MM"),
                                                    ImportQuantity = importPoDetail.QuantityKg,
                                                    UnitPrice = detail.UnitPrice,
                                                    ExchangeRate = importPoDetail.ExchangeRate,
                                                    Currency = detail.Currency
                                                };
                                                detail.ImportDetails.Add(importDetail);
                                            }

                                            break;
                                        case 2:
                                            var fuel =
                                                vfi.Fuels.FirstOrDefault(m => m.FuelId == poDetail.ReferenceId);
                                            if (fuel == null)
                                                throw new AggregateException("Lỗi! Không tìm thấy nhiên liệu");
                                            detail.PoDetailName = fuel.FuelName;
                                            detail.PoDetailDesign = fuel.FuelDesignNo;

                                            var transactionFuelFirsts = imports.Where(x => x.PoId == purchaseOrder.PurchaseOrderId
                                                                                        && x.FptId == poDetail.ReferenceId
                                                                                        && x.TransactionDate < fDate).ToList();
                                            if (transactionFuelFirsts.Any())
                                                detail.Quantity -= transactionFuelFirsts.Sum(id => id.Quantity);
                                            if (detail.Quantity < 0) {
                                                detail.Quantity = 0;
                                                continue;
                                            }

                                            var transactionFuels = imports.Where(x => x.PoId == purchaseOrder.PurchaseOrderId
                                                                                        && x.FptId == poDetail.ReferenceId
                                                                                        && x.TransactionDate >= fDate
                                                                                        && x.TransactionDate <= tDate).ToList();
                                            foreach (var importPoDetail in transactionFuels) {
                                                var importDetail = new PoTrackingImportDetail {
                                                    ImportDateString = importPoDetail.TransactionDate.ToString("dd/MM"),
                                                    ImportQuantity = importPoDetail.Quantity,
                                                    UnitPrice = detail.UnitPrice,
                                                    ExchangeRate = importPoDetail.ExchangeRate,
                                                    Currency = detail.Currency
                                                };
                                                detail.ImportDetails.Add(importDetail);
                                            }
                                            break;
                                        case 3:
                                            var tool =
                                                vfi.Tools.FirstOrDefault(m => m.ToolId == poDetail.ReferenceId);
                                            if (tool == null)
                                                throw new AggregateException("Lỗi! Không tìm thấy công cụ");
                                            detail.PoDetailName = tool.ToolName;
                                           // detail.PoDetailDesign = tool.ToolDesignNo + "-" + tool.ToolMaterial;
                                            detail.PoDetailDesign = tool.ToolFullCode;

                                            var transactionToolFirsts = imports.Where(x => x.PoId == purchaseOrder.PurchaseOrderId
                                                                                        && x.FptId == poDetail.ReferenceId
                                                                                        && x.TransactionDate < fDate).ToList();
                                            if (transactionToolFirsts.Any())
                                                detail.Quantity -= transactionToolFirsts.Sum(id => id.Quantity);
                                            if (detail.Quantity < 0) {
                                                detail.Quantity = 0;
                                                continue;
                                            }

                                            var transactionTools = imports.Where(x => x.PoId == purchaseOrder.PurchaseOrderId
                                                                                        && x.FptId == poDetail.ReferenceId
                                                                                        && x.TransactionDate >= fDate
                                                                                        && x.TransactionDate <= tDate).ToList();
                                            foreach (var importPoDetail in transactionTools) {
                                                var importDetail = new PoTrackingImportDetail {
                                                    ImportDateString = importPoDetail.TransactionDate.ToString("dd/MM"),
                                                    ImportQuantity = importPoDetail.Quantity,
                                                    UnitPrice = detail.UnitPrice,
                                                    ExchangeRate = importPoDetail.ExchangeRate,
                                                    Currency = detail.Currency
                                                };
                                                detail.ImportDetails.Add(importDetail);
                                            }
                                            break;
                                    }
                                    if (!detail.ImportDetails.Any() && detail.Quantity > 0) {
                                        var importDetail = new PoTrackingImportDetail {
                                            ImportDateString = "",
                                            UnitPrice = detail.UnitPrice,
                                            ExchangeRate = detail.ExchangeRate
                                        };
                                        detail.ImportDetails.Add(importDetail);
                                    }

                                    if (poDetail.InquiryPoes.Any()) {
                                        var inquiry = poDetail.InquiryPoes.FirstOrDefault();
                                        if (!string.IsNullOrWhiteSpace(inquiryNumber)) {
                                            if (!inquiry.InquiryNumber.Contains(inquiryNumber)) continue;
                                        }
                                        detail.InquiryNumber = inquiry.InquiryNumber;
                                        if (inquiry.DueDate != null) {
                                            detail.InquiryDateStr = inquiry.DueDate.Value.ToString("dd/MM/yyyy");
                                            if (detail.Quantity > detail.ImportQuantity) {
                                                if (inquiry.DueDate.Value <= DateTime.Today) detail.InquiryState = 1; // red
                                                else if (inquiry.DueDate.Value <= DateTime.Today.AddDays(5)) detail.InquiryState = 3; // orange
                                                else if (inquiry.DueDate.Value <= DateTime.Today.AddDays(10)) detail.InquiryState = 2; // yellow
                                                else detail.InquiryState = 0;
                                            }
                                        }
                                        else {
                                            detail.InquiryDateStr = "";
                                            detail.InquiryState = 1;
                                        }
                                        detail.InquiryStatus = MyUtilities.PurchaseOrder.GetInquiryTrackingStatusName(inquiry.Status);
                                    }
                                    else if (!string.IsNullOrWhiteSpace(inquiryNumber)) continue;

                                    if (detail.ImportDetails.Any())
                                        entity.Details.Add(detail);
                                }
                                if (entity.Details.Any())
                                    group.List.Add(entity);
                            }
                        }

                        if (group.List.Any()) {
                            model.Add(group);
                        }
                    }

                    var pendingInquiries = (from x in vfi.InquiryPoes
                                            where (x.Status == (byte)MyUtilities.PurchaseOrder.InquiryEnum.Pending
                                                || x.Status == (byte)MyUtilities.PurchaseOrder.InquiryEnum.Approved)
                                            && (x.DueDate == null || x.DueDate.Value <= tDate)
                                            && (classifiedId == 0 || x.ClasstifiedId == classifiedId)
                                            orderby x.InquiryNumber
                                            select new {
                                                x.Status,
                                                InquiryNumber = x.InquiryNumber + "",
                                                x.DueDate,
                                                x.ClasstifiedId,
                                                x.VendorId,
                                                VendorName = x.VendorId == null ? "Chưa có nhà cung cấp" : x.Vendor.VendorName,
                                                x.ReferenceId,
                                                x.Currency,
                                                x.UnitPrice,
                                                x.Unit,
                                                x.OrderQty,
                                            }).ToList();
                    if (!string.IsNullOrWhiteSpace(inquiryNumber)) {
                        pendingInquiries = pendingInquiries.Where(x => x.InquiryNumber.Contains(inquiryNumber)).ToList();
                    }
                    if (pendingInquiries.Any()) {
                        var group = new GroupPoTracking {
                            TypeName = "Chưa đặt hàng",
                            ReportDate = tDate
                        };
                        model.Insert(0, group);
                        var index = 1;
                        //var inquiriesVendors = pendingInquiries.Select(x => new { x.VendorId, x.VendorName }).Distinct().ToList();
                        foreach (var inquiry in pendingInquiries) {
                            var entity = new PoTracking {
                                VendorId = inquiry.VendorId ?? 0,
                                VendorName = inquiry.VendorName,
                            };
                            group.List.Add(entity);
                            //var inquiriesById = pendingInquiries.Where(x => x.VendorId == vendor.VendorId).ToList();
                            //foreach (var inquiry in inquiriesById) {
                                var detail = new PoTrackingDetail {
                                    Currency = (inquiry.Currency + "").Trim(),
                                    Quantity = inquiry.OrderQty,
                                    UnitPrice = inquiry.UnitPrice,
                                    ExchangeRate = 1,
                                    UnitMeasure = inquiry.Unit,
                                    Index = index++,
                                    //PoDate = inquiry.PoDate,
                                    //CreateDate = entity.CreateDate,
                                    InquiryDateStr = inquiry.DueDate != null ? inquiry.DueDate.Value.ToString("dd/MM/yyyy") : "",
                                    InquiryNumber = inquiry.InquiryNumber,
                                    InquiryStatus = MyUtilities.PurchaseOrder.GetInquiryTrackingStatusName(inquiry.Status)
                                };
                                if (inquiry.DueDate != null) {
                                    detail.InquiryDateStr = inquiry.DueDate.Value.ToString("dd/MM/yyyy");
                                    if (inquiry.DueDate.Value <= DateTime.Today) detail.InquiryState = 1; // red
                                    else if (inquiry.DueDate.Value <= DateTime.Today.AddDays(5)) detail.InquiryState = 3; // orange
                                    else if (inquiry.DueDate.Value <= DateTime.Today.AddDays(10)) detail.InquiryState = 2; // yellow
                                    else detail.InquiryState = 0;
                                }
                                else {
                                    detail.InquiryDateStr = "";
                                    detail.InquiryState = 1;
                                }
                                //detail.InquiryStatus = MyUtilities.PurchaseOrder.GetInquiryTrackingStatusName(inquiry.Status);
                                if (detail.Currency.Equals("USD"))
                                    detail.ExchangeRate = exchangeRate;
                                else if (detail.Currency.Equals("EUR"))
                                    detail.ExchangeRate = 25000;
                                entity.Details.Add(detail);
                                switch (inquiry.ClasstifiedId) {
                                    case 1:
                                        var material =
                                            vfi.Materials.FirstOrDefault(m => m.MaterialId == inquiry.ReferenceId);
                                        if (material == null)
                                            throw new AggregateException("Lỗi! Không tìm thấy nguyên liệu");
                                        detail.PoDetailName = material.MaterialName;
                                        detail.PoDetailDesign =
                                            MyUtilities.Material.GetMaterialDesignNo(material.OutDiameter,
                                                                                     material.InDiameter,
                                                                                     material.DiameterType,
                                                                                     material.Shape);
                                        break;
                                    case 2:
                                        var fuel =
                                            vfi.Fuels.FirstOrDefault(m => m.FuelId == inquiry.ReferenceId);
                                        if (fuel == null)
                                            throw new AggregateException("Lỗi! Không tìm thấy nhiên liệu");
                                        detail.PoDetailName = fuel.FuelName;
                                        detail.PoDetailDesign = fuel.FuelDesignNo;

                                        break;
                                    case 3:
                                        var tool =
                                            vfi.Tools.FirstOrDefault(m => m.ToolId == inquiry.ReferenceId);
                                        if (tool == null)
                                            throw new AggregateException("Lỗi! Không tìm thấy công cụ");
                                        detail.PoDetailName = tool.ToolName;
                                        //detail.PoDetailDesign = tool.ToolDesignNo + "-" + tool.ToolMaterial;
                                        detail.PoDetailDesign = tool.ToolFullCode;
                                        break;
                                }
                            //}
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PagePrintPurchaseOrderTracking", "\n" + ex.Message);
                return Json(ex.Message);
            }
            return PartialView("PagePrintPurchaseOrderTracking", model);
            //return PartialView(null);
        }


        [HttpPost]
        public ActionResult PrintPurchaseOrder(int purchaseOrderId) {
            var model = GetPurchaseOrderDetailModel(purchaseOrderId);
            //using (var vfi = new tammaContext()) {

            //}
            return PartialView("PagePrintPurchaseOrderDetail", model);
            //return PartialView(null);
        }


        [HttpPost]
        public ActionResult PrintPurchaseOrderEN(int purchaseOrderId) {
            var model = GetPurchaseOrderDetailModel(purchaseOrderId);
            return PartialView("PagePrintPurchaseOrderDetailEN", model);
        }

        [HttpPost]
        public ActionResult PrintEvationForm(int formId) {
            var model = GetEvaluationFormModel(formId);
            return PartialView("PagePrintEvaluationForm", model);
        }


        [GridAction]
        public ActionResult UpdatePurchaseOrder(PurchaseOrderModel update) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdatePurchaseOrder",
                                         "Bạn đã bị mất quyền đăng nhập. " +
                                         "\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         "\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<PurchaseOrderModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var po = vfi.PurchaseOrders.FirstOrDefault(p => p.PurchaseOrderId == update.PurchaseOrderId);
                    if (po == null)
                        throw new AggregateException("Không tìm thấy phiếu mua hàng");
                    int vendorId = 0;
                    if (update.VendorName != po.Vendor.VendorName) {
                        try {
                            vendorId = Convert.ToInt32(update.VendorName);
                        }
                        catch (Exception) {
                        }

                        switch (po.MaterialClassifiedId) {
                            // Material
                            case 1:
                                var importDetails1 =
                                    vfi.ImportPurchaseOrderDetails
                                        .Where(td => td.ImportPurchaseOrder.Transaction.PoId == po.PurchaseOrderId &&
                                                     td.ImportPurchaseOrder.Transaction.Status ==
                                                     (byte)MyUtilities.Transaction.Status.Approved);
                                foreach (var importDetail in importDetails1) {
                                    var materialInv = vfi.MaterialInventories
                                        .FirstOrDefault(ti => ti.MaterialId == importDetail.MaterialId &&
                                                              ti.LotNumber.Equals(importDetail.LotNumber) &&
                                                              ti.VendorId == importDetail.VendorId);
                                    if (materialInv != null)
                                        materialInv.VendorId = vendorId;
                                    importDetail.VendorId = vendorId;
                                }
                                break;
                            // Fuel
                            case 2:
                                var importDetails2 =
                                    vfi.TransactionFptDetails
                                        .Where(td => td.TransactionFpt.PoId == po.PurchaseOrderId &&
                                                     td.TransactionFpt.Status ==
                                                     (byte)MyUtilities.Transaction.Status.Approved);
                                foreach (var importDetail in importDetails2) {
                                    var fuelInv = vfi.FuelInventories
                                        .FirstOrDefault(ti => ti.FuelId == importDetail.FptId &&
                                                              ti.LotNumber.Equals(importDetail.LotNumber) &&
                                                              ti.VendorId == importDetail.VendorId);
                                    if (fuelInv != null)
                                        fuelInv.VendorId = vendorId;
                                    importDetail.VendorId = vendorId;
                                }
                                break;
                            // Tools
                            case 3:
                                var importDetails3 =
                                    vfi.TransactionFptDetails
                                        .Where(td => td.TransactionFpt.PoId == po.PurchaseOrderId &&
                                                     td.TransactionFpt.Status ==
                                                     (byte)MyUtilities.Transaction.Status.Approved);
                                foreach (var importDetail in importDetails3) {
                                    var toolInv = vfi.ToolInventories
                                        .FirstOrDefault(ti => ti.ToolId == importDetail.FptId &&
                                                              ti.LotNumber.Equals(importDetail.LotNumber) &&
                                                              ti.VendorId == importDetail.VendorId);
                                    if (toolInv != null)
                                        toolInv.VendorId = vendorId;
                                    importDetail.VendorId = vendorId;
                                }
                                break;
                            //case 6:
                            //    var product =
                            //        vfi.Products.FirstOrDefault(m => m.ProductId == poDetail.ReferenceId);
                            //    if (product == null)
                            //        throw new AggregateException("Lỗi! Không tìm thấy sản phẩm");
                            //    entity.Name = product.ProductName;
                            //    entity.Code = product.ProductCode;
                            //    break;

                        }
                        if (!string.IsNullOrWhiteSpace(update.EmployeeName)) {
                            if (po.EmployeeName != update.EmployeeName) {
                                po.EmployeeName = update.EmployeeName;
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(update.EmployeeName)) {
                            if( string.IsNullOrWhiteSpace(po.EmployeeName)){
                                throw new AggregateException("Vui lòng điền Tên nhân viên.");
                            }
                            else if (!string.IsNullOrWhiteSpace(po.EmployeeName)){
                                po.EmployeeName = po.EmployeeName;
                            }
                        }

                        // Address
                        switch (update.AddressName) {
                            case "1":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "2":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "3":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "4":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "5":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "6":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "7":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "8":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "9":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "10":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            default: 
                                break;

                        }
                        // Bill to
                        switch (update.BillTo) {
                            case "1":
                                po.BillToId = Convert.ToInt32(update.BillTo);
                                break;
                            case "2":
                                po.BillToId = Convert.ToInt32(update.BillTo);
                                break;
                            case "3":
                                po.BillToId = Convert.ToInt32(update.BillTo);
                                break;
                            case "4":
                                po.BillToId = Convert.ToInt32(update.BillTo);
                                break;
                            case "5":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            case "6":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            case "7":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            case "8":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            case "9":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            case "10":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            default:
                                break;
                        }

                        // B/L
                        if (po.BillOfLanding != update.BillOfLanding) {
                            po.BillOfLanding = update.BillOfLanding;
                        }

                        if (!string.IsNullOrWhiteSpace(po.BillOfLanding)) {
                            if (po.Status == 1) {
                                po.Status = 4;
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(po.BillOfLanding)) {
                            if (po.Status == 4) {
                                po.Status = 1;
                            }
                        }


                        // 3 PT
                        if (update.PaymentMethodId != 0) {
                            po.PaymentId = update.PaymentMethodId;
                        }
                        else if (update.PaymentMethodId == 0 && (po.PaymentId == 0 || po.PaymentId == null)) {
                            throw new AggregateException("Vui lòng cập nhật Phương thức thanh toán.");
                        }
                        if (update.ShipMethodId != 0) {
                            po.DeliveryById = update.ShipMethodId;
                        }
                        else if (update.ShipMethodId == 0 && (po.DeliveryById == 0 || po.DeliveryById == null)) {
                            throw new AggregateException("Vui lòng cập nhật Hình thức vận chuyển.");
                        }

                        if (update.DeliveryMethodId != 0) {
                            po.ConditionDeliveryId = update.DeliveryMethodId;
                        }
                        else if (update.DeliveryMethodId == 0 && (po.ConditionDeliveryId == 0 || po.ConditionDeliveryId == null)) {
                            throw new AggregateException("Vui lòng cập nhật Phương thức vận chuyển.");
                        }


                        po.VendorId = vendorId;
                        var a = vfi.SaveChanges();
                        ModelState.AddModelError("UpdatePurchaseOrder", "Đã cập nhật " + a + " dòng! Hãy làm mới dữ liệu.");
                        return View(new GridModel(new List<PurchaseOrderModel>()));
                    }

                    // TH2: khong cap nhat gi ca
                    else if (update.VendorName == po.Vendor.VendorName
                             && update.EmployeeName == po.EmployeeName
                             && po.DeliveryAddress.AddressShortName == update.AddressName
                             && update.BillOfLanding == po.BillOfLanding
                             && update.DeliveryMethodId == 0 
                             && update.PaymentMethodId == 0 
                             && update.ShipMethodId == 0) {
                        throw new AggregateException("Không có gì cập nhật");
                    }


                    // TH3: co cap nhat it nhat 1 
                    else {
                        // employee name
                        if (!string.IsNullOrWhiteSpace(update.EmployeeName)) {
                            if (po.EmployeeName != update.EmployeeName) {
                                po.EmployeeName = update.EmployeeName;
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(update.EmployeeName)) {
                            if (string.IsNullOrWhiteSpace(po.EmployeeName)) {
                                throw new AggregateException("Vui lòng điền Tên nhân viên.");
                            }
                            else if (!string.IsNullOrWhiteSpace(po.EmployeeName)) {
                                po.EmployeeName = po.EmployeeName;
                            }
                        }

                        // B/L 
                        if (po.BillOfLanding != update.BillOfLanding){
                            po.BillOfLanding = update.BillOfLanding;
                        }

                        if(!string.IsNullOrWhiteSpace(po.BillOfLanding)){
                            if (po.Status == 1) {
                                po.Status = 4;
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(po.BillOfLanding)) {
                            if (po.Status == 4) {
                                po.Status = 1;
                            }
                        }
                        // address
                        switch (update.AddressName) {
                            case "1":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "2":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "3":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "4":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "5":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "6":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "7":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "8":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "9":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            case "10":
                                po.AddressId = Convert.ToInt32(update.AddressName);
                                break;
                            default:
                                break;
                        }

                        // Bill to
                        switch (update.BillTo){
                            case "1":
                                po.BillToId = Convert.ToInt32(update.BillTo);
                                break;
                            case "2":
                                po.BillToId = Convert.ToInt32(update.BillTo);
                                break;
                            case "3":
                                po.BillToId = Convert.ToInt32(update.BillTo);
                                break;
                            case "4":
                                po.BillToId = Convert.ToInt32(update.BillTo);
                                break;
                            case "5":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            case "6":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            case "7":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            case "8":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            case "9":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            case "10":
                                po.AddressId = Convert.ToInt32(update.BillTo);
                                break;
                            default:
                                break;
                        }


                        if (update.PaymentMethodId != 0) {
                            po.PaymentId = update.PaymentMethodId;
                        }
                        else if ( update.PaymentMethodId == 0 && (po.PaymentId == 0|| po.PaymentId == null)){
                            throw new AggregateException("Vui lòng cập nhật Phương thức thanh toán.");
                        }

                        if (update.ShipMethodId != 0) {
                            po.DeliveryById = update.ShipMethodId;
                        }
                        else if (update.ShipMethodId == 0 && (po.DeliveryById == 0 || po.DeliveryById == null)) {
                            throw new AggregateException("Vui lòng cập nhật Hình thức vận chuyển.");
                        }

                        if (update.DeliveryMethodId != 0) {
                            po.ConditionDeliveryId = update.DeliveryMethodId;
                        }
                        else if (update.DeliveryMethodId == 0 && (po.ConditionDeliveryId == 0 || po.ConditionDeliveryId == null)) {
                            throw new AggregateException("Vui lòng cập nhật Phương thức vận chuyển.");
                        }

                        vfi.SaveChanges();
                        ModelState.AddModelError("UpdatePurchaseOrder", "Đã cập nhật!! Hãy làm mới dữ liệu.");
                        return View(new GridModel(new List<PurchaseOrderModel>()));
                    }
                }
            }
            catch (Exception ex) {
                // Thông điệp của exception chính
                ModelState.AddModelError("UpdatePurchaseOrder", ex.Message);

                // Kiểm tra và lấy inner exception nếu có
                //if (ex.InnerException != null) {
                //    ModelState.AddModelError("UpdatePurchaseOrder", ex.InnerException.Message);
                //}
            }

            return View(new GridModel(new List<PurchaseOrderModel>()));
        }


        [GridAction]
        public ActionResult SelectManagePurchaseOrders(byte? status, bool? active, string fromDate, string toDate, string inquiryNumber = "") {
            return View(new GridModel(SelectPurchaseOrderView(status, active, fromDate, toDate, inquiryNumber)));
        }

        List<TransactionFptModel> GetImportByPoId(long purchaseOrderId) {
            var model = new List<TransactionFptModel>();
            using (var vfi = new tammaContext()) {
                var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == purchaseOrderId);
                if (purchaseOrder == null)
                    throw new AggregateException("Lỗi! Không tìm thấy phiếu mua");
                switch (purchaseOrder.MaterialClassifiedId) {
                    case 1:
                        //var importMaterials =
                        //    vfi.Transactions.Where(
                        //        t => t.MoP && t.Status == (byte) MyUtilities.Transaction.Status.Approved );
                        var importPos =
                            vfi.ImportPurchaseOrders.Where(
                                i =>
                                i.PurchaseOrderId == purchaseOrderId &&
                                i.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved).ToList();
                        foreach (var importPo in importPos) {
                            var transaction = importPo.Transaction;
                            var entity = new TransactionFptModel {
                                TransactionId = transaction.TransactionId,
                                TransactionCode = transaction.TransactionCode,
                                ModifiedDate = transaction.ModifiedDate,
                                ModifiedUser = transaction.ModifiedUser,
                                TransactionDate = transaction.CreatedDate,
                                PoId = purchaseOrderId,
                                Type = purchaseOrder.MaterialClassifiedId,
                                
                            };
                            foreach (var detail in importPo.ImportPurchaseOrderDetails) {
                                var purchaseDetail =
                                    vfi.PurchaseOrderDetails.FirstOrDefault(
                                        pod => pod.PurchaseOrderId == purchaseOrder.PurchaseOrderId
                                               && pod.ReferenceId == detail.MaterialId);
                                if (purchaseDetail.Unit.Contains("Kg")) {
                                    entity.TotalQuantity += detail.QuantityKg;
                                }
                                else {
                                    entity.TotalQuantity += detail.Quantity;
                                }
                            }
                            model.Add(entity);
                        }
                        break;
                    case 2:
                    case 3:
                        var transactionFpts = (from i in vfi.TransactionFpts
                                               where
                                                   i.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                   i.PoId == purchaseOrderId &&
                                                   i.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import
                                               select i).ToList();
                        foreach (var transaction in transactionFpts) {
                            var entity = new TransactionFptModel {
                                TransactionId = transaction.TransactionId,
                                TransactionCode = transaction.TransactionCode,
                                ModifiedDate = transaction.ModifiedDate,
                                ModifiedUser = transaction.ModifiedUser,
                                TransactionDate = transaction.TransactionDate,
                                PoId = purchaseOrderId,
                                Type = purchaseOrder.MaterialClassifiedId,
                                TotalQuantity = transaction.TransactionFptDetails.Sum(x => x.Quantity)
                            };
                            model.Add(entity);
                        }
                        break;

                    case 6:
                        var transactions = (from i in vfi.Transactions
                                            where
                                                i.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                i.PoId == purchaseOrderId
                                            select i).ToList();
                        foreach (var transaction in transactions) {
                            var entity = new TransactionFptModel {
                                TransactionId = transaction.TransactionId,
                                TransactionCode = transaction.TransactionCode,
                                ModifiedDate = transaction.ModifiedDate,
                                ModifiedUser = transaction.ModifiedUser,
                                TransactionDate = transaction.CreatedDate,
                                PoId = purchaseOrderId,
                                Type = purchaseOrder.MaterialClassifiedId,
                                TotalQuantity = transaction.TransactionDetails.Sum(x => x.Quantity)
                            };
                            model.Add(entity);
                        }
                        break;
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectImportByPoId(long purchaseOrderId) {

            var model = new List<TransactionFptModel>();
            try {
                model = GetImportByPoId(purchaseOrderId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportByPoId", "\n" + ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.TransactionDate)));
        }

        List<TransactionFptDetailModel> GetPoImportDetail(int transactionId, long purchaseOrderId) {

            var model = new List<TransactionFptDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == purchaseOrderId);
                    if (purchaseOrder == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu mua");
                    var vendor = vfi.Vendors.FirstOrDefault(x => x.VendorId == purchaseOrder.VendorId);
                    switch (purchaseOrder.MaterialClassifiedId) {
                        case 1:
                            var importMaterial = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                            if (importMaterial == null)
                                throw new AggregateException("Lỗi! Không tìm thấy phiếu nhập");
                            var importPoMaterial = vfi.ImportPurchaseOrders.FirstOrDefault(i => i.TransactionId == transactionId);
                            if (importPoMaterial == null)
                                throw new AggregateException("Lỗi! Không tìm thấy phiếu nhập");
                            foreach (var detail in importPoMaterial.ImportPurchaseOrderDetails) {
                                //var importDetail =
                                //    importPoMaterial.ImportPurchaseOrderDetails.FirstOrDefault(
                                //        id => id.MaterialId == detail.ReferenceId);
                                var material =
                                    vfi.Materials.FirstOrDefault(m => m.MaterialId == detail.MaterialId);
                                var purchaseDetail =
                                    vfi.PurchaseOrderDetails.FirstOrDefault(
                                        pod => pod.PurchaseOrderDetailId == detail.PoDetailId);
                                if (purchaseDetail == null) {
                                    purchaseDetail =
                                      vfi.PurchaseOrderDetails.FirstOrDefault(
                                          pod => pod.PurchaseOrderId == purchaseOrder.PurchaseOrderId
                                                 && pod.ReferenceId == detail.MaterialId);
                                }
                                //var materialInv =
                                //    vfi.MaterialInventories.FirstOrDefault(
                                //        mi =>
                                //        mi.MaterialId == detail.MaterialId &&
                                //        mi.LotNumber.Equals(detail.LotNumber) &&
                                //        mi.VendorId == detail.VendorId);
                                var entity = new TransactionFptDetailModel {
                                    DetailId = detail.ImportDetailId,
                                    LotNumber = detail.LotNumber,
                                    Note = detail.Note,
                                    Quantity = purchaseDetail.Unit.Contains("Kg") ? detail.QuantityKg : detail.Quantity,
                                    FuelCode = material.MaterialCode,
                                    FuelName = material.MaterialName,
                                    //FuelDesignNo = MaterialModel.GetDesignNo(material),
                                    UnitMeasure = purchaseDetail.Unit,
                                    UnitPrice = purchaseDetail.UnitPrice,
                                    //Price = detail.QuantityKg * purchaseDetail.UnitPrice,
                                    CurrencyCode = purchaseOrder.CurrencyCode.Trim(),
                                    TransactionId = transactionId,
                                    PoId = purchaseOrderId,
                                    NG = detail.NG,
                                    Lock = detail.Lock,
                                    //ImportDetailId = detail.ImportDetailId,
                                };
                                entity.Price = entity.Quantity * entity.UnitPrice;
                                entity.FuelDesignNo = MyUtilities.Material.GetMaterialDesignNo(material.OutDiameter,
                                                                                               material.InDiameter,
                                                                                               material.DiameterType,
                                                                                               material.Shape);
                                model.Add(entity);
                            }
                            break;
                        case 2:
                            var importFuel = vfi.TransactionFpts.FirstOrDefault(t => t.TransactionId == transactionId);
                            if (importFuel == null)
                                throw new AggregateException("Lỗi! Không tìm thấy phiếu nhập");
                            foreach (var detail in importFuel.TransactionFptDetails) {
                                var purchaseDetail =
                                    vfi.PurchaseOrderDetails.FirstOrDefault(
                                        pod => pod.PurchaseOrderDetailId == detail.PoDetailId);
                                if (purchaseDetail == null) {
                                    purchaseDetail =
                                        vfi.PurchaseOrderDetails.FirstOrDefault(
                                            pod => pod.PurchaseOrderId == purchaseOrder.PurchaseOrderId
                                                   && pod.ReferenceId == detail.FptId);
                                }
                                var fuel =
                                    vfi.Fuels.FirstOrDefault(m => m.FuelId == detail.FptId);
                                //var fuelInv =
                                //    vfi.FuelInventories.FirstOrDefault(
                                //        mi =>
                                //        mi.FuelId == detail.FptId &&
                                //        mi.LotNumber.Equals(detail.LotNumber) &&
                                //        mi.VendorId == detail.VendorId);
                                var entity = new TransactionFptDetailModel {
                                    DetailId = detail.DetailId,
                                    LotNumber = detail.LotNumber,
                                    Note = detail.Note,
                                    Quantity = detail.Quantity,
                                    FuelCode = fuel.FuelCode,
                                    FuelName = fuel.FuelName,
                                    FuelDesignNo = fuel.FuelDesignNo,
                                    UnitMeasure = detail.UnitMeasure,
                                    UnitPrice = purchaseDetail.UnitPrice,
                                    Price = detail.Quantity * purchaseDetail.UnitPrice,
                                    CurrencyCode = purchaseOrder.CurrencyCode.Trim(),
                                    TransactionId = transactionId,
                                    PoId = purchaseOrderId,
                                    NG = detail.NG,
                                    Lock = detail.Lock,
                                };
                                model.Add(entity);
                            }
                            break;
                        case 3:
                            var importTool = vfi.TransactionFpts.FirstOrDefault(t => t.TransactionId == transactionId);
                            if (importTool == null)
                                throw new AggregateException("Lỗi! Không tìm thấy phiếu nhập");
                            //var importPoTool = vfi.ImportPurchaseOrders.FirstOrDefault(i => i.TransactionId == transactionId);
                            //if (importPoTool == null)
                            //    throw new AggregateException("Lỗi! Không tìm thấy phiếu nhập");
                            foreach (var detail in importTool.TransactionFptDetails) {
                                var purchaseDetail = vfi.PurchaseOrderDetails.FirstOrDefault( pod => pod.PurchaseOrderDetailId == detail.PoDetailId);
                                if (purchaseDetail == null) {
                                    purchaseDetail = vfi.PurchaseOrderDetails.FirstOrDefault( pod => pod.PurchaseOrderId == purchaseOrder.PurchaseOrderId && pod.ReferenceId == detail.FptId);
                                }
                                var tool =
                                    vfi.Tools.FirstOrDefault(m => m.ToolId == detail.FptId);
                                //var toolInv =
                                //    vfi.ToolInventories.FirstOrDefault(
                                //        mi =>
                                //        mi.ToolId == detail.FptId &&
                                //        mi.LotNumber.Equals(detail.LotNumber) &&
                                //        mi.VendorId == detail.VendorId);
                                var entity = new TransactionFptDetailModel {
                                    DetailId = detail.DetailId,
                                    LotNumber = detail.LotNumber,
                                    Note = detail.Note,
                                    Quantity = detail.Quantity,
                                    FuelCode = tool.ToolCode,
                                    FuelName = tool.ToolName,
                                    FuelDesignNo = tool.ToolDesignNo,
                                    UnitMeasure = detail.UnitMeasure,
                                    UnitPrice = purchaseDetail.UnitPrice,
                                    Price = detail.Quantity * purchaseDetail.UnitPrice,
                                    CurrencyCode = purchaseOrder.CurrencyCode.Trim(),
                                    TransactionId = transactionId,
                                    PoId = purchaseOrderId,
                                    NG = detail.NG,
                                    Lock = detail.Lock,


                                };
                                model.Add(entity);
                            }
                            break;
                        // product
                        case 6:
                            var transaction = (from tf in vfi.Transactions
                                               where tf.TransactionId == transactionId
                                               select tf).FirstOrDefault();
                            if (transaction == null)
                                throw new AggregateException("Lỗi! Không tìm thấy phiếu");
                            foreach (var detail in transaction.TransactionDetails) {
                                var product = vfi.Products.FirstOrDefault(x => x.ProductId == detail.ReferenceId);
                                var purchaseDetail =
                                    vfi.PurchaseOrderDetails.FirstOrDefault(
                                        pod => pod.PurchaseOrderDetailId == detail.PoDetailId);
                                if (purchaseDetail == null) {
                                    purchaseDetail =
                                        vfi.PurchaseOrderDetails.FirstOrDefault(
                                            pod => pod.PurchaseOrderId == purchaseOrder.PurchaseOrderId
                                                   && pod.ReferenceId == detail.ReferenceId);
                                }
                                var entity = new TransactionFptDetailModel {
                                    DetailId = detail.TransactionDetailId,
                                    LotNumber = detail.LotNumber,
                                    Note = detail.Note,
                                    Quantity = detail.Quantity,
                                    FuelCode = product.ProductCode,
                                    FuelName = product.ProductName,
                                    FuelDesignNo = product.DesignNo,
                                    UnitMeasure = detail.UnitMeasure,
                                    TotalInv = 0,
                                    UnitPrice = purchaseDetail.UnitPrice,
                                    Price = detail.Quantity * purchaseDetail.UnitPrice,
                                    CurrencyCode = purchaseOrder.CurrencyCode.Trim(),
                                    TransactionId = transactionId,
                                    PoId = purchaseOrderId,
                                    NG = detail.NG,

                                };
                                model.Add(entity);
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
            return model;
        }

        [GridAction]
        public ActionResult SelectImportDetail(int transactionId, long purchaseOrderId) {
            var model = new List<TransactionFptDetailModel>();
            try {
                model = GetPoImportDetail(transactionId, purchaseOrderId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportDetail", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectImportPoManagement2(int classifiedId, int vendorId, string fromDate, string toDate) {
            var model2 = new List<TransactionFptDetailModel>();
            try {
                var fDate = MyUtilities.Function.ParseDate(fromDate);
                var tDate = MyUtilities.Function.ParseDate(toDate);
                using (var vfi = new tammaContext()) {
                    var index = 1;
                    if (classifiedId == 0 || classifiedId == 1) {// material
                            var importDetails = (from x in vfi.ImportPurchaseOrderDetails.OrderBy(t => t.Vendor.VendorName)
                                                 where x.ImportPurchaseOrder.ImportDate >= fDate
                                                 && x.ImportPurchaseOrder.PurchaseOrderId != null
                                                 && x.ImportPurchaseOrder.ImportDate <= tDate
                                                 && x.ImportPurchaseOrder.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved
                                                 && (vendorId == 0 || x.VendorId == vendorId)
                                                 && x.ImportPurchaseOrder.PurchaseOrder.MaterialClassifiedId == 1
                                                 select new TransactionFptDetailModel {
                                                     FptId = x.MaterialId.Value,
                                                     FptCode = x.Material.MaterialCode,
                                                     FptName = x.Material.MaterialName,
                                                     //FptDesignNo = MyUtilities.Material.GetMaterialDesignNo(x.Material.OutDiameter,
                                                     //                                           x.Material.InDiameter,
                                                     //                                           x.Material.DiameterType,
                                                     //                                           x.Material.Shape),
                                                     LotNumber = "x" + (x.Length / 1000) + "-" + x.LotNumber,
                                                     Note = x.Note,
                                                     NG = x.NG,
                                                     Lock = x.Lock,
                                                     ImportDetailId = x.ImportDetailId,

                                                     Quantity = x.QuantityKg,
                                                     UnitPrice = x.UnitPrice,
                                                     VendorId = x.VendorId,
                                                     VendorName = x.Vendor.VendorName,
                                                     VendorCode = x.Vendor.VendorCode,
                                                     TransactionDate = x.ImportPurchaseOrder.ImportDate,
                                                     ModifiedUser = x.ImportPurchaseOrder.ModifiedUser,
                                                     ModifiedDate = x.ImportPurchaseOrder.ModifiedDate,
                                                     TransactionCode = x.ImportPurchaseOrder.Transaction.TransactionCode,
                                                     //ExchangeRate = x.ImportPurchaseOrder.ExchangeRate,

                                                     CurrencyCode = x.ImportPurchaseOrder.PurchaseOrder.CurrencyCode,
                                                     PoId = x.ImportPurchaseOrder.PurchaseOrderId.Value,
                                                     TaxInvoiceNumber = x.PoReferenceDetailId != null
                                                                     ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.TaxInvoiceNumber
                                                                     : "",
                                                     TaxPercent = x.PoReferenceDetailId != null
                                                                     ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.TaxPercent
                                                                     : 0,
                                                     ExchangeRate = x.PoReferenceDetailId != null
                                                                     ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.ExchangeRate
                                                                     : x.ImportPurchaseOrder.ExchangeRate,
                                                     TaxInvoiceDate = x.PoReferenceDetailId != null
                                                                     ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.PoDate
                                                                     : DateTime.Today,
                                                 }).ToList();

                            var poIds = importDetails.Select(x => x.PoId).Distinct().ToList();

                            var purchaseDetails = vfi.PurchaseOrderDetails.Where(x => poIds.Contains(x.PurchaseOrderId))
                                                    .Select(x => new { x.PurchaseOrderId, x.ReferenceId, x.Unit, x.UnitPrice, x.PurchaseOrder.ShipDate, x.PurchaseOrder.RevisionNumber, x.OrderQty })
                                                    .ToList();
                            foreach (var detail in importDetails) {
                                //if (detail.FptId == null) {
                                //    continue;
                                //}
                                var purchaseDetail = purchaseDetails.FirstOrDefault(
                                        pod => pod.PurchaseOrderId == detail.PoId
                                               && pod.ReferenceId == detail.FptId);
                                detail.FptDesignNo = detail.FptCode.Replace(detail.FptName, "");

                                if (purchaseDetail != null) {
                                    detail.DeliveryDate = purchaseDetail.ShipDate;
                                    detail.UnitMeasure = purchaseDetail.Unit;
                                    detail.UnitPrice = purchaseDetail.UnitPrice;
                                    detail.PoCode = purchaseDetail.RevisionNumber;
                                    detail.VendorDeliveryOnDate = purchaseDetail.ShipDate.HasValue && detail.TransactionDate > purchaseDetail.ShipDate.Value;
                                    detail.Index = index;
                                    detail.OrderQuantity = purchaseDetail.OrderQty;
                                }
                                detail.Price = detail.Quantity * detail.UnitPrice * detail.ExchangeRate;
                                if (string.IsNullOrWhiteSpace(detail.TaxInvoiceNumber)) { detail.TaxInvoiceDate = null; }
                                index++;
                            }
                            model2.AddRange(importDetails);
                    }


                    if (classifiedId == 0 || classifiedId == 2) { // fuel
                        var importDetails = (from x in vfi.TransactionFptDetails.OrderBy(t => t.TransactionFpt.PurchaseOrder.Vendor.VendorName)
                                             where
                                                x.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                x.TransactionFpt.TransactionDate >= fDate && x.TransactionFpt.TransactionDate <= tDate &&
                                                x.TransactionFpt.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import &&
                                                x.TransactionFpt.Fpt == (int)MyUtilities.PurchaseOrder.FptLot.Fuel &&
                                                x.TransactionFpt.PoId != null &&
                                                 (vendorId == 0 || x.VendorId == vendorId)
                                             select new TransactionFptDetailModel {
                                                 FptId = x.FptId,
                                                 Note = x.Note,
                                                 NG = x.NG,
                                                 Lock = x.Lock,
                                                 Quantity = x.Quantity,
                                                 UnitMeasure = x.UnitMeasure,
                                                 UnitPrice = x.UnitPrice,
                                                 VendorId = x.VendorId,
                                                 //VendorName = x.Vendor.VendorName,
                                                 //VendorCode = x.Vendor.VendorCode,
                                                 TransactionDate = x.TransactionFpt.TransactionDate,
                                                 ModifiedUser = x.TransactionFpt.ModifiedUser,
                                                 ModifiedDate = x.TransactionFpt.ModifiedDate,
                                                 TransactionCode = x.TransactionFpt.TransactionCode,
                                                 LotNumber = x.LotNumber,
                                                 PoId = x.TransactionFpt.PoId.Value,
                                                 CurrencyCode = x.TransactionFpt.PurchaseOrder.CurrencyCode.Trim(),
                                                 TaxInvoiceNumber = x.PoReferenceDetailId != null
                                                                 ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.TaxInvoiceNumber
                                                                 : "",
                                                 TaxPercent = x.PoReferenceDetailId != null
                                                                 ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.TaxPercent
                                                                 : 0,
                                                 ExchangeRate = x.PoReferenceDetailId != null
                                                                 ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.ExchangeRate
                                                                 : x.TransactionFpt.ExchangeRate,
                                                 TaxInvoiceDate = x.PoReferenceDetailId != null
                                                                 ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.PoDate
                                                                 : DateTime.Today,
                                             }).ToList();

                        var poIds = importDetails.Select(x => x.PoId).Distinct().ToList();
                        var purchaseDetails = vfi.PurchaseOrderDetails.Where(x => poIds.Contains(x.PurchaseOrderId))
                                                .Select(x => new {
                                                    x.PurchaseOrderId,
                                                    x.ReferenceId,
                                                    x.Unit,
                                                    x.UnitPrice,
                                                    x.PurchaseOrder.Vendor.VendorName,
                                                    x.PurchaseOrder.Vendor.VendorCode,
                                                    x.PurchaseOrder.ShipDate,
                                                    x.PurchaseOrder.RevisionNumber,
                                                    x.OrderQty,
                                                })
                                                .ToList();
                        var itemIds = importDetails.Select(x => x.FptId).Distinct().ToList();
                        var items = vfi.Fuels.Where(x => itemIds.Contains(x.FuelId))
                            .Select(x => new { Id = x.FuelId, Name = x.FuelName, Code = x.FuelFullCode, DesignNo = x.FuelDesignNo })
                            .ToList();
                        foreach (var detail in importDetails) {
                            var purchaseDetail =
                                purchaseDetails.FirstOrDefault(
                                    pod => pod.PurchaseOrderId == detail.PoId
                                           && pod.ReferenceId == detail.FptId);

                            if (purchaseDetail != null) {
                                detail.UnitMeasure = purchaseDetail.Unit;
                                detail.UnitPrice = purchaseDetail.UnitPrice;
                                detail.VendorName = purchaseDetail.VendorName;
                                detail.VendorCode = purchaseDetail.VendorCode;
                                detail.PoCode = purchaseDetail.RevisionNumber;
                                detail.DeliveryDate = purchaseDetail.ShipDate;
                                detail.VendorDeliveryOnDate = purchaseDetail.ShipDate.HasValue && detail.TransactionDate > purchaseDetail.ShipDate.Value;
                                detail.OrderQuantity = purchaseDetail.OrderQty;
                            }
                            detail.Price = detail.Quantity * detail.UnitPrice * detail.ExchangeRate;
                            var item = items.FirstOrDefault(m => m.Id == detail.FptId);
                            if (item != null) {
                                detail.FptName = item.Name;
                                detail.FptCode = item.Code;
                                detail.FptDesignNo = item.DesignNo;
                                detail.Index = index;
                            }
                            if (string.IsNullOrWhiteSpace(detail.TaxInvoiceNumber)) { detail.TaxInvoiceDate = null; }
                            index++;

                        }
                        model2.AddRange(importDetails);
                    }

                    if (classifiedId == 0 || classifiedId == 3) {// tool
                        var importDetails = (from x in vfi.TransactionFptDetails.OrderBy(t => t.TransactionFpt.PurchaseOrder.Vendor.VendorName)
                                             where
                                                x.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                x.TransactionFpt.TransactionDate >= fDate && x.TransactionFpt.TransactionDate <= tDate &&
                                                x.TransactionFpt.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import &&
                                                x.TransactionFpt.Fpt == (int)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                                x.TransactionFpt.PoId != null &&
                                                 (vendorId == 0 || x.VendorId == vendorId)
                                             select new TransactionFptDetailModel {
                                                 FptId = x.FptId,
                                                 Note = x.Note,
                                                 NG = x.NG,
                                                 Lock = x.Lock,
                                                 Quantity = x.Quantity,
                                                 UnitMeasure = x.UnitMeasure,
                                                 UnitPrice = x.UnitPrice,
                                                 VendorId = x.VendorId,
                                                 //VendorName = x.Vendor.VendorName,
                                                 //VendorCode = x.Vendor.VendorCode,
                                                 TransactionDate = x.TransactionFpt.TransactionDate,
                                                 ModifiedUser = x.TransactionFpt.ModifiedUser,
                                                 ModifiedDate = x.TransactionFpt.ModifiedDate,
                                                 TransactionCode = x.TransactionFpt.TransactionCode,
                                                 LotNumber = x.LotNumber,
                                                 PoId = x.TransactionFpt.PoId.Value,
                                                 CurrencyCode = x.TransactionFpt.PurchaseOrder.CurrencyCode.Trim(),
                                                 TaxInvoiceNumber = x.PoReferenceDetailId != null
                                                                 ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.TaxInvoiceNumber
                                                                 : "",
                                                 TaxPercent = x.PoReferenceDetailId != null
                                                                 ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.TaxPercent
                                                                 : 0,
                                                 ExchangeRate = x.PoReferenceDetailId != null
                                                                 ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.ExchangeRate
                                                                 : x.TransactionFpt.ExchangeRate,
                                                 TaxInvoiceDate = x.PoReferenceDetailId != null
                                                                 ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.PoDate
                                                                 : DateTime.Today,

                                             }).ToList();

                        var poIds = importDetails.Select(x => x.PoId).Distinct().ToList();
                        var purchaseDetails = vfi.PurchaseOrderDetails.Where(x => poIds.Contains(x.PurchaseOrderId))
                                                .Select(x => new {
                                                    x.PurchaseOrderId,
                                                    x.ReferenceId,
                                                    x.Unit,
                                                    x.UnitPrice,
                                                    x.PurchaseOrder.Vendor.VendorName,
                                                    x.PurchaseOrder.Vendor.VendorCode,
                                                    x.PurchaseOrder.ShipDate,
                                                    x.PurchaseOrder.RevisionNumber,
                                                    x.OrderQty,
                                                })
                                                .ToList();
                        var itemIds = importDetails.Select(x => x.FptId).Distinct().ToList();
                        var items = vfi.Tools.Where(x => itemIds.Contains(x.ToolId))
                            .Select(x => new { Id = x.ToolId, Name = x.ToolName, Code = x.ToolFullCode, DesignNo = x.ToolDesignNo })
                            .ToList();
                        foreach (var detail in importDetails) {
                            var purchaseDetail =
                                purchaseDetails.FirstOrDefault(
                                    pod => pod.PurchaseOrderId == detail.PoId
                                           && pod.ReferenceId == detail.FptId);

                            if (purchaseDetail != null) {
                                detail.UnitMeasure = purchaseDetail.Unit;
                                detail.UnitPrice = purchaseDetail.UnitPrice;
                                detail.VendorName = purchaseDetail.VendorName;
                                detail.VendorCode = purchaseDetail.VendorCode;
                                detail.DeliveryDate = purchaseDetail.ShipDate;
                                detail.PoCode = purchaseDetail.RevisionNumber;
                                detail.VendorDeliveryOnDate = purchaseDetail.ShipDate.HasValue && detail.TransactionDate > purchaseDetail.ShipDate.Value;
                                detail.OrderQuantity = purchaseDetail.OrderQty;
                            }
                            detail.Price = detail.Quantity * detail.UnitPrice * detail.ExchangeRate;
                            var item = items.FirstOrDefault(m => m.Id == detail.FptId);
                            if (item != null) {
                                detail.FptName = item.Name;
                                detail.FptCode = item.Code;
                                detail.FptDesignNo = item.DesignNo;
                                detail.Index = index;
                            }
                            if (string.IsNullOrWhiteSpace(detail.TaxInvoiceNumber)) { detail.TaxInvoiceDate = null; }
                            index++;
                        }
                        model2.AddRange(importDetails);
                        
                    }

                    if (classifiedId == 0 || classifiedId == 4) {//plating
                        var importDetails = (from x in vfi.ImportNCU_QCBDetail.OrderBy(t => t.ExportGCN_NCUDetail.PlatingFormDetail.PlatingForm.Vendor.VendorName)
                                             where x.ImportNCU_QCB.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved
                                             && x.ImportNCU_QCB.ImportDate >= fDate && x.ImportNCU_QCB.ImportDate <= tDate
                                             && (vendorId == 0 || x.ImportNCU_QCB.PlatingForm.VendorId == vendorId)
                                             select new TransactionFptDetailModel
                                             {
                                                 Note = x.Note,
                                                 //Quantity = importDetail.,
                                                 UnitMeasure = x.ExportGCN_NCUDetail.PlatingFormDetail.Unit,
                                                 UnitPrice = x.ExportGCN_NCUDetail.PlatingFormDetail.UnitPrice,
                                                 //Price = importDetail.Quantity*purchaseDetail.UnitPrice,
                                                 CurrencyCode = x.ExportGCN_NCUDetail.PlatingFormDetail.PlatingForm.CurrencyCode,
                                                 VendorId = x.ExportGCN_NCUDetail.PlatingFormDetail.PlatingForm.VendorId,
                                                 VendorName = x.ExportGCN_NCUDetail.PlatingFormDetail.PlatingForm.Vendor.VendorName,
                                                 VendorCode = x.ExportGCN_NCUDetail.PlatingFormDetail.PlatingForm.Vendor.VendorCode,
                                                 TransactionDate = x.ImportNCU_QCB.ImportDate,
                                                 ModifiedUser = x.ImportNCU_QCB.ModifiedUser,
                                                 ModifiedDate = x.ImportNCU_QCB.ModifiedDate,
                                                 TransactionCode = x.ImportNCU_QCB.TransactionCode,

                                                 //LotNumber = toolInv.LotNumber,
                                                 //FptCode = x.Product.ProductCode,
                                                 FptName = x.Product.ProductName + "-" + x.Product.ProductCode,
                                                 FptCode = x.ExportGCN_NCUDetail.PlatingFormDetail.PlatingCode,
                                                 ExchangeRate = x.ExportGCN_NCUDetail.PlatingFormDetail.PlatingForm.ExchangeRate,
                                                 Quantity = x.ExportGCN_NCUDetail.PlatingFormDetail.Unit.Contains("Kg")
                                                                 ? x.Weight / 1000
                                                                 : x.RealNumber,
                                                 LotNumber = x.ProductInventory.LotNumber,
                                                 PlatingId = x.ImportNCU_QCB.PlatingForm.FormId,
                                                 DeliveryDate = x.ExportGCN_NCUDetail.PlatingFormDetail.ImportDateRequirement,



                                             }).ToList();
                        var random = new Random();
                        foreach (var detail in importDetails) {
                            detail.Price = detail.Quantity * detail.UnitPrice * detail.ExchangeRate;
                            detail.Index = index;
                            if (detail.DeliveryDate == null) {
                                int randomDays = random.Next(0, 3);
                                detail.DeliveryDate = detail.TransactionDate;
                                detail.DeliveryDate = detail.DeliveryDate.Value.AddDays(randomDays);
                            };
                            index++;
                        }
                        model2.AddRange(importDetails);
                    }

                    if (classifiedId == 0 || classifiedId == 6) { // product

                        var importDetails = (from x in vfi.TransactionDetails
                                             where
                                                 x.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                 x.Transaction.CreatedDate >= fDate && x.Transaction.CreatedDate <= tDate &&
                                                 x.Transaction.PoId != null &&
                                                 //x.Transaction.EoI == MyUtilities.PurchaseOrder.EoILot.Import.ToString() &&
                                                 (vendorId == 0 || x.VendorId == vendorId)
                                                 && x.UnitMeasure == "Pcs"
                                             select new TransactionFptDetailModel {
                                                 FptId = x.ReferenceId.Value,
                                                 Note = x.Note,
                                                 Quantity = x.Quantity,
                                                 NG = x.NG,
                                                 UnitMeasure = x.UnitMeasure,
                                                 //UnitPrice = x.UnitPrice,
                                                 //VendorId = x.VendorId,
                                                 //VendorName = x.Vendor.VendorName,
                                                 //VendorCode = x.Vendor.VendorCode,
                                                 TransactionDate = x.Transaction.CreatedDate,
                                                 ModifiedUser = x.Transaction.ModifiedUser,
                                                 ModifiedDate = x.Transaction.ModifiedDate,
                                                 TransactionCode = x.Transaction.TransactionCode,
                                                 LotNumber = x.LotNumber,
                                                 PoId = x.Transaction.PoId.Value,
                                                 ExchangeRate = 1,
                                                 PoDetailId = (long)x.PoDetailId,
                                                 TransactionId = (long)x.TransactionId,
                                                 //CurrencyCode = x.Transaction.PurchaseOrder.CurrencyCode.Trim(),
                                                 //TaxInvoiceNumber = x.PoReferenceDetailId != null
                                                 //                ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.TaxInvoiceNumber
                                                 //                : "",
                                                 //TaxPercent = x.PoReferenceDetailId != null
                                                 //                ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.TaxPercent
                                                 //                : 0,
                                                 //ExchangeRate = x.PoReferenceDetailId != null
                                                 //                ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.ExchangeRate
                                                 //                : x.TransactionFpt.ExchangeRate,
                                                 //TaxInvoiceDate = x.PoReferenceDetailId != null
                                                 //                ? x.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice.PoDate
                                                 //                : null,

                                             }).ToList();

                        var poIds = importDetails.Select(x => x.PoId).Distinct().ToList();
                        var purchaseDetails = vfi.PurchaseOrderDetails.Where(x => poIds.Contains(x.PurchaseOrderId))
                                                .Select(x => new {
                                                    x.PurchaseOrderId,
                                                    x.ReferenceId,
                                                    x.Unit,
                                                    x.UnitPrice,
                                                    x.PurchaseOrder.CurrencyCode,
                                                    x.PurchaseOrder.Vendor.VendorName,
                                                    x.PurchaseOrder.Vendor.VendorCode,
                                                    x.PurchaseOrder.ShipDate,
                                                    x.PurchaseOrder.RevisionNumber,
                                                    
                                                })
                                                .ToList();
                        var itemIds = importDetails.Select(x => x.FptId).Distinct().ToList();
                        var items = vfi.Products.Where(x => itemIds.Contains(x.ProductId))
                            .Select(x => new { Id = x.ProductId, Name = x.ProductName, Code = x.ProductCode, DesignNo = x.DesignNo })
                            .ToList();

                        foreach (var detail in importDetails) {
                            var purchaseDetail =
                                purchaseDetails.FirstOrDefault(
                                    pod => pod.PurchaseOrderId == detail.PoId
                                           && pod.ReferenceId == detail.FptId);

                            if (purchaseDetail != null) {
                                detail.UnitMeasure = purchaseDetail.Unit;
                                detail.DeliveryDate = purchaseDetail.ShipDate;
                                detail.UnitPrice = purchaseDetail.UnitPrice;
                                detail.CurrencyCode = purchaseDetail.CurrencyCode;
                                detail.VendorCode = purchaseDetail.VendorCode;
                                detail.VendorName = purchaseDetail.VendorName;
                                detail.PoCode = purchaseDetail.RevisionNumber;
                                detail.VendorDeliveryOnDate = purchaseDetail.ShipDate.HasValue && detail.TransactionDate > purchaseDetail.ShipDate.Value;
                                detail.Index = index;
                            }
                            detail.Price = detail.Quantity * detail.UnitPrice * detail.ExchangeRate;
                            var item = items.FirstOrDefault(m => m.Id == detail.FptId);
                            if (item != null) {
                                detail.FptName = item.Name + "-" + item.Code;
                                detail.FptCode = item.DesignNo;
                                detail.FptDesignNo = item.DesignNo;
                            }
                            index++;

                        }
                        model2.AddRange(importDetails);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportPOManagement", ex.Message);
            }
            return View(new GridModel(model2));
        }

        public ActionResult ActiveNG2(long DetailId, long PoId) {
            try {
                using (var vfi = new tammaContext()) {
                    var materialClassifiedId = vfi.PurchaseOrders.Where(t => t.PurchaseOrderId == PoId).Select(t => t.MaterialClassifiedId).FirstOrDefault();
                    if (materialClassifiedId == 0) {
                        throw new AggregateException("Khong thay ma~");
                    }
                    long returnId = 0;
                    switch (materialClassifiedId) {
                        case 1:
                            var activeMaterial = vfi.ImportPurchaseOrderDetails.FirstOrDefault(x => x.ImportDetailId == DetailId);
                            if (activeMaterial == null) {
                                throw new AggregateException("Không tìm thấy phiếu nhập kho Nguyen lieu");
                            }
                            activeMaterial.NG = !activeMaterial.NG;
                            var materialInvs = vfi.MaterialInventories.Where(t => t.MaterialId == activeMaterial.MaterialId
                                                                             && t.LotNumber == activeMaterial.LotNumber
                                                                             && t.Length == activeMaterial.Length
                                                                             && t.UnitPrice == activeMaterial.UnitPrice
                                                                             && t.UnitWeight == activeMaterial.UnitWeight)
                                                                             .ToList();
                            if (materialInvs.Count == 0) {
                                throw new AggregateException("Không tìm thấy phiếu nhập kho Nguyen lieu");
                            }
                            foreach (var materialInv in materialInvs) {
                                materialInv.NG = activeMaterial.NG;
                            }
                            returnId = activeMaterial.ImportDetailId;
                            break;
                        case 2:
                            var activeFuel = vfi.TransactionFptDetails.FirstOrDefault(t => t.DetailId == DetailId);
                            if (activeFuel == null) {
                                throw new AggregateException("Không tìm thấy Nhien lieu");
                            }
                            activeFuel.NG = !activeFuel.NG;
                            var fuelInvs = vfi.FuelInventories.Where(t => t.FuelId == activeFuel.FptId
                                                                     && t.LotNumber == activeFuel.LotNumber
                                                                     && t.UnitPrice == activeFuel.UnitPrice
                                                                    ).ToList();
                            if (fuelInvs.Count == 0) {
                                throw new AggregateException("Không tìm thấy phiếu nhập kho Nhien lieu");
                            }
                            foreach (var fuelInv in fuelInvs) {
                                fuelInv.NG = activeFuel.NG;
                            }
                            returnId = activeFuel.TransactionId;
                            break;
                        case 3:
                            var activeTool = vfi.TransactionFptDetails.FirstOrDefault(t => t.DetailId == DetailId);
                            if (activeTool == null) {
                                throw new AggregateException("Không tìm thấy CC");
                            }
                            activeTool.NG = !activeTool.NG;
                            var toolInvs = vfi.ToolInventories.Where(t => t.ToolId == activeTool.FptId
                                                                     && t.LotNumber == activeTool.LotNumber
                                                                     && t.UnitPrice == activeTool.UnitPrice
                                                                    ).ToList();
                            if (toolInvs.Count == 0) {
                                throw new AggregateException("Không tìm thấy phiếu nhập kho CC");
                            }
                            foreach (var toolInv in toolInvs) {
                                toolInv.NG = activeTool.NG;
                            }
                            returnId = activeTool.TransactionId;
                            break;

                        // Phiếu GCN không có trong bảng QL Phiếu mua

                        //case 4: 
                        //    var activeGCN = vfi.ImportNCU_QCBDetail.FirstOrDefault(t => t.DetailId == DetailId);
                        //    if (activeGCN == null) {
                        //        throw new AggregateException("Không tìm thấy GCN");
                        //    }
                        //    activeGCN.NG = !activeGCN.NG;
                        //    var importGCNInvs = vfi.ProductInventories.Where(t => t.ProductInventoryId == activeGCN.ProductInvId
                        //                                                     && t.ProductId == activeGCN.ProductId
                        //                                                     && t.TotalQty == activeGCN.RealNumber
                        //                                                     ).ToList();
                        //    if (importGCNInvs.Count == 0) {
                        //        throw new AggregateException("Không tìm thấy phiếu nhập kho GCN");
                        //    }
                        //    foreach (var importGCNInv in importGCNInvs) {
                        //        importGCNInv.NG = activeGCN.NG;
                        //    }
                        //    returnId = activeGCN.DetailId;
                        //    break;

                        case 6:
                            var activeProduct = vfi.TransactionDetails.FirstOrDefault(t => t.TransactionDetailId == DetailId);
                            if (activeProduct == null){
                                throw new AggregateException("Không tìm thấy SP");
                            }
                            activeProduct.NG = !activeProduct.NG;
                            var producInvs = vfi.ProductInventories.Where(t => t.ProductId == activeProduct.ReferenceId
                                                                          && t.LotNumber == activeProduct.LotNumber
                                                                          && t.TotalQty == activeProduct.Quantity
                                                                          ).ToList();
                            if (producInvs.Count == 0){
                                throw new AggregateException("Không tìm thấy phiếu nhập kho SP");
                            }
                            foreach (var productInv in producInvs) {
                                productInv.NG = activeProduct.NG;
                            }
                            returnId = activeProduct.TransactionId ?? 0;
                            break;

                    }
                    vfi.SaveChanges();
                    return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.NoError, "", returnId));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.Exception, ex.Message, ""));
            }
        }

        // Lock lot bi NG
        public ActionResult LockId(long DetailId, long PoId) {
            try {
                using (var vfi = new tammaContext()) {
                    var materialClassifiedId = vfi.PurchaseOrders.Where(t => t.PurchaseOrderId == PoId).Select(t => t.MaterialClassifiedId).FirstOrDefault();
                    if (materialClassifiedId == 0) {
                        throw new AggregateException("Khong thay ma~");
                    }
                    long returnId = 0;
                    switch (materialClassifiedId) {
                        case 1:
                            var activeMaterial = vfi.ImportPurchaseOrderDetails.FirstOrDefault(x => x.ImportDetailId == DetailId);
                            if (activeMaterial == null) {
                                throw new AggregateException("Không tìm thấy phiếu nhập kho Nguyen lieu");
                            }
                            activeMaterial.Lock = !activeMaterial.Lock;
                            var materialInvs = vfi.MaterialInventories.Where(t => t.MaterialId == activeMaterial.MaterialId
                                                                             && t.LotNumber == activeMaterial.LotNumber
                                                                             && t.Length == activeMaterial.Length
                                                                             && t.UnitPrice == activeMaterial.UnitPrice
                                                                             && t.UnitWeight == activeMaterial.UnitWeight)
                                                                             .ToList();
                            if (materialInvs.Count == 0) {
                                throw new AggregateException("Không tìm thấy phiếu nhập kho Nguyen lieu");
                            }
                            foreach (var materialInv in materialInvs) {
                                materialInv.Lock = activeMaterial.Lock;
                            }
                            returnId = activeMaterial.ImportDetailId;
                            break;
                        case 2:
                            var activeFuel = vfi.TransactionFptDetails.FirstOrDefault(t => t.DetailId == DetailId);
                            if (activeFuel == null) {
                                throw new AggregateException("Không tìm thấy Nhien lieu");
                            }
                            activeFuel.Lock = !activeFuel.Lock;
                            var fuelInvs = vfi.FuelInventories.Where(t => t.FuelId == activeFuel.FptId
                                                                     && t.LotNumber == activeFuel.LotNumber
                                                                     && t.UnitPrice == activeFuel.UnitPrice
                                                                    ).ToList();
                            if (fuelInvs.Count == 0) {
                                throw new AggregateException("Không tìm thấy phiếu nhập kho Nhien lieu");
                            }
                            foreach (var fuelInv in fuelInvs) {
                                fuelInv.Lock = activeFuel.Lock;
                            }
                            returnId = activeFuel.TransactionId;
                            break;
                        case 3:
                            var activeTool = vfi.TransactionFptDetails.FirstOrDefault(t => t.DetailId == DetailId);
                            if (activeTool == null) {
                                throw new AggregateException("Không tìm thấy CC");
                            }
                            activeTool.Lock = !activeTool.Lock;
                            var toolInvs = vfi.ToolInventories.Where(t => t.ToolId == activeTool.FptId
                                                                     && t.LotNumber == activeTool.LotNumber
                                                                     && t.UnitPrice == activeTool.UnitPrice
                                                                    ).ToList();
                            if (toolInvs.Count == 0) {
                                throw new AggregateException("Không tìm thấy phiếu nhập kho CC");
                            }
                            foreach (var toolInv in toolInvs) {
                                toolInv.Lock = activeTool.Lock;
                            }
                            returnId = activeTool.TransactionId;
                            break;

                        // Phiếu GCN không có trong bảng QL Phiếu mua

                        //case 4: 
                        //    var activeGCN = vfi.ImportNCU_QCBDetail.FirstOrDefault(t => t.DetailId == DetailId);
                        //    if (activeGCN == null) {
                        //        throw new AggregateException("Không tìm thấy GCN");
                        //    }
                        //    activeGCN.Lock = !activeGCN.Lock;
                        //    var importGCNInvs = vfi.ProductInventories.Where(t => t.ProductInventoryId == activeGCN.ProductInvId
                        //                                                     && t.ProductId == activeGCN.ProductId
                        //                                                     && t.TotalQty == activeGCN.RealNumber
                        //                                                     ).ToList();
                        //    if (importGCNInvs.Count == 0) {
                        //        throw new AggregateException("Không tìm thấy phiếu nhập kho GCN");
                        //    }
                        //    foreach (var importGCNInv in importGCNInvs) {
                        //        importGCNInv.Lock = activeGCN.Lock;
                        //    }
                        //    returnId = activeGCN.DetailId;
                        //    break;

                        //case 6:
                        //    var activeProduct = vfi.TransactionDetails.FirstOrDefault(t => t.TransactionDetailId == DetailId);
                        //    if (activeProduct == null) {
                        //        throw new AggregateException("Không tìm thấy SP");
                        //    }
                        //    activeProduct.Lock = !activeProduct.Lock;
                        //    var producInvs = vfi.ProductInventories.Where(t => t.ProductId == activeProduct.ReferenceId
                        //                                                  && t.LotNumber == activeProduct.LotNumber
                        //                                                  && t.TotalQty == activeProduct.Quantity
                        //                                                  ).ToList();
                        //    if (producInvs.Count == 0) {
                        //        throw new AggregateException("Không tìm thấy phiếu nhập kho SP");
                        //    }
                        //    foreach (var productInv in producInvs) {
                        //        productInv.Lock = activeProduct.Lock;
                        //    }
                        //    returnId = activeProduct.TransactionId ?? 0;
                        //    break;

                    }
                    vfi.SaveChanges();
                    return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.NoError, "", returnId));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.Exception, ex.Message, ""));
            }
        }


        //public ActionResult ActiveNG(int? DetailId) {
        //    try {
        //        using (var vfi = new tammaContext()) {
        //            var active = vfi.ImportPurchaseOrderDetails.FirstOrDefault(x => x.ImportDetailId == DetailId);
        //            if (active == null) {
        //                throw new AggregateException("Không tìm thấy phiếu nhập kho");
        //            }
        //            active.NG = !active.NG;
        //            var materialInvs = vfi.MaterialInventories.Where(t => t.MaterialId == active.MaterialId
        //                                                             && t.LotNumber == active.LotNumber
        //                                                             && t.Length == active.Length
        //                                                             && t.UnitPrice == active.UnitPrice
        //                                                             && t.UnitWeight == active.UnitWeight)
        //                                                             .ToList();
        //            if (materialInvs.Count == 0) {
        //                throw new AggregateException("Khong thay NL");
        //            }
        //            foreach (var materialInv in materialInvs) {
        //                materialInv.NG = active.NG;
        //            }

        //            vfi.SaveChanges();
        //            return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "", active.ImportDetailId));
        //        }
        //    }
        //    catch (Exception ex) {
        //        return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.Exception, ex.Message, ""));
        //    }
        //}




        private void GetPoTaxInvoice(PoTaxInvoiceReport entity) {
            using (var vfi = new tammaContext()) {
                //2
                var taxInvoices =
                    vfi.PoTaxInvoices.Where(
                        ti =>
                        ti.PoDate < entity.FromDate && ti.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                        ti.VendorId == entity.VendorId &&
                        ti.CurrencyCode.Equals("USD")).ToList();
                entity.LastMonthRequireUsd =
                    Math.Round(
                        taxInvoices.Sum(ti => ti.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice)),
                        2);
                var receive =
                    vfi.PoTaxInvoiceMoneys.Where(
                        tim =>
                        tim.ImportDate < entity.FromDate &&
                        tim.PoTaxInvoice.VendorId == entity.VendorId &&
                        tim.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                        tim.PoTaxInvoice.CurrencyCode.Equals("USD")).ToList();
                entity.LastMonthRequireUsd -= Math.Round(receive.Sum(tim => Math.Abs(tim.Money)), 2);

                taxInvoices =
                    vfi.PoTaxInvoices.Where(
                        ti =>
                        ti.PoDate < entity.FromDate && ti.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                        ti.VendorId == entity.VendorId &&
                        !ti.CurrencyCode.Equals("USD")).ToList();
                entity.LastMonthRequireVnd =
                    Math.Round(taxInvoices.Sum(
                        ti =>
                        (ti.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice) +
                         (ti.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice) * ti.TaxPercent / 100)) *
                        ti.ExchangeRate), 0);
                receive =
                    vfi.PoTaxInvoiceMoneys.Where(
                        tim =>
                        tim.ImportDate < entity.FromDate &&
                        tim.PoTaxInvoice.VendorId == entity.VendorId &&
                        tim.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                        !tim.PoTaxInvoice.CurrencyCode.Equals("USD")).ToList();
                entity.LastMonthRequireVnd -= Math.Round(receive.Sum(tim => Math.Abs(tim.Money)), 0);
                //5
                taxInvoices =
                    vfi.PoTaxInvoices.Where(
                        ti =>
                        ti.PoDate >= entity.FromDate && ti.PoDate <= entity.ToDate &&
                        ti.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                        ti.VendorId == entity.VendorId &&
                        ti.CurrencyCode.Equals("USD")).ToList();
                entity.InMonthTaxInvoiceUsd =
                    Math.Round(
                        taxInvoices.Sum(ti => ti.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice)),
                        2);
                taxInvoices =
                    vfi.PoTaxInvoices.Where(
                        ti =>
                        ti.PoDate >= entity.FromDate && ti.PoDate <= entity.ToDate &&
                        ti.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                        ti.VendorId == entity.VendorId &&
                        !ti.CurrencyCode.Equals("USD")).ToList();
                entity.InMonthTaxInvoiceVnd = Math.Round(taxInvoices.Sum(
                    ti =>
                    (ti.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice) +
                     (ti.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice) * ti.TaxPercent / 100)) *
                    ti.ExchangeRate), 0);
                //6
                receive =
                    vfi.PoTaxInvoiceMoneys.Where(
                        tim =>
                        tim.ImportDate >= entity.FromDate && tim.ImportDate <= entity.ToDate &&
                        tim.PoTaxInvoice.VendorId == entity.VendorId &&
                        tim.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                        tim.PoTaxInvoice.CurrencyCode.Equals("USD") &&
                        tim.Money > 0).ToList();
                entity.ReceiveInMonthUsd = Math.Round(receive.Sum(tim => Math.Abs(tim.Money)), 2);

                receive =
                    vfi.PoTaxInvoiceMoneys.Where(
                        tim =>
                        tim.ImportDate >= entity.FromDate && tim.ImportDate <= entity.ToDate &&
                        tim.PoTaxInvoice.VendorId == entity.VendorId &&
                        tim.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                        !tim.PoTaxInvoice.CurrencyCode.Equals("USD") &&
                        tim.Money > 0).ToList();
                entity.ReceiveInMonthVnd = Math.Round(receive.Sum(tim => Math.Abs(tim.Money)), 0);

                //7
                receive =
                    vfi.PoTaxInvoiceMoneys.Where(
                        tim =>
                        tim.ImportDate >= entity.FromDate && tim.ImportDate <= entity.ToDate &&
                        tim.PoTaxInvoice.VendorId == entity.VendorId &&
                        tim.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                        tim.PoTaxInvoice.CurrencyCode.Equals("USD") &&
                        tim.Money < 0).ToList();
                entity.ReduceInMonthUsd = Math.Round(receive.Sum(tim => Math.Abs(tim.Money)), 2);

                receive =
                    vfi.PoTaxInvoiceMoneys.Where(
                        tim =>
                        tim.ImportDate >= entity.FromDate && tim.ImportDate <= entity.FromDate &&
                        tim.PoTaxInvoice.VendorId == entity.VendorId &&
                        tim.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                        !tim.PoTaxInvoice.CurrencyCode.Equals("USD") &&
                        tim.Money < 0).ToList();
                entity.ReduceInMonthVnd = Math.Round(receive.Sum(tim => Math.Abs(tim.Money)), 0);
            }
        }

        [GridAction]
        public ActionResult SelectVendorInDept(int vendorId, string fromDate, string toDate) {
            var model = new List<PoTaxInvoiceReport>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fDate = string.IsNullOrWhiteSpace(fromDate)
                                ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
                                : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                                ? DateTime.Now
                                : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    var index = 0;
                    var vendors = vfi.Vendors.Where(v => v.Active && v.MaterialClassifiedId == 1 && (vendorId == 0 || v.VendorId == vendorId));
                    foreach (var vendor in vendors) {
                        PoTaxInvoiceReport entity = new PoTaxInvoiceReport {
                            ClassifiedId = vendor.MaterialClassifiedId.Value,
                            ClassifiedName = vendor.MaterialClassified.MaterialClassifiedName,
                            VendorId = vendor.VendorId,
                            VendorName = vendor.VendorName,
                            FromDate = fDate,
                            ToDate = tDate,
                            //Index = ++index
                        };
                        //1
                        var importDetails =
                            vfi.ImportPurchaseOrderDetails.Where(
                                i =>
                                i.ImportPurchaseOrder.ImportDate < entity.FromDate &&
                                i.ImportPurchaseOrder.ImportDate > MyUtilities.PurchaseOrder.StartTaxInvoiceDate &&
                                i.ImportPurchaseOrder.PurchaseOrderId != null &&
                                i.ImportPurchaseOrder.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                i.PoReferenceDetailId == null &&
                                i.ImportPurchaseOrder.PurchaseOrder.VendorId == vendor.VendorId).ToList();

                        foreach (var importDetail in importDetails) {
                            var purchaseDetail =
                                vfi.PurchaseOrderDetails.FirstOrDefault(
                                    pod =>
                                    pod.PurchaseOrderId ==
                                    importDetail.ImportPurchaseOrder.PurchaseOrder.PurchaseOrderId
                                    && pod.ReferenceId == importDetail.MaterialId);
                            var detail = new ImportPoReport {
                                CurrencyCode = purchaseDetail.PurchaseOrder.CurrencyCode.Trim(),
                                ExchangeRate = Convert.ToInt32(importDetail.ImportPurchaseOrder.ExchangeRate),
                                Quantity = importDetail.QuantityKg,
                                UnitPrice = purchaseDetail.UnitPrice,
                            };
                            if (!detail.CurrencyCode.Equals("USD")) {
                                detail.CurrencyCode = "VND";
                                detail.UnitPrice = purchaseDetail.UnitPrice * detail.ExchangeRate;
                            }
                            entity.ImportLastMonths.Add(detail);
                        }
                        //2
                        importDetails =
                            vfi.ImportPurchaseOrderDetails.Where(
                                i =>
                                i.ImportPurchaseOrder.ImportDate >= entity.FromDate &&
                                i.ImportPurchaseOrder.ImportDate <= entity.ToDate &&
                                i.ImportPurchaseOrder.PurchaseOrderId != null &&
                                i.ImportPurchaseOrder.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                    //i.PoReferenceDetailId == null &&
                                i.ImportPurchaseOrder.PurchaseOrder.VendorId == vendor.VendorId).ToList();
                        foreach (var importDetail in importDetails) {
                            var purchaseDetail =
                                vfi.PurchaseOrderDetails.FirstOrDefault(
                                    pod =>
                                    pod.PurchaseOrderId ==
                                    importDetail.ImportPurchaseOrder.PurchaseOrder.PurchaseOrderId
                                    && pod.ReferenceId == importDetail.MaterialId);
                            var detail = new ImportPoReport {
                                CurrencyCode = purchaseDetail.PurchaseOrder.CurrencyCode.Trim(),
                                ExchangeRate = Convert.ToInt32(importDetail.ImportPurchaseOrder.ExchangeRate),
                                Quantity = importDetail.QuantityKg,
                                UnitPrice = purchaseDetail.UnitPrice,
                            };
                            if (!detail.CurrencyCode.Equals("USD")) {
                                detail.CurrencyCode = "VND";
                                detail.UnitPrice = purchaseDetail.UnitPrice * detail.ExchangeRate;
                            }
                            if (importDetail.PoReferenceDetailId != null) {
                                detail.TaxPercent =
                                    importDetail.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice
                                                .TaxPercent;
                            }
                            entity.ImportInMonths.Add(detail);
                        }
                        GetPoTaxInvoice(entity);
                        if (entity.Show) {
                            entity.Index = ++index;
                            model.Add(entity);
                        }
                    }

                    vendors = vfi.Vendors.Where(v => v.Active && v.MaterialClassifiedId == 2 && (vendorId == 0 || v.VendorId == vendorId));
                    foreach (var vendor in vendors) {
                        var entity = new PoTaxInvoiceReport {
                            ClassifiedId = vendor.MaterialClassifiedId.Value,
                            ClassifiedName = vendor.MaterialClassified.MaterialClassifiedName,
                            VendorId = vendor.VendorId,
                            VendorName = vendor.VendorName,
                            FromDate = fDate,
                            ToDate = tDate,
                            //Index = ++index
                        };
                        //1
                        var transactionDetails = from t in vfi.TransactionFptDetails
                                                 where
                                                     t.TransactionFpt.Status ==
                                                     (byte)MyUtilities.Transaction.Status.Approved &&
                                                     t.TransactionFpt.TransactionDate < entity.FromDate &&
                                                     t.TransactionFpt.TransactionDate >
                                                     MyUtilities.PurchaseOrder.StartTaxInvoiceDate &&
                                                     t.TransactionFpt.PoId != null &&
                                                     t.TransactionFpt.PurchaseOrder.VendorId == vendor.VendorId &&
                                                     t.TransactionFpt.EoI ==
                                                     (int)MyUtilities.PurchaseOrder.EoILot.Import &&
                                                     t.TransactionFpt.Fpt == (int)MyUtilities.PurchaseOrder.FptLot.Fuel &&
                                                     t.PoReferenceDetailId == null
                                                 select t;
                        foreach (var importDetail in transactionDetails) {
                            var purchaseDetail =
                                vfi.PurchaseOrderDetails.FirstOrDefault(
                                    pod => pod.PurchaseOrderId == importDetail.TransactionFpt.PoId
                                           && pod.ReferenceId == importDetail.FptId);
                            var detail = new ImportPoReport {
                                CurrencyCode = purchaseDetail.PurchaseOrder.CurrencyCode.Trim(),
                                ExchangeRate = Convert.ToInt32(importDetail.TransactionFpt.ExchangeRate),
                                Quantity = importDetail.Quantity,
                                UnitPrice = purchaseDetail.UnitPrice,
                            };
                            if (!detail.CurrencyCode.Equals("USD")) {
                                detail.CurrencyCode = "VND";
                                detail.UnitPrice = purchaseDetail.UnitPrice * detail.ExchangeRate;
                            }
                            entity.ImportLastMonths.Add(detail);
                        }
                        //4
                        transactionDetails = from t in vfi.TransactionFptDetails
                                             where
                                                 t.TransactionFpt.Status ==
                                                 (byte)MyUtilities.Transaction.Status.Approved &&
                                                 t.TransactionFpt.TransactionDate >= entity.FromDate &&
                                                 t.TransactionFpt.TransactionDate <= entity.ToDate &&
                                                 t.TransactionFpt.PoId != null &&
                                                 t.TransactionFpt.PurchaseOrder.VendorId == vendor.VendorId &&
                                                 t.TransactionFpt.EoI ==
                                                 (int)MyUtilities.PurchaseOrder.EoILot.Import &&
                                                 t.TransactionFpt.Fpt == (int)MyUtilities.PurchaseOrder.FptLot.Fuel
                                             select t;
                        foreach (var importDetail in transactionDetails) {
                            var purchaseDetail =
                               vfi.PurchaseOrderDetails.FirstOrDefault(
                                   pod => pod.PurchaseOrderId == importDetail.TransactionFpt.PoId
                                          && pod.ReferenceId == importDetail.FptId);
                            var detail = new ImportPoReport {
                                CurrencyCode = purchaseDetail.PurchaseOrder.CurrencyCode.Trim(),
                                ExchangeRate = Convert.ToInt32(importDetail.TransactionFpt.ExchangeRate),
                                Quantity = importDetail.Quantity,
                                UnitPrice = purchaseDetail.UnitPrice,
                            };
                            if (!detail.CurrencyCode.Equals("USD")) {
                                detail.CurrencyCode = "VND";
                                detail.UnitPrice = purchaseDetail.UnitPrice * detail.ExchangeRate;
                            }
                            if (importDetail.PoReferenceDetailId != null) {
                                detail.TaxPercent =
                                    importDetail.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice
                                                .TaxPercent;
                            }
                            entity.ImportInMonths.Add(detail);
                        }
                        GetPoTaxInvoice(entity);
                        if (entity.Show) {
                            entity.Index = ++index;
                            model.Add(entity);
                        }
                    }
                    vendors = vfi.Vendors.Where(v => v.Active && v.MaterialClassifiedId == 3 && (vendorId == 0 || v.VendorId == vendorId));
                    foreach (var vendor in vendors) {
                        var entity = new PoTaxInvoiceReport {
                            ClassifiedId = vendor.MaterialClassifiedId.Value,
                            ClassifiedName = vendor.MaterialClassified.MaterialClassifiedName,
                            VendorId = vendor.VendorId,
                            VendorName = vendor.VendorName,
                            FromDate = fDate,
                            ToDate = tDate,
                            //Index = ++index
                        };
                        //1
                        var transactionDetails = from t in vfi.TransactionFptDetails
                                                 where
                                                     t.TransactionFpt.Status ==
                                                     (byte)MyUtilities.Transaction.Status.Approved &&
                                                     t.TransactionFpt.TransactionDate < entity.FromDate &&
                                t.TransactionFpt.TransactionDate > MyUtilities.PurchaseOrder.StartTaxInvoiceDate &&
                                                     t.TransactionFpt.PoId != null &&
                                                     t.TransactionFpt.PurchaseOrder.VendorId == vendor.VendorId &&
                                                     t.TransactionFpt.EoI ==
                                                     (int)MyUtilities.PurchaseOrder.EoILot.Import &&
                                                     t.TransactionFpt.Fpt == (int)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                                     t.PoReferenceDetailId == null
                                                 select t;
                        foreach (var importDetail in transactionDetails) {
                            var purchaseDetail =
                                vfi.PurchaseOrderDetails.FirstOrDefault(
                                    pod => pod.PurchaseOrderId == importDetail.TransactionFpt.PoId
                                           && pod.ReferenceId == importDetail.FptId);
                            var detail = new ImportPoReport {
                                CurrencyCode = purchaseDetail.PurchaseOrder.CurrencyCode.Trim(),
                                ExchangeRate = Convert.ToInt32(importDetail.TransactionFpt.ExchangeRate),
                                Quantity = importDetail.Quantity,
                                UnitPrice = purchaseDetail.UnitPrice,
                            };
                            if (!detail.CurrencyCode.Equals("USD")) {
                                detail.CurrencyCode = "VND";
                                detail.UnitPrice = purchaseDetail.UnitPrice * detail.ExchangeRate;
                            }
                            entity.ImportLastMonths.Add(detail);
                        }
                        //4
                        transactionDetails = from t in vfi.TransactionFptDetails
                                             where
                                                 t.TransactionFpt.Status ==
                                                 (byte)MyUtilities.Transaction.Status.Approved &&
                                                 t.TransactionFpt.TransactionDate >= entity.FromDate &&
                                                 t.TransactionFpt.TransactionDate <= entity.ToDate &&
                                                 t.TransactionFpt.PoId != null &&
                                                 t.TransactionFpt.PurchaseOrder.VendorId == vendor.VendorId &&
                                                 t.TransactionFpt.EoI ==
                                                 (int)MyUtilities.PurchaseOrder.EoILot.Import &&
                                                 t.TransactionFpt.Fpt == (int)MyUtilities.PurchaseOrder.FptLot.Tool
                                             select t;
                        foreach (var importDetail in transactionDetails) {
                            var purchaseDetail =
                               vfi.PurchaseOrderDetails.FirstOrDefault(
                                   pod => pod.PurchaseOrderId == importDetail.TransactionFpt.PoId
                                          && pod.ReferenceId == importDetail.FptId);
                            var detail = new ImportPoReport {
                                CurrencyCode = purchaseDetail.PurchaseOrder.CurrencyCode.Trim(),
                                ExchangeRate = Convert.ToInt32(importDetail.TransactionFpt.ExchangeRate),
                                Quantity = importDetail.Quantity,
                                UnitPrice = purchaseDetail.UnitPrice,
                            };
                            if (!detail.CurrencyCode.Equals("USD")) {
                                detail.CurrencyCode = "VND";
                                detail.UnitPrice = purchaseDetail.UnitPrice * detail.ExchangeRate;
                            }
                            if (importDetail.PoReferenceDetailId != null) {
                                detail.TaxPercent =
                                    importDetail.PoTaxInvoiceReferenceDetail.PoTaxInvoiceReference.PoTaxInvoice
                                                .TaxPercent;
                            }
                            entity.ImportInMonths.Add(detail);
                        }
                        GetPoTaxInvoice(entity);
                        if (entity.Show) {
                            entity.Index = ++index;
                            model.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectVendorInDept", ex.Message);
            }
            return View(new GridModel(model));
        }
        [GridAction]
        public ActionResult SelectImportPoManagement(string fromDate, string toDate) {
            var model = new List<TransactionFptModel>();
            var model2 = new List<TransactionFptDetailModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fDate = string.IsNullOrWhiteSpace(fromDate)
                                ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
                                : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                                ? DateTime.Now
                                : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    var importMaterials =
                        vfi.ImportPurchaseOrders.Where(
                            i =>
                            i.ImportDate >= fDate && i.ImportDate <= tDate &&
                            i.PurchaseOrderId != null &&
                            i.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);

                    foreach (var importPo in importMaterials) {
                        var transaction = importPo.Transaction;
                        var entity = new TransactionFptModel {
                            TransactionId = transaction.TransactionId,
                            TransactionCode = transaction.TransactionCode,
                            ModifiedDate = transaction.ModifiedDate,
                            ModifiedUser = transaction.ModifiedUser,
                            TransactionDate = transaction.CreatedDate,
                            PoId = importPo.PurchaseOrderId.Value,
                            PoCode = importPo.PurchaseOrder.RevisionNumber,
                            Fpt = importPo.PurchaseOrder.MaterialClassifiedId,
                            FptName = importPo.PurchaseOrder.MaterialClassified.MaterialClassifiedName,
                            VendorId = importPo.PurchaseOrder.VendorId,
                            VendorName = importPo.PurchaseOrder.Vendor.VendorName,
                            CurrencyCode = importPo.PurchaseOrder.CurrencyCode.Trim(),
                        };
                        foreach (var importDetail in importPo.ImportPurchaseOrderDetails) {
                            //var importDetail =
                            //    importPoMaterial.ImportPurchaseOrderDetails.FirstOrDefault(
                            //        id => id.MaterialId == detail.ReferenceId);
                            var material =
                                vfi.Materials.FirstOrDefault(m => m.MaterialId == importDetail.MaterialId);
                            var purchaseDetail =
                                vfi.PurchaseOrderDetails.FirstOrDefault(
                                    pod => pod.PurchaseOrderId == importPo.PurchaseOrderId
                                           && pod.ReferenceId == importDetail.MaterialId);
                            var materialInv =
                                vfi.MaterialInventories.FirstOrDefault(
                                    mi =>
                                    mi.MaterialId == importDetail.MaterialId &&
                                    mi.LotNumber.Equals(importDetail.LotNumber) &&
                                    mi.VendorId == importDetail.VendorId);
                            var detail = new TransactionFptDetailModel {
                                LotNumber = materialInv.LotNumber,
                                Note = importDetail.Note,
                                Quantity = importDetail.QuantityKg,
                                FuelCode = material.MaterialCode,
                                FuelName = material.MaterialName,
                                //FuelDesignNo = MaterialModel.GetDesignNo(material),
                                UnitMeasure = importDetail.StoreCode,
                                UnitPrice = purchaseDetail.UnitPrice,
                                Price = importDetail.QuantityKg * purchaseDetail.UnitPrice,
                                CurrencyCode = purchaseDetail.PurchaseOrder.CurrencyCode.Trim(),
                                VendorId = purchaseDetail.PurchaseOrder.VendorId,
                                VendorName = purchaseDetail.PurchaseOrder.Vendor.VendorName,
                                VendorCode = purchaseDetail.PurchaseOrder.Vendor.VendorCode,
                                TransactionDate = importPo.ImportDate,
                                ModifiedUser = importPo.ModifiedUser,
                                ModifiedDate = importPo.ModifiedDate,
                            };
                            detail.FuelDesignNo = MyUtilities.Material.GetMaterialDesignNo(material.OutDiameter,
                                                                                           material.InDiameter,
                                                                                           material.DiameterType,
                                                                                           material.Shape);
                            model2.Add(detail);
                            entity.Details.Add(detail);
                        }
                        entity.TotalQuantity = entity.Details.Sum(d => d.Quantity);
                        entity.TotalPrice = entity.Details.Sum(d => d.Price);
                        model.Add(entity);
                    }

                    var transactions = from i in vfi.TransactionFpts
                                       where
                                           i.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                           i.TransactionDate >= fDate && i.TransactionDate <= tDate &&
                                           i.PoId != null &&
                                           i.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import
                                       select i;
                    foreach (var transaction in transactions) {
                        var entity = new TransactionFptModel {
                            TransactionId = transaction.TransactionId,
                            TransactionCode = transaction.TransactionCode,
                            ModifiedDate = transaction.ModifiedDate,
                            ModifiedUser = transaction.ModifiedUser,
                            TransactionDate = transaction.TransactionDate,
                            PoId = transaction.PoId.Value,
                            PoCode = transaction.PurchaseOrder.RevisionNumber,
                            Fpt = transaction.PurchaseOrder.MaterialClassifiedId,
                            FptName = transaction.PurchaseOrder.MaterialClassified.MaterialClassifiedName,
                            VendorId = transaction.PurchaseOrder.VendorId,
                            VendorName = transaction.PurchaseOrder.Vendor.VendorName,
                            CurrencyCode = transaction.PurchaseOrder.CurrencyCode.Trim(),
                        };
                        foreach (var importDetail in transaction.TransactionFptDetails) {
                            var purchaseDetail =
                                vfi.PurchaseOrderDetails.FirstOrDefault(
                                    pod => pod.PurchaseOrderId == transaction.PoId
                                           && pod.ReferenceId == importDetail.FptId);
                            var detail = new TransactionFptDetailModel {
                                //LotNumber = fuelInv.LotNumber,
                                Note = importDetail.Note,
                                Quantity = importDetail.Quantity,
                                ////FuelCode = fuel.FuelFullCode,
                                //FuelName = fuel.FuelName,
                                //FuelDesignNo = fuel.FuelDesignNo,
                                UnitMeasure = importDetail.UnitMeasure,
                                UnitPrice = purchaseDetail.UnitPrice,
                                Price = importDetail.Quantity * purchaseDetail.UnitPrice,
                                CurrencyCode = transaction.PurchaseOrder.CurrencyCode.Trim(),
                                VendorId = purchaseDetail.PurchaseOrder.VendorId,
                                VendorName = purchaseDetail.PurchaseOrder.Vendor.VendorName,
                                VendorCode = purchaseDetail.PurchaseOrder.Vendor.VendorCode,
                                TransactionDate = transaction.TransactionDate,
                                ModifiedUser = transaction.ModifiedUser,
                                ModifiedDate = transaction.ModifiedDate,
                            };
                            switch (purchaseDetail.PurchaseOrder.MaterialClassifiedId) {
                                case 2:
                                    var fuel =
                                        vfi.Fuels.FirstOrDefault(m => m.FuelId == importDetail.FptId);
                                    var fuelInv =
                                        vfi.FuelInventories.FirstOrDefault(
                                            mi =>
                                            mi.FuelId == importDetail.FptId &&
                                            mi.LotNumber.Equals(importDetail.LotNumber) &&
                                            mi.VendorId == importDetail.VendorId);
                                    detail.LotNumber = fuelInv.LotNumber;
                                    detail.FuelCode = fuel.FuelFullCode;
                                    detail.FuelName = fuel.FuelName;
                                    detail.FuelDesignNo = fuel.FuelDesignNo;
                                    break;
                                case 3:
                                    var tool =
                                        vfi.Tools.FirstOrDefault(m => m.ToolId == detail.FptId);
                                    var toolInv =
                                        vfi.ToolInventories.FirstOrDefault(
                                            mi =>
                                            mi.ToolId == detail.FptId &&
                                            mi.LotNumber.Equals(detail.LotNumber) &&
                                            mi.VendorId == detail.VendorId);
                                    detail.LotNumber = toolInv.LotNumber;
                                    detail.FuelCode = tool.ToolFullCode;
                                    detail.FuelName = tool.ToolName;
                                    detail.FuelDesignNo = tool.ToolDesignNo;
                                    break;
                                default:
                                    break;


                            }
                            model2.Add(detail);
                            entity.Details.Add(detail);
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportPOManagement", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.Fpt).ThenBy(m => m.TransactionDate)));
        }

        public ActionResult SelectPOorPONote(int? type, int? classtified) {
            using (var vfi = new tammaContext()) {
                if (type == 1) {
                    var purchaseOrders = from po in vfi.PurchaseOrders
                                         where
                                             po.Active &&
                                             (po.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                              po.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                             po.MaterialClassifiedId == classtified
                                         orderby po.ShipDate
                                         select new {
                                             po.PurchaseOrderId,
                                             Description = po.RevisionNumber + " - " + po.Vendor.VendorName
                                         };
                    return new JsonResult {
                        Data = new SelectList(purchaseOrders.ToList(), "PurchaseOrderId", "Description")
                    };
                }
                //else if (type == 2)
                //{
                //    var models =
                //        vfi.OrderNotes.Where(
                //            on =>
                //            on.NoteType == 2 && on.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                //            !(on.IsComplete ?? false))
                //           .ToList();
                //    return new JsonResult
                //    {
                //        Data = new SelectList(models, "NoteId", "NoteNumber")
                //    };
                //}
            }
            return new JsonResult {
                Data = new SelectList(new List<string>(), "", "")
            };
        }

        public ActionResult SelectApprovedPo() {
            using (var vfi = new tammaContext()) {
                var purchaseOrders = from po in vfi.PurchaseOrders
                                     where
                                         po.Active &&
                                         (po.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                          po.Status == (byte)MyUtilities.Sales.Status.InProcess) &&
                                         po.ShipDate != null
                                     orderby po.ShipDate
                                     select new {
                                         po.PurchaseOrderId,
                                         Description = po.RevisionNumber + " - " + po.Vendor.VendorName
                                     };
                return new JsonResult {
                    Data = new SelectList(purchaseOrders.ToList(), "PurchaseOrderId", "Description")
                };
            }
            return new JsonResult {
                Data = new SelectList(new List<string>(), "", "")
            };
        }

        public ActionResult SelectPoInfo(long poId) {
            using (var vfi = new tammaContext()) {
                var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == poId);
                if (purchaseOrder != null) {
                    var data = new List<string>
                        {
                            purchaseOrder.CurrencyCode + "",
                            purchaseOrder.Note + "",
                        };
                    return new JsonResult {
                        Data = data
                    };
                }
            }
            return new JsonResult {
                Data = new SelectList(new List<string>(), "", "")
            };
        }
        public ActionResult SelectPONeedToCancel(int? type) {
            using (var vfi = new tammaContext()) {
                if (type == 1) {
                    var purchaseOrders = from po in vfi.PurchaseOrders
                                         where
                                             po.Active &&
                                             (po.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                                              po.Status == (byte)MyUtilities.Sales.Status.InProcess)

                                         select new {
                                             po.PurchaseOrderId,
                                             po.RevisionNumber,
                                         };
                    return new JsonResult {
                        Data = new SelectList(purchaseOrders.ToList(), "PurchaseOrderId", "RevisionNumber")
                    };
                }
                //else if (type == 2)
                //{
                //    var models =
                //        vfi.OrderNotes.Where(
                //            on =>
                //            on.NoteType == 2 && on.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                //            !(on.IsComplete ?? false))
                //           .ToList();
                //    return new JsonResult
                //    {
                //        Data = new SelectList(models, "NoteId", "NoteNumber")
                //    };
                //}
            }
            return new JsonResult {
                Data = new SelectList(new List<string>(), "", "")
            };
        }

        [GridAction]
        public ActionResult SelectApprovedPurchaseOrder() {
            try {
                return View(new GridModel(GetApprovedPO().OrderByDescending(po => po.ShipDate)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectApprovedPurchaseOrder", ex.Message);
            }
            return View(new GridModel(new List<PurchaseOrderModel>()));
        }

        [GridAction]
        public ActionResult UpdateStatusPurchaseOrder(PurchaseOrderModel updated) {
            try {
                using (var vfi = new tammaContext()) {
                    var purchaseOrder =
                        vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == updated.PurchaseOrderId);
                    if (purchaseOrder == null)
                        throw new AggregateException("Lỗi! Không tìm thấy đơn đặt hàng");
                    byte newStatus = 0;
                    try {
                        newStatus = Convert.ToByte(updated.StatusName);
                    }
                    catch (FormatException) {

                    }
                    if (newStatus == (byte)MyUtilities.Sales.Status.Completed) {
                        switch (purchaseOrder.MaterialClassifiedId) {
                            case 1: {
                                    var isImport = vfi.ImportPurchaseOrders.Any(x =>
                                            x.PurchaseOrderId == updated.PurchaseOrderId &&
                                            x.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open);
                                    if (isImport) {
                                        throw new AggregateException("Lỗi! Còn phiếu nhập mua chưa xử lý");
                                    }
                                }
                                break;
                            case 2:
                            case 3: {
                                    var isImport = vfi.TransactionFpts.Any(x =>
                                        x.PoId == updated.PurchaseOrderId &&
                                        x.Status == (byte)MyUtilities.Transaction.Status.Open);
                                    if (isImport) {
                                        throw new AggregateException("Lỗi! Còn phiếu nhập mua chưa xử lý");
                                    }
                                }
                                break;
                            //case 5:
                            case 6: {
                                    var isImport = vfi.Transactions.Any(x =>
                                        x.PoId == updated.PurchaseOrderId &&
                                        x.Status == (byte)MyUtilities.Transaction.Status.Open);
                                    if (isImport) {
                                        throw new AggregateException("Lỗi! Còn phiếu nhập mua chưa xử lý");
                                    }
                                }
                                break;
                            default:
                                throw new AggregateException("Lỗi! Chưa xử lý trường hợp này");
                        }
                    }
                    else if (newStatus == (byte)MyUtilities.Sales.Status.Cancel) {
                        switch (purchaseOrder.MaterialClassifiedId) {
                            case 1: {
                                    var isImport = vfi.ImportPurchaseOrders.Any(x =>
                                        x.PurchaseOrderId == updated.PurchaseOrderId && (
                                        x.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open
                                        || x.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved));
                                    if (isImport) {
                                        throw new AggregateException("Lỗi! Còn phiếu nhập mua chưa xử lý");
                                    }
                                }
                                break;
                            case 2:
                            case 3: {
                                    var isImport = vfi.TransactionFpts.Any(x =>
                                        x.PoId == updated.PurchaseOrderId && (
                                        x.Status == (byte)MyUtilities.Transaction.Status.Open
                                        || x.Status == (byte)MyUtilities.Transaction.Status.Approved));
                                    if (isImport) {
                                        throw new AggregateException("Lỗi! Còn phiếu nhập mua chưa xử lý");
                                    }
                                }
                                break;
                            //case 5:
                            case 6: {
                                    var isImport = vfi.Transactions.Any(x =>
                                        x.PoId == updated.PurchaseOrderId && (
                                        x.Status == (byte)MyUtilities.Transaction.Status.Open
                                        || x.Status == (byte)MyUtilities.Transaction.Status.Approved));
                                    if (isImport) {
                                        throw new AggregateException("Lỗi! Còn phiếu nhập mua chưa xử lý");
                                    }
                                }
                                break;
                            default:
                                throw new AggregateException("Lỗi! Chưa xử lý trường hợp này");
                        }
                    }
                    if (newStatus > 0) {
                        purchaseOrder.Status = newStatus;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateStatusPurchaseOrder", ex.Message);
            }
            return View(new GridModel(GetApprovedPO().OrderByDescending(po => po.ShipDate)));
        }

        private List<PurchaseOrderModel> GetApprovedPO() {
            var model = new List<PurchaseOrderModel>();
            using (var vfi = new tammaContext()) {
                var purchaseOrders =
                    vfi.PurchaseOrders.Where(
                        po =>
                        po.ShipDate != null && po.Active &&
                        (po.Status == (byte)MyUtilities.Sales.Status.Waiting ||
                         po.Status == (byte)MyUtilities.Sales.Status.InProcess));
                foreach (var po in purchaseOrders) {
                    var entity = new PurchaseOrderModel {
                        PurchaseOrderId = po.PurchaseOrderId,
                        ModifiedUser = po.ModifiedUser,
                        ModifiedDate = po.ModifiedDate,
                        EmployeeName = po.EmployeeName,
                        VendorName = po.Vendor.VendorName,
                        TotalQuality = po.PurchaseOrderDetails.Sum(pod => pod.OrderQty),
                        ShipDate = po.ShipDate,
                        Note = po.Note,
                        VendorCode = po.Vendor.VendorCode,
                        VendorCodeName = po.Vendor.VendorCode + "-" + po.Vendor.VendorName,
                        RevisionNumber = po.RevisionNumber,
                        CurrencyCode = po.CurrencyCode,
                        Tolerance = po.Tolerance,
                        MaterialClasstifiedName = po.MaterialClassified.MaterialClassifiedName,
                        MaterialClasstifiedId = po.MaterialClassifiedId,
                    };
                    entity.PaymentMethodName = vfi.Methods.Where(t => t.MethodId == po.PaymentId).Select(t => t.MethodName_EN).FirstOrDefault();
                    entity.DeliveryMethodName = vfi.Methods.Where(t => t.MethodId == po.ConditionDeliveryId).Select(t => t.MethodName_EN).FirstOrDefault();
                    entity.ShipMethodName = vfi.Methods.Where(t => t.MethodId == po.DeliveryById).Select(t => t.MethodName_EN).FirstOrDefault();

                    entity.AddressName = vfi.DeliveryAddresses.Where(t => t.AddressId == po.AddressId).Select(t => t.AddressShortName).FirstOrDefault();
                    entity.StatusName = MyUtilities.Sales.GetText(po.Status);
                    model.Add(entity);
                }
                //model.AddRange(purchaseOrders.Select(po => new PurchaseOrderModel
                //    {
                //        PurchaseOrderId = po.PurchaseOrderId,
                //        ModifiedUser = po.ModifiedUser,
                //        ModifiedDate = po.ModifiedDate,
                //        EmployeeName = po.EmployeeName,
                //        VendorName = po.Vendor.VendorName,
                //        MethodName = po.Method.MethodName,
                //        Method1Name = po.Method1.MethodName,
                //        Method2Name = po.Method2.MethodName,
                //        Method3Name = po.Method3.MethodName,
                //        TotalQuality = po.PurchaseOrderDetails.Sum(pod => pod.OrderQty),
                //        ShipDate = po.ShipDate,
                //        Note = po.Note,
                //        StatusName = CastMyUtilities.Sales.StatusDomain.GetText(Convert.ToInt32(po.Status)),
                //        VendorCode = po.Vendor.VendorCode,
                //        VendorCodeName = po.Vendor.VendorCode + po.Vendor.VendorName,
                //        RevisionNumber = po.RevisionNumber,
                //        CurrencyCode = po.CurrencyCode,
                //        Tolerance = po.Tolerance ?? 0,
                //    }));
            }
            return model;
        }

        [HttpPost]
        public ActionResult PurchaseOrderSent(long purchaseOrderId) {
            using (var vfi = new tammaContext()) {
                var po = vfi.PurchaseOrders.FirstOrDefault(o => o.PurchaseOrderId == purchaseOrderId);
                if (po != null &&
                    po.Status == (byte)MyUtilities.Sales.Status.Waiting &&
                    po.Status == (byte)MyUtilities.Sales.Status.InProcess) {
                    po.Status = (byte)MyUtilities.Transaction.Status.Approved;
                    vfi.SaveChanges();
                }
            }
            return Json("");
        }

        [HttpPost]
        public ActionResult PurchaseOrderCancel(long purchaseOrderId) {
            using (var vfi = new tammaContext()) {
                var po = vfi.PurchaseOrders.FirstOrDefault(o => o.PurchaseOrderId == purchaseOrderId);
                if (po != null &&
                    po.Status == (byte)MyUtilities.Sales.Status.Waiting &&
                    po.Status == (byte)MyUtilities.Sales.Status.InProcess) {
                    po.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    vfi.SaveChanges();
                }
            }
            return Json("");
        }

        [GridAction]
        public ActionResult UpdatePoImportDetail(TransactionFptDetailModel update, int transactionId, long purchaseOrderId) {
            try {
                var managerlv2 = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.InvManagementLv2);
                if (!managerlv2)
                    throw new AggregateException("Lỗi! Không đủ quyền để làm điều này.");
                using (var vfi = new tammaContext()) {

                    var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == purchaseOrderId);
                    if (purchaseOrder == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu mua hàng");

                    var status = purchaseOrder.Status;

                    switch (purchaseOrder.MaterialClassifiedId) {
                        case 1: // material
                            {
                                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                                if (transaction == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy phiếu giao dịch");
                                if (transaction.Status != (byte)MyUtilities.Transaction.Status.Approved)
                                    break;

                                var importPoDetail = vfi.ImportPurchaseOrderDetails
                                    .FirstOrDefault(i => i.ImportDetailId == update.DetailId);
                                if (importPoDetail == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy chi tiết nhập");
                                // Note
                                if (importPoDetail.Note != update.Note){
                                    importPoDetail.Note = update.Note;
                                    if (update.Quantity == importPoDetail.QuantityKg)
                                        break;
                                }

                                var transactionDetail = transaction.TransactionDetails.FirstOrDefault(td => td.ReferenceId == importPoDetail.MaterialId);
                                if (transactionDetail == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy giao dịch chi tiết");
                                var poDetail = vfi.PurchaseOrderDetails.FirstOrDefault(pod => pod.PurchaseOrderId == purchaseOrderId &&
                                    pod.ReferenceId == importPoDetail.MaterialId);
                                if (poDetail == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy chi tiết mua hàng");

                                var inv =
                                    vfi.MaterialInventories.FirstOrDefault(
                                        mi =>
                                        mi.LotNumber.Equals(importPoDetail.LotNumber) &&
                                        mi.MaterialId == importPoDetail.MaterialId &&
                                        mi.VendorId == importPoDetail.VendorId &&
                                        mi.Length == importPoDetail.Length);
                                if (inv == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy tồn kho.");

                                var periods = vfi.MaterialInventoryPeriods.Where(mip => mip.MaterialInventoryId == inv.MaterialInventoryId);
                                if (!periods.Any())
                                    throw new AggregateException("Lỗi! MaterialInventoryPeriods không tìm thấy");
                                else if (periods.Count() > 1)
                                    throw new AggregateException("Lỗi! Chi tiết nhập kho không còn nguyên vẹn! Không thể sửa.");
                                var period = periods.FirstOrDefault();
                                var quantity = Math.Round(update.Quantity / inv.UnitWeight, 0);

                                poDetail.ReceivedQty = Math.Round(poDetail.ReceivedQty - importPoDetail.QuantityKg + update.Quantity, 2);
                                if (poDetail.ReceivedQty < 0) { poDetail.ReceivedQty = 0; }
                                else if (poDetail.ReceivedQty == 0) { poDetail.IsComplete = true; }
                                else { poDetail.IsComplete = false; }

                                if (update.Quantity < inv.TotalQty) { status = (byte)MyUtilities.Sales.Status.Waiting; }
                                
                                if (update.Quantity > 0) {
                                    inv.ImportQuantity = quantity;
                                    inv.ImportQuantityKg = update.Quantity;
                                    inv.TotalQty = quantity;
                                    inv.TotalQtyKg = update.Quantity;
                                    period.Quantity = quantity;
                                    period.QuantityKg = update.Quantity;
                                    period.LastPeriodQuantity = quantity;
                                    period.LastPeriodQuantityKg = update.Quantity;
                                    importPoDetail.Quantity = quantity;
                                    importPoDetail.QuantityKg = update.Quantity;
                                }
                                else {
                                    vfi.MaterialInventoryPeriods.Remove(period);
                                    vfi.MaterialInventories.Remove(inv);
                                    if (transaction.TransactionDetails.Count == 1)
                                        transaction.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                                    else {
                                        vfi.ImportPurchaseOrderDetails.Remove(importPoDetail);
                                        vfi.TransactionDetails.Remove(transactionDetail);
                                    }
                                }
                            }
                            break;
                        case 2: //fuel
                            {
                                var transaction = vfi.TransactionFpts.FirstOrDefault(t => t.TransactionId == transactionId);
                                if (transaction == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy phiếu giao dịch");
                                if (transaction.Status != (byte)MyUtilities.Transaction.Status.Approved)
                                    break;

                                var importPoDetail = transaction.TransactionFptDetails.FirstOrDefault(i => i.DetailId == update.DetailId);
                                if (importPoDetail == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy chi tiết nhập");
                                if (importPoDetail.Note != update.Note) {
                                    importPoDetail.Note =  update.Note;
                                    if (update.Quantity == importPoDetail.Quantity)
                                        break;
                                }

                                var poDetail = vfi.PurchaseOrderDetails.FirstOrDefault(pod => pod.PurchaseOrderId == purchaseOrderId &&
                                    pod.ReferenceId == importPoDetail.FptId);
                                if (poDetail == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy chi tiết mua hàng");

                                var inv =
                                    vfi.FuelInventories.FirstOrDefault(
                                        mi =>
                                        mi.FuelId == importPoDetail.FptId &&
                                        mi.LotNumber.Equals(importPoDetail.LotNumber) &&
                                        mi.VendorId == importPoDetail.VendorId);
                                if (inv == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy tồn kho.");

                                var periods = vfi.FuelInventoryPeriods.Where(mip => mip.FuelInvId == inv.FuelInvId);
                                if (!periods.Any())
                                    throw new AggregateException("Lỗi! FuelInventoryPeriods không tìm thấy");
                                else if (periods.Count() > 1)
                                    throw new AggregateException("Lỗi! Chi tiết nhập kho không còn nguyên vẹn! Không thể sửa.");
                                var period = periods.FirstOrDefault();

                                poDetail.ReceivedQty = Math.Round(poDetail.ReceivedQty - importPoDetail.Quantity + update.Quantity, 2);
                                if (poDetail.ReceivedQty < 0) { poDetail.ReceivedQty = 0; }
                                else if (poDetail.ReceivedQty == 0) { poDetail.IsComplete = true; }
                                else { poDetail.IsComplete = false; }

                                if (update.Quantity < inv.TotalQuantity) { status = (byte)MyUtilities.Sales.Status.Waiting; }

                                if (update.Quantity > 0) {
                                    inv.TotalQuantity = update.Quantity;
                                    period.Quantity = update.Quantity;
                                    period.LastQuantity = update.Quantity;
                                    importPoDetail.Quantity = update.Quantity;
                                }
                                else {
                                    vfi.FuelInventoryPeriods.Remove(period);
                                    vfi.FuelInventories.Remove(inv);
                                    if (transaction.TransactionFptDetails.Count == 1) {
                                        transaction.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                                    }
                                    else {
                                        vfi.TransactionFptDetails.Remove(importPoDetail);
                                    }
                                }
                            }
                            break;

                        case 3: {
                                var transaction = vfi.TransactionFpts.FirstOrDefault(t => t.TransactionId == transactionId);
                                if (transaction == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy phiếu giao dịch");
                                if (transaction.Status != (byte)MyUtilities.Transaction.Status.Approved)
                                    break;

                                var importPoDetail = transaction.TransactionFptDetails.FirstOrDefault(i => i.DetailId == update.DetailId);
                                if (importPoDetail == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy chi tiết nhập");
                                if (importPoDetail.Note != update.Note) {
                                    importPoDetail.Note = update.Note;
                                    if (update.Quantity == importPoDetail.Quantity)
                                        break;
                                }
                                var poDetail = vfi.PurchaseOrderDetails.FirstOrDefault(pod => pod.PurchaseOrderId == purchaseOrderId &&
                                    pod.ReferenceId == importPoDetail.FptId);
                                if (poDetail == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy chi tiết mua hàng");

                                var inv =
                                    vfi.ToolInventories.FirstOrDefault(
                                        mi =>
                                        mi.ToolId == importPoDetail.FptId &&
                                        mi.LotNumber.Equals(importPoDetail.LotNumber) &&
                                        mi.VendorId == importPoDetail.VendorId);
                                if (inv == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy tồn kho.");

                                var periods = vfi.ToolInventoryPeriods.Where(mip => mip.ToolInvId == inv.ToolInvId);
                                if (!periods.Any())
                                    throw new AggregateException("Lỗi! ToolInventoryPeriods không tìm thấy");
                                else if (periods.Count() > 1)
                                    throw new AggregateException("Lỗi! Chi tiết nhập kho không còn nguyên vẹn! Không thể sửa.");
                                var period = periods.FirstOrDefault();

                                poDetail.ReceivedQty = Math.Round(poDetail.ReceivedQty - importPoDetail.Quantity + update.Quantity, 2);
                                if (poDetail.ReceivedQty < 0) { poDetail.ReceivedQty = 0; }
                                else if (poDetail.ReceivedQty == 0) { poDetail.IsComplete = true; }
                                else { poDetail.IsComplete = false; }

                                if (update.Quantity < inv.TotalQuantity) { status = (byte)MyUtilities.Sales.Status.Waiting; }

                                if (update.Quantity > 0) {
                                    inv.TotalQuantity = update.Quantity;
                                    period.Quantity = update.Quantity;
                                    period.LastQuantity = update.Quantity;
                                    importPoDetail.Quantity = update.Quantity;
                                }
                                else {
                                    vfi.ToolInventoryPeriods.Remove(period);
                                    vfi.ToolInventories.Remove(inv);
                                    if (transaction.TransactionFptDetails.Count == 1) {
                                        transaction.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                                    }
                                    else {
                                        vfi.TransactionFptDetails.Remove(importPoDetail);
                                    }
                                }
                            }
                            break;
                            // product

                        case 6: {
                                var transaction = vfi.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
                                if (transaction == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy phiếu giao dịch");
                                if (transaction.Status != (byte)MyUtilities.Transaction.Status.Approved)
                                    break;

                                var importPoDetail = transaction.TransactionDetails.FirstOrDefault(i => i.TransactionDetailId == update.DetailId);
                                if (importPoDetail == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy chi tiết nhập");
                                var poDetail = vfi.PurchaseOrderDetails.FirstOrDefault(pod => pod.PurchaseOrderId == purchaseOrderId &&
                                    pod.ReferenceId == importPoDetail.ReferenceId);
                                if (poDetail == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy chi tiết mua hàng");

                                var inv =
                                    vfi.ProductInventories.FirstOrDefault(
                                        mi =>
                                        mi.ProductId == importPoDetail.ReferenceId &&
                                        mi.LotNumber.Equals(importPoDetail.LotNumber) &&
                                        mi.VendorId == importPoDetail.VendorId);
                                if (inv == null)
                                    throw new AggregateException("Lỗi! Không tìm thấy tồn kho.");

                                var periods = vfi.ProductInventoryPeriods.Where(mip => mip.ProductInvId == inv.ProductInventoryId);
                                if (!periods.Any())
                                    throw new AggregateException("Lỗi! ProductInventoryPeriods không tìm thấy");
                                else if (periods.Count() > 1)
                                    throw new AggregateException("Lỗi! Chi tiết nhập kho không còn nguyên vẹn! Không thể sửa.");
                                var period = periods.FirstOrDefault();

                                poDetail.ReceivedQty = Math.Round(poDetail.ReceivedQty - importPoDetail.Quantity + update.Quantity, 2);
                                if (poDetail.ReceivedQty < 0) { poDetail.ReceivedQty = 0; }
                                else if (poDetail.ReceivedQty == 0) { poDetail.IsComplete = true; }
                                else { poDetail.IsComplete = false; }

                                if (update.Quantity < inv.TotalQty) { status = (byte)MyUtilities.Sales.Status.Waiting; }

                                if (update.Quantity > 0) {
                                    inv.TotalQty = update.Quantity;
                                    period.Quantity = update.Quantity;
                                    period.LastPeriodQuantity = update.Quantity;
                                    importPoDetail.Quantity = update.Quantity;
                                }
                                else {
                                    vfi.ProductInventoryPeriods.Remove(period);
                                    vfi.ProductInventories.Remove(inv);
                                    if (transaction.TransactionDetails.Count == 1) {
                                        transaction.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                                    }
                                    else {
                                        vfi.TransactionDetails.Remove(importPoDetail);
                                    }
                                }
                            }
                            break;

                        default:
                            throw new AggregateException("Lỗi! Chưa hỗ trợ chức năng này!");
                            break;
                    }
                    purchaseOrder.Status = status;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdatePoImportDetail", ex.Message);
            }
            return View(new GridModel(GetPoImportDetail(transactionId, purchaseOrderId)));
        }

        #endregion

        #region Material


        [GridAction]
        public ActionResult CancelQuantityPurchaseOrder(
            [Bind(Prefix = "inserted")] IEnumerable<PurchaseOrderDetailModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<PurchaseOrderDetailModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<PurchaseOrderDetailModel> deleteds
            , long purchaseOrderId
            ) {
            try {
                using (var vfi = new tammaContext()) {
                    var po = vfi.PurchaseOrders.FirstOrDefault(o => o.PurchaseOrderId == purchaseOrderId);
                    if (po == null)
                        throw new AggregateException("Không tìm thấy phiếu mua hàng !!");
                    foreach (var updated in updateds) {
                        var poDetail =
                            po.PurchaseOrderDetails.FirstOrDefault(
                                pod => pod.PurchaseOrderDetailId == updated.PurchaseOrderDetailId);
                        if (poDetail == null)
                            throw new AggregateException("Không tìm thấy chi tiết phiếu mua hàng !! " +
                                                         po.RevisionNumber);
                        if (poDetail.OrderQty - poDetail.ReceivedQty - poDetail.RejectedQty - updated.Quantity < 0)
                            throw new AggregateException("Không thể giảm quá số lượng còn lại " + updated.Code);
                        poDetail.RejectedQty += updated.Quantity;
                        if (poDetail.OrderQty - poDetail.ReceivedQty - poDetail.RejectedQty == 0)
                            poDetail.IsComplete = true;
                    }
                    if (!po.PurchaseOrderDetails.Any(od => od.IsComplete == false)) {
                        po.Status = (byte)MyUtilities.Sales.Status.Completed;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CancelQuantityPurchaseOrder", ex.Message);
            }
            return View(new GridModel(GetPurchaseOrderDetailModel(purchaseOrderId)));
        }


        [GridAction]
        public ActionResult UpdateMaterialPurchaseOrderDetail(
            [Bind(Prefix = "inserted")] IEnumerable<PurchaseOrderDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<PurchaseOrderDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<PurchaseOrderDetailModel> deletedDetails,
            //int materialClassifiedId, int materialTypeId, 
            string shipDate,
            int materialClassifiedId, int vendorId, string contractNumber, string orderDate,
            //int shipMethodId, int deliveryMethodId,int packagedMethodId, int paymentMethodId, 
            string employeeName, int tolerance, string currencyCode, string note, string standard, int deliveryAddress ,int billToAddress
            ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateMaterialPurchaseOrderDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<PurchaseOrderDetailModel>()));
            }
            try {
                var ci = new CultureInfo("vi-VN");
                var oDate = string.IsNullOrWhiteSpace(orderDate)
                                ? DateTime.Today
                                : Convert.ToDateTime(orderDate, ci);
                if (insertedDetails != null) {
                    using (var vfi = new tammaContext()) {
                        var purchaseOrder = new PurchaseOrder {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            EmployeeName = employeeName,
                            VendorId = vendorId,
                            ShipDate = null,
                            OrderDate = oDate,
                            RevisionNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.PurchaseOrder, 1),
                            Active = false,
                            Status = (byte)MyUtilities.Sales.Status.Waiting,
                            Tolerance = tolerance,
                            Note = note,
                            CurrencyCode = currencyCode,
                            MaterialClassifiedId = materialClassifiedId,
                            ContractNumber = contractNumber + "",
                            AddressId = deliveryAddress,
                            BillToId = billToAddress,
                            
                            
                        };
                        foreach (var detail in insertedDetails) {
                            if (detail.MaterialId == 0)
                                throw new AggregateException("Vui lòng chọn lại mã " + detail.MaterialCodeName);
                            if (detail.OrderQty > 0) {
                                if (purchaseOrder.MaterialClassifiedId == 1) {
                                    var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == detail.MaterialId);
                                    if (material == null)
                                        throw new AggregateException("Nguyên liệu lỗi " + detail.MaterialCodeName);
                                }
                                else if (purchaseOrder.MaterialClassifiedId == 2) {
                                    var fuel = vfi.Fuels.FirstOrDefault(f => f.FuelId == detail.MaterialId);
                                    if (fuel == null)
                                        throw new AggregateException("Nhiên liệu lỗi " + detail.MaterialCodeName);
                                }
                                else if (purchaseOrder.MaterialClassifiedId == 6) {
                                    var product = vfi.Products.FirstOrDefault(f => f.ProductId == detail.MaterialId);
                                    if (product == null)
                                        throw new AggregateException("Sản phẩm lỗi " + detail.MaterialCodeName);
                                }
                                if (string.IsNullOrWhiteSpace(detail.Unit))
                                    throw new AggregateException("Thiếu đơn vị tính " + detail.MaterialCodeName);
                                if (detail.Standard == null)
                                    throw new AggregateException("Thiếu tiêu chuẩn rồi kìa ");
                                var poDetail = new PurchaseOrderDetail {
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    PurchaseOrder = purchaseOrder,
                                    PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                                    ReferenceId = detail.MaterialId,
                                    OrderQty = detail.OrderQty,
                                    UnitPrice = detail.UnitPrice,
                                    DueDate = detail.DueDate ?? DateTime.Now,
                                    ReceivedQty = 0,
                                    RejectedQty = 0,
                                    Active = true,
                                    Unit = detail.Unit,
                                    IsComplete = false,
                                    MaterialClassifiedId = materialClassifiedId,
                                    Standard = detail.Standard,
                                    
                                };
                                purchaseOrder.PurchaseOrderDetails.Add(poDetail);
                            }
                        }
                        if (purchaseOrder.PurchaseOrderDetails.Any()) {
                            vfi.PurchaseOrders.Add(purchaseOrder);
                            vfi.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMaterialPO",
                                         ex.Message);
            }
            return View(new GridModel(new List<PurchaseOrderDetailModel>()));
        }

        #endregion

        #region inquiry

        // 13/04/2026
        //[HttpPost]
        //public ActionResult CheckPurchaseProgress(int materialClassify, string vedomCodeName, int status, string inquiryNumber, string fromdate, string todate) {
        //    var model = GetPurchaseProgress(materialClassify, vedomCodeName, status, inquiryNumber, fromdate, todate);
        //    return View(new GridModel(model));
        //}


        //// 13/04/2026
        //[GridAction]
        //public List<PurchaseProgressModel> GetPurchaseProgress(int materialClassify, string vedomCodeName, int status, string inquiryNumber, string fromdate, string todate) {
        //    var model = new List<PurchaseProgressModel>();
        //    try {
        //        using (var vfi = new tammaContext()) {
        //            var fdate = MyUtilities.Function.ParseDate(fromdate);
        //            var tdate = MyUtilities.Function.ParseDate(todate);
        //            var inquiryList = vfi.
        //        }
        //    }
        //}

        [HttpPost]
        public ActionResult CheckPoReferenceUnitPrice(int classified, int referenceId) {
            var entity = new InquiryPo { };
            try {
                using (var vfi = new tammaContext()) {
                    var lastPurchase = (from x in vfi.PurchaseOrderDetails
                                        where x.PurchaseOrder.Status == (byte)MyUtilities.Transaction.Status.Approved
                                            && x.PurchaseOrder.MaterialClassifiedId == classified
                                            && x.ReferenceId == referenceId
                                        orderby x.PurchaseOrder.OrderDate descending
                                        select x).FirstOrDefault();
                    if (lastPurchase != null) {
                        entity.UnitPrice = lastPurchase.UnitPrice;
                        entity.Currency = (lastPurchase.PurchaseOrder.CurrencyCode + "").Trim();
                        entity.Unit = (lastPurchase.Unit + "").Trim();
                        entity.Note = "giá từ PO cũ";
                    }
                    else {
                        var lastInquiry = (from x in vfi.InquiryPoes
                                           where x.ClasstifiedId == classified
                                               && x.ReferenceId == referenceId
                                               && x.Status != (byte)MyUtilities.Transaction.Status.Cancel
                                           orderby x.DueDate descending
                                           select x).FirstOrDefault();
                        if (lastInquiry != null) {
                            entity.UnitPrice = lastInquiry.UnitPrice;
                            entity.Currency = (lastInquiry.Currency + "").Trim();
                            entity.Unit = (lastInquiry.Unit + "").Trim();
                            entity.Note = "giá từ YC cũ";
                        }
                        else {
                            switch (classified) {
                                case 1:
                                    var materialInv =
                                        vfi.MaterialInventories.Where(
                                            mi =>
                                            mi.MaterialId == referenceId).OrderByDescending(mi => mi.ImportDate).FirstOrDefault();
                                    if (materialInv != null) {
                                        entity.UnitPrice = materialInv.UnitPrice;
                                        entity.Unit = materialInv.UnitMeasure;
                                        entity.Currency = "VND";
                                        entity.Note = "giá từ tồn kho";
                                    }

                                    break;
                                case 2:
                                    var fuelInv =
                                        vfi.FuelInventories.Where(
                                            mi =>
                                            mi.FuelId == referenceId).OrderByDescending(mi => mi.CreateDate).FirstOrDefault();
                                    if (fuelInv != null) {
                                        entity.UnitPrice = fuelInv.UnitPrice;
                                        entity.Unit = fuelInv.UnitMeasure;
                                        entity.Currency = "VND";
                                        entity.Note = "giá từ tồn kho";
                                    }
                                    break;
                                case 3:
                                    var toolInv =
                                        vfi.ToolInventories.Where(
                                            mi =>
                                            mi.ToolId == referenceId).OrderByDescending(mi => mi.CreateDate).FirstOrDefault();
                                    if (toolInv != null) {
                                        entity.UnitPrice = toolInv.UnitPrice;
                                        entity.Unit = toolInv.UnitMeasure;
                                        entity.Currency = "VND";
                                        entity.Note = "giá từ tồn kho";
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { return Json(null); }

            return Json(entity);
        }

        [GridAction]
        public ActionResult SelectPoProgress(int classified, int type, int vendorId, string inquiryNumber, string revisionNumber) {
            var model = new List<PoProgressModel>();
            try {
                model = GetPoProgress(classified, type, vendorId, inquiryNumber, revisionNumber);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMaterialPO", ex.Message);
            }
            return View(new GridModel(model));
        }


        List<PoProgressModel> GetPoProgress(int classified, int type, int vendorId, string inquiryNumber, string revisionNumber) {
            var model = new List<PoProgressModel>();
            using (var vfi = new tammaContext()) {
                var fDate = DateTime.Now.AddDays(-10);
                var fmonth = DateTime.Now.AddMonths(-2);
                var status = 0;
                var statuses1 = new List<int>();
                if (status == 0) {
                    statuses1.Add((int)MyUtilities.PurchaseOrder.InquiryEnum.Pending);
                    statuses1.Add((int)MyUtilities.PurchaseOrder.InquiryEnum.Approved);
                    statuses1.Add((int)MyUtilities.PurchaseOrder.InquiryEnum.MakePo);
                }
                else {
                    statuses1.Add(status);
                }

                var poList = (from po in vfi.PurchaseOrders
                               where (
                                        po.Status != 3
                                     && po.Status != 2  
                                     && (string.IsNullOrEmpty(revisionNumber) || po.RevisionNumber.Contains(revisionNumber))
                                     && (classified == 0 || po.MaterialClassifiedId == classified)
                                     && (vendorId == 0 || po.VendorId == vendorId))
                                     //&& po.PurchaseOrderId == 4655
                               select po.PurchaseOrderId).ToList();

                var poDetailList = vfi.PurchaseOrderDetails.Where(t => poList.Contains(t.PurchaseOrderId)
                                                                  && t.ReceivedQty < t.OrderQty).Select(t => t).ToList();

                var inquiryPos = (from ip in vfi.InquiryPoes
                                  where (classified == 0 || ip.ClasstifiedId == classified)
                                        && (vendorId == 0 || ip.VendorId == vendorId)
                                        && (statuses1.Contains(ip.Status))
                                        && ip.PurchaseOrderDetail.PurchaseOrder.Status != 3
                                        && ip.PurchaseOrderDetail.PurchaseOrder.Status != 2
                                        && (string.IsNullOrEmpty(inquiryNumber) || ip.InquiryNumber.Contains(inquiryNumber))
                                        && (string.IsNullOrEmpty(revisionNumber) || ip.PurchaseOrderDetail.PurchaseOrder.RevisionNumber.Contains(revisionNumber)
                                        )

                                  //&& ip.InquiryNumber == "VF1260131"
                                  orderby ip.DueDate
                                  select ip).ToList();
                
                // lay tu` phieu mua
                var inquiryPoStatus5 = inquiryPos.Where(t => t.Status == 5).Select(t => t.PoDetailId).ToList();
                int Index = 1;
                if (string.IsNullOrEmpty(inquiryNumber)) {
                    foreach (var po in poDetailList) {
                        if (!inquiryPoStatus5.Contains(po.PurchaseOrderDetailId)) {
                            var entity2 = new PoProgressModel {
                                index = Index,
                                Status = po.PurchaseOrder.Status,
                                ClassifiedId = po.MaterialClassifiedId,
                                OrderQty = po.OrderQty,
                                ReceivedQty = po.ReceivedQty,
                                UnitPrice = po.UnitPrice,
                                Unit = po.Unit,
                                PurchaseOrderId = po.PurchaseOrderId,
                                RevisionNumber = po.PurchaseOrder.RevisionNumber,
                                ReferenceId = po.ReferenceId,
                                ModifiedDate = po.ModifiedDate,
                                ModifiedUser = po.ModifiedUser,
                                Active = po.PurchaseOrder.Active,
                                VendorId = po.PurchaseOrder.VendorId,
                                Currency = po.PurchaseOrder.CurrencyCode != null
                                           ? po.PurchaseOrder.CurrencyCode.Trim()
                                           : string.Empty,
                                ClassifiedName = po.MaterialClassified.MaterialClassifiedName,
                                PurchaseDetailId = po.PurchaseOrderDetailId,
                                BillOfLanding = po.PurchaseOrder.BillOfLanding
                            };
                            entity2.VendorCode = vfi.Vendors.Where(t => t.VendorId == entity2.VendorId).Select(t => t.VendorName).FirstOrDefault();
                            
                            // Phan loai
                            switch (entity2.ClassifiedId) {
                                case 1:
                                    var material =
                                        vfi.Materials.FirstOrDefault(m => m.MaterialId == entity2.ReferenceId);
                                    if (material == null) continue;
                                    entity2.TypeId = material.MaterialTypeId;
                                    entity2.TypeName = material.MaterialType.MaterialTypeName;
                                    entity2.ReferenceName = material.MaterialName;
                                    entity2.ReferenceCode = material.MaterialCode;

                                    var materialInvs =
                                        vfi.MaterialInventories.Where(
                                            mi => mi.MaterialId == entity2.ReferenceId && mi.TotalQty > 0).ToList();
                                    entity2.TotalInv = materialInvs.Sum(mi => mi.TotalQty * mi.UnitWeight);

                                    if (entity2.ReceivedQty > 0) {
                                        var importCheck = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == entity2.PurchaseDetailId).Select(t => t.ImportPurchaseOrder.ImportDate).FirstOrDefault();
                                        entity2.ImportDate = importCheck;
                                        entity2.DeliveryDate = po.PurchaseOrder.ShipDate;
                                        //entity2.BillOfLanding = po.PurchaseOrder.BillOfLanding;
                                        if (entity2.ReceivedQty >= (entity2.OrderQty * 0.9)) {
                                            if (importCheck < fDate) {
                                                continue;
                                            }
                                            else {
                                                entity2.ProgressState = 11.5;
                                            }
                                        }
                                        else {
                                            entity2.ProgressState = 9.5;
                                        }
                                    }

                                    else if (entity2.ReceivedQty == 0){
                                        switch (entity2.Active) {
                                            case true:
                                                entity2.DeliveryDate = po.PurchaseOrder.ShipDate;
                                                if (entity2.Status == 1) {
                                                    entity2.ProgressState = 5.5;
                                                }
                                                else if (entity2.Status == 4) {
                                                    entity2.ProgressState = 7.5;
                                                }
                                                break;

                                            case false:
                                                entity2.ProgressState = 3.5;
                                                break;
                                        }
                                    }

                                    break;

                                case 2:
                                    var fuel =
                                        vfi.Fuels.FirstOrDefault(m => m.FuelId == entity2.ReferenceId);
                                    if (fuel == null) continue;
                                    entity2.ReferenceCode = fuel.FuelFullCode;
                                    entity2.ReferenceName = fuel.FuelName;

                                    var fuelInvs =
                                        vfi.FuelInventories.Where(
                                            mi => mi.FuelId == entity2.ReferenceId && mi.TotalQuantity > 0).ToList();
                                    entity2.TotalInv = fuelInvs.Sum(mi => mi.TotalQuantity);

                                    if (entity2.ReceivedQty > 0) {
                                        var importCheck = vfi.TransactionFptDetails.Where(t => t.PoDetailId == entity2.PurchaseDetailId).Select(t => t.TransactionFpt.TransactionDate).FirstOrDefault();
                                        entity2.ImportDate = importCheck;
                                        entity2.DeliveryDate = po.PurchaseOrder.ShipDate;
                                        //entity2.BillOfLanding = po.PurchaseOrder.BillOfLanding;
                                        if (entity2.ReceivedQty >= entity2.OrderQty) {
                                            if (importCheck < fDate) {
                                                continue;
                                            }
                                            else {
                                                entity2.ProgressState = 11.5;
                                            }
                                        }
                                        else {
                                            entity2.ProgressState = 9.5;

                                        }
                                    }

                                    else if (entity2.ReceivedQty == 0) {
                                        switch (entity2.Active) {
                                            case true:
                                                entity2.DeliveryDate = po.PurchaseOrder.ShipDate;
                                                if (entity2.Status == 1) {
                                                    entity2.ProgressState = 5.5;
                                                }
                                                else if (entity2.Status == 4) {
                                                    entity2.ProgressState = 7.5;
                                                    //entity2.BillOfLanding = po.PurchaseOrder.BillOfLanding;
                                                }
                                                break;

                                            case false:
                                                entity2.ProgressState = 3.5;
                                                break;
                                        }
                                    }

                                    break;

                                case 3:
                                    var tool =
                                        vfi.Tools.FirstOrDefault(m => m.ToolId == entity2.ReferenceId);
                                    if (tool == null) continue;
                                    entity2.TypeId = tool.MaterialTypeId;
                                    entity2.TypeName = tool.MaterialType.MaterialTypeName;
                                    entity2.ReferenceCode = tool.ToolFullCode;
                                    entity2.ReferenceName = tool.ToolName;

                                    var toolInvs =
                                        vfi.ToolInventories.Where(
                                            mi => mi.ToolId == entity2.ReferenceId && mi.TotalQuantity > 0).ToList();
                                    entity2.TotalInv = toolInvs.Sum(mi => mi.TotalQuantity);

                                    if (entity2.ReceivedQty > 0) {
                                        var importCheck = vfi.TransactionFptDetails.Where(t => t.PoDetailId == entity2.PurchaseDetailId).Select(t => t.TransactionFpt.TransactionDate).FirstOrDefault();
                                        entity2.ImportDate = importCheck;
                                        entity2.DeliveryDate = po.PurchaseOrder.ShipDate;
                                        //entity2.BillOfLanding = po.PurchaseOrder.BillOfLanding;
                                        if (entity2.ReceivedQty >= entity2.OrderQty) {
                                            if (importCheck < fDate) {
                                                continue;
                                            }
                                            else {
                                                entity2.ProgressState = 11.5;
                                            }
                                        }
                                        else {
                                            entity2.ProgressState = 9.5;
                                        }
                                    }

                                    else if (entity2.ReceivedQty == 0) {
                                        switch (entity2.Active) {
                                            case true:
                                                entity2.DeliveryDate = po.PurchaseOrder.ShipDate;
                                                if (entity2.Status == 1) {
                                                    entity2.ProgressState = 5.5;
                                                }
                                                else if (entity2.Status == 4) {
                                                    entity2.ProgressState = 7.5;
                                                    //entity2.BillOfLanding = po.PurchaseOrder.BillOfLanding;
                                                }
                                                break;

                                            case false:
                                                entity2.ProgressState = 3.5;
                                                break;
                                        }
                                    }
                                    break;

                                case 6:
                                    var product =
                                        vfi.Products.FirstOrDefault(m => m.ProductId == entity2.ReferenceId);
                                    if (product == null) continue;
                                    entity2.TypeId = product.Material.MaterialTypeId;
                                    entity2.TypeName = product.Material.MaterialType.MaterialTypeName;
                                    entity2.ReferenceName = product.ProductName;
                                    entity2.ReferenceCode = product.ProductCode;

                                    var productInvs =
                                        vfi.ProductInventories.Where(
                                            mi => mi.ProductId == entity2.ReferenceId && mi.TotalQty > 0).ToList();
                                    entity2.TotalInv = productInvs.Sum(mi => mi.TotalQty);

                                    if (entity2.ReceivedQty > 0) {
                                        var importCheck = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == entity2.PurchaseDetailId).Select(t => t.ImportPurchaseOrder.ImportDate).FirstOrDefault();
                                        entity2.ImportDate = importCheck;
                                        entity2.DeliveryDate = po.PurchaseOrder.ShipDate;
                                        //entity2.BillOfLanding = po.PurchaseOrder.BillOfLanding;
                                        if (entity2.ReceivedQty >= (entity2.OrderQty * 0.9)) {
                                            if (importCheck < fDate) {
                                                continue;
                                            }
                                            else {
                                                entity2.ProgressState = 11.5;
                                            }
                                        }
                                        else {
                                            entity2.ProgressState = 9.5;
                                        }
                                    }

                                    else if (entity2.ReceivedQty == 0) {
                                        switch (entity2.Active) {
                                            case true:
                                                entity2.DeliveryDate = po.PurchaseOrder.ShipDate;
                                                if (entity2.Status == 1) {
                                                    entity2.ProgressState = 5.5;
                                                }
                                                else if (entity2.Status == 4) {
                                                    entity2.ProgressState = 7.5;
                                                }
                                                break;

                                            case false:
                                                entity2.ProgressState = 3.5;
                                                break;
                                        }
                                    }

                                    break;


                                default:
                                    break;
                            }

                            model.Add(entity2);
                            Index++;
                        }

                    }

                }

                foreach(var ip in inquiryPos){
                    var entity = new PoProgressModel{
                        index = Index,
                        ModifiedDate = ip.ModifiedDate,
                        ModifiedUser = ip.ModifiedUser,
                        InquiryId = ip.InquiryId,
                        Note = ip.Note,
                        ClassifiedId = ip.ClasstifiedId,
                        ClassifiedName = ip.MaterialClassified.MaterialClassifiedName,
                        Currency = ip.Currency.Trim(),
                        OrderQty = Math.Round(ip.OrderQty, 2),
                        //ReceivedQty = ip.PurchaseOrderDetail.ReceivedQty,
                        UnitPrice = ip.UnitPrice,
                        Unit = ip.Unit,
                        Status = ip.Status,
                        DueDate = ip.DueDate,
                        ReferenceId = ip.ReferenceId,
                        InquiryNumber = ip.InquiryNumber,
                        PoDetailId = ip.PoDetailId ?? 0,
                    };
                    if (ip.VendorId != null) {
                        entity.VendorId = ip.VendorId.Value;
                        entity.VendorCode = ip.Vendor.VendorName;
                    }

                    switch (entity.ClassifiedId) {
                        case 1:
                            var material =
                                vfi.Materials.FirstOrDefault(m => m.MaterialId == entity.ReferenceId);
                            if (material == null) continue;
                            entity.TypeId = material.MaterialTypeId;
                            entity.TypeName = material.MaterialType.MaterialTypeName;
                            entity.ReferenceName = material.MaterialName;
                            entity.ReferenceCode = material.MaterialCode;

                            var materialInvs =
                                vfi.MaterialInventories.Where(
                                    mi => mi.MaterialId == entity.ReferenceId && mi.TotalQty > 0).ToList();
                            entity.TotalInv = materialInvs.Sum(mi => mi.TotalQty * mi.UnitWeight);


                            switch (entity.Status) {
                                case 1:
                                    entity.ProgressState = -1;
                                    break;

                                case 3:
                                    entity.ProgressState = 1;
                                    break;
                                case 5:
                                    var poDetail = vfi.PurchaseOrderDetails.FirstOrDefault(t => t.PurchaseOrderDetailId == entity.PoDetailId);
                                    entity.RevisionNumber = poDetail.PurchaseOrder.RevisionNumber;
                                    if (poDetail.PurchaseOrder.Active == false) {
                                        entity.ProgressState = 3;
                                    }
                                    else if (poDetail.PurchaseOrder.Active == true) {
                                        entity.DeliveryDate = poDetail.PurchaseOrder.ShipDate;

                                        if (poDetail.ReceivedQty > 0) {
                                            entity.ImportDate = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId).Select(t => t.ImportPurchaseOrder.ImportDate).FirstOrDefault();
                                            if (poDetail.ReceivedQty >= (entity.OrderQty * 0.9) && entity.ImportDate < fDate) {
                                                continue;
                                            }
                                            else {
                                                entity.ReceivedQty = poDetail.ReceivedQty;
                                                entity.BillOfLanding = poDetail.PurchaseOrder.BillOfLanding;
                                                entity.ProgressState = 9;
                                            }
                                        }

                                        else if (poDetail.ReceivedQty == 0) {
                                            if (poDetail.PurchaseOrder.Status == 4) {
                                                entity.ProgressState = 7;
                                                entity.BillOfLanding = poDetail.PurchaseOrder.BillOfLanding;
                                            }
                                            else if (poDetail.PurchaseOrder.Status == 1) {
                                                entity.ProgressState = 5;
                                            }
                                        }

                                    }
                                    break;
                                default:
                                    break;
                            }


                            break;

                        case 2:
                            var fuel =
                                vfi.Fuels.FirstOrDefault(m => m.FuelId == entity.ReferenceId);
                            if (fuel == null) continue;
                            entity.ReferenceCode = fuel.FuelFullCode;
                            entity.ReferenceName = fuel.FuelName;

                            var fuelInvs =
                                vfi.FuelInventories.Where(
                                    mi => mi.FuelId == entity.ReferenceId && mi.TotalQuantity > 0).ToList();
                            entity.TotalInv = fuelInvs.Sum(mi => mi.TotalQuantity);
                            if (ip.PurchaseOrderDetail != null) {
                                entity.ReceivedQty = ip.PurchaseOrderDetail.ReceivedQty;
                            }

                            switch (entity.Status) {
                                case 1:
                                    entity.ProgressState = -1;
                                    break;

                                case 3:
                                    entity.ProgressState = 1;
                                    break;
                                case 5:
                                    var poDetail = vfi.PurchaseOrderDetails.FirstOrDefault(t => t.PurchaseOrderDetailId == entity.PoDetailId);
                                    entity.RevisionNumber = poDetail.PurchaseOrder.RevisionNumber;
                                    if (poDetail.PurchaseOrder.Active == false) {
                                        entity.ProgressState = 3;
                                    }
                                    else if (poDetail.PurchaseOrder.Active == true) {
                                        entity.DeliveryDate = poDetail.PurchaseOrder.ShipDate;

                                        if (poDetail.ReceivedQty > 0) {
                                            entity.ImportDate = vfi.TransactionFptDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId).Select(t => t.TransactionFpt.TransactionDate).FirstOrDefault();
                                            if (poDetail.ReceivedQty >= (entity.OrderQty * 0.9) && entity.ImportDate < fDate) {
                                                continue;
                                            }
                                            else {
                                                entity.ReceivedQty = poDetail.ReceivedQty;
                                                entity.BillOfLanding = poDetail.PurchaseOrder.BillOfLanding;
                                                entity.ProgressState = 9;
                                            }
                                        }

                                        else if (poDetail.ReceivedQty == 0) {
                                            if (poDetail.PurchaseOrder.Status == 4) {
                                                entity.ProgressState = 7;
                                                entity.BillOfLanding = poDetail.PurchaseOrder.BillOfLanding;
                                            }
                                            else if (poDetail.PurchaseOrder.Status == 1) {
                                                entity.ProgressState = 5;
                                            }
                                        }

                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;

                        case 3:
                            var tool =
                                vfi.Tools.FirstOrDefault(m => m.ToolId == entity.ReferenceId);
                            if (tool == null) continue;
                            entity.TypeId = tool.MaterialTypeId;
                            entity.TypeName = tool.MaterialType.MaterialTypeName;
                            entity.ReferenceCode = tool.ToolFullCode;
                            entity.ReferenceName = tool.ToolName;

                            var toolInvs =
                                vfi.ToolInventories.Where(
                                    mi => mi.ToolId == entity.ReferenceId && mi.TotalQuantity > 0).ToList();
                            entity.TotalInv = toolInvs.Sum(mi => mi.TotalQuantity);


                            switch (entity.Status) {
                                case 1:
                                    entity.ProgressState = -1;
                                    break;

                                case 3:
                                    entity.ProgressState = 1;
                                    break;
                                case 5:
                                    var poDetail = vfi.PurchaseOrderDetails.FirstOrDefault(t => t.PurchaseOrderDetailId == entity.PoDetailId);
                                    entity.RevisionNumber = poDetail.PurchaseOrder.RevisionNumber;
                                    if (poDetail.PurchaseOrder.Active == false) {
                                        entity.ProgressState = 3;
                                    }
                                    else if (poDetail.PurchaseOrder.Active == true) {
                                        entity.DeliveryDate = poDetail.PurchaseOrder.ShipDate;

                                        if (poDetail.ReceivedQty > 0) {
                                            entity.ImportDate = vfi.TransactionFptDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId).Select(t => t.TransactionFpt.TransactionDate).FirstOrDefault();
                                            if (poDetail.ReceivedQty >= (entity.OrderQty * 0.9) && entity.ImportDate < fDate) {
                                                continue;
                                            }
                                            else {
                                                entity.ReceivedQty = poDetail.ReceivedQty;
                                                entity.BillOfLanding = poDetail.PurchaseOrder.BillOfLanding;
                                                entity.ProgressState = 9;
                                            }
                                        }

                                        else if (poDetail.ReceivedQty == 0) {
                                            if (poDetail.PurchaseOrder.Status == 4) {
                                                entity.ProgressState = 7;
                                                entity.BillOfLanding = poDetail.PurchaseOrder.BillOfLanding;
                                            }
                                            else if (poDetail.PurchaseOrder.Status == 1) {
                                                entity.ProgressState = 5;
                                            }
                                        }

                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case 6:
                            var product =
                                vfi.Products.FirstOrDefault(m => m.ProductId == entity.ReferenceId);
                            if (product == null) continue;
                            entity.TypeId = product.Material.MaterialTypeId;
                            entity.TypeName = product.Material.MaterialType.MaterialTypeName;
                            entity.ReferenceName = product.ProductName;
                            entity.ReferenceCode = product.ProductCode;

                            var productInvs =
                                vfi.ProductInventories.Where(
                                    mi => mi.ProductId == entity.ReferenceId && mi.TotalQty > 0).ToList();
                            entity.TotalInv = productInvs.Sum(mi => mi.TotalQty);

                            switch (entity.Status) {
                                case 1:
                                    entity.ProgressState = -1;
                                    break;

                                case 3:
                                    entity.ProgressState = 1;
                                    break;
                                case 5:
                                    var poDetail = vfi.PurchaseOrderDetails.FirstOrDefault(t => t.PurchaseOrderDetailId == entity.PoDetailId);
                                    entity.RevisionNumber = poDetail.PurchaseOrder.RevisionNumber;
                                    if (poDetail.PurchaseOrder.Active == false) {
                                        entity.ProgressState = 3;
                                    }
                                    else if (poDetail.PurchaseOrder.Active == true) {
                                        entity.DeliveryDate = poDetail.PurchaseOrder.ShipDate;

                                        if (poDetail.ReceivedQty > 0) {
                                            if (poDetail.ReceivedQty >= (entity.OrderQty * 0.9) && entity.ImportDate < fDate) {
                                                continue;
                                            }
                                            else {
                                                entity.ImportDate = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == poDetail.PurchaseOrderDetailId).Select(t => t.ImportPurchaseOrder.ImportDate).FirstOrDefault();
                                                entity.ReceivedQty = poDetail.ReceivedQty;
                                                entity.BillOfLanding = poDetail.PurchaseOrder.BillOfLanding;
                                                entity.ProgressState = 9;
                                            }
                                        }

                                        else if (poDetail.ReceivedQty == 0) {
                                            if (poDetail.PurchaseOrder.Status == 4) {
                                                entity.ProgressState = 7;
                                                entity.BillOfLanding = poDetail.PurchaseOrder.BillOfLanding;
                                            }
                                            else if (poDetail.PurchaseOrder.Status == 1) {
                                                entity.ProgressState = 5;
                                            }
                                        }

                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;


                        default:
                            break;
                    }

                    // loc. tu phieu YC
                    //switch (entity.Status) {
                    //    case 1:
                    //        // Chờ xác nhận
                    //        entity.ProgressState = -1;
                    //        break;

                    //    case 3:
                    //        // Đã xác nhận
                    //        entity.ProgressState = 1;
                    //        break;

                    //    case 5:
                    //        var poActive = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrderDetailId == entity.PoDetailId).Select(t => t.PurchaseOrder.Active).FirstOrDefault();
                    //        entity.RevisionNumber = vfi.PurchaseOrderDetails.Where(p => entity.PoDetailId == p.PurchaseOrderDetailId).Select(t => t.PurchaseOrder.RevisionNumber).FirstOrDefault();
                    //        switch (poActive) {
                    //            case false:
                    //                // Đã chuyển thành phiếu mua
                    //                entity.ProgressState = 3;
                    //                break;

                    //            case true:
                    //                var poStatus = vfi.PurchaseOrderDetails.Where(t => t.PurchaseOrderDetailId == entity.PoDetailId).Select(t => t.PurchaseOrder.Status).FirstOrDefault();
                    //                entity.DeliveryDate = vfi.PurchaseOrders.Where(t => t.RevisionNumber == entity.RevisionNumber).Select(p => p.ShipDate).FirstOrDefault();
                    //                if (entity.ReceivedQty >= (entity.OrderQty *0.9)) continue;

                    //                var checkImport = vfi.ImportPurchaseOrderDetails.Where(t => entity.PoDetailId == t.PoDetailId).Select(t => t.ImportPurchaseOrder.ImportDate).ToList();
                    //                var checkImport2 = vfi.TransactionFptDetails.Where(t => t.PoDetailId == entity.PoDetailId).Select(t => t.TransactionFpt.TransactionDate).ToList();
                    //            switch (poStatus) {
                    //                    case 1:
                    //                        // Đã duyệt phiếu mua
                    //                        if (checkImport.Count == 0 && checkImport2.Count == 0) {
                    //                            entity.ProgressState = 5;
                    //                        }
                    //                        if ((checkImport.Count != 0 && checkImport2.Count == 0 ) || (checkImport.Count == 0 && checkImport2.Count != 0)) {
                    //                            entity.ProgressState = 7;
                    //                            if (checkImport.Count != 0) {
                    //                                entity.ImportDate = checkImport.FirstOrDefault();
                    //                            }
                    //                            if (checkImport2.Count != 0) {
                    //                                entity.ImportDate = checkImport2.FirstOrDefault();
                    //                            }
                    //                        }
                    //                        break;
                    //                    case 4:
                    //                        // Đang giao                                            
                    //                        if (checkImport.Count == 0 && checkImport2.Count == 0) {
                    //                            entity.ProgressState = 7;
                    //                        }
                    //                        if ((checkImport.Count != 0 && checkImport2.Count == 0) || (checkImport.Count == 0 && checkImport2.Count != 0)) {
                    //                            entity.ProgressState = 9;
                    //                            if (checkImport.Count != 0) {
                    //                                entity.ImportDate = checkImport.FirstOrDefault();
                    //                            }
                    //                            if (checkImport2.Count != 0) {
                    //                                entity.ImportDate = checkImport2.FirstOrDefault();
                    //                            }
                    //                        }
                    //                        break;

                    //                    //case 2:
                    //                    //    // Đã giao
                    //                    //    entity.ProgressState = 11;
                    //                    //    break;

                    //                    //case 3:
                    //                    //    // Hủy
                    //                    //    break;
                    //                }
                    //                break;
                    //        }
                    //        break;

                    //    case 9:
                    //        // Hủy
                    //        entity.ProgressState = 9;
                    //        break;
                    //}


                    model.Add(entity);
                    Index++;

                }


                // lay nhung phieu  Đã giao trong pham vi 7 ngay
                var PurchasingList = vfi.PurchaseOrders
                    .Where(t => t.Status == 2
                                && t.PurchaseOrderId != null
                                && (string.IsNullOrEmpty(revisionNumber) || t.RevisionNumber.Contains(revisionNumber))
                                && (classified == 0 || t.MaterialClassifiedId == classified)
                                && (vendorId == 0 || t.VendorId == vendorId)
                                && t.ShipDate >= fmonth)
                    .Select(t => t.PurchaseOrderId)
                    .ToList();


                var importDetailList = vfi.PurchaseOrderDetails.Where(t => PurchasingList.Contains(t.PurchaseOrderId)
                                                                      && t.DueDate >= fDate
                                                                      ).Select(t => t).ToList();
                foreach (var po in importDetailList) {
                    var entity3 = new PoProgressModel {
                        index = Index,
                        Status = po.PurchaseOrder.Status,
                        ClassifiedId = po.MaterialClassifiedId,
                        OrderQty = po.OrderQty,
                        UnitPrice = po.UnitPrice,
                        ReceivedQty = po.ReceivedQty,
                        Unit = po.Unit,
                        PurchaseOrderId = po.PurchaseOrderId,
                        RevisionNumber = po.PurchaseOrder.RevisionNumber,
                        ReferenceId = po.ReferenceId,
                        ModifiedDate = po.ModifiedDate,
                        ModifiedUser = po.ModifiedUser,
                        Active = po.PurchaseOrder.Active,
                        VendorId = po.PurchaseOrder.VendorId,
                        Currency = po.PurchaseOrder.CurrencyCode != null
                                   ? po.PurchaseOrder.CurrencyCode.Trim()
                                   : string.Empty,
                        ClassifiedName = po.MaterialClassified.MaterialClassifiedName,
                        PoDetailId = po.PurchaseOrderDetailId,
                        DeliveryDate = po.PurchaseOrder.ShipDate,
                        //BillOfLanding = po.PurchaseOrder.BillOfLanding,
                        
                    };
                    entity3.VendorCode = vfi.Vendors.Where(t => t.VendorId == entity3.VendorId).Select(t => t.VendorName).FirstOrDefault();
                    var checkInquiryPo = vfi.InquiryPoes.Where(t => t.PoDetailId == entity3.PoDetailId).Select(t => t).ToList();
                    switch (entity3.ClassifiedId) {
                        case 1:
                            var material =
                                vfi.Materials.FirstOrDefault(m => m.MaterialId == entity3.ReferenceId);
                            
                            if (material == null) continue;
                            var importDate = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == entity3.PoDetailId).Select(t => t.ImportPurchaseOrder.ImportDate).FirstOrDefault();
                            if (importDate <= fDate) continue;
                            entity3.TypeId = material.MaterialTypeId;
                            entity3.TypeName = material.MaterialType.MaterialTypeName;
                            entity3.ReferenceName = material.MaterialName;
                            entity3.ReferenceCode = material.MaterialCode;

                            var materialInvs =
                                vfi.MaterialInventories.Where(
                                    mi => mi.MaterialId == entity3.ReferenceId && mi.TotalQty > 0).ToList();
                            entity3.TotalInv = materialInvs.Sum(mi => mi.TotalQty * mi.UnitWeight);

                            entity3.ImportDate = importDate;
                            entity3.ReceivedQty = 0.00;

                            if (checkInquiryPo.Count != 0) {
                                entity3.ProgressState = 11;
                                entity3.DueDate = checkInquiryPo.Select(t => t.DueDate).FirstOrDefault();
                                entity3.InquiryNumber = checkInquiryPo.Select(t => t.InquiryNumber).FirstOrDefault();
                            }
                            else if (checkInquiryPo.Count == 0) {
                                entity3.ProgressState = 11.5;
                            }
                            break;

                        case 2:
                            var fuel =
                                vfi.Fuels.FirstOrDefault(m => m.FuelId == entity3.ReferenceId);
                            if (fuel == null) continue;

                            var transactionDate = vfi.TransactionFptDetails.Where(t => t.PoDetailId == entity3.PoDetailId).Select(t => t.TransactionFpt.TransactionDate).FirstOrDefault();
                            if (transactionDate <= fDate) continue;
                            entity3.ReferenceCode = fuel.FuelFullCode;
                            entity3.ReferenceName = fuel.FuelName;

                            var fuelInvs =
                                vfi.FuelInventories.Where(
                                    mi => mi.FuelId == entity3.ReferenceId && mi.TotalQuantity > 0).ToList();
                            entity3.TotalInv = fuelInvs.Sum(mi => mi.TotalQuantity);

                            entity3.ReceivedQty = 0.00;
                            entity3.ImportDate = vfi.TransactionFptDetails.Where(t => t.PoDetailId == po.PurchaseOrderDetailId).Select(t => t.TransactionFpt.TransactionDate).FirstOrDefault();

                            if (checkInquiryPo.Count != 0) {
                                entity3.ProgressState = 11;
                                entity3.DueDate = checkInquiryPo.Select(t => t.DueDate).FirstOrDefault();
                                entity3.InquiryNumber = checkInquiryPo.Select(t => t.InquiryNumber).FirstOrDefault();
                            }
                            else if (checkInquiryPo.Count == 0) {
                                entity3.ProgressState = 11.5;
                            }
                            break;

                        case 3:
                            var tool =
                                vfi.Tools.FirstOrDefault(m => m.ToolId == entity3.ReferenceId);
                            if (tool == null) continue;
                            
                            var transactionDate2 = vfi.TransactionFptDetails.Where(t => t.PoDetailId == entity3.PoDetailId).Select(t => t.TransactionFpt.TransactionDate).FirstOrDefault();
                            if (transactionDate2 <= fDate) continue;

                            entity3.TypeId = tool.MaterialTypeId;
                            entity3.TypeName = tool.MaterialType.MaterialTypeName;
                            entity3.ReferenceCode = tool.ToolFullCode;
                            entity3.ReferenceName = tool.ToolName;

                            var toolInvs =
                                vfi.ToolInventories.Where(
                                    mi => mi.ToolId == entity3.ReferenceId && mi.TotalQuantity > 0).ToList();
                            entity3.TotalInv = toolInvs.Sum(mi => mi.TotalQuantity);

                            entity3.ImportDate = vfi.TransactionFptDetails.Where(t => t.PoDetailId == po.PurchaseOrderDetailId).Select(t => t.TransactionFpt.TransactionDate).FirstOrDefault();
                            entity3.ReceivedQty = 0.00;
                            if (checkInquiryPo.Count != 0) {
                                entity3.ProgressState = 11;
                                entity3.DueDate = checkInquiryPo.Select(t => t.DueDate).FirstOrDefault();
                                entity3.InquiryNumber = checkInquiryPo.Select(t => t.InquiryNumber).FirstOrDefault();


                            }
                            else if (checkInquiryPo.Count == 0) {
                                entity3.ProgressState = 11.5;

                            }
                            break;


                        case 6:
                            var product =
                                vfi.Products.FirstOrDefault(m => m.ProductId == entity3.ReferenceId);

                            if (product == null) continue;
                            var importDate1 = vfi.ImportPurchaseOrderDetails.Where(t => t.PoDetailId == entity3.PoDetailId).Select(t => t.ImportPurchaseOrder.ImportDate).FirstOrDefault();
                            if (importDate1 <= fDate) continue;
                            entity3.TypeId = product.Material.MaterialTypeId;
                            entity3.TypeName = product.Material.MaterialType.MaterialTypeName;
                            entity3.ReferenceName = product.ProductName;
                            entity3.ReferenceCode = product.ProductCode;

                            var productInvs =
                                vfi.ProductInventories.Where(
                                    mi => mi.ProductId == entity3.ReferenceId && mi.TotalQty > 0).ToList();
                            entity3.TotalInv = productInvs.Sum(mi => mi.TotalQty);

                            entity3.ImportDate = importDate1;
                            entity3.ReceivedQty = 0.00;

                            if (checkInquiryPo.Count != 0) {
                                entity3.ProgressState = 11;
                                entity3.DueDate = checkInquiryPo.Select(t => t.DueDate).FirstOrDefault();
                                entity3.InquiryNumber = checkInquiryPo.Select(t => t.InquiryNumber).FirstOrDefault();
                            }
                            else if (checkInquiryPo.Count == 0) {
                                entity3.ProgressState = 11.5;
                            }
                            break;


                        default:
                            break;
                    };
                    model.Add(entity3);
                    Index++;
                }

                }
               return model;
            }


        [GridAction]
        public ActionResult SelectInquiryPo(int classified, int type, int vendorId, int status, string fromDate, string toDate) {
            var model = new List<InquiryPoModel>();
            try {
                model = GetInquiryPo(classified, type, vendorId, status, fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMaterialPO", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<InquiryPoModel> GetInquiryPo(int classified, int type, int vendorId, int status, string fromDate, string toDate) {
            var model = new List<InquiryPoModel>();
            var fDate = MyUtilities.Function.ParseDate(fromDate);
            var tDate = MyUtilities.Function.ParseDate(toDate);

            var ManagementApproved = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.ManagementApprovedPurchase);
            using (var vfi = new tammaContext()) {
                var todayMonth = DateTime.Now.Month;
                var threeMonthBefor = DateTime.Now.AddMonths(-3).Month;
                var todayYear = DateTime.Now.Year;

                var statuses = new List<int>();
                if (status == 0) {
                    statuses.Add((int)MyUtilities.PurchaseOrder.InquiryEnum.Pending);
                    statuses.Add((int)MyUtilities.PurchaseOrder.InquiryEnum.Approved);
                } if (status == -1) {
                    statuses.Add((int)MyUtilities.PurchaseOrder.InquiryEnum.Pending);
                    statuses.Add((int)MyUtilities.PurchaseOrder.InquiryEnum.Approved);
                    statuses.Add((int)MyUtilities.PurchaseOrder.InquiryEnum.MakePo);
                }
                else {
                    statuses.Add(status);
                }
                var inquiryPos = (from ip in vfi.InquiryPoes
                                  where (classified == 0 || ip.ClasstifiedId == classified) &&
                                      //(type == 0 || ip.MaterialClassified.MaterialTypes.Any(mt=> mt.MaterialTypeId == type)) &&
                                  (vendorId == 0 || ip.VendorId == vendorId) &&
                                      //(ip.ReferenceId == 6562) &&
                                  (statuses.Contains(ip.Status))
                                  orderby ip.DueDate
                                  select ip).ToList();
                if (!string.IsNullOrWhiteSpace(toDate)) {
                    inquiryPos = inquiryPos.Where(ip => ip.DueDate >= fDate && ip.DueDate <= tDate).ToList();
                }
                foreach (var ip in inquiryPos) {
                    var entity = new InquiryPoModel {
                        ModifiedDate = ip.ModifiedDate,
                        ModifiedUser = ip.ModifiedUser,
                        InquiryId = ip.InquiryId,
                        Note = ip.Note,
                        ClassifiedId = ip.ClasstifiedId,
                        ClassifiedName = ip.MaterialClassified.MaterialClassifiedName,
                        Currency = ip.Currency.Trim(),
                        OrderQty = ip.OrderQty,
                        UnitPrice = ip.UnitPrice,
                        Unit = ip.Unit,
                        Status = ip.Status,
                        DueDate = ip.DueDate,
                        ReferenceId = ip.ReferenceId,
                        InquiryNumber = ip.InquiryNumber,
                        PurchasingSignatureType = 1,
                        Total3MonthsUsed = 0,
                        Standard = ip.Standard,
                        ManagerConfirm = ip.ManagerConfirm,
                        DateConfirm = ip.DateConfirm,
                        ApprovedPo = ip.ApprovedPo,
                        DateApproved = ip.DateApproved,

                    };
                    if (ip.VendorId != null) {
                        entity.VendorId = ip.VendorId.Value;
                        entity.VendorCode = ip.Vendor.VendorName;
                    }

                    if (ip.ManagementSignature == 1) {
                        entity.PurchasingSignatureType = 1;
                    }
                    else if (ManagementApproved) {
                        entity.PurchasingSignatureType = 2;
                    }
                    
                    switch (entity.ClassifiedId) {
                        case 1:
                            var material =
                                vfi.Materials.FirstOrDefault(m => m.MaterialId == entity.ReferenceId);
                            if (material == null) continue;
                            entity.TypeId = material.MaterialTypeId;
                            entity.TypeName = material.MaterialType.MaterialTypeName;
                            entity.ReferenceName = material.MaterialName;
                            entity.ReferenceCode = material.MaterialCode;

                            var materialInvs =
                                vfi.MaterialInventories.Where(
                                    mi => mi.MaterialId == entity.ReferenceId && mi.TotalQty > 0).ToList();
                            entity.TotalInv = materialInvs.Sum(mi => mi.TotalQty * mi.UnitWeight);
                            var exportMaterial = vfi.MaterialUseDetails.Where(t => t.MaterialUseInShift.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                                                   t.MaterialUseInShift.UsedDate.Year == todayYear &&
                                                                                   t.MaterialUseInShift.UsedDate.Month <= todayMonth &&
                                                                                   t.MaterialUseInShift.UsedDate.Month >= threeMonthBefor &&
                                                                                   t.MaterialInventory.MaterialId == entity.ReferenceId)
                                                                        .Select(t => new {
                                                                            t.MaterialInventory.MaterialId,
                                                                            t.MaterialUseInShift.UsedDate.Month,
                                                                            t.MaterialUseInShift.Type,
                                                                            QuantyKg = t.MaterialInventory.UnitWeight * (t.EditQuantity + t.EditQuantity2),
                                                                        }).ToList();

                            if (exportMaterial.Count != 0) {
                                double total3MonthUsed = 0;
                                for (int month = threeMonthBefor; month <= todayMonth; month++) {
                                    var exportsInMonth = exportMaterial.Where(t => t.MaterialId == entity.ReferenceId && t.Month == month).ToList();
                                    if (exportsInMonth.Any()) {
                                        var exportParam = exportsInMonth.Where(p => p.Type == 1).ToList();
                                        total3MonthUsed += exportParam.Sum(t => t.QuantyKg);
                                    }
                                }
                                entity.Total3MonthsUsed = Math.Round(total3MonthUsed, 1);
                            }

                            break;

                        case 2:
                            var fuel =
                                vfi.Fuels.FirstOrDefault(m => m.FuelId == entity.ReferenceId);
                            if (fuel == null) continue;
                            entity.ReferenceCode = fuel.FuelFullCode;
                            entity.ReferenceName = fuel.FuelName;

                            var fuelInvs =
                                vfi.FuelInventories.Where(
                                    mi => mi.FuelId == entity.ReferenceId && mi.TotalQuantity > 0).ToList();
                            entity.TotalInv = fuelInvs.Sum(mi => mi.TotalQuantity);
                            var exportFuels = vfi.FuelInventoryPeriods.Where(t => t.FuelId == entity.ReferenceId &&
                                                                                  t.PeriodDate.Year == todayYear &&
                                                                                  t.PeriodDate.Month >= threeMonthBefor &&
                                                                                  t.PeriodDate.Month <= todayYear &&
                                                                                  t.LastQuantity < t.EarlyQuantity)
                                                                      .Select(t => new {
                                                                          t.FuelId,
                                                                          t.Quantity,
                                                                          t.PeriodDate.Month,
                                                                          t.TransactionFpt.Type,
                                                                      }).ToList();
                            if (exportFuels.Count != 0) {
                                double total3MonthUsed = 0;
                                for (int month = threeMonthBefor; month <= todayMonth; month++) {
                                    var exportsInMonth = exportFuels.Where(t => t.FuelId == entity.ReferenceId && t.Month == month).ToList();
                                    if (exportsInMonth.Any()) {
                                        var exportsParam = exportsInMonth.Where(p => p.Type != (int)MyUtilities.Tool.ExportType.Destroy).ToList();
                                        total3MonthUsed += exportsParam.Sum(e => e.Quantity);
                                    }
                                }
                                entity.Total3MonthsUsed = Math.Round(total3MonthUsed, 2);

                            }

                            break;

                        case 3:
                            var tool =
                                vfi.Tools.FirstOrDefault(m => m.ToolId == entity.ReferenceId);
                            if (tool == null) continue;
                            entity.TypeId = tool.MaterialTypeId;
                            entity.TypeName = tool.MaterialType.MaterialTypeName;
                            entity.ReferenceCode = tool.ToolFullCode;
                            entity.ReferenceName = tool.ToolName;

                            var toolInvs =
                                vfi.ToolInventories.Where(
                                    mi => mi.ToolId == entity.ReferenceId && mi.TotalQuantity > 0).ToList();
                                entity.TotalInv = toolInvs.Sum(mi => mi.TotalQuantity);
                            var exportTools = vfi.ToolInventoryPeriods.Where(t => t.ToolId == entity.ReferenceId &&
                                                                              t.PeriodDate.Year == todayYear &&
                                                                              t.PeriodDate.Month >= threeMonthBefor &&
                                                                              t.PeriodDate.Month <= todayMonth &&
                                                                              t.LastQuantity < t.EarlyQuantity)
                                                                  .Select(t => new {
                                                                          t.ToolId,
                                                                          t.Quantity,
                                                                          t.PeriodDate.Month,
                                                                          t.TransactionFpt.Type,
                                                                  }).ToList();
                            if (exportTools.Count != 0) {
                                double total3MonthsUsed = 0;
                                for (int month = threeMonthBefor; month <= todayMonth; month++) {
                                    var exportsInMonths = exportTools.Where(t => t.ToolId == entity.ReferenceId && t.Month == month).ToList();
                                    if (exportsInMonths.Any()) {
                                        var exportsParam = exportsInMonths.Where(p => p.Type != (int)MyUtilities.Tool.ExportType.Destroy).ToList();
                                        total3MonthsUsed += exportsParam.Sum(e => e.Quantity);
                                    }
                                }
                                entity.Total3MonthsUsed = Math.Round(total3MonthsUsed, 1);
                            }
                            break;
                        default:
                            break;
                    }
                    model.Add(entity);
                }
            }
            return model;
        }




        [GridAction]
        public ActionResult CreateNewInquiryPo(
            [Bind(Prefix = "inserted")] IEnumerable<InquiryPoModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<InquiryPoModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<InquiryPoModel> deletedDetails,
            int materialClassifiedId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreateNewInquiryPo",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<InquiryPoModel>()));
            }
            try {
                if (insertedDetails != null) {
                    using (var vfi = new tammaContext()) {
                        foreach (var inserted in insertedDetails) {
                            if (inserted.ReferenceId <= 0) {
                                throw new AggregateException("Lỗi! Vui lòng chọn lại mã " + inserted.ReferenceCode);
                            }
                            if (string.IsNullOrWhiteSpace(inserted.Unit))
                                throw new AggregateException("Lỗi! Chưa chọn đơn vị tính");
                            //if (inserted.OrderQty == 0) continue;
                            if (inserted.OrderQty <= 0)
                                throw new AggregateException("Lỗi! Chưa nhập số lượng yêu cầu");
                            if (inserted.UnitPrice > 0 && string.IsNullOrWhiteSpace(inserted.Currency))
                                throw new AggregateException("Lỗi! Chưa chọn tiền tệ");
                            if (inserted.DueDate == null)
                                throw new AggregateException("Lỗi! Chưa nhập ngày yêu cầu");

                            if (inserted.Standard == null)
                                throw new AggregateException("Quên nhập tiêu chuẩn rồi kìa");
                            if (inserted.VendorId == 0) {
                                throw new AggregateException("Lỗi! Chưa chọn Nhà cung cấp");
                            }
                            var inquiry = new InquiryPo {
                                ClasstifiedId = materialClassifiedId,
                                Currency = (inserted.Currency + ""),
                                DueDate = inserted.DueDate,
                                ReferenceId = inserted.ReferenceId,
                                Status = (byte)MyUtilities.PurchaseOrder.InquiryEnum.Pending,
                                Unit = (inserted.Unit + ""),
                                UnitPrice = inserted.UnitPrice,
                                OrderQty = inserted.OrderQty,
                                Note = inserted.Note,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                Standard = inserted.Standard,
                                //VendorId = inserted.VendorId
                            };
                            if (inserted.VendorId > 0)
                                inquiry.VendorId = inserted.VendorId;
                            //if(inserted.DueDate)

                            vfi.InquiryPoes.Add(inquiry);
                        }
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CreateNewInquiryPo", ex.Message);
            }
            return View(new GridModel(new List<InquiryPoModel>()));
        }

        [GridAction]
        public ActionResult UpdateInquiryPo(InquiryPoModel update, int classified, int type, int vendorId) {
            try {
                //if (update.OrderQty == 0)
                //    throw new AggregateException("Đùa nhau ah! Huỷ yêu cầu đi!");
                using (var vfi = new tammaContext()) {
                    var inquiry = vfi.InquiryPoes.FirstOrDefault(ip => ip.InquiryId == update.InquiryId);
                    if (inquiry.VendorId != null) {
                        var oldVendorId = vfi.InquiryPoes.Where(t => t.VendorId == inquiry.VendorId).Select(t => t.Vendor.VendorId).FirstOrDefault();
                    }
                    if (inquiry == null)
                        throw new AggregateException("Không tìm thấy yêu cầu");
                    if (update.UnitPrice > 0 && string.IsNullOrWhiteSpace(update.Currency))
                        throw new AggregateException("Lỗi! Chưa chọn tiền tệ");
                    var vendorUpdateId = 0;
                    try {
                        vendorUpdateId = Convert.ToInt32(update.VendorCode);
                    }
                    catch (Exception ex) {
                    }
                    if (vendorUpdateId != 0)
                    inquiry.VendorId = vendorUpdateId;
                    inquiry.Unit = update.Unit;
                    inquiry.UnitPrice = update.UnitPrice;
                    if (inquiry.UnitPrice == 0) {
                        inquiry.Currency = "";
                    }
                    else {
                        inquiry.Currency = (update.Currency + "");
                    }
                    inquiry.Note = update.Note;
                    //inquiry.OrderQty = update.OrderQty;
                    inquiry.DueDate = update.DueDate;
                    //inquiry.ManagementSignature = 1;
                    //update.PurchasingSignatureType = 1;
                    if (update.Standard != "") {
                        inquiry.Standard = update.Standard;
                    }

                    if (vendorUpdateId != 0) {
                        if (inquiry.InquiryNumber != null) {
                            var vendorCode = vfi.Vendors.Where(t => t.VendorId == vendorUpdateId).Select(t => t.VendorCode).FirstOrDefault();
                            var datePart = inquiry.InquiryNumber.Substring(inquiry.InquiryNumber.Length - 6);
                            inquiry.InquiryNumber = vendorCode + datePart;
                        }
                        else if (inquiry.InquiryNumber == null) {
                            update.InquiryNumber = null;
                        }
                    }

                    else if (vendorUpdateId == 0) {
                        if (inquiry.InquiryNumber != null) {
                            update.InquiryNumber = inquiry.InquiryNumber;
                        }
                        else if (inquiry.InquiryNumber == null) {
                            update.InquiryNumber = null;
                        }
                    }





                    //    // nếu chưa có mã thì tạo mới
                    //    //var now = DateTime.Now;
                    //    //var datePart = now.ToString("yyMMdd");
                    //    //var vendorCode = vfi.InquiryPoes.Where(t => t.VendorId == vendorUpdateId).Select(t => t.Vendor.VendorCode).FirstOrDefault();
                    //    //inquiry.InquiryNumber = vendorCode + datePart;


                    //if (!string.IsNullOrWhiteSpace(inquiry.InquiryNumber)) {
                    //    inquiry.Status = (byte)MyUtilities.PurchaseOrder.InquiryEnum.Approved;
                    //}
                    //else {
                    //    inquiry.Status = (byte)MyUtilities.PurchaseOrder.InquiryEnum.Approved;
                    //    throw new AggregateException("GG!");
                    //}
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("UpdateInquiryPo", ex.Message);
            }
            return View(new GridModel( GetInquiryPo(classified, type, vendorId, 0, "", "")));
        }

        [GridAction]
        public ActionResult CancelInquiryPo(InquiryPoModel update, int classified, int type, int vendorId) {
            try {
                using (var vfi = new tammaContext()) {
                    var inquiry = vfi.InquiryPoes.FirstOrDefault(ip => ip.InquiryId == update.InquiryId);
                    if (inquiry == null)
                        throw new AggregateException("Không tìm thấy yêu cầu");
                    inquiry.Status = (byte)MyUtilities.PurchaseOrder.InquiryEnum.Cancel;
                    inquiry.DateApproved = DateTime.Now;
                    inquiry.ApprovedPo = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateInquiryPo", ex.Message);
            }
            return View(new GridModel( GetInquiryPo(classified, type, vendorId, 0, "", "")));
        }

        public ActionResult TransformToPo(string ids) {
            var model = new List<InquiryPoModel>();
            var checkedRecords = new List<long>();
            try {
                var lst = ids.Split(':');
                for (var i = 0; i < lst.Count(); i++) {
                    checkedRecords.Add(Convert.ToInt64(lst.ElementAt(i)));
                }
                //using (var vfi = new tammaContext()) {
                //var entity = new InquiryPoModel {
                //    DateApproved = DateTime.Now,
                //};
                //var managerConfirm = HttpContext.User.Identity.Name;
                //entity.ApprovedPo = vfi.Users.Where(t => managerConfirm.Contains(t.Username)).Select(t => t.FullName).FirstOrDefault(); 
                //model.Add(entity);
                //}
            }
            catch (FormatException) {
                return View(new GridModel(new List<MaterialInventoryModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var inquiries = vfi.InquiryPoes.Where(ip => checkedRecords.Contains(ip.InquiryId)).ToList();
                    if (!inquiries.Any())
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết cần chuyển");
                    var msg = "";
                    if (inquiries.Any(ip => string.IsNullOrWhiteSpace(ip.InquiryNumber)))
                        msg = "Chưa có nhập số phiếu đầy đủ \n";
                    if (inquiries.Any(ip => ip.VendorId == null))
                        msg = "Vui lòng điền đủ nhà cung cấp \n";
                    //if (inquiries.Any(ip => ip.UnitPrice == 0))
                    //    msg = "Vui lòng điền đủ đơn giá \n";
                    if (inquiries.Select(ip => ip.VendorId).Distinct().Count() > 1)
                        msg = "Chuyển phiếu mua hàng chỉ được 1 nhà cung cấp \n";
                    if (inquiries.Select(ip => ip.Currency).Distinct().Count() > 1)
                        msg = "Chuyển phiếu mua hàng chỉ được 1 đơn vị tiền tệ \n";
                    //if (inquiries.Any(ip => ip.Unit.Length > 0))
                    //    msg = "Chuyển phiếu mua hàng chỉ được 1 đơn vị tiền tệ \n";
                    if (!string.IsNullOrWhiteSpace(msg))
                        throw new AggregateException(msg);

                    var inquiryId = checkedRecords.FirstOrDefault();

                    var import = vfi.InquiryPoes.FirstOrDefault(t => t.InquiryId == inquiryId);
                    var approvedPo = HttpContext.User.Identity.Name;
                    import.ApprovedPo = vfi.Users.Where(t => t.Username == approvedPo).Select(t => t.FullName).FirstOrDefault();
                    import.DateApproved = DateTime.Now;
                    

                    var purchaseOrder = new PurchaseOrder {
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        //EmployeeName = employeeName,
                        VendorId = inquiries.FirstOrDefault().VendorId.Value,
                        ShipDate = null,
                        OrderDate = DateTime.Now,
                        RevisionNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.PurchaseOrder, 1),
                        Active = false,
                        Status = (byte)MyUtilities.Sales.Status.Waiting,
                        Tolerance = 10,
                        Note = "",
                        CurrencyCode = inquiries.FirstOrDefault().Currency,
                        MaterialClassifiedId = inquiries.FirstOrDefault().ClasstifiedId,
                        ContractNumber = "",
                        AddressId = 1,
                    };
                    foreach (var ip in inquiries) {
                        if (string.IsNullOrWhiteSpace(ip.Unit))
                            throw new AggregateException("Thiếu đơn vị tính");
                        var poDetail = new PurchaseOrderDetail {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            PurchaseOrder = purchaseOrder,
                            PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                            ReferenceId = ip.ReferenceId,
                            OrderQty = ip.OrderQty,
                            UnitPrice = ip.UnitPrice,
                            DueDate = ip.DueDate ?? DateTime.Now,
                            ReceivedQty = 0,
                            RejectedQty = 0,
                            Active = true,
                            Unit = ip.Unit,
                            IsComplete = false,
                            MaterialClassifiedId = purchaseOrder.MaterialClassifiedId,
                            Standard = ip.Standard,
                            Note = ip.Note,
                            ApprovedPo = import.ApprovedPo,
                            DateApproved = import.DateApproved,
                        };
                            purchaseOrder.PurchaseOrderDetails.Add(poDetail);
                            ip.PurchaseOrderDetail = poDetail;
                            ip.Status = (byte)MyUtilities.PurchaseOrder.InquiryEnum.MakePo;
                            ip.ApprovedPo = import.ApprovedPo;
                            ip.DateApproved = import.DateApproved;
                    }
                    vfi.PurchaseOrders.Add(purchaseOrder);
                    vfi.SaveChanges();
                    return Json("Thành công");

                }
            }
            catch (Exception ex) {
                var message = ex.Message;
                if (ex.InnerException != null) {
                    message += " | Inner Exception: " + ex.InnerException.Message;
                }
                return Json(message);
            }

            return Json("Lỗi");
        }




        public ActionResult PrintInquiryForm(string ids) {
            var model = new List<InquiryPoModel>();

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
            try {
                using (var vfi = new tammaContext()) {
                    var inquiries = vfi.InquiryPoes.Where(ip => checkedRecords.Contains(ip.InquiryId));
                    var inquiriesNumber = inquiries.Select(x => x.InquiryNumber).Distinct().ToList();
                    if (inquiriesNumber.Count > 1) {
                        throw new AggregateException("Lỗi! Danh sách in có nhiều số yêu cầu! Chỉ được cho phép có 1 số yêu cầu");
                    }
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
                    foreach (var ip in inquiries) {
                        var entity = new InquiryPoModel {
                            ModifiedDate = ip.ModifiedDate,
                            ModifiedUser = ip.ModifiedUser,
                            InquiryId = ip.InquiryId,
                            Note = ip.Note,
                            ClassifiedId = ip.ClasstifiedId,
                            ClassifiedName = ip.MaterialClassified.MaterialClassifiedName,
                            Currency = ip.Currency.Trim(),
                            OrderQty = ip.OrderQty,
                            UnitPrice = ip.UnitPrice,
                            Unit = ip.Unit,
                            Status = ip.Status,
                            DueDate = ip.DueDate,
                            ReferenceId = ip.ReferenceId,
                            Info = info,
                            InquiryNumber = ip.InquiryNumber,
                            //State = ip.Status
                        };
                        if (ip.VendorId != null) {
                            entity.VendorId = ip.VendorId.Value;
                            entity.VendorCode = ip.Vendor.VendorName;
                        }

                        switch (entity.ClassifiedId) {
                            case 1:
                                var material =
                                    vfi.Materials.FirstOrDefault(m => m.MaterialId == entity.ReferenceId);
                                entity.TypeId = material.MaterialTypeId;
                                entity.TypeName = material.MaterialType.MaterialTypeName;
                                entity.ReferenceName = material.MaterialName;
                                entity.ReferenceCode = material.MaterialCode;

                                var materialInvs =
                                    vfi.MaterialInventories.Where(
                                        mi =>
                                        mi.MaterialId == entity.ReferenceId && mi.TotalQty > 0).ToList();
                                entity.TotalInv = materialInvs.Sum(mi => mi.TotalQty * mi.UnitWeight);

                                break;
                            case 2:
                                var fuel =
                                    vfi.Fuels.FirstOrDefault(m => m.FuelId == entity.ReferenceId);
                                entity.ReferenceName = fuel.FuelName;
                                entity.ReferenceCode = fuel.FuelFullCode;

                                var fuelInvs =
                                    vfi.FuelInventories.Where(
                                        mi =>
                                        mi.FuelId == entity.ReferenceId && mi.TotalQuantity > 0).ToList();
                                entity.TotalInv = fuelInvs.Sum(mi => mi.TotalQuantity);
                                break;
                            case 3:
                                var tool =
                                    vfi.Tools.FirstOrDefault(m => m.ToolId == entity.ReferenceId);
                                entity.TypeId = tool.MaterialTypeId;
                                entity.TypeName = tool.MaterialType.MaterialTypeName;
                                entity.ReferenceName = tool.ToolName;
                                entity.ReferenceCode = tool.ToolFullCode;

                                var toolInvs =
                                    vfi.ToolInventories.Where(
                                        mi =>
                                        mi.ToolId == entity.ReferenceId && mi.TotalQuantity > 0).ToList();
                                entity.TotalInv = toolInvs.Sum(mi => mi.TotalQuantity);
                                break;
                            default:
                                break;
                        }
                        model.Add(entity);
                    }
                    return PartialView("PagePrintInquiryForm", model);
                }
            }
            catch (Exception ex) {
                //ModelState.AddModelError("PrintInquiryForm", ex.Message);
                //return PartialView("PagePrintInquiryForm", ex.Message);
                return Json(ex.Message);
            }
            //return PartialView(null);
        }

        public ActionResult SelectComboBoxAllInquiryStatus() {
            var val = from MyUtilities.PurchaseOrder.InquiryEnum stt in Enum.GetValues(typeof(MyUtilities.PurchaseOrder.InquiryEnum))
                      select new {
                          Value = (int)Enum.Parse(typeof(MyUtilities.PurchaseOrder.InquiryEnum), stt.ToString()),
                          Text =
                      MyUtilities.PurchaseOrder.GetInquiryEnumStatusName(
                          (int)Enum.Parse(typeof(MyUtilities.PurchaseOrder.InquiryEnum), stt.ToString()))
                      };

            return new JsonResult {
                Data = new SelectList(val, "Value", "Text")
            };
        }


        public ActionResult SelectComboBoxInquiryStatus() {
            var val = from MyUtilities.PurchaseOrder.InquiryEnum stt in Enum.GetValues(typeof(MyUtilities.PurchaseOrder.InquiryEnum))
                      where stt != MyUtilities.PurchaseOrder.InquiryEnum.MakePo &&
                            stt != MyUtilities.PurchaseOrder.InquiryEnum.Cancel
                      select new {
                          Value = (int)Enum.Parse(typeof(MyUtilities.PurchaseOrder.InquiryEnum), stt.ToString()),
                          Text =
                      MyUtilities.PurchaseOrder.GetInquiryEnumStatusName(
                          (int)Enum.Parse(typeof(MyUtilities.PurchaseOrder.InquiryEnum), stt.ToString()))
                      };

            return new JsonResult {
                Data = new SelectList(val, "Value", "Text")
            };
        }

        // 15/04/2026
        public ActionResult SelectComboBoxEvaluationEnum() {
            var val = from MyUtilities.PurchaseOrder.EvaluationEnum stt in Enum.GetValues(typeof(MyUtilities.PurchaseOrder.EvaluationEnum))
                      select new {
                          Value = (int)Enum.Parse(typeof(MyUtilities.PurchaseOrder.EvaluationEnum), stt.ToString()),
                          Text = MyUtilities.PurchaseOrder.GetEvaluationEnumStatusName(
                              (int)Enum.Parse(typeof(MyUtilities.PurchaseOrder.EvaluationEnum), stt.ToString()))
                      };
            return new JsonResult {
                Data = new SelectList(val, "Value", "Text")
            };
        }

        #endregion

        #region Fuel


        [GridAction]
        public ActionResult UpdateFuelPurchaseOrderDetail(
            [Bind(Prefix = "inserted")] IEnumerable<PurchaseOrderDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<PurchaseOrderDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<PurchaseOrderDetailModel> deletedDetails,
            //int materialClassifiedId, int materialTypeId, 
            string shipDate,
            int vendorId, int shipMethodId, int deliveryMethodId,
            int packagedMethodId, int paymentMethodId, string employeeName,
            int tolerance, string currencyCode, string note
            ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateFuelPurchaseOrderDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<PurchaseOrderDetailModel>()));
            }
            try {
                if (insertedDetails != null) {
                    using (var vfi = new tammaContext()) {
                        var purchaseOrder = new PurchaseOrder {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            EmployeeName = employeeName,
                            VendorId = vendorId,
                            ShipDate = null,
                            OrderDate = DateTime.Now,
                            RevisionNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.PurchaseOrder, 1),
                            Active = false,
                            Status = (byte)MyUtilities.Sales.Status.Waiting,
                            Tolerance = tolerance,
                            Note = note,
                            CurrencyCode = currencyCode,
                            MaterialClassifiedId = 2
                        };
                        foreach (var detail in insertedDetails) {
                            if (detail.OrderQty > 0) {
                                var fuel = vfi.Fuels.FirstOrDefault(m => m.FuelId == detail.ToolId);
                                if (fuel == null)
                                    throw new AggregateException("Công cụ lỗi " + detail.FuelCode);
                                var poDetail = new PurchaseOrderDetail {
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    PurchaseOrder = purchaseOrder,
                                    PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                                    //FuelId = fuel.FuelId,
                                    OrderQty = detail.OrderQty,
                                    UnitPrice = detail.UnitPrice,
                                    DueDate = detail.DueDate,
                                    ReceivedQty = 0,
                                    RejectedQty = 0,
                                    Active = true,
                                    Unit = detail.Unit,
                                    IsComplete = false
                                };
                                purchaseOrder.PurchaseOrderDetails.Add(poDetail);
                            }
                        }
                        if (purchaseOrder.PurchaseOrderDetails.Any()) {
                            vfi.PurchaseOrders.Add(purchaseOrder);
                            vfi.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateToolPO",
                                         ex.Message);
            }
            return View(new GridModel(new List<PurchaseOrderDetailModel>()));
        }

        [GridAction]
        public ActionResult SelectImportFuelForm(string fromDate, string toDate) {
            var model = new List<TransactionFptModel>();
            try {
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
                    var transactions = from i in vfi.TransactionFpts
                                       where i.Status == 2 &&
                                             i.TransactionDate >= fdate &&
                                             i.TransactionDate <= tdate &&
                                             i.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import
                                       select i;
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
                        };
                        if (entity.PoId != 0)
                            entity.PoCode = transaction.PurchaseOrder.RevisionNumber;
                        entity.StatusName = MyUtilities.Transaction.CastText.GetTextStatus(entity.Status);
                        if (entity.EoI != 0)
                            entity.EoIName = MyUtilities.PurchaseOrder.GetEoIName(entity.EoI, entity.Type);
                        if (entity.Fpt != 0)
                            entity.FptName = MyUtilities.PurchaseOrder.GetFptName(entity.Fpt);
                        if (purchasing) {
                            entity.PurchasingSignatureType = 1;
                        }
                        foreach (var detail in transaction.TransactionFptDetails) {
                            entity.TotalQuantity += detail.Quantity;
                            entity.TotalPrice += (detail.Quantity * detail.UnitPrice);
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectManageImportFuel", "\n" + ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.TransactionDate)));
        }


        #endregion

        #region plating

        public ActionResult SelectComboBoxPlating(int productId) {
            var model = new List<ProductionPlatingModel>();
            using (var vfi = new tammaContext()) {
                var productPlatings = from pp in vfi.ProductionPlatings
                                      where pp.Active && (productId == 0 || pp.ProductId == productId)
                                      select new {
                                          pp.PlatingId,
                                          pp.PlatingName,
                                      };
                foreach (var productPlating in productPlatings) {
                    var entity = new ProductionPlatingModel {
                        PlatingId = productPlating.PlatingId,
                        PlatingName = productPlating.PlatingName,
                    };
                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "PlatingId", "PlatingName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [GridAction]
        public ActionResult SelectPlatingDetail() {
            return View(new GridModel(new List<PlatingDetailModel>()));
        }

        [GridAction]
        public ActionResult CreatePlatingDetail(
            [Bind(Prefix = "inserted")] IEnumerable<PlatingDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<PlatingDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<PlatingDetailModel> deletedDetails,
            int vendorId, int platingType, int exchangeRate, string currencyCode, string note
            ) {

            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<PlatingDetailModel>()));
            }
            try {

                if (insertedDetails != null && insertedDetails.Count() > 0) {
                    var errMsg = "";
                    if (insertedDetails.Any(x => x.ProductId == 0)) {
                        errMsg += "Lỗi! Có sản phẩm lỗi! \r\n";
                    }
                    if (insertedDetails.Any(x => x.QuantityRequirement <= 0)) {
                        errMsg += "Lỗi! Số lượng yêu cầu phải lớn hơn 0! \r\n";
                    }
                    if (insertedDetails.Any(x => string.IsNullOrWhiteSpace(x.Unit))) {
                        errMsg += "Lỗi! Đơn vị tính lỗi! \r\n";
                    }
                    if (insertedDetails.Select(x => x.ProductId).Distinct().Count() != insertedDetails.Count()) {
                        errMsg += "Lỗi! Có sản phẩm nhập 2 lần \r\n";
                    }
                    if (!string.IsNullOrWhiteSpace(errMsg)) {
                        throw new AggregateException(errMsg);
                    }
                    using (var vfi = new tammaContext()) {
                        var entity = new PlatingForm {
                            CreateDate = DateTime.Now,
                            CreateUser = HttpContext.User.Identity.Name,
                            Status = (byte)MyUtilities.Sales.Status.Waiting,
                            VendorId = vendorId,
                            CurrencyCode = currencyCode,
                            ExchangeRate = exchangeRate,
                            Note = note,
                            PlatingFormNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Plating, 1),
                            PlatingFormDetails = new List<PlatingFormDetail>(),
                            PlatingType = platingType,
                        };
                        foreach (var detailModel in insertedDetails) {
                            var detail = new PlatingFormDetail {
                                FormId = entity.FormId,
                                ProductId = detailModel.ProductId,
                                PlatingForm = entity,
                                ExportDateRequirement = detailModel.ExportDateRequirement,
                                ImportDateRequirement = detailModel.ImportDateRequirement,
                                PlatingCode = detailModel.PlatingCode,
                                QuantityRequirement = detailModel.QuantityRequirement,
                                SaltSprayTime = detailModel.SaltSprayTime + "",
                                Sample = detailModel.Sample + "",
                                SpecialRequest = detailModel.SpecialRequest + "",
                                TestingEquipment = detailModel.TestingEquipment + "",
                                Thickness = detailModel.Thickness + "",
                                Unit = detailModel.Unit + "",
                                UnitPrice = detailModel.UnitPrice,
                                Note = detailModel.Note + ""
                            };
                            entity.PlatingFormDetails.Add(detail);
                        }
                        vfi.PlatingForms.Add(entity);
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CreatePlatingDetail", ex.Message);
            }
            return View(new GridModel(new List<PlatingDetailModel>()));
        }

        [GridAction]
        public ActionResult SelectWaitingPlatingForm() {
            //var model = new List<ImportFuelModel>();
            try {
                return View(new GridModel(GetPlatingFormModel().OrderBy(m => m.CreateDate)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportFuelByStatus", "" + ex.Message);
            }
            return View(new GridModel(new List<PlatingFormModel>()));
        }

        private List<PlatingFormModel> GetPlatingFormModel() {
            var model = new List<PlatingFormModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var forms = from pf in vfi.PlatingForms
                                where pf.Status == (byte)MyUtilities.Transaction.Status.Open
                                select pf;
                    var platingWarehouse = vfi.Warehouses.Where(w => w.WarehouseTypeId == 1);
                    foreach (var platingForm in forms) {
                        var entity = new PlatingFormModel {
                            CreateDate = platingForm.CreateDate,
                            CreateUser = platingForm.CreateUser,
                            CurrencyCode = platingForm.CurrencyCode,
                            ExchangeRate = platingForm.ExchangeRate,
                            Note = platingForm.Note,
                            PlatingFormNumber = platingForm.PlatingFormNumber,
                            StatusName = MyUtilities.PurchaseOrder.GetPlatingStatusText(platingForm.Status),
                            VendorCode = platingForm.Vendor.VendorCode,
                            VendorName = platingForm.Vendor.VendorName,
                            Status = platingForm.Status,
                            VendorId = platingForm.VendorId,
                            FormId = platingForm.FormId,
                            PlatingType = platingForm.PlatingType,
                        };
                        entity.PlatingTypeName =
                            platingWarehouse.FirstOrDefault(pw => pw.WarehouseId == entity.PlatingType).WarehouseName;
                        entity.TotalQuantityRequirement = platingForm.PlatingFormDetails.Sum(pfd => pfd.QuantityRequirement);
                        entity.TotalPrice =
                            platingForm.PlatingFormDetails.Sum(pfd => pfd.QuantityRequirement * pfd.UnitPrice);
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportFuelByStatus", "/n" + ex.Message);
            }
            return model;
        }

        [GridAction]
        public ActionResult CancelPlatingForm(int formId) {
            try {
                using (var vfi = new tammaContext()) {
                    var form = vfi.PlatingForms.FirstOrDefault(pf => pf.FormId == formId);
                    if (form == null) throw new AggregateException("Lỗi phiếu nhập ! Không tìm thấy phiếu nhập");
                    form.Status = (byte)MyUtilities.Sales.Status.Cancel;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CancelImportFuel ", "/n" + ex.Message);
            }
            return View(new GridModel(GetPlatingFormModel().OrderBy(m => m.CreateDate)));
        }

        [HttpPost]
        public ActionResult UpdateApprovePlatingForm(long[] checkedRecords) {
            try {
                if (!Request.IsAuthenticated)
                    return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                var modifiedUser = HttpContext.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(modifiedUser))
                    return Json(@"Vui lòng đăng nhập hệ thống. (user null). ");
                if (checkedRecords.Count() <= 0)
                    return Json(@"Vui lòng chọn ít nhất 1 lệnh. (checkedRecords <= 0). ");
                using (var vfi = new tammaContext()) {
                    var forms =
                        vfi.PlatingForms.Where(f => checkedRecords.Contains(f.FormId));
                    foreach (var form in forms) {
                        form.Status = (byte)MyUtilities.Sales.Status.InProcess;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                return
                    Json(@"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }
            return Json("okie");
        }

        [GridAction]
        public ActionResult UpdatePlatingFormDetail(PlatingDetailModel update) {
            try {
                using (var vfi = new tammaContext()) {
                    var platingDetail = vfi.PlatingFormDetails.FirstOrDefault(pfd => pfd.DetailId == update.DetailId);
                    if (platingDetail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết cần sửa!");
                    update.FormId = platingDetail.FormId;
                    platingDetail.UnitPrice = update.UnitPrice;
                    //platingDetail.Unit = update.Unit;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdatePlatingFormDetail", ex.Message);
            }
            return View(new GridModel(GetPlatingFormDetails(update.FormId)));
        }

        [GridAction]
        public ActionResult UpdatePlatingDetailInfo(PlatingDetailModel update) {
            try {
                using (var vfi = new tammaContext()) {
                    var platingDetail = vfi.PlatingFormDetails.FirstOrDefault(pfd => pfd.DetailId == update.DetailId);
                    if (platingDetail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết cần sửa!");
                    update.FormId = platingDetail.FormId;
                    var platingCodeId = 0;
                    try {
                        platingCodeId = Convert.ToInt32(update.PlatingCode);
                    }
                    catch (Exception ex) {
                    }
                    if (platingCodeId != 0) {
                        var plating =
                            vfi.ProductionPlatings.FirstOrDefault(pp => pp.PlatingId == platingCodeId);
                        platingDetail.PlatingCode = plating.PlatingName;

                    }
                    platingDetail.Thickness = update.Thickness + "";
                    platingDetail.SaltSprayTime = update.SaltSprayTime + "";
                    platingDetail.SpecialRequest = update.SpecialRequest + "";
                    platingDetail.Sample = update.Sample + "";
                    platingDetail.TestingEquipment = update.TestingEquipment + "";
                    platingDetail.Note = update.Note + "";
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdatePlatingFormDetail", ex.Message);
            }
            return View(new GridModel(GetPlatingFormDetails(update.FormId)));
        }

        List<PlatingDetailModel> GetPlatingFormDetails(int formId) {
            var model = new List<PlatingDetailModel>();
            var purchaseManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                MyUtilities.UserRole.PurchasingManagement);

            using (var vfi = new tammaContext()) {
                var platingForm = vfi.PlatingForms.FirstOrDefault(pf => pf.FormId == formId);
                var exportDetails = (from ed in vfi.ExportGCN_NCUDetail
                                    where
                                        ed.ExportGCN_NCU.PlatingFormId == formId &&
                                        ed.ExportGCN_NCU.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved
                                    select new {
                                        ed.RealNumber,
                                        ed.PlatingDetailId,
                                        Weight = ed.Weight ?? 0
                                    }).ToList();
                var importDetails = (from id in vfi.ImportNCU_QCBDetail
                                    where id.ImportNCU_QCB.PlatingFormId == formId &&
                                          id.ImportNCU_QCB.Transaction.Status ==
                                          (byte)MyUtilities.Transaction.Status.Approved
                                    select new {
                                        id.RealNumber,
                                        id.Weight,
                                        id.ExportGCN_NCUDetail,
                                        id.ExportGCN_NCUDetail.PlatingDetailId
                                    }).ToList();
                foreach (var detailModel in platingForm.PlatingFormDetails) {
                    var detail = new PlatingDetailModel {
                        FormId = detailModel.FormId,
                        ProductId = detailModel.ProductId,
                        ExportDateRequirement = detailModel.ExportDateRequirement,
                        ImportDateRequirement = detailModel.ImportDateRequirement,
                        PlatingCode = detailModel.PlatingCode,
                        QuantityRequirement = detailModel.QuantityRequirement,
                        SaltSprayTime = detailModel.SaltSprayTime,
                        Sample = detailModel.Sample,
                        SpecialRequest = detailModel.SpecialRequest,
                        TestingEquipment = detailModel.TestingEquipment,
                        Thickness = detailModel.Thickness,
                        Unit = detailModel.Unit,
                        UnitPrice = detailModel.UnitPrice,
                        ProductCode = detailModel.Product.ProductCode,
                        Note = detailModel.Note,
                        Export = 0,
                        Import = 0,
                        PurchaseManager = purchaseManager,
                        DetailId = detailModel.DetailId
                    };
                    var exportDetailsById = exportDetails.Where(id => id.PlatingDetailId == detailModel.DetailId);
                    if (exportDetailsById.Any()) {
                        if (detail.Unit.Contains("Kg"))
                            detail.ExportString = string.Format("{0:n3}",
                                                                exportDetailsById.Sum(id => id.Weight / 1000));
                        else
                            detail.ExportString = string.Format("{0:n0}",
                                                                exportDetailsById.Sum(id => id.RealNumber));
                    }
                    var importDetailsById = importDetails.Where(id => id.PlatingDetailId == detailModel.DetailId);
                    if (importDetailsById.Any()) {
                        if (detail.Unit.Contains("Kg"))
                            detail.ImportString = string.Format("{0:n3}",
                                                                importDetailsById.Sum(id => id.Weight / 1000));
                        else
                            detail.ImportString = string.Format("{0:n0}", importDetailsById.Sum(id => id.RealNumber));
                    }
                    if (platingForm.CurrencyCode.Equals("USD")) {
                        detail.UnitPriceString = string.Format("{0:n4}", detail.UnitPrice);
                    }
                    else { detail.UnitPriceString = string.Format("{0:n0}", detail.UnitPrice); }
                    model.Add(detail);
                }
            }
            return model;
        }
        [GridAction]
        public ActionResult SelectPlatingFormDetail(int formId) {
            var model = new List<PlatingDetailModel>();
            try {
                model = GetPlatingFormDetails(formId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectPlatingFormDetail", "\n" + ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectExportPlating(int formId) {
            var model = new List<ExportGCN_NCUModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var exports = vfi.ExportGCN_NCU.Where(e => e.PlatingFormId == formId);
                    foreach (var export in exports) {
                        var transaction =
                            vfi.Transactions.FirstOrDefault(
                                t =>
                                t.TransactionCode.Equals(export.TransactionCode) &&
                                t.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        if (transaction == null) continue;
                        var entity = new ExportGCN_NCUModel {
                            TransactionCode = export.TransactionCode,
                            ModifiedDate = export.ModifiedDate,
                            ModifiedUser = export.ModifiedUser,
                            BlockNumber = export.BlockNumber,
                            BoxNumber = export.BoxNumber,
                            ExportDate = export.ExportDate,
                            ExportFormId = export.ExportId,
                            TransactionId = transaction.TransactionId,
                            TotalNumber = export.ExportGCN_NCUDetail.Sum(ed => ed.RealNumber),
                            TotalWeight = export.ExportGCN_NCUDetail.Sum(id => id.Weight ?? 0),

                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectExportPlating", "\n" + ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectImportPlating(int formId) {
            var model = new List<ImportNCU_QCBModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var imports = vfi.ImportNCU_QCB.Where(e => e.PlatingFormId == formId);
                    foreach (var import in imports) {
                        var transaction =
                            vfi.Transactions.FirstOrDefault(
                                t =>
                                t.TransactionCode.Equals(import.TransactionCode) &&
                                t.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        if (transaction == null) continue;
                        var entity = new ImportNCU_QCBModel {
                            TransactionCode = import.TransactionCode,
                            ModifiedDate = import.ModifiedDate,
                            ModifiedUser = import.ModifiedUser,
                            BoxNumber = import.BoxNumber,
                            ImportDate = import.ImportDate,
                            ImportId = import.ImportId,
                            TransactionId = transaction.TransactionId,
                            TotalNumber = import.ImportNCU_QCBDetail.Sum(id => id.RealNumber),
                            TotalWeight = import.ImportNCU_QCBDetail.Sum(id => id.Weight),
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectImportPlating", "\n" + ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectPlatingFormUnfinish() {
            return View(new GridModel(GetPlatingFormUnfinish().OrderBy(m => m.CreateDate)));
        }

        public List<PlatingFormModel> GetPlatingFormUnfinish() {
            var model = new List<PlatingFormModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                using (var vfi = new tammaContext()) {
                    var forms = from pf in vfi.PlatingForms
                                where pf.Status == (byte)MyUtilities.Sales.Status.InProcess
                                select pf;
                    foreach (var platingForm in forms) {
                        var entity = new PlatingFormModel {
                            CreateDate = platingForm.CreateDate,
                            CreateUser = platingForm.CreateUser,
                            CurrencyCode = platingForm.CurrencyCode,
                            ExchangeRate = platingForm.ExchangeRate,
                            Note = platingForm.Note,
                            PlatingFormNumber = platingForm.PlatingFormNumber,
                            StatusName = MyUtilities.PurchaseOrder.GetPlatingStatusText(platingForm.Status),
                            VendorCode = platingForm.Vendor.VendorCode,
                            VendorName = platingForm.Vendor.VendorName,
                            Status = platingForm.Status,
                            VendorId = platingForm.VendorId,
                            FormId = platingForm.FormId,
                        };
                        entity.TotalQuantityRequirement = platingForm.PlatingFormDetails.Sum(pfd => pfd.QuantityRequirement);
                        entity.TotalPrice =
                            platingForm.PlatingFormDetails.Sum(pfd => pfd.QuantityRequirement * pfd.UnitPrice);

                        foreach (var import in platingForm.ImportNCU_QCB.Where(i => i.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved)) {
                            foreach (var detail in import.ImportNCU_QCBDetail) {
                                var unit = detail.ExportGCN_NCUDetail.PlatingFormDetail.Unit;
                                var unitPrice = detail.ExportGCN_NCUDetail.PlatingFormDetail.UnitPrice;
                                if (unit.Contains("Kg")) {
                                    entity.TotalImportPrice += (detail.Weight) * unitPrice / 1000;
                                }
                                else {
                                    entity.TotalImportPrice += (detail.RealNumber) * unitPrice;
                                }
                            }
                        }
                        foreach (var export in platingForm.ExportGCN_NCU.Where(i => i.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved)) {
                            foreach (var detail in export.ExportGCN_NCUDetail) {
                                var unit = detail.PlatingFormDetail.Unit;
                                var unitPrice = detail.PlatingFormDetail.UnitPrice;
                                if (unit.Contains("Kg")) {
                                    entity.TotalExportPrice += (detail.Weight ?? 0) * unitPrice / 1000;
                                }
                                else {
                                    entity.TotalExportPrice += (detail.RealNumber) * unitPrice;
                                }
                            }
                        }

                        if (entity.CurrencyCode.Equals("USD")) {
                            entity.TotalPriceString = string.Format("{0:n2}", entity.TotalPrice);
                        }
                        else
                            entity.TotalPriceString = string.Format("{0:n0}", entity.TotalPrice);
                        model.Add(entity);
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetPlatingFormUnfinish", "\n" + ex.Message);
            }
            return model;
        }
        [HttpPost]
        [GridAction]
        public ActionResult UpdatePlatingFormStatus(PlatingFormModel updated) {
            try {
                using (var vfi = new tammaContext()) {
                    var form = vfi.PlatingForms.FirstOrDefault(o => o.FormId == updated.FormId);
                    if (form == null)
                        throw new AggregateException("Lỗi phiếu !");
                    byte status = 0;
                    try {
                        status = Convert.ToByte(updated.StatusName);
                    }
                    catch (FormatException) {

                    }
                    form.Status = status;
                    vfi.SaveChanges();

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdatePlatingFormStatus", "" + ex.Message);
            }
            return View(new GridModel(GetPlatingFormUnfinish().OrderBy(m => m.CreateDate)));
        }

        public ActionResult SelectComboBoxPlatingStatus() {
            var val = from MyUtilities.Sales.Status stt in Enum.GetValues(typeof(MyUtilities.Sales.Status))
                      select new {
                          Value = (int)Enum.Parse(typeof(MyUtilities.Sales.Status), stt.ToString()),
                          Text =
                      MyUtilities.PurchaseOrder.GetPlatingStatusText(
                          (int)Enum.Parse(typeof(MyUtilities.Sales.Status), stt.ToString()))
                      };

            return new JsonResult {
                Data = new SelectList(val, "Value", "Text")
            };
        }

        public ActionResult SelectComboBoxPlatingType() {
            var model = new List<WarehouseModel>();
            using (var vfi = new tammaContext()) {
                var platingWarehouses = vfi.Warehouses.Where(w => w.WarehouseTypeId == 1 && w.Active);
                foreach (var warehouse in platingWarehouses) {
                    var entity = new WarehouseModel {
                        WarehouseId = warehouse.WarehouseId,
                        WarehouseName = warehouse.WarehouseName,
                    };
                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "WarehouseId", "WarehouseName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [GridAction]
        public ActionResult SelectManagePlatingForm(byte status, string fromDate, string toDate) {
            var model = new List<PlatingFormModel>();
            try {
                model = GetAllPlating(status, fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectManagePlatingForm", "\n" + ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CreateDate)));
        }

        List<PlatingFormModel> GetAllPlating(byte status, string fromDate, string toDate) {
            var model = new List<PlatingFormModel>();
            var ci = new CultureInfo("vi-VN");
            var fdate = string.IsNullOrWhiteSpace(fromDate)
                            ? DateTime.Today
                            : Convert.ToDateTime(fromDate, ci);
            var tdate = string.IsNullOrWhiteSpace(toDate)
                            ? DateTime.Today
                            : Convert.ToDateTime(toDate, ci);
            tdate = tdate.AddDays(1).AddSeconds(-1);
            using (var vfi = new tammaContext()) {

                var purchasing = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.PurchasingManagement);
                var forms = from pf in vfi.PlatingForms
                            //where pf.CreateDate.Month == month && pf.CreateDate.Year == year &&
                            where pf.CreateDate >= fdate && pf.CreateDate <= tdate &&
                                  pf.Status == status
                            select pf;
                var platingWarehouses = vfi.Warehouses.Where(w => w.WarehouseTypeId == 1);
                foreach (var platingForm in forms) {
                    var entity = new PlatingFormModel {
                        CreateDate = platingForm.CreateDate,
                        CreateUser = platingForm.CreateUser,
                        CurrencyCode = platingForm.CurrencyCode,
                        ExchangeRate = platingForm.ExchangeRate,
                        Note = platingForm.Note,
                        PlatingFormNumber = platingForm.PlatingFormNumber,
                        StatusName = MyUtilities.PurchaseOrder.GetPlatingStatusText(platingForm.Status),
                        VendorCode = platingForm.Vendor.VendorCode,
                        VendorName = platingForm.Vendor.VendorName,
                        Status = platingForm.Status,
                        VendorId = platingForm.VendorId,
                        FormId = platingForm.FormId,
                        PlatingType = platingForm.PlatingType,
                        IsPurchasingManager = purchasing
                    };
                    entity.PlatingTypeName =
                        platingWarehouses.FirstOrDefault(pw => pw.WarehouseId == entity.PlatingType).WarehouseName;
                    entity.TotalQuantityRequirement = platingForm.PlatingFormDetails.Sum(pfd => pfd.QuantityRequirement);
                    entity.TotalPrice =
                        platingForm.PlatingFormDetails.Sum(pfd => pfd.QuantityRequirement * pfd.UnitPrice);

                    foreach (var import in platingForm.ImportNCU_QCB.Where(i => i.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved)) {
                        foreach (var detail in import.ImportNCU_QCBDetail) {
                            var unit = detail.ExportGCN_NCUDetail.PlatingFormDetail.Unit;
                            var unitPrice = detail.ExportGCN_NCUDetail.PlatingFormDetail.UnitPrice;
                            if (unit.Contains("Kg")) {
                                entity.TotalImportPrice += (detail.Weight) * unitPrice / 1000;
                            }
                            else {
                                entity.TotalImportPrice += (detail.RealNumber) * unitPrice;
                            }
                        }
                    }
                    foreach (var export in platingForm.ExportGCN_NCU.Where(i => i.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved)) {
                        foreach (var detail in export.ExportGCN_NCUDetail) {
                            var unit = detail.PlatingFormDetail.Unit;
                            var unitPrice = detail.PlatingFormDetail.UnitPrice;
                            if (unit.Contains("Kg")) {
                                entity.TotalExportPrice += (detail.Weight ?? 0) * unitPrice / 1000;
                            }
                            else {
                                entity.TotalExportPrice += (detail.RealNumber) * unitPrice;
                            }
                        }
                    }

                    if (entity.CurrencyCode.Equals("USD")) {
                        entity.TotalPriceString = string.Format("{0:n2}", entity.TotalPrice);
                    }
                    else
                        entity.TotalPriceString = string.Format("{0:n0}", entity.TotalPrice);
                    model.Add(entity);
                }

            }
            return model;
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdatePlatingManage(PlatingFormModel update) {
            if (!Request.IsAuthenticated)
                return Json(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            try {
                using (var vfi = new tammaContext()) {
                    var platingForm = vfi.PlatingForms.FirstOrDefault(pf => pf.FormId == update.FormId);
                    if (platingForm == null)
                        return View(new GridModel());
                    return
                        View(
                            new GridModel(
                                GetAllPlating(platingForm.Status,
                                platingForm.CreateDate.ToString("dd/MM/yyyy"),
                                              platingForm.CreateDate.ToString("dd/MM/yyyy"))));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ApproveOrder", ex.Message);
            }
            return View(new GridModel());
        }
        public ActionResult SelectPlatingFormInfo(int formId) {
            using (var vfi = new tammaContext()) {
                var platingForm = vfi.PlatingForms.FirstOrDefault(pf => pf.FormId == formId);
                if (platingForm == null)
                    return Json("9");
                var entity = new PlatingFormModel {
                    FormId = formId,
                    VendorCode = platingForm.Vendor.VendorCode,
                    VendorName = platingForm.Vendor.VendorName,
                };
                return Json(entity);
            }
        }

        public ActionResult SelectPlatingFormForExport() {
            var model = new List<PlatingFormModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var forms = (from pf in vfi.PlatingForms
                                 orderby pf.PlatingFormNumber
                                 where pf.Status == (byte)MyUtilities.Sales.Status.InProcess
                                 select pf).ToList();
                    foreach (var form in forms) {
                        var entity = new PlatingFormModel {
                            FormId = form.FormId,
                            PlatingFormNumber = form.PlatingFormNumber + " - " + form.Vendor.VendorName
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(model, "FormId", "PlatingFormNumber")
            };
        }

        public ActionResult SelectPlatingFormForImport() {
            var model = new List<PlatingFormModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var forms = (from pf in vfi.PlatingForms
                                 orderby pf.PlatingFormNumber
                                 where pf.Status == (byte)MyUtilities.Sales.Status.InProcess &&
                                       pf.ExportGCN_NCU.Any(
                                           e => e.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved)
                                 select pf).ToList();
                    foreach (var form in forms) {
                        var entity = new PlatingFormModel {
                            FormId = form.FormId,
                            PlatingFormNumber = form.PlatingFormNumber + " - " + form.Vendor.VendorName
                        };
                        var insert = false;
                        if (form.ImportNCU_QCB.Any()) {
                            foreach (var detail in form.PlatingFormDetails) {
                                if (detail.Unit.Contains("Kg")) {

                                    var exportQuantity =
                                        form.ExportGCN_NCU.Where(
                                            e => e.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved)
                                            .Sum(
                                                e => e.ExportGCN_NCUDetail.Where(ed => ed.ProductId == detail.ProductId)
                                                      .Sum(ed => ed.Weight));
                                    var importQuantiy = form.ImportNCU_QCB.Where(
                                        e => e.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved).Sum(
                                            e => e.ImportNCU_QCBDetail.Where(ed => ed.ProductId == detail.ProductId)
                                                  .Sum(ed => ed.Weight));
                                    if (Math.Round((exportQuantity - importQuantiy).Value, 0) > 0) {
                                        insert = true;
                                        break;
                                    }
                                }
                                else {
                                    var exportQuantity =
                                        form.ExportGCN_NCU.Where(
                                            e => e.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved)
                                            .Sum(
                                                e => e.ExportGCN_NCUDetail.Where(ed => ed.ProductId == detail.ProductId)
                                                      .Sum(ed => ed.RealNumber));
                                    var importQuantiy = form.ImportNCU_QCB.Where(
                                        e => e.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved).Sum(
                                            e => e.ImportNCU_QCBDetail.Where(ed => ed.ProductId == detail.ProductId)
                                                  .Sum(ed => ed.RealNumber));
                                    if (Math.Round(exportQuantity - importQuantiy, 0) > 0) {
                                        insert = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else {
                            insert = true;
                        }
                        if (insert)
                            model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(model, "FormId", "PlatingFormNumber")
            };
        }

        [HttpPost]
        public ActionResult GetPlatingInfo(int platingId) {
            try {

                using (var vfi = new tammaContext()) {
                    var plating = vfi.ProductionPlatings.FirstOrDefault(pp => pp.PlatingId == platingId && pp.Active);
                    if (plating == null)
                        return Json("9");
                    var lastExport = (from pd in vfi.PlatingFormDetails
                                      orderby pd.PlatingForm.CreateDate descending
                                      where pd.PlatingForm.Status != (byte)MyUtilities.Transaction.Status.Cancel
                                            && pd.PlatingForm.Status != (byte)MyUtilities.Transaction.Status.Open
                                            && pd.ProductId == plating.ProductId
                                            && pd.PlatingCode.Equals(plating.PlatingName)
                                            && pd.UnitPrice != 0
                                      select pd).FirstOrDefault();
                    var detail = new PlatingDetailModel();
                    if (lastExport != null) {
                        detail.Thickness = lastExport.Thickness;
                        detail.SaltSprayTime = lastExport.SaltSprayTime;
                        detail.SpecialRequest = lastExport.SpecialRequest;
                        detail.Sample = lastExport.Sample;
                        detail.TestingEquipment = lastExport.TestingEquipment;
                        detail.Unit = lastExport.Unit;
                        detail.UnitPrice = lastExport.UnitPrice;
                    }
                    return
                        Json(new object[]
                            {
                                "","",
                                detail.Thickness,
                                detail.SaltSprayTime,
                                detail.SpecialRequest,
                                detail.Sample,
                                detail.TestingEquipment,
                                detail.Unit,
                                string.Format("{0:n2}", detail.UnitPrice),
                            });
                }
            }
            catch (Exception ex) {
                return Json("0");
            }
            return Json("0");
        }


        [HttpPost]
        public ActionResult GetPlatingInfoById(int vendorId) {
            try {

                using (var vfi = new tammaContext()) {
                    var lastExport = (from pd in vfi.PlatingForms
                                      orderby pd.CreateDate descending
                                      where pd.Status != (byte)MyUtilities.Transaction.Status.Cancel
                                            && pd.Status != (byte)MyUtilities.Transaction.Status.Open
                                            && pd.VendorId == vendorId
                                      select pd).FirstOrDefault();
                    var detail = new PlatingFormModel {
                        ExchangeRate = 1,
                    };
                    if (lastExport != null) {
                        detail.PlatingType = lastExport.PlatingType;
                        var warehouse = vfi.Warehouses.FirstOrDefault(w => w.WarehouseId == detail.PlatingType);
                        if (warehouse != null)
                            detail.PlatingTypeName = warehouse.WarehouseName;
                        detail.ExchangeRate = lastExport.ExchangeRate;
                        detail.CurrencyCode = lastExport.CurrencyCode;
                        var currency = vfi.Currencies.FirstOrDefault(c => c.CurrencyCode.Equals(detail.CurrencyCode));
                        if (currency != null)
                            detail.CurrencyCodeName = currency.CurrencyCode + " -- " + currency.CurrencyName;
                    }
                    return Json(detail);
                }
            }
            catch (Exception ex) {
                return Json("0");
            }
            return Json("0");
        }

        [HttpPost]
        public ActionResult PrintPlatingForm(int exportId) {
            var model = new List<PlatingDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var export = vfi.ExportGCN_NCU.FirstOrDefault(e => e.ExportId == exportId);
                    if (export == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu xuất kho");
                    var count =
                        vfi.ExportGCN_NCU.Count(
                            e =>
                            e.PlatingForm.VendorId == export.PlatingForm.VendorId &&
                            e.ExportDate.Value.Month == export.ExportDate.Value.Month &&
                            e.ExportDate.Value.Year == export.ExportDate.Value.Year);
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
                    foreach (var detail in export.ExportGCN_NCUDetail) {
                        var entity = model.FirstOrDefault(m => m.PlatingDetailId == detail.PlatingDetailId);
                        if (entity == null) {
                            entity = new PlatingDetailModel {
                                PlatingDetailId = detail.PlatingDetailId ?? 0,
                                DetailId = detail.DetailId,
                                ProductId = detail.ProductId,
                                Note = detail.Note,
                                Package = detail.Package,
                                Thickness = detail.PlatingFormDetail.Thickness,
                                SaltSprayTime = detail.PlatingFormDetail.SaltSprayTime,
                                SpecialRequest = detail.PlatingFormDetail.SpecialRequest,
                                Sample = detail.PlatingFormDetail.Sample,
                                TestingEquipment = detail.PlatingFormDetail.TestingEquipment,
                                Unit = detail.PlatingFormDetail.Unit,
                                ProductCode = detail.Product.ProductCode,
                                PlatingCode = detail.PlatingFormDetail.PlatingCode,
                                ImportDateRequirement = detail.PlatingFormDetail.ImportDateRequirement,
                                VendorName = export.PlatingForm.Vendor.VendorName,
                                VendorCode = export.PlatingForm.Vendor.VendorCode,
                                VendorAddress = export.PlatingForm.Vendor.Address,
                                VendorContact = export.PlatingForm.Vendor.ContactName,
                                VendorFax = export.PlatingForm.Vendor.Fax,
                                VendorPhone = export.PlatingForm.Vendor.Phone,
                                ExportDate = export.ExportDate ?? DateTime.Now,
                                FormNumber = export.PlatingForm.PlatingFormNumber,
                                TransactionCode = export.TransactionCode,
                                Info = info
                            };
                            if (entity.Unit.Contains("Kg")) {
                                entity.Export = detail.Weight.Value / 1000;
                                entity.ExportString = string.Format("{0:n3}", entity.Export);
                                var str = entity.ExportString;
                                int i = entity.ExportString.Length - 1;
                                while (i > -1) {
                                    if (!str[i].Equals('0')) break;
                                    i--;
                                }
                                if (i != entity.ExportString.Length - 1)
                                    entity.ExportString =
                                        str.Remove(entity.ExportString.Length - i == 4 ? i : i + 1);
                                entity.NumberKg = Math.Round(entity.Export, 3);
                            }
                            else {
                                entity.Export = detail.RealNumber;
                                entity.ExportString = string.Format("{0:n0}", entity.Export);
                                entity.Number = entity.Export;
                            }
                            if (count < 99)
                                entity.AutoNumber = string.Format("{0:00}", count);
                            else
                                entity.AutoNumber = count + "";
                            entity.AutoNumber += "-" + DateTime.Now.ToString("MM") + "/"
                                                 + entity.VendorCode + "-" + DateTime.Now.ToString("yyyy");
                            model.Add(entity);
                        }
                        else {
                            if (entity.Unit.Contains("Kg")) {
                                entity.Export += detail.Weight.Value / 1000;
                                entity.ExportString = string.Format("{0:n3}", entity.Export);
                                var str = entity.ExportString;
                                int i = entity.ExportString.Length - 1;
                                while (i > -1) {
                                    if (!str[i].Equals('0')) break;
                                    i--;
                                }
                                if (i != entity.ExportString.Length - 1)
                                    entity.ExportString =
                                        str.Remove(entity.ExportString.Length - i == 4 ? i : i + 1);
                                entity.NumberKg = Math.Round(entity.Export, 3);
                            }
                            else {
                                entity.Export += detail.RealNumber;
                                entity.ExportString = string.Format("{0:n0}", entity.Export);
                                entity.Number = entity.Export;
                            }
                            //if (count < 99)
                            //    entity.AutoNumber = string.Format("{0:00}", count);
                            //else
                            //    entity.AutoNumber = count + "";
                            //entity.AutoNumber += "-" + DateTime.Now.ToString("MM") + "/"
                            //                     + entity.VendorCode + "-" + DateTime.Now.ToString("yyyy");
                        }
                    }

                    return PartialView("PagePrinPlatingForm", model);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PagePrinPlatingForm", ex.Message);

            }
            return PartialView(null);
        }

        [HttpPost]
        public ActionResult PrintPlatingPriceReport(int vendorId, string fromDate, string toDate) {
            var model = new List<PlatingDetailModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fdate = string.IsNullOrWhiteSpace(fromDate)
                                ? DateTime.Today
                                : Convert.ToDateTime(fromDate, ci);
                var tdate = string.IsNullOrWhiteSpace(toDate)
                                ? DateTime.Today
                                : Convert.ToDateTime(toDate, ci);
                using (var vfi = new tammaContext()) {
                    var vendor = vfi.Vendors.FirstOrDefault(v => v.VendorId == vendorId);
                    if (vendor == null)
                        throw new AggregateException("Lỗi! Không tìm thấy nhà cung cấp!");
                    var importPlatings = (from id in vfi.ImportNCU_QCBDetail
                                         where
                                             id.ImportNCU_QCB.Transaction.Status ==
                                             (byte)MyUtilities.Transaction.Status.Approved
                                             //&& id.ImportNCU_QCB.ImportDate.Value.Month == month
                                             //&& id.ImportNCU_QCB.ImportDate.Value.Year == year
                                             && id.ImportNCU_QCB.ImportDate >= fdate
                                             && id.ImportNCU_QCB.ImportDate <= tdate
                                             && id.ImportNCU_QCB.PlatingForm.VendorId == vendorId
                                             &&
                                             id.ExportGCN_NCUDetail.PlatingFormDetail.PlatingForm.Status !=
                                             (byte)MyUtilities.Sales.Status.Completed
                                         select new {
                                             id.ExportGCN_NCUDetail.PlatingDetailId,
                                             id.Note,
                                             id.Package,
                                             id.ImportNCU_QCB.TransactionCode,
                                             id.ProductId,
                                             id.Product.ProductCode,
                                             id.ExportGCN_NCUDetail.PlatingFormDetail.Unit,
                                             id.ExportGCN_NCUDetail.PlatingFormDetail.PlatingCode,
                                             ExportDate =
                                         id.ExportGCN_NCUDetail.ExportGCN_NCU.ExportDate ?? DateTime.Now,
                                             id.ExportGCN_NCUDetail.PlatingFormDetail.UnitPrice,
                                             ImportDate = id.ImportNCU_QCB.ImportDate,
                                             Weight = id.Weight,
                                             id.RealNumber,
                                         }).ToList();
                    var index = 1;

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
                    foreach (var detail in importPlatings) {
                        var entity = model.FirstOrDefault(m => m.ProductId == detail.ProductId &&
                            m.TransactionCode == detail.TransactionCode &&
                            m.PlatingDetailId == detail.PlatingDetailId);
                        if (entity == null) {
                            entity = new PlatingDetailModel {
                                Index = index,
                                Note = detail.Note,
                                Package = detail.Package,
                                Unit = detail.Unit,
                                ProductId = detail.ProductId,
                                ProductCode = detail.ProductCode,
                                PlatingCode = detail.PlatingCode,
                                VendorName = vendor.VendorName,
                                VendorCode = vendor.VendorCode,
                                VendorAddress = vendor.Address,
                                VendorContact = vendor.ContactName,
                                VendorFax = vendor.Fax,
                                VendorPhone = vendor.Phone,
                                ExportDate = detail.ExportDate,
                                ImportDate = detail.ImportDate,
                                TransactionCode = detail.TransactionCode,
                                UnitPrice = detail.UnitPrice,
                                PlatingDetailId = detail.PlatingDetailId ?? 0,
                                Info = info,
                            };
                            if (entity.Unit.Contains("Kg")) {
                                entity.Import = detail.Weight / 1000;
                                entity.ImportString = string.Format("{0:n3}", entity.Import);
                                var str = entity.ImportString;
                                int i = entity.ImportString.Length - 1;
                                while (i > -1) {
                                    if (!str[i].Equals('0')) break;
                                    i--;
                                }
                                if (i != entity.ImportString.Length - 1)
                                    entity.ImportString =
                                        str.Remove(entity.ImportString.Length - i == 4 ? i : i + 1);
                                entity.NumberKg = Math.Round(entity.Import, 3);
                                entity.TotalPrice = entity.UnitPrice * entity.NumberKg;
                            }
                            else {
                                entity.Import = detail.RealNumber;
                                entity.ImportString = string.Format("{0:n0}", entity.Import);
                                entity.Number = entity.Import;
                                entity.TotalPrice = entity.UnitPrice * entity.Number;
                            }
                            model.Add(entity);
                        }
                        else {
                            if (entity.Unit.Contains("Kg")) {
                                entity.Import += (detail.Weight / 1000);
                                entity.ImportString = string.Format("{0:n3}", entity.Import);
                                var str = entity.ImportString;
                                int i = entity.ImportString.Length - 1;
                                while (i > -1) {
                                    if (!str[i].Equals('0')) break;
                                    i--;
                                }
                                if (i != entity.ImportString.Length - 1)
                                    entity.ImportString =
                                        str.Remove(entity.ImportString.Length - i == 4 ? i : i + 1);
                                entity.NumberKg = Math.Round(entity.Import, 3);
                                entity.TotalPrice = entity.UnitPrice * entity.NumberKg;
                            }
                            else {
                                entity.Import += detail.RealNumber;
                                entity.ImportString = string.Format("{0:n0}", entity.Import);
                                entity.Number = entity.Import;
                                entity.TotalPrice = entity.UnitPrice * entity.Number;
                            }
                        }
                    }

                    return PartialView("PagePrinPlatingPriceForm",
                                       model.OrderBy(m => m.ImportDate).ThenBy(m => m.ProductCode).ToList());
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PagePrinPlatingPriceForm", "\n" + ex.Message);

            }
            return PartialView(null);
        }
        #endregion

        #region Tool


        [GridAction]
        public ActionResult UpdateToolPurchaseOrderDetail(
            [Bind(Prefix = "inserted")] IEnumerable<PurchaseOrderDetailModel> insertedDetails,
            [Bind(Prefix = "updated")] IEnumerable<PurchaseOrderDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")] IEnumerable<PurchaseOrderDetailModel> deletedDetails,
            //int materialClassifiedId, int materialTypeId, 
            string shipDate,
            int vendorId, int shipMethodId, int deliveryMethodId,
            int packagedMethodId, int paymentMethodId, string employeeName,
            int tolerance, string currencyCode, string note
            ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateToolPurchaseOrderDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. " +
                                         @"\r\n 1 trong các nguyên nhân như mất thời gian chờ. " +
                                         @"\r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<PurchaseOrderDetailModel>()));
            }
            try {
                if (insertedDetails != null) {
                    using (var vfi = new tammaContext()) {
                        var purchaseOrder = new PurchaseOrder {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            EmployeeName = employeeName,
                            VendorId = vendorId,
                            ShipDate = null,
                            OrderDate = DateTime.Now,
                            RevisionNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.PurchaseOrder, 1),
                            Active = false,
                            Status = (byte)MyUtilities.Sales.Status.Waiting,
                            Tolerance = tolerance,
                            Note = note,
                            CurrencyCode = currencyCode,
                            MaterialClassifiedId = 3
                        };
                        foreach (var detail in insertedDetails) {
                            if (detail.OrderQty > 0) {
                                var tool = vfi.Tools.FirstOrDefault(m => m.ToolId == detail.ToolId);
                                if (tool == null)
                                    throw new AggregateException("Công cụ lỗi " + detail.ToolCode);
                                var poDetail = new PurchaseOrderDetail {
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    PurchaseOrder = purchaseOrder,
                                    PurchaseOrderId = purchaseOrder.PurchaseOrderId,

                                    //ToolId = tool.ToolId,
                                    OrderQty = detail.OrderQty,
                                    UnitPrice = detail.UnitPrice,
                                    DueDate = detail.DueDate,
                                    ReceivedQty = 0,
                                    RejectedQty = 0,
                                    Active = true,
                                    Unit = detail.Unit,
                                    IsComplete = false
                                };
                                purchaseOrder.PurchaseOrderDetails.Add(poDetail);
                            }
                        }
                        if (purchaseOrder.PurchaseOrderDetails.Any()) {
                            vfi.PurchaseOrders.Add(purchaseOrder);
                            vfi.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateToolPO",
                                         ex.Message);
            }
            return View(new GridModel(new List<PurchaseOrderDetailModel>()));
        }

        #endregion

        #region product


        [GridAction]
        public ActionResult SelectProductInPo(string ids, int poId) {
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
                    var poDetails = (from pod in vfi.PurchaseOrderDetails
                                     where checkedRecords.Contains(pod.PurchaseOrderDetailId)
                                           && !pod.IsComplete.Value
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
                    if (!poDetails.Any())
                        throw new AggregateException("Lỗi! Các mục chọn đã hoàn thành");
                    var productIds = poDetails.Select(pod => pod.ReferenceId).ToList();
                    var products = from f in vfi.Products
                                   where productIds.Contains(f.ProductId)
                                   select f;
                    foreach (var id in checkedRecords) {
                        var poDetail =
                            poDetails.FirstOrDefault(
                                pod => pod.PurchaseOrderDetailId == id);
                        var product = products.FirstOrDefault(m => m.ProductId == poDetail.ReferenceId);
                        var entity = new TransactionFptDetailModel() {
                            PoDetailId = id,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            RequiredQuantity = poDetail.OrderQty - poDetail.ReceivedQty,
                            VendorName = poDetail.Vendor.ShortName,
                            UnitMeasure = poDetail.Unit,
                            UnitPrice = poDetail.UnitPrice,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductInPo", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.ProductCode)));
        }

        [GridAction]
        public ActionResult UpdateImportProductPurchase(
            [Bind(Prefix = "inserted")]IEnumerable<TransactionFptDetailModel> insertedDetails,
            [Bind(Prefix = "updated")]IEnumerable<TransactionFptDetailModel> updatedDetails,
            [Bind(Prefix = "deleted")]IEnumerable<TransactionFptDetailModel> deletedDetails,
            string monthlyDate, long? poId,
            string exportOrImport, int exchangeRate,
            int warehouseId
            ) {
            if (poId == null || poId == 0) throw new AggregateException("Lỗi phiếu nhập!");
            if (updatedDetails != null) {
                try {
                    if (!Request.IsAuthenticated)
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");

                    var modifiedUser = HttpContext.User.Identity.Name;
                    if (string.IsNullOrWhiteSpace(modifiedUser))
                        throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (user null). ");
                    ModelState.Clear();
                    var ci = new CultureInfo("vi-VN");
                    var createdDate = string.IsNullOrWhiteSpace(monthlyDate)
                                          ? DateTime.Today
                                          : Convert.ToDateTime(monthlyDate, ci);
                    using (var vfi = new tammaContext()) {
                        var purchaseOrder = vfi.PurchaseOrders.FirstOrDefault(po => po.PurchaseOrderId == poId);
                        if (purchaseOrder == null) throw new AggregateException("Lỗi phiếu nhập!");
                        var transaction = new Vfi.Models.Transaction {
                            TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Product, 1),
                            EoI = exportOrImport,
                            MoP = false,
                            CreatedUser = HttpContext.User.Identity.Name,
                            CreatedDate = createdDate,
                            Status = (byte)MyUtilities.Transaction.Status.Open,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            WarehouseIssueId = null,
                            WarehouseReceiptId = warehouseId,
                            PoId = poId
                        };
                        var importPO = new ImportPurchaseOrder {
                            ImportDate = createdDate,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            PurchaseOrderId = poId,
                            TransactionId = transaction.TransactionId,
                            Transaction = transaction,
                            ExchangeRate = exchangeRate,
                            PurchasingSignature = 0,
                        };
                        var weeklyLot = MyUtilities.MySystem.LotNumber_Weekly(createdDate);
                        foreach (var detail in updatedDetails) {
                            detail.Quantity = Math.Round(detail.Quantity);
                            if (detail.Quantity == 0) continue;
                            //var poDetail =
                            //    purchaseOrder.PurchaseOrderDetails.FirstOrDefault(
                            //        pod => pod.PurchaseOrderDetailId == detail.PurchaseOrderDetailId);
                            //var lastTotal = (poDetail.OrderQty*purchaseOrder.Tolerance/100) + poDetail.OrderQty -
                            //                poDetail.ReceivedQty - poDetail.RejectedQty;
                            //if (lastTotal - detail.QuantityKg < 0)
                            //    throw new AggregateException("Số lượng không được vượt quá cho phép " +
                            //                                 detail.MaterialCode + ": " + lastTotal + "(kg)");
                            var transactionDetail = new Vfi.Models.TransactionDetail {
                                ReferenceId = detail.ProductId,
                                MoP = false,
                                Quantity = detail.Quantity,
                                Price = detail.UnitPrice * importPO.ExchangeRate,
                                UnitMeasure = detail.UnitMeasure.Trim(),
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                StoreCode = detail.StoreCode,
                                Note = detail.Note,
                                PoDetailId = detail.PoDetailId,
                                VendorId = purchaseOrder.VendorId,
                                LotNumber = weeklyLot + purchaseOrder.Vendor.VendorCode,
                            };
                            transaction.TransactionDetails.Add(transactionDetail);

                            var importDetail = new ImportPurchaseOrderDetail {
                                ImportId = importPO.ImportId,
                                ImportPurchaseOrder = importPO,
                                Quantity = detail.Quantity,
                                LotNumber = transactionDetail.LotNumber,
                                UnitPrice = detail.UnitPrice * importPO.ExchangeRate,
                                //UnitWeight = detail.UnitWeight,
                                Note = detail.Note,
                                VendorId = purchaseOrder.VendorId,
                                StoreCode = detail.StoreCode,
                                ProductId = detail.ProductId,
                            };
                            importPO.ImportPurchaseOrderDetails.Add(importDetail);
                        }
                        if (importPO.ImportPurchaseOrderDetails.Count != 0 &&
                            transaction.TransactionDetails.Count != 0) {
                            vfi.Transactions.Add(transaction);
                            vfi.ImportPurchaseOrders.Add(importPO);
                            vfi.SaveChanges();
                        }
                    }
                }
                catch (Exception exception) {
                    ModelState.AddModelError("UpdateImportProductPurchase", "" + exception.Message);
                }
            }

            Session["ListImportTransactionMaterialIds"] = null;

            return View(new GridModel(new List<MaterialInventoryModel>()));
        }


        #endregion

        #region po tax invioce

        [GridAction]
        public ActionResult UpdatePoTaxInvoice(
            [Bind(Prefix = "inserted")] IEnumerable<PurchaseOrderDetailModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<PurchaseOrderDetailModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<PurchaseOrderDetailModel> deleteds,
            int classified, int vendorId,
            string taxInvoiceDate, string taxInvoiceCode, string currencyCode,
            int tax, int exchangeRate
            ) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException(@"Vui lòng đăng nhập hệ thống. (IsAuthenticated). ");
            }

            var ci = new CultureInfo("vi-VN");
            var date = string.IsNullOrWhiteSpace(taxInvoiceDate)
                                   ? DateTime.Now
                                   : Convert.ToDateTime(taxInvoiceDate, ci);
            try {
                using (var vfi = new tammaContext()) {

                    if (string.IsNullOrWhiteSpace(taxInvoiceCode))
                        throw new AggregateException("Lỗi! chưa nhập số hóa đơn!");
                    var taxInvoice =
                        vfi.PoTaxInvoices.FirstOrDefault(
                            ti =>
                            ti.TaxInvoiceNumber.Equals(taxInvoiceCode) && ti.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                            ti.VendorId == vendorId);
                    if (taxInvoice != null)
                        throw new AggregateException("Lỗi! Hóa đơn đã tồn tại");
                    taxInvoice = new PoTaxInvoice {
                        TaxPercent = tax,
                        ExchangeRate = exchangeRate,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        CurrencyCode = currencyCode,
                        ClassifiedId = classified,
                        Note = "",
                        Status = (byte)MyUtilities.Sales.Status.Waiting,
                        TaxInvoiceNumber = taxInvoiceCode,
                        VendorId = vendorId,
                        PoDate = date,
                    };
                    var list = new List<PoTaxInvoiceReference>();
                    foreach (var inserted in inserteds) {
                        var taxInvoiceProduct = new PoTaxInvoiceReference {
                            ReferenceId = inserted.MaterialId,
                            UnitPrice = inserted.UnitPrice,
                            Quantity = inserted.Quantity,
                            TaxInvoiceId = taxInvoice.TaxInvoiceId,
                            Unit = inserted.Unit,
                            ClassifiedId = classified,
                            PoTaxInvoice = taxInvoice
                        };
                        list.Add(taxInvoiceProduct);
                    }
                    vfi.PoTaxInvoices.Add(taxInvoice);
                    vfi.PoTaxInvoiceReferences.AddRange(list);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdatePoTaxInvoice", ex.Message);
            }

            return View(new GridModel(new List<PurchaseOrderDetailModel>()));
        }


        [GridAction]
        public ActionResult SelectTaxInvoiceReferenceById(int taxInvoiceId) {
            var model = new List<PoTaxInvoiceReferenceModel>();
            try {
                model = TaxInvoiceReferenceById(taxInvoiceId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTaxInvoiceReferenceById", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<PoTaxInvoiceReferenceModel> TaxInvoiceReferenceById(int taxInvoiceId) {
            var model = new List<PoTaxInvoiceReferenceModel>();
            if (taxInvoiceId == 0)
                return model;
            try {
                using (var vfi = new tammaContext()) {
                    var taxInvoice = vfi.PoTaxInvoices.FirstOrDefault(ti => ti.TaxInvoiceId == taxInvoiceId);

                    foreach (var detail in taxInvoice.PoTaxInvoiceReferences) {
                        var entity = new PoTaxInvoiceReferenceModel {
                            DetailId = detail.DetailId,
                            //Code = detail.Product.ProductCode,
                            ReferenceId = detail.ReferenceId,
                            Quantity = detail.Quantity,
                            UnitPrice = detail.UnitPrice,
                            CurrencyCode = taxInvoice.CurrencyCode,
                            TaxPercent = taxInvoice.TaxPercent,
                            ExchangeRate = taxInvoice.ExchangeRate,
                            Unit = detail.Unit,
                        };
                        if (detail.PoTaxInvoiceReferenceDetails.Any())
                            entity.Note = "Đã phân";
                        try {

                            switch (taxInvoice.ClassifiedId) {
                                case 1:
                                    var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == detail.ReferenceId);
                                    entity.Code = material.MaterialCode;
                                    break;
                                case 2:
                                    var fuel = vfi.Fuels.FirstOrDefault(f => f.FuelId == detail.ReferenceId);
                                    entity.Code = fuel.FuelFullCode;
                                    break;
                                case 3:
                                    var tool = vfi.Tools.FirstOrDefault(t => t.ToolId == detail.ReferenceId);
                                    entity.Code = tool.ToolFullCode;
                                    break;
                                case 4:
                                case 6:
                                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == detail.ReferenceId);
                                    entity.Code = product.ProductCode;
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (ArgumentNullException) {
                            throw new AggregateException("Lỗi! Không tìm thấy mã");
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductInTaxInvoice", ex.Message);
            }
            return model.OrderBy(m => m.Code)
                        .ToList();
        }

        [GridAction]
        public ActionResult SelectTaxInvoiceReferenceDetailById(int referenceId) {
            if (referenceId == 0) {
                return View(new GridModel(new List<PoTaxInvoiceReferenceDetailModel>()));
            }
            var startDate = MyUtilities.PurchaseOrder.StartTaxInvoiceDate;
            // material
            var model = new List<PoTaxInvoiceReferenceDetailModel>();
            try {

                using (var vfi = new tammaContext()) {
                    var taxInvoiceReference = vfi.PoTaxInvoiceReferences.FirstOrDefault(ed => ed.DetailId == referenceId);
                    if (taxInvoiceReference == null || taxInvoiceReference.PoTaxInvoiceReferenceDetails.Any())
                        return View(new GridModel(new List<PoTaxInvoiceReferenceDetailModel>()));
                    switch (taxInvoiceReference.PoTaxInvoice.ClassifiedId) {
                        // nguyen lieu
                        case 1:
                            var importMaterialDetails =
                                vfi.ImportPurchaseOrderDetails.Where(
                                    i =>
                                    i.ImportPurchaseOrder.PurchaseOrderId != null &&
                                    i.ImportPurchaseOrder.Transaction.Status ==
                                    (byte)MyUtilities.Transaction.Status.Approved &&
                                    i.PoReferenceDetailId == null &&
                                    i.MaterialId == taxInvoiceReference.ReferenceId
                                    && i.ImportPurchaseOrder.ImportDate >= startDate
                                    );
                            foreach (var importDetail in importMaterialDetails) {
                                var purchaseDetail =
                                    vfi.PurchaseOrderDetails.FirstOrDefault(
                                        pod => pod.PurchaseOrderId == importDetail.ImportPurchaseOrder.PurchaseOrderId &&
                                        pod.ReferenceId == importDetail.MaterialId);
                                if (purchaseDetail.UnitPrice != taxInvoiceReference.UnitPrice)
                                    continue;
                                var entity = new PoTaxInvoiceReferenceDetailModel {
                                    Code = importDetail.Material.MaterialCode,
                                    ImportDetailId = importDetail.ImportDetailId,
                                    ImportId = importDetail.ImportId,
                                    ImportCode = importDetail.ImportPurchaseOrder.Transaction.TransactionCode,
                                    ImportDate = importDetail.ImportPurchaseOrder.ImportDate,
                                    Note = importDetail.Note,
                                    Quantity = importDetail.QuantityKg,
                                    ReferenceId = referenceId,
                                    Unit = purchaseDetail.Unit,
                                    UnitPrice = purchaseDetail.UnitPrice,
                                    CurrencyCode = purchaseDetail.PurchaseOrder.CurrencyCode.Trim(),
                                };
                                model.Add(entity);
                            }
                            break;
                        //nhien lieu
                        case 2:
                            var transactionDetails = from i in vfi.TransactionFptDetails
                                                     where
                                                         i.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                         i.TransactionFpt.PoId != null &&
                                                         i.TransactionFpt.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import &&
                                                         i.TransactionFpt.Fpt == (int)MyUtilities.PurchaseOrder.FptLot.Fuel &&
                                                         i.PoReferenceDetailId == null &&
                                                         i.FptId == taxInvoiceReference.ReferenceId
                                          && i.TransactionFpt.TransactionDate >= startDate
                                                     select i;
                            var fuel = vfi.Fuels.FirstOrDefault(f => f.FuelId == taxInvoiceReference.ReferenceId);
                            foreach (var importDetail in transactionDetails) {
                                var purchaseDetail =
                                    vfi.PurchaseOrderDetails.FirstOrDefault(
                                        pod => pod.PurchaseOrderId == importDetail.TransactionFpt.PoId &&
                                        pod.ReferenceId == importDetail.FptId);
                                if (purchaseDetail.UnitPrice != taxInvoiceReference.UnitPrice)
                                    continue;
                                var entity = new PoTaxInvoiceReferenceDetailModel {
                                    Code = fuel.FuelFullCode,
                                    ImportDetailId = importDetail.DetailId,
                                    ImportId = importDetail.TransactionId,
                                    ImportCode = importDetail.TransactionFpt.TransactionCode,
                                    ImportDate = importDetail.TransactionFpt.TransactionDate,
                                    Note = importDetail.Note,
                                    Quantity = importDetail.Quantity,
                                    ReferenceId = referenceId,
                                    Unit = purchaseDetail.Unit,
                                    UnitPrice = purchaseDetail.UnitPrice,
                                    CurrencyCode = purchaseDetail.PurchaseOrder.CurrencyCode.Trim(),
                                };
                                model.Add(entity);
                            }
                            break;
                        //cong cu
                        case 3:
                            var transactionDetails2 = from i in vfi.TransactionFptDetails
                                                      where
                                                          i.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                                          i.TransactionFpt.PoId != null &&
                                                          i.TransactionFpt.EoI == (int)MyUtilities.PurchaseOrder.EoILot.Import &&
                                                          i.TransactionFpt.Fpt == (int)MyUtilities.PurchaseOrder.FptLot.Tool &&
                                                          i.PoReferenceDetailId == null &&
                                                          i.FptId == taxInvoiceReference.ReferenceId
                                 && i.TransactionFpt.TransactionDate >= startDate
                                                      select i;
                            var tool = vfi.Tools.FirstOrDefault(f => f.ToolId == taxInvoiceReference.ReferenceId);
                            foreach (var importDetail in transactionDetails2) {
                                var purchaseDetail =
                                    vfi.PurchaseOrderDetails.FirstOrDefault(
                                        pod => pod.PurchaseOrderId == importDetail.TransactionFpt.PoId &&
                                        pod.ReferenceId == importDetail.FptId);
                                if (purchaseDetail.UnitPrice != taxInvoiceReference.UnitPrice)
                                    continue;
                                var entity = new PoTaxInvoiceReferenceDetailModel {
                                    Code = tool.ToolFullCode,
                                    ImportDetailId = importDetail.DetailId,
                                    ImportId = importDetail.TransactionId,
                                    ImportCode = importDetail.TransactionFpt.TransactionCode,
                                    ImportDate = importDetail.TransactionFpt.TransactionDate,
                                    Note = importDetail.Note,
                                    Quantity = importDetail.Quantity,
                                    ReferenceId = referenceId,
                                    Unit = purchaseDetail.Unit,
                                    UnitPrice = purchaseDetail.UnitPrice,
                                    CurrencyCode = purchaseDetail.PurchaseOrder.CurrencyCode.Trim(),
                                };
                                model.Add(entity);
                            }
                            break;
                        //gia cong ngoai-mua san pham
                        case 4:
                        case 6:
                            break;
                        default:
                            break;
                    }



                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectInvoiceDetailById", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult UpdateTaxInvoiceReferenceDetail(
            [Bind(Prefix = "inserted")] IEnumerable<PoTaxInvoiceReferenceDetailModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<PoTaxInvoiceReferenceDetailModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<PoTaxInvoiceReferenceDetailModel> deleteds
            //, int? id
            ) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("ProductCode",
                                         "Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<PoTaxInvoiceReferenceDetailModel>()));
            }
            try {
                updateds = updateds.Where(u => u.IsAdd);
                if (!updateds.Any())
                    throw new AggregateException("Không cập nhật mới!");
                using (var vfi = new tammaContext()) {
                    var referenceId = updateds.FirstOrDefault().ReferenceId;
                    var taxInvoiceReference =
                        vfi.PoTaxInvoiceReferences.FirstOrDefault(tir => tir.DetailId == referenceId);
                    if (taxInvoiceReference == null)
                        throw new AggregateException("Không tìm thấy chi tiết hóa đơn");
                    if (taxInvoiceReference.PoTaxInvoiceReferenceDetails.Any())
                        throw new AggregateException("Chi tiết hóa đơn đã phân nhập kho!");
                    if (taxInvoiceReference.Quantity != updateds.Sum(u => u.Quantity))
                        throw new AggregateException("Chi tiết hóa đơn phân sai số lượng");
                    var list = new List<PoTaxInvoiceReferenceDetail>();
                    foreach (var updated in updateds) {
                        var detail = new PoTaxInvoiceReferenceDetail {
                            DetailReferenceId = referenceId,
                            ImportDetailId = updated.ImportDetailId,
                            ImportId = updated.ImportId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Unit = taxInvoiceReference.Unit,
                            UnitPrice = taxInvoiceReference.UnitPrice,
                            Quantity = updated.Quantity,
                            ReferenceId = taxInvoiceReference.ReferenceId,
                        };
                        list.Add(detail);
                        switch (taxInvoiceReference.PoTaxInvoice.ClassifiedId) {
                            // nguyen lieu
                            case 1:
                                var importDetail =
                                    vfi.ImportPurchaseOrderDetails.FirstOrDefault(
                                        id => id.ImportDetailId == detail.ImportDetailId);
                                if (importDetail == null)
                                    throw new AggregateException("Không tìm thấy chi tiết nhập kho");
                                importDetail.PoTaxInvoiceReferenceDetail = detail;
                                break;
                            case 2:
                            case 3:
                                var transactionDetail =
                                    vfi.TransactionFptDetails.FirstOrDefault(td => td.DetailId == detail.ImportDetailId);
                                if (transactionDetail == null)
                                    throw new AggregateException("Không tìm thấy chi tiết nhập kho");
                                transactionDetail.PoTaxInvoiceReferenceDetail = detail;
                                break;
                            default:
                                break;
                        }
                    }
                    vfi.PoTaxInvoiceReferenceDetails.AddRange(list);
                    vfi.SaveChanges();
                    //foreach (var detail in list)
                    //{
                    //    switch (taxInvoiceReference.PoTaxInvoice.ClassifiedId)
                    //    {
                    //        // nguyen lieu
                    //        case 1:
                    //            var importDetail =
                    //                vfi.ImportPurchaseOrderDetails.FirstOrDefault(
                    //                    id => id.ImportDetailId == detail.ImportDetailId);
                    //            if (importDetail == null)
                    //                throw new AggregateException("Không tìm thấy chi tiết nhập kho");
                    //            importDetail.PoReferenceDetailId = detail.DetailId;
                    //            break;
                    //        case 2:
                    //        case 3:
                    //            var transactionDetail =
                    //                vfi.TransactionFptDetails.FirstOrDefault(td => td.DetailId == detail.ImportDetailId);
                    //            if (transactionDetail == null)
                    //                throw new AggregateException("Không tìm thấy chi tiết nhập kho");
                    //            transactionDetail.PoReferenceDetailId = detail.DetailId;
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}
                    //vfi.SaveChanges();
                    var taxInvoice =
                        vfi.PoTaxInvoices.FirstOrDefault(ti => ti.TaxInvoiceId == taxInvoiceReference.TaxInvoiceId);
                    var total = taxInvoice.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice);
                    var money = taxInvoice.PoTaxInvoiceMoneys.Where(
                        tim => tim.TaxInvoiceId == taxInvoiceReference.TaxInvoiceId && tim.Status == (byte)MyUtilities.Sales.Status.Completed)
                                          .Sum(tim => Math.Abs(tim.Money));
                    if ((Math.Round(total - money, 2) == 0)) {
                        if (taxInvoice.PoTaxInvoiceReferences.Any(tip => !tip.PoTaxInvoiceReferenceDetails.Any())) {
                            taxInvoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                        }
                        else {
                            taxInvoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                        }
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateTaxInvoiceReferenceDetail", ex.Message);
            }
            return View(new GridModel(new List<PoTaxInvoiceReferenceDetailModel>()));
        }

        [GridAction]
        public ActionResult SelectPoTaxInvoice(string fromDate, string toDate, int status) {
            try {
                return View(new GridModel(GetPoTaxInvoices(fromDate, toDate, status)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectPoTaxInvoice", ex.Message);
                return View(new GridModel(new List<PoTaxInvoiceModel>()));
            }
        }
        List<PoTaxInvoiceModel> GetPoTaxInvoices(string fromDate, string toDate, int status) {
            var model = new List<PoTaxInvoiceModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var poTaxInvoice = vfi.PoTaxInvoices.Where(ti => ti.Status == status);
                    foreach (var taxInvoice in poTaxInvoice) {
                        var entity = new PoTaxInvoiceModel {
                            TaxInvoiceId = taxInvoice.TaxInvoiceId,
                            VendorId = taxInvoice.VendorId,
                            VendorName = taxInvoice.Vendor.VendorName,
                            CurrencyCode = taxInvoice.CurrencyCode,
                            TaxPercent = taxInvoice.TaxPercent,
                            ExchangeRate = taxInvoice.ExchangeRate,
                            TaxInvoiceNumber = taxInvoice.TaxInvoiceNumber,
                            PoDate = taxInvoice.PoDate,
                            ClassifiedId = taxInvoice.ClassifiedId,
                            ModifiedDate = taxInvoice.ModifiedDate,
                            ModifiedUser = taxInvoice.ModifiedUser,
                            ClassifiedName = taxInvoice.MaterialClassified.MaterialClassifiedName,
                        };
                        entity.TotalQuantity = taxInvoice.PoTaxInvoiceReferences.Sum(tir => tir.Quantity);
                        entity.Total = taxInvoice.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice);
                        entity.Total += (entity.Total * entity.TaxPercent / 100);
                        entity.Money =
                            taxInvoice.PoTaxInvoiceMoneys.Where(
                                tim => tim.Status == (byte)MyUtilities.Transaction.Status.Approved && tim.Money > 0)
                                      .Sum(tim => tim.Money);
                        entity.Reduce =
                            taxInvoice.PoTaxInvoiceMoneys.Where(
                                tim => tim.Status == (byte)MyUtilities.Transaction.Status.Approved && tim.Money < 0)
                                      .Sum(tim => tim.Money);
                        entity.Required = entity.Total - entity.Money - entity.Reduce;
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
        public ActionResult SelectPoTaxInvoiceReferenceById(int taxInvoiceId) {
            var model = new List<PoTaxInvoiceReferenceModel>();
            try {
                model = TaxInvoiceProductById(taxInvoiceId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectPoTaxInvoiceReferenceById", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<PoTaxInvoiceReferenceModel> TaxInvoiceProductById(int taxInvoiceId) {
            var model = new List<PoTaxInvoiceReferenceModel>();
            using (var vfi = new tammaContext()) {
                var taxInvoiceReferences = vfi.PoTaxInvoiceReferences.Where(tir => tir.TaxInvoiceId == taxInvoiceId);
                foreach (var reference in taxInvoiceReferences) {
                    var entity = new PoTaxInvoiceReferenceModel {

                        DetailId = reference.DetailId,
                        ReferenceId = reference.ReferenceId,
                        Quantity = reference.Quantity,
                        UnitPrice = reference.UnitPrice,
                        CurrencyCode = reference.PoTaxInvoice.CurrencyCode,
                        TaxPercent = reference.PoTaxInvoice.TaxPercent,
                        ExchangeRate = reference.PoTaxInvoice.ExchangeRate,
                        Unit = reference.Unit,
                    };
                    try {
                        switch (reference.PoTaxInvoice.ClassifiedId) {
                            case 1:
                                var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == reference.ReferenceId);
                                entity.Code = material.MaterialCode;
                                break;
                            case 2:
                                var fuel = vfi.Fuels.FirstOrDefault(f => f.FuelId == reference.ReferenceId);
                                entity.Code = fuel.FuelFullCode;
                                break;
                            case 3:
                                var tool = vfi.Tools.FirstOrDefault(t => t.ToolId == reference.ReferenceId);
                                entity.Code = tool.ToolFullCode;
                                break;
                            case 4:
                            case 6:
                                var product = vfi.Products.FirstOrDefault(p => p.ProductId == reference.ReferenceId);
                                entity.Code = product.ProductCode;
                                break;
                            default:
                                break;
                        }
                    }
                    catch (ArgumentNullException) {
                        throw new AggregateException("Lỗi! Không tìm thấy mã");
                    }
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectPoTaxInvoiceReferenceDetailById(int referenceId) {
            var model = new List<PoTaxInvoiceReferenceDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var taxInvoiceReferenceDetails =
                        vfi.PoTaxInvoiceReferenceDetails.Where(tird => tird.DetailReferenceId == referenceId);
                    foreach (var detail in taxInvoiceReferenceDetails) {
                        var entity = new PoTaxInvoiceReferenceDetailModel {
                            ImportDetailId = detail.ImportDetailId,
                            ImportId = detail.ImportId,
                            Quantity = detail.Quantity,
                            ReferenceId = detail.ReferenceId,
                            Unit = detail.Unit,
                            UnitPrice = detail.UnitPrice,
                            CurrencyCode = detail.PoTaxInvoiceReference.PoTaxInvoice.CurrencyCode.Trim(),
                            ExchangeRate = detail.PoTaxInvoiceReference.PoTaxInvoice.ExchangeRate,
                            TaxPercent = detail.PoTaxInvoiceReference.PoTaxInvoice.TaxPercent,
                            ModifiedDate = detail.ModifiedDate,
                            ModifiedUser = detail.ModifiedUser,
                            DetailReferenceId = referenceId,
                            DetailId = detail.DetailId,
                        };
                        model.Add(entity);
                        try {
                            switch (detail.PoTaxInvoiceReference.PoTaxInvoice.ClassifiedId) {
                                case 1:
                                    var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == detail.ReferenceId);
                                    entity.Code = material.MaterialCode;
                                    var importDetail =
                                        vfi.ImportPurchaseOrderDetails.FirstOrDefault(
                                            id => id.ImportDetailId == entity.ImportDetailId);
                                    entity.ImportDate = importDetail.ImportPurchaseOrder.ImportDate;
                                    entity.ImportCode = importDetail.ImportPurchaseOrder.Transaction.TransactionCode;
                                    break;
                                case 2:
                                    var fuel = vfi.Fuels.FirstOrDefault(f => f.FuelId == detail.ReferenceId);
                                    entity.Code = fuel.FuelFullCode;
                                    var transactionDetail =
                                        vfi.TransactionFptDetails.FirstOrDefault(
                                            td => td.DetailId == entity.ImportDetailId);
                                    entity.ImportDate = transactionDetail.TransactionFpt.TransactionDate;
                                    entity.ImportCode = transactionDetail.TransactionFpt.TransactionCode;
                                    break;
                                case 3:
                                    var tool = vfi.Tools.FirstOrDefault(t => t.ToolId == detail.ReferenceId);
                                    entity.Code = tool.ToolFullCode;
                                    var transactionDetail2 =
                                        vfi.TransactionFptDetails.FirstOrDefault(
                                            td => td.DetailId == entity.ImportDetailId);
                                    entity.ImportDate = transactionDetail2.TransactionFpt.TransactionDate;
                                    entity.ImportCode = transactionDetail2.TransactionFpt.TransactionCode;
                                    break;
                                case 4:
                                case 6:
                                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == detail.ReferenceId);
                                    entity.Code = product.ProductCode;
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (ArgumentNullException) {
                            throw new AggregateException("Lỗi! Không tìm thấy mã");
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectPoTaxInvoiceReferenceById", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult CancelPoTaxInvoiceReference(int detailId) {
            try {
                using (var vfi = new tammaContext()) {
                    var taxInvoiceReference = vfi.PoTaxInvoiceReferences.FirstOrDefault(tir => tir.DetailId == detailId);
                    foreach (var detail in taxInvoiceReference.PoTaxInvoiceReferenceDetails) {
                        switch (detail.PoTaxInvoiceReference.PoTaxInvoice.ClassifiedId) {
                            case 1:
                                var importDetail =
                                    vfi.ImportPurchaseOrderDetails.FirstOrDefault(
                                        id => id.ImportDetailId == detail.ImportDetailId);
                                importDetail.PoReferenceDetailId = null;
                                break;
                            case 2:
                            case 3:
                                var transactionDetail =
                                    vfi.TransactionFptDetails.FirstOrDefault(
                                        td => td.DetailId == detail.ImportDetailId);
                                transactionDetail.PoReferenceDetailId = null;
                                break;
                            case 4:
                            case 6:
                                break;
                            default:
                                break;
                        }
                    }
                    vfi.PoTaxInvoiceReferenceDetails.RemoveRange(taxInvoiceReference.PoTaxInvoiceReferenceDetails);
                    vfi.SaveChanges();
                    return View(new GridModel(TaxInvoiceProductById(taxInvoiceReference.TaxInvoiceId)));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CancelPoTaxInvoiceReference", ex.Message);
            }
            return View(new GridModel(new List<PoTaxInvoiceReferenceModel>()));
        }

        [GridAction]
        public ActionResult CancelPoTaxInvoice(int taxInvoiceId) {
            try {
                using (var vfi = new tammaContext()) {
                    var taxInvoice = vfi.PoTaxInvoices.FirstOrDefault(ti => ti.TaxInvoiceId == taxInvoiceId);
                    if (taxInvoice.PoTaxInvoiceReferences.Any(tir => tir.PoTaxInvoiceReferenceDetails.Any()))
                        throw new AggregateException("Lỗi! Phải hủy phân nhập kho trước khi hủy hóa đơn");
                    taxInvoice.Status = (byte)MyUtilities.Sales.Status.Cancel;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CancelTaxInvoice", ex.Message);
            }
            return View(new GridModel(GetPoTaxInvoices("", "", (byte)MyUtilities.Sales.Status.Waiting)));
        }

        public ActionResult SelectComboBoxPoTaxInvoiceByVendor(int vendorId) {
            var model = new List<PoTaxInvoiceModel>();
            using (var vfi = new tammaContext()) {
                var taxInvoiceList =
                    vfi.PoTaxInvoices.Where(
                        ti =>
                            ti.VendorId == vendorId &&
                        ti.PoDate != null &&
                        ti.Status == (byte)MyUtilities.Sales.Status.Waiting);
                foreach (var taxInvoice in taxInvoiceList) {
                    if (!taxInvoice.PoTaxInvoiceReferences.Any(tir => !tir.PoTaxInvoiceReferenceDetails.Any()))
                        continue;
                    var entity = new PoTaxInvoiceModel {
                        TaxInvoiceNumber = taxInvoice.TaxInvoiceNumber,
                        VendorName = taxInvoice.Vendor.VendorName,
                        Total = taxInvoice.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice),
                        TaxInvoiceId = taxInvoice.TaxInvoiceId,
                        ExchangeRate = taxInvoice.ExchangeRate,
                        CurrencyCode = taxInvoice.CurrencyCode,
                        PoDate = taxInvoice.PoDate
                    };

                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "TaxInvoiceId", "TaxInvoiceNumber"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [GridAction]
        public ActionResult SelectPoTaxInvoiceByVendor(int vendorId) {
            var model = new List<PoTaxInvoiceModel>();
            if (vendorId == 0)
                return View(new GridModel(model));
            try {
                using (var vfi = new tammaContext()) {
                    var taxInvoices =
                        vfi.PoTaxInvoices.Where(
                            ti =>
                            ti.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                            ti.Status != (byte)MyUtilities.Sales.Status.Completed &&
                            ti.VendorId == vendorId);
                    foreach (var poTaxInvoice in taxInvoices) {
                        var entity = new PoTaxInvoiceModel {
                            ClassifiedName = poTaxInvoice.MaterialClassified.MaterialClassifiedName,
                            CurrencyCode = poTaxInvoice.CurrencyCode,
                            ModifiedDate = poTaxInvoice.ModifiedDate,
                            ModifiedUser = poTaxInvoice.ModifiedUser,
                            ExchangeRate = poTaxInvoice.ExchangeRate,
                            TaxPercent = poTaxInvoice.TaxPercent,
                            VendorName = poTaxInvoice.Vendor.VendorName,
                            TaxInvoiceNumber = poTaxInvoice.TaxInvoiceNumber,
                            TotalQuantity = poTaxInvoice.PoTaxInvoiceReferences.Sum(tir => tir.Quantity),
                            Total = poTaxInvoice.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice),
                            PoDate = poTaxInvoice.PoDate,
                            VendorId = poTaxInvoice.VendorId,
                            TaxInvoiceId = poTaxInvoice.TaxInvoiceId,
                            ClassifiedId = poTaxInvoice.ClassifiedId,
                        };
                        entity.Total += (entity.Total * entity.TaxPercent / 100);
                        entity.Required = entity.Total;
                        var taxInvoiceDetailAdd =
                                 vfi.PoTaxInvoiceMoneys.Where(
                                     tid =>
                                     tid.TaxInvoiceId == entity.TaxInvoiceId &&
                                     tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        if (taxInvoiceDetailAdd.Any())
                            entity.Required -= -taxInvoiceDetailAdd.Sum(tid => Math.Abs(tid.Money));
                        if (entity.Required == 0)
                            continue;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectPoTaxInvoiceByVendor", ex.Message);
            }
            return View(new GridModel(model));
        }



        [GridAction]
        public ActionResult PushPoTaxInvoiceMoneyModel(
            [Bind(Prefix = "inserted")] IEnumerable<PoTaxInvoiceModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<PoTaxInvoiceModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<PoTaxInvoiceModel> deleteds,
            string importDate
            ) {
            if (updateds != null) {
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

                        foreach (var detail in updateds) {
                            var taxInvoice =
                                vfi.PoTaxInvoices.FirstOrDefault(ti => ti.TaxInvoiceId == detail.TaxInvoiceId);
                            if (taxInvoice == null)
                                throw new ArgumentException("Error liên hệ admin!" + detail.TaxInvoiceNumber);

                            if (Math.Abs(detail.Money) + Math.Abs(detail.Reduce) <= 0) continue;
                            var moneyReceived = 0.0;
                            var taxInvoiceDetailAdd =
                                taxInvoice.PoTaxInvoiceMoneys.Where(
                                    tid =>
                                    tid.TaxInvoiceId == detail.TaxInvoiceId &&
                                    tid.Status == (byte)MyUtilities.Transaction.Status.Approved);
                            if (taxInvoiceDetailAdd.Any())
                                moneyReceived = Math.Round(taxInvoiceDetailAdd.Sum(tid => Math.Abs(tid.Money)), 2);
                            var total = taxInvoice.PoTaxInvoiceReferences.Sum(tip => tip.Quantity * tip.UnitPrice);
                            total += (total * taxInvoice.TaxPercent / 100);
                            if (Math.Round((total - moneyReceived -
                                            (Math.Abs(detail.Money) - Math.Abs(detail.Reduce))), 2) < 0)
                                throw new ArgumentException("Số tiền không được lớn hơn số nợ. " +
                                                            detail.TaxInvoiceNumber);
                            if (detail.Money > 0) {
                                var entity = new PoTaxInvoiceMoney {
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    Money = Math.Round(detail.Money, 2),
                                    TaxInvoiceId = detail.TaxInvoiceId,
                                    Note = detail.Note,
                                    Times = taxInvoiceDetailAdd.Count() + 1,
                                    Status = (byte)MyUtilities.Sales.Status.Waiting,
                                    ImportDate = iDate,
                                };
                                vfi.PoTaxInvoiceMoneys.Add(entity);
                            }
                            if (detail.Reduce > 0) {
                                var entity = new PoTaxInvoiceMoney {
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    Money = Math.Round(detail.Reduce, 2) * -1,
                                    TaxInvoiceId = detail.TaxInvoiceId,
                                    Note = detail.Note,
                                    Times = taxInvoiceDetailAdd.Count() + 1,
                                    Status = (byte)MyUtilities.Sales.Status.Waiting,
                                    ImportDate = iDate,
                                };
                                vfi.PoTaxInvoiceMoneys.Add(entity);

                            }
                            //if (detail.IsReduce)
                            //    entity.Money = Math.Round(detail.Money*(-1), 2);
                        }
                        vfi.SaveChanges();
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("PushTaxInvoice", ex.Message);
                }
            }
            return View(new GridModel(new List<PoTaxInvoiceMoneyModel>()));
        }

        [GridAction]
        public ActionResult SelectTaxInvoideMoney(int taxInvoiceId, int status) {
            var model = new List<PoTaxInvoiceMoneyModel>();
            try {
                model = GetPoTaxInvoiceMoney(taxInvoiceId, status);
            }
            catch (Exception ex) {
                ModelState.AddModelError("PushTaxInvoice", ex.Message);
            }
            return View(new GridModel(model));
        }
        List<PoTaxInvoiceMoneyModel> GetPoTaxInvoiceMoney(int taxInvoiceId, int status) {
            var model = new List<PoTaxInvoiceMoneyModel>();
            using (var vfi = new tammaContext()) {
                var taxInvoiceMoneys = from tim in vfi.PoTaxInvoiceMoneys
                                       where tim.Status == status
                                             && (taxInvoiceId == 0 || tim.TaxInvoiceId == taxInvoiceId)
                                       select tim;
                foreach (var poTaxInvoiceMoney in taxInvoiceMoneys) {
                    var entity = new PoTaxInvoiceMoneyModel {
                        CurrencyCode = poTaxInvoiceMoney.PoTaxInvoice.CurrencyCode,
                        ImportDate = poTaxInvoiceMoney.ImportDate,
                        IsReduce = poTaxInvoiceMoney.Money < 0,
                        ModifiedDate = poTaxInvoiceMoney.ModifiedDate,
                        ModifiedUser = poTaxInvoiceMoney.ModifiedUser,
                        Money = poTaxInvoiceMoney.Money,
                        TaxInvoiceNumber = poTaxInvoiceMoney.PoTaxInvoice.TaxInvoiceNumber,
                        Required = poTaxInvoiceMoney.PoTaxInvoice.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice),
                        Times = poTaxInvoiceMoney.Times,
                        Note = poTaxInvoiceMoney.Note,
                        DetailId = poTaxInvoiceMoney.DetailId,
                        TaxInvoiceId = poTaxInvoiceMoney.TaxInvoiceId,
                        ExchangeRate = poTaxInvoiceMoney.PoTaxInvoice.ExchangeRate,
                        TaxPercent = poTaxInvoiceMoney.PoTaxInvoice.TaxPercent
                    };
                    entity.Required += (entity.Required * entity.TaxPercent / 100);
                    model.Add(entity);
                }
            }
            return model;

        }

        [GridAction]
        public ActionResult CancelTaxInvoideMoney(int detailId) {
            try {
                using (var vfi = new tammaContext()) {
                    var taxInvoiceMoney = vfi.PoTaxInvoiceMoneys.FirstOrDefault(tim => tim.DetailId == detailId);
                    taxInvoiceMoney.Status = (byte)MyUtilities.Sales.Status.Cancel;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PushTaxInvoice", ex.Message);
            }
            return View(new GridModel(GetPoTaxInvoiceMoney(0, 1)));
        }

        [HttpPost]
        public ActionResult UpdatePoTaxInvoiceMoney(long[] checkedRecords) {
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
                        var taxInvoiceMoney = vfi.PoTaxInvoiceMoneys.FirstOrDefault(tim => tim.DetailId == detailId);
                        if (taxInvoiceMoney.Status != (byte)MyUtilities.Transaction.Status.Open) continue;
                        var taxInvoice = vfi.PoTaxInvoices.FirstOrDefault(ti => ti.TaxInvoiceId == taxInvoiceMoney.TaxInvoiceId);
                        var total = taxInvoice.PoTaxInvoiceReferences.Sum(tir => tir.Quantity * tir.UnitPrice);
                        total += (total * taxInvoice.TaxPercent / 100);
                        var totalMoneyHasAdd = taxInvoice.PoTaxInvoiceMoneys.Where(tim => tim.Status == (byte)MyUtilities.Sales.Status.Completed).Sum(tim => Math.Abs(tim.Money));
                        if ((Math.Round(total - totalMoneyHasAdd - Math.Abs(taxInvoiceMoney.Money), 2) < 0))
                            return Json(@"Tien can duyet vuot qua so tien can thiet ");
                        taxInvoiceMoney.Status = (byte)MyUtilities.Sales.Status.Completed;
                        vfi.SaveChanges();
                        if ((Math.Round(total - totalMoneyHasAdd - Math.Abs(taxInvoiceMoney.Money), 2) == 0)) {
                            if (taxInvoice.PoTaxInvoiceReferences.Any(tip => !tip.PoTaxInvoiceReferenceDetails.Any())) {
                                taxInvoice.Status = (byte)MyUtilities.Sales.Status.InProcess;
                            }
                            else {
                                taxInvoice.Status = (byte)MyUtilities.Sales.Status.Completed;
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


        // Purchasing Progress
        //[GridAction]
        //public ActionResult GetPurchaseOrderProgress(int classifiedId, int vendorId, string inquiryNumber) {
        //    var model = new List<PurchaseProgressModel>();
        //        //model = PurchaseOrderProgress(classifiedId, vendorId, inquiryNumber);
        //    return View(new GridModel(model));
        //}


        //List<PoTaxInvoiceMoneyModel> PurchaseOrderProgress(int classifiedId, int vendorId, string inquiryNumber) {
        //    var model = new List<PurchaseProgressModel>();
        //    using (var vfi = new tammaContext()) {

        //    }
        //    return model;

        //}

        #endregion

        #region DeliveryAddress
        //Select
        [GridAction]
        public ActionResult SelectDeliveryAddress() {
            return View(new GridModel(GetDeliveryAddress()));
        }

        public IEnumerable<DeliveryAddressModel> GetDeliveryAddress() {
            try {
                using (var vfi = new tammaContext()) {
                    return vfi.DeliveryAddresses.Select(t => new DeliveryAddressModel {
                        Telephone = t.Telephone,
                        Address = t.Address,
                        Active = t.Active,
                        AddressName = t.AddressName,
                        AddressShortName = t.AddressShortName,
                        ModifiedDate = t.ModifiedDate,
                        ModifiedUser = t.ModifiedUser,
                        AddressId = t.AddressId,
                        Recipient = t.Recipient,
                    })
                    .OrderBy(t => t.AddressId)
                    .ToList();
                }
            }
            catch (Exception) {
                return null;
            }
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertDeliveryAddress(DeliveryAddressModel insert) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<DeliveryAddressModel>()));
            }

            var address = new DeliveryAddress {
                Address = insert.Address,
                AddressName = insert.AddressName,
                AddressShortName = insert.AddressShortName,
                Telephone = insert.Telephone,
                ModifiedDate = DateTime.Now,
                ModifiedUser = HttpContext.User.Identity.Name,
                Active = true,
                Recipient = insert.Recipient,
            };
            using (var vfi = new tammaContext()) {
                vfi.DeliveryAddresses.Add(address);
                vfi.SaveChanges();
            }
            return View(new GridModel(GetDeliveryAddress()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateDeliveryAddress(DeliveryAddressModel update) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<DeliveryAddressModel>()));
            }

            using (var vfi = new tammaContext()) {
                var address = vfi.DeliveryAddresses.FirstOrDefault(x => x.AddressId == update.AddressId);
                if (address == null) { throw new AggregateException("Lỗi! Không tìm thấy phương thức"); }
                address.Address = update.Address;
                address.AddressName = update.AddressName;
                address.AddressShortName = update.AddressShortName;
                address.Active = update.Active;
                address.Telephone = update.Telephone;
                address.ModifiedDate = DateTime.Now;
                address.ModifiedUser = HttpContext.User.Identity.Name;
                address.Recipient = update.Recipient;
                vfi.SaveChanges();
            }
            return View(new GridModel(GetDeliveryAddress()));
        }


        public ActionResult SelectComboBoxDeliveryAddress() {
            return new JsonResult {
                Data = new SelectList(GetAllDeliveryAddress().Where(f => f.Active), "AddressId", "AddressShortName")
            };
        }

        List<DeliveryAddressModel> GetAllDeliveryAddress() {
            var model = new List<DeliveryAddressModel>();
            using (var vfi = new tammaContext()) {
                model.AddRange(vfi.DeliveryAddresses.Select(t => new DeliveryAddressModel {
                    Active = t.Active,
                    Address = t.Address,
                    AddressId = t.AddressId,
                    AddressName = t.AddressName,
                    AddressShortName = t.AddressShortName,
                    ModifiedDate = t.ModifiedDate,
                    ModifiedUser = t.ModifiedUser,
                    Telephone = t.Telephone,
                }));
            }
            return model.OrderBy(m => m.AddressId).ToList();
        }

        #endregion


    }
}

