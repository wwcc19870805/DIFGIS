using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace DF2DTool.Frm
{
    public partial class FrmMapExportByCoordinate : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.TextEdit te_width;
        private DevExpress.XtraEditors.TextEdit te_length;
        private DevExpress.XtraEditors.TextEdit te_Y;
        private DevExpress.XtraEditors.TextEdit te_X;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        IPoint _point;
    
        public FrmMapExportByCoordinate()
        {
            InitializeComponent();
        }
        public FrmMapExportByCoordinate(IPoint point)
        {
            InitializeComponent();
            _point = point;
        }


        public string X
        {
            get { return te_X.Text; }
            set { te_X.Text = value; }
        }
        public string Y
        {
            get { return te_Y.Text; }
            set { te_Y.Text = value; }
        }
        public double Length
        {
            get
            {
                double length;
                if (double.TryParse(te_length.Text, out length))
                {
                    return length;
                }
                else
                    return 0.0;
            }
        }
        public double Width
        {
            get
            {
                double width;
                if (double.TryParse(te_width.Text, out width))
                {
                    return width;
                }
                else
                    return 0.0;
            }
        }
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.te_width = new DevExpress.XtraEditors.TextEdit();
            this.te_length = new DevExpress.XtraEditors.TextEdit();
            this.te_Y = new DevExpress.XtraEditors.TextEdit();
            this.te_X = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_width.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_length.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Y.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_X.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.te_width);
            this.layoutControl1.Controls.Add(this.te_length);
            this.layoutControl1.Controls.Add(this.te_Y);
            this.layoutControl1.Controls.Add(this.te_X);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(219, 184);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(107, 155);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(5, 155);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // te_width
            // 
            this.te_width.Location = new System.Drawing.Point(88, 129);
            this.te_width.Name = "te_width";
            this.te_width.Size = new System.Drawing.Size(126, 22);
            this.te_width.StyleController = this.layoutControl1;
            this.te_width.TabIndex = 7;
            // 
            // te_length
            // 
            this.te_length.Location = new System.Drawing.Point(88, 103);
            this.te_length.Name = "te_length";
            this.te_length.Size = new System.Drawing.Size(126, 22);
            this.te_length.StyleController = this.layoutControl1;
            this.te_length.TabIndex = 6;
            // 
            // te_Y
            // 
            this.te_Y.Enabled = false;
            this.te_Y.Location = new System.Drawing.Point(88, 51);
            this.te_Y.Name = "te_Y";
            this.te_Y.Size = new System.Drawing.Size(126, 22);
            this.te_Y.StyleController = this.layoutControl1;
            this.te_Y.TabIndex = 5;
            // 
            // te_X
            // 
            this.te_X.Enabled = false;
            this.te_X.Location = new System.Drawing.Point(88, 25);
            this.te_X.Name = "te_X";
            this.te_X.Size = new System.Drawing.Size(126, 22);
            this.te_X.StyleController = this.layoutControl1;
            this.te_X.TabIndex = 4;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(219, 184);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "原点坐标 (单位：米)";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(219, 78);
            this.layoutControlGroup2.Text = "原点坐标 (单位：米)";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.te_X;
            this.layoutControlItem1.CustomizationFormText = "      X:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(213, 26);
            this.layoutControlItem1.Text = "      坐标X:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(80, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.te_Y;
            this.layoutControlItem2.CustomizationFormText = "      Y:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(213, 26);
            this.layoutControlItem2.Text = "      坐标Y:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(80, 14);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "图幅范围 (单位：米)";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 78);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(219, 106);
            this.layoutControlGroup3.Text = "图幅范围 (单位：米)";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.te_length;
            this.layoutControlItem3.CustomizationFormText = "     图幅长度：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(213, 26);
            this.layoutControlItem3.Text = "     图幅长度：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(80, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.te_width;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(213, 26);
            this.layoutControlItem4.Text = "     图幅宽度：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(80, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnOK;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(102, 28);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(102, 52);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(111, 28);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // FrmMapExportByCoordinate
            // 
            this.ClientSize = new System.Drawing.Size(219, 184);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmMapExportByCoordinate";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "图幅参数设置";
            this.Load += new System.EventHandler(this.FrmMapExportByCoordinate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_width.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_length.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Y.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_X.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        private void FrmMapExportByCoordinate_Load(object sender, EventArgs e)
        {
            te_X.Text = _point.Y.ToString();
            te_Y.Text = _point.X.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            double length ,width;
            if (!double.TryParse(te_length.Text, out length)||!double.TryParse(te_width.Text,out width))
            {
                MessageBox.Show("图幅范围参数非法...请重新输入");
                te_length.Text = null;
                te_width.Text = null;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            te_length.Text = null;
            te_width.Text = null;
            this.Close();
        }
    }
}
