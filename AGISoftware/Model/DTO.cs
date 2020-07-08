using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGISoftware.Model
{
    public class DTO
    {
    }
    public class DependentparameterDetails
    {
        public string dependentParameter { get; set; }
        public List<ParameterDependency> parameterDependencies { get; set; }
        public DependentparameterDetails()
        {
            parameterDependencies = new List<ParameterDependency>();
        }
    }

    public class OMDerivedParameter
    {
        public string SDocName { get; set; }
        public List<DerivedInputParameter> derivedInputParameters { get; set; }
        public List<DerivedOutputParameter> derivedInputParametersOutput { get; set; }
        public List<DerivedOutputParameter> derivedCalculateParametersOutput { get; set; }
        public List<DerivedCalculateParameter> derivedcalculatedtParameters { get; set; }
        public OMDerivedParameter()
        {
            derivedInputParameters = new List<DerivedInputParameter>();
            derivedInputParametersOutput = new List<DerivedOutputParameter>();
            derivedCalculateParametersOutput = new List<DerivedOutputParameter>();
            derivedcalculatedtParameters = new List<DerivedCalculateParameter>();
        }
        public string Sparkouttime { get; set; }        public string Targetrelieftime { get; set; }        public string EquivalentDia { get; set; }        public string WheelWorkspeedratio { get; set; }        public string WheelWorkrpm { get; set; }        public string GrindingCycletime { get; set; }
        public string TraverseSpeed { get; set; }
        public string SlideForward { get; set; }
        public string ProgramRead { get; set; }
        public string Flagging { get; set; }
        public string SlideReturn { get; set; }
        public string Others { get; set; }
        public string OthersTimeDescription { get; set; }
        public string LoadUnloadTime { get; set; }
        public string NongrindingCycleTime { get; set; }
        public string TotalCycletime { get; set; }
        public string EquivalentDiaFace { get; set; }
        public string CuttingEdgeDensity { get; set; }
        public string Chipwidthratio { get; set; }
        public string Wheeltiltangle { get; set; }
        public string ManualLoadingUnloading { get; set; }
        public string remarks { get; set; }
        public string SparkOutRevolutions { get; set; }
        public string TotalGrindingTime { get; set; }
        public string FloorToFloor { get; set; }
    }
    public class RoleAccessRight    {        public string Role { get; set; }        public string Page { get; set; }
        public bool visibilty { get; set; }
        public string PageNameForText { get; set; }        public string PageNameForValue { get; set; }
        public string EmpId { get; set; }        public string EmpName { get; set; }    }
    public class StatisticParamList
    {
        public string InpputModule { get; set; }
        public List<CustomColumn> Values { get; set; }
        public StatisticParamList()
        {
            Values = new List<CustomColumn>();
        }
    }
    public class ParameterNameID    {        public string Parameter { get; set; }        public string ParameterID { get; set; }    }
    public class EmployeeDetails
    {
        public string AdminName { get; set; }
        public string Name { get; set; }
        public string id { get; set; }
        public string email { get; set; }
        public string mblno { get; set; }
        public string role { get; set; }
        public string password { get; set; }
    }
    public class QualityParam
    {
        public string SdocName { get; set; }
        public List<DataInputModuleParameter> Values { get; set; }
        public QualityParam()
        {
            Values = new List<DataInputModuleParameter>();
        }
    }
    public class SdocImages
    {
        public string SdocName { get; set; }
        public List<WorkpieceImage> Values { get; set; }
        public SdocImages()
        {
            Values = new List<WorkpieceImage>();
        }
    }
    public class SdocPlungCat
    {        public string Sdocname { get; set; }        public string Plunge { get; set; }        public string Category { get; set; }        public string Parameter { get; set; }    }

    public class WorkpieceImage
    {
        public string wpImagePath { get; set; }
        public string wpImageName { get; set; }
    }
    public class OperationParameter
    {
        public string Bachid { get; set; }
        public string Bachname { get; set; }
    }
    public class DataInputModuleParameter
    {
        public string Mandatory { get; set; }
        public string Dependancy { get; set; }
        public string IndependentParameter { get; set; }
        public string DependancyFlag { get; set; }
        public string SDOcName { get; set; }
        public string PlungeId { get; set; }
        public string SubcategoryId { get; set; }
        public string Username { get; set; }
        public string CalculatedFlag { get; set; }
        public string CalculatedFormula { get; set; }

        public string Prameter { get; set; }
        public string CustomeName { get; set; }
        public string PrameterId { get; set; }
        public string Datatype { get; set; }
        public string Value { get; set; }
        public string ObjectType { get; set; }
        public string LimitRange { get; set; }
        public string Recommendation { get; set; }
        public string Image { get; set; }

        public string ImagePath { get; set; }
        public string ImageName { get; set; }

        public string TargetLower { get; set; }
        public string TargetUpper { get; set; }
        public string ActualLower { get; set; }
        public string ActualUpper { get; set; }
        public string ParamIdTargetLower { get; set; }
        public string ParamIdTargetUpper { get; set; }
        public string ParamIdActualLower { get; set; }
        public string ParamIdActualUpper { get; set; }
        public string LslUslTargetLower { get; set; }
        public string LslUslTargetUpper { get; set; }
        public string LslUslActualLower { get; set; }
        public string LslUslActualUpper { get; set; }
        public string RecommendationTL { get; set; }
        public string RecommendationTU { get; set; }
        public string RecommendationlAL { get; set; }
        public string RecommendationAU { get; set; }
        public string EntryTypeTL { get; set; }
        public string EntryTypeTU { get; set; }
        public string EntryTypeAL { get; set; }
        public string EntryTypeAU { get; set; }
        public string DataTypeTL { get; set; }
        public string DataTypeTU { get; set; }
        public string DataTypeAL { get; set; }
        public string DataTypeAU { get; set; }
        public string ImageRecommandationTL { get; set; }
        public string ImageRecommandationTU { get; set; }
        public string ImageRecommandationAL { get; set; }
        public string ImageRecommandationAU { get; set; }
        public string DependencyTargetLower { get; set; }
        public string DependencyTargetUpper { get; set; }
        public string DependencyActualLower { get; set; }
        public string DependencyActualUpper { get; set; }
        public string IndependentParameterTargetLower { get; set; }
        public string IndependentParameterTargetUpper { get; set; }
        public string IndependentParameterActualLower { get; set; }
        public string IndependentParameterActualUpper { get; set; }
        public string MandatoryTargetLower { get; set; }
        public string MandatoryTargetUpper { get; set; }
        public string MandatoryActualLower { get; set; }
        public string MandatoryActualUpper { get; set; }
        public string ParameterLowerTarget { get; set; }
        public string ParameterTargetUpper { get; set; }
        public string ParameterActualLower { get; set; }
        public string ParameterActualUpper { get; set; }
        public string CustomenameLowerTarget { get; set; }
        public string CustomenameTargetUpper { get; set; }
        public string CustomenameActualLower { get; set; }
        public string CustomenameActualUpper { get; set; }



        public string ParameterIDFeedRate { get; set; }
        public string ParameterFeedRate { get; set; }
        public string LslUslFeedRate { get; set; }
        public string RecommendationFeedRate { get; set; }
        public string FeedRate { get; set; }
        public string ImageFeedRate { get; set; }
        public string EntryTypeFeedRate { get; set; }
        public string DataTypeFeedRate { get; set; }
        public string DependancyFeedRate { get; set; }
        public string IndependentParameterFeedRate { get; set; }
        public string MandatoryFeedRate { get; set; }
        public string CustomenameFeedRate { get; set; }

        public string ParameterIDDOC { get; set; }
        public string ParameterDOC { get; set; }
        public string LslUslDOC { get; set; }
        public string RecommendationDOC { get; set; }
        public string DOC { get; set; }
        public string ImageDOC { get; set; }
        public string EntryTypeDOC { get; set; }
        public string DataTypeDOC { get; set; }
        public string DependancyDoc { get; set; }
        public string IndependentParameterDoc { get; set; }
        public string MandatoryDoc { get; set; }
        public string CustomenameDoc { get; set; }

        public string ParameterIDFace { get; set; }
        public string ParameterFace { get; set; }
        public string LslUslFace { get; set; }
        public string RecommendationFace { get; set; }
        public string Face { get; set; }
        public string ImageFace { get; set; }
        public string EntryTypeFace { get; set; }
        public string DataTypeFace { get; set; }
        public string DependancyFace { get; set; }
        public string IndependentParameterFace { get; set; }
        public string MandatoryFace { get; set; }
        public string CustomenameFace { get; set; }

        public string ParameterIDWorkRPM { get; set; }
        public string ParameterWorkRPM { get; set; }
        public string LslUslWorkRPM { get; set; }
        public string RecommendationWorkRPM { get; set; }
        public string WorkRPM { get; set; }
        public string ImageWorkRPM { get; set; }
        public string EntryTypeWorkRPM { get; set; }
        public string DataTypeWorkRPM { get; set; }
        public string DependancyWorkRPM { get; set; }
        public string IndependentParameterWorkRPM { get; set; }
        public string MandatoryWorkRPM { get; set; }
        public string CustomenameWorkRPM { get; set; }

        public string ParameterIDWheelms { get; set; }
        public string ParameterWheelms { get; set; }
        public string LslUslWheelms { get; set; }
        public string RecommendationWheelms { get; set; }
        public string Wheelms { get; set; }
        public string ImageWheelms { get; set; }
        public string EntryTypeWheelms { get; set; }
        public string DataTypeWheelms { get; set; }
        public string DependancyWheelms { get; set; }
        public string IndependentParameterWheelms { get; set; }
        public string MandatoryWheelms { get; set; }
        public string CustomenameWheelms { get; set; }

        public string ParameterIDWorkms { get; set; }
        public string ParameterWorkms { get; set; }
        public string LslUslWorkms { get; set; }
        public string RecommendationWorkms { get; set; }
        public string Workms { get; set; }
        public string ImageWorkms { get; set; }
        public string EntryTypeWorkms { get; set; }
        public string DataTypeWorkms { get; set; }
        public string GrindingProcess { get; set; }
        public string DependancyWorkms { get; set; }
        public string IndependentParameterWorkms { get; set; }
        public string MandatoryWorkms { get; set; }
           public string CustomenameWorkms { get; set; }

        public string ParameterIDWheelRPM { get; set; }
        public string ParameterWheelRPM { get; set; }
        public string LslUslWheelRPM { get; set; }
        public string RecommendationWheelRPM { get; set; }
        public string WheelRPM { get; set; }
        public string ImageWheelRPM { get; set; }
        public string EntryTypeWheelRPM { get; set; }
        public string DataTypeWheelRPM { get; set; }
        public string DependancyWheelRPM { get; set; }
        public string IndependentParameterWheelRPM { get; set; }
        public string MandatoryWheelRPM { get; set; }
        public string CustomenameWheelRPM { get; set; }
    }
    public class ParameterDetails
    {
        public string AdminName { get; set; }
        public string Parameter { get; set; }
        public string Id { get; set; }
        public string LSL { get; set; }
        public string USL { get; set; }
        public string Reccomandation { get; set; }
        public string InputModule { get; set; }
        public string SubInputModule { get; set; }
        public bool Enableflag { get; set; }
        public string IsNumeric { get; set; }
        public string SortOrder { get; set; }
        public string Image { get; set; }
        public byte[] ImageLimit { get; set; }
        public bool DefaultParam { get; set; }
        public string EntryType { get; set; }
        public string DataType { get; set; }
        public string Mandatoryflag { get; set; }
        public bool Dependencyflag { get; set; }
        public string Deletableflag { get; set; }
        public string IndependentParameter { get; set; }
        public List<string> IndepenedentParameterList { get; set; }
        public string Customname { get; set; }
    }

    public class ADKParameter
    {
        public string Parameter { get; set; }

    }
    public class DerivedOutputParameter    {
        public string EquivalentDiaFace { get; set; }
        public string CuttingEdgeDensity { get; set; }
        public string Chipwidthratio { get; set; }
        public string Wheeltiltangle { get; set; }        public string Sparkouttime { get; set; }        public string Targetrelieftime { get; set; }        public string EquivalentDia { get; set; }        public string WheelWorkspeedratio { get; set; }        public string WheelWorkrpm { get; set; }        public string GrindingCycletime { get; set; }        public string signalfilename { get; set; }
        public string signalfilepath { get; set; }
       // public byte[] signalfilepath { get; set; }
        public string TraverseSpeed { get; set; }
        public string SlideForward { get; set; }
        public string ProgramRead { get; set; }
        public string Flagging { get; set; }
        public string SlideReturn { get; set; }
        public string Others { get; set; }
        public string OthersTimeDescription { get; set; }
        public string LoadUnloadTime { get; set; }
        public string NongrindingCycleTime { get; set; }
        public string TotalCycletime { get; set; }
        public string SDocId { get; set; }
        public string ManualLoadingUnloading { get; set; }
        public string remarks { get; set; }
        public string SparkOutRevolutions { get; set; }
        public string TotalGrindingTime { get; set; }
        public string FloorToFloor { get; set; }
        public string DressingCycleTime { get; set; }
    }
    public class ADKParameterMinMaxAvg
    {
        public string Parameter { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Avg { get; set; }
        public string DerivedFlag { get; set; }
    }

    public class InputModuleParameter
    {
        public string ParameterDetails { get; set; }
        public string LSL { get; set; }
        public string USL { get; set; }
        public string Limitrange { get; set; }
    }
    public class DataInputModuleDetails
    {
        public string giSdoc { get; set; }
        public string giCustomer { get; set; }
        public string giPlant { get; set; }
        public string giObjective { get; set; }
        public string giDateofTrial { get; set; }
        public string giLocation { get; set; }
        public string giContactPerson { get; set; }
        public string giMblNo { get; set; }
        public string giMail { get; set; }
        public string giComment { get; set; }

        //Machine tool
        public string mtMachineModel { get; set; }
        public string mtMachineNo { get; set; }
        public string mtMachineToolBuilder { get; set; }
        public string mtGrindingWheelDriver { get; set; }
        public string mtGrindingtype { get; set; }
        public string mtGrindingHead { get; set; }
        public string mtCoolantmake { get; set; }
        public string mtCoolanttype { get; set; }
        public string mtCoolantcapacity { get; set; }
        public string mtGuideXaxis { get; set; }
        public string mtGuideYaxis { get; set; }
        public string mtGuideZaxis { get; set; }
        public string mtGuideUaxis { get; set; }
        public string mtGuideWaxis { get; set; }
        public string mtGrindingMotorPwr { get; set; }
        public string mtWorkholdtype { get; set; }
        public string mtCentertypehead { get; set; }
        public string mtCentertypestock { get; set; }
        public string mtControlSysMake { get; set; }
        public string mtControlSysModel { get; set; }
        public string mtGuagetype { get; set; }
        public string mtGuagemake { get; set; }
        public string mtGuagemodel { get; set; }
        public string mtSteadyrest { get; set; }
        public string mtFlagtype { get; set; }
        public string mtFlagmake { get; set; }
        public string mtFlagmodel { get; set; }
        public string mtBalancertype { get; set; }
        public string mtBalancermake { get; set; }
        public string mtBalancermodel { get; set; }
        public string mtAutomationtype { get; set; }
        public string mtGapcrachmake { get; set; }
        public string mtGapcrachmodel { get; set; }
        public string mtJobrest { get; set; }
        public string mtCoolantNozzle { get; set; }

        //Consumable
        public string cmWheelmake { get; set; }
        public string cmWheelspecs { get; set; }
        public string cmAbrasivetype { get; set; }
        public string cmAbrasivesize { get; set; }
        public string cmGrade { get; set; }
        public string cmStructure { get; set; }
        public string cmBondtype { get; set; }
        public string cmWheelmaxDia { get; set; }
        public string cmWheelminDia { get; set; }
        public string cmWheelwidth { get; set; }
        public string cmWheelmaxCutspeed { get; set; }
        public string cmDressermake { get; set; }
        public string cmDressertype { get; set; }
        public string cmDresserspecs { get; set; }
        public string cmCutterwidth { get; set; }
        public string cmMounting { get; set; }
        public string cmDiarollsize { get; set; }
        public string cmCoolanttype { get; set; }
        public string cmCoolantmake { get; set; }
        public string cmCoolantspecs { get; set; }

        //Workpiece
        public string wpComponentname { get; set; }
        public string wpDrawingNo { get; set; }
        public string wpMaterial { get; set; }
        public string wpHardness { get; set; }
        public string wpHardnessunit { get; set; }
        public string wpInitialdia { get; set; }
        public string wpStockODdia { get; set; }
        public string wpStockFacedia { get; set; }
        public string wpGrindingwidth { get; set; }
        public string wpInputdiaFace { get; set; }
        public string wpFacewidth { get; set; }

        //Operational Parameters
        public string opgCurrentwheeldia { get; set; }
        public string opgWheelspeedVs { get; set; }
        public string opgWheelspeedNs { get; set; }
        public string opgWorkspeedNw { get; set; }
        public string opgWorkspeedVw { get; set; }
        public string opgRoughingfeed1 { get; set; }
        public string opgRoughingfeed2 { get; set; }
        public string opgRoughingfeed3 { get; set; }
        public string opgRoughingfeed4 { get; set; }
        public string opgRoughingfeed5 { get; set; }
        public string opgSemifinishFeed1 { get; set; }
        public string opgSemifinishFeed2 { get; set; }
        public string opgSemifinishFeed3 { get; set; }
        public string opgFinishfeed1 { get; set; }
        public string opgFinishfeed2 { get; set; }
        public string opgRoughDoc1 { get; set; }
        public string opgRoughDoc2 { get; set; }
        public string opgRoughDoc3 { get; set; }
        public string opgRoughDoc4 { get; set; }
        public string opgRoughDoc5 { get; set; }
        public string opgSemifinishDoc1 { get; set; }
        public string opgSemifinishDoc2 { get; set; }
        public string opgSemifinishDoc3 { get; set; }
        public string opgFinishDoc1 { get; set; }
        public string opgFinishDoc2 { get; set; }
        public string opgCoolaneConcentrate { get; set; }


        public string opdDressspeed { get; set; }
        public string opdDressfeedrateOD { get; set; }
        public string opdDressfeedrateFace { get; set; }
        public string opdDressfeedrateRadius { get; set; }
        public string opdDressdepthDia { get; set; }
        public string opdTypeofCut { get; set; }
        public string opdDressfrequency { get; set; }
        public string opdNoofPass { get; set; }
        public string opddLeadDI { get; set; }
        public string opddOverlapratio { get; set; }
        public string opddCrushratio { get; set; }

        //Quality Parameters
        public string qpODTolarance1TL { get; set; }
        public string qpODTolarance1TU { get; set; }
        public string qpODTolarance1AL { get; set; }
        public string qpODTolarance1AU { get; set; }
        public string qpODTolarance2TL { get; set; }
        public string qpODTolarance2TU { get; set; }
        public string qpODTolarance2AL { get; set; }
        public string qpODTolarance2AU { get; set; }
        public string qpODTolarance3TL { get; set; }
        public string qpODTolarance3TU { get; set; }
        public string qpODTolarance3AL { get; set; }
        public string qpODTolarance3AU { get; set; }
        public string qpSurFinishRaTL { get; set; }
        public string qpSurFinishRaTU { get; set; }
        public string qpSurFinishRaAL { get; set; }
        public string qpSurFinishRaAU { get; set; }
        public string qpRzTL { get; set; }
        public string qpRzTU { get; set; }
        public string qpRzAL { get; set; }
        public string qpRzAU { get; set; }
        public string qpRtTL { get; set; }
        public string qpRtTU { get; set; }
        public string qpRtAL { get; set; }
        public string qpRtAU { get; set; }
        public string qpRmaxTL { get; set; }
        public string qpRmaxTU { get; set; }
        public string qpRmaxAL { get; set; }
        public string qpRmaxAU { get; set; }
        public string qpTpTL { get; set; }
        public string qpTpTU { get; set; }
        public string qpTpAL { get; set; }
        public string qpTpAU { get; set; }
        public string qpCpTL { get; set; }
        public string qpCpTU { get; set; }
        public string qpCpAL { get; set; }
        public string qpCpAU { get; set; }
        public string qpCpkTL { get; set; }
        public string qpCpkTU { get; set; }
        public string qpCpkAL { get; set; }
        public string qpCpkAU { get; set; }
        public string qpPpTL { get; set; }
        public string qpPpTU { get; set; }
        public string qpPpAL { get; set; }
        public string qpPpAU { get; set; }
        public string qpPpkTL { get; set; }
        public string qpPpkTU { get; set; }
        public string qpPpkAL { get; set; }
        public string qpPpkAU { get; set; }
        public string qpCmTL { get; set; }
        public string qpCmTU { get; set; }
        public string qpCmAL { get; set; }
        public string qpCmAU { get; set; }
        public string qpCmkTL { get; set; }
        public string qpCmkTU { get; set; }
        public string qpCmkAL { get; set; }
        public string qpCmkAU { get; set; }

        public string qpGrindcycletimeTL { get; set; }
        public string qpGrindcycletimeTU { get; set; }
        public string qpGrindcycletimeAL { get; set; }
        public string qpGrindcycletimeAU { get; set; }

        public string qpGrindcycletimeFloorTL { get; set; }
        public string qpGrindcycletimeFloorTU { get; set; }
        public string qpGrindcycletimeFloorAL { get; set; }
        public string qpGrindcycletimeFloorAU { get; set; }

        public string qpDresscycletimeTL { get; set; }
        public string qpDresscycletimeTU { get; set; }
        public string qpDresscycletimeAL { get; set; }
        public string qpDresscycletimeAU { get; set; }

        public string qpStraightnessTL { get; set; }
        public string qpStraightnessTU { get; set; }
        public string qpStraightnessAL { get; set; }
        public string qpStraightnessAU { get; set; }

        public string qpFlatnessTL { get; set; }
        public string qpFlatnessTU { get; set; }
        public string qpFlatnessAL { get; set; }
        public string qpFlatnessAU { get; set; }

        public string qpRoundnessTL { get; set; }
        public string qpRoundnessTU { get; set; }
        public string qpRoundnessAL { get; set; }
        public string qpRoundnessAU { get; set; }

        public string qpCylindricityTL { get; set; }
        public string qpCylindricityTU { get; set; }
        public string qpCylindricityAL { get; set; }
        public string qpCylindricityAU { get; set; }

        public string qpPerpendicularityTL { get; set; }
        public string qpPerpendicularityTU { get; set; }
        public string qpPerpendicularityAL { get; set; }
        public string qpPerpendicularityAU { get; set; }

        public string qpAngleTL { get; set; }
        public string qpAngleTU { get; set; }
        public string qpAngleAL { get; set; }
        public string qpAngleAU { get; set; }

        public string qpParallelismTL { get; set; }
        public string qpParallelismTU { get; set; }
        public string qpParallelismAL { get; set; }
        public string qpParallelismAU { get; set; }

        public string qpProfAccuracyTL { get; set; }
        public string qpProfAccuracyTU { get; set; }
        public string qpProfAccuracyAL { get; set; }
        public string qpProfAccuracyAU { get; set; }

        public string qpRunoutODTL { get; set; }
        public string qpRunoutODTU { get; set; }
        public string qpRunoutODAL { get; set; }
        public string qpRunoutODAU { get; set; }

        public string qpRunoutFaceTL { get; set; }
        public string qpRunoutFaceTU { get; set; }
        public string qpRunoutFaceAL { get; set; }
        public string qpRunoutFaceAU { get; set; }

        public string qpTotalRunoutODTL { get; set; }
        public string qpTotalRunoutODTU { get; set; }
        public string qpTotalRunoutODAL { get; set; }
        public string qpTotalRunoutODAU { get; set; }

        public string qplinearDiaTL { get; set; }
        public string qplinearDiaTU { get; set; }
        public string qplinearDiaAL { get; set; }
        public string qplinearDiaAU { get; set; }

        public string qpTaperTL { get; set; }
        public string qpTaperTU { get; set; }
        public string qpTaperAL { get; set; }
        public string qpTaperAU { get; set; }

        public string qpChamferTL { get; set; }
        public string qpChamferTU { get; set; }
        public string qpChamferAL { get; set; }
        public string qpChamferAU { get; set; }

        public string qpConcertricityTL { get; set; }
        public string qpConcertricityTU { get; set; }
        public string qpConcertricityAL { get; set; }
        public string qpConcertricityAU { get; set; }

        public string qpSymmetryTL { get; set; }
        public string qpSymmetryTU { get; set; }
        public string qpSymmetryAL { get; set; }
        public string qpSymmetryAU { get; set; }

    }

    public class CustomColumn    {
        public string DerivedFlag { get; set; }
        public string InputModule { get; set; }
        public string DistinctInputModule { get; set; }        public string ColumnName { get; set; }        public string CustomName { get; set; }    }
    public class DerivedInputParameter    {        public string Point { get; set; }        public string Identifier { get; set; }        public string Diameter { get; set; }        public string InFeed { get; set; }
        public string StockonFace { get; set; }        public string WorkSpeed { get; set; }        public string ODWidth { get; set; }
        public string DOC { get; set; }
        public string SDocId { get; set; }
        public string Parameter { get; set; }
        public string FeedAngle { get; set; }        public string WorkSpeedOD { get; set; }
        public string WorkSpeedFace { get; set; }
        public string XFeed { get; set; }
        public string ZFeed { get; set; }
        public string TangoFlagOD { get; set; }
        public string TangoFlagFace { get; set; }
        public string TangoColor { get; set; }    }


    public class DerivedCalculateParameter    {
        public string Point { get; set; }        public string MRR { get; set; }        public string ToralMRR { get; set; }        public string GritPenetrationDepth { get; set; }        public string Time { get; set; }        public string RadialDepthofCut { get; set; }
        public string WorkSpeedRatio { get; set; }
        public string WorkRPMRatio { get; set; }
        public string SDocId { get; set; }
        public string Parameter { get; set; }
        public string MRRX { get; set; }
        public string MRRZ { get; set; }
        public string TotalMRRX { get; set; }
        public string GritPenetrationDepthX { get; set; }
        public string GritPenetrationDepthZ { get; set; }
        public string RadialDOCX { get; set; }
        public string RadialDOCZ { get; set; }
        public string TangoFlagOD { get; set; }
        public string TangoFlagFace { get; set; }
        public string TangoColor { get; set; }
    }
    public class OutputModuleParam    {
        public string Parameter { get; set; }
        public string Value { get; set; }
        public string TargetLower { get; set; }
        public string TargetUpper { get; set; }
        public string ActualLower { get; set; }
        public string ActualUpper { get; set; }        public string RoughTime { get; set; }        public string SemifinishTime { get; set; }        public string FinishTime { get; set; }        public string SparkoutTime { get; set; }        public string TangoReliefTime { get; set; }        public string RoughMRR { get; set; }        public string SemifinishMRR { get; set; }        public string FinishMRR { get; set; }        public string WorkRatio { get; set; }        public string EquivalentDia { get; set; }        public string OverlapRatio { get; set; }        public string DressLead { get; set; }    }


    public class ParameterDependency    {
        public string InputModule { get; set; }
        public string ParameterId1 { get; set; }
        public string Parameter1 { get; set; }
        public string Parameter1Value { get; set; }
        public string ParameterId2 { get; set; }
        public string Parameter2 { get; set; }
        public string Parameter2Value { get; set; }
        public string LSL { get; set; }
        public string USL { get; set; }
       // public string USL { get; set; }
    }


    public class DeletedSDocDetails    {
        public string InputModule { get; set; }
        public string SubInputModule { get; set; }
        public string Parameter { get; set; }
        public string SDocId { get; set; }
        public string Value { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedDate { get; set; }
    }
    public class DrillChart
    {
        public List<ChartSeries> listChartSeries { get; set; }
        public List<DrildownSeries> listDrilldownSeries { get; set; }
    }

    public class ChartSeries
    {
        
        public string name { get; set; }
        public decimal y { get; set; }
        public string drilldown { get; set; }
        public string color { get; set; }
    }

    public class DrildownSeries
    {
       // public string type { get; set; }
        public string name { get; set; }
        public string id { get; set; }
      //  public bool colorByPoint { get; set; }
        public List<DrildownData> data { get; set; }
    }

    public class DrildownData
    {
        public string name { get; set; }
        public decimal y { get; set; }
        public string color { get; set; }
        //public string drilldown { get; set; }
        //public string afterTitel { get; set; }
        //public string beforeTitle { get; set; }
    }
    public class TotalCycleTimeGrpah
    {
        public string SDocid { get; set; }
        public List<TotalCycleTimeGrpahValue> values { get; set; }
        public TotalCycleTimeGrpah()
        {
            values = new List<TotalCycleTimeGrpahValue>();
        }
    }
    public class TotalCycleTimeGrpahValue
    {
        public string Parameter { get; set; }
        public string Value { get; set; }
    }
}