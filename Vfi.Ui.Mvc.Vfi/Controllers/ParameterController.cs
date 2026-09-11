using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Models;
using System.Collections.Generic;
using Vfi.Ui.Mvc.Vfi.Utilities;
using System.Threading;
using System.Globalization;

namespace Vfi.Ui.Mvc.Vfi.Controllers {
    public class ParameterController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        [InjectionConstructor]
        public ParameterController(IUnitOfWork unitOfWork) {
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
        public ActionResult ParameterManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        [GridAction]
        public ActionResult SelectParam() {
            return View(new GridModel(GetAllParameters()));
        }

        List<ParameterModel> GetAllParameters() {
            var model = new List<ParameterModel>();
            using (var vfi = new tammaContext()) {
                model.AddRange(vfi.Parameters.Select(x => new ParameterModel {
                    ParamId = x.ParamId,
                    ParamCode = x.ParamCode,
                    Name = x.Name,
                    Value = x.Value,
                    ModifiedDate = x.ModifiedDate
                }));
            }

            return model;
        }


        [HttpPost]
        [GridAction]
        public ActionResult InsertParam() {
            return View(new GridModel(GetAllParameters()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateParam(ParameterModel update) {
            try {
                using (var vfi = new tammaContext()) {
                    var param = vfi.Parameters.FirstOrDefault(p => p.ParamId == update.ParamId);
                    if (param == null)
                        throw new AggregateException("Param not found");
                    param.Name = update.Name;
                    param.ModifiedDate = DateTime.Now;
                    param.Value = update.Value;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("ParamCode", @"Lỗi không thể cập nhật thông số (update). " + ex);

            }
            return View(new GridModel(GetAllParameters()));
        }
    }
}
