using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Class;
using DFCommon.Class;

namespace DF3DPipeCreateTool.Class
{
    public class DF3DPipeCreateApp
    {
        private static DF3DPipeCreateApp app = null;
        private static readonly object syncRoot = new object();

        private IDataSource _PipeLib;
        private IDataSource _TemplateLib;
        private IDataSource _TempLib;
        public IDataSource PipeLib
        {
            get { return this._PipeLib; }
            set { this._PipeLib = value; }
        }
        public IDataSource TemplateLib
        {
            get { return this._TemplateLib; }
            set { this._TemplateLib = value; }
        }
        public IDataSource TempLib
        {
            get { return this._TempLib; }
            set { this._TempLib = value; }
        }

        private DF3DPipeCreateApp()
        {
            try
            {
                IConnectionInfo connInfo1 = new ConnectionInfo();
                connInfo1.FromConnectionString(Config.GetConfigValue("3DTemplateDataConnStr"));
                IDataSourceFactory dsf1 = new DataSourceFactory();
                if (dsf1.HasDataSource(connInfo1))
                {
                    this._TemplateLib = dsf1.OpenDataSource(connInfo1);
                }

                IConnectionInfo connInfo2 = new ConnectionInfo();
                connInfo2.FromConnectionString(Config.GetConfigValue("3DPipeDataConnStr"));
                IDataSourceFactory dsf2 = new DataSourceFactory();
                if (dsf2.HasDataSource(connInfo2))
                {
                    this._PipeLib = dsf2.OpenDataSource(connInfo2);
                }

                IConnectionInfo connInfo3 = new ConnectionInfo();
                connInfo3.FromConnectionString(Config.GetConfigValue("3DTempDataConnStr"));
                IDataSourceFactory dsf3 = new DataSourceFactory();
                if (dsf3.HasDataSource(connInfo3))
                {
                    this._TempLib = dsf3.OpenDataSource(connInfo3);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static DF3DPipeCreateApp App
        {
            get
            {
                if (DF3DPipeCreateApp.app == null)
                {
                    lock (syncRoot)
                    {
                        if (DF3DPipeCreateApp.app == null)
                        {
                            DF3DPipeCreateApp.app = new DF3DPipeCreateApp();
                        }
                    }
                }
                return DF3DPipeCreateApp.app;
            }
        }

    }
}
