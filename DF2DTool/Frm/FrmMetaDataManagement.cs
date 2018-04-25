using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DFDataConfig.Class;
using DF2DData.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using DevExpress.XtraTreeList.Nodes;
using DevExpress;
using DevExpress.XtraTreeList;
using ESRI.ArcGIS.Carto;
using DF2DControl.Base;
using DFCommon.Class;

namespace DF2DTool.Frm
{
    public partial class FrmMetaDataManagement : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraEditors.SimpleButton btn_OK;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tpGeneral;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraTab.XtraTabPage tpSheetCoord;
        private DevExpress.XtraTab.XtraTabPage tpProduceInfo;
        private DevExpress.XtraTab.XtraTabPage tpQualityInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup11;
        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup12;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup13;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup14;
        private TextEdit te_ClassDesc;
        private TextEdit te_SurveyDesc;
        private TextEdit te_SYMDesc;
        private TextEdit te_MapNum;
        private TextEdit te_Maptitle;
        private ComboBoxEdit cb_DisName;
        private ComboBoxEdit cb_Scale;
        private ComboBoxEdit cb_Contour;
        private ComboBoxEdit cb_PSYS;
        private ComboBoxEdit cb_HSYS;
        private ComboBoxEdit cb_Secrecy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private TextEdit te_NWY;
        private TextEdit te_NWX;
        private TextEdit te_SWY;
        private TextEdit te_SWX;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private TextEdit te_UpdateDept;
        private TextEdit te_UpdateMapper;
        private TextEdit te_UpdateReason;
        private TextEdit te_UpdateSurveyor;
        private TextEdit te_UpdateArea;
        private TextEdit te_CreateDept;
        private TextEdit te_MapCollate;
        private TextEdit te_Mapper;
        private TextEdit te_Checker;
        private TextEdit te_Surveyor;
        private TextEdit te_TechnologyPrincipal;
        private TextEdit te_ProjectPrincipal;
        private TextEdit te_CartoMode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private TextEdit te_UpdateDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem34;
        private TextEdit te_QualityExamDept;
        private TextEdit te_QualityJudge;
        private TextEdit te_AttMSE;
        private TextEdit te_PMSE;
        private TextEdit te_HMSE;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem35;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem36;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem37;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem38;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem39;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem40;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Sheet;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Object;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private Dictionary<string,List<IFeature>> dictFC;
        private HashSet<string> secrecy;
        private HashSet<string> scale;
        private HashSet<string> contour;
        private HashSet<string> pSYS;
        private HashSet<string> hSYS;

        private int indexDisName;
        private int indexGEOOBJNUM;
        private int indexSecrecy;
        private int indexScale;
        private int indexContour;
        private int indexPSYS;
        private int indexHSYS;
        private int indexSYMDesc;
        private int indexClassDesc;
        private int indexSurveyDesc;
        private int indexSWY;
        private int indexSWX;
        private int indexNWY;
        private int indexNWX;
        private int indexCarto;
        private int indexSurveyor;
        private int indexCreateDept;
        private int indexMapper;
        private int indexMapcollate;
        private int indexCreateDate;
        private int indexProjectPrincipal;
        private int indexTechnologyPrincipal;
        private int indexChecker;
        private int indexUpdater;
        private int indexUpdateArea;
        private int indexUpdateReason;
        private int indexUpdateMapper;
        private int indexUpdateDept;
        private int indexUpdateDate;
        private int indexHMSE;
        private int indexPMSE;
        private int indexQualityJudge;
        private int indexQualityExamDept;
        private int indexAttMSE;
        private int indexQualityDate;
        private int indexMapTitle;
        private int indexMapNO;
        private IFeatureClass fc;
        private TextEdit te_SurveyDate;
        private TextEdit te_QualityDate;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private IContainer components;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private FacilityClass facilityClass;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barButtonItem9;
        private DevExpress.XtraBars.PopupMenu popupMenu2;
        DF2DApplication app = DF2DApplication.Application;
    
