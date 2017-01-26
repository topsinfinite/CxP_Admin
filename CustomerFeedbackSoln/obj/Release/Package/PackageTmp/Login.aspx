<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CustomerFeedbackSoln.Login" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
<title>Customer Xperience Platform</title>
<link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="css/styleNew.css" rel="stylesheet" type="text/css" />
<script src="js/jquery.min.js" type="text/javascript"></script>
<script src="js/action.js" type="text/javascript"></script>
        
    <!--[if lt IE 9]>
    <script src="//cdnjs.cloudflare.com/ajax/libs/html5shiv/r29/html5.min.js"></script>
    <![endif]-->
</head>
    <body>
    
   <div class="bg-image"></div>
    <section id="landing-page">
        <div class="container content">
            <div class="row">
                <div class="col-md-2"></div>
                <form id="form1" runat="server" class="col-md-8 col-xs-12 login">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class="row">
                        <div class="col-xs-12 logo">
                            <img src="images/logo.png" />
                        </div>
                        <div class="col-sm-12 col-xs-12">
                          <div class="alert alert-danger" runat="server" id="error" visible="false">
                            <a href="#" class="close" data-dismiss="alert">&times;</a>
                            <strong>Error!</strong> A problem has been occurred while submitting your data.
                        </div>
                          <div class="alert alert-success" runat="server" id="success" visible="false">
                            <a href="#" class="close" data-dismiss="alert">&times;</a>
                            <strong>Success!</strong> Your message has been sent successfully.
                        </div>
                        </div>
                        <div class="col-sm-6 col-xs-12">
                            <input type="text" name="email" id="username" runat="server" placeholder="Username(*)">
                            <asp:RequiredFieldValidator ID="Reqlg" runat="server" Font-Size="9px" ErrorMessage="required" ForeColor="Maroon" Display="Dynamic" ControlToValidate="username"></asp:RequiredFieldValidator>
                                
                        </div>
                        <div class="col-sm-6 col-xs-12">
                             <input type="password" name="key" id="txtpwd" runat="server"  placeholder="Password(*)">
                            <asp:RequiredFieldValidator ID="Reqpwd1" runat="server" Font-Size="9px" ForeColor="Maroon" ErrorMessage="required" Display="Dynamic" ControlToValidate="txtpwd"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3 hidden-xs"></div>
                        <div class="col-sm-6 col-xs-12 text-center">
                             <asp:Button ID="btnLogin" runat="server" CssClass="submit" Text="Log in" OnClick="btnLogin_Click" />
                           <%-- <input type="submit" class="submit" />--%>
                            <span class="forgot"> <a href="javascript:;" runat="server" id="popDv">Forgot your password?</a></span>
                        </div>
                    </div>
                    <asp:ModalPopupExtender ID="mpeAppr" runat="server" PopupControlID="pnlPopupAppr" TargetControlID="popDv"
        CancelControlID="btnCloseAppr" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAppr" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            Customer Xperience Platform:: Recovery password
        </div>
        <div class="body">
            <div role="form">
                <div class=" alert alert-danger" runat="server" id="modalErr" visible="false">
                    <a href="#" class="close" data-dismiss="alert">&times;</a>
                </div>

                <div class="form-group">
                    <label for="email">Type your profiled email address:<span class="require">*</span></label>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Font-Size="9px" ErrorMessage="Required" ForeColor="Maroon" Display="Dynamic" ControlToValidate="txtemail"></asp:RequiredFieldValidator>--%>
                    <input type="text" name="email" id="txtemail" runat="server" class="form-control" placeholder="somebody@example.com">
                    <small>A new password will be sent to the email address.</small>
                </div>

            </div>
        </div>
        <div class="footer" align="right">
            <div class="control-group">
                <label class="control-label" for="btnSubmit">
                </label>
                <div class="controls">
                    <asp:Button ID="btnAppr" runat="server" Text="Recover Password" CausesValidation="false" CssClass="submit"
                        OnClick="btnAppr_Click" />
                    <asp:Button ID="btnCloseAppr" runat="server" Text="Close" CausesValidation="false" CssClass="btn  btn-submit btn-block" />
                </div>
            </div>
        </div>
    </asp:Panel>
                </form>
                <div class="col-md-3 col-sm-2 hidden-xs"></div>
            </div>
        </div>
    </section>
    

    

    <%-- <div class="modal fade forget-modal" tabindex="-1" role="dialog" aria-labelledby="myForgetModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">×</span>
                        <span class="sr-only">Close</span>
                    </button>
                    <h4 class="modal-title">Recovery password</h4>
                </div>
                <div class="modal-body">
                    <p>Type your email account</p>
                    <input type="email" name="recovery-email" id="recovery-email" class="form-control" autocomplete="off">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <button type="button" class="btn btn-custom">Recovery</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>--%>
        <script src="js/scripts.js"></script>
        <script src="js/bootstrap.min.js"></script>
</body>
    </html>
