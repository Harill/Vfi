using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Vfi.Ui.Mvc.Vfi.Utilities;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Models {
    public class ProductState {
        public ProductState() {
            Details = new List<OrderProgressDetailModel>();
            DetailInMonths = new List<OrderProgressDetailModel>();
            Productions = new List<OrderProgressProduction>();
            FinishProgress = new OrderProgressInventory();
            QcProgress = new OrderProgressInventory();
            PlatingProgress = new OrderProgressInventory();
            WaitingPlatingProgress = new OrderProgressInventory();
            SurfaceProgress = new OrderProgressInventory();
            HeatProgress = new OrderProgressInventory();
            Production2Progress = new OrderProgressInventory();
            CncProgress = new OrderProgressInventory();
        }
        public int Index { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string CustomerCode { get; set; }
        public double ExportInMonth { get; set; }
        public double RequirementInventory { get; set; }
        public double RequirementInvStatus { get; set; }
        public double ProductionForecasts { get; set; }
        public double ProductionForecastsStatus { get; set; }
        public double OrderInMonth { get; set; }
        public int OrderStatus { get; set; } // 1 red; 2 yello; 3 green
        public double RequirementOrder { get; set; }
        public double RequirementOrderInMonth { get; set; }
        public double RequirementOrderNextMonth { get; set; }
        public double RequirementOrderStatus { get; set; }

        public double QuantityNeedProduction { get; set; }
        [UIHint("_DateTemplate")]
        public DateTime? EnoughMaterialDate { get; set; }
        [UIHint("_DateTemplate")]
        public DateTime? StartProductionDate { get; set; }
        [UIHint("_DateTemplate")]
        public DateTime? FinishDate { get; set; }

        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public double MaterialInv { get; set; }
        public int MaterialStatus { get; set; }
        //tong ton
        public double TotalInv { get; set; }

        //thang pham
        public OrderProgressInventory FinishProgress { get; set; }
        public double FinishInv { get; set; }
        //qc
        public OrderProgressInventory QcProgress { get; set; }
        public double QcInv { get; set; }
        public int QcPendingDay { get { return 2; } }
        public double AfterQcInv { get { return FinishInv; } }
        public double AfterQcProcessInv { get { return FinishProgress.Inv; } }
        //ncu
        public OrderProgressInventory PlatingProgress { get; set; }
        public double PlatingInv { get; set; }
        public double AfterPlatingInv { get { return AfterQcInv + QcInv; } }
        public double AfterPlatingProcessInv { get { return AfterQcProcessInv + QcProgress.Inv; } }
        public int PlatingPendingDay { get; set; }
        //cho gcn
        public OrderProgressInventory WaitingPlatingProgress { get; set; }
        public double WaitingPlatingInv { get; set; }
        public double AfterWaitingPlatingInv { get { return AfterPlatingInv + PlatingInv; } }
        public double AfterWaitingPlatingProcessInv { get { return AfterPlatingProcessInv + PlatingProgress.Inv; } }
        public int WaitingPlatingPendingDay { get; set; }

        //rung bong
        public OrderProgressInventory SurfaceProgress { get; set; }
        public double SurfaceInv { get; set; }
        public double AfterSurfaceInv { get { return AfterWaitingPlatingInv + WaitingPlatingInv; } }
        public double AfterSurfaceProcessInv { get { return AfterWaitingPlatingProcessInv + WaitingPlatingProgress.Inv; } }
        public int SurfacePendingDay { get; set; }
        // nhiet luyen
        public OrderProgressInventory HeatProgress { get; set; }
        public double HeatInv { get; set; }
        public double AfterHeatInv { get { return AfterSurfaceInv + SurfaceInv; } }
        public double AfterHeatProcessInv { get { return AfterSurfaceProcessInv + SurfaceProgress.Inv; } }
        public int HeatPendingDay { get; set; }
        // san xuat 2
        public OrderProgressInventory Production2Progress { get; set; }
        public double Production2Inv { get; set; }
        public double AfterProduction2Inv { get { return AfterHeatInv + HeatInv; } }
        public double AfterProduction2ProcessInv { get { return AfterHeatProcessInv + HeatProgress.Inv; } }
        public double SmallestProduction2Productivity { get; set; }
        public int Production2PendingDay { get; set; }

        public double Production2InDay {
            get {
                return MyUtilities.Product.GetProductionRateInDayTime(SmallestProduction2Productivity);
                //return SmallestProduction2Productivity > 0
                //           ? Math.Round(MyUtilities.Product.Second7_5h / SmallestProduction2Productivity, 0)
                //           : 0;
            }
        }
        // cnc
        public OrderProgressInventory CncProgress { get; set; }
        public double CncInv { get; set; }
        public double AfterCncInv { get { return AfterProduction2Inv + Production2Inv; } }
        public double AfterCncProcessInv { get { return AfterProduction2ProcessInv + Production2Progress.Inv; } }
        public double CncProductivity { get; set; }

        public double CncInDay {
            get {
                return MyUtilities.Product.GetProductionRateInFactoryDayTime(Productivity);
                //return CncProductivity > 0
                //           ? Math.Round(MyUtilities.Product.Second20h / CncProductivity, 0)
                //           : 0;
            }
        }
        //sx1
        public double ProductionRequirement { get; set; }
        public double Productivity { get; set; }
        public double ProductivityInDay {
            get {
                return MyUtilities.Product.GetProductionRateInFactoryDayTime(Productivity * 0.7);
                //return Productivity > 0
                //           ? MyUtilities.Product.Second20h / (Productivity*0.7)
                //           : 0;
            }
        }
        public double RequireProductionQuantity {
            get {
                return (ProductionRequirement > 0)
                           ? FirstOrderDate > DateTime.Now
                                 ? ProductionRequirement * 1.2 / MyUtilities.Function.Days(DateTime.Now, FirstOrderDate)
                                 : ProductionRequirement * 1.2 / 26
                           : 0;
            }
        }
        public double AfterProductionInv { get { return AfterCncInv + CncInv; } }
        public double AfterProductionProcessInv { get { return AfterCncProcessInv + CncProgress.Inv; } }
        public int CncPendingDay { get; set; }

        public double Average { get; set; }
        public int ProductionStatus { get; set; }
        public string ProductionDate { get; set; }
        public int ProductionDateStatus { get; set; }
        public DateTime? ProductionLast { get; set; }
        public string ProductionLastDate { get; set; }
        public DateTime? ProductionFirst { get; set; }
        public string ProductionFirstDate { get; set; }
        public double MachineRun { get; set; }
        public double ProductionQuantity { get { return Productions.Sum(p => p.LastQuantity); } }
        // cho xl
        public double ProcessingInv { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FirstOrderDate { get; set; }
        public string InMonthDateString { get; set; }
        public string NextMonthDateString { get; set; }
        public double AveragePerformance { get; set; }
        public double RealPerformance { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        //public int PendingDay { get; set; }
        public List<OrderProgressDetailModel> Details { get; set; }
        public List<OrderProgressDetailModel> DetailInMonths { get; set; }
        public List<OrderProgressProduction> Productions { get; set; }
        public int Type { get; set; }
        public string Width {
            get { return Details.Count > 0 ? Math.Round(100.00 / Details.Count, 2) - 0.05 + "%" : "99.95%"; }
        }
    }
    public class OrderProgressDetailModel {
        public OrderProgressDetailModel() {
            ProductionProgress = new OrderProgressInventory();
            CncProgress = new OrderProgressInventory();
            Production2Progress = new OrderProgressInventory();
            SurfaceProgress = new OrderProgressInventory();
            HeatProgress = new OrderProgressInventory();
            QcProgress = new OrderProgressInventory();
            PlatingProgress = new OrderProgressInventory();
            WaitingPlatingProgress = new OrderProgressInventory();
            StartDate = new DateTime();
            CompleteDate = new DateTime();
            LateOrders = new List<LateOrder>();
        }
        public int Index { get; set; }
        // order
        public long OrderDetailId { get; set; }
        public int ForecastOrderId { get; set; }
        public DateTime OrderDueDate { get; set; }
        public string ProcessName { get; set; }
        public double TotalInv { get; set; }
        public string TotalInvString {
            get { return TotalInv > 0 ? string.Format("{0:n0}", TotalInv) : ""; }
        }
        public double WarehouseInv { get; set; }
        public string WarehouseInvString {
            get { return WarehouseInv > 0 ? string.Format("{0:n0}", WarehouseInv) : ""; }
        }
        public double OrderQuantity { get; set; }
        public string OrderDueDateString { get; set; }
        public double OrderRequired { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        //sx1
        public double MachineRun { get; set; }
        [DataType("_DateTemplate")]
        public DateTime? StartDate { get; set; }
        public string StartDateString { get; set; }
        //public double PendingDayAfterProduction1()
        //{
        //    return QcProgress.PendingDay + PlatingProgress.PendingDay + WaitingPlatingProgress.PendingDay +
        //           SurfaceProgress.PendingDay + HeatProgress.PendingDay +
        //           Production2Progress.PendingDay + CncProgress.PendingDay;
        //}
        // thanh pham
        public int FinishPendingDay { get { return 1; } }
        public int BeforeFinishPendingDay {
            get { return BeforeQcPendingDay + FinishPendingDay; }
        }
        //qc
        public OrderProgressInventory QcProgress { get; set; }
        public int QcPendingDay { get { return 2; } }
        public int AfterQcPendingDay { get { return FinishPendingDay; } }
        public int BeforeQcPendingDay {
            get { return BeforePlatingPendingDay + QcPendingDay; }
        }
        //ncu
        public OrderProgressInventory PlatingProgress { get; set; }
        public int PlatingPendingDay { get; set; }
        public int AfterPlatingPendingDay {
            get {
                return AfterQcPendingDay + QcPendingDay;
            }
        }
        public int BeforePlatingPendingDay {
            get { return BeforeWaitingPlatingPendingDay + PlatingPendingDay; }
        }
        //cho gcn
        public OrderProgressInventory WaitingPlatingProgress { get; set; }
        public int WaitingPlatingPendingDay { get; set; }
        public int AfterWaitingPlatingPendingDay {
            get {
                return AfterPlatingPendingDay + PlatingPendingDay;
            }
        }
        public int BeforeWaitingPlatingPendingDay {
            get { return BeforeSurfacePendingDay + WaitingPlatingPendingDay; }
        }
        //rung bong
        public OrderProgressInventory SurfaceProgress { get; set; }
        public int SurfacePendingDay { get; set; }
        public int AfterSurfacePendingDay {
            get {
                return AfterWaitingPlatingPendingDay + WaitingPlatingPendingDay;
            }
        }
        public int BeforeSurfacePendingDay {
            get { return BeforeHeatPendingDay + SurfacePendingDay; }
        }
        // nhiet luyen
        public OrderProgressInventory HeatProgress { get; set; }
        public int HeatPendingDay { get; set; }
        public int AfterHeatPendingDay {
            get {
                return AfterSurfacePendingDay + SurfacePendingDay;
            }
        }
        public int BeforeHeatPendingDay {
            get { return BeforeProduction2PendingDay + HeatPendingDay; }
        }
        // san xuat 2
        public OrderProgressInventory Production2Progress { get; set; }
        public double SmallestProduction2Productivity { get; set; }
        public double Production2InDay {
            get {
                return MyUtilities.Product.GetProductionRateInDayTime(SmallestProduction2Productivity);
                //return SmallestProduction2Productivity > 0
                //           ? Math.Round(MyUtilities.Product.Second7_5h / SmallestProduction2Productivity, 0)
                //           : 0;
            }
        }
        public int Production2PendingDay2 { get; set; }
        public int Production2PendingDay {
            get {
                return Production2InDay > 0
                           ? MyUtilities.Function.RoundUp(Production2Progress.Requirement / Production2InDay)
                           : 0;
            }
        }
        public int AfterProduction2PendingDay {
            get {
                return AfterHeatPendingDay + HeatPendingDay;
            }
        }
        public int BeforeProduction2PendingDay {
            get { return BeforeCncPendingDay + Production2PendingDay2; }
        }
        // cnc
        public OrderProgressInventory CncProgress { get; set; }
        public double CncProductivity { get; set; }
        public double CncInDay {
            get {
                return MyUtilities.Product.GetProductionRateInFactoryDayTime(CncProductivity);
                //return Productivity > 0
                //           ? MyUtilities.Product.Second20h / CncProductivity
                //           : 0;
            }
        }
        public int CncPendingDay {
            get {
                return CncInDay > 0
                           ? MyUtilities.Function.RoundUp(ProductionProgress.Requirement / CncInDay)
                           : 0;
            }
        }
        public int AfterCncPendingDay {
            get {
                return AfterProduction2PendingDay + Production2PendingDay2;
            }
        }
        public int BeforeCncPendingDay {
            get { return ProductionPendingDay + CncPendingDay; }
        }
        // sx1
        public OrderProgressInventory ProductionProgress { get; set; }
        public double Productivity { get; set; }
        public double ProductivityInDay {
            get {
                return MyUtilities.Product.GetProductionRateInFactoryDayTime(Productivity);
                //return Productivity > 0
                //           ? MyUtilities.Product.Second20h/ Productivity
                //           : 0;
            }
        }
        public int ProductionPendingDay {
            get {
                return ProductivityInDay > 0
                           ? MyUtilities.Function.RoundUp(ProductionProgress.Requirement / (ProductivityInDay * 0.7))
                           : 0;
            }
        }
        public int AfterProductionPendingDay {
            get {
                return AfterCncPendingDay + CncPendingDay;
            }
        }

        public int ProductionDay { get; set; }
        [DataType("_DateTemplate")]
        public DateTime? CompleteDate { get; set; }
        public string CompleteDateString {
            get { return CompleteDate == null ? "" : CompleteDate.Value.ToString("dd/MM"); }
        }
        public int DiffDay { get; set; }

        public int TotalDay {
            get {
                return OrderDueDate > StartDate
                           ? Convert.ToInt32((OrderDueDate - StartDate).Value.TotalDays)
                           : 0;
            }
        }

        public int Day { get { return Convert.ToInt32((DateTime.Now - StartDate).Value.TotalDays); } }

        public int Percent { get; set; }
        public string OrderQuantityString {
            get { return OrderQuantity > 0 ? string.Format("{0:n0}", OrderQuantity) : ""; }
        }
        public bool WillLate { get; set; }
        public int Type {
            get {
                // đủ SL xanh lá
                if (Percent == 100) return -1;
                // đã trễ đỏ
                if (OrderDueDate <= DateTime.Now) return 2;
                ////sẽ trễ vàng
                if (WillLate) return 1;
                //bình thường xanh dương
                return 0;
            }
        }
        public string TypeString {
            get {
                switch (Type) {
                    case 0:
                        return "progress-bar-info";
                    case 1:
                        return "progress-bar-warning";
                    case 2:
                        return "progress-bar-danger";
                    default:
                        return "progress-bar-success";
                }
            }
        }
        public string RedAltert {
            get { return (OrderDueDate <= DateTime.Now && WarehouseInv <= 0) ? "background-color:#db4242;" : ""; }          //#ff1000
        }
        public List<LateOrder> LateOrders { get; set; }
    }

    public class LateOrder {
        public DateTime OrderDueDate { get; set; }
        public double OrderQuantity { get; set; }
        public string OrderDueDateString { get; set; }
        public long OrderDetailId { get; set; }
        public int Status { get; set; }
    }

    public class OrderProgressInventory {
        public OrderProgressInventory() {
            Inv = 0;
            Requirement = 0;
            Status = 0;
            DateStatus = 0;
            //PendingDay = 0;
            Export = 0;
        }

        public double Inv { get; set; }
        public double Requirement { get; set; }
        public double Export { get; set; }
        public int Status { get; set; }

        public string DateString {
            get { return Date == null ? "" : Date.Value.ToString("dd/MM"); }
        }

        public DateTime? DateRed {
            get { return Date == null ? null : new DateTime?(Date.Value.AddDays(7)); }
        }

        public DateTime? Date { get; set; }

        public void AddDate(int days, DateTime fromDate) {
            if (days < 0)
                Date = MyUtilities.Function.FromDate(fromDate, days);
            else if (days >= 0)
                Date = MyUtilities.Function.ToDate(fromDate, days);
        }

        public int DateStatus { get; set; }
        //public int PendingDay { get; set; }
        //public double Productivity { get; set; }
    }

    public class OrderProgressProduction {
        public string StartDateString { get; set; }
        public string LastDateString { get; set; }
        public string MachineName { get; set; }
        public double AverageByMachine { get; set; }
        public double LastQuantity { get; set; }
        public double RealPerformance { get; set; }
        public double AveragePerformance { get; set; }
        public double ProductivityInDay { get; set; }
    }
}