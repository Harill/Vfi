
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Vfi.Server.Core.DataModel.Models.Inv;
using Vfi.Ui.Mvc.Vfi.Areas.Factory.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Models
{
    public class TransactionDetailModel: TransactionDetailDomainModel
    {
        public TransactionDetailModel()
        {
            DefectDetails = new List<DefectTransactionDetailModel>();
        }
        public string Note2 { get; set; }
        [DisplayName(@"Số lô")]
        public string LotNumber { get; set; }
        public double Length { get; set; }
        public string TransactionCode { get; set; }
        [DataType("Number")]
        [DisplayName(@"Đơn giá")]
        public virtual double UnitPrice { get; set; }
        [DataType("Number")]
        [DisplayName(@"Trọng lượng")]
        public virtual double UnitWeight { get; set; }

        public int WarehouseIssueId { get; set; }
        [DisplayName(@"Kho xuất")]
        public string WarehouseIssueName { get; set; }
        //public string WarehouseIssueSetting { get; set; }
        public int WarehouseReceiptId { get; set; }
        [DisplayName(@"Kho nhập")]
        public string WarehouseReceiptName { get; set; }
        //public string WarehouseIssueSetting { get; set; }

        [DisplayName(@"Ngày báo cáo")]
        public DateTime PeriodDate { get; set; }

        [DisplayName(@"Tồn đầu")]
        public double EarlyQuantity { get; set; }
        [DisplayName(@"Tồn cuối")]
        public double LastQuantity { get; set; }
        [DisplayName(@"Xuất?Nhập")]
        public string EoIName { get; set; }
        [DisplayName(@"Loại nguyên liệu")]
        public string MaterialTypeName { get; set; }
        public string Identity { get; set; }
        public new string MaterialCode { get; set; }
        [DisplayName(@"Nhà cung cấp")]
        public string VendorName { get; set; }
        [DisplayName(@"Thông số")]
        public string DesignCode { get; set; }
        public int Status { get; set; }

        public string StoreCode { get; set; }
        public string StatusName
        {
            get
            {
                switch (Status)
                {
                    case 1:
                        return "Chưa cập nhật lỗi";
                    case 2:
                        return "Đã phân";
                    case 3:
                        return "Chưa phân lỗi";
                    default:
                        return "";
                }
            }
        }
        public string MachineName { get; set; }

        public double RecheckQuantity
        {
            get { return DefectDetails.Where(dd => dd.Recheck).Sum(dd => dd.QuantityDefect); }
        }
        public double ReprocessQuantity
        {
            get { return DefectDetails.Where(dd => dd.Reprocess).Sum(dd => dd.QuantityDefect); }
        }
        public double DestroyQuantity
        {
            get { return DefectDetails.Where(dd => dd.Destroy).Sum(dd => dd.QuantityDefect); }
        }
        public List<DefectTransactionDetailModel> DefectDetails { get; set; }

        public bool IsManager { get; set; }
        public bool IsManagerLv2 { get; set; }
        public string ProductImg { get; set; }
        public string UploadDate { get; set; }
        public int Shift { get; set; }

        public long PoDetailId { get; set; }
        public string PoNumber { get; set; }
        public bool IsInternal { get; set; }
        public WorkGroupInfo Info { get; set; }
    }
}