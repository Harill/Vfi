using System;
using System.ComponentModel.DataAnnotations;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models
{
    public class TransactionFptDetailModel
    {
        public TransactionFptDetailModel()
        {
            UnitPrice = 0;
            CurrencyCode = "";
            ExchangeRate = 1;
            Qc = "Đạt";
        }

        public string TransactionTitle { get; set; }

        public int Index { get; set; }
        public long DetailId { get; set; }
        public long PoDetailId { get; set; }
        public long TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public int VendorId { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string StoreCode { get; set; }
        public string VendorCodeName
        {
            get { return VendorCode + " - " + VendorName; }
        }

        public int MachineId { get; set; }
        [UIHint("_AllMachineEditTemplate")]
        public string MachineName { get; set; }
        public double QuotaQuantity { get; set; }
        public double RequiredQuantity { get; set; }

        [UIHint("Number2")]
        public double Quantity { get; set; }
        //public string QuantityStr                     khong dung` vi` o footer tinh tong
        //{
        //    get 
        //    {
        //        var stringq = (UnitMeasure ?? string.Empty).Replace(" ", string.Empty).Trim();
        //        return stringq.Equals("Pcs", StringComparison.OrdinalIgnoreCase)
        //                   ? string.Format("{0:n0}", Quantity)
        //                   : string.Format("{0:n2}", Quantity);
        //    }
        //}


        public double TotalInv { get; set; }
        public double AvailInv { get; set; }
        public string CurrencyCode { get; set; }

        [UIHint("Number2")]
        public double UnitPrice { get; set; }

        public string UnitPriceString
        {
            get
            {
                var code = (CurrencyCode ?? string.Empty).Replace(" ", string.Empty).Trim(); 
                return !code.Equals("VND", StringComparison.OrdinalIgnoreCase)                           
                           ? string.Format("{0:n3}", UnitPrice)
                           : string.Format("{0:n0}", UnitPrice);
            }
        }

        public double Price { get; set; }

        public string PriceString
        {
            get
            {
                return !CurrencyCode.Equals("VND")
                           ? string.Format("{0:n2}", Price)
                           : string.Format("{0:n0}", Price);
            }
        }

        [UIHint("_UnitTemplate")]
        public string UnitMeasure { get; set; }

        public string LotNumber { get; set; }
        public string Note { get; set; }

        public long PoId { get; set; }
        public string PoNumber { get; set; }

        public byte Status { get; set; }
        public string StatusName { get; set; }

        public int Type { get; set; }
        public string TypeName { get; set; }
        public int EoI { get; set; }
        public string EoIName { get; set; }
        public int FptType { get; set; }
        public string FptTypeName { get; set; }

        public int FptId { get; set; }
        public string FptName { get; set; }
        public string FptCode { get; set; }
        public string FptDesignNo { get; set; }
        public string FptFullCodeName { get; set; }

        public int FuelId { get; set; }
        public int FuelInvId { get; set; }
        public string FuelCode { get; set; }
        public string FuelName { get; set; }
        public string FuelDesignNo { get; set; }

        [UIHint("_FuelEditTemplate")]
        public string FuelFullCodeName { get; set; }


        public int ToolId { get; set; }
        public int ToolInvId { get; set; }
        public string ToolCode { get; set; }
        public string ToolName { get; set; }
        public string ToolDesignNo { get; set; }

        [UIHint("_ToolEditTemplate")]
        public string ToolFullCodeName { get; set; }

        public string ToolMaterial { get; set; }

        public int ProductId { get; set; }
        public int ProductInvId { get; set; }

        [UIHint("_ProductEditTemplate")]
        public string ProductCode { get; set; }

        public string ProductName { get; set; }
        public string ProductDesignNo { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        public byte InventorySignature { get; set; }
        public byte QcSignature { get; set; }
        public byte PurchasingSignature { get; set; }
        public byte AccountantSignature { get; set; }

        public int TaxPercent { get; set; }

        public double Tax
        {
            get { return Price*TaxPercent/100; }
        }

        public double ExchangeRate { get; set; }
        public string TaxInvoiceNumber { get; set; }
        public DateTime? TaxInvoiceDate { get; set; }

        public string TaxInvoiceDateString
        {
            get
            {
                return TaxInvoiceDate != null
                           ? string.Format("{0:dd/MM/yyyy}", TaxInvoiceDate)
                           : "";
            }
        }
        public string Qc { get; set; }

        public double MachineInv { get; set; }
        [UIHint("Number2")]
        public double MachineUse { get; set; }
        [UIHint("Number2")]
        public double MachineReturn { get; set; }

        public bool IsDestroy { get; set; }

        public WorkGroupInfo Info { get; set; }


        public Nullable<int> ProductionToolId { get; set; }                 //20/01/2026

        public DateTime? DeliveryDate { get; set; }

        public bool VendorDeliveryOnDate { get; set; }

        public int VendorDeliveryOnDateValue {
            get {
                return VendorDeliveryOnDate ? 1 : 0;
            } 
        }

        public string PoCode { get; set; }
        public int  PlatingId { get; set; }
        public double OrderQuantity { get; set;}
     
       public string OrderQuantityString { 
            get{
                return OrderQuantity == 0.00 
                    ? "" 
                    : string.Format("{0:n2}", OrderQuantity);
                }
       }

       public int Fpt { get; set; }
       public double ToolInv { get; set; }

       public long ImportDetailId { get; set; }

       public bool NG { get; set; }
       public string NGString {
            get {
                return NG == true
                    ? "NG"
                    : "Đạt";
            }
        }


        public bool Lock { get; set; }
        public string LockStr {
            get {
                return Lock == true
                    ? "Đã khóa"
                    : "";
            }
        }

    }
}