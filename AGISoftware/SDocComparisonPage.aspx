<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SDocComparisonPage.aspx.cs" Inherits="AGISoftware.SDocComparisonPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script src="Scripts/HighChartV8/data.js"></script>
    <script src="Scripts/HighChartV8/drilldown.js"></script>
    <script src="Scripts/Canvg/canvg.js"></script>
    <script src="Scripts/Canvas2Image/canvas2image.js"></script>
    <script src="Scripts/Html2Canvas/html2canvas.js"></script>
    <style>
        #OutputModules {
            background-color: white;
        }

            #OutputModules a, #OutputModules svg {
                color: brown;
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

        #gvGeneralInfo tr th:nth-child(2), #gvGeneralInfo tr td:nth-child(2), #gvGeneralInfo tr th:nth-child(3), #gvGeneralInfo tr td:nth-child(3) {
            display: none;
        }

        .checkCS tr td {
            border-bottom: none;
        }

        .checkCS label {
            font-weight: unset;
        }
        .SdocCs {
            color: orangered;
            background-color: white;
            font-size: 16px;
            text-align: center;
            width: 100%;
        }
        #genInfoContainer tr:hover, #genInfoContainer tr:hover td, .tblhover tr:hover, .tblhover tr:hover td {
            background-color: #f1f3f4 !important;
        }
        /*.highcharts-container {
            width: 100vw !important;
        }

        .highcharts-root {
            width: 100vw !important;
        }*/

    </style>

    <div class="container-fluid">
            <asp:HiddenField runat="server" ID="hdnGrindingTimeGraph" ClientIDMode="Static" />
                    <asp:HiddenField runat="server" ID="hdnNonGrindingTimeGraph" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnTotalCycleTimeGraph" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdActualTimeGraph" ClientIDMode="Static" />
         <asp:HiddenField runat="server" ID="hdnDrillChart" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnCalculatedTimeGraph" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnCalcParamGraph" ClientIDMode="Static" />
        <div class="row">
            <div class="col-sm-1 col-lg-1 col-md-1"></div>
            <div class="col-sm-10 col-lg-10 col-md-10">

                <asp:UpdatePanel runat="server" ID="ouputmoduleUpdate" ClientIDMode="Static">
                    <ContentTemplate>
                        <div style="width: 100%; display: inline-block; float: left">
                            Select System Documents&nbsp;
                     <%--       <asp:DropDownList runat="server" ID="ddlSodcId1" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlSodcId1_SelectedIndexChanged" Style="display: inline-block"></asp:DropDownList>
                      
                            <asp:DropDownList runat="server" ID="ddlSDocId2" CssClass="form-control" ClientIDMode="Static" Style="display: inline-block"></asp:DropDownList>--%>

                                 <asp:TextBox runat="server" ID="txtSDocId1" Style="display: inline-block; min-width: 340px" ForeColor="Black" AutoPostBack="true" OnTextChanged="txtSDocId1_TextChanged" ClientIDMode="Static" placeholder="SDoc ID1" AutoCompleteType="Disabled" list="SDocid1lists" CssClass="form-control"></asp:TextBox>
                    <datalist id="SDocid1lists" runat="server" clientidmode="static" autopostback="true"></datalist>

                                 <asp:TextBox runat="server" ID="txtSDocId2" Style="display: inline-block; min-width: 340px" ForeColor="Black" ClientIDMode="Static" placeholder="SDoc ID2" AutoCompleteType="Disabled" list="SDocid2lists" CssClass="form-control"></asp:TextBox>
                    <datalist id="SDocid2lists" runat="server" clientidmode="static" autopostback="true"></datalist>
                       
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button runat="server" ID="btnView" Text="View" CssClass="Btns" OnClick="btnView_Click" />
                             <asp:Button runat="server" ID="btnPDF" Text="PDF" CssClass="Btns"  OnClick="btnPDF_Click" />
                             <asp:Button runat="server" ID="btnExcel" Text="Excel" CssClass="Btns"  OnClick="btnExcel_Click" />
                            <asp:Button runat="server" ID="btnBack" Text="Back to Output Module" OnClientClick="gotoOutputModule()" CssClass="Btns" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtSDocId1" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
                          <asp:PostBackTrigger ControlID="btnPDF" />
                        <asp:PostBackTrigger ControlID="btnExcel" />
                    </Triggers>
                </asp:UpdatePanel>
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
                                    <asp:CheckBoxList ID="chkParameter" runat="server" CssClass="checkCS" ClientIDMode="Static" Style="color: #454444"></asp:CheckBoxList>
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
                                    <div style="display: inline-block; overflow: auto; border: 1px solid silver; width: 49%">
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

                <div id="totalCycleTimeConatiner"></div>
                 <div id="totalcycletimeCanvas" style="display: none"></div>
                   <div id="totalcycletimedrillCanvas" style="display: none"></div>
                
                <div id="grindingtimeConatiner"></div>
                 <div id="grindingtimeCanvas" style="display: none"></div>
                <div id="nongrindingtimeConatiner"></div>
                 <div id="nongrindingtimeCanvas" style="display: none"></div>
                <div id="calcParameter">
                </div>
                <div id="calcParameterCanvas" style="display: none;"></div>

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

                <div class="row">
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                    <div class="col-sm-10 col-lg-10 col-md-10">
                        <h4 class="subheader">Remarks</h4>
                        <div id="remarksContainer" style="overflow: auto" class="opTable">
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" style="width: 100%; max-width:unset"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-1 col-lg-1 col-md-1"></div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
                           <asp:PostBackTrigger ControlID="btnPDF" />
                <asp:PostBackTrigger ControlID="btnExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <script>
        $(document).ready(function () {
            //btnSDocGraphClick();
            bindGrindingTimeGraph();
             bindNonGrindingTimeGraph();
            bindTotalCycleTimeGraph();
            bingMRRGraph();
            $('#gvGeneralInfo th:first-child').empty();            $('#gvGeneralInfo th:first-child').append('&nbsp;Items <i class="glyphicon glyphicon-triangle-bottom" style="padding: 2px; font-size: 10px; border: 1px solid silver"></i>');


        });

        $('#txtSDocId2').change(function () {
            if ($('#txtSDocId2').val() != "") {
                 $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("SDocComparisonPage.aspx/setSDocID2") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{SDcoid2: "' + $('#txtSDocId2').val() + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                      
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });
            }
           
        });

        function gotoOutputModule() {
            window.location.href = "OutputModules.aspx";
        }
        $('#gvGeneralInfo th:first-child').click(function () {
            document.getElementById("parameterList").style.visibility = 'visible';
        });
        function parameterListCancel() {
            document.getElementById("parameterList").style.visibility = 'hidden';
        }
        function showFilterQualityParameter() {
            document.getElementById("QualityFilterparameterList").style.visibility = 'visible';
        }
        function filterQlyparameterListCancel() {
            document.getElementById("QualityFilterparameterList").style.visibility = 'hidden';
        }
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

        function bingMRRGraph() {
            var noOfRow;
            var noOfColumn;
            var calDetails = [];
            var itemName = [];

            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("SDocComparisonPage.aspx/getMRRGraphData") %>',
                //url: "DataInputModule.aspx/giObjective",
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{SDocid1:"' + $('#txtSDocId1').val() + '",SDocid2:"' + $('#txtSDocId2').val() + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    noOfRow = dataitem[0];
                    noOfColumn = dataitem[1];
                    var k = 0;
                    var j = 0;
                    for (var i = 3; i < dataitem.length; i++) {
                        if (i > 2 && i < (parseInt(noOfRow) + 3)) {
                            itemName[j] = dataitem[i];
                            j++;
                        }
                        else {
                            calDetails[k] = dataitem[i];
                            k++;
                        }
                    }
                },
                error: function (Result) {
                    alert("Error");
                }
            });
            var calDetailsCount = 0;
            $('#calcParameter').empty();
            $('#calcParameterCanvas').empty();
            var hdnString = "";
            for (var i = 0; i < noOfColumn - 1; i++) {
                var appendString = '<div class="row"><div class="col-sm-1 col-lg-1 col-md-1"></div>';
                var roughingMRR;
                var semiFinishMRR;
                var finishMRR;
                if (i == 0) {
                    appendString += '<div class="col-sm-10 col-lg-10 col-md-10"> <h4 class="subheader">Calculated Parameter</h4><div class="row "><div class="col-sm-6 col-lg-6 col-md-6"><label class="SdocCs">' + calDetails[calDetailsCount] + '</label> <table  style="width: 100%" border="0" class="opTable tblhover"><tr><th>Item</th><th>Value</th></tr>';
                } else {
                    appendString += '<div class="col-sm-10 col-lg-10 col-md-10"><div class="row "><div class="col-sm-6 col-lg-6 col-md-6"><label class="SdocCs">' + calDetails[calDetailsCount] + '</label> <table  style="width: 100%" border="0" class="opTable tblhover"><tr><th>Item</th><th>Value</th></tr>';
                }

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
                        appendString += '<tr><td>' + itemName[k] + '</td><td>' + calDetails[calDetailsCount] + '</td></tr></table></div>';
                    }
                    else {
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
                        data: [
                            {
                                name: 'Average Roughing MRR (cu. mm/sec)',
                                y: roughingMRR,
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
                        ]
                    }]
                });
                $("#mrrGraph" + i).css('overflow', 'unset');
                var canvasString = "<canvas id=calcCanvas" + i + "></canvas>";
                $('#calcParameterCanvas').append(canvasString);
                var mysvg = chart.getSVG();
                var c = document.getElementById('calcCanvas' + i);
                canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
                hdnString += document.getElementById('calcCanvas' + i).toDataURL() + "$";
                $("[id*=hdnCalcParamGraph]").val(hdnString);
                $("#mrrGraph" + i).css('overflow', 'unset');
            }
        }
        function bindGrindingTimeGraph() {
            var SDocgrindDetails = [];
            //get SDoc grinding time details
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("SDocComparisonPage.aspx/getGrindingTime") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{SDocid1:"' + $('#txtSDocId1').val() + '",SDocid2:"' + $('#txtSDocId2').val() + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    SDocgrindDetails = dataitem;

                },
                error: function (Result) {
                     alert("Error");
                }
            });
            
            $('#grindingtimeConatiner').empty();
            $('#grindingtimeCanvas').empty();
            var hdnString = "";
            for (var i = 0; i < SDocgrindDetails.length; i++) {
                var SDocgrindTimeData = [];
                var SDocgrindTimeDataColor = ["#000090", "#8fff6f", "#800001", "#e67312", "#1c1c21", "#056aad"];
                let j = 0;
                var appendString = '<div class="row"><div class="col-sm-1 col-lg-1 col-md-1"></div>';
                if (i == 0) {
                    appendString += '<div class="col-sm-10 col-lg-10 col-md-10"> <h4 class="subheader">Grinding Time</h4><div class="row "><div class="col-sm-6 col-lg-6 col-md-6"><label class="SdocCs">' + SDocgrindDetails[i].SDocid + '</label> <table  style="width: 100%" border="0" class="opTable tblhover"><tr><th>Item</th><th>Value</th></tr>';
                } else {
                    appendString += '<div class="col-sm-10 col-lg-10 col-md-10"><div class="row "><div class="col-sm-6 col-lg-6 col-md-6"><label class="SdocCs">' + SDocgrindDetails[i].SDocid + '</label> <table  style="width: 100%" border="0" class="opTable tblhover"><tr><th>Item</th><th>Value</th></tr>';
                }

                for (var k = 0; k < SDocgrindDetails[i].values.length; k++) {

                    if (k == SDocgrindDetails[i].values.length - 1) {
                        appendString += '<tr><td>' + SDocgrindDetails[i].values[k].Parameter + '</td><td>' + SDocgrindDetails[i].values[k].Value + '</td></tr></table></div>';
                    } else {
                        appendString += '<tr><td>' + SDocgrindDetails[i].values[k].Parameter + '</td><td>' + SDocgrindDetails[i].values[k].Value + '</td></tr>';
                    }
                    SDocgrindTimeData[j] = {
                        name: SDocgrindDetails[i].values[k].Parameter, y: parseFloat(SDocgrindDetails[i].values[k].Value), color: SDocgrindTimeDataColor[j]
                    };
                    j++;
                }

                appendString += '<div class="col-sm-6 col-lg-6 col-md-6"><div id="grindingtimegraph' + i + '" style="overflow: unset; width: 1000px; height: 400px"></div></div></div></div>';
                appendString += '<div class="col-sm-1 col-lg-1 col-md-1"></div></div>';
                $('#grindingtimeConatiner').append(appendString);
                var grindingtime = Highcharts.chart('grindingtimegraph' + i, {
                    chart: {
                        type: 'pie',
                        options3d: {
                            enabled: true,
                            alpha: 60,
                            beta: 0
                        },
                        //width: 1000,
                        //height: $('#grindingtimegraph' + i).height()
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
                $("#grindingtimegraph" + i).css('overflow', 'unset');
                var canvasString = "<canvas id=grindingtimeCanvas" + i + "></canvas>";
                $('#grindingtimeCanvas').append(canvasString);
                var mysvg = grindingtime.getSVG();
                var c = document.getElementById('grindingtimeCanvas' + i);
                canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
                hdnString += document.getElementById('grindingtimeCanvas' + i).toDataURL() + "$";
                $("[id*=hdnGrindingTimeGraph]").val(hdnString);
                $("#grindingtimegraph" + i).css('overflow', 'unset');
            }
        }
        function bindNonGrindingTimeGraph() {
            var SDocnongrindDetails = [];
            //get SDoc non grinding time details
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("SDocComparisonPage.aspx/getNonGrindingTime") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{SDocid1:"' + $('#txtSDocId1').val() + '",SDocid2:"' + $('#txtSDocId2').val() + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    SDocnongrindDetails = dataitem;
                },
                error: function (Result) {
                    alert("Error");
                }
            });

            $('#nongrindingtimeConatiner').empty();
            $('#nongrindingtimeCanvas').empty();
            var hdnString = "";
            for (var i = 0; i < SDocnongrindDetails.length; i++) {
                var SDocnongrindTimeData = [];
                var SDocnongrindTimeDataColor = ["#000090", "#8fff6f", "#800001", "#e67312", "#1c1c21", "#056aad"];
                let nj = 0;
                var appendString = '<div class="row"><div class="col-sm-1 col-lg-1 col-md-1"></div>';
                if (i == 0) {
                    appendString += '<div class="col-sm-10 col-lg-10 col-md-10"> <h4 class="subheader">Non Grinding Time</h4><div class="row "><div class="col-sm-6 col-lg-6 col-md-6"><label class="SdocCs">' + SDocnongrindDetails[i].SDocid + '</label> <table  style="width: 100%" border="0" class="opTable tblhover"><tr><th>Item</th><th>Value</th></tr>';
                } else {
                    appendString += '<div class="col-sm-10 col-lg-10 col-md-10"><div class="row "><div class="col-sm-6 col-lg-6 col-md-6"><label class="SdocCs">' + SDocnongrindDetails[i].SDocid + '</label> <table  style="width: 100%" border="0" class="opTable tblhover"><tr><th>Item</th><th>Value</th></tr>';
                }

                for (var k = 0; k < SDocnongrindDetails[i].values.length; k++) {

                    if (k == SDocnongrindDetails[i].values.length - 1) {
                        appendString += '<tr><td>' + SDocnongrindDetails[i].values[k].Parameter + '</td><td>' + SDocnongrindDetails[i].values[k].Value + '</td></tr></table></div>';
                    } else {
                        appendString += '<tr><td>' + SDocnongrindDetails[i].values[k].Parameter + '</td><td>' + SDocnongrindDetails[i].values[k].Value + '</td></tr>';
                    }
                    SDocnongrindTimeData[nj] = {
                        name: SDocnongrindDetails[i].values[k].Parameter, y: parseFloat(SDocnongrindDetails[i].values[k].Value), color: SDocnongrindTimeDataColor[nj]
                    };
                    nj++;
                }

                appendString += '<div class="col-sm-6 col-lg-6 col-md-6"><div id="nongrindingtimegraph' + i + '" style="overflow: unset; width: 800px; height: 400px"></div></div></div></div>';
                appendString += '<div class="col-sm-1 col-lg-1 col-md-1"></div></div>';
                $('#nongrindingtimeConatiner').append(appendString);
                var nongrindingtime = Highcharts.chart('nongrindingtimegraph' + i, {
                    chart: {
                        type: 'pie',
                        options3d: {
                            enabled: true,
                            alpha: 60,
                            beta: 0
                        },
                        //width: 800,
                        //height: $('#nongrindingtimegraph' + i).height()
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
                $("#nongrindingtimegraph" + i).css('overflow', 'unset');
                var canvasString = "<canvas id=nongrindingtimeCanvas" + i + "></canvas>";
                $('#nongrindingtimeCanvas').append(canvasString);
                var mysvg = nongrindingtime.getSVG();
                var c = document.getElementById('nongrindingtimeCanvas' + i);
                canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
                hdnString += document.getElementById('nongrindingtimeCanvas' + i).toDataURL() + "$";
                $("[id*=hdnNonGrindingTimeGraph]").val(hdnString);
                $("#nongrindingtimegraph" + i).css('overflow', 'unset');
            }
        }


        function bindTotalCycleTimeGraph() {
            var SDoctotalCycleTimeDetails = [];

            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("SDocComparisonPage.aspx/getTotalCycleTimeGraphData") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                 data: '{SDocid1:"' + $('#txtSDocId1').val() + '",SDocid2:"' + $('#txtSDocId2').val() + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                    SDoctotalCycleTimeDetails = dataitem;
                },
                error: function (Result) {
                    // alert("Error");
                }
            });

             //Actual Grinding Time vs  Grinding Time
            $('#totalCycleTimeConatiner').empty();
            $('#totalcycletimeCanvas').empty();
            $('#totalcycletimedrillCanvas').empty();
            var hdnString = "";
            var hdndrillString = "";
            for (var i = 0; i < SDoctotalCycleTimeDetails.length; i++) {
                var SDoctotalCycleTimeData = [];
                var SDoctotalCycleTimeDataColor = ["#000090", "#e67312", "#8fff6f", "#800001", "#1c1c21", "#056aad"];
                var SDocGrindingAndNongrindingTimeData = [];
                var SDocGrindingAndNongrindingTimeDataColor = ["#8fff6f", "#e67312", "#800001", "#000090", "#1c1c21", "#056aad"];
                let tj = 0;
                let gn = 0;
                var appendString = '<div class="row"><div class="col-sm-1 col-lg-1 col-md-1"></div>';
                if (i == 0) {
                    appendString += '<div class="col-sm-10 col-lg-10 col-md-10"> <h4 class="subheader">Total Cycle Time</h4><div class="row "><div class="col-sm-6 col-lg-6 col-md-6"><label class="SdocCs">' + SDoctotalCycleTimeDetails[i].SDocid + '</label> <table  style="width: 100%" border="0" class="opTable tblhover"><tr><th>Item</th><th>Value</th></tr>';
                } else {
                    appendString += '<div class="col-sm-10 col-lg-10 col-md-10"><div class="row "><div class="col-sm-6 col-lg-6 col-md-6"><label class="SdocCs">' + SDoctotalCycleTimeDetails[i].SDocid + '</label> <table  style="width: 100%" border="0" class="opTable tblhover"><tr><th>Item</th><th>Value</th></tr>';
                }

                for (var k = 0; k < SDoctotalCycleTimeDetails[i].values.length; k++) {

                    if (k == SDoctotalCycleTimeDetails[i].values.length - 1) {
                        appendString += '<tr><td>' + SDoctotalCycleTimeDetails[i].values[k].Parameter + '</td><td>' + SDoctotalCycleTimeDetails[i].values[k].Value + '</td></tr></table></div>';
                    } else {
                        appendString += '<tr><td>' + SDoctotalCycleTimeDetails[i].values[k].Parameter + '</td><td>' + SDoctotalCycleTimeDetails[i].values[k].Value + '</td></tr>';
                    }


                    if (SDoctotalCycleTimeDetails[i].values[k].Parameter == "Grinding Time (sec)") {
                        SDoctotalCycleTimeData[tj] = {
                            name: SDoctotalCycleTimeDetails[i].values[k].Parameter, y: parseFloat(SDoctotalCycleTimeDetails[i].values[k].Value), color: SDoctotalCycleTimeDataColor[tj]
                        };
                        tj++;
                    }
                    if (SDoctotalCycleTimeDetails[i].values[k].Parameter == "Actual Grinding Time (sec)") {
                        SDoctotalCycleTimeData[tj] = {
                            name: SDoctotalCycleTimeDetails[i].values[k].Parameter, y: parseFloat(SDoctotalCycleTimeDetails[i].values[k].Value), color: SDoctotalCycleTimeDataColor[tj]
                        };
                        tj++;
                        SDocGrindingAndNongrindingTimeData[gn] = {
                            name: SDoctotalCycleTimeDetails[i].values[k].Parameter, y: parseFloat(SDoctotalCycleTimeDetails[i].values[k].Value), color: SDocGrindingAndNongrindingTimeDataColor[gn]
                        };
                        gn++;
                    }
                    if (SDoctotalCycleTimeDetails[i].values[k].Parameter == "Non Grinding Time (sec)") {
                        SDocGrindingAndNongrindingTimeData[gn] = {
                            name: SDoctotalCycleTimeDetails[i].values[k].Parameter, y: parseFloat(SDoctotalCycleTimeDetails[i].values[k].Value), color: SDocGrindingAndNongrindingTimeDataColor[gn]
                        };
                        gn++;
                    }
                }

                appendString += '<div class="col-sm-6 col-lg-6 col-md-6"><div id="totalcycletimegraph' + i + '" style="overflow: unset; height: 400px; width: 800px"></div></div></div></div>';
                appendString += '<div class="col-sm-1 col-lg-1 col-md-1"></div></div>';
                //$('#calcParameter').append(appendString);
                // appendString += '</table></div><div class="col-sm-5 col-lg-5 col-md-5" style="padding-top: 5px"><div id="SDoctotalcycletimeGraph" style="overflow: unset"></div></div><div class="col-sm-1 col-lg-1 col-md-1"></div>';
                $('#totalCycleTimeConatiner').append(appendString);
                var totalcycletime = Highcharts.chart('totalcycletimegraph' + i, {
                    chart: {
                        type: 'pie',
                        options3d: {
                            enabled: true,
                            alpha: 60,
                            beta: 0
                        },
                        //width: 800,
                        //height: $('#totalcycletimegraph' + i).height()
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
                        //menuItemDefinitions: {
                        //    fullscreen: {
                        //        onclick: function () {
                        //            Highcharts.FullScreen.prototype.init(this.renderTo).attr({ width: 2000 });
                        //        },
                        //        text: 'Full screen'
                        //    }
                        //},
                        //buttons: {
                        //    contextButton: {
                        //        menuItems: ['downloadPNG', 'downloadSVG', 'separator', 'fullscreen']
                        //    }
                        //}
                        enable: true
                    },
                    navigation: {
                        buttonOptions: {
                            align: 'top'
                        }
                    },
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
                
                $("#totalcycletimegraph" + i).css('overflow', 'unset');
                var canvasString = "<canvas id=tctCanvas" + i + "></canvas>";
                $('#totalcycletimeCanvas').append(canvasString);
                var mysvg = totalcycletime.getSVG();
                var c = document.getElementById('tctCanvas' + i);
                canvg(c, mysvg, { ignoreMouse: true, ignoreAnimation: true });
                hdnString += document.getElementById('tctCanvas' + i).toDataURL() + "$";
                $("[id*=hdnTotalCycleTimeGraph]").val(hdnString);
                $("#totalcycletimegraph" + i).css('overflow', 'unset');



                appendString = "";
                appendString = '<div class="row"><div class="col-sm-1 col-lg-1 col-md-1"></div>';
                appendString += '<div class="col-sm-5 col-lg-5 col-md-5"><div id="SdocDrillableCycleTimeConatiner' + i + '" style="width: 700px;height: 400px"></div></div>';
                appendString += '<div class="col-sm-5 col-lg-5 col-md-5"><div id="SdocActualGrindingAndNonGrindingTimeConatiner' + i + '" style="width: 800px; height: 400px; overflow: unset"></div> </div><div class="col-sm-1 col-lg-1 col-md-1"></div></div>';
                $('#totalCycleTimeConatiner').append(appendString);

                //Drillable Cycle Time
                var drillPieData;
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("SDocComparisonPage.aspx/getDrilldownTimeData") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{Sdocid:"' + SDoctotalCycleTimeDetails[i].SDocid + '"}',
                    dataType: "json",
                    success: function (response) {
                        drillPieData = response.d;
                        console.log("drillPieData.listDrilldownSeries =" + drillPieData.listDrilldownSeries);
                    },
                    error: function (Result) {
                        // alert("Error");
                    }
                });
                (function (H) {
                    H.wrap(H.Point.prototype, 'destroy', function (proceed) {
                        if (this.legendItem) { // pies have legend items
                            this.series.chart.legend.destroyItem(this);
                        }
                        proceed.apply(this, Array.prototype.slice.call(arguments, 1));
                    });
                }(Highcharts));
                var drillChart = Highcharts.chart('SdocDrillableCycleTimeConatiner' + i, {

                    chart: {
                        type: 'pie',
                        //events: {
                        //    drilldown: function (e) {
                        //        this.options.legend["enabled"] = true;
                        //    },
                        //    drillup: function (e) {
                        //        this.options.legend["enabled"] = false;
                        //    },

                        //}
                        //width: 600,
                        //height: $('#SdocDrillableCycleTimeConatiner' + i).height()

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
                
                $("#SdocDrillableCycleTimeConatiner" + i).css('overflow', 'unset');
                var drillcanvasString = "<canvas id=tctdrillCanvas" + i + "></canvas>";
                $('#totalcycletimedrillCanvas').append(drillcanvasString);
                var mydrillsvg = drillChart.getSVG();
                var cdrill = document.getElementById('tctdrillCanvas' + i);
                canvg(cdrill, mydrillsvg, { ignoreMouse: true, ignoreAnimation: true });
                hdndrillString += document.getElementById('tctdrillCanvas' + i).toDataURL() + "$";
                $("[id*=hdnDrillChart]").val(hdndrillString);
                $("#SdocDrillableCycleTimeConatiner" + i).css('overflow', 'unset');

                //Actual Grinding Time vs Non Grinding Time
                var ActualGrindingAndNonGrindingTime = Highcharts.chart('SdocActualGrindingAndNonGrindingTimeConatiner' + i, {
                    chart: {
                        type: 'pie',
                        options3d: {
                            enabled: true,
                            alpha: 60,
                            beta: 0
                        },
                        //width: 800,
                        //height: $('#SdocActualGrindingAndNonGrindingTimeConatiner' + i).height()

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
                $("#SdocActualGrindingAndNonGrindingTimeConatiner" + i).css('overflow', 'unset');


            }
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {

            $(document).ready(function () {
                //btnSDocGraphClick();
                bindGrindingTimeGraph();
                 bindNonGrindingTimeGraph();
                 bindTotalCycleTimeGraph();
                bingMRRGraph();
                $('#gvGeneralInfo th:first-child').empty();                $('#gvGeneralInfo th:first-child').append('&nbsp;Items <i class="glyphicon glyphicon-triangle-bottom" style="padding: 2px; font-size: 10px; border: 1px solid silver"></i>');
            });
            $('#gvGeneralInfo th:first-child').click(function () {
                document.getElementById("parameterList").style.visibility = 'visible';
            });

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
        });

        //function checkSDoclimits(val) {
        //    debugger;
        //    let count = 0;
        //    let checkbox = $("#dcSdoclist input");
        //    for (let i = 0; i < checkbox.length; i++) {
        //        if (checkbox[i].checked) {
        //            count++;
        //        }
        //        if (count > 2) {
        //            //$(val).checked = false;
        //            checkbox[i].checked = false;
        //            break;
        //        }
        //    }
        //}
    </script>

</asp:Content>
