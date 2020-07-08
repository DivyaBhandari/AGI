<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParameterDependenacyList.aspx.cs" Inherits="AGISoftware.ParameterDependenacyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        .lbl {
            color: #87878a;
            font-size: 16px;
        }
        .headerFix {
            box-shadow: 2px 2px 8px 2px #efe7e7;
            border: none;
            width:100%;
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
                text-align:center;
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
          .CentreHeader {
            width: 100%;
            margin: auto;
            text-align: center !important;
        }
        
        #gvDependencyTransaction {
            border: none;
        }

            #gvDependencyTransaction tr:last-child td table tr td span {
                color: #87878a;
                border: 1px solid #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 16px;
            }

            #gvDependencyTransaction tr:last-child td table tr td a {
                color: #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 14px;
            }
        #ParameterDependenacyList {
            background-color: white;
        }

            #ParameterDependenacyList a,  #ParameterDependenacyList svg {
                color: brown;
            }
            #gvDependencyTransaction tr:hover, #gvDependencyTransaction tr:hover td {
                background-color: #f1f3f4 !important;
            }
    </style>

    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12 col-lg-12 col-md-12">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div style="display: inline; width: 100%; text-align: center">
                            <span style="display: inline" class="lbl">Dependent Parameter</span>
                            <asp:DropDownList runat="server" ID="ddlDependentParam" AutoPostBack="true" Style="display: inline" CssClass="form-control" OnSelectedIndexChanged="ddlDependentParam_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                     <span style="display: inline" class="lbl">Independent Parameter</span>
                             <asp:DropDownList runat="server" ID="ddlIndependentParam"  Style="display: inline" CssClass="form-control"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnView" OnClick="btnView_Click"  Style="display: inline" CssClass="Btns" Text="View" />
                            <asp:Button runat="server" ID="btnSave"  OnClick="btnSave_Click" Style="display: inline" CssClass="Btns" Text="Save" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDependentParam" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                     <div style="margin: 15px auto 0px auto; overflow:auto; width:70%" id="gridContainer">
                     <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                    <asp:GridView runat="server" ID="gvDependencyTransaction" CssClass="headerFix CentreHeader" Style="box-shadow: none" ClientIDMode="Static" AutoGenerateColumns="false" HeaderStyle-BackColor="#edeef5" HeaderStyle-ForeColor="White" BackColor="White" ShowFooter="false" EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center">
                        <Columns>

                            <asp:TemplateField HeaderText="Dependent Parameter">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDependentParam" Text='<%# Bind("Parameter1") %>' />
                                    <asp:HiddenField runat="server" ID="hdDependentParamID" Value='<%# Bind("ParameterId1") %>' />
                                     <asp:HiddenField runat="server" ID="hdIndependentParamID" Value='<%# Bind("ParameterId2") %>' />
                                     <asp:HiddenField runat="server" ID="hdIndependentParam" Value='<%# Bind("Parameter2") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                             <asp:TemplateField HeaderText="Independent Parameter Value">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblIndependentParamValue" Text='<%# Bind("Parameter2Value") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LSL">
                                <ItemTemplate>
                                 <asp:TextBox runat="server" ID="txtLSL" CssClass="form-control decimalPt"   AutoCompleteType="Disabled" onblur="return checkLSLUSLValue(this, 'LSL')" Text='<%# Bind("LSL") %>'></asp:TextBox>
                                    <%--onblur="return LSLUSLValidation(this,'LSL');"--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="USL">
                                <ItemTemplate>
                                 <asp:TextBox runat="server" ID="txtUSL" onblur="return checkLSLUSLValue(this, 'USL')" CssClass="form-control decimalPt" AutoCompleteType="Disabled"  Text='<%# Bind("USL") %>'></asp:TextBox>
                                    <%--onblur="return LSLUSLValidation(this,'USL');"--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#dedddd" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:GridView>
                        </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
                         <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
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

     <div class="modal fade" id="WarningModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Warning!</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/warnig.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="warningmessageText" style="font-size: 17px;">Higest value of LSL is greater than lowest value of USL</span>
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

           

            $('.decimalPt').keypress(function (evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;

                if (charCode == 43 && ($(this).val().length - 1) != -1) {
                    return false;
                } else if (charCode == 45 && ($(this).val().length - 1) != -1) {
                    return false;
                } else if (charCode == 46 && $(this).val().indexOf('.') != -1) {
                    return false;
                } else if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57) && charCode != 43 && charCode != 45) {
                    return false;
                }
                return true;
            });
            function openErrorModal(msg) {
                $('[id*=myErrorModal]').modal('show');
                $("#errormessageText").text(msg);
            };
            function openWarningrModal() {
                $('[id*=WarningModal]').modal('show');
                 $("#warningmessageText").text('Higest value of LSL is greater than lowest value of USL');

            };
            function ExceedRangeWarningForQualityParam(data, param) {
                debugger;
                if (isNaN($(data).val())) {
                    $(data).val("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("This value is not a number");
                    return;
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                }
            } 

            function checkLSLUSLValue(val, param) {
                debugger;
                if (param == "LSL") {
                    if (parseFloat($(val).val()) > parseFloat($(val).closest('tr').find('#txtUSL').val())) {
                        $(val).val("");
                        $('[id*=WarningModal]').modal('show');
                        $("#warningmessageText").text('LSL value is greater than USL value');
                    }
                }
                if (param == "USL") {
                    if (parseFloat($(val).val()) < parseFloat($(val).closest('tr').find('#txtLSL').val())) {
                        $(val).val("");
                        $('[id*=WarningModal]').modal('show');
                        $("#warningmessageText").text('USL value is smaller than LSL value');
                    }
                }
            }
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

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $(document).ready(function () {
                var wHeight = $(window).height() - (200);
                $('#gridContainer').css('height', wHeight);
            });
            $(window).resize(function () {
                var Height = $(window).height() - (200);
                $('#gridContainer').css('height', Height);
            });

            function checkLSLUSLValue(val, param) {
                debugger;
                if (param == "LSL") {
                    if (parseFloat($(val).val()) > parseFloat($(val).closest('tr').find('#txtUSL').val())) {
                        $(val).val("");
                        $('[id*=WarningModal]').modal('show');
                        $("#warningmessageText").text('LSL value is greater than USL value');
                    }
                }
                if (param == "USL") {
                    if (parseFloat($(val).val()) < parseFloat($(val).closest('tr').find('#txtLSL').val())) {
                        $(val).val("");
                        $('[id*=WarningModal]').modal('show');
                        $("#warningmessageText").text('USL value is smaller than LSL value');
                    }
                }
            }

            $('.decimalPt').keypress(function (evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;

                if (charCode == 43 && ($(this).val().length - 1) != -1) {
                    return false;
                } else if (charCode == 45 && ($(this).val().length - 1) != -1) {
                    return false;
                } else if (charCode == 46 && $(this).val().indexOf('.') != -1) {
                    return false;
                } else if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57) && charCode != 43 && charCode != 45) {
                    return false;
                }
                return true;
            });
            function openErrorModal(msg) {
                $('[id*=myErrorModal]').modal('show');
                $("#errormessageText").text(msg);
            };
            function openWarningrModal() {
                $('[id*=WarningModal]').modal('show');
                  $("#warningmessageText").text('Higest value of LSL is greater than lowest value of USL');
            }
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
        });
    </script>


</asp:Content>
