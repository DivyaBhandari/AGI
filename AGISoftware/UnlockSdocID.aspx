<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnlockSdocID.aspx.cs" Inherits="AGISoftware.UnlockSdocID" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <style>
        .lbl {
            color: #87878a;
            font-size: 16px;
        }
        #tbl tr td {
            padding-right: 10px;
        }

        #tbl {
            margin-left: 6%
        }

        #lstVendors {
            padding: 20px;
        }

            #lstVendors div {
                padding: 10px;
            }

        #gvDeletedSdoc {
            border: none;
        }

            #gvDeletedSdoc tr:last-child td table tr td span {
                color: #87878a;
                border: 1px solid #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 16px;
            }

            #gvDeletedSdoc tr:last-child td table tr td a {
                color: #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 14px;
            }

        .headerFix {
            box-shadow: 2px 2px 8px 2px #efe7e7;
            border: none;
        }

            .headerFix tr th {
                position: sticky;
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

        #UnlockSdocID {
            background-color: white;
        }

            #UnlockSdocID a, #UnlockSdocID svg {
                color: brown;
            }
    </style>

     <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12 col-lg-12 col-md-12">
                 <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                 <div style="display: inline">
                     <span style="display: inline" class="lbl">SystemDoc for Unlock</span>
                     <asp:DropDownList runat="server" ID="ddlUnlockSdoc" Style="display: inline;min-width: 340px" CssClass="form-control"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Button runat="server" ID="btnView" Style="display: inline" CssClass="Btns" Text="View" OnClick="btnView_Click" />
                     <asp:Button runat="server" ID="btnUnlock" Style="display: inline" OnClientClick="return showConfirmationForUnlockSDoc();" CssClass="Btns" Text="Unlock" />
                    
                 </div>

               
                <div id="displayContainer" style="width: 100%; overflow: auto; margin: auto; box-shadow: 2px 2px 8px 2px #efe7e7; margin-top: 20px">
                    <table id="tbl" clientidmode="static" runat="server" visible="false">
                        <tr>
                            <td>Username:</td>
                            <td><span runat="server" id="username"></span></td>
                        </tr>
                        <tr>
                            <td>DateTime:</td>
                            <td><span runat="server" id="lockedDate"></span></td>
                        </tr>
                    </table>
                    <asp:ListView runat="server" ID="lvGeneralInfo" ClientIDMode="Static">
                        <LayoutTemplate>

                            <ul runat="server" id="lstVendors" style="text-align: center; width: 98%; margin: auto;">
                                <li runat="server" id="itemPlaceholder" />
                            </ul>
                        </LayoutTemplate>

                        <ItemTemplate>
                            <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 45%; margin-right: 20px">

                                <asp:Label runat="server" CssClass="toggleColor" ID="item" Width="200px" Style="min-width: 200px; color: #454444; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;" Text='<%# Eval("Parameter") %>'></asp:Label>

                                <asp:Label runat="server" ID="gicalculatedflag" CssClass="form-control" Width="350px" Text='<%# Eval("Value") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                        </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="unlockSdocYes" EventName="serverclick" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

     <div class="modal fade" id="unlockSDocConfimation" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">
                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/confirm.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="deleteSDocconfirmationmessageText" style="font-size: 17px;">Are you sure, you want to Unlock this SDocId?</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <input type="button" value="Unlock" class="btn btn-info" id="unlockSdocYes" runat="server" onserverclick="unlockSdocYes_ServerClick" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="Cancel" class="btn btn-info" id="unlockSdocNo" onclick="unlockSdocNo()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
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
        function showConfirmationForUnlockSDoc() {
            $('[id*=unlockSDocConfimation]').modal('show');
            return false;
        }
        function unlockSdocNo() {
            $('[id*=unlockSDocConfimation]').modal('hide');
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
