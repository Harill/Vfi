
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vfi.Server.Core.DataModel.Models.Inv;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Models
{
    public class ProductInventoryPeriodModel : ProductInventoryPeriodDomainModel
    {
        [DisplayName("Nhập (PCS)")]
        public double? QtyImport { get; set; }

        [DisplayName("Xuất (PCS)")]
        public double? QtyExport { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int? Idx { get; set; }

        [DisplayName("Mã Kh")]
        public string CustomerCode { get; set; }

        [DisplayName("Tong Nhập")]
        public double TotalImport { get; set; }
        [DisplayName("Tong xuat")]
        public double TotalExport { get; set; }

        public DateTime Date { get; set; }
        public double Period { get; set; }
        public double Last { get; set; }
        public double Early { get; set; }

    }

    public class ProductInventoryByListWarehouseModel //: ProductInventoryPeriodModel
    {
        public string MachineName { get; set; }

        public long ProductId { get; set; }
        [DisplayName("Mã sản phẩm")]
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        [DisplayName("Tồn tổng (PCS)")]
        public double? TotalQty { get; set; }

        // Inv
        [DisplayName("Kho SX 1")]
        public string InvName1 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport1 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport1 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty1 { get; set; }
        [DisplayName("Tồn")]
        public double? TotalQtyImport1 { get; set; }
        [DisplayName("Xuất")]
        public double? TotalQtyExport1 { get; set; }
        [DisplayName("Tồn")]
        public double? TotalQty1 { get; set; }

        [DisplayName("Kho SX 2")]
        public string InvName2 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport2 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport2 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty2 { get; set; }

        [DisplayName("Chờ nhiệt luyện")]
        public string InvName3 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport3 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport3 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty3 { get; set; }

        [DisplayName("Chờ rung bóng")]
        public string InvName4 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport4 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport4 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty4 { get; set; }

        [DisplayName("Chờ GCN")]
        public string InvName5 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport5 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport5 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty5 { get; set; }

        [DisplayName("Kho nhà cung ứng")]
        public string InvName6 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport6 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport6 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty6 { get; set; }

        [DisplayName("Kho QC")]
        public string InvName7 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport7 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport7 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty7 { get; set; }

        [DisplayName("Chờ XL")]
        public string InvName8 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport8 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport8 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty8 { get; set; }

        [DisplayName("Phế phẩm")]
        public string InvName9 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport9 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport9 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty9 { get; set; }

        [DisplayName("Thành phẩm")]
        public string InvName10 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport10 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport10 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty10 { get; set; }

        [DisplayName("Phòng KD")]
        public string InvName11 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport11 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport11 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty11 { get; set; }

        [DisplayName("Kho SX 2/ Kho B")]
        public string InvName12 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport12 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport12 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty12 { get; set; }

        [DisplayName("Kho QC/ Kho B")]
        public string InvName13 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport13 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport13 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty13 { get; set; }

        [DisplayName("Kho Thành Phẩm/ Kho B")]
        public string InvName14 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport14 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport14 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty14 { get; set; }
        // Inv
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        //
        public List<ProductInventoryPeriodModel> ProductInventoryPeriodModels { get; set; }
    }

    public class LuyKeXuatTungThang
    {
        public double LuyKeXuat { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }

    public class SoLieuTongHopSanPham
    {
        public SoLieuTongHopSanPham()
        {
            TonKhoSX2CNC = 0;
            TonKhoSX2SX2 = 0;
            TonKhoSX2SX2B = 0;
            TonKhoSX2SX2C = 0;
            TonKhoSX2SX2D = 0;
            TonKhoNhietLuyen = 0;
            TonKhoRungBong = 0;
            TonKhoGCN = 0;
            TonKhoNCU = 0;
            TonKhoNCUKiemTra = 0;
            TonKhoQCA = 0;
            TonKhoQCB = 0;
            TonKhoQCC = 0;
            TonKhoCXL = 0;
            TonKhoCXL2 = 0;
            Packing = 0;
            TonKhoTPA = 0;
            ReProcessing = 0;
            DeductionMachine = 0;
            ShiftAFunc = new ShiftFund();
            ShiftBFunc = new ShiftFund();
            ShiftCFunc = new ShiftFund();
        }

        public string ReportMessage { get; set; }

        //public List<ProductInventoryIEInfo> ProductInventoryIEInfos { get; set; }

        //public SoLieuTongHopSanPham()
        //{
        //    ProductInventoryIEInfos = new List<ProductInventoryIEInfo>();
        //    SoLieuCacKho = new List<double?>
        //        {
        //            0.0, //1kho 
        //            0.0, //2kho SX2/CNC
        //            0.0, //3kho cho nhiet luyen
        //            0.0, //4kho cho rung bong
        //            0.0, //5kho cho GCN
        //            0.0, //6kho kho NCU
        //            0.0, //7kho QC A
        //            0.0, //8kho cho XL
        //            0.0, //9kho PP
        //            0.0, //10kho TP A
        //            0.0, //11phong KD
        //            0.0, //12kho SX2/SX2
        //            0.0, //13kho QC B
        //            0.0, //14kho TP B
        //            0.0, //kho 
        //            0.0, //kho 
        //        };
        //}
        public string Title { get; set; }
        public int Index { get; set; }
        public double UnitPrice { get; set; }
        public string CustomerCode { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int OnMachine { get; set; }
        public double SanXuat1 { get; set; }

        public double XuatThanhPham { get; set; }
        public double LuyKeXuat { get; set; }

        public double TonTong
        {
            get
            {
                return TonKhoSX2CNC +
                       //TonKhoSX2SX2 + TonKhoSX2SX2B + TonKhoSX2SX2C + TonKhoSX2SX2D +
                       Production2Inv +
                       TonKhoNhietLuyen + TonKhoRungBong +
                       TonKhoGCN + TonKhoNCU + TonKhoNCUKiemTra +
                       TonKhoQCA + TonKhoQCB + TonKhoQCC +
                       //TonKhoCXL + TonKhoCXL2 + ReProcessing +
                       ProcessingInv +
                       Packing + TonKhoTPA;
            }
        }

        public double ProductionDaily { get; set; }
        public double ProductionDailyPoint { get; set; }
        public double ProductionWeekly { get; set; }
        public double ProductionWeeklyPoint { get; set; }
        public double ProductionMonthly { get; set; }
        public double ProductionMonthlyPoint { get; set; }

        public double CamesDaily { get; set; }
        public double CamesDailyPoint { get; set; }
        public double CamesWeekly { get; set; }
        public double CamesWeeklyPoint { get; set; }
        public double CamesMonthly { get; set; }
        public double CamesMonthlyPoint { get; set; }

        public double CncDaily { get; set; }
        public double CncDailyPoint { get; set; }
        public double CncWeekly { get; set; }
        public double CncWeeklyPoint { get; set; }
        public double CncMonthly { get; set; }
        public double CncMonthlyPoint { get; set; }

        //public double Cnc2Daily { get; set; }
        public double Cnc2DailyPoint { get; set; }
        //public double Cnc2Monthly { get; set; }
        public double Cnc2WeeklyPoint { get; set; }
        public double Cnc2MonthlyPoint { get; set; }

        public double ForecastPoint { get; set; }
        public double OrderPoint { get; set; }
        public double OrderRemainingPoint { get; set; }

        public double ExportDailyPoint { get; set; }
        public double ExportWeekly { get; set; }
        public double ExportWeeklyPoint { get; set; }
        public double ExportMonthlyPoint { get; set; }

        public double DonHangThangTruoc { get; set; }
        public double DonHangTrongThang { get; set; }
        public double DonHangThangKe { get; set; }
        public double DonHangSauThangKe { get; set; }
        public double DonHangConLai { get; set; }
        public double DuBaoSX { get; set; }

        public double SLCanSX { get; set; }

        public DateTime? ToDate { get; set; }

        public string DayOfSX1 { get; set; }
        public string DayOfStartSx1 { get; set; }
        public double InventoryPointRate { get; set; }
        public double InventoryPoint { get { return TonTong * UnitPrice * InventoryPointRate / 1000000; } }
        [DisplayName("Kho SX 1")]
        public double TonKhoSX1 { get; set; }
        [DisplayName("Kho SX 2/CNC")]
        public double TonKhoSX2CNC { get; set; }
        public double Production2Inv { get { return TonKhoSX2SX2 + TonKhoSX2SX2B + TonKhoSX2SX2C + TonKhoSX2SX2D; } }
        public double AfterProduction2Inv { get { return TonTong - ProcessingInv - TonKhoSX2CNC; } }
        [DisplayName("Kho SX 2/SX 2")]
        public double TonKhoSX2SX2 { get; set; }
        [DisplayName("Kho SX 2/Văn")]
        public double TonKhoSX2SX2B { get; set; }
        [DisplayName("Kho SX 2/Mẫn")]
        public double TonKhoSX2SX2C { get; set; }
        [DisplayName("Kho SX 2/Cơ khí")]
        public double TonKhoSX2SX2D { get; set; }
        [DisplayName("Kho Nhiệt Luyện")]
        public double TonKhoNhietLuyen { get; set; }
        [DisplayName("Kho Rung Bóng")]
        public double TonKhoRungBong { get; set; }
        [DisplayName("Kho Chờ GCN")]
        public double TonKhoGCN { get; set; }
        [DisplayName("Kho NCU")]
        public double TonKhoNCU { get; set; }
        [DisplayName("Kho NCU Kiem tra")]
        public double TonKhoNCUKiemTra { get; set; }
        [DisplayName("Kho QC A")]
        public double TonKhoQCA { get; set; }
        [DisplayName("Kho QC B")]
        public double TonKhoQCB { get; set; }
        [DisplayName("Kho QC C")]
        public double TonKhoQCC { get; set; }
        [DisplayName("Kho Chờ XL")]
        public double TonKhoCXL { get; set; }
        [DisplayName("Kho Chờ XL 2")]
        public double TonKhoCXL2 { get; set; }
        [DisplayName("Kho Phế Phẩm")]
        public double TonKhoPP { get; set; }
        [DisplayName("Kho Đóng gói")]
        public double Packing { get; set; }
        [DisplayName("Kho TP A")]
        public double TonKhoTPA { get; set; }
        [DisplayName("Kho TP B")]
        public double TonKhoTPB { get; set; }
        [DisplayName("Luỹ Kế SX")]
        public double LuyKeSX { get; set; }
        public double Tranfer { get; set; }
        public double ReProcessing { get; set; }
        public double ProcessingInv { get { return TonKhoCXL + TonKhoCXL2 + ReProcessing; } }
        public double ImportInternal { get; set; }
        public double ExportInternal { get; set; }


        //public List<double?> SoLieuCacKho { get; set; }
        public bool UseForecast { get; set; }

        public bool HienThi { get; set; }
        public int HintProduction { get; set; }
        public int HintOrder { get; set; }
        public double Productivity { get; set; }
        public int HintProduction2 { get; set; }
        public double Productivity2 { get; set; }
        public double ProductionPrice { get; set; }
        public double ProductionPriceInMonth { get; set; }
        public double DefectPrice { get; set; }
        public double DefectPriceInWeek { get; set; }
        public double DefectPriceInMonth { get; set; }
        public string InMonthString { get; set; }
        public string NextMonthString { get; set; }
        public string ProcessingType { get; set; }
        public bool OrderAlert { get; set; }
        public DateTime? MinOrderDate { get; set; }
        public DateTime ReportDate { get; set; }
        public int IsFinish {
            get {

                if (MinOrderDate!=null && MinOrderDate < ReportDate) {
                    if((ReportDate- MinOrderDate.Value).TotalDays>=7){
                        return 1;
                    }
                    return 2;
                }
                return 0;

                //if (TonTong < DonHangConLai)
                //    return 1; // red
                //if (TonKhoTPA + Packing < DonHangConLai)
                //    return 2; // yellow
                //if (TonKhoTPA + Packing < DonHangConLai + DonHangThangKe)
                //    return 3; // 
                //return 0;
            }
        }

        public double DeductionMachine { get; set; }
        public double CncMonthlyLeft { get { return TotalShiftFunc.CncMonthly - DeductionMachine; } }

        public ShiftFund TotalShiftFunc {
            get {
                return new ShiftFund() {
                    CamesDaily = ShiftAFunc.CamesDaily + ShiftBFunc.CamesDaily + ShiftCFunc.CamesDaily,
                    CamesMonthly = ShiftAFunc.CamesMonthly + ShiftBFunc.CamesMonthly + ShiftCFunc.CamesMonthly,
                    ReplaceDaily = ShiftAFunc.ReplaceDaily + ShiftBFunc.ReplaceDaily + ShiftCFunc.ReplaceDaily,
                    ReplaceMonthly = ShiftAFunc.ReplaceMonthly + ShiftBFunc.ReplaceMonthly + ShiftCFunc.ReplaceMonthly,
                    CncDaily = ShiftAFunc.CncDaily + ShiftBFunc.CncDaily + ShiftCFunc.CncDaily,
                    CncMonthly = ShiftAFunc.CncMonthly + ShiftBFunc.CncMonthly + ShiftCFunc.CncMonthly,
                    TeamDDaiLy = ShiftAFunc.TeamDDaiLy + ShiftBFunc.TeamDDaiLy + ShiftCFunc.TeamDDaiLy,
                    TeamDMonthly = ShiftAFunc.TeamDMonthly + ShiftBFunc.TeamDMonthly + ShiftCFunc.TeamDMonthly,
                    TechnicalDaily = ShiftAFunc.TechnicalDaily + ShiftBFunc.TechnicalDaily + ShiftCFunc.TechnicalDaily,
                    TechnicalMonthly = ShiftAFunc.TechnicalMonthly + ShiftBFunc.TechnicalMonthly + ShiftCFunc.TechnicalMonthly,
                    
                };
            }
        } 
        public ShiftFund ShiftAFunc { get; set; }
        public ShiftFund ShiftBFunc { get; set; }
        public ShiftFund ShiftCFunc { get; set; } 

    }
    public class ShiftFund {
        public double CamesDaily { get; set; }
        public double CamesMonthly { get; set; }
        public double ReplaceDaily { get; set; }
        public double ReplaceMonthly { get; set; }
        public double CncDaily { get; set; }
        public double CncMonthly { get; set; }
        public double TeamDDaiLy { get; set; }
        public double TeamDMonthly { get; set; }
        public double TechnicalDaily { get; set; }
        public double TechnicalMonthly { get; set; }

        public double SupportDepartmentDaily { get { return CncDaily * 0.1 + CamesDaily * 0.1; } }
        public double SupportDepartmentMonthly { get { return CncMonthly * 0.1 + CamesMonthly * 0.1; } }

        public double StaffCncDaily { get { return CncDaily * 0.65; } }
        public double StaffCncMonthly { get { return CncMonthly * 0.65; } }
        public double RepairCncDaily { get { return CncDaily * 0.35; } }
        public double RepairCncMonthly { get { return CncMonthly * 0.35; } }
        public double HeadStaffDaily {
            get {
                return CncDaily * 0.18 + ReplaceDaily * 0.19 + TeamDDaiLy * 0.20 + TechnicalDaily * 0.20;
            }
        }
        public double HeadStaffMonthly {
            get {
                return CncMonthly * 0.18 + ReplaceMonthly * 0.19 + TeamDMonthly * 0.20 + TechnicalMonthly * 0.20;
            }
        }

        public double TotalDaily {
            get {
                return ReplaceDaily + CncDaily + TechnicalDaily + TeamDDaiLy + HeadStaffDaily + SupportDepartmentDaily;
            }
        }
        public double TotalMonthly {
            get {
                return ReplaceMonthly + CncMonthly + TechnicalMonthly + TeamDMonthly + HeadStaffMonthly +SupportDepartmentMonthly;
            }
        }
    }

    //public class ProductInventoryIEInfo
    //{
    //    public int WarehouseId { get; set; }
    //    public string WarehouseName { get; set; }
    //    public double Import { get; set; }
    //    public double Export { get; set; }
    //}

    public class ProductInventoryByMonthlyModel
    {
        public DateTime MonthlyDate { get; set; }

        public string MachineName { get; set; }


        public long ProductId { get; set; }
        [DisplayName("Mã sản phẩm")]
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        [DisplayName("Tồn tổng (PCS)")]
        public double? TotalQty { get; set; }

        // Inv
        [DisplayName("Kho SX 1")]
        public string InvName1 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport1 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport1 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty1 { get; set; }

        [DisplayName("Kho SX 2")]
        public string InvName2 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport2 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport2 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty2 { get; set; }

        [DisplayName("Chờ nhiệt luyện")]
        public string InvName3 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport3 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport3 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty3 { get; set; }

        [DisplayName("Chờ rung bóng")]
        public string InvName4 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport4 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport4 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty4 { get; set; }

        [DisplayName("Chờ GCN")]
        public string InvName5 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport5 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport5 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty5 { get; set; }

        [DisplayName("Kho nhà cung ứng")]
        public string InvName6 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport6 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport6 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty6 { get; set; }

        [DisplayName("Kho QC")]
        public string InvName7 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport7 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport7 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty7 { get; set; }

        [DisplayName("Chờ XL")]
        public string InvName8 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport8 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport8 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty8 { get; set; }

        [DisplayName("Phế phẩm")]
        public string InvName9 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport9 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport9 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty9 { get; set; }

        [DisplayName("Thành phẩm")]
        public string InvName10 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport10 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport10 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty10 { get; set; }

        [DisplayName("Phòng KD")]
        public string InvName11 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport11 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport11 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty11 { get; set; }

        [DisplayName("Kho SX 2/ Kho B")]
        public string InvName12 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport12 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport12 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty12 { get; set; }

        [DisplayName("Kho QC/ Kho B")]
        public string InvName13 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport13 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport13 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty13 { get; set; }

        [DisplayName("Kho Thành Phẩm/ Kho B")]
        public string InvName14 { get; set; }
        [DisplayName("Nhập")]
        public double? QtyImport14 { get; set; }
        [DisplayName("Xuất")]
        public double? QtyExport14 { get; set; }
        [DisplayName("Tồn")]
        public double? Qty14 { get; set; }

        [DisplayName("Lũy kế sản xuất")]
        public double? AccumulatedQty1 { get; set; }

        [DisplayName("Lũy kế xuất")]
        public double? AccumulatedQty11 { get; set; }
    }


    public class SoLieuTongHopSanPham2 {
        public SoLieuTongHopSanPham2() {
            TonKhoSX2CNC = 0;
            TonKhoSX2SX2 = 0;
            TonKhoSX2SX2B = 0;
            TonKhoSX2SX2C = 0;
            TonKhoSX2SX2D = 0;
            TonKhoNhietLuyen = 0;
            TonKhoRungBong = 0;
            TonKhoGCN = 0;
            TonKhoNCU = 0;
            TonKhoNCUKiemTra = 0;
            TonKhoQCA = 0;
            TonKhoQCB = 0;
            TonKhoQCC = 0;
            TonKhoCXL = 0;
            TonKhoCXL2 = 0;
            Packing = 0;
            TonKhoTPA = 0;

        }
        public double TonKhoSX2CNC { get; set; }
        [DisplayName("Kho SX 2/SX 2")]
        public double TonKhoSX2SX2 { get; set; }
        [DisplayName("Kho SX 2/Văn")]
        public double TonKhoSX2SX2B { get; set; }
        [DisplayName("Kho SX 2/Mẫn")]
        public double TonKhoSX2SX2C { get; set; }
        [DisplayName("Kho SX 2/Cơ khí")]
        public double TonKhoSX2SX2D { get; set; }
        [DisplayName("Kho Nhiệt Luyện")]
        public double TonKhoNhietLuyen { get; set; }
        [DisplayName("Kho Rung Bóng")]
        public double TonKhoRungBong { get; set; }
        [DisplayName("Kho Chờ GCN")]
        public double TonKhoGCN { get; set; }
        [DisplayName("Kho NCU")]
        public double TonKhoNCU { get; set; }
        [DisplayName("Kho NCU Kiem tra")]
        public double TonKhoNCUKiemTra { get; set; }
        [DisplayName("Kho QC A")]
        public double TonKhoQCA { get; set; }
        [DisplayName("Kho QC B")]
        public double TonKhoQCB { get; set; }
        [DisplayName("Kho QC C")]
        public double TonKhoQCC { get; set; }
        [DisplayName("Kho Chờ XL")]
        public double TonKhoCXL { get; set; }
        [DisplayName("Kho Chờ XL 2")]
        public double TonKhoCXL2 { get; set; }
        [DisplayName("Kho Phế Phẩm")]
        public double TonKhoPP { get; set; }
        [DisplayName("Kho Đóng gói")]
        public double Packing { get; set; }
        [DisplayName("Kho TP A")]
        public double TonKhoTPA { get; set; }

        public string Title { get; set; }
        public int Index { get; set; }
        public string CustomerCode { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }

        public double XuatThanhPham { get; set; }
        public double LuyKeXuat { get; set; }

        public double TonTong {
            get {
                return TonKhoSX2CNC +
                       Production2Inv +
                       TonKhoNhietLuyen + TonKhoRungBong +
                       TonKhoGCN + TonKhoNCU + TonKhoNCUKiemTra +
                       TonKhoQCA + TonKhoQCB + TonKhoQCC +
                       ProcessingInv +
                       Packing + TonKhoTPA;
            }
        }
        public double ProcessingInv { get { return TonKhoCXL + TonKhoCXL2 + ReProcessing; } }
        public double ReProcessing { get; set; }
        public double Production2Inv { get { return TonKhoSX2SX2 + TonKhoSX2SX2B + TonKhoSX2SX2C + TonKhoSX2SX2D; } }
        public bool HienThi { get; set; }
        public bool UseForecast { get; set; }

        public double DonHangThangTruoc { get; set; }
        public double DonHangTrongThang { get; set; }
        public double DonHangThangKe { get; set; }
        public double DonHangSauThangKe { get; set; }
        public double DonHangConLai { get; set; }
        public double DuBaoSX { get; set; }
    }

    public class TestingTransactionDetail {
        public int TransactionId { get; set; }
        public long TransactionDetailId { get; set; }
        public string LotNumber { get; set; }
        public double Quantity {set; get;}
        public int ProductId { get; set; }
        public string Note { get; set; }
    }


}