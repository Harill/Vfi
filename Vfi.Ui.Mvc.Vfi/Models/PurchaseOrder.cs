using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            this.ImportPurchaseOrders = new List<ImportPurchaseOrder>();
            this.PurchaseOrderDetails = new List<PurchaseOrderDetail>();
            this.TransactionFpts = new List<TransactionFpt>();
        }

        public long PurchaseOrderId { get; set; }
        public int VendorId { get; set; }
        public string RevisionNumber { get; set; }
        public byte Status { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string EmployeeName { get; set; }
        public string Note { get; set; }
        public int Tolerance { get; set; }
        public string CurrencyCode { get; set; }
        public int MaterialClassifiedId { get; set; }
        public string ContractNumber { get; set; }

        public int AddressId { get; set; }
        public int? PaymentId { get; set; }
        public int? ConditionDeliveryId { get; set; }
        public int? DeliveryById { get; set; }

        public int BillToId { get; set; }
        public string BillOfLanding { get; set; }



        public virtual MaterialClassified MaterialClassified { get; set; }
        public virtual ICollection<ImportPurchaseOrder> ImportPurchaseOrders { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<TransactionFpt> TransactionFpts { get; set; }
        public virtual DeliveryAddress DeliveryAddress { get; set; }
    }
}
