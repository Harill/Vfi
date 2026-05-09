using System;
using System.Collections.Generic;

namespace Vfi.Ui.Mvc.Vfi.Models
{
    public partial class TransactionImg
    {
        public long TransactionId { get; set; }
        public int ImgId { get; set; }
        public string TransactionNumber { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
