<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CustomerFeedbackSoln._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:UpdatePanel runat="server" ID="updatePnlMain" ChildrenAsTriggers="true">
       <ContentTemplate>--%>
    
    <asp:ModalPopupExtender ID="mpeAlert" runat="server" PopupControlID="mpeAlertPopup" TargetControlID="popDv" BehaviorID="mpe1"
         BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="mpeAlertPopup" runat="server" CssClass="modalPopupAlert">
        <div class="touch-content" runat="server" id="Div1">
           Thank You For Your Feeback
        </div>
        <div class="touch-body">
              <div class="alert alert-success" runat="server" id="success" visible="false" style="padding:40px; font-size:2.8em">
              <a href="#" class="close" data-dismiss="alert">&times;</a>
             <strong>Success!</strong><br /> Your feedback has been sent successfully.
             </div>
        </div>
        <div class="footer" align="right">
          
        </div>
    </asp:Panel>
    <div class="touch-content">
        <div>Hello! How did we make you feel today?</div>
    </div>
    <div class="touch-body" >
        <img src="Images/loader.GIF" style="display:none" id="loader" class="loader"/>
        <asp:ListView ID="lstMain" runat="server" ItemPlaceholderID="itmContainer" OnItemCommand="lstMain_ItemCommand">
            <LayoutTemplate>
                <ul>
                    <asp:PlaceHolder ID="itmContainer" runat="server"></asp:PlaceHolder>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li class=" col-md-4">
                    <asp:ImageButton ID="lnkbtn" CssClass="center-block img-responsive img-circle" CommandArgument='<%#Eval("ID") %>' runat="server" Text='<%#Eval("Name") %>' ImageUrl='<%#Eval("SmileyImage","~/Upload/{0}") %>' />
                    <br />
                    <asp:Label ID="Literal1" CssClass="touch-text" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
            </ItemTemplate>
            <EmptyDataTemplate>
                <div>Sorry no data found!!!</div>
            </EmptyDataTemplate>
        </asp:ListView>
           
        <a href="javascript:;" runat="server" id="popDv" visible="true"></a>
    </div>
     

    <asp:ModalPopupExtender ID="mpehappy" BehaviorID="mpe" runat="server" PopupControlID="pnlPopupAppr" TargetControlID="popDv"
         BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAppr" runat="server" CssClass="modalPopup" style="display:none;background: url(Images/bg.jpg);background-size:100% 100%;position:static;" >
        <div class="touch-content" runat="server" id="dvcategoryCont">
            <asp:Image ID="smileyicon1" runat="server" />
            <%--<img src="Images/smiley-icon.gif" runat="server" id="smileyicon" />--%>Nice!
            What did we do to make you happy today?
        </div>
        <div class="touch-body">
             <img src="Images/loader.GIF" style="display:none" id="ImgCat" class="loader"/>
            <asp:ListView ID="lstCat" runat="server" ItemPlaceholderID="itmContainer" OnItemCommand="lstCat_ItemCommand">
                <LayoutTemplate>
                    <ul>
                        <asp:PlaceHolder ID="itmContainer" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li class=" col-md-6">
                        <asp:LinkButton ID="lnkbtn" CommandArgument='<%#Eval("ID") %>' runat="server" Text='<%#Eval("Name") %>' CssClass="catload"></asp:LinkButton></li>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div>Sorry no data found!!!</div>
                </EmptyDataTemplate>
            </asp:ListView>

        
        <div class="footer" align="right">
            <asp:Button ID="btnCloseAppr" OnClick="btn_happy_Click" runat="server" Text="Save & Exit" CausesValidation="false" CssClass="btn-modal btn-submit btn-block" />
        </div>
      </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="mpeService" runat="server" PopupControlID="pnlpopupSer" TargetControlID="popDv"
       BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopupSer" runat="server" CssClass="modalPopup-service" style="display:none;background: url(Images/bg.jpg);background-size:100% 100%;position:static;">
        <div class="touch-content-service touch-content" runat="server" id="dvserviceCont">
            <asp:Image ID="Image1" runat="server" />
            Oh, goodness!<br />
            What can we do better?
        </div>
        <div class="touch-body-service" >
            <img src="Images/loader.GIF" style="display:none" id="ImgSvc" class="loader-svr"/>
            <asp:ListView ID="lstService" runat="server" ItemPlaceholderID="itmContainer" OnItemCommand="lstServices_ItemCommand">
                <LayoutTemplate>
                    <ul class="row">
                        <asp:PlaceHolder ID="itmContainer" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>

                <ItemTemplate>
                    <li class="col-md-3">
                        <asp:LinkButton ID="lnkbtn" CommandArgument='<%#Eval("ID") %>' runat="server" Text='<%#Eval("Name") %>' CssClass="svrload"></asp:LinkButton></li>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div>Sorry no data found!!!</div>
                </EmptyDataTemplate>
            </asp:ListView>
            <div class="footer-service" align="right">
                <asp:Button ID="btnCloseIndif" OnClick="btn_Service_Click" runat="server" Text="Save & Exit" CausesValidation="false" CssClass="btn-modal btn-submit btn-block" />
            </div>
        </div>

    </asp:Panel>

    <asp:ModalPopupExtender ID="mpeStaff" runat="server" PopupControlID="pnlPopupStaff" TargetControlID="popDv"
         BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopupStaff" runat="server" CssClass="modalPopup" style="display:none;background: url(Images/bg.jpg);background-size:100% 100%;position:static;">
        <div class="touch-content" runat="server" id="dvStaffCont">
            <asp:Image ID="Image2" runat="server" />
            <%--<img src="Images/smiley-icon.gif" runat="server" id="smileyicon" />--%>Nice!<br />
            What did we do to make you happy today?
        </div>
        <div class="touch-body">
            <ul class="row">
                <li class="col-md-offset-1 col-md-5">
                    <asp:LinkButton ID="lnkstaff" runat="server" OnClick="lnkstaff_Click">Staff Name</asp:LinkButton></li>
                <li class="col-md-5">
                    <asp:LinkButton ID="lnkDept" runat="server" OnClick="lnkDept_Click">Staff Department</asp:LinkButton></li>
            </ul>
        </div>
        <br /> 
        <div class="footer" align="right">
           <asp:Button ID="btnCloseStff" OnClick="btn_happy_Click"  runat="server" Text="Save & Exit" CausesValidation="false" CssClass="btn-modal btn-submit btn-block" />
        </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="mpeform" runat="server" PopupControlID="pnlPopupForm" TargetControlID="popDv"
         BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopupForm" runat="server" CssClass="modalPopup" style="display:none;background: url(Images/bg.jpg);background-size:100% 100%;position:static;">
        <div class="touch-content" runat="server" id="dvComment">
            What did we do to make you happy today?
        </div>
        <div class="touch-body" style="padding-top:20px;">
            <div class="row">
                <div class="col-md-offset-1 col-md-4">
                    <asp:Label ID="lbError" runat="server" Text="" ForeColor="Maroon"></asp:Label>
                   <%-- <div class="form-group">
                        <%--<label for="inputEmail">Account Number: </label>
                          <input type="text" id="txtAcct" runat="server" class="form-control form-control-height" placeholder="Account Number"/>
                        <small>Only if you are an existing customer</small>
                    </div>--%>
                    <div class="form-group">
                        <label for="inputbranchname">Customer Name:<span style="color:maroon">*</span></label>
                        <input type="text" id="txtNameId" runat="server" class="form-control form-control-height" placeholder="Customer Name" />
                    </div>
                   <%--<div class="form-group">
                       <%-- <label for="inputbranchname">Gender:</label>
                       <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control form-control-height"  >
                           <asp:ListItem Selected="true" Value="0">...select gender...</asp:ListItem>
                           <asp:ListItem>Male</asp:ListItem>
                           <asp:ListItem    >Female</asp:ListItem>
                       </asp:DropdownList>
                    </div>--%>
                    <div class="form-group">
                        <%--<label for="inputbranchname">Phone number:</label>--%>
                        <input type="text" id="txtphoneId" runat="server" Class="form-control form-control-height" placeholder="Phone Number"/>
                    </div>
                     <div class="form-group">
                       <%-- <label for="inputbranchname">Email Address:</label>--%>
                        <input type="text" id="emailId" runat="server" Class="form-control form-control-height" placeholder="Email Address"/>
                    </div>
                     <div class="form-group">
                        <%--<label for="inputbranchname">Your Comment:</label>--%>
                         <textarea id="txtCommentArea" rows="3"  Class="form-control" style="font-size:1.8em"  runat="server"  placeholder="Your Comment"/>
                        <%--<TextArea type="text" id="txtCommentId" runat="server" TextMode="MultiLine" Rows="4" Class="form-control" placeholder="Your Comment"/>--%>
                    </div>
                    
                </div>
               <div class="col-md-4">
                     <asp:Button ID="btnCloseForm" OnClick="btn_Comment_Click"  runat="server" Text="Provide Comment & Submit " CausesValidation="false" CssClass="btn-modal btn-submit btn-block" /><br />
                     <asp:Button ID="btnExit" OnClick="btn_CommentExit_Click"  runat="server" Text="Not Interested! Just Submit" CausesValidation="false" CssClass="btn-modal btn-submit btn-block" />
               </div>
            </div>
           <div class="footer row">
             <div class="col-md-4">
           
                </div>
            <div class="col-md-4">
           
           </div>
        </div>
        </div>
       
    </asp:Panel>

    <asp:HiddenField ID="hidHappy" runat="server" />
    <asp:HiddenField ID="hidSad" runat="server" />
     <asp:HiddenField ID="hidIndif" runat="server" />
    <asp:HiddenField ID="hidstaff" runat="server" />
     <asp:HiddenField ID="hidcounter" runat="server" />
    <asp:HiddenField ID="hidMain" runat="server" />
   <%-- <img id="imgSessionCheck" width="1" height="1" />--%>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
     <script type="text/javascript">
          
         $(document).ready(function () {
             window.setTimeout(function () {
                 $("#ContentPlaceHolder1_mpeAlertPopup").fadeTo(1500, 0).slideUp(500, function () {
                     $(this).remove();
                     $find("mpe1").hide();
                     return false;
                     //location.reload(false);
                 });
             }, 2000);

         });
         //Bounce Jquery
         $(document).ready(function () {
             setInterval(function () {
                 $(".center-block").effect("bounce",
                     { times: 3 }, 4000);
             }, 2000);

             //$(".center-block").click(function () {
             //    $('img#loader').fadeIn("slow");
             //});
             //$(".catload").click(function () {
             //    $('img#ImgCat').fadeIn("slow");
             //});
             //$(".svrload").click(function () {
             //    $('img#ImgSvr').fadeIn("slow");
             //});
         });
         ///////////////////////////////////////keep session alive
         var counter;
         counter = 0;

          
         // Call this function for a first time
       // KeepSession();
         function KeepSession() {
                 // A request to server
                 //$.post("KeepSessionAlive.aspx");

                 //now schedule this process to happen in some time interval, in this example its 1 min
                 //setInterval(KeepSession, 60000 * 10 - 10);

                 setTimeout(KeepSession, 60000*20);
                //}

              //First time call of function
             // KeepSession();
         }

         function ReloadPage() {
             window.location.reload(true);
         }
    </script>
    <script language="javascript" type="text/javascript">

        
 </script>
</asp:Content>

