
using System;
using System.ComponentModel.DataAnnotations;
using Vfi.Server.Core.DataModel.Models.Purchasing;
using Vfi.Ui.Mvc.Vfi.Models;
using Vfi.Ui.Mvc.Vfi.Models.Production;

namespace Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models
{
    public class PurchaseOrderDetailModel: PurchaseOrderDetailDomainModel
    {
        public PurchaseOrderDetailModel()
        {
            base.UnitPrice = 0;
            base.LineTotal = 0;
            CurrencyCode = "";
            LastPrice = 0;
            SmallestPrice = 0;
        }
        //public string CurrencyCode { get; set; }
        public override double LineTotal
        {
            get { return OrderQty*UnitPrice; }
            set
            {
                base.LineTotal = value;
            }
        }

        public new string LineTotalString
        {
            get
            {
                return CurrencyCode.Equals("VND")
                           ? string.Format("{0:n0}", LineTotal)
                           : string.Format("{0:n2}", LineTotal);
            }
        }

        public new string UnitPriceString
        {
            get
            {
                return CurrencyCode .Equals("VND")
                           ? string.Format("{0:n0}", UnitPrice)
                           : string.Format("{0:n2}", UnitPrice);
            }
        }


        public string Note { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public string ClassifiedName { get; set; }
        public string StatusName { get; set; }
        public MaterialModel MaterialModel { get; set; }
        public FuelModel FuelModel { get; set; }
        public ToolModel ToolModel { get; set; }


        public bool IsPurchaseManager { get; set; }

        public double LastPrice { get; set; }
        public DateTime LastPoDate { get; set; }
        /// <summary>
        /// in 3 month
        /// </summary>
        public double SmallestPrice { get; set; }
        public DateTime SmallestPricePoDate { get; set; }
        public string SpecificNote {
            get {
                return LastPrice + SmallestPrice > 0
                    ? LastPrice == SmallestPrice
                        ? "ĐG gần nhất nhỏ nhất:" + LastPrice + "-" + LastPoDate.ToString("dd/MM/yyyy")
                        : LastPrice > 0 && SmallestPrice > 0
                            ? "ĐG gần nhất:" + LastPrice + "-" + LastPoDate.ToString("dd/MM/yyyy")
                                + "| ĐG nhỏ nhất:" + SmallestPrice + "-" + SmallestPricePoDate.ToString("dd/MM/yyyy")
                            : LastPrice > 0
                                ? "ĐG gần nhất:" + LastPrice + "-" + LastPoDate.ToString("dd/MM/yyyy")
                                : "ĐG nhỏ nhất:" + SmallestPrice + "-" + SmallestPricePoDate.ToString("dd/MM/yyyy")
                    : "";
            }
        }


        //public string SpecificNote { get; set; }
        public InquiryPoModel Inquiry { get; set; }
        public string InquiryNote {
            get {
                var note = "";
                if (Inquiry == null) return note;
                note = Inquiry.InquiryNumber;
                if (Inquiry.DueDate != null) {
                    note += "-" + Inquiry.DueDate.Value.ToString("dd/MM/yy");
                }
                return note;
            }
        }

        public WorkGroupInfo Info { get; set; }
    }
}