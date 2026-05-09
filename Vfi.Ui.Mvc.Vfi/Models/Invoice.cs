using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class Invoice {
        public Invoice() {
            this.InvoiceDetails = new List<InvoiceDetail>();
            this.OrderNotes = new List<OrderNote>();
        }

        public long InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public Nullable<int> PriceListId { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ExportId { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<long> OrderId { get; set; }
        public string TaxInvoice { get; set; }
        public int TaxPercent { get; set; }
        public Nullable<System.DateTime> ShipmentDate { get; set; }
        public Nullable<byte> Status { get; set; }
        public string Note { get; set; }
        public int ExchangeRate { get; set; }
        public bool FinishDesign { get; set; }


        public virtual ExportFormTP_KD ExportFormTP_KD { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<OrderNote> OrderNotes { get; set; }

    }
}
