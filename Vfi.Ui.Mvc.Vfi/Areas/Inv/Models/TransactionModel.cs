
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vfi.Server.Core.DataModel.Models.Inv;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Models {
    public class TransactionModel {
        public long TransactionId { get; set; }

        public long? StockOrderId { get; set; }

        [DisplayName("Mã lệnh")]
        public virtual string StockOrderCode { get; set; }

        [DisplayName("Kho xuất")]
        public int? WarehouseIssueId { get; set; }

        [DisplayName("Kho xuất")]
        public virtual string WarehouseIssueName { get; set; }

        [DisplayName("Kho nhập")]
        public int? WarehouseReceiptId { get; set; }

        [DisplayName("Kho nhập")]
        public string WarehouseReceiptName { get; set; }

        [DisplayName("Mã giao dịch")]
        public string TransactionCode { get; set; }

        [DisplayName("Xuất ? Nhập")]
        public string EoI { get; set; }

        [DisplayName("Xuất ? Nhập")]
        public string EoIName { get; set; }

        [DisplayName("Nliệu ? Tphẩm")]
        public bool MoP { get; set; }

        [DisplayName("Người tạo")]
        [DataType(DataType.Date)]
        public string CreatedUser { get; set; }

        [DisplayName("Ngày")]
        //[DataType(DataType.Date)]
        [UIHint("_DateTemplate")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Trạng thái")]
        public byte Status { get; set; }

        [DisplayName("Tình trạng")]
        public string StatusName { get; set; }

        public bool CanApprove { get; set; }
        public string LotNumber { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Kích hoạt")]
        public bool Active { get; set; }

        [DisplayName("Người thay đổi")]
        public string ModifiedUser { get; set; }

        [DisplayName("Ngày thay đổi")]
        public DateTime ModifiedDate { get; set; }

        [DisplayName("Tổng số lượng(cây)")]
        public double TotalQuality { get; set; }

        [DisplayName("Tổng số lượng(Kg)")]
        public double TotalQualityKg { get; set; }

        public string SpecialNote { get; set; }

        public int AlertColor { get; set; }

        public int PurchasingSignatureType { get; set; }

        public double ExchangeRate { get; set; }

        public int InvManager { get; set; }

        public int SpecialFormType { get; set; }

        public bool IsInternal { get; set; }
        public bool Highlight { get; set; }

        public bool HaveNG { get; set; }
        public bool HaveLock { get; set; }
    }


}