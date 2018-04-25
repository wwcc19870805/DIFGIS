using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using System.Drawing;
using System.Data;
using System.IO;
using Gvitech.CityMaker.Resource;
using DFDataConfig.Class;
using DFCommon.Class;

namespace DF3DPipeCreateTool.Class
{
    public class PipeLineStyleClass:FacStyleClass
    {
        private uint[] _colorArray;            // 颜色列表 1外壁+2内壁
        private string[] _textureArray;
        private HeightMode _heightMode;        // 高程类型
        private HeightParam _heightParam;      // 高程参数
        private double _pipeThick;             // 管壁厚度
        private RenderType _renderType;        // 渲染方式
        private string _textureInside;       // 管线内壁
        private string _textureOutside;      // 管线外壁 
        // Properties
        public string[] TextureArray
        {
            get
            {
                return this._textureArray;
            }
            set
            {
                this._textureArray = value;
            }
        }

        public uint[] ColorArray
        {
            get
            {
                return this._colorArray;
            }
            set
            {
                this._colorArray = value;
            }
        }

        public HeightMode HeightMode
        {
            get
            {
                return this._heightMode;
            }
            set
            {
                this._heightMode = value;
            }
        }

        public HeightParam HeightParam
        {
            get
            {
                return this._heightParam;
            }
            set
            {
                this._heightParam = value;
            }
        }

        public double PipeThick
        {
            get
            {
                return this._pipeThick;
            }
            set
            {
                this._pipeThick = value;
            }
        }

        public RenderType RenderType
        {
            get
            {
                return this._renderType;
            }
            set
            {
                this._renderType = value;
            }
        }

        public string TextureInside
        {
            get
            {
                return this._textureInside;
            }
            set
            {
                this._textureInside = value;
            }
        }

