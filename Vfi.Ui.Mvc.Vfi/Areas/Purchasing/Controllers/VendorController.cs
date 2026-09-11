using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
using System.Web;
using System.Drawing;

//using Vfi.Client.Module.Purchasing.Interfaces;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
using System.Threading;
using System.Globalization;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Controllers {
    public class VendorController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        [InjectionConstructor]
        public VendorController(IUnitOfWork unitOfWork
            //, IVendorService vendorService
            ) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }

            return ViewData;
        }
        // View
        public ActionResult VendorManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        #region Vendor

        [GridAction]
        public ActionResult SelectAllVendor() {
            var models = GetAllVendor();
            return View(new GridModel(models));
        }
        List<VendorModel> GetAllVendor() {
            var model = new List<VendorModel>();
            using (var vfi = new tammaContext()) {
                model.AddRange(vfi.Vendors.Select(vendor => new VendorModel {
                    CompanyName = vendor.CompanyName,
                    BankAccount = vendor.BankAccount,
                    ContactName = vendor.ContactName,
                    Active = vendor.Active,
                    Address = vendor.Address,
                    MaterialClassifiedId = vendor.MaterialClassifiedId ?? 0,
                    MaterialClassifiedName = vendor.MaterialClassified.MaterialClassifiedName,
                    ModifiedDate = vendor.ModifiedDate,
                    ModifiedUser = vendor.ModifiedUser,
                    MaxCredit = vendor.MaxCredit,
                    Note = vendor.Note,
                    Eaddress = vendor.Eaddress,
                    Email = vendor.Email,
                    Fax = vendor.Fax,
                    Phone = vendor.Phone,
                    ShortName = vendor.ShortName,
                    SpecialInfo = vendor.SpecialInfo,
                    TaxCode = vendor.TaxCode,
                    VendorCode = vendor.VendorCode,
                    VendorName = vendor.VendorName,
                    VendorId = vendor.VendorId,
                    Priority = vendor.Priority,
                }));
            }
            return model.OrderByDescending(m => m.Priority).ThenBy(t => t.MaterialClassifiedName).ThenBy(m => m.VendorCode).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertVendor(VendorModel newVendor) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<VendorModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var vendor = vfi.Vendors.FirstOrDefault(v => v.VendorCode.Equals(newVendor.VendorCode));
                    if (vendor == null) {
                        int classtifiedId = 1;
                        try {
                            classtifiedId = Convert.ToInt32(newVendor.MaterialClassifiedName);
                        }
                        catch (Exception) {
                            classtifiedId =
                                vfi.MaterialClassifieds.FirstOrDefault(
                                    a => a.MaterialClassifiedName.Equals(newVendor.MaterialClassifiedName))
                                   .MaterialClassifiedId;
                        }

                        vendor = new Vendor {
                            CompanyName = newVendor.CompanyName,
                            BankAccount = newVendor.BankAccount,
                            ContactName = newVendor.ContactName,
                            Active = true,
                            Address = newVendor.Address,
                            MaterialClassifiedId = classtifiedId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            MaxCredit = newVendor.MaxCredit,
                            Note = newVendor.Note,
                            Eaddress = newVendor.Eaddress,
                            Email = newVendor.Email,
                            Fax = newVendor.Fax,
                            Phone = newVendor.Phone,
                            ShortName = newVendor.ShortName,
                            SpecialInfo = newVendor.SpecialInfo,
                            TaxCode = newVendor.TaxCode,
                            VendorCode = newVendor.VendorCode,
                            VendorName = newVendor.VendorName,
                            Priority = false,
                        };
                        vfi.Vendors.Add(vendor);
                        vfi.SaveChanges();
                    }
                    else
                        throw new AggregateException("Thêm mới thất bại !Mã nhà cung cấp đã tồn tại ! Vui lòng dùng mã khác!");
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertVendor", ex.Message);
            }
            return View(new GridModel(GetAllVendor()));

        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateVendor(VendorModel updateVendor) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<VendorModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    var vendor =
                        vfi.Vendors.FirstOrDefault(
                            v => v.VendorCode.Equals(updateVendor.VendorCode) && v.VendorId != updateVendor.VendorId);
                    if (vendor == null) {
                        int classtifiedId = 1;
                        try {
                            classtifiedId = Convert.ToInt32(updateVendor.MaterialClassifiedName);
                        }
                        catch (Exception) {
                            classtifiedId =
                                vfi.MaterialClassifieds.FirstOrDefault(
                                    a => a.MaterialClassifiedName.Equals(updateVendor.MaterialClassifiedName))
                                   .MaterialClassifiedId;
                        }
                        vendor = vfi.Vendors.FirstOrDefault(v => v.VendorId == updateVendor.VendorId);
                        if (vendor == null)
                            throw new AggregateException("Lỗi nhà cung cấp ! ");
                        vendor.CompanyName = updateVendor.CompanyName;
                        vendor.BankAccount = updateVendor.BankAccount;
                        vendor.ContactName = updateVendor.ContactName;
                        vendor.Active = updateVendor.Active;
                        vendor.Address = updateVendor.Address;
                        vendor.MaterialClassifiedId = classtifiedId;
                        vendor.ModifiedDate = DateTime.Now;
                        vendor.ModifiedUser = HttpContext.User.Identity.Name;
                        vendor.MaxCredit = updateVendor.MaxCredit;
                        vendor.Note = updateVendor.Note;
                        vendor.Eaddress = updateVendor.Eaddress;
                        vendor.Email = updateVendor.Email;
                        vendor.Fax = updateVendor.Fax;
                        vendor.Phone = updateVendor.Phone;
                        vendor.ShortName = updateVendor.ShortName;
                        vendor.SpecialInfo = updateVendor.SpecialInfo;
                        vendor.TaxCode = updateVendor.TaxCode;
                        vendor.VendorCode = updateVendor.VendorCode;
                        vendor.VendorName = updateVendor.VendorName;
                        vendor.Priority = updateVendor.Priority;
                        vfi.SaveChanges();
                    }
                    else
                        throw new AggregateException("Cập nhật thất bại ! Mã nhà cung cấp đã tồn tại ! Vui lòng dùng mã khác!");
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateVendor", ex.Message);
            }
            return View(new GridModel(GetAllVendor()));
        }
        List<VendorModel> GetVendorCombobox(int materialClasstified) {
            var model = new List<VendorModel>();
            using (var vfi = new tammaContext()) {
                model.AddRange(vfi.Vendors.Where(v => v.MaterialClassifiedId == materialClasstified).Select(vendor => new VendorModel {
                    VendorCode = vendor.VendorCode,
                    VendorName = vendor.VendorName,
                    VendorId = vendor.VendorId,
                    Active = vendor.Active
                }));
            }
            return model;
        }
        public ActionResult SelectComboBoxAllVendor() {
            return new JsonResult {
                Data = new SelectList(GetAllVendor().Where(f => f.Active), "VendorId", "VendorName")
            };
        }
        public ActionResult SelectComboBoxVendorByClasstified(int classified) {
            try {
                if (classified > 0)
                    return new JsonResult {
                        Data = new SelectList(GetVendorCombobox(classified).Where(f => f.Active), "VendorId", "VendorCodeName")
                    };
            }
            catch (Exception ex) { return Json(ex.Message); }
            return new JsonResult {
                Data = new SelectList(new List<object>())
            };
        }

        public ActionResult SelectComboBoxMaterialVendor() {
            return new JsonResult {
                Data =
                    new SelectList(
                        GetVendorCombobox(1).Where(f => f.Active),
                        "VendorId", "VendorCodeName")
            };
        }

        public ActionResult SelectComboBoxToolVendor() {
            return new JsonResult {
                Data =
                    new SelectList(
                        GetVendorCombobox(3).Where(f => f.Active),
                        "VendorId", "VendorCodeName")
            };
        }

        public ActionResult SelectComboBoxFuelVendor() {
            return new JsonResult {
                Data =
                    new SelectList(
                        GetVendorCombobox(2).Where(f => f.Active),
                        "VendorId", "VendorCodeName")
            };
        }

        public ActionResult SelectComboBoxPlatingVendor() {
            return new JsonResult {
                Data =
                    new SelectList(
                        GetVendorCombobox(4).Where(f => f.Active),
                        "VendorId", "VendorCodeName")
            };
        }


        [GridAction]
        public ActionResult SelectGetEvaluationForm(int vendorId) {
            var model = new List<EvaluationFormModel>();
            try {
                model = GetEvaluationForm(vendorId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetEvaluationForm", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<EvaluationFormModel> GetEvaluationForm(int vendorId) {
            var model = new List<EvaluationFormModel>();
            using (var vfi = new tammaContext()) {
                try {
                    var materialClassifiedId = vfi.Vendors.Where(t => t.VendorId == vendorId).Select(t => t.MaterialClassifiedId).FirstOrDefault();
                    var evaluationForms = vfi.EvaluationForms.Where(t => t.VendorId == vendorId 
                                                                    && t.MaterialClassifiedId == materialClassifiedId 
                                                                    && t.Status == (byte)MyUtilities.PurchaseOrder.EvaluationEnum.Approved)
                                                             .ToList();
                    int index = 1;
                    foreach (var evaluation in evaluationForms) {
                        var entity = new EvaluationFormModel {
                            Index = index,
                            Name = evaluation.Name,
                            FromDate = evaluation.FromDate,
                            ToDate = evaluation.ToDate,
                            TotalPoint = evaluation.TotalPoint,
                            ZeroPointCount = evaluation.ZeroPointCount,
                            PricePoint = evaluation.PricePoint,
                            TechSpPoint = evaluation.TechSpPoint,
                            LogisticsPoint = evaluation.LogisticsPoint,
                            QuantityDeliveryPoint = evaluation.QuantityDeliveryPoint,
                            NGNumberPoint = evaluation.NGNumberPoint,
                            NGTimePoint = evaluation.NGTimePoint,
                            Note = evaluation.Note,
                            ModifiedDate = evaluation.ModifiedDate,
                            ModifiedUser = evaluation.ModifiedUser,
                            ApprovedDate = evaluation.ApprovedDate,
                            ApprovedUser = evaluation.ApprovedUser,
                            Grade = evaluation.Grade,
                        };
                        if (entity.Grade == null) {
                            entity.Grade = "E";
                        }

                        index++;
                        model.Add(entity);

                    }

                }
                catch (Exception ex) {
                    ModelState.AddModelError("GetEvationForm", ex.Message);
                }

            }

            return model;
        }


        #endregion


        #region Vendor Img

        [GridAction]
        public ActionResult SelectVendorImgById(int vendorId, int type) {
            var model = new List<VendorImgModel>();
            try {
                model = GetVendorImgById(vendorId, type);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectVendorImgById", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<VendorImgModel> GetVendorImgById(int vendorId, int type) {
            var model = new List<VendorImgModel>();
            try {
                //var qcManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.QcManager);
                using (var vfi = new tammaContext()) {
                    var vendorImgs = vfi.VendorImgs.Where(t => t.VendorId == vendorId && t.Type == type).OrderBy(t => t.ImgId);
                    foreach (var vendorImg in vendorImgs) {
                        var entity = new VendorImgModel {
                            ImgId = vendorImg.ImgId,
                            ImgUrl = vendorImg.ImgUrl,
                            ModifiedDate = vendorImg.ModifiedDate,
                            ModifiedUser = vendorImg.ModifiedUser,
                            CanModify = true,
                            Name = vendorImg.Name,
                            VendorId = vendorId,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                var errorMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : ex.Message;

                ModelState.AddModelError("GetVendorImgById", errorMessage);
            }

            return model;
        }

        public ActionResult DownloadFile(string fileName) {
            var path = Server.MapPath("~/Content/FileUpload/VendorImg/" + fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [GridAction]
        public ActionResult InsertVendorImg(VendorImgModel insert, int vendorId, int type) {
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

                var vendorImg = new VendorImg() {
                    VendorId = vendorId,
                    ImgId = insert.ImgId,
                    ImgUrl = insert.ImgUrl,
                    Name = insert.Name,
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = HttpContext.User.Identity.Name,
                    Type = type,
                };
                using (var vfi = new tammaContext()) {
                    vfi.VendorImgs.Add(vendorImg);
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
                ModelState.AddModelError("InsertVendorImg", errorMessage);
            }
            return View(new GridModel(GetVendorImgById(vendorId, type)));

        }

        [GridAction]
        public ActionResult UpdateVendorImg(VendorImgModel update, int vendorId, int type) {
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
                    var vendorImg = vfi.VendorImgs.FirstOrDefault(t => t.ImgId == update.ImgId && t.Type == type);
                    if (vendorImg == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy bản vẽ sản phẩm!");
                    }
                    if (!string.IsNullOrWhiteSpace(update.ImgUrl)) {
                        var vendorImgs = vfi.VendorImgs.Where(t => t.ImgUrl.Equals(vendorImg.ImgUrl));
                        if (!vendorImgs.Any()) {
                            DeleteVendorImg(vendorImg.ImgUrl);
                        }
                        vendorImg.ImgUrl = update.ImgUrl;
                    }
                    vendorImg.Name = update.Name;
                    vendorImg.ModifiedUser = HttpContext.User.Identity.Name;
                    vendorImg.ModifiedDate = DateTime.Now;
                    vfi.SaveChanges();

                }
                MyUtilities.Product.UpdateProductDesign(vendorId);


            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateVendorImg", ex.Message);
            }

            return View(new GridModel(GetVendorImgById(vendorId, type)));
        }

        [GridAction]
        public ActionResult DeleteVendorImg(VendorImgModel delete, int vendorId, int type) {
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
                    var vendorImg = vfi.VendorImgs.FirstOrDefault(pi => pi.ImgId == delete.ImgId && pi.Type == type);
                    if (vendorImg == null)
                        throw new AggregateException("Lỗi! Không tìm thấy bản vẽ sản phẩm!");
                    var vendorImgs =
                        vfi.VendorImgs.Where(pi => pi.ImgUrl.Equals(vendorImg.ImgUrl) && pi.VendorId != vendorId);
                    if (!vendorImgs.Any())
                        DeleteVendorImg(vendorImg.ImgUrl);
                    vfi.VendorImgs.Remove(vendorImg);
                    vfi.SaveChanges();

                }
                MyUtilities.Product.UpdateProductDesign(vendorId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteVendorImg", ex.Message);
            }
            return View(new GridModel(GetVendorImgById(vendorId, type)));
        }

        void DeleteVendorImg(string imgUrl) {
            var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/VendorImg"),
                imgUrl);
            if (System.IO.File.Exists(@destinationPath)) {
                System.IO.File.Delete(@destinationPath);
            }
        }


        public ActionResult CheckVendorImage(string upload) {
            try {

                using (var vfi = new tammaContext()) {
                    var vendorCode = "";
                    var VendorImgs = vfi.VendorImgs.Where(p => p.ImgUrl.Equals(upload));
                    if (VendorImgs.Any()) {
                        foreach (var vendorImg in VendorImgs) {
                            vendorCode += vendorImg.Vendor.VendorCode + " | ";
                        }
                        return Json("9! " + vendorCode);
                    }
                }
            }
            catch (Exception ex) {
                return Json("0! " + ex.Message);
            }
            return Json("");
        }

        [HttpPost]
        public ActionResult SaveVendorImg(IEnumerable<System.Web.HttpPostedFileBase> VendorImg) {
            // The Name of the Upload component is "attachments"       
            try {
                //var attachments = new List<HttpPostedFileBase>();
                if (VendorImg.Any()) {
                    var fileExtension = "";
                    foreach (var file in VendorImg) {
                        // Some browsers send file names with full path. We only care about the file name.
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName == null) continue;
                        var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/VendorImg"), fileName);
                        var imgExtensions = new List<String>() { "png", "jpg", "jpeg" };
                        fileExtension = fileName.Split('.').Reverse().First().ToLower();
                        if (fileExtension.Contains("pdf")) {
                            file.SaveAs(destinationPath);
                        }
                        else if (imgExtensions.Any(x => fileExtension.Contains(x))) {
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
                    }
                    return Json("Upload thành công !" + fileExtension);
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


        #region Vendor Objective

        [GridAction]
        public ActionResult SelectVendorObjective(int vendorId) {
            var model = new List<VendorObjectiveModel>();
            try {
                model = GetVendorObjective(vendorId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectVendorObjective", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<VendorObjectiveModel> GetVendorObjective(int vendorId) {
            var model = new List<VendorObjectiveModel>();
            using (var vfi = new tammaContext()) {
                var vendorObjectives = vfi.VendorObjectives.Where(t => t.VendorId == vendorId).OrderByDescending(t => t.Year).ToList();
                if (!vendorObjectives.Any()) {
                    return model;
                }
                else {
                    foreach (var objective in vendorObjectives) {
                        var entity = new VendorObjectiveModel {
                            ObjectiveId = objective.ObjectiveId,
                            VendorId = vendorId,
                            Year = objective.Year,
                            ModifiedDate = objective.ModifiedDate,
                            ModifiedUser = objective.ModifiedUser,
                            Note = objective.Note,

                            PricePoint = objective.PricePoint,
                            TechSpPoint = objective.TechSpPoint,

                            LateTimes = objective.LateTimes,
                            LessTimes = objective.LessTimes,
                            NGTimes = objective.NGTimes,
                            PercentNGNumber = objective.PercentNGNumber,

                            LogisticsPoint = objective.LogisticsPoint,
                            QuantityDeliveryPoint = objective.QuantityDeliveryPoint,
                            NGTimesPoint = objective.NGTimesPoint,
                            NGNumberPoint = objective.NGNumberPoint,

                            TotalPoint = objective.TotalPoint,
                        };
                        model.Add(entity);
                    }
                }
                return model;
            }
        }


        [HttpPost]
        [GridAction]
        public ActionResult InsertVendorObjective(VendorObjectiveModel insert, int vendorId) {
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

                    var entity = new VendorObjective {
                        ObjectiveId = insert.ObjectiveId,
                        VendorId = vendorId,
                        Note = insert.Note,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,

                        PricePoint = insert.PricePoint,
                        TechSpPoint = insert.TechSpPoint,

                        LateTimes = insert.LateTimes,
                        LessTimes = insert.LessTimes,
                        NGTimes = insert.NGTimes,
                        PercentNGNumber = Math.Round(insert.PercentNGNumber, 1),
                    };
                    switch (insert.PricePoint) {
                        case 0:
                        case 5:
                        case 10:
                            entity.PricePoint = insert.PricePoint;
                            break;
                        default:
                            throw new AggregateException ("Lỗi! Hãy nhập điểm 'Competitivity' (0 - 5 - 10)");
                    }

                    switch (insert.TechSpPoint) {
                        case 0:
                        case 5:
                        case 10:
                            entity.TechSpPoint = insert.TechSpPoint;
                            break;
                        default:
                            throw new AggregateException("Lỗi! Hãy nhập điểm 'Technical Support' (0 - 5 - 10)");
                    }

                    var checkLatestYear = vfi.VendorObjectives.Where(t => t.VendorId == vendorId).Select(t => t.Year).ToList();
                    if (!checkLatestYear.Any()) {
                        entity.Year = DateTime.Today.Year;
                    }
                    else {
                        var nextYear = checkLatestYear.Max();
                        if (checkLatestYear.Max() > (DateTime.Today.Year +1)) {
                            return View(new GridModel(GetVendorObjective(vendorId)));
                        }
                        entity.Year = nextYear + 1;
                    }



                    if (insert.LateTimes == 0) {
                        entity.LogisticsPoint = 20;
                    }
                    else if (insert.LateTimes == 1 || insert.LateTimes == 2) {
                        entity.LogisticsPoint = 15;
                    }
                    else if (insert.LateTimes >= 3 && insert.LateTimes <= 5) {
                        entity.LogisticsPoint = 10;
                    }
                    else if (insert.LateTimes > 5) {
                        entity.LogisticsPoint = 0;
                    }

                    if (insert.LessTimes == 0) {
                        entity.QuantityDeliveryPoint = 20;
                    }
                    else if (insert.LessTimes == 1 || insert.LessTimes == 2) {
                        entity.QuantityDeliveryPoint = 15;
                    }
                    else if (insert.LessTimes >= 3 && insert.LessTimes <= 5) {
                        entity.QuantityDeliveryPoint = 10;
                    }
                    else if (insert.LessTimes > 5) {
                        entity.QuantityDeliveryPoint = 0;
                    }


                    if (insert.NGTimes == 0) {
                        entity.NGTimesPoint = 20;
                    }
                    else if (insert.NGTimes == 1 || insert.NGTimes == 2) {
                        entity.NGTimesPoint = 15;
                    }
                    else if (insert.NGTimes == 3 || insert.NGTimes == 4) {
                        entity.NGTimesPoint = 10;
                    }
                    else if (insert.NGTimes > 4) {
                        entity.NGTimesPoint = 0;
                    }

                    if (insert.PercentNGNumber < 2) {
                        entity.NGNumberPoint = 20;
                    }
                    else if (insert.PercentNGNumber >= 2 && insert.PercentNGNumber < 3) {
                        entity.NGNumberPoint = 15;
                    }
                    else if (insert.PercentNGNumber >= 3 && insert.PercentNGNumber <= 4) {
                        entity.NGNumberPoint = 10;
                    }
                    else if (insert.PercentNGNumber > 4) {
                        entity.NGNumberPoint = 0;
                    }

                    entity.TotalPoint = entity.PricePoint +
                                entity.TechSpPoint +
                                entity.LogisticsPoint +
                                entity.QuantityDeliveryPoint +
                                entity.NGTimesPoint +
                                entity.NGNumberPoint;

                    vfi.VendorObjectives.Add(entity);
                    vfi.SaveChanges();

                }
            }
            catch (Exception ex) {
                var message = ex.Message;
                if (ex.InnerException != null) {
                    message += " | Inner Exception: " + ex.InnerException.ToString();
                }
                return Json(message);
            }

            return View(new GridModel(GetVendorObjective(vendorId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateVendorObjective(VendorObjectiveModel update, int vendorId) {
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
                    var entity = vfi.VendorObjectives.FirstOrDefault(t => t.VendorId == vendorId && t.ObjectiveId == update.ObjectiveId);
                    if (entity == null)
                        throw new AggregateException("Lỗi! Không tìm thấy NCC");
                    else if (entity != null) {
                        entity.ModifiedDate = DateTime.Now;
                        entity.ModifiedUser = HttpContext.User.Identity.Name;

                        if (update.PricePoint != entity.PricePoint) {
                            switch (update.PricePoint) {
                                case 0:
                                case 5:
                                case 10:
                                    entity.PricePoint = update.PricePoint;
                                    break;
                                default:
                                    throw new AggregateException("Chỉ được sửa điểm 'Competitivity' trong (0 - 5 - 10)");
                            }
                        }
                        if (update.TechSpPoint != entity.TechSpPoint) {
                            switch (update.TechSpPoint) {
                                case 0:
                                case 5:
                                case 10:
                                    entity.TechSpPoint = update.TechSpPoint;
                                    break;
                                default:
                                    throw new AggregateException("Chỉ được sửa điểm 'Technical Support' trong (0 - 5 - 10)");
                            }
                        }

                        if (update.Note != entity.Note) {
                            entity.Note = update.Note;
                        }

                        if (update.LateTimes != entity.LateTimes) {
                            entity.LateTimes = update.LateTimes;
                            if (update.LateTimes == 0) {
                                entity.LogisticsPoint = 20;
                            }
                            else if (update.LateTimes == 1 || update.LateTimes == 2) {
                                entity.LogisticsPoint = 15;
                            }
                            else if (update.LateTimes >= 3 && update.LateTimes <= 5) {
                                entity.LogisticsPoint = 10;
                            }
                            else if (update.LateTimes > 5) {
                                entity.LogisticsPoint = 0;
                            }
                        }

                        if (update.LessTimes != entity.LessTimes) {
                            entity.LessTimes = update.LessTimes;
                            if (update.LessTimes == 0) {
                                entity.QuantityDeliveryPoint = 20;
                            }
                            else if (update.LessTimes == 1 || update.LessTimes == 2) {
                                entity.QuantityDeliveryPoint = 15;
                            }
                            else if (update.LessTimes >= 3 && update.LessTimes <= 5) {
                                entity.QuantityDeliveryPoint = 10;
                            }
                            else if (update.LessTimes > 5) {
                                entity.QuantityDeliveryPoint = 0;
                            }
                        }

                        if (update.NGTimes != entity.NGTimes) {
                            entity.NGTimes = update.NGTimes;
                            if (update.NGTimes == 0) {
                                entity.NGTimesPoint = 20;
                            }
                            else if (update.NGTimes == 1 || update.NGTimes == 2) {
                                entity.NGTimesPoint = 15;
                            }
                            else if (update.NGTimes == 3 || update.NGTimes == 4) {
                                entity.NGTimesPoint = 10;
                            }
                            else if (update.NGTimes > 4) {
                                entity.NGTimesPoint = 0;
                            }
                        }

                        if (update.PercentNGNumber != entity.PercentNGNumber) {
                            entity.PercentNGNumber = Math.Round(update.PercentNGNumber, 1);
                            if (update.PercentNGNumber < 2) {
                                entity.NGNumberPoint = 20;
                            }
                            else if (update.PercentNGNumber >= 2 && update.PercentNGNumber < 3) {
                                entity.NGNumberPoint = 15;
                            }
                            else if (update.PercentNGNumber >= 3 && update.PercentNGNumber <= 4) {
                                entity.NGNumberPoint = 10;
                            }
                            else if (update.PercentNGNumber > 4) {
                                entity.NGNumberPoint = 0;
                            }
                        }

                        entity.TotalPoint = entity.PricePoint +
                                            entity.TechSpPoint +
                                            entity.LogisticsPoint +
                                            entity.QuantityDeliveryPoint +
                                            entity.NGTimesPoint +
                                            entity.NGNumberPoint;




                        vfi.SaveChanges();
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateVendorObjective", ex.Message);
            }

            return View(new GridModel(GetVendorObjective(vendorId)));
        }

        # endregion
    }
}
