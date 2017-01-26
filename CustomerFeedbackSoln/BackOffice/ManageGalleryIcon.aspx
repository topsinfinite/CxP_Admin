<%@ Page Title="" Language="C#" MasterPageFile="SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ManageGalleryIcon.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.ManageGalleryIcon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Manage Gallery Icons Page
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
                <label for="inputbranchname">Icon Type:</label>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                     <asp:ListItem Value="" Selected="True">..Select Icon Type..</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlType" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            
            <div class="form-group" runat="server" id="dvUpload">
                <label for="inputbranchname">Upload Icon:</label>
                 <asp:FileUpload ID="fileUploadDoc" runat="server" CssClass="form-control" />
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator22"  runat="server" ForeColor=Maroon ControlToValidate="fileUploadDoc" Display="Dynamic" Text="Field is required"></asp:RequiredFieldValidator>  
                <br />
                   <small style="color:maroon">file must be in required format: *jpeg,*jpg,*png. Required file size 4MB max</small>
            </div>
           
            <div class="checkbox">
                <label>
                    <asp:CheckBox ID="chk" runat="server" Text="Is Active" /></label>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            <%-- <button type="submit" class="btn btn-primary">Add</button>--%>
        </div>
          <div class="col-md-4" runat="server" ID="dvEdit" visible="false">
            <asp:Image ID="Image1" runat="server"  Height="120px" Width="120px" ImageAlign="Middle" />
              <div class="form-group" >
                <label for="inputbranchname">Upload Icon:</label>
                 <asp:FileUpload ID="fileUpload1" runat="server" CssClass="form-control" />
                   
                <br />
                   <small style="color:maroon">file must be in required format: *jpeg,*jpg,*png. Required file size 4MB max</small>
            </div>
             <asp:Button ID="btnUpdate" runat="server" Text="Update Icon" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
        </div>
    </div>
      <div class="row page-body-control">
        <div class="col-md-8">
            <div class="panel panel-default">
                <div class="panel-heading">List of Icons
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
                <div class="panel-body" style="background-color:#b8cbd3"> <asp:HiddenField ID="hid" runat="server" /><asp:HiddenField ID="hidkey" runat="server" />
                    <asp:GridView ID="gvDefault" runat="server" GridLines="None"
                        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID"
                        AllowPaging="true" PageIndex="0" PageSize="15" EmptyDataText="No Record Found" OnSelectedIndexChanged="gvDefault_SelectedIndexChanged" OnPageIndexChanging="gvDefault_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="#" DataField="ID" />
                           <%-- <asp:BoundField HeaderText="Branch Code" DataField="Code" />--%>
                             <asp:BoundField HeaderText="Icon Name" DataField="Name" />
                             <asp:TemplateField HeaderText="Icon">
                                   <ItemTemplate>
                                     <asp:Image ID="Image1" runat="server" ImageUrl='<%#(Eval("Name") != null)?Eval("Name","~/Upload/{0}"):Eval("Name","~/Upload/default.png") %>' Height="60px" Width="60px" />
                                   </ItemTemplate>
                                 </asp:TemplateField>
                             <asp:TemplateField HeaderText="Category Type">
                                <ItemTemplate>
                                     <asp:Label ID="lbtype" runat="server" Text=' <%#GetType(Eval("Type"))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                     <asp:Label ID="lbEnable" runat="server" Text=' <%# (Eval("IsActive") != null&&Boolean.Parse(Eval("IsActive").ToString())) ? "Active" : "Inactive"%>' ForeColor="Maroon"></asp:Label>
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
