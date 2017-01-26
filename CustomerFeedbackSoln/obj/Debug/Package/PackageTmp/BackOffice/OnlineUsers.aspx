<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="OnlineUsers.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.OnlineUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Check Users/Terminals Status Page
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
            <%--  <div class="alert alert-info">Total Users Online at the moment:<asp:Label ID="lbusrTot" runat="server" Text="" CssClass="badge"></asp:Label>

                  <br />
                  Registered users now online: <asp:Label ID="OnlineNow" runat="server"></asp:Label>
              </div>--%>
             <div class="panel panel-default">
                <div class="panel-heading">List of Users/Devices</div>
                <div class="panel-body"> <asp:HiddenField ID="hid" runat="server" /><asp:HiddenField ID="hidkey" runat="server" />
                    <asp:GridView ID="gvDefault" runat="server" GridLines="None"
                        AutoGenerateColumns="false" CssClass="table table-striped" DataKeyNames="ID"
                        AllowPaging="true" PageIndex="0" PageSize="100" EmptyDataText="No Record Found"  OnPageIndexChanging="gvDefault_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="#" DataField="ID" Visible="false" />
                           <%-- <asp:BoundField HeaderText="Branch Code" DataField="Code" />--%>
                             <asp:BoundField HeaderText="UserID/DeviceID" DataField="UserName" />
                             <asp:BoundField HeaderText="Full Name" DataField="FullName" />
                             <asp:BoundField HeaderText="Last Active Time" DataField="LastActive" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# (Eval("isOnline").Equals(true) ? "~/Images/green.png" : "~/Images/red_light.png")%>' />
                                     <asp:Label ID="lbEnable" runat="server" Text=' <%# (Eval("isOnline") != null&&Boolean.Parse(Eval("isOnline").ToString())) ? "Online" : "Offline"%>' ForeColor="Maroon"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:CommandField ButtonType="Image" HeaderText="Edit" ShowSelectButton="true" SelectImageUrl="../images/edit_icon.png" />--%>
                        </Columns>
                        <SelectedRowStyle BackColor="#DBB549" />
                        <PagerStyle CssClass="pagination" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
