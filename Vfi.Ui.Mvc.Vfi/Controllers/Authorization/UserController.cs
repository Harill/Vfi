using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Utilities;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
using Vfi.Client.Module.Authentication.Interfaces;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using UserModel = Vfi.Server.Core.DataModel.Models.System.UserModel;
using ChangePasswordModel = Vfi.Server.Core.DataModel.Models.System.ChangePasswordModel;
using FunctionModel = Vfi.Server.Core.DataModel.Models.System.FunctionModel;
//using WorkGroup = Vfi.Server.Core.DataModel.BaseEntities.WorkGroup;
using Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Controllers;
using System.Threading;
//using Vfi.Server.Core.DataModel.Models.System;

namespace Vfi.Ui.Mvc.Vfi.Controllers.Authorization {
    [Authorize]
    public class UserController : Controller {
        private readonly IFormAuthenticationService _formAuthenticationService;
        private readonly IUserService _userService;
        private readonly ProductionController _productionController;
        private readonly IUnitOfWork _unitOfWork;
        [InjectionConstructor]
        public UserController(IFormAuthenticationService formAuthenticationService,
                                IUserService userService,
                                IFunctionService functionService,
                                IUnitOfWork unitOfWork,
                                ProductionController productionController) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            if (userService == null) throw new ArgumentNullException("userService");
            if (formAuthenticationService == null) throw new ArgumentNullException("formAuthenticationService");

            _unitOfWork = unitOfWork;
            _userService = userService;
            _formAuthenticationService = formAuthenticationService;
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
        // View
        [Authentication]
        public ActionResult UserAccount() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        //[Authentication]
        public ActionResult UserFunction() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        //
        public ActionResult WarehousePermission() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        //
        public ActionResult WorkGroupManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        [Authentication]
        public ActionResult ChangePassword() {
            ViewData = GetPageConfigData();
            return View(new ChangePasswordModel());
        }
        // 
        [Authentication]
        public ActionResult EditProfile() {
            ViewData = GetPageConfigData();
            return View();
        }

        [Authentication]
        public ActionResult ThinhMapDoor() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        [Authentication]
        public ActionResult AssignSignature() {
            ViewData = GetPageConfigData();
            return View();
        }

        [Authentication]
        public ActionResult WarehouseRotate() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        [Authentication]
        public ActionResult DbSystemMonitor() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        [Authentication]
        public ActionResult ItemCard() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        [Authentication]
        public ActionResult ProductionMonitor() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        [Authentication]
        public ActionResult TestingFuelInv() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        [Authentication]
        public ActionResult TestingMaterialInv() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        [Authentication]
        public ActionResult TestingMaterialInvOnMachine() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        [Authentication]
        public ActionResult TestingProductInv() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        [Authentication]
        public ActionResult TestingToolInv() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        #endregion

        #region User
        
        // Get Data
        private IEnumerable<UserModel> GetAllUsers() {
            var userAccounts = _userService.GetUsers().OrderBy(u => u.Username);

            var listUserAccountModels = userAccounts.Select
            (user => new UserModel {
                UserId = user.UserId,
                Username = user.Username.ToString(),
                Password = user.Password,
                Active = user.Active != null && user.Active.Value,
                Email = user.Email,
                FullName = user.FullName,
                //ModifiedDate = user.ModifiedDate
            }).ToList();

            return listUserAccountModels;
        }

        private List<UserModel> GetUsersInfo() {
            var model = new List<UserModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.Users
                        where x.Active.Value
                        orderby x.Username
                        select new UserModel { 
                            UserId =  x.UserId,
                            Username = x.Username,
                            Email = x.Email,
                            FullName = x.FullName
                        }).ToList();
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectUser() {
            return View(new GridModel(GetAllUsers().Where(f => !string.Equals("thangle", f.Username)).OrderBy(x=> x.Active)));
        }

