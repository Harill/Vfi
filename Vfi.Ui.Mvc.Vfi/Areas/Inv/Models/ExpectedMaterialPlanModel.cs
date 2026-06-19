
using System.Collections.Generic;
using System.Linq;
using Vfi.Ui.Mvc.Vfi.Utilities;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Models
{
    public class ExpectedMaterialPlanModel {
        public ExpectedMaterialPlanModel() {
            Details = new List<ExpectedProductionMaterialPlanModel>();
        }
        public int Index { get; set; }
        public string MaterialTypeName { get; set; }
        public int MaterialId { get; set; }
        public double MaterialWeight { get; set; }
        public double ProductWeight { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public double OutDiameter { get; set; }
        public string DiameterType { get; set; }
        public double InDiameter { get; set; }
        public string Shape { get; set; }

        public double TotalInv { get; set; }
        public double TotalInvKg { get; set; }

        public double OnPurchaseQuantity { get; set; }
        public int MonthCount { get; set; }

        public List<ExpectedProductionMaterialPlanModel> Details { get; set; }

        public double UseInMonthKg { get { return Details.Sum(x => x.UseInMonthKg); } }
        public double UseAvgInYearKg { get { return Details.Sum(x => x.UseAvgInYearKg); } }
        public double OrderRemaining { get { return Details.Sum(x => x.OrderRemaining); } }
        public double OrderRemainingKg { get { return Details.Sum(x => x.OrderRemainingKg); } }

        public double OrderLastYear { get { return Details.Sum(x => x.OrderLastYear); } }
        public double OrderLastYearKg { get { return Details.Sum(x => x.OrderLastYearKg); } }

        public double ForecastInYearKg { get { return Details.Sum(d => d.ForecastInYearKg); } }

        public double RequireInYearKg {
            get {
                return ForecastInYearKg > TotalInvKg
                           ? ForecastInYearKg - TotalInvKg
                           : 0;
            }
        }
        public double ForecastByMonthKg { get { return Details.Sum(d => d.ForecastByMonthKg); } }

        public double ProductRequireByMonthKg { get { return Details.Sum(d => d.RequireByMonthKg); } }

        public double RequireByMonthKg {
            get {
                return ProductRequireByMonthKg > TotalInvKg
                         ? ProductRequireByMonthKg - TotalInvKg
                         : 0;
            }
        }

        public double ForecastInMonthKg { get { return Details.Sum(d => d.ForecastInMonthKg); } }
        public double RequireInMonthKg {
            get {
                return ForecastInMonthKg > TotalInvKg
                         ? ForecastInMonthKg - TotalInvKg
                         : 0;
            }
        }
        public double ForecastNextMonthKg { get { return Details.Sum(d => d.ForecastNextMonthKg); } }
        public double RequireNextMonthKg {
            get {
                return ForecastNextMonthKg > (TotalInvKg - ForecastInMonthKg)
                         ? ForecastNextMonthKg - (TotalInvKg - ForecastInMonthKg)
                         : 0;
            }
        }
        public double TotalRequireKg { get { return RequireInMonthKg + RequireNextMonthKg; } }

        public int ExpectedProductionDay {
            get {
                return Details.Any(x => x.MaxUseInMonthKg > 0)
                    ? MyUtilities.Function.Round(TotalInvKg / Details.Sum(x => x.MaxUseInMonthKg))
                    : 0;
            }
        }

        public bool HasProductDuplicate { get; set; }
    }

    public class ExpectedProductionMaterialPlanModel
    {
        public ExpectedProductionMaterialPlanModel()
        {
            UnitWeightByMaterial = 0;
        }
        public int Index { get; set; }
        public string MaterialTypeName { get; set; }
        public int MaterialId { get; set; }
        public double MaterialWeight { get; set; }
        public double ProductWeight { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public double OutDiameter { get; set; }
        public string DiameterType { get; set; }
        public double InDiameter { get; set; }
        public string Shape { get; set; }
        //public string GetDesignNoLenght()
        //{
        //    string a = MaterialName;
        //    if (InDiameter == 0)
        //    {
        //        a += "-(" + Shape.Trim() + ")" + OutDiameter + "-" + DiameterType.Trim();
        //    }
        //    else
        //    {
        //        a += "-(" + Shape.Trim() + ")(" + OutDiameter +
        //            "x" + (InDiameter) + ")-" + DiameterType.Trim();
        //    }
        //    return a;
        //}
        public double TotalInv { get; set; }
        public double TotalInvKg { get; set; }
        public int MonthCount { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public bool UseForecast { get; set; }
        public double UnitWeightByMaterial { get; set; }
        public bool Show { get { return RequireByMonthKg + UseInMonthKg > 0; } }

        public double UseInMonthKg { get; set; }
        public double MaxUseInMonthKg { get; set; }
        public double UseAvgInYearKg { get; set; }

        public double OrderRemaining { get; set; }
        public double OrderRemainingKg {
            get { return OrderRemaining * UnitWeightByMaterial / 1000; }
        }
        public double OrderLastYear { get; set; }
        public double OrderLastYearKg {
            get { return OrderLastYear * UnitWeightByMaterial / 1000; }
        }

        public double ForecastInYear { get; set; }
        public double ForecastInYearKg
        {
            get { return ForecastInYear*UnitWeightByMaterial/1000; }
        }

        public double RequireInYear { get; set; }
        public double RequireInYearKg
        {
            get { return RequireInYear * UnitWeightByMaterial / 1000; }
        }

        public double ForecastByMonth { get; set; }
        public double ForecastByMonthKg
        {
            get { return ForecastByMonth * UnitWeightByMaterial / 1000; }
        }
        public double ForecastInMonth { get; set; }
        public double ForecastInMonthKg {
            get { return ForecastInMonth * UnitWeightByMaterial / 1000; }
        }
        public double ForecastNextMonth { get; set; }
        public double ForecastNextMonthKg {
            get { return ForecastNextMonth * UnitWeightByMaterial / 1000; }
        }
        public double RequireByMonth { get; set; }
        public double RequireByMonthKg {
            get { return RequireByMonth * UnitWeightByMaterial / 1000; }
        }

        //public double OrderInMonth { get; set; }
        //public double OrderInMonthKg
        //{
        //    get { return OrderInMonth * UnitWeightByMaterial / 1000; }
        //}
        public double RequireInMonth { get; set; }
        public double RequireInMonthKg {
            get { return RequireInMonth * UnitWeightByMaterial / 1000; }
        }

        //public double OrderNextMonth { get; set; }
        //public double OrderNextMonthKg
        //{
        //    get { return OrderNextMonth * UnitWeightByMaterial / 1000; }
        //}
        //public double RequireNextMonth { get; set; }
        //public double RequireNextMonthKg
        //{
        //    get { return RequireNextMonth * UnitWeightByMaterial / 1000; }
        //}
        public double TotalRequire { get; set; }
        public double TotalRequireKg {
            get { return TotalRequire * UnitWeightByMaterial / 1000; }
        }
        public double OnPoKg { get; set; }
        public double RequirePoKg { get; set; }

        public bool IsDuplicate { get; set; }

        public double SaleInMonth { get; set; }
    }
}