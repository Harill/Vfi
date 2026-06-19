using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public class VendorImgModel
    {
        public int ImgId { get; set; }
        public int VendorId { get; set; }
        public string VendorCode { get; set; }
        public int Type { get; set; }
        [DataType("_UpLoadVendorImgTemplate")]
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public bool CanModify { get; set; }
        public string UploadDate { get { return ModifiedDate.ToString("yyyyMMddhhmmss"); } }


    }
}