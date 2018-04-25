using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Text;
using DF2DTool.Interface;

namespace DF2DTool.Class
{
    public class GisRead : IGisRead
    {
        private const string GISLAYERNAME = "GISLAYERNAME";
        //public event ReadGisHandler ReadEvent;

		private CoreData currentData=new CoreData ();			
		private const double PI = Math.PI;
		private ConvertLog logWriter;
        private IMap pMap;

        #region 对外的属性和方法

        public ConvertLog LogWriter
        {
            set { logWriter = value; }
        }
        /// <summary>
        ///以文件输出形式输入的图层名称
        /// </summary>
        private DataSetNames inputDs;
        public DataSetNames InputDs
        {
            set { inputDs = value; }
        }

        /// <summary>
        /// 以选择集导出方式输入的选择集
        /// </summary>
        private IFeatureSelection pFeaSelection;
        public ESRI.ArcGIS.Carto.IFeatureSelection PFeaSelection
        {
            set { pFeaSelection = value; }
        }

        /// <summary>
        /// 地物字段名
        /// </summary>
        private string strObjNum;
        public string StrObjNum
        {
            set { strObjNum = value; }
        }

        /// <summary>
        /// 块符号倾斜角度
        /// </summary>
        private string strAngle;
        public string StrAngle
        {
            set { strAngle = value; }
        }

        /// <summary>
        /// 配置的mdb文件名
        /// </summary>
        private string mdbFileName;
        public string MdbFileName
        {
            set { mdbFileName = value; }
        }

        /// <summary>
        /// 图层对应表
        /// </summary>
        private string layerTable;
        public string LayerTable
        {
            set { layerTable = value; }
        }

        /// <summary>
        /// 符号对应表
        /// </summary>
        private string symbolTable;
        public string SymbolTable
        {
            set { symbolTable = value; }
        }

      

        /// <summary>
        /// 以选择及导出方式输入的地图
        /// </summary>
        public ESRI.ArcGIS.Carto.IMap PMap
        {
            set { pMap = value; }
        }

        /// <summary>
        /// 当前的数据集
        /// </summary>
        private DataSet currentDs;
        public DataSet CurrentDs
        {
            get { return currentDs; }
            set { currentDs = value; }
        }

        //地图比例尺
        private double mapScale = 0.5;
        public double MapScale
        {
            set { mapScale = value; }
        }

        #endregion

        #region 初始化
		/// <summary>
		/// 初始化
		/// </summary>
		public bool GisReadInit()
		{
			bool initOk=true;
			currentDs=currentData.CoreDs ;
			logWriter.CurrentDs=currentDs;
			//向用过的字体表中添加固定的三种字体
            this.addUsed3Fonts();

			//读出图层符号对应表
			try
			{
				this.loadLayerTable();
				this.loadSymbolTable();
                this.loadAttributeTable();   //2013.11.07  TianK 添加
                this.loadBlockLineTypeFontTable();   //2013.11.11  TianK 添加
			}
            catch (Exception ex)
			{
                Console.WriteLine(ex.Message+"\r\n"+ex.StackTrace);
				logWriter.AddErrorLog("5","读取配置文件错误<br>");
				MessageBox.Show("读取配置文件错误","提示信息",MessageBoxButtons.OK ,MessageBoxIcon.Error);				
				initOk=false;				
			}
			return initOk;
		}
		#endregion

		#region 读取配置文件的内容到dataset
		/// <summary>
		/// 读取layertable表
		/// </summary>
		private void loadLayerTable()
		{
			ITable pLayerTable;
			pLayerTable=PublicFun.GetAccessTable(mdbFileName,layerTable);

			IRow pRow;
			ICursor pCursor;
		
			DataTable tmpTable;
			DataRow tmpRow;
			tmpTable=this.CurrentDs.Tables["layerTable"];

			pCursor = pLayerTable.Search(null,true);
			pRow = pCursor.NextRow();
			
			while(pRow!=null)
			{				
				tmpRow = tmpTable.NewRow();
				tmpRow["CadLayer"]=getTableValue((object)pRow.get_Value(pRow.Fields.FindField("CadLayer")));	
				tmpRow["LCOLOR"]=getTableValue((object)pRow.get_Value(pRow.Fields.FindField("LColor")));    //2007.06.06 TianK 添加

				tmpTable.Rows.Add(tmpRow);	

				pRow = pCursor.NextRow();
			}
		}

		/// <summary>
		/// 读取symboltable表
		/// </summary>
		private void loadSymbolTable()
		{
			ITable pSymbolTable;
			pSymbolTable=PublicFun.GetAccessTable(mdbFileName,symbolTable);

			IRow pRow;
			ICursor pCursor;
		
			DataTable tmpTable;
			DataRow tmpRow;
			tmpTable=this.CurrentDs.Tables["symbolTable"];

			pCursor = pSymbolTable.Search(null,true);
			pRow = pCursor.NextRow();
			
			while(pRow!=null)
			{				
				tmpRow = tmpTable.NewRow();
                tmpRow["DifGISCode"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("DifGISCode")));
                tmpRow["SymbolCode"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("SymbolCode")));
                tmpRow["LWIDTH"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("LWidth")));
				tmpTable.Rows.Add(tmpRow);	

