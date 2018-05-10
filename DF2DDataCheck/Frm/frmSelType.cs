using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DFDataConfig.Logic;

namespace DF2DDataCheck.Frm
{
    public partial class frmSelType : XtraForm
    {
        private ArrayList m_arrPipeType;

        public frmSelType()
        {
            InitializeComponent();
        }

        public ArrayList PipeType
        {
            get
            {
                return m_arrPipeType;
            }
        }

        private void frmSelType_Load(object sender, EventArgs e)
        {
            //从配置文件读取管线类别信息，填充界面控件
            foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
            {
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    this.lstPipeType.Items.Add(mc, mc.Alias);
                }
            }

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            m_arrPipeType = new ArrayList();

            for (int i = 0; i < this.lstPipeType.CheckedItems.Count; i++)
            {
                DevExpress.XtraEditors.Controls.CheckedListBoxItem a = this.lstPipeType.Items[this.lstPipeType.CheckedIndices[i]];
                m_arrPipeType.Add(a.Value);
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(); 

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
