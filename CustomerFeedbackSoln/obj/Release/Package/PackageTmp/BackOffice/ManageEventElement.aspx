<%@ Page Title="" Language="C#" MasterPageFile="~/BackOffice/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="ManageEventElement.aspx.cs" Inherits="CustomerFeedbackSoln.BackOffice.ManageEventElement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="visible-xs">
                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </p>

            <h1 class="page-header">Manage Event Element Page
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
                <label for="inputbranchname">Title</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Maroon" ControlToValidate="txtTitle" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
             <div class="form-group">
                <label for="inputbranchname">Event Name:</label>
                <asp:DropDownList ID="ddlEvt" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEvt_SelectedIndexChanged" AutoPostBack="true">
                     <asp:ListItem Value="" Selected="True">..Select an Event..</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlEvt" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
           <div class="form-group">
                <label for="inputbranchname">Select a Metric:</label>
                <asp:DropDownList ID="ddlMet" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                     <asp:ListItem Value="" Selected="True">..Select..</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ForeColor="Maroon" ControlToValidate="ddlMet" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
           
             <div class="form-group">
                <label for="inputbranchname">Note:</label>
                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Maroon" ControlToValidate="txtNote" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
            </div>
            <div class="checkbox">
                <label>
                    <asp:RadioButtonList ID="rdIcon" runat="server"   AutoPostBack="true" OnSelectedIndexChanged="rdIcon_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select an Icon</asp:ListItem>
                        <asp:ListItem Value="1">Upload an Icon</asp:ListItem>
                        </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Maroon" ControlToValidate="rdIcon" Display="Dynamic" Text="Required!"></asp:RequiredFieldValidator>
                </label>
            </div>
           <div class="form-group" runat="server" id="dvUpload" visible="false">
                <label for="inputbranchname">Upload Icon:</label>
                 <asp:FileUpload ID="fileUploadDoc" runat="server" CssClass="form-control" />
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator22"  runat="server" ForeColor="Maroon" ControlToValidate="fileUploadDoc" Display="Dynamic" Text="Field is required"></asp:RequiredFieldValidator>  
                <br />
                   <small style="color:maroon">file must be in required format: *jpeg,*jpg,*png. Required file size 4MB max</small>
            </div>
           
           <asp:HiddenField ID="hddStore" runat="server" />
            <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            <%-- <button type="submit" class="btn btn-primary">Add</button>--%>
        </div>
          <div class="col-md-6 icon-display" runat="server" ID="dvIcon" visible="false">
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
      <div class="row page-body-control" style="width:100%">
        <div class="col-md-8" style="width:100%">
            <div class="panel panel-default" style="width:100%" >
                <div class="panel-heading">List of Elements
                     <div class="form-horizontal" style="float: right; margin-top: -5px; margin-right: 2px;" runat="server" visible="false" id="dvfilter">
                         <div class="form-group">
                             <asp:DropDownList ID="ddlOrgFiler" runat="server" CssClass="form-control-inline" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlOrgFiler_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                                 <asp:ListItem Value="0" Selected="True">..All Organizations..</asp:ListItem>
                             </asp:DropDownList>
                             <asp:DropDownList ID="ddlEvtfilter" runat="server" CssClass="form-control-inline" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEvtfilter_SelectedIndexChanged" AutoPostBack="true">
                                 <asp:ListItem Value="0" Selected="True">..Select an Event..</asp:ListItem>
                             </asp:DropDownList>
                             <asp:DropDownList ID="ddlMetricFilter" runat="server" CssClass="form-control-inline" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlMetricFilter_SelectedIndexChanged">
                                 <asp:ListItem Value="0" Selected="True">..Select a Metric..</asp:ListItem>
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
                             <asp:BoundField HeaderText="Title" DataField="Title" />
                            <asp:TemplateField HeaderText="Picture">
                                   <ItemTemplate>
                                     <asp:Image ID="Image1" runat="server" ImageUrl='<%#(Eval("Icon") != null)?Eval("Icon","~/Upload/{0}"):Eval("Icon","~/Upload/default.png") %>' Height="60px" Width="60px" />
                                   </ItemTemplate>
                                 </asp:TemplateField>
                            <asp:TemplateField HeaderText="Metric">
                                <ItemTemplate>
                                     <asp:Label ID="lbMetID" runat="server" Text='<%#Eval("EventMetricID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbMet" runat="server" Text='<%#Eval("EventMetric.Label") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Event Name">
                                <ItemTemplate>
                                     <asp:Label ID="lbEvtID" runat="server" Text='<%#Eval("EventID") %>' Visible="false"></asp:Label>
                                     <asp:Label ID="lbEvt" runat="server" Text='<%#Eval("Event.Title") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Remarks" DataField="Note" />
                           
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