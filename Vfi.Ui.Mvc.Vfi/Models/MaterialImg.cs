using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class MaterialImg
    {
        public int MaterialId { get; set; }
        public int ImgId { get; set; }
        public int Type { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        //public string Description { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public virtual Material Material { get; set; }
    }
}
