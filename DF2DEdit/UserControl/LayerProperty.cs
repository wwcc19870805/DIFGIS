using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using DF2DData.Class;
using DFDataConfig.Class;

namespace DF2DEdit.UserControl
{
    public partial class LayerProperty : XtraUserControl
    {
        private IFeatureLayer m_FeaLay;
        private IMapControl2 m_MapCtrl;
        private ITableFields m_pTableFields;//图层属性表

        public LayerProperty()
        {
            InitializeComponent();
        }

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
                initControls();
            }
        }

        /// <summary>
        /// 传入的IMapControl2
        /// </summary>
        public IMapControl2 MapControl
        {
            get
            {
                return m_MapCtrl;
            }
            set
            {
                m_MapCtrl = value;
            }
        }

        private void btnSetDataSourcePath_Click(object sender, EventArgs e)
        {
            string sDir = Application.StartupPath;

            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = sDir + @"\..\Result\";
            openFileDialog.Filter = "mdb|*.mdb";
            openFileDialog.Title = "请选择数据库";

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            string strFileName = openFileDialog.FileName;

            IDataLayer pDataLayer = (IDataLayer)m_FeaLay;
            IName pName = pDataLayer.DataSourceName;
            IDatasetName pDatasetName = pName as IDatasetName;
            string strFeatureClassName = pDatasetName.Name;

            IWorkspaceFactory pFact = new AccessWorkspaceFactoryClass();
            IWorkspace pWorkspace = pFact.OpenFromFile(strFileName, 0);
            IFeatureWorkspace pFeatws = pWorkspace as IFeatureWorkspace;
            IFeatureClass pFeatcls = pFeatws.OpenFeatureClass(strFeatureClassName);
            m_FeaLay.FeatureClass = pFeatcls;

        }

        #region 初始化控件
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void initControls()
        {
            IFeatureLayer pFeaLayer = (IFeatureLayer)m_FeaLay;
            if (pFeaLayer == null) return;
            IDatasetEditInfo pEdit = (IDatasetEditInfo)pFeaLayer.FeatureClass;
            ILayerEffects pLayerEff = (ILayerEffects)m_FeaLay;

            //数据源
            IDataLayer pDataLayer = (IDataLayer)m_FeaLay;
            IDatasetName pDatasetName = pDataLayer.DataSourceName as IDatasetName;
            IWorkspaceName pWSName = pDatasetName.WorkspaceName;
            IFeatureClassName pFCName = pDatasetName as IFeatureClassName;

            string strDataSourceType = "";
            string strFeatureDataSet = "";
            if (pFCName.FeatureDatasetName == null)
            {
                strDataSourceType = "数据类型 ：" + "个人空间数据库 要素类";
                strFeatureDataSet = "数 据 集 ：" + "无";
            }
            else
            {
                strDataSourceType = "数据类型 ：" + pFCName.FeatureDatasetName.Category + "(" + pFeaLayer.DataSourceType + ")";
                strFeatureDataSet = "数 据 集 ：" + pFCName.FeatureDatasetName.Name;
            }
            string strFeatureClass = "要 素 类 ：" + pDatasetName.Name;
            string strFeatureType = "要素类型 ：" + pFCName.FeatureType.ToString();
            string strGeometyrType = "几何类型 ：" + pFCName.ShapeType.ToString();
            string strLocationPath = "位    置 ：" + pWSName.PathName;
            string strRelativeBase = "相对路径 ：" + pDataLayer.RelativeBase;

            this.txtSoureFile.Text = strDataSourceType + "\r\n" +
                                          strFeatureDataSet + "\r\n" +
                                          strFeatureClass + "\r\n" +
                                          strFeatureType + "\r\n" +
                                          strGeometyrType + "\r\n" +
                                          strRelativeBase + "\r\n" +
                                          strLocationPath;

            this.txtXMin.Text = m_FeaLay.AreaOfInterest.XMin.ToString(".##");
            this.txtXMax.Text = m_FeaLay.AreaOfInterest.XMax.ToString(".##");
            this.txtYMin.Text = m_FeaLay.AreaOfInterest.YMin.ToString(".##");
            this.txtYMax.Text = m_FeaLay.AreaOfInterest.YMax.ToString(".##");

            //基本信息
            this.txtLayerName.Text = m_FeaLay.Name;
            if (pEdit != null)
            {
                this.chkEdit.Checked = pEdit.CanEdit;
            }

            if (m_FeaLay.FeatureClass != null)
            {
                int dfIndex = -1;
                this.chkVisible.Checked = pFeaLayer.Visible;
                this.cboDisplayField.Properties.Items.Clear();

                for (int i = 0; i < m_FeaLay.FeatureClass.Fields.FieldCount; i++)
                {
                    IField pField = m_FeaLay.FeatureClass.Fields.get_Field(i);
                    if (pField.Name == pFeaLayer.DisplayField)
                    {
                        dfIndex = i;
                    }
                    Class.Item item = new Class.Item(pField.AliasName, pField.Name);
                    cboDisplayField.Properties.Items.Add(item);
                }
                this.cboDisplayField.SelectedIndex = dfIndex;
                this.txtTransparency.Text = pLayerEff.Transparency.ToString();

                //this.txtScaleMin.Text = (pFeaLayer as ILayerGeneralProperties).LastMinimumScale.ToString();
                //this.txtScaleMax.Text = (pFeaLayer as ILayerGeneralProperties).LastMaximumScale.ToString();
                this.txtScaleMin.Text = pFeaLayer.MinimumScale.ToString();
                this.txtScaleMax.Text = pFeaLayer.MaximumScale.ToString();

                this.txtDescription.Text = (pFeaLayer as ILayerGeneralProperties).LayerDescription;
                if (pFeaLayer.MinimumScale == 0 && pFeaLayer.MaximumScale == 0)
                {
                    this.rdoGroupScale.SelectedIndex = 0;
                    txtScaleMin.Enabled = false;
                    txtScaleMax.Enabled = false;
                }
                else
                {
                    this.rdoGroupScale.SelectedIndex = 1;
                    txtScaleMin.Enabled = true;
                    txtScaleMax.Enabled = true;
                }

                chkShowTip.Checked = pFeaLayer.ShowTips;
                if (pFeaLayer is IGeoFeatureLayer)
                {
                    chkLabelAll.Checked = (pFeaLayer as IGeoFeatureLayer).DisplayAnnotation;
                }
                chkScaleSymbol.Checked = pFeaLayer.ScaleSymbols;

                m_pTableFields = pFeaLayer as ITableFields;

                //字段信息
                string strType = "";

                for (int i = 0; i < m_pTableFields.FieldCount; i++)
                {
                    IField pField = m_pTableFields.get_Field(i);
                    IFieldInfo pFieldInfo = m_pTableFields.get_FieldInfo(i);

                    switch (pField.Type)//类型
                    {
                        case esriFieldType.esriFieldTypeBlob:
                            strType = "二进制块";
                            break;
                        case esriFieldType.esriFieldTypeDate:
                            strType = "日期型";
                            break;
                        case esriFieldType.esriFieldTypeDouble:
                            strType = "双精度";
                            break;
                        case esriFieldType.esriFieldTypeGeometry:
                            strType = "几何形体";
                            break;
                        case esriFieldType.esriFieldTypeGlobalID:
                            strType = "GlobalID";
                            break;
                        case esriFieldType.esriFieldTypeGUID:
                            strType = "GUID";
                            break;
                        case esriFieldType.esriFieldTypeOID:
                            strType = "OID";
                            break;
                        case esriFieldType.esriFieldTypeRaster:
                            strType = "栅格";
                            break;
                        case esriFieldType.esriFieldTypeSingle:
                            strType = "单精度";
                            break;
                        case esriFieldType.esriFieldTypeSmallInteger:
                            strType = "短整形";
                            break;
                        case esriFieldType.esriFieldTypeString:
                            strType = "字符型";
                            break;

                    }
                    TreeListNode node = this.listViewFieldInfo.Nodes.Add(new object[] { pField.AliasName, pField.Name, strType });
                    node.Checked = pFieldInfo.Visible;
                }

                //初始化过滤条件面板
                initFilterCondition();
            }
        }

        /// <summary>
        /// 初始化过滤条件面板
        /// </summary>
        private void initFilterCondition()
        {
            listField.Items.Clear();
            listValue.Items.Clear();
            txtCondition.Text = (this.m_FeaLay as IFeatureLayerDefinition).DefinitionExpression;

            //得到当前要素图层的所有字段
            ITableFields pTableFields = m_FeaLay as ITableFields;
            IField pField;
            for (int i = 0; i < pTableFields.FieldCount; i++)
            {
                if (pTableFields.get_FieldInfo(i).Visible == true)
                {
                    pField = pTableFields.get_Field(i);
                    if (pField.Type == esriFieldType.esriFieldTypeOID ||
                        pField.Type == esriFieldType.esriFieldTypeSmallInteger ||
                        pField.Type == esriFieldType.esriFieldTypeInteger ||
                        pField.Type == esriFieldType.esriFieldTypeSingle ||
                        pField.Type == esriFieldType.esriFieldTypeDouble ||
                        pField.Type == esriFieldType.esriFieldTypeDate ||
                        pField.Type == esriFieldType.esriFieldTypeString)
                    {
                        //ListViewItem item = new ListViewItem(new string[] { pField.Name, pField.AliasName });
                        //item.Tag = pField;
                        Class.Item item = new Class.Item(pField.AliasName, pField);
                        listField.Items.Add(item);
                    }
                }

            }
        }
        #endregion

        #region 保存图层属性
        /// <summary>
        ///  保存图层属性
        /// </summary>
        /// <returns></returns>
        public bool SaveProperties()
        {
            if (txtLayerName.Text.Trim() == "") return false;
            if (this.rdoGroupScale.SelectedIndex == 1)
            {
                try
                {
                    if (double.Parse(txtScaleMin.Text) <= double.Parse(txtScaleMax.Text)) return false;
                }
                catch
                {
                    return false;
                }
            }
            m_FeaLay.Name = txtLayerName.Text;
            if (this.rdoGroupScale.SelectedIndex == 0)
            {
                m_FeaLay.MinimumScale = 0;
                m_FeaLay.MaximumScale = 0;
            }
            else if (this.rdoGroupScale.SelectedIndex == 1)
            {
                m_FeaLay.MinimumScale = double.Parse(txtScaleMin.Text);
                m_FeaLay.MaximumScale = double.Parse(txtScaleMax.Text);
            }

            (m_FeaLay as ILayerGeneralProperties).LayerDescription = txtDescription.Text; //地图描述

            Class.Item item = cboDisplayField.SelectedItem as Class.Item;
            m_FeaLay.DisplayField = item.Value.ToString();//主显示字段
            m_FeaLay.Visible = chkVisible.Checked;											//可见性
            (m_FeaLay as ILayerEffects).Transparency = short.Parse(txtTransparency.Text);	//透明度

            m_FeaLay.ShowTips = chkShowTip.Checked; //显示主显示字段
            if (m_FeaLay is IGeoFeatureLayer)
            {
                if (chkLabelAll.Checked)
                {
                    ChangeLayerAnno(m_FeaLay);
                }

                (m_FeaLay as IGeoFeatureLayer).DisplayAnnotation = chkLabelAll.Checked; //标注图层中所有要素
            }

            m_FeaLay.ScaleSymbols = chkScaleSymbol.Checked;//符号随比例尺缩放


            ////////字段信息
            m_pTableFields = m_FeaLay as ITableFields;
            for (int i = 0; i < listViewFieldInfo.Nodes.Count; i++)
            {
                int index = m_pTableFields.FindField(listViewFieldInfo.Nodes[i][2].ToString());
                if (index > -1)
                {
                    IFieldInfo pFieldInfo = m_pTableFields.get_FieldInfo(index);
                    pFieldInfo.Visible = listViewFieldInfo.Nodes[i].Checked == true ? true : false;
                }
            }

            if (!CheckCondition(this.m_FeaLay, txtCondition.Text))
            {
                MessageBox.Show("查询条件错误！");
            }
            else
            {
                (this.m_FeaLay as IFeatureLayerDefinition).DefinitionExpression = txtCondition.Text;
                m_MapCtrl.ActiveView.Refresh();
            }

            return true;
        }
        #endregion

        #region 改变一个图层的标注符号和标注字段
        /// <summary>
        /// 改变一个图层的标注符号和标注字段
        /// </summary>
        /// <param name="feaLayer"></param>
        private void ChangeLayerAnno(IFeatureLayer feaLayer)
        {
            IGeoFeatureLayer geoLayer = feaLayer as IGeoFeatureLayer;
            if (geoLayer == null) return;

            geoLayer.AnnotationProperties.Clear();

            RgbColorClass color = new RgbColorClass();
            color.Red = 255;
            color.Blue = 0;
            color.Green = 0;

            TextSymbolClass txtSymbol = new TextSymbolClass();
            stdole.StdFontClass font = new stdole.StdFontClass();
            font.Name = "黑体";
            font.Size = 12;
            txtSymbol.Font = font as stdole.IFontDisp;
            txtSymbol.Color = color;

            LineLabelPositionClass linePos = new LineLabelPositionClass();
            linePos.Parallel = false;
            linePos.Perpendicular = true;
            linePos.Above = true;

            LineLabelPlacementPrioritiesClass linePlac = new LineLabelPlacementPrioritiesClass();
            BasicOverposterLayerPropertiesClass basOve = new BasicOverposterLayerPropertiesClass();
            //不屏蔽这句有些线图层的标注不显示
            //basOve.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolyline;
            basOve.LineLabelPosition = linePos;
            basOve.LineLabelPlacementPriorities = linePlac;

            LabelEngineLayerPropertiesClass labelProp = new LabelEngineLayerPropertiesClass();
            labelProp.BasicOverposterLayerProperties = basOve;
            labelProp.Symbol = txtSymbol;
            labelProp.Expression = "[" + feaLayer.DisplayField + "]";

            geoLayer.AnnotationProperties.Add(labelProp);

        }
        #endregion

        #region 检查SQL语句
        /// <summary>
        /// 检查一个SQL语句对指定序号的图层做查询时是否正确
        /// </summary>
        /// <param name="m_LayerIndex"></param>
        /// <param name="m_SQL"></param>
        /// <returns></returns>
        public string CheckConditionEx(IFeatureLayer pFeaLay, string m_SQL)
        {
            if (pFeaLay == null) return "没有目标图层！";
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.WhereClause = m_SQL;
            try
            {
                pFeaLay.Search(pQueryFilter, false);
                return "查询条件正确！";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 检查查询条件是否正确
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="m_SQL"></param>
        /// <returns></returns>
        public bool CheckCondition(IFeatureLayer pFeaLay, string m_SQL)
        {
            if (pFeaLay == null) return false;
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.WhereClause = m_SQL;
            try
            {
                pFeaLay.Search(pQueryFilter, false);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listViewFieldInfo.Nodes.Count; i++)
            {
                this.listViewFieldInfo.Nodes[i].Checked = true;
            }
        }

        private void btnSelectNull_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listViewFieldInfo.Nodes.Count; i++)
            {
                this.listViewFieldInfo.Nodes[i].Checked = false;
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            if (m_FeaLay.FeatureClass.AliasName.Contains("_点") || m_FeaLay.FeatureClass.AliasName.Contains("_辅助"))
            {
                for (int i = 0; i < listViewFieldInfo.Nodes.Count; i++)
                {
                    string strFieldName = listViewFieldInfo.Nodes[i][1].ToString();

                    if (strFieldName == "NODEID" || strFieldName == "DETECTID" || strFieldName == "TOPHEIGHT" || strFieldName == "BOTTOMHEIGHT" || strFieldName == "MATTER"
                    || strFieldName == "SIZE_" || strFieldName == "PROAD" || strFieldName == "BUILDBY" || strFieldName == "BUILDYEAR" || strFieldName == "DESIGNBY" || strFieldName == "MANAGEBY")
                    {
                        listViewFieldInfo.Nodes[i].Checked = false;
                    }
                    else
                    {
                        listViewFieldInfo.Nodes[i].Checked = true;
                    }

                }
            }

            if (m_FeaLay.FeatureClass.AliasName.Contains("_线"))
            {
                for (int i = 0; i < listViewFieldInfo.Nodes.Count; i++)
                {
                    string strFieldName = listViewFieldInfo.Nodes[i][1].ToString();

                    if (strFieldName == "BUILDBY" || strFieldName == "BUILDYEAR" || strFieldName == "DESIGNBY" || strFieldName == "MANAGEBY"
                    || strFieldName == "PRESSURE" || strFieldName == "NUMBER_" || strFieldName == "HOLE" || strFieldName == "HOLEUSING" || strFieldName == "VOLTAGE")
                    {
                        listViewFieldInfo.Nodes[i].Checked = false;
                    }
                    else
                    {
                        listViewFieldInfo.Nodes[i].Checked = true;
                    }

                }
            }

        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            this.txtCondition.Text += " > ";
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            this.txtCondition.Text += " = ";
        }

        private void btnLess_Click(object sender, EventArgs e)
        {
            this.txtCondition.Text += " < ";
        }

        private void btnNoLess_Click(object sender, EventArgs e)
        {
            this.txtCondition.Text += " >= ";
        }

        private void btnNoEqual_Click(object sender, EventArgs e)
        {
            this.txtCondition.Text += " <> ";
        }

        private void btnNoMore_Click(object sender, EventArgs e)
        {
            this.txtCondition.Text += " <= ";
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            this.txtCondition.Text += " or ";
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {
            this.txtCondition.Text += " and ";
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            this.txtCondition.Text += " like ";
        }

        private void listField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listField.Items.Count == 0) return;
            if (listField.SelectedItems.Count == 0) return;

            Class.Item selItem = listField.SelectedItems[0] as Class.Item;
            IField pFld = selItem.Value as IField;
            ArrayList uniqueValues = Class.Common.GetUniqueValue(this.m_FeaLay.FeatureClass as ITable, new string[] { pFld.Name});
            this.listValue.Items.Clear();

            IField pField = selItem.Value as IField;

            if (pField.Domain is ICodedValueDomain)//如果为编码域
            {
                for (int i = 0; i < uniqueValues.Count; i++)
                {
                    if (uniqueValues[i].ToString() != "<Null>")
                    {
                        listValue.Items.Add(Class.Common.GetCodedDescriptionDomainValue(pField.Domain as ICodedValueDomain, uniqueValues[i].ToString()));
                    }
                }

            }
            else//如果不为编码域
            {
                if (pField.Type == esriFieldType.esriFieldTypeString)
                {
                    for (int i = 0; i < uniqueValues.Count; i++)
                    {
                        if (uniqueValues[i].ToString() != "<Null>")
                        {
                            listValue.Items.Add("'" + uniqueValues[i].ToString() + "'");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < uniqueValues.Count; i++)
                    {
                        if (uniqueValues[i].ToString() != "<Null>")
                        {
                            listValue.Items.Add(uniqueValues[i].ToString());
                        }
                    }
                }
            }

        }

        private void listField_DoubleClick(object sender, EventArgs e)
        {
            if (listField.Items.Count > 0)
            {
                if (listField.SelectedItems.Count > 0)
                {
                    Class.Item selItem = this.listField.SelectedItems[0] as Class.Item;
                    IField pFld = selItem.Value as IField;
                    this.txtCondition.Text += pFld.Name;
                }
            }

        }

        private void txtStation_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            string s;

            for (i = 0; i < this.listValue.Items.Count; i++)
            {
                s = this.listValue.Items[i].ToString().Replace("'", "");
                if (s.Length >= txtStation.Text.Length)
                {
                    if (s.Substring(0, txtStation.Text.Length).ToUpper() == txtStation.Text.ToUpper())
                    {
                        this.listValue.SelectedIndex = i;
                        this.listValue.TopIndex = i;
                        return;
                    }
                }
            }

        }

        private void listValue_DoubleClick(object sender, EventArgs e)
        {
            if (this.listValue.SelectedItem == null) return;

            Class.Item selItem = listField.SelectedItems[0] as Class.Item;
            IField pField = selItem.Value as IField;

            if (pField.Domain is ICodedValueDomain)//如果为编码域
            {
                this.txtCondition.Text += Class.Common.GetCodedValueDomainValue(pField.Domain as ICodedValueDomain, this.listValue.SelectedItem.ToString());
            }
            else//如果不为编码域
            {
                this.txtCondition.Text += this.listValue.SelectedItem.ToString();
            }

        }

    }
}
