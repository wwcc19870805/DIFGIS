using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DFDataConfig.Logic;
using DevExpress.XtraEditors;

namespace DF2DPipe.Query.Frm
{
    public partial class FrmMajorClass : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl chlbx_MajorClass;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;

        public  FrmMajorClass()
        {
            InitializeComponent();
        }

        private  void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chlbx_MajorClass = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chlbx_MajorClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chlbx_MajorClass);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(167, 262);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chlbx_MajorClass
            // 
            this.chlbx_MajorClass.Location = new System.Drawing.Point(5, 5);
            this.chlbx_MajorClass.Name = "chlbx_MajorClass";
            this.chlbx_MajorClass.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.chlbx_MajorClass.Size = new System.Drawing.Size(157, 252);
            this.chlbx_MajorClass.StyleController = this.layoutControl1;
            this.chlbx_MajorClass.TabIndex = 4;
            this.chlbx_MajorClass.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.chlbx_MajorClass_ItemCheck);
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(167, 262);
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
            this.layoutControlGroup2.Size = new System.Drawing.Size(167, 262);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chlbx_MajorClass;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(161, 256);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmMajorClass
            // 
            this.ClientSize = new System.Drawing.Size(167, 262);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmMajorClass";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管线类别";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMajorClass_FormClosed);
            this.Load += new System.EventHandler(this.FrmMajorClass_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chlbx_MajorClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        public static FrmMajorClass Instance
        {
            get 
            {
                if (FrmMajorClass.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (FrmMajorClass.instance == null)
                        {
                            FrmMajorClass.instance = new FrmMajorClass();
                        }
                    }
                }
                return FrmMajorClass.instance;
            }
        }
        private static FrmMajorClass instance = null;
        private static readonly object syncRoot = new object();

        private List<MajorClass> mcList = new List<MajorClass>();
        public List<MajorClass> MajorClasses
        {
            get { return this.mcList; }
        }
        private void FrmMajorClass_Load(object sender, EventArgs e)
        {
            try
            {
                 foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
                 {
                     foreach (MajorClass mc in lg.MajorClasses)
                     {
                         chlbx_MajorClass.Items.Add(mc, mc.Alias, CheckState.Unchecked, true);
                     }
                 }       
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }

        private void chlbx_MajorClass_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            List<object> objList = chlbx_MajorClass.Items.GetCheckedValues();
            if (objList.Count > 0)
            {
                this.mcList.Clear();
                foreach (object obj in objList)
                {
                    if (obj is MajorClass)
                    {
                        this.mcList.Add(obj as MajorClass);
                    }
                }
            }
        }

        private void FrmMajorClass_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }
    }
}
