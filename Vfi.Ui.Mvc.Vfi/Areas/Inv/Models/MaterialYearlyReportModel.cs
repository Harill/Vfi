using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Models
{
    public class MaterialYearlyReportModel
    {
        public int MaterialId { get; set; }
        public string MaterialName{get; set;}
        public string MaterialCode { get; set; }
        public int MaterialTypeId { get; set; }
        public int Year { get; set; }
        public string MaterialTypeName { get; set; }
        public double TotalInv { get; set; }
        public double Avg { get; set; }
        public double TotalExport3MonthUsed { get; set; }

        public List<MaterialMonthlyReportModel> Months { get; set; }
    }

    public class MaterialMonthlyReportModel
    {
        public int MaterialId { get; set; }
        public double Import { get; set; }
        public double ImportMore { get; set; }
        public double ExportUse { get; set; }
        public double ExportDestroy { get; set; }
        public double Export3MonthUsed { get; set; }

    }

    public class MaterialYearlyDetailReportModel {
        public MaterialYearlyDetailReportModel() {
            Productions = new List<MaterialProductionModel>();
        }
        public int MaterialId { get; set; }
        public string MaterialCode { get; set; }

        public int ProductId { get; set; }
        public double ProductWeight { get; set; }
        public string ProductCode { get; set; }
        public double Production {
            get { return Productions.Sum(p => p.Production); }
        }
        public double Processing {
            get { return Productions.Sum(p => p.Processing); }
        }
        public double Defect {
            get { return Productions.Sum(p => p.Defect); }
        }

        public double MonthCount { get { return Productions.Count(p => p.MaterialUse > 0); } }
        public double MaterialUse {
            get { return Productions.Sum(p => p.MaterialUse); }
        }
        public double AvgMaterialUse {
            get {
                return MonthCount > 0 ? MaterialUse / MonthCount : 0;
            }
        }

        public double AvgMaterialUse3 {
            get {
                return AvgMaterialUse * 3;
            }
        }
        public List<MaterialProductionModel> Productions { get; set; }
        public string Note { get; set; }
    }
    public class MaterialProductionModel {
        public int Month { get; set; }
        public int Year { get; set; }
        public double MaterialUse { get; set; }
        public double Production { get; set; }
        public double Processing { get; set; }
        public double Defect { get; set; }
    }
}