using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class TransactionFptDetail
    {
        public TransactionFptDetail()
        {
            this.ExportToolDetails = new List<ExportToolDetail>();
        }

        public long DetailId { get; set; }
        public long TransactionId { get; set; }
        public int FptId { get; set; }
        public int VendorId { get; set; }
        public Nullable<int> MachineId { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string UnitMeasure { get; set; }
        public string LotNumber { get; set; }
        public string Note { get; set; }
        public Nullable<int> PoReferenceDetailId { get; set; }
        public Nullable<long> PoDetailId { get; set; }
        public Nullable<bool> IsInternal { get; set; }

        public bool NG { get; set; }
        public bool Lock { get; set; }


        public virtual ICollection<ExportToolDetail> ExportToolDetails { get; set; }
        public virtual PoTaxInvoiceReferenceDetail PoTaxInvoiceReferenceDetail { get; set; }
        public virtual TransactionFpt TransactionFpt { get; set; }



    }
}
