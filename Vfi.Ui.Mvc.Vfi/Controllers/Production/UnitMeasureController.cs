using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Telerik.Web.Mvc;
//using Vfi.Client.Module.Production.Interfaces;
using Vfi.Server.Core.CrossCutting.UnitOfWork;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;
using Vfi.Ui.Mvc.Vfi.Utilities;
using System.Threading;
using System.Globalization;

//using UnitMeasure = Vfi.Server.Core.DataModel.BaseEntities.UnitMeasure;

namespace Vfi.Ui.Mvc.Vfi.Controllers.Production
{
    public class UnitMeasureController : Controller
    {
        [InjectionConstructor]
        public UnitMeasureController(IUnitOfWork unitOfWork
            //,                                    IUnitMeasureService unitMeasureService
            )
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            //if (unitMeasureService == null) throw new ArgumentNullException("unitMeasureService");

        }

        ViewDataDictionary GetPageConfigData() {
            var viewModel = MyUtilities.MySystem.GetPageConfig(HttpContext.User.Identity.Name);
            foreach (var property in viewModel.GetType().GetProperties()) {
                ViewData[property.Name] = property.GetValue(viewModel, null);
            }
            return ViewData;
        }
        // View
        public ActionResult UnitMeasureManagement() {
            ViewData = GetPageConfigData();
            return View();
        }

        // GetData
        public List<UnitMeasureModel> GetUnitMeasureByModels()
        {
            try
            {
                using (var vfi = new tammaContext())
                {
                    return vfi.UnitMeasures.Select(
                        entity => new UnitMeasureModel
                            {
                                UnitMeasureCode = entity.UnitCode,
                                UnitMeasureName = entity.UnitName,
                                //Active = entity.Active,
                                IsPlatingUnit = entity.IsPlatingUnit,
                                ModifiedUser = entity.ModifiedUser,
                                ModifiedDate = entity.ModifiedDate,
                                UnitId = entity.UnitId
                            }).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region UnitMeasure

        [GridAction]
        public ActionResult SelectUnitMeasure()
        {
            var model = new List<UnitMeasureModel>();
            try
            {
                model = GetUnitMeasureByModels();
                //return View(new GridModel(GetUnitMeasureByModels()));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("SelectUnitMeasure", ex.Message);
            }
            return View(new GridModel(model));
        }

        [HttpPost]
        [GridAction]
        public ActionResult InsertUnitMeasure(UnitMeasureModel insert)
        {
            try
            {
                using (var vfi = new tammaContext())
                {
                    var entity = vfi.UnitMeasures.FirstOrDefault(um => um.UnitCode.Equals(insert.UnitMeasureCode));
                    if (entity == null)
                    {
                        entity = new UnitMeasure
                            {
                                IsPlatingUnit = insert.IsPlatingUnit,
                                ModifiedUser = HttpContext.User.Identity.Name,
                                ModifiedDate = DateTime.Now,
                                UnitCode = insert.UnitMeasureCode,
                                UnitName = insert.UnitMeasureName
                            };
                        vfi.UnitMeasures.Add(entity);
                        vfi.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("InsertUnitMeasure", @"Lỗi giá trị nhập! Mã đơn vị không được trùng!");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("InsertUnitMeasure", @"Lỗi giá trị nhập. (try-catch)");
            }
            return View(new GridModel(GetUnitMeasureByModels()));
        }

        [HttpPost]
        [GridAction]
        public ActionResult UpdateUnitMeasure(UnitMeasureModel update)
        {
            try
            {
                using (var vfi = new tammaContext())
                {
                    var entity =
                        vfi.UnitMeasures.FirstOrDefault(
                            um => um.UnitCode.Equals(update.UnitMeasureCode) && um.UnitId != update.UnitId);
                    if (entity == null)
                    {
                        entity = vfi.UnitMeasures.FirstOrDefault(um => um.UnitId == update.UnitId);
                        if(entity == null)
                            throw new AggregateException("Lỗi! Không tìm thấy đơn vị! Liên hệ admin!");
                        entity.IsPlatingUnit = update.IsPlatingUnit;
                        entity.ModifiedUser = HttpContext.User.Identity.Name;
                        entity.ModifiedDate = DateTime.Now;
                        entity.UnitCode = update.UnitMeasureCode;
                        entity.UnitName = update.UnitMeasureName;
                        vfi.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("UpdateUnitMeasure", @"Lỗi giá trị nhập! Mã đơn vị không được trùng!");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("UpdateUnitMeasure", @"Lỗi giá trị nhập. (try-catch)");
            }
            return View(new GridModel(GetUnitMeasureByModels()));
        }

        public ActionResult SelectComboBoxUnitMeasure()
        {
            return new JsonResult
            {
                Data = new SelectList(GetUnitMeasureByModels().Where(f => !f.IsPlatingUnit), "UnitMeasureCode", "UnitMeasureCodeName")
            };
        }

        public ActionResult SelectComboBoxPlatingUnit()
        {
            return new JsonResult
            {
                Data = new SelectList(GetUnitMeasureByModels().Where(f => f.IsPlatingUnit), "UnitMeasureCode", "UnitMeasureCodeName")
            };
        }
        #endregion
    }
}
