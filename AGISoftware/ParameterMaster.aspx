<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ParameterMaster.aspx.cs" Inherits="AGISoftware.ParameterMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <link href="Scripts/ModalDragNew/jquery-ui.min.css" rel="stylesheet" />
    <script src="Scripts/ModalDragNew/jquery-ui.min.js"></script>
    <%: Scripts.Render("~/bundles/multiselectjs") %>
    <%: Styles.Render("~/bundles/multiselectcss") %>

    <style>
        #ParameterMaster {
            /*background-color: #fd3801;*/
            /*background-color: #3c3b54;*/
            background-color: white;
        }

            #ParameterMaster a, #ParameterMaster svg {
                color: brown;
            }

        .ParameterMasterLabel {
            color: #87878a;
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
            /*margin-left: 15px;*/
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
            font-size: 18px;
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

        .CentreHeader {
            width: 100%;
            margin: auto;
            text-align: center !important;
        }

        #divInput {
            width: 70%;
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

        .multiselect-container {
            height: 300px;
            overflow: auto;
        }

        .headerFix tr:nth-child(2n+1) td:nth-child(3), .headerFix tr:nth-child(2n) td:nth-child(3) {
            position: sticky;
            left: 0px;
            z-index: 1;
            background-color: white;
        }

        .headerFix tr:first-child th:nth-child(3) {
            position: sticky;
            left: 0px;
            z-index: 6;
            background-color: #edeef5;
        }
        #gvParameterList tr:hover, #gvParameterList tr:hover td {
            background-color: #f1f3f4 !important;
        }
        input[type="checkbox"] {
            height: 18px;
            width: 18px;
            
        }
        .tooltipclass {
            background-color: #2b2a2a;
            color: white;
            border-color: #2b2a2a;
            min-width: 200px;
            
        }
        .ui-tooltip-content::after, .ui-tooltip-content::before {
                content: "";
                position: absolute;
                border-style: solid;
                display: block;
                left: 90px;
            }
         .ui-tooltip-content::before {
            bottom: -10px;
            border-color: #AAA transparent;
            border-width: 10px 10px 0;
         }
         .ui-tooltip-content::after {
            bottom: -7px;
            border-color: #2b2a2a transparent;
            border-width: 10px 10px 0;
         }
    </style>

    <asp:HiddenField ID="hdnScrollPos" ClientIDMode="Static" runat="server" />
    <div class="container-fluid">
        <div class="row" style="color: white;">
            <div class="col-sm-12 col-lg-12 col-md-12">

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                   
                <div id="divFilter">
                    <div>
                        <asp:Label runat="server" Text="Input Category" CssClass="ParameterMasterLabel" Style="vertical-align: middle; font-size: 16px; display: inline-block;" />
                        <asp:DropDownList runat="server" ID="ddlInputModule" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlInputModule_SelectedIndexChanged" Style="display: inline-block; margin: auto; width: 200px; min-width: 120px;" ToolTip="Select Input Category" />
                    </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <div>
                        <asp:Label runat="server" Text="Sub-Input Category" CssClass="ParameterMasterLabel" Style="vertical-align: middle; font-size: 16px; display: inline-block;" />
                        <asp:DropDownList runat="server" ID="ddlSubInputModule" AutoPostBack="true" OnSelectedIndexChanged="ddlSubInputModule_SelectedIndexChanged" CssClass="form-control" Style="display: inline-block; margin: auto; width: 200px; min-width: 120px;" ToolTip="Select Sub-Input Category" />
                    </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <div>
                        <asp:Label runat="server" Text="Parameter" CssClass="ParameterMasterLabel" Style="vertical-align: middle; font-size: 16px; display: inline-block;" />
                        <asp:DropDownList runat="server" ID="ddlParameter" AutoPostBack="true" CssClass="form-control" Style="display: inline-block; margin: auto; width: 200px; min-width: 120px;" ToolTip="Select Parameter" />
                    </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <div style="margin-top: 2px">
                        <asp:Button runat="server" ID="btnView" OnClick="btnView_Click" Text="View" CssClass="Btns" Style="display: inline-block;" ToolTip="View" />
                        <asp:Button runat="server" ID="Save" OnClick="Save_Click"  OnClientClick="if(!saveNewParameter()){return false};"  Text="Save" CssClass="Btns" Style="display: inline-block;" ToolTip="Save" />
                        <asp:Button runat="server" ID="btnNew" OnClick="btnNew_Click" Text="New" CssClass="Btns" Style="display: inline-block;" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" Visible="false" CssClass="Btns" Style="display: inline-block; background-color: #d88282; border-color: #d88282" />
                        <%-- <asp:LinkButton runat="server" ID="lbnReload" CssClass="glyphicon glyphicon-refresh ReloadBtn" Style="" ToolTip="Reload" />--%>
                    </div>
                </div>
                 </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlInputModule" EventName="SelectedIndexChanged" />
                           <asp:AsyncPostBackTrigger ControlID="ddlSubInputModule" EventName="SelectedIndexChanged" />
                        <asp:PostBackTrigger ControlID="btnView" />
                         <asp:PostBackTrigger ControlID="Save" />
                         <asp:PostBackTrigger ControlID="btnNew" />
                         <asp:PostBackTrigger ControlID="btnCancel" />
                    </Triggers>
                </asp:UpdatePanel>

               <%-- <asp:UpdatePanel runat="server">
                      
                    <ContentTemplate>--%>

                    
                <div id="displayContainer" style="width: 100%; overflow: auto; margin: auto; box-shadow: 2px 2px 8px 2px #efe7e7;">
                    <asp:GridView runat="server" ID="gvParameterList" CssClass="headerFix CentreHeader" Style="box-shadow: none" ClientIDMode="Static" AutoGenerateColumns="false" HeaderStyle-BackColor="#edeef5" HeaderStyle-ForeColor="White" BackColor="White" ShowFooter="false"  EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center" OnRowDeleting="gvParameterList_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Input Category">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="edtlblInputModule" Text='<%# Bind("InputModule") %>' />
                                    <asp:Label runat="server" ID="edtlblID" Visible="false" Text='<%# Bind("Id") %>' />
                                    <asp:HiddenField runat="server" ID="hdParamID" Value='<%# Bind("Id") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label runat="server" ID="lblNewInputModule" Text='<%# Bind("InputModule") %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Input Sub-Category">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="edtlblSubInputModule" ClientIDMode="Static" Text='<%# Bind("SubInputModule") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                      <asp:Label runat="server" ID="lblNewSubInput" ClientIDMode="Static"></asp:Label>
                                    <%--<asp:DropDownList runat="server" ID="ddlNewSubInput" CssClass="form-control"></asp:DropDownList>--%>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Parameter">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="edtlblParameter" Text='<%# Bind("Parameter") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txtNewParameter" onkeypress="return notAllowedNumericatEnd(this, event);" AutoCompleteType="Disabled" ClientIDMode="Static"  CssClass="form-control"></asp:TextBox>
                                    <%--data-toggle="tooltip"--%>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Entry Type">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="edtlblEntryType" Text='<%# Bind("EntryType") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label runat="server" ID="lblNewQPEntryType" Text="TextBox" />
                                    <asp:DropDownList runat="server" ID="ddlNewEntryType" ClientIDMode="Static"  style="min-width: 120px" onchange="return ddlEntrytypechange()" CssClass="form-control">
                                        <asp:ListItem>TextBox</asp:ListItem>
                                        <asp:ListItem>Drop Down</asp:ListItem>
                                        <asp:ListItem>CheckBox</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Data Type">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="edtlblDataType" Text='<%# Bind("DataType") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label runat="server" ID="lblNewQPDataType" Text="Decimal" />
                                    <%--OnSelectedIndexChanged="ddlNewDataType_SelectedIndexChanged" AutoPostBack="true"--%>
                                    <asp:DropDownList runat="server" ID="ddlNewDataType" ClientIDMode="Static" style="min-width: 150px" onchange="return ddlDatatypechange()" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hdNewDatatype" ClientIDMode="Static" />
                                </FooterTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Custom Name">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="edttxtCustomname" AutoCompleteType="Disabled" style="min-width: 200px" CssClass="form-control" Text='<%# Bind("Customname") %>' onmouseover="return showRecommendation(this);"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txtnewCustomname" AutoCompleteType="Disabled"  CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Recommendation">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="edtlblRecommendation" AutoCompleteType="Disabled"  style="min-width: 200px" CssClass="form-control" Text='<%# Bind("Reccomandation") %>' onmouseover="return showRecommendation(this);"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txtNewRecommendation" AutoCompleteType="Disabled"  style="min-width: 200px" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Upload Image">
                                <ItemTemplate>
                                    <asp:FileUpload runat="server" ID="RecommendationImage" Style="display: inline-block" />
                                    <asp:Image runat="server" ID="imgRecommendation" ClientIDMode="Static" onclick="showLargeImage(this)" Width="30px" Height="30px" ImageUrl='<%#Eval("Image") %>' />
                                    <asp:LinkButton runat="server" ClientIDMode="Static" OnClientClick="return removeImageConfirmation(this);"  CssClass="glyphicon glyphicon-remove-circle" ID="removeRecommandationImage" Style="color: #e86b6b" ToolTip="Remove Image"></asp:LinkButton>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:FileUpload runat="server" ID="newRecommendationImage" Style="display: inline-block" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="LSL">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="edttxtLSL" AutoCompleteType="Disabled" Style="text-align: right" Width="100px" CssClass="form-control decimalPt" onblur="return checkLSLUSLValue(this,'LSL','Edit')" Text='<%# Bind("LSL") %>'></asp:TextBox>

                                    <asp:Label runat="server" ID="edtlblLSL" AutoCompleteType="Disabled" CssClass="form-control" Style="text-align: right" Text='<%# Bind("LSL") %>'></asp:Label>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txtNewLSL" ClientIDMode="Static" onblur="return checkLSLUSLValue(this,'LSL','New')" AutoCompleteType="Disabled" CssClass="form-control decimalPt"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="USL">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="edttxtUSL" AutoCompleteType="Disabled" Width="100px" Style="text-align: right" CssClass="form-control decimalPt" onblur="return checkLSLUSLValue(this,'USL','Edit')" Text='<%# Bind("USL") %>'></asp:TextBox>
                                    <asp:Label runat="server" ID="edtlblUSL" CssClass="form-control" Style="text-align: right" Text='<%# Bind("USL") %>'></asp:Label>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txtNewUSL" ClientIDMode="Static" onblur="return checkLSLUSLValue(this,'USL','New')" AutoCompleteType="Disabled" CssClass="form-control decimalPt"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Enable / Disable">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="edtcbEnable" onchange="return allowForEnableorDisable(this);" Checked='<%# Bind("Enableflag") %>' />
                                    <asp:HiddenField runat="server" ID="edthdDeletable" ClientIDMode="Static" Value='<%# Bind("Deletableflag") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox runat="server" ID="chkNewEnable" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mandatory">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="edtddlMandatory"  style="min-width: 120px" CssClass="form-control">
                                        <asp:ListItem>Normal</asp:ListItem>
                                        <asp:ListItem>Mandatory</asp:ListItem>
                                        <asp:ListItem>Important</asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:HiddenField runat="server" ClientIDMode="Static" ID="edthdMandatory" Value='<%# Bind("Mandatoryflag") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList runat="server" ID="ddlNewMandatory"  style="min-width: 120px" CssClass="form-control">
                                        <asp:ListItem>Normal</asp:ListItem>
                                        <asp:ListItem>Mandatory</asp:ListItem>
                                        <asp:ListItem>Important</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Default Parameter">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="edtcbDefaultParam" Checked='<%# Bind("DefaultParam") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox runat="server" ID="chkNewDefaultParam" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dependent Parameter">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="edtcbDependentParam"  Checked='<%# Bind("Dependencyflag") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox runat="server" ID="chkNewDependentParam" ClientIDMode="Static" onclick="return false"  />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Independent Parameters">
                                <ItemTemplate>
                                    <asp:ListBox runat="server" ID="edtlbIndependentParam" onchange="checkOruncheckDepenedentParameter(this,'Edit')" SelectionMode="Multiple"></asp:ListBox> 
                                                <asp:LinkButton runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-share" ID="gotoParameterRelatioshipPage" Visible="false" OnClientClick="return gotoParameterRelationship(this);" Style="color: forestgreen" ToolTip="Go to Parameter Relationship"></asp:LinkButton>
                                    
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div id="div1">
                                    <asp:ListBox runat="server" ID="newlbIndependentParam" ClientIDMode="Static" onchange="checkOruncheckDepenedentParameter(this,'New')" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SortOrder" >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="edtlblSortOrder" Text='<%# Bind("SortOrder") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Action" >
                                <ItemTemplate>
                                       <asp:LinkButton runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-trash" CommandName="Delete" ID="lbdeleteParameter" Style="color: #e86b6b" ToolTip="Delete Parameter"></asp:LinkButton>
                                   
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:TemplateField>

                        </Columns>

                        <EditRowStyle BackColor="#dedddd" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" />

                    </asp:GridView>
                </div>
                       <%-- </ContentTemplate>
                  <Triggers>
                         
                         <asp:AsyncPostBackTrigger  ControlID="btnView"  EventName="Click"/>
                         <asp:AsyncPostBackTrigger ControlID="Save"  EventName="Click"/>
                       
                    </Triggers>

                </asp:UpdatePanel>--%>
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


    <div class="modal fade" id="removeImageConfimation" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/confirm.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="removeImageconfirmationmessageText" style="font-size: 17px;">Are you sure, you want to remove this image?</span>
                    <span id="removeParameterName"  style="visibility: hidden"></span>
                    <span id="removeParameterID" style="visibility: hidden"></span> 
                      <span id="removeRowIndex" style="visibility: hidden"></span> 
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <%--<asp:Button runat="server" Text="Close" ID="Button1" CssClass="btn btn-info" BackColor="#5D7B9D" ForeColor="white" />--%>
                    <%--  onserverclick="saveConfirmYes_ServerClick"--%>
                    <input type="button" value="Yes" class="btn btn-info" id="removeImageConfimYes" runat="server" onclick="removeImageYes()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                     <%--onserverclick="removeImageConfimYes_ServerClick"--%>
                    <input type="button" value="No" class="btn btn-info" id="removeImageConfirmNo" onclick="removeImageNo()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

     <div class="modal fade" id="removeParameterConfimation" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/confirm.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="removeparameterCnfirmationmessageText" style="font-size: 17px;">Are you sure, you want to delete this Parameter?</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <input type="button" value="Yes" class="btn btn-info" id="removeParameterYes" runat="server" onserverclick="removeParameterYes_ServerClick" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="No" class="btn btn-info" id="removePrameterConfirmNo" onclick="removeImageNo()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="showLargeImage" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 60vw; height: 80vh; padding: 5px">
            <div class="modal-content" style="border: 2px solid #5D7B9D; width: 100%; height: 100%;">
                <div class="modal-header" style="position: relative; padding: 0px; border-bottom: none">
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove" style="float: right; z-index: 5; color: #5D7B9D; font-size: 25px"></a>
                </div>
                <div class="modal-body" style="text-align: center; padding: 0px; width: 100%; height: 95%">
                    <img id="largeImage" style="height: 100%; width: 100%" />&nbsp;&nbsp;&nbsp;
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
                   
							<span id="warningmessageText" style="font-size: 17px;">Parameter Name required.</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <%--<asp:Button runat="server" Text="Close" ID="Button1" CssClass="btn btn-info" BackColor="#5D7B9D" ForeColor="white" />--%>
                    <input type="button" value="OK" class="btn btn-info" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>



    <script>
        function showRecommendation(ele) {
            $(ele).attr('title', $(ele).val());
            $(ele).tooltip({
                items: $(ele),
                tooltipClass: 'tooltipclass',
                position: {
                    my: "center bottom",
                    at: "center top-10",
                    collision: "none"
                }
            });
               $(ele).tooltip("open");
        }

        function saveNewParameter() {
            debugger;
            let gridlen = $('#gvParameterList tr').length;
            let row = $('#gvParameterList tr')[gridlen-1];
            if ($(row).find('#txtNewParameter').length) {
                let param = $(row).find('#txtNewParameter').val();
                if (param == "") {
                    openWarningModal('Parameter Name required.');
                    return false;
                }
                else {
                    return true;
                }
            } else {
              
                return true;
            }
        }

        function notAllowedNumericatEnd(val, evt) {
            debugger;
            let subInputModule = $(val).closest('tr').find("#lblNewSubInput").text();
            if (subInputModule == "Grinding Parameters") {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var pos = evt.target.selectionStart;
                if ((charCode >= 48 && charCode <= 57)) {
                    if (pos == $(val).val().length) {
                        $(val).tooltip({
                            items: $(val),
                            content: "Not allowed numeric at the end of parameter.",
                            tooltipClass: 'tooltipclass',
                            position: {
                                my: "center bottom",
                                at: "center top-10",
                                collision: "none"
                            }
                        });
                        $(val).tooltip("open");
                        setTimeout(function () {
                            if ($(val).is(':ui-tooltip')) {
                                $(val).tooltip("close");
                            }
                        }, 2000);
                        return false;
                    }
                }
                if ($(val).is(':ui-tooltip')) {
                    $(val).tooltip("close");
                }
                return true;
            }
            return true;
        }

        function removeImageConfirmation(val) {
            debugger;
            var row = val.parentNode.parentNode;
            var rowIndex =(row.rowIndex);
            let parametername = $(val).closest('tr').find("#edtlblParameter").text();
            let parameterid = $(val).closest('tr').find("#hdParamID").val();
            $("#removeParameterName").val(parametername);
            $("#removeParameterID").val(parameterid);
            $("removeRowIndex").val(rowIndex);
            openConfirmModal('Are you sure, you want to remove this image?');
            return false;
        }
        
        function openParameterRemoveConfirmModal() {
            $('[id*=removeParameterConfimation]').modal('show');
        }
        function removeImageYes() {

            debugger;
            let name = $("#removeParameterName").val();
            let id = $("#removeParameterID").val();
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ParameterMaster.aspx/removeImage") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{paramname: "' + name + '", paramid: "' + id + '"}',
                dataType: "json",
                success: function (response) {
                    debugger;
                    var dataitem = response.d;
                    if (dataitem) {

                        for (let i = 1; i < $('#gvParameterList tr').length; i++) {
                            let row = $('#gvParameterList tr')[i];
                            let param = $(row).find('#edtlblParameter').text();
                            if (name == param) {
                                $(row).find('#imgRecommendation').css('display', 'none');
                                $(row).find('#removeRecommandationImage').css('display', 'none');
                                break;
                            }
                        }
                    } else {
                        openErrorModal('Failed to delete image.')
                    }

                },
                error: function (Result) {
                    alert("Error");
                }
            });
            $("#removeParameterName").val('');
            $("#removeParameterID").val('');
        }

        function ddlEntrytypechange() {
            debugger;
            let entrytype = $("#ddlNewEntryType").val();
            let dataitem = [];
             if (entrytype == "TextBox")
             {
                 dataitem[0] = "Alpha Numeric";
                 dataitem[1] = "Date";
                 dataitem[2] = "Decimal";
                 dataitem[3] = "Integer";
                 dataitem[4] = "Varchar";
            }
            else if (entrytype == "Drop Down")
            {
                 dataitem[0] = "Integer";
                 dataitem[1] = "Varchar";
            }
            else if (entrytype == "CheckBox")
            {
                 dataitem[0] = "";
            }
            $("#ddlNewDataType").empty();
            for (let i = 0; i < dataitem.length; i++) {
                $("#ddlNewDataType").append($("<option></option>").val(dataitem[i]).html(dataitem[i]));
            }
            ddlDatatypechange();
        }
        function ddlDatatypechange() {
            let entrytype = $("#ddlNewEntryType").val();
            let datatype = $("#ddlNewDataType").val();
            if (entrytype == "TextBox" && (datatype == "Decimal" || datatype == "Integer")) {

                $("#txtNewLSL").attr('readonly', false);
                $("#txtNewUSL").attr('readonly', false);
                if ($("#txtNewLSL").val() == "NA") {
                    $("#txtNewLSL").val("");
                }
                if ($("#txtNewUSL").val() == "NA") {
                    $("#txtNewUSL").val("");
                }
                $("#chkNewDependentParam").css('display', 'block');
                document.getElementById('div1').style.display = "block";
            }
            else {
                debugger;
                document.getElementById('div1').style.display = "none";
                $("#txtNewLSL").val("NA");
                $("#txtNewLSL").attr('readonly', true);
                $("#txtNewUSL").val("NA");
                $("#txtNewUSL").attr('readonly', true);
                $("#chkNewDependentParam").css('display', 'none');
            }
            $("#hdNewDatatype").val(datatype);
           //setScrollPosition();
        }
      

        function gotoParameterRelationship(val) {
            debugger;
            let parametername = $(val).closest('tr').find("#edtlblParameter").text();
            let parameterid = $(val).closest('tr').find("#hdParamID").val();
            let param = parametername + "," + parameterid;
            //window.location.href = "ParameterDependenacyList.aspx?param=" + param;
             $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ParameterMaster.aspx/setParameterName") %>',
                contentType: "application/json; charset=utf-8",
                 crossDomain: true,
                 data: '{Param: "' + param + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    window.location.href = "ParameterDependenacyList.aspx";
                },
                error: function (Result) {
                    alert("Error");
                }
            });

            return false;
        }

        function checkOruncheckDepenedentParameter(val, mode) {
            debugger;
            if (mode == "Edit") {
                if (val.selectedOptions.length > 0) {
                    $(val).closest('tr').find("#edtcbDependentParam").prop('checked', true);
                } else {
                    $(val).closest('tr').find("#edtcbDependentParam").prop('checked', false);
                }
            }
            if (mode = "New") {
                if (val.selectedOptions.length > 0) {
                    $(val).closest('tr').find("#chkNewDependentParam").prop('checked', true);
                 } else {
                    $(val).closest('tr').find("#edtcbDependentParam").prop('checked', false);
                }
            }
        }

        function checkLSLUSLValue(val, param, mode) {
            debugger;
            if (mode == "Edit") {
                if (param == "LSL") {
                    if (parseFloat($(val).val()) > parseFloat($(val).closest('tr').find('#edttxtUSL').val())) {
                        $(val).val("");
                        $('[id*=WarningModal]').modal('show');
                        $("#warningmessageText").text('LSL value is greater than USL value');
                    }
                }
                if (param == "USL") {
                    if (parseFloat($(val).val()) < parseFloat($(val).closest('tr').find('#edttxtLSL').val())) {
                        $(val).val("");
                        $('[id*=WarningModal]').modal('show');
                        $("#warningmessageText").text('USL value is smaller than LSL value');
                    }
                }
            }
            if (mode == "New") {
                if (param == "LSL") {
                    if (parseFloat($(val).val()) > parseFloat($(val).closest('tr').find('#txtNewUSL').val())) {
                        $(val).val("");
                        $('[id*=WarningModal]').modal('show');
                        $("#warningmessageText").text('LSL value is greater than USL value');
                    }
                }
                if (param == "USL") {
                    if (parseFloat($(val).val()) < parseFloat($(val).closest('tr').find('#txtNewLSL').val())) {
                        $(val).val("");
                        $('[id*=WarningModal]').modal('show');
                        $("#warningmessageText").text('USL value is smaller than LSL value');
                    }
                }
            }
        }

        $('.modal-content').resizable({
            //alsoResize: ".modal-dialog",
            //minHeight: 150
        });
        $('.modal-dialog').draggable();

        $('#showLargeImage').on('show.bs.modal', function () {
            $(this).find('.modal-body').css({
                'max-height': '100%'
            });
        });

        function allowForEnableorDisable(val) {
            debugger;
            if ($(val).closest('td').find('#edthdDeletable').val() == "True") {
                if ($(val).closest('span').find('#edtcbEnable').prop('checked') == false) {
                    $(val).closest('span').find('#edtcbEnable').prop('checked', true);
                    $('[id*=WarningModal]').modal('show');
                    $("#warningmessageText").text('Not able to disable this parameter because this parameter is used in calculation.');
                }
            }
            return false;
        }

        function showLargeImage(image) {
            $('[id*=largeImage]').attr('src', '');
            $('[id*=showLargeImage]').modal('show');
            $('[id*=largeImage]').attr('src', $(image).attr('src'));
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
        function allowSortOrder(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function openConfirmModal(msg) {
            $('[id*=removeImageConfimation]').modal('show');
        }

        function openErrorModal(msg) {
            $('[id*=myErrorModal]').modal('show');
            $("#errormessageText").text(msg);
        };

        function openWarningModal(msg) {
            $('[id*=WarningModal]').modal('show');
            $("#warningmessageText").text(msg);
        };

        var bigDiv = document.getElementById('displayContainer');
        bigDiv.onscroll = function () {
            $('[id*=hdnScrollPos]').val(bigDiv.scrollTop);
        }
        window.onload = function () {
            $('#displayContainer').animate({ scrollTop: $('[id*=hdnScrollPos]').val() }, 10);
        }

        $(document).ready(function () {
            var wHeight = $(window).height() - (200);
            $('#displayContainer').css('height', wHeight);
            $('[id$=edtlbIndependentParam]').multiselect({
                includeSelectAllOption: true
            });
            $('[id$=newlbIndependentParam]').multiselect({
                includeSelectAllOption: true
            });

          
        });


        $(window).resize(function () {
            var Height = $(window).height() - (200);
            $('#displayContainer').css('height', Height);
        });
        function setScrollPosition() {

            //alert($('#displayContainer').height());
            window.onload = function () {
              // $('#displayContainer').animate({ scrollTop: $(document).height()}, 10);
                 $("#displayContainer").animate({ scrollTop: $("#displayContainer")[0].scrollHeight }, 1000);
                 //$('#displayContainer').scrollTop($('#displayContainer').height()); 

            }
        }

       

        function removeLimitRangeImageFun() {

            $('[id*=removeImageConfimation]').modal('show');

            return false;
        }
        function removeImageNo() {

            $('[id*=removeImageConfimation]').modal('hide');
            $("#removeParameterName").val('');
            $("#removeParameterID").val('');
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

            function showRecommendation(ele) {
                $(ele).attr('title', $(ele).val());
                $(ele).tooltip({
                    items: $(ele),
                    tooltipClass: 'tooltipclass',
                    position: {
                        my: "center bottom",
                        at: "center top-10",
                        collision: "none"
                    }
                });
                $(ele).tooltip("open");
            }

            function notAllowedNumericatEnd(val, evt) {
                debugger;
                let subInputModule = $(val).closest('tr').find("#lblNewSubInput").text();
                if (subInputModule == "Grinding Parameters") {
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    var pos = evt.target.selectionStart;
                    if ((charCode >= 48 && charCode <= 57)) {
                        if (pos == $(val).val().length) {
                            $(val).tooltip({
                                items: $(val),
                                content: "Not allowed numeric at the end of parameter.",
                                tooltipClass: 'tooltipclass',
                                position: {
                                    my: "center bottom",
                                    at: "center top-10",
                                    collision: "none"
                                }
                            });
                            $(val).tooltip("open");
                            setTimeout(function () {
                                if ($(val).is(':ui-tooltip')) {
                                    $(val).tooltip("close");
                                }
                            }, 2000);
                            return false;
                        }
                    }
                    if ($(val).is(':ui-tooltip')) {
                        $(val).tooltip("close");
                    }
                    return true;
                }
                return true;
            }

            function removeImageConfirmation(val) {
                debugger;
                var row = val.parentNode.parentNode;
                var rowIndex = (row.rowIndex);
                let parametername = $(val).closest('tr').find("#edtlblParameter").text();
                let parameterid = $(val).closest('tr').find("#hdParamID").val();
                $("#removeParameterName").val(parametername);
                $("#removeParameterID").val(parameterid);
                $("removeRowIndex").val(rowIndex);
                openConfirmModal('Are you sure, you want to remove this image?');
                return false;
            }
           
            function gotoParameterRelationship(val) {
                debugger;
                let parametername = $(val).closest('tr').find("#edtlblParameter").text();
                let parameterid = $(val).closest('tr').find("#hdParamID").val();
                let param = parametername + "," + parameterid;
                //window.location.href = "ParameterDependenacyList.aspx?param=" + param;
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ParameterMaster.aspx/setParameterName") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{Param: "' + param + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                        window.location.href = "ParameterDependenacyList.aspx";
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });

                return false;
            }

            function checkOruncheckDepenedentParameter(val, mode) {
                debugger;
                if (mode == "Edit") {
                    if (val.selectedOptions.length > 0) {
                        $(val).closest('tr').find("#edtcbDependentParam").prop('checked', true);
                    } else {
                        $(val).closest('tr').find("#edtcbDependentParam").prop('checked', false);
                    }
                }
                if (mode = "New") {
                    if (val.selectedOptions.length > 0) {
                        $(val).closest('tr').find("#chkNewDependentParam").prop('checked', true);
                    } else {
                        $(val).closest('tr').find("#edtcbDependentParam").prop('checked', false);
                    }
                }
            }


            function checkLSLUSLValue(val, param, mode) {
                debugger;
                if (mode == "Edit") {
                    if (param == "LSL") {
                        if (parseFloat($(val).val()) > parseFloat($(val).closest('tr').find('#edttxtUSL').val())) {
                            $(val).val("");
                            $('[id*=WarningModal]').modal('show');
                            $("#warningmessageText").text('LSL value is greater than USL value');
                        }
                    }
                    if (param == "USL") {
                        if (parseFloat($(val).val()) < parseFloat($(val).closest('tr').find('#edttxtLSL').val())) {
                            $(val).val("");
                            $('[id*=WarningModal]').modal('show');
                            $("#warningmessageText").text('USL value is smaller than LSL value');
                        }
                    }
                }
                if (mode == "New") {
                    if (param == "LSL") {
                        if (parseFloat($(val).val()) > parseFloat($(val).closest('tr').find('#txtNewUSL').val())) {
                            $(val).val("");
                            $('[id*=WarningModal]').modal('show');
                            $("#warningmessageText").text('LSL value is greater than USL value');
                        }
                    }
                    if (param == "USL") {
                        if (parseFloat($(val).val()) < parseFloat($(val).closest('tr').find('#txtNewLSL').val())) {
                            $(val).val("");
                            $('[id*=WarningModal]').modal('show');
                            $("#warningmessageText").text('USL value is smaller than LSL value');
                        }
                    }
                }
            }

            $('.modal-content').resizable({
                //alsoResize: ".modal-dialog",
                //minHeight: 150
            });
            $('.modal-dialog').draggable();

            $('#showLargeImage').on('show.bs.modal', function () {
                $(this).find('.modal-body').css({
                    'max-height': '100%'
                });
            });

            function allowForEnableorDisable(val) {
                debugger;
                if ($(val).closest('td').find('#edthdDeletable').val() == "True") {
                    if ($(val).closest('span').find('#edtcbEnable').prop('checked') == false) {
                        $(val).closest('span').find('#edtcbEnable').prop('checked', true);
                        $('[id*=WarningModal]').modal('show');
                        $("#warningmessageText").text('Not able to disable this parameter because this parameter is used in calculation.');
                    }
                }
                return false;
            }

            function showLargeImage(image) {
                $('[id*=largeImage]').attr('src', '');
                $('[id*=showLargeImage]').modal('show');
                $('[id*=largeImage]').attr('src', $(image).attr('src'));
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
            function allowSortOrder(evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if ((charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            function openConfirmModal(msg) {
                $('[id*=removeImageConfimation]').modal('show');
            }

            function openErrorModal(msg) {
                $('[id*=myErrorModal]').modal('show');
                $("#errormessageText").text(msg);
            };

            function openWarningModal(msg) {
                $('[id*=WarningModal]').modal('show');
                $("#warningmessageText").text(msg);
            };

            var bigDiv = document.getElementById('displayContainer');
            bigDiv.onscroll = function () {
                $('[id*=hdnScrollPos]').val(bigDiv.scrollTop);
            }
            window.onload = function () {
                $('#displayContainer').animate({ scrollTop: $('[id*=hdnScrollPos]').val() }, 10);
            }

            $(document).ready(function () {
                var wHeight = $(window).height() - (200);
                $('#displayContainer').css('height', wHeight);
                $('[id$=edtlbIndependentParam]').multiselect({
                    includeSelectAllOption: true
                });
                $('[id$=newlbIndependentParam]').multiselect({
                    includeSelectAllOption: true
                });


            });


            $(window).resize(function () {
                var Height = $(window).height() - (200);
                $('#displayContainer').css('height', Height);
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
        });
    </script>

</asp:Content>
