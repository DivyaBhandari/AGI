<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="OutputModules.aspx.cs" Inherits="AGISoftware.OutputModules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/HighChartV8/data.js"></script>
    <script src="Scripts/HighChartV8/drilldown.js"></script>
    <script src="Scripts/Canvg/canvg.js"></script>
    <%--    <script src="Scripts/HighChartV8/highcharts-custom.src.js"></script>--%>
    <%-- <script src="Scripts/HighChart/exporting.js"></script>--%>

    <%--<script src="Scripts/Canvg/canvg.js"></script>--%>
    <%--<script src="Scripts/C2I/canvas2image.js"></script>--%>
    <script src="Scripts/Canvas2Image/canvas2image.js"></script>
    <script src="Scripts/Html2Canvas/html2canvas.js"></script>

    <style>
        .derivedGrid tr th {
            white-space: nowrap;
        }

            .derivedGrid tr th:first-child {
                position: sticky;
                left: 0px;
                white-space: nowrap;
            }

        .derivedGrid tr td:first-child {
            position: sticky;
            left: 0px;
            white-space: nowrap;
        }

        #inputFieldsContainer {
            display: grid;
            /*grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));*/
            grid-template-columns: 1fr 1fr;
            grid-column-gap: 2px;
            grid-row-gap: 2px;
        }

            #inputFieldsContainer > div {
                display: flex;
                align-items: center;
            }

                #inputFieldsContainer > div > span {
                    min-width: 250px;
                    /*max-width: 250px;*/
                    white-space: nowrap;
                    /*overflow: hidden;*/
                    text-overflow: ellipsis;
                    /*max-width: 0px;*/
                }

        /*@media (max-width : 1448px) {
            #inputFieldsContainer > div > span {
                min-width: 150px;
                max-width: 150px;
                white-space: nowrap;
               
            }
        }*/

        .inputFieldsContainer {
            display: grid;
            /*grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));*/
            grid-template-columns: 1fr 1fr;
            grid-column-gap: 2px;
            grid-row-gap: 2px;
        }

            .inputFieldsContainer > div {
                display: flex;
                align-items: center;
            }

                .inputFieldsContainer > div > span {
                    min-width: 250px;
                    /*max-width: 250px;*/
                    white-space: nowrap;
                    /*overflow: hidden;*/
                    text-overflow: ellipsis;
                    /*max-width: 0px;*/
                }



        #gvGeneralInfo tr th:nth-child(2), #gvGeneralInfo tr td:nth-child(2), #gvGeneralInfo tr th:nth-child(3), #gvGeneralInfo tr td:nth-child(3) {
            display: none;
        }

        .gridColumnFix tr td:first-child, .gridColumnFix tr th:first-child {
            position: sticky;
            left: 0px;
        }

        #topBtn {
            display: none;
            position: fixed;
            bottom: 70px;
            right: 30px;
            z-index: 99;
            font-size: 15px;
            border: none;
            outline: none;
            background-color: #e8e8ea;
            /*#70ccc9*/
            color: #87878a;
            cursor: pointer;
            padding: 5px;
            border-radius: 4px;
            animation: blinkingText 3.0s infinite;
        }

            #topBtn:hover {
                background-color: #555;
            }

        @keyframes blinkingText {
            0% {
                background-color: #e8e8ea;
            }

            49% {
                background-color: #d9dade;
            }

            50% {
                background-color: #d9dade;
            }
            /*99% {                color: transparent;            }*/
            100% {
                background-color: #e8e8ea;
            }
        }

        .SdocCs {
            color: orangered;
            background-color: white;
            font-size: 16px;
            text-align: center;
            width: 100%;
        }

        .QlyTbl {
            display: inline;
            margin-right: 1%;
            margin-bottom: 2%;
        }

        #gvGeneralInfo {
            width: 100%;
        }

            #gvGeneralInfo th {
                border: none;
                min-width: 200px;
            }

        div.dd_chk_drop {
            top: 32px;
        }

        div.dd_chk_select {
            height: auto;
            padding: 6px 12px;
            color: #555;
        }

            div.dd_chk_select div#caption {
                height: auto
            }

        #gnrlInfoItem:hover {
            cursor: pointer;
        }

        svg {
            width: 100%;
        }

        #chkParameter tr td {
            border-bottom: none;
        }

        #chkParameter label {
            font-weight: unset;
        }

        #chkFilterQlyParam tr td {
            border-bottom: none;
        }

        #chkFilterQlyParam label {
            font-weight: unset;
        }

        .row {
            margin-top: 1%;
        }

        .subheader {
            color: #4e4949;
            margin: 5px 0px;
            text-align: center;
            font-size: 20px;
            background-color: #d1f2d0;
            border-radius: 3px;
            padding: 2px;
        }

        .Sdoc {
            color: orangered;
            text-align: center;
        }

        #powerTbl tr td {
            padding: 2px 6px;
            margin-top: 5px;
            color: #454444;
            font-size: 15px;
            z-index: unset;
            border: none;
            border-bottom: 1px solid #f3f2f5;
        }

        .opTable {
            box-shadow: 2px 2px 8px 2px #efe7e7;
        }

            .opTable tr {
                background-color: white;
            }

                .opTable tr th {
                    color: white;
                    background-color: #edeef5;
                    font-size: 16px;
                    padding: 0px 6px;
                    color: #87878a;
                    border: none;
                }

                .opTable tr td {
                    padding: 0px 6px;
                    color: #454444;
                    font-size: 15px;
                    border: none;
                    border-bottom: 1px solid #f3f2f5;
                    background-color: white;
                }

        #OutputModules {
            background-color: white;
        }

            #OutputModules a, #OutputModules svg {
                color: brown;
            }

        #genInfoContainer tr:hover, #genInfoContainer tr:hover td, .tblhover tr:hover, .tblhover tr:hover td {
            background-color: #f1f3f4 !important;
        }
        .redColor {
            color: red;
        }
        .blackColor{
           
        }
    </style>

    <asp:HiddenField runat="server" ID="hfImageData" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnTotalCycleTimeGraph" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdActualTimeGraph" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnCalculatedTimeGraph" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnCalcParamGraph" ClientIDMode="Static" />
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-1 col-lg-1 col-md-1"></div>
            <div class="col-sm-10 col-lg-10 col-md-10">
                <%-- <asp:UpdatePanel runat="server">
                   <ContentTemplate>--%>
                <asp:UpdatePanel runat="server" ID="ouputmoduleUpdate" ClientIDMode="Static">
                    <ContentTemplate>
                        <div style="width: 65%; display: inline-block; float: left">
                            Select SystemDoc
                <asp:DropDownList runat="server" ID="ddlSdocID" CssClass="form-control" Style="display: inline; width: 20%" ClientIDMode="Static" OnSelectedIndexChanged="ddlSdocID_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <%-- <asp:CheckBoxList runat="server" ID="chkPlunges" ClientIDMode="Static" ></asp:CheckBoxList>
            
                <asp:CheckBoxList runat="server" ID="chkSubCategory" ClientIDMode="Static"></asp:CheckBoxList>
                <br />--%>
                            <asp:DropDownCheckBoxes runat="server" ClientIDMode="Static" CssClass="form-control" AddJQueryReference="true" AutoPostBack="true" ID="ddlChkPlunges" UseSelectAllNode="false" OnSelectedIndexChanged="ddlChkPlunges_SelectedIndexChanged">
                                <Style DropDownBoxBoxWidth="200" SelectBoxWidth="20%" />
                                <Texts SelectBoxCaption="Select Plunge" />
                            </asp:DropDownCheckBoxes>

                            <asp:DropDownCheckBoxes runat="server" CssClass="form-control" ClientIDMode="Static" ID="ddlChkSubCatogery" UseSelectAllNode="false">
                                <Style DropDownBoxBoxWidth="200" SelectBoxWidth="30%" />
                                <Texts SelectBoxCaption="Select SubCategory" />
                            </asp:DropDownCheckBoxes>
                        </div>
                        <asp:Button runat="server" ID="viewBtn" Text="View" CssClass="Btns" OnClick="viewBtn_Click" />
                        <%--   <asp:Button runat="server" ID="btnPDF" Text="PDF" CssClass="Btns" OnClientClick="return generatePDF();" />--%>
                        <input type="button" id="btnPDF" value="PDF" class="Btns" onclick="generatePDF();" />
                        <%--  <asp:Button runat="server" ID="btnPDF" Text="PDF" CssClass="Btns" OnClick="btnPDF_Click" />
                         <asp:Button runat="server" ID="CustomePDF" Text="CustomePDF" CssClass="Btns" OnClick="CustomePDF_Click" />--%>
                        <asp:Button runat="server" ID="Export" Text="Excel" CssClass="Btns" OnClick="exportBtn_Click" />
                        <asp:Button runat="server" ID="Comparison" Text="Comparison" CssClass="Btns" OnClick="Comparison_Click" />

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlSdocID" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlChkPlunges" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlChkSubCatogery" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="viewBtn" EventName="Click" />
                        <%--  <asp:PostBackTrigger ControlID="btnPDF" />
                         <asp:PostBackTrigger ControlID="CustomePDF" />--%>
                        <asp:PostBackTrigger ControlID="pdfOK" />
                    </Triggers>
                </asp:UpdatePanel>

                <%--  </ContentTemplate>
               </asp:UpdatePanel>--%>
            </div>
            <div class="col-sm-1 col-lg-1 col-md-1"></div>
        </div>

        <asp:UpdatePanel runat="server">
            <ContentTemplate>


                <div class="row">
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                    <div class="col-sm-10 col-lg-10 col-md-10">
                        <h4 class="subheader">General Information</h4>
                        <div id="genInfoContainer" class="opTable" style="position: relative;">
                            <div style="overflow: auto">
                                <asp:GridView runat="server" ID="gvGeneralInfo" Width="100%" ClientIDMode="Static" Border="0" BorderStyle="None" HeaderStyle-BorderStyle="None" CssClass="gridColumnFix"></asp:GridView>
                            </div>
                            <div class="panel panel-default" id="parameterList" style="visibility: hidden; position: absolute; top: 32px; box-shadow: 2px 2px 8px 2px #efe7e7; z-index: 15; border: 1px solid black">
                                <div class="panel-heading" style="padding: 3px; text-align: center; font-weight: bold; border-bottom: 1px solid black">Filter by Parameters</div>
                                <div class="panel-body" style="height: 300px; width: 400px; overflow: auto; padding: 1px">
                                    <asp:TextBox runat="server" ID="txtsearch" ClientIDMode="Static" CssClass="form-control" onkeyup="search(this,event,'GeneralInfo');" Style="margin: 10px"></asp:TextBox>
                                    <asp:CheckBoxList ID="chkParameter" runat="server" ClientIDMode="Static" Style="color: #454444"></asp:CheckBoxList>
                                </div>
                                <div class="panel-footer" style="padding: 3px; text-align: center; border-top: 1px solid black">
                                    <input type="button" value="Ok" class="btn" runat="server" id="generalInfoOk" onserverclick="generalInfoOk_ServerClick" style="font-size: 15px; background-color: white; color: black; border: 1px solid black; padding: 0px 15px;" />
                                    <input type="button" value="Cancel" class="btn" onclick="parameterListCancel()" style="font-size: 15px; background-color: white; color: black; border: 1px solid black; padding: 0px 15px;" />
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                </div>
                <div class="row">
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                    <div class="col-sm-10 col-lg-10 col-md-10">
                        <h4 class="subheader" onclick="showFilterQualityParameter()">Quality Parameters</h4>
                        <div id="QlyParamContainer" class="opTable" style="position: relative">
                            <asp:ListView runat="server" ID="lvQualityParam" ItemPlaceholderID="QltPlaceholder">
                                <LayoutTemplate>
                                    <div runat="server" style="width: 100%; white-space: nowrap; overflow: auto">

                                        <asp:PlaceHolder runat="server" ID="QltPlaceholder"></asp:PlaceHolder>
                                    </div>

                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div style="display: inline-block; overflow: auto; border: 1px solid silver; vertical-align: top">
                                        <label runat="server" id="SDocId" style="color: orangered; text-align: center; width: 100%; font-size: 16px;"><%# Eval("SdocName") %></label>
                                        <asp:ListView runat="server" ID="lvinnerQly" DataSource='<%# Eval("Values") %>' ItemPlaceholderID="itemPlaceHolder">
                                            <LayoutTemplate>
                                                <table runat="server" class="tblhover" style="width: 100%" border="0">
                                                    <tr>
                                                        <th></th>
                                                        <th colspan="2" style="text-align: center">Target</th>
                                                        <th colspan="2" style="text-align: center">Achieved</th>
                                                    </tr>
                                                    <tr>
                                                        <th onclick="showFilterQualityParameter()" id="qlyParamItem">Item<i class="glyphicon glyphicon-triangle-bottom" style="padding: 2px; font-size: 10px; border: 1px solid silver"></i></th>
                                                        <th>Lower</th>
                                                        <th>Upper</th>
                                                        <th>Lower</th>
                                                        <th>Upper</th>
                                                    </tr>
                                                    <tr id="itemPlaceHolder" runat="server"></tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Qlyparam" runat="server" Text='<%# Eval("Prameter") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblTargetLower" Text='<%# Eval("TargetLower") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblTargetUpper" Text='<%# Eval("TargetUpper") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblActualLower" Text='<%# Eval("ActualLower") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblActualUpper" Text='<%# Eval("ActualUpper") %>'></asp:Label></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div style="background-color: silver; color: black; font-size: 20px; text-align: center">No Data Found</div>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <div class="panel panel-default" id="QualityFilterparameterList" style="visibility: hidden; position: absolute; top: 100px; box-shadow: 2px 2px 8px 2px #efe7e7; z-index: 20; border: 1px solid black">
                                <div class="panel-heading" style="padding: 3px; text-align: center; font-weight: bold; border-bottom: 1px solid black;">Filter by Quality Parameters</div>
                                <div class="panel-body" style="height: 300px; width: 400px; overflow: auto; padding: 1px">
                                    <asp:TextBox runat="server" ID="txtsearchqly" ClientIDMode="Static" CssClass="form-control" onkeyup="search(this,event,'Qly');" Style="margin: 10px"></asp:TextBox>
                                    <asp:CheckBoxList ID="chkFilterQlyParam" CssClass="checkCS" runat="server" ClientIDMode="Static" Style="color: #454444"></asp:CheckBoxList>
                                </div>
                                <div class="panel-footer" style="padding: 3px; text-align: center; border-top: 1px solid black;">
                                    <input type="button" value="Ok" class="btn" runat="server" id="qltFilterOk" onserverclick="qltFilterOk_ServerClick" style="font-size: 15px; background-color: white; color: black; border: 1px solid black; padding: 0px 15px;" />
                                    <input type="button" value="Cancel" class="btn" onclick="filterQlyparameterListCancel()" style="font-size: 15px; background-color: white; color: black; border: 1px solid black; padding: 0px 15px;" />
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                </div>

                <div class="row">
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                    <div class="col-sm-10 col-lg-10 col-md-10">
                        <h4 class="subheader">Derived Parameters</h4>
                        <div id="derivedParamContainer" style="box-shadow: 2px 2px 8px 2px #efe7e7;">
                            <%--  <div style="overflow: auto">--%>
                            <asp:ListView runat="server" ID="lvderivedParameter" ItemPlaceholderID="itemPlaceHolder">
                                <LayoutTemplate>
                                    <%-- <table runat="server" style="width: 100%">
                                        <tr id="itemPlaceHolder" runat="server"></tr>
                                       
                                    </table>--%>
                                    <div runat="server">
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>


                                    <div style="text-align: center">
                                        <asp:Label runat="server" ID="lblSdocId" ClientIDMode="Static" Style="color: orangered; font-size: 16px; font-weight: bold" Text='<%# Eval("SdocName") %>'></asp:Label>
                                    </div>
                                    <div style="background-color: #d1f2d0; color: #4e4949; text-align: center; margin-top: 3px; margin-bottom: 3px; border-radius: 3px">
                                        <span title="">Input Parameters</span>&nbsp;
                                    </div>
                                    <div style="width: 100%; overflow: auto">

                                        <asp:ListView runat="server" ID="lvInputParam" DataSource='<%# Eval("derivedInputParameters") %>'>
                                            <LayoutTemplate>
                                                <table runat="server" id="inputtbl" class="derivedGrid  opTable tblhover" border="0" style="width: 100%;" clientidmode="static">
                                                    <tr>
                                                        <th>Parameter</th>
                                                        <th>Dia (x) (mm</th>
                                                        <th>Stock Diametrically (mm)</th>
                                                        <th>Stock on Face (mm)</th>
                                                        <th>In Feed (mm/min)</th>
                                                        <th>Grinding OD Width (mm) </th>
                                                        <th>Feed angle</th>
                                                        <th>Work Speed (m/min) OD</th>
                                                        <th>Work Speed (m/min) Face</th>
                                                        <th>X-Feed (mm/min)</th>
                                                        <th>Z-Feed (mm/min)</th>
                                                    </tr>
                                                    <tr id="itemPlaceHolder" runat="server"></tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>

                                                    <td>
                                                        <asp:Label runat="server" ID="identifier" Text='<%# Eval("Parameter") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="diameter" Text='<%# Eval("Diameter") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="doc" Text='<%# Eval("DOC") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="TangoFlagOD" Value='<%# Eval("TangoFlagOD") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="face" Text='<%# Eval("StockonFace") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="TangoFlagFace" Value='<%# Eval("TangoFlagFace") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="infeed" Text='<%# Eval("InFeed") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>

                                                    <td>
                                                        <asp:Label runat="server" ID="odwidth" Text='<%# Eval("ODWidth") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="feedangle" Text='<%# Eval("FeedAngle") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="workspeedOD" Text='<%# Eval("WorkSpeedOD") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="workspeedface" Text='<%# Eval("WorkSpeedFace") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                   
                                                    <td>
                                                        <asp:Label runat="server" ID="xfeed" Text='<%# Eval("XFeed") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="zfeed" Text='<%# Eval("ZFeed") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <div style="background-color: silver; color: black; font-size: 20px; text-align: center">No Data Found</div>
                                            </EmptyDataTemplate>
                                        </asp:ListView>

                                    </div>

                                    <div class="inputFieldsContainer" style="margin-top: 10px; overflow: auto">

                                        <div>
                                            <span title="Chip width / thickness ratio">Chip width / thickness ratio</span>&nbsp;
                        <asp:Label runat="server" ID="txtChipwidthratio" AutoCompleteType="Disabled" Style="display: inline-block;" CssClass="" Text='<%# Eval("Chipwidthratio") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Wheel Tilt Angle (Deg)">Wheel Tilt Angle (Deg)</span>&nbsp;
                         <asp:Label runat="server" ID="txtWheeltiltangle" AutoCompleteType="Disabled" Style="display: inline-block;" CssClass="" Text='<%# Eval("Wheeltiltangle") %>'></asp:Label>
                                        </div>
                                    </div>

                                    <div style="background-color: #d1f2d0; color: #4e4949; text-align: center; margin-top: 3px; margin-bottom: 3px; border-radius: 3px">
                                        <span title="Non Grinding Time (sec)">Grinding Time (sec)</span>&nbsp;
                                    </div>

                                    <div id="inputFieldsContainer" style="margin-top: 10px;">

                                        <div>
                                            <span title="Spark Out Time (sec)">Spark Out Time (sec)</span>&nbsp;
                        <asp:Label runat="server" ID="txtSparkOutTime" Style="display: inline-block;" Text='<%# Eval("Sparkouttime") %>' CssClass="  allowDecimal"></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Tango / Relief Time (sec)">Tango / Relief Time (sec)</span>&nbsp;
                         <asp:Label runat="server" ID="txtTangotTime" Style="display: inline-block;" Text='<%# Eval("Targetrelieftime") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Traverse Grinding Time (sec)">Traverse Grinding Time (sec)</span>&nbsp;
                    <asp:Label runat="server" ID="txtFeedGrindTime" Style="display: inline-block;" Text='<%# Eval("TraverseSpeed") %>'></asp:Label>
                                        </div>
                                    </div>

                                    <div style="background-color: #d1f2d0; color: #4e4949; text-align: center; margin-top: 3px; margin-bottom: 3px; border-radius: 3px">
                                        <span title="Non Grinding Time (sec)">Non Grinding Time (sec)</span>&nbsp;
                                    </div>

                                    <div class="inputFieldsContainer">
                                        <div>
                                            <span title="Slide Forward (sec)">Slide Forward (sec)</span>&nbsp;
                    <asp:Label runat="server" ID="txtSlideForward" Style="display: inline-block;" Text='<%# Eval("SlideForward") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Program Read (sec)">Program Read (sec)</span>&nbsp;
                    <asp:Label runat="server" ID="txtPrgmRead" Style="display: inline-block;" Text='<%# Eval("ProgramRead") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Flagging (sec)">Flagging (sec)</span>&nbsp;
                    <asp:Label runat="server" ID="txtFlagging" Style="display: inline-block;" Text='<%# Eval("Flagging") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Slide Return (sec)">Slide Return (sec)</span>&nbsp;
                    <asp:Label runat="server" ID="txtSlideRetuen" Style="display: inline-block;" Text='<%# Eval("SlideReturn") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Manual Loading / Unloading (sec)">Manual Loading / Unloading (sec)</span>&nbsp;
                    <asp:Label runat="server" ID="txtManualLoadUnload"  Text='<%# Eval("ManualLoadingUnloading") %>'  Style="display: inline-block;"></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Auto Loading / Unloading (sec)">Auto Loading / Unloading (sec)</span>&nbsp;
                    <asp:Label runat="server" ID="txtLoadUnload" Style="display: inline-block;" Text='<%# Eval("LoadUnloadTime") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Other (sec)">Other (sec)</span>
                                            <%--<asp:Label runat="server" ID="txtothertimediscription" Text='<%# Eval("OthersTimeDescription") %>'></asp:Label>--%>&nbsp;
                                              
                    <asp:Label runat="server" ID="txtOther" Style="display: inline-block;" Text='<%# Eval("Others") %>'></asp:Label>
                                        </div>

                                    </div>


                                    <div style="background-color: #d1f2d0; color: #4e4949; text-align: center; margin-top: 3px; margin-bottom: 3px; border-radius: 3px;">
                                        <span title="">Calculate Parameters</span>&nbsp;
                                    </div>
                                    <div style="width: 100%; overflow: auto; margin-top: 1%; box-shadow: 2px 2px 8px 2px #efe7e7;">

                                        <asp:ListView runat="server" ID="lvCalculatedPara" ClientIDMode="Static" DataSource='<%# Eval("derivedcalculatedtParameters") %>'>
                                            <LayoutTemplate>
                                                <table runat="server" class="derivedGrid opTable tblhover" border="0" style="width: 100%">
                                                    <tr>
                                                        <th>Parameter</th>
                                                        <th>Radial Depth of Cut (X) (mm/rev)</th>
                                                        <th>Depth of Cut (Z) (mm/rev)</th>
                                                        <th>MRR'(X) (cu.mm/mm/sec)</th>
                                                        <th>MRR'(Z) (cu.mm/mm/sec)</th>
                                                        <%--<th>Total MRR' (cu.mm/sec)</th>--%>
                                                         <th>Total MRR (cu.mm/sec)</th>
                                                        <th>Grit Penetration Depth (X) (μm)</th>
                                                        <th>Grit Penetration Depth (Z) (μm)</th>
                                                        <th>Time (sec)</th>
                                                        <th>Wheel Work Speed Ratio</th>
                                                        <th>Wheel Work RPM Ratio</th>

                                                    </tr>
                                                    <tr id="itemPlaceHolder" runat="server"></tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="parameter" Text='<%# Eval("Parameter") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label>
                                                          <asp:HiddenField runat="server" ID="TangoFlagOD" Value='<%# Eval("TangoFlagOD") %>' />
                                                          <asp:HiddenField runat="server" ID="TangoFlagFace" Value='<%# Eval("TangoFlagFace") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="radialDOCX" Text='<%# Eval("RadialDOCX") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="radialDOCZ" Text='<%# Eval("RadialDOCZ") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="mrrx" Text='<%# Eval("MRRX") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="mrrz" Text='<%# Eval("MRRZ") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                   <%-- <td>
                                                        <asp:Label runat="server" ID="totalmrrx" Text='<%# Eval("TotalMRRX") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>--%>
                                                    <td>
                                                        <asp:Label runat="server" ID="totalmrr" Text='<%# Eval("ToralMRR") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="gpdx" Text='<%# Eval("GritPenetrationDepthX") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="gpdz" Text='<%# Eval("GritPenetrationDepthZ") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="time" Text='<%# Eval("Time") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="workspeedratio" Text='<%# Eval("WorkSpeedRatio") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="workrpmratio" Text='<%# Eval("WorkRPMRatio") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>

                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <div style="background-color: silver; color: black; font-size: 20px; text-align: center">No Data Found</div>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div style="background-color: #d1f2d0; color: #4e4949; text-align: center; margin-top: 3px; margin-bottom: 3px; border-radius: 3px">
                                        <span>Other Calculations</span>&nbsp;
                                    </div>
                                    <div class="inputFieldsContainer" style="margin-top: 10px">
                                        <div>
                                            <span title="Equivalent Dia for OD (De) (mm)">Equivalent Dia for OD (De) (mm)</span>
                                            <asp:Label runat="server" ID="txtEquivalentDia"  Text='<%# Eval("EquivalentDia") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Equivalent Dia Face (mm)">Equivalent Dia Face (mm)</span>
                                            <asp:Label runat="server" ID="txtEquivalentDiaFace" Text='<%# Eval("EquivalentDiaFace") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Cutting Edge Density (/sq.m.)">Cutting Edge Density (/sq.m.)</span>
                                            <asp:Label runat="server" ID="txtCuttingEdge"   Text='<%# Eval("CuttingEdgeDensity") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Spark Out Revolutions (rev)">Spark Out Revolutions (rev)</span>
                                            <asp:Label runat="server" ID="txtSpartOutRev" Text='<%# Eval("SparkOutRevolutions") %>'  ></asp:Label>
                                        </div>

                                    </div>
                                    <div style="background-color: #d1f2d0; color: #4e4949; text-align: center; margin-top: 3px; margin-bottom: 3px; border-radius: 3px">
                                        <span>Time Calculations</span>&nbsp;
                                    </div>
                                    <div class="inputFieldsContainer" style="overflow: auto">
                                        <div>
                                            <span title="Grinding Cycle Time (sec)">Grinding Time (sec)</span>
                                            <asp:Label runat="server" ID="txtGrindcycletime"  Text='<%# Eval("GrindingCycletime") %>'  ></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Non Grinding Time (sec)">Non Grinding Time (sec)</span>
                                            <asp:Label runat="server" ID="txtNongrindingtime" Text='<%# Eval("NongrindingCycleTime") %>' ></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Total Grinding Time (sec)">Total Grinding Time (sec)</span>
                                            <asp:Label runat="server" ID="txtTotalGrindingTime" Text='<%# Eval("TotalGrindingTime") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Total Cycle Time (sec)">Total Cycle Time (sec)</span>
                                            <asp:Label runat="server" ID="txtTotalCycleTime" Text='<%# Eval("TotalCycletime") %>'></asp:Label>
                                        </div>
                                        <div>
                                            <span title="Floor to Floor Time (sec)">Floor to Floor Time (sec)</span>
                                            <asp:Label runat="server" ID="txtFloortoFloor" Text='<%# Eval("FloorToFloor") %>' ></asp:Label>
                                        </div>
                                        <%--<div>
                                            <asp:Label runat="server" ID="txtRemarks" Style="width: 300px; margin-bottom: 10px" ></asp:Label>
                                        </div>--%>
                                    </div>

                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div style="background-color: silver; color: black; font-size: 20px; text-align: center">No Data Found</div>
                                </EmptyDataTemplate>
                            </asp:ListView>

                        </div>
                    </div>
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                </div>

                <div class="row">
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                    <div class="col-sm-10 col-lg-10 col-md-10">
                        
                        <asp:DropDownCheckBoxes runat="server" ClientIDMode="Static" CssClass="form-control" AddJQueryReference="true" ID="dcSdoclist" UseSelectAllNode="true">
                            <Style DropDownBoxBoxWidth="200" SelectBoxWidth="30%" />
                        </asp:DropDownCheckBoxes>
                        <input type="button" id="btnSDocGraph" class="Btns" onclick="btnSDocGraphClick()" value="OK" style="display: inline" />
                    </div>
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                </div>
                <div class="row" id="SdoctotalCycleTimeConatiner"></div>
                <div class="row">
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                    <div class="col-sm-5 col-lg-5 col-md-5">
                        <div id="SdocDrillableCycleTimeConatiner" style="width: 700px; height: 400px"></div>
                        <%--style="width: 80%; margin: auto"--%>
                    </div>
                    <div class="col-sm-5 col-lg-5 col-md-5">
                        <div id="SdocActualGrindingAndNonGrindingTimeConatiner" style="width: 800px; height: 400px; overflow: unset"></div>
                    </div>
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                </div>

                <div class="row" id="SdocgrindingTimeContainer"></div>
                <div class="row" id="SdocnonGrindingTimeConatiner"></div>

                <div style="display: none">
                    <canvas id="mycanvas"></canvas>
                    <canvas id="totalcycletimecanvas" style="width: 800px; height: 800px"></canvas>
                    <canvas id="actualtimecanvas" style="width: 800px; height: 800px"></canvas>
                    <canvas id="calculatedtimecanvas"></canvas>

                </div>
                <%--  <div class="row" id="totalCycleTimeConatiner">
                </div>
                <div class="row" id="grindingTimeConatiner">
                </div>
                <div class="row" id="nonGrindingTimeConatiner">
                </div>--%>

                <div id="calcParameter">
                </div>
                <div id="calcParameterCanvas" style="display: none"></div>
                <div class="row">
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                    <div class="col-sm-10 col-lg-10 col-md-10">
                        <h4 class="subheader">Inferences from Power Signal</h4>
                        <div id="pwSignalContainer" style="overflow: auto" class="opTable">
                            <asp:GridView runat="server" ID="gvInferenceSignal" Width="100%" CssClass="gridColumnFix tblhover" ClientIDMode="Static" Border="0" BorderStyle="None" HeaderStyle-BorderStyle="None"></asp:GridView>
                        </div>
                    </div>
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                </div>
                <div class="row">
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                    <div class="col-sm-10 col-lg-10 col-md-10">
                        <h4 class="subheader">Images</h4>
                        <asp:ListView runat="server" ID="lvImages" ItemPlaceholderID="placeHolderSdocIamges">
                            <LayoutTemplate>
                                <table style="width: 100%">
                                    <asp:PlaceHolder runat="server" ID="placeHolderSdocIamges" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr style="text-align: center">
                                    <td style="font-size: 16px; color: orangered; font-weight: 700"><%# Eval("SdocName") %>
                                        <asp:HiddenField runat="server" ID="imgSdoc" Value='<%# Eval("SdocName") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListView runat="server" ID="lvImageDetails" ItemPlaceholderID="placeHolderImg" DataSource='<%# Eval("Values") %>'>
                                            <LayoutTemplate>
                                                <div style="width: 100%">
                                                    <asp:PlaceHolder runat="server" ID="placeHolderImg" />
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <%-- <tr>
                                        <td><%# Eval("wpImageName") %></td>
                                         <td> <asp:Image runat="server" Width="100" Height="100" ImageUrl='<%# Eval("wpImagePath") %>' /></td>
                                    </tr>--%>
                                                <div style="width: 24%; display: inline-block; padding: 5px; text-align: center;">
                                                    <asp:HiddenField runat="server" ID="hdnImagePath" Value='<%# Eval("wpImagePath") %>' />
                                                    <label style="font-weight: 400" runat="server" id="imgName"><%# Eval("wpImageName") %></label><br />
                                                    <asp:Image runat="server" Width="90%" Height="200" ID="sdocImages" ImageUrl='<%# Eval("wpImagePath") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                                <tr style="height: 20px;"></tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="viewBtn" EventName="Click" />
                <asp:PostBackTrigger ControlID="Export" />
                <asp:PostBackTrigger ControlID="pdfOK" />
                <%-- <asp:PostBackTrigger ControlID="btnPDF" />
               <asp:PostBackTrigger ControlID="CustomePDF" />--%>
            </Triggers>
        </asp:UpdatePanel>


    </div>
    <button onclick="topFunction()" id="topBtn" title="Go to top" style="border-radius: 20px; padding: 5px 12px"><i class="glyphicon glyphicon-chevron-up"></i></button>

    <%--   <div class="modal fade" id="generatePDFModel" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                    <label style="font-weight: unset">
                        <input type="radio" id="defaultReport" runat="server" name="confirm" value="defaultReport" />
                        <span>Default Report</span>
                    </label>
                    <br />

                    <label style="font-weight: unset">
                        <input type="radio" id="customeReport" runat="server" name="confirm" value="customeReport" />
                        <span>Custome Report</span>
                    </label>
               
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                   
                    <input type="button" runat="server" value="OK" class="btn btn-info" id="pdfOK"  style="background-color: #5D7B9D; color: white" data-dismiss="modal" onserverclick="pdfOK_ServerClick" />
                    <input type="button" value="Cancel" class="btn btn-info" id="Cancel" onclick="Cancelclick()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>--%>

    <div class="panel panel-default" id="generatePDFModel" style="visibility: hidden; position: absolute; top: 0%; left: 38%; box-shadow: 10px 10px 30px 30px #c4c4c4; z-index: 15; border: 2px solid #5D7B9D">
        <div class="panel-heading" style="padding: 8px; text-align: left; font-weight: bold; background-color: #5D7B9D; color: white">Select PDF Type</div>
        <div class="panel-body" style="width: 380px; height: 100px; overflow: auto; padding: 10px 5px 5px 5px">
            <label style="font-weight: unset">
                <input type="radio" id="defaultReport" runat="server" name="confirm" value="defaultReport" />
                <span>Default Report</span>
            </label>
            <br />

            <label style="font-weight: unset">
                <input type="radio" id="customeReport" runat="server" name="confirm" value="customeReport" />
                <span>Custom Report</span>
            </label>
        </div>
        <div class="panel-footer" style="padding: 3px; text-align: right; border-top: 1px solid #5D7B9D;">
            <input type="submit" runat="server" value="OK" class="btn btn-info" id="pdfOK" style="background-color: #5D7B9D; color: white" data-dismiss="modal" onclick="return closepanel()" onserverclick="pdfOK_ServerClick" />
            <input type="button" value="Cancel" class="btn btn-info" id="Cancel" onclick="Cancelclick()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
        </div>
    </div>
    <script>
        //var j183 = jQuery.noConflict();
        // var j331 = jQuery.noConflict();
        $(document).ready(function () {
            //bindTimeGraph();
            btnSDocGraphClick();
            bingMRRGraph();
            $('#gvGeneralInfo th:first-child').empty();            $('#gvGeneralInfo th:first-child').append('&nbsp;Items <i class="glyphicon glyphicon-triangle-bottom" style="padding: 2px; font-size: 10px; border: 1px solid silver"></i>');


        });

        window.onscroll = function () { scrollFunction() };        var topBtn = document.getElementById("topBtn");        function scrollFunction() {            if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {                topBtn.style.display = "block";            } else {                topBtn.style.display = "none";            }        }        function SdocgotoComparisonPage() {            window.location.href = "SDocComparisonPage.aspx";
        }        function topFunction() {            document.body.scrollTop = 0;            document.documentElement.scrollTop = 0;        }

        function search(val, evt, ipmodule) {
            let txtSearch = $(val).val();
            debugger;
            let input;
            let label;
            let td
            if (ipmodule == "GeneralInfo") {
                input = $("#chkParameter input");
                label = $("#chkParameter label");
                td = $("#chkParameter td");
            }
            if (ipmodule == "Qly") {
                input = $("#chkFilterQlyParam input");
                label = $("#chkFilterQlyParam label");
                td = $("#chkFilterQlyParam td");
            }

            for (var i = 0; i < input.length; i++) {
                var chkvalue = label[i].innerText.slice(0, txtSearch.length);
                if (chkvalue.toLowerCase() == txtSearch.toLowerCase()) {
                    td[i].style.display = "block";
                } else {
                    td[i].style.display = "none";
                }
            }
        }

        $('#gvGeneralInfo th:first-child').click(function () {
            document.getElementById("parameterList").style.visibility = 'visible';
        });

        function showFilterQualityParameter() {
            document.getElementById("QualityFilterparameterList").style.visibility = 'visible';
        }

        function generatePDF() {
            document.getElementById("generatePDFModel").style.visibility = 'visible';
            return false;
        }
        function Cancelclick() {
            document.getElementById("generatePDFModel").style.visibility = 'hidden';
        }
        function closepanel() {
            document.getElementById("generatePDFModel").style.visibility = 'hidden';
            return true;
        }

        function filterQlyparameterListCancel() {
            //var CHK = document.getElementById("chkFilterQlyParam");
            //var checkbox = CHK.getElementsByTagName("input");
            //for (var i = 0; i < checkbox.length; i++) {
            //    checkbox[i].checked = false;
            //}
            document.getElementById("QualityFilterparameterList").style.visibility = 'hidden';
        }
        function parameterListSelected() {

            var CHK = document.getElementById("chkParameter");
            var checkbox = CHK.getElementsByTagName("input");
            var colName = "";
            var temp = "";

            for (var i = 1; i < checkbox.length; i++) {
                if (checkbox[i].checked == true) {
                    if (temp == "") {
                        colName = checkbox[i].value;
                    }
                    else {
                        colName = temp + "," + checkbox[i].value;
                    }

                    temp = colName;
                }
            }
            document.getElementById("parameterList").style.visibility = 'hidden';
            alert(colName);
        }

        function parameterListCancel() {
            //var CHK = document.getElementById("chkParameter");
            //var checkbox = CHK.getElementsByTagName("input");
            //for (var i = 0; i < checkbox.length; i++) {
            //    checkbox[i].checked = false;
            //}
            document.getElementById("parameterList").style.visibility = 'hidden';
        }

        $('#chkParameter td').change(function () {

            var CHK = document.getElementById("chkParameter");
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
        $('#chkFilterQlyParam td').change(function () {

            var CHK = document.getElementById("chkFilterQlyParam");
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
        function showParameter() {
            openWarningModal();
        }
        function openWarningModal(msg) {
            //$('[id*=parameterList]').modal('show');
            document.getElementById("parameterList").style.visibility = 'visible';
            $("#warningmessageText").text(msg);
        };

        function btnSDocGraphClick() {
            let label = $("#dcSdoclist label");
            let checkbox = $("#dcSdoclist input");
            let Sdocname = "";
            let Plung = "";
            let SubCategory = "";

            for (let i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    let SDocid = label[i].textContent.replace("SDoc", "").split("_");
                    Sdocname = SDocid[0];
                    Plung += "'" + SDocid[1] + "',";
                    SubCategory += "'" + SDocid[2] + "',";
                }
            }

            Plung = Plung.substring(0, Plung.length - 1);
            SubCategory = SubCategory.substring(0, SubCategory.length - 1);

            if (Sdocname == "" && Plung == "" && SubCategory == "") {
                $('#SdocgrindingTimeContainer').empty();
                $('#SdocnonGrindingTimeConatiner').empty();
                $('#SdocDrillableCycleTimeConatiner').empty();
                $('#SdoctotalCycleTimeConatiner').empty();
                $("[id*=hfImageData]").val(null);
                $("[id*=hdnTotalCycleTimeGraph]").val(null);
                $("[id*=hdActualTimeGraph]").val(null);
                $("[id*=hdnCalculatedTimeGraph]").val(null);
                return;
            }

            var SDocgrindDetails = [];
            var SDocgrindTimeData = [];
            var SDocgrindTimeDataColor = ["#000090", "#8fff6f", "#800001", "#e67312", "#1c1c21", "#056aad"];
            //get SDoc grinding time details
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("OutputModules.aspx/getGrindingTime") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{Sdoc:"' + Sdocname + '",Plunge:"' + Plung + '",SubCat:"' + SubCategory + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    SDocgrindDetails = dataitem;

                },
                error: function (Result) {
                    // alert("Error");
                }
            });

            $('#SdocgrindingTimeContainer').empty();
            var appendString = '<div class="col-sm-1 col-lg-1 col-md-1"></div><div class="col-sm-5 col-lg-5 col-md-5"><h4 class="subheader">Grinding Time</h4><table class="opTable tblhover" style="width: 100%" border="0"><tr><th>Item</th><th>Value</th></tr>';
            let j = 0;

            for (var i = 0; i < SDocgrindDetails.length; i = i + 2) {

                appendString += '<tr><td>' + SDocgrindDetails[i] + '</td><td>' + SDocgrindDetails[i + 1] + '</td></tr>';
                if (SDocgrindDetails[i] == "Roughing Time") {
                    roughingTime = parseFloat(SDocgrindDetails[i + 1]);
                }
                else if (SDocgrindDetails[i] == "Semi-finishing Time") {
                    semiFinishTime = parseFloat(SDocgrindDetails[i + 1]);
                } else if (SDocgrindDetails[i] == "Finishing Time") {
                    finishTime = parseFloat(SDocgrindDetails[i + 1]);
                }

                SDocgrindTimeData[j] = {
                    name: SDocgrindDetails[i], y: parseFloat(SDocgrindDetails[i + 1]), color: SDocgrindTimeDataColor[j]
                };
                j++;
            }

            appendString += '</table></div><div class="col-sm-5 col-lg-5 col-md-5" style="padding-top: 5px"><div id="SdoctimeGraph" style="overflow:unset; width: 1000px; height: 400px"></div></div><div class="col-sm-1 col-lg-1 col-md-1"></div>';
            $('#SdocgrindingTimeContainer').append(appendString);
            var calculatedtime = Highcharts.chart('SdoctimeGraph', {
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 60,
                        beta: 0
                    },
                    //width: 1000,
                    // height: $('#SdoctimeGraph').height()

                },
                title: {
                    text: ''
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
                tooltip: {
                    pointFormat: '<b>{point.y} sec - {point.percentage:.1f}%</b>'

                },
                plotOptions: {
                    pie: {
                        size: '100%',
                        allowPointSelect: true,
                        cursor: 'pointer',
                        depth: 35,
                        dataLabels: {
                            enabled: true,
                            // format: '{point.name}<br/><b>{point.y} sec - {point.percentage:.1f}%</b>'
                            useHTML: true,
                            format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                            style: {
                                color: "black",
                                fontSize: "17px"
                            }

                        },
                        showInLegend: true
                    }
                },
                legend: {
                    useHTML: true,
                    itemStyle: {
                        fontSize: '16px',
                        // font: '10px Trebuchet MS, Verdana, sans-serif'
                    }
                },
                series: [{
                    type: 'pie',
                    //name: 'Browser share',
                    data: SDocgrindTimeData

                }]
            });
            $("#SdoctimeGraph").css('overflow', 'unset');
            var mysvg = calculatedtime.getSVG();
            var c = document.getElementById('calculatedtimecanvas');
            canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
            $("[id*=hdnCalculatedTimeGraph]").val(document.getElementById('calculatedtimecanvas').toDataURL());


            //get non grinding time details
            var SDocnongrindDetails = [];
            var SDocnongrindTimeData = [];
            var SDocnongrindTimeDataColor = ["#000090", "#8fff6f", "#800001", "#e67312", "#1c1c21", "#056aad"];
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("OutputModules.aspx/getNonGrindingTime") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{Sdoc:"' + Sdocname + '",Plunge:"' + Plung + '",SubCat:"' + SubCategory + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    SDocnongrindDetails = dataitem;
                },
                error: function (Result) {
                    // alert("Error");
                }
            });

            $('#SdocnonGrindingTimeConatiner').empty();
            var appendString = '<div class="col-sm-1 col-lg-1 col-md-1"></div><div class="col-sm-5 col-lg-5 col-md-5"><h4 class="subheader">Non Grinding Time</h4><table class="opTable tblhover" style="width: 100%" border="0"><tr><th>Item</th><th>Value</th></tr>';
            let nj = 0;
            for (var i = 0; i < SDocnongrindDetails.length; i = i + 2) {

                appendString += '<tr><td>' + SDocnongrindDetails[i] + '</td><td>' + SDocnongrindDetails[i + 1] + '</td></tr>';

                SDocnongrindTimeData[nj] = {
                    name: SDocnongrindDetails[i], y: parseFloat(SDocnongrindDetails[i + 1]), color: SDocnongrindTimeDataColor[nj]
                };
                nj++;
            }

            appendString += '</table></div><div class="col-sm-5 col-lg-5 col-md-5" style="padding-top: 5px"><div id="SDocnongindtimeGraph" style="overflow:unset; width: 800px; height: 400px"></div></div><div class="col-sm-1 col-lg-1 col-md-1"></div>';
            $('#SdocnonGrindingTimeConatiner').append(appendString);
            console.log("Non grinding " + appendString);
            var nonGrindTimeChart = Highcharts.chart('SDocnongindtimeGraph', {
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 60,
                        beta: 0
                    },
                    //width: 800,
                    //  height: $('#SDocnongindtimeGraph').height()
                },
                title: {
                    text: ''
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
                tooltip: {
                    pointFormat: '<b>{point.y} sec - {point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        size: '100%',
                        allowPointSelect: true,
                        cursor: 'pointer',
                        depth: 35,
                        dataLabels: {
                            enabled: true,
                            //format: '{point.name}<br/><b>{point.y} sec - {point.percentage:.1f}%</b>'
                            useHTML: true,
                            format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                            style: {
                                color: "black",
                                fontSize: "17px"
                            }
                        },
                        showInLegend: true
                    }
                },
                legend: {
                    useHTML: true,
                    itemStyle: {
                        fontSize: '16px',
                        // font: '10px Trebuchet MS, Verdana, sans-serif'
                    }
                },
                series: [{
                    type: 'pie',
                    //name: 'Browser share',
                    data: SDocnongrindTimeData

                }]
            });
            $("#SDocnongindtimeGraph").css('overflow', 'unset');
            //var nonGrindTimesvg = nonGrindTimeChart.getSVG();
            //var cNonGrindTime = document.getElementById('nonGrindTimeCanvas');
            //canvg(cNonGrindTime, nonGrindTimesvg, { ignoreMouse: true, ignoreAnimation: true });
            //Canvas2Image.saveAsPNG(cNonGrindTime, false);
            //$("[id*=hdnNonGrindTime]").val(document.getElementById('nonGrindTimeCanvas').toDataURL());


            //get total cycletime details
            var SDoctotalCycleTimeDetails = [];
            var SDoctotalCycleTimeData = [];
            var SDoctotalCycleTimeDataColor = ["#000090", "#e67312", "#8fff6f", "#800001", "#1c1c21", "#056aad"];
            var SDocGrindingAndNongrindingTimeData = [];
            var SDocGrindingAndNongrindingTimeDataColor = ["#8fff6f", "#e67312", "#800001", "#000090", "#1c1c21", "#056aad"];
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("OutputModules.aspx/getTotalCycleTime") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{Sdoc:"' + Sdocname + '",Plunge:"' + Plung + '",SubCat:"' + SubCategory + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    SDoctotalCycleTimeDetails = dataitem;
                },
                error: function (Result) {
                    // alert("Error");
                }
            });

            $('#SdoctotalCycleTimeConatiner').empty();
            var appendString = '<div class="col-sm-1 col-lg-1 col-md-1"></div><div class="col-sm-5 col-lg-5 col-md-5"><h4 class="subheader">Total Cycle Time</h4><table class="opTable tblhover" style="width: 100%" border="0"><tr><th>Item</th><th>Value</th></tr>';

            let tj = 0;
            let gn = 0;
            for (var i = 0; i < SDoctotalCycleTimeDetails.length; i = i + 2) {

                appendString += '<tr><td>' + SDoctotalCycleTimeDetails[i] + '</td><td>' + SDoctotalCycleTimeDetails[i + 1] + '</td></tr>';

                //if (i == (SDoctotalCycleTimeDetails.length - 6)) {
                //    continue;
                //} else {
                //    SDoctotalCycleTimeData[tj] = {
                //        name: SDoctotalCycleTimeDetails[i], y: parseFloat(SDoctotalCycleTimeDetails[i + 1]), color: SDoctotalCycleTimeDataColor[tj]
                //    };
                //    tj++;
                //}
                if (SDoctotalCycleTimeDetails[i] == "Grinding Time (sec)") {
                    SDoctotalCycleTimeData[tj] = {
                        name: SDoctotalCycleTimeDetails[i], y: parseFloat(SDoctotalCycleTimeDetails[i + 1]), color: SDoctotalCycleTimeDataColor[tj]
                    };
                    tj++;
                }
                if (SDoctotalCycleTimeDetails[i] == "Actual Grinding Time (sec)") {
                    SDoctotalCycleTimeData[tj] = {
                        name: SDoctotalCycleTimeDetails[i], y: parseFloat(SDoctotalCycleTimeDetails[i + 1]), color: SDoctotalCycleTimeDataColor[tj]
                    };
                    tj++;
                    SDocGrindingAndNongrindingTimeData[gn] = {
                        name: SDoctotalCycleTimeDetails[i], y: parseFloat(SDoctotalCycleTimeDetails[i + 1]), color: SDocGrindingAndNongrindingTimeDataColor[gn]
                    };
                    gn++;
                }
                if (SDoctotalCycleTimeDetails[i] == "Non Grinding Time (sec)") {
                    SDocGrindingAndNongrindingTimeData[gn] = {
                        name: SDoctotalCycleTimeDetails[i], y: parseFloat(SDoctotalCycleTimeDetails[i + 1]), color: SDocGrindingAndNongrindingTimeDataColor[gn]
                    };
                    gn++;
                }
            }
            appendString += '</table></div><div class="col-sm-5 col-lg-5 col-md-5" style="padding-top: 5px"><div id="SDoctotalcycletimeGraph" style="overflow: unset; width: 800px; ; height: 400px"></div></div><div class="col-sm-1 col-lg-1 col-md-1"></div>';
            $('#SdoctotalCycleTimeConatiner').append(appendString);

            //bar chart
            //var chart = Highcharts.chart('SDoctotalcycletimeGraph', {
            //    chart: {
            //        type: 'column'
            //    },
            //    title: {
            //        useHTML: true,
            //        text: '<b><h5>Calculated Grinding Time vs Actual Grinding time</h5></b>',
            //        style: {
            //            color: 'green'
            //        }
            //    },
            //    subtitle: {
            //        text: ''
            //    },
            //    accessibility: {
            //        announceNewData: {
            //            enabled: true
            //        }
            //    },
            //    xAxis: {
            //        type: 'category'
            //    },
            //    yAxis: {
            //        title: {
            //            text: ''
            //        }

            //    },
            //    legend: {
            //        enabled: false
            //    },
            //    plotOptions: {
            //        series: {
            //            borderWidth: 0,
            //            dataLabels: {
            //                enabled: true,
            //                format: '{point.y}'
            //            }
            //        }
            //    },

            //    tooltip: {
            //        //headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            //        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}</b><br/>'
            //    },

            //    series: [
            //        {
            //            name: "Browsers",
            //            colorByPoint: true,
            //            data: SDoctotalCycleTimeData
            //        }
            //    ]

            //});
            //var mysvg = chart.getSVG();
            //var c = document.getElementById('mycanvas');
            //canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
            //Canvas2Image.saveAsPNG(c, false);
            //$("[id*=hfImageData]").val(document.getElementById('mycanvas').toDataURL());
            //pie chart

            var totalcycletime = Highcharts.chart('SDoctotalcycletimeGraph', {
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 60,
                        beta: 0
                    },
                    //width: 800,
                    //height: $('#SDoctotalcycletimeGraph').height()
                },
                title: {
                    useHTML: true,
                    // text: '<b><h5>Calcualted Grinding Time v/s Actual Grinding time</h5></b>',
                    text: '<b><h4>Total Cycle Time</h4></b>',
                    style: {
                        color: 'green'
                    }
                },
                credits: {
                    enabled: false
                },
                exporting: {
                    enabled: true
                },
                navigation: {                    buttonOptions: {                        align: 'top'                    }                },
                tooltip: {
                    pointFormat: '<b>{point.y} sec -  {point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        size: '100%',
                        allowPointSelect: true,
                        cursor: 'pointer',
                        depth: 35,
                        dataLabels: {
                            enabled: true,
                            // format: '{point.name}<br/><b>{point.y} sec -  {point.percentage:.1f}%</b>'
                            useHTML: true,
                            format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                            style: {
                                color: "black",
                                fontSize: "17px"
                            }
                        },
                        showInLegend: true
                    }
                },
                legend: {
                    useHTML: true,
                    itemStyle: {
                        fontSize: '16px',
                        // font: '10px Trebuchet MS, Verdana, sans-serif'
                    }
                },
                series: [{
                    type: 'pie',
                    //name: 'Browser share',
                    data: SDoctotalCycleTimeData
                }]
            });

            $("#SDoctotalcycletimeGraph").css('overflow', 'unset');
            //  $("#totalcycletimecanvas").css('zoom', '50%');
            var mysvg = totalcycletime.getSVG();
            var c = document.getElementById('totalcycletimecanvas');
            canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
            $("[id*=hdnTotalCycleTimeGraph]").val(document.getElementById('totalcycletimecanvas').toDataURL());
            // $("#SDoctotalcycletimeGraph > div").css('width', '400');


            //Actual Grinding Time vs Non Grinding Time
            var actualTime = Highcharts.chart('SdocActualGrindingAndNonGrindingTimeConatiner', {
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 60,
                        beta: 0
                    },
                    //width: 800,
                    // height: $('#SdocActualGrindingAndNonGrindingTimeConatiner').height()
                },
                title: {
                    useHTML: true,
                    text: '<b><h4>Actual Grinding Time v/s Non Grinding Time</h4></b>',
                    style: {
                        color: 'green'
                    }
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
                tooltip: {
                    pointFormat: '<b>{point.y} sec - {point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        size: '100%',
                        allowPointSelect: true,
                        cursor: 'pointer',
                        depth: 35,
                        dataLabels: {
                            enabled: true,
                            useHTML: true,
                            format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                            style: {
                                color: "black",
                                fontSize: "17px"
                            }
                        },
                        showInLegend: true
                    }
                },
                legend: {
                    useHTML: true,
                    itemStyle: {
                        fontSize: '16px',
                        // font: '10px Trebuchet MS, Verdana, sans-serif'
                    }
                },
                series: [{
                    type: 'pie',
                    //name: 'Browser share',
                    data: SDocGrindingAndNongrindingTimeData
                }]

            });
            $("#SdocActualGrindingAndNonGrindingTimeConatiner").css('overflow', 'unset');
            var mysvg = actualTime.getSVG();
            var c = document.getElementById('actualtimecanvas');
            canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
            $("[id*=hdActualTimeGraph]").val(document.getElementById('actualtimecanvas').toDataURL());

            //Drillable Cycle Time
            var drillPieData;
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("OutputModules.aspx/getDrilldownTimeData") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{Sdoc:"' + Sdocname + '",Plunge:"' + Plung + '",SubCat:"' + SubCategory + '"}',
                dataType: "json",
                success: function (response) {
                    drillPieData = response.d;
                    console.log("drillPieData.listDrilldownSeries =" + drillPieData.listDrilldownSeries);
                },
                error: function (Result) {
                    // alert("Error");
                }
            });

            $('#SdocDrillableCycleTimeConatiner').empty();
            (function (H) {
                H.wrap(H.Point.prototype, 'destroy', function (proceed) {
                    if (this.legendItem) { // pies have legend items
                        this.series.chart.legend.destroyItem(this);
                    }
                    proceed.apply(this, Array.prototype.slice.call(arguments, 1));
                });
            }(Highcharts));

            var chartCol = Highcharts.chart('SdocDrillableCycleTimeConatiner', {

                chart: {
                    type: 'pie',
                    //width: 600,
                    // height: $('#SdocDrillableCycleTimeConatiner').height()
                    //events: {
                    //    drilldown: function (e) {
                    //        this.options.legend["enabled"] = true;
                    //    },
                    //    drillup: function (e) {
                    //        this.options.legend["enabled"] = false;
                    //    },

                    //}
                },
                title: {
                    useHTML: true,
                    text: '<b><h4>Total Cycle Time</h4></b>',
                    style: {
                        color: 'green'
                    }
                },
                subtitle: {
                    text: ''
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
                legend: {
                    useHTML: true,
                    itemStyle: {
                        fontSize: '16px'
                    }
                },
                accessibility: {
                    announceNewData: {
                        enabled: true
                    },
                    point: {
                        valueSuffix: '%'
                    }
                },

                plotOptions: {
                    series: {

                        dataLabels: {
                            // grouping: false,
                            enabled: true,
                            // format: '{point.name}<br/><b>{point.y} sec - {point.percentage:.1f}%</b>'
                            useHTML: true,
                            format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                            style: {
                                fontSize: "17px"
                            }

                        }
                    },
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            useHTML: true,
                            format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                            style: {
                                fontSize: "17px"
                            }

                        },
                        showInLegend: true
                    }
                },

                tooltip: {
                    //headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<b>{point.y} sec - {point.percentage:.1f}%</b><br/>'
                },

                series: [
                    {
                        name: "Browsers",
                        colorByPoint: true,
                        data: drillPieData.listChartSeries,
                        //dataLabels: {
                        //    enabled: false,
                        //}
                    }
                ],
                drilldown: {
                    activeAxisLabelStyle: {
                        textDecoration: 'none',
                        color: "#000000",
                        fontSize: "17px"
                    },
                    activeDataLabelStyle: {
                        textDecoration: 'none',
                        color: "#000000",
                        fontSize: "17px"
                    },
                    series: drillPieData.listDrilldownSeries
                }

            });

            var mysvg = chartCol.getSVG();
            var c = document.getElementById('mycanvas');
            canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
            console.log("Canvas   " + c);
            // saveCanvas(c, "PNG");
            //Canvas2Image.saveAsPNG(c, 5, 5, false);
            $("[id*=hfImageData]").val(document.getElementById('mycanvas').toDataURL());
        }


        function bingMRRGraph() {
            var noOfRow;
            var noOfColumn;
            var calDetails = [];
            var itemName = [];
            var moduleName = [];

            var Sdocname = "";
            var Plung = "";
            var SubCategory = "";
            Sdocname = $('#ddlSdocID').val();
            var checkedPlungesIndex = [];
            var cpc = 0;
            var checkedSubCatIndex = [];
            var csc = 0;
            for (var p = 0; p < $('#ddlChkPlunges input[type=checkbox]').length; p++) {
                if ($('#ddlChkPlunges input[type=checkbox]')[p].checked) {
                    checkedPlungesIndex[cpc] = p;
                    cpc++;
                }
            }
            for (var p = 0; p < checkedPlungesIndex.length; p++) {
                var i = checkedPlungesIndex[p];
                if (p == checkedPlungesIndex.length - 1) {
                    Plung += "'" + $('#ddlChkPlunges input[type=checkbox]')[parseInt(i)].value + "'";
                }
                else {
                    Plung += "'" + $('#ddlChkPlunges input[type=checkbox]')[parseInt(i)].value + "',";
                }
            }
            for (var p = 0; p < $('#ddlChkSubCatogery input[type=checkbox]').length; p++) {
                if ($('#ddlChkSubCatogery input[type=checkbox]')[p].checked) {
                    checkedSubCatIndex[csc] = p;
                    csc++;
                }
            }
            for (var p = 0; p < checkedSubCatIndex.length; p++) {
                var i = checkedSubCatIndex[p];
                if (p == checkedSubCatIndex.length - 1) {
                    SubCategory += "'" + $('#ddlChkSubCatogery input[type=checkbox]')[i].value + "'";
                }
                else {
                    SubCategory += "'" + $('#ddlChkSubCatogery input[type=checkbox]')[i].value + "',";
                }
            }
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("OutputModules.aspx/getCalculatedGraphData") %>',
                //url: "DataInputModule.aspx/giObjective",
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{Sdoc:"' + Sdocname + '",Plunge:"' + Plung + '",SubCat:"' + SubCategory + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    noOfRow = dataitem[0];
                    noOfColumn = dataitem[1];
                    var k = 0;
                    var j = 0;
                    let m = 0;
                    for (var i = 3; i < dataitem.length; i++) {
                        if (i > 2 && i < (parseInt(noOfRow) + 3)) {
                            moduleName[m] = dataitem[i];
                            m++;
                        } else if (i > ((parseInt(noOfRow) + 3)) && i < ((2 * parseInt(noOfRow)) + 4)) {
                            itemName[j] = dataitem[i];
                            j++;
                        }
                        else if (i > ((2 * parseInt(noOfRow)) + 3)) {
                            calDetails[k] = dataitem[i];
                            k++;
                        }
                    }
                },
                error: function (Result) {
                    alert("Error");
                }
            });
            debugger;
            var calDetailsCount = 0;
            $('#calcParameter').empty();
            $('#calcParameterCanvas').empty();
            var hdnString = "";
            for (var i = 0; i < noOfColumn - 2; i++) {
                var appendString = '<div class="row"><div class="col-sm-1 col-lg-1 col-md-1"></div>';
                var roughingMRR;
                var semiFinishMRR;
                var finishMRR;
                let derivedflag = 0, dressingflag = 0;
                appendString += '<div class="col-sm-10 col-lg-10 col-md-10"><div class="row "><div class="col-sm-6 col-lg-6 col-md-6"> <h4 class="subheader">Calculated Parameter</h4><label class="SdocCs">' + calDetails[calDetailsCount] + '</label> <table  style="width: 100%" border="0" class="opTable tblhover">';
                calDetailsCount++;
                for (var k = 0; k < noOfRow; k++) {
                    if (itemName[k] == 'Average Roughing MRR (cu. mm/sec)') {
                        roughingMRR = parseFloat(calDetails[calDetailsCount]);
                    } else if (itemName[k] == 'Average Semi-finishing MRR (cu. mm/sec)') {
                        semiFinishMRR = parseFloat(calDetails[calDetailsCount]);
                    } else if (itemName[k] == 'Average Finishing MRR (cu. mm/sec)') {
                        finishMRR = parseFloat(calDetails[calDetailsCount]);
                    }
                    if (k == noOfRow - 1) {
                        if (moduleName[k] == "Dervied Parameters") {
                            if (derivedflag == 0) {
                                derivedflag++;
                                appendString += '<tr><td colspan="2" style="text-align: center; background-color:#d1f2d0;font-weight: 540; color: #4e4949">Calculated Parameters</td></tr><tr><th>Item</th><th>Value</th></tr>';
                            }
                        }
                        if (moduleName[k] == "Dressing Parameters") {
                            if (dressingflag == 0) {
                                dressingflag++;
                                appendString += '<tr><td colspan="2" style="text-align: center;background-color:#d1f2d0;font-weight: 540; color: #4e4949">Dressing Parameters</td></tr><tr><th>Item</th><th>Value</th></tr>';
                            }
                        }
                        appendString += '<tr><td>' + itemName[k] + '</td><td>' + calDetails[calDetailsCount] + '</td></tr></table></div>';
                    }
                    else {
                        if (moduleName[k] == "Dervied Parameters") {
                            if (derivedflag == 0) {
                                derivedflag++;
                                appendString += '<tr><td colspan="2" style="text-align: center;background-color:#d1f2d0;;font-weight: 540; color: #4e4949">Calculated Parameters</td></tr><tr><th>Item</th><th>Value</th></tr>';
                            }
                        }
                        if (moduleName[k] == "Dressing Parameters") {
                            if (dressingflag == 0) {
                                dressingflag++;
                                appendString += '<tr><td colspan="2" style="text-align: center;background-color:#d1f2d0;font-weight: 540; color: #4e4949">Dressing Parameters</td></tr><tr><th>Item</th><th>Value</th></tr>';
                            }
                        }
                        appendString += '<tr><td>' + itemName[k] + '</td><td>' + calDetails[calDetailsCount] + '</td></tr>';

                    }
                    calDetailsCount++;
                }

                appendString += '<div class="col-sm-6 col-lg-6 col-md-6"><div id="mrrGraph' + i + '" style="overflow: unset; width: 800px; height: 400px"></div></div></div></div>';

                appendString += '<div class="col-sm-1 col-lg-1 col-md-1"></div></div>';
                $('#calcParameter').append(appendString);
                var chart = Highcharts.chart('mrrGraph' + i, {
                    chart: {
                        type: 'pie',
                        options3d: {
                            enabled: true,
                            alpha: 60,
                            beta: 0
                        },
                        //width: 800,
                        //height: $('#mrrGraph' + i).height()
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
                        text: ''
                    },
                    tooltip: {
                        pointFormat: '<b>{point.y} sec - {point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            size: '100%',
                            allowPointSelect: true,
                            cursor: 'pointer',
                            depth: 35,
                            dataLabels: {
                                enabled: true,
                                //format: '{point.name}<br/><b>{point.y} sec - {point.percentage:.1f}%</b>'
                                useHTML: true,
                                format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                                style: {
                                    color: "black",
                                    fontSize: "17px"
                                }
                            },
                            showInLegend: true
                        }
                    },
                    legend: {
                        useHTML: true,
                        itemStyle: {
                            fontSize: '16px',
                            // font: '10px Trebuchet MS, Verdana, sans-serif'
                        }
                    },
                    series: [{
                        type: 'pie',
                        //name: 'Browser share',
                        data: [
                            {
                                name: 'Average Roughing MRR (cu. mm/sec)',
                                y: roughingMRR,
                                // sliced: true,
                                // selected: true,
                                color: '#000090'
                            },
                            {
                                name: 'Average Semi-finishing MRR (cu. mm/sec)',
                                y: semiFinishMRR,
                                color: '#8fff6f'

                            },
                            {
                                name: 'Average Finishing MRR (cu. mm/sec)',
                                y: finishMRR,
                                color: '#800001'
                            },
                            //['Semi-finishing MRR', data[1]],
                            //['Finishing MRR', data[2]]
                        ]
                    }]
                });
                $("#mrrGraph" + i).css('overflow', 'unset');
                var canvasString = "<canvas id=calcCanvas" + i + "></canvas>";
                $('#calcParameterCanvas').append(canvasString);
                var mysvg = chart.getSVG();
                var c = document.getElementById('calcCanvas' + i);
                canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
                //saveCanvas(c, "PNG");
                //Canvas2Image.saveAsPNG(c, false);
                hdnString += document.getElementById('calcCanvas' + i).toDataURL() + "$";
                $("[id*=hdnCalcParamGraph]").val(hdnString);
                $("#mrrGraph" + i).css('overflow', 'unset');
            }
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {

            $(document).ready(function () {
                //bindTimeGraph();
                btnSDocGraphClick();
                bingMRRGraph();
                $('#gvGeneralInfo th:first-child').empty();                $('#gvGeneralInfo th:first-child').append('&nbsp;Items <i class="glyphicon glyphicon-triangle-bottom" style="padding: 2px; font-size: 10px; border: 1px solid silver"></i>');
                //$('#timeGraph').children()[0].style.width = "100%";
                // $('#mrrGraph').children()[0].style.width = "100%";

            });

            window.onscroll = function () { scrollFunction() };            var topBtn = document.getElementById("topBtn");            function scrollFunction() {                if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {                    topBtn.style.display = "block";                } else {                    topBtn.style.display = "none";                }            }            function SdocgotoComparisonPage() {                window.location.href = "SDocComparisonPage.aspx";
            }            function topFunction() {                document.body.scrollTop = 0;                document.documentElement.scrollTop = 0;            }


            function search(val, evt, ipmodule) {
                let txtSearch = $(val).val();
                debugger;
                let input;
                let label;
                let td
                if (ipmodule == "GeneralInfo") {
                    input = $("#chkParameter input");
                    label = $("#chkParameter label");
                    td = $("#chkParameter td");
                }
                if (ipmodule == "Qly") {
                    input = $("#chkFilterQlyParam input");
                    label = $("#chkFilterQlyParam label");
                    td = $("#chkFilterQlyParam td");
                }

                for (var i = 0; i < input.length; i++) {
                    var chkvalue = label[i].innerText.slice(0, txtSearch.length);
                    if (chkvalue.toLowerCase() == txtSearch.toLowerCase()) {
                        td[i].style.display = "block";
                    } else {
                        td[i].style.display = "none";
                    }
                }
            }


            $('#gvGeneralInfo th:first-child').click(function () {
                document.getElementById("parameterList").style.visibility = 'visible';
            });

            function showFilterQualityParameter() {
                document.getElementById("QualityFilterparameterList").style.visibility = 'visible';
            }
            function generatePDF() {
                document.getElementById("generatePDFModel").style.visibility = 'visible';
                return false;
            }
            function Cancelclick() {
                document.getElementById("generatePDFModel").style.visibility = 'hidden';
            }
            function closepanel() {
                document.getElementById("generatePDFModel").style.visibility = 'hidden';
                return true;
            }

            function filterQlyparameterListCancel() {
                //var CHK = document.getElementById("chkFilterQlyParam");
                //var checkbox = CHK.getElementsByTagName("input");
                //for (var i = 0; i < checkbox.length; i++) {
                //    checkbox[i].checked = false;
                //}
                document.getElementById("QualityFilterparameterList").style.visibility = 'hidden';
            }
            function parameterListSelected() {

                var CHK = document.getElementById("chkParameter");
                var checkbox = CHK.getElementsByTagName("input");
                var colName = "";
                var temp = "";

                for (var i = 1; i < checkbox.length; i++) {
                    if (checkbox[i].checked == true) {
                        if (temp == "") {
                            colName = checkbox[i].value;
                        }
                        else {
                            colName = temp + "," + checkbox[i].value;
                        }

                        temp = colName;
                    }
                }
                document.getElementById("parameterList").style.visibility = 'hidden';
                alert(colName);
            }

            function parameterListCancel() {
                var CHK = document.getElementById("chkParameter");
                var checkbox = CHK.getElementsByTagName("input");
                for (var i = 0; i < checkbox.length; i++) {
                    checkbox[i].checked = false;
                }
                document.getElementById("parameterList").style.visibility = 'hidden';
            }

            $('#chkParameter td').change(function () {

                var CHK = document.getElementById("chkParameter");
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
            $('#chkFilterQlyParam td').change(function () {

                var CHK = document.getElementById("chkFilterQlyParam");
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
            function showParameter() {
                openWarningModal();
            }
            function openWarningModal(msg) {
                //$('[id*=parameterList]').modal('show');
                document.getElementById("parameterList").style.visibility = 'visible';
                $("#warningmessageText").text(msg);
            };


            function btnSDocGraphClick() {
                let label = $("#dcSdoclist label");
                let checkbox = $("#dcSdoclist input");
                let Sdocname = "";
                let Plung = "";
                let SubCategory = "";

                for (let i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].checked) {
                        let SDocid = label[i].textContent.replace("SDoc", "").split("_");
                        Sdocname = SDocid[0];
                        Plung += "'" + SDocid[1] + "',";
                        SubCategory += "'" + SDocid[2] + "',";
                    }
                }

                Plung = Plung.substring(0, Plung.length - 1);
                SubCategory = SubCategory.substring(0, SubCategory.length - 1);
                if (Sdocname == "" && Plung == "" && SubCategory == "") {
                    $('#SdocgrindingTimeContainer').empty();
                    $('#SdocnonGrindingTimeConatiner').empty();
                    $('#SdocDrillableCycleTimeConatiner').empty();
                    $('#SdoctotalCycleTimeConatiner').empty();
                    $("[id*=hdnTotalCycleTimeGraph]").val(null);
                    $("[id*=hdActualTimeGraph]").val(null);
                    $("[id*=hdnTimeGraph]").val(null);
                    $("[id*=hdnCalculatedTimeGraph]").val(null);
                    return;
                }

                var SDocgrindDetails = [];
                var SDocgrindTimeData = [];
                var SDocgrindTimeDataColor = ["#000090", "#8fff6f", "#800001", "#e67312", "#1c1c21", "#056aad"];
                //get SDoc grinding time details
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("OutputModules.aspx/getGrindingTime") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{Sdoc:"' + Sdocname + '",Plunge:"' + Plung + '",SubCat:"' + SubCategory + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                        SDocgrindDetails = dataitem;

                    },
                    error: function (Result) {
                        // alert("Error");
                    }
                });

                $('#SdocgrindingTimeContainer').empty();
                var appendString = '<div class="col-sm-1 col-lg-1 col-md-1"></div><div class="col-sm-5 col-lg-5 col-md-5"><h4 class="subheader">Grinding Time</h4><table class="opTable tblhover" style="width: 100%" border="0"><tr><th>Item</th><th>Value</th></tr>';
                let j = 0;

                for (var i = 0; i < SDocgrindDetails.length; i = i + 2) {

                    appendString += '<tr><td>' + SDocgrindDetails[i] + '</td><td>' + SDocgrindDetails[i + 1] + '</td></tr>';
                    if (SDocgrindDetails[i] == "Roughing Time") {
                        roughingTime = parseFloat(SDocgrindDetails[i + 1]);
                    }
                    else if (SDocgrindDetails[i] == "Semi-finishing Time") {
                        semiFinishTime = parseFloat(SDocgrindDetails[i + 1]);
                    } else if (SDocgrindDetails[i] == "Finishing Time") {
                        finishTime = parseFloat(SDocgrindDetails[i + 1]);
                    }

                    SDocgrindTimeData[j] = {
                        name: SDocgrindDetails[i], y: parseFloat(SDocgrindDetails[i + 1]), color: SDocgrindTimeDataColor[j]
                    };
                    j++;
                }

                appendString += '</table></div><div class="col-sm-5 col-lg-5 col-md-5" style="padding-top: 5px"><div id="SdoctimeGraph" style="overflow:unset;width: 1000px; height: 400px"></div></div><div class="col-sm-1 col-lg-1 col-md-1"></div>';
                $('#SdocgrindingTimeContainer').append(appendString);
                var calculatedtime = Highcharts.chart('SdoctimeGraph', {
                    chart: {
                        type: 'pie',
                        options3d: {
                            enabled: true,
                            alpha: 60,
                            beta: 0
                        },
                        //width: 1000,
                        // height: $('#SdoctimeGraph').height()
                    },
                    title: {
                        text: ''
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
                    tooltip: {
                        pointFormat: '<b>{point.y} sec - {point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            size: '100%',
                            allowPointSelect: true,
                            cursor: 'pointer',
                            depth: 35,
                            dataLabels: {
                                enabled: true,
                                // format: '{point.name}<br/><b>{point.y} sec - {point.percentage:.1f}%</b>'
                                useHTML: true,
                                format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                                style: {
                                    color: "black",
                                    fontSize: "17px"
                                }
                            },
                            showInLegend: true
                        }
                    },
                    legend: {
                        useHTML: true,
                        itemStyle: {
                            fontSize: '16px',
                            // font: '10px Trebuchet MS, Verdana, sans-serif'
                        }
                    },
                    series: [{
                        type: 'pie',
                        //name: 'Browser share',
                        data: SDocgrindTimeData

                    }]
                });

                $("#SdoctimeGraph").css('overflow', 'unset');
                var mysvg = calculatedtime.getSVG();
                var c = document.getElementById('calculatedtimecanvas');
                canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
                $("[id*=hdnCalculatedTimeGraph]").val(document.getElementById('calculatedtimecanvas').toDataURL());


                //get non grinding time details
                var SDocnongrindDetails = [];
                var SDocnongrindTimeData = [];
                var SDocnongrindTimeDataColor = ["#000090", "#8fff6f", "#800001", "#e67312", "#1c1c21", "#056aad"];
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("OutputModules.aspx/getNonGrindingTime") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{Sdoc:"' + Sdocname + '",Plunge:"' + Plung + '",SubCat:"' + SubCategory + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                        SDocnongrindDetails = dataitem;
                    },
                    error: function (Result) {
                        // alert("Error");
                    }
                });
                $('#SdocnonGrindingTimeConatiner').empty();
                var appendString = '<div class="col-sm-1 col-lg-1 col-md-1"></div><div class="col-sm-5 col-lg-5 col-md-5"><h4 class="subheader">Non Grinding Time</h4><table class="opTable tblhover" style="width: 100%" border="0"><tr><th>Item</th><th>Value</th></tr>';
                let nj = 0;
                for (var i = 0; i < SDocnongrindDetails.length; i = i + 2) {

                    appendString += '<tr><td>' + SDocnongrindDetails[i] + '</td><td>' + SDocnongrindDetails[i + 1] + '</td></tr>';

                    SDocnongrindTimeData[nj] = {
                        name: SDocnongrindDetails[i], y: parseFloat(SDocnongrindDetails[i + 1]), color: SDocnongrindTimeDataColor[nj]
                    };
                    nj++;
                }

                appendString += '</table></div><div class="col-sm-5 col-lg-5 col-md-5" style="padding-top: 5px"><div id="SDocnongindtimeGraph" style="overflow:unset;width: 800px; height: 400px"></div></div><div class="col-sm-1 col-lg-1 col-md-1"></div>';
                $('#SdocnonGrindingTimeConatiner').append(appendString);
                console.log("Non grinding " + appendString);
                var nonGrindTimeChart = Highcharts.chart('SDocnongindtimeGraph', {
                    chart: {
                        type: 'pie',
                        options3d: {
                            enabled: true,
                            alpha: 60,
                            beta: 0
                        },
                        //width: 800,
                        //height: $('#SDocnongindtimeGraph').height()
                    },
                    title: {
                        text: ''
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
                    tooltip: {
                        pointFormat: '<b>{point.y} sec - {point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            size: '100%',
                            allowPointSelect: true,
                            cursor: 'pointer',
                            depth: 35,
                            dataLabels: {
                                enabled: true,
                                //   format: '{point.name}<br/><b>{point.y} sec - {point.percentage:.1f}%</b>'
                                useHTML: true,
                                format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                                style: {
                                    color: "black",
                                    fontSize: "17px"
                                }
                            },
                            showInLegend: true
                        }
                    },
                    legend: {
                        useHTML: true,
                        itemStyle: {
                            fontSize: '16px',
                            // font: '10px Trebuchet MS, Verdana, sans-serif'
                        }
                    },
                    series: [{
                        type: 'pie',
                        //name: 'Browser share',
                        data: SDocnongrindTimeData

                    }]
                });
                $("#SDocnongindtimeGraph").css('overflow', 'unset');
                //var nonGrindTimesvg = nonGrindTimeChart.getSVG();
                //var cNonGrindTime = document.getElementById('nonGrindTimeCanvas');
                //canvg(cNonGrindTime, nonGrindTimesvg, { ignoreMouse: true, ignoreAnimation: true });
                //Canvas2Image.saveAsPNG(cNonGrindTime, false);
                //$("[id*=hdnNonGrindTime]").val(document.getElementById('nonGrindTimeCanvas').toDataURL());


                //get total cycletime details
                var SDoctotalCycleTimeDetails = [];
                var SDoctotalCycleTimeData = [];
                var SDoctotalCycleTimeDataColor = ["#000090", "#e67312", "#8fff6f", "#800001", "#1c1c21", "#056aad"];
                var SDocGrindingAndNongrindingTimeData = [];
                var SDocGrindingAndNongrindingTimeDataColor = ["#8fff6f", "#e67312", "#800001", "#000090", "#1c1c21", "#056aad"];
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("OutputModules.aspx/getTotalCycleTime") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{Sdoc:"' + Sdocname + '",Plunge:"' + Plung + '",SubCat:"' + SubCategory + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                        SDoctotalCycleTimeDetails = dataitem;
                    },
                    error: function (Result) {
                        // alert("Error");
                    }
                });
                $('#SdoctotalCycleTimeConatiner').empty();
                var appendString = '<div class="col-sm-1 col-lg-1 col-md-1"></div><div class="col-sm-5 col-lg-5 col-md-5"><h4 class="subheader">Total Cycle Time</h4><table class="opTable tblhover" style="width: 100%" border="0"><tr><th>Item</th><th>Value</th></tr>';

                let tj = 0;
                let gn = 0;
                for (var i = 0; i < SDoctotalCycleTimeDetails.length; i = i + 2) {

                    appendString += '<tr><td>' + SDoctotalCycleTimeDetails[i] + '</td><td>' + SDoctotalCycleTimeDetails[i + 1] + '</td></tr>';

                    //if (i == (SDoctotalCycleTimeDetails.length - 6)) {
                    //    continue;
                    //} else {
                    //    SDoctotalCycleTimeData[tj] = {
                    //        name: SDoctotalCycleTimeDetails[i], y: parseFloat(SDoctotalCycleTimeDetails[i + 1]), color: SDoctotalCycleTimeDataColor[tj]
                    //    };
                    //    tj++;
                    //}
                    if (SDoctotalCycleTimeDetails[i] == "Grinding Time (sec)") {
                        SDoctotalCycleTimeData[tj] = {
                            name: SDoctotalCycleTimeDetails[i], y: parseFloat(SDoctotalCycleTimeDetails[i + 1]), color: SDoctotalCycleTimeDataColor[tj]
                        };
                        tj++;
                    }
                    if (SDoctotalCycleTimeDetails[i] == "Actual Grinding Time (sec)") {
                        SDoctotalCycleTimeData[tj] = {
                            name: SDoctotalCycleTimeDetails[i], y: parseFloat(SDoctotalCycleTimeDetails[i + 1]), color: SDoctotalCycleTimeDataColor[tj]
                        };
                        tj++;
                        SDocGrindingAndNongrindingTimeData[gn] = {
                            name: SDoctotalCycleTimeDetails[i], y: parseFloat(SDoctotalCycleTimeDetails[i + 1]), color: SDocGrindingAndNongrindingTimeDataColor[gn]
                        };
                        gn++;
                    }
                    if (SDoctotalCycleTimeDetails[i] == "Non Grinding Time (sec)") {
                        SDocGrindingAndNongrindingTimeData[gn] = {
                            name: SDoctotalCycleTimeDetails[i], y: parseFloat(SDoctotalCycleTimeDetails[i + 1]), color: SDocGrindingAndNongrindingTimeDataColor[gn]
                        };
                        gn++;
                    }
                }
                appendString += '</table></div><div class="col-sm-5 col-lg-5 col-md-5" style="padding-top: 5px"><div id="SDoctotalcycletimeGraph" style="overflow: unset; width: 800px; height: 400px"></div></div><div class="col-sm-1 col-lg-1 col-md-1"></div>';
                $('#SdoctotalCycleTimeConatiner').append(appendString);

                //bar chart
                //var chart = Highcharts.chart('SDoctotalcycletimeGraph', {
                //    chart: {
                //        type: 'column'
                //    },
                //    title: {
                //        useHTML: true,
                //        text: '<b><h5>Calculated Grinding Time vs Actual Grinding time</h5></b>',
                //        style: {
                //            color: 'green'
                //        }
                //    },
                //    subtitle: {
                //        text: ''
                //    },
                //    accessibility: {
                //        announceNewData: {
                //            enabled: true
                //        }
                //    },
                //    xAxis: {
                //        type: 'category'
                //    },
                //    yAxis: {
                //        title: {
                //            text: ''
                //        }

                //    },
                //    legend: {
                //        enabled: false
                //    },
                //    plotOptions: {
                //        series: {
                //            borderWidth: 0,
                //            dataLabels: {
                //                enabled: true,
                //                format: '{point.y}'
                //            }
                //        }
                //    },

                //    tooltip: {
                //        //headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                //        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}</b><br/>'
                //    },

                //    series: [
                //        {
                //            name: "Browsers",
                //            colorByPoint: true,
                //            data: SDoctotalCycleTimeData
                //        }
                //    ]

                //});
                //var mysvg = chart.getSVG();
                //var c = document.getElementById('mycanvas');
                //canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
                //Canvas2Image.saveAsPNG(c, false);
                //$("[id*=hfImageData]").val(document.getElementById('mycanvas').toDataURL());
                //pie chart
                var totalcycletime = Highcharts.chart('SDoctotalcycletimeGraph', {
                    chart: {
                        type: 'pie',
                        options3d: {
                            enabled: true,
                            alpha: 60,
                            beta: 0
                        },
                        //width: 800,
                        //height: $('#SDoctotalcycletimeGraph').height()
                    },
                    title: {
                        useHTML: true,
                        //  text: '<b><h5>Calcualted Grinding Time v/s Actual Grinding time</h5></b>',
                        text: '<b><h4>Total Cycle Time</h4></b>',
                        style: {
                            color: 'green'
                        }
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
                    tooltip: {
                        pointFormat: '<b>{point.y} sec - {point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            size: '100%',
                            allowPointSelect: true,
                            cursor: 'pointer',
                            depth: 35,
                            dataLabels: {
                                enabled: true,
                                //format: '{point.name}<br/><b>{point.y} sec -  {point.percentage:.1f}%</b>'
                                useHTML: true,
                                format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                                style: {
                                    color: "black",
                                    fontSize: "17px"
                                }
                            },
                            showInLegend: true
                        }
                    },
                    legend: {
                        useHTML: true,
                        itemStyle: {
                            fontSize: '16px',
                            // font: '10px Trebuchet MS, Verdana, sans-serif'
                        }
                    },
                    series: [{
                        type: 'pie',
                        //name: 'Browser share',
                        data: SDoctotalCycleTimeData
                    }]
                });
                $("#SDoctotalcycletimeGraph").css('overflow', 'unset');
                var mysvg = totalcycletime.getSVG();;
                var c = document.getElementById('totalcycletimecanvas');
                canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
                $("[id*=hdnTotalCycleTimeGraph]").val(document.getElementById('totalcycletimecanvas').toDataURL());

                //Actual Grinding Time vs Non Grinding Time
                var actualTime = Highcharts.chart('SdocActualGrindingAndNonGrindingTimeConatiner', {
                    chart: {
                        type: 'pie',
                        options3d: {
                            enabled: true,
                            alpha: 60,
                            beta: 0
                        },
                        //width: 800,
                        //height: $('#SdocActualGrindingAndNonGrindingTimeConatiner').height()
                    },
                    title: {
                        useHTML: true,
                        text: '<b><h4>Actual Grinding Time v/s Non Grinding Time</h4></b>',
                        style: {
                            color: 'green'
                        }
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
                    tooltip: {
                        pointFormat: '<b>{point.y} sec - {point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            size: '100%',
                            allowPointSelect: true,
                            cursor: 'pointer',
                            depth: 35,
                            dataLabels: {
                                enabled: true,
                                //format: '{point.name}<br/><b>{point.y} sec - {point.percentage:.1f}%</b>'
                                useHTML: true,
                                format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                                style: {
                                    color: "black",
                                    fontSize: "17px"
                                }
                            },
                            showInLegend: true
                        }
                    },
                    legend: {
                        useHTML: true,
                        itemStyle: {
                            fontSize: '16px',
                            // font: '10px Trebuchet MS, Verdana, sans-serif'
                        }
                    },
                    series: [{
                        type: 'pie',
                        //name: 'Browser share',
                        data: SDocGrindingAndNongrindingTimeData
                    }]
                });
                $("#SdocActualGrindingAndNonGrindingTimeConatiner").css('overflow', 'unset');
                var mysvg = actualTime.getSVG();
                var c = document.getElementById('actualtimecanvas');
                canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
                $("[id*=hdActualTimeGraph]").val(document.getElementById('actualtimecanvas').toDataURL());


                //Drillable Cycle Time
                var drillPieData;
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("OutputModules.aspx/getDrilldownTimeData") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{Sdoc:"' + Sdocname + '",Plunge:"' + Plung + '",SubCat:"' + SubCategory + '"}',
                    dataType: "json",
                    success: function (response) {
                        drillPieData = response.d;
                        console.log(drillPieData);
                    },
                    error: function (Result) {
                        // alert("Error");
                    }
                });


                $('#SdocDrillableCycleTimeConatiner').empty();

                var chartCol = Highcharts.chart('SdocDrillableCycleTimeConatiner', {

                    chart: {
                        type: 'pie',
                        //width: 600,
                        // height: $('#SdocDrillableCycleTimeConatiner').height()
                    },
                    title: {
                        useHTML: true,
                        text: '<b><h4>Total Cycle Time</h4></b>',
                        style: {
                            color: 'green'
                        }
                    },
                    subtitle: {
                        text: ''
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
                    accessibility: {
                        announceNewData: {
                            enabled: true
                        },
                        point: {
                            valueSuffix: '%'
                        }
                    },
                    legend: {
                        useHTML: true,
                        itemStyle: {
                            fontSize: '16px'
                        }
                    },

                    plotOptions: {
                        series: {
                            dataLabels: {
                                enabled: true,
                                //format: '{point.name}<br/><b>{point.y} sec - {point.percentage:.1f}%</b>'
                                useHTML: true,
                                format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                                style: {
                                    fontSize: "17px"
                                }
                            }
                        },
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: true,
                                useHTML: true,
                                format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                                style: {
                                    fontSize: "17px"
                                }

                            },
                            showInLegend: true
                        }
                    },

                    tooltip: {
                        //headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<b>{point.y} sec - {point.percentage:.1f}%</b><br/>'
                    },

                    series: [
                        {
                            name: "Browsers",
                            colorByPoint: true,
                            data: drillPieData.listChartSeries
                        }
                    ],
                    drilldown: {
                        activeAxisLabelStyle: {
                            textDecoration: 'none',
                            color: "#000000",
                            fontSize: "17px"
                        },
                        activeDataLabelStyle: {
                            textDecoration: 'none',
                            color: "#000000",
                            fontSize: "17px"
                        },
                        series: drillPieData.listDrilldownSeries
                    }
                });

                var mysvg = chartCol.getSVG();
                var c = document.getElementById('mycanvas');
                canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
                console.log("Canvas   " + c);
                // saveCanvas(c, "PNG");
                //Canvas2Image.saveAsPNG(c, 5, 5, false);
                $("[id*=hfImageData]").val(document.getElementById('mycanvas').toDataURL());
            }


            function bingMRRGraph() {
                var noOfRow;
                var noOfColumn;
                var calDetails = [];
                var itemName = [];
                var moduleName = [];

                var Sdocname = "";
                var Plung = "";
                var SubCategory = "";
                Sdocname = $('#ddlSdocID').val();
                var checkedPlungesIndex = [];
                var cpc = 0;
                var checkedSubCatIndex = [];
                var csc = 0;
                for (var p = 0; p < $('#ddlChkPlunges input[type=checkbox]').length; p++) {
                    if ($('#ddlChkPlunges input[type=checkbox]')[p].checked) {
                        checkedPlungesIndex[cpc] = p;
                        cpc++;
                    }
                }
                for (var p = 0; p < checkedPlungesIndex.length; p++) {
                    var i = checkedPlungesIndex[p];
                    if (p == checkedPlungesIndex.length - 1) {
                        Plung += "'" + $('#ddlChkPlunges input[type=checkbox]')[parseInt(i)].value + "'";
                    }
                    else {
                        Plung += "'" + $('#ddlChkPlunges input[type=checkbox]')[parseInt(i)].value + "',";
                    }
                }
                for (var p = 0; p < $('#ddlChkSubCatogery input[type=checkbox]').length; p++) {
                    if ($('#ddlChkSubCatogery input[type=checkbox]')[p].checked) {
                        checkedSubCatIndex[csc] = p;
                        csc++;
                    }
                }
                for (var p = 0; p < checkedSubCatIndex.length; p++) {
                    var i = checkedSubCatIndex[p];
                    if (p == checkedSubCatIndex.length - 1) {
                        SubCategory += "'" + $('#ddlChkSubCatogery input[type=checkbox]')[i].value + "'";
                    }
                    else {
                        SubCategory += "'" + $('#ddlChkSubCatogery input[type=checkbox]')[i].value + "',";
                    }
                }
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("OutputModules.aspx/getCalculatedGraphData") %>',
                    //url: "DataInputModule.aspx/giObjective",
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{Sdoc:"' + Sdocname + '",Plunge:"' + Plung + '",SubCat:"' + SubCategory + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                        noOfRow = dataitem[0];
                        noOfColumn = dataitem[1];
                        var k = 0;
                        var j = 0;
                        let m = 0;
                        for (var i = 3; i < dataitem.length; i++) {
                            if (i > 2 && i < (parseInt(noOfRow) + 3)) {
                                moduleName[m] = dataitem[i];
                                m++;
                            } else if (i > ((parseInt(noOfRow) + 3)) && i < ((2 * parseInt(noOfRow)) + 4)) {
                                itemName[j] = dataitem[i];
                                j++;
                            }
                            else if (i > ((2 * parseInt(noOfRow)) + 3)) {
                                calDetails[k] = dataitem[i];
                                k++;
                            }
                        }
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });
                debugger;
                var calDetailsCount = 0;
                $('#calcParameter').empty();
                $('#calcParameterCanvas').empty();
                var hdnString = "";
                for (var i = 0; i < noOfColumn - 2; i++) {
                    var appendString = '<div class="row"><div class="col-sm-1 col-lg-1 col-md-1"></div>';
                    var roughingMRR;
                    var semiFinishMRR;
                    var finishMRR;
                    let derivedflag = 0, dressingflag = 0;
                    appendString += '<div class="col-sm-10 col-lg-10 col-md-10"><div class="row "><div class="col-sm-6 col-lg-6 col-md-6"> <h4 class="subheader">Calculated Parameter</h4><label class="SdocCs">' + calDetails[calDetailsCount] + '</label> <table  style="width: 100%" border="0" class="opTable tblhover">';
                    calDetailsCount++;
                    for (var k = 0; k < noOfRow; k++) {
                        if (itemName[k] == 'Average Roughing MRR (cu. mm/sec)') {
                            roughingMRR = parseFloat(calDetails[calDetailsCount]);
                        } else if (itemName[k] == 'Average Semi-finishing MRR (cu. mm/sec)') {
                            semiFinishMRR = parseFloat(calDetails[calDetailsCount]);
                        } else if (itemName[k] == 'Average Finishing MRR (cu. mm/sec)') {
                            finishMRR = parseFloat(calDetails[calDetailsCount]);
                        }
                        if (k == noOfRow - 1) {
                            if (moduleName[k] == "Dervied Parameters") {
                                if (derivedflag == 0) {
                                    derivedflag++;
                                    appendString += '<tr><td colspan="2" style="text-align: center; background-color:#d1f2d0;font-weight: 540; color: #4e4949">Calculated Parameters</td></tr><tr><th>Item</th><th>Value</th></tr>';
                                }
                            }
                            if (moduleName[k] == "Dressing Parameters") {
                                if (dressingflag == 0) {
                                    dressingflag++;
                                    appendString += '<tr><td colspan="2" style="text-align: center;background-color:#d1f2d0;font-weight: 540; color: #4e4949">Dressing Parameters</td></tr><tr><th>Item</th><th>Value</th></tr>';
                                }
                            }
                            appendString += '<tr><td>' + itemName[k] + '</td><td>' + calDetails[calDetailsCount] + '</td></tr></table></div>';
                        }
                        else {
                            if (moduleName[k] == "Dervied Parameters") {
                                if (derivedflag == 0) {
                                    derivedflag++;
                                    appendString += '<tr><td colspan="2" style="text-align: center;background-color:#d1f2d0;font-weight: 540; color: #4e4949">Calculated Parameters</td></tr><tr><th>Item</th><th>Value</th></tr>';
                                }
                            }
                            if (moduleName[k] == "Dressing Parameters") {
                                if (dressingflag == 0) {
                                    dressingflag++;
                                    appendString += '<tr><td colspan="2" style="text-align: center;background-color:#d1f2d0;font-weight: 540; color: #4e4949">Dressing Parameters</td></tr><tr><th>Item</th><th>Value</th></tr>';
                                }
                            }
                            appendString += '<tr><td>' + itemName[k] + '</td><td>' + calDetails[calDetailsCount] + '</td></tr>';

                        }
                        calDetailsCount++;
                    }

                    appendString += '<div class="col-sm-6 col-lg-6 col-md-6"><div id="mrrGraph' + i + '" style="overflow: unset;width: 800px; height: 400px"></div></div></div></div>';

                    appendString += '<div class="col-sm-1 col-lg-1 col-md-1"></div></div>';
                    $('#calcParameter').append(appendString);
                    var chart = Highcharts.chart('mrrGraph' + i, {
                        chart: {
                            type: 'pie',
                            options3d: {
                                enabled: true,
                                alpha: 60,
                                beta: 0
                            },
                            //width: 800,
                            //height: $('#mrrGraph' + i).height()
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
                            text: ''
                        },
                        tooltip: {
                            pointFormat: '<b>{point.y} sec - {point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: '100%',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                depth: 35,
                                dataLabels: {
                                    enabled: true,
                                    // format: '{point.name}<br/><b>{point.y} sec - {point.percentage:.1f}%</b>'
                                    useHTML: true,
                                    format: '{point.name}<br/>{point.y} sec - {point.percentage:.1f}%',
                                    style: {
                                        color: "black",
                                        fontSize: "17px"
                                    }
                                },
                                showInLegend: true
                            }
                        },
                        legend: {
                            useHTML: true,
                            itemStyle: {
                                fontSize: '16px',
                                // font: '10px Trebuchet MS, Verdana, sans-serif'
                            }
                        },
                        series: [{
                            type: 'pie',
                            //name: 'Browser share',
                            data: [
                                {
                                    name: 'Average Roughing MRR (cu. mm/sec)',
                                    y: roughingMRR,
                                    // sliced: true,
                                    // selected: true,
                                    color: '#000090'
                                },
                                {
                                    name: 'Average Semi-finishing MRR (cu. mm/sec)',
                                    y: semiFinishMRR,
                                    color: '#8fff6f'

                                },
                                {
                                    name: 'Average Finishing MRR (cu. mm/sec)',
                                    y: finishMRR,
                                    color: '#800001'
                                },
                                //['Semi-finishing MRR', data[1]],
                                //['Finishing MRR', data[2]]
                            ]
                        }]
                    });
                    $("#mrrGraph" + i).css('overflow', 'unset');
                    var canvasString = "<canvas id=calcCanvas" + i + "></canvas>";
                    $('#calcParameterCanvas').append(canvasString);
                    var mysvg = chart.getSVG();
                    var c = document.getElementById('calcCanvas' + i);
                    canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
                    //saveCanvas(c, "PNG");
                    //Canvas2Image.saveAsPNG(c, false);
                    hdnString += document.getElementById('calcCanvas' + i).toDataURL() + "$";
                    $("[id*=hdnCalcParamGraph]").val(hdnString);
                    $("#mrrGraph" + i).css('overflow', 'unset');
                }
            }

        });
    </script>
</asp:Content>
