<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OperatorDetailsMaster.aspx.cs" Inherits="AGISoftware.OperatorDetailsMaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hdnScrollPos" ClientIDMode="Static" runat="server" />
    <div class="container-fluid">
        <div class="row" style="color: white;">
            <div class="col-sm-12 col-lg-12 col-md-12">
                 <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                <asp:Button runat="server" ID="save" Text="Save" OnClick="save_Click" CssClass="Btns" Style="display: inline-block;" ToolTip="Save" />
                <asp:Button runat="server" ID="newEmp" OnClick="newEmp_Click" CssClass="Btns" Style="" Text="New" />
                <asp:Button runat="server" ID="Button1" OnClick="Button1_Click" Visible="false" CssClass="Btns" Style="background-color: #d88282; border-color: #d88282" Text="Cancel" />

               

                   
                <div id="displayContainer" style="width: 100%; overflow: auto;  margin: 10px auto 0px auto; box-shadow: 2px 2px 8px 2px #efe7e7;">
                    <asp:GridView runat="server" ID="gvEmpDetails" CssClass="headerFix CentreHeader" Style="box-shadow: none;" ClientIDMode="Static" AutoGenerateColumns="false" HeaderStyle-BackColor="#edeef5" HeaderStyle-ForeColor="White" BackColor="White" ShowFooter="false" EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center" Width="100%" OnRowDeleting="gvEmpDetails_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Employee ID">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblid" Text='<%# Eval("id") %>'></asp:Label>

                                </ItemTemplate>
                                <%-- <EditItemTemplate>
                                    <asp:Label runat="server" ID="txtid" Text='<%# Eval("id") %>'></asp:Label>
                                </EditItemTemplate>--%>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="newtxtid" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtname" AutoCompleteType="Disabled" CssClass="form-control" Text='<%# Eval("Name") %>'></asp:TextBox>

                                </ItemTemplate>
                                <%-- <EditItemTemplate>
                                     <asp:Label runat="server" ID="lblname" Text='<%# Eval("Name") %>' />
                                </EditItemTemplate>--%>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="newtxtname" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlrole">
                                        <asp:ListItem>Admin</asp:ListItem>
                                        <asp:ListItem>Master User</asp:ListItem>
                                        <asp:ListItem>Normal User</asp:ListItem>
                                        <asp:ListItem>Operator</asp:ListItem>
                                    </asp:DropDownList>

                                </ItemTemplate>
                                <%--   <EditItemTemplate>

                                     <asp:Label runat="server" ID="lblrole" Text='<%# Eval("role") %>' />
                                </EditItemTemplate>--%>
                                <FooterTemplate>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="newddlrole">
                                       <asp:ListItem>Admin</asp:ListItem>
                                        <asp:ListItem>Master User</asp:ListItem>
                                        <asp:ListItem>Normal User</asp:ListItem>
                                        <asp:ListItem>Operator</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtemail" AutoCompleteType="Disabled" CssClass="form-control" Text='<%# Eval("email") %>'></asp:TextBox>

                                </ItemTemplate>
                                <%-- <EditItemTemplate>
                                        <asp:Label runat="server" ID="lblemail" Text='<%# Eval("email") %>' />
                                </EditItemTemplate>--%>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="newtxtemail" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No.">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtmblno" AutoCompleteType="Disabled" onkeypress="return allowNumberic(event, this);" CssClass="form-control" Text='<%# Eval("mblno") %>'></asp:TextBox>

                                </ItemTemplate>
                                <%--    <EditItemTemplate>
                                     <asp:Label runat="server" ID="lblmblno" Text='<%# Eval("mblno") %>' />
                                </EditItemTemplate>--%>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="newtxtmblno" AutoCompleteType="Disabled" onkeypress="return allowNumberic(event, this);" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Password">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtpassword" TextMode="Password" Width="93%" Style="display: inline;" CssClass="form-control" Text='<%# Eval("password") %>'></asp:TextBox>
                                    <span onclick="showHidePassword(this)" style="position: absolute; right: 0px; color: #87878a; font-size: 14px"><i class="glyphicon glyphicon-eye-open"></i></span>
                                </ItemTemplate>
                                <%--   <EditItemTemplate>
                                     <asp:Label runat="server" ID="lblpassword" Text="****" />
                                </EditItemTemplate>--%>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="newtxtpassword"  TextMode="Password" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <%-- <asp:LinkButton runat="server" CssClass="glyphicon glyphicon-edit " ID="lbtnupdate" CommandName="Update" ToolTip="Update" ></asp:LinkButton>--%>

                                    <asp:LinkButton runat="server" ID="lbtnDelete" CommandName="Delete" Style="text-align: center" CssClass="glyphicon glyphicon-trash DeleteBtn" ToolTip="Delete" />
                                </ItemTemplate>
                                <%--   <EditItemTemplate>
                                    <asp:LinkButton runat="server" CssClass="glyphicon  glyphicon-plus-sign " ID="lbtnupdate" CommandName="Update" ToolTip="Update" ></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" CssClass="glyphicon glyphicon-remove " ForeColor="#d88282"  ToolTip="Cancel" />
                                </EditItemTemplate>--%>
                                <FooterTemplate>
                                 <%--   <asp:LinkButton runat="server" CssClass="glyphicon  glyphicon-plus-sign " OnClick="addNewEmp_Click" ID="addNewEmp" CommandName="New" ToolTip="New" Font-Size="18px"></asp:LinkButton>--%>
                                </FooterTemplate>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                <FooterStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>

                        <EditRowStyle BackColor="#dedddd" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <%--  <PagerStyle BackColor="#2E6886" ForeColor="White" HorizontalAlign="Center" />--%>
                    </asp:GridView>


                </div>
                         </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="save" EventName="Click" />
                         <asp:AsyncPostBackTrigger ControlID="newEmp" EventName="Click" />
                         <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                         <asp:AsyncPostBackTrigger ControlID="saveConfirmYes" EventName="serverclick" />
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
                    <input type="button" value="Yes" class="btn btn-info" id="saveConfirmYes" onserverclick="saveConfirmYes_ServerClick" runat="server" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="No" class="btn btn-info" id="saveConfirmNo" onclick="saveConfirmNo()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
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
        #gvEmpDetails tr:hover, #gvEmpDetails tr:hover td {
            background-color: #f1f3f4 !important;
        }
        .DeleteBtn {
            color: #d88282;
        }

            .DeleteBtn:hover {
                color: #d88282;
            }

        #OperatorDetailsMaster {
            /*background-color: #fd3801;*/
            /*background-color: #3c3b54;*/
            background-color: white;
        }

            #OperatorDetailsMaster a, #OperatorDetailsMaster svg {
                color: brown;
            }

        #gvEmpDetails tr:last-child td table {
            /*margin:auto;*/
        }

        #gvEmpDetails {
            border: none;
        }

            #gvEmpDetails tr:last-child td table tr td span {
                color: #87878a;
                border: 1px solid #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 16px;
            }

            #gvEmpDetails tr:last-child td table tr td a {
                color: #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 14px
            }

            #gvEmpDetails tr td:nth-child(6) {
                position: relative;
            }

            #gvEmpDetails tr td:nth-child(7) {
                text-align: center;
            }

        .headerFix {
            box-shadow: 2px 2px 8px 2px #efe7e7;
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
    </style>

    <script>
        function showHidePassword(lnk) {
            if (lnk.parentElement.firstElementChild.type == "password") {
                lnk.parentElement.firstElementChild.type = 'text';
                lnk.firstElementChild.setAttribute('class', 'glyphicon glyphicon-eye-close');
            }
            else {
                lnk.parentElement.firstElementChild.type = 'password';
                lnk.firstElementChild.setAttribute('class', 'glyphicon glyphicon-eye-open');
            }
        }

        var bigDiv = document.getElementById('displayContainer');        bigDiv.onscroll = function () {            $('[id*=hdnScrollPos]').val(bigDiv.scrollTop);        }
        window.onload = function () {            $('#displayContainer').animate({ scrollTop: $('[id*=hdnScrollPos]').val() }, 10);        }
       
        $(document).ready(function () {
            var wHeight = $(window).height() - (180);
            $('#displayContainer').css('height', wHeight);
        });
        $(window).resize(function () {
            var Height = $(window).height() - (180);
            $('#displayContainer').css('height', Height);
        });
        function openWarningModal(msg) {
            $('[id*=myWarningModal]').modal('show');
            $("#warningmessageText").text(msg);
        };
        function openConfirmModal(msg) {
            $('[id*=myConfirmationModal]').modal('show');
            $("#confirmationmessageText").text(msg);
        }
        function saveConfirmNo() {
            $('[id*=myConfirmationModal]').modal('hide');
        }
        function allowNumberic(evt, val) {
            debugger;
            var charCode = (evt.which) ? evt.which : evt.keyCode;

            if ((charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
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
        function setScrollPosition() {
            
            window.onload = function () {                $("#displayContainer").animate({ scrollTop: $("#displayContainer")[0].scrollHeight }, 1000);            }
        }
    </script>
</asp:Content>
