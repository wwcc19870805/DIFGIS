using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Collections;
using DF2DDocumentManage.Class;
using DFWinForms.Class;

namespace DF2DDocumentManage.Frm
{
    public partial class FrmImportFile : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_OK;
        private DevExpress.XtraEditors.MemoEdit me_Mark;
        private DevExpress.XtraEditors.ComboBoxEdit cb_DesignPhase;
        private DevExpress.XtraEditors.TextEdit te_Designer;
        private DevExpress.XtraEditors.TextEdit te_DesignUnit;
        private DevExpress.XtraEditors.TextEdit te_ProNum;
        private DevExpress.XtraEditors.TextEdit te_ProName;
        private DevExpress.XtraEditors.DateEdit de_ProDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private SimpleButton btn_Browse;
        private ListBoxControl lbx_Files;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private SimpleButton btn_Delete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    
        public FrmImportFile()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_Browse = new DevExpress.XtraEditors.SimpleButton();
            this.lbx_Files = new DevExpress.XtraEditors.ListBoxControl();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.me_Mark = new DevExpress.XtraEditors.MemoEdit();
            this.cb_DesignPhase = new DevExpress.XtraEditors.ComboBoxEdit();
            this.te_Designer = new DevExpress.XtraEditors.TextEdit();
            this.te_DesignUnit = new DevExpress.XtraEditors.TextEdit();
            this.te_ProNum = new DevExpress.XtraEditors.TextEdit();
            this.te_ProName = new DevExpress.XtraEditors.TextEdit();
            this.de_ProDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btn_Delete = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbx_Files)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.me_Mark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_DesignPhase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Designer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_DesignUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ProNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ProName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_ProDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_ProDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_Delete);
            this.layoutControl1.Controls.Add(this.btn_Browse);
            this.layoutControl1.Controls.Add(this.lbx_Files);
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Controls.Add(this.btn_OK);
            this.layoutControl1.Controls.Add(this.me_Mark);
            this.layoutControl1.Controls.Add(this.cb_DesignPhase);
            this.layoutControl1.Controls.Add(this.te_Designer);
            this.layoutControl1.Controls.Add(this.te_DesignUnit);
            this.layoutControl1.Controls.Add(this.te_ProNum);
            this.layoutControl1.Controls.Add(this.te_ProName);
            this.layoutControl1.Controls.Add(this.de_ProDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(385, 287);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(5, 202);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(107, 22);
            this.btn_Browse.StyleController = this.layoutControl1;
            this.btn_Browse.TabIndex = 14;
            this.btn_Browse.Text = "浏览";
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // lbx_Files
            // 
            this.lbx_Files.Location = new System.Drawing.Point(5, 25);
            this.lbx_Files.Name = "lbx_Files";
            this.lbx_Files.Size = new System.Drawing.Size(107, 173);
            this.lbx_Files.StyleController = this.layoutControl1;
            this.lbx_Files.TabIndex = 13;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(195, 260);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(185, 22);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 12;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(5, 260);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(186, 22);
            this.btn_OK.StyleController = this.layoutControl1;
            this.btn_OK.TabIndex = 11;
            this.btn_OK.Text = "开始上传";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // me_Mark
            // 
            this.me_Mark.Location = new System.Drawing.Point(185, 181);
            this.me_Mark.Name = "me_Mark";
            this.me_Mark.Size = new System.Drawing.Size(195, 69);
            this.me_Mark.StyleController = this.layoutControl1;
            this.me_Mark.TabIndex = 10;
            this.me_Mark.UseOptimizedRendering = true;
            // 
            // cb_DesignPhase
            // 
            this.cb_DesignPhase.Location = new System.Drawing.Point(185, 155);
            this.cb_DesignPhase.Name = "cb_DesignPhase";
            this.cb_DesignPhase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_DesignPhase.Size = new System.Drawing.Size(195, 22);
            this.cb_DesignPhase.StyleController = this.layoutControl1;
            this.cb_DesignPhase.TabIndex = 9;
            // 
            // te_Designer
            // 
            this.te_Designer.Location = new System.Drawing.Point(185, 129);
            this.te_Designer.Name = "te_Designer";
            this.te_Designer.Size = new System.Drawing.Size(195, 22);
            this.te_Designer.StyleController = this.layoutControl1;
            this.te_Designer.TabIndex = 8;
            // 
            // te_DesignUnit
            // 
            this.te_DesignUnit.Location = new System.Drawing.Point(185, 103);
            this.te_DesignUnit.Name = "te_DesignUnit";
            this.te_DesignUnit.Size = new System.Drawing.Size(195, 22);
            this.te_DesignUnit.StyleController = this.layoutControl1;
            this.te_DesignUnit.TabIndex = 7;
            // 
            // te_ProNum
            // 
            this.te_ProNum.Location = new System.Drawing.Point(185, 51);
            this.te_ProNum.Name = "te_ProNum";
            this.te_ProNum.Size = new System.Drawing.Size(195, 22);
            this.te_ProNum.StyleController = this.layoutControl1;
            this.te_ProNum.TabIndex = 5;
            // 
            // te_ProName
            // 
            this.te_ProName.Location = new System.Drawing.Point(185, 25);
            this.te_ProName.Name = "te_ProName";
            this.te_ProName.Size = new System.Drawing.Size(195, 22);
            this.te_ProName.StyleController = this.layoutControl1;
            this.te_ProName.TabIndex = 4;
            // 
            // de_ProDate
            // 
            this.de_ProDate.EditValue = null;
            this.de_ProDate.Location = new System.Drawing.Point(185, 77);
            this.de_ProDate.Name = "de_ProDate";
            this.de_ProDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_ProDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_ProDate.Properties.Mask.EditMask = "";
            this.de_ProDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.de_ProDate.Size = new System.Drawing.Size(195, 22);
            this.de_ProDate.StyleController = this.layoutControl1;
            this.de_ProDate.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(385, 287);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "选择文件";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(117, 255);
            this.layoutControlGroup2.Text = "选择文件";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.lbx_Files;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(111, 177);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btn_Browse;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 177);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(111, 26);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "技改项目基本信息";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.layoutControlGroup3.Location = new System.Drawing.Point(117, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(268, 255);
            this.layoutControlGroup3.Text = "技改项目基本信息";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.te_ProName;
            this.layoutControlItem1.CustomizationFormText = "工程名称：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(262, 26);
            this.layoutControlItem1.Text = "工程名称：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.te_ProNum;
            this.layoutControlItem2.CustomizationFormText = "工程编号：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(262, 26);
            this.layoutControlItem2.Text = "工程编号：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.de_ProDate;
            this.layoutControlItem3.CustomizationFormText = "工程时间：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(262, 26);
            this.layoutControlItem3.Text = "工程时间：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.te_DesignUnit;
            this.layoutControlItem4.CustomizationFormText = "设计单位：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(262, 26);
            this.layoutControlItem4.Text = "设计单位：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.te_Designer;
            this.layoutControlItem5.CustomizationFormText = "设计者：";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(262, 26);
            this.layoutControlItem5.Text = "设计者：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cb_DesignPhase;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(262, 26);
            this.layoutControlItem6.Text = "设计阶段：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.me_Mark;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 156);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(262, 73);
            this.layoutControlItem7.Text = "备注：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 255);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(385, 32);
            this.layoutControlGroup4.Text = "layoutControlGroup4";
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btn_OK;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(190, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btn_Cancel;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(190, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(189, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(5, 228);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(107, 22);
            this.btn_Delete.StyleController = this.layoutControl1;
            this.btn_Delete.TabIndex = 15;
            this.btn_Delete.Text = "删除";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.btn_Delete;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 203);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(111, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // FrmImportFile
            // 
            this.ClientSize = new System.Drawing.Size(385, 287);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmImportFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "技改文件上传";
            this.Load += new System.EventHandler(this.FrmImportFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbx_Files)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.me_Mark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_DesignPhase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Designer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_DesignUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ProNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ProName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_ProDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_ProDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            this.ResumeLayout(false);

        }

        ArrayList m_ArrayListFiles = new ArrayList();
        string[] designPhase = new string[] { "施工图阶段", "竣工图阶段" };
        private void btn_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.RestoreDirectory = true;
            ofd.Filter = "CAD文件|*.dxf;*.dwg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileNames.Length == 0) return;
                m_ArrayListFiles.Clear();
                for (int i = 0; i < ofd.FileNames.Length; i++)
                {
                    this.lbx_Files.Items.Add(ofd.FileNames[i]);
                    FileInfo fi = new FileInfo(ofd.FileNames[i]);
                    m_ArrayListFiles.Add(fi);
                }
            }
            FrmImportFile_Load(null, null);
        }

        private void FrmImportFile_Load(object sender, EventArgs e)
        {
            //if (this.m_ArrayListFiles.Count == 0) return;
            this.lbx_Files.Items.Clear();
            this.lbx_Files.Items.AddRange(m_ArrayListFiles.ToArray());
            this.cb_DesignPhase.Properties.Items.Clear();
            this.cb_DesignPhase.Properties.Items.AddRange(designPhase);
            this.cb_DesignPhase.SelectedIndex = 0;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            ImportFiles import = new ImportFiles();
            import.SetPara(m_ArrayListFiles,te_ProName.Text,te_ProNum.Text,te_DesignUnit.Text,te_Designer.Text,cb_DesignPhase.Text,de_ProDate.DateTime);
            import.Import();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string item = this.lbx_Files.SelectedItem.ToString();
                object temp = null;
                foreach (object obj in m_ArrayListFiles)
                {
                    if (obj is FileInfo)
                    {
                        FileInfo fi = obj as FileInfo;
                        if (fi.FullName == item)
                        {
                            temp = obj;
                            break;
                        }
                    }
                }
                if (temp == null) return;
                m_ArrayListFiles.Remove(temp);
                FrmImportFile_Load(null, null);
            }
            catch (System.Exception ex)
            {
            	
            }
          
        }
    }
}
