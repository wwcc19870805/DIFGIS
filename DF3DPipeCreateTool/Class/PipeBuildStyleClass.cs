using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.Common;
using System.Data;
using Gvitech.CityMaker.FdeCore;
using System.Drawing;
using System.IO;
using Gvitech.CityMaker.Resource;
using DFDataConfig.Class;
using DFCommon.Class;

namespace DF3DPipeCreateTool.Class
{
    public class PipeBuildStyleClass:FacStyleClass
    {
        private RenderType _renderType;
        private string _textureFacade;
        private string _textureRoof;
        private uint[] _colorArray;
        private string[] _textureArray;

        // Properties
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

        public string TextureFacade
        {
            get
            {
                return this._textureFacade;
            }
            set
            {
                this._textureFacade = value;
            }
        }

        public string TextureRoof
        {
            get
            {
                return this._textureRoof;
            }
            set
            {
                this._textureRoof = value;
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

        public PipeBuildStyleClass()
        {
            this._type = StyleType.PipeBuildStyle;
        }

        public PipeBuildStyleClass(Dictionary<string, string> dictionary)
        {
            this._type = StyleType.PipeBuildStyle;
            if (dictionary != null)
            {
                if (dictionary.ContainsKey("RenderType") && !string.IsNullOrEmpty(dictionary["RenderType"]))
                {
                    Enum.TryParse<RenderType>(dictionary["RenderType"], out this._renderType);
                }
                if (dictionary.ContainsKey("TextureRoof") && !string.IsNullOrEmpty(dictionary["TextureRoof"]))
                {
                    this._textureRoof = dictionary["TextureRoof"];
                }
                if (dictionary.ContainsKey("TextureFacade") && !string.IsNullOrEmpty(dictionary["TextureFacade"]))
                {
                    this._textureFacade = dictionary["TextureFacade"];
                }
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
                if(ds == null) return null;
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
                    if (this._textureRoof != null)
                    {
                        colorClass = GetColorClass(this._textureRoof.ToString());
                        if (colorClass != null)
                        {
                            row = table.NewRow();
                            row["Name"] = "顶面";
                            row["Thumbnail"] = colorClass.Thumbnail;
                            row["Comment"] = colorClass.Name;
                            row["Info"] = colorClass.ObjectId;
                            table.Rows.Add(row);
                        }
                        else
                        {
                            row = table.NewRow();
                            row["Name"] = "顶面";
                            row["Thumbnail"] = null;
                            row["Comment"] = "";
                            row["Info"] = null;
                            table.Rows.Add(row);
                        }
                    }
                    if (this._textureFacade != null)
                    {
                        colorClass = GetColorClass(this._textureFacade.ToString());
                        if (colorClass != null)
                        {
                            row = table.NewRow();
                            row["Name"] = "侧面";
                            row["Thumbnail"] = colorClass.Thumbnail;
                            row["Comment"] = colorClass.Name;
                            row["Info"] = colorClass.ObjectId;
                            table.Rows.Add(row);
                            return table;
                        }
                        row = table.NewRow();
                        row["Name"] = "侧面";
                        row["Thumbnail"] = null;
                        row["Comment"] = "";
                        row["Info"] = null;
                        table.Rows.Add(row);
                    }
                    return table;
                }
                TextureClass textureClass = null;
                if (this._textureRoof != null)
                {
                    textureClass = GetTextureClass(this._textureRoof.ToString());
                    if (textureClass != null)
                    {
                        row = table.NewRow();
                        row["Name"] = "顶面";
                        row["Thumbnail"] = textureClass.Thumbnail;
                        row["Comment"] = textureClass.Name;
                        row["Info"] = textureClass.ObjectId;
                        table.Rows.Add(row);
                    }
                    else
                    {
                        row = table.NewRow();
                        row["Name"] = "顶面";
                        row["Thumbnail"] = null;
                        row["Comment"] = "";
                        row["Info"] = null;
                        table.Rows.Add(row);
                    }
                }
                if (this._textureFacade == null)
                {
                    return table;
                }
                textureClass = GetTextureClass(this._textureFacade.ToString());
                if (textureClass != null)
                {
                    row = table.NewRow();
                    row["Name"] = "侧面";
                    row["Thumbnail"] = textureClass.Thumbnail;
                    row["Comment"] = textureClass.Name;
                    row["Info"] = textureClass.ObjectId;
                    table.Rows.Add(row);
                    return table;
                }
                row = table.NewRow();
                row["Name"] = "侧面";
                row["Thumbnail"] = null;
                row["Comment"] = "";
                row["Info"] = null;
                table.Rows.Add(row);
            }
            catch (Exception exception)
            {
                table.Rows.Clear();
                row = table.NewRow();
                row["Name"] = "顶面";
                row["Thumbnail"] = null;
                row["Comment"] = "顶面";
                row["Info"] = null;
                table.Rows.Add(row);
                row = table.NewRow();
                row["Name"] = "侧面";
                row["Thumbnail"] = null;
                row["Comment"] = "侧面";
                row["Info"] = null;
                table.Rows.Add(row);
            }
            return table;
        }

        public override Gvitech.CityMaker.Common.IBinaryBuffer ObjectToJson()
        {
            IBinaryBuffer buffer = null;
            try
            {
                buffer = new BinaryBufferClass();
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("RenderType", this._renderType.ToString());
                item.Add("TextureRoof", this._textureRoof);
                item.Add("TextureFacade", this._textureFacade);
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
                    if (this._textureRoof != null)
                    {
                        colorClass = GetColorClass(this._textureRoof.ToString());
                        if (colorClass != null)
                        {
                            this._colorArray[0] = ColorToAbgr(colorClass.Color);
                        }
                    }
                    if (this._textureFacade != null)
                    {
                        colorClass = GetColorClass(this._textureFacade.ToString());
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
                            if (this._textureRoof != null)
                            {
                                this._textureArray[0] = this._textureRoof.ToString();
                                if ((image = res.GetImage(this._textureArray[0])) != null)
                                {
                                    base._images.SetProperty(this._textureArray[0], image);
                                }
                            }
                            if (this._textureFacade != null)
                            {
                                this._textureArray[1] = this._textureFacade.ToString();
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
