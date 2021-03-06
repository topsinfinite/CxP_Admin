﻿<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ManageService.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.ManageService" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Manage Service Page
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
                <asp:DropDownList ID="ddlOrg" runat="server" CssClass="form-control" AppendDataBoundItems="true" >
                     <asp:ListItem Value="" Selected="True">..Select Organisation..</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlOrg" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputbranchname">Service Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Maroon" ControlToValidate="txtName" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
             <div class="form-group">
                <label for="inputbranchname">Category Type:</label>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                     <asp:ListItem Value="" Selected="True">..Select Service Category..</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlType" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="checkbox">
                <label>
                    <asp:CheckBox ID="chk" runat="server" Text="Is Active" /></label>
            </div>
            <%--<div class="form-group icon-display" runat="server" id="dvUpload">
                 <label for="inputbranchname">select Icon:</label>
            <asp:ListView ID="lstSrvIcon" runat="server" ItemPlaceholderID="itmContainer" >
            <LayoutTemplate>
                <ul>
                    <asp:PlaceHolder ID="itmContainer" runat="server"></asp:PlaceHolder>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
             <li>
                   <input id="c1"  runat="server" value='<%# Eval("Name") %>' type="checkbox" />
                    <asp:Image ID="lnkbtn" CssClass="img-responsive" runat="server"  ImageUrl='<%#Eval("Name","~/Upload/{0}") %>' />
                </li>   
            </ItemTemplate>
            <EmptyDataTemplate>
                <div>Sorry no data found!!!</div>
            </EmptyDataTemplate>
        </asp:ListView>
                <%--<label for="inputbranchname">Upload Icon:</label>
                 <asp:FileUpload ID="fileUploadDoc" runat="server" CssClass="form-control" />
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator22"  runat="server" ForeColor=Maroon ControlToValidate="fileUploadDoc" Display="Dynamic" Text="Field is required"></asp:RequiredFieldValidator>  
                <br />
                   <small style="color:maroon">file must be in required format: *jpeg,*jpg,*png. Required file size 4MB max</small>
            </div>--%>
           
            <br />
            <div class="form-group" style="clear:both;margin-top:8px">
            <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-primary"  OnClick="btnSubmit_Click" />
            <%-- <button type="submit" class="btn btn-primary">Add</button>--%>
                    </div>
        </div>
         <div class="col-md-8 icon-display" runat="server" ID="dvEdit" visible="true">
              <label for="inputbranchname">select Icon:</label>
            <asp:ListView ID="ListViewUpdate" runat="server" ItemPlaceholderID="itmContainer" >
            <LayoutTemplate>
                <ul>
                    <asp:PlaceHolder ID="itmContainer" runat="server"></asp:PlaceHolder>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
             <li>
                   <input id="c1"  runat="server" value='<%# Eval("Name") %>' type="checkbox" />
                    <asp:Image ID="lnkbtn" CssClass="img-responsive" runat="server"  ImageUrl='<%#Eval("Name","~/Upload/{0}") %>' />
                </li>   
            </ItemTemplate>
            <EmptyDataTemplate>
                <div>Sorry no data found!!!</div>
            </EmptyDataTemplate>
        </asp:ListView>
           <%-- <asp:Image ID="Image1" runat="server"  Height="120px" Width="120px" ImageAlign="Middle" />
              <div class="form-group" >
                <label for="inputbranchname">Upload Icon:</label>
                 <asp:FileUpload ID="fileUpload1" runat="server" CssClass="form-control" />
                   
                <br />
                   <small style="color:maroon">file must be in required format: *jpeg,*jpg,*png. Required file size 4MB max</small>
            </div>--%>
             <asp:Button ID="btnUpdate" runat="server" Visible="false" Text="Update Icon" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
        </div>
    </div>
      <div class="row page-body-control">
        <div class="col-md-8">
            <div class="panel panel-default">
                <div class="panel-heading">List of Services
                    <div class="form-horizontal" style="float: right; margin-top: -5px; margin-right: 2px;" runat="server" visible="false" id="dvfilter">
                         <div class="form-group">
                             <asp:DropDownList ID="ddlorgfilter" runat="server" CssClass="form-control-inline" AppendDataBoundItems="true">
                                 <asp:ListItem Value="0" Selected="True">..Select All..</asp:ListItem>
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
                            <asp:BoundField HeaderText="#" DataField="ID" />
                           <%-- <asp:BoundField HeaderText="Branch Code" DataField="Code" />--%>
                             <asp:BoundField HeaderText="Service Name" DataField="Name" />
                              <asp:TemplateField HeaderText="Icon">
                                   <ItemTemplate>
                                     <asp:Image ID="Image1" runat="server" ImageUrl='<%#(Eval("ServiceIcon") != null)?Eval("ServiceIcon","~/Upload/{0}"):Eval("ServiceIcon","~/Upload/default.png") %>' Height="60px" Width="60px" />
                                   </ItemTemplate>
                                 </asp:TemplateField>
                             <asp:TemplateField HeaderText="Category Type">
                                <ItemTemplate>
                                     <asp:Label ID="lbtype" runat="server" Text=' <%#GetType(Eval("ServiceCatId"))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Organisation">
                                <ItemTemplate>
                                     <asp:Label ID="lbOrgID" runat="server" Text='<%#Eval("OrganisationID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbOrg" runat="server" Text='<%#Eval("Organisation.OrganisationName") %>'></asp:Label>
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
                        <PagerStyle CssClass="pagination" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
