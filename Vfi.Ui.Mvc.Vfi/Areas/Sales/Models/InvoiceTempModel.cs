using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vfi.Ui.Mvc.Vfi.Areas.Sales.Models
{
    public class InvoiceTempModel
    {
        public InvoiceTempModel()
        {
            TotalAmount = 0;
            TotalAmountAfterTax = 0;
        }
        public int ExportId { get; set; }

        public long OrderId { get; set; }
        [DisplayName("Mã lệnh bán")]
        public string OrderNumber { get; set; }
        [DisplayName("Mã lệnh mua")]
        public string PoNumber { get; set; }

        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }

        [DisplayName("Địa chỉ bill")]
        public string BillToAddress { get; set; }
        [DisplayName("Địa chỉ ship")]
        public string ShipToAddress { get; set; }
        [DisplayName("Đvt")]
        public string CurrencyCode { get; set; }


        public long InvoiceId { get; set; }
        public int PriceListId { get; set; }
        public bool Active { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string InvoiceNumber { get; set; }

        [Required(ErrorMessage = @"Vui lòng nhập % thue.")]
        [Range(0, 100, ErrorMessage = @" 0 < % < 100")]
        [DataType("NumberPossitive")]
        public int TaxPercent { get; set; }

        public string TaxInvoice { get; set; }
        [UIHint("_DateTemplate")]
        public DateTime ShiftmentDate { get; set; }

        public double TotalAmount { get; set; }
        public string TotalAmountFormat
        {
            get
            {
                return string.IsNullOrWhiteSpace(CurrencyCode)
                           ? TotalAmount + ""
                           : CurrencyCode.Equals("VND")
                                 ? string.Format("{0:n0}", TotalAmount)
                                 : string.Format("{0:n3}", TotalAmount);
            }
        }
        public double TotalAmountAfterTax { get; set; }
        public string TotalAmountAfterTaxFormat
        {
            get
            {
                return string.IsNullOrWhiteSpace(CurrencyCode)
                           ? string.Format("{0:n0}", TotalAmountAfterTax)
                           : CurrencyCode.Equals("VND")
                                 ? string.Format("{0:n0}", TotalAmountAfterTax)
                                 : string.Format("{0:n3}", TotalAmountAfterTax);
            }
        }

        public double TotalQuantity { get; set; }

        public string Note { get; set; }
        public double ExchangeRate { get; set; }
        public double TotalAmountAfterTaxVND { get; set; }

        public string EmployeeSale { get; set; }
        public string TransactionCode { get; set; }


    }
}