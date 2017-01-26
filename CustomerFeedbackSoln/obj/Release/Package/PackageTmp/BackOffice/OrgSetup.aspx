<%@ Page Title="" Language="C#" MasterPageFile="SiteAdmin.Master" AutoEventWireup="true" CodeBehind="OrgSetup.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.OrgSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Manage Organisation Page
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
        <div class="col-md-8">
            <div class="row">
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="inputbranchname" class=" col-md-2 control-label">Organisation ID<span>*</span></label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtorgId" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Maroon" ControlToValidate="txtorgId" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputbranchname" class=" col-md-2 control-label">Organisation Name<span>*</span></label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtorgname" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Maroon" ControlToValidate="txtorgname" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputbranchname" class="col-md-2 control-label">Contact Email<span>*</span></label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Maroon" ControlToValidate="txtEmail" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegEmail" runat="server" ForeColor="Maroon" ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="cat"></asp:RegularExpressionValidator>
                    </div>
                </div>

                <div class="form-group">
                    <label for="inputbranchname" class="col-md-2 control-label">Contact MobileNo<span>*</span></label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Maroon" ControlToValidate="txtmobile" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                </div>
                 <div class="form-group">
                    <label for="inputbranchname" class="col-md-2 control-label">Contact PhoneNo</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtphoneno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Maroon" ControlToValidate="txtmobile" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                  <div class="form-group">
                    <label for="inputbranchname" class="col-md-2 control-label">Address<span>*</span></label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Maroon" ControlToValidate="txtaddress" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                </div>
                 <div class="form-group">
                    <label for="inputbranchname" class="col-md-2 control-label">City</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ForeColor="Maroon" ControlToValidate="txtCity" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputbranchname" class="col-md-2 control-label">Home Label</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtHomeLb" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CheckBox ID="chkDefault" runat="server" Text="Default" AutoPostBack="true" OnCheckedChanged="chkDefault_CheckedChanged" />
                         (<small>How Did we make you feel today ?</small>)
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Maroon" ControlToValidate="txtHomeLb" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail" class="col-md-2 control-label">Select State <span>*</span></label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <asp:ListItem Value="" Selected="True">..Select State..</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlState" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group" runat="server" id="dvUpload">
                <label for="inputbranchname" class="col-md-2 ">Upload Logo:</label>
                    <div class="col-md-6">
                 <asp:FileUpload ID="fileUploadDoc" runat="server" CssClass="form-control " />
                    <small style="color:maroon">file must be in required format: *jpeg,*jpg,*png. Required file size 4MB max</small>
                </div>
                 <div class="col-md-2"> <asp:RequiredFieldValidator ID="RequiredFieldValidator22"  runat="server" ForeColor=Maroon ControlToValidate="fileUploadDoc" Display="Dynamic" Text="Field is required"></asp:RequiredFieldValidator>  </div>
            </div>
                 <div class="form-group" runat="server" id="dvBackgd">
                <label for="inputbranchname" class="col-md-2 ">Upload Background:</label>
                    <div class="col-md-6">
                 <asp:FileUpload ID="fileUploadBg" runat="server" CssClass="form-control " />
                    <small style="color:maroon">file must be in required format: *jpeg,*jpg,*png. Required file size 4MB max</small>
                </div>
                 <div class="col-md-2"> <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ForeColor=Maroon ControlToValidate="fileUploadBg" Display="Dynamic" Text="Field is required"></asp:RequiredFieldValidator>  </div>
            </div>
                <div class="form-group">
                <div class="col-md-offset-2 col-md-6">
                    <div class="checkbox">
                 <label>
                    <asp:CheckBox ID="chk" runat="server" Text="Is Active" /></label>
                        </div>
                </div>
                    </div>
            <div class="form-group">
                <label for="inputbranchname" class="col-md-2 control-label"></label>
                <div class="col-md-6">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add Organisation" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                </div>
            </div>


            </div>
                </div>
        </div>
        <div class="col-md-4" runat="server" ID="dvEdit" visible="false">
            <asp:Image ID="Image1" runat="server"  Height="120px" Width="120px" ImageAlign="Middle" />
              <div class="form-group" >
                <label for="inputbranchname">Upload Logo:</label>
                 <asp:FileUpload ID="fileUpload1" runat="server" CssClass="form-control" />
                   
                <br />
                   <small style="color:maroon">file must be in required format: *jpeg,*jpg,*png. Required file size 4MB max</small>
            </div>
             <asp:Image ID="Image2" runat="server"  Height="120px" Width="120px" ImageAlign="Middle" />
              <div class="form-group" >
                <label for="inputbranchname">Upload Background:</label>
                 <asp:FileUpload ID="fileUpload2" runat="server" CssClass="form-control" />
                   
                <br />
                   <small style="color:maroon">file must be in required format: *jpeg,*jpg,*png. Required file size 4MB max</small>
            </div>
             <asp:Button ID="btnUpdate" runat="server" Text="Update Logo" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
        </div>
    </div>
    <div class="row page-body-control">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">List of Organisations <div class="form-horizontal" style="float:right;margin-top:-5px;margin-right:2px">
            <div class="form-group">
                <input type="text" runat="server" id="txtSea" placeholder="Organisation Name" class="" />
                <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="btn" CausesValidation="false" 
                    onclick="btnSearch_Click"/>  <asp:Button ID="btnClr" runat="server" CausesValidation="false"
                    Text="Clr" CssClass="btn" onclick="btnClr_Click" />

            </div>
         
              
          </div></div>
                <div class="panel-body"> <asp:HiddenField ID="hid" runat="server" /><asp:HiddenField ID="hidkey" runat="server" />
                    <asp:GridView ID="gvDefault" runat="server" GridLines="None"
                        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID"
                        AllowPaging="true" PageIndex="0" PageSize="15" EmptyDataText="No Record Found" OnSelectedIndexChanged="gvDefault_SelectedIndexChanged" OnPageIndexChanging="gvDefault_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="#" DataField="ID" Visible="false" />
                            <asp:BoundField HeaderText="OrgID" DataField="OrgID" />
                             <asp:BoundField HeaderText="Name" DataField="OrganisationName" />
                            <asp:BoundField HeaderText="ContactEmail" DataField="ContactEmail" />
                            <asp:BoundField HeaderText="ContactMobile" DataField="ContactMobileNo" />
                           <asp:BoundField HeaderText="City" DataField="City" />
                              <asp:TemplateField HeaderText="State">
                                <ItemTemplate>
                                     <asp:Label ID="lbRegionID" runat="server" Text='<%#Eval("StateID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbRegion" runat="server" Text='<%#Eval("State.Name") %>'></asp:Label>
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
