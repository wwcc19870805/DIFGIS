using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;

namespace DF3DPipeCreateTool.Class
{
    public class DataModel
    {
        // Methods
        public static IFieldInfoCollection GetDataModel(string name, out string[] arrDBIndex)
        {
            IFieldInfoCollection infos = null;
            IFieldInfo newVal = null;
            arrDBIndex = null;
            infos = new FieldInfoCollectionClass();
            switch (name)
            {
                case "OC_Catalog":
                    newVal = new FieldInfoClass
                    {
                        Name = "Name",
                        Alias = "名称",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Code",
                        Alias = "设施类编码",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "PCode",
                        Alias = "父节点ID",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "FacilityType",
                        Alias = "设施类类型",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "LocationType",
                        Alias = "位置类型",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "TurnerStyle",
                        Alias = "拐角类型",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "TopoLayerId",
                        Alias = "拓扑层ID",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Visible",
                        Alias = "显示顺序",
                        FieldType = gviFieldType.gviFieldInt32
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "RenderStyle",
                        Alias = "渲染样式",
                        FieldType = gviFieldType.gviFieldBlob
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "OrderBy",
                        Alias = "显示顺序",
                        FieldType = gviFieldType.gviFieldInt32
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Comment",
                        Alias = "备注",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 200
                    };
                    infos.Add(newVal);
                    arrDBIndex = new string[] { "Code", "PCode", "FacilityType", "TopoLayerId" };
                    return infos;

                case "OC_FieldConfig":
                    newVal = new FieldInfoClass
                    {
                        Name = "FacClassCode",
                        Alias = "设施类编码",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80,
                        Nullable = false
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Name",
                        Alias = "字段名称",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80,
                        Nullable = false
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Alias",
                        Alias = "字段别名",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "StdFieldName",
                        Alias = "标准字段名",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "FieldType",
                        Alias = "字段类型",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Length",
                        Alias = "字段长度",
                        FieldType = gviFieldType.gviFieldInt32
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Nullable",
                        Alias = "是否可为空",
                        FieldType = gviFieldType.gviFieldInt16
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Visible",
                        Alias = "是否可见",
                        FieldType = gviFieldType.gviFieldInt16
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "OrderBy",
                        Alias = "显示排序",
                        FieldType = gviFieldType.gviFieldInt16
                    };
                    infos.Add(newVal);
                    arrDBIndex = new string[] { "FacClassCode" };
                    return infos;

                case "OC_FacilityStyle":
                    newVal = new FieldInfoClass
                    {
                        Name = "FacClassCode",
                        Alias = "设施类编码",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "ObjectId",
                        Alias = "唯一编码",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Name",
                        Alias = "名称",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "StyleType",
                        Alias = "风格类型",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 50
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "StyleInfo",
                        Alias = "风格信息",
                        FieldType = gviFieldType.gviFieldBlob
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Thumbnail",
                        Alias = "缩略图",
                        FieldType = gviFieldType.gviFieldBlob
                    };
                    infos.Add(newVal);
                    arrDBIndex = new string[] { "FacClassCode", "ObjectId", "StyleType" };
                    return infos;

                case "OC_ModelInfo":
                    newVal = new FieldInfoClass
                    {
                        Name = "GroupId",
                        Alias = "组编号",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "ObjectId",
                        Alias = "唯一编码",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Name",
                        Alias = "名称",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Code",
                        Alias = "资源编码",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Thumbnail",
                        Alias = "缩略图",
                        FieldType = gviFieldType.gviFieldBlob
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Comment",
                        Alias = "备注",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    arrDBIndex = new string[] { "GroupId", "ObjectId", "Name" };
                    return infos;

                case "OC_TextureInfo":
                    newVal = new FieldInfoClass
                    {
                        Name = "GroupId",
                        Alias = "组编号",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "ObjectId",
                        Alias = "唯一编码",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Name",
                        Alias = "名称",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Code",
                        Alias = "资源编码",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Thumbnail",
                        Alias = "缩略图",
                        FieldType = gviFieldType.gviFieldBlob
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Type",
                        Alias = "类型",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 50
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Comment",
                        Alias = "备注",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    arrDBIndex = new string[] { "GroupId", "ObjectId", "Name" };
                    return infos;

                case "OC_ColorInfo":
                    newVal = new FieldInfoClass
                    {
                        Name = "GroupId",
                        Alias = "组编号",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "ObjectId",
                        Alias = "唯一编码",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Name",
                        Alias = "名称",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Code",
                        Alias = "资源编码",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Thumbnail",
                        Alias = "缩略图",
                        FieldType = gviFieldType.gviFieldBlob
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Type",
                        Alias = "类型",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 50
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Comment",
                        Alias = "备注",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    arrDBIndex = new string[] { "GroupId", "ObjectId", "Name" };
                    return infos;
                
                case "OC_FacilityClass":
                    newVal = new FieldInfoClass
                    {
                        Name = "FacClassCode",
                        Alias = "设施类编码",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Name",
                        Alias = "名称",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "DataSetName",
                        Alias = "数据集名称",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "FeatureClassId",
                        Alias = "要素类ID",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "FcName",
                        Alias = "要素类名称",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "DataType",
                        Alias = "数据类型",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "FacilityType",
                        Alias = "设施类类型",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass                  // 添加字段标定管线位置—地下\架空 FX 2014.04.01
                    {
                        Name = "LocationType",
                        Alias = "位置类型",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass                  // 添加字段标定拐角位置—直角\圆角 FX 2014.09.22
                    {
                        Name = "TurnerStyle",
                        Alias = "拐角类型",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "RenderStyle",
                        Alias = "渲染样式",
                        FieldType = gviFieldType.gviFieldBlob
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Comment",
                        Alias = "备注",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 300
                    };
                    infos.Add(newVal);
                    arrDBIndex = new string[] { "FacClassCode", "FeatureClassId", "DataType", "FacilityType" };
                    return infos;

                case "OC_TopoManage":
                    newVal = new FieldInfoClass
                    {
                        Name = "ObjectId",
                        Alias = "唯一编号",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "TopoLayerName",
                        Alias = "拓扑层名称",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "TopoTableName",
                        Alias = "拓扑信息表名称",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Tolerance",
                        Alias = "容差值",
                        FieldType = gviFieldType.gviFieldDouble
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "IgnoreZ",
                        Alias = "空间建拓扑是否考虑Z",
                        FieldType = gviFieldType.gviFieldInt16,
                        Length = 80
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "ToleranceZ",
                        Alias = "Z方向容差",
                        FieldType = gviFieldType.gviFieldDouble
                    };
                    infos.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Comment",
                        Alias = "备注",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 200
                    };
                    infos.Add(newVal);
                    arrDBIndex = new string[] { "ObjectId", "TopoLayerName" };
                    return infos;

                case "OC_TopoInfo":
                    {
                        newVal = new FieldInfoClass
                        {
                            Name = "GroupId",
                            Alias = "逻辑组ID",
                            FieldType = gviFieldType.gviFieldString,
                            Length = 80
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "A_FacClass",
                            Alias = "边所在设施类",
                            FieldType = gviFieldType.gviFieldString,
                            Length = 80
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "Edge",
                            Alias = "边对应的要素ID",
                            FieldType = gviFieldType.gviFieldInt32
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "P_FacClass",
                            Alias = "前点所在设施类",
                            FieldType = gviFieldType.gviFieldString,
                            Length = 80
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "PNode",
                            Alias = "前点要素ID",
                            FieldType = gviFieldType.gviFieldInt32
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass                   // FX 2014.04.03
                        {
                            Name = "PDis",
                            Alias = "前点距离",
                            FieldType = gviFieldType.gviFieldDouble
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "E_FacClass",
                            Alias = "后点所在设施类",
                            FieldType = gviFieldType.gviFieldString,
                            Length = 80
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "ENode",
                            Alias = "后点要素ID",
                            FieldType = gviFieldType.gviFieldInt32
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass                   // FX 2014.04.03
                        {
                            Name = "EDis",
                            Alias = "后点距离",
                            FieldType = gviFieldType.gviFieldDouble
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "ResistanceA",
                            Alias = "正向权值",
                            FieldType = gviFieldType.gviFieldInt16
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "ResistanceB",
                            Alias = "反向权值",
                            FieldType = gviFieldType.gviFieldInt16
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "Length",
                            Alias = "弧长",
                            FieldType = gviFieldType.gviFieldDouble
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "Topo_Error",
                            Alias = "拓扑错误",
                            FieldType = gviFieldType.gviFieldString,
                            Length = 100
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "LaseUpdate",
                            Alias = "最后更新时间",
                            FieldType = gviFieldType.gviFieldDate
                        };
                        infos.Add(newVal);
                        newVal = new FieldInfoClass
                        {
                            Name = "Geometry",
                            Alias = "空间列",
                            RegisteredRenderIndex = true,
                            FieldType = gviFieldType.gviFieldGeometry
                        };
                        IGeometryDef def = new GeometryDefClass
                        {
                            GeometryColumnType = gviGeometryColumnType.gviGeometryColumnPolyline,
                            HasZ = true
                        };
                        newVal.GeometryDef = def;
                        infos.Add(newVal);
                        arrDBIndex = new string[] { "GroupId", "A_FacClass", "Edge", "P_FacClass", "PNode", "E_FacClass", "ENode" };
                        return infos;
                    }

            }
            return null;
        }

        public static bool IsSysField(string fieldName)
        {
            switch (fieldName)
            {
                case "oid":
                case "facilityid":
                case "groupid":
                case "styleid":
                case "modelname":
                case "metadata":
                case "geometry":
                case "shape":
                case "footprint":
                    return true;
            }
            return false;
        }
    }
}
