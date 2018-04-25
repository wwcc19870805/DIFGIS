using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using DF2DFileConvert.Interface;
using DF2DFileConvert.Class;
using DF2DFileConvert.Frm;
using DFWinForms.Class;

namespace DF2DFileConvert
{
    public partial class MainForm : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit txtExportFile;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.TextEdit txtImportFile;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.SimpleButton btnSym;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSymbol;
        private DevExpress.XtraEditors.SimpleButton btnLayer;
        private DevExpress.XtraEditors.ComboBoxEdit cmbLayer;
        private DevExpress.XtraEditors.SimpleButton btnFile;
        private DevExpress.XtraEditors.TextEdit txtFile;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private string inputFileName = "";
        private string outputFileName = "";
        private string fileType = "1";			//导入格式  1--dxf  2--南方CASS  3--青山智绘
        private string mdbFileName = "";
        private string layerTable = "";
        private string symbolTable = "";
        private string defMdbFileName = "";
        private string defLayerTable = "";
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private TextEdit txtFont;
        private TextEdit txtSpace;
        private TextEdit txtHeight;
        private TextEdit txtAngle;
        private TextEdit txtObjNum;
        private TextEdit txtPrecision;
        private TextEdit txtMaxY;
        private TextEdit txtMinY;
        private TextEdit txtMaxX;
        private TextEdit txtMinX;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private ComboBoxEdit cmbMapScale;
        private SimpleButton btnStart;
        private SimpleButton btnQuite;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private TextEdit textFont;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private SimpleButton btnFont;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private string defSymbolTable = "";
    
        public MainForm()
        {
            InitializeComponent();
            //缺省设置
            defMdbFileName = Application.StartupPath + @"\..\Support\ConvertSymbol.mdb";
            defMdbFileName = System.IO.Path.GetFullPath(defMdbFileName);
            defLayerTable = "CadLayerCompar";
            defSymbolTable = "CadCompar";

            //初始化变量           
            inputFileName = Application.StartupPath + @"\..\Data\Cad\test.dxf";
            inputFileName = System.IO.Path.GetFullPath(inputFileName);
            outputFileName = Application.StartupPath + @"\..\Data\Cad\test.mdb";      //TianK20061227修改
            outputFileName = System.IO.Path.GetFullPath(outputFileName);
            fileType = "1";
            mdbFileName = Application.StartupPath + @"\..\Support\ConvertSymbol.mdb";
            mdbFileName = System.IO.Path.GetFullPath(mdbFileName);          
            layerTable = "CadLayerCompar";
            symbolTable = "CadCompar";

            //初始化系统状态
            //chkFileSet.Checked=true;
            if (File.Exists(mdbFileName))
            {
                txtImportFile.Text = inputFileName;
                txtExportFile.Text = outputFileName;
                txtFile.Text = mdbFileName;
                cmbLayer.Properties.Items.Add(layerTable);
                cmbSymbol.Properties.Items.Add(symbolTable);
                cmbLayer.Text = layerTable;
                cmbSymbol.Text = symbolTable;
            }
            else
            {
                defMdbFileName = "";
                defLayerTable = "";
                defSymbolTable = "";
                mdbFileName = "";
                layerTable = "";
                symbolTable = "";
            }
            this.refCmd();

        }

