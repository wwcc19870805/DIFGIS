using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Base;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.RenderControl;
using ICSharpCode.Core;
using Gvitech.CityMaker.Controls;
using DFCommon.Class;
using System.IO;
using DevExpress.XtraSplashScreen;

namespace DF3DControl.Command
{
    public class Init3DControlCommand : AbstractMap3DCommand
    {
        public override void  Run(object sender, EventArgs e)
        {
            DF3DApplication app = (DF3DApplication)this.Hook;
            if (app == null || app.Current3DMapControl == null) return;
            try
            {
                //初始化
                SplashScreenManager.Default.SendCommand(null, "开始初始化三维控件......");

                AxRenderControl d3 = app.Current3DMapControl;
                IPropertySet ps = new PropertySet();
                ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
                if (!d3.Initialize(true, ps))
                {
                    app.IsInit3DControl = false;
                    return;
                }
                app.IsInit3DControl = true;
                double flyTime = 2.0;
                bool bRes = double.TryParse(Config.GetConfigValue("FlyTime"), out flyTime);
                if (bRes) d3.Camera.FlyTime = flyTime;
                else d3.Camera.FlyTime = 2.0;
                //d3.Camera.AutoClipPlane = false;
                d3.Camera.NearClipPlane = 0.01f;
                d3.Camera.FarClipPlane = 999999.0f;
                d3.CacheManager.FileCacheEnabled = true;
                d3.CacheManager.FileCachePath = "D:\\3DCache";
                d3.CacheManager.MemoryCacheEnabled = true; 
                //d3.Viewport.LogoVisible = false;

                // 设置天空盒
                string tmpSkyboxPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\Resource\Skybox");
                for (int i = 0; i < 4; i++)
                {
                    ISkyBox skybox = d3.ObjectManager.GetSkyBox(i);
                    skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\13_BK.jpg");
                    skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\13_DN.jpg");
                    skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\13_FR.jpg");
                    skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\13_LF.jpg");
                    skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\13_RT.jpg");
                    skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\13_UP.jpg");
                }
            }
            catch (Exception ex)
            {
                LoggingService.Error("3D Control Init Failed!" + ex.Message);
            }
        }
    }
}
