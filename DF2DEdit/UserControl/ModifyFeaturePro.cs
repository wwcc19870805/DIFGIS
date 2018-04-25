/*-----------------------------------------------------------------------------------------
			// Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：ModifyFeaturePro.cs
			// 文件功能描述：修改要素属性
			//
			// 
			// 创建标识：LuoXuan 20171022
-----------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraRichEdit;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS;
using ESRI.ArcGIS.Controls;
using DF2DEdit.Interface;
using DF2DEdit.CmdModify;

namespace DF2DEdit.UserControl
{
    public partial class ModifyFeaturePro : XtraUserControl
    {
        private const string SELECTION_COUNT = "选中要素： %";

        private ContextMenu m_CMFeature = new ContextMenu(); //要素树上的快捷菜单，高亮，查看，复制属性、粘贴属性、取消选择，删除
        private ContextMenu m_CMGeometry = new ContextMenu(); //geometry网格上的快捷菜单，前面插入，后面插入，删除

        private IMapControl2 m_MapControl = null; //MapControl
        private IActiveView m_ActiveView = null; //MapControl.ActiveView
        private IMap m_Map = null; //地图
        private ArrayList m_AllLayers = new ArrayList(); //所有图层
        private IEnvelope m_pEnvelope = new EnvelopeClass();
        private IFeatureLayer m_FeatureLayer;         //选择树节点所属的图层
        private IWorkspace m_Workspace;                            //当前图层的工作空间
        private IWorkspaceEdit m_WorkspaceEdit;                        //当前图层的工作空间编辑
        private IFeature m_Feature;              //选择树节点关联的要素
        private IFeature m_ClipboardFeature;
        private ITableFields m_pTableFields = null; //图层属性表的字段    －－－－－－－－By 袁怀月－－－－－－－－－
        private DataTable m_DtGeo = null; //要素的几何数据中的点信息
        private DataView m_DvPart = null; //要素的几何数据中的点信息

        public ModifyFeaturePro()
        {
            InitializeComponent();

            initControls();
        }

        /////////////////////属性\\\\\\\\\\\\\\\\\\\\

        /// <summary>
        /// 设置获取地图和数据的MapControl
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
                if (m_MapControl == null) return;
                m_ActiveView = m_MapControl.ActiveView;
                m_Map = m_ActiveView.FocusMap;
                m_AllLayers = (ArrayList)Class.Common.GetAllLayersFromMap(this.m_Map, typeof(ArrayList)); //By LuoX 2008.11.07
                if (m_AllLayers.Count > 0)
                {
                    m_Workspace = ((m_AllLayers[0] as IFeatureLayer).FeatureClass as IDataset).Workspace;
                    m_WorkspaceEdit = m_Workspace as IWorkspaceEdit;

                    RefreshSelection();
                }
            }
        }

        /////////////////////////////////私有方法\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        #region 初始化控件
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void initControls()
        {
            txtSelectionCount.Text = SELECTION_COUNT.Replace("%", "");
            tvwFeature.Nodes.Clear();
            cboPart.Properties.Items.Clear();
            this.uGridProInfo.DataSource = null;
            this.uGridGeoInfo.DataSource = null;

            showGrid();
            m_DtGeo = CreateNewGeoTable();

        }
        #endregion

        #region 刷新选择集后，显示选择集内容
        /// <summary>
        /// 刷新选择集后，显示选择集内容
        /// </summary>
        public void RefreshSelection()
        {
            IFeatureLayer pFeaLay;
            txtSelectionCount.Text = SELECTION_COUNT.Replace("%", m_ActiveView.FocusMap.SelectionCount.ToString());
            tvwFeature.BeginUpdate();
            this.tvwFeature.Nodes.Clear();
            //cboPart.Items.Clear();
            this.uGridProInfo.DataSource = null;
            this.uGridGeoInfo.DataSource = null;
            //重置注记面板中所有控件的状态
            this.txtText.Text = "";
            this.cmbFontName.SelectedIndex = -1;
            this.cmbScale.SelectedIndex = -1;
            this.cmbSize.SelectedIndex = -1;
            this.btnColor.Color = Color.Empty;
            this.btnBold.Checked = false;
            this.btnItalic.Checked = false;
            this.btnUnder.Checked = false;
            this.rdoCenter.Checked = false;
            this.rdoFull.Checked = false;
            this.rdoLeft.Checked = false;
            this.rdoRight.Checked = false;
            this.degreeControl1.Angle = 0.0;

            for (int i = 0; i < m_AllLayers.Count; i++)
            {
                pFeaLay = m_AllLayers[i] as IFeatureLayer;

                try
                {
                    addLayerSelectionToTree(pFeaLay, tvwFeature);
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    int m = 1;
                }
            }
            tvwFeature.EndUpdate();
            tvwFeature.ExpandAll();
            m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, m_ActiveView.Extent);

            if (tvwFeature.Nodes.Count > 0)
            {
                tvwFeature.FocusedNode = tvwFeature.Nodes[0].Nodes[0];
                //tvwFeature.Nodes[0].EnsureVisible();
            }
            //m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,null,m_pEnvelope);
        }
        #endregion

        #region 更新要素的属性信息，更新几何信息中一个点的信息
        /// <summary>
        /// 更新要素的属性信息
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="OID"></param>
        /// <param name="fieldIndex"></param>
        /// <param name="newValue"></param>
        [Obsolete("使用该方法的另外一个重载")]
        private void SaveFeatureProperty(IFeatureLayer pFeaLay, int OID, int fieldIndex, object newValue)
        {
            QueryFilterClass pFilter = new QueryFilterClass();
            pFilter.WhereClause = pFeaLay.FeatureClass.OIDFieldName + " = " + OID;
            IFeatureCursor pFeaCur = pFeaLay.Search(pFilter, false);
            if (pFeaCur != null)
            {
                IFeature pFeature = pFeaCur.NextFeature();//pFeaCur.NextFeature();
                if (pFeature != null)
                {
                    pFeature.set_Value(fieldIndex, newValue);
                    pFeature.Store();
                }
            }
        }

        /// <summary>
        /// 更新要素的属性信息
        /// </summary>
        /// <param name="pFeature"></param>
        /// <param name="fieldIndex"></param>
        /// <param name="newValue"></param>
        private void SaveFeatureProperty(IFeature pFeature, int fieldIndex, object newValue)
        {
            if (pFeature != null && pFeature.Fields.get_Field(fieldIndex).Editable)
            {
                if ((newValue as string) == "")
                {
                    newValue = System.DBNull.Value;
                }

                pFeature.set_Value(fieldIndex, newValue);

                pFeature.Store();
            }
        }

        /// <summary>
        /// 更新要素的几何信息
        /// </summary>
        /// <param name="pFeature"></param>
        /// <param name="partIndex"></param>
        /// <param name="pointIndex"></param>
        /// <param name="newPoint"></param>
        private void SaveFeatureGeometry(int partIndex, int pointIndex, IPoint newPoint)
        {
            IUpdatePoint pModGeo = null;
            switch (m_Feature.Shape.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    pModGeo = new ModifyPoint();
                    break;
                case esriGeometryType.esriGeometryMultipoint:
                    pModGeo = new ModifyMultiPoint();
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    pModGeo = new ModifyPolyline();
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    pModGeo = new ModifyPolygon();
                    break;
            }
            pModGeo.Feature = m_Feature;
            pModGeo.UpdatePoint(partIndex, pointIndex, newPoint);
            m_ActiveView.Refresh();
        }
        #endregion

        #region 显示所选的网格控件
        /// <summary>
        /// 显示所选的网格控件
        /// </summary>
        private void showGrid()
        {
            if (btnPro.Checked)
            {
                btnGeo.Checked=false;
                pnlPro.Visible = true;
                pnlBtn.Visible = false;
                pnlAnno.Visible = false;

                if (tvwFeature.FocusedNode != null)
                {
                    if (tvwFeature.FocusedNode.ParentNode == null)//选择的是图层结点
                    {
                        IFeatureLayer pFLayer = tvwFeature.FocusedNode.Tag as IFeatureLayer;
                        if (pFLayer != null && pFLayer is IAnnotationLayer)
                        {
                            showInfo(pFLayer);
                        }
                    }
                    else
                    {
                        m_Feature = tvwFeature.FocusedNode.Tag as IFeature;
                        if (m_Feature != null && m_Feature.FeatureType == esriFeatureType.esriFTAnnotation)
                        {
                            showProInfo(m_Feature);
                        }
                    }
                }
            }
            else if (btnGeo.Checked)
            {
                //rdoPro.Checked = false;
                pnlBtn.Visible = true;
                pnlPro.Visible = false;
                pnlAnno.Visible = false;
            }
            else if (btnAnno.Checked)
            {
                btnGeo.Checked = false;
                pnlAnno.Visible = true;
                pnlPro.Visible = false;
                pnlBtn.Visible = false;

                if (tvwFeature.FocusedNode != null)
                {
                    if (tvwFeature.FocusedNode.ParentNode == null)//选择的是图层结点
                    {
                        IFeatureLayer pFLayer = tvwFeature.FocusedNode.Tag as IFeatureLayer;
                        if (pFLayer != null && pFLayer is IAnnotationLayer)
                        {
                            showAnnoInfo(pFLayer);
                        }
                    }
                    else
                    {
                        m_Feature = tvwFeature.FocusedNode.Tag as IFeature;
                        if (m_Feature != null && m_Feature.FeatureType == esriFeatureType.esriFTAnnotation)
                        {
                            getAnnoInfo(m_Feature);
                        }
                    }
                    this.btnApply.Enabled = false;
                    this.btnReset.Enabled = false;
                }
            }
        }
        #endregion

        #region 显示要素类中所有选中要素的信息
        /// <summary>
        /// 显示一个要素类中所有选中要素的信息
        /// </summary>
        /// <param name="pFea"></param>
        private void showInfo(IFeatureLayer pFeaLay)
        {
            ICursor pCur;
            IFeature pFea;
            IFeatureSelection pFeaSel = pFeaLay as IFeatureSelection;
            pFeaSel.SelectionSet.Search(null, false, out pCur);

            pFea = pCur.NextRow() as IFeature;
            if (pFea != null)
            {
                //创建数据源DataTable
                this.uGridProInfo.DataSource = null;
                DataTable dataTable = new DataTable();//数据源
                DataColumn dataCol;
                DataRow dataRow;

                //添加字段
                //添加字段
                dataCol = new DataColumn("Name");
                dataCol.Caption = "属性";
                dataCol.DataType = typeof(System.String);
                dataCol.ReadOnly = true;
                dataTable.Columns.Add(dataCol);
                dataCol = new DataColumn("Value");
                dataCol.Caption = "值";
                dataCol.DataType = typeof(System.String);
                dataTable.Columns.Add(dataCol);

                IField pField;
                string strFieldValue = "";
                //填写值
                for (int i = 0; i < pFea.Fields.FieldCount; i++)
                {
                    try
                    {
                        if (m_pTableFields.get_FieldInfo(i).Visible == true)
                        {
                            dataRow = dataTable.NewRow();
                            pField = pFea.Fields.get_Field(i);
                            dataRow[0] = pField.AliasName;
                            strFieldValue = getFeaLayFieldValue(pFeaLay, pField);

                            if (pField.Domain is ICodedValueDomain)
                            {
                                dataRow[1] = Class.Common.GetCodedDescriptionDomainValue(pField.Domain as ICodedValueDomain, strFieldValue);//字段值
                            }
                            else
                            {
                                dataRow[1] = strFieldValue;//字段值
                            }
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString() + pFea.OID.ToString() + pFea.Class.AliasName + i.ToString());

                    }
                }
                this.uGridProInfo.DataSource = dataTable;
                this.gViewProInfo.PopulateColumns(); //显示gridCOntrol　数据
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pCur);

            //this.AddValueList(pFea);
        }

        /// <summary>
        /// 获得要素类选择集某个字段的值，如何该字段所有要素的值相同则返回值为公共值，否则返回空字符串
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="pField"></param>
        /// <returns></returns>
        private string getFeaLayFieldValue(IFeatureLayer pFeaLay, IField pField)
        {
            string strFieldValue = "";

            ICursor pCur;
            IFeature pFea;
            IFeatureSelection pFeaSel = pFeaLay as IFeatureSelection;
            pFeaSel.SelectionSet.Search(null, false, out pCur);
            pFea = pCur.NextRow() as IFeature;

            if (pFea == null) return strFieldValue;

            object val = pFea.get_Value(pFea.Fields.FindField(pField.Name));

            strFieldValue = pFea.get_Value(pFea.Fields.FindField(pField.Name)).ToString();

            pFea = pCur.NextRow() as IFeature;
            while (pFea != null)
            {
                string strTempFieldValue = pFea.get_Value(pFea.Fields.FindField(pField.Name)).ToString();

                if (strTempFieldValue != strFieldValue)
                {
                    strFieldValue = "";
                    break;
                }

                pFea = pCur.NextRow() as IFeature;
            }


            if (pField.Type == esriFieldType.esriFieldTypeGeometry)
            {
                strFieldValue = "<几何数据>";
            }
            else if (pField.Type == esriFieldType.esriFieldTypeRaster)
            {
                strFieldValue = "<栅格数据>";
            }
            else if (pField.Type == esriFieldType.esriFieldTypeBlob)
            {
                strFieldValue = "<二进制数据>";
            }
            else if (pField.Type == esriFieldType.esriFieldTypeSingle && strFieldValue != "")
            {
                if (val != null && val != System.DBNull.Value)
                {
                    Single s = (Single)val;
                    strFieldValue = s.ToString("0.###");
                }
                else
                {
                    strFieldValue = Class.Common.ConvertNull(val);
                }
            }
            else if (pField.Type == esriFieldType.esriFieldTypeDouble && strFieldValue != "")
            {
                if (val != null && val != System.DBNull.Value)
                {
                    Double d = (Double)val;
                    strFieldValue = d.ToString("0.###");

                }
                else
                {
                    strFieldValue = Class.Common.ConvertNull(val);
                }
            }
            else if (pField.Type == esriFieldType.esriFieldTypeDate && strFieldValue != "")
            {
                if (val is DateTime)
                {
                    strFieldValue = ((DateTime)val).ToString("yyyy-MM-dd");
                }
                else
                {
                    strFieldValue = "<Null>";
                }
            }

            return strFieldValue;

        }
        #endregion

        #region 显示属性信息
        /// <summary>
        /// 显示所有的属性信息
        /// </summary>
        private void showProInfo(IFeature pFea)
        {
            //创建数据源DataTable
            this.uGridProInfo.DataSource = null;
            DataTable dataTable = new DataTable();//数据源
            DataColumn dataCol;
            DataRow dataRow;
            IField pField;

            //添加字段
            dataCol = new DataColumn("Name");
            dataCol.Caption = "属性";
            dataCol.DataType = typeof(System.String);
            dataCol.ReadOnly = true;
            dataTable.Columns.Add(dataCol);
            dataCol = new DataColumn("Value");
            dataCol.Caption = "值";
            dataCol.DataType = typeof(System.String);
            dataTable.Columns.Add(dataCol);

            //填写值
            for (int i = 0; i < pFea.Fields.FieldCount; i++)
            {
                if (m_pTableFields.get_FieldInfo(i).Visible == true)
                {
                    dataRow = dataTable.NewRow();
                    pField = pFea.Fields.get_Field(i);
                    dataRow[0] = pField.AliasName;

                    if (pField.Domain is ICodedValueDomain)
                    {
                        dataRow[1] = Class.Common.GetCodedDescriptionDomainValue(pField.Domain as ICodedValueDomain, Class.Common.GetFieldValue(pField, pFea.get_Value(i)));//字段值
                    }
                    else
                    {
                        dataRow[1] = Class.Common.GetFieldValue(pField, pFea.get_Value(i));//字段值
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            this.uGridProInfo.DataSource = dataTable;
            this.gViewProInfo.PopulateColumns();
        }
        #endregion

        #region 显示Geometry中 part 部分的点信息
        /// <summary>
        /// 显示Geometry中 part 部分的点信息
        /// </summary>
        /// <param name="part"></param>
        private void showGeoInfo(IFeature pFea, int part)
        {
            m_DvPart = this.m_DtGeo.DefaultView;
            m_DvPart.RowFilter = "部分 = " + (part + 1).ToString();

            //创建数据源DataTable
            this.uGridGeoInfo.DataSource = null;
            DataTable dt = new DataTable();//数据源
            DataColumn column;
            DataRow dataRow;

            //添加字段
            column = new DataColumn();
            column.ColumnName = "序号";
            column.DataType = typeof(System.Int32);
            column.ReadOnly = true;
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "X坐标";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Y坐标";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Z高程";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "M埋深";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);

            //填写字段值
            for (int i = 0; i < this.m_DvPart.Count; i++)
            {
                dataRow = dt.NewRow();
                for (int j = 1; j < this.m_DtGeo.Columns.Count; j++)
                {
                    if (m_DvPart[i][j].GetType().Name == "Double")
                    {
                        dataRow[j - 1] = Math.Round((double)m_DvPart[i][j], 3);
                    }
                    else
                    {
                        dataRow[j - 1] = m_DvPart[i][j];
                    }
                }
                dt.Rows.Add(dataRow);
            }
            this.uGridGeoInfo.DataSource = dt;
            this.gGeoView.PopulateColumns();
            //this.SetWidth(uGridGeoInfo);
            //this.SetMVisable(pFea, uGridGeoInfo);
            //this.uGridGeoInfo.DisplayLayout.Bands[0].Columns[0].AutoEdit = false;
            //this.uGridGeoInfo.DisplayLayout.Bands[0].Columns[0].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
        }
        #endregion

        #region 得到选中要素的Geometry中所有点信息
        /// <summary>
        /// 得到一个Shape字段中的Geometry所有点的信息
        /// </summary>
        private void getGeoInfo(IFeature pFea, DataTable dtGeometry)
        {
            IPointCollection pPointColl;
            IPoint pPoint;
            DataRow dr;
            int i;
            int j;
            string geoID = "第 0 部分";

            dtGeometry.Clear();
            this.cboPart.Properties.Items.Clear();
            try
            {
                IGeometryCollection pGeoColl = null;
                IGeometry pGeo;
                if ((pFea.Shape.GeometryType != esriGeometryType.esriGeometryPoint))
                {
                    pGeoColl = (IGeometryCollection)pFea.Shape;
                }
                else
                {
                    object a = Type.Missing;
                    pGeoColl = new MultipointClass();
                    pGeoColl.AddGeometry((IPoint)pFea.Shape, ref a, ref a);
                }

                for (i = 0; i < pGeoColl.GeometryCount; i++)
                {
                    this.cboPart.Properties.Items.Add(geoID.Replace("0", (i + 1).ToString()));
                    pGeo = pGeoColl.get_Geometry(i);
                    if (pGeo.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        pPoint = (IPoint)pGeo;
                        dr = dtGeometry.NewRow();
                        dr["部分"] = i + 1;
                        dr["序号"] = 1;
                        dr["X坐标"] = pPoint.Y;
                        dr["Y坐标"] = pPoint.X;
                        dr["Z高程"] = pPoint.Z;
                        dr["M埋深"] = pPoint.M;
                        dtGeometry.Rows.Add(dr);
                    }
                    else
                    {
                        pPointColl = (IPointCollection)pGeo;
                        for (j = 0; j < pPointColl.PointCount; j++)
                        {
                            pPoint = pPointColl.get_Point(j);
                            dr = dtGeometry.NewRow();
                            dr["部分"] = i + 1;
                            dr["序号"] = j + 1;
                            dr["X坐标"] = pPoint.Y;
                            dr["Y坐标"] = pPoint.X;
                            dr["Z高程"] = pPoint.Z;
                            dr["M埋深"] = pPoint.M;
                            dtGeometry.Rows.Add(dr);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 显示一个要素的所有信息
        /// <summary>
        /// 显示一个要素的详细信息
        /// </summary>
        /// <param name="pFea"></param>
        private void showInfo(IFeature pFea)
        {
            if (pFea == null) return;
            showProInfo(pFea);
            getGeoInfo(pFea, m_DtGeo);
            if (cboPart.Properties.Items.Count > 0)
            {
                cboPart.SelectedIndex = 0;
            }

        }
        #endregion

        #region 为单元格添加下拉框
        /// <summary>
        /// 为单元格添加下拉框
        /// </summary>
        private void AddValueList(IFeature pFeature)
        {
            string strFldName;
            IField pField;

            for (int i = 0; i < this.gViewProInfo.RowCount; i++)
            {
                strFldName = this.gViewProInfo.GetRowCellValue(i, "Name").ToString();
                pField = pFeature.Fields.get_Field(pFeature.Fields.FindFieldByAliasName(strFldName));

                if (pField.Domain is ICodedValueDomain)
                {
                    //vlist = new Infragistics.Win.ValueList();
                    //for (int j = 0; j < (pField.Domain as ICodedValueDomain).CodeCount; j++)
                    //{
                    //    vlist.ValueListItems.Add((pField.Domain as ICodedValueDomain).get_Name(j));
                    //}
                    //uGridProInfo.Rows[i].Cells[1].ValueList = vlist;
                    //uGridProInfo.Rows[i].Cells[1].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
                }
            }
        }
        #endregion

        #region 得到图层中的选择要素并将OID（或主显示字段）写到树
        /// <summary>
        /// 得到图层中的选择要素并将OID（或主显示字段）写到树
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="tvw"></param>
        /// <returns>是否有结果</returns>
        private void addLayerSelectionToTree(IFeatureLayer pFeaLay, TreeList tvw)
        {
            int nLayerId = -1;
            IFeature pFea;
            TreeListNode layNode;
            TreeListNode tmpNode;
            int displayFieldIndex;
            string strNodeText = "";
            string strDisText = "";
            IField pDisplayField;
            bool bCodeDomainFld = false;

            if (pFeaLay.Valid == false) return;

            displayFieldIndex = pFeaLay.FeatureClass.FindField(pFeaLay.DisplayField);
            if (displayFieldIndex == -1) return;
            pDisplayField = pFeaLay.FeatureClass.Fields.get_Field(displayFieldIndex);
            if (pDisplayField.Domain is ICodedValueDomain)
                bCodeDomainFld = true;
            IFeatureSelection pFeaSel = pFeaLay as IFeatureSelection;

            //直接将IRow转换成IFeature
            ICursor pCur;
            pFeaSel.SelectionSet.Search(null, false, out pCur);
            pFea = pCur.NextRow() as IFeature;
            if (pFea != null)
            {
                layNode = tvw.AppendNode(new object[] { pFeaLay.Name }, -1, pFeaLay);
                nLayerId = layNode.Id;
            }

            while (pFea != null)
            {
                //----------By 罗璇 2008.11.30-------------//
                if (displayFieldIndex > -1) //如果图层设置了主显示字段，树节点的文本值就显示该字段
                {
                    strNodeText = Class.Common.ConvertNull(pFea.get_Value(displayFieldIndex));
                    if (strNodeText != "")
                    {
                        if (!bCodeDomainFld)
                            strDisText = strNodeText;
                        else
                            strDisText = Class.Common.GetCodedDescriptionDomainValue(pDisplayField.Domain as ICodedValueDomain, strNodeText);
                    }
                    else
                        strDisText = pFea.OID.ToString();
                }
                else //如果没有设置主显示字段，树节点的文本值就要素ID
                {
                    strDisText = pFea.OID.ToString();
                }

                //----------By 罗璇 2008.11.30-------------//
                tmpNode = tvw.AppendNode(new object[] { strDisText }, nLayerId, pFea);
                tmpNode.SetValue(0, strDisText);
                //tmpNode.Tag = pFea;
                pFea = pCur.NextRow() as IFeature;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pCur);
        }
        #endregion

        #region 闪烁一个要素
        /// <summary>
        /// 闪烁一个要素
        /// </summary>
        /// <param name="pFeature"></param>
        private void FlashFeature(IFeature pFeature)
        {
            IGeometry pGeo = pFeature.Shape;
            if (pGeo != null)
            {
                m_MapControl.FlashShape(pGeo, 1, 100, Class.Common.GetDefaultSymbol(pGeo.GeometryType));
            }

        }

        /// <summary>
        /// 闪烁要素上的一个点
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="mapControl"></param>
        public void FlashNode(ESRI.ArcGIS.Geometry.IGeometry shape)
        {
            IRgbColor color;
            SimpleMarkerSymbolClass marker;

            color = new RgbColorClass();
            color.Red = 255;
            color.Green = 0;
            color.Blue = 0;
            marker = new SimpleMarkerSymbolClass();
            marker.Size = 30;
            marker.Style = esriSimpleMarkerStyle.esriSMSCircle;
            marker.Color = color;

            m_MapControl.FlashShape(shape, 3, 100, marker);
        }

        #endregion

        #region 构造存放Geometry信息的Datatable
        /// <summary>
        /// 存放一个要素几何信息的DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateNewGeoTable()
        {
            DataTable dt = new DataTable();
            DataColumn column;

            DataColumn col;
            col = new DataColumn();
            col.ColumnName = "部分";
            col.DataType = typeof(System.Int32);
            dt.Columns.Add(col);

            column = new DataColumn();
            column.ColumnName = "序号";
            column.DataType = typeof(System.Int32);
            dt.Columns.Add(column);
            dt.PrimaryKey = new DataColumn[] { col, column };

            column = new DataColumn();
            column.ColumnName = "X坐标";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Y坐标";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Z高程";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "M埋深";
            column.DataType = typeof(System.Double);
            dt.Columns.Add(column);
            return dt;
        }
        #endregion

        ///////////////////////////////////////////////控件事件\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        #region“属性信息”按钮
        /// <summary>
        /// “属性信息”按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPro_Click(object sender, EventArgs e)
        {
            pnlPro.Visible = true;
            pnlAnno.Visible = false;
            pnlGeo.Visible = false;

            if (tvwFeature.FocusedNode != null)
            {
                if (tvwFeature.FocusedNode.ParentNode == null)//选择的是图层结点
                {
                    IFeatureLayer pFLayer = tvwFeature.FocusedNode.Tag as IFeatureLayer;
                    if (pFLayer != null && pFLayer is IAnnotationLayer)
                    {
                        showInfo(pFLayer);
                    }
                }
                else
                {
                    m_Feature = tvwFeature.FocusedNode.Tag as IFeature;
                    if (m_Feature != null && m_Feature.FeatureType != esriFeatureType.esriFTAnnotation)
                    {
                        showProInfo(m_Feature);
                    }
                }
            }

        }
        #endregion

        #region "空间信息"按钮
        /// <summary>
        /// "空间信息"按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGeo_Click(object sender, EventArgs e)
        {
            pnlGeo.Visible = true;
            pnlPro.Visible = false;
            pnlAnno.Visible = false;

            if (tvwFeature.FocusedNode != null && tvwFeature.FocusedNode.ParentNode != null)
            {
                m_Feature = tvwFeature.FocusedNode.Tag as IFeature;
                if (m_Feature != null && m_Feature.FeatureType != esriFeatureType.esriFTAnnotation)
                {
                    showGeoInfo(tvwFeature.FocusedNode.Tag as IFeature, cboPart.SelectedIndex);
                }
            }
        }
        #endregion

        #region "注记信息"按钮
        /// <summary>
        /// "注记信息"按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnno_Click(object sender, EventArgs e)
        {
            pnlAnno.Visible = true;
            pnlPro.Visible = false;
            pnlGeo.Visible = false;

            if (tvwFeature.FocusedNode != null)
            {
                if (tvwFeature.FocusedNode.ParentNode == null)//选择的是图层结点
                {
                    IFeatureLayer pFLayer = tvwFeature.FocusedNode.Tag as IFeatureLayer;
                    if (pFLayer != null && pFLayer is IAnnotationLayer)
                    {
                        showAnnoInfo(pFLayer);
                    }
                }
                else
                {
                    m_Feature = tvwFeature.FocusedNode.Tag as IFeature;
                    if (m_Feature != null && m_Feature.FeatureType != esriFeatureType.esriFTAnnotation)
                    {
                        getAnnoInfo(m_Feature);
                    }
                }
                this.btnApply.Enabled = false;
                this.btnReset.Enabled = false;
            }
        }
        #endregion

        #region 点击要素或图层节点
        /// <summary>
        /// 点击要素或图层节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvwFeature_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (tvwFeature.FocusedNode != null)
            {
                if (tvwFeature.FocusedNode.ParentNode == null)//选择的是图层结点
                {
                    cboPart.Properties.Items.Clear();
                    this.uGridGeoInfo.DataSource = null;
                    this.uGridProInfo.DataSource = null;

                    IFeatureLayer pFLayer = tvwFeature.FocusedNode.Tag as IFeatureLayer;
                    m_pTableFields = (tvwFeature.FocusedNode.Tag as IFeatureLayer) as ITableFields;//-----By 袁怀月------

                    if (pFLayer is IAnnotationLayer)//如果为注记图层，则显示属性信息及注记信息面板
                    {
                        if (!this.btnAnno.Visible)
                        {
                            this.btnAnno.Visible = true;
                            this.btnGeo.Visible = false;
                            this.btnGeo.Checked = true;
                            this.pnlPro.Visible = true;
                            this.pnlAnno.Visible = false;
                        }
                        showInfo(pFLayer);//将该图层所有选择注记的属性加载到网格中显示
                        showAnnoInfo(pFLayer);//将该图层所有选择注记的注记信息加载到网格中显示
                    }
                    else//如果不为注记图层，则显示属性信息及空间信息面板
                    {
                        if (!this.btnGeo.Visible)
                        {
                            this.btnGeo.Visible = true;
                            this.btnAnno.Visible = false;
                            this.btnPro.Checked = true;
                            this.pnlPro.Visible = true;
                            this.pnlGeo.Visible = false;
                        }
                        this.pnlPro.Visible = true;
                        this.pnlGeo.Visible = false;

                        showInfo(pFLayer);//将该图层所有选择要素的属性加载到网格中显示
                    }
                }
                else//选择的是要素结点
                {
                    if (m_FeatureLayer != tvwFeature.FocusedNode.ParentNode.Tag as IFeatureLayer)
                    {
                        m_FeatureLayer = tvwFeature.FocusedNode.ParentNode.Tag as IFeatureLayer;
                        m_pTableFields = m_FeatureLayer as ITableFields;//---------------------By 袁怀月------------------------
                    }

                    m_Feature = tvwFeature.FocusedNode.Tag as IFeature;
                    m_pEnvelope = m_Feature.Shape.Envelope;

                    if (m_Feature.FeatureType == esriFeatureType.esriFTAnnotation)//如果为注记要素
                    {
                        if (!this.btnAnno.Visible)
                        {
                            this.btnAnno.Visible = true;
                            this.btnGeo.Visible = false;
                            this.btnPro.Checked = true;
                            this.pnlPro.Visible = true;
                            this.pnlAnno.Visible = false;
                        }
                        showAnnoInfo(m_Feature);//显示要素的属性信息及注记信息面板
                    }
                    else//如果不为注记要素
                    {
                        if (!this.btnGeo.Visible)
                        {
                            this.btnGeo.Visible = true;
                            this.btnAnno.Visible = false;
                            this.btnPro.Checked = true;
                            this.pnlPro.Visible = true;
                            this.pnlGeo.Visible = false;
                        }
                        this.pnlPro.Visible = true;
                        this.pnlGeo.Visible = false;

                        showInfo(m_Feature);//显示要素的属性信息及空间信息面板
                    }
                    FlashFeature(m_Feature);
                }
            }
        }
        #endregion

        #region 闪烁要素
        /// <summary>
        /// 闪烁要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFlash_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_Feature == null || cboPart.SelectedIndex == -1) return;
                if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    m_MapControl.FlashShape(m_Feature.Shape, 1, 100, Class.Common.GetDefaultSymbol(esriGeometryType.esriGeometryPoint));
                }
                else
                {
                    IGeometryCollection pGeoColl = m_Feature.Shape as IGeometryCollection;
                    IGeometry pGeo = null;
                    IPointCollection pColl;
                    if (pGeoColl != null)
                    {
                        IPointCollection pPointColl = pGeoColl.get_Geometry(cboPart.SelectedIndex) as IPointCollection;
                        if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryMultipoint)
                        {
                            pGeo = new PointClass();
                            pColl = pGeo as IPointCollection;
                            pColl.AddPointCollection(pPointColl);
                        }
                        else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                        {
                            pGeo = new PolylineClass();
                            pColl = pGeo as IPointCollection;
                            pColl.AddPointCollection(pPointColl);
                        }
                        else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                        {
                            pGeo = new PolygonClass();
                            pColl = pGeo as IPointCollection;
                            pColl.AddPointCollection(pPointColl);
                        }
                        m_MapControl.FlashShape(pGeo, 1, 100, Class.Common.GetDefaultSymbol(pGeo.GeometryType));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

        }
        #endregion

        #region 选择要素不同部分
        /// <summary>
        /// 选择要素不同部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPart.SelectedIndex > -1)
            {
                showGeoInfo(tvwFeature.FocusedNode.Tag as IFeature, cboPart.SelectedIndex);
            }

        }
        #endregion

        #region 保存编辑
        /// <summary>
        /// 保存属性信息编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gViewProInfo_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (tvwFeature.FocusedNode != null)
                {
                    int editFieldIndex;
                    IField pField;
                    IEnvelope pEnv = new EnvelopeClass();

                    if (tvwFeature.FocusedNode.ParentNode == null)//选择的是图层结点
                    {
                        IFeatureLayer pFLayer = tvwFeature.FocusedNode.Tag as IFeatureLayer;
                        m_pTableFields = pFLayer as ITableFields;

                        editFieldIndex = pFLayer.FeatureClass.Fields.FindFieldByAliasName(gViewProInfo.GetDataRow(e.RowHandle).ItemArray[0].ToString());
                        pField = m_pTableFields.get_Field(editFieldIndex);

                        ICursor pCur;
                        IFeature pFea;
                        IFeatureSelection pFeaSel = pFLayer as IFeatureSelection;
                        pFeaSel.SelectionSet.Search(null, false, out pCur);

                        m_WorkspaceEdit.StartEditOperation();

                        pFea = pCur.NextRow() as IFeature;
                        pEnv = pFea.Shape.Envelope;

                        while (pFea != null)
                        {
                            pEnv.Union(pFea.Shape.Envelope);

                            if (pField.Domain is ICodedValueDomain)
                            {
                                SaveFeatureProperty(pFea, editFieldIndex, (object)Class.Common.GetCodedValueDomainValue(pField.Domain as ICodedValueDomain, e.Value.ToString()));
                            }
                            else
                            {
                                SaveFeatureProperty(pFea, editFieldIndex, e.Value.ToString());
                            }
                            pFea = pCur.NextRow() as IFeature;
                        }
                        m_WorkspaceEdit.StopEditOperation();
                    }
                    else //选择的是要素结点
                    {
                        if (m_FeatureLayer != tvwFeature.FocusedNode.ParentNode.Tag as IFeatureLayer)
                        {
                            m_FeatureLayer = tvwFeature.FocusedNode.ParentNode.Tag as IFeatureLayer;
                        }
                        m_pTableFields = m_FeatureLayer as ITableFields;
                        m_Feature = tvwFeature.FocusedNode.Tag as IFeature;
                        pEnv = m_Feature.Shape.Envelope;

                        editFieldIndex = m_Feature.Fields.FindFieldByAliasName(gViewProInfo.GetDataRow(e.RowHandle).ItemArray[0].ToString());
                        pField = m_pTableFields.get_Field(editFieldIndex);

                        m_WorkspaceEdit.StartEditOperation();

                        if (pField.Domain is ICodedValueDomain)
                        {
                            SaveFeatureProperty(m_Feature, editFieldIndex, (object)Class.Common.GetCodedValueDomainValue(pField.Domain as ICodedValueDomain, e.Value.ToString()));
                        }
                        else
                        {
                            SaveFeatureProperty(m_Feature, editFieldIndex, e.Value.ToString());
                        }
                        m_WorkspaceEdit.StopEditOperation();
                    }

                    m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, pEnv);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }

        }

        /// <summary>
        /// 为域的字段值单元格加上下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gViewProInfo_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Value")
            {
                int editFieldIndex;
                IField pField;
                DevExpress.XtraEditors.Repository.RepositoryItemComboBox com = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

                //结合实际获取到下拉框中需要显示的项目
                DataTable dt=new DataTable();

                if (tvwFeature.FocusedNode.ParentNode == null)//选择的是图层结点
                {
                    IFeatureLayer pFLayer = tvwFeature.FocusedNode.Tag as IFeatureLayer;
                    m_pTableFields = pFLayer as ITableFields;

                    editFieldIndex = pFLayer.FeatureClass.Fields.FindFieldByAliasName(gViewProInfo.GetDataRow(e.RowHandle).ItemArray[0].ToString());
                    pField = m_pTableFields.get_Field(editFieldIndex);
                }
                else //选择的是要素结点
                {
                    if (m_FeatureLayer != tvwFeature.FocusedNode.ParentNode.Tag as IFeatureLayer)
                    {
                        m_FeatureLayer = tvwFeature.FocusedNode.ParentNode.Tag as IFeatureLayer;
                    }
                    m_pTableFields = m_FeatureLayer as ITableFields;
                    m_Feature = tvwFeature.FocusedNode.Tag as IFeature;

                    editFieldIndex = m_Feature.Fields.FindFieldByAliasName(gViewProInfo.GetDataRow(e.RowHandle).ItemArray[0].ToString());
                    pField = m_pTableFields.get_Field(editFieldIndex);
                }
                com.Items.Clear();
                if (pField.Domain is ICodedValueDomain)
                {
                    ICodedValueDomain domain = pField.Domain as ICodedValueDomain;
                    for(int i = 0; i < domain.CodeCount;i++)
                    {
                        com.Items.Add(domain.get_Name(i));
                    }
                    e.RepositoryItem = com;

                }
            }

        }


        /// <summary>
        /// 保存空间信息编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gGeoView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                m_WorkspaceEdit.StartEditOperation();
                //更新要素一个点的坐标
                PointClass newPoint = new PointClass();
                newPoint.ZAware = true;
                newPoint.MAware = true;
                newPoint.Y = double.Parse(gGeoView.GetDataRow(e.RowHandle).ItemArray[0].ToString());
                newPoint.X = double.Parse(gGeoView.GetDataRow(e.RowHandle).ItemArray[0].ToString());
                newPoint.Z = double.Parse(gGeoView.GetDataRow(e.RowHandle).ItemArray[0].ToString());
                newPoint.M = double.Parse(gGeoView.GetDataRow(e.RowHandle).ItemArray[0].ToString());

                SaveFeatureGeometry(cboPart.SelectedIndex, e.RowHandle, newPoint);
                m_WorkspaceEdit.StopEditOperation();
            }
            catch (Exception ex)
            {
                m_WorkspaceEdit.StopEditOperation();
                XtraMessageBox.Show("更新Geometry失败！");
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

        }
        #endregion

        #region 右键选择一个要素(树控件中的一个节点)，显示快捷菜单
        /// <summary>
        /// 右键选择一个要素(树控件中的一个节点)，显示快捷菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvwFeature_MouseUp(object sender, MouseEventArgs e)
        {
            TreeList tree = sender as TreeList;
            if (e.Button == MouseButtons.Right
                    && ModifierKeys == Keys.None
                    && tvwFeature.State == TreeListState.Regular)
            {
                System.Drawing.Point p = new System.Drawing.Point(MousePosition.X, MousePosition.Y);
                TreeListHitInfo hitInfo = tree.CalcHitInfo(e.Location);
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    tree.SetFocusedNode(hitInfo.Node);
                }

                if (tree.FocusedNode != null)
                {
                    pMenuPro.ShowPopup(p);
                }
            }
            else
            {
                if (tvwFeature.FocusedNode != null && tvwFeature.FocusedNode.ParentNode != null)
                {
                    FlashFeature(tvwFeature.FocusedNode.Tag as IFeature);
                }
            }
        }
        #endregion

        #region 显示几何信息编辑快捷菜单
        /// <summary>
        /// 显示几何信息编辑快捷菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gGeoView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowHandle != -1)
            {
                System.Drawing.Point p = new System.Drawing.Point(MousePosition.X, MousePosition.Y);
                pMenuGeo.ShowPopup(p);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                PointClass newPoint = new PointClass();
                newPoint.Y = double.Parse(gGeoView.GetDataRow(e.RowHandle).ItemArray[1].ToString());
                newPoint.X = double.Parse(gGeoView.GetDataRow(e.RowHandle).ItemArray[2].ToString());
                FlashNode(newPoint);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(newPoint);
            }
        }
        #endregion

        #region 右键菜单事件
        private void menuFlash_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_MapControl.FlashShape(m_Feature.Shape, 1, 100, Class.Common.GetDefaultSymbol(m_Feature.Shape.GeometryType));
        }


        private void menuLocate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IEnvelope pEnvelope = m_Feature.Extent;
            pEnvelope.Expand(1, 1, true);

            if (pEnvelope.XMin == pEnvelope.XMax && pEnvelope.YMax == pEnvelope.YMin)
            {
                IPoint tempPoint = new PointClass();
                tempPoint.X = pEnvelope.XMin;
                tempPoint.Y = pEnvelope.YMin;
                pEnvelope = Class.Common.NewRect(tempPoint, 10);
            }
            m_MapControl.Extent = pEnvelope;
        }

        private void menuCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_ClipboardFeature = m_Feature;

        }

        private void menuPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //把m_ClipboardFeature的属性复制到当前要素,OID,SHAPE,SHAPE_LENGTH,SHAPE_AREA等字段不复制
            try
            {
                m_WorkspaceEdit.StartEditOperation();
                for (int i = 0; i < m_FeatureLayer.FeatureClass.Fields.FieldCount; i++)
                {
                    IField pField = m_FeatureLayer.FeatureClass.Fields.get_Field(i);
                    if (pField.Editable == true &&
                        pField.Type != esriFieldType.esriFieldTypeOID &&
                        pField.Type != esriFieldType.esriFieldTypeGeometry &&
                        pField.Type != esriFieldType.esriFieldTypeBlob &&
                        pField.Type != esriFieldType.esriFieldTypeGlobalID &&
                        pField.Type != esriFieldType.esriFieldTypeGUID &&
                        pField.Type != esriFieldType.esriFieldTypeRaster)
                    {
                        m_Feature.set_Value(i, m_ClipboardFeature.get_Value(i));
                    }
                }
                m_WorkspaceEdit.StopEditOperation();
                tvwFeature_FocusedNodeChanged(null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                MessageBox.Show("粘贴属性发生错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void menuUnsel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IFeatureSelection pFeaSel = m_FeatureLayer as IFeatureSelection;
            int tmpOID = m_Feature.OID;
            pFeaSel.SelectionSet.RemoveList(1, ref tmpOID);
            //RefreshSelection();
            if (m_ActiveView.FocusMap.SelectionCount > 0)
            {
                m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            }
            else
            {
                m_ActiveView.Refresh();
            }
            if (this.tvwFeature.FocusedNode.ParentNode != null && this.tvwFeature.FocusedNode.ParentNode.Nodes.Count == 1)
            {
                this.tvwFeature.DeleteNode(this.tvwFeature.FocusedNode.ParentNode);
            }
            else
            {
                this.tvwFeature.DeleteNode(this.tvwFeature.FocusedNode);
            }
            if (this.tvwFeature.Nodes.Count > 0)
            {
                this.tvwFeature.FocusedNode = this.tvwFeature.Nodes[0].FirstNode;
            }
            else
            {
                this.btnPro.Visible = true;
                this.btnGeo.Visible = true;
                this.btnAnno.Visible = false;
                this.btnPro.Checked = true;

                cboPart.Properties.Items.Clear();
                this.pnlPro.Visible = true;
                this.pnlGeo.Visible = false;
                this.pnlAnno.Visible = false;

                this.uGridProInfo.DataSource = null;
                this.uGridGeoInfo.DataSource = null;

                //重置注记面板中所有控件的状态
                this.txtText.Text = "";
                this.cmbFontName.SelectedIndex = -1;
                this.cmbScale.SelectedIndex = -1;
                this.cmbSize.SelectedIndex = -1;
                this.btnColor.Color = Color.Empty;
                this.btnBold.Checked = false;
                this.btnItalic.Checked = false;
                this.btnUnder.Checked = false;
                this.rdoCenter.Checked = false;
                this.rdoFull.Checked = false;
                this.rdoLeft.Checked = false;
                this.rdoRight.Checked = false;
                this.degreeControl1.Angle = 0.0;
            }

        }

        private void menuDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_WorkspaceEdit.StartEditOperation();
            m_Feature.Delete();
            m_WorkspaceEdit.StopEditOperation();
            RefreshSelection();
            m_ActiveView.Refresh();
        }

        private void menuInsBefore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PointClass newPoint = new PointClass();
            newPoint.ZAware = true;
            newPoint.MAware = true;
            newPoint.Y = double.Parse(this.gGeoView.GetDataRow(this.gGeoView.FocusedRowHandle).ItemArray[1].ToString());
            newPoint.X = double.Parse(this.gGeoView.GetDataRow(this.gGeoView.FocusedRowHandle).ItemArray[2].ToString());
            newPoint.Z = double.Parse(this.gGeoView.GetDataRow(this.gGeoView.FocusedRowHandle).ItemArray[3].ToString());
            newPoint.M = double.Parse(this.gGeoView.GetDataRow(this.gGeoView.FocusedRowHandle).ItemArray[4].ToString());
            SaveFeatureGeometry(cboPart.SelectedIndex, this.gGeoView.FocusedRowHandle, newPoint);
            //删除一个部分
            IModifyGeometry pModGeo = null;
            switch (m_Feature.Shape.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    break;
                case esriGeometryType.esriGeometryMultipoint:
                    pModGeo = new ModifyMultiPoint();
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    pModGeo = new ModifyPolyline();
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    pModGeo = new ModifyPolygon();
                    break;
            }
            m_WorkspaceEdit.StartEditOperation();
            pModGeo.Feature = m_Feature;
            pModGeo.InsertPoint(cboPart.SelectedIndex, this.gGeoView.FocusedRowHandle, newPoint);
            m_WorkspaceEdit.StopEditOperation();
            m_ActiveView.Refresh();
            tvwFeature_FocusedNodeChanged(null, null);

        }

        private void menuInsAfter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PointClass newPoint = new PointClass();
            newPoint.ZAware = true;
            newPoint.MAware = true;
            newPoint.Y = double.Parse(this.gGeoView.GetDataRow(this.gGeoView.FocusedRowHandle).ItemArray[1].ToString());
            newPoint.X = double.Parse(this.gGeoView.GetDataRow(this.gGeoView.FocusedRowHandle).ItemArray[2].ToString());
            newPoint.Z = double.Parse(this.gGeoView.GetDataRow(this.gGeoView.FocusedRowHandle).ItemArray[3].ToString());
            newPoint.M = double.Parse(this.gGeoView.GetDataRow(this.gGeoView.FocusedRowHandle).ItemArray[4].ToString());
            SaveFeatureGeometry(cboPart.SelectedIndex, this.gGeoView.FocusedRowHandle, newPoint);

            IModifyGeometry pModGeo = null;
            switch (m_Feature.Shape.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    break;
                case esriGeometryType.esriGeometryMultipoint:
                    pModGeo = new ModifyMultiPoint();
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    pModGeo = new ModifyPolyline();
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    pModGeo = new ModifyPolygon();
                    break;
            }
            m_WorkspaceEdit.StartEditOperation();
            pModGeo.Feature = m_Feature;
            pModGeo.InsertPoint(cboPart.SelectedIndex, this.gGeoView.FocusedRowHandle + 1, newPoint);
            m_WorkspaceEdit.StopEditOperation();
            m_ActiveView.Refresh();
            tvwFeature_FocusedNodeChanged(null, null);

        }

        private void menuDelNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //删除一个部分
            IModifyGeometry pModGeo = null;
            switch (m_Feature.Shape.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    break;
                case esriGeometryType.esriGeometryMultipoint:
                    pModGeo = new ModifyMultiPoint();
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    pModGeo = new ModifyPolyline();
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    pModGeo = new ModifyPolygon();
                    break;
            }
            m_WorkspaceEdit.StartEditOperation();
            pModGeo.Feature = m_Feature;
            pModGeo.RemovePoint(cboPart.SelectedIndex, this.gGeoView.FocusedRowHandle);
            m_WorkspaceEdit.StopEditOperation();
            m_ActiveView.Refresh();
            tvwFeature_FocusedNodeChanged(null, null);

        }

        #endregion

        #region 显示当前选中要素,并闪烁选中的Part
        /// <summary>
        /// 显示当前选中要素,并闪烁选中的Part
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZoom_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (m_Feature == null || cboPart.SelectedIndex == -1) return;
                if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    m_MapControl.FlashShape(m_Feature.Shape, 1, 100, Class.Common.GetDefaultSymbol(esriGeometryType.esriGeometryPoint));
                }
                else
                {
                    IGeometryCollection pGeoColl = m_Feature.Shape as IGeometryCollection;
                    IGeometry pGeo = null;
                    IPointCollection pColl;
                    if (pGeoColl != null)
                    {
                        IPointCollection pPointColl = pGeoColl.get_Geometry(cboPart.SelectedIndex) as IPointCollection;
                        if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryMultipoint)
                        {
                            pGeo = new PointClass();
                            pColl = pGeo as IPointCollection;
                            pColl.AddPointCollection(pPointColl);
                        }
                        else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                        {
                            pGeo = new PolylineClass();
                            pColl = pGeo as IPointCollection;
                            pColl.AddPointCollection(pPointColl);
                        }
                        else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                        {
                            pGeo = new PolygonClass();
                            pColl = pGeo as IPointCollection;
                            pColl.AddPointCollection(pPointColl);
                        }
                        m_MapControl.FlashShape(pGeo, 1, 100, Class.Common.GetDefaultSymbol(pGeo.GeometryType));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        #endregion

        #region 删除当前选中要素的选中Part
        /// <summary>
        /// 删除当前选中要素的选中Part
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            //删除一个部分
            if (m_Feature == null) return;
            IModifyGeometry pModGeo = null;
            switch (m_Feature.Shape.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    break;
                case esriGeometryType.esriGeometryMultipoint:
                    pModGeo = new ModifyMultiPoint();
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    pModGeo = new ModifyPolyline();
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    pModGeo = new ModifyPolygon();
                    break;
            }
            m_WorkspaceEdit.StartEditOperation();
            pModGeo.Feature = m_Feature;
            pModGeo.RemovePart(cboPart.SelectedIndex);
            m_WorkspaceEdit.StopEditOperation();
            m_ActiveView.Refresh();
            tvwFeature_FocusedNodeChanged(null, null);
        }
        #endregion

        ////////////////////////////////////////////注记信息面板变量\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        #region 注记编辑面板变量、方法、事件
        /// <summary>
        /// 类内部的数据结构，用于定义字体样式
        /// </summary>
        public struct TextStyle
        {
            public string m_pText;			//标注的文字
            public string m_pFontName;		//标注的字体
            public string m_pSize;			//字体的大小
            public IRgbColor m_pColor;			//文字的颜色
            public esriTextHorizontalAlignment m_pHAlign;	//水平标注方式
            public bool m_isBold;			//是否黑字
            public bool m_isItalic;			//是否倾斜
            public bool m_isUnder;			//是否有下划线
            public double m_pAngle;			//倾斜角度					
        }

        /// <summary>
        /// 当前的字体样式
        /// </summary>
        private TextStyle m_pStyle = new TextStyle();
        /// <summary>
        /// 原来的字体样式
        /// </summary>
        private TextStyle o_pStyle = new TextStyle();

        /// <summary>
        /// 当前的字体元素
        /// </summary>
        private ITextElement m_pTextElement;
        /// <summary>
        /// 获取和设置当前字体元素的属性
        /// </summary>
        public ITextElement TextElementIO
        {
            get { return m_pTextElement; }
            set { m_pTextElement = value; }
        }

        /// <summary>
        /// 字体预览中，字体显示的比例，声明为静态变量来保存当前比例
        /// </summary>
        static private double m_pScale = 100;

        //////////////////////////////////////////////////////////////////////////

        ////////////////////////////注记信息面板事件、函数（增加 by 罗璇 2008-05-23）\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        #region 显示要素类中所有选中注记的信息
        /// <summary>
        /// 显示要素类中所有选中注记的信息
        /// </summary>
        /// <param name="pFLayer"></param>
        private void showAnnoInfo(IFeatureLayer pFLayer)
        {
            m_pStyle = getAnnoCommonVal(pFLayer);

            //还原用的结构
            o_pStyle = m_pStyle;
            if (m_pStyle.m_pColor != null)
            {
                IRgbColor pColor = new RgbColorClass();
                pColor.Red = m_pStyle.m_pColor.Red;
                pColor.Green = m_pStyle.m_pColor.Green;
                pColor.Blue = m_pStyle.m_pColor.Blue;
                o_pStyle.m_pColor = pColor;
            }
            else
            {
                o_pStyle.m_pColor = null;
            }

            RefreshForm(m_pStyle);
        }

        /// <summary>
        /// 得到要素类中所有选中注记的公共属性值
        /// </summary>
        /// <param name="pAnnoLayer"></param>
        private TextStyle getAnnoCommonVal(IFeatureLayer pFLayer)
        {
            /////////////////字体样式定义\\\\\\\\\\\\\\\\\\\

            TextStyle textStyle = new TextStyle();
            string strText = "";
            string strTempText = "";
            string strFontName = "";
            string strTempFontName = "";
            string strSize = "";
            string strTempSize = "";
            IRgbColor color;
            IRgbColor tempColor;
            esriTextHorizontalAlignment textAlignment;
            esriTextHorizontalAlignment tempTextAlignment;
            bool textAlign = true;
            bool isBold;
            bool tempIsBold;
            bool isItalic;
            bool tempIsItalic;
            bool isUnder;
            bool tempIsUnder;
            double angle;
            double tempAngle;

            ////////////////////////////////////////////////////

            ICursor pCur;
            IFeature pFea;
            IFeatureSelection pFeaSel = pFLayer as IFeatureSelection;
            pFeaSel.SelectionSet.Search(null, false, out pCur);

            pFea = pCur.NextRow() as IFeature;
            if (pFea == null) return textStyle;

            ITextElement textElement;
            textElement = (pFea as IAnnotationFeature).Annotation as ITextElement;

            strText = textElement.Text;
            strFontName = textElement.Symbol.Font.Name;
            strSize = textElement.Symbol.Size.ToString();
            color = textElement.Symbol.Color as IRgbColor;
            textAlignment = textElement.Symbol.HorizontalAlignment;
            isBold = textElement.Symbol.Font.Bold;
            isItalic = textElement.Symbol.Font.Italic;
            isUnder = textElement.Symbol.Font.Underline;
            angle = textElement.Symbol.Angle;

            pFea = pCur.NextRow() as IFeature;
            while (pFea != null)
            {
                textElement = (pFea as IAnnotationFeature).Annotation as ITextElement;

                strTempText = textElement.Text;
                strTempFontName = textElement.Symbol.Font.Name;
                strTempSize = textElement.Symbol.Size.ToString();
                tempColor = textElement.Symbol.Color as IRgbColor;
                tempTextAlignment = textElement.Symbol.HorizontalAlignment;
                tempIsBold = textElement.Symbol.Font.Bold;
                tempIsItalic = textElement.Symbol.Font.Italic;
                tempIsUnder = textElement.Symbol.Font.Underline;
                tempAngle = textElement.Symbol.Angle;

                if (strTempText != strText)
                {
                    strText = "";
                }
                if (strTempFontName != strFontName)
                {
                    strFontName = "";
                }
                if (strTempSize != strSize)
                {
                    strSize = "";
                }
                if (tempColor.RGB != color.RGB)
                {
                    color = null;
                }
                if (tempTextAlignment != textAlignment)
                {
                    textAlign = false;
                }
                if (tempIsBold != isBold)
                {
                    isBold = false;
                }
                if (tempIsItalic != isItalic)
                {
                    isItalic = false;
                }
                if (tempIsUnder != isUnder)
                {
                    isUnder = false;
                }
                if (tempAngle != angle)
                {
                    angle = double.NaN;
                }
                pFea = pCur.NextRow() as IFeature;
            }

            textStyle.m_pText = strText;
            textStyle.m_pFontName = strFontName;
            textStyle.m_pSize = strSize;
            textStyle.m_pColor = color;
            textStyle.m_pHAlign = textAlignment;
            textStyle.m_isBold = isBold;
            textStyle.m_isItalic = isItalic;
            textStyle.m_isUnder = isUnder;
            textStyle.m_pAngle = angle;

            return textStyle;
        }
        #endregion

        #region 显示一个注记的所有信息
        /// <summary>
        /// 显示一个注记的所有信息
        /// </summary>
        /// <param name="pFeature"></param>
        private void showAnnoInfo(IFeature pFeature)
        {
            if (pFeature == null) return;
            showProInfo(pFeature);
            getAnnoInfo(pFeature);
        }
        #endregion

        #region 显示注记信息
        /// <summary>
        /// 显示注记信息
        /// </summary>
        private void getAnnoInfo(IFeature pFeature)
        {
            if (pFeature == null) return;

            ITextElement tElement;
            tElement = (pFeature as IAnnotationFeature).Annotation as ITextElement;

            m_pStyle.m_pText = tElement.Text;
            m_pStyle.m_pFontName = tElement.Symbol.Font.Name;
            m_pStyle.m_pSize = tElement.Symbol.Size.ToString();
            m_pStyle.m_pColor = new RgbColorClass();
            m_pStyle.m_pColor = (IRgbColor)tElement.Symbol.Color;
            m_pStyle.m_pHAlign = tElement.Symbol.HorizontalAlignment;
            m_pStyle.m_isBold = tElement.Symbol.Font.Bold;
            m_pStyle.m_isItalic = tElement.Symbol.Font.Italic;
            m_pStyle.m_isUnder = tElement.Symbol.Font.Underline;
            m_pStyle.m_pAngle = tElement.Symbol.Angle;

            //还原用的结构
            o_pStyle = m_pStyle;
            o_pStyle.m_pColor = new RgbColorClass();
            o_pStyle.m_pColor = (IRgbColor)tElement.Symbol.Color;

            this.RefreshForm(m_pStyle);
        }
        #endregion

        #region 刷新注记信息面板内容
        /// <summary>
        /// 刷新窗体内容
        /// </summary>
        /// <param name="tStyle">用于刷新窗体内容的内部数据结构</param>
        private void RefreshForm(TextStyle tStyle)
        {
            //设置文本框的内容
            txtText.Text = tStyle.m_pText;

            //设置颜色
            if (tStyle.m_pColor != null)
            {
                Color dispColor = new Color();
                dispColor = Color.FromArgb(tStyle.m_pColor.Red, tStyle.m_pColor.Green, tStyle.m_pColor.Blue);
                btnColor.Color = dispColor;
                txtText.ForeColor = dispColor;
            }
            else
            {
                btnColor.Color = Color.Empty;
                btnColor.Refresh();
            }

            //设置字体、大小、角度
            if (tStyle.m_pFontName != "")
            {
                cmbFontName.Text = tStyle.m_pFontName;
            }
            else
            {
                cmbFontName.SelectedIndex = -1;
            }
            cmbSize.Text = tStyle.m_pSize;
            this.degreeControl1.Angle = tStyle.m_pAngle;

            //设置字体样式、对齐方式
            this.setStyle(tStyle);
            this.setAlignment(tStyle);

            //设置显示比例尺
            cmbScale.Text = m_pScale.ToString();
        }
        #endregion

        #region 设置字体的样式，包括是否加粗、是否倾斜、是否加下划线等
        /// <summary>
        /// 设置字体的样式，包括是否加粗、是否倾斜、是否加下划线等
        /// </summary>
        /// <param name="tStyle">用于刷新窗体内容的内部数据结构</param>
        private void setStyle(TextStyle tStyle)
        {
            if (tStyle.m_isBold)
            {
                txtText.Font = new Font(txtText.Font, txtText.Font.Style | FontStyle.Bold);
                btnBold.Checked = true;
            }
            else
            {
                txtText.Font = new Font(txtText.Font, txtText.Font.Style & ~FontStyle.Bold);
                btnBold.Checked = false;
            }

            if (tStyle.m_isItalic)
            {
                txtText.Font = new Font(txtText.Font, txtText.Font.Style | FontStyle.Italic);
                btnItalic.Checked = true;
            }
            else
            {
                txtText.Font = new Font(txtText.Font, txtText.Font.Style & ~FontStyle.Italic);
                btnItalic.Checked = false;
            }

            if (tStyle.m_isUnder)
            {
                txtText.Font = new Font(txtText.Font, txtText.Font.Style | FontStyle.Underline);
                btnUnder.Checked = true;
            }
            else
            {
                txtText.Font = new Font(txtText.Font, txtText.Font.Style & ~FontStyle.Underline);
                btnUnder.Checked = false;
            }
        }
        #endregion

        #region 设置字体对齐方式
        /// <summary>
        /// 设置字体的对齐方式
        /// </summary>
        /// <param name="tStyle">用于刷新窗体内容的内部数据结构</param>
        private void setAlignment(TextStyle tStyle)
        {
            if (tStyle.m_pHAlign == esriTextHorizontalAlignment.esriTHACenter)
            {
                txtText.SelectionAlignment = HorizontalAlignment.Center;
                rdoCenter.Checked = true;
            }
            else if (tStyle.m_pHAlign == esriTextHorizontalAlignment.esriTHAFull)
            {
                txtText.SelectionAlignment = HorizontalAlignment.Center;
                rdoFull.Checked = true;
            }
            else if (tStyle.m_pHAlign == esriTextHorizontalAlignment.esriTHALeft)
            {
                txtText.SelectionAlignment = HorizontalAlignment.Left;
                rdoLeft.Checked = true;
            }
            else if (tStyle.m_pHAlign == esriTextHorizontalAlignment.esriTHARight)
            {
                txtText.SelectionAlignment = HorizontalAlignment.Right;
                rdoRight.Checked = true;
            }
        }
        #endregion

        #region 创建字体元素
        /// <summary>
        /// 创建字体元素
        /// </summary>
        /// <param name="tStyle">用于刷新窗体内容的内部数据结构</param>
        private ITextElement MakeTextElement(TextStyle tStyle)
        {
            decimal annoSize = decimal.Parse(tStyle.m_pSize);

            ITextElement pTextElement = new TextElementClass();

            pTextElement.Text = tStyle.m_pText;

            IFormattedTextSymbol myTextSym = new TextSymbolClass();
            stdole.IFontDisp myFont = (stdole.IFontDisp)new stdole.StdFontClass();
            myFont.Name = tStyle.m_pFontName;
            decimal dec = annoSize;
            myFont.Size = dec;
            myFont.Bold = tStyle.m_isBold;
            myFont.Italic = tStyle.m_isItalic;
            myFont.Underline = tStyle.m_isUnder;

            myTextSym.Font = myFont;
            myTextSym.HorizontalAlignment = tStyle.m_pHAlign;
            myTextSym.Angle = tStyle.m_pAngle;
            myTextSym.Color = tStyle.m_pColor;

            pTextElement.Symbol = myTextSym;
            pTextElement.Symbol.Size = (double)annoSize;

            ISymbolCollectionElement sce = (ISymbolCollectionElement)pTextElement;
            sce.Size = (double)annoSize;

            return pTextElement;
        }
        #endregion

        #region 验证用户输入的正确性
        /// <summary>
        /// 验证用户输入的正确性
        /// </summary>
        /// <returns>验证是否成功</returns>
        private bool ValidateText()
        {
            try
            {
                int i = (int)System.Double.Parse(this.cmbSize.Text);
                if (i <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                this.cmbSize.Focus();
                return false;
            }

            try
            {
                int i = System.Int32.Parse(this.cmbScale.Text);
                if (i <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                this.cmbScale.Focus();
                return false;
            }

            if (!this.degreeControl1.Envaluate())
            {
                this.degreeControl1.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region 控件事件
        /// <summary>
        /// 注记内容文本发生变化时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtText_TextChanged(object sender, EventArgs e)
        {
            m_pStyle.m_pText = this.txtText.Text;

            if (!this.btnApply.Enabled)
                this.btnApply.Enabled = true;
            if (!this.btnReset.Enabled)
                this.btnReset.Enabled = true;
        }

        /// <summary>
        /// 按下加粗、倾斜或者下划线按钮触发的事件
        /// </summary>
        private void btnStyle_CheckedChanged(object sender, System.EventArgs e)
        {
            m_pStyle.m_isBold = btnBold.Checked;
            m_pStyle.m_isItalic = btnItalic.Checked;
            m_pStyle.m_isUnder = btnUnder.Checked;

            this.setStyle(m_pStyle);
            txtText.Refresh();

            if (!this.btnApply.Enabled)
                this.btnApply.Enabled = true;
            if (!this.btnReset.Enabled)
                this.btnReset.Enabled = true;
        }

        /// <summary>
        /// 对齐方式按钮按下时文本发生变化
        /// </summary>
        private void rdoAlignment_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rdoLeft.Checked) m_pStyle.m_pHAlign = esriTextHorizontalAlignment.esriTHALeft;
            if (rdoCenter.Checked) m_pStyle.m_pHAlign = esriTextHorizontalAlignment.esriTHACenter;
            if (rdoRight.Checked) m_pStyle.m_pHAlign = esriTextHorizontalAlignment.esriTHARight;
            if (rdoFull.Checked) m_pStyle.m_pHAlign = esriTextHorizontalAlignment.esriTHAFull;

            this.setAlignment(m_pStyle);
            txtText.Refresh();

            if (!this.btnApply.Enabled)
                this.btnApply.Enabled = true;
            if (!this.btnReset.Enabled)
                this.btnReset.Enabled = true;
        }

        /// <summary>
        /// 颜色按钮改变后字体颜色发生变化
        /// </summary>
        private void btnColor_CenterColorChanged(object sender, EventArgs e)
        {
            if (m_pStyle.m_pColor == null)
            {
                m_pStyle.m_pColor = new RgbColorClass();
            }
            m_pStyle.m_pColor.Red = btnColor.Color.R;
            m_pStyle.m_pColor.Green = btnColor.Color.G;
            m_pStyle.m_pColor.Blue = btnColor.Color.B;

            txtText.ForeColor = btnColor.Color;

            if (!this.btnApply.Enabled)
                this.btnApply.Enabled = true;
            if (!this.btnReset.Enabled)
                this.btnReset.Enabled = true;
        }

        /// <summary>
        /// 字体改变后字体框内的字体发生变化
        /// </summary>
        private void cmbFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_pStyle.m_pFontName = cmbFontName.Text;
            if (m_pStyle.m_pFontName != "" || m_pStyle.m_pSize != "")
            {
                txtText.Font = new Font(m_pStyle.m_pFontName, float.Parse(m_pStyle.m_pSize));
            }
            txtText.Refresh();

            if (!this.btnApply.Enabled)
                this.btnApply.Enabled = true;
            if (!this.btnReset.Enabled)
                this.btnReset.Enabled = true;
        }

        /// <summary>
        /// 字体显示比例发生变化时触发的事件
        /// </summary>
        private void cmbScale_TextChanged(object sender, EventArgs e)
        {
            float sSize, sScale, newSize;

            try
            {
                sSize = float.Parse(cmbSize.Text);
                sScale = float.Parse(cmbScale.Text);
                newSize = sSize * sScale / 100;

                m_pScale = sScale;

                txtText.Font = new Font(m_pStyle.m_pFontName, newSize);

                this.setStyle(m_pStyle);
                this.setAlignment(m_pStyle);
                txtText.Refresh();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 字体大小发生变化时激活
        /// </summary>
        private void cmbSize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float sSize, sScale, newSize;
                sSize = float.Parse(cmbSize.Text);
                sScale = float.Parse(cmbScale.Text);
                newSize = sSize * sScale / 100;

                m_pStyle.m_pSize = newSize.ToString();

                txtText.Font = new Font(m_pStyle.m_pFontName, newSize);
                this.setStyle(m_pStyle);
                this.setAlignment(m_pStyle);
                txtText.Refresh();

                if (!this.btnApply.Enabled)
                    this.btnApply.Enabled = true;
                if (!this.btnReset.Enabled)
                    this.btnReset.Enabled = true;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 用户在显示比例和字体大小文本框中输入时，限制输入数字
        /// </summary>
        private void combol_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                bool hasDot = false;
                System.Windows.Forms.ComboBox cb = sender as System.Windows.Forms.ComboBox;
                string txt = cb.Text;
                if (txt.IndexOf(".") > 0) hasDot = true;

                if (e.KeyChar == 46 && hasDot)
                    e.Handled = true;

                if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == 45 || e.KeyChar == 46 || e.KeyChar == 8))
                {
                    e.Handled = true;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 激活角度控件时出发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void degreeControl1_Enter(object sender, EventArgs e)
        {
            try
            {
                m_pStyle.m_pAngle = this.degreeControl1.Angle;

                if (!this.btnApply.Enabled)
                    this.btnApply.Enabled = true;
                if (!this.btnReset.Enabled)
                    this.btnReset.Enabled = true;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 鼠标离开“比例”下拉框时，进行数据验证
        /// </summary>
        private void cmbScale_Leave(object sender, EventArgs e)
        {
            try
            {
                float sSize, sScale, newSize;
                sSize = float.Parse(cmbSize.Text);
                sScale = float.Parse(cmbScale.Text);
                newSize = sSize * sScale / 100;

                m_pStyle.m_pSize = newSize.ToString();

                txtText.Font = new Font(m_pStyle.m_pFontName, newSize);
                this.setStyle(m_pStyle);
                this.setAlignment(m_pStyle);
                txtText.Refresh();
            }
            catch
            {
                this.cmbScale.Focus();
                MessageBox.Show("比例尺必须为大于0的数字，请重新输入！");
            }
        }

        /// <summary>
        /// 鼠标离开“字体大小”下拉框时，进行数据验证
        /// </summary>
        private void cmbSize_Leave(object sender, EventArgs e)
        {
            try
            {
                float sSize, sScale, newSize;
                sSize = float.Parse(cmbSize.Text);
                sScale = float.Parse(cmbScale.Text);
                newSize = sSize * sScale / 100;

                m_pStyle.m_pSize = newSize.ToString();

                txtText.Font = new Font(m_pStyle.m_pFontName, newSize);
                this.setStyle(m_pStyle);
                this.setAlignment(m_pStyle);
                txtText.Refresh();
            }
            catch
            {
                this.cmbSize.Focus();
                MessageBox.Show("字体大小必须为大于0的数字，请重新输入！");
            }
        }

        /// <summary>
        /// 点击“应用”按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!this.ValidateText())
            {
                return;
            }

            if (tvwFeature.FocusedNode != null)
            {
                if (tvwFeature.FocusedNode.ParentNode == null)//选择的是图层结点
                {
                    IFeatureLayer pFLayer = tvwFeature.FocusedNode.Tag as IFeatureLayer;

                    ICursor pCur;
                    IFeature pFea;
                    IFeatureSelection pFeaSel = pFLayer as IFeatureSelection;
                    pFeaSel.SelectionSet.Search(null, false, out pCur);
                    ITextElement pTextElement;
                    IElement pElement;
                    IEnvelope pEnv = new EnvelopeClass();

                    m_WorkspaceEdit.StartEditOperation();

                    pFea = pCur.NextRow() as IFeature;
                    pEnv = pFea.Shape.Envelope;

                    while (pFea != null)
                    {
                        pTextElement = (pFea as IAnnotationFeature).Annotation as ITextElement;

                        //注记内容
                        if (txtText.Text != "" && txtText.Text != pTextElement.Text)
                        {
                            m_pStyle.m_pText = txtText.Text;
                        }
                        else
                        {
                            m_pStyle.m_pText = pTextElement.Text;
                        }
                        //注记字体
                        if (pTextElement.Symbol.Font.Name != this.cmbFontName.Text)
                        {
                            m_pStyle.m_pFontName = this.cmbFontName.Text;
                        }
                        //注记大小
                        if (pTextElement.Symbol.Size != double.Parse(this.cmbSize.Text))
                        {
                            m_pStyle.m_pSize = this.cmbSize.Text;
                        }
                        else
                        {
                            m_pStyle.m_pSize = pTextElement.Symbol.Size.ToString();
                        }
                        //注记颜色
                        if (this.btnColor.Color != Color.Empty && pTextElement.Symbol.Color.RGB != this.btnColor.Color.ToArgb())
                        {
                            IRgbColor color = new RgbColorClass();
                            color.Red = this.btnColor.Color.R;
                            color.Green = this.btnColor.Color.G;
                            color.Blue = this.btnColor.Color.B;
                            m_pStyle.m_pColor = color;
                        }
                        else
                        {
                            m_pStyle.m_pColor = (IRgbColor)pTextElement.Symbol.Color;
                        }
                        //注记角度
                        if (pTextElement.Symbol.Angle != this.degreeControl1.Angle)
                        {
                            m_pStyle.m_pAngle = this.degreeControl1.Angle;
                        }
                        else
                        {
                            m_pStyle.m_pAngle = pTextElement.Symbol.Angle;
                        }
                        //注记字体样式
                        if (pTextElement.Symbol.Font.Bold != this.btnBold.Checked)
                        {
                            m_pStyle.m_isBold = this.btnBold.Checked;
                        }
                        else
                        {
                            m_pStyle.m_isBold = pTextElement.Symbol.Font.Bold;
                        }
                        if (pTextElement.Symbol.Font.Italic != this.btnItalic.Checked)
                        {
                            m_pStyle.m_isItalic = this.btnItalic.Checked;
                        }
                        else
                        {
                            m_pStyle.m_isItalic = pTextElement.Symbol.Font.Italic;
                        }
                        if (pTextElement.Symbol.Font.Underline != this.btnUnder.Checked)
                        {
                            m_pStyle.m_isUnder = this.btnUnder.Checked;
                        }
                        else
                        {
                            m_pStyle.m_isUnder = pTextElement.Symbol.Font.Underline;
                        }
                        //注记对齐方式
                        if (this.rdoLeft.Checked && pTextElement.Symbol.HorizontalAlignment != esriTextHorizontalAlignment.esriTHALeft)
                        {
                            m_pStyle.m_pHAlign = esriTextHorizontalAlignment.esriTHALeft;
                        }
                        else if (this.rdoRight.Checked && pTextElement.Symbol.HorizontalAlignment != esriTextHorizontalAlignment.esriTHARight)
                        {
                            m_pStyle.m_pHAlign = esriTextHorizontalAlignment.esriTHARight;
                        }
                        else if (this.rdoCenter.Checked && pTextElement.Symbol.HorizontalAlignment != esriTextHorizontalAlignment.esriTHACenter)
                        {
                            m_pStyle.m_pHAlign = esriTextHorizontalAlignment.esriTHACenter;
                        }
                        else if (this.rdoFull.Checked && pTextElement.Symbol.HorizontalAlignment != esriTextHorizontalAlignment.esriTHAFull)
                        {
                            m_pStyle.m_pHAlign = esriTextHorizontalAlignment.esriTHAFull;
                        }
                        else
                        {
                            m_pStyle.m_pHAlign = pTextElement.Symbol.HorizontalAlignment;
                        }

                        this.m_pTextElement = this.MakeTextElement(m_pStyle);

                        pElement = (IElement)m_pTextElement;
                        pElement.Geometry = (pFea as IAnnotationFeature).Annotation.Geometry;
                        (pFea as IAnnotationFeature).Annotation = pElement;
                        pEnv.Union(pFea.Shape.Envelope);

                        pFea.Store();
                        pFea = pCur.NextRow() as IFeature;
                    }
                    m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, pEnv);
                    m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, pEnv);

                    m_WorkspaceEdit.StopEditOperation();
                }
                else//选择的是要素结点
                {
                    if (m_FeatureLayer != tvwFeature.FocusedNode.ParentNode.Tag as IFeatureLayer)
                    {
                        m_FeatureLayer = tvwFeature.FocusedNode.ParentNode.Tag as IFeatureLayer;
                    }
                    m_Feature = tvwFeature.FocusedNode.Tag as IFeature;

                    m_pStyle.m_pText = txtText.Text;
                    m_pStyle.m_pSize = this.cmbSize.Text;
                    m_pStyle.m_pColor.Red = this.btnColor.Color.R;
                    m_pStyle.m_pColor.Green = this.btnColor.Color.G;
                    m_pStyle.m_pColor.Blue = this.btnColor.Color.B;
                    m_pStyle.m_pAngle = this.degreeControl1.Angle;
                    this.m_pTextElement = this.MakeTextElement(m_pStyle);

                    m_WorkspaceEdit.StartEditOperation();

                    IElement pElement = (IElement)m_pTextElement;
                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords((m_Feature as IAnnotationFeature).Annotation.Geometry.Envelope.XMin, (m_Feature as IAnnotationFeature).Annotation.Geometry.Envelope.YMin);
                    pElement.Geometry = pPoint;

                    (m_Feature as IAnnotationFeature).Annotation = pElement;
                    m_Feature.Store();

                    m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_Feature.Shape.Envelope);
                    m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, m_Feature.Shape.Envelope);

                    m_WorkspaceEdit.StopEditOperation();
                }
            }
            this.btnApply.Enabled = false;
            this.btnReset.Enabled = false;
        }

        /// <summary>
        /// 点击“重置”按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.RefreshForm(o_pStyle);

            this.btnApply.Enabled = false;
            this.btnReset.Enabled = false;
        }
        #endregion

        #endregion

    }
}
