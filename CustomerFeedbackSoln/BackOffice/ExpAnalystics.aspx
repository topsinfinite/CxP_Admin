<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ExpAnalystics.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.ExpAnalystics" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" >
        <ContentTemplate>
            <div class="row">
        <div class="col-md-12">
            <!--toggle sidebar button-->
            <div class="alert alert-danger" runat="server" id="error" visible="false">
                <a href="#" class="close" data-dismiss="alert">&times;</a>
                <strong>Error!</strong> A problem has been occurred while submitting your data.
            </div>
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>
             
              <h1 class="page-header">Analytics and Reports  </h1>
             
              <div class="alert alert-info">
               <span class="glyphicon glyphicon-calendar"></span>  <asp:Label ID="lbview" runat="server" Font-Size="14px" ForeColor="#708512" Font-Bold="true" Text=""></asp:Label> 
            </div>
              <div class="panel panel-default">
             <div class="panel-heading">
                    <div class="form-inline" role="form">
                         
                        <div class="form-group" runat="server" id="dvOrg" visible="true">
                            <asp:DropDownList ID="ddlOrg" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged" >
                                <asp:ListItem Value="" Selected="True">..Select Organisation..</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="form-group"  runat="server" id="dvEvent" visible="true">
                            <asp:DropDownList ID="ddlEvent" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged" >
                                <asp:ListItem Value="" Selected="True">..Select Event..</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                          <div class="form-group"  runat="server" id="dvMetrics" visible="true">
                            <asp:DropDownList ID="ddlMetrics" runat="server" CssClass="form-control" AppendDataBoundItems="true"  >
                                <asp:ListItem Value="" Selected="True">..Select Event Metrics..</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:Button ID="btnFilter" Cssclass="btn btn-default" Text="Generate Report" runat="server" OnClick="btnFilter_Click"></asp:Button>
                    </div>
                </div>
                    <div class="panel-body">
                          <div class="row placeholders">
                <div class="col-xs-6 col-sm-6 placeholder text-center">
                     <div class="table-responsive">
                        <rsweb:ReportViewer ID="ReportViewer1" ShowToolBar="false" Width="100%" Height="300px" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Report\EventOverallRatingPieChart.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="dsFeedback" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData"
                            TypeName="CustomerFeedbackSoln.BackOffice.dsFeedbackTableAdapters.EventOverallRatingTableAdapter" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:Parameter Name="eventID" Type="Int32" />
                                <asp:Parameter Name="orgId" Type="Int32" />
                            </SelectParameters>
                             
                         </asp:ObjectDataSource>

                    </div>
                </div>
                <div class="col-xs-6 col-sm-6 placeholder text-center">
                    <div class="table-responsive">
                        <rsweb:ReportViewer ID="ReportViewer2" Width="100%" Height="300px"   ShowToolBar="false" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Report\EventOverallRatingBarChart.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsFeedback" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" 
                             TypeName="CustomerFeedbackSoln.BackOffice.dsFeedbackTableAdapters.EventOverallRatingTableAdapter" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:Parameter Name="eventID" Type="Int32" />
                                <asp:Parameter Name="orgId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        
                    </div>
                </div>
                  
            </div>
             <div class="row placeholders">
                <div class="col-xs-6 col-sm-6 placeholder text-center">
                   
                     <div class="table-responsive">
                        <rsweb:ReportViewer ID="ReportViewer4" ShowToolBar="false" Width="100%" Height="300px" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Report\EventMetricsRatingBarChart.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource4" Name="dsFeedback" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="GetData"
                            TypeName="CustomerFeedbackSoln.BackOffice.dsFeedbackTableAdapters.EventMetricRatingTableAdapter" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:Parameter Name="eventID" Type="Int32" />
                                <asp:Parameter Name="orgId" Type="Int32" />
                            </SelectParameters>
                             
                         </asp:ObjectDataSource>

                    </div>
                </div>
                <div class="col-xs-6 col-sm-6 placeholder text-center">
                    <div class="table-responsive">
                        <rsweb:ReportViewer ID="ReportViewer5" Width="100%" Height="300px" AsyncRendering="true"  ShowToolBar="false" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Report\EventMetricElementsBarChart.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource5" Name="dsFeedback" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" SelectMethod="GetData" 
                            TypeName="CustomerFeedbackSoln.BackOffice.dsFeedbackTableAdapters.EventMetricElementRptTableAdapter" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:Parameter Name="eventID" Type="Int32" />
                                <asp:Parameter Name="orgId" Type="Int32" />
                                <asp:Parameter Name="metricId" Type="Int32" />
                            </SelectParameters>
                             

                        </asp:ObjectDataSource>
                    </div>
                </div>
                  
            </div>

          </div>
      </div>
          
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ForeColor="Maroon" />
           
            <div class="panel panel-default">
                 <div class="panel-heading">
                      <h2 class="sub-header">Detail Report</h2>
                 </div>
                <div class="panel-body">
                    <div class="row placeholders">
                        <div class="col-xs-12 col-sm-12 placeholder text-center">
                      <rsweb:ReportViewer ID="ReportViewer3" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                        <LocalReport ReportPath="Report\EventFeedbackCommentRpt.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource3" Name="dsFeedback" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server"  SelectMethod="GetData" 
                         TypeName="CustomerFeedbackSoln.BackOffice.dsFeedbackTableAdapters.EventFeedbackCommentTableAdapter" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:Parameter Name="eventId" Type="Int32" />
                                 
                            </SelectParameters>
                        
                    </asp:ObjectDataSource>
                        </div>
                    </div>
                    <div>
                        <div></div>
                        <div></div>
                    </div>
                    
                </div>
            </div>
           
        </div>
    </div>
        </ContentTemplate>
        <Triggers>
          <asp:PostBackTrigger ControlID="btnFilter" />
    </Triggers>
    </asp:UpdatePanel>
    
</asp:Content>
