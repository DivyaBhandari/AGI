<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Version.aspx.cs" Inherits="AGISoftware.Version" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Version</title>
    <style>
        div{
            max-width: 80vw;
            margin:auto;
        }
        div > h3{
            color: #fc3503;
            font-weight:bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="versionContainer">
            <h3>Vesions 1</h3>
            <ul>
                <li>Application Version No. 1.6</li>
            </ul>
            <h3>Vesions 2</h3>
            <ul>
                <li>Application Version No. 2.1</li>
            </ul>
        </div>
    </form>
</body>
</html>
