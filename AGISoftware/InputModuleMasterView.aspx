<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputModuleMasterView.aspx.cs" Inherits="AGISoftware.InputModuleMasterView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="row" style="color: white;">
            <div class="col-sm-12 col-lg-12 col-md-12">
                <div style=" overflow: auto;   margin: 4% auto; ">
                    <asp:Button runat="server" ID="btnSave" CssClass="Btns" Text="Save" OnClick="btnSave_Click" style="margin-left:20%; margin-bottom: 10px" />
                    <asp:GridView runat="server" ID="gvInputModule" ClientIDMode="Static" CssClass="formalGrid  headerFix" AutoGenerateColumns="false" HeaderStyle-BackColor="#2E6886" HeaderStyle-ForeColor="White" BackColor="White"  ShowFooter="false" EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:TemplateField HeaderText="Input Category">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hdfId" Value='<%# Eval("InputModuleID") %>' />
                                    <asp:Label runat="server" ID="lblInputModule" Text='<%# Eval("InputModule") %>' />
                                </ItemTemplate>

                            </asp:TemplateField>
                          <%--  <asp:TemplateField HeaderText="Input Sub-Category">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSubInputModule" Text='<%# Eval("SubInputModule") %>' />
                                </ItemTemplate>

                            </asp:TemplateField>--%>

                             <asp:TemplateField HeaderText="Update Name">
                                <ItemTemplate>
                                   <asp:TextBox runat="server" ID="txtRenameInputModule" AutoCompleteType="Disabled" CssClass="form-control"  Text='<%# Eval("UpdateName") %>' ></asp:TextBox>
                                </ItemTemplate>
                             
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="Sort Order">
                                <ItemTemplate>
                                   <asp:TextBox runat="server" ID="txtSortOrder" AutoCompleteType="Disabled" CssClass="form-control"  Text='<%# Eval("SortOrder") %>' ></asp:TextBox>
                                </ItemTemplate>
                             
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2E6886" CssClass="footerGrid" />
                        <HeaderStyle HorizontalAlign="Center" />

                    </asp:GridView>
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
         #gvInputModule tr:hover, #gvInputModule tr:hover td {
                background-color: #f1f3f4 !important;
            }
        #InputModuleMasterView {
            /*background-color: #fd3801;*/
            background-color: white;
        }
            #InputModuleMasterView a, #InputModuleMasterView svg {
                color: brown;
            }
        .headerFix {
            box-shadow: 2px 2px 8px 2px #efe7e7;
            border: none;
          margin:auto;
            width: 60%;
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
    </style>
    <script>

        function showpop5(msg, title) {
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-top-right",
                "preventDuplicates": true,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
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
    </script>
</asp:Content>
