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
using DF2DTool.Class;
using System.Collections;
using DFWinForms.Class;
using System.IO;

namespace DF2DTool.Frm
{
    public partial class FrmUpDatalineMerge : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.ListBoxControl lbx_UnMerge;
        private DevExpress.XtraEditors.ListBoxControl lbx_Merge;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_OK;
        private DevExpress.XtraEditors.CheckEdit ce_Merge;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    
        public FrmUpDatalineMerge()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lbx_UnMerge = new DevExpress.XtraEditors.ListBoxControl();
            this.lbx_Merge = new DevExpress.XtraEditors.ListBoxControl();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.ce_Merge = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbx_UnMerge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbx_Merge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_Merge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lbx_UnMerge);
            this.layoutControl1.Controls.Add(this.lbx_Merge);
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Controls.Add(this.btn_OK);
            this.layoutControl1.Controls.Add(this.ce_Merge);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(313, 287);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lbx_UnMerge
            // 
            this.lbx_UnMerge.Location = new System.Drawing.Point(160, 25);
            this.lbx_UnMerge.Name = "lbx_UnMerge";
            this.lbx_UnMerge.Size = new System.Drawing.Size(148, 196);
            this.lbx_UnMerge.StyleController = this.layoutControl1;
            this.lbx_UnMerge.TabIndex = 8;
            // 
            // lbx_Merge
            // 
            this.lbx_Merge.Location = new System.Drawing.Point(5, 25);
            this.lbx_Merge.Name = "lbx_Merge";
            this.lbx_Merge.Size = new System.Drawing.Size(145, 196);
            this.lbx_Merge.StyleController = this.layoutControl1;
            this.lbx_Merge.TabIndex = 7;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(163, 260);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(145, 22);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(5, 260);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(154, 22);
            this.btn_OK.StyleController = this.layoutControl1;
            this.btn_OK.TabIndex = 5;
            this.btn_OK.Text = "确定";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // ce_Merge
            // 
            this.ce_Merge.Enabled = false;
            this.ce_Merge.Location = new System.Drawing.Point(5, 231);
            this.ce_Merge.Name = "ce_Merge";
            this.ce_Merge.Properties.Caption = "合并要素";
            this.ce_Merge.Size = new System.Drawing.Size(303, 19);
            this.ce_Merge.StyleController = this.layoutControl1;
            this.ce_Merge.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlGroup5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(313, 287);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(155, 226);
            this.layoutControlGroup2.Text = "合并接边范围";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lbx_Merge;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(149, 200);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup3.Location = new System.Drawing.Point(155, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(158, 226);
            this.layoutControlGroup3.Text = "未导入更新范围";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lbx_UnMerge;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(152, 200);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 226);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(313, 29);
            this.layoutControlGroup4.Text = "layoutControlGroup4";
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ce_Merge;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(307, 23);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup5";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 255);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(313, 32);
            this.layoutControlGroup5.Text = "layoutControlGroup5";
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btn_OK;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_Cancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(158, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(149, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // FrmUpDatalineMerge
            // 
            this.ClientSize = new System.Drawing.Size(313, 287);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmUpDatalineMerge";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据接边";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbx_UnMerge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbx_Merge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_Merge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        public FrmUpDatalineMerge(IWorkspace pWorkspace, IFeatureClass fc)
        {
            InitializeComponent();
            this.fcUpdata = fc;
            this.pWorkspace = pWorkspace;
            this.InitControls();
        }

        private IFeatureClass fcUpdata;
        private IWorkspace pWorkspace;
        private IGeometryCollection pGeometryC;
        private double douTolerence = 0.1;
        private static string GEOOBJNUM = "GEOOBJNUM";//地物编码字段名
        private static string GEOGUID = "GeoGUID";//唯一标识符字段名
        private static string CARTOBORDER = "CartoBORDER";
        private static string REGIONNAME = "REGIONNAME";
        private static string MARK = "Mark";

        private const string FILE_NAME = "GeoCollectionNum.txt";


        private void InitControls()
        {
            Item item;
            IFeatureCursor fCursor;
            IFeature pFeature;
            if(fcUpdata == null) return;
            fCursor = fcUpdata.Search(null, true);
            pFeature = fCursor.NextFeature();
            if (pFeature == null)
            {
                XtraMessageBox.Show("当前地图中没有修测范围，已合并完成或没有进行更新数据操作！");
                return;
            }
            pGeometryC = new PolygonClass();
            int indexMark = pFeature.Fields.FindField(MARK);
            int index1 = pFeature.Fields.FindField(REGIONNAME);
            while (pFeature != null)
            {
                if (pFeature.Shape != null)
                {
                    if (pFeature.get_Value(indexMark).ToString() == "已导出，未导入更新")
                    {
                        item = new Item(pFeature.get_Value(index1).ToString(), pFeature);
                        lbx_UnMerge.Items.Add(item);
                    }
                    else
                    {
                        item = new Item(pFeature.get_Value(index1).ToString(), pFeature);
                        lbx_Merge.Items.Add(item);
                        pGeometryC.AddGeometryCollection(pFeature.Shape as IGeometryCollection);
                    }
                }
                pFeature = fCursor.NextFeature();
            }
            ITopologicalOperator pTopOperator = pGeometryC as ITopologicalOperator;
            pTopOperator.Simplify();


        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList m_ArListGeoCGEOOBJNUM = new ArrayList();
                ReadGeoCollectionGEOOBJNUM(ref m_ArListGeoCGEOOBJNUM);
                ArrayList pFeaClassNameList = CommonFunction.GetFeactureClassName_From_AccessWorkSpace(pWorkspace, "500");
                IFeatureClass pFeaClass;
                string name;
                for (int i = 0; i < pFeaClassNameList.Count; i++)
                {
                    name = pFeaClassNameList[i].ToString();
                    pFeaClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(name);
                    if (name.Length > 5
                        && name.IndexOf(CARTOBORDER) <= -1
                        && pFeaClass.FindField(GEOOBJNUM) != -1
                        && pFeaClass.FindField(GEOGUID) != -1
                        && pFeaClass.FeatureType != esriFeatureType.esriFTAnnotation
                        && (pFeaClass.ShapeType == esriGeometryType.esriGeometryPolyline ||
                        pFeaClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                    {
                        WaitForm.SetCaption("正要对要素类：" + pFeaClass.AliasName + "进行接边...");
                        clsUpDatalineMerge.FeatureClassMerge(pFeaClass, pGeometryC, m_ArListGeoCGEOOBJNUM, douTolerence);
                    }
                }
                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = MARK + "='已导入未合并'";
                IFeatureCursor cursor = fcUpdata.Search(filter, true);
                IFeature feature = cursor.NextFeature();
                if (feature != null)
                {
                    ITable pTable = fcUpdata as ITable;
                    pTable.DeleteSearchedRows(filter);
                }
               

                if (this.ce_Merge.Checked)
                {
                    //clsAutoMerge p_clsAutoMerge = new clsAutoMerge(pWorkspace as IFeatureWorkspace);
                    //for (int i = 0; i < p_clsAutoMerge.MergeCount;i++)
                    //{
                    //    WaitForm.SetCaption("正在合并要素【" + p_clsAutoMerge.MergeArray[i].strDescrip + "】");
                    //    p_clsAutoMerge.autoMerge(p_clsAutoMerge.MergeArray[i]);
                    //}
                }
                this.Close();
                this.Dispose();
                XtraMessageBox.Show("图形合并接边已完成！");
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
                return;
            
            }
            
        }

        /// <summary>
        /// 从support目录下读取GeoCollectionNum.txt文件中的应该为多部件的地物编码
        /// </summary>
        /// <param name="inArGEOOGJNUM"></param>
        private void ReadGeoCollectionGEOOBJNUM(ref ArrayList inArGEOOGJNUM)
        {
            //按行读取文本文件
            string strFilePath = System.Windows.Forms.Application.StartupPath + @"\..\Support\" + FILE_NAME;
            if (!File.Exists(strFilePath))
            {
                System.Windows.Forms.MessageBox.Show("找不到文件" + FILE_NAME + ",请在Support目中添加该文件，存放应该为多部件的要素的地物编码", "找不到文件", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            StreamReader sr = File.OpenText(strFilePath);
            char[] charSep = { '\t' };
            string strInfo;
            string[] strCode = new string[2];

            while ((strInfo = sr.ReadLine()) != null)
            {
                strCode = strInfo.Split(charSep);
                inArGEOOGJNUM.Add(strCode[1].Trim());
            }
            sr.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
     
    }


  
}
