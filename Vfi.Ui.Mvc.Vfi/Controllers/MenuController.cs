using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;
using System.Threading;
using System.Globalization;

namespace Vfi.Ui.Mvc.Vfi.Controllers {
    public class MenuController : Controller {

        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }
            return ViewData;
        }
        public ActionResult MenuManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        #region Get Menu by Menu Entity

        //public ActionResult TopMenu(string menuLvl)
        //{
        //    if (menuLvl == null) menuLvl = "0";
        //    var menuRoot = _menu.Service.GetMenuRoot().ToList();

        //    return View(menuRoot);
        //}

        #endregion

        #region Get Menu by MenuModel

        public ActionResult MainMenu() {
            var listMenuModels = new List<MenuModel>();
            var currentUser = HttpContext.User.Identity.Name;
            try {
                using (var vfi = new tammaContext()) {
                    var menus = vfi.Menus.Where(x => x.Active == true).OrderBy(x => x.IDX).ToList();
                    var user = (from u in vfi.Users
                                where u.Username.Equals(currentUser) && u.Active == true
                                select new UserModel {
                                    UserId = u.UserId,
                                    Username = u.Username,
                                    FunctionCodes = u.Permissions.Where(x => x.Execution == true).Select(x => x.Function.FunctionCode).ToList()
                                }).FirstOrDefault();
                    listMenuModels = GetMenuTree(menus, user, null);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("MainMenu", ex.Message);
            }

            return PartialView("_MainMenu", listMenuModels);
        }

        public ActionResult MainMenuVertical() {
            var model = new List<MenuModel>();
            var currentUser = HttpContext.User.Identity.Name;
            try {
                using (var vfi = new tammaContext()) {
                    var menus = vfi.Menus.Where(x => x.Active == true).OrderBy(x => x.IDX).ToList();
                    var user = (from u in vfi.Users
                                where u.Username.Equals(currentUser) && u.Active == true
                                select new UserModel {
                                    UserId = u.UserId,
                                    Username = u.Username,
                                    FunctionCodes = u.Permissions.Where(x => x.Execution == true).Select(x => x.Function.FunctionCode).ToList()
                                }).FirstOrDefault();
                    var listMenuModels = GetMenuTree(menus, user, null);

                    var entity = new MenuModel {
                        Menu = new Menu { ControllerName = "", ActionName = "", MenuName = "Menu" },
                        HasPermissionExecute = false,
                        IsOkie = false,
                        Children = listMenuModels,
                    };
                    model.Add(entity);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("MainMenuVertical", ex.Message);
            }

            return PartialView("_MainMenuVertical", model);
        }

        private List<MenuModel> GetMenuTree(IEnumerable<Menu> menus, UserModel user, int? parentId) {
            var menuModels = new List<MenuModel>();
            var childs = menus.Where(x => x.ParentId == parentId);
            foreach (var menu in childs) {
                var entity = new MenuModel {
                    Menu = menu,
                    HasPermissionExecute = false,
                    IsOkie = false
                };
                var hasChild = menus.Any(x => x.ParentId == menu.MenuId);
                if (hasChild) {
                    entity.Children = GetMenuTree(menus, user, menu.MenuId);
                }
                if (!entity.Children.Any(x => !x.Menu.MenuName.Contains("-----"))) {
                    if (!string.IsNullOrWhiteSpace(menu.ActionName)) {
                        var userPermission = user.FunctionCodes.FirstOrDefault(x => x == menu.ActionName);
                        if (userPermission != null) {
                            entity.IsOkie = true;
                            entity.HasPermissionExecute = true;
                        }
                    }
                    else {
                        // false case
                    }
                }
                else {
                    entity.IsOkie = true;
                    entity.HasPermissionExecute = true;
                }
                if (entity.IsShow || entity.HasPermissionExecute) {
                    menuModels.Add(entity);
                }
                else if (HttpContext.User.Identity.Name.Equals("admin") && string.IsNullOrWhiteSpace(menu.ActionName)) {
                    menuModels.Add(entity);
                }
            }
            return menuModels;
        }


        [GridAction]
        public ActionResult SelectMenu(int parentId) {
            var model = new List<MenuManagementModel>();
            try {
                model = GetListMenu(parentId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMenuLevel0", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<MenuManagementModel> GetListMenu(int parentId) {
            var model = new List<MenuManagementModel>();
            using (var vfi = new tammaContext()) {
                var menus =
                    vfi.Menus.Where(m => parentId == 0 ? m.ParentId == null : m.ParentId == parentId)
                       .OrderBy(m => m.IDX);
                var menuIds = menus.Select(x => x.MenuId);
                var menuChildren = vfi.Menus.Where(x => x.ParentId != null && menuIds.Contains(x.ParentId.Value))
                    .Select(x => x.ParentId);
                foreach (var menu in menus) {
                    var entity = new MenuManagementModel {
                        MenuId = menu.MenuId,
                        MenuName = menu.MenuName,
                        ControllerName = menu.ControllerName,
                        ActionName = menu.ActionName,
                        Area = menu.Area,
                        Index = menu.IDX ?? 0,
                        ParentId = menu.ParentId ?? 0,
                        ParentName = menu.ParentId == null
                                         ? ""
                                         : menu.Menu2.MenuName,
                        FunctionId = 0,
                        ChildrenCount = 0
                    };
                    if (!string.IsNullOrWhiteSpace(entity.ActionName)) {
                        var function = vfi.Functions.FirstOrDefault(f => f.FunctionCode.Equals(entity.ActionName));
                        if (function != null) {
                            entity.FunctionId = function.FunctionId;
                        }
                    }
                    entity.ChildrenCount = menuChildren.Count(x => x.Value == entity.MenuId);
                    model.Add(entity);
                }
            }
            return model;
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertMenu(MenuManagementModel inserted, int parentId) {
            try {
                using (var vfi = new tammaContext()) {
                    var parenMenu = vfi.Menus.FirstOrDefault(m => m.MenuId == parentId);
                    var menu = new Vfi.Models.Menu {
                        MenuName = inserted.MenuName,
                        ControllerName = inserted.ControllerName,
                        ActionName = inserted.ActionName,
                        Area = inserted.Area,
                        IDX = inserted.Index,
                        ParentId = parentId,
                        MenuLevel = (byte)((parenMenu.MenuLevel ?? 0) + 1),
                        Active = true,
                    };
                    if (!string.IsNullOrWhiteSpace(inserted.ControllerName) &&
                        !string.IsNullOrWhiteSpace(inserted.ActionName)) {
                        var function = new Function {
                            FunctionName = menu.MenuName,
                            FunctionCode = menu.ActionName,
                            ModifiedDate = DateTime.Now,
                            Active = true,

                        };
                        vfi.Functions.Add(function);
                        var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name));
                        var entity = new Permission {
                            Creation = true,
                            Deletion = true,
                            Execution = true,
                            Modification = true,
                            FunctionID = function.FunctionId,
                            ModifiedDate = DateTime.Now,
                            UserID = user.UserId
                        };
                        vfi.Permissions.Add(entity);
                    }
                    vfi.Menus.Add(menu);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMenu", ex.Message);
            }
            return View(new GridModel(GetListMenu(parentId)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateMenu(MenuManagementModel updated) {
            var menuId = 0;
            try {
                using (var vfi = new tammaContext()) {
                    var menu = vfi.Menus.FirstOrDefault(m => m.MenuId == updated.MenuId);
                    menuId = menu.ParentId ?? 0;
                    menu.MenuName = updated.MenuName;
                    menu.IDX = updated.Index;
                    menu.Area = updated.Area;
                    menu.Active = true;
                    menu.ControllerName = updated.ControllerName;
                    if (menuId != 0) {
                        int parentId = menu.ParentId ?? 0;
                        try {
                            parentId = Convert.ToInt32(updated.ParentName);
                        }
                        catch (Exception) {
                        }
                        menu.ParentId = parentId;
                    }
                    if (!string.IsNullOrWhiteSpace(updated.ActionName) &&
                        !menu.ActionName.Equals(updated.ActionName)) {
                        var function = vfi.Functions.FirstOrDefault(f => f.FunctionCode.Equals(menu.ActionName));
                        if (function != null) {
                            function.FunctionName = updated.MenuName;
                            function.FunctionCode = updated.ActionName;
                            function.Active = true; // re-active
                                }
                        else {
                            function = new Function {
                                FunctionName = updated.MenuName,
                                FunctionCode = updated.ActionName,
                                ModifiedDate = DateTime.Now,
                                Active = true,
                            };
                            vfi.Functions.Add(function);
                        }

                        var user = vfi.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.Identity.Name)); 
                        var permission = vfi.Permissions.FirstOrDefault(x => x.UserID == user.UserId && x.FunctionID == function.FunctionId);
                        if (permission == null) {
                            permission = new Permission {
                                Creation = true,
                                Deletion = true,
                                Execution = true,
                                Modification = true,
                                FunctionID = function.FunctionId,
                                ModifiedDate = DateTime.Now,
                                UserID = user.UserId
                            };
                            vfi.Permissions.Add(permission);
                        }
                        else {
                            permission.Creation = true;
                            permission.Deletion = true;
                            permission.Execution = true;
                            permission.Modification = true;
                            permission.ModifiedDate = DateTime.Now;
                        }

                        menu.ActionName = updated.ActionName;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMenuLevel0", ex.Message);
            }
            return View(new GridModel(GetListMenu(menuId)));
        }

        public ActionResult SelectMenuParent() {
            var model = new List<MenuManagementModel>();
            using (var vfi = new tammaContext()) {
                var menus = vfi.Menus.Where(m => m.MenuLevel <= 1)
                                   .OrderBy(m => m.MenuLevel)
                                   .ThenBy(m => m.IDX);
                foreach (var menu in menus) {
                    var entity = new MenuManagementModel {
                        MenuId = menu.MenuId,
                        MenuName = menu.MenuName
                    };
                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data =
                    new SelectList(model, "MenuId", "MenuName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

    }
}
