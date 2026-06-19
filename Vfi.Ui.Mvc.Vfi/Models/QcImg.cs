using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models 
{
    public partial class QcImg {
        public int ImgId { get; set; }
        public int ProductId { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }

        public virtual Product Product { get; set; }
    }
}
