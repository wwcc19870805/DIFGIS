using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList.Nodes;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DFWinForms.Class;
using DF2DTool.Class;
using ESRI.ArcGIS.esriSystem;
using DF2DControl.Base;
using DFCommon.Class;
using System.IO;

namespace DF2DTool.Frm
{
    public partial class FrmSpecialMap : XtraForm
    {
        IMap m_Map;
        DF2DApplication app = DF2DApplication.Application;
        DataTable _dt;
        Dictionary<string, IFeatureDataset> _dictDS;
        Dictionary<string, Color> _dicColors;
        FrmFilter frmFilter;
        private IColor m_ColorBack;
        private IColor m_BackFill;
        private IColor m_ColorMap;
        private IColor m_ColorOutline;
        private IColor m_ColorPipeline;
        private IColor m_ColorPipeUp;
        private IColor m_ColorPipeDown;

        private string SYMBOL_SIGN_FIELD_UPDOWN = "UpOrDown";
        private string SYMBOL_SIGN_FIELD_GEOOBJNUM = "GeoObjNum";
        private string SYMBOL_SIGN_FIELD_DIRCTION = "DIRCTION";
        int custom = 0;
        bool select;
        public FrmSpecialMap()
        {
            InitializeComponent();
        }
        public FrmSpecialMap(IMap map)
        {
            InitializeComponent();
            m_Map = map;
        }

        private void FrmSpecialMap_Load(object sender, EventArgs e)
        {
            InitControls();
        }
        //初始化控件
        private void InitControls()
        {
            try
            {
                this.lbx_BackMap.Items.Clear();
                this.cboMapName.Properties.Items.Clear();
                this._dictDS = new Dictionary<string, IFeatureDataset>();
                this._dicColors = new Dictionary<string, Color>();
                _dictDS.Clear();
                _dt = new DataTable();
                _dt.Columns.AddRange(new DataColumn[] { new DataColumn("Layers"), new DataColumn("Colors"), new DataColumn("Filter") });
                //this.gridControl1.DataSource = _dt;
                AddMapLayersToControls();

                this.rgChoose.SelectedIndex = 0;
                this.cboMapName.Enabled = true;
                this.btnSel.Enabled = false;
                this.btnSelAll.Enabled = false;
                this.btnUnSel.Enabled = false;
                this.btnUnSelAll.Enabled = false;
                this.ce_ColorOutline.Enabled = false;
                this.che_SetAllColor.Enabled = false;
            }
            catch (System.Exception ex)
            {
            	
            }
           
            
        }
        private void AddMapLayersToControls()
        {
            try
            {
                for (int i = 0; i < m_Map.LayerCount; i++)
                {
                    ILayer layer = m_Map.get_Layer(i);
                    if (layer.Visible)
                    {
                        AddLayerToList(layer);
                        AddLayerToComboBox(layer);
                    }
                }
                this.cboMapName.SelectedIndex = 0;
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }
        private void AddLayerToList(ILayer layer)
        {
            if (layer is IGroupLayer)
            {               
                ICompositeLayer comLayer = layer as ICompositeLayer;
                for (int i = 0; i < comLayer.Count;i++ )
                {
                    AddLayerToList(comLayer.get_Layer(i));
                }
            }
            else if (layer is IFeatureLayer && layer.Visible)
            {
                IFeatureClass fc = (layer as IFeatureLayer).FeatureClass;
                this.lbx_BackMap.Items.Add(fc.AliasName);
            }
        }
        private void AddLayerToComboBox(ILayer layer)
        {
            if (layer is IGroupLayer)
            {
                string strMapName = layer.Name;
                if (strMapName.EndsWith("_2D"))
                {
                    strMapName = strMapName.Remove(strMapName.IndexOf("_2D"), 3) + "专题图";
                }
                else if (strMapName.IndexOf("_") > -1)
                {
                    strMapName = strMapName.Substring(strMapName.IndexOf("_") + 1) + "专题图";
                }
                this.cboMapName.Properties.Items.Add(strMapName);
                ICompositeLayer comlayer = layer as ICompositeLayer;
                for (int i = 0; i < comlayer.Count; i++)
                {
                    AddLayerToComboBox(comlayer.get_Layer(i));  
                }
            }
            if (layer is IFeatureLayer && layer.Visible)
            {
                IFeatureLayer fl = layer as IFeatureLayer;
                IFeatureDataset fDs = fl.FeatureClass.FeatureDataset;
                if (_dictDS.ContainsValue(fDs)) return;
                IEnumDataset eNumDS = fDs.Subsets;
                IDataset pDS = eNumDS.Next();
                IFeatureClass fc = pDS as IFeatureClass;

                string strMapName = fc.AliasName;
                if (strMapName.EndsWith("_2D"))
                {
                    strMapName = strMapName.Remove(strMapName.IndexOf("_2D"), 3) + "专题图";
                }
                else if (strMapName.IndexOf("_") > -1)
                {
                    strMapName = strMapName.Substring(0,strMapName.IndexOf("_") + 1) + "专题图";
                }
                if (this._dictDS.ContainsKey(strMapName)) return;
                this._dictDS[strMapName] = fDs;
                this.cboMapName.Properties.Items.Add(strMapName);
                this.cboMapName.SelectedIndex = 0;
                

            }
        }

        private void cboMapName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                custom = 0;
                if (this._dt == null) return;
                _dt.Rows.Clear();
                this.rgGrnBelow.Enabled = true;
                this.rgGrnBelow.SelectedIndex = 0;
                if (this._dictDS == null || this._dictDS.Count == 0) return;
                if (_dictDS.ContainsKey(this.cboMapName.SelectedItem.ToString()))
                {
                    IDataset ds;
                    IFeatureDataset fDS = _dictDS[this.cboMapName.SelectedItem.ToString()];
                    IEnumDataset eNumDS = fDS.Subsets;
                    if (eNumDS == null) return;
                    while ((ds = eNumDS.Next()) != null)
                    {
                        IFeatureClass fc = ds as IFeatureClass;
                        AddRowToDataTable(fc);
                    }
                    this.gridControl1.DataSource = _dt;
                    this.ce_ColorOutline.Enabled = true;
                    this.che_SetAllColor.Enabled = true;
                }
            
            }
            catch (System.Exception ex)
            {
            	
            }
                      
        }

