<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Change Password Page
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
                <label for="inputbranchname">Current Password</label>
               <asp:TextBox ID="txtcPwd" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Maroon" ControlToValidate="txtcPwd" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputbranchname">New Password</label>
               <asp:TextBox ID="txtnPwd" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <small>Password must be minimum 6 characters</small>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Maroon" ControlToValidate="txtnPwd" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegExp1" runat="server" ErrorMessage="**" ControlToValidate="txtnPwd" ValidationExpression="^([a-zA-Z0-9]).{6,}$" ForeColor="Maroon" />
            </div>
            <div class="form-group">
                <label for="inputbranchname">Confirm Password</label>
               <asp:TextBox ID="txtpwdConfirmed" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Maroon" ControlToValidate="txtpwdConfirmed" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                 <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password does not match" ForeColor="Maroon" Text="Password does not match" ControlToValidate="txtpwdConfirmed" ControlToCompare="txtnPwd"></asp:CompareValidator>
            </div>

             <asp:Button ID="btnSubmit" runat="server" Text="Change Password" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </div>     
</asp:Content>
