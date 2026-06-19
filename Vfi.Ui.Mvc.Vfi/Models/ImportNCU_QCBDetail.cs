using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class ImportNCU_QCBDetail
    {
        public int DetailId { get; set; }
        public int ImportId { get; set; }
        public int ProductId { get; set; }
        public double RequestNumber { get; set; }
        public double RealNumber { get; set; }
        public string Note { get; set; }
        public double Weight { get; set; }
        public Nullable<int> ExportDetailId { get; set; }
        public string Package { get; set; }
        public Nullable<int> ProductInvId { get; set; }
        public Nullable<long> TransactionDetailId { get; set; }

        public virtual ExportGCN_NCUDetail ExportGCN_NCUDetail { get; set; }
        public virtual ImportNCU_QCB ImportNCU_QCB { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductInventory ProductInventory { get; set; }
        public virtual TransactionDetail TransactionDetail { get; set; }
    }
}
