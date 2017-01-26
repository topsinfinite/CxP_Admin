<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.UserDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">User Detail Page
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
        <div class="col-md-12">

       <asp:HiddenField ID="hid" runat="server" />
       <asp:GridView ID="gvheader" runat="server" Width="100%" ShowHeader="false" EnableTheming="false" EmptyDataText="No Record found" GridLines="None" 
        AutoGenerateColumns="False"> 
        <Columns>
             <asp:TemplateField>
              <ItemTemplate>
              
               <asp:Label ID="lbInit" runat="server" Text='<%# Eval("UserName")%>' Visible="false" />
               <%-- <div>
                     
                    <h4>Username:       <span class="label label-default"><%#Eval("UserName")%></span></h4>
                    <h4>Full Name:      <span class="label label-default"> <%# Eval("FullName")%></span> </h4>
                </div>--%>
               <table border="0" class="table table-striped">
                <tr>
                <td class="detail-title">User Name:</td><td> <strong><%#Eval("UserName")%> </strong></td>
                <td class="detail-title">Full Name:</td><td> <strong><%# Eval("FullName")%> </strong></td>
                </tr>
                 <tr>
                <td class="detail-title">Branch:</td> <td><strong><%# Eval("Branch.Name")%> </strong> <asp:Label ID="lbDept" runat="server" Text=' <%# Eval("BranchID")%>' Visible="false"></asp:Label></td>
                <td class="detail-title">Email:</td><td><strong><%#Eval("Email")%></strong></td
                </tr>
                <tr>
                <td class="detail-title">Added By:</td><td><strong><%# Eval("AddedBy")%></strong></td>
                 <td class="detail-title">Date Added:</td><td><strong><%#Eval("DateAdded","{0:dd-MM-yyyy hh:mm:ss tt}")%></strong></td>
                </tr>
                <tr>
                  <td class="detail-title"> User Status:</td><td><strong>
                   <asp:Label ID="lbEbl" runat="server" Text=' <%# Eval("IsActive")%>' Visible="false"></asp:Label>
                  <asp:Label ID="lbEnable" runat="server" Text=' <%# (Eval("IsActive") != null&&Boolean.Parse(Eval("isActive").ToString())) ? "Active" : "Inactive"%>' ForeColor="Maroon"></asp:Label></strong></td>
                  <td class="detail-title">User Role:</td><td style="color:Maroon"><strong><asp:Label ID="lbrole" runat="server" Text= '<%# Eval("RoleName")%>'></asp:Label></strong></td>
                </tr>
                    <tr>
                <td class="detail-title">Organisation:</td> <td><strong><%# Eval("Branch.Area.Region.Organisation.OrganisationName")%> </td>
                <td class="detail-title">Area:</td><td><strong><%#Eval("Branch.Area.Name")%></strong></td
                </tr>
               </table>
               
                 </ItemTemplate>
             </asp:TemplateField>
        </Columns>
    </asp:GridView>
            
            <div class="alert">
                <asp:Button ID="btnEdit" runat="server" Text="Edit User" CausesValidation="false"  CssClass="btn btn-primary" OnClick="btnEdit_Click"/>&nbsp;&nbsp<asp:Button ID="btnAct" runat="server" Text="Activate/Deactivate" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAct_Click" /></div>
        </div>
    </div>
   
    <div class="row" runat="server" id="dvDet" visible="false">
        <div class="col-md-8">
           <h3 class="page-header">Update User Details:</h3>
             <div class="form-horizontal">
                <div class="form-group">
                    <label for="inputbranchname" class=" col-md-2 control-label">Staff Name<span>*</span></label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtfname" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Maroon" ControlToValidate="txtfname" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputbranchname" class="col-md-2 control-label">Email<span>*</span></label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Maroon" ControlToValidate="txtEmail" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegEmail" runat="server" ForeColor="Maroon" ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="cat"></asp:RegularExpressionValidator>
                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail" class="col-md-2 control-label">Select Role<span>*</span></label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                            <asp:ListItem Value="" Selected="True">..Select Role..</asp:ListItem>
                            <asp:ListItem Value="Administrator">Administrator</asp:ListItem>
                            <asp:ListItem Value="OrgAdmin">Organisation Admin</asp:ListItem>
                            <asp:ListItem Value="AreaManager">Area Manager</asp:ListItem>
                            <asp:ListItem Value="BranchManager">Branch Manager</asp:ListItem>
                            <asp:ListItem Value="BranchOperator">Branch Operator </asp:ListItem>
                            <asp:ListItem Value="BranchUser">Branch User </asp:ListItem>
                            <asp:ListItem Value="HeadOfficeAdmin">Head Office Admin </asp:ListItem>
                            <asp:ListItem Value="RegionalManager">Regional Manager  </asp:ListItem>

                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlRole" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                </div>
              <div class="form-group">
                <label for="inputEmail"  class="col-md-2 control-label">Select Organisation <span>*</span></label>
                   <div class="col-md-6">
                <asp:DropDownList ID="ddlOrg" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged">
                     <asp:ListItem Value="" Selected="True">..Select Organisation..</asp:ListItem>
                </asp:DropDownList>
                       </div>
                   <div class="col-md-2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlOrg" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                        </div>
                <div class="form-group">
                    <label for="inputEmail" class="col-md-2 control-label">Select Branch <span>*</span></label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <asp:ListItem Value="" Selected="True">..Select Branch..</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlBranch" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                </div>

            <div class="form-group">
                <label for="inputbranchname" class="col-md-2 control-label"></label>
                <div class="col-md-6">
                    <asp:Button ID="btnSubmit" runat="server" Text="Update User" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                </div>
            </div>


            </div>
        </div>
    </div>
         </ContentTemplate>
      </asp:UpdatePanel>
</asp:Content>
