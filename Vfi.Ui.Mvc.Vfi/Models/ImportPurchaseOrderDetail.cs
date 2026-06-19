using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class ImportPurchaseOrderDetail
    {
        public long ImportDetailId { get; set; }
        public long ImportId { get; set; }
        public Nullable<int> MaterialId { get; set; }
        public string LotNumber { get; set; }
        public double Quantity { get; set; }
        public double QuantityKg { get; set; }
        public string Note { get; set; }
        public double UnitPrice { get; set; }
        public double UnitWeight { get; set; }
        public int VendorId { get; set; }
        public string StoreCode { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> PoReferenceDetailId { get; set; }
        public double Length { get; set; }
        public Nullable<long> PoDetailId { get; set; }

        public bool NG { get; set; }
        public bool Lock { get; set; }


        public virtual Material Material { get; set; }
        public virtual ImportPurchaseOrder ImportPurchaseOrder { get; set; }
        public virtual PoTaxInvoiceReferenceDetail PoTaxInvoiceReferenceDetail { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
