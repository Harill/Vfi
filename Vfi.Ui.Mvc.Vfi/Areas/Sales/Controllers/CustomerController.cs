using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
//using Vfi.Client.Module.Sales.Interfaces;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Ui.Mvc.Vfi.Areas.Sales.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Utilities;
using Customer = Vfi.Server.Core.DataModel.BaseEntities.Customer;
using System.Threading;

namespace Vfi.Ui.Mvc.Vfi.Areas.Sales.Controllers {
    public class CustomerController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ICustomerService _customerService;
        [InjectionConstructor]
        public CustomerController(IUnitOfWork unitOfWork
            //, ICustomerService customerService
            ) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            //if (customerService == null) throw new ArgumentNullException("customerService");

            _unitOfWork = unitOfWork;
            //_customerService = customerService;
        }
        #region view
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
        // View
        public ActionResult CustomerManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            string flag = "hidden";
            if (MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name, MyUtilities.UserRole.SaleManagement)) {
                flag = "visible";
            }
            ViewData = GetPageConfigData();
            return View(new CustomerModel { PrintCustomerList = flag });
        }

        public ActionResult CustomerClassified() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        #endregion

        #region Customer

        public List<CustomerModel> GetAllCustomer(int customerId) {
            var models = new List<CustomerModel>();

            using (var vfi = new tammaContext()) {
                var customers = vfi.Customers.Where(c => customerId == 0 || c.CustomerId == customerId).ToList();
                var payType = vfi.CustomerPayTypes.ToList();
                foreach (var customer in customers) {
                    var entity = new CustomerModel {
                        CustomerId = customer.CustomerId,
                        CustomerCode = customer.CustomerCode.Trim(),
                        CustomerName = customer.CustomerName.Trim(),
                        ShortName = customer.ShortName,
                        CompanyName = customer.CompanyName,
                        ContactName = customer.ContactName,
                        Address = customer.Address,
                        Eaddress = customer.Eaddress,
                        Phone = customer.Phone,
                        Fax = customer.Fax,
                        Email = customer.Email,
                        TaxCode = customer.TaxCode,
                        BankAccount = customer.BankAccount,
                        SpecialInfo = customer.SpecialInfo,
                        Note = customer.Note,
                        //Active = entity.Active,
                        ModifiedUser = customer.ModifiedUser,
                        ModifiedDate = customer.ModifiedDate ?? DateTime.Now,
                        AreaId = customer.AreaId,
                        AreaName = customer.Area.AreaName,
                        CustomerTypeId = customer.CustomerTypeId,
                        CustomerTypeName = customer.CustomerType.CustomerTypeName,
                        UseForecast = customer.UseForecast,
                        MaxCredit = customer.MaxCredit,
                        EmployeeId = customer.EmployeeId,
                        EmloyeeName = customer.Employee.EmployeeName,
                        IsMonitor = customer.IsMonitor ?? false,
                        ClassifiedId = customer.ClassifiedId,
                        ClassifiedName = customer.CustomerClassified.Name,
                        StartDate = customer.StartDate,
                        State = customer.State,
                        StateName = MyUtilities.Sales.GetCustomerState(customer.State),
                        IsNotRequireApproveOrder = customer.IsNotRequireApproveOrder,
                        IsWorkOrder = customer.IsWorkOrder,
                        IsWorkOrderNotFullMaterial = customer.IsWorkOrderNotFullMaterial,
                        ImportTax = customer.ImportTax,
                        QuotationFactor = customer.QuotationFactor,
                    };
                    entity.PayType = payType.FirstOrDefault(pt => pt.Id == customer.CustomerPayTypeId).TypeName;
                    if (customer.ShippingMethodId != null) {
                        entity.ShippingMethodId = customer.ShippingMethodId ?? 0;
                        entity.ShippingMethodName = customer.ShipMethod.Name;
                        entity.ShippingMethodFee = customer.ShipMethod.ShipBase ?? 0;
                    }
                    models.Add(entity);
                }
            }
            return models;
        }

        [GridAction]
        public ActionResult SelectCustomer() {
            try {
                return
                    View(
                        new GridModel(GetAllCustomer(0).OrderBy(m => m.CustomerCode)
                                                      .ThenBy(m => m.AreaId)
                                                      .ThenBy(m => m.CustomerTypeId)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
                return View(new GridModel(new List<Customer>()));
            }
        }

        [GridAction]
        public ActionResult SelectCustomerInfoById(int customerId) {
            try {
                return View(new GridModel(GetAllCustomer(customerId)
                                              .OrderBy(m => m.CustomerCode)
                                              .ThenBy(m => m.AreaId)
                                              .ThenBy(m => m.CustomerTypeId)));
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
                return View(new GridModel(new List<Customer>()));
            }
        }

        [GridAction]
        public ActionResult SelectCustomerProductById(int customerId) {
            var model = new List<ProductModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var products = from p in vfi.Products
                                   where p.Active && p.CustomerId == customerId
                                   select new {
                                       p.ProductId,
                                       p.ProductCode,
                                       p.ProductName,
                                       p.DesignNo
                                   };
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
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
            }
            return View(new GridModel(model));
        }

        [GridAction]
        public ActionResult SelectCustomerAccessUserById(int customerId) {
            var model = new List<CustomerAccessPermissionModel>();
            try {
                model = GetCustomerAccess(customerId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectCustomerAccessUserById", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<CustomerAccessPermissionModel> GetCustomerAccess(int customerId) {
            var model = new List<CustomerAccessPermissionModel>();
            using (var vfi = new tammaContext()) {
                var accessUsers = from p in vfi.CustomerAccessPermissions
                                  where p.CustomerId == customerId && p.Active
                                  select p;
                foreach (var accessUser in accessUsers) {
                    var entity = new CustomerAccessPermissionModel {
                        CustomerId = customerId,
                        UserId = accessUser.UserId,
                        UserName = accessUser.User.Username,
                        ModifiedDate = accessUser.ModifiedDate,
                        ModifiedUser = accessUser.ModifiedUser,
                        Note = accessUser.Note,
                        RoleId = accessUser.RoleId,
                        Active = accessUser.Active
                    };
                    model.Add(entity);
                }
            }
            return model;
        }
        [HttpPost]
        [GridAction]
        public ActionResult InsertCustomerAccess(CustomerAccessPermissionModel insert, int customerId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerAccessPermissionModel>()));
            }
            try {
                int userId = 0;
                try {
                    userId = Convert.ToInt32(insert.UserName);
                }
                catch (Exception) {
                }
                if (userId != 0) {
                    using (var vfi = new tammaContext()) {
                        var permission =
                            vfi.CustomerAccessPermissions.FirstOrDefault(
                                cap => cap.CustomerId == customerId && cap.UserId == userId);

                        if (permission != null) {
                            permission.Active = true;
                            permission.ModifiedDate = DateTime.Now;
                            permission.ModifiedUser = HttpContext.User.Identity.Name;
                        }
                        else {
                            permission = new CustomerAccessPermission {
                                CustomerId = customerId,
                                UserId = userId,
                                Note = insert.Note,
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                Active = true,
                            };
                            vfi.CustomerAccessPermissions.Add(permission);
                        }
                        vfi.SaveChanges();
                    }

                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertCustomerAccess", ex.Message);
            }

            return View(new GridModel(GetCustomerAccess(customerId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateCustomerAccess(CustomerAccessPermissionModel update, int customerId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerAccessPermissionModel>()));
            }
            try {
                int userId = 0;
                try {
                    userId = Convert.ToInt32(update.UserName);
                }
                catch (Exception) {
                    userId = update.UserId;
                }
                if (userId != 0) {
                    using (var vfi = new tammaContext()) {
                        var permission =
                            vfi.CustomerAccessPermissions.FirstOrDefault(
                                cap => cap.CustomerId == update.CustomerId && cap.UserId == userId);

                        if (permission != null) {
                            permission.UserId = userId;
                            permission.Active = update.Active;
                            permission.ModifiedDate = DateTime.Now;
                            permission.ModifiedUser = HttpContext.User.Identity.Name;
                        }
                        else {
                            permission = vfi.CustomerAccessPermissions.FirstOrDefault(cap => cap.RoleId == update.RoleId);
                            if (permission != null) {
                                permission.UserId = userId;
                                permission.Active = update.Active;
                                permission.ModifiedDate = DateTime.Now;
                                permission.ModifiedUser = HttpContext.User.Identity.Name;
                            }

                        }
                        vfi.SaveChanges();
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateCustomerAccess", ex.Message);
            }
            return View(new GridModel(GetCustomerAccess(customerId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertCustomer(CustomerModel customerUpdate) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerModel>()));
            }
            try {
                using (var vfi = new tammaContext()) {
                    int areaId = 1;
                    try {
                        areaId = Convert.ToInt32(customerUpdate.AreaName);
                    }
                    catch (Exception) {
                        areaId =
                            vfi.Areas.FirstOrDefault(a => a.AreaName.Equals(customerUpdate.AreaName)).AreaId;
                    }
                    int typeId = 1;
                    try {
                        typeId = Convert.ToInt32(customerUpdate.CustomerTypeName);
                    }
                    catch (Exception) {
                        typeId =
                            vfi.CustomerTypes.FirstOrDefault(
                                a => a.CustomerTypeName.Equals(customerUpdate.CustomerTypeName)).CustomerTypeId;
                    }
                    int payTypeId = 1;
                    try {
                        payTypeId = Convert.ToInt32(customerUpdate.PayType);
                    }
                    catch (Exception) {
                        payTypeId =
                            vfi.CustomerPayTypes.FirstOrDefault(
                                a => a.TypeName.Equals(customerUpdate.PayType)).Id;
                    }
                    int employeeId = 3;
                    try {
                        employeeId = Convert.ToInt32(customerUpdate.EmloyeeName);
                    }
                    catch (Exception) {
                        employeeId =
                            vfi.Employees.FirstOrDefault(
                                a => a.EmployeeName.Equals(customerUpdate.EmloyeeName)).EmployeeId;
                    }
                    int classifiedId = 1;
                    try {
                        classifiedId = Convert.ToInt32(customerUpdate.ClassifiedName);
                    }
                    catch (Exception) {
                        classifiedId =
                            vfi.CustomerClassifieds.FirstOrDefault(
                                a => a.Name.Equals(customerUpdate.ClassifiedName)).ClassifiedId;
                    }
                    Vfi.Models.Customer customer = null;
                    if (!string.IsNullOrWhiteSpace(customerUpdate.CustomerCode))
                        customer = vfi.Customers.FirstOrDefault(c => c.CustomerCode.Equals(customerUpdate.CustomerCode));
                    if (customer != null)
                        throw new AggregateException("Lỗi! Mã khách hàng đã tồn tại");
                    customer = new Vfi.Models.Customer {
                        CustomerCode = (customerUpdate.CustomerCode + "").Trim(),
                        CustomerName = (customerUpdate.CustomerName + "").Trim(),
                        ShortName = (customerUpdate.ShortName + "").Trim(),
                        CompanyName = (customerUpdate.CompanyName + "").Trim(),
                        //1
                        Address = customerUpdate.Address ?? "",
                        Eaddress = customerUpdate.Eaddress ?? "",
                        //2
                        ContactName = customerUpdate.ContactName ?? "",
                        Phone = customerUpdate.Phone ?? "",
                        Fax = customerUpdate.Fax ?? "",
                        Email = customerUpdate.Email ?? "",
                        //3
                        TaxCode = customerUpdate.TaxCode ?? "",
                        MaxCredit = customerUpdate.MaxCredit ?? 0,
                        BankAccount = customerUpdate.BankAccount ?? "",
                        //4
                        SpecialInfo = customerUpdate.SpecialInfo ?? "",
                        Note = customerUpdate.Note ?? "",
                        //Active = customerUpdate.Active,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        AreaId = areaId,
                        CustomerTypeId = typeId,
                        CustomerPayTypeId = payTypeId,
                        EmployeeId = employeeId,
                        ClassifiedId = classifiedId,
                        UseForecast = true,
                        State = (byte)MyUtilities.Sales.CustomerState.Active,
                        IsMonitor = true,
                        StartDate = customerUpdate.StartDate,
                        IsNotRequireApproveOrder = customerUpdate.IsNotRequireApproveOrder,
                        IsWorkOrder = customerUpdate.IsWorkOrder,
                        IsWorkOrderNotFullMaterial = customerUpdate.IsWorkOrderNotFullMaterial,
                        QuotationFactor = 1,
                        ImportTax = 0
                    };
                    if (string.IsNullOrWhiteSpace(customer.CustomerCode))
                        customer.State = (byte)MyUtilities.Sales.CustomerState.NewCustomer;
                    vfi.SaveChanges();
                    // cho nay la la
                    vfi.Customers.Add(customer);
                    var rs =
                    vfi.SaveChanges();
                    if (rs == 0) {
                        throw new AggregateException("Không thể tạo giá trị mới. Xin vui lòng nhập lại. (savechanges)");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CustomerName", ex.Message);
            }

            return View(new GridModel(GetAllCustomer(0)
                                          .OrderBy(m => m.CustomerCode)
                                          .ThenBy(m => m.AreaId)
                                          .ThenBy(m => m.CustomerTypeId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateCustomer(CustomerModel customerUpdate) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerModel>()));
            }
            var model = new Customer { CustomerId = customerUpdate.CustomerId };

            if (TryUpdateModel(model)) {
                try {
                    using (var vfi = new tammaContext()) {
                        //if (vfi.Customers.FirstOrDefault(
                        //        c =>
                        //        c.CustomerCode.Equals(customerUpdate.CustomerCode) &&
                        //        c.CustomerId != customerUpdate.CustomerId) == null)
                        //{
                        int areaId = 1;
                        try {
                            areaId = Convert.ToInt32(customerUpdate.AreaName);
                        }
                        catch (Exception) {
                            areaId =
                                vfi.Areas.FirstOrDefault(a => a.AreaName.Equals(customerUpdate.AreaName)).AreaId;
                        }
                        int typeId = 1;
                        try {
                            typeId = Convert.ToInt32(customerUpdate.CustomerTypeName);
                        }
                        catch (Exception) {
                            typeId =
                                vfi.CustomerTypes.FirstOrDefault(
                                    a => a.CustomerTypeName.Equals(customerUpdate.CustomerTypeName)).CustomerTypeId;
                        }
                        int payTypeId = 1;
                        try {
                            payTypeId = Convert.ToInt32(customerUpdate.PayType);
                        }
                        catch (Exception) {
                            payTypeId =
                                vfi.CustomerPayTypes.FirstOrDefault(
                                    a => a.TypeName.Equals(customerUpdate.PayType)).Id;
                        }
                        int employeeId = 3;
                        try {
                            employeeId = Convert.ToInt32(customerUpdate.EmloyeeName);
                        }
                        catch (Exception) {
                            employeeId =
                                vfi.Employees.FirstOrDefault(
                                    a => a.EmployeeName.Equals(customerUpdate.EmloyeeName)).EmployeeId;
                        }
                        int classifiedId = 3;
                        try {
                            classifiedId = Convert.ToInt32(customerUpdate.ClassifiedName);
                        }
                        catch (Exception) {
                            classifiedId =
                                vfi.CustomerClassifieds.FirstOrDefault(
                                    a => a.Name.Equals(customerUpdate.ClassifiedName)).ClassifiedId;
                        }
                        int state = (byte)MyUtilities.Sales.CustomerState.NoActive;
                        try {
                            state = Convert.ToInt32(customerUpdate.StateName);
                        }
                        catch (Exception) {
                            state = customerUpdate.State;
                        }

                        var customer =
                            vfi.Customers.FirstOrDefault(c => c.CustomerCode.Equals(customerUpdate.CustomerCode) &&
                                                              c.CustomerId != customerUpdate.CustomerId);
                        if (customer != null && !string.IsNullOrWhiteSpace(customerUpdate.CustomerCode))
                            throw new AggregateException("Lỗi! Mã khách hàng đã tồn tại.");
                        customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerUpdate.CustomerId);
                        customer.CustomerCode = (customerUpdate.CustomerCode + "").Trim();
                        customer.CustomerName = (customerUpdate.CustomerName + "").Trim();
                        customer.ShortName = (customerUpdate.ShortName + "").Trim();
                        customer.CompanyName = (customerUpdate.CompanyName + "").Trim();
                        //1
                        //customer.Address = customerUpdate.Address ?? "";
                        //customer.Eaddress = customerUpdate.Eaddress ?? "";
                        ////2
                        //customer.ContactName = customerUpdate.ContactName ?? "";
                        //customer.Phone = customerUpdate.Phone ?? "";
                        //customer.Fax = customerUpdate.Fax ?? "";
                        //customer.Email = customerUpdate.Email ?? "";
                        ////3
                        //customer.TaxCode = customerUpdate.TaxCode ?? "";
                        //customer.BankAccount = customerUpdate.BankAccount ?? "";
                        //customer.MaxCredit = customerUpdate.MaxCredit ?? 0;
                        ////4
                        //customer.SpecialInfo = customerUpdate.SpecialInfo ?? "";
                        //customer.Note = customerUpdate.Note ?? "";
                        customer.ModifiedUser = HttpContext.User.Identity.Name;
                        customer.ModifiedDate = DateTime.Now;
                        customer.AreaId = areaId;
                        customer.CustomerTypeId = typeId;
                        customer.CustomerPayTypeId = payTypeId;
                        customer.EmployeeId = employeeId;
                        customer.ClassifiedId = classifiedId;
                        customer.StartDate = customerUpdate.StartDate;
                        customer.IsMonitor = customerUpdate.IsMonitor;
                        customer.State = state;
                        customer.IsNotRequireApproveOrder = customerUpdate.IsNotRequireApproveOrder;
                        customer.IsWorkOrder = customerUpdate.IsWorkOrder;
                        customer.IsWorkOrderNotFullMaterial = customerUpdate.IsWorkOrderNotFullMaterial;
                        if (string.IsNullOrWhiteSpace(customer.CustomerCode))
                            customer.State = (byte)MyUtilities.Sales.CustomerState.NewCustomer;
                        else if (customer.State == (byte)MyUtilities.Sales.CustomerState.NewCustomer)
                            customer.State = (byte)MyUtilities.Sales.CustomerState.Active;
                        vfi.SaveChanges();
                        //}
                        //else
                        //    ModelState.AddModelError("CustomerName",
                        //                             @"Mã khách hàng đã tồn tại. Xin vui lòng nhập lại. (existed code). ");
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("CustomerName", ex.Message);
                }
            }
            else
                ModelState.AddModelError("CustomerName", @"Lỗi giá trị nhập. (TryUpdateModel)");

            return View(new GridModel(GetAllCustomer(0)
                                          .OrderBy(m => m.CustomerCode)
                                          .ThenBy(m => m.AreaId)
                                          .ThenBy(m => m.CustomerTypeId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateCustomerAddress(CustomerModel customerUpdate, int customerId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerModel>()));
            }

            try {
                using (var vfi = new tammaContext()) {
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    int methodId = 0;
                    try {
                        methodId = Convert.ToInt32(customerUpdate.ShippingMethodName);
                    }
                    catch (Exception) {
                    }
                    if (methodId != 0) {
                        customer.ShippingMethodId = methodId;
                    }
                    customer.Address = customerUpdate.Address ?? "";
                    customer.Eaddress = customerUpdate.Eaddress ?? "";
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CustomerName", @"Lỗi giá trị nhập. (Try-Catch) \n" + ex.Message);
            }
            return View(new GridModel(GetAllCustomer(customerId)
                                          .OrderBy(m => m.CustomerCode)
                                          .ThenBy(m => m.AreaId)
                                          .ThenBy(m => m.CustomerTypeId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateCustomerContact(CustomerModel customerUpdate, int customerId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerModel>()));
            }

            try {
                using (var vfi = new tammaContext()) {
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);

                    customer.ContactName = customerUpdate.ContactName ?? "";
                    customer.Phone = customerUpdate.Phone ?? "";
                    customer.Fax = customerUpdate.Fax ?? "";
                    customer.Email = customerUpdate.Email ?? "";
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CustomerName", @"Lỗi giá trị nhập. (Try-Catch) \n" + ex.Message);
            }
            return View(new GridModel(GetAllCustomer(customerId)
                                          .OrderBy(m => m.CustomerCode)
                                          .ThenBy(m => m.AreaId)
                                          .ThenBy(m => m.CustomerTypeId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateCustomerCredit(CustomerModel customerUpdate, int customerId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerModel>()));
            }

            try {
                using (var vfi = new tammaContext()) {
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);

                    customer.TaxCode = customerUpdate.TaxCode ?? "";
                    customer.BankAccount = customerUpdate.BankAccount ?? "";
                    customer.MaxCredit = customerUpdate.MaxCredit ?? 0;
                    customer.ImportTax = customerUpdate.ImportTax;
                    customer.QuotationFactor = customerUpdate.QuotationFactor;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CustomerName", @"Lỗi giá trị nhập. (Try-Catch) \n" + ex.Message);
            }
            return View(new GridModel(GetAllCustomer(customerId)
                                          .OrderBy(m => m.CustomerCode)
                                          .ThenBy(m => m.AreaId)
                                          .ThenBy(m => m.CustomerTypeId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateCustomerNote(CustomerModel customerUpdate, int customerId) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerModel>()));
            }

            try {
                using (var vfi = new tammaContext()) {
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);

                    customer.SpecialInfo = customerUpdate.SpecialInfo ?? "";
                    customer.Note = customerUpdate.Note ?? "";
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("CustomerName", @"Lỗi giá trị nhập. (Try-Catch) \n" + ex.Message);
            }
            return View(new GridModel(GetAllCustomer(customerId)
                                          .OrderBy(m => m.CustomerCode)
                                          .ThenBy(m => m.AreaId)
                                          .ThenBy(m => m.CustomerTypeId)));
        }

        public ActionResult SelectComboBoxCustomer() {
            var model = new List<CustomerModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var customers =
                        vfi.Customers.Where(c => c.State == (byte)MyUtilities.Sales.CustomerState.Active)
                           .OrderBy(c => c.CustomerCode);
                    foreach (var customer in customers) {
                        var entity = new CustomerModel {
                            CustomerId = customer.CustomerId,
                            CustomerCode = customer.CustomerCode,
                            CustomerName = customer.CustomerName
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(model, "CustomerId", "CustomerCodeName")
            };
        }

        public ActionResult SelectComboBoxWorkOrderCustomer() {
            var model = new List<CustomerModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var customers =
                        vfi.Customers.Where(c => c.State == (byte)MyUtilities.Sales.CustomerState.Active && c.IsWorkOrder == true)
                           .OrderBy(c => c.CustomerCode);
                    foreach (var customer in customers) {
                        var entity = new CustomerModel {
                            CustomerId = customer.CustomerId,
                            CustomerCode = customer.CustomerCode,
                            CustomerName = customer.CustomerName
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(model, "CustomerId", "CustomerCodeName")
            };
        }

        public ActionResult SelectComboBoxCustomerAccess() {
            var model = new List<CustomerModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var user = vfi.Users.FirstOrDefault(u => u.Username.Contains(HttpContext.User.Identity.Name));
                    var salesManager = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.SaleManagement);
                    var customerAccessIds = MyUtilities.Sales.GetCustomerAccessList(HttpContext.User.Identity.Name);

                    var customers = from c in vfi.Customers
                                    where c.State == (byte)MyUtilities.Sales.CustomerState.Active &&
                                          (salesManager || customerAccessIds.Contains(c.CustomerId))
                                    select new {
                                        c.CustomerId,
                                        c.CustomerCode,
                                        c.CustomerName
                                    };

                    foreach (var customer in customers) {
                        var entity = new CustomerModel {
                            CustomerId = customer.CustomerId,
                            CustomerCode = customer.CustomerCode,
                            CustomerName = customer.CustomerName
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("", ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(model, "CustomerId", "CustomerCodeName")
            };
        }
        [HttpPost]
        public ActionResult GetCustomerById(int customerId) {
            try {

                using (var vfi = new tammaContext()) {
                    var customer = vfi.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    if (customer == null)
                        return Json(
                            new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NotFound,
                            "Lỗi! Không tìm thấy khách hàng",
                            null));
                    var lastOrder = (from o in vfi.Orders
                                     orderby o.DueDate descending
                                     where o.DueDate != null &&
                                           o.Status != (byte)MyUtilities.Sales.Status.Cancel
                                           && o.CustomerId == customerId
                                     select o).FirstOrDefault();
                    var info = new OrderModel {
                        BillToAddress = customer.Address,
                        CurrencyCode = "",
                        ShipMethodName = "",
                        SalesPersonName = "",
                        PaymentMethodName = ""
                    };
                    if (lastOrder != null) {
                        info.CurrencyCode = lastOrder.CurrencyCode;
                        if (lastOrder.ShipMethodId != null) {
                            var shipMethod =
                                vfi.ShipMethods.FirstOrDefault(sm => sm.ShipMethodId == lastOrder.ShipMethodId);
                            info.ShipMethodId = lastOrder.ShipMethodId.Value;
                            info.ShipMethodName = shipMethod.Name;
                        }
                        if (lastOrder.PaymentTermId != null) {
                            var paymentMethod =
                                vfi.PaymentTerms.FirstOrDefault(sm => sm.Id == lastOrder.PaymentTermId);
                            info.PaymentMethodId = lastOrder.PaymentTermId.Value;
                            info.PaymentMethodName = paymentMethod.TermName;
                        }
                        if (lastOrder.Employee != null) {
                            info.SalesPersonId = lastOrder.SalesPersonId.Value;
                            info.SalesPersonName = lastOrder.Employee.EmployeeName;
                        }
                    }
                    else {
                        info.SalesPersonId = customer.EmployeeId;
                        info.SalesPersonName = customer.Employee.EmployeeName;
                        info.BillToAddress = customer.Address;
                        info.ShipToAddress = customer.Eaddress;
                        if (customer.ShippingMethodId != null) {
                            info.ShipMethodId = customer.ShippingMethodId ?? 0;
                            info.ShipMethodName = customer.ShipMethod.Name;
                        }
                    }
                    return Json(
                        new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError,
                        "",
                        info));
                }
            }
            catch (Exception ex) {
                return Json(
                    new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.Exception,
                    ex.Message,
                    null));
            }
        }

        [HttpPost]
        public ActionResult PrintCustomerList() {
            try {
                var model = new List<CustomerModel>();
                using (var vfi = new tammaContext()) {
                    var customers = vfi.Customers;
                    var payType = vfi.CustomerPayTypes.ToList();
                    foreach (var customer in customers) {
                        var entity = new CustomerModel {
                            CustomerId = customer.CustomerId,
                            CustomerCode = customer.CustomerCode.Trim(),
                            CustomerName = customer.CustomerName.Trim(),
                            ShortName = customer.ShortName ?? "",
                            CompanyName = customer.CompanyName ?? "",
                            ContactName = customer.ContactName ?? "",
                            Address = customer.Address ?? "",
                            Eaddress = customer.Eaddress ?? "",
                            //Phone = entity.Phone ?? "",
                            //Fax = entity.Fax ?? "",
                            //Email = entity.Email ?? "",
                            //TaxCode = entity.TaxCode ?? "",
                            //BankAccount = entity.BankAccount ?? "",
                            //SpecialInfo = entity.SpecialInfo ?? "",
                            Note = customer.Note ?? "",
                            //Active = customer.s,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            ModifiedDate = DateTime.Now,
                            AreaId = customer.AreaId,
                            CustomerTypeId = customer.CustomerTypeId,
                            Info1 = "Liên hệ: " + (customer.Phone) + "\n" + "Fax: " + (customer.Fax)
                        + "\n" + "Email: " + (customer.Email),
                            Info2 =
                                "Mã số thuế: " + (customer.TaxCode) + "\n" +
                                "Tài khoản ngân hàng: " + (customer.BankAccount ?? "") + "\n" + "Max credit: " +
                                (customer.SpecialInfo),
                            AreaName = customer.Area.AreaName,
                            CustomerTypeName = customer.CustomerType.CustomerTypeName,
                            ClassifiedName = customer.CustomerClassified.Name,
                            EmloyeeName = customer.Employee.EmployeeName,
                        };
                        //entity.AreaName = customer.Area.AreaName
                        //entity.CustomerTypeName = customerType.FirstOrDefault(a => a.CustomerTypeId == customer.CustomerTypeId).CustomerTypeName;
                        entity.PayType = payType.FirstOrDefault(pt => pt.Id == customer.CustomerPayTypeId).TypeName;
                        //entity.ClassifiedName = cus
                        model.Add(entity);
                    }
                }
                return PartialView("PageCustomerList", model);
            }
            catch (Exception ex) {
                ModelState.AddModelError("CustomerList", "" + ex.Message);
            }
            return PartialView("PageCustomerList", null);

        }
        [GridAction]
        public ActionResult SelectCustomerClassified() {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerModel>()));
            }
            var model = new List<CustomerClassifiedModel>();

            model = GetCustomerClassifiedModel();
            return View(new GridModel(model));
        }
        List<CustomerClassifiedModel> GetCustomerClassifiedModel() {
            var model = new List<CustomerClassifiedModel>();
            try {
                using (var vfi = new tammaContext()) {
                    var classifieds = vfi.CustomerClassifieds;
                    foreach (var classified in classifieds) {
                        var entity = new CustomerClassifiedModel {
                            ClassifiedId = classified.ClassifiedId,
                            Name = classified.Name,
                            Description = classified.Description,
                            Active = classified.Active,
                            ModifiedDate = classified.ModifiedDate,
                            ModifiedUser = classified.ModifiedUser
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetCustomerClassifiedModel", ex.Message);
            }
            return model;
        }
        public ActionResult SelectComboBoxCustomerClassified() {
            using (var vfi = new tammaContext()) {
                return new JsonResult {
                    Data = new SelectList(vfi.CustomerClassifieds.Where(cc => cc.Active).ToList(), "ClassifiedId", "Name")
                };

            }

        }
        public ActionResult SelectComboBoxCustomerState() {
            var val = from MyUtilities.Sales.CustomerState stt in Enum.GetValues(typeof(MyUtilities.Sales.CustomerState))
                      select new {
                          Value = (int)Enum.Parse(typeof(MyUtilities.Sales.CustomerState), stt.ToString()),
                          Text =
                      MyUtilities.Sales.GetCustomerState(
                          (int)Enum.Parse(typeof(MyUtilities.Sales.CustomerState), stt.ToString()))
                      };

            return new JsonResult {
                Data = new SelectList(val, "Value", "Text")
            };

        }
        [GridAction]
        public ActionResult InsertCustomerClassified(CustomerClassifiedModel insert) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerModel>()));
            }
            try {
                if (string.IsNullOrWhiteSpace(insert.Name.Trim()))
                    throw new AggregateException("Lỗi tên loại chưa cập nhật.");
                using (var vfi = new tammaContext()) {
                    var classified = vfi.CustomerClassifieds.FirstOrDefault(cc => cc.Name.Equals(insert.Name.Trim()));
                    if (classified != null) {
                        if (classified.Active)
                            throw new AggregateException("Lỗi! Tên loại bị trùng.");
                        classified.Active = true;
                        classified.Description = insert.Description;
                        classified.ModifiedDate = DateTime.Now;
                        classified.ModifiedUser = HttpContext.User.Identity.Name;
                    }
                    else {
                        classified = new CustomerClassified {
                            Active = true,
                            Name = insert.Name,
                            Description = insert.Description,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                        };
                        vfi.CustomerClassifieds.Add(classified);
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertCustomerClassified", ex.Message);
            }
            return View(new GridModel(GetCustomerClassifiedModel()));
        }

        [GridAction]
        public ActionResult UpdateCustomerClassified(CustomerClassifiedModel update) {
            if (!Request.IsAuthenticated) {
                ModelState.AddModelError("CreatePlatingDetail",
                                         @"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         @"Xin vui lòng đăng nhập lại hệ thống.");
                return View(new GridModel(new List<CustomerModel>()));
            }
            try {
                if (string.IsNullOrWhiteSpace(update.Name.Trim()))
                    throw new AggregateException("Lỗi tên loại chưa cập nhật.");
                using (var vfi = new tammaContext()) {
                    var classified =
                        vfi.CustomerClassifieds.FirstOrDefault(
                            cc => cc.Name.Equals(update.Name.Trim()) && cc.ClassifiedId != update.ClassifiedId);
                    if (classified != null) {
                        if (classified.Active)
                            throw new AggregateException("Lỗi! Tên loại bị trùng.");
                        classified.Active = true;
                        classified.Description = update.Description;
                        classified.ModifiedDate = DateTime.Now;
                        classified.ModifiedUser = HttpContext.User.Identity.Name;
                    }
                    else {
                        classified = vfi.CustomerClassifieds.FirstOrDefault(cc => cc.ClassifiedId == update.ClassifiedId);
                        if (classified == null)
                            throw new AggregateException("Lỗi! Không tìm thấy loại khách hàng.");
                        classified.Active = update.Active;
                        classified.Name = update.Name;
                        classified.Description = update.Description;
                        classified.ModifiedDate = DateTime.Now;
                        classified.ModifiedUser = HttpContext.User.Identity.Name;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateCustomerClassified", ex.Message);
            }
            return View(new GridModel(GetCustomerClassifiedModel()));
        }
        #endregion
    }
}
