﻿<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ManageBranch.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.ManageBranch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Manage Branch Page
          </h1>
            <div class="alert alert-danger" runat="server" id="error" visible="false">
                <a href="#" class="close" data-dismiss="alert">&times;</a>
                <strong>Error!</strong> A problem has been occurred while submitting your data.
            </div>
            <div class="alert alert-success" runat="server" id="success" visible="false">
                <a href="#" class="close" data-dismiss="alert">&times;</a>
                <strong>Success!</strong> Your message has been sent successfully.
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
                <div class="form-group">
                <label for="inputEmail">Select Organisation <span>*</span></label>
                <asp:DropDownList ID="ddlOrg" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged">
                     <asp:ListItem Value="" Selected="True">..Select Organisation..</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlOrg" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
                <div class="form-group">
                <label for="inputEmail">Select Area <span>*</span></label>
                <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                     <asp:ListItem Value="" Selected="True">..Select Area..</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlArea" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputEmail">Branch Code <span>*</span></label>
                <asp:TextBox ID="txtcode" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ForeColor="Maroon" ControlToValidate="txtcode" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputbranchname">Branch Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Maroon" ControlToValidate="txtName" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="checkbox">
                <label>
                    <asp:CheckBox ID="chk" runat="server" Text="Is Active" /></label>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            <%-- <button type="submit" class="btn btn-primary">Add</button>--%>
        </div>
    </div>
         </ContentTemplate>
      </asp:UpdatePanel>
    <div class="row page-body-control">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">List of Organisations
                    <div class="form-horizontal" style="float: right; margin-top: -5px; margin-right: 2px;" runat="server" visible="false" id="dvfilter">
                         <div class="form-group">
                             <asp:DropDownList ID="ddlorgfilter" runat="server" CssClass="form-control-inline" AppendDataBoundItems="true">
                                 <asp:ListItem Value="" Selected="True">..Select Organisation..</asp:ListItem>
                             </asp:DropDownList>
                             <asp:Button ID="btnSearch" runat="server" Text="Filter" CssClass="btn" CausesValidation="false"
                                 OnClick="btnSearch_Click" />
                             <asp:Button ID="btnClr" runat="server" CausesValidation="false"
                                 Text="Clr" CssClass="btn" OnClick="btnClr_Click" />

                         </div>

                     </div>
                </div>
                <div class="panel-body"> <asp:HiddenField ID="hid" runat="server" /><asp:HiddenField ID="hidkey" runat="server" />
                    <asp:GridView ID="gvDefault" runat="server" GridLines="None"
                        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID"
                        AllowPaging="true" PageIndex="0" PageSize="15" EmptyDataText="No Record Found" OnSelectedIndexChanged="gvDefault_SelectedIndexChanged" OnPageIndexChanging="gvDefault_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="#" DataField="ID"  />
                            <asp:BoundField HeaderText="Branch Code" DataField="Code" />
                             <asp:BoundField HeaderText="Branch Name" DataField="Name" />
                              <asp:TemplateField HeaderText="Area">
                                <ItemTemplate>
                                     <asp:Label ID="lbAreaID" runat="server" Text='<%#Eval("AreaID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="Area" runat="server" Text='<%#Eval("Area.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                     <asp:Label ID="lbRegionID" runat="server" Text='<%#Eval("Area.RegionalID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbRegion" runat="server" Text='<%#Eval("Area.Region.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Organisation">
                                <ItemTemplate>
                                     <asp:Label ID="lbOrgID" runat="server" Text='<%#Eval("Area.Region.OrganisationID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbOrg" runat="server" Text='<%#Eval("Area.Region.Organisation.OrganisationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                     <asp:Label ID="lbEnable" runat="server" Text=' <%# (Eval("isActive") != null&&Boolean.Parse(Eval("isActive").ToString())) ? "Active" : "Inactive"%>' ForeColor="Maroon"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" SelectImageUrl="../images/edit_icon.png" />
                        </Columns>
                        <SelectedRowStyle BackColor="#DBB549" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
