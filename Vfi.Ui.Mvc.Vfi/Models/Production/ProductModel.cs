
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vfi.Server.Core.DataModel.Models.Production;
using Vfi.Ui.Mvc.Vfi.Areas.Sales.Models;

namespace Vfi.Ui.Mvc.Vfi.Models.Production
{
    public class ProductModel: ProductDomainModel {
        //[Required(ErrorMessage = @"*")]
        [UIHint("_MaterialEditTemplate")]
        public override string MaterialCodeName { get { return MaterialCode + " -- " + MaterialName; } set { MaterialCode = value; } }

        //[Required(ErrorMessage = "*")]
        [UIHint("_CustomerEditTemplate")]
        public override string CustomerCodeName { get { return CustomerCode + " -- " + CustomerName; } set { CustomerCode = value; } }

        public string CustomerShortName { get; set; }
        public string CustomerShortCodeName { get { return CustomerCode + " -- " + CustomerShortName; } set { CustomerCode = value; } }

        public int MachineFunction { get; set; }
        public string ProductTaxCode
        {
            get { return DesignNo + " - "+ ProductName + " - " + TaxCode; }
        }
        public int Index { get; set; }
        [DisplayName("Mã KH")]
        //[Required(ErrorMessage = "*")]
        [UIHint("_CustomerEditTemplate")]
        public override string CustomerCode { get; set; }

        //[Required(ErrorMessage = "*")]
        [UIHint("_MaterialEditTemplate")]
        public override string MaterialCode { get; set; }

        [DisplayName("Mã vf - Kh")]
        public string PrductVfCodeDesignNo { get { return ProductCode; } set { ProductCode = value; } }
        //public string PrductVfCodeDesignNo { get { return ProductCode + " -- " + DesignNo; } set { ProductCode = value; } }
        public string TaxCode { get; set; }
        public bool ProductCodeChanged { get; set; }
        public bool ProductionChanged { get; set; }
        public bool OutsideProcessChanged { get; set; }

        [DisplayName("Mã nguyên liệu thiết kế")]
        public string MaterialCodeDesign { get; set; }

        public string Note { get; set; }

        [UIHint("_CurrencyTemplate")]
        public string Currency { get; set; }

        public string UploadDate { get; set; }

        public string IdentityCode { get; set; }

        public int MaxQuantityInTray { get; set; }
        public int MaxQuantityInTrayRunTime { get; set; }


        public int ProductionLevel { get; set; }
        [UIHint("_ProductionLevelEditTemplate")]
        public string ProductionLevelName { get; set; }
        [DataType("Number2Digit")]
        public double PackingFee { get; set; }

        public int ProcessClassifiedId { get; set; }
        [DataType("_ProcessClassifiedTemplate")]
        public string ProcessClassifiedName { get; set; }

        //public double GetProductWeight()
        //{
        //    var w = 0.0;
        //    w = OutDiameterDesign*OutDiameterDesign;
        //    switch (ShapeDesign)
        //    {
        //        case "H":
        //            w = w * 1.1;
        //            break;
        //        case "S":
        //        case "R":
        //            w = w * 1.3;
        //            break;
        //        case "T":
        //            w -= (InDiameterDesign*InDiameterDesign);
        //            break;
        //    }
        //    w = w*((Length + KnifeCut) ?? 0)*1.1*0.0066;

        //    if (MaterialNameDesign.StartsWith("A"))
        //        w = w/3;
        //    return w;
        //}

        //public double GetProductWeight(double outDiameter, double inDiameter, double lenght, double knifeCut,
        //                               string name, string shape)
        //{
        //    var w = 0.0;
        //    w = outDiameter*outDiameter;
        //    switch (shape)
        //    {
        //        case "H":
        //            w = w*1.1;
        //            break;
        //        case "S":
        //        case "R":
        //            w = w*1.3;
        //            break;
        //        case "T":
        //            w -= (inDiameter*inDiameter);
        //            break;
        //    }
        //    w = w * (lenght + knifeCut) * 1.1 * 0.0066;

        //    if (name.StartsWith("A"))
        //        w = w/3;
        //    return w;
        //}

        //public string GetMaterialCodeDesign()
        //{
        //    if (string.IsNullOrWhiteSpace(MaterialNameDesign))
        //        return "";
        //    string a = MaterialNameDesign;
        //    if (!string.IsNullOrWhiteSpace(ShapeDesign))
        //    {
        //        a += "-(" + ShapeDesign.Trim() + ")";
        //    }
        //    else
        //        return a;
        //    //if (!string.IsNullOrWhiteSpace(ShapeDesign))
        //    //    ShapeDesign = ShapeDesign.Trim();
        //    if (ShapeDesign.Contains("H") || ShapeDesign.Contains("D") || ShapeDesign.Contains("S"))
        //        a +=OutDiameterDesign;
        //    else if (ShapeDesign.Contains("T"))
        //    {
        //        a += "(" + (OutDiameterDesign) + "x" + (InDiameterDesign) + ")";
        //    }
        //    if (!string.IsNullOrWhiteSpace(DiameterTypeDesign))
        //        a += "-" + DiameterTypeDesign;
        //    return a;
        //}