        [GridAction]
        public ActionResult SelectActiveUser() {
            return View(new GridModel(GetUsersInfo()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertUser() {
            //Create a new instance of the userModel class.
            var userModel = new UserModel();

            //Perform model binding (fill the user properties and validate it).
            if (TryUpdateModel(userModel)) {
                var result = _userService.GetUserByUsername(userModel.Username);
                // check if exist user account
                if (result != null)
                    ModelState.AddModelError("Username", "Exist Username");
                else if (ModelState.IsValid) {
                    //The model is valid - insert the user.
                    var r = _userService.CreateNewUser(userModel);
                    _unitOfWork.SaveChanges();
                }
            }
            else if (ModelState.IsValid == false) {
                ModelState.AddModelError("Username", "Error");
            }

            //Rebind the grid
            return View(new GridModel(GetAllUsers().Where(f => !string.Equals("thangle", f.Username))));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateUser(int userId) {
            var user = _userService.GetUserById(userId);

            var userModel = new UserModel { UserId = user.UserId };

            if (TryUpdateModel(userModel)) {
                if (ModelState.IsValid) {
                    //The model is valid - edit the user.
                    _userService.SaveUser(userModel);
                }
            }
            else if (ModelState.IsValid == false) {
                ModelState.AddModelError("Username", "Error");
            }

            return View(new GridModel(GetAllUsers().Where(f => !string.Equals("thangle", f.Username))));
        }

        [HttpPost]
        [GridAction]
        public ActionResult DeleteUser(int userId) {
            var user = _userService.GetUserById(userId);

            try {
                _userService.DeleteUser(userId);
            }
            catch (Exception) {
                throw;
            }

            //Rebind the grid
            return View(new GridModel(GetAllUsers().Where(f => !string.Equals("thangle", f.Username))));
        }

        public ActionResult SelectComboBoxSalesUser() {

            using (var vfi = new tammaContext()) {
                return new JsonResult {
                    Data = new SelectList(
                        vfi.Users.Where(e => e.Active == true).OrderBy(e => e.Username).ToList(),
                        "UserId",
                        "Username"),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        #endregion

        #region Profile
        [GridAction]
        public ActionResult SelectProfile() {
            var username = HttpContext.User.Identity.Name;
            return View(new GridModel(GetAllUsers().Where(f => string.Equals(username, f.Username))));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateProfile(string username) {
            //var username = HttpContext.User.Identity.Name;
            return View(new GridModel(GetAllUsers().Where(f => string.Equals(username, f.Username))));
        }
        #endregion

        #region Change Password
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel) {
            var msg = "";
            if (ModelState.IsValid) {
                var username = HttpContext.User.Identity.Name;
                var result = _userService.ChangePassword(username, changePasswordModel.OldPassword, changePasswordModel.NewPassword);

                switch (result) {
                    case 0:
                        //msg = "Mật khẩu đã được thay đỗi";
                        ModelState.AddModelError("ConfirmPassword",
                                                 _unitOfWork.SaveChanges() > 0 ? msg = "Mật khẩu đã được thay đổi" : msg = "Không thể đổi password. Có lỗi bất ngờ.");
                        break;
                    case 1:
                        msg = "Tài khoản chưa được kích hoạt hoặc không tồn tại.";
                        ModelState.AddModelError("ConfirmPassword", "Tài khoản chưa được kích hoạt hoặc không tồn tại.");
                        break;
                    case 2:
                        msg = "Tên đăng nhập hoặc mật khẩu không đúng.";
                        ModelState.AddModelError("ConfirmPassword", "Tên đăng nhập hoặc mật khẩu không đúng.");
                        break;
                    case 3:
                        msg = "Have prolem with network connection.!";
                        ModelState.AddModelError("ConfirmPassword", "Have prolem with network connection. (connection)!");
                        break;
                }

                return Json(msg);
            }
            msg = "Giá trị không hợp lệ. (InValid)";
            ModelState.AddModelError("ConfirmPassword", "Giá trị không hợp lệ.");
            return Json(msg);

        }
        #endregion

        #region UserWorkGroup

        [HttpPost]
        public ActionResult SelectComboBoxTelerikTheme() {
            var lst = new List<string> {"black", "common", "default",
                "forest", "hay", "metro", 
                "office2007", "office2010black", "office2010blue", "office2010silver", 
                "outlook", "rtl", "simple", "sitefinity", "sunset", 
                "telerik", "transparent", "vista", 
                "web20", "webblue", "windows7" };
            return new JsonResult { Data = lst };
        }

        [GridAction]
        public ActionResult SelectWorkGroup() {
            var model = new List<WorkGroupModel>();
            try {
                model = GetWorkGroups();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectWorkGroup", ex.Message);
            }
            return View(new GridModel(model));
        }

        public List<WorkGroupModel> GetWorkGroups() {
            var model = new List<WorkGroupModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.WorkGroups
                        select new WorkGroupModel { 
                            WorkGroupId = x.WorkGroupId,
                            WorkGroupCode = x.WorkGroupCode,
                            WorkGroupName = x.WorkGroupName,
                            Theme = x.Theme,
                            Active = x.Active,
                            Description = x.Description,
                            ModifiedDate = x.ModifiedDate,
                        }).ToList();
            }

            return model;
        }


        [HttpPost]
        [GridAction]
        public ActionResult InsertWorkGroup(WorkGroupModel inserted) {
            try {
                using (var vfi = new tammaContext()) {
                    if (string.IsNullOrWhiteSpace(inserted.WorkGroupCode)) {
                        throw new AggregateException("Lỗi! Mã không được để trống");
                    }
                    else { inserted.WorkGroupCode = inserted.WorkGroupCode.Trim(); }
                    var workgroup = vfi.WorkGroups.FirstOrDefault(x=> x.WorkGroupCode.Equals(inserted.WorkGroupCode));
                    if (workgroup != null) {
                        throw new AggregateException("Lỗi! Bị trùng mã");
                    }
                    workgroup = new WorkGroup { 
                        WorkGroupCode = inserted.WorkGroupCode,
                        WorkGroupName = inserted.WorkGroupName,
                        Description = inserted.Description,
                        ModifiedDate = DateTime.Now,
                        Active = true,
                        
                    };
                    vfi.WorkGroups.Add(workgroup);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertWorkGroup", ex.Message);
            }
            return View(new GridModel(GetWorkGroups()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateWorkGroup(WorkGroupModel updated) {
            try {
                using (var vfi = new tammaContext()) {
                    if (string.IsNullOrWhiteSpace(updated.WorkGroupCode)) {
                        throw new AggregateException("Lỗi! Mã không được để trống");
                    }
                    else { updated.WorkGroupCode = updated.WorkGroupCode.Trim(); }
                    var workgroup = vfi.WorkGroups.FirstOrDefault(x => x.WorkGroupCode.Equals(updated.WorkGroupCode) && x.WorkGroupId != updated.WorkGroupId);
                    if (workgroup != null) {
                        throw new AggregateException("Lỗi! Bị trùng mã");
                    }
                    workgroup = vfi.WorkGroups.FirstOrDefault(x => x.WorkGroupId == updated.WorkGroupId);
                    if (workgroup == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy data");
                    }
                    workgroup.WorkGroupCode = updated.WorkGroupCode;
                    workgroup.WorkGroupName = updated.WorkGroupName;
                    workgroup.Theme = updated.Theme;
                    workgroup.Active = updated.Active;
                    workgroup.Description = updated.Description;
                    workgroup.ModifiedDate = DateTime.Now;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateWorkGroup", ex.Message);
            }
            return View(new GridModel(GetWorkGroups()));
        }


        public ActionResult DisplayCheckedUser(int[] checkedRecords) {
            checkedRecords = checkedRecords ?? new int[] { };

            var userModels = GetAllUsers().Where(u => checkedRecords.Contains(u.UserId));
            //var userModels = GetAllUsers();
            //return View("UserChecked", new GridModel(userModels));
            return PartialView("UserChecked", userModels);
        }

        [HttpPost]
        public ActionResult AddUserToWorkGroup(string strWorkGroupId, string checkedRecords) {
            var lstUserIds = checkedRecords.Split(':');

            //for (var i = 0; i < lstUserIds.Length; i++) {
            //    var userWorkGroupModel = new UserWorkGroupModel {
            //        User = new User { UserId = Convert.ToInt32(lstUserIds[i]) },
            //        WorkGroup = new WorkGroup { WorkGroupId = Convert.ToInt32(strWorkGroupId) }
            //    };
            //}

            return new JsonResult { Data = lstUserIds };
        }

        #endregion

        #region Assign Signature

        public ActionResult FuelSignature(long transactionId, int type) {
            using (var vfi = new tammaContext()) {
                var fuel = vfi.TransactionFpts.FirstOrDefault(t => t.TransactionId == transactionId);
                if (fuel != null)
                    switch (type) {
                        case 1:
                            fuel.PurchasingSignature = 1;
                            break;
                        case 2:
                            fuel.InventorySignature = 1;
                            break;
                        case 3:
                            fuel.AccountantSignature = 1;
                            break;
                        case 4:
                            fuel.QcSignature = 1;
                            break;
                    }
                vfi.SaveChanges();
            }
            return null;
        }

        #endregion

        #region Function

        public ActionResult DisplayCheckedUserForFunction(int userId) {
            try {
                //var functions = _function/Service.GetAllFunction().Where(f => f.Active == true).OrderBy(f => f.IDx);

                //var permissions = _permissionService.GetPermissionPerUser(username);

                var model = new List<FunctionModel>();
                using (var vfi = new tammaContext()) {
                    model.AddRange(vfi.Functions.Where(x => x.Active).Select(x => new FunctionModel {
                        FunctionId = x.FunctionId,
                        FunctionCode = x.FunctionCode,
                        FunctionName = x.FunctionName,
                        Description = x.Description,
                        Active = x.Active,
                        ModifiedDate = x.ModifiedDate,
                        IsUserFunction = x.Permissions.Any(f => f.User.UserId == userId)
                    }));
                }

                return PartialView("FunctionPerUserChecked", model);
            }
            catch (Exception) {
                return null;
            }
        }

        public ActionResult DisplayCheckedUserForWarehousePermission(int userId) {
            var wPermissionsModels = new List<WarehousePermissionNewModel>();
            try {
                using (var vfi = new tammaContext()) {


                    //var permissions = _permissionService.GetPermissionPerUser(username);

                    //var wPermissions = vfi.WarehousePermissions.Where(wp => wp.UserId == userId).Select(x => new { x.WarehouseId, x.Import, x.Rotate, x.OrderProgress }).ToList();
                    //var warehouses = vfi.Warehouses.Where(w => w.Active).OrderBy(w => w.Idx).Select(x => new { x.WarehouseId, x.WarehouseName, x.IsMainProcess }).ToList();
                    var warehouses = vfi.Warehouses.Where(w => w.Active).OrderBy(w => w.Idx).ToList();
                    var wPermissions = vfi.WarehousePermissions.Where(wp => wp.UserId == userId).ToList();

                    foreach (var w in warehouses) {
                        var model = new WarehousePermissionNewModel {
                            WarehouseId = w.WarehouseId,
                            UserId = userId,
                            WarehouseName = w.WarehouseName,
                            Import = false,
                            ImportReadOnly = false,
                            Rotate = false,
                            OrderProgress = false,
                            MainProgress = w.IsMainProcess
                        };
                        var wPermission = wPermissions.FirstOrDefault(x => x.WarehouseId == model.WarehouseId);
                        if (wPermission != null) {
                            model.Import = wPermission.Import ?? false;
                            model.Rotate = wPermission.Rotate ?? false;
                            model.OrderProgress = wPermission.OrderProgress ?? false;
                        }
                        wPermissionsModels.Add(model);
                    }
                }
            }
            catch (Exception ex) {
                return PartialView("WarehousePerUserChecked", ex.Message);
            }
            return PartialView("WarehousePerUserChecked", wPermissionsModels);
        }


        public ActionResult DisplayWarehouseRotate(int warehouseId) {
            try {
                using (var vfi = new tammaContext()) {

                    var warehouses = vfi.Warehouses.Where(w => w.Active && w.WarehouseId != warehouseId)
                                        .OrderBy(w => w.Idx)
                                        .ToList();

                    var model = new List<WarehousePermissionNewModel>();

                    foreach (var w in warehouses) {
                        var entity = new WarehousePermissionNewModel {
                            WarehouseId = w.WarehouseId,
                            WarehouseName = w.WarehouseName,
                            Import = false,
                            ImportReadOnly = true,
                            Rotate = false,
                        };
                        var warehouseImport =
                            vfi.WarehouseRotates.FirstOrDefault(
                                wr => wr.WarehouseId == w.WarehouseId && wr.ToWarehouseId == warehouseId && wr.Active);
                        if (warehouseImport != null) {
                            entity.Import = true;
                        }
                        var warehouseExport =
                            vfi.WarehouseRotates.FirstOrDefault(
                                wr => wr.WarehouseId == warehouseId && wr.ToWarehouseId == w.WarehouseId && wr.Active);
                        if (warehouseExport != null) {
                            entity.Rotate = true;
                        }
                        model.Add(entity);
                    }

                    return PartialView("WarehousePerUserChecked", model);
                }
            }
            catch (Exception) {
                return null;
            }

        }

        #endregion

        #region ThinhMapDoor

        [HttpPost]
        public ActionResult CreateVitureInvoice(string orderNumber) {
            try {
                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    //(2) tao invoice ao tu hoa con con thieu
                    // tao invoice
                    var order = vfi.Orders.FirstOrDefault(o => o.OrderNumber.Equals(orderNumber));

                    if (order == null)
                        return Json(9);
                    if (order.DueDate == null)
                        return Json(8);
                    var exportTP = new ExportFormTP_KD {
                        CustomerId = order.CustomerId,
                        DateCreate = order.DueDate,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        OrderId = order.OrderId,
                        TotalBox = 1,
                        Transporter = "",
                        CompanyTransporter = "",
                        CreatedInvoice = true,
                        CarNumber = "",
                        DateTransporter = order.DueDate,
                        TransactionCode = "",
                    };
                    var orderDetail = vfi.OrderDetails.Where(od => od.OrderId == order.OrderId);
                    foreach (var detail in orderDetail) {
                        if (detail.RequiedNumber > 0) {
                            var exportDetail = new ExportFormTP_KDDetail {
                                ExportId = exportTP.ExportId,
                                ProductId = detail.ProductId,
                                Quality = detail.RequiedNumber,
                                Weight = 0.0,
                                OrderDetailId = detail.OrderDetailId,
                                OrderDetail = detail,
                            };
                            exportTP.ExportFormTP_KDDetail.Add(exportDetail);
                        }
                        detail.RequiedNumber = 0;
                    }
                    if (exportTP.ExportFormTP_KDDetail.Any()) {
                        vfi.ExportFormTP_KD.Add(exportTP);
                        //vfi.ExportFormTP_KDDetail.AddRange(exportTP.ExportFormTP_KDDetail);
                        vfi.SaveChanges();
                        var invoice = new Invoice {
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            OrderId = order.OrderId,
                            ShipmentDate = order.DueDate,
                            CustomerId = order.CustomerId,
                            Active = true,
                            TaxPercent = 0,
                            InvoiceNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Invoice, 1),
                            ExportId = exportTP.ExportId,
                            Note = "T",
                        };
                        vfi.Invoices.Add(invoice);
                    }
                    vfi.SaveChanges();
                    return Json(1);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult CleanDatabaseHardCode() {
            return Json(0);
            var saved = 0;
            try {
                using (var vfi = new tammaContext()) {
                    if (!User.Identity.Name.Equals("admin")) return Json(0);
                    var customers = vfi.Customers.Where(x => !x.CustomerCode.Contains("D2")); // except specific customer
                    var customerIds = customers.Select(x => x.CustomerId).ToList();

                    //var productIds = new List<int> { 182, 579, 1881 }; // except specific product
                    var products = vfi.Products.Where(x => customerIds.Contains(x.CustomerId) 
                        //&& !productIds.Contains(x.ProductId)
                        );
                    var productIds = products.Select(x => x.ProductId).ToList();

                    vfi.ProductionProcesses.RemoveRange(vfi.ProductionProcesses.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionMaterials.RemoveRange(vfi.ProductionMaterials.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionTestingMachines.RemoveRange(vfi.ProductionTestingMachines.Where(x => productIds.Contains(x.ProductionTestingDetail.ProductionTesting.ProductId)));
                    saved += vfi.SaveChanges(); 
                    vfi.ProductionTestingDetails.RemoveRange(vfi.ProductionTestingDetails.Where(x => productIds.Contains(x.ProductionTesting.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionTestings.RemoveRange(vfi.ProductionTestings.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionTools.RemoveRange(vfi.ProductionTools.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionSections.RemoveRange(vfi.ProductionSections.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionPlatings.RemoveRange(vfi.ProductionPlatings.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionFuels.RemoveRange(vfi.ProductionFuels.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionPricings.RemoveRange(vfi.ProductionPricings.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionDefects.RemoveRange(vfi.ProductionDefects.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionHeatTreatments.RemoveRange(vfi.ProductionHeatTreatments.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionPolishes.RemoveRange(vfi.ProductionPolishes.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductCombinationRecipeDetails.RemoveRange(vfi.ProductCombinationRecipeDetails.Where(x => productIds.Contains(x.ProductCombinationRecipe.ProductId)));
                    saved += vfi.SaveChanges(); 
                    vfi.ProductCombinationRecipes.RemoveRange(vfi.ProductCombinationRecipes.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.ProductionTestingNotes.RemoveRange(vfi.ProductionTestingNotes.Where(x => productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    // purchasing
                    //vfi.Warehouses.RemoveRange(vfi.Warehouses);
                    vfi.Machines.RemoveRange(vfi.Machines.Where(x => (!x.MachineName.Contains("VF2")
                        && (x.ProcessingType.Warehouse.IsProduction
                            || x.ProcessingType.Warehouse.IsCncMilling
                            || x.ProcessingType.Warehouse.IsProduction2))
                        || ( !x.Active && !x.Production2)));
                    saved += vfi.SaveChanges();
                    vfi.ProductImgs.RemoveRange(vfi.ProductImgs.Where(x=> productIds.Contains(x.ProductId)));
                    saved += vfi.SaveChanges();
                    vfi.Products.RemoveRange(products);
                    saved += vfi.SaveChanges();
                    vfi.Customers.RemoveRange(customers.Where(x => !x.Products.Any()));
                    saved += vfi.SaveChanges();
                    return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "", saved));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.Exception,
                        MyUtilities.MySystem.FetchExceptionMessage(ex),
                        saved));
            }
            //return Json(0);
        }

        [HttpPost]
        public ActionResult GenerateQuoteMaterial() {
            var saved = 0;

            try {

                using (var vfi = new tammaContext()) {
                    var materials = vfi.Materials.Select(x => new { x.MaterialTypeId, x.MaterialName, x.Shape, x.DiameterType })
                        .Distinct().ToList();
                    var quoteMaterials = vfi.MaterialQuoteBases;
                    var defaultSizes = new List<double> { 2.5, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 50.8, 110 };
                    foreach (var material in materials) {
                        var entity = quoteMaterials.FirstOrDefault(x => x.MaterialTypeId == material.MaterialTypeId
                            && x.MaterialName == material.MaterialName.Trim()
                            && x.MaterialShape == material.Shape.Trim()
                            && x.MaterialDiameterType == material.DiameterType.Trim());
                        if (entity == null) {
                            entity = new MaterialQuoteBase {
                                MaterialTypeId = material.MaterialTypeId,
                                MaterialName = material.MaterialName.Trim(),
                                MaterialShape = material.Shape.Trim(),
                                MaterialDiameterType = material.DiameterType.Trim(),
                                BasePrice = 0,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = "Auto",
                            };
                            var fromSize = 0.0;
                            foreach (var size in defaultSizes) {
                                entity.MaterialQuoteBaseDetails.Add(new MaterialQuoteBaseDetail {
                                    FromOutDiameter = fromSize,
                                    ToOutDiameter = size,
                                    Value = 0,
                                });
                                fromSize = size;
                            }
                            vfi.MaterialQuoteBases.Add(entity);
                        }
                    }
                    saved += vfi.SaveChanges();
                }


                return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.NoError,
                        "",
                        saved));
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.Exception,
                        MyUtilities.MySystem.FetchExceptionMessage(ex),
                        saved));
            }
        }

        [HttpPost]
        public ActionResult GenerateQuoteOutsideProcess() {
            var saved = 0;

            try {

                using (var vfi = new tammaContext()) {
                    var processNames = vfi.ProductionPlatings.Select(x => x.PlatingName).Distinct().ToList();
                    foreach (var name in processNames) {
                        var entity = new OutsideProcess { 
                            Name = name,
                            Active = true,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = "Auto",
                            Price = 0,
                        };
                        vfi.OutsideProcesses.Add(entity);
                    }
                    saved += vfi.SaveChanges();
                }


                return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.NoError,
                        "",
                        saved));
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int)MyUtilities.Monitor.ErrorCode.Exception,
                        MyUtilities.MySystem.FetchExceptionMessage(ex),
                        saved));
            }
        }

        [HttpPost]
        public ActionResult ReApproveOrder(string orderNumber) {
            try {
                using (var vfi = new tammaContext()) {
                    // chuyen ngay giao don hang lai null de duyet lai
                    var order = vfi.Orders.FirstOrDefault(o => o.OrderNumber.Equals(orderNumber));

                    if (order == null)
                        return Json(9);
                    if (order.DueDate == null)
                        return Json(8);
                    if (order.OrderDetails.Any(od => od.OrderQty != od.RequiedNumber))
                        return Json(7);
                    order.DueDate = null;
                    vfi.SaveChanges();
                    return Json(1);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ReTaxInvoice(string taxInvoiceNumber) {
            try {
                using (var vfi = new tammaContext()) {
                    // chuyen ngay giao don hang lai null de duyet lai
                    var taxInvoice = vfi.TaxInvoices.FirstOrDefault(o => o.TaxInvoiceList.Equals(taxInvoiceNumber));

                    if (taxInvoice == null)
                        return Json(9);
                    if (taxInvoice.TaxInvoiceDetails.Any(tid => tid.Status == (byte)MyUtilities.Transaction.Status.Approved))
                        return Json(7);
                    taxInvoice.SetupDate = null;
                    taxInvoice.Status = (byte)MyUtilities.Sales.Status.Waiting;
                    vfi.SaveChanges();
                    return Json(1);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public ActionResult ChangeCurrencyCode(string orderNumber, string currency) {
            try {
                using (var vfi = new tammaContext()) {
                    // chuyen ngay giao don hang lai null de duyet lai
                    var order = vfi.Orders.FirstOrDefault(o => o.OrderNumber.Equals(orderNumber));

                    if (order == null)
                        return Json(9);
                    order.CurrencyCode = currency;
                    vfi.SaveChanges();
                    return Json(1);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChangeProductUnitPrice(
            string orderNumber, string productCode, double? unitPrice, int? orderDetailNumber
            ) {
            // doi gia trong don hang
            try {
                using (var vfi = new tammaContext()) {
                    if (string.IsNullOrWhiteSpace(productCode))
                        return Json(8);
                    if ((unitPrice == null || unitPrice == 0) && (orderDetailNumber == null || orderDetailNumber == 0))
                        return Json(6);
                    var order = vfi.Orders.FirstOrDefault(o => o.OrderNumber.Equals(orderNumber));
                    if (order == null)
                        return Json(9);
                    var product = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(productCode));
                    if (product == null)
                        return Json(8);
                    var orderDetail =
                        vfi.OrderDetails.FirstOrDefault(
                            od => od.OrderId == order.OrderId && od.ProductId == product.ProductId);
                    if (orderDetail == null)
                        return Json(7);
                    if (unitPrice != null && unitPrice != 0) {
                        orderDetail.UnitPrice = unitPrice.Value;
                        orderDetail.UnitPriceDiscount = unitPrice.Value;
                    }
                    if (orderDetailNumber != null && orderDetailNumber != 0) {
                        if (orderDetail.OrderQty != orderDetail.RequiedNumber)
                            return Json(5);
                        orderDetail.OrderQty = orderDetailNumber;
                        orderDetail.RequiedNumber = orderDetailNumber.Value;
                    }
                    vfi.SaveChanges();
                    return Json(1);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
            //return Json(-1);
        }

        [HttpPost]
        public ActionResult ChinhSoSx1ChoNa() {

            var a = 0;
            try {
                using (var vfi = new tammaContext()) {
                    //475
                    var importSx1Sai = vfi.ImportFormSX1Detail.Where(i => i.ImportId == 473);
                    var importSx1Dung = vfi.ImportFormSX1Detail.Where(i => i.ImportId == 475);
                    foreach (var sx1Detail in importSx1Dung) {
                        var sx1DetailSai =
                            importSx1Sai.FirstOrDefault(
                                id =>
                                id.MachineId == sx1Detail.MachineId &&
                                id.MaterialInvId == sx1Detail.MaterialInvId &&
                                sx1Detail.ProductId == id.ProductId &&
                                sx1Detail.Number1 + sx1Detail.Number2 == id.Number1 + id.Number2);
                        if (sx1DetailSai == null) continue;
                        sx1Detail.MaterialUse1 = sx1DetailSai.MaterialUse1;
                        sx1Detail.MaterialUse2 = sx1DetailSai.MaterialUse2;
                    }
                    a += vfi.SaveChanges();
                }
            }
            catch (Exception) {
                return Json(-1);
            }
            return Json(a);
        }

        [HttpPost]
        public ActionResult ChinhKhoNguyenLieu2SoLe() {

            var a = 0;
            try {
                using (var vfi = new tammaContext()) {
                    foreach (var mi in vfi.MaterialInventories) {
                        mi.TotalQty = Math.Round(mi.TotalQty, 2);
                    }
                    foreach (var mim in vfi.MaterialInvOnMachines) {
                        mim.TotalQuantity = Math.Round(mim.TotalQuantity, 2);
                    }
                    a += vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                return Json(-1);
            }
            return Json(a);
        }

        [HttpPost]
        public ActionResult ChinhLechSoNguyenLieuTrongKho(string monthlyDate) {
            var a = 0;
            try {
                var ci = new CultureInfo("vi-VN");
                var date = string.IsNullOrWhiteSpace(monthlyDate)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(monthlyDate, ci);
                using (var vfi = new tammaContext()) {
                    var materialInvs = vfi.MaterialInventories;

                    var transactionImport = new Transaction {
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Material, 1),
                        EoI =
                            MyUtilities.Transaction.CastText.GetValueEoI(
                                MyUtilities.Transaction.EoIEnum.Import.ToString()),
                        MoP = true,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = date,
                        Status = (byte)MyUtilities.Transaction.Status.Approved,
                        Active = true,
                        IsApprove = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    };
                    var transactionExport = new Transaction {
                        TransactionCode = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Material, 2),
                        EoI =
                            MyUtilities.Transaction.CastText.GetValueEoI(
                                MyUtilities.Transaction.EoIEnum.Export.ToString()),
                        MoP = true,
                        CreatedUser = HttpContext.User.Identity.Name,
                        CreatedDate = date,
                        Status = (byte)MyUtilities.Transaction.Status.Approved,
                        Active = true,
                        IsApprove = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    };
                    var importPeriods = new List<MaterialInventoryPeriod>();
                    var exportPeriods = new List<MaterialInventoryPeriod>();
                    foreach (var materialInventory in materialInvs) {
                        var materialInvPeriods =
                            vfi.MaterialInventoryPeriods.Where(
                                mip => mip.MaterialInventoryId == materialInventory.MaterialInventoryId);
                        if (!materialInvPeriods.Any()) continue;
                        var periodQuantity =
                            Math.Round(
                                materialInvPeriods.Sum(mip => mip.LastPeriodQuantity - mip.EarlyPeriodQuantity), 2);
                        materialInventory.TotalQty = Math.Round(materialInventory.TotalQty, 2);
                        var difference = Math.Round(materialInventory.TotalQty - periodQuantity, 2);

                        //thuc te it hon
                        if (difference < 0) {
                            var period = new MaterialInventoryPeriod {
                                PeriodDay = date.Day,
                                PeriodMonth = date.Month,
                                PeriodYear = date.Year,
                                PeriodDate = date,
                                MaterialId = materialInventory.MaterialId,
                                MaterialInventoryId = materialInventory.MaterialInventoryId,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                TransactionId = transactionImport.TransactionId,
                                Transaction = transactionImport,
                                EarlyPeriodQuantity = periodQuantity,
                                Quantity = Math.Abs(difference),
                                LastPeriodQuantity = materialInventory.TotalQty,
                            };
                            importPeriods.Add(period);
                            var transactionDetail = new TransactionDetail {
                                ReferenceId = materialInventory.MaterialId,
                                MoP = true,
                                Quantity = Math.Abs(difference),
                                QuantityKg = 0,
                                Price = 0,
                                UnitMeasure = "",
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = "Nhập thêm",
                            };
                            transactionImport.TransactionDetails.Add(transactionDetail);
                        }
                        else if (difference > 0) {
                            var period = new MaterialInventoryPeriod {
                                PeriodDay = date.Day,
                                PeriodMonth = date.Month,
                                PeriodYear = date.Year,
                                PeriodDate = date,
                                MaterialId = materialInventory.MaterialId,
                                MaterialInventoryId = materialInventory.MaterialInventoryId,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                TransactionId = transactionImport.TransactionId,
                                Transaction = transactionImport,
                                EarlyPeriodQuantity = periodQuantity,
                                Quantity = difference,
                                LastPeriodQuantity = materialInventory.TotalQty,
                            };
                            exportPeriods.Add(period);
                            var transactionDetail = new TransactionDetail {
                                ReferenceId = materialInventory.MaterialId,
                                MoP = true,
                                Quantity = Math.Abs(difference),
                                QuantityKg = 0,
                                Price = 0,
                                UnitMeasure = "",
                                Active = true,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                Note = "Nhập thêm",
                            };
                            transactionExport.TransactionDetails.Add(transactionDetail);
                        }
                    }
                    if (transactionImport.TransactionDetails.Any()) {
                        vfi.Transactions.Add(transactionImport);
                        vfi.MaterialInventoryPeriods.AddRange(importPeriods);
                    }
                    if (transactionExport.TransactionDetails.Any()) {
                        vfi.Transactions.Add(transactionExport);
                        vfi.MaterialInventoryPeriods.AddRange(exportPeriods);
                    }
                    a += vfi.SaveChanges();
                }
            }
            catch (Exception) {
                return Json(-1);
            }
            return Json(a);

        }

        [HttpPost]
        public ActionResult ChinhLechSoNguyenLieuTrenMay(string monthlyDate) {
            try {
                using (var vfi = new tammaContext()) {
                    var ci = new CultureInfo("vi-VN");
                    var date = string.IsNullOrWhiteSpace(monthlyDate)
                                          ? DateTime.Today
                                          : Convert.ToDateTime(monthlyDate, ci);
                    var a = 0;
                    var machines = vfi.Machines;
                    foreach (var machine in machines) {
                        var materialOnMachines =
                            vfi.MaterialInvOnMachines.Where(mim => mim.MachineId == machine.MachineId);
                        if (!materialOnMachines.Any()) continue;
                        var listPeriod = new List<MaterialInvOnMachinePeriod>();
                        foreach (var materialInvOnMachine in materialOnMachines) {
                            var materialInvPeriods =
                                vfi.MaterialInvOnMachinePeriods.Where(
                                    mimp =>
                                    mimp.MaterialInvId == materialInvOnMachine.MaterialInvId &&
                                    mimp.MachineId == materialInvOnMachine.MachineId);
                            if (!materialInvPeriods.Any()) continue;
                            var periodQuantity =
                                Math.Round(
                                    materialInvPeriods.Sum(mip => mip.LastQuantity - mip.EarlyQuantity), 2);
                            materialInvOnMachine.TotalQuantity = Math.Round(materialInvOnMachine.TotalQuantity, 2);

                            var difference = Math.Round(materialInvOnMachine.TotalQuantity - periodQuantity, 2);
                            //thuc te it hon
                            if (difference < 0) {
                                var period = new MaterialInvOnMachinePeriod {
                                    EarlyQuantity = periodQuantity,
                                    Quantity = Math.Abs(difference),
                                    LastQuantity = materialInvOnMachine.TotalQuantity,
                                    MachineId = materialInvOnMachine.MachineId,
                                    MaterialInvId = materialInvOnMachine.MaterialInvId,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    Note = "",
                                    PeriodDate = date,
                                };
                                listPeriod.Add(period);
                            }
                            else if (difference > 0) {
                                var period = new MaterialInvOnMachinePeriod {
                                    EarlyQuantity = periodQuantity,
                                    Quantity = Math.Abs(difference),
                                    LastQuantity = materialInvOnMachine.TotalQuantity,
                                    MachineId = materialInvOnMachine.MachineId,
                                    MaterialInvId = materialInvOnMachine.MaterialInvId,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    Note = "",
                                    PeriodDate = date,
                                };
                                listPeriod.Add(period);
                            }
                        }
                        if (listPeriod.Any()) {
                            vfi.MaterialInvOnMachinePeriods.AddRange(listPeriod);
                        }
                        a += vfi.SaveChanges();
                    }
                    return Json(a);
                }
            }
            catch (Exception) {
                return Json(-1);
            }

        }

        [HttpPost]
        public ActionResult TaoNangSuatThucTe() {
            try {
                using (var vfi = new tammaContext()) {
                    var tracks = vfi.TrackUpMachines
                                    .Where(tm => tm.Status != (byte)MyUtilities.Transaction.Status.Cancel)
                                    .OrderBy(tm => tm.DeliveryDate);
                    var productIds = tracks.Select(tm => tm.ProductId).Distinct();
                    var model = new List<RealProduction>();
                    foreach (var productId in productIds) {
                        var machineIds = tracks.Where(tm => tm.ProductId == productId)
                                               .Select(tm => tm.MachineId)
                                               .Distinct();
                        foreach (var machineId in machineIds) {
                            var track =
                                tracks.FirstOrDefault(tm => tm.ProductId == productId && tm.MachineId == machineId);
                            var entity = new RealProduction {
                                MachineId = track.MachineId,
                                ProductId = track.ProductId,
                                //RealProductRate = Convert.ToInt32(track.RealRate),
                                //RealProductivity = track.RealProductivity,
                                //ModifiedDate = track.DeliveryDate,
                                //ModifiedUser = HttpContext.User.Identity.Name,
                            };
                            model.Add(entity);
                        }
                    }
                    vfi.RealProductions.AddRange(model);
                    var a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception) {
                return Json(-1);
            }

        }

        [HttpPost]
        public ActionResult TaoData() {
            try {
                using (var vfi = new tammaContext()) {
                    var tracks = from t in vfi.TrackUpMachines
                                 where t.StartDate == null
                                 select t;
                    foreach (var trackUpMachine in tracks) {
                        var productionMaterial =
                            vfi.ProductionMaterials.Where(pm => pm.ProductId == trackUpMachine.ProductId && pm.Active).ToList();
                        if (productionMaterial.Any()) {
                            trackUpMachine.MaterialId = productionMaterial.LastOrDefault().MaterialId;
                        }
                        trackUpMachine.StartDate = trackUpMachine.DeliveryDate.Value;
                    }
                    var i = vfi.SaveChanges();
                    return Json(i);
                }
            }
            catch (Exception) {
                return Json(-1);
            }

        }

        [HttpPost]
        public ActionResult TaoDataSpCu() {
            try {
                using (var vfi = new tammaContext()) {
                    var products = from p in vfi.Products
                                   where p.Active && p.ProductionProcesses.Count < 3
                                   select p;
                    foreach (var product in products) {
                        var processes = product.ProductionProcesses;
                        //if (processes.Count > 3) continue;
                        if (processes.FirstOrDefault(ps => ps.WarehouseId == MyUtilities.Warehouse.Production1) == null) {
                            var process = new ProductionProcess {
                                IsAlert = true,
                                IsNecessary = true,
                                ProcessIndex = 1,
                                WarehouseId = MyUtilities.Warehouse.Production1,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = "admin",
                                ProductId = product.ProductId,
                            };
                            vfi.ProductionProcesses.Add(process);
                        }
                        if (
                            processes.FirstOrDefault(
                                ps => ps.WarehouseId == MyUtilities.Warehouse.QcA || ps.WarehouseId == MyUtilities.Warehouse.QcB) ==
                            null) {
                            var process = new ProductionProcess {
                                IsAlert = true,
                                IsNecessary = true,
                                ProcessIndex = processes.Count + 1,
                                WarehouseId = MyUtilities.Warehouse.QcA,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = "admin",
                                ProductId = product.ProductId,
                            };
                            vfi.ProductionProcesses.Add(process);
                        }
                        if (processes.FirstOrDefault(ps => ps.WarehouseId == MyUtilities.Warehouse.Finish) == null) {
                            var process = new ProductionProcess {
                                IsAlert = true,
                                IsNecessary = true,
                                ProcessIndex = processes.Count + 1,
                                WarehouseId = MyUtilities.Warehouse.Finish,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = "admin",
                                ProductId = product.ProductId,
                            };
                            vfi.ProductionProcesses.Add(process);
                        }
                    }
                    var i = vfi.SaveChanges();
                    return Json(i);
                }
            }
            catch (Exception ex) {
                return Json(ex);
            }

        }
        [HttpPost]
        public ActionResult ChinhTatCaMaNguyenLieu() {
            try {
                using (var vfi = new tammaContext()) {
                    var a = 0;
                    var materials = vfi.Materials.ToList();
                    foreach (var material in materials) {
                        material.Shape = material.Shape.Trim();
                        material.DiameterType = material.DiameterType.Trim().ToLower();
                        material.MaterialCode = material.MaterialName +
                                                MyUtilities.Material.GetMaterialDesignNo(material.OutDiameter,
                                                                                         material.InDiameter,
                                                                                         material.DiameterType,
                                                                                         material.Shape);
                    }
                    a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult NLthanhNLThietKe() {
            try {
                using (var vfi = new tammaContext()) {
                    var a = 0;
                    foreach (var product in vfi.Products) {
                        product.MaterialNameDesign = product.Material.MaterialName;
                        product.OutDiameterDesign = product.Material.OutDiameter;
                        product.InDiameterDesign = product.Material.InDiameter;
                        product.ShapeDesign = product.Material.Shape;
                    }
                    a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChinhMenuLevel() {
            try {
                using (var vfi = new tammaContext()) {
                    var a = 0;
                    var menus = vfi.Menus.Where(m => m.ParentId == null);
                    foreach (var menu in menus) {
                        menu.MenuLevel = 0;
                        if (menu.Menu1.Any()) {
                            foreach (var menuChild1 in menu.Menu1) {
                                menuChild1.MenuLevel = 1;

                                if (menuChild1.Menu1.Any()) {
                                    foreach (var menuChild2 in menuChild1.Menu1) {
                                        menuChild2.MenuLevel = 2;
                                    }
                                }
                            }
                        }
                    }
                    a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChinhTaxInvoiceProduct() {
            try {
                using (var vfi = new tammaContext()) {
                    var a = 0;
                    var taxInvoices =
                        vfi.TaxInvoices.Where(
                            ti =>
                            ti.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                            ti.SetupDate != null &&
                            !ti.TaxInvoiceProducts.Any() &&
                            ti.TaxInvoiceProductDetails.Any(tipd => tipd.Active == true));
                    var list = new List<TaxInvoiceProduct>();
                    foreach (var taxInvoice in taxInvoices) {
                        var productIds =
                            taxInvoice.TaxInvoiceProductDetails.Select(tipd => tipd.ExportFormTP_KDDetail.ProductId)
                                      .Distinct()
                                      .ToList();
                        foreach (var productId in productIds) {
                            var taxInvoiceProductDetails =
                                taxInvoice.TaxInvoiceProductDetails.Where(
                                    tipd => tipd.ExportFormTP_KDDetail.ProductId == productId);
                            var taxInvoiceProduct = new TaxInvoiceProduct {
                                ProductId = productId.Value,
                                UnitPrice = taxInvoiceProductDetails.FirstOrDefault().UnitPrice,
                                Quantity = taxInvoiceProductDetails.Sum(tipd => tipd.Quantity),
                                TaxInvoiceId = taxInvoice.Id,
                                IsFinish = true,
                            };
                            list.Add(taxInvoiceProduct);
                        }
                    }
                    vfi.TaxInvoiceProducts.AddRange(list);
                    a = vfi.SaveChanges();

                    return Json(a);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChinhNhanVienSuaMay() {
            try {
                var a = 0;
                using (var vfi = new tammaContext()) {
                    var repairs = vfi.MachineRepairForms.Where(mr => mr.EmployeeId != null);
                    foreach (var repair in repairs) {
                        var detail = new RepairFormDetail {
                            FormId = repair.FormId,
                            EmployeeId = repair.EmployeeId.Value,
                            FixId = repair.FixId,
                            StartDate = repair.StartDate.Value,
                            StartUser = repair.StartUser,
                            ModifiedDate = repair.StartDate.Value,
                            ModifiedUser = repair.StartUser,
                            FinishDate = repair.FinishDate,
                            FinishUser = repair.FinishUser,
                            Note = repair.Note,
                            MoreTime = repair.MoreTime,
                            Shift = repair.Shift,
                            Status = repair.Status,
                        };
                        repair.RepairFormDetails.Add(detail);
                        if (repair.Status != (byte)MyUtilities.Machine.State.RepairStatus.None)
                            repair.Status = (byte)MyUtilities.Machine.State.RepairStatus.Finish;
                    }
                    a = vfi.SaveChanges();
                }
                return Json(a);
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChinhKhoSanPham() {
            try {
                var a = 0;
                using (var vfi = new tammaContext()) {
                    var productInvs = vfi.ProductInventories;
                    var periods = vfi.ProductInventoryPeriods.Where(pi => pi.ProductInvId == 1428).Take(100);
                    foreach (var period in periods) {
                        var productInv =
                            productInvs.FirstOrDefault(
                                pi => pi.ProductId == period.ProductId && pi.WarehouseId == period.WarehouseId);
                        if (productInv == null)
                            continue;
                        period.ProductInvId = productInv.ProductInventoryId;
                    }
                    a = vfi.SaveChanges();
                }
                return Json(a);
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChinhNguyenLieuThietKe() {
            try {
                var a = 0;
                using (var vfi = new tammaContext()) {
                    var products = vfi.Products;
                    foreach (var p in products) {
                        var lastProduction = (from id in vfi.ImportFormSX1Detail
                                              where id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                                    id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                                    id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault().Transaction.Status ==
                                                    (byte)MyUtilities.Transaction.Status.Approved &&
                                                    id.ProductId == p.ProductId
                                              orderby id.ImportFormSX1.MaterialUseDate descending
                                              select id).FirstOrDefault();
                        if (lastProduction != null) {
                            p.MaterialId = lastProduction.MaterialInventory.MaterialId;
                            p.MaterialNameDesign = lastProduction.MaterialInventory.Material.MaterialName;
                            p.OutDiameterDesign = lastProduction.MaterialInventory.Material.OutDiameter;
                            p.InDiameterDesign = lastProduction.MaterialInventory.Material.InDiameter;
                            p.ShapeDesign = lastProduction.MaterialInventory.Material.Shape.Trim();
                            p.DiameterTypeDesign = lastProduction.MaterialInventory.Material.DiameterType.Trim();
                        }
                    }
                    a = vfi.SaveChanges();
                }
                return Json(a);
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChinhTrangThaiSanPham() {
            try {
                var a = 0;
                using (var vfi = new tammaContext()) {
                    var products = vfi.Products.Where(p => !p.FinishDesign);

                    foreach (var product in products) {
                        var productId = product.ProductId;
                        //if (product == null) return false;
                        //thong tin san pham
                        if (product.Diameter == null || product.Length == null || product.KnifeCut == null ||
                            product.Productivity == null || product.ProductionRate == null)
                            continue;
                        if (product.Diameter == 0 || product.Length == 0 || product.KnifeCut == 0 ||
                            product.Productivity == 0 || product.ProductionRate == 0)
                            continue;
                        if (product.MaterialId == null)
                            continue;
                        if (product.ProcessingDesign == null)
                            continue;
                        var toolsDesign = vfi.ProductionTools.Where(pt => pt.Active && pt.ProductId == productId);
                        if (!toolsDesign.Any())
                            continue;
                        var process =
                            vfi.ProductionProcesses.Where(pp => pp.ProductId == productId && pp.IsNecessary);
                        if (!process.Any())
                            continue;
                        product.FinishDesign = true;
                    }
                    a = vfi.SaveChanges();
                }
                return Json(a);
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChinhLotNumber_ProductInventoryPeriod() {
            var a = 0;
            try {
                using (var vfi = new tammaContext()) {

                    var periods = vfi.ProductInventoryPeriods.Where(pip => pip.LotNumber == null).ToList();
                    if (!periods.Any())
                        return Json(9);
                    var customerIds = periods.Select(p => p.Product.CustomerId).Distinct().ToList();
                    if (!customerIds.Any())
                        return Json(8);
                    //var customerId = customerIds.FirstOrDefault();
                    //var periodsById = periods.Where(pip => pip.Product.CustomerId == customerId);
                    //if (periodsById.Count() > 10000) {
                    //    periodsById = periodsById.Take(10000);
                    //}
                    //foreach (var period in periodsById) {
                    //    period.LotNumber = period.ProductInventory.LotNumber;
                    //}
                    //a += vfi.SaveChanges();
                    foreach (var customerId in customerIds) {
                        var periodsById = periods.Where(pip => pip.Product.CustomerId == customerId);
                        for (int i = 0; i < periodsById.Count(); i += 10000) {
                            var periodsByIdByI = periodsById.Skip(i).Take(10000);
                            foreach (var period in periodsByIdByI) {
                                period.LotNumber = period.ProductInventory.LotNumber;
                            }
                            a += vfi.SaveChanges();

                        }
                    }
                }
                return Json(a);
            }
            catch (Exception ex) {
                return Json(ex);
                //return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChinhTrangMuaHangDaNhan() {
            try {
                var a = 0;
                using (var vfi = new tammaContext()) {
                    var purchaseOrderMaterials = vfi.PurchaseOrderDetails.Where(pod =>
                        pod.PurchaseOrder.MaterialClassifiedId == 1 &&
                        (pod.OrderQty == 5 || pod.ReceivedQty == 5));
                    foreach (var detail in purchaseOrderMaterials) {
                        var importDetails = vfi.ImportPurchaseOrderDetails.Where(ipd =>
                            ipd.ImportPurchaseOrder.PurchaseOrderId == detail.PurchaseOrderId
                             && ipd.MaterialId == detail.ReferenceId &&
                             ipd.ImportPurchaseOrder.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        if (importDetails.Any()) {
                            detail.ReceivedQty = importDetails.Sum(id => id.QuantityKg);
                        }
                        else {
                            detail.ReceivedQty = 0;
                        }
                    }

                    var purchaseOrderFuels = vfi.PurchaseOrderDetails.Where(pod =>
                        (pod.PurchaseOrder.MaterialClassifiedId == 2 ||
                        pod.PurchaseOrder.MaterialClassifiedId == 3) &&
                        (pod.OrderQty == 5 || pod.ReceivedQty == 5));

                    foreach (var detail in purchaseOrderFuels) {
                        var importDetails = vfi.TransactionFptDetails.Where(td =>
                            td.TransactionFpt.PoId == detail.PurchaseOrderId
                             && td.FptId == detail.ReferenceId &&
                             td.TransactionFpt.Status == (byte)MyUtilities.Transaction.Status.Approved);
                        if (importDetails.Any()) {
                            detail.ReceivedQty = importDetails.Sum(id => id.Quantity);
                        }
                        else {
                            detail.ReceivedQty = 0;
                        }
                    }
                    //var purchaseOrderTools = vfi.PurchaseOrderDetails.Where(pod => pod.PurchaseOrder.Status != 3 &&
                    //    pod.PurchaseOrder.MaterialClassifiedId == 3);



                    a = vfi.SaveChanges();
                }
                return Json(a);
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChinhTienTeSanPham() {
            try {
                var a = 0;
                using (var vfi = new tammaContext()) {
                    var products = vfi.Products;
                    foreach (var product in products) {
                        if (product.UnitPrice == null || product.UnitPrice < 0)
                            product.UnitPrice = 0;
                        product.Currency = MyUtilities.Product.DetectCurrency(product.UnitPrice.Value);
                    }

                    a = vfi.SaveChanges();
                }
                return Json(a);
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ProductConfigForWorkOrder() {
            try {
                var a = 0;
                var packingProductivity = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.PackingProductivity);
                using (var vfi = new tammaContext()) {
                    var products = vfi.Products;
                    var maxWeight = 20000;
                    var maxTime = 1.5 * 24 * 3600;
                    foreach (var product in products) {
                        if (string.IsNullOrWhiteSpace(product.IdentityCode)) {
                            product.IdentityCode = String.Format("{0:0000}", product.ProductId);
                        }
                        var quantityWeight = 0;
                        if (product.ProductionWeight > 0) {
                            quantityWeight = MyUtilities.Function.RoundDown(maxWeight / product.ProductionWeight.Value);
                        }
                        var quantityTime = 0;
                        if (product.Productivity > 0) {
                            quantityTime =MyUtilities.Function.RoundDown(maxTime / product.Productivity.Value);
                        }
                        product.MaxQuantityInTray = quantityWeight > quantityTime ? quantityTime : quantityWeight;
                        product.MaxQuantityInTrayRunTime = _productionController.CalculateProductionWorkOrderRunTime(product, packingProductivity);
                    }

                    a += vfi.SaveChanges();
                }
                return Json(a);
            }
            catch (Exception ex) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChinhTienTrinhSanPham() {
            try {
                var a = 0;
                using (var vfi = new tammaContext()) {
                    var products = vfi.Products.Where(p => p.Active);
                    foreach (var product in products) {

                        //var process =
                        //    product.ProductionProcesses.FirstOrDefault(
                        //        pp => pp.WarehouseId == MyUtilities.Warehouse.Production1);
                        //if (process == null) {
                        //    var newProcess = new ProductionProcess {
                        //        WarehouseId = MyUtilities.Warehouse.Production1,
                        //        ProcessIndex = 1,
                        //        IsAlert = true,
                        //        IsNecessary = true,
                        //        ProductId = product.ProductId,
                        //        Product = product,
                        //        ModifiedDate = DateTime.Now,
                        //        ModifiedUser = HttpContext.User.Identity.Name,
                        //    };
                        //    product.ProductionProcesses.Add(newProcess);
                        //}
                        //process =
                        //    product.ProductionProcesses.FirstOrDefault(
                        //        pp => pp.WarehouseId == MyUtilities.Warehouse.QcA ||
                        //              pp.WarehouseId == MyUtilities.Warehouse.QcB ||
                        //              pp.WarehouseId == MyUtilities.Warehouse.QcC);
                        //if (process == null) {
                        //    var newProcess = new ProductionProcess {
                        //        WarehouseId = MyUtilities.Warehouse.QcA,
                        //        ProcessIndex = 7,
                        //        IsAlert = true,
                        //        IsNecessary = true,
                        //        ProductId = product.ProductId,
                        //        Product = product,
                        //        ModifiedDate = DateTime.Now,
                        //        ModifiedUser = HttpContext.User.Identity.Name,
                        //    };
                        //    product.ProductionProcesses.Add(newProcess);
                        //}
                        //else {
                        //    process.ProcessIndex = 7;
                        //}
                        var process =
                            product.ProductionProcesses.FirstOrDefault(
                                pp => pp.WarehouseId == MyUtilities.Warehouse.Packing);
                        if (process == null) {
                            var newProcess = new ProductionProcess {
                                WarehouseId = MyUtilities.Warehouse.Packing,
                                ProcessIndex = 8,
                                IsAlert = true,
                                IsNecessary = true,
                                ProductId = product.ProductId,
                                Product = product,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                            };
                            product.ProductionProcesses.Add(newProcess);
                        }
                        else if (!process.IsAlert || !process.IsNecessary) {
                            process.IsAlert = true;
                            process.IsNecessary = true;
                        }
                        //process =
                        //    product.ProductionProcesses.FirstOrDefault(
                        //        pp => pp.WarehouseId == MyUtilities.Warehouse.Finish);
                        //if (process == null) {
                        //    var newProcess = new ProductionProcess {
                        //        WarehouseId = MyUtilities.Warehouse.Finish,
                        //        ProcessIndex = 9,
                        //        IsAlert = true,
                        //        IsNecessary = true,
                        //        ProductId = product.ProductId,
                        //        Product = product,
                        //        ModifiedDate = DateTime.Now,
                        //        ModifiedUser = HttpContext.User.Identity.Name,
                        //    };
                        //    product.ProductionProcesses.Add(newProcess);
                        //}
                        //var processes = product.ProductionProcesses.Where(pp => pp.IsNecessary).ToList();
                        var tracks = product.RealProductions.Select(rp => rp.TrackUpMachine).ToList();
                        foreach (var track in tracks) {
                            var processByMachine =
                                   product.ProductionProcessByMachines.FirstOrDefault(
                                       ppm => ppm.MachineId == track.MachineId &&
                                              ppm.WarehouseId == process.WarehouseId);
                            if (processByMachine == null) {
                                processByMachine = new ProductionProcessByMachine {
                                    ProductId = track.ProductId,
                                    MachineId = track.MachineId,
                                    WarehouseId = process.WarehouseId,
                                    Active = true,
                                    Note = "Auto",
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    ProcessIndex = process.ProcessIndex,
                                    UnitWeight =
                                        MyUtilities.Product.GetProductInvWeight(track.ProductId,
                                            process.WarehouseId)
                                };
                                product.ProductionProcessByMachines.Add(processByMachine);
                            }
                            //foreach (var productionProcess in processes) {

                            //}

                        }
                        a += vfi.SaveChanges();
                    }
                }
                return Json(a);
            }
            catch (Exception) {
                return Json(-1);
            }
            //var processProduction1 = new ProductionProcess
            //{
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
            //var processQc = new ProductionProcess
            //{
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
            //var processPacking = new ProductionProcess
            //{
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
            //var processFinish = new ProductionProcess
            //{
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
        }
        [HttpPost]
        public ActionResult ChinhMaNguyenLieu() {
            try {
                var a = 0;
                using (var vfi = new tammaContext()) {
                    var materials = vfi.Materials.ToList();
                    var deleteMaterialIds = new List<int>();
                    var deleteProductionMaterialIds = new List<int>();
                    var rootId = 0;
                    var deleteMaterial = new Material();
                    foreach (var material in materials) {
                        if (deleteMaterialIds.Contains(material.MaterialId))
                            continue;
                        var sameMaterials = vfi.Materials.Where(m => m.MaterialName.Equals(material.MaterialName)
                                                                     && m.OutDiameter == material.OutDiameter
                                                                     && m.InDiameter == material.InDiameter
                                                                     && m.DiameterType.Equals(material.DiameterType)
                                                                     && m.Shape.Equals(material.Shape)).ToList();
                        if (sameMaterials.Count == 0)
                            continue;
                        rootId = sameMaterials[0].MaterialId;
                        if (sameMaterials.Count > 1) {
                            while (sameMaterials.Count != 1) {
                                deleteMaterial = sameMaterials[1];
                                var tracks = vfi.TrackUpMachines.Where(t => t.MaterialId == deleteMaterial.MaterialId);
                                foreach (var track in tracks) {
                                    track.MaterialId = rootId;
                                }
                                var productionMaterials =
                                    vfi.ProductionMaterials.Where(pm => pm.MaterialId == deleteMaterial.MaterialId);
                                foreach (var productionMaterial in productionMaterials) {
                                    var existProductionMaterial =
                                        vfi.ProductionMaterials.FirstOrDefault(
                                            pm =>
                                            pm.MaterialId == rootId && pm.ProductId == productionMaterial.ProductId);
                                    if (existProductionMaterial != null) {
                                        deleteProductionMaterialIds.Add(productionMaterial.RealMaterialId);
                                    }
                                    else {
                                        productionMaterial.MaterialId = rootId;
                                    }
                                }
                                var exportMaterials =
                                    vfi.ExportMaterialDetails.Where(ed => ed.MaterialId == deleteMaterial.MaterialId);
                                foreach (var exportMaterialDetail in exportMaterials) {
                                    exportMaterialDetail.MaterialId = rootId;
                                }
                                var periods =
                                    vfi.MaterialInventoryPeriods.Where(
                                        mip => mip.MaterialId == deleteMaterial.MaterialId);
                                foreach (var period in periods) {
                                    period.MaterialId = rootId;
                                }
                                var transactionDetails =
                                    vfi.TransactionDetails.Where(
                                        td =>
                                        td.ReferenceId == deleteMaterial.MaterialId &&
                                        td.Transaction.MoP);
                                foreach (var transactionDetail in transactionDetails) {
                                    transactionDetail.ReferenceId = rootId;
                                }
                                var purchaseMaterials =
                                    vfi.PurchaseOrderDetails.Where(
                                        pod =>
                                        pod.ReferenceId == deleteMaterial.MaterialId && pod.MaterialClassifiedId == 1);
                                foreach (var purchaseMaterial in purchaseMaterials) {
                                    purchaseMaterial.ReferenceId = rootId;
                                }
                                var materialInvs =
                                    vfi.MaterialInventories.Where(mi => mi.MaterialId == deleteMaterial.MaterialId);
                                foreach (var materialInventory in materialInvs) {
                                    materialInventory.Length = deleteMaterial.Length.Value;
                                    materialInventory.MaterialId = rootId;
                                }
                                var importMaterials =
                                    vfi.ImportPurchaseOrderDetails.Where(
                                        id => id.MaterialId == deleteMaterial.MaterialId);
                                foreach (var importMaterial in importMaterials) {
                                    importMaterial.Length = deleteMaterial.Length.Value;
                                    importMaterial.MaterialId = rootId;
                                }
                                var products = vfi.Products.Where(p => p.MaterialId == deleteMaterial.MaterialId);
                                foreach (var product in products) {
                                    product.MaterialId = rootId;
                                }
                                deleteMaterialIds.Add(deleteMaterial.MaterialId);
                                sameMaterials.Remove(deleteMaterial);
                            }
                            //break;
                        }
                        var materialInvs2 = vfi.MaterialInventories.Where(mi => mi.MaterialId == rootId);
                        foreach (var materialInventory in materialInvs2) {
                            materialInventory.Length = sameMaterials[0].Length.Value;
                        }
                        var importMaterials2 =
                            vfi.ImportPurchaseOrderDetails.Where(
                                id => id.MaterialId == rootId);
                        foreach (var importMaterial in importMaterials2) {
                            importMaterial.Length = sameMaterials[0].Length.Value;
                        }
                        material.MaterialCode = material.MaterialName +
                                                MyUtilities.Material.GetMaterialDesignNo(material.OutDiameter,
                                                                                         material.InDiameter,
                                                                                         material.DiameterType,
                                                                                         material.Shape);
                    }
                    var deleteProductionMaterials =
                        vfi.ProductionMaterials.Where(pm => deleteProductionMaterialIds.Contains(pm.RealMaterialId));
                    vfi.ProductionMaterials.RemoveRange(deleteProductionMaterials);
                    var deleteMaterials = vfi.Materials.Where(m => deleteMaterialIds.Contains(m.MaterialId));
                    vfi.Materials.RemoveRange(deleteMaterials);
                    a = vfi.SaveChanges();
                }
                return Json(a);
            }
            catch (Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public ActionResult TaoPhePhamSanXuat() {
            try {
                using (var vfi = new tammaContext()) {
                    var a = 0;

                    var importSx1Details =
                        vfi.ImportFormSX1Detail.Where(
                            id => id.ImportFormSX1.MaterialUseDate >= MyUtilities.Product.StartWorkpieceDate &&
                                  id.DefectProduct1 + id.DefectProduct2 > 0)
                           .ToList();
                    var importIds = importSx1Details.Select(id => id.ImportId).Distinct().ToList();
                    foreach (var importId in importIds) {
                        var importWorkpiece = vfi.ImportWorkpieceMaterials.FirstOrDefault(i => i.ImportSx1Id == importId);

                        var list = new List<WorkpieceMaterialPeriod>();
                        foreach (var identity in MaterialIdentityCode.GetMaterialIdentityCodes(0)) {
                            var totalQuantity = vfi.WorkpieceMaterialPeriods.Where(
                                p => p.IdentityCode.Equals(identity.IdentityCode) && p.Type == 3).ToList()
                                                   .Sum(p => p.LastQuantity - p.EarlyQuantity);
                            var period = new WorkpieceMaterialPeriod {
                                IdentityCode = identity.IdentityCode,
                                Type = 3,
                                EarlyQuantity = totalQuantity,
                                EoIId = importWorkpiece.ImportId,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                PeriodDate = importWorkpiece.ImportDate.Value,
                                EoI = (byte)MyUtilities.Transaction.EoIEnum.Import,
                                Weight = 0,
                                IsDestroy = false,
                            };
                            var importDetails =
                                importSx1Details.Where(
                                    id =>
                                    id.DefectProduct1 + id.DefectProduct2 > 0 &&
                                    id.ImportId == importId &&
                                    id.MaterialInventory.Material.MaterialType.IdentityCode.Equals(
                                        identity.IdentityCode));
                            if (importDetails.Any()) {
                                period.Weight =
                                    Math.Round(importDetails.Sum(
                                        id => (id.DefectProduct1 + id.DefectProduct2) * id.ProductWeight / 1000), 2);
                            }
                            if (period.Weight == 0) continue;
                            period.LastQuantity = Math.Round(period.EarlyQuantity + period.Weight, 2);
                            list.Add(period);
                        }
                        vfi.WorkpieceMaterialPeriods.AddRange(list);
                        a += vfi.SaveChanges();
                    }
                    return Json(a);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }
        [HttpPost]
        public ActionResult TaoNoPhaiThu(int customerId, double money, string currency) {
            try {
                using (var vfi = new tammaContext()) {
                    var a = 0;
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    if (customer == null)
                        return Json(9);
                    var productId = 1245;
                    //var product = vfi.Products.FirstOrDefault(p => p.CustomerId == customerId && p.Active);
                    // if (product == null)
                    //     return Json(8);

                    var exportDetail = new ExportFormTP_KDDetail {
                        ProductId = productId,
                        Quality = 1,
                        Note = "tạo ảo",
                        Weight = 1,
                        IsInvoiced = true,
                    };
                    var taxInvoice = new TaxInvoice {
                        CustomerId = customerId,
                        Status = (byte)MyUtilities.Sales.Status.InProcess,
                        Currency = currency,
                        ExchangeRate = 1,
                        TaxPercent = 0,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        TaxInvoiceProductDetails = new List<TaxInvoiceProductDetail>(),
                        SetupDate = MyUtilities.Sales.StartTaxInvoiceDate,
                        Note = "tạo ảo",
                        TaxInvoiceList =
                            "VP" +
                            MyUtilities.Sales.StartTaxInvoiceDate.Year + "" +
                            MyUtilities.Sales.StartTaxInvoiceDate.Month + "-" +
                            customer.ShortName,
                        TotalAmount = money,
                    };
                    var taxInvoiceProduct = new TaxInvoiceProduct {
                        UnitPrice = money,
                        Quantity = exportDetail.Quality,
                        TaxInvoice = taxInvoice,
                        TaxInvoiceId = taxInvoice.Id,
                        ProductId = exportDetail.ProductId.Value,
                        IsFinish = true,
                    };
                    var taxInvoiceProductDetail = new TaxInvoiceProductDetail {
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ExportDate = MyUtilities.Sales.StartTaxInvoiceDate,
                        UnitPrice = money,
                        Quantity = exportDetail.Quality,
                        TaxInvoice = taxInvoice,
                        TaxInvoiceId = taxInvoice.Id,
                        ExportDetailId = exportDetail.DetailId,
                        ExportFormTP_KDDetail = exportDetail,
                        Active = true,
                    };
                    taxInvoice.TaxInvoiceProducts.Add(taxInvoiceProduct);
                    taxInvoice.TaxInvoiceProductDetails.Add(taxInvoiceProductDetail);
                    vfi.TaxInvoices.Add(taxInvoice);
                    a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult TaoChuaXuatHoaDon(int customerId, double money, string currency) {
            try {
                using (var vfi = new tammaContext()) {
                    var a = 0;
                    return Json(a);
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    if (customer == null)
                        return Json(9);
                    var productId = 1245;
                    //var product = vfi.Products.FirstOrDefault(p => p.CustomerId == customerId && p.Active);
                    //if (product == null)
                    //    return Json(8);
                    //var transaction = new Vfi.Models.Transaction
                    //{
                    //    WarehouseIssueId =  MyUtilities.Warehouse.Finish,
                    //    WarehouseReceiptId =  MyUtilities.Warehouse.Business,

                    //    TransactionCode = "",
                    //    EoI = "2",
                    //    MoP = false,
                    //    CreatedUser = HttpContext.User.Identity.Name,
                    //    CreatedDate = MyUtilities.Sales.StartTaxInvoiceDateCheat,
                    //    Status = (byte)MyUtilities.Transaction.Status.Open,
                    //    Active = true,
                    //    ModifiedUser = HttpContext.User.Identity.Name,
                    //    ModifiedDate = DateTime.Now,
                    //};
                    var export = new ExportFormTP_KD() {
                        CustomerId = customerId,
                        Transporter = "",
                        TransactionCode = "",
                        CompanyTransporter = "",
                        CarNumber = "",
                        DateTransporter = MyUtilities.Sales.StartTaxInvoiceDate,
                        DateCreate = DateTime.Now,
                        TotalBox = 0,
                        //OrderId = id,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    };
                    var exportDetail = new ExportFormTP_KDDetail {
                        ExportId = export.ExportId,
                        ProductId = productId,
                        Quality = 1,
                        Note = "tạo ảo",
                        Weight = 1,
                    };
                    var order = new Order {
                        CustomerId = customerId,
                        SalesPersonId = null,
                        CreatedDate = DateTime.Now,
                        OrderDate = export.DateTransporter.Value,
                        DueDate = export.DateTransporter,
                        OrderNumber = "",
                        //PoNumber = poNumber,
                        LotNumber = "",
                        ModelNumber = "",
                        BillToAddress = "",
                        ShipToAddress = "",
                        ShipMethodId = null,
                        CurrencyCode = currency,
                        Note = "tạo ảo",
                        PaymentTermId = null,
                        //DueDate = Convert.ToDateTime(dueDate),
                        Active = true,
                        Status = 0,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                    };
                    var orderDetail = new OrderDetail {
                        OrderId = order.OrderId,
                        ProductId = productId,
                        OrderQty = 1,
                        RequiedNumber = 0,
                        LineTotal = 0,
                        CustomerDueDate = export.DateTransporter,
                        UnitPrice = money,
                        //VFIDueDate =  entity.VFIDueDate,
                        LotNumber = "",
                        ModelNumber = "",
                        Active = true,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        PONumber = "",
                        IsComplete = false,
                        Status = (int)MyUtilities.Sales.Status.Waiting,
                    };
                    var invoice = new Vfi.Models.Invoice {
                        InvoiceNumber = MyUtilities.AutoIncrease.GetParam((int)MyUtilities.AutoIncrease.IncreaseNum.Invoice, 1),
                        CustomerId = customerId,
                        ExportId = export.ExportId,
                        Active = true,
                        ShipmentDate = export.DateTransporter,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ExchangeRate = 1,
                        TaxPercent = 0,
                        Status = (byte)MyUtilities.Sales.Status.Waiting
                    };
                    var entity = new InvoiceDetail {
                        Piece = 1,
                        OrderDetailId = orderDetail.OrderDetailId,
                        Price = orderDetail.UnitPrice,
                        ProductId = productId,
                        Active = true,
                        ExportDetailId = exportDetail.DetailId,
                        InvoiceId = invoice.InvoiceId,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                    };
                    vfi.ExportFormTP_KD.Add(export);
                    vfi.ExportFormTP_KDDetail.Add(exportDetail);
                    vfi.Orders.Add(order);
                    vfi.OrderDetails.Add(orderDetail);
                    vfi.Invoices.Add(invoice);
                    vfi.InvoiceDetails.Add(entity);
                    a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChinhHoaDon() {
            try {
                using (var vfi = new tammaContext()) {
                    var taxProducts = vfi.TaxInvoiceProductDetails;
                    foreach (var taxProduct in taxProducts) {
                        taxProduct.Quantity = taxProduct.ExportFormTP_KDDetail.Quality;
                    }
                    var i = vfi.SaveChanges();
                    return Json(i);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChuyenDoiTransactionChoDung() {
            try {
                var i = 0;
                using (var vfi = new tammaContext()) {
                    var exports = vfi.ExportFormTP_KD;
                    foreach (var export in exports) {
                        if (string.IsNullOrWhiteSpace(export.TransactionCode))
                            i = 5;
                        var isTamMaCode = false;
                        var transaction =
                            vfi.Transactions.FirstOrDefault(t => t.TransactionCode.Equals(export.TransactionCode));
                        if (transaction == null) {
                            if (string.IsNullOrWhiteSpace(export.TransactionCode))
                                isTamMaCode = true;
                            else {
                                ModelState.AddModelError("TransactionNull", export.TransactionCode);
                                continue;
                            }
                        }
                        else if (transaction.Status != (byte)MyUtilities.Transaction.Status.Approved) continue;
                        foreach (var exportDetail in export.ExportFormTP_KDDetail) {
                            var transactionDetails = new List<TransactionDetail>();
                            if (!isTamMaCode) {
                                transactionDetails =
                                    transaction.TransactionDetails.Where(
                                        td =>
                                        td.ReferenceId == exportDetail.ProductId && td.Quantity == exportDetail.Quality)
                                               .ToList();
                                if (!transactionDetails.Any()) {
                                    ModelState.AddModelError("TransactionDetailNull",
                                                             export.TransactionCode + "" +
                                                             exportDetail.Product.ProductCode);
                                    continue;
                                }
                            }
                            if (exportDetail.OrderDetailId != null) continue;
                            try {
                                if (transactionDetails.Count() > 1) {
                                    foreach (var transactionDetail in transactionDetails) {
                                        var orderDetailId = Convert.ToInt32(transactionDetail.Note);
                                        exportDetail.OrderDetailId = orderDetailId;
                                        exportDetail.TransactionDetailId = transactionDetail.TransactionDetailId;
                                        transactionDetail.Note = "";
                                    }
                                }
                                else {
                                    var orderDetails =
                                        vfi.OrderDetails.Where(
                                            od => od.ProductId == exportDetail.ProductId && od.OrderId == export.OrderId);
                                    if (!orderDetails.Any()) {
                                        ModelState.AddModelError("ChangeOrderError",
                                                                 export.TransactionCode + "" +
                                                                 exportDetail.Product.ProductCode);
                                        continue;
                                    }
                                    if (orderDetails.Count() > 1) {
                                        ModelState.AddModelError("ChangeOrderError",
                                                                 export.TransactionCode + "" +
                                                                 exportDetail.Product.ProductCode);
                                        continue;
                                    }
                                    var orderDetail = orderDetails.First();
                                    exportDetail.OrderDetailId = orderDetail.OrderDetailId;
                                    if (!isTamMaCode) {
                                        var transactionDetail = transactionDetails.First();
                                        exportDetail.TransactionDetailId = transactionDetail.TransactionDetailId;
                                    }
                                }
                            }
                            catch (FormatException) {
                                ModelState.AddModelError("FormatError",
                                                         export.TransactionCode + "" + exportDetail.Product.ProductCode);
                                continue;
                            }
                        }
                        i += vfi.SaveChanges();
                    }
                    vfi.SaveChanges();
                    return Json(i);
                }
            }
            catch (Exception) {

            }
            return Json(-1);
        }

        [HttpPost]
        public ActionResult DieuChinhInvoice() {
            try {
                using (var vfi = new tammaContext()) {
                    var exportDetailIds = new List<int>
                        {
                            274,
                            304,
                            309,
                            866,
                            2537,
                            3179,
                            3404,
                            3406,
                            3408,
                            3528,
                            3601,
                            3641,
                            3669,
                            3696,
                            3697,
                            3700,
                            3736,
                            3797,
                            3839,
                            3845,
                            3849,
                            3861,
                            3864,
                            3869,
                            3871,
                            3872,
                            3873,
                            3879,
                            3881,
                            3884,
                            3885,
                            3887,
                            3888,
                            3889,
                            3890,
                            3891,
                            3892,
                            3893,
                            3894,
                            3895,
                            3897,
                            3900,
                            3902,
                            3907,
                            3915,
                            3919,
                            3923,
                            3927,
                            3932,
                            3933,
                            3934,
                            3939,
                            3954,
                            3955,
                            3958,
                            3959,
                            3960,
                            4304,
                            5398
                        };
                    var exportIds = new List<int>();
                    foreach (var detailId in exportDetailIds) {
                        var detail = vfi.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.DetailId == detailId);
                        detail.IsInvoiced = true;
                        exportIds.Add(detail.ExportId.Value);
                    }
                    var a = vfi.SaveChanges();
                    var invoices =
                        vfi.Invoices.Where(
                            i =>
                            i.Status != (byte)MyUtilities.Sales.Status.Completed || i.Status != (byte)MyUtilities.Sales.Status.Cancel);
                    foreach (var invoice in invoices) {
                        var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                        if (export == null || export.ExportFormTP_KDDetail.Any(ed => ed.IsInvoiced == false)) continue;
                        invoice.Status = (byte)MyUtilities.Sales.Status.Completed;
                    }
                    a += vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception) {

            }
            return Json(-1);
        }

        [HttpPost]
        public ActionResult ApGiaSanPham() {
            try {
                using (var vfi = new tammaContext()) {
                    var products = from p in vfi.Products
                                   where p.UnitPrice == 0 || p.UnitPrice == null
                                   select p;
                    var productIds = products.Select(p => p.ProductId);
                    var orderDetails = from od in vfi.OrderDetails
                                       where od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                             productIds.Contains(od.ProductId)
                                       orderby od.Order.DueDate descending
                                       select new {
                                           od.ProductId,
                                           od.UnitPrice
                                       };
                    var productId2s = orderDetails.Select(od => od.ProductId).Distinct().ToList();
                    foreach (var productId in productId2s) {
                        var orderDetail = orderDetails.FirstOrDefault(od => od.ProductId == productId);
                        if (orderDetail != null) {
                            var product = products.FirstOrDefault(p => p.ProductId == productId);
                            product.UnitPrice = orderDetail.UnitPrice;
                        }
                    }
                    var a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception) {
            }
            return Json(-1);
        }

        [HttpPost]
        public ActionResult ChuyenGiaCongSanPham() {
            try {
                using (var vfi = new tammaContext()) {
                    var products = vfi.Products.Where(p => p.Active && p.KnifeCut == 0);
                    var productIds = products.Select(p => p.ProductId).ToList();

                    var realProductions = from rp in vfi.RealProductions
                                          where productIds.Contains(rp.ProductId)
                                          select new {
                                              rp.ProductId,
                                              rp.TrackUpMachine.KnifeCut,
                                          };
                    foreach (var product in products) {
                        var realKnifes = realProductions.Where(rp => rp.ProductId == product.ProductId);
                        if (realKnifes.Any()) {
                            product.KnifeCut = realKnifes.Max(rp => rp.KnifeCut);
                        }
                    }
                    var a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception) {
            }
            return Json(-1);
        }

        [HttpPost]
        public ActionResult TaoTLSX1() {
            try {
                using (var vfi = new tammaContext()) {
                    var productionProducts = (from id in vfi.ImportFormSX1Detail
                                              where id.Number1 + id.Number2 > 0
                                              //&&id.ImportFormSX1.
                                              select id).ToList();
                    var productIds = productionProducts.Select(id => id.ProductId).Distinct().ToList();
                    var products =
                        vfi.Products.Where(
                            p =>
                            p.Active &&
                                //(p.ProductionWeight == null || p.ProductionWeight == 0 || p.ProductionWeight == 1) &&
                            productIds.Contains(p.ProductId));
                    foreach (var p in products) {
                        var lastWeight = productionProducts.LastOrDefault(id => id.ProductId == p.ProductId);
                        if (p.ProductionWeight == null || p.ProductionWeight == 0 || p.ProductionWeight == 1) {
                            p.ProductionWeight = lastWeight.ProductWeight;
                        }
                        if (p.ProductionProcesses.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.Cnc) !=
                            null && (p.CncWeight == null || p.CncWeight == 0 || p.CncWeight == 1)) {
                            p.CncWeight = p.ProductionWeight;
                        }

                        if (p.ProductionProcesses.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.Production2) !=
                            null && (p.Production2Weight == null || p.Production2Weight == 0 || p.Production2Weight == 1)) {
                            p.Production2Weight = p.ProductionWeight;
                        }
                        if (p.ProductionProcesses.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.HeatTreatment) !=
                            null && (p.HeatTreatmentWeight == null || p.HeatTreatmentWeight == 0 || p.HeatTreatmentWeight == 1)) {
                            p.HeatTreatmentWeight = p.ProductionWeight;
                        }
                        if (p.ProductionProcesses.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment) !=
                            null && (p.SurfaceTreatmentWeight == null || p.SurfaceTreatmentWeight == 0 || p.SurfaceTreatmentWeight == 1)) {
                            p.SurfaceTreatmentWeight = p.ProductionWeight;
                        }
                        if (p.ProductionProcesses.FirstOrDefault(pp => pp.WarehouseId == MyUtilities.Warehouse.WaitingPlating) !=
                            null && (p.WaitingPlatingWeight == null || p.WaitingPlatingWeight == 0 || p.WaitingPlatingWeight == 1)) {
                            p.WaitingPlatingWeight = p.ProductionWeight;
                        }
                        if ((p.QcWeight == null || p.QcWeight == 0 || p.QcWeight == 1)) {
                            p.QcWeight = p.ProductionWeight;
                        }
                    }
                    var a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception) {
            }
            return Json(-1);
        }

        [HttpPost]
        public ActionResult TaoTLNLSP() {
            try {
                using (var vfi = new tammaContext()) {
                    var list = new List<ProductionMaterial>();
                    foreach (var material in vfi.ProductionMaterials) {
                        material.UnitWeightByMaterial =
                            MyUtilities.Product.GetProductWeight(material.Material.MaterialName,
                                                                 material.Material.OutDiameter,
                                                                 material.Material.InDiameter,
                                                                 material.Product.Length ?? 0,
                                                                 material.Product.KnifeCut ?? 0,
                                                                 material.Material.Shape + "");
                        var materials =
                            vfi.Materials.Where(m => m.MaterialName.Equals(material.Material.MaterialName) &&
                                                     m.OutDiameter == material.Material.OutDiameter &&
                                                     m.InDiameter == material.Material.InDiameter &&
                                                     m.Shape.Equals(material.Material.Shape) &&
                                                     m.DiameterType.Equals(material.Material.DiameterType));
                        foreach (var material1 in materials) {
                            var productionMaterial =
                                vfi.ProductionMaterials.FirstOrDefault(
                                    pm => pm.ProductId == material.ProductId && pm.MaterialId == material1.MaterialId);
                            if (productionMaterial == null) {
                                productionMaterial = new ProductionMaterial {
                                    MaterialId = material.MaterialId,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = "Auto",
                                    ProductId = material.ProductId,
                                };
                                list.Add(productionMaterial);
                            }
                            productionMaterial.Active = true;
                            productionMaterial.UnitWeightByMaterial = material.UnitWeightByMaterial;
                        }
                    }
                    vfi.ProductionMaterials.AddRange(list);
                    var a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception) {
            }
            return Json(-1);
        }

        [HttpPost]
        public ActionResult TaoDinhMucSpSx1() {
            try {

                using (var vfi = new tammaContext()) {
                    var importSx1Details = from id in vfi.ImportFormSX1Detail
                                           where id.ProductionRate == null || id.ProductionRate == 0
                                           select id;
                    foreach (var importDetail in importSx1Details) {
                        var lastImports =
                            vfi.ImportFormSX1Detail.Where(
                                id =>
                                id.ProductId == importDetail.ProductId && id.MaterialInvId == importDetail.MaterialInvId &&
                                id.MachineId == importDetail.MachineId && id.DetailId != importDetail.DetailId);
                        if (lastImports.Any()) {
                            importDetail.ProductionRate = lastImports.ToList().LastOrDefault().ProductionRate;
                        }
                        else
                            importDetail.ProductionRate = importDetail.Product.ProductionRate ?? 0;
                    }
                    var a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult ChinhLaiTraHang() {
            var i = 0;
            try {

                using (var vfi = new tammaContext()) {
                    var orderNotes = (from o in vfi.OrderNotes
                                      where o.CreatedDate > MyUtilities.Sales.StartOrderReportDate
                                            && o.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved
                                            && o.NoteType == 1
                                      //&& (o.NoteId == 264 
                                      //|| o.NoteId == 267)
                                      select o).ToList();
                    var exportIds = orderNotes.Select(on => on.ExportId).Distinct().ToList();
                    var exports = vfi.ExportFormTP_KD.Where(e => exportIds.Contains(e.ExportId));

                    //var export2 = exports.Where(e => e.TransactionCode.Equals("")).ToList();
                    //i = 0;
                    //return Json(0);


                    foreach (var export in exports) {
                        i++;
                        //if (i == 15)
                        //    i = 15;
                        var orderNoteByIds = orderNotes.Where(on => on.ExportId == export.ExportId).ToList();
                        var orderNoteIds = orderNoteByIds.Select(on => on.NoteId).Distinct().ToList();
                        var transactionReturns = orderNoteByIds.Select(on => on.TransactionId).Distinct().ToList();
                        var orderNoteDetails =
                            vfi.OrderNoteDetails.Where(ond => orderNoteIds.Contains(ond.NoteId.Value)).ToList();
                        var productIds = orderNoteDetails.Select(ond => ond.ProductId.Value).Distinct().ToList();
                        foreach (var productId in productIds) {
                            var orderNoteDetailByIds = orderNoteDetails.Where(ond => ond.ProductId == productId);
                            var exportDetail =
                                vfi.ExportFormTP_KDDetail.FirstOrDefault(
                                    ed =>
                                    ed.ProductId == productId && ed.ExportId == export.ExportId);
                            var transactionExport =
                                vfi.TransactionDetails.FirstOrDefault(
                                    td =>
                                    td.Transaction.TransactionCode.Equals(exportDetail.ExportFormTP_KD.TransactionCode) &&
                                    td.ReferenceId == exportDetail.ProductId);
                            if (transactionExport == null)
                                continue;
                            var periods =
                                vfi.ProductInventoryPeriods.Where(
                                    pip =>
                                    pip.TransactionId == transactionExport.TransactionId &&
                                    pip.ProductId == exportDetail.ProductId &&
                                    pip.WarehouseId == MyUtilities.Warehouse.Business).ToList();
                            var periodReturns =
                                vfi.ProductInventoryPeriods.Where(
                                    pip =>
                                  transactionReturns.Contains(pip.TransactionId) &&
                                    pip.ProductId == exportDetail.ProductId &&
                                    pip.WarehouseId == MyUtilities.Warehouse.Business).ToList();
                            var totalQuantity = periods.Sum(pip => pip.Quantity) -
                                                periodReturns.Sum(pip => pip.Quantity);

                            //if (transactionDetail.Quantity != periods.Sum(pip=> pip.Quantity))
                            //{
                            //if (transactionExport.Quantity != periods.Sum(pip => pip.Quantity))
                            if (transactionExport.Quantity == totalQuantity && exportDetail.Quality == totalQuantity)
                                continue;
                            if (transactionExport.Quantity != totalQuantity)
                                transactionExport.Quantity = totalQuantity;
                            if (exportDetail.Quality != totalQuantity)
                                exportDetail.Quality = totalQuantity;
                            //}
                            var invoiceDetails = exportDetail.InvoiceDetails.Where(id => id.Active && id.Piece > 0);
                            var totalSendBack =
                                exportDetail.InvoiceDetails.Where(id => id.Active && id.Piece < 0)
                                            .Sum(id => id.Piece * -1);
                            foreach (var invoiceDetail in invoiceDetails) {
                                var orderDetail = invoiceDetail.OrderDetail;
                                if (totalSendBack > invoiceDetail.Piece) {
                                    totalSendBack -= invoiceDetail.Piece;
                                    orderDetail.OrderQty -= invoiceDetail.Piece;
                                    orderDetail.RequiedNumber -= invoiceDetail.Piece;
                                    orderDetail.Note += " !Trả hàng:" + invoiceDetail.Piece;
                                }
                                else {
                                    orderDetail.OrderQty -= Convert.ToInt32(totalSendBack);
                                    orderDetail.RequiedNumber -= Convert.ToInt32(totalSendBack);
                                    orderDetail.Note += " !Trả hàng:" + totalSendBack;
                                    totalSendBack = 0;
                                }
                                if (orderDetail.RequiedNumber < 0)
                                    orderDetail.RequiedNumber = 0;
                                if (totalSendBack == 0) break;
                            }
                            //}
                        }
                        //break;
                    }


                    var a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult TaoChiTietInvoice() {
            try {
                var listDetail = new List<InvoiceDetail>();
                using (var vfi = new tammaContext()) {
                    //var startTaxInvoiceDate = new DateTime(2014, 12, 31, 10, 0, 0);
                    //vfi.Configuration.LazyLoadingEnabled = false;
                    var invoices = from i in vfi.Invoices
                                   where
                                       i.Status != (byte)MyUtilities.Sales.Status.Cancel
                                       && i.ShipmentDate > MyUtilities.Sales.StartOrderReportDate
                                       && i.OrderId != null
                                   //&& i.InvoiceDetails ==null
                                   //&& !i.InvoiceDetails.Any()
                                   //&& i.ExportFormTP_KD.ExportFormTP_KDDetail.FirstOrDefault().OrderDetailId != null
                                   //&& i.InvoiceId == 90
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
                    var invoiceIds = invoices.Select(i => i.InvoiceId).Distinct();
                    //var orderNotes =
                    //    vfi.OrderNotes.Where(
                    //        on =>
                    //        invoiceIds.Contains(on.InvoiceId.Value) &&
                    //        on.Transaction.Status == (byte) MyUtilities.Transaction.Status.Approved);
                    var orderNoteDetails =
                        vfi.OrderNoteDetails.Where(ond => invoiceIds.Contains(ond.OrderNote.InvoiceId.Value) &&
                                                          ond.OrderNote.Transaction.Status ==
                                                          (byte)MyUtilities.Transaction.Status.Approved);
                    //var a = 0;
                    foreach (var invoice in invoices) {
                        var export = vfi.ExportFormTP_KD.FirstOrDefault(e => e.ExportId == invoice.ExportId);
                        if (export == null) continue;
                        var exportDetail = export.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.InvoiceDetails.Any(id => id.Active));
                        if (exportDetail != null) continue;
                        //var orderNotesById = orderNoteDetails.Where(ond => ond.OrderNote.InvoiceId == invoice.InvoiceId);
                        foreach (var detail in export.ExportFormTP_KDDetail) {
                            if (detail.OrderDetail == null) continue;
                            var orderDetail = detail.OrderDetail;
                            var exportInvoiceDetail =
                                vfi.InvoiceDetails.FirstOrDefault(
                                    id =>
                                    id.OrderDetailId == orderDetail.OrderDetailId
                                    && invoice.InvoiceId == id.InvoiceId
                                    && id.ExportDetailId == detail.DetailId);
                            if (exportInvoiceDetail == null) {
                                exportInvoiceDetail = new InvoiceDetail {
                                    Piece = Convert.ToInt32(detail.Quality),
                                    OrderDetailId = orderDetail.OrderDetailId,
                                    Price = orderDetail.UnitPrice,
                                    ProductId = detail.ProductId,
                                    Active = true,
                                    ExportDetailId = detail.DetailId,
                                    InvoiceId = invoice.InvoiceId,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedUser = HttpContext.User.Identity.Name,
                                    Weight = detail.Weight
                                };
                                listDetail.Add(exportInvoiceDetail);
                            }

                            var orderNoteDetailsById =
                                orderNoteDetails.Where(
                                    ond =>
                                    ond.OrderNote.InvoiceId == invoice.InvoiceId &&
                                    ond.ProductId == detail.ProductId);
                            foreach (var orderNoteDetail in orderNoteDetailsById) {
                                var returnInvoiceDetail =
                                    vfi.InvoiceDetails.FirstOrDefault(
                                        id =>
                                         invoice.InvoiceId == id.InvoiceId
                                        && id.NoteDetailId == orderNoteDetail.NoteDetailId);
                                if (returnInvoiceDetail == null) {
                                    returnInvoiceDetail =
                                        vfi.InvoiceDetails.FirstOrDefault(
                                            id =>
                                            id.OrderDetailId == null
                                            && invoice.InvoiceId == id.InvoiceId
                                            && id.ExportDetailId == detail.DetailId
                                            && id.ProductId == detail.ProductId);
                                    if (returnInvoiceDetail == null) {
                                        returnInvoiceDetail = new InvoiceDetail {
                                            Piece = Convert.ToInt32(orderNoteDetail.Quantity * -1),
                                            OrderDetailId = null,
                                            Price = orderDetail.UnitPrice,
                                            ProductId = detail.ProductId,
                                            Active = true,
                                            ExportDetailId = detail.DetailId,
                                            InvoiceId = invoice.InvoiceId,
                                            ModifiedDate = DateTime.Now,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            Note = "Trả hàng " + orderNoteDetail.OrderNote.NoteNumber,
                                            NoteDetailId = orderNoteDetail.NoteDetailId,
                                        };
                                        listDetail.Add(returnInvoiceDetail);
                                    }
                                    else {
                                        returnInvoiceDetail.NoteDetailId = orderNoteDetail.NoteDetailId;
                                    }
                                }
                            }
                        }

                        //a += vfi.SaveChanges();
                    }
                }

                var a = AddInvoiceDetail(listDetail);
                return Json(a);
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult TaoLienKetSx2() {
            try {

                using (var vfi = new tammaContext()) {
                    //foreach (var inv in vfi.Production2Inventory)
                    //{
                    //    var productionSection =
                    //        vfi.ProductionSections.Where(
                    //            ps => ps.ProductId == inv.ProductId && ps.SectionId == inv.SectionId)
                    //           .OrderBy(ps => ps.SectionIndex)
                    //           .FirstOrDefault();
                    //    inv.ProductionSectionId = productionSection.ProductionSectionId;
                    //}
                    var a = vfi.SaveChanges();
                    return Json(a);
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
        }

        int AddInvoiceDetail(List<InvoiceDetail> list) {
            using (var vfi = new tammaContext()) {
                vfi.InvoiceDetails.AddRange(list);
                return vfi.SaveChanges();
            }
        }

        #region material use

        [HttpPost]
        public ActionResult DestroyMaterialUseByDate(string materialUseNumber) {
            if (string.IsNullOrWhiteSpace(materialUseNumber))
                return Json(9);
            try {
                var i = 0;
                using (var vfi = new tammaContext()) {
                    var materialUse = vfi.MaterialUseInShifts.FirstOrDefault(mu => mu.UsedCode.Equals(materialUseNumber));
                    if (materialUse == null)
                        return Json(8);
                    if (materialUse.Status != (byte)MyUtilities.Transaction.Status.Approved)
                        return Json(6);
                    foreach (var useDetail in materialUse.MaterialUseDetails) {
                        var materialOnMachine =
                            vfi.MaterialInvOnMachines.FirstOrDefault(
                                mim =>
                                mim.MachineId == useDetail.MachineId &&
                                mim.MaterialInvId == useDetail.MaterialInvId);
                        if (materialOnMachine == null)
                            return Json(7);
                        var materialOnMachinePeriod =
                            vfi.MaterialInvOnMachinePeriods.FirstOrDefault(
                                mimp =>
                                mimp.MachineId == useDetail.MachineId &&
                                mimp.MaterialInvId == useDetail.MaterialInvId &&
                                mimp.Quantity == (useDetail.EditQuantity + useDetail.EditQuantity2) &&
                                mimp.PeriodDate.Day == materialUse.UsedDate.Day &&
                                mimp.PeriodDate.Month == materialUse.UsedDate.Month &&
                                mimp.PeriodDate.Year == materialUse.UsedDate.Year &&
                                mimp.LastQuantity < mimp.EarlyQuantity);
                        if (materialOnMachinePeriod == null)
                            continue;
                        materialOnMachine.TotalQuantity += materialOnMachinePeriod.Quantity;
                        vfi.MaterialInvOnMachinePeriods.Remove(materialOnMachinePeriod);
                        i += vfi.SaveChanges();
                    }
                    materialUse.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    vfi.SaveChanges();
                    return Json(i);
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }
        #endregion

        #region Production

        [HttpPost]
        public ActionResult ChangeProductionProduct(string transactionCode, int oldProduct, int newProduct, int machineId) {

            //var a = 0;
            try {
                using (var vfi = new tammaContext()) {

                    var sx1Detail = (from id in vfi.ImportFormSX1Detail
                                     where
                                         id.ImportFormSX1.TransactionCode.Equals(transactionCode) &&
                                         id.MachineId == machineId && id.ProductId == oldProduct
                                     select id).FirstOrDefault();
                    if (sx1Detail == null)
                        return Json(9);
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == newProduct);
                    if (product == null)
                        return Json(9);
                    sx1Detail.ProductId = newProduct;
                    sx1Detail.ProductWeight = product.ProductionWeight ?? 0;
                    var importQuantity = (sx1Detail.Number1 + sx1Detail.Number2 + sx1Detail.Processing1 +
                                          sx1Detail.Processing2);
                    var transactionDetails = from td in vfi.TransactionDetails
                                             where td.Transaction.TransactionCode.Equals(transactionCode) &&
                                                   td.ReferenceId == oldProduct
                                             select td;
                    var isNew = false;
                    foreach (var detail in transactionDetails) {
                        if (detail.Quantity == importQuantity) {
                            detail.ReferenceId = newProduct;
                            if (detail.Transaction.WarehouseIssueId != null &&
                                detail.Transaction.WarehouseReceiptId != 1) {
                                if (detail.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved) {
                                    var oldInventory =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                            pi.ProductId == oldProduct &&
                                            pi.WarehouseId == detail.Transaction.WarehouseReceiptId);
                                    oldInventory.TotalQty -= detail.Quantity;

                                    var newInventory =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                            pi.ProductId == newProduct &&
                                            pi.WarehouseId == detail.Transaction.WarehouseReceiptId);
                                    if (newInventory == null) {
                                        newInventory = new ProductInventory {
                                            Active = true,
                                            ModifiedDate = DateTime.Now,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            ProductId = newProduct,
                                            Status = 0,
                                            TotalQty = detail.Quantity,
                                            WarehouseId = detail.Transaction.WarehouseReceiptId.Value,
                                            AvailableQty = 0,
                                            UnavailableQty = 0,
                                        };
                                        vfi.ProductInventories.Add(newInventory);
                                    }
                                    else {
                                        newInventory.TotalQty += detail.Quantity;
                                    }
                                }
                            }
                        }
                        else {
                            detail.Quantity = detail.Quantity - importQuantity;
                            var transactionDetail = new TransactionDetail {
                                TransactionId = detail.TransactionId,
                                ReferenceId = newProduct,
                                Quantity = importQuantity,
                                Active = detail.Active,
                                MoP = detail.MoP,
                                Note = detail.Note,
                                Price = detail.Price,
                                ModifiedDate = detail.ModifiedDate,
                                ModifiedUser = detail.ModifiedUser,
                                QuantityKg = detail.QuantityKg,
                                UnitMeasure = detail.UnitMeasure,
                            };
                            vfi.TransactionDetails.Add(transactionDetail);
                            isNew = true;
                            if (detail.Transaction.WarehouseIssueId != null &&
                                detail.Transaction.WarehouseReceiptId != 1) {
                                if (detail.Transaction.Status == (byte)MyUtilities.Transaction.Status.Approved) {
                                    var oldInventory =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                            pi.ProductId == oldProduct &&
                                            pi.WarehouseId == detail.Transaction.WarehouseReceiptId);
                                    oldInventory.TotalQty -= importQuantity;

                                    var newInventory =
                                        vfi.ProductInventories.FirstOrDefault(
                                            pi =>
                                            pi.ProductId == newProduct &&
                                            pi.WarehouseId == detail.Transaction.WarehouseReceiptId);
                                    if (newInventory == null) {
                                        newInventory = new ProductInventory {
                                            Active = true,
                                            ModifiedDate = DateTime.Now,
                                            ModifiedUser = HttpContext.User.Identity.Name,
                                            ProductId = newProduct,
                                            Status = 0,
                                            TotalQty = importQuantity,
                                            WarehouseId = detail.Transaction.WarehouseReceiptId.Value,
                                            AvailableQty = 0,
                                            UnavailableQty = 0,
                                        };
                                        vfi.ProductInventories.Add(newInventory);
                                    }
                                    else {
                                        newInventory.TotalQty += importQuantity;
                                    }
                                }
                            }

                        }
                    }
                    var productPeriods = from pip in vfi.ProductInventoryPeriods
                                         where
                                             pip.Transaction.TransactionCode.Equals(transactionCode) &&
                                             pip.ProductId == oldProduct
                                         select pip;
                    foreach (var period in productPeriods) {
                        if (!isNew)
                            period.ProductId = newProduct;
                        else {
                            var newPeriod = new ProductInventoryPeriod {
                                ProductId = newProduct,
                                EarlyPeriodQuantity = period.EarlyPeriodQuantity,
                                Quantity = importQuantity,
                                LastPeriodQuantity = period.LastPeriodQuantity,
                                PeriodDate = period.PeriodDate,
                                TransactionId = period.TransactionId,
                                WarehouseId = period.WarehouseId,
                                UnitPrice = period.UnitPrice,
                                UnitMeasure = period.UnitMeasure,
                                ModifiedUser = period.ModifiedUser,
                                ModifiedDate = period.ModifiedDate,
                            };
                            period.Quantity -= importQuantity;
                            var newInventory =
                                vfi.ProductInventories.FirstOrDefault(
                                    pi => pi.ProductId == period.ProductId && pi.WarehouseId
                                          == period.WarehouseId);
                            if (newInventory == null) continue;
                            if (period.EarlyPeriodQuantity < period.LastPeriodQuantity) {
                                period.LastPeriodQuantity -= importQuantity;
                                newPeriod.LastPeriodQuantity = newInventory.TotalQty;
                            }
                            else {
                                period.EarlyPeriodQuantity -= importQuantity;
                                newPeriod.EarlyPeriodQuantity = newInventory.TotalQty - importQuantity;
                            }
                            vfi.ProductInventoryPeriods.Add(newPeriod);
                        }
                    }
                    vfi.SaveChanges();
                    return Json("Oke");
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }

        [HttpPost]
        public ActionResult ChangeExportSaleProduct(string invoiceNumber, int oldProduct, int newProduct) {
            //var a = 0;
            try {
                using (var vfi = new tammaContext()) {
                    var invoice = vfi.Invoices.FirstOrDefault(i => i.InvoiceNumber.Equals(invoiceNumber));
                    if (invoice == null)
                        return Json(9);
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == newProduct);
                    if (product == null)
                        return Json(9);
                    var exportDetail =
                        invoice.ExportFormTP_KD.ExportFormTP_KDDetail.FirstOrDefault(ed => ed.ProductId == oldProduct);
                    if (exportDetail == null)
                        return Json(9);
                    exportDetail.ProductId = newProduct;
                    var transactionDetails = from td in vfi.TransactionDetails
                                             where
                                                 td.Transaction.TransactionCode.Equals(
                                                     invoice.ExportFormTP_KD.TransactionCode) &&
                                                 td.ReferenceId == oldProduct
                                             select td;
                    var productInvOldExport =
                        vfi.ProductInventories.FirstOrDefault(
                            pi => pi.ProductId == oldProduct && pi.WarehouseId == MyUtilities.Warehouse.Finish);
                    var productInvOldImport =
                        vfi.ProductInventories.FirstOrDefault(
                            pi => pi.ProductId == oldProduct && pi.WarehouseId == MyUtilities.Warehouse.Business);
                    var productInNewExport =
                        vfi.ProductInventories.FirstOrDefault(
                            pi => pi.ProductId == newProduct && pi.WarehouseId == MyUtilities.Warehouse.Finish);
                    var productInvNewImport =
                        vfi.ProductInventories.FirstOrDefault(
                            pi => pi.ProductId == newProduct && pi.WarehouseId == MyUtilities.Warehouse.Business);
                    foreach (var detail in transactionDetails) {
                        var periods =
                            vfi.ProductInventoryPeriods.Where(
                                pid => pid.TransactionId == detail.TransactionId && pid.ProductId == detail.ReferenceId);
                    }
                    vfi.SaveChanges();
                    return Json("Oke");
                }
            }
            catch (Exception) {
                return Json(-1);
            }
        }
        #endregion

        #region db system monitor
        [GridAction]
        public ActionResult SelectMonitor() {
            var model = new List<DbSystemMonitorModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var products = vfi.Products.Where(p => p.Active && p.Customer.IsMonitor == true);
                    var productionProducts = (from id in vfi.ImportFormSX1Detail
                                              where id.Number1 + id.Number2 > 0
                                              //&&id.ImportFormSX1.
                                              select id.ProductId).ToList();
                    var productsByType = products.Where(p => p.Productivity == null || p.Productivity == 0);
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.Production1,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = products.Where(p => p.ProductionRate == null || p.ProductionRate == 0);
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.ProductionRate,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where
                                     p.ProductionProcesses.FirstOrDefault(
                                         pp => pp.WarehouseId == MyUtilities.Warehouse.Cnc && pp.IsNecessary) != null &&
                                     (p.CncProductivity == null || p.CncProductivity == 0)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.Cnc,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where
                                     (p.Diameter == null || p.Diameter == 0 || p.Diameter == 1) &&
                                     (p.KnifeCut == null || p.KnifeCut == 0 || p.KnifeCut == 1)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.ProductDesign,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where
                                     p.ProductionProcesses.FirstOrDefault(
                                         pp => pp.WarehouseId == MyUtilities.Warehouse.Production2 && pp.IsNecessary) !=
                                     null &&
                                     p.ProductionSections.FirstOrDefault(
                                         ps => ps.Active == true && ps.Productivity > 0) == null
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.Production2,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where (p.UnitPrice == null || p.UnitPrice == 0) &&
                                           productionProducts.Contains(p.ProductId)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.ProductionNoUnitPrice,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where
                                     p.ProductionProcesses.FirstOrDefault(
                                         pp => pp.WarehouseId == MyUtilities.Warehouse.Plating && pp.IsNecessary) !=
                                     null &&
                                     p.ProductionPlatings.FirstOrDefault(
                                         ps => ps.Active && ps.PlatingDay > 0) == null
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.Plating,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }

                    productsByType = from p in products
                                     where
                                     !p.ProductionTools.Any(pt => pt.Active) &&
                                     productionProducts.Contains(p.ProductId)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.ProductionTool,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where
                                     !p.ProductionFuels.Any(pt => pt.Active) &&
                                     productionProducts.Contains(p.ProductId)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.ProductionFuel,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }

                    productsByType = from p in products
                                     where
                                     !p.ProductionMaterials.Any(pt => pt.Active) &&
                                     productionProducts.Contains(p.ProductId)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.ProductionMaterial,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    var warehouses = MyUtilities.Warehouse.GetWarehouseId_Process();
                    var productIds = products.Select(p => p.ProductId).ToList();
                    var productInvs = (from pi in vfi.ProductInventories
                                       where warehouses.Contains(pi.WarehouseId) &&
                                             productIds.Contains(pi.ProductId) &&
                                             pi.TotalQty > 0 &&
                                             pi.Product.ProductionProcesses.FirstOrDefault(
                                                 pp => pp.WarehouseId == pi.WarehouseId) == null
                                       select pi.ProductId).Distinct().Count();
                    if (productInvs > 0) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.ProductionProcesses,
                            Count = productInvs
                        };
                        model.Add(entity);
                    }

                    var productCount = (from p in products
                                        where p.ProductName.Contains("1") || p.DesignNo.Contains("1")
                                        select p.ProductId).Count();
                    if (productCount > 0) {
                        var entity = new DbSystemMonitorModel {
                            Type = (int)MyUtilities.Monitor.Type.ProductName,
                            Count = productCount
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("Select", ex.Message);
            }
            return View(new GridModel(model));
        }
        [GridAction]
        public ActionResult SelectMonitorDetail(int type) {
            var model = new List<MonitorDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var products =
                        vfi.Products.Where(p => p.Active && p.Customer.IsMonitor == true)
                           .OrderBy(p => p.Customer.CustomerCode)
                           .ThenBy(p => p.ProductCode);
                    switch (type) {
                        case (int)MyUtilities.Monitor.Type.Production1:
                            //var productsByType = products.Where(p => p.Productivity == null || p.Productivity == 0);
                            var productionProducts = (from id in vfi.ImportFormSX1Detail
                                                      where id.Number1 + id.Number2 > 0
                                                      //&&id.ImportFormSX1.
                                                      select id.ProductId).ToList();
                            var productsByType = from p in products
                                                 where (p.Productivity == null || p.Productivity == 0) &&
                                                       productionProducts.Contains(p.ProductId)

                                                 select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Monitor.Type.Cnc:
                            productsByType = from p in products
                                             where
                                                 p.ProductionProcesses.FirstOrDefault(
                                                     pp => pp.WarehouseId == MyUtilities.Warehouse.Cnc && pp.IsNecessary) !=
                                                 null &&
                                                 (p.CncProductivity == null || p.CncProductivity == 0)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Monitor.Type.Production2:
                            productsByType = from p in products
                                             where
                                                 p.ProductionProcesses.FirstOrDefault(
                                                     pp =>
                                                     pp.WarehouseId == MyUtilities.Warehouse.Production2 &&
                                                     pp.IsNecessary) != null &&
                                                 p.ProductionSections.FirstOrDefault(
                                                     ps => ps.Active == true && ps.Productivity > 0) == null
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                foreach (var productionSection in product.ProductionSections) {
                                    detail.Value += productionSection.Section.SectionName + ",";
                                }
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Monitor.Type.Plating:
                            productsByType = from p in products
                                             where
                                                 p.ProductionProcesses.FirstOrDefault(
                                                     pp =>
                                                     pp.WarehouseId == MyUtilities.Warehouse.Plating &&
                                                     pp.IsNecessary) != null &&
                                                 p.ProductionPlatings.FirstOrDefault(
                                                     ps => ps.Active && ps.PlatingDay > 0) == null
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                foreach (var productionPlating in product.ProductionPlatings) {
                                    detail.Value += productionPlating.PlatingName + ",";
                                }

                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Monitor.Type.ProductionNoUnitPrice:
                            productionProducts = (from id in vfi.ImportFormSX1Detail
                                                  where id.Number1 + id.Number2 > 0
                                                  //&&id.ImportFormSX1.
                                                  select id.ProductId).ToList();
                            productsByType = from p in products
                                             where (p.UnitPrice == null || p.UnitPrice == 0) &&
                                                   productionProducts.Contains(p.ProductId)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Monitor.Type.ProductionRate:
                            productionProducts = (from id in vfi.ImportFormSX1Detail
                                                  where id.Number1 + id.Number2 > 0
                                                  //&&id.ImportFormSX1.
                                                  select id.ProductId).ToList();
                            productsByType = from p in products
                                             where (p.ProductionRate == null || p.ProductionRate == 0) &&
                                                   productionProducts.Contains(p.ProductId)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Monitor.Type.ProductionFuel:
                            productionProducts = (from id in vfi.ImportFormSX1Detail
                                                  where id.Number1 + id.Number2 > 0
                                                  //&&id.ImportFormSX1.
                                                  select id.ProductId).ToList();
                            productsByType = from p in products
                                             where
                                                 !p.ProductionFuels.Any(pt => pt.Active) &&
                                                   productionProducts.Contains(p.ProductId)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Monitor.Type.ProductionTool:
                            productionProducts = (from id in vfi.ImportFormSX1Detail
                                                  where id.Number1 + id.Number2 > 0
                                                  //&&id.ImportFormSX1.
                                                  select id.ProductId).ToList();
                            productsByType = from p in products
                                             where
                                                 !p.ProductionTools.Any(pt => pt.Active) &&
                                                   productionProducts.Contains(p.ProductId)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Monitor.Type.ProductDesign:
                            productsByType = from p in products
                                             where
                                                 (p.Diameter == null || p.Diameter == 0 || p.Diameter == 1) &&
                                                 (p.KnifeCut == null || p.KnifeCut == 0 || p.KnifeCut == 1)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                if (product.Diameter == null || product.Diameter == 0 || product.Diameter == 1)
                                    detail.Value += "Đường kính,";
                                if (product.KnifeCut == null || product.KnifeCut == 0 || product.KnifeCut == 1)
                                    detail.Value += "Dao cắt,";
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Monitor.Type.ProductName:
                            productsByType = from p in products
                                             where p.ProductName.Contains("1") || p.DesignNo.Contains("1")
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Monitor.Type.ProductionMaterial:
                            productionProducts = (from id in vfi.ImportFormSX1Detail
                                                  where id.Number1 + id.Number2 > 0
                                                  //&&id.ImportFormSX1.
                                                  select id.ProductId).ToList();
                            productsByType = from p in products
                                             where
                                                 !p.ProductionMaterials.Any(pt => pt.Active) &&
                                                   productionProducts.Contains(p.ProductId)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Monitor.Type.ProductionProcesses:
                            var warehouses = MyUtilities.Warehouse.GetWarehouseId_Process();
                            var productIds = products.Select(p => p.ProductId).ToList();
                            var productInvs = from pi in vfi.ProductInventories
                                              where warehouses.Contains(pi.WarehouseId) &&
                                                    productIds.Contains(pi.ProductId) &&
                                                    pi.TotalQty > 0 &&
                                                    pi.Product.ProductionProcesses.FirstOrDefault(
                                                        pp => pp.WarehouseId == pi.WarehouseId) == null
                                              select pi;
                            productIds = productInvs.Select(pi => pi.ProductId).Distinct().ToList();
                            productsByType = products.Where(p => productIds.Contains(p.ProductId));

                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                var productInvsById = productInvs.Where(p => p.ProductId == product.ProductId);
                                foreach (var productInventory in productInvsById) {
                                    detail.Value += productInventory.Warehouse.ShortName + ",";
                                }
                                model.Add(detail);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMonitorDetail", ex.Message);
            }
            return View(new GridModel(model));
        }


        [GridAction]
        public ActionResult SelectWeightMonitor() {
            var model = new List<DbSystemMonitorModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var products = vfi.Products.Where(p => p.Active && p.Customer.IsMonitor == true);
                    var productionProducts = (from id in vfi.ImportFormSX1Detail
                                              where id.Number1 + id.Number2 > 0 &&
                                                    id.ProductWeight > 0
                                              //&&id.ImportFormSX1.
                                              select id.ProductId).ToList();
                    var productsByType =
                        products.Where(
                            p => (p.ProductionWeight == null || p.ProductionWeight == 0 || p.ProductionWeight == 1) &&
                                 productionProducts.Contains(p.ProductId));
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = MyUtilities.Warehouse.Production1,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where
                                         p.ProductionProcesses.FirstOrDefault(
                                             pp => pp.WarehouseId == MyUtilities.Warehouse.Cnc && pp.IsNecessary) !=
                                         null &&
                                         (p.CncWeight == null || p.CncWeight == 0 || p.CncWeight == 1 ||
                                          p.CncWeight > p.ProductionWeight)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = MyUtilities.Warehouse.Cnc,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where
                                         p.ProductionProcesses.FirstOrDefault(
                                             pp => pp.WarehouseId == MyUtilities.Warehouse.Production2 && pp.IsNecessary) !=
                                         null &&
                                         (p.Production2Weight == null || p.Production2Weight == 0 ||
                                          p.Production2Weight == 1 || p.Production2Weight > p.ProductionWeight)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = MyUtilities.Warehouse.Production2,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where
                                         p.ProductionProcesses.FirstOrDefault(
                                             pp =>
                                             pp.WarehouseId == MyUtilities.Warehouse.HeatTreatment && pp.IsNecessary) !=
                                         null &&
                                         (p.HeatTreatmentWeight == null || p.HeatTreatmentWeight == 0 ||
                                          p.HeatTreatmentWeight == 1 || p.HeatTreatmentWeight > p.ProductionWeight)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = MyUtilities.Warehouse.HeatTreatment,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where
                                         p.ProductionProcesses.FirstOrDefault(
                                             pp =>
                                             pp.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment && pp.IsNecessary) !=
                                         null &&
                                         (p.SurfaceTreatmentWeight == null || p.SurfaceTreatmentWeight == 0 ||
                                          p.SurfaceTreatmentWeight == 1 || p.SurfaceTreatmentWeight > p.ProductionWeight)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = MyUtilities.Warehouse.SurfaceTreatment,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where
                                         p.ProductionProcesses.FirstOrDefault(
                                             pp =>
                                             pp.WarehouseId == MyUtilities.Warehouse.WaitingPlating && pp.IsNecessary) !=
                                         null &&
                                         (p.WaitingPlatingWeight == null || p.WaitingPlatingWeight == 0 || p.WaitingPlatingWeight == 1 ||
                                          p.WaitingPlatingWeight > p.ProductionWeight)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = MyUtilities.Warehouse.Plating,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                    productsByType = from p in products
                                     where
                                         (p.QcWeight == null || p.QcWeight == 0 || p.QcWeight == 1 ||
                                          p.QcWeight > p.ProductionWeight)
                                     select p;
                    if (productsByType.Any()) {
                        var entity = new DbSystemMonitorModel {
                            Type = MyUtilities.Warehouse.QcA,
                            Count = productsByType.Count()
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("Select", ex.Message);
            }
            return View(new GridModel(model));
        }
        [GridAction]
        public ActionResult SelectMonitorWeightDetail(int type) {
            var model = new List<MonitorDetailModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var products =
                        vfi.Products.Where(p => p.Active && p.Customer.IsMonitor == true)
                           .OrderBy(p => p.Customer.CustomerCode)
                           .ThenBy(p => p.ProductCode);
                    switch (type) {
                        case (int)MyUtilities.Warehouse.Id.Production1:

                            var productionProducts = (from id in vfi.ImportFormSX1Detail
                                                      where id.Number1 + id.Number2 > 0 &&
                                         id.ProductWeight > 0
                                                      //&&id.ImportFormSX1.
                                                      select id.ProductId).ToList();
                            var productsByType =
                                products.Where(
                                    p =>
                                    (p.ProductionWeight == null || p.ProductionWeight == 0 || p.ProductionWeight == 1) &&
                                    productionProducts.Contains(p.ProductId));
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Warehouse.Id.Cnc:
                            productsByType = from p in products
                                             where
                                                 p.ProductionProcesses.FirstOrDefault(
                                                     pp => pp.WarehouseId == MyUtilities.Warehouse.Cnc && pp.IsNecessary) !=
                                                 null &&
                                                 (p.CncWeight == null || p.CncWeight == 0 || p.CncWeight == 1 || p.CncWeight > p.ProductionWeight)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Warehouse.Id.Production2:
                            productsByType = from p in products
                                             where
                                                 p.ProductionProcesses.FirstOrDefault(
                                                     pp => pp.WarehouseId == MyUtilities.Warehouse.Production2 && pp.IsNecessary) !=
                                                 null &&
                                                 (p.Production2Weight == null || p.Production2Weight == 0 || p.Production2Weight == 1 || p.Production2Weight > p.ProductionWeight)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Warehouse.Id.HeatTreatment:
                            productsByType = from p in products
                                             where
                                                 p.ProductionProcesses.FirstOrDefault(
                                                     pp => pp.WarehouseId == MyUtilities.Warehouse.HeatTreatment && pp.IsNecessary) !=
                                                 null &&
                                                 (p.HeatTreatmentWeight == null || p.HeatTreatmentWeight == 0 || p.HeatTreatmentWeight == 1 || p.HeatTreatmentWeight > p.ProductionWeight)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Warehouse.Id.SurfaceTreatment:
                            productsByType = from p in products
                                             where
                                                 p.ProductionProcesses.FirstOrDefault(
                                                     pp => pp.WarehouseId == MyUtilities.Warehouse.SurfaceTreatment && pp.IsNecessary) !=
                                                 null &&
                                                 (p.SurfaceTreatmentWeight == null || p.SurfaceTreatmentWeight == 0 || p.SurfaceTreatmentWeight == 1 || p.SurfaceTreatmentWeight > p.ProductionWeight)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Warehouse.Id.WaitingPlating:
                            productsByType = from p in products
                                             where
                                                 p.ProductionProcesses.FirstOrDefault(
                                                     pp => pp.WarehouseId == MyUtilities.Warehouse.WaitingPlating && pp.IsNecessary) !=
                                                 null &&
                                                 (p.WaitingPlatingWeight == null || p.WaitingPlatingWeight == 0 || p.WaitingPlatingWeight == 1 || p.WaitingPlatingWeight > p.ProductionWeight)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;
                        case (int)MyUtilities.Warehouse.Id.QcA:
                            productsByType = from p in products
                                             where
                                                 (p.QcWeight == null || p.QcWeight == 0 || p.QcWeight == 1 || p.QcWeight > p.ProductionWeight)
                                             select p;
                            foreach (var product in productsByType) {
                                var detail = new MonitorDetailModel {
                                    Code = product.ProductCode,
                                    Id = product.ProductId,
                                    Type = product.Customer.CustomerCode
                                };
                                model.Add(detail);
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("(Detail", ex.Message);
            }
            return View(new GridModel(model));
        }


        public ActionResult SelectItemCardInfo(int? productId) {
            var itemcard = new ItemCardModel();
            if (productId == null || productId == 0)
                return Json(itemcard);
            try {
                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product != null) {
                        itemcard.ProductId = productId.Value;
                        itemcard.ProductCustomerCode = product.DesignNo;
                        itemcard.ProductName = product.ProductName;
                        var fuel = product.ProductionFuels.FirstOrDefault(pf => pf.Active && pf.Fuel.FuelName.Equals("Bao bì"));
                        if (fuel != null) {
                            itemcard.Quantity = fuel.Quota;
                            itemcard.FuelId = fuel.FuelId;
                            itemcard.FuelCode = fuel.Fuel.FuelFullCode;
                        }
                    }
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return Json(itemcard);
        }

        public ActionResult PrintItemCard(int productId, string name, string code, int quantity, string lot, string date, string employee, string note, int fuelId) {
            var model = new List<ItemCardModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null)
                        throw new AggregateException("Lỗi! Sản phẩm không tìm thấy");
                    var ci = new CultureInfo("vi-VN");
                    var cardDate = string.IsNullOrWhiteSpace(date)
                                         ? DateTime.Today
                                         : Convert.ToDateTime(date, ci);
                    var item = new ItemCardModel {
                        EmployeeName = employee,
                        Date = cardDate,
                        Lot = lot,
                        ProductName = name,
                        ProductCustomerCode = code,
                        Quantity = quantity,
                        Note = note,
                        ProductId = productId,
                        ProductCode = product.ProductCode
                    };
                    var fuel = vfi.ProductionFuels.FirstOrDefault(f => f.FuelId == fuelId && f.ProductId == item.ProductId);
                    if (fuel == null) {
                        fuel = new ProductionFuel {
                            Active = true,
                            FuelId = fuelId,
                            ProductId = productId,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Quota = quantity,
                            Note = note,
                        };
                        vfi.ProductionFuels.Add(fuel);
                    }
                    else {
                        if (!fuel.Active)
                            fuel.Active = true;
                        else
                            fuel.Quota = quantity;
                    }
                    vfi.SaveChanges();
                    model.Add(item);
                    model.Add(item);
                    model.Add(item);
                    model.Add(item);
                    model.Add(item);
                    model.Add(item);
                }
                return PartialView("PageItemCard", model);
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return PartialView(null);
        }
        [GridAction]
        public ActionResult SelectProductionMonitor(string productCode, string fromDate, string toDate) {
            var model = new List<ProductionMonitorModel>();
            var ci = new CultureInfo("vi-VN");
            var fDate = string.IsNullOrWhiteSpace(fromDate)
                                 ? DateTime.Today
                                 : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                                 ? DateTime.Today
                                 : Convert.ToDateTime(toDate, ci);
            try {

                using (var vfi = new tammaContext()) {
                    var importSx1InDay = (from i in vfi.ImportFormSX1Detail
                                          where
                                              i.ImportFormSX1.MaterialUseDate >= fDate &&
                                              i.ImportFormSX1.MaterialUseDate <= tDate &&
                                              i.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                              i.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                              i.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                               .Transaction.Status ==
                                              (byte)MyUtilities.Transaction.Status.Approved
                                              && i.Product.ProductCode.Contains(productCode) &&
                                              i.Number1 + i.Number2 > 0
                                          select i);
                    var productIds = importSx1InDay.Select(id => id.ProductId).Distinct().ToList();
                    var tracks = vfi.RealProductions.Where(rp => productIds.Contains(rp.ProductId));
                    foreach (var productId in productIds) {
                        if (model.Count == 27)
                            ci = new CultureInfo("vi-VN");
                        var importById = importSx1InDay.Where(id => id.ProductId == productId);
                        var entity = new ProductionMonitorModel {
                            CustomerId = importById.FirstOrDefault().Product.CustomerId,
                            CustomerCode = importById.FirstOrDefault().Product.Customer.CustomerCode,
                            ProductId = importById.FirstOrDefault().ProductId,
                            ProductCode = importById.FirstOrDefault().Product.ProductCode,
                            ProductivityDesign = importById.FirstOrDefault().Product.Productivity ?? 0,
                            MissInfo = false,
                        };
                        var machineIds = importById.Select(id => id.MachineId.Value).Distinct().ToList();
                        entity.MachineCount = machineIds.Count;
                        var track =
                            tracks.Where(rp => rp.ProductId == productId && machineIds.Contains(rp.MachineId.Value)).ToList();
                        if (track.Count() != machineIds.Count)
                            entity.MissInfo = true;
                        if (machineIds.Count > 0) {
                            entity.ProductivityReal = track.Sum(t => t.TrackUpMachine.RealProductivity) /
                                                      machineIds.Count;
                            entity.ProductionQuantity = importById.Sum(id => (id.Number1 + id.Number2));
                            entity.AvgProductionQuantity = entity.ProductionQuantity * entity.ProductivityReal /
                                                         machineIds.Count;
                        }
                        model.Add(entity);
                    }
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionMonitor", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.CustomerCode).ThenBy(m => m.ProductCode)));
        }

        #endregion
        [GridAction]
        public ActionResult SelectWrongOrderProduct() {
            var model = new List<MonitorDetailModel>();
            using (var vfi = new tammaContext()) {
                var products = (from p in vfi.Products
                                where p.Active
                                select new {
                                    p.ProductId,
                                    p.ProductCode
                                }).ToList();
                var orderDetails = (from od in vfi.OrderDetails
                                    where od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                          od.Order.DueDate > MyUtilities.Sales.StartOrderReportDate &&
                                          od.Order.DueDate != null
                                    select new {
                                        od.ProductId,
                                        od.Order.DueDate,
                                        od.OrderQty,
                                    }).ToList();
                var requireOrders = (from od in vfi.OrderDetails
                                     where od.Order.Status != (byte)MyUtilities.Sales.Status.Cancel &&
                                     od.Order.Status != (byte)MyUtilities.Sales.Status.Completed &&
                                           od.Order.DueDate > MyUtilities.Sales.StartOrderReportDate &&
                                           od.Order.DueDate != null &&
                                           od.RequiedNumber > 0
                                     select new {
                                         od.ProductId,
                                         od.Order.DueDate,
                                         od.RequiedNumber,
                                     }).ToList();
                var allExportFinish = (from pip in vfi.ProductInventoryPeriods
                                       where pip.PeriodDate > MyUtilities.Sales.StartOrderReportDate &&
                                       pip.WarehouseId == MyUtilities.Warehouse.Finish &&
                                       ((pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Finish &&
                                       pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Business) ||
                                       (pip.Transaction.WarehouseIssueId == MyUtilities.Warehouse.Business &&
                                       pip.Transaction.WarehouseReceiptId == MyUtilities.Warehouse.Finish))
                                       select new {
                                           pip.ProductId,
                                           Quantity = pip.EarlyPeriodQuantity - pip.LastPeriodQuantity
                                       }).ToList();
                foreach (var p in products) {
                    var orderDetailsById = orderDetails.Where(od => od.ProductId == p.ProductId).ToList().Sum(od => od.OrderQty);
                    var requireById = requireOrders.Where(od => od.ProductId == p.ProductId).ToList().Sum(od => od.RequiedNumber);
                    var exportById = allExportFinish.Where(e => e.ProductId == p.ProductId).ToList().Sum(r => r.Quantity);
                    var diff = orderDetailsById - exportById - requireById;
                    if (diff != 0) {
                        var entity = new MonitorDetailModel {
                            Id = p.ProductId,
                            Code = p.ProductCode,
                            Value = (orderDetailsById - exportById) + " != " + requireById + " = " + diff
                        };
                        model.Add(entity);
                    }
                }
            }
            return View(new GridModel(model));
        }
        #endregion

        #region System testing

        [GridAction]
        public ActionResult SelectTestingProductInv(int customerId, string productCode) {
            if (customerId == -1)
                return View(new GridModel(new List<ProductInventoryModel>()));
            var model = new List<ProductInventoryModel>();

            try {
                using (var vfi = new tammaContext()) {
                    var productIds = !string.IsNullOrWhiteSpace(productCode)
                        ? vfi.Products.Where(x => x.ProductCode.Contains(productCode)).Select(x => x.ProductId).ToList()
                        : new List<int>();
                    var periods = vfi.SelectProductInventoryAndPeriods.Where(p =>
                        (customerId == 0 || p.CustomerId == customerId) &&
                        (!productIds.Any() || productIds.Contains(p.ProductId)))
                        //.OrderBy(p => p.CustomerCode)
                        //.ThenBy(p => p.ProductCode)
                        //.ThenBy(p => p.WarehouseName)
                        .ToList();
                    //if (!string.IsNullOrWhiteSpace(productCode))
                    //    periods = periods.Where(p => p.ProductCode.Contains(productCode)).ToList();
                    foreach (var period in periods) {
                        var entity = new ProductInventoryModel {
                            CustomerCode = period.CustomerCode,
                            ProductId = period.ProductId,
                            ProductCode = period.ProductCode,
                            WarehouseId = period.WarehouseId,
                            WarehouseName = period.WarehouseName,
                            TotalQty = period.TotalInv ?? 0,
                            AvailableQty = period.TotalPeriod ?? 0,
                            LotNumber = period.LotNumber
                        };
                        entity.Quantity = Math.Round(entity.TotalQty - entity.AvailableQty, 0);
                        if (entity.Quantity != 0)
                            model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTestingProductInv", ex.Message);
            }

            return View(new GridModel(model
                        .OrderBy(p => p.CustomerCode)
                        .ThenBy(p => p.ProductCode)
                        .ThenBy(p => p.WarehouseName)
                        ));
        }

        [GridAction]
        public ActionResult SelectTestingProductInvDetail(int? productId, int? warehouseId, string lotNumber) {
            var model = new List<ProductInventoryPeriodModel>();
            if (productId == null || productId == -1 || warehouseId == null || warehouseId == -1)
                return View(new GridModel(model));
            try {
                model = GetTestingProductInvDetail(productId.Value, 
                                                   warehouseId.Value, 
                                                   lotNumber == null 
                                                      ? string.Empty 
                                                      : lotNumber.ToString());
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTestingProductInvDetail", ex.Message);
            }

            return View(new GridModel(model));
        }

        List<ProductInventoryPeriodModel> GetTestingProductInvDetail(int productId, int warehouseId, string lotNumber) {
            var model = new List<ProductInventoryPeriodModel>();
            if (productId == null || productId == -1 || warehouseId == null || warehouseId == -1)
                return model;
            try {
                using (var vfi = new tammaContext()) {
                    var periods = vfi.ProductInventoryPeriods
                        .Where(p => p.ProductId == productId && 
                                    //p.WarehouseId == warehouseId && 
                                    p.LotNumber == lotNumber)
                        .OrderBy(p => p.PeriodDate)
                        //.Select(t => new {
                        //    t.ProductId,
                        //    t.ProductInventoryPeriodId,
                        //    t.WarehouseId,
                        //    t.TransactionId,
                        //    t.EarlyPeriodQuantity,
                        //    t.Quantity,
                        //    t.LastPeriodQuantity,
                        //    t.PeriodDate,
                        //    t.ModifiedDate,
                        //    t.ModifiedUser,
                        //    t.Transaction,
                        //})
                        .ToList();

                    var dupPeriods = periods
                        .GroupBy(p => new {
                            p.ProductId,
                            p.WarehouseId,
                            p.EarlyPeriodQuantity,
                            p.LastPeriodQuantity,
                            p.Quantity,
                            p.PeriodDate,
                            p.ModifiedDate,
                        })
                        .Where(group => group.Count() > 1)
                        .Select(grp => grp.Key)
                        .ToList();
                    foreach (var dup in dupPeriods) {
                        var periodsById = periods.Where(p => 
                            //p.TransactionId == dup.TransactionId &&
                            p.EarlyPeriodQuantity == dup.EarlyPeriodQuantity &&
                            p.LastPeriodQuantity == dup.LastPeriodQuantity &&
                            p.Quantity == dup.Quantity &&
                            p.PeriodDate == dup.PeriodDate &&
                            p.ModifiedDate == dup.ModifiedDate);
                        foreach (var period in periodsById) {
                            var entity = new ProductInventoryPeriodModel {
                                ProductInventoryPeriodId = period.ProductInventoryPeriodId,
                                Date = period.ModifiedDate,
                                Period = period.Quantity,
                                Last = period.LastPeriodQuantity,
                                Early = period.EarlyPeriodQuantity,
                                TransactionId = period.TransactionId,
                                WarehouseName = period.Warehouse.WarehouseName,
                            };

                            model.Add(entity);
                        }
                    }
                    model = model.OrderBy(t => t.TransactionId).ToList();
                }
            }
            catch (Exception ex) {
                var errorMessage = ex.Message;

                if (ex.InnerException != null) {
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;
                }

                ModelState.AddModelError("SelectTestingProductInvDetail", errorMessage);
            }
            return model;
        }

        [GridAction]
        public ActionResult DeleteDuplicatePeriod(int periodId) {
            try {
                using (var vfi = new tammaContext()) {
                    var period = vfi.ProductInventoryPeriods.FirstOrDefault(p => p.ProductInventoryPeriodId == periodId);
                    if (period != null) {

                        // 03/09/2026
                        var transaction = vfi.Transactions.Where(t => t.TransactionId == period.TransactionId)
                                                          .Select(t => new {
                                                              t.Status,
                                                              t.WarehouseReceiptId,
                                                              t.TransactionId,
                                                          }).FirstOrDefault();
                        var periodB = vfi.ProductInventoryPeriods.FirstOrDefault(t => t.TransactionId == transaction.TransactionId
                                                                                 && t.WarehouseId == transaction.WarehouseReceiptId
                                                                                 && t.ProductId == period.ProductId
                                                                                 && t.LotNumber == period.LotNumber
                                                                                 && t.Quantity == period.Quantity
                                                                                 && t.ProductInventoryPeriodId != periodId
                                                                                 );
                        if (periodB != null) {
                            vfi.ProductInventoryPeriods.Remove(periodB);
                        }

                        vfi.ProductInventoryPeriods.Remove(period);
                        vfi.SaveChanges();
                        return View(new GridModel(GetTestingProductInvDetail(period.ProductId, period.WarehouseId, period.LotNumber)));
                    }
                }
            }
            catch (Exception ex) {
                var errorMessage = ex.Message;
                if (ex.InnerException != null) {
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;
                }
                ModelState.AddModelError("DeleteDuplicatePeriod", errorMessage);
            }

            var model = new List<ProductInventoryPeriodModel>();
            return View(new GridModel(model));
        }

        // 03/09/2026
        [GridAction]
        public ActionResult SelectTestingTransactionDetail(int? transactionId) {
            var model = new List<TestingTransactionDetail>();
            if (transactionId == null)
                return View(new GridModel(model));
            try {
                model = GetTestingTransactionDetail(transactionId.Value);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTestingTransactionDetail", ex.Message);
            }

            return View(new GridModel(model));
        }

        List<TestingTransactionDetail> GetTestingTransactionDetail(int transactionId) {
            var model = new List<TestingTransactionDetail>();
            if (transactionId == null)
                return model;
            try {
                using (var vfi = new tammaContext()) {
                    var transactionDetailList = vfi.TransactionDetails.Where(t => t.TransactionId == transactionId).ToList();
                    foreach (var transaction in transactionDetailList) {
                        var entity = new TestingTransactionDetail {
                            TransactionDetailId = transaction.TransactionDetailId,
                            LotNumber = transaction.LotNumber,
                            Note = transaction.Note,
                            ProductId = transaction.ReferenceId ?? 0,
                            Quantity = transaction.Quantity,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                var errorMessage = ex.Message;

                if (ex.InnerException != null) {
                    errorMessage += " | Inner Exception: " + ex.InnerException.Message;
                }

                ModelState.AddModelError("SelectTestingTransactionDetail", errorMessage);
            }
            return model;
        }


        public ActionResult MakeProductChangeInventory(int productId, int warehouseId) {
            var a = 0;
            try {
                using (var vfi = new tammaContext()) {
                    var period = vfi.SelectProductInventoryAndPeriods.FirstOrDefault(p =>
                        p.TotalPeriod != p.TotalInv &&
                        p.ProductId == productId &&
                        p.WarehouseId == warehouseId);

                    if (period == null)
                        return Json(9);
                    if (period.TotalPeriod < 0)
                        return Json(8);

                    var productInvs = vfi.ProductInventories.Where(pi => pi.ProductInventoryId == period.ProductInventoryId);
                    if (!productInvs.Any()) {
                        productInvs = vfi.ProductInventories.Where(pi => pi.WarehouseId == warehouseId &&
                            pi.ProductId == productId).Take(1);
                    }
                    var totalDiff = Math.Round((period.TotalInv ?? 0) - (period.TotalPeriod ?? 0));
                    if (totalDiff == 0)
                        return Json(a);

                    foreach (var productInv in productInvs) {
                        if (productInv.TotalQty >= totalDiff) {
                            productInv.TotalQty -= totalDiff;
                            totalDiff = 0;
                            break;
                        }
                        else {
                            totalDiff -= productInv.TotalQty;
                            productInv.TotalQty = 0;
                        }
                    }
                    a = vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return Json(a);
        }



        [GridAction]
        public ActionResult SelectTestingMaterialInv() {
            var model = new List<MaterialInventoryModel>();

            try {
                using (var vfi = new tammaContext()) {

                    var periods = vfi.SelectMaterialInvAndPeriods.Where(p => p.PeriodQuantity != p.TotalQuantity)
                        .OrderBy(p => p.MaterialCode)
                        .ThenBy(p => p.VendorName)
                        .ThenBy(p => p.LotNumber)
                        .ToList();
                    foreach (var period in periods) {
                        var entity = new MaterialInventoryModel {
                            MaterialInventoryId = period.MaterialInventoryId,
                            MaterialCode = period.MaterialCode,
                            LotNumber = period.LotNumber,
                            VendorName = period.VendorName,
                            TotalQty = period.TotalQuantity,
                            TotalInv = period.PeriodQuantity ?? 0,
                        };
                        entity.Quantity = Math.Round(entity.TotalQty - entity.TotalInv, 2);
                        if (entity.Quantity != 0)
                            model.Add(entity);
                    }
                    var materialInvsUnfinish = vfi.MaterialInventories.Where(x => Math.Round(x.TotalQty, 2) == 0 && x.EndDate == null);
                    foreach (var materialInv in materialInvsUnfinish) {
                        materialInv.TotalQty = 0;
                        materialInv.TotalQtyKg = 0;
                        materialInv.EndDate = DateTime.Now;
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTestingMaterialInv", ex.Message);
            }

            return View(new GridModel(model));
        }

        public ActionResult MakeMaterialChangeInventory(int materialInvId) {
            var a = 0;
            try {
                using (var vfi = new tammaContext()) {
                    var period = vfi.SelectMaterialInvAndPeriods.FirstOrDefault(p =>
                         p.PeriodQuantity != p.TotalQuantity && p.MaterialInventoryId == materialInvId);

                    if (period == null)
                        return Json(9);
                    if (period.PeriodQuantity < 0)
                        return Json(8);

                    var invs = vfi.MaterialInventories.Where(pi => pi.MaterialInventoryId == materialInvId && pi.TotalQty > 0);
                    if (!invs.Any()) {
                        invs = vfi.MaterialInventories.Where(pi => pi.MaterialInventoryId == materialInvId).Take(1);
                    }
                    var totalDiff = Math.Round(period.TotalQuantity - (period.PeriodQuantity ?? 0.0), 2);
                    if (totalDiff == 0)
                        return Json(a);

                    foreach (var inv in invs) {
                        if (inv.TotalQty >= totalDiff) {
                            inv.TotalQty -= totalDiff;
                            totalDiff = 0;
                            break;
                        }
                        else {
                            totalDiff -= inv.TotalQty;
                            inv.TotalQty = 0;
                        }
                        if (Math.Round(inv.TotalQty, 2) == 0) {
                            inv.EndDate = DateTime.Now;
                        }
                    }
                    a = vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return Json(a);
        }
        [GridAction]
        public ActionResult SelectTestingMaterialInvOnMachine() {
            var model = new List<MaterialInvOnMachineModel>();

            try {
                using (var vfi = new tammaContext()) {

                    var periods = vfi.SelectMaterialInvOnMachineAndPeriods.Where(p => p.PeriodQuantity != p.TotalQuantity)
                        .OrderBy(p => p.MachineName)
                        .ThenBy(p => p.MaterialCode)
                        .ThenBy(p => p.VendorName)
                        .ThenBy(p => p.LotNumber)
                        .ToList();
                    foreach (var period in periods) {
                        var entity = new MaterialInvOnMachineModel {
                            MaterialInvId = period.MaterialInventoryId,
                            MaterialDesign = period.MaterialCode,
                            LotNumber = period.LotNumber,
                            VendorCode = period.VendorName,
                            MachineId = period.MachineId,
                            MachineName = period.MachineName,
                            EarlyQuantity = period.TotalQuantity,
                            LastQuantity = period.PeriodQuantity ?? 0,
                        };
                        entity.AssignQuantity = Math.Round(entity.EarlyQuantity - entity.LastQuantity, 2);
                        if (entity.AssignQuantity != 0)
                            model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTestingMaterialInvOnMachine", ex.Message);
            }

            return View(new GridModel(model));
        }

        public ActionResult MakeMaterialOnMachineChangeInventory(int materialInvId, int machineId) {
            var a = 0;
            try {
                using (var vfi = new tammaContext()) {
                    var period = vfi.SelectMaterialInvOnMachineAndPeriods.FirstOrDefault(p =>
                         p.PeriodQuantity != p.TotalQuantity
                         && p.MaterialInventoryId == materialInvId && p.MachineId == machineId);

                    if (period == null)
                        return Json(9);
                    if (period.PeriodQuantity < 0)
                        return Json(8);

                    var invs = vfi.MaterialInvOnMachines.Where(pi => pi.MaterialInvId == materialInvId && pi.MachineId == machineId
                        && pi.TotalQuantity > 0);
                    if (!invs.Any()) {
                        invs = vfi.MaterialInvOnMachines.Where(pi => pi.MaterialInvId == materialInvId && pi.MachineId == machineId)
                            .Take(1);
                    }
                    var totalDiff = Math.Round(period.TotalQuantity - (period.PeriodQuantity ?? 0.0), 2);
                    if (totalDiff == 0)
                        return Json(a);

                    foreach (var inv in invs) {
                        if (inv.TotalQuantity >= totalDiff) {
                            inv.TotalQuantity -= totalDiff;
                            totalDiff = 0;
                            break;
                        }
                        else {
                            totalDiff -= inv.TotalQuantity;
                            inv.TotalQuantity = 0;
                        }
                    }
                    a = vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return Json(a);
        }


        [GridAction]
        public ActionResult SelectTestingFuelInv() {
            var model = new List<FuelInventoryModel>();

            try {
                using (var vfi = new tammaContext()) {

                    var periods = vfi.SelectFuelInvAndPeriods.Where(p => p.PeriodQuantity != p.TotalQuantity)
                        .OrderBy(p => p.FuelFullCode)
                        .ThenBy(p => p.VendorName)
                        .ThenBy(p => p.LotNumber)
                        .ToList();
                    foreach (var period in periods) {
                        var entity = new FuelInventoryModel {
                            FuelInvId = period.FuelInvId,
                            FuelCode = period.FuelFullCode,
                            LotNumber = period.LotNumber,
                            VendorCode = period.VendorName,
                            TotalQuantity = period.TotalQuantity,
                            FirstImportQuantity = period.PeriodQuantity ?? 0,
                        };
                        entity.Quantity = Math.Round(entity.TotalQuantity - entity.FirstImportQuantity, 2);
                        if (entity.Quantity != 0)
                            model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTestingFuelInv", ex.Message);
            }

            return View(new GridModel(model));
        }

        public ActionResult MakeFuelChangeInventory(int fuelInvId) {
            var a = 0;
            try {
                using (var vfi = new tammaContext()) {
                    var period = vfi.SelectFuelInvAndPeriods.FirstOrDefault(p =>
                         p.PeriodQuantity != p.TotalQuantity && p.FuelInvId == fuelInvId);

                    if (period == null)
                        return Json(9);
                    if (period.PeriodQuantity < 0)
                        return Json(8);

                    var invs = vfi.FuelInventories.Where(pi => pi.FuelInvId == fuelInvId && pi.TotalQuantity > 0);
                    if (!invs.Any()) {
                        invs = vfi.FuelInventories.Where(pi => pi.FuelInvId == fuelInvId).Take(1);
                    }
                    var totalDiff = Math.Round(period.TotalQuantity - (period.PeriodQuantity ?? 0.0), 2);
                    if (totalDiff == 0)
                        return Json(a);

                    foreach (var inv in invs) {
                        if (inv.TotalQuantity >= totalDiff) {
                            inv.TotalQuantity -= totalDiff;
                            totalDiff = 0;
                            break;
                        }
                        else {
                            totalDiff -= inv.TotalQuantity;
                            inv.TotalQuantity = 0;
                        }
                    }
                    a = vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return Json(a);
        }


        [GridAction]
        public ActionResult SelectTestingToolInv() {
            var model = new List<ToolInventoryModel>();

            try {
                using (var vfi = new tammaContext()) {

                    var periods = vfi.SelectToolInvAndPeriods.Where(p => p.PeriodQuantity != p.TotalQuantity)
                        .OrderBy(p => p.ToolFullCode)
                        .ThenBy(p => p.VendorName)
                        .ThenBy(p => p.LotNumber)
                        .ToList();
                    foreach (var period in periods) {
                        var entity = new ToolInventoryModel {
                            ToolInvId = period.ToolInvId,
                            ToolFullCode = period.ToolFullCode,
                            LotNumber = period.LotNumber,
                            VendorCode = period.VendorName,
                            TotalQuantity = period.TotalQuantity,
                            FirstImportQuantity = period.PeriodQuantity ?? 0,
                        };
                        entity.Quantity = Math.Round(entity.TotalQuantity - entity.FirstImportQuantity, 2);
                        if (entity.Quantity != 0)
                            model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTestingFuelInv", ex.Message);
            }

            return View(new GridModel(model));
        }

        public ActionResult MakeToolChangeInventory(int toolInvId) {
            var a = 0;
            try {
                using (var vfi = new tammaContext()) {
                    var period = vfi.SelectToolInvAndPeriods.FirstOrDefault(p =>
                         p.PeriodQuantity != p.TotalQuantity && p.ToolInvId == toolInvId);

                    if (period == null)
                        return Json(9);
                    if (period.PeriodQuantity < 0)
                        return Json(8);

                    var invs = vfi.ToolInventories.Where(pi => pi.ToolInvId == toolInvId && pi.TotalQuantity > 0);
                    if (!invs.Any()) {
                        invs = vfi.ToolInventories.Where(pi => pi.ToolInvId == toolInvId).Take(1);
                    }
                    var totalDiff = Math.Round(period.TotalQuantity - (period.PeriodQuantity ?? 0.0), 2);
                    if (totalDiff == 0)
                        return Json(a);

                    foreach (var inv in invs) {
                        if (inv.TotalQuantity >= totalDiff) {
                            inv.TotalQuantity -= totalDiff;
                            totalDiff = 0;
                            break;
                        }
                        else {
                            totalDiff -= inv.TotalQuantity;
                            inv.TotalQuantity = 0;
                        }
                    }
                    a = vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return Json(a);
        }

        #endregion

    }

}