        public FrmMetaDataManagement()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.Sheet = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Object = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tpGeneral = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.te_ClassDesc = new DevExpress.XtraEditors.TextEdit();
            this.te_SurveyDesc = new DevExpress.XtraEditors.TextEdit();
            this.te_SYMDesc = new DevExpress.XtraEditors.TextEdit();
            this.te_MapNum = new DevExpress.XtraEditors.TextEdit();
            this.te_Maptitle = new DevExpress.XtraEditors.TextEdit();
            this.cb_DisName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cb_Scale = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cb_Contour = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cb_PSYS = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cb_HSYS = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cb_Secrecy = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpSheetCoord = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.te_NWY = new DevExpress.XtraEditors.TextEdit();
            this.te_NWX = new DevExpress.XtraEditors.TextEdit();
            this.te_SWY = new DevExpress.XtraEditors.TextEdit();
            this.te_SWX = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpProduceInfo = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.te_UpdateDate = new DevExpress.XtraEditors.TextEdit();
            this.te_UpdateDept = new DevExpress.XtraEditors.TextEdit();
            this.te_UpdateMapper = new DevExpress.XtraEditors.TextEdit();
            this.te_UpdateReason = new DevExpress.XtraEditors.TextEdit();
            this.te_UpdateSurveyor = new DevExpress.XtraEditors.TextEdit();
            this.te_UpdateArea = new DevExpress.XtraEditors.TextEdit();
            this.te_CreateDept = new DevExpress.XtraEditors.TextEdit();
            this.te_MapCollate = new DevExpress.XtraEditors.TextEdit();
            this.te_Mapper = new DevExpress.XtraEditors.TextEdit();
            this.te_Checker = new DevExpress.XtraEditors.TextEdit();
            this.te_Surveyor = new DevExpress.XtraEditors.TextEdit();
            this.te_TechnologyPrincipal = new DevExpress.XtraEditors.TextEdit();
            this.te_ProjectPrincipal = new DevExpress.XtraEditors.TextEdit();
            this.te_CartoMode = new DevExpress.XtraEditors.TextEdit();
            this.te_SurveyDate = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup11 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup14 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpQualityInfo = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.te_QualityExamDept = new DevExpress.XtraEditors.TextEdit();
            this.te_QualityJudge = new DevExpress.XtraEditors.TextEdit();
            this.te_AttMSE = new DevExpress.XtraEditors.TextEdit();
            this.te_PMSE = new DevExpress.XtraEditors.TextEdit();
            this.te_HMSE = new DevExpress.XtraEditors.TextEdit();
            this.te_QualityDate = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup12 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup13 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem36 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem37 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem38 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem39 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem40 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu2 = new DevExpress.XtraBars.PopupMenu();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_ClassDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_SurveyDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_SYMDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_MapNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Maptitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_DisName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_Scale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_Contour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_PSYS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_HSYS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_Secrecy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            this.tpSheetCoord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_NWY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_NWX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_SWY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_SWX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            this.tpProduceInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateMapper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateSurveyor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_CreateDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_MapCollate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Mapper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Checker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Surveyor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_TechnologyPrincipal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ProjectPrincipal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_CartoMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_SurveyDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).BeginInit();
            this.tpQualityInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_QualityExamDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_QualityJudge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_AttMSE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_PMSE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_HMSE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_QualityDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.treeList1);
            this.layoutControl1.Controls.Add(this.btn_OK);
            this.layoutControl1.Controls.Add(this.btn_Save);
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(613, 331);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.Sheet,
            this.Object});
            this.treeList1.Location = new System.Drawing.Point(5, 5);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.treeList1.OptionsView.ShowColumns = false;
            this.treeList1.Size = new System.Drawing.Size(150, 321);
            this.treeList1.TabIndex = 7;
            this.treeList1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseClick);
            this.treeList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseUp);
            // 
            // Sheet
            // 
            this.Sheet.Caption = "图幅名";
            this.Sheet.FieldName = "Sheet";
            this.Sheet.Name = "Sheet";
            this.Sheet.OptionsColumn.AllowEdit = false;
            this.Sheet.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            this.Sheet.Visible = true;
            this.Sheet.VisibleIndex = 0;
            // 
            // Object
            // 
            this.Object.Caption = "对象";
            this.Object.FieldName = "Object";
            this.Object.Name = "Object";
            this.Object.OptionsColumn.AllowEdit = false;
            this.Object.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.Object;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(516, 304);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(92, 22);
            this.btn_OK.StyleController = this.layoutControl1;
            this.btn_OK.TabIndex = 6;
            this.btn_OK.Text = "确定";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(424, 304);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(88, 22);
            this.btn_Save.StyleController = this.layoutControl1;
            this.btn_Save.TabIndex = 5;
            this.btn_Save.Text = "保存图幅";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(165, 5);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tpGeneral;
            this.xtraTabControl1.Size = new System.Drawing.Size(443, 289);
            this.xtraTabControl1.TabIndex = 4;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpGeneral,
            this.tpSheetCoord,
            this.tpProduceInfo,
            this.tpQualityInfo});
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.layoutControl2);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Size = new System.Drawing.Size(437, 260);
            this.tpGeneral.Text = "基本信息";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.te_ClassDesc);
            this.layoutControl2.Controls.Add(this.te_SurveyDesc);
            this.layoutControl2.Controls.Add(this.te_SYMDesc);
            this.layoutControl2.Controls.Add(this.te_MapNum);
            this.layoutControl2.Controls.Add(this.te_Maptitle);
            this.layoutControl2.Controls.Add(this.cb_DisName);
            this.layoutControl2.Controls.Add(this.cb_Scale);
            this.layoutControl2.Controls.Add(this.cb_Contour);
            this.layoutControl2.Controls.Add(this.cb_PSYS);
            this.layoutControl2.Controls.Add(this.cb_HSYS);
            this.layoutControl2.Controls.Add(this.cb_Secrecy);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup3;
            this.layoutControl2.Size = new System.Drawing.Size(437, 260);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // te_ClassDesc
            // 
            this.te_ClassDesc.Location = new System.Drawing.Point(104, 207);
            this.te_ClassDesc.Name = "te_ClassDesc";
            this.te_ClassDesc.Size = new System.Drawing.Size(328, 22);
            this.te_ClassDesc.StyleController = this.layoutControl2;
            this.te_ClassDesc.TabIndex = 14;
            // 
            // te_SurveyDesc
            // 
            this.te_SurveyDesc.Location = new System.Drawing.Point(104, 181);
            this.te_SurveyDesc.Name = "te_SurveyDesc";
            this.te_SurveyDesc.Size = new System.Drawing.Size(328, 22);
            this.te_SurveyDesc.StyleController = this.layoutControl2;
            this.te_SurveyDesc.TabIndex = 13;
            // 
            // te_SYMDesc
            // 
            this.te_SYMDesc.Location = new System.Drawing.Point(104, 155);
            this.te_SYMDesc.Name = "te_SYMDesc";
            this.te_SYMDesc.Size = new System.Drawing.Size(328, 22);
            this.te_SYMDesc.StyleController = this.layoutControl2;
            this.te_SYMDesc.TabIndex = 12;
            // 
            // te_MapNum
            // 
            this.te_MapNum.Location = new System.Drawing.Point(325, 51);
            this.te_MapNum.Name = "te_MapNum";
            this.te_MapNum.Size = new System.Drawing.Size(107, 22);
            this.te_MapNum.StyleController = this.layoutControl2;
            this.te_MapNum.TabIndex = 6;
            // 
            // te_Maptitle
            // 
            this.te_Maptitle.Location = new System.Drawing.Point(104, 51);
            this.te_Maptitle.Name = "te_Maptitle";
            this.te_Maptitle.Size = new System.Drawing.Size(118, 22);
            this.te_Maptitle.StyleController = this.layoutControl2;
            this.te_Maptitle.TabIndex = 5;
            // 
            // cb_DisName
            // 
            this.cb_DisName.Location = new System.Drawing.Point(104, 25);
            this.cb_DisName.Name = "cb_DisName";
            this.cb_DisName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_DisName.Size = new System.Drawing.Size(118, 22);
            this.cb_DisName.StyleController = this.layoutControl2;
            this.cb_DisName.TabIndex = 4;
            // 
            // cb_Scale
            // 
            this.cb_Scale.Location = new System.Drawing.Point(104, 77);
            this.cb_Scale.Name = "cb_Scale";
            this.cb_Scale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_Scale.Size = new System.Drawing.Size(118, 22);
            this.cb_Scale.StyleController = this.layoutControl2;
            this.cb_Scale.TabIndex = 7;
            // 
            // cb_Contour
            // 
            this.cb_Contour.Location = new System.Drawing.Point(325, 77);
            this.cb_Contour.Name = "cb_Contour";
            this.cb_Contour.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_Contour.Size = new System.Drawing.Size(107, 22);
            this.cb_Contour.StyleController = this.layoutControl2;
            this.cb_Contour.TabIndex = 8;
            // 
            // cb_PSYS
            // 
            this.cb_PSYS.Location = new System.Drawing.Point(104, 103);
            this.cb_PSYS.Name = "cb_PSYS";
            this.cb_PSYS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_PSYS.Size = new System.Drawing.Size(118, 22);
            this.cb_PSYS.StyleController = this.layoutControl2;
            this.cb_PSYS.TabIndex = 9;
            // 
            // cb_HSYS
            // 
            this.cb_HSYS.Location = new System.Drawing.Point(325, 103);
            this.cb_HSYS.Name = "cb_HSYS";
            this.cb_HSYS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_HSYS.Size = new System.Drawing.Size(107, 22);
            this.cb_HSYS.StyleController = this.layoutControl2;
            this.cb_HSYS.TabIndex = 10;
            // 
            // cb_Secrecy
            // 
            this.cb_Secrecy.Location = new System.Drawing.Point(325, 25);
            this.cb_Secrecy.Name = "cb_Secrecy";
            this.cb_Secrecy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_Secrecy.Size = new System.Drawing.Size(107, 22);
            this.cb_Secrecy.StyleController = this.layoutControl2;
            this.cb_Secrecy.TabIndex = 11;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup4,
            this.layoutControlGroup5});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(437, 260);
            this.layoutControlGroup3.Text = "layoutControlGroup3";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "基本属性信息";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(437, 130);
            this.layoutControlGroup4.Text = "基本属性信息";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cb_DisName;
            this.layoutControlItem5.CustomizationFormText = "测区名称：";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem5.Text = "测区名称：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.te_Maptitle;
            this.layoutControlItem6.CustomizationFormText = "图名：";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem6.Text = "图名：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.te_MapNum;
            this.layoutControlItem7.CustomizationFormText = "图号：";
            this.layoutControlItem7.Location = new System.Drawing.Point(221, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(210, 26);
            this.layoutControlItem7.Text = "图号：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cb_Scale;
            this.layoutControlItem8.CustomizationFormText = "比例尺：";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem8.Text = "比例尺：";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.cb_Contour;
            this.layoutControlItem9.CustomizationFormText = "等高距：";
            this.layoutControlItem9.Location = new System.Drawing.Point(221, 52);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(210, 26);
            this.layoutControlItem9.Text = "等高距：";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.cb_PSYS;
            this.layoutControlItem10.CustomizationFormText = "坐标系统：";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem10.Text = "坐标系统：";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.cb_HSYS;
            this.layoutControlItem11.CustomizationFormText = "高程系统：";
            this.layoutControlItem11.Location = new System.Drawing.Point(221, 78);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(210, 26);
            this.layoutControlItem11.Text = "高程系统：";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.cb_Secrecy;
            this.layoutControlItem12.CustomizationFormText = "保密等级：";
            this.layoutControlItem12.Location = new System.Drawing.Point(221, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(210, 26);
            this.layoutControlItem12.Text = "保密等级：";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "图式、编码及规范";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 130);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(437, 130);
            this.layoutControlGroup5.Text = "图式、编码及规范";
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.te_SYMDesc;
            this.layoutControlItem13.CustomizationFormText = "图式及编号：";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem13.Text = "图式及编号：";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.te_SurveyDesc;
            this.layoutControlItem14.CustomizationFormText = "测量规范及编号：";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem14.Text = "测量规范及编号：";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.te_ClassDesc;
            this.layoutControlItem15.CustomizationFormText = "分类编码及编号：";
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(431, 52);
            this.layoutControlItem15.Text = "分类编码及编号：";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(96, 14);
            // 
            // tpSheetCoord
            // 
            this.tpSheetCoord.Controls.Add(this.layoutControl3);
            this.tpSheetCoord.Name = "tpSheetCoord";
            this.tpSheetCoord.Size = new System.Drawing.Size(437, 260);
            this.tpSheetCoord.Text = "图幅范围";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.te_NWY);
            this.layoutControl3.Controls.Add(this.te_NWX);
            this.layoutControl3.Controls.Add(this.te_SWY);
            this.layoutControl3.Controls.Add(this.te_SWX);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup8;
            this.layoutControl3.Size = new System.Drawing.Size(437, 260);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // te_NWY
            // 
            this.te_NWY.Location = new System.Drawing.Point(122, 103);
            this.te_NWY.Name = "te_NWY";
            this.te_NWY.Size = new System.Drawing.Size(310, 22);
            this.te_NWY.StyleController = this.layoutControl3;
            this.te_NWY.TabIndex = 7;
            // 
            // te_NWX
            // 
            this.te_NWX.Location = new System.Drawing.Point(122, 77);
            this.te_NWX.Name = "te_NWX";
            this.te_NWX.Size = new System.Drawing.Size(310, 22);
            this.te_NWX.StyleController = this.layoutControl3;
            this.te_NWX.TabIndex = 6;
            // 
            // te_SWY
            // 
            this.te_SWY.Location = new System.Drawing.Point(122, 51);
            this.te_SWY.Name = "te_SWY";
            this.te_SWY.Size = new System.Drawing.Size(310, 22);
            this.te_SWY.StyleController = this.layoutControl3;
            this.te_SWY.TabIndex = 5;
            // 
            // te_SWX
            // 
            this.te_SWX.Location = new System.Drawing.Point(122, 25);
            this.te_SWX.Name = "te_SWX";
            this.te_SWX.Size = new System.Drawing.Size(310, 22);
            this.te_SWX.StyleController = this.layoutControl3;
            this.te_SWX.TabIndex = 4;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.CustomizationFormText = "layoutControlGroup8";
            this.layoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup8.GroupBordersVisible = false;
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup9});
            this.layoutControlGroup8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup8.Size = new System.Drawing.Size(437, 260);
            this.layoutControlGroup8.Text = "layoutControlGroup8";
            this.layoutControlGroup8.TextVisible = false;
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.CustomizationFormText = "layoutControlGroup9";
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem16,
            this.layoutControlItem17,
            this.layoutControlItem18,
            this.layoutControlItem19});
            this.layoutControlGroup9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup9.Size = new System.Drawing.Size(437, 260);
            this.layoutControlGroup9.Text = "图廓角点坐标信息";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 104);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(431, 130);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.te_SWX;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem16.Text = "西南图廓角点x坐标：";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(114, 14);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.te_SWY;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem17.Text = "西南图廓角点y坐标：";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(114, 14);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.te_NWX;
            this.layoutControlItem18.CustomizationFormText = "东北图廓角点x坐标：";
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem18.Text = "东北图廓角点x坐标：";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(114, 14);
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.te_NWY;
            this.layoutControlItem19.CustomizationFormText = "东北图廓角点y坐标：";
            this.layoutControlItem19.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem19.Text = "东北图廓角点y坐标：";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(114, 14);
            // 
            // tpProduceInfo
            // 
            this.tpProduceInfo.Controls.Add(this.layoutControl4);
            this.tpProduceInfo.Name = "tpProduceInfo";
            this.tpProduceInfo.Size = new System.Drawing.Size(437, 260);
            this.tpProduceInfo.Text = "生产信息";
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.te_UpdateDate);
            this.layoutControl4.Controls.Add(this.te_UpdateDept);
            this.layoutControl4.Controls.Add(this.te_UpdateMapper);
            this.layoutControl4.Controls.Add(this.te_UpdateReason);
            this.layoutControl4.Controls.Add(this.te_UpdateSurveyor);
            this.layoutControl4.Controls.Add(this.te_UpdateArea);
            this.layoutControl4.Controls.Add(this.te_CreateDept);
            this.layoutControl4.Controls.Add(this.te_MapCollate);
            this.layoutControl4.Controls.Add(this.te_Mapper);
            this.layoutControl4.Controls.Add(this.te_Checker);
            this.layoutControl4.Controls.Add(this.te_Surveyor);
            this.layoutControl4.Controls.Add(this.te_TechnologyPrincipal);
            this.layoutControl4.Controls.Add(this.te_ProjectPrincipal);
            this.layoutControl4.Controls.Add(this.te_CartoMode);
            this.layoutControl4.Controls.Add(this.te_SurveyDate);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup10;
            this.layoutControl4.Size = new System.Drawing.Size(437, 260);
            this.layoutControl4.TabIndex = 0;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // te_UpdateDate
            // 
            this.te_UpdateDate.Location = new System.Drawing.Point(312, 233);
            this.te_UpdateDate.Name = "te_UpdateDate";
            this.te_UpdateDate.Size = new System.Drawing.Size(120, 22);
            this.te_UpdateDate.StyleController = this.layoutControl4;
            this.te_UpdateDate.TabIndex = 18;
            // 
            // te_UpdateDept
            // 
            this.te_UpdateDept.Location = new System.Drawing.Point(92, 233);
            this.te_UpdateDept.Name = "te_UpdateDept";
            this.te_UpdateDept.Size = new System.Drawing.Size(129, 22);
            this.te_UpdateDept.StyleController = this.layoutControl4;
            this.te_UpdateDept.TabIndex = 17;
            // 
            // te_UpdateMapper
            // 
            this.te_UpdateMapper.Location = new System.Drawing.Point(313, 207);
            this.te_UpdateMapper.Name = "te_UpdateMapper";
            this.te_UpdateMapper.Size = new System.Drawing.Size(119, 22);
            this.te_UpdateMapper.StyleController = this.layoutControl4;
            this.te_UpdateMapper.TabIndex = 16;
            // 
            // te_UpdateReason
            // 
            this.te_UpdateReason.Location = new System.Drawing.Point(92, 207);
            this.te_UpdateReason.Name = "te_UpdateReason";
            this.te_UpdateReason.Size = new System.Drawing.Size(130, 22);
            this.te_UpdateReason.StyleController = this.layoutControl4;
            this.te_UpdateReason.TabIndex = 15;
            // 
            // te_UpdateSurveyor
            // 
            this.te_UpdateSurveyor.Location = new System.Drawing.Point(313, 181);
            this.te_UpdateSurveyor.Name = "te_UpdateSurveyor";
            this.te_UpdateSurveyor.Size = new System.Drawing.Size(119, 22);
            this.te_UpdateSurveyor.StyleController = this.layoutControl4;
            this.te_UpdateSurveyor.TabIndex = 14;
            // 
            // te_UpdateArea
            // 
            this.te_UpdateArea.Location = new System.Drawing.Point(92, 181);
            this.te_UpdateArea.Name = "te_UpdateArea";
            this.te_UpdateArea.Size = new System.Drawing.Size(130, 22);
            this.te_UpdateArea.StyleController = this.layoutControl4;
            this.te_UpdateArea.TabIndex = 13;
            // 
            // te_CreateDept
            // 
            this.te_CreateDept.Location = new System.Drawing.Point(92, 129);
            this.te_CreateDept.Name = "te_CreateDept";
            this.te_CreateDept.Size = new System.Drawing.Size(340, 22);
            this.te_CreateDept.StyleController = this.layoutControl4;
            this.te_CreateDept.TabIndex = 12;
            // 
            // te_MapCollate
            // 
            this.te_MapCollate.Location = new System.Drawing.Point(313, 103);
            this.te_MapCollate.Name = "te_MapCollate";
            this.te_MapCollate.Size = new System.Drawing.Size(119, 22);
            this.te_MapCollate.StyleController = this.layoutControl4;
            this.te_MapCollate.TabIndex = 11;
            // 
            // te_Mapper
            // 
            this.te_Mapper.Location = new System.Drawing.Point(92, 103);
            this.te_Mapper.Name = "te_Mapper";
            this.te_Mapper.Size = new System.Drawing.Size(130, 22);
            this.te_Mapper.StyleController = this.layoutControl4;
            this.te_Mapper.TabIndex = 10;
            // 
            // te_Checker
            // 
            this.te_Checker.Location = new System.Drawing.Point(313, 77);
            this.te_Checker.Name = "te_Checker";
            this.te_Checker.Size = new System.Drawing.Size(119, 22);
            this.te_Checker.StyleController = this.layoutControl4;
            this.te_Checker.TabIndex = 9;
            // 
            // te_Surveyor
            // 
            this.te_Surveyor.Location = new System.Drawing.Point(92, 77);
            this.te_Surveyor.Name = "te_Surveyor";
            this.te_Surveyor.Size = new System.Drawing.Size(130, 22);
            this.te_Surveyor.StyleController = this.layoutControl4;
            this.te_Surveyor.TabIndex = 8;
            // 
            // te_TechnologyPrincipal
            // 
            this.te_TechnologyPrincipal.Location = new System.Drawing.Point(313, 51);
            this.te_TechnologyPrincipal.Name = "te_TechnologyPrincipal";
            this.te_TechnologyPrincipal.Size = new System.Drawing.Size(119, 22);
            this.te_TechnologyPrincipal.StyleController = this.layoutControl4;
            this.te_TechnologyPrincipal.TabIndex = 7;
            // 
            // te_ProjectPrincipal
            // 
            this.te_ProjectPrincipal.Location = new System.Drawing.Point(92, 51);
            this.te_ProjectPrincipal.Name = "te_ProjectPrincipal";
            this.te_ProjectPrincipal.Size = new System.Drawing.Size(130, 22);
            this.te_ProjectPrincipal.StyleController = this.layoutControl4;
            this.te_ProjectPrincipal.TabIndex = 6;
            // 
            // te_CartoMode
            // 
            this.te_CartoMode.Location = new System.Drawing.Point(92, 25);
            this.te_CartoMode.Name = "te_CartoMode";
            this.te_CartoMode.Size = new System.Drawing.Size(130, 22);
            this.te_CartoMode.StyleController = this.layoutControl4;
            this.te_CartoMode.TabIndex = 4;
            // 
            // te_SurveyDate
            // 
            this.te_SurveyDate.Location = new System.Drawing.Point(313, 25);
            this.te_SurveyDate.Name = "te_SurveyDate";
            this.te_SurveyDate.Size = new System.Drawing.Size(119, 22);
            this.te_SurveyDate.StyleController = this.layoutControl4;
            this.te_SurveyDate.TabIndex = 5;
            // 
            // layoutControlGroup10
            // 
            this.layoutControlGroup10.CustomizationFormText = "layoutControlGroup10";
            this.layoutControlGroup10.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup10.GroupBordersVisible = false;
            this.layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup11,
            this.layoutControlGroup14});
            this.layoutControlGroup10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup10.Name = "layoutControlGroup10";
            this.layoutControlGroup10.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup10.Size = new System.Drawing.Size(437, 260);
            this.layoutControlGroup10.Text = "layoutControlGroup10";
            this.layoutControlGroup10.TextVisible = false;
            // 
            // layoutControlGroup11
            // 
            this.layoutControlGroup11.CustomizationFormText = "生产";
            this.layoutControlGroup11.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem20,
            this.layoutControlItem21,
            this.layoutControlItem22,
            this.layoutControlItem23,
            this.layoutControlItem24,
            this.layoutControlItem25,
            this.layoutControlItem26,
            this.layoutControlItem27,
            this.layoutControlItem28});
            this.layoutControlGroup11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup11.Name = "layoutControlGroup11";
            this.layoutControlGroup11.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup11.Size = new System.Drawing.Size(437, 156);
            this.layoutControlGroup11.Text = "生产";
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.te_CartoMode;
            this.layoutControlItem20.CustomizationFormText = "layoutControlItem20";
            this.layoutControlItem20.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem20.Text = "成图方式：";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.te_SurveyDate;
            this.layoutControlItem21.CustomizationFormText = "layoutControlItem21";
            this.layoutControlItem21.Location = new System.Drawing.Point(221, 0);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(210, 26);
            this.layoutControlItem21.Text = "测量日期：";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.te_ProjectPrincipal;
            this.layoutControlItem22.CustomizationFormText = "layoutControlItem22";
            this.layoutControlItem22.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem22.Text = "工程负责：";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.te_TechnologyPrincipal;
            this.layoutControlItem23.CustomizationFormText = "layoutControlItem23";
            this.layoutControlItem23.Location = new System.Drawing.Point(221, 26);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(210, 26);
            this.layoutControlItem23.Text = "技术负责：";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.te_Surveyor;
            this.layoutControlItem24.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem24.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem24.Text = "测图员：";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.te_Checker;
            this.layoutControlItem25.CustomizationFormText = "layoutControlItem25";
            this.layoutControlItem25.Location = new System.Drawing.Point(221, 52);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(210, 26);
            this.layoutControlItem25.Text = "检查员：";
            this.layoutControlItem25.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.te_Mapper;
            this.layoutControlItem26.CustomizationFormText = "layoutControlItem26";
            this.layoutControlItem26.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem26.Text = "制图员：";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.te_MapCollate;
            this.layoutControlItem27.CustomizationFormText = "layoutControlItem27";
            this.layoutControlItem27.Location = new System.Drawing.Point(221, 78);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(210, 26);
            this.layoutControlItem27.Text = "制图校对：";
            this.layoutControlItem27.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.te_CreateDept;
            this.layoutControlItem28.CustomizationFormText = "layoutControlItem28";
            this.layoutControlItem28.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem28.Text = "生产单位：";
            this.layoutControlItem28.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlGroup14
            // 
            this.layoutControlGroup14.CustomizationFormText = "更新";
            this.layoutControlGroup14.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem29,
            this.layoutControlItem30,
            this.layoutControlItem31,
            this.layoutControlItem32,
            this.layoutControlItem33,
            this.layoutControlItem34});
            this.layoutControlGroup14.Location = new System.Drawing.Point(0, 156);
            this.layoutControlGroup14.Name = "layoutControlGroup14";
            this.layoutControlGroup14.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup14.Size = new System.Drawing.Size(437, 104);
            this.layoutControlGroup14.Text = "更新";
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.te_UpdateArea;
            this.layoutControlItem29.CustomizationFormText = "layoutControlItem29";
            this.layoutControlItem29.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem29.Text = "更新区域：";
            this.layoutControlItem29.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem30
            // 
            this.layoutControlItem30.Control = this.te_UpdateSurveyor;
            this.layoutControlItem30.CustomizationFormText = "layoutControlItem30";
            this.layoutControlItem30.Location = new System.Drawing.Point(221, 0);
            this.layoutControlItem30.Name = "layoutControlItem30";
            this.layoutControlItem30.Size = new System.Drawing.Size(210, 26);
            this.layoutControlItem30.Text = "测图更新人员：";
            this.layoutControlItem30.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.Control = this.te_UpdateReason;
            this.layoutControlItem31.CustomizationFormText = "layoutControlItem31";
            this.layoutControlItem31.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem31.Text = "更新原因：";
            this.layoutControlItem31.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.Control = this.te_UpdateMapper;
            this.layoutControlItem32.CustomizationFormText = "layoutControlItem32";
            this.layoutControlItem32.Location = new System.Drawing.Point(221, 26);
            this.layoutControlItem32.Name = "layoutControlItem32";
            this.layoutControlItem32.Size = new System.Drawing.Size(210, 26);
            this.layoutControlItem32.Text = "制图更新人员：";
            this.layoutControlItem32.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem33
            // 
            this.layoutControlItem33.Control = this.te_UpdateDept;
            this.layoutControlItem33.CustomizationFormText = "layoutControlItem33";
            this.layoutControlItem33.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem33.Name = "layoutControlItem33";
            this.layoutControlItem33.Size = new System.Drawing.Size(220, 26);
            this.layoutControlItem33.Text = "更新单位：";
            this.layoutControlItem33.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem34
            // 
            this.layoutControlItem34.Control = this.te_UpdateDate;
            this.layoutControlItem34.CustomizationFormText = "更  新  时  间：";
            this.layoutControlItem34.Location = new System.Drawing.Point(220, 52);
            this.layoutControlItem34.Name = "layoutControlItem34";
            this.layoutControlItem34.Size = new System.Drawing.Size(211, 26);
            this.layoutControlItem34.Text = "更  新  时  间：";
            this.layoutControlItem34.TextSize = new System.Drawing.Size(84, 14);
            // 
            // tpQualityInfo
            // 
            this.tpQualityInfo.Controls.Add(this.layoutControl5);
            this.tpQualityInfo.Name = "tpQualityInfo";
            this.tpQualityInfo.Size = new System.Drawing.Size(437, 260);
            this.tpQualityInfo.Text = "质量信息";
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.te_QualityExamDept);
            this.layoutControl5.Controls.Add(this.te_QualityJudge);
            this.layoutControl5.Controls.Add(this.te_AttMSE);
            this.layoutControl5.Controls.Add(this.te_PMSE);
            this.layoutControl5.Controls.Add(this.te_HMSE);
            this.layoutControl5.Controls.Add(this.te_QualityDate);
            this.layoutControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl5.Location = new System.Drawing.Point(0, 0);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.Root = this.layoutControlGroup12;
            this.layoutControl5.Size = new System.Drawing.Size(437, 260);
            this.layoutControl5.TabIndex = 0;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // te_QualityExamDept
            // 
            this.te_QualityExamDept.Location = new System.Drawing.Point(92, 129);
            this.te_QualityExamDept.Name = "te_QualityExamDept";
            this.te_QualityExamDept.Size = new System.Drawing.Size(340, 22);
            this.te_QualityExamDept.StyleController = this.layoutControl5;
            this.te_QualityExamDept.TabIndex = 8;
            // 
            // te_QualityJudge
            // 
            this.te_QualityJudge.Location = new System.Drawing.Point(92, 103);
            this.te_QualityJudge.Name = "te_QualityJudge";
            this.te_QualityJudge.Size = new System.Drawing.Size(340, 22);
            this.te_QualityJudge.StyleController = this.layoutControl5;
            this.te_QualityJudge.TabIndex = 7;
            // 
            // te_AttMSE
            // 
            this.te_AttMSE.EditValue = "0";
            this.te_AttMSE.Location = new System.Drawing.Point(92, 77);
            this.te_AttMSE.Name = "te_AttMSE";
            this.te_AttMSE.Size = new System.Drawing.Size(340, 22);
            this.te_AttMSE.StyleController = this.layoutControl5;
            this.te_AttMSE.TabIndex = 6;
            // 
            // te_PMSE
            // 
            this.te_PMSE.EditValue = "0";
            this.te_PMSE.Location = new System.Drawing.Point(92, 51);
            this.te_PMSE.Name = "te_PMSE";
            this.te_PMSE.Size = new System.Drawing.Size(340, 22);
            this.te_PMSE.StyleController = this.layoutControl5;
            this.te_PMSE.TabIndex = 5;
            // 
            // te_HMSE
            // 
            this.te_HMSE.EditValue = "0";
            this.te_HMSE.Location = new System.Drawing.Point(92, 25);
            this.te_HMSE.Name = "te_HMSE";
            this.te_HMSE.Size = new System.Drawing.Size(340, 22);
            this.te_HMSE.StyleController = this.layoutControl5;
            this.te_HMSE.TabIndex = 4;
            // 
            // te_QualityDate
            // 
            this.te_QualityDate.Location = new System.Drawing.Point(92, 155);
            this.te_QualityDate.Name = "te_QualityDate";
            this.te_QualityDate.Size = new System.Drawing.Size(340, 22);
            this.te_QualityDate.StyleController = this.layoutControl5;
            this.te_QualityDate.TabIndex = 9;
            // 
            // layoutControlGroup12
            // 
            this.layoutControlGroup12.CustomizationFormText = "layoutControlGroup12";
            this.layoutControlGroup12.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup12.GroupBordersVisible = false;
            this.layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup13});
            this.layoutControlGroup12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup12.Name = "layoutControlGroup12";
            this.layoutControlGroup12.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup12.Size = new System.Drawing.Size(437, 260);
            this.layoutControlGroup12.Text = "layoutControlGroup12";
            this.layoutControlGroup12.TextVisible = false;
            // 
            // layoutControlGroup13
            // 
            this.layoutControlGroup13.CustomizationFormText = "精度及质量评价";
            this.layoutControlGroup13.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem6,
            this.layoutControlItem35,
            this.layoutControlItem36,
            this.layoutControlItem37,
            this.layoutControlItem38,
            this.layoutControlItem39,
            this.layoutControlItem40});
            this.layoutControlGroup13.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup13.Name = "layoutControlGroup13";
            this.layoutControlGroup13.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup13.Size = new System.Drawing.Size(437, 260);
            this.layoutControlGroup13.Text = "精度及质量评价";
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem6";
            this.emptySpaceItem6.Location = new System.Drawing.Point(0, 156);
            this.emptySpaceItem6.MaxSize = new System.Drawing.Size(431, 50);
            this.emptySpaceItem6.MinSize = new System.Drawing.Size(431, 50);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(431, 78);
            this.emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem6.Text = "emptySpaceItem6";
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem35
            // 
            this.layoutControlItem35.Control = this.te_HMSE;
            this.layoutControlItem35.CustomizationFormText = "数据高程精度：";
            this.layoutControlItem35.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem35.Name = "layoutControlItem35";
            this.layoutControlItem35.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem35.Text = "数据高程精度：";
            this.layoutControlItem35.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem36
            // 
            this.layoutControlItem36.Control = this.te_PMSE;
            this.layoutControlItem36.CustomizationFormText = "数据平面精度：";
            this.layoutControlItem36.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem36.Name = "layoutControlItem36";
            this.layoutControlItem36.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem36.Text = "数据平面精度：";
            this.layoutControlItem36.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem37
            // 
            this.layoutControlItem37.Control = this.te_AttMSE;
            this.layoutControlItem37.CustomizationFormText = "数据属性精度：";
            this.layoutControlItem37.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem37.Name = "layoutControlItem37";
            this.layoutControlItem37.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem37.Text = "数据属性精度：";
            this.layoutControlItem37.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem38
            // 
            this.layoutControlItem38.Control = this.te_QualityJudge;
            this.layoutControlItem38.CustomizationFormText = "数据质量评价：";
            this.layoutControlItem38.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem38.Name = "layoutControlItem38";
            this.layoutControlItem38.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem38.Text = "数据质量评价：";
            this.layoutControlItem38.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem39
            // 
            this.layoutControlItem39.Control = this.te_QualityExamDept;
            this.layoutControlItem39.CustomizationFormText = "质量评检单位：";
            this.layoutControlItem39.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem39.Name = "layoutControlItem39";
            this.layoutControlItem39.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem39.Text = "质量评检单位：";
            this.layoutControlItem39.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem40
            // 
            this.layoutControlItem40.Control = this.te_QualityDate;
            this.layoutControlItem40.CustomizationFormText = "质量评检日期：";
            this.layoutControlItem40.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItem40.Name = "layoutControlItem40";
            this.layoutControlItem40.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem40.Text = "质量评检日期：";
            this.layoutControlItem40.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup6,
            this.layoutControlGroup7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(613, 331);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(160, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(453, 299);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.xtraTabControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(447, 293);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "layoutControlGroup6";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(160, 331);
            this.layoutControlGroup6.Text = "layoutControlGroup6";
            this.layoutControlGroup6.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.treeList1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(154, 325);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.CustomizationFormText = "layoutControlGroup7";
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem4});
            this.layoutControlGroup7.Location = new System.Drawing.Point(160, 299);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Size = new System.Drawing.Size(453, 32);
            this.layoutControlGroup7.Text = "layoutControlGroup7";
            this.layoutControlGroup7.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btn_Save;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(259, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(92, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_OK;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(351, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(96, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(259, 26);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "添加图幅";
            this.barButtonItem2.Id = 12;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "删除图幅";
            this.barButtonItem3.Id = 13;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "图幅定位";
            this.barButtonItem4.Id = 14;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "图幅裁切";
            this.barButtonItem5.Id = 15;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem1,
            this.barSubItem2,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem6,
            this.barButtonItem7,
            this.barButtonItem8,
            this.barButtonItem9});
            this.barManager1.MaxItemId = 20;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(613, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 331);
            this.barDockControlBottom.Size = new System.Drawing.Size(613, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 331);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(613, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 331);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "添加图幅";
            this.barSubItem1.Id = 9;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "删除图幅";
            this.barSubItem2.Id = 10;
            this.barSubItem2.Name = "barSubItem2";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "添加图幅";
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "图幅导出(dxf)";
            this.barButtonItem6.Id = 16;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "添加图幅";
            this.barButtonItem7.Id = 17;
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem7_ItemClick);
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "测区定位";
            this.barButtonItem8.Id = 18;
            this.barButtonItem8.Name = "barButtonItem8";
            this.barButtonItem8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem8_ItemClick);
            // 
            // barButtonItem9
            // 
            this.barButtonItem9.Caption = "测区地图裁切";
            this.barButtonItem9.Id = 19;
            this.barButtonItem9.Name = "barButtonItem9";
            this.barButtonItem9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem9_ItemClick);
            // 
            // popupMenu2
            // 
            this.popupMenu2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem7),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem8),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem9)});
            this.popupMenu2.Manager = this.barManager1;
            this.popupMenu2.Name = "popupMenu2";
            // 
            // FrmMetaDataManagement
            // 
            this.ClientSize = new System.Drawing.Size(613, 331);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmMetaDataManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "元数据管理";
            this.Load += new System.EventHandler(this.FrmMetaDataManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_ClassDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_SurveyDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_SYMDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_MapNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Maptitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_DisName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_Scale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_Contour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_PSYS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_HSYS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_Secrecy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            this.tpSheetCoord.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_NWY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_NWX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_SWY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_SWX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            this.tpProduceInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateMapper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateSurveyor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_UpdateArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_CreateDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_MapCollate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Mapper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Checker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Surveyor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_TechnologyPrincipal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ProjectPrincipal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_CartoMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_SurveyDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).EndInit();
            this.tpQualityInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_QualityExamDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_QualityJudge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_AttMSE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_PMSE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_HMSE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_QualityDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
            this.ResumeLayout(false);

        }

        private void FrmMetaDataManagement_Load(object sender, EventArgs e)
        {
           try
           {
               Init();
               InitTreeList();           
               InitItemsInComboBox(secrecy, cb_Secrecy);
               InitItemsInComboBox(scale, cb_Scale);
               InitItemsInComboBox(contour, cb_Contour);
               InitItemsInComboBox(pSYS, cb_PSYS);
               InitItemsInComboBox(hSYS, cb_HSYS);

             
           }
           catch (System.Exception ex)
           {
           	
           }
        }
        private void InitTreeList()
        {
            string mapTitle;
            this.cb_DisName.Properties.Items.Clear();
            this.treeList1.ClearNodes();
            foreach (string dis in dictFC.Keys)
            {
                TreeListNode treeNode = treeList1.AppendNode(new object[] { dis, dictFC[dis] }, null);
                this.cb_DisName.Properties.Items.Add(dis);
                foreach (IFeature feature in dictFC[dis])
                {
                    mapTitle = GetFieldValueByIndex(feature, indexMapTitle).ToString();
                    treeList1.AppendNode(new object[] { mapTitle, feature }, treeNode);
                }


            }
            this.cb_DisName.SelectedIndex = 0;
        }
        private void InitItemsInComboBox(HashSet<string> hs, ComboBoxEdit cb)
        {
            cb.Properties.Items.Clear();
            foreach (string s in hs)
            {
                cb.Properties.Items.Add(s);
            }
            cb.SelectedIndex = 0;
        }

        private void InitIndex()
        {
            indexDisName = GetIndexBySystemName("DisName");
            indexSecrecy = GetIndexBySystemName("Secrecy");
            indexScale = GetIndexBySystemName("Scale");
            indexContour = GetIndexBySystemName("Contour");
            indexPSYS = GetIndexBySystemName("PSYS");
            indexHSYS = GetIndexBySystemName("HSYS");
            indexSYMDesc = GetIndexBySystemName("SYMDesc");
            indexClassDesc = GetIndexBySystemName("ClassDesc");
            indexSurveyDesc = GetIndexBySystemName("SurveyDesc");
            indexSWY = GetIndexBySystemName("SWY");
            indexSWX = GetIndexBySystemName("SWX");
            indexNWY = GetIndexBySystemName("NWY");
            indexNWX = GetIndexBySystemName("NWX");
            indexCarto = GetIndexBySystemName("Carto");
            indexSurveyor = GetIndexBySystemName("Surveyor");
            indexCreateDept = GetIndexBySystemName("CreateDept");
            indexMapper = GetIndexBySystemName("Mapper");
            indexMapcollate = GetIndexBySystemName("Mapcollate");
            indexCreateDate = GetIndexBySystemName("CreateDate");
            indexProjectPrincipal = GetIndexBySystemName("ProjectPrincipal");
            indexTechnologyPrincipal = GetIndexBySystemName("TechnologyPrincipal");
            indexChecker = GetIndexBySystemName("Checker");
            indexUpdater = GetIndexBySystemName("Updater");
            indexUpdateArea = GetIndexBySystemName("UpdateArea");
            indexUpdateReason = GetIndexBySystemName("UpdateReason");
            indexUpdateMapper = GetIndexBySystemName("UpdateMapper");
            indexUpdateDept = GetIndexBySystemName("UpdateDept");
            indexUpdateDate = GetIndexBySystemName("UpdateDate");
            indexHMSE = GetIndexBySystemName("HMSE");
            indexPMSE = GetIndexBySystemName("PMSE");
            indexQualityJudge = GetIndexBySystemName("QualityJudge");
            indexQualityExamDept = GetIndexBySystemName("QualityExamDept");
            indexAttMSE = GetIndexBySystemName("AttMSE");
            indexQualityDate = GetIndexBySystemName("QualityDate");
            indexMapTitle = GetIndexBySystemName("Maptitle");
            indexMapNO = GetIndexBySystemName("MapNO");
            indexGEOOBJNUM = GetIndexBySystemName("Geoobjnum");
        }

        private void Init()
        {
            try
            {
                facilityClass = FacilityClassManager.Instance.GetFacilityClassByName("MeteData");
                if (facilityClass == null) return;

                string[] fc2D = facilityClass.Fc2D.Split(';');
                if (fc2D.Length == 0) return;
                string fcID = fc2D[0];
                DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fcID);
                if (dffc == null) return;
                fc = dffc.GetFeatureClass();
                if (fc == null) return;
                InitIndex();
                IFeatureCursor cursor = fc.Search(null, false);
                IFeature feature;
                string district = null;
                string secrecy = null;
                string scale = null;
                string contour = null;
                string pSYS = null;
                string hSYS = null;
                dictFC = new Dictionary<string, List<IFeature>>();
                this.secrecy = new HashSet<string>();
                this.scale = new HashSet<string>();
                this.contour = new HashSet<string>();
                this.pSYS = new HashSet<string>();
                this.hSYS = new HashSet<string>();
                while ((feature = cursor.NextFeature()) != null)
                {
                    district = GetFieldValueByIndex(feature, indexDisName).ToString();
                               
                    if (dictFC.ContainsKey(district))
                    {
                        dictFC[district].Add(feature);
                    }
                    else
                    {
                        dictFC[district] = new List<IFeature>();
                        dictFC[district].Add(feature);
                    }

                    secrecy = GetFieldValueByIndex(feature, indexSecrecy).ToString();
                    this.secrecy.Add(secrecy);
                    scale = GetFieldValueByIndex(feature, indexScale).ToString();
                    this.scale.Add(scale);
                    contour = GetFieldValueByIndex(feature, indexContour).ToString();
                    this.contour.Add(contour);
                    pSYS = GetFieldValueByIndex(feature, indexPSYS).ToString();
                    this.pSYS.Add(pSYS);
                    hSYS = GetFieldValueByIndex(feature, indexHSYS).ToString();
                    this.hSYS.Add(hSYS);

                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        private int GetIndexBySystemName(string sysName)
        {
            DFDataConfig.Class.FieldInfo fi = facilityClass.GetFieldInfoBySystemName(sysName);
            if (fi == null) return -1;
            int index = fc.Fields.FindField(fi.Name);
            return index;
        }
        private object GetFieldValueByIndex(IFeature feature, int index)
        {
            object obj = null;
            if (index == -1) return null;
            IDomain domain = feature.Fields.get_Field(index).Domain;
            if (domain != null && domain.Type == esriDomainType.esriDTCodedValue)
            {
                ICodedValueDomain pCD = domain as ICodedValueDomain;
                for (int i = 0; i < pCD.CodeCount; i++)
                {
                    if (object.Equals(pCD.get_Value(i), feature.get_Value(index)))
                    {
                        obj = pCD.get_Name(i);
                    }
                }
            }
            else
            {
                obj = feature.get_Value(index).ToString();
            }
            return obj;    
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            app.Current2DMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
            app.Current2DMapControl.ActiveView.Refresh();
            this.Close();

        }

      

        private void treeList1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                TreeListNode treeNode = this.treeList1.FocusedNode;
                if (treeNode == null || treeNode.ParentNode == null) return;
                object obj = treeNode.GetValue("Object");
                if (obj is IFeature)
                {
                    IFeature feature = obj as IFeature;
                    this.cb_DisName.Text = GetFieldValueByIndex(feature, indexDisName).ToString();
                    this.cb_Secrecy.Text = GetFieldValueByIndex(feature, indexSecrecy).ToString();
                    this.cb_Scale.Text = GetFieldValueByIndex(feature, indexScale).ToString();
                    this.cb_HSYS.Text = GetFieldValueByIndex(feature, indexHSYS).ToString();
                    this.cb_PSYS.Text = GetFieldValueByIndex(feature, indexPSYS).ToString();
                    this.cb_Contour.Text = GetFieldValueByIndex(feature, indexContour).ToString();
                    this.te_Maptitle.Text = GetFieldValueByIndex(feature, indexMapTitle).ToString();
                    this.te_MapNum.Text = GetFieldValueByIndex(feature, indexMapNO).ToString();
                    this.te_SYMDesc.Text = GetFieldValueByIndex(feature, indexSYMDesc).ToString();
                    this.te_SurveyDesc.Text = GetFieldValueByIndex(feature, indexSurveyDesc).ToString();
                    this.te_ClassDesc.Text = GetFieldValueByIndex(feature, indexClassDesc).ToString();

                    this.te_SWX.Text = GetFieldValueByIndex(feature, indexSWX).ToString();
                    this.te_SWY.Text = GetFieldValueByIndex(feature, indexSWY).ToString();
                    this.te_NWX.Text = GetFieldValueByIndex(feature, indexNWX).ToString();
                    this.te_NWY.Text = GetFieldValueByIndex(feature, indexNWY).ToString();

                    this.te_CartoMode.Text = GetFieldValueByIndex(feature, indexCarto).ToString();
                    this.te_SurveyDate.Text = GetFieldValueByIndex(feature, indexCreateDate).ToString();
                    this.te_ProjectPrincipal.Text = GetFieldValueByIndex(feature, indexProjectPrincipal).ToString();
                    this.te_TechnologyPrincipal.Text = GetFieldValueByIndex(feature, indexTechnologyPrincipal).ToString();
                    this.te_Surveyor.Text = GetFieldValueByIndex(feature, indexSurveyor).ToString();
                    this.te_Checker.Text = GetFieldValueByIndex(feature, indexChecker).ToString();
                    this.te_Mapper.Text = GetFieldValueByIndex(feature, indexMapper).ToString();
                    this.te_MapCollate.Text = GetFieldValueByIndex(feature, indexMapcollate).ToString();
                    this.te_CreateDept.Text = GetFieldValueByIndex(feature, indexCreateDept).ToString();
                    this.te_UpdateArea.Text = GetFieldValueByIndex(feature, indexUpdateArea).ToString();
                    this.te_UpdateSurveyor.Text = GetFieldValueByIndex(feature, indexUpdater).ToString();
                    this.te_UpdateMapper.Text = GetFieldValueByIndex(feature, indexUpdateMapper).ToString();
                    this.te_UpdateReason.Text = GetFieldValueByIndex(feature, indexUpdateReason).ToString();
                    this.te_UpdateDept.Text = GetFieldValueByIndex(feature, indexUpdateDept).ToString();
                    this.te_UpdateDate.Text = GetFieldValueByIndex(feature, indexUpdateDate).ToString();

                    this.te_HMSE.Text = GetFieldValueByIndex(feature, indexHMSE).ToString();
                    this.te_PMSE.Text = GetFieldValueByIndex(feature, indexPMSE).ToString();
                    this.te_QualityExamDept.Text = GetFieldValueByIndex(feature, indexQualityExamDept).ToString();
                    this.te_AttMSE.Text = GetFieldValueByIndex(feature, indexAttMSE).ToString();
                    this.te_QualityJudge.Text = GetFieldValueByIndex(feature, indexQualityJudge).ToString();
                    this.te_QualityDate.Text = GetFieldValueByIndex(feature, indexQualityDate).ToString();

                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && treeList1.State == TreeListState.Regular)
            {
                System.Drawing.Point p = new System.Drawing.Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
                TreeListHitInfo hitInfo = treeList1.CalcHitInfo(e.Location);
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    treeList1.SetFocusedNode(hitInfo.Node);
                }
                if (treeList1.FocusedNode != null)
                {
                    if (treeList1.FocusedNode.ParentNode == null)
                    {
                        popupMenu2.ShowPopup(p);
                    }
                    else
                    {
                        popupMenu1.ShowPopup(p);
                    }
                    
                }
            }
        }

        //添加图幅
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                TreeListNode treeNode = this.treeList1.FocusedNode;
                string disName;
                if (treeNode.ParentNode == null)
                {


                }
                else
                {
                    disName = treeNode.ParentNode.GetValue("Sheet").ToString();
                    FrmAddSheet frmAddSheet = new FrmAddSheet();
                    frmAddSheet.SetPara(ref dictFC, scale, disName, fc, indexDisName, indexScale, indexMapTitle, indexMapNO, indexNWX, indexNWY, indexSWX, indexSWY, indexGEOOBJNUM);
                    frmAddSheet.ShowDialog();
                    if (frmAddSheet.DialogResult == DialogResult.OK)
                    {
                        InitTreeList();
                    }
                    XtraMessageBox.Show("添加图幅成功", "提示");
                }
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show("添加图幅失败", "提示");
            }
           
        }
        //删除图幅      
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                TreeListNode treeNode = treeList1.FocusedNode;
                if (treeNode.ParentNode == null)
                {
                    return;
                }
                else
                {
                    string disName = treeNode.ParentNode.GetValue("Sheet").ToString();
                    string maptitle = treeNode.GetValue("Sheet").ToString();
                    IQueryFilter filter = new QueryFilter();
                    filter.WhereClause = "Maptitle = '" + maptitle + "'";
                    IFeatureCursor cursor = fc.Search(filter, false);
                    IFeature feature = cursor.NextFeature();
                    if (feature != null) 
                    {
                        if (dictFC.ContainsKey(disName))
                        {
                            dictFC[disName].Remove(feature);
                        }
                        feature.Delete();
                
                    }
                    InitTreeList();                
                    XtraMessageBox.Show("删除图幅成功", "提示");
                }
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show("删除图幅失败", "提示");
            }
        }
        //图幅定位
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                TreeListNode treeNode = treeList1.FocusedNode;
                if (treeNode == null || treeNode.ParentNode == null) return;
                else
                {
                    string maptitle = treeNode.GetValue("Sheet").ToString();
                    IQueryFilter filter = new QueryFilter();
                    filter.WhereClause = "Maptitle = '" + maptitle + "'";
                    IFeatureCursor cursor = fc.Search(filter, true);
                    IFeature feature = cursor.NextFeature();
                    if (feature == null) return;                 
                    double swx = Convert.ToDouble(feature.get_Value(indexSWX));
                    double swy = Convert.ToDouble(feature.get_Value(indexSWY));
                    double nwx = Convert.ToDouble(feature.get_Value(indexNWX));
                    double nwy = Convert.ToDouble(feature.get_Value(indexNWY));

                    double width = Math.Abs(swy - nwy);
                    double length = Math.Abs(swx - nwx);


                    IPointCollection pCol = new PolygonClass();
                    IPoint p1 = new PointClass();
                    p1.PutCoords(swx, swy);
                    pCol.AddPoint(p1);
                    IPoint p2 = new PointClass();
                    p2.PutCoords(nwx, swy);
                    pCol.AddPoint(p2);
                    IPoint p3 = new PointClass();
                    p3.PutCoords(nwx, nwy);
                    pCol.AddPoint(p3);
                    IPoint p4 = new PointClass();
                    p4.PutCoords(swx, nwy);
                    pCol.AddPoint(p4);
                    pCol.AddPoint(p1);
                    IPoint pCenter = new PointClass();
                    pCenter.PutCoords((swx + nwx) / 2, (swy + nwy) / 2);
                    IGeometry geo = pCol as IGeometry;
                    if (app == null || app.Current2DMapControl == null) return;
                    IGraphicsContainer gc = app.Current2DMapControl.ActiveView.GraphicsContainer;
                    gc.DeleteAllElements();
                    AddElement(geo, gc);
                    app.Current2DMapControl.MapScale = 2000;
                    app.Current2DMapControl.CenterAt(pCenter);
                    app.Current2DMapControl.ActiveView.Refresh();
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
        }


        private void AddElement(IGeometry geo, IGraphicsContainer gc)
        {
            IElement pElem = new RectangleElement();
            pElem.Geometry = geo;

            ISimpleFillSymbol pSFSym = new SimpleFillSymbol();
            Color color = ColorTranslator.FromHtml(SystemInfo.Instance.FillColor);
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            pSFSym.Color = pColor;
            pSFSym.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;

            ISimpleLineSymbol pSLSym = new SimpleLineSymbol();
            pSLSym.Style = esriSimpleLineStyle.esriSLSSolid;
            pSLSym.Width = 1;
            pSLSym.Color = pColor;

            pSFSym.Outline = pSLSym;

            IFillShapeElement pElemFillShp = pElem as IFillShapeElement;
            pElemFillShp.Symbol = pSFSym;
            gc.AddElement(pElem, 0);
        }
        //图幅裁切
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                TreeListNode treeNode = treeList1.FocusedNode;
                if (treeNode.ParentNode == null || treeNode == null) return;
                else
                {
                    object obj = treeNode.GetValue("Object");
                    if (obj is IFeature)
                    {
                        IFeature feature = obj as IFeature;
                        double swx = Convert.ToDouble(feature.get_Value(indexSWX));
                        double swy = Convert.ToDouble(feature.get_Value(indexSWY));
                        double nwx = Convert.ToDouble(feature.get_Value(indexNWX));
                        double nwy = Convert.ToDouble(feature.get_Value(indexNWY));

                        IPointCollection pCol = new PolygonClass();
                        IPoint p1 = new PointClass();
                        p1.PutCoords(swx, swy);
                        pCol.AddPoint(p1);
                        IPoint p2 = new PointClass();
                        p2.PutCoords(nwx, swy);
                        pCol.AddPoint(p2);
                        IPoint p3 = new PointClass();
                        p3.PutCoords(nwx, nwy);
                        pCol.AddPoint(p3);
                        IPoint p4 = new PointClass();
                        p4.PutCoords(swx, nwy);
                        pCol.AddPoint(p4);
                        pCol.AddPoint(p1);

                        IGeometry geo = pCol as IGeometry;
                        ISelectionEnvironment selEnv = new SelectionEnvironmentClass();
                        app.Current2DMapControl.Map.SelectByShape(geo, selEnv,false);
                        ISelection pSelection = app.Current2DMapControl.Map.FeatureSelection;
                        FrmDxfExport dialog = new FrmDxfExport(pSelection, app.Current2DMapControl.Map);
                        dialog.ShowDialog();
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        //测区添加图幅
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                TreeListNode treeNode = this.treeList1.FocusedNode;
                string disName;
                if (treeNode.ParentNode == null)
                {
                    disName = treeNode.GetValue("Sheet").ToString();
                    FrmAddSheet frmAddSheet = new FrmAddSheet();
                    frmAddSheet.SetPara(ref dictFC, scale, disName, fc, indexDisName, indexScale, indexMapTitle, indexMapNO, indexNWX, indexNWY, indexSWX, indexSWY, indexGEOOBJNUM);
                    frmAddSheet.ShowDialog();
                    if (frmAddSheet.DialogResult == DialogResult.OK)
                    {
                        InitTreeList();
                    }

                }

            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show("添加图幅失败", "提示");
            }
        }
        //测区定位
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FacilityClass facc = FacilityClassManager.Instance.GetFacilityClassByName("District");
                if (facc == null) return;
                string[] fc2D = facc.Fc2D.Split(';');
                string fcID = fc2D[0];
                IFeatureClass district = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fcID).GetFeatureClass();
                if (district == null) return;
                TreeListNode treeNode = treeList1.FocusedNode;
                if (treeNode.ParentNode == null)
                {
                    string disName = treeNode.GetValue("Sheet").ToString();
                    IQueryFilter filter = new QueryFilter();
                    filter.WhereClause = "Name = '" + disName + "'";
                    IFeatureCursor cursor = district.Search(filter, false);
                    IFeature feature = cursor.NextFeature();
                    if (feature == null) return;
                    IGeometry geo = feature.Shape;
                    IEnvelope env = geo.Envelope;
                    IPoint pCenter = new PointClass();
                    pCenter.PutCoords((env.XMax + env.XMin) / 2, (env.YMax + env.YMin) / 2);
                    if (app == null || app.Current2DMapControl == null) return;
                    IGraphicsContainer gc = app.Current2DMapControl.ActiveView.GraphicsContainer;
                    gc.DeleteAllElements();
                    AddElement(geo, gc);
                    app.Current2DMapControl.MapScale = 10000;
                    app.Current2DMapControl.CenterAt(pCenter);
                    app.Current2DMapControl.ActiveView.Refresh();

                }
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }
        //测区地图裁切
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FacilityClass facc = FacilityClassManager.Instance.GetFacilityClassByName("District");
                if (facc == null) return;
                string[] fc2D = facc.Fc2D.Split(';');
                string fcID = fc2D[0];
                IFeatureClass district = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fcID).GetFeatureClass();
                if (district == null) return;
                TreeListNode treeNode = treeList1.FocusedNode;
                if (treeNode.ParentNode == null)
                {
                    string disName = treeNode.GetValue("Sheet").ToString();
                    IQueryFilter filter = new QueryFilter();
                    filter.WhereClause = "Name = '" + disName + "'";
                    IFeatureCursor cursor = district.Search(filter, false);
                    IFeature feature = cursor.NextFeature();
                    if (feature == null) return;
                    IGeometry geo = feature.Shape;
                    if (app == null || app.Current2DMapControl == null) return;
                    ISelectionEnvironment selEnv = new SelectionEnvironmentClass();
                    app.Current2DMapControl.Map.SelectByShape(geo, selEnv, false);
                    ISelection pSelection = app.Current2DMapControl.Map.FeatureSelection;
                    FrmDxfExport dialog = new FrmDxfExport(pSelection, app.Current2DMapControl.Map);
                    dialog.ShowDialog();

                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }
       
    }
}
