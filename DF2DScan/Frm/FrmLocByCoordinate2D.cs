using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DF2DControl.Base;
using System.IO;
using DFCommon.Class;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using DF2DControl.UserControl.View;
using DFWinForms.Service;

namespace DF2DScan.Frm
{
    public class FormLocByCoordinate2D : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btn_Loc;
        private TextEdit te_Y;
        private TextEdit te_X;
        private ComboBoxEdit cbe_History;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;

        private FormLocByCoordinate2D()
        {
            InitializeComponent();

            string localDataPath = Config.GetConfigValue("localDataPath");
            if (string.IsNullOrEmpty(localDataPath))
                localDataPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DIFGIS\\Data");
            if (!Directory.Exists(localDataPath))
            {
                Directory.CreateDirectory(localDataPath);
            }
            filePath = localDataPath + "\\LocByCoordHistory.txt";
        }
        public static FormLocByCoordinate2D Instance
        {
            get
            {
                if (FormLocByCoordinate2D.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (FormLocByCoordinate2D.instance == null)
                        {
                            FormLocByCoordinate2D.instance = new FormLocByCoordinate2D();
                        }
                    }
                }
                return FormLocByCoordinate2D.instance;
            }
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_Loc = new DevExpress.XtraEditors.SimpleButton();
            this.te_Y = new DevExpress.XtraEditors.TextEdit();
            this.te_X = new DevExpress.XtraEditors.TextEdit();
            this.cbe_History = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_Y.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_X.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbe_History.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_Loc);
            this.layoutControl1.Controls.Add(this.te_Y);
            this.layoutControl1.Controls.Add(this.te_X);
            this.layoutControl1.Controls.Add(this.cbe_History);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(254, 86);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_Loc
            // 
            this.btn_Loc.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Loc.Appearance.Options.UseFont = true;
            this.btn_Loc.Location = new System.Drawing.Point(2, 54);
            this.btn_Loc.Name = "btn_Loc";
            this.btn_Loc.Size = new System.Drawing.Size(250, 30);
            this.btn_Loc.StyleController = this.layoutControl1;
            this.btn_Loc.TabIndex = 7;
            this.btn_Loc.Text = "定    位";
            this.btn_Loc.Click += new System.EventHandler(this.btn_Loc_Click);
            // 
            // te_Y
            // 
            this.te_Y.Location = new System.Drawing.Point(146, 28);
            this.te_Y.Name = "te_Y";
            this.te_Y.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_Y.Size = new System.Drawing.Size(106, 22);
            this.te_Y.StyleController = this.layoutControl1;
            this.te_Y.TabIndex = 6;
            // 
            // te_X
            // 
            this.te_X.Location = new System.Drawing.Point(18, 28);
            this.te_X.Name = "te_X";
            this.te_X.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_X.Size = new System.Drawing.Size(107, 22);
            this.te_X.StyleController = this.layoutControl1;
            this.te_X.TabIndex = 5;
            // 
            // cbe_History
            // 
            this.cbe_History.Location = new System.Drawing.Point(65, 2);
            this.cbe_History.Name = "cbe_History";
            this.cbe_History.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbe_History.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbe_History.Size = new System.Drawing.Size(187, 22);
            this.cbe_History.StyleController = this.layoutControl1;
            this.cbe_History.TabIndex = 4;
            this.cbe_History.SelectedIndexChanged += new System.EventHandler(this.cbe_History_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(254, 86);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbe_History;
            this.layoutControlItem1.CustomizationFormText = "历史记录：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(254, 26);
            this.layoutControlItem1.Text = "历史记录：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.te_X;
            this.layoutControlItem2.CustomizationFormText = "X:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(127, 26);
            this.layoutControlItem2.Text = "X:";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(11, 14);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.te_Y;
            this.layoutControlItem3.CustomizationFormText = "Y:";
            this.layoutControlItem3.Location = new System.Drawing.Point(127, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(127, 26);
            this.layoutControlItem3.Text = "Y:";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(12, 14);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_Loc;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(254, 34);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // FormLocByCoordinate2D
            // 
            this.AcceptButton = this.btn_Loc;
            this.ClientSize = new System.Drawing.Size(254, 86);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(5, 180);
            this.Name = "FormLocByCoordinate2D";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "坐标定位";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormLocByCoordinate2D_FormClosed);
            this.Load += new System.EventHandler(this.FormLocByCoordinate2D_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_Y.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_X.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbe_History.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        private static FormLocByCoordinate2D instance = null;
        private static readonly object syncRoot = new object();

        private double x;
        private double y;

        private string filePath;
        IPoint pPoint = new PointClass();
       
        
        DF2DApplication app = DF2DApplication.Application;

        
        private static IEnvelope NewEnv(IPoint point,double offset)
        {
            IEnvelope env = new EnvelopeClass();
            env.XMin = point.X - offset;
            env.YMin = point.Y - offset;
            env.XMax = point.X + offset;
            env.YMax = point.Y + offset;
            return env;

        }

        
       

        private void btn_Loc_Click(object sender, EventArgs e)
        {
            if (OnLocation()) cbe_History.Properties.Items.Add(te_X.Text + "," + te_Y.Text);
        }

        private bool OnLocation()
        {
            try
            {
                if (te_X.Text != "" && te_Y.Text != "")
                {
                    x = Convert.ToDouble(te_X.Text);
                    y = Convert.ToDouble(te_Y.Text);
                }
                else
                {
                    XtraMessageBox.Show("请输入正确的坐标！");
                    return false;
                }


                pPoint.X = x;
                pPoint.Y = y;
                string strText = "X:" + pPoint.X.ToString("F2") + "\n" + "Y:" + pPoint.Y.ToString("F2");
                AddCallout(pPoint, strText);
                app.Current2DMapControl.CenterAt(pPoint);
                app.Current2DMapControl.MapScale = 500;
                app.Current2DMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                return true;
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show("请输入正确的坐标！");
                return false;
            }
           
        }

        public void MapScaleChanged()
        {
            pPoint.X = x;
            pPoint.Y = y;
            //IEnvelope env = NewEnv(pPoint, 100);
            string strText = "X:" + pPoint.X.ToString("F2") + "\n" + "Y:" + pPoint.Y.ToString("F2");
            AddCallout(pPoint, strText);
            app.Current2DMapControl.CenterAt(pPoint);
            app.Current2DMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public void AddCallout(IPoint pPoint, string strText)
        {
            IActiveView pActiveView = app.Current2DMapControl.ActiveView;
            IGraphicsContainer pGraphicsContainer = pActiveView.GraphicsContainer;
            IPoint pPointText = new PointClass();
            pPointText.PutCoords(pPoint.X + 3, pPoint.Y + 3);

            ITextElement pTextElement = new TextElementClass();
            IFormattedTextSymbol pTextSymbol = new TextSymbolClass();

            IElement pElement = pTextElement as IElement;
            pTextElement.Text = strText;
            pTextElement.ScaleText = true;
            pElement.Geometry = pPointText;

            IRgbColor pRgbColor = GetRGBColor(255, 255, 255);

            ISimpleFillSymbol pSmplFill = new SimpleFillSymbolClass();
            pSmplFill.Color = pRgbColor;
            pSmplFill.Style = esriSimpleFillStyle.esriSFSHollow;

            IBalloonCallout pBalloonCallout = new BalloonCalloutClass();
            pBalloonCallout.Symbol = pSmplFill;
            pBalloonCallout.Style = esriBalloonCalloutStyle.esriBCSRoundedRectangle;
            pBalloonCallout.AnchorPoint = pPoint;
            pBalloonCallout.LeaderTolerance = 5;

            pRgbColor = GetRGBColor(255, 0, 0);

            pTextSymbol.Background = pBalloonCallout as ITextBackground;
            pTextSymbol.Color = pRgbColor;
            //pTextSymbol.Size = (app.Current2DMapControl.MapScale/100)*5;
            pTextSymbol.Size = 25;
            pTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;

            pTextElement.Symbol = pTextSymbol;
            
      
            pGraphicsContainer.AddElement(pElement, 1);


        }

        private IRgbColor GetRGBColor(int red, int green, int blue)
        {
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = red;
            pRgbColor.Green = green;
            pRgbColor.Blue = blue;
            return pRgbColor;
        }

        private void FormLocByCoordinate2D_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
            try
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                foreach (object obj in this.cbe_History.Properties.Items)
                {
                    sw.WriteLine(obj.ToString());
                }
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void FormLocByCoordinate2D_Load(object sender, EventArgs e)
        {
            this.cbe_History.Properties.Items.Clear();
            // 加载历史记录
            try
            {
                if (File.Exists(filePath))
                {
                    StreamReader sr = new StreamReader(filePath);
                    string str = sr.ReadLine();
                    while (!string.IsNullOrEmpty(str))
                    {
                        string[] arr = str.Split(',');
                        if (arr.Length >= 2)
                        {
                            string str1 = arr[0];
                            string str2 = arr[1];
                            double tempx, tempy;
                            bool bRes1 = double.TryParse(str1, out tempx);
                            bool bRes2 = double.TryParse(str2, out tempy);
                            if (bRes1 && bRes2)
                            {
                                this.cbe_History.Properties.Items.Add(str1 + "," + str2);
                            }
                        }
                        str = sr.ReadLine();
                    }
                    sr.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cbe_History_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = this.cbe_History.Text;
            string[] arr = str.Split(',');
            string str1 = arr[0];
            string str2 = arr[1];
            this.te_X.Text = str1;
            this.te_Y.Text = str2;
            OnLocation();
        }
     


        
    }
}
