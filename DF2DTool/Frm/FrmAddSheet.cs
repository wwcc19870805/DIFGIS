using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using DevExpress.XtraEditors;

namespace DF2DTool.Frm
{
    public partial class FrmAddSheet : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_OK;
        private DevExpress.XtraEditors.TextEdit te_Y;
        private DevExpress.XtraEditors.TextEdit te_X;
        private DevExpress.XtraEditors.TextEdit te_maptitle;
        private DevExpress.XtraEditors.ComboBoxEdit cb_scale;
        private DevExpress.XtraEditors.ComboBoxEdit cb_district;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        Dictionary<string, List<IFeature>> _dicFC;
        HashSet<string> _scale;
        string _currentDis;
        IFeatureClass _fc;
        int _indexDisName;
        int _indexGEOOBJNUM;
        int _indexScale;
        int _indexMaptitle;
        int _indexMapNO;
        int _indexNWX;
        int _indexNWY;
        int _indexSWX;
        int _indexSWY;

        public FrmAddSheet()
        {
            InitializeComponent();
        }
       
        public void SetPara(ref Dictionary<string, List<IFeature>> dicFC, HashSet<string> scale, string currentDis, IFeatureClass fc, int indexDisName,
            int indexScale, int indexMaptitle, int indexMapNO,int indexNWX, int indexNWY, int indexSWX, int indexSWY,int indexGEOOBJNUM)
        {
            _dicFC = dicFC;
            _scale = scale;
            _currentDis = currentDis;
            _fc = fc;
            _indexDisName = indexDisName;
            _indexMaptitle = indexMaptitle;
            _indexScale = indexScale;
            _indexMapNO = indexMapNO;
            _indexNWX = indexNWX;
            _indexNWY = indexNWY;
            _indexSWX = indexSWX;
            _indexSWY = indexSWY;
            _indexGEOOBJNUM = indexGEOOBJNUM;
        }


        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.te_Y = new DevExpress.XtraEditors.TextEdit();
            this.te_X = new DevExpress.XtraEditors.TextEdit();
            this.te_maptitle = new DevExpress.XtraEditors.TextEdit();
            this.cb_scale = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cb_district = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_Y.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_X.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_maptitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_scale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_district.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Controls.Add(this.btn_OK);
            this.layoutControl1.Controls.Add(this.te_Y);
            this.layoutControl1.Controls.Add(this.te_X);
            this.layoutControl1.Controls.Add(this.te_maptitle);
            this.layoutControl1.Controls.Add(this.cb_scale);
            this.layoutControl1.Controls.Add(this.cb_district);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(284, 164);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(143, 135);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(136, 24);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 10;
            this.btn_Cancel.Text = "取消";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(5, 135);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(134, 22);
            this.btn_OK.StyleController = this.layoutControl1;
            this.btn_OK.TabIndex = 9;
            this.btn_OK.Text = "确定";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // te_Y
            // 
            this.te_Y.Location = new System.Drawing.Point(68, 109);
            this.te_Y.Name = "te_Y";
            this.te_Y.Size = new System.Drawing.Size(211, 22);
            this.te_Y.StyleController = this.layoutControl1;
            this.te_Y.TabIndex = 8;
            // 
            // te_X
            // 
            this.te_X.Location = new System.Drawing.Point(68, 83);
            this.te_X.Name = "te_X";
            this.te_X.Size = new System.Drawing.Size(211, 22);
            this.te_X.StyleController = this.layoutControl1;
            this.te_X.TabIndex = 7;
            // 
            // te_maptitle
            // 
            this.te_maptitle.Location = new System.Drawing.Point(68, 57);
            this.te_maptitle.Name = "te_maptitle";
            this.te_maptitle.Size = new System.Drawing.Size(211, 22);
            this.te_maptitle.StyleController = this.layoutControl1;
            this.te_maptitle.TabIndex = 6;
            // 
            // cb_scale
            // 
            this.cb_scale.Location = new System.Drawing.Point(68, 31);
            this.cb_scale.Name = "cb_scale";
            this.cb_scale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_scale.Size = new System.Drawing.Size(211, 22);
            this.cb_scale.StyleController = this.layoutControl1;
            this.cb_scale.TabIndex = 5;
            // 
            // cb_district
            // 
            this.cb_district.Location = new System.Drawing.Point(68, 5);
            this.cb_district.Name = "cb_district";
            this.cb_district.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_district.Size = new System.Drawing.Size(211, 22);
            this.cb_district.StyleController = this.layoutControl1;
            this.cb_district.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(284, 164);
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
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(284, 164);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cb_district;
            this.layoutControlItem1.CustomizationFormText = "测区名称：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(278, 26);
            this.layoutControlItem1.Text = "测区名称：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cb_scale;
            this.layoutControlItem2.CustomizationFormText = "比例尺：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(278, 26);
            this.layoutControlItem2.Text = "比例尺：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.te_maptitle;
            this.layoutControlItem3.CustomizationFormText = "图幅名称：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(278, 26);
            this.layoutControlItem3.Text = "图幅名称：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.te_X;
            this.layoutControlItem4.CustomizationFormText = "X前缀：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(278, 26);
            this.layoutControlItem4.Text = "X前缀：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.te_Y;
            this.layoutControlItem5.CustomizationFormText = "Y前缀：";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(278, 26);
            this.layoutControlItem5.Text = "Y前缀：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btn_OK;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(138, 28);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btn_Cancel;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(138, 130);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(140, 28);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(140, 28);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(140, 28);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // FrmAddSheet
            // 
            this.ClientSize = new System.Drawing.Size(284, 164);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmAddSheet";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加图幅";
            this.Load += new System.EventHandler(this.FrmAddSheet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_Y.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_X.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_maptitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_scale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_district.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        private void FrmAddSheet_Load(object sender, EventArgs e)
        {
            if (_dicFC == null || _dicFC.Count == 0) return;
            foreach (string dis in _dicFC.Keys)
            {
                this.cb_district.Properties.Items.Add(dis);
            }
            foreach (string sca in _scale)
            {
                this.cb_scale.Properties.Items.Add(sca);
            }
            this.cb_district.Text = _currentDis;
            this.cb_scale.SelectedIndex = 0;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                string disName = this.cb_district.Text;
                string scale = this.cb_scale.Text;
                string maptitle = this.te_maptitle.Text;
                string preX = this.te_X.Text;
                string preY = this.te_Y.Text;
                AddSheet(disName, maptitle, scale, preX, preY);
                this.DialogResult = DialogResult.OK;
                
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        private void AddSheet(string disName, string maptitle, string scale, string preX, string preY)
        {
            IFeatureBuffer pFeatureBuffer;
            IFeatureCursor pCursor;
            try
            {
                if (IsMapNameExist(maptitle))
                {
                    XtraMessageBox.Show("该图幅名称已存在，请重新输入", "提示");
                }
                else
                {
                    pCursor = _fc.Insert(true);

                    pFeatureBuffer = _fc.CreateFeatureBuffer();
                    long GEOOBJNUM = 10000;
                    string strMapNO = CalMapNOfromTitle(maptitle);

                    IEnvelope pEnvelope = GetMapEnvelope(strMapNO, preX, preY);
                    IPolyline pPolyline = ConvertEnvelopeToPolyline(pEnvelope);

                    pFeatureBuffer.Shape = pPolyline;
                    pFeatureBuffer.set_Value(_indexDisName, disName);
                    pFeatureBuffer.set_Value(_indexMaptitle, maptitle);
                    pFeatureBuffer.set_Value(_indexMapNO, strMapNO);
                    pFeatureBuffer.set_Value(_indexGEOOBJNUM, GEOOBJNUM);
                    pFeatureBuffer.set_Value(_indexSWX, pEnvelope.XMin);
                    pFeatureBuffer.set_Value(_indexSWY, pEnvelope.YMin);
                    pFeatureBuffer.set_Value(_indexNWX, pEnvelope.XMax);
                    pFeatureBuffer.set_Value(_indexNWY, pEnvelope.YMax);
                    pCursor.InsertFeature(pFeatureBuffer);
                    if (_dicFC.ContainsKey(disName))
                    {
                        _dicFC[disName].Add(pFeatureBuffer as IFeature);
                    }
                    XtraMessageBox.Show("添加图幅成功！", "提示");
                }
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show("添加失败，图幅坐标超过地图范围", "提示");
            }
           
        }

        private bool IsMapNameExist(string strMapName)
        {
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = "Maptitle = '" + strMapName + "'";
            IFeatureCursor cursor = _fc.Search(filter, false);
            IFeature feature = cursor.NextFeature();
            if (feature != null) return true;
            else return false;
        }
        private string CalMapNOfromTitle(string maptitle)
        {
            string[] strMapNo = new string[2];
            int length = maptitle.Length / 2;
            strMapNo[0] = maptitle.Substring(0, length);
            strMapNo[1] = maptitle.Substring(length + 1, maptitle.Length - length - 1);
            string strMapNumber = strMapNo[0].Substring(0, strMapNo[0].Length - 2) + "." + strMapNo[1].Substring(strMapNo[0].Length - 2, 2);
            return strMapNumber;
        }
        private IEnvelope GetMapEnvelope(string mapNO,string preX,string preY)
        {
            string strCoodX;
            string strCoordY;
            string[] s = null;
            double dblSWX;
            double dblSWY;
            IEnvelope pMapEnvelope = new EnvelopeClass();
            s = mapNO.Split('-');
            strCoodX = preX + s[0];
            strCoordY = preY + s[1];
            dblSWX = Convert.ToDouble(strCoodX) * 1000.0;
            dblSWY = Convert.ToDouble(strCoordY) * 1000.0;

            pMapEnvelope.XMin = dblSWX;
            pMapEnvelope.XMax = dblSWX + 250.0;
            pMapEnvelope.YMin = dblSWY;
            pMapEnvelope.YMax = dblSWY + 250;
            return pMapEnvelope;

        }
        private IPolyline ConvertEnvelopeToPolyline(IEnvelope pEnvelope)
        {
            IPointCollection pPointCol = new PolylineClass();
            object ep = System.Reflection.Missing.Value;
            IPoint pPointLowerLeft = pEnvelope.LowerLeft;
            IPoint pPointUpperLeft = pEnvelope.UpperLeft;
            IPoint pPointLowerRight = pEnvelope.LowerRight;
            IPoint pPointUpperRight = pEnvelope.UpperRight;

            pPointLowerLeft.Z = 0;
            pPointUpperLeft.Z = 0;
            pPointLowerRight.Z = 0;
            pPointUpperRight.Z = 0;

            pPointCol.AddPoint(pPointLowerLeft, ref ep, ref ep);
            pPointCol.AddPoint(pPointLowerRight, ref ep, ref ep);
            pPointCol.AddPoint(pPointUpperRight, ref ep, ref ep);
            pPointCol.AddPoint(pPointUpperLeft, ref ep, ref ep);
            pPointCol.AddPoint(pPointLowerLeft, ref ep, ref ep);
            IPolyline pPolyline = pPointCol as IPolyline;
            return pPolyline;
        }
    }
}
