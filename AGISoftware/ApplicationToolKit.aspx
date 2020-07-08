<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ApplicationToolKit.aspx.cs" Inherits="AGISoftware.ApplicationToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--style="margin-top: 20px"  --%>
      <script src="Scripts/Resize/colResizable-1.6.js"></script>
    <script src="Scripts/Resize/colResizable-1.6.min.js"></script>
   <%-- <script src="Scripts/Jquery%201.8/jquery.min.js"></script>--%>
    <%: Scripts.Render("~/bundles/multiselectjs") %>
    <%: Styles.Render("~/bundles/multiselectcss") %>
    <script src="Scripts/jquery.blockUI.js"></script>
     <style>

         .tooltipclass {
             background-color: #2b2a2a;
             color: white;
             border-color: #2b2a2a;
             min-width: 150px;
             z-index: 3;
         }
         /*.ui-tooltip-content::after, .ui-tooltip-content::before {
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
         }*/


         .backcolorchange{
             background-color: red;
         }
        #gvDisplayData tr td:nth-child(2),#gvDisplayData tr th:nth-child(2)  {
            display:none;
        }
             #gvnested tr td:nth-child(2), #gvnested tr th:nth-child(2) {
                 display: block;
             }

        #gvDisplayData tr:hover, #gvDisplayData tr:hover td:first-child {
            /*background-color: #f1f3f4 !important;*/
        }
         #gvminmaxLarge tr:hover, #gvminmaxLarge tr:hover td {            background-color: #f1f3f4 !important;        }
        #gvMinMaxAvg tr:hover, #gvMinMaxAvg tr:hover td {
            background-color: #f1f3f4 !important;
        }

        #gvDisplayData tr:last-child:hover td {
            background-color: #edeef5 !important;
        }

        #searchIcon:hover {
            cursor: pointer;
        }

        .blockUI {
            border-radius: 20px;
        }

        .charts {
        }

        #chkMinMaxParameterList tr td {
            border-bottom: none;
            position: unset;
        }

        #chkMinMaxParameterList label {
            font-weight: unset;
        }

        .rotateImage {
            transform: rotate(180deg);
        }

        .btn-group .multiselect {
            width: 100px;
            overflow: hidden;
        }

        fieldset {
            /*border: 1px solid #2B7B78;*/
            padding: 0px;
            border-radius: 4px;
            width: auto;
            box-shadow: 2px 2px 8px 2px #efe7e7;
        }

        .masterFS {
            padding: 0 5px 0 5px;
        }

        legend {
            text-align: left;
            color: white;
            display: block;
            width: auto;
            padding: 0;
            margin-bottom: 5px;
            font-size: 21px;
            line-height: inherit;
            border-bottom: transparent;
            color: black;
        }

        #expand {
            font-size: 23px;
            right: -30px;
            text-decoration: none;
            color: #59588c;
            animation: blinkingText 2.5s infinite;
        }

        @keyframes blinkingText {
            0% {
                color: #4f4d8a;
            }

            49% {
                /*color: #9493cc;*/
                color: #0e0b6e;
            }

            50% {
                /*color: #9493cc;*/
                color: #0e0b6e;
            }
            /*99% {                color: transparent;            }*/
            100% {
                color: #4f4d8a;
            }
        }


        #expand:hover {
            cursor: pointer;
        }

        .multiselect-container {
            height: 250px;
            overflow: auto;
            position: absolute;
            top: -250px;
        }

        #lblMean {
            color: white;
            font-size: 16px;
            border: 1px solid silver;
            /*padding: 3px 8px;*/
        }

        #executeBtn {
            /*float: right;*/
        }

        .txtQueryCs {
            width: 100%;
            max-width: 100%;
            margin-top: 5px;
        }

        .active {
            /*background-color: #4b84e3;
            color: white;
            border: 1px solid #4b84e3;*/
            background-color: #3c3b54;
            color: white;
            border: 1px solid #3c3b54;
        }

        .inactive {
            border: 1px solid white;
            background-color: white;
            /*color: #4b84e3;*/
            color: #3c3b54;
        }

            .inactive:hover {
                /*background-color: #4b84e3;
                color: white;
                border: 1px solid #4b84e3;*/
                background-color: #3c3b54;
                color: white;
                border: 1px solid #3c3b54;
            }

        .atkBtns {
            /*width: 100%;*/
            white-space: normal;
            line-height: 1.5;
            margin-bottom: 3px;
            /*background-color: #579694;*/
            /*color: white;*/
            padding: 4px;
            border-radius: 5px;
            /*border: 1px solid #4b84e3;*/
            border: 1px solid #3c3b54;
            outline: none;
        }

        .atkAllAnyBtns {
            /*width: 30%;*/
            width: 20%;
            background-color: #44908d;
            color: white;
            border: 3px solid #44908d;
            border-radius: 5px;
        }

         #gvDisplayData > tbody > tr:last-child  td {
            position: sticky;
            bottom: 0px;
            text-align: center;
            background-color: #edeef5;
            z-index: 5;
            padding: 0px 6px;
        }

            #gvDisplayData tr:last-child td table {
                /*margin:auto;*/
            }

        #gvDisplayData {
            border: none;
        }

            #gvDisplayData tr:last-child td table tr td span {
                color: #87878a;
                border: 1px solid #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 16px;
            }

            #gvDisplayData tr:last-child td table tr td a {
                color: #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 14px;
            }

            #gvDisplayData tr td:first-child:hover {
                cursor: pointer;
            }

         #gvnested tr:hover, #gvnested tr:hover td {
             background-color: #f1f3f4 !important;
         }


             #gvNormalSDocbind > tbody > tr:last-child td {
                 position: sticky;
                 bottom: 0px;
                 text-align: center;
                 background-color: #edeef5;
                 z-index: 5;
                 padding: 0px 6px;
             }

            #gvNormalSDocbind tr:last-child td table {
                /*margin:auto;*/
            }

        #gvNormalSDocbind {
            border: none;
        }

            #gvNormalSDocbind tr:last-child td table tr td span {
                color: #87878a;
                border: 1px solid #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 16px;
            }

            #gvNormalSDocbind tr:last-child td table tr td a {
                color: #87878a;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 14px;
            }

            #gvNormalSDocbind tr td:first-child:hover {
                cursor: pointer;
            }
             #gvNormalSDocbind tr th {
              white-space: nowrap;
            }
         #gvNormalSDocbind tr:hover, #gvNormalSDocbind tr:hover td {
             background-color: #f1f3f4 !important;
         }

/**/

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
                /*white-space: nowrap;*/
            }

                .headerFix tr th a {
                    color: #87878a;
                }

            .headerFix tr td {
                padding: 0px 6px;
                color: #454444;
                font-size: 15px;
                z-index: unset;
                border: none;
                border-bottom: 1px solid #f3f2f5;
                /*white-space: nowrap;*/
            }

                .headerFix tr td span {
                    padding: 3px;
                    color: #454444;
                    font-size: 15px;
                }

            .headerFix tr:nth-child(2n+1) td:first-child {
                position: sticky;
                left: 0px;
                z-index: 1;
                background-color: white;
                /*background-color:#DCDCDC;*/
            }

            .headerFix tr th:first-child {
                position: sticky;
                top: -1px;
                left: -1px;
                z-index: 10;
                color: #87878a;
                border: none;
                background-color: #edeef5;
            }

            .headerFix tr:nth-child(2n) td:first-child {
                position: sticky;
                /*top: -1px;*/
                left: 0px;
                z-index: 1;
                background-color: white;
            }
