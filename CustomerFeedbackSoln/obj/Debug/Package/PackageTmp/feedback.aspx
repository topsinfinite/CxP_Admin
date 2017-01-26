<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="feedback.aspx.cs" Inherits="CustomerFeedbackSoln.feedback" %>

<html lang="en">
<head id="Head1">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
    <meta charset="utf-8" />
    <title>Customer eXperience Platform</title>
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>
		<link href="css/bootstrap.min.css" rel="stylesheet"/>
        <link href="css/styles.css" rel="stylesheet"/>
    
       <%-- <link href="css/ModelPopup.css" rel="stylesheet" />--%>
        
 <%--   <link href="css/jmobile.css" rel="stylesheet" />--%>
   
    <!--[if lt IE 9]>
    <script src="//cdnjs.cloudflare.com/ajax/libs/html5shiv/r29/html5.min.js"></script>
    <![endif]-->
    <script src="js/app.js"></script>
</head>
<body>
   
  <form id="Form1">
    
   <%-- <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
      <div class="container-fluid">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
        
        </div>
           <div class="navbar-brand"> <a href="#"><img id="Img1" src="~/Images/logo_md.png" runat="server" align="left" class="applogo"  />  </a><span>Customer eXperience Platform</span></div>
        <div class="navbar-collapse collapse">
          <ul class="nav navbar-nav navbar-right">
          </ul>
        </div>
      </div>
</nav>--%>
    <div class="container-fluid contain-admin-feedback">
     
      <div class="row row-offcanvas">
        <div class="col-sm-12 col-md-12 main content">
         
   <div id="content" class="throbber"></div>
<div id="mainCat">
        <div class="touch-content">
            <div id="dvHomeLb"> ?</div>
       </div>
    <div class="touch-body">
        <ul id="mainList">
            
        </ul>
    </div>
</div>

 <div id="subMainCat">
        <div id="dvCatHeader" class="touch-content">
           <img id="imgCat" src="" />
            Nice!
            What did we do to make you happy today?
        </div>
        <div class="touch-body">
            <ul class="row" id="catList">

            </ul>
        </div>
        <div align="center">
           <input type="submit" name="ctl00$ContentPlaceHolder1$btnCloseStff" value="Submit & Return to Smiley Home" id="ContentPlaceHolder1_btnCloseStff" class="submit" />
        </div>
    
</div>
<div id="serviceCat" class="modalPopup-service">
    <div class="touch-content-service touch-content">
        <div id="dvsvrheader">Which of our Service blew you away?</div>
     </div>
    <div class="touch-body-service">
        <ul class="row" id="serviceList">
          
        </ul>
    </div>
    <br />
    <div align="center">
           <input type="submit" name="ctl00$ContentPlaceHolder1$btnCloseStff" value="Submit & Return to Smiley Home" id="btnservice" class="submit" />
     </div>
</div>

<div id="dvcomment">
      <div class="touch-content">
           Mind Filling Our Feedback Form?
        </div>
    <div class="touch-body" style="padding-top:40px;">
        <div class="row">
             
            <div class="col-md-offset-1 col-md-8">
                <span id="errmsg" class="alert alert-danger" ></span>
            </div>
          <div class="col-md-offset-1 col-md-4">
           <%-- <div class="form-group">
                <input type="text" name="txtAcct" class="form-control" placeholder="Account Number" />
                <small>Only if you are an existing customer</small>
            </div>
            <div class="form-group">
                <input type="text" id="txtNameId" class="form-control" placeholder="Customer Name" />
            </div>--%>
           <%-- <div class="form-group">

                <select id="ddlGender" class="form-control form-control-height">
                    <option selected="selected">...select gender...</option>
                    <option>Male</option>
                    <option>Female</option>
                </select>
            </div>--%>
              <div class="form-group">
                <input type="text" id="txtname" class="form-control form-control-height" placeholder="Your Name" />
            </div>
              <div class="form-group">
                <input type="email" id="emailId" class="form-control form-control-height" placeholder="Email Address(*)" /> 
            </div>
            <div class="form-group">
                <input type="tel" id="txtphoneId"  class="form-control form-control-height" placeholder="Phone Number" />
            </div>
            
            <div class="form-group">
                <textarea rows="4" cols="70"  placeholder="Enter comment here(*)" class="form-control"  style="font-size:1.8em"></textarea>
                
            </div>
       
    </div>
            <div class="col-md-6">
                <div class="form-group">
               <input id="btnSub" type="button" value="Not Interested! Just Submit" class="submit" />
                <input id="btnsaveForm" type="button" value="Submit Comment" class="submit" />
             
            </div>
               
            </div>
         </div>
    </div>
