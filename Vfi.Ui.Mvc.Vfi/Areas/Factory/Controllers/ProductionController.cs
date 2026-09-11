using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Utilities;
using System.Globalization;
using System.Threading;

namespace Vfi.Ui.Mvc.Vfi.Areas.Factory.Controllers {
    public class ProductionController : Controller {

        #region view

        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }
            return ViewData;
        }

        public ActionResult OutsideProcessManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductionLevelManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProductionOnTesting(int machineId, int productId) {
            var entity = new MachineDiagram { MachineId = machineId, ProductId = productId };
            try {
                using (var vfi = new tammaContext()) {
                    var machine = vfi.Machines.FirstOrDefault(x => x.MachineId == machineId);
                    var product = vfi.Products.FirstOrDefault(x => x.ProductId == productId);
                    if (machine.ProcessingType.Warehouse.IsProduction) {
                        var testingNotes = vfi.ProductionTestingNotes.Where(x => x.Active && x.Warehouse.IsProduction && x.ProductId == productId)
                                                                    .OrderBy(x => x.Idx)
                                                                    .Select(x => x.Note)
                                                                    .ToList();
                        var productImgs = vfi.ProductImgs.Where(x => x.ProductId == productId && x.Warehouse.IsProduction)
                                                        .OrderBy(x => x.Step)
                                                        .Select(x => new ProductImgModel {
                                                            ImgUrl = x.ImgUrl,
                                                            ModifiedDate = x.ModifiedDate
                                                        })
                                                        .ToList();
                        var qcImgs = vfi.QcImgs.Where(t => t.ProductId == productId
                                                      && t.Type == 1
                                                      && t.Active == true)
                                               .OrderBy(t => t.ImgId)
                                               .Select(t => new QcImgModel {
                                                   ImgUrl = t.ImgUrl,
                                                   ModifiedDate = t.ModifiedDate,
                                               }).ToList();
                        entity = new MachineDiagram {
                            MachineId = machineId,
                            MachineName = machine != null ? machine.MachineName : "Không tìm thấy máy " + machineId,
                            ProductId = productId,
                            ProductCode = product != null ? product.ProductCode : "Không tìm thấy sản phẩm " + productId,
                            Notes = testingNotes,
                            ProductImgs = productImgs,
                            QcImgs = qcImgs,
                            WarehouseId = machine.ProcessingType.ForWarehouseId.Value
                        };
                    }
                    else if (machine.ProcessingType.Warehouse.IsCncMilling) {
                        var testingNotes = vfi.ProductionTestingNotes.Where(x => x.Active && x.Warehouse.IsCncMilling 
                                                                            && x.ProductId == productId)
                                                                    .OrderBy(x => x.Idx)
                                                                    .Select(x => x.Note)
                                                                    .ToList();
                        var productImgs = vfi.ProductImgs.Where(x => x.ProductId == productId && x.Warehouse.IsCncMilling)
                                                        .OrderBy(x => x.Step)
                                                        .Select(x => new ProductImgModel {
                                                            ImgUrl = x.ImgUrl,
                                                            ModifiedDate = x.ModifiedDate
                                                        })
                                                        .ToList();
                        var qcImgs = vfi.QcImgs.Where(t => t.ProductId == productId
                                                      && t.Active == true)
                                               .OrderBy(t => t.ImgId)
                                               .Select(t => new QcImgModel {
                                                   ImgUrl = t.ImgUrl,
                                                   ModifiedDate = t.ModifiedDate,
                                               }).ToList();
                        entity = new MachineDiagram {
                            MachineId = machineId,
                            MachineName = machine != null ? machine.MachineName : "Không tìm thấy máy " + machineId,
                            ProductId = productId,
                            ProductCode = product != null ? product.ProductCode : "Không tìm thấy sản phẩm " + productId,
                            Notes = testingNotes,
                            ProductImgs = productImgs,
                            QcImgs = qcImgs,
                            WarehouseId = machine.ProcessingType.ForWarehouseId.Value
                        };
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ProductionOnTesting", ex.Message);
            }
            ViewData = GetPageConfigData();
            return View(entity);
        }
        public ActionResult ProductionOnTestingInput() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        #endregion

        #region heat treatment management

        [GridAction]
        public ActionResult SelectProductionHeatTreatment(int customerId, int productId, string productCode) {
            var model = new List<ProductionHeatTreatmentModel>();
            try {
                model = GetProductionHeatTreatmentByProductId(customerId, productId, productCode);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionHeatTreatment", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionHeatTreatmentModel> GetProductionHeatTreatmentByProductId(int customerId, int productId, string productCode) {
            var model = new List<ProductionHeatTreatmentModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.ProductionHeatTreatments
                         where (productId == 0 || x.ProductId == productId) &&
                             (customerId == 0 || x.Product.CustomerId == customerId)
                         select new ProductionHeatTreatmentModel {
                             Id = x.Id,
                             Name = x.Name,
                             Section = x.Section,
                             Rate = x.Rate,
                             Timing = x.Timing,
                             Note = x.Note,
                             Temperature = x.Temperature,
                             Stiffness = x.Stiffness,
                             ModifiedDate = x.ModifiedDate,
                             ModifiedUser = x.ModifiedUser,
                             Active = x.Active,
                             ProductId = x.ProductId,
                             ProductCode = x.Product.ProductCode,
                             CustomerId = x.Product.CustomerId,
                             CustomerCode = x.Product.Customer.CustomerCode,
                             MachineId = x.MachineId ?? 0,
                             MachineName = x.MachineId != null ? x.Machine.MachineName : "",
                             IsCalculateLock = x.Product.IsCalculateLock ?? false
                         }).ToList();
                if (!string.IsNullOrWhiteSpace(productCode)) {
                    model = model.Where(x => x.ProductCode.Contains(productCode)).ToList();
                }
            }
            return model.OrderBy(x => x.CustomerCode).ThenBy(x => x.ProductCode).ThenBy(m => m.Section).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionHeatTreatment(ProductionHeatTreatmentModel insert, int productId) {
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

                    var entity = new ProductionHeatTreatment {
                        Name = insert.Name,
                        Section = insert.Section,
                        Rate = insert.Rate,
                        Timing = insert.Timing,
                        Note = insert.Note,
                        Temperature = insert.Temperature,
                        Stiffness = insert.Stiffness,
                        Active = true,
                        ProductId = productId,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name
                    };
                    var machineId = 0;
                    try { machineId = Convert.ToInt32(insert.MachineName); }
                    catch (Exception) { }
                    if (machineId != 0) { entity.MachineId = machineId; }

                    vfi.ProductionHeatTreatments.Add(entity);
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionHeatTreatment", ex.Message);
            }

            return View(new GridModel(GetProductionHeatTreatmentByProductId(0, productId, "")));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionHeatTreatment(ProductionHeatTreatmentModel update, int productId) {
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
                    var entity = vfi.ProductionHeatTreatments.FirstOrDefault(pt => pt.Id == update.Id);
                    if (entity == null)
                        throw new AggregateException("Lỗi! Không tìm thấy công cụ trong sản phấm! Liên hệ admin");
                    entity.Name = update.Name;
                    entity.Section = update.Section;
                    entity.Rate = update.Rate;
                    entity.Timing = update.Timing;
                    entity.Note = update.Note;
                    entity.Temperature = update.Temperature;
                    entity.Stiffness = update.Stiffness;
                    entity.Active = update.Active;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    var machineId = 0;
                    try { machineId = Convert.ToInt32(update.MachineName); }
                    catch (Exception) { }
                    if (machineId != 0 && machineId != entity.MachineId) { entity.MachineId = machineId; }

                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionHeatTreatment", ex.Message);
            }

            return View(new GridModel(GetProductionHeatTreatmentByProductId(0, productId, "")));
        }

        #endregion

        #region polish management

        [GridAction]
        public ActionResult SelectProductionPolish(int customerId, int productId, string productCode) {
            var model = new List<ProductionPolishModel>();
            try {
                model = GetProductionPolishByProductId(customerId, productId, productCode);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionPolish", ex.Message);
            }
            return View(new GridModel(model));
        }
        List<ProductionPolishModel> GetProductionPolishByProductId(int customerId, int productId, string productCode) {
            var model = new List<ProductionPolishModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.ProductionPolishes
                         where (productId == 0 || x.ProductId == productId) &&
                                 (customerId == 0 || x.Product.CustomerId == customerId)
                         select new ProductionPolishModel {
                             Id = x.Id,
                             Name = x.Name,
                             Section = x.Section,
                             Rate = x.Rate,
                             Timing = x.Timing,
                             Note = x.Note,
                             Rock = x.Rock,
                             Using = x.Using,
                             ModifiedDate = x.ModifiedDate,
                             ModifiedUser = x.ModifiedUser,
                             Active = x.Active,
                             ProductId = x.ProductId,
                             ProductCode = x.Product.ProductCode,
                             CustomerId = x.Product.CustomerId,
                             CustomerCode = x.Product.Customer.CustomerCode,
                             MachineId = x.MachineId ?? 0,
                             MachineName = x.MachineId != null ? x.Machine.MachineName : ""
                         }).ToList();
                if (!string.IsNullOrWhiteSpace(productCode)) {
                    model = model.Where(x => x.ProductCode.Contains(productCode)).ToList();
                }
            }
            return model.OrderBy(x => x.CustomerCode).ThenBy(x => x.ProductCode).ThenBy(m => m.Section).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionPolish(ProductionPolishModel insert, int productId) {
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

                    var entity = new ProductionPolish {
                        Name = insert.Name,
                        Section = insert.Section,
                        Rate = insert.Rate,
                        Timing = insert.Timing,
                        Note = insert.Note,
                        Rock = insert.Rock,
                        Using = insert.Using,
                        Active = true,
                        ProductId = productId,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name
                    };

                    var machineId = 0;
                    try { machineId = Convert.ToInt32(insert.MachineName); }
                    catch (Exception) { }
                    if (machineId != 0) { entity.MachineId = machineId; }

                    vfi.ProductionPolishes.Add(entity);
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionPolish", ex.Message);
            }

            return View(new GridModel(GetProductionPolishByProductId(0, productId, "")));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionPolish(ProductionPolishModel update, int productId) {
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
                    var entity = vfi.ProductionPolishes.FirstOrDefault(pt => pt.Id == update.Id);
                    if (entity == null)
                        throw new AggregateException("Lỗi! Không tìm thấy công cụ trong sản phấm! Liên hệ admin");
                    entity.Name = update.Name;
                    entity.Section = update.Section;
                    entity.Rate = update.Rate;
                    entity.Timing = update.Timing;
                    entity.Note = update.Note;
                    entity.Rock = update.Rock;
                    entity.Using = update.Using;
                    entity.Active = update.Active;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;

                    var machineId = 0;
                    try { machineId = Convert.ToInt32(update.MachineName); }
                    catch (Exception) { }
                    if (machineId != 0 && machineId != entity.MachineId) { entity.MachineId = machineId; }

                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionPolish", ex.Message);
            }

            return View(new GridModel(GetProductionPolishByProductId(0, productId, "")));
        }

        #endregion

        #region plating management

        [GridAction]
        public ActionResult SelectProductionPlating(int customerId, int productId, string productCode, bool isQuote =  false) {
            var model = new List<ProductionPlatingModel>();
            try {
                model = GetProductionPlatingByProductId(customerId, productId, productCode, isQuote);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionPlating", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionPlatingModel> GetProductionPlatingByProductId(int customerId, int productId, string productCode, bool isQuote) {
            var model = new List<ProductionPlatingModel>();
            using (var vfi = new tammaContext()) {

                var processes = (from x in vfi.ProductionProcesses
                                 where (productId == 0 || x.ProductId == productId) &&
                                 (customerId == 0 || x.Product.CustomerId == customerId) &&
                                 x.Warehouse.IsPlating
                                 select new {
                                     x.ProcessId,
                                     ModifiedDate = x.ModifiedDate,
                                     ModifiedUser = x.ModifiedUser,
                                     x.IsAlert,
                                     x.IsNecessary,
                                     ProductId = x.ProductId,
                                     ProductCode = x.Product.ProductCode,
                                     CustomerCode = x.Product.Customer.CustomerCode,
                                     UnitWeight = x.Product.OutsideProcessWeight ?? 0,
                                     x.Product.ProductionPlatings,
                                 }).ToList();
                if (!string.IsNullOrWhiteSpace(productCode)) {
                    processes = processes.Where(x => x.ProductCode.Contains(productCode)).ToList();
                }
                foreach (var process in processes) {
                    if (process.ProductionPlatings.Any()) {
                        foreach (var plating in process.ProductionPlatings) {
                            var entity = new ProductionPlatingModel {
                                ProductCode = process.ProductCode,
                                CustomerCode = process.CustomerCode,
                                ModifiedDate = plating.ModifiedDate,
                                ModifiedUser = plating.ModifiedUser,
                                Active = plating.Active,
                                ProductId = plating.ProductId,
                                PlatingId = plating.PlatingId,
                                Description = plating.Description + "",
                                PlatingCost = plating.PlatingCost,
                                PlatingIndex = plating.PlatingIndex,
                                PlatingDay = plating.PlatingDay,
                                SaltSprayTime = plating.SaltSprayTime,
                                Thickness = plating.Thickness,
                                IsMainProcess = plating.IsMainProcess,
                                OutsideProcessId = plating.ProcessId ?? 0,
                                PlatingName = plating.PlatingName,
                                UnitWeight = process.UnitWeight,
                            };
                            model.Add(entity);
                        }
                    }
                    else {
                        var entity = new ProductionPlatingModel {
                            ProductCode = process.ProductCode,
                            CustomerCode = process.CustomerCode,
                            ModifiedDate = process.ModifiedDate,
                            ModifiedUser = process.ModifiedUser,
                            Active = process.IsNecessary,
                            IsMainProcess = process.IsNecessary,
                            ProductId = process.ProductId,
                            PlatingId = 0,
                            Description = "",
                            PlatingCost = 0,
                            PlatingIndex = 0,
                            PlatingDay = 0,
                            SaltSprayTime = "",
                            Thickness = "",
                            OutsideProcessId = process.ProcessId,
                            PlatingName = "Chưa thiết lập",
                            UnitWeight = process.UnitWeight,
                        };
                        model.Add(entity);
                    }
                }
                //model = (from x in vfi.ProductionPlatings
                //         where (productId == 0 || x.ProductId == productId) &&
                //                (customerId == 0 || x.Product.CustomerId == customerId)
                //         select new ProductionPlatingModel {
                //             ModifiedDate = x.ModifiedDate,
                //             ModifiedUser = x.ModifiedUser,
                //             Active = x.Active,
                //             ProductId = x.ProductId,
                //             PlatingId = x.PlatingId,
                //             Description = x.Description + "",
                //             PlatingCost = x.PlatingCost,
                //             PlatingIndex = x.PlatingIndex,
                //             PlatingDay = x.PlatingDay,
                //             SaltSprayTime = x.SaltSprayTime,
                //             Thickness = x.Thickness,
                //             ProductCode = x.Product.ProductCode,
                //             CustomerCode = x.Product.Customer.CustomerCode,
                //             IsMainProcess = x.IsMainProcess,
                //             OutsideProcessId = x.ProcessId ?? 0,
                //             PlatingName = x.PlatingName,
                //             UnitWeight = x.Product.OutsideProcessWeight > 0
                //                         ? x.Product.OutsideProcessWeight.Value
                //                         : (x.Product.QcWeight ?? 0),
                //             //ProcessPrice = x.ProcessId != null ? x.OutsideProcess.Price : 0,
                //             //ProcessPrice = x.PlatingCost
                //         }).ToList();
                //if (!string.IsNullOrWhiteSpace(productCode)) {
                //    model = model.Where(x => x.ProductCode.Contains(productCode)).ToList();
                //}
                if (isQuote) {
                    model = model.Where(x => x.Active && x.IsMainProcess).ToList();
                }
                model.ForEach(x => {
                    if (x.UnitWeight > 0) {
                        //x.PlatingCost = Math.Round(x.ProcessPrice / x.UnitWeight, 4);
                        x.ProcessPrice = (x.PlatingCost * x.UnitWeight) / 1000;
                    }
                });
            }
            return model.OrderBy(x => !x.Active)
                .ThenBy(x => x.CustomerCode)
                .ThenBy(x => x.ProductCode)
                .ThenBy(m => m.PlatingIndex).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionPlating(ProductionPlatingModel insert, int productId) {
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
                    var processId = 0;
                    try {
                        processId = Convert.ToInt32(insert.PlatingName);
                    }
                    catch (Exception) { }
                    if (processId == 0) { throw new AggregateException("Lỗi! Vui lòng chọn lại gia công ngoài!"); }
                    var outsideProcess = vfi.OutsideProcesses.FirstOrDefault(x => x.ProcessId == processId);
                    if (outsideProcess == null) { throw new AggregateException("Lỗi! Vui lòng chọn lại gia công ngoài!"); }
                    var entity = new ProductionPlating {
                        ProductId = productId,
                        Active = true,
                        //PlatingName = insert.PlatingName,
                        PlatingName = outsideProcess.Name,
                        ProcessId = outsideProcess.ProcessId,
                        Description = insert.Description + "",
                        InsertDate = DateTime.Now,
                        InserUser = HttpContext.User.Identity.Name,
                        PlatingCost = 0,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        PlatingIndex = insert.PlatingIndex,
                        PlatingDay = insert.PlatingDay,
                        SaltSprayTime = insert.SaltSprayTime,
                        Thickness = insert.Thickness,
                        IsMainProcess = insert.IsMainProcess,
                    };
                    vfi.ProductionPlatings.Add(entity);
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionPlating", ex.Message);
            }

            return View(new GridModel(GetProductionPlatingByProductId(0, productId, "", false)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionPlating(ProductionPlatingModel update, int productId) {
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
                    var entity = vfi.ProductionPlatings.FirstOrDefault(pt => pt.PlatingId == update.PlatingId);
                    //if (entity == null)
                    //    throw new AggregateException("Lỗi! Không tìm thấy công đoạn trong sản phấm! Liên hệ admin");

                    var processId = 0;
                    try {
                        processId = Convert.ToInt32(update.PlatingName);
                    }
                    catch (Exception) { }
                    if (processId != 0) {
                        var outsideProcess = vfi.OutsideProcesses.FirstOrDefault(x => x.ProcessId == processId);
                        if (outsideProcess == null) { throw new AggregateException("Lỗi! Vui lòng chọn lại gia công ngoài!"); }
                        update.PlatingName = outsideProcess.Name;
                        update.OutsideProcessId = processId;
                    }
                    if (update.PlatingId == 0) {
                        entity = new ProductionPlating {
                            ProductId = productId,
                            Active = true,
                            //PlatingName = insert.PlatingName,
                            PlatingName = update.PlatingName,
                            ProcessId = update.OutsideProcessId,
                            Description = update.Description + "",
                            InsertDate = DateTime.Now,
                            InserUser = HttpContext.User.Identity.Name,
                            PlatingCost = 0,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            PlatingIndex = update.PlatingIndex,
                            PlatingDay = update.PlatingDay,
                            SaltSprayTime = update.SaltSprayTime,
                            Thickness = update.Thickness,
                            IsMainProcess = update.IsMainProcess,
                        };
                        vfi.ProductionPlatings.Add(entity);
                    }
                    else {
                        entity.Active = update.Active;
                        entity.IsMainProcess = update.IsMainProcess;
                        entity.ModifiedDate = DateTime.Now;
                        entity.ModifiedUser = HttpContext.User.Identity.Name;
                        if (processId != 0) {
                            entity.ProcessId = update.OutsideProcessId;
                            entity.PlatingName = update.PlatingName;
                        }
                        entity.PlatingIndex = update.PlatingIndex;
                        entity.Description = update.Description + "";
                        entity.PlatingDay = update.PlatingDay;
                        entity.SaltSprayTime = update.SaltSprayTime;
                        entity.Thickness = update.Thickness;
                        //entity.PlatingCost = 0;
                    }
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionPlating", ex.Message);
            }

            return View(new GridModel(GetProductionPlatingByProductId(0, productId, "", false)));
        }

        #endregion

        #region production section management

        [GridAction]
        public ActionResult SelectProductionSection(int productId, bool isQuote = false) {
            var model = new List<ProductionSectionModel>();
            try {
                model = GetProductionSectionByProductId(productId, isQuote);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionSection", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionSectionModel> GetProductionSectionByProductId(int productId,bool isQuote) {
            var model = new List<ProductionSectionModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.ProductionSections.Where(x => productId == 0 || x.ProductId == productId)
                    .Select(entity => new ProductionSectionModel {
                        ProductionSectionId = entity.ProductionSectionId,
                        SectionName = entity.Section.SectionName,
                        SectionCost = entity.Section.SaleFactor * entity.Productivity,
                        UpdateDate = entity.UpdateDate ?? DateTime.Now,
                        UpdateUser = entity.UpdateUser,
                        Description = entity.Description,
                        Active = entity.Active,
                        IsProductionManagement = 1,
                        IsSaleManagement = 1,
                        SectionIndex = entity.SectionIndex,
                        Productivity = entity.Productivity,
                        Weight = entity.Weight,
                        IsMainProcess = entity.IsMainProcess,
                        MachineId = entity.MachineId ?? 0,
                        MachineName = entity.MachineId != null ? entity.Machine.MachineName : "",
                        SalesFactor = entity.Section.SaleFactor,
                        SectionPrice = Math.Round(entity.Section.SaleFactor * entity.Productivity, 4),
                        IsCalculateLock = entity.Product.IsCalculateLock ?? false
                    }).ToList();
                if (isQuote) {
                    model = model.Where(x => x.Active && x.IsMainProcess).ToList();
                }
            }
            return model.OrderBy(m => m.SectionIndex).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionSection(ProductionSectionModel insert, int productId) {
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
                    int sectionId = 1;
                    try {
                        sectionId = Convert.ToInt32(insert.SectionName);
                    }
                    catch (Exception) {
                        var section2 = vfi.Sections.FirstOrDefault(w => w.SectionName.Equals(insert.SectionName));
                        if (section2 == null)
                            throw new AggregateException("Lỗi công đoạn ! Chọn lại công đoạn");
                        sectionId = section2.SectionId;
                    }


                    var entity = new ProductionSection {
                        ProductId = productId,
                        Active = true,
                        Description = insert.Description + "",
                        UpdateDate = DateTime.Now,
                        UpdateUser = HttpContext.User.Identity.Name,
                        Productivity = insert.Productivity,
                        SectionIndex = insert.SectionIndex,
                        SectionId = sectionId,
                        InsertDate = DateTime.Now,
                        InsertUser = HttpContext.User.Identity.Name,
                        Weight = insert.Weight,
                        IsMainProcess = insert.IsMainProcess
                    };

                    var machineId = 0;
                    try { machineId = Convert.ToInt32(insert.MachineName); }
                    catch (Exception) { }
                    if (machineId != 0) { entity.MachineId = machineId; }

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
                    vfi.ProductionSections.Add(entity);
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionSection", ex.Message);
            }

            return View(new GridModel(GetProductionSectionByProductId(productId, false)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionSection(ProductionSectionModel update, int productId) {
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
                    var entity =
                        vfi.ProductionSections.FirstOrDefault(
                            ps => ps.ProductionSectionId == update.ProductionSectionId);
                    if (entity == null)
                        throw new AggregateException("Lỗi! Không tìm thấy công cụ trong sản phấm! Liên hệ admin");
                    int sectionId = 0;
                    if (!string.IsNullOrWhiteSpace(update.SectionName)) {
                        try {
                            sectionId = Convert.ToInt32(update.SectionName);
                        }
                        catch (Exception) {
                            var section2 =
                                vfi.Sections.FirstOrDefault(w => w.SectionName.Equals(update.SectionName));
                            if (section2 == null)
                                throw new AggregateException("Lỗi ! Chọn lại gia công");
                            sectionId = section2.SectionId;
                        }
                    }
                    if (sectionId != 0)
                        entity.SectionId = sectionId;

                    var machineId = 0;
                    try { machineId = Convert.ToInt32(update.MachineName); }
                    catch (Exception) { }
                    if (machineId != 0 && machineId != entity.MachineId) { entity.MachineId = machineId; }

                    entity.Active = update.Active;
                    entity.Description = update.Description;
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateUser = HttpContext.User.Identity.Name;
                    entity.SectionIndex = update.SectionIndex;
                    entity.Weight = update.Weight;
                    entity.IsMainProcess = update.IsMainProcess;
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
                    if (entity.Productivity != update.Productivity && update.Productivity != 0) {
                        var log = new SectionLog {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            NewProductivity = update.Productivity,
                            OldProductivity = entity.Productivity,
                            ProductionSectionId = entity.ProductionSectionId
                        };
                        entity.Productivity = update.Productivity;
                        vfi.SectionLogs.Add(log);
                    }
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionSection", ex.Message);
            }

            return View(new GridModel(GetProductionSectionByProductId(productId,false)));
        }

        #endregion

        #region production packing
        [GridAction]
        public ActionResult SelectProductionFuel(int customerId, int productId, string productCode, bool isQuote = false) {
            var model = new List<ProductionFuelModel>();
            try {
                model = GetProductionFuelByProductId(customerId, productId, productCode, isQuote);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionFuel", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionFuelModel> GetProductionFuelByProductId(int customerId, int productId, string productCode, bool isQuote) {
            var model = new List<ProductionFuelModel>();
            using (var vfi = new tammaContext()) {
                var packings = vfi.ProductionFuels.Where(pt => 
                   (isQuote == false || pt.Active) && 
                    (productId == 0 || pt.ProductId == productId) &&
                    (customerId ==0 || pt.Product.CustomerId == customerId)
                    ).ToList();
                if (!string.IsNullOrWhiteSpace(productCode)) {
                    packings = packings.Where(x => x.Product.ProductCode.Contains(productCode)).ToList();
                }
                foreach (var productionFuel in packings) {
                    var entity = new ProductionFuelModel {
                        RealId = productionFuel.RealId,
                        FuelId = productionFuel.FuelId,
                        ProductId = productionFuel.ProductId,
                        ProductCode = productionFuel.Product.ProductCode,
                        CustomerId = productionFuel.Product.CustomerId,
                        CustomerCode = productionFuel.Product.Customer.CustomerCode,
                        Priority = productionFuel.Priority,
                        Active = productionFuel.Active,
                        ModifiedDate = productionFuel.ModifiedDate,
                        ModifiedUser = productionFuel.ModifiedUser,
                        FuelFullCode = productionFuel.Fuel.FuelFullCode,
                        FuelCode = productionFuel.Fuel.FuelCode,
                        FuelName = productionFuel.Fuel.FuelName,
                        FuelDesign = productionFuel.Fuel.FuelDesignNo,
                        Note = productionFuel.Note,
                        Quota = productionFuel.Quota,

                        Fuel2Id = productionFuel.Fuel2Id,
                        CrossWeight = productionFuel.CrossWeight,
                        CrossWeight2 = productionFuel.CrossWeight2,
                        Quota2 = productionFuel.Quota2,
                    };
                    if (productionFuel.Fuel2Id != null) {
                        entity.FuelFullCode2 = productionFuel.Fuel1.FuelFullCode;
                        entity.FuelCode2 = productionFuel.Fuel1.FuelCode;
                        entity.FuelName2 = productionFuel.Fuel1.FuelName;
                    }
                    model.Add(entity);
                }
            }
            return model.OrderBy(x => x.CustomerCode).ThenBy(x => x.ProductCode).ThenBy(m => m.Priority).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionFuel(ProductionFuelModel insert, int productId) {
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
                    int fuelId = 1;
                    try {
                        fuelId = Convert.ToInt32(insert.FuelName);
                    }
                    catch (Exception) {
                        fuelId = vfi.Fuels.FirstOrDefault(w => w.FuelFullCode.Equals(insert.FuelName)).FuelId;
                    }
                    Nullable<int> fuelId2 = null;
                    try {
                        fuelId2 = Convert.ToInt32(insert.FuelName2);
                    }
                    catch (Exception) { }
                    var entity =
                        vfi.ProductionFuels.FirstOrDefault(
                            pt => pt.ProductId == productId && pt.FuelId == fuelId && pt.Fuel2Id == fuelId2 && !pt.Active);
                    if (entity == null) {
                        entity = new ProductionFuel {
                            FuelId = fuelId,
                            ProductId = productId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Active = true,
                            Priority = insert.Priority,
                            Note = insert.Note,
                            Quota = insert.Quota,

                            Fuel2Id = fuelId2,
                            CrossWeight = insert.CrossWeight,
                            CrossWeight2 = insert.CrossWeight2,
                            Quota2 = insert.Quota2,
                        };
                        vfi.ProductionFuels.Add(entity);
                    }
                    else {
                        entity.ModifiedDate = DateTime.Now;
                        entity.ModifiedUser = HttpContext.User.Identity.Name;
                        entity.Active = true;
                        entity.Priority = insert.Priority;
                        entity.Note = insert.Note;
                        entity.Quota = insert.Quota;
                        entity.CrossWeight = insert.CrossWeight;
                        entity.CrossWeight2 = insert.CrossWeight2;
                        entity.Quota2 = insert.Quota2;
                    }
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionFuel", ex.Message);
            }

            return View(new GridModel(GetProductionFuelByProductId(0, productId, "", false)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionFuel(ProductionFuelModel update, int productId) {
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
                    int fuelId = 0;
                    try {
                        fuelId = Convert.ToInt32(update.FuelName);
                    }
                    catch (Exception) { }
                    Nullable<int> fuelId2 = null;
                    try {
                        if (!string.IsNullOrWhiteSpace(update.FuelName2))
                            fuelId2 = Convert.ToInt32(update.FuelName2);
                    }
                    catch (Exception) { }
                    var entity = vfi.ProductionFuels.FirstOrDefault(pt => pt.RealId == update.RealId);
                    if (entity == null)
                        throw new AggregateException("Lỗi! Không tìm thấy thùng/túi/vĩ trong sản phấm! Liên hệ admin");
                    if (fuelId > 0) {
                        entity.FuelId = fuelId;
                    }
                    //entity.ProductId = productId;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    entity.Active = update.Active;
                    entity.Priority = update.Priority;
                    entity.Note = update.Note;
                    entity.Quota = update.Quota;
                    if (fuelId2 == null) {
                        entity.Fuel2Id = null;
                    }
                    else if (fuelId2 > 0) {
                        entity.Fuel2Id = fuelId2;
                    }
                    entity.CrossWeight = update.CrossWeight;
                    entity.CrossWeight2 = update.CrossWeight2;
                    entity.Quota2 = update.Quota2;
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionFuel", ex.Message);
            }

            return View(new GridModel(GetProductionFuelByProductId(0, productId, "", false)));
        }

        public ActionResult SelectComboboxProductionFuel(int productId) {
            var model = new List<FuelModel>();
            using (var vfi = new tammaContext()) {
                var productionFuels = vfi.ProductionFuels.Where(x => x.ProductId == productId && x.Active).ToList();
                foreach (var productionFuel in productionFuels) {
                    var entity1 = new FuelModel {
                        FuelId = productionFuel.FuelId,
                        FuelFullCode = productionFuel.Fuel.FuelFullCode.Trim(),
                        UnitWeight = productionFuel.Fuel.UnitWeight,
                        Quota = productionFuel.Quota,
                        CrossWeight= productionFuel.CrossWeight,
                    };
                    model.Add(entity1);
                    if (productionFuel.Fuel2Id > 0) {
                        var entity2 = new FuelModel {
                            FuelId = productionFuel.Fuel2Id.Value,
                            FuelFullCode = productionFuel.Fuel1.FuelFullCode.Trim(),
                            UnitWeight = productionFuel.Fuel1.UnitWeight,
                            Quota = productionFuel.Quota2,
                            CrossWeight = productionFuel.CrossWeight2,
                        };
                        model.Add(entity2);
                    }
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "ProductionFuelId", "FuelFullCode")
            };
        }
        #endregion

        #region production testing

        [GridAction]
        public ActionResult SelectProductionTesting(int customerId, int productId, string productCode) {
            var model = new List<ProductionTestingModel>();
            try {
                model = GetProductionTestingById(customerId, productId, productCode);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionTesting", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionTestingModel> GetProductionTestingById(int customerId, int productId, string productCode) {
            var model = new List<ProductionTestingModel>();
            using (var vfi = new tammaContext()) {
                var processes = (from x in vfi.ProductionProcesses
                                where x.IsNecessary && x.IsAlert &&
                                (customerId == 0 || x.Product.CustomerId == customerId) &&
                                (productId == 0 || x.ProductId == productId)
                                select x).ToList();
                if(!string.IsNullOrWhiteSpace(productCode)){
                    processes = processes.Where(x => x.Product.ProductCode.Contains(productCode)).ToList();
                }
                foreach (var process in processes) {
                    var entity = new ProductionTestingModel {
                        Idx = process.ProcessIndex,
                        ProductId = process.ProductId,
                        ProductCode = process.Product.ProductCode,
                        CustomerId = process.Product.CustomerId,
                        CustomerCode = process.Product.Customer.CustomerCode,
                        WarehouseId = process.WarehouseId,
                        WarehouseName = process.Warehouse.WarehouseName,
                        ProductionTestingId = 0,
                        ModifiedUser = "Auto-" + process.ModifiedUser,
                        ModifiedDate = process.ModifiedDate,
                    };
                    var productionTesting = vfi.ProductionTestings.FirstOrDefault(x => x.ProductId == entity.ProductId && x.WarehouseId == entity.WarehouseId);
                    if (productionTesting != null) {
                        entity.ProductionTestingId = productionTesting.ProductionTestingId;
                        entity.Note = productionTesting.Note;
                        entity.ModifiedUser = productionTesting.ModifiedUser;
                        entity.ModifiedDate = productionTesting.ModifiedDate;
                    }
                    model.Add(entity);
                }
            }
            return model.OrderBy(x => x.CustomerCode).ThenBy(x => x.ProductCode).ThenBy(m => m.Idx).ToList();

        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionTesting(ProductionTestingModel update, int productId) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var productionTesting = vfi.ProductionTestings.FirstOrDefault(x => x.ProductId == productId && x.WarehouseId == update.WarehouseId);
                    if (productionTesting == null) {
                        productionTesting = new ProductionTesting {
                            ProductId = productId,
                            WarehouseId = update.WarehouseId,
                            Note = update.Note,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                        };
                        vfi.ProductionTestings.Add(productionTesting);
                    }
                    else {
                        productionTesting.Note = update.Note;
                        productionTesting.ModifiedUser = HttpContext.User.Identity.Name;
                        productionTesting.ModifiedDate = DateTime.Now;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionTesting", ex.Message);
            }

            return View(new GridModel(GetProductionTestingById(0, productId, "")));
        }

        [GridAction]
        public ActionResult SelectProductionTestingNote(int productId, int warehouseId) {
            var model = new List<ProductionTestingNoteModel>();
            try {
                model = GetProductionTestingNotesById(productId, warehouseId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionTestingNote", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionTestingNoteModel> GetProductionTestingNotesById(int productId, int warehouseId) {
            var model = new List<ProductionTestingNoteModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.ProductionTestingNotes
                         where x.WarehouseId == warehouseId && x.ProductId == productId
                         select new ProductionTestingNoteModel {
                             WarehouseId = warehouseId,
                             WarehouseName = x.Warehouse.WarehouseName,
                             ProductId = productId,
                             ProductCode = x.Product.ProductCode,
                             NoteId = x.NoteId,
                             Idx = x.Idx,
                             Active = x.Active,
                             ModifiedDate = x.ModifiedDate,
                             ModifiedUser = x.ModifiedUser,
                             Note = x.Note,
                         }).ToList();
            }

            return model.OrderByDescending(x => x.Active).ThenBy(m => m.Idx).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionTestingNote(ProductionTestingNoteModel insert, int productId, int warehouseId) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var entity = new ProductionTestingNote {
                        Idx = insert.Idx,
                        Active = true,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        WarehouseId = warehouseId,
                        ProductId = productId,
                        Note = insert.Note,
                    };
                    vfi.ProductionTestingNotes.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionTestingNote", ex.Message);
            }
            return View(new GridModel(GetProductionTestingNotesById(productId, warehouseId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionTestingNote(ProductionTestingNoteModel update, int productId, int warehouseId) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var entity = vfi.ProductionTestingNotes.FirstOrDefault(x => x.NoteId == update.NoteId);
                    entity.Active = update.Active;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    entity.Note = update.Note;
                    entity.Idx = update.Idx;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionTestingNote", ex.Message);
            }

            return View(new GridModel(GetProductionTestingNotesById(productId, warehouseId)));
        }

        [GridAction]
        public ActionResult SelectProductionTestingDetail(int customerId, string productCode, int productId, int warehouseId) {
            var model = new List<ProductionTestingDetailModel>();
            try {
                model = GetProductionTestingDetailsById(customerId, productCode,productId,  warehouseId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionTestingDetail", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionTestingDetailModel> GetProductionTestingDetailsById(int customerId, string productCode, int productId, int warehouseId) {
            var model = new List<ProductionTestingDetailModel>();
            using (var vfi = new tammaContext()) {
                var testingDetails = (from x in vfi.ProductionTestingDetails
                                      where (customerId == 0 || x.ProductionTesting.Product.CustomerId == customerId) &&
                                      (warehouseId == 0 || x.ProductionTesting.WarehouseId == warehouseId) &&
                                      (productId == 0 || x.ProductionTesting.ProductId == productId)
                                      select new {
                                          x.Active,
                                          x.ModifiedUser,
                                          x.ModifiedDate,

                                          x.DetailId,
                                          x.TestingCode,
                                          x.TestingName,
                                          x.Idx,
                                          x.ProductionTestingId,
                                          x.MinNumber,
                                          x.MaxNumber,
                                          x.TestRate,

                                          x.ProductionTesting.ProductId,
                                          x.ProductionTesting.Product.ProductCode,
                                          x.ProductionTesting.WarehouseId,
                                          x.ProductionTesting.Warehouse.WarehouseName,
                                          x.ProductionTesting.Product.CustomerId,
                                          x.ProductionTesting.Product.Customer.CustomerCode,
                                      }).ToList();
                if (!string.IsNullOrWhiteSpace(productCode)) {
                    testingDetails = testingDetails.Where(x => x.ProductCode.Contains(productCode)).ToList();
                }
                var testingDetailIds = testingDetails.Select(x => x.DetailId).ToList();
                var testingMachines = vfi.ProductionTestingMachines.Where(x => x.Active && testingDetailIds.Contains(x.TestingDetailId)).ToList();
                foreach (var detail in testingDetails) {
                    var entity = new ProductionTestingDetailModel {
                        DetailId = detail.DetailId,
                        TestingCode = detail.TestingCode,
                        TestingName = detail.TestingName,
                        Idx = detail.Idx,
                        ProductionTestingId = detail.ProductionTestingId,
                        MinNumber = detail.MinNumber,
                        MaxNumber = detail.MaxNumber,
                        TestRate = detail.TestRate,
                        ProductId = detail.ProductId,
                        ProductCode = detail.ProductCode,
                        CustomerId = detail.CustomerId,
                        CustomerCode = detail.CustomerCode,
                        WarehouseId = detail.WarehouseId,
                        WarehouseName = detail.WarehouseName,

                        ModifiedUser = detail.ModifiedUser,
                        ModifiedDate = detail.ModifiedDate,
                        Active = detail.Active,
                    };
                    var testingMachinesById = testingMachines.Where(x => x.TestingDetailId == detail.DetailId).ToList();
                    foreach (var testingMachine in testingMachinesById) {
                        entity.TypeIds.Add(testingMachine.MachineTypeId);
                        entity.MachineTypeName += testingMachine.ProcessingType.TypeName + "/ ";
                    }
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.Idx).ThenBy(x => x.TestingCode).ThenBy(x => x.TestingName).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionTestingDetail(ProductionTestingDetailModel insert, int productId, int warehouseId) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    if (insert.ProductionTestingId == 0) {
                        var productionTesting = vfi.ProductionTestings.FirstOrDefault(x => x.ProductId == productId && x.WarehouseId == warehouseId);
                        if (productionTesting == null) {
                            productionTesting = new ProductionTesting {
                                ProductId = productId,
                                WarehouseId = warehouseId,
                                Note = "",
                                ModifiedUser = "Auto-" + HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                            };
                            vfi.ProductionTestings.Add(productionTesting);
                            vfi.SaveChanges();
                        }
                        insert.ProductionTestingId = productionTesting.ProductionTestingId;
                    }
                    var detail = new ProductionTestingDetail { 
                        Idx = insert.Idx,
                        ProductionTestingId = insert.ProductionTestingId,
                        TestingCode = insert.TestingCode,
                        TestingName = insert.TestingName,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        MinNumber = insert.MinNumber,
                        MaxNumber = insert.MaxNumber,
                        TestRate = insert.TestRate,
                        Active = true,
                    };
                    vfi.ProductionTestingDetails.Add(detail);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionTestingDetail", ex.Message);
            }

            return View(new GridModel(GetProductionTestingDetailsById(0, "", productId, warehouseId)));
        }
        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionTestingDetail(ProductionTestingDetailModel update, int productId, int warehouseId) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var detail = vfi.ProductionTestingDetails.FirstOrDefault(x => x.DetailId == update.DetailId);
                    if (detail == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết.");
                    }
                    detail.Active = update.Active;
                    detail.ModifiedUser = HttpContext.User.Identity.Name;
                    detail.ModifiedDate = DateTime.Now;
                    detail.Idx = update.Idx;
                    detail.TestingCode = update.TestingCode;
                    detail.TestingName = update.TestingName;
                    detail.MinNumber = update.MinNumber;
                    detail.MaxNumber = update.MaxNumber;
                    detail.TestRate = update.TestRate;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionTestingDetail", ex.Message);
            }

            return View(new GridModel(GetProductionTestingDetailsById(0, "", productId, warehouseId)));
        }

        public List<RealTestingModel> GetActiveTestingDetails(int productId, int warehouseId) {
            var model = new List<RealTestingModel>();
            if (productId == 0) return model;
            using (var vfi = new tammaContext()) {
                var list = GetProductionTestingDetailsById(0, "", productId, warehouseId);
                model = (from x in list
                         where x.Active
                         select new RealTestingModel {
                             ReferenceTestingDetailId = x.DetailId,
                             Idx = x.Idx,
                             TestCode = x.TestingCode,
                             TestName = x.TestingName,
                             TestNumber = 0,
                             MinNumber = x.MinNumber,
                             MaxNumber = x.MaxNumber,
                             TestRate = x.TestRate,
                             MachineName = "",
                             MachineId = 0,
                             TypeIds = x.TypeIds,
                             MachineTypeName = x.MachineTypeName,
                             TypeIdsStr = MyUtilities.Function.IdsToString(x.TypeIds)
                         }).ToList();
            }
            return model;
        }



        [GridAction]
        public ActionResult SelectProductionTestingMachine(int testingDetailId) {
            var model = new List<ProductionTestingMachineModel>();
            try {
                model = GetProductionTestingMachinesById(testingDetailId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionTestingMachine", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionTestingMachineModel> GetProductionTestingMachinesById(int testingDetailId) {
            var model = new List<ProductionTestingMachineModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.ProductionTestingMachines
                        where x.TestingDetailId == testingDetailId
                        select new ProductionTestingMachineModel {
                            TestingToolId = x.TestingToolId,
                            TestingDetailId = testingDetailId,
                            Idx = x.Idx,
                            Active = x.Active,
                            ModifiedDate = x.ModifiedDate,
                            ModifiedUser = x.ModifiedUser,
                            MachineTypeId = x.MachineTypeId,
                            MachineTypeName = x.ProcessingType.TypeName,
                        }).ToList();
            }

            return model.OrderByDescending(x => x.Active).ThenBy(m => m.Idx).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionTestingMachine(ProductionTestingMachineModel insert, int testingDetailId) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var machineTypeId = 0;
                    try { machineTypeId = Convert.ToInt32(insert.MachineTypeName); }
                    catch (FormatException) { }
                    if (machineTypeId == 0) {
                        throw new AggregateException("Lỗi! Không tìm thấy dụng cụ");
                    }
                    var entity = new ProductionTestingMachine { 
                        Idx = insert.Idx,
                        Active = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        TestingDetailId = testingDetailId,
                        MachineTypeId = machineTypeId,
                    };
                    vfi.ProductionTestingMachines.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionTestingMachine", ex.Message);
            }
            return View(new GridModel(GetProductionTestingMachinesById(testingDetailId)));
        }
        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionTestingMachine(ProductionTestingMachineModel update, int testingDetailId) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var entity = vfi.ProductionTestingMachines.FirstOrDefault(x => x.TestingToolId == update.TestingToolId);
                    if (entity == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy dụng cụ đo");
                    }
                    entity.Idx = update.Idx;
                    entity.Active = update.Active;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    var machineTypeId = 0;
                    try { machineTypeId = Convert.ToInt32(update.MachineTypeName); }
                    catch (FormatException) { }
                    if (machineTypeId != 0) {
                        entity.MachineTypeId = machineTypeId;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionTestingNote", ex.Message);
            }

            return View(new GridModel(GetProductionTestingMachinesById(testingDetailId)));
        }


        [GridAction]
        public ActionResult SelectProductionTestingProduction1(int productId, int warehouseId) {
            var model = new List<RealTestingModel>();
            try {
                if (productId != 0) {
                    model = GetActiveTestingDetails(productId, warehouseId);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionTestingProduction1", ex.Message);
            }

            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SaveProductionTestingProduction1(
            [Bind(Prefix = "inserted")] IEnumerable<RealTestingModel> inserteds,
            [Bind(Prefix = "updated")] IEnumerable<RealTestingModel> updateds,
            [Bind(Prefix = "deleted")] IEnumerable<RealTestingModel> deleteds,
            int machineId, int productId, int warehouseId, int forWarehouseId, int employeeId, string datetime) {
            try {
                var save = SaveProductionTesting(updateds.ToList(), warehouseId, machineId, productId, forWarehouseId, employeeId, datetime);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SaveProductionTestingProduction1", ex.Message);
            }

            return View(new GridModel(GetActiveTestingDetails(productId, warehouseId)));
        }
        public bool SaveProductionTesting(List<RealTestingModel> model, 
            int warehouseId, int machineId, int productId,
            int forWarehouseId, int employeeId, string datetime) {
            using (var vfi = new tammaContext()) {
                var machine = vfi.Machines.FirstOrDefault(x=> x.MachineId == machineId);
                if (machine == null) {
                    throw new AggregateException("Lỗi! Không tìm thấy máy");
                }
                if (warehouseId == 0) {
                    warehouseId = machine.ProcessingType.ForWarehouseId ?? 0;
                }
                if (warehouseId == 0) {
                    throw new AggregateException("Lỗi! Chưa thiết lập công đoạn cho máy");
                }
                var inputDate = MyUtilities.Function.ParseDateTime(datetime);
                foreach (var testing in model) {
                    var entity = new RealTesting {
                        Idx = testing.Idx,
                        ProductId = productId,
                        WarehouseId = warehouseId,
                        FromWarehouseId = forWarehouseId,
                        ProductionMachineId = machineId,
                        
                        //MachineTypeId = testing.MachineTypeId,
                        TestEmployeeId = employeeId,
                        ReferenceTestingDetailId = testing.ReferenceTestingDetailId,
                        TestCode = testing.TestCode,
                        TestName = testing.TestName,
                        TestNumber = testing.TestNumber,
                        Active = true,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        MinNumber = testing.MinNumber,
                        MaxNumber = testing.MaxNumber,
                        TestDate = inputDate,
                        ProductionDate = inputDate,
                        
                        //MachineId = testing.MachineId
                    };
                    if (forWarehouseId == 0) { 
                        //var machine = 
                    }
                    if (testing.MachineId != 0) { entity.MachineId = testing.MachineId; }
                    //var machineId = 0;
                    //try { machineId = Convert.ToInt32(testing.MachineId }
                    vfi.RealTestings.Add(entity);
                }
                vfi.SaveChanges();
                return true;
            }
            //return false;
        }
        #endregion

        #region production work order

        [GridAction]
        public ActionResult SelectProductionWorkOrder(int productId) {
            var model = new List<ProductionWorkOrderModel>();
            try {
                model = GetProductionWorkOrder(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionWorkOrder", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionWorkOrderModel> GetProductionWorkOrder(int productId) {
            var model = new List<ProductionWorkOrderModel>();
            var packingProductivity = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.PackingProductivity);
            using (var vfi = new tammaContext()) {
                var product = vfi.Products.FirstOrDefault(x => x.ProductId == productId);
                if (product == null) { throw new AggregateException("Lỗi! Không tìm thấy sản phẩm"); }
                var entity = new ProductionWorkOrderModel {
                    ProductId = productId,
                    MaxQuantityInTray = product.MaxQuantityInTray,
                    ProductionWeight = product.ProductionWeight ?? 0,
                    ProductionRate = product.ProductionRate ?? 1,
                    Productivity = product.Productivity ?? 1,
                    Productivity2 = product.ProductionSections.Where(x => x.Active && x.IsMainProcess).Sum(x => x.Productivity),
                    ProductivityQC = product.QcProductivity,
                    ProductivityPacking = packingProductivity,
                    ProductionLossRate = product.ProductionLossRate ?? 0,
                };

                model.Add(entity);
            }
            return model;

        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionWorkOrder(ProductionWorkOrderModel update) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(x => x.ProductId == update.ProductId);
                    if (product == null) { }
                    //product.ProductionWeight = update.ProductionWeight;
                    //product.Productivity = update.Productivity;
                    product.QcProductivity = update.ProductivityQC;
                    //product.ProductionRate =  update.ProductionRate;
                    product.MaxQuantityInTray = update.MaxQuantityInTray;
                    //product.MaxQuantityInTrayRunTime = update.MaxQuantityInTrayRunTime;
                    var packingProductivity = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.PackingProductivity);
                    product.MaxQuantityInTrayRunTime = CalculateProductionWorkOrderRunTime(product, packingProductivity);
                    product.ProductionLossRate = update.ProductionLossRate;
                    vfi.SaveChanges();
                }
                
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionWorkOrder", ex.Message);
            }
            return View(new GridModel(GetProductionWorkOrder(update.ProductId)));
        }

        public int CalculateProductionWorkOrderRunTime(Product product,double packingProductivity) {
            var result = 0;
            using (var vfi = new tammaContext()) {
                if (product == null) return result;
                var time = 0.0;
                if (product.Productivity > 0) {
                    time += product.Productivity.Value * product.MaxQuantityInTray;
                }
                if (product.ProductionSections.Any(x => x.Active)
                    && product.ProductionProcesses.Any(x => x.Warehouse.IsProduction2 && x.IsNecessary && x.IsAlert)) {
                    time += product.ProductionSections.Where(x => x.Active).Sum(x => x.Productivity) * product.MaxQuantityInTray;
                }
                if (product.QcProductivity > 0) {
                    time += product.QcProductivity * product.MaxQuantityInTray;
                }
                if (packingProductivity > 0) {
                    time += packingProductivity * product.MaxQuantityInTray;
                }
                result = MyUtilities.Function.RoundUp(time / 3600);
            }
            return result;
        }

        #endregion

        #region production ProductivityQuote

        [GridAction]
        public ActionResult SelectProductionProductivityQuote(int productId, bool isQuote = false) {
            var model = new List<ProductionProductivityQuoteBaseModel>();
            try {
                model = GetProductionProductivityQuote(productId, isQuote);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionProductivityQuote", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionProductivityQuoteBaseModel> GetProductionProductivityQuote(int productId, bool isQuote) {
            var model = new List<ProductionProductivityQuoteBaseModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.ProductionProductivityQuoteBases.Where(x => x.ProductId == productId && (isQuote == false || x.Active))
                    .Select(x => new ProductionProductivityQuoteBaseModel {
                        BaseId = x.BaseId,
                        ProductId = productId,
                        F = x.F,
                        L = x.L,
                        Name = x.Name,
                        Round = x.Round,
                        Time = x.Time,
                        ModifiedDate = x.ModifiedDate,
                        ModifiedUser = x.ModifiedUser,
                        Idx = x.Idx,
                        Active = x.Active,
                        IsCalculateLock = x.Product.IsCalculateLock ?? false
                    })
                    .ToList();
            }
            return model.OrderBy(x => x.Idx).ToList();

        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionProductivityQuote(ProductionProductivityQuoteBaseModel insert, int productId) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var quote = new ProductionProductivityQuoteBase {
                        ProductId = productId,
                        F = insert.F,
                        L = insert.L,
                        Name = insert.Name,
                        Round = insert.Round,
                        //Time = (insert.Round * insert.F) > 0 ? (insert.L * 60) / (insert.Round * insert.F) : 0,
                        Time = insert.Time,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        Idx = insert.Idx,
                        Active = true,
                    };
                    vfi.ProductionProductivityQuoteBases.Add(quote);
                    vfi.SaveChanges();
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionProductivityQuote", ex.Message);
            }
            return View(new GridModel(GetProductionProductivityQuote(productId,false)));
        }


        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionProductivityQuote(ProductionProductivityQuoteBaseModel update) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var quote = vfi.ProductionProductivityQuoteBases.FirstOrDefault(x => x.BaseId == update.BaseId);
                    if (quote == null) { throw new AggregateException("Lỗi! Không tìm thấy"); }
                    quote.F = update.F;
                    quote.L = update.L;
                    quote.Round = update.Round;
                    //quote.Time = (update.Round * update.F) > 0 ? (update.L * 60) / (update.Round * update.F) : 0;
                    quote.Time = update.Time;
                    quote. ModifiedDate = DateTime.Now;
                    quote.ModifiedUser = HttpContext.User.Identity.Name;
                    quote.Idx = update.Idx;
                    quote.Active = update.Active;
                    vfi.SaveChanges();
                    update.ProductId = quote.ProductId;
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionProductivityQuote", ex.Message);
            }
            return View(new GridModel(GetProductionProductivityQuote(update.ProductId, false)));
        }

        #endregion

        #region addition fee
        [GridAction]
        public ActionResult SelectProductAdditionFee(int productId, bool isQuote = false) {
            var model = new List<ProductAdditionFeeModel>();
            try {
                model = GetProductAdditionFee(productId, isQuote);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductAdditionFee", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductAdditionFeeModel> GetProductAdditionFee(int productId, bool isQuote) {
            var model = new List<ProductAdditionFeeModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.ProductAdditionFees.Where(x => x.ProductId == productId && (isQuote == false || x.Active))
                    .Select(x => new ProductAdditionFeeModel {
                        FeeId = x.FeeId,
                        ProductId = productId,
                        Name = x.Name,
                        Price = x.Price,
                        ModifiedDate = x.ModifiedDate,
                        ModifiedUser = x.ModifiedUser,
                        Active = x.Active,
                        IsCalculateLock = x.Product.IsCalculateLock ?? false
                    })
                    .ToList();
            }
            return model.ToList();

        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductAdditionFee(ProductAdditionFeeModel insert, int productId) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var fee = new ProductAdditionFee {
                        ProductId = productId,
                        Name = insert.Name,
                        Price = insert.Price,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        Active = true,
                    };
                    vfi.ProductAdditionFees.Add(fee);
                    vfi.SaveChanges();
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductAdditionFee", ex.Message);
            }
            return View(new GridModel(GetProductAdditionFee(productId, false)));
        }


        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductAdditionFee(ProductAdditionFeeModel update) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var fee = vfi.ProductAdditionFees.FirstOrDefault(x => x.FeeId == update.FeeId);
                    if (fee == null) {
                        throw new AggregateException("Lỗi! không tìm thấy dữ liệu");
                    }
                    fee.Name = update.Name;
                    fee.Active = update.Active;
                    fee.Price = update.Price;
                    fee.ModifiedDate = DateTime.Now;
                    fee.ModifiedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();
                    update.ProductId = fee.ProductId;
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductAdditionFee", ex.Message);
            }
            return View(new GridModel(GetProductAdditionFee(update.ProductId, false)));
        }
        #endregion

        #region production product level

        [GridAction]
        public ActionResult SelectProductionProductLevel() {
            var model = new List<ProductionProductLevelModel>();
            try {
                model = GetProductionProductLevel();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionProductLevel", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProductionProductLevelModel> GetProductionProductLevel() {
            var model = new List<ProductionProductLevelModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.ProductionProductLevels
                    .Select(x => new ProductionProductLevelModel {
                       LevelId = x.LevelId,
                       LevelName = x.LevelName,
                       Active = x.Active,
                       Factor = x.Factor,
                       ModifiedDate= x.ModifiedDate,
                       ModifiedUser = x.ModifiedUser,
                    })
                    .ToList();
            }
            return model.OrderBy(x => x.LevelName).ToList();

        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductionProductLevel(ProductionProductLevelModel insert) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var level = new ProductionProductLevel {
                        LevelName = insert.LevelName,
                        Active = true,
                        Factor = insert.Factor,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                    };
                    vfi.ProductionProductLevels.Add(level);
                    vfi.SaveChanges();
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionProductLevel", ex.Message);
            }
            return View(new GridModel(GetProductionProductLevel()));
        }


        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductionProductLevel(ProductionProductLevelModel update) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var level = vfi.ProductionProductLevels.FirstOrDefault(x => x.LevelId == update.LevelId);
                    if (level == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy");
                    }
                    level.LevelName = update.LevelName;
                    level.Factor = update.Factor;
                    level.Active = update.Active;
                    level.ModifiedDate = DateTime.Now;
                    level.ModifiedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionProductLevel", ex.Message);
            }
            return View(new GridModel(GetProductionProductLevel()));
        }

        public ActionResult SelectComboboxProductionLevel() {
            var model = new List<ProductionProductLevelModel>();
            using (var vfi = new tammaContext()) {
                model = GetProductionProductLevel().Where(x => x.Active).ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "LevelId", "LevelName")
            };
        }
        #endregion

        #region outside process

        [GridAction]
        public ActionResult SelectOutsideProcess() {
            var model = new List<OutsideProcessModel>();
            try {
                model = GetOutsideProcess();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionProductLevel", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<OutsideProcessModel> GetOutsideProcess() {
            var model = new List<OutsideProcessModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.OutsideProcesses
                    .Select(x => new OutsideProcessModel {
                        ProcessId = x.ProcessId,
                        Name = x.Name,
                        Price = x.Price,
                        Active = x.Active,
                        ModifiedDate = x.ModifiedDate,
                        ModifiedUser = x.ModifiedUser,
                    })
                    .ToList();
            }
            return model.OrderBy(x => x.Name).ToList();

        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertOutsideProcess(OutsideProcessModel insert) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var entity = new OutsideProcess {
                        Name = insert.Name,
                        Price = insert.Price,
                        Active = true,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                    };
                    vfi.OutsideProcesses.Add(entity);
                    vfi.SaveChanges();
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertOutsideProcess", ex.Message);
            }
            return View(new GridModel(GetOutsideProcess()));
        }


        [HttpPost]
        [GridAction]
        public ActionResult UpdateOutsideProcess(OutsideProcessModel update) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var entity = vfi.OutsideProcesses.FirstOrDefault(x => x.ProcessId == update.ProcessId);
                    if (entity == null) { throw new AggregateException("Lỗi! Không tìm thấy gia công ngoài"); }
                    entity.Name = update.Name;
                    entity.Price = update.Price;
                    entity.Active = update.Active;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateOutsideProcess", ex.Message);
            }
            return View(new GridModel(GetOutsideProcess()));
        }

        public ActionResult SelectComboboxOutsideProcess() {
            var model = new List<OutsideProcessModel>();
            using (var vfi = new tammaContext()) {
                model = GetOutsideProcess().Where(x => x.Active).ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "ProcessId", "Name")
            };
        }
        #endregion

        #region history management

        [GridAction]
        public ActionResult SelectProductHistory(int customerId, int productId, string productCode) {
            var model = new List<ProductHistoryModel>();
            try {
                model = GetProductHistoryByProductId(customerId, productId, productCode);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductHistory", ex.Message);
            }
            return View(new GridModel(model));
        }
        List<ProductHistoryModel> GetProductHistoryByProductId(int customerId, int productId, string productCode) {
            var model = new List<ProductHistoryModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.ProductHistories
                         where (productId == 0 || x.ProductId == productId) &&
                                 (customerId == 0 || x.Product.CustomerId == customerId)
                         select new ProductHistoryModel {
                             HistoryId = x.HistoryId,
                             ProductId = x.ProductId,
                             HistoryDate = x.HistoryDate,
                             CodeNumber = x.CodeNumber,
                             Before = x.Before,
                             After = x.After,
                             ModifiedDate = x.ModifiedDate,
                             ModifiedUser = x.ModifiedUser,
                             RefImage = x.RefImage,
                             //UploadDate = x.ModifiedDate.ToString("yyyyMMddhhmmss"),
                         }).ToList();
            }
            return model.OrderBy(x => x.CodeNumber).ToList();
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertProductHistory(ProductHistoryModel insert, int productId) {
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

                    var entity = new ProductHistory {
                        HistoryId = insert.HistoryId,
                        HistoryDate = insert.HistoryDate.Value,
                        CodeNumber = insert.CodeNumber,
                        Before = insert.Before,
                        After = insert.After,
                        ProductId = productId,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name
                    };
                    if (!string.IsNullOrWhiteSpace(insert.RefImage))
                        entity.RefImage = insert.RefImage;
                    else
                        entity.RefImage = "askquestion.jpg";

                    vfi.ProductHistories.Add(entity);
                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductHistory", ex.Message);
            }

            return View(new GridModel(GetProductHistoryByProductId(0, productId, "")));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProductHistory(ProductHistoryModel update, int productId) {
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
                    var entity = vfi.ProductHistories.FirstOrDefault(pt => pt.HistoryId == update.HistoryId);
                    if (entity == null)
                        throw new AggregateException("Lỗi! Không tìm thấy công cụ trong sản phấm! Liên hệ admin");
                    entity.CodeNumber = update.CodeNumber;
                    entity.HistoryDate = update.HistoryDate.Value;
                    entity.Before = update.Before;
                    entity.After = update.After;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;

                    if (!string.IsNullOrWhiteSpace(update.RefImage))
                        entity.RefImage = update.RefImage;

                    vfi.SaveChanges();
                }
                MyUtilities.Product.UpdateProductDesign(productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductHistory", ex.Message);
            }

            return View(new GridModel(GetProductHistoryByProductId(0, productId, "")));
        }

        public ActionResult CheckUploadHistoryImage(string upload) {
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
                }
            }
            catch (Exception ex) {
                return Json("0! " + ex.Message);
            }
            return Json("");
        }


        [HttpPost]
        public ActionResult SaveProductHistoryImage(IEnumerable<HttpPostedFileBase> HistoryImage) {
            // The Name of the Upload component is "attachments"       
            try {
                var attachments = new List<HttpPostedFileBase>();
                if (HistoryImage != null && HistoryImage.Any()) {
                    attachments.AddRange(HistoryImage.ToList());
                }
                if (attachments.Any()) {
                    foreach (var file in attachments) {
                        // Some browsers send file names with full path. We only care about the file name.
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName == null) continue;
                        var destinationPath = Path.Combine(Server.MapPath("~/Content/FileUpload/ProductHistory"), fileName);
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


        #endregion

        [HttpPost]
        public ActionResult PrintProductionForm(int productId) {
            try {
                return PartialView("PageProductionForm", null);
            }
            catch (Exception exception) {
                throw new Exception(exception.Message);
            }
        }
    }
}
