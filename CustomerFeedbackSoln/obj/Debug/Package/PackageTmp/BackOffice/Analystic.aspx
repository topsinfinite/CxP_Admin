<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="Analystic.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.Analystic" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <!--toggle sidebar button-->
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Analytics and Reports  </h1>
         
              <div class="alert alert-info">
              <asp:Label ID="lbview" runat="server" Font-Size="14px" ForeColor="#708512" Font-Bold="true" Text=""></asp:Label> <span class="glyphicon glyphicon-calendar"></span>(<asp:Label ID="lbrange" runat="server" Text=""></asp:Label>)
            </div>
              
             <div class="alert alert-danger" runat="server" id="error" visible="false">
                <a href="#" class="close" data-dismiss="alert">&times;</a>
                <strong>Error!</strong> A problem has been occurred while submitting your data.
            </div>
            <div class="row placeholders">
                <div class="col-xs-6 col-sm-6 placeholder text-center">
                     <div class="table-responsive">
                        <rsweb:ReportViewer ID="ReportViewer1" ShowToolBar="false" Width="100%" Height="300px" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Report\SmileyRatingPieChart.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="dsFeedback" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData"
                            TypeName="CustomerFeedbackSoln.BackOffice.dsFeedbackTableAdapters.SmileyActionRatingTableAdapter">
                             
                         </asp:ObjectDataSource>

                    </div>
                </div>
                <div class="col-xs-6 col-sm-6 placeholder text-center">
                    <div class="table-responsive">
                        <rsweb:ReportViewer ID="ReportViewer2" Width="100%" Height="300px" AsyncRendering="true"  ShowToolBar="false" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Report\SmileyActionSvcRating.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsFeedback" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="dsFeedbackTableAdapters.SmileyActionServiceRatingTableAdapter"></asp:ObjectDataSource>
                       <%-- <rsweb:ReportViewer ID="ReportViewer2" ShowToolBar="false" Width="100%"  Height="300px" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Report\SmileyActionSvcRating.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsFeedback" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
                            TypeName="CustomerFeedbackSoln.BackOffice.dsFeedbackTableAdapters.SmileyActionServiceRatingTableAdapter"></asp:ObjectDataSource>--%>

                    </div>
                </div>
                  
            </div>
          <hr />
            <div class="row placeholders">
                <div class="col-xs-6 col-sm-6 placeholder text-center">
                   
                     <div class="table-responsive">
                        <rsweb:ReportViewer ID="ReportViewer5" ShowToolBar="false" Width="100%" Height="300px" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Report\SmileyActionCatRating.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource4" Name="dsFeedback" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="GetData"
                            TypeName="CustomerFeedbackSoln.BackOffice.dsFeedbackTableAdapters.SmileyActionCategoryRatingTableAdapter">
                             
                         </asp:ObjectDataSource>

                    </div>
                </div>
                <div class="col-xs-6 col-sm-6 placeholder text-center">
                    <div class="table-responsive">
                        <rsweb:ReportViewer ID="ReportViewer4" Width="100%" Height="300px" AsyncRendering="true"  ShowToolBar="false" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Report\SmileyActionStfRating.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource5" Name="dsFeedback" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" SelectMethod="GetData" TypeName="dsFeedbackTableAdapters.SmileyActionStaffRatingTableAdapter"></asp:ObjectDataSource>
                    </div>
                </div>
                  
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" ForeColor="Maroon" />
            <h2 class="sub-header">Detail Report</h2>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="form-inline" role="form">
                        <div class="form-group">
                            <label for="from">From:</label>
                             <asp:TextBox ID="txtfrom" runat="server" CssClass="form-control" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Maroon" ErrorMessage="From Date is required" ControlToValidate="txtfrom" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                             <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/cal.gif" />
                             <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtfrom" PopupButtonID="Image2"></asp:CalendarExtender> 
                        </div>
                        <div class="form-group">
                            <label for="To">To:</label>
                            <asp:TextBox ID="txtto" runat="server" CssClass="form-control" ></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Maroon" ErrorMessage="To Date is required" ControlToValidate="txtto" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/cal.gif" />
                             <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtto" PopupButtonID="Image1"></asp:CalendarExtender> 
                        </div>
                        <div class="form-group" runat="server" id="dvRegion" visible="false">
                            <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" AppendDataBoundItems="true" >
                                <asp:ListItem Value="" Selected="True">..Select Region..</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="form-group"  runat="server" id="dvArea" visible="false">
                            <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control" AppendDataBoundItems="true" >
                                <asp:ListItem Value="" Selected="True">..Select Area..</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="form-group"  runat="server" id="dvBranch" visible="false">
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="true" >
                                <asp:ListItem Value="" Selected="True">..Select Branch..</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:Button ID="btnFilter" Cssclass="btn btn-default" Text="Filter" runat="server" OnClick="btnFilter_Click"></asp:Button>
                    </div>
                </div>
                <div class="panel-body">
                    <rsweb:ReportViewer ID="ReportViewer3" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                        <LocalReport ReportPath="Report\FeedbackCommentRpt.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource3" Name="dsFeedback" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server"  SelectMethod="GetData" TypeName="CustomerFeedbackSoln.BackOffice.dsFeedbackTableAdapters.FeedbackCommentTableAdapter">
                        
                    </asp:ObjectDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