        #region 窗体生成代码
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuite = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.btnSym = new DevExpress.XtraEditors.SimpleButton();
            this.cmbSymbol = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnLayer = new DevExpress.XtraEditors.SimpleButton();
            this.cmbLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnFile = new DevExpress.XtraEditors.SimpleButton();
            this.txtFile = new DevExpress.XtraEditors.TextEdit();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.txtExportFile = new DevExpress.XtraEditors.TextEdit();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.txtImportFile = new DevExpress.XtraEditors.TextEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.btnFont = new DevExpress.XtraEditors.SimpleButton();
            this.textFont = new DevExpress.XtraEditors.TextEdit();
            this.txtSpace = new DevExpress.XtraEditors.TextEdit();
            this.txtHeight = new DevExpress.XtraEditors.TextEdit();
            this.txtAngle = new DevExpress.XtraEditors.TextEdit();
            this.txtObjNum = new DevExpress.XtraEditors.TextEdit();
            this.txtPrecision = new DevExpress.XtraEditors.TextEdit();
            this.txtMaxY = new DevExpress.XtraEditors.TextEdit();
            this.txtMinY = new DevExpress.XtraEditors.TextEdit();
            this.txtMaxX = new DevExpress.XtraEditors.TextEdit();
            this.txtMinX = new DevExpress.XtraEditors.TextEdit();
            this.cmbMapScale = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup11 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSymbol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExportFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImportFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textFont.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAngle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObjNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecision.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMapScale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnStart);
            this.layoutControl1.Controls.Add(this.btnQuite);
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(373, 371);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(2, 347);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(182, 22);
            this.btnStart.StyleController = this.layoutControl1;
            this.btnStart.TabIndex = 16;
            this.btnStart.Text = "开始导入";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click_1);
            // 
            // btnQuite
            // 
            this.btnQuite.Location = new System.Drawing.Point(188, 347);
            this.btnQuite.Name = "btnQuite";
            this.btnQuite.Size = new System.Drawing.Size(183, 22);
            this.btnQuite.StyleController = this.layoutControl1;
            this.btnQuite.TabIndex = 15;
            this.btnQuite.Text = "退出";
            this.btnQuite.Click += new System.EventHandler(this.btnQuite_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(369, 341);
            this.xtraTabControl1.TabIndex = 4;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.layoutControl2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(363, 312);
            this.xtraTabPage1.Text = "导入设置";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.progressBarControl1);
            this.layoutControl2.Controls.Add(this.btnSym);
            this.layoutControl2.Controls.Add(this.cmbSymbol);
            this.layoutControl2.Controls.Add(this.btnLayer);
            this.layoutControl2.Controls.Add(this.cmbLayer);
            this.layoutControl2.Controls.Add(this.btnFile);
            this.layoutControl2.Controls.Add(this.txtFile);
            this.layoutControl2.Controls.Add(this.btnExport);
            this.layoutControl2.Controls.Add(this.txtExportFile);
            this.layoutControl2.Controls.Add(this.btnImport);
            this.layoutControl2.Controls.Add(this.txtImportFile);
            this.layoutControl2.Controls.Add(this.radioGroup1);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(363, 312);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(5, 291);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Properties.ShowTitle = true;
            this.progressBarControl1.Size = new System.Drawing.Size(353, 16);
            this.progressBarControl1.StyleController = this.layoutControl2;
            this.progressBarControl1.TabIndex = 14;
            // 
            // btnSym
            // 
            this.btnSym.Location = new System.Drawing.Point(269, 239);
            this.btnSym.Name = "btnSym";
            this.btnSym.Size = new System.Drawing.Size(89, 22);
            this.btnSym.StyleController = this.layoutControl2;
            this.btnSym.TabIndex = 13;
            this.btnSym.Text = "...";
            this.btnSym.Click += new System.EventHandler(this.btnSym_Click);
            // 
            // cmbSymbol
            // 
            this.cmbSymbol.Location = new System.Drawing.Point(80, 239);
            this.cmbSymbol.Name = "cmbSymbol";
            this.cmbSymbol.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSymbol.Size = new System.Drawing.Size(185, 22);
            this.cmbSymbol.StyleController = this.layoutControl2;
            this.cmbSymbol.TabIndex = 12;
            // 
            // btnLayer
            // 
            this.btnLayer.Location = new System.Drawing.Point(269, 213);
            this.btnLayer.Name = "btnLayer";
            this.btnLayer.Size = new System.Drawing.Size(89, 22);
            this.btnLayer.StyleController = this.layoutControl2;
            this.btnLayer.TabIndex = 11;
            this.btnLayer.Text = "...";
            this.btnLayer.Click += new System.EventHandler(this.btnLayer_Click);
            // 
            // cmbLayer
            // 
            this.cmbLayer.Location = new System.Drawing.Point(80, 213);
            this.cmbLayer.Name = "cmbLayer";
            this.cmbLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLayer.Size = new System.Drawing.Size(185, 22);
            this.cmbLayer.StyleController = this.layoutControl2;
            this.cmbLayer.TabIndex = 10;
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(269, 187);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(89, 22);
            this.btnFile.StyleController = this.layoutControl2;
            this.btnFile.TabIndex = 9;
            this.btnFile.Text = "...";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(80, 187);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(185, 22);
            this.txtFile.StyleController = this.layoutControl2;
            this.txtFile.TabIndex = 8;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(269, 135);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(89, 22);
            this.btnExport.StyleController = this.layoutControl2;
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtExportFile
            // 
            this.txtExportFile.Location = new System.Drawing.Point(80, 135);
            this.txtExportFile.Name = "txtExportFile";
            this.txtExportFile.Size = new System.Drawing.Size(185, 22);
            this.txtExportFile.StyleController = this.layoutControl2;
            this.txtExportFile.TabIndex = 7;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(269, 109);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(89, 22);
            this.btnImport.StyleController = this.layoutControl2;
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtImportFile
            // 
            this.txtImportFile.Location = new System.Drawing.Point(80, 109);
            this.txtImportFile.Name = "txtImportFile";
            this.txtImportFile.Size = new System.Drawing.Size(185, 22);
            this.txtImportFile.StyleController = this.layoutControl2;
            this.txtImportFile.TabIndex = 5;
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = true;
            this.radioGroup1.Location = new System.Drawing.Point(5, 25);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 3;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "AutoCad dxf"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "南方Cass"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "青山智慧")});
            this.radioGroup1.Size = new System.Drawing.Size(353, 54);
            this.radioGroup1.StyleController = this.layoutControl2;
            this.radioGroup1.TabIndex = 4;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlGroup5,
            this.layoutControlGroup6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(363, 312);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(363, 84);
            this.layoutControlGroup3.Text = "导入数据类型";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.radioGroup1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(357, 58);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 84);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(363, 78);
            this.layoutControlGroup4.Text = "配置文件设置";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtImportFile;
            this.layoutControlItem3.CustomizationFormText = "导入文件：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem3.Text = "导入文件：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnImport;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(264, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtExportFile;
            this.layoutControlItem5.CustomizationFormText = "导出文件：";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem5.Text = "导出文件：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnExport;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(264, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup5";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem13});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 266);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(363, 46);
            this.layoutControlGroup5.Text = "进度";
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.progressBarControl1;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(357, 20);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "使用默认配置";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12});
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 162);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(363, 104);
            this.layoutControlGroup6.Text = "使用默认配置";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtFile;
            this.layoutControlItem7.CustomizationFormText = "配置文件：";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem7.Text = "配置文件：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnFile;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(264, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.cmbLayer;
            this.layoutControlItem9.CustomizationFormText = "图层对照表：";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem9.Text = "图层对照表：";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnLayer;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(264, 26);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.cmbSymbol;
            this.layoutControlItem11.CustomizationFormText = "符号对照表：";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem11.Text = "符号对照表：";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.btnSym;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(264, 52);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.layoutControl3);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(363, 312);
            this.xtraTabPage2.Text = "高级设置";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.btnFont);
            this.layoutControl3.Controls.Add(this.textFont);
            this.layoutControl3.Controls.Add(this.txtSpace);
            this.layoutControl3.Controls.Add(this.txtHeight);
            this.layoutControl3.Controls.Add(this.txtAngle);
            this.layoutControl3.Controls.Add(this.txtObjNum);
            this.layoutControl3.Controls.Add(this.txtPrecision);
            this.layoutControl3.Controls.Add(this.txtMaxY);
            this.layoutControl3.Controls.Add(this.txtMinY);
            this.layoutControl3.Controls.Add(this.txtMaxX);
            this.layoutControl3.Controls.Add(this.txtMinX);
            this.layoutControl3.Controls.Add(this.cmbMapScale);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem26});
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(608, 192, 250, 350);
            this.layoutControl3.Root = this.layoutControlGroup7;
            this.layoutControl3.Size = new System.Drawing.Size(363, 312);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(269, 285);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(89, 22);
            this.btnFont.StyleController = this.layoutControl3;
            this.btnFont.TabIndex = 15;
            this.btnFont.Text = "...";
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // textFont
            // 
            this.textFont.Location = new System.Drawing.Point(92, 285);
            this.textFont.Name = "textFont";
            this.textFont.Size = new System.Drawing.Size(173, 22);
            this.textFont.StyleController = this.layoutControl3;
            this.textFont.TabIndex = 14;
            // 
            // txtSpace
            // 
            this.txtSpace.EditValue = "75";
            this.txtSpace.Location = new System.Drawing.Point(92, 233);
            this.txtSpace.Name = "txtSpace";
            this.txtSpace.Size = new System.Drawing.Size(266, 22);
            this.txtSpace.StyleController = this.layoutControl3;
            this.txtSpace.TabIndex = 13;
            // 
            // txtHeight
            // 
            this.txtHeight.EditValue = "6";
            this.txtHeight.Location = new System.Drawing.Point(92, 207);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(266, 22);
            this.txtHeight.StyleController = this.layoutControl3;
            this.txtHeight.TabIndex = 12;
            // 
            // txtAngle
            // 
            this.txtAngle.EditValue = "Direction";
            this.txtAngle.Location = new System.Drawing.Point(92, 155);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(266, 22);
            this.txtAngle.StyleController = this.layoutControl3;
            this.txtAngle.TabIndex = 11;
            // 
            // txtObjNum
            // 
            this.txtObjNum.EditValue = "GeoObjNum";
            this.txtObjNum.Location = new System.Drawing.Point(92, 129);
            this.txtObjNum.Name = "txtObjNum";
            this.txtObjNum.Size = new System.Drawing.Size(266, 22);
            this.txtObjNum.StyleController = this.layoutControl3;
            this.txtObjNum.TabIndex = 10;
            // 
            // txtPrecision
            // 
            this.txtPrecision.EditValue = "1000";
            this.txtPrecision.Location = new System.Drawing.Point(92, 77);
            this.txtPrecision.Name = "txtPrecision";
            this.txtPrecision.Size = new System.Drawing.Size(87, 22);
            this.txtPrecision.StyleController = this.layoutControl3;
            this.txtPrecision.TabIndex = 8;
            // 
            // txtMaxY
            // 
            this.txtMaxY.EditValue = "20000000";
            this.txtMaxY.Location = new System.Drawing.Point(270, 51);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Size = new System.Drawing.Size(88, 22);
            this.txtMaxY.StyleController = this.layoutControl3;
            this.txtMaxY.TabIndex = 7;
            // 
            // txtMinY
            // 
            this.txtMinY.EditValue = "0";
            this.txtMinY.Location = new System.Drawing.Point(92, 51);
            this.txtMinY.Name = "txtMinY";
            this.txtMinY.Size = new System.Drawing.Size(87, 22);
            this.txtMinY.StyleController = this.layoutControl3;
            this.txtMinY.TabIndex = 6;
            // 
            // txtMaxX
            // 
            this.txtMaxX.EditValue = "20000000";
            this.txtMaxX.Location = new System.Drawing.Point(270, 25);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Size = new System.Drawing.Size(88, 22);
            this.txtMaxX.StyleController = this.layoutControl3;
            this.txtMaxX.TabIndex = 5;
            // 
            // txtMinX
            // 
            this.txtMinX.EditValue = "0";
            this.txtMinX.Location = new System.Drawing.Point(92, 25);
            this.txtMinX.Name = "txtMinX";
            this.txtMinX.Size = new System.Drawing.Size(87, 22);
            this.txtMinX.StyleController = this.layoutControl3;
            this.txtMinX.TabIndex = 4;
            // 
            // cmbMapScale
            // 
            this.cmbMapScale.EditValue = "1:500";
            this.cmbMapScale.Location = new System.Drawing.Point(270, 77);
            this.cmbMapScale.Name = "cmbMapScale";
            this.cmbMapScale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMapScale.Size = new System.Drawing.Size(88, 22);
            this.cmbMapScale.StyleController = this.layoutControl3;
            this.cmbMapScale.TabIndex = 9;
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.CustomizationFormText = "字体定义：";
            this.layoutControlItem26.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(357, 48);
            this.layoutControlItem26.Text = "字体定义：";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(50, 20);
            this.layoutControlItem26.TextToControlDistance = 5;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.CustomizationFormText = "layoutControlGroup7";
            this.layoutControlGroup7.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup7.GroupBordersVisible = false;
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup8,
            this.layoutControlGroup9,
            this.layoutControlGroup10,
            this.layoutControlGroup11});
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Size = new System.Drawing.Size(363, 312);
            this.layoutControlGroup7.Text = "layoutControlGroup7";
            this.layoutControlGroup7.TextVisible = false;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.CustomizationFormText = "坐标范围";
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem16,
            this.layoutControlItem17,
            this.layoutControlItem18,
            this.layoutControlItem19,
            this.layoutControlItem20,
            this.layoutControlItem21});
            this.layoutControlGroup8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup8.Size = new System.Drawing.Size(363, 104);
            this.layoutControlGroup8.Text = "坐标范围";
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.txtMinX;
            this.layoutControlItem16.CustomizationFormText = "  最小X:";
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem16.MaxSize = new System.Drawing.Size(0, 26);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(134, 26);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(178, 26);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.Text = "  最小X:";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.txtMaxX;
            this.layoutControlItem17.CustomizationFormText = "  最大X：";
            this.layoutControlItem17.Location = new System.Drawing.Point(178, 0);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(179, 26);
            this.layoutControlItem17.Text = "  最大X：";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.txtMinY;
            this.layoutControlItem18.CustomizationFormText = "  最小Y:";
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(178, 26);
            this.layoutControlItem18.Text = "  最小Y:";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.txtMaxY;
            this.layoutControlItem19.CustomizationFormText = "  最大Y:";
            this.layoutControlItem19.Location = new System.Drawing.Point(178, 26);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(179, 26);
            this.layoutControlItem19.Text = "  最大Y:";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.txtPrecision;
            this.layoutControlItem20.CustomizationFormText = "  精度：";
            this.layoutControlItem20.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(178, 26);
            this.layoutControlItem20.Text = "  精度：";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.cmbMapScale;
            this.layoutControlItem21.CustomizationFormText = " 原图比例尺：";
            this.layoutControlItem21.Location = new System.Drawing.Point(178, 52);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(179, 26);
            this.layoutControlItem21.Text = " 原图比例尺：";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.CustomizationFormText = "字段设置";
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem22,
            this.layoutControlItem23});
            this.layoutControlGroup9.Location = new System.Drawing.Point(0, 104);
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup9.Size = new System.Drawing.Size(363, 78);
            this.layoutControlGroup9.Text = "字段设置";
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.txtObjNum;
            this.layoutControlItem22.CustomizationFormText = "地物编码：";
            this.layoutControlItem22.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(357, 26);
            this.layoutControlItem22.Text = "地物编码：";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.txtAngle;
            this.layoutControlItem23.CustomizationFormText = "旋转角度：";
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(357, 26);
            this.layoutControlItem23.Text = "旋转角度：";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlGroup10
            // 
            this.layoutControlGroup10.CustomizationFormText = "字体设置";
            this.layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem24,
            this.layoutControlItem25});
            this.layoutControlGroup10.Location = new System.Drawing.Point(0, 182);
            this.layoutControlGroup10.Name = "layoutControlGroup10";
            this.layoutControlGroup10.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup10.Size = new System.Drawing.Size(363, 78);
            this.layoutControlGroup10.Text = "字体设置";
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.txtHeight;
            this.layoutControlItem24.CustomizationFormText = "字高比例：";
            this.layoutControlItem24.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(357, 26);
            this.layoutControlItem24.Text = "字高比例：";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.txtSpace;
            this.layoutControlItem25.CustomizationFormText = "字体间距比例：";
            this.layoutControlItem25.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(357, 26);
            this.layoutControlItem25.Text = "字体间距比例：";
            this.layoutControlItem25.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlGroup11
            // 
            this.layoutControlGroup11.CustomizationFormText = "配置文件";
            this.layoutControlGroup11.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem27,
            this.layoutControlItem28});
            this.layoutControlGroup11.Location = new System.Drawing.Point(0, 260);
            this.layoutControlGroup11.Name = "layoutControlGroup11";
            this.layoutControlGroup11.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup11.Size = new System.Drawing.Size(363, 52);
            this.layoutControlGroup11.Text = "配置文件";
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.textFont;
            this.layoutControlItem27.CustomizationFormText = "字体：";
            this.layoutControlItem27.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem27.Text = "字体：";
            this.layoutControlItem27.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.btnFont;
            this.layoutControlItem28.CustomizationFormText = "layoutControlItem28";
            this.layoutControlItem28.Location = new System.Drawing.Point(264, 0);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem28.Text = "layoutControlItem28";
            this.layoutControlItem28.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem28.TextToControlDistance = 0;
            this.layoutControlItem28.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem14,
            this.layoutControlItem15});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(373, 371);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.xtraTabControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(373, 345);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.btnQuite;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(186, 345);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(187, 26);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.btnStart;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 345);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(186, 26);
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 52);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(357, 141);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(373, 371);
            this.Controls.Add(this.layoutControl1);
            this.Name = "MainForm";
            this.Text = "数据导入";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSymbol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExportFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImportFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textFont.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAngle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObjNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecision.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMapScale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
       

        //控制导入按钮的状态
        private void refCmd()
        {
            if (txtImportFile.Text != "" && txtExportFile.Text != "" && txtFile.Text != "" && cmbLayer.Text != "" && cmbSymbol.Text != "")
                btnStart.Enabled = true;
            else
                btnStart.Enabled = false;
        }

        //根据系统数据库刷新combobox列表
        private void refComboTable()
        {
            if (File.Exists(mdbFileName))
            {
                cmbLayer.Properties.Items.Clear();
                cmbSymbol.Properties.Items.Clear();

                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IWorkspace pWs = pWsF.OpenFromFile(mdbFileName, 0);
                IEnumDatasetName enumDatasetName = pWs.get_DatasetNames(esriDatasetType.esriDTTable);

                if (enumDatasetName == null) return;

                int i = 0;
                int j = 0;
                IDatasetName datasetName = enumDatasetName.Next();
                while (datasetName != null)
                {
                    if (fileType == "1")
                    {
                        if (CheckIsValidTable(mdbFileName, datasetName, "CadLayer"))
                        {
                            this.cmbLayer.Properties.Items.Add(datasetName.Name);
                            if (i == 0) cmbLayer.Text = datasetName.Name;
                            i++;
                        }
                        if (CheckIsValidTable(mdbFileName, datasetName, "SymbolCode"))
                        {
                            this.cmbSymbol.Properties.Items.Add(datasetName.Name);
                            if (j == 0) cmbSymbol.Text = datasetName.Name;
                            j++;
                        }
                    }
                    else if (fileType == "3")
                    {
                        if (CheckIsValidTable(mdbFileName, datasetName, "SMSLayerName"))
                        {
                            this.cmbLayer.Properties.Items.Add(datasetName.Name);
                            if (i == 0) cmbLayer.Text = datasetName.Name;
                            i++;
                        }
                        if (CheckIsValidTable(mdbFileName, datasetName, "PROGNAME"))
                        {
                            this.cmbSymbol.Properties.Items.Add(datasetName.Name);
                            if (j == 0) cmbSymbol.Text = datasetName.Name;
                            j++;
                        }
                    }
                    datasetName = enumDatasetName.Next();
                }
            }
        }

        private void cmbLayer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            layerTable = cmbLayer.Text;
            this.refCmd();
        }

        private void cmbSymbol_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            symbolTable = cmbSymbol.Text;
            this.refCmd();
        }

        public static bool CheckIsValidTable(string mdbFile, IDatasetName datasetname, string validField)
        {
            bool rtnValue = false;
            string fieldName = "";
            int fieldCount = 0;
            ITable pTable;
            IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
            IFeatureWorkspace pFWs = pWsF.OpenFromFile(mdbFile, 0) as IFeatureWorkspace;
            pTable = pFWs.OpenTable(datasetname.Name.ToString());           
            fieldCount = pTable.Fields.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                fieldName = pTable.Fields.get_Field(i).Name;
                if (fieldName == validField)
                    rtnValue = true;
            }
            return rtnValue;
        }

    
        private GisWriteFromDxf gisWriter;
        private CadWriteData cwd;
        //private SMSRead smsReader;

        private void beginImport()
        {
            WaitForm.Start("准备导入，请稍后...", new Size(340, 50));
            IDxfImport iMain = new ImportMain();
            iMain.StrObjNum = txtObjNum.Text;
            iMain.StrAngle = txtAngle.Text;
            if (cmbMapScale.Text == "1:500") iMain.MapScale = "500";
            if (cmbMapScale.Text == "1:1000") iMain.MapScale = "1000";
            iMain.InputFileName = inputFileName;
            iMain.OutputFileName = outputFileName;
            iMain.FileType = fileType;
            iMain.MdbFileName = mdbFileName;
            iMain.LayerTable = layerTable;
            iMain.SymbolTable = symbolTable;
            iMain.HeightScale = double.Parse(txtHeight.Text);
            iMain.SpaceScale = double.Parse(txtSpace.Text);
            iMain.ImportInit();
            if (this.fileType == "1")
            {
                //cwd = iMain.CadWriter;
                //cwd.ReadEvent += new ReadCadHandler(cwd_ReadEvent);
                //cwd.readPlEvent += new ReadPlHandler(cwd_readPlEvent);

                //gisWriter = iMain.GisWriter;
                //gisWriter.WriteEvent += new WriteGisFromDxfHandler(gisWriter_WriteEvent);
                //gisWriter.WriteMsgEvent += new WriteStateHandler(gisWriter_WriteMsgEvent);
                //gisWriter.WritePlineEvent += new WritePlineHandler(gisWriter_WritePlineEvent);
                //gisWriter.WriteFinishedEvent += new WriteFinishedHandler(gisWriter_WriteFinishedEvent);
                //this.pgbImportstat.Value = 0;
            }
            iMain.ImportRun();
        }
        private void cwd_ReadEvent(int rEntCount, int wEntCount)
        {
            //readStat.Visible = true;
            //readStat.Update();

            //this.Refresh();
            //lblProcessStat.Text = "状态：读取数据 " + rEntCount.ToString();
            //lblProcessStat.Update();
        }
        private void gisWriter_WriteEvent(int rEntCount, int wEntCount)
        {
            //readStat.Visible = false;

            //lblProcessStat.Text = "状态：写入数据 " + wEntCount.ToString() + "/" + rEntCount.ToString();
            //lblProcessStat.Update();

            //this.pgbImportstat.Minimum = 0;
            //this.pgbImportstat.Maximum = rEntCount;
            //if (wEntCount > rEntCount)
            //    this.pgbImportstat.Value = rEntCount;
            //else
            //    this.pgbImportstat.Value = wEntCount;
            //this.pgbImportstat.Update();
        }
        private void cwd_readPlEvent(string typestr, int plcount)
        {
            //if (typestr == "POLYLINE" || typestr == "LWPOLYLINE")
            //    this.processPlStat.Text = "处理PolyLine点：" + plcount.ToString();
            //else
            //    this.processPlStat.Text = "处理" + typestr;

            //this.processPlStat.Update();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdImport = new OpenFileDialog();
            if (fileType == "1")
                ofdImport.Filter = "AutoCad dxf file(*.dxf)|*.dxf";
            else
                ofdImport.Filter = "SMS file(*.SAS)|*.SAS|Txt file(*.txt)|*.txt|All file(*.*)|*.*";
            ofdImport.ShowDialog();
            if (ofdImport.FileName.Length > 0)
            {
                inputFileName = ofdImport.FileName;
                txtImportFile.Text = inputFileName;

                //dxf文件格式设置边界
                //if (fileType == "1") setLimite();
            }
            this.refCmd();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdImport = new SaveFileDialog();
            sfdImport.Filter = "Access file(*.mdb)|*.mdb";
            sfdImport.ShowDialog();
            if (sfdImport.FileName.Length > 0)
            {
                outputFileName = sfdImport.FileName;
                txtExportFile.Text = outputFileName;
            }
            this.refCmd();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdImport = new OpenFileDialog();
            ofdImport.Filter = "Access file(*.mdb)|*.mdb";
            if (ofdImport.ShowDialog() != DialogResult.OK) return;
            if (ofdImport.FileName.Length > 0)
            {
                mdbFileName = ofdImport.FileName;
                txtFile.Text = mdbFileName;
                this.refComboTable();
            }
            this.refCmd();
        }

        private void btnLayer_Click(object sender, EventArgs e)
        {
            if (cmbLayer.Text.Length == 0)
                MessageBox.Show("文件名不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                LayerCompar lc = new LayerCompar(mdbFileName, layerTable, fileType);
                lc.ShowDialog();
            }
        }

        private void btnSym_Click(object sender, EventArgs e)
        {
            if (cmbSymbol.Text.Length == 0)
                MessageBox.Show("文件名不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                SymbolCompar lc = new SymbolCompar(mdbFileName, symbolTable, fileType);
                lc.ShowDialog();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.beginImport();
        }

        private void btnQuite_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            System.GC.Collect();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            this.Refresh();
            this.beginImport();
        }

       
      

    }
}
