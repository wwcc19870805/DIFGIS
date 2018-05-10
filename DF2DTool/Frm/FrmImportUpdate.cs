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
using ESRI.ArcGIS.Geometry;
using System.IO;
using DFCommon.Class;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Collections;
using DF2DTool.Class;
using ESRI.ArcGIS.Carto;
using DF2DTool.Interface;
using DFWinForms.Class;

namespace DF2DTool.Frm
{
    public partial class FrmImportUpdate : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btn_cancel;
        private DevExpress.XtraEditors.SimpleButton btn_import;
        private DevExpress.XtraEditors.CheckEdit ce_backupMdb;
        private DevExpress.XtraEditors.SimpleButton btn_mdbOpen;
        private DevExpress.XtraEditors.SimpleButton btn_dxfOpen;
        private DevExpress.XtraEditors.TextEdit te_mdb;
        private DevExpress.XtraEditors.TextEdit te_dxf;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    
        public FrmImportUpdate()
        {
            InitializeComponent();
            defMdbFileName = Application.StartupPath + @"\..\Support\ConvertSymbol.mdb";
            defMdbFileName = System.IO.Path.GetFullPath(defMdbFileName);
            defLayerTable = "CadLayerCompar";
            defSymbolTable = "CadCompar";
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_import = new DevExpress.XtraEditors.SimpleButton();
            this.ce_backupMdb = new DevExpress.XtraEditors.CheckEdit();
            this.btn_mdbOpen = new DevExpress.XtraEditors.SimpleButton();
            this.btn_dxfOpen = new DevExpress.XtraEditors.SimpleButton();
            this.te_mdb = new DevExpress.XtraEditors.TextEdit();
            this.te_dxf = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ce_backupMdb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_mdb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_dxf.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_cancel);
            this.layoutControl1.Controls.Add(this.btn_import);
            this.layoutControl1.Controls.Add(this.ce_backupMdb);
            this.layoutControl1.Controls.Add(this.btn_mdbOpen);
            this.layoutControl1.Controls.Add(this.btn_dxfOpen);
            this.layoutControl1.Controls.Add(this.te_mdb);
            this.layoutControl1.Controls.Add(this.te_dxf);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(382, 113);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(195, 86);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(182, 22);
            this.btn_cancel.StyleController = this.layoutControl1;
            this.btn_cancel.TabIndex = 10;
            this.btn_cancel.Text = "退出";
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_import
            // 
            this.btn_import.Location = new System.Drawing.Point(5, 86);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(186, 22);
            this.btn_import.StyleController = this.layoutControl1;
            this.btn_import.TabIndex = 9;
            this.btn_import.Text = "开始导入";
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // ce_backupMdb
            // 
            this.ce_backupMdb.Location = new System.Drawing.Point(5, 57);
            this.ce_backupMdb.Name = "ce_backupMdb";
            this.ce_backupMdb.Properties.Caption = "备份数据库";
            this.ce_backupMdb.Size = new System.Drawing.Size(372, 19);
            this.ce_backupMdb.StyleController = this.layoutControl1;
            this.ce_backupMdb.TabIndex = 8;
            // 
            // btn_mdbOpen
            // 
            this.btn_mdbOpen.Location = new System.Drawing.Point(311, 31);
            this.btn_mdbOpen.Name = "btn_mdbOpen";
            this.btn_mdbOpen.Size = new System.Drawing.Size(66, 22);
            this.btn_mdbOpen.StyleController = this.layoutControl1;
            this.btn_mdbOpen.TabIndex = 7;
            this.btn_mdbOpen.Text = "...";
            this.btn_mdbOpen.Click += new System.EventHandler(this.btn_mdbOpen_Click);
            // 
            // btn_dxfOpen
            // 
            this.btn_dxfOpen.Location = new System.Drawing.Point(311, 5);
            this.btn_dxfOpen.Name = "btn_dxfOpen";
            this.btn_dxfOpen.Size = new System.Drawing.Size(66, 22);
            this.btn_dxfOpen.StyleController = this.layoutControl1;
            this.btn_dxfOpen.TabIndex = 6;
            this.btn_dxfOpen.Text = "...";
            this.btn_dxfOpen.Click += new System.EventHandler(this.btn_dxfOpen_Click);
            // 
            // te_mdb
            // 
            this.te_mdb.Location = new System.Drawing.Point(80, 31);
            this.te_mdb.Name = "te_mdb";
            this.te_mdb.Size = new System.Drawing.Size(227, 22);
            this.te_mdb.StyleController = this.layoutControl1;
            this.te_mdb.TabIndex = 5;
            // 
            // te_dxf
            // 
            this.te_dxf.Location = new System.Drawing.Point(80, 5);
            this.te_dxf.Name = "te_dxf";
            this.te_dxf.Size = new System.Drawing.Size(227, 22);
            this.te_dxf.StyleController = this.layoutControl1;
            this.te_dxf.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(382, 113);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(382, 81);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.te_dxf;
            this.layoutControlItem1.CustomizationFormText = "更新文件dxf:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(306, 26);
            this.layoutControlItem1.Text = "更新文件dxf:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.te_mdb;
            this.layoutControlItem2.CustomizationFormText = "更新数据库：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(306, 26);
            this.layoutControlItem2.Text = "更新数据库：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_dxfOpen;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(306, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_mdbOpen;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(306, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ce_backupMdb;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(376, 23);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 81);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(382, 32);
            this.layoutControlGroup3.Text = "layoutControlGroup3";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btn_import;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(190, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btn_cancel;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(190, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(186, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // FrmImportUpdate
            // 
            this.ClientSize = new System.Drawing.Size(382, 113);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmImportUpdate";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据更新导入";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ce_backupMdb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_mdb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_dxf.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        private string dxfName;
        private string mdbName;
        private string layerTable;
        private string symbolTable;
        private string defMdbFileName;
        private string defLayerTable;
        private string defSymbolTable;
        private string mdbFileName;
        private IFeatureClass fcUpdate;
        private IFeatureWorkspace pCurWorkspace;
        private GisWriteFromDxf gisWriter;
        private CadWriteData cwd;
        private ArrayList m_arrVertex = new ArrayList();
        private string[] str = new string[2];
        private StreamReader sr;
        readonly static string GEOOBJNUM = "GEOOBJNUM";
        readonly static string DIRECTIONLINE = "867012,866012,865012,853002,852202,852102,843202,843102,842012,841202,841102,655202,655102,654002,652002,648022,646102642012,642022,643002,644102,644202,644302,645102,645202,645302,645402,646012,646022,633002,634102,634202,453102,453202,454102,454112,431012,362402,325202,325102,318032,245002,244202,243202,242002,241402,241102";
        private void btn_dxfOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "AutoCad dxf file(*.dxf)|*.dxf";
            ofd.RestoreDirectory = true;
            ofd.ShowDialog();
            if (ofd.FileName.Length > 0)
            {
                dxfName = ofd.FileName;
                te_dxf.Text = dxfName;
                setLimite();
            }
            refCmd();
        }

        private void setLimite()
        {
            CadRead cadReadLimite = new CadRead();
            cadReadLimite.InputFileName = dxfName;
            cadReadLimite = null;
        }

        private void refCmd()
        {
            if (te_dxf.Text != "" && te_mdb.Text != "") btn_import.Enabled = true;
            else btn_import.Enabled = false;
        }

        private void btn_mdbOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Access file(*.mdb)|*.mdb";
            ofd.ShowDialog();
            if (ofd.FileName.Length > 0)
            {
                mdbName = ofd.FileName;
                te_mdb.Text = mdbName;
            }
            this.refCmd();
            this.refCoordinateArea();
        }

        /// <summary>
        /// 获取打开的工作空间的相关信息
        /// </summary>
        private void refCoordinateArea()
        {
            ISpatialReference pSr;
            IFields pFields;
            double minX, minY, maxX, maxY, dScale = 0, dFalseX = 0, dFalseY = 0;
            if (File.Exists(mdbName))
            {
                this.fcUpdate = GetUpdateRegionFC();
                IEnumDatasetName enumDatasetName = (pCurWorkspace as IWorkspace).get_DatasetNames(esriDatasetType.esriDTFeatureClass);
                IDatasetName datasetName = enumDatasetName.Next();
                if (datasetName != null)
                {
                    string shapeField = this.fcUpdate.ShapeFieldName;
                    pFields = this.fcUpdate.Fields;
                    pSr = pFields.get_Field(pFields.FindField(shapeField)).GeometryDef.SpatialReference;
                    pSr.GetDomain(out minX, out maxX, out minY, out maxY);
                    pSr.GetFalseOriginAndUnits(out dFalseX, out dFalseY, out dScale);
                }
            }
        }

        private IFeatureClass GetUpdateRegionFC()
        {
            try
            {
                string path = Config.GetConfigValue("2DMdbPipe");
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IFeatureWorkspace pFWs = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
                this.pCurWorkspace = pFWs;
                IFeatureDataset pFDs = pFWs.OpenFeatureDataset("Assi_10");
                if (pFDs == null) return null;
                IEnumDataset pEnumDs = pFDs.Subsets;
                IDataset fDs;
                IFeatureClass fc = null;
                while ((fDs = pEnumDs.Next()) != null)
                {
                    if (fDs.Name == "UpdataRegionPLY500")
                    {
                        fc = fDs as IFeatureClass;
                    }

                }
                return fc;
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            int index1, indexMark;
            string strFileName;
            IFeature pFeature;
            IFeatureCursor pCursor;
            IQueryFilter pFilter = new QueryFilter();
            try
            {
                WaitForm.Start("正在准备导入，请稍后...");
                strFileName = dxfName.Substring(dxfName.LastIndexOf("\\") + 1);
                strFileName = strFileName.Remove(strFileName.LastIndexOf(@"."));
                index1 = fcUpdate.FindField("REGIONNAME");
                indexMark = fcUpdate.FindField("Mark");

                pFilter.WhereClause = string.Format("REGIONNAME = '{0}'", strFileName);
                pCursor = fcUpdate.Update(pFilter, true);
                if (pCursor != null)
                {
                    pFeature = pCursor.NextFeature();
                    if (pFeature == null) { WaitForm.Stop(); XtraMessageBox.Show("找不到与导入文件名相对应的区域范围，请确认文件名没有被更改过！"); return; }

                    IPolygon pPlyRegion = this.GetRegionPolygon(dxfName);
                    IPolygon ply = pFeature.Shape as IPolygon;
                    if (Math.Abs((pPlyRegion as IArea).Area - (pFeature.Shape as IArea).Area) < 0.08)
                    {
                        //备份数据库
                        if (this.ce_backupMdb.Checked)
                        {
                            WaitForm.SetCaption("正在备份数据库，请稍后...");
                            string strRegionName = String.Format("{0}{1}点{2}分", DateTime.Now.ToLongDateString(), DateTime.Now.Hour, DateTime.Now.Minute);
                            string strCopyPath = mdbName.Insert(mdbName.LastIndexOf("."), strRegionName) + ".temp";
                            FileInfo fileCopy = new FileInfo(strCopyPath);
                            if (!fileCopy.Exists) FileCopy(mdbName, strCopyPath);
                            else { XtraMessageBox.Show("数据库临时备份文件已存在！"); return; }

                        }

                        //删除范围线内的旧数据
                        WaitForm.SetCaption("正在删除范围线内的旧数据...");
                        DeleteOldFeatures(pPlyRegion);
                        BeginImport();
                    }
                    else
                    {
                        XtraMessageBox.Show("导入文件的范围线与导出时不一致，请确认范围线图层名为“更新范围”,并且更新范围线没有被更改过！");
                        return;
                    }
                }
                //记录更新范围当前的状态
                pFilter.WhereClause = string.Format("REGIONNAME = '{0}'", strFileName);
                pCursor = fcUpdate.Update(pFilter, true);
                pFeature = pCursor.NextFeature();
                if (indexMark > -1)
                {
                    pFeature.set_Value(indexMark, "已导入未合并");
                    pFeature.Store();
                }
                this.btn_import.Enabled = false;
                XtraMessageBox.Show("导入数据库成功！");
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
                return;
            }
           
        }

        private IPolygon GetRegionPolygon(string fileName)
        {
            try
            {
                object Missing = Type.Missing;
                double dblX, dblY;
                string strXY;
                IPoint p;
                IPolygon pPlyRegion = new PolygonClass();
                IPointCollection pPointCol = pPlyRegion as IPointCollection;

                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, Encoding.Default);
                this.Read();
                for (int i = 0; i < m_arrVertex.Count;i++ )
                {
                    strXY = m_arrVertex[i].ToString();
                    dblX = double.Parse(strXY.Split(new char[] { ',' })[0]);
                    dblY = double.Parse(strXY.Split(new char[] { ',' })[1]);
                    p = new PointClass();
                    p.PutCoords(dblX, dblY);
                    pPointCol.AddPoint(p, ref Missing, ref Missing);
                }
                m_arrVertex.Clear();
                return pPlyRegion;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        
        /// <summary>
        /// 文件备份
        /// </summary>
        /// <param name="mdbName"></param>
        /// <param name="path"></param>
        private void FileCopy(string mdbName,string path)
        {
            FileInfo fSrcFile = new FileInfo(mdbName);
            FileInfo fCopyFile = new FileInfo(path);
            fSrcFile.CopyTo(path);
        }
        private void DeleteOldFeatures(IPolygon ply)
        {
            ArrayList pFcNameList = CommonFunction.GetFeactureClassName_From_AccessWorkSpace(pCurWorkspace as IWorkspace, "500");
            ClipWorkspace(pCurWorkspace as IWorkspace,pFcNameList,ply);
        }

        private void BeginImport()
        {
            WaitForm.SetCaption("正在初始化导入配置，请稍后...");
            IDxfImport iMain = new ImportMain();
            iMain.StrObjNum = "GeoObjNum";
            iMain.StrAngle = "Dirction";
            iMain.MapScale = "500";
            iMain.InputFileName = dxfName;
            iMain.OutputFileName = mdbName;
            iMain.FileType = "1";
            iMain.MdbFileName = defMdbFileName;
            iMain.LayerTable = defLayerTable;
            iMain.SymbolTable = defSymbolTable;
            iMain.HeightScale = 6;
            iMain.SpaceScale = 75;

            iMain.ImportInit();
            cwd = iMain.CadWriter;
            gisWriter = iMain.GisWriter;
            iMain.ImportRun();       
        }

        private void Read()
        {
            try
            {
                while (sr.Peek() != -1)
                {
                    str = ReadPair();
                    if(str[1] == "SECTION")
                    {
                        str = ReadPair();
                        if (str[1] == "ENTITIES")
                        {
                            ReadEntities();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
          
        }

        private string[] ReadPair()
        {
            string code = sr.ReadLine().Trim();
            string codedata = sr.ReadLine();
            string[] result = new string[2] { code, codedata };
            return result;
        }
        private void ReadEntities()
        {
            while (str[1] != "ENDSEC")
            {
                str = ReadPair();
                if (str[1] == "POLYLINE")
                {
                    ReadPolyline();
                }
                if (str[1] == "LWPOLYLINE")
                {
                    ReadLWPolyline();
                }
            }
        }
        private void ReadPolyline()
        {
            string strX = "";
            string strY = "";
            while (str[1] != "ENDSEC" && str[1] != "SEQEND")
            {
                str = ReadPair();
                if (str[0] == "8" && str[1] == "更新范围")
                {
                    while (str[1] != "SEQEND")
                    {
                        if (str[0] == "0" && str[1] == "VERTEX")
                        {           
                            str = ReadPair();       
                            while (str[0] != "0" && str[1] != "VERTEX")
                            {
                                if (str[0] == "10") strX = str[1];
                                else if (str[0] == "20") strY = str[1];
                                if (strX != "" && strY != "")
                                {
                                    m_arrVertex.Add(String.Format("{0},{1}", strX, strY));
                                    strX = "";
                                    strY = "";
                                }
                                str = ReadPair();
                            }
                           
                        }
                        else
                        {
                            str = ReadPair();
                        }
                    }
                    break;
                }
            }
        }
        private void ReadLWPolyline()
        {
            string strX = "", strY = "";
            int count;
            while (str[1] != "ENDSEC")
            {
                str = ReadPair();
                if (str[0] == "8" && str[1] == "更新范围")
                {
                    str = ReadPair();
                    while (str[0] != "90")
                    {
                        str = ReadPair();
                    }
                    count = int.Parse(str[1]);
                    while (count != 0)
                    {
                        if (str[0] == "10") strX = str[1];
                        else if (str[0] == "20") strY = str[1];
                        if (strX != "" && strY != "")
                        {
                            m_arrVertex.Add(String.Format("{0},{1}", strX, strY));
                            strX = "";
                            strY = "";
                            count--;
                        }
                        str = ReadPair();
                    }
                    break;
                }
            }
        }
        private void ClipWorkspace(IWorkspace inWorkspace, ArrayList inArray, IPolygon clipPolygon)
        {
            string pFCName = "";
            try
            {
                for (int i = 0; i < inArray.Count; i++)
                {
                    pFCName = inArray[i].ToString();
                    if (pFCName == "UpdataRegionPLY500") 
                        continue;//删除的时候不要删除更新范围要素类
                    IFeatureClass fc = (inWorkspace as IFeatureWorkspace).OpenFeatureClass(pFCName);
                    if (fc == null) continue;
                    ClipFeatureClass(fc,clipPolygon);
                }
            }
            catch (System.Exception ex)
            {
                return;
            }
           
        }

        /// <summary>
        /// 裁切一个FeatureClass,在原来基础上裁切，不产生新的FeatureClass
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="ply"></param>
        private bool ClipFeatureClass(IFeatureClass fc, IPolygon ply)
        {
            try
            {
                esriGeometryType geometryType = fc.ShapeType;
                IDataset pDs = fc as IDataset;
                string strFcName = pDs.Name;
                ISpatialFilter filter = new SpatialFilter();
                filter.Geometry = ply;
                filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                IFeatureCursor cursor;
                IFeature feature;
                if (fc.FeatureType == esriFeatureType.esriFTAnnotation)
                {                 
                    cursor = fc.Update(filter, true);
                    feature = cursor.NextFeature();
                    while (feature != null)
                    {
                        if (ClipAnnotationFeature(feature, ply)) feature.Delete();
                        feature = cursor.NextFeature();
                    }   
                }
                else if (fc.FeatureType == esriFeatureType.esriFTSimple)
                {
                    cursor = fc.Update(filter, true);
                    feature = cursor.NextFeature();
                    while (feature != null)
                    {
                        if (ClipFeature(ref feature, geometryType, ply)) feature.Delete();
                        else feature.Store();
                        feature = cursor.NextFeature();
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        private bool ClipAnnotationFeature(IFeature feature, IPolygon ply)
        {
            IAnnotationFeature afin = feature as IAnnotationFeature;
            IRelationalOperator ro = ply as IRelationalOperator;
            IPoint pPoint;
            IEnvelope pEnvelope;
            pPoint = new PointClass();
            pEnvelope = afin.Annotation.Geometry.Envelope;
            pPoint.PutCoords((pEnvelope.XMin + pEnvelope.XMax) / 2, (pEnvelope.YMin + pEnvelope.YMax) / 2);

            if (ro.Contains(pPoint)) return true;
            else
                return false;
        }
        private bool ClipFeature(ref IFeature feature, esriGeometryType geometryType, IPolygon ply)
        {
            int index;
            string geoObjNum = "";
            bool retunValue = false;
            IGeometry resultGeo = null;
            try
            {
                index = fcUpdate.Fields.FindField(GEOOBJNUM);
                if (index != -1)
                {
                    geoObjNum = feature.get_Value(index).ToString();
                }
                IGeometry pFeaShape = feature.ShapeCopy;
                retunValue = ClipGeometry(ref pFeaShape, geometryType, ply, geoObjNum);
                feature.Shape = pFeaShape;

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return retunValue;
        }

        /// <summary>
        /// 裁切一个几何对象，不需要传出
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="geometryType"></param>
        /// <param name="ply"></param>
        /// <param name="geoObjNum"></param>
        /// <returns></returns>
        private bool ClipGeometry(ref IGeometry inGeometry, esriGeometryType geometryType, IPolygon clipPolygon, string GeoObjNum)
        {
            IPolycurve pPolyCurve;

            bool returnValue = false;
            IGeometry tempGeometry = null;
            IGeometry tempGeometry1 = null;
            ITopologicalOperator topologyOper = clipPolygon as ITopologicalOperator;
            topologyOper.Simplify();
            IRelationalOperator relationOper = clipPolygon as IRelationalOperator;

            IZAware zIn = inGeometry as IZAware;
            IMAware mAware;
            IPoint pPoint, pPoint1;
            IPointCollection pPtC;
            ICurve pCurve;
            IGeometryCollection pGeoC;
            bool HasM = true;

            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();	//返回击中位置
            IPath pPath;
            IPointCollection pPointC;
            IPoint P1, P2;

            switch (geometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    //如果点包含在多边形中
                    if (relationOper.Contains(inGeometry))
                    {
                        //tempGeometry = inGeometry;
                        returnValue = true;
                    }
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    //'如果多义线穿越多边形或包含在多边形中
                    if (relationOper.Contains(inGeometry))
                    {
                        returnValue = true;
                        //tempGeometry = inGeometry;
                    }
                    else //if(relationOper.Crosses(inGeometry))    2008.1.29 TianK 注释掉  解决了裁切丢数据的问题
                    {
                        pPolyCurve = inGeometry as IPolycurve;
                        pPolyCurve.Generalize(0.03);

                        if (DIRECTIONLINE.IndexOf(GeoObjNum) != -1)     //如果是有方向的线性地物
                        {
                            pHitTest = (IHitTest)inGeometry;

                            mAware = inGeometry as IMAware;                   ////2008.1.27TianK 修改 确保裁切后的线不反向
                            if (mAware.MAware == true)
                            {
                                HasM = true;
                                pPtC = inGeometry as IPointCollection;
                                for (int i = 0; i < pPtC.PointCount; i++)
                                {
                                    pPoint = pPtC.get_Point(i);
                                    pPoint.M = i;
                                    pPtC.UpdatePoint(i, pPoint);
                                }
                            }
                            else
                            {
                                HasM = false;
                                mAware.MAware = true;
                                pPtC = inGeometry as IPointCollection;
                                for (int i = 0; i < pPtC.PointCount; i++)
                                {
                                    pPoint = pPtC.get_Point(i);
                                    pPoint.M = i;
                                    pPtC.UpdatePoint(i, pPoint);
                                }
                            }
                            tempGeometry1 = topologyOper.Intersect(inGeometry, esriGeometryDimension.esriGeometry1Dimension);
                            tempGeometry = (inGeometry as ITopologicalOperator).Difference(tempGeometry1);
                            ////2008.1.27TianK 修改 确保裁切后的线不反向
                            mAware = tempGeometry as IMAware;
                            mAware.MAware = true;
                            pGeoC = tempGeometry as IGeometryCollection;
                            for (int j = 0; j < pGeoC.GeometryCount; j++)
                            {
                                pPtC = pGeoC.get_Geometry(j) as IPointCollection;
                                pPoint = pPtC.get_Point(0);
                                pPoint1 = pPtC.get_Point(pPtC.PointCount - 1);
                                if (double.IsNaN(pPoint.M))
                                {
                                    if (pHitTest.HitTest(pPoint, 0.01, esriGeometryHitPartType.esriGeometryPartBoundary,
                                        hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                                    {
                                        pPath = (IPath)(inGeometry as IGeometryCollection).get_Geometry(partIndex);
                                        pPointC = (IPointCollection)pPath;//接口跳转

                                        P1 = pPointC.get_Point(segmentIndex);
                                        P2 = pPointC.get_Point(segmentIndex + 1);
                                        AddM_P1_P2(P1, P2, ref pPoint);
                                        pPtC.UpdatePoint(0, pPoint);
                                    }
                                }
                                if (double.IsNaN(pPoint1.M))
                                {
                                    if (pHitTest.HitTest(pPoint1, 0.01, esriGeometryHitPartType.esriGeometryPartBoundary,
                                        hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                                    {
                                        pPath = (IPath)(inGeometry as IGeometryCollection).get_Geometry(partIndex);
                                        pPointC = (IPointCollection)pPath;//接口跳转

                                        P1 = pPointC.get_Point(segmentIndex);
                                        P2 = pPointC.get_Point(segmentIndex + 1);
                                        AddM_P1_P2(P1, P2, ref pPoint1);
                                        pPtC.UpdatePoint(pPtC.PointCount - 1, pPoint1);
                                    }
                                }
                                pPoint = pPtC.get_Point(0);
                                pPoint1 = pPtC.get_Point(pPtC.PointCount - 1);
                                if (pPoint.M >= pPoint1.M)
                                {
                                    pCurve = pGeoC.get_Geometry(j) as ICurve;
                                    pCurve.ReverseOrientation();
                                }
                                if (HasM)
                                {
                                    for (int i = 0; i < pPtC.PointCount; i++)
                                    {
                                        pPoint = pPtC.get_Point(i);
                                        pPoint.M = 0;
                                        pPtC.UpdatePoint(i, pPoint);
                                    }
                                }
                                else
                                {
                                    mAware.MAware = false;
                                }
                            }
                        }
                        else
                        {
                            int i;
                            IPoint p;
                            tempGeometry1 = topologyOper.Intersect(inGeometry, esriGeometryDimension.esriGeometry1Dimension);
                            tempGeometry = (inGeometry as ITopologicalOperator).Difference(tempGeometry1);
                        }

                        if (zIn.ZAware)
                        {
                            IZAware zOut = tempGeometry as IZAware;
                            zOut.ZAware = zIn.ZAware;

                            IZ iz = tempGeometry as IZ;
                            try
                            {
                                iz.CalculateNonSimpleZs();
                            }
                            catch
                            {
                                //该值应该传入，表示如果栽得的线如果不能插值出Z值赋一个默认值，或再次计算
                                //可以根据inGeometry计算
                                iz.SetConstantZ(0);
                            }
                        }
                    }
                    break;
                case esriGeometryType.esriGeometryMultipoint:
                case esriGeometryType.esriGeometryPolygon:
                    //如果多边形与裁剪多边形相交或在裁剪多边形中
                    if (relationOper.Contains(inGeometry))
                    {
                        //tempGeometry = inGeometry;
                        returnValue = true;
                    }
                    else //if( relationOper.Overlaps(inGeometry))     2008.1.29 TianK 注释掉
                    {
                        pPolyCurve = inGeometry as IPolycurve;
                        pPolyCurve.Generalize(0.03);
                        tempGeometry1 = topologyOper.Intersect(inGeometry, esriGeometryDimension.esriGeometry2Dimension);
                        tempGeometry = (inGeometry as ITopologicalOperator).Difference(tempGeometry1);

                        if (zIn.ZAware)
                        {
                            IZAware zOut = tempGeometry as IZAware;
                            zOut.ZAware = zIn.ZAware;
                            IZ iz = tempGeometry as IZ;
                            try
                            {
                                iz.CalculateNonSimpleZs();
                            }
                            catch
                            {
                                //该值应该传入，表示如果栽得的线如果不能插值出Z值赋一个默认值，或再次计算
                                //可以根据inGeometry计算
                                iz.SetConstantZ(0);
                            }

                        }
                    }
                    break;
            }
            if (tempGeometry != null && !tempGeometry.IsEmpty)
            {
                inGeometry = tempGeometry;
            }
            return returnValue;
        }

        /// <summary>
        /// 根据P1和P2插入一点Pm的M值
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <param name="Pm"></param>
        private void AddM_P1_P2(IPoint P1, IPoint P2, ref IPoint Pm)
        {
            try
            {
                double dm1 = GetDistance_P12(P1, Pm);
                double dm2 = GetDistance_P12(P2, Pm);

                Pm.M = P1.M + (P2.M - P1.M) * (dm1 / (dm1 + dm2));
            }
            catch
            {
            }
        }
        /// <summary>
        /// 计算P1到P2的距离
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public double GetDistance_P12(IPoint p1, IPoint p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double s = dx * dx + dy * dy;
            return Math.Sqrt(s);
        }

    }
}
