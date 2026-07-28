<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.ReportViewer.WebForms" 
Assembly="Telerik.ReportViewer.WebForms, Version=6.1.12.611, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" %>
<%@ Import Namespace="Vfi.Ui.Mvc.Vfi.Areas.Inv.Reports" %>


<%--<%@ Register assembly="Telerik.ReportViewer.WebForms, Version=5.1.11.713, Culture=neutral, PublicKeyToken=A9D7983DFCC261BE" 
namespace="Telerik.ReportViewer.WebForms" tagprefix="telerik" %>--%>

<script runat="server">
        
    public override void VerifyRenderingInServerForm(Control control)
    {
        // to avoid the server form (<form runat="server">) requirement
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
            
        // bind the report viewer
        var rp = new RptAssignProductionTool();
        //rp.ReferenceTypeParam = "M";
        
        //rp.BindDataOwner();
        rp.BindDataOwner(Model);

        rptvPageAssignProductionTool.Report = rp;   
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>
<%--<telerik:ReportViewer ID="rptvPageReportMaterialInvManagement" runat="server">
</telerik:ReportViewer>--%>
<%--
public void BindDataOwner(object models)
        {
            DataSource = models;
        }
--%>


<telerik:ReportViewer ID="rptvPageAssignProductionTool" runat="server" Width="100%" Height="100%" >
</telerik:ReportViewer>



