using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            this.FuelInventories = new List<FuelInventory>();
            this.MaterialInventories = new List<MaterialInventory>();
            this.ToolInventories = new List<ToolInventory>();
            this.ImportPurchaseOrderDetails = new List<ImportPurchaseOrderDetail>();
            this.InquiryPoes = new List<InquiryPo>();
            this.PlatingForms = new List<PlatingForm>();
            this.PoTaxInvoices = new List<PoTaxInvoice>();
            this.PurchaseOrders = new List<PurchaseOrder>();
            this.EvaluationForms = new List<EvaluationForm>();
            this.VendorImgs = new List<VendorImg>();
        }

        public int VendorId { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string ShortName { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string Eaddress { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string TaxCode { get; set; }
        public string BankAccount { get; set; }
        public Nullable<double> MaxCredit { get; set; }
        public string SpecialInfo { get; set; }
        public string Note { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> MaterialClassifiedId { get; set; }
        public bool Priority { get; set; }

        public virtual ICollection<FuelInventory> FuelInventories { get; set; }
        public virtual ICollection<MaterialInventory> MaterialInventories { get; set; }
        public virtual ICollection<ToolInventory> ToolInventories { get; set; }
        public virtual MaterialClassified MaterialClassified { get; set; }
        public virtual ICollection<ImportPurchaseOrderDetail> ImportPurchaseOrderDetails { get; set; }
        public virtual ICollection<InquiryPo> InquiryPoes { get; set; }
        public virtual ICollection<PlatingForm> PlatingForms { get; set; }
        public virtual ICollection<PoTaxInvoice> PoTaxInvoices { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<EvaluationForm> EvaluationForms { get; set; }
        public virtual ICollection<VendorImg> VendorImgs { get; set; }

    }
}
