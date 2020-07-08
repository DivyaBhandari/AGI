<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataInputModule.aspx.cs"  EnableEventValidation="false" Inherits="AGISoftware.Contentpage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Scripts/ModalDragNew/jquery-ui.min.css" rel="stylesheet" />
    <script src="Scripts/ModalDragNew/jquery-ui.min.js"></script>
   <%--  <script src="Scripts/Datetimepicker/bootstrap-datepicker.js"></script>
    <link href="Scripts/Datetimepicker/bootstrap-datepicker3.css" rel="stylesheet" />
   <script src="Scripts/Datetimepicker/moment-with-locales.js"></script>
    <link href="Scripts/Datetimepicker/bootstrap-datetimepicker.css" rel="stylesheet" />
    <script src="Scripts/Datetimepicker/bootstrap-datetimepicker.js"></script>--%>
     
   
        <style>
            .tooltipclass{
                background-color: #2b2a2a;
                color: white;
                border-color: #2b2a2a;
                min-width: 150px;
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
        #ulQualityParameter div:first-child {
            position: sticky;
            top: 0px;
            background-color: rgb(245, 245, 245);
            z-index: 2;
        }

            #ulQualityParameter div:nth-child(2) {
                position: sticky;
                top: 17px;
                /*background-color: rgb(245, 245, 245);*/
                z-index: 1;
            }

        /*#ulOperationalParameterGrind div:nth-child(2n) {
            padding-left: 0px;
        }

            #ulOperationalParameterGrind div:nth-child(2n) #opItem {
                width: 50% !important;
                min-width: 50% !important;
            }*/                                                                     

        .calculatedBackcolor {
            background-color: #d3e6e6;
        }

        .opimages {
            width: 20%;
        }

        .opspan {
            max-width: 17%;
            min-width: 17%;
        }

        .opcs {
            width: 70%;
            display: inline;
        }

        #qpheader li, #ulOperationalParameter li {
            display: inline;
            float: left;
        }


        .fieldDiv {
            border: none;
            padding: 2px 10px 2px 10px;
            bottom: -1px;
        }



        .recommandation {
            color: red;
            font-size: 18px;
        }

        .menuData {
            border: 1px solid #3c3b54;
            border-radius: 5px;
            margin-right: 5px;
        }

        #inputContainer {
            box-shadow: 2px 2px 8px 2px #efe7e7;
        }

        .rowChange {
            margin: 0px;
        }

        input[type="checkbox"] {
            height: 18px;
            width: 18px;
            vertical-align: sub;
        }

        .navbar-inverse {
            background-color: white;
            border-color: white;
        }

        .navbar .navbar-nav > .active > a:focus {
            background-color: #fd6601;
        }

        .navbar-nav {
            background-color: white;
            border-color: #3c3b54;
            border-radius: 5px;
        }

        .collapse > .navbar-nav > li > a {
            color: #3c3b54;
            /*font-size: 17px;*/
            /*font-weight: bold;*/
            padding: 4px;
        }


            .collapse > .navbar-nav > li > a:hover {
                background-color: #3c3b54;
                color: white;
            }


        .tablegrid {
            /*width: 100%;*/
            background-color: white;
            border-radius: 5px;
            margin-left: 0px;
            /*padding: 10px;*/
        }

        .inputTbl {
            width: 90%;
            margin: auto;
        }




            .inputTbl tr td {
                padding: 5px 10px;
                /*color: white;*/
                color: #454444;
                font-size: 15px;
            }


        .navbar-inverse .navbar-toggle .icon-bar {
            background-color: red;
        }

        .navbar-inverse .navbar-toggle {
            border-color: white;
            /*background-color: white;
            color: red;*/
        }

       #DataInputModule {
            /*background-color: #fd3801;*/
            background-color: white;
        }

           #DataInputModule  a {
                color: brown;
            }
           .mandatory{
               font-size: 10px;
               vertical-align: top;
           }
           /*#ulOperationalParameterGrind div:nth-child(2n+1){
               width: 42%;
           }
            #ulOperationalParameterGrind div:nth-child(2n){
               width: 38%;
           }
            #ulOperationalParameterGrind div:nth-child(2n+1) span {
                min-width: 380px;
            }
            #ulOperationalParameterGrind div:nth-child(2n) span {
                min-width: 50px;
            }*/
    </style>
    <asp:HiddenField ID="uploadImagePath" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="uploadImageName" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnScrollPos" ClientIDMode="Static" runat="server" />
    <asp:HiddenField runat="server" ID="UploadedImages" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="removeImage" ClientIDMode="Static" />
 
    
    <div class="navbar navbar-inverse" style="margin-bottom: 0px">
        <div class="container-fluid" style="padding-left: 0px">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <%--   <a class="navbar-brand" runat="server" href="#" style="color: black;">Application name</a>--%>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav nextPrevious" >
                    <li class="active"><a runat="server"  id="generalInfo" class="menuData" data-toggle="tab" href="#menu0">General Information</a> </li>
                    <li><a runat="server" class="menuData" id="machineTool"  data-toggle="tab" href="#menu1">Machine Tool</a></li>
                    <li><a runat="server" class="menuData"  id="consumables" data-toggle="tab" href="#menu2">Consumables</a></li>
                    <li><a runat="server" class="menuData" id="workpiece" data-toggle="tab" href="#menu3">Workpiece</a></li>
                    <li><a runat="server" class="menuData" id="operationalParam" data-toggle="tab" href="#menu4">Operational Parameters</a></li>
                    <li><a runat="server" class="menuData" id="qualityParam"  data-toggle="tab" href="#menu5">Quality Parameters</a></li>
                    <li> &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<i class="glyphicon glyphicon-star" style="color: red; font-size: 10px; vertical-align: baseline"><span> Mandatory Fields</span></i></li>
                     <li>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <i class="glyphicon glyphicon-star" style="color: green;font-size: 10px; vertical-align: baseline"><span> Important Fields</span></i></li>
                </ul>
                
            </div>
        </div>
    </div>
         
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid" style="margin-bottom: 10px">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtViewSdocid" Style="display: inline-block; min-width: 340px" CssClass="form-control" placeholder="SDoc ID" AutoCompleteType="Disabled" list="SdocList" ></asp:TextBox>
                          <datalist id="SdocList" runat="server" clientidmode="static" autopostback="true"></datalist>
                        <asp:Button runat="server" Text="View" ID="viewInputModule"  CssClass="Btns" OnClick="viewInputModule_Click" UseSubmitBehavior="false" OnClientClick="if(!viewInputModule()){return false};"  Style="margin-left: 8px; padding-left: 14px; padding-right: 14px; display: inline-block" />
                        <%-- OnClick="saveInputModule_Click"--%>
                        <asp:Button runat="server" Text="Save As" ID="saveInputModule"  Visible="false" UseSubmitBehavior="false" OnClientClick="return saveAsInputModule();" CssClass="Btns" Style="margin-left: 8px; padding-left: 14px; padding-right: 14px; display: inline-block" />
                        <%--   OnClick="editInputModule_Click" --%>
                        <asp:Button runat="server" Text="Save" ID="editInputModule" ClientIDMode="Static"  UseSubmitBehavior="false" OnClientClick="return saveInputModuleFun();" CssClass="Btns" Style="margin-left: 8px; padding-left: 14px; padding-right: 14px; display: inline-block" />
                        <%--   <i class="glyphicon glyphicon-cog" id="themebtn" style="color: white; margin-left: 7px; font-size: 17px; display: inline-block"></i>--%>

                        <asp:Button runat="server" Text="Edit" ID="allowEdit" OnClick="allowEdit_Click"  UseSubmitBehavior="false" CssClass="Btns" Style="margin-left: 8px; padding-left: 14px; padding-right: 14px; display: inline-block" />

                            <asp:Button runat="server" Text="Clear" ID="Clear"  UseSubmitBehavior="false" OnClientClick="if(!ClearClick()){return false};" OnClick="Clear_Click" CssClass="Btns" Style="margin-left: 8px; padding-left: 14px; padding-right: 14px; display: inline-block" />

                         <asp:Button runat="server" Text="Calculate" ID="btnCalculation"  UseSubmitBehavior="false" OnClientClick="return allParameterCalculation();"  CssClass="Btns" Style="margin-left: 8px; padding-left: 14px; padding-right: 14px; display: inline-block" />

                         <asp:Button runat="server" ID="btnDeleteSDoc" UseSubmitBehavior="false" Style="margin-left: 8px; padding-left: 14px; padding-right: 14px; display: inline-block" CssClass="Btns" Visible="false" Text="Delete" OnClientClick="return showConfirmationForDeleteSDoc();" />

                         <%-- <asp:Button runat="server" Text="Template" ID="btnTemplate"  Visible="true" UseSubmitBehavior="false" OnClientClick="return templateSave();" CssClass="Btns" Style="margin-left: 8px; padding-left: 14px; padding-right: 14px; display: inline-block" />--%>
                        <asp:CheckBox runat="server" ID="cbTemplate" ClientIDMode="Static" Visible="true" Text="Template"  Style="margin-left: 8px; display: inline-block" />
                        <%--onchange="enableDisableSaveButton()"--%>
 
                      <%--  <asp:Button runat="server" Text="Cycle Design Parameters (OD)" UseSubmitBehavior="false" OnClick="cycleDesignParameters_Click" ID="cycleDesignParameters" CssClass="Btns" Style="margin-left: 8px; padding-left: 14px; padding-right: 14px; display: inline-block" />--%>
                       <a href="DerivedParameters.aspx" class="Btns" style="margin-left: 8px; padding-left: 14px; padding-right: 14px; display: inline-block; text-decoration:none; color: white;" >Cycle Design Parameters (OD)</a>

                        <span runat="server" id="lockstatus" style="margin-left: 8px; display: inline-block; font-weight: bold">Status: Locked</span>

                        <span style="float: right; margin-right: 30px">
                            <%--  <img src="Images/next1.png" style="width: 30px; margin-right: 20px; transform: rotate(180deg);" class=" back" />
                            <img src="Images/next1.png" style="width: 30px;" class=" continue" />--%>
                            <i class="glyphicon glyphicon-chevron-left back" style="color: #3c3b54; font-size: 20px; vertical-align: middle"></i>
                            &nbsp;  &nbsp;  &nbsp;
                            <i class="glyphicon glyphicon-chevron-right continue" style="color: #3c3b54; font-size: 20px; vertical-align: middle"></i>
                        </span>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="viewInputModule" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="allowEdit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="saveASOK" EventName="serverclick" />
             <asp:AsyncPostBackTrigger ControlID="templateCreate" EventName="serverclick" />
            <asp:AsyncPostBackTrigger ControlID="templateornewSdocOK" EventName="serverclick" />
            <asp:AsyncPostBackTrigger ControlID="saveConfirmYes" EventName="serverclick" />
            <asp:AsyncPostBackTrigger ControlID="removeImageConfimYes" EventName="serverclick" />
             <asp:AsyncPostBackTrigger ControlID="deleteSdocYes" EventName="serverclick" />
            <asp:AsyncPostBackTrigger ControlID="leaveunsavedDataWarning" EventName="serverclick" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="inputModuleUP" ClientIDMode="Static" runat="server">
        <ContentTemplate>

            <div class="tab-content themetoggle" id="inputContainer" style="overflow: auto; margin-left: 10px; background-color: #f5f5f5">
                <div id="menu0" class="tab-pane fade">
                    <div class="row text-left rowChange">
                        <asp:ListView runat="server" ID="lvGeneralInfo" ClientIDMode="Static">
                            <LayoutTemplate>

                                <ul runat="server" id="lstVendors" style="text-align: center; width: 98%; margin: auto;">
                                    <li runat="server" id="itemPlaceholder" />
                                </ul>
                            </LayoutTemplate>

                            <ItemTemplate>
                             
                                <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 45%; margin-right: 20px">

                                    <asp:Label runat="server" CssClass="toggleColor" ID="item" Width="200px" Style="min-width: 200px; color: #454444; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;" Text='<%# Eval("CustomeName") %>' ToolTip='<%# Eval("CustomeName") %>'></asp:Label>
                                     <asp:HiddenField runat="server" ID="gihdParameterName" Value='<%# Eval("Prameter") %>' />
                                    <asp:TextBox runat="server" ID="gitxtvalue" onmouseover="return showRecommendation(this,'GeneralInfo');" onfocus="return showRecommendation(this,'GeneralInfo');" onblur="return ExceedRangeWarning(this,'GeneralInfo');" Style="width: 40%; min-width: 20%" CssClass="form-control " ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="gitxtallowNumeric" onkeypress="return allowNumberic(event);" onblur="return ExceedRangeWarning(this,'GeneralInfo');" onmouseover="return showRecommendation(this,'GeneralInfo');" onfocus="return showRecommendation(this,'GeneralInfo');" Style="width: 40%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="gitxtallowalphaNumeric" onkeypress="return allowAlphaNumber(event);" onblur="return ExceedRangeWarning(this,'GeneralInfo');" onmouseover="return showRecommendation(this,'GeneralInfo');" onfocus="return showRecommendation(this,'GeneralInfo');" Style="width: 40%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="gitxtallowDecimal" onmouseover="return showRecommendation(this,'GeneralInfo');" onfocus="return showRecommendation(this,'GeneralInfo');" onblur="return ExceedRangeWarning(this,'GeneralInfo');" Style="width: 40%; min-width: 20%" CssClass="form-control allowDecimal" ClientIDMode="Static"></asp:TextBox>
                                     <asp:TextBox runat="server" ID="gitxtDate" TextMode="DateTimeLocal" onblur="return DateTimeFormateCheck(this);" onmouseover="return showRecommendation(this,'GeneralInfo');" onfocus="return showRecommendation(this,'GeneralInfo');" Style="width: 40%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="giComments" TextMode="MultiLine" onblur="return ExceedRangeWarning(this,'GeneralInfo');" onmouseover="return showRecommendation(this,'GeneralInfo');" onfocus="return showRecommendation(this,'GeneralInfo');" Style=" min-width: 20%;width: 40%; resize: vertical" CssClass="form-control  allow200ChComment" ClientIDMode="Static"></asp:TextBox>
                                    <%-- </div>
                                        <div class="fieldDiv" style="width: 150px; margin-right: 15px; padding: 0px">--%>

                                    <asp:CheckBox runat="server" ID="gicbvalue" ClientIDMode="Static" />
                                    <asp:DropDownList runat="server" ID="giddlvalue" onchange="return bindDependancyValue(this,'GeneralInfo')" onmouseover="return showRecommendation(this,'GeneralInfo');" onfocus="return savePreviousdata(this,'GeneralInfo');" Style="width: 40%" CssClass="form-control " ClientIDMode="Static">
                                    </asp:DropDownList>
                                     <asp:HiddenField runat="server" ID="hfddlvalue" Value='<%# Eval("Value") %>' />
                                    <asp:HiddenField runat="server" ID="hdDependancyFlag" Value='<%# Eval("DependancyFlag") %>' />
                                     <asp:HiddenField runat="server" ID="hdDependency" Value='<%# Eval("Dependancy") %>' />
                                     <asp:HiddenField runat="server" ID="hdIndependentParameter" Value='<%# Eval("IndependentParameter") %>' />
                                      <asp:HiddenField runat="server" ID="hdMandatory" Value='<%# Eval("Mandatory") %>' />

                                    <asp:HiddenField runat="server" ID="giLimitRange" Value='<%# Eval("LimitRange") %>' />
                                    <asp:HiddenField runat="server" ID="giRecommandation" Value='<%# Eval("Recommendation") %>' />
                                    <asp:Label runat="server" ID="giMandatory" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>
                                    <asp:Image runat="server" ID="giimgRecommendation" Width="30px" Height="30px" onclick="showLargeImage(this)" Style="margin-left: 2px" ImageUrl='<%#Eval("Image") %>' />
                                    <asp:Label runat="server" ID="giParameterID" Text='<%# Eval("PrameterId") %>' Visible="false"></asp:Label>
                                     <asp:HiddenField runat="server" ID="hfParameterID" Value='<%# Eval("PrameterId") %>' />
                                    <asp:Label runat="server" ID="giDateType" Text='<%# Eval("Datatype") %>' Visible="false"></asp:Label>
                                       <asp:HiddenField runat="server" ID="hiddenDatatype" Value='<%# Eval("Datatype") %>' />
                                    <asp:Label runat="server" ID="giobjectType" Text='<%# Eval("ObjectType") %>' Visible="false"></asp:Label>
                                     <asp:Label runat="server" ID="gicalculatedflag" Text='<%# Eval("CalculatedFlag") %>' Visible="false"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div id="menu1" class="tab-pane fade">
                    <div class="row text-left rowChange">
                        <asp:ListView runat="server" ID="lvMachinetool" ClientIDMode="Static">
                            <LayoutTemplate>

                                <ul runat="server" id="ulMachinetool" style="text-align: center; width: 98%; margin: auto;">
                                    <li runat="server" id="itemPlaceholder" />
                                </ul>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 48%; margin-right: 20px">

                                    <asp:Label runat="server" CssClass="toggleColor" ID="mtItem" Width="285px" Style="min-width: 285px; color: #454444; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;" Text='<%# Eval("CustomeName") %>' ToolTip='<%# Eval("CustomeName") %>'></asp:Label>
                                    <asp:HiddenField runat="server" ID="mthdParameterName" Value='<%# Eval("Prameter") %>' />
                                    <asp:TextBox runat="server" ID="mttxtvalue" onmouseover="return showRecommendation(this,'MachineTool');" onfocus="return showRecommendation(this,'MachineTool');" onblur="return ExceedRangeWarning(this,'MachineTool')" Style="width: 30%; min-width: 20%" CssClass="form-control " ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="mttxtallowNumeric" onmouseover="return showRecommendation(this,'MachineTool');" onfocus="return showRecommendation(this,'MachineTool');" onkeypress="return allowNumberic(event);" onblur="return ExceedRangeWarning(this,'MachineTool')" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="mttxtallowalphaNumeric" onmouseover="return showRecommendation(this,'MachineTool');" onfocus="return showRecommendation(this,'MachineTool');" onkeypress="return allowAlphaNumber(event);" onblur="return ExceedRangeWarning(this,'MachineTool')" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="mttxtallowDecimal" onmouseover="return showRecommendation(this,'MachineTool');" onfocus="return showRecommendation(this,'MachineTool');" onblur="return ExceedRangeWarning(this,'MachineTool')" Style="width: 30%; min-width: 20%" CssClass="form-control allowDecimal" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="mttxtDate" onmouseover="return showRecommendation(this,'MachineTool');" onfocus="return showRecommendation(this,'MachineTool');" TextMode="DateTimeLocal" onblur="return DateTimeFormateCheck(this);" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>

                                    <asp:CheckBox runat="server" ID="mtcbvalue" ClientIDMode="Static" />
                                    <asp:DropDownList runat="server" ID="mtddlvalue" onchange="return bindDependancyValue(this,'MachineTool')" onmouseover="return showRecommendation(this,'MachineTool');" onfocus="return savePreviousdata(this,'MachineTool');" Style="width: 30%; min-width: 20%" CssClass="form-control " ClientIDMode="Static">
                                    </asp:DropDownList>
                                    
                                    <asp:HiddenField runat="server" ID="hfddlvalue" Value='<%# Eval("Value") %>' />
                                    <asp:HiddenField runat="server" ID="hdDependancyFlag" Value='<%# Eval("DependancyFlag") %>' />
                                     <asp:HiddenField runat="server" ID="hdDependency" Value='<%# Eval("Dependancy") %>' />
                                     <asp:HiddenField runat="server" ID="hdIndependentParameter" Value='<%# Eval("IndependentParameter") %>' />
                                      <asp:HiddenField runat="server" ID="hdMandatory" Value='<%# Eval("Mandatory") %>' />

                                    <asp:HiddenField runat="server" ID="mtLimitRange" Value='<%# Eval("LimitRange") %>' />
                                    <asp:HiddenField runat="server" ID="mtRecommandation" Value='<%# Eval("Recommendation") %>' />

                                    <asp:Label runat="server" ID="mtMandatory" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>
                                    <asp:Image runat="server" ID="mtimgRecommendation" Width="30px" Height="30px" onclick="showLargeImage(this)" Style="margin-left: 2px" ImageUrl='<%#Eval("Image") %>' />
                                    <asp:Label runat="server" ID="mtParameterID" Text='<%# Eval("PrameterId") %>' Visible="false"></asp:Label>
                                      <asp:HiddenField runat="server" ID="hfParameterID" Value='<%# Eval("PrameterId") %>' />
                                    <asp:Label runat="server" ID="mtDateType" Text='<%# Eval("Datatype") %>' Visible="false"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hiddenDatatype" Value='<%# Eval("Datatype") %>' />
                                    <asp:Label runat="server" ID="mtobjectType" Text='<%# Eval("ObjectType") %>' Visible="false"></asp:Label>
                                     <asp:Label runat="server" ID="mtcalculatedflag" Text='<%# Eval("CalculatedFlag") %>' Visible="false"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div id="menu2" class="tab-pane fade">
                    <div class="row text-left rowChange" style="overflow: auto">
                        <asp:ListView runat="server" ID="lvConsumable" ClientIDMode="Static">
                            <LayoutTemplate>

                                <ul runat="server" id="ulConsumable" style="text-align: center; width: 98%; margin: auto;">
                                    <li runat="server" id="itemPlaceholder" />
                                </ul>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 45%; margin-right: 20px">

                                    <asp:Label runat="server" CssClass="toggleColor" ID="cmItem" Width="285px" Style="min-width: 285px; color: #454444; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;" Text='<%# Eval("CustomeName") %>' ToolTip='<%# Eval("CustomeName") %>'></asp:Label>
                                    <asp:HiddenField runat="server" ID="cmhdParameterName" Value='<%# Eval("Prameter") %>' />
                                    <asp:TextBox runat="server" ID="cmtxtvalue" onmouseover="return showRecommendation(this,'Consumable');" onfocus="return showRecommendation(this,'Consumable');" onblur="return OPDressingParametersCalculations(this,'Consumables');" Style="width: 30%; min-width: 20%" CssClass="form-control " ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="cmtxtallowNumeric" onmouseover="return showRecommendation(this,'Consumable');" onfocus="return showRecommendation(this,'Consumable');" onkeypress="return allowNumberic(event);" onblur="return OPDressingParametersCalculations(this,'Consumables');" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="cmtxtallowalphaNumeric" onkeypress="return allowAlphaNumber(event);" onblur="return OPDressingParametersCalculations(this,'Consumables');" onmouseover="return showRecommendation(this,'Consumable');" onfocus="return showRecommendation(this,'Consumable');" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="cmtxtallowDecimal" onblur="return OPDressingParametersCalculations(this,'Consumables');" Style="width: 30%; min-width: 20%" onmouseover="return showRecommendation(this,'Consumable');" onfocus="return showRecommendation(this,'Consumable');"  CssClass="form-control allowDecimal" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="cmtxtDate" onmouseover="return showRecommendation(this,'Consumable');" onfocus="return showRecommendation(this,'Consumable');" TextMode="DateTimeLocal" onblur="return DateTimeFormateCheck(this);" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:CheckBox runat="server" ID="cmcbvalue" ClientIDMode="Static" />
                                    <asp:DropDownList runat="server" ID="cmddlvalue" onchange="return bindDependancyValue(this,'Consumable')"  onmouseover="return showRecommendation(this,'Consumable');" onfocus="return savePreviousdata(this,'Consumable');"  Style="width: 30%; min-width: 20%" CssClass="form-control " ClientIDMode="Static">
                                    </asp:DropDownList>

                                     <asp:HiddenField runat="server" ID="hfddlvalue" Value='<%# Eval("Value") %>' />
                                    <asp:HiddenField runat="server" ID="hdDependancyFlag" Value='<%# Eval("DependancyFlag") %>' />
                                     <asp:HiddenField runat="server" ID="hdDependency" Value='<%# Eval("Dependancy") %>' />
                                     <asp:HiddenField runat="server" ID="hdIndependentParameter" Value='<%# Eval("IndependentParameter") %>' />
                                      <asp:HiddenField runat="server" ID="hdMandatory" Value='<%# Eval("Mandatory") %>' />
                                    <asp:HiddenField runat="server" ID="cmLimitRange" Value='<%# Eval("LimitRange") %>' />
                                    <asp:HiddenField runat="server" ID="cmRecommandation" Value='<%# Eval("Recommendation") %>' />
                                    <asp:Label runat="server" ID="cmMandatory" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>
                                    <asp:Image runat="server" ID="cmimgRecommendation" Width="30px" Height="30px" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("Image") %>' />
                                    <asp:Label runat="server" ID="cmParameterID" Text='<%# Eval("PrameterId") %>' Visible="false"></asp:Label>
                                     <asp:HiddenField runat="server" ID="hfParameterID" Value='<%# Eval("PrameterId") %>' />
                                    <asp:Label runat="server" ID="cmDateType" Text='<%# Eval("Datatype") %>' Visible="false"></asp:Label>
                                     <asp:HiddenField runat="server" ID="cmhiddenDateType" Value='<%# Eval("Datatype") %>' />
                                    <asp:Label runat="server" ID="cmobjectType" Text='<%# Eval("ObjectType") %>' Visible="false"></asp:Label>
                                     <asp:Label runat="server" ID="cmcalculatedflag" Text='<%# Eval("CalculatedFlag") %>' Visible="false"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div id="menu3" class="tab-pane fade">
                    <div class="row text-left rowChange" style="overflow: auto">

                        <asp:ListView runat="server" ID="lvWorkpiece" ClientIDMode="Static">
                            <LayoutTemplate>

                                <ul runat="server" id="ulWorkpiece" style="text-align: center; width: 98%; margin: auto;">
                                    <li runat="server" id="itemPlaceholder" />
                                </ul>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 45%; margin-right: 20px">

                                    <asp:Label runat="server" CssClass="toggleColor" ID="wpItem" Width="285px" Style="min-width: 285px; color: #454444; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;" Text='<%# Eval("CustomeName") %>' ToolTip='<%# Eval("CustomeName") %>'></asp:Label>
                                      <asp:HiddenField runat="server" ID="wphdParameterName" Value='<%# Eval("Prameter") %>' />
                                    <asp:TextBox runat="server" ID="wptxtvalue" onmouseover="return showRecommendation(this,'Workpiece');" onfocus="return showRecommendation(this,'Workpiece');" onblur="return CalculateVw(this,'Workpiece Details')" Style="width: 40%; min-width: 20%;max-width:40%" CssClass="form-control " ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="wptxtallowNumeric" onkeypress="return allowNumberic(event);" onblur="return CalculateVw(this,'Workpiece Details');" onmouseover="return showRecommendation(this,'Workpiece');" onfocus="return showRecommendation(this,'Workpiece');" Style="width: 40%;max-width:40%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="wptxtallowalphaNumeric" onkeypress="return allowAlphaNumber(event);" onblur="return CalculateVw(this,'Workpiece Details');" onmouseover="return showRecommendation(this,'Workpiece');" onfocus="return showRecommendation(this,'Workpiece');" Style="width: 40%;max-width:40%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="wptxtallowDecimal" onblur="return CalculateVw(this);" Style="width: 40%;max-width:40%; min-width: 20%" onmouseover="return showRecommendation(this,'Workpiece');" onfocus="return showRecommendation(this,'Workpiece');" CssClass="form-control allowDecimal" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="wptxtDate" TextMode="DateTimeLocal" onmouseover="return showRecommendation(this,'Workpiece');" onfocus="return showRecommendation(this,'Workpiece');" onblur="return DateTimeFormateCheck(this);" Style="width: 40%;max-width:40%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>

                                    <%-- may Problem while showing LSL and USL --%>
                                    <span id="hardness" runat="server" style="display: flex;width:40% ;max-width:40%">
                                        <asp:TextBox runat="server" ID="txtHardness" onblur="return wpHardness(this)" Style="max-width: none;width:50%" CssClass="form-control " ClientIDMode="Static"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="ddlHardnessUnit" onchange="HardnessUnitChange(this);" Style="max-width: none;width:50%" CssClass="form-control " ClientIDMode="Static"></asp:DropDownList>
                                    </span>


                                    <asp:CheckBox runat="server" ID="wpcbvalue" ClientIDMode="Static" />
                                    <%--    --%>
                                    <asp:DropDownList runat="server" ID="wpddlvalue" onchange="return bindDependancyValue(this,'Workpiece')" onmouseover="return showRecommendation(this,'Workpiece');" onfocus="return savePreviousdata(this,'Workpiece');" Style="width: 40%; max-width: 40%" CssClass="form-control " ClientIDMode="Static">
                                    </asp:DropDownList>
                                     <asp:HiddenField runat="server" ID="hfddlvalue" Value='<%# Eval("Value") %>' />
                                    <asp:HiddenField runat="server" ID="hdDependancyFlag" Value='<%# Eval("DependancyFlag") %>' />
                                     <asp:HiddenField runat="server" ID="hdDependency" Value='<%# Eval("Dependancy") %>' />
                                     <asp:HiddenField runat="server" ID="hdIndependentParameter" Value='<%# Eval("IndependentParameter") %>' />
                                      <asp:HiddenField runat="server" ID="hdMandatory" Value='<%# Eval("Mandatory") %>' />

                                    <%--   <asp:FileUpload runat="server"   ID="imageUpload1" />--%>
                                    <div id="image" runat="server" style="min-width: 20%; width: 40%;max-width: 40%">


                                        <asp:LinkButton runat="server" ID="addnewImage"  ToolTip="Add New Image" OnClientClick="return addNewFileUpload(this);" Style="color: #70ccc9" CssClass="glyphicon glyphicon-plus-sign"></asp:LinkButton>
                                        &nbsp;
                                         <asp:LinkButton runat="server" ID="LinkButton1"  OnClientClick="return removeNewFileUpload(this);" Style="color: #70ccc9" CssClass="glyphicon glyphicon-minus-sign"></asp:LinkButton>

                                        <span id="appendImage">
                                            <span id="span1">
                                                <asp:FileUpload runat="server" ID="imageUpload1" />
                                                <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Image Name" ID="txtimageName1" Style=""></asp:TextBox>
                                            </span>
                                        </span> 

                                        <%--  <asp:LinkButton runat="server" ID="imagedone" Text="Upload Image" CssClass="Btns" OnClientClick="return imageUpadateDone();" Style=""></asp:LinkButton>--%>
                                        <asp:Button runat="server" ID="imagedone" Text="Upload Image"  CssClass="Btns" Style="margin-top: 10px; margin-bottom: 10px" UseSubmitBehavior="false" OnClientClick="return imageUpadateDone();"></asp:Button>
                                        <br />

                                        <asp:DataList ID="dlImages" runat="server"
                                            RepeatColumns="5"
                                            RepeatDirection="Horizontal"
                                            RepeatLayout="Flow">

                                            <ItemTemplate>
                                                <asp:Label runat="server" Style="width: 100px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;" Text='<%#Eval("wpImageName") %>' ID="lblImageName"></asp:Label>
                                                <br />
                                                <img src='<%#Eval("wpImagePath") %>' runat="server" id="img" onclick="showLargeImage(this)" style="width: 40px; height: 40px; margin-bottom: 10px" />

                                                <asp:LinkButton runat="server" CssClass="glyphicon glyphicon-remove-circle" ToolTip="Remove Image" ID="removeImage" Style="color: #fc3503; margin-left: 25px; vertical-align: super" OnClientClick="return removeImageFun(this)" ClientIDMode="Static"></asp:LinkButton>

                                                <%--    <i class="glyphicon glyphicon-remove-circle" id="removeImage" style="color: #fc3503; margin-left: 25px; vertical-align: super" onclick="return removeImageFun(this)" runat="server"></i>--%>
                                                <br />
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>

                                    <asp:HiddenField runat="server" ID="wpLimitRange" Value='<%# Eval("LimitRange") %>' />
                                    <asp:HiddenField runat="server" ID="wpRecommandation" Value='<%# Eval("Recommendation") %>' />

                                    <asp:Label runat="server" ID="wpMandatory" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>
                                    <asp:Image runat="server" ID="wpimgRecommendation" Width="30px" Height="30px" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("Image") %>' />
                                    <asp:Label runat="server" ID="wpParameterID" Text='<%# Eval("PrameterId") %>' Visible="false"></asp:Label>
                                     <asp:HiddenField runat="server" ID="hfParameterID" Value='<%# Eval("PrameterId") %>' />
                                    <asp:Label runat="server" ID="wpDateType" Text='<%# Eval("Datatype") %>' Visible="false"></asp:Label>
                                    <asp:HiddenField runat="server" ID="wphiddebDatatype" Value='<%# Eval("Datatype") %>' />
                                    <asp:Label runat="server" ID="wpobjectType" Text='<%# Eval("ObjectType") %>' Visible="false"></asp:Label>
                                     <asp:Label runat="server" ID="wpcalculatedflag" Text='<%# Eval("CalculatedFlag") %>' Visible="false"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div id="menu4" class="tab-pane fade">
                    <div class="row text-left rowChange" style="overflow: auto">

                        <div style="width: 100%; overflow: auto; padding-bottom: 10px">
                        <asp:ListView runat="server" ID="lvOperationalParameter" ClientIDMode="Static" >
                            <LayoutTemplate>
                                <ul runat="server" id="ulOperationalParameter" style="text-align: center; width: 100%; margin: auto; ">
                       

                                    <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 100%">

                                        <h4 style="width: 100%; background-color: #d1f2d0; text-align: center; color: #4e4949; font-size: 20px; border-radius: 3px; padding: 2px">Grinding Parameters</h4>
                                        <%--    <asp:Label runat="server" CssClass=" qpInputControls QPTA" ID="Label10" Width="100%" Style= " background-color: #d1f2d0 ; text-align: center; border-color: #d1f2d0; color: #4e4949; font-size: 20px; border-radius:3px;" Text="Grinding Parameters" ForeColor="White"></asp:Label>--%>
                                    </div>
                                    <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 112%;">
                                        <asp:Label runat="server" ID="Label2" Style="background-color: rgb(245, 245, 245); border-color: rgb(245, 245, 245); color: #87878a; font-size: 16px; font-weight: bold; text-align: left;min-width: 10%; max-width: 10%" CssClass=" qpInputControls QPLU"  Text="Stage"></asp:Label>

                                         <asp:Label runat="server" CssClass=" qpInputControls QPLU " ID="Label11" Style="background-color: rgb(245, 245, 245); border-color: rgb(245, 245, 245); color: #87878a; font-size: 16px; font-weight: bold; text-align: center; width: 8%" Text="For"></asp:Label>

                                        <asp:Label runat="server" CssClass=" qpInputControls QPLU " ID="lbl" Style="background-color: rgb(245, 245, 245); border-color: rgb(245, 245, 245); color: #87878a; font-size: 16px; font-weight: bold; text-align: center; width: 16% " Text="Feed Rate (mm/min)"></asp:Label>
                                        <asp:Label runat="server" CssClass="qpInputControls QPLU " ID="Label6" Style="background-color: rgb(245, 245, 245); border-color: rgb(245, 245, 245); color: #87878a; font-size: 16px; font-weight: bold; text-align: center; width: 14%" Text="Stock Diametrically (mm)"></asp:Label>

                                          <asp:Label runat="server" CssClass="qpInputControls QPLU " ID="Label10" Style="background-color: rgb(245, 245, 245); border-color: rgb(245, 245, 245); color: #87878a; font-size: 16px; font-weight: bold; text-align: center;  width: 16%" Text="Stock on Face (mm)"></asp:Label>

                                        <asp:Label runat="server" CssClass=" qpInputControls QPLU " ID="Label5" Style="background-color: rgb(245, 245, 245); border-color: rgb(245, 245, 245); color: #87878a; font-size: 16px; font-weight: bold; text-align: center; width: 14%" Text="Workspeed (RPM)"></asp:Label>

                                        <asp:Label runat="server" CssClass=" qpInputControls QPLU " ID="Label7" Style="background-color: rgb(245, 245, 245); border-color: rgb(245, 245, 245); color: #87878a; font-size: 16px; font-weight: bold; text-align: center; width: 17%" Text="Wheelspeed (m/s)"></asp:Label>

                                        <asp:Label runat="server" CssClass=" qpInputControls QPLU " ID="Label8" Visible="false" Style="background-color: rgb(245, 245, 245); border-color: rgb(245, 245, 245); color: #87878a; font-size: 16px; font-weight: bold; text-align: center" Text="Workspeed (m/s)"></asp:Label>
                                        <asp:Label runat="server" CssClass=" qpInputControls QPLU" ID="Label9" Visible="false" Style="background-color: rgb(245, 245, 245); border-color: rgb(245, 245, 245); color: #87878a; font-size: 16px; font-weight: bold; text-align: center" Text="Wheelspeed (RPM)"></asp:Label>
                                    </div>


                                    <li runat="server" id="itemPlaceholder" />
                                </ul>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 100%">
                                    <asp:Label runat="server" CssClass="toggleColor " ID="opItem" Style="color: #454444; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; max-width: 13%;min-width: 13%" Text='<%# Eval("Prameter") %>' ToolTip='<%# Eval("Prameter") %>'></asp:Label>

                                    <span class="opspan" style="min-width: 10%;max-width:10%" >
                                        <asp:DropDownList runat="server" ID="ddlopfor" CssClass="form-control" style="max-width: 120px">
                                            <asp:ListItem>OD</asp:ListItem>
                                             <asp:ListItem>Face + OD</asp:ListItem>
                                             <asp:ListItem>Face</asp:ListItem>
                                             <asp:ListItem>Radius</asp:ListItem>
                                        </asp:DropDownList>
                                        
                                    </span>

                                    <span class="opspan">
                                        <asp:HiddenField runat="server" ID="hdnParamIDopFeedRate" Value='<%# Eval("ParameterIDFeedRate") %>' />
                                        <asp:HiddenField runat="server" ID="hdnLslUslopFeedRate" Value='<%# Eval("LslUslFeedRate") %>' />
                                        <asp:HiddenField runat="server" ID="hdnRecomopFeedRate" Value='<%# Eval("RecommendationFeedRate") %>' />
                                        <asp:TextBox runat="server" ID="opFeedRatedecimal"  onmouseover="ShowREcommandationforOpeartionalParam(this, 'FeedRate');" onfocus="ShowREcommandationforOpeartionalParam(this, 'FeedRate');" Text='<%# Eval("FeedRate") %>' onblur="OPCalculation(this, 'FeedRate')" CssClass="form-control  qpInputControls allowDecimal opcs" ClientIDMode="Static"></asp:TextBox>
                                        <asp:Label runat="server" ID="opMandatoryFeedRate" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>
                                        <asp:Image runat="server" ID="opimgRecommendationFeedRate" Width="30px" Height="30px" CssClass="opimages" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("ImageFeedRate") %>' />
                                        <asp:Label runat="server" ID="opFeedrateObjectType" Text='<%# Eval("DataTypeFeedRate") %>' Visible="false"></asp:Label>
                                        <asp:HiddenField runat="server" ID="opFeedratehiddenObjecttype"  Value='<%# Eval("DataTypeFeedRate") %>' />
                                        <asp:Label runat="server" ID="opFeedrateEntryType" Text='<%# Eval("EntryTypeFeedRate") %>' Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="opFeedrateParameter" Text='<%# Eval("ParameterFeedRate") %>' Visible="false"></asp:Label>
                                              <asp:HiddenField runat="server" ID="hdFeedrateDependency" Value='<%# Eval("DependancyFeedRate") %>' />
                                     <asp:HiddenField runat="server" ID="hdFeedrateIndependentParameter" Value='<%# Eval("IndependentParameterFeedRate") %>' />
                                         <asp:HiddenField runat="server" ID="hdFeedrateMandatory" Value='<%# Eval("MandatoryFeedRate") %>' />
                                           <asp:HiddenField runat="server" ID="hdFeedrateParameter" Value='<%# Eval("ParameterFeedRate") %>' />
                                         <asp:HiddenField runat="server" ID="hdFeedrateCustomename" Value='<%# Eval("CustomenameFeedRate") %>' />

                                    </span>


                                    <span class="opspan">
                                        <asp:HiddenField runat="server" ID="hdnParamIDopDOC" Value='<%# Eval("ParameterIDDOC") %>' />
                                        <asp:HiddenField runat="server" ID="hdnLslUslopDOC" Value='<%# Eval("LslUslDOC") %>' />
                                        <asp:HiddenField runat="server" ID="hdnRecomopDOC" Value='<%# Eval("RecommendationDOC") %>' />
                                        <asp:TextBox runat="server" ID="opDOCDecimal" Text='<%# Eval("DOC") %>' onmouseover="ShowREcommandationforOpeartionalParam(this, 'DOC');" onfocus="ShowREcommandationforOpeartionalParam(this, 'DOC');"  onblur="ExceedRangeWarningForOperationalParam(this, 'DOC')" CssClass="form-control  qpInputControls allowDecimalwithoperator opcs" ClientIDMode="Static"></asp:TextBox>
                                       
                                        <asp:Label runat="server" ID="opMandatoryDOC" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>
                                        <asp:Image runat="server" ID="opimgRecommendationDOC" Width="30px" Height="30px" CssClass="opimages" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("ImageDOC") %>' />
                                        <asp:Label runat="server" ID="opDOCObjectType" Text='<%# Eval("DataTypeDOC") %>' Visible="false"></asp:Label>
                                         <asp:HiddenField runat="server" ID="opDOChiddenObjectType"  Value='<%# Eval("DataTypeDOC") %>' />
                                        <asp:Label runat="server" ID="opDOCEntryType" Text='<%# Eval("EntryTypeDOC") %>' Visible="false"></asp:Label>
                                           <asp:Label runat="server" ID="opDOCParameter" Text='<%# Eval("ParameterDOC") %>' Visible="false"></asp:Label>
                                              <asp:HiddenField runat="server" ID="opDochdDependency" Value='<%# Eval("DependancyDoc") %>' />
                                     <asp:HiddenField runat="server" ID="opDochdIndependentParameter" Value='<%# Eval("IndependentParameterDoc") %>' />
                                          <asp:HiddenField runat="server" ID="opDOChdMandatory" Value='<%# Eval("MandatoryDoc") %>' />
                                         <asp:HiddenField runat="server" ID="opDOChdParameter" Value='<%# Eval("ParameterDOC") %>' />
                                           <asp:HiddenField runat="server" ID="opDOChdCustomename" Value='<%# Eval("CustomenameDoc") %>' />
                                    </span>

                                     <span class="opspan">
                                        <asp:HiddenField runat="server" ID="hdnParamIDopFace" Value='<%# Eval("ParameterIDFace") %>' />
                                        <asp:HiddenField runat="server" ID="hdnLslUslopFace" Value='<%# Eval("LslUslFace") %>' />
                                        <asp:HiddenField runat="server" ID="hdnRecomopFace" Value='<%# Eval("RecommendationFace") %>' />
                                        <asp:TextBox runat="server" ID="opFaceDecimal" Text='<%# Eval("Face") %>' onmouseover="ShowREcommandationforOpeartionalParam(this, 'Face');" onfocus="ShowREcommandationforOpeartionalParam(this, 'Face');"  onblur="ExceedRangeWarningForOperationalParam(this, 'Face')" CssClass="form-control  qpInputControls allowDecimalwithoperator opcs" ClientIDMode="Static"></asp:TextBox>
                                        
                                         <asp:Label runat="server" ID="opMandatoryFace" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>
                                        <asp:Image runat="server" ID="opimgRecommendationFace" Width="30px" Height="30px" CssClass="opimages" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("ImageFace") %>' />
                                        <asp:Label runat="server" ID="opFaceObjectType" Text='<%# Eval("DataTypeFace") %>' Visible="false"></asp:Label>
                                         <asp:HiddenField runat="server" ID="opFacehiddenObjectType"  Value='<%# Eval("DataTypeFace") %>' />
                                        <asp:Label runat="server" ID="opFaceEntryType" Text='<%# Eval("EntryTypeFace") %>' Visible="false"></asp:Label>
                                           <asp:Label runat="server" ID="opFaceParameter" Text='<%# Eval("ParameterFace") %>' Visible="false"></asp:Label>
                                         <asp:HiddenField runat="server" ID="opFacehdDependency" Value='<%# Eval("DependancyFace") %>' />
                                     <asp:HiddenField runat="server" ID="opFacehdIndependentParameter" Value='<%# Eval("IndependentParameterFace") %>' />
                                           <asp:HiddenField runat="server" ID="opFacehdMandatory" Value='<%# Eval("MandatoryFace") %>' />
                                         <asp:HiddenField runat="server" ID="opFacehdParameter" Value='<%# Eval("ParameterFace") %>' />
                                           <asp:HiddenField runat="server" ID="opFacehdCustomename" Value='<%# Eval("CustomenameFace") %>' />
                                    </span>

                                    <span class="opspan">
                                        <asp:HiddenField runat="server" ID="hdnParamIDopWorkRPM" Value='<%# Eval("ParameterIDWorkRPM") %>' />
                                        <asp:HiddenField runat="server" ID="hdnLslUslopWorkRPM" Value='<%# Eval("LslUslWorkRPM") %>' />
                                        <asp:HiddenField runat="server" ID="hdnRecomopWorkRPM" Value='<%# Eval("RecommendationWorkRPM") %>' />
                                        <asp:TextBox runat="server" ID="opWorkRPMDecimal" onmouseover="ShowREcommandationforOpeartionalParam(this, 'WorkRPM');" onfocus="ShowREcommandationforOpeartionalParam(this, 'WorkRPM');" Text='<%# Eval("WorkRPM") %>' onblur="OPCalculation(this, 'WorkRPM')" CssClass="form-control  qpInputControls allowDecimal opcs" ClientIDMode="Static"></asp:TextBox>
                                     
                                        <asp:Label runat="server" ID="opMandatoryWorkRPM" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>
                                        <asp:Image runat="server" ID="opimgWorkRPM" Width="30px" Height="30px" CssClass="opimages" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("ImageWorkRPM") %>' />
                                        <asp:Label runat="server" ID="opWorkRPMObjectType" Text='<%# Eval("DataTypeWorkRPM") %>' Visible="false"></asp:Label>
                                          <asp:HiddenField runat="server" ID="opWorkRPMhiddenObjectType"  Value='<%# Eval("DataTypeWorkRPM") %>' />
                                        <asp:Label runat="server" ID="opWorkRPMEntryType" Text='<%# Eval("EntryTypeWorkRPM") %>' Visible="false"></asp:Label>
                                          <asp:Label runat="server" ID="opWorkRPMParameter" Text='<%# Eval("ParameterWorkRPM") %>' Visible="false"></asp:Label>
                                         <asp:HiddenField runat="server" ID="ophdWokRPMDependency" Value='<%# Eval("DependancyWorkRPM") %>' />
                                     <asp:HiddenField runat="server" ID="ophdWorkRPMIndependentParameter" Value='<%# Eval("IndependentParameterWorkRPM") %>' />
                                          <asp:HiddenField runat="server" ID="ophdWorkRPMMandatory" Value='<%# Eval("MandatoryWorkRPM") %>' />
 <asp:HiddenField runat="server" ID="ophdWorkRPMParameter" Value='<%# Eval("ParameterWorkRPM") %>' />
                                         <asp:HiddenField runat="server" ID="ophdWorkRPMCustomename" Value='<%# Eval("CustomenameWorkRPM") %>' />
                                    </span>

                                    <span class="opspan">
                                        <asp:HiddenField runat="server" ID="hdnParamIDopWheelms" Value='<%# Eval("ParameterIDWheelms") %>' />
                                        <asp:HiddenField runat="server" ID="hdnLslUslopWheelms" Value='<%# Eval("LslUslWheelms") %>' />
                                        <asp:HiddenField runat="server" ID="hdnRecomopWheelms" Value='<%# Eval("RecommendationWheelms") %>' />
                                        <asp:TextBox runat="server" ID="opWheelmsDEcimal" onmouseover="ShowREcommandationforOpeartionalParam(this, 'Wheelms');" onfocus="ShowREcommandationforOpeartionalParam(this, 'Wheelms');" onblur="OPCalculation(this, 'Wheelms')" Text='<%# Eval("Wheelms") %>' CssClass="form-control  qpInputControls allowDecimal opcs" ClientIDMode="Static"></asp:TextBox>
                                      
                                        <asp:Label runat="server" ID="opMandatoryWheelms" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>
                                        <asp:Image runat="server" ID="opimgWheelms" Width="30px" Height="30px" CssClass="opimages" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("ImageWheelms") %>' />
                                        <asp:Label runat="server" ID="opWheelmsObjectType" Text='<%# Eval("DataTypeWheelms") %>' Visible="false"></asp:Label>
                                         <asp:HiddenField runat="server" ID="opWheelmshiddenObjectType"  Value='<%# Eval("DataTypeWheelms") %>' />
                                        <asp:Label runat="server" ID="opWheelmsEntryType" Text='<%# Eval("EntryTypeWheelms") %>' Visible="false"></asp:Label>
                                         <asp:Label runat="server" ID="opWheelmsParameter" Text='<%# Eval("ParameterWheelms") %>' Visible="false"></asp:Label>
                                        <asp:HiddenField runat="server" ID="ophdWheelmsDependency" Value='<%# Eval("DependancyWheelms") %>' />
                                     <asp:HiddenField runat="server" ID="ophdWheelmsIndependentParameter" Value='<%# Eval("IndependentParameterWheelms") %>' />

                                       <asp:HiddenField runat="server" ID="ophdWheelmsMandatory" Value='<%# Eval("MandatoryWheelms") %>' />
 <asp:HiddenField runat="server" ID="ophdWheelmsParameter" Value='<%# Eval("ParameterWheelms") %>' />
                                         <asp:HiddenField runat="server" ID="ophdWheelmsCustomename" Value='<%# Eval("CustomenameWheelms") %>' />
                                    </span>

                                    <span class="">
                                        <asp:HiddenField runat="server" ID="hdnParamIDopWorkms" Value='<%# Eval("ParameterIDWorkms") %>' />
                                        <asp:HiddenField runat="server" ID="hdnLslUslopWorkms" Value='<%# Eval("LslUslWorkms") %>' />
                                        <asp:HiddenField runat="server" ID="hdnRecomopWorkms" Value='<%# Eval("RecommendationWorkms") %>' />
                                        <asp:TextBox runat="server" ID="opWorkmsDEcimal" onmouseover="ShowREcommandationforOpeartionalParam(this, 'Workms');" onfocus="ShowREcommandationforOpeartionalParam(this, 'Workms');" onblur="ExceedRangeWarningForOperationalParam(this, 'Workms')" Style="visibility: hidden" Text='<%# Eval("Workms") %>' CssClass="form-control  qpInputControls allowDecimal opcs " ClientIDMode="Static"></asp:TextBox>

                                        <asp:Label runat="server" ID="opMandatoryWorkms" CssClass="glyphicon glyphicon-star mandatory" style="visibility: hidden"></asp:Label>
                                    
                                        <asp:Image runat="server" ID="opimgWorkms" Width="30px" Height="30px" CssClass="opimages" Style="margin-left: 2px; visibility: hidden" onclick="showLargeImage(this)" ImageUrl='<%#Eval("ImageWorkms") %>' />
                                        <asp:Label runat="server" ID="opWorkmsObjectType" Text='<%# Eval("DataTypeWorkms") %>' Visible="false"></asp:Label>
                                          <asp:HiddenField runat="server" ID="opWorkmshiddenObjectType"  Value='<%# Eval("DataTypeWorkms") %>' />
                                        <asp:Label runat="server" ID="opWorkmsEntryType" Text='<%# Eval("EntryTypeWorkms") %>' Visible="false"></asp:Label>
                                         <asp:Label runat="server" ID="opWorkmsParameter" Text='<%# Eval("ParameterWorkms") %>' Visible="false"></asp:Label>
                                          <asp:HiddenField runat="server" ID="hdworkmsDependency" Value='<%# Eval("DependancyWorkms") %>' />
                                     <asp:HiddenField runat="server" ID="hdworkmsIndependentParameter" Value='<%# Eval("IndependentParameterWorkms") %>' />
                                          <asp:HiddenField runat="server" ID="hdworkmsMandatory" Value='<%# Eval("MandatoryWorkms") %>' />
                                          <asp:HiddenField runat="server" ID="ophdWorkmsParameter" Value='<%# Eval("ParameterWorkms") %>'></asp:HiddenField>
                                           <asp:HiddenField runat="server" ID="ophdWorkmsCustomename" Value='<%# Eval("CustomenameWorkms") %>'></asp:HiddenField>
                                    </span>


                                    <span class="">

                                        <asp:HiddenField runat="server" ID="hdnParamIDopWheelRPM" Value='<%# Eval("ParameterIDWheelRPM") %>' />
                                        <asp:HiddenField runat="server" ID="hdnLslUslopWheelRPM" Value='<%# Eval("LslUslWheelRPM") %>' />
                                        <asp:HiddenField runat="server" ID="hdnRecomopWheelRPM" Value='<%# Eval("RecommendationWheelRPM") %>' />
                                        <asp:TextBox runat="server" ID="opWheelRPMDEcimal" Style="visibility: hidden" Text='<%# Eval("WheelRPM") %>' onmouseover="ShowREcommandationforOpeartionalParam(this, 'WheelRPM');"  onfocus="ShowREcommandationforOpeartionalParam(this, 'WheelRPM');"   onblur="ExceedRangeWarningForOperationalParam(this, 'WheelRPM')" CssClass="form-control  qpInputControls allowDecimal " ClientIDMode="Static"></asp:TextBox>
                                       
                                        <asp:Label runat="server" ID="opMandatoryWheelRPM" CssClass="glyphicon glyphicon-star mandatory" style="visibility: hidden"></asp:Label>
                                        <asp:Image runat="server" ID="opimgWheelRPM" Width="30px" Height="30px" CssClass="" Style="margin-left: 2px;visibility: hidden" onclick="showLargeImage(this)" ImageUrl='<%#Eval("ImageWheelRPM") %>' />
                                        <asp:Label runat="server" ID="opWheelRPMObjectType" Text='<%# Eval("DataTypeWheelRPM") %>' Visible="false"></asp:Label>
                                         <asp:HiddenField runat="server" ID="opWheelRPMhiddenObjectType"  Value='<%# Eval("DataTypeWheelRPM") %>' />
                                        <asp:Label runat="server" ID="opWheelRPMEntryType" Text='<%# Eval("EntryTypeWheelRPM") %>' Visible="false"></asp:Label>
                                         <asp:Label runat="server" ID="opWheelRPMPrameter" Text='<%# Eval("ParameterWheelRPM") %>' Visible="false"></asp:Label>
                                         <asp:HiddenField runat="server" ID="hdWheelRPMDependency" Value='<%# Eval("DependancyWheelRPM") %>' />
                                     <asp:HiddenField runat="server" ID="hdWheelRPMIndependentParameter" Value='<%# Eval("IndependentParameterWheelRPM") %>' />
                                          <asp:HiddenField runat="server" ID="hdWheelRPMMandatory" Value='<%# Eval("MandatoryWheelRPM") %>' />
                                         <asp:HiddenField runat="server" ID="hdWheelRPMPaarmeter" Value='<%# Eval("ParameterWheelRPM") %>' ></asp:HiddenField>
                                           <asp:HiddenField runat="server" ID="hdWheelRPMCustomename" Value='<%# Eval("CustomenameWheelRPM") %>' ></asp:HiddenField>
                                    </span>
                                 
                                </div>
                            </ItemTemplate>

                        </asp:ListView>
                              </div>
                        <asp:ListView runat="server" ID="lvOperationalParameterGrind" ClientIDMode="Static" Style="">
                            <LayoutTemplate>

                                <ul runat="server" id="ulOperationalParameterGrind" style="text-align: left; width: 95%; margin:auto">
                                    <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 100%">

                                        <h4 style="width: 100%; background-color: #d1f2d0; text-align: center; color: #4e4949; font-size: 20px; border-radius: 3px; padding: 2px">New Grinding Parameters</h4>
                                    </div>
                                    <li runat="server" id="itemPlaceholder" />
                                </ul>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 45%; margin-right: 20px">
                                   <%-- margin-right: 20px--%>
                                    <asp:Label runat="server" CssClass="toggleColor" ID="opItem" Width="285px"  Style=" min-width: 285px; color: #454444; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;" Text='<%# Eval("CustomeName") %>' ToolTip='<%# Eval("CustomeName") %>'></asp:Label>
                                     <asp:HiddenField runat="server" ID="ophdParameterName" Value='<%# Eval("Prameter") %>' />
                                    <asp:TextBox runat="server" ID="optxtvalue" onmouseover="return showRecommendation(this,'OperationalGrinding');" onfocus="return showRecommendation(this,'OperationalGrinding');" onblur="return OPDressingParametersCalculations(this,'OPGrindingParam')" Style="width: 30%; min-width: 20%" CssClass="form-control " ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="optxtallowNumeric" onmouseover="return showRecommendation(this,'OperationalGrinding');" onfocus="return showRecommendation(this,'OperationalGrinding');" onkeypress="return allowNumberic(event);" onblur="return OPDressingParametersCalculations(this,'OPGrindingParam')" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="optxtallowalphaNumeric" onmouseover="return showRecommendation(this,'OperationalGrinding');" onfocus="return showRecommendation(this,'OperationalGrinding');" onkeypress="return allowAlphaNumber(event);" onblur="return OPDressingParametersCalculations(this,'OPGrindingParam')" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="optxtallowDecimal" onmouseover="return showRecommendation(this,'OperationalGrinding');" onfocus="return showRecommendation(this,'OperationalGrinding');" onblur="return OPDressingParametersCalculations(this,'OPGrindingParam')" Style="width: 30%; min-width: 20%" CssClass="form-control allowDecimal" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="optxtDate" TextMode="DateTimeLocal" onmouseover="return showRecommendation(this,'OperationalGrinding');" onfocus="return showRecommendation(this,'OperationalGrinding');" onblur="return DateTimeFormateCheck(this);" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>

                                    <asp:CheckBox runat="server" ID="opcbvalue" ClientIDMode="Static" />
                                    <asp:DropDownList runat="server" ID="opddlvalue" onchange="return bindDependancyValue(this,'OperationalGrinding')" onmouseover="return showRecommendation(this,'OperationalGrinding');" onfocus="return savePreviousdata(this,'OperationalGrinding');" Style="width: 30%; min-width: 20%" CssClass="form-control " ClientIDMode="Static">
                                    </asp:DropDownList>

                                     <asp:HiddenField runat="server" ID="hfddlvalue" Value='<%# Eval("Value") %>' />
                                    <asp:HiddenField runat="server" ID="hdDependancyFlag" Value='<%# Eval("DependancyFlag") %>' />
                                     <asp:HiddenField runat="server" ID="hdDependency" Value='<%# Eval("Dependancy") %>' />
                                     <asp:HiddenField runat="server" ID="hdIndependentParameter" Value='<%# Eval("IndependentParameter") %>' />
                                      <asp:HiddenField runat="server" ID="hdMandatory" Value='<%# Eval("Mandatory") %>' />

                                    <asp:HiddenField runat="server" ID="opLimitRange" Value='<%# Eval("LimitRange") %>' />
                                    <asp:HiddenField runat="server" ID="opRecommandation" Value='<%# Eval("Recommendation") %>' />

                                    <asp:Label runat="server" ID="opMandatory" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>
                                    <asp:Image runat="server" ID="opimgRecommendation" Width="30px" Height="30px" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("Image") %>' />
                                    <asp:Label runat="server" ID="opParameterID" Text='<%# Eval("PrameterId") %>' Visible="false"></asp:Label>
                                     <asp:HiddenField runat="server" ID="hfParameterID" Value='<%# Eval("PrameterId") %>' />
                                    <asp:Label runat="server" ID="opDateType" Text='<%# Eval("Datatype") %>' Visible="false"></asp:Label>
                                     <asp:HiddenField runat="server" ID="ophiddenDateType" Value='<%# Eval("Datatype") %>' />
                                    <asp:Label runat="server" ID="opobjectType" Text='<%# Eval("ObjectType") %>' Visible="false"></asp:Label>
                                     <asp:Label runat="server" ID="opcalculatedflag" Text='<%# Eval("CalculatedFlag") %>' Visible="false"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>

                      

                        <asp:ListView runat="server" ID="lvOPDressing" ClientIDMode="Static" Style="margin-top: 5%">
                            <LayoutTemplate>

                                <ul runat="server" id="ulOPDressing" style="text-align: left; width: 95%; margin: auto; padding-top: 1%">
                                    <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 100%">

                                        <h4 style="width: 100%; background-color: #d1f2d0; text-align: center; color: #4e4949; font-size: 20px; border-radius: 3px; padding: 2px">Dressing Parameters</h4>
                                    </div>
                                    <li runat="server" id="itemPlaceholder" />
                                </ul>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 45%; margin-right: 20px">

                                    <asp:Label runat="server" CssClass="toggleColor" ID="opItem" Width="285px" Style="min-width: 285px; color: #454444; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;" Text='<%# Eval("CustomeName") %>' ToolTip='<%# Eval("CustomeName") %>'></asp:Label>
                                       <asp:HiddenField runat="server" ID="ophdParameterName" Value='<%# Eval("Prameter") %>' />
                                    <asp:TextBox runat="server" ID="optxtvalue" onmouseover="return showRecommendation(this,'OperationalDressing');" onfocus="return showRecommendation(this,'OperationalDressing');" onblur="return OPDressingParametersCalculations(this,'OPDressingParam')" Style="width: 30%; min-width: 20%" CssClass="form-control " ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="optxtallowNumeric" onmouseover="return showRecommendation(this,'OperationalDressing');" onfocus="return showRecommendation(this,'OperationalDressing');" onkeypress="return allowNumberic(event);" onblur="return OPDressingParametersCalculations(this,'OPDressingParam')" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="optxtallowalphaNumeric" onmouseover="return showRecommendation(this,'OperationalDressing');" onfocus="return showRecommendation(this,'OperationalDressing');" onkeypress="return allowAlphaNumber(event);" onblur="return OPDressingParametersCalculations(this,'OPDressingParam')" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="optxtallowDecimal" onmouseover="return showRecommendation(this,'OperationalDressing');" onfocus="return showRecommendation(this,'OperationalDressing');" onblur="return  OPDressingParametersCalculations(this,'OPDressingParam')" Style="width: 30%; min-width: 20%" CssClass="form-control allowDecimal" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="optxtDate" TextMode="DateTimeLocal"  onmouseover="return showRecommendation(this,'OperationalDressing');" onfocus="return showRecommendation(this,'OperationalDressing');" onblur="return DateTimeFormateCheck(this);" Style="width: 30%; min-width: 20%" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>

                                    <asp:CheckBox runat="server" ID="opcbvalue" ClientIDMode="Static" />
                                    <asp:DropDownList runat="server" ID="opddlvalue" onchange="return bindDependancyValue(this,'OperationalDressing')" onmouseover="return showRecommendation(this,'OperationalDressing');" onfocus="return savePreviousdata(this,'OperationalDressing');" Style="width: 30%; min-width: 20%" CssClass="form-control " ClientIDMode="Static">
                                    </asp:DropDownList>

                                     <asp:HiddenField runat="server" ID="hfddlvalue" Value='<%# Eval("Value") %>' />
                                    <asp:HiddenField runat="server" ID="hdDependancyFlag" Value='<%# Eval("DependancyFlag") %>' />
                                     <asp:HiddenField runat="server" ID="hdDependency" Value='<%# Eval("Dependancy") %>' />
                                     <asp:HiddenField runat="server" ID="hdIndependentParameter" Value='<%# Eval("IndependentParameter") %>' />
                                      <asp:HiddenField runat="server" ID="hdMandatory" Value='<%# Eval("Mandatory") %>' />

                                    <asp:HiddenField runat="server" ID="opLimitRange" Value='<%# Eval("LimitRange") %>' />
                                    <asp:HiddenField runat="server" ID="opRecommandation" Value='<%# Eval("Recommendation") %>' />

                                    <asp:Label runat="server" ID="opMandatory" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>

                                    <asp:Image runat="server" ID="opimgRecommendation" Width="30px" Height="30px" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("Image") %>' />
                                    <asp:Label runat="server" ID="opParameterID" Text='<%# Eval("PrameterId") %>' Visible="false"></asp:Label>
                                     <asp:HiddenField runat="server" ID="hfParameterID" Value='<%# Eval("PrameterId") %>' />
                                    <asp:Label runat="server" ID="opDateType" Text='<%# Eval("Datatype") %>' Visible="false"></asp:Label>
                                     <asp:HiddenField runat="server" ID="ophiddenDateType" Value='<%# Eval("Datatype") %>' />
                                    <asp:Label runat="server" ID="opobjectType" Text='<%# Eval("ObjectType") %>' Visible="false"></asp:Label>
                                     <asp:Label runat="server" ID="opcalculatedflag" Text='<%# Eval("CalculatedFlag") %>' Visible="false"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>

                    </div>
                </div>
                <div id="menu5" class="tab-pane fade">

                    <div class="row text-left rowChange" >
                        <asp:ListView runat="server" ID="lvQualityParameter" ClientIDMode="Static">
                            <LayoutTemplate>

                                <ul runat="server" id="ulQualityParameter" style="text-align: center; width: 100%; margin: auto;">


                                    <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 100%; height: 20px">
                                        <asp:Label runat="server" ID="lbl" Style="" Text="" CssClass="opspan" ForeColor="White"></asp:Label>
                                        <asp:Label runat="server" CssClass=" qpInputControls QPTA" ID="Label1" Style="max-width: 30%; min-width: 30%; background-color: rgb(245, 245, 245); text-align: center; border-color: white; font-weight: bold; color: #87878a; font-size: 18px" Text="Target"></asp:Label>
                                        <%--  <asp:Label runat="server" CssClass="form-control qpInputControlsl"  ID="Label3" Width="100%" Text="Target2" ForeColor="White"></asp:Label>--%>
                                        <asp:Label runat="server" CssClass=" qpInputControls QPTA" ID="Label4" Style="max-width: 35%; min-width: 35%; background-color: rgb(245, 245, 245); text-align: center; border-color: white; font-weight: bold; color: #87878a; font-size: 18px" Text="Achieved"></asp:Label>
                                        <%-- <asp:Label runat="server" CssClass="form-control qpInputControls"  ID="Label2" Width="100%" Text="Target4" ForeColor="White"></asp:Label>--%>
                                    </div>

                                    <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 100%">
                                        <asp:Label runat="server" ID="Label2" Style="border-color: white;" Text="" CssClass="opspan" ForeColor="White"></asp:Label>
                                        <asp:Label runat="server" CssClass=" qpInputControls QPLU " ID="Label3" Style="width: 12%; background-color: rgb(245, 245, 245); text-align: center; color: #87878a; font-size: 16px; font-weight: bold" Text="Lower"></asp:Label>
                                        <asp:Label runat="server" CssClass="qpInputControls QPLU " ID="Label6" Style="width: 23%; background-color: rgb(245, 245, 245); text-align: center; color: #87878a; font-size: 16px; font-weight: bold" Text="Upper"></asp:Label>
                                        <asp:Label runat="server" CssClass="qpInputControls QPLU " ID="Label5" Style="width: 11%; background-color: rgb(245, 245, 245); text-align: center; color: #87878a; font-size: 16px; font-weight: bold" Text="Lower"></asp:Label>
                                        <asp:Label runat="server" CssClass=" qpInputControls QPLU " ID="Label7" Style="width: 22%; background-color: rgb(245, 245, 245); text-align: center; color: #87878a; font-size: 16px; font-weight: bold" Text="Upper"></asp:Label>
                                    </div>


                                    <li runat="server" id="itemPlaceholder" style="overflow: auto"/>
                                </ul>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <div class="fieldDiv" style="text-align: left; display: inline-flex; width: 100%">

                                    <asp:Label runat="server" CssClass="toggleColor opspan" ID="qpItem" Style="color: #454444; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;" Text='<%# Eval("Prameter") %>' ToolTip='<%# Eval("Prameter") %>'></asp:Label>

                                    <span class="opspan">
                                        <asp:HiddenField runat="server" ID="hdnParamIDqpTargetLower" Value='<%# Eval("ParamIdTargetLower") %>' />
                                        <asp:HiddenField runat="server" ID="hdnLslUslqpTargetLower" Value='<%# Eval("LslUslTargetLower") %>' />
                                        <asp:HiddenField runat="server" ID="hdnRecomqpTargetLower" Value='<%# Eval("RecommendationTL") %>' />
                                        <asp:TextBox runat="server" ID="qpTargetLower" onmouseover="ShowREcommandationforQParam(this, 'TargetLower');" onfocus="ShowREcommandationforQParam(this, 'TargetLower');" onblur="ExceedRangeWarningForQualityParam(this, 'TargetLower')" CssClass="form-control  qpInputControls allowDecimal opcs" ClientIDMode="Static" Text='<%# Eval("TargetLower") %>'></asp:TextBox>
                                         <asp:Label runat="server" ID="qpMandatoryTargetLower" CssClass="glyphicon glyphicon-star mandatory" ></asp:Label>
                                        <asp:Image runat="server" ID="qpLimitImgTargetLower" Width="30px" Height="30px" CssClass="opimages" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("ImageRecommandationTL") %>' />

                                           <asp:HiddenField runat="server" ID="hdTLDependency" Value='<%# Eval("DependencyTargetLower") %>' />
                                        <asp:HiddenField runat="server" ID="hdTLIndependentParameter" Value='<%# Eval("IndependentParameterTargetLower") %>' />
                                        <asp:HiddenField runat="server" ID="hdTLParameter" Value='<%# Eval("ParameterLowerTarget") %>' />
                                         <asp:HiddenField runat="server" ID="hdTLCustomename" Value='<%# Eval("CustomenameLowerTarget") %>' />
                                            <asp:HiddenField runat="server" ID="hdTLMandatory" Value='<%# Eval("MandatoryTargetLower") %>' />
                                    </span>

                                    <span class="opspan">
                                        <asp:HiddenField runat="server" ID="hdnParamIDqpTargetUppper" Value='<%# Eval("ParamIdTargetUpper") %>' />
                                        <asp:HiddenField runat="server" ID="hdnLslUslqpTargetUppper" Value='<%# Eval("LslUslTargetUpper") %>' />
                                        <asp:HiddenField runat="server" ID="hdnRecomqpTargetUppper" Value='<%# Eval("RecommendationTU") %>' />
                                        <asp:TextBox runat="server" ID="qpTargetUppper" onmouseover="ShowREcommandationforQParam(this, 'TargetUpper');"  onfocus="ShowREcommandationforQParam(this, 'TargetUpper');" onblur="ExceedRangeWarningForQualityParam(this, 'TargetUpper')" CssClass="form-control qpInputControls allowDecimal opcs" ClientIDMode="Static" Text='<%# Eval("TargetUpper") %>'></asp:TextBox>
                                         <asp:Label runat="server" ID="qpMandatoryTargetUppper" CssClass="glyphicon glyphicon-star mandatory" ></asp:Label>
                                        <asp:Image runat="server" ID="qpLimitImgTargetUppper" Width="30px" Height="30px" CssClass="opimages" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("ImageRecommandationTU") %>' />

                                         <asp:HiddenField runat="server" ID="hdTUDependency" Value='<%# Eval("DependencyTargetUpper") %>' />
                                        <asp:HiddenField runat="server" ID="hdTUIndependentParameter" Value='<%# Eval("IndependentParameterTargetUpper") %>' />
                                        <asp:HiddenField runat="server" ID="hdTUParameter" Value='<%# Eval("ParameterTargetUpper") %>' />
                                         <asp:HiddenField runat="server" ID="hdTUCustomename" Value='<%# Eval("CustomenameTargetUpper") %>' />
                                            <asp:HiddenField runat="server" ID="hdTUMandatory" Value='<%# Eval("MandatoryTargetUpper") %>' />
                                    </span>

                                    <span class="opspan">
                                        <asp:HiddenField runat="server" ID="hdnParamIDqpAchievedLower" Value='<%# Eval("ParamIdActualLower") %>' />
                                        <asp:HiddenField runat="server" ID="hdnLslUslqpAchievedLower" Value='<%# Eval("LslUslActualLower") %>' />
                                        <asp:HiddenField runat="server" ID="hdnRecomqpAchievedLower" Value='<%# Eval("RecommendationlAL") %>' />
                                        <asp:TextBox runat="server" ID="qpAchievedLower" onmouseover="ShowREcommandationforQParam(this, 'ActualLower');" onfocus="ShowREcommandationforQParam(this, 'ActualLower');" onblur="ExceedRangeWarningForQualityParam(this, 'ActualLower')" CssClass="form-control qpInputControls allowDecimal opcs" ClientIDMode="Static" Text='<%# Eval("ActualLower") %>'></asp:TextBox>
                                         <asp:Label runat="server" ID="qpMandatoryAchievedLower" CssClass="glyphicon glyphicon-star mandatory"></asp:Label>
                                        <asp:Image runat="server" ID="qpLimitImgAchievedLower" Width="30px" Height="30px" CssClass="opimages"
 Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("ImageRecommandationAL") %>' />
                                         <asp:HiddenField runat="server" ID="hdALDependency" Value='<%# Eval("DependencyActualLower") %>' />
                                        <asp:HiddenField runat="server" ID="hdALIndependentParameter" Value='<%# Eval("IndependentParameterActualLower") %>' />
                                        <asp:HiddenField runat="server" ID="hdALParameter" Value='<%# Eval("ParameterActualLower") %>' />
                                        <asp:HiddenField runat="server" ID="hdALCustomename" Value='<%# Eval("CustomenameActualLower") %>' />
                                            <asp:HiddenField runat="server" ID="hdALMandatory" Value='<%# Eval("MandatoryActualLower") %>' />
                                    </span>

                                    <span class="opspan">
                                        <asp:HiddenField runat="server" ID="hdnParamIDAchievedUppper" Value='<%# Eval("ParamIdActualUpper") %>' />
                                        <asp:HiddenField runat="server" ID="hdnLslUslAchievedUppper" Value='<%# Eval("LslUslActualUpper") %>' />
                                        <asp:HiddenField runat="server" ID="hdnRecomAchievedUppper" Value='<%# Eval("RecommendationAU") %>' />
                                        <asp:TextBox runat="server" ID="qpAchievedUppper" onmouseover="ShowREcommandationforQParam(this, 'ActualUpper');" onfocus="ShowREcommandationforQParam(this, 'ActualUpper');" onblur="ExceedRangeWarningForQualityParam(this, 'ActualUpper')" CssClass="form-control qpInputControls allowDecimal opcs" ClientIDMode="Static" Text='<%# Eval("ActualUpper") %>'></asp:TextBox>
                                         <asp:Label runat="server" ID="qpMandatoryAchievedUppper" CssClass="glyphicon glyphicon-star mandatory" ></asp:Label>
                                        <asp:Image runat="server" ID="qpLimitImgAchievedUppper" Width="30px" Height="30px" CssClass="opimages" Style="margin-left: 2px" onclick="showLargeImage(this)" ImageUrl='<%#Eval("ImageRecommandationAU") %>' />
                                         <asp:HiddenField runat="server" ID="hdAUDependency" Value='<%# Eval("DependencyActualUpper") %>' />
                                        <asp:HiddenField runat="server" ID="hdAUIndependentParameter" Value='<%# Eval("IndependentParameterActualUpper") %>' />
                                        <asp:HiddenField runat="server" ID="hdAUParameter" Value='<%# Eval("ParameterActualUpper") %>' />
                                          <asp:HiddenField runat="server" ID="hdAUCustomename" Value='<%# Eval("CustomenameActualUpper") %>' />
                                            <asp:HiddenField runat="server" ID="hdAUMandatory" Value='<%# Eval("MandatoryActualUpper") %>' />
                                    </span>
                                    <%--  <asp:CheckBox runat="server" ID="qpcbvalue" ClientIDMode="Static" />
                            <asp:DropDownList runat="server" ID="qpddlvalue" Style="width: 100%" CssClass="form-control qpInputControls" ClientIDMode="Static">
                            </asp:DropDownList>--%>
                                    <asp:Label runat="server" ID="qpParameterID" Text='<%# Eval("PrameterId") %>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="qpobjectType" Text='<%# Eval("ObjectType") %>' Visible="false"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>

            <div style="text-align: center">
                <span id="showRecommandation"></span>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="allowEdit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="viewInputModule" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="saveASOK" EventName="serverclick" />
              <asp:AsyncPostBackTrigger ControlID="templateCreate" EventName="serverclick" />
            <asp:AsyncPostBackTrigger ControlID="templateornewSdocOK" EventName="serverclick" />
             <asp:AsyncPostBackTrigger ControlID="deleteSdocYes" EventName="serverclick" />
            <asp:AsyncPostBackTrigger ControlID="saveConfirmYes" EventName="serverclick" />
            <asp:AsyncPostBackTrigger ControlID="CreatenewSDocYes" EventName="serverclick" />
                 <asp:AsyncPostBackTrigger ControlID="leaveunsavedDataWarning" EventName="serverclick" />
            <%-- <asp:postbacktrigger ControlID="saveASOK"  />--%>
        </Triggers>
    </asp:UpdatePanel>




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

      <div class="modal fade" id="unsavedDataWarningModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Warning!</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/warnig.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="unsavedDataWarningmessageText" style="font-size: 17px;">Changes you made may not be saved.</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                  
                      <input type="button" value="Leave" class="btn btn-info" id="leaveunsavedDataWarning" runat="server" onserverclick="leaveunsavedDataWarning_ServerClick" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="Cancel" class="btn btn-info" onclick="CancelunsavedDataWarning()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
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
                    <input type="button" value="Yes" class="btn btn-info dataSaved" id="saveConfirmYes" onserverclick="saveConfirmYes_ServerClick" runat="server" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="No" class="btn btn-info" id="saveConfirmNo" onclick="saveConfirmNoFun()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="CreateNewSDocID" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/confirm.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="CreateNewSDocmessageText" style="font-size: 17px;">Confirmation</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <%--<asp:Button runat="server" Text="Close" ID="Button1" CssClass="btn btn-info" BackColor="#5D7B9D" ForeColor="white" />--%>
                    <%--  onserverclick="saveConfirmYes_ServerClick"--%>
                    <input type="button" value="Yes" class="btn btn-info dataSaved" id="CreatenewSDocYes" onserverclick="CreatenewSDocYes1_ServerClick" runat="server" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="No" class="btn btn-info" id="CreatenewSDocNo" onclick="CreatenewSDocNoFun()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="SaveasConfirmationModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                    <label style="font-weight: unset">
                        <input type="radio" id="newSDocid" runat="server" name="confirm" value="newSdoc" />
                        <span>Create new System Document ID?</span>
                    </label>
                    <br />

                    <label style="font-weight: unset">
                        <input type="radio" id="incrementPlunge" runat="server" name="confirm" value="IncrementPlunge" />
                        <span>Increment Plunge?</span>
                    </label>
                    <br>
                    <label style="font-weight: unset">
                        <input type="radio" name="confirm" runat="server" id="incrementSdocSubCategory" value="IcrementSDoc" />
                        <span>Increment Sub category?</span>
                    </label>

                 
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                   
                    <input type="button" runat="server" value="OK" class="btn btn-info dataSaved" id="saveASOK" onclick="if (!SaveAsclick1()) return;" style="background-color: #5D7B9D; color: white" data-dismiss="modal" onserverclick="saveASOK_ServerClick" />
                    <%-- if (!SaveAsclick1()) return;--%>
                    <input type="button" value="Cancel" class="btn btn-info" id="saveAsCancel" onclick="SaveAsCancelclick()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="TemplateCreateConfirmationModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                   <span style="font-size: 17px;">Are you sure, you want to create new template?</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                   
                    <input type="button" runat="server"  value="OK" class="btn btn-info" id="templateCreate"  style="background-color: #5D7B9D; color: white" data-dismiss="modal" onserverclick="templateCreate_ServerClick" />
                    <%-- if (!SaveAsclick1()) return;--%>
                    <input type="button" value="Cancel" class="btn btn-info dataSaved" id="templatecreateCancel" onclick="TemplateCreateCancelclick()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

        <div class="modal fade" id="templateOrnewSdocConfirmationModal" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Confirmation?</h4>
                </div>
                <div class="modal-body">
                    <label style="font-weight: unset">
                        <input type="radio" id="TemplatenewSdoc" runat="server" name="confirm" value="newSdoc" />
                        <span>Create new System Document ID?</span>
                    </label>
                    <br />

                    <label style="font-weight: unset">
                        <input type="radio" id="newTemplate" runat="server" name="confirm" value="newTemplate" />
                        <span>Create new Template?</span>
                    </label>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                   
                    <input type="button" runat="server" value="OK" class="btn btn-info dataSaved" id="templateornewSdocOK" style="background-color: #5D7B9D; color: white" data-dismiss="modal" onserverclick="templateornewSdocOK_ServerClick" />
                    <%-- if (!SaveAsclick1()) return;--%>
                    <input type="button" value="Cancel" class="btn btn-info" id="templateornewSdcoCancel" onclick="templateornewSdocCancelclick()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="showLargeImage" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 60vw; height: 80vh; padding: 5px">
            <div class="modal-content" style="border: 2px solid #5D7B9D;width:100%;height:100%;">
                <div class="modal-header" style="position: relative; padding: 0px; border-bottom: none">
                    <a data-dismiss="modal" class="glyphicon glyphicon-remove" style="float: right; z-index: 5; color: #5D7B9D; font-size: 25px"></a>
                </div>
                <div class="modal-body" style="text-align: center; padding: 0px;width:100%;height:95%">
                    <img id="largeImage" style="height: 100%; width: 100%" />&nbsp;&nbsp;&nbsp;
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
                    <span id="removeImageNameValue" runat="server" style="visibility: hidden"></span>
                    <span id="removeImagePathValue" runat="server" style="visibility: hidden"></span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                
                    <input type="button" value="Yes" class="btn btn-info" id="removeImageConfimYes" onserverclick="removeImageConfimYes_ServerClick" runat="server" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="No" class="btn btn-info" id="removeImageConfirmNo" onclick="removeImageNo()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="exitPageWarning" role="dialog" style="min-width: 300px;">
        <div class="modal-dialog modal-dialog-centered" style="width: 450px">
            <div class="modal-content" style="border: 2px solid #5D7B9D">
                <div class="modal-header" style="background-color: #5D7B9D; padding: 8px">

                    <h4 class="modal-title" style="color: white;">Warning!</h4>
                </div>
                <div class="modal-body">
                    <img src="Images/warnig.png" width="40" />&nbsp;&nbsp;&nbsp;
                   
							<span id="exitPagewarningmsgtext" style="font-size: 17px;">Are u sur ?</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <%--<asp:Button runat="server" Text="Close" ID="Button1" CssClass="btn btn-info" BackColor="#5D7B9D" ForeColor="white" />--%>
                    <input type="button" value="OK" class="btn btn-info" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
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
							<span style="font-size: 17px;">Are you sure, you want to delete this SDocId?</span>
                </div>
                <div class="modal-footer" style="padding: 5px; border-top: 1px solid #5D7B9D">
                    <%--<asp:Button runat="server" ID="btn" OnClick="deleteSdocYes_ServerClick" UseSubmitBehavior="false" CssClass="btn btn-info" Text="Delete" style="background-color: #5D7B9D; color: white" />--%>
                    <input type="button" value="Delete" class="btn btn-info dataSaved" id="deleteSdocYes"  runat="server" onserverclick="deleteSdocYes_ServerClick" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
                    <input type="button" value="Cancel" class="btn btn-info" id="deleteSdocNo" onclick="fundeleteSdocNo()" style="background-color: #5D7B9D; color: white" data-dismiss="modal" />
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

    <script>
        //$(function () {

        //    formUnloadPrompt('form');

        //});
        $(document).ready(function () {

            $("#menu0").addClass("in active");

            $("a[href$='#menu0']").css("background-color", "#3c3b54");
            $("a[href$='#menu0']").css("color", "#FFFFFF");

            var wHeight = $(window).height() - (235);
            $('.themetoggle').css('height', wHeight);
            console.log("H =" + wHeight);

           
            //$('#lstVendors').children().each(function () {
            //    let datetime = $(this).find('#gitxtDate');
            //    let date = $(datetime);
            //     debugger;
            //    console.log("$$" + $(datetime));
            //    $(datetime).datetimepicker({
            //        format: 'DD-MM-YYYY HH:mm',
            //        //yyyy-MM-ddTHH:mm
            //        locale: 'fr',
            //        tooltips: {
            //            today: 'Revenir à aujourd\'hui',
            //            clear: 'Effacer la sélection',
            //            close: 'Fermer',
            //            selectMonth: 'Sélectionner le mois',
            //            prevMonth: 'Mois précédent',
            //            nextMonth: 'Mois suivant',
            //            selectYear: 'Sélectionner l\'année',
            //            prevYear: 'Année précédente',
            //            nextYear: 'Année suivante',
            //            selectDecade: 'Sélectionner la décénnie',
            //            prevDecade: 'Décénnie précédente',
            //            nextDecade: 'Décennie suivante',
            //            prevCentury: 'Siècle précédent',
            //            nextCentury: 'Siècle suivant'
            //        }
            //    });
            //    //$(datetime).on("dp.change", function (e) {
            //    //    $('[id$=txtToDate]').data("DateTimePicker").minDate(e.date);
            //    //});
            //});


            //document.querySelector('.eStore_buy_now_button').addEventListener("click", function () {
            //    window.btn_clicked = true;      //set btn_clicked to true
            //});

           //formUnloadPrompt('form');


             // formmodified=0;
             //$('form *').change(function () {
             //    formmodified = 1;
             //});
             //window.onbeforeunload = confirmExit;
             //function confirmExit() {
             //    if (formmodified == 1) {
             //        return "New information not saved. Do you wish to leave the page?";
             //    }
             //}
             //$("input[name='commit']").click(function () {
             //    formmodified = 0;
             //});
        });
        'use strict';
        (() => {
            const modified_inputs = new Set();
            const defaultValue = 'defaultValue';
            // store default values
            addEventListener('beforeinput', evt => {
                const target = evt.target;
                if (!(defaultValue in target.dataset)) {
                    target.dataset[defaultValue] = ('' + (target.value || target.textContent)).trim();
                }
            });

            // detect input modifications
            addEventListener('input', evt => {
                const target = evt.target;
                let original = target.dataset[defaultValue];

                let current = ('' + (target.value || target.textContent)).trim();

                if (original !== current) {
                    if (!modified_inputs.has(target)) {
                        modified_inputs.add(target);
                    }
                } else if (modified_inputs.has(target)) {
                    modified_inputs.delete(target);
                }
            });

            addEventListener(
                'saved',
                function (e) {
                    modified_inputs.clear();
                },
                false
            );
            $(".dataSaved").click(function () {
                modified_inputs.clear();
            });
            //$("[id*=saveConfirmYes]").click(function () {
            //    modified_inputs.clear();
            //});

            addEventListener('beforeunload', evt => {
                if (modified_inputs.size) {
                    const unsaved_changes_warning = 'Changes you made may not be saved.';
                    evt.returnValue = unsaved_changes_warning;
                    return unsaved_changes_warning;
                }
            });

        })();

        function formUnloadPrompt(formSelector) {
            debugger;
            var formA = $(formSelector).serialize(), formB, formSubmit = false;

            // Detect Form Submit
            $(formSelector).submit(function () {
                window.onbeforeunload = null;
                formSubmit = true;
            });

            window.onbeforeunload = null;
            // Handle Form Unload    
            window.onbeforeunload = function (e) {
                debugger;

                if (formSubmit) return;
                formB = $(formSelector).serialize();
                if (formA != formB) {
                    //$("[id*=exitPageWarning]").modal("show");
                    // return alert("jnaj");
                    //return;
                    e.preventDefault();
                    e.returnValue = "Your changes have not been saved.";
                    // return "Your changes have not been saved.";

                    return false;
                }

            };

        }



        $('.modal-content').resizable({        });            $('.modal-dialog').draggable();        $('#showlargeimage').on('show.bs.modal', function () {            $(this).find('.modal-body').css({                'max-height': '100%'            });        });

        function showConfirmationForDeleteSDoc() {
            $('[id*=deleteSDocConfimation]').modal('show');
            return false;
        }
        function fundeleteSdocNo() {
            $('[id*=deleteSDocConfimation]').modal('hide');
        }

        function allParameterCalculation() {

            for (var i = 2; i < $('#lstVendors div').length; i++) {
                var div = $('#lstVendors').children()[i];
                if (div.children[2].readOnly == true) {
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Page is not in edit mode.");
                    break;
                }
            }
            let cInitialComponentDia = "";
             let cInputComponentDiaOD = "";
            let cCurrentWheelDia = "";
            let cWheelSpeedVs = "";
            let cWheelSpeddNs = "";
            let cDressFeedrateOD = "", cDressFeedrateFace = "", cDressFeedrateRadius = "";
            let cDressLeadOD = "", cDressLeadFace = "", cDressLeadODRadius = "";
            let cCutterWidth = "", cDresserDia="", cDresserSpeedRPM ="", cDresserSpeedMPS="";
            for (var i = 0; i < $('#ulWorkpiece').children().length; i++) {
                debugger;
                let div = $('#ulWorkpiece').children()[i];
                if (div.children[1].value == "Initial Component Dia (mm)") {
                    cInitialComponentDia = div.children[2].value;
                    // break;
                }
                if (div.children[1].value == "Input Component Dia-OD (mm)") {
                    cInputComponentDiaOD = div.children[2].value;
                    // break;
                }
            }

            for (var i = 1; i < $('#ulOperationalParameterGrind').children().length; i++) {
                var div = $('#ulOperationalParameterGrind').children()[i];
                if (div.children[1].value == "Current Wheel Diameter (mm)") {
                    cCurrentWheelDia = div.children[2].value;
                }
            }
            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];
                if (div.children[1].value == "Dressing Feed Rate- OD (mm/min)") {
                    cDressFeedrateOD = div.children[2].value;

                }
                if (div.children[1].value == "Dressing Feed Rate- Face (mm/min)") {
                    cDressFeedrateFace = div.children[2].value;
                }
                if (div.children[1].value == "Dressing Feed Rate- Radius (mm/min)") {
                    cDressFeedrateRadius = div.children[2].value;
                }
                if (div.children[1].value == "Wheel Speed (Vs) (m/s)") {
                    cWheelSpeedVs = div.children[2].value;
                }
                if (div.children[1].value == "Dresser Speed (rpm)") {
                    cDresserSpeedRPM = div.children[2].value;
                }
            }
            for (var i = 0; i < $('#ulConsumable').children().length; i++) {
                var div = $('#ulConsumable').children()[i];
                if (div.children[1].value == "Cutter Width (mm)") {
                    cCutterWidth = div.children[2].value;
                }
                if (div.children[1].value == "Dresser Dia") {
                    cDresserDia = div.children[2].value;
                }
            }

            //Work RPM Calculation
            ($('#ulOperationalParameter').children()).each(function () {
                let cworkspeedrpm = $(this).find('#opWorkRPMDecimal').val();
                if (cInputComponentDiaOD != "" && cworkspeedrpm != "") {
                    let result = (Math.PI * parseFloat(cInputComponentDiaOD) * parseFloat(cworkspeedrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(this).find('#opWorkmsDEcimal').val(result);
                } else {
                    $(this).find('#opWorkmsDEcimal').val("");
                }
            });

            //Wheelms calculation
            ($('#ulOperationalParameter').children()).each(function () {
                let cwheelms = $(this).find('#opWheelmsDEcimal').val();
                if (cCurrentWheelDia != "" && cwheelms != "") {
                    let result = (60 * parseFloat(cwheelms)) / (Math.PI * (parseFloat(cCurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(this).find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(this).find('#opWheelRPMDEcimal').val("");
                }
            });

            //WheelSpeed NS calculation, Lead DI calculation, Overlap- ratio
            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];
                if (div.children[1].value == "Wheel Speed (Ns) (rpm)") {
                    if (cWheelSpeedVs != "" && cCurrentWheelDia != "") {
                        let result = (parseFloat(cWheelSpeedVs) * 60000) / (Math.PI * parseFloat(cCurrentWheelDia));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        cWheelSpeddNs = result;
                    }
                    else {
                        div.children[2].value = "";
                        cWheelSpeddNs = "";
                    }
                }

                if (div.children[1].value == "Lead-DI OD (mm/rev)") {
                    if (cWheelSpeddNs != "" && cDressFeedrateOD != "") {

                        let result = parseFloat(cDressFeedrateOD) / parseFloat(cWheelSpeddNs);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        cDressLeadOD = result;
                    }
                    else {
                        div.children[2].value = "";
                        cDressLeadOD = ""
                    }
                }
                if (div.children[1].value == "Lead-DI Face (mm/rev)") {
                    if (cWheelSpeddNs != "" && cDressFeedrateFace != "") {
                        let result = parseFloat(cDressFeedrateFace) / parseFloat(cWheelSpeddNs);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        cDressLeadFace = result;
                    }
                    else {
                        div.children[2].value = "";
                        cDressLeadFace = "";
                    }
                }
                if (div.children[1].value == "Lead-DI Radius (mm/rev)") {
                    if (cWheelSpeddNs != "" && cDressFeedrateRadius != "") {
                        let result = parseFloat(cDressFeedrateRadius) / parseFloat(cWheelSpeddNs);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        cDressLeadODRadius = result;
                    }
                    else {
                        div.children[2].value = "";
                        cDressLeadODRadius = ""
                    }
                }

                if (div.children[1].value == "Overlap Ratio - OD") {
                    if (cDressLeadOD != "" && cCutterWidth != "") {
                        let result = parseFloat(cCutterWidth) / parseFloat(cDressLeadOD);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                    }
                    else {
                        div.children[2].value = "";
                    }
                }
                if (div.children[1].value == "Overlap Ratio - Face") {
                    if (cDressLeadFace != "" && cCutterWidth != "") {
                        let result = parseFloat(cCutterWidth) / parseFloat(cDressLeadFace);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                    }
                    else {
                        div.children[2].value = "";
                    }
                }
                if (div.children[1].value == "Overlap Ratio - Radius") {
                    if (cDressLeadODRadius != "" && cCutterWidth != "") {
                        let result = parseFloat(cCutterWidth) / parseFloat(cDressLeadODRadius);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                    }
                    else {
                        div.children[2].value = "";
                    }
                }

                if (div.children[1].value == "Dresser Speed (mps)") {
                    if (cDresserSpeedRPM != "" && cDresserDia != "") {
                        let result = Math.PI * parseFloat(cDresserDia) * parseFloat(cDresserSpeedRPM) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        cDresserSpeedMPS = result;
                    }
                    else {
                        div.children[2].value = "";
                        cDresserSpeedMPS = ""
                    }
                }
            }
            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];
                if (div.children[1].value == "Crush Ratio") {
                    if (cDresserSpeedMPS != "" && cWheelSpeedVs != "") {
                        let result = parseFloat(cDresserSpeedMPS) / parseFloat(cWheelSpeedVs);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                    }
                    else {
                        div.children[2].value = "";
                    }
                }
            }
            return false;
        }

        function bindDependancyValue(val, inputModuleTab) {
            debugger;


            let result;
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("DataInputModule.aspx/getDependentparameterDetails") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{Independentparametervalue: "' +  $(val).val() + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                     result= checkLSLUSLValue(dataitem, $(val).val());
                },
                error: function (Result) {
                    alert("Error");
                }
            });

            if (result == "") {
                //alert("Not affected");
                let dependencyFlagValue = $(val).closest('div').find('#hdDependancyFlag').val();
                let param2Id = $(val).closest('div').find('#hfParameterID').val();
                let param2selectedddlValue = $(val).val();
                if (dependencyFlagValue == "1") {
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: '<%= ResolveUrl("DataInputModule.aspx/getDependencyValue") %>',
                        contentType: "application/json; charset=utf-8",
                        crossDomain: true,
                        data: '{param2Id: "' + param2Id + '", param2selectedddlValue: "' + param2selectedddlValue + '"}',
                        dataType: "json",
                        success: function (response) {
                            var dataitem = response.d;

                            if (dataitem.length != 0) {
                                param1Id = dataitem[0].ParameterId1;
                                param1 = dataitem[0].Parameter1;
                                //General Info
                                if (inputModuleTab == "GeneralInfo") {
                                    ($('#lstVendors').children()).each(function () {
                                        if ($(this).find('#hfParameterID').val() == param1Id) {
                                            $(this).find("#giddlvalue").empty();
                                            for (let i = 0; i < dataitem.length; i++) {
                                                if (i == 0) {
                                                    $(this).find("#giddlvalue").append($("<option></option>").html("Select " + param1));
                                                }
                                                if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                } else {
                                                    $(this).find("#giddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                }
                                                if ($(this).find("#giddlvalue").prop('selectedIndex') == 0) {
                                                    $(this).find('#hfddlvalue').val("");
                                                } else {
                                                    $(this).find('#hfddlvalue').val($(this).find("#giddlvalue").val());
                                                }
                                            }
                                        }
                                    });
                                }
                                //Machine Tool
                                if (inputModuleTab == "MachineTool") {
                                    ($('#ulMachinetool').children()).each(function () {
                                        if ($(this).find('#hfParameterID').val() == param1Id) {
                                            $(this).find("#mtddlvalue").empty();
                                            for (let i = 0; i < dataitem.length; i++) {
                                                if (i == 0) {
                                                    $(this).find("#mtddlvalue").append($("<option></option>").html("Select " + param1));
                                                }
                                                if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                } else {
                                                    $(this).find("#mtddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                }

                                                if ($(this).find("#mtddlvalue").prop('selectedIndex') == 0) {
                                                    $(this).find('#hfddlvalue').val("");
                                                } else {
                                                    $(this).find('#hfddlvalue').val($(this).find("#mtddlvalue").val());
                                                }
                                            }
                                        }
                                    });
                                }
                                //Consumables
                                if (inputModuleTab == "Consumable") {
                                    ($('#ulConsumable').children()).each(function () {
                                        if ($(this).find('#hfParameterID').val() == param1Id) {
                                            $(this).find("#cmddlvalue").empty();
                                            for (let i = 0; i < dataitem.length; i++) {
                                                if (i == 0) {
                                                    $(this).find("#cmddlvalue").append($("<option></option>").html("Select " + param1));
                                                }
                                                if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                } else {
                                                    $(this).find("#cmddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                }
                                                if ($(this).find("#cmddlvalue").prop('selectedIndex') == 0) {
                                                    $(this).find('#hfddlvalue').val("");
                                                } else {
                                                    $(this).find('#hfddlvalue').val($(this).find("#cmddlvalue").val());
                                                }
                                            }
                                        }
                                    });
                                }
                                //Workpiece
                                if (inputModuleTab == "Workpiece") {
                                    ($('#ulWorkpiece').children()).each(function () {
                                        if ($(this).find('#hfParameterID').val() == param1Id) {
                                            $(this).find("#wpddlvalue").empty();
                                            for (let i = 0; i < dataitem.length; i++) {
                                                if (i == 0) {
                                                    $(this).find("#wpddlvalue").append($("<option></option>").html("Select " + param1));
                                                }
                                                if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                } else {
                                                    $(this).find("#wpddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                }
                                                if ($(this).find("#wpddlvalue").prop('selectedIndex') == 0) {
                                                    $(this).find('#hfddlvalue').val("");
                                                } else {
                                                    $(this).find('#hfddlvalue').val($(this).find("#wpddlvalue").val());
                                                }
                                            }
                                        }
                                    });
                                }
                                //Operational Grinding
                                if (inputModuleTab == "OperationalGrinding") {
                                    ($('#ulOperationalParameterGrind').children()).each(function () {
                                        if ($(this).find('#hfParameterID').val() == param1Id) {
                                            $(this).find("#opddlvalue").empty();
                                            for (let i = 0; i < dataitem.length; i++) {
                                                if (i == 0) {
                                                    $(this).find("#opddlvalue").append($("<option></option>").html("Select " + param1));
                                                }
                                                if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                } else {
                                                    $(this).find("#opddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                }
                                                if ($(this).find("#opddlvalue").prop('selectedIndex') == 0) {
                                                    $(this).find('#hfddlvalue').val("");
                                                } else {
                                                    $(this).find('#hfddlvalue').val($(this).find("#opddlvalue").val());
                                                }
                                            }
                                        }
                                    });
                                }
                                //OperationalDressing
                                if (inputModuleTab == "OperationalDressing") {
                                    ($('#ulOPDressing').children()).each(function () {
                                        if ($(this).find('#hfParameterID').val() == param1Id) {
                                            $(this).find("#opddlvalue").empty();
                                            for (let i = 0; i < dataitem.length; i++) {
                                                if (i == 0) {
                                                    $(this).find("#opddlvalue").append($("<option></option>").html("Select " + param1));
                                                }
                                                if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                } else {
                                                    $(this).find("#opddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                }
                                                if ($(this).find("#opddlvalue").prop('selectedIndex') == 0) {
                                                    $(this).find('#hfddlvalue').val("");
                                                } else {
                                                    $(this).find('#hfddlvalue').val($(this).find("#opddlvalue").val());
                                                }
                                            }
                                        }
                                    });
                                }
                            }

                        },
                        error: function (Result) {
                            alert("Error");
                        }
                    });
                }
                if (inputModuleTab == "GeneralInfo") {
                    if ($(val).closest('div').find('#giddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#giddlvalue").val());
                    }
                }
                if (inputModuleTab == "MachineTool") {
                    if ($(val).closest('div').find('#mtddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#mtddlvalue").val());
                    }
                }
                if (inputModuleTab == "Consumable") {
                    if ($(val).closest('div').find('#cmddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#cmddlvalue").val());
                    }
                }
                if (inputModuleTab == "Workpiece") {
                    if ($(val).closest('div').find('#wpddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#wpddlvalue").val());
                    }
                }
                if (inputModuleTab == "OperationalGrinding") {
                    if ($(val).closest('div').find('#opddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#opddlvalue").val());
                    }
                }
                if (inputModuleTab == "OperationalDressing") {
                    if ($(val).closest('div').find('#opddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#opddlvalue").val());
                    }
                }
            } else {
                console.log(result);
                let warningtxt = "";
                for (let i = 0; i < result.length; i++) {
                    if (warningtxt == "") {

                        warningtxt = "<span>You can't change the value from " + previousDDlvalue + " to " + $(val).val() + ". Because it affect the following prameters LSL and USL value</span> <br/><span>" + (i + 1) + ". " + result[i].parameter + ", its  value is " + result[i].value + " but it should be between " + result[i].lsl + " and " + result[i].usl + "</span>";

                    } else {
                        warningtxt = warningtxt + " <br /><span> " + (i + 1) + ". " + result[i].parameter + ", its value is " + result[i].value + " but it should be between " + result[i].lsl + " and " + result[i].usl + "</span>";

                    }

                }
                $(val).val(previousDDlvalue);
                if (inputModuleTab == "GeneralInfo") {
                    if ($(val).closest('div').find('#giddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#giddlvalue").val());
                    }
                }
                if (inputModuleTab == "MachineTool") {
                    if ($(val).closest('div').find('#mtddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#mtddlvalue").val());
                    }
                }
                if (inputModuleTab == "Consumable") {
                    if ($(val).closest('div').find('#cmddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#cmddlvalue").val());
                    }
                }
                if (inputModuleTab == "Workpiece") {
                    if ($(val).closest('div').find('#wpddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#wpddlvalue").val());
                    }
                }
                if (inputModuleTab == "OperationalGrinding") {
                    if ($(val).closest('div').find('#opddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#opddlvalue").val());
                    }
                }
                if (inputModuleTab == "OperationalDressing") {
                    if ($(val).closest('div').find('#opddlvalue').prop('selectedIndex') == 0) {
                        $(val).closest('div').find('#hfddlvalue').val("");
                    } else {
                        $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#opddlvalue").val());
                    }
                }
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("");
                $("#warningmessageText").append(warningtxt);

            }
            return false;
        }
        var previousDDlvalue = "";
        function savePreviousdata(val, im) {
            previousDDlvalue = $(val).val();
        }

        function checkLSLUSLValue(dataitem, ddlvalue) {
            console.log(dataitem);
            let result = false;
            let affectedparameters = [];
            let increment = 0;
            for (let i = 0; i < dataitem.length; i++) {
                let lslarray = [];
                let uslarray = [];
                let z = 0;
                let dependparam = dataitem[i].dependentParameter;
                for (let j = 0; j < dataitem[i].parameterDependencies.length; j++) {
                   
                    let lsl = dataitem[i].parameterDependencies[j].LSL;
                    let usl = dataitem[i].parameterDependencies[j].USL;
                    let param = dataitem[i].parameterDependencies[j].Parameter2;
                    let paramvalue = dataitem[i].parameterDependencies[j].Parameter2Value;

                    for (let gi = 0; gi < $('#lstVendors').children().length; gi++) {
                        let div = $('#lstVendors').children()[gi];
                        if (div.children[1].value == param && div.children[2].value == paramvalue) {
                            lslarray[z] = lsl;
                            uslarray[z] = usl;
                            z++;
                        }
                    }

                    for (let m = 0; m < $('#ulMachinetool').children().length; m++) {
                        var div = $('#ulMachinetool').children()[m];
                        if (div.children[1].value == param && div.children[2].value == paramvalue) {
                            lslarray[z] = lsl;
                            uslarray[z] = usl;
                            z++;
                        }
                    }
                    for (let c = 0; c < $('#ulConsumable').children().length; c++) {
                        var div = $('#ulConsumable').children()[c];
                        if (div.children[1].value == param && div.children[2].value == paramvalue) {
                            lslarray[z] = lsl;
                            uslarray[z] = usl;
                            z++;
                        }
                    }

                    for (let w = 0; w < $('#ulWorkpiece').children().length; w++) {
                        let div = $('#ulWorkpiece').children()[w];
                        if (div.children[1].value == param && div.children[2].value == paramvalue) {
                            lslarray[z] = lsl;
                            uslarray[z] = usl;
                            z++;
                        }
                    }
                    for (var g = 1; g < $('#ulOperationalParameterGrind').children().length; g++) {
                        let div = $('#ulOperationalParameterGrind').children()[g];
                        if (div.children[1].value == param && div.children[2].value == paramvalue) {
                            lslarray[z] = lsl;
                            uslarray[z] = usl;
                            z++;
                        }
                    }
                    for (let d = 1; d < $('#ulOPDressing').children().length; d++) {
                        let div = $('#ulOPDressing').children()[d];
                        if (div.children[1].value == param && div.children[2].value == paramvalue) {
                            lslarray[z] = lsl;
                            uslarray[z] = usl;
                            z++;
                        }
                    }
                }

                let finalLSL = Math.max.apply(null, lslarray);
                let finalUSL = Math.min.apply(null, uslarray);
               // alert(finalLSL + " ; " + finalUSL);

                for (let gi = 0; gi < $('#lstVendors').children().length; gi++) {
                    let div = $('#lstVendors').children()[gi];
                    if (div.children[1].value == dependparam) {
                        let val = div.children[2].value;
                        if (val != "") {
                            if (parseFloat(val) < parseFloat(finalLSL) || parseFloat(val) > parseFloat(finalUSL)) {
                                result = true;
                                //affectedparameters = affectedparameters +","+  dependparam;
                                affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: val };
                                increment++;
                            }
                        }
                    }
                }

                for (let m = 0; m < $('#ulMachinetool').children().length; m++) {
                    var div = $('#ulMachinetool').children()[m];
                    if (div.children[1].value == dependparam) {
                        let val = div.children[2].value;
                        if (val != "") {
                            if (parseFloat(val) < parseFloat(finalLSL) || parseFloat(val) > parseFloat(finalUSL)) {
                                result = true;
                                 affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: val };
                                increment++;
                            }
                        }
                    }
                }

                for (let c = 0; c < $('#ulConsumable').children().length; c++) {
                    var div = $('#ulConsumable').children()[c];
                    if (div.children[1].value == dependparam) {
                        let val = div.children[2].value;
                        if (val != "") {
                            if (parseFloat(val) < parseFloat(finalLSL) || parseFloat(val) > parseFloat(finalUSL)) {
                                result = true;
                                  affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: val };
                                increment++;
                            }
                        }
                    }
                }

                for (let w = 0; w < $('#ulWorkpiece').children().length; w++) {
                    let div = $('#ulWorkpiece').children()[w];
                    if (div.children[1].value == dependparam) {
                        let val = div.children[2].value;
                        if (val != "") {
                            if (parseFloat(val) < parseFloat(finalLSL) || parseFloat(val) > parseFloat(finalUSL)) {
                                result = true;
                                affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: val };
                                increment++;
                            }
                        }
                    }
                }

                ($('#ulOperationalParameter').children()).each(function () {
                    let feedparam = $(this).find('#hdFeedrateParameter').val();
                    let feedvalue = $(this).find('#opFeedRatedecimal').val();
                    if (feedparam == dependparam) {
                        if (feedvalue != "") {
                            if (parseFloat(feedvalue) < parseFloat(finalLSL) || parseFloat(feedvalue) > parseFloat(finalUSL)) {
                                result = true;
                                 affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: feedvalue };
                                increment++;
                            }
                        }
                    }

                    let docparam = $(this).find('#opDOChdParameter').val();
                    let docvalue = $(this).find('#opDOCDecimal').val();
                    if (docparam == dependparam) {
                        if (docvalue != "") {
                            if (parseFloat(docvalue) < parseFloat(finalLSL) || parseFloat(docvalue) > parseFloat(finalUSL)) {
                                result = true;
                                  affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: docvalue };
                                increment++;
                            }
                        }
                    }

                    let faceparam = $(this).find('#opFacehdParameter').val();
                    let facevalue = $(this).find('#opFaceDecimal').val();
                    if (faceparam == dependparam) {
                        if (facevalue != "") {
                            if (parseFloat(facevalue) < parseFloat(finalLSL) || parseFloat(facevalue) > parseFloat(finalUSL)) {
                                result = true;
                                affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: facevalue };
                                increment++;
                            }
                        }
                    }

                    let workrpmparam = $(this).find('#ophdWorkRPMParameter').val();
                    let workrpmvalue = $(this).find('#opWorkRPMDecimal').val();
                    if (workrpmparam == dependparam) {
                        if (workrpmvalue != "") {
                            if (parseFloat(workrpmvalue) < parseFloat(finalLSL) || parseFloat(workrpmvalue) > parseFloat(finalUSL)) {
                                result = true;
                                affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: workrpmvalue };
                                increment++;
                            }
                        }
                    }

                    let wheelmsparam = $(this).find('#ophdWheelmsParameter').val();
                    let wheelmsvalue = $(this).find('#opWheelmsDEcimal').val();
                    if (wheelmsparam == dependparam) {
                        if (wheelmsvalue != "") {
                            if (parseFloat(wheelmsvalue) < parseFloat(finalLSL) || parseFloat(wheelmsvalue) > parseFloat(finalUSL)) {
                                result = true;
                                affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: wheelmsvalue };
                                increment++;
                            }
                        }
                    }

                    //let workmsparam = $(this).find('#ophdWorkmsParameter').val();
                    //let workmsvalue = $(this).find('#opWorkmsDEcimal').val();
                    //if (workmsparam == dependparam) {
                    //    if (workmsvalue != "") {
                    //        if (parseFloat(workmsvalue) < parseFloat(finalLSL) || parseFloat(workmsvalue) > parseFloat(finalUSL)) {
                    //            result = true;
                    //        }
                    //    }
                    //}

                    //let wheelrpmparam = $(this).find('#hdWheelRPMPaarmeter').val();
                    //let wheelrpmvalue = $(this).find('#opWheelRPMDEcimal').val();
                    //if (wheelrpmparam == dependparam) {
                    //    if (wheelrpmvalue != "") {
                    //        if (parseFloat(wheelrpmvalue) < parseFloat(finalLSL) || parseFloat(wheelrpmvalue) > parseFloat(finalUSL)) {
                    //            result = true;
                    //        }
                    //    }
                    //}

                });
                for (var g = 1; g < $('#ulOperationalParameterGrind').children().length; g++) {
                    let div = $('#ulOperationalParameterGrind').children()[g];
                    if (div.children[1].value == dependparam) {
                        let val = div.children[2].value;
                        if (val != "") {
                            if (parseFloat(val) < parseFloat(finalLSL) || parseFloat(val) > parseFloat(finalUSL)) {
                                result = true;
                                affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: val };
                                increment++;
                            }
                        }
                    }
                }
                for (let d = 1; d < $('#ulOPDressing').children().length; d++) {
                    let div = $('#ulOPDressing').children()[d];
                    if (div.children[1].value == dependparam) {
                        let val = div.children[2].value;
                        if (val != "") {
                            if (parseFloat(val) < parseFloat(finalLSL) || parseFloat(val) > parseFloat(finalUSL)) {
                                result = true;
                                 affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: val };
                                increment++;
                            }
                        }
                    }
                }

                ($('#ulQualityParameter').children()).each(function () {
                    let TLparam = $(this).find('#hdTLParameter').val();
                    let TLparamvalue = $(this).find('#qpTargetLower').val();
                    if (TLparam == dependparam) {
                        if (TLparamvalue != "") {
                            if (parseFloat(TLparamvalue) < parseFloat(finalLSL) || parseFloat(TLparamvalue) > parseFloat(finalUSL)) {
                                result = true;
                               affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: TLparamvalue };
                                increment++;
                            }
                        }
                    }

                    let TUparam = $(this).find('#hdTUParameter').val();
                    let TUparamvalue = $(this).find('#qpTargetUppper').val();
                    if (TUparam == dependparam) {
                        if (TUparamvalue != "") {
                            if (parseFloat(TUparamvalue) < parseFloat(finalLSL) || parseFloat(TUparamvalue) > parseFloat(finalUSL)) {
                                result = true;
                               affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: TUparamvalue };
                                increment++;
                            }
                        }
                    }
                    let ALparam = $(this).find('#hdALParameter').val();
                    let ALparamvalue = $(this).find('#qpAchievedLower').val();
                    if (ALparam == dependparam) {
                        if (ALparamvalue != "") {
                            if (parseFloat(ALparamvalue) < parseFloat(finalLSL) || parseFloat(ALparamvalue) > parseFloat(finalUSL)) {
                                result = true;
                               affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: ALparamvalue };
                                increment++;
                            }
                        }
                    }

                    let AUparam = $(this).find('#hdAUParameter').val();
                    let AUparamvalue = $(this).find('#qpAchievedUppper').val();
                    if (AUparam == dependparam) {
                        if (AUparamvalue != "") {
                            if (parseFloat(AUparamvalue) < parseFloat(finalLSL) || parseFloat(AUparamvalue) > parseFloat(finalUSL)) {
                                result = true;
                                affectedparameters[increment] = { parameter: dependparam, lsl: finalLSL, usl: finalUSL, value: AUparamvalue };
                                increment++;
                            }
                        }
                    }
                });

            }
            if (affectedparameters.length > 0) {

            }
            return affectedparameters;
        }

        function setActiveTabe(tabname) {

            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("DataInputModule.aspx/setActiveTabinSession") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{tabname: "' + $(tabname).attr('href') + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                },
                error: function (Result) {
                    alert("Error");
                }
            });
        }



        function DateTimeFormateCheck(val) {

            var date = val.value.toString("dd/MM/yyyy hh:mm tt");
            if (date == "") {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Invalid date format, Expecting date in dd/MM/yyyy hh24:mm format.")
                val.value = "";
            }

        }



        //RemoveImage
        function removeImageFun(imgval) {
            debugger;
            let imageName = $(imgval).closest('span').find('#lblImageName').text();
            let imagePath = "~/" + $(imgval).closest('span').find('#img').attr('src');
            $("#removeImage").val(imageName + "," + imagePath);
            //$('[id*=removeImageNameValue]').val(imageName);
            //$('[id*=removeImagePathValue]').val(imagePath);
            $('[id*=removeImageConfimation]').modal('show');
            return false;
        }
        function removeImageNo() {
            $("#removeImage").val("");
            $('[id*=removeImageConfimation]').modal('hide');
        }


        //For Hardness limit range
        function HardnessUnitChange(el) {
            debugger;
            let hardness = $(el).parent().children()[0].value;
            let limitrange = [];
            limitrange = $(el).val().split(',');
            if (parseFloat(hardness) < parseFloat(limitrange[0]) || parseFloat(hardness) > parseFloat(limitrange[1])) {
                $(el).parent().children()[0].value = "";
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Value should be between " + limitrange[0] + " and " + limitrange[1]);
            }
            //$(ele).attr('title', param);
            //$(ele).tooltip();
        }
        function wpHardness(el) {
            debugger;
            let hardness = $(el).val();
            let limitrange = [];
            limitrange = $(el).parent().children()[1].value.split(',');
            if (parseFloat(hardness) < parseFloat(limitrange[0]) || parseFloat(hardness) > parseFloat(limitrange[1])) {
                $(el).val("");
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Value should be between " + limitrange[0] + " and " + limitrange[1]);
            }
        }

        function showLargeImage(image) {

            $('[id*=largeImage]').attr('src', '');            $('[id*=showLargeImage]').modal('show');            $('[id*=largeImage]').attr('src', $(image).attr('src'));
        }



        function sendFile(file, id) {

            var formData = new FormData();
            formData.append('file', $('#' + id)[0].files[0]);
            name = $('#' + id)[0].parentElement.children[1].value;
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("DataInputModule.aspx/AddIamgeNameToSessionImageDetails") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{imagename: "' + name + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                },
                error: function (Result) {
                    alert("Error");
                }
            });
            $.ajax({
                async: false,
                type: 'post',
                url: '<%= ResolveUrl("fileUploader.ashx") %>',
                //url: 'fileUploader.ashx'
                data: formData,
                success: function (status) {
                    if (status != 'error') {
                        var my_path = "UploadImages/" + status;
                        $("#imgPhoto").attr("src", my_path);
                        console.log("Enter sendfile");
                    }
                },
                processData: false,
                contentType: false,
                error: function () {
                    alert("Whoops something went wrong!");
                }
            });
        }
        var _URL = window.URL || window.webkitURL;
        function file(e) {
            var file, img;
            debugger;


            var id = e.getAttribute('id');
            if ((file = e.files[0])) {
                img = new Image();
                img.onload = function () {
                    sendFile(file, id);
                };
                img.onerror = function () {
                    alert("Not a valid file:" + file.type);
                };
                img.src = _URL.createObjectURL(file);
            }
        }

        function showpop(msg, title) {
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

        function imageUpadateDone() {

            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("DataInputModule.aspx/SetSessionImageDetailsNull") %>',
                //url: "DataInputModule.aspx/giObjective",
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;

                },
                error: function (Result) {
                    alert("Error");
                }
            });
            var i = 0;
            for (i = 0; i < $('input[type="file"]').length; i++) {

                file($('input[type="file"]')[i]);
            }
            showpop('Images Uploaded successfully');
            return false;
        }
        function addNewFileUpload(img) {
            debugger;
            //var newImageId = ($(img).parent().children().length - 1);
            //var spanid = 'span' + (newImageId + 1);
            //var imgid = 'imageUpload' + (newImageId + 1);
            //var imgname = 'txtimageName' + (newImageId + 1);
            //var str = '<span id="' + spanid + '"><asp:FileUpload  runat="server"  /><asp:TextBox runat="server" CssClass="form-control" ></asp:TextBox></span>';
            ////onchange="file(this)"
            //$("#" + $(img).parent().attr('id')).append(str);
            //$("#" + spanid).children()[0].setAttribute("id", imgid);
            //$("#" + spanid).children()[1].setAttribute("id", imgname);

            //var hiddenval = $('#UploadedImages').val();
            //if ($('#UploadedImages').val() == "") {
            //    $('#UploadedImages').val(imgid + ',' + imgname);
            //} else {
            //    $('#UploadedImages').val(hiddenval + ',' + imgid + ',' + imgname);
            //}
            //return false;

            var newImageId = $(img).parent().children()[2].children.length;
            var spanid = 'span' + (newImageId + 1);
            var imgid = 'imageUpload' + (newImageId + 1);
            var imgname = 'txtimageName' + (newImageId + 1);
            var str = '<span id="' + spanid + '"><asp:FileUpload  runat="server"  /> <asp:TextBox runat="server" placeholder="Enter Image Name"  CssClass="form-control" ></asp:TextBox></span>';
            //onchange="file(this)"
            $("#appendImage").append(str);
            $("#" + spanid).children()[0].setAttribute("id", imgid);
            $("#" + spanid).children()[1].setAttribute("id", imgname);
            $("#" + imgname).val("");
            return false;
        }
        function removeNewFileUpload(img) {

            var removeImageId = $(img).parent().children()[2].children.length;
            if (removeImageId == 1) {
                return false;
            }
            $("#appendImage span").last().remove();

            return false;
        }



        $('.continue').click(function () {            $('.nextPrevious > .active').next('li').find('a').trigger('click');        });        $('.back').click(function () {            $('.nextPrevious > .active').prev('li').find('a').trigger('click');        });

        //For Show  Recommendation

        function showRecommendation(ele, param) {

            var recommandation="",LSL="",USL="", finalrecommandation="", limit="";
            if (param == "GeneralInfo") {
                recommandation = $(ele).closest('div').find('#giRecommandation').val();
                 limit = $(ele).closest('div').find('#giLimitRange').val();
            } else if (param == "MachineTool") {
                recommandation = $(ele).closest('div').find('#mtRecommandation').val();
                 limit = $(ele).closest('div').find('#mtLimitRange').val();
            } else if (param == "Consumable") {
                recommandation = $(ele).closest('div').find('#cmRecommandation').val();
                limit = $(ele).closest('div').find('#cmLimitRange').val();
            } else if (param == "Workpiece") {
                recommandation = $(ele).closest('div').find('#wpRecommandation').val();
                 limit = $(ele).closest('div').find('#wpLimitRange').val();
            } else if (param == "OperationalGrinding") {
                recommandation = $(ele).closest('div').find('#opRecommandation').val();
                 limit = $(ele).closest('div').find('#opLimitRange').val();
            } else if (param == "OperationalDressing") {
                recommandation = $(ele).closest('div').find('#opRecommandation').val();
                 limit = $(ele).closest('div').find('#opLimitRange').val();
            }
            if (limit != "") {
                LSL = limit.split(';')[0];
                USL = limit.split(';')[1];
            }
           
            if (LSL != "" && USL != "" && recommandation != "") {
                finalrecommandation = recommandation + " ; LSL=" + LSL + ", USL=" + USL;
            } else if (LSL != "" && USL != "" && recommandation == "") {
                finalrecommandation = "LSL=" + LSL + ", USL=" + USL;;
            } else if (LSL == "" && USL == "" && recommandation != "") {
                finalrecommandation = recommandation;
            }
            $(ele).attr('title', finalrecommandation);
            $(ele).tooltip({
                tooltipClass: 'tooltipclass',
                position: {
                    my: "center bottom",
                    at: "center top-10",
                    collision: "none"
                }
            });
        }

        function ShowREcommandationforQParam(ele, parameter) {

            let param="", LSL="",USL="",finalrecommandation="",limit="";
            if (parameter == "TargetLower") {
                param = $(ele).parent().closest('div').find("#hdnRecomqpTargetLower").val();
                limit = $(ele).parent().closest('div').find("#hdnLslUslqpTargetLower").val();
            } else if (parameter == "TargetUpper") {
                param = $(ele).parent().closest('div').find("#hdnRecomqpTargetUppper").val();
                  limit = $(ele).parent().closest('div').find("#hdnLslUslqpTargetUppper").val();
            } else if (parameter == "ActualLower") {
                param = $(ele).parent().closest('div').find("#hdnRecomqpAchievedLower").val();
                  limit = $(ele).parent().closest('div').find("#hdnLslUslqpAchievedLower").val();
            } else if (parameter == "ActualUpper") {
                param = $(ele).parent().closest('div').find("#hdnRecomAchievedUppper").val();
                  limit = $(ele).parent().closest('div').find("#hdnLslUslAchievedUppper").val();
            }
             if (limit != "") {
                LSL = limit.split(';')[0];
                USL = limit.split(';')[1];
            }
           
            if (LSL != "" && USL != "" && param != "") {
                finalrecommandation = param + " ; LSL=" + LSL + ", USL=" + USL;
            } else if (LSL != "" && USL != "" && param == "") {
                finalrecommandation = "LSL=" + LSL + ", USL=" + USL;;
            } else if (LSL == "" && USL == "" && param != "") {
                finalrecommandation = param;
            }

            $(ele).attr('title', finalrecommandation);
            $(ele).tooltip({tooltipClass: 'tooltipclass',
                position: {
                    my: "center bottom",
                    at: "center top-10",
                    collision: "none"
                }
            });
        }
        function ShowREcommandationforOpeartionalParam(ele, parameter) {

            let param = "", LSL = "", USL = "", finalrecommandation = "", limit = "";
            if (parameter == "FeedRate") {
                // param = $(ele).parent().children()[3].value;
                param = $(ele).parent().closest('div').find("#hdnRecomopFeedRate").val();
                limit = $(ele).parent().closest('div').find("#hdnLslUslopFeedRate").val();
                

            } else if (parameter == "DOC") {
                param = $(ele).parent().closest('div').find("#hdnRecomopDOC").val();
                 limit = $(ele).parent().closest('div').find("#hdnLslUslopDOC").val();
                
            } else if (parameter == "Face") {
                param = $(ele).parent().closest('div').find("#hdnRecomopFace").val();
                limit = $(ele).parent().closest('div').find("#hdnLslUslopFace").val();
                
            }
            else if (parameter == "WorkRPM") {
                param = $(ele).parent().closest('div').find("#hdnRecomopWorkRPM").val();
                limit = $(ele).parent().closest('div').find("#hdnLslUslopWorkRPM").val();
                
            } else if (parameter == "Wheelms") {
                param = $(ele).parent().closest('div').find("#hdnRecomopWheelms").val();
                limit = $(ele).parent().closest('div').find("#hdnLslUslopWheelms").val();
                

            } else if (parameter == "WheelRPM") {
                param = $(ele).parent().closest('div').find("#hdnRecomopWheelRPM").val();
                limit = $(ele).parent().closest('div').find("#hdnLslUslopWheelRPM").val();

            } else if (parameter == "Workms") {
                param = $(ele).parent().closest('div').find("#hdnRecomopWorkms").val();
                limit = $(ele).parent().closest('div').find("#hdnLslUslopWorkms ").val();

                }
            if (limit != "") {
                LSL = limit.split(';')[0];
                USL = limit.split(';')[1];
            }
           
            if (LSL != "" && USL != "" && param != "") {
                finalrecommandation = param + " ; LSL=" + LSL + ", USL=" + USL;
            } else if (LSL != "" && USL != "" && param == "") {
                finalrecommandation = "LSL=" + LSL + ", USL=" + USL;;
            } else if (LSL == "" && USL == "" && param != "") {
                finalrecommandation = param;
            }

            $(ele).attr('title', finalrecommandation);
            $(ele).tooltip({tooltipClass: 'tooltipclass',
                position: {
                    my: "center bottom",
                    at: "center top-10",
                    collision: "none"
                }});
        }



        //For Show Limit Range Warning

        function ExceedRangeWarning(data, param) {
            debugger;
            if ($(data).closest('div').find('#hiddenDatatype').val() == 'Decimal' || $(data).closest('div').find('#hiddenDatatype').val() == 'Integer') {
                if (isNaN($(data).val())) {
                    $(data).val("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("This value is not a number");
                    return;
                }
                if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                    $(data).val("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Before and After number space is not allowed");
                    return;
                }
            } else {
                if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                    $(data).val("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Before and After string space is not allowed");
                    return;
                }
            }
            let value;
            let parameter = [];
            if (param == "GeneralInfo") {
               
                if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                    let parameter = $(data).parent().closest('div').find('#gihdParameterName').val();
                    let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFunc(parseFloat(dataitem[0]),parseFloat(dataitem[1]));
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    value = $(data).closest('div').find('#giLimitRange').val();
                    parameter = value.split(';');
                    if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                        $(data).val("");
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                         callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                        return;
                    }
                }
            } else if (param == "MachineTool") {
                if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                    let parameter = $(data).parent().closest('div').find('#mthdParameterName').val();
                    let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFunc(parseFloat(dataitem[0]),parseFloat(dataitem[1]));
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    value = $(data).closest('div').find('#mtLimitRange').val();
                    parameter = value.split(';');
                    if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                        $(data).val("");
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                         callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                        return;
                    }
                }
            }
        }

        function ExceedRangeWarningOP(data) {

            let value = $(data).val();
            let parameter = [];
            parameter = $(data).parent().children()[3].value.split(';');
            if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                $(data).val("");
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                return;
            }
        }

        function ExceedRangeWarningForQualityParam(data, param) {
            debugger;
            if (isNaN($(data).val())) {
                $(data).val("");
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("This value is not a number");
                return;
            } else {
                if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                    $(data).val("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Before and After number space is not allowed");
                    return;
                }
            }
            let value = $(data).val();
            let parameter = [];

            if (param == "TargetLower") {
                if ($(data).parent().closest('div').find('#hdTLDependency').val() == "True") {
                    let parameter = $(data).parent().closest('div').find('#hdTLParameter').val();
                    let independentparam = $(data).parent().closest('div').find('#hdTLIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    parameter = $(data).parent().closest('div').find("#hdnLslUslqpTargetLower").val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                        $(data).val("");
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                        callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                        return;
                    }
                }
            } else if (param == "TargetUpper") {
                if ($(data).parent().closest('div').find('#hdTUDependency').val() == "True") {
                    let parameter = $(data).parent().closest('div').find('#hdTUParameter').val();
                    let independentparam = $(data).parent().closest('div').find('#hdTUIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    parameter = $(data).parent().closest('div').find("#hdnLslUslqpTargetUppper").val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                        $(data).val("");
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                        callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                        return;
                    }
                }
            } else if (param == "ActualLower") {
                if ($(data).parent().closest('div').find('#hdALDependency').val() == "True") {
                    let parameter = $(data).parent().closest('div').find('#hdALParameter').val();
                    let independentparam = $(data).parent().closest('div').find('#hdALIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    parameter = $(data).parent().closest('div').find("#hdnLslUslqpAchievedLower").val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                        $(data).val("");
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                        callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                        return;
                    }
                }
            } else if (param == "ActualUpper") {
                if ($(data).parent().closest('div').find('#hdAUDependency').val() == "True") {
                    let parameter = $(data).parent().closest('div').find('#hdAUParameter').val();
                    let independentparam = $(data).parent().closest('div').find('#hdAUIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    parameter = $(data).parent().closest('div').find("#hdnLslUslAchievedUppper").val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                        $(data).val("");
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                        callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                        return;
                    }
                }
            }
        }


        function ExceedRangeWarningForOperationalParam(data, param) {
            debugger;
            let value = $(data).val();
            let parameter = [];

            if (param == "FeedRate") {
                //parameter = $(data).parent().children()[2].value.split(';');
                parameter = $(data).closest('span').find('#hdnLslUslopFeedRate').val().split(';');
            } else if (param == "DOC") {
                if ($(data).closest('div').find('#opDOChiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opDOChiddenObjectType').val() == 'Integer') {
                    if (isNaN($(data).val())) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }
                if ($(data).closest('div').find('#opDochdDependency').val() == "True") {
                    let parameter = $(data).closest('div').find('#opDOChdParameter').val();
                    let independentparam = $(data).closest('div').find('#opDochdIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    parameter = $(data).closest('span').find('#hdnLslUslopDOC').val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                        $(data).val("");
                           callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                        return;
                    }
                }
            
            }else if (param == "Face") {
                if ($(data).closest('div').find('#opFacehiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opFacehiddenObjectType').val() == 'Integer') {
                    if (isNaN($(data).val())) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }
                if ($(data).closest('div').find('#opFacehdDependency').val() == "True") {
                    let parameter = $(data).closest('div').find('#opFacehdParameter').val();
                    let independentparam = $(data).closest('div').find('#opFacehdIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    parameter = $(data).closest('span').find('#hdnLslUslopFace').val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                        $(data).val("");
                        callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                        return;
                    }
                }
            }

            else if (param == "WorkRPM") {
                //parameter = $(data).parent().children()[10].value.split(';');
                parameter = $(data).closest('span').find('#hdnLslUslopWorkRPM').val().split(';');
            } else if (param == "Wheelms") {
                //parameter = $(data).parent().children()[14].value.split(';');
                parameter = $(data).closest('span').find('#hdnLslUslopWheelms').val().split(';');

            } else if (param == "Workms") {
                if ($(data).closest('div').find('#opWorkmshiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opWorkmshiddenObjectType').val() == 'Integer') {
                    if (isNaN($(data).val())) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }
                if ($(data).closest('div').find('#hdworkmsDependency').val() == "True") {
                    let parameter = $(data).closest('div').find('#ophdWorkmsParameter').val();
                    let independentparam = $(data).closest('div').find('#hdworkmsIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    parameter = $(data).closest('span').find('#hdnLslUslopWorkms').val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                        $(data).val("");
                        callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                        return;
                    }
                }
            } else if (param == "WheelRPM") {

                if ($(data).closest('div').find('#opWheelRPMhiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opWheelRPMhiddenObjectType').val() == 'Integer') {
                    if (isNaN($(data).val())) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }
                if ($(data).closest('div').find('#hdWheelRPMDependency').val() == "True") {
                    let parameter = $(data).closest('div').find('#hdWheelRPMPaarmeter').val();
                    let independentparam = $(data).closest('div').find('#hdWheelRPMIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    parameter = $(data).closest('span').find('#hdnLslUslopWheelRPM').val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                        $(data).val("");
                        callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                        return;
                    }
                }
            }

            //if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
            //    $(data).val("");
            //    $('[id*=myWarningModal]').modal('show');
            //    $("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
            //    return;
            //}
        }

        function parameterDependencyFun(param, independentParam) {
            let LSLUSL = "";
            let independentParameters = [];
            let independentParameterswithValue = [];
            let j = 0;
            if (independentParam != "") {
                independentParameters = independentParam.split(';');
            }
            for (let i = 0; i < independentParameters.length; i++) {
                
                for (let gi = 0; gi < $('#lstVendors').children().length; gi++) {
                    let div = $('#lstVendors').children()[gi];
                    if (div.children[1].value == independentParameters[i]) {
                        let value = div.children[2].value;
                        independentParameterswithValue[j] = { parameter: div.children[1].value, value: value };
                        j++;
                    }
                }
                for (let m = 0; m < $('#ulMachinetool').children().length; m++) {
                    var div = $('#ulMachinetool').children()[m];
                    if (div.children[1].value == independentParameters[i]) {
                        let value = div.children[2].value;
                        independentParameterswithValue[j] = { parameter: div.children[1].value, value: value };
                        j++;
                    }
                }
                for (let c = 0; c < $('#ulConsumable').children().length; c++) {
                    var div = $('#ulConsumable').children()[c];
                    if (div.children[1].value == independentParameters[i]) {
                        let value = div.children[2].value;
                        independentParameterswithValue[j] = { parameter: div.children[1].value, value: value };
                        j++;
                    }
                }
                for (let w = 0; w < $('#ulWorkpiece').children().length; w++) {
                    let div = $('#ulWorkpiece').children()[w];
                    if (div.children[1].value == independentParameters[i]) {
                        let value = div.children[2].value;
                        independentParameterswithValue[j] = { parameter: div.children[1].value, value: value };
                        j++;
                    }
                }
                for (var g = 1; g < $('#ulOperationalParameterGrind').children().length; g++) {
                    let div = $('#ulOperationalParameterGrind').children()[g];
                    if (div.children[1].value == independentParameters[i]) {
                        let value = div.children[2].value;
                        independentParameterswithValue[j] = { parameter: div.children[1].value, value: value };
                        j++;
                    }
                }
                for (let d = 1; d < $('#ulOPDressing').children().length; d++) {
                    let div = $('#ulOPDressing').children()[d];
                    if (div.children[1].value == independentParameters[i]) {
                        let value = div.children[2].value;
                        independentParameterswithValue[j] = { parameter: div.children[1].value, value: value };
                        j++;
                    }
                }
            }
            console.log(independentParameterswithValue + " ; " + param);
            $.ajax({
                async: false,
                type: "POST",
                url: '<%= ResolveUrl("DataInputModule.aspx/getLimitRangeforParameterDependency") %>',
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                data: '{IndependentParameters: ' + JSON.stringify(independentParameterswithValue) + ', DependentParameter: "' + param + '"}',
                dataType: "json",
                success: function (response) {
                    var dataitem = response.d;
                   
                    LSLUSL = dataitem;
                },
                error: function (Result) {
                    alert("Error");
                }
            });
            return LSLUSL;
        }
        function callWarningFunc(lsl,usl) {
            $('[id*=myWarningModal]').modal('show');
            $("#warningmessageText").text("LSL value is "+ lsl+"and USL value is "+usl+". LSL value is greater than USL value. Please check the master data.");
        }
        function callWarningFuncwithLSLUSL(lsl,usl) {
            $('[id*=myWarningModal]').modal('show');
            $("#warningmessageText").text("Value should be between " + lsl + " and " + usl);
        }

        //Calculations


        var WheelSpeedVs = "", CurrentWheelDia = "";
        var WheelDia = "";
        var WheelCuttingSpeed = "";
        var InitialComponentDia = "";
         var InputComponentDiaOD = "";
        var WorkSpeedNw = "";
        var WorkSpeddVw = "";
        var WheelSpeddNs = "";
        var CutterWidth = "", DresserDia="";
        var DressLeadOD = "", DressLeadFace = "", DressLeadODRadius = "";
        var DresserSpeedRPM = "", DresserSpeedMPS="";
        var DressFeedrateOD = "", DressFeedrateFace = "", DressFeedrateRadius = "";
        var OverlapratioOD = "", OverlapratioFace = "", OverlapratioRadius = "";


        function OPDressingParametersCalculations(data, inputModule) {
            debugger;

            if (inputModule == "Consumables") {

                if ($(data).closest('div').find('#cmhiddenDateType').val() == 'Decimal' || $(data).closest('div').find('#cmhiddenDateType').val() == 'Integer') {
                    if (isNaN($(data).val())) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }

                if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                    let parameter = $(data).parent().closest('div').find('#cmhdParameterName').val();
                    let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    let dataitem = [];
                    dataitem = $(data).parent().closest('div').find('#cmLimitRange').val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                        $(data).val("");
                         callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + dataitem[0] + " and " + dataitem[1]);
                        return;
                    }
                }
            }

            if (inputModule == "OPDressingParam") {

                if ($(data).closest('div').find('#ophiddenDateType').val() == 'Decimal' || $(data).closest('div').find('#ophiddenDateType').val() == 'Integer') {
                    if (isNaN($(data).val())) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }

                if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                    let parameter = $(data).parent().closest('div').find('#ophdParameterName').val();
                    let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    let dataitem = [];
                    dataitem = $(data).parent().closest('div').find('#opLimitRange').val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                        $(data).val("");
                          callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + dataitem[0] + " and " + dataitem[1]);
                        return;
                    }
                }
            }

            if (inputModule == "OPGrindingParam") {

                if ($(data).closest('div').find('#ophiddenDateType').val() == 'Decimal' || $(data).closest('div').find('#ophiddenDateType').val() == 'Integer') {
                    if (isNaN($(data).val())) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }

                let value = $(data).val();

                if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                    let parameter = $(data).parent().closest('div').find('#ophdParameterName').val();
                    let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {

                    let parameter = [];
                    //parameter = $(data).parent().children()[3].value.split(';');
                    parameter = $(data).parent().closest('div').find('#opLimitRange').val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                        $(data).val("");
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                         callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                        return;
                    }
                }
            }


            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];
                if (div.children[1].value == "Dressing Feed Rate- OD (mm/min)") {
                    DressFeedrateOD = div.children[2].value;
                }
                if (div.children[1].value == "Dressing Feed Rate- Face (mm/min)") {
                    DressFeedrateFace = div.children[2].value;
                }
                if (div.children[1].value == "Dressing Feed Rate- Radius (mm/min)") {
                    DressFeedrateRadius = div.children[2].value;
                }
                if (div.children[1].value == "Wheel Speed (Ns) (rpm)") {
                    WheelSpeddNs = div.children[2].value;
                }
                if (div.children[1].value == "Overlap Ratio - OD") {
                    OverlapratioOD = div.children[2].value;
                }
                if (div.children[1].value == "Overlap Ratio - Face") {
                    OverlapratioFace = div.children[2].value;
                }
                if (div.children[1].value == "Overlap Ratio - Radius") {
                    OverlapratioRadius = div.children[2].value;
                }
                if (div.children[1].value == "Wheel Speed (Vs) (m/s)") {
                    WheelSpeedVs = div.children[2].value;
                }
                if (div.children[1].value == "Work Speed (Nw) (rpm)") {
                    WorkSpeedNw = div.children[2].value;
                }
                if (div.children[1].value == "Dresser Speed (rpm)") {
                    DresserSpeedRPM = div.children[2].value;
                }
            }

            for (var i = 0; i < $('#ulConsumable').children().length; i++) {
                var div = $('#ulConsumable').children()[i];
                if (div.children[1].value == "Max. Wheel Dia (mm)") {
                    WheelDia = div.children[2].value;

                }
                if (div.children[1].value == "Maximum Cutting Speed (m/s)") {
                    WheelCuttingSpeed = div.children[2].value;
                }
                if (div.children[1].value == "Cutter Width (mm)") {
                    CutterWidth = div.children[2].value;
                }
                if (div.children[1].value == "Dresser Dia") {
                    DresserDia = div.children[2].value;
                }
            }

            for (var i = 1; i < $('#ulOperationalParameterGrind').children().length; i++) {
                var div = $('#ulOperationalParameterGrind').children()[i];
                if (div.children[1].value == "Current Wheel Diameter (mm)") {
                    CurrentWheelDia = div.children[2].value;

                }
            }

            for (var i = 0; i < $('#ulWorkpiece').children().length; i++) {
                debugger;
                let div = $('#ulWorkpiece').children()[i];
                if (div.children[1].value == "Initial Component Dia (mm)") {
                    InitialComponentDia = div.children[2].value;
                    // break;
                }
                if (div.children[1].value == "Input Component Dia-OD (mm)") {
                    InputComponentDiaOD = div.children[2].value;
                    // break;
                }
            }

            debugger;

            ($('#ulOperationalParameter').children()).each(function () {
                if ($(this).find('#opItem').text() == "Roughing 1") {
                    R1Wheelms = $(this).find('#opWheelmsDEcimal').val();

                }
                if ($(this).find('#opItem').text() == "Roughing 2") {
                    R2Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
                if ($(this).find('#opItem').text() == "Roughing 3") {
                    R3Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
                if ($(this).find('#opItem').text() == "Roughing 4") {
                    R4Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
                if ($(this).find('#opItem').text() == "Roughing 5") {
                    R5Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }

                if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                    S1Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
                if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                    S2Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
                if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                    S3Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
                if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                    S4Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
                if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                    S5Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }

                if ($(this).find('#opItem').text() == "Finishing 1") {
                    F1Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
                if ($(this).find('#opItem').text() == "Finishing 2") {
                    F2Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
                if ($(this).find('#opItem').text() == "Finishing 3") {
                    F3Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
                if ($(this).find('#opItem').text() == "Finishing 4") {
                    F4Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
                if ($(this).find('#opItem').text() == "Finishing 5") {
                    F5Wheelms = $(this).find('#opWheelmsDEcimal').val();
                }
            });
            if (CurrentWheelDia != "" && R1Wheelms != "") {
                let result = (60 * parseFloat(R1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 1") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 1") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && R2Wheelms != "") {
                let result = (60 * parseFloat(R2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 2") {
                        $(this).find('#opWheelRPMDEcimal').val(result);

                    }
                });

            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 2") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && R3Wheelms != "") {
                let result = (60 * parseFloat(R3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 3") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 3") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && R4Wheelms != "") {
                let result = (60 * parseFloat(R4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 4") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 4") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && R5Wheelms != "") {
                let result = (60 * parseFloat(R5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 5") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 5") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && S1Wheelms != "") {
                let result = (60 * parseFloat(S1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && S2Wheelms != "") {
                let result = (60 * parseFloat(S2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && S3Wheelms != "") {
                let result = (60 * parseFloat(S3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && S4Wheelms != "") {
                let result = (60 * parseFloat(S4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && S5Wheelms != "") {
                let result = (60 * parseFloat(S5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && F1Wheelms != "") {
                let result = (60 * parseFloat(F1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 1") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 1") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && F2Wheelms != "") {
                let result = (60 * parseFloat(F2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 2") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 2") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && F3Wheelms != "") {
                let result = (60 * parseFloat(F3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 3") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 3") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && F4Wheelms != "") {
                let result = (60 * parseFloat(F4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 4") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 4") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }
            if (CurrentWheelDia != "" && F5Wheelms != "") {
                let result = (60 * parseFloat(F5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 5") {
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 5") {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });
            }


            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];
                if (div.children[1].value == "Wheel Speed (Ns) (rpm)") {
                    if (WheelSpeedVs != "" && CurrentWheelDia != "") {
                        let result = (parseFloat(WheelSpeedVs) * 60000) / (Math.PI * parseFloat(CurrentWheelDia));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        //DressLeadODRadius = result;
                        WheelSpeddNs = result;
                    }
                    else {
                        div.children[2].value = "";
                        WheelSpeddNs = "";
                        //DressLeadODRadius = ""
                    }
                }
            }
            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];
                if (div.children[1].value == "Work Speed (Vw) (m/s)") {
                    if (WorkSpeedNw != "" && InitialComponentDia != "") {
                        let result = (Math.PI * parseFloat(WorkSpeedNw) * parseFloat(InitialComponentDia)) / 60;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        //DressLeadODRadius = result;
                    }
                    else {
                        div.children[2].value = "";
                        //DressLeadODRadius = ""
                    }
                }
            }

            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];

                if (div.children[1].value == "Lead-DI OD (mm/rev)") {
                    if (WheelSpeddNs != "" && DressFeedrateOD != "") {
                        let result = parseFloat(DressFeedrateOD) / parseFloat(WheelSpeddNs);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        DressLeadOD = result;
                    }
                    else {
                        div.children[2].value = "";
                        DressLeadOD = ""
                    }
                }
                if (div.children[1].value == "Lead-DI Face (mm/rev)") {
                    if (WheelSpeddNs != "" && DressFeedrateFace != "") {
                        let result = parseFloat(DressFeedrateFace) / parseFloat(WheelSpeddNs);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        DressLeadFace = result;
                    }
                    else {
                        div.children[2].value = "";
                        DressLeadFace = "";
                    }
                }
                if (div.children[1].value == "Lead-DI Radius (mm/rev)") {
                    if (WheelSpeddNs != "" && DressFeedrateRadius != "") {
                        let result = parseFloat(DressFeedrateRadius) / parseFloat(WheelSpeddNs);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        DressLeadODRadius = result;
                    }
                    else {
                        div.children[2].value = "";
                        DressLeadODRadius = ""
                    }
                }


            }
            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];
                if (div.children[1].value == "Overlap Ratio - OD") {
                    if (DressLeadOD != "" && CutterWidth != "") {
                        let result = parseFloat(CutterWidth) / parseFloat(DressLeadOD);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                    }
                    else {
                        div.children[2].value = "";
                    }
                }
                if (div.children[1].value == "Overlap Ratio - Face") {
                    if (DressLeadFace != "" && CutterWidth != "") {
                        let result = parseFloat(CutterWidth) / parseFloat(DressLeadFace);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                    }
                    else {
                        div.children[2].value = "";
                    }
                }
                if (div.children[1].value == "Overlap Ratio - Radius") {
                    if (DressLeadODRadius != "" && CutterWidth != "") {
                        let result = parseFloat(CutterWidth) / parseFloat(DressLeadODRadius);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                    }
                    else {
                        div.children[2].value = "";
                    }
                }
            }

            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];
                if (div.children[1].value == "Dresser Speed (mps)") {
                    if (DresserSpeedRPM != "" && DresserDia != "") {
                        let result = Math.PI * parseFloat(DresserDia) * parseFloat(DresserSpeedRPM) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        DresserSpeedMPS = result;
                    }
                    else {
                        div.children[2].value = "";
                        DresserSpeedMPS = ""
                    }
                }
            }

            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];
                if (div.children[1].value == "Crush Ratio") {
                    if (DresserSpeedMPS != "" && WheelSpeedVs != "") {
                        let result = parseFloat(DresserSpeedMPS) / parseFloat(WheelSpeedVs);
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                    }
                    else {
                        div.children[2].value = "";
                    }
                }
            }


        }



        //Work Speed Vw
        var R1Workrpm, S1Workrpm, F1Workrpm;
        var R2Workrpm, S2Workrpm, F2Workrpm;
        var R3Workrpm, S3Workrpm, F3Workrpm;
        var R4Workrpm, S4Workrpm, F4Workrpm;
        var R5Workrpm, S5Workrpm, F5Workrpm;
        var R1FeedRate, R2FeedRate, R3FeedRate, R4FeedRate, R5FeedRate;
        var R1Wheelms, R2Wheelms, R3Wheelms, R4Wheelms, R5Wheelms;
        var S1Wheelms, S2Wheelms, S3Wheelms, S4Wheelms, S5Wheelms;
        var F1Wheelms, F2Wheelms, F3Wheelms, F4Wheelms, F5Wheelms;


        function OPCalculation(data, inModule) {
            debugger;
            if (inModule == "FeedRate") {
                if ($(data).closest('div').find('#opFeedratehiddenObjecttype').val() == 'Decimal' || $(data).closest('div').find('#opFeedratehiddenObjecttype').val() == 'Integer') {
                    if (isNaN($(data).val())) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }

                if ($(data).closest('div').find('#hdFeedrateDependency').val() == "True") {
                    let parameter = $(data).closest('div').find('#hdFeedrateParameter').val();
                    let independentparam = $(data).closest('div').find('#hdFeedrateIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {

                    let lslusl = [];
                    lslusl = $(data).closest('div').find('#hdnLslUslopFeedRate').val().split(";");
                    if (parseFloat($(data).val()) < parseFloat(lslusl[0]) || parseFloat($(data).val()) > parseFloat(lslusl[1])) {
                        $(data).val("");
                          callWarningFuncwithLSLUSL(lslusl[0], lslusl[1]);
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + lslusl[0] + " and " + lslusl[1]);
                        return;
                    }
                }
            }

            if (inModule == "WorkRPM") {
                if ($(data).closest('div').find('#opWorkRPMhiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opWorkRPMhiddenObjectType').val() == 'Integer') {
                    if (isNaN($(data).val())) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }

                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }
                if ($(data).closest('div').find('#ophdWokRPMDependency').val() == "True") {
                    let parameter = $(data).closest('div').find('#ophdWorkRPMParameter').val();
                    let independentparam = $(data).closest('div').find('#ophdWorkRPMIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {

                    let lslusl = [];
                    lslusl = $(data).closest('div').find('#hdnLslUslopWorkRPM').val().split(";");
                    if (parseFloat($(data).val()) < parseFloat(lslusl[0]) || parseFloat($(data).val()) > parseFloat(lslusl[1])) {
                        $(data).val("");
                         callWarningFuncwithLSLUSL(lslusl[0], lslusl[1]);
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + lslusl[0] + " and " + lslusl[1]);
                        return;
                    }
                }
            }
            
            if (inModule == "Wheelms") {
                if ($(data).closest('div').find('#opWheelmshiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opWheelmshiddenObjectType').val() == 'Integer') {
                    if (isNaN($(data).val())) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }

                if ($(data).closest('div').find('#ophdWheelmsDependency').val() == "True") {
                    let parameter = $(data).closest('div').find('#ophdWheelmsParameter').val();
                    let independentparam = $(data).closest('div').find('#ophdWheelmsIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    let lslusl = [];
                    lslusl = $(data).closest('div').find('#hdnLslUslopWheelms').val().split(";");
                    if (parseFloat($(data).val()) < parseFloat(lslusl[0]) || parseFloat($(data).val()) > parseFloat(lslusl[1])) {
                        $(data).val("");
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + lslusl[0] + " and " + lslusl[1]);
                         callWarningFuncwithLSLUSL(lslusl[0], lslusl[1]);
                        return;
                    }
                }
            }

           
            for (var i = 1; i < $('#ulOperationalParameterGrind').children().length; i++) {
                var div = $('#ulOperationalParameterGrind').children()[i];
                if (div.children[1].value == "Current Wheel Diameter (mm)") {
                    CurrentWheelDia = div.children[2].value;

                }
            }
           
            for (var i = 0; i < $('#ulWorkpiece').children().length; i++) {
                debugger;
                let div = $('#ulWorkpiece').children()[i];
                if (div.children[1].value == "Initial Component Dia (mm)") {
                    InitialComponentDia = div.children[2].value;
                    // break;
                }
                if (div.children[1].value == "Input Component Dia-OD (mm)") {
                    InputComponentDiaOD = div.children[2].value;
                    // break;
                }
            }
          

            if ($(data).closest('div').find('#opItem').text() == "Roughing 1") {
                R1Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && R1Wheelms != "") {
                    let result = (60 * parseFloat(R1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Roughing 2") {
                R2Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && R2Wheelms != "") {
                    let result = (60 * parseFloat(R2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Roughing 3") {
                R3Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && R3Wheelms != "") {
                    let result = (60 * parseFloat(R3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Roughing 4") {
                R4Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && R4Wheelms != "") {
                    let result = (60 * parseFloat(R4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Roughing 5") {
                R5Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && R5Wheelms != "") {
                    let result = (60 * parseFloat(R5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 1") {
                S1Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && S1Wheelms != "") {
                    let result = (60 * parseFloat(S1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 2") {
                S2Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && S2Wheelms != "") {
                    let result = (60 * parseFloat(S2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 3") {
                S3Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && S3Wheelms != "") {
                    let result = (60 * parseFloat(S3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 4") {
                S4Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && S4Wheelms != "") {
                    let result = (60 * parseFloat(S4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 5") {
                S5Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && S5Wheelms != "") {
                    let result = (60 * parseFloat(S5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Finishing 1") {
                F1Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && F1Wheelms != "") {
                    let result = (60 * parseFloat(F1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Finishing 2") {
                F2Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && F2Wheelms != "") {
                    let result = (60 * parseFloat(F2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Finishing 3") {
                F3Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && F3Wheelms != "") {
                    let result = (60 * parseFloat(F3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Finishing 4") {
                F4Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && F4Wheelms != "") {
                    let result = (60 * parseFloat(F4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Finishing 5") {
                F5Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                if (CurrentWheelDia != "" && F5Wheelms != "") {
                    let result = (60 * parseFloat(F5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                }
            }





            if ($(data).closest('div').find('#opItem').text() == "Roughing 1") {
                R1Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && R1Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R1Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                } else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }

                R1FeedRate = $(data).closest('div').find('#opFeedRatedecimal').val();
                //if (R1FeedRate != "" && R1Workrpm != "") {
                //    let result = (1000 * parseFloat(R1FeedRate)) / parseFloat(R1Workrpm);
                //    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                //    $(data).closest('div').find('#opDOCDecimal').val(result);
                //} else {
                //    $(data).closest('div').find('#opDOCDecimal').val("");
                //}
            }
            if ($(data).closest('div').find('#opItem').text() == "Roughing 2") {
                R2Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();

                if (InputComponentDiaOD != "" && R2Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R2Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }

                R2FeedRate = $(data).closest('div').find('#opFeedRatedecimal').val();
                //if (R2FeedRate != "" && R2Workrpm != "") {
                //    let result = (1000 * parseFloat(R2FeedRate)) / parseFloat(R2Workrpm);
                //    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                //    $(data).closest('div').find('#opDOCDecimal').val(result);
                //} else {
                //    $(data).closest('div').find('#opDOCDecimal').val("");
                //}
            }
            if ($(data).closest('div').find('#opItem').text() == "Roughing 3") {
                R3Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && R3Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R3Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }

                R3FeedRate = $(data).closest('div').find('#opFeedRatedecimal').val();
                //if (R3FeedRate != "" && R3Workrpm != "") {
                //    let result = (1000 * parseFloat(R3FeedRate)) / parseFloat(R3Workrpm);
                //    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                //    $(data).closest('div').find('#opDOCDecimal').val(result);
                //} else {
                //    $(data).closest('div').find('#opDOCDecimal').val("");
                //}
            }
            if ($(data).closest('div').find('#opItem').text() == "Roughing 4") {
                R4Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && R4Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R4Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }

                R4FeedRate = $(data).closest('div').find('#opFeedRatedecimal').val();
                //if (R4FeedRate != "" && R4Workrpm != "") {
                //    let result = (1000 * parseFloat(R4FeedRate)) / parseFloat(R4Workrpm);
                //    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                //    $(data).closest('div').find('#opDOCDecimal').val(result);
                //} else {
                //    $(data).closest('div').find('#opDOCDecimal').val("");
                //}
            }
            if ($(data).closest('div').find('#opItem').text() == "Roughing 5") {
                R5Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && R5Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R5Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }

                R5FeedRate = $(data).closest('div').find('#opFeedRatedecimal').val();
                //if (R5FeedRate != "" && R5Workrpm != "") {
                //    let result = (1000 * parseFloat(R5FeedRate)) / parseFloat(R5Workrpm);
                //    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                //    $(data).closest('div').find('#opDOCDecimal').val(result);
                //} else {
                //    $(data).closest('div').find('#opDOCDecimal').val("");
                //}
            }
            if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 1") {
                S1Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && S1Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S1Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 2") {
                S2Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && S2Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S2Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 3") {
                S3Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && S3Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S3Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 4") {
                S4Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && S4Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S4Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 5") {
                S5Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && S5Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S5Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Finishing 1") {
                F1Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && F1Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F1Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Finishing 2") {
                F2Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();

                if (InputComponentDiaOD != "" && F2Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F2Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Finishing 3") {
                F3Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && F3Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F3Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Finishing 4") {
                F4Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && F4Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F4Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }
            }
            if ($(data).closest('div').find('#opItem').text() == "Finishing 5") {
                F5Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                if (InputComponentDiaOD != "" && F5Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F5Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                }
                else {
                    $(data).closest('div').find('#opWorkmsDEcimal').val("");
                }
            }
        }




        function CalculateVw(data, ipModule) {
            debugger;
            let value = $(data).val();

            if ($(data).closest('div').find('#wphiddebDatatype').val() == 'Decimal' || $(data).closest('div').find('#wphiddebDatatype').val() == 'Integer') {
                if (isNaN(value)) {
                    $(data).val("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("This value is not a number");
                    return;
                }
                if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                    $(data).val("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Before and After number space is not allowed");
                    return;
                }
            } else {
                if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                    $(data).val("")
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Before and After string space is not allowed");
                    return;
                }
            }

            if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                let parameter = $(data).parent().closest('div').find('#wphdParameterName').val();
                let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                let LSLUSL = parameterDependencyFun(parameter, independentparam);
                if (LSLUSL != "") {
                    let dataitem = [];
                    dataitem = LSLUSL.split(',');
                    if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                        $(data).val("");
                       callWarningFunc(dataitem[0],dataitem[1]);
                        return;
                    }
                    if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                        $(data).val("");
                        callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                        return;
                    }
                }

            } else {
                let dataitem = [];
                dataitem = $(data).parent().closest('div').find('#wpLimitRange').val().split(';');
                if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                    $(data).val("");
                     callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                    //$('[id*=myWarningModal]').modal('show');
                    //$("#warningmessageText").text("Value should be between " + dataitem[0] + " and " + dataitem[1]);
                    return;
                }
            }

            ($('#ulOperationalParameter').children()).each(function () {
                if ($(this).find('#opItem').text() == "Roughing 1") {
                    R1Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Roughing 2") {
                    R2Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Roughing 3") {
                    R3Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Roughing 4") {
                    R4Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Roughing 5") {
                    R5Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }

                if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                    S1Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                    S2Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                    S3Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                    S4Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                    S5Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }

                if ($(this).find('#opItem').text() == "Finishing 1") {
                    F1Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Finishing 2") {
                    F2Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Finishing 3") {
                    F3Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Finishing 4") {
                    F4Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
                if ($(this).find('#opItem').text() == "Finishing 5") {
                    F5Workrpm = $(this).find('#opWorkRPMDecimal').val();
                }
            });

          
            //for (var i = 0; i < $('#ulWorkpiece div').length; i++) {
            //    debugger;
            //    let div = $('#ulWorkpiece').children()[i];

            //    if (div.children[1].value == "Initial Component Dia (mm)") {
            //        InitialComponentDia = div.children[2].value;
            //       // break;
            //    }
            //    if (div.children[1].value == "Input Component Dia-OD (mm)") {
            //        InputComponentDiaOD = div.children[2].value;
            //       // break;
            //    }
            //}
            for (var i = 0; i < $('#ulWorkpiece').children().length; i++) {
                debugger;
                let div = $('#ulWorkpiece').children()[i];
                if (div.children[1].value == "Initial Component Dia (mm)") {
                    InitialComponentDia = div.children[2].value;
                    // break;
                }
                if (div.children[1].value == "Input Component Dia-OD (mm)") {
                    InputComponentDiaOD = div.children[2].value;
                    // break;
                }
            }

            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];
                if (div.children[1].value == "Work Speed (Nw) (rpm)") {
                    WorkSpeedNw = div.children[2].value;
                }
            }

            for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                var div = $('#ulOPDressing').children()[i];
                if (div.children[1].value == "Work Speed (Vw) (m/s)") {
                    if (WorkSpeedNw != "" && InitialComponentDia != "") {
                        let result = (Math.PI * parseFloat(WorkSpeedNw) * parseFloat(InitialComponentDia)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        div.children[2].value = result;
                        //DressLeadODRadius = result;
                    }
                    else {
                        div.children[2].value = "";
                        //DressLeadODRadius = ""
                    }
                }
            }



            if (InputComponentDiaOD != "" && R1Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R1Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 1") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 1") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && R2Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R2Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 2") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 2") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && R3Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R3Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 3") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 3") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && R4Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R4Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 4") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 4") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && R5Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R5Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 5") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 5") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && S1Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S1Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && S2Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S2Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && S3Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S3Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && S4Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S4Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && S5Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S5Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && F1Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F1Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 1") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 1") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && F2Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F2Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 2") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 2") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && F3Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F3Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 3") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 3") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && F4Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F4Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 4") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 4") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
            if (InputComponentDiaOD != "" && F5Workrpm != "") {
                let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F5Workrpm)) / 60000;
                result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 5") {
                        $(this).find('#opWorkmsDEcimal').val(result);
                    }
                });
            }
            else {
                $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                    if ($(this).find('#opItem').text() == "Finishing 5") {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });
            }
        }




        function allowNumberic(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function allowAlphaNumber(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode < 97 || charCode > 122) && (charCode < 65 || charCode > 90)) {
                return false;
            }
            return true;
        }

        function allowNumber(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode < 48 || charCode > 57)) {
                return false;
            }


            return true;
        }
        $('.allowDecimal').keypress(function (evt) {
            debugger;
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            var afterdecimalpt = $(this).val().split('.')[1];
            var beforedecimalpt = $(this).val().split('.')[0];
            var pos = evt.target.selectionStart;
            var ptpos = $(this).val().indexOf(".");


            if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            if (charCode == 46 && $(this).val().indexOf('.') != -1) {
                return false;
            }


            if ($(this).val().length > 6 && $(this).val().indexOf('.') == -1) {
                if (charCode == 46) {
                    return true;
                } else {
                    return false;
                }

            }
            else {
                if (pos <= ptpos) {
                    if (beforedecimalpt != undefined) {
                        if (beforedecimalpt.length > 6) {

                            return false;
                        }

                    }

                } else {
                    if (afterdecimalpt != undefined) {
                        if (afterdecimalpt.length > 3) {
                            return false;
                        }
                    }

                }
            }


            return true;
        });
        $('.allowDecimalwithoperator').keypress(function (evt) {
            debugger;
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            var pos = evt.target.selectionStart;
            if (charCode == 45 && pos != 0) {
                return false;
            } else if (charCode == 46 && $(this).val().indexOf('.') != -1) {
                return false;
            } else if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57)  && charCode != 45) {
                return false;
            }
            return true;
        });

        $('.allow200ChComment').keypress(function (evt) {

            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ($("#giComments").val().length > 199) {
                return false;
            }
            return true;
        });

        function SaveAsCancelclick() {
            $('[id*=SaveasConfirmationModal]').modal('hide');
            //$("[id*=newSDocid]").prop("checked", false);
            //$("[id*=incrementSdocSubCategory]").prop("checked", false);
        }
        function CreatenewSDocNoFun() {
            $('[id*=CreateNewSDocID]').modal('hide');
        }
        function saveConfirmNoFun() {
            $('[id*=myConfirmationModal]').modal('hide');
        }
        function SaveAsclick1() {

            debugger;
            if ($("[id*=newSDocid]").prop("checked") == true && $("[id*=incrementSdocSubCategory]").prop("checked") == true && $("[id*=incrementPlunge]").prop("checked") == true) {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Can't select all options, only one option can select.");
                $("[id*=newSDocid]").prop("checked", false);
                $("[id*=incrementSdocSubCategory]").prop("checked", false);
                $("[id*=incrementPlunge]").prop("checked", false);
                return false;
            }
            else if ($("[id*=newSDocid]").prop("checked") == false && $("[id*=incrementSdocSubCategory]").prop("checked") == false && $("[id*=incrementPlunge]").prop("checked") == false) {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Select any one option.");
                return false;
            } else {

                return true;
            }
        }

        function enableDisableSaveButton() {
            debugger;
            if ($("#cbTemplate").is(':Checked')) {
                $("#editInputModule").css("display", "none");
            } else {
                 $("#editInputModule").css("display", "inline-block");
            }
        }

        function saveAsInputModule() {
            debugger;
            let Sdoctype;
            for (var i = 0; i < $('#lstVendors div').length; i++) {
                var div = $('#lstVendors').children()[i];
                if (div.children[1].value == "SDoc ID") {
                    let sdocid = div.children[2].value;
                    if (sdocid != "") {
                        let Sdocname = sdocid.replace("SDoc", "").split("_")[0];
                        if (Sdocname == "000000") {
                            Sdoctype = "Template";
                        } else {
                            Sdoctype = "Normal";
                        }
                    }
                    break;
                }
            }


            if ($("#cbTemplate").is(':Checked') && Sdoctype == "Normal") {
                let mandatorylist = "";
                mandatorylist = checkMandatoryFields();
                if (mandatorylist == "") {
                    $('[id*=TemplateCreateConfirmationModal]').modal('show');
                } else {
                    $("#warningmessageText").text("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").append(mandatorylist);
                }
            } else if ($("#cbTemplate").is(':Checked') == false && Sdoctype == "Normal") {
                for (var i = 0; i < $('#lstVendors div').length; i++) {
                    var div = $('#lstVendors').children()[i];
                    if (div.children[1].value == "SDoc ID") {
                        if (div.children[2].value == "") {
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Load SDoc id.");
                            return false;
                        }
                    }
                }
                let mandatorylist = "";
                mandatorylist = checkMandatoryFields();
                if (mandatorylist == "") {
                    $('[id*=SaveasConfirmationModal]').modal('show');
                } else {
                    $("#warningmessageText").text("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").append(mandatorylist);
                }
            } else if ($("#cbTemplate").is(':Checked') && Sdoctype == "Template") {
                let mandatorylist = "";
                mandatorylist = checkMandatoryFields();
                if (mandatorylist == "") {
                    $('[id*=templateOrnewSdocConfirmationModal]').modal('show');
                } else {
                    $("#warningmessageText").text("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").append(mandatorylist);
                }
            } else {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("Something went wrong.");
            }
            return false;
        }

        function templateornewSdocCancelclick() {
            $('[id*=templateOrnewSdocConfirmationModal]').modal('hide');
        }
        function templateSave() {
            //for (var i = 0; i < $('#lstVendors div').length; i++) {
            //    var div = $('#lstVendors').children()[i];
            //    if (div.children[1].value == "SDoc ID") {
            //        if (div.children[2].value == "") {
            //            $('[id*=myWarningModal]').modal('show');
            //            $("#warningmessageText").text("Load SDoc id.");
            //            return false;
            //        }
            //    }
            //}
            let mandatorylist = "";
             mandatorylist = checkMandatoryFields();
            if (mandatorylist == "") {
                  $('[id*=TemplateCreateConfirmationModal]').modal('show');
            } else {
                $("#warningmessageText").text("");
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").append(mandatorylist);
            }
          
            return false;
        }
         function TemplateCreateCancelclick() {
            $('[id*=TemplateCreateConfirmationModal]').modal('hide');
            //$("[id*=newSDocid]").prop("checked", false);
            //$("[id*=incrementSdocSubCategory]").prop("checked", false);
        }


        function ClearClick() {

            if ($('[id*=allowEdit]').length > 0) {
                var readonly;
                for (var i = 2; i < $('#lstVendors div').length; i++) {

                    var div = $('#lstVendors').children()[i];
                    if (div.children[2].readOnly == true) {
                        readonly = true;
                        break;
                    }
                }
                if (readonly) {
                    return true;

                } else {
                    $('[id*=unsavedDataWarningModal').modal('show');
                    return false;
                }
            } else {
                return true;
            }

        }
        function CancelunsavedDataWarning() {
            $('[id*=unsavedDataWarningModal').modal('hide');
        }


        function saveInputModuleFun() {
            debugger;
        
            for (var i = 0; i < $('#lstVendors div').length; i++) {
                var div = $('#lstVendors').children()[i];
                let mandatorylist = "";
                if (div.children[1].value == "SDoc ID") {
                    let Sdocid = div.children[2].value;
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: '<%= ResolveUrl("DataInputModule.aspx/exitOrcreatenewSdocConfirmation") %>',
                        //url: "DataInputModule.aspx/giObjective",
                        contentType: "application/json; charset=utf-8",
                        crossDomain: true,
                        data: '{SdocId: "' + div.children[2].value + '"}',
                        dataType: "json",
                        success: function (response) {
                            var dataitem = response.d;
                            if (dataitem == "Exists") {
                                let sdoctype = Sdocid.split('_')[0].replace('SDoc','');
                                var readonly;
                                if ($('[id*=allowEdit]').length > 0) {
                                    for (var i = 2; i < $('#lstVendors div').length; i++) {
                                        var div = $('#lstVendors').children()[i];
                                        if (div.children[2].readOnly == true) {
                                            readonly = true;
                                            break;
                                        }
                                    }
                                }
                                if (readonly == true) {
                                    $('[id*=myWarningModal]').modal('show');
                                    $("#warningmessageText").text("Page is not in edit mode.");
                                } else {
                                    mandatorylist = checkMandatoryFields();
                                    if (mandatorylist == "") {
                                        if ($("#cbTemplate").is(':Checked') && sdoctype!="000000") {
                                            $('[id*=myWarningModal]').modal('show');
                                            $("#warningmessageText").text("When Template is checked cannot save the loaded Sdocid. You can Save As the Sdocid or you can uncheck the template and save the loaded Sdocid.");
                                        } else {
                                            $('[id*=myConfirmationModal]').modal('show');
                                            $("#confirmationmessageText").text("Are you sure, you want to save this data?");
                                        }
                                       
                                    } else {
                                          $("#warningmessageText").text("");
                                        $('[id*=myWarningModal]').modal('show');
                                        $("#warningmessageText").append(mandatorylist);
                                    }
                                   
                                }

                            } else {
                                mandatorylist = checkMandatoryFields();
                                if (mandatorylist == "") {
                                    $('[id*=CreateNewSDocID]').modal('show');
                                    $("#CreateNewSDocmessageText").text("Are you sure, you want to create new SDoc Id?");
                                } else {
                                    $("#warningmessageText").text("");
                                    $('[id*=myWarningModal]').modal('show');
                                    $("#warningmessageText").append(mandatorylist);
                                }

                            }
                        },
                        error: function (Result) {
                            alert("Error");
                        }
                    });


                }
            }


            return false;
            //for (var i = 0; i < $('#lstVendors div').length; i++) {
            //    var div = $('#lstVendors').children()[i];
            //    if (div.children[0].innerText == "SDoc ID") {
            //        // alert(div.children[1].val());
            //        if (div.children[1].value == "") {
            //            $('[id*=myConfirmationModal]').modal('show');
            //            $("#confirmationmessageText").text("Are you sure, you want to create new SDoc Id?");
            //            return false;
            //        }
            //    }
            //}
            //$('[id*=myConfirmationModal]').modal('show');
            //$("#confirmationmessageText").text("Are you sure, you want to save this data?");
            //return false;
        }
        function checkMandatoryFields() {
            debugger;
            let mandatoryParameterlist = [];
            let increment = 0;
            //($('#lstVendors').children()).each(function () {
            //    if ($(this).find('#hdMandatory').val() == "1") {

            //    }
            //});
            for (let gi = 0; gi < $('#lstVendors').children().length; gi++) {
                let div = $('#lstVendors').children()[gi];
                if ($(div).find("#hdMandatory").val() == "1") {
                    if ($(div).find("#giddlvalue").prop('selectedIndex') == 0) {
                        mandatoryParameterlist[increment] = $(div).find("#item").text();
                        increment++;
                    } else {
                        let val = div.children[2].value;
                        if (val == "") {
                            mandatoryParameterlist[increment] = $(div).find("#item").text();
                            increment++;
                        }
                    }
                   
                }
            }
        

            for (let m = 0; m < $('#ulMachinetool').children().length; m++) {
                var div = $('#ulMachinetool').children()[m];
                if ($(div).find("#hdMandatory").val() == "1") {
                    if ($(div).find("#mtddlvalue").prop('selectedIndex') == 0) {
                        mandatoryParameterlist[increment] = $(div).find("#mtItem").text();
                        increment++;
                    } else {
                        let val = div.children[2].value;
                        if (val == "") {
                            mandatoryParameterlist[increment] = $(div).find("#mtItem").text();
                            increment++;
                        }
                    }
                }
            }

            for (let c = 0; c < $('#ulConsumable').children().length; c++) {
                var div = $('#ulConsumable').children()[c];
                if ($(div).find("#hdMandatory").val() == "1") {
                    if ($(div).find("#cmddlvalue").prop('selectedIndex') == 0) {
                        mandatoryParameterlist[increment] = $(div).find("#cmItem").text();
                        increment++;
                    } else {
                        let val = div.children[2].value;
                        if (val == "") {
                            mandatoryParameterlist[increment] = $(div).find("#cmItem").text();
                            increment++;
                        }
                    }
                }
            }

            for (let w = 0; w < $('#ulWorkpiece').children().length; w++) {
                let div = $('#ulWorkpiece').children()[w];
                if ($(div).find("#hdMandatory").val() == "1") {
                    if ($(div).find("#wpddlvalue").prop('selectedIndex') == 0) {
                        mandatoryParameterlist[increment] = $(div).find("#wpItem").text();
                        increment++;
                    } else {
                        if ($(div).find("#wphdParameterName").val() == "Hardness") {
                            let val = $(div).find("#txtHardness").val();
                            if (val == "") {
                                mandatoryParameterlist[increment] = $(div).find("#wpItem").text();
                                increment++;
                            }
                        } else {
                            let val = div.children[2].value;
                            if (val == "") {
                                mandatoryParameterlist[increment] = $(div).find("#wpItem").text();
                                increment++;
                            }
                        }
                    }
                }
            }
            ($('#ulOperationalParameter').children()).each(function () {

                if ($(this).find('#hdFeedrateMandatory').val() == "1") {
                    let feedvalue = $(this).find('#opFeedRatedecimal').val();
                    if (feedvalue == "") {
                        mandatoryParameterlist[increment] = $(this).find('#hdFeedrateCustomename').val();
                        increment++;
                    }
                }

                if ($(this).find('#opDOChdMandatory').val() == "1") {
                    let docvalue = $(this).find('#opDOCDecimal').val();
                    if (docvalue == "") {
                        mandatoryParameterlist[increment] = $(this).find('#opDOChdCustomename').val();
                        increment++;
                    }
                }

                if ($(this).find('#opFacehdMandatory').val() == "1") {
                    let facevalue = $(this).find('#opFaceDecimal').val();
                    if (facevalue == "") {
                        mandatoryParameterlist[increment] = $(this).find('#opFacehdCustomename').val();
                        increment++;
                    }
                }

                if ($(this).find('#ophdWorkRPMMandatory').val() == "1") {
                    let workrpmvalue = $(this).find('#opWorkRPMDecimal').val();
                    if (workrpmvalue == "") {
                        mandatoryParameterlist[increment] = $(this).find('#ophdWorkRPMCustomename').val();
                        increment++;
                    }
                }


                if ($(this).find('#ophdWheelmsMandatory').val() == "1") {
                    let wheelmsvalue = $(this).find('#opWheelmsDEcimal').val();
                    if (wheelmsvalue == "") {
                        mandatoryParameterlist[increment] = $(this).find('#ophdWheelmsCustomename').val();
                        increment++;
                    }
                }


                if ($(this).find('#hdworkmsMandatory').val() == "1") {
                    let workmsvalue = $(this).find('#opworkmsdecimal').val();
                    if (workmsvalue == "") {
                        mandatoryParameterlist[increment] = $(this).find('#ophdWorkmsCustomename').val();
                        increment++;
                    }
                }

                if ($(this).find('#hdWheelRPMMandatory').val() == "1") {
                    let wheelrpmvalue = $(this).find('#opWheelRPMDEcimal').val();
                    if (wheelrpmvalue == "") {
                        mandatoryParameterlist[increment] = $(this).find('#hdWheelRPMCustomename').val();
                        increment++;
                    }
                }

            });

            for (var g = 1; g < $('#ulOperationalParameterGrind').children().length; g++) {
                let div = $('#ulOperationalParameterGrind').children()[g];
                if ($(div).find("#hdMandatory").val() == "1") {
                    if ($(div).find("#opddlvalue").prop('selectedIndex') == 0) {
                        mandatoryParameterlist[increment] = $(div).find("#opItem").text();
                        increment++;
                    } else {
                        let val = div.children[2].value;
                        if (val == "") {
                            mandatoryParameterlist[increment] = $(div).find("#opItem").text();
                            increment++;
                        }
                    }
                }
            }
            for (let d = 1; d < $('#ulOPDressing').children().length; d++) {
                let div = $('#ulOPDressing').children()[d];
                if ($(div).find("#hdMandatory").val() == "1") {
                    if ($(div).find("#opddlvalue").prop('selectedIndex') == 0) {
                        mandatoryParameterlist[increment] = $(div).find("#opItem").text();
                        increment++;
                    } else {
                        let val = div.children[2].value;
                        if (val == "") {
                            mandatoryParameterlist[increment] = $(div).find("#opItem").text();
                            increment++;
                        }
                    }
                }
            }

            ($('#ulQualityParameter').children()).each(function () {
                if ($(this).find('#hdTLMandatory').val() == "1") {
                   let TLparamvalue = $(this).find('#qpTargetLower').val();
                    if (TLparamvalue == "") {
                        mandatoryParameterlist[increment] = $(this).find('#hdTLCustomename').val();
                        increment++;
                    }
                }

               
                if ($(this).find('#hdTUMandatory').val() == "1") {
                    let TUparamvalue = $(this).find('#qpTargetUppper').val();
                    if (TUparamvalue == "") {
                        mandatoryParameterlist[increment] = $(this).find('#hdTUCustomename').val();
                        increment++;
                    }
                }
               
                if ($(this).find('#hdALMandatory').val() == "1") {
                    let ALparamvalue = $(this).find('#qpAchievedLower').val();
                    if (ALparamvalue == "") {
                        mandatoryParameterlist[increment] = $(this).find('#hdALCustomename').val();
                        increment++;
                    }
                }
                
                if ($(this).find('#hdAUMandatory').val() == "1") {
                   let AUparamvalue = $(this).find('#qpAchievedUppper').val();
                    if (AUparamvalue == "") {
                        mandatoryParameterlist[increment] = $(this).find('#hdAUCustomename').val();
                        increment++;
                    }
                }
            });

            let warningtxt = "";
            for (let i = 0; i < mandatoryParameterlist.length; i++) {
                if (warningtxt == "") {

                    warningtxt = "<span>Following parameters are mandatory: <br/><span>" + (i + 1) + ". " + mandatoryParameterlist[i] + "</span>";

                } else {
                    warningtxt = warningtxt + " <br /><span> " + (i + 1) + ". " + mandatoryParameterlist[i] + "</span>";

                }

            }

            return warningtxt;
        }

        function viewInputModule() {

            if ($("#txtViewSdocid").val() == "") {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text("SDoc ID is required.");
                return false;
            }
            else {
                return true;
            }
        }

        function openWarningModal(msg) {
            $('[id*=myWarningModal]').modal('show');
            $("#warningmessageText").text(msg);
        };
        function openErrorModal(msg) {
            $('[id*=myErrorModal]').modal('show');
            $("#errormessageText").text(msg);
        }


        $(window).resize(function () {
            var Height = $(window).height() - (235);
            $('.themetoggle').css('height', Height);
        });

       


        var bigDiv = document.getElementById('inputContainer');        bigDiv.onscroll = function () {            $('[id*=hdnScrollPos]').val(bigDiv.scrollTop);            console.log("id scroll =" + $('[id*=hdnScrollPos]').val());        }
        window.onload = function () {            bigDiv.scrollTop = $('[id*=hdnScrollPos]').val();            console.log("id load =" + bigDiv.scrollTop);        }




        var menu = "#menu0";

        $(".menuData").click(function () {
            $(".menuData").css("background-color", "");
            $(".menuData").css("color", "");

            $(this).css("background-color", "#3c3b54");
            $(this).css("color", "#FFFFFF");

            menu = $(this).attr('href');

        });




        //For Update Panel

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            //$(function () {
            //    formUnloadPrompt('form');
            //});
            $(document).ready(function () {
                // $("a[href$='#menu0']").css("background-color", "#f56740");
                //$("a[href$='#menu0']").css("color", "#FFFFFF");
                // alert(menu);

                $(menu).addClass("in active");

                var wHeight = $(window).height() - (235);
                $('.themetoggle').css('height', wHeight);
                console.log("H =" + wHeight);
                document.getElementById('inputContainer').scrollTop = $('[id*=hdnScrollPos]').val();

                //document.querySelector('.eStore_buy_now_button').addEventListener("click", function () {
                //    window.btn_clicked = true;      //set btn_clicked to true
                //});

                //window.onbeforeunload = function () {
                //    if (!window.btn_clicked) {
                //        return 'You must click "Buy Now" to make payment and finish your order. If you leave now your order will be canceled.';
                //    }
                //};

                // formmodified=0;
                //$('form *').change(function () {
                //    formmodified = 1;
                //});
                //window.onbeforeunload = confirmExit;
                //function confirmExit() {
                //    if (formmodified == 1) {
                //        return "New information not saved. Do you wish to leave the page?";
                //    }
                //}
                //$("input[name='commit']").click(function () {
                //    formmodified = 0;
                //});

               
            });
           //'use strict';
           // (() => {
           //     const modified_inputs = new Set();
           //     const defaultValue = 'defaultValue';
           //     // store default values
           //     addEventListener('beforeinput', evt => {
           //         const target = evt.target;
           //         if (!(defaultValue in target.dataset)) {
           //             target.dataset[defaultValue] = ('' + (target.value || target.textContent)).trim();
           //         }
           //     });

           //     // detect input modifications
           //     addEventListener('input', evt => {
           //         const target = evt.target;
           //         let original = target.dataset[defaultValue];

           //         let current = ('' + (target.value || target.textContent)).trim();

           //         if (original !== current) {
           //             if (!modified_inputs.has(target)) {
           //                 modified_inputs.add(target);
           //             }
           //         } else if (modified_inputs.has(target)) {
           //             modified_inputs.delete(target);
           //         }
           //     });

           //     addEventListener(
           //         'saved',
           //         function (e) {
           //             modified_inputs.clear();
           //         },
           //         false
           //     );
           //     $("[id*=saveConfirmYes]").click(function () {
           //         modified_inputs.clear();
           //     });

           //     addEventListener('beforeunload', evt => {
           //         if (modified_inputs.size) {
           //             const unsaved_changes_warning = 'Changes you made may not be saved.';
           //             evt.returnValue = unsaved_changes_warning;
           //             return unsaved_changes_warning;
           //         }
           //     });

           // })();


            function formUnloadPrompt(formSelector) {
                debugger;
                var formA = $(formSelector).serialize(), formB, formSubmit = false;
                // Detect Form Submit
                $(formSelector).submit(function () {
                    window.onbeforeunload = null;
                    formSubmit = true;
                });
                 window.onbeforeunload = null;
                 // Handle Form Unload    
                window.onbeforeunload = function (e) {
                    if (formSubmit) return;
                    formB = $(formSelector).serialize();
                    if (formA != formB) {
                        e.preventDefault();
                        e.returnValue = "Your changes have not been saved.";
                        return false;
                    }

                };
                //window.addEventListener("beforeunload", function (event) {
                //    //if (formSubmit) return;
                //    //formB = $(formSelector).serialize();
                //    //if (formA != formB) {
                //    event.returnValue = "Write something clever here..";
                //    //}
                //});
            }

           

            $('.modal-content').resizable({            });            $('.modal-dialog').draggable();            $('#showlargeimage').on('show.bs.modal', function () {                $(this).find('.modal-body').css({                    'max-height': '100%'                });            });

            function showConfirmationForDeleteSDoc() {
                $('[id*=deleteSDocConfimation]').modal('show');
                return false;
            }
            function fundeleteSdocNo() {
                $('[id*=deleteSDocConfimation]').modal('hide');
            }

           
            function allParameterCalculation() {

                for (var i = 2; i < $('#lstVendors div').length; i++) {
                    var div = $('#lstVendors').children()[i];
                    if (div.children[2].readOnly == true) {
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Page is not in edit mode.");
                        break;
                    }
                }
                let cInitialComponentDia = "";
                let cInputComponentDiaOD = "";
                let cCurrentWheelDia = "";
                let cWheelSpeedVs = "";
                let cWheelSpeddNs = "";
                let cDressFeedrateOD = "", cDressFeedrateFace = "", cDressFeedrateRadius = "";
                let cDressLeadOD = "", cDressLeadFace = "", cDressLeadODRadius = "";
                let cCutterWidth = "", cDresserDia = "", cDresserSpeedRPM = "", cDresserSpeedMPS = "";
                for (var i = 0; i < $('#ulWorkpiece').children().length; i++) {
                    debugger;
                    let div = $('#ulWorkpiece').children()[i];
                    if (div.children[1].value == "Initial Component Dia (mm)") {
                        cInitialComponentDia = div.children[2].value;
                        // break;
                    }
                    if (div.children[1].value == "Input Component Dia-OD (mm)") {
                        cInputComponentDiaOD = div.children[2].value;
                        // break;
                    }
                }

                for (var i = 1; i < $('#ulOperationalParameterGrind').children().length; i++) {
                    var div = $('#ulOperationalParameterGrind').children()[i];
                    if (div.children[1].value == "Current Wheel Diameter (mm)") {
                        cCurrentWheelDia = div.children[2].value;
                    }
                }
                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];
                    if (div.children[1].value == "Dressing Feed Rate- OD (mm/min)") {
                        cDressFeedrateOD = div.children[2].value;

                    }
                    if (div.children[1].value == "Dressing Feed Rate- Face (mm/min)") {
                        cDressFeedrateFace = div.children[2].value;
                    }
                    if (div.children[1].value == "Dressing Feed Rate- Radius (mm/min)") {
                        cDressFeedrateRadius = div.children[2].value;
                    }
                    if (div.children[1].value == "Wheel Speed (Vs) (m/s)") {
                        cWheelSpeedVs = div.children[2].value;
                    }
                    if (div.children[1].value == "Dresser Speed (rpm)") {
                        cDresserSpeedRPM = div.children[2].value;
                    }
                }
                for (var i = 0; i < $('#ulConsumable').children().length; i++) {
                    var div = $('#ulConsumable').children()[i];
                    if (div.children[1].value == "Cutter Width (mm)") {
                        cCutterWidth = div.children[2].value;
                    }
                    if (div.children[1].value == "Dresser Dia") {
                        cDresserDia = div.children[2].value;
                    }
                }

                //Work RPM Calculation
                ($('#ulOperationalParameter').children()).each(function () {
                    let cworkspeedrpm = $(this).find('#opWorkRPMDecimal').val();
                    if (cInputComponentDiaOD != "" && cworkspeedrpm != "") {
                        let result = (Math.PI * parseFloat(cInputComponentDiaOD) * parseFloat(cworkspeedrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(this).find('#opWorkmsDEcimal').val(result);
                    } else {
                        $(this).find('#opWorkmsDEcimal').val("");
                    }
                });

                //Wheelms calculation
                ($('#ulOperationalParameter').children()).each(function () {
                    let cwheelms = $(this).find('#opWheelmsDEcimal').val();
                    if (cCurrentWheelDia != "" && cwheelms != "") {
                        let result = (60 * parseFloat(cwheelms)) / (Math.PI * (parseFloat(cCurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(this).find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(this).find('#opWheelRPMDEcimal').val("");
                    }
                });

                //WheelSpeed NS calculation, Lead DI calculation, Overlap- ratio
                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];
                    if (div.children[1].value == "Wheel Speed (Ns) (rpm)") {
                        if (cWheelSpeedVs != "" && cCurrentWheelDia != "") {
                            let result = (parseFloat(cWheelSpeedVs) * 60000) / (Math.PI * parseFloat(cCurrentWheelDia));
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            cWheelSpeddNs = result;
                        }
                        else {
                            div.children[2].value = "";
                            cWheelSpeddNs = "";
                        }
                    }

                    if (div.children[1].value == "Lead-DI OD (mm/rev)") {
                        if (cWheelSpeddNs != "" && cDressFeedrateOD != "") {

                            let result = parseFloat(cDressFeedrateOD) / parseFloat(cWheelSpeddNs);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            cDressLeadOD = result;
                        }
                        else {
                            div.children[2].value = "";
                            cDressLeadOD = ""
                        }
                    }
                    if (div.children[1].value == "Lead-DI Face (mm/rev)") {
                        if (cWheelSpeddNs != "" && cDressFeedrateFace != "") {
                            let result = parseFloat(cDressFeedrateFace) / parseFloat(cWheelSpeddNs);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            cDressLeadFace = result;
                        }
                        else {
                            div.children[2].value = "";
                            cDressLeadFace = "";
                        }
                    }
                    if (div.children[1].value == "Lead-DI Radius (mm/rev)") {
                        if (cWheelSpeddNs != "" && cDressFeedrateRadius != "") {
                            let result = parseFloat(cDressFeedrateRadius) / parseFloat(cWheelSpeddNs);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            cDressLeadODRadius = result;
                        }
                        else {
                            div.children[2].value = "";
                            cDressLeadODRadius = ""
                        }
                    }

                    if (div.children[1].value == "Overlap Ratio - OD") {
                        if (cDressLeadOD != "" && cCutterWidth != "") {
                            let result = parseFloat(cCutterWidth) / parseFloat(cDressLeadOD);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                        }
                        else {
                            div.children[2].value = "";
                        }
                    }
                    if (div.children[1].value == "Overlap Ratio - Face") {
                        if (cDressLeadFace != "" && cCutterWidth != "") {
                            let result = parseFloat(cCutterWidth) / parseFloat(cDressLeadFace);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                        }
                        else {
                            div.children[2].value = "";
                        }
                    }
                    if (div.children[1].value == "Overlap Ratio - Radius") {
                        if (cDressLeadODRadius != "" && cCutterWidth != "") {
                            let result = parseFloat(cCutterWidth) / parseFloat(cDressLeadODRadius);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                        }
                        else {
                            div.children[2].value = "";
                        }
                    }

                    if (div.children[1].value == "Dresser Speed (mps)") {
                        if (cDresserSpeedRPM != "" && cDresserDia != "") {
                            let result = Math.PI * parseFloat(cDresserDia) * parseFloat(cDresserSpeedRPM) / 60000;
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            cDresserSpeedMPS = result;
                        }
                        else {
                            div.children[2].value = "";
                            cDresserSpeedMPS = ""
                        }
                    }
                }
                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];
                    if (div.children[1].value == "Crush Ratio") {
                        if (cDresserSpeedMPS != "" && cWheelSpeedVs != "") {
                            let result = parseFloat(cDresserSpeedMPS) / parseFloat(cWheelSpeedVs);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                        }
                        else {
                            div.children[2].value = "";
                        }
                    }
                }
                return false;
            }

             var previousDDlvalue = "";
            function savePreviousdata(val, im) {
                previousDDlvalue = $(val).val();
            }


            function bindDependancyValue(val, inputModuleTab) {
                debugger;
              

                let result;
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("DataInputModule.aspx/getDependentparameterDetails") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{Independentparametervalue: "' + $(val).val() + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                        result = checkLSLUSLValue(dataitem, $(val).val());
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });

                if (result == "") {
                    //alert("Not affected");
                    let dependencyFlagValue = $(val).closest('div').find('#hdDependancyFlag').val();
                    let param2Id = $(val).closest('div').find('#hfParameterID').val();
                    let param2selectedddlValue = $(val).val();
                    if (dependencyFlagValue == "1") {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: '<%= ResolveUrl("DataInputModule.aspx/getDependencyValue") %>',
                            contentType: "application/json; charset=utf-8",
                            crossDomain: true,
                            data: '{param2Id: "' + param2Id + '", param2selectedddlValue: "' + param2selectedddlValue + '"}',
                            dataType: "json",
                            success: function (response) {
                                var dataitem = response.d;
                                if (dataitem.length != 0) {
                                    param1Id = dataitem[0].ParameterId1;
                                    param1 = dataitem[0].Parameter1;
                                    //General Info
                                    if (inputModuleTab == "GeneralInfo") {
                                        ($('#lstVendors').children()).each(function () {
                                            if ($(this).find('#hfParameterID').val() == param1Id) {
                                                $(this).find("#giddlvalue").empty();
                                                for (let i = 0; i < dataitem.length; i++) {
                                                    if (i == 0) {
                                                        $(this).find("#giddlvalue").append($("<option></option>").html("Select " + param1));
                                                    }
                                                    if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                    } else {
                                                        $(this).find("#giddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                    }
                                                    if ($(this).find("#giddlvalue").prop('selectedIndex') == 0) {
                                                        $(this).find('#hfddlvalue').val("");
                                                    } else {
                                                        $(this).find('#hfddlvalue').val($(this).find("#giddlvalue").val());
                                                    }
                                                }
                                            }
                                        });
                                    }
                                    //Machine Tool
                                    if (inputModuleTab == "MachineTool") {
                                        ($('#ulMachinetool').children()).each(function () {
                                            if ($(this).find('#hfParameterID').val() == param1Id) {
                                                $(this).find("#mtddlvalue").empty();
                                                for (let i = 0; i < dataitem.length; i++) {
                                                    if (i == 0) {
                                                        $(this).find("#mtddlvalue").append($("<option></option>").html("Select " + param1));
                                                    }
                                                    if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                    } else {
                                                        $(this).find("#mtddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                    }
                                                    if ($(this).find("#mtddlvalue").prop('selectedIndex') == 0) {
                                                        $(this).find('#hfddlvalue').val("");
                                                    } else {
                                                        $(this).find('#hfddlvalue').val($(this).find("#mtddlvalue").val());
                                                    }
                                                }
                                            }
                                        });
                                    }
                                    //Consumables
                                    if (inputModuleTab == "Consumable") {
                                        ($('#ulConsumable').children()).each(function () {
                                            if ($(this).find('#hfParameterID').val() == param1Id) {
                                                $(this).find("#cmddlvalue").empty();
                                                for (let i = 0; i < dataitem.length; i++) {
                                                    if (i == 0) {
                                                        $(this).find("#cmddlvalue").append($("<option></option>").html("Select " + param1));
                                                    }
                                                    if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                    } else {
                                                        $(this).find("#cmddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                    }
                                                    if ($(this).find("#cmddlvalue").prop('selectedIndex') == 0) {
                                                        $(this).find('#hfddlvalue').val("");
                                                    } else {
                                                        $(this).find('#hfddlvalue').val($(this).find("#cmddlvalue").val());
                                                    }
                                                }
                                            }
                                        });
                                    }
                                    //Workpiece
                                    if (inputModuleTab == "Workpiece") {
                                        ($('#ulWorkpiece').children()).each(function () {
                                            if ($(this).find('#hfParameterID').val() == param1Id) {
                                                $(this).find("#wpddlvalue").empty();
                                                for (let i = 0; i < dataitem.length; i++) {
                                                    if (i == 0) {
                                                        $(this).find("#wpddlvalue").append($("<option></option>").html("Select " + param1));
                                                    }
                                                    if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                    } else {
                                                        $(this).find("#wpddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                    }
                                                    if ($(this).find("#wpddlvalue").prop('selectedIndex') == 0) {
                                                        $(this).find('#hfddlvalue').val("");
                                                    } else {
                                                        $(this).find('#hfddlvalue').val($(this).find("#wpddlvalue").val());
                                                    }
                                                }
                                            }
                                        });
                                    }
                                    //Operational Grinding
                                    if (inputModuleTab == "OperationalGrinding") {
                                        ($('#ulOperationalParameterGrind').children()).each(function () {
                                            if ($(this).find('#hfParameterID').val() == param1Id) {
                                                $(this).find("#opddlvalue").empty();
                                                for (let i = 0; i < dataitem.length; i++) {
                                                    if (i == 0) {
                                                        $(this).find("#opddlvalue").append($("<option></option>").html("Select " + param1));
                                                    }
                                                    if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                    } else {
                                                        $(this).find("#opddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                    }
                                                    if ($(this).find("#opddlvalue").prop('selectedIndex') == 0) {
                                                        $(this).find('#hfddlvalue').val("");
                                                    } else {
                                                        $(this).find('#hfddlvalue').val($(this).find("#opddlvalue").val());
                                                    }
                                                }
                                            }
                                        });
                                    }
                                    //OperationalDressing
                                    if (inputModuleTab == "OperationalDressing") {
                                        ($('#ulOPDressing').children()).each(function () {
                                            if ($(this).find('#hfParameterID').val() == param1Id) {
                                                $(this).find("#opddlvalue").empty();
                                                for (let i = 0; i < dataitem.length; i++) {
                                                    if (i == 0) {
                                                        $(this).find("#opddlvalue").append($("<option></option>").html("Select " + param1));
                                                    }
                                                    if (dataitem[i].Parameter1Value == null || dataitem[i].Parameter1Value == "") {

                                                    } else {
                                                        $(this).find("#opddlvalue").append($("<option></option>").val(dataitem[i].Parameter1Value).html(dataitem[i].Parameter1Value));
                                                    }
                                                    if ($(this).find("#opddlvalue").prop('selectedIndex') == 0) {
                                                        $(this).find('#hfddlvalue').val("");
                                                    } else {
                                                        $(this).find('#hfddlvalue').val($(this).find("#opddlvalue").val());
                                                    }
                                                }
                                            }
                                        });
                                    }
                                }

                            },
                            error: function (Result) {
                                alert("Error");
                            }
                        });
                    }
                    if (inputModuleTab == "GeneralInfo") {
                        if ($(val).closest('div').find('#giddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#giddlvalue").val());
                        }
                    }
                    if (inputModuleTab == "MachineTool") {
                        if ($(val).closest('div').find('#mtddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#mtddlvalue").val());
                        }
                    }
                    if (inputModuleTab == "Consumable") {
                        if ($(val).closest('div').find('#cmddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#cmddlvalue").val());
                        }
                    }
                    if (inputModuleTab == "Workpiece") {
                        if ($(val).closest('div').find('#wpddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#wpddlvalue").val());
                        }
                    }
                    if (inputModuleTab == "OperationalGrinding") {
                        if ($(val).closest('div').find('#opddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#opddlvalue").val());
                        }
                    }
                    if (inputModuleTab == "OperationalDressing") {
                        if ($(val).closest('div').find('#opddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#opddlvalue").val());
                        }
                    }
                } else {
                    console.log(result);
                    let warningtxt = "";
                    for (let i = 0; i < result.length; i++) {
                        if (warningtxt == "") {

                            warningtxt = "<span>You can't change the value from " + previousDDlvalue + " to " + $(val).val() + ". Because it affect the following prameters LSL and USL value</span> <br/><span>" + (i + 1) + ". " + result[i].parameter + ", its  value is " + result[i].value + " but it should be between " + result[i].lsl + " and " + result[i].usl + "</span>";

                        } else {
                            warningtxt = warningtxt + " <br /><span> " + (i + 1) + ". " + result[i].parameter + ", its value is " + result[i].value + " but it should be between " + result[i].lsl + " and " + result[i].usl + "</span>";

                        }

                    }
                    $(val).val(previousDDlvalue);
                    if (inputModuleTab == "GeneralInfo") {
                        if ($(val).closest('div').find('#giddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#giddlvalue").val());
                        }
                    }
                    if (inputModuleTab == "MachineTool") {
                        if ($(val).closest('div').find('#mtddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#mtddlvalue").val());
                        }
                    }
                    if (inputModuleTab == "Consumable") {
                        if ($(val).closest('div').find('#cmddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#cmddlvalue").val());
                        }
                    }
                    if (inputModuleTab == "Workpiece") {
                        if ($(val).closest('div').find('#wpddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#wpddlvalue").val());
                        }
                    }
                    if (inputModuleTab == "OperationalGrinding") {
                        if ($(val).closest('div').find('#opddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#opddlvalue").val());
                        }
                    }
                    if (inputModuleTab == "OperationalDressing") {
                        if ($(val).closest('div').find('#opddlvalue').prop('selectedIndex') == 0) {
                            $(val).closest('div').find('#hfddlvalue').val("");
                        } else {
                            $(val).closest('div').find('#hfddlvalue').val($(val).closest('div').find("#opddlvalue").val());
                        }
                    }
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("");
                    $("#warningmessageText").append(warningtxt);

                }

                return false;
            }

            function DateTimeFormateCheck(val) {

                var date = val.value.toString("dd/MM/yyyy hh:mm tt");
                if (date == "") {
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Invalid date format, Expecting date in dd/MM/yyyy hh24:mm format.")
                    val.value = "";
                }

            }


            function ClearClick() {


                if ($('[id*=allowEdit]').length > 0) {
                    var readonly;
                    for (var i = 2; i < $('#lstVendors div').length; i++) {

                        var div = $('#lstVendors').children()[i];
                        if (div.children[2].readOnly == true) {
                            readonly = true;
                            break;
                        }
                    }
                    if (readonly) {
                        return true;

                    } else {
                        $('[id*=unsavedDataWarningModal').modal('show');
                        return false;
                    }
                } else {
                    return true;
                }
                //debugger;
                //if ($('[id*=allowEdit]').length > 0) {
                //    $('[id*=unsavedDataWarningModal').modal('show');
                //    return false;
                //} else {
                //    return true;
                //}
            }
            function CancelunsavedDataWarning() {
                $('[id*=unsavedDataWarningModal').modal('hide');
            }

            //allowdeciaml
            $('.allowDecimal').keypress(function (evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var afterdecimalpt = $(this).val().split('.')[1];
                var beforedecimalpt = $(this).val().split('.')[0];
                var pos = evt.target.selectionStart;
                var ptpos = $(this).val().indexOf(".");
                if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                if (charCode == 46 && $(this).val().indexOf('.') != -1) {
                    return false;
                }


                if ($(this).val().length > 6 && $(this).val().indexOf('.') == -1) {
                    if (charCode == 46) {
                        return true;
                    } else {
                        return false;
                    }

                }
                else {
                    if (pos <= ptpos) {
                        if (beforedecimalpt != undefined) {
                            if (beforedecimalpt.length > 6) {

                                return false;
                            }

                        }

                    } else {
                        if (afterdecimalpt != undefined) {
                            if (afterdecimalpt.length > 3) {
                                return false;
                            }
                        }

                    }
                }


                return true;
            });

            //allowDecimalwithoperator
            $('.allowDecimalwithoperator').keypress(function (evt) {
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
            });

            //RemoveImage
            function removeImageFun(imgval) {
                debugger;
                let imageName = $(imgval).closest('span').find('#lblImageName').text();
                let imagePath = "~/" + $(imgval).closest('span').find('#img').attr('src');
                $("#removeImage").val(imageName + "," + imagePath);
                //$('[id*=removeImageNameValue]').val(imageName);
                //$('[id*=removeImagePathValue]').val(imagePath);
                $('[id*=removeImageConfimation]').modal('show');
                return false;
            }
            function removeImageNo() {
                $("#removeImage").val("");
                $('[id*=removeImageConfimation]').modal('hide');
            }


            //For Hardness limit range
            function HardnessUnitChange(el) {
                let hardness = $(el).parent().children()[1].value;
                let limitrange = [];
                limitrange = $(el).val().split(',');
                if (parseFloat(hardness) < parseFloat(limitrange[0]) || parseFloat(hardness) > parseFloat(limitrange[1])) {
                    $(el).parent().children()[1].value = "";
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Value should be between " + limitrange[0] + " and " + limitrange[1]);
                }
                //$(ele).attr('title', param);
                //$(ele).tooltip();
            }
            function wpHardness(el) {
                let hardness = $(el).val();
                let limitrange = [];
                limitrange = $(el).parent().children()[2].value.split(',');
                if (parseFloat(hardness) < parseFloat(limitrange[0]) || parseFloat(hardness) > parseFloat(limitrange[1])) {
                    $(el).parent().children()[1].value = "";
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Value should be between " + limitrange[0] + " and " + limitrange[1]);
                }
            }


            function showLargeImage(image) {
                console.log($(image).attr('src'));
                $('[id*=largeImage]').attr('src', '');                $('[id*=showLargeImage]').modal('show');                $('[id*=largeImage]').attr('src', $(image).attr('src'));
            }


            function showRecommendation(ele, param) {

                var recommandation = "", LSL = "", USL = "", finalrecommandation = "", limit = "";
                if (param == "GeneralInfo") {
                    recommandation = $(ele).closest('div').find('#giRecommandation').val();
                    limit = $(ele).closest('div').find('#giLimitRange').val();
                } else if (param == "MachineTool") {
                    recommandation = $(ele).closest('div').find('#mtRecommandation').val();
                    limit = $(ele).closest('div').find('#mtLimitRange').val();
                } else if (param == "Consumable") {
                    recommandation = $(ele).closest('div').find('#cmRecommandation').val();
                    limit = $(ele).closest('div').find('#cmLimitRange').val();
                } else if (param == "Workpiece") {
                    recommandation = $(ele).closest('div').find('#wpRecommandation').val();
                    limit = $(ele).closest('div').find('#wpLimitRange').val();
                } else if (param == "OperationalGrinding") {
                    recommandation = $(ele).closest('div').find('#opRecommandation').val();
                    limit = $(ele).closest('div').find('#opLimitRange').val();
                } else if (param == "OperationalDressing") {
                    recommandation = $(ele).closest('div').find('#opRecommandation').val();
                    limit = $(ele).closest('div').find('#opLimitRange').val();
                }
                if (limit != "") {
                    LSL = limit.split(';')[0];
                    USL = limit.split(';')[1];
                }

                if (LSL != "" && USL != "" && recommandation != "") {
                    finalrecommandation = recommandation + " ; LSL=" + LSL + ", USL=" + USL;
                } else if (LSL != "" && USL != "" && recommandation == "") {
                    finalrecommandation = "LSL=" + LSL + ", USL=" + USL;;
                } else if (LSL == "" && USL == "" && recommandation != "") {
                    finalrecommandation = recommandation;
                }
                $(ele).attr('title', finalrecommandation);
                $(ele).tooltip({ tooltipClass: 'tooltipclass' ,
                position: {
                    my: "center bottom",
                    at: "center top-10",
                    collision: "none"
                }});
            }

            function ShowREcommandationforQParam(ele, parameter) {

                let param = "", LSL = "", USL = "", finalrecommandation = "", limit = "";
                if (parameter == "TargetLower") {
                    param = $(ele).parent().closest('div').find("#hdnRecomqpTargetLower").val();
                    limit = $(ele).parent().closest('div').find("#hdnLslUslqpTargetLower").val();
                } else if (parameter == "TargetUpper") {
                    param = $(ele).parent().closest('div').find("#hdnRecomqpTargetUppper").val();
                    limit = $(ele).parent().closest('div').find("#hdnLslUslqpTargetUppper").val();
                } else if (parameter == "ActualLower") {
                    param = $(ele).parent().closest('div').find("#hdnRecomqpAchievedLower").val();
                    limit = $(ele).parent().closest('div').find("#hdnLslUslqpAchievedLower").val();
                } else if (parameter == "ActualUpper") {
                    param = $(ele).parent().closest('div').find("#hdnRecomAchievedUppper").val();
                    limit = $(ele).parent().closest('div').find("#hdnLslUslAchievedUppper").val();
                }
                if (limit != "") {
                    LSL = limit.split(';')[0];
                    USL = limit.split(';')[1];
                }

                if (LSL != "" && USL != "" && param != "") {
                    finalrecommandation = param + " ; LSL=" + LSL + ", USL=" + USL;
                } else if (LSL != "" && USL != "" && param == "") {
                    finalrecommandation = "LSL=" + LSL + ", USL=" + USL;;
                } else if (LSL == "" && USL == "" && param != "") {
                    finalrecommandation = param;
                }

                $(ele).attr('title', finalrecommandation);
                $(ele).tooltip({ tooltipClass: 'tooltipclass',
                position: {
                    my: "center bottom",
                    at: "center top-10",
                    collision: "none"
                } });
            }
            function ShowREcommandationforOpeartionalParam(ele, parameter) {

                let param = "", LSL = "", USL = "", finalrecommandation = "", limit = "";
                if (parameter == "FeedRate") {
                    // param = $(ele).parent().children()[3].value;
                    param = $(ele).parent().closest('div').find("#hdnRecomopFeedRate").val();
                    limit = $(ele).parent().closest('div').find("#hdnLslUslopFeedRate").val();


                } else if (parameter == "DOC") {
                    param = $(ele).parent().closest('div').find("#hdnRecomopDOC").val();
                    limit = $(ele).parent().closest('div').find("#hdnLslUslopDOC").val();

                } else if (parameter == "Face") {
                    param = $(ele).parent().closest('div').find("#hdnRecomopFace").val();
                    limit = $(ele).parent().closest('div').find("#hdnLslUslopFace").val();

                }
                else if (parameter == "WorkRPM") {
                    param = $(ele).parent().closest('div').find("#hdnRecomopWorkRPM").val();
                    limit = $(ele).parent().closest('div').find("#hdnLslUslopWorkRPM").val();

                } else if (parameter == "Wheelms") {
                    param = $(ele).parent().closest('div').find("#hdnRecomopWheelms").val();
                    limit = $(ele).parent().closest('div').find("#hdnLslUslopWheelms").val();


                } else if (parameter == "WheelRPM") {
                    param = $(ele).parent().closest('div').find("#hdnRecomopWheelRPM").val();
                    limit = $(ele).parent().closest('div').find("#hdnLslUslopWheelRPM").val();

                } else if (parameter == "Workms") {
                    param = $(ele).parent().closest('div').find("#hdnRecomopWorkms").val();
                    limit = $(ele).parent().closest('div').find("#hdnLslUslopWorkms ").val();

                }
                if (limit != "") {
                    LSL = limit.split(';')[0];
                    USL = limit.split(';')[1];
                }

                if (LSL != "" && USL != "" && param != "") {
                    finalrecommandation = param + " ; LSL=" + LSL + ", USL=" + USL;
                } else if (LSL != "" && USL != "" && param == "") {
                    finalrecommandation = "LSL=" + LSL + ", USL=" + USL;;
                } else if (LSL == "" && USL == "" && param != "") {
                    finalrecommandation = param;
                }

                $(ele).attr('title', finalrecommandation);
                $(ele).tooltip({ tooltipClass: 'tooltipclass' ,
                position: {
                    my: "center bottom",
                    at: "center top-10",
                    collision: "none"
                }});
            }



            function ExceedRangeWarning(data, param) {
                if ($(data).closest('div').find('#hiddenDatatype').val() == 'Decimal' || $(data).closest('div').find('#hiddenDatatype').val() == 'Integer') {
                    if (isNaN($(data).val())) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }
                let value;
                let parameter = [];
                if (param == "GeneralInfo") {
                    if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                        let parameter = $(data).parent().closest('div').find('#gihdParameterName').val();
                        let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        value = $(data).closest('div').find('#giLimitRange').val();
                        parameter = value.split(';');
                        if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                            $(data).val("");
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                            callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                            return;
                        }
                    }
                } else if (param == "MachineTool") {
                    if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                        let parameter = $(data).parent().closest('div').find('#mthdParameterName').val();
                        let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                               callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        value = $(data).closest('div').find('#mtLimitRange').val();
                        parameter = value.split(';');
                        if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                            $(data).val("");
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                            callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                            return;
                        }
                    }
                }
            }
            function ExceedRangeWarningOP(data) {
                debugger;
                let value = $(data).val();
                let parameter = [];
                parameter = $(data).parent().children()[3].value.split(';');
                if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                    $(data).val("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                    return;
                }
            }
            function ExceedRangeWarningForQualityParam(data, param) {
                debugger;
                if (isNaN($(data).val())) {
                    $(data).val("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("This value is not a number");
                    return;
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                }
                let value = $(data).val();
                let parameter = [];

                if (param == "TargetLower") {
                    if ($(data).parent().closest('div').find('#hdTLDependency').val() == "True") {
                        let parameter = $(data).parent().closest('div').find('#hdTLParameter').val();
                        let independentparam = $(data).parent().closest('div').find('#hdTLIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                               callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        parameter = $(data).parent().closest('div').find("#hdnLslUslqpTargetLower").val().split(';');
                        if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                            $(data).val("");
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                            callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                            return;
                        }
                    }
                } else if (param == "TargetUpper") {
                    if ($(data).parent().closest('div').find('#hdTUDependency').val() == "True") {
                        let parameter = $(data).parent().closest('div').find('#hdTUParameter').val();
                        let independentparam = $(data).parent().closest('div').find('#hdTUIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                               callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        parameter = $(data).parent().closest('div').find("#hdnLslUslqpTargetUppper").val().split(';');
                        if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                            $(data).val("");
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                            callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                            return;
                        }
                    }
                } else if (param == "ActualLower") {
                    if ($(data).parent().closest('div').find('#hdALDependency').val() == "True") {
                        let parameter = $(data).parent().closest('div').find('#hdALParameter').val();
                        let independentparam = $(data).parent().closest('div').find('#hdALIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        parameter = $(data).parent().closest('div').find("#hdnLslUslqpAchievedLower").val().split(';');
                        if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                            $(data).val("");
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                            callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                            return;
                        }
                    }
                } else if (param == "ActualUpper") {
                    if ($(data).parent().closest('div').find('#hdAUDependency').val() == "True") {
                        let parameter = $(data).parent().closest('div').find('#hdAUParameter').val();
                        let independentparam = $(data).parent().closest('div').find('#hdAUIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        parameter = $(data).parent().closest('div').find("#hdnLslUslAchievedUppper").val().split(';');
                        if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                            $(data).val("");
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                            callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                            return;
                        }
                    }
                }
            }

            function ExceedRangeWarningForOperationalParam(data, param) {
                debugger;
                let value = $(data).val();
                let parameter = [];

                if (param == "FeedRate") {
                    //parameter = $(data).parent().children()[2].value.split(';');
                    parameter = $(data).closest('span').find('#hdnLslUslopFeedRate').val().split(';');
                } else if (param == "DOC") {
                    if ($(data).closest('div').find('#opDOChiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opDOChiddenObjectType').val() == 'Integer') {
                        if (isNaN($(data).val())) {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("This value is not a number");
                            return;
                        }
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After number space is not allowed");
                            return;
                        }
                    } else {
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After string space is not allowed");
                            return;
                        }
                    }
                    if ($(data).closest('div').find('#opDochdDependency').val() == "True") {
                        let parameter = $(data).closest('div').find('#opDOChdParameter').val();
                        let independentparam = $(data).closest('div').find('#opDochdIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        parameter = $(data).closest('span').find('#hdnLslUslopDOC').val().split(';');
                        if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                            return;
                        }
                    }
                }

                else if (param == "Face") {
                    if ($(data).closest('div').find('#opFacehiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opFacehiddenObjectType').val() == 'Integer') {
                        if (isNaN($(data).val())) {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("This value is not a number");
                            return;
                        }
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After number space is not allowed");
                            return;
                        }
                    } else {
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After string space is not allowed");
                            return;
                        }
                    }
                    if ($(data).closest('div').find('#opFacehdDependency').val() == "True") {
                        let parameter = $(data).closest('div').find('#opFacehdParameter').val();
                        let independentparam = $(data).closest('div').find('#opFacehdIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                               callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        parameter = $(data).closest('span').find('#hdnLslUslopFace').val().split(';');
                        if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                            return;
                        }
                    }
                }
                else if (param == "WorkRPM") {
                    //parameter = $(data).parent().children()[10].value.split(';');
                    parameter = $(data).closest('span').find('#hdnLslUslopWorkRPM').val().split(';');
                } else if (param == "Wheelms") {
                    //parameter = $(data).parent().children()[14].value.split(';');
                    parameter = $(data).closest('span').find('#hdnLslUslopWheelms').val().split(';');

                } else if (param == "Workms") {
                    if ($(data).closest('div').find('#opWorkmshiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opWorkmshiddenObjectType').val() == 'Integer') {
                        if (isNaN($(data).val())) {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("This value is not a number");
                            return;
                        }
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After number space is not allowed");
                            return;
                        }
                    } else {
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After string space is not allowed");
                            return;
                        }
                    }
                    if ($(data).closest('div').find('#hdworkmsDependency').val() == "True") {
                        let parameter = $(data).closest('div').find('#ophdWorkmsParameter').val();
                        let independentparam = $(data).closest('div').find('#hdworkmsIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                               callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        parameter = $(data).closest('span').find('#hdnLslUslopWorkms').val().split(';');
                        if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                            return;
                        }
                    }
                } else if (param == "WheelRPM") {

                    if ($(data).closest('div').find('#opWheelRPMhiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opWheelRPMhiddenObjectType').val() == 'Integer') {
                        if (isNaN($(data).val())) {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("This value is not a number");
                            return;
                        }
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After number space is not allowed");
                            return;
                        }
                    } else {
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After string space is not allowed");
                            return;
                        }
                    }
                    if ($(data).closest('div').find('#hdWheelRPMDependency').val() == "True") {
                        let parameter = $(data).closest('div').find('#hdWheelRPMPaarmeter').val();
                        let independentparam = $(data).closest('div').find('#hdWheelRPMIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                              callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        parameter = $(data).closest('span').find('#hdnLslUslopWheelRPM').val().split(';');
                        if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                            return;
                        }
                    }
                }

                if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                    $(data).val("");
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                    return;
                }
            }




            $('.continue').click(function () {                $('.nextPrevious > .active').next('li').find('a').trigger('click');            });            $('.back').click(function () {                $('.nextPrevious > .active').prev('li').find('a').trigger('click');            });



            $(window).resize(function () {
                var Height = $(window).height() - (235);
                $('.themetoggle').css('height', Height);
            });

            document.getElementById('inputContainer').onscroll = function () {                $('[id*=hdnScrollPos]').val(document.getElementById('inputContainer').scrollTop);            }

            $(".menuData").click(function () {
                $(".menuData").css("background-color", "");
                $(".menuData").css("color", "");

                $(this).css("background-color", "#3c3b54");
                $(this).css("color", "#FFFFFF");

                // console.log($(this).parent().children(0).innerHTML);
                //$(this).addClass("in active");

                menu = $(this).attr('href');


            });

            function allowNumber(evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if ((charCode < 48 || charCode > 57)) {
                    return false;
                }


                return true;
            }
            function allowNumberic(evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if ((charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }

            function allowAlphaNumber(evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if ((charCode < 48 || charCode > 57) && (charCode < 97 || charCode > 122) && (charCode < 65 || charCode > 90)) {
                    return false;
                }
                return true;
            }


            $('.allow200ChComment').keypress(function (evt) {

                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if ($("#giComments").val().length > 199) {
                    return false;
                }
                return true;
            });
            function SaveAsCancelclick() {
                $('[id*=SaveasConfirmationModal]').modal('hide');
                $("[id*=newSDocid]").prop("checked", false);
                $("[id*=incrementSdocSubCategory]").prop("checked", false);
            }
            function CreatenewSDocNoFun() {
                $('[id*=CreateNewSDocID]').modal('hide');
            }
            function saveConfirmNoFun() {
                $('[id*=myConfirmationModal]').modal('hide');
            }
            function SaveAsclick1() {

                if ($("[id*=newSDocid]").prop("checked") == true && $("[id*=incrementSdocSubCategory]").prop("checked") == true && $("[id*=incrementPlunge]").prop("checked") == true) {
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Can't select all options, only one option can select.");
                    $("[id*=newSDocid]").prop("checked", false);
                    $("[id*=incrementSdocSubCategory]").prop("checked", false);
                    $("[id*=incrementPlunge]").prop("checked", false);
                    return false;
                }
                else if ($("[id*=newSDocid]").prop("checked") == false && $("[id*=incrementSdocSubCategory]").prop("checked") == false && $("[id*=incrementPlunge]").prop("checked") == false) {
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("Select any one option.");
                    return false;
                } else {

                    return true;
                }
            }


          

            //function saveAsInputModule() {
            //    ////for (var i = 0; i < $('#lstVendors div').length; i++) {
            //    ////    var div = $('#lstVendors').children()[i];
            //    ////    if (div.children[0].innerText == "SDoc ID") {
            //    ////        // alert(div.children[1].val());
            //    ////        if (div.children[1].value == "") {
            //    ////            $('[id*=myWarningModal]').modal('show');
            //    ////            $("#warningmessageText").text("SDoc ID is required.");
            //    ////            return;
            //    ////        }
            //    ////    }
            //    ////}
            //    //$('[id*=SaveasConfirmationModal]').modal('show');
            //    for (var i = 0; i < $('#lstVendors div').length; i++) {
            //        var div = $('#lstVendors').children()[i];
            //        if (div.children[1].value == "SDoc ID") {
            //            if (div.children[2].value == "") {
            //                $('[id*=myWarningModal]').modal('show');
            //                $("#warningmessageText").text("Load SDoc id.");
            //                return false;
            //            }
            //        }
            //    }
            //    let mandatorylist = "";
            //    mandatorylist = checkMandatoryFields();
            //    if (mandatorylist == "") {
            //        $('[id*=SaveasConfirmationModal]').modal('show');
            //    } else {
            //        $("#warningmessageText").text("");
            //        $('[id*=myWarningModal]').modal('show');
            //        $("#warningmessageText").append(mandatorylist);
            //    }
            //    return false;
            //}

            function saveInputModuleFun() {
                let mandatorylist = "";
                for (var i = 0; i < $('#lstVendors div').length; i++) {
                    var div = $('#lstVendors').children()[i];
                    if (div.children[1].value == "SDoc ID") {
                         let Sdocid = div.children[2].value;
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: '<%= ResolveUrl("DataInputModule.aspx/exitOrcreatenewSdocConfirmation") %>',
                            //url: "DataInputModule.aspx/giObjective",
                            contentType: "application/json; charset=utf-8",
                            crossDomain: true,
                            data: '{SdocId: "' + div.children[2].value + '"}',
                            dataType: "json",
                            success: function (response) {
                                var dataitem = response.d;
                                if (dataitem == "Exists") {
                                    //$('[id*=myConfirmationModal]').modal('show');
                                    //$("#confirmationmessageText").text("Are you sure, you want to save this data?");
                                     let sdoctype = Sdocid.split('_')[0].replace('SDoc','');
                                    var readonly;
                                    if ($('[id*=allowEdit]').length > 0) {
                                        for (var i = 2; i < $('#lstVendors div').length; i++) {
                                            var div = $('#lstVendors').children()[i];
                                            if (div.children[2].readOnly == true) {
                                                readonly = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (readonly == true) {
                                        $('[id*=myWarningModal]').modal('show');
                                        $("#warningmessageText").text("Page is not in edit mode.");
                                    } else {
                                        mandatorylist = checkMandatoryFields();
                                        if (mandatorylist == "") {

                                            if ($("#cbTemplate").is(':Checked') && sdoctype!="000000") {
                                                $('[id*=myWarningModal]').modal('show');
                                                $("#warningmessageText").text("When Template is checked cannot save the loaded Sdocid. You can Save As the Sdocid or you can uncheck the template and save the loaded Sdocid.");
                                            } else {
                                                $('[id*=myConfirmationModal]').modal('show');
                                                $("#confirmationmessageText").text("Are you sure, you want to save this data?");
                                            }
                                        } else {
                                            $("#warningmessageText").text("");
                                            $('[id*=myWarningModal]').modal('show');
                                            $("#warningmessageText").append(mandatorylist);
                                        }

                                    }


                                } else {
                                    mandatorylist = checkMandatoryFields();
                                    if (mandatorylist == "") {
                                        $('[id*=CreateNewSDocID]').modal('show');
                                        $("#CreateNewSDocmessageText").text("Are you sure, you want to create new SDoc Id?");
                                    } else {
                                        $("#warningmessageText").text("");
                                        $('[id*=myWarningModal]').modal('show');
                                        $("#warningmessageText").append(mandatorylist);
                                    }

                                }
                            },
                            error: function (Result) {
                                alert("Error");
                            }
                        });


                    }
                }


                return false;
                //for (var i = 0; i < $('#lstVendors div').length; i++) {
                //    var div = $('#lstVendors').children()[i];
                //    if (div.children[0].innerText == "SDoc ID") {
                //        // alert(div.children[1].val());
                //        if (div.children[1].value == "") {
                //            $('[id*=myConfirmationModal]').modal('show');
                //            $("#confirmationmessageText").text("Are you sure, you want to create new SDoc Id?");
                //            return false;
                //        }
                //    }
                //}
                //$('[id*=myConfirmationModal]').modal('show');
                //$("#confirmationmessageText").text("Are you sure, you want to save this data?");
                //return false;
            }
            function viewInputModule() {
                if ($("#txtViewSdocid").val() == "") {
                    $('[id*=myWarningModal]').modal('show');
                    $("#warningmessageText").text("SDoc ID is required.");
                    return false;
                }
                else {
                    return true;
                }
            }
            function openWarningModal(msg) {
                $('[id*=myWarningModal]').modal('show');
                $("#warningmessageText").text(msg);
            };

            function openErrorModal(msg) {
                $('[id*=myErrorModal]').modal('show');
                $("#errormessageText").text(msg);
            }
            //Calculations


            var WheelSpeedVs = "", CurrentWheelDia = "";
            var WheelDia = "";
            var WheelCuttingSpeed = "";
            var InitialComponentDia = "";
            var InputComponentDiaOD = "";
            var WorkSpeedNw = "";
            var WorkSpeddVw = "";
            var WheelSpeddNs = "";
            var CutterWidth = "", DresserDia = "";
            var DresserSpeedRPM = "",  DresserSpeedMPS="";
            var DressLeadOD = "", DressLeadFace = "", DressLeadODRadius = "";
            var DressFeedrateOD = "", DressFeedrateFace = "", DressFeedrateRadius = "";
            var OverlapratioOD = "", OverlapratioFace = "", OverlapratioRadius = "";


            function OPDressingParametersCalculations(data, inputModule) {
                debugger;

                if (inputModule == "Consumables") {

                    if ($(data).closest('div').find('#cmhiddenDateType').val() == 'Decimal' || $(data).closest('div').find('#cmhiddenDateType').val() == 'Integer') {
                        if (isNaN($(data).val())) {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("This value is not a number");
                            return;
                        }
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After number space is not allowed");
                            return;
                        }
                    } else {
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After string space is not allowed");
                            return;
                        }
                    }

                    if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                        let parameter = $(data).parent().closest('div').find('#cmhdParameterName').val();
                        let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                              callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        let dataitem = [];
                        dataitem = $(data).parent().closest('div').find('#cmLimitRange').val().split(';');
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + dataitem[0] + " and " + dataitem[1]);
                            return;
                        }
                    }
                }

                if (inputModule == "OPDressingParam") {

                    if ($(data).closest('div').find('#ophiddenDateType').val() == 'Decimal' || $(data).closest('div').find('#ophiddenDateType').val() == 'Integer') {
                        if (isNaN($(data).val())) {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("This value is not a number");
                            return;
                        }
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After number space is not allowed");
                            return;
                        }
                    } else {
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After string space is not allowed");
                            return;
                        }
                    }

                    if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                        let parameter = $(data).parent().closest('div').find('#ophdParameterName').val();
                        let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        let dataitem = [];
                        dataitem = $(data).parent().closest('div').find('#opLimitRange').val().split(';');
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + dataitem[0] + " and " + dataitem[1]);
                            return;
                        }
                    }
                }

                if (inputModule == "OPGrindingParam") {

                    if ($(data).closest('div').find('#ophiddenDateType').val() == 'Decimal' || $(data).closest('div').find('#ophiddenDateType').val() == 'Integer') {
                        if (isNaN($(data).val())) {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("This value is not a number");
                            return;
                        }
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After number space is not allowed");
                            return;
                        }
                    } else {
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After string space is not allowed");
                            return;
                        }
                    }

                    let value = $(data).val();

                    if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                        let parameter = $(data).parent().closest('div').find('#ophdParameterName').val();
                        let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                               callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {

                        let parameter = [];
                        //parameter = $(data).parent().children()[3].value.split(';');
                        parameter = $(data).parent().closest('div').find('#opLimitRange').val().split(';');
                        if (parseFloat($(data).val()) < parseFloat(parameter[0]) || parseFloat($(data).val()) > parseFloat(parameter[1])) {
                            $(data).val("");
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + parameter[0] + " and " + parameter[1]);
                            callWarningFuncwithLSLUSL(parameter[0], parameter[1]);
                            return;
                        }
                    }
                }


                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];
                    if (div.children[1].value == "Dressing Feed Rate- OD (mm/min)") {
                        DressFeedrateOD = div.children[2].value;
                    }
                    if (div.children[1].value == "Dressing Feed Rate- Face (mm/min)") {
                        DressFeedrateFace = div.children[2].value;
                    }
                    if (div.children[1].value == "Dressing Feed Rate- Radius (mm/min)") {
                        DressFeedrateRadius = div.children[2].value;
                    }
                    if (div.children[1].value == "Wheel Speed (Ns) (rpm)") {
                        WheelSpeddNs = div.children[2].value;
                    }
                    if (div.children[1].value == "Overlap Ratio - OD") {
                        OverlapratioOD = div.children[2].value;
                    }
                    if (div.children[1].value == "Overlap Ratio - Face") {
                        OverlapratioFace = div.children[2].value;
                    }
                    if (div.children[1].value == "Overlap Ratio - Radius") {
                        OverlapratioRadius = div.children[2].value;
                    }
                    if (div.children[1].value == "Wheel Speed (Vs) (m/s)") {
                        WheelSpeedVs = div.children[2].value;
                    }
                    if (div.children[1].value == "Work Speed (Nw) (rpm)") {
                        WorkSpeedNw = div.children[2].value;
                    }
                    if (div.children[1].value == "Dresser Speed (rpm)") {
                        DresserSpeedRPM = div.children[2].value;
                    }
                }

                for (var i = 0; i < $('#ulConsumable').children().length; i++) {
                    var div = $('#ulConsumable').children()[i];
                    if (div.children[1].value == "Max. Wheel Dia (mm)") {
                        WheelDia = div.children[2].value;

                    }
                    if (div.children[1].value == "Maximum Cutting Speed (m/s)") {
                        WheelCuttingSpeed = div.children[2].value;
                    }
                    if (div.children[1].value == "Cutter Width (mm)") {
                        CutterWidth = div.children[2].value;
                    }
                    if (div.children[1].value == "Dresser Dia") {
                        DresserDia = div.children[2].value;
                    }
                }

                for (var i = 1; i < $('#ulOperationalParameterGrind').children().length; i++) {
                    var div = $('#ulOperationalParameterGrind').children()[i];
                    if (div.children[1].value == "Current Wheel Diameter (mm)") {
                        CurrentWheelDia = div.children[2].value;

                    }
                }

                for (var i = 0; i < $('#ulWorkpiece').children().length; i++) {
                    debugger;
                    let div = $('#ulWorkpiece').children()[i];
                    if (div.children[1].value == "Initial Component Dia (mm)") {
                        InitialComponentDia = div.children[2].value;
                        // break;
                    }
                    if (div.children[1].value == "Input Component Dia-OD (mm)") {
                        InputComponentDiaOD = div.children[2].value;
                        // break;
                    }
                }

                debugger;

                ($('#ulOperationalParameter').children()).each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 1") {
                        R1Wheelms = $(this).find('#opWheelmsDEcimal').val();

                    }
                    if ($(this).find('#opItem').text() == "Roughing 2") {
                        R2Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Roughing 3") {
                        R3Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Roughing 4") {
                        R4Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Roughing 5") {
                        R5Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }

                    if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                        S1Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                        S2Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                        S3Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                        S4Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                        S5Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }

                    if ($(this).find('#opItem').text() == "Finishing 1") {
                        F1Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Finishing 2") {
                        F2Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Finishing 3") {
                        F3Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Finishing 4") {
                        F4Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Finishing 5") {
                        F5Wheelms = $(this).find('#opWheelmsDEcimal').val();
                    }
                });
                if (CurrentWheelDia != "" && R1Wheelms != "") {
                    let result = (60 * parseFloat(R1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 1") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 1") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && R2Wheelms != "") {
                    let result = (60 * parseFloat(R2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 2") {
                            $(this).find('#opWheelRPMDEcimal').val(result);

                        }
                    });

                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 2") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && R3Wheelms != "") {
                    let result = (60 * parseFloat(R3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 3") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 3") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && R4Wheelms != "") {
                    let result = (60 * parseFloat(R4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 4") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 4") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && R5Wheelms != "") {
                    let result = (60 * parseFloat(R5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 5") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 5") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && S1Wheelms != "") {
                    let result = (60 * parseFloat(S1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && S2Wheelms != "") {
                    let result = (60 * parseFloat(S2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && S3Wheelms != "") {
                    let result = (60 * parseFloat(S3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && S4Wheelms != "") {
                    let result = (60 * parseFloat(S4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && S5Wheelms != "") {
                    let result = (60 * parseFloat(S5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && F1Wheelms != "") {
                    let result = (60 * parseFloat(F1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 1") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 1") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && F2Wheelms != "") {
                    let result = (60 * parseFloat(F2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 2") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 2") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && F3Wheelms != "") {
                    let result = (60 * parseFloat(F3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 3") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 3") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && F4Wheelms != "") {
                    let result = (60 * parseFloat(F4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 4") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 4") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }
                if (CurrentWheelDia != "" && F5Wheelms != "") {
                    let result = (60 * parseFloat(F5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 5") {
                            $(this).find('#opWheelRPMDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 5") {
                            $(this).find('#opWheelRPMDEcimal').val("");
                        }
                    });
                }


                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];
                    if (div.children[1].value == "Wheel Speed (Ns) (rpm)") {
                        if (WheelSpeedVs != "" && CurrentWheelDia != "") {
                            let result = (parseFloat(WheelSpeedVs) * 60000) / (Math.PI * parseFloat(CurrentWheelDia));
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            //DressLeadODRadius = result;
                            WheelSpeddNs = result;
                        }
                        else {
                            div.children[2].value = "";
                            WheelSpeddNs = "";
                            //DressLeadODRadius = ""
                        }
                    }
                }
                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];
                    if (div.children[1].value == "Work Speed (Vw) (m/s)") {
                        if (WorkSpeedNw != "" && InitialComponentDia != "") {
                            let result = (Math.PI * parseFloat(WorkSpeedNw) * parseFloat(InitialComponentDia)) / 60;
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            //DressLeadODRadius = result;
                        }
                        else {
                            div.children[2].value = "";
                            //DressLeadODRadius = ""
                        }
                    }
                }

                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];

                    if (div.children[1].value == "Lead-DI OD (mm/rev)") {
                        if (WheelSpeddNs != "" && DressFeedrateOD != "") {
                            let result = parseFloat(DressFeedrateOD) / parseFloat(WheelSpeddNs);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            DressLeadOD = result;
                        }
                        else {
                            div.children[2].value = "";
                            DressLeadOD = ""
                        }
                    }
                    if (div.children[1].value == "Lead-DI Face (mm/rev)") {
                        if (WheelSpeddNs != "" && DressFeedrateFace != "") {
                            let result = parseFloat(DressFeedrateFace) / parseFloat(WheelSpeddNs);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            DressLeadFace = result;
                        }
                        else {
                            div.children[2].value = "";
                            DressLeadFace = "";
                        }
                    }
                    if (div.children[1].value == "Lead-DI Radius (mm/rev)") {
                        if (WheelSpeddNs != "" && DressFeedrateRadius != "") {
                            let result = parseFloat(DressFeedrateRadius) / parseFloat(WheelSpeddNs);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            DressLeadODRadius = result;
                        }
                        else {
                            div.children[2].value = "";
                            DressLeadODRadius = ""
                        }
                    }


                }
                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];
                    if (div.children[1].value == "Overlap Ratio - OD") {
                        if (DressLeadOD != "" && CutterWidth != "") {
                            let result = parseFloat(CutterWidth) / parseFloat(DressLeadOD);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                        }
                        else {
                            div.children[2].value = "";
                        }
                    }
                    if (div.children[1].value == "Overlap Ratio - Face") {
                        if (DressLeadFace != "" && CutterWidth != "") {
                            let result = parseFloat(CutterWidth) / parseFloat(DressLeadFace);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                        }
                        else {
                            div.children[2].value = "";
                        }
                    }
                    if (div.children[1].value == "Overlap Ratio - Radius") {
                        if (DressLeadODRadius != "" && CutterWidth != "") {
                            let result = parseFloat(CutterWidth) / parseFloat(DressLeadODRadius);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                        }
                        else {
                            div.children[2].value = "";
                        }
                    }
                }

                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];
                    if (div.children[1].value == "Dresser Speed (mps)") {
                        if (DresserSpeedRPM != "" && DresserDia != "") {
                            let result = Math.PI * parseFloat(DresserDia) * parseFloat(DresserSpeedRPM) / 60000;
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            DresserSpeedMPS = result;
                        }
                        else {
                            div.children[2].value = "";
                            DresserSpeedMPS = ""
                        }
                    }
                }

                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];
                    if (div.children[1].value == "Crush Ratio") {
                        if (DresserSpeedMPS != "" && WheelSpeedVs != "") {
                            let result = parseFloat(DresserSpeedMPS) / parseFloat(WheelSpeedVs);
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                        }
                        else {
                            div.children[2].value = "";
                        }
                    }
                }


            }




            //Work Speed Vw
            var R1Workrpm, S1Workrpm, F1Workrpm;
            var R2Workrpm, S2Workrpm, F2Workrpm;
            var R3Workrpm, S3Workrpm, F3Workrpm;
            var R4Workrpm, S4Workrpm, F4Workrpm;
            var R5Workrpm, S5Workrpm, F5Workrpm;
            var R1FeedRate, R2FeedRate, R3FeedRate, R4FeedRate, R5FeedRate;
            var R1Wheelms, R2Wheelms, R3Wheelms, R4Wheelms, R5Wheelms;
            var S1Wheelms, S2Wheelms, S3Wheelms, S4Wheelms, S5Wheelms;
            var F1Wheelms, F2Wheelms, F3Wheelms, F4Wheelms, F5Wheelms;

            function OPCalculation(data, inModule) {
                debugger;
                if (inModule == "FeedRate") {
                    if ($(data).closest('div').find('#opFeedratehiddenObjecttype').val() == 'Decimal' || $(data).closest('div').find('#opFeedratehiddenObjecttype').val() == 'Integer') {
                        if (isNaN($(data).val())) {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("This value is not a number");
                            return;
                        }
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After number space is not allowed");
                            return;
                        }
                    } else {
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After string space is not allowed");
                            return;
                        }
                    }
                    if ($(data).closest('div').find('#hdFeedrateDependency').val() == "True") {
                        let parameter = $(data).closest('div').find('#hdFeedrateParameter').val();
                        let independentparam = $(data).closest('div').find('#hdFeedrateIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                               callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {

                        let lslusl = [];
                        lslusl = $(data).closest('div').find('#hdnLslUslopFeedRate').val().split(";");
                        if (parseFloat($(data).val()) < parseFloat(lslusl[0]) || parseFloat($(data).val()) > parseFloat(lslusl[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(lslusl[0], lslusl[1]);
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + lslusl[0] + " and " + lslusl[1]);
                            return;
                        }
                    }

                }

                if (inModule == "WorkRPM") {
                    if ($(data).closest('div').find('#opWorkRPMhiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opWorkRPMhiddenObjectType').val() == 'Integer') {
                        if (isNaN($(data).val())) {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("This value is not a number");
                            return;
                        }
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After number space is not allowed");
                            return;
                        }
                    } else {
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After string space is not allowed");
                            return;
                        }
                    }
                    if ($(data).closest('div').find('#ophdWokRPMDependency').val() == "True") {
                        let parameter = $(data).closest('div').find('#ophdWorkRPMParameter').val();
                        let independentparam = $(data).closest('div').find('#ophdWorkRPMIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {

                        let lslusl = [];
                        lslusl = $(data).closest('div').find('#hdnLslUslopWorkRPM').val().split(";");
                        if (parseFloat($(data).val()) < parseFloat(lslusl[0]) || parseFloat($(data).val()) > parseFloat(lslusl[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(lslusl[0], lslusl[1]);
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + lslusl[0] + " and " + lslusl[1]);
                            return;
                        }
                    }

                }

                if (inModule == "Wheelms") {
                    if ($(data).closest('div').find('#opWheelmshiddenObjectType').val() == 'Decimal' || $(data).closest('div').find('#opWheelmshiddenObjectType').val() == 'Integer') {
                        if (isNaN($(data).val())) {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("This value is not a number");
                            return;
                        }
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After number space is not allowed");
                            return;
                        }
                    } else {
                        if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                            $(data).val("");
                            $('[id*=myWarningModal]').modal('show');
                            $("#warningmessageText").text("Before and After string space is not allowed");
                            return;
                        }
                    }
                    if ($(data).closest('div').find('#ophdWheelmsDependency').val() == "True") {
                        let parameter = $(data).closest('div').find('#ophdWheelmsParameter').val();
                        let independentparam = $(data).closest('div').find('#ophdWheelmsIndependentParameter').val();
                        let LSLUSL = parameterDependencyFun(parameter, independentparam);
                        if (LSLUSL != "") {
                            let dataitem = [];
                            dataitem = LSLUSL.split(',');
                            if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFunc(dataitem[0],dataitem[1]);
                                return;
                            }
                            if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                                $(data).val("");
                                callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                                return;
                            }
                        }

                    } else {
                        let lslusl = [];
                        lslusl = $(data).closest('div').find('#hdnLslUslopWheelms').val().split(";");
                        if (parseFloat($(data).val()) < parseFloat(lslusl[0]) || parseFloat($(data).val()) > parseFloat(lslusl[1])) {
                            $(data).val("");
                            //$('[id*=myWarningModal]').modal('show');
                            //$("#warningmessageText").text("Value should be between " + lslusl[0] + " and " + lslusl[1]);
                            callWarningFuncwithLSLUSL(lslusl[0], lslusl[1]);
                            return;
                        }
                    }
                }
                for (var i = 1; i < $('#ulOperationalParameterGrind').children().length; i++) {
                    var div = $('#ulOperationalParameterGrind').children()[i];
                    if (div.children[1].value == "Current Wheel Diameter (mm)") {
                        CurrentWheelDia = div.children[2].value;
                    }
                }

                for (var i = 0; i < $('#ulWorkpiece').children().length; i++) {
                    debugger;
                    let div = $('#ulWorkpiece').children()[i];
                    if (div.children[1].value == "Initial Component Dia (mm)") {
                        InitialComponentDia = div.children[2].value;
                        // break;
                    }
                    if (div.children[1].value == "Input Component Dia-OD (mm)") {
                        InputComponentDiaOD = div.children[2].value;
                        // break;
                    }
                }

                if ($(data).closest('div').find('#opItem').text() == "Roughing 1") {
                    R1Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && R1Wheelms != "") {
                        let result = (60 * parseFloat(R1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Roughing 2") {
                    R2Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && R2Wheelms != "") {
                        let result = (60 * parseFloat(R2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Roughing 3") {
                    R3Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && R3Wheelms != "") {
                        let result = (60 * parseFloat(R3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Roughing 4") {
                    R4Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && R4Wheelms != "") {
                        let result = (60 * parseFloat(R4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Roughing 5") {
                    R5Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && R5Wheelms != "") {
                        let result = (60 * parseFloat(R5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 1") {
                    S1Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && S1Wheelms != "") {
                        let result = (60 * parseFloat(S1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 2") {
                    S2Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && S2Wheelms != "") {
                        let result = (60 * parseFloat(S2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 3") {
                    S3Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && S3Wheelms != "") {
                        let result = (60 * parseFloat(S3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 4") {
                    S4Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && S4Wheelms != "") {
                        let result = (60 * parseFloat(S4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 5") {
                    S5Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && S5Wheelms != "") {
                        let result = (60 * parseFloat(S5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Finishing 1") {
                    F1Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && F1Wheelms != "") {
                        let result = (60 * parseFloat(F1Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Finishing 2") {
                    F2Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && F2Wheelms != "") {
                        let result = (60 * parseFloat(F2Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Finishing 3") {
                    F3Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && F3Wheelms != "") {
                        let result = (60 * parseFloat(F3Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Finishing 4") {
                    F4Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && F4Wheelms != "") {
                        let result = (60 * parseFloat(F4Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Finishing 5") {
                    F5Wheelms = $(data).closest('div').find('#opWheelmsDEcimal').val();
                    if (CurrentWheelDia != "" && F5Wheelms != "") {
                        let result = (60 * parseFloat(F5Wheelms)) / (Math.PI * (parseFloat(CurrentWheelDia) * 0.001));
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWheelRPMDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWheelRPMDEcimal').val("");
                    }
                }





                if ($(data).closest('div').find('#opItem').text() == "Roughing 1") {
                    R1Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && R1Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R1Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    } else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }

                    R1FeedRate = $(data).closest('div').find('#opFeedRatedecimal').val();
                    //if (R1FeedRate != "" && R1Workrpm != "") {
                    //    let result = (1000 * parseFloat(R1FeedRate)) / parseFloat(R1Workrpm);
                    //    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    //    $(data).closest('div').find('#opDOCDecimal').val(result);
                    //} else {
                    //    $(data).closest('div').find('#opDOCDecimal').val("");
                    //}
                }
                if ($(data).closest('div').find('#opItem').text() == "Roughing 2") {
                    R2Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();

                    if (InputComponentDiaOD != "" && R2Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R2Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }

                    R2FeedRate = $(data).closest('div').find('#opFeedRatedecimal').val();
                    //if (R2FeedRate != "" && R2Workrpm != "") {
                    //    let result = (1000 * parseFloat(R2FeedRate)) / parseFloat(R2Workrpm);
                    //    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    //    $(data).closest('div').find('#opDOCDecimal').val(result);
                    //} else {
                    //    $(data).closest('div').find('#opDOCDecimal').val("");
                    //}
                }
                if ($(data).closest('div').find('#opItem').text() == "Roughing 3") {
                    R3Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && R3Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R3Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }

                    R3FeedRate = $(data).closest('div').find('#opFeedRatedecimal').val();
                    //if (R3FeedRate != "" && R3Workrpm != "") {
                    //    let result = (1000 * parseFloat(R3FeedRate)) / parseFloat(R3Workrpm);
                    //    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    //    $(data).closest('div').find('#opDOCDecimal').val(result);
                    //} else {
                    //    $(data).closest('div').find('#opDOCDecimal').val("");
                    //}
                }
                if ($(data).closest('div').find('#opItem').text() == "Roughing 4") {
                    R4Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && R4Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R4Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }

                    R4FeedRate = $(data).closest('div').find('#opFeedRatedecimal').val();
                    //if (R4FeedRate != "" && R4Workrpm != "") {
                    //    let result = (1000 * parseFloat(R4FeedRate)) / parseFloat(R4Workrpm);
                    //    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    //    $(data).closest('div').find('#opDOCDecimal').val(result);
                    //} else {
                    //    $(data).closest('div').find('#opDOCDecimal').val("");
                    //}
                }
                if ($(data).closest('div').find('#opItem').text() == "Roughing 5") {
                    R5Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && R5Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R5Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }

                    R5FeedRate = $(data).closest('div').find('#opFeedRatedecimal').val();
                    //if (R5FeedRate != "" && R5Workrpm != "") {
                    //    let result = (1000 * parseFloat(R5FeedRate)) / parseFloat(R5Workrpm);
                    //    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    //    $(data).closest('div').find('#opDOCDecimal').val(result);
                    //} else {
                    //    $(data).closest('div').find('#opDOCDecimal').val("");
                    //}
                }
                if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 1") {
                    S1Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && S1Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S1Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 2") {
                    S2Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && S2Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S2Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 3") {
                    S3Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && S3Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S3Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 4") {
                    S4Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && S4Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S4Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Semi Finishing 5") {
                    S5Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && S5Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S5Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Finishing 1") {
                    F1Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && F1Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F1Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Finishing 2") {
                    F2Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();

                    if (InputComponentDiaOD != "" && F2Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F2Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Finishing 3") {
                    F3Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && F3Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F3Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Finishing 4") {
                    F4Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && F4Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F4Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }
                }
                if ($(data).closest('div').find('#opItem').text() == "Finishing 5") {
                    F5Workrpm = $(data).closest('div').find('#opWorkRPMDecimal').val();
                    if (InputComponentDiaOD != "" && F5Workrpm != "") {
                        let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F5Workrpm)) / 60000;
                        result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                        $(data).closest('div').find('#opWorkmsDEcimal').val(result);
                    }
                    else {
                        $(data).closest('div').find('#opWorkmsDEcimal').val("");
                    }
                }
            }




            function CalculateVw(data, ipModule) {
                debugger;
                let value = $(data).val();
                if ($(data).closest('div').find('#wphiddebDatatype').val() == 'Decimal' || $(data).closest('div').find('#wphiddebDatatype').val() == 'Integer') {
                    if (isNaN(value)) {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("This value is not a number");
                        return;
                    }
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("");
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After number space is not allowed");
                        return;
                    }
                } else {
                    if ($(data).val().indexOf(' ') == 0 || $(data).val()[$(data).val().length - 1] == ' ') {
                        $(data).val("")
                        $('[id*=myWarningModal]').modal('show');
                        $("#warningmessageText").text("Before and After string space is not allowed");
                        return;
                    }
                }

                if ($(data).parent().closest('div').find('#hdDependency').val() == "True") {
                    let parameter = $(data).parent().closest('div').find('#wphdParameterName').val();
                    let independentparam = $(data).parent().closest('div').find('#hdIndependentParameter').val();
                    let LSLUSL = parameterDependencyFun(parameter, independentparam);
                    if (LSLUSL != "") {
                        let dataitem = [];
                        dataitem = LSLUSL.split(',');
                        if (parseFloat(dataitem[0]) > parseFloat(dataitem[1])) {
                            $(data).val("");
                           callWarningFunc(dataitem[0],dataitem[1]);
                            return;
                        }
                        if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                            $(data).val("");
                            callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                            return;
                        }
                    }

                } else {
                    let dataitem = [];
                    dataitem = $(data).parent().closest('div').find('#wpLimitRange').val().split(';');
                    if (parseFloat($(data).val()) < parseFloat(dataitem[0]) || parseFloat($(data).val()) > parseFloat(dataitem[1])) {
                        $(data).val("");
                         callWarningFuncwithLSLUSL(dataitem[0], dataitem[1]);
                        //$('[id*=myWarningModal]').modal('show');
                        //$("#warningmessageText").text("Value should be between " + dataitem[0] + " and " + dataitem[1]);
                        return;
                    }
                }

                ($('#ulOperationalParameter').children()).each(function () {
                    if ($(this).find('#opItem').text() == "Roughing 1") {
                        R1Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Roughing 2") {
                        R2Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Roughing 3") {
                        R3Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Roughing 4") {
                        R4Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Roughing 5") {
                        R5Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }

                    if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                        S1Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                        S2Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                        S3Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                        S4Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                        S5Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }

                    if ($(this).find('#opItem').text() == "Finishing 1") {
                        F1Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Finishing 2") {
                        F2Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Finishing 3") {
                        F3Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Finishing 4") {
                        F4Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                    if ($(this).find('#opItem').text() == "Finishing 5") {
                        F5Workrpm = $(this).find('#opWorkRPMDecimal').val();
                    }
                });

                debugger;
                //for (var i = 0; i < $('#ulWorkpiece div').length; i++) {
                //    let div = $('#ulWorkpiece').children()[i];

                //    if (div.children[1].value == "Initial Component Dia (mm)") {
                //        InitialComponentDia = div.children[2].value;
                //       // break;
                //    }
                //    if (div.children[1].value == "Input Component Dia-OD (mm)") {
                //        InputComponentDiaOD = div.children[2].value;
                //       // break;
                //    }
                //}
                for (var i = 0; i < $('#ulWorkpiece').children().length; i++) {
                    debugger;
                    let div = $('#ulWorkpiece').children()[i];
                    if (div.children[1].value == "Initial Component Dia (mm)") {
                        InitialComponentDia = div.children[2].value;
                        // break;
                    }
                    if (div.children[1].value == "Input Component Dia-OD (mm)") {
                        InputComponentDiaOD = div.children[2].value;
                        // break;
                    }
                }

                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];
                    if (div.children[1].value == "Work Speed (Nw) (rpm)") {
                        WorkSpeedNw = div.children[2].value;
                    }
                }

                for (var i = 1; i < $('#ulOPDressing').children().length; i++) {
                    var div = $('#ulOPDressing').children()[i];
                    if (div.children[1].value == "Work Speed (Vw) (m/s)") {
                        if (WorkSpeedNw != "" && InitialComponentDia != "") {
                            let result = (Math.PI * parseFloat(WorkSpeedNw) * parseFloat(InitialComponentDia)) / 60000;
                            result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                            div.children[2].value = result;
                            //DressLeadODRadius = result;
                        }
                        else {
                            div.children[2].value = "";
                            //DressLeadODRadius = ""
                        }
                    }
                }



                if (InputComponentDiaOD != "" && R1Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R1Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 1") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 1") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && R2Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R2Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 2") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 2") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && R3Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R3Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 3") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 3") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && R4Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R4Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 4") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 4") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && R5Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(R5Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 5") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Roughing 5") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && S1Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S1Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 1") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && S2Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S2Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 2") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && S3Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S3Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 3") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && S4Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S4Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 4") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && S5Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(S5Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Semi Finishing 5") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && F1Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F1Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 1") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 1") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && F2Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F2Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 2") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 2") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && F3Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F3Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 3") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 3") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && F4Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F4Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 4") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 4") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
                if (InputComponentDiaOD != "" && F5Workrpm != "") {
                    let result = (Math.PI * parseFloat(InputComponentDiaOD) * parseFloat(F5Workrpm)) / 60000;
                    result = (isNaN(result)) ? '' : parseFloat(result).toFixed(4);
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 5") {
                            $(this).find('#opWorkmsDEcimal').val(result);
                        }
                    });
                }
                else {
                    $('#ulOperationalParameter').children('.fieldDiv').each(function () {
                        if ($(this).find('#opItem').text() == "Finishing 5") {
                            $(this).find('#opWorkmsDEcimal').val("");
                        }
                    });
                }
            }

            function sendFile(file, id) {

                var formData = new FormData();
                formData.append('file', $('#' + id)[0].files[0]);
                name = $('#' + id)[0].parentElement.children[1].value;
                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("DataInputModule.aspx/AddIamgeNameToSessionImageDetails") %>',
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{imagename: "' + name + '"}',
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });
                $.ajax({
                    async: false,
                    type: 'post',
                    url: '<%= ResolveUrl("fileUploader.ashx") %>',
                    //url: 'fileUploader.ashx'
                    data: formData,
                    success: function (status) {
                        if (status != 'error') {
                            var my_path = "UploadImages/" + status;
                            $("#imgPhoto").attr("src", my_path);
                            console.log("Enter sendfile");
                        }
                    },
                    processData: false,
                    contentType: false,
                    error: function () {
                        alert("Whoops something went wrong!");
                    }
                });
            }
            var _URL = window.URL || window.webkitURL;
            function file(e) {
                var file, img;
                debugger;


                var id = e.getAttribute('id');
                if ((file = e.files[0])) {
                    img = new Image();
                    img.onload = function () {
                        sendFile(file, id);
                    };
                    img.onerror = function () {
                        alert("Not a valid file:" + file.type);
                    };
                    img.src = _URL.createObjectURL(file);
                }
            }
            function showpop(msg, title) {
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
            function imageUpadateDone() {

                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("DataInputModule.aspx/SetSessionImageDetailsNull") %>',
                    //url: "DataInputModule.aspx/giObjective",
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    dataType: "json",
                    success: function (response) {
                        var dataitem = response.d;
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });
                var i = 0;
                for (i = 0; i < $('input[type="file"]').length; i++) {

                    file($('input[type="file"]')[i]);
                }
                showpop('Images Uploaded successfully');
                return false;
            }
            function addNewFileUpload(img) {
                var newImageId = $(img).parent().children()[2].children.length;
                var spanid = 'span' + (newImageId + 1);
                var imgid = 'imageUpload' + (newImageId + 1);
                var imgname = 'txtimageName' + (newImageId + 1);
                var str = '<span id="' + spanid + '"><asp:FileUpload  runat="server"  /><asp:TextBox runat="server" placeholder="Enter Image Name" CssClass="form-control" ></asp:TextBox></span>';
                //onchange="file(this)"
                $("#appendImage").append(str);
                $("#" + spanid).children()[0].setAttribute("id", imgid);
                $("#" + spanid).children()[1].setAttribute("id", imgname);
                $("#" + imgname).val("");

                return false;
            }

            function removeNewFileUpload(img) {

                var removeImageId = $(img).parent().children()[2].children.length;
                if (removeImageId == 1) {
                    return false;
                }
                $("#appendImage span").last().remove();

                return false;
            }

        });





         <%--   function DisplayDropdown(ctrl) {
                var inputtext = $("#txtTime").val();

                $.ajax({
                    async: false,
                    type: "POST",
                    url: '<%= ResolveUrl("DataInputModule.aspx/giObjective") %>',
                    //url: "DataInputModule.aspx/giObjective",
                    contentType: "application/json; charset=utf-8",
                    crossDomain: true,
                    data: '{inputtext:"' + inputtext + '"}',
                    dataType: "json",
                    success: function (response) {
                        debugger;
                        var dataitem = response.d;
                        for (let i = 0; i < dataitem.length; i++) {
                            $("#ddlObjective").append("<option>" + dataitem[i] + "</option>");
                        }
                        //$.each(Result.d, function (key, value) {
                        //    $("#ddlcountry").append($("<option></option>").val(value.CountryId).html(value.CountryName));
                        //});
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });
            }

            function DisplayText(control) {
                var timeText = document.getElementById('<%=txtTime.ClientID%>');
                timeText.value = control.value;
                timeText.focus();
            }--%>

</script>
</asp:Content>
