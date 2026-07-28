using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
//using Vfi.Client.Module.Production.Interfaces;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Utilities;
using System.Globalization;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Controllers;
using System.Data.Entity.Validation;
using Telerik.Web.Mvc.Extensions;
using Vfi.Ui.Mvc.Vfi.Areas.Sales.Models;

namespace Vfi.Ui.Mvc.Vfi.Controllers.Production {
    public class ProductController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductionController _productionController;
        [InjectionConstructor]
        public ProductController(IUnitOfWork unitOfWork
            , ProductionController productionController
            ) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
            _productionController = productionController;
        }
        #region view

        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }
            return ViewData;
        }
        public ActionResult ProductManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementInv() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementTax() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementQc() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementProduction() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementMaterial() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementCnc() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementProduction2() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementPricing() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductComparePricing() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementSales() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            string flag = "hidden";
            if (MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.SaleManagement)) {
                flag = "visible";
            }
            ViewData = GetPageConfigData();
            return View(new ProductModel { PrintProductList = flag });
        }
        public ActionResult ProductManagementQuote() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();

           var canLock = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.SaleManagementLv2);

           return View(new ProductModel { IsCalculateLock = canLock });
        }
        public ActionResult ProductManagementHeatTreatment() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementSurfaceTreatment() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementPlating() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductManagementPacking() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        #endregion

        #region Product

        [GridAction]
        public ActionResult SelectProductMaterialDesign(int productId) {
            try {
                using (var vfi = new tammaContext()) {
                    var model = new List<ProductModel>();
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null) throw new AggregateException("Lỗi! Không tìm thấy sản phẩm");
                    var material = product.ProductionMaterials.Where(pm => pm.Active);
                    foreach (var productionMaterial in material) {
                        var entity =
                            model.FirstOrDefault(
                                m => m.MaterialNameDesign.Equals(productionMaterial.Material.MaterialName)
                                     && m.OutDiameterDesign == productionMaterial.Material.OutDiameter
                                     && m.InDiameterDesign == productionMaterial.Material.InDiameter
                                     && m.ShapeDesign.Equals(productionMaterial.Material.Shape)
                                     && m.DiameterTypeDesign == productionMaterial.Material.DiameterType);
                        if (entity == null) {
                            entity = new ProductModel {
                                ProductId = productId,
                                SectionName = "Nguyên liệu thiết kế",
                                MaterialNameDesign = productionMaterial.Material.MaterialName,
                                OutDiameterDesign = productionMaterial.Material.OutDiameter,
                                InDiameterDesign = productionMaterial.Material.InDiameter,
                                ShapeDesign = productionMaterial.Material.Shape,
                                DiameterTypeDesign = productionMaterial.Material.DiameterType,
                            };
                        }
                        model.Add(entity);
                    }
                    //else
                    return View(new GridModel(model));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProduct", ex.Message);
            }
            return View(new GridModel(new List<ProductModel>()));
        }

        public List<ProductModel> SelectAllProducts(bool active, int status, int customerId, 
            string productCode, int productId, 
            string fromDate, string productName) {
            var model = new List<ProductModel>();
            //var customerId = -1;
            //var productCode = "";
            if (customerId == 0 && string.IsNullOrWhiteSpace(productCode) && string.IsNullOrWhiteSpace(productName) && 
                status == 0 && productId == 0)
                return model;
            try {

                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;

                    var products = (from p in vfi.Products
                                    where (!active || p.Active == active)
                                        //&& (status == 0 || p.Status == status)
                                          && (customerId <= 0 || p.CustomerId == customerId)
                                          && (productId <= 0 || p.ProductId == productId)
                                    //&& p.ProductCode.Contains(productCode)
                                    select new {
                                        p.ProductId,
                                        ProductCode = (p.ProductCode + "").Trim(),
                                        ProductName = (p.ProductName + "").Trim(),
                                        p.CustomerId,
                                        p.Customer.CustomerCode,
                                        p.Customer.CustomerName,
                                        CustomerShortName = p.Customer.ShortName,
                                        p.MaterialId,
                                        p.Material,
                                        //p.Material.MaterialName,
                                        p.DesignNo,
                                        p.MachineFunction,
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
                                        UploadDate = p.UploadDate ?? DateTime.Now,
                                        Status = p.Status.Value,
                                        p.ProcessingType,
                                        p.DrawingFinish,
                                        p.DiameterTypeDesign,
                                        KnifeCut = p.KnifeCut ?? 0,
                                        p.ProductionSections,
                                        MillProductivity = p.MillProductivity ?? 0,
                                        MaterialCost = p.MaterialCost ?? 0,
                                        p.QcProductivity,
                                        p.ProductShape,
                                        CncProductivity = p.CncProductivity ?? 0,
                                        p.TaxCode,
                                        p.FinishDesign,
                                        p.NewUpdateDate,
                                        ProductionMaterial =
                                        p.ProductionMaterials.Where(pm => pm.Active).OrderBy(pm => pm.Priority).FirstOrDefault(),
                                        p.Note,
                                        p.Currency,
                                        p.PackingFee,
                                        IsCalculateLock = p.IsCalculateLock ?? false,
                                        ProductionLossRate = p.ProductionLossRate ?? 0
                                    }).ToList();

                    //if (!string.IsNullOrWhiteSpace(productCode))
                    //    products =
                    //        products.Where(
                    //            p =>
                    //                p.ProductCode.Contains(productCode) ||
                    //                p.ProductName.Contains(productCode) ||
                    //                p.DesignNo.Contains(productCode)).ToList();
                    if (!string.IsNullOrWhiteSpace(productCode))
                        products = products.Where(p => p.ProductCode.ToLower().Contains(productCode.ToLower())).ToList();
                    if (!string.IsNullOrWhiteSpace(productName))
                        products = products.Where(p => p.ProductName.ToLower().Contains(productName.ToLower())).ToList();
                    if (!string.IsNullOrWhiteSpace(fromDate)) {
                        var ci = new CultureInfo("vi-VN");
                        var fdate = string.IsNullOrWhiteSpace(fromDate)
                            ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
                            : Convert.ToDateTime(fromDate, ci);
                        products = products.Where(p => p.NewUpdateDate >= fdate).ToList();
                    }
                    //if (status != 0) {
                    //    if (status == (byte)MyUtilities.Product.ProductStatusEnum.Calculated) {
                    //        products = products.Where(p => p.FinishDesign).ToList();
                    //    }
                    //    else if (status == (byte)MyUtilities.Product.ProductStatusEnum.Calculating)
                    //        products = products.Where(p => !p.FinishDesign).ToList();
                    //}
                    var productIds = products.Select(p => p.ProductId).ToList();
                    var productionSections = (from ps in vfi.ProductionSections
                                             where productIds.Contains(ps.ProductId)
                                                   && ps.Active
                                             select new {
                                                 ps.ProductId,
                                                 ps.Section.SectionName,
                                                 SectionCost = ps.Productivity * ps.Section.SaleFactor
                                             }).ToList();
                    var productPlatings = (from pp in vfi.ProductionPlatings
                                          where productIds.Contains(pp.ProductId)
                                                && pp.Active
                                          select new {
                                              pp.ProductId,
                                              pp.PlatingName,
                                              pp.PlatingCost,
                                          }).ToList();
                    //if (active)
                    //    products = products.Where(p => p.Active);
                    //if (status != 0)
                    //    products = products.Where(p => p.Status == status);
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null)
                        throw new AggregateException("Vui lòng đăng nhập lại");
                    var processingType = vfi.ProcessingTypes.FirstOrDefault(m => m.TypeId == 6);
                    foreach (var entity in products) {
                        //if (entity.ProductCode.Equals("1003"))
                        //    model = new List<ProductModel>();
                        var product = new ProductModel {
                            ProductId = entity.ProductId,
                            ProductCode = entity.ProductCode,
                            ProductName = entity.ProductName,
                            CustomerId = entity.CustomerId,
                            CustomerCode = entity.CustomerCode,
                            CustomerName = entity.CustomerName,
                            CustomerShortName = entity.CustomerShortName,
                            MachineFunction = entity.MachineFunction,
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
                            UploadDate = entity.UploadDate.ToString("yyyyMMddhhmmss"),
                            //ProcessingTypeId = entity.ProcessingType.TypeId,
                            //ProcessingTypeName = entity.ProcessingType.TypeName,
                            DiameterTypeDesign = entity.DiameterTypeDesign,
                            KnifeCut = entity.KnifeCut,
                            StatusFilter = entity.Status,
                            Status = entity.Status,
                            StatusName = MyUtilities.Product.GetText(entity.Status),
                            MillProductivity = entity.MillProductivity,
                            //MaterialCost = entity.MaterialCost,
                            MillCost = entity.MillProductivity * processingType.ProcessingSaleFactor.Value,
                            ProcessingSalesCost = 0,
                            ProcessingCost = 0,
                            QcProductivity = entity.QcProductivity,
                            ProductShape = entity.ProductShape,
                            CncProductivity = entity.CncProductivity,
                            TaxCode = entity.TaxCode,
                            Weight = 0,
                            Note = entity.Note,
                            Currency = entity.Currency,
                            MaterialCost = 0,
                            PackingFee = entity.PackingFee ?? 0,
                            IsCalculateLock = entity.IsCalculateLock,
                            ProductionLossRate = entity.ProductionLossRate,
                        };
                        if (entity.MaterialId != null) {
                            product.MaterialId = entity.MaterialId.Value;
                            product.MaterialName = entity.Material.MaterialName;
                            product.MaterialCode = entity.Material.MaterialCode;
                            product.MaterialCodeDesign = entity.Material.MaterialName +
                                                         MyUtilities.Material.GetMaterialDesignNo(entity.Material);
                            product.Weight = MyUtilities.Product
                                .GetProductWeight(entity.Material.MaterialName,
                                    entity.Material.OutDiameter,
                                    entity.Material.InDiameter,
                                    product.Length ?? 0,
                                    product.KnifeCut,
                                    entity.Material.Shape + "");
                            product.MaterialCost = entity.Material.UnitPrice;
                        }
                        if (entity.ProcessingType != null) {
                            product.ProcessingTypeId = entity.ProcessingType.TypeId;
                            product.ProcessingTypeName = entity.ProcessingType.TypeName;
                            product.ProcessingSalesCost =
                                entity.ProcessingType.ProcessingSaleFactor.Value * entity.Productivity;
                            product.ProcessingCost = entity.ProcessingType.ProcessingFactor.Value * entity.Productivity;
                        }
                        //var material =
                        //    vfi.Materials.FirstOrDefault(
                        //        m =>
                        //        m.MaterialName.Equals(product.MaterialNameDesign) &&
                        //        m.OutDiameter == product.OutDiameterDesign);
                        //if (material != null)
                        //product.MaterialCost = material.UnitPrice;
                        //if (entity.Material != null)
                        //{
                        //}
                        var productionSectionById = productionSections.Where(ps => ps.ProductId == product.ProductId);

                        if (productionSectionById.Any()) {
                            //product.SectionCount = productionSectionById.Count();
                            //product.SectionCost = productionSectionById.Sum(ps => ps.SectionCost);
                            foreach (var section in productionSectionById) {
                                product.SectionName += (section.SectionName + ",");
                                product.SectionCost += section.SectionCost;
                                product.SectionCount++;
                            }
                            product.SectionName.Remove(product.SectionName.Length - 1);
                        }
                        else {
                            product.SectionName = "Without";
                        }
                        var productPlatingById = productPlatings.Where(ps => ps.ProductId == product.ProductId);
                        if (productPlatingById.Any()) {
                            product.PlatingName = productPlatingById.FirstOrDefault().PlatingName;
                            product.PlatingCost = productPlatingById.Sum(ps => ps.PlatingCost);
                        }
                        else {
                            product.PlatingName = "Without";
                        }
                        //if(!string.IsNullOrWhiteSpace(entity.Drawing2D))
                        //    product.Upload = Path.Combine(Server.MapPath("~/Content/FileUpload/Drawing"), entity.Drawing2D);
                        if (string.IsNullOrWhiteSpace(entity.Drawing2D))
                            product.Upload2D = "askquestion.jpg";
                        if (string.IsNullOrWhiteSpace(entity.DrawingFinish))
                            product.UploadReal = "askquestion.jpg";
                        product.MaterialUnitPrice = (product.Weight ?? 0) / 1000 * product.MaterialCost;
                        product.ProductBaseCost = product.MaterialUnitPrice + product.MillCost +
                                                  product.ProcessingSalesCost +
                                                  product.SectionCost + product.PlatingCost;
                        model.Add(product);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }

            return model.OrderByDescending(m => m.Active).ThenBy(m => m.CustomerCodeName).ThenBy(m => m.ProductCode).ToList();
        }

        [GridAction]
        public ActionResult SelectProductManagementCnc(int customerId, string productCode) {
            var model = new List<ProductModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var processCnc = vfi.ProductionProcesses.Where(pp => pp.WarehouseId == MyUtilities.Warehouse.Cnc && 
                                                                        pp.IsNecessary);
                    var productIds = processCnc.Select(pp => pp.ProductId).Distinct().ToList();
                    var products = (from p in vfi.Products
                                   where productIds.Contains(p.ProductId) &&
                                   p.Active &&
                                   (customerId == 0 || p.CustomerId == customerId)
                                   select new {
                                       p.ProductId,
                                       p.ProductCode,
                                       p.CustomerId,
                                       CustomerCode = p.Customer.CustomerCode,
                                       p.MillProductivity,
                                   }).ToList();
                    if (!String.IsNullOrWhiteSpace(productCode)) {
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    }
                    foreach (var product in products) {
                        var entity = new ProductModel {
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerId = product.CustomerId,
                            CustomerCode = product.CustomerCode,
                            MillProductivity = product.MillProductivity ?? 0
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductManagementCnc", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m=> m.CustomerCode).ThenBy(m=> m.ProductCode)));
        }

        [GridAction]
        public ActionResult SelectProductManagementProduction2(int customerId, string productCode, int fromIndex) {
            var model = new List<ProductModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var processProduction2 = vfi.ProductionProcesses.Where(pp => pp.WarehouseId == MyUtilities.Warehouse.Production2 &&
                                                                                pp.IsNecessary);
                    var productIds = processProduction2.Select(pp => pp.ProductId).Distinct().ToList();
                    var products = (from p in vfi.Products
                                    where productIds.Contains(p.ProductId) &&
                                    p.Active &&
                                    (customerId == 0 || p.CustomerId == customerId)
                                    select new {
                                        p.ProductId,
                                        p.ProductCode,
                                        p.CustomerId,
                                        CustomerCode = p.Customer.CustomerCode
                                    }).ToList();
                    if (!String.IsNullOrWhiteSpace(productCode)) {
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    }
                    productIds = products.Select(p => p.ProductId).ToList();
                    var productionProcess = from pp in vfi.ProductionProcesses
                                            where productIds.Contains(pp.ProductId) &&
                                            pp.WarehouseId == MyUtilities.Warehouse.Production2 &&
                                            pp.IsNecessary
                                            select pp;
                    productIds = productionProcess.Select(pp => pp.ProductId).Distinct().ToList();
                    var sections = from ps in vfi.ProductionSections
                                   where productIds.Contains(ps.ProductId) &&
                                        ps.Active == true &&
                                        (fromIndex < 0 || ps.SectionIndex >= fromIndex)
                                   orderby ps.SectionIndex
                                   select ps;
                    var production2Warehouses = MyUtilities.Warehouse.GetWarehouseIdProduction2_ALL();
                    var productInvs = from pi in vfi.ProductInventories
                                      where production2Warehouses.Contains(pi.WarehouseId) &&
                                            pi.TotalQty > 0
                                      select new {
                                          pi.ProductId,
                                          pi.TotalQty,
                                      };
                    foreach (var section in sections) {
                        var product = products.FirstOrDefault(p => p.ProductId == section.ProductId);
                        if (product == null) continue;
                        var entity = new ProductModel {
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            CustomerId = product.CustomerId,
                            CustomerCode = product.CustomerCode,
                            SectionName = section.Section.SectionName,
                            Productivity = section.Productivity,
                            Weight = section.Weight,
                            Index = section.SectionIndex
                        };
                        var productInvsById = productInvs.Where(pi => pi.ProductId == entity.ProductId).ToList();
                        entity.TotalQuantity = productInvsById.Sum(pi => pi.TotalQty);
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductManagementProduction2", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ThenBy(m => m.Index)));
        }



        [GridAction]
        public ActionResult SelectProductComparePricing(int customerId, int materialTypeId, string productCode) {
            var model = new List<ProductPricingModel>();
            try {
                if (customerId != -1)
                    model = GetProductComparePricing(customerId,materialTypeId, productCode);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductComparePricing", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductPricingModel> GetProductComparePricing(int customerId,int materialTypeId, string productCode) {
            var model = new List<ProductPricingModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var products = (from p in vfi.Products
                                    where p.Active &&
                                    (customerId == 0 || p.CustomerId == customerId)
                                    select new {
                                        p.ProductId,
                                        p.ProductCode,
                                        p.ProductName,
                                        p.DesignNo,
                                        ProductPrice = p.UnitPrice,
                                        p.Currency,
                                        p.CustomerId,
                                        CustomerCode = p.Customer.CustomerCode,

                                        p.OutDiameterDesign,
                                        p.Length,
                                        p.KnifeCut,
                                        p.Weight,
                                        p.QcWeight,

                                        p.MaterialId,
                                        p.Material,
                                        MaterialTypeId = p.MaterialId != null ? p.Material.MaterialTypeId : 0,
                                        MaterialCode = p.MaterialId != null ? p.Material.MaterialCode : "",
                                        MaterialPrice = p.MaterialId != null ? p.Material.UnitPrice : 0,
                                    }).ToList();
                    if (materialTypeId != 0) {
                        products = products.Where(x => x.MaterialTypeId == materialTypeId).ToList();
                    }
                    if (!String.IsNullOrWhiteSpace(productCode)) {
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    }
                    var exchangeRate = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.ExchangeToVndRate);
                    foreach (var product in products) {
                        var entity = new ProductPricingModel {

                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            ProductDesignNo = product.DesignNo,
                            ProductPrice = product.ProductPrice ?? 0,
                            ProductCurrency = product.Currency,
                            CustomerId = product.CustomerId,
                            CustomerCode = product.CustomerCode,

                            OutDiameterDesign = product.OutDiameterDesign ?? 0,
                            Length = (product.Length + product.KnifeCut) ?? 0,
                            MaterialWeight = product.Weight ?? 0,
                            ProductWeight = product.QcWeight ?? 0,

                            MaterialId = product.MaterialId ?? 0,
                            MaterialCode = product.MaterialCode,
                            MaterialPrice = product.MaterialPrice,
                        };
                        entity.ProductPrice = MyUtilities.Product.ProductVndPrice(entity.ProductPrice, 1, exchangeRate);
                        if (product.MaterialId != null) {
                            entity.MaterialWeight = MyUtilities.Product.GetProductWeight(product.Material.MaterialName,
                              product.Material.OutDiameter,
                              product.Material.InDiameter,
                              product.Length ?? 0,
                              product.KnifeCut ?? 0,
                              product.Material.Shape);
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ToList();
        }

        [GridAction]
        public ActionResult SelectProductManagementPricing(int customerId, string productCode) {
            var model = new List<ProductionPricingModel>();
            try {
                model = GetProductManagementPricing(customerId, productCode);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductManagementPricing", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionPricingModel> GetProductManagementPricing(int customerId, string productCode) {
            var model = new List<ProductionPricingModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var products = (from p in vfi.Products
                                    where p.Active &&
                                    (customerId == 0 || p.CustomerId == customerId)
                                    select new {
                                        p.ProductId,
                                        p.ProductCode,
                                        p.CustomerId,
                                        CustomerCode = p.Customer.CustomerCode
                                    }).ToList();
                    if (!String.IsNullOrWhiteSpace(productCode)) {
                        products = products.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    }
                    var productIds = products.Select(p => p.ProductId).ToList();
                    var productionPricings = from pp in vfi.ProductionPricings
                                             where productIds.Contains(pp.ProductId)
                                             select pp;
                    foreach (var product in products) {
                        var entity = new ProductionPricingModel {
                            CustomerId = product.CustomerId,
                            CustomerCode = product.CustomerCode,
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            ProductionCamesPricing = 0,
                            ProductionCncPricing = 0,
                            Production2Pricing = 0,
                            CncPricing = 0,
                            ProductionTeamDPricing = 0,
                            ProductionTechnicalPricing = 0
                        };
                        var productionPricingById = productionPricings.FirstOrDefault(pp => pp.ProductId == entity.ProductId);
                        if (productionPricingById != null) {
                            entity.Id = productionPricingById.Id;
                            entity.ProductionCamesPricing = productionPricingById.ProductionCamesPricing;
                            entity.ProductionCncPricing = productionPricingById.ProductionCncPricing;
                            entity.Production2Pricing = productionPricingById.Production2Pricing;
                            entity.CncPricing = productionPricingById.CncPricing;
                            entity.ProductionTeamDPricing = productionPricingById.ProductionTeamDPricing;
                            entity.ProductionTechnicalPricing = productionPricingById.ProductionTechnicalPricing;
                            entity.ModifiedDate = productionPricingById.ModifiedDate;
                            entity.ModifiedUser = productionPricingById.ModifiedUser;

                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode).ToList();
        }

        [GridAction]
        public ActionResult UpdateProductionPricing(ProductionPricingModel update, int customerId, string productCode) {
            try {
                using (var vfi = new tammaContext()) {
                    var productionPricingById = vfi.ProductionPricings.FirstOrDefault(pp => pp.ProductId == update.ProductId);
                    if (productionPricingById == null) {
                        productionPricingById = new ProductionPricing {
                            ProductId = update.ProductId,
                            CncPricing = update.CncPricing,
                            ProductionCamesPricing = update.ProductionCamesPricing,
                            ProductionCncPricing = update.ProductionCncPricing,
                            Production2Pricing = update.Production2Pricing,
                            ProductionTeamDPricing = update.ProductionTeamDPricing,
                            ProductionTechnicalPricing = update.ProductionTechnicalPricing,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                        };
                        vfi.ProductionPricings.Add(productionPricingById);
                    }
                    else {
                        productionPricingById.CncPricing = update.CncPricing;
                        productionPricingById.ProductionCamesPricing = update.ProductionCamesPricing;
                        productionPricingById.ProductionCncPricing = update.ProductionCncPricing;
                        productionPricingById.Production2Pricing = update.Production2Pricing;
                        productionPricingById.ProductionTeamDPricing = update.ProductionTeamDPricing;
                        productionPricingById.ProductionTechnicalPricing = update.ProductionTechnicalPricing;
                        productionPricingById.ModifiedDate = DateTime.Now;
                        productionPricingById.ModifiedUser = HttpContext.User.Identity.Name;
                    }
                    vfi.SaveChanges();

                }
                return View(new GridModel(GetProductManagementPricing(customerId, productCode)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionPricing", ex.Message);
            }
            return View(new GridModel(new List<ProductionPricingModel>()));
        }


        [GridAction]
        public ActionResult SelectProduct(bool active, int status, int customerId, string productCode, int productId, string productName) {
            var model = new List<ProductModel>();
            try {
                model = SelectAllProducts(active, status, customerId, productCode, productId, "", productName);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProduct", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProduct(ProductModel newProduct) {
            try {
                using (var vfi = new tammaContext()) {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                                 "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                                 "Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    if (string.IsNullOrWhiteSpace(newProduct.Attachment2D))
                        throw new AggregateException("Lỗi! \n Không thể tạo sản phẩm mới khi chưa có bản vẽ!");
                    //var product = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(newProduct.ProductCode));
                    var product = vfi.Products.FirstOrDefault(p => p.DesignNo.Equals(newProduct.DesignNo));

                    if (product == null) {
                        var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                        if (user == null)
                            throw new AggregateException("Vui lòng đăng nhập lại");
                        //var permisstion =
                        //    vfi.Permissions.FirstOrDefault(
                        //        p =>
                        //        p.FunctionID == PermisstionSpecialModel.EditProductCommon && p.UserID == user.UserId);
                        //if (permisstion != null)
                        //{
                        int customerId2 = 1;
                        try {
                            customerId2 = Convert.ToInt32(newProduct.CustomerCode);
                        }
                        catch (Exception) {
                            customerId2 =
                                vfi.Customers.FirstOrDefault(c => c.CustomerName.Equals(newProduct.CustomerCode))
                                    .CustomerId;
                        }
                        //int materialId = 1;
                        //try
                        //{
                        //    materialId = Convert.ToInt32(newProduct.MaterialCode);
                        //}
                        //catch (Exception)
                        //{
                        //    throw new AggregateException("Lỗi mã nguyên liệu ! Chọn lại nguyên liệu");
                        //}
                        newProduct.CustomerId = customerId2;
                        product = new Vfi.Models.Product() {
                            //import new product
                            ProductName = (newProduct.ProductName+"").Trim(),
                            CustomerId = customerId2,
                            //MaterialId = 1,
                            DesignNo = newProduct.DesignNo,
                            UnitPrice = newProduct.UnitPrice ?? 0,
                            //default weight
                            CncWeight = 0,
                            Production2Weight = 0,
                            HeatTreatmentWeight = 0,
                            SurfaceTreatmentWeight = 0,
                            WaitingPlatingWeight = 0,
                            PlatingWeight = 0,
                            QcWeight = 0,
                            FinishWeight = 0,
                            OutsideProcessWeight = 0,
                            //default design
                            //ProcessingDesign = 1,
                            ProductionFactor = 1,
                            OutDiameterDesign = 0,
                            InDiameterDesign = 0,
                            ProductionWeight = 0,
                            Productivity = 0,
                            ProductionRate = 0,
                            Diameter = newProduct.Diameter ?? 0,
                            Length = newProduct.Length ?? 0,
                            Weight = newProduct.Weight ?? 0,
                            ShapeDesign = "",
                            MaterialNameDesign = "",
                            ForecastsQuality = 0,
                            // default
                            Active = true,
                            IsSelling = false,
                            SaleFactor = 1,
                            Status = (byte)MyUtilities.Product.ProductStatusEnum.Calculating,
                            Diff = 0,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            DiameterTypeDesign = "",
                            OutDiameterTolerance = "",
                            InDiameterTolerance = "",
                            ProcessingCost = 0,
                            NewUpdateDate = DateTime.Now,
                            FinishProcessingDate = DateTime.Now,
                            LastMonthSaleFactor = 1,
                            LastSaleFactor = 1,
                            LastMonthUpdateDate = DateTime.Now,
                            LastUpdateDate = DateTime.Now,
                            KnifeCut = 0,
                            MaterialCost = 0,
                            MillProductivity = 0,
                            DiffRequestInv = 0,
                            QcProductivity = 0,
                            CncProductivity = 0,
                            FinishDesign = false,
                            TaxCode = newProduct.ProductName + " " + newProduct.DesignNo,
                            ProductShape = "",
                            Note = newProduct.Note,
                            //IdentityCode = newProduct.IdentityCode,
                            MaxQuantityInTray = 0,
                            MaxQuantityInTrayRunTime = 0,
                            PackingFee = newProduct.PackingFee,
                        };
                        product.ProductCode = MyUtilities.Product.GetAutoProductCode();
                        if (String.IsNullOrWhiteSpace(newProduct.Currency)) {
                            product.Currency = MyUtilities.Product.DetectCurrency(newProduct.UnitPrice ?? 0);
                        }
                        else {
                            product.Currency = newProduct.Currency;
                        }
                        //var material = vfi.Materials.FirstOrDefault();
                        //product.ShapeDesign = (material.Shape + "").Trim();
                        //product.OutDiameterDesign = material.OutDiameter ?? 0;
                        //product.InDiameterDesign = material.InDiameter ?? 0;
                        //product.MaterialNameDesign = (material.MaterialName + "").Trim();
                        if (!string.IsNullOrWhiteSpace(newProduct.Attachment2D))
                            product.Drawing2D = newProduct.Attachment2D;
                        else
                            product.Drawing2D = "askquestion.jpg";
                        product.DrawingFinish = product.Drawing2D;
                        vfi.Products.Add(product);
                        vfi.SaveChanges();
                        product.IdentityCode = String.Format("{0:0000}", product.ProductId);
                        var autoProcesses = vfi.Warehouses.Where(x => x.Active && x.AutoGenerateProcess == true)
                                                        .Select(x => new { x.WarehouseId, x.Idx })
                                                        .ToList();
                        foreach (var warehouse in autoProcesses) {
                            var process = new ProductionProcess {
                                WarehouseId = warehouse.WarehouseId,
                                ProcessIndex = warehouse.Idx,
                                IsAlert = true,
                                IsNecessary = true,
                                ProductId = product.ProductId,
                                Product = product,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                            };
                            vfi.ProductionProcesses.Add(process);
                            vfi.SaveChanges();
                        }
                        vfi.SaveChanges();
                        //var processProduction1 = new ProductionProcess {
                        //    WarehouseId = MyUtilities.Warehouse.Production1,
                        //    ProcessIndex = 1,
                        //    IsAlert = true,
                        //    IsNecessary = true,
                        //    ProductId = product.ProductId,
                        //    Product = product,
                        //    ModifiedDate = DateTime.Now,
                        //    ModifiedUser = HttpContext.User.Identity.Name,
                        //};
                        //vfi.ProductionProcesses.Add(processProduction1);
                        //var processQc = new ProductionProcess {
                        //    WarehouseId = MyUtilities.Warehouse.QcA,
                        //    ProcessIndex = 7,
                        //    IsAlert = true,
                        //    IsNecessary = true,
                        //    ProductId = product.ProductId,
                        //    Product = product,
                        //    ModifiedDate = DateTime.Now,
                        //    ModifiedUser = HttpContext.User.Identity.Name,
                        //};
                        //vfi.ProductionProcesses.Add(processQc);
                        //var processPacking = new ProductionProcess {
                        //    WarehouseId = MyUtilities.Warehouse.Packing,
                        //    ProcessIndex = 8,
                        //    IsAlert = true,
                        //    IsNecessary = true,
                        //    ProductId = product.ProductId,
                        //    Product = product,
                        //    ModifiedDate = DateTime.Now,
                        //    ModifiedUser = HttpContext.User.Identity.Name,
                        //};
                        //vfi.ProductionProcesses.Add(processPacking);
                        //var processFinish = new ProductionProcess {
                        //    WarehouseId = MyUtilities.Warehouse.Finish,
                        //    ProcessIndex = 9,
                        //    IsAlert = true,
                        //    IsNecessary = true,
                        //    ProductId = product.ProductId,
                        //    Product = product,
                        //    ModifiedDate = DateTime.Now,
                        //    ModifiedUser = HttpContext.User.Identity.Name,
                        //};
                        //vfi.ProductionProcesses.Add(processFinish);
                        //vfi.SaveChanges();
                        //}
                        //else
                        //{
                        //    throw new AggregateException("Không có quyền thêm sản phẩm mới!\n Liên hệ Admin!");
                        //}
                        return View(new GridModel(SelectAllProducts(false, 0, 0, product.ProductCode, 0, "","")));
                    }
                    else {
                        throw new AggregateException("Mã sản phẩm của khách hàng đã tồn tại ! \n Vui lòng chọn mã khác");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProduct", ex.Message);
            }
            return View(new GridModel(SelectAllProducts(false, (byte)MyUtilities.Product.ProductStatusEnum.Calculating, 0, "", 0, "","")));
        }


        public ActionResult DuplicateProduct(int productId) {
            try {
                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(x => x.ProductId == productId);
                    if (product == null) {
                        return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NotFound, "Không tìm thấy", null));
                    }
                    var newProduct = new Product() {
                        //import new product
                        ProductName = (product.ProductName + "").Trim(),
                        CustomerId = product.CustomerId,
                        //MaterialId = 1,
                        DesignNo = product.DesignNo,
                        UnitPrice = product.UnitPrice ?? 0,
                        //default weight
                        CncWeight = product.CncWeight,
                        Production2Weight = product.Production2Weight,
                        HeatTreatmentWeight = product.HeatTreatmentWeight,
                        SurfaceTreatmentWeight = product.SurfaceTreatmentWeight,
                        WaitingPlatingWeight = product.WaitingPlatingWeight,
                        PlatingWeight = product.PlatingWeight,
                        QcWeight = product.QcWeight,
                        FinishWeight = product.FinishWeight,
                        OutsideProcessWeight = product.OutsideProcessWeight,
                        //default design
                        ProcessingDesign = product.ProcessingDesign,
                        ProductionFactor = 1,
                        OutDiameterDesign = product.OutDiameterDesign,
                        InDiameterDesign = product.InDiameterDesign,
                        ProductionWeight = product.ProductionWeight,
                        Productivity = product.Productivity,
                        ProductionRate = product.ProductionRate,
                        Diameter = product.Diameter ?? 0,
                        Length = product.Length ?? 0,
                        Weight = product.Weight ?? 0,
                        ShapeDesign = product.ShapeDesign,
                        MaterialNameDesign = product.MaterialNameDesign,
                        ForecastsQuality = 0,
                        // default
                        Active = true,
                        IsSelling = false,
                        SaleFactor = 1,
                        Status = (byte)MyUtilities.Product.ProductStatusEnum.Calculating,
                        Diff = 0,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        DiameterTypeDesign = product.DiameterTypeDesign,
                        OutDiameterTolerance = product.OutDiameterTolerance,
                        InDiameterTolerance = product.InDiameterTolerance,
                        ProcessingCost = product.ProcessingCost,
                        NewUpdateDate = DateTime.Now,
                        FinishProcessingDate = DateTime.Now,
                        LastMonthSaleFactor = 1,
                        LastSaleFactor = 1,
                        LastMonthUpdateDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        KnifeCut = product.KnifeCut,
                        MaterialCost = product.MaterialCost,
                        MillProductivity = product.MillProductivity,
                        DiffRequestInv = product.DiffRequestInv,
                        QcProductivity = product.QcProductivity,
                        CncProductivity = product.CncProductivity,
                        FinishDesign = false,
                        TaxCode = product.ProductName + " " + product.DesignNo,
                        ProductShape = product.ProductShape,
                        Note = product.Note,
                        MaxQuantityInTray = product.MaxQuantityInTray,
                        MaxQuantityInTrayRunTime = product.MaxQuantityInTrayRunTime,
                        MaterialId = product.MaterialId,
                        Currency = product.Currency,
                        Drawing2D = product.Drawing2D,
                        DrawingFinish = product.DrawingFinish,
                        IdentityCode = "",
                        ProductCode = MyUtilities.Product.GetAutoProductCode(),
                        MachineFunction = product.MachineFunction,
                    };
                    // clone process
                    foreach (var process in product.ProductionProcesses.Where(x => x.IsAlert && x.IsNecessary).OrderBy(x => x.ProcessIndex)) {
                        newProduct.ProductionProcesses.Add(new ProductionProcess {
                            IsNecessary = process.IsNecessary,
                            IsAlert = process.IsAlert,
                            ModifiedDate = newProduct.ModifiedDate,
                            ModifiedUser = newProduct.ModifiedUser,
                            ProcessIndex = process.ProcessIndex,
                            WarehouseId = process.WarehouseId,
                        });
                        if (process.Warehouse.IsProduction2) {
                            foreach (var production in product.ProductionSections.Where(x => x.IsMainProcess && x.Active).OrderBy(x => x.SectionIndex)) {
                                newProduct.ProductionSections.Add(new ProductionSection {
                                    SectionId = production.SectionId,
                                    IsMainProcess = production.IsMainProcess,
                                    Active = production.Active,
                                    InsertDate = newProduct.ModifiedDate,
                                    InsertUser = newProduct.ModifiedUser,
                                    SectionIndex = production.SectionIndex,
                                    MachineId = production.MachineId,
                                    Productivity = production.Productivity,
                                    SectionCost = production.SectionCost
                                });
                            }
                        }
                        else if (process.Warehouse.IsHeatTreatment) {
                            foreach (var production in product.ProductionHeatTreatments.Where(x => x.Active)) {
                                newProduct.ProductionHeatTreatments.Add(new ProductionHeatTreatment {
                                    ModifiedDate = newProduct.ModifiedDate,
                                    ModifiedUser = newProduct.ModifiedUser,
                                    Active = production.Active,
                                    MachineId = production.MachineId,
                                    Name = production.Name,
                                    Rate = production.Rate,
                                    Section = production.Section,
                                    Stiffness = production.Stiffness,
                                    Temperature = production.Temperature,
                                    Timing = production.Timing,
                                });
                            }
                        }
                        else if (process.Warehouse.IsPolish) {
                            foreach (var production in product.ProductionPolishes.Where(x => x.Active)) {
                                newProduct.ProductionPolishes.Add(new ProductionPolish {
                                    ModifiedDate = newProduct.ModifiedDate,
                                    ModifiedUser = newProduct.ModifiedUser,
                                    Active = production.Active,
                                    MachineId = production.MachineId,
                                    Name = production.Name,
                                    Rate = production.Rate,
                                    Section = production.Section,
                                    Timing = production.Timing,
                                    Rock = production.Rock,
                                    Using = production.Using,
                                });
                            }
                        }
                        else if (process.Warehouse.IsPlating) {
                            foreach (var production in product.ProductionPlatings.Where(x => x.Active && x.IsMainProcess)) {
                                newProduct.ProductionPlatings.Add(new ProductionPlating {
                                    ModifiedDate = newProduct.ModifiedDate,
                                    ModifiedUser = newProduct.ModifiedUser,
                                    Active = production.Active,
                                    IsMainProcess = production.IsMainProcess,
                                    InsertDate = newProduct.ModifiedDate,
                                    InserUser = newProduct.ModifiedUser,
                                    PlatingIndex = production.PlatingIndex,
                                    PlatingCost = production.PlatingCost,
                                    PlatingDay = production.PlatingDay,
                                    PlatingName = production.PlatingName,
                                    SaltSprayTime = production.SaltSprayTime,
                                    Thickness = production.Thickness,
                                });
                            }
                        }
                        else if (process.Warehouse.IsPacking) {
                            foreach (var production in product.ProductionFuels.Where(x => x.Active)) {
                                newProduct.ProductionFuels.Add(new ProductionFuel {
                                    ModifiedDate = newProduct.ModifiedDate,
                                    ModifiedUser = newProduct.ModifiedUser,
                                    Active = production.Active,
                                    CrossWeight = production.CrossWeight,
                                    CrossWeight2 = production.CrossWeight2,
                                    Fuel2Id = production.Fuel2Id,
                                    FuelId = production.FuelId,
                                    Priority = production.Priority,
                                    Quota = production.Quota,
                                    Quota2 = production.Quota2,
                                });
                            }
                        }
                    }
                    // clone production
                    {
                        foreach (var production in product.ProductionTools.Where(x => x.Active)) {
                            newProduct.ProductionTools.Add(new ProductionTool {
                                ModifiedDate = newProduct.ModifiedDate,
                                ModifiedUser = newProduct.ModifiedUser,
                                Active = production.Active,
                                ProcessWarehouseId = production.ProcessWarehouseId,
                                Quota = production.Quota,
                                ToolId = production.ToolId,
                                ToolIndex = production.ToolIndex,
                                ToolLocation = production.ToolLocation,
                                UseNumber = production.UseNumber,
                                NamingToolId = production.NamingToolId,
                            });
                        }
                    }
                    {
                        foreach (var production in product.ProductionMaterials.Where(x => x.Active)) {
                            newProduct.ProductionMaterials.Add(new ProductionMaterial {
                                ModifiedDate = newProduct.ModifiedDate,
                                ModifiedUser = newProduct.ModifiedUser,
                                Active = production.Active,
                                MaterialId = production.MaterialId,
                                Priority = production.Priority,
                                UnitWeightByMaterial = production.UnitWeightByMaterial,
                                Note = production.Note
                            });
                        }
                    }
                    {
                        foreach (var testing in product.ProductionTestings) {
                            var newTesting = new ProductionTesting {
                                ModifiedDate = newProduct.ModifiedDate,
                                ModifiedUser = newProduct.ModifiedUser,
                                Note = testing.Note,
                                WarehouseId = testing.WarehouseId,
                            };
                            foreach (var production in testing.ProductionTestingDetails.Where(x => x.Active)) {
                                newTesting.ProductionTestingDetails.Add(new ProductionTestingDetail {
                                    ModifiedDate = newProduct.ModifiedDate,
                                    ModifiedUser = newProduct.ModifiedUser,
                                    Active = production.Active,
                                    MaxNumber = production.MaxNumber,
                                    MinNumber = production.MinNumber,
                                    TestingCode = production.TestingCode,
                                    TestingName = production.TestingName,
                                    TestRate = production.TestRate,
                                });

                            }
                            product.ProductionTestings.Add(newTesting);
                        }
                    }
                    vfi.Products.Add(newProduct);
                    var saved = vfi.SaveChanges();

                    product.IdentityCode = String.Format("{0:0000}", product.ProductId);
                    saved += vfi.SaveChanges();

                    return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "", saved));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult(
                    (int)MyUtilities.Monitor.ErrorCode.Exception,
                    MyUtilities.MySystem.FetchExceptionMessage(ex),
                    null));
            }
            //return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoThing, "", null));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProduct(ProductModel updateProduct) {
            return View(new GridModel(SelectAllProducts(false, updateProduct.StatusFilter, 0, "", 0, "", "")));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductManagementInv(ProductModel updateProduct, int customerId, string productCode) {
            using (var vfi = new tammaContext()) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                                 "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                                 "Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null)
                        throw new AggregateException("Vui lòng đăng nhập lại");
                    var product =
                        vfi.Products.FirstOrDefault(p => p.ProductId == updateProduct.ProductId);
                    if (product == null)
                        throw new AggregateException("Lỗi hệ thống");
                    product.ProductionRate = updateProduct.ProductionRate;
                    product.ProductionWeight = updateProduct.ProductionWeight;
                    product.CncWeight = updateProduct.CncWeight;
                    product.Production2Weight = updateProduct.Production2Weight;
                    product.HeatTreatmentWeight = updateProduct.HeatTreatmentWeight;
                    product.SurfaceTreatmentWeight = updateProduct.SurfaceTreatmentWeight;
                    product.WaitingPlatingWeight = updateProduct.WaitingPlatingWeight;
                    if (updateProduct.OutsideProcessWeight > 0) {
                        product.OutsideProcessWeight = updateProduct.OutsideProcessWeight;
                    }
                    else { 
                        product.OutsideProcessWeight = product.WaitingPlatingWeight; 
                    }
                    product.PlatingWeight = updateProduct.PlatingWeight;
                    product.QcWeight = updateProduct.QcWeight;
                    //product.FinishWeight = updateProduct.FinishWeight; 
                    if (product.MaxQuantityInTray <= 0 && product.Productivity > 0 && product.ProductionWeight > 0) {
                        var maxWeight = 20000;
                        var maxTime = 1.5 * 24 * 3600;
                        var quantityWeight = 0;
                        if (product.ProductionWeight > 0) {
                            quantityWeight = MyUtilities.Function.RoundDown(maxWeight / product.ProductionWeight.Value);
                        }
                        var quantityTime = 0;
                        if (product.Productivity > 0) {
                            quantityTime = MyUtilities.Function.RoundDown(maxTime / product.Productivity.Value);
                        }
                        product.MaxQuantityInTray = quantityWeight > quantityTime ? quantityTime : quantityWeight;
                        var packingProductivity = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.PackingProductivity);
                        product.MaxQuantityInTrayRunTime = _productionController.CalculateProductionWorkOrderRunTime(product, packingProductivity);
                    }
                    vfi.SaveChanges();
                    if (string.IsNullOrWhiteSpace(product.IdentityCode)) {
                        product.IdentityCode = String.Format("{0:0000}", product.ProductId);
                        vfi.SaveChanges();
                    }
                    return View(new GridModel(SelectAllProducts(false, 0, customerId, productCode, 0, "", "")));
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProductManagementInv", MyUtilities.MySystem.FetchExceptionMessage(ex));
                }
                return View(new GridModel(new List<ProductModel>()));
            }
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductManagementProduction(ProductModel updateProduct) {
            using (var vfi = new tammaContext()) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                                 "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                                 "Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null)
                        throw new AggregateException("Vui lòng đăng nhập lại");
                    var product =
                        vfi.Products.FirstOrDefault(p => p.ProductId == updateProduct.ProductId);
                    if (product == null)
                        throw new AggregateException("Lỗi hệ thống");
                    product.ForecastsQuality = updateProduct.ForecastsQuality;
                    product.MachineFunction = updateProduct.MachineFunction;
                    vfi.SaveChanges();
                    product.FinishDesign = MyUtilities.Product.CheckDesign(product.ProductId);
                    vfi.SaveChanges();
                    return View(new GridModel(SelectAllProducts(false, product.Status.Value,
                                                                product.CustomerId, product.ProductCode, 0, "", "")));
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProductManagementProduction", ex.Message);
                }
            }
            return View(new GridModel(SelectAllProducts(false, updateProduct.StatusFilter, -1, "", 0, "", "")));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductManagementTax(ProductModel updateProduct) {
            using (var vfi = new tammaContext()) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                                 "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                                 "Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null)
                        throw new AggregateException("Vui lòng đăng nhập lại");
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == updateProduct.ProductId);
                    if (product == null)
                        throw new AggregateException("Lỗi hệ thống");
                    product.TaxCode = updateProduct.TaxCode;
                    vfi.SaveChanges();
                    return View(new GridModel(SelectAllProducts(false, product.Status.Value,
                                                                product.CustomerId, product.ProductCode, 0, "", "")));
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProductManagementProduction", ex.Message);
                }
            }
            return View(new GridModel(SelectAllProducts(false, updateProduct.StatusFilter, -1, "", 0, "", "")));
        }
        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductManagementSales(ProductModel updateProduct, int customerId, int status, string productCodeFilter) {
            using (var vfi = new tammaContext()) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                                 "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                                 "Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null)
                        throw new AggregateException("Vui lòng đăng nhập lại");
                    var product =
                        vfi.Products.FirstOrDefault(
                            p =>
                            p.ProductCode.Equals(updateProduct.ProductCode) && p.ProductId != updateProduct.ProductId);
                    //var products = vfi.Products.Where(p => p.ProductCode.Equals(updateProduct.ProductCode)).ToList();
                    if (product == null) {
                        product = vfi.Products.FirstOrDefault(p => p.ProductId == updateProduct.ProductId);
                        if (product == null)
                            throw new AggregateException("Lỗi hệ thống");

                        int customerId2 = 1;
                        try {
                            customerId2 = Convert.ToInt32(updateProduct.CustomerCode);
                        }
                        catch (Exception) {
                            try {
                                customerId2 =
                                    vfi.Customers.FirstOrDefault(c => c.CustomerName.Equals(updateProduct.CustomerCode))
                                       .CustomerId;
                            }
                            catch (NullReferenceException) {
                                customerId2 = product.CustomerId;
                            }
                        }
                        //int materialId = 1;
                        //try
                        //{
                        //    materialId = Convert.ToInt32(updateProduct.MaterialCode);
                        //}
                        //catch (Exception)
                        //{
                        //    materialId =
                        //        vfi.Materials.FirstOrDefault(a => a.MaterialCode.Equals(updateProduct.MaterialCode))
                        //           .MaterialId;
                        //}
                        //var status = 1;
                        //try
                        //{
                        //    status = Convert.ToInt32(updateProduct.StatusName);
                        //}
                        //catch (FormatException)
                        //{
                        //}
                        //product.Status = (byte)status;
                        //kinh doanh
                        updateProduct.ProductCode = updateProduct.ProductCode.Trim();
                        if (!product.ProductCode.Equals(updateProduct.ProductCode) ||
                            product.UnitPrice != updateProduct.UnitPrice) {
                            if (!product.ProductCode.Equals(updateProduct.ProductCode)) {
                                if (!MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                                    MyUtilities.UserRole.EditProductCode)) {
                                    throw new AggregateException(
                                        "Bạn không có quyền thay đổi mã sản phẩm \n Vui lòng liên hệ admin");
                                }
                            }
                            var productChange = new ProductChange {
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ProductId = product.ProductId,
                                OldProductCode = product.ProductCode,
                                NewProductCode = updateProduct.ProductCode,
                                OldPrice = product.UnitPrice ?? 0,
                                NewPrice = updateProduct.UnitPrice ?? 0
                            };
                            vfi.ProductChanges.Add(productChange);
                            product.ProductCode = updateProduct.ProductCode;
                        }
                        product.ProductName = updateProduct.ProductName;
                        product.CustomerId = customerId2;
                        product.PackingFee = updateProduct.PackingFee;
                        if (!product.DesignNo.Equals(updateProduct.DesignNo)) {
                            var elseProduct =
                                vfi.Products.FirstOrDefault(p => p.DesignNo.Equals(updateProduct.DesignNo) && p.Active);
                            if (elseProduct != null)
                                throw new AggregateException("Lỗi! Bị trùng mã sản phẩm của khách hàng! Vui lòng đổi lại mã khác");
                            product.DesignNo = updateProduct.DesignNo;
                        }
                        product.UnitPrice = updateProduct.UnitPrice ?? 0;
                        if (String.IsNullOrWhiteSpace(updateProduct.Currency))
                            product.Currency = MyUtilities.Product.DetectCurrency(product.UnitPrice ?? 0);
                        else {
                            product.Currency = updateProduct.Currency;
                        }
                        if (!updateProduct.Active) {
                            var warehouses = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                            var productInv =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi =>
                                        pi.ProductId == product.ProductId &&
                                        warehouses.Contains(pi.WarehouseId) &&
                                        pi.TotalQty > 0);
                            if (productInv != null)
                                throw new AggregateException("Lỗi! Yêu cầu xử lý hết tồn kho trước khi tắt kích hoạt!");
                        }
                        product.Active = updateProduct.Active;

                        if (string.IsNullOrWhiteSpace(updateProduct.ModifiedUser))
                            product.ModifiedUser = HttpContext.User.Identity.Name;

                        if (updateProduct.ModifiedDate == null)
                            product.ModifiedDate = DateTime.Now;

                        if (!string.IsNullOrWhiteSpace(updateProduct.Attachment2D)) {
                            product.Drawing2D = updateProduct.Attachment2D;
                            product.UploadUser = HttpContext.User.Identity.Name;
                            product.UploadDate = DateTime.Now;
                        }
                        else if (string.IsNullOrWhiteSpace(updateProduct.Attachment2D) &&
                                 string.IsNullOrWhiteSpace(product.Drawing2D))
                            product.Drawing2D = "askquestion.jpg";

                        if (!string.IsNullOrWhiteSpace(updateProduct.AttachmentReal)) {
                            product.DrawingFinish = updateProduct.AttachmentReal;
                            product.UploadUser = HttpContext.User.Identity.Name;
                            product.UploadDate = DateTime.Now;
                        }
                        else if (string.IsNullOrWhiteSpace(updateProduct.AttachmentReal) &&
                                 string.IsNullOrWhiteSpace(product.DrawingFinish))
                            product.DrawingFinish = "askquestion.jpg";

                        product.Note = updateProduct.Note;

                        vfi.SaveChanges();
                        //return
                        //    View(
                        //        new GridModel(SelectAllProducts(false, status,
                        //                                        customerId,
                        //                                        productCode,0)));
                    }
                    else {
                        throw new AggregateException("Mã sản phẩm đã tồn tại ! \n Vui lòng chọn mã khác");
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProductManagementSales", ex.Message);
                }
            }
            return View(new GridModel(SelectAllProducts(false, status, customerId, productCodeFilter, 0, "", "")));
        }

        [HttpPost]
        public ActionResult GetProductPlatingCode(int productId) {
            try {

                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null)
                        return Json("9");
                    if (!product.ProductionPlatings.Any())
                        return Json("8");
                    var plating = (from pp in vfi.ProductionPlatings
                                   where pp.ProductId == product.ProductId
                                         && pp.Active
                                   orderby pp.PlatingIndex
                                   select pp).FirstOrDefault();
                    //var plating = vfi.ProductionPlatings.FirstOrDefault(pp => pp.PlatingId == platingId && pp.Active);
                    var lastExport = (from pd in vfi.PlatingFormDetails
                                      orderby pd.PlatingForm.CreateDate descending
                                      where pd.PlatingForm.Status != (byte)MyUtilities.Transaction.Status.Cancel
                                            && pd.PlatingForm.Status != (byte)MyUtilities.Transaction.Status.Open
                                            && pd.ProductId == productId
                                            && pd.PlatingCode.Equals(plating.PlatingName)
                                            && pd.UnitPrice != 0
                                      select pd).FirstOrDefault();
                    var detail = new PlatingDetailModel {
                        PlatingId = plating.PlatingId,
                        PlatingCode = plating.PlatingName,
                    };
                    if (lastExport != null) {
                        detail.Thickness = lastExport.Thickness;
                        detail.SaltSprayTime = lastExport.SaltSprayTime;
                        detail.SpecialRequest = lastExport.SpecialRequest;
                        detail.Sample = lastExport.Sample;
                        detail.TestingEquipment = lastExport.TestingEquipment;
                        detail.Unit = lastExport.Unit;
                        detail.UnitPrice = lastExport.UnitPrice;
                    }
                    return Json(new object[]
                        {
                            detail.PlatingId,
                            detail.PlatingCode,
                            
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
                ModelState.AddModelError("GetProductPlatingCode", ex.Message);
            }
            return Json("0");
        }

        [GridAction]
        public ActionResult SelectRealProductionById(int productId) {
            var model = new List<TrackUpMachineModel>();
            try {
                using (var vfi = new tammaContext()) {

                    var realProduction = from rp in vfi.RealProductions
                                         where
                                             rp.ProductId == productId &&
                                             rp.TrackUpMachine.Status == (byte)MyUtilities.Transaction.Status.Approved
                                         orderby rp.Machine.MachineName
                                         select new {
                                             rp.Machine,
                                             rp.TrackUpMachine
                                         };
                    foreach (var production in realProduction) {
                        var entity = new TrackUpMachineModel() {
                            MachineName = production.Machine.MachineName,
                            RealRate = production.TrackUpMachine.RealRate,
                            RealProductivity = production.TrackUpMachine.RealProductivity,
                            KnifeCut = production.TrackUpMachine.KnifeCut,
                            DeliveryDate = production.TrackUpMachine.DeliveryDate ?? production.TrackUpMachine.StartDate,
                            DeliveryEmployee = production.TrackUpMachine.DeliveryEmployee,
                            Phase = production.TrackUpMachine.Phase,
                            ReceiveEmployee = production.TrackUpMachine.ReceiveEmployee,
                            RoundPerMinute = production.TrackUpMachine.RoundPerMinute,
                            WorkPiece = production.TrackUpMachine.WorkPiece,
                            Quantity = production.TrackUpMachine.Quantity,
                            EndDate = production.TrackUpMachine.EndDate,
                            Note = production.TrackUpMachine.Note,
                            StatusName = production.Machine.ProcessingType.TypeName,
                        };
                        if (production.TrackUpMachine.Material != null) {
                            entity.MaterialCode = production.TrackUpMachine.Material.MaterialCode;
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectRealProductionById", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectFirstTrackUpProductionById(int productId) {
            var model = new List<TrackUpMachineModel>();
            try {
                using (var vfi = new tammaContext()) {

                    var realProduction = (from rp in vfi.RealProductions
                                          where
                                              rp.ProductId == productId &&
                                              rp.TrackUpMachine.Status == (byte)MyUtilities.Transaction.Status.Approved
                                          orderby rp.Machine.MachineName
                                          select new {
                                              rp.Machine,
                                              rp.TrackUpMachine
                                          }).ToList();

                    var allTrackUpMachines = (from x in vfi.TrackUpMachines
                                              where x.ProductId == productId
                                              select x).ToList();

                    foreach (var production in realProduction) {
                        var track = production.TrackUpMachine;
                        var lastDiffTrack = allTrackUpMachines.Where(x => x.MachineId == production.Machine.MachineId
                            && x.MaterialId != production.TrackUpMachine.MaterialId
                            && x.TrackId != production.TrackUpMachine.TrackId)
                            .OrderByDescending(x => x.DeliveryDate)
                            .FirstOrDefault();
                        if (lastDiffTrack != null) {
                            var lastTrack = allTrackUpMachines.Where(x => x.TrackId > lastDiffTrack.TrackId
                            && x.MachineId == production.Machine.MachineId
                            && x.MaterialId == production.TrackUpMachine.MaterialId)
                                .OrderBy(x => x.DeliveryDate)
                                .FirstOrDefault();
                            if (lastTrack != null) {
                                track = lastTrack;
                            }
                        }
                        else {
                            var lastTrack = allTrackUpMachines.Where(x => x.MachineId == production.Machine.MachineId
                            && x.MaterialId == production.TrackUpMachine.MaterialId)
                                .OrderBy(x => x.DeliveryDate)
                                .FirstOrDefault();
                            if (lastTrack != null) {
                                track = lastTrack;
                            }
                        }
                        var entity = new TrackUpMachineModel() {
                            MachineName = production.Machine.MachineName,
                            RealRate = track.RealRate,
                            RealProductivity = track.RealProductivity,
                            KnifeCut = track.KnifeCut,
                            DeliveryDate = track.DeliveryDate ?? track.StartDate,
                            DeliveryEmployee = track.DeliveryEmployee,
                            Phase = track.Phase,
                            ReceiveEmployee = track.ReceiveEmployee,
                            RoundPerMinute = track.RoundPerMinute,
                            WorkPiece = track.WorkPiece,
                            Quantity = track.Quantity,
                            EndDate = track.EndDate,
                            Note = track.Note,
                            StatusName = production.Machine.ProcessingType.TypeName,
                        };
                        if (production.TrackUpMachine.Material != null) {
                            entity.MaterialCode = track.Material.MaterialCode;
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectRealProductionById", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectTrackUpMachineById(int productId) {
            var model = new List<TrackUpMachineModel>();
            try {
                using (var vfi = new tammaContext()) {

                    var tracks = from tm in vfi.TrackUpMachines
                                 where
                                     tm.ProductId == productId &&
                                     tm.Status == (byte)MyUtilities.Transaction.Status.Approved
                                 orderby tm.DeliveryDate descending
                                 select new {
                                     tm.TrackId,
                                     tm.MachineId,
                                     tm.Machine,
                                     tm.ProductId,
                                     tm.Product,
                                     tm.RealProductivity,
                                     tm.RealRate,
                                     DeliveryDate = tm.DeliveryDate ?? tm.StartDate,
                                     tm.KnifeCut,
                                     tm.Phase,
                                     tm.DeliveryEmployee,
                                     tm.ReceiveEmployee,
                                     tm.RoundPerMinute,
                                     tm.WorkPiece,
                                     tm.Note,
                                 };
                    foreach (var track in tracks) {
                        var entity = new TrackUpMachineModel {
                            TrackId = track.TrackId,
                            MachineId = track.MachineId,
                            MachineName = track.Machine.MachineName,
                            ProductId = track.ProductId,
                            ProductCode = track.Product.ProductCode,
                            Productivity = track.Product.Productivity ?? 0,
                            ProductRate = track.Product.ProductionRate ?? 0,
                            DeliveryDate = track.DeliveryDate,
                            RealProductivity = track.RealProductivity,
                            RealRate = track.RealRate,
                            KnifeCut = track.KnifeCut,
                            DeliveryEmployee = track.DeliveryEmployee,
                            Phase = track.Phase,
                            ReceiveEmployee = track.ReceiveEmployee,
                            WorkPiece = track.WorkPiece,
                            RoundPerMinute = track.RoundPerMinute,
                            Note = track.Note,
                            StatusColor = 1,
                            PermisstionType = 1,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectRealProductionById", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        public ActionResult CheckUnitPrice(int productId) {
            try {
                //var intRevisionNumber = Convert.ToInt32(revisionNumber);
                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var unitPrice = vfi.Products.FirstOrDefault(p => p.ProductId == productId).UnitPrice;
                    return Json(unitPrice);
                }
            }
            catch (FormatException) {
                return Json(0);
            }
        }

        [HttpPost]
        public ActionResult GetFinishWeightById(int productId) {
            try {
                //var intRevisionNumber = Convert.ToInt32(revisionNumber);
                using (var vfi = new tammaContext()) {
                    var finishWeight = 0.0;
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product != null)
                        finishWeight = product.QcWeight ?? 0.0;

                    var total = 0.0;
                    var productInv =
                        vfi.ProductInventories.Where(
                            p => p.ProductId == productId && p.WarehouseId == MyUtilities.Warehouse.Finish);
                    if (productInv.Any())
                        total = productInv.Sum(pi => pi.TotalQty);

                    return Json(new object[]
                    {
                        finishWeight,
                        total
                    });
                }
            }
            catch (FormatException) {
                return Json(0);
            }
        }
        [HttpPost]
        public ActionResult GetFinishWeight(int productInvId) {
            try {
                //var intRevisionNumber = Convert.ToInt32(revisionNumber);
                using (var vfi = new tammaContext()) {
                    var finishWeight = 0.0;
                    var total = 0.0;
                    var productInv = vfi.ProductInventories.FirstOrDefault(p => p.ProductInventoryId == productInvId);
                    if (productInv != null) {
                        finishWeight = productInv.Product.QcWeight ?? 0.0;
                        total = productInv.TotalQty;
                    }

                    return Json(new object[]
                    {
                        finishWeight,
                        total
                    });
                }
            }
            catch (FormatException) {
                return Json(0);
            }
        }
        private ImageCodecInfo GetEncoder(ImageFormat format) {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs) {
                if (codec.FormatID == format.Guid) {
                    return codec;
                }
            }
            return null;
        }

        [HttpPost]
        public ActionResult Save(IEnumerable<HttpPostedFileBase> Attachment2D, IEnumerable<HttpPostedFileBase> AttachmentReal) {
            // The Name of the Upload component is "attachments"       
            try {
                var attachments = new List<HttpPostedFileBase>();
                if (Attachment2D != null && Attachment2D.Any()) {
                    attachments.AddRange(Attachment2D.ToList());
                }
                if (AttachmentReal != null && AttachmentReal.Any()) {
                    attachments.AddRange(AttachmentReal.ToList());
                }
                if (attachments.Any()) {
                    foreach (var file in attachments) {
                        // Some browsers send file names with full path. We only care about the file name.
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName == null) continue;
                        var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/Drawing"), fileName);
                        // giam kich thuoc
                        Image bm = Image.FromStream(file.InputStream);
                        var designWidth = 3000.0;
                        var designHeight = 1500.0;
                        var ratioW = designWidth / (double)bm.Width;
                        var ratioH = designHeight / (double)bm.Height;
                        var ratio = ratioH < ratioW ? ratioH : ratioW;
                        var newWidth = Convert.ToInt32(bm.Width * ratio);
                        var newHeight = Convert.ToInt32(bm.Height * ratio);
                        bm = MyUtilities.Function.ResizeBitmap((Bitmap)bm, newWidth, newHeight);
                        bm.Save(destinationPath, bm.RawFormat);

                        //giam chat luong hinh anh
                        //ImageCodecInfo jpgEncoder;
                        //switch (fileName.Split('.')[1].ToLower())
                        //{
                        //    case "png":
                        //        jpgEncoder = GetEncoder(ImageFormat.Png);
                        //        break;
                        //    default:
                        //        jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                        //        break;
                        //}
                        ////Encoder myEncoder = Encoder.Quality;
                        //var myEncoderParameters = new EncoderParameters(1);
                        //var myEncoderParameter = new EncoderParameter(Encoder.Quality, 50L);
                        //myEncoderParameters.Param[0] = myEncoderParameter;
                        //bm.Save(destinationPath, jpgEncoder, myEncoderParameters);

                        //save cu
                        //byte[] fileBytes = new byte[file.ContentLength];
                        //file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        //System.IO.File.WriteAllBytes(destinationPath, fileBytes);
                    }
                    return Json("Upload thành công !");
                }
                else {
                    throw new AggregateException("attachments null");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UploadSave", ex.Message);
                return Json(ex.Message);
            }
            // Redirect to a view showing the result of the form submissSaveion.    
            //return Json("False");
        }

        [HttpPost]
        public ActionResult SaveProductImg(IEnumerable<HttpPostedFileBase> ProductImg) {
            // The Name of the Upload component is "attachments"       
            try {
                //var attachments = new List<HttpPostedFileBase>();
                if (ProductImg.Any()) {
                    foreach (var file in ProductImg) {
                        // Some browsers send file names with full path. We only care about the file name.
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName == null) continue;
                        var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/ProductImg"), fileName);
                        // giam kich thuoc
                        Image bm = Image.FromStream(file.InputStream);
                        var designWidth = 3000.0;
                        var designHeight = 1500.0;
                        var ratioW = designWidth / (double)bm.Width;
                        var ratioH = designHeight / (double)bm.Height;
                        var ratio = ratioH < ratioW ? ratioH : ratioW;
                        var newWidth = Convert.ToInt32(bm.Width * ratio);
                        var newHeight = Convert.ToInt32(bm.Height * ratio);
                        bm = MyUtilities.Function.ResizeBitmap((Bitmap)bm, newWidth, newHeight);
                        bm.Save(destinationPath, bm.RawFormat);
                    }
                    return Json("Upload thành công !");
                }
                else {
                    throw new AggregateException("attachments null");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UploadSave", ex.Message);
                return Json(ex.Message);
            }
            // Redirect to a view showing the result of the form submissSaveion.    
            //return Json("False");
        }


        [HttpPost]
        public ActionResult SaveQcImg(IEnumerable<HttpPostedFileBase> QcImg) {
            try {
                if (QcImg.Any()) {
                    foreach (var file in QcImg) {
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName == null) continue;
                        var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/QcImg"), fileName);
                        // giam kich thuoc
                        Image bm = Image.FromStream(file.InputStream);
                        var designWidth = 3000.0;
                        var designHeight = 1500.0;
                        var ratioW = designWidth / (double)bm.Width;
                        var ratioH = designHeight / (double)bm.Height;
                        var ratio = ratioH < ratioW ? ratioH : ratioW;
                        var newWidth = Convert.ToInt32(bm.Width * ratio);
                        var newHeight = Convert.ToInt32(bm.Height * ratio);
                        bm = MyUtilities.Function.ResizeBitmap((Bitmap)bm, newWidth, newHeight);
                        bm.Save(destinationPath, bm.RawFormat);
                    }
                    return Json("Upload thành công !");
                }
                else {
                    throw new AggregateException("attachments null");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UploadSave", ex.Message);
                return Json(ex.Message);
            }
        }





        [HttpPost]
        public ActionResult Save2D(IEnumerable<HttpPostedFileBase> Attachment2D, IEnumerable<HttpPostedFileBase> AttachmentReal) {
            // The Name of the Upload component is "attachments"       
            try {
                if (Attachment2D.Any()) {
                    foreach (var file in Attachment2D) {
                        // Some browsers send file names with full path. We only care about the file name.
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName == null) continue;
                        var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/Drawing"), fileName);

                        byte[] fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        System.IO.File.WriteAllBytes(destinationPath, fileBytes);
                    }
                    return Json("Oke");
                }
                else {
                    throw new AggregateException("attachments null");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UploadSave", ex.Message);
                return Json(ex.Message);
            }
            // Redirect to a view showing the result of the form submission.    
            //return Json("False");
        }
        public ActionResult CheckProductImage(string upload) {
            try {

                using (var vfi = new tammaContext()) {
                    var productCodes = "";
                    var productImgs = vfi.ProductImgs.Where(p => p.ImgUrl.Equals(upload));
                    if (productImgs.Any()) {
                        foreach (var productImg in productImgs) {
                            productCodes += productImg.Product.ProductCode + " | ";
                        }
                        return Json("9! " + productCodes);
                    }

                }
            }
            catch (Exception ex) {
                return Json("0! " + ex.Message);
            }
            return Json("");
        }

        public ActionResult CheckQcImage(string upload) {
            try {

                using (var vfi = new tammaContext()) {
                    var productCodes = "";
                    var qcImgs = vfi.QcImgs.Where(p => p.ImgUrl.Equals(upload));
                    if (qcImgs.Any()) {
                        foreach (var qcImg in qcImgs) {
                            productCodes += qcImg.Product.ProductCode + " || ";
                        }
                        return Json("9! " + productCodes);
                    }
                }
            }
            catch (Exception ex) {
                return Json("0! " + ex.Message);
            }
            return Json("");
        }



        public ActionResult CheckUploadImage(string upload) {
            //if (date.DayOfWeek == DayOfWeek.Monday)
            //    date = date.AddDays(-2);
            //else
            //    date = date.AddDays(-1);
            try {

                using (var vfi = new tammaContext()) {
                    var productCodes = "";
                    var products = vfi.Products.Where(p => p.Drawing2D.Equals(upload));
                    if (products.Any()) {
                        foreach (var product in products) {
                            productCodes += product.ProductCode + " | ";
                        }
                        return Json("9! " + productCodes);
                    }
                    products = vfi.Products.Where(p => p.DrawingFinish.Equals(upload));
                    if (products.Any()) {
                        foreach (var product in products) {
                            productCodes += product.ProductCode + " | ";
                        }
                        return Json("8! " + productCodes);
                    }
                    //if (string.IsNullOrWhiteSpace(upload) && (string.IsNullOrWhiteSpace(product.Drawing2D) ||
                    //    product.Drawing2D.Equals("askquestion.jpg")))
                    //    return Json("8!");
                    //if (materialUse == null)
                    //{
                    //    return Json("");
                    //}
                    //if (type == 1)
                    //    return Json("1" + materialUse.Shift1);
                    //if (type == 2)
                    //    return Json("2" + materialUse.Shift2);

                }
            }
            catch (Exception ex) {
                return Json("0! " + ex.Message);
            }
            return Json("");
        }

        public ActionResult CheckUploadRealImage(string upload) {
            //if (date.DayOfWeek == DayOfWeek.Monday)
            //    date = date.AddDays(-2);
            //else
            //    date = date.AddDays(-1);
            try {

                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.DrawingFinish.Equals(upload));
                    if (product != null)
                        return Json("9! " + product.ProductCode);
                    //if (string.IsNullOrWhiteSpace(upload) && (string.IsNullOrWhiteSpace(product.Drawing2D) ||
                    //    product.Drawing2D.Equals("askquestion.jpg")))
                    //    return Json("8!");
                    //if (materialUse == null)
                    //{
                    //    return Json("");
                    //}
                    //if (type == 1)
                    //    return Json("1" + materialUse.Shift1);
                    //if (type == 2)
                    //    return Json("2" + materialUse.Shift2);

                }
            }
            catch (Exception ex) {
                return Json("0! " + ex.Message);
            }
            return Json("");
        }

        #endregion

        #region combobox

        public ActionResult SelectComboBoxProduct() {
            var model = new List<ProductModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.Products.Where(f => f.Active)
                    .Select(p => new ProductModel {
                        ProductId = p.ProductId,
                        ProductCode = p.ProductCode
                    })
                    .OrderBy(p=> p.ProductCode)
                    .ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "ProductId", "ProductCode"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxTestingProduct() {
            var model = new List<ProductModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.Products.Where(f => f.Active && f.ProductionTestings.Any(x => x.ProductionTestingDetails.Any(y => y.Active)))
                    .Select(p => new ProductModel {
                        ProductId = p.ProductId,
                        ProductCode = p.ProductCode
                    })
                    .OrderBy(p => p.ProductCode)
                    .ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "ProductId", "ProductCode"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxProductStatus() {
            var val = from MyUtilities.Product.ProductStatusEnum stt in Enum.GetValues(typeof(MyUtilities.Product.ProductStatusEnum))
                      select new {
                          Value = (int)Enum.Parse(typeof(MyUtilities.Product.ProductStatusEnum), stt.ToString()),
                          Text =
                      MyUtilities.Product.GetText(
                          (int)Enum.Parse(typeof(MyUtilities.Product.ProductStatusEnum), stt.ToString()))
                      };
            return new JsonResult {
                Data = new SelectList(val.OrderBy(v => v.Text), "Value", "Text")
            };
        }

        public ActionResult SelectComboBoxProductCodeName() {
            var model = new List<ProductModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.Products.Where(f => f.Active)
                    .Select(p => new ProductModel {
                        ProductId = p.ProductId,
                        ProductCode = p.ProductCode,
                        ProductName = p.ProductName
                    })
                    .OrderBy(p => p.ProductCode)
                    .ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "ProductId", "ProductCodeName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult SelectComboBoxWorkOrderProduct(string text) {
            var model = new List<string>();
            if (text.HasValue()) {
                try {
                    using (var vfi = new tammaContext()) {
                        model =
                           (from p in vfi.Products
                            where p.Customer.IsWorkOrder == true
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

        public ActionResult SelectComboBoxProductPlatingCodeName() {
            var model = new List<ProductModel>();
            using (var vfi = new tammaContext()) {
                var productPlatings = (from pp in vfi.ProductionPlatings
                                       where pp.Active && pp.Product.Active
                                       orderby pp.Product.ProductCode
                                       select new {
                                           pp.ProductId,
                                           pp.Product.ProductCode,
                                       }).Distinct();
                foreach (var productPlating in productPlatings) {

                    var entity = new ProductModel {
                        ProductId = productPlating.ProductId,
                        ProductCode = productPlating.ProductCode,
                    };
                    model.Add(entity);
                }

            }
            return new JsonResult {
                Data =
                    new SelectList(model, "ProductId", "ProductCode"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxProductFullCodeName() {
            var model = new List<ProductModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.Products.Where(f => f.Active)
                    .Select(p => new ProductModel {
                        ProductId = p.ProductId,
                        ProductCode = p.ProductCode,
                        ProductName = p.ProductName,
                        DesignNo = p.DesignNo,
                    })
                    .OrderBy(p => p.ProductCode)
                    .ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "ProductId", "ProductFullCodeName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxProductCustomerAccess() {

            var model = new List<ProductModel>();
            var customerIds = MyUtilities.Sales.GetCustomerAccessList(HttpContext.User.Identity.Name);
            using (var vfi = new tammaContext()) {
                vfi.Configuration.LazyLoadingEnabled = false;
                var products = vfi.Products.Where(p => p.Active && customerIds.Contains(p.CustomerId));
                foreach (var product in products) {
                    var entity = new ProductModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        ProductName = product.ProductName,
                        DesignNo = product.DesignNo
                    };
                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "ProductId", "ProductFullCodeName")
            };
        }

        public ActionResult SelectComboBoxProductCodeTax() {
            var model = new List<ProductModel>();
            using (var vfi = new tammaContext()) {
                vfi.Configuration.LazyLoadingEnabled = false;
                var products = vfi.Products.Where(p => p.Active);
                foreach (var product in products) {
                    var entity = new ProductModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        ProductName = product.ProductName,
                        DesignNo = product.DesignNo,
                        TaxCode = product.TaxCode
                    };
                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "ProductId", "ProductTaxCode")
            };
        }

        public ActionResult SelectComboBoxProductCodePlating() {
            var model = new List<ProductModel>();
            using (var vfi = new tammaContext()) {
                var products = (from p in vfi.Products
                                where p.ProductionPlatings.Any() && p.Active

                                //&& p.Status > (byte)MyUtilities.Product.ProductStatusEnum.Quoting
                                select new {
                                    p.ProductId,
                                    p.ProductCode
                                }).ToList();
                foreach (var product in products) {
                    model.Add(new ProductModel {
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode
                    });
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "ProductId", "ProductCode")
            };
        }

        public ActionResult SelectComboBoxProductVfCodeDesignNo() {
            var model = new List<ProductModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.Products.Where(f => f.Active)
                    .Select(p => new ProductModel {
                        ProductId = p.ProductId,
                        ProductCode = p.ProductCode,
                        ProductName = p.ProductName,
                        DesignNo = p.DesignNo,
                    })
                    .OrderBy(p => p.ProductCode)
                    .ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "ProductId", "PrductVfCodeDesignNo"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxProductInvCode() {
            var model = new List<ProductModel>();
            return new JsonResult {
                Data = new SelectList(model, "ProductInvId", "PrductInvCode")
            };
        }

        public ActionResult SelectComboBoxProductName() {
            var model = new List<ProductModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.Products.Where(f => f.Active)
                    .Select(p => new ProductModel {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                    })
                    .OrderBy(p => p.ProductCode)
                    .ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "ProductId", "ProductName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #region Production Section

        [GridAction]
        public ActionResult SelectProductionSectionById(int productId) {
            var model = new List<ProductionSectionModel>();
            try {
                model = GetSectionListByProductId(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionSectionById", ex.Message);
            }
            return View(new GridModel(model));
        }

        public List<ProductionSectionModel> GetSectionListByProductId(int productId) {
            var model = new List<ProductionSectionModel>();
            using (var vfi = new tammaContext()) {
                var production = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.ProductionManagement);
                var sale = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.SaleManagement);
                var sections = vfi.ProductionSections.Where(ps => ps.ProductId == productId);
                foreach (var section in sections) {
                    var entity = new ProductionSectionModel {
                        SectionId = section.ProductionSectionId,
                        SectionName = section.Section.SectionName,
                        SectionCost = section.Section.SaleFactor * section.Productivity,
                        Weight = section.Weight,
                        UpdateDate = section.UpdateDate ?? DateTime.Now,
                        UpdateUser = section.UpdateUser,
                        Description = section.Description,
                        Active = section.Active,
                        IsProductionManagement = 1,
                        IsSaleManagement = 1,
                        SectionIndex = section.SectionIndex,
                        Productivity = section.Productivity
                    };
                    if (production) {
                        entity.IsProductionManagement = 2;
                    }
                    if (sale || string.IsNullOrWhiteSpace(entity.UpdateUser)) {
                        entity.IsSaleManagement = 2;
                    }
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.SectionIndex).ToList();
        }

        [GridAction]
        public ActionResult InsertProductionSection(ProductionSectionModel newSection,
            //[Bind(Prefix = "inserted")]IEnumerable<ProductionSectionModel> insertedDetails,
            //[Bind(Prefix = "updated")]IEnumerable<ProductionSectionModel> updatedDetails,
            //[Bind(Prefix = "deleted")]IEnumerable<ProductionSectionModel> deletedDetails,
            int productId) {
            //if (!insertedDetails.Any())
            //    return View(new GridModel(GetSectionListByProductId(productId)));
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                //    var insertList = new List<ProductionSection>();
                //    foreach (var newSection in insertedDetails)
                //    {
                if (string.IsNullOrWhiteSpace(newSection.SectionName))
                    throw new AggregateException("Lỗi! Nhập tên công đoạn!");
                var section = new ProductionSection {
                    ProductId = productId,
                    Active = true,
                    Description = newSection.Description + "",
                    SectionIndex = newSection.SectionIndex,
                    Productivity = newSection.Productivity,
                    UpdateDate = DateTime.Now,
                    UpdateUser = HttpContext.User.Identity.Name,
                };
                //insertList.Add(section);
                //}
                using (var vfi = new tammaContext()) {
                    //vfi.ProductionSections.AddRange(insertList);                    
                    vfi.ProductionSections.Add(section);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionSection", ex.Message);
            }
            return View(new GridModel(GetSectionListByProductId(productId)));
        }

        [GridAction]
        public ActionResult EditProductionSection(int productId, ProductionSectionModel editSection) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(editSection.SectionName))
                    throw new AggregateException("Lỗi! Nhập tên công đoạn!");

                using (var vfi = new tammaContext()) {
                    var section = vfi.ProductionSections.FirstOrDefault(ps => ps.SectionId == editSection.SectionId);
                    if (section != null) {
                        //section.ProductId = productId;
                        section.Active = true;
                        section.Description = editSection.Description;
                        section.UpdateDate = DateTime.Now;
                        section.UpdateUser = HttpContext.User.Identity.Name;
                        section.SectionIndex = editSection.SectionIndex;
                        section.Productivity = editSection.Productivity;
                        //section.SectionCost = 0;
                        vfi.SaveChanges();
                    }
                    //vfi.ProductionSections.Add(section);
                    else {
                        throw new AggregateException("Lỗi! Không tìm thấy công đoạn cần sửa!");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionSection", ex.Message);
            }
            return View(new GridModel(GetSectionListByProductId(productId)));
        }

        [GridAction]
        public ActionResult UpdateProductionSection(int productId, ProductionSectionModel updateSection) {
            //try
            //{
            //    if (!Request.IsAuthenticated)
            //    {
            //        throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
            //                                 "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
            //                                 "Xin vui lòng đăng nhập lại hệ thống.");
            //    }
            //    using (var vfi = new tammaContext())
            //    {
            //        var section = vfi.ProductionSections.FirstOrDefault(ps => ps.SectionId == updateSection.SectionId);
            //        if (section != null)
            //        {
            //            section.SectionCost = updateSection.SectionCost;
            //            section.UpdateDate = DateTime.Now;
            //            section.UpdateUser = HttpContext.User.Identity.Name;
            //        }
            //        //vfi.ProductionSections.Add(section);
            //        vfi.SaveChanges("Product", "UpdateProductionSection", DateTime.Now,
            //                        HttpContext.User.Identity.Name);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("UpdateProductionSection", ex.Message);
            //}
            return View(new GridModel(GetSectionListByProductId(productId)));
        }

        List<ProductModel> GetComparePriceList(int customerId, string productCode, int status, string fromDate, string productName) {
            var model = new List<ProductModel>();
            //var customerId = -1;
            //var productCode = "";
            //if (customerId == 0 && string.IsNullOrWhiteSpace(productCode) && status == 0)
            //    return PartialView("PageProductPriceList", model);
            try {

                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var products = from p in vfi.Products
                                   orderby p.Customer.CustomerCode, p.ProductCode
                                   where p.Active && p.Customer.State == (byte)MyUtilities.Sales.CustomerState.Active
                                         && (status == 0 || p.Status == status)
                                         && ((customerId <= 0) || (p.CustomerId == customerId))
                                   //&& p.ProductCode.Contains(productCode)
                                   //&& p.ProductName.Contains(productName)
                                   select new {
                                       p.ProductId,
                                       ProductCode = (p.ProductCode + "").Trim(),
                                       ProductName = (p.ProductName + "").Trim(),
                                       p.CustomerId,
                                       p.Customer.CustomerCode,
                                       p.Customer.CustomerName,
                                       p.MaterialId,
                                       p.Material,
                                       //p.Material.MaterialCode,
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
                                       p.ProcessingDesign,
                                       p.DrawingFinish,
                                       p.DiameterTypeDesign,
                                       KnifeCut = p.KnifeCut ?? 0,
                                       p.ProductionSections,
                                       MillProductivity = p.MillProductivity ?? 0,
                                       MaterialCost = p.MaterialCost ?? 0,
                                       p.QcProductivity,
                                       p.ProductShape,
                                       p.NewUpdateDate,
                                       ProductionMaterial =
                                       p.ProductionMaterials.Where(pm => pm.Active).OrderBy(pm => pm.Priority).FirstOrDefault().Material,
                                   };
                    if (!string.IsNullOrWhiteSpace(productCode)) {
                        products = products.Where(p => p.ProductCode.ToLower().Contains(productCode.ToLower()));
                    }
                    if (!string.IsNullOrWhiteSpace(productName)) {
                        products = products.Where(p => p.ProductName.ToLower().Contains(productName.ToLower()));
                    }

                    if (!string.IsNullOrWhiteSpace(fromDate)) {
                        var ci = new CultureInfo("vi-VN");
                        var fdate = string.IsNullOrWhiteSpace(fromDate)
                            ? DateTime.Now
                            : Convert.ToDateTime(fromDate, ci).AddDays(-1);
                        products = products.Where(p => p.NewUpdateDate >= fdate);
                    }
                    var productIds = products.Select(p => p.ProductId).ToList();
                    var productionSections = from ps in vfi.ProductionSections
                                             where productIds.Contains(ps.ProductId)
                                                   && ps.Active
                                             select new {
                                                 ps.ProductId,
                                                 ps.Section.SectionName,
                                                 SectionCost = ps.Productivity * ps.Section.SaleFactor,
                                                 ps.Productivity,
                                             };
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
                                              rp.Machine,
                                              rp.Machine.ProcessingType,
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
                    foreach (var product in products) {
                        //if (entity.ProductCode.Equals("H2739"))
                        //    model = new List<ProductModel>();
                        var entity = new ProductModel {
                            ProductId = product.ProductId,
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            CustomerId = product.CustomerId,
                            CustomerCode = product.CustomerCode,
                            CustomerName = product.CustomerName,
                            MaterialId = product.MaterialId ?? 0,
                            //MaterialCode = product.MaterialCode,
                            //MaterialName = product.MaterialName,
                            DesignNo = product.DesignNo,
                            Diameter = product.Diameter,
                            Length = product.Length,
                            //Weight = product.Weight,
                            Active = product.Active,
                            ForecastsQuality = product.ForecastsQuality,
                            ModifiedUser = product.ModifiedUser,
                            ModifiedDate = product.ModifiedDate,
                            ProductionWeight = product.ProductionWeight,
                            CncWeight = product.CncWeight,
                            Production2Weight = product.Production2Weight,
                            HeatTreatmentWeight = product.HeatTreatmentWeight,
                            SurfaceTreatmentWeight = product.SurfaceTreatmentWeight,
                            WaitingPlatingWeight = product.WaitingPlatingWeight,
                            PlatingWeight = product.PlatingWeight,
                            QcWeight = product.QcWeight,
                            FinishWeight = product.FinishWeight,
                            ProductionRate = product.ProductionRate,
                            Productivity = product.Productivity,
                            UnitPrice = product.UnitPrice,
                            ProductionFactor = product.ProductionFactor,
                            SalesFactor = product.SaleFactor,
                            IsSelling = product.IsSelling,
                            MaterialNameDesign = product.MaterialNameDesign + "",
                            OutDiameterDesign = product.OutDiameterDesign,
                            OutDiameterTolerance = product.OutDiameterTolerance,
                            InDiameterDesign = product.InDiameterDesign,
                            InDiameterTolerance = product.InDiameterTolerance,
                            ShapeDesign = product.ShapeDesign.Trim(),
                            Upload2D = product.Drawing2D,
                            UploadReal = product.DrawingFinish,
                            DiameterTypeDesign = product.DiameterTypeDesign,
                            KnifeCut = product.KnifeCut,
                            StatusFilter = product.Status,
                            Status = product.Status,
                            StatusName = MyUtilities.Product.GetText(product.Status),
                            MillProductivity = product.MillProductivity,
                            MillCost = product.MillProductivity * processingType.ProcessingSaleFactor.Value,
                            //ProcessingSalesCost =
                            //    product.ProcessingType.ProcessingSaleFactor.Value*product.Productivity,
                            //ProcessingCost = product.ProcessingType.ProcessingFactor.Value*product.Productivity,
                            QcProductivity = product.QcProductivity,
                            ProductShape = product.ProductShape + product.Diameter,
                            SectionList = new List<ProductionSectionModel>(),
                            ToDate = fromDate,
                        };
                        if (product.ProcessingDesign != null) {
                            entity.ProcessingTypeId = product.ProcessingType.TypeId;
                            entity.ProcessingTypeName = product.ProcessingType.TypeName;
                            entity.ProcessingSalesCost =
                                product.ProcessingType.ProcessingSaleFactor.Value * product.Productivity;
                            entity.ProcessingCost = product.ProcessingType.ProcessingFactor.Value * product.Productivity;
                        }
                        if (product.MaterialId != null) {
                            entity.MaterialId = product.MaterialId.Value;
                            entity.MaterialName = product.Material.MaterialName;
                            entity.MaterialCode = product.Material.MaterialCode;
                        }
                        var realMaterialByProductId =
                            realMaterials.FirstOrDefault(rm => rm.ProductId == product.ProductId);
                        if (realMaterialByProductId != null) {
                            entity.MaterialTypeId = realMaterialByProductId.Material.MaterialTypeId;
                            entity.MaterialNameDesign = realMaterialByProductId.Material.MaterialName;
                            entity.OutDiameterDesign = realMaterialByProductId.Material.OutDiameter;
                            entity.ShapeDesign = realMaterialByProductId.Material.Shape;
                            entity.InDiameterDesign = realMaterialByProductId.Material.InDiameter;
                            entity.DiameterTypeDesign = (realMaterialByProductId.Material.DiameterType + "").Trim();
                            entity.MaterialCost = realMaterialByProductId.Material.UnitPrice;
                            entity.MaterialCodeDesign = product.Material.MaterialName +
                                MyUtilities.Material.GetMaterialDesignNo(product.Material);
                            if (product.ProductionMaterial != null) {
                                entity.Weight = MyUtilities.Product.GetProductWeight(
                            product.ProductionMaterial.MaterialName,
                                product.ProductionMaterial.OutDiameter,
                                product.ProductionMaterial.InDiameter,
                                product.Length,
                                product.KnifeCut,
                                product.ProductionMaterial.Shape + "");
                            }
                            else {
                                entity.Weight = MyUtilities.Product.GetProductWeight(
                            product.MaterialNameDesign,
                                product.OutDiameterDesign,
                                product.InDiameterDesign,
                                product.Length,
                                product.KnifeCut,
                                product.ShapeDesign + "");
                            }
                        }
                        // nguyen lieu thiet ke
                        //var material =
                        //    vfi.Materials.FirstOrDefault(
                        //        m =>
                        //        m.MaterialName.Equals(product.MaterialNameDesign) &&
                        //        m.OutDiameter == product.OutDiameterDesign);
                        //if (material != null)
                        //    product.MaterialCost = material.UnitPrice ?? 0;
                        //product.MaterialCodeDesign = product.GetMaterialCodeDesign();
                        //product.Weight = product.GetProductWeight();
                        var productionSectionById = productionSections.Where(ps => ps.ProductId == product.ProductId);

                        if (productionSectionById.Any()) {
                            foreach (var section in productionSectionById) {
                                var sectionModel = new ProductionSectionModel {
                                    SectionName = section.SectionName,
                                    SectionCost = section.SectionCost,
                                    Productivity = section.Productivity,
                                };
                                entity.SectionList.Add(sectionModel);
                            }
                        }
                        var productPlatingById = productPlatings.Where(ps => ps.ProductId == product.ProductId);
                        if (productPlatingById.Any()) {
                            var sectionModel = new ProductionSectionModel {
                                SectionName = productPlatingById.FirstOrDefault().PlatingName,
                                SectionCost = 0,
                                Productivity = 0,
                            };
                            if (product.PlatingWeight != 0)
                                sectionModel.SectionCost = Math.Round(50000 / 1000 * product.PlatingWeight, 0);
                            entity.PlatingCost = sectionModel.SectionCost;
                            entity.SectionList.Add(sectionModel);
                        }
                        if (!entity.SectionList.Any()) {
                            var sectionModel = new ProductionSectionModel {
                                SectionName = "Without",
                                SectionCost = 0,
                            };
                            entity.SectionList.Add(sectionModel);

                        }
                        //if (product.PlatingWeight != 0)
                        //    product.PlatingCost = Math.Round(50000/1000*product.PlatingWeight, 0);
                        entity.MaterialUnitPrice = ((entity.Weight ?? 0) / 1000) * entity.MaterialCost;
                        entity.ProductBaseCost = entity.MaterialUnitPrice + entity.MillCost +
                                                  entity.ProcessingSalesCost +
                                                  entity.SectionCost + entity.PlatingCost;
                        entity.UnitPrice = MyUtilities.Product.ProductVndPrice(product.UnitPrice,1,exchangeRate);
                        //if (product.UnitPrice != 0) {
                        //    var productPrice = Math.Round(product.UnitPrice, 4);
                        //    var temp = Convert.ToInt32(productPrice);
                        //    if (productPrice - temp != 0)
                        //        productPrice = Math.Round(productPrice * MyUtilities.Product.ExchangeRateDesign, 0);
                        //    entity.UnitPrice = productPrice; //* MyUtilities.Product.DesignPrice;
                        //}
                        var realProductionsByProductId = realProductions.Where(rp => rp.ProductId == product.ProductId);
                        if (realProductionsByProductId.Any()) {
                            var realProductivity =
                                realProductionsByProductId.Where(rp => !rp.Machine.MachineName.Contains("P"))
                                                          .OrderByDescending(rp => rp.RealProductivity).FirstOrDefault();
                            if (realProductivity != null) {
                                entity.RealProductivity = realProductivity.RealProductivity;
                                entity.RealProcessingTypeName =
                                    realProductivity.ProcessingType.TypeName;
                                entity.RealProcessingCost =
                                    realProductivity.ProcessingType.ProcessingSaleFactor.Value *
                                    entity.RealProductivity;
                            }
                            var realMillProductivity =
                                 realProductionsByProductId.Where(rp => rp.Machine.MachineName.Contains("P"))
                                                          .OrderByDescending(rp => rp.RealProductivity).FirstOrDefault();
                            if (realMillProductivity != null) {
                                entity.RealMillProductivity = realMillProductivity.RealProductivity /
                                                               realMillProductivity.RealRate;
                                entity.RealMillCost = entity.RealMillProductivity * processingType.ProcessingSaleFactor.Value;
                            }
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetComparePriceList", "" + ex.Message);
            }
            return model;

        }

        [HttpPost]
        public ActionResult PrintComparePriceList(int customerId, string productCode, int status, string productName) {
            var model = new List<ProductModel>();
            //var customerId = -1;
            //var productCode = "";
            if (customerId == 0 && string.IsNullOrWhiteSpace(productCode) && string.IsNullOrWhiteSpace(productName) && status == 0)
                return PartialView("PageProductPriceList", model);
            try {
                model = GetComparePriceList(customerId, productCode, status, "", productName);
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintComparePriceList", "" + ex.Message);
            }
            return PartialView("PageProductPriceList", model);
        }

        [HttpPost]
        public ActionResult PrintPriceList(int customerId, string productCode, int status, string fromDate, string productName) {
            var model = new List<ProductModel>();
            try {
                model = SelectAllProducts(true, -1, customerId, productCode, 0, fromDate, productName);
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintPriceList", "" + ex.Message);
            }
            return PartialView("PageProductList", model);

        }

        [HttpPost]
        public ActionResult PrintTechnicalList(int customerId, string productCode, int status, string fromDate) {
            var model = new List<ProductModel>();

            try {
                model = GetComparePriceList(customerId, productCode, status, fromDate, "");
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintTechnicalList", "" + ex.Message);
            }
            return PartialView("PageProductTechnicalList", model);

        }

        #endregion

        #region Production Detail

        [GridAction]
        public ActionResult SelectProductionMachine(int productId) {
            var model = new List<ProductModel>();
            try {
                model.Add(GetProductionDetailByProductId(productId));
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionMachine", ex.Message);
            }
            return View(new GridModel(model));
        }

        private ProductModel GetProductionDetailByProductId(int productId) {
            var product = new ProductModel();
            using (var vfi = new tammaContext()) {
                //vfi.Configuration.LazyLoadingEnabled = false;
                var entity = (from p in vfi.Products
                              where p.ProductId == productId
                              select new {
                                  p.ProductId,
                                  p.ProductCode,
                                  p.ProductName,
                                  p.CustomerId,
                                  p.Customer.CustomerCode,
                                  p.Customer.CustomerName,
                                  p.MaterialId,
                                  p.Material,
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
                                  //p.MaterialNameDesign,
                                  //OutDiameterDesign = p.OutDiameterDesign ?? 0,
                                  //p.OutDiameterTolerance,
                                  //InDiameterDesign = p.InDiameterDesign ?? 0,
                                  //p.InDiameterTolerance,
                                  //ShapeDesign = p.ShapeDesign ?? "",
                                  //p.DiameterTypeDesign,
                                  p.Drawing2D,
                                  Status = p.Status.Value,
                                  //p.id,
                                  p.ProcessingType,
                                  p.DrawingFinish,
                                  KnifeCut = p.KnifeCut ?? 0,
                                  p.ProductionSections,
                                  MillProductivity = p.MillProductivity ?? 0,
                                  MaterialCost = p.MaterialCost ?? 0,
                                  p.QcProductivity,
                                  CncProductivity = p.CncProductivity ?? 0,
                                  IsCalculateLock = p.IsCalculateLock ?? false,
                                  ProductionMaterial =
                                  p.ProductionMaterials.Where(pm => pm.Active).OrderBy(pm => pm.Priority).FirstOrDefault(),
                                  p.ProductionLevel,
                                  ProductionLevelName = p.ProductionLevel != null ? p.ProductionProductLevel.LevelName : "",
                                  p.ProcessClassifiedId,
                                  ProcessClassifiedName = p.ProcessClassifiedId != null ? p.ProcessClassified.Name+"-"+p.ProcessClassified.Description : "",
                              }).FirstOrDefault();
                //if (active)
                //    products = products.Where(p => p.Active);
                //if (status != 0)
                //    products = products.Where(p => p.Status == status);
                var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                if (user == null)
                    throw new AggregateException("Vui lòng đăng nhập lại");
                var processingType = vfi.ProcessingTypes.FirstOrDefault(m => m.TypeId == 6);
                //if (entity.ProductCode.Equals("1003"))
                //    model = new List<ProductModel>();
                product = new ProductModel {
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
                    //MaterialNameDesign = entity.MaterialNameDesign + "",
                    //OutDiameterDesign = entity.OutDiameterDesign,
                    //OutDiameterTolerance = entity.OutDiameterTolerance,
                    //InDiameterDesign = entity.InDiameterDesign,
                    //InDiameterTolerance = entity.InDiameterTolerance,
                    //ShapeDesign = entity.ShapeDesign.Trim(),
                    //DiameterTypeDesign = entity.DiameterTypeDesign,
                    Upload2D = entity.Drawing2D,
                    UploadReal = entity.DrawingFinish,
                    //ProcessingTypeId = entity.ProcessingType.TypeId,
                    //ProcessingTypeName = entity.ProcessingType.TypeName,
                    KnifeCut = entity.KnifeCut,
                    StatusFilter = entity.Status,
                    Status = entity.Status,
                    StatusName = MyUtilities.Product.GetText(entity.Status),
                    MillProductivity = entity.MillProductivity,
                    MaterialCost = entity.MaterialCost,
                    //MillCost = entity.MillProductivity * processingType.ProcessingSaleFactor.Value,
                    //ProcessingSalesCost = entity.ProcessingType.ProcessingSaleFactor.Value*entity.Productivity,
                    //ProcessingCost = entity.ProcessingType.ProcessingFactor.Value*entity.Productivity,
                    QcProductivity = entity.QcProductivity,
                    CncProductivity = entity.CncProductivity,
                    Weight = 0,
                    ProductionLevel = entity.ProductionLevel ?? 0,
                    ProductionLevelName = entity.ProductionLevelName,
                    ProcessClassifiedId = entity.ProcessClassifiedId ?? 0,
                    ProcessClassifiedName = entity.ProcessClassifiedName,
                    IsCalculateLock = entity.IsCalculateLock,
                };
                if (entity.MaterialId != null) {
                    product.MaterialId = entity.MaterialId.Value;
                    product.MaterialName = entity.Material.MaterialName;
                    product.MaterialCode = entity.Material.MaterialCode;
                    product.MaterialNameDesign = entity.Material.MaterialName;
                    product.OutDiameterDesign = entity.Material.OutDiameter;
                    product.InDiameterDesign = entity.Material.InDiameter;
                    product.ShapeDesign = entity.Material.Shape;
                    product.DiameterTypeDesign = entity.Material.DiameterType;
                }
                if (entity.ProcessingType != null) {
                    product.ProcessingTypeId = entity.ProcessingType.TypeId;
                    product.ProcessingTypeName = entity.ProcessingType.TypeName;
                    product.MillCost = entity.MillProductivity * processingType.ProcessingSaleFactor.Value;
                    product.ProcessingSalesCost = entity.ProcessingType.ProcessingSaleFactor.Value * entity.Productivity;
                    product.ProcessingCost = entity.ProcessingType.ProcessingFactor.Value * entity.Productivity;

                }
                if (entity.ProductionMaterial != null) {
                    product.MaterialCodeDesign = entity.ProductionMaterial.Material.MaterialName +
                        MyUtilities.Material.GetMaterialDesignNo(entity.ProductionMaterial.Material);

                    product.Weight = MyUtilities.Product
                        .GetProductWeight(entity.ProductionMaterial.Material.MaterialName,
                            entity.ProductionMaterial.Material.OutDiameter,
                            entity.ProductionMaterial.Material.InDiameter,
                            product.Length ?? 0,
                            product.KnifeCut,
                            entity.ProductionMaterial.Material.Shape + "");
                }
                var productionSections = from ps in vfi.ProductionSections
                                         where ps.ProductId == productId
                                               && ps.Active
                                         select new {
                                             ps.ProductId,
                                             SectionCost = ps.Productivity * ps.Section.SaleFactor
                                         };
                if (productionSections != null && productionSections.Any()) {
                    product.SectionCount = productionSections.Count();
                    product.SectionCost = productionSections.Sum(ps => ps.SectionCost);
                }
                //if(!string.IsNullOrWhiteSpace(entity.Drawing2D))
                //    product.Upload = Path.Combine(Server.MapPath("~/Content/FileUpload/Drawing"), entity.Drawing2D);
                if (string.IsNullOrWhiteSpace(entity.Drawing2D))
                    product.Upload2D = "askquestion.jpg";
                if (string.IsNullOrWhiteSpace(entity.DrawingFinish))
                    product.UploadReal = "askquestion.jpg";
            }
            return product;
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionDesign(ProductModel updateProduct) {
            using (var vfi = new tammaContext()) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                                 "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                                 "Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null)
                        throw new AggregateException("Vui lòng đăng nhập lại");
                    var product =
                        vfi.Products.FirstOrDefault(p => p.ProductId == updateProduct.ProductId);
                    if (product == null)
                        throw new AggregateException("Lỗi hệ thống");
                    product.OutDiameterDesign = updateProduct.OutDiameterDesign;
                    product.OutDiameterTolerance = updateProduct.OutDiameterTolerance;
                    product.Length = updateProduct.Length;
                    product.ProductionWeight = updateProduct.ProductionWeight;
                    product.QcWeight = updateProduct.QcWeight;
                    product.Diameter = updateProduct.Diameter;
                    product.Productivity = updateProduct.Productivity;
                    product.ProductionRate = updateProduct.ProductionRate;
                    product.KnifeCut = updateProduct.KnifeCut;
                    product.MillProductivity = updateProduct.MillProductivity;
                    product.CncProductivity = updateProduct.CncProductivity;
                    var processingType = 0;
                    try {
                        processingType = Convert.ToInt32(updateProduct.ProcessingTypeName);
                    }
                    catch (FormatException) {
                        //processingType =
                        //    vfi.ProcessingTypes.FirstOrDefault(
                        //        pt => pt.TypeName.Equals(updateProduct.ProcessingTypeName)).TypeId;
                    }
                    var processClassified = 0;
                    try {
                        processClassified = Convert.ToInt32(updateProduct.ProcessClassifiedName);
                    }
                    catch (FormatException) {
                        //processingType =
                        //    vfi.ProcessingTypes.FirstOrDefault(
                        //        pt => pt.TypeName.Equals(updateProduct.ProcessingTypeName)).TypeId;
                    }
                    if (processingType != 0)
                        product.ProcessingDesign = processingType;
                    if (processClassified != 0)
                        product.ProcessClassifiedId = processClassified;
                    if (product.ProductionMaterials.Any(pm => pm.Active)) {
                        foreach (var productionMaterial in product.ProductionMaterials.Where(pm => pm.Active)) {
                            productionMaterial.UnitWeightByMaterial = MyUtilities.Product
                                .GetProductWeight(productionMaterial.Material.MaterialName,
                                    productionMaterial.Material.OutDiameter,
                                    productionMaterial.Material.InDiameter,
                                    product.Length ?? 0,
                                    product.KnifeCut ?? 0,
                                    productionMaterial.Material.Shape + "");
                        }
                    }

                    var productionLevel = 0;
                    try {
                        productionLevel = Convert.ToInt32(updateProduct.ProductionLevelName);
                    }
                    catch (FormatException) {
                    }
                    if (productionLevel != 0) {
                        product.ProductionLevel = productionLevel;
                    } else {
                        product.ProductionLevel = null;
                    }
                    // auto update work order config
                    if (product.MaxQuantityInTray <= 0 && product.Productivity > 0 && product.ProductionWeight > 0) {
                        var maxWeight = 20000;
                        var maxTime = 1.5 * 24 * 3600;
                        var quantityWeight = 0;
                        if (product.ProductionWeight > 0) {
                            quantityWeight = MyUtilities.Function.RoundDown(maxWeight / product.ProductionWeight.Value);
                        }
                        var quantityTime = 0;
                        if (product.Productivity > 0) {
                            quantityTime = MyUtilities.Function.RoundDown(maxTime / product.Productivity.Value);
                        }
                        product.MaxQuantityInTray = quantityWeight > quantityTime ? quantityTime : quantityWeight;
                        var packingProductivity = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.PackingProductivity);
                        product.MaxQuantityInTrayRunTime = _productionController.CalculateProductionWorkOrderRunTime(product, packingProductivity);
                    }
                    vfi.SaveChanges();

                    product.FinishDesign = MyUtilities.Product.CheckDesign(product.ProductId);
                    vfi.SaveChanges();
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProductManagementProduction", ex.Message);
                }
            }
            return
                View(new GridModel(new List<ProductModel> { GetProductionDetailByProductId(updateProduct.ProductId) }));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionMaterialDesign(ProductModel updateProduct) {
            using (var vfi = new tammaContext()) {
                try {
                    if (!Request.IsAuthenticated) {
                        throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                                 "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                                 "Xin vui lòng đăng nhập lại hệ thống.");
                    }
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                    if (user == null)
                        throw new AggregateException("Vui lòng đăng nhập lại");
                    var product =
                        vfi.Products.FirstOrDefault(p => p.ProductId == updateProduct.ProductId);
                    if (product == null)
                        throw new AggregateException("Lỗi hệ thống");
                    int materialId = 0;
                    try {
                        materialId = Convert.ToInt32(updateProduct.MaterialCode);
                    }
                    catch (Exception) { }
                    var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == materialId);
                    if (material != null) {
                        product.MaterialId = materialId;
                        product.MaterialNameDesign = material.MaterialName;
                        product.OutDiameterDesign = material.OutDiameter;
                        product.InDiameterDesign = material.InDiameter;
                        product.ShapeDesign = (material.Shape + "").Trim();
                        product.DiameterTypeDesign = (material.DiameterType + "").Trim();
                        var productionMaterial =
                            vfi.ProductionMaterials.FirstOrDefault(
                                pm => pm.ProductId == product.ProductId && pm.MaterialId == materialId);
                        if (productionMaterial == null) {
                            productionMaterial = new ProductionMaterial() {
                                ProductId = product.ProductId,
                                MaterialId = materialId,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                Active = true,
                                Priority = 0,
                                Note = "Auto",
                            };
                            vfi.ProductionMaterials.Add(productionMaterial);
                        }
                        else if (!productionMaterial.Active) {
                            productionMaterial.Active = true;
                        }
                        productionMaterial.UnitWeightByMaterial =
                            MyUtilities.Product.GetProductWeight(product.MaterialNameDesign,
                                product.OutDiameterDesign ?? 0,
                                product.InDiameterDesign ?? 0,
                                product.Length ?? 0,
                                product.KnifeCut ?? 0,
                                product.ShapeDesign);
                        product.Weight = productionMaterial.UnitWeightByMaterial;
                    }
                    vfi.SaveChanges();
                    product.FinishDesign = MyUtilities.Product.CheckDesign(product.ProductId);
                    vfi.SaveChanges();
                    //return View(new GridModel(SelectAllProducts(false, product.Status.Value,
                    //                                            product.CustomerId, product.ProductCode,0)));
                }
                catch (Exception ex) {
                    ModelState.AddModelError("UpdateProductManagementProduction", ex.Message);
                }
            }
            //return View(new GridModel(SelectAllProducts(false, updateProduct.StatusFilter, -1, "", 0)));
            return
                View(new GridModel(new List<ProductModel> { GetProductionDetailByProductId(updateProduct.ProductId) }));
        }
        #endregion

        #region Production Plating

        [GridAction]
        public ActionResult SelectProductionPlatingById(int productId) {
            var model = new List<ProductionPlatingModel>();
            try {
                model = GetPlatingListByProductId(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionSectionById", ex.Message);
            }
            return View(new GridModel(model));
        }

        public List<ProductionPlatingModel> GetPlatingListByProductId(int productId) {
            var model = new List<ProductionPlatingModel>();
            using (var vfi = new tammaContext()) {
                var production = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.ProductionManagement);
                var sale = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.SaleManagement);
                var platings = vfi.ProductionPlatings.Where(ps => ps.ProductId == productId);
                foreach (var plating in platings) {
                    var entity = new ProductionPlatingModel {
                        PlatingId = plating.PlatingId,
                        PlatingName = plating.PlatingName,
                        PlatingCost = plating.PlatingCost,
                        InsertDate = plating.InsertDate,
                        InserUser = plating.InserUser,
                        ModifiedDate = plating.ModifiedDate,
                        ModifiedUser = plating.ModifiedUser,
                        Description = plating.Description,
                        Active = plating.Active,
                        IsProductionManagement = 1,
                        IsSaleManagement = 1
                    };
                    if (production) {
                        entity.IsProductionManagement = 2;
                    }
                    if (sale || string.IsNullOrWhiteSpace(entity.ModifiedUser)) {
                        entity.IsSaleManagement = 2;
                    }
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertProductionPlating(ProductionPlatingModel newSection,
            int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(newSection.PlatingName))
                    throw new AggregateException("Lỗi! Nhập tên công đoạn!");
                var plating = new ProductionPlating {
                    ProductId = productId,
                    Active = true,
                    PlatingName = newSection.PlatingName,
                    Description = newSection.Description + "",
                    InsertDate = DateTime.Now,
                    InserUser = HttpContext.User.Identity.Name,
                    //PlatingCost = newSection.PlatingCost,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = "",
                };
                using (var vfi = new tammaContext()) {
                    vfi.ProductionPlatings.Add(plating);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionPlating", ex.Message);
            }
            return View(new GridModel(GetPlatingListByProductId(productId)));
        }

        [GridAction]
        public ActionResult EditProductionPlating(int productId, ProductionPlatingModel edit) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(edit.PlatingName))
                    throw new AggregateException("Lỗi! Nhập tên công đoạn!");

                using (var vfi = new tammaContext()) {
                    var plating = vfi.ProductionPlatings.FirstOrDefault(ps => ps.PlatingId == edit.PlatingId);
                    if (plating != null) {
                        //section.ProductId = productId;
                        plating.Active = true;
                        plating.PlatingName = edit.PlatingName;
                        //plating.PlatingCost = edit.PlatingCost;
                        plating.Description = edit.Description;
                        plating.InsertDate = DateTime.Now;
                        plating.InserUser = HttpContext.User.Identity.Name;
                        //section.SectionCost = 0;
                        vfi.SaveChanges();
                    }
                    //vfi.ProductionSections.Add(section);
                    else {
                        throw new AggregateException("Lỗi! Không tìm thấy công đoạn cần sửa!");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("EditProductionPlating", ex.Message);
            }
            return View(new GridModel(GetPlatingListByProductId(productId)));
        }
        [GridAction]
        public ActionResult UpdateProductionPlating(int productId, ProductionPlatingModel update) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var plating = vfi.ProductionPlatings.FirstOrDefault(ps => ps.PlatingId == update.PlatingId);
                    if (plating != null) {
                        plating.PlatingCost = update.PlatingCost;
                        plating.ModifiedDate = DateTime.Now;
                        plating.ModifiedUser = HttpContext.User.Identity.Name;
                    }
                    //vfi.ProductionSections.Add(section);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionPlating", ex.Message);
            }
            return View(new GridModel(GetPlatingListByProductId(productId)));
        }
        #endregion

        #region Product change
        [GridAction]
        public ActionResult SelectProductChangeById(int productId) {
            var model = new List<ProductChangeModel>();
            try {
                using (var vfi = new tammaContext()) {
                    //var productChanges =
                    //    vfi.ProductChanges.Where(pc => pc.ProductId == productId)
                    //       .OrderByDescending(pc => pc.ModifiedDate);
                    model.AddRange(vfi.ProductChanges.Where(pc => pc.ProductId == productId)
                                      .OrderByDescending(pc => pc.ModifiedDate)
                                      .Select(productChange => new ProductChangeModel {
                                          ChangeId = productChange.ChangeId,
                                          ProductId = productChange.ProductId,
                                          NewProductCode =
                                              productChange.NewProductCode.Equals(productChange.OldProductCode)
                                                  ? ""
                                                  : productChange.NewProductCode,
                                          OldProductCode =
                                              productChange.NewProductCode.Equals(productChange.OldProductCode)
                                                  ? ""
                                                  : productChange.OldProductCode,
                                          NewPrice =
                                              productChange.NewPrice == productChange.OldPrice
                                                  ? 0
                                                  : productChange.NewPrice.Value,
                                          OldPrice =
                                              productChange.NewPrice == productChange.OldPrice
                                                  ? 0
                                                  : productChange.OldPrice.Value,
                                          ModifiedDate = productChange.ModifiedDate,
                                          ModifiedUser = productChange.ModifiedUser
                                      }));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductChangeById", ex.Message);
            }
            return View(new GridModel(model));
        }
        #endregion

        #region production tool

        [GridAction]
        public ActionResult SelectProductionToolById(int productId) {
            var model = new List<ProductionToolModel>();
            try {
                model = GetToolListByProductId(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionToolById", ex.Message);
            }
            return View(new GridModel(model));
        }

        public List<ProductionToolModel> GetToolListByProductId(int productId) {
            var model = new List<ProductionToolModel>();
            using (var vfi = new tammaContext()) {
                var tools =
                    vfi.ProductionTools.Where(pp => pp.ProductId == productId && pp.Active);
                foreach (var tool in tools) {
                    var entity = new ProductionToolModel {
                        RealToolId = tool.RealToolId,
                        ToolId = tool.ToolId,
                        ToolFullCode = tool.Tool.ToolFullCode,
                        ProductId = tool.ProductId,
                        ModifiedUser = tool.ModifiedUser,
                        ModifiedDate = tool.ModifiedDate,
                    };
                    model.Add(entity);
                }
            }
            return model;
        }

        #endregion

        #region Production Process

        [GridAction]
        public ActionResult SelectProductionProcessMachineById(int productId) {
            var model = new List<RealProductionModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var realProductions = from rp in vfi.RealProductions
                                          where
                                          rp.ProductId == productId &&
                                          rp.TrackUpMachine.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                          rp.Machine.MachineName.Contains("C") &&
                                          rp.Machine.Active
                                          select new {
                                              rp.Machine,
                                              rp.TrackUpMachine,
                                              rp.TrackUpMachine.ModifiedDate,
                                              rp.TrackUpMachine.ModifiedUser,
                                          };
                    foreach (var realProduction in realProductions) {
                        var entity = new RealProductionModel() {
                            ProductId = productId,
                            MachineId = realProduction.Machine.MachineId,
                            MachineName = realProduction.Machine.MachineName,
                            ModifiedUser = realProduction.ModifiedUser,
                            ModifiedDate = realProduction.ModifiedDate,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionProcessById", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        [GridAction]
        public ActionResult SelectProductionProcessById(int productId) {
            var model = new List<ProductionProcessModel>();
            try {
                model = GetProcessListByProductId(productId);
                //using (var vfi = new tammaContext())
                //{
                //}
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionProcessById", ex.Message);
            }
            return View(new GridModel(model));
        }
        public List<ProductionProcessModel> GetProcessListByProductId(int productId) {
            var model = new List<ProductionProcessModel>();
            using (var vfi = new tammaContext()) {
                var processes =
                    vfi.ProductionProcesses.Where(pp => pp.ProductId == productId && pp.IsNecessary).OrderBy(pp => pp.ProcessIndex);
                foreach (var process in processes) {
                    var entity = new ProductionProcessModel {
                        ProcessId = process.ProcessId,
                        ProductId = process.ProductId,
                        WarehouseId = process.WarehouseId,
                        WarehouseName = process.Warehouse.WarehouseName,
                        IsAlert = process.IsAlert,
                        IsNecessary = process.IsNecessary,
                        ProcessIndex = process.ProcessIndex,
                        ModifiedUser = process.ModifiedUser,
                        ModifiedDate = process.ModifiedDate,
                    };
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertProductionProcess(ProductionProcessModel insert,
            int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {

                    int warehouseId = 1;
                    try {
                        warehouseId = Convert.ToInt32(insert.WarehouseName);
                    }
                    catch (Exception) {
                        throw new AggregateException("Lỗi kho ! Chọn lại kho.");
                    }
                    if (!MyUtilities.Warehouse.GetWarehouseId_Process().Contains(warehouseId)) {
                        throw new AggregateException("Lỗi kho ! Kho chọn không chính thống.");
                    }
                    insert.IsNecessary = true;
                    var process =  vfi.ProductionProcesses.FirstOrDefault(  pp => pp.ProductId == productId && pp.WarehouseId == warehouseId);
                    if (process != null) {
                        if (!process.IsNecessary) {
                            process.IsAlert = insert.IsAlert;
                            process.IsNecessary = true;
                            process.ProcessIndex = insert.ProcessIndex;
                            process.ModifiedDate = DateTime.Now;
                            process.ModifiedUser = HttpContext.User.Identity.Name;
                            process.Note = insert.Note;
                        }
                        else {
                            throw new AggregateException("Lỗi kho ! Kho này đã có trong quy trình");
                        }
                    }
                    else {
                        process = new ProductionProcess {
                            ProductId = productId,
                            WarehouseId = warehouseId,
                            IsAlert = insert.IsAlert,
                            IsNecessary = true,
                            ProcessIndex = insert.ProcessIndex,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Note = insert.Note
                        };
                        vfi.ProductionProcesses.Add(process);
                    }
                    vfi.SaveChanges();

                    var finishDesign = MyUtilities.Product.CheckDesign(productId);
                    if (finishDesign) {
                        var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                        product.FinishDesign = true;
                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionProcess", ex.Message);
            }
            return View(new GridModel(GetProcessListByProductId(productId)));
        }

        [GridAction]
        public ActionResult UpdateProductionProcess(ProductionProcessModel update) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var process = vfi.ProductionProcesses.FirstOrDefault(pp => pp.ProcessId == update.ProcessId);
                    if (process == null) throw new AggregateException("Lỗi! Không tìm thấy công đoạn!");
                    int warehouseId = process.WarehouseId;
                    try {
                        warehouseId = Convert.ToInt32(update.WarehouseName);
                    }
                    catch (Exception) { }
                    if (process.WarehouseId != warehouseId) {
                        // UI cover this case
                    }
                    process.IsAlert = update.IsAlert;
                    process.IsNecessary = update.IsNecessary;
                    if (process.WarehouseId != MyUtilities.Warehouse.Finish &&
                        process.WarehouseId != MyUtilities.Warehouse.Packing &&
                        process.WarehouseId != MyUtilities.Warehouse.Production1)
                        process.ProcessIndex = update.ProcessIndex;
                    process.ModifiedDate = DateTime.Now;
                    process.ModifiedUser = HttpContext.User.Identity.Name;
                    process.Note = update.Note;
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(update.ProductId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionProcess", ex.Message);
            }
            return View(new GridModel(GetProcessListByProductId(update.ProductId)));
        }


        [GridAction]
        public ActionResult SelectProductionSectionByProcess(int productId, int warehouseId) {
            var model = new List<ProductionSectionModel>();
            try {
                model = GetProductionSectionByProcess(productId, warehouseId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionSectionByProcess", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<ProductionSectionModel> GetProductionSectionByProcess(int productId, int warehouseId) {
            var model = new List<ProductionSectionModel>();
            using (var vfi = new tammaContext()) {
                var production = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.ProductionManagement);
                var sale = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.SaleManagement);
                if (warehouseId == MyUtilities.Warehouse.Production2) {
                    var sections = vfi.ProductionSections.Where(ps => ps.ProductId == productId);
                    foreach (var section in sections) {
                        var entity = new ProductionSectionModel {
                            ProductionSectionId = section.ProductionSectionId,
                            SectionName = section.Section.SectionName,
                            SectionCost = section.Section.SaleFactor * section.Productivity,
                            UpdateDate = section.UpdateDate ?? DateTime.Now,
                            UpdateUser = section.UpdateUser,
                            Description = section.Description,
                            Active = section.Active,
                            IsProductionManagement = 1,
                            IsSaleManagement = 1,
                            SectionIndex = section.SectionIndex,
                            Productivity = section.Productivity,
                            Weight = section.Weight,
                            WarehouseId = warehouseId
                        };
                        if (production) {
                            entity.IsProductionManagement = 2;
                        }
                        if (sale || string.IsNullOrWhiteSpace(entity.UpdateUser)) {
                            entity.IsSaleManagement = 2;
                        }
                        model.Add(entity);
                    }
                    return model.OrderBy(m => m.SectionIndex).ToList();
                }
                if (warehouseId == MyUtilities.Warehouse.WaitingPlating) {
                    var platings = vfi.ProductionPlatings.Where(ps => ps.ProductId == productId);
                    foreach (var plating in platings) {
                        var entity = new ProductionSectionModel {
                            ProductionSectionId = plating.PlatingId,
                            ProductId = plating.ProductId,
                            SectionName = plating.PlatingName,
                            SectionCost = plating.PlatingCost,
                            InsertDate = plating.InsertDate,
                            InsertUser = plating.InserUser,
                            UpdateDate = plating.ModifiedDate,
                            UpdateUser = plating.ModifiedUser,
                            Description = plating.Description,
                            Active = plating.Active,
                            IsProductionManagement = 1,
                            IsSaleManagement = 1,
                            SectionIndex = plating.PlatingIndex,
                            Productivity = plating.PlatingDay,
                            WarehouseId = warehouseId
                        };
                        if (production) {
                            entity.IsProductionManagement = 2;
                        }
                        if (sale || string.IsNullOrWhiteSpace(entity.UpdateUser)) {
                            entity.IsSaleManagement = 2;
                        }
                        model.Add(entity);
                    }
                }
                return model.OrderBy(m => m.SectionIndex).ToList();
            }
        }

        [GridAction]
        public ActionResult InsertProductionSectionByProcess(ProductionSectionModel newSection,
            //[Bind(Prefix = "inserted")]IEnumerable<ProductionSectionModel> insertedDetails,
            //[Bind(Prefix = "updated")]IEnumerable<ProductionSectionModel> updatedDetails,
            //[Bind(Prefix = "deleted")]IEnumerable<ProductionSectionModel> deletedDetails,
            int productId, int warehouseId) {
            //if (!insertedDetails.Any())
            //    return View(new GridModel(GetSectionListByProductId(productId)));
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                //    var insertList = new List<ProductionSection>();
                //    foreach (var newSection in insertedDetails)
                //    {
                if (string.IsNullOrWhiteSpace(newSection.SectionName))
                    throw new AggregateException("Lỗi! Nhập tên công đoạn!");
                if (warehouseId == MyUtilities.Warehouse.Production2) {
                    if (newSection.Productivity <= 0) {
                        throw new AggregateException("Lỗi! Năng suất lỗi! Phải lớn hơn 0.");
                    }
                    using (var vfi = new tammaContext()) {
                        int sectionId = 1;
                        try {
                            sectionId = Convert.ToInt32(newSection.SectionName);
                        }
                        catch (Exception) {
                            var section2 = vfi.Sections.FirstOrDefault(w => w.SectionName.Equals(newSection.SectionName));
                            if (section2 == null)
                                throw new AggregateException("Lỗi công đoạn ! Chọn lại công đoạn");
                            sectionId = section2.SectionId;
                        }
                        var productionProgress =
                            vfi.ProductionProcesses.FirstOrDefault(
                                pp =>
                                pp.ProductId == productId && pp.WarehouseId == MyUtilities.Warehouse.Production2);
                        if (productionProgress == null) {
                            productionProgress = new ProductionProcess {
                                IsAlert = true,
                                IsNecessary = true,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ProductId = productId,
                                WarehouseId = MyUtilities.Warehouse.Production2,
                                ProcessIndex = 10
                            };
                            vfi.ProductionProcesses.Add(productionProgress);
                        }
                        else {
                            if (!productionProgress.IsNecessary) productionProgress.IsNecessary = true;
                        }
                        var section = new ProductionSection {
                            ProductId = productId,
                            Active = true,
                            Description = newSection.Description + "",
                            UpdateDate = DateTime.Now,
                            UpdateUser = HttpContext.User.Identity.Name,
                            Productivity = newSection.Productivity,
                            SectionIndex = newSection.SectionIndex,
                            SectionId = sectionId,
                            InsertDate = DateTime.Now,
                            InsertUser = HttpContext.User.Identity.Name,
                            Weight = newSection.Weight,
                        };
                        vfi.ProductionSections.Add(section);
                        vfi.SaveChanges();
                    }
                }
                else if (warehouseId == MyUtilities.Warehouse.WaitingPlating) {
                    if (string.IsNullOrWhiteSpace(newSection.SectionName))
                        throw new AggregateException("Lỗi! Nhập tên gia công!");
                    if (newSection.Productivity <= 0) {
                        throw new AggregateException("Lỗi! Ngày gia công ngoài lỗi! Phải lớn hơn 0.");
                    }
                    var plating = new ProductionPlating {
                        ProductId = productId,
                        Active = true,
                        PlatingName = newSection.SectionName,
                        Description = newSection.Description + "",
                        InsertDate = DateTime.Now,
                        InserUser = HttpContext.User.Identity.Name,
                        PlatingCost = 0,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "",
                        PlatingIndex = newSection.SectionIndex,
                        PlatingDay = Convert.ToInt32(newSection.Productivity),
                    };
                    using (var vfi = new tammaContext()) {
                        vfi.ProductionPlatings.Add(plating);
                        vfi.SaveChanges();
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionSection", ex.Message);
            }
            return View(new GridModel(GetProductionSectionByProcess(productId, warehouseId)));
        }

        [GridAction]
        public ActionResult EditProductionSectionByProcess(int productId, int warehouseId, ProductionSectionModel editSection) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (warehouseId == MyUtilities.Warehouse.Production2) {


                    using (var vfi = new tammaContext()) {

                        int sectionId = 0;
                        if (!string.IsNullOrWhiteSpace(editSection.SectionName)) {
                            try {
                                sectionId = Convert.ToInt32(editSection.SectionName);
                            }
                            catch (Exception) {
                                var section2 =
                                    vfi.Sections.FirstOrDefault(w => w.SectionName.Equals(editSection.SectionName));
                                if (section2 == null)
                                    throw new AggregateException("Lỗi ! Chọn lại gia công");
                                sectionId = section2.SectionId;
                            }
                        }
                        var section =
                            vfi.ProductionSections.FirstOrDefault(
                                ps => ps.ProductionSectionId == editSection.ProductionSectionId);
                        if (section != null) {
                            var productionProgress =
                                vfi.ProductionProcesses.FirstOrDefault(
                                    pp =>
                                    pp.ProductId == productId && pp.WarehouseId == MyUtilities.Warehouse.Production2);
                            if (productionProgress == null) {
                                productionProgress = new ProductionProcess {
                                    IsAlert = true,
                                    IsNecessary = true,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ProductId = productId,
                                    WarehouseId = MyUtilities.Warehouse.Production2
                                };
                                vfi.ProductionProcesses.Add(productionProgress);
                            }
                            else {
                                if (!productionProgress.IsNecessary) productionProgress.IsNecessary = true;
                            }
                            if (section.Productivity != editSection.Productivity && editSection.Productivity != 0) {
                                var log = new SectionLog {
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    NewProductivity = editSection.Productivity,
                                    OldProductivity = section.Productivity,
                                    ProductionSectionId = section.ProductionSectionId
                                };
                                section.Productivity = editSection.Productivity;
                                vfi.SectionLogs.Add(log);
                            }
                            if (sectionId != 0)
                                section.SectionId = sectionId;
                            //section.ProductId = productId;
                            section.Active = editSection.Active;
                            //section.SectionName = editSection.SectionName;
                            section.Description = editSection.Description;
                            section.UpdateDate = DateTime.Now;
                            section.UpdateUser = HttpContext.User.Identity.Name;
                            section.SectionIndex = editSection.SectionIndex;
                            section.Weight = editSection.Weight;

                            //section.SectionCost = 0;
                            vfi.SaveChanges();
                        }
                        //vfi.ProductionSections.Add(section);
                        else {
                            throw new AggregateException("Lỗi! Không tìm thấy công đoạn cần sửa!");
                        }
                    }
                }
                else if (warehouseId == MyUtilities.Warehouse.WaitingPlating) {
                    using (var vfi = new tammaContext()) {
                        var plating = vfi.ProductionPlatings.FirstOrDefault(ps => ps.PlatingId == editSection.ProductionSectionId);
                        if (plating != null) {
                            plating.PlatingName = editSection.SectionName;
                            plating.PlatingIndex = editSection.SectionIndex;
                            plating.ModifiedDate = DateTime.Now;
                            plating.ModifiedUser = HttpContext.User.Identity.Name;
                            plating.Description = editSection.Description + "";
                            plating.Active = editSection.Active;
                            plating.PlatingDay = Convert.ToInt32(editSection.Productivity); // cap nhat ngay GCN
                        }
                        //vfi.ProductionSections.Add(section);
                        vfi.SaveChanges();
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("EditProductionSectionByProcess", ex.Message);
            }
            return View(new GridModel(GetProductionSectionByProcess(productId, warehouseId)));
        }
        #endregion

        #region production defect


        [GridAction]
        public ActionResult SelectProductionDefectById(int productId) {
            var model = new List<ProductionDefectModel>();
            try {
                model = GetProductionDefectModelById(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionSectionByProcess", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<ProductionDefectModel> GetProductionDefectModelById(int productId) {
            var model = new List<ProductionDefectModel>();
            using (var vfi = new tammaContext()) {
                var defects = vfi.ProductionDefects.Where(pd => pd.ProductId == productId);
                foreach (var defect in defects) {
                    var entity = new ProductionDefectModel() {
                        ProductId = productId,
                        Active = defect.Active,
                        ModifiedDate = defect.ModifiedDate,
                        ModifiedUser = defect.ModifiedUser,
                        DefectName = defect.DefectName,
                        Description = defect.Description,
                        DefectId = defect.DefectId
                    };
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.Active).ThenBy(m => m.DefectName).ToList();
        }

        [GridAction]
        public ActionResult InsertProductionDefect(ProductionDefectModel insert, int productId) {
            //if (!insertedDetails.Any())
            //    return View(new GridModel(GetSectionListByProductId(productId)));
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var defect = vfi.ProductionDefects.FirstOrDefault(
                        pd => pd.DefectName.Equals(insert.DefectName) &&
                              pd.ProductId == productId);
                    if (defect == null) {
                        defect = new ProductionDefect {
                            ProductId = productId,
                            DefectName = insert.DefectName,
                            Active = true,
                            Description = insert.Description + "",
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                        };
                        vfi.ProductionDefects.Add(defect);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Lỗi ! Tên lỗi sản phẩm bị trùng! " + insert.DefectName);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionDefect", ex.Message);
            }
            return View(new GridModel(GetProductionDefectModelById(productId)));
        }

        [GridAction]
        public ActionResult EditProductionDefect(ProductionDefectModel update, int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var defect =
                        vfi.ProductionDefects.FirstOrDefault(
                            pd =>
                            pd.DefectName.Equals(update.DefectName) &&
                            pd.ProductId == productId &&
                            pd.DefectId != update.DefectId);
                    if (defect == null) {
                        defect = vfi.ProductionDefects.FirstOrDefault(pd => pd.DefectId == update.DefectId);
                        //defect.ProductId = productId;
                        defect.DefectName = update.DefectName;
                        defect.Active = update.Active;
                        defect.Description = update.Description;
                        defect.ModifiedUser = HttpContext.User.Identity.Name;
                        defect.ModifiedDate = DateTime.Now;
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Lỗi ! Tên lỗi sản phẩm bị trùng! " + update.DefectName);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("EditProductionDefect", ex.Message);
            }
            return View(new GridModel(GetProductionDefectModelById(productId)));
        }
        #endregion

        #region product img

        [GridAction]
        public ActionResult SelectProductImgById(int productId) {
            var model = new List<ProductImgModel>();
            try {
                var techicalManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.TechicalManagerLv2);
                if (techicalManager) {
                    model = GetProductImgById(productId);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductImgById", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductImgModel> GetProductImgById(int productId) {
            var model = new List<ProductImgModel>();
            var techicalManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.TechicalManagerLv2);
            //if (!techicalManager)
            //    return model;

            using (var vfi = new tammaContext()) {
                var productImgs = vfi.ProductImgs.Where(p => p.ProductId == productId).OrderBy(p => p.Step);
                foreach (var productImg in productImgs) {
                    var entity = new ProductImgModel {
                        ImgId = productImg.ImgId,
                        ProductId = productId,
                        ProductCode = productImg.Product.ProductCode,
                        ImgUrl = productImg.ImgUrl,
                        Step = productImg.Step,
                        Description = productImg.Description,
                        ModifiedDate = productImg.ModifiedDate,
                        ModifiedUser = productImg.ModifiedUser,
                        CanModify = techicalManager,
                        WarehouseId = productImg.WarehouseId,
                        WarehouseName = productImg.Warehouse.WarehouseName
                    };
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertProductImg(ProductImgModel insert, int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                var techicalManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.TechicalManagerLv2);
                if (!techicalManager) {
                    throw new AggregateException("Lỗi! Không có quyền thêm - sửa hình.");
                }
                if (string.IsNullOrWhiteSpace(insert.ImgUrl)) {
                    throw new AggregateException("Lỗi! Không tìm thấy hình được upload!");
                } 
                int warehouseId = 0;
                try {
                    warehouseId = Convert.ToInt32(insert.WarehouseName);
                }
                catch (Exception) { }
                if (warehouseId == 0) {
                    throw new AggregateException("Lỗi! Không tìm thấy công đoạn! Vui lòng chọn lại");
                }
                var productImg = new ProductImg() {
                    ProductId = productId,
                    ImgUrl = insert.ImgUrl,
                    Step = insert.Step,
                    Description = insert.Description,
                    WarehouseId = warehouseId,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name
                };
                using (var vfi = new tammaContext()) {
                    vfi.ProductImgs.Add(productImg);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductImg", ex.Message);
            }
            return View(new GridModel(GetProductImgById(productId)));
        }

        [GridAction]
        public ActionResult UpdateProductImg(ProductImgModel update, int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                var techicalManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.TechicalManagerLv2);
                if (!techicalManager)
                    throw new AggregateException("Lỗi! Không có quyền thêm - sửa hình.");
                using (var vfi = new tammaContext()) {
                    var productImg = vfi.ProductImgs.FirstOrDefault(pi => pi.ImgId == update.ImgId);
                    if (productImg == null)
                        throw new AggregateException("Lỗi! Không tìm thấy bản vẽ sản phẩm!");

                    if (!string.IsNullOrWhiteSpace(update.ImgUrl)) {
                        var productImgs = vfi.ProductImgs.Where(pi => pi.ImgUrl.Equals(productImg.ImgUrl));
                        if (!productImgs.Any()) {
                            DeleteProductImg(productImg.ImgUrl);
                        }
                        productImg.ImgUrl = update.ImgUrl;
                    }
                    int warehouseId = 0;
                    try {
                        warehouseId = Convert.ToInt32(update.WarehouseName);
                    }
                    catch (Exception) { }
                    if (warehouseId > 0) {
                        productImg.WarehouseId = warehouseId;
                    }
                    productImg.Description = update.Description;
                    productImg.Step = update.Step;
                    productImg.ModifiedDate = DateTime.Now;
                    productImg.ModifiedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductImg", ex.Message);
            }
            return View(new GridModel(GetProductImgById(productId)));
        }
        [GridAction]
        public ActionResult DeleteProductImg(ProductImgModel delete, int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                var techicalManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.TechicalManagerLv2);
                if (!techicalManager)
                    throw new AggregateException("Lỗi! Không có quyền thêm - sửa hình.");
                using (var vfi = new tammaContext()) {
                    var productImg = vfi.ProductImgs.FirstOrDefault(pi => pi.ImgId == delete.ImgId);
                    if (productImg == null)
                        throw new AggregateException("Lỗi! Không tìm thấy bản vẽ sản phẩm!");
                    var productImgs =
                        vfi.ProductImgs.Where(pi => pi.ImgUrl.Equals(productImg.ImgUrl) && pi.ProductId != productId);
                    if (!productImgs.Any())
                        DeleteProductImg(productImg.ImgUrl);
                    vfi.ProductImgs.Remove(productImg);
                    vfi.SaveChanges();

                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteProductImg", ex.Message);
            }
            return View(new GridModel(GetProductImgById(productId)));
        }

        void DeleteProductImg(string imgUrl) {
            var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/ProductImg"),
                imgUrl);
            if (System.IO.File.Exists(@destinationPath)) {
                System.IO.File.Delete(@destinationPath);
            }
        }
        #endregion



        #region product img For QC

        //[GridAction]
        //public ActionResult SelectProductImgById2(int productId) {
        //    var model = new List<ProductImgModel>();
        //    try {
        //        //var techicalManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.TechicalManagerLv2);
        //        model = GetProductImgById2(productId);
        //    }
        //    catch (Exception ex) {
        //        ModelState.AddModelError("SelectProductImgById2", ex.Message);
        //    }
        //    return View(new GridModel(model));
        //}

        List<ProductImgModel> GetProductImgById2(int productId) {
            var model = new List<ProductImgModel>();
            //var techicalManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.TechicalManagerLv2);
            //if (!techicalManager)
            //    return model;

            using (var vfi = new tammaContext()) {
                var productImgs = vfi.ProductImgs.Where(p => p.ProductId == productId).OrderBy(p => p.Step);
                foreach (var productImg in productImgs) {
                    var entity = new ProductImgModel {
                        ImgId = productImg.ImgId,
                        ProductId = productId,
                        ProductCode = productImg.Product.ProductCode,
                        ImgUrl = productImg.ImgUrl,
                        Step = productImg.Step,
                        Description = productImg.Description,
                        ModifiedDate = productImg.ModifiedDate,
                        ModifiedUser = productImg.ModifiedUser,
                        CanModify = true,
                        WarehouseId = productImg.WarehouseId,
                        WarehouseName = productImg.Warehouse.WarehouseName
                    };
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertProductImg2(ProductImgModel insert, int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                //var techicalManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                //    MyUtilities.UserRole.TechicalManagerLv2);
                //if (!techicalManager) {
                //    throw new AggregateException("Lỗi! Không có quyền thêm - sửa hình.");
                //}
                if (string.IsNullOrWhiteSpace(insert.ImgUrl)) {
                    throw new AggregateException("Lỗi! Không tìm thấy hình được upload!");
                }
                int warehouseId = 7;
                //try {
                //    //warehouseId = Convert.ToInt32(insert.WarehouseName);
                //}
                //catch (Exception) { }
                if (warehouseId == 0) {
                    throw new AggregateException("Lỗi! Không tìm thấy công đoạn! Vui lòng chọn lại");
                }
                var productImg = new ProductImg() {
                    ProductId = productId,
                    ImgUrl = insert.ImgUrl,
                    Step = insert.Step,
                    Description = insert.Description,
                    WarehouseId = warehouseId,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name
                };
                using (var vfi = new tammaContext()) {
                    vfi.ProductImgs.Add(productImg);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductImg", ex.Message);
            }
            return View(new GridModel(GetProductImgById2(productId)));
        }

        //[GridAction]
        //public ActionResult UpdateProductImg2(ProductImgModel update, int productId) {
        //    try {
        //        if (!Request.IsAuthenticated) {
        //            throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
        //                                     "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
        //                                     "Xin vui lòng đăng nhập lại hệ thống.");
        //        }
        //        //var techicalManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
        //        //    MyUtilities.UserRole.TechicalManagerLv2);
        //        //if (!techicalManager)
        //        //    throw new AggregateException("Lỗi! Không có quyền thêm - sửa hình.");
        //        using (var vfi = new tammaContext()) {
        //            var productImg = vfi.ProductImgs.FirstOrDefault(pi => pi.ImgId == update.ImgId);
        //            if (productImg == null)
        //                throw new AggregateException("Lỗi! Không tìm thấy bản vẽ sản phẩm!");

        //            if (!string.IsNullOrWhiteSpace(update.ImgUrl)) {
        //                var productImgs = vfi.ProductImgs.Where(pi => pi.ImgUrl.Equals(productImg.ImgUrl));
        //                if (!productImgs.Any()) {
        //                    DeleteProductImg(productImg.ImgUrl);
        //                }
        //                productImg.ImgUrl = update.ImgUrl;
        //            }
        //            int warehouseId = 0;
        //            try {
        //                warehouseId = Convert.ToInt32(update.WarehouseName);
        //            }
        //            catch (Exception) { }
        //            if (warehouseId > 0) {
        //                productImg.WarehouseId = warehouseId;
        //            }
        //            productImg.Description = update.Description;
        //            productImg.Step = update.Step;
        //            productImg.ModifiedDate = DateTime.Now;
        //            productImg.ModifiedUser = HttpContext.User.Identity.Name;
        //            vfi.SaveChanges();
        //        }
        //        MyUtilities.Product.UpdateProductDesign(productId);
        //    }
        //    catch (Exception ex) {
        //        ModelState.AddModelError("UpdateProductImg", ex.Message);
        //    }
        //    return View(new GridModel(GetProductImgById(productId)));
        //}
        //[GridAction]
        //public ActionResult DeleteProductImg2(ProductImgModel delete, int productId) {
        //    try {
        //        if (!Request.IsAuthenticated) {
        //            throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
        //                                     "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
        //                                     "Xin vui lòng đăng nhập lại hệ thống.");

        //        }

        //        using (var vfi = new tammaContext()) {
        //            var productImg = vfi.ProductImgs.FirstOrDefault(pi => pi.ImgId == delete.ImgId);
        //            if (productImg == null)
        //                throw new AggregateException("Lỗi! Không tìm thấy bản vẽ sản phẩm!");
        //            var productImgs =
        //                vfi.ProductImgs.Where(pi => pi.ImgUrl.Equals(productImg.ImgUrl) && pi.ProductId != productId);
        //            if (!productImgs.Any())
        //                DeleteProductImg(productImg.ImgUrl);
        //            vfi.ProductImgs.Remove(productImg);
        //            vfi.SaveChanges();

        //        }
        //        MyUtilities.Product.UpdateProductDesign(productId);
        //    }
        //    catch (Exception ex) {
        //        ModelState.AddModelError("DeleteProductImg", ex.Message);
        //    }
        //    return View(new GridModel(GetProductImgById(productId)));
        //}


        #endregion


        #region Qc Img
        [GridAction]
        public ActionResult SelectQcImgById2(int productId, int type) {
            var model = new List<QcImgModel>();
            try {
                model = GetQcImgById2(productId, type);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectQcImgById", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<QcImgModel> GetQcImgById2(int productId, int type) {
            var model = new List<QcImgModel>();

            using (var vfi = new tammaContext()) {
                var qcImgs = vfi.QcImgs.Where(t => t.ProductId == productId && t.Type == type && t.Active == true).OrderBy(t => t.ImgId);
                foreach (var qcImg in qcImgs) {
                    var entity = new QcImgModel {
                        ImgId = qcImg.ImgId,
                        ImgUrl = qcImg.ImgUrl,
                        ModifiedDate = qcImg.ModifiedDate,
                        ModifiedUser = qcImg.ModifiedUser,
                        Name = qcImg.Name,
                        ProductId = productId,
                        Type = type,
                    };
                    model.Add(entity);
                }
            }
            return model;
        }



        [GridAction]
        public ActionResult SelectQcImgById(int productId, int type) {
            var model = new List<QcImgModel>();
            try {
                model = GetQcImgById(productId, type);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectQcImgById", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<QcImgModel> GetQcImgById(int productId, int type) {
            var model = new List<QcImgModel>();
            //var qcManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.QcManager);
            using (var vfi = new tammaContext()) {
                var qcImgs = vfi.QcImgs.Where(t => t.ProductId == productId && t.Type == type).OrderBy(t => t.ImgId);
                foreach (var qcImg in qcImgs) {
                    var entity = new QcImgModel {
                        ImgId = qcImg.ImgId,
                        ImgUrl = qcImg.ImgUrl,
                        ModifiedDate = qcImg.ModifiedDate,
                        ModifiedUser = qcImg.ModifiedUser,
                        CanModify = true,
                        Name = qcImg.Name,
                        ProductId = productId,
                        Type = type,
                        ProductCode = qcImg.Product.ProductCode,
                        Active = qcImg.Active,
                    };
                    model.Add(entity);
                }

            }
            return model;
        }

        [GridAction]
        public ActionResult InsertQcImg(QcImgModel insert, int productId, int type) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                //var qcManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.QcManager);
                //if (!qcManager) {
                //    throw new AggregateException("Lỗi! Không có quyền thêm - sửa hình.");
                //}
                if (string.IsNullOrWhiteSpace(insert.ImgUrl)){
                    throw new AggregateException("Lỗi! Không tìm thấy hình được upload!");
                }
                insert.ProductId = productId;
                insert.Type = type;

                var qcImg = new QcImg(){
                    ProductId = productId,
                    ImgId = insert.ImgId,
                    ImgUrl = insert.ImgUrl,
                    Name = insert.Name,
                    Type = type,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name,
                    Active = false,
                };
                using (var vfi = new tammaContext()) {
                    vfi.QcImgs.Add(qcImg);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                var errorMessage = ex.Message;
                if (ex.InnerException != null) {
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null) {
                        errorMessage += " | Inner Inner Exception: " + ex.InnerException.InnerException.Message;
                    }
                }
                ModelState.AddModelError("InsertQcImg", errorMessage);
            }
            return View(new GridModel(GetQcImgById(productId, type)));
            
        }

        [GridAction]
        public ActionResult UpdateQcImg(QcImgModel update, int productId, int type) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                //var qcManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.QcManager);
                //if (!qcManager){
                //    throw new AggregateException ("Lỗi! Không có quyền thêm - sửa hình.");
                //}
                using (var vfi = new tammaContext()) {
                    var qcImg = vfi.QcImgs.FirstOrDefault(t => t.ImgId == update.ImgId && type == t.Type);
                    if (qcImg == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy bản vẽ sản phẩm!");
                    }
                    if(!string.IsNullOrWhiteSpace(update.ImgUrl)){
                        var qcImgs = vfi.QcImgs.Where(t => t.ImgUrl.Equals(qcImg.ImgUrl));
                        if (!qcImgs.Any()){
                            DeleteQcImg(qcImg.ImgUrl);
                        }
                        qcImg.ImgUrl = update.ImgUrl;
                    }
                    qcImg.Name = update.Name;
                    qcImg.ModifiedUser = HttpContext.User.Identity.Name;
                    qcImg.ModifiedDate = DateTime.Now;
                    vfi.SaveChanges();

                }
                MyUtilities.Product.UpdateProductDesign(productId);


            }
            catch (Exception ex){
                ModelState.AddModelError("UpdateQcImg", ex.Message);
            }

            return View(new GridModel(GetQcImgById(productId, type)));
        }

        [GridAction]
        public ActionResult DeleteQcImg(QcImgModel delete, int productId, int type) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                //var techicalManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                //    MyUtilities.UserRole.TechicalManagerLv2);
                //if (!techicalManager)
                //    throw new AggregateException("Lỗi! Không có quyền thêm - sửa hình.");
                using (var vfi = new tammaContext()) {
                    var qcImg = vfi.QcImgs.FirstOrDefault(pi => pi.ImgId == delete.ImgId && pi.Type == type);
                    if (qcImg == null)
                        throw new AggregateException("Lỗi! Không tìm thấy bản vẽ sản phẩm!");
                    var qcImgs =
                        vfi.QcImgs.Where(pi => pi.ImgUrl.Equals(qcImg.ImgUrl) && pi.ProductId != productId);
                    if (!qcImgs.Any())
                        DeleteQcImg(qcImg.ImgUrl);
                    vfi.QcImgs.Remove(qcImg);
                    vfi.SaveChanges();

                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteQcImg", ex.Message);
            }
            return View(new GridModel(GetQcImgById(productId, type)));
        }

        void DeleteQcImg(string imgUrl) {
            var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/QcImg"),
                imgUrl);
            if (System.IO.File.Exists(@destinationPath)) {
                System.IO.File.Delete(@destinationPath);
            }
        }


        public ActionResult ActiveImg(int ImgId) {
            try {
                using (var vfi = new tammaContext()) {
                    var Img = vfi.QcImgs.FirstOrDefault(x => x.ImgId== ImgId);
                    if (Img == null) {
                        throw new AggregateException("Không tìm thấy ảnh");
                    }
                    Img.Active = !Img.Active;
                    vfi.SaveChanges();
                    //return View(new GridModel(GetQcImgById(Img.ProductId)));
                    return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "", Img.ImgId));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.Exception, ex.Message, ""));
            }
        }




        #endregion


        #region production material

        [GridAction]
        public ActionResult SelectProductMaterialDesignById(int customerId, int productId, string productCode,bool isQuote) {
            var model = new List<ProductMaterialDesignModel>();
            try {
                model = GetProductMaterialDesignById(customerId, productId, productCode, isQuote);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductMaterialDesignById", ex.Message);
            }
            return View(new GridModel(model));
        }
        List<ProductMaterialDesignModel> GetProductMaterialDesignById(int customerId, int productId, string productCode, bool isQuote) {
            var model = new List<ProductMaterialDesignModel>();
            using (var vfi = new tammaContext()) {
                var products = vfi.Products.Where(x => 
                    (customerId == 0 || x.CustomerId == customerId) 
                    && x.Active
                    && (productId==0|| x.ProductId == productId)).ToList();
                if (!string.IsNullOrWhiteSpace(productCode)) {
                    products = products.Where(x => x.ProductCode.Contains(productCode)).ToList();
                }

                foreach (var product in products) {
                    var entity = new ProductMaterialDesignModel { 
                        ProductId = product.ProductId,
                    };
                    if (product.MaterialId != null) {
                        entity.MaterialId = product.MaterialId.Value;
                        entity.MaterialCode = product.Material.MaterialCode;
                        entity.MaterialName = product.Material.MaterialName;
                        entity.OutDiameter = product.Material.OutDiameter;
                        entity.InDiameter = product.Material.InDiameter;
                        entity.Shape = product.Material.Shape.Trim();
                        entity.DiameterType = product.Material.DiameterType.Trim();
                        entity.ProductMaterialWeight = Math.Round(product.Weight ?? 0, 4);
                        if (isQuote) {
                            var materialQuoteBase = product.Material.MaterialType.MaterialQuoteBases.FirstOrDefault(x =>
                                x.MaterialName.Equals(entity.MaterialName)
                                && x.MaterialShape.Equals(entity.Shape)
                                && x.MaterialDiameterType.Equals(entity.DiameterType));
                            if (materialQuoteBase != null) {
                                entity.MaterialBasePrice = materialQuoteBase.BasePrice;
                                var onSize = materialQuoteBase.MaterialQuoteBaseDetails
                                    .FirstOrDefault(x => x.FromOutDiameter <= entity.OutDiameter
                                                      && x.ToOutDiameter > entity.OutDiameter);
                                if (onSize != null) {
                                    entity.MaterialBasePrice += onSize.Value;
                                }
                            }
                        }
                    }
                    entity.MaterialPrice = Math.Round(entity.ProductMaterialWeight * entity.MaterialBasePrice / 1000, 4);
                    model.Add(entity);
                }
            }
            return model;
        }


        [GridAction]
        public ActionResult UpdateProductMaterialDesign(ProductMaterialDesignModel update,
            int customerId, int productId, string productCode, bool isQuote) {
            try {
                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == update.ProductId);
                    if (product == null)
                        throw new AggregateException("Lỗi hệ thống");
                    int materialId = 0;
                    try {
                        materialId = Convert.ToInt32(update.MaterialCode);
                    }
                    catch (Exception) { }
                    var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == materialId);
                    if (material != null) {
                        product.MaterialId = materialId;
                        product.MaterialNameDesign = material.MaterialName;
                        product.OutDiameterDesign = material.OutDiameter;
                        product.InDiameterDesign = material.InDiameter;
                        product.ShapeDesign = (material.Shape + "").Trim();
                        product.DiameterTypeDesign = (material.DiameterType + "").Trim();
                        var productionMaterial =
                            vfi.ProductionMaterials.FirstOrDefault(
                                pm => pm.ProductId == product.ProductId && pm.MaterialId == materialId);
                        if (productionMaterial == null) {
                            productionMaterial = new ProductionMaterial() {
                                ProductId = product.ProductId,
                                MaterialId = materialId,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                Active = true,
                                Priority = 0,
                                Note = "Auto",
                            };
                            vfi.ProductionMaterials.Add(productionMaterial);
                        }
                        else if (!productionMaterial.Active) {
                            productionMaterial.Active = true;
                        }
                        productionMaterial.UnitWeightByMaterial =
                            MyUtilities.Product.GetProductWeight(product.MaterialNameDesign,
                                product.OutDiameterDesign ?? 0,
                                product.InDiameterDesign ?? 0,
                                product.Length ?? 0,
                                product.KnifeCut ?? 0,
                                product.ShapeDesign);
                        product.Weight = productionMaterial.UnitWeightByMaterial;
                    }
                    vfi.SaveChanges();
                    product.FinishDesign = MyUtilities.Product.CheckDesign(product.ProductId);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductMaterialDesign", ex.Message);
            }
            return View(new GridModel(GetProductMaterialDesignById(customerId, productId, productCode, isQuote)));
        }

        [GridAction]
        public ActionResult SelectProductionMaterialById(int productId) {
            var model = new List<ProductionMaterialModel>();
            try {
                model = GetMaterialListByProductId(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionMaterialById", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionMaterialModel> GetMaterialListByProductId(int productId) {
            var model = new List<ProductionMaterialModel>();
            using (var vfi = new tammaContext()) {
                var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                if (user == null)
                    throw new AggregateException("Lỗi! Mất đăng nhập! Vui lòng đăng nhập lại!");
                var materials =
                    vfi.ProductionMaterials.Where(pm => pm.ProductId == productId)
                       .OrderBy(pm => pm.Priority);
                foreach (var material in materials) {
                    var entity = new ProductionMaterialModel {
                        RealMaterialId = material.RealMaterialId,
                        MaterialId = material.MaterialId,
                        MaterialCode = material.Material.MaterialCode,
                        ProductId = material.ProductId,
                        Priority = material.Priority,
                        Note = material.Note,
                        UnitWeightByMaterial = MyUtilities.Product.GetProductWeight(material.Material.MaterialName,
                                                                                    material.Material.OutDiameter,
                                                                                    material.Material.InDiameter,
                                                                                    material.Product.Length ?? 0,
                                                                                    material.Product.KnifeCut ?? 0,
                                                                                    material.Material.Shape + ""),
                        ProductWeight = material.UnitWeightByMaterial,
                        Active = material.Active,
                        ModifiedDate = material.ModifiedDate,
                        ModifiedUser = material.ModifiedUser,
                    };
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.Priority).ToList();
        }

        [GridAction]
        public ActionResult InsertProductionMaterial(ProductionMaterialModel insert,
            int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(insert.MaterialCode))
                    throw new AggregateException("Lỗi! Nhập tên nguyên liệu!");
                int materialId = 1;
                try {
                    materialId = Convert.ToInt32(insert.MaterialCode);
                }
                catch (Exception) {
                    throw new AggregateException("Lỗi mã nguyên liệu ! Chọn lại nguyên liệu");
                }
                using (var vfi = new tammaContext()) {
                    var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == materialId);
                    var product = vfi.Products.FirstOrDefault(m => m.ProductId == productId);
                    var entity =
                        vfi.ProductionMaterials.FirstOrDefault(
                            pm => pm.MaterialId == materialId && pm.ProductId == productId);
                    if (entity == null) {
                        entity = new ProductionMaterial {
                            ProductId = productId,
                            Priority = insert.Priority,
                            Note = insert.Note + "",
                            MaterialId = materialId,
                            UnitWeightByMaterial = MyUtilities.Product
                                .GetProductWeight(material.MaterialName,
                                                  material.OutDiameter,
                                                  material.InDiameter,
                                                  product.Length ?? 0,
                                                  product.KnifeCut ?? 0,
                                                  material.Shape + ""),
                            Active = true,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                        };
                        vfi.ProductionMaterials.Add(entity);
                    }
                    else {
                        entity.MaterialId = materialId;
                        entity.Priority = insert.Priority;
                        entity.Note = insert.Note + "";
                        entity.UnitWeightByMaterial = MyUtilities.Product
                            .GetProductWeight(material.MaterialName,
                                              material.OutDiameter,
                                              material.InDiameter,
                                              entity.Product.Length ?? 0,
                                              entity.Product.KnifeCut ?? 0,
                                              material.Shape + "");
                        entity.Active = true;
                        entity.ModifiedDate = DateTime.Now;
                        entity.ModifiedUser = HttpContext.User.Identity.Name;
                    }
                    if (product.MaterialId == null) {
                        product.MaterialId = materialId;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionMaterial", MyUtilities.MySystem.FetchExceptionMessage(ex));
            }
            return View(new GridModel(GetMaterialListByProductId(productId)));
        }

        [GridAction]
        public ActionResult UpdateProductionMaterial(ProductionMaterialModel update) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var entity = vfi.ProductionMaterials.FirstOrDefault(ps => ps.RealMaterialId == update.RealMaterialId);
                    if (entity != null) {
                        int materialId = 1;
                        try {
                            materialId = Convert.ToInt32(update.MaterialCode);
                        }
                        catch (Exception) {
                            throw new AggregateException("Lỗi mã nguyên liệu ! Chọn lại nguyên liệu");
                        }
                        var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == materialId);
                        entity.MaterialId = materialId;
                        entity.Priority = update.Priority;
                        entity.Note = update.Note + "";
                        entity.UnitWeightByMaterial = MyUtilities.Product
                            .GetProductWeight(material.MaterialName,
                                              material.OutDiameter,
                                              material.InDiameter,
                                              entity.Product.Length ?? 0,
                                              entity.Product.KnifeCut ?? 0,
                                              material.Shape + "");
                        entity.Active = update.Active;
                        entity.ModifiedDate = DateTime.Now;
                        entity.ModifiedUser = HttpContext.User.Identity.Name;
                    }
                    //vfi.ProductionSections.Add(section);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionMaterial", ex.Message);
            }
            return View(new GridModel(GetMaterialListByProductId(update.ProductId)));
        }
        #endregion
        
        #region product combination recipe

        [GridAction]
        public ActionResult SelectProductCombinationRecipe(int productId) {
            var model = new List<ProductCombinationRecipeModel>();
            try {
                model = GetProductCombinationRecipe(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductCombinationRecipe", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductCombinationRecipeModel> GetProductCombinationRecipe(int productId) {
            var model = new List<ProductCombinationRecipeModel>();
            using (var vfi = new tammaContext()) {
                var recipes = vfi.ProductCombinationRecipes.Where(x => x.ProductId == productId);
                foreach (var recipe in recipes) {
                    var entity = new ProductCombinationRecipeModel() {
                        RecipeId = recipe.RecipeId,
                        RecipeCode = recipe.RecipeCode,
                        RecipeName = recipe.RecipeName,
                        Active = recipe.Active,
                        ProductId = productId,
                        ModifiedUser = recipe.ModifiedUser,
                        ModifiedDate = recipe.ModifiedDate,
                    };
                    if (recipe.ProductCombinationRecipeDetails.Any()) {
                        var details = recipe.ProductCombinationRecipeDetails.Select(x =>
                            new ProductCombinationRecipeDetailModel { 
                                FromProductId = x.FromProductId,
                                FromProductCode = x.Product.ProductCode,
                                RequireNumber = x.RequireNumber
                            }).ToList();
                        entity.DetailDescription = ProductCombinationRecipeNote.GetDetailDescription(details);
                    }
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertProductCombinationRecipe(ProductCombinationRecipeModel insert, int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                var recipe = new ProductCombinationRecipe {
                    RecipeCode = (insert.RecipeCode + "").Trim(),
                    RecipeName = (insert.RecipeName + "").Trim(),
                    ProductId = productId,
                    Active = true,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name,
                };
                using (var vfi = new tammaContext()) {
                    vfi.ProductCombinationRecipes.Add(recipe);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductCombinationRecipe", ex.Message);
            }
            return View(new GridModel(GetProductCombinationRecipe(productId)));
        }

        [GridAction]
        public ActionResult UpdateProductCombinationRecipe(ProductCombinationRecipeModel update, int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var recipe = vfi.ProductCombinationRecipes.FirstOrDefault(x => x.RecipeId == update.RecipeId);
                    if (recipe == null) throw new AggregateException("Lỗi! Không tìm thấy công thức ghép");
                    recipe.RecipeCode = update.RecipeCode;
                    recipe.RecipeName = update.RecipeName;
                    recipe.Active = update.Active;
                    recipe.ModifiedUser = HttpContext.User.Identity.Name;
                    recipe.ModifiedDate = DateTime.Now;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductCombinationRecipe", ex.Message);
            }
            return View(new GridModel(GetProductCombinationRecipe(productId)));
        }

        [GridAction]
        public ActionResult SelectProductCombinationRecipeDetail(int recipeId) {
            var model = new List<ProductCombinationRecipeDetailModel>();
            try {
                model = GetProductCombinationRecipeDetail(recipeId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductCombinationRecipeDetail", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductCombinationRecipeDetailModel> GetProductCombinationRecipeDetail(int recipeId) {
            var model = new List<ProductCombinationRecipeDetailModel>();
            using (var vfi = new tammaContext()) {
                var recipeDetails = vfi.ProductCombinationRecipeDetails.Where(x => x.RecipeId == recipeId);
                foreach (var detail in recipeDetails) {
                    var entity = new ProductCombinationRecipeDetailModel {
                        DetailId = detail.DetailId,
                        FromProductId = detail.FromProductId,
                        FromProductCode = detail.Product.ProductCode,
                        RequireNumber = detail.RequireNumber,
                        RecipeId = recipeId
                    };
                    model.Add(entity);
                }
            }
            return model.OrderBy(x => x.FromProductCode).ToList();
        }

        [GridAction]
        public ActionResult InsertProductCombinationRecipeDetail(ProductCombinationRecipeDetailModel insert, int recipeId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (insert.RequireNumber <= 0) {
                    throw new AggregateException("Vui lòng nhập số lượng >0");
                }
                int fromProductId = 0;
                try {
                    fromProductId = Convert.ToInt32(insert.FromProductCode);
                }
                catch (Exception) {
                }
                if (fromProductId == 0) {
                    throw new AggregateException("Lỗi sản phẩm ! Chọn lại sản phẩm.");
                }
                using (var vfi = new tammaContext()) {
                    var recipe = vfi.ProductCombinationRecipes.FirstOrDefault(x => x.RecipeId == recipeId);
                    if (recipe == null) {
                        throw new AggregateException("Lỗi ! Không tìm thấy công thức ghép");
                    }
                    if (recipe.ProductId == fromProductId) {
                        throw new AggregateException("Lỗi ! Sản phẩm trong công thức không được trùng với sản phẩm ghép");
                    }
                    var existedDetail = vfi.ProductCombinationRecipeDetails.FirstOrDefault(x => x.RecipeId == recipeId
                        && x.FromProductId == fromProductId);
                    if (existedDetail != null) {
                        throw new AggregateException("Lỗi ! Sản phẩm trong công thức đã có");
                    }
                    var entity = new ProductCombinationRecipeDetail {
                        RecipeId = recipeId,
                        FromProductId = fromProductId,
                        RequireNumber = insert.RequireNumber
                    };
                    vfi.ProductCombinationRecipeDetails.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductCombinationRecipeDetail", ex.Message);
            }
            return View(new GridModel(GetProductCombinationRecipeDetail(recipeId)));
        }

        [GridAction]
        public ActionResult UpdateProductCombinationRecipeDetail(ProductCombinationRecipeDetailModel update, int recipeId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    var recipeDetail = vfi.ProductCombinationRecipeDetails.FirstOrDefault(x => x.DetailId == update.DetailId);
                    if (recipeDetail == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết ! Vui lòng F5 lại");
                    }

                    if (update.RequireNumber <= 0) {
                        vfi.ProductCombinationRecipeDetails.Remove(recipeDetail);
                    }
                    else {
                        int fromProductId = 0;
                        try {
                            fromProductId = Convert.ToInt32(update.FromProductCode);
                        }
                        catch (Exception) { }
                        if (fromProductId > 0) {
                            var recipe = vfi.ProductCombinationRecipes.FirstOrDefault(x => x.RecipeId == recipeId);
                            if (recipe == null) {
                                throw new AggregateException("Lỗi ! Không tìm thấy công thức ghép");
                            }
                            if (recipe.ProductId == fromProductId) {
                                throw new AggregateException("Lỗi ! Sản phẩm trong công thức không được trùng với sản phẩm ghép");
                            }
                            var existedDetail = vfi.ProductCombinationRecipeDetails.FirstOrDefault(x => x.RecipeId == recipeId
                                && x.FromProductId == fromProductId);
                            if (existedDetail != null) {
                                throw new AggregateException("Lỗi ! Sản phẩm trong công thức đã có");
                            }
                            recipeDetail.FromProductId = fromProductId;
                        }
                        recipeDetail.RequireNumber = update.RequireNumber;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductCombinationRecipeDetail", ex.Message);
            }
            return View(new GridModel(GetProductCombinationRecipeDetail(recipeId)));
        }
        #endregion

        #region product quote

        [GridAction]
        public ActionResult SelectProductQuoteCalculate(int customerId, string productCode, bool isLock = false) {
            if (customerId == 0 && string.IsNullOrWhiteSpace(productCode))
                return View(new GridModel(new List<ProductQuoteCalculateModel>()));
            var model = new List<ProductQuoteCalculateModel>();
            try {
                model = GetProductQuoteCalculate(customerId, productCode, 0, new List<int>(), isLock);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductQuoteCalculate", ex.Message);
            }
            return View(new GridModel(model));
        }

        public List<ProductQuoteCalculateModel> GetProductQuoteCalculate(
            int customerId, string productCode, int productId, 
            List<int> productIds, bool isLock) {
            var model = new List<ProductQuoteCalculateModel>();
            var salesManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.SaleManagementLv2);
            using (var vfi = new tammaContext()) {
                var products = vfi.Products.Where(x => (customerId == 0 || x.CustomerId == customerId)
                    && (productId == 0 || x.ProductId == productId)
                    && (!isLock || x.IsCalculateLock == isLock)
                    && (!productIds.Any() || productIds.Contains(x.ProductId))
                    && x.Active).ToList();
                if (!string.IsNullOrWhiteSpace(productCode)) { 
                    products = products.Where(x => x.ProductCode.Contains(productCode)).ToList(); 
                }
                //var materialTypeIds = products.Where(x => x.MaterialId != null).Select(x => x.Material.MaterialTypeId).ToList();
                //var materialTypes = 
                foreach (var product in products) {
                    var entity = new ProductQuoteCalculateModel {
                        CustomerId = product.CustomerId,
                        ProductId = product.ProductId,
                        ProductCode = product.ProductCode,
                        PackingPrice = (product.PackingFee ?? 0),
                        UnitWeight = product.QcWeight ?? 1,
                        IsCalculateLock = product.IsCalculateLock ?? false,
                        CanLock = salesManager
                    };
                    if (product.MaterialId != null) {
                        //entity.mater
                        var baseMaterialPrices = product.Material.MaterialType.MaterialQuoteBases.ToList();
                        var baseMaterialPrice = baseMaterialPrices.FirstOrDefault(x =>
                            x.MaterialName.Equals(product.Material.MaterialName)
                            && x.MaterialShape.Equals(product.Material.Shape.Trim())
                            && x.MaterialDiameterType.Equals(product.Material.DiameterType.Trim()));
                        if (baseMaterialPrice != null) {
                            var basePrice = baseMaterialPrice.BasePrice;
                            var onSizeMaterial = baseMaterialPrice.MaterialQuoteBaseDetails
                                .FirstOrDefault(x => x.FromOutDiameter <= product.Material.OutDiameter
                                                  && x.ToOutDiameter > product.Material.OutDiameter);
                            if (onSizeMaterial != null) {
                                basePrice += onSizeMaterial.Value;
                            }
                            entity.MaterialPrice = Math.Round((product.Weight ?? 0) * basePrice / 1000, 4);
                        }
                        entity.MaterialTypeFactor = product.Material.MaterialType.Factor ?? 1;
                        entity.MaterialProductionFactor = product.Material.MaterialType.ProductionFactor ?? 1;
                        entity.MaterialTaxFactor = product.Material.MaterialType.TaxFactor ?? 1;
                        if (product.Material.IsExpensive) {
                            entity.MaterialExpensivePrice = entity.MaterialPrice * 2;
                        }
                    }

                    //entity.Productivity = product.ProductionProductivityQuoteBases.Where(x => x.Active).Sum(x => x.Time);
                    entity.Productivity = product.Productivity ?? 0;
                    entity.MachineClassifiedFactor = product.ProcessClassifiedId != null ? (product.ProcessClassified.SalesFactor) : 0;
                    entity.ProductLevelFactor = product.ProductionLevel != null ? product.ProductionProductLevel.Factor : 1;

                    entity.Production2Price = product.ProductionSections.Where(x => x.Active)
                        .Sum(x => Math.Round(x.Productivity * x.Section.SaleFactor, 4));
                    //entity.OutsideProductionPrice = product.ProductionPlatings.Where(x => x.IsMainProcess && x.ProcessId != null)
                    //    .Sum(x => Math.Round(x.OutsideProcess.Price / entity.UnitWeight, 4));
                    entity.OutsideProductionPrice = product.ProductionPlatings.Where(x => x.Active).Sum(x => x.PlatingCost);
                    entity.ShippingPrice = (product.Customer.ShippingMethodId != null)
                                        ? Math.Round((product.Customer.ShipMethod.ShipBase ?? 0) * entity.UnitWeight / 1000, 4)
                                        : 0;
                    entity.PackingPrice = (product.PackingFee ?? 0);
                    entity.AdditionFee = product.ProductAdditionFees.Where(x => x.Active).Sum(x => x.Price);
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectProduction1QuoteCalculate(int customerId, string productCode, int productId) {
            if (customerId == 0 && string.IsNullOrWhiteSpace(productCode) && productId == 0)
                return View(new GridModel(new List<ProductQuoteCalculateModel>()));
            var model = new List<ProductQuoteCalculateModel>();
            try {
                var entity = new ProductQuoteCalculateModel { ProductId = productId};
                model.Add(entity);
                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(x => x.ProductId == productId);
                    if (product == null) {
                        return View(new GridModel(model));
                    }
                    //if (product.ProcessingDesign != null) {
                    //    entity.MachineTypeName = product.ProcessingType.TypeName;
                    //    entity.MachineTypeFactor = product.ProcessingType.ProcessingSaleFactor ?? 0;
                    //}
                    if (product.ProcessClassifiedId != null) {
                        entity.MachineTypeName = product.ProcessClassified.Name + "-" + product.ProcessClassified.Description;
                        entity.MachineClassifiedFactor = product.ProcessClassified.SalesFactor;
                    }
                    if (product.ProductionLevel != null) {
                        entity.ProductLevel = product.ProductionProductLevel.LevelName;
                        entity.ProductLevelFactor = product.ProductionProductLevel.Factor;
                    }
                    else {
                        entity.ProductLevel = "0";
                        entity.ProductLevelFactor = 1;
                    }
                    if (product.MaterialId != null) {
                        entity.MaterialTypeName = product.Material.MaterialType.MaterialTypeName;
                        entity.MaterialProductionFactor = (product.Material.MaterialType.ProductionFactor ?? 0);
                    }
                    entity.Productivity = product.Productivity ?? 0;
                    //if (product.ProductionProductivityQuoteBases.Any(x => x.Active)) {
                    //    entity.Productivity = product.ProductionProductivityQuoteBases.Where(x => x.Active).Sum(x => x.Time);
                    //}
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProduction1QuoteCalculate", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectMOQQuoteCalculate(int productId) {
            var model = new List<MOQTemplateModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var templates = vfi.MOQTemplates.Where(x => x.Active).OrderBy(x => x.FromQuantity).ToList();
                    var productCalculate = GetProductQuoteCalculate(0, "", productId, new List<int>(), false).FirstOrDefault();
                    foreach (var template in templates) {
                        var entity = new MOQTemplateModel {
                            TemplateId = template.TemplateId,
                            FromQuantity = template.FromQuantity,
                            ToQuantity = template.ToQuantity,
                            FactorDefault = template.FactorDefault,
                            QuotePrice = productCalculate.QuotePrice,
                            MOQQuotePrice = template.FactorDefault * productCalculate.QuotePrice
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMOQQuoteCalculate", ex.Message);
            }
            return View(new GridModel(model));
        }

        public ActionResult LockProductCalculate(int productId) {
            try {
                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(x => x.ProductId == productId);
                    if (product == null) {
                        return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NotFound, "Không tìm thấy", null));
                    }
                    product.IsCalculateLock = !(product.IsCalculateLock ?? false);
                    vfi.SaveChanges();
                    return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "", product.ProductCode));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.Exception, ex.Message, ""));
            }
        }
        #endregion
    }

}
