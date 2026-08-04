using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Models;
using Vfi.Ui.Mvc.Vfi.Areas.Inv.Models;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;

namespace Vfi.Ui.Mvc.Vfi.Areas.Factory.Controllers {
    public class MachineController : Controller {
        //
        // GET: /Factory/Machine/
        #region View
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

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi-VN");
            return ViewData;
        }
        public ActionResult MachineManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MachineProcess() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult MachineStateManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MachineFixManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MachineHistory() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MachineReport() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MachineErrorManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ErrorMachineList() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MachineStateHandover() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MachineErrorState() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MachineErrorState2() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult TrackUpMachine() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            Session["InserNewTrack"] = new int();
            return View();
        }
        public ActionResult MachineRepairManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ToolInventoryRequirementManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ProcessClassifiedManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult ProcessingTypeManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MachineRepair2Management() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MachineStateStatistic() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult MaterialLimitPlanManagement() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }
        public ActionResult TrackingRepairEmployee() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult MachineCard() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData = GetPageConfigData();
            return View();
        }

        public ActionResult ProductionMachineDiagram() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            var group = GetGroupMachineDiagram(MyUtilities.MySystem.CultureVN);
            ViewData = GetPageConfigData();
            ViewData["BackgroundImage"] = "";
            return View(group);
        }

        public ActionResult ProductionMachineDiagramAVF() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            var group = GetGroupMachineDiagram(MyUtilities.MySystem.CultureEN);
            ViewData = GetPageConfigData();
            ViewData["BackgroundImage"] = "";
            return View(group);
        }

        GroupMachineDiagram GetGroupMachineDiagram(string culture) {
            var group = new GroupMachineDiagram();
            try {
                var model = new List<List<MachineDiagram>>();
                var allMachineDiagram = new List<MachineDiagram>();
                using (var vfi = new vfiContext()) {

                    var cames = vfi.SelectDiagram1;
                    var staticStateList = MyUtilities.Machine.State.GetStaticStateList();
                    var productIds = cames.Select(c => c.ProductId).Distinct().ToList();
                    var materialIds = cames.Select(c => c.MaterialId).Distinct().ToList();
                    var lastTrackDate = cames.Min(c => c.DeliveryDate) ?? DateTime.Now;

                    var machineIds = cames.Select(c => c.MachineId).Distinct().ToList();
                    var machines = vfi.Machines.Where(m => m.Active && m.DiagramType == 1);
                    //var importDetailsSx1 = (from id in vfi.ImportFormSX1Detail
                    //                        where id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                    //                              id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                    //                              id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault().Transaction.Status ==
                    //                              (byte)MyUtilities.Transaction.Status.Approved &&
                    //                              productIds.Contains(id.ProductId) &&
                    //                              machineIds.Contains(id.MachineId.Value) &&
                    //                              materialIds.Contains(id.MaterialInventory.MaterialId) &&
                    //                              id.ImportFormSX1.MaterialUseDate > lastTrackDate
                    //                        orderby id.ImportFormSX1.MaterialUseDate descending
                    //                        select new {
                    //                            id.MaterialInventory.MaterialId,
                    //                            MachineId = id.MachineId ?? 0,
                    //                            id.ProductId,
                    //                            id.ImportFormSX1.MaterialUseDate,
                    //                            Quantity = id.Number1 + id.Number2 +
                    //                                       id.Processing1 + id.Processing2,
                    //                            MaterialUse = (id.MaterialUse1 + id.MaterialUse2) * id.MaterialInventory.UnitWeight,
                    //                        }).ToList();


                    var maxColumn = machines.Max(x => x.ColumnIndex).Value;
                    for (int i = 1; i <= maxColumn; i++) {
                        var machineColumns = machines.Where(c => c.ColumnIndex == i).OrderByDescending(c => c.RowIndex);
                        var listMachines = new List<MachineDiagram>();
                        foreach (var machine in machineColumns) {
                            var stateName = culture == MyUtilities.MySystem.CultureEN && !string.IsNullOrWhiteSpace(machine.MachineState.NameEN)
                                ? machine.MachineState.NameEN
                                : machine.MachineState.Description;
                            var entity = new MachineDiagram {
                                MachineId = machine.MachineId,
                                MachineName = machine.MachineName,
                                MachineIdName = machine.MachineIdName,              // moi them
                                Information = "",
                                MachineState = machine.StateId ?? 0,
                                WarrningColor = machine.MachineState.WarrningColor,
                                StateCode = machine.MachineState.StateCode,
                                StateName = stateName,
                                //MachineType = "Cames",
                                ProductId = 0,
                                ProductCode = "__",
                                MaterialCode = "__",
                                MachineFunction = machine.MachineFunction,
                            };
                            if (!staticStateList.Contains(entity.MachineState)) {
                                entity.StateCode = "M-06";
                            }
                            var camesInColumn = cames.Where(c => c.MachineId == entity.MachineId)
                                .OrderByDescending(c => c.ModifiedDate)
                                .FirstOrDefault();
                            if (camesInColumn != null) {
                                entity.ProductId = camesInColumn.ProductId;
                                entity.ProductCode = camesInColumn.ProductCode;
                                entity.ForecastsQuality = camesInColumn.Quantity;
                                entity.Productivity = camesInColumn.RealProductivity;
                                entity.MaterialCode = camesInColumn.MaterialCode;
                                entity.StartDate = camesInColumn.DeliveryDate;
                                entity.EndDate = camesInColumn.EndDate;
                                //var productions = importDetailsSx1.Where(id => id.MachineId == camesInColumn.MachineId &&
                                //    id.MaterialId == camesInColumn.MaterialId &&
                                //    id.ProductId == camesInColumn.ProductId
                                //    && id.MaterialUseDate >= camesInColumn.DeliveryDate
                                //    )
                                //    .ToList();
                                //entity.Production = productions.Sum(p => p.Quantity);
                                //entity.MaterialUse = productions.Sum(p => p.MaterialUse);
                            }
                            if (machine.ProcessingType != null) {
                                entity.MachineType = machine.ProcessingType.TypeName;
                            }
                            if (machine.MachineState.IsSetProduct) {
                                var state = vfi.MachineRepairForms
                                    .Where(mr => mr.MachineId == entity.MachineId &&
                                        mr.StateId == entity.MachineState &&
                                        mr.FinishDate == null)
                                    .OrderByDescending(mr => mr.CauseDate)
                                    .FirstOrDefault();
                                if (state != null) {
                                    entity.ProductCode = state.Product.ProductCode;
                                    if (state.Product.MaterialId != null)
                                        entity.MaterialCode = state.Product.Material.MaterialCode;
                                }
                            }
                            var startDate = DateTime.Now;
                            if (machine.ModifiedState != null) {
                                startDate = machine.ModifiedState.Value;
                            }
                            else if (machine.StartProductionDate != null) {
                                startDate = machine.StartProductionDate.Value;
                            }
                            entity.Seconds = MyUtilities.Function.RoundUp((DateTime.Now - startDate).TotalSeconds);
                            listMachines.Add(entity);
                        }
                        allMachineDiagram.AddRange(listMachines);
                        model.Add(listMachines);
                    }

                    group.ListMachineDiagram = model;

                    var states = from ms in vfi.MachineStates
                                 where staticStateList.Contains(ms.StateId)
                                 select new { 
                                     ms.StateId,
                                    ms.StateCode,
                                    ms.Description,
                                    ms.WarrningColor,
                                    ms.NameEN,
                                 };
                    var list = new List<MachineStateModel>();
                    foreach (var state in states) {
                        var fontColor = "black";
                        if (state.StateId == MyUtilities.Machine.State.BadState)
                            fontColor = "white";
                        var count = allMachineDiagram.Where(a => a.StateCode.Equals(state.StateCode)).ToList().Count;
                        var stateName = culture == MyUtilities.MySystem.CultureEN && !string.IsNullOrWhiteSpace(state.NameEN)
                            ? state.NameEN
                            : state.Description;
                        list.Add(new MachineStateModel() { StateCode = state.StateCode, Description = stateName, WarrningColor = state.WarrningColor, WarrningPoint = count, FontColor = fontColor });
                    }
                    var errorState = (from ms in vfi.MachineStates
                                      where ms.Active && !staticStateList.Contains(ms.StateId)
                                      select ms).FirstOrDefault();
                    var countError = allMachineDiagram.Where(a => a.StateCode.Equals("M-06")).ToList().Count;
                    if (errorState != null) {
                        var stateName = culture == MyUtilities.MySystem.CultureEN ? "Mild Damage" : "Hư nhẹ";
                        list.Add(new MachineStateModel() { StateCode = "M-06", Description = stateName, WarrningColor = errorState.WarrningColor, WarrningPoint = countError, FontColor = "black" });
                    }

                    group.ListState = list.OrderBy(l => l.StateCode).ToList();

                    var listFunction = allMachineDiagram.Select(md => md.MachineFunction).Distinct().OrderBy(mf => mf).ToList();                          // 29/01/2026  cmt
                    foreach (var func in listFunction) {
                        var count = allMachineDiagram.Where(md => md.MachineFunction == func).ToList().Count();
                        group.ListFunction.Add(new MachineFunctionModel() { Code = func + "", Count = count });
                    }

                    var listType = allMachineDiagram.Select(md => md.MachineType).Distinct().OrderBy(mf => mf).ToList();                                   // 29/01/2026   cmt
                    foreach (var type in listType) {
                        var count = allMachineDiagram.Where(md => md.MachineType.Equals(type)).ToList().Count();
                        group.ListType.Add(new MachineTypeModel() { Code = type, Count = count });
                    }

                }
            }
            catch (Exception ex) { throw ex; }
            return group;
        }

        public ActionResult ProductionMachineDiagramVF2() {
            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            var group = new GroupMachineDiagram();
            var model = new List<List<MachineDiagram>>();
            var allMachineDiagram = new List<MachineDiagram>();
            using (var vfi = new vfiContext()) {

                var cames = vfi.SelectDiagram3;
                var staticStateList = MyUtilities.Machine.State.GetStaticStateList();
                var productIds = cames.Select(c => c.ProductId).Distinct().ToList();
                var materialIds = cames.Select(c => c.MaterialId).Distinct().ToList();
                var lastTrackDate = cames.Min(c => c.DeliveryDate) ?? DateTime.Now;

                var machineIds = cames.Select(c => c.MachineId).Distinct().ToList();
                var machines = vfi.Machines.Where(m => machineIds.Contains(m.MachineId));

                var importDetailsSx1 = (from id in vfi.ImportFormSX1Detail
                                        where id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                              id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                              id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault().Transaction.Status ==
                                              (byte)MyUtilities.Transaction.Status.Approved &&
                                              productIds.Contains(id.ProductId) &&
                                              machineIds.Contains(id.MachineId.Value) &&
                                              materialIds.Contains(id.MaterialInventory.MaterialId) &&
                                              id.ImportFormSX1.MaterialUseDate > lastTrackDate
                                        orderby id.ImportFormSX1.MaterialUseDate descending
                                        select new {
                                            id.MaterialInventory.MaterialId,
                                            MachineId = id.MachineId ?? 0,
                                            id.ProductId,
                                            id.ImportFormSX1.MaterialUseDate,
                                            Quantity = id.Number1 + id.Number2 +
                                                       id.Processing1 + id.Processing2,
                                            MaterialUse = (id.MaterialUse1 + id.MaterialUse2) * id.MaterialInventory.UnitWeight,
                                        }).ToList();
                for (int i = 1; i <= 4; i++) {
                    var machineColumns = machines.Where(c => c.ColumnIndex == i).OrderByDescending(c => c.RowIndex);
                    var listMachines = new List<MachineDiagram>();
                    foreach (var machine in machineColumns) {
                        var entity = new MachineDiagram {
                            MachineId = machine.MachineId,
                            MachineName = machine.MachineName,
                            Information = "",
                            MachineState = machine.StateId ?? 0,
                            WarrningColor = machine.MachineState.WarrningColor,
                            StateCode = machine.MachineState.StateCode,
                            StateName = machine.MachineState.Description,
                            //MachineType = "Cames",
                            ProductCode = "__",
                            MaterialCode = "__",
                            MachineFunction = machine.MachineFunction,
                        };
                        if (!staticStateList.Contains(entity.MachineState)) {
                            entity.StateCode = "M-06";
                        }
                        var camesInColumn = cames.Where(c => c.MachineId == entity.MachineId)
                            .OrderByDescending(c => c.ModifiedDate)
                            .FirstOrDefault();
                        if (camesInColumn != null) {
                            entity.ProductId = camesInColumn.ProductId;
                            entity.ProductCode = camesInColumn.ProductCode;
                            entity.ForecastsQuality = camesInColumn.Quantity;
                            entity.Productivity = camesInColumn.RealProductivity;
                            entity.MaterialCode = camesInColumn.MaterialCode;
                            entity.StartDate = camesInColumn.DeliveryDate;
                            entity.EndDate = camesInColumn.EndDate;
                            var productions = importDetailsSx1.Where(id => id.MachineId == camesInColumn.MachineId &&
                                id.MaterialId == camesInColumn.MaterialId &&
                                id.ProductId == camesInColumn.ProductId &&
                                id.MaterialUseDate >= camesInColumn.DeliveryDate).ToList();
                            entity.Production = productions.Sum(p => p.Quantity);
                            entity.MaterialUse = productions.Sum(p => p.MaterialUse);
                        }
                        if (machine.ProcessingType != null) {
                            entity.MachineType = machine.ProcessingType.TypeName;
                        }
                        if (machine.MachineState.IsSetProduct) {
                            var state = vfi.MachineRepairForms
                                .Where(mr => mr.MachineId == entity.MachineId &&
                                    mr.StateId == entity.MachineState &&
                                    mr.FinishDate == null)
                                .OrderByDescending(mr => mr.CauseDate)
                                .FirstOrDefault();
                            if (state != null) {
                                entity.ProductCode = state.Product.ProductCode;
                                if (state.Product.MaterialId != null)
                                    entity.MaterialCode = state.Product.Material.MaterialCode;
                            }
                        }
                        var startDate = DateTime.Now;
                        if (machine.ModifiedState != null) {
                            startDate = machine.ModifiedState.Value;
                        }
                        else if (machine.StartProductionDate != null) {
                            startDate = machine.StartProductionDate.Value;
                        }
                        entity.Seconds = MyUtilities.Function.RoundUp((DateTime.Now - startDate).TotalSeconds);
                        listMachines.Add(entity);
                    }
                    allMachineDiagram.AddRange(listMachines);
                    model.Add(listMachines);
                }

                group.ListMachineDiagram = model;

                var states = from ms in vfi.MachineStates
                             where staticStateList.Contains(ms.StateId)
                             select new {
                                 ms.StateId,
                                 ms.StateCode,
                                 ms.Description,
                                 ms.WarrningColor,
                             };
                var list = new List<MachineStateModel>();
                foreach (var item in states) {
                    var fontColor = "black";
                    if (item.StateId == MyUtilities.Machine.State.BadState)
                        fontColor = "white";
                    var count = allMachineDiagram.Where(a => a.StateCode.Equals(item.StateCode)).ToList().Count;
                    list.Add(new MachineStateModel() { StateCode = item.StateCode, Description = item.Description, WarrningColor = item.WarrningColor, WarrningPoint = count, FontColor = fontColor });
                }
                var errorState = (from ms in vfi.MachineStates
                                  where ms.Active && !staticStateList.Contains(ms.StateId)
                                  select ms).FirstOrDefault();
                var countError = allMachineDiagram.Where(a => a.StateCode.Equals("M-06")).ToList().Count;
                if (errorState != null) {
                    list.Add(new MachineStateModel() { StateCode = "M-06", Description = "Hư nhẹ", WarrningColor = errorState.WarrningColor, WarrningPoint = countError, FontColor = "black" });
                }

                group.ListState = list.OrderBy(l => l.StateCode).ToList();

                var listFunction = allMachineDiagram.Select(md => md.MachineFunction).Distinct().OrderBy(mf => mf).ToList();
                foreach (var func in listFunction) {
                    var count = allMachineDiagram.Where(md => md.MachineFunction == func).ToList().Count();
                    group.ListFunction.Add(new MachineFunctionModel() { Code = func + "", Count = count });
                }

                var listType = allMachineDiagram.Select(md => md.MachineType).Distinct().OrderBy(mf => mf).ToList();
                foreach (var type in listType) {
                    var count = allMachineDiagram.Where(md => md.MachineType.Equals(type)).ToList().Count();
                    group.ListType.Add(new MachineTypeModel() { Code = type, Count = count });
                }

            }
            ViewData = GetPageConfigData();
            ViewData["BackgroundImage"] = "";
            return View(group);
        }

        public ActionResult ProductionMachineDiagramCNC() {

            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            var group = new GroupMachineDiagram();
            var model = new List<List<MachineDiagram>>();
            var allMachineDiagram = new List<MachineDiagram>();
            using (var vfi = new vfiContext()) {
                var cncs = vfi.SelectDiagram2;
                var staticStateList = MyUtilities.Machine.State.GetStaticStateList();
                var productIds = cncs.Select(c => c.ProductId).Distinct().ToList();
                var materialIds = cncs.Select(c => c.MaterialId).Distinct().ToList();
                var lastTrackDate = cncs.Min(c => c.DeliveryDate) ?? DateTime.Now;

                var machineIds = cncs.Select(c => c.MachineId).Distinct().ToList();
                var machines = vfi.Machines.Where(m => machineIds.Contains(m.MachineId));

                //var importDetailsSx1 = (from id in vfi.ImportFormSX1Detail                                                              
                //                        where
                //                        id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                //                              id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                //                              id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault().Transaction.Status ==
                //                              (byte)MyUtilities.Transaction.Status.Approved &&
                //                              productIds.Contains(id.ProductId) &&
                //                              machineIds.Contains(id.MachineId.Value) &&
                //                              materialIds.Contains(id.MaterialInventory.MaterialId) &&
                //                              id.ImportFormSX1.MaterialUseDate > lastTrackDate
                //                        orderby id.ImportFormSX1.MaterialUseDate descending
                //                        select new {
                //                            id.MaterialInventory.MaterialId,
                //                            MachineId = id.MachineId ?? 0,
                //                            id.ProductId,
                //                            id.ImportFormSX1.MaterialUseDate,
                //                            Quantity = id.Number1 + id.Number2 +
                //                                       id.Processing1 + id.Processing2,
                //                            MaterialUse = (id.MaterialUse1 + id.MaterialUse2) * id.MaterialInventory.UnitWeight,
                //                        }).ToList();
                var importDetailsSx12 = vfi.ImportFormSX1Detail
                        .Where(x => productIds.Contains(x.ProductId)
                            && machineIds.Contains(x.MachineId.Value)
                            && materialIds.Contains(x.MaterialInventory.MaterialId)
                            && x.ImportFormSX1.MaterialUseDate > lastTrackDate
                            && x.ImportFormSX1.Status == (byte)MyUtilities.Transaction.Status.Approved)
                        .GroupBy(x => new { x.ProductId, x.MaterialInventory.MaterialId, MachineId = x.MachineId.Value })
                        .Select(x => new {
                            x.Key,
                            Quantity = x.Sum(y => y.Number1 + y.Number2 + y.Processing1 + y.Processing2),
                            MaterialUse = x.Sum(y => y.MaterialUse1 + y.MaterialUse2)
                        })
                        .ToList();

                var maxColumn = machines.Max(m => m.ColumnIndex);
                for (int i = 1; i <= maxColumn; i++) {
                    var machineColumns = machines.Where(c => c.ColumnIndex == i).OrderByDescending(c => c.RowIndex);
                    var listMachines = new List<MachineDiagram>();
                    foreach (var machine in machineColumns) {
                        var entity = new MachineDiagram {
                            MachineId = machine.MachineId,
                            MachineName = machine.MachineName,
                            Information = "",
                            MachineState = machine.StateId ?? 0,
                            WarrningColor = machine.MachineState.WarrningColor,
                            StateCode = machine.MachineState.StateCode,
                            StateName = machine.MachineState.Description,
                            //MachineType = "Cnc",
                            ProductCode = "__",
                            MaterialCode = "__",
                            MachineFunction = machine.MachineFunction,
                        };
                        if (!staticStateList.Contains(entity.MachineState)) {
                            entity.StateCode = "M-06";
                        }
                        var cnc = cncs.Where(c => c.MachineId == entity.MachineId)
                            .OrderByDescending(c => c.ModifiedDate)
                            .FirstOrDefault();
                        if (cnc != null) {
                            entity.ProductId = cnc.ProductId;
                            entity.ProductCode = cnc.ProductCode;
                            entity.ForecastsQuality = cnc.Quantity;
                            entity.Productivity = cnc.RealProductivity;
                            entity.MaterialCode = cnc.MaterialCode;
                            entity.StartDate = cnc.DeliveryDate;
                            entity.EndDate = cnc.EndDate;


                            //var productions = importDetailsSx1.Where(id => id.MachineId == cnc.MachineId &&
                            //    id.MaterialId == cnc.MaterialId &&
                            //    id.ProductId == cnc.ProductId &&
                            //    id.MaterialUseDate >= cnc.DeliveryDate).ToList();



                            var productions = importDetailsSx12.Where(id => id.Key.MachineId == cnc.MachineId &&                              
                                id.Key.MaterialId == cnc.MaterialId &&
                                id.Key.ProductId == cnc.ProductId).ToList();
                            entity.Production = productions.Sum(p => p.Quantity);
                            entity.MaterialUse = productions.Sum(p => p.MaterialUse);
                        }

                        if (machine.ProcessingType != null) {
                            entity.MachineType = machine.ProcessingType.TypeName;
                        }
                        if (machine.MachineState.IsSetProduct) {
                            var state = vfi.MachineRepairForms
                                .Where(mr => mr.MachineId == entity.MachineId &&
                                    mr.StateId == entity.MachineState &&
                                    mr.FinishDate == null)
                                .OrderByDescending(mr => mr.CauseDate)
                                .FirstOrDefault();
                            if (state != null) {
                                entity.ProductCode = state.Product.ProductCode;
                                if (state.Product.MaterialId != null)
                                    entity.MaterialCode = state.Product.Material.MaterialCode;
                            }
                        }
                        var startDate = DateTime.Now;
                        if (machine.ModifiedState != null) {
                            startDate = machine.ModifiedState.Value;
                        }
                        else if (machine.StartProductionDate != null) {
                            startDate = machine.StartProductionDate.Value;
                        }
                        entity.Seconds = MyUtilities.Function.RoundUp((DateTime.Now - startDate).TotalSeconds);
                        listMachines.Add(entity);
                    }
                    allMachineDiagram.AddRange(listMachines);
                    model.Add(listMachines);
                }

                group.ListMachineDiagram = model;

                var states = from ms in vfi.MachineStates
                             where staticStateList.Contains(ms.StateId)
                             select new {
                                 ms.StateId,
                                 ms.StateCode,
                                 ms.Description,
                                 ms.WarrningColor,
                             };
                var list = new List<MachineStateModel>();
                foreach (var item in states) {
                    var fontColor = "black";
                    if (item.StateId == MyUtilities.Machine.State.BadState)
                        fontColor = "white";
                    var count = allMachineDiagram.Where(a => a.StateCode.Equals(item.StateCode)).ToList().Count;
                    list.Add(new MachineStateModel() { StateCode = item.StateCode, Description = item.Description, WarrningColor = item.WarrningColor, WarrningPoint = count, FontColor = fontColor });
                }
                var errorState = (from ms in vfi.MachineStates
                                  where ms.Active && !staticStateList.Contains(ms.StateId)
                                  select ms).FirstOrDefault();
                var countError = allMachineDiagram.Where(a => a.StateCode.Equals("M-06")).ToList().Count;
                if (errorState != null) {
                    list.Add(new MachineStateModel() { StateCode = "M-06", Description = "Hư nhẹ", WarrningColor = errorState.WarrningColor, WarrningPoint = countError, FontColor = "black" });
                }

                group.ListState = list.OrderBy(l => l.StateCode).ToList();

                var listFunction = allMachineDiagram.Select(md => md.MachineFunction).Distinct().OrderBy(mf => mf).ToList();
                foreach (var func in listFunction) {
                    var count = allMachineDiagram.Where(md => md.MachineFunction == func).ToList().Count();
                    group.ListFunction.Add(new MachineFunctionModel() { Code = func + "", Count = count });
                }

                var listType = allMachineDiagram.Select(md => md.MachineType).Distinct().OrderBy(mf => mf).ToList();
                foreach (var type in listType) {
                    var count = allMachineDiagram.Where(md => md.MachineType.Equals(type)).ToList().Count();
                    group.ListType.Add(new MachineTypeModel() { Code = type, Count = count });
                }

            }
            ViewData = GetPageConfigData();
            ViewData["BackgroundImage"] = "";
            return View(group);
        }

        public ActionResult MachineLog(int id) {

            if (!Request.IsAuthenticated) {
                return RedirectToAction("Index", "Home", new { area = "" });
            } 
            try {
                using (var vfi = new vfiContext()) {
                    var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == id);
                    if (machine == null) throw new AggregateException("Lỗi! không tìm thấy máy này");
                    var entity = new MachineDiagram {
                        MachineId = machine.MachineId,
                        MachineName = machine.MachineName,
                        Information = "",
                        MachineState = machine.StateId ?? 0,
                        ProductCode = "__",
                        MaterialCode = "__",
                    };
                    entity.DiagramName = entity.GetDiagramName(machine);
                    var lastTrackUp = MyUtilities.Machine.LastTrackUpMachine(id, null, null, DateTime.Now);
                    if (lastTrackUp != null) {
                        entity.ProductCode = lastTrackUp.ProductCode;
                        entity.StartDate = lastTrackUp.DeliveryDate;
                        //entity.ProductionPerDay = entity.ProductionPerDay

                        var importProductionDetails = (from x in vfi.ImportFormSX1Detail
                                                       where x.MachineId == lastTrackUp.MachineId
                                                       && x.ProductId == lastTrackUp.ProductId
                                                       && x.ImportFormSX1.MaterialUseDate >= lastTrackUp.DeliveryDate
                                                       orderby x.ImportFormSX1.MaterialUseDate descending
                                                       select x).ToList();
                        if (importProductionDetails.Any()) {
                            var detail = importProductionDetails.FirstOrDefault();
                            entity.Productivity = detail.ProductionRate;
                            entity.Production = importProductionDetails.Sum(x => x.Number1 + x.Number2);
                            entity.ProcessingQuantity = importProductionDetails.Sum(x => x.Processing1 + x.Processing2);
                            entity.DefectQuantity = importProductionDetails.Sum(x => x.DefectProduct1 + x.DefectProduct2);
                            entity.MaterialUse = importProductionDetails.Sum(x => x.MaterialUse1 + x.MaterialUse2);
                            entity.MaterialCode =
                                    MyUtilities.Material.GetMaterialInvDesignNo(
                                    detail.MaterialInventory.Material.MaterialName,
                                        detail.MaterialInventory.Material.OutDiameter,
                                        detail.MaterialInventory.Material.InDiameter,
                                        detail.MaterialInventory.Length,
                                        detail.MaterialInventory.Material.DiameterType,
                                        detail.MaterialInventory.Material.Shape,
                                        detail.MaterialInventory.Vendor.VendorCode,
                                        detail.MaterialInventory.LotNumber);
                        }
                        //entity.MaterialCode = lastTrackUp.MaterialCode;
                        //var materialInvId = (from ifd in vfi.ImportFormSX1Detail
                        //                     where ifd.ProductId == lastTrackUp.ProductId && ifd.MachineId == id
                        //                     orderby ifd.ImportFormSX1.MaterialUseDate descending
                        //                     select ifd.MaterialInvId).First();
                        //if (materialInvId == null) {
                        //    entity.MaterialCode = "__";
                        //}
                        //else {
                        //    var materialInv =
                        //        vfi.MaterialInventories.FirstOrDefault(m => m.MaterialInventoryId == materialInvId);
                        //    entity.MaterialCode =
                        //            MyUtilities.Material.GetMaterialInvDesignNo(
                        //            materialInv.Material.MaterialName,
                        //                materialInv.Material.OutDiameter,
                        //                materialInv.Material.InDiameter,
                        //                materialInv.Length,
                        //                materialInv.Material.DiameterType,
                        //                materialInv.Material.Shape,
                        //                materialInv.Vendor.VendorCode,
                        //                materialInv.LotNumber);
                        //}
                    }

                    //if (machine.ProductActive == null) {
                    //    entity.ProductCode = "__";
                    //    entity.EndDate = null;
                    //    entity.StartDate = null;
                    //    //entity.Number = 0;
                    //    //entity.ProductionPerDay = 0;
                    //    entity.MaterialCode = "__";
                    //}
                    //else {
                    //    entity.ProductCode = machine.Product.ProductCode;
                    //    entity.StartDate = machine.StartProductionDate;
                    //    //entity.Number = machine.Number ?? 0;
                    //    //entity.ProductionPerDay = machine.DayRate ?? 0;
                    //    var materialInvId = (from ifd in vfi.ImportFormSX1Detail
                    //                         where ifd.ProductId == machine.ProductActive
                    //                         orderby ifd.DetailId descending
                    //                         select ifd.MaterialInvId).First();
                    //    if (materialInvId == null)
                    //        entity.MaterialCode = "__";
                    //    else {
                    //        var materialInv =
                    //            vfi.MaterialInventories.FirstOrDefault(m => m.MaterialInventoryId == materialInvId);
                    //        entity.MaterialCode =
                    //                MyUtilities.Material.GetMaterialInvDesignNo(
                    //                materialInv.Material.MaterialName,
                    //                    materialInv.Material.OutDiameter,
                    //                    materialInv.Material.InDiameter,
                    //                    materialInv.Length,
                    //                    materialInv.Material.DiameterType,
                    //                    materialInv.Material.Shape,
                    //                    materialInv.Vendor.VendorCode,
                    //                    materialInv.LotNumber);
                    //    }

                    ViewData = GetPageConfigData();
                    return View(entity);
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("ErrorMachine", ex.Message);
            }
            ViewData = GetPageConfigData();
            return View();
        }

        #endregion

        [GridAction]
        public ActionResult SelectMachineStateById(int machineId) {
            var model = GetMachineStateById(machineId);
            return View(new GridModel(model));
        }

        List<MachineModel> GetMachineStateById(int machineId) {
            var model = new List<MachineModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == machineId);
                    if (machine == null) throw new AggregateException("Lỗi! không tìm thấy máy này");
                    var entity = new MachineModel {
                        MachineId = machine.MachineId,
                        MachineName = machine.MachineName,
                        StateId = machine.StateId ?? 1,
                        StateName = machine.MachineState.Description,
                        StateColor = machine.MachineState.WarrningColor,
                        Active = machine.Active,
                        Production2 = machine.Production2,
                    };
                    model.Add(entity);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetMachineStateById", ex.Message);
            }
            return model;
        }

        [GridAction]
        public ActionResult UpdateMachineStateById(int machineId, MachineModel updateModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == updateModel.MachineId);
                    if (machine == null)
                        throw new AggregateException("Lỗi ! Không tìm thấy máy");
                    var stateId = 1;
                    try {
                        stateId = Convert.ToInt32(updateModel.StateName);
                    }
                    catch (FormatException) {
                        stateId = machine.StateId ?? 1;
                    }
                    machine.StateId = stateId;
                    var machineLog = new MachineLog {
                        MachineId = updateModel.MachineId,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        Note = updateModel.GetMachineStateLog(),
                        Type = (byte)MachineLogTypeEnum.State,
                    };
                    vfi.MachineLogs.Add(machineLog);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMachineStateById", ex.Message);
            }
            return View(new GridModel(GetMachineStateById(machineId)));
        }

        [GridAction]
        public ActionResult SelectMachinePlanById(int machineId) {
            var model = GetMachinePlanById(machineId);
            return View(new GridModel(model));
        }

        private List<MachineDiagram> GetMachinePlanById(int machineId) {

            var model = new List<MachineDiagram>();
            //try {
            //    using (var vfi = new vfiContext()) {
            //        var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == machineId);
            //        if (machine == null) throw new AggregateException("Lỗi! không tìm thấy máy này");
            //        var entity = new MachineDiagram {
            //            MachineId = machine.MachineId,
            //            MachineName = machine.MachineName,
            //        };
            //        if (machine.ProductActive == null) {
            //            entity.ProductCode = "__";
            //            entity.EndDate = null;
            //            entity.StartDate = null;
            //            //entity.MaterialCode = "__";
            //            entity.Number = 0;
            //            entity.ProductionPerDay = 0;
            //        }
            //        else {
            //            entity.ProductCode = machine.Product.ProductCode;
            //            entity.StartDate = machine.StartProductionDate;
            //            //entity.MaterialCode =
            //            entity.Production = machine.Number ?? 0;
            //            entity.ProductionPerDay = machine.DayRate ?? 0;
            //            //.MaterialCode = "__";
            //        }
            //        model.Add(entity);
            //    }
            //}
            //catch (Exception ex) {
            //    ModelState.AddModelError("SelectMachinePlanById", ex.Message);
            //}
            return model;
        }

        [GridAction]
        public ActionResult UpdateMachinePlan(MachineDiagram updateModel) {

            //try {
            //if (!Request.IsAuthenticated) {
            //    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
            //}
            //    using (var vfi = new vfiContext()) {
            //        var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == updateModel.MachineId);
            //        if (machine == null)
            //            throw new AggregateException("Lỗi ! Không tìm thấy máy");
            //        machine.StartProductionDate = updateModel.StartDate;
            //        //machine.DayRate = Convert.ToInt32(updateModel.ProductionPerDay);
            //        machine.Number = updateModel.Number;
            //        machine.Note = updateModel.Note;
            //        machine.ModifiedDate = DateTime.Now;
            //        machine.ModifiedUser = HttpContext.User.Identity.Name;
            //        int? productId = null;
            //        if (!updateModel.ProductCode.Equals("--")) {
            //            try {
            //                productId = Convert.ToInt32(updateModel.ProductCode);
            //            }
            //            catch (FormatException) {
            //                productId =
            //                    vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(updateModel.ProductCode))
            //                       .ProductId;
            //            }
            //            catch (Exception) {
            //                productId = null;
            //            }
            //        }
            //        if (productId != null) {
            //            machine.ProductActive = productId;
            //            var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
            //            var machinePlanModel = new MachineDiagram {
            //                ProductCode = product.ProductCode,
            //                Number = machine.Number ?? 0,
            //                //ProductionPerDay = machine.DayRate ?? 0,
            //                StartDate = machine.StartProductionDate,
            //                Note = machine.Note,
            //            };
            //            var machineLog = new MachineLog {
            //                MachineId = updateModel.MachineId,
            //                ModifiedDate = DateTime.Now,
            //                ModifiedUser = HttpContext.User.Identity.Name,
            //                Note = machinePlanModel.GetLogNote(),
            //                Type = (byte)MachineLogTypeEnum.Plan,
            //            };
            //            vfi.MachineLogs.Add(machineLog);
            //        }
            //        else {
            //            var machineLog = new MachineLog {
            //                MachineId = updateModel.MachineId,
            //                ModifiedDate = DateTime.Now,
            //                ModifiedUser = HttpContext.User.Identity.Name,
            //                Note = "Sản phẩm xuống máy: " + machine.Product.ProductCode,
            //                Type = (byte)MachineLogTypeEnum.Plan,
            //            };
            //            machine.ProductActive = null;
            //            vfi.MachineLogs.Add(machineLog);
            //        }
            //        vfi.SaveChanges();
            //    }
            //}
            //catch (Exception ex) {
            //    ModelState.AddModelError("UpdateMachinePlan", ex.Message);
            //}

            return View(new GridModel(GetMachinePlanById(updateModel.MachineId)));

        }

        [GridAction]
        public ActionResult SelectMachineLogsById(int machineId) {
            var model = new List<MachineLogModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var machineLogs = vfi.MachineLogs.Where(ml => ml.MachineId == machineId);
                    foreach (var machineLog in machineLogs) {
                        var entity = new MachineLogModel();
                        entity.MachineId = machineLog.MachineId;
                        entity.MachineName = machineLog.Machine.MachineName;
                        entity.ModifiedDate = machineLog.ModifiedDate;
                        entity.ModifiedUser = machineLog.ModifiedUser;
                        entity.TypeName = CastMachineLogTypeEnum.GetText(machineLog.Type);
                        entity.Note = machineLog.Note;
                        entity.DateLog = machineLog.DateLog ?? DateTime.Now;
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMachineLogsById", ex.Message);
            }
            return View(new GridModel(model.OrderByDescending(m => m.ModifiedDate).ThenByDescending(m => m.DateLog).ThenBy(m => m.TypeName)));
        }





        //16/03/2026
        [GridAction]
        public ActionResult TrackUpMachineList(int machineId, string fromDate, string toDate) {
            var model = GetTrackUpMachine(machineId, fromDate, toDate);
            return View(new GridModel(model));
        }

        // 30/03/2026
        [GridAction]
        public ActionResult GetProductDetail(int machineId, string fromDate, string toDate) {
            var data = GetTrackUpMachine(machineId, fromDate, toDate)
                        .Where(x => x.MachineId == machineId)
                        .SelectMany(x => x.ProductDetails)
                        .ToList();

            return View(new GridModel(data));
        }

        // 30/03/2026
        [GridAction]
        public ActionResult GetMaterialDetail(int machineId, string fromDate, string toDate) {
            var data = GetTrackUpMachine(machineId, fromDate, toDate)
                        .Where(x => x.MachineId == machineId)
                        .SelectMany(x => x.MaterialDetails)
                        .ToList();

            return View(new GridModel(data)); 
        }        
               
        // 30/03/2026
        [GridAction]
        public ActionResult GetToolDetail(int machineId, string fromDate, string toDate) {
            var data = GetTrackUpMachine(machineId, fromDate, toDate)
                        .Where(x => x.MachineId == machineId)
                        .SelectMany(x => x.ToolDetails)
                        .ToList();

            return View(new GridModel(data));
        }


        // 03/04/2026
        [GridAction]
        public ActionResult GetRepairDetail(int machineId, string fromDate, string toDate) {
            var data = GetTrackUpMachine(machineId, fromDate, toDate)
                        .Where(x => x.MachineId == machineId)
                        .SelectMany(x => x.RepairDetails)
                        .ToList();

            return View(new GridModel(data));
        }



        //16/03/2026
        public List<MachineHistoryModel> GetTrackUpMachine(int machineId, string fromDate, string toDate) {
            var model = new List<MachineHistoryModel>();
            try {
                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var fDate = MyUtilities.Function.ParseDate(fromDate);
                    var tDate = MyUtilities.Function.ParseLastDateTime(toDate);

                    var results = (from d in vfi.ImportFormSX1Detail
                                   join h in vfi.ImportFormSX1
                                       on d.ImportId equals h.ImportId
                                   where ((machineId == 0 || d.MachineId == machineId) &&
                                         h.MaterialUseDate >= fDate &&
                                         h.MaterialUseDate <= tDate
                                         )
                                   select new {
                                       d.ImportId,
                                       d.Machine,
                                       h.MaterialUseDate,
                                       d.MachineId,
                                       StateId = d.Machine1.MachineState.StateId,
                                       StateDate = d.Machine1.ModifiedState,
                                       
                                       //product
                                       d.ProductId,
                                       ProductCode = d.Product.ProductCode,
                                       ProductLenght = d.Product.Length,
                                       ProductionNumber = d.Number1 + d.Number2,
                                       DefectNumber = d.DefectProduct1 + d.DefectProduct2,
                                       ProductUnitPrice = d.Product.UnitPrice,
                                       Currency = d.Product.Currency,
                                       Productivity = d.Product.Productivity,
                                       Processing = d.Processing1 + d.Processing2,
                                       TotalNumber = (d.Number1 + d.Number2) + (d.Processing1 + d.Processing2) + (d.DefectProduct1 + d.DefectProduct2),
                                       NGNumber = (d.Processing1 + d.Processing2) + (d.DefectProduct1 + d.DefectProduct2),
                                       
                                       // Material
                                       d.MaterialInvId,
                                       MaterialUsed = d.MaterialUse1 + d.MaterialUse2,
                                       MaterialId = d.MaterialInventory.MaterialId,
                                       MaterialUnitPrice = d.MaterialInventory.UnitPrice,
                                       MaterialUnitWeight = d.MaterialInventory.UnitWeight,
                                       MaterialCode = d.MaterialInventory.Material.MaterialCode,
                                       LotMaterial = d.MaterialInventory.LotNumber,
                                       ProductionRate = d.Product.ProductionRate ?? 0,

                                  }).OrderBy(m => m.Machine).ToList();
                    var exchangeRate2 = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.ExchangeToVndRate2);
                    var EUR = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.ExchangeToVndRate3);

                    // Machine
                    var machineIds = results.Select(t => t.MachineId).Distinct().ToList();
                    // MachineState
                    var machineStateIdList = results.Select(t => t.StateId).ToList();
                    var getMachineState = vfi.MachineStates.Where(t => machineStateIdList.Contains(t.StateId))
                                                           .Select(t => new {
                                                               StateId = t.StateId,
                                                               Description = t.Description,
                                                           }).OrderByDescending(t => t.StateId).Distinct().ToList();
                    //var lastStateDate = results.Where(t => t.StateId == x.FirstOrDefault().StateId)
                    //                     .Select(t => t.StateDate)
                    //                     .LastOrDefault();




                    // Material
                    var lotNumberList = results.Select(t => t.LotMaterial).Distinct().ToList();
                    var materialIds = results.Select(t => t.MaterialId).Distinct().ToList();
                    var materialDict = vfi.Materials
                        .Where(t => materialIds.Contains(t.MaterialId))
                        .ToDictionary(t => t.MaterialId, t => t.MaterialCode);

                    var materialLenghtList = vfi.MaterialInventories.Where(t => materialIds.Contains(t.MaterialId)
                                                                            && lotNumberList.Contains(t.LotNumber))
                                                                    .Select(t => new {
                                                                        t.Length,
                                                                        t.MaterialId,
                                                                        t.LotNumber,
                                                                    }).Distinct().ToList();

                    // Product
                    var productIds = results.Select(t => t.ProductId).Distinct().ToList();
                    var productProductivityList = vfi.Products.Where(t => productIds.Contains(t.ProductId))
                                                     .Select(p => new{
                                                        p.Productivity,
                                                        p.ProductId,
                                                        ProductLenght = p.Length,
                                                  }).Distinct().ToList();

                    // Tool RCM
                    var exp = (from ed in vfi.ExportToolDetails
                               where ed.ExportTool.TransactionFpt.Status == 2
                                  && ed.ExportTool.TransactionFpt.TransactionDate >= fDate
                                  && ed.ExportTool.TransactionFpt.TransactionDate <= tDate
                                  && machineIds.Contains(ed.MachineId)
                               select new {
                                   ed.MachineId,
                                   ed.ToolInventory.ToolId,
                                   ed.ToolInventory.UnitPrice,
                                   ed.Quantity,
                                   ed.ToolInventory.Tool.ToolFullCode,
                                   ed.ToolInventory.Tool.ToolName,
                               }).ToList();


                    // repair machine
                    var repairList = (from re in vfi.RepairFormDetails
                                      where re.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete
                                            && re.MachineRepairForm.CauseDate <= tDate
                                            && (re.FinishDate == null || re.FinishDate >= fDate)
                                            && machineIds.Contains(re.MachineRepairForm.MachineId)
                                            && re.Status == 2
                                      select new {
                                          MachineId = re.MachineRepairForm.MachineId,
                                          MachineName = re.MachineRepairForm.Machine.MachineName,
                                          re.DetailId,
                                          re.StartDate,
                                          re.FinishDate,
                                          HowToFix = re.MachineStateDetail.Description,
                                          ProductCode = re.MachineRepairForm.Product.ProductCode,
                                          CauseDate = re.MachineRepairForm.CauseDate,
                                          ErrorCause = re.MachineRepairForm.ErrorCauseForm.Name,
                                          StateCode = re.MachineRepairForm.MachineState.StateCode,
                                          Descripbe = re.MachineRepairForm.MachineState.Description,
                                          Status = re.MachineRepairForm.Status,
                                      }).ToList()
                                        .GroupBy(re => new { re.MachineId, re.CauseDate })
                                        .Select(g => new {
                                            MachineId = g.Key.MachineId,
                                            CauseDate = g.Key.CauseDate,
                                            MachineName = g.First().MachineName,
                                            Repairs = g.Select(re => {
                                                var finishDate = (re.FinishDate == null || re.FinishDate > tDate)
                                                             ? tDate
                                                             : re.FinishDate.Value;
                                                var causeDate = re.CauseDate < fDate ? fDate : re.CauseDate;


                                                var causeDate1 = causeDate;
                                                var finishDate1 = finishDate;
                                                bool causeIsSunday = causeDate1.DayOfWeek == DayOfWeek.Sunday;
                                                bool finishIsSunday = finishDate1.DayOfWeek == DayOfWeek.Sunday;

                                                double checkErrorTime = 0;

                                                if (causeIsSunday && finishIsSunday) {
                                                    checkErrorTime = 0;
                                                }

                                                else if (causeIsSunday) {
                                                        // Đẩy causeDate sang thứ 2
                                                        var hoursOverMonday = 23 - causeDate1.Hour;
                                                        var minuteOverMonday = 60 - causeDate1.Minute;
                                                        causeDate1 = causeDate1.AddHours(hoursOverMonday).AddMinutes(minuteOverMonday);
                                                        checkErrorTime = (finishDate - causeDate1).TotalHours;
                                                    }

                                                else if (finishIsSunday) {
                                                        // Lùi finish về trước Chủ nhật
                                                        var hourBeforeSunday = finishDate1.Hour;
                                                        var minuteBeforSunday = finishDate1.Minute;
                                                        finishDate1 = finishDate1.AddHours(-hourBeforeSunday).AddMinutes(-minuteBeforSunday).AddSeconds(-1);
                                                        checkErrorTime = (finishDate1 - causeDate).TotalHours;
                                                    }

                                                else{ checkErrorTime = (((finishDate - causeDate).TotalHours)
                                                        - (((int)Math.Round((finishDate - causeDate).TotalDays)
                                                        - (MyUtilities.Function.DaysNoSunDay(causeDate, finishDate) - 1)) * 24));
                                                }

                                                return new {
                                                    re.DetailId,
                                                    re.MachineId,
                                                    ProductCode = re.ProductCode,
                                                    StateCode = re.StateCode,
                                                    Descripbe = re.Descripbe,
                                                    ErrorCause = re.ErrorCause,
                                                    HowToFix = re.HowToFix,
                                                    CauseDate = re.CauseDate,
                                                    FinishDate = finishDate,
                                                    CheckErrorTime = checkErrorTime,
                                                    causeDate1 = causeDate1,
                                                    finishDate1 = finishDate1,
                                                };
                                            }).ToList()
                                        })
                                        .ToList();








                    var dayCount = ((MyUtilities.Function.DaysNoSunDay(fDate, tDate)) -1);
                    var fullDayTiming = 20;
                    var maxRunTime = MyUtilities.Function.RoundDown( dayCount * fullDayTiming);



                   // get trackup
                    var DayBefore = MyUtilities.Function.ParseDate(fromDate).AddDays(-15);
                    //var DayAfter = MyUtilities.Function.ParseLastDateTime(toDate).AddDays(+1);
                    var trackupList = (from tr in vfi.TrackUpMachines
                                       where machineIds.Contains(tr.MachineId)
                                             && tr.StartDate >= DayBefore
                                             && tr.StartDate <= tDate
                                       group tr by new { tr.MachineId, tr.ProductId, tr.MaterialId, tr.RealProductivity, tr.RealRate, tr.StartDate } into g
                                       select new {
                                           MachineId = g.Key.MachineId,
                                           ProductId = g.Key.ProductId,
                                           MaterialId = g.Key.MaterialId,
                                           Productivity = g.Key.RealProductivity,
                                           RealRate = g.Key.RealRate,
                                           StartDate = g.Key.StartDate,
                                       }).OrderBy(t => t.StartDate).ToList();


                    // model
                    model = results.GroupBy(m => m.Machine)
                                  .Select(x => new MachineHistoryModel {
                                      MachineName = x.Key,
                                      MachineId = x.FirstOrDefault().MachineId ?? 0,
                                      MachineState = getMachineState.Where(t => x.FirstOrDefault().StateId == t.StateId).Select(p => p.Description).LastOrDefault(),

                                      // products
                                      TotalAll =  x.Sum(t => t.TotalNumber),
                                      ProductName = string.Join(",  ", x.Select(t => t.ProductCode).Distinct()),
                                      TotalProductPrice = x.Sum(t => t.Currency == "USD"
                                                 ? (t.ProductionNumber + t.Processing + t.DefectNumber) * (double)t.ProductUnitPrice * exchangeRate2
                                                 : t.Currency == "EUR"
                                                    ? (t.ProductionNumber + t.Processing + t.DefectNumber) * (double)t.ProductUnitPrice * EUR
                                                    : (t.ProductionNumber + t.Processing + t.DefectNumber) * (double)t.ProductUnitPrice
                                                 ),

                                      NoNGPrice = x.Sum(t => t.Currency == "USD"
                                          ? (t.ProductionNumber + t.Processing ) * (double)t.ProductUnitPrice * exchangeRate2
                                          : t.Currency == "EUR"
                                            ? (t.ProductionNumber + t.Processing ) * (double)t.ProductUnitPrice * EUR
                                            : (t.ProductionNumber + t.Processing) * (double)t.ProductUnitPrice
                                          ),



                                      Productivity = x.GroupBy(p => new { p.ProductId, p.MachineId, p.MaterialId, p.Productivity })
                                                      .Select(g =>
                                                        {
                                                            var trackProd = trackupList
                                                                .Where(t => t.MachineId == g.Key.MachineId 
                                                                            && t.ProductId == g.Key.ProductId 
                                                                            && t.MaterialId == g.Key.MaterialId)
                                                                .Select(p => p.Productivity)
                                                                .LastOrDefault();

                                                            var prod = trackProd == 0 
                                                                ? productProductivityList
                                                                    .Where(t => t.ProductId == g.Key.ProductId)
                                                                    .Select(p => p.Productivity)
                                                                    .LastOrDefault()
                                                                : trackProd ;

                                                            return (g.Sum(t => t.TotalNumber) * (double)prod) / 3600;
                                                        })
                                                        .Sum(),


                                      NGPersent = (x.Sum(t => t.NGNumber)) / (x.Sum(t => t.TotalNumber)) * 100, 

                                      ProductDetails = x.GroupBy(p => new { p.ProductId, p.ProductCode, p.ProductUnitPrice, p.Currency, p.MachineId, p.Productivity, p.MaterialId})
                                                        .OrderBy(t => t.Key.ProductCode)
                                                        .Select((g, index) => new ProductDetailModel {
                                                            Index = index + 1,
                                                            ProductId = g.Key.ProductId,
                                                            ProductCode = g.Key.ProductCode,
                                                            ProductUnitPrice = (double)g.Key.ProductUnitPrice,
                                                            TotalProduct = g.Sum(t => t.ProductionNumber),

                                                            Currency = g.Key.Currency,
                                                            ProductPrice = g.Sum(t => t.Currency == "USD"
                                                                ? t.ProductionNumber * (double)t.ProductUnitPrice * exchangeRate2
                                                                : t.Currency == "EUR"
                                                                    ? t.ProductionNumber * (double)t.ProductUnitPrice * EUR
                                                                    : t.ProductionNumber * (double)t.ProductUnitPrice
                                                               ),
                                                            TotalDefect = g.Sum(t => t.DefectNumber),
                                                            DefectPrice = g.Sum(t => t.Currency == "USD"
                                                                ? t.DefectNumber * (double)t.ProductUnitPrice * exchangeRate2
                                                                : t.Currency == "EUR"
                                                                    ? t.DefectNumber * (double)t.ProductUnitPrice * EUR
                                                                    : t.DefectNumber * (double)t.ProductUnitPrice 
                                                                ),

                                                            TotalProcessing = g.Sum(t => t.Processing),
                                                            ProcessingPrice = g.Sum(t => t.Currency == "USD"
                                                                ? t.Processing * (double)t.ProductUnitPrice * exchangeRate2
                                                                : t.Currency == "EUR"
                                                                    ? t.Processing * (double)t.ProductUnitPrice * EUR
                                                                    : t.Processing * (double)t.ProductUnitPrice 
                                                                ),

                                                            TotalAllProduction = g.Sum(t => t.ProductionNumber) + g.Sum(t => t.Processing) + g.Sum(t => t.DefectNumber),

                                                            NGPersent = (g.Sum(t => t.NGNumber)) / (g.Sum(t => t.TotalNumber)) * 100, 

                                                            TotalPrice = g.Sum(t => t.Currency == "USD"
                                                                ? (t.ProductionNumber + t.Processing + t.DefectNumber) * (double)t.ProductUnitPrice * exchangeRate2
                                                                : t.Currency == "EUR"
                                                                    ? (t.ProductionNumber + t.Processing + t.DefectNumber) * (double)t.ProductUnitPrice * EUR
                                                                    : (t.ProductionNumber + t.Processing + t.DefectNumber) * (double)t.ProductUnitPrice 
                                                                ),
                                                            ProductProductivityActual = trackupList.Where(t => t.MachineId == g.Key.MachineId                   
                                                                                                            && t.ProductId == g.Key.ProductId
                                                                                                            && t.MaterialId == g.Key.MaterialId)
                                                                                                   .Select(p => p.Productivity)
                                                                                                   .LastOrDefault(),
                                                                                                   //.DefaultIfEmpty(0)
                                                                                                   //.Average(),

                                                            ProductProductivitySetting = (double)g.Key.Productivity,

                                                            ProductProductionTime = (g.GroupBy(p => new { p.ProductId, p.MachineId, p.MaterialId, p.Productivity })
                                                                                     .Select(d => {
                                                                                         var trackProd = trackupList
                                                                                             .Where(t => t.MachineId == g.Key.MachineId
                                                                                                      && t.ProductId == g.Key.ProductId
                                                                                                      && t.MaterialId == g.Key.MaterialId)
                                                                                             .Select(p => p.Productivity)
                                                                                             .LastOrDefault();

                                                                                         var prod = trackProd == 0
                                                                                             ? productProductivityList
                                                                                                 .Where(t => t.ProductId == g.Key.ProductId)
                                                                                                 .Select(p => p.Productivity)
                                                                                                 .LastOrDefault()
                                                                                             : trackProd;

                                                                                         return (d.Sum(t => t.TotalNumber) * (double)prod) / 3600;
                                                                                     }).Sum())
                                                                

                                                        }).OrderBy(t => t.Index)
                                                         .ToList(),



                                      TotalCost = (x.Sum(t => t.MaterialUnitPrice * t.MaterialUsed * t.MaterialUnitWeight)) 
                                                  + (exp.Where(e => e.MachineId == x.FirstOrDefault().MachineId).Sum(t => t.Quantity * t.UnitPrice)),

                                       
                                      // materials
                                      TotalMaterialUsed = x.Sum(t => t.MaterialUsed),
                                      MaterialName = string.Join(",  ",  x.Select(t => t.MaterialCode).Distinct()),
                                      TotalMaterialCost = x.Sum(t => t.MaterialUnitPrice * t.MaterialUsed * t.MaterialUnitWeight),

                                      MaterialDetails = x.GroupBy(p => new { p.MaterialId, p.MaterialUnitPrice, p.MaterialUnitWeight, p.LotMaterial, p.ProductCode, p.ProductionRate, p.ProductId })
                                                         .OrderBy(t => t.Key.ProductCode)
                                                         .ThenBy(t => t.Key.MaterialId)
                                                         .ThenBy(t => t.Key.LotMaterial)
                                                         .Select((g, index) => new MaterialDetailModel {
                                                             Index = index + 1,
                                                             MaterialId = g.Key.MaterialId,
                                                             MaterialCode = materialDict.ContainsKey(g.Key.MaterialId)
                                                                             ? materialDict[g.Key.MaterialId]
                                                                             : "",
                                                             Productcode = g.Key.ProductCode,
                                                             MaterialUnitPrice = (double)g.Key.MaterialUnitPrice,
                                                             TotalMaterial = g.Sum(t => t.MaterialUsed),
                                                             MaterialUnitWeight = g.Key.MaterialUnitWeight,
                                                             MaterialLot = g.Key.LotMaterial,
                                                             ProductionRate = g.Key.ProductionRate,
                                                             MaterialLenght = materialLenghtList.Where(t => t.MaterialId == g.Key.MaterialId
                                                                                                        && t.LotNumber == g.Key.LotMaterial)
                                                                                                 .Select(t => t.Length).Distinct().LastOrDefault(),
                                                             ProductionRateNumber = (g.Sum(z => z.MaterialUsed * (trackupList.Where(t => t.MachineId == x.FirstOrDefault().MachineId
                                                                                                       && t.ProductId == g.Key.ProductId
                                                                                                       && t.MaterialId == g.Key.MaterialId)
                                                                                                .Select(t => t.RealRate)
                                                                                                .LastOrDefault()-1)
                                                                                                )),
                                                             ProductionRateDiffNumber = g.Sum(z => z.TotalNumber - (z.MaterialUsed * (trackupList.Where(t => t.MachineId == x.FirstOrDefault().MachineId
                                                                                                       && t.ProductId == g.Key.ProductId
                                                                                                       && t.MaterialId == g.Key.MaterialId
                                                                                                       && lotNumberList.Contains(g.Key.LotMaterial))
                                                                                                .Select(t => t.RealRate)
                                                                                                .LastOrDefault()-1)
                                                                                                )),
                                                             MaxDiffNumber = g.Sum(t => t.MaterialUsed) * 5,
                                                             MinDiffNumber = g.Sum(t => t.MaterialUsed) * 3,
                                                             PercentProductionRate = 
                                                             (((g.Sum(t => t.TotalNumber)) - (g.Sum(z => z.MaterialUsed * (trackupList.Where(t => t.MachineId == x.FirstOrDefault().MachineId
                                                                                                       && t.ProductId == g.Key.ProductId
                                                                                                       && t.MaterialId == g.Key.MaterialId)
                                                                                                .Select(t => t.RealRate)
                                                                                                .LastOrDefault()-1)
                                                                                                ))) / 
                                                                                                 (g.Sum(z => z.MaterialUsed * (trackupList.Where(t => t.MachineId == x.FirstOrDefault().MachineId
                                                                                                       && t.ProductId == g.Key.ProductId
                                                                                                       && t.MaterialId == g.Key.MaterialId
                                                                                                       && lotNumberList.Contains(g.Key.LotMaterial))
                                                                                                .Select(t => t.RealRate)
                                                                                                .LastOrDefault()-1)
                                                                                                ))) * 100,

                                                             ProductionRateActual = (trackupList.Where(t => t.MachineId == x.FirstOrDefault().MachineId 
                                                                                                       && t.ProductId == g.Key.ProductId
                                                                                                       && t.MaterialId == g.Key.MaterialId)
                                                                                                .Select(t => t.RealRate)
                                                                                                .LastOrDefault()-1),

                                                                                                
                                                             MaterialPrice = g.Sum(t => t.MaterialUsed * (double)t.MaterialUnitWeight * t.MaterialUnitPrice),
                                                         }).OrderBy(t => t.Index)
                                                         .ToList(),

                                        
                                      // ProductionRate

                                      //ProductionRate = x.Sum(t => t.MaterialUsed * t.ProductionRate),

                                      PercentProductionRate = (((x.Sum(t => t.TotalNumber)) - (x.Sum(t => t.MaterialUsed *
                                                                                                        (trackupList.Where(z => z.MachineId == x.FirstOrDefault().MachineId
                                                                                                       && z.ProductId == t.ProductId
                                                                                                       && z.MaterialId == t.MaterialId)
                                                                                                .Select(z => z.RealRate)
                                                                                                .LastOrDefault()-1)
                                                                                                ))) 
                                                                                                / 
                                                                                                (x.Sum(t => t.MaterialUsed * 
                                                                                                    (trackupList.Where(z => z.MachineId == x.FirstOrDefault().MachineId 
                                                                                                       && z.ProductId == t.ProductId
                                                                                                       && z.MaterialId == t.MaterialId)
                                                                                                .Select(z => z.RealRate)
                                                                                                .LastOrDefault()-1)
                                                                                                ))) 
                                                                                                * 100
                                                                                                ,

                                      ProductionRateDiffNumber = (x.Sum(t => t.TotalNumber)) - (x.Sum(t => t.MaterialUsed * t.ProductionRate)),

                                      ProductionTimeDiffNumber = (x.GroupBy(p => new { p.ProductId, p.MachineId, p.MaterialId, p.Productivity })
                                                      .Select(g => {
                                                            var trackProd = trackupList
                                                                .Where(t => t.MachineId == g.Key.MachineId
                                                                            && t.ProductId == g.Key.ProductId
                                                                            && t.MaterialId == g.Key.MaterialId)
                                                                .Select(p => p.Productivity)
                                                                .LastOrDefault();

                                                            var prod = trackProd == 0
                                                                ? productProductivityList
                                                                    .Where(t => t.ProductId == g.Key.ProductId)
                                                                    .Select(p => p.Productivity)
                                                                    .LastOrDefault()
                                                                : trackProd;

                                                            return (g.Sum(t => t.TotalNumber) * (double)prod) / 3600;
                                                        })
                                                        .Sum()) - 
                                                        ((x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                              .Select(t => t.StateDate)
                                                              .LastOrDefault()) == null
                                                           ? maxRunTime
                                                               - repairList.Where(e => e.MachineId == x.FirstOrDefault().MachineId)
                                                                           .SelectMany(e => e.Repairs)
                                                                           .Sum(r => r.CheckErrorTime)
                                                           : (
                                                               (x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                  .Select(t => t.StateDate)
                                                                  .LastOrDefault() < fDate)
                                                               ? 0
                                                               : (
                                                                   (x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                      .Select(t => t.StateDate)
                                                                      .LastOrDefault() >= fDate &&
                                                                    x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                      .Select(t => t.StateDate)
                                                                      .LastOrDefault() <= tDate)
                                                                   ?
                                                                   (x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                       .Select(t => t.StateDate)
                                                                       .LastOrDefault().Value - fDate).TotalHours
                                                                     - repairList.Where(e => e.MachineId == x.FirstOrDefault().MachineId)
                                                                                 .SelectMany(e => e.Repairs)
                                                                                 .Sum(r => r.CheckErrorTime)
                                                                     -
                                                                     (((int)Math.Round(
                                                                            (x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                               .Select(t => t.StateDate)
                                                                               .LastOrDefault().Value - fDate).TotalDays,
                                                                            MidpointRounding.AwayFromZero)
                                                                         - (MyUtilities.Function.DaysNoSunDay(
                                                                                fDate,
                                                                                x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                                 .Select(t => t.StateDate)
                                                                                 .LastOrDefault().Value) - 1)) * 24)

                                                                   : maxRunTime
                                                                     - repairList.Where(e => e.MachineId == x.FirstOrDefault().MachineId)
                                                                                 .SelectMany(e => e.Repairs)
                                                                                 .Sum(r => r.CheckErrorTime)
                                                                 )
                                                             )),

                                      //MaxDiffNumber = x.Sum(t => t.MaterialUsed ) * 5,
                                      //MinDiffNumber = x.Sum(t => t.MaterialUsed ) * 3,


                                      // tools
                                      CountToolId = exp.Where(e => e.MachineId == x.FirstOrDefault().MachineId).Select(t => t.ToolId).Distinct().Count(),
                                      TotalToolUsed = exp.Where(e => e.MachineId == x.FirstOrDefault().MachineId).Sum(t => t.Quantity),
                                      TotalToolPrice = exp.Where(e => e.MachineId == x.FirstOrDefault().MachineId).Sum(t => t.Quantity * t.UnitPrice),
                                      ToolDetails = exp.Where(e => e.MachineId == x.FirstOrDefault().MachineId)
                                                       .GroupBy(p => new { p.ToolId, p.ToolFullCode, p.UnitPrice, p.ToolName })
                                                       .OrderBy(t => t.Key.ToolName)
                                                       .Select((g, index) => new ToolDetailModel {
                                                           Index = index + 1,
                                                           ToolId = g.Key.ToolId,
                                                           ToolCode = g.Key.ToolFullCode,
                                                           ToolName = g.Key.ToolName,
                                                           ToolUnitPrice = g.Key.UnitPrice,
                                                           TotalToolUsed = g.Sum(t => t.Quantity),
                                                           ToolPrice = g.Sum(t => t.Quantity * t.UnitPrice),
                                                       }).OrderBy(t => t.Index)
                                                       .ToList(),

                                      // repair
                                      //MaxProductionTime = maxRunTime - repairList.Where(e => e.MachineId == x.FirstOrDefault().MachineId).SelectMany(e => e.Repairs).Sum(r => r.CheckErrorTime),
                                      MaxProductionTime2 =  (x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                              .Select(t => t.StateDate)
                                                              .LastOrDefault()) == null
                                                           ? maxRunTime
                                                               - repairList.Where(e => e.MachineId == x.FirstOrDefault().MachineId)
                                                                           .SelectMany(e => e.Repairs)
                                                                           .Sum(r => r.CheckErrorTime)
                                                           : (
                                                               (x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                  .Select(t => t.StateDate)
                                                                  .LastOrDefault() < fDate)
                                                               ? 0
                                                               : (
                                                                   (x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                      .Select(t => t.StateDate)
                                                                      .LastOrDefault() >= fDate &&
                                                                    x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                      .Select(t => t.StateDate)
                                                                      .LastOrDefault() <= tDate)
                                                                   ?
                                                                   (x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                       .Select(t => t.StateDate)
                                                                       .LastOrDefault().Value - fDate).TotalHours
                                                                     - repairList.Where(e => e.MachineId == x.FirstOrDefault().MachineId)
                                                                                 .SelectMany(e => e.Repairs)
                                                                                 .Sum(r => r.CheckErrorTime)
                                                                     -
                                                                     (((int)Math.Round(
                                                                            (x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                               .Select(t => t.StateDate)
                                                                               .LastOrDefault().Value - fDate).TotalDays,
                                                                            MidpointRounding.AwayFromZero)
                                                                         - (MyUtilities.Function.DaysNoSunDay(
                                                                                fDate,
                                                                                x.Where(t => t.StateId == x.FirstOrDefault().StateId)
                                                                                 .Select(t => t.StateDate)
                                                                                 .LastOrDefault().Value) - 1)) * 24)

                                                                   : maxRunTime
                                                                     - repairList.Where(e => e.MachineId == x.FirstOrDefault().MachineId)
                                                                                 .SelectMany(e => e.Repairs)
                                                                                 .Sum(r => r.CheckErrorTime)
                                                                 )
                                                             ),



                                                                          
                                      CountRepairTimes = repairList.Where(e => e.MachineId == x.FirstOrDefault().MachineId)
                                          .SelectMany(e => e.Repairs)
                                          .Select(r => r.DetailId)
                                          .Distinct()
                                          .Count(),
                                      //TotalRepairTime = repairList.Where(e => e.MachineId == x.FirstOrDefault().MachineId).SelectMany(e => e.Repairs).Sum(r => r.FixTime),
                                      RepairDetails = repairList.Where(e => e.MachineId == x.FirstOrDefault().MachineId)
                                                                  .SelectMany(e => e.Repairs)
                                                                  .OrderBy(t => t.CauseDate)
                                                                  .ThenBy(t => t.ProductCode)
                                                                  .Select((r, index) => new RepairDetailModel {
                                                                      Index = index +1,
                                                                      ProductCode = r.ProductCode,
                                                                      CauseDate = r.CauseDate,
                                                                      ErrorCause = string.IsNullOrEmpty(r.ErrorCause) ? " " : r.ErrorCause,
                                                                      HowToFix = string.IsNullOrEmpty(r.HowToFix) ? " " : r.HowToFix,
                                                                      //StartDate = r.StartDate,
                                                                      //FixTime = r.FixTime,
                                                                      FinishDate = r.FinishDate,
                                                                      StatusMachine = r.StateCode + "."+ r.Descripbe,
                                                                      CheckTime = r.CheckErrorTime,

                                                                  }).OrderBy(t => t.Index)
                                                                    .ToList(),

                                        //MachineStateTime = x.Select(t => t.StateDate).LastOrDefault(),
                                        MachineStopTime = repairList.Where(e => e.MachineId == x.FirstOrDefault().MachineId).SelectMany(p => p.Repairs).Sum(t => t.CheckErrorTime),
                                                            Efficiency = (
                                                                (x.GroupBy(p => new { p.ProductId, p.MachineId, p.MaterialId, p.Productivity })
                                                                     .Select(g =>
                                                                     {
                                                                         var trackProd = trackupList
                                                                             .Where(t => t.MachineId == g.Key.MachineId
                                                                                      && t.ProductId == g.Key.ProductId
                                                                                      && t.MaterialId == g.Key.MaterialId)
                                                                             .Select(p => p.Productivity)
                                                                             .LastOrDefault();

                                                                         var prod = trackProd == 0
                                                                             ? productProductivityList
                                                                                 .Where(t => t.ProductId == g.Key.ProductId)
                                                                                 .Select(p => p.Productivity)
                                                                                 .LastOrDefault()
                                                                             : trackProd;

                                                                         return (g.Sum(t => t.TotalNumber) * (double)prod) / 3600;
                                                                     }).Sum())
                                                                / (maxRunTime ))
                                                                * 100,

                                  }).ToList();
                }
            }
            catch (Exception) {
                throw;
            }
            return model;
        }



        public List<MachineHistoryModel> GetTrackUpMachine2(int machineId, string fromDate, string toDate) {
            var model = new List<MachineHistoryModel>();
            try {
                using (var vfi = new tammaContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var fDate = MyUtilities.Function.ParseDate(fromDate);
                    var tDate = MyUtilities.Function.ParseLastDateTime(toDate);
                    var results = (from d in vfi.ImportFormSX1Detail
                                   join h in vfi.ImportFormSX1
                                         on d.ImportId equals h.ImportId
                                   where ((machineId == 0 || d.MachineId == machineId) &&
                                            h.ImportDate >= fDate &&
                                            h.ImportDate <= tDate)
                                   group new { d, h } by new { d.MachineId, d.ProductId, d.MaterialInventory.MaterialId } into sx_group
                                   select new {
                                       //sx_group.Key
                                       Machine = sx_group.First().d.Machine,
                                       MachineId = sx_group.Key.MachineId.Value,
                                       ProductId = sx_group.Key.ProductId,

                                       //d.ProductId,
                                       //ProductCode = d.Product.ProductCode,

                                       ProductionNumber = sx_group.Sum(x => x.d.Number1 + x.d.Number2),
                                       //ProductionNumber = d.Number1 + d.Number2,
                                       ProductUnitPrice = sx_group.First().d.Product.UnitPrice ?? 0,
                                       //ProductUnitPrice = d.Product.UnitPrice,
                                       //Currency = d.Product.Currency,

                                       //d.MaterialInvId,
                                       //MaterialUsed = d.MaterialUse1 + d.MaterialUse2,
                                       //MaterialId = d.MaterialInventory.MaterialId,
                                       //MaterialUnitPrice = d.MaterialInventory.UnitPrice,
                                       //MaterialUnitWeight = d.MaterialInventory.UnitWeight,
                                       //LotMaterial = d.LotNumber,
                                   })
                        //.OrderBy(m => m.Machine)
                        //.GroupBy(p => new { p.MachineId })
                                     .ToList();
                    var exchangeRate = 26000;
                    //Tools
                    var machine_ids = results.Select(x => x.MachineId).Distinct().ToList();
                    var exp = (from ed in vfi.ExportToolDetails
                               where ed.ExportTool.TransactionFpt.Status == 2
                                  && ed.ExportTool.TransactionFpt.TransactionDate >= fDate
                                  && ed.ExportTool.TransactionFpt.TransactionDate <= tDate
                                  && ed.MachineId != null
                                  && machine_ids.Contains(ed.MachineId.Value)
                               select new {
                                   ed.MachineId,
                                   ed.ToolInventory.ToolId,
                                   ed.ToolInventory.UnitPrice,
                                   ed.Quantity,
                                   ed.ToolInventory.Tool.ToolName,
                               }).ToList();
                    var toolIdList = exp.Select(t => t.ToolId).Distinct().ToList();


                    // tu lam dua vao recomment
                    foreach (var result in results) {
                        var entity = model.FirstOrDefault(x => x.MachineId == result.MachineId);
                        if (entity == null) {
                            entity = new MachineHistoryModel {
                                CountMaterialId = 0,
                                CountProductId = 0,
                                CountToolId = 0,

                                TotalMaterialUsed = 0,
                                TotalProduction = 0,
                                TotalToolUsed = 0,

                                ProductProduction = 0,

                                MaterialUnitPrice = 0,
                                ProductUnitPrice = 0,
                                ToolUnitPrice = 0,

                                ProductCurrency = "",

                                MaterialUnitWeight = 0,

                                TotalProductPrice = 0,
                                TotalToolPrice = 0,
                                TotalMaterialCost = 0,
                                MachineName = result.Machine,


                            };
                            model.Add(entity);
                        }
                        //entity.MachineName = result.FirstOrDefault().Machine;
                        //entity.ProductProduction = result.FirstOrDefault().ProductionNumber;
                        //entity.TotalProduction += entity.ProductProduction;
                        //entity.ProductCurrency = result.FirstOrDefault().Currency;
                        //entity.ProductUnitPrice = (double)result.FirstOrDefault().ProductUnitPrice;
                        var productprice = 0.00;
                        if (entity.ProductCurrency == "USD") {
                            productprice = entity.ProductProduction * entity.ProductUnitPrice * exchangeRate;
                        }
                        else {
                            productprice = entity.ProductProduction * entity.ProductUnitPrice;
                        }
                        entity.TotalProductPrice += productprice;

                        entity.CountProductId += 1;

                    }






                    // recommend
                    //foreach (var result in results) {
                    //    var entity = model.FirstOrDefault(x => x.MachineId == result.Key.MachineId);
                    //    if (entity == null) {
                    //        entity = new MachineHistoryModel {
                    //            TotalMaterialUsed = 0,
                    //        };
                    //        model.Add(entity);
                    //    }

                    //    var productDetail = entity.ProductDetails.FirstOrDefault(x => x.ProductId == result.Key.MachineId);

                    //    if (productDetail == null) {
                    //        productDetail = new ProductDetailModel {
                    //        };
                    //        entity.ProductDetails.Add(productDetail);
                    //    }

                    //    var toolDetail = entity.ToolDetails.FirstOrDefault(x => x.ProductId == result.Key.ProductId);

                    //    if (toolDetail == null) {
                    //        toolDetail = new ToolDetailModel {
                    //        };
                    //        entity.ToolDetails.Add(toolDetail);
                    //    }
                    //    entity.TotalMaterialUsed = result.Sum(x => x.MaterialUsed);

                    //}
                }
            }
            catch (Exception) {
                throw;
            }
            return model;
        }




        [GridAction]
        public ActionResult SelectMachine() {
            var model = new List<MachineModel>();
            try {
                model = MachineList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMachine", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        public List<MachineModel> MachineList() {
            var model = new List<MachineModel>();
            try {

                using (var vfi = new vfiContext()) {
                    //var user = vfi.Users.FirstOrDefault(u => u.Username.Contains(HttpContext.User.Identity.Name));
                    //if (user == null)
                    //    throw new AggregateException(
                    //        "Lỗi ! Không thấy tên đăng nhập ! Vui lòng đăng nhập lại hoặc liên hệ Admin");
                    var production2 = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.Production2Management);
                    var production1 = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.ProductionManagement);
                    model = (from x in vfi.Machines
                                    select new MachineModel {
                                        MachineId = x.MachineId,
                                        MachineCode = x.MachineName,
                                        MachineIdName = x.MachineIdName,
                                        Active = x.Active,
                                        DiagramType = x.DiagramType ?? 1,
                                        ColumnIndex = x.ColumnIndex ?? 1,
                                        RowIndex = x.RowIndex ?? 1,
                                        ProcessingTypeId = x.ProcessingTypeId.Value,
                                        ProcessingTypeName = x.ProcessingType.TypeName,
                                        Production2 = x.Production2,
                                        MachineFunction = x.MachineFunction,
                                        //DiagramTypeName = MyUtilities.Machine.Diagram.GetText(x.DiagramType ?? 1),
                                        ModifiedDate = x.ModifiedDate,
                                        ModifiedUser = x.ModifiedUser,
                                    }).ToList();
                    model.ForEach(x => x.DiagramTypeName = MyUtilities.Machine.GetDiagramText(x.DiagramType));
                    if (production1 && production2) { }
                    else if (production1) {
                        model = model.Where(m => m.Active).ToList();
                    }
                    else if (production2) {
                        model = model.Where(m => m.Production2).ToList();
                    }
                    //foreach (var machine in machines) {
                    //    var entity = new MachineModel {
                    //        MachineId = machine.MachineId,
                    //        MachineCode = machine.MachineName,
                    //        Active = machine.Active,
                    //        DiagramType = machine.DiagramType ?? 1,
                    //        ColumnIndex = machine.ColumnIndex ?? 1,
                    //        RowIndex = machine.RowIndex ?? 1,
                    //        ProcessingTypeId = machine.ProcessingTypeId.Value,
                    //        ProcessingTypeName = machine.ProcessingType.TypeName,
                    //        Production2 = machine.Production2,
                    //        MachineFunction = machine.MachineFunction
                    //    };

                    //    entity.DiagramTypeName = MyUtilities.Machine.Diagram.GetText(entity.DiagramType);
                    //    model.Add(entity);
                    //}
                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex.Message);
            }
            return model.OrderByDescending(m => m.Active).ThenByDescending(m => m.Production2).ThenBy(m => m.MachineCode).ToList();
        }

        [GridAction]
        public ActionResult InsertMachine(MachineModel newMachine) {
            //if (newMachine.ColumnIndex == 0 || newMachine.RowIndex == 0)
            //    throw new AggregateException("Sai dòng cột !Vui lòng nhập số dòng - cột !");
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(newMachine.MachineCode)) {
                    throw new AggregateException("Lỗi! Tên máy không được để trống!");
                }
                if (newMachine.Active == newMachine.Production2) {
                    if (!newMachine.Active) newMachine.Active = true;
                    else throw new AggregateException("Lỗi! 1 máy không thể ở nhiều vị trí !");
                }
                using (var vfi = new vfiContext()) {
                    //var entity = vfi.Machines.FirstOrDefault(m => m.MachineName.Equals(newMachine.MachineName));
                    var entity = new Machine();
                    if (newMachine.Active)
                        entity =
                            vfi.Machines.FirstOrDefault(m => m.MachineName.Equals(newMachine.MachineCode) && m.Active);
                    else if (newMachine.Production2)
                        entity =
                            vfi.Machines.FirstOrDefault(m => m.MachineName.Equals(newMachine.MachineCode) && m.Production2);
                    if (entity == null) {
                        var diagram = 1;
                        try {
                            diagram = Convert.ToInt32(newMachine.DiagramTypeName);
                        }
                        catch (FormatException) {
                            diagram = newMachine.DiagramType;
                        }
                        if (diagram == 2) {
                            if (newMachine.ColumnIndex > 4)
                                throw new AggregateException("Sai cột ! Sơ đồ máy CNC chỉ có 4 cột");
                        }
                        else if (newMachine.ColumnIndex > 4) {
                            throw new AggregateException("Sai cột ! Sơ đồ máy Cames chỉ có 4 cột");
                        }
                        var processingType = 1;
                        try {
                            processingType = Convert.ToInt32(newMachine.ProcessingTypeName);
                        }
                        catch (FormatException) {
                            processingType =
                                vfi.ProcessingTypes.FirstOrDefault(
                                    pt => pt.TypeName.Equals(newMachine.ProcessingTypeName)).TypeId;
                        }
                        entity = new Machine {
                            MachineName = newMachine.MachineCode,
                            MachineIdName = newMachine.MachineIdName,
                            Active = newMachine.Active,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            StateId = MyUtilities.Machine.State.Normal,
                            Number = 0,
                            DiagramType = diagram,
                            ColumnIndex = newMachine.ColumnIndex,
                            RowIndex = newMachine.RowIndex,
                            ProcessingTypeId = processingType,
                            Production2 = newMachine.Production2,
                            MachineFunction = newMachine.MachineFunction,
                            StartProductionDate = DateTime.Now
                        };
                        if (!entity.Production2) {
                            var indexExist =
                                vfi.Machines.FirstOrDefault(
                                    m => m.ColumnIndex == newMachine.ColumnIndex &&
                                         m.RowIndex == newMachine.RowIndex);
                            if (indexExist != null) {
                                var machinesInColumn =
                                    vfi.Machines.Where(
                                        m =>
                                        m.ColumnIndex == newMachine.ColumnIndex && m.RowIndex >= newMachine.RowIndex)
                                       .OrderBy(m => m.RowIndex);
                                var rowIndex = entity.RowIndex;
                                foreach (var machine in machinesInColumn) {
                                    rowIndex++;
                                    machine.RowIndex = rowIndex;
                                }
                            }
                        }
                        var production2 = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                            MyUtilities.UserRole.Production2Management);
                        var production = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                            MyUtilities.UserRole.ProductionManagement);
                        if (production && production) {
                            //throw new AggregateException(
                            //    "Lỗi ! Không có quyền xem vị trí này ! Vui lòng đăng nhập lại hoặc liên hệ Admin");
                        }
                        else if (production2) {
                            entity.Production2 = true;
                            entity.Active = false;
                        }
                        else if (production) {
                            entity.Production2 = false;
                            entity.Active = true;
                        }
                        vfi.Machines.Add(entity);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Machine Name is Exist !!");
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("Machine", ex.Message);
            }
            return View(new GridModel(MachineList()));
        }

        [GridAction]
        public ActionResult UpdateMachine(MachineModel updateMachine) {
            //if (updateMachine.ColumnIndex == 0 || updateMachine.RowIndex == 0)
            //    throw new AggregateException("Sai dòng cột !Vui lòng nhập số dòng - cột !");
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var entity = vfi.Machines.FirstOrDefault(m => m.MachineId == updateMachine.MachineId);
                    if (entity == null)
                        throw new AggregateException("Lỗi! Không tìm thấy máy ! Liên hệ admin");
                    if (updateMachine.Active == updateMachine.Production2) {
                        if (updateMachine.Active)
                            throw new AggregateException("Lỗi! 1 máy không thể ở nhiều vị trí !");
                    }
                    if (entity.MachineName !=null &&
                        entity.MachineName.Equals(updateMachine.MachineCode) &&
                        entity.Active == updateMachine.Active &&
                        entity.Production2 == updateMachine.Production2 &&
                        entity.MachineId != updateMachine.MachineId &&
                        entity.MachineIdName != updateMachine.MachineIdName)                                // moi them
                        throw new AggregateException("Lỗi! Tên máy đã được sử dụng");
                    var diagram = 1;
                    try {
                        diagram = Convert.ToInt32(updateMachine.DiagramTypeName);
                    }
                    catch (FormatException) {
                        diagram = updateMachine.DiagramType;
                    }
                    if (diagram == 2) {
                        if (updateMachine.ColumnIndex > 4)
                            throw new AggregateException("Sai cột ! Sơ đồ máy CNC chỉ có 4 cột");
                    }
                    else if (updateMachine.ColumnIndex > 4)
                        throw new AggregateException("Sai cột ! Sơ đồ máy Cames chỉ có 4 cột");
                    var processingType = 1;
                    try {
                        processingType = Convert.ToInt32(updateMachine.ProcessingTypeName);
                    }
                    catch (FormatException) {
                        processingType =
                            vfi.ProcessingTypes.FirstOrDefault(
                                pt => pt.TypeName.Equals(updateMachine.ProcessingTypeName)).TypeId;
                    }
                    entity.MachineName = updateMachine.MachineCode;
                    entity.MachineIdName = updateMachine.MachineIdName;
                    entity.Active = updateMachine.Active;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    if (diagram != 0)
                        entity.DiagramType = diagram;
                    entity.ColumnIndex = updateMachine.ColumnIndex;
                    entity.RowIndex = updateMachine.RowIndex;
                    entity.ProcessingTypeId = processingType;
                    entity.Production2 = updateMachine.Production2;
                    entity.MachineFunction = updateMachine.MachineFunction;
                    if (!entity.Production2) {
                        var indexExist =
                         vfi.Machines.FirstOrDefault(
                             m => m.ColumnIndex == updateMachine.ColumnIndex &&
                                 m.RowIndex == updateMachine.RowIndex &&
                                  m.MachineId != updateMachine.MachineId);
                        if (indexExist != null) {
                            var machinesInColumn =
                                vfi.Machines.Where(
                                    m =>
                                    m.ColumnIndex == updateMachine.ColumnIndex &&
                                    m.RowIndex >= updateMachine.RowIndex &&
                                    m.MachineId != updateMachine.MachineId).OrderBy(m => m.RowIndex);
                            var rowIndex = entity.RowIndex;
                            foreach (var machine in machinesInColumn) {
                                rowIndex++;
                                machine.RowIndex = rowIndex;
                            }
                        }
                    }
                    vfi.SaveChanges();

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("Machine", ex.Message);
            }
            return View(new GridModel(MachineList()));
        }

        [HttpPost]
        public ActionResult SwapMachineIndex(int machineIdFrom, int machineIdTo) {
            try {
                using (var vfi = new vfiContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var machineFrom = vfi.Machines.FirstOrDefault(m => m.MachineId == machineIdFrom);
                    var machineTo = vfi.Machines.FirstOrDefault(m => m.MachineId == machineIdTo);
                    if (machineFrom == null)
                        return Json(9);
                    if (machineTo == null)
                        return Json(8);
                    var columnTamp = machineFrom.ColumnIndex;
                    var rowTamp = machineFrom.RowIndex;
                    machineFrom.ColumnIndex = machineTo.ColumnIndex;
                    machineFrom.RowIndex = machineTo.RowIndex;
                    machineTo.ColumnIndex = columnTamp;
                    machineTo.RowIndex = rowTamp;
                    var i =
                    vfi.SaveChanges();
                    if (i != 0)
                        return Json(GetMachineIndexModel.GetMachineIndex(machineFrom));
                }
            }
            catch (Exception) {
                return Json(0);
            }
            return Json(0);
        }

        #region combobox

        public ActionResult SelectComboBoxMachineDiagramType() {
            using (var vfi = new vfiContext()) {
                var model = MyUtilities.Machine.DiagramTemplate;
                return new JsonResult {
                    Data = new SelectList(model, "DiagramType", "DiagramName")
                };
            }
        }

        public ActionResult SelectComboBoxMachineIndex(int machineId) {
            using (var vfi = new vfiContext()) {
                var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == machineId);
                var machinesInColumn =
                    vfi.Machines.Where(
                        m =>
                        m.DiagramType == machine.DiagramType &&
                        m.MachineId != machineId);
                var model = new List<MachineIndexModel>();
                foreach (var machineInColumn in machinesInColumn) {
                    var entity = new MachineIndexModel {
                        MachineId = machineInColumn.MachineId,
                        MachineDiagram = machineInColumn.DiagramType ?? 0,
                        ColumnIndex = machineInColumn.ColumnIndex ?? 0,
                        RowIndex = machineInColumn.RowIndex ?? 0,
                        MachineName = machineInColumn.MachineName,
                    };
                    model.Add(entity);
                }
                return new JsonResult {
                    Data = new SelectList(model.OrderBy(m => m.MachineName), "MachineId", "MachineIndex")
                };
            }
        }

        public List<MachineModel> GetActiveMachines(MachineConfiguration config) {
            var model = new List<MachineModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.Machines
                         where (x.Active || x.Production2)
                         && (config.IsMainProcess == null || x.ProcessingType.Warehouse.IsMainProcess == config.IsMainProcess)
                         && (config.IsProduction == null || x.ProcessingType.Warehouse.IsProduction == config.IsProduction)
                         && (config.IsProduction2 == null || x.ProcessingType.Warehouse.IsProduction2 == config.IsProduction2)
                         && (config.IsProduction2Process == null || x.ProcessingType.Warehouse.IsProduction2Process == config.IsProduction2Process)
                         && (config.IsHeatTreatment == null || x.ProcessingType.Warehouse.IsHeatTreatment == config.IsHeatTreatment)
                         && (config.IsPolish == null || x.ProcessingType.Warehouse.IsPolish == config.IsPolish)
                         && (config.IsReprocessing == null || x.ProcessingType.Warehouse.IsReprocessing == config.IsReprocessing)
                         && (config.IsQC == null || x.ProcessingType.Warehouse.IsQC == config.IsQC)
                         && (config.IsCncMilling == null || x.ProcessingType.Warehouse.IsCncMilling == config.IsCncMilling)
                         && (!config.TypeIds.Any() || (x.ProcessingTypeId != null && config.TypeIds.Contains(x.ProcessingTypeId.Value)))
                         orderby x.MachineName
                         select new MachineModel {
                             MachineId = x.MachineId,
                             MachineName = x.MachineName,
                             ProcessingTypeName = x.ProcessingType.TypeName
                         }).ToList();
            }
            if (config.AddFirstAll == true) {
                model.Insert(0, new MachineModel {
                    MachineId = 0,
                    MachineName = "Tất cả"
                });
            }
            return model;
        }

        public ActionResult SelectComboBoxMachine() {
            return new JsonResult {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { }), "MachineId", "MachineName")
            };
        }
        public ActionResult SelectComboBoxMachineAllProduction() {
            var model = new List<MachineModel>();
            using (var vfi = new tammaContext()) {
                model = (from x in vfi.Machines
                         where x.Active 
                             && x.ProcessingTypeId != null 
                             && (x.ProcessingType.Warehouse.IsProduction || x.ProcessingType.Warehouse.IsCncMilling || x.ProcessingType.Warehouse.IsProduction2)
                         orderby x.MachineName
                         select new MachineModel {
                             MachineId = x.MachineId,
                             MachineName = x.MachineName,
                             ProcessingTypeName = x.ProcessingType.TypeName
                         }).ToList();
            }
            return new JsonResult { Data = new SelectList(model, "MachineId", "MachineName") };
        }
        public ActionResult SelectComboBoxMachineProduction() {
            return new JsonResult {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsProduction = true }), "MachineId", "MachineName")
            };
        }
        public ActionResult SelectComboBoxMachineProductionMoreInfo() {
            return new JsonResult {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsProduction = true }), "MachineId", "MachineFullName")
            };
        }
        public ActionResult SelectComboBoxMachineCNC()
        {
            return new JsonResult
            {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsCncMilling = true }), "MachineId", "MachineName")
            };
        }
        public ActionResult SelectComboBoxMachineProduction2() {
            return new JsonResult {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsProduction2 = true }), "MachineId", "MachineName")
            };
        }
        public ActionResult SelectComboBoxMachineProduction2Processing() {
            return new JsonResult {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsProduction2Process = true }), "MachineId", "MachineName")
            };
        }
        public ActionResult SelectComboBoxMachineHeatTreatment() {
            return new JsonResult {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsHeatTreatment = true }), "MachineId", "MachineName")
            };
        }
        public ActionResult SelectComboBoxMachinePolish() {
            return new JsonResult {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsPolish = true }), "MachineId", "MachineName")
            };
        }
        public ActionResult SelectComboBoxMachineReprocessing() {
            return new JsonResult {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsReprocessing = true }), "MachineId", "MachineName")
            };
        }
        public ActionResult SelectComboBoxMachineQc() {
            return new JsonResult {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsQC = true }), "MachineId", "MachineFullName")
            };
        }

        public ActionResult SelectComboBoxMachineQcByType2(string typeIds) {
            var ids = MyUtilities.Function.StringToIds(typeIds);
            return new JsonResult {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsQC = true, TypeIds = ids }).OrderBy(x => x.ProcessingTypeName).ThenBy(x=> x.MachineName).ToList(), 
                    "MachineId", 
                    "MachineFullName")
            };
        }

        public ActionResult SelectComboBoxMachineQcByType(List<int> typeIds) {
            return new JsonResult {
                Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsQC = true, TypeIds = typeIds.ToList() }), "MachineId", "MachineFullName")
            };
        }

        //public ActionResult SelectComboBoxMachine2() {
        //    return new JsonResult {
        //        Data = new SelectList(GetActiveMachines(new MachineConfiguration { IsProduction2 = true }), "MachineId", "MachineName")
        //    };
        //}

        public ActionResult SelectComboBoxAllMachine() {
            using (var vfi = new vfiContext()) {
                return new JsonResult {
                    Data = new SelectList(GetActiveMachines(new MachineConfiguration { }), "MachineId", "MachineName")
                };
            }
        }

        public ActionResult SelectComboBoxMachineState() {
            using (var vfi = new vfiContext()) {
                var model = from m in vfi.MachineStates
                            where m.Active && m.StateId != 1
                            select new {
                                m.StateId,
                                Code = m.StateCode + "." + m.Description
                            };
                return new JsonResult {
                    Data = new SelectList(model.OrderBy(m => m.Code).ToList(), "StateId", "Code")
                };
            }
        }


        public ActionResult SelectComboBoxMachineFix() {
            using (var vfi = new vfiContext()) {
                var model = from m in vfi.MachineStateDetails
                            where m.Active
                            select new {
                                m.DetailId,
                                Code = m.StateCode + "." + m.Description
                            };
                return new JsonResult {
                    Data = new SelectList(model.OrderBy(m => m.Code).ToList(), "DetailId", "Code")
                };
            }
        }

        public ActionResult SelectComboBoxMachineError() {
            using (var vfi = new vfiContext()) {
                var model = from m in vfi.ErrorCauseForms
                            where m.Active
                            select new {
                                m.ErrorCauseId,
                                Code = m.StateCode + "." + m.Name
                            };
                model = model.Distinct();
                return new JsonResult {
                    Data = new SelectList(model.OrderBy(m => m.Code).ToList(), "ErrorCauseId", "Code")
                };
            }
        }

        public ActionResult SelectComboBoxMachineErrorById(int id) {
            using (var vfi = new vfiContext()) {
                var model = from m in vfi.ErrorCauseForms
                            where m.Active && m.StateId == id
                            select new {
                                m.ErrorCauseId,
                                Code = m.StateCode + "." + m.Name
                            };
                model = model.Distinct();
                return new JsonResult {
                    Data = new SelectList(model.OrderBy(m => m.Code).ToList(), "ErrorCauseId", "Code")
                };
            }
        }
        //public ActionResult SelectComboBoxSection() {
        //    using (var vfi = new vfiContext()) {
        //        //var model = vfi.Machines.Where(m => m.Active).ToList();
        //        var model = from m in vfi.Sections
        //                    where m.Active
        //                    select new {
        //                        m.SectionId,
        //                        m.SectionName,
        //                    };
        //        return new JsonResult {
        //            Data = new SelectList(model.ToList(), "SectionId", "SectionName")
        //        };
        //    }
        //}
        #endregion

        #region machine state

        [GridAction]
        public ActionResult SelectMachineState() {
            var model = GetMachineStateList();
            return View(new GridModel(model));
        }

        List<MachineStateModel> GetMachineStateList() {
            var model = new List<MachineStateModel>();
            try {
                using (var vfi = new vfiContext()) {
                    model = vfi.MachineStates.OrderBy(ms => !ms.Active).ThenBy(ms => ms.StateCode)
                        .Select(machineState => new MachineStateModel {
                            StateId = machineState.StateId,
                            StateCode = machineState.StateCode,
                            Active = machineState.Active,
                            Description = machineState.Description,
                            WarrningColor = machineState.WarrningColor,
                            Timing = machineState.Timing,
                            ModifiedDate = machineState.ModifiedDate,
                            ModifiedUser = machineState.ModifiedUser,
                            WarrningPoint = machineState.WarrningPoint,
                            Index = 0,
                            EstimateTime = machineState.EstimateTime,
                            IsSetProduct = machineState.IsSetProduct,
                            NameEN = machineState.NameEN,
                        }).ToList();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetMachineStateList", ex.Message);
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertMachineState(MachineStateModel newModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(newModel.Description))
                    throw new AggregateException("Mô tả lỗi không được để trống");
                if (string.IsNullOrWhiteSpace(newModel.StateCode))
                    throw new AggregateException("Mã lỗi không được để trống");
                //if (string.IsNullOrWhiteSpace(colorPick))
                //    throw new AggregateException("Màu báo hiệu lỗi");
                using (var vfi = new vfiContext()) {
                    var entity = vfi.MachineStates
                        .FirstOrDefault(ms => ms.Description.Trim().ToUpper().Equals(newModel.Description.ToUpper()) ||
                        ms.StateCode.Trim().ToUpper().Equals(newModel.StateCode));
                    if (entity == null) {
                        entity = new MachineState {
                            Description = newModel.Description.Trim(),
                            Active = true,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            WarrningColor = newModel.WarrningColor,
                            Timing = 0,
                            StateCode = newModel.StateCode.ToUpper().Trim(),
                            EstimateTime = newModel.EstimateTime,
                            IsSetProduct = newModel.IsSetProduct,
                            NameEN = newModel.NameEN,
                        };
                        vfi.MachineStates.Add(entity);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Mô tả lỗi hoặc mã lỗi bị trùng! Vui lòng tạo mô tả khác");
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMachineState", ex.Message);
            }
            return View(new GridModel(GetMachineStateList()));
        }
        [GridAction]
        public ActionResult UpdateMachineState(MachineStateModel updateModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(updateModel.Description))
                    throw new AggregateException("Mô tả lỗi không được để trống");
                if (string.IsNullOrWhiteSpace(updateModel.StateCode))
                    throw new AggregateException("Mã lỗi không được để trống");
                //if (string.IsNullOrWhiteSpace(colorPick))
                //    throw new AggregateException("Màu báo hiệu lỗi");
                updateModel.NameEN = (updateModel.NameEN + "").Trim();
                updateModel.StateCode = updateModel.StateCode.Trim().ToUpper();
                updateModel.Description = (updateModel.Description + "").Trim();
                using (var vfi = new vfiContext()) {
                    var entity = vfi.MachineStates.FirstOrDefault(ms => (ms.Description.ToUpper().Equals(updateModel.Description.ToUpper())
                                                                            || ms.StateCode.Equals(updateModel.StateCode))
                                                                        && ms.StateId != updateModel.StateId);

                    if (entity != null) {
                        throw new AggregateException("Lỗi! Mô tả lỗi hoặc mã lỗi bị trùng! Vui lòng tạo mô tả khác");
                    }
                    entity = vfi.MachineStates.FirstOrDefault(ms => ms.StateId == updateModel.StateId);
                    if (entity == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy lỗi này");
                    }

                    var staticStates = MyUtilities.Machine.State.GetStaticStateList();
                    if (!staticStates.Contains(updateModel.StateId)) {
                        entity.Description = updateModel.Description;
                        entity.Active = updateModel.Active;
                    }

                    entity.StateCode = updateModel.StateCode;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    entity.WarrningColor = updateModel.WarrningColor;
                    entity.Timing = 0;
                    entity.EstimateTime = updateModel.EstimateTime;
                    entity.IsSetProduct = updateModel.IsSetProduct;
                    entity.NameEN = updateModel.NameEN;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMachineState", ex.Message);
            }
            return View(new GridModel(GetMachineStateList()));
        }
        //

        [GridAction]
        public ActionResult SelectMachineStateDetail(int stateId) {
            var model = GetMachineStateListDetail(stateId);
            return View(new GridModel(model));
        }
        List<MachineStateDetailModel> GetMachineStateListDetail(int stateId) {
            var model = new List<MachineStateDetailModel>();
            try {
                using (var vfi = new vfiContext()) {
                    model = vfi.MachineStateDetails.Where(ms => ms.StateId == stateId).OrderBy(ms => !ms.Active).ThenBy(ms => ms.StateCode)
                        .Select(machineState => new MachineStateDetailModel {
                            StateId = machineState.StateId,
                            StateCode = machineState.StateCode,
                            Active = machineState.Active,
                            Description = machineState.Description,
                            Timing = machineState.Timing,
                            DetailId = machineState.DetailId,
                            ModifiedDate = machineState.ModifiedDate,
                            ModifiedUser = machineState.ModifiedUser,
                            Index = 0,
                        }).ToList();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetMachineStateList", ex.Message);
            }
            return model;
        }


        [GridAction]
        public ActionResult InsertMachineStateDetail(MachineStateDetailModel newModel, int stateId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(newModel.Description))
                    throw new AggregateException("Khắc phục không được để trống");
                if (string.IsNullOrWhiteSpace(newModel.StateCode))
                    throw new AggregateException("Mã khắc phục không được để trống");
                using (var vfi = new vfiContext()) {
                    if (stateId == 1)
                        throw new AggregateException("Lỗi! Không có biện pháp cho trạng thái bình thường");
                    var model =
                        vfi.MachineStateDetails.FirstOrDefault(
                            ms =>
                            (ms.Description.Trim().ToUpper().Equals(newModel.Description.ToUpper()) ||
                            ms.StateCode.Trim().ToUpper().Equals(newModel.StateCode)) &&
                            ms.StateId == stateId);
                    if (model == null) {
                        var machineState = new MachineStateDetail {
                            Description = newModel.Description.Trim(),
                            StateCode = newModel.StateCode.ToUpper().Trim(),
                            Active = true,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Timing = newModel.Timing,
                            StateId = stateId,
                        };
                        vfi.MachineStateDetails.Add(machineState);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Cách khăc phục hoặc mã bị trùng! Vui lòng tạo mô tả khác");
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMachineState", ex.Message);
            }
            return View(new GridModel(GetMachineStateListDetail(stateId)));
        }
        [GridAction]
        public ActionResult UpdateMachineStateDetail(MachineStateDetailModel updateModel, int stateId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(updateModel.Description))
                    throw new AggregateException("Khắc phục không được để trống");
                if (string.IsNullOrWhiteSpace(updateModel.StateCode))
                    throw new AggregateException("Mã khắc phục không được để trống");
                using (var vfi = new vfiContext()) {
                    var existModel = vfi.MachineStateDetails.FirstOrDefault(ms => ms.DetailId == updateModel.DetailId);
                    if (existModel != null) {
                        var model =
                             vfi.MachineStateDetails.FirstOrDefault(
                                 ms =>
                                 (ms.Description.Trim().ToUpper().Equals(updateModel.Description.ToUpper()) ||
                                 ms.StateCode.Trim().ToUpper().Equals(updateModel.StateCode)) &&
                                 ms.StateId == updateModel.StateId && ms.DetailId != updateModel.DetailId);
                        if (model != null) {
                            throw new AggregateException("Cách khăc phục hoặc mã bị trùng! Vui lòng tạo mô tả khác");
                        }
                        else {
                            existModel.Description = updateModel.Description.Trim();
                            existModel.StateCode = updateModel.StateCode.ToUpper().Trim();
                            existModel.Active = updateModel.Active;
                            existModel.ModifiedDate = DateTime.Now;
                            existModel.ModifiedUser = HttpContext.User.Identity.Name;
                            existModel.Timing = updateModel.Timing;
                            vfi.SaveChanges();

                        }
                    }
                    else {
                        throw new AggregateException("Bị lỗi !Không tìm thấy lỗi này");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMachineStateDetail", ex.Message);
            }
            return View(new GridModel(GetMachineStateListDetail(stateId)));
        }
        //

        [GridAction]
        public ActionResult SelectMachineFixManagement() {
            var model = GetMachineFixManagement();
            return View(new GridModel(model));
        }

        List<MachineStateDetailModel> GetMachineFixManagement() {
            var model = new List<MachineStateDetailModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var machineStates = vfi.MachineStateDetails.OrderBy(ms => !ms.Active).ThenBy(ms => ms.StateCode);
                    foreach (var machineState in machineStates) {
                        var entity = new MachineStateDetailModel {
                            StateId = machineState.StateId,
                            StateCode = machineState.StateCode,
                            Active = machineState.Active,
                            Description = machineState.Description,
                            Timing = machineState.Timing,
                            DetailId = machineState.DetailId,
                            ModifiedDate = machineState.ModifiedDate,
                            ModifiedUser = machineState.ModifiedUser,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetMachineFixManagement", ex.Message);
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertMachineFix(MachineStateDetailModel newModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(newModel.Description))
                    throw new AggregateException("Khắc phục không được để trống");
                if (string.IsNullOrWhiteSpace(newModel.StateCode))
                    throw new AggregateException("Mã khắc phục không được để trống");
                using (var vfi = new vfiContext()) {
                    var model =
                        vfi.MachineStateDetails.FirstOrDefault(
                            ms =>
                            (ms.Description.Trim().ToUpper().Equals(newModel.Description.ToUpper()) ||
                            ms.StateCode.Trim().ToUpper().Equals(newModel.StateCode)));
                    if (model == null) {
                        var machineState = new MachineStateDetail {
                            Description = newModel.Description.Trim(),
                            StateCode = newModel.StateCode.ToUpper().Trim(),
                            Active = true,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Timing = newModel.Timing
                        };
                        vfi.MachineStateDetails.Add(machineState);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Cách khăc phục hoặc mã bị trùng! Vui lòng tạo mô tả khác");
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMachineState", ex.Message);
            }
            return View(new GridModel(GetMachineFixManagement()));
        }
        [GridAction]
        public ActionResult UpdateMachineFix(MachineStateDetailModel updateModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(updateModel.Description))
                    throw new AggregateException("Khắc phục không được để trống");
                if (string.IsNullOrWhiteSpace(updateModel.StateCode))
                    throw new AggregateException("Mã khắc phục không được để trống");
                using (var vfi = new vfiContext()) {
                    var existModel = vfi.MachineStateDetails.FirstOrDefault(ms => ms.DetailId == updateModel.DetailId);
                    if (existModel != null) {
                        var model =
                             vfi.MachineStateDetails.FirstOrDefault(
                                 ms =>
                                 (ms.Description.Trim().ToUpper().Equals(updateModel.Description.ToUpper()) ||
                                 ms.StateCode.Trim().ToUpper().Equals(updateModel.StateCode)) &&
                                 ms.DetailId != updateModel.DetailId);
                        if (model != null) {
                            throw new AggregateException("Cách khăc phục hoặc mã bị trùng! Vui lòng tạo mô tả khác");
                        }
                        else {
                            existModel.Description = updateModel.Description.Trim();
                            existModel.StateCode = updateModel.StateCode.ToUpper().Trim();
                            existModel.Active = updateModel.Active;
                            existModel.ModifiedDate = DateTime.Now;
                            existModel.ModifiedUser = HttpContext.User.Identity.Name;
                            existModel.Timing = updateModel.Timing;
                            vfi.SaveChanges();

                        }
                    }
                    else {
                        throw new AggregateException("Bị lỗi !Không tìm thấy lỗi này");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMachineFix", ex.Message);
            }
            return View(new GridModel(GetMachineFixManagement()));
        }
        //

        [GridAction]
        public ActionResult SelectMachineErrorCause(int stateId) {
            var model = GetMachineErrorList(stateId);
            return View(new GridModel(model));
        }
        List<MachineErrorCauseModel> GetMachineErrorList(int stateId) {
            var model = new List<MachineErrorCauseModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var machineErrors = vfi.ErrorCauseForms.Where(ms => ms.StateId == stateId).OrderBy(ms => !ms.Active).ThenBy(ms => ms.StateCode);
                    int i = 0;
                    foreach (var detail in machineErrors) {
                        var entity = new MachineErrorCauseModel {
                            StateId = detail.StateId,
                            StateCode = detail.StateCode,
                            Active = detail.Active,
                            Description = detail.Description,
                            Name = detail.Name,
                            ErrorCauseId = detail.ErrorCauseId,
                            ModifiedDate = detail.ModifiedDate,
                            ModifiedUser = detail.ModifiedUser,
                            Index = ++i,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetMachineErrorList", ex.Message);
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertMachineErrorCause(MachineErrorCauseModel newModel, int stateId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(newModel.Name))
                    throw new AggregateException("Nguyên nhân không được để trống");
                if (string.IsNullOrWhiteSpace(newModel.StateCode))
                    throw new AggregateException("Mã nguyên nhân không được để trống");
                using (var vfi = new vfiContext()) {
                    if (stateId == 1)
                        throw new AggregateException("Lỗi! Không có nguyên nhân cho trạng thái bình thường");
                    var model =
                        vfi.ErrorCauseForms.FirstOrDefault(
                            ms =>
                            (ms.Name.Trim().ToUpper().Equals(newModel.Name.ToUpper()) ||
                            ms.StateCode.Trim().ToUpper().Equals(newModel.StateCode)) &&
                            ms.StateId == stateId);
                    if (model == null) {
                        var entity = new ErrorCauseForm {
                            Description = newModel.Description,
                            Active = true,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Name = newModel.Name.Trim(),
                            StateCode = newModel.StateCode.ToUpper().Trim(),
                            StateId = stateId,
                        };
                        vfi.ErrorCauseForms.Add(entity);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Tên nguyên nhân hoặc mã nguyên nhân bị trùng! Vui lòng tạo khác");
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMachineErrorCause", ex.Message);
            }
            return View(new GridModel(GetMachineErrorList(stateId)));
        }

        [GridAction]
        public ActionResult UpdateMachineErrorCause(MachineErrorCauseModel updateModel, int stateId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(updateModel.Name))
                    throw new AggregateException("Mô tả lỗi không được để trống");
                using (var vfi = new vfiContext()) {
                    var existModel = vfi.ErrorCauseForms.FirstOrDefault(ms => ms.ErrorCauseId == updateModel.ErrorCauseId);
                    if (existModel != null) {
                        var model =
                             vfi.ErrorCauseForms.FirstOrDefault(
                                 ms =>
                                 (ms.Name.Trim().ToUpper().Equals(updateModel.Name.ToUpper()) ||
                                 ms.StateCode.Trim().ToUpper().Equals(updateModel.StateCode)) &&
                                 ms.StateId == updateModel.StateId && ms.ErrorCauseId != updateModel.ErrorCauseId);
                        if (model != null) {
                            throw new AggregateException("Tên nguyên nhân hoặc mã nguyên nhân bị trùng! Vui lòng tạo khác");
                        }
                        else {
                            existModel.Description = (updateModel.Description + "").Trim();
                            existModel.StateCode = updateModel.StateCode.ToUpper().Trim();
                            existModel.Active = updateModel.Active;
                            existModel.ModifiedDate = DateTime.Now;
                            existModel.ModifiedUser = HttpContext.User.Identity.Name;
                            existModel.Name = updateModel.Name.Trim();
                            vfi.SaveChanges();

                        }
                    }
                    else {
                        throw new AggregateException("Bị lỗi !Không tìm thấy nguyên nhân này");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMachineErrorCause", ex.Message);
            }
            return View(new GridModel(GetMachineErrorList(stateId)));
        }
        //


        [GridAction]
        public ActionResult SelectMachineErrorManagement() {
            var model = GetMachineErrorManagement();
            return View(new GridModel(model));
        }
        List<MachineErrorCauseModel> GetMachineErrorManagement() {
            var model = new List<MachineErrorCauseModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var machineErrors = vfi.ErrorCauseForms.OrderBy(ms => !ms.Active).ThenBy(ms => ms.StateCode);
                    int i = 0;
                    foreach (var detail in machineErrors) {
                        var entity = new MachineErrorCauseModel {
                            StateId = detail.StateId,
                            StateCode = detail.StateCode,
                            Active = detail.Active,
                            Description = detail.Description,
                            Name = detail.Name,
                            ErrorCauseId = detail.ErrorCauseId,
                            ModifiedDate = detail.ModifiedDate,
                            ModifiedUser = detail.ModifiedUser,
                            Index = ++i,
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetMachineErrorManagement", ex.Message);
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertMachineError(MachineErrorCauseModel newModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(newModel.Name))
                    throw new AggregateException("Nguyên nhân không được để trống");
                if (string.IsNullOrWhiteSpace(newModel.StateCode))
                    throw new AggregateException("Mã nguyên nhân không được để trống");
                using (var vfi = new vfiContext()) {
                    var model =
                        vfi.ErrorCauseForms.FirstOrDefault(
                            ms =>
                            (ms.Name.Trim().ToUpper().Equals(newModel.Name.ToUpper()) ||
                            ms.StateCode.Trim().ToUpper().Equals(newModel.StateCode)));
                    if (model == null) {
                        var entity = new ErrorCauseForm {
                            Description = newModel.Description,
                            Active = true,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Name = newModel.Name.Trim(),
                            StateCode = newModel.StateCode.ToUpper().Trim(),
                        };
                        vfi.ErrorCauseForms.Add(entity);
                        vfi.SaveChanges();
                    }
                    else {
                        throw new AggregateException("Tên nguyên nhân hoặc mã nguyên nhân bị trùng! Vui lòng tạo khác");
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMachineError", ex.Message);
            }
            return View(new GridModel(GetMachineErrorManagement()));
        }

        [GridAction]
        public ActionResult UpdateMachineError(MachineErrorCauseModel updateModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                if (string.IsNullOrWhiteSpace(updateModel.Name))
                    throw new AggregateException("Mô tả lỗi không được để trống");
                using (var vfi = new vfiContext()) {
                    var existModel = vfi.ErrorCauseForms.FirstOrDefault(ms => ms.ErrorCauseId == updateModel.ErrorCauseId);
                    if (existModel != null) {
                        var model =
                             vfi.ErrorCauseForms.FirstOrDefault(
                                 ms =>
                                 (ms.Name.Trim().ToUpper().Equals(updateModel.Name.ToUpper()) ||
                                 ms.StateCode.Trim().ToUpper().Equals(updateModel.StateCode)) &&
                                 ms.ErrorCauseId != updateModel.ErrorCauseId);
                        if (model != null) {
                            throw new AggregateException("Tên nguyên nhân hoặc mã nguyên nhân bị trùng! Vui lòng tạo khác");
                        }
                        else {
                            existModel.Description = (updateModel.Description + "").Trim();
                            existModel.StateCode = updateModel.StateCode.ToUpper().Trim();
                            existModel.Active = updateModel.Active;
                            existModel.ModifiedDate = DateTime.Now;
                            existModel.ModifiedUser = HttpContext.User.Identity.Name;
                            existModel.Name = updateModel.Name.Trim();
                            vfi.SaveChanges();

                        }
                    }
                    else {
                        throw new AggregateException("Bị lỗi !Không tìm thấy nguyên nhân này");
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMachineError", ex.Message);
            }
            return View(new GridModel(GetMachineErrorManagement()));
        }
        //

        public ActionResult GetEmployeeState(int userId) {
            var model = new List<MachineRepairFormModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var employees = vfi.Employees.Where(e => e.Repair == true).OrderBy(e => e.EmployeeName);
                    foreach (var employee in employees) {
                        var entity = new MachineRepairFormModel {
                            EmployeeId = employee.EmployeeId,
                            EmployeeName = employee.EmployeeName,
                        };
                        var form =
                            vfi.RepairFormDetails.FirstOrDefault(
                                rd =>
                                rd.EmployeeId == employee.EmployeeId && rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.None);
                        if (form != null) {
                            entity.MachineId = form.MachineRepairForm.MachineId;
                            entity.MachineName = form.MachineRepairForm.Machine.MachineName;
                            entity.StartDate = form.StartDate;
                        }
                        var forms = vfi.RepairFormDetails.Where(
                            rd =>
                            rd.EmployeeId == employee.EmployeeId &&
                            rd.Status != (byte)MyUtilities.Machine.State.RepairStatus.None &&
                            rd.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                            rd.StartDate.Day == DateTime.Now.Day &&
                            rd.StartDate.Month == DateTime.Now.Month &&
                            rd.StartDate.Year == DateTime.Now.Year);
                        if (forms.Any()) {

                        }
                        model.Add(entity);
                    }
                    return PartialView("EmployeeState", model);
                }
            }
            catch (Exception) {
                return null;
            }

        }
        [GridAction]
        public ActionResult SelectMachineErrorStateDetail(int formId) {
            var model = new List<RepairFormDetailModel>();
            try {
                model = GetMachineErrorStateDetails(formId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMachineErrorStateDetail", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<RepairFormDetailModel> GetMachineErrorStateDetails(int formId) {
            var model = new List<RepairFormDetailModel>();
            var exchangeRate = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.ExchangeToVndRate);
            using (var vfi = new vfiContext()) {
                var repairDetails = vfi.RepairFormDetails.Where(rd => rd.FormId == formId);
                foreach (var repairFormDetail in repairDetails) {
                    var entity = new RepairFormDetailModel {
                        ModifiedDate = repairFormDetail.ModifiedDate,
                        ModifiedUser = repairFormDetail.FinishUser,
                        Status = repairFormDetail.Status,
                        StatusName = MyUtilities.Machine.State.GetRepairStatusText(repairFormDetail.Status),
                        FinishDate = repairFormDetail.FinishDate,
                        FinishUser = repairFormDetail.FinishUser,
                        StartDate = repairFormDetail.StartDate,
                        StartUser = repairFormDetail.StartUser,
                        Note = repairFormDetail.Note,
                        Shift = repairFormDetail.Shift,
                        ToDate = DateTime.Now,
                        //FromDate = from,
                        MoreTime = repairFormDetail.MoreTime,
                        StateId = repairFormDetail.MachineRepairForm.StateId,
                        DetailId = repairFormDetail.DetailId,
                        FormId = repairFormDetail.FormId,
                        //FixId = repairFormDetail.FixId.Value,
                        //HowToFix = repairFormDetail.MachineStateDetail.Description,
                        //Timing = repairFormDetail.MachineStateDetail.Timing,
                        FixQuantity = repairFormDetail.FixQuantity ?? 0,

                    };
                    if (repairFormDetail.FixId != null) {
                        entity.FixId = repairFormDetail.FixId.Value;
                        entity.HowToFix = repairFormDetail.MachineStateDetail.StateCode + "." + repairFormDetail.MachineStateDetail.Description;
                        entity.Timing = repairFormDetail.MachineStateDetail.Timing;
                    }
                    if (repairFormDetail.EmployeeId > 0) {
                        entity.EmployeeId = repairFormDetail.EmployeeId;
                        entity.EmployeeName = repairFormDetail.Employee.EmployeeName;
                    }
                    //if (!string.IsNullOrWhiteSpace(entity.StartDate))
                    //{
                    var trackUpProducion = vfi.TrackUpMachines.Where(
                        tp =>
                        tp.DeliveryDate != null && tp.DeliveryDate <= entity.StartDate &&
                        tp.MachineId == repairFormDetail.MachineRepairForm.MachineId &&
                        tp.ProductId == repairFormDetail.MachineRepairForm.ProductId &&
                        tp.Status != (byte)MyUtilities.Transaction.Status.Cancel)
                                              .OrderByDescending(tp => tp.DeliveryDate)
                                              .ThenByDescending(tp => tp.ModifiedDate).FirstOrDefault();
                    if (trackUpProducion != null) {
                        entity.Productivity = trackUpProducion.RealProductivity;
                        entity.ProductPrice = MyUtilities.Product.ProductVndPrice(trackUpProducion.Product.UnitPrice,1, exchangeRate);
                        //if (trackUpProducion.Product.UnitPrice != null ||
                        //    trackUpProducion.Product.UnitPrice != 0) {
                        //    var productPrice = Math.Round(trackUpProducion.Product.UnitPrice.Value, 4);
                        //    var temp = Convert.ToInt32(productPrice);
                        //    if (productPrice - temp != 0)
                        //        productPrice = Math.Round(productPrice * MyUtilities.Product.ExchangeRateDesign, 0);
                        //    entity.ProductPrice = productPrice;
                        //}
                    }
                    //}
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertMachineErrorStateDetail(int formId, RepairFormDetailModel inserted) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var form = vfi.MachineRepairForms.FirstOrDefault(rf => rf.FormId == formId);
                    if (form == null) {
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu.");
                    }
                    if (form.Status != (byte)MyUtilities.Machine.State.RepairStatus.None)
                        throw new AggregateException("Lỗi! Phiếu này không được chỉnh sửa.");
                    if (inserted.StartDate == null) {
                        throw new AggregateException("Lỗi! Vui lòng chọn ngày giờ kết thúc hoặc F5 làm lại.");
                    }
                    if (inserted.StartDate.Value < form.CauseDate)
                        throw new AggregateException("Lỗi! Thời gian sửa máy phải lớn hơn thời gian bị lỗi.");
                    if (inserted.Shift == 0)
                        throw new AggregateException("Lỗi! Cập nhật ca nhân viên sửa máy.");
                    var employeeId = 0;
                    try {
                        employeeId = Convert.ToInt32(inserted.EmployeeName);
                    }
                    catch (FormatException) {
                        employeeId = inserted.EmployeeId;
                    }
                    if (employeeId == 0)
                        throw new AggregateException("Lỗi! Chọn lại nhân viên.");
                    var detail =
                        vfi.RepairFormDetails.FirstOrDefault(
                            rd => rd.EmployeeId == employeeId && rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.None);
                    if (detail != null) {
                        throw new AggregateException("Lỗi! Nhân viên " + detail.Employee.EmployeeName + " đang bận sửa máy " +
                                                     detail.MachineRepairForm.Machine.MachineName + "!");
                    }
                    var trackingMachine = (from ms in vfi.TrackingRepairEmployees
                                           where ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                           ms.RepairEmployeeId == employeeId &&
                                           (inserted.StartDate >= ms.StartDate && inserted.StartDate <= ms.FinishDate)
                                           select ms).FirstOrDefault();
                    if (trackingMachine != null) {
                        throw new AggregateException("Lỗi! Nhân viên sửa máy đang theo dõi ở máy " + trackingMachine.Machine.MachineName);
                    }
                    detail = new RepairFormDetail {
                        FormId = formId,
                        EmployeeId = employeeId,
                        StartDate = inserted.StartDate.Value,
                        StartUser = HttpContext.User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        Note = inserted.Note,
                        MoreTime = inserted.MoreTime,
                        Shift = inserted.Shift,
                        Status = 1,
                    };
                    form.RepairFormDetails.Add(detail);
                    var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == form.MachineId);
                    if (machine.StateId != MyUtilities.Machine.State.Setup)
                        machine.StateId = MyUtilities.Machine.State.Repairing;
                    machine.ModifiedFix = detail.StartDate;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMachineErrorStateDetail", ex.Message);
            }
            return View(new GridModel(GetMachineErrorStateDetails(formId)));
        }

        [GridAction]
        public ActionResult UpdateMachineErrorStateDetail(int formId, RepairFormDetailModel updated) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var form = vfi.MachineRepairForms.FirstOrDefault(rf => rf.FormId == formId);
                    var detail = vfi.RepairFormDetails.FirstOrDefault(rd => rd.DetailId == updated.DetailId);
                    if (detail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết sửa.");
                    byte status = 1;
                    try {
                        status = Convert.ToByte(updated.StatusName);
                    }
                    catch (FormatException) { }
                    if (status == 1)
                        throw new AggregateException("Lỗi! Vui lòng chọn kết quả sửa máy!");
                    var fixId = 0;
                    try {
                        fixId = Convert.ToInt32(updated.HowToFix);
                    }
                    catch (FormatException) {
                        fixId = updated.FixId.Value;
                    }
                    if (fixId == 0)
                        throw new AggregateException("Lỗi! Chọn lại cách khắc phục.");
                    if (updated.FinishDate == null) {
                        throw new AggregateException("Lỗi! Vui lòng chọn ngày giờ kết thúc hoặc F5 làm lại.");
                    }
                    detail.FinishDate = updated.FinishDate;
                    detail.FinishUser = HttpContext.User.Identity.Name;
                    detail.Note = updated.Note;
                    detail.MoreTime = updated.MoreTime;
                    if (detail.FinishDate <= detail.StartDate)
                        throw new AggregateException("Lỗi! Thời gian hoàn thành nhỏ hoặc bằng hơn thời gian bắt đầu!Vui lòng chọn lại");
                    var trackingMachine = (from ms in vfi.TrackingRepairEmployees
                                           where ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                           ms.RepairEmployeeId == detail.EmployeeId &&
                                            (
                                                (ms.StartDate >= detail.StartDate && ms.StartDate <= detail.FinishDate) ||
                                                (ms.FinishDate >= detail.StartDate && ms.FinishDate <= detail.FinishDate) ||
                                                (ms.StartDate < detail.StartDate && ms.FinishDate > detail.FinishDate)
                                           )
                                           select ms).FirstOrDefault();
                    if (trackingMachine != null) {
                        throw new AggregateException("Lỗi! Nhân viên sửa máy đang theo dõi ở máy " + trackingMachine.Machine.MachineName);
                    }
                    if (status == (byte)MyUtilities.Machine.State.RepairStatus.Delete && string.IsNullOrWhiteSpace(updated.Note)) {
                        throw new AggregateException("Hủy phiếu vui lòng ghi lý do vào ghi chú !");
                    }
                    detail.FixQuantity = updated.FixQuantity;
                    detail.Status = status;
                    if (detail.Status == (byte)MyUtilities.Machine.State.RepairStatus.Finish && updated.FixQuantity == 0)
                        throw new AggregateException("Lỗi! Phải cập nhật số lượng chỉnh máy");
                    detail.FixId = fixId;
                    if (detail.Status == (byte)MyUtilities.Machine.State.RepairStatus.Finish) {
                        var checkDetail =
                            vfi.RepairFormDetails.Any(
                                rd =>
                                rd.FormId == form.FormId && rd.DetailId != detail.DetailId &&
                                rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.Wrong);
                        if (checkDetail) {
                            throw new AggregateException("Lỗi! Phiếu tình trạng máy đã bị sai lỗi! Không thể hoàn thành sửa máy");
                        }
                    }
                    else if (detail.Status == (byte)MyUtilities.Machine.State.RepairStatus.Wrong) {
                        var checkDetail =
                            vfi.RepairFormDetails.Any(
                                rd =>
                                rd.FormId == form.FormId && rd.DetailId != detail.DetailId &&
                                rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.Finish);
                        if (checkDetail) {
                            throw new AggregateException("Lỗi! Phiếu tình trạng máy đã có người hoàn thành! Không bị sai lỗi");
                        }
                    }
                    var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == form.MachineId);

                    switch (detail.Status) {
                        case (byte)MyUtilities.Machine.State.RepairStatus.UnFinish: {
                                var elseDetail = vfi.RepairFormDetails.FirstOrDefault(
                                   rd =>
                                   rd.FormId == form.FormId && rd.DetailId != detail.DetailId &&
                                   rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.None);
                                if (elseDetail == null) {
                                    elseDetail =
                                        vfi.RepairFormDetails.FirstOrDefault(
                                            rd =>
                                            rd.FormId == form.FormId && rd.DetailId != detail.DetailId &&
                                            (rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.Finish ||
                                             rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.Wrong));
                                    if (elseDetail != null) {
                                        machine.StateId = MyUtilities.Machine.State.Normal;
                                        machine.StartProductionDate = elseDetail.FinishDate;
                                        machine.ModifiedState = null;
                                        machine.ModifiedFix = null;
                                        form.Status = elseDetail.Status;
                                        form.FinishDate = elseDetail.FinishDate;
                                    }
                                }
                            }
                            break;
                        case (byte)MyUtilities.Machine.State.RepairStatus.Delete: {
                            }
                            break;
                        case (byte)MyUtilities.Machine.State.RepairStatus.Finish:
                        case (byte)MyUtilities.Machine.State.RepairStatus.Wrong: {
                                if (form.ErrorCauseId == null) {
                                    throw new AggregateException("Lỗi! Hoàn Thành phiếu phải có nguyên nhân");
                                }
                                var elseDetail =
                                    vfi.RepairFormDetails.FirstOrDefault(
                                        rd =>
                                        rd.FormId == form.FormId && rd.DetailId != detail.DetailId &&
                                        rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.None);
                                if (elseDetail == null) {
                                    machine.StateId = MyUtilities.Machine.State.Normal;
                                    machine.StartProductionDate = detail.FinishDate;
                                    machine.ModifiedState = null;
                                    machine.ModifiedFix = null;
                                    form.Status = detail.Status;
                                    form.FinishDate = detail.FinishDate;
                                }
                            }
                            break;
                        case (byte)MyUtilities.Machine.State.RepairStatus.Stop: {
                                machine.StateId = detail.MachineRepairForm.StateId;
                            }
                            break;
                        default:
                            break;
                    }


                    //if (detail.Status == (byte)MyUtilities.Machine.State.RepairStatus.Stop) {
                    //    machine.StateId = detail.MachineRepairForm.StateId;
                    //}
                    //else 
                    //    if (detail.Status == (byte)MyUtilities.Machine.State.RepairStatus.Finish ||
                    //    detail.Status == (byte)MyUtilities.Machine.State.RepairStatus.Wrong) {
                    //    var elseDetail =
                    //        vfi.RepairFormDetails.FirstOrDefault(
                    //            rd =>
                    //            rd.FormId == form.FormId && rd.DetailId != detail.DetailId &&
                    //            rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.None);
                    //    if (elseDetail == null) {
                    //        machine.StateId = MyUtilities.Machine.State.Normal;
                    //        machine.StartProductionDate = detail.FinishDate;
                    //        machine.ModifiedState = null;
                    //        machine.ModifiedFix = null;
                    //        form.Status = detail.Status;
                    //        form.FinishDate = detail.FinishDate;
                    //    }
                    //}
                    //else 
                    //    {
                    //    var elseDetail =
                    //           vfi.RepairFormDetails.FirstOrDefault(
                    //               rd =>
                    //               rd.FormId == form.FormId && rd.DetailId != detail.DetailId &&
                    //               rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.None);
                    //    if (elseDetail == null) {
                    //        elseDetail =
                    //            vfi.RepairFormDetails.FirstOrDefault(
                    //                rd =>
                    //                rd.FormId == form.FormId && rd.DetailId != detail.DetailId &&
                    //                (rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.Finish ||
                    //                 rd.Status == (byte)MyUtilities.Machine.State.RepairStatus.Wrong));
                    //        if (elseDetail != null) {
                    //            machine.StateId = MyUtilities.Machine.State.Normal;
                    //            machine.StartProductionDate = elseDetail.FinishDate;
                    //            machine.ModifiedState = null;
                    //            machine.ModifiedFix = null;
                    //            form.Status = elseDetail.Status;
                    //        }
                    //    }
                    //}
                    vfi.SaveChanges();
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMachineErrorStateDetail", ex.Message);
            }
            return View(new GridModel(GetMachineErrorStateDetails(formId)));
        }

        [GridAction]
        public ActionResult DeleteMachineErrorStateDetail(int formId, int detailId) {
            try {
                using (var vfi = new vfiContext()) {
                    var detail = vfi.RepairFormDetails.FirstOrDefault(rd => rd.DetailId == detailId);
                    if (detail == null)
                        throw new AggregateException("Lỗi! Không tìm thấy chi tiết sửa.");
                    detail.Status = (byte)MyUtilities.Machine.State.RepairStatus.Delete;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteMachineErrorStateDetail", ex.Message);

            }
            return View(new GridModel(GetMachineErrorStateDetails(formId)));
        }

        [GridAction]
        public ActionResult SelectMachineErrorState(int machineId, string fromDate, string toDate, int status) {
            var model = new List<MachineRepairFormModel>();
            if (status == -1)
                return View(new GridModel(model));
            try {
                model = GetMachineErrorStateList(0, machineId, "", fromDate, toDate, status, 0);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMachineErrorState", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<MachineRepairFormModel> GetMachineErrorStateList(
            int formId, int machineId, string machineName,
            string fromDate, string toDate, int status,
            int reportType
            ) {
            var model = new List<MachineRepairFormModel>();
            var from = MyUtilities.Function.ParseDateTime(fromDate);
            var to = MyUtilities.Function.ParseDateTime(toDate);
            try {
                using (var vfi = new vfiContext()) {
                    var machineStates = vfi.MachineRepairForms.Where(ms => ms.SectionId == null).ToList();
                    if (machineId != 0)
                        machineStates = machineStates.Where(mr => mr.MachineId == machineId).ToList();
                    if (!string.IsNullOrWhiteSpace(toDate))
                        machineStates = machineStates.Where(mr => mr.CreateDate >= from && mr.CreateDate <= to).ToList();
                    if (status == 0) {
                        if (string.IsNullOrWhiteSpace(toDate))
                            machineStates =
                                machineStates.Where(
                                    ms =>
                                        ms.Status == (byte)MyUtilities.Machine.State.RepairStatus.None ||
                                        ms.Status == (byte)MyUtilities.Machine.State.RepairStatus.UnFinish ||
                                        ms.Status == (byte)MyUtilities.Machine.State.RepairStatus.Stop).ToList();
                        else
                            machineStates =
                                machineStates.Where(ms => ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete).ToList();
                    }
                    else if (status != -1)
                        machineStates = machineStates.Where(mr => mr.Status == status).ToList();

                    if (!string.IsNullOrWhiteSpace(machineName)) {
                        if (machineName.Equals("C")) {
                            machineStates = machineStates.Where(mr => mr.Machine.MachineName.Contains(machineName.ToUpper()) &&
                                                                        !mr.Machine.MachineName.Contains("CNC"))
                                                        .ToList();
                        }
                        else {
                            machineStates = machineStates.Where(mr => mr.Machine.MachineName.Contains(machineName.ToUpper()))
                                                        .ToList();
                        }
                    }
                    var exchangeRate = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.ExchangeToVndRate);
                    foreach (var machineState in machineStates) {
                        var entity = new MachineRepairFormModel {
                            FormId = machineState.FormId,
                            MachineId = machineState.MachineId,
                            MachineName = machineState.Machine.MachineName,
                            ProductId = machineState.ProductId,
                            ProductCode = machineState.Product.ProductCode,
                            StateId = machineState.StateId,
                            StateCode = machineState.MachineState.StateCode,
                            StateName = machineState.MachineState.Description,
                            EstimateTime = machineState.MachineState.EstimateTime,
                            //ErrorCause = machineState.ErrorCause,
                            CreateDate = machineState.CreateDate,
                            CreateUser = machineState.CreateUser,
                            Status = machineState.Status,
                            StatusName = MyUtilities.Machine.State.GetRepairStatusText(machineState.Status),
                            FinishDate = machineState.FinishDate,
                            FinishUser = machineState.FinishUser,
                            StartDate = machineState.StartDate,
                            StartUser = machineState.StartUser,
                            Note = machineState.Note,
                            Shift = machineState.Shift,
                            ToDate = to,
                            FromDate = from,
                            MoreTime = machineState.MoreTime,
                            CauseDate = machineState.CauseDate,
                            ErrorQuantity = machineState.ErrorQuantity ?? 0,
                            Time = "15h và 21h",
                            Title = "BIÊN BẢN BÀN GIAO MÁY"
                        };
                        if (reportType == 2) {
                            entity.Time = "7h";
                            entity.Title = "HIỆN TRẠNG MÁY";
                        }
                        if (machineState.FixId != null) {
                            entity.FixId = machineState.FixId.Value;
                            entity.HowToFix = machineState.MachineStateDetail.StateCode + "." + machineState.MachineStateDetail.Description;
                            entity.Timing = machineState.MachineStateDetail.Timing;
                        }
                        if (machineState.ErrorCauseId != null) {
                            entity.ErrorCauseId = machineState.ErrorCauseId.Value;
                            entity.ErrorCause = machineState.ErrorCauseForm.StateCode + "." + machineState.ErrorCauseForm.Name;
                        }
                        var details = machineState.RepairFormDetails
                            .Where(rd => rd.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete);
                        if (details.Any()) {
                            foreach (
                                var detail in
                                    machineState.RepairFormDetails.Where(
                                        rd => rd.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete)) {
                                entity.EmployeeName += detail.Employee.EmployeeName + ",";
                            }
                            entity.DetailCount = details.Count();
                        }
                        if (machineState.CreateUserId != null) {
                            entity.CreateUserId = machineState.CreateUserId.Value;
                            entity.CreateUser = machineState.Employee1.EmployeeName;
                        }
                        if (!string.IsNullOrWhiteSpace(toDate)) {
                            var trackUpProducion = vfi.TrackUpMachines.Where(
                                tp =>
                                tp.DeliveryDate != null && tp.DeliveryDate <= entity.CreateDate &&
                                tp.MachineId == entity.MachineId && tp.ProductId == entity.ProductId &&
                                tp.Status != (byte)MyUtilities.Transaction.Status.Cancel)
                                                      .OrderByDescending(tp => tp.DeliveryDate)
                                                      .ThenByDescending(tp => tp.ModifiedDate).FirstOrDefault();
                            if (trackUpProducion != null) {
                                entity.Productivity = trackUpProducion.RealProductivity;
                                entity.ProductPrice = MyUtilities.Product.ProductVndPrice(trackUpProducion.Product.UnitPrice,1,exchangeRate);
                                //if (trackUpProducion.Product.UnitPrice != null ||
                                //    trackUpProducion.Product.UnitPrice != 0) {
                                //    var productPrice = Math.Round(trackUpProducion.Product.UnitPrice.Value, 4);
                                //    var temp = Convert.ToInt32(productPrice);
                                //    if (productPrice - temp != 0)
                                //        productPrice = Math.Round(productPrice * MyUtilities.Product.ExchangeRateDesign, 0);
                                //    entity.ProductPrice = productPrice;
                                //}
                            }
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex.Message);
            }
            return model.OrderBy(m => m.EmployeeName).ThenBy(m => m.StartDate).ThenBy(m => m.MachineName).ToList();
        }

        List<MachineRepairFormModel> GetMachineErrorStateList2(int formId, int machineId, string fromDate, string toDate, int status) {
            var model = new List<MachineRepairFormModel>();
            var ci = new CultureInfo("vi-VN");
            var from = string.IsNullOrWhiteSpace(fromDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(fromDate, ci);
            var to = string.IsNullOrWhiteSpace(toDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(toDate, ci);
            try {
                using (var vfi = new vfiContext()) {
                    var machineStates = vfi.MachineRepairForms.Where(ms => ms.SectionId == null).ToList();
                    if (machineId != 0)
                        machineStates = machineStates.Where(mr => mr.MachineId == machineId).ToList();
                    if (!string.IsNullOrWhiteSpace(toDate))
                        machineStates = machineStates.Where(mr => mr.CauseDate >= from && mr.CauseDate <= to).ToList();
                    if (status != -1)
                        machineStates = machineStates.Where(mr => mr.Status == status).ToList();

                    var exchangeRate = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.ExchangeToVndRate);
                    foreach (var machineState in machineStates) {
                        var entity = new MachineRepairFormModel {
                            FormId = machineState.FormId,
                            MachineId = machineState.MachineId,
                            MachineName = machineState.Machine.MachineName,
                            ProductId = machineState.ProductId,
                            ProductCode = machineState.Product.ProductCode,
                            StateId = machineState.StateId,
                            StateName = machineState.MachineState.Description,
                            ErrorCause = machineState.ErrorCause,
                            CreateDate = machineState.CreateDate,
                            CreateUser = machineState.CreateUser,
                            Status = machineState.Status,
                            StatusName = MyUtilities.Machine.State.GetRepairStatusText(machineState.Status),
                            FinishDate = machineState.FinishDate,
                            FinishUser = machineState.FinishUser,
                            StartDate = machineState.StartDate,
                            StartUser = machineState.StartUser,
                            Note = machineState.Note,
                            Shift = machineState.Shift,
                            ToDate = to,
                            FromDate = from,
                            MoreTime = machineState.MoreTime,
                            CauseDate = machineState.CauseDate,
                        };
                        //if (machineState.FixId != null)
                        //{
                        //    entity.FixId = machineState.FixId.Value;
                        //    entity.HowToFix = machineState.MachineStateDetail.Description;
                        //    entity.Timing = machineState.MachineStateDetail.Timing;
                        //}
                        if (machineState.RepairFormDetails.Any()) {
                            foreach (var detail in machineState.RepairFormDetails) {
                                entity.EmployeeName += detail.Employee.EmployeeName + ",";
                            }
                        }
                        if (machineState.CreateUserId != null) {
                            entity.CreateUserId = machineState.CreateUserId.Value;
                            entity.CreateUser = machineState.Employee1.EmployeeName;
                        }
                        if (!string.IsNullOrWhiteSpace(toDate)) {
                            var trackUpProducion = vfi.TrackUpMachines.Where(
                                tp =>
                                tp.DeliveryDate != null && tp.DeliveryDate <= entity.CreateDate &&
                                tp.MachineId == entity.MachineId && tp.ProductId == entity.ProductId &&
                                tp.Status != (byte)MyUtilities.Transaction.Status.Cancel)
                                                      .OrderByDescending(tp => tp.DeliveryDate)
                                                      .ThenByDescending(tp => tp.ModifiedDate).FirstOrDefault();
                            if (trackUpProducion != null) {
                                entity.Productivity = trackUpProducion.RealProductivity;
                                entity.ProductPrice = MyUtilities.Product.ProductVndPrice(trackUpProducion.Product.UnitPrice,1,exchangeRate);
                                //if (trackUpProducion.Product.UnitPrice != null ||
                                //    trackUpProducion.Product.UnitPrice != 0) {
                                //    var productPrice = Math.Round(trackUpProducion.Product.UnitPrice.Value, 4);
                                //    var temp = Convert.ToInt32(productPrice);
                                //    if (productPrice - temp != 0)
                                //        productPrice = Math.Round(productPrice * MyUtilities.Product.ExchangeRateDesign, 0);
                                //    entity.ProductPrice = productPrice;
                                //}
                            }
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex.Message);
            }
            return model.OrderBy(m => m.EmployeeName).ThenBy(m => m.StartDate).ThenBy(m => m.MachineName).ToList();
        }
        [GridAction]
        public ActionResult InsertMachineErrorState(MachineRepairFormModel newModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var machineId = 1;
                    try {
                        machineId = Convert.ToInt32(newModel.MachineName);
                    }
                    catch (FormatException) {
                        machineId = newModel.MachineId;
                    }
                    var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == machineId);
                    if (machine == null)
                        throw new AggregateException("Lỗi ! Không tìm thấy máy");
                    var machineError =
                        vfi.MachineRepairForms.FirstOrDefault(mrf => mrf.MachineId == machineId && mrf.Status == (byte)MyUtilities.Machine.State.RepairStatus.None);
                    if (machineError != null)
                        throw new AggregateException("Lỗi! Máy " + machine.MachineName +
                                                     " đang trong tình trạng chưa xong! Hãy hoàn thành phiếu trước đó.");
                    //machine.ModifiedState = DateTime.Now;
                    var stateId = 0;
                    try {
                        stateId = Convert.ToInt32(newModel.StateName);
                    }
                    catch (FormatException) {
                        stateId = newModel.StateId;
                    }
                    if (stateId == 0)
                        throw new AggregateException("Lỗi! Vui lòng chọn tình trạng máy");
                    var machineState = vfi.MachineStates.FirstOrDefault(x => x.StateId == stateId);
                    var errorId = 0;
                    try {
                        errorId = Convert.ToInt32(newModel.ErrorCause);
                    }
                    catch (FormatException) {
                        errorId = newModel.ErrorCauseId;
                    }
                    //if (errorId == 0)
                    //    throw new AggregateException("Lỗi! Vui lòng chọn nguyên nhân");
                    var createUserId = 0;
                    try {
                        createUserId = Convert.ToInt32(newModel.CreateUser);
                    }
                    catch (FormatException) {
                        createUserId = newModel.CreateUserId;
                    }
                    if (createUserId == 0)
                        throw new AggregateException("Lỗi! Vui lòng chọn nhân viên tạo phiếu");
                    var createUser = vfi.Employees.FirstOrDefault(e => e.EmployeeId == createUserId);
                    machineError = new MachineRepairForm {
                        CreateUser = createUser.EmployeeName,
                        CreateDate = DateTime.Now,
                        Status = (byte)MyUtilities.Machine.State.RepairStatus.None,
                        MachineId = machineId,
                        ErrorCause = newModel.ErrorCause,
                        //ErrorCauseId = errorId,
                        StateId = stateId,
                        Note = newModel.Note,
                        Shift = 0,
                        MoreTime = 0,
                        CauseDate = newModel.CauseDate.Value,
                        CreateUserId = createUserId,
                        ErrorQuantity = newModel.ErrorQuantity
                    };
                    if (errorId != 0) {
                        machineError.ErrorCauseId = errorId;
                    }
                    if (machine.ModifiedState == null || machine.ModifiedState < machineError.CauseDate) {
                        machine.ModifiedState = machineError.CauseDate;
                        machine.ModifiedFix = null;
                    }
                    machine.StateId = machineError.StateId;
                    machine.ErrorCause = machineError.ErrorCause;
                    if (!machineState.IsSetProduct) {
                        var lastTrack =
                            (from t in vfi.TrackUpMachines
                             where
                             t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                             t.MachineId == machine.MachineId &&
                             ((t.DeliveryDate != null
                                 ? t.DeliveryDate.Value <= machineError.CauseDate
                                 : t.StartDate <= machineError.CauseDate)
                             || (t.StartDate <= machineError.CauseDate))
                             select new {
                                 t.MachineId,
                                 t.ProductId,
                                 Date = t.DeliveryDate != null ? t.DeliveryDate.Value : t.StartDate
                             })
                            .OrderByDescending(t => t.Date)
                            .FirstOrDefault();
                        if (lastTrack != null) {
                            machineError.ProductId = lastTrack.ProductId;
                        }
                        else {
                            var smartMachine = vfi.SmartProductions.FirstOrDefault(sp => sp.MachineId == machineId);
                            if (smartMachine != null)
                                machineError.ProductId = smartMachine.ProductId.Value;
                            else {
                                var productId = 1;
                                try {
                                    productId = Convert.ToInt32(newModel.ProductCode);
                                }
                                catch (FormatException) {
                                    productId = newModel.ProductId;
                                }
                                if (productId == 0)
                                    throw new AggregateException("Lỗi! Lên máy phải có sản phẩm đi kèm. Vui lòng chọn sản phẩm.");
                                machineError.ProductId = productId;
                            }
                        }
                    }
                    else {
                        var productId = 1;
                        try {
                            productId = Convert.ToInt32(newModel.ProductCode);
                        }
                        catch (FormatException) {
                            productId = newModel.ProductId;
                        }
                        if (productId == 0)
                            throw new AggregateException("Lỗi! Lên máy phải có sản phẩm đi kèm. Vui lòng chọn sản phẩm.");
                        machineError.ProductId = productId;
                    }
                    vfi.MachineRepairForms.Add(machineError);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMachineErrorState", ex.Message);
            }
            return View(new GridModel(GetMachineErrorStateList(0, 0, "", "", "", 1, 0)));
        }

        [GridAction]
        public ActionResult UpdateMachineErrorState(MachineRepairFormModel updateModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == updateModel.MachineId);
                    if (machine == null)
                        throw new AggregateException("Lỗi ! Không tìm thấy máy này");
                    if (string.IsNullOrWhiteSpace(updateModel.HowToFix))
                        throw new AggregateException("Lỗi ! Khắc phục để trống");
                    var form = vfi.MachineRepairForms.FirstOrDefault(mr => mr.FormId == updateModel.FormId);

                    if (form.FixId == null) {
                        if (updateModel.StartDate == null) {
                            throw new AggregateException("Lỗi! Vui lòng chọn ngày giờ kết thúc hoặc F5 làm lại.");
                        }
                        if (updateModel.Shift == 0)
                            throw new AggregateException("Lỗi! Cập nhật ca nhân viên sửa máy.");
                        form.StartDate = updateModel.StartDate;
                        form.StartUser = HttpContext.User.Identity.Name;
                        var employeeId = 0;
                        try {
                            employeeId = Convert.ToInt32(updateModel.EmployeeName);
                        }
                        catch (FormatException) {
                            employeeId = updateModel.EmployeeId;
                        }
                        var fixId = 0;
                        try {
                            fixId = Convert.ToInt32(updateModel.HowToFix);
                        }
                        catch (FormatException) {
                            fixId = updateModel.FixId;
                        }
                        var errorId = 0;
                        try {
                            errorId = Convert.ToInt32(updateModel.ErrorCause);
                        }
                        catch (FormatException) {
                            errorId = updateModel.ErrorCauseId;
                        }
                        if (employeeId == 0)
                            throw new AggregateException("Lỗi! Chọn lại nhân viên.");
                        if (fixId == 0)
                            throw new AggregateException("Lỗi! Chọn lại cách khắc phục.");
                        form.EmployeeId = employeeId;
                        if (errorId == 0) {
                            form.ErrorCauseId = null;
                        }
                        else
                            form.ErrorCauseId = errorId;
                        form.FixId = fixId;
                        form.Shift = updateModel.Shift;
                    }
                    else {
                        byte status = 0;
                        try {
                            status = Convert.ToByte(updateModel.StatusName);
                        }
                        catch (FormatException) {

                        }
                        if (status == 0)
                            throw new AggregateException("Lỗi! Vui lòng chọn đạt hoặc không đạt!");
                        form.Status = status;
                        if (updateModel.FinishDate == null) {
                            throw new AggregateException("Lỗi! Vui lòng chọn ngày giờ kết thúc hoặc F5 làm lại.");
                        }
                        form.FinishDate = updateModel.FinishDate;
                        form.FinishUser = HttpContext.User.Identity.Name;
                        form.MoreTime = updateModel.MoreTime;
                        if (form.FinishDate < form.StartDate)
                            throw new AggregateException("Lỗi! Thời gian hoàn thành nhỏ hoặc bằng hơn thời gian bắt đầu!Vui lòng chọn lại");
                        if (string.IsNullOrWhiteSpace(updateModel.ErrorCause) && string.IsNullOrWhiteSpace(form.ErrorCause))
                            throw new AggregateException("Lỗi! Nguyên nhân không được để trống");
                        //updateModel.FinishDate = form.FinishDate;
                        updateModel.StartDate = form.StartDate;
                        if (status == (byte)MyUtilities.Machine.State.RepairStatus.Delete && string.IsNullOrWhiteSpace(updateModel.Note)) {
                            throw new AggregateException("Hủy phiếu vui lòng ghi lý do vào ghi chú !");
                        }
                        updateModel.Status = status;
                        var machineLog = new MachineLog {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            DateLog = DateTime.Now,
                            MachineId = machine.MachineId,
                            Type = (byte)MachineLogTypeEnum.State,
                            Note = updateModel.GetUpdateStateLog(),
                        };
                        vfi.MachineLogs.Add(machineLog);
                        machine.ErrorCause = "";
                        machine.HowToFix = "";
                        if (form.Status == 2)
                            machine.StateId = 1;
                    }
                    form.Note = updateModel.Note;
                    form.ErrorCause = updateModel.ErrorCause;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMachineErrorState", ex.Message);
            }
            return View(new GridModel(GetMachineErrorStateList(0, 0, "", "", "", 1, 0)));
        }

        [GridAction]
        public ActionResult UpdateMachineErrorState2(MachineRepairFormModel updateModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var form = vfi.MachineRepairForms.FirstOrDefault(mr => mr.FormId == updateModel.FormId);
                    if (form == null)
                        throw new AggregateException("Lỗi ! Không tìm thấy phiếu");
                    var errorId = form.ErrorCauseId;
                    if (!string.IsNullOrWhiteSpace(updateModel.ErrorCause)) {
                        try {
                            errorId = Convert.ToInt32(updateModel.ErrorCause);
                        }
                        catch (FormatException) {
                            errorId = form.ErrorCauseId;
                        }
                    }
                    form.Note = updateModel.Note;
                    form.ErrorCauseId = errorId;
                    form.ErrorCause = updateModel.ErrorCause;
                    form.ErrorQuantity = updateModel.ErrorQuantity;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMachineErrorState", ex.Message);
            }
            return View(new GridModel(GetMachineErrorStateList(0, 0, "", "", "", 1, 0)));
        }

        [GridAction]
        public ActionResult DeleteMachineErrorState(MachineRepairFormModel updateModel) {
            try {
                using (var vfi = new vfiContext()) {
                    var form = vfi.MachineRepairForms.FirstOrDefault(mr => mr.FormId == updateModel.FormId);
                    form.Status = (byte)MyUtilities.Machine.State.RepairStatus.Delete;
                    foreach (var detail in form.RepairFormDetails) {
                        detail.Status = (byte)MyUtilities.Machine.State.RepairStatus.Delete;
                    }
                    form.FinishDate = DateTime.Now;
                    form.FinishUser = HttpContext.User.Identity.Name;
                    form.Machine.StateId = 1;
                    form.Machine.ModifiedState = null;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteMachineErrorState", ex.Message);
            }
            return View(new GridModel(GetMachineErrorStateList(0, 0, "", "", "", 1, 0)));
        }


        [GridAction]
        public ActionResult SelectMachine2ErrorState(int machineId, string fromDate, string toDate, int status) {
            var model = new List<MachineRepairFormModel>();
            try {
                model = GetMachine2ErrorStateList(machineId, fromDate, toDate, status);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMachine2ErrorState", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<MachineRepairFormModel> GetMachine2ErrorStateList(int machineId, string fromDate, string toDate, int status) {
            var model = new List<MachineRepairFormModel>();
            var ci = new CultureInfo("vi-VN");
            var from = string.IsNullOrWhiteSpace(fromDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(fromDate, ci);
            var to = string.IsNullOrWhiteSpace(toDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(toDate, ci);
            try {
                using (var vfi = new vfiContext()) {

                    var machineStates = vfi.MachineRepairForms.Where(ms => ms.SectionId != null).ToList();
                    if (machineId != 0)
                        machineStates = machineStates.Where(mr => mr.MachineId == machineId).ToList();
                    if (!string.IsNullOrWhiteSpace(toDate))
                        machineStates = machineStates.Where(mr => mr.CreateDate >= from && mr.CreateDate <= to).ToList();
                    if (status == 0) {
                        if (string.IsNullOrWhiteSpace(toDate))
                            machineStates =
                                machineStates.Where(
                                    ms =>
                                        ms.Status == (byte)MyUtilities.Machine.State.RepairStatus.None ||
                                        ms.Status == (byte)MyUtilities.Machine.State.RepairStatus.UnFinish ||
                                        ms.Status == (byte)MyUtilities.Machine.State.RepairStatus.Stop).ToList();
                        else
                            machineStates =
                                machineStates.Where(ms => ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete).ToList();
                    }
                    else if (status != -1)
                        machineStates = machineStates.Where(mr => mr.Status == status).ToList();

                    var exchangeRate = MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.ExchangeToVndRate);
                    foreach (var machineState in machineStates) {
                        var entity = new MachineRepairFormModel {
                            FormId = machineState.FormId,
                            MachineId = machineState.MachineId,
                            MachineName2 = machineState.Machine.MachineName,
                            ProductId = machineState.ProductId,
                            ProductCode2 = machineState.Product.ProductCode,
                            StateId = machineState.StateId,
                            StateName = machineState.MachineState.Description,
                            ErrorCause = machineState.ErrorCause,
                            CreateDate = machineState.CreateDate,
                            CreateUser = machineState.CreateUser,
                            Status = machineState.Status,
                            StatusName = MyUtilities.Machine.State.GetRepairStatusText(machineState.Status),
                            FinishDate = machineState.FinishDate,
                            FinishUser = machineState.FinishUser,
                            StartDate = machineState.StartDate,
                            StartUser = machineState.StartUser,
                            Note = machineState.Note,
                            Shift = machineState.Shift,
                            ToDate = to,
                            MoreTime = machineState.MoreTime,
                        };
                        if (machineState.FixId != null) {
                            entity.FixId = machineState.FixId.Value;
                            entity.HowToFix = machineState.MachineStateDetail.Description;
                            entity.Timing = machineState.MachineStateDetail.Timing;
                        }
                        if (machineState.EmployeeId != null) {
                            entity.EmployeeId = machineState.EmployeeId.Value;
                            entity.EmployeeName2 = machineState.Employee.EmployeeName;
                        }
                        if (machineState.SectionId != null) {
                            entity.SectionId = machineState.SectionId.Value;
                            entity.SectionName = machineState.ProductionSection.SectionIndex + "." +
                                                 machineState.ProductionSection.Section.SectionName;
                        }
                        if (!string.IsNullOrWhiteSpace(toDate)) {

                            var trackUpProducion = vfi.TrackUpMachines.Where(
                                tp =>
                                tp.DeliveryDate != null && tp.DeliveryDate <= entity.CreateDate &&
                                tp.MachineId == entity.MachineId && tp.ProductId == entity.ProductId &&
                                tp.Status != (byte)MyUtilities.Transaction.Status.Cancel)
                                                      .OrderByDescending(tp => tp.DeliveryDate)
                                                      .ThenByDescending(tp => tp.ModifiedDate).FirstOrDefault();
                            if (trackUpProducion != null) {
                                entity.Productivity = trackUpProducion.RealProductivity;
                                entity.ProductPrice = MyUtilities.Product.ProductVndPrice(trackUpProducion.Product.UnitPrice,1,exchangeRate);
                                //if (trackUpProducion.Product.UnitPrice != null ||
                                //    trackUpProducion.Product.UnitPrice != 0) {
                                //    var productPrice = Math.Round(trackUpProducion.Product.UnitPrice.Value, 4);
                                //    var temp = Convert.ToInt32(productPrice);
                                //    if (productPrice - temp != 0)
                                //        productPrice = Math.Round(productPrice * MyUtilities.Product.ExchangeRateDesign, 0);
                                //    entity.ProductPrice = productPrice;
                                //}
                            }
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw new AggregateException(ex.Message);
            }
            return model.OrderBy(m => m.EmployeeName).ThenBy(m => m.StartDate).ThenBy(m => m.MachineName).ToList();
        }

        [GridAction]
        public ActionResult InsertMachine2ErrorState(MachineRepairFormModel newModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var machineId = 1;
                    try {
                        machineId = Convert.ToInt32(newModel.MachineName2);
                    }
                    catch (FormatException) {
                        machineId = newModel.MachineId;
                    }
                    var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == machineId);
                    if (machine == null)
                        throw new AggregateException("Lỗi ! Không tìm thấy máy");
                    var machineState =
                        vfi.MachineRepairForms.FirstOrDefault(mrf => mrf.MachineId == machineId && mrf.Status == (byte)MyUtilities.Machine.State.RepairStatus.None);
                    if (machineState != null)
                        throw new AggregateException("Lỗi! Máy " + machine.MachineName +
                                                     " đang trong tình trạng chưa xong! Hãy hoàn thành phiếu trước đó.");
                    //machine.ModifiedState = DateTime.Now;
                    var stateId = 1;
                    try {
                        stateId = Convert.ToInt32(newModel.StateName);
                    }
                    catch (FormatException) {
                        stateId = newModel.StateId;
                    }
                    var productId = 1;
                    try {
                        productId = Convert.ToInt32(newModel.ProductCode2);
                    }
                    catch (FormatException) {
                        productId = newModel.ProductId;
                    }
                    if (productId == 0)
                        throw new AggregateException("Lỗi! Vui lòng chọn sản phẩm.");
                    var sectionId = 1;
                    try {
                        sectionId = Convert.ToInt32(newModel.SectionName);
                    }
                    catch (FormatException) {
                        sectionId = newModel.SectionId;
                    }
                    var produtionSection =
                        vfi.ProductionSections.FirstOrDefault(
                            ps => ps.ProductionSectionId == sectionId && ps.ProductId == productId);
                    if (produtionSection == null)
                        throw new AggregateException("Lỗi! Chọn lại công đoạn");


                    machine.StateId = stateId;
                    machine.ErrorCause = newModel.ErrorCause;
                    machineState = new MachineRepairForm {
                        CreateUser = HttpContext.User.Identity.Name,
                        CreateDate = DateTime.Now,
                        CauseDate = DateTime.Now,
                        Status = 1,
                        MachineId = machineId,
                        ErrorCause = newModel.ErrorCause,
                        StateId = stateId,
                        Note = newModel.Note,
                        Shift = 0,
                        MoreTime = 0,
                        SectionId = sectionId,
                        ProductId = productId,
                    };
                    vfi.MachineRepairForms.Add(machineState);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMachine2ErrorState", ex.Message);
            }
            return View(new GridModel(GetMachine2ErrorStateList(0, "", "", 1)));
        }

        [GridAction]
        public ActionResult UpdateMachine2ErrorState(MachineRepairFormModel updateModel) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var machine = vfi.Machines.FirstOrDefault(m => m.MachineId == updateModel.MachineId);
                    if (machine == null)
                        throw new AggregateException("Lỗi ! Không tìm thấy máy này");
                    if (string.IsNullOrWhiteSpace(updateModel.HowToFix))
                        throw new AggregateException("Lỗi ! Khắc phục để trống");
                    var form = vfi.MachineRepairForms.FirstOrDefault(mr => mr.FormId == updateModel.FormId);

                    if (form.FixId == null) {
                        if (updateModel.StartDate == null) {
                            throw new AggregateException("Lỗi! Vui lòng chọn ngày giờ kết thúc hoặc F5 làm lại.");
                        }
                        form.StartDate = updateModel.StartDate;
                        form.StartUser = HttpContext.User.Identity.Name;
                        var employeeId = 0;
                        try {
                            employeeId = Convert.ToInt32(updateModel.EmployeeName2);
                        }
                        catch (FormatException) {
                            employeeId = updateModel.EmployeeId;
                        }
                        var fixId = 0;
                        try {
                            fixId = Convert.ToInt32(updateModel.HowToFix);
                        }
                        catch (FormatException) {
                            fixId = updateModel.FixId;
                        }
                        if (employeeId == 0)
                            throw new AggregateException("Lỗi! Chọn lại nhân viên.");
                        if (fixId == 0)
                            throw new AggregateException("Lỗi! Chọn lại cách khắc phục.");
                        form.EmployeeId = employeeId;
                        form.FixId = fixId;
                        form.Shift = updateModel.Shift;
                    }
                    else {

                        byte status = 0;
                        try {
                            status = Convert.ToByte(updateModel.StatusName);
                        }
                        catch (FormatException) {

                        }
                        if (status == 0)
                            throw new AggregateException("Lỗi! Vui lòng chọn đạt hoặc không đạt!");
                        form.Status = status;
                        if (updateModel.FinishDate == null) {
                            throw new AggregateException("Lỗi! Vui lòng chọn ngày giờ kết thúc hoặc F5 làm lại.");
                        }
                        form.FinishDate = updateModel.FinishDate;
                        form.FinishUser = HttpContext.User.Identity.Name;
                        form.MoreTime = updateModel.MoreTime;
                        if (form.FinishDate < form.StartDate)
                            throw new AggregateException("Lỗi! Thời gian hoàn thành nhỏ hoặc bằng hơn thời gian bắt đầu!Vui lòng chọn lại");
                        if (string.IsNullOrWhiteSpace(updateModel.ErrorCause) && string.IsNullOrWhiteSpace(form.ErrorCause))
                            throw new AggregateException("Lỗi! Nguyên nhân không được để trống");
                        //updateModel.FinishDate = form.FinishDate;
                        updateModel.StartDate = form.StartDate;
                        updateModel.Status = status;
                        var machineLog = new MachineLog {
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            DateLog = DateTime.Now,
                            MachineId = machine.MachineId,
                            Type = (byte)MachineLogTypeEnum.State,
                            Note = updateModel.GetUpdateStateLog(),
                        };
                        vfi.MachineLogs.Add(machineLog);
                        machine.ErrorCause = "";
                        machine.HowToFix = "";
                        if (form.Status == 2)
                            machine.StateId = 1;
                    }
                    form.Note = updateModel.Note;
                    form.ErrorCause = updateModel.ErrorCause;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateMachine2ErrorState", ex.Message);
            }
            return View(new GridModel(GetMachine2ErrorStateList(0, "", "", 1)));
        }


        [GridAction]
        public ActionResult SelectErrorMachineList() {
            var model = new List<MachineRepairFormModel>();
            try {
                model = GetErrorMachineList("", 0);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectErrorMachineList", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        List<MachineRepairFormModel> GetErrorMachineList(string machineName, int reportType) {
            var errorList = GetMachineErrorStateList(0, 0, machineName, "", "", 0, reportType);
            var statisStateList = MyUtilities.Machine.State.GetStaticStateList();
            errorList = errorList.Where(e => !statisStateList.Contains(e.StateId)).ToList();
            return errorList;
        }


        public ActionResult PrintErrorMachineList(string machineName, bool isState) {
            var model = new List<MachineRepairFormModel>();
            try {
                var reportType = 0;
                if (isState)
                    reportType = 2;
                model = GetErrorMachineList(machineName, reportType);
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintErrorMachineList", ex.Message);
            }
            return PartialView("PageErrorMachineList", model);
        }

        public ActionResult PrintJobAssignment(string fromDate, string toDate) {
            //var model = new List<MachineRepairFormModelByEmployee>();
            var groupModel = new List<JobAssignmentGroup>();
            try {
                var model = GetJobAssignment(fromDate, toDate);
                var groups = model.Select(m => m.GroupName).Distinct();
                foreach (var group in groups) {
                    var details = model.Where(m => m.GroupName.Equals(group)).ToList();
                    var entity = new JobAssignmentGroup {
                        GroupName = group,
                        Details = details,
                        ReportDate = toDate,
                    };
                    groupModel.Add(entity);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintJobAssignment", ex.Message);
            }
            return PartialView("PageJobAssignment", groupModel);
        }

        List<MachineRepairFormModelByEmployee> GetJobAssignment(string fDate, string tDate) {

            var model = new List<MachineRepairFormModelByEmployee>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fromDate = string.IsNullOrWhiteSpace(fDate)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(fDate, ci);
                var toDate = string.IsNullOrWhiteSpace(tDate)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(tDate, ci);
                toDate = toDate.AddDays(1).AddSeconds(-1);
                //var fromDate = new DateTime(pDate.Year, pDate.Month, pDate.Day, MyUtilities.Product.StartShift1_HOUR, 0, 0);
                //var toDate = new DateTime(pDate.Year, pDate.Month, pDate.Day, MyUtilities.Product.EndShift_HOUR, 0, 0);

                using (var vfi = new vfiContext()) {

                    var repairMachines = (from ms in vfi.RepairFormDetails
                                          where ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                         (
                                             (ms.StartDate >= fromDate && ms.StartDate <= toDate) ||
                                             (ms.StartDate <= toDate && ms.FinishDate == null) ||
                                             (ms.FinishDate >= fromDate && ms.FinishDate <= toDate) ||
                                             (ms.StartDate < fromDate && ms.FinishDate > toDate)
                                        )
                                          select new {
                                              ms.MachineRepairForm,
                                              ms.EmployeeId,
                                              ms.Status,
                                              StartDate = ms.StartDate < fromDate ? fromDate : ms.StartDate,
                                              StartRepair = ms.StartDate,
                                              FinishDate = (ms.FinishDate == null || ms.FinishDate > toDate)
                                              ? toDate
                                              : ms.FinishDate.Value,
                                              FinishRepair = ms.FinishDate
                                          }).ToList();

                    var employees = from m in vfi.Employees
                                    where m.Repair == true
                                    orderby m.EmployeeCode, m.EmployeeName
                                    select new {
                                        m.EmployeeId,
                                        m.EmployeeName,
                                        m.EmployeeCode,
                                        EmployeeCodeName = m.EmployeeCode + "." + m.EmployeeName,
                                        m.GroupName,
                                    };
                    foreach (var employee in employees) {
                        var entity = new MachineRepairFormModelByEmployee {
                            EmployeeId = employee.EmployeeId,
                            EmployeeName = employee.EmployeeCodeName,
                            Details = new List<MachineRepairFormModel>(),
                            GroupName = employee.GroupName + "",
                        };
                        var repairMachinesById = repairMachines.Where(rm => rm.EmployeeId == entity.EmployeeId);
                        var details = new List<MachineRepairFormModel>();
                        foreach (var repair in repairMachinesById) {
                            var detail = new MachineRepairFormModel() {
                                MachineName = repair.MachineRepairForm.Machine.MachineName,
                                EstimateTime = repair.MachineRepairForm.MachineState.EstimateTime,
                                StateCode = repair.MachineRepairForm.MachineState.StateCode,
                                StartDate = repair.StartRepair,
                                FinishDate = repair.FinishRepair,
                                StatusName = MyUtilities.Machine.State.GetRepairStatusText(repair.Status)
                            };
                            details.Add(detail);
                        }
                        entity.Details = details.OrderBy(d => d.StartDate).ThenBy(d => d.FinishDate).ToList();
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model;
        }

        [GridAction]
        public ActionResult SelectMachineStateStatistic(string fromDate, string toDate) {
            var model = new List<MachineStateStatisticModel>();
            try {
                model = GetMachineStateStatistic(fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMachineStateStatistic", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        public List<MachineStateStatisticModel> GetMachineStateStatistic(string fromDate, string toDate) {
            var model = new List<MachineStateStatisticModel>();
            var ci = new CultureInfo("vi-VN");
            var fDate = string.IsNullOrWhiteSpace(fromDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(fromDate, ci);
            var tDate = string.IsNullOrWhiteSpace(toDate)
                                  ? DateTime.Today
                                  : Convert.ToDateTime(toDate, ci).AddDays(1).AddSeconds(-1);

            using (var vfi = new vfiContext()) {

                var repairMachines = (from ms in vfi.RepairFormDetails
                                      where ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                     (
                                         (ms.StartDate >= fDate && ms.StartDate <= tDate) ||
                                         (ms.StartDate <= fDate && ms.FinishDate == null) ||
                                         (ms.FinishDate >= fDate && ms.FinishDate <= tDate) ||
                                         (ms.StartDate < fDate && ms.FinishDate > tDate)
                                    )
                                      select new {
                                          ms.MachineRepairForm,
                                          ms.Employee,
                                          ms.EmployeeId,
                                          //ms.
                                          ms.Status,
                                          StartDate = ms.StartDate < fDate ? fDate : ms.StartDate,
                                          StartRepair = ms.StartDate,
                                          FinishDate = (ms.FinishDate == null || ms.FinishDate > tDate)
                                          ? tDate
                                          : ms.FinishDate.Value,
                                          FinishRepair = ms.FinishDate,
                                          HowToFix = ms.FixId > 0 
                                               ?   ms.MachineStateDetail.Description 
                                               : "" ,
                                       ErrorCause = ms.MachineRepairForm.ErrorCauseId  > 0 
                                               ?   ms.MachineRepairForm.ErrorCauseForm.Name 
                                               : ""
                                      }).ToList();
                var dayCount = MyUtilities.Function.DaysNoSunDay(fDate, tDate);
                var maxRunTime = MyUtilities.Function.RoundDown(dayCount
                                                * MyUtilities.Monitor.GetParameterValue(MyUtilities.Monitor.FactoryFullDayTiming)
                                                / 60);
                foreach (var repair in repairMachines) {
                    var entity = new MachineStateStatisticModel() {
                        ErrorCause = repair.ErrorCause,
                        HowToFix = repair.HowToFix,
                        MachineName = repair.MachineRepairForm.Machine.MachineName,
                        ProductCode = repair.MachineRepairForm.Product.ProductCode,
                        EstimateTime = repair.MachineRepairForm.MachineState.EstimateTime,
                        StateCode =
                            repair.MachineRepairForm.MachineState.StateCode + "." +
                            repair.MachineRepairForm.MachineState.Description,
                        StartDate = repair.StartRepair,
                        FinishDate = repair.FinishRepair,
                        StatusName = MyUtilities.Machine.State.GetRepairStatusText(repair.Status),
                        Note = repair.MachineRepairForm.Note,
                        CauseDate = repair.MachineRepairForm.CauseDate
                    };
                    var qcEmployee = repair.MachineRepairForm.Employee1;
                    if (qcEmployee != null) {
                        entity.EmployeeQcName = qcEmployee.EmployeeCode + "." + qcEmployee.EmployeeName;
                    }
                    var repairEmployee = repair.Employee;
                    if (repairEmployee != null) {
                        entity.EmployeeRepairName = repairEmployee.EmployeeCode + "." +
                            repairEmployee.EmployeeName + "-" +
                            repairEmployee.GroupName;
                    }
                    entity.FixTimeHhMm = (entity.FixTime / 60) + ":" + string.Format("{0:00}", (entity.FixTime % 60));
                    entity.RunTime = maxRunTime - entity.FixTime;
                    model.Add(entity);
                }

                var trackingMachines = (from ms in vfi.TrackingRepairEmployees
                                        where ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                       (
                                           (ms.StartDate >= fDate && ms.StartDate <= tDate) ||
                                           (ms.StartDate <= fDate && ms.FinishDate == null) ||
                                           (ms.FinishDate >= fDate && ms.FinishDate <= tDate) ||
                                           (ms.StartDate < fDate && ms.FinishDate > tDate)
                                      )
                                        select new {
                                            ms.Machine.MachineName,
                                            RepairEmployee = ms.Employee.EmployeeCode + "." +
                                                               ms.Employee.EmployeeName + "-" +
                                                               ms.Employee.GroupName,
                                            QcEmployee = ms.Employee1.EmployeeCode + "." + ms.Employee1.EmployeeName,
                                            ms.Status,
                                            ms.StartDate,
                                            ms.FinishDate,
                                            ms.Note
                                        }).ToList();

                foreach (var tracking in trackingMachines) {
                    var entity = new MachineStateStatisticModel() {
                        MachineName = tracking.MachineName,
                        ProductCode = "",
                        EstimateTime = 0,
                        StateCode = "Theo dõi",
                        StartDate = tracking.StartDate,
                        FinishDate = tracking.FinishDate,
                        StatusName = MyUtilities.Machine.State.GetRepairStatusText(tracking.Status),
                        Note = tracking.Note,
                        EmployeeQcName = tracking.QcEmployee,
                        EmployeeRepairName = tracking.RepairEmployee,
                    };
                    entity.FixTimeHhMm = (entity.FixTime / 60) + ":" + string.Format("{0:00}", (entity.FixTime % 60));
                    entity.RunTime = maxRunTime - entity.FixTime;
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.EmployeeRepairName).ThenBy(m => m.StartDate).ToList();
        }

        public ActionResult PrintTechnicalHandover(string machineType, string printDate) {
            var model = new List<MachineStateHandoverModel>();
            try {
                model = GetMachineStateHandover(machineType, -1, printDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintTechnicalHandover", ex.Message);
            }
            return PartialView("PageTechnicalHandover", model);
        }

        [GridAction]
        public ActionResult SelectMachineStateHandover(int shift, string printDate) {
            if (string.IsNullOrWhiteSpace(printDate))
                return View(new GridModel(new List<MachineStateHandoverModel>()));

            var model = new List<MachineStateHandoverModel>();
            try {
                model = GetMachineStateHandover("", shift, printDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMachineStateHandover", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        public List<MachineStateHandoverModel> GetMachineStateHandover(string machineType, int shift, string printDate) {
            var model = new List<MachineStateHandoverModel>();
            try {
                var pDate = MyUtilities.Function.ParseDate(printDate);
                var shiftDate = MyUtilities.Product.GetShiftRangeDate(shift, pDate);
                using (var vfi = new vfiContext()) {
                    var errorMachineState = from ms in vfi.MachineRepairForms
                                            where ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                            ms.CauseDate >= shiftDate.FromDate && ms.CauseDate <= shiftDate.ToDate
                                            select ms;
                    var repairForms = (from ms in vfi.MachineRepairForms
                                       where ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                         (
                                          (ms.CauseDate >= shiftDate.FromDate && ms.CauseDate <= shiftDate.ToDate) ||
                                           (ms.CauseDate <= shiftDate.ToDate && ms.FinishDate == null) ||
                                           (ms.FinishDate >= shiftDate.FromDate && ms.FinishDate <= shiftDate.ToDate) ||
                                           (ms.CauseDate < shiftDate.FromDate && ms.FinishDate > shiftDate.ToDate)
                                          )
                                       select new {
                                           ms.MachineId,
                                           StartDate = (ms.CauseDate < shiftDate.FromDate ? shiftDate.FromDate : ms.CauseDate),
                                           FinishDate = (ms.FinishDate == null || ms.FinishDate > shiftDate.ToDate)
                                           ? shiftDate.ToDate
                                           : ms.FinishDate.Value,
                                           StartState = ms.CauseDate,
                                           FinishState = ms.FinishDate,
                                           ms.FormId,

                                           ms.MachineState.StateId,
                                           ms.MachineState.StateCode,
                                           ms.MachineState.EstimateTime,
                                           StateName = ms.MachineState.Description,
                                       }).ToList();
                    var machines = vfi.Machines.Where(m => m.Active && !m.MachineName.Contains("VF2")).ToList();

                    switch (machineType) {
                        case "Cames":
                            machines = machines.Where(m => m.MachineName.StartsWith("C") && !m.MachineName.StartsWith("CNC")).ToList();
                            break;
                        case "CNC":
                            machines = machines.Where(m => m.MachineName.StartsWith("CNC")).ToList();
                            break;
                        case "Phay CNC":
                            machines = machines.Where(m => m.MachineName.StartsWith("P")).ToList();
                            break;
                        default:
                            break;
                    }
                    var statisStateList = MyUtilities.Machine.State.GetStaticStateList();
                    var productions = (from id in vfi.ImportFormSX1Detail
                                       where
                                           id.ImportFormSX1.MaterialUseDate.Day == shiftDate.FromDate.Day &&
                                           id.ImportFormSX1.MaterialUseDate.Month == shiftDate.FromDate.Month &&
                                           id.ImportFormSX1.MaterialUseDate.Year == shiftDate.FromDate.Year &&
                                           //id.ImportFormSX1.MaterialUseDate <= shiftDate.ToDate &&
                                           //id.ImportFormSX1.MaterialUseDate <= shiftDate.ToDate &&
                                           //shift == 1 ? id.Shift1 != "" :  id.Shift2 != "" &&
                                             id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                             id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                             id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                               .Transaction.Status ==
                                             (byte)MyUtilities.Transaction.Status.Approved
                                       select new {
                                           id.MachineId,
                                           //id.MaterialInventory.MaterialId,
                                           //id.ImportFormSX1.MaterialUseDate,
                                           MaterialUse = shift == 1 ? (id.MaterialUse1) : (id.MaterialUse2),
                                           Production = shift == 1 ? (id.Number1) : (id.Number2),
                                       }).GroupBy(x => new {
                                           MachineId = x.MachineId.Value,
                                           //MaterialId = x.MaterialId
                                       })
                                       .Select(x => new {
                                           x.Key.MachineId,
                                           //x.Key.MaterialId,
                                           MaterialUse = x.Sum(y => y.MaterialUse),
                                           Production = x.Sum(y => y.Production),
                                       }).ToList();

                    foreach (var machine in machines) {
                        var repair = repairForms.Where(ms => ms.MachineId == machine.MachineId).
                            OrderByDescending(ms => ms.FinishDate).FirstOrDefault();
                        var countErrorState = errorMachineState.Where(e => e.MachineId == machine.MachineId).Count();
                        var entity = new MachineStateHandoverModel();
                        if (repair != null) {
                            entity = new MachineStateHandoverModel() {
                                FormId = repair.FormId,
                                MachineId = machine.MachineId,
                                MachineName = machine.MachineName,
                                ReportDate = pDate,
                                StateId = 1,
                                CountErrorState = countErrorState,
                                StartState = repair.StartState,
                                FinishState = repair.FinishState,
                                StartDate = repair.StartDate,
                                FinishDate = repair.FinishDate,
                                EstimateTime = repair.EstimateTime,
                                StateName = repair.StateName,
                                StateCode = repair.StateCode,
                            };
                            if (machine.StateId != null) {
                                entity.StateId = machine.StateId.Value;
                                entity.EstimateTime = machine.MachineState.EstimateTime;
                                entity.StateCode = machine.MachineState.StateCode;
                                entity.StateName = machine.MachineState.Description;
                            }
                            if (!statisStateList.Contains(entity.StateId))
                                entity.StateId = 0;
                            else if (entity.StateId == MyUtilities.Machine.State.BadState)
                                entity.StateId = 0;

                            entity.ErrorTime = (repair.FinishDate - repair.StartDate).TotalHours;
                            entity.RunningTime = 12 - entity.ErrorTime;

                        }
                        else {
                            entity = new MachineStateHandoverModel() {
                                FormId = 0,
                                MachineId = machine.MachineId,
                                MachineName = machine.MachineName,
                                ReportDate = pDate,
                                StateId = 1,
                                CountErrorState = countErrorState,
                                StartState = shiftDate.FromDate,
                                FinishState = shiftDate.ToDate,
                                StartDate = shiftDate.FromDate,
                                FinishDate = shiftDate.ToDate,
                                ErrorTime = 0,
                                RunningTime = 12,
                            };
                            if (machine.StateId != null) {
                                entity.StateId = machine.StateId.Value;
                                entity.StateName = machine.MachineState.Description;
                            }
                            if (!statisStateList.Contains(entity.StateId))
                                entity.StateId = 0;
                            else if (entity.StateId == MyUtilities.Machine.State.BadState)
                                entity.StateId = 0;
                        }
                        var lastTrack = MyUtilities.Machine.LastTrackUpProduct(machine.MachineId, "", 0, 0, shiftDate.ToDate);
                        //var lastTrack =
                        //        (from t in vfi.TrackUpMachines
                        //         where
                        //         t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                        //         t.MachineId == machine.MachineId &&
                        //         ((t.DeliveryDate != null ? t.DeliveryDate.Value <= shiftDate.ToDate : t.StartDate <= shiftDate.ToDate) ||
                        //          (t.StartDate <= shiftDate.ToDate))
                        //         select new {
                        //             t.MachineId,
                        //             t.ProductId,
                        //             t.Product.ProductCode,
                        //             t.Quantity,
                        //             t.RealProductivity,
                        //             t.RealRate,
                        //             t.TrackUpMaterials,
                        //             t.MaterialId,
                        //             t.Material,
                        //             t.WorkPiece,
                        //             t.KnifeCut,
                        //             Length = t.Product.Length ?? 0,
                        //             Date = t.DeliveryDate != null ? t.DeliveryDate.Value : t.StartDate,
                        //             t.Note,
                        //         })
                        //        .OrderByDescending(t => t.Date)
                        //        .FirstOrDefault();
                        if (lastTrack != null) {
                            entity.RealProductivity = lastTrack.RealProductivity;
                            //entity.RealRate = lastTrack.RealRate;
                            entity.RealRate = MyUtilities.Product.GetProductRate(3000, lastTrack.WorkPiece, lastTrack.ProductLength, lastTrack.KnifeCut); 	
                            entity.ProductId = lastTrack.ProductId;
                            entity.MaterialId = lastTrack.MaterialId.Value;
                            entity.ProductCode = lastTrack.ProductCode;
                            if (entity.MachineName.Contains("P")) {
                                entity.ProductionProductPlan = MyUtilities.Product.GetCncProductionRateInFactoryShiftTime(entity.RealProductivity, entity.RealRate);
                                entity.ProductionProductPlan = (entity.RunningTime / 12) * entity.ProductionProductPlan;
                                entity.ProductionMaterialPlan = 0;
                            }
                            else {
                                entity.ProductionProductPlan = MyUtilities.Product.GetProductionRateInFactoryShiftTime(entity.RealProductivity);
                                entity.ProductionProductPlan = (entity.RunningTime / 12) * entity.ProductionProductPlan;
                                entity.ProductionMaterialPlan = entity.ProductionProductPlan / entity.RealRate;
                            }
                            entity.Note = lastTrack.Note;

                        }
                        var productionsByMachine = productions.Where(x => x.MachineId == entity.MachineId).ToList();
                        if (productionsByMachine.Any()) {
                            entity.MaterialUse = productionsByMachine.Sum(x => x.MaterialUse);
                            entity.ProductionQuantity = productionsByMachine.Sum(x => x.Production);
                        }
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model;
        }

        public ActionResult PrintMachineStateHandover(int shift, string printDate) {
            var group = new List<GroupMachineStateHandoverModel>();
            var model = new List<MachineStateHandoverModel>();
            try {
                var pDate = MyUtilities.Function.ParseDate(printDate);
                var shiftDate = MyUtilities.Product.GetShiftRangeDate(shift, pDate);
                //var productions = 
                var list = GetMachineStateHandover("", shift, printDate);
                var machineIds = list.Select(m => m.MachineId).Distinct().ToList();
                foreach (var machineId in machineIds) {
                    var listById = list.Where(l => l.MachineId == machineId);
                    var first = listById.FirstOrDefault();
                    var entity = new MachineStateHandoverModel {
                        FormId = first.FormId,
                        MachineId = first.MachineId,
                        MachineName = first.MachineName,
                        ReportDate = first.ReportDate,
                        StateId = first.StateId,
                        CountErrorState = first.CountErrorState,
                        FinishState = first.FinishDate,
                        StartState = first.StartState,
                        StartDate = first.StartDate,
                        FinishDate = first.FinishDate,
                        ErrorTime = listById.Sum(l => l.ErrorTime),
                        ProductId = first.ProductId,
                        ProductCode = first.ProductCode,
                        ProductionProductPlan = first.ProductionProductPlan,
                        ProductionMaterialPlan = 0,
                        RealProductivity = first.RealProductivity,
                        RealRate = first.RealRate,
                        Note = first.Note,
                        MaterialUse = listById.Sum(l => l.MaterialUse),
                        ProductionQuantity = listById.Sum(l => l.ProductionQuantity),
                    };

                    if (entity.ErrorTime > 12)
                        entity.ErrorTime = 12;
                    else if (entity.ErrorTime < 0)
                        entity.ErrorTime = 0;
                    entity.RunningTime = 12 - entity.ErrorTime;
                    if (entity.MachineName.Contains("P")) {
                        entity.ProductionProductPlan = MyUtilities.Product.GetCncProductionRateInFactoryShiftTime(entity.RealProductivity, entity.RealRate);
                        entity.ProductionProductPlan = (entity.RunningTime / 12) * entity.ProductionProductPlan;
                        entity.ProductionMaterialPlan = 0;
                    }
                    else {
                        entity.ProductionProductPlan = MyUtilities.Product.GetProductionRateInFactoryShiftTime(entity.RealProductivity);
                        entity.ProductionProductPlan = (entity.RunningTime / 12) * entity.ProductionProductPlan;
                        entity.ProductionMaterialPlan = entity.ProductionProductPlan / entity.RealRate;
                    }
                    model.Add(entity);
                }
                model = model.OrderBy(m => m.MachineName).ToList();
                var cames = model.Where(m => m.MachineName.Contains("C") && !m.MachineName.Contains("CNC")).ToList();
                var cnc = model.Where(m => m.MachineName.Contains("CNC")).ToList();
                var phay = model.Where(m => m.MachineName.Contains("P")).ToList();

                group.Add(new GroupMachineStateHandoverModel() {
                    MachineName = "CAMES",
                    ReportDate = pDate,
                    Details = cames,
                });
                group.Add(new GroupMachineStateHandoverModel() {
                    MachineName = "CNC",
                    ReportDate = pDate,
                    Details = cnc,
                });
                group.Add(new GroupMachineStateHandoverModel() {
                    MachineName = "PHAY CNC",
                    ReportDate = pDate,
                    Details = phay,
                });

            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintMachineStateHandover", ex.Message);
            }
            return PartialView("PageMachineStateHandover", group);
        }

        [GridAction]
        public ActionResult DeleteMachine2ErrorState(MachineRepairFormModel updateModel) {
            try {
                using (var vfi = new vfiContext()) {
                    var form = vfi.MachineRepairForms.FirstOrDefault(mr => mr.FormId == updateModel.FormId);
                    form.Status = (byte)MyUtilities.Machine.State.RepairStatus.Delete;
                    form.FinishDate = DateTime.Now;
                    form.FinishUser = HttpContext.User.Identity.Name;
                    form.Machine.StateId = 1;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteMachine2ErrorState", ex.Message);
            }
            return View(new GridModel(GetMachine2ErrorStateList(0, "", "", 1)));
        }

        public ActionResult SelectComboBoxMachineRepairStatus() {
            var val = from MyUtilities.Machine.State.RepairStatus stt in Enum.GetValues(typeof(MyUtilities.Machine.State.RepairStatus))
                      select new {
                          Value = (int)Enum.Parse(typeof(MyUtilities.Machine.State.RepairStatus), stt.ToString()),
                          Text =
                      MyUtilities.Machine.State.GetRepairStatusText(
                          (int)Enum.Parse(typeof(MyUtilities.Machine.State.RepairStatus), stt.ToString()))
                      };

            return new JsonResult {
                Data = new SelectList(val, "Value", "Text")
            };
        }
        #endregion

        #region trackupmachine

        public ActionResult SelectComboBoxMaterialByProductId() {
            using (var vfi = new vfiContext()) {
                var productId = (int)Session["InserNewTrack"];
                var materialIds = vfi.ProductionMaterials.Where(pm => pm.ProductId == productId).Select(pm => pm.MaterialId).ToList();
                var materials = vfi.Materials.Where(
                    f => f.Active && materialIds.Contains(f.MaterialId))
                                   .ToList();
                return new JsonResult {
                    Data = new SelectList(materials, "MaterialId", "MaterialCode")
                };
            }
        }

        [HttpPost]
        public ActionResult GetProductionMaterial(int productId) {
            try {
                using (var vfi = new vfiContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null)
                        return Json("");
                    //if (!product.ProductionMaterials.Any())
                    //return Json("");
                    //Session["InserNewTrack"] = productId;
                    //return Json("1"); 
                    var materials =
                        vfi.ProductionMaterials.Where(pm => pm.ProductId == productId && pm.Active && pm.Material.Active)
                            .OrderBy(m => m.Material.MaterialName)
                            .ThenBy(m => m.Material.Shape)
                            .ThenBy(m => m.Material.DiameterType)
                            .ThenBy(m => m.Material.InDiameter)
                            .ThenBy(m => m.Material.OutDiameter)
                           .Select(pm => new { pm.MaterialId, pm.Material.MaterialCode })
                           .ToList();

                    //var materials = vfi.Materials.Where(f => f.Active)
                    //        .OrderBy(m => m.MaterialName)
                    //        .ThenBy(m => m.Shape)
                    //        .ThenBy(m => m.DiameterType)
                    //        .ThenBy(m => m.InDiameter)
                    //        .ThenBy(m => m.OutDiameter)
                    //        .Select(x => new { x.MaterialId, x.MaterialCode })
                    //        .ToList();
                    //var material = materials.Where(f => materialIds.Contains(f.MaterialId))
                    //                        .ToList().FirstOrDefault();
                    return new JsonResult {
                        Data = new SelectList(
                                materials,
                                "MaterialId", "MaterialCode",
                                materials.Any() ? materials.FirstOrDefault().MaterialId : -1)
                    };
                    //return Json(product.ProductionMaterials.ToList().LastOrDefault().Material);
                }
            }
            catch (Exception) {
                return Json("0");
            }
            //return Json("0");
        }

        [HttpPost]
        public ActionResult GetErrorCauseById(int stateId) {
            var model = new List<MachineStateDetailModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var stateDetails = vfi.ErrorCauseForms.Where(msd => msd.StateId == stateId);
                    foreach (var machineStateDetail in stateDetails) {
                        var detail = new MachineStateDetailModel {
                            DetailId = machineStateDetail.ErrorCauseId,
                            StateCode = machineStateDetail.StateCode + "." + machineStateDetail.Name
                        };
                        model.Add(detail);
                    }
                    //return Json(product.ProductionMaterials.ToList().LastOrDefault().Material);
                }
            }
            catch (Exception) {
                return Json("0");
            }
            return new JsonResult {
                Data = new SelectList(model.OrderBy(m => m.StateCode), "DetailId", "StateCode")
            };
            //return Json("0");
        }

        [HttpPost]
        public ActionResult GetMachineStateDetailById(int stateId) {
            var model = new List<MachineStateDetailModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var stateDetails = vfi.MachineStateDetails.Where(msd => msd.StateId == stateId);
                    foreach (var machineStateDetail in stateDetails) {
                        var detail = new MachineStateDetailModel {
                            DetailId = machineStateDetail.DetailId,
                            StateCode = machineStateDetail.StateCode + "." + machineStateDetail.Description
                        };
                        model.Add(detail);
                    }
                    //return Json(product.ProductionMaterials.ToList().LastOrDefault().Material);
                }
            }
            catch (Exception) {
                return Json("0");
            }
            return new JsonResult {
                Data = new SelectList(model.OrderBy(m => m.StateCode), "DetailId", "StateCode")
            };
            //return Json("0");
        }

        [HttpPost]
        public ActionResult GetTrackUpDesign(int productId) {
            try {
                using (var vfi = new vfiContext()) {
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null)
                        return Json("");
                    var entity = MyUtilities.Machine.LastTrackUpProduct(0, "", 0, productId, DateTime.Now);
                    if (entity == null) {
                        return Json(new MyUtilities.Monitor.MyJsonResult(
                            (int)MyUtilities.Monitor.ErrorCode.NotFound,
                            "Error",
                            null));
                    }
                    //var lastTracks =
                    //    vfi.TrackUpMachines.Where(
                    //        t => t.ProductId == productId && t.Status == (byte)MyUtilities.Transaction.Status.Approved)
                    //       .ToList();
                    //if (!lastTracks.Any())
                    //    return Json("9");
                    //var entity = new TrackUpMachineModel {
                    //    ProductId = productId,
                    //    RoundPerMinute = lastTracks.LastOrDefault().RoundPerMinute,
                    //    WorkPiece = lastTracks.LastOrDefault().WorkPiece,
                    //    RealProductivity = product.Productivity ?? 1,
                    //    RealRate = product.ProductionRate ?? 1,
                    //    KnifeCut = product.KnifeCut ?? 0,
                    //};
                    //if (entity.RealProductivity == 1)
                    //    entity.RealProductivity = lastTracks.LastOrDefault().RealProductivity;
                    //if (entity.RealRate == 1)
                    //    entity.RealRate = lastTracks.LastOrDefault().RealRate;
                    return Json(new MyUtilities.Monitor.MyJsonResult(
                        (int) MyUtilities.Monitor.ErrorCode.NoError,
                        "",
                        entity));
                    //return Json(product.ProductionMaterials.ToList().LastOrDefault().Material);
                }
            }
            catch (Exception) {
                return Json("0");
            }
            //return Json("0");
        }

        [GridAction]
        public ActionResult SelectRealTrackUpMachine() {
            var model = new List<TrackUpMachineModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var machines = (from m in vfi.Machines
                                   where m.Active 
                                        && m.ProcessingType.ForWarehouseId != null 
                                        && m.ProcessingType.Warehouse.IsProduction
                                   select new {
                                       m.MachineId,
                                       m.MachineName
                                   }).ToList();
                    var checkDate = DateTime.Now.AddMonths(-1);
                    foreach (var machine in machines) {
                        var lastTrack = MyUtilities.Machine.LastTrackUpMachine(machine.MachineId, null, null, DateTime.Now);
                        //var lastTrack =
                        //    vfi.TrackUpMachines.Where(
                        //        t =>
                        //        t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                        //        t.MachineId == machine.MachineId)
                        //       .OrderByDescending(t => t.DeliveryDate)
                        //       .FirstOrDefault();
                        if (lastTrack != null) {
                            if (lastTrack.DeliveryDate < checkDate) {
                                if (!lastTrack.TrackUpMaterials.Any()) continue;
                                if (!lastTrack.TrackUpMaterials.Any(tm => tm.ModifiedDate > checkDate.AddMonths(-2))) continue;
                            }
                            var entity = new TrackUpMachineModel {
                                TrackId = lastTrack.TrackId,
                                MachineName = machine.MachineName,
                                MachineId = machine.MachineId,
                                ProductId = lastTrack.ProductId,
                                MaterialId = lastTrack.MaterialId,
                                MaterialCode = lastTrack.MaterialCode,
                                ProductCode = lastTrack.ProductCode,
                                RealRate = lastTrack.RealRate,
                                RealProductivity = lastTrack.RealProductivity,
                                ProductLength = lastTrack.ProductLength,
                                WorkPiece = lastTrack.WorkPiece,
                                KnifeCut = lastTrack.KnifeCut,
                                Quantity = lastTrack.Quantity,
                            };
                            entity.RealRate = MyUtilities.Product.GetProductRate(3000, entity.WorkPiece, entity.ProductLength, entity.KnifeCut);
                            entity.MaxAssign =
                                //MyUtilities.Product.GetMaterialRateInTime(MyUtilities.Product.Second7_5h, entity.RealProductivity, entity.RealRate);
                            MyUtilities.Product.GetMaterialRateInFactoryDayTime(entity.RealProductivity, entity.RealRate);
                            //MyUtilities.Function.RoundUp(MyUtilities.Product.Second7_5h /
                            //                             entity.RealProductivity /
                            //                             entity.RealRate);
                            entity.MoreDay = lastTrack.TrackUpMaterials.Count;
                            model.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectRealTrackUpMachine", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        [GridAction]
        public ActionResult SelectAssignMaterials(string factory) {
            var model = new List<SmartProductionModel>();
            try {
                using (var vfi = new vfiContext()) {
                    var machines = (from m in vfi.Machines
                                   where m.Active
                                   orderby m.MachineName
                                   //&& m.MachineName.Equals("C16")
                                   select new {
                                       m.MachineId,
                                       m.MachineName
                                   }).ToList();
                    //if (!string.IsNullOrWhiteSpace(factory)) {
                    //    if (factory.Equals(MyUtilities.Machine.FactoryVF2)) {
                    //        machines = machines.Where(x => x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //    else {
                    //        machines = machines.Where(x => !x.MachineName.Contains(MyUtilities.Machine.FactoryVF2)).ToList();
                    //    }
                    //}
                    var now = DateTime.Now.AddDays(1);
                    var checkDate = now.AddMonths(-1);
                    var machineIds = machines.Select(x => x.MachineId).ToList();
                    var waitingUses = (from x in vfi.MaterialUseDetails
                                       where x.MaterialUseInShift.Status == (byte)MyUtilities.Transaction.Status.Open &&
                                       machineIds.Contains(x.MachineId)
                                       select new {
                                           x.MachineId,
                                           x.MaterialInvId,
                                           x.EditQuantity,
                                           x.EditQuantity2
                                       }).ToList();
                    foreach (var machine in machines) {
                        var lastTrack =
                            (from t in vfi.TrackUpMachines
                             where
                             t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                             t.MachineId == machine.MachineId &&
                             t.DeliveryDate != null && t.DeliveryDate.Value <= now &&
                             t.TrackUpMaterials.Any()
                             //t.MaterialId == entity.MaterialId
                             select new {
                                 t.MachineId,
                                 t.ProductId,
                                 t.Product,
                                 t.Product.ProductCode,
                                 t.RealProductivity,
                                 t.RealRate,
                                 t.TrackUpMaterials,
                                 t.MaterialId,
                                 t.Material,
                                 t.WorkPiece,
                                 t.KnifeCut,
                                 Length = t.Product.Length ?? 0,
                                 Date = t.DeliveryDate.Value
                             })
                            .OrderByDescending(t => t.Date)
                            .FirstOrDefault();
                        if (lastTrack != null) {
                            if (lastTrack.Date < checkDate) {
                                //if (!lastTrack.TrackUpMaterials.Any()) continue;
                                if (!lastTrack.TrackUpMaterials.Any(tm => tm.ModifiedDate > checkDate.AddMonths(-2))) continue;
                            }
                            var lastMaterialInv = lastTrack.TrackUpMaterials.ToList().LastOrDefault();
                            var entity = new SmartProductionModel {
                                MachineName = machine.MachineName,
                                MachineId = machine.MachineId,
                                ProductId = lastTrack.ProductId,
                                //MaterialCode = lastTrack.Material.MaterialCode,
                                ProductCode = lastTrack.Product.ProductCode,
                                //MaterialAlert = 2.5;
                                SmartId = lastMaterialInv.DetailId,
                                //IsLimit = true,
                                ProductionQuantity = 0,
                                MaterialUsedQuantity = 0,
                                MaterialLimitQuantity = 0,
                                ProductLimitQuantity = 0,
                            };
                            var realRate = MyUtilities.Product.GetProductRate(3000, entity.WorkPiece, lastTrack.Product.Length ?? 0, entity.KnifeCut);
                            entity.MaxAssign =  MyUtilities.Product.GetMaterialRateInFactoryShiftTime(lastTrack.RealProductivity, realRate);
                            entity.MaterialInventoryId = lastMaterialInv.MaterialInvId;
                            entity.MaterialId = lastMaterialInv.MaterialInventory.MaterialId;
                            entity.MaterialInventoryCode =
                                MyUtilities.Material.GetMaterialInvDesignNo(lastMaterialInv.MaterialInventory);
                            entity.MaterialInvTotal = lastMaterialInv.MaterialInventory.TotalQty;
                            var materialInvOnmachine =
                                vfi.MaterialInvOnMachines.FirstOrDefault(
                                    mim =>
                                    mim.MachineId == entity.MachineId && mim.MaterialInvId == entity.MaterialInventoryId);
                            if (materialInvOnmachine != null)
                                entity.MaterialInvOnMachine = materialInvOnmachine.TotalQuantity;

                            var waitingUse = waitingUses.Where(mud => mud.MachineId == entity.MachineId
                                                                    && mud.MaterialInvId == entity.MaterialInventoryId);
                            entity.DiffMaterial = entity.MaterialInvOnMachine;
                            if (waitingUse.Any()) {
                                entity.DiffMaterial = entity.MaterialInvOnMachine -
                                                      waitingUse.Sum(mud => mud.EditQuantity + mud.EditQuantity2);
                            }
                            //if (lastTrack.Product.CustomerId == MyUtilities.Product.DaiwaRing)
                            //    entity.IsLimit = false;
                            model.Add(entity);
                        }
                    }
                    // material planing checking
                    //var materialIds = model.Select(m => m.MaterialId).Distinct().ToList();
                    //var productIds = model.Select(m => m.ProductId).Distinct().ToList();
                    //var materialLimitPlans = (from mlp in vfi.MaterialLimitPlans
                    //                         where materialIds.Contains(mlp.MaterialId) &&
                    //                                productIds.Contains(mlp.ProductId) &&
                    //                                mlp.IsLock
                    //                         orderby mlp.ApplyDate
                    //                         select mlp).ToList();
                    //var materialInvs = (from mi in vfi.MaterialInventories
                    //                   where mi.TotalQty > 0 && materialIds.Contains(mi.MaterialId)
                    //                   select new {
                    //                       mi.MaterialId,
                    //                       mi.TotalQty,
                    //                       mi.UnitWeight
                    //                   }).ToList();
                    //var fromDate = new DateTime(DateTime.Now.Year, 1, 1);
                    //var oldestLimit = materialLimitPlans.OrderBy(o => o.ApplyDate).FirstOrDefault();
                    //if (oldestLimit != null)
                    //    fromDate = oldestLimit.ApplyDate;
                    //var sx1Details = (from id in vfi.ImportFormSX1Detail
                    //                  where
                    //                      id.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                    //                      id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                    //                      id.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                    //                        .Transaction.Status ==
                    //                      (byte)MyUtilities.Transaction.Status.Approved &&
                    //                      id.ImportFormSX1.MaterialUseDate >= fromDate &&
                    //                      materialIds.Contains(id.MaterialInventory.MaterialId) &&
                    //                      productIds.Contains(id.ProductId)
                    //                  select new {

                    //                      MaterialId = id.MaterialInventory.MaterialId,
                    //                      ProductId = id.ProductId,
                    //                      MaterialUse = (id.MaterialUse1) + (id.MaterialUse2),
                    //                      MaterialWeight = id.MaterialInventory.UnitWeight,
                    //                      Production = (id.Number1) + (id.Number2) +
                    //                                   (id.Processing1) + (id.Processing2) +
                    //                                   (id.DefectProduct1) + (id.DefectProduct2),
                    //                      id.ImportFormSX1.MaterialUseDate,
                    //                      id.ImportFormSX1.ImportDate,
                    //                  }).ToList();
                    //foreach (var limitPlan in materialLimitPlans) {
                    //    var entities = model.Where(m => m.ProductId == limitPlan.ProductId &&
                    //                                m.MaterialId == limitPlan.MaterialId);
                    //    var productions = sx1Details
                    //        .Where(sd => sd.MaterialId == limitPlan.MaterialId &&
                    //                        sd.ProductId == limitPlan.ProductId &&
                    //                        sd.ImportDate >= limitPlan.ApplyDate);
                    //    var materialInvById = materialInvs.Where(mi => mi.MaterialId == limitPlan.MaterialId)
                    //                                        .Sum(mi => mi.TotalQty * mi.UnitWeight);
                    //    foreach (var entity in entities) {
                    //        entity.TotalMaterialInv = materialInvById;
                    //        entity.MaterialLimitQuantity = limitPlan.MaterialLimitQuantity;
                    //        entity.ProductLimitQuantity = limitPlan.ProductLimitQuantity;
                    //        if (productions.Any()) {
                    //            entity.ProductionQuantity = productions.Sum(p => p.Production);
                    //            entity.MaterialUsedQuantity = productions.Sum(p => p.MaterialUse * p.MaterialWeight);
                    //        }
                    //        if (entity.ProductLimitQuantity > entity.ProductionQuantity ||
                    //            entity.MaterialLimitQuantity > entity.MaterialUsedQuantity)
                    //            entity.IsLimit = false;
                    //    }
                    //}
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectAssignMaterials", ex.Message);
            }
            return View(new GridModel(model.OrderBy(m => m.MachineName)));
        }

        [GridAction]
        public ActionResult SelectTrackUpMaterial(int trackId) {
            var model = new List<TrackUpMaterialModel>();
            try {
                model = GetTrackUpMaterial(trackId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTrackUpMaterial", ex.Message);

            }
            return View(new GridModel(model));
        }

        private List<TrackUpMaterialModel> GetTrackUpMaterial(int trackId) {
            var model = new List<TrackUpMaterialModel>();
            using (var vfi = new vfiContext()) {
                var track = vfi.TrackUpMachines.FirstOrDefault(t => t.TrackId == trackId);
                var trackMaterials = vfi.TrackUpMaterials.Where(tm => tm.TrackId == trackId);
                foreach (var trackUpMaterial in trackMaterials) {
                    var entity = new TrackUpMaterialModel {
                        TrackId = trackId,
                        ProductId = track.ProductId,
                        ProductCode = track.Product.ProductCode,
                        MaterialId = track.MaterialId.Value,
                        MaterialCode = track.Material.MaterialCode,
                        TrackProductRate = track.RealRate,

                        MaterialInvCode =
                            MyUtilities.Material.GetMaterialInvDesignNo(trackUpMaterial.MaterialInventory),
                        ModifiedDate = trackUpMaterial.ModifiedDate,
                        ModifiedUser = trackUpMaterial.ModifiedUser,
                        MaterialInvId = trackUpMaterial.MaterialInvId,
                        MaterialInv = trackUpMaterial.MaterialInventory.TotalQty,
                        Note = trackUpMaterial.Note
                    };
                    entity.ProductRate = MyUtilities.Product.GetProductRate(trackUpMaterial.MaterialInventory.Length,
                                                                            track.WorkPiece,
                                                                            track.Product.Length ?? 0,
                                                                            track.KnifeCut);
                    var materialInvOnMachine =
                        vfi.MaterialInvOnMachines.FirstOrDefault(
                            mim => mim.MachineId == track.MachineId && mim.MaterialInvId == entity.MaterialInvId);
                    if (materialInvOnMachine != null)
                        entity.MaterialInvOnMachine = materialInvOnMachine.TotalQuantity;

                    entity.MaxAssign =
                        //MyUtilities.Function.RoundUp(MyUtilities.Product.Second7_5h /
                        //                             track.RealProductivity /
                        //                             entity.ProductRate);
                    MyUtilities.Product.GetMaterialRateInFactoryDayTime(track.RealProductivity, entity.ProductRate);
                    model.Add(entity);
                }

            }
            return model;
        }

        [GridAction]
        public ActionResult InsertTrackUpMaterial(TrackUpMaterialModel insert, int trackId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    int materialInvId = 1;
                    try {
                        materialInvId = Convert.ToInt32(insert.MaterialInvCode);
                    }
                    catch (Exception) {
                    }
                    if (materialInvId == 1)
                        throw new AggregateException("Vui lòng chọn lại lô nguyên liệu");
                    var track = vfi.TrackUpMachines.FirstOrDefault(t => t.TrackId == trackId);
                    if (track == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu lên máy");
                    if ((track.Product.Length == 0 || track.Product.Length == null) &&
                        track.ProductId != MyUtilities.Product.RingId)
                        throw new AggregateException("Lỗi! Sản phẩm chưa có chiều dài! Vui lòng cập nhật trước!");
                    var entity = new TrackUpMaterial {
                        MaterialInvId = materialInvId,
                        TrackId = insert.TrackId,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        Note = insert.Note,
                    };
                    vfi.TrackUpMaterials.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertTrackUpMaterial", ex.Message);
            }
            return View(new GridModel(GetTrackUpMaterial(insert.TrackId)));
        }

        [GridAction]
        public ActionResult UpdateTrackUpMaterial(TrackUpMaterialModel update) {
            return View(new GridModel(GetTrackUpMaterial(update.TrackId)));
        }

        public ActionResult SelectComboBoxMaterialInvCodeByTrack(int trackId) {
            var model = new List<MaterialInventoryModel>();
            try {

                using (var vfi = new vfiContext()) {
                    var track = vfi.TrackUpMachines.FirstOrDefault(t => t.TrackId == trackId);
                    var materialInvs = from mi in vfi.MaterialInventories
                                       where Math.Round(mi.TotalQty) > 0 &&
                                             mi.MaterialId == track.MaterialId
                                       select mi;
                    foreach (var materialInv in materialInvs) {
                        var entity = new MaterialInventoryModel {
                            MaterialInventoryId = materialInv.MaterialInventoryId,
                            DesignNo = MyUtilities.Material.GetMaterialInvDesignNo(materialInv)
                        };
                        model.Add(entity);
                    }

                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectComboBoxMaterialInvCodeByTrack", ex.Message);
            }
            return new JsonResult {
                Data = new SelectList(model, "MaterialInventoryId", "DesignNo")
            };

        }

        [GridAction]
        public ActionResult SelectTrack(int month, int year, int status) {
            var model = new List<TrackUpMachineModel>();
            try {
                model = GetListTrackUpMachine(month, year, status);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTrack", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<TrackUpMachineModel> GetListTrackUpMachine(int month, int year, int status) {
            var model = new List<TrackUpMachineModel>();
            var deliveryDate = new DateTime(year, month, 1);
            using (var vfi = new vfiContext()) {
                var production = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                    MyUtilities.UserRole.ProductionManagement);
                var tracks = from tm in vfi.TrackUpMachines
                             where
                                 (tm.DeliveryDate == null
                                      ? (tm.StartDate.Month == deliveryDate.Month &&
                                         tm.StartDate.Year == deliveryDate.Year)
                                      : (tm.DeliveryDate.Value.Month == deliveryDate.Month &&
                                         tm.DeliveryDate.Value.Year == deliveryDate.Year)) &&
                                 (status == 1 || tm.Status == status)
                             select tm;
                //if (status != 1)
                //    tracks = tracks.Where(tm => tm.Status == status);
                foreach (var track in tracks) {
                    var entity = new TrackUpMachineModel {
                        TrackId = track.TrackId,
                        MachineId = track.MachineId,
                        MachineName = track.Machine.MachineName,
                        ProductId = track.ProductId,
                        ProductCode = track.Product.ProductCode,
                        MaterialId = track.MaterialId,
                        MaterialCode = track.Material == null ? "" : track.Material.MaterialCode,
                        Productivity = track.Product.Productivity ?? 0,
                        ProductRate = track.Product.ProductionRate ?? 0,
                        DeliveryDate = track.DeliveryDate,
                        StartDate = track.StartDate,
                        RealProductivity = track.RealProductivity,
                        RealRate = track.RealRate,
                        KnifeCut = track.KnifeCut,
                        DeliveryEmployee = track.DeliveryEmployee,
                        Phase = track.Phase,
                        ReceiveEmployee = track.ReceiveEmployee,
                        WorkPiece = track.WorkPiece,
                        RoundPerMinute = track.RoundPerMinute,
                        Note = track.Note,
                        Status = track.Status,
                        StatusColor = track.Status == 3 ? 2 : 1,
                        PermisstionType = production == true ? 2 : 1,
                        ModifiedDate = track.ModifiedDate,
                        ModifiedUser = track.ModifiedUser,
                        Quantity = track.Quantity,
                        ForecastDay = track.ForecastDay,
                        EndDate = track.EndDate,
                        ForecastDate = track.ForecastDate,
                        ProductImg = track.Product.DrawingFinish,
                        DrawingImg = track.Product.Drawing2D,
                        UploadDate = track.Product.UploadDate != null
                                            ? track.Product.UploadDate.Value.ToString("yyyyMMddhhmmss")
                                            : DateTime.Now.ToString("yyyyMMddhhmmss")
                    };
                    if (string.IsNullOrWhiteSpace(entity.ProductImg))
                        entity.ProductImg = "askquestion.jpg";
                    if (string.IsNullOrWhiteSpace(entity.DrawingImg))
                        entity.DrawingImg = "askquestion.jpg";
                    entity.Title = deliveryDate.ToString("MM/yyyy");
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.DeliveryDate)
                        .ThenBy(m => m.StartDate)
                        .ThenBy(m => m.MachineName)
                        .ThenBy(m => m.ProductCode)
                        .ThenBy(m => m.MaterialCode)
                        .ToList();
        }

        [GridAction]
        public ActionResult InsertTrack(TrackUpMachineModel newTrack, int month, int year, int status) {
            try {
                using (var vfi = new vfiContext()) {
                    var msg = "";
                    var productId = 0;
                    try {
                        productId = Convert.ToInt32(newTrack.ProductCode);
                    }
                    catch (FormatException) {
                        productId = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(newTrack.ProductCode))
                                       .ProductId;
                    }
                    var machineId = 0;
                    try {
                        machineId = Convert.ToInt32(newTrack.MachineName);
                    }
                    catch (FormatException) {
                        machineId = vfi.Machines.FirstOrDefault(p => p.MachineName.Equals(newTrack.MachineName))
                                       .MachineId;
                    }
                    var materialId = 0;
                    try {
                        materialId = Convert.ToInt32(newTrack.MaterialCode);
                    }
                    catch (FormatException) {
                        materialId = vfi.Materials.FirstOrDefault(p => p.MaterialCode.Equals(newTrack.MaterialCode))
                                       .MaterialId;
                    }
                    if (machineId == 0) {
                        msg += "Vui lòng chọn máy sản xuất \n";
                    }
                    if (productId == 0) {
                        msg += "Vui lòng chọn mã sản phẩm \n";
                    }
                    if (materialId == 0) {
                        msg += "Vui lòng chọn nguyên liệu \n";
                    }
                    if (newTrack.RoundPerMinute == 0 || newTrack.RealProductivity == 0 || 
                        // newTrack.RealRate == 0 ||
                        newTrack.WorkPiece == 0 || newTrack.KnifeCut == 0 || newTrack.Quantity == 0) {
                        msg += "Vui lòng điền đầy đủ thông tin RPM - NS - Phoi - Dao cắt - SL chạy \n";
                    }
                    if (!string.IsNullOrWhiteSpace(msg)) {
                        throw new AggregateException(msg);
                    }
                    var track = new TrackUpMachine {
                        ProductId = productId,
                        MachineId = machineId,
                        MaterialId = materialId,
                        //ProductId = newTrack.ProductId,
                        //MachineId = newTrack.MachineId,
                        //MaterialId = newTrack.MaterialId,
                        Status = (byte)MyUtilities.Transaction.Status.Approved,
                        DeliveryDate = newTrack.DeliveryDate,
                        StartDate = newTrack.StartDate.Value,
                        ForecastDay = newTrack.ForecastDay,
                        DeliveryEmployee = newTrack.DeliveryEmployee,
                        ReceiveEmployee = newTrack.ReceiveEmployee,
                        KnifeCut = newTrack.KnifeCut,
                        RealProductivity = newTrack.RealProductivity,
                        //RealRate = newTrack.RealRate,
                        RealRate = 0,
                        Phase = newTrack.Phase,
                        RoundPerMinute = newTrack.RoundPerMinute,
                        WorkPiece = newTrack.WorkPiece,
                        Note = newTrack.Note,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        Quantity = newTrack.Quantity,
                    };
                    var days =
                        MyUtilities.Function.RoundUp(
                            track.Quantity / MyUtilities.Product.GetProductionRateInFactoryDayTime(track.RealProductivity)) +
                        track.ForecastDay - 1;
                    track.ForecastDate = MyUtilities.Function.ToDate(track.StartDate, newTrack.ForecastDay);
                    track.EndDate = MyUtilities.Function.ToDate(track.StartDate, days);
                    var realProduction =
                        vfi.RealProductions.FirstOrDefault(
                            rp => rp.ProductId == track.ProductId && rp.MachineId == track.MachineId);
                    if (realProduction == null) {
                        realProduction = new RealProduction {
                            ProductId = track.ProductId,
                            MachineId = track.MachineId,
                            TrackUpMachine = track,
                        };
                        vfi.RealProductions.Add(realProduction);
                    }
                    else {
                        if (realProduction.TrackUpMachine.DeliveryDate <= track.DeliveryDate) {
                            realProduction.TrackUpMachine = track;
                        }
                        else if (realProduction.TrackUpMachine.StartDate <= track.StartDate)
                            realProduction.TrackUpMachine = track;
                    }
                    vfi.TrackUpMachines.Add(track);
                    vfi.SaveChanges();
                    newTrack.TrackId = track.TrackId;
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertTrack", ex.Message);
            }
            return View(new GridModel(GetListTrackUpMachine(month, year, status)));
        }

        [GridAction]
        public ActionResult UpdateTrack(TrackUpMachineModel updateTrack, int month, int year, int status) {
            try {
                using (var vfi = new vfiContext()) {

                    var track = vfi.TrackUpMachines.FirstOrDefault(tm => tm.TrackId == updateTrack.TrackId);
                    if (track == null) throw new AggregateException("Lỗi ! Không tìm thấy phiếu theo dõi !");

                    //var msg = "";
                    if (updateTrack.RoundPerMinute == 0 || updateTrack.RealProductivity == 0 || 
                        // updateTrack.RealRate == 0 ||
                        updateTrack.WorkPiece == 0 || updateTrack.KnifeCut == 0 || updateTrack.Quantity == 0) {
                        throw new AggregateException("Vui lòng điền đầy đủ thông tin RPM - NS - ĐM - Phoi - Dao cắt - SL chạy \n");
                    }

                    track.DeliveryDate = updateTrack.DeliveryDate;
                    track.DeliveryEmployee = updateTrack.DeliveryEmployee;
                    track.ReceiveEmployee = updateTrack.ReceiveEmployee;
                    track.KnifeCut = updateTrack.KnifeCut;
                    track.RealProductivity = updateTrack.RealProductivity;
                    //track.RealRate = updateTrack.RealRate;
                    track.RealRate = MyUtilities.Product.GetProductRate(3000, track.WorkPiece, track.Product.Length ?? 0, track.KnifeCut);
                    track.Phase = updateTrack.Phase;
                    track.RoundPerMinute = updateTrack.RoundPerMinute;
                    track.WorkPiece = updateTrack.WorkPiece;
                    track.Note = updateTrack.Note;
                    track.Quantity = updateTrack.Quantity;
                    track.ModifiedDate = DateTime.Now;
                    track.ModifiedUser = HttpContext.User.Identity.Name;
                    if (!track.Machine.MachineName.Contains("P")) {
                        var product = vfi.Products.FirstOrDefault(p => p.ProductId == track.ProductId);
                        //if (product.ProductionRate == null || product.ProductionRate == 0)
                        //    product.ProductionRate = Convert.ToInt32(track.RealRate);
                        if (product.MaterialId != null)
                            track.RealRate = MyUtilities.Product.GetProductRate(product.Material.Length ?? 0, track.WorkPiece, product.Length ?? 0, track.KnifeCut);
                        if (product.Productivity == null || product.Productivity == 0)
                            product.Productivity = track.RealProductivity;
                        if (product.KnifeCut == null || track.KnifeCut > product.KnifeCut)
                            product.KnifeCut = track.KnifeCut;
                    }

                    var realProduction =
                        vfi.RealProductions.FirstOrDefault(
                            rp => rp.ProductId == track.ProductId && rp.MachineId == track.MachineId);
                    if (realProduction == null) {
                        realProduction = new RealProduction {
                            ProductId = track.ProductId,
                            MachineId = track.MachineId,
                            TrackUpMachine = track,
                        };
                        vfi.RealProductions.Add(realProduction);
                    }
                    else {
                        if (realProduction.TrackUpMachine.DeliveryDate == null ||
                            realProduction.TrackUpMachine.DeliveryDate < track.DeliveryDate) {
                            realProduction.TrackUpMachine = track;
                        }
                        else if (realProduction.TrackUpMachine.DeliveryDate == track.DeliveryDate && realProduction.TrackUpMachine.ModifiedDate < track.ModifiedDate) {
                            realProduction.TrackUpMachine = track;
                        }
                    }
                    var days =
                        MyUtilities.Function.RoundUp(
                            track.Quantity / MyUtilities.Product.GetProductionRateInFactoryDayTime(track.RealProductivity)) - 1;
                    track.EndDate = MyUtilities.Function.ToDate(track.DeliveryDate.Value, days);
                    vfi.SaveChanges();


                    SaveTrackProcess(track.MachineId, track.ProductId, HttpContext.User.Identity.Name);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateTrack", ex.Message);
            }
            return View(new GridModel(GetListTrackUpMachine(month, year, status)));
        }

        public int SaveTrack(TrackUpMachineModel entity, string username) {
            var saved = 0;
            try {
                using(var vfi = new tammaContext()) {
                    TrackUpMachine track;
                    if (entity.TrackId > 0) {
                        track = vfi.TrackUpMachines.FirstOrDefault(tm => tm.TrackId == entity.TrackId);
                        if (track == null) throw new AggregateException("Lỗi ! Không tìm thấy phiếu theo dõi !");
                        track.DeliveryDate = entity.DeliveryDate;
                        track.DeliveryEmployee = entity.DeliveryEmployee;
                        track.ReceiveEmployee = entity.ReceiveEmployee;
                        track.KnifeCut = entity.KnifeCut;
                        track.RealProductivity = entity.RealProductivity;
                        track.RealRate = entity.RealRate;
                        track.Phase = entity.Phase;
                        track.RoundPerMinute = entity.RoundPerMinute;
                        track.WorkPiece = entity.WorkPiece;
                        track.Note = entity.Note;
                        track.Quantity = entity.Quantity;
                        track.ModifiedDate = DateTime.Now;
                        track.ModifiedUser = username;
                    }
                    else {
                        track = new TrackUpMachine {
                            ProductId = entity.ProductId,
                            MachineId = entity.MachineId,
                            MaterialId = entity.MaterialId,
                            Status = (byte)MyUtilities.Transaction.Status.Approved,
                            DeliveryDate = entity.DeliveryDate,
                            StartDate = entity.StartDate.Value,
                            ForecastDay = entity.ForecastDay,
                            DeliveryEmployee = (entity.DeliveryEmployee + "").Trim(),
                            ReceiveEmployee = (entity.ReceiveEmployee + "").Trim(),
                            KnifeCut = entity.KnifeCut,
                            RealProductivity = entity.RealProductivity,
                            RealRate = entity.RealRate,
                            Phase = entity.Phase,
                            RoundPerMinute = entity.RoundPerMinute,
                            WorkPiece = entity.WorkPiece,
                            Note = entity.Note,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = username,
                            Quantity = entity.Quantity,
                        };
                        vfi.TrackUpMachines.Add(track);
                    }
                    var days =
                        MyUtilities.Function.RoundUp(
                            track.Quantity / MyUtilities.Product.GetProductionRateInFactoryDayTime(track.RealProductivity)) - 1;
                    track.ForecastDay = days;
                    track.EndDate = MyUtilities.Function.ToDate(track.DeliveryDate.Value, days);
                    track.ForecastDate = MyUtilities.Function.ToDate(track.StartDate, days);
                    saved += vfi.SaveChanges();

                    var realProduction =
                        vfi.RealProductions.FirstOrDefault(
                            rp => rp.ProductId == track.ProductId && rp.MachineId == track.MachineId);
                    if (realProduction == null) {
                        realProduction = new RealProduction {
                            ProductId = track.ProductId,
                            MachineId = track.MachineId,
                            TrackUpMachine = track,
                        };
                        vfi.RealProductions.Add(realProduction);
                    }
                    else {
                        if (realProduction.TrackUpMachine.DeliveryDate == null ||
                            realProduction.TrackUpMachine.DeliveryDate < track.DeliveryDate) {
                            realProduction.TrackUpMachine = track;
                        }
                        else if (realProduction.TrackUpMachine.DeliveryDate == track.DeliveryDate && realProduction.TrackUpMachine.ModifiedDate < track.ModifiedDate) {
                            realProduction.TrackUpMachine = track;
                        }
                    }
                    saved += vfi.SaveChanges();

                    saved += SaveTrackProcess(track.MachineId, track.ProductId, username);


                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return saved;
        }

        public int SaveTrackProcess(int machineId, int productId, string username) {
            var saved = 0;
            using (var vfi = new tammaContext()) {

                var productionProcessByMachines =
                    vfi.ProductionProcessByMachines.Where(
                        ppm => ppm.Active &&
                               ppm.ProductId == productId &&
                               ppm.MachineId == machineId);
                if (productionProcessByMachines.Any()) {
                    //return View(new GridModel(GetListTrackUpMachine(month, year, status)));
                    // replace old production process: {change}
                    var productionProcesses =
                        vfi.ProductionProcesses.Where(pp => pp.ProductId == productId && pp.IsNecessary && pp.IsAlert);
                    foreach (var process in productionProcesses) {
                        var byMachine = productionProcessByMachines
                            .FirstOrDefault(ppm => ppm.WarehouseId == process.WarehouseId);
                        if (byMachine == null) {
                            byMachine = new ProductionProcessByMachine {
                                ProductId = productId,
                                MachineId = machineId,
                                WarehouseId = process.WarehouseId,
                                Active = true,
                                Note = "Auto",
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = username,
                                ProcessIndex = process.ProcessIndex,
                                UnitWeight = MyUtilities.Product.GetProductInvWeight(productId, process.WarehouseId)
                            };
                            vfi.ProductionProcessByMachines.Add(byMachine);
                        }
                        else {
                            byMachine.Active = true;
                            byMachine.Note = "Auto";
                            byMachine.ModifiedDate = DateTime.Now;
                            byMachine.ModifiedUser = username;
                            byMachine.ProcessIndex = process.ProcessIndex;
                            byMachine.UnitWeight =
                                MyUtilities.Product.GetProductInvWeight(productId, process.WarehouseId);
                        }
                    }
                    foreach (var byMachine in productionProcessByMachines) {
                        if (productionProcesses.Any(x => x.WarehouseId == byMachine.WarehouseId)) continue;
                        byMachine.Active = false;
                        byMachine.ModifiedDate = DateTime.Now;
                        byMachine.ModifiedUser = username;
                    }
                }
                else {
                    //insert new production process
                    var productionProcesses =
                        vfi.ProductionProcesses.Where(pp => pp.ProductId == productId && pp.IsNecessary);
                    foreach (var process in productionProcesses) {
                        var byMachine =
                            vfi.ProductionProcessByMachines.FirstOrDefault(
                                ppm =>
                                    ppm.ProductId == process.ProductId &&
                                    ppm.WarehouseId == process.WarehouseId &&
                                    ppm.MachineId == machineId);
                        if (byMachine == null) {
                            byMachine = new ProductionProcessByMachine {
                                ProductId = productId,
                                MachineId = machineId,
                                WarehouseId = process.WarehouseId,
                                Active = true,
                                Note = "Auto",
                                ModifiedDate = DateTime.Now,
                                ModifiedUser = username,
                                ProcessIndex = process.ProcessIndex,
                                UnitWeight =
                                    MyUtilities.Product.GetProductInvWeight(productId, process.WarehouseId)
                            };
                            vfi.ProductionProcessByMachines.Add(byMachine);
                        }
                        else {
                            byMachine.Active = true;
                            byMachine.Note = "Auto";
                            byMachine.ModifiedDate = DateTime.Now;
                            byMachine.ModifiedUser = username;
                            byMachine.ProcessIndex = process.ProcessIndex;
                            byMachine.UnitWeight =
                                MyUtilities.Product.GetProductInvWeight(productId, process.WarehouseId);
                        }
                    }
                }
                saved += vfi.SaveChanges();
            }
            return saved;
        }

        [GridAction]
        public ActionResult DeleteTrack(int trackId, int month, int year, int status) {
            try {
                using (var vfi = new vfiContext()) {
                    var track = vfi.TrackUpMachines.FirstOrDefault(tm => tm.TrackId == trackId);
                    if (track == null)
                        throw new Exception("Lỗi ! Không tìm thấy phiếu theo dõi ! Liên hệ admin");
                    track.Status = (byte)MyUtilities.Transaction.Status.Cancel;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("DeleteTrack", ex.Message);
            }
            return View(new GridModel(GetListTrackUpMachine(month, year, status)));
        }

        public ActionResult PrintMachineRepair(string fromDate, string toDate) {
            var model = new List<MachineRepairFormModelByShift>();
            try {
                var ci = new CultureInfo("vi-VN");
                var to = string.IsNullOrWhiteSpace(toDate)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(toDate, ci);
                var details =
                     GetMachine2ErrorStateList(0, fromDate, toDate, -1)
                        .Where(d => d.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete);
                var shifts = details.Select(d => d.Shift).Distinct().OrderBy(d => d).ToList();
                foreach (var shift in shifts) {
                    var byShift = new MachineRepairFormModelByShift {
                        Shift = shift + "",
                        ReportDateString = to.ToString("dd/MM/yyyy"),
                    };
                    var employeeIds = details.Where(d => d.Shift == shift).Select(d => d.EmployeeId).Distinct();

                    var i = 0;
                    foreach (var employeeId in employeeIds) {
                        var entities =
                            details.Where(d => d.Shift == shift && d.EmployeeId == employeeId)
                                   .OrderBy(d => d.EmployeeName);
                        var byEmloyee = new MachineRepairFormModelByEmployee {
                            EmployeeId = employeeId,
                            EmployeeName = entities.FirstOrDefault().EmployeeName,
                            Index = ++i,
                            CountDay = entities.FirstOrDefault().CountDay
                        };
                        var j = 0;
                        foreach (var entity in entities) {
                            entity.Index = ++j;
                        }
                        byEmloyee.Details.AddRange(entities);
                        byShift.List.Add(byEmloyee);
                    }
                    //sửa máy
                    var types = details.Where(d => d.Color == 0).ToList();
                    var type = new CountMachineRepairType {
                        NameType = "Không đạt",
                        CountType = types.Count,
                        TotalDiffTime = types.Sum(d => d.DiffTime),
                        TotalTime = types.Sum(d => d.FixTime),
                        Color = 0,
                    };
                    byShift.CountType.Add(type);
                    //lên máy
                    types = details.Where(d => d.Color == 2).ToList();
                    type = new CountMachineRepairType {
                        NameType = "Lên máy",
                        CountType = types.Count,
                        TotalDiffTime = types.Sum(d => d.DiffTime),
                        TotalTime = types.Sum(d => d.FixTime),
                        Color = 2,
                    };
                    byShift.CountType.Add(type);

                    //lên máy
                    types = details.Where(d => d.Color == 1).ToList();
                    type = new CountMachineRepairType {
                        NameType = "Không đạt",
                        CountType = types.Count,
                        TotalDiffTime = types.Sum(d => d.DiffTime),
                        TotalTime = types.Sum(d => d.FixTime),
                        Color = 1,
                    };
                    byShift.CountType.Add(type);
                    model.Add(byShift);
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintMachineRepair", ex.Message);
            }
            return PartialView("PageMachineRepairReport", model);
        }

        public ActionResult PrintMachineRepair2(string fromDate, string toDate) {
            var model = new List<MachineRepairFormModelByShift>();
            //var model2 = new List<MachineRepairFormModel>();
            var model3 = new List<RepairFormDetailModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fromD = string.IsNullOrWhiteSpace(fromDate)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(fromDate, ci);
                var to = string.IsNullOrWhiteSpace(toDate)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(toDate, ci);
                using (var vfi = new vfiContext()) {
                    //var machineStates =
                    //    vfi.MachineRepairForms.Where(mr => mr.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                    //                                       mr.CauseDate >= from && mr.CauseDate <= to)
                    //       .ToList();
                    var machineRepairs = from rd in vfi.RepairFormDetails
                                         where rd.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                               ((rd.StartDate >= fromD && rd.StartDate <= to) ||
                                                (rd.FinishDate >= fromD && rd.FinishDate <= to)) &&
                                               rd.MachineRepairForm.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                               rd.MachineRepairForm.SectionId == null
                                         select rd;

                    //var details =
                    //    GetMachineErrorStateList(0, 0, fromDate, toDate, -1)
                    //        .Where(d => d.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete);
                    var shifts = machineRepairs.Select(d => d.Shift).Distinct().OrderBy(d => d).ToList();
                    foreach (var shift in shifts) {
                        var byShift = new MachineRepairFormModelByShift {
                            Shift = shift + "",
                            FromDateString = fromD.ToString("dd/MM/yyyy"),
                            ToDateString = to.ToString("dd/MM/yyyy"),
                        };
                        var employees =
                            machineRepairs.Where(d => d.Shift == shift)
                                          .Select(d => d.Employee)
                                          .Distinct()
                                          .OrderBy(e => e.EmployeeName);
                        var i = 0;
                        foreach (var employee in employees) {
                            var machineRepairsByEmployee =
                                machineRepairs.Where(d => d.Shift == shift && d.EmployeeId == employee.EmployeeId);
                            var byEmployee = new MachineRepairFormModelByEmployee {
                                EmployeeId = employee.EmployeeId,
                                EmployeeName = employee.EmployeeName,
                                Index = ++i,
                            };
                            byShift.List.Add(byEmployee);
                            var j = 0;
                            foreach (var machineRepairDetail in machineRepairsByEmployee) {
                                var byMachine = byEmployee.Details.FirstOrDefault(d => d.FormId == machineRepairDetail.FormId);
                                if (byMachine == null) {
                                    byMachine = new MachineRepairFormModel {
                                        Index = ++j,
                                        FormId = machineRepairDetail.FormId,
                                        MachineId = machineRepairDetail.MachineRepairForm.MachineId,
                                        MachineName = machineRepairDetail.MachineRepairForm.Machine.MachineName,
                                        ProductId = machineRepairDetail.MachineRepairForm.ProductId,
                                        ProductCode = machineRepairDetail.MachineRepairForm.Product.ProductCode,
                                        StateId = machineRepairDetail.MachineRepairForm.StateId,
                                        StateName = machineRepairDetail.MachineRepairForm.MachineState.Description,
                                        ErrorCause = machineRepairDetail.MachineRepairForm.ErrorCause,
                                        CreateDate = machineRepairDetail.MachineRepairForm.CreateDate,
                                        CreateUser = machineRepairDetail.MachineRepairForm.CreateUser,
                                        Status = machineRepairDetail.MachineRepairForm.Status,
                                        StatusName =
                                           MyUtilities.Machine.State.GetRepairStatusText(
                                                machineRepairDetail.MachineRepairForm.Status),
                                        CauseDate = machineRepairDetail.MachineRepairForm.CauseDate,
                                    };
                                    byEmployee.Details.Add(byMachine);
                                    //model2.Add(byMachine);
                                }
                                var detail = new RepairFormDetailModel {
                                    ModifiedDate = machineRepairDetail.ModifiedDate,
                                    ModifiedUser = machineRepairDetail.FinishUser,
                                    Status = machineRepairDetail.Status,
                                    StatusName = MyUtilities.Machine.State.GetRepairStatusText(machineRepairDetail.Status),
                                    FinishDate = machineRepairDetail.FinishDate,
                                    FinishUser = machineRepairDetail.FinishUser,
                                    StartDate = machineRepairDetail.StartDate,
                                    StartUser = machineRepairDetail.StartUser,
                                    Note = machineRepairDetail.Note,
                                    Shift = machineRepairDetail.Shift,
                                    ToDate = DateTime.Now,
                                    //FromDate = from,
                                    MoreTime = machineRepairDetail.MoreTime,
                                    StateId = machineRepairDetail.MachineRepairForm.StateId,
                                    EmployeeId = machineRepairDetail.EmployeeId,
                                    EmployeeName = machineRepairDetail.Employee.EmployeeName,
                                    DetailId = machineRepairDetail.DetailId,
                                    FormId = machineRepairDetail.FormId,
                                    //FixId = machineRepairDetail.FixId,
                                    //HowToFix = machineRepairDetail.MachineStateDetail.Description,
                                    //Timing = machineRepairDetail.MachineStateDetail.Timing,
                                };
                                if (machineRepairDetail.FixId != null) {
                                    detail.FixId = machineRepairDetail.FixId.Value;
                                    detail.HowToFix = machineRepairDetail.MachineStateDetail.Description;
                                    detail.Timing = machineRepairDetail.MachineStateDetail.Timing;
                                }
                                byMachine.Details.Add(detail);
                                model3.Add(detail);
                            }
                        }
                        //sửa máy
                        var types = model3.Where(d3 => d3.Color == 0).ToList();
                        var type = new CountMachineRepairType {
                            NameType = "Sửa máy",
                            CountType = types.Count,
                            TotalDiffTime = types.Sum(d => d.DiffTime),
                            TotalTime = types.Sum(d => d.FixTime),
                            Color = 0,
                        };
                        byShift.CountType.Add(type);
                        //lên máy
                        types = model3.Where(d => d.Color == 2).ToList();
                        type = new CountMachineRepairType {
                            NameType = "Lên máy",
                            CountType = types.Count,
                            TotalDiffTime = types.Sum(d => d.DiffTime),
                            TotalTime = types.Sum(d => d.FixTime),
                            Color = 2,
                        };
                        byShift.CountType.Add(type);

                        //không đạt do nhân viên
                        types = model3.Where(d => d.Color == 1).ToList();
                        type = new CountMachineRepairType {
                            NameType = "Không đạt",
                            CountType = types.Count,
                            TotalDiffTime = types.Sum(d => d.DiffTime),
                            TotalTime = types.Sum(d => d.FixTime),
                            Color = 1,
                        };
                        byShift.CountType.Add(type);

                        // bảo trì máy
                        types = model3.Where(d => d.Color == 3).ToList();
                        type = new CountMachineRepairType {
                            NameType = "Bảo trì",
                            CountType = types.Count,
                            TotalDiffTime = types.Sum(d => d.DiffTime),
                            TotalTime = types.Sum(d => d.FixTime),
                            Color = 3,
                        };
                        byShift.CountType.Add(type);
                        model.Add(byShift);
                    }
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintMachineRepair2", ex.Message);
            }
            return PartialView("PageMachineRepairReport2", model);
        }

        public ActionResult PrintTrackUpMachine(int status, int month, int year) {
            var model = new List<TrackUpMachineModel>();
            try {
                using (var vfi = new vfiContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    model = GetListTrackUpMachine(month, year, status);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("PrintTrackUpMachine", ex.Message);
            }
            return PartialView("PageTrackUpMachineReport", model);
        }

        public ActionResult PrintPreparationTools(int trackId) {
            var model = new List<PreparationToolModel>();
            try {
                using (var vfi = new vfiContext()) {

                    var track = vfi.TrackUpMachines.FirstOrDefault(t => t.TrackId == trackId);
                    if (track == null)
                        throw new AggregateException("Lỗi! Không tìm thấy phiếu lên máy");
                    var replaceTools = vfi.ProductionToolReplacements.Where(pt => pt.TrackUpMachine.MachineId == track.MachineId);
                    var productionTools = from pt in vfi.ProductionTools
                                          where pt.Active && pt.ProductId == track.ProductId
                                          select pt;
                    var allProductionTools = vfi.SelectCurrentProductionTools;
                    var materialInv = 0.0;
                    var materialInvs = vfi.MaterialInventories.Where(mi => mi.TotalQty > 0 &&
                        mi.MaterialId == track.MaterialId);
                    if (materialInvs.Any())
                        materialInv = materialInvs.Sum(mi => mi.TotalQty);
                    var index = 1;
                    foreach (var productionTool in productionTools) {
                        var detail = new PreparationToolDetail {
                            ToolId = productionTool.ToolId,
                            ToolName = productionTool.Tool.ToolFullCode,
                            ToolUse = productionTool.Note,
                            TypeId = productionTool.Tool.MaterialTypeId,
                            TypeName = productionTool.Tool.MaterialType.MaterialTypeName,

                            ToolIndex = productionTool.ToolIndex ?? 0,
                            ExportQuantity = productionTool.UseNumber,
                            PrepareQuantity = 0,
                            TotalInv = 0,
                        };
                        var replaceTool = replaceTools.FirstOrDefault(rt => rt.ProductionToolId == productionTool.RealToolId);
                        if (replaceTool != null) {
                            if (replaceTool.ToolId != null) {
                                detail.ToolId = replaceTool.ToolId.Value;
                                detail.ToolName = replaceTool.Tool.ToolFullCode;
                                detail.TypeId = replaceTool.Tool.MaterialTypeId;
                                detail.TypeName = replaceTool.Tool.MaterialType.MaterialTypeName;
                            }
                            detail.PrepareQuantity = replaceTool.PrepareQuantity;
                            detail.Note = replaceTool.Note;
                        }
                        var sameProductionTools = allProductionTools.Where(pt => pt.ToolId == detail.ToolId &&
                            pt.MachineId != track.MachineId);
                        foreach (var sameProductionTool in sameProductionTools) {

                            replaceTool = vfi.ProductionToolReplacements.FirstOrDefault(pt => pt.TrackUpMachine.MachineId == track.MachineId &&
                                pt.ProductionToolId == productionTool.RealToolId);
                            if (replaceTool != null) {
                                if (replaceTool.ToolId != null) continue;
                            }
                            var same = detail.SameMachines.FirstOrDefault(s => s.MachineId == sameProductionTool.MachineId);
                            if (same == null) {
                                same = new SamePreparation {
                                    MachineId = sameProductionTool.MachineId,
                                    MachineName = sameProductionTool.MachineName,
                                };
                                detail.SameMachines.Add(same);
                            }
                        }

                        var toolInvs = vfi.ToolInventories.Where(ti => ti.ToolId == detail.ToolId);
                        if (toolInvs.Any()) {
                            detail.TotalInv = toolInvs.Sum(ti => ti.TotalQuantity);
                        }

                        var entity = model.FirstOrDefault(m => m.ToolTypeId == detail.TypeId);
                        if (entity == null) {
                            entity = new PreparationToolModel {
                                Index = index,
                                TrackId = trackId,
                                Quantity = track.Quantity,
                                DeliveryDate = track.DeliveryDate != null ? track.DeliveryDate.Value : track.StartDate,

                                DeliveryEmployee = track.DeliveryEmployee,
                                ReceiveEmployee = track.ReceiveEmployee,

                                ProductId = track.ProductId,
                                ProductCode = track.Product.ProductCode,
                                Productivity = track.RealProductivity,
                                ProductionRate = track.RealRate,

                                MaterialId = track.MaterialId.Value,
                                MaterialCode = track.Material.MaterialCode,
                                MaterialTypeName = track.Material.MaterialType.MaterialTypeName,

                                MachineId = track.MachineId,
                                MachineName = track.Machine.MachineName,
                                ToolTypeId = detail.TypeId,
                                ToolTypeName = detail.TypeName,
                                MaterialInv = materialInv
                            };
                            var sameMaterialProductions = vfi.SelectCurrentTrackUpMachines
                                .Where(t => t.MaterialId == entity.MaterialId && t.MachineId != entity.MachineId);
                            foreach (var sameMaterialProduction in sameMaterialProductions) {
                                var same = new SamePreparation {
                                    MachineId = sameMaterialProduction.MachineId,
                                    MachineName = sameMaterialProduction.MachineName
                                };
                                entity.SameMachines.Add(same);
                            }
                            index++;
                            model.Add(entity);

                        }
                        entity.Details.Add(detail);
                    }
                }
            }
            catch (Exception ex) {
                return Json(ex.Message);
            }
            return PartialView("PagePreparationTools", model);
        }

        [GridAction]
        public ActionResult SelectTrackingEmployeeMachine(string fromDate, string toDate) {
            var model = new List<TrackingRepairEmployeeModel>();
            try {
                model = GetTrackingEmployeeMachines(fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectTrackingEmployeeMachine", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<TrackingRepairEmployeeModel> GetTrackingEmployeeMachines(string fromDate, string toDate) {
            var model = new List<TrackingRepairEmployeeModel>();
            try {
                var ci = new CultureInfo("vi-VN");
                var fDate = string.IsNullOrWhiteSpace(fromDate)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(fromDate, ci);
                var tDate = string.IsNullOrWhiteSpace(toDate)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(toDate, ci).AddDays(1).AddSeconds(-1);
                using (var vfi = new vfiContext()) {
                    var repairMachines = (from ms in vfi.RepairFormDetails
                                          where ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                         (
                                             (ms.StartDate >= fDate && ms.StartDate <= tDate) ||
                                             (ms.StartDate <= fDate && ms.FinishDate == null) ||
                                             (ms.FinishDate >= fDate && ms.FinishDate <= tDate) ||
                                             (ms.StartDate < fDate && ms.FinishDate > tDate)
                                        )
                                          select new {
                                              ms.MachineRepairForm.Machine.MachineName,
                                              QcEmployee = ms.MachineRepairForm.Employee.EmployeeName,
                                              RepairEmployee = ms.Employee.EmployeeName,
                                              ms.Status,
                                              ms.Shift,
                                              ms.StartDate,
                                              ms.FinishDate,
                                              ms.Note,
                                              ms.ModifiedDate,
                                              ms.ModifiedUser
                                          }).ToList();
                    foreach (var repairMachine in repairMachines) {
                        var entity = new TrackingRepairEmployeeModel {
                            MachineName = repairMachine.MachineName,
                            QcEmployeeName = repairMachine.QcEmployee,
                            RepairEmployeeName = repairMachine.RepairEmployee,
                            StartDate = repairMachine.StartDate,
                            FinishDate = repairMachine.FinishDate,
                            Note = repairMachine.Note,
                            Shift = repairMachine.Shift,
                            ModifiedDate = repairMachine.ModifiedDate,
                            ModifiedUser = repairMachine.ModifiedUser,
                            StatusName = MyUtilities.Machine.State.GetRepairStatusText(repairMachine.Status)
                        };
                        model.Add(entity);
                    }
                    var trackingMachines = (from ms in vfi.TrackingRepairEmployees
                                          where ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                         (
                                             (ms.StartDate >= fDate && ms.StartDate <= tDate) ||
                                             (ms.FinishDate >= fDate && ms.FinishDate <= tDate) ||
                                             (ms.StartDate < fDate && ms.FinishDate > tDate)
                                        )
                                          select new {
                                              ms.Machine.MachineName,
                                              QcEmployee = ms.Employee1.EmployeeName,
                                              RepairEmployee = ms.Employee.EmployeeName,
                                              ms.Status,
                                              ms.Shift,
                                              ms.StartDate,
                                              ms.FinishDate,
                                              ms.Note,
                                              ms.ModifiedDate,
                                              ms.ModifiedUser
                                          }).ToList();
                    foreach (var trackingMachine in trackingMachines) {
                        var entity = new TrackingRepairEmployeeModel {
                            MachineName = trackingMachine.MachineName,
                            QcEmployeeName = trackingMachine.QcEmployee,
                            RepairEmployeeName = trackingMachine.RepairEmployee,
                            StartDate = trackingMachine.StartDate,
                            FinishDate = trackingMachine.FinishDate,
                            Note = trackingMachine.Note,
                            Shift = trackingMachine.Shift,
                            ModifiedDate = trackingMachine.ModifiedDate,
                            ModifiedUser = trackingMachine.ModifiedUser,
                            StatusName = MyUtilities.Machine.State.GetRepairStatusText(trackingMachine.Status)
                        };
                        model.Add(entity);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return model.OrderBy(m => m.RepairEmployeeName).ThenBy(m => m.StartDate).ToList();
        }

        [GridAction]
        public ActionResult InsertTrackingEmployeeMachine(TrackingRepairEmployeeModel inserted, string fromDate, string toDate) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }

                try {
                    inserted.MachineId = Convert.ToInt32(inserted.MachineName);
                }
                catch (FormatException) { }
                if (inserted.MachineId == 0)
                    throw new AggregateException("Lỗi! Chọn lại máy.");

                try {
                    inserted.RepairEmployeeId = Convert.ToInt32(inserted.RepairEmployeeName);
                }
                catch (FormatException) { }
                if (inserted.RepairEmployeeId == 0)
                    throw new AggregateException("Lỗi! Chọn lại nhân viên sửa máy.");

                try {
                    inserted.QcEmployeeId = Convert.ToInt32(inserted.QcEmployeeName);
                }
                catch (FormatException) { }
                if (inserted.QcEmployeeId == 0)
                    throw new AggregateException("Lỗi! Chọn lại nhân viên QC.");
                if (inserted.StartDate >= inserted.FinishDate)
                    throw new AggregateException("Lỗi! Thời gian bắt đầu và kết thúc sai. BĐ phải nhỏ hơn KT");
                using (var vfi = new vfiContext()) {

                    var repairMachine = (from ms in vfi.RepairFormDetails
                                         where ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                         ms.EmployeeId == inserted.RepairEmployeeId &&
                                          (
                                              (ms.StartDate >= inserted.StartDate && ms.StartDate <= inserted.FinishDate) ||
                                              (ms.StartDate <= inserted.StartDate && ms.FinishDate == null) ||
                                              (ms.FinishDate >= inserted.StartDate && ms.FinishDate <= inserted.FinishDate) ||
                                              (ms.StartDate < inserted.StartDate && ms.FinishDate > inserted.FinishDate)
                                         )
                                         select ms).FirstOrDefault();
                    if (repairMachine != null) {
                        throw new AggregateException("Lỗi! Nhân viên sửa máy đang sửa ở máy " + repairMachine.MachineRepairForm.Machine.MachineName);
                    }
                    var trackingMachine = (from ms in vfi.TrackingRepairEmployees
                                           where ms.Status != (byte)MyUtilities.Machine.State.RepairStatus.Delete &&
                                           ms.RepairEmployeeId == inserted.RepairEmployeeId &&
                                            (
                                                (ms.StartDate >= inserted.StartDate && ms.StartDate <= inserted.FinishDate) ||
                                                (ms.StartDate <= inserted.StartDate && ms.FinishDate == null) ||
                                                (ms.FinishDate >= inserted.StartDate && ms.FinishDate <= inserted.FinishDate) ||
                                                (ms.StartDate < inserted.StartDate && ms.FinishDate > inserted.FinishDate)
                                           )
                                           select ms).FirstOrDefault();
                    if (trackingMachine != null) {
                        throw new AggregateException("Lỗi! Nhân viên sửa máy đang theo dõi ở máy " + trackingMachine.Machine.MachineName);
                    }
                    var entity = new TrackingRepairEmployee() {
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        FixQuantity = 0,
                        MoreTime = 0,
                        Shift = inserted.Shift,
                        Status = (byte)MyUtilities.Machine.State.RepairStatus.Finish,
                        StartDate = inserted.StartDate,
                        FinishDate = inserted.FinishDate.Value,
                        Note = inserted.Note,
                        QcEmployeeId = inserted.QcEmployeeId,
                        RepairEmployeeId = inserted.RepairEmployeeId,
                        MachineId = inserted.MachineId
                    };
                    vfi.TrackingRepairEmployees.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertTrackingEmployeeMachine", ex.Message);
            }
            return View(new GridModel(GetTrackingEmployeeMachines(fromDate, toDate)));
        }

        [GridAction]
        public ActionResult UpdateTrackingEmployeeMachine(TrackingRepairEmployeeModel updated, string fromDate, string toDate) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateTrackingEmployeeMachine", ex.Message);
            }
            return View(new GridModel(GetTrackingEmployeeMachines(fromDate, toDate)));
        }

        [GridAction]
        public ActionResult SelectToolInvRequirement(int toolTypeId, string toolName, string fromDate, string toDate) {
            var model = new List<ToolInvRequirementModel>();
            try {
                model = GetToolInventoryRequirement(toolTypeId, toolName, fromDate, toDate);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectToolInvRequirement", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ToolInvRequirementModel> GetToolInventoryRequirement(int toolTypeId, string toolName, string fromDate, string toDate) {
            var model = new List<ToolInvRequirementModel>();
            var ci = new CultureInfo("vi-VN");
            DateTime fDate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(fromDate))
                fDate = Convert.ToDateTime(fromDate, ci);
            DateTime tDate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(toDate))
                tDate = Convert.ToDateTime(toDate, ci);
            using (var vfi = new vfiContext()) {
                var requirements = from tir in vfi.ToolInventoryRequirements
                                   where tir.StartDate >= fDate && tir.StartDate <= tDate &&
                                   (toolTypeId == 0 || tir.Tool.MaterialTypeId == toolTypeId)
                                   select tir;
                foreach (var requirement in requirements) {
                    var entity = new ToolInvRequirementModel {
                        ToolTypeId = requirement.Tool.MaterialTypeId,
                        ToolTypeName = requirement.Tool.MaterialType.MaterialTypeName,
                        ToolId = requirement.ToolId,
                        ToolFullCode = requirement.Tool.ToolFullCode,
                        StartDate = requirement.StartDate,
                        ModifiedUser = requirement.ModifiedUser,
                        ModifiedDate = requirement.ModifiedDate,
                        Note = requirement.Note,
                        RequireQuantity = requirement.RequireQuantity,
                        RequirementId = requirement.RequirementId
                    };
                    model.Add(entity);
                }

            }
            return model;
        }

        [GridAction]
        public ActionResult InsertToolInventoryRequirement(ToolInvRequirementModel inserted, int toolTypeId, string toolName, string fromDate, string toDate) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var toolId = 0;
                    try {
                        toolId = Convert.ToInt32(inserted.ToolFullCode);
                    }
                    catch (FormatException) {
                        toolId = vfi.Tools.FirstOrDefault(p => p.ToolFullCode.Equals(inserted.ToolFullCode))
                                       .ToolId;
                    }
                    if (toolId == 0)
                        throw new AggregateException("Lỗi! Không tìm thấy công cụ ! Vui lòng chọn lại.");
                    if (inserted.RequireQuantity < 0)
                        throw new AggregateException("Lỗi! Vui lòng điền số lượng yêu cầu.");
                    var requirement = new ToolInventoryRequirement {
                        ToolId = toolId,
                        RequireQuantity = inserted.RequireQuantity,
                        Note = inserted.Note,
                        StartDate = DateTime.Now.Date,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                    };
                    vfi.ToolInventoryRequirements.Add(requirement);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertToolInventoryRequirement", ex.Message);
            }
            return View(new GridModel(GetToolInventoryRequirement(toolTypeId, toolName, fromDate, toDate)));
        }

        [GridAction]
        public ActionResult UpdateToolInventoryRequirement(ToolInvRequirementModel updated, int toolTypeId, string toolName, string fromDate, string toDate) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var toolId = 0;
                    try {
                        toolId = Convert.ToInt32(updated.ToolFullCode);
                    }
                    catch (FormatException) {
                        toolId = vfi.Tools.FirstOrDefault(p => p.ToolFullCode.Equals(updated.ToolFullCode))
                                       .ToolId;
                    }
                    if (toolId == 0)
                        throw new AggregateException("Lỗi! Không tìm thấy công cụ ! Vui lòng chọn lại.");
                    if (updated.RequireQuantity < 0)
                        throw new AggregateException("Lỗi! Vui lòng điền số lượng yêu cầu.");
                    var requirement = vfi.ToolInventoryRequirements.FirstOrDefault(tir => tir.RequirementId == updated.RequirementId);
                    if (requirement == null)
                        throw new AggregateException("Lỗi! Không tìm thấy yêu cầu!");
                    requirement.RequireQuantity = updated.RequireQuantity;
                    requirement.Note = updated.Note;
                    if (toolId != 0)
                        requirement.ToolId = toolId;
                    requirement.ModifiedUser = HttpContext.User.Identity.Name;
                    requirement.ModifiedDate = DateTime.Now;

                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateToolInventoryRequirement", ex.Message);
            }
            return View(new GridModel(GetToolInventoryRequirement(toolTypeId, toolName, fromDate, toDate)));
        }


        public ActionResult GetLastTrackUpMachineInfo(int machineId, int productId, int materialId) {
            try {
                using (var vfi = new tammaContext()) {
                    var track = vfi.TrackUpMachines.Where(x => x.Status == (byte)MyUtilities.Transaction.Status.Approved 
                                                            && x.DeliveryDate < DateTime.Now 
                                                            && x.MachineId == machineId 
                                                            && (productId == 0 || x.ProductId == productId)
                                                            && (materialId == 0 || x.MaterialId == materialId))
                                                    .OrderByDescending(x => x.DeliveryDate)
                                                    .FirstOrDefault();
                    if (track == null) {
                        track = vfi.TrackUpMachines.Where(x => x.Status == (byte)MyUtilities.Transaction.Status.Approved
                                                                && x.DeliveryDate < DateTime.Now
                                                                && x.MachineId == machineId
                                                                && (productId == 0 || x.ProductId == productId))
                                                        .OrderByDescending(x => x.DeliveryDate)
                                                        .FirstOrDefault();
                        if (track == null) {
                            var product = vfi.Products.FirstOrDefault(x => x.ProductId == productId);
                            if (product == null) {
                                return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NotFound, "Không tìm thấy dữ liệu cũ!", null));
                            }
                            return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "",
                                new TrackUpMachineModel {
                                    Productivity = product.Productivity ?? 0,
                                    KnifeCut = product.KnifeCut ?? 0,
                                    WorkPiece = 0
                                }));
                        }
                    }

                    return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.NoError, "",
                        new TrackUpMachineModel {
                            Productivity = track.RealProductivity,
                            KnifeCut = track.KnifeCut,
                            WorkPiece = track.WorkPiece
                        }));
                }
            }
            catch (Exception ex) {
                return Json(new MyUtilities.Monitor.MyJsonResult((int)MyUtilities.Monitor.ErrorCode.Exception, ex.Message, null));
            }
        }

        #endregion

        #region production process

        [GridAction]
        public ActionResult SelectProductionProcessByMachine(int machineId, int productId) {
            var model = new List<ProductionProcessByMachineModel>();
            try {
                model = GetProductionProcessByMachines(machineId, productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProductionProcessByMachine", ex.Message);
            };
            return View(new GridModel(model));
        }

        List<ProductionProcessByMachineModel> GetProductionProcessByMachines(int machineId, int productId) {

            var model = new List<ProductionProcessByMachineModel>();
            using (var vfi = new vfiContext()) {
                var processByMachines =
                    vfi.ProductionProcessByMachines.Where(
                        ppm => ppm.MachineId == machineId && ppm.ProductId == productId && ppm.Active);
                foreach (var process in processByMachines) {
                    var entity = new ProductionProcessByMachineModel {
                        MachineId = machineId,
                        MachineName = process.Machine.MachineName,
                        ProductId = productId,
                        ProductCode = process.Product.ProductCode,
                        WarehouseId = process.WarehouseId,
                        WarehouseName = process.Warehouse.WarehouseName,
                        ModifiedUser = process.ModifiedUser,
                        ModifiedDate = process.ModifiedDate,
                        Note = process.Note,
                        UnitWeight = process.UnitWeight,
                        DetailId = process.DetailId,
                        Active = process.Active,
                        ProcessIndex = process.ProcessIndex,
                    };
                    model.Add(entity);
                }
            }
            return model.OrderBy(m => m.ProcessIndex).ToList();
        }

        [GridAction]
        public ActionResult InsertProductionProcessByMachine(int machineId, int productId, ProductionProcessByMachineModel insert) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var warehouseId = 0;
                    try {
                        warehouseId = Convert.ToInt32(insert.WarehouseName);
                    }
                    catch (FormatException) {
                        warehouseId = insert.WarehouseId;
                    }
                    var entity =
                        vfi.ProductionProcessByMachines.FirstOrDefault(
                            ppm =>
                                ppm.ProductId == productId &&
                                ppm.MachineId == machineId &&
                                ppm.WarehouseId == insert.WarehouseId);
                    if (entity == null) {
                        entity = new ProductionProcessByMachine {
                            ProductId = productId,
                            MachineId = machineId,
                            WarehouseId = warehouseId,
                            Active = true,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = HttpContext.User.Identity.Name,
                            Note = insert.Note,
                            ProcessIndex = insert.ProcessIndex,
                            UnitWeight = insert.UnitWeight,
                        };
                        vfi.ProductionProcessByMachines.Add(entity);
                    }
                    else {
                        if (!entity.Active)
                            entity.Active = true;
                        entity.ModifiedDate = DateTime.Now;
                        entity.ModifiedUser = HttpContext.User.Identity.Name;
                        entity.Note = insert.Note;
                        entity.UnitWeight = insert.UnitWeight;
                        entity.ProcessIndex = insert.ProcessIndex;
                    }
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product == null)
                        throw new AggregateException("Lỗi ! Tìm không thấy sản phẩm");
                    switch (warehouseId) {
                        //        1	NULL	Kho SX 1
                        case (int)MyUtilities.Warehouse.Id.Production1:
                            if (product.ProductionWeight == null ||
                                product.ProductionWeight == 1 ||
                                product.ProductionWeight == 0)
                                product.ProductionWeight = entity.UnitWeight;
                            break;
                        //2	NULL	Kho SX 2/ CNC
                        case (int)MyUtilities.Warehouse.Id.Cnc:
                            if (product.CncWeight == null ||
                                product.CncWeight == 1 ||
                                product.CncWeight == 0)
                                product.CncWeight = entity.UnitWeight;
                            break;
                        case (int)MyUtilities.Warehouse.Id.Production2:
                        case (int)MyUtilities.Warehouse.Id.Production2B:
                        case (int)MyUtilities.Warehouse.Id.Production2C:
                        case (int)MyUtilities.Warehouse.Id.Production2D:
                            if (product.Production2Weight == null ||
                                product.Production2Weight == 1 ||
                                product.Production2Weight == 0)
                                product.Production2Weight = entity.UnitWeight;
                            break;
                        //3	NULL	Chờ nhiệt luyện
                        case (int)MyUtilities.Warehouse.Id.HeatTreatment:
                            if (product.HeatTreatmentWeight == null ||
                                product.HeatTreatmentWeight == 1 ||
                                product.HeatTreatmentWeight == 0)
                                product.HeatTreatmentWeight = entity.UnitWeight;
                            break;
                        //4	NULL	Chờ rung bóng
                        case (int)MyUtilities.Warehouse.Id.SurfaceTreatment:
                            if (product.SurfaceTreatmentWeight == null ||
                                product.SurfaceTreatmentWeight == 1 ||
                                product.SurfaceTreatmentWeight == 0)
                                product.SurfaceTreatmentWeight = entity.UnitWeight;
                            break;
                        //5	NULL	Chờ GCN
                        case (int)MyUtilities.Warehouse.Id.WaitingPlating:
                            if (product.WaitingPlatingWeight == null ||
                                product.WaitingPlatingWeight == 1 ||
                                product.WaitingPlatingWeight == 0)
                                product.WaitingPlatingWeight = entity.UnitWeight;
                            break;
                        //6	NULL	Kho nhà cung ứng
                        case (int)MyUtilities.Warehouse.Id.Plating:
                        case (int)MyUtilities.Warehouse.Id.PlatingTest:
                            if (product.PlatingWeight == null ||
                                product.PlatingWeight == 1 ||
                                product.PlatingWeight == 0)
                                product.PlatingWeight = entity.UnitWeight;
                            break;
                        default:
                            if (product.QcWeight == null ||
                                product.QcWeight == 1 ||
                                product.QcWeight == 0)
                                product.QcWeight = entity.UnitWeight;
                            break;
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProductionProcessByMachine", ex.Message);
            };
            return View(new GridModel(GetProductionProcessByMachines(machineId, productId)));
        }

        [GridAction]
        public ActionResult UpdateProductionProcessByMachine(ProductionProcessByMachineModel update) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                var warehouseId = 0;
                try {
                    warehouseId = Convert.ToInt32(update.WarehouseName);
                }
                catch (FormatException) {
                    //warehouseId = update.WarehouseId;
                }
                using (var vfi = new vfiContext()) {
                    var entity = vfi.ProductionProcessByMachines.FirstOrDefault(ppm => ppm.DetailId == update.DetailId);
                    if (entity == null)
                        throw new AggregateException("Lỗi! Không tìm thấy quy trình.");
                    if (warehouseId != 0)
                        entity.WarehouseId = warehouseId;
                    entity.Active = update.Active;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    entity.Note = update.Note;
                    entity.UnitWeight = update.UnitWeight;
                    switch (warehouseId) {
                        //        1	NULL	Kho SX 1
                        case (int)MyUtilities.Warehouse.Id.Production1:
                            if (entity.Product.ProductionWeight == null ||
                                entity.Product.ProductionWeight == 1 ||
                                entity.Product.ProductionWeight == 0)
                                entity.Product.ProductionWeight = entity.UnitWeight;
                            break;
                        //2	NULL	Kho SX 2/ CNC
                        case (int)MyUtilities.Warehouse.Id.Cnc:
                            if (entity.Product.CncWeight == null ||
                                entity.Product.CncWeight == 1 ||
                                entity.Product.CncWeight == 0)
                                entity.Product.CncWeight = entity.UnitWeight;
                            break;
                        case (int)MyUtilities.Warehouse.Id.Production2:
                        case (int)MyUtilities.Warehouse.Id.Production2B:
                        case (int)MyUtilities.Warehouse.Id.Production2C:
                        case (int)MyUtilities.Warehouse.Id.Production2D:
                            if (entity.Product.Production2Weight == null ||
                                entity.Product.Production2Weight == 1 ||
                                entity.Product.Production2Weight == 0)
                                entity.Product.Production2Weight = entity.UnitWeight;
                            break;
                        //3	NULL	Chờ nhiệt luyện
                        case (int)MyUtilities.Warehouse.Id.HeatTreatment:
                            if (entity.Product.HeatTreatmentWeight == null ||
                                entity.Product.HeatTreatmentWeight == 1 ||
                                entity.Product.HeatTreatmentWeight == 0)
                                entity.Product.HeatTreatmentWeight = entity.UnitWeight;
                            break;
                        //4	NULL	Chờ rung bóng
                        case (int)MyUtilities.Warehouse.Id.SurfaceTreatment:
                            if (entity.Product.SurfaceTreatmentWeight == null ||
                                entity.Product.SurfaceTreatmentWeight == 1 ||
                                entity.Product.SurfaceTreatmentWeight == 0)
                                entity.Product.SurfaceTreatmentWeight = entity.UnitWeight;
                            break;
                        //5	NULL	Chờ GCN
                        case (int)MyUtilities.Warehouse.Id.WaitingPlating:
                            if (entity.Product.WaitingPlatingWeight == null ||
                                entity.Product.WaitingPlatingWeight == 1 ||
                                entity.Product.WaitingPlatingWeight == 0)
                                entity.Product.WaitingPlatingWeight = entity.UnitWeight;
                            break;
                        //6	NULL	Kho nhà cung ứng
                        case (int)MyUtilities.Warehouse.Id.Plating:
                        case (int)MyUtilities.Warehouse.Id.PlatingTest:
                            if (entity.Product.PlatingWeight == null ||
                                entity.Product.PlatingWeight == 1 ||
                                entity.Product.PlatingWeight == 0)
                                entity.Product.PlatingWeight = entity.UnitWeight;
                            break;
                        default:
                            if (entity.Product.QcWeight == null ||
                                entity.Product.QcWeight == 1 ||
                                entity.Product.QcWeight == 0)
                                entity.Product.QcWeight = entity.UnitWeight;
                            break;
                    }
                    entity.ProcessIndex = update.ProcessIndex;
                    vfi.SaveChanges();
                    update.ProductId = entity.ProductId;
                    update.MachineId = entity.MachineId;
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProductionProcessByMachine", ex.Message);
            }
            return View(new GridModel(GetProductionProcessByMachines(update.MachineId, update.ProductId)));
        }
        #endregion

        #region processing classified

        [GridAction]
        public ActionResult SelectProcessClassified() {
            var model = new List<ProcessClassifiedModel>();
            try {
                model = GetProcessClassified();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectProcessClassified", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<ProcessClassifiedModel> GetProcessClassified() {
            var model = new List<ProcessClassifiedModel>();
            using (var vfi = new tammaContext()) {
                model = vfi.ProcessClassifieds
                    .Select(x => new ProcessClassifiedModel {
                        ClassifiedId = x.ClassifiedId,
                        Name = x.Name,
                        Description = x.Description,
                        SalesFactor = x.SalesFactor,
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
        public ActionResult InsertProcessClassified(ProcessClassifiedModel insert) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var entity = new ProcessClassified {
                        Name = insert.Name,
                        Description = insert.Description,
                        SalesFactor = insert.SalesFactor,
                        Active = true,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                    };
                    vfi.ProcessClassifieds.Add(entity);
                    vfi.SaveChanges();
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProcessClassified", ex.Message);
            }
            return View(new GridModel(GetProcessClassified()));
        }


        [HttpPost]
        [GridAction]
        public ActionResult UpdateProcessClassified(ProcessClassifiedModel update) {
            if (!Request.IsAuthenticated) {
                throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n " +
                                         "1 trong các nguyên nhân như mất thời gian chờ. \r\n " +
                                         "Xin vui lòng đăng nhập lại hệ thống.");
            }
            try {
                using (var vfi = new tammaContext()) {
                    var entity = vfi.ProcessClassifieds.FirstOrDefault(x => x.ClassifiedId == update.ClassifiedId);
                    if (entity == null) { throw new AggregateException("Lỗi! Không tìm thấy data"); }
                    entity.Name = update.Name;
                    entity.Description = update.Description;
                    entity.SalesFactor = update.SalesFactor;
                    entity.Active = update.Active;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    vfi.SaveChanges();
                }

            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProcessClassified", ex.Message);
            }
            return View(new GridModel(GetProcessClassified()));
        }

        public ActionResult SelectComboboxProcessClassified() {
            var model = new List<ProcessClassifiedModel>();
            using (var vfi = new tammaContext()) {
                model = GetProcessClassified().Where(x => x.Active).ToList();
            }
            return new JsonResult {
                Data = new SelectList(model, "ClassifiedId", "FullDescription")
            };
        }
        #endregion

        #region processing type

        [GridAction]
        public ActionResult SelectProcessingType() {
            var model = GetProcessingType();
            return View(new GridModel(model));
        }

        List<ProcessingTypeModel> GetProcessingType() {
            var model = new List<ProcessingTypeModel>();
            try {
                using (var vfi = new vfiContext()) {
                    model = (from x in vfi.ProcessingTypes
                             select new ProcessingTypeModel {
                                 TypeId = x.TypeId,
                                 TypeName = x.TypeName,
                                 Active = x.Active,
                                 Description = x.Description,
                                 ModifiedDate = x.ModifiedDate ?? DateTime.Now,
                                 ModifiedUser = x.ModifiedUser,
                                 ProcessingFactor = x.ProcessingFactor ?? 0,
                                 ProcessingSaleFactor = x.ProcessingSaleFactor ?? 0,
                                 IsProductionManagement = 1,
                                 IsSaleManagement = 1,
                                 ForWarehouseId = x.ForWarehouseId ?? 0,
                                 WarehouseName = x.ForWarehouseId != null ? x.Warehouse.WarehouseName : "",
                                 ForIdx = x.ForWarehouseId != null ? x.Warehouse.Idx : 0,
                             }).ToList();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("GetProcessingType", ex.Message);
            }
            return model.OrderByDescending(m => m.Active).ThenBy(x => x.ForIdx).ThenBy(m => m.TypeName).ToList();
        }

        [GridAction]
        public ActionResult InsertProcessingType(ProcessingTypeModel newType) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var entity = new ProcessingType() {
                        Active = true,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        TypeName = newType.TypeName,
                        Description = newType.Description,
                        ProcessingFactor = newType.ProcessingFactor,
                        ProcessingSaleFactor = newType.ProcessingSaleFactor * 8,                        
                    };
                    var warehouseId = 0;
                    try { warehouseId = Convert.ToInt32(newType.WarehouseName); }
                    catch (FormatException) { }
                    if (warehouseId != 0) {
                        entity.ForWarehouseId = warehouseId;
                    }
                    vfi.ProcessingTypes.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertProcessingType", ex.Message);
            }
            return View(new GridModel(GetProcessingType()));
        }

        [GridAction]
        public ActionResult UpdateProcessingType(ProcessingTypeModel updateType) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var production = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.ProductionManagement);
                    var sale = MyUtilities.UserRole.CheckRole(HttpContext.User.Identity.Name,
                        MyUtilities.UserRole.SaleManagement);
                    //var permisstionProduction = vfi.per
                    if (!production && !sale)
                        throw new AggregateException("Lỗi! Bạn không có quyền thay đổi! Liên hệ Admin");
                    var entity = vfi.ProcessingTypes.FirstOrDefault(pt => pt.TypeId == updateType.TypeId);
                    if (entity != null) {
                        if (production) {
                            entity.Active = updateType.Active;
                            entity.TypeName = updateType.TypeName;
                            entity.Description = updateType.Description;
                            entity.ProcessingFactor = updateType.ProcessingFactor;
                        }
                        if (sale) {
                            entity.ProcessingSaleFactor = updateType.ProcessingSaleFactor;
                        }

                        var warehouseId = updateType.ForWarehouseId;
                        try { warehouseId = Convert.ToInt32(updateType.WarehouseName); }
                        catch (FormatException) { }
                        if (warehouseId != 0) {
                            entity.ForWarehouseId = warehouseId;
                        }
                    }
                    else {
                        throw new AggregateException("Lỗi! \nKhông tìm thấy loại máy gia công!");
                    }
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateProcessingType", ex.Message);
            }
            return View(new GridModel(GetProcessingType()));
        }

        public ActionResult SelectComboBoxProcessingType() {
            var model = new List<ProcessingTypeModel>();
            using (var vfi = new vfiContext()) {
                foreach (var type in vfi.ProcessingTypes.Where(pt => pt.Active)) {
                    var entity = new ProcessingTypeModel {
                        TypeId = type.TypeId,
                        TypeName = type.TypeName,
                    };
                    model.Add(entity);
                }
            }
            return new JsonResult {
                Data = new SelectList(model, "TypeId", "TypeName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public List<ProcessingTypeModel> GetActiveProcessingType(MachineConfiguration config) {
            var model = new List<ProcessingTypeModel>();
            using (var vfi = new vfiContext()) {
                model = (from x in vfi.ProcessingTypes
                        where x.Active &&
                        (config.IsQC == null || (x.ForWarehouseId != null && x.Warehouse.IsQC == config.IsQC))
                        select new ProcessingTypeModel {
                            TypeId = x.TypeId,
                            TypeName = x.TypeName,
                        }).ToList();
            }
            return model.OrderBy(x => x.TypeName).ToList();
        }

        public ActionResult SelectComboBoxProcessingTypeQc() {
            return new JsonResult {
                Data = new SelectList(GetActiveProcessingType(new MachineConfiguration { IsQC = true }), "TypeId", "TypeName"),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult GetMachineProcessDiv(int? month, int? year, string machineName) {
            if (month == null || year == null) {
                month = DateTime.Now.Month;
                year = DateTime.Now.Year;
            }
            var startDate = new DateTime(year.Value, month.Value, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var days = MyUtilities.Function.DaysNoSunDay(startDate, endDate);
            var listDays = MyUtilities.Function.ListDaysNoSunday(startDate, endDate);
            var model = new List<MachineModel>();
            try {
                using (var vfi = new vfiContext()) {
                    vfi.Configuration.LazyLoadingEnabled = false;
                    var tracks = (from t in vfi.TrackUpMachines
                                  where t.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                        ((t.DeliveryDate == null
                                              ? t.ForecastDate <= endDate &&
                                                t.ForecastDate >= startDate
                                              : t.DeliveryDate <= endDate && t.DeliveryDate >= startDate) ||
                                         (t.EndDate >= startDate && t.EndDate <= endDate) ||
                                         ((t.DeliveryDate == null
                                               ? t.ForecastDate < startDate
                                               : t.DeliveryDate < startDate)
                                          && t.EndDate > endDate))
                                        && t.Machine.MachineName.Contains(machineName)
                                        && t.Quantity > 0
                                  select new {
                                      t.ProductId,
                                      t.Product.ProductCode,
                                      t.MachineId,
                                      t.Machine.MachineName,
                                      t.Quantity,
                                      StartDate = t.DeliveryDate ?? t.ForecastDate,
                                      t.EndDate,
                                      t.RealProductivity,
                                      t.ForecastDay,
                                      Real = t.DeliveryDate != null
                                  }).OrderBy(t => t.StartDate).ToList();
                    var startTrackDate = tracks.Any() ? tracks.FirstOrDefault().StartDate : startDate;
                    var importProductions = (from i in vfi.ImportFormSX1Detail
                                             where
                                                 //i.ImportFormSX1.MaterialUseDate.Month == month &&
                                                 //i.ImportFormSX1.MaterialUseDate.Year == year &&
                                                 i.ImportFormSX1.MaterialUseDate >= startTrackDate &&
                                                 i.ImportFormSX1.MaterialUseDate.Year == year &&
                                                 i.ImportFormSX1.ImportWorkpieceMaterials.Any() &&
                                                 i.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault() != null &&
                                                 i.ImportFormSX1.ImportWorkpieceMaterials.FirstOrDefault()
                                                  .Transaction.Status ==
                                                 (byte)MyUtilities.Transaction.Status.Approved &&
                                                 i.Machine1.MachineName.Contains(machineName)
                                             orderby i.ImportFormSX1.MaterialUseDate
                                             select new {
                                                 i.MachineId,
                                                 i.ProductId,
                                                 Quantity = (i.Number1 + i.Number2),
                                                 i.ImportFormSX1.MaterialUseDate,
                                             }).ToList();
                    var forecasts = (from f in vfi.ForecastOrders
                                     where f.Status == (byte)MyUtilities.Transaction.Status.Approved &&
                                           f.ForecastDate.Month == month &&
                                           f.ForecastDate.Year == year
                                     select new {
                                         f.ProductId,
                                         f.Quantity,
                                     }).ToList();
                    var warehouseSum = MyUtilities.Warehouse.GetWarehouseId_SumTotalQuantity();
                    var productInvs = (from pi in vfi.ProductInventories
                                       where warehouseSum.Contains(pi.WarehouseId)
                                       select new {
                                           pi.ProductId,
                                           pi.TotalQty,
                                       }).ToList();
                    var smartProduction =
                        vfi.SmartProductions.Where(sp => sp.Machine.MachineName.Contains(machineName)).ToList();
                    var machines =
                        vfi.Machines.Where(
                            m => m.Active && m.MachineName.Contains(machineName) && m.MachineName.Contains("C"))
                           .OrderBy(m => m.MachineName)
                           .ToList();
                    foreach (var machine in machines) {
                        var entity = new MachineModel {
                            MachineId = machine.MachineId,
                            MachineName = machine.MachineName,
                            DayRate = days,
                            Days = listDays,
                        };
                        if (machine.DiagramType == 1)
                            entity.DiagramTypeName = "Cames";
                        else if (machine.DiagramType == 2)
                            entity.DiagramTypeName = "Cnc";
                        var tracksById = tracks.Where(t => t.MachineId == entity.MachineId);
                        foreach (var trackUpMachine in tracksById) {
                            var track = new TrackUpMachineModel {
                                ProductId = trackUpMachine.ProductId,
                                ProductCode = trackUpMachine.ProductCode,
                                StartDate = trackUpMachine.StartDate,
                                Quantity = trackUpMachine.Quantity,
                                RealProductivity = trackUpMachine.RealProductivity,
                                EndDate = trackUpMachine.EndDate,
                                Process = new List<Process>(),
                                Production = new List<Process>(),
                                ForecastDay = trackUpMachine.ForecastDay,
                                ForecastQuantity = forecasts.Where(f => f.ProductId == trackUpMachine.ProductId).ToList().Sum(f => f.Quantity),
                                TotalInv = productInvs.Where(pi => pi.ProductId == trackUpMachine.ProductId).ToList().Sum(pi => pi.TotalQty)
                            };
                            track.ProcessDay = MyUtilities.Function.RoundUp(
                                track.Quantity / MyUtilities.Product.GetProductionRateInFactoryDayTime(track.RealProductivity));
                            // du kien sx
                            if (track.StartDate <= startDate) {
                                if (track.EndDate >= endDate) {
                                    track.Process.Add(new Process(days, 1));
                                }
                                else {
                                    track.Process.Add(
                                        new Process(
                                            MyUtilities.Function.DaysNoSunDay(startDate, track.EndDate), 1));
                                    track.Process.Add(
                                        new Process(
                                            MyUtilities.Function.DaysNoSunDay(track.EndDate.AddDays(1), endDate), 0));
                                }
                            }
                            else {
                                var forecastDay = track.ForecastDay > 0 && !trackUpMachine.Real
                                                      ? track.ForecastDay + 2
                                                      : 1;
                                if (track.StartDate == track.EndDate)
                                    forecastDay--;
                                if (track.StartDate.Value.AddDays(forecastDay * -1) < startDate) {
                                    forecastDay = MyUtilities.Function.DaysNoSunDay(startDate,
                                                                                    track.StartDate.Value.AddDays(-1));
                                    if (!trackUpMachine.Real && forecastDay > 0)
                                        track.Process.Add(new Process(forecastDay, 2));
                                }
                                else {
                                    track.Process.Add(
                                        new Process(
                                            MyUtilities.Function.DaysNoSunDay(startDate,
                                                                              track.StartDate.Value.AddDays(forecastDay *
                                                                                                            -1)),
                                            0));
                                    if (!trackUpMachine.Real && track.ForecastDay > 0)
                                        track.Process.Add(new Process(track.ForecastDay, 2));
                                }
                                if (track.EndDate >= endDate) {
                                    track.Process.Add(
                                        new Process(
                                            MyUtilities.Function.DaysNoSunDay(track.StartDate.Value, endDate), 1));
                                }
                                else {
                                    track.Process.Add(
                                        new Process(
                                            MyUtilities.Function.DaysNoSunDay(track.StartDate.Value, track.EndDate), 1));
                                    track.Process.Add(
                                        new Process(
                                            MyUtilities.Function.DaysNoSunDay(track.EndDate.AddDays(1), endDate), 0));
                                }
                            }
                            //thuc te sx
                            var endTrackDate = track.EndDate < endDate ? endDate : track.EndDate;
                            var importById =
                                importProductions.Where(
                                    id =>
                                    id.ProductId == track.ProductId && id.MachineId == entity.MachineId &&
                                    id.MaterialUseDate >= track.StartDate &&
                                    id.MaterialUseDate <= endTrackDate)
                                                 .ToList();
                            if (!importById.Any())
                                goto add;
                            track.ProductionDay = importById.GroupBy(i => i.MaterialUseDate).Count();
                            track.ProductionQuantity = importById.Sum(i => i.Quantity);
                            var lastProduction = importById.LastOrDefault();
                            var process = new Process(0, 0);
                            var process2 = new Process(track.ForecastDay, 2);
                            foreach (var day in entity.Days) {
                                //if (day > lastProduction.MaterialUseDate.Day)
                                //    break;
                                var importByDay =
                                    importById.Where(
                                        id =>
                                        id.MaterialUseDate.Day == day && id.MaterialUseDate.Month == month)
                                              .ToList();
                                if (importByDay.Any()) {
                                    if (process.Type == 0) {
                                        if (process.Day > 0) {
                                            if (trackUpMachine.Real && !track.Production.Any()) {
                                                if (process2.Day > process.Day) {
                                                    process2.Day = process.Day;
                                                    process.Day = 0;
                                                }
                                                else {
                                                    process.Day -= process2.Day;
                                                }
                                                track.Production.Add(process);
                                                track.Production.Add(process2);
                                            }
                                            else
                                                track.Production.Add(process);
                                        }
                                        process = new Process(0, 3);
                                    }
                                    //process.Quantity += importByDay.Sum(id => id.Quantity);
                                }
                                else {
                                    if (process.Type != 0) {
                                        if (process.Day > 0)
                                            track.Production.Add(process);
                                        process = new Process(0, 0);
                                    }
                                }
                                process.Day++;

                            }
                            if (process.Day != 0) {
                                if (lastProduction.MaterialUseDate < track.EndDate &&
                                    track.ProductionQuantity < track.Quantity) {
                                    var moreDay = MyUtilities.Function.RoundUp(
                                        (track.Quantity - track.ProductionQuantity) /
                                        MyUtilities.Product.GetProductionRateInFactoryDayTime(track.RealProductivity));
                                    track.MoreDay = moreDay;
                                    if (moreDay > process.Day)
                                        moreDay = process.Day;
                                    process2 = new Process(moreDay, 4);
                                    track.Production.Add(process2);
                                    process.Day -= moreDay;
                                }
                                track.Production.Add(process);
                            }
                        add:
                            //track.ProductionQuantity = track.Production.Sum(p => p.Quantity);
                            entity.Tracks.Add(track);
                        }
                        var onMachine = smartProduction.FirstOrDefault(sp => sp.MachineId == entity.MachineId);
                        if (onMachine != null) {
                            var track = entity.Tracks.FirstOrDefault(t => t.ProductId == onMachine.ProductId);
                            if (track != null) {
                                entity.ActiveTrack = track;
                            }
                        }
                        model.Add(entity);
                    }
                }
                return PartialView("MachineProcessDiv", model);
            }
            catch (Exception) {
                return null;
            }

        }


        #endregion

        #region  Section

        [GridAction]
        public ActionResult SelectSectionManagement() {
            var model = new List<SectionModel>();
            try {
                model = GetSectionList();
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectSectionManagement", ex.Message);
            };
            return View(new GridModel(model));
        }

        List<SectionModel> GetSectionList() {
            var model = new List<SectionModel>();
            using (var vfi = new vfiContext()) {
                var sections = from s in vfi.Sections
                               select s;
                foreach (var section in sections) {
                    var entity = new SectionModel {
                        SectionId = section.SectionId,
                        Active = section.Active,
                        ModifiedDate = section.ModifiedDate,
                        ModifiedUser = section.ModifiedUser,
                        SaleFactor = section.SaleFactor,
                        SectionName = section.SectionName,
                    };
                    model.Add(entity);
                }
            }
            return model;
        }

        [GridAction]
        public ActionResult InsertSection(SectionModel insert) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var entity = new Section() {
                        Active = true,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        SaleFactor = insert.SaleFactor,
                        SectionName = insert.SectionName,
                    };
                    vfi.Sections.Add(entity);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertSection", ex.Message);
            }
            return View(new GridModel(GetSectionList()));
        }

        [GridAction]
        public ActionResult UpdateSection(SectionModel update) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var entity = vfi.Sections.FirstOrDefault(pt => pt.SectionId == update.SectionId);
                    if (entity == null)
                        throw new AggregateException("Lỗi! Không tìm thấy gia công.");
                    entity.Active = update.Active;
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedUser = HttpContext.User.Identity.Name;
                    entity.SaleFactor = update.SaleFactor;
                    entity.SectionName = update.SectionName;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("UpdateSection", ex.Message);
            }
            return View(new GridModel(GetSectionList()));
        }
        #endregion

        #region material plan

        [GridAction]
        public ActionResult SelectMaterialLimitPlan(string date, int materialId, int productId) {
            var model = new List<MaterialLimitPlanModel>();
            try {
                model = GetMaterialLimitPlan(date, materialId, productId);
            }
            catch (Exception ex) {
                ModelState.AddModelError("SelectMaterialLimitPlan", ex.Message);
            }
            return View(new GridModel(model));
        }

        List<MaterialLimitPlanModel> GetMaterialLimitPlan(string date, int materialId, int productId) {
            var model = new List<MaterialLimitPlanModel>();
            using (var vfi = new vfiContext()) {
                var ci = new CultureInfo("vi-VN");
                var filterDate = string.IsNullOrWhiteSpace(date)
                                      ? DateTime.Today
                                      : Convert.ToDateTime(date, ci);
                var materialLimitPlans = vfi.MaterialLimitPlans;
                foreach (var plan in materialLimitPlans) {
                    var entity = new MaterialLimitPlanModel {
                        LimitId = plan.LimitId,
                        ApplyDate = plan.ApplyDate,
                        ModifiedDate = plan.ModifiedDate,
                        ModifiedUser = plan.ModifiedUser,

                        ProductId = plan.ProductId,
                        ProductCode = plan.Product.ProductCode,
                        MaterialId = plan.MaterialId,
                        MaterialCode = plan.Material.MaterialCode,
                        MaterialType = plan.Material.MaterialType.MaterialTypeName,

                        ProductLimitQuantity = plan.ProductLimitQuantity,
                        RawProductionWeight = plan.RawProductionWeight,
                        EstimateLossPercent = plan.EstimateLossPercent,
                        WorkpieceLossPercent = plan.WorkpieceLossPercent,
                        ExpiredDay = plan.ExpiredDay,
                        IsLock = plan.IsLock
                    };
                    model.Add(entity);
                }

            }
            return model;
        }

        [GridAction]
        public ActionResult InsertMaterialLimitPlan(MaterialLimitPlanModel newModel, string date, int materialId, int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var msg = "";
                    var productIdNew = 0;
                    try {
                        productIdNew = Convert.ToInt32(newModel.ProductCode);
                    }
                    catch (FormatException) {
                        productIdNew = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(newModel.ProductCode))
                                       .ProductId;
                    }
                    var materialIdNew = 0;
                    try {
                        materialIdNew = Convert.ToInt32(newModel.MaterialCode);
                    }
                    catch (FormatException) {
                        materialIdNew = vfi.Materials.FirstOrDefault(p => p.MaterialCode.Equals(newModel.MaterialCode))
                                       .MaterialId;
                    }
                    if (productIdNew == 0) {
                        msg += "Vui lòng chọn mã sản phẩm \n";
                    }
                    if (materialIdNew == 0) {
                        msg += "Vui lòng chọn nguyên liệu \n";
                    }
                    if (newModel.ProductLimitQuantity == 0) {
                        msg += "Vui lòng điền số lượng hạn mức";
                    }
                    if (!string.IsNullOrWhiteSpace(msg)) {
                        throw new AggregateException(msg);
                    }

                    var lastPlan = vfi.MaterialLimitPlans
                                        .FirstOrDefault(ml => ml.MaterialId == materialIdNew &&
                                                                ml.ProductId == productIdNew &&
                                                                ml.IsLock);
                    if (lastPlan != null) {
                        if (newModel.ApplyDate <= lastPlan.ApplyDate)
                            throw new AggregateException("Lỗi! Kế hoạch mới nhỏ ngày hơn kế hoạch cũ");
                        lastPlan.IsLock = false;
                    }
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productIdNew);
                    if (product == null)
                        throw new AggregateException("Lỗi! Không tìm thấy sản phẩm");
                    newModel.EstimateLossPercent = 10;
                    newModel.RawProductionWeight = MyUtilities.Product
                            .GetProductWeight(product.Material.MaterialName,
                                product.Material.OutDiameter,
                                product.Material.InDiameter,
                                product.Length ?? 0,
                                product.KnifeCut ?? 0,
                                product.Material.Shape + "");
                    var plan = new MaterialLimitPlan {
                        ProductId = productIdNew,
                        MaterialId = materialIdNew,
                        ApplyDate = newModel.ApplyDate.Value,
                        MaterialLimitQuantity = newModel.MaterialLimitQuantity,
                        ProductLimitQuantity = newModel.ProductLimitQuantity,
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = HttpContext.User.Identity.Name,
                        Productivity = product.Productivity ?? 0,
                        WorkpieceLossPercent = newModel.WorkpieceLossPercent,
                        EstimateLossPercent = newModel.EstimateLossPercent,
                        RawProductionWeight = newModel.RawProductionWeight,
                        ExpiredDay = 180,
                        IsLock = true,
                    };
                    vfi.MaterialLimitPlans.Add(plan);
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMaterialLimitPlan", ex.Message);
            }
            return View(new GridModel(GetMaterialLimitPlan(date, materialId, productId)));
        }

        [GridAction]
        public ActionResult UpdateMaterialLimitPlan(MaterialLimitPlanModel newModel, string date, int materialId, int productId) {
            try {
                if (!Request.IsAuthenticated) {
                    throw new AggregateException("Bạn đã bị mất quyền đăng nhập. \r\n 1 trong các nguyên nhân như mất thời gian chờ. \r\n Xin vui lòng đăng nhập lại hệ thống.");
                }
                using (var vfi = new vfiContext()) {
                    var msg = "";
                    var productIdNew = 0;
                    try {
                        productIdNew = Convert.ToInt32(newModel.ProductCode);
                    }
                    catch (FormatException) {
                        productIdNew = vfi.Products.FirstOrDefault(p => p.ProductCode.Equals(newModel.ProductCode))
                                       .ProductId;
                    }
                    var materialIdNew = 0;
                    try {
                        materialIdNew = Convert.ToInt32(newModel.MaterialCode);
                    }
                    catch (FormatException) {
                        materialIdNew = vfi.Materials.FirstOrDefault(p => p.MaterialCode.Equals(newModel.MaterialCode))
                                       .MaterialId;
                    }
                    if (productIdNew == 0) {
                        msg += "Vui lòng chọn mã sản phẩm \n";
                    }
                    if (materialIdNew == 0) {
                        msg += "Vui lòng chọn nguyên liệu \n";
                    }
                    if (newModel.ProductLimitQuantity == 0) {
                        msg += "Vui lòng điền số lượng hạn mức";
                    }
                    if (!string.IsNullOrWhiteSpace(msg)) {
                        throw new AggregateException(msg);
                    }

                    var lastPlan = vfi.MaterialLimitPlans
                                        .FirstOrDefault(ml => ml.MaterialId == materialIdNew &&
                                                                ml.ProductId == productIdNew &&
                                                                ml.LimitId != newModel.LimitId &&
                                                                ml.IsLock);
                    if (lastPlan != null) {
                        if (newModel.ApplyDate <= lastPlan.ApplyDate)
                            throw new AggregateException("Lỗi! Kế hoạch mới nhỏ ngày hơn kế hoạch cũ");
                        lastPlan.IsLock = false;
                    }
                    var product = vfi.Products.FirstOrDefault(p => p.ProductId == productIdNew);
                    if (product == null)
                        throw new AggregateException("Lỗi! Không tìm thấy sản phẩm");
                    newModel.EstimateLossPercent = 10;
                    newModel.RawProductionWeight = MyUtilities.Product
                            .GetProductWeight(product.Material.MaterialName,
                                product.Material.OutDiameter,
                                product.Material.InDiameter,
                                product.Length ?? 0,
                                product.KnifeCut ?? 0,
                                product.Material.Shape + "");
                    var plan = vfi.MaterialLimitPlans.FirstOrDefault(ml => ml.LimitId == newModel.LimitId);
                    if (plan == null)
                        throw new AggregateException("Lỗi! Không tìm thấy hạn mức");
                    if (!plan.IsLock)
                        throw new AggregateException("Lỗi! Hạn mức không còn được sử dụng.");
                    plan.ProductId = productIdNew;
                    plan.MaterialId = materialIdNew;
                    plan.ApplyDate = newModel.ApplyDate.Value;
                    plan.MaterialLimitQuantity = newModel.MaterialLimitQuantity;
                    plan.ProductLimitQuantity = newModel.ProductLimitQuantity;
                    plan.ModifiedDate = DateTime.Now;
                    plan.ModifiedUser = HttpContext.User.Identity.Name;
                    plan.Productivity = product.Productivity ?? 0;
                    plan.WorkpieceLossPercent = newModel.WorkpieceLossPercent;
                    plan.EstimateLossPercent = newModel.EstimateLossPercent;
                    plan.IsLock = newModel.IsLock;
                    vfi.SaveChanges();
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("InsertMaterialLimitPlan", ex.Message);
            }
            return View(new GridModel(GetMaterialLimitPlan(date, materialId, productId)));
        }



        #endregion

    }
}
