<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ManageEvent.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.ManageEvent" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>--%>
     <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Manage Event Page
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
                <label for="inputEmail">Event Code: <span>*</span></label><small> [Enter unique 7 alphanumeric code e.g (TESTCODE)] </small>
                <asp:TextBox ID="txtEvtCode" runat="server" CssClass="form-control" ValidationGroup="evt"></asp:TextBox> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Maroon" ValidationGroup="evt" ControlToValidate="txtEvtCode" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Maroon" ValidationGroup="evt" ErrorMessage="Code does not match required specification" Text="Code does not match required specification" ControlToValidate="txtEvtCode" ValidationExpression="^[a-zA-Z0-9]{7,}$"></asp:RegularExpressionValidator>
               <asp:Button ID="btnCheck" runat="server" CausesValidation="true" ValidationGroup="evt" Text="Validate Event Code" OnClick="btnCheck_Click" CssClass="btn btn-primary form-control"/>
            </div>
           <div class="form-group">
                <label for="inputEmail">Event Title: <span>*</span></label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Maroon" ControlToValidate="txtTitle" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputbranchname">Question:<span>*</span></label><small> [Smiley homepage question e.g (How was your overall experience at xyz event?)] </small>
                <asp:TextBox ID="txtQuestion" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Maroon" ControlToValidate="txtTitle" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="inputEmail">Note/Description:</label>
                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ForeColor="Maroon" ControlToValidate="txtcode" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>--%>
            </div>
             <div class="form-group">
                <label for="inputbranchname">Expiry Date: <span>*</span></label> <small> [kindly select the expiry date . After the expiry date users will not be able to submit feedback for this event] </small>
                <asp:TextBox ID="txtValidDate" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Maroon" ControlToValidate="txtValidDate" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/cal.gif" />
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="txtValidDate" PopupButtonID="Image2"></asp:CalendarExtender> 
             </div>
            <div class="checkbox">
                <label>
                    <asp:CheckBox ID="chk" runat="server" Text="Is Active" /></label>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Add Event" CssClass="btn btn-primary" Enabled="false" OnClick="btnSubmit_Click"/>
            <%-- <button type="submit" class="btn btn-primary">Add</button>--%>
        </div>
    </div>
   <%--  </ContentTemplate>
      </asp:UpdatePanel>--%>
 <div class="row page-body-control">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">List of Events
                    <div class="form-horizontal" style="float: right; margin-top: -5px; margin-right: 2px;" runat="server" visible="false" id="dvfilter">
                         <div class="form-group">
                             <asp:DropDownList ID="ddlEventfilter" runat="server" CssClass="form-control-inline" AppendDataBoundItems="true">
                                 <asp:ListItem Value="" Selected="True">..Select Events..</asp:ListItem>
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
                            <asp:BoundField HeaderText="#" DataField="ID" Visible="false"  />
                            <asp:BoundField HeaderText="Event Code" DataField="Code" />
                            <asp:BoundField HeaderText="Title" DataField="Title" />
                             <asp:BoundField HeaderText="Question" DataField="Question" />
                              <asp:TemplateField HeaderText="Organisation">
                                <ItemTemplate>
                                     <asp:Label ID="lbOrganisationID" runat="server" Text='<%#Eval("OrganisationID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="Organisation" runat="server" Text='<%#Eval("Organisation.OrganisationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="Note" DataField="Note" Visible="false" />
                             <asp:BoundField HeaderText="Expiry Date " DataField="ValidTill" DataFormatString="{0:dd/MMM/yyyy}" />
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