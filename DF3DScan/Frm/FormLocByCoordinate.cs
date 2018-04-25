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
using DF3DControl.Base;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using System.IO;
using Gvitech.CityMaker.FdeGeometry;
using DFCommon.Class;

namespace DF3DScan.Frm
{
    public class FormLocByCoordinate : XtraForm
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

        private FormLocByCoordinate()
        {
            InitializeComponent();
            if (DF3DApplication.Application != null) d3 = DF3DApplication.Application.Current3DMapControl;
            listRender = new List<IRenderable>();
            string localDataPath = SystemInfo.Instance.LocalDataPath;
            filePath = localDataPath + "\\LocBy3DCoordHistory.txt";
        }
        public static FormLocByCoordinate Instance
        {
            get
            {
                if (FormLocByCoordinate.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (FormLocByCoordinate.instance == null)
                        {
                            FormLocByCoordinate.instance = new FormLocByCoordinate();
                        }
                    }
                }
                return FormLocByCoordinate.instance;
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
            this.layoutControl1.Size = new System.Drawing.Size(254, 98);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_Loc
            // 
            this.btn_Loc.Location = new System.Drawing.Point(12, 64);
            this.btn_Loc.Name = "btn_Loc";
            this.btn_Loc.Size = new System.Drawing.Size(230, 22);
            this.btn_Loc.StyleController = this.layoutControl1;
            this.btn_Loc.TabIndex = 3;
            this.btn_Loc.Text = "定    位";
            this.btn_Loc.Click += new System.EventHandler(this.btn_Loc_Click);
            // 
            // te_Y
            // 
            this.te_Y.Location = new System.Drawing.Point(146, 38);
            this.te_Y.Name = "te_Y";
            this.te_Y.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_Y.Size = new System.Drawing.Size(96, 22);
            this.te_Y.StyleController = this.layoutControl1;
            this.te_Y.TabIndex = 2;
            // 
            // te_X
            // 
            this.te_X.Location = new System.Drawing.Point(28, 38);
            this.te_X.Name = "te_X";
            this.te_X.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_X.Size = new System.Drawing.Size(97, 22);
            this.te_X.StyleController = this.layoutControl1;
            this.te_X.TabIndex = 1;
            // 
            // cbe_History
            // 
            this.cbe_History.Location = new System.Drawing.Point(75, 12);
            this.cbe_History.Name = "cbe_History";
            this.cbe_History.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbe_History.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbe_History.Size = new System.Drawing.Size(167, 22);
            this.cbe_History.StyleController = this.layoutControl1;
            this.cbe_History.TabIndex = 0;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(254, 98);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbe_History;
            this.layoutControlItem1.CustomizationFormText = "历史记录：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(234, 26);
            this.layoutControlItem1.Text = "历史记录：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.te_X;
            this.layoutControlItem2.CustomizationFormText = "X:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(117, 26);
            this.layoutControlItem2.Text = "X:";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(11, 14);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.te_Y;
            this.layoutControlItem3.CustomizationFormText = "Y:";
            this.layoutControlItem3.Location = new System.Drawing.Point(117, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(117, 26);
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
            this.layoutControlItem4.Size = new System.Drawing.Size(234, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // FormLocByCoordinate
            // 
            this.AcceptButton = this.btn_Loc;
            this.ClientSize = new System.Drawing.Size(254, 98);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(5, 180);
            this.Name = "FormLocByCoordinate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "坐标定位";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormLocByCoordinate_FormClosed);
            this.Load += new System.EventHandler(this.FormLocByCoordinate_Load);
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

        private static FormLocByCoordinate instance = null;
        private static readonly object syncRoot = new object();
        private AxRenderControl d3;
        private double x;
        private double y;
        private List<IRenderable> listRender;
        private string filePath;

        private void FormLocByCoordinate_Load(object sender, EventArgs e)
        {
            this.cbe_History.Properties.Items.Clear();
            IVector3 vect = null;
            IEulerAngle angle = null;
            d3.Camera.GetCamera(out vect, out angle);
            this.te_X.Text = vect.X.ToString("0.00");
            this.te_Y.Text = vect.Y.ToString("0.00");
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
            catch(Exception ex)
            {

            }
        }

        private bool OnLocation()
        {
            if (d3 == null) return false;
            bool bX = double.TryParse(this.te_X.Text, out x);
            bool bY = double.TryParse(this.te_Y.Text, out y);
            if (!bX || !bY) return false;
            IVector3 vect = null;
            IEulerAngle angle = null;
            d3.Camera.GetCamera(out vect, out angle);
            if (d3.Terrain.IsRegistered)
            {
                double z = d3.Terrain.GetElevation(x, y, Gvitech.CityMaker.RenderControl.gviGetElevationType.gviGetElevationFromDatabase);
                vect.Set(x, y, z);
            }
            else vect.Set(x, y, vect.Z);

            IImagePointSymbol imagePointSymbol = new ImagePointSymbolClass();
            imagePointSymbol.ImageName = Path.Combine(Application.StartupPath, "..\\Resource\\Images\\POI\\Location.png");
            imagePointSymbol.Size = SystemInfo.Instance.SymbolSize;
            imagePointSymbol.Alignment = gviPivotAlignment.gviPivotAlignBottomCenter;
            IPoint pt = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            pt.SetCoords(vect.X, vect.Y, vect.Z, 0, 0);
            IRenderPoint renderPoint = d3.ObjectManager.CreateRenderPoint(pt, imagePointSymbol, d3.ProjectTree.RootID);
            renderPoint.MaxVisibleDistance = 10000000000.0;

            ITextSymbol textSymbol = new TextSymbolClass();
            textSymbol.TextAttribute = new TextAttributeClass
            {
                TextColor = Convert.ToUInt32(SystemInfo.Instance.TextColor, 16),
                TextSize = SystemInfo.Instance.TextSize
            };
            textSymbol.PivotAlignment = gviPivotAlignment.gviPivotAlignTopCenter;
            ILabel label = d3.ObjectManager.CreateLabel(d3.ProjectTree.RootID);
            label.Position = pt;
            label.MaxVisibleDistance = 10000000000.0;
            label.TextSymbol = textSymbol;
            label.Text = this.te_X.Text + "," + this.te_Y.Text;
            this.listRender.Add(renderPoint);
            this.listRender.Add(label);

            d3.Camera.LookAt(vect, 500, angle);
            return true;
        }

        private void btn_Loc_Click(object sender, EventArgs e)
        {
            if (OnLocation())
            this.cbe_History.Properties.Items.Add(this.te_X.Text + "," + this.te_Y.Text);
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
        private void FormLocByCoordinate_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (IRenderable r in listRender)
            {
                d3.ObjectManager.DeleteObject(r.Guid);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(r);
            }

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
            instance = null;
        }

    }
}
