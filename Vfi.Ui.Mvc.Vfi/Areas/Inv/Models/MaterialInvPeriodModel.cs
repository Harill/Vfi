using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Models {
    public class MaterialInvPeriodModel {
        public MaterialInvPeriodModel() {
            EarlyQuantity = 0;
            Import = 0;
            Export = 0;
            LastQuantity = 0;
            MaterialUnitWeight = 0;
            MaterialUnitPrice = 0;
            Destroy = 0;
            DestroyOnMachine = 0;
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
            ImportMore = 0;
            ImportOnMachine = 0;
        }
        public bool CanUpload { get; set; }
        public bool CanEdit { get; set; }
        [DataType("_DateTemplate")]
        public DateTime FromDate { get; set; }
        public string FromDateString { get; set; }
        [DataType("_DateTemplate")]
        public DateTime ToDate { get; set; }
        public string ToDateString { get; set; }

        public int MaterialTypeId { get; set; }
        public string MaterialTypeCode { get; set; }
        public string MaterialTypeName { get; set; }

        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialCode { get; set; }
        public string Shape { get; set; }
        public double OutDiameter { get; set; }
        public double InDiameter { get; set; }
        public string DiameterType { get; set; }
        //
        public int MaterialInvId { get; set; }
        public string MaterialLot { get; set; }
        public double Length { get; set; }
        public double MaterialUnitWeight { get; set; }
        public double MaterialUnitPrice { get; set; }
        public string ImportDateString { get; set; }
        public string FirstImport { get; set; }
        public string FirstImportKg { get; set; }
        public string FirstUseDateString { get; set; }
        public string EndUseDateString { get; set; }
        public string StoreCode { get; set; }

        public int VendorId { get; set; }
        public string VendorCode { get; set; }

        public double EarlyQuantity { get; set; }
        public double EarlyKg { get { return EarlyTotal * MaterialUnitWeight; } }
        public double EarlyPrice { get { return EarlyKg * MaterialUnitPrice; } }
        public double EarlyOnMachine { get; set; }
        public double EarlyTotal { get { return EarlyQuantity + EarlyOnMachine; } }

        public double Import { get; set; }
        public double ImportKg { get { return Import * MaterialUnitWeight; } }
        public double ImportPrice { get { return ImportKg * MaterialUnitPrice; } }
        public double ImportMore { get; set; }
        public double ImportOnMachine { get; set; }
        public double ImportInternal { get; set; }
        public double ImportInternalWeight { get { return ImportInternal * MaterialUnitWeight; } }
        public double ImportInternalPrice { get { return ImportInternal * MaterialUnitPrice; } }
        public double TotalImport { get { return Import + ImportMore + ImportInternal; } }
        public double TotalImportKg { get { return TotalImport * MaterialUnitWeight; } }
        public double TotalImportPrice { get { return TotalImportKg * MaterialUnitPrice; } }

        public double Export { get; set; }
        public double ExportKg { get { return (Export) * MaterialUnitWeight; } }
        public double ExportPrice { get { return ExportKg * MaterialUnitPrice; } }
        public double Destroy { get; set; }
        public double DestroyOnMachine { get; set; }
        public double DestroyKg { get { return (Destroy + DestroyOnMachine) * MaterialUnitWeight; } }
        public double DestroyPrice { get { return DestroyKg * MaterialUnitPrice; } }
        public double ExportInternal { get; set; }
        public double ExportInternalWeight { get { return ExportInternal * MaterialUnitWeight; } }
        public double ExportInternalPrice { get { return ExportInternalWeight * MaterialUnitPrice; } }
        
        public double LastQuantity { get; set; }
        public double LastKg { get { return LastTotal * MaterialUnitWeight; } }
        public double LastPrice { get { return LastKg * MaterialUnitPrice; } }
        public double LastOnMachine { get; set; }
        public double LastTotal { get { return LastQuantity + LastOnMachine; } }

        public bool Show { get; set; }
        public bool Active { get; set; }

        public double DiffImport { get; set; }
        public double DiffExport { get; set; }

        public double DiffInv {
            get {
                return EarlyQuantity + EarlyOnMachine
                       + TotalImport
                       - Export - Destroy - DestroyOnMachine - ExportInternal
                       - LastQuantity - LastOnMachine;
            }
        }

        public bool Check {
            get {
                return EarlyQuantity + EarlyOnMachine +
                       Import + ImportMore + ImportOnMachine + ImportInternal +
                       Export + Destroy + DestroyOnMachine + ExportInternal +
                       LastQuantity + LastOnMachine > 0;
            }
        }

        public bool HaveNG { get; set; }
        public bool HaveLock { get; set; }



        [UIHint("_UploadTemplate")]
        public string InfoImg { get; set; }
        public string UploadDate { get; set; }
        [UIHint("_Upload2Template")]
        public string InfoImg2 { get; set; }
    }

    public class MaterialPeriodModel {
        public MaterialPeriodModel() {
            Invs = new List<MaterialInvPeriodModel>();
        }
        public List<MaterialInvPeriodModel> Invs { get; set; }
        [DataType("_DateTemplate")]
        public DateTime FromDate { get; set; }
        public string FromDateString { get; set; }
        [DataType("_DateTemplate")]
        public DateTime ToDate { get; set; }
        public string ToDateString { get; set; }

        public int MaterialTypeId { get; set; }
        public string MaterialTypeCode { get; set; }
        public string MaterialTypeName { get; set; }

        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialCode { get; set; }
        public string Shape { get; set; }
        public double OutDiameter { get; set; }
        public double InDiameter { get; set; }
        public string DiameterType { get; set; }
        //
        public double EarlyQuantity { get { return Invs.Sum(i => i.EarlyQuantity); } }
        public double EarlyKg { get { return Invs.Sum(i => i.EarlyKg); } }
        public double EarlyPrice { get { return Invs.Sum(i => i.EarlyPrice); } }
        public double EarlyOnMachine { get { return Invs.Sum(i => i.EarlyOnMachine); } }
        public double EarlyTotal { get { return EarlyQuantity + EarlyOnMachine; } }

        public double Import { get { return Invs.Sum(i => i.Import); } }
        public double ImportKg { get { return Invs.Sum(i => i.ImportKg); } }
        public double ImportPrice { get { return Invs.Sum(i => i.ImportPrice); } }
        public double ImportMore { get { return Invs.Sum(i => i.ImportMore); } }
        public double ImportOnMachine { get { return Invs.Sum(i => i.ImportOnMachine); } }
        public double ImportInternal { get { return Invs.Sum(i => i.ImportInternal); } }
        public double ImportInternalWeight { get { return Invs.Sum(i => i.ImportInternalWeight); } }
        public double ImportInternalPrice { get { return Invs.Sum(i => i.ImportInternalPrice); } }
        public double TotalImport { get { return Invs.Sum(i => i.TotalImport); } }
        public double TotalImportKg { get { return Invs.Sum(i => i.TotalImportKg); } }
        public double TotalImportPrice { get { return Invs.Sum(i => i.TotalImportPrice); } }

        public double Export { get { return Invs.Sum(i => i.Export); } }
        public double ExportKg { get { return Invs.Sum(i => i.ExportKg); } }
        public double ExportPrice { get { return Invs.Sum(i => i.ExportPrice); } }
        public double Destroy { get { return Invs.Sum(i => i.Destroy); } }
        public double DestroyOnMachine { get { return Invs.Sum(i => i.DestroyOnMachine); } }
        public double DestroyKg { get { return Invs.Sum(i => i.DestroyKg); } }
        public double DestroyPrice { get { return Invs.Sum(i => i.DestroyPrice); } }
        public double ExportInternal { get { return Invs.Sum(i => i.ExportInternal); } }
        public double ExportInternalWeight { get { return Invs.Sum(i => i.ExportInternalWeight); } }
        public double ExportInternalPrice { get { return Invs.Sum(i => i.ExportInternalPrice); } }

        public double LastQuantity { get { return Invs.Sum(i => i.LastQuantity); } }
        public double LastKg { get { return Invs.Sum(i => i.LastKg); } }
        public double LastPrice { get { return Invs.Sum(i => i.LastPrice); } }
        public double LastOnMachine { get { return Invs.Sum(i => i.LastOnMachine); } }
        public double LastTotal { get { return LastQuantity + LastOnMachine; } }

        public bool Show { get; set; }
    }
    public class MaterialGroupModel {
        public MaterialGroupModel() {
            Materials = new List<MaterialPeriodModel>();
            InjectExports = new List<MaterialInjectReportModel>();
        }
        public List<MaterialPeriodModel> Materials { get; set; }
        [DataType("_DateTemplate")]
        public DateTime FromDate { get; set; }
        public string FromDateString { get; set; }
        [DataType("_DateTemplate")]
        public DateTime ToDate { get; set; }
        public string ToDateString { get; set; }

        public int MaterialTypeId { get; set; }
        public string MaterialTypeCode { get; set; }
        public string MaterialTypeName { get; set; }
        //
        public double EarlyQuantity { get { return Materials.Sum(i => i.EarlyQuantity); } }
        public double EarlyKg { get { return Materials.Sum(i => i.EarlyKg); } }
        public double EarlyPrice { get { return Materials.Sum(i => i.EarlyPrice); } }
        public double EarlyOnMachine { get { return Materials.Sum(i => i.EarlyOnMachine); } }
        public double EarlyTotal { get { return EarlyQuantity + EarlyOnMachine; } }

        public double Import { get { return Materials.Sum(i => i.Import); } }
        public double ImportKg { get { return Materials.Sum(i => i.ImportKg); } }
        public double ImportPrice { get { return Materials.Sum(i => i.ImportPrice); } }
        public double ImportMore { get { return Materials.Sum(i => i.ImportMore); } }
        public double ImportOnMachine { get { return Materials.Sum(i => i.ImportOnMachine); } }
        public double ImportInternal { get { return Materials.Sum(i => i.ImportInternal); } }
        public double ImportInternalWeight { get { return Materials.Sum(i => i.ImportInternalWeight); } }
        public double ImportInternalPrice { get { return Materials.Sum(i => i.ImportInternalPrice); } }
        public double TotalImport { get { return Materials.Sum(i => i.TotalImport); } }
        public double TotalImportKg { get { return Materials.Sum(i => i.TotalImportKg); } }
        public double TotalImportPrice { get { return Materials.Sum(i => i.TotalImportPrice); } }

        public double Export { get { return Materials.Sum(i => i.Export); } }
        public double ExportKg { get { return Materials.Sum(i => i.ExportKg); } }
        public double ExportPrice { get { return Materials.Sum(i => i.ExportPrice); } }
        public double Destroy { get { return Materials.Sum(i => i.Destroy); } }
        public double DestroyOnMachine { get { return Materials.Sum(i => i.DestroyOnMachine); } }
        public double DestroyKg { get { return Materials.Sum(i => i.DestroyKg); } }
        public double DestroyPrice { get { return Materials.Sum(i => i.DestroyPrice); } }
        public double ExportInternal { get { return Materials.Sum(i => i.ExportInternal); } }
        public double ExportInternalWeight { get { return Materials.Sum(i => i.ExportInternalWeight); } }
        public double ExportInternalPrice { get { return Materials.Sum(i => i.ExportInternalPrice); } }

        public double LastQuantity { get { return Materials.Sum(i => i.LastQuantity); } }
        public double LastKg { get { return Materials.Sum(i => i.LastKg); } }
        public double LastPrice { get { return Materials.Sum(i => i.LastPrice); } }
        public double LastOnMachine { get { return Materials.Sum(i => i.LastOnMachine); } }
        public double LastTotal { get { return LastQuantity + LastOnMachine; } }

        public bool Show { get; set; }

        public List<MaterialInjectReportModel> InjectExports { get; set; }
    }
    public class MaterialInjectReportModel {
        public string MaterialTypeName { get; set; }
        public double Destroy { get; set; }
        public double DestroyWeight { get; set; }
        public double DestroyPrice { get; set; }
        public double ExportInternal { get; set; }
        public double ExportInternalWeight { get; set; }
        public double ExportInternalPrice { get; set; }
    }
    public class MaterialPeriodDetailModel {
        [DataType("_DateTemplate")]
        public DateTime PeriodDate { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public long TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public double Quantity { get; set; }
        public double QuantityKg { get; set; }
        public string Note { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
    }
}