<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParametersRelationshipMaster.aspx.cs" Inherits="AGISoftware.ParametersRelationshipMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12 col-lg-12 col-md-12">
                <asp:UpdatePanel runat="server" >
                    <ContentTemplate>
                <div style="display: inline; width: 50vw; text-align: center">
                     <span style="display: inline" class="lbl">Input Module</span>
                    <asp:DropDownList runat="server" ID="ddlInputModule" AutoPostBack="true"  OnSelectedIndexChanged="ddlInputModule_SelectedIndexChanged" Style="display: inline; max-width: 200px" CssClass="form-control"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                    <span style="display: inline" class="lbl">Dependent Parameter</span>
                    <asp:DropDownList runat="server" ID="ddlIP1" AutoPostBack="true" OnSelectedIndexChanged="ddlIP1_SelectedIndexChanged" Style="display: inline; max-width: 200px" CssClass="form-control"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                    <span style="display: inline" class="lbl">Independent Parameter</span>
                    <asp:DropDownList runat="server" ID="ddlIP2" Style="display: inline;max-width: 200px" CssClass="form-control"></asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button runat="server" ID="btnNewParameterDEpendency" OnClick="btnNewParameterDEpendency_Click" Style="display: inline; margin-top: 2px;" CssClass="Btns" Text="New" />
                    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Style="display: inline; " CssClass="Btns" Text="Save" />
                       <asp:Button runat="server" ID="gotoAssignvaluedependencypage"  OnClientClick="return gotoAssignValueDepedency()" Style="display: inline; " CssClass="Btns" Text="Assign Value For Dependency" />
                </div>
                         </ContentTemplate>
                   <Triggers>
                       <asp:AsyncPostBackTrigger ControlID="ddlInputModule" EventName="SelectedIndexChanged" />
                          <asp:AsyncPostBackTrigger ControlID="ddlIP1" EventName="SelectedIndexChanged" />
                   </Triggers>
                </asp:UpdatePanel>

                <asp:UpdatePanel runat="server" >
                    <ContentTemplate>
                <div style="margin-top: 15px">
                    <asp:GridView runat="server" ID="gvDependencyMaster" CssClass="headerFix CentreHeader" Style="box-shadow: none" ClientIDMode="Static" AutoGenerateColumns="false" HeaderStyle-BackColor="#edeef5" HeaderStyle-ForeColor="White" BackColor="White" ShowFooter="false" EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center">
                        <Columns>
                      
                            <asp:TemplateField HeaderText="Dependent Parameter">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblParam1" Text='<%# Bind("Parameter1") %>' />
                                    <asp:HiddenField runat="server" ID="param1InputModule" Value='<%# Bind("InputModule") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Independent Parameter">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" CssClass="form-control" style="margin: auto" ID="ddlParam2"></asp:DropDownList>
                                   <%-- <asp:Label runat="server" ID="lblParamId2" Text='<%# Bind("ParamID2") %>' />
                                    <asp:TextBox runat="server" ID="lblParam2" CssClass="form-control" Text='<%# Bind("Reccomandation") %>'></asp:TextBox>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#dedddd" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnNewParameterDEpendency" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
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
                    <input type="button" value="OK" class="btn btn-info" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
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
    <style>
        #gvDependencyMaster tr:hover, #gvDependencyMaster tr:hover td {
            background-color: #f1f3f4 !important;
        }
        .lbl{
            color: #87878a;
            font-size: 16px;
        }
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
            width: 60%;
            margin: auto;
            text-align: center !important;
        }
        
        #gvDependencyMaster {
            border: none;
        }

            #gvDependencyMaster tr:last-child td table tr td span {
                color: #87878a;
                border: 1px solid #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 16px;
            }

            #gvDependencyMaster tr:last-child td table tr td a {
                color: #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 14px;
            }
        #ParametersRelationshipMaster {
            background-color: white;
        }

            #ParametersRelationshipMaster a, #ParametersRelationshipMaster svg {
                color: brown;
            }
    </style>
    <script>

        function gotoAssignValueDepedency() {
            window.location.href = "AssignValueForDependency.aspx";
            return false;
        }
        function openWarningModal(msg) {
            $('[id*=myWarningModal]').modal('show');
            $("#warningmessageText").text(msg);
        }
        function openErrorModal(msg) {
            $('[id*=myErrorModal]').modal('show');
            $("#errormessageText").text(msg);
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
            function openWarningModal(msg) {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text(msg);
            }
            function openErrorModal(msg) {
                $('[id*=myErrorModal]').modal('show');
                $("#errormessageText").text(msg);
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
