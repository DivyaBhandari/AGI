<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputModuleMaster.aspx.cs" Inherits="AGISoftware.InputModuleMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div id="divInputModule">
                    <div id="divHeader">
                        <label runat="server" class="HeaderText">Input Category</label>
                    </div>
                    <asp:GridView runat="server" ID="gvInputModule" CssClass="formalGrid CentreHeader" AutoGenerateColumns="false" HeaderStyle-BackColor="#2E6886" HeaderStyle-ForeColor="White" BackColor="White" AlternatingRowStyle-BackColor="#DCDCDC" ShowFooter="false" EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvInputModule_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Input Category">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hdfId" Value='<%# Eval("Id") %>' />
                                    <asp:Label runat="server" ID="lblInputModule" Text='<%# Eval("InputModule") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txtInputModule" CssClass="form-control" Style="text-align: center;width:100%;" ClientIDMode="Static" ToolTip="Enter Input Category" placeholder="Enter Input Category" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sub-Input Category">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSubInputModule" Text='<%# Eval("SubInputModule") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txtSubInputModule" CssClass="form-control" Style="text-align: center;width:100%;" ClientIDMode="Static" ToolTip="Enter Sub-Input Category" placeholder="Enter Sub-Input Category" />
                                </FooterTemplate>
                            </asp:TemplateField>
                             
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lbtnDelete" CommandName="Delete" CssClass="glyphicon glyphicon-trash DeleteBtn" ToolTip="Delete" OnClick="lbtnDelete_Click" OnClientClick="return confirm('Are you sure?')" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-sm btn-primary" Text="Save" ToolTip="Save" OnClick="btnSave_Click" OnClientClick="return checkForEmpty()" />
                                    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-sm btn-danger" Text="Cancel" ToolTip="Cancel" OnClientClick="showDiv('hide');" OnClick="btnCancel_Click" Style="" />
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                <HeaderStyle Width="20%" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2E6886" CssClass="footerGrid" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <PagerStyle BackColor="#2E6886" ForeColor="White" HorizontalAlign="Center" />
                    </asp:GridView>
                    <%--<div id="divInsertion">
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtInputModule" CssClass="form-control" Style="text-align: center;width:100%;" ClientIDMode="Static" ToolTip="Enter Input Category" placeholder="Enter Input Category" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtSubInputModule" CssClass="form-control" Style="text-align: center;width:100%;" ClientIDMode="Static" ToolTip="Enter Sub-Input Category" placeholder="Enter Sub-Input Category" />
                                </td>
                                <td style="">
                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Save" ToolTip="Save" OnClick="btnSave_Click" OnClientClick="return checkForEmpty()" />
                                    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Cancel" ToolTip="Cancel" OnClientClick="showDiv('hide');" OnClick="btnCancel_Click" Style="" />
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <div style="width: 100%; margin: auto; margin-top: 10px; display: flex; flex-flow: row wrap; justify-content: flex-end;">
                        <asp:Button runat="server" ID="btnNew" Text="New" CssClass="btn btn-primary" OnClick="btnNew_Click" ClientIDMode="Static" ToolTip="New" Style="width: 15%;" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
    <style>
        #sidebar ul li:nth-child(4) {
            /*background-color: #fd3801;*/
            background-color: white;
        }
            #sidebar ul li:nth-child(4) a {
                color: brown;
            }
        #divInputModule {
            width: 60%;
            margin-top: 50px;
            margin-left: auto;
            margin-right: auto;
        }

        #divHeader {
            width: 100%;
            text-align: center;
        }

        .HeaderText {
            font-size: 30px;
            color: white;
        }
        .CentreHeader {
            width: 100%;
            margin: auto;
            text-align: center !important;
        }
        .formalGrid{
            width:100%;
        }
        .formalGrid tr th {
            color: white;
            background-color: #2E6886;
            font-size: 16px;
            padding: 0px 3px;
            white-space: nowrap;
            text-align: center;
            width: 40%;
        }

        .formalGrid tr td {
            padding: 0px 3px;
            color: black;
            font-size: 15px;
        }
        .footerGrid{
            padding:3px 3px;
        }
         a:hover{
            text-decoration:none;
        }
        .DeleteBtn{
            color:#ff5050;
            font-size:18px;
        }
        .DeleteBtn:hover{
            color:darkred;
        }
        #divInsertion {
            width: 100%;
            margin:auto;
            background-color: #2E6886;
        }
            #divInsertion table{
                width:100%;
            }
            #divInsertion tr td {
                font-size: 12px;
                padding: 3px 3px;
                text-align: center;
                width: 40%;
            }

    </style>
    <script>
        function checkForEmpty() {
            if ($("#txtInputModule").val() == "") {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Please enter an Input Category!");
                return false;
            }
            else {
                return true;
            }
        };
        function openSuccessModal(msg) {

            $('[id*=mySuccessModal]').modal('show');
            $("#successmessageText").text(msg);
        };
        function openWarningModal(msg) {
            $('[id*=myWarningModal]').modal('show');
            $("#warningmessageText").text(msg);
        };
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            function checkForEmpty() {
                if ($("#txtInputModule").val() == "") {
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Please enter an Input Category!");
                    return false;
                }
                else {
                    return true;
                }
            };
            function openSuccessModal(msg) {

                $('[id*=mySuccessModal]').modal('show');
                $("#successmessageText").text(msg);
            };
            function openWarningModal(msg) {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text(msg);
            };
        });
    </script>
</asp:Content>
