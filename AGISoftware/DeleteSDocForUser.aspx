<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteSDocForUser.aspx.cs" Inherits="AGISoftware.DeleteSDocForUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12 col-lg-12 col-md-12">
                <div style="display: inline; width: 50vw; text-align: center">
                    <span style="display: inline">SystemDoc for Delete</span>
                    <asp:DropDownList runat="server" ID="ddlDeleteSdoc" Style="display: inline" CssClass="form-control"></asp:DropDownList>

                    <asp:Button runat="server" ID="btnDeleteSDoc" Style="display: inline" CssClass="Btns" Text="Delete" OnClientClick="return showConfirmationForDeleteSDoc();" />
                   
                </div>
            </div>
        </div>
    </div>
     <div class="modal fade" id="deleteSDocConfimation" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">
                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/confirm.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="deleteSDocconfirmationmessageText" style="font-size: 17px;">Are you sure, you want to delete this SDocId?</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <input type="button" value="Delete" class="btn btn-info" id="deleteSdocYes" runat="server" onserverclick="deleteSdocYes_ServerClick" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="Cancel" class="btn btn-info" id="deleteSdocNo" onclick="deleteSdocNo()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <style>
        #sidebar ul li:nth-child(6) {
            /*background-color: #fd3801;*/
            /*background-color: #3c3b54;*/
            background-color: white;
        }

            #sidebar ul li:nth-child(6) a {
                /*background-color: #fd3801;*/
                /*background-color: #3c3b54;*/
                color: brown;
            }
    </style>
     <script>
        function showConfirmationForDeleteSDoc() {
            $('[id*=deleteSDocConfimation]').modal('show');
            return false;
        }
        function deleteSdocNo() {
            $('[id*=deleteSDocConfimation]').modal('hide');
        }
    </script>
</asp:Content>
