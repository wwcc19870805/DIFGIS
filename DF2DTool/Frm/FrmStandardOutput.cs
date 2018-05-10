using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;
using DF2DTool.Class;

namespace DF2DTool.Frm
{
    public partial class FrmStandardOutput : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit teIsoDistance;
        private DevExpress.XtraEditors.ComboBoxEdit cbScaleRuler;
        private DevExpress.XtraEditors.TextEdit teHeight;
        private DevExpress.XtraEditors.TextEdit teMapNumber;
        private DevExpress.XtraEditors.TextEdit teCoord;
        private DevExpress.XtraEditors.TextEdit teMapName;
        private DevExpress.XtraEditors.ComboBoxEdit cbMapTitle;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraEditors.TextEdit teMapchecker;
        private DevExpress.XtraEditors.TextEdit teMapdrawer;
        private DevExpress.XtraEditors.TextEdit teSurveyor;
        private DevExpress.XtraEditors.TextEdit teProductUnit;
        private DevExpress.XtraEditors.TextEdit teMakemapMethod;
        private DevExpress.XtraEditors.TextEdit teYOrig;
        private DevExpress.XtraEditors.TextEdit teXOrig;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit teYPrefix;
        private DevExpress.XtraEditors.TextEdit teXPrefix;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit teMarginDown;
        private DevExpress.XtraEditors.TextEdit teMarginLeft;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraEditors.TextEdit teAddress;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.TextEdit teJoin8;
        private DevExpress.XtraEditors.TextEdit teJoin7;
        private DevExpress.XtraEditors.TextEdit teJoin6;
        private DevExpress.XtraEditors.TextEdit teJoin5;
        private DevExpress.XtraEditors.TextEdit teJoin4;
        private DevExpress.XtraEditors.TextEdit teJoin3;
        private DevExpress.XtraEditors.TextEdit teJoin2;
        private DevExpress.XtraEditors.TextEdit teJoin1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem34;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem35;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem36;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem37;
        private DevExpress.XtraEditors.TextEdit textEdit25;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private string strAccessName;
        private ESRI.ArcGIS.Geodatabase.IFeatureWorkspace m_pFWorkspace;
        private ESRI.ArcGIS.Geodatabase.IWorkspace m_pWorkspace;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnLoadData;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem38;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem39;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem40;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private LabelControl labelControl14;
        private LabelControl labelControl13;
        private LabelControl labelControl12;
        private LabelControl labelControl11;
        private LabelControl labelControl10;
        private LabelControl labelControl9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem41;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem42;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem43;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem44;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem45;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem46;
        private LabelControl labelControl19;
        private LabelControl labelControl18;
        private LabelControl labelControl17;
        private LabelControl labelControl16;
        private LabelControl labelControl15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem47;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem48;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem49;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem50;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem51;
        public MapCode mapCode = new MapCode();

        public string XPrefix
        {
            get
            {
                return this.teXPrefix.Text;
            }
        }
        public string YPrefix
        {
            get
            {
                return this.teYPrefix.Text;
            }
        }

        public string Coord
        {
            get { return this.teCoord.Text; }
        }
        public string Height
        {
            get { return this.teHeight.Text; }
        }
        public string IsoDistance
        {
            get { return this.teIsoDistance.Text; }
        }
        public string MapNumber
        {
            get { return this.teMapNumber.Text; }
        }
        public string MapChecker
        {
            get { return this.teMapchecker.Text; }

        }
        public string MapName
        {
            get { return this.teMapName.Text; }
        }
        public string MakemapMethod
        {
            get { return this.teMakemapMethod.Text; }
        }
        public string MapDrawer
        {
            get { return this.teMapdrawer.Text; }
        }
        public string Surveyor
        {
            get { return this.teSurveyor.Text; }
        }
        public string ProductUnit
        {
            get { return this.teProductUnit.Text; }
        }
        public RadioGroup RadioGroup
        {
            get { return this.radioGroup1; }
        }
        public string OrigX
        {
            get { return this.teXOrig.Text; }
        }
        public string OrigY
        {
            get { return this.teYOrig.Text; }
        }
    
