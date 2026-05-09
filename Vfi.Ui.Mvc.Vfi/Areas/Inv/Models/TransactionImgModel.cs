using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Models
{
    public class TransactionImgModel {
        [DataType("Int")]
        public int ImgId { get; set; }
        public long TransactionId { get; set; }
        public string TransactionNumber { get; set; }
        [DataType("_UploadTransactionImgTemplate")]
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public int Index { get; set; }

        public bool CanModify { get; set; }
        public string UploadDate { get { return ModifiedDate.ToString("yyyyMMddhhmmss"); } }


        //public int WarehouseId { get; set; }
        //[DataType("_WarehouseEditTemplate")]
        //public string WarehouseName { get; set; }
    }
}