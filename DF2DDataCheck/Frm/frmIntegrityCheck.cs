using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DF2DData.Class;
using DFDataConfig.Class;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Geodatabase;

namespace DF2DDataCheck.Frm
{
    public partial class frmIntegrityCheck : XtraForm
    {
        private ArrayList m_arrPntField;
        private ArrayList m_arrArcField;
        private DF2DFeatureClass dfcc;
        private FacilityClass fcc;
        string FieldName;
        private ArrayList m_arrPntFieldSel;
        private ArrayList m_arrArcFieldSel;
        private ArrayList m_arrPipeType;

        public ArrayList PntFieldSel
        {
            get
            {
                return m_arrPntFieldSel;
            }
        }

        public ArrayList ArcFieldSel
        {
            get
            {
                return m_arrArcFieldSel;
            }
        }

        public ArrayList PipeType
        {
            get
            {
                return m_arrPipeType;
            }
        }

        public frmIntegrityCheck()
        {
            InitializeComponent();
        }

        private void ckbPntCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbPntCheck.Checked)
            {
                this.lstPntField.Enabled = true;
            }
            else
            {
                this.lstPntField.Enabled = false;

            }

        }

        private void ckbArcCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbArcCheck.Checked)
            {
                this.lstArcField.Enabled = true;
            }
            else
            {
                this.lstArcField.Enabled = false;

            }

        }

        private void frmIntegrityCheck_Load(object sender, EventArgs e)
        {
            string strValue;
            string strDescription;
            string[] s;

            //从配置文件读取管线类别信息，填充界面控件
            foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
            {
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    this.lstPipeType.Items.Add(mc, mc.Alias);
                }
            }
            //从配置文件读取点表线表的字段信息，填充界面控件
            m_arrPntField = new ArrayList();
            m_arrArcField = new ArrayList();

            ReadFieldInfo();

            for (int i = 0; i < m_arrPntField.Count; i++)
            {
                s = m_arrPntField[i].ToString().Split(new char[] { ',' });
                strValue = s[0].Trim();
                strDescription = s[1].Trim();

                this.lstPntField.Items.Add(strValue, strDescription);
            }

            for (int j = 0; j < m_arrArcField.Count; j++)
            {
                s = m_arrArcField[j].ToString().Split(new char[] { ',' });
                strValue = s[0].Trim();
                strDescription = s[1].Trim();

                this.lstArcField.Items.Add(strValue, strDescription);
            }

        }

        private void ReadFieldInfo()
        {
            List<DF2DFeatureClass> list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
            if (list == null) return;
            dfcc = list[0];
            IFeatureClass fc = dfcc.GetFeatureClass();
            if (fc == null) return;
            FacilityClass fcc = dfcc.GetFacilityClass();
            List<DFDataConfig.Class.FieldInfo> m_list = fcc.FieldInfoCollection;
            foreach (DFDataConfig.Class.FieldInfo Field in m_list)
            {
                if (Field.NeedCheck)
                {
                    FieldName = Field.Name + "," + Field.Alias;
                    m_arrPntField.Add(FieldName);
                }
                else continue;

            }
            List<DF2DFeatureClass> nist = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
            if (nist == null) return;

            dfcc = nist[0];
            fcc = dfcc.GetFacilityClass();

            List<DFDataConfig.Class.FieldInfo> n_list = fcc.FieldInfoCollection;
            foreach (DFDataConfig.Class.FieldInfo Field in n_list)
            {
                if (Field.NeedCheck)
                {
                    FieldName = Field.Name + "," + Field.Alias;
                    m_arrArcField.Add(FieldName);
                }
                else continue;

            }

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string item;
            m_arrPntFieldSel = new ArrayList();
            m_arrArcFieldSel = new ArrayList();
            m_arrPipeType = new ArrayList();

            for (int i = 0; i < this.lstPipeType.CheckedItems.Count; i++)
            {
                DevExpress.XtraEditors.Controls.CheckedListBoxItem a = this.lstPipeType.Items[this.lstPipeType.CheckedIndices[i]];
                m_arrPipeType.Add(a.Value);
            }

            if (this.ckbPntCheck.Checked)
            {
                for (int i = 0; i < lstPntField.CheckedItems.Count; i++)
                {
                    item = this.lstPntField.CheckedItems[i].ToString();
                    m_arrPntFieldSel.Add(item);
                }
            }
            if (this.ckbArcCheck.Checked)
            {
                for (int j = 0; j < lstArcField.CheckedItems.Count; j++)
                {
                    item = this.lstArcField.CheckedItems[j].ToString();
                    m_arrArcFieldSel.Add(item);
                }
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
