<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignPagesforUser.aspx.cs" Inherits="AGISoftware.AssignPagesforUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     
    <style>
        #AssignPagesforUser {
            background-color: white;
        }

            #AssignPagesforUser a, #AssignPagesforUser svg {
                color: brown;
            }

        div.dd_chk_select {
            height: 30px;
        }

        .headerFix {
            box-shadow: 2px 2px 8px 2px #efe7e7;
            border: none;
            margin: auto;
            width: 100%;
        }

            .headerFix tr th {
                position: sticky;
                /*top:0px;*/
                top: -1px;
                color: #87878a;
                border: none;
                background-color: #edeef5;
                z-index: 5;
                font-size: 16px;
                padding: 0px 6px;
                white-space: nowrap;
                text-align: center;
            }

            .headerFix tr td {
                padding: 5px 6px;
                color: #454444;
                font-size: 15px;
                z-index: unset;
                border: none;
                border-bottom: 1px solid #f3f2f5;
                white-space: nowrap;
                text-align: left;
            }

        #dcPages_sl {
            min-width: 120px;
            width: 200px;
        }

        .lbl {
            color: #87878a;
            font-size: 16px;
        }

        .cblist {
            /*margin: auto;*/
            /*border-collapse: separate;
            border-spacing: 0 15px;*/
            box-shadow: 2px 2px 8px 5px #b0aeae;
            width: 70%;
            padding-left: 5px;
            margin: auto;
        }

            .cblist tr:first-child {
                background-color: #3c3b54;
                color: white;
                font-weight: bold;
            }

            .cblist tr td {
                border-bottom: 1px solid #f3f2f5;
                padding: 7px 13px;
                border-left: 8px solid #3c3b54;
                border-right: 8px solid #3c3b54;
            }

            .cblist tr:last-child td {
                border-bottom: 8px solid #3c3b54;
            }

        /*#cblTransactionPages tr td {
                padding: 8px 25px;
                border: 1px solid #3c3b54;
                border-radius: 5px;
              
            }*/
        input[type=checkbox], input[type=radio] {
            width: 18px;
            height: 18px;
        }

        .cblist tr td label {
            padding-left: 5px;
            font-weight: unset;
        }
    </style>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div style="display: inline; text-align: center; margin-left: 6%">
                            <span style="display: inline" class="lbl">Employee ID</span>
                            <asp:DropDownList runat="server" ID="ddlEmployeeId" CssClass="form-control" OnSelectedIndexChanged="ddlEmployeeId_SelectedIndexChanged" AutoPostBack="true" Style="display: inline; min-width: 120px; width: 200px"></asp:DropDownList>&nbsp;&nbsp;
                            <asp:TextBox runat="server" ID="txtempname" CssClass="form-control" Style="display: inline; min-width: 120px; width: 200px" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Button runat="server" ID="Save" ClientIDMode="Static" OnClick="Save_Click" Style="display: inline" CssClass="Btns" Text="Save" />
                        </div>

                        <div style=" display: flex; margin-top: 25px;">
                            <div style=" width: 40%; ">
                                <asp:CheckBoxList runat="server" ID="cblTransactionPages" CssClass="cblist" ClientIDMode="Static"></asp:CheckBoxList>
                            </div>
                            <div style=" width: 40%; ">
                                <asp:CheckBoxList runat="server" ID="cblMasterPages" CssClass="cblist" ClientIDMode="Static"></asp:CheckBoxList>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Save" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEmployeeId" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>


                <div style="overflow: auto; width: 60%; margin: 2% auto 0px auto; display: none" id="gridContainer">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>



                            <asp:GridView runat="server" ID="gvRoleAccessRight" Visible="false" CssClass="formalGrid  headerFix" AutoGenerateColumns="false" HeaderStyle-BackColor="#2E6886" HeaderStyle-ForeColor="White" BackColor="White" ShowFooter="false" EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblRole" Text='<%# Eval("Role") %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Page">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPage" Text='<%# Eval("Page") %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2E6886" CssClass="footerGrid" />
                                <HeaderStyle HorizontalAlign="Center" />

                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Save" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="myErrorModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Error</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/error.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="errormessageText" style="font-size: 17px;">This Line ID exist in Station Information Table. Do u want to delete?</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <%--<asp:Button runat="server" Text="Close" ID="Button1" CssClass="btn btn-info" BackColor="#5D7B9D" ForeColor="white" />--%>
                    <input type="button" value="OK" class="btn btn-info" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            var wHeight = $(window).height() - (200);
            $('#gridContainer').css('height', wHeight);
        });
        $(window).resize(function () {
            var Height = $(window).height() - (200);
            $('#gridContainer').css('height', Height);
        });

        $("#cblTransactionPages td").change(function () {
            var CHK = document.getElementById("cblTransactionPages");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");
            var counter = 0;
            if ($(this).children()[1].innerHTML == "Transactions") {
                if (checkbox[0].checked == true) {
                    for (var i = 1; i < checkbox.length; i++) {
                        checkbox[i].checked = true;
                    }
                }
                else {
                    for (var i = 1; i < checkbox.length; i++) {
                        checkbox[i].checked = false;
                    }
                }
            }
            if ($(this).children()[0].checked == false) {
                checkbox[0].checked = false;
            }
            for (var i = 1; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }

            if (counter == checkbox.length - 1) {
                checkbox[0].checked = true;
            }
        });

         $("#cblMasterPages td").change(function () {
            var CHK = document.getElementById("cblMasterPages");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");
            var counter = 0;
            if ($(this).children()[1].innerHTML == "Masters") {
                if (checkbox[0].checked == true) {
                    for (var i = 1; i < checkbox.length; i++) {
                        checkbox[i].checked = true;
                    }
                }
                else {
                    for (var i = 1; i < checkbox.length; i++) {
                        checkbox[i].checked = false;
                    }
                }
            }
            if ($(this).children()[0].checked == false) {
                checkbox[0].checked = false;
            }
            for (var i = 1; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }

            if (counter == checkbox.length - 1) {
                checkbox[0].checked = true;
            }
        });

        function showpop5(msg, title) {
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": true,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": true,
                    "onclick": null,
                    "showDuration": "3000",
                    "hideDuration": "10000",
                    "timeOut": "500",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                // toastr['success'](msg, title);
                var d = Date();
                toastr.success(msg, title);
                return false;
            }
        function openErrorModal(msg) {
            $('[id*=myErrorModal]').modal('show');
            $("#errormessageText").text(msg);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $(document).ready(function () {
                var wHeight = $(window).height() - (200);
                $('#gridContainer').css('height', wHeight);
            });
            $(window).resize(function () {
                var Height = $(window).height() - (200);
                $('#gridContainer').css('height', Height);
            });
            $("#cblMasterPages td").change(function () {
                var CHK = document.getElementById("cblMasterPages");
                var checkbox = CHK.getElementsByTagName("input");
                var label = CHK.getElementsByTagName("label");
                var counter = 0;
                if ($(this).children()[1].innerHTML == "Masters") {
                    if (checkbox[0].checked == true) {
                        for (var i = 1; i < checkbox.length; i++) {
                            checkbox[i].checked = true;
                        }
                    }
                    else {
                        for (var i = 1; i < checkbox.length; i++) {
                            checkbox[i].checked = false;
                        }
                    }
                }
                if ($(this).children()[0].checked == false) {
                    checkbox[0].checked = false;
                }
                for (var i = 1; i < checkbox.length; i++) {
                    if (checkbox[i].checked) {
                        counter++;
                    }
                }

                if (counter == checkbox.length - 1) {
                    checkbox[0].checked = true;
                }
            });

            $("#cblTransactionPages td").change(function () {
                var CHK = document.getElementById("cblTransactionPages");
                var checkbox = CHK.getElementsByTagName("input");
                var label = CHK.getElementsByTagName("label");
                var counter = 0;
                if ($(this).children()[1].innerHTML == "Transactions") {
                    if (checkbox[0].checked == true) {
                        for (var i = 1; i < checkbox.length; i++) {
                            checkbox[i].checked = true;
                        }
                    }
                    else {
                        for (var i = 1; i < checkbox.length; i++) {
                            checkbox[i].checked = false;
                        }
                    }
                }
                if ($(this).children()[0].checked == false) {
                    checkbox[0].checked = false;
                }
                for (var i = 1; i < checkbox.length; i++) {
                    if (checkbox[i].checked) {
                        counter++;
                    }
                }

                if (counter == checkbox.length - 1) {
                    checkbox[0].checked = true;
                }
            });
            function showpop5(msg, title) {
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": true,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": true,
                    "onclick": null,
                    "showDuration": "3000",
                    "hideDuration": "10000",
                    "timeOut": "500",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                // toastr['success'](msg, title);
                var d = Date();
                toastr.success(msg, title);
                return false;
            }
            function openErrorModal(msg) {
                $('[id*=myErrorModal]').modal('show');
                $("#errormessageText").text(msg);
            }
        });
    </script>

</asp:Content>
