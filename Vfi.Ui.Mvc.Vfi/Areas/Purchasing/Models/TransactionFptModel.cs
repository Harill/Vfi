using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models
{
    public class TransactionFptModel
    {
        public TransactionFptModel()
        {
            TotalPrice = 0;
            CurrencyCode = "";
            Details = new List<TransactionFptDetailModel>();
        }
        public long TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public byte Status { get; set; }
        public string StatusName { get; set; }
        public string Department { get; set; }
        public int Type { get; set; }
        public int EoI { get; set; }
        public string EoIName { get; set; }
        public int Fpt { get; set; }
        public string FptName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public long PoId { get; set; }
        public string PoCode { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        public string TotalPriceString
        {
            get
            {
                return !CurrencyCode.Equals("VND")
                           ? string.Format("{0:n3}", TotalPrice)
                           : string.Format("{0:n0}", TotalPrice);
            }
        }
        public int AlertColor { get; set; }
        public bool CanUpdate { get; set; }
        public byte InventorySignature { get; set; }
        public byte QcSignature { get; set; }
        public byte PurchasingSignature { get; set; }
        public byte AccountantSignature { get; set; }

        public byte InventorySignatureType { get; set; }
        public byte QcSignatureType { get; set; }
        public byte PurchasingSignatureType { get; set; }
        public byte AccountantSignatureType { get; set; }
        public string Note { get; set; }
        public string CurrencyCode { get; set; }
        public List<TransactionFptDetailModel> Details { get; set; }

        public int InvManager { get; set; }
        public bool IsInternal { get; set; }
        public string MachineName { get; set; }                 // 28/03/2026


    }
}