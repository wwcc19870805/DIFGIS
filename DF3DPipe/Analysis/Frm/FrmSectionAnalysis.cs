using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing.Drawing2D;
using DF3DPipe.Analysis.Class;
using Gvitech.CityMaker.FdeGeometry;

namespace DF3DPipe.Analysis.Frm
{
    public class FrmSectionAnalysis : XtraForm
    {
        private DevExpress.XtraBars.BarManager barManager1;
        private IContainer components;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiRefresh;
        private DevExpress.XtraBars.BarButtonItem bbiZoomOut;
        private DevExpress.XtraBars.BarButtonItem bbiZoomIn;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Panel panel1;
        private PictureBox pictureBox1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSectionAnalysis));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.bbiZoomOut = new DevExpress.XtraBars.BarButtonItem();
            this.bbiZoomIn = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panel1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1004, 504);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 500);
            this.panel1.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1000, 500);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1004, 504);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panel1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1004, 504);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiZoomOut,
            this.bbiZoomIn,
            this.bbiRefresh,
            this.bbiSave});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 4;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiZoomOut),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiZoomIn)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.AutoPopupMode = DevExpress.XtraBars.BarAutoPopupMode.None;
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "保存";
            this.bbiSave.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiSave.Glyph")));
            this.bbiSave.Id = 3;
            this.bbiSave.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiSave.LargeGlyph")));
            this.bbiSave.Name = "bbiSave";
            this.bbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSave_ItemClick);
            // 
            // bbiRefresh
            // 
            this.bbiRefresh.Caption = "刷新";
            this.bbiRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiRefresh.Glyph")));
            this.bbiRefresh.Id = 2;
            this.bbiRefresh.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiRefresh.LargeGlyph")));
            this.bbiRefresh.Name = "bbiRefresh";
            this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRefresh_ItemClick);
            // 
            // bbiZoomOut
            // 
            this.bbiZoomOut.Caption = "放大";
            this.bbiZoomOut.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiZoomOut.Glyph")));
            this.bbiZoomOut.Id = 0;
            this.bbiZoomOut.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiZoomOut.LargeGlyph")));
            this.bbiZoomOut.Name = "bbiZoomOut";
            this.bbiZoomOut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiZoomOut_ItemClick);
            // 
            // bbiZoomIn
            // 
            this.bbiZoomIn.Caption = "缩小";
            this.bbiZoomIn.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiZoomIn.Glyph")));
            this.bbiZoomIn.Id = 1;
            this.bbiZoomIn.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiZoomIn.LargeGlyph")));
            this.bbiZoomIn.Name = "bbiZoomIn";
            this.bbiZoomIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiZoomIn_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1004, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 528);
            this.barDockControlBottom.Size = new System.Drawing.Size(1004, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 504);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1004, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 504);
            // 
            // FrmSectionAnalysis
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.ClientSize = new System.Drawing.Size(1004, 528);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmSectionAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "横断面分析结果";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmHorizontalSectionAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        private int bmpWidth;
        private int bmpheight;
        private Graphics g;
        private Bitmap bmp;
        private string mapnum;
        private string mapname;
        private List<PPLine> pplines;
        private double hmax;
        private double hmin;
        private double distsum;
        private string roadname;
        private int _type;
        public FrmSectionAnalysis(string title, int type)
        {
            InitializeComponent();
            this.Text = title;
            this._type = type;
        }

        public void SetInfo(string mapname, string mapnum, List<PPLine> pplines, double hmax, double hmin, double distsum, string roadname)
        {
            this.mapname = mapname;
            this.mapnum = mapnum;
            this.pplines = pplines;
            this.hmax = hmax;
            this.hmin = hmin;
            this.distsum = distsum;
            this.roadname = roadname;
            this.bmpWidth = 1000;
            this.bmpheight = 500;
        }

        private void reset()
        {

        }

        private void FrmHorizontalSectionAnalysis_Load(object sender, EventArgs e)
        {
            DrawHDM();
            this.pictureBox1.Image = (Image)bmp;
        }

        private const int top = 10;
        private const int bottom = 10;
        private const int left = 10;
        private const int right = 10;
        private const int leftYAxis = 100;
        private const int tableHeight = 200;
        private const int rowCount = 5;
        private const int outlineInterval = 10;
        private const int titleHeight = 40;
        private const int interval = 30;        
        private const int dialCount = 10;
        private void DrawHDM()
        {
            try
            {
                if (this.bmp != null)
                {
                    this.bmp.Dispose();
                    this.bmp = null;
                }
                if (this.g != null)
                {
                    this.g.Dispose();
                    this.bmp = null;
                }
                this.bmp = new Bitmap(this.bmpWidth, this.bmpheight);
                this.g = Graphics.FromImage(bmp);
                this.g.Clear(Color.White);

                // 外廓线
                Pen pen = new Pen(Color.Black, 4);
                int outlineXOrigin = left;
                int outlineYOrigin = top + titleHeight;
                int outlineWidth = this.bmpWidth - outlineXOrigin - right;
                int outlineHeight = this.bmpheight - outlineYOrigin - bottom;
                this.g.DrawRectangle(pen, outlineXOrigin, outlineYOrigin, outlineWidth, outlineHeight);

                // 标题
                Font font = new Font("黑体", 16);
                SolidBrush brush = new SolidBrush(Color.Black);
                SizeF vSizeF = g.MeasureString(this.mapname, font);
                int pxMapName = (int)vSizeF.Width;
                g.DrawString(this.mapname, font, brush, outlineXOrigin + (outlineWidth - pxMapName) / 2, 3);
                // 所在道路
                font = new Font("Times New Roman", 9);
                g.DrawString("所在道路：" + roadname, font, brush, outlineXOrigin, outlineYOrigin - 20);
                // 断面号
                string tempMapNum = "断面号：" + mapnum;
                vSizeF = g.MeasureString(tempMapNum, font);
                int pxMapNum = (int)vSizeF.Width;
                g.DrawString(tempMapNum, font, brush, outlineXOrigin + outlineWidth - pxMapNum, outlineYOrigin - 20);
                // 表格线
                pen = new Pen(Color.Black, 3);
                int tableXOrigin = outlineXOrigin + outlineInterval;
                int tableYEnd = this.bmpheight - bottom - outlineInterval;
                int tableYOrigin = tableYEnd - tableHeight;
                int tableXEnd = this.bmpWidth - tableXOrigin - right + outlineInterval;
                int rowHeight = tableHeight / rowCount;
                this.g.DrawLine(pen, tableXOrigin, tableYOrigin, tableXEnd, tableYOrigin);
                this.g.DrawLine(pen, tableXOrigin, tableYEnd, tableXEnd, tableYEnd);
                this.g.DrawLine(pen, tableXOrigin, tableYOrigin, tableXOrigin, tableYEnd);
                this.g.DrawLine(pen, tableXEnd, tableYOrigin, tableXEnd, tableYEnd);
                pen = new Pen(Color.Black, 2);
                for (int i = 1; i <= rowCount - 1; i++)
                {
                    this.g.DrawLine(pen, tableXOrigin, tableYOrigin + rowHeight * i, tableXEnd, tableYOrigin + rowHeight * i);
                }
                this.g.DrawLine(pen, tableXOrigin + leftYAxis, tableYOrigin, tableXOrigin + leftYAxis, tableYEnd);
                // 表格文字
                font = new Font("Times New Roman", 11);
                g.DrawString("地面高程(m)", font, brush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 0 + 15);
                g.DrawString("管线高程(m)", font, brush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 1 + 15);
                g.DrawString("规   格(mm)", font, brush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 2 + 15);
                g.DrawString("间    距(m)", font, brush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 3 + 15);
                g.DrawString("总    长(m)", font, brush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 4 + 15);

                //绘图            
                int drawXOrigin1 = tableXOrigin + leftYAxis;
                int drawYOrigin1 = outlineYOrigin + outlineInterval;
                int drawXEnd1 = tableXEnd;
                int drawYEnd1 = tableYOrigin;
                this.g.DrawLine(pen, drawXOrigin1, drawYOrigin1, drawXOrigin1, drawYEnd1);
                int drawXOrigin = drawXOrigin1 + interval;
                int drawYOrigin = drawYOrigin1 + 2 * interval;
                int drawXEnd = drawXEnd1 - 2 * interval;
                int drawYEnd = drawYEnd1 - interval;
                int drawWidth = drawXEnd - drawXOrigin;
                int drawHeight = drawYEnd - drawYOrigin;
                double scaleX = distsum / drawWidth;
                double scaleY = (hmax - hmin) / drawHeight;
                double scaleXReal = scaleX * g.DpiX / 0.0254;
                double scaleYReal = scaleY * g.DpiY / 0.0254;
                g.DrawString("比例尺", font, brush, drawXOrigin1 + (drawXEnd1 - drawXOrigin1) / 2 - 120, drawYOrigin1 + 10);
                g.DrawString("水平 1:" + scaleXReal.ToString("0"), font, brush, drawXOrigin1 + (drawXEnd1 - drawXOrigin1) / 2 - 60, drawYOrigin1);
                g.DrawString("垂直 1:" + scaleYReal.ToString("0"), font, brush, drawXOrigin1 + (drawXEnd1 - drawXOrigin1) / 2 - 60, drawYOrigin1 + 20);
                // 刻度
                font = new Font("Times New Roman", 9);
                int ihmax= int.Parse(Math.Ceiling(hmax).ToString());
                int ihmin = int.Parse(Math.Ceiling(hmin).ToString());
                int deth = ihmax - ihmin;
                if (deth <= 10)
                {
                    for (int i = ihmin; i <= ihmax; i = i + 1)
                    {
                        float dialY = (float)(drawYOrigin + (hmax - i) / scaleY);
                        g.DrawLine(pen, drawXOrigin1 - 4, dialY, drawXOrigin1, dialY);
                        g.DrawString(i.ToString("0.0"), font, brush, drawXOrigin1 - 30, dialY - 5);
                    }
                }
                else if (deth < 20 && deth > 10)
                {
                    for (int i = ihmin; i <= ihmax; i = i + 2)
                    {
                        float dialY = (float)(drawYOrigin + (hmax - i) / scaleY);
                        g.DrawLine(pen, drawXOrigin1 - 4, dialY, drawXOrigin1, dialY);
                        g.DrawString(i.ToString("0.0"), font, brush, drawXOrigin1 - 30, dialY - 5);
                    }
                }
                else
                {
                    double intervalDial = deth / 10 * 10.0 / dialCount;
                    for (int i = 0; i <= dialCount; i++)
                    {
                        float dialY = (float)(drawYOrigin + i * intervalDial);
                        double dialValue = hmax - i * intervalDial * scaleY;
                        g.DrawLine(pen, drawXOrigin1 - 4, dialY, drawXOrigin1, dialY);
                        g.DrawString(dialValue.ToString("0.0"), font, brush, drawXOrigin1 - 30, dialY - 5);
                    }
                }

                // 管线
                RotateText rotate = new RotateText();
                rotate.Graphics = g;
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                PPLine pt0 = this.pplines[0];
                foreach (PPLine pt in this.pplines)
                {
                    PPPoint ptInter = pt.interPoint;
                    double dis = Math.Sqrt((ptInter.X - pt0.interPoint.X) * (ptInter.X - pt0.interPoint.X) + (ptInter.Y - pt0.interPoint.Y) * (ptInter.Y - pt0.interPoint.Y));
                    int pxx = int.Parse(Math.Ceiling(dis / scaleX).ToString());
                    int pxy = int.Parse(Math.Ceiling((pt.clh - hmin) / scaleY).ToString());
                    int ippw = 10;
                    int ipph = 10;
                    int ppw = int.Parse(Math.Ceiling(pt.gj[0] * 0.001 / scaleX).ToString());
                    int pph = int.Parse(Math.Ceiling(pt.gj[1] * 0.001 / scaleY).ToString());
                    //if (ppw > ippw) ippw = ppw;
                    //if (pph > ipph) ipph = pph;
                    int detStandard = 0;// 内底or外顶
                    if (pt.hlb == 1) detStandard += ipph / 2;
                    else if (pt.hlb == -1) detStandard -= ipph / 2;
                    int ppcx = drawXOrigin + pxx - ippw / 2 ;
                    int ppcy = drawYEnd - pxy - ipph / 2 + detStandard;
                    pen = new Pen(Color.DarkBlue, 2);
                    if (pt.isrect)
                    {
                        g.DrawRectangle(pen, ppcx, ppcy, ippw, ipph);
                    }
                    else
                    {
                        g.DrawEllipse(pen, ppcx, ppcy, ippw, ipph);
                    }
                    pen = new Pen(Color.Black, 1);
                    pen.DashStyle = DashStyle.Dash;
                    g.DrawLine(pen, drawXOrigin + pxx, drawYEnd - pxy, drawXOrigin + pxx, tableYEnd - rowHeight);

                    font = new Font("Times New Roman", 8);
                    brush = new SolidBrush(Color.DarkBlue);
                    if (pt.hlb == 1) g.DrawString(pt.facType, font, brush, ppcx + ippw / 2 + 3, ppcy + 3);
                    else if (pt.hlb == -1) g.DrawString(pt.facType, font, brush, ppcx + ippw / 2 + 3, ppcy + ipph  + 3);
                    else if (pt.hlb == 0) g.DrawString(pt.facType, font, brush, ppcx + ippw / 2 + 3, ppcy + ipph / 2 + 3);

                    rotate.DrawString(pt.clh.ToString("0.000"), font, brush, new PointF(ppcx - 3, tableYOrigin + rowHeight * 1 + 20), format, -90f);
                    if (pt.isrect) rotate.DrawString(pt.dia, font, brush, new PointF(ppcx - 3, tableYOrigin + rowHeight * 2 + 20), format, -90f);
                    else rotate.DrawString("DN" + pt.dia, font, brush, new PointF(ppcx - 3, tableYOrigin + rowHeight * 2 + 20), format, -90f);

                    brush = new SolidBrush(Color.Sienna);
                    rotate.DrawString(pt.cgh.ToString("0.000"), font, brush, new PointF(ppcx - 3, tableYOrigin + rowHeight * 0 + 20), format, -90f);
                }
                int gxx0 = 0;
                int gxy0 = 0;
                int pxy0 = 0;
                for (int i = 0; i < this.pplines.Count; i++)
                {
                    PPLine pt = this.pplines[i];
                    PPPoint ptInter = pt.interPoint;
                    double dis = Math.Sqrt((ptInter.X - pt0.interPoint.X) * (ptInter.X - pt0.interPoint.X) + (ptInter.Y - pt0.interPoint.Y) * (ptInter.Y - pt0.interPoint.Y));
                    int gxx = int.Parse(Math.Ceiling(dis / scaleX).ToString());
                    int gxy = int.Parse(Math.Ceiling((pt.cgh - hmin) / scaleY).ToString());
                    int pxy = int.Parse(Math.Ceiling((pt.clh - hmin) / scaleY).ToString());
                    if (i == 0)
                    {
                        // 标识             
                        font = new Font("Times New Roman", 9);
                        brush = new SolidBrush(Color.Sienna);
                        g.DrawString("地面", font, brush, drawXOrigin1 - 60, drawYEnd - gxy - 5);
                        font = new Font("Times New Roman", 9);
                        brush = new SolidBrush(Color.DarkBlue);
                        g.DrawString("管线", font, brush, drawXOrigin1 - 60, drawYEnd - pxy - 5);

                        gxx0 = gxx;
                        gxy0 = gxy;
                        pxy0 = pxy;
                    }
                    else
                    {
                        if (this._type == 1)
                        {
                            pen = new Pen(Color.DarkBlue, 3);
                            g.DrawLine(pen, drawXOrigin + gxx0, drawYEnd - pxy0, drawXOrigin + gxx, drawYEnd - pxy);
                        }
                        //间距             
                        double space = pt.space;
                        string strspace = space.ToString("0.00");
                        vSizeF = g.MeasureString(strspace, font);
                        int pxspace = (int)vSizeF.Width;
                        g.DrawString(strspace, font, brush, drawXOrigin + (gxx + gxx0) / 2 - pxspace / 2, (float)tableYOrigin + rowHeight * 3 + 12);
                        //地面线               
                        pen = new Pen(Color.Sienna, 3);
                        g.DrawLine(pen, drawXOrigin + gxx0, drawYEnd - gxy0, drawXOrigin + gxx, drawYEnd - gxy);
                        gxx0 = gxx;
                        gxy0 = gxy;
                    }
                }
                //总长
                string strdistnum=this.distsum.ToString("0.00");
                vSizeF = g.MeasureString(strdistnum, font);
                int pxdistnum = (int)vSizeF.Width;
                g.DrawString(strdistnum, font, brush, (drawXOrigin + drawXEnd) / 2 - pxdistnum / 2, (float)tableYOrigin + rowHeight * 4 + 12);
            }
            catch (Exception ex)
            {
                 
            }
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "jpg";
            dialog.Filter = "Picture Files(*.jpg)|*.jpg";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.pictureBox1.Image.Save(dialog.FileName);
            }
        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.bmpWidth = 1000;
            this.bmpheight = 500;
            DrawHDM();
            this.pictureBox1.Image = (Image)bmp;

        }

        private void bbiZoomOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.bmpWidth > 4000 || this.bmpheight > 2000) return;
            this.bmpWidth += 100;
            this.bmpheight += 50;
            DrawHDM();
            this.pictureBox1.Image = (Image)bmp;
        }

        private void bbiZoomIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.bmpWidth <= 1000 || this.bmpheight <= 500) return;
            this.bmpWidth -= 100;
            this.bmpheight -= 50;
            DrawHDM();
            this.pictureBox1.Image = (Image)bmp;
        }

    }
}
