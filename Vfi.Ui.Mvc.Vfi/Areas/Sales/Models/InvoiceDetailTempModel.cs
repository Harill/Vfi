using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Areas.Sales.Models
{
    public class InvoiceDetailTempModel
    {
        public InvoiceDetailTempModel()
        {
            Amount = 0;
            UnitPrice = 0;
            CurrencyCode="VND";
            ExchangeRate = 1;
            TaxPercent = 0;
        }
        public int ExportId { get; set; }
        public long DetailId { get; set; }
        [DataType("Number")]
        public double Quantity { get; set; }
        public double Receive { get; set; }
        [DataType("Number")]
        public double UnitPrice { get; set; }
        public double Amount { get; set; }
        public string AmountFormat
        {
            get
            {
                return CurrencyCode.Equals("VND")
                           ? string.Format("{0:n0}", Amount)
                           : string.Format("{0:n2}", Amount);
            }
        }
        public double TotalAmount { get { return Amount + (Amount * TaxPercent / 100); } }
        public string TotalAmountFormat {
            get {
                return CurrencyCode.Equals("VND")
                           ? string.Format("{0:n0}", TotalAmount)
                           : string.Format("{0:n2}", TotalAmount);
            }
        }

        [UIHint("_ProductCodeTaxTemplate")]
        public string ProductCode { get; set; }
        public string PONumber { get; set; }

        public string UnitPriceFormat
        {
            get
            {
                return CurrencyCode.Equals("VND")
                                 ? string.Format("{0:n0}", UnitPrice)
                                 : string.Format("{0:n4}", UnitPrice);
            }
        }

        public string Note { get; set; }
        public string TaxInvoiceList { get; set; }
        public string InvoiceNumber { get; set; }
        //[DataType("_DateTemplate")]
        public DateTime ExportedDate { get; set; }
        public string ExportedDateStr { get; set; }
        public DateTime SetupDate { get; set; }

        //
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string CurrencyCode { get; set; }
        public int ProductId { get; set; }

        public string OrderNumber { get; set; }
        public double ExchangeRate { get; set; }
        public int TaxPercent { get; set; }
        public double Tax { get; set; }
        public double TaxVnd { get; set; }
        public double AmountVnd { get; set; }
        public DateTime VFIDueDate { get; set; }
        public string VFIDueDateStr {
            get {
                return VFIDueDate != null
                                 ? string.Format("{0:dd/MM/yyyy}", VFIDueDate)
                                 : "";
            }
        }
        public long OrderDetailId { get; set; }
        public int ExportDetailId { get; set; }
        public bool IsAdd { get; set; }


        public string TransactionCode { get; set; }
        public string ProductCodeDetail { get; set; }
        public int ProductIdDetail { get; set; }
        public string LotNumber { get; set; }
        public double Number { get; set; }
        public string NoteDetail { get; set; }

        public int Index { get; set; }
        public int MaterialId { get; set; }
        public string InfoImg { get; set; }
        public string InfoImg2 { get; set; }
        public string MaterialCode { get; set; }

        public string ImportDate { get; set; }
        public string LastUsedDate { get; set; }
        public string SerialNumber { get; set; }
        public string MachineCode { get; set; }

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



    }


}