/**/

        #gvMinMaxAvg tr td:first-child {
            position: sticky;
            top: -1px;
            left: 0px;
            z-index: 1;
            background-color: white;
            width: 200px;
            max-width: 200px;
            overflow: hidden;
            text-overflow: ellipsis;
            /*white-space: nowrap;*/
        }

        @media screen and (max-width: 1116px) {
            #gvMinMaxAvg tr td:first-child {
                position: sticky;
                top: -1px;
                left: 0px;
                z-index: 1;
                background-color: white;
                width: 50px;
                max-width: 50px;
                overflow: hidden;
                text-overflow: ellipsis;
                white-space: nowrap;
            }
        }

        #ApplicationToolKit {
            /*background-color: #fd3801;*/
            background-color: white;
        }

            #ApplicationToolKit a {
                color: brown;
            }

        svg {
            width: 100%;
            height: 98%;
        }

        .expand {
            position: absolute;
            /*left:200px;*/
        }

        #gvnested tr:hover, #gvnested tr:hover td{
            background-color: #f1f3f4 !important;
        }
        #gvnested tr:last-child:hover td {
            /*background-color: #edeef5 !important;*/
        }
        #gvnested tr th {
            color: #87878a;
            background-color: #edeef5;
            border: none;
            white-space: nowrap;
            padding: 0px 6px;
        }
        /*, #gvnested tr td:last-child*/
        #gvnested tr td {
            background-color: white;   
            border: none;
            border-bottom: 1px solid #f3f2f5;
            white-space: nowrap;
            padding: 0px 6px;
        }
        #gvnested tr:last-child td {
            background-color: white;
        }

         .noCrsr {
            cursor: default;
            margin-right: +5px;
        }

        .noSelect {
            -webkit-touch-callout: none;
            -webkit-user-select: none;
            -khtml-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }
        .resizeColumn th.resizing {
            cursor: col-resize;
        }
        .resizeColumn tr th{
            cursor: col-resize;
        }
        #tblminmaxparamlist tr td{
            border-bottom:none;
        }
        #MainContent_cbSdocdrillOption{
            height: 18px;
            width: 18px;
            vertical-align: sub;
            display:inline;
        }
        #chkList label{
            font-weight: unset;
        }
         input[type="checkbox"] {
             height: 18px;
             width: 18px;
             vertical-align: sub;
         }
         #ddlParameternew > option{
             color: red;
         }
        /*#ddlGraphParams1 > option{
           font-size: 23px;
           -moz-border-bottom-colors: red;
        }*/
        .ddlGraph > option{
            font-size: 20px;
            color: red;
            font-weight: unset;
        }
       
    </style>
    <div class="container-fluid">
       


        <asp:HiddenField runat="server" ID="hdnActiveBtn" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnChartsIcon" ClientIDMode="Static" Value="charts" />
        <asp:HiddenField runat="server" ID="hdnSearchToggle" ClientIDMode="Static" />
         <asp:HiddenField runat="server" ID="hdnMinMaxAvgGridClick" ClientIDMode="Static" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="panel panel-default" id="showbidGridMinMax" style="visibility: hidden; width: 80%; z-index: 30; position: absolute; top: 10px; box-shadow: 10px 10px 30px 30px #c4c4c4; border: 1px solid #3c3b54"">
                    <div class="panel-heading" style="padding: 3px; text-align: center;border-bottom: 1px solid #3c3b54; font-size: 18px; font-weight:bold">Statistics Data</div>
                    <div class="panel-body" style="height: 60vh;">
                        <div class="headerFix" style="height: 100%; overflow: auto">
                            <asp:GridView runat="server" ID="gvminmaxLarge" AutoGenerateColumns="false" ClientIDMode="Static" Width="100%" HeaderStyle-BackColor="#2E6886" BackColor="White" EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center" BorderStyle="None">
                                <Columns>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label onclick="largeminMaxParameterClick()" style="width: 100%">Item&nbsp;<i class="glyphicon glyphicon-triangle-bottom" style="padding: 2px; font-size: 10px; border: 1px solid silver"></i></label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnFlag" Value='<%# Eval("DerivedFlag")%>' />
                                            <asp:Label runat="server" ID="item" Text='<%# Eval("Parameter")%>' ToolTip='<%# Eval("Parameter")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="min" Text='<%# Eval("Min")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="max" Text='<%# Eval("Max")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Avg">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="avg" Text='<%# Eval("Avg")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Flag" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="flag" Text='<%# Eval("DerivedFlag")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="panel-footer" style="padding: 3px; text-align: center;border-top: 1px solid #3c3b54">
                        <input type="button" value="Cancel" class="Btns" onclick="showbidGridMinMaxCancel()"  />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="minMaxOk" EventName="serverclick" />
                <asp:AsyncPostBackTrigger ControlID="statistics" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="row" id="div1">
            <div id="dataSection" class="col-sm-8 col-md-8 col-lg-8" style="padding-right: 5px;">
                <div class="row">

                    <div class="col-sm-12 col-md-12 col-lg-12" style="padding-right: 5px;">

                        <div id="btnContainer">
                            <asp:Button runat="server" Text="General Information" CssClass="atkBtns active" ClientIDMode="Static" ID="systemDocBtn" OnClick="systemDocBtn_Click" />
                            <asp:Button runat="server" Text="Machine Tool" CssClass="atkBtns inactive" ClientIDMode="Static" ID="machineToolBtn" OnClick="machineToolBtn_Click" />
                            <asp:Button runat="server" Text="Consumables" CssClass="atkBtns inactive" ClientIDMode="Static" ID="wheelBtn" OnClick="wheelBtn_Click" />
                            <asp:Button runat="server" Text="Workpiece Details" CssClass="atkBtns inactive" ClientIDMode="Static" ID="workPieceBtn" OnClick="workPieceBtn_Click" />
                            <asp:Button runat="server" Text="Operational Parameters" CssClass="atkBtns inactive" ClientIDMode="Static" ID="opeParBtn" OnClick="opeParBtn_Click" />
                            <asp:Button runat="server" Text="Target Quality Parameters" CssClass="atkBtns inactive" ClientIDMode="Static" ID="targetQlyBtn" OnClick="targetQlyBtn_Click" />
                            <asp:Button runat="server" Text="All" CssClass="atkBtns inactive" ID="allInfoBtn" ClientIDMode="Static" OnClick="allInfoBtn_Click" Style="min-width: 80px" />&nbsp;
                            <asp:CheckBox runat="server" ID="cbSdocdrillOption" ClientIDMode="Static" Text="NormalBind" AutoPostBack="true" OnCheckedChanged="cbSdocdrillOption_CheckedChanged" />
                          
                        </div>
                        <asp:UpdatePanel runat="server" ID="gridUpdatePanel">
                            <ContentTemplate>
                                <%--<div class="col-sm-7 col-md-7 col-lg-7" style="padding: 0px">--%>
                                <%--class="headerFix "--%>
                                <div style="width: 100%; overflow: auto; max-height: 60vh;" class="headerFix" id="gridContainer">
                                    <asp:GridView runat="server" ID="gvDisplayData" ClientIDMode="Static" Width="100%" BackColor="White" EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center" AllowPaging="true" OnPageIndexChanging="gvDisplayData_PageIndexChanging"  OnRowDataBound="gvDisplayData_RowDataBound" PageSize="150" OnPreRender="gvDisplayData_PreRender" BorderStyle="NotSet">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SDoc ID">
                                                <ItemTemplate>
                                                   <i class="glyphicon glyphicon-chevron-right" id="plus" onclick="drillSDoc(this)"></i>
                                                    <%# Eval("SDocName") %>
                                                    <asp:GridView runat="server" Style="display:none;border:0"  ClientIDMode="Static" Width="100%"  id="gvnested"></asp:GridView>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>


                                     <asp:GridView runat="server" ID="gvNormalSDocbind" CssClass="" ClientIDMode="Static" Width="100%" BackColor="White" EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center" AllowPaging="true" OnPageIndexChanging="gvNormalSDocbind_PageIndexChanging"  PageSize="150" OnPreRender="gvNormalSDocbind_PreRender" BorderStyle="NotSet"></asp:GridView>

                                </div>
                                <span>Number of records :</span>
                                <asp:Label runat="server" ID="noOfRows"></asp:Label>
                                <%-- </div>--%>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="systemDocBtn" EventName="Click" />
                                <%--<asp:PostBackTrigger ControlID="cbSdocdrillOption"/>--%>
                                <asp:AsyncPostBackTrigger ControlID="machineToolBtn" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="wheelBtn" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="workPieceBtn" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="opeParBtn" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="targetQlyBtn" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="allInfoBtn" EventName="Click" />
                                   <asp:AsyncPostBackTrigger ControlID="cbSdocdrillOption" EventName="CheckedChanged" />
                                
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>
                <div class="row" style="margin-top: 8px;">

                    <asp:UpdatePanel runat="server" ID="ddlUpdatePanel">
                        <ContentTemplate>
                            <div class="col-sm-12 col-md-12 col-lg-12" style="padding: 0px; text-align: left; padding-left: 15px; padding-right: 5px;">
                                <%--    <span style="float: left; width: 70%; text-align: left; display: inline-flex">--%>
                                <fieldset class="masterFS">
                                    <legend>&nbsp;<b>Search Criteria by</b>&nbsp;<i onclick="searchCriteriaClick(this)" id="searchIcon" runat="server" clientidmode="static">Parameter</i></legend>
                                    <span>Field Name </span>
                                                                        <%--<asp:DropDownList runat="server" CssClass="form-control" ClientIDMode="Static" ID="ddlParameter" Style="display: inline-block; margin-bottom: 2px"></asp:DropDownList>--%>
                                     <asp:TextBox runat="server" ID="txtParameternew" Style="display: inline-block; margin-bottom: 2px" ForeColor="Black" ClientIDMode="Static"  AutoCompleteType="Disabled" list="ddlParameternew" CssClass="form-control"></asp:TextBox>
                    <datalist id="ddlParameternew" runat="server" clientidmode="static"  autopostback="true"></datalist>

                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSdocPlungeCat" ClientIDMode="Static" Style="display: none; margin-bottom: 2px;">
                                        <asp:ListItem>Sdocname</asp:ListItem>
                                        <asp:ListItem>PlungeId</asp:ListItem>
                                        <asp:ListItem>SubCategoryId</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;                                   <span style="margin-left: 10px">Field Value </span>
                                    <asp:DropDownList runat="server" ID="ddlParamValue" ClientIDMode="Static" CssClass="form-control" Style="display: inline-block; margin-bottom: 2px"></asp:DropDownList>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSdocPlungeCatValues" ClientIDMode="Static" Style="display: none; margin-bottom: 2px">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;                                        
                                    <asp:Button runat="server" ID="allBtn" Text="And" Width="70px" CssClass="Btns" OnClientClick="return andBtnClick();" />
                                    <asp:Button runat="server" ID="anyBtn" Text="Or" Width="70px" CssClass="Btns" OnClientClick="return orBtnClick();" />

                                    <asp:Button runat="server" ID="executeBtn" ClientIDMode="Static" Text="Execute" OnClientClick="return executeClick();" OnClick="executeBtn_Click" CssClass="Btns" />
                                    <asp:Button runat="server" ID="clearBtn" Text="Clear" Width="70px" CssClass="Btns" OnClick="clearBtn_Click" OnClientClick="return clearClick();" />
                                    <%--  </span>--%>

                                    <%--   <span style="float: right;">--%>
                                    <br />
                                    <span>Display Parameters</span>
                                    <asp:ListBox ID="ddlMultiDownID" runat="server" Rows="1" ClientIDMode="Static" SelectionMode="Multiple" CssClass="form-control" Style="width: 100px; display: inline-block"></asp:ListBox>

                                         <%--<asp:DropDownCheckBoxes runat="server" ClientIDMode="Static" CssClass="form-control" AddJQueryReference="true"  ID="ddlMultiDownID1" UseSelectAllNode="true" >
                                <Style DropDownBoxBoxWidth="200" SelectBoxWidth="30%" />
                            </asp:DropDownCheckBoxes>--%>

                                    <span style="margin-left: 10px">Queries</span>
                                    <asp:DropDownList runat="server" ID="ddlQueryList" CssClass="form-control" Style="display: inline; width: 10px" ClientIDMode="Static"></asp:DropDownList>
                                    &nbsp;&nbsp;
                                    <asp:Button runat="server" ID="Export" Text="Export" CssClass="Btns" OnClick="Export_Click" />


                                    <%--   </span>--%>
                                    <%--</div>--%>
                                    <%-- </div>
                    <div class="col-sm-4 col-md-4 col-lg-4" style="padding: 0px">--%>

                                    <asp:TextBox TextMode="MultiLine" runat="server" spellcheck="false" ID="txtQuery" CssClass="txtQueryCs" Height="70px" ClientIDMode="Static"></asp:TextBox>
                                </fieldset>
                                <%-- <asp:Button runat="server" ID="executeBtn" ClientIDMode="Static" Text="Execute" Width="20%" OnClick="executeBtn_Click" CssClass="atkAllAnyBtns" />--%>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="systemDocBtn" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="machineToolBtn" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="wheelBtn" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="workPieceBtn" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="opeParBtn" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="targetQlyBtn" EventName="Click" />
                            <asp:PostBackTrigger ControlID="Export" />
                            <asp:AsyncPostBackTrigger ControlID="allInfoBtn" EventName="Click" />
                               <%--<asp:PostBackTrigger ControlID="cbSdocdrillOption"/>--%>
                                  <asp:AsyncPostBackTrigger ControlID="cbSdocdrillOption" EventName="CheckedChanged" />
                            <%--  <asp:AsyncPostBackTrigger ControlID="Export" EventName="Click" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                    <%--   <div class="col-sm-4 col-md-4 col-lg-4">
                  <div id="graphContainer" style="position:relative">
                   
                    <div style="height:300px; border: 1px solid red" id="gcontainer"></div>
                </div>
            </div>--%>
                </div>

            </div>
            <div id="graghSection" class="col-sm-4 col-md-4 col-lg-4" style="padding-right: 5px">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div style="" id="graphDiv1">
                            <asp:Button runat="server" ID="statistics" Text="Statistics" Visible="false" CssClass="Btns" OnClick="statistics_Click" />
                           
                        <asp:TextBox runat="server" ID="txtSdocID" CssClass="form-control" AutoCompleteType="Disabled" ClientIDMode="Static" placeholder="SDoc ID" Style="display: inline; color: black" Width="310px"></asp:TextBox>

                            <asp:Button runat="server" ID="loadSdoc" Text="Load SDoc" CssClass="Btns" OnClientClick="return LoadSDoc();" />
                             <input type="button" id="showGridBtn" class="Btns" onclick="showGridBtnClick();" value="View" />
                            <div style="overflow: auto; width: 100%; max-height: 40vh;" class="headerFix" id="MinMaxContainer">

                                <asp:GridView runat="server" ID="gvMinMaxAvg" CssClass="resizeColumn" AutoGenerateColumns="false" ClientIDMode="Static" Width="150%" HeaderStyle-BackColor="#2E6886" BackColor="White" OnSorting="gvMinMaxAvg_Sorting" OnRowCreated="gvMinMaxAvg_RowCreated" CurrentSortField="Min" CurrentSortDirection="ASC" AllowSorting="True" EmptyDataText="No Data Found" EmptyDataRowStyle-BackColor="Silver" EmptyDataRowStyle-Font-Size="20px" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center" BorderStyle="None">
                                    <Columns>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <label onclick="minMaxParameterClick()" style="width: 100%">Item&nbsp;<i class="glyphicon glyphicon-triangle-bottom" style="padding: 2px; font-size: 10px; border: 1px solid silver"></i></label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdnFlag" Value='<%# Eval("DerivedFlag")%>' />
                                                <asp:Label runat="server" ID="item" Text='<%# Eval("Parameter")%>' ToolTip='<%# Eval("Parameter")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Min" SortExpression="Min">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="min" Text='<%# Eval("Min")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Max" SortExpression="Max">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="max" Text='<%# Eval("Max")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Avg" SortExpression="Avg">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="avg" Text='<%# Eval("Avg")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                               
                                <div class="panel panel-default" id="parameterList" style="width:96%; visibility: hidden; z-index: 30; position: absolute; top: 66px; box-shadow: 2px 2px 8px 2px #efe7e7;border: 1px solid black">
                                    <div class="panel-heading" style="padding: 3px; text-align: center;font-weight:bold;border-bottom: 1px solid black;border-left: 1px solid black ">Filter by Parameters</div>
                                    <div class="panel-body" style="height: 300px; overflow: auto">

                                     <%--   <asp:DropDownList runat="server" ID="ddlMinMaxInputModule" CssClass="form-control" ClientIDMode="Static" ></asp:DropDownList>--%>
                                          <asp:TextBox runat="server" ID="txtsearch" ClientIDMode="Static" CssClass="form-control" onkeyup="search(this,event);"></asp:TextBox>

                                              <asp:ListView runat="server" ID="lvMinMaxParameterList" ClientIDMode="Static" ItemPlaceholderID="placeHolderList">
                                            <LayoutTemplate>
                                                <table id="tblminmaxparamlist">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="cbselectAllInputModule" onchange="selectAllInputModule(this)" Text="Select All" />
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder runat="server" ID="placeHolderList" />
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>

                                                    <td>
                                                        <asp:CheckBox runat="server" ID="cbSelectallInput" onchange="selectAllInputParameter(this)" />
                                                        <%--<asp:Label runat="server" ID="lblInput" ForeColor="Red" Text='<%# Eval("InpputModule") %>'></asp:Label>--%>
                                                         <asp:Label runat="server" ID="lblInput" ForeColor="white" style="border: 1px solid #3c3b54; background-color:#3c3b54; border-radius: 5px; padding:0px 4px" BorderStyle="Double" Text='<%# Eval("InpputModule") %>'></asp:Label>
                                                        <asp:CheckBoxList runat="server" ID="chkList" onchange="checkoruncheckInputModule(this)" ClientIDMode="Static" DataSource='<%# Eval("Values") %>' DataTextField="CustomName" DataValueField="ColumnName"></asp:CheckBoxList>
                                                    </td>
                                                </tr>


                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div class="panel-footer" style="padding: 3px; text-align: center;border-top: 1px solid black;border-left: 1px solid black">
                                        <input type="button" value="Ok" class="btn" id="minMaxOk" runat="server" onserverclick="minMaxOk_ServerClick" style="font-size: 15px; background-color: white; color: black; border: 1px solid black; padding: 0px 15px;" />
                                        <input type="button" value="Cancel" class="btn" onclick="parameterListCancel()" style="font-size: 15px; background-color: white; color: black; border: 1px solid black; padding: 0px 15px;" />
                                    </div>
                                </div>
                                        
                                 
                            </div>

                            <%--<div style="box-shadow:2px 2px 8px 2px #e2dbdb;">--%>

                            <%--   <span style="margin-top: 15px;" >--%>


                           <%-- <asp:DropDownList runat="server" ID="ddlGraphParam" CssClass="form-control" ClientIDMode="Static" Style="display: inline; margin-top: 15px;" Width="60%"></asp:DropDownList>--%>                                                         <asp:TextBox runat="server" ID="txtGraphParams1" Style="display: inline-block; font-size: 15px " ForeColor="Black" Width="50%" ClientIDMode="Static"  AutoCompleteType="Disabled" list="ddlGraphParams1" CssClass="form-control"></asp:TextBox>
                    <datalist id="ddlGraphParams1" runat="server" clientidmode="static" autopostback="true"></datalist>                            &nbsp;                <span style="display: inline-block;"><span style="color: #87878a; font-size: 16px">Mean:</span>
                                <asp:Label runat="server" CssClass="" ID="lblMean" Style="color: #87878a; border: none; vertical-align: middle" ClientIDMode="Static"></asp:Label></span>                       <%--  <span style="color: white">SDoc ID</span>--%>

                            <a class="glyphicon glyphicon-align-right charts" id="expand" onclick="expandClick()" style="margin-top: 15px; vertical-align: text-bottom; transform: rotate(90deg);"></a>
                            <%--   </span>--%>
                            <div style="margin-top: 5px; background-color: white" id="gcontainer"></div>
                        </div>

                    </ContentTemplate>
                   
                </asp:UpdatePanel>

                <%-- </div>--%>
            </div>
        </div>
    </div>
    <div style="position: absolute; top: 0px; display: none; width: 100%" id="atkGraphs">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div>
                    <div style="width: 50%; float: left; box-shadow: 2px 2px 8px 2px #efe7e7;">
                        <div style="width: 100%; margin: 5px;">
                            <%--<asp:DropDownList runat="server" ID="ddlGraphParam2" CssClass="form-control" ClientIDMode="Static" Style="display: inline;" Width="50%"></asp:DropDownList>--%>
                            <asp:TextBox runat="server" ID="txtGraphParams2" Style="display: inline-block; font-size: 15px" ForeColor="Black" Width="50%" ClientIDMode="Static"  AutoCompleteType="Disabled" list="ddlGraphParams2" CssClass="form-control"></asp:TextBox>
                    <datalist id="ddlGraphParams2" runat="server" clientidmode="static"  autopostback="true"></datalist>
                            &nbsp;
                            <span style="display: inline-block;"><span style="color: #87878a; font-size: 16px">Mean:</span>
                                <asp:Label runat="server" CssClass="" ID="lblMean2" Style="color: #87878a; vertical-align: middle; border: none" ClientIDMode="Static"></asp:Label></span>
                        </div>
                        <div id="graph2" class="graphDiv"></div>
                    </div>
                    <div style="width: 47%; float: right; box-shadow: 2px 2px 8px 2px #efe7e7; margin-left: 2px">
                        <div style="width: 100%; margin: 5px;">
                            <%--<asp:DropDownList runat="server" ID="ddlGraphParam3" CssClass="form-control" ClientIDMode="Static" Style="display: inline;" Width="50%"></asp:DropDownList>--%>
                               <asp:TextBox runat="server" ID="txtGraphParams3" Style="display: inline-block;font-size: 15px " ForeColor="Black" Width="50%" ClientIDMode="Static"  AutoCompleteType="Disabled" list="ddlGraphParams3" CssClass="form-control"></asp:TextBox>
                    <datalist id="ddlGraphParams3" runat="server" clientidmode="static"  autopostback="true"></datalist>
                            &nbsp;
                            <span style="display: inline-block;"><span style="color: #87878a; font-size: 16px">Mean: </span>
                                <asp:Label runat="server" CssClass="" ID="lblMean3" Style="color: #87878a; vertical-align: middle; border: none" ClientIDMode="Static"></asp:Label></span>
                        </div>
                        <div id="graph3" class="graphDiv"></div>
                    </div>
                </div>
                <br />
                <%--    <div style="margin-top:10px;">--%>
                <div style="width: 50%; float: left; box-shadow: 2px 2px 8px 2px #efe7e7; margin-top: 10px;">
                    <div style="width: 100%; margin: 5px;">
                        <%--<asp:DropDownList runat="server" ID="ddlGraphParam4" CssClass="form-control" ClientIDMode="Static" Style="display: inline;" Width="50%"></asp:DropDownList>--%>
                        <asp:TextBox runat="server" ID="txtGraphParams4" Style="display: inline-block;font-size: 15px " ForeColor="Black" Width="50%" ClientIDMode="Static"  AutoCompleteType="Disabled" list="ddlGraphParams4" CssClass="form-control"></asp:TextBox>
                    <datalist id="ddlGraphParams4" runat="server" clientidmode="static"  autopostback="true"></datalist>
                        &nbsp;
                        <span style="display: inline-block;"><span style="color: #87878a; font-size: 16px">Mean:</span>
                            <asp:Label runat="server" CssClass="" ID="lblMean4" Style="color: #87878a; vertical-align: middle; border: none" ClientIDMode="Static"></asp:Label></span>
                    </div>
                    <div id="graph4" class="graphDiv" style="width: 100%"></div>
                </div>
                <div style="width: 47%; float: right; box-shadow: 2px 2px 8px 2px #efe7e7; margin-top: 10px; margin-left: 2px">
                    <div style="width: 100%; margin: 5px;">
                       <%-- <asp:DropDownList runat="server" ID="ddlGraphParam5" CssClass="form-control" ClientIDMode="Static" Style="display: inline;" Width="50%"></asp:DropDownList>--%>
                         <asp:TextBox runat="server" ID="txtGraphParams5" Style="display: inline-block;font-size: 15px " ForeColor="Black" Width="50%" ClientIDMode="Static"  AutoCompleteType="Disabled" list="ddlGraphParams5" CssClass="form-control"></asp:TextBox>
                    <datalist id="ddlGraphParams5" runat="server" clientidmode="static"  autopostback="true"></datalist>
                        &nbsp;
                        <span style="display: inline-block;"><span style="color: #87878a; font-size: 16px">Mean:</span>
                            <asp:Label runat="server" CssClass="" ID="lblMean5" Style="color: #87878a; vertical-align: middle; border: none" ClientIDMode="Static"></asp:Label></span>
                    </div>
                    <div id="graph5" class="graphDiv"></div>
                    <%-- </div>--%>
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
                    <input type="button" value="OK" class="btn btn-info" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>
  <%--  <div class="panel panel-default" id="myWarningModal" style="visibility: hidden; position: absolute; top: 10%; left: 38%; box-shadow: 10px 10px 30px 30px #c4c4c4; z-index: 15; border: 2px solid #5D7B9D">
        <div class="panel-heading" style="padding: 8px; text-align: left; font-weight: bold; background-color: #5D7B9D; color: white">Warning!</div>
        <div class="panel-body" style="width: 380px; height: 100px; overflow: auto; padding: 10px 5px 5px 5px">
           <img src="Images/warnig.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="warningmessageText" style="font-size: 17px;">This Line ID exist in Station Information Table. Do u want to delete?</span>
        </div>
        <div class="panel-footer" style="padding: 3px; text-align: right; border-top: 1px solid #5D7B9D;">
          <input type="button" value="OK" class="btn btn-info" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
        </div>
    </div>--%>

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

   
    <script>
         var pressed = false;        var start = undefined;        var startX, startWidth;        $(".resizeColumn th").mousedown(function (e) {            start = $(this);            pressed = true;            startX = e.pageX;            startWidth = $(this).width();            $(start).addClass("resizing");            $(start).addClass("noSelect");        });        $(document).mousemove(function (e) {            if (pressed) {                $(start).width(startWidth + (e.pageX - startX));            }        });        $(document).mouseup(function () {            if (pressed) {                $(start).removeClass("resizing");                $(start).removeClass("noSelect");                pressed = false;            }        });

        function loadingFun() {
            $.blockUI({ message: '<img src="Images/ajax-loader (1).gif"  style="display: inline-block;vertical-align: text-bottom"/>&nbsp;&nbsp;<h2 style="display: inline-block;color:#524b4b;margin:20px 0px;">Just a moment...</h2>' });
            setTimeout($.unblockUI, 3000);
        }

        function selectAllInputModule(val) {
            debugger;
            $("[id*=txtsearch]").val("");
            let inputcheckbox = $("#tblminmaxparamlist  input");
            let td = $("#tblminmaxparamlist  td");
            if ($(val).children()[0].checked) {
                for (let i = 0; i < inputcheckbox.length; i++) {
                    td[i].style.display = "block";
                    inputcheckbox[i].checked = true;
                }
            } else {
                for (let i = 0; i < inputcheckbox.length; i++) {
                    td[i].style.display = "block";
                    inputcheckbox[i].checked = false;
                }
            }
        }

        function checkoruncheckInputModule(val) {
            debugger;
            let inputchekbox = $(val).closest('td').find('#chkList input');
            let count = 0;
            for (let i = 0; i < inputchekbox.length; i++) {
                if (inputchekbox[i].checked) {
                    count++;
                }
            }
            if (count == inputchekbox.length) {
                $(val).closest('td').children()[0].children[0].checked = true;
            } else {
                $(val).closest('td').children()[0].children[0].checked = false;
            }

            count = 0;
            for (let i = 0; i < $("#tblminmaxparamlist #cbSelectallInput").length; i++) {
                if ($("#tblminmaxparamlist #cbSelectallInput")[i].checked) {
                    count++;
                }
            }
            if (count == $("#tblminmaxparamlist #cbSelectallInput").length) {
                $("#cbselectAllInputModule").prop('checked', true);
            } else {
                $("#cbselectAllInputModule").prop('checked', false);
            }
           
        }

        function selectAllInputParameter(val) {
            debugger;
            let inputcheckbox = $(val).closest('td').find('#chkList input');
            let label = $(val).closest('td').find('#chkList label');

            if ($(val).children()[0].checked) {
                for (let i = 0; i < inputcheckbox.length; i++) {
                    if ($(val).closest('td').find('#chkList td')[i].style.display == "block" || $(val).closest('td').find('#chkList td')[i].style.display == "") {
                        inputcheckbox[i].checked = true;
                    }
                }
            } else {
                for (let i = 0; i < inputcheckbox.length; i++) {
                    inputcheckbox[i].checked = false;
                }
            }

            let count = 0;
            for (let i = 0; i < $("#tblminmaxparamlist #cbSelectallInput").length; i++) {
                if ($("#tblminmaxparamlist #cbSelectallInput")[i].checked) {
                    count++;
                }
            }
            if (count == $("#tblminmaxparamlist #cbSelectallInput").length) {
                $("#cbselectAllInputModule").prop('checked', true);
            } else {
                $("#cbselectAllInputModule").prop('checked', false);
            }
        }

        function search(val, evt) {
            let txtSearch = $(val).val();
            debugger;
            let input = $("#tblminmaxparamlist #chkList input");
            let label = $("#tblminmaxparamlist #chkList label");
            let td = $("#tblminmaxparamlist #chkList td");
            for (var i = 0; i < input.length; i++) {
                var chkvalue = label[i].innerText.slice(0, txtSearch.length);
                if (chkvalue.toLowerCase() == txtSearch.toLowerCase()) {
                    td[i].style.display = "block";
                } else {
                    td[i].style.display = "none";
                }
            }
        }
        function lvminmaxActivation() {
            debugger;
          
            let input = $("#tblminmaxparamlist #chkList input");
            let label = $("#tblminmaxparamlist #chkList label");
            
            for (var i = 0; i < input.length; i++) {
                var chkvalue = input[i].value.split(',')[1];
                if (chkvalue == "0") {
                    label[i].style.color = "black";
                } else {
                
                    label[i].style.color = "#6f1616";
                }
            }
        }

        function drillSDoc(val) {
            debugger;

            if ($(val).hasClass('plus')) {
                $(val).removeClass('plus');
                $(val).removeClass('glyphicon-chevron-down');
                $(val).addClass('glyphicon-chevron-right'); 
                $(val).parent().children()[1].children[0].style.display = 'none';
            } else {
                $(val).addClass('plus');
                $(val).addClass('glyphicon-chevron-down');
                $(val).removeClass('glyphicon-chevron-right');
                $(val).parent().children()[1].children[0].style.display = 'block';
            }
        }
             
        $("[id*=ddlMinMaxInputModule]").change(function () {
          
            var inputModule = $(this).val();
            let inputModuleParameterCount = 0;
             let inputModuleParameterCheckedCount = 0;
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ApplicationToolKit.aspx/setInputModuletoSession") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{inputModule:"' + inputModule + '"}',
                dataType: "json",
                success: function (response) {

                    var dataitem = response.d;
                },
                error: function (Result) {
                
                }
            });
            debugger;
             checkGridParametertoPanel();
            if (inputModule == "All") {
                for (var i = 1; i < $('#chkMinMaxParameterList tr').length; i++) {
                    var tr = $('#chkMinMaxParameterList tr').children()[i];
                    var input = $("#chkMinMaxParameterList tr input");
                    tr.style.display = "block";
                    inputModuleParameterCount++;
                    if (input[i].checked == true) {
                        inputModuleParameterCheckedCount++;
                    }
                }
            } else {
                for (var i = 1; i < $('#chkMinMaxParameterList tr').length; i++) {
                    var tr = $('#chkMinMaxParameterList tr').children()[i];
                    var input = $("#chkMinMaxParameterList tr input");
                    //var inputModuleName = tr.closest('td').children[0].value.split(',')[1];
                    var inputModuleName = input[i].value.split(',')[1];
                    if (inputModuleName == inputModule) {
                        tr.style.display = "block";
                        inputModuleParameterCount++;
                        if (input[i].checked == true) {
                            inputModuleParameterCheckedCount++;
                        }

                    } else {
                        tr.style.display = "none";
                           input[i].checked = false;
                    }
                }
            }
           
          
            if (inputModuleParameterCount == inputModuleParameterCheckedCount) {
             
                input[0].checked = true;
            } else {
              
                input[0].checked = false;
            }
          
           
        });
       

        function checkGridParametertoPanel() {
            debugger;
            let CHKid = $("#chkMinMaxParameterList tr");
            let checkboxItem = $("#chkMinMaxParameterList tr input");
            let gridparameters = $("#gvMinMaxAvg tr td:first-Child span");
            for (let i = 0; i < checkboxItem.length; i++) {
                let parameterName = checkboxItem[i].value.split(',')[0];
                for (let j = 0; j < gridparameters.length; j++) {
                    let gridparametrName = gridparameters[j].textContent;
                    if (gridparametrName == parameterName) {
                        checkboxItem[i].checked = true;
                        console.log("Equal" + gridparametrName + " , " + parameterName);
                    } else {
                        //checkboxItem[i].checked = false;
                           console.log("not Equal"+ gridparametrName + " , "+ parameterName);
                    }
                }
            }
        }




        $(document).ready(function () {
            //var wHeight = $(window).height() - 340;
            var wHeight = $(window).height() - ($('#btnContainer').height() + 160 + 220);
            $('#gridContainer').css('height', wHeight);
            console.log("H =" + wHeight);
            var minmaxHeight = (($(window).height() - 180) / 2);
            $('#MinMaxContainer').css('height', minmaxHeight);
            $('#gcontainer').css('height', minmaxHeight - 50);
            $('[id$=ddlMultiDownID]').multiselect({                includeSelectAllOption: true            });
            $('#atkGraphs').css('width', $(window).width() - $('#graphDiv1').width() - $('#sidebar').width() - 135);
            //   alert($(window).width() - $('#graphDiv1').width() - $('#sidebar').width() - 200);
            $('.graphDiv').css('height', minmaxHeight - 40);
            bindGraph();
            bindGraph2();
            bindGraph3();
            bindGraph4();
            bindGraph5();
            $('#gcontainer').children()[0].style.width = "100%";
            $('#gcontainer').children()[0].style.height = "100%";
            $('#gcontainer').children()[0].style.overflow = 'auto';
            $('#graph2').children()[0].style.width = "100%";
            $('#graph2').children()[0].style.overflow = 'auto';
            $('#graph3').children()[0].style.width = "100%";
            $('#graph3').children()[0].style.overflow = 'auto';
            $('#graph4').children()[0].style.width = "100%";
            $('#graph4').children()[0].style.overflow = 'auto';
            $('#graph5').children()[0].style.width = "100%";
            $('#graph5').children()[0].style.overflow = 'auto';

            $('#ddlQueryList option').each(function () {                var optionText = this.text;                var newOption = optionText.substring(0, 100);                if (newOption.length > 99) {                    $(this).text(newOption + '...');                }                else {                    $(this).text(newOption);                }            });
            bindSdocPlungeCatValue();
            bindParameterValue();
            minMaxAvgGridActivation();
        });
         function showGridBtnClick() {
            document.getElementById("showbidGridMinMax").style.visibility = "visible";
            $('#hdnMinMaxAvgGridClick').val("largegrid");
            // $('#parameterList').css('height', minmaxHeight - 50);
        }
        function showbidGridMinMaxCancel() {
            document.getElementById("showbidGridMinMax").style.visibility = "hidden";
        }
        function largeminMaxParameterClick() {
            document.getElementById("parameterList").style.visibility = "visible";
            $('#parameterList').css('top', '153px');
            $('#parameterList').css('left', '-180%');
        }

        $('#txtParameternew').change(function () {
            bindParameterValue();
        });
        function minMaxAvgGridActivation() {
            var ddlSelectedText = [];
            var ddlSelectedText1 = "";
            if ($("#txtGraphParams1").val() != "") {
                ddlSelectedText.push($("#txtGraphParams1").val());
                ddlSelectedText1 += $("#txtGraphParams1").val().trim();
            }
            if ($("#txtGraphParams2").val() != "") {
                ddlSelectedText.push($("#txtGraphParams2").val());
                ddlSelectedText1 += $("#txtGraphParams2").val().trim();
            }
            if ($("#txtGraphParams3").val() != "") {
                ddlSelectedText.push($("#txtGraphParams3").val());
                ddlSelectedText1 += $("#txtGraphParams3").val().trim();
            }
            if ($("#txtGraphParams4").val() != "") {
                ddlSelectedText.push($("#txtGraphParams4").val());
                ddlSelectedText1 += $("#txtGraphParams4").val().trim();
            }
            if ($("#txtGraphParams5").val() != "") {
                ddlSelectedText.push($("#txtGraphParams5").val());
                ddlSelectedText1 += $("#txtGraphParams5").val().trim();
            }

           						 var gridTbl = document.getElementById("gvMinMaxAvg");
            var gridTblLarge = document.getElementById("gvminmaxLarge");
            if (gridTbl != null) {
                for (var j = 1; j < gridTbl.rows.length; j++) {
                    var gridTblRow = gridTbl.rows[j];
                    var largeGridTblRow = gridTblLarge.rows[j];
                    if (gridTblRow.cells.length > 0) {

                        if (ddlSelectedText1.includes(gridTblRow.firstElementChild.textContent.trim())) {
                            gridTblRow.firstElementChild.style.backgroundColor = "#dadaec";
                            gridTblRow.cells[1].style.backgroundColor = "#dadaec";
                            gridTblRow.cells[2].style.backgroundColor = "#dadaec";
                            gridTblRow.cells[3].style.backgroundColor = "#dadaec";

                            largeGridTblRow.firstElementChild.style.backgroundColor = "#dadaec";
                            largeGridTblRow.cells[1].style.backgroundColor = "#dadaec";
                            largeGridTblRow.cells[2].style.backgroundColor = "#dadaec";
                            largeGridTblRow.cells[3].style.backgroundColor = "#dadaec";
                            //debugger;
                            // gridTblRow.firstElementChild.classList.add('gridCs');

                        }
                        else {
                            gridTblRow.firstElementChild.style.backgroundColor = "white";
                            gridTblRow.cells[1].style.backgroundColor = "white";
                            gridTblRow.cells[2].style.backgroundColor = "white";
                            gridTblRow.cells[3].style.backgroundColor = "white";
                            //  gridTblRow.firstElementChild.classList.add('gridCsRev');

                            largeGridTblRow.firstElementChild.style.backgroundColor = "white";
                            largeGridTblRow.cells[1].style.backgroundColor = "white";
                            largeGridTblRow.cells[2].style.backgroundColor = "white";
                            largeGridTblRow.cells[3].style.backgroundColor = "white";

                        }
                        if (gridTblRow.cells[0].firstElementChild.value.trim() == "1") {
                            gridTblRow.cells[0].children[1].style.color = "#622f2f";
                            largeGridTblRow.cells[0].children[1].style.color = "#622f2f";
                        }
                        else {
                            gridTblRow.cells[0].children[1].style.color = "black";
                            largeGridTblRow.cells[0].children[1].style.color = "black";
                        }
                    }
                }
            }
            var chkMultiSelect = document.getElementById("chkMinMaxParameterList");
            if (chkMultiSelect != null) {
                var chkbox = chkMultiSelect.getElementsByTagName('input');
                var lbl = chkMultiSelect.getElementsByTagName('label');
                for (var i = 1; i < chkbox.length; i++) {
                    var derivedFlag = chkbox[i].value.split(',')[2];
                    if (derivedFlag.trim() == "1") {
                        lbl[i].style.color = "#6f1616";
                    }
                }
            }
        }
        function bindParameterValue() {
           
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ApplicationToolKit.aspx/bindParameterValues") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{parameter:"' + $('[id*=txtParameternew]').val().toString() + '",txtqueryvalue:"' + $('[id*=txtQuery]').val() + '"}',
                dataType: "json",
                success: function (response) {

                    var dataitem = response.d;
                    var list = "";
                    for (var i = 0; i < dataitem.length; i++) {
                        list += '<option value="' + dataitem[i] + '">' + dataitem[i] + '</option>';
                    }
                    $('[id*=ddlParamValue]').empty();
                    $('[id*=ddlParamValue]').append(list);
                },
                error: function (Result) {
                    // alert("Error 1");
                }
            });
        }
        function searchCriteriaClick(lnk) {
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ApplicationToolKit.aspx/setSessionAPKDisplayCondition") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                dataType: "json",
                success: function (response) {

                    var dataitem = response.d;
                },
                error: function (Result) {
                    // alert("Error 2");
                }
            });
            $('[id*=txtQuery]').val("select * from SystemDocTransaction");
            if (lnk.textContent == "Parameter") {
                lnk.textContent = "System Doc";
                document.getElementById('ddlSdocPlungeCat').style.display = 'inline-block';
                document.getElementById('ddlSdocPlungeCatValues').style.display = 'inline-block';
                document.getElementById('txtParameternew').style.display = 'none';
                document.getElementById('ddlParamValue').style.display = 'none';
                bindSdocPlungeCatValue();
            }
            else {
                lnk.textContent = "Parameter";
                document.getElementById('ddlSdocPlungeCat').style.display = 'none';
                document.getElementById('ddlSdocPlungeCatValues').style.display = 'none';
                document.getElementById('txtParameternew').style.display = 'inline-block';
                document.getElementById('ddlParamValue').style.display = 'inline-block';
                bindParameterValue();
            }
            //$('[id*=txtQuery]').val("select * from SystemDocTransaction");
        }
        $('#ddlSdocPlungeCat').change(function () {
            bindSdocPlungeCatValue();
        });
        function bindSdocPlungeCatValue() {
            console.log("bindSdocPlungeCatValue");
          
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ApplicationToolKit.aspx/bindSdocPlungeCatValues") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{parameter:"' + $('[id*=ddlSdocPlungeCat]').val() + '",txtqueryvalue:"' + $('[id*=txtQuery]').val() + '"}',
                dataType: "json",
                success: function (response) {

                    var dataitem = response.d;
                    var list = "";
                    for (var i = 0; i < dataitem.length; i++) {
                        list += '<option value="' + dataitem[i] + '">' + dataitem[i] + '</option>';
                    }
                    $('[id*=ddlSdocPlungeCatValues]').empty();
                    $('[id*=ddlSdocPlungeCatValues]').append(list);
                },
                error: function (Result) {
                    // alert("Error 3");
                }
            });
        }
        function orBtnClick() {
            if ($('[id*=txtQuery]').val() == "") {
                openErrorModal('Enter query.');
                return false;
            }
            var query = $('[id*=txtQuery]').val();
            var ddlpram;            var ddlpramvalue;            if ($('#searchIcon').text() == "Parameter") {                ddlpram = $('[id*=txtParameternew').val();                ddlpramvalue = $('[id*=ddlParamValue').val();            }            else {                ddlpram = $('[id*=ddlSdocPlungeCat').val();                ddlpramvalue = $('[id*=ddlSdocPlungeCatValues').val();            }
            if (ddlpramvalue == null) {
                ddlpramvalue = ' ';
            }
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ApplicationToolKit.aspx/orBtnClick") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{txtquery:"' + query + '",ddlparam:"' + ddlpram + '",ddlparamvalue:"' + ddlpramvalue + '"}',
                dataType: "json",
                success: function (response) {

                    var dataitem = response.d;
                    $('[id*=txtQuery]').val(dataitem);
                },
                error: function (Result) {
                    //  alert("Error 4");
                }
            });
            return false;
        }
        function andBtnClick() {
            if ($('[id*=txtQuery]').val() == "") {
                openErrorModal('Enter query.');
                return false;
            }
            var query = $('[id*=txtQuery]').val();
            var ddlpram;            var ddlpramvalue;            if ($('#searchIcon').text() == "Parameter") {                ddlpram = $('[id*=txtParameternew').val();                ddlpramvalue = $('[id*=ddlParamValue').val();            }            else {                ddlpram = $('[id*=ddlSdocPlungeCat').val();                ddlpramvalue = $('[id*=ddlSdocPlungeCatValues').val();            }
            if (ddlpramvalue == null) {
                ddlpramvalue = ' ';
            }
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ApplicationToolKit.aspx/andBtnClick") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{txtquery:"' + query + '",ddlparam:"' + ddlpram + '",ddlparamvalue:"' + ddlpramvalue + '"}',
                dataType: "json",
                success: function (response) {

                    var dataitem = response.d;
                    $('[id*=txtQuery]').val(dataitem);
                },
                error: function (Result) {
                    //  alert("Error 5");
                }
            });
            return false;
        }
        function parameterListCancel() {
            document.getElementById("parameterList").style.visibility = "hidden";
        }

        function minMaxParameterClick() {
            document.getElementById("parameterList").style.visibility = "visible";
            $('#parameterList').css('top', '66px');
            $('#parameterList').css('left', 'unset');
            $('#hdnMinMaxAvgGridClick').val("smallgrid");
        }

        $('#chkMinMaxParameterList td').change(function () {
            debugger;
            var CHK = document.getElementById("chkMinMaxParameterList");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");
            var counter = 0;

            if ($(this).children()[1].innerHTML == "Select All") {
                if (checkbox[0].checked == true) {
                    for (var i = 1; i < checkbox.length; i++) {
                        checkbox[i].checked = true;
                    }
                }
                else {
                    for (var i = 1; i < checkbox.length; i++) {
                        checkbox[i].checked = false;
                    }
                }
            }
            if ($(this).children()[0].checked == false) {
                checkbox[0].checked = false;
            }
        });
        function clearClick() {

            for (var i = 0; i < $('#btnContainer').children().length-1; i++) {
                if (i == 0) {
                    $('#btnContainer').children()[i].classList.remove("inactive");
                    $('#btnContainer').children()[i].setAttribute("class", "atkBtns active");
                }
                else {
                    console.log("Enter not 0 =" + i);
                    $('#btnContainer').children()[i].setAttribute("class", "atkBtns inactive");
                    $('#btnContainer').children()[i].classList.remove("active");
                }
            }
             
            
            return true;
        }
        $('#ddlQueryList').change(function () {
            $('#txtQuery').val($('#ddlQueryList').val());
        });
        function expandClick() {            console.log("HiddenCharts " + $('#hdnChartsIcon').val());            if ($('#hdnChartsIcon').val() == "charts") {                $('#hdnChartsIcon').val("");                document.getElementById('expand').classList.remove('charts');                // lnk.classList.add('glyphicon-chevron-left');                document.getElementById('expand').classList.remove('glyphicon-align-right');                document.getElementById('expand').classList.add('glyphicon-list-alt');                document.getElementById('dataSection').style.visibility = 'hidden';                $('#graghSection').addClass('expand');                document.getElementById('atkGraphs').style.display = 'block';                $('#atkGraphs').css("left", $('#graphDiv1').width() + 100);            }            else {                // lnk.classList.remove('glyphicon-chevron-left');                $('#hdnChartsIcon').val("charts");                document.getElementById('expand').classList.add('charts');                document.getElementById('expand').classList.remove('glyphicon-list-alt');                document.getElementById('expand').classList.add('glyphicon-align-right');                document.getElementById('dataSection').style.visibility = 'visible';                $('#graghSection').removeClass('expand');                document.getElementById('atkGraphs').style.display = 'none';            }        }

        $('#ddlMultiDownID').change(function () {            var whereCondition = "";            var query;            if ($('#txtQuery').val().includes('where') == true) {                whereCondition = $('#txtQuery').val().split('where')[1];                if ($("#ddlMultiDownID").find("option").length == $(this).val().length) {                    query = "select * from SystemDocTransaction where " + whereCondition.trim();                }                else {                    query = "select " + $(this).val() + " from SystemDocTransaction where " + whereCondition.trim();                }            }            else {                if ($("#ddlMultiDownID").find("option").length == $(this).val().length) {                    query = "select * from SystemDocTransaction";                }                else {                    query = "select " + $(this).val() + " from SystemDocTransaction";                }            }            $('#txtQuery').val(query);        });
        function executeClick() {
            for (var i = 0; i < $('#btnContainer').children().length; i++) {
                if ($('#btnContainer').children()[i].classList.contains("active")) {
                    if ($('#btnContainer').children()[i].id.trim() == "allInfoBtn") {
                        $('#hdnActiveBtn').val("allinformation");
                    }
                    else
                        if ($('#btnContainer').children()[i].id.trim() == "systemDocBtn") {
                            $('#hdnActiveBtn').val("systemdoc");
                        }
                        else if ($('#btnContainer').children()[i].id.trim() == "machineToolBtn") {
                            $('#hdnActiveBtn').val("machinetool");
                        }
                        else if ($('#btnContainer').children()[i].id.trim() == "wheelBtn") {
                            $('#hdnActiveBtn').val("wheel");
                        }
                        else if ($('#btnContainer').children()[i].id.trim() == "workPieceBtn") {
                            $('#hdnActiveBtn').val("workpiece");
                        }
                        else if ($('#btnContainer').children()[i].id.trim() == "opeParBtn") {
                            $('#hdnActiveBtn').val("operational");
                        }
                        else if ($('#btnContainer').children()[i].id.trim() == "targetQlyBtn") {
                            $('#hdnActiveBtn').val("quality");
                        }
                }
            }
            $('#hdnSearchToggle').val($('#searchIcon').text());
            if ($('#searchIcon').text() == "Parameter") {
                document.getElementById('ddlSdocPlungeCat').style.display = 'none';
                document.getElementById('ddlSdocPlungeCatValues').style.display = 'none';
                document.getElementById('txtParameternew').style.display = 'inline-block';
                document.getElementById('ddlParamValue').style.display = 'inline-block';
            }
            else {
                document.getElementById('ddlSdocPlungeCat').style.display = 'inline-block';
                document.getElementById('ddlSdocPlungeCatValues').style.display = 'inline-block';
                document.getElementById('txtParameternew').style.display = 'none';
                document.getElementById('ddlParamValue').style.display = 'none';
            }
            if ($("#txtQuery").val() == "") {
                $('[id*=myWarningModal]').modal('show');
                // document.getElementById("myWarningModal").style.visibility = 'visible';
                $("#warningmessageText").text("Some Query is required.");
                return false;
            }
            else {
                $.blockUI({ message: '<img src="Images/ajax-loader (1).gif"  style="display: inline-block;vertical-align: text-bottom"/>&nbsp;&nbsp;<h2 style="display: inline-block;color:#524b4b;margin:20px 0px;">Just a moment...</h2>' });
                setTimeout($.unblockUI, 1000);
                return true;
            }
        }
        function openErrorModal(msg) {

            $('[id*=myErrorModal]').modal('show');
            $("#errormessageText").text(msg);
        };
        $(window).resize(function () {
            var wHeight = $(window).height() - ($('#btnContainer').height() + 160 + 220);
            $('#gridContainer').css('height', wHeight);
            var minmaxHeight = (($(window).height() - 180) / 2);
            $('#MinMaxContainer').css('height', minmaxHeight);
            $('#gcontainer').css('height', minmaxHeight - 50);
            $('#atkGraphs').css('width', $(window).width() - $('#graphDiv1').width() - $('#sidebar').width() - 200);
            $('#atkGraphs').css("left", $('#graphDiv1').width() + 100);
            //alert( $(window).width() - $('#graphDiv1').width() - $('#sidebar').width() - 200);
            $('.graphDiv').css('height', minmaxHeight - 40);
        });
        $('.atkBtns').click(function () {

            $(this).addClass("active");
            $(this).removeClass("inactive");
            $(this).siblings().removeClass("active");
            $(this).siblings().addClass("inactive");
        });

        function bindGraph() {
            var xitems = [];
            var yitems = [];

           // var xaxis = $('#ddlGraphParam option:selected').text();
           // $('#lblMean').text($('#ddlGraphParam').val());
                  var xaxis = $("#txtGraphParams1").val();
            var optionlist = $("#ddlGraphParams1 option");
            var hiddenlist = $("#ddlGraphParams1 input");
            getSelectedParameterAvgValue(optionlist,hiddenlist,xaxis,"lblMean");
            var value1 = [];
            var frequency = [];
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ApplicationToolKit.aspx/getDDLParamValueFrequency") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{param:"' + xaxis + '",querytxt:"' + $('#txtQuery').val() + '"}',
                dataType: "json",
                success: function (response) {

                    var dataitem = response.d;

                    for (var i = 0; i < dataitem.length; i++) {

                        value1[i] = dataitem[i].Value;
                        if (value1[i] == "") {
                            value1[i] = 0;
                        }
                        else {
                            value1[i] = dataitem[i].Value;
                        }
                        frequency[i] = parseFloat(dataitem[i].Text);

                        var serie = new Array(value1[i], frequency[i]);
                        yitems.push(serie);

                        var serie1 = new Array(value1[i]);
                        xitems.push(serie1);

                        console.log("value = " + value1[i] + "  Fre=" + frequency[i]);
                    }
                },
                error: function (Result) {
                    // alert("Error 6");
                }
            });
            //var processedData = [];;            //for (var i = 0; i < yitems.length; i++) {            //    processedData.push({            //        y: value1[i],            //        frequency: frequency[i]            //    })            //}
            console.log("data" + value1[0] + "" + frequency[0]);

            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'gcontainer',
                    type: 'column'
                    //options3d: {
                    //    enabled: true,
                    //    alpha: 15,
                    //    beta: 15,
                    //    depth: 50,
                    //    viewDistance: 25
                    //}
                },
                credits: {
                    enabled: false
                },
                exporting: {
                    enabled: true
                },
                navigation: {
                    buttonOptions: {
                        align: 'top'
                    }
                },
                title: {
                    text: xaxis
                },
                tooltip: {                    //pointFormat: '{series.name}: <b>{point.frequency}</b>'                    formatter: function () {                        //return '<b>' + this.point.y + ':</b> ' + this.point.xitems;                        return '<b>Frequency: ' + this.point.y;                    }                },
                yAxis: {
                    allowDecimals: true,
                    title: {
                        text: 'Frequency'
                    }
                },
                xAxis: {
                    allowDecimals: false,
                    categories: xitems,
                    title: {
                        text: xaxis
                    }
                }
                ,
                //subtitle: {
                //    text: 'Test options by dragging the sliders below'
                //},
                plotOptions: {
                    column: {
                        depth: 25
                    }
                },
                series: [{
                    color: '#9594e3',
                    // name: 'Frequency' + frequency,
                    //data: processedData,
                    //   name: ["sd","asd"]
                    data: yitems,
                    showInLegend: false,
                    pointPadding: -0.3
                }]
            });
        }
        function getSelectedParameterAvgValue(optionList,hiddenlist, selectedParam, labelID) {
            var flag = 0;
            debugger;
            for (var i = 0; i < optionList.length; i++) {
                if (selectedParam.trim() == optionList[i].text.trim()) {
                    $('#' + labelID).text(hiddenlist[i].value.trim());
                    flag = 1;
                    break;
                }
            }
            if (flag == 0) {
                $('#' + labelID).text('');
            }
        }
        function bindGraph2() {
            var xitems2 = [];
            var yitems2 = [];
            //var param = $('#ddlGraphParam2 option:selected').text();
              // $('#lblMean2').text($('#ddlGraphParam2').val());
            var param = $("#txtGraphParams2").val();
            var optionlist = $("#ddlGraphParams2 option");
            var hiddenlist = $("#ddlGraphParams2 input");
            getSelectedParameterAvgValue(optionlist,hiddenlist,param,"lblMean2");

            var value1 = [];
            var frequency = [];
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ApplicationToolKit.aspx/getDDLParamValueFrequency") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{param:"' + param + '",querytxt:"' + $('#txtQuery').val() + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    for (var i = 0; i < dataitem.length; i++) {
                        value1[i] = dataitem[i].Value;
                        if (value1[i] == "") {
                            value1[i] = 0;
                        }
                        else {
                            value1[i] = dataitem[i].Value;
                        }
                        frequency[i] = parseFloat(dataitem[i].Text);

                        var serie = new Array(value1[i], frequency[i]);
                        yitems2.push(serie);

                        var serie1 = new Array(value1[i]);
                        xitems2.push(serie1);
                        console.log("value = " + value1[i] + "  Fre=" + frequency[i]);
                    }
                },
                error: function (Result) {
                    // alert("Error 7");
                }
            });
            //var processedData = [];            //for (var i = 0; i < value1.length; i++) {            //    processedData.push({            //        y: value1[i],            //        frequency: frequency[i]            //    })            //}
            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'graph2',
                    type: 'column'
                    //options3d: {
                    //    enabled: true,
                    //    alpha: 15,
                    //    beta: 15,
                    //    depth: 50,
                    //    viewDistance: 25
                    //}
                },
                title: {
                    text: param
                },
                credits: {
                    enabled: false
                },
                exporting: {
                    enabled: true
                },
                navigation: {
                    buttonOptions: {
                        align: 'top'
                    }
                },
                tooltip: {                    //pointFormat: '{series.name}: <b>{point.frequency}</b>'                    formatter: function () {                        //  return '<b>' + this.point.y + ':</b> ' + this.point.frequency;                        return '<b>Frequency: ' + this.point.y;                    }                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Frequency'
                    }
                },
                xAxis: {
                    allowDecimals: false,
                    categories: xitems2,
                    title: {
                        text: param
                    }
                },
                //subtitle: {
                //    text: 'Test options by dragging the sliders below'
                //},
                plotOptions: {
                    column: {
                        depth: 25
                    }
                },
                series: [{
                    color: '#9594e3',
                    // name: 'Frequency',
                    data: yitems2,
                    showInLegend: false,
                    pointPadding: -0.3
                }]
            });
        }
        function bindGraph3() {
            var xitems3 = [];
            var yitems3 = [];
          //  var param = $('#ddlGraphParam3 option:selected').text();
            //$('#lblMean3').text($('#ddlGraphParam3').val());
             var param = $("#txtGraphParams3").val();
            var optionlist = $("#ddlGraphParams3 option");
            var hiddenlist = $("#ddlGraphParams3 input");
            getSelectedParameterAvgValue(optionlist,hiddenlist,param,"lblMean3");
            var value1 = [];
            var frequency = [];
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ApplicationToolKit.aspx/getDDLParamValueFrequency") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{param:"' + param + '",querytxt:"' + $('#txtQuery').val() + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    for (var i = 0; i < dataitem.length; i++) {
                        value1[i] = dataitem[i].Value;
                        if (value1[i] == "") {
                            value1[i] = 0;
                        }
                        else {
                            value1[i] = dataitem[i].Value;
                        }
                        frequency[i] = parseFloat(dataitem[i].Text);
                        var serie = new Array(value1[i], frequency[i]);
                        yitems3.push(serie);

                        var serie1 = new Array(value1[i]);
                        xitems3.push(serie1);
                        console.log("value = " + value1[i] + "  Fre=" + frequency[i]);
                    }
                },
                error: function (Result) {
                    //alert("Error 8");
                }
            });
            var processedData = [];            for (var i = 0; i < value1.length; i++) {                processedData.push({                    y: value1[i],                    frequency: frequency[i]                })            }
            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'graph3',
                    type: 'column'
                    //options3d: {
                    //    enabled: true,
                    //    alpha: 15,
                    //    beta: 15,
                    //    depth: 50,
                    //    viewDistance: 25
                    //}
                },
                title: {
                    text: param
                },
                credits: {
                    enabled: false
                },
                exporting: {
                    enabled: true
                },
                navigation: {
                    buttonOptions: {
                        align: 'top'
                    }
                },
                tooltip: {                    //pointFormat: '{series.name}: <b>{point.frequency}</b>'                    formatter: function () {                        //return '<b>' + this.point.y + ':</b> ' + this.point.frequency;                        return '<b>Frequency: ' + this.point.y;                    }                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Frequency'
                    }
                },
                xAxis: {
                    allowDecimals: false,
                    categories: xitems3,
                    title: {
                        text: param
                    }
                },
                //subtitle: {
                //    text: 'Test options by dragging the sliders below'
                //},
                plotOptions: {
                    column: {
                        depth: 25
                    }
                },
                series: [{
                    color: '#9594e3',
                    name: 'Frequency',
                    data: yitems3,
                    showInLegend: false,
                    pointPadding: -0.3
                }]
            });
        }
        function bindGraph4() {
            var xitems4 = [];
            var yitems4 = [];
            //var param = $('#ddlGraphParam4 option:selected').text();
            //$('#lblMean4').text($('#ddlGraphParam4').val());
             var param = $("#txtGraphParams4").val();
            var optionlist = $("#ddlGraphParams4 option");
            var hiddenlist = $("#ddlGraphParams4 input");
            getSelectedParameterAvgValue(optionlist,hiddenlist,param,"lblMean4");
            var value1 = [];
            var frequency = [];
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ApplicationToolKit.aspx/getDDLParamValueFrequency") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{param:"' + param + '",querytxt:"' + $('#txtQuery').val() + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    for (var i = 0; i < dataitem.length; i++) {
                        value1[i] = dataitem[i].Value;
                        if (value1[i] == "") {
                            value1[i] = 0;
                        }
                        else {
                            value1[i] = dataitem[i].Value;
                        }
                        frequency[i] = parseFloat(dataitem[i].Text);

                        var serie = new Array(value1[i], frequency[i]);
                        yitems4.push(serie);

                        var serie1 = new Array(value1[i]);
                        xitems4.push(serie1);

                        console.log("value = " + value1[i] + "  Fre=" + frequency[i]);
                    }
                },
                error: function (Result) {
                    // alert("Error 9");
                }
            });
            var processedData = [];            for (var i = 0; i < value1.length; i++) {                processedData.push({                    y: value1[i],                    frequency: frequency[i]                })            }
            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'graph4',
                    type: 'column'
                    //options3d: {
                    //    enabled: true,
                    //    alpha: 15,
                    //    beta: 15,
                    //    depth: 50,
                    //    viewDistance: 25
                    //}
                },
                title: {
                    text: param
                },
                credits: {
                    enabled: false
                },
                exporting: {
                    enabled: true
                },
                navigation: {
                    buttonOptions: {
                        align: 'top'
                    }
                },
                tooltip: {                    //pointFormat: '{series.name}: <b>{point.frequency}</b>'                    formatter: function () {                        //  return '<b>' + this.point.y + ':</b> ' + this.point.frequency;                        return '<b>Frequency: ' + this.point.y;                    }                },
                yAxis: {
                    allowDecimals: false,

                    title: {
                        text: 'Frequency'
                    }
                },
                xAxis: {
                    allowDecimals: false,
                    categories: xitems4,
                    title: {
                        text: param
                    }
                },
                //subtitle: {
                //    text: 'Test options by dragging the sliders below'
                //},
                plotOptions: {
                    column: {
                        depth: 25
                    }
                },
                series: [{
                    color: '#9594e3',
                    name: 'Frequency',
                    data: yitems4,
                    showInLegend: false,
                    pointPadding: -0.3
                }]
            });
        }
        function bindGraph5() {
            var xitems5 = [];
            var yitems5 = [];
            //var param = $('#ddlGraphParam5 option:selected').text();
            //$('#lblMean5').text($('#ddlGraphParam5').val());
             var param = $("#txtGraphParams5").val();
            var optionlist = $("#ddlGraphParams5 option");
            var hiddenlist = $("#ddlGraphParams5 input");
            getSelectedParameterAvgValue(optionlist,hiddenlist,param,"lblMean5");
            var value1 = [];
            var frequency = [];
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("ApplicationToolKit.aspx/getDDLParamValueFrequency") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{param:"' + param + '",querytxt:"' + $('#txtQuery').val() + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    for (var i = 0; i < dataitem.length; i++) {
                        value1[i] = dataitem[i].Value;
                        if (value1[i] == "") {
                            value1[i] = 0;
                        }
                        else {
                            value1[i] = dataitem[i].Value;
                        }
                        frequency[i] = parseFloat(dataitem[i].Text);

                        var serie = new Array(value1[i], frequency[i]);
                        yitems5.push(serie);

                        var serie1 = new Array(value1[i]);
                        xitems5.push(serie1);
                        console.log("value = " + value1[i] + "  Fre=" + frequency[i]);
                    }
                },
                error: function (Result) {
                    //alert("Error 10");
                }
            });
            var processedData = [];            for (var i = 0; i < value1.length; i++) {                processedData.push({                    y: value1[i],                    frequency: frequency[i]                })            }
            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'graph5',
                    type: 'column'
                    //options3d: {
                    //    enabled: true,
                    //    alpha: 15,
                    //    beta: 15,
                    //    depth: 50,
                    //    viewDistance: 25
                    //}
                },
                title: {
                    text: param
                },
                credits: {
                    enabled: false
                },
                exporting: {
                    enabled: true
                },
                navigation: {
                    buttonOptions: {
                        align: 'top'
                    }
                },
                tooltip: {                    //pointFormat: '{series.name}: <b>{point.frequency}</b>'                    formatter: function () {                        // return '<b>' + this.point.y + ':</b> ' + this.point.frequency;                        return '<b>Frequency: ' + this.point.y;                    }                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Frequency'
                    }
                },
                xAxis: {
                    allowDecimals: false,
                    categories: xitems5,
                    title: {
                        text: param
                    }
                },
                //subtitle: {
                //    text: 'Test options by dragging the sliders below'
                //},
                plotOptions: {
                    column: {
                        depth: 25
                    }
                },
                series: [{
                    color: '#9594e3',
                    name: 'Frequency',
                    data: yitems5,
                    showInLegend: false,
                    pointPadding: -0.3
                }]
            });
        }

        $('#txtGraphParams1').change(function () {
            bindGraph();
            minMaxAvgGridActivation();
        });
        $('#txtGraphParams2').change(function () {
            bindGraph2();
            minMaxAvgGridActivation();
        });
        $('#txtGraphParams3').change(function () {
            bindGraph3();
            minMaxAvgGridActivation();
        });
        $('#txtGraphParams4').change(function () {
            bindGraph4();
            minMaxAvgGridActivation();
        });
        $('#txtGraphParams5').change(function () {
            bindGraph5();
            minMaxAvgGridActivation();
        });


        function LoadSDoc() {
            var sDocId = $("#txtSdocID").val();
            if (sDocId == "") {
                $('[id*=myWarningModal]').modal('show');
                //document.getElementById("myWarningModal").style.visibility = 'visible';
                $("#warningmessageText").text("SDoc ID is required.");
                return false;
            }
            $.ajax({
                async: false,
                type: "POST",
                url: "ApplicationToolKit.aspx/setSDocIDInSession",
                contentType: "application/json; charset=utf-8",
                data: '{id:"' + sDocId + '"}',
                datatype: "json",
                success: function (response) {
                    var itmdata = response.d;
                    window.location.href = "DataInputModule.aspx";
                },
                error: function (jqXHR, textStatus, err) {
                    console.log(err);
                    //  alert('Error: ' + err);
                    // if (jqXHR.status == 401)
                    //  alert('Error: 11' + err);
                }
            })

            return false;
        }

        $("[id*=gvnested] tr td").click(function () {
            var index = $(this).index();
            //var colname = $(this).closest("table").find('th:eq(' + index + ')').text();
            if (index == 0) {
                var sDocId = $(this).text();
                $("#txtSdocID").val(sDocId);
               // navigator.clipboard.writeText(sDocId);
                  var copyGfGText = document.getElementById("txtSdocID");
                copyGfGText.select();
                document.execCommand('copy');

                // $(ele).tooltip({
                //    //tooltipClass: 'tooltipclass',
                //    content: "<strong>Copied</strong>",
                //    position: {
                //        my: "center bottom",
                //        at: "center top-10",
                //        collision: "none"
                //    }
                //});
            }
        });

        $("[id*=gvNormalSDocbind] tr td").click(function () {

            let val = $(this);
            var index = $(this).index();
            //var colname = $(this).closest("table").find('th:eq(' + index + ')').text();
            if (index == 0) {
                var sDocId = $(this).text();
                $("#txtSdocID").val(sDocId);
                // navigator.clipboard.writeText(sDocId);
                var copyGfGText = document.getElementById("txtSdocID");
                copyGfGText.select();
                document.execCommand('copy');

                // $("[id*=gvNormalSDocbind] tr td:eq("+ index+")").prop('title', 'Copied');
                //// alert(val.prop('title'));
                // val.tooltip({
                //     tooltipClass: 'tooltipclass',
                //     //content: "<strong>Copied</strong>",
                //     position: {
                //         my: "center bottom",
                //         at: "center top-10",
                //         at: "center top-10",
                //         collision: "none"
                //     }
                // });
            }
        });

        function openWarningModal(msg) {
            $('[id*=myWarningModal]').modal('show');
              // document.getElementById("myWarningModal").style.visibility = 'visible';
            $("#warningmessageText").text(msg);
        }

      

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {

            var pressed = false;            var start = undefined;            var startX, startWidth;            $(".resizeColumn th").mousedown(function (e) {                start = $(this);                pressed = true;                startX = e.pageX;                startWidth = $(this).width();                $(start).addClass("resizing");                $(start).addClass("noSelect");            });            $(document).mousemove(function (e) {                if (pressed) {                    $(start).width(startWidth + (e.pageX - startX));                }            });            $(document).mouseup(function () {                if (pressed) {                    $(start).removeClass("resizing");                    $(start).removeClass("noSelect");                    pressed = false;                }            });

            function drillSDoc(val) {
                debugger;

                if ($(val).hasClass('plus')) {
                    $(val).removeClass('plus');
                    $(val).removeClass('glyphicon-chevron-down');
                    $(val).addClass('glyphicon-chevron-right');
                    $(val).parent().children()[1].children[0].style.display = 'none';
                } else {
                    $(val).addClass('plus');
                    $(val).addClass('glyphicon-chevron-down');
                    $(val).removeClass('glyphicon-chevron-right');
                    $(val).parent().children()[1].children[0].style.display = 'block';
                }
            }

            function selectAllInputModule(val) {
                debugger;
                $("[id*=txtsearch]").val("");
                let inputcheckbox = $("#tblminmaxparamlist  input");
                let td = $("#tblminmaxparamlist  td");
                if ($(val).children()[0].checked) {
                    for (let i = 0; i < inputcheckbox.length; i++) {
                        td[i].style.display = "block";
                        inputcheckbox[i].checked = true;
                    }
                } else {
                    for (let i = 0; i < inputcheckbox.length; i++) {
                        td[i].style.display = "block";
                        inputcheckbox[i].checked = false;
                    }
                }
            }

            function checkoruncheckInputModule(val) {
                debugger;
                let inputchekbox = $(val).closest('td').find('#chkList input');
                let count = 0;
                for (let i = 0; i < inputchekbox.length; i++) {
                    if (inputchekbox[i].checked) {
                        count++;
                    }
                }
                if (count == inputchekbox.length) {
                    $(val).closest('td').children()[0].children[0].checked = true;
                } else {
                    $(val).closest('td').children()[0].children[0].checked = false;
                }

                count = 0;
                for (let i = 0; i < $("#tblminmaxparamlist #cbSelectallInput").length; i++) {
                    if ($("#tblminmaxparamlist #cbSelectallInput")[i].checked) {
                        count++;
                    }
                }
                if (count == $("#tblminmaxparamlist #cbSelectallInput").length) {
                    $("#cbselectAllInputModule").prop('checked', true);
                } else {
                    $("#cbselectAllInputModule").prop('checked', false);
                }

            }

            function selectAllInputParameter(val) {
                debugger;
                let inputcheckbox = $(val).closest('td').find('#chkList input');
                let label = $(val).closest('td').find('#chkList label');

                if ($(val).children()[0].checked) {
                    for (let i = 0; i < inputcheckbox.length; i++) {
                        if ($(val).closest('td').find('#chkList td')[i].style.display == "block" || $(val).closest('td').find('#chkList td')[i].style.display == "") {
                            inputcheckbox[i].checked = true;
                        }
                    }
                } else {
                    for (let i = 0; i < inputcheckbox.length; i++) {
                        inputcheckbox[i].checked = false;
                    }
                }

                let count = 0;
                for (let i = 0; i < $("#tblminmaxparamlist #cbSelectallInput").length; i++) {
                    if ($("#tblminmaxparamlist #cbSelectallInput")[i].checked) {
                        count++;
                    }
                }
                if (count == $("#tblminmaxparamlist #cbSelectallInput").length) {
                    $("#cbselectAllInputModule").prop('checked', true);
                } else {
                    $("#cbselectAllInputModule").prop('checked', false);
                }
            }

            function search(val, evt) {
                let txtSearch = $(val).val();
                debugger;
                let input = $("#tblminmaxparamlist #chkList input");
                let label = $("#tblminmaxparamlist #chkList label");
                let td = $("#tblminmaxparamlist #chkList td");
                for (var i = 0; i < input.length; i++) {
                    var chkvalue = label[i].innerText.slice(0, txtSearch.length);
                    if (chkvalue.toLowerCase() == txtSearch.toLowerCase()) {
                        td[i].style.display = "block";
                    } else {
                        td[i].style.display = "none";
                    }
                }
            }
            lvminmaxActivation();
            function checkGridParametertoPanel() {
                debugger;
                let CHKid = $("#chkMinMaxParameterList tr");
                let checkboxItem = $("#chkMinMaxParameterList tr input");
                let gridparameters = $("#gvMinMaxAvg tr td:first-Child span");
                for (let i = 0; i < checkboxItem.length; i++) {
                    let parameterName = checkboxItem[i].value.split(',')[0];
                    for (let j = 0; j < gridparameters.length; j++) {
                        let gridparametrName = gridparameters[j].textContent;
                        if (gridparametrName == parameterName) {
                            checkboxItem[i].checked = true;
                            console.log("Equal" + gridparametrName + " , " + parameterName);
                        } else {
                            //checkboxItem[i].checked = false;
                            console.log("not Equal" + gridparametrName + " , " + parameterName);
                        }
                    }
                }
            }

            $("[id*=ddlMinMaxInputModule]").change(function () {
              
                var inputModule = $(this).val();
                 let inputModuleParameterCount = 0;
             let inputModuleParameterCheckedCount = 0;
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ApplicationToolKit.aspx/setInputModuletoSession") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{inputModule:"' + inputModule + '"}',
                    dataType: "json",
                    success: function (response) {

                        var dataitem = response.d;
                    },
                    error: function (Result) {

                    }
                });
                //let CHK = document.getElementById("chkMinMaxParameterList");
                //let checkbox = CHK.getElementsByTagName("input");
                //for (let i = 1; i < checkbox.length; i++) {
                //    checkbox[i].checked = false;
                //}
                 checkGridParametertoPanel();
                if (inputModule == "All") {
                    for (var i = 1; i < $('#chkMinMaxParameterList tr').length; i++) {
                        var tr = $('#chkMinMaxParameterList tr').children()[i];
                         var input = $("#chkMinMaxParameterList tr input");
                        tr.style.display = "block";
                         inputModuleParameterCount++;
                    if (input[i].checked == true) {
                        inputModuleParameterCheckedCount++;
                    }
                    }
                } else {
                    for (var i = 1; i < $('#chkMinMaxParameterList tr').length; i++) {
                        var tr = $('#chkMinMaxParameterList tr').children()[i];
                        var input = $("#chkMinMaxParameterList tr input");
                        //var inputModuleName = tr.closest('td').children[0].value.split(',')[1];
                        var inputModuleName = input[i].value.split(',')[1];
                        if (inputModuleName == inputModule) {
                            tr.style.display = "block";
                            inputModuleParameterCount++;
                            if (input[i].checked == true) {
                                inputModuleParameterCheckedCount++;
                            }

                        } else {
                            tr.style.display = "none";
                               input[i].checked = false;
                        }
                    }
                }
               
                if (inputModuleParameterCount == inputModuleParameterCheckedCount) {
                    input[0].checked = true;
                } else {
                    input[0].checked = false;
                }
                
            });

          



            function openWarningModal(msg) {
                $('[id*=myWarningModal]').modal('show');
                 //document.getElementById("myWarningModal").style.visibility = 'visible';
                $("#warningmessageText").text(msg);
            }

            $(document).ready(function () {
            
                var Height = $(window).height() - ($('#btnContainer').height() + 160 + 220);
                $('#gridContainer').css('height', Height);
                console.log("H =" + Height);
                var minmaxHeight = (($(window).height() - 180) / 2);
                $('#MinMaxContainer').css('height', minmaxHeight);
                $('#gcontainer').css('height', minmaxHeight - 50);
                $('#atkGraphs').css('width', $(window).width() - $('#graphDiv1').width() - $('#sidebar').width() - 200);
                $('.graphDiv').css('height', minmaxHeight - 40);
                $('[id$=ddlMultiDownID]').multiselect({                    includeSelectAllOption: true                });
                bindGraph();
                bindGraph2();
                bindGraph3();
                bindGraph4();
                bindGraph5();
                $('#gcontainer').children()[0].style.width = "100%";
                $('#gcontainer').children()[0].style.height = "100%";
                $('#gcontainer').children()[0].style.overflow = 'auto';
                $('#graph2').children()[0].style.width = "100%";
                $('#graph2').children()[0].style.overflow = 'auto';
                $('#graph3').children()[0].style.width = "100%";
                $('#graph3').children()[0].style.overflow = 'auto';
                $('#graph4').children()[0].style.width = "100%";
                $('#graph4').children()[0].style.overflow = 'auto';
                $('#graph5').children()[0].style.width = "100%";
                $('#graph5').children()[0].style.overflow = 'auto';

                $('#ddlQueryList option').each(function () {                    var optionText = this.text;                    var newOption = optionText.substring(0, 100);                    if (newOption.length > 99) {                        $(this).text(newOption + '...');                    }                    else {                        $(this).text(newOption);                    }                });
                //  bindGraph();
                //$('#gcontainer').children()[0].style.width = "100%";
                bindSdocPlungeCatValue();
                bindParameterValue();


                expandClick();
                minMaxAvgGridActivation();
                if ($('#hdnMinMaxAvgGridClick').val() == "largegrid") {
                    document.getElementById("showbidGridMinMax").style.visibility = "visible";
                }
            });

             function showGridBtnClick() {
            document.getElementById("showbidGridMinMax").style.visibility = "visible";
            $('#hdnMinMaxAvgGridClick').val("largegrid");
            // $('#parameterList').css('height', minmaxHeight - 50);
        }
            function showbidGridMinMaxCancel() {
                document.getElementById("showbidGridMinMax").style.visibility = "hidden";
            }
            function largeminMaxParameterClick() {
                document.getElementById("parameterList").style.visibility = "visible";
                $('#parameterList').css('top', '153px');
                $('#parameterList').css('left', '-180%');
            }

            $('#txtParameternew').change(function () {
                bindParameterValue();
            });


            function minMaxAvgGridActivation() {
                var ddlSelectedText = [];
                var ddlSelectedText1 = "";
                if ($("#txtGraphParams1").val() != "") {
                    ddlSelectedText.push($("#txtGraphParams1").val());
                    ddlSelectedText1 += $("#txtGraphParams1").val().trim();
                }
                if ($("#txtGraphParams2").val() != "") {
                    ddlSelectedText.push($("#txtGraphParams2").val());
                    ddlSelectedText1 += $("#txtGraphParams2").val().trim();
                }
                if ($("#txtGraphParams3").val() != "") {
                    ddlSelectedText.push($("#txtGraphParams3").val());
                    ddlSelectedText1 += $("#txtGraphParams3").val().trim();
                }
                if ($("#txtGraphParams4").val() != "") {
                    ddlSelectedText.push($("#txtGraphParams4").val());
                    ddlSelectedText1 += $("#txtGraphParams4").val().trim();
                }
                if ($("#txtGraphParams5").val() != "") {
                    ddlSelectedText.push($("#txtGraphParams5").val());
                    ddlSelectedText1 += $("#txtGraphParams5").val().trim();
                }

                var gridTbl = document.getElementById("gvMinMaxAvg");
                var gridTblLarge = document.getElementById("gvminmaxLarge");
                if (gridTbl != null) {
                    for (var j = 1; j < gridTbl.rows.length; j++) {
                        var gridTblRow = gridTbl.rows[j];
                        var largeGridTblRow = gridTblLarge.rows[j];
                        if (gridTblRow.cells.length > 0) {

                            if (ddlSelectedText1.includes(gridTblRow.firstElementChild.textContent.trim())) {
                                gridTblRow.firstElementChild.style.backgroundColor = "#dadaec";
                                gridTblRow.cells[1].style.backgroundColor = "#dadaec";
                                gridTblRow.cells[2].style.backgroundColor = "#dadaec";
                                gridTblRow.cells[3].style.backgroundColor = "#dadaec";

                                largeGridTblRow.firstElementChild.style.backgroundColor = "#dadaec";
                                largeGridTblRow.cells[1].style.backgroundColor = "#dadaec";
                                largeGridTblRow.cells[2].style.backgroundColor = "#dadaec";
                                largeGridTblRow.cells[3].style.backgroundColor = "#dadaec";
                                //debugger;
                                // gridTblRow.firstElementChild.classList.add('gridCs');

                            }
                            else {
                                gridTblRow.firstElementChild.style.backgroundColor = "white";
                                gridTblRow.cells[1].style.backgroundColor = "white";
                                gridTblRow.cells[2].style.backgroundColor = "white";
                                gridTblRow.cells[3].style.backgroundColor = "white";
                                //  gridTblRow.firstElementChild.classList.add('gridCsRev');

                                largeGridTblRow.firstElementChild.style.backgroundColor = "white";
                                largeGridTblRow.cells[1].style.backgroundColor = "white";
                                largeGridTblRow.cells[2].style.backgroundColor = "white";
                                largeGridTblRow.cells[3].style.backgroundColor = "white";

                            }
                            if (gridTblRow.cells[0].firstElementChild.value.trim() == "1") {
                                gridTblRow.cells[0].children[1].style.color = "#622f2f";
                                largeGridTblRow.cells[0].children[1].style.color = "#622f2f";
                            }
                            else {
                                gridTblRow.cells[0].children[1].style.color = "black";
                                largeGridTblRow.cells[0].children[1].style.color = "black";
                            }
                        }
                    }
                }
                var chkMultiSelect = document.getElementById("chkMinMaxParameterList");
                if (chkMultiSelect != null) {
                    var chkbox = chkMultiSelect.getElementsByTagName('input');
                    var lbl = chkMultiSelect.getElementsByTagName('label');
                    for (var i = 1; i < chkbox.length; i++) {
                        var derivedFlag = chkbox[i].value.split(',')[2];
                        if (derivedFlag.trim() == "1") {
                            lbl[i].style.color = "#6f1616";
                        }
                    }
                }
            }
            function bindParameterValue() {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ApplicationToolKit.aspx/bindParameterValues") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{parameter:"' + $('[id*=txtParameternew]').val().toString() + '",txtqueryvalue:"' + $('[id*=txtQuery]').val() + '"}',
                    dataType: "json",
                    success: function (response) {

                        var dataitem = response.d;
                        var list = "";
                        for (var i = 0; i < dataitem.length; i++) {
                            list += '<option value="' + dataitem[i] + '">' + dataitem[i] + '</option>';
                        }
                        $('[id*=ddlParamValue]').empty();
                        $('[id*=ddlParamValue]').append(list);
                    },
                    error: function (Result) {
                        // alert("Error 12");
                    }
                });
            }
            function searchCriteriaClick(lnk) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ApplicationToolKit.aspx/setSessionAPKDisplayCondition") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    dataType: "json",
                    success: function (response) {

                        var dataitem = response.d;
                    },
                    error: function (Result) {
                        //alert("Error 13");
                    }
                });
                $('[id*=txtQuery]').val("select * from SystemDocTransaction");
                if (lnk.textContent == "Parameter") {
                    lnk.textContent = "System Doc";
                    document.getElementById('ddlSdocPlungeCat').style.display = 'inline-block';
                    document.getElementById('ddlSdocPlungeCatValues').style.display = 'inline-block';
                    document.getElementById('txtParameternew').style.display = 'none';
                    document.getElementById('ddlParamValue').style.display = 'none';
                    bindSdocPlungeCatValue();
                }
                else {
                    lnk.textContent = "Parameter";
                    document.getElementById('ddlSdocPlungeCat').style.display = 'none';
                    document.getElementById('ddlSdocPlungeCatValues').style.display = 'none';
                    document.getElementById('txtParameternew').style.display = 'inline-block';
                    document.getElementById('ddlParamValue').style.display = 'inline-block';
                    bindParameterValue();
                }
                //  $('[id*=txtQuery]').val("select * from SystemDocTransaction");
            }
            $('#ddlSdocPlungeCat').change(function () {
                bindSdocPlungeCatValue();
            });
            function bindSdocPlungeCatValue() {
                console.log("bindSdocPlungeCatValue");
               
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ApplicationToolKit.aspx/bindSdocPlungeCatValues") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{parameter:"' + $('[id*=ddlSdocPlungeCat]').val() + '",txtqueryvalue:"' + $('[id*=txtQuery]').val() + '"}',
                    dataType: "json",
                    success: function (response) {

                        var dataitem = response.d;
                        var list = "";
                        for (var i = 0; i < dataitem.length; i++) {
                            list += '<option value="' + dataitem[i] + '">' + dataitem[i] + '</option>';
                        }
                        $('[id*=ddlSdocPlungeCatValues]').empty();
                        $('[id*=ddlSdocPlungeCatValues]').append(list);
                    },
                    error: function (Result) {
                        // alert("Error 14");
                    }
                });
            }
            function orBtnClick() {
                if ($('[id*=txtQuery]').val() == "") {
                    openErrorModal('Enter query.');
                    return false;
                }
                var query = $('[id*=txtQuery]').val();
                var ddlpram;                var ddlpramvalue;                if ($('#searchIcon').text() == "Parameter") {                    ddlpram = $('[id*=txtParameternew').val();                    ddlpramvalue = $('[id*=ddlParamValue').val();                }                else {                    ddlpram = $('[id*=ddlSdocPlungeCat').val();                    ddlpramvalue = $('[id*=ddlSdocPlungeCatValues').val();                }
                if (ddlpramvalue == null) {
                    ddlpramvalue = ' ';
                }
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ApplicationToolKit.aspx/orBtnClick") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{txtquery:"' + query + '",ddlparam:"' + ddlpram + '",ddlparamvalue:"' + ddlpramvalue + '"}',
                    dataType: "json",
                    success: function (response) {

                        var dataitem = response.d;
                        $('[id*=txtQuery]').val(dataitem);
                    },
                    error: function (Result) {
                        // alert("Error 15");
                    }
                });
                return false;
            }
            function andBtnClick() {
                if ($('[id*=txtQuery]').val() == "") {
                    openErrorModal('Enter query.');
                    return false;
                }
                var query = $('[id*=txtQuery]').val();
                var ddlpram;                var ddlpramvalue;                if ($('#searchIcon').text() == "Parameter") {                    ddlpram = $('[id*=txtParameternew').val();                    ddlpramvalue = $('[id*=ddlParamValue').val();                }                else {                    ddlpram = $('[id*=ddlSdocPlungeCat').val();                    ddlpramvalue = $('[id*=ddlSdocPlungeCatValues').val();                }
                if (ddlpramvalue == null) {
                    ddlpramvalue = ' ';
                }
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ApplicationToolKit.aspx/andBtnClick") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{txtquery:"' + query + '",ddlparam:"' + ddlpram + '",ddlparamvalue:"' + ddlpramvalue + '"}',
                    dataType: "json",
                    success: function (response) {

                        var dataitem = response.d;
                        $('[id*=txtQuery]').val(dataitem);
                    },
                    error: function (Result) {
                        //  alert("Error 16");
                    }
                });
                return false;
            }
            function parameterListCancel() {
                document.getElementById("parameterList").style.visibility = "hidden";
            }

            function minMaxParameterClick() {
                document.getElementById("parameterList").style.visibility = "visible";
                $('#parameterList').css('top', '66px');
                $('#parameterList').css('left', 'unset');
                $('#hdnMinMaxAvgGridClick').val("smallgrid");
            }

            $('#chkMinMaxParameterList td').change(function () {
                var CHK = document.getElementById("chkMinMaxParameterList");
                var checkbox = CHK.getElementsByTagName("input");
                var label = CHK.getElementsByTagName("label");
                var counter = 0;

                if ($(this).children()[1].innerHTML == "Select All") {
                    if (checkbox[0].checked == true) {
                        for (var i = 1; i < checkbox.length; i++) {
                            checkbox[i].checked = true;
                        }
                    }
                    else {
                        for (var i = 1; i < checkbox.length; i++) {
                            checkbox[i].checked = false;
                        }
                    }
                }
                if ($(this).children()[0].checked == false) {
                    checkbox[0].checked = false;
                }

            });
            function clearClick() {

                for (var i = 0; i < $('#btnContainer').children().length-1; i++) {
                    if (i == 0) {
                        $('#btnContainer').children()[i].classList.remove("inactive");
                        $('#btnContainer').children()[i].setAttribute("class", "atkBtns active");
                    }
                    else {
                        console.log("Enter not 0 =" + i);
                        $('#btnContainer').children()[i].setAttribute("class", "atkBtns inactive");
                        $('#btnContainer').children()[i].classList.remove("active");
                    }
                }
             
                return true;
            }
            $('#ddlQueryList').change(function () {
                $('#txtQuery').val($('#ddlQueryList').val());
            });
            $('#txtGraphParams2').change(function () {
                bindGraph2();
                minMaxAvgGridActivation();
            });
            $('#txtGraphParams3').change(function () {
                bindGraph3();
                minMaxAvgGridActivation();
            });
            $('#txtGraphParams4').change(function () {
                bindGraph4();
                minMaxAvgGridActivation();
            });
            $('#txtGraphParams5').change(function () {
                bindGraph5();
                minMaxAvgGridActivation();
            });
            function expandClick() {                console.log("HiddenCharts  sys" + $('#hdnChartsIcon').val());                if ($('#hdnChartsIcon').val() == "") {                    $('#hdnChartsIcon').val("");                    document.getElementById('expand').classList.remove('charts');                    // lnk.classList.add('glyphicon-chevron-left');                    document.getElementById('expand').classList.remove('glyphicon-align-right');                    document.getElementById('expand').classList.add('glyphicon-list-alt');                    document.getElementById('dataSection').style.visibility = 'hidden';                    $('#graghSection').addClass('expand');                    document.getElementById('atkGraphs').style.display = 'block';                    $('#atkGraphs').css("left", $('#graphDiv1').width() + 100);                }                else {                    // lnk.classList.remove('glyphicon-chevron-left');                    $('#hdnChartsIcon').val("charts");                    document.getElementById('expand').classList.add('charts');                    document.getElementById('expand').classList.remove('glyphicon-list-alt');                    document.getElementById('expand').classList.add('glyphicon-align-right');                    document.getElementById('dataSection').style.visibility = 'visible';                    $('#graghSection').removeClass('expand');                    document.getElementById('atkGraphs').style.display = 'none';                }            }
            $('#ddlMultiDownID').change(function () {                var whereCondition = "";                var query;                if ($('#txtQuery').val().includes('where') == true) {                    whereCondition = $('#txtQuery').val().split('where')[1];                    if ($("#ddlMultiDownID").find("option").length == $(this).val().length) {                        query = "select * from SystemDocTransaction where " + whereCondition.trim();                    }                    else {                        query = "select " + $(this).val() + " from SystemDocTransaction where " + whereCondition.trim();                    }                }                else {                    if ($("#ddlMultiDownID").find("option").length == $(this).val().length) {                        query = "select * from SystemDocTransaction";                    }                    else {                        query = "select " + $(this).val() + " from SystemDocTransaction";                    }                }                $('#txtQuery').val(query);            });

            $(window).resize(function () {
                var Height = $(window).height() - ($('#btnContainer').height() + 160 + 220);
                $('#gridContainer').css('height', Height);
                var minmaxHeight = (($(window).height() - 180) / 2);
                $('#MinMaxContainer').css('height', minmaxHeight);
                $('#gcontainer').css('height', minmaxHeight - 50);
                $('#atkGraphs').css('width', $(window).width() - $('#graphDiv1').width() - $('#sidebar').width() - 200);
                $('#atkGraphs').css("left", $('#graphDiv1').width() + 100);
                $('.graphDiv').css('height', minmaxHeight - 40);
            });

            $('#txtGraphParams1').change(function () {
                bindGraph();
                minMaxAvgGridActivation();
            });
            $('.atkBtns').click(function () {

                $(this).addClass("active");
                $(this).removeClass("inactive");
                $(this).siblings().removeClass("active");
                $(this).siblings().addClass("inactive");
            });
            $("[id*=gvnested] tr td").click(function () {
                var index = $(this).index();
                if (index == 0) {
                    var sDocId = $(this).text();
                    $("#txtSdocID").val(sDocId);
                    //navigator.clipboard.writeText(sDocId);
                    var copyGfGText = document.getElementById("txtSdocID");
                    copyGfGText.select();
                    document.execCommand('copy');
                }
            });
            $("[id*=gvNormalSDocbind] tr td").click(function () {
                var index = $(this).index();
                if (index == 0) {
                    var sDocId = $(this).text();
                    $("#txtSdocID").val(sDocId);
                    //navigator.clipboard.writeText(sDocId);
                    var copyGfGText = document.getElementById("txtSdocID");
                    copyGfGText.select();
                    document.execCommand('copy');
                }
            });
            function executeClick() {
                for (var i = 0; i < $('#btnContainer').children().length; i++) {
                    if ($('#btnContainer').children()[i].classList.contains("active")) {
                        if ($('#btnContainer').children()[i].id.trim() == "allInfoBtn") {
                            $('#hdnActiveBtn').val("allinformation");
                        } else if ($('#btnContainer').children()[i].id.trim() == "systemDocBtn") {
                            $('#hdnActiveBtn').val("systemdoc");
                        }
                        else if ($('#btnContainer').children()[i].id.trim() == "machineToolBtn") {
                            $('#hdnActiveBtn').val("machinetool");
                        }
                        else if ($('#btnContainer').children()[i].id.trim() == "wheelBtn") {
                            $('#hdnActiveBtn').val("wheel");
                        }
                        else if ($('#btnContainer').children()[i].id.trim() == "workPieceBtn") {
                            $('#hdnActiveBtn').val("workpiece");
                        }
                        else if ($('#btnContainer').children()[i].id.trim() == "opeParBtn") {
                            $('#hdnActiveBtn').val("operational");
                        }
                        else if ($('#btnContainer').children()[i].id.trim() == "targetQlyBtn") {
                            $('#hdnActiveBtn').val("quality");
                        }
                    }
                }

                $('#hdnSearchToggle').val($('#searchIcon').text());
                if ($('#searchIcon').text() == "Parameter") {
                    document.getElementById('ddlSdocPlungeCat').style.display = 'none';
                    document.getElementById('ddlSdocPlungeCatValues').style.display = 'none';
                    document.getElementById('txtParameternew').style.display = 'inline-block';
                    document.getElementById('ddlParamValue').style.display = 'inline-block';
                }
                else {
                    document.getElementById('ddlSdocPlungeCat').style.display = 'inline-block';
                    document.getElementById('ddlSdocPlungeCatValues').style.display = 'inline-block';
                    document.getElementById('txtParameternew').style.display = 'none';
                    document.getElementById('ddlParamValue').style.display = 'none';
                }
                if ($("#txtQuery").val() == "") {
                   $('[id*=myWarningModal]').modal('show');
                   //  document.getElementById("myWarningModal").style.visibility = 'visible';
                    $("#warningmessageText").text("Some Query is required.");
                    return false;
                }
                else {
                    $.blockUI({ message: '<img src="Images/ajax-loader (1).gif"  style="display: inline-block;vertical-align: text-bottom"/>&nbsp;&nbsp;<h2 style="display: inline-block;color:#524b4b;margin:20px 0px;">Just a moment...</h2>' });
                    setTimeout($.unblockUI, 1000);
                    return true;
                }

            }
            function openErrorModal(msg) {
                $('[id*=myErrorModal]').modal('show');
                $("#errormessageText").text(msg);
            };
            function bindGraph() {
                var xitems = [];
                var yitems = [];

                //var xaxis = $('#ddlGraphParam option:selected').text();
                //$('#lblMean').text($('#ddlGraphParam').val())
                 var xaxis = $("#txtGraphParams1").val();
            var optionlist = $("#ddlGraphParams1 option");
            var hiddenlist = $("#ddlGraphParams1 input");
            getSelectedParameterAvgValue(optionlist,hiddenlist,xaxis,"lblMean");
                var value1 = [];
                var frequency = [];
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ApplicationToolKit.aspx/getDDLParamValueFrequency") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{param:"' + xaxis + '",querytxt:"' + $('#txtQuery').val() + '"}',
                    dataType: "json",
                    success: function (response) {

                        var dataitem = response.d;

                        for (var i = 0; i < dataitem.length; i++) {
                            value1[i] = dataitem[i].Value;
                            if (value1[i] == "") {
                                value1[i] = 0;
                            }
                            else {
                                value1[i] = dataitem[i].Value;
                            }
                            frequency[i] = parseFloat(dataitem[i].Text);

                            var serie = new Array(value1[i], frequency[i]);
                            yitems.push(serie);

                            var serie1 = new Array(value1[i]);
                            xitems.push(serie1);

                            console.log("value = " + value1[i] + "  Fre=" + frequency[i]);
                        }
                    },
                    error: function (Result) {
                        // alert("Error 17");
                    }
                });
                //var processedData = [];;                //for (var i = 0; i < yitems.length; i++) {                //    processedData.push({                //        y: value1[i],                //        frequency: frequency[i]                //    })                //}
                console.log("data" + value1[0] + "" + frequency[0]);

                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'gcontainer',
                        type: 'column'
                        //options3d: {
                        //    enabled: true,
                        //    alpha: 15,
                        //    beta: 15,
                        //    depth: 50,
                        //    viewDistance: 25
                        //}
                    },
                    title: {
                        text: xaxis
                    },
                    credits: {
                        enabled: false
                    },
                    exporting: {
                    enabled: true
                },
                navigation: {
                    buttonOptions: {
                        align: 'top'
                    }
                },
                    tooltip: {                        //pointFormat: '{series.name}: <b>{point.frequency}</b>'                        formatter: function () {                            //return '<b>' + this.point.y + ':</b> ' + this.point.xitems;                            return '<b>Frequency: ' + this.point.y;                        }                    },
                    yAxis: {
                        allowDecimals: true,
                        title: {
                            text: 'Frequency'
                        }
                    },
                    xAxis: {
                        allowDecimals: false,
                        categories: xitems,
                        title: {
                            text: xaxis
                        }
                    }
                    ,
                    //subtitle: {
                    //    text: 'Test options by dragging the sliders below'
                    //},
                    plotOptions: {
                        column: {
                            depth: 25
                        }
                    },
                    series: [{
                        color: '#9594e3',
                        // name: 'Frequency' + frequency,
                        //data: processedData,
                        //   name: ["sd","asd"]
                        data: yitems,
                        showInLegend: false,
                        pointPadding: -0.3
                    }]
                });
            }
            function bindGraph2() {
                var xitems2 = [];
                var yitems2 = [];
                //var param = $('#ddlGraphParam2 option:selected').text();
                //$('#lblMean2').text($('#ddlGraphParam2').val());
                 var param = $("#txtGraphParams2").val();
            var optionlist = $("#ddlGraphParams2 option");
            var hiddenlist = $("#ddlGraphParams2 input");
            getSelectedParameterAvgValue(optionlist,hiddenlist,param,"lblMean2");
                var value1 = [];
                var frequency = [];
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ApplicationToolKit.aspx/getDDLParamValueFrequency") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{param:"' + param + '",querytxt:"' + $('#txtQuery').val() + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                        for (var i = 0; i < dataitem.length; i++) {
                            value1[i] = dataitem[i].Value;
                            if (value1[i] == "") {
                                value1[i] = 0;
                            }
                            else {
                                value1[i] = dataitem[i].Value;
                            }
                            frequency[i] = parseFloat(dataitem[i].Text);

                            var serie = new Array(value1[i], frequency[i]);
                            yitems2.push(serie);

                            var serie1 = new Array(value1[i]);
                            xitems2.push(serie1);
                            console.log("value = " + value1[i] + "  Fre=" + frequency[i]);
                        }
                    },
                    error: function (Result) {
                        // alert("Error 18");
                    }
                });
                //var processedData = [];                //for (var i = 0; i < value1.length; i++) {                //    processedData.push({                //        y: value1[i],                //        frequency: frequency[i]                //    })                //}
                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'graph2',
                        type: 'column'
                        //options3d: {
                        //    enabled: true,
                        //    alpha: 15,
                        //    beta: 15,
                        //    depth: 50,
                        //    viewDistance: 25
                        //}
                    },
                    title: {
                        text: param
                    },
                    credits: {
                        enabled: false
                    },
                    exporting: {
                    enabled: true
                },
                navigation: {
                    buttonOptions: {
                        align: 'top'
                    }
                },
                    tooltip: {                        //pointFormat: '{series.name}: <b>{point.frequency}</b>'                        formatter: function () {                            //  return '<b>' + this.point.y + ':</b> ' + this.point.frequency;                            return '<b>Frequency: ' + this.point.y;                        }                    },
                    yAxis: {
                        allowDecimals: false,
                        title: {
                            text: 'Frequency'
                        }
                    },
                    xAxis: {
                        allowDecimals: false,
                        categories: xitems2,
                        title: {
                            text: param
                        }
                    },
                    //subtitle: {
                    //    text: 'Test options by dragging the sliders below'
                    //},
                    plotOptions: {
                        column: {
                            depth: 25
                        }
                    },
                    series: [{
                        color: '#9594e3',
                        // name: 'Frequency',
                        data: yitems2,
                        showInLegend: false,
                        pointPadding: -0.3
                    }]
                });
            }
            function bindGraph3() {
                var xitems3 = [];
                var yitems3 = [];
                //var param = $('#ddlGraphParam3 option:selected').text();
                //$('#lblMean3').text($('#ddlGraphParam3').val());
                var param = $("#txtGraphParams3").val();
            var optionlist = $("#ddlGraphParams3 option");
            var hiddenlist = $("#ddlGraphParams3 input");
            getSelectedParameterAvgValue(optionlist,hiddenlist,param,"lblMean3");
                var value1 = [];
                var frequency = [];
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ApplicationToolKit.aspx/getDDLParamValueFrequency") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{param:"' + param + '",querytxt:"' + $('#txtQuery').val() + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                        for (var i = 0; i < dataitem.length; i++) {
                            value1[i] = dataitem[i].Value;
                            if (value1[i] == "") {
                                value1[i] = 0;
                            }
                            else {
                                value1[i] = dataitem[i].Value;
                            }
                            frequency[i] = parseFloat(dataitem[i].Text);
                            var serie = new Array(value1[i], frequency[i]);
                            yitems3.push(serie);

                            var serie1 = new Array(value1[i]);
                            xitems3.push(serie1);
                            console.log("value = " + value1[i] + "  Fre=" + frequency[i]);
                        }
                    },
                    error: function (Result) {
                        //alert("Error 19");
                    }
                });
                var processedData = [];                for (var i = 0; i < value1.length; i++) {                    processedData.push({                        y: value1[i],                        frequency: frequency[i]                    })                }
                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'graph3',
                        type: 'column'
                        //options3d: {
                        //    enabled: true,
                        //    alpha: 15,
                        //    beta: 15,
                        //    depth: 50,
                        //    viewDistance: 25
                        //}
                    },
                    title: {
                        text: param
                    },
                    credits: {
                        enabled: false
                    },
                    exporting: {
                    enabled: true
                },
                navigation: {
                    buttonOptions: {
                        align: 'top'
                    }
                },
                    tooltip: {                        //pointFormat: '{series.name}: <b>{point.frequency}</b>'                        formatter: function () {                            //return '<b>' + this.point.y + ':</b> ' + this.point.frequency;                            return '<b>Frequency: ' + this.point.y;                        }                    },
                    yAxis: {
                        allowDecimals: false,
                        title: {
                            text: 'Frequency'
                        }
                    },
                    xAxis: {
                        allowDecimals: false,
                        categories: xitems3,
                        title: {
                            text: param
                        }
                    },
                    //subtitle: {
                    //    text: 'Test options by dragging the sliders below'
                    //},
                    plotOptions: {
                        column: {
                            depth: 25
                        }
                    },
                    series: [{
                        color: '#9594e3',
                        name: 'Frequency',
                        data: yitems3,
                        showInLegend: false,
                        pointPadding: -0.3
                    }]
                });
            }
            function bindGraph4() {
                var xitems4 = [];
                var yitems4 = [];
                //var param = $('#ddlGraphParam4 option:selected').text();
                //$('#lblMean4').text($('#ddlGraphParam4').val());
                 var param = $("#txtGraphParams4").val();
            var optionlist = $("#ddlGraphParams4 option");
            var hiddenlist = $("#ddlGraphParams4 input");
                getSelectedParameterAvgValue(optionlist, hiddenlist, param, "lblMean4");
                var value1 = [];
                var frequency = [];
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ApplicationToolKit.aspx/getDDLParamValueFrequency") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{param:"' + param + '",querytxt:"' + $('#txtQuery').val() + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                        for (var i = 0; i < dataitem.length; i++) {
                            value1[i] = dataitem[i].Value;
                            if (value1[i] == "") {
                                value1[i] = 0;
                            }
                            else {
                                value1[i] = dataitem[i].Value;
                            }
                            frequency[i] = parseFloat(dataitem[i].Text);

                            var serie = new Array(value1[i], frequency[i]);
                            yitems4.push(serie);

                            var serie1 = new Array(value1[i]);
                            xitems4.push(serie1);

                            console.log("value = " + value1[i] + "  Fre=" + frequency[i]);
                        }
                    },
                    error: function (Result) {
                        //  alert("Error 20");
                    }
                });
                var processedData = [];                for (var i = 0; i < value1.length; i++) {                    processedData.push({                        y: value1[i],                        frequency: frequency[i]                    })                }
                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'graph4',
                        type: 'column'
                        //options3d: {
                        //    enabled: true,
                        //    alpha: 15,
                        //    beta: 15,
                        //    depth: 50,
                        //    viewDistance: 25
                        //}
                    },
                    title: {
                        text: param
                    },
                    credits: {
                        enabled: false
                    },
                    exporting: {
                    enabled: true
                },
                navigation: {
                    buttonOptions: {
                        align: 'top'
                    }
                },
                    tooltip: {                        //pointFormat: '{series.name}: <b>{point.frequency}</b>'                        formatter: function () {                            //  return '<b>' + this.point.y + ':</b> ' + this.point.frequency;                            return '<b>Frequency: ' + this.point.y;                        }                    },
                    yAxis: {
                        allowDecimals: false,

                        title: {
                            text: 'Frequency'
                        }
                    },
                    xAxis: {
                        allowDecimals: false,
                        categories: xitems4,
                        title: {
                            text: param
                        }
                    },
                    //subtitle: {
                    //    text: 'Test options by dragging the sliders below'
                    //},
                    plotOptions: {
                        column: {
                            depth: 25
                        }
                    },
                    series: [{
                        color: '#9594e3',
                        name: 'Frequency',
                        data: yitems4,
                        showInLegend: false,
                        pointPadding: -0.3
                    }]
                });
            }
            function bindGraph5() {
                var xitems5 = [];
                var yitems5 = [];
                //var param = $('#ddlGraphParam5 option:selected').text();
                //$('#lblMean5').text($('#ddlGraphParam5').val());
                      var param = $("#txtGraphParams5").val();
            var optionlist = $("#ddlGraphParams5 option");
            var hiddenlist = $("#ddlGraphParams5 input");
                getSelectedParameterAvgValue(optionlist, hiddenlist, param, "lblMean5");
                var value1 = [];
                var frequency = [];
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("ApplicationToolKit.aspx/getDDLParamValueFrequency") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{param:"' + param + '",querytxt:"' + $('#txtQuery').val() + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                        for (var i = 0; i < dataitem.length; i++) {
                            value1[i] = dataitem[i].Value;
                            if (value1[i] == "") {
                                value1[i] = 0;
                            }
                            else {
                                value1[i] = dataitem[i].Value;
                            }
                            frequency[i] = parseFloat(dataitem[i].Text);

                            var serie = new Array(value1[i], frequency[i]);
                            yitems5.push(serie);

                            var serie1 = new Array(value1[i]);
                            xitems5.push(serie1);
                            console.log("value = " + value1[i] + "  Fre=" + frequency[i]);
                        }
                    },
                    error: function (Result) {
                        //  alert("Error 21");
                    }
                });
                var processedData = [];                for (var i = 0; i < value1.length; i++) {                    processedData.push({                        y: value1[i],                        frequency: frequency[i]                    })                }
                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'graph5',
                        type: 'column'
                        //options3d: {
                        //    enabled: true,
                        //    alpha: 15,
                        //    beta: 15,
                        //    depth: 50,
                        //    viewDistance: 25
                        //}
                    },
                    title: {
                        text: param
                    },
                    credits: {
                        enabled: false
                    },
                    exporting: {
                    enabled: true
                },
                navigation: {
                    buttonOptions: {
                        align: 'top'
                    }
                },
                    tooltip: {                        //pointFormat: '{series.name}: <b>{point.frequency}</b>'                        formatter: function () {                            // return '<b>' + this.point.y + ':</b> ' + this.point.frequency;                            return '<b>Frequency: ' + this.point.y;                        }                    },
                    yAxis: {
                        allowDecimals: false,
                        title: {
                            text: 'Frequency'
                        }
                    },
                    xAxis: {
                        allowDecimals: false,
                        categories: xitems5,
                        title: {
                            text: param
                        }
                    },
                    //subtitle: {
                    //    text: 'Test options by dragging the sliders below'
                    //},
                    plotOptions: {
                        column: {
                            depth: 25
                        }
                    },
                    series: [{
                        color: '#9594e3',
                        name: 'Frequency',
                        data: yitems5,
                        showInLegend: false,
                        pointPadding: -0.3
                    }]
                });
            }

        });
    </script>
</asp:Content>
