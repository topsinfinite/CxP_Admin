<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ManageLabels.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.ManageLabels" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Manage Label Page
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
     <div class="row" runat="server" id="dvMain" visible="false">
        <div class="col-md-4">
               <div class="form-group">
                <label for="inputEmail">Select Organisation <span>*</span></label>
                <asp:DropDownList ID="ddlOrg" runat="server" CssClass="form-control" AppendDataBoundItems="true" >
                     <asp:ListItem Value="" Selected="True">..Select Organisation..</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlOrg" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputEmail">Home Label <span>*</span></label>
                 <asp:TextBox ID="txtHome" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ForeColor="Maroon" ControlToValidate="txtHome" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputbranchname">Category Label(Awesome)</label>
                <asp:TextBox ID="txtCatAwe" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Maroon" ControlToValidate="txtCatAwe" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputbranchname">Category Label(Bad)</label>
                <asp:TextBox ID="txtCatBad" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Maroon" ControlToValidate="txtCatBad" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputbranchname">Service Label(Awesome)</label>
                <asp:TextBox ID="txtSerAwe" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Maroon" ControlToValidate="txtSerAwe" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputbranchname">Service Label(Bad)</label>
                <asp:TextBox ID="txtSerBad" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Maroon" ControlToValidate="txtSerBad" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputbranchname">Service Label(Indiff)</label>
                <asp:TextBox ID="txtSerInd" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Maroon" ControlToValidate="txtSerInd" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
             <div class="form-group">
                <label for="inputbranchname">Staff Label(Awe)</label>
                <asp:TextBox ID="txtStfAwe" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Maroon" ControlToValidate="txtStfAwe" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputbranchname">Staff Label(Bad)</label>
                <asp:TextBox ID="txtStfBad" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Maroon" ControlToValidate="txtStfBad" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
             
            <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            <%-- <button type="submit" class="btn btn-primary">Add</button>--%>
        </div>
    </div>
     <div class="row page-body-control">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">List of Label Settings
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
                            <asp:BoundField HeaderText="#" DataField="ID" Visible="false" />
                          <%--  <asp:BoundField HeaderText="Branch Code" DataField="Code" />--%>
                            <asp:TemplateField HeaderText="Organisation">
                                <ItemTemplate>
                                     <asp:Label ID="lbOrg" runat="server" Text='<%#Eval("Organisation.OrganisationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="Home Label" DataField="HomeLabel" />
                             <asp:BoundField HeaderText="Category Label(Awesome)" DataField="CategoryLabelAwesome" />
                             <asp:BoundField HeaderText="Category Label(Bad)" DataField="CategoryLabelBad" />
                             <asp:BoundField HeaderText="Service Label(Awesome)" DataField="ServiceLabelAwesome" />
                             <asp:BoundField HeaderText="Service Label(Bad)" DataField="ServiceLabelBad" />
                            <asp:BoundField HeaderText="Service Label(Indifferent)" DataField="ServiceLabelIndifferent" />
                            <asp:BoundField HeaderText="Staff Label(Awesome)" DataField="StaffLabelAwesome" />
                            <asp:BoundField HeaderText="Staff Label(Bad)" DataField="StaffLabelBad" />
                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" SelectImageUrl="../images/edit_icon.png" />
                        </Columns>
                        <SelectedRowStyle BackColor="#DBB549" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
