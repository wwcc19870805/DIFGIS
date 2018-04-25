using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using DF2DTool.Class;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;
using DFWinForms.Class;
using DFCommon.Class;
using DevExpress.XtraEditors;

namespace DF2DTool.Frm
{
    public partial class FrmDxfExport : XtraForm
    {

        private string outputFileName = "";
        //		private IFeatureSelection pFeaSelection;
        private ISelection pSelection;
        private string fileType = "1";			//导入格式  1--dxf  2--南方CASS  3--青山智绘  4--Access mdb
        private string operType = "2";			//操作类型  1--从选择集导出 2--从文件导出
        private string mdbFileName = "";			//配置文件
        private string layerTable = "";			//图层对应表
        private string symbolTable = "";			//要素对应表
        private string defMdbFileName = "";		//缺省的配置文件名
        private string defLayerTable = "";		//缺省的图层对应表名
        private string defSymbolTable = "";		//缺省的符号对应表名     
        private DataSetNames inputDs = new DataSetNames();
        private IMap pMap;
        private IWorkspace pCurWorkspace;

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraEditors.SimpleButton btnOutFile;
        private DevExpress.XtraEditors.TextEdit txtFile;
        private DevExpress.XtraEditors.TextEdit txtAngle;
        private DevExpress.XtraEditors.TextEdit txtObjNum;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFile;
        private DevExpress.XtraEditors.CheckedListBoxControl chlOperType;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl chlFiletype;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbScale;
        private DevExpress.XtraEditors.SimpleButton btnFont;
        private DevExpress.XtraEditors.SimpleButton btnLinetype;
        private DevExpress.XtraEditors.SimpleButton btnBlock;
        private DevExpress.XtraEditors.TextEdit txtFont;
        private DevExpress.XtraEditors.TextEdit txtLinetype;
        private DevExpress.XtraEditors.TextEdit txtBlock;
        private DevExpress.XtraEditors.CheckEdit chkSymSet;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSym;
        private DevExpress.XtraEditors.ComboBoxEdit cmbLayer;
        private DevExpress.XtraEditors.TextEdit txtProFile;
        private DevExpress.XtraEditors.CheckEdit chkFileSet;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraEditors.SimpleButton btnExport;        
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private System.Windows.Forms.OpenFileDialog ofdExport;
        private DevExpress.XtraEditors.SimpleButton btnSym;
        private DevExpress.XtraEditors.SimpleButton btnLayer;
        private DevExpress.XtraEditors.SimpleButton btnProFile;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraEditors.SimpleButton btnQuit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraEditors.SimpleButton btnInFile;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private System.Windows.Forms.SaveFileDialog sfdExport;
    
      
        private System.ComponentModel.Container components = null;
        //常规的导入方法
		public FrmDxfExport()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			this.InitForm("2");
			cmbScale.Text="1:500";
		}
        //输入的文件名
		public FrmDxfExport(string mFile)
		{
			InitializeComponent();			
			this.InitForm("2");
			this.fullInputDs(mFile);
            //this.grpOther.Visible=false;
			refCmbFile(inputDs);
		}
        //通过选择集的方式调用
		public FrmDxfExport(ISelection pSele,IMap pInputMap)
		{			
			InitializeComponent();
			this.InitForm("1");

			pSelection=pSele;
			pMap=pInputMap;
			cmbScale.Text="1:500";
		}
        public FrmDxfExport(ISelection pSele, IMap pInputMap, IWorkspace pCurWsp)
        {
            InitializeComponent();
            this.InitForm("1");

            pSelection = pSele;
            pMap = pInputMap;
            pCurWorkspace = pCurWsp;
            cmbScale.Text = "1:500";
        }
        private void InitForm(string type)
        {
            //IDataObjectHelper h = new DataObjectHelper();
            //缺省设置	（图层和符号）
            defMdbFileName = Application.StartupPath + @"\..\Support\ConvertSymbol.mdb";
            defMdbFileName = System.IO.Path.GetFullPath(defMdbFileName);
            defLayerTable = "CadLayerCompar";
            defSymbolTable = Application.StartupPath + @"\..\Result\test.dxf";
            //初始化变量 （图层和符号）			
            outputFileName = @"D:\Test.dxf";
            fileType = "1";
            mdbFileName = Application.StartupPath + @"\..\Support\ConvertSymbol.mdb";
            mdbFileName = System.IO.Path.GetFullPath(mdbFileName);
            cmbLayer.Properties.Items.Add("CadLayerCompar");
            cmbSym.Properties.Items.Add("CadCompar");
            layerTable = "CadLayerCompar";
            symbolTable = "CadCompar";
           
            txtFile.Text = outputFileName;
            if (File.Exists(mdbFileName))
            {
                txtProFile.Text = mdbFileName;
                cmbLayer.Text = layerTable;
                cmbSym.Text = symbolTable;
            }
            	
            operType = type;		//从选择集或数据库导出
            if (operType == "1")
            {
                chlOperType.SelectedIndex = 0;
                cmbFile.Enabled = false;
                btnInFile.Enabled = false;
                btnExport.Enabled = true;
            }
            else
            {
                chlOperType.SelectedIndex = 1;
                cmbFile.Enabled = true;
                btnInFile.Enabled = true;
            }
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.btnInFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuit = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnOutFile = new DevExpress.XtraEditors.SimpleButton();
            this.txtFile = new DevExpress.XtraEditors.TextEdit();
            this.txtAngle = new DevExpress.XtraEditors.TextEdit();
            this.txtObjNum = new DevExpress.XtraEditors.TextEdit();
            this.cmbFile = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chlOperType = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chlFiletype = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSym = new DevExpress.XtraEditors.SimpleButton();
            this.btnLayer = new DevExpress.XtraEditors.SimpleButton();
            this.btnProFile = new DevExpress.XtraEditors.SimpleButton();
            this.cmbScale = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnFont = new DevExpress.XtraEditors.SimpleButton();
            this.btnLinetype = new DevExpress.XtraEditors.SimpleButton();
            this.btnBlock = new DevExpress.XtraEditors.SimpleButton();
            this.txtFont = new DevExpress.XtraEditors.TextEdit();
            this.txtLinetype = new DevExpress.XtraEditors.TextEdit();
            this.txtBlock = new DevExpress.XtraEditors.TextEdit();
            this.chkSymSet = new DevExpress.XtraEditors.CheckEdit();
            this.cmbSym = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtProFile = new DevExpress.XtraEditors.TextEdit();
            this.chkFileSet = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAngle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObjNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chlOperType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chlFiletype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFont.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLinetype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBlock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSymSet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSym.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFileSet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(302, 327);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(298, 323);
            this.xtraTabControl1.TabIndex = 4;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.layoutControl2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(292, 294);
            this.xtraTabPage1.Text = "导出设置";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.btnInFile);
            this.layoutControl2.Controls.Add(this.btnQuit);
            this.layoutControl2.Controls.Add(this.btnExport);
            this.layoutControl2.Controls.Add(this.btnOutFile);
            this.layoutControl2.Controls.Add(this.txtFile);
            this.layoutControl2.Controls.Add(this.txtAngle);
            this.layoutControl2.Controls.Add(this.txtObjNum);
            this.layoutControl2.Controls.Add(this.cmbFile);
            this.layoutControl2.Controls.Add(this.chlOperType);
            this.layoutControl2.Controls.Add(this.groupControl1);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(292, 294);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // btnInFile
            // 
            this.btnInFile.Location = new System.Drawing.Point(235, 137);
            this.btnInFile.Name = "btnInFile";
            this.btnInFile.Size = new System.Drawing.Size(52, 22);
            this.btnInFile.StyleController = this.layoutControl2;
            this.btnInFile.TabIndex = 13;
            this.btnInFile.Text = "...";
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(148, 267);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(139, 22);
            this.btnQuit.StyleController = this.layoutControl2;
            this.btnQuit.TabIndex = 12;
            this.btnQuit.Text = "退出";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(5, 267);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(139, 22);
            this.btnExport.StyleController = this.layoutControl2;
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "开始导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnOutFile
            // 
            this.btnOutFile.Location = new System.Drawing.Point(237, 241);
            this.btnOutFile.Name = "btnOutFile";
            this.btnOutFile.Size = new System.Drawing.Size(50, 22);
            this.btnOutFile.StyleController = this.layoutControl2;
            this.btnOutFile.TabIndex = 11;
            this.btnOutFile.Text = "...";
            this.btnOutFile.Click += new System.EventHandler(this.btnOutFile_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(68, 241);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(165, 22);
            this.txtFile.StyleController = this.layoutControl2;
            this.txtFile.TabIndex = 10;
            // 
            // txtAngle
            // 
            this.txtAngle.EditValue = "Direction";
            this.txtAngle.Location = new System.Drawing.Point(68, 189);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(219, 22);
            this.txtAngle.StyleController = this.layoutControl2;
            this.txtAngle.TabIndex = 9;
            // 
            // txtObjNum
            // 
            this.txtObjNum.EditValue = "GeoObjNum";
            this.txtObjNum.Location = new System.Drawing.Point(68, 163);
            this.txtObjNum.Name = "txtObjNum";
            this.txtObjNum.Size = new System.Drawing.Size(219, 22);
            this.txtObjNum.StyleController = this.layoutControl2;
            this.txtObjNum.TabIndex = 8;
            // 
            // cmbFile
            // 
            this.cmbFile.Location = new System.Drawing.Point(68, 137);
            this.cmbFile.Name = "cmbFile";
            this.cmbFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFile.Size = new System.Drawing.Size(163, 22);
            this.cmbFile.StyleController = this.layoutControl2;
            this.cmbFile.TabIndex = 6;
            // 
            // chlOperType
            // 
            this.chlOperType.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(true, "从选择集导出", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(false, "从文件导出", System.Windows.Forms.CheckState.Unchecked, false)});
            this.chlOperType.Location = new System.Drawing.Point(5, 80);
            this.chlOperType.MultiColumn = true;
            this.chlOperType.Name = "chlOperType";
            this.chlOperType.Size = new System.Drawing.Size(282, 53);
            this.chlOperType.StyleController = this.layoutControl2;
            this.chlOperType.TabIndex = 5;
            this.chlOperType.SelectedIndexChanged += new System.EventHandler(this.chlOperType_SelectedIndexChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.chlFiletype);
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(288, 51);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "导出数据类型";
            // 
            // chlFiletype
            // 
            this.chlFiletype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chlFiletype.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(true, "AutoCad Dxf", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(false, "Mdb")});
            this.chlFiletype.Location = new System.Drawing.Point(2, 22);
            this.chlFiletype.MultiColumn = true;
            this.chlFiletype.Name = "chlFiletype";
            this.chlFiletype.Size = new System.Drawing.Size(284, 27);
            this.chlFiletype.TabIndex = 0;
            this.chlFiletype.SelectedIndexChanged += new System.EventHandler(this.chlFiletype_SelectedIndexChanged);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlGroup3,
            this.layoutControlGroup4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(292, 294);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.groupControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(292, 55);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem28});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 55);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(292, 161);
            this.layoutControlGroup3.Text = "导出方式";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chlOperType;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(286, 57);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbFile;
            this.layoutControlItem4.CustomizationFormText = "文件名：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 57);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(230, 26);
            this.layoutControlItem4.Text = "文件名：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtObjNum;
            this.layoutControlItem6.CustomizationFormText = "符号编码：";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 83);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem6.Text = "符号编码：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtAngle;
            this.layoutControlItem7.CustomizationFormText = "旋转角度：";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 109);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem7.Text = "旋转角度：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.btnInFile;
            this.layoutControlItem28.CustomizationFormText = "layoutControlItem28";
            this.layoutControlItem28.Location = new System.Drawing.Point(230, 57);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(56, 26);
            this.layoutControlItem28.Text = "layoutControlItem28";
            this.layoutControlItem28.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem28.TextToControlDistance = 0;
            this.layoutControlItem28.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "导出文件";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem26,
            this.layoutControlItem27});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 216);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(292, 78);
            this.layoutControlGroup4.Text = "导出文件";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtFile;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(232, 26);
            this.layoutControlItem8.Text = "文件名：";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnOutFile;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(232, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(54, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.btnExport;
            this.layoutControlItem26.CustomizationFormText = "layoutControlItem26";
            this.layoutControlItem26.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(143, 26);
            this.layoutControlItem26.Text = "layoutControlItem26";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem26.TextToControlDistance = 0;
            this.layoutControlItem26.TextVisible = false;
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.btnQuit;
            this.layoutControlItem27.CustomizationFormText = "layoutControlItem27";
            this.layoutControlItem27.Location = new System.Drawing.Point(143, 26);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(143, 26);
            this.layoutControlItem27.Text = "layoutControlItem27";
            this.layoutControlItem27.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem27.TextToControlDistance = 0;
            this.layoutControlItem27.TextVisible = false;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.layoutControl3);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(292, 294);
            this.xtraTabPage2.Text = "高级设置";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.btnSym);
            this.layoutControl3.Controls.Add(this.btnLayer);
            this.layoutControl3.Controls.Add(this.btnProFile);
            this.layoutControl3.Controls.Add(this.cmbScale);
            this.layoutControl3.Controls.Add(this.btnFont);
            this.layoutControl3.Controls.Add(this.btnLinetype);
            this.layoutControl3.Controls.Add(this.btnBlock);
            this.layoutControl3.Controls.Add(this.txtFont);
            this.layoutControl3.Controls.Add(this.txtLinetype);
            this.layoutControl3.Controls.Add(this.txtBlock);
            this.layoutControl3.Controls.Add(this.chkSymSet);
            this.layoutControl3.Controls.Add(this.cmbSym);
            this.layoutControl3.Controls.Add(this.cmbLayer);
            this.layoutControl3.Controls.Add(this.txtProFile);
            this.layoutControl3.Controls.Add(this.chkFileSet);
            this.layoutControl3.Controls.Add(this.groupControl2);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10});
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup5;
            this.layoutControl3.Size = new System.Drawing.Size(292, 294);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // btnSym
            // 
            this.btnSym.Location = new System.Drawing.Point(228, 100);
            this.btnSym.Name = "btnSym";
            this.btnSym.Size = new System.Drawing.Size(59, 22);
            this.btnSym.StyleController = this.layoutControl3;
            this.btnSym.TabIndex = 22;
            this.btnSym.Text = "...";
            // 
            // btnLayer
            // 
            this.btnLayer.Location = new System.Drawing.Point(226, 74);
            this.btnLayer.Name = "btnLayer";
            this.btnLayer.Size = new System.Drawing.Size(61, 22);
            this.btnLayer.StyleController = this.layoutControl3;
            this.btnLayer.TabIndex = 21;
            this.btnLayer.Text = "...";
            // 
            // btnProFile
            // 
            this.btnProFile.Location = new System.Drawing.Point(227, 48);
            this.btnProFile.Name = "btnProFile";
            this.btnProFile.Size = new System.Drawing.Size(60, 22);
            this.btnProFile.StyleController = this.layoutControl3;
            this.btnProFile.TabIndex = 20;
            this.btnProFile.Text = "...";
            this.btnProFile.Click += new System.EventHandler(this.btnProFile_Click);
            // 
            // cmbScale
            // 
            this.cmbScale.Location = new System.Drawing.Point(80, 253);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbScale.Size = new System.Drawing.Size(207, 22);
            this.cmbScale.StyleController = this.layoutControl3;
            this.cmbScale.TabIndex = 19;
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(226, 227);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(61, 22);
            this.btnFont.StyleController = this.layoutControl3;
            this.btnFont.TabIndex = 18;
            this.btnFont.Text = "...";
            // 
            // btnLinetype
            // 
            this.btnLinetype.Location = new System.Drawing.Point(224, 201);
            this.btnLinetype.Name = "btnLinetype";
            this.btnLinetype.Size = new System.Drawing.Size(63, 22);
            this.btnLinetype.StyleController = this.layoutControl3;
            this.btnLinetype.TabIndex = 17;
            this.btnLinetype.Text = "...";
            // 
            // btnBlock
            // 
            this.btnBlock.Location = new System.Drawing.Point(226, 175);
            this.btnBlock.Name = "btnBlock";
            this.btnBlock.Size = new System.Drawing.Size(61, 22);
            this.btnBlock.StyleController = this.layoutControl3;
            this.btnBlock.TabIndex = 16;
            this.btnBlock.Text = "...";
            // 
            // txtFont
            // 
            this.txtFont.Location = new System.Drawing.Point(80, 227);
            this.txtFont.Name = "txtFont";
            this.txtFont.Size = new System.Drawing.Size(142, 22);
            this.txtFont.StyleController = this.layoutControl3;
            this.txtFont.TabIndex = 15;
            // 
            // txtLinetype
            // 
            this.txtLinetype.Location = new System.Drawing.Point(80, 201);
            this.txtLinetype.Name = "txtLinetype";
            this.txtLinetype.Size = new System.Drawing.Size(140, 22);
            this.txtLinetype.StyleController = this.layoutControl3;
            this.txtLinetype.TabIndex = 14;
            // 
            // txtBlock
            // 
            this.txtBlock.Location = new System.Drawing.Point(80, 175);
            this.txtBlock.Name = "txtBlock";
            this.txtBlock.Size = new System.Drawing.Size(142, 22);
            this.txtBlock.StyleController = this.layoutControl3;
            this.txtBlock.TabIndex = 13;
            // 
            // chkSymSet
            // 
            this.chkSymSet.EditValue = true;
            this.chkSymSet.Location = new System.Drawing.Point(5, 152);
            this.chkSymSet.Name = "chkSymSet";
            this.chkSymSet.Properties.Caption = "使用默认配置";
            this.chkSymSet.Size = new System.Drawing.Size(282, 19);
            this.chkSymSet.StyleController = this.layoutControl3;
            this.chkSymSet.TabIndex = 12;
            // 
            // cmbSym
            // 
            this.cmbSym.Location = new System.Drawing.Point(80, 100);
            this.cmbSym.Name = "cmbSym";
            this.cmbSym.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSym.Size = new System.Drawing.Size(144, 22);
            this.cmbSym.StyleController = this.layoutControl3;
            this.cmbSym.TabIndex = 10;
            // 
            // cmbLayer
            // 
            this.cmbLayer.Location = new System.Drawing.Point(80, 74);
            this.cmbLayer.Name = "cmbLayer";
            this.cmbLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLayer.Size = new System.Drawing.Size(142, 22);
            this.cmbLayer.StyleController = this.layoutControl3;
            this.cmbLayer.TabIndex = 8;
            // 
            // txtProFile
            // 
            this.txtProFile.Location = new System.Drawing.Point(80, 48);
            this.txtProFile.Name = "txtProFile";
            this.txtProFile.Size = new System.Drawing.Size(143, 22);
            this.txtProFile.StyleController = this.layoutControl3;
            this.txtProFile.TabIndex = 6;
            // 
            // chkFileSet
            // 
            this.chkFileSet.EditValue = true;
            this.chkFileSet.Location = new System.Drawing.Point(5, 25);
            this.chkFileSet.Name = "chkFileSet";
            this.chkFileSet.Properties.Caption = "使用默认配置";
            this.chkFileSet.Size = new System.Drawing.Size(282, 19);
            this.chkFileSet.StyleController = this.layoutControl3;
            this.chkFileSet.TabIndex = 5;
            this.chkFileSet.CheckStateChanged += new System.EventHandler(this.chkFileSet_CheckStateChanged);
            // 
            // groupControl2
            // 
            this.groupControl2.Location = new System.Drawing.Point(128, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(132, 226);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "groupControl2";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.groupControl2;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(252, 230);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(50, 20);
            this.layoutControlItem10.TextToControlDistance = 5;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup5";
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup6,
            this.layoutControlGroup7});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(292, 294);
            this.layoutControlGroup5.Text = "layoutControlGroup5";
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "配置文件设置";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem14,
            this.layoutControlItem16,
            this.layoutControlItem5,
            this.layoutControlItem15,
            this.layoutControlItem17});
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(292, 127);
            this.layoutControlGroup6.Text = "配置文件设置";
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.chkFileSet;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(286, 23);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.txtProFile;
            this.layoutControlItem12.CustomizationFormText = "配置文件：";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(222, 26);
            this.layoutControlItem12.Text = "配置文件：";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.cmbLayer;
            this.layoutControlItem14.CustomizationFormText = "图层对照表：";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem14.Text = "图层对照表：";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.cmbSym;
            this.layoutControlItem16.CustomizationFormText = "符号对照表：";
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 75);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(223, 26);
            this.layoutControlItem16.Text = "符号对照表：";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnProFile;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(222, 23);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.btnLayer;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new System.Drawing.Point(221, 49);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(65, 26);
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.btnSym;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(223, 75);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(63, 26);
            this.layoutControlItem17.Text = "layoutControlItem17";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextToControlDistance = 0;
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.CustomizationFormText = "符号定义文件配置";
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem3,
            this.layoutControlItem18,
            this.layoutControlItem19,
            this.layoutControlItem20,
            this.layoutControlItem21,
            this.layoutControlItem22,
            this.layoutControlItem23,
            this.layoutControlItem24,
            this.layoutControlItem25});
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 127);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Size = new System.Drawing.Size(292, 167);
            this.layoutControlGroup7.Text = "符号定义文件配置";
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 127);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(286, 14);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.chkSymSet;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(286, 23);
            this.layoutControlItem18.Text = "layoutControlItem18";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextToControlDistance = 0;
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.txtBlock;
            this.layoutControlItem19.CustomizationFormText = "符号定义：";
            this.layoutControlItem19.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem19.Text = "  符号定义：";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.txtLinetype;
            this.layoutControlItem20.CustomizationFormText = "  线型定义：";
            this.layoutControlItem20.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(219, 26);
            this.layoutControlItem20.Text = "  线型定义：";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.txtFont;
            this.layoutControlItem21.CustomizationFormText = "  字体定义：";
            this.layoutControlItem21.Location = new System.Drawing.Point(0, 75);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem21.Text = "  字体定义：";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.btnBlock;
            this.layoutControlItem22.CustomizationFormText = "layoutControlItem22";
            this.layoutControlItem22.Location = new System.Drawing.Point(221, 23);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(65, 26);
            this.layoutControlItem22.Text = "layoutControlItem22";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem22.TextToControlDistance = 0;
            this.layoutControlItem22.TextVisible = false;
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.btnLinetype;
            this.layoutControlItem23.CustomizationFormText = "layoutControlItem23";
            this.layoutControlItem23.Location = new System.Drawing.Point(219, 49);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(67, 26);
            this.layoutControlItem23.Text = "layoutControlItem23";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem23.TextToControlDistance = 0;
            this.layoutControlItem23.TextVisible = false;
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.btnFont;
            this.layoutControlItem24.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem24.Location = new System.Drawing.Point(221, 75);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(65, 26);
            this.layoutControlItem24.Text = "layoutControlItem24";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem24.TextToControlDistance = 0;
            this.layoutControlItem24.TextVisible = false;
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.cmbScale;
            this.layoutControlItem25.CustomizationFormText = "  比例尺：";
            this.layoutControlItem25.Location = new System.Drawing.Point(0, 101);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem25.Text = "  比例尺：";
            this.layoutControlItem25.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(302, 327);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.xtraTabControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(302, 327);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmDxfExport
            // 
            this.ClientSize = new System.Drawing.Size(302, 327);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmDxfExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "地图导出";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAngle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObjNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chlOperType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chlFiletype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbScale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFont.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLinetype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBlock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSymSet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSym.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFileSet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            if (fileType == "4")
            {
                ExportToMdb eToMdb = new ExportToMdb();
                eToMdb.strDestWorkspaceName = outputFileName;
                eToMdb.OperType = operType;
                if (operType == "1")
                {
                    eToMdb.Selection = pSelection;
                    eToMdb.DsNames = null;
                    //eToMdb.OrigWorkspace = pCurWorkspace;
                    IWorkspaceFactory pWspFactory = new AccessWorkspaceFactoryClass();
                    string path = Config.GetConfigValue("2DMdbPipe");
                    eToMdb.OrigWorkspace = pWspFactory.OpenFromFile(path, 0);
                  
                }
                else
                {
                    eToMdb.DsNames = inputDs;
                    eToMdb.Selection = null;
                    if (inputDs != null)
                    {
                        IWorkspaceFactory pWspFactory = new AccessWorkspaceFactoryClass();
                        eToMdb.OrigWorkspace = pWspFactory.OpenFromFile(inputDs.get_Item(0).WorkspaceName.PathName, 0);
                    }
                }
                eToMdb.DataExportToMdb();
                MessageBox.Show("导出完成！");
                
            }
            else
            {
                outputFileName = txtFile.Text;
                mdbFileName = txtProFile.Text;
                layerTable = cmbLayer.Text;
                symbolTable = cmbSym.Text;
                bool initOk = true;
                PublicFun.WrtieEntCount = 0;
                PublicFun.ReadEntCount = 0;

                ExportMain eMain = new ExportMain();
                eMain.FileType = fileType;
                eMain.OperType = operType;
                if (operType == "1")
                {
                    eMain.PFeaSelection = pSelection as IFeatureSelection;
                    eMain.PMap = pMap;
                    inputDs = null;
                }
                else
                {
                    eMain.InputDs = inputDs;
                    eMain.PFeaSelection = null;
                    eMain.PMap = null;
                }
                eMain.MdbFileName = mdbFileName;
                eMain.LayerTable = layerTable;
                eMain.SymbolTable = symbolTable;
                eMain.OutputFileName = outputFileName;
                eMain.StrObjNum = txtObjNum.Text;
                eMain.StrAngle = txtAngle.Text;
                eMain.MapScale = 0.5;
                if (cmbScale.Text == "1:500") eMain.MapScale = 0.5;
                if (cmbScale.Text == "1:1000") eMain.MapScale = 1;
                //eMain.IfoutputZ = this.outputZorNo.Checked;
                eMain.IfoutputZ = false;
                initOk = eMain.ExportInit();
                if (initOk)
                    eMain.ExportRun();
                WaitForm.Stop();
            }
        }

       

        private void btnInFile_Click(object sender, EventArgs e)
        {
            DatasetEdit dsEdit = new DatasetEdit(inputDs);
            dsEdit.ShowDialog();
            inputDs = dsEdit.dsNames;
            refCmbFile(inputDs);
        }
        private void refCmbFile(DataSetNames dsNames)
        {
            cmbFile.Properties.Items.Clear();
            //填充ComboBox
            IDatasetName datasetName;
            IFeatureClassName featureClassName;

            for (int i = 0; i < dsNames.Count; i++)
            {
                datasetName = dsNames.get_Item(i);
                if (datasetName.Type == esriDatasetType.esriDTFeatureClass)
                {
                    featureClassName = (IFeatureClassName)datasetName;

                    string item;
                    item = datasetName.Name + " [" + getGeotype(featureClassName.FeatureType, featureClassName.ShapeType) + "]";
                    cmbFile.Properties.Items.Add(item);
                }

                if (datasetName.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    IFeatureDatasetName featureDatasetName = (IFeatureDatasetName)datasetName;

                    string item;
                    item = datasetName.Name + " [数据集]";
                    cmbFile.Properties.Items.Add(item);
                }
            }

            if (cmbFile.Properties.Items.Count > 0)
                cmbFile.Text = cmbFile.Properties.Items[0].ToString();
            this.refCmd();
        }

      
        private void btnOutFile_Click(object sender, EventArgs e)
        {
            if (chlFiletype.SelectedIndex == 1)
            {
                sfdExport = new SaveFileDialog();
                sfdExport.Filter = "Access Mdb file(*.mdb)|*.mdb";
                sfdExport.ShowDialog();
                if (sfdExport.FileName.Length > 0)
                {
                    outputFileName = sfdExport.FileName;
                    txtFile.Text = outputFileName;
                }
            }
            else
            {
                sfdExport = new SaveFileDialog();
                sfdExport.Filter = "AutoCad Dxf file(*.Dxf)|*.Dxf";
                sfdExport.ShowDialog();
                if (sfdExport.FileName.Length > 0)
                {
                    outputFileName = sfdExport.FileName;
                    txtFile.Text = outputFileName;
                }
                this.refCmd();
            }
        }

        private void chkFileSet_CheckStateChanged(object sender, EventArgs e)
        {
            if (File.Exists(mdbFileName))
            {
                cmbLayer.Properties.Items.Clear();
                cmbSym.Properties.Items.Clear();

                if (chkFileSet.Checked)
                {
                    cmbLayer.Enabled = false;
                    cmbSym.Enabled = false;
                    btnProFile.Enabled = false;
                    btnLayer.Enabled = false;
                    btnSym.Enabled = false;

                    txtProFile.Text = defMdbFileName;
                    cmbLayer.Properties.Items.Add(defLayerTable);
                    cmbSym.Properties.Items.Add(defSymbolTable);
                    cmbLayer.Text = defLayerTable;
                    cmbSym.Text = defSymbolTable;
                }
                else
                {
                    cmbLayer.Enabled = true;
                    cmbSym.Enabled = true;
                    btnProFile.Enabled = true;
                    btnLayer.Enabled = true;
                    btnSym.Enabled = true;

                    txtProFile.Text = defMdbFileName;
                    mdbFileName = defMdbFileName;

                    this.refComboTable();
                }
            }
            this.refCmd();
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {
            ofdExport = new OpenFileDialog();
            ofdExport.Filter = "Access file(*.mdb)|*.mdb";
            ofdExport.ShowDialog();
            if (ofdExport.FileName.Length > 0)
            {
                mdbFileName = ofdExport.FileName;
                txtProFile.Text = mdbFileName;
            }
            this.refComboTable();
            this.refCmd();
        }

        //控制导入按钮的状态
        private void refCmd()
        {
            bool isValid = true;
            if (txtFile.Text == "") isValid = false;
            if (txtObjNum.Text == "") isValid = false;
            if (txtAngle.Text == "") isValid = false;
            if (txtProFile.Text == "") isValid = false;
            if (cmbLayer.Text == "") isValid = false;
            //if(txtBlock.Text=="") isValid=false;
            //if(txtLinetype.Text=="") isValid=false;
            //if(txtFont.Text=="") isValid=false;			

            if (operType == "1")
            {
                if (pMap.SelectionCount == 0) isValid = false;
            }
            else
            {
                if (cmbFile.Text == "") isValid = false;
            }

            if (isValid) btnExport.Enabled = true;
            else btnExport.Enabled = false;
        }
        //根据系统数据库刷新combobox列表
        private void refComboTable()
        {
            int i = 0;
            int j = 0;
            cmbLayer.Properties.Items.Clear();
            cmbSym.Properties.Items.Clear();

            IWorkspace pWS = PublicFun.GetAccessWs(mdbFileName);

            IEnumDatasetName enumDatasetName = pWS.get_DatasetNames(esriDatasetType.esriDTTable);

            if (enumDatasetName == null)
            {
                return;
            }

            IDatasetName datasetName = enumDatasetName.Next();
            while (datasetName != null)
            {
                if (FcOpertion.CheckIsValidTable(mdbFileName, datasetName, "CadLayer"))
                {
                    this.cmbLayer.Properties.Items.Add(datasetName.Name);
                    if (i == 0) this.cmbLayer.Text = datasetName.Name;
                    i++;
                }
                if (FcOpertion.CheckIsValidTable(mdbFileName, datasetName, "SymbolCode"))
                {
                    this.cmbSym.Properties.Items.Add(datasetName.Name);
                    if (j == 0) this.cmbSym.Text = datasetName.Name;
                    j++;
                }

                datasetName = enumDatasetName.Next();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            pMap.ClearSelection();
            System.GC.Collect();
            this.Dispose();
            this.Close();
        }
        private string getGeotype(esriFeatureType ftype, esriGeometryType gtype)
        {
            string rtnType = "";
            if (ftype == esriFeatureType.esriFTAnnotation)
                rtnType = "注记";
            else
            {
                switch (gtype)
                {
                    case esriGeometryType.esriGeometryPoint:
                        rtnType = "点图层";
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        rtnType = "线图层";
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        rtnType = "面图层";
                        break;
                    default:
                        rtnType = "";
                        break;
                }
            }
            return rtnType;
        }

        /// <summary>
        /// 向inputDs添加数据
        /// </summary>
        /// <param name="mFile"></param>
        private void fullInputDs(string mFile)
        {
            IWorkspace workspace = PublicFun.GetAccessWs(mFile);

            IEnumDatasetName enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
            if (enumDatasetName == null)
            {
                return;
            }

            IDatasetName datasetName = enumDatasetName.Next();
            while (datasetName != null)
            {
                inputDs.AddItem(datasetName);
                datasetName = enumDatasetName.Next();
            }

            enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
            if (enumDatasetName == null)
            {
                return;
            }

            datasetName = enumDatasetName.Next();
            while (datasetName != null)
            {
                inputDs.AddItem(datasetName);
                datasetName = enumDatasetName.Next();
            }
        }

      

        private void chlFiletype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chlFiletype.SelectedIndex == 0)
            {
                chlFiletype.SetItemCheckState(0, CheckState.Checked);
                chlFiletype.SetItemCheckState(1, CheckState.Unchecked);
                fileType = "1";
           }
            else
            {
                chlFiletype.SetItemCheckState(0, CheckState.Unchecked);
                chlFiletype.SetItemCheckState(1, CheckState.Checked);
                fileType = "4";
            }
        }

        private void chlOperType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chlOperType.SelectedIndex == 0)
            {
                chlOperType.SetItemCheckState(0, CheckState.Checked);
                chlOperType.SetItemCheckState(1, CheckState.Unchecked);
                operType = "1";
            }
            else
            {
                chlOperType.SetItemCheckState(0, CheckState.Unchecked);
                chlOperType.SetItemCheckState(1, CheckState.Checked);
                operType = "2";
            }
        }


    }
}
