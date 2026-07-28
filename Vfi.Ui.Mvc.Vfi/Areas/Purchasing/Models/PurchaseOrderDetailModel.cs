
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

        public string Standard { get; set; }
        public string Employee { get; set; }
        public string QuotationNumber { get; set; }
        public string DeliveryMethodName { get; set; }
        public string ShipMethodName { get; set; }
        public string PaymentMethodName { get; set; }

        public string ManagerNote { get; set; }
        // Address
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public string Address { get; set; }
        public string AddressFullName { get; set; }
        public string Recipient { get; set; }
        public string Tel { get; set; }
        public DateTime? OrderDate { get; set; }

        // BillTo 
        public int BillToId { get; set; }
        public string BillToAddress { get; set; }
        public string BillToFullName { get; set; }
        public string ReceiptBill { get; set; }
        public string ReceiptBillPhoneNumber { get; set; }


        // Vendor
        public int VendorNo { get; set; }
        public string RevisionNumber { get; set; }
        public string VendorAddress { get; set; }
        public string VendorPhone { get; set; }
        public string Year { get; set; }
        public string VendorCompanyName { get; set; }
        public string VendorContactName { get; set; }



        public bool Active { get; set; }
        public string ActiveString {
            get {
                return Active == false
                    ? "(PENDING)"
                    : Active == true && Status == 3
                        ? "(CANCELED)"
                        : "(APPROVED)";
            }
        }

        public int Status { get; set; }

        public DateTime OrderDateCheck {
            get {
                return OrderDate == null ? DateTime.Today : OrderDate.Value;
            }
        }
        public string Creater { set; get; }

        public bool NG { get; set; }

    }
}