				pRow = pCursor.NextRow();
			}
		}

        /// <summary>
        /// 读取AttributeCASStoDifGIS对照表   2013.11.07  TianK 添加
        /// </summary>
        private void loadAttributeTable()
        {
            ITable pLayerTable;
            pLayerTable = PublicFun.GetAccessTable(mdbFileName, "AttributeCASStoDifGIS");

            IRow pRow;
            ICursor pCursor;

            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = this.CurrentDs.Tables["AttributeCASStoDifGIS"];

            pCursor = pLayerTable.Search(null, true);
            pRow = pCursor.NextRow();

            while (pRow != null)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["CASSAttributeName"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("CASSAttributeName"))).ToUpper ();
                tmpRow["DifGISAttributeName"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("DifGISAttributeName"))).ToUpper ();
                tmpTable.Rows.Add(tmpRow);
                pRow = pCursor.NextRow();
            }
        }
        /// <summary>
        /// 读取块、线型、字体定义表   2013.11.11  TianK 添加
        /// </summary>
        private void loadBlockLineTypeFontTable()
        {
            ITable pTable;
            pTable = PublicFun.GetAccessTable(mdbFileName, "DefineBlockLineTypeFont");

            IRow pRow;
            ICursor pCursor;

            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = this.CurrentDs.Tables["DefineBlockLineTypeFont"];

            pCursor = pTable.Search(null, true);
            pRow = pCursor.NextRow();

            while (pRow != null)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["ID"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("ID")));
                tmpRow["DEFINE"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("DEFINE")));
                tmpRow["TYPE"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("TYPE")));
                tmpTable.Rows.Add(tmpRow);
                pRow = pCursor.NextRow();
            }
        }
		/// <summary>
		/// 获取数据库字段的值
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		private string getTableValue(object obj)
		{
			string rtnValue="";
			if(obj==System.DBNull.Value)
				rtnValue="";
			else
				rtnValue=obj.ToString();

			return rtnValue;
		}
		#endregion

		#region 读取实体主程序
		//从数据库中读取数据
		//遍历选择的featureclass
		public void Read_EntitiesFromDatabase()
		{
			string				layerName;			
			IName				pName;
			IFeatureClassName	pFcn;		
			IFeatureClass		pFC;
		
			//默认的输入图层与输出图层名字一样
			//需要修改的时候与对照表对照
			for(int i=0;i<inputDs.Count;i++)
			{
				IDatasetName datasetName=inputDs.get_Item(i);

				//feactureclass
				if (datasetName.Type==esriDatasetType.esriDTFeatureClass)
				{
					pFcn=(IFeatureClassName)datasetName;
					pName=(IName)pFcn;								
					pFC=(IFeatureClass)pName.Open ();
					layerName = datasetName.Name;

					Read_Entity_0(pFC,layerName);
				}
				//dataset
				if (datasetName.Type==esriDatasetType.esriDTFeatureDataset)
				{
					IFeatureDatasetName featureDatasetName = (IFeatureDatasetName) datasetName;

					IEnumDatasetName enumDatasetNameFC = featureDatasetName.FeatureClassNames;

					IDatasetName datasetNameFC = enumDatasetNameFC.Next();
					while (datasetNameFC != null)
					{                        
						IFeatureClassName featureClassName = (IFeatureClassName) datasetNameFC;	
						pName=(IName)featureClassName;
						pFC=(IFeatureClass)pName.Open();
						layerName=datasetNameFC.Name;						

						Read_Entity_0(pFC,layerName);

						datasetNameFC = enumDatasetNameFC.Next();
					}
				}
			}			
		}

		//从选择集中读取数据
		public void Read_EntitiesFromSelection()
		{
			string				sName,dName;            
			string				dAngle="0.0";   
			string				sSymId,m_LinTypeorSymbolorFont="",m_LineWidth="";
			string				layerName="0"; 

			ILayer				pLayer;
			IEnumLayer			enumLayers;
			IFeatureSelection	pFeatureselection;
			IFeature			pFeature;			
			IFeatureCursor		fCursor;
			ICursor				pCursor;
			IGeometry			pGeometry;
			ISelectionSet       pSelectionSet;
			IFeatureLayer		pFeatureLayer;
			int				fieldIndex;
			enumLayers = pMap.get_Layers(null,true);			

			while((pLayer = enumLayers.Next())!=null)
			{
				if (pLayer is IFeatureLayer)
				{
					pFeatureLayer=pLayer as IFeatureLayer ;
                    if (pFeatureLayer.FeatureClass == null) continue;
                    writeAppidTable(pFeatureLayer.FeatureClass.Fields);  //记录属性字段名
					layerName=(pFeatureLayer.FeatureClass as IDataset ).Name  ;		//图层名称,要对比后使用

					pFeatureselection = (IFeatureSelection)pFeatureLayer;
					
					if(pFeatureselection.SelectionSet.Count>0)
					{
						pSelectionSet = (ISelectionSet)pFeatureselection.SelectionSet;
						pSelectionSet.Search(null,false,out pCursor);
						
						fCursor=(IFeatureCursor)pCursor;
						pFeature=fCursor.NextFeature();

						while (pFeature != null)
						{   
							PublicFun.ReadEntCount++;
                            //this.ReadEvent(PublicFun.ReadEntCount,PublicFun.WrtieEntCount);
	                        
							//取出源图层，和原符号
							sName=layerName;
							sSymId=this.getSSymId(pFeature);
							//对比出目标图层和目标符号
							m_LinTypeorSymbolorFont=this.findSymbol(sName,sSymId,pFeature,ref m_LineWidth);
                            dName = this.findLayer(pFeature,sName);	

							if(pFeature is IAnnotationFeature)
								Read_anno( pFeature,dName,m_LinTypeorSymbolorFont);					
							else
							{
								pGeometry = pFeature.Shape;		
								//如果是点图层,查找字段中有无角度字段				
								if (pGeometry.GeometryType==esriGeometryType.esriGeometryPoint)
								{
									fieldIndex=pFeature.Fields.FindField(strAngle);
									if(fieldIndex>0)
										dAngle=pFeature.get_Value(fieldIndex).ToString();
								}

								if( pFeature.Shape!=null)
                                    Read_Entity(pFeature, m_LinTypeorSymbolorFont, dName, dAngle, sSymId, m_LineWidth);						
							}
							
							pFeature = fCursor.NextFeature();
						}
					}	
				}	
				
			}			
		}
		#endregion

		#region 读取实体
		/// <summary>
		/// 在一个featureclass中遍历实体
		/// </summary>
		/// <param name="pFC"></param>
		/// <param name="layerName"></param>
		private void Read_Entity_0(IFeatureClass pFC,string layerName)
		{	
			IGeometry		pGeometry;
			IFeature		pFeature;
			IFeatureCursor	fCursor;			
			string			sName,dName,dAngle="0";
			string			sSymId,m_LinTypeorSymbolorFont="",m_LineWidth="";
			int				fieldIndex;

			if(pFC != null)
			{
                writeAppidTable(pFC.Fields);  //记录属性字段名
				fCursor = pFC.Search(null,true);
				pFeature = fCursor.NextFeature();

				while (pFeature != null)
				{   
					PublicFun.ReadEntCount++;
                    //this.ReadEvent(PublicFun.ReadEntCount,PublicFun.WrtieEntCount);

					//取出源图层，和原符号
					sName=layerName;
					sSymId=this.getSSymId(pFeature);
					//对比出目标图层和目标符号
					m_LinTypeorSymbolorFont=this.findSymbol(sName,sSymId,pFeature,ref m_LineWidth);
                    dName = this.findLayer(pFeature,sName);	

					if(pFeature is IAnnotationFeature)
					{
						if(pFeature.Shape!=null)
							this.Read_anno( pFeature,dName,m_LinTypeorSymbolorFont);					
					}
					else
					{
						if( pFeature.Shape!=null)	
						{
							pGeometry = pFeature.Shape;		
							//如果是点图层,查找字段中有无角度字段				
							if (pGeometry.GeometryType==esriGeometryType.esriGeometryPoint)
							{
								fieldIndex=pFeature.Fields.FindField(strAngle);
								if(fieldIndex>0)
									dAngle=pFeature.get_Value(fieldIndex).ToString();
							}
                            this.Read_Entity(pFeature, m_LinTypeorSymbolorFont, dName, dAngle, sSymId, m_LineWidth);		
						}
					}
					pFeature = fCursor.NextFeature();
				}		
			}		
		}

		/// <summary>
		/// 读实体   
		/// </summary>
		/// <param name="pGeometry"></param>
		/// <param name="strGeoObjNum"></param>
		/// <param name="strLayName"></param>
        private void Read_Entity(IFeature pfeature, string strGeoObjNum, string strLayName, string dangle, string sSymId, string m_LineWidth)
		{	
//			pEnv.Union (pGeometry.Envelope );
            switch (pfeature.Shape.GeometryType)
			{
				case esriGeometryType.esriGeometryPoint:
                    Read_point(pfeature, strGeoObjNum, strLayName, dangle);
					break;
				case esriGeometryType.esriGeometryPolyline:
				case esriGeometryType.esriGeometryLine:
				case esriGeometryType.esriGeometryCircularArc:
				case esriGeometryType.esriGeometryPolygon:		//填充
                    ReadPolyline(pfeature, strGeoObjNum, strLayName, sSymId, m_LineWidth);
					break;
			}
		}

		#region 绘制点
        private void Read_point(IFeature pfeature, string strGeoObjNum, string strLayName, string dangle)
		{
			//PublicFun.ReadEntCount++;
			//this.ReadEvent(PublicFun.ReadEntCount,PublicFun.WrtieEntCount);

			IPoint pPoint;
            pPoint = (IPoint)pfeature.Shape ;

			string dName,dSym,dAngle="0";
			string X,Y,Z;
		
			dName=strLayName;
			dSym=strGeoObjNum;
			dAngle=dangle;

			X=pPoint.X.ToString();
			Y=pPoint.Y.ToString();
			Z=pPoint.Z.ToString();
			if(Z=="非数字")
				Z="0.0";
				
			DataTable tmpTable;
			DataRow   tmpRow ;
		
			tmpTable=this.CurrentDs.Tables["pointTable"];
			tmpRow = tmpTable.NewRow();
			tmpRow["dLayer"]=dName;
			tmpRow["DifGISCode"]=dSym;
            //tmpRow["lcolor"]=m_Color;
			tmpRow["Dirction"]=dAngle;
			tmpRow["X"]=X;
			tmpRow["Y"]=Y;
			tmpRow["Z"]=Z;
            tmpRow["AttriBute"] = getAttriBute(pfeature);

            tmpTable.Rows.Add(tmpRow);		
		}	
	
		#endregion

		#region 绘制Polyline
		/// <summary>
		/// 向dxf文件中写入线要素，将每个线要素拆分成为最小的绘图单元
		/// </summary>
		/// <param name="pGeometry"></param>
		/// <param name="strGeoObjNum"></param>
		/// <param name="strLayName"></param>
        private void ReadPolyline(IFeature pfeature, string dSym, string dName, string sSymId, string m_LineWidth)
		{
			IGeometryCollection pGeoColl;
			ISegmentCollection	pSegColl;
			ISegment			pSeg;			
			DataTable polyLineTable;
			Guid puid=new Guid();
			int pointCount=0,length;
			IPoint pPoint=new PointClass ();
			IPoint pPoint1=new PointClass ();
			ICircularArc		pCa;
			IPoint cenPt=new PointClass();
			IPoint fromPt=new PointClass();
			IPoint toPt=new PointClass();
			double fromAngle,toAngle,td=0;
			double dirction;
            string attribute = getAttriBute(pfeature);

			polyLineTable=this.CurrentDs.Tables["polylineTable"];

            pGeoColl = (IGeometryCollection)pfeature.Shape ;
			for(int j=0;j<pGeoColl.GeometryCount ;j++)
			{
				pSegColl	= (ISegmentCollection)pGeoColl.get_Geometry(j);
				puid=Guid.NewGuid();
				pointCount=0;
				for (int i=0 ;i<pSegColl.SegmentCount;i++)
				{
					pSeg = pSegColl.get_Segment(i);
					switch(pSeg.GeometryType)
					{
						case esriGeometryType.esriGeometryLine:			//Pline中的Line		
                            AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, pSeg.FromPoint, pSeg.ToPoint, 0, m_LineWidth, attribute);		
							break;
						case esriGeometryType.esriGeometryCircularArc:	//Pline中的Arc	圆弧曲线
							pCa=(ICircularArc)pSeg;
							cenPt=pCa.CenterPoint;
							fromPt=pCa.FromPoint;
							toPt=pCa.ToPoint;

							if(pCa.IsCounterClockwise)
							{
								fromAngle = pCa.FromAngle;
								toAngle   = pCa.ToAngle;
								dirction=1;
							}
							else
							{
								fromAngle = pCa.ToAngle ;
								toAngle   = pCa.FromAngle;
								dirction=-1;
							}
							try
							{
								//PublicFun.GetARC_u(cenPt,fromPt,toPt,fromAngle,toAngle,ref td);				
								//转换弧
								if(pCa.IsClosed && cenPt!=null)	//独立的圆	
								{
									fromPt.X =cenPt.X ;
									fromPt.Y =cenPt.Y-pCa.Radius ;
									toPt.X =cenPt.X ;
                                    toPt.Y = cenPt.Y + pCa.Radius;
                                    AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, fromPt, toPt, 1 * dirction, m_LineWidth, attribute);
                                    AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, toPt, fromPt, 1 * dirction, m_LineWidth, attribute);		
								}
								else						//polyLine中的弧
								{
									if(cenPt==null) td=0;
									else PublicFun.GetARC_u(cenPt,fromPt,toPt,fromAngle,toAngle,dirction,ref td);
                                    AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, fromPt, toPt, td, m_LineWidth,  attribute);		
								}				
							}
							catch(Exception ex)
							{
								Console.WriteLine(ex.Message+"\r\n"+ex.StackTrace);
								string log="Arc读错误，坐标：X="+fromPt.X.ToString()+" Y="+fromPt.Y.ToString()+"<br>";
								logWriter.AddErrorLog("5",log);			
							}
							break;
						case esriGeometryType.esriGeometryBezier3Curve:	//贝塞尔曲线
							if(pSeg.Length<=1)  //如果线长度小于1m按直线处理
							{
                                AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, pSeg.FromPoint, pSeg.ToPoint, 0, m_LineWidth,  attribute);		
							}
							else  //否则长度大于1m要内插点
							{
								length=0;
								//处理贝塞尔曲线的点（每隔1米内插一个点）
								while(length<pSeg.Length-1)
								{
									pSeg.QueryPoint(esriSegmentExtension.esriExtendAtFrom, length, false, pPoint);
									pSeg.QueryPoint(esriSegmentExtension.esriExtendAtFrom, length+1, false, pPoint1);

									if(length!=0)
										pPoint.Z =pSeg.FromPoint.Z ;
									pPoint1.Z=pSeg.FromPoint.Z;

									length=length+1;
                                    AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, pPoint, pPoint1, 0, m_LineWidth, attribute);		
								}	
								//贝塞尔曲线部分终点信息
								pSeg.QueryPoint(esriSegmentExtension.esriExtendAtFrom,length, false, pPoint);
								pPoint.Z =0;
                                AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, pPoint, pSeg.ToPoint, 0, m_LineWidth, attribute);		
							}
							break;
					}
				}
			}
		}

		/// <summary>
		/// 添加到多段线表中
		/// </summary>
		/// <param name="polyLineTable"></param>
		/// <param name="puid"></param>
		/// <param name="pointCount"></param>
		/// <param name="dName"></param>
		/// <param name="dSym"></param>
		/// <param name="sSym"></param>
		/// <param name="fromPt"></param>
		/// <param name="toPt"></param>
		/// <param name="td"></param>
		/// <param name="rwidth"></param>
		/// <param name="rcolor"></param>
        private void AddToPolyLineTable(DataTable polyLineTable, Guid puid, int pointCount, string dName, string dSym, string sSymId, IPoint fromPt, IPoint toPt, double td, string rwidth,  string attribute)
		{
			DataRow   tmpRow ;
			tmpRow = polyLineTable.NewRow();
			tmpRow["plId"]=puid.ToString ();
			tmpRow["plIndex"]=pointCount;
			tmpRow["dLayer"]=dName;
			tmpRow["DifGISCode"]=dSym;
			tmpRow["SMSSymbol"]=sSymId;  //2007.08.15  TianK 添加 用于导出编码
			tmpRow["beginX"]=fromPt.X.ToString();
			tmpRow["beginY"]=fromPt.Y.ToString();
			tmpRow["beginZ"]=fromPt.Z.ToString();
			if(tmpRow["beginZ"].ToString ()=="非数字")
				tmpRow["beginZ"]="0.0";
			tmpRow["endX"]=toPt.X.ToString();
			tmpRow["endY"]=toPt.Y.ToString();
			tmpRow["endZ"]=toPt.Z.ToString();
			if(tmpRow["endZ"].ToString ()=="非数字")
				tmpRow["endZ"]="0.0";
			tmpRow["td"]=td.ToString();
			if(tmpRow["td"].ToString ()=="非数字")
				tmpRow["td"]="0.0";
            //tmpRow["lcolor"]=rcolor;
			tmpRow["lwidth"]=rwidth;
            tmpRow["AttriBute"] = attribute;

            polyLineTable.Rows.Add(tmpRow);
		}
		
		#endregion

		#region 写注记图层
		/// <summary>
		/// 写注记图层
		/// </summary>
		/// <param name="pFeat"></param>
		private void Read_anno(IFeature pFeat,string strDXFLayName,string symbolId)
		{
			string dName,DifGISCode;
            string strAngle, sHeight, sScale, sFontName;
            int fieldIndex;

			dName=strDXFLayName;
			DifGISCode=symbolId;

			IAnnotationFeature pAnno;
			IGeometry pShape;
			IPolygon pPoly;

			pAnno = (IAnnotationFeature)pFeat;
			pShape=pFeat.ShapeCopy;
			pPoly=(IPolygon)pShape;

			IElement pElem;
			ITextElement pTextEl;

			pElem = pAnno.Annotation;
			
			if(pElem is ITextElement)
			{
				//PublicFun.ReadEntCount++;
				//this.ReadEvent(PublicFun.ReadEntCount,PublicFun.WrtieEntCount);

				pTextEl = (ITextElement)pElem;				
				IAnnoClass pAnnoClass;
				pAnnoClass =(IAnnoClass) pFeat.Class.Extension;
    
				IPointCollection pPoints;
				pPoints = (IPointCollection)pFeat.ShapeCopy;

				if(pPoints.PointCount>0 && pTextEl.Text !="")
				{    
					double dAngle,dHeight;

					dHeight=pTextEl.Symbol.Size;

                    fieldIndex = pFeat.Fields.FindField("angle");
                    if (fieldIndex > 0)
                    {
                        strAngle = pFeat.get_Value(fieldIndex).ToString();
                    }
                    else strAngle = "0";
                    dAngle = double.Parse(strAngle);

                    fieldIndex = pFeat.Fields.FindField("SHeight");                //2009.9.20 TianK  添加
                    if (fieldIndex > 0)
                    {
                        sHeight = pFeat.get_Value(fieldIndex).ToString();
                    }
                    else sHeight = "";

                    fieldIndex = pFeat.Fields.FindField("SScale");                //2009.9.20 TianK  添加
                    if (fieldIndex > 0)
                    {
                        sScale = pFeat.get_Value(fieldIndex).ToString();
                    }
                    else sScale = "";

                    fieldIndex = pFeat.Fields.FindField("SFontName");                //2009.9.20 TianK  添加
                    if (fieldIndex > 0)
                    {
                        sFontName = pFeat.get_Value(fieldIndex).ToString();
                    }
                    else sFontName = "";

					DataTable tmpTable;
					DataRow   tmpRow ;
		
					tmpTable=this.CurrentDs.Tables["textTable"];
					tmpRow = tmpTable.NewRow();
					tmpRow["dLayer"]=dName;
					tmpRow["DifGISCode"]=DifGISCode;
                    //tmpRow["lcolor"]=m_color;
					tmpRow["X1"]=pPoints.get_Point(0).X.ToString();
					tmpRow["Y1"]=pPoints.get_Point(0).Y.ToString();		
					tmpRow["X2"]=pPoints.get_Point(pPoints.PointCount - 2).X.ToString();
					tmpRow["Y2"]=pPoints.get_Point(pPoints.PointCount - 2).Y.ToString();
					tmpRow["Dirction"]=dAngle.ToString();
					tmpRow["dHeight"]=dHeight.ToString();
                    tmpRow["SHeight"] = sHeight;                 //2009.9.20 TianK  添加
                    tmpRow["SScale"] = sScale;                 //2009.9.20 TianK  添加
                    tmpRow["SFontName"] = sFontName;                 //2009.9.20 TianK  添加
					tmpRow["text"]=pTextEl.Text;
                    tmpRow["AttriBute"] = getAttriBute(pFeat);

					tmpTable.Rows.Add(tmpRow);	
				}
			}
		}
		#endregion
        #endregion



        #region 内部辅助函数
        /// <summary>
		/// 读出源图层的源符号名字
		/// </summary>
		/// <param name="pfea"></param>
		/// <returns></returns>
		private string getSSymId(IFeature pFeature)
		{
			int fieldIndex=0;
			string rtnValue="";
			//读出源图层的源符号编码
			if(pFeature is IAnnotationFeature)		//注记
				fieldIndex=pFeature.Fields.FindField(strObjNum);
			else
				fieldIndex=pFeature.Fields.FindField(strObjNum);

			if(fieldIndex<=-1)
				rtnValue="";
			else 
			{
				if(pFeature.get_Value(fieldIndex)==System.DBNull.Value )
					rtnValue="";
				else
					rtnValue=((object)pFeature.get_Value(fieldIndex)).ToString();
			}	
			return rtnValue;
		}

        /// <summary>
        /// 在图层对照表中查找对应的图层  //2009.03.21 TianK 修改
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="findField"></param>
        /// <returns></returns>
        private string findLayer(IFeature pFea, string sourceName)
        {
            string pSql = "", rtnValue = "", m_Color = "", strSMSCode = ""; //2007.06.06 TianK 修改
            DataTable tmpDt = currentDs.Tables["layerTable"];
            DataRow[] secTable;

            int index;
            bool find = false;
            index = pFea.Fields.FindField("SMSCode");
            if (index <= -1)
            {
                find = false;
            }
            else
            {
                strSMSCode = pFea.get_Value(index).ToString().Trim();
                if (strSMSCode == "")
                {
                    find = false;
                }
                else
                {
                    rtnValue = strSMSCode;
                    pSql = "CadLayer = '" + strSMSCode + "' ";
                    secTable = tmpDt.Select(pSql);
                    if (secTable.Length > 0)
                    {
                        m_Color = (string)secTable[0]["LCOLOR"];
                        find = true;
                    }
                    else
                    {
                        find = false;
                    }
                }
            }
            if (find == false)
            {
                if (strSMSCode != "")                   //2012.02.15  TianK修改  
                {
                    logWriter.AddErrorLog("0", "图层“" + strSMSCode + "”没找到对应颜色,使用索引色1的红色<br>");
                    rtnValue = strSMSCode;
                    m_Color = "1";
                }
                else
                {
                    logWriter.AddErrorLog("0", "数据表" + sourceName + "中有空图层“" + strSMSCode + "”，没找到对应关系，写到tmpLayer图层，,使用索引色1的红色<br>");
                    rtnValue = "tmpLayer";
                    m_Color = "1";
                }
            }

            rtnValue = rtnValue.Replace("、", "");                   //TianK 2009.8.13   添加 

            this.addUsedLayer(rtnValue, m_Color);//2007.06.06 TianK 添加
            return rtnValue;
        }


		/// <summary>
		/// 由原符号名通过符号对应表和符号定义文件计算出目标符号的名字，
		/// 并且将出现过的符号添加到usedsymbol表中
		/// </summary>
		/// <param name="sourceName"></param>
		/// <param name="findField"></param>
		/// <returns></returns>
		private string findSymbol(string sourceLayerName,string sourceName,IFeature pFeature,ref string m_LineWidth)
		{
			//步骤0：预定义参数
            string pSql = "", rtnValue = "", tmpValue = "", tmpValue1="";
			string defSym="";				//如果不符合条件缺省的符号名字
            string symTable = "DefineBlockLineTypeFont";				//符号、线形、字体在dataset中的表名
			string usedTable="";			//在系统中用到的符号的集合
			string type="",ctype="",log="";
			double width=0;
            int fieldIndex, index;
			string LAYERPIPE="DPARC500,FPARC500,BPARC500,HPARC500,ICPARC500,IGPARC500,IWPARC500,IOPARC500";

			m_LineWidth="";
            //m_Color="";
			DataRow [] secTable;
			DataTable tmpDt=new DataTable();

			if(pFeature is IAnnotationFeature)
			{
				defSym="HZTXT";
                //symTable="FONT";		//暂时没有字体定义
				usedTable="usedFont";
				type="4";
				ctype="字体";
                index = pFeature.Fields.FindField("SFontName");    //Tiank   2013.11.06 添加
			}
			else
			{ 
				if(pFeature.Shape==null )
					return "";
				switch(pFeature.Shape.GeometryType)
				{
					case esriGeometryType.esriGeometryPoint:
						defSym="defaultBlock";
                        //symTable="BLOCK";
						usedTable="usedBlock";
						type="1";
						ctype="点符号";
                        index = pFeature.Fields.FindField("BlockName");    //Tiank   2013.11.06 添加
						break;
					case esriGeometryType.esriGeometryPolygon :
						defSym="Continuous";			//无面的填充符号，要用线
                        //symTable="LINETYPE";
						usedTable="Usedlinetype";
						type="2";
						ctype="面符号";
                        index = pFeature.Fields.FindField("LineType");    //Tiank   2013.11.06 添加
						break;
					default:
						defSym="Continuous";
                        //symTable="LINETYPE";
						usedTable="Usedlinetype";
						type="2";
						ctype="线符号";
                        index = pFeature.Fields.FindField("LineType");    //Tiank   2013.11.06 添加
						break;
				}
			}
			//步骤1：原符号通过对应表对应出目标符号，如果没有对应关系则符号定义为defaultSymbol
			tmpDt=currentDs.Tables["symbolTable"];

			if(sourceName!="")   //如果原地物编码不为空则查找
			{
                pSql = "DifGISCode = '" + sourceName + "'";
                secTable = tmpDt.Select(pSql);
				if(secTable.Length>0)
				{
					tmpValue=(string)secTable[0]["SymbolCode"];
					m_LineWidth=(string)secTable[0]["LWIDTH"];
                    //m_Color=(string)secTable[0]["LCOLOR"];
                    tmpValue1 = tmpValue;         //TianK  2013.11.13  修改
				}
				if(LAYERPIPE.IndexOf (sourceLayerName)!=-1)
				{
					fieldIndex=pFeature.Fields .FindField ("UPORDOWN");
					if(fieldIndex>-1)
						if(pFeature.get_Value (fieldIndex).ToString ()=="1")
							m_LineWidth="0.15";
				}			
				if(m_LineWidth!="")
				{
					width=double.Parse (m_LineWidth)*2*mapScale;
					m_LineWidth=width.ToString ();
				}

			}
            if (index > -1 && pFeature.get_Value(index).ToString() != "")   //如果有线型字段或块名字段  Tiank   2013.11.06 添加
            {
                tmpValue = pFeature.get_Value(index).ToString();
            }
			if(tmpValue=="")   //如果没查找到对应符号则使用默认的
			{
                log = ctype + "地物编码为[" + sourceName + "]在转换对照表文件ConvertSymbol.mdb中没找到对应关系，使用默认" + ctype + "：" + defSym + "<br>";
				logWriter.AddErrorLog(type,log);
				tmpValue=defSym;
			}

			//步骤2：由目标符号检查符号定义表该符号的定义记录，如果没有则设定为defaultsymbol
			tmpDt=currentDs.Tables[symTable];
            secTable = tmpDt.Select("ID='" + tmpValue + "' and TYPE ='" + type + "'");
            if (secTable.Length <= 0)
            {
                secTable = tmpDt.Select("ID='" + tmpValue1 + "' and TYPE ='" + type + "'");
                if (secTable.Length > 0)
                    tmpValue = tmpValue1;
                else
                {
                    log = ctype + "地物编码为[" + sourceName + "]对应的符号[" + tmpValue + "]在ConvertSymbol.mdb中的DefineBlockLineTypeFont表中未定义，使用默认" + ctype + "：" + defSym + "<br>";
                    logWriter.AddErrorLog(type, log);
                    tmpValue = defSym;
                }
            } 
            rtnValue = tmpValue;
			//步骤3：将用过的符号添加到usedsymbol表中
			this.addUsedSymbol(usedTable,rtnValue);
			return rtnValue;
		}

		//向UsedBlock表中添加出现过的Block
		private void addUsedSymbol(string tableName,string symbolName)
		{
			DataTable tmpTable;
			DataRow   tmpRow ;
			DataRow [] secTable;	
		
			tmpTable=this.CurrentDs.Tables[tableName];
			
			secTable=tmpTable.Select("SymbolId='" + symbolName +"'");
			if(secTable.Length==0)
			{
				tmpRow = tmpTable.NewRow();
				tmpRow["SymbolId"]=symbolName;	
		
				tmpTable.Rows.Add(tmpRow);		
			}
		}

		//向UsedLayer表中添加出现过的图层    20007.06.06 TianK 添加
		private void addUsedLayer(string layerName,string color)
		{
			DataTable tmpTable;
			DataRow   tmpRow ;
			DataRow [] secTable;	
		
			tmpTable=this.CurrentDs.Tables["UsedLayer"];
			
			secTable=tmpTable.Select("Layer='" + layerName +"'");
			if(secTable.Length==0)
			{
				tmpRow = tmpTable.NewRow();
				tmpRow["Layer"]=layerName;	
				tmpRow["LCOLOR"]=color;	
		
				tmpTable.Rows.Add(tmpRow);		
			}
		}

		//向UsedFont表中添加固定的四种字体  2007.08.10 TianK 添加 
		private void addUsed3Fonts()
		{
			string font;
			font="HZTXT";
			this.addUsedSymbol("usedFont",font);
			font="HT";
			this.addUsedSymbol("usedFont",font);
			font="STANDARD";
			this.addUsedSymbol("usedFont",font);
            font = "SX";                                     //2011.11.11 TianK 添加 
            this.addUsedSymbol("usedFont", font);
		}

 		//2009.1.14 TianK 添加  用于记录读出的属性名表
        private void writeAppidTable(IFields pfields)
        {
            DataTable tmpTable;
            DataRow tmpRow;
            DataRow[] secTable;
            string attributeName;
            string attributeNameCAD;

            tmpTable = this.CurrentDs.Tables["AppID"];
            secTable = tmpTable.Select("AppID='" + GISLAYERNAME + "'");
            if (secTable.Length == 0)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["AppID"] = GISLAYERNAME;
                tmpTable.Rows.Add(tmpRow);
            }

            for (int i = 0; i < pfields.FieldCount; i++)
            {
                if (pfields.get_Field(i).Type == esriFieldType.esriFieldTypeOID ||
                    (pfields.get_Field(i).Editable
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeGeometry
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeBlob
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeGlobalID
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeGUID
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeRaster
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeXML))
                {
                    attributeName = pfields.get_Field(i).Name;
                    attributeNameCAD = QureyAttribute(attributeName);   //查找对应属性    2013.03.07  TianK   添加
                    secTable = tmpTable.Select("AppID='" + attributeNameCAD + "'");
                    if (secTable.Length == 0)
                    {
                        tmpRow = tmpTable.NewRow();
                        tmpRow["AppID"] = attributeNameCAD;
                        tmpTable.Rows.Add(tmpRow);
                    }
                }
            }
        }

        //2009.1.15 TianK 添加  用于读出要素的属性
        private string getAttriBute(IFeature pFea)
        {
            string retValue = "";
            string strValue = "";     //2012.1.10添加
            string attributeNameCAD;
            string layername = (pFea.Class as IDataset).Name;
            retValue = retValue + GISLAYERNAME + "&" + layername + "&";   //2013.4.9  TianK  添加  
            for (int i = 0; i < pFea.Fields.FieldCount; i++)
            {
                if (pFea.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeOID  || 
                    (pFea.Fields.get_Field(i).Editable 
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeGeometry
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeBlob
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeGlobalID
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeGUID
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeRaster
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeXML ))
                {
                    attributeNameCAD = QureyAttribute(pFea.Fields.get_Field(i).Name);   //查找对应属性    2013.03.07  TianK   添加

                    if (pFea.Fields.get_Field(i).Domain is ICodedValueDomain)
                    {
                        retValue = retValue + attributeNameCAD + "&" + PublicFun.GetCodedDescriptionDomainValue(pFea.Fields.get_Field(i).Domain as ICodedValueDomain, pFea.get_Value(i).ToString()) + "&";
                    }
                    else
                    {
                        strValue = pFea.get_Value(i).ToString().Replace("\r\n", "");    //2012.1.10添加，替换掉部分属性字段里的换行符
                        retValue = retValue + attributeNameCAD + "&" + strValue + "&";
                    }
                }
            }
            if (retValue.Length > 1 && retValue.Substring(retValue.Length - 1) == "&")  //移除最后一位分隔符号
            {
                retValue = retValue.Remove(retValue.Length - 1, 1);
            }
            return retValue;
        }
        #endregion

        #region 由DifGIS属性名查找对应CASS属性名    2013.03.07  TianK   添加
        private string QureyAttribute(string arcgisAttribute)
        {
            string rentenString = "";
            DataTable tmpTable;
            DataRow[] secTable;

            tmpTable = this.CurrentDs.Tables["AttributeCASStoDifGIS"];
            secTable = tmpTable.Select("DifGISAttributeName = '" + arcgisAttribute.ToUpper () + "' ");
            if (secTable.Length > 0)
            {
                rentenString = (string)secTable[0]["CASSAttributeName"];
            }
            if (rentenString == "")
            {
                rentenString = arcgisAttribute;
            }
            return rentenString;
        }
        #endregion

        
    }
    
}