        public int MaterialTypeId { get; set; }
        public double MaterialTypeCost
        {

            get
            {
                if (Weight == null) return 0;
                switch (MaterialTypeId)
                {
                    //sat-thep
                    case 2:
                    case 6:
                        return 50 * Weight.Value - (50 * Weight.Value/10);
                        //nhom
                    case 3:
                        return 120 * Weight.Value - (120 * Weight.Value / 5);
                        //dong thau
                    case 4:
                        return 140 * Weight.Value - (140 * Weight.Value / 3);
                        //inox
                    case 5:
                        return 120 * Weight.Value - (120 * Weight.Value / 10);
                    default:
                        return 0;
                }
            }
        }

        [UIHint("_UploadTemplate")]
        public string Upload2D { get; set; }
        public string Attachment2D { get; set; }

        [UIHint("_Upload2Template")]
        public string UploadReal { get; set; }
        public string AttachmentReal { get; set; }

        public byte Status { get; set; }
        [UIHint("_ProductStatusTemplate")]
        public string StatusName { get; set; }

        public int ProcessingTypeId { get; set; }
        [DisplayName("Loại máy gia công")]
        [DataType("_ProcessingTypeTemplate")]
        public string ProcessingTypeName { get; set; }
        public string RealProcessingTypeName { get; set; }
        public double RealProcessingCost { get; set; }
        public string DiameterTypeDesign { get; set; }
        [DisplayName("Dao cắt (mm)")]
        [DataType("Number")]
        public double KnifeCut { get; set; }

        public string SectionName { get; set; }
        public int SectionCount { get; set; }
        public double SectionCost { get; set; }
        public double SectionProductivity { get; set; }

        public int StatusFilter { get; set; }
        public string PrintProductList { get; set; }
        [DataType("NumberAsInt")]
        public double MillProductivity { get; set; }
        [DataType("NumberAsInt")]
        public double CncProductivity { get; set; }

        public double MaterialCost { get; set; }
        public double MillCost { get; set; }
        public double RealMillCost { get; set; }
        public double ProcessingSalesCost { get; set; }
        public double ProcessingCost { get; set; }
        [DataType("NumberAsInt")]
        public double QcProductivity { get; set; }

        public double ProductBaseCost { get; set; }
        public double RealProductBaseCost { get; set; }
        public double MaterialUnitPrice { get; set; }
        public string PlatingName { get; set; }
        public double PlatingCost { get; set; }
        public List<ProductionSectionModel> SectionList { get; set; }
        public List<OrderDetailModel> OrderList { get; set; }

        public string ProductShape { get; set; }

        public double RealProductivity { get; set; }
        public double RealMillProductivity { get; set; }

        public string ToDate { get; set; }
        public string NextMonth { get; set; }
        public double OrderQuantity { get; set; }
        public double ForecastNextMonth { get; set; }
        public double TotalQuantity { get; set; }
        public double OrderQuantityNeed { get; set; }
        public double ForecastQuantityNeed { get; set; }
        public double ForecastNextMonthNeed { get; set; }
        public double OrderProcessTime { get; set; }
        public double ForecastProcessTime { get; set; }
        public double NextMonthProcessTime { get; set; }
        public int IsOrderInMonth { get; set; }

        public int BaseProductId { get; set; }

        [UIHint("_ProductEditTemplate")]
        public string BaseProductCode { get; set; }

        public bool IsCalculateLock { get; set; }
        public int ProductionLossRate { get; set; }
    }

    public class ProductManagementSales {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }

        [UIHint("_CustomerEditTemplate")]
        public string CustomerCodeName { get { return CustomerCode + " -- " + CustomerName; } set { CustomerCode = value; } }
        public string DesignNo { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
        public double? UnitPrice { get; set; }

        [UIHint("_UploadTemplate")]
        public string Upload2D { get; set; }
        public string Attachment2D { get; set; }

        [UIHint("_Upload2Template")]
        public string UploadReal { get; set; }
        public string AttachmentReal { get; set; }

        public string UploadDate { get; set; }
        [DataType("NumberAsInt")]
        public double MillCost { get; set; }
        public double ProcessingSalesCost { get; set; }
        public double ProcessingCost { get; set; }
        public string Note { get; set; }
        public bool IsCalculateLock { get; set; }

        [UIHint("_CurrencyTemplate")]
        public string Currency { get; set; }
        public double MaterialCost { get; set; }

        [DataType("Number2Digit")]
        public double PackingFee { get; set; }
        public double SectionCost { get; set; }
        public byte Status { get; set; }

    }


    public class ProductManagementProduction {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }

        [UIHint("_UploadTemplate")]
        public string Upload2D { get; set; }
        public string Attachment2D { get; set; }

        [UIHint("_Upload2Template")]
        public string UploadReal { get; set; }
        public string AttachmentReal { get; set; }
        public bool Active { get; set; }
        public byte Status { get; set; }
        public double ForecastsQuality { get; set; }
        public string UploadDate { get; set; }
        public bool IsCalculateLock { get; set; }
        public int MachineFunction { get; set; }
        public int SectionCount { get; set; }
        public string MaterialCodeDesign { get; set; }

    }

}