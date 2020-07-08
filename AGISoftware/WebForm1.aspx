<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AGISoftware.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.3.1.js"></script>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">

        <asp:Button runat="server" ID="export" Text="Export" OnClick="export_Click" />

        <asp:GridView runat="server" ID="gv" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <i class="glyphicon glyphicon-user">Plus</i>
                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
                                <%-- <Columns>
                            <asp:BoundField ItemStyle-Width="150px" DataField="OrderId" HeaderText="Order Id" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="OrderDate" HeaderText="Date" />
                        </Columns>--%>
                            </asp:GridView>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate></ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>

        <asp:Button runat="server" ID="btn" />

        <div class="modal fade" id="deleteSDocConfimation" role="dialog" style="min-width: 300px;">
            <div class="modal-dialog modal-dialog-centered" style="width: 450px">
                <div class="modal-content" style="border: 2px solid #5D7B9D">
                   <%-- <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">
                        <h4 class="modal-title" style="color: white;"></h4>
                    </div>--%>
                    <div class="modal-body">
                      <%--  <img src="Images/confirm.png" width="40" />&nbsp;&nbsp;&nbsp;--%>
                        <h4>Select System Document for restore</h4>
                        
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddl" style="margin-top: 10px">
                            <asp:ListItem>
                                SDoc000001_06_0001_141119_10:44
                            </asp:ListItem>
                            <asp:ListItem>
                               SDoc000003_03_0001_131119_11:44
                            </asp:ListItem>
                            <asp:ListItem>
                                SDoc000003_04_0001_131119_14:11
                            </asp:ListItem>
                            <asp:ListItem>SDoc000003_05_0001_131119_14:11</asp:ListItem>
                        </asp:DropDownList>
							
                    </div>
                    <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                        <input type="button" value="OK" class="btn btn-info" id="deleteSdocYes" runat="server" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                        <input type="button" value="Cancel" class="btn btn-info" id="deleteSdocNo" onclick="deleteSdocNo()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>

      

        <script>
            $('[id*=btn]').click(function (e) {
                $("[id*=deleteSDocConfimation]").modal('show');
                e.preventDefault();
            });
            var count = 0;
            $("#parameterList").hide();
            $("#click").click(function (e) {
                if (count % 2 == 0) {

                    $("#parameterList").show();

                } else {
                    $("#parameterList").hide();
                }
                count++;
                e.preventDefault();
            });
        </script>
    </form>

</body>

</html>