        private void AddRowToDataTable(IFeatureClass fc)
        {
            if (this._dt == null) return;
            DataRow dr = _dt.NewRow();
            dr["Layers"] = fc.AliasName;
            _dt.Rows.Add(dr);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (custom == 2) return;

                if (e.Column.FieldName == "Colors")
                {
                    DataRow row = gridView1.GetDataRow(e.RowHandle);
                    string fcName = row["Layers"].ToString();
                    if (custom == 1)
                    {
                        if (_dicColors.ContainsKey(fcName))
                        {
                            e.Appearance.BackColor = _dicColors[fcName];
                        }
                    }
                    else if (custom == 0)
                    {
                        ILayer layer = GetLayerByName(fcName);
                        if (layer == null) return;
                        IColor color = GetAnnoColorByLayer(layer);
                        if (color == null) return;
                        if (_dicColors.ContainsKey(fcName))
                        {
                            _dicColors.Remove(fcName);
                            _dicColors[fcName] = System.Drawing.ColorTranslator.FromWin32(color.RGB);
                        }
                        else
                        {
                            _dicColors[fcName] = System.Drawing.ColorTranslator.FromWin32(color.RGB);
                        }
                        e.Appearance.BackColor = _dicColors[fcName];
                    }
                  
                }

             
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }
        private ILayer GetLayerByName(string fcName)
        {
            ILayer layer = null;
            if (this.m_Map == null) return null;
            for (int i = 0; i < m_Map.LayerCount; i++)
            {
                layer = m_Map.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    IFeatureLayer featurelayer = layer as IFeatureLayer;
                    if (featurelayer.FeatureClass.AliasName == fcName)
                    {
                        return layer;
                    }
                }
            }
            return null;

        }
        private IFeatureClass GetFeatureClassByName(string fcName)
        {
            ILayer layer;
            if (this.m_Map == null) return null;
            for (int i = 0; i < m_Map.LayerCount; i++)
            {
                layer = m_Map.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    IFeatureLayer featurelayer = layer as IFeatureLayer;
                    if (featurelayer.FeatureClass.AliasName == fcName)
                    {
                        return featurelayer.FeatureClass;
                    }
                }
            }
            return null;
        }
        private IColor GetAnnoColorByLayer(ILayer layer)
        {
            try
            {
                IColor annoColor = null;
                IFeatureClass fc = (layer as IFeatureLayer).FeatureClass;
                if (fc == null) return annoColor;          
                //获得要素类所有符号
                if (fc.FeatureType != esriFeatureType.esriFTAnnotation)
                {
                    IGeoFeatureLayer geoFL = layer as IGeoFeatureLayer;
                    if (geoFL == null) return annoColor;
                    if (geoFL.Renderer is IUniqueValueRenderer)
                    {
                        IUniqueValueRenderer pUVR = geoFL.Renderer as IUniqueValueRenderer;
                        if (pUVR == null) return null;
                        switch (fc.ShapeType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                                annoColor = (pUVR.get_Symbol(pUVR.get_Value(0)) as IMarkerSymbol).Color;
                                break;
                            case esriGeometryType.esriGeometryPolyline:
                                annoColor = (pUVR.get_Symbol(pUVR.get_Value(0)) as ILineSymbol).Color;
                                break;
                            case esriGeometryType.esriGeometryPolygon:
                                annoColor = (pUVR.get_Symbol(pUVR.get_Value(0)) as IFillSymbol).Color;
                                break;
                        }
                        return annoColor;
                    }
                    else if (geoFL.Renderer is ISimpleRenderer)
                    {
                        ISimpleRenderer pSR = geoFL.Renderer as ISimpleRenderer;
                        switch (fc.ShapeType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                                annoColor = (pSR.Symbol as IMarkerSymbol).Color;
                                break;
                            case esriGeometryType.esriGeometryPolyline:
                                annoColor = (pSR.Symbol as ILineSymbol).Color;
                                break;
                            case esriGeometryType.esriGeometryPolygon:
                                annoColor = (pSR.Symbol as IFillSymbol).Color;
                                break;
                        }
                        return annoColor;
                    }
                }
                else//若为注记层
                {
                    IFeatureCursor cursor = fc.Search(null, true);
                    if (cursor == null) return annoColor;
                    ITextElement textElement = (cursor.NextFeature() as IAnnotationFeature).Annotation as ITextElement;
                    annoColor = textElement.Symbol.Color;
                    return annoColor;

                }
                return annoColor;

            }
            catch
            {
                return null;
            }
        }

        private void rgChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgChoose.SelectedIndex == 0)
            {
                if (_dt != null)
                {
                    this._dt.Rows.Clear();
                    this._dicColors.Clear();
                    this.gridControl1.DataSource = _dt;
                }
                this.cboMapName.Enabled = true;
                this.lbx_BackMap.Enabled = false;
                this.btnSel.Enabled = false;
                this.btnUnSel.Enabled = false;
                this.btnSelAll.Enabled = false;
                this.btnUnSelAll.Enabled = false;
                this.rgGrnBelow.Enabled = true;
                this.rgGrnBelow.SelectedIndex = 0;
            }
            else
            {
                if (_dt != null)
                {
                    this._dt.Rows.Clear();
                    this._dicColors.Clear();
                    this.gridControl1.DataSource = _dt;
                }
                this.cboMapName.Enabled = false;
                this.lbx_BackMap.Enabled = true;
                this.btnSel.Enabled = true;
                this.btnUnSel.Enabled = true;
                this.btnSelAll.Enabled = true;
                this.btnUnSelAll.Enabled = true;
                this.rgGrnBelow.Enabled = true;
                this.rgGrnBelow.SelectedIndex = 0;
            }
        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._dt == null) return;
                this._dt.Rows.Clear();
                foreach (object obj in this.lbx_BackMap.Items)
                {
                    string strMapName = obj.ToString();
                    IFeatureClass fc = GetFeatureClassByName(strMapName);
                    AddRowToDataTable(fc);
                }

                this.gridControl1.DataSource = _dt;
                this.lbx_BackMap.Items.Clear();
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }

        private void btnUnSelAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._dt == null || this.m_Map == null) return;
                this._dt.Rows.Clear();
                this.gridControl1.DataSource = _dt;
                for (int i = 0; i < m_Map.LayerCount; i++)
                {
                    ILayer layer = m_Map.get_Layer(i);
                    AddLayerToList(layer);
                }
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            try
            {
                custom = 0;
                if (this._dt == null) return;
                if (this._dicColors == null) return;
                string strMapName = this.lbx_BackMap.SelectedItem.ToString();
                this.lbx_BackMap.Items.Remove(this.lbx_BackMap.SelectedItem);
                IFeatureClass fc = GetFeatureClassByName(strMapName);
                AddRowToDataTable(fc);
                //_dicColors[strMapName] = ce_BackFill.Color;
                this.gridControl1.DataSource = _dt;
            }
            catch (System.Exception ex)
            {
            	
            }            
        }

        private void btnUnSel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._dt == null) return;
                DataRow row = gridView1.GetFocusedDataRow();
                string strMapName = row["Layers"].ToString();
                if (!IsMapNameExist(strMapName)) this.lbx_BackMap.Items.Add(strMapName);
                _dt.Rows.Remove(row);
                if (_dicColors.ContainsKey(strMapName)) _dicColors.Remove(strMapName);
                this.gridControl1.DataSource = _dt;
               
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        private bool IsMapNameExist(string mapName)
        {
            foreach (object obj in this.lbx_BackMap.Items)
            {
                if (mapName == obj.ToString()) return true;
            }
            return false;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void ce_BackFill_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_dicColors == null) return;
                custom = 1;
                string strMapName = gridView1.GetFocusedRowCellValue("Layers").ToString();
                if (_dicColors.ContainsKey(strMapName))
                {
                    _dicColors.Remove(strMapName);
                    _dicColors[strMapName] = ce_BackFill.Color;
                }
                else
                {
                    _dicColors[strMapName] = ce_BackFill.Color;
                }
 
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }

        private void che_SetAllColor_CheckedChanged(object sender, EventArgs e)
        {
            if (this.che_SetAllColor.Checked) this.ce_AllColor.Enabled = true;
            else this.ce_AllColor.Enabled = false;
        }

        private void ce_AllColor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                custom = 3;
                if (this._dt == null) return;
                else
                {
                    GridColumn column = this.gridView1.Columns.ColumnByName("Colors");
                    column.AppearanceCell.BackColor = ce_AllColor.Color;
                }
            }
            catch (System.Exception ex)
            {
            	
            }
          
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName ==  "Filter")
                {
                    string strMapName = gridView1.GetDataRow(e.RowHandle)["Layers"].ToString();
                    ILayer layer = GetLayerByName(strMapName);
                    if (layer == null) return;
                    this.frmFilter = new FrmFilter(layer);
                    if (frmFilter.ShowDialog() == DialogResult.OK)
                    {
                        DataRow row = gridView1.GetDataRow(e.RowHandle);
                        row["Filter"] = frmFilter.Filter;
                        this.rgGrnBelow.Enabled = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("专题图栏不能为空,请重新选择");
                    return;
                }
                ILayer pLayer;
                string strFilter = "";
                WaitForm.Start("开始制作专题图,请稍后...");
                m_BackFill = CommonFunction.GetRGBColor(ce_BackFill.Color.R, ce_BackFill.Color.G, ce_BackFill.Color.B);
                m_ColorBack = CommonFunction.GetRGBColor(ce_ColorBack.Color.R, ce_ColorBack.Color.G, ce_ColorBack.Color.B);
                foreach (object obj in lbx_BackMap.Items)
                {
                    string item = obj.ToString();
                    pLayer = GetLayerByName(item);
                    if (pLayer == null) continue;
                    WaitForm.SetCaption("正在渲染底图图层" + pLayer.Name + "请稍后...");
                    BackLayerRender(pLayer as IFeatureLayer, m_ColorBack, m_BackFill);
                }
                foreach (DataRow dr in _dt.Rows)
                {
                    string layerName = dr["Layers"].ToString();
                    if (!_dicColors.ContainsKey(layerName)) continue;
                    Color colorMap = _dicColors[layerName];
                    pLayer = GetLayerByName(layerName);
                    if (pLayer == null) continue;
                    WaitForm.SetCaption("正在渲染底图图层" + pLayer.Name + "请稍后...");
                    if (this.rgGrnBelow.Enabled)
                    {
                        if (this.rgGrnBelow.SelectedIndex == 1)
                        {
                            m_ColorPipeDown = CommonFunction.GetRGBColor(colorMap.R, colorMap.G, colorMap.B);
                            m_ColorPipeUp = CommonFunction.GetRGBColor(ce_Ground.Color.R, ce_Ground.Color.G, ce_Ground.Color.B);
                        }
                        if (this.rgGrnBelow.SelectedIndex == 0)
                        {
                            m_ColorPipeUp = CommonFunction.GetRGBColor(colorMap.R, colorMap.G, colorMap.B);
                            m_ColorPipeDown = CommonFunction.GetRGBColor(ce_Below.Color.R, ce_Below.Color.G, ce_Below.Color.B);
                        }
                        PipeLayerRender(pLayer as IFeatureLayer, m_ColorPipeUp, m_ColorPipeDown);
                    }
                    else
                    {
                        m_ColorMap = CommonFunction.GetRGBColor(colorMap.R, colorMap.G, colorMap.B);
                        m_ColorOutline = CommonFunction.GetRGBColor(ce_ColorOutline.Color.R, ce_ColorOutline.Color.G, ce_ColorOutline.Color.B);
                        strFilter = dr["Filter"].ToString();
                        if (strFilter != "")
                        {
                            SpecialLayerRender(pLayer as IFeatureLayer, m_ColorMap, m_ColorOutline, m_ColorBack, m_BackFill, strFilter);
                        }
                        else
                        {
                            SpecialLayerRender(pLayer as IFeatureLayer, m_ColorMap, m_ColorOutline);
                        }
                    }


                }
                app.Current2DMapControl.ActiveView.Refresh();
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
            }
           


        }
        private void BackLayerRender(IFeatureLayer featureLayer, IColor color, IColor colorPlyFill)
        {
            int i;
            IFeatureClass pFC = featureLayer.FeatureClass;
            IGeoFeatureLayer geoFL = featureLayer as IGeoFeatureLayer;
            if (pFC == null|| geoFL == null) return;
            if (color == null || colorPlyFill == null) return;
            if (pFC.FeatureType != esriFeatureType.esriFTAnnotation && pFC.FeatureType != esriFeatureType.esriFTCoverageAnnotation)
            {
                IMarkerSymbol markerSymbol = null;
                ILineSymbol lineSymbol = null;
                IFillSymbol fillSymbol = null;
                if (geoFL.Renderer is IUniqueValueRenderer)
                {
                    IUniqueValueRenderer pUniqueRender = geoFL.Renderer as IUniqueValueRenderer;
                    switch (pFC.ShapeType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                            for (i = 0; i < pUniqueRender.ValueCount; i++)
                            {
                                markerSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as IMarkerSymbol;
                                markerSymbol.Color = color;
                            }
                            if (pUniqueRender.UseDefaultSymbol)
                            {
                                markerSymbol = pUniqueRender.DefaultSymbol as IMarkerSymbol;
                                markerSymbol.Color = color;
                                
                            }
                            break;
                        case esriGeometryType.esriGeometryPolyline:
                            for ( i = 0; i < pUniqueRender.ValueCount;i++ )
                            {
                                lineSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as ILineSymbol;
                                lineSymbol.Color = color;
                            }
                            if (pUniqueRender.UseDefaultSymbol)
                            {
                                lineSymbol = pUniqueRender.DefaultSymbol as ILineSymbol;
                                lineSymbol.Color = color;
                            }
                            break;
                        case esriGeometryType.esriGeometryPolygon:
                            for (i = 0; i < pUniqueRender.ValueCount; i++)
                            {
                                fillSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as IFillSymbol;
                                fillSymbol.Color = colorPlyFill;
                                lineSymbol = fillSymbol.Outline;
                                lineSymbol.Color = color;
                                fillSymbol.Outline = lineSymbol;
                            }
                            if (pUniqueRender.UseDefaultSymbol)
                            {
                                fillSymbol = pUniqueRender.DefaultSymbol as IFillSymbol;
                                fillSymbol.Color = colorPlyFill;
                                lineSymbol = fillSymbol.Outline;
                                lineSymbol.Color = color;
                                fillSymbol.Outline = lineSymbol;
                            }
                            break;
                            
                    }

                }
                else if (geoFL.Renderer is ISimpleRenderer)
                {
                    ISimpleRenderer pSimpleRender = geoFL.Renderer as ISimpleRenderer;
                    switch (pFC.ShapeType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                            (pSimpleRender.Symbol as IMarkerSymbol).Color = color;
                            break;
                        case esriGeometryType.esriGeometryPolyline:
                            (pSimpleRender.Symbol as ILineSymbol).Color = color;
                            break;
                        case esriGeometryType.esriGeometryPolygon:
                            fillSymbol = pSimpleRender.Symbol as IFillSymbol;
                            fillSymbol.Color = colorPlyFill;
                            lineSymbol = fillSymbol.Outline;
                            lineSymbol.Color = color;
                            fillSymbol.Outline = lineSymbol;
                            break;
                    }
                }
            }
            else if (pFC.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
            {
                ICoverageAnnotationLayer pCAnnoLayer = pFC as ICoverageAnnotationLayer;
                for (i = 0; i < pCAnnoLayer.SymbolCount; i++)
                {
                    pCAnnoLayer.set_FontColor(pCAnnoLayer.get_SymbolNumber(i), color);
                }
            }
            else
            {
                (pFC as ISymbolSubstitution).SubstituteType = esriSymbolSubstituteType.esriSymbolSubstituteColor;
                (pFC as ISymbolSubstitution).MassColor = color;
            }
        }

        private void PipeLayerRender(IFeatureLayer featureLayer, IColor colorUp, IColor colorDown)
        {
            int i, j;
            int index;
            bool bUseUpDown = false;
            if (colorUp == null || colorDown == null) return;
            IGeoFeatureLayer geoFL = featureLayer as IGeoFeatureLayer;
            if (geoFL == null||featureLayer.FeatureClass == null ) return;
            
            if (featureLayer.FeatureClass.FeatureType != esriFeatureType.esriFTAnnotation)
            {
                IUniqueValueRenderer pUniqueRender_new = new UniqueValueRendererClass();
                if (geoFL.Renderer is IUniqueValueRenderer)
                {
                    index = featureLayer.FeatureClass.FindField(SYMBOL_SIGN_FIELD_UPDOWN);
                    if (index > -1)
                    {
                        string strValue = "";
                        string s = "";
                        IUniqueValueRenderer pUniqueRender = (featureLayer as IGeoFeatureLayer).Renderer as IUniqueValueRenderer;
                        for (j = 0; j < pUniqueRender.FieldCount; j++)
                        {
                            s = pUniqueRender.get_Field(j);
                            if (s == SYMBOL_SIGN_FIELD_UPDOWN.ToUpper())
                            {
                                bUseUpDown = true;
                                break;
                            }
                        }
                        //如果图层符号化方案采用了“地上/地下”字段，则直接改变颜色
                        if (bUseUpDown)
                        {
                            for (i = 0; i < pUniqueRender.ValueCount; i++)
                            {
                                ISymbol sym = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i));
                                if (pUniqueRender.get_Label(pUniqueRender.get_Value(i)).IndexOf("地下") > -1)
                                {
                                    switch (featureLayer.FeatureClass.ShapeType)
                                    {
                                        case esriGeometryType.esriGeometryPoint:
                                            (sym as IMarkerSymbol).Color = colorDown;
                                            break;
                                        case esriGeometryType.esriGeometryPolyline:
                                            (sym as ILineSymbol).Color = colorDown;
                                            break;
                                    }
                                }
                                else if (pUniqueRender.get_Label(pUniqueRender.get_Value(i)).IndexOf("地上") > -1)
                                {
                                    switch (featureLayer.FeatureClass.ShapeType)
                                    {
                                        case esriGeometryType.esriGeometryPoint:
                                            (sym as IMarkerSymbol).Color = colorUp;
                                            break;
                                        case esriGeometryType.esriGeometryPolyline:
                                            (sym as ILineSymbol).Color = colorUp;
                                            break;
                                    }
                                }
                            }
                        }
                        else//如果没用“地上/地下“字段，则重新定义符号化方案
                        {
                            if (pUniqueRender.FieldCount < 3)
                            {
                                pUniqueRender_new.FieldCount = pUniqueRender.FieldCount + 1;
                                for (i = 0; i < pUniqueRender.FieldCount; i++)
                                {
                                    pUniqueRender_new.set_Field(i, pUniqueRender.get_Field(i));
                                }
                                pUniqueRender_new.set_Field(i, SYMBOL_SIGN_FIELD_UPDOWN);
                                pUniqueRender_new.FieldDelimiter = "_";
                                for (i = 0; i < pUniqueRender.ValueCount; i++)
                                {
                                    strValue = pUniqueRender.get_Value(i);
                                    IClone pCloneSymbolDown = (pUniqueRender.get_Symbol(strValue) as IClone).Clone();
                                    IClone pCloneSymbolUp = (pUniqueRender.get_Symbol(strValue) as IClone).Clone();
                                    pUniqueRender_new.AddValue(strValue + "_2", pUniqueRender.get_Heading(strValue), pCloneSymbolDown as ISymbol);
                                    pUniqueRender_new.set_Label(strValue + "_2", pUniqueRender.get_Label(strValue) + "_地下");
                                    pUniqueRender_new.AddValue(strValue + "_1", pUniqueRender.get_Heading(strValue), pCloneSymbolUp as ISymbol);
                                    pUniqueRender_new.set_Label(strValue + "_1", pUniqueRender.get_Label(strValue) + "_地上");

                                    //根据地上/地下改变符号颜色
                                    switch (featureLayer.FeatureClass.ShapeType)
                                    {
                                        case esriGeometryType.esriGeometryPoint:
                                            (pCloneSymbolDown as IMarkerSymbol).Color = colorDown;
                                            (pCloneSymbolUp as IMarkerSymbol).Color = colorUp;
                                            (pUniqueRender_new as IRotationRenderer).RotationField = SYMBOL_SIGN_FIELD_DIRCTION;
                                            (pUniqueRender_new as IRotationRenderer).RotationType = esriSymbolRotationType.esriRotateSymbolGeographic;
                                            break;
                                        case esriGeometryType.esriGeometryPolyline:
                                            (pCloneSymbolDown as ILineSymbol).Color = colorDown;
                                            (pCloneSymbolUp as ILineSymbol).Color = colorUp;
                                            break;
                                    }
                                }
                                if (pUniqueRender.UseDefaultSymbol)
                                {
                                    pUniqueRender_new.DefaultSymbol = pUniqueRender.DefaultSymbol;
                                    pUniqueRender_new.DefaultLabel = pUniqueRender.DefaultLabel;
                                }
                                (featureLayer as IGeoFeatureLayer).Renderer = pUniqueRender_new as IFeatureRenderer;
                            }
                        }
                    }
                }
                else if (geoFL.Renderer is ISimpleRenderer)
                {
                    pUniqueRender_new.FieldCount = 1;
                    pUniqueRender_new.set_Field(0, SYMBOL_SIGN_FIELD_UPDOWN);
                    ISimpleRenderer pSimpleRender = (featureLayer as IGeoFeatureLayer).Renderer as ISimpleRenderer;

                    IClone pCloneSymbolDown = (pSimpleRender.Symbol as IClone).Clone();
                    IClone pCloneSymbolUp = (pSimpleRender.Symbol as IClone).Clone();
                    pUniqueRender_new.AddValue("2", "地下", pCloneSymbolDown as ISymbol);
                    pUniqueRender_new.AddValue("1", "地上", pCloneSymbolUp as ISymbol);

                    switch (featureLayer.FeatureClass.ShapeType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                            (pCloneSymbolDown as IMarkerSymbol).Color = colorDown;
                            (pCloneSymbolUp as IMarkerSymbol).Color = colorUp;
                            break;
                        case esriGeometryType.esriGeometryPolyline:
                            (pCloneSymbolDown as ILineSymbol).Color = colorDown;
                            (pCloneSymbolUp as ILineSymbol).Color = colorUp;
                            break;
                    }
                    (featureLayer as IGeoFeatureLayer).Renderer = pUniqueRender_new as IFeatureRenderer;
                }
            }
            else//如果为注记图层
            {
                ILayer currentEditLayer = featureLayer;
                IDataset pDataset = featureLayer.FeatureClass as IDataset;
                IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pDataset.Workspace;
                IWorkspaceEdit pWorkspaceEdit;
                pWorkspaceEdit = pDataset.Workspace as IWorkspaceEdit;
                pWorkspaceEdit.StartEditOperation();
                //设置地上管线颜色
                IQueryFilter pQFilter = new QueryFilter();
                pQFilter.WhereClause = SYMBOL_SIGN_FIELD_UPDOWN + "=1";
                IFeatureCursor pCur = featureLayer.FeatureClass.Update(pQFilter, true);
                IFeature pFea = pCur.NextFeature();

                IAnnotationFeature pAnnotationFeature = null;
                while (pFea != null)
                {
                    pAnnotationFeature = pFea as IAnnotationFeature;
                    this.UpdateColor(pAnnotationFeature, colorUp);
                    pFea = pCur.NextFeature();
                }

                //设置地上下管线颜色
                pQFilter.WhereClause = SYMBOL_SIGN_FIELD_UPDOWN + "=2";
                pCur = featureLayer.FeatureClass.Update(pQFilter, true);
                pFea = pCur.NextFeature();
                while (pFea != null)
                {
                    pAnnotationFeature = pFea as IAnnotationFeature;
                    this.UpdateColor(pAnnotationFeature, colorDown);
                    pFea = pCur.NextFeature();
                }
                pWorkspaceEdit.StopEditOperation();


            }
        }

        private void UpdateColor(IAnnotationFeature pAnnoFeature, IColor colorUp)
        {
            ITextElement ptxtElement = new TextElementClass();
            ITextElement annoElement = pAnnoFeature.Annotation as ITextElement;
            ptxtElement.Text = annoElement.Text;
            ITextSymbol ptxtSym = new TextSymbolClass();
            IRgbColor rgbColor = colorUp as IRgbColor;
            IRgbColor rgb = new RgbColorClass();
            rgb.Red = rgbColor.Red;
            rgb.Green = rgbColor.Green;
            rgb.Blue = rgbColor.Blue;

            ptxtSym.Angle = annoElement.Symbol.Angle;
            ptxtSym.Color = rgb;
            ptxtSym.Font = annoElement.Symbol.Font;
            ptxtSym.HorizontalAlignment = annoElement.Symbol.HorizontalAlignment;
            ptxtSym.RightToLeft = annoElement.Symbol.RightToLeft;
            ptxtSym.Size = annoElement.Symbol.Size;
            ptxtSym.Text = annoElement.Symbol.Text;
            ptxtSym.VerticalAlignment = annoElement.Symbol.VerticalAlignment;
            ptxtElement.Symbol = ptxtSym;
            ((IElement)ptxtElement).Geometry = pAnnoFeature.Annotation.Geometry;
            ((IElement)ptxtElement).Locked = pAnnoFeature.Annotation.Locked;
            pAnnoFeature.Annotation = ptxtElement as IElement;
            ((IFeature)pAnnoFeature).Store();
        }

        private void SpecialLayerRender(IFeatureLayer featureLayer, IColor color, IColor colorOutline)
        {
            int i;
            IFeatureClass pFc = featureLayer.FeatureClass;
            IGeoFeatureLayer geoFL = featureLayer as IGeoFeatureLayer;
            if (pFc == null || geoFL == null) return;
            if (pFc.FeatureType != esriFeatureType.esriFTAnnotation && pFc.FeatureType != esriFeatureType.esriFTCoverageAnnotation)
            {
                IMarkerSymbol markerSymbol = null;
                ILineSymbol lineSymbol = null;
                IFillSymbol fillSymbol = null;
                if (geoFL.Renderer is IUniqueValueRenderer)
                {
                    IUniqueValueRenderer pUniqueRender = geoFL.Renderer as IUniqueValueRenderer;
                    switch (pFc.ShapeType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                            for (i = 0; i < pUniqueRender.ValueCount;i++ )
                            {
                                markerSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as IMarkerSymbol;
                                markerSymbol.Color = color;
                            }
                            if (pUniqueRender.UseDefaultSymbol)
                            {
                                markerSymbol = pUniqueRender.DefaultSymbol as IMarkerSymbol;
                                markerSymbol.Color = color;
                            }
                            break;
                        case esriGeometryType.esriGeometryPolyline:
                            for (i = 0; i < pUniqueRender.ValueCount; i++)
                            {
                                lineSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as ILineSymbol;
                                lineSymbol.Color = color;
                            }
                            if (pUniqueRender.UseDefaultSymbol)
                            {
                                lineSymbol = pUniqueRender.DefaultSymbol as ILineSymbol;
                                lineSymbol.Color = color;
                            }
                            break;
                        case esriGeometryType.esriGeometryPolygon:
                            for (i = 0; i < pUniqueRender.ValueCount;i++ )
                            {
                                fillSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as IFillSymbol;
                                fillSymbol.Color = color;
                                lineSymbol = fillSymbol.Outline;
                                lineSymbol.Color = colorOutline;
                                fillSymbol.Outline = lineSymbol;
                            }
                            if (pUniqueRender.UseDefaultSymbol)
                            {
                                fillSymbol = pUniqueRender.DefaultSymbol as IFillSymbol;
                                fillSymbol.Color = color;
                                lineSymbol = fillSymbol.Outline;
                                lineSymbol.Color = colorOutline;
                                fillSymbol.Outline = lineSymbol;
                            }
                            break;
                    }
                }
                else if (geoFL.Renderer is ISimpleRenderer)
                {
                    ISimpleRenderer pSimpleRender = geoFL.Renderer as ISimpleRenderer;
                    switch (pFc.ShapeType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                            (pSimpleRender.Symbol as IMarkerSymbol).Color = color;
                            break;
                        case esriGeometryType.esriGeometryPolyline:
                            (pSimpleRender.Symbol as ILineSymbol).Color = color;
                            break;
                        case esriGeometryType.esriGeometryPolygon:
                            fillSymbol = pSimpleRender.Symbol as IFillSymbol;
                            fillSymbol.Color = color;
                            lineSymbol = fillSymbol.Outline;
                            lineSymbol.Color = colorOutline;
                            fillSymbol.Outline = lineSymbol;
                            break;
                    }
                }
            }
            else if (pFc.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
            {
                ICoverageAnnotationLayer pCAnnoLayer = featureLayer as ICoverageAnnotationLayer;
                for (i = 0; i < pCAnnoLayer.SymbolCount; i++)
                {
                    pCAnnoLayer.set_FontColor(pCAnnoLayer.get_SymbolNumber(i), color);
                }
            }
            else
            {
                (featureLayer as ISymbolSubstitution).SubstituteType = esriSymbolSubstituteType.esriSymbolSubstituteColor;
                (featureLayer as ISymbolSubstitution).MassColor = color;
            }
        }
        private void SpecialLayerRender(IFeatureLayer featureLayer, IColor color, IColor colorOutline,IColor colorBack,IColor colorFill,string strFilter)
        {
            int i, j;
            bool bFlag;
            IUniqueValueRenderer pUniqueRender = null;
            IUniqueValueRenderer pUniqueRender_new = null;
            IMarkerSymbol markerSymbol = null;
            ILineSymbol lineSymbol = null;
            IFillSymbol fillSymbol = null;
            IGeoFeatureLayer geoFL = featureLayer as IGeoFeatureLayer;
            if (geoFL == null) return;
            IArray arrProperty = this.GetProperty(strFilter);
            IField pFilterFld = this.frmFilter.Field;

            if (geoFL.Renderer is IUniqueValueRenderer)
            {
                pUniqueRender = geoFL.Renderer as IUniqueValueRenderer;
                switch (featureLayer.FeatureClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                    for (i = 0; i < pUniqueRender.ValueCount;i++)
                    {
                        bFlag = false;
                        for (j=0;j < arrProperty.Count;j++)
                        {
                            if(pUniqueRender.get_Value(i) == arrProperty.get_Element(j).ToString())
                            {
                                markerSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as IMarkerSymbol;
                                markerSymbol.Color = color;
                                bFlag = true;
                                break;
                            }
                 
                        }
                        if(bFlag) continue;
                        markerSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as IMarkerSymbol;
                        markerSymbol.Color = colorBack;                       
                                      
                    }
                    if (pUniqueRender.UseDefaultSymbol)
                    {
                        markerSymbol = pUniqueRender.DefaultSymbol as IMarkerSymbol;
                        markerSymbol.Color = colorBack;
                    }
                    break;
                    case esriGeometryType.esriGeometryPolyline:
                    for (i = 0; i < pUniqueRender.ValueCount;i++)
                    {
                        bFlag = false;
                        for (j=0;j < arrProperty.Count;j++)
                        {
                            if(pUniqueRender.get_Value(i) == arrProperty.get_Element(j).ToString())
                            {
                                lineSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as ILineSymbol;
                                lineSymbol.Color = color;
                                bFlag = true;
                                break;
                            }
                 
                        }
                        if(bFlag) continue;
                        lineSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as ILineSymbol;
                        lineSymbol.Color = colorBack;                       
                                      
                    }
                    if (pUniqueRender.UseDefaultSymbol)
                    {
                        lineSymbol = pUniqueRender.DefaultSymbol as ILineSymbol;
                        lineSymbol.Color = colorBack;
                    }
                    break;
                    case esriGeometryType.esriGeometryPolygon:
                    for (i = 0; i < pUniqueRender.ValueCount;i++)
                    {
                        bFlag = false;
                        for (j=0;j < arrProperty.Count;j++)
                        {
                            if(pUniqueRender.get_Value(i) == arrProperty.get_Element(j).ToString())
                            {
                                fillSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as IFillSymbol;
                                fillSymbol.Color = color;
                                lineSymbol = fillSymbol.Outline;
                                lineSymbol.Color = colorOutline;
                                fillSymbol.Outline = lineSymbol;
                                bFlag = true;
                                break;
                            }
                 
                        }
                        if(bFlag) continue;
                        fillSymbol = pUniqueRender.get_Symbol(pUniqueRender.get_Value(i)) as IFillSymbol;
                        fillSymbol.Color = colorFill;
                        lineSymbol = fillSymbol.Outline;
                        lineSymbol.Color = colorBack;
                        fillSymbol.Outline = lineSymbol;
                                                                                
                    }
                    if (pUniqueRender.UseDefaultSymbol)
                    {
                        fillSymbol = pUniqueRender.DefaultSymbol as IFillSymbol;
                        fillSymbol.Color = colorFill;
                        lineSymbol = fillSymbol.Outline;
                        lineSymbol.Color = colorBack;
                        fillSymbol.Outline = lineSymbol;
                    }
                    break;
                
                }
            }
            else if (geoFL.Renderer is ISimpleRenderer)
            {
                pUniqueRender_new = new UniqueValueRendererClass();
                pUniqueRender_new.FieldCount = 1;
                pUniqueRender_new.set_Field(0, pFilterFld.Name);
                ISimpleRenderer pSimpleRender = geoFL.Renderer as ISimpleRenderer;

                IClone pCloneSymbol_2 = (pSimpleRender.Symbol as IClone).Clone();
                pUniqueRender_new.DefaultSymbol = pCloneSymbol_2 as ISymbol;
                pUniqueRender_new.UseDefaultSymbol = true;
                switch (featureLayer.FeatureClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        for (j = 0; j < arrProperty.Count; j++)
                        {
                            IClone pCloneSymbol_1 = (pSimpleRender.Symbol as IClone).Clone();
                            pUniqueRender_new.AddValue(arrProperty.get_Element(j).ToString(), arrProperty.get_Element(j).ToString(), pCloneSymbol_1 as ISymbol);
                            (pCloneSymbol_1 as IMarkerSymbol).Color = color;
                        }
                        (pCloneSymbol_2 as IMarkerSymbol).Color = colorBack;
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                         for (j = 0; j < arrProperty.Count; j++)
                         {
                             IClone pCloneSymbol_1 = (pSimpleRender.Symbol as IClone).Clone();
                             pUniqueRender_new.AddValue(arrProperty.get_Element(j).ToString(), arrProperty.get_Element(j).ToString(), pCloneSymbol_1 as ISymbol);
                             (pCloneSymbol_1 as ILineSymbol).Color = color;
                         }
                         (pCloneSymbol_2 as ILineSymbol).Color = colorBack;
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                         for (j = 0; j < arrProperty.Count; j++)
                         {
                             IClone pCloneSymbol_1 = (pSimpleRender.Symbol as IClone).Clone();
                             pUniqueRender_new.AddValue(arrProperty.get_Element(j).ToString(), arrProperty.get_Element(j).ToString(), pCloneSymbol_1 as ISymbol);
                             (pCloneSymbol_1 as IFillSymbol).Color = color;
                             lineSymbol = (pCloneSymbol_1 as IFillSymbol).Outline;
                             lineSymbol.Color = colorOutline;
                             (pCloneSymbol_1 as IFillSymbol).Outline = lineSymbol;
                         }
                         (pCloneSymbol_2 as IFillSymbol).Color = colorFill;
                         lineSymbol = (pCloneSymbol_2 as IFillSymbol).Outline;
                         lineSymbol.Color = colorBack;
                         (pCloneSymbol_2 as IFillSymbol).Outline = lineSymbol;

                        break;
                    default:
                        break;

                }
                geoFL.Renderer = pUniqueRender_new as IFeatureRenderer;
            }
        }

        private IArray GetProperty(string strFilter)
        {
            string[] s = null;
            string[] p = null;
            IArray arrProperty = new ArrayClass();
            if (strFilter.IndexOf("or") > -1)
            {
                strFilter = strFilter.Replace("or", "-");
                s = strFilter.Split('-');
                for (int i = 0; i < s.Length;i++ )
                {
                    if (s[i].IndexOf("=") > -1)
                    {
                        p = s[i].Split('=');
                        arrProperty.Add(p[1].Trim());
                    }
                    else
                    {
                        arrProperty.Add(s[i].Trim());
                    }
                }

            }
            else
            {
                if (strFilter.IndexOf("=") > -1)
                {
                    s = strFilter.Split('=');
                    arrProperty.Add(s[1].Trim());
                }
            }
            return arrProperty;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "mxd files (*.mxd)|*.mxd|All files (*.*)|*.*";
                sfd.FilterIndex = 1;
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    IMapDocument mapDoc = new MapDocumentClass();
                    mapDoc.New(fileName);
                    mapDoc.Open(fileName, null);
                    int layer2DNum = 0;
                    IMap map = mapDoc.get_Map(0);
                    ILayer pMapLayer;
                    for (int i = app.Current2DMapControl.Map.LayerCount - 1; i > -1; i--)
                    {
                        pMapLayer = app.Current2DMapControl.Map.get_Layer(i);
                        if (pMapLayer is ICadLayer)
                        {
                            map.AddLayer(pMapLayer);
                        }
                    }
                    for (int i = app.Current2DMapControl.Map.LayerCount - 1; i > -1; i--)
                    {
                        pMapLayer = app.Current2DMapControl.Map.get_Layer(i);
                        if (pMapLayer is ICadLayer) continue;
                        if (pMapLayer.Name.Length < 3) pMapLayer.Name += "_2D";
                        else
                        {
                            if (pMapLayer.Name.Substring(pMapLayer.Name.Length - 3) != "_2D")
                            {
                                pMapLayer.Name += "_2D";
                            }
                        }
                        map.AddLayer(pMapLayer);
                        layer2DNum++;
                    }
                    IActiveView pActiveView = mapDoc.ActiveView;
                    if (layer2DNum > 0) pActiveView.Extent = app.Current2DMapControl.ActiveView.Extent;
                    else if (layer2DNum == 0)
                    {
                        pActiveView = null;
                    }
                    map.ReferenceScale = app.Current2DMapControl.ReferenceScale;
                    map.MapUnits = app.Current2DMapControl.MapUnits;
                    IMapBookmarks docBookMarks = app.Current2DMapControl.Map as IMapBookmarks;
                    IEnumSpatialBookmark enumBookMarks = docBookMarks.Bookmarks;
                    enumBookMarks.Reset();
                    ISpatialBookmark bookMark = enumBookMarks.Next();
                    while (bookMark != null)
                    {
                        docBookMarks.AddBookmark(bookMark);
                        bookMark = enumBookMarks.Next();
                    }
                    mapDoc.Save(true, false);
                    WaitForm.SetCaption("成功保存工程文件");

                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
        }
    }
}
