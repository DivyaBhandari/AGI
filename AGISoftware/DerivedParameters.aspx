<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DerivedParameters.aspx.cs" Inherits="AGISoftware.DerivedParameters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/Resize/colResizable-1.6.js"></script>
    <script src="Scripts/Resize/colResizable-1.6.min.js"></script>
    <script src="Scripts/jquery.blockUI.js"></script>
    <style>
        input[type="checkbox"] {
            height: 18px;
            width: 18px;
            vertical-align: sub;
        }
        label{
            font-weight: unset;
        }
        #gvSignalProcessfiles tr:first-child {
            display: none;
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
                    max-width: 250px;
                    white-space: nowrap;
                    overflow: hidden;
                    text-overflow: ellipsis;
                    /*max-width: 0px;*/
                }

        @media (max-width : 1448px) {
            #inputFieldsContainer > div > span {
                min-width: 150px;
                max-width: 150px;
                white-space: nowrap;
                /*max-width: 0px;*/
            }
        }

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
                    max-width: 250px;
                    white-space: nowrap;
                    overflow: hidden;
                    text-overflow: ellipsis;
                    /*max-width: 0px;*/
                }
                 #txtRemarks{
                    min-width: 250px;
                      max-width: 250px;
                }
        @media (max-width : 1448px) {
            .inputFieldsContainer > div > span {
                min-width: 150px;
                max-width: 150px;
                white-space: nowrap;
                /*max-width: 0px;*/
            }
             #txtRemarks {
                min-width: 150px;
                max-width: 150px;
            }
        }


        .derivedGrid tr:first-child th {
            position: sticky;
            top: -1px;
            z-index: 2;
        }

        /*.HeaderFixer tr:nth-child(2n+1) td:nth-child(2), .HeaderFixer tr:nth-child(2n) td:nth-child(2) {
            position: sticky;
            left: 0px;
            z-index: 1;
            background-color: white;
        }*/
        /*.HeaderFixer tr:first-child th:nth-child(2) {
            position: sticky;
            left: 0px;
            z-index: 3;
            background-color: #edeef5;
        }*/


        .calculatedLeftfixer tr:nth-child(2n+1) td:first-child, .calculatedLeftfixer tr:nth-child(2n) td:first-child {
            position: sticky;
            left: 0px;
            z-index: 1;
            background-color: white;
        }

        .calculatedLeftfixer tr:first-child th:first-child {
            position: sticky;
            left: 0px;
            z-index: 3;
            background-color: #edeef5;
        }

        .HeaderFixer tr:nth-child(2n+1) td:first-child, .HeaderFixer tr:nth-child(2n) td:first-child {
            position: sticky;
            left: 0px;
            z-index: 1;
            background-color: white;
        }

        .HeaderFixer tr:first-child th:first-child {
            position: sticky;
            left: 0px;
            z-index: 3;
            background-color: #edeef5;
        }

        .HeaderFixer tr:nth-child(2n+1) td:nth-child(2), .HeaderFixer tr:nth-child(2n) td:nth-child(2) {
            position: sticky;
            left: 51px;
            z-index: 1;
            background-color: white;
        }

        .HeaderFixer tr:first-child th:nth-child(2) {
            position: sticky;
            left: 51px;
            z-index: 3;
            background-color: #edeef5;
        }


        span {
            color: #454444;
        }

        .subHeader {
            text-align: center;
            /*background-color: #c3863f;*/
            padding: 3px;
            /*border-radius: 5px;*/
            background-color: #d1f2d0;
            border-radius: 3px;
            font-size: 20px;
            color: #4e4949;
        }

        #tbl tr td {
            padding-left: 4px;
            color: #454444;
            line-height: 2;
            /*white-space: nowrap;*/
            /*width: 25%;*/
        }


        #DerivedParameters {
            /*background-color: #fd6601;*/
            background-color: white;
        }

            #DerivedParameters a {
                color: brown;
            }

        fieldset {
            border: 1px solid #2B7B78;
            padding: 0px;
            border-radius: 4px;
            width: auto;
        }

        .masterFS {
            /*margin: 0 8px 0 8px;*/
            padding: 0 10px 5px 10px;
        }

        legend {
            text-align: left;
            color: white;
            display: block;
            width: auto;
            padding: 0;
            margin-bottom: 0px;
            font-size: 16px;
            line-height: inherit;
            border-bottom: transparent;
        }

        .derivedGrid tr th {
            background-color: #edeef5;
            /*border: 1px solid grey;*/
            padding: 0px 6px;
            font-size: 16px;
            /*white-space: nowrap;*/
            color: #87878a;
            cursor: col-resize;
            border-right: 1px solid #dbdbdb;
        }

        .derivedGrid tr td {
            border-bottom: 1px solid #f3f2f5;
            border-right: 1px solid #f3f2f5;
            padding: 0px 6px;
            text-align: left;
            color: #454444;
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

        .derivedGrid th.resizing {
            cursor: col-resize;
        }

        #inputtbl tr:hover, #inputtbl tr:hover td, #calculatetbl tr:hover, #calculatetbl tr:hover td {
            background-color: #f1f3f4 !important;
        }
        .redColor{
            color: red;
        }
        .blackColor{
           
        }
    </style>

    <div class="container-fluid" style="margin-top: -15px">
        <%--    <asp:UpdatePanel runat="server">
            <ContentTemplate>--%>
        <div class="row" style="color: white;">
            <div class="col-sm-6 col-lg-6 col-md-6" style="padding-left: 5px; padding-right: 5px">
                <%--  <fieldset class="masterFS">                        <legend>&nbsp;<b>Input Parameters</b></legend>--%>
                <h4 class="subHeader">Input Parameters</h4>
                <div>
                    <asp:TextBox runat="server" ID="txtsdocid" Style="display: inline-block; min-width: 340px" ForeColor="Black" ClientIDMode="Static" placeholder="SDoc ID" AutoCompleteType="Disabled" list="SdocList" CssClass="form-control"></asp:TextBox>
                    <datalist id="SdocList" runat="server" clientidmode="static" autopostback="true"></datalist>
                    &nbsp;&nbsp;               
                            <asp:Button runat="server" CssClass="Btns" Style="display: inline-block" Text="Load" ID="load" UseSubmitBehavior="false" OnClientClick="if(!load()){return false};" OnClick="load_Click"  />
                    <%--  <asp:Button runat="server" ID="SaveInputParameter" CssClass="Btns" OnClick="SaveInputParameter_Click" Text="Save" />--%>
                </div>
                <div id="inputContainer" style="overflow: auto; margin-top: 4px; box-shadow: 2px 2px 8px 2px #efe7e7;">
                    <asp:ListView runat="server" ID="lvInputParam" ClientIDMode="Static" >
                        <LayoutTemplate>
                            <table runat="server" id="inputtbl" class="derivedGrid HeaderFixer" border="0" style="width: 200%" clientidmode="static">
                                <tr>
                                    <th>Point</th>
                                    <th>Identifier</th>
                                    <th>Dia (x) (mm)</th>
                                    <th>Stock Diametrically (mm)</th>
                                     <th>Stock on Face (mm)</th>
                                    <th>In Feed (mm/min)</th>
                                    <th>Grinding OD Width (mm) </th>
                                    <th>Feed Angle</th>
                                    <th>Work Speed (m/min) OD</th>
                                    <th>Work Speed (m/min) Face</th>
                                    <th>X-Feed (mm/min)</th>
                                    <th>Z-Feed (mm/min)</th>
                                </tr>
                                <tr id="itemPlaceHolder" runat="server"></tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr style="white-space: nowrap;" runat="server" id="inputtblrow">
                                <td>
                                    <asp:Label runat="server" ID="point" Text='<%# Eval("Point") %>' CssClass='<%# Eval("TangoColor")%>' ></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="identifier" Text='<%# Eval("Identifier") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
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
                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("FeedAngle") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("WorkSpeedOD") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("WorkSpeedFace") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                               
                                <td>
                                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("XFeed") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label5" Text='<%# Eval("ZFeed") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div style="background-color: silver; color: black; font-size: 20px; text-align: center">No Data Found</div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
                <div id="footer">

                    <div class="inputFieldsContainer" style="margin-top: 10px; overflow: auto">

                        <div>
                            <span title="Chip width / thickness ratio">Chip width / thickness ratio</span>&nbsp;
                        <asp:TextBox runat="server" ID="txtChipwidthratio" AutoCompleteType="Disabled" Style="display: inline-block; width: 100px" CssClass="form-control  allowDecimal"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Wheel Tilt Angle (Deg)">Wheel Tilt Angle (Deg)</span>&nbsp;
                         <asp:TextBox runat="server" ID="txtWheeltiltangle" AutoCompleteType="Disabled" Style="display: inline-block; width: 100px" CssClass="form-control allowDecimal"></asp:TextBox>
                        </div>
                    </div>

                    <div style="background-color: #d1f2d0; color: #4e4949; text-align: center; margin-top: 3px; margin-bottom: 3px; border-radius: 3px">
                        <span title="Non Grinding Time (sec)">Grinding Time (sec)</span>&nbsp;
                    </div>
                    <div id="inputFieldsContainer" style="margin-top: 10px; overflow: auto">

                        <div>
                            <span title="Spark Out Time (sec)">Spark Out Time (sec)</span>&nbsp;
                        <asp:TextBox runat="server" ID="txtSparkOutTime" AutoCompleteType="Disabled" Style="display: inline-block; width: 100px" CssClass="form-control  allowDecimal"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Tango / Relief Time (sec)">Tango / Relief Time (sec)</span>&nbsp;
                         <asp:TextBox runat="server" ID="txtTangotTime"  ReadOnly="true" Style="display: inline-block; width: 100px" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Traverse Grinding Time (sec)">Traverse Grinding Time (sec)</span>&nbsp;
                    <asp:TextBox runat="server" ID="txtFeedGrindTime" AutoCompleteType="Disabled" Style="width: 100px; display: inline-block;" CssClass="form-control  allowDecimal"></asp:TextBox>
                        </div>
                    </div>



                    <div style="background-color: #d1f2d0; color: #4e4949; text-align: center; margin-top: 3px; margin-bottom: 3px; border-radius: 3px">
                        <span title="Non Grinding Time (sec)">Non Grinding Time (sec)</span>&nbsp;
                    </div>
                    <div class="inputFieldsContainer" style="overflow: auto">
                        <div>
                            <span title="Slide Forward (sec)">Slide Forward (sec)</span>&nbsp;
                    <asp:TextBox runat="server" ID="txtSlideForward" AutoCompleteType="Disabled" Style="width: 100px; display: inline-block;" CssClass="form-control  allowDecimal"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Program Read (sec)">Program Read (sec)</span>&nbsp;
                    <asp:TextBox runat="server" ID="txtPrgmRead" AutoCompleteType="Disabled" Style="width: 100px; display: inline-block;" CssClass="form-control  allowDecimal"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Flagging (sec)">Flagging (sec)</span>&nbsp;
                    <asp:TextBox runat="server" ID="txtFlagging" AutoCompleteType="Disabled" Style="width: 100px; display: inline-block;" CssClass="form-control  allowDecimal"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Slide Return (sec)">Slide Return (sec)</span>&nbsp;
                    <asp:TextBox runat="server" ID="txtSlideRetuen" AutoCompleteType="Disabled" Style="width: 100px; display: inline-block;" CssClass="form-control  allowDecimal"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Manual Loading / Unloading (sec)">Manual Loading / Unloading (sec)</span>&nbsp;
                    <asp:TextBox runat="server" ID="txtManualLoadUnload" AutoCompleteType="Disabled" Style="width: 100px; display: inline-block;" CssClass="form-control  allowDecimal"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Auto Loading / Unloading (sec)">Auto Loading / Unloading (sec)</span>&nbsp;
                    <asp:TextBox runat="server" ID="txtLoadUnload" AutoCompleteType="Disabled" Style="width: 100px; display: inline-block;" CssClass="form-control  allowDecimal"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Other (sec)">Other (sec)</span>
                            <asp:TextBox runat="server" ID="txtothertimediscription" Placeholder="Note" AutoCompleteType="Disabled" Style="width: 100px; display: inline-block; margin-left: -100px" CssClass="form-control"></asp:TextBox>&nbsp;
                    <asp:TextBox runat="server" ID="txtOther" AutoCompleteType="Disabled" Style="width: 100px; display: inline-block;" CssClass="form-control  allowDecimal"></asp:TextBox>
                        </div>
                         <div>
                            <span title="Dressing Cycle Time (sec)">Dressing Cycle Time (sec)</span>&nbsp;
                    <asp:TextBox runat="server" ID="txtDressingCycleTime" AutoCompleteType="Disabled" ReadOnly="true" Style="width: 100px; display: inline-block;" CssClass="form-control  allowDecimal"></asp:TextBox>
                        </div>
                    </div>

                </div>


            </div>
            <div class="col-sm-6 col-lg-6 col-md-6" style="padding-left: 5px; padding-right: 5px">
                <h4 class="subHeader">Calculated Parameters</h4>
                <asp:Button runat="server" CssClass="Btns" Style="display: inline-block; visibility: hidden" Text="Calculate" ID="Button2" UseSubmitBehavior="false" />
                <%-- <asp:Button runat="server" CssClass="Btns" OnClick="viewFormula_Click" Style="display: inline-block" Text="Calculation Log"  ID="viewFormula" />--%>
                <asp:LinkButton runat="server" CssClass="glyphicon glyphicon-question-sign" UseSubmitBehavior="false" ToolTip="Calculation Log" OnClick="viewFormulaa_Click" Style="display: inline-block; color: #fc3503; float: right; margin-top: 10px; text-decoration: none" ID="viewFormulaa"></asp:LinkButton>

                <div id="outputContainer" style="overflow: auto; margin-top: 4px; box-shadow: 2px 2px 8px 2px #efe7e7;">
                    <asp:ListView runat="server" ID="lvCalculatedPara" ClientIDMode="Static">
                        <LayoutTemplate>
                            <table runat="server" id="calculatetbl" class="derivedGrid calculatedLeftfixer" border="0" style="width: 200%">
                                <tr>
                                   
                                    <th>Point</th>
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
                                    <asp:Label runat="server" ID="point" Text='<%# Eval("Point") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                <asp:HiddenField runat="server" ID="hdTangoOD" Value='<%# Eval("TangoFlagOD") %>'  />
                                <asp:HiddenField runat="server" ID="hdTangoFace" Value='<%# Eval("TangoFlagFace") %>' />
                                 <td>
                                    <asp:Label runat="server" ID="Label11" Text='<%# Eval("RadialDOCX") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label12" Text='<%# Eval("RadialDOCZ") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("MRRX") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label7" Text='<%# Eval("MRRZ") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                              <%--  <td>
                                    <asp:Label runat="server" ID="Label8" Text='<%# Eval("TotalMRRX") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>--%>
                                  <td>
                                    <asp:Label runat="server" ID="Label13" Text='<%# Eval("ToralMRR") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label9" Text='<%# Eval("GritPenetrationDepthX") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="Label10" Text='<%# Eval("GritPenetrationDepthZ") %>' CssClass='<%# Eval("TangoColor")%>'></asp:Label></td>
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
                <div style="margin-top: 10px;">
                    <asp:Button runat="server" CssClass="Btns" Style="display: inline-block" Text="Calculate" UseSubmitBehavior="false" OnClick="calculateBtn_Click" ID="calculateBtn" />
                    &nbsp; &nbsp; &nbsp; 
                   <%-- <span style="margin-left: 20px; color: #216b69; font-weight: bold; font-size: 18px">Lock / Unlock: </span>--%>
                    <asp:CheckBox runat="server" ID="cbLockUnlock" Text="UnLocked" ClientIDMode="Static" Style="font-weight: unset" />
                    &nbsp; 
                      <span style="margin-left: 20px;">Status: </span>
                    <span runat="server" id="sdocCompleteStatus" style=""></span>
                    &nbsp; &nbsp; &nbsp; 
                    <asp:Button runat="server" ID="outputModule" UseSubmitBehavior="false" CssClass="Btns" Style="height: 35px" Text="Process Health Report" OnClick="outputModule_Click" />

                    <div style="background-color: #d1f2d0; color: #4e4949; text-align: center; margin-top: 3px; margin-bottom: 3px; border-radius: 3px">
                        <span>Other Calculations</span>&nbsp;
                    </div>

                    <div class="inputFieldsContainer" style="overflow: auto">
                        <div>
                            <span title="Equivalent Dia for OD (De) (mm)">Equivalent Dia for OD (De) (mm)</span>
                            <asp:TextBox runat="server" ID="txtEquivalentDia" ReadOnly="true" Style="width: 100px; margin-bottom: 10px" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Equivalent Dia Face (mm)">Equivalent Dia Face (mm)</span>
                            <asp:TextBox runat="server" ID="txtEquivalentDiaFace" ReadOnly="true" Style="width: 100px; margin-bottom: 10px" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Cutting edge density (/sq.m.)">Cutting Edge Density (/sq.m.)</span>
                            <asp:TextBox runat="server" ID="txtCuttingEdge" ReadOnly="true" Style="width: 100px; margin-bottom: 10px" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Spark Out Revolutions (rev)">Spark Out Revolutions (rev)</span>
                            <asp:TextBox runat="server" ID="txtSpartOutRev" ReadOnly="true" Style="width: 100px; margin-bottom: 10px" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div style="background-color: #d1f2d0; color: #4e4949; text-align: center; margin-top: 3px; margin-bottom: 3px; border-radius: 3px">
                        <span>Time Calculations</span>&nbsp;
                    </div>
                    <div class="inputFieldsContainer" style="overflow: auto">
                        <div>
                            <span title="Grinding Cycle Time (sec)">Grinding Time (sec)</span>
                            <asp:TextBox runat="server" ID="txtGrindcycletime" ReadOnly="true" Style="width: 100px; margin-bottom: 10px" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Non Grinding Time (sec)">Non Grinding Time (sec)</span>
                            <asp:TextBox runat="server" ID="txtNongrindingtime" ReadOnly="true" Style="width: 100px; margin-bottom: 10px" CssClass="form-control"></asp:TextBox>
                        </div>
                         <div>
                            <span title="Total Grinding Time (sec)">Total Grinding Time (sec)</span>
                            <asp:TextBox runat="server" ID="txtTotalGrindingTime" ReadOnly="true" Style="width: 100px; margin-bottom: 10px" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div>
                            <span title="Total Cycle Time (sec)">Total Cycle Time (sec)</span>
                            <asp:TextBox runat="server" ID="txtTotalcycletime" ReadOnly="true" Style="width: 100px; margin-bottom: 10px" CssClass="form-control"></asp:TextBox>
                        </div>
                         <div>
                            <span title="Floor to Floor Time (sec)">Floor to Floor Time (sec)</span>
                            <asp:TextBox runat="server" ID="txtFloortoFloor" ReadOnly="true" Style="width: 100px; margin-bottom: 10px" CssClass="form-control"></asp:TextBox>
                        </div>
                         <div>
                            
                            <asp:TextBox runat="server" ID="txtRemarks" ClientIDMode="Static" Placeholder="Note" AutoCompleteType="Disabled" Style="margin-bottom: 10px" CssClass="form-control"></asp:TextBox>
                              <%--<asp:TextBox runat="server" ID="TextBox1" Placeholder="Note" AutoCompleteType="Disabled" Style="visibility: hidden; margin-bottom: 10px" CssClass="form-control"></asp:TextBox>--%>
                              <%--<span></span>--%>
                        </div>
                    </div>

                    <div class="text-left" style="margin-top: 13px; display: flex">

                        <a runat="server" id="Button1" onclick="return signalClick();" style="height: 35px; display: inline-block; text-decoration: none" class="Btns">
                            <i class="fas fa-cloud-upload-alt"></i>&nbsp;&nbsp;&nbsp;Upload Signal File </a>
                        <asp:FileUpload runat="server" ID="selectSignalfile" ClientIDMode="Static" AllowMultiple="true" CssClass="form-control Btns" Style="color: #454444; display: none" />
                        <br />
                        <asp:Button runat="server" ID="SaveSignalfile" UseSubmitBehavior="false" OnClick="SaveSignalfile_Click" CssClass="Btns" Style="margin-left: 4px; display: none" Text="Save Signal File" />
                        <asp:GridView runat="server" ID="gvSignalProcessfiles" ClientIDMode="Static" AutoGenerateColumns="false" OnRowEditing="gvSignalProcessfiles_RowEditing" OnRowDeleting="gvSignalProcessfiles_RowDeleting" Style="display: inline-block; border: 0; margin-left: 12px">
                            <Columns>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ClientIDMode="Static" ID="lbfilename" Text='<%# Bind("signalfilename") %>' Style="display: inline; vertical-align: middle; color: black" ToolTip="View Signal File" CommandName="Edit"></asp:LinkButton>
                                        <asp:HiddenField runat="server" ID="hdfilepath" Value='<%# Eval("signalfilepath") %>' />
                                        &nbsp;
                                            <asp:LinkButton runat="server" ClientIDMode="Static" CssClass="glyphicon glyphicon-remove-circle" ID="removeSignalProcessfile" CommandName="Delete" Style="color: #e86b6b; display: inline; vertical-align: middle;" ToolTip="Remove Signal filepath"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
        <%--  <div class="row">            <div class="col-sm-12 col-lg-12 col-md-12 text-center" style="display: inline-block; margin-top: 30px">                  <asp:Button runat="server" ID="calculate" style="display:inline-block" Text="Calculate" CssClass="Btns" />&nbsp;&nbsp;                  <span>Please select the signal file </span>                  <asp:FileUpload runat="server" CssClass="form-control" style="display:inline-block"/>&nbsp;&nbsp;                <span style="margin-left: 10px; color: white; font-size: 20px">ID</span>                <asp:TextBox runat="server" CssClass="form-control" Style="display: inline-block; width: 200px"></asp:TextBox>&nbsp;&nbsp;                  <asp:Button runat="server" ID="createSDoc" style="display:inline-block" Text="Create SDoc" CssClass="Btns" />            </div>        </div>--%>
    </div>

    <div class="modal fade" id="lockConfirmation" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/confirm.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="lockConfirmationmessageText" style="font-size: 17px;">Are you sure, you want to Lock this SdocId?</span>

                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <input type="button" value="Yes" class="btn btn-info" id="lockConfirmationYes" onserverclick="lockConfirmationYes_ServerClick" runat="server" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="No" class="btn btn-info" id="lockConfirmationNo" onclick="lockConfirmationNoClick()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
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

    <div class="modal fade" id="removeSignalfilepathModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/confirm.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="removeSignalfilepathconfirmationmessageText" style="font-size: 17px;">Are you sure, you want to remove this Signal file path?</span>
                    <span id="removeImageNameValue" runat="server" style="visibility: hidden"></span>
                    <span id="removeImagePathValue" runat="server" style="visibility: hidden"></span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <%--<asp:Button runat="server" Text="Close" ID="Button1" CssClass="btn btn-info" BackColor="#5D7B9D" ForeColor="white" />--%>
                    <%--  onserverclick="saveConfirmYes_ServerClick"--%>
                    <input type="button" value="Yes" class="btn btn-info" id="removeImageConfimYes" onserverclick="removeImageConfimYes_ServerClick" runat="server" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="No" class="btn btn-info" id="removeImageConfirmNo" onclick="removeImageNo()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
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



    <script>        $(document).ready(function () {            // var wHeight = $(window).height() - (350);            let footerht = $("#footer").height();            var wHeight = $(window).height() - (footerht + 28 + 36 + 61 + 50 + 40);            $('#inputContainer').css('height', wHeight);            $('#outputContainer').css('height', wHeight);        });        var pressed = false;        var start = undefined;        var startX, startWidth;        $(".derivedGrid th").mousedown(function (e) {            start = $(this);            pressed = true;            startX = e.pageX;            startWidth = $(this).width();            $(start).addClass("resizing");            $(start).addClass("noSelect");        });        $(document).mousemove(function (e) {            if (pressed) {                $(start).width(startWidth + (e.pageX - startX));            }        });        $(document).mouseup(function () {            if (pressed) {                $(start).removeClass("resizing");                $(start).removeClass("noSelect");                pressed = false;            }        });        $("#cbLockUnlock").change(function () {            if ($("#cbLockUnlock").is(':Checked')) {
                $('[id*=lockConfirmation]').modal('show');
            }

        });        function lockConfirmationNoClick() {            $('[id*=lockConfirmation]').modal('hide');
            $("#cbLockUnlock").prop("checked", false);
        }        function openWarningModal(msg) {
            $('[id*=myWarningModal]').modal('show');
            $("#warningmessageText").text(msg);
        }        function removeSignalFilepathConfirm() {            $("[id*=removeSignalfilepathModal]").modal('show');
            return false;
        }        function removeImageNo() {            $("[id*=removeSignalfilepathModal]").modal('hide');
        }        function signalClick() {
            document.getElementById("selectSignalfile").click();
            return false;
        }        function UploadSignalFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=SaveSignalfile.ClientID %>").click();
            }
        }        function load() {            if ($("#txtsdocid").val() == "") {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("SDoc ID is required.");
                return false;
            }
            else {
                return true;
            }
        }        $(window).resize(function () {            //var wHeight = $(window).height() - (350);            let footerht = $("#footer").height();            var wHeight = $(window).height() - (footerht + 28 + 36 + 61 + 50 + 40);            $('#inputContainer').css('height', wHeight);            $('#outputContainer').css('height', wHeight);        });        $('.allowDecimal').keypress(function (evt) {

            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            var decimalno = $(this).val().split('.')[1];

            if ($(this).val().length > 2 && charCode != 46) {
                if ($(this).val().indexOf('.') == -1) {
                    return false;
                } else if (decimalno != undefined) {
                    if (decimalno.length > 3)
                        return false;
                }

            } else if (charCode == 46 && $(this).val().indexOf('.') != -1) {
                return false;
            }

            else if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        });        $('.allowDecimalwithoperator').keypress(function (evt) {
            debugger;
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            var pos = evt.target.selectionStart;
            if (charCode == 45 && pos != 0) {
                return false;
            } else if (charCode == 46 && $(this).val().indexOf('.') != -1) {
                return false;
            } else if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57) && charCode != 45) {
                return false;
            }
            return true;
        });        function showpop5(msg, title) {
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
        }        function openErrorModal(msg) {
            $('[id*=myErrorModal]').modal('show');
            $("#errormessageText").text(msg);
        }        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {            $("#cbLockUnlock").change(function () {                if ($("#cbLockUnlock").is(':Checked')) {
                    $('[id*=lockConfirmation]').modal('show');
                }
            });            var pressed = false;            var start = undefined;            var startX, startWidth;            $("#inputtbl th").mousedown(function (e) {                start = $(this);                pressed = true;                startX = e.pageX;                startWidth = $(this).width();                $(start).addClass("resizing");                $(start).addClass("noSelect");            });            $(document).mousemove(function (e) {                if (pressed) {                    $(start).width(startWidth + (e.pageX - startX));                }            });            $(document).mouseup(function () {                if (pressed) {                    $(start).removeClass("resizing");                    $(start).removeClass("noSelect");                    pressed = false;                }            });            // var wHeight = $(window).height() - (350);            let footerht = $("#footer").height();            var wHeight = $(window).height() - (footerht + 28 + 36 + 61 + 50 + 40);            $('#inputContainer').css('height', wHeight);            $('#outputContainer').css('height', wHeight);            $(window).resize(function () {                // var wHeight = $(window).height() - (350);                let footerht = $("#footer").height();                var wHeight = $(window).height() - (footerht + 28 + 36 + 61 + 50 + 40);                $('#inputContainer').css('height', wHeight);                $('#outputContainer').css('height', wHeight);            });            function load() {                if ($("#txtsdocid").val() == "") {
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("SDoc ID is required.");
                    return false;
                }
                else {
                    return true;
                }
            }            function openWarningModal(msg) {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text(msg);
            }            $('.allowDecimal').keypress(function (evt) {

                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var decimalno = $(this).val().split('.')[1];

                if ($(this).val().length > 2 && charCode != 46) {
                    if ($(this).val().indexOf('.') == -1) {
                        return false;
                    } else if (decimalno != undefined) {
                        if (decimalno.length > 3)
                            return false;
                    }

                } else if (charCode == 46 && $(this).val().indexOf('.') != -1) {
                    return false;
                }

                else if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            });            function showpop5(msg, title) {
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
            }            function openErrorModal(msg) {
                $('[id*=myErrorModal]').modal('show');
                $("#errormessageText").text(msg);
            }
        });    </script>
</asp:Content>
