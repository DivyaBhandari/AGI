<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="AGISoftware.LoginPage" %>

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
            width: 380px;
            position: absolute;
            top: 54%;
            left: 50%;
            transform: translate(-50%,-50%);
            background-color: white;
            padding: 1% 2% 1% 2%;
            border-radius: 8px;
        }

            .loginContainer h2 {
                margin: 10px 0px 10% 0px;
                text-align: center;
                color: #d13400;
            }

            .loginContainer input {
                margin-bottom: 50px;
                padding: 10px 0px;
                width: 100%;
                outline: none;
                border: none;
                /*border-bottom: 2px solid #ed8139;*/
                border-bottom: 2px solid #a1978d;
            }

        #loginBtn {
            width: 100%;
            border: 1px solid #d13400;
            color: #d13400;
            background-color: white;
            font-size: 20px;
            box-shadow: 5px 5px 5px rgba(1,1,1,0.2);
            border-radius: 20px;
            padding: 10px;
            outline: none;
        }

            #loginBtn:hover {
                background-color: #d13400;
                color: white;
                box-shadow: 3px 5px 10px rgba(1,1,1,0.3);
                font-size: 20px;
            }

        .loginContainer div {
            position: relative;
        }

            .loginContainer div label {
                position: absolute;
                top: 10px;
                left: 0px;
                pointer-events: none;
                color: #a1978d;
                transition: .5s;
                font-size: 15px;
                font-family: sans-serif;
                font-weight: 600;
            }

        .loginContainer input:focus ~ label, .loginContainer input:valid ~ label {
            top: -12px;
            left: 0px;
            font-size: 13px;
            color: #d13400;
            font-family: sans-serif;
            font-weight: 600;
        }

        .loginContainer input:focus, .loginContainer input:valid {
            border-bottom: 2px solid #d13400;
        }

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
                <br />
                <span  style="color: white; font-weight: bold; font-size: 25px">Level Type: <span runat="server" id="leveltype"></span>   &nbsp;&nbsp;&nbsp;&nbsp;  Version: 2.2.4 </span>
            </div>
            <div style="position: absolute;top: 23px; right: 7px;">
                <img src="Images/BackArrow1.png" onclick="gotoAccessLevelPage()" style="height: 50px; " />
                <img src="Images/iitm.png" style="height: 120px; padding: 3px" />
            </div>
        </div>

        <div class="loginContainer">
            <h2>Sign in</h2>
            <div>
                <input type="text" id="txtUsername" runat="server" autocomplete="off" required="required" />
                <label>User ID</label>
            </div>
            <div>
                <input type="password" id="txtPassword" runat="server" required="required" />
                <label>Password</label>
            </div>
            <button type="submit" id="loginBtn" runat="server" onserverclick="loginBtn_ServerClick">Login</button>
            <br />
            <br />
            <div style="text-align: center">
                <span id="errorMsg" runat="server" visible="false" style="color: #bf0808; font-weight: 500;">Invalid username or password.</span>
            </div>
        </div>
         
            <footer>
                <div class="navbar  navbar-fixed-bottom footerBottom" style="padding: 0px 5px 20px 5px;">
                   
                    <div class="footerPanel">
                        <div class="btnImgPanel" style="margin-bottom: 0px;float:left">

                            <img src="Images/micromaticgrinding.png" style="height: 100px;padding: 3px;background-color: white" />
                         
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
        function gotoAccessLevelPage() {
             window.location.href = "LevelAccesssPage.aspx";
        }
    </script>
</body>
</html>
