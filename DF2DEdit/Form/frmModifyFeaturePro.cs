/*-----------------------------------------------------------------------------------------
			// Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：frmModifyFeaturePro.cs
			// 文件功能描述：修改要素属性
			//
			// 
			// 创建标识：LuoXuan 20171022
-----------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using DevExpress.XtraEditors;
using DF2DEdit.UserControl;

namespace DF2DEdit.Form
{
    public partial class frmModifyFeaturePro : XtraForm
    {
        public frmModifyFeaturePro()
        {
            InitializeComponent();
        }

        public IMapControl2 MapControl
        {
            get 
            {
                return modifyFeaturePro1.MapControl; 
            }
            set 
            {
                modifyFeaturePro1.MapControl = value;
            }
        }

        public void RefreshSelection()
        {
            modifyFeaturePro1.RefreshSelection();
        }

        private void frmModifyFeaturePro_FormClosed(object sender, FormClosedEventArgs e)
        {
            Class.Common.System_ModifyFeature_Form = null;
        }

    }
}
