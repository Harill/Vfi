using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Utilities;

namespace Vfi.Ui.Mvc.Vfi.Areas.Factory.Models {
    public class MachineDiagram {
        public MachineDiagram() {
            Production = 0;
            MaterialUse = 0;
            RunDate = DateTime.Now;
            ProductImgs = new List<ProductImgModel>();
            QcImgs = new List<QcImgModel>();
        }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public string MachineIdName { get; set; }               // moi them
        public int ProductId { get; set; }
        [UIHint("_ProductCodeNameTemplate")]
        public string ProductCode { get; set; }
        public string MaterialCode { get; set; }
        public string Information { get; set; }
        public int MachineState { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public DateTime RunDate { get; set; }
        public int Seconds { get; set; }
        [DataType("Number")]
        public double Production { get; set; }
        public string ProductionStr { get { return string.Format("{0:n0}", Production); } }
        [DataType("Number")]
        public double MaterialUse { get; set; }
        public string MaterialUseStr { get { return string.Format("{0:n0}", MaterialUse); } }
        public double Productivity { get; set; }
        public int MachineFunction { get; set; }
        [DataType("Number")]
        public double ProductionPerDay {
            get {
                return MyUtilities.Product.GetProductionRateInFactoryDayTime(Productivity);
                //return MyUtilities.Product.GetProductionRateInTime(MyUtilities.Product.Second20h, Productivity);
            }
        }
        public string ProductionPerDayStr { get { return string.Format("{0:n0}", ProductionPerDay); } }
        [UIHint("_DateTemplateNullable")]
        public DateTime? StartDate { get; set; }
        [UIHint("_DateTemplateNullable")]
        public DateTime? EndDate { get; set; }

        public string StartDateString {
            get { return StartDate != null ? StartDate.Value.ToString("dd/MM/yyyy") : "__/__/____"; }
        }

        public string EndDateString {
            get { return EndDate != null ? EndDate.Value.ToString("dd/MM/yyyy") : "__/__/____"; }
        }
        public string Note { get; set; }
        public double ForecastsQuality { get; set; }
        public string ForecastsQualityStr { get { return string.Format("{0:n0}", ForecastsQuality); } }
        public string WarrningColor { get; set; }

        public string MachineType { get; set; }

        public string GetLogNote() {
            string a = "Lên máy: " + ProductCode
                //+ ", số lượng: " + Number + ", năng xuất:" + ProductionPerDay
                       + ", ngày bắt đầu:" + StartDate.Value.ToString("dd/MM/yyyy");
            //+ ", ngày kết thúc:" + EndDate.Value.AddDays(GetRunDay()).ToString("dd/MM/yyyy");
            if (!string.IsNullOrWhiteSpace(Note))
                a += ", ghi chú:" + Note;
            return "";
        }

        public string GetDiagramName(Machine machine) {
            string a = "";
            if (machine != null)
                a = MyUtilities.Machine.GetDiagramText(machine.DiagramType ?? 0) + "-" +
                    machine.ColumnIndex + "-" + machine.RowIndex;
            return a;
        }

        public int DiagramType { get; set; }
        public string DiagramName { get; set; }

        public List<string> Notes { get; set; }
        public List<ProductImgModel> ProductImgs { get; set; }
        public List<QcImgModel> QcImgs { get; set; }

        public double ProcessingQuantity { get; set; }
        public string ProcessingQuantityStr { get { return string.Format("{0:n0}", ProcessingQuantity); } }
        public double DefectQuantity { get; set; }
        public string DefectQuantityStr { get { return string.Format("{0:n0}", DefectQuantity); } }
        public int WarehouseId { get; set; }
    }
    public class MachineTypeModel {
        public string Code { get; set; }
        public int Count { get; set; }
    }
    public class MachineFunctionModel {
        public string Code { get; set; }
        public int Count { get; set; }
    }
    public class GroupMachineDiagram {
        public GroupMachineDiagram() {
            ListMachineDiagram = new List<List<MachineDiagram>>();
            ListState = new List<MachineStateModel>();
            ListFunction = new List<MachineFunctionModel>();
            ListType = new List<MachineTypeModel>();
        }
        public List<List<MachineDiagram>> ListMachineDiagram { get; set; }
        public List<MachineStateModel> ListState { get; set; }
        public List<MachineFunctionModel> ListFunction { get; set; }
        public List<MachineTypeModel> ListType { get; set; }
    }
}