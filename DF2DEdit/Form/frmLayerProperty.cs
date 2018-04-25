using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using DevExpress.XtraEditors;

namespace DF2DEdit.Form
{
    public partial class frmLayerProperty : XtraForm
    {
        private IFeatureLayer m_FeaLay;
        private IMapControl2 m_MapControl;

        /// <summary>
        /// 传入的IFeatureLayer
        /// </summary>
        public IFeatureLayer FeatureLayer
        {
            get
            {
                return m_FeaLay;
            }
            set
            {
                m_FeaLay = value;
            }
        }

        /// <summary>
        /// 传入的IMapControl2
        /// </summary>
        public IMapControl2 MapControl
        {
            get
            {
                return m_MapControl;
            }
            set
            {
                m_MapControl = value;
            }
        }

        public frmLayerProperty()
        {
            InitializeComponent();
        }

        public frmLayerProperty(IFeatureLayer pFeaLay)
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			this.FeatureLayer = pFeaLay;
		}

        private void frmLayerProperty_Load(object sender, EventArgs e)
        {
            if (m_FeaLay != null)
            {
                this.layerProperty.FeatureLayer = m_FeaLay;
                this.layerProperty.MapControl = m_MapControl;
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (layerProperty.SaveProperties())
            {
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("图层属性填写错误。", "图层属性", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }
    }
}
