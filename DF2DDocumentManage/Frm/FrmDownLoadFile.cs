using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using DFCommon.Class;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using DevExpress.XtraEditors;

namespace DF2DDocumentManage.Frm
{
    public partial class FrmDownLoadFile : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_DownLoad;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    
        public FrmDownLoadFile()
        {
            InitializeComponent();
        }
        private IWorkspace pWs;
        private Dictionary<string,object> _dictCad = new Dictionary<string,object>();
        private Dictionary<string,object> _dictFiles = new Dictionary<string,object>();
        private DataTable _dtCad;
        private DataTable _dtFile;
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_DownLoad = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Controls.Add(this.btn_DownLoad);
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(577, 262);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(445, 238);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(130, 22);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_DownLoad
            // 
            this.btn_DownLoad.Location = new System.Drawing.Point(315, 238);
            this.btn_DownLoad.Name = "btn_DownLoad";
            this.btn_DownLoad.Size = new System.Drawing.Size(126, 22);
            this.btn_DownLoad.StyleController = this.layoutControl1;
            this.btn_DownLoad.TabIndex = 5;
            this.btn_DownLoad.Text = "下载";
            this.btn_DownLoad.Click += new System.EventHandler(this.btn_DownLoad_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(5, 5);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(567, 226);
            this.xtraTabControl1.TabIndex = 4;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gridControl2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(561, 197);
            this.xtraTabPage1.Text = "技改项目";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(561, 197);
            this.gridControl2.TabIndex = 0;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gridControl1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(342, 197);
            this.xtraTabPage2.Text = "项目文档";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(342, 197);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(577, 262);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(577, 236);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.xtraTabControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(571, 230);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btn_DownLoad;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(313, 236);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(130, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_Cancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(443, 236);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(134, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 236);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(313, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmDownLoadFile
            // 
            this.ClientSize = new System.Drawing.Size(577, 262);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmDownLoadFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "文件下载";
            this.Load += new System.EventHandler(this.FrmDownLoadFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        private void FrmDownLoadFile_Load(object sender, EventArgs e)
        {
            try
            {
                pWs = GetWorkSpace();
                if (pWs == null) return;
                QueryFiles("WSGRI_MAP", _dictCad);
                QueryFiles("WSGRI_Docment", _dictFiles);
                this.gridControl1.DataSource = _dtFile;
                this.gridControl2.DataSource = _dtCad;
            }
            catch (System.Exception ex)
            {
            	
            }
           
            

        }
        private IWorkspace GetWorkSpace()
        {
            IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
            string path = Config.GetConfigValue("2DDocument");
            IWorkspace pWs = pWsF.OpenFromFile(path, 0);
            return pWs;

        }
        private void QueryFiles(string tableName,Dictionary<string,object> dict)
        {
            try
            {
                dict.Clear();
                if (pWs == null) return;
                IQueryFilter filter = new QueryFilter();
                ITable table = (pWs as IFeatureWorkspace).OpenTable(tableName);
                if (table == null) return;
                ICursor cursor = table.Search(filter, false);
                IRow row;
                int index;
                if (tableName == "WSGRI_MAP")
                {
                    _dtCad = new DataTable();
                    _dtCad.Columns.AddRange(new DataColumn[] { new DataColumn("项目名称"), new DataColumn("项目编号"), 
                        new DataColumn("设计单位"), new DataColumn("设计者"), new DataColumn("设计阶段"),
                        new DataColumn("工程时间"), new DataColumn("文件名称") });
                    while ((row = cursor.NextRow()) != null)
                    {
                        DataRow dr = _dtCad.NewRow();
                        dr["项目名称"] = row.get_Value(row.Fields.FindField("PROJECTNAME")).ToString();
                        dr["项目编号"] = row.get_Value(row.Fields.FindField("PROJECTNUM")).ToString();
                        dr["设计单位"] = row.get_Value(row.Fields.FindField("DESIGN")).ToString();
                        dr["设计者"] = row.get_Value(row.Fields.FindField("DESIGNER")).ToString();
                        dr["设计阶段"] = row.get_Value(row.Fields.FindField("DESIGNPHASE")).ToString();
                        dr["工程时间"] = row.get_Value(row.Fields.FindField("PROJECTDATE")).ToString();
                        dr["文件名称"] = row.get_Value(row.Fields.FindField("CADNAME")).ToString();
                        string cadName = dr["文件名称"].ToString();
                        object objfile = row.get_Value(row.Fields.FindField("OBJFILE"));
                        _dtCad.Rows.Add(dr);
                        if (!dict.ContainsKey(cadName)) dict[cadName] = objfile; 

                    }
                }
                else
                {
                    _dtFile = new DataTable();
                    _dtFile.Columns.AddRange(new DataColumn[]{new DataColumn("文件名称"),new DataColumn("文件类型"), 
                        new DataColumn("文件版本"), new DataColumn("发布日期"), new DataColumn("发布单位"),
                        new DataColumn("文件类别"), new DataColumn("文件描述"),new DataColumn("文件备注"),
                    new DataColumn("存档人"),new DataColumn("存档日期")});
                    while ((row = cursor.NextRow()) != null)
                    {
                        DataRow dr = _dtFile.NewRow();
                        dr["文件名称"] = row.get_Value(row.Fields.FindField("FILENAME")).ToString();
                        dr["文件类型"] = row.get_Value(row.Fields.FindField("FileClass")).ToString();
                        dr["文件版本"] = row.get_Value(row.Fields.FindField("VERSION")).ToString();
                        dr["发布日期"] = row.get_Value(row.Fields.FindField("PROMULGATEDATE")).ToString();
                        dr["发布单位"] = row.get_Value(row.Fields.FindField("PROMULGATEUNIT")).ToString();
                        dr["文件类别"] = row.get_Value(row.Fields.FindField("FILETYPE")).ToString();
                        dr["文件描述"] = row.get_Value(row.Fields.FindField("Description")).ToString();
                        dr["文件备注"] = row.get_Value(row.Fields.FindField("MARK")).ToString();
                        dr["存档人"] = row.get_Value(row.Fields.FindField("SAVER")).ToString();
                        dr["存档日期"] = row.get_Value(row.Fields.FindField("SAVEDATE"));
                        string fileName = dr["文件名称"].ToString();
                        object objfile = row.get_Value(row.Fields.FindField("OBJFILE"));
                        _dtFile.Rows.Add(dr);
                        if (!dict.ContainsKey(fileName)) dict[fileName] = objfile;

                    }

                }
           
             
            }
            catch (System.Exception ex)
            {
                return;
            }
           
        }
        private bool IsExist(string strField, string strFileName, ITable pTable)
        {
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = strField + "='" + strFileName + "'";
            ICursor cursor = pTable.Search(filter, true);
            IRow row = cursor.NextRow();
            if (row == null) return false;
            else
                return true;
        }

        private void btn_DownLoad_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd;
            if (this.xtraTabControl1.SelectedTabPageIndex == 0)
            {
                DataRow dr = gridView2.GetFocusedDataRow();
                string fileName = dr["文件名称"].ToString();
                string fileType = fileName.Substring(fileName.LastIndexOf('.'));
                if (_dictCad.ContainsKey(fileName))
                {
                    object fileCad = _dictCad[fileName];
                    sfd = new SaveFileDialog();
                    sfd.RestoreDirectory = true;
                    sfd.Filter = "保存文件|*" + fileType;
                    
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        IMemoryBlobStream mbs = fileCad as IMemoryBlobStream;
                        if (mbs == null) return;
                        mbs.SaveToFile(sfd.FileName);
                    }
                }
            }
            else
            {
                DataRow dr = gridView1.GetFocusedDataRow();
                string fileName = dr["文件名称"].ToString();
                string fileType = dr["文件类别"].ToString();
                if (_dictFiles.ContainsKey(fileName))
                {
                    object file = _dictFiles[fileName];
                    sfd = new SaveFileDialog();
                    sfd.RestoreDirectory = true;
                    sfd.Filter = "保存文件|*" + fileType;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        IMemoryBlobStream mbs = file as IMemoryBlobStream;
                        if (mbs == null) return;
                        mbs.SaveToFile(sfd.FileName);
                    }
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
    }
}
