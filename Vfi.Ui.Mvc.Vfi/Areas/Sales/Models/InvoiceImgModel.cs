using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vfi.Ui.Mvc.Vfi.Areas.Sales.Models
{
    public class InvoiceImgModel
    {
        [DataType("Int")]
        public int ImgId { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        //[DataType("_UploadInvoiceImgTemplate")]
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        public bool CanModify { get; set; }
        public string UploadDate { get { return ModifiedDate.ToString("yyyyMMddhhmmss"); } }


    }
}