</div><hr />
 
     <div ID="mpeAlertPopup" class="modalPopupAlert" style="display:none;  position:absolute; left:25%;top:20%;">
         <div class="touch-content-alert">
           Thank You For Your Feeback
        </div>
        <div class="touch-body-alert">
           
           <div class="alert alert-success" id="success" style="padding:40px; font-size:2.5em">
             <%-- <a href="#" class="close" data-dismiss="alert">&times;</a>--%>
             <strong>Success!</strong><br /> Your feedback has been sent successfully.
        </div>
        </div>
    </div>
   <%-- <script src="js/app.js"></script>--%>
    
       <footer>
       <p class="pull-right"><img src="" class="img-responsive" id="imgLogo" /></p>
     </footer>
            
      </div><!--/row-->
	</div>
</div><!--/.container-->

      
    </form>
    <!-- script references -->
		<%--<script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>--%>
    <%-- <script src="Scripts/jquery-1.7.1.min.js"></script>--%>
		
		<%--<script src="/js/scripts.js"></script>--%>
      <script src="/js/jquery-1.10.2.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
      
      <script type="text/javascript">
          var smileyType = { "Good": 1, "Indiff": 2, "Bad": 3 };
          var serviceCat = { "OurStaff": 1, "OurService": 2 };
          var actiotype = { "MainList": 1, "CatList": 2, "ServiceList": 3 }
          var defaultCat = 0;
          var homeLabel = ""; var CatLabelAwe = ""; var CatLabelBad = ""; var SvcLabelAwe = "";
          var SvcLabelBad = ""; var SvcLabelInd = ""; var StfLabelAwe = ""; var StfLabelBad = "";

          function LoadOrgSetting(apiurl) {
              $.getJSON(apiurl, function (result) {
                  $('body').css({
                      "width": "100%", "height": "100%","position":"static", "background-image": "url(Upload/"+result.Background+")",
                      "background-position":"center bottom","background-repeat":"repeat-x",
                      "-webkit-background-size":"cover",
                  "background-size": "cover"
                  });

                  $('#dvHomeLb').text(result.HomeLabel);
                  $('#imgLogo').attr("src", "../Upload/"+result.Logo);
              });
          }
          function LoadLabelSetting(apiurl) {
              $.getJSON(apiurl, function (result) {
                   
                   homeLabel=result.HomeLabel;
                   CatLabelAwe = result.CategoryLabelAwesome;
                   CatLabelBad = result.CategoryLabelBad;
                   SvcLabelAwe = result.ServiceLabelAwesome;
                   SvcLabelBad = result.ServiceLabelBad;
                   SvcLabelInd = result.ServiceLabelIndifferent;
                   StfLabelAwe = result.StaffLabelAwesome;
                   StfLabelBad = result.StaffLabelBad;
              });
          }
          function LoadServices(apiurl, type) {
              $.getJSON(apiurl, function (result) {
                  //listid.empty();//clear the list 1st
                  //store result in localstorage based on listtype returned
                  //localStorage.clear();
                  if (type == actiotype.MainList) {
                      localStorage.mainResult = JSON.stringify(result);
                  }
                  if (type == actiotype.CatList) {
                      localStorage.catResult = JSON.stringify(result);
                  }
                  if (type == actiotype.ServiceList) {
                      localStorage.svrResult = JSON.stringify(result);
                  }
              });
          }
          function SetDisplay(dataJson, type, listid, smileytype, categorytype)
          {
              $.each(dataJson, function (i, data) {
                  if (type == actiotype.MainList) {
                      refval = data.SmileyType, hrefval = "#awe";
                      $("<li><input/><span/></li>") // li 
                     .find("input") // input
                     .attr({ type: "image", Id: "#Awe", name: "image" + data.ID, data_val:data.ID, src: "Upload/" + data.SmileyImage, alt: data.SmileyType }).addClass("center-block img-responsive img-circle") // a 
                     .end().addClass("col-md-4") // li 
                     .find("span").html("<br/> " + data.Name).addClass("touch-text text-center")
                     .end()
                     .appendTo(listid);
                  }
                  if (type == actiotype.CatList) {
                      refval = data.CategoryType, hrefval = "#cat";
                      $("<li><a></a></li>") // li 
                     .find("a") // a 
                     .attr({ data_href: hrefval, Id: refval, data_val: data.ID, data_type: smileytype }) // a 
                     .html("<img src=Upload/" + data.CategoryIcon + ">" + data.Name) // a 
                     .end().addClass("col-md-offset-1 col-md-5") // li 
                     .appendTo(listid);
                  }
                  if (type == actiotype.ServiceList) {
                      refval = data.ID, hrefval = "#last"
                      $("<li><a></a></li>") // li 
                        .find("a") // a 
                        .attr({ data_href: hrefval, Id: refval, data_val: data.ID, data_type: smileytype }) // a 
                        .html("<img src=Upload/" + data.ServiceIcon + ">" + data.Name) // a 
                        .end().addClass("col-md-3") // li

                        .appendTo(listid);
                  }
              });
          }
          var populateList = function (apiurl, type, listid, smileytype, categorytype) {
              var refval = ""; var hrefval = "";
              //for Online Mode
              $.getJSON(apiurl, function (result) {
                  listid.empty();//clear the list 1st
                  //store result in localstorage based on listtype returned
                  //localStorage.clear();
                  if (type == actiotype.MainList) {
                      localStorage.mainResult = JSON.stringify(result);
                  }
                  if (type == actiotype.CatList) {
                      localStorage.catResult = JSON.stringify(result);
                  }
                  if (type == actiotype.ServiceList) {
                      localStorage.svrResult = JSON.stringify(result);
                  }
                  if (categorytype == serviceCat.OurService || categorytype == serviceCat.OurStaff) {
                      result = $.grep(result, function (n, i) { return n.ServiceCatId == categorytype });
                  }
                  SetDisplay(result, type, listid, smileytype, categorytype);
              })
              .fail(function (jqxhr, textStatus, error) {
                  if (jqxhr.statusCode == 404) {
                      alert('An error occurred!!! Device has NOT been configured. Kindly contact vendor');
                  }
                  var serResult = null;
                  var SerResultAll = null; var catResult = null;
                  if (type == actiotype.ServiceList && categorytype != 0)//check if it service List and not from Indifferece
                  {
                      serResult = JSON.parse(localStorage.svrResult);
                  }
                  if (type == actiotype.CatList)
                  {
                      catResult = JSON.parse(localStorage.catResult)
                      listid.empty();
                      SetDisplay(catResult, type, listid, smileytype, categorytype);
                  }
                  if (categorytype == serviceCat.OurService || categorytype == serviceCat.OurStaff) {
                      serResult = $.grep(serResult, function (n, i) { return n.ServiceCatId == categorytype });
                      listid.empty();
                      SetDisplay(serResult, type, listid, smileytype, categorytype);
                  }
                  if(smileytype=="2")//run this for indifference or Not That Great
                  {
                      SerResultAll = JSON.parse(localStorage.svrResult);
                      listid.empty();
                      SetDisplay(SerResultAll, type, listid, smileytype, categorytype);
                  }
              });
          }

          //function to get current date
          var currentDate = function () {
              fullDate = new Date();
              console.log(fullDate);
              //Thu May 19 2011 17:25:38 GMT+1000 {}

              //convert month to 2 digits
              var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) :  (fullDate.getMonth() + 1);

              var currentDate = twoDigitMonth + "/" + fullDate.getDate() + "/" + fullDate.getFullYear() + " " + fullDate.getHours() + ":" + fullDate.getMinutes() + ":" + fullDate.getSeconds();

              console.log(currentDate);
              return currentDate;
          }
          //function to retrieve querystring value
          function getQueryStrings() {
              //Holds key:value pairs
              var queryStringColl = null;
              //Get querystring from url
              var requestUrl = window.location.search.toString();

              if (requestUrl != '') {
                  //window.location.search returns the part of the URL 
                  //that follows the ? symbol, including the ? symbol
                  requestUrl = requestUrl.substring(1);

                  queryStringColl = new Array();

                  //Get key:value pairs from querystring
                  var kvPairs = requestUrl.split('&');

                  for (var i = 0; i < kvPairs.length; i++) {
                      var kvPair = kvPairs[i].split('=');
                      queryStringColl[kvPair[0]] = kvPair[1];
                  }
              }

              return queryStringColl;
          }
          function ResetScreen() {
              $("#subMainCat,#serviceCat,#dvcomment,#btnSave,#mpeAlertPopup,#errmsg,#content").hide();
          }

          var querystringkey = "DeviceId";
          var query = getQueryStrings();
          var DeviceId = query[querystringkey];
          var apiurl_main = '../api/custfeedback/Get/'.concat(DeviceId);
          var apiurl_cat = '../api/custfeedback/GetCategory/'.concat(DeviceId);
          var apiurl_svr = '../api/custfeedback/GetServices/'.concat(DeviceId);
          var apiurl_org = '../api/custfeedback/GetOrgSetting/'.concat(DeviceId);
          var apiUrl_lbset = '../api/custfeedback/GetLabelSetting/'.concat(DeviceId);
          var apiurl_post = '../api/custfeedback/postFeedback'
          var listId_main = $('#mainList');
          var listId_cat = $('#catList');
          var listId_svr = $("#serviceList")
          var Feedback = new Object();

          $(document).ready(function () {
              ResetScreen();
              dao.initialize(function () {
                  //console.log('database initialized');
              });
              // set interval to synchronize data--
              setInterval(function () {
                  dao.CheckConnection(function (retval) {
                      if (retval == true) {
                          dao.Sync();
                      }
                  });
              }, 60000);
              //bounce the code
              setInterval(function () {
                  $(".center-block").effect("bounce",
                      { times: 3 }, 4000);
              }, 2000);
              populateList(apiurl_main, actiotype.MainList, listId_main);
              //Load the remaining services to device.
              LoadServices(apiurl_cat, actiotype.CatList);
              LoadServices(apiurl_svr, actiotype.ServiceList);

              LoadOrgSetting(apiurl_org);
              LoadLabelSetting(apiUrl_lbset);
              // getSmileys();  
          });
          $('#mainList').on('click', 'input[Id $="#Awe"]', function (event) {
              event.preventDefault();
              var smileDataValue = $(this).attr('data_val');
              var mainvalu = $(this).attr('alt');
              if (mainvalu == smileyType.Good || mainvalu == smileyType.Bad)///for Awesome and Bad
              {
                  $("body").data('mainCat', smileDataValue);
                  $('#mainCat').slideDown().hide();
                  $('#subMainCat').fadeIn('slow', function () {
                      //$('#dvmsg').text($("body").data('mainCat'));  
                  });
                  if (mainvalu == smileyType.Good) {
                      //$("#dvCatHeader").html("<img src='Images/smiley-icon.gif' />Nice! What did we do to make you happy today?");
                      $("#dvCatHeader").html(CatLabelAwe);
                  }
                  if (mainvalu == smileyType.Bad) {
                      //$("#dvCatHeader").html("<img src='Images/sad-icon.gif' />Oh No! What did we do wrong?");
                      $("#dvCatHeader").html(CatLabelBad);
                  }
                  populateList(apiurl_cat, actiotype.CatList, listId_cat, mainvalu, defaultCat);

              }
              if (mainvalu == smileyType.Indiff)//for indifference
              {
                 // $("body").data('mainCat', mainvalu);
                  $("body").data('mainCat', smileDataValue);
                  $('#mainCat,#subMainCat').hide();
                  $('#serviceCat').fadeIn('slow', function () {
                      //$('#dvmsg').text($("body").data('mainCat'));
                  });
                  if (mainvalu == smileyType.Indiff) {
                      //$("#dvsvrheader").html("<img src='Images/indif-icon.gif' /> Oh, goodness! What can we do better?");
                      $("#dvsvrheader").html(SvcLabelInd);
                  }
                  populateList(apiurl_svr, actiotype.ServiceList, listId_svr, mainvalu, defaultCat);
              }
              // $("#btnSave").fadeIn('slow');
          });
          $('#catList').on('click', 'a[data_href $="#cat"]', function (event) {
              event.preventDefault();
              var catId = $(this).attr('Id');
              var catDataValue = $(this).attr('data_val');
              var smType = $(this).attr('data_type');
              if (catId == serviceCat.OurService) {
                  $('body').data("subMainCat", catDataValue);
                  $('#subMainCat').hide();
                  $('#serviceCat').fadeIn('slow')
                  if (smType == smileyType.Good) {
                     // $("#dvsvrheader").html("<img src='Images/smiley-icon.gif' /> Which of our services blew you away?");
                      $("#dvsvrheader").html(SvcLabelAwe);
                  }
                  if (smType == smileyType.Bad) {
                      //$("#dvsvrheader").html("<img src='Images/sad-icon.gif' /> Which service needs improvement?");
                      $("#dvsvrheader").html(SvcLabelBad);
                  }

              }
              if (catId == serviceCat.OurStaff) {
                  $('body').data("subMainCat", catDataValue );
                  $('#subMainCat').hide();
                  $('#serviceCat').fadeIn('slow')
                  if (smType == smileyType.Good) {
                     // $("#dvsvrheader").html("<img src='Images/smiley-icon.gif' /> Which Staff attribute blew you away?");
                      $("#dvsvrheader").html(StfLabelAwe);
                  }
                  if (smType == smileyType.Bad) {
                      //$("#dvsvrheader").html("<img src='Images/sad-icon.gif' /> Which Staff attribute needs improvement?");
                      $("#dvsvrheader").html(StfLabelBad);
                  }
              }

              populateList(apiurl_svr, actiotype.ServiceList, listId_svr, smType, catId);
              //$("#btnSave").fadeIn('slow');
          });
          $('#serviceList').on('click', 'a[data_href $="#last"]', function () {
              var svrId = $(this).attr('Id');
              $('body').data("subLastCat", svrId);
              $('#serviceCat').hide();
              $('#dvcomment').fadeIn('slow')
              // $("#btnSave").fadeIn('slow');
          });
          $('.submit').click(function (e) {
              e.preventDefault();

              var mainCat = ""; var subMain = ""; var lastCat = "";
              if ($('body').data('mainCat') != undefined) {
                  mainCat = $('body').data('mainCat');
              }
              if ($('body').data('subMainCat') != undefined) {
                  subMain = $('body').data('subMainCat');
              }
              if ($('body').data('subLastCat') != undefined) {
                  lastCat = $('body').data('subLastCat');
              }
              var queryStringColl = getQueryStrings();//get the device id from querystring
              if (queryStringColl == null) {
                  ResetScreen();
                  alert('Invalid access. Kindly contact vendor');
                  return;

              }

              var DeviceId = queryStringColl[querystringkey];
              var btn = $(this);
              Feedback.SmileyActionID = mainCat;
              Feedback.CategoryID = subMain;
              Feedback.ServiceID = lastCat;
              Feedback.DeviceID = DeviceId;
              Feedback.DateAdded = currentDate();
              Feedback.FullName = "";
              Feedback.Email = "";
              Feedback.PhoneNo = "";
              Feedback.Comment = "";
              var email = $("#emailId").val();
              var custname = $("#txtname").val();
              var phoneno = $("#txtphoneId").val();
              var comment = $("textarea").val();
              if (btn.attr("id") == "btnsaveForm")//if the save with comment button is clicked
              {
                  if (email == "" || comment == "") {

                      $("#errmsg").show().text("Field mark with asterix (*) is/are required");
                      return;
                  } else {

                      $("#errmsg").empty().hide();
                      $('#content').show().html('<img id="loader-img" alt="" src="images/loader.gif" width="100" height="100" align="center" />');
                      Feedback.CommentInclude = "1";
                      Feedback.FullName = custname;
                      Feedback.Email = email;
                      Feedback.PhoneNo = phoneno;
                      Feedback.Comment = comment;
                  }
              } else {
                  Feedback.CommentInclude = "0";//comment not added
                  $('#content').show().html('<img id="loader-img" alt="" src="images/loader.gif" width="100" height="100" align="center" />');
              }
              var dataJson = JSON.stringify(Feedback);
              $.ajax({
                  url: apiurl_post,
                  type: "Post",
                  data: dataJson,
                  contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      $('#content').html('');
                      $("#mpeAlertPopup").fadeIn(1000).delay(2000).fadeOut("slow");
                  },
                  error: function (xhr, status) {
                      $('#content').html('');
                      if (xhr.status == 417) {
                          alert('An error occurred!!! Device has NOT been configured. Kindly contact vendor');

                      }
                      else if (status == 'timeout' || status == 'error' || status == 'notmodified' || status == 'parsererror') {
                          dao.AddFeedback(dataJson, function () {
                              $('#content').html('');
                              $("#mpeAlertPopup").fadeIn(1000).delay(2000).fadeOut("slow");
                          });
                          //alert('Data Connection Lost!');
                      }

                  }
              });

              $("#subMainCat,#serviceCat,#dvcomment").hide();
              $(this).closest('form').find("input[type=text],input[type=tel],input[type=email], textarea").val("");//clear form fielsd

              $("#mainCat").show(); //$(this).hide();
              $('body').data('mainCat', "");
              $('body').data('subMainCat', "");
              $('body').data('subLastCat', "");
          });
    </script>
</body>
</html>
    


