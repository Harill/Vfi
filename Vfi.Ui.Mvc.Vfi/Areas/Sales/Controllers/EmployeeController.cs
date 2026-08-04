using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Ui.Mvc.Vfi.Areas.Sales.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
using System.Threading;

namespace Vfi.Ui.Mvc.Vfi.Areas.Sales.Controllers {
    public class EmployeeController : Controller {
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmployeeService _employeeService;
        [InjectionConstructor]
        public EmployeeController(IUnitOfWork unitOfWork
            //, IEmployeeService employeeService
            ) {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            //if (employeeService == null) throw new ArgumentNullException("employeeService");

            //_unitOfWork = unitOfWork;
            //_employeeService = employeeService;
        }

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
        public ActionResult EmployeeManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        // Data
        public List<EmployeeModel> GetEmployeeByModels() {
            using (var vfi = new tammaContext()) {
                var production2 = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.Production2Management);
                var sale = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.SaleManagement);
                var employees = vfi.Employees.ToList();
                if (production2 && sale) { }
                else if (production2) {
                    employees = employees.Where(e => e.Production2 || e.Production2B).ToList();
                }
                else if (sale) {
                    employees = employees.Where(e => e.Active).ToList();
                }
                return employees.Select(employee => new EmployeeModel {
                    Active = employee.Active,
                    Production2 = employee.Production2,
                    Production2B = employee.Production2B,
                    EmployeeCode = employee.EmployeeCode,
                    EmployeeName = employee.EmployeeName,
                    ModifiedDate = employee.ModifiedDate,
                    ModifiedUser = employee.ModifiedUser,
                    EmployeeId = employee.EmployeeId,
                    Repair = employee.Repair,
                    QcLine = employee.QcLine,
                    GroupName = employee.GroupName,
                    UserId = employee.UserId ?? 0,
                    UserName = employee.UserId > 0 ? employee.User.Username : ""
                })
                                .OrderByDescending(e => e.Active)
                                .ThenByDescending(e => e.Production2)
                                .ThenByDescending(e => e.Production2B)
                                .ThenBy(e => e.EmployeeCode)
                                .ToList();
            }
        }

        #region Employee

        [GridAction]
        public ActionResult SelectEmployee() {
            return View(new GridModel(GetEmployeeByModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertEmployee(EmployeeModel insert) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                                 @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                                 @"Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {

                    var production2 = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.Production2Management);
                    var sale = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.SaleManagement);
                    if (production2 && sale) {
                    }
                    else if (sale) {
                        insert.Production2 = false;
                        insert.Production2B = false;
                        insert.Active = true;
                    }
                    else if (production2) {
                        insert.Production2 = true;
                        insert.Production2B = false;
                        insert.Active = false;
                    }
                    insert.EmployeeCode = insert.EmployeeCode.Trim();
                    insert.EmployeeName = insert.EmployeeName.Trim();
                    var entity =
                          vfi.Employees.FirstOrDefault(
                              e =>
                              e.EmployeeCode.Equals(insert.EmployeeCode) &&
                              e.Active == insert.Active &&
                              e.Production2 == insert.Production2 &&
                              e.Production2B == insert.Production2B);
                    var userId = 0;
                    try {
                        userId = Convert.ToInt32(insert.UserName);
                    }
                    catch (FormatException) { }
                    if (entity == null) {
                        entity = new Employee {
                            Active = insert.Active,
                            EmployeeCode = insert.EmployeeCode,
                            EmployeeName = insert.EmployeeName,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Production2 = insert.Production2,
                            Production2B = insert.Production2B,
                            Repair = insert.Repair,
                            QcLine = insert.QcLine,
                            GroupName = (insert.GroupName + "").Trim(),
                        };
                        if (userId > 0) { entity.UserId = userId; }
                        vfi.Employees.Add(entity);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Mã nhân viên đã tồn tại. Xin vui lòng nhập lại. (existed code). ");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertEmployee", ex.Message);
            }
            return View(new GridModel(GetEmployeeByModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateEmployee(EmployeeModel update) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException(@"Bạn đã bị mất quyền đăng nhập. \r\n " +
                                                 @"1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                                 @"Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new tammaContext()) {
                    update.EmployeeCode = update.EmployeeCode.Trim();
                    update.EmployeeName = update.EmployeeName.Trim();
                    var entity =
                           vfi.Employees.FirstOrDefault(
                               e =>
                               e.EmployeeCode.Equals(update.EmployeeCode) &&
                               e.Active == update.Active &&
                               e.Production2 == update.Production2 &&
                               e.Production2B == update.Production2B &&
                               e.EmployeeId != update.EmployeeId);
                    int? userId = null;
                    if (!string.IsNullOrWhiteSpace(update.UserName)) {
                        try {
                            userId = Convert.ToInt32(update.UserName);
                        }
                        catch (FormatException) {
                            userId = 0;
                        }
                    }
                    if (entity == null) {
                        entity = vfi.Employees.FirstOrDefault(e => e.EmployeeId == update.EmployeeId);
                        if (entity != null) {
                            entity.Active = update.Active;
                            entity.EmployeeCode = update.EmployeeCode;
                            entity.EmployeeName = update.EmployeeName;
                            entity.ModifiedDate = DateTime.Now;
                            entity.ModifiedUser = HttpContext.User.Identity.Name;
                            entity.Production2 = update.Production2;
                            entity.Production2B = update.Production2B;
                            entity.Repair = update.Repair;
                            entity.QcLine = update.QcLine;
                            entity.GroupName = (update.GroupName + "").Trim();
                            if (userId != 0) {
                                entity.UserId = userId;
                            }
                            vfi.SaveChanges();
                        }
                        else {
                            throw new AggregateException("Lỗi ! Không tìm thấy nhân viên ! Liên hệ admin");
                        }
                    }
                    else {
                        throw new AggregateException("Mã nhân viên đã tồn tại. Xin vui lòng nhập lại. (existed code). ");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateEmployee", ex.Message);
            }
            return View(new GridModel(GetEmployeeByModels()));
        }

        public ActionResult SelectComboBoxEmployeeProduction2() {
            using (var vfi = new tammaContext()) {
                var model = from m in vfi.Employees
                            where m.Production2 || m.Production2B
                            select new {
                                m.EmployeeId,
                                m.EmployeeName,
                                m.EmployeeCode,
                                EmployeeCodeName = m.EmployeeCode + "-" + m.EmployeeName,
                            };
                return new JsonResult {
                    Data = new SelectList(model.ToList(), "EmployeeId", "EmployeeCodeName")
                };
            }
        }

        public List<EmployeeModel> GetActiveEmployeeByConfig(EmployeeConfiguration config) {
            var model = new List<EmployeeModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.Employees
                         where
                         (config.Active == null || (config.Active  == true && (x.Active || x.QcLine || x.Repair || x.Production2 || x.Production2B))) &&
                         (config.IsSales == null || x.Active == config.IsSales) &&
                         (config.IsQCLine == null || x.QcLine == config.IsQCLine) &&
                         (config.IsRepair == null || x.Repair == config.IsRepair) &&
                         (config.IsProduction2 == null || x.Production2 == config.IsProduction2 || x.Production2B == config.IsProduction2)
                         select new EmployeeModel {
                             EmployeeId = x.EmployeeId,
                             EmployeeName = x.EmployeeName,
                             EmployeeCode = x.EmployeeCode
                         }).ToList();
            }
            return model;
        }

        // using for testing employee - tester
        public ActionResult SelectComboBoxEmployeeByWarehouseId(int warehouseId) {
            var config = new EmployeeConfiguration { };
            if (warehouseId > 0) {
                using (var vfi = new tammaContext()) {
                    var warehouse = vfi.Warehouses.FirstOrDefault(x => x.WarehouseId == warehouseId);
                    if (warehouse != null) {
                        if (warehouse.IsQC) { config.IsQCLine = true; }
                        if (warehouse.IsProduction) { config.IsProduction = true; }
                    }
                }
            }
            return new JsonResult {
                Data = new SelectList(GetActiveEmployeeByConfig(config), "EmployeeId", "EmployeeCodeName")
            };
        }

        [HttpPost]
        public ActionResult GetComboBoxEmployeeLogin() {
            var employeeId = 0;
            using (var vfi = new tammaContext()) {
                var userLink = vfi.Users.FirstOrDefault(x => x.Username.Equals(HttpContext.User.Identity.Name));
                if (userLink != null) {
                    var employee = userLink.Employees.FirstOrDefault();
                    if (employee != null) {
                        employeeId = employee.EmployeeId;
                    }
                }
            }
            //var employees = GetActiveEmployeeByConfig(new EmployeeConfiguration { });
            //var employee = employees.FirstOrDefault(x=> x.EmployeeId == 
            return new JsonResult {
                Data = new SelectList(
                    GetActiveEmployeeByConfig(new EmployeeConfiguration { Active = true }),
                    "EmployeeId", "EmployeeCodeName",
                    employeeId > 0 ? employeeId : -1)
            };
        }

        public ActionResult SelectComboBoxEmployee() {
            return new JsonResult {
                Data = new SelectList(GetActiveEmployeeByConfig(new EmployeeConfiguration { }), "EmployeeId", "EmployeeCodeName")
            };
        }
        public ActionResult SelectComboBoxSalesEmployee() {
            return new JsonResult {
                Data = new SelectList(GetActiveEmployeeByConfig(new EmployeeConfiguration { IsSales = true }), "EmployeeId", "EmployeeCodeName")
            };
        }
        public ActionResult SelectComboBoxEmployeeQcLine() {
            return new JsonResult {
                Data = new SelectList(GetActiveEmployeeByConfig(new EmployeeConfiguration { IsQCLine = true }), "EmployeeId", "EmployeeCodeName")
            };
        }
        public ActionResult SelectComboBoxEmployeeRepair() {
            return new JsonResult {
                Data = new SelectList(GetActiveEmployeeByConfig(new EmployeeConfiguration { IsRepair = true }), "EmployeeId", "EmployeeCodeName")
            };
        }

        #endregion
    }
}
