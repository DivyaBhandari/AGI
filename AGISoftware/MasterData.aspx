<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterData.aspx.cs" Inherits="AGISoftware.MasterData" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <asp:HiddenField ID="hdnScrollPos" ClientIDMode="Static" runat="server" />
    <div>

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div id="divFilter">
                    <div>
                        <asp:Label runat="server" Text="Input Category" CssClass="lbls" Style="vertical-align: middle; font-size: 16px; display: inline-block;" />
                        <asp:DropDownList runat="server" ID="ddlInputModule" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlInputModule_SelectedIndexChanged" Style="display: inline-block; margin: auto; width: 200px; min-width: 120px;" ToolTip="Select Input Category" />
                    </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <div>
                        <asp:Label runat="server" Text="Sub-Input Category" CssClass="lbls" Style="vertical-align: middle; font-size: 16px; display: inline-block;" />
                        <asp:DropDownList runat="server" ID="ddlSubInputModule" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSubInputModule_SelectedIndexChanged" Style="display: inline-block; margin: auto; width: 200px; min-width: 120px;" ToolTip="Select Sub-Input Category" />
                    </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <div>
                        <asp:Label runat="server" CssClass="lbls" Text="Parameter" Style="vertical-align: middle; font-size: 16px; display: inline-block;" />
                        <asp:DropDownList runat="server" ID="ddlParameter" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlParameter_SelectedIndexChanged" Style="display: inline-block; margin: auto; width: 200px; min-width: 120px;" ToolTip="Select Parameter" />
                    </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <div>
                        <asp:Button runat="server" ID="btnView" Text="View" CssClass="Btns" Style="display: inline-block; " OnClick="btnView_Click" ToolTip="View" />
                        <asp:Button runat="server" ID="save" Text="Save" CssClass="Btns" OnClick="save_Click" Style="display: inline-block; "  ToolTip="Save" />
                        <asp:Button runat="server" ID="btnNew" Text="New" CssClass="Btns" Style="display: inline-block; " OnClick="btnNew_Click" ClientIDMode="Static" ToolTip="New"  />
                        <asp:LinkButton runat="server" ID="lbnReload" CssClass="glyphicon glyphicon-refresh ReloadBtn" Style="" OnClick="lbnReload_Click" ToolTip="Reload" />

                    </div>
                </div>
         
                <div id="displayContainer" style="width: 75%; overflow: auto; margin: auto; box-shadow: 2px 2px 8px 2px #efe7e7;">
                    <asp:GridView runat="server" ID="gvParameterList" CssClass="headerFix CentreHeader" Style="box-shadow: none" ClientIDMode="Static" AutoGenerateColumns="false" HeaderStyle-BackColor="#edeef5" HeaderStyle-ForeColor="White" BackColor="White" ShowFooter="false" EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center" OnRowEditing="gvParameterList_RowEditing" OnRowUpdating="gvParameterList_RowUpdating" OnRowDeleting="gvParameterList_RowDeleting" OnRowCancelingEdit="gvParameterList_RowCancelingEdit">
                        <Columns>
                            <asp:TemplateField HeaderText="Input Category">
                                <ItemTemplate>
                                     <asp:HiddenField runat="server" ID="hdfIDEdit" Value='<%# Eval("Id") %>' />
                                    <asp:Label runat="server" ID="edtlblInputModule" Text='<%# Bind("InputModule") %>' />
                                  
                                </ItemTemplate>
                                <EditItemTemplate>
                                     <asp:HiddenField runat="server" ID="hdfID" Value='<%# Eval("Id") %>' />
                                    <asp:Label runat="server" ID="lblInputModule" Text='<%# Bind("InputModule") %>' />
                                </EditItemTemplate>
                                <%--<FooterTemplate>
                                    <asp:DropDownList runat="server" ID="ddlIpModule" CssClass="form-control" OnSelectedIndexChanged="ddlIpModule_SelectedIndexChanged" />
                                </FooterTemplate>--%>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Input Sub-Category">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSubInputModule" Text='<%# Bind("SubInputModule") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label runat="server" ID="edtlblSubInputModule" Text='<%# Bind("SubInputModule") %>' />
                                </EditItemTemplate>
                                <%--<FooterTemplate>
                                    <asp:DropDownList runat="server" ID="ddlSipModule" CssClass="form-control" OnSelectedIndexChanged="ddlSipModule_SelectedIndexChanged" />
                                </FooterTemplate>--%>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parameter">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hiddenParamID" Value='<%# Bind("ParameterID") %>' />
                                     <asp:HiddenField runat="server" ID="hiddenParamDataType" Value='<%# Bind("DataType") %>' />
                                    <asp:Label runat="server" ID="lblParameter" Text='<%# Bind("Parameter") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:HiddenField runat="server" ID="hiddenParamID" Value='<%# Bind("ParameterID") %>' />
                                    <asp:Label runat="server" ID="edtlblParameter" Text='<%# Bind("Parameter") %>' />
                                </EditItemTemplate>
                                <%--<FooterTemplate>
                                    <asp:DropDownList runat="server" ID="ddlParam" CssClass="form-control" OnSelectedIndexChanged="ddlParam_SelectedIndexChanged" />
                                </FooterTemplate>--%>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                       <asp:TextBox runat="server" ID="txtListValue" AutoCompleteType="Disabled" CssClass="form-control" Text='<%# Bind("ListValue") %>'  />
                                    
                                    <%-- <asp:HiddenField runat="server" ID="hdfColName" Value='<%# Eval("column_name") %>' />--%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                 <asp:Label runat="server" ID="lblListValue" Text='<%# Bind("ListValue") %>' />
                                </EditItemTemplate>
                                <%--<FooterTemplate>
                                    <asp:TextBox runat="server" ID="ftxtListValue" CssClass="form-control" />
                                </FooterTemplate>--%>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                 <%--   <asp:LinkButton runat="server" ID="lbtnEdit" CommandName="Edit" CssClass="glyphicon glyphicon-edit" ToolTip="Edit" />--%>
                                    <asp:LinkButton runat="server" ID="lbtnDelete" CommandName="Delete" CssClass="glyphicon glyphicon-trash DeleteBtn" ToolTip="Delete" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton runat="server" CssClass="glyphicon  glyphicon-plus-sign " ID="lbtnupdate" CommandName="Update" ToolTip="Update" OnClick="lbtnupdate_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" CssClass="glyphicon glyphicon-remove " ForeColor="#d88282" ToolTip="Cancel" />
                                </EditItemTemplate>
                                <%--<FooterTemplate>
                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Save" Font-Size="16" ToolTip="Save" OnClick="btnSave_Click" />
                                </FooterTemplate>--%>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                <FooterStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>

                        <EditRowStyle BackColor="#dedddd" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <%--  <PagerStyle BackColor="#2E6886" ForeColor="White" HorizontalAlign="Center" />--%>
                    </asp:GridView>
                </div>

                <div id="divInput" style="margin-top: 5px;">
                    <table style="width: 100%;">
                        <tr>
                            <th>
                                <asp:DropDownList runat="server" ID="ddlIpModule" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIpModule_SelectedIndexChanged" ClientIDMode="Static" ToolTip="Select Input Category" />
                                <asp:Label runat="server" ID="lblIpModule" Font-Size="12" Visible="false" Style="padding: 3px 2px;" CssClass="Elliplabel" />
                            </th>
                            <th>
                                <asp:DropDownList runat="server" ID="ddlSipModule" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSipModule_SelectedIndexChanged" ClientIDMode="Static" ToolTip="Select Sub-Input Category" />
                                <asp:Label runat="server" ID="lblSipModule" Font-Size="12" Visible="false" Style="padding: 3px 3px;" CssClass="Elliplabel" />
                            </th>
                            <th>
                                <asp:DropDownList runat="server" ID="ddlParam" CssClass="form-control" OnSelectedIndexChanged="ddlParam_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" ToolTip="Select Parameter" />
                                <asp:HiddenField runat="server" ID="hiddenlblParamId" />
                                  <asp:HiddenField runat="server" ID="hiddenlblParamDatatype" />
                                <asp:Label runat="server" ID="lblParam" Font-Size="12" Visible="false" Style="padding: 3px 2px;" CssClass="Elliplabel" />
                            </th>
                            <th>
                                <asp:TextBox runat="server" ID="ftxtListValue" AutoCompleteType="Disabled" CssClass="form-control" Style="text-align: center;" ClientIDMode="Static" ToolTip="Enter List Value" placeholder="Enter Value" />
                            </th>
                            <th style="">
                                <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Save" ToolTip="Save" OnClick="btnSave_Click" OnClientClick="return checkForEmpty()" />
                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Style="background-color: #d88282; border: 1px solid #d88282" Text="Cancel" ToolTip="Cancel" OnClientClick="showDiv('hide');" OnClick="btnCancel_Click" />
                            </th>
                        </tr>
                    </table>
                </div>
                <div style="width: 70%; margin: auto; margin-top: 10px; display: flex; flex-flow: row wrap; justify-content: flex-end;">
                    <%--<asp:Button runat="server" ID="btnNew" Text="New" CssClass="Btns" OnClick="btnNew_Click" ClientIDMode="Static" ToolTip="New"  />--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    <div class="modal fade" id="mySuccessModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Success!</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/icon-success.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="successmessageText" style="font-size: 17px;">This Line ID exist in Station Information Table. Do u want to delete?</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <%--<asp:Button runat="server" Text="Close" ID="Button1" CssClass="btn btn-info" BackColor="#5D7B9D" ForeColor="white" />--%>
                    <input type="button" value="OK" class="btn btn-info" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="myWarningModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Warning!</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/warnig.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="warningmessageText" style="font-size: 17px;">This Line ID exist in Station Information Table. Do u want to delete?</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <%--<asp:Button runat="server" Text="Close" ID="Button1" CssClass="btn btn-info" BackColor="#5D7B9D" ForeColor="white" />--%>
                    <input type="button" value="OK" class="btn btn-info" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="myErrorModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Warning!</h4>
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

    <div class="modal fade" id="myConfirmationModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/confirm.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="confirmationmessageText" style="font-size: 17px;">Confirmation</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <%--<asp:Button runat="server" Text="Close" ID="Button1" CssClass="btn btn-info" BackColor="#5D7B9D" ForeColor="white" />--%>
                    <%--  onserverclick="saveConfirmYes_ServerClick"--%>
                    <input type="button" value="Yes" class="btn btn-info" id="deleteConfirmYes" onserverclick="deleteConfirmYes_ServerClick" runat="server" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="No" class="btn btn-info" id="deleteConfirmNo" onclick="deleteConfirmNoFunc()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>
    <style>
        #gvParameterList tr:hover, #gvParameterList tr:hover td {
            background-color: #f1f3f4 !important;
        }

         /*#displayContainer tr:last-child td {
            position: sticky;
            bottom: 0px;
            text-align: center;
            background-color: #edeef5;
            z-index: 5;
            padding: 0px 6px;
        }*/

        #MasterData {
            /*background-color: #fd3801;*/
            background-color: white;
        }

            #MasterData a, #MasterData svg {
                color: brown;
            }

        .lbls {
            color: #87878a;
        }
        /*#gvParameterList tr:last-child td {
            position: sticky;
            bottom: 0px;
            text-align: center;
            background-color: #edeef5;
            z-index: 5;
            padding: 0px 6px;
        }*/

        #gvParameterList tr:last-child td table {
            /*margin:auto;*/
        }

        #gvParameterList {
            border: none;
        }

            #gvParameterList tr:last-child td table tr td span {
                color: #87878a;
                border: 1px solid #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 16px;
            }

            #gvParameterList tr:last-child td table tr td a {
                color: #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 14px;
            }


        #tblFilter {
            border: none;
            width: 100%;
            color: white;
            margin-bottom: 20px;
            margin-left: 5px;
        }

        #divFilter {
            display: flex;
            flex-wrap: wrap;
            /*justify-content: space-evenly;
            align-content: space-between;*/
            width: 100%;
            color: white;
            margin-bottom: 20px;
            margin-left: 15px;
        }

        .ReloadBtn {
            display: inline-block;
            font-size: 26px;
            margin: auto;
            vertical-align: middle;
            color: #44908d;
            text-decoration: none;
        }

            .ReloadBtn:hover {
                color: #2d5e5c;
            }

        a:hover {
            text-decoration: none;
        }

        .DeleteBtn {
            color: #d88282;
        }

            .DeleteBtn:hover {
                color: #d88282;
            }
        /*.headerFix{
            width:70%;
            margin:auto;
            height:50%;
            text-align:center;
        }*/
        .headerFix {
            box-shadow: 2px 2px 8px 2px #efe7e7;
            border: none;
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
                padding: 4px 6px;
                color: #454444;
                font-size: 15px;
                z-index: unset;
                border: none;
                border-bottom: 1px solid #f3f2f5;
                white-space: nowrap;
                text-align: left;
            }

              #gvParameterList tr td:nth-child(5) {
               text-align:center;
            }
        /*.headerFix tr:last-child td {
            position:sticky;
            bottom:-1px;
            z-index:5;
            color: white;
            background-color: #2E6886;
        }*/
        .CentreHeader {
            width: 100%;
            margin: auto;
            text-align: center !important;
        }

        #divInput {
            width: 96%;
            margin: auto;
            background-color: #edeef5;
        }

            #divInput tr th {
                font-size: 12px;
                padding: 3px 3px;
                text-align: center;
                width: 20% !important;
            }

        .Elliplabel {
            color: #87878a;
        }

        /*.Elliplabel {
            display: block;
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
        }*/
    </style>
    <script>
        //function formUnloadPrompt(formSelector) {
        //    var formA = $(formSelector).serialize(), formB, formSubmit = false;

        //    // Detect Form Submit
        //    $(formSelector).submit(function () {
        //        formSubmit = true;
        //    });

        //    // Handle Form Unload    
        //    window.onbeforeunload = function (e) {
        //        if (formSubmit) return;
        //        formB = $(formSelector).serialize();
        //        if (formA != formB) {

        //            e.returnValue = "Your changes have not been saved.";

        //        }

        //    };
        //}

        //$(function () {
        //    formUnloadPrompt('form');
        //});

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
    

        $('.allowDecimal').keypress(function (evt) {
            debugger;
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            var afterdecimalpt = $(this).val().split('.')[1];
            var beforedecimalpt = $(this).val().split('.')[0];
            var pos = evt.target.selectionStart;
            var ptpos = $(this).val().indexOf(".");


            if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            if (charCode == 46 && $(this).val().indexOf('.') != -1) {
                return false;
            }


            if ($(this).val().length > 6 && $(this).val().indexOf('.') == -1) {
                if (charCode == 46) {
                    return true;
                } else {
                    return false;
                }

            }
            else {
                if (pos <= ptpos) {
                    if (beforedecimalpt != undefined) {
                        if (beforedecimalpt.length > 6) {

                            return false;
                        }

                    }

                } else {
                    if (afterdecimalpt != undefined) {
                        if (afterdecimalpt.length > 3) {
                            return false;
                        }
                    }

                }
            }


            return true;
        });

       function deleteConfirmNoFunc() {
           document.getElementById('divInput').style.display = 'none';
            $('#btnNew').removeAttr('disabled');
           $('[id*=myConfirmationModal]').modal('hide');
            }
        function openConfirmModal(msg) {
            $('[id*=myConfirmationModal]').modal('show');
            $("#confirmationmessageText").text(msg);
        }

        function openErrorModal(msg) {
            debugger;
            $('[id*=myErrorModal]').modal('show');
            $("#errormessageText").text(msg);
        };
        function openWarningModal(msg) {
            $('[id*=myWarningModal]').modal('show');
            $("#warningmessageText").text(msg);
        };
        function openSuccessModal(msg) {

            $('[id*=mySuccessModal]').modal('show');
            $("#successmessageText").text(msg);
        };
        $('[id$=btnNew]').click(function () {
            debugger;
            $(this).removeClass("notClicked");
            $(this).addClass("clicked");
            //$(this).siblings().removeClass("active");
            //$(this).siblings().addClass("inactive");
        });

        var bigDiv = document.getElementById('displayContainer');
        bigDiv.onscroll = function () {
            $('[id*=hdnScrollPos]').val(bigDiv.scrollTop);
        }
        window.onload = function () {
              
            $('#displayContainer').animate({ scrollTop: $('[id*=hdnScrollPos]').val() }, 50);
        }

        //var bigDiv = document.getElementById('displayContainer');
        //bigDiv.onscroll = function () {

        //    $('[id*=hdnScrollPos]').val(bigDiv.scrollTop);
        //    console.log("id scroll =" + $('[id*=hdnScrollPos]').val());
        //}
        //window.onload = function () {

        //    bigDiv.scrollTop = $('[id*=hdnScrollPos]').val();
        //    console.log("id load =" + bigDiv.scrollTop);
        //}

        $(document).ready(function () {
            document.getElementById('divInput').style.display = 'none';

            var wHeight = $(window).height() - (250);
            $('#displayContainer').css('height', wHeight);

        });

        $(window).resize(function () {
            var Height = $(window).height() - (250);
            $('#displayContainer').css('height', Height);
        });
        function showDiv(option) {
            if (option == "show") {
                document.getElementById('divInput').style.display = 'block';
            }
            else {
                document.getElementById('divInput').style.display = 'none';
            }
        }
        function checkForEmpty() {
            if ($("#ddlIpModule").val() == "ALL") {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Please select an Input Category!");
                return false;
            }
            else if ($('#ddlSipModule').val() == "") {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Please select a Sub-Input Category!");
                return false;
            }
            else if ($("#ddlParam").val() == "") {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Please select a Parameter!");
                return false;
            }
            else if ($("#ftxtListValue").val() == "") {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Please enter a List Value!");
                return false;
            }
            else {
                return true;
            }
        }

       

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
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
      

            $('.allowDecimal').keypress(function (evt) {
                debugger;
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var afterdecimalpt = $(this).val().split('.')[1];
                var beforedecimalpt = $(this).val().split('.')[0];
                var pos = evt.target.selectionStart;
                var ptpos = $(this).val().indexOf(".");


                if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                if (charCode == 46 && $(this).val().indexOf('.') != -1) {
                    return false;
                }


                if ($(this).val().length > 6 && $(this).val().indexOf('.') == -1) {
                    if (charCode == 46) {
                        return true;
                    } else {
                        return false;
                    }

                }
                else {
                    if (pos <= ptpos) {
                        if (beforedecimalpt != undefined) {
                            if (beforedecimalpt.length > 6) {

                                return false;
                            }

                        }

                    } else {
                        if (afterdecimalpt != undefined) {
                            if (afterdecimalpt.length > 3) {
                                return false;
                            }
                        }

                    }
                }


                return true;
            });
        
         var bigDiv = document.getElementById('displayContainer');
            bigDiv.onscroll = function () {
                $('[id*=hdnScrollPos]').val(bigDiv.scrollTop);
            }
           $('#displayContainer').animate({ scrollTop: $('[id*=hdnScrollPos]').val() }, 50);
            window.onload = function () {
                $('#displayContainer').animate({ scrollTop: $('[id*=hdnScrollPos]').val() }, 50);
            }



            //function formUnloadPrompt(formSelector) {
            //    var formA = $(formSelector).serialize(), formB, formSubmit = false;

            //    // Detect Form Submit
            //    $(formSelector).submit(function () {
            //        formSubmit = true;
            //    });

            //    // Handle Form Unload    
            //    window.onbeforeunload = function (e) {
            //        if (formSubmit) return;
            //        formB = $(formSelector).serialize();
            //        if (formA != formB) {

            //            e.returnValue = "Your changes have not been saved.";

            //        }

            //    };
            //}

            //$(function () {
            //    formUnloadPrompt('form');
            //});



            function openConfirmModal(msg) {
                $('[id*=myConfirmationModal]').modal('show');
                $("#confirmationmessageText").text(msg);
            }
            function deleteConfirmNoFunc() {
                document.getElementById('divInput').style.display = 'none';
                $('#btnNew').removeAttr('disabled');
                $('[id*=myConfirmationModal]').modal('hide');
            }
            //function openErrorModal(msg) {
            //    $('[id*=myErrorModal]').modal('show');
            //    $("#errormessageText").text(msg);
            //};

          
            $(document).ready(function () {

                var wHeight = $(window).height() - (230);
                $('#displayContainer').css('height', wHeight);

            });

            $(window).resize(function () {
                var Height = $(window).height() - (230);
                $('#displayContainer').css('height', Height);
            });

            $('[id$=btnNew]').click(function () {
                debugger;
                $(this).removeClass("notClicked");
                $(this).addClass("clicked");
                //$(this).siblings().removeClass("active");
                //$(this).siblings().addClass("inactive");
            });
            function openErrorModal(msg) {
                debugger;
                $('[id*=myErrorModal]').modal('show');
                $("#errormessageText").text(msg);
            };
            function showDiv(option) {
                if (option == "show") {
                    document.getElementById('divInput').style.display = 'block';
                }
                else {
                    document.getElementById('divInput').style.display = 'none';
                }
            }
            function checkForEmpty() {
                if ($("#ddlIpModule").val() == "Select I/P Module") {
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Please select an Input Module!");
                    return false;
                }
                else if ($("#ftxtListValue").val() == "") {
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Please enter a List Value!");
                    return false;
                }
                else {
                    return true;
                }
            }

        });
    </script>
</asp:Content>
