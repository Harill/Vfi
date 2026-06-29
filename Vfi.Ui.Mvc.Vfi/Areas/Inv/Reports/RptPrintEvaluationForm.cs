using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Vfi.Ui.Mvc.Vfi.Areas.Purchasing.Models;

namespace Vfi.Ui.Mvc.Vfi.Areas.Inv.Reports
{
    /// <summary>
    /// Summary description for RptMaterialInvManagement.
    /// </summary>
    public partial class RptPrintEvaluationForm : Telerik.Reporting.Report
    {
        public RptPrintEvaluationForm()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            //this.Name = "NhapCC";
            //this.pictureBox1.Value = Properties.Resources.ql_muahang;
        }
        
        public void BindDataOwner(object models)
        {
            DataSource = models;
            var list = (List<PrintEvaluationForm>)models;
            var data = list.FirstOrDefault();
            this.picLogo.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.picLogo.Value = data.Info.Logo;
        }
    }
}