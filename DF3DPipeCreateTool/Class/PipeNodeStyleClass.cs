using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Class;
using DFCommon.Class;

namespace DF3DPipeCreateTool.Class
{
    public class PipeNodeStyleClass : FacStyleClass
    {
        private string _modelId;
        private bool _isBlendPipe;
        private bool _isFollowDia;
        private bool _isFollowDir;
        private bool _isFollowSurfH;
        private bool _isRotateZ;
        private bool _isStretchZ;
        private IModel _smodel;
        private IModel _fmodel;
        private bool _isModelReuse;

        public string ModelId
        {
            get
            {
                return this._modelId;
            }
            set
            {
                this._modelId = value;
            }
        }

        public bool IsBlendPipe
        {
            get
            {
                return this._isBlendPipe;
            }
            set
            {
                this._isBlendPipe = value;
            }
        }

        public bool IsFollowDia
        {
            get
            {
                return this._isFollowDia;
            }
            set
            {
                this._isFollowDia = value;
            }
        }

        public bool IsFollowDir
        {
            get
            {
                return this._isFollowDir;
            }
            set
            {
                this._isFollowDir = value;
            }
        }

        public bool IsFollowSurfH
        {
            get
            {
                return this._isFollowSurfH;
            }
            set
            {
                this._isFollowSurfH = value;
            }
        }

        public bool IsRotateZ
        {
            get
            {
                return this._isRotateZ;
            }
            set
            {
                this._isRotateZ = value;
            }
        }

        public bool IsStretchZ
        {
            get
            {
                return this._isStretchZ;
            }
            set
            {
                this._isStretchZ = value;
            }
        }

        public IModel FineModel
        {
            get
            {
                return this._fmodel;
            }
            set
            {
                this._fmodel = value;
            }
        }

        public IModel SimpleModel
        {
            get
            {
                return this._smodel;
            }
            set
            {
                this._smodel = value;
            }
        }

        public PipeNodeStyleClass()
        {
            this._type = StyleType.PipeNodeStyle;

        }
        public PipeNodeStyleClass(Dictionary<string, string> dictionary)
        {
            this._type = StyleType.PipeNodeStyle;
            if (dictionary != null)
            {
                if (dictionary.ContainsKey("ModelId") && !string.IsNullOrEmpty(dictionary["ModelId"]))
                {
                    this._modelId = dictionary["ModelId"];
                }
                if (dictionary.ContainsKey("IsFollowDir") && !string.IsNullOrEmpty(dictionary["IsFollowDir"]))
                {
                    this._isFollowDir = bool.Parse(dictionary["IsFollowDir"]);
                }
                if (dictionary.ContainsKey("IsFollowSurfH") && !string.IsNullOrEmpty(dictionary["IsFollowSurfH"]))
                {
                    this._isFollowSurfH = bool.Parse(dictionary["IsFollowSurfH"]);
                }
                if (dictionary.ContainsKey("IsFollowDia") && !string.IsNullOrEmpty(dictionary["IsFollowDia"]))
                {
                    this._isFollowDia = bool.Parse(dictionary["IsFollowDia"]);
                }
                if (dictionary.ContainsKey("IsStretchZ") && !string.IsNullOrEmpty(dictionary["IsStretchZ"]))
                {
                    this._isStretchZ = bool.Parse(dictionary["IsStretchZ"]);
                }
                if (dictionary.ContainsKey("IsRotateZ") && !string.IsNullOrEmpty(dictionary["IsRotateZ"]))
                {
                    this._isRotateZ = bool.Parse(dictionary["IsRotateZ"]);
                }
                if (dictionary.ContainsKey("IsBlendPipe") && !string.IsNullOrEmpty(dictionary["IsBlendPipe"]))
                {
                    this._isBlendPipe = bool.Parse(dictionary["IsBlendPipe"]);
                    if (this._isBlendPipe)
                    {
                        this._isModelReuse = false;
                    }
                    else
                    {
                        this._isModelReuse = true;
                    }
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
                item.Add("ModelId", this._modelId);
                item.Add("IsFollowDir", this._isFollowDir.ToString());
                item.Add("IsFollowSurfH", this._isFollowSurfH.ToString());
                item.Add("IsFollowDia", this._isFollowDia.ToString());
                item.Add("IsStretchZ", this._isStretchZ.ToString());
                item.Add("IsBlendPipe", this._isBlendPipe.ToString());
                item.Add("IsRotateZ", this._isRotateZ.ToString());
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
                IDataSource ds = DF3DPipeCreateApp.App.TemplateLib;
                if (ds != null)
                {
                    IFeatureDataSet fds = ds.OpenFeatureDataset("DataSet_BIZ");
                    if (fds != null)
                    {
                        IResourceManager res = fds as IResourceManager;
                        this._fmodel = res.GetModel(this._modelId.ToString());
                        this._smodel = res.GetSimplifiedModel(this._modelId.ToString());
                        List<string> list = new List<string>();
                        if (this._fmodel != null)
                        {
                            string[] imageNames = this._fmodel.GetImageNames();
                            if ((imageNames != null) && (imageNames.Length > 0))
                            {
                                foreach (string str in imageNames)
                                {
                                    if (!list.Contains(str))
                                    {
                                        list.Add(str);
                                    }
                                }
                            }
                        }
                        if (this._smodel != null)
                        {
                            string[] strArray2 = this._smodel.GetImageNames();
                            if ((strArray2 != null) && (strArray2.Length > 0))
                            {
                                foreach (string str2 in strArray2)
                                {
                                    if (!list.Contains(str2))
                                    {
                                        list.Add(str2);
                                    }
                                }
                            }
                        }
                        if (list.Count > 0)
                        {
                            this._images = new PropertySetClass();
                            foreach (string str3 in list)
                            {
                                IImage image = res.GetImage(str3);
                                this._images.SetProperty(str3, image);
                            }
                        }
                    }
                }
                this._isInit = true;
                return true;
            }
            catch (Exception ex)
            {
                this._isInit = false;
                return false;
            }
        }
    }
}
