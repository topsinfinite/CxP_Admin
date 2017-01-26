<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="UserSetup.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.UserSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Setup User Page
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
        <div class="col-md-10">
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
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
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
                    <label for="inputbranchname" class="col-md-2 control-label">User ID<span>*</span></label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtuserID" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Maroon" ControlToValidate="txtuserID" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputbranchname" class="col-md-2 control-label">Password<span>*</span></label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtPwd" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <small>Password must be minimum 6 characters with</small>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Maroon" ControlToValidate="txtPwd" Display="Dynamic" Text="Required"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegExp1" runat="server" ErrorMessage="Password length must be at least 6 characters" ControlToValidate="txtPwd" ValidationExpression="^([a-zA-Z0-9]).{6,}$" ForeColor="Maroon" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputbranchname" class="col-md-2 control-label">Confirm Password<span>*</span></label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtpwdConfirmed" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Maroon" ControlToValidate="txtpwdConfirmed" Display="Dynamic" Text="Field is required"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password does not match" ForeColor="Maroon" Text="Password does not match" ControlToValidate="txtpwdConfirmed" ControlToCompare="txtPwd"></asp:CompareValidator>
                    </div>
            </div>
            <div class="form-group">
                <label for="inputbranchname" class="col-md-2 control-label"></label>
                <div class="col-md-6">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add User" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                </div>
            </div>


            </div>
        </div>
    </div>
    <div class="row page-body-control">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">List All Users
                     <div class="form-horizontal" style="float: right; margin-top: -5px; margin-right: 2px;" runat="server" visible="false" id="dvfilter">
                         <div class="form-group">
                             <asp:DropDownList ID="ddlorgfilter" runat="server" CssClass="form-control-inline" AppendDataBoundItems="true">
                                 <asp:ListItem Value="" Selected="True">..Select Organisation..</asp:ListItem>
                             </asp:DropDownList>
                              <input type="text" ID="txtsrchname" runat="server" class="form-control-inline" placeholder="search by fullname"/>
                             <asp:Button ID="btnSearch" runat="server" Text="Filter" CssClass="btn" CausesValidation="false"
                                 OnClick="btnSearch_Click" />
                             <asp:Button ID="btnClr" runat="server" CausesValidation="false"
                                 Text="Clr" CssClass="btn" OnClick="btnClr_Click" />

                         </div>

                     </div>
                </div>
                <div class="panel-body">
                    <asp:HiddenField ID="hid" runat="server" />
                    <asp:HiddenField ID="hidkey" runat="server" />
                    <asp:GridView ID="gvUsers" runat="server" CssClass="table table-striped"
                        DataKeyNames="ID" HorizontalAlign="Center"
                        GridLines="None" AutoGenerateColumns="false"
                        OnSelectedIndexChanged="gvUsers_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="UserName" HeaderText="UserName" />
                            <asp:BoundField DataField="FullName" HeaderText="FullName" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <%--<asp:BoundField DataField="isHOD" HeaderText="IsHOD" />--%>
                            <asp:BoundField DataField="RoleName" HeaderText="User Role" />
                            <asp:TemplateField HeaderText="BranchName">
                                <ItemTemplate>
                                    <asp:Label ID="lbbrchID" runat="server" Text='<%#Eval("BranchID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbbrch" runat="server" Text='<%#Eval("Branch.Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Organisation">
                                <ItemTemplate>
                                    
                                     <asp:Label ID="lbOrg" runat="server" Text='<%#Eval("Branch.Area.Region.Organisation.OrganisationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DateAdded" HeaderText="Date Created" DataFormatString="{0:dd-MM-yyyy hh:mm:ss tt}" />
                            <asp:TemplateField HeaderText="IsActive">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkHod" runat="server" Checked='<%#Eval("IsActive") %>' Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" HeaderText="View Detail" ShowSelectButton="true" SelectImageUrl="../images/view_icon.png" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
