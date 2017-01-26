<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ManageEventDevice.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.ManageEventDevice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>--%>
     <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Device Setup Page
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
                <label for="inputEmail"  class="col-md-2 control-label">Select Organisation <span>*</span></label>
                     <div class="col-md-6">
                <asp:DropDownList ID="ddlOrg" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged" >
                     <asp:ListItem Value="" Selected="True">..Select Organisation..</asp:ListItem>
                </asp:DropDownList>
                  </div>
                     <div class="col-md-2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlOrg" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                 </div>
              </div>
                <div class="form-group">
                <label for="inputbranchname" class="col-md-2 control-label">Select Device <span>*</span></label>
                    <div class="col-md-6">
                <asp:DropDownList ID="ddlDev" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDev_SelectedIndexChanged">
                     <asp:ListItem Value="" Selected="True">..Select a device..</asp:ListItem>
                </asp:DropDownList>
                        </div>
                    <div class="col-md-2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlDev" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                 </div>
                    </div>
                <div class="form-group">
                <label for="inputEmail" class="col-md-2 control-label">Select Event <span>*</span></label>
                    <div class="col-md-6">
                <asp:DropDownList ID="ddlEvt" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEvt_SelectedIndexChanged">
                     <asp:ListItem Value="" Selected="True">..Select Event..</asp:ListItem>
                </asp:DropDownList>
                        </div>
                    <div class="col-md-2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlEvt" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                 </div>
                         </div>
               
                <div class="form-group">
                    <div class="col-md-2"></div>
               <div class=" col-md-6 ">
                <label>
                    <asp:CheckBox ID="chk" runat="server" Text=" IsActive" CssClass="checkbox" /></label>
              </div>
              </div>
            <div class="form-group">
               
                <div class="col-md-6">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add Device" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                </div>
            </div>


            </div>
        </div>

      <div class="row page-body-control">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">List of Devices
                    <div class="form-horizontal" style="float: right; margin-top: -5px; margin-right: 2px;" runat="server" visible="false" id="dvfilter">
                         <div class="form-group">
                             <asp:DropDownList ID="ddlorgfilter" runat="server" CssClass="form-control-inline" AppendDataBoundItems="true">
                                 <asp:ListItem Value="" Selected="True">..Select Event..</asp:ListItem>
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
                              <asp:TemplateField HeaderText="Device ID">
                                <ItemTemplate>
                                     <asp:Label ID="lbDeviceID" runat="server" Text='<%#Eval("DeviceID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbDeviceNo" runat="server" Text='<%#Eval("DeviceTbl.DeviceID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Event Name.">
                                <ItemTemplate>
                                     <asp:Label ID="lbEventID" runat="server" Text='<%#Eval("EventID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbUname" runat="server" Text='<%#Eval("Event.Title") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Organisation">
                                <ItemTemplate>
                                     <asp:Label ID="lbOrgID" runat="server" Text='<%#Eval("DeviceTbl.BranchID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbOrg" runat="server" Text='<%#Eval("DeviceTbl.Branch.Area.Region.Organisation.OrganisationName") %>'></asp:Label>
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
    </div>
          <%-- </ContentTemplate>
       </asp:UpdatePanel>--%>
</asp:Content>
