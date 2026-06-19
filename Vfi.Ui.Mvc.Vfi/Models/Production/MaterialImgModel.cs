using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vfi.Ui.Mvc.Vfi.Models.Production
{
    public class MaterialImgModel
    {
        [DataType("Int")]
        public int ImgId { get; set; }
        public int MaterialId { get; set; }
        //public string TransactionNumber { get; set; }
        [DataType("_UploadMaterialImgTemplate")]
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public int Type { get; set; }

        public bool CanModify { get; set; }
        public string UploadDate { get { return ModifiedDate.ToString("yyyyMMddhhmmss"); } }
    }
}