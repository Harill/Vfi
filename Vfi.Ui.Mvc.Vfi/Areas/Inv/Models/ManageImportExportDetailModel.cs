using System.ComponentModel.DataAnnotations;
using System;



namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Models
{
    public class ManageImportExportDetailModel
    {
        public int? PlatingDetailId { get; set; }
        public long DetailId { get; set; }
        public int ProductId { get; set; }
        [DataType("_ProductEditTemplate")]
        public string ProductCode { get; set; }
        public double Number { get; set; }
        public double Weight { get; set; }
        public string Note { get; set; }
        public string Package { get; set; }
        public long TransactionId { get; set; }
        public int FormId { get; set; }
        public int FormType { get; set; }
        public bool CanEdit { get; set; }
        public string LotNumber { get; set; }
        public int Index { get; set; }
        public string MachineName { get; set; }

        public DateTime StartDate { get; set; }
        public string StartDateStr {
            get {
                return StartDate == DateTime.MinValue
                                 ? ""
                                 : string.Format("{0:HH:mm dd/MM/yyyy}", StartDate);
            }
        }


        public DateTime? EndDate { get; set; }
        public string EndDateStr {
            get {
                return EndDate != null
                                 ? string.Format("{0:HH:mm dd/MM/yyyy}", EndDate)
                                 : "";
            }
        }
        public string Eol { set; get; }
    }
}