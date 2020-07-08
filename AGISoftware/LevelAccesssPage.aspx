<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LevelAccesssPage.aspx.cs" Inherits="AGISoftware.LevelAccesssPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <style>
        body {
            /*background-color: #615f5d;*/
            width: 100vw;
            height: 100vh;
            font-family: sans-serif;
            background-image: url("Images/img1.jpeg");
            background-repeat: no-repeat;
            background-size: cover;
        }

        .loginContainer {
            width: 40%;
            min-width: 40%;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%,-50%);
            padding: 1% 2% 1% 2%;
            border-radius: 8px;
            /*border: 1px solid red;*/
            /*overflow: hidden;*/
        }
       /*#accessLevel{
             position: absolute;
            top: 50%;
            left: 50%;
           
       }*/
        .userImage {
            position: relative;
            left: 42%;
            font-size: 50px;
            background-color: gray;
            border-radius: 30px;
            padding: 7px;
            margin-bottom: 5px;
        }

            .userImage i {
                color: white;
            }
        .HeaderImage {
            flex: 1;
            float: left;
        }
      
        #accessLevel li {
            font-weight: bold;
            color: white;
            list-style: none;
            margin-bottom: 30px;
            /*border: 2px solid #fc3503;*/
            border-radius: 6px;
            display: inline-block;
            width: 770px;
            padding: 10px;
            /*text-align: center;*/
        }
         #accessLevel li:hover{
            cursor: pointer;
         }
        .levelDesciption{
            font-size: 17px;
            text-align: left;
            margin-left: 5px;
            border-radius: 4px;
        }
        .levelName {
            float: left;
            margin-right: 20px;
            font-size: 25px;
            /*color: #83e3f9;*/
           /*color:  #10343c;*/
            /*background-color: aliceblue;*/
            border-radius: 4px;
            padding: 7px 7px 10px 7px;
            /*opacity: 0.6;*/
            max-width: 260px;
             min-width: 260px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="navbar  navbar-fixed-top text-center" style="padding: 20px 5px 0px 5px;">
            <div class="HeaderImage">
                <img src="Images/amtdc.png" height="140" style="padding: 3px" />
            </div>
            <div style="width:800px; margin:auto">
                <span  style="color: #fc3503; font-weight: bold; font-size: 40px">AGI</span>
                <br />
                <span id="headerName" style="color: #fc3503; font-weight: bold; font-size: 35px">
                    (A<span style="color: white; font-weight: bold; font-size: 35px">utomation of</span> G<span style="color: white; font-weight: bold; font-size: 35px">rinding</span> P<span style="color: white; font-weight: bold; font-size: 35px">rocess</span> I<span style="color: white; font-weight: bold; font-size: 35px">ntelligence</span>)
                </span>
             
            </div>
            <div style="position: absolute;top: 23px; right: 7px;">
                <img src="Images/iitm.png" style="height: 120px; padding: 3px" />
            </div>
        </div>

        <div class="loginContainer" style="display:table">
            <ul id="accessLevel" style="display:table-cell;padding:0px;vertical-align:central;text-align:center">
                <li onclick="gotoLoginPage('level1','Basic Edition')" id="level1" runat="server" >
                    <div class="levelName" style="background-color: orange; color: white">
                        Basic Edition
                    </div>
                    <div class="levelDesciption" style="border: 2px solid orange ;padding: 0px;">
                  Limited parameters with Application Toolkit, Data input, Calculated Parameters and Output Module.
                    </div>
                </li>
                <li onclick="gotoLoginPage('level2','Standard Edition')" id="level2" runat="server">
                     <div class="levelName" style="background-color: yellow; color: black">
                         Standard Edition
                    </div>
                    <div class="levelDesciption" style="border: 2px solid yellow; padding: 0px;">
                          Basic Edition with Signal Processing module for signal analysis.
                    </div>
                </li>
                <li onclick="gotoLoginPage('level3', 'Professional Edition')" id="level3" runat="server">
                     <div class="levelName" style="background-color: #82c640; color: white">
                        Professional Edition
                    </div>
                    <div class="levelDesciption" style="border: 2px solid #82c640;padding: 0px; ">
                      Standard Edition with Admin module for complete customisation of parameters and recommendations.
                    </div>
                </li>
            </ul>
        </div>

            <footer>
                <div class="navbar  navbar-fixed-bottom footerBottom" style="padding: 0px 5px 20px 5px;">
                   
                    <div class="footerPanel">
                        <div class="btnImgPanel" style="margin-bottom: 0px;float:left">

                            <img src="Images/micromaticgrinding.png" style="height: 100px;padding: 3px; background-color: white" />
                         
                        </div>
                        <div class="" style="float:right">
                           
                            <img src="Images/DHI-Logo (2).jpg" style="height: 100px; padding: 3px" />
                        </div>
                    </div>
                    <div class="footerMaterial"></div>
                </div>
            </footer>

    </form>

    <script>
        function gotoLoginPage(leveltypeid, leveltypename) {
               $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("LevelAccesssPage.aspx/setLevelType") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                 data: '{leveltype: "' + leveltypeid + '", leveltypename: "'+leveltypename +'"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    window.location.href = "LoginPage.aspx";
                },
                error: function (Result) {
                    alert("Error");
                }
            });
        }
    </script>
</body>
</html>