        public string TextureOutside
        {
            get
            {
                return this._textureOutside;
            }
            set
            {
                this._textureOutside = value;
            }
        }
        private ColorClass GetColorClass(string objectId)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IDataSource ds = DF3DPipeCreateApp.App.TemplateLib;
                if (ds == null) return null;
                IFeatureDataSet fds = ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_ColorInfo");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "ObjectId = '" + objectId + "'"
                };
                cursor = oc.Search(filter, true);
                if ((row = cursor.NextRow()) != null)
                {
                    int id = -1;
                    string name = "", objectid = "", groupid = "", code = "", comment = "";
                    Image thumbnail = null;
                    int index = row.FieldIndex("oid");
                    if (index != -1 && !row.IsNull(index))
                    {
                        id = Convert.ToInt32(row.GetValue(index).ToString());
                    }
                    index = row.FieldIndex("Name");
                    if (index != -1 && !row.IsNull(index))
                    {
                        name = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("ObjectId");
                    if (index != -1 && !row.IsNull(index))
                    {
                        objectid = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("GroupId");
                    if (index != -1 && !row.IsNull(index))
                    {
                        groupid = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("Code");
                    if (index != -1 && !row.IsNull(index))
                    {
                        code = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("Comment");
                    if (index != -1 && !row.IsNull(index))
                    {
                        comment = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("Thumbnail");
                    if (index != -1 && !row.IsNull(index))
                    {
                        IBinaryBuffer b = row.GetValue(index) as IBinaryBuffer;
                        if (row != null)
                        {
                            MemoryStream stream = new MemoryStream(b.AsByteArray());
                            thumbnail = Image.FromStream(stream);
                        }
                    }
                    if (id != -1 && thumbnail != null)
                    {
                        ColorClass cc = new ColorClass();
                        cc.Id = id; cc.Name = name; cc.Group = groupid;
                        cc.ObjectId = objectid; cc.Code = code; cc.Comment = comment;
                        cc.Thumbnail = thumbnail;
                        return cc;
                    }
                }
                return null;
            }
            catch (Exception exception)
            {
                return null;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
        private TextureClass GetTextureClass(string objectId)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IDataSource ds = DF3DPipeCreateApp.App.TemplateLib;
                if (ds == null) return null;
                IFeatureDataSet fds = ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_TextureInfo");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "ObjectId = '" + objectId + "'"
                };
                cursor = oc.Search(filter, true);
                if ((row = cursor.NextRow()) != null)
                {
                    int id = -1;
                    string name = "", objectid = "", groupid = "", code = "", comment = "";
                    Image thumbnail = null;
                    int index = row.FieldIndex("oid");
                    if (index != -1 && !row.IsNull(index))
                    {
                        id = Convert.ToInt32(row.GetValue(index).ToString());
                    }
                    index = row.FieldIndex("Name");
                    if (index != -1 && !row.IsNull(index))
                    {
                        name = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("ObjectId");
                    if (index != -1 && !row.IsNull(index))
                    {
                        objectid = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("GroupId");
                    if (index != -1 && !row.IsNull(index))
                    {
                        groupid = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("Code");
                    if (index != -1 && !row.IsNull(index))
                    {
                        code = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("Comment");
                    if (index != -1 && !row.IsNull(index))
                    {
                        comment = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("Thumbnail");
                    if (index != -1 && !row.IsNull(index))
                    {
                        IBinaryBuffer b = row.GetValue(index) as IBinaryBuffer;
                        if (row != null)
                        {
                            MemoryStream stream = new MemoryStream(b.AsByteArray());
                            thumbnail = Image.FromStream(stream);
                        }
                    }
                    if (id != -1 && thumbnail != null)
                    {
                        TextureClass cc = new TextureClass();
                        cc.Id = id; cc.Name = name; cc.Group = groupid;
                        cc.ObjectId = objectid; cc.Code = code; cc.Comment = comment;
                        cc.Thumbnail = thumbnail;
                        return cc;
                    }
                }
                return null;
            }
            catch (Exception exception)
            {
                return null;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
        private uint ColorToAbgr(Color c)
        {
            int num = c.R << 0x10;
            int num2 = c.G << 8;
            int b = c.B;
            int num4 = ((c.A == null) ? 0xff : c.A) << 0x18;
            return (uint)(((b | num2) | num) | num4);
        }
        public DataTable GetRenderInfo()
        {
            DataTable table = null;
            DataRow row = null;
            table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Thumbnail", typeof(object));
            table.Columns.Add("Comment", typeof(string));
            table.Columns.Add("Info", typeof(object));
            try
            {
                if (this._renderType == RenderType.Color)
                {
                    ColorClass colorClass = null;
                    if (this._textureOutside != null)
                    {
                        colorClass = GetColorClass(this._textureOutside.ToString());
                        if (colorClass != null)
                        {
                            row = table.NewRow();
                            row["Name"] = "外壁";
                            row["Thumbnail"] = colorClass.Thumbnail;
                            row["Comment"] = colorClass.Name;
                            row["Info"] = colorClass.ObjectId;
                            table.Rows.Add(row);
                        }
                        else
                        {
                            row = table.NewRow();
                            row["Name"] = "外壁";
                            row["Thumbnail"] = null;
                            row["Comment"] = "";
                            row["Info"] = null;
                            table.Rows.Add(row);
                        }
                    }
                    if (this._textureInside != null)
                    {
                        colorClass = GetColorClass(this._textureInside.ToString());
                        if (colorClass != null)
                        {
                            row = table.NewRow();
                            row["Name"] = "内壁";
                            row["Thumbnail"] = colorClass.Thumbnail;
                            row["Comment"] = colorClass.Name;
                            row["Info"] = colorClass.ObjectId;
                            table.Rows.Add(row);
                            return table;
                        }
                        row = table.NewRow();
                        row["Name"] = "内壁";
                        row["Thumbnail"] = null;
                        row["Comment"] = "";
                        row["Info"] = null;
                        table.Rows.Add(row);
                    }
                    return table;
                }
                TextureClass textureClass = null;
                if (this._textureOutside != null)
                {
                    textureClass = GetTextureClass(this._textureOutside.ToString());
                    if (textureClass != null)
                    {
                        row = table.NewRow();
                        row["Name"] = "外壁";
                        row["Thumbnail"] = textureClass.Thumbnail;
                        row["Comment"] = textureClass.Name;
                        row["Info"] = textureClass.ObjectId;
                        table.Rows.Add(row);
                    }
                    else
                    {
                        row = table.NewRow();
                        row["Name"] = "外壁";
                        row["Thumbnail"] = null;
                        row["Comment"] = "";
                        row["Info"] = null;
                        table.Rows.Add(row);
                    }
                }
                if (this._textureInside == null)
                {
                    return table;
                }
                textureClass = GetTextureClass(this._textureInside.ToString());
                if (textureClass != null)
                {
                    row = table.NewRow();
                    row["Name"] = "内壁";
                    row["Thumbnail"] = textureClass.Thumbnail;
                    row["Comment"] = textureClass.Name;
                    row["Info"] = textureClass.ObjectId;
                    table.Rows.Add(row);
                    return table;
                }
                row = table.NewRow();
                row["Name"] = "内壁";
                row["Thumbnail"] = null;
                row["Comment"] = "";
                row["Info"] = null;
                table.Rows.Add(row);
            }
            catch (Exception exception)
            {
                table.Rows.Clear();
                row = table.NewRow();
                row["Name"] = "外壁";
                row["Thumbnail"] = null;
                row["Comment"] = "外壁";
                row["Info"] = null;
                table.Rows.Add(row);
                row = table.NewRow();
                row["Name"] = "内壁";
                row["Thumbnail"] = null;
                row["Comment"] = "内壁";
                row["Info"] = null;
                table.Rows.Add(row);
            }
            return table;
        }

        public PipeLineStyleClass()
        {
            this._type = StyleType.PipeLineStyle;
        }

        public PipeLineStyleClass(Dictionary<string, string> dictionary)
        {
            this._type = StyleType.PipeLineStyle;
            if (dictionary != null)
            {
                if (dictionary.ContainsKey("RenderType") && !string.IsNullOrEmpty(dictionary["RenderType"]))
                {
                    Enum.TryParse<RenderType>(dictionary["RenderType"], out this._renderType);
                }
                if (dictionary.ContainsKey("TextureInside") && !string.IsNullOrEmpty(dictionary["TextureInside"]))
                {
                    this._textureInside = dictionary["TextureInside"];
                }
                if (dictionary.ContainsKey("TextureOutside") && !string.IsNullOrEmpty(dictionary["TextureOutside"]))
                {
                    this._textureOutside = dictionary["TextureOutside"];
                }
                if (dictionary.ContainsKey("PipeThick") && !string.IsNullOrEmpty(dictionary["PipeThick"]))
                {
                    this._pipeThick = double.Parse(dictionary["PipeThick"]);
                }
                if (dictionary.ContainsKey("HeightMode") && !string.IsNullOrEmpty(dictionary["HeightMode"]))
                {
                    Enum.TryParse<HeightMode>(dictionary["HeightMode"], out this._heightMode);
                }
                if (dictionary.ContainsKey("HeightParam") && !string.IsNullOrEmpty(dictionary["HeightParam"]))
                {
                    Enum.TryParse<HeightParam>(dictionary["HeightParam"], out this._heightParam);
                }
            }
        }

        public override Gvitech.CityMaker.Common.IBinaryBuffer ObjectToJson()
        {
            IBinaryBuffer buffer = null;
            try
            {
                buffer = new BinaryBufferClass();
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("RenderType", this._renderType.ToString());
                item.Add("TextureInside", this._textureInside);
                item.Add("TextureOutside", this._textureOutside);
                item.Add("PipeThick", this._pipeThick.ToString());
                item.Add("HeightMode", this._heightMode.ToString());
                item.Add("HeightParam", this._heightParam.ToString());
                string stringValue = JsonTool.ObjectToJson(item);
                buffer.FromString(stringValue);
                return buffer;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        
        public override bool InitResource()
        {
            if (this._isInit) return true;
            if (DF3DPipeCreateApp.App.TemplateLib == null)
            {
                this._isInit = false;
                return false;
            }
            try
            {
                if (this._renderType == RenderType.Color)
                {
                    ColorClass colorClass = null;
                    this._colorArray = new uint[3];
                    if (this._textureOutside != null)
                    {
                        colorClass = GetColorClass(this._textureOutside.ToString());
                        if (colorClass != null)
                        {
                            this._colorArray[0] = ColorToAbgr(colorClass.Color);
                        }
                    }
                    if (this._textureInside != null)
                    {
                        colorClass = GetColorClass(this._textureInside.ToString());
                        if (colorClass != null)
                        {
                            this._colorArray[1] = ColorToAbgr(colorClass.Color);
                        }
                    }
                }
                else
                {
                    this._textureArray = new string[3];
                    base._images = new PropertySetClass();
                    IImage image = null;
                    IDataSource ds = DF3DPipeCreateApp.App.TemplateLib;
                    if (ds != null)
                    {
                        IFeatureDataSet fds = ds.OpenFeatureDataset("DataSet_BIZ");
                        if (fds != null)
                        {
                            IResourceManager res = fds as IResourceManager;
                            if (this._textureOutside != null)
                            {
                                this._textureArray[0] = this._textureOutside.ToString();
                                if ((image = res.GetImage(this._textureArray[0]))!= null)
                                {
                                    base._images.SetProperty(this._textureArray[0], image);
                                }
                            }
                            if (this._textureInside != null)
                            {
                                this._textureArray[1] = this._textureInside.ToString();
                                if ((image = res.GetImage(this._textureArray[1])) != null)
                                {
                                    base._images.SetProperty(this._textureArray[1], image);
                                }
                            }
                        }
                    }
                }
                base._isInit = true;
                return true;
            }
            catch (Exception exception)
            {
                base._isInit = false;
                return false;
            }
        }
    }
}