        public FrmStandardOutput()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadData = new DevExpress.XtraEditors.SimpleButton();
            this.teAddress = new DevExpress.XtraEditors.TextEdit();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.teJoin8 = new DevExpress.XtraEditors.TextEdit();
            this.teJoin7 = new DevExpress.XtraEditors.TextEdit();
            this.teJoin6 = new DevExpress.XtraEditors.TextEdit();
            this.teJoin5 = new DevExpress.XtraEditors.TextEdit();
            this.teJoin4 = new DevExpress.XtraEditors.TextEdit();
            this.teJoin3 = new DevExpress.XtraEditors.TextEdit();
            this.teJoin2 = new DevExpress.XtraEditors.TextEdit();
            this.teJoin1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.teMarginDown = new DevExpress.XtraEditors.TextEdit();
            this.teMarginLeft = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.teYPrefix = new DevExpress.XtraEditors.TextEdit();
            this.teXPrefix = new DevExpress.XtraEditors.TextEdit();
            this.teMapchecker = new DevExpress.XtraEditors.TextEdit();
            this.teMapdrawer = new DevExpress.XtraEditors.TextEdit();
            this.teSurveyor = new DevExpress.XtraEditors.TextEdit();
            this.teProductUnit = new DevExpress.XtraEditors.TextEdit();
            this.teMakemapMethod = new DevExpress.XtraEditors.TextEdit();
            this.teYOrig = new DevExpress.XtraEditors.TextEdit();
            this.teXOrig = new DevExpress.XtraEditors.TextEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.teIsoDistance = new DevExpress.XtraEditors.TextEdit();
            this.cbScaleRuler = new DevExpress.XtraEditors.ComboBoxEdit();
            this.teHeight = new DevExpress.XtraEditors.TextEdit();
            this.teMapNumber = new DevExpress.XtraEditors.TextEdit();
            this.teCoord = new DevExpress.XtraEditors.TextEdit();
            this.teMapName = new DevExpress.XtraEditors.TextEdit();
            this.cbMapTitle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem41 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem42 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem43 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem44 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem45 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem46 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem47 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem48 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem49 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem51 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem36 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem38 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem39 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem40 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem37 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.textEdit25 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin8.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin7.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMarginDown.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMarginLeft.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teYPrefix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teXPrefix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMapchecker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMapdrawer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSurveyor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teProductUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMakemapMethod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teYOrig.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teXOrig.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIsoDistance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbScaleRuler.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMapNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCoord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMapName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMapTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem43)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem45)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem46)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem47)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem48)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem49)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem51)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit25.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelControl19);
            this.layoutControl1.Controls.Add(this.labelControl18);
            this.layoutControl1.Controls.Add(this.labelControl17);
            this.layoutControl1.Controls.Add(this.labelControl16);
            this.layoutControl1.Controls.Add(this.labelControl15);
            this.layoutControl1.Controls.Add(this.labelControl14);
            this.layoutControl1.Controls.Add(this.labelControl13);
            this.layoutControl1.Controls.Add(this.labelControl12);
            this.layoutControl1.Controls.Add(this.labelControl11);
            this.layoutControl1.Controls.Add(this.labelControl10);
            this.layoutControl1.Controls.Add(this.labelControl9);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.btnLoadData);
            this.layoutControl1.Controls.Add(this.teAddress);
            this.layoutControl1.Controls.Add(this.checkEdit1);
            this.layoutControl1.Controls.Add(this.teJoin8);
            this.layoutControl1.Controls.Add(this.teJoin7);
            this.layoutControl1.Controls.Add(this.teJoin6);
            this.layoutControl1.Controls.Add(this.teJoin5);
            this.layoutControl1.Controls.Add(this.teJoin4);
            this.layoutControl1.Controls.Add(this.teJoin3);
            this.layoutControl1.Controls.Add(this.teJoin2);
            this.layoutControl1.Controls.Add(this.teJoin1);
            this.layoutControl1.Controls.Add(this.labelControl8);
            this.layoutControl1.Controls.Add(this.labelControl7);
            this.layoutControl1.Controls.Add(this.labelControl6);
            this.layoutControl1.Controls.Add(this.labelControl5);
            this.layoutControl1.Controls.Add(this.teMarginDown);
            this.layoutControl1.Controls.Add(this.teMarginLeft);
            this.layoutControl1.Controls.Add(this.labelControl4);
            this.layoutControl1.Controls.Add(this.labelControl3);
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.teYPrefix);
            this.layoutControl1.Controls.Add(this.teXPrefix);
            this.layoutControl1.Controls.Add(this.teMapchecker);
            this.layoutControl1.Controls.Add(this.teMapdrawer);
            this.layoutControl1.Controls.Add(this.teSurveyor);
            this.layoutControl1.Controls.Add(this.teProductUnit);
            this.layoutControl1.Controls.Add(this.teMakemapMethod);
            this.layoutControl1.Controls.Add(this.teYOrig);
            this.layoutControl1.Controls.Add(this.teXOrig);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.teIsoDistance);
            this.layoutControl1.Controls.Add(this.cbScaleRuler);
            this.layoutControl1.Controls.Add(this.teHeight);
            this.layoutControl1.Controls.Add(this.teMapNumber);
            this.layoutControl1.Controls.Add(this.teCoord);
            this.layoutControl1.Controls.Add(this.teMapName);
            this.layoutControl1.Controls.Add(this.cbMapTitle);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(382, 619);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(195, 207);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(36, 14);
            this.labelControl19.StyleController = this.layoutControl1;
            this.labelControl19.TabIndex = 54;
            this.labelControl19.Text = "检查：";
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(195, 181);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(36, 14);
            this.labelControl18.StyleController = this.layoutControl1;
            this.labelControl18.TabIndex = 53;
            this.labelControl18.Text = "绘图：";
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(195, 155);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(36, 14);
            this.labelControl17.StyleController = this.layoutControl1;
            this.labelControl17.TabIndex = 52;
            this.labelControl17.Text = "测图：";
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(5, 193);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(60, 14);
            this.labelControl16.StyleController = this.layoutControl1;
            this.labelControl16.TabIndex = 51;
            this.labelControl16.Text = "生产单位：";
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(5, 167);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(60, 14);
            this.labelControl15.StyleController = this.layoutControl1;
            this.labelControl15.TabIndex = 50;
            this.labelControl15.Text = "成图方式：";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(193, 103);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(72, 14);
            this.labelControl14.StyleController = this.layoutControl1;
            this.labelControl14.TabIndex = 49;
            this.labelControl14.Text = "图幅等高距：";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(193, 77);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(72, 14);
            this.labelControl13.StyleController = this.layoutControl1;
            this.labelControl13.TabIndex = 48;
            this.labelControl13.Text = "高程坐标系：";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(193, 51);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(72, 14);
            this.labelControl12.StyleController = this.layoutControl1;
            this.labelControl12.TabIndex = 47;
            this.labelControl12.Text = "平面坐标系：";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(5, 103);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(48, 14);
            this.labelControl11.StyleController = this.layoutControl1;
            this.labelControl11.TabIndex = 46;
            this.labelControl11.Text = "比例尺：";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(5, 77);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(36, 14);
            this.labelControl10.StyleController = this.layoutControl1;
            this.labelControl10.TabIndex = 45;
            this.labelControl10.Text = "图号：";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(5, 51);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(36, 14);
            this.labelControl9.StyleController = this.layoutControl1;
            this.labelControl9.TabIndex = 44;
            this.labelControl9.Text = "图名：";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(296, 595);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "取消";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(208, 595);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(84, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 42;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(2, 595);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(98, 22);
            this.btnLoadData.StyleController = this.layoutControl1;
            this.btnLoadData.TabIndex = 41;
            this.btnLoadData.Text = "加载元数据表";
            // 
            // teAddress
            // 
            this.teAddress.Location = new System.Drawing.Point(101, 569);
            this.teAddress.Name = "teAddress";
            this.teAddress.Size = new System.Drawing.Size(279, 22);
            this.teAddress.StyleController = this.layoutControl1;
            this.teAddress.TabIndex = 40;
            // 
            // checkEdit1
            // 
            this.checkEdit1.EditValue = true;
            this.checkEdit1.Location = new System.Drawing.Point(127, 505);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "自动生成接图编号";
            this.checkEdit1.Size = new System.Drawing.Size(127, 19);
            this.checkEdit1.StyleController = this.layoutControl1;
            this.checkEdit1.TabIndex = 39;
            // 
            // teJoin8
            // 
            this.teJoin8.Location = new System.Drawing.Point(258, 531);
            this.teJoin8.Name = "teJoin8";
            this.teJoin8.Size = new System.Drawing.Size(110, 22);
            this.teJoin8.StyleController = this.layoutControl1;
            this.teJoin8.TabIndex = 38;
            // 
            // teJoin7
            // 
            this.teJoin7.Location = new System.Drawing.Point(127, 531);
            this.teJoin7.Name = "teJoin7";
            this.teJoin7.Size = new System.Drawing.Size(127, 22);
            this.teJoin7.StyleController = this.layoutControl1;
            this.teJoin7.TabIndex = 37;
            // 
            // teJoin6
            // 
            this.teJoin6.Location = new System.Drawing.Point(14, 531);
            this.teJoin6.Name = "teJoin6";
            this.teJoin6.Size = new System.Drawing.Size(109, 22);
            this.teJoin6.StyleController = this.layoutControl1;
            this.teJoin6.TabIndex = 36;
            // 
            // teJoin5
            // 
            this.teJoin5.Location = new System.Drawing.Point(258, 505);
            this.teJoin5.Name = "teJoin5";
            this.teJoin5.Size = new System.Drawing.Size(110, 22);
            this.teJoin5.StyleController = this.layoutControl1;
            this.teJoin5.TabIndex = 35;
            // 
            // teJoin4
            // 
            this.teJoin4.Location = new System.Drawing.Point(14, 505);
            this.teJoin4.Name = "teJoin4";
            this.teJoin4.Size = new System.Drawing.Size(109, 22);
            this.teJoin4.StyleController = this.layoutControl1;
            this.teJoin4.TabIndex = 34;
            // 
            // teJoin3
            // 
            this.teJoin3.EditValue = "";
            this.teJoin3.Location = new System.Drawing.Point(258, 479);
            this.teJoin3.Name = "teJoin3";
            this.teJoin3.Size = new System.Drawing.Size(110, 22);
            this.teJoin3.StyleController = this.layoutControl1;
            this.teJoin3.TabIndex = 33;
            // 
            // teJoin2
            // 
            this.teJoin2.Location = new System.Drawing.Point(127, 479);
            this.teJoin2.Name = "teJoin2";
            this.teJoin2.Size = new System.Drawing.Size(127, 22);
            this.teJoin2.StyleController = this.layoutControl1;
            this.teJoin2.TabIndex = 32;
            // 
            // teJoin1
            // 
            this.teJoin1.Location = new System.Drawing.Point(14, 479);
            this.teJoin1.Name = "teJoin1";
            this.teJoin1.Size = new System.Drawing.Size(109, 22);
            this.teJoin1.StyleController = this.layoutControl1;
            this.teJoin1.TabIndex = 31;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(361, 418);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(16, 14);
            this.labelControl8.StyleController = this.layoutControl1;
            this.labelControl8.TabIndex = 30;
            this.labelControl8.Text = "cm";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(159, 418);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(16, 14);
            this.labelControl7.StyleController = this.layoutControl1;
            this.labelControl7.TabIndex = 29;
            this.labelControl7.Text = "cm";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(224, 418);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.StyleController = this.layoutControl1;
            this.labelControl6.TabIndex = 28;
            this.labelControl6.Text = "下：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(5, 418);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.StyleController = this.layoutControl1;
            this.labelControl5.TabIndex = 27;
            this.labelControl5.Text = "左：";
            // 
            // teMarginDown
            // 
            this.teMarginDown.EditValue = "1";
            this.teMarginDown.Location = new System.Drawing.Point(252, 418);
            this.teMarginDown.Name = "teMarginDown";
            this.teMarginDown.Size = new System.Drawing.Size(105, 22);
            this.teMarginDown.StyleController = this.layoutControl1;
            this.teMarginDown.TabIndex = 26;
            // 
            // teMarginLeft
            // 
            this.teMarginLeft.EditValue = "1";
            this.teMarginLeft.Location = new System.Drawing.Point(33, 418);
            this.teMarginLeft.Name = "teMarginLeft";
            this.teMarginLeft.Size = new System.Drawing.Size(122, 22);
            this.teMarginLeft.StyleController = this.layoutControl1;
            this.teMarginLeft.TabIndex = 25;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(224, 366);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(32, 14);
            this.labelControl4.StyleController = this.layoutControl1;
            this.labelControl4.TabIndex = 24;
            this.labelControl4.Text = "Y前：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 366);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(31, 14);
            this.labelControl3.StyleController = this.layoutControl1;
            this.labelControl3.TabIndex = 23;
            this.labelControl3.Text = "X前：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(223, 314);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(32, 14);
            this.labelControl2.StyleController = this.layoutControl1;
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "Y:     ";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 314);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(31, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "X:     ";
            // 
            // teYPrefix
            // 
            this.teYPrefix.EditValue = "";
            this.teYPrefix.Location = new System.Drawing.Point(260, 366);
            this.teYPrefix.Name = "teYPrefix";
            this.teYPrefix.Size = new System.Drawing.Size(117, 22);
            this.teYPrefix.StyleController = this.layoutControl1;
            this.teYPrefix.TabIndex = 20;
            // 
            // teXPrefix
            // 
            this.teXPrefix.Location = new System.Drawing.Point(40, 366);
            this.teXPrefix.Name = "teXPrefix";
            this.teXPrefix.Size = new System.Drawing.Size(180, 22);
            this.teXPrefix.StyleController = this.layoutControl1;
            this.teXPrefix.TabIndex = 19;
            // 
            // teMapchecker
            // 
            this.teMapchecker.Location = new System.Drawing.Point(235, 207);
            this.teMapchecker.Name = "teMapchecker";
            this.teMapchecker.Size = new System.Drawing.Size(142, 22);
            this.teMapchecker.StyleController = this.layoutControl1;
            this.teMapchecker.TabIndex = 18;
            // 
            // teMapdrawer
            // 
            this.teMapdrawer.Location = new System.Drawing.Point(235, 181);
            this.teMapdrawer.Name = "teMapdrawer";
            this.teMapdrawer.Size = new System.Drawing.Size(142, 22);
            this.teMapdrawer.StyleController = this.layoutControl1;
            this.teMapdrawer.TabIndex = 17;
            // 
            // teSurveyor
            // 
            this.teSurveyor.Location = new System.Drawing.Point(235, 155);
            this.teSurveyor.Name = "teSurveyor";
            this.teSurveyor.Size = new System.Drawing.Size(142, 22);
            this.teSurveyor.StyleController = this.layoutControl1;
            this.teSurveyor.TabIndex = 16;
            // 
            // teProductUnit
            // 
            this.teProductUnit.Location = new System.Drawing.Point(69, 193);
            this.teProductUnit.Name = "teProductUnit";
            this.teProductUnit.Size = new System.Drawing.Size(116, 22);
            this.teProductUnit.StyleController = this.layoutControl1;
            this.teProductUnit.TabIndex = 15;
            // 
            // teMakemapMethod
            // 
            this.teMakemapMethod.Location = new System.Drawing.Point(69, 167);
            this.teMakemapMethod.Name = "teMakemapMethod";
            this.teMakemapMethod.Size = new System.Drawing.Size(116, 22);
            this.teMakemapMethod.StyleController = this.layoutControl1;
            this.teMakemapMethod.TabIndex = 14;
            // 
            // teYOrig
            // 
            this.teYOrig.Location = new System.Drawing.Point(259, 314);
            this.teYOrig.Name = "teYOrig";
            this.teYOrig.Size = new System.Drawing.Size(118, 22);
            this.teYOrig.StyleController = this.layoutControl1;
            this.teYOrig.TabIndex = 13;
            // 
            // teXOrig
            // 
            this.teXOrig.Location = new System.Drawing.Point(40, 314);
            this.teXOrig.Name = "teXOrig";
            this.teXOrig.Size = new System.Drawing.Size(179, 22);
            this.teXOrig.StyleController = this.layoutControl1;
            this.teXOrig.TabIndex = 12;
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = true;
            this.radioGroup1.Location = new System.Drawing.Point(5, 259);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 3;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "50cm x 50cm"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "50cm x 40cm"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "40cm x 40cm")});
            this.radioGroup1.Size = new System.Drawing.Size(372, 25);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 11;
            // 
            // teIsoDistance
            // 
            this.teIsoDistance.Location = new System.Drawing.Point(269, 103);
            this.teIsoDistance.Name = "teIsoDistance";
            this.teIsoDistance.Size = new System.Drawing.Size(108, 22);
            this.teIsoDistance.StyleController = this.layoutControl1;
            this.teIsoDistance.TabIndex = 10;
            // 
            // cbScaleRuler
            // 
            this.cbScaleRuler.EditValue = "500";
            this.cbScaleRuler.Location = new System.Drawing.Point(57, 103);
            this.cbScaleRuler.Name = "cbScaleRuler";
            this.cbScaleRuler.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbScaleRuler.Properties.Items.AddRange(new object[] {
            "500",
            "1000",
            "2000",
            "5000"});
            this.cbScaleRuler.Size = new System.Drawing.Size(132, 22);
            this.cbScaleRuler.StyleController = this.layoutControl1;
            this.cbScaleRuler.TabIndex = 9;
            this.cbScaleRuler.SelectedIndexChanged += new System.EventHandler(this.cbScaleRuler_SelectedIndexChanged);
            // 
            // teHeight
            // 
            this.teHeight.Location = new System.Drawing.Point(269, 77);
            this.teHeight.Name = "teHeight";
            this.teHeight.Size = new System.Drawing.Size(108, 22);
            this.teHeight.StyleController = this.layoutControl1;
            this.teHeight.TabIndex = 8;
            // 
            // teMapNumber
            // 
            this.teMapNumber.Location = new System.Drawing.Point(45, 77);
            this.teMapNumber.Name = "teMapNumber";
            this.teMapNumber.Size = new System.Drawing.Size(144, 22);
            this.teMapNumber.StyleController = this.layoutControl1;
            this.teMapNumber.TabIndex = 7;
            // 
            // teCoord
            // 
            this.teCoord.Location = new System.Drawing.Point(269, 51);
            this.teCoord.Name = "teCoord";
            this.teCoord.Size = new System.Drawing.Size(108, 22);
            this.teCoord.StyleController = this.layoutControl1;
            this.teCoord.TabIndex = 6;
            // 
            // teMapName
            // 
            this.teMapName.Location = new System.Drawing.Point(45, 51);
            this.teMapName.Name = "teMapName";
            this.teMapName.Size = new System.Drawing.Size(144, 22);
            this.teMapName.StyleController = this.layoutControl1;
            this.teMapName.TabIndex = 5;
            // 
            // cbMapTitle
            // 
            this.cbMapTitle.Location = new System.Drawing.Point(101, 2);
            this.cbMapTitle.Name = "cbMapTitle";
            this.cbMapTitle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbMapTitle.Size = new System.Drawing.Size(279, 22);
            this.cbMapTitle.StyleController = this.layoutControl1;
            this.cbMapTitle.TabIndex = 4;
            this.cbMapTitle.SelectedIndexChanged += new System.EventHandler(this.cbMapTitle_SelectedIndexChanged);
            this.cbMapTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbMapTitle_KeyDown);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlGroup5,
            this.layoutControlGroup6,
            this.layoutControlGroup7,
            this.layoutControlGroup8,
            this.layoutControlGroup9,
            this.layoutControlItem38,
            this.layoutControlItem39,
            this.layoutControlItem40,
            this.layoutControlItem37,
            this.emptySpaceItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(382, 619);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbMapTitle;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(382, 26);
            this.layoutControlItem1.Text = " 选择图幅：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "基本信息";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem41,
            this.layoutControlItem42,
            this.layoutControlItem43,
            this.layoutControlItem44,
            this.layoutControlItem45,
            this.layoutControlItem46});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(382, 104);
            this.layoutControlGroup2.Text = "基本信息";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teMapName;
            this.layoutControlItem2.CustomizationFormText = "图名：";
            this.layoutControlItem2.Location = new System.Drawing.Point(40, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(148, 26);
            this.layoutControlItem2.Text = "图名：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teCoord;
            this.layoutControlItem3.CustomizationFormText = "平面坐标系：";
            this.layoutControlItem3.Location = new System.Drawing.Point(264, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(112, 26);
            this.layoutControlItem3.Text = "平面坐标系：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teMapNumber;
            this.layoutControlItem4.CustomizationFormText = "图号：";
            this.layoutControlItem4.Location = new System.Drawing.Point(40, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(148, 26);
            this.layoutControlItem4.Text = "图号：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teHeight;
            this.layoutControlItem5.CustomizationFormText = "高程基准：";
            this.layoutControlItem5.Location = new System.Drawing.Point(264, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(112, 26);
            this.layoutControlItem5.Text = "高程基准：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cbScaleRuler;
            this.layoutControlItem6.CustomizationFormText = "比例尺：";
            this.layoutControlItem6.Location = new System.Drawing.Point(52, 52);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(136, 26);
            this.layoutControlItem6.Text = "比例尺：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.teIsoDistance;
            this.layoutControlItem7.CustomizationFormText = "图幅等高距：";
            this.layoutControlItem7.Location = new System.Drawing.Point(264, 52);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(112, 26);
            this.layoutControlItem7.Text = "图幅等高距：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem41
            // 
            this.layoutControlItem41.Control = this.labelControl9;
            this.layoutControlItem41.CustomizationFormText = "layoutControlItem41";
            this.layoutControlItem41.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem41.Name = "layoutControlItem41";
            this.layoutControlItem41.Size = new System.Drawing.Size(40, 26);
            this.layoutControlItem41.Text = "layoutControlItem41";
            this.layoutControlItem41.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem41.TextToControlDistance = 0;
            this.layoutControlItem41.TextVisible = false;
            // 
            // layoutControlItem42
            // 
            this.layoutControlItem42.Control = this.labelControl10;
            this.layoutControlItem42.CustomizationFormText = "layoutControlItem42";
            this.layoutControlItem42.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem42.Name = "layoutControlItem42";
            this.layoutControlItem42.Size = new System.Drawing.Size(40, 26);
            this.layoutControlItem42.Text = "layoutControlItem42";
            this.layoutControlItem42.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem42.TextToControlDistance = 0;
            this.layoutControlItem42.TextVisible = false;
            // 
            // layoutControlItem43
            // 
            this.layoutControlItem43.Control = this.labelControl11;
            this.layoutControlItem43.CustomizationFormText = "layoutControlItem43";
            this.layoutControlItem43.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem43.Name = "layoutControlItem43";
            this.layoutControlItem43.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem43.Text = "layoutControlItem43";
            this.layoutControlItem43.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem43.TextToControlDistance = 0;
            this.layoutControlItem43.TextVisible = false;
            // 
            // layoutControlItem44
            // 
            this.layoutControlItem44.Control = this.labelControl12;
            this.layoutControlItem44.CustomizationFormText = "layoutControlItem44";
            this.layoutControlItem44.Location = new System.Drawing.Point(188, 0);
            this.layoutControlItem44.Name = "layoutControlItem44";
            this.layoutControlItem44.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem44.Text = "layoutControlItem44";
            this.layoutControlItem44.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem44.TextToControlDistance = 0;
            this.layoutControlItem44.TextVisible = false;
            // 
            // layoutControlItem45
            // 
            this.layoutControlItem45.Control = this.labelControl13;
            this.layoutControlItem45.CustomizationFormText = "layoutControlItem45";
            this.layoutControlItem45.Location = new System.Drawing.Point(188, 26);
            this.layoutControlItem45.Name = "layoutControlItem45";
            this.layoutControlItem45.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem45.Text = "layoutControlItem45";
            this.layoutControlItem45.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem45.TextToControlDistance = 0;
            this.layoutControlItem45.TextVisible = false;
            // 
            // layoutControlItem46
            // 
            this.layoutControlItem46.Control = this.labelControl14;
            this.layoutControlItem46.CustomizationFormText = "layoutControlItem46";
            this.layoutControlItem46.Location = new System.Drawing.Point(188, 52);
            this.layoutControlItem46.Name = "layoutControlItem46";
            this.layoutControlItem46.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem46.Text = "layoutControlItem46";
            this.layoutControlItem46.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem46.TextToControlDistance = 0;
            this.layoutControlItem46.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "资料来源";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.emptySpaceItem3,
            this.emptySpaceItem2,
            this.layoutControlItem47,
            this.layoutControlItem48});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 130);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(190, 104);
            this.layoutControlGroup3.Text = "资料来源";
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.teMakemapMethod;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(64, 12);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(120, 26);
            this.layoutControlItem11.Text = "成图方式：";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.teProductUnit;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(64, 38);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(120, 26);
            this.layoutControlItem12.Text = "生产单位：";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 64);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(184, 14);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(184, 12);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem47
            // 
            this.layoutControlItem47.Control = this.labelControl15;
            this.layoutControlItem47.CustomizationFormText = "layoutControlItem47";
            this.layoutControlItem47.Location = new System.Drawing.Point(0, 12);
            this.layoutControlItem47.Name = "layoutControlItem47";
            this.layoutControlItem47.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem47.Text = "layoutControlItem47";
            this.layoutControlItem47.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem47.TextToControlDistance = 0;
            this.layoutControlItem47.TextVisible = false;
            // 
            // layoutControlItem48
            // 
            this.layoutControlItem48.Control = this.labelControl16;
            this.layoutControlItem48.CustomizationFormText = "layoutControlItem48";
            this.layoutControlItem48.Location = new System.Drawing.Point(0, 38);
            this.layoutControlItem48.Name = "layoutControlItem48";
            this.layoutControlItem48.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem48.Text = "layoutControlItem48";
            this.layoutControlItem48.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem48.TextToControlDistance = 0;
            this.layoutControlItem48.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "责任人员";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.layoutControlItem49,
            this.layoutControlItem50,
            this.layoutControlItem51});
            this.layoutControlGroup4.Location = new System.Drawing.Point(190, 130);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(192, 104);
            this.layoutControlGroup4.Text = "责任人员";
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.teSurveyor;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(40, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(146, 26);
            this.layoutControlItem13.Text = "测图：";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.teMapdrawer;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(40, 26);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(146, 26);
            this.layoutControlItem14.Text = "绘图：";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.teMapchecker;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new System.Drawing.Point(40, 52);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(146, 26);
            this.layoutControlItem15.Text = "检查：";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem49
            // 
            this.layoutControlItem49.Control = this.labelControl17;
            this.layoutControlItem49.CustomizationFormText = "layoutControlItem49";
            this.layoutControlItem49.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem49.Name = "layoutControlItem49";
            this.layoutControlItem49.Size = new System.Drawing.Size(40, 26);
            this.layoutControlItem49.Text = "layoutControlItem49";
            this.layoutControlItem49.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem49.TextToControlDistance = 0;
            this.layoutControlItem49.TextVisible = false;
            // 
            // layoutControlItem50
            // 
            this.layoutControlItem50.Control = this.labelControl18;
            this.layoutControlItem50.CustomizationFormText = "layoutControlItem50";
            this.layoutControlItem50.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem50.Name = "layoutControlItem50";
            this.layoutControlItem50.Size = new System.Drawing.Size(40, 26);
            this.layoutControlItem50.Text = "layoutControlItem50";
            this.layoutControlItem50.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem50.TextToControlDistance = 0;
            this.layoutControlItem50.TextVisible = false;
            // 
            // layoutControlItem51
            // 
            this.layoutControlItem51.Control = this.labelControl19;
            this.layoutControlItem51.CustomizationFormText = "layoutControlItem51";
            this.layoutControlItem51.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem51.Name = "layoutControlItem51";
            this.layoutControlItem51.Size = new System.Drawing.Size(40, 26);
            this.layoutControlItem51.Text = "layoutControlItem51";
            this.layoutControlItem51.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem51.TextToControlDistance = 0;
            this.layoutControlItem51.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "分幅方式";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 234);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(382, 55);
            this.layoutControlGroup5.Text = "分幅方式";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.radioGroup1;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(376, 29);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "西南角坐标";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem18,
            this.layoutControlItem19});
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 289);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(382, 52);
            this.layoutControlGroup6.Text = "西南角坐标";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.teXOrig;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(35, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem9.Text = "X:";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.teYOrig;
            this.layoutControlItem10.CustomizationFormText = "Y:";
            this.layoutControlItem10.Location = new System.Drawing.Point(254, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(122, 26);
            this.layoutControlItem10.Text = "Y:";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.labelControl1;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(35, 26);
            this.layoutControlItem18.Text = "layoutControlItem18";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextToControlDistance = 0;
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.labelControl2;
            this.layoutControlItem19.CustomizationFormText = "layoutControlItem19";
            this.layoutControlItem19.Location = new System.Drawing.Point(218, 0);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(36, 26);
            this.layoutControlItem19.Text = "layoutControlItem19";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem19.TextToControlDistance = 0;
            this.layoutControlItem19.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.CustomizationFormText = "坐标前缀";
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem16,
            this.layoutControlItem17,
            this.layoutControlItem20,
            this.layoutControlItem21});
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 341);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Size = new System.Drawing.Size(382, 52);
            this.layoutControlGroup7.Text = "坐标前缀";
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.teXPrefix;
            this.layoutControlItem16.CustomizationFormText = "X前：";
            this.layoutControlItem16.Location = new System.Drawing.Point(35, 0);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(184, 26);
            this.layoutControlItem16.Text = "X前：";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextToControlDistance = 0;
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.teYPrefix;
            this.layoutControlItem17.CustomizationFormText = "Y前：";
            this.layoutControlItem17.Location = new System.Drawing.Point(255, 0);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(121, 26);
            this.layoutControlItem17.Text = "Y前：";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextToControlDistance = 0;
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.labelControl3;
            this.layoutControlItem20.CustomizationFormText = "layoutControlItem20";
            this.layoutControlItem20.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(35, 26);
            this.layoutControlItem20.Text = "layoutControlItem20";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem20.TextToControlDistance = 0;
            this.layoutControlItem20.TextVisible = false;
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.labelControl4;
            this.layoutControlItem21.CustomizationFormText = "layoutControlItem21";
            this.layoutControlItem21.Location = new System.Drawing.Point(219, 0);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(36, 26);
            this.layoutControlItem21.Text = "layoutControlItem21";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem21.TextToControlDistance = 0;
            this.layoutControlItem21.TextVisible = false;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.CustomizationFormText = "左下角页边框";
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem22,
            this.layoutControlItem23,
            this.layoutControlItem24,
            this.layoutControlItem25,
            this.layoutControlItem26,
            this.layoutControlItem27,
            this.emptySpaceItem1});
            this.layoutControlGroup8.Location = new System.Drawing.Point(0, 393);
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup8.Size = new System.Drawing.Size(382, 52);
            this.layoutControlGroup8.Text = "左下角页边框";
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.teMarginLeft;
            this.layoutControlItem22.CustomizationFormText = "layoutControlItem22";
            this.layoutControlItem22.Location = new System.Drawing.Point(28, 0);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(126, 26);
            this.layoutControlItem22.Text = "layoutControlItem22";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem22.TextToControlDistance = 0;
            this.layoutControlItem22.TextVisible = false;
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.teMarginDown;
            this.layoutControlItem23.CustomizationFormText = "layoutControlItem23";
            this.layoutControlItem23.Location = new System.Drawing.Point(247, 0);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(109, 26);
            this.layoutControlItem23.Text = "layoutControlItem23";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem23.TextToControlDistance = 0;
            this.layoutControlItem23.TextVisible = false;
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.labelControl5;
            this.layoutControlItem24.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem24.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(28, 26);
            this.layoutControlItem24.Text = "layoutControlItem24";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem24.TextToControlDistance = 0;
            this.layoutControlItem24.TextVisible = false;
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.labelControl6;
            this.layoutControlItem25.CustomizationFormText = "layoutControlItem25";
            this.layoutControlItem25.Location = new System.Drawing.Point(219, 0);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(28, 26);
            this.layoutControlItem25.Text = "layoutControlItem25";
            this.layoutControlItem25.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem25.TextToControlDistance = 0;
            this.layoutControlItem25.TextVisible = false;
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.labelControl7;
            this.layoutControlItem26.CustomizationFormText = "layoutControlItem26";
            this.layoutControlItem26.Location = new System.Drawing.Point(154, 0);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(20, 26);
            this.layoutControlItem26.Text = "layoutControlItem26";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem26.TextToControlDistance = 0;
            this.layoutControlItem26.TextVisible = false;
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.labelControl8;
            this.layoutControlItem27.CustomizationFormText = "layoutControlItem27";
            this.layoutControlItem27.Location = new System.Drawing.Point(356, 0);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(20, 26);
            this.layoutControlItem27.Text = "layoutControlItem27";
            this.layoutControlItem27.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem27.TextToControlDistance = 0;
            this.layoutControlItem27.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(174, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(45, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.CustomizationFormText = "接图表";
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem28,
            this.layoutControlItem29,
            this.layoutControlItem30,
            this.layoutControlItem31,
            this.layoutControlItem32,
            this.layoutControlItem33,
            this.layoutControlItem34,
            this.layoutControlItem35,
            this.layoutControlItem36});
            this.layoutControlGroup9.Location = new System.Drawing.Point(0, 445);
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Size = new System.Drawing.Size(382, 122);
            this.layoutControlGroup9.Text = "接图表";
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.teJoin1;
            this.layoutControlItem28.CustomizationFormText = "layoutControlItem28";
            this.layoutControlItem28.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(113, 26);
            this.layoutControlItem28.Text = "layoutControlItem28";
            this.layoutControlItem28.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem28.TextToControlDistance = 0;
            this.layoutControlItem28.TextVisible = false;
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.teJoin2;
            this.layoutControlItem29.CustomizationFormText = "layoutControlItem29";
            this.layoutControlItem29.Location = new System.Drawing.Point(113, 0);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(131, 26);
            this.layoutControlItem29.Text = "layoutControlItem29";
            this.layoutControlItem29.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem29.TextToControlDistance = 0;
            this.layoutControlItem29.TextVisible = false;
            // 
            // layoutControlItem30
            // 
            this.layoutControlItem30.Control = this.teJoin3;
            this.layoutControlItem30.CustomizationFormText = "layoutControlItem30";
            this.layoutControlItem30.Location = new System.Drawing.Point(244, 0);
            this.layoutControlItem30.Name = "layoutControlItem30";
            this.layoutControlItem30.Size = new System.Drawing.Size(114, 26);
            this.layoutControlItem30.Text = "layoutControlItem30";
            this.layoutControlItem30.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem30.TextToControlDistance = 0;
            this.layoutControlItem30.TextVisible = false;
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.Control = this.teJoin4;
            this.layoutControlItem31.CustomizationFormText = "layoutControlItem31";
            this.layoutControlItem31.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(113, 26);
            this.layoutControlItem31.Text = "layoutControlItem31";
            this.layoutControlItem31.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem31.TextToControlDistance = 0;
            this.layoutControlItem31.TextVisible = false;
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.Control = this.teJoin5;
            this.layoutControlItem32.CustomizationFormText = "layoutControlItem32";
            this.layoutControlItem32.Location = new System.Drawing.Point(244, 26);
            this.layoutControlItem32.Name = "layoutControlItem32";
            this.layoutControlItem32.Size = new System.Drawing.Size(114, 26);
            this.layoutControlItem32.Text = "layoutControlItem32";
            this.layoutControlItem32.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem32.TextToControlDistance = 0;
            this.layoutControlItem32.TextVisible = false;
            // 
            // layoutControlItem33
            // 
            this.layoutControlItem33.Control = this.teJoin6;
            this.layoutControlItem33.CustomizationFormText = "layoutControlItem33";
            this.layoutControlItem33.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem33.Name = "layoutControlItem33";
            this.layoutControlItem33.Size = new System.Drawing.Size(113, 26);
            this.layoutControlItem33.Text = "layoutControlItem33";
            this.layoutControlItem33.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem33.TextToControlDistance = 0;
            this.layoutControlItem33.TextVisible = false;
            // 
            // layoutControlItem34
            // 
            this.layoutControlItem34.Control = this.teJoin7;
            this.layoutControlItem34.CustomizationFormText = "layoutControlItem34";
            this.layoutControlItem34.Location = new System.Drawing.Point(113, 52);
            this.layoutControlItem34.Name = "layoutControlItem34";
            this.layoutControlItem34.Size = new System.Drawing.Size(131, 26);
            this.layoutControlItem34.Text = "layoutControlItem34";
            this.layoutControlItem34.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem34.TextToControlDistance = 0;
            this.layoutControlItem34.TextVisible = false;
            // 
            // layoutControlItem35
            // 
            this.layoutControlItem35.Control = this.teJoin8;
            this.layoutControlItem35.CustomizationFormText = "layoutControlItem35";
            this.layoutControlItem35.Location = new System.Drawing.Point(244, 52);
            this.layoutControlItem35.Name = "layoutControlItem35";
            this.layoutControlItem35.Size = new System.Drawing.Size(114, 26);
            this.layoutControlItem35.Text = "layoutControlItem35";
            this.layoutControlItem35.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem35.TextToControlDistance = 0;
            this.layoutControlItem35.TextVisible = false;
            // 
            // layoutControlItem36
            // 
            this.layoutControlItem36.Control = this.checkEdit1;
            this.layoutControlItem36.CustomizationFormText = "layoutControlItem36";
            this.layoutControlItem36.Location = new System.Drawing.Point(113, 26);
            this.layoutControlItem36.Name = "layoutControlItem36";
            this.layoutControlItem36.Size = new System.Drawing.Size(131, 26);
            this.layoutControlItem36.Text = "layoutControlItem36";
            this.layoutControlItem36.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem36.TextToControlDistance = 0;
            this.layoutControlItem36.TextVisible = false;
            // 
            // layoutControlItem38
            // 
            this.layoutControlItem38.Control = this.btnLoadData;
            this.layoutControlItem38.CustomizationFormText = "layoutControlItem38";
            this.layoutControlItem38.Location = new System.Drawing.Point(0, 593);
            this.layoutControlItem38.Name = "layoutControlItem38";
            this.layoutControlItem38.Size = new System.Drawing.Size(102, 26);
            this.layoutControlItem38.Text = "layoutControlItem38";
            this.layoutControlItem38.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem38.TextToControlDistance = 0;
            this.layoutControlItem38.TextVisible = false;
            // 
            // layoutControlItem39
            // 
            this.layoutControlItem39.Control = this.btnOK;
            this.layoutControlItem39.CustomizationFormText = "layoutControlItem39";
            this.layoutControlItem39.Location = new System.Drawing.Point(206, 593);
            this.layoutControlItem39.Name = "layoutControlItem39";
            this.layoutControlItem39.Size = new System.Drawing.Size(88, 26);
            this.layoutControlItem39.Text = "layoutControlItem39";
            this.layoutControlItem39.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem39.TextToControlDistance = 0;
            this.layoutControlItem39.TextVisible = false;
            // 
            // layoutControlItem40
            // 
            this.layoutControlItem40.Control = this.btnCancel;
            this.layoutControlItem40.CustomizationFormText = "layoutControlItem40";
            this.layoutControlItem40.Location = new System.Drawing.Point(294, 593);
            this.layoutControlItem40.Name = "layoutControlItem40";
            this.layoutControlItem40.Size = new System.Drawing.Size(88, 26);
            this.layoutControlItem40.Text = "layoutControlItem40";
            this.layoutControlItem40.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem40.TextToControlDistance = 0;
            this.layoutControlItem40.TextVisible = false;
            // 
            // layoutControlItem37
            // 
            this.layoutControlItem37.Control = this.teAddress;
            this.layoutControlItem37.CustomizationFormText = "元数据表数据库：";
            this.layoutControlItem37.Location = new System.Drawing.Point(0, 567);
            this.layoutControlItem37.Name = "layoutControlItem37";
            this.layoutControlItem37.Size = new System.Drawing.Size(382, 26);
            this.layoutControlItem37.Text = "元数据表数据库：";
            this.layoutControlItem37.TextSize = new System.Drawing.Size(96, 14);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(102, 593);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(104, 26);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // textEdit25
            // 
            this.textEdit25.Location = new System.Drawing.Point(105, 571);
            this.textEdit25.Name = "textEdit25";
            this.textEdit25.Size = new System.Drawing.Size(272, 22);
            this.textEdit25.StyleController = this.layoutControl1;
            this.textEdit25.TabIndex = 40;
            // 
            // FrmStandardOutput
            // 
            this.ClientSize = new System.Drawing.Size(382, 619);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmStandardOutput";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "标准图幅输出";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin8.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin7.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teJoin1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMarginDown.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMarginLeft.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teYPrefix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teXPrefix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMapchecker.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMapdrawer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSurveyor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teProductUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMakemapMethod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teYOrig.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teXOrig.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIsoDistance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbScaleRuler.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMapNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCoord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMapName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMapTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem43)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem45)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem46)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem47)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem48)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem49)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem51)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit25.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        /// <summary>
		/// 构造函数
		/// </summary>
        public FrmStandardOutput(IWorkspace pWorkspace)
		{
			m_pWorkspace = pWorkspace;
			m_pFWorkspace = (IFeatureWorkspace)pWorkspace;
			InitializeComponent();
			try
			{
				this.LoadMetaData(m_pFWorkspace);
				this.teAddress.Text = m_pWorkspace.PathName;
                this.teAddress.Enabled = false;
				this.btnOK.Enabled = false;
			}
			catch
			{
			}
		}

        #region//读取并更新指定图幅的参数设置
        /// <summary>
        /// 读取并更新指定图幅的参数设置
        /// </summary>
        /// <param name="mapTitle"></param>
        private void UpdateContent(string mapTitle)
        {
            IWorkspaceFactory workspaceFactory = new ESRI.ArcGIS.DataSourcesGDB.AccessWorkspaceFactoryClass();
            IWorkspace workspace = workspaceFactory.OpenFromFile(strAccessName, 0);
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
            ITable table = featureWorkspace.OpenTable("Metadata");

            ICursor cursor = table.Search(null, false);
            IRow row = cursor.NextRow();

            while (row != null)
            {
                if (mapTitle == this.getTableValue(row.get_Value(row.Fields.FindField("Maptitle"))))
                {
                    this.teMapName.Text = mapTitle;

                    this.cbScaleRuler.SelectedIndex = this.getScale(getTableValue(row.get_Value(row.Fields.FindField("PSYSType"))));

                    this.teIsoDistance.Text = getTableValue(row.get_Value(row.Fields.FindField("AtlasContour")));

                    this.teHeight.Text = this.getHeightType(getTableValue(row.get_Value(row.Fields.FindField("HSYSType"))));

                    this.teCoord.Text = this.getCoordType(getTableValue(row.get_Value(row.Fields.FindField("PSYSType"))));

                    this.teProductUnit.Text = getTableValue(row.get_Value(row.Fields.FindField("CreateDept")));
                    this.teMakemapMethod.Text = getTableValue(row.get_Value(row.Fields.FindField("CartographyMode")));

                    this.teSurveyor.Text = getTableValue(row.get_Value(row.Fields.FindField("Surveyor")));
                    this.teMapdrawer.Text = getTableValue(row.get_Value(row.Fields.FindField("Mapper")));
                    this.teMapchecker.Text = getTableValue(row.get_Value(row.Fields.FindField("Checker")));

                    this.teXOrig.Text = getTableValue(row.get_Value(row.Fields.FindField("SWY")));
                    this.teYOrig.Text = getTableValue(row.get_Value(row.Fields.FindField("SWX")));
                    this.mapX = System.Int32.Parse(this.teXOrig.Text);
                    this.mapY = System.Int32.Parse(this.teYOrig.Text);

                    try
                    {
                        this.NormalizeXY();
                    }
                    catch
                    {
                    }
                    this.UpdateMapCode();

                    return;
                }
                row = cursor.NextRow();
            }
        }
        #endregion

        #region//读取并更新指定图幅的参数设置


        /// <summary>
		/// 读取并更新指定图幅的参数设置
		/// </summary>
		/// <param name="mapTitle"></param>
		private void UpdateContent(IFeatureWorkspace featureWorkspace, string mapTitle)
		{
			ITable table = featureWorkspace.OpenTable("Metadata");

			ICursor cursor = table.Search(null,false);
			IRow row = cursor.NextRow();
			
			while(row != null)
			{
				if(mapTitle == this.getTableValue(row.get_Value(row.Fields.FindField("Maptitle"))))
				{
					this.teMapName.Text = mapTitle;

					this.teMapNumber.Text = getTableValue(row.get_Value(row.Fields.FindField("MapNO")));

					try
					{
						this.cbScaleRuler.SelectedIndex = this.getScale(getTableValue(row.get_Value(row.Fields.FindField("Scale"))));
					}
					catch
					{
					}
				
					this.teIsoDistance.Text = this.getAtlasContourType(getTableValue(row.get_Value(row.Fields.FindField("AtlasContour"))));

					this.teHeight.Text = this.getHeightType(getTableValue(row.get_Value(row.Fields.FindField("HSYSType"))));

					this.teCoord.Text = this.getCoordType(getTableValue(row.get_Value(row.Fields.FindField("PSYSType"))));

					this.teProductUnit.Text = getTableValue(row.get_Value(row.Fields.FindField("CreateDept")));
					this.teMakemapMethod.Text = getTableValue(row.get_Value(row.Fields.FindField("CartographyMode")));

					this.teSurveyor.Text = getTableValue(row.get_Value(row.Fields.FindField("Surveyor")));
					this.teMapdrawer.Text = getTableValue(row.get_Value(row.Fields.FindField("Mapper")));
					this.teMapchecker.Text = getTableValue(row.get_Value(row.Fields.FindField("Checker")));

					this.teXOrig.Text = getTableValue(row.get_Value(row.Fields.FindField("SWX")));
					this.teYOrig.Text = getTableValue(row.get_Value(row.Fields.FindField("SWY")));
					this.mapX = System.Int32.Parse(this.teXOrig.Text);
					this.mapY = System.Int32.Parse(this.teYOrig.Text);

					try
					{
						this.NormalizeXY();
					}
					catch
					{
					}
					this.UpdateMapCode();

					return;
				}
				row = cursor.NextRow();
			}
		}
		#endregion

        #region //获取所有的图幅图号
        /// <summary>
        /// 获取所有的图幅图号
        /// </summary>
        /// <param name="featureWorkspace"></param>
        /// <returns></returns>
        private ArrayList GetAllMapNo(IFeatureWorkspace featureWorkspace)
        {
            ArrayList arrMapNo = new ArrayList();
            ITable table = featureWorkspace.OpenTable("Metadata");
            ICursor cursor = table.Search(null, false);
            IRow row = cursor.NextRow();

            while (row != null)
            {
                arrMapNo.Add(getTableValue(row.get_Value(row.Fields.FindField("MapNO"))));
                row = cursor.NextRow();
            }
            return arrMapNo;
        }
        #endregion

        private int mapX;
        private int MapX
        {
            get
            {
                return mapX;
            }
            set
            {
                mapX = value;
            }
        }
        private int mapY;
        private int MapY
        {
            get
            {
                return mapY;
            }
            set
            {
                mapY = value;
            }
        }

        /// <summary>
        /// 根据标准图幅的比例尺修正左下角（西南角）的X、Y坐标
        /// </summary>
        private void NormalizeXY()
        {
            int oldX = mapX;
            int oldY = mapY;

            int cell = 50;

            switch (this.MapScale)
            {
                case 500:
                    cell = 50;
                    break;
                case 1000:
                    cell = 100;
                    break;
                case 2000:
                    cell = 1000;
                    break;
                case 5000:
                    cell = 1000;
                    break;
            }

            int newX = (int)System.Math.Floor((double)oldX / cell) * cell;
            int newY = (int)System.Math.Floor((double)oldY / cell) * cell;

            this.teXOrig.Text = newX.ToString();
            this.teYOrig.Text = newY.ToString();
        }

        /// <summary>
        /// 获取数据表中的内容
        /// </summary>
        private string getTableValue(object obj)
        {
            string rtnValue = "";
            if (obj == System.DBNull.Value)
                rtnValue = "";
            else
                rtnValue = obj.ToString();

            return rtnValue;
        }

        /// <summary>
        /// 获取等高距离
        /// </summary>
        private string getAtlasContourType(string index)
        {
            switch (index)
            {
                case "0":
                    return "0.5m";
                case "1":
                    return "1m";
                case "2":
                    return "2m";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取坐标系统类型
        /// </summary>
        private string getCoordType(string index)
        {
            switch (index)
            {
                case "0":
                    return "1954年北京坐标系";
                case "1":
                    return "1980年西安坐标系";
                case "2":
                    return "2000国家大地坐标系";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取高程类型
        /// </summary>
        private string getHeightType(string index)
        {
            switch (index)
            {
                case "0":
                    return "吴淞高程系";
                case "1":
                    return "1956黄海高程基准";
                case "2":
                    return "1985国家高程基准";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取比例尺
        /// </summary>
        private int getScale(string index)
        {
            return System.Int32.Parse(index);
        }

        private void cbScaleRuler_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.NormalizeXY();
            if (this.MapScale == 5000)
            {
                this.radioGroup1.SelectedIndex = 2;
                
            }
            else
            {
                this.radioGroup1.SelectedIndex = 0;
            }

            this.UpdateMapCode();
        }
        private int XCellCount
        {
            get
            {
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    return 5;
                }
                else
                {
                    return 4;
                }
            }
        }
        private int YCellCount
        {
            get
            {
                if (this.radioGroup1.SelectedIndex == 2)
                {
                    return 4;
                }
                else
                {
                    return 5;
                }
            }
        }

        public int MapScale
        {
            get
            {
                switch (this.cbScaleRuler.SelectedIndex)
                {
                    case 0:
                        return 500;
                    case 1:
                        return 1000;
                    case 2:
                        return 2000;
                    case 3:
                        return 5000;
                    default:
                        return 0;
                }
            }
            set
            {
                switch (value)
                {
                    case 500:
                        this.cbScaleRuler.SelectedIndex = 0;
                        break;
                    case 1000:
                        this.cbScaleRuler.SelectedIndex = 1;
                        break;
                    case 2000:
                        this.cbScaleRuler.SelectedIndex = 2;
                        break;
                    case 5000:
                        this.cbScaleRuler.SelectedIndex = 3;
                        break;
                }
            }
        }

        /// <summary>
        /// 自动计算当前图幅的编号，并更新接图表的图幅编号
        /// </summary>
        private void UpdateMapCode()
        {
            int x = 0;
            int y = 0;
            try
            {
                x = System.Int32.Parse(this.teXOrig.Text);
                y = System.Int32.Parse(this.teYOrig.Text);
            }
            catch
            {
            }

            if (this.MapX == 0 || this.MapY == 0)
            {
                return;
            }

            mapCode.GenerateMapCode(this.MapScale, x, y, this.XCellCount, this.YCellCount);
            this.teMapNumber.Text = this.mapCode.ToString();

            if (this.checkEdit1.Checked)
            {
                ArrayList arrMapNo = this.GetAllMapNo(this.m_pFWorkspace);

                this.teJoin1.Text = null;
                this.teJoin2.Text = null;
                this.teJoin3.Text = null;
                this.teJoin4.Text = null;
                this.teJoin5.Text = null;
                this.teJoin6.Text = null;
                this.teJoin7.Text = null;
                this.teJoin8.Text = null;

                if (arrMapNo.Contains(this.mapCode[1]))
                    this.teJoin1.Text = this.mapCode[1];

                if (arrMapNo.Contains(this.mapCode[2]))
                    this.teJoin2.Text = this.mapCode[2];

                if (arrMapNo.Contains(this.mapCode[3]))
                    this.teJoin3.Text = this.mapCode[3];

                if (arrMapNo.Contains(this.mapCode[4]))
                    this.teJoin4.Text = this.mapCode[4];

                if (arrMapNo.Contains(this.mapCode[5]))
                    this.teJoin5.Text = this.mapCode[5];

                if (arrMapNo.Contains(this.mapCode[6]))
                    this.teJoin6.Text = this.mapCode[6];

                if (arrMapNo.Contains(this.mapCode[7]))
                    this.teJoin7.Text = this.mapCode[7];

                if (arrMapNo.Contains(this.mapCode[8]))
                    this.teJoin8.Text = this.mapCode[8];
            }
        }

        private void cbMapTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateContent(m_pFWorkspace, this.cbMapTitle.SelectedItem.ToString());
            this.btnOK.Enabled = true;
        }

        private void cbMapTitle_KeyDown(object sender, KeyEventArgs e)
        {
            //检查combobox中的图幅名称是否正确
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cbMapTitle.Properties.Items.Contains(this.cbMapTitle.Text) == true)
                {
                    this.UpdateContent(m_pFWorkspace, this.cbMapTitle.Text);
                    this.btnOK.Enabled = true;
                }
                else
                {
                    MessageBox.Show("该图幅名不存在，请重新输入或选择！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.ClearText();
                    this.btnOK.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 点击“加载图幅元数据”按钮时触发的事件
        /// </summary>
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = m_pWorkspace.PathName;
            ofd.Filter = "ACCESS 数据库文件|*.mdb";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.LoadMetaData(ofd.FileName);
            }
        }

        #region//加载图幅元数据
        /// <summary>
        /// 加载图幅元数据
        /// </summary>		
        private void LoadMetaData(string fileName)
        {
            strAccessName = fileName;

            ESRI.ArcGIS.Geodatabase.IWorkspaceFactory workspaceFactory = new ESRI.ArcGIS.DataSourcesGDB.AccessWorkspaceFactoryClass();

            ESRI.ArcGIS.Geodatabase.IWorkspace workspace = workspaceFactory.OpenFromFile(strAccessName, 0);
            ESRI.ArcGIS.Geodatabase.IFeatureWorkspace featureWorkspace = (ESRI.ArcGIS.Geodatabase.IFeatureWorkspace)workspace;

            ESRI.ArcGIS.Geodatabase.ITable table = null;
            try
            {
                table = featureWorkspace.OpenTable("MetaData");
                this.teAddress.Text = fileName;
            }
            catch
            {
                MessageBox.Show("文件中不包含描述图幅元数据的表格", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ESRI.ArcGIS.Geodatabase.ICursor cursor = table.Search(null, false);
            ESRI.ArcGIS.Geodatabase.IRow row = cursor.NextRow();

            string strTitle;

            while (row != null)
            {
                strTitle = this.getTableValue(row.get_Value(row.Fields.FindField("Maptitle")));
                if (strTitle != "")
                {
                    this.cbMapTitle.Properties.Items.Add(strTitle);
                }

                row = cursor.NextRow();
            }
            this.cbMapTitle.SelectedIndex = -1;
        }
        #endregion

        #region//加载图幅元数据
        /// <summary>
        /// 加载图幅元数据
        /// </summary>		
        private void LoadMetaData(ESRI.ArcGIS.Geodatabase.IFeatureWorkspace featureWorkspace)
        {
            ESRI.ArcGIS.Geodatabase.ITable table = null;
            try
            {
                table = featureWorkspace.OpenTable("Metadata");
            }
            catch
            {
                MessageBox.Show("文件中不包含描述图幅元数据的表格", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ESRI.ArcGIS.Geodatabase.ICursor cursor = table.Search(null, false);
            ESRI.ArcGIS.Geodatabase.IRow row = cursor.NextRow();

            string strTitle;

            while (row != null)
            {
                strTitle = this.getTableValue(row.get_Value(row.Fields.FindField("Maptitle")));
                if (strTitle != "")
                {
                    this.cbMapTitle.Properties.Items.Add(strTitle);
                }

                row = cursor.NextRow();
            }
            this.cbMapTitle.SelectedIndex = -1;
        }
        #endregion

        #region//清除控件中的文本
        private void ClearText()
        {
            this.teMapName.Text = null;
            this.teMapNumber.Text = null;
            this.teCoord.Text = null;
            this.teHeight.Text = null;
            this.teMakemapMethod.Text = null;
            this.teProductUnit.Text = null;
            this.teSurveyor.Text = null;
            this.teMapdrawer.Text = null;
            this.teMapchecker.Text = null;
            this.teXOrig.Text = null;
            this.teYOrig.Text = null;
            this.teXPrefix.Text = null;
            this.teYPrefix.Text = null;
            this.teJoin1.Text = null;
            this.teJoin2.Text = null;
            this.teJoin3.Text = null;
            this.teJoin4.Text = null;
            this.teJoin5.Text = null;
            this.teJoin6.Text = null;
            this.teJoin7.Text = null;
            this.teJoin8.Text = null;
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
          
        }

        public double MarginLeft
        {
            get
            {
                return System.Double.Parse(this.teMarginLeft.Text);
            }
        }

        public double MarginDown
        {
            get
            {
                return System.Double.Parse(this.teMarginDown.Text);
            }
        }

        private bool ValidateControls()
        {
            try
            {
                double temp = this.MarginLeft;
            }
            catch
            {
                MessageBox.Show(
                    "", "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.teMarginLeft.Focus();
                return false;
            }

            try
            {
                double temp = this.MarginDown;
            }
            catch
            {
                MessageBox.Show(
                    "", "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.teMarginDown.Focus();
                return false;
            }

            return true;
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            if (this.ValidateControls())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

     



       




        

       


    }
}
