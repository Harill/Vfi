using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Web;

namespace Vfi.Ui.Mvc.Vfi.Controllers.Production {
    public class MaterialController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        [InjectionConstructor]
        public MaterialController(IUnitOfWork unitOfWork) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }
        #region view
        // Viewup
        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }
            return ViewData;
        }
        public ActionResult MaterialClassifiedManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MaterialTypeManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MaterialManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ToolManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult FuelManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MaterialQuoteBaseManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        #endregion

        #region Material Classified

        // Data
        public IEnumerable<MaterialClassifiedModel> GetMaterialClassifiedModels() {
            var model = new List<MaterialClassifiedModel>();
            using (var vfi = new tammaContext()) {
                model.AddRange(vfi.MaterialClassifieds.Select(
                    entity => new MaterialClassifiedModel {
                        MaterialClassifiedId = entity.MaterialClassifiedId,
                        MaterialClassifiedName = entity.MaterialClassifiedName,
                        Active = entity.Active,
                        ModifiedUser = entity.ModifiedUser,
                        ModifiedDate = entity.ModifiedDate
                    }));
            }
            return model.OrderByDescending(x => x.Active).ThenBy(x => x.MaterialClassifiedName);
        }

        [GridAction]
        public ActionResult SelectMaterialClassified() {
            return View(new GridModel(GetMaterialClassifiedModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertMaterialClassified(MaterialClassifiedModel insert) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("InsertMaterialClassified",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(GetMaterialClassifiedModels()));
            }
            try {
                var entity = new MaterialClassified {
                    MaterialClassifiedName = insert.MaterialClassifiedName,
                    Active = true,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name,
                };
                using (var vfi = new tammaContext()) {
                    vfi.MaterialClassifieds.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception) {
                ModelState.AddModelError("InsertMaterialClassified", @"Lỗi giá trị nhập. (try-catch)");
            }

            return View(new GridModel(GetMaterialClassifiedModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateMaterialClassified(MaterialClassifiedModel update) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("UpdateMaterialClassified",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(GetMaterialClassifiedModels()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var entity = vfi.MaterialClassifieds.FirstOrDefault(x => x.MaterialClassifiedId == update.MaterialClassifiedId);
                    if (entity == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy giá trị cập nhật");
                    }
                    entity.Active = update.Active;
                    entity.MaterialClassifiedName = update.MaterialClassifiedName;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();
                }
            }
            catch (Exception) {
                ModelState.AddModelError("UpdateMaterialClassified", @"Lỗi giá trị nhập. (try-catch)");
            }
            return View(new GridModel(GetMaterialClassifiedModels()));
        }

        public ActionResult SelectComboBoxWorkpieceMaterial() {
            return new JsonResult {
                Data = new SelectList(MaterialIdentityCode.GetMaterialIdentityCodes(0).OrderBy(l => l.IdentityCode), "IdentityCode", "MaterialTypeName")
            };
        }

        public ActionResult SelectComboBoxMaterialClassified() {
            var model = GetMaterialClassifiedModels().Where(x=> x.Active).ToList();
            return new JsonResult {
                Data = new SelectList(model, "MaterialClassifiedId", "MaterialClassifiedName")
            };
        }

        public ActionResult SelectAllComboBoxMaterialClassified() {
            var model = new List<Vfi.Models.MaterialClassified>();
            using (var vfi = new tammaContext()) {
                model.Add(new Vfi.Models.MaterialClassified { MaterialClassifiedId = 0, MaterialClassifiedName = "All" });
                model.AddRange(vfi.MaterialClassifieds);
            }
            return new JsonResult {
                Data = new SelectList(model, "MaterialClassifiedId", "MaterialClassifiedName")
            };
        }

        #endregion

        #region Material Type

        public List<MaterialTypeModel> GetMaterialTypeModels() {
            var model = new List<MaterialTypeModel>();
            try {
                using (var vfi = new tammaContext()) {
                    model = vfi.MaterialTypes.Select(
                        entity => new MaterialTypeModel {
                            MaterialTypeId = entity.MaterialTypeId,
                            MaterialClassifiedId = entity.MaterialClassifiedId,
                            MaterialClassifiedName = entity.MaterialClassified.MaterialClassifiedName,
                            MaterialTypeName = entity.MaterialTypeName,
                            Active = entity.Active,
                            ModifiedUser = entity.ModifiedUser,
                            ModifiedDate = entity.ModifiedDate,
                            IdentityCode = entity.IdentityCode,
                            DiagramColor = (entity.DiagramColor + ""),
                            Factor = entity.Factor ?? 0,
                            ProductionFactor = entity.ProductionFactor ?? 0,
                            TaxFactor = entity.TaxFactor ?? 0
                        })
                        .OrderBy(x => x.MaterialClassifiedName).ThenByDescending(x => x.Active).ThenBy(x => x.IdentityCode)
                        .ToList();

                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex);
            }
            return model;
        }
        [GridAction]
        public ActionResult SelectMaterialType() {
            var model = new List<MaterialTypeModel>();
            try {
                model = GetMaterialTypeModels();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterialType", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertMaterialType(MaterialTypeModel insert) {
            try {
                using (var vfi = new tammaContext()) {
                    int classtifiedId = 1;
                    try {
                        classtifiedId = Convert.ToInt32(insert.MaterialClassifiedName);
                    }
                    catch (Exception) {
                        classtifiedId =
                            vfi.MaterialClassifieds.FirstOrDefault(
                                c => c.MaterialClassifiedName.Equals(insert.MaterialClassifiedName))
                               .MaterialClassifiedId;
                    }
                    var entity = new Vfi.Models.MaterialType {
                        Active = true,
                        MaterialTypeName = insert.MaterialTypeName.Trim(),
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        IdentityCode = insert.IdentityCode.Trim().ToUpper(),
                        MaterialClassifiedId = classtifiedId,
                        DiagramColor = insert.DiagramColor,
                        Factor = insert.Factor,
                        ProductionFactor = insert.ProductionFactor,
                        TaxFactor = insert.TaxFactor,
                    };
                    vfi.MaterialTypes.Add(entity);
                    vfi.SaveChanges();
                }

            }
            catch (Exception exception) {
                ModelState.AddModelError("MaterialTypeName", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }

            return View(new GridModel(GetMaterialTypeModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateMaterialType(MaterialTypeModel update) {
            try {
                using (var vfi = new tammaContext()) {
                    int classtifiedId = 0;
                    try {
                        classtifiedId = Convert.ToInt32(update.MaterialClassifiedName);
                    }
                    catch (Exception) {
                        classtifiedId =
                            vfi.MaterialClassifieds.FirstOrDefault(
                                c => c.MaterialClassifiedName.Equals(update.MaterialClassifiedName))
                               .MaterialClassifiedId;
                    }
                    var entity = vfi.MaterialTypes.FirstOrDefault(mt => mt.MaterialTypeId == update.MaterialTypeId);
                    entity.MaterialClassifiedId = classtifiedId;
                    entity.Active = update.Active;
                    entity.MaterialTypeName = update.MaterialTypeName.Trim();
                    entity.IdentityCode = update.IdentityCode.Trim().ToUpper();
                    entity.DiagramColor = update.DiagramColor;
                    entity.Factor = update.Factor;
                    entity.ProductionFactor = update.ProductionFactor;
                    entity.TaxFactor = update.TaxFactor;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    entity.ModifiedDate = DateTime.Now;
                    vfi.SaveChanges();
                }

            }
            catch (Exception exception) {
                ModelState.AddModelError("MaterialTypeName", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }
            return View(new GridModel(GetMaterialTypeModels()));
        }

        public ActionResult SelectComboBoxMaterialTypeByClassified(int? classifiedId) {
            if (classifiedId == null || classifiedId == 0) {
                return new JsonResult {
                    Data = new SelectList(new List<MaterialType>(), "MaterialTypeId", "MaterialTypeName")
                };
            }
            var model = GetMaterialTypeModels().Where(x => x.Active && x.MaterialClassifiedId == classifiedId)
                .OrderBy(x => x.IdentityCode).ThenBy(x => x.MaterialTypeName);
            return new JsonResult {
                Data = new SelectList(model, "MaterialTypeId", "MaterialTypeName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult SelectComboBoxAllMaterialType() {
            using (var vfi = new tammaContext()) {
                var materialType = (from mt in vfi.MaterialTypes
                                    where mt.MaterialClassifiedId == 1 && mt.Active
                                    select new {
                                        mt.MaterialTypeId,
                                        mt.MaterialTypeName
                                    }).ToList();
                materialType.Add(new { MaterialTypeId = 0, MaterialTypeName = "Tất cả" });
                return new JsonResult {
                    Data =
                        new SelectList(materialType.OrderBy(mt => mt.MaterialTypeId), "MaterialTypeId",
                                       "MaterialTypeName"),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

        }
        public ActionResult SelectAllMaterialTypeByClassified(int? classifiedId) {
            var model = new List<Vfi.Models.MaterialType>();
            if (classifiedId == null) {
                return new JsonResult {
                    Data = new SelectList(model, "MaterialTypeId", "MaterialTypeName")
                };
            }
            using (var vfi = new tammaContext()) {

                if (classifiedId == 0) {
                    model.Add(new Vfi.Models.MaterialType { MaterialTypeId = 0, MaterialTypeName = "All Type" });
                    model.AddRange(vfi.MaterialTypes);
                }
                else {
                    model = vfi.MaterialTypes.Where(mt => mt.MaterialClassifiedId == classifiedId).ToList();
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "MaterialTypeId", "MaterialTypeName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #region Material

        public List<MaterialModel> GetMaterialModels(string productCode) {
            var model = new List<MaterialModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var productIds = string.IsNullOrWhiteSpace(productCode)
                                       ? new List<int>()
                                       : vfi.Products.Where(p => p.ProductCode.Contains(productCode))
                                            .Select(p => p.ProductId)
                                            .ToList();
                    var materialIds = productIds.Count == 0
                                          ? new List<int>()
                                          : vfi.ProductionMaterials.Where(rp => productIds.Contains(rp.ProductId))
                                               .Select(p => p.MaterialId)
                                               .ToList();
                    var materials = !string.IsNullOrWhiteSpace(productCode) && materialIds.Count == 0
                                        ? new List<Models.Material>()
                                        : materialIds.Count == 0
                                              ? vfi.Materials.ToList()
                                              : vfi.Materials.Where(m => materialIds.Contains(m.MaterialId)).ToList();

                    model = materials.Select(entity => new MaterialModel {
                        MaterialId = entity.MaterialId,
                        MaterialCode = entity.MaterialCode,
                        MaterialName = entity.MaterialName,
                        MaterialTypeId = entity.MaterialTypeId,
                        MaterialTypeName = entity.MaterialType != null ? entity.MaterialType.MaterialTypeName : "",
                        IdentityCode = entity.MaterialType != null ? entity.MaterialType.IdentityCode : "",
                        OutDiameter = entity.OutDiameter,
                        InDiameter = entity.InDiameter,
                        Shape = entity.Shape.Trim(),
                        DiameterType = entity.DiameterType,
                        Weight = entity.Weight,
                        UnitPrice = entity.UnitPrice,
                        Active = entity.Active,
                        ModifiedUser = entity.ModifiedUser,
                        ModifiedDate = entity.ModifiedDate,
                        IsExpensive = entity.IsExpensive,
                    }).ToList();
                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex);
            }
            return model.OrderBy(m => m.IdentityCode)
                            .ThenBy(m => m.MaterialName)
                            .ThenBy(m => m.Shape)
                            .ThenBy(m => m.DiameterType)
                            .ThenBy(m => m.InDiameter)
                            .ThenBy(m => m.OutDiameter).ToList();
        }
        [GridAction]
        public ActionResult SelectMaterial(string productCode) {
            var model = new List<MaterialModel>();
            try {
                model = GetMaterialModels(productCode).ToList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterial", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertMaterial(MaterialModel inserted) {
            try {
                using (var vfi = new tammaContext()) {
                    var material =
                        vfi.Materials.FirstOrDefault(
                            m =>
                            m.MaterialName.Equals(inserted.MaterialName) &&
                            m.DiameterType == inserted.DiameterType &&
                            m.OutDiameter == inserted.OutDiameter &&
                            m.InDiameter == inserted.InDiameter &&
                            m.Shape == inserted.Shape);
                    if (material == null) {
                        int typeId = 1;
                        try {
                            typeId = Convert.ToInt32(inserted.MaterialTypeName);
                        }
                        catch (FormatException) {
                            typeId =
                                vfi.MaterialTypes.FirstOrDefault(c => c.MaterialTypeName.Equals(inserted.MaterialTypeName)).MaterialTypeId;
                        }
                        material = new Vfi.Models.Material {
                            MaterialId = inserted.MaterialId,
                            MaterialName = inserted.MaterialName,
                            //MaterialCode = inserted.MaterialName + MyUtilities.Material.GetMaterialDesignNo(
                            //    inserted.OutDiameter, inserted.InDiameter,
                            //    inserted.DiameterType, inserted.Shape),
                            MaterialTypeId = typeId,
                            OutDiameter = inserted.OutDiameter,
                            InDiameter = inserted.InDiameter,
                            Shape = inserted.Shape.Trim(),
                            DiameterType = inserted.DiameterType.Trim(),
                            Weight = inserted.Weight,
                            UnitPrice = inserted.UnitPrice,
                            Active = true,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            IsExpensive = inserted.IsExpensive,
                        };
                        material.MaterialCode = inserted.MaterialName +
                                                MyUtilities.Material.GetMaterialDesignNo(material);
                        vfi.Materials.Add(material);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Mã nguyên liệu đã tồn tại ! \nVui lòng dùng mã khác !");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMaterial", ex.Message);
            }

            return View(new GridModel(GetMaterialModels("")));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateMaterial(MaterialModel updateMaterial) {
            try {
                using (var vfi = new tammaContext()) {
                    var material =
                        vfi.Materials.FirstOrDefault(
                            m =>
                            m.MaterialName.Equals(updateMaterial.MaterialName) &&
                            m.DiameterType == updateMaterial.DiameterType &&
                            m.OutDiameter == updateMaterial.OutDiameter &&
                            m.InDiameter == updateMaterial.InDiameter &&
                            m.Shape == updateMaterial.Shape &&
                            m.MaterialId != updateMaterial.MaterialId);
                    if (material == null) {
                        material = vfi.Materials.FirstOrDefault(m => m.MaterialId == updateMaterial.MaterialId);
                        if (material == null)
                            throw new AggregateException("Cập nhật lỗi");
                        int typeId = 1;
                        try {
                            typeId = Convert.ToInt32(updateMaterial.MaterialTypeName);
                        }
                        catch (Exception) {
                            typeId =
                                vfi.MaterialTypes.FirstOrDefault(c => c.MaterialTypeName.Equals(updateMaterial.MaterialTypeName)).MaterialTypeId;
                        }

                        material.MaterialName = updateMaterial.MaterialName;
                        material.MaterialTypeId = typeId;
                        material.OutDiameter = updateMaterial.OutDiameter;
                        material.InDiameter = updateMaterial.InDiameter;
                        material.Shape = updateMaterial.Shape.Trim();
                        material.DiameterType = updateMaterial.DiameterType.Trim();
                        material.Weight = updateMaterial.Weight;
                        material.UnitPrice = updateMaterial.UnitPrice;
                        material.IsExpensive = updateMaterial.IsExpensive;
                        if (!updateMaterial.Active && updateMaterial.Active != material.Active) {
                            var materialInvs = vfi.MaterialInventories.Where(mi => mi.MaterialId == material.MaterialId && mi.TotalQty > 0);
                            if (materialInvs.Any())
                                throw new AggregateException("Lỗi! Vui lòng huỷ tồn kho trước khi tắt active");
                            material.Active = updateMaterial.Active;
                        }
                        material.ModifiedUser = HttpContext.User.Identity.Name;
                        material.Active = updateMaterial.Active;
                        material.ModifiedDate = DateTime.Now;
                        material.MaterialCode = updateMaterial.MaterialName +
                            MyUtilities.Material.GetMaterialDesignNo(material);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Mã nguyên liệu đã tồn tại ! /nVui lòng dùng mã khác !");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMaterial", ex.Message);
            }
            return View(new GridModel(GetMaterialModels("")));
        }

        public ActionResult SelectComboBoxMaterial() {
            using (var vfi = new tammaContext()) {
                return new JsonResult {
                    Data =
                        new SelectList(
                            vfi.Materials.Where(f => f.Active)
                               .OrderBy(m => m.MaterialName)
                               .ThenBy(m => m.Shape)
                               .ThenBy(m => m.DiameterType)
                               .ThenBy(m => m.OutDiameter)
                               .ThenBy(m => m.InDiameter)
                               .ToList(), "MaterialId",
                            "MaterialCode")
                };
            }
        }

        public ActionResult SelectComboBoxMaterialInventory() {
            using (var vfi = new tammaContext()) {
                var materialInvs = vfi.MaterialInventories.Where(m => m.TotalQty > 0);
                var model = materialInvs.Select(m => new MaterialInventoryModel {
                    MaterialInventoryId = m.MaterialInventoryId,
                    MaterialCode = m.Material.MaterialCode,
                    LotNumber = m.LotNumber,
                    Length = m.Length,
                    VendorCode = m.Vendor.VendorCode,
                    MaterialName = m.Material.MaterialName,
                    OutDiameter = m.Material.OutDiameter,
                    InDiameter = m.Material.InDiameter,
                    Shape = m.Material.Shape,
                    DiameterType = m.Material.DiameterType,
                }).OrderBy(m => m.MaterialName).ThenBy(m => m.OutDiameter).ThenBy(m => m.InDiameter).ThenBy(m => m.Length);
                return new JsonResult {
                    Data =
                        new SelectList(model.ToList(), "MaterialInventoryId", "MaterialCodeLotNumber")
                };
            }
        }

        public ActionResult SelectComboBoxProductionMaterial(int productId) {
            try {
                using (var vfi = new tammaContext()) {
                    var materialIds = vfi.ProductionMaterials.Where(x => x.Active && x.ProductId == productId)
                        .Select(x => x.MaterialId)
                        .ToList();
                    var product = vfi.Products.FirstOrDefault(x => x.ProductId == productId);
                    if (product != null && product.MaterialId != null) {
                        materialIds.Add(product.MaterialId.Value);
                        materialIds = materialIds.Distinct().ToList();
                    }
                    var model = vfi.MaterialInventories
                        .Where(m => materialIds.Contains(m.MaterialId) && m.TotalQty > 0)
                        .Select(m => new MaterialInventoryModel {
                            MaterialInventoryId = m.MaterialInventoryId,
                            MaterialCode = m.Material.MaterialCode,
                            LotNumber = m.LotNumber,
                            Length = m.Length,
                            VendorCode = m.Vendor.VendorCode,
                            MaterialName = m.Material.MaterialName,
                            OutDiameter = m.Material.OutDiameter,
                            InDiameter = m.Material.InDiameter,
                            Shape = m.Material.Shape,
                            DiameterType = m.Material.DiameterType,
                        })
                        .OrderBy(m => m.MaterialName).ThenBy(m => m.OutDiameter).ThenBy(m => m.InDiameter).ThenBy(m => m.Length);


                    //var a = 10;

                        return new JsonResult {
                            Data =
                                new SelectList(model.ToList(), "MaterialInventoryId", "MaterialCodeLotNumber")
                    };
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectComboBoxProductionMaterial", ex.Message);
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SelectComboBoxAllMaterialInventory() {
            using (var vfi = new tammaContext()) {
                var materialInvs =
                    vfi.MaterialInventories.Where(m => m.Active && m.LotNumber != "" && m.LotNumber != null);
                var model = materialInvs.Select(m => new MaterialInventoryModel {
                    MaterialInventoryId = m.MaterialInventoryId,
                    MaterialCode = m.Material.MaterialCode,
                    LotNumber = m.LotNumber,
                    Length = m.Length,
                    VendorCode = m.Vendor.VendorCode,
                    MaterialName = m.Material.MaterialName,
                    OutDiameter = m.Material.OutDiameter,
                    InDiameter = m.Material.InDiameter,
                    Shape = m.Material.Shape,
                    DiameterType = m.Material.DiameterType,
                }).OrderBy(m => m.MaterialName).ThenBy(m => m.OutDiameter).ThenBy(m => m.InDiameter).ThenBy(m => m.Length);
                return new JsonResult {
                    Data =
                        new SelectList(model.ToList(), "MaterialInventoryId", "MaterialCodeLotNumber")
                };
            }
        }

        public ActionResult SelectComboBoxMaterialInvCodeByTrack(int? trackId) {
            if (trackId == 0 || trackId == null) ;
            using (var vfi = new tammaContext()) {
                var track = vfi.TrackUpMachines.FirstOrDefault(t => t.TrackId == trackId);
                var materialInvs = vfi.MaterialInventories.Where(mi => mi.MaterialId == track.MaterialId);
                var model = materialInvs.Select(m => new MaterialInventoryModel {
                    MaterialInventoryId = m.MaterialInventoryId,
                    MaterialCode = m.Material.MaterialCode,
                    LotNumber = m.LotNumber,
                    Length = m.Length,
                    VendorCode = m.Vendor.VendorCode,
                    MaterialName = m.Material.MaterialName,
                    OutDiameter = m.Material.OutDiameter,
                    InDiameter = m.Material.InDiameter,
                    Shape = m.Material.Shape,
                    DiameterType = m.Material.DiameterType,
                }).OrderBy(m => m.MaterialName).ThenBy(m => m.OutDiameter).ThenBy(m => m.InDiameter).ThenBy(m => m.Length);
                return new JsonResult {
                    Data =
                        new SelectList(model.ToList(), "MaterialInventoryId", "MaterialCodeLotNumber")
                };
            }
        }

        public ActionResult GetMaterialInvTotal(int materialInventoryId) {
            try {
                //var intRevisionNumber = Convert.ToInt32(revisionNumber);
                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var materialInv =
                        vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == materialInventoryId);
                    return Json(materialInv.TotalQty);
                }
            }
            catch (FormatException) {
                return Json(0);
            }
        }

        public ActionResult GetMaterialInvInfo(int materialInvId) {
            try {
                using (var vfi = new tammaContext()) {
                    var materialInv = vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == materialInvId);
                    if (materialInv == null) {
                        return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NotFound, "", null));
                    }

                    return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "",
                        new MaterialInventoryModel {
                            MaterialId = materialInv.MaterialId,
                            LotNumber = materialInv.LotNumber,
                            TotalQty = materialInv.TotalQty,
                            AvailableQty = materialInv.TotalQty,
                            Length = materialInv.Length
                        }));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.Exception, ex.Message, null));
            }
        }

        public ActionResult GetMaterialInvWOInfo(int materialInvId) {
            try {
                using (var vfi = new tammaContext()) {
                    var materialInv = vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == materialInvId);
                    if (materialInv == null) {
                        return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NotFound, "", null));
                    }
                    var availableInv = materialInv.TotalQty;
                    var waitingTransactions = vfi.ExportMaterialDetails.Where(x => x.MaterialInvId == materialInvId &&
                                                    x.TransactionDetail.Transaction.Status == (byte)MyUtilities.Transaction.Status.Open)
                                            .ToList()
                                            .Sum(x => x.Quantity);
                    var waitingAssignWO = vfi.WorkOrderRoutings.Where(x => x.MaterialInvId == materialInvId
                                                                        && x.WarehouseId == null
                                                                        && x.Status == (byte)MyUtilities.WorkOrder.Status.Actived)
                                        .ToList()
                                        .Sum(x => x.PlannedCost);
                    availableInv -= Math.Round(waitingTransactions + waitingAssignWO, 2);
                    if (availableInv < 0) availableInv = 0;
                    return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "",
                        new MaterialInventoryModel {
                            MaterialId = materialInv.MaterialId,
                            LotNumber = materialInv.LotNumber,
                            TotalQty = materialInv.TotalQty,
                            AvailableQty = availableInv,
                            Length = materialInv.Length
                        }));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.Exception, ex.Message, null));
            }
        }

        [HttpPost]
        public ActionResult GetAssignMaterial(int machineId, int materialInventoryId) {
            try {

                using (var vfi = new tammaContext()) {
                    var materialInv =
                        vfi.MaterialInventories.FirstOrDefault(mi => mi.MaterialInventoryId == materialInventoryId);
                    if (materialInv == null)
                        return Json("9");

                    var materialInvOnMachine =
                        vfi.MaterialInvOnMachines.FirstOrDefault(
                            mim => mim.MachineId == machineId && mim.MaterialInvId == materialInventoryId);
                    return
                        Json(new object[]
                            {
                                string.Format("{0:n2}", materialInv.TotalQty),
                                materialInvOnMachine == null
                                    ? "0"
                                    : string.Format("{0:n2}", materialInvOnMachine.TotalQuantity)
                            });
                }
            }
            catch (Exception ex) {
                return Json("0");
            }
            return Json("0");
        }

        public ActionResult SelectComboBoxMaterialType() {
            return new JsonResult {
                Data = new SelectList(GetMaterialTypeModels()
                    .Where(f => f.Active && f.MaterialClassifiedId == 1), "MaterialTypeId", "MaterialTypeName")
            };
        }

        public ActionResult SelectComboBoxToolType() {
            return new JsonResult {
                Data = new SelectList(GetMaterialTypeModels()
                    .Where(f => f.Active && f.MaterialClassifiedId == 3), "MaterialTypeId", "MaterialTypeName")
            };
        }
        public ActionResult SelectComboBoxToolUseType() {
            var val = from MyUtilities.Tool.ExportType stt in Enum.GetValues(typeof(MyUtilities.Tool.ExportType))
                      select new {
                          Value = (int)Enum.Parse(typeof(MyUtilities.Tool.ExportType), stt.ToString()),
                          Text = MyUtilities.Tool.GetTypeText((int)Enum.Parse(typeof(MyUtilities.Tool.ExportType), stt.ToString()))
                      };

            return new JsonResult {
                Data = new SelectList(val, "Value", "Text")
            };
        }
        public ActionResult SelectComboBoxMaterialByType(int? materialTypeId) {
            if (materialTypeId == 0 || materialTypeId == null) {
                return new JsonResult {
                    Data = new SelectList(new List<MaterialModel>(), "MaterialId", "MaterialCode")
                };
            }

            var model = GetMaterialModels("").Where(m => m.MaterialTypeId == materialTypeId && m.Active).ToList();
            return new JsonResult {
                Data = new SelectList(model, "MaterialId", "MaterialCode")
            };
        }


        [HttpPost]
        public ActionResult CheckMaterialUnitPrice(int materialId) {
            try {
                //var intRevisionNumber = Convert.ToInt32(revisionNumber);
                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var unitPrice = vfi.Materials.FirstOrDefault(p => p.MaterialId == materialId).UnitPrice;
                    return Json(unitPrice);
                }
            }
            catch (FormatException) {
                return Json(0);
            }
        }

        [GridAction]
        public ActionResult SelectProductionMaterialById(int materialId) {
            var model = new List<ProductionMaterialModel>();
            try {
                model = GetMaterialListByProductId(materialId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionMaterialById", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionMaterialModel> GetMaterialListByProductId(int materialId) {
            var model = new List<ProductionMaterialModel>();
            using (var vfi = new tammaContext()) {
                var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                if (user == null)
                    throw new AggregateException("Lỗi! Mất đăng nhập! Vui lòng đăng nhập lại!");
                var materials = vfi.ProductionMaterials
                                   .Where(ps => ps.MaterialId == materialId && ps.Product.Active)
                                   .OrderBy(m => m.Priority);
                foreach (var material in materials) {
                    var entity = new ProductionMaterialModel {
                        RealMaterialId = material.RealMaterialId,
                        CustomerId = material.Product.CustomerId,
                        CustomerCode = material.Product.Customer.CustomerCode,
                        MaterialId = material.MaterialId,
                        MaterialCode = material.Material.MaterialCode,
                        ProductId = material.ProductId,
                        ProductCode = material.Product.ProductCode,
                        Priority = material.Priority,
                        Note = material.Note,
                        UnitWeightByMaterial =
                            MyUtilities.Product.GetProductWeight(material.Material.MaterialName,
                                                                 material.Material.OutDiameter,
                                                                 material.Material.InDiameter,
                                                                 material.Product.Length ?? 0,
                                                                 material.Product.KnifeCut ?? 0,
                                                                 material.Material.Shape + ""),
                        ForecastInYear = material.Product.ForecastsQuality ?? 0,
                        Active = material.Active,
                        ModifiedDate = material.ModifiedDate,
                        ModifiedUser = material.ModifiedUser,
                    };
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.ProductCode).ToList();
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
                        int productId = 1;
                        try {
                            productId = Convert.ToInt32(update.ProductCode);
                        }
                        catch (Exception) {
                            productId =
                                vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(update.ProductCode)).ProductId;
                        }
                        var product = vfi.Products.FirstOrDefault(m => m.ProductId == productId);
                        product.ForecastsQuality = update.ForecastInYear;
                        entity.Active = update.Active;
                        if (entity.Active) {
                            var materials =
                                vfi.Materials.Where(
                                    m => m.MaterialName.Equals(entity.Material.MaterialName) &&
                                         m.Shape.Equals(entity.Material.Shape) &&
                                         m.DiameterType.Equals(entity.Material.DiameterType) &&
                                         m.OutDiameter == entity.Material.OutDiameter &&
                                         m.InDiameter == entity.Material.InDiameter &&
                                         m.MaterialTypeId == entity.Material.MaterialTypeId);
                            //var materialIds = materials.Select(m => m.MaterialId);
                            foreach (var material1 in materials) {
                                var productionMaterial =
                                    vfi.ProductionMaterials.FirstOrDefault(
                                        pm => pm.MaterialId == material1.MaterialId && pm.ProductId == productId);
                                if (productionMaterial == null) {
                                    productionMaterial = new ProductionMaterial {
                                        ProductId = productId,
                                        Priority = 0,
                                        Note = update.Note + "",
                                        MaterialId = material1.MaterialId,
                                        UnitWeightByMaterial = MyUtilities.Product
                                            .GetProductWeight(material1.MaterialName,
                                                              material1.OutDiameter,
                                                              material1.InDiameter,
                                                              product.Length ?? 0,
                                                              product.KnifeCut ?? 0,
                                                              material1.Shape + ""),
                                        Active = true,
                                        ModifiedDate = DateTime.Now,
                                        ModifiedUser = HttpContext.User.Identity.Name,
                                    };
                                    vfi.ProductionMaterials.Add(productionMaterial);
                                }
                            }
                        }
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionMaterial", ex.Message);
            }
            return View(new GridModel(GetMaterialListByProductId(update.MaterialId)));
        }

        [GridAction]
        public ActionResult InsertProductionMaterial(ProductionMaterialModel insert,
            int materialId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                             "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                             "Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(insert.ProductCode))
                    throw new AggregateException("Lỗi! Nhập mã sản phẩm!");
                int productId = 1;
                try {
                    productId = Convert.ToInt32(insert.ProductCode);
                }
                catch (Exception) {
                    throw new AggregateException("Lỗi mã nguyên liệu ! Chọn lại sản phẩm");
                }
                using (var vfi = new tammaContext()) {
                    var entity =
                        vfi.ProductionMaterials.FirstOrDefault(
                            pm => pm.ProductId == productId && pm.MaterialId == materialId);
                    var material = vfi.Materials.FirstOrDefault(m => m.MaterialId == materialId);
                    var materials =
                        vfi.Materials.Where(
                            m => m.MaterialName.Equals(material.MaterialName) &&
                                 m.Shape.Equals(material.Shape) &&
                                 m.DiameterType.Equals(material.DiameterType) &&
                                 m.OutDiameter == material.OutDiameter &&
                                 m.InDiameter == material.InDiameter &&
                                 m.MaterialTypeId == material.MaterialTypeId &&
                                 m.Active);
                    var product = vfi.Products.FirstOrDefault(m => m.ProductId == productId);
                    if (entity == null) {
                        foreach (var material1 in materials) {
                            entity = new ProductionMaterial {
                                ProductId = productId,
                                Priority = 0,
                                Note = insert.Note + "",
                                MaterialId = material1.MaterialId,
                                UnitWeightByMaterial = MyUtilities.Product
                                    .GetProductWeight(material1.MaterialName,
                                        material1.OutDiameter,
                                        material1.InDiameter,
                                        product.Length ?? 0,
                                        product.KnifeCut ?? 0,
                                        material1.Shape + ""),
                                Active = true,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                            };
                            vfi.ProductionMaterials.Add(entity);
                        }
                    }
                    else {
                        if (!entity.Active) {
                            //entity.MaterialId = materialId;
                            entity.Priority = 0;
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
                        else {
                            throw new AggregateException("Lỗi! Sản phẩm đã được nguyên liệu sử dụng.");
                        }
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionMaterial", ex.Message);
            }
            return View(new GridModel(GetMaterialListByProductId(materialId)));
        }
        #endregion


        #region Material Img 

        [GridAction]
        public ActionResult SelectMaterialImgById(int materialId, int type) {
            var model = new List<MaterialImgModel>();
            try {
                model = GetMaterialImgById(materialId, type);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterialImgById", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<MaterialImgModel> GetMaterialImgById(int materialId ,int type) {
            var model = new List<MaterialImgModel>();
            //var qcManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.QcManager);
            using (var vfi = new tammaContext()) {
                var materialImgs = vfi.MaterialImgs.Where(t => t.MaterialId == materialId && t.Type == type).OrderBy(t => t.ImgId);
                foreach (var materialimg in materialImgs) {
                    var entity = new MaterialImgModel {
                        ImgId = materialimg.ImgId,
                        ImgUrl = materialimg.ImgUrl,
                        ModifiedDate = materialimg.ModifiedDate,
                        ModifiedUser = materialimg.ModifiedUser,
                        CanModify = true,
                        Name = materialimg.Name,
                        MaterialId = materialId,
                    };
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertMaterialImg(MaterialImgModel insert, int materialId, int type) {
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
                if (string.IsNullOrWhiteSpace(insert.ImgUrl)) {
                    throw new AggregateException("Lỗi! Không tìm thấy hình được upload!");
                }

                var materialImg = new MaterialImg() {
                    MaterialId = materialId,
                    ImgId = insert.ImgId,
                    ImgUrl = insert.ImgUrl,
                    Name = insert.Name,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name,
                    Type = type,
                };
                using (var vfi = new tammaContext()) {
                    vfi.MaterialImgs.Add(materialImg);
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
                ModelState.AddModelError("InsertMaterialImg", errorMessage);
            }
            return View(new GridModel(GetMaterialImgById(materialId,type)));

        }

        [GridAction]
        public ActionResult UpdateMaterialImg(MaterialImgModel update, int materialId, int type) {
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
                    var materialImg = vfi.MaterialImgs.FirstOrDefault(t => t.ImgId == update.ImgId && t.Type == type);
                    if (materialImg == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy bản vẽ sản phẩm!");
                    }
                    if (!string.IsNullOrWhiteSpace(update.ImgUrl)) {
                        var materialImgs = vfi.MaterialImgs.Where(t => t.ImgUrl.Equals(materialImg.ImgUrl));
                        if (!materialImgs.Any()) {
                            DeleteMaterialImg(materialImg.ImgUrl);
                        }
                        materialImg.ImgUrl = update.ImgUrl;
                    }
                    materialImg.Name = update.Name;
                    materialImg.ModifiedUser = HttpContext.User.Identity.Name;
                    materialImg.ModifiedDate = DateTime.Now;
                    vfi.SaveChanges();

                }
                MyUtilities.Product.UpdateProductDesign(materialId);


            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMaterialImg", ex.Message);
            }

            return View(new GridModel(GetMaterialImgById(materialId, type)));
        }

        [GridAction]
        public ActionResult DeleteMaterialImg(MaterialImgModel delete, int materialId, int type) {
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
                    var materialImg = vfi.MaterialImgs.FirstOrDefault(pi => pi.ImgId == delete.ImgId && pi.Type == type);
                    if (materialImg == null)
                        throw new AggregateException("Lỗi! Không tìm thấy bản vẽ sản phẩm!");
                    var materialImgs =
                        vfi.MaterialImgs.Where(pi => pi.ImgUrl.Equals(materialImg.ImgUrl) && pi.MaterialId != materialId);
                    if (!materialImgs.Any())
                        DeleteMaterialImg(materialImg.ImgUrl);
                    vfi.MaterialImgs.Remove(materialImg);
                    vfi.SaveChanges();

                }
                MyUtilities.Product.UpdateProductDesign(materialId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteMaterialImg", ex.Message);
            }
            return View(new GridModel(GetMaterialImgById(materialId, type)));
        }

        void DeleteMaterialImg(string imgUrl) {
            var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/MaterialImg"),
                imgUrl);
            if (System.IO.File.Exists(@destinationPath)) {
                System.IO.File.Delete(@destinationPath);
            }
        }


        public ActionResult CheckMaterialImage(string upload) {
            try {

                using (var vfi = new tammaContext()) {
                    var materialCode = "";
                    var MaterialImgs = vfi.MaterialImgs.Where(p => p.ImgUrl.Equals(upload));
                    if (MaterialImgs.Any()) {
                        foreach (var materialImg in MaterialImgs) {
                            materialCode += materialImg.Material.MaterialCode + " | ";
                        }
                        return Json("9! " + materialCode);
                    }
                }
            }
            catch (Exception ex) {
                return Json("0! " + ex.Message);
            }
            return Json("");
        }

        [HttpPost]
        public ActionResult SaveMaterialImg(IEnumerable<HttpPostedFileBase> MaterialImg) {
            // The Name of the Upload component is "attachments"       
            try {
                //var attachments = new List<HttpPostedFileBase>();
                if (MaterialImg.Any()) {
                    foreach (var file in MaterialImg) {
                        // Some browsers send file names with full path. We only care about the file name.
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName == null) continue;
                        var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/MaterialImg"), fileName);
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

        # endregion




        #region material product quote

        public List<MaterialQuoteBaseModel> GetMaterialQuoteBaseModels() {
            var model = new List<MaterialQuoteBaseModel>();
            try {
                using (var vfi = new tammaContext()) {
                    model = vfi.MaterialQuoteBases.Select(
                        x => new MaterialQuoteBaseModel {
                            BaseId = x.BaseId,
                            BasePrice = x.BasePrice,
                            MaterialName = x.MaterialName,
                            MaterialShape = x.MaterialShape,
                            MaterialDiameterType = x.MaterialDiameterType,
                            MaterialTypeId = x.MaterialTypeId,
                            MaterialTypeName = x.MaterialType.MaterialTypeName,
                            ModifiedDate = x.ModifiedDate,
                           ModifiedUser = x.ModifiedUser,
                        })
                        .OrderBy(x => x.MaterialTypeName).ThenByDescending(x => x.MaterialName).ThenBy(x => x.MaterialShape).ThenBy(x => x.MaterialDiameterType)
                        .ToList();

                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex);
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectMaterialQuoteBase() {
            var model = new List<MaterialQuoteBaseModel>();
            try {
                model = GetMaterialQuoteBaseModels();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterialQuoteBase", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertMaterialQuoteBase(MaterialQuoteBaseModel insert) {
            try {
                using (var vfi = new tammaContext()) {
                    int typeId = 1;
                    try {
                        typeId = Convert.ToInt32(insert.MaterialTypeName);
                    }
                    catch (Exception) { throw new AggregateException("Lỗi! Chọn lại loại nguyên liệu"); }
                    var quote = vfi.MaterialQuoteBases.FirstOrDefault(x => x.MaterialTypeId == typeId
                        && x.MaterialName.Equals(insert.MaterialName)
                        && x.MaterialDiameterType.Equals(insert.MaterialDiameterType)
                        && x.MaterialShape.Equals(insert.MaterialShape));
                    if (quote == null) {
                        quote = new MaterialQuoteBase {
                            BasePrice = insert.BasePrice,
                            MaterialDiameterType = insert.MaterialDiameterType,
                            MaterialName = insert.MaterialName,
                            MaterialShape = insert.MaterialShape,
                            MaterialTypeId = typeId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                        };
                        vfi.MaterialQuoteBases.Add(quote);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Lỗi! Nguyên liệu này đã tồn tại");
                    }
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("InsertMaterialQuoteBase", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }

            return View(new GridModel(GetMaterialQuoteBaseModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateMaterialQuoteBase(MaterialQuoteBaseModel update) {
            try {
                using (var vfi = new tammaContext()) {
                    var typeId = update.MaterialTypeId;
                    try {
                        typeId = Convert.ToInt32(update.MaterialTypeName);
                    }
                    catch (Exception) { }
                    var quote = vfi.MaterialQuoteBases.FirstOrDefault(x => x.BaseId != update.BaseId
                        && x.MaterialTypeId != typeId
                        && x.MaterialName.Equals(update.MaterialName)
                        && x.MaterialDiameterType.Equals(update.MaterialDiameterType)
                        && x.MaterialShape.Equals(update.MaterialShape));
                    if (quote != null) { throw new AggregateException("Lỗi! Nguyên liệu này đã tồn tại"); }
                    quote = vfi.MaterialQuoteBases.FirstOrDefault(x => x.BaseId == update.BaseId);
                    quote.MaterialTypeId = typeId;
                    quote.MaterialName = update.MaterialName;
                    quote.MaterialDiameterType = update.MaterialDiameterType;
                    quote.MaterialShape = update.MaterialShape;
                    quote.BasePrice = update.BasePrice;
                    quote.ModifiedUser = HttpContext.User.Identity.Name;
                    quote.ModifiedDate = DateTime.Now;
                    vfi.SaveChanges();
                }

            }
            catch (Exception exception) {
                ModelState.AddModelError("UpdateMaterialQuoteBase", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }
            return View(new GridModel(GetMaterialQuoteBaseModels()));
        }




        public List<MaterialQuoteBaseDetailModel> GetMaterialQuoteBaseDetailModels(int baseId) {
            var model = new List<MaterialQuoteBaseDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    model = vfi.MaterialQuoteBaseDetails.Where(x => x.MaterialQuoteBaseId == baseId).Select(
                        x => new MaterialQuoteBaseDetailModel {
                            DetailId = x.DetailId,
                            MaterialQuoteBaseId = x.MaterialQuoteBaseId,
                            FromOutDiameter = x.FromOutDiameter,
                            ToOutDiameter = x.ToOutDiameter,
                            InDiameter = x.InDiameter,
                            Value = x.Value,
                            Price = x.MaterialQuoteBase.BasePrice + x.Value,
                        })
                        .OrderBy(x => x.FromOutDiameter).ThenByDescending(x => x.ToOutDiameter).ThenBy(x => x.InDiameter)
                        .ToList();
                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex);
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectMaterialQuoteBaseDetail(int baseId) {
            var model = new List<MaterialQuoteBaseDetailModel>();
            try {
                model = GetMaterialQuoteBaseDetailModels(baseId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterialQuoteBaseDetail", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertMaterialQuoteBaseDetail(MaterialQuoteBaseDetailModel insert, int baseId) {
            try {
                using (var vfi = new tammaContext()) {
                    var detail = new MaterialQuoteBaseDetail { 
                        MaterialQuoteBaseId = baseId,
                        FromOutDiameter = insert.FromOutDiameter,
                        ToOutDiameter = insert.ToOutDiameter,
                        InDiameter = insert.InDiameter,
                        Value = insert.Value,
                    };
                    vfi.MaterialQuoteBaseDetails.Add(detail);
                    vfi.SaveChanges();
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("InsertMaterialQuoteBaseDetail", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }

            return View(new GridModel(GetMaterialQuoteBaseDetailModels(baseId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult updateMaterialQuoteBaseDetail(MaterialQuoteBaseDetailModel update) {
            var baseId = 0;
            try {
                using (var vfi = new tammaContext()) {
                    var detail = vfi.MaterialQuoteBaseDetails.FirstOrDefault(x => x.DetailId == update.DetailId);
                    if (detail == null) { }
                    detail.FromOutDiameter = update.FromOutDiameter;
                    detail.ToOutDiameter = update.ToOutDiameter;
                    detail.Value = update.Value;
                    detail.InDiameter = update.InDiameter;
                    vfi.SaveChanges();
                    baseId = detail.MaterialQuoteBaseId;
                }
            }
            catch (Exception exception) {
                ModelState.AddModelError("updateMaterialQuoteBaseDetail", @"Lỗi giá trị nhập. (try-catch). " + exception.Message);
            }
            return View(new GridModel(GetMaterialQuoteBaseDetailModels(baseId)));
        }
        #endregion

        #region Tool

        [HttpPost]
        public ActionResult SaveToolImg(IEnumerable<HttpPostedFileBase> ToolImg) {
            // The Name of the Upload component is "attachments"       
            try {
                var attachments = new List<HttpPostedFileBase>();
                if (ToolImg != null && ToolImg.Any()) {
                    attachments.AddRange(ToolImg.ToList());
                }
                if (attachments.Any()) {
                    foreach (var file in attachments) {
                        // Some browsers send file names with full path. We only care about the file name.
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName == null) continue;
                        var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/ToolImg"), fileName);
                        // giam kich thuoc
                        Image bm = Image.FromStream(file.InputStream);
                        var designWidth = 3000.0;
                        var designHeight = 1500.0;
                        var ratioW = designWidth / (double)bm.Width;
                        var ratioH = designHeight / (double)bm.Height;
                        var ratio = ratioH < ratioW ? ratioH : ratioW;
                        var newWidth = Convert.ToInt32(bm.Width * ratio);
                        var newHeight = Convert.ToInt32(bm.Height * ratio);
                        bm = ResizeBitmap((Bitmap)bm, newWidth, newHeight);
                        bm.Save(destinationPath, bm.RawFormat);
                        //bm.Save(destinationPath);
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
            return Json("False");
        }

        private Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight) {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }
        public ActionResult CheckUploadImage(string upload) {
            try {

                using (var vfi = new tammaContext()) {
                    var toolCodes = "";
                    var tools = vfi.Tools.Where(p => p.Img.Equals(upload));
                    if (tools.Any()) {
                        foreach (var tool in tools) {
                            toolCodes += tool.ToolFullCode + " | ";
                        }
                        return Json("9! " + toolCodes);
                    }
                }
            }
            catch (Exception ex) {
                return Json("0! " + ex.Message);
            }
            return Json("");
        }

        [GridAction]
        public ActionResult SelectAllTool(int toolTypeId, int useType, string toolName) {
            if (toolTypeId == 0 && useType == 0 && string.IsNullOrWhiteSpace(toolName))
                return View(new GridModel(new List<ToolModel>()));
            var model = new List<ToolModel>();
            try {
                model = GetAllTools(toolTypeId, useType, toolName);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectAllTool", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<ToolModel> GetAllTools(int toolTypeId, int useType, string toolName) {
            var model = new List<ToolModel>();
            using (var vfi = new tammaContext()) {
                var tools = (from t in vfi.Tools
                             where (toolTypeId == 0 || t.MaterialTypeId == toolTypeId) &&
                                   (useType == 0 || t.ToolTypeId == useType) &&
                                   t.IsDeleted != true
                             //&& t.ToolId == 1337
                             orderby t.ToolFullCode
                             select t).ToList();
                if (!string.IsNullOrWhiteSpace(toolName))
                    tools = tools.Where(t => t.ToolFullCode.Contains(toolName)).ToList();
                foreach (var tool in tools) {
                    var entity = new ToolModel {
                        ToolId = tool.ToolId,
                        ToolName = tool.ToolName,
                        ToolCode = tool.ToolCode,
                        Active = tool.Active,
                        Description = tool.Description,
                        ModifiedDate = tool.ModifiedDate,
                        ModifiedUser = tool.ModifiedUser,
                        ToolFullCode = tool.ToolFullCode,
                        UnitPrice = tool.UnitPrice,
                        ToolDesignNo = tool.ToolDesignNo,
                        ToolMaterial = tool.ToolMaterial,
                        MaterialTypeId = tool.MaterialTypeId,
                        MaterialTypeName = tool.MaterialType.MaterialTypeName,
                        ToolType = tool.ToolTypeId,
                        ToolTypeName = MyUtilities.Tool.GetTypeText(tool.ToolTypeId),
                        Img = tool.Img,
                        UploadDate = tool.ModifiedDate.ToString("yyyyMMddhhmmss"),
                        ToolProduction = tool.ToolProduction,
                        IsDeleted = tool.IsDeleted ?? false
                    };
                    if (string.IsNullOrWhiteSpace(entity.Img))
                        entity.Img = "askquestion.jpg";
                    model.Add(entity);
                }
            }

            return model;
        }

        [GridAction]
        public ActionResult InsertTool(ToolModel newTool) {
            int typeId = 0;
            var useType = 0;
            try {
                if (string.IsNullOrWhiteSpace(newTool.ToolCode))
                    throw new AggregateException("Thêm công cụ thất bại! Thiếu mã công cụ.");
                //if (string.IsNullOrWhiteSpace(newTool.ToolMaterial))
                //    throw new AggregateException("Thêm công cụ thất bại! Thiếu nguyên liệu.");
                if (string.IsNullOrWhiteSpace(newTool.ToolDesignNo))
                    throw new AggregateException("Thêm công cụ thất bại! Thiếu quy cách.");
                using (var vfi = new tammaContext()) {
                    //var tool =
                    //    vfi.Tools.FirstOrDefault(
                    //        t =>
                    //        t.ToolCode.Equals(newTool.ToolCode.Trim()) &&
                    //        t.ToolMaterial.Equals(newTool.ToolMaterial.Trim()) &&
                    //        t.ToolDesignNo.Equals(newTool.ToolDesignNo.Trim()) &&
                    //        t.ToolProduction.Equals(newTool.ToolProduction.Trim()));
                    var tool =
                        vfi.Tools.FirstOrDefault(
                            t =>
                            t.ToolCode.Equals(newTool.ToolCode.Trim()));
                    if (tool == null) {
                        try {
                            typeId = Convert.ToInt32(newTool.MaterialTypeName);
                        }
                        catch (FormatException) {
                            typeId =
                                vfi.MaterialTypes.FirstOrDefault(c => c.MaterialTypeName.Equals(newTool.MaterialTypeName)).MaterialTypeId;
                        }
                        try {
                            useType = Convert.ToInt32(newTool.ToolTypeName);
                        }
                        catch (FormatException) {
                        }
                        tool = new Tool {
                            ToolName = newTool.ToolName + "",
                            ToolCode = newTool.ToolCode.Trim(),
                            Active = true,
                            Description = newTool.Description + "",
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ToolDesignNo = newTool.ToolDesignNo.Trim(),
                            UnitPrice = 0,
                            ToolFullCode = newTool.GetFullToolCode(),
                            ToolMaterial = newTool.ToolMaterial.Trim(),
                            MaterialTypeId = typeId,
                            ToolTypeId = useType,
                            ToolProduction = newTool.ToolProduction,
                            IsDeleted = false
                        };
                        if (!string.IsNullOrWhiteSpace(newTool.Img))
                            tool.Img = newTool.Img;
                        vfi.Tools.Add(tool);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Thêm công cụ thất bại! Mã công cụ đã tồn tại !  Vui lòng đặt tên khác!");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertTool", ex.Message);
            }
            return View(new GridModel(GetAllTools(typeId, useType, "")));
        }
        [GridAction]
        public ActionResult UpdateTool(ToolModel updateTool) {
            try {
                if (string.IsNullOrWhiteSpace(updateTool.ToolCode))
                    throw new AggregateException("Thêm công cụ thất bại! Thiếu mã công cụ.");
                //if (string.IsNullOrWhiteSpace(updateTool.ToolMaterial))
                //    throw new AggregateException("Thêm công cụ thất bại! Thiếu nguyên liệu.");
                if (string.IsNullOrWhiteSpace(updateTool.ToolDesignNo))
                    throw new AggregateException("Thêm công cụ thất bại! Thiếu quy cách.");
                using (var vfi = new tammaContext()) {
                    //var tool =
                    //    vfi.Tools.FirstOrDefault(
                    //        t =>
                    //        t.ToolCode.Equals(updateTool.ToolCode.Trim()) &&
                    //        t.ToolMaterial.Equals(updateTool.ToolMaterial.Trim()) &&
                    //        t.ToolDesignNo.Equals(updateTool.ToolDesignNo.Trim()) &&
                    //        t.ToolProduction.Equals(updateTool.ToolProduction.Trim()) &&
                    //        t.ToolId != updateTool.ToolId);
                    var tool =
                        vfi.Tools.FirstOrDefault(
                            t =>
                            t.ToolCode.Equals(updateTool.ToolCode.Trim()) && t.ToolId != updateTool.ToolId);
                    if (tool == null) {
                        tool = vfi.Tools.FirstOrDefault(t => t.ToolId == updateTool.ToolId);
                        if (tool == null)
                            throw new AggregateException("Sửa công cụ thất bại! Lỗi!");
                        updateTool.MaterialTypeId = tool.MaterialTypeId;
                        updateTool.ToolType = tool.ToolTypeId;
                        int typeId = 1;
                        try {
                            typeId = Convert.ToInt32(updateTool.MaterialTypeName);
                        }
                        catch (Exception) {
                            typeId =
                                vfi.MaterialTypes.FirstOrDefault(c => c.MaterialTypeName.Equals(updateTool.MaterialTypeName)).MaterialTypeId;
                        }
                        var useType = 1;
                        try {
                            useType = Convert.ToInt32(updateTool.ToolTypeName);
                        }
                        catch (FormatException) {
                        }
                        tool.ToolName = updateTool.ToolName + "";
                        tool.ToolCode = updateTool.ToolCode.Trim();
                        if (!updateTool.Active && tool.Active != updateTool.Active) {
                            var toolInvs = vfi.ToolInventories.Where(ti => ti.ToolId == tool.ToolId && ti.TotalQuantity > 0);
                            if (toolInvs.Any())
                                throw new AggregateException("Lỗi! Công cụ còn tồn kho! Vui lòng huỷ hết tồn kho trước khi tắt active");

                            tool.Active = updateTool.Active;
                        }
                        else {
                            tool.Active = updateTool.Active;
                        }
                        tool.Description = updateTool.Description + "";
                        tool.ModifiedDate = DateTime.Now;
                        tool.ModifiedUser = HttpContext.User.Identity.Name;
                        tool.ToolDesignNo = updateTool.ToolDesignNo.Trim();
                        tool.ToolMaterial = updateTool.ToolMaterial + "" ;
                        tool.UnitPrice = 0;
                        tool.MaterialTypeId = typeId;
                        tool.ToolTypeId = useType;
                        tool.ToolProduction = updateTool.ToolProduction + "";
                        tool.ToolFullCode = updateTool.GetFullToolCode();
                        tool.IsDeleted = updateTool.IsDeleted;
                        if (!string.IsNullOrWhiteSpace(updateTool.Img))
                            tool.Img = updateTool.Img;
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Sửa công cụ thất bại! Mã công cụ đã tồn tại ! Vui lòng đặt tên khác!");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateTool", ex.Message);
            }
            return View(new GridModel(GetAllTools(updateTool.MaterialTypeId, updateTool.ToolType, "")));
        }

        public ActionResult SelectComboBoxTool() {
            using (var vfi = new tammaContext()) {
                return new JsonResult {
                    Data =
                        new SelectList(vfi.Tools.Where(f => f.Active).ToList(), "ToolId",
                                       "ToolFullCode")
                };
            }
        }

        [GridAction]
        public ActionResult SelectChest() {
            var model = new List<DepartmentModel>();
            try {
                model = GetDepartments();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectChest", ex.Message);
            }
            return View(new GridModel(model));
        }

        private List<DepartmentModel> GetDepartments() {
            var model = new List<DepartmentModel>();
            using (var vfi = new tammaContext()) {
                var departments = (from t in vfi.Departments
                                   select t).ToList();
                foreach (var department in departments) {
                    var entity = new DepartmentModel {
                        DepartmentId = department.DepartmentId,
                        DepartmentName = department.DepartmentName,
                        Active = department.Active,
                        ModifiedUser = department.ModifiedUser,
                        ModifiedDate = department.ModifiedDate,
                    };
                    model.Add(entity);
                }
            }

            return model.OrderBy(m => m.DepartmentName).ToList();
        }

        [GridAction]
        public ActionResult InsertDepartment(DepartmentModel insert) {
            try {
                using (var vfi = new tammaContext()) {
                    var department = new Department {
                        DepartmentName = insert.DepartmentName,
                        Active = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    };
                    vfi.Departments.Add(department);

                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertDepartment", ex.Message);
            }
            return View(new GridModel(GetDepartments()));
        }
        [GridAction]
        public ActionResult UpdateDepartment(DepartmentModel update) {
            try {
                using (var vfi = new tammaContext()) {
                    var department = vfi.Departments.FirstOrDefault(c => c.DepartmentId == update.DepartmentId);
                    if (department == null) {
                        throw new AggregateException("Không tìm thấy tủ !");
                    }
                    department.DepartmentName = update.DepartmentName;
                    department.Active = update.Active;
                    department.ModifiedUser = HttpContext.User.Identity.Name;
                    department.ModifiedDate = DateTime.Now;

                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateDepartment", ex.Message);
            }
            return View(new GridModel(GetDepartments()));
        }

        public ActionResult SelectComboBoxDepartment() {
            using (var vfi = new tammaContext()) {
                return new JsonResult {
                    Data = new SelectList(vfi.Departments.Where(f => f.Active).ToList(), "DepartmentId", "DepartmentName")
                };
            }
        }

        #endregion

        #region Fuel


        public ActionResult SelectComboBoxAllFuel() {
            using (var vfi = new tammaContext()) {
                return new JsonResult {
                    Data =
                        new SelectList(vfi.Fuels.Where(f => f.Active).Select(entity => new FuelModel {
                            FuelId = entity.FuelId,
                            FuelFullCode = entity.FuelFullCode,
                            FuelName = entity.FuelName
                        }).ToList(), "FuelId", "FuelFullCode")
                };
            }
        }
        [GridAction]
        public ActionResult SelectAllFuel() {
            var model = GetAllFuels();
            return View(new GridModel(model));
        }
        List<FuelModel> GetAllFuels() {
            var model = new List<FuelModel>();
            try {
                using (var vfi = new tammaContext()) {
                    foreach (var fuel in vfi.Fuels) {
                        var entity = new FuelModel {
                            FuelId = fuel.FuelId,
                            FuelName = fuel.FuelName,
                            FuelCode = fuel.FuelCode,
                            Active = fuel.Active,
                            FuelDesctiption = fuel.FuelDesctiption,
                            FuelDesignNo = fuel.FuelDesignNo,
                            FuelFullCode = fuel.FuelFullCode,
                            //Description = Fuel.Description,
                            ModifiedDate = fuel.ModifiedDate,
                            ModifiedUser = fuel.ModifiedUser,
                            UnitWeight = fuel.UnitWeight
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetAllFuels", ex.Message);
            }
            return model.OrderBy(m => m.FuelCode).ThenBy(m => m.FuelDesignNo).ToList();
        }

        [GridAction]
        public ActionResult InsertFuel(FuelModel newFuel) {
            try {
                using (var vfi = new tammaContext()) {
                    newFuel.FuelName = (newFuel.FuelName + "").Trim();
                    newFuel.FuelCode = (newFuel.FuelCode + "").Trim();
                    newFuel.FuelDesignNo = (newFuel.FuelDesignNo + "").Trim();
                    newFuel.FuelDesctiption = (newFuel.FuelDesctiption + "").Trim();
                    var fuel =
                        vfi.Fuels.FirstOrDefault(
                            t =>
                            t.FuelCode.Equals(newFuel.FuelCode) &&
                            t.FuelDesignNo.Equals(newFuel.FuelDesignNo));
                    if (fuel == null) {
                        fuel = new Fuel {
                            FuelName = newFuel.FuelName.Trim(),
                            FuelCode = newFuel.FuelCode,
                            Active = true,
                            //Description = newFuel.Description,
                            FuelDesctiption = newFuel.FuelDesctiption,
                            FuelDesignNo = newFuel.FuelDesignNo,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            FuelFullCode = newFuel.GetFuelFullCode(),
                            UnitPrice = 0,
                            UnitWeight = newFuel.UnitWeight,
                        };
                        vfi.Fuels.Add(fuel);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Thêm nhiên liệu thất bại! Mã nhiên liệu đã tồn tại !  Vui lòng đặt tên khác!");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertFuel", ex.Message);
            }
            return View(new GridModel(GetAllFuels()));
        }
        [GridAction]
        public ActionResult UpdateFuel(FuelModel updateFuel) {
            try {
                using (var vfi = new tammaContext()) {
                    updateFuel.FuelName = updateFuel.FuelName.Trim();
                    updateFuel.FuelCode = updateFuel.FuelCode.Trim();
                    updateFuel.FuelDesignNo = updateFuel.FuelDesignNo.Trim();
                    updateFuel.FuelDesctiption = (updateFuel.FuelDesctiption + "").Trim();
                    var fuel =
                        vfi.Fuels.FirstOrDefault(
                            t =>
                            t.FuelCode.Equals(updateFuel.FuelCode) &&
                            t.FuelDesignNo.Equals(updateFuel.FuelDesignNo) &&
                            t.FuelId != updateFuel.FuelId);
                    if (fuel == null) {
                        fuel = vfi.Fuels.FirstOrDefault(t => t.FuelId == updateFuel.FuelId);
                        if (fuel == null)
                            throw new AggregateException("Sửa nhiên liệu thất bại! Lỗi!");
                        fuel.FuelName = updateFuel.FuelName;
                        fuel.FuelCode = updateFuel.FuelCode;
                        if (!updateFuel.Active && fuel.Active != updateFuel.Active) {
                            var fuelInvs = vfi.FuelInventories.Where(fi => fi.FuelId == fuel.FuelId && fi.TotalQuantity > 0);
                            if (fuelInvs.Any())
                                throw new AggregateException("Lỗi! Vui lòng huỷ tồn kho trước khi tắt active");
                        }
                        fuel.Active = updateFuel.Active;
                        fuel.FuelDesctiption = updateFuel.FuelDesctiption;
                        fuel.FuelDesignNo = updateFuel.FuelDesignNo;
                        fuel.FuelFullCode = updateFuel.GetFuelFullCode();
                        fuel.UnitWeight = updateFuel.UnitWeight;
                        fuel.ModifiedDate = DateTime.Now;
                        fuel.ModifiedUser = HttpContext.User.Identity.Name;
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Sửa nhiên liệu thất bại! Mã nhiên liệu đã tồn tại ! Vui lòng đặt tên khác!");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateFuel", ex.Message);
            }
            return View(new GridModel(GetAllFuels()));
        }

        public ActionResult SelectComboBoxFuel() {
            using (var vfi = new tammaContext()) {
                return new JsonResult {
                    Data = new SelectList(vfi.Fuels.Where(f => f.Active).ToList(), "FuelId", "FuelFullCode"),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        #endregion
    }
}