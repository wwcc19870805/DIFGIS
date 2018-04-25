using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DF3DScan.Class;
using DF3DScan.Frm;
using DF3DControl.Base;
using ICSharpCode.Core;
using Gvitech.CityMaker.FdeGeometry;
using System.Drawing.Imaging;
using DFCommon.Class;
using System.Collections;
namespace DF3DScan.UC
{
    public enum USERMODE
    {
        New,
        Replace,
        Insert
    }
    
    public class UCCertainScene : XtraUserControl
    {
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCertainScene));
        private System.ComponentModel.IContainer components;
        private TreeList treeList1;
        private GridControl gridControl1;
        private SplitContainerControl splitContainerControl1;
        private TreeListColumn tl_Name;
        private LayoutView layoutView1;
        private LayoutViewColumn colPicture;
        private RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private LayoutViewColumn colName;
        private RepositoryItemTextEdit repositoryItemTextEdit1;
        private TreeListColumn tl_Object;
        private LayoutViewColumn colDuration;
        private LayoutViewColumn colLocation;
        private LayoutViewColumn colComment;
        private LayoutViewField layoutViewField_colPicture;
        private LayoutViewField layoutViewField_colName;
        private LayoutViewField layoutViewField_colComment;
        private LayoutViewField layoutViewField_layoutViewColumn1;
        private LayoutViewField layoutViewField_layoutViewColumn1_1;
        private LayoutViewField layoutViewField_layoutViewColumn1_2;
        private LayoutViewCard layoutViewCard1;
        private SimpleSeparator item1;
        private PopupMenu popupMenuLocation;
        private BarButtonItem barEdit;
        private BarButtonItem barReplace;
        private BarButtonItem barDelete;
        private BarButtonItem barInsert;
        private BarManager barManager1;
        private Bar bar2;
        private BarButtonItem btnShowGroup;
        private BarButtonItem btnNewGroup;
        private BarButtonItem btnNewLocation;
        private BarButtonItem btnForwardLocation;
        private BarButtonItem btnBackwardLocation;
        private BarDockControl barDockControlTop;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarButtonItem barPlayGroup;
        private BarButtonItem barDeleteGroup;
        private BarButtonItem barRenameGroup;
        private BarButtonItem btn_ImportXml;
        private BarButtonItem btn_ExportXml;
        private BarButtonItem barStopGroup;
        private PopupMenu popupMenuGroup;
        private LayoutViewColumn colFilePath;

        public UCCertainScene()
        {
            this.InitializeComponent();
            this.setButtonState();
            this.iterater = this.groupNameTable.Count + 1;
            this.d3 = DF3DApplication.Application.Current3DMapControl;
            string localDataPath = SystemInfo.Instance.LocalDataPath;
            this._tmpPath = Path.Combine(localDataPath, "CertainScene");
            if (!Directory.Exists(this._tmpPath))
            {
                Directory.CreateDirectory(this._tmpPath);
            }
            
        }

        private int iterater = -1;
        private AxRenderControl d3;
        private string oldname = "";
        private Hashtable groupNameTable = new Hashtable();  // 存放所有新特定场景组
        private FormCreateLocation newLocWnd;
        private FormEditLocation editLocWnd;
        private readonly string _tmpPath;
        private string _filePath;
        private USERMODE mode;
        private ICameraTour cameraTour;
        private bool bIsPlaying = false;

        private DataTable ConstructTable()
        {
            DataTable dt = new System.Data.DataTable();
            System.Data.DataColumn dataColumn = new System.Data.DataColumn("Picture", typeof(object));
            System.Data.DataColumn dataColumn2 = new System.Data.DataColumn("Name", typeof(string));
            System.Data.DataColumn dataColumn3 = new System.Data.DataColumn("Comment", typeof(string));
            System.Data.DataColumn dataColumn4 = new System.Data.DataColumn("Duration", typeof(double));
            System.Data.DataColumn dataColumn5 = new System.Data.DataColumn("Location", typeof(object));
            System.Data.DataColumn dataColumn6 = new System.Data.DataColumn("FilePath", typeof(object));
            dt.Columns.AddRange(new System.Data.DataColumn[]
			{
				dataColumn, 
				dataColumn2, 
				dataColumn3, 
				dataColumn4, 
				dataColumn5,
                dataColumn6
			});
            return dt;
        }
        private void setButtonState()
        {
            this.btnForwardLocation.Enabled = false;
            this.btnBackwardLocation.Enabled = false;
            if (this.treeList1.Nodes.Count == 0 || this.treeList1.FocusedNode == null)
            {
                this.btnNewLocation.Enabled = false;
                this.btnNewGroup.Enabled = true;
                return;
            }
            if (this.treeList1.Nodes.Count >= 1)
            {
                this.btnNewLocation.Enabled = true;
                this.btnNewGroup.Enabled = true;
                return;
            }
        }
        private void setButtonDisable()
        {
            this.btnNewLocation.Enabled = false;
            this.btnNewGroup.Enabled = false;
            this.btnForwardLocation.Enabled = false;
            this.btnBackwardLocation.Enabled = false;
        }
        private void setLocationButtonDisable()
        {
            this.btnForwardLocation.Enabled = false;
            this.btnBackwardLocation.Enabled = false;
        }
        private void setLocationButtonState()
        {
            int rowhandle = this.layoutView1.FocusedRowHandle;
            if (rowhandle < 0 || this.treeList1.FocusedNode == null)
            {
                this.btnForwardLocation.Enabled = false;
                this.btnBackwardLocation.Enabled = false;
                return;
            }
            DataTable dt = (System.Data.DataTable)this.treeList1.FocusedNode.GetValue("tl_Object");
            if (rowhandle == 0)
            {
                if (dt.Rows.Count == 1)
                {
                    this.btnForwardLocation.Enabled = false;
                    this.btnBackwardLocation.Enabled = false;
                    return;
                }
                this.btnForwardLocation.Enabled = false;
                this.btnBackwardLocation.Enabled = true;
                return;
            }
            else
            {
                if (rowhandle == dt.Rows.Count - 1)
                {
                    this.btnForwardLocation.Enabled = true;
                    this.btnBackwardLocation.Enabled = false;
                    return;
                }
                this.btnForwardLocation.Enabled = true;
                this.btnBackwardLocation.Enabled = true;
                return;
            }
        }
        public void AdviseEvent()
        {
            this.d3.RcPictureExportEnd -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportEndEventHandler(this.AxRenderControl_RcExportEnd);
            this.d3.RcPictureExportEnd += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportEndEventHandler(this.AxRenderControl_RcExportEnd);
        }
        private void AxRenderControl_RcExportEnd(object sender, _IRenderControlEvents_RcPictureExportEndEvent e)
        {
            switch (this.mode)
            {
                case USERMODE.New:
                    {
                        string nodeName = this.treeList1.FocusedNode.GetValue("tl_Name").ToString();
                        DataTable dt = (System.Data.DataTable)this.treeList1.FocusedNode.GetValue("tl_Object");
                        System.Data.DataRow dataRow = dt.NewRow();
                        dataRow["Name"] = this.newLocWnd.LocationName;
                        dataRow["Comment"] = this.newLocWnd.Comment;
                        dataRow["Duration"] = this.newLocWnd.Duration;
                        try
                        {
                            if (System.IO.File.Exists(this._filePath))
                            {
                                Image img = System.Drawing.Image.FromFile(this._filePath);
                                Bitmap bmp = new Bitmap(img);
                                img.Dispose();
                                dataRow["Picture"] = bmp;
                                dataRow["FilePath"] = this._filePath;
                            }
                        }
                        catch (Exception ex)
                        {
                            dataRow["Picture"] = null;
                            dataRow["FilePath"] = "";
                        }
                        CameraProperty cp = new CameraProperty();
                        IVector3 vector;
                        IEulerAngle eulerAngle;
                        this.d3.Camera.GetCamera(out vector, out eulerAngle);
                        cp.X = vector.X;
                        cp.Y = vector.Y;
                        cp.Z = vector.Z;
                        cp.Heading = eulerAngle.Heading;
                        cp.Tilt = eulerAngle.Tilt;
                        cp.Roll = eulerAngle.Roll;
                        dataRow["Location"] = cp;
                        dt.Rows.Add(dataRow);
                        this.treeList1.FocusedNode.SetValue("tl_Object", dt);
                        nodeName = this.treeList1.FocusedNode.GetValue("tl_Name").ToString();
                        HashSet<string> locationNameTable = (HashSet<string>)this.groupNameTable[nodeName];
                        locationNameTable.Add(this.newLocWnd.LocationName);
                        this.groupNameTable[nodeName] = locationNameTable;
                        this.d3.RcPictureExportEnd -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportEndEventHandler(this.AxRenderControl_RcExportEnd);
                        this.newLocWnd.Dispose();
                        this.layoutView1.FocusedRowHandle = dt.Rows.Count - 1;
                        return;
                    }
                case USERMODE.Replace:
                    {
                        DataTable dt = (System.Data.DataTable)this.treeList1.FocusedNode.GetValue("tl_Object");
                        int rowhandle = this.layoutView1.FocusedRowHandle;
                        if (rowhandle < 0) return;
                        System.Data.DataRow dataRow2 = dt.Rows[rowhandle];
                        try
                        {
                            if (System.IO.File.Exists(this._filePath))
                            {
                                Image img = System.Drawing.Image.FromFile(this._filePath);
                                Bitmap bmp = new Bitmap(img);
                                img.Dispose();
                                dataRow2["Picture"] = bmp; 
                                dataRow2["FilePath"] = this._filePath;
                            }
                        }
                        catch (System.Exception)
                        {
                            dataRow2["Picture"] = null;
                            dataRow2["FilePath"] = "";
                        }
                        CameraProperty cp = new CameraProperty();
                        IVector3 vector;
                        IEulerAngle eulerAngle;
                        this.d3.Camera.GetCamera(out vector, out eulerAngle);
                        cp.X = vector.X;
                        cp.Y = vector.Y;
                        cp.Z = vector.Z;
                        cp.Heading = eulerAngle.Heading;
                        cp.Tilt = eulerAngle.Tilt;
                        cp.Roll = eulerAngle.Roll;

                        dataRow2["Location"] = cp;
                        this.treeList1.FocusedNode.SetValue("tl_Object", dt);
                        this.layoutView1.FocusedRowHandle = rowhandle;
                        this.d3.RcPictureExportEnd -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportEndEventHandler(this.AxRenderControl_RcExportEnd);
                        return;
                    }
                case USERMODE.Insert:
                    {
                        DataTable dt = (System.Data.DataTable)this.treeList1.FocusedNode.GetValue("tl_Object");
                        System.Data.DataRow dataRow3 = dt.NewRow();
                        try
                        {
                            if (System.IO.File.Exists(this._filePath))
                            {
                                Image img = System.Drawing.Image.FromFile(this._filePath);
                                Bitmap bmp = new Bitmap(img);
                                img.Dispose();
                                dataRow3["Picture"] = bmp; dataRow3["FilePath"] = this._filePath;
                            }
                        }
                        catch (System.Exception)
                        {
                            dataRow3["Picture"] = null;
                            dataRow3["FilePath"] ="";
                        }
                        dataRow3["Name"] = this.newLocWnd.LocationName;
                        dataRow3["Comment"] = this.newLocWnd.Comment;
                        dataRow3["Duration"] = this.newLocWnd.Duration;
                        CameraProperty cp = new CameraProperty();
                        IVector3 vector;
                        IEulerAngle eulerAngle;
                        this.d3.Camera.GetCamera(out vector, out eulerAngle);
                        cp.X = vector.X;
                        cp.Y = vector.Y;
                        cp.Z = vector.Z;
                        cp.Heading = eulerAngle.Heading;
                        cp.Tilt = eulerAngle.Tilt;
                        cp.Roll = eulerAngle.Roll;
                        dataRow3["Location"] = cp;
                        int rowhandle = this.layoutView1.FocusedRowHandle;
                        if (rowhandle >= 0)
                        {
                            dt.Rows.InsertAt(dataRow3, rowhandle);
                            this.layoutView1.FocusedRowHandle = rowhandle;
                        }
                        else
                        {
                            dt.Rows.Add(dataRow3);
                            this.layoutView1.FocusedRowHandle = 0;
                        }
                        this.treeList1.FocusedNode.SetValue("tl_Object", dt);
                        string nodeName = this.treeList1.FocusedNode.GetValue("tl_Name").ToString();
                        HashSet<string> locationNameTable = (HashSet<string>)this.groupNameTable[nodeName];
                        locationNameTable.Add(this.newLocWnd.LocationName);
                        this.groupNameTable[nodeName] = locationNameTable;
                        this.d3.RcPictureExportEnd -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportEndEventHandler(this.AxRenderControl_RcExportEnd);
                        this.newLocWnd.Dispose();
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }
        
        private void splitContainerControl1_SplitGroupPanelCollapsed(object sender, SplitGroupPanelCollapsedEventArgs e)
        {
            bool collapsed = this.splitContainerControl1.Collapsed;
            if (!collapsed)
            {
                this.btnShowGroup.Caption = "隐藏组";
                this.btnShowGroup.Glyph = (System.Drawing.Image)this.resources.GetObject("btnShowGroup.Glyph");
                this.btnNewGroup.Visibility = BarItemVisibility.Always;
                return;
            }
            this.btnShowGroup.Caption = "显示组";
            this.btnShowGroup.Glyph = (System.Drawing.Image)this.resources.GetObject("btnShowGroup.Glyph");
            this.btnNewGroup.Visibility = BarItemVisibility.Never;
        }

        private void btnShowGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool collapsed = this.splitContainerControl1.Collapsed;
            this.splitContainerControl1.Collapsed = !collapsed;
            if (collapsed)
            {
                this.btnShowGroup.Caption = "隐藏组";
                this.btnShowGroup.Glyph = (System.Drawing.Image)this.resources.GetObject("btnShowGroup.Glyph");
                this.btnNewGroup.Visibility = BarItemVisibility.Always;
                return;
            }
            this.btnShowGroup.Caption = "显示组";
            this.btnShowGroup.Glyph = (System.Drawing.Image)this.resources.GetObject("btnShowGroup.Glyph");
            this.btnNewGroup.Visibility = BarItemVisibility.Never;
        }
        
        private void btnNewLocation_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode == null)
            {
                return;
            }
            
            string nodeName = this.treeList1.FocusedNode.GetValue("tl_Name").ToString();
            using (this.newLocWnd = new FormCreateLocation(this.groupNameTable, nodeName, false))
            {
                if (this.newLocWnd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this._filePath = this._tmpPath + "\\" + System.Guid.NewGuid().ToString() + ".jpg";
                    this.AdviseEvent();
                    this.mode = USERMODE.New;
                    if (!this.d3.ExportManager.ExportImage(this._filePath, 128u, 128u, false))
                    {
                    }
                }
            }
        }
        private void btnForwardLocation_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode == null) return;
            int rowhandle = this.layoutView1.FocusedRowHandle;
            if (rowhandle <= 0) return;
            DataTable dt = (System.Data.DataTable)this.treeList1.FocusedNode.GetValue("tl_Object");
            System.Data.DataRow dataRow = dt.Rows[rowhandle];
            System.Data.DataRow dataRow2 = dt.NewRow();
            dataRow2[0] = dataRow[0];
            dataRow2[1] = dataRow[1];
            dataRow2[2] = dataRow[2];
            dataRow2[3] = dataRow[3];
            dataRow2[4] = dataRow[4];
            dataRow2[5] = dataRow[5];
            dt.Rows.InsertAt(dataRow2, rowhandle - 1);
            dt.Rows.RemoveAt(rowhandle + 1);
            this.layoutView1.FocusedRowHandle = rowhandle - 1;
            this.setLocationButtonState();
        }
        private void btnBackwardLocation_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode == null) return;
            int rowhandle = this.layoutView1.FocusedRowHandle;
            if (rowhandle < 0) return;
            
            DataTable dt = (System.Data.DataTable)this.treeList1.FocusedNode.GetValue("tl_Object");
            if (rowhandle == dt.Rows.Count - 1) return;

            System.Data.DataRow dataRow = dt.Rows[rowhandle];
            System.Data.DataRow dataRow2 = dt.NewRow();
            dataRow2[0] = dataRow[0];
            dataRow2[1] = dataRow[1];
            dataRow2[2] = dataRow[2];
            dataRow2[3] = dataRow[3];
            dataRow2[4] = dataRow[4];
            dataRow2[5] = dataRow[5];
            dt.Rows.InsertAt(dataRow2, rowhandle + 2);
            dt.Rows.RemoveAt(rowhandle);
            this.layoutView1.FocusedRowHandle = rowhandle + 1;
            this.setLocationButtonState();
        }
        private void barEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            int rowhandle = this.layoutView1.FocusedRowHandle;
            if (rowhandle < 0) return;
            if (this.treeList1.FocusedNode == null) return;
            string nodeName = this.treeList1.FocusedNode.GetValue("tl_Name").ToString();
            DataTable dt = this.treeList1.FocusedNode.GetValue("tl_Object") as DataTable;
            System.Data.DataRow dataRow = dt.Rows[rowhandle];
            using (this.editLocWnd = new FormEditLocation(dataRow["Name"].ToString(), dataRow["Comment"].ToString(), dataRow["Duration"], dataRow["Location"], (HashSet<string>)this.groupNameTable[nodeName]))
            {
                if (this.editLocWnd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (!(this.editLocWnd.LocationName == dataRow["Name"].ToString()) || !(this.editLocWnd.Comment == dataRow["Comment"].ToString()) || this.editLocWnd.Duration != double.Parse(dataRow["Duration"].ToString()))
                    {
                        HashSet<string> locationNameTable = (HashSet<string>)this.groupNameTable[nodeName];
                        locationNameTable.Remove(nodeName);
                        locationNameTable.Add(this.editLocWnd.LocationName);
                        dataRow["Name"] = this.editLocWnd.LocationName;
                        dataRow["Comment"] = this.editLocWnd.Comment;
                        dataRow["Duration"] = this.editLocWnd.Duration;
                        this.treeList1.FocusedNode.SetValue("tl_Object", dt);
                        this.groupNameTable[nodeName] = locationNameTable;
                        this.layoutView1.RefreshData();
                    }
                }
            }
        }
        private void barReplace_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定替换该特定场景吗？", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                int[] sr = this.layoutView1.GetSelectedRows();
                if (sr == null || sr.Length > 1) return;
                DataTable dt = this.treeList1.FocusedNode.GetValue("tl_Object") as DataTable;
                if(dt == null || dt.Rows.Count == 0) return;
                DataRow dr = dt.Rows[sr[0]];
                CameraProperty cp = new CameraProperty();
                IVector3 vector;
                IEulerAngle eulerAngle;
                this.d3.Camera.GetCamera(out vector, out eulerAngle);
                cp.X = vector.X;
                cp.Y = vector.Y;
                cp.Z = vector.Z;
                cp.Heading = eulerAngle.Heading;
                cp.Roll = eulerAngle.Roll;
                cp.Tilt = eulerAngle.Tilt;
                this._filePath = dr["FilePath"].ToString();
                this.AdviseEvent();
                this.mode = USERMODE.Replace;
                if (!this.d3.ExportManager.ExportImage(this._filePath, 128u, 128u, true))
                {
                }
            }
        }
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定删除该特定场景？", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string nodeName = this.treeList1.FocusedNode.GetValue("tl_Name").ToString();
                DataTable dt = this.treeList1.FocusedNode.GetValue("tl_Object") as DataTable;
                HashSet<string> locationNameTable = (HashSet<string>)this.groupNameTable[nodeName];
                int[] rowsToDel = this.layoutView1.GetSelectedRows();
                try
                {
                    for (int i = 0; i < rowsToDel.Length; i++)
                    {
                        int rowhandle = rowsToDel[i] - i;
                        System.Data.DataRow dataRow = dt.Rows[rowhandle];
                        string filePath = dataRow["FilePath"].ToString();
                        if (File.Exists(filePath)) File.Delete(filePath);
                        locationNameTable.Remove(dataRow["Name"].ToString());
                        dt.Rows.Remove(dataRow);
                        
                    }
                }
                catch (System.Exception ex)
                {
                }
                this.treeList1.FocusedNode.SetValue("tl_Object", dt);
                this.groupNameTable[nodeName] = locationNameTable;
                this.setLocationButtonState();
            }
        }
        private void barInsert_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode == null)
            {
                return;
            }
            
            string nodeName = this.treeList1.FocusedNode.GetValue("tl_Name").ToString();
            using (this.newLocWnd = new FormCreateLocation(this.groupNameTable, nodeName, true))
            {
                if (this.newLocWnd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CameraProperty cp = new CameraProperty();
                    IVector3 vector;
                    IEulerAngle eulerAngle;
                    this.d3.Camera.GetCamera(out vector, out eulerAngle);
                    cp.X = vector.X;
                    cp.Y = vector.Y;
                    cp.Z = vector.Z;
                    cp.Heading = eulerAngle.Heading;
                    cp.Tilt = eulerAngle.Tilt;
                    cp.Roll = eulerAngle.Roll;
                    this._filePath = this._tmpPath + "\\" + System.Guid.NewGuid().ToString() + ".jpg";
                    this.AdviseEvent();
                    this.mode = USERMODE.Insert;
                    if (!this.d3.ExportManager.ExportImage(this._filePath, 128u, 128u, true))
                    {
                    }
                }
            }
        }

        private void btnNewGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            string nodeName = "新建组" + this.iterater.ToString();
            while (this.groupNameTable.ContainsKey(nodeName))
            {
                this.iterater++;
                nodeName = "新建组" + this.iterater.ToString();
            }
            DataTable dt = ConstructTable();
            TreeListNode temp = this.treeList1.AppendNode(new object[]
			{
				nodeName, 
				dt
			}, null);
            HashSet<string> set = new HashSet<string>();
            this.groupNameTable.Add(nodeName, set);
            this.iterater++;
            this.treeList1.FocusedNode = temp;
            this.setButtonState();
        }
        private void barDeleteGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode == null) return;

            if (XtraMessageBox.Show("确定删除该组吗？", "提示", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                return;
            try
            {
                System.Data.DataTable dataTable = (System.Data.DataTable)this.treeList1.FocusedNode.GetValue("tl_Object");
                if (dataTable != null && dataTable.Rows.Count != 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string filePath = dataTable.Rows[i]["FilePath"].ToString();
                        if (File.Exists(filePath)) File.Delete(filePath);
                    }
                    dataTable.Rows.Clear();
                }
                string nodeName = this.treeList1.FocusedNode.GetValue("tl_Name").ToString();
                string xmlFilePath = this._tmpPath + "\\" + nodeName + ".xml";
                if (File.Exists(xmlFilePath)) File.Delete(xmlFilePath);
                HashSet<string> locationNames = (HashSet<string>)this.groupNameTable[nodeName];
                locationNames.Clear();
                this.groupNameTable.Remove(nodeName);
                this.treeList1.DeleteSelectedNodes();
                this.setButtonState();
            }
            catch (System.Exception ex)
            {
            }
        }
        private void barRenameGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode == null)
            {
                return;
            }
            this.treeList1.FocusedNode.TreeList.Columns.ColumnByFieldName("tl_Name").OptionsColumn.AllowEdit = true;
            this.treeList1.FocusedNode.TreeList.Columns.ColumnByFieldName("tl_Name").OptionsColumn.AllowFocus = true;
            this.treeList1.FocusedColumn = this.treeList1.FocusedNode.TreeList.Columns.ColumnByFieldName("tl_Name");
            this.oldname = this.treeList1.FocusedNode.GetValue("tl_Name").ToString();
            this.treeList1.ShowEditor();
            this.setButtonDisable();
        }

        private void btn_ExportXml_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode != null)
            {
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    DefaultExt = "xml"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    WaitDialogForm form = new WaitDialogForm("正在导出，请稍候...", "提示");
                    form.Show();
                    DataTable dt = this.treeList1.FocusedNode.GetValue("tl_Object") as DataTable;
                    bool flag = this.ExportXml(dialog.FileName, dt);
                    form.Close();
                    if (flag)
                    {
                        XtraMessageBox.Show("导出完成！", "提示");
                    }
                    else
                    {
                        XtraMessageBox.Show("导出失败！", "提示");
                    }
                }
            }
        }
        private void btn_ImportXml_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "XML File(*.xml)|*.xml",
                Multiselect = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                WaitDialogForm form = new WaitDialogForm("提示", "正在导入，请稍候！");
                form.Show();
                int num = 1;
                string[] fileNames = dialog.FileNames;
                foreach (string fileName in fileNames)
                {
                    string key = fileName.Substring(fileName.LastIndexOf("\\") + 1,
                        fileName.LastIndexOf(".") - fileName.LastIndexOf("\\") - 1);
                    while (this.groupNameTable.ContainsKey(key))
                    {
                        num++;
                        key = key + num.ToString();
                    }
                    ImportXml(fileName, key);
                }
                form.Close();
            }
        }
        private void ImportXml(string fileName, string key)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(fileName);
                if (document.SelectSingleNode("CameraGroup") != null)
                {
                    DataTable dt = ConstructTable();
                    TreeListNode nodeTemp = this.treeList1.AppendNode(new object[] { key, dt }, (TreeListNode)null);
                    XmlNodeList childNodes = document.SelectSingleNode("CameraGroup").ChildNodes;
                    HashSet<string> set = new HashSet<string>();
                    if (childNodes.Count > 0)
                    {
                        foreach (XmlNode node in childNodes)
                        {
                            if (node.Name == "Camera")
                            {
                                string locationName = node.Attributes["Name"].Value;
                                CameraProperty cp = new CameraProperty();
                                cp.X = double.Parse(node.Attributes["x"].Value);
                                cp.Y = double.Parse(node.Attributes["y"].Value);
                                cp.Z = double.Parse(node.Attributes["z"].Value);
                                cp.Heading = double.Parse(node.Attributes["heading"].Value);
                                cp.Roll = double.Parse(node.Attributes["roll"].Value);
                                cp.Tilt = double.Parse(node.Attributes["tilt"].Value);
                                string filePath = node.Attributes["FilePath"].Value;
                                DataRow row = dt.NewRow();
                                try
                                {
                                    if (File.Exists(filePath))
                                    {
                                        Image img = System.Drawing.Image.FromFile(filePath);
                                        Bitmap bmp = new Bitmap(img);
                                        img.Dispose();
                                        row["Picture"] = bmp;
                                        row["FilePath"] = filePath;
                                    }
                                }
                                catch (Exception exception)
                                {
                                    row["Picture"] = null;
                                    row["FilePath"] = "";
                                }
                                row["Name"] = locationName;
                                row["Comment"] = "";
                                row["Duration"] = "5";
                                row["Location"] = cp;
                                dt.Rows.Add(row);
                                set.Add(locationName);
                            }
                        }
                    }
                    this.groupNameTable.Add(key, set);
                    this.layoutView1.ClearSelection();
                    if (dt.Rows.Count > 0) this.layoutView1.FocusedRowHandle = 0;
                    this.treeList1.FocusedNode = nodeTemp;
                }
                else
                {
                    XtraMessageBox.Show("导入文件格式不匹配！", "提示");
                }
            }
            catch (Exception exception2)
            {
                XtraMessageBox.Show("加载特定场景图层树失败！", "提示");
            }
            this.treeList1.Focus();
            this.setButtonState();
        }
        private bool ExportXml(string file, DataTable dt)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "utf-8", null);
                document.AppendChild(newChild);
                XmlNode node = document.CreateElement("CameraGroup");
                document.AppendChild(node);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        XmlElement element = document.CreateElement("Camera");
                        element.SetAttribute("Name", row["Name"].ToString());
                        if (row["FilePath"] != null && File.Exists(row["FilePath"].ToString()))
                        {
                            element.SetAttribute("FilePath", row["FilePath"].ToString());
                        }
                        else
                        {
                            element.SetAttribute("FilePath", "");
                        }
                        CameraProperty cp = row["Location"] as CameraProperty;
                        if (cp == null) continue;
                        element.SetAttribute("x", cp.X.ToString());
                        element.SetAttribute("y", cp.Y.ToString());
                        element.SetAttribute("z", cp.Z.ToString());
                        element.SetAttribute("heading", cp.Heading.ToString());
                        element.SetAttribute("tilt", cp.Tilt.ToString());
                        element.SetAttribute("roll", cp.Roll.ToString());
                        node.AppendChild(element);
                    }
                }
                document.Save(file);
                return true;
            }
            catch (Exception exception2)
            {
                return false;
            }
        }

        private void barPlayGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode == null)
            {
                return;
            }
            if (cameraTour != null)
            {
                if (bIsPlaying) { cameraTour.Pause(); bIsPlaying = false; this.barPlayGroup.Caption = "播放"; }
                else { cameraTour.Play(); bIsPlaying = true; this.barPlayGroup.Caption = "暂停"; }
                return;
            }
            DataTable dt = (System.Data.DataTable)this.treeList1.FocusedNode.GetValue("tl_Object");
            if (dt.Rows.Count < 2)
            {
                return;
            }
            cameraTour = this.d3.ObjectManager.CreateCameraTour(this.d3.ProjectTree.RootID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                System.Data.DataRow dataRow = dt.Rows[i];
                new System.Data.DataColumn("Picture", typeof(object));
                new System.Data.DataColumn("Name", typeof(string));
                new System.Data.DataColumn("Comment", typeof(string));
                new System.Data.DataColumn("Duration", typeof(double));
                new System.Data.DataColumn("Location", typeof(object));
                CameraProperty cp = (CameraProperty)dataRow["Location"];
                IVector3 vector = new Vector3Class();
                vector.Set(cp.X, cp.Y, cp.Z);
                cameraTour.AddWaypoint(vector, new EulerAngleClass
                {
                    Heading = cp.Heading,
                    Tilt = cp.Tilt,
                    Roll = cp.Roll
                }, (double)dataRow["Duration"], gviCameraTourMode.gviCameraTourLinear);
            }
            cameraTour.Play();
            bIsPlaying = true;
            this.barPlayGroup.Caption = "暂停";
            this.gridControl1.Enabled = false;
            this.btnNewLocation.Enabled = false;
            setLocationButtonDisable();
            d3.RcCameraFlyFinished += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraFlyFinishedEventHandler(d3_RcCameraFlyFinished);
        }

        private void d3_RcCameraFlyFinished(object sender, _IRenderControlEvents_RcCameraFlyFinishedEvent e)
        {
            if (e.type == 1)
            {
                StopCameraTour();
            }
        }
        private void StopCameraTour()
        {
            if (cameraTour != null)
            {
                cameraTour.Stop();
                bIsPlaying = false;
                this.barPlayGroup.Caption = "播放";
                this.gridControl1.Enabled = true;
                this.btnNewLocation.Enabled = true;
                d3.RcCameraFlyFinished -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraFlyFinishedEventHandler(d3_RcCameraFlyFinished);
                d3.ObjectManager.DeleteObject(cameraTour.Guid);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(cameraTour);
                cameraTour = null;
            }
        }
        private void barStopGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            StopCameraTour();
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "tl_Name")
            {
                string text = e.Value.ToString();
                text.Trim();
                e.Node.SetValue("tl_Name", text);
            }
        }
        private void treeList1_HiddenEditor(object sender, System.EventArgs e)
        {
            string text = this.treeList1.FocusedNode.GetValue("tl_Name").ToString().Trim();
            if (text == string.Empty)
            {
                XtraMessageBox.Show("组名不能为空！","提示");
                this.treeList1.FocusedNode.SetValue("tl_Name", this.oldname);
            }
            else if (text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                XtraMessageBox.Show("输入存在非法字符！", "提示");
                this.treeList1.FocusedNode.SetValue("tl_Name", this.oldname);
            }
            else
            {
                if (this.groupNameTable.ContainsKey(text))
                {
                    if (text != this.oldname)
                    {
                        XtraMessageBox.Show("已存在该组！", "提示");
                    }
                    this.treeList1.FocusedNode.SetValue("tl_Name", this.oldname);
                }
                else
                {
                    HashSet<string> locationNameTable = (HashSet<string>)this.groupNameTable[this.oldname];
                    groupNameTable.Remove(this.oldname);
                    this.groupNameTable.Add(text, locationNameTable);
                    string xmlFilePath =this._tmpPath + "\\" + this.oldname + ".xml";
                    string newXmlFilePath = this._tmpPath + "\\" + text + ".xml";//名称是否有效
                    if (File.Exists(xmlFilePath)) System.IO.File.Move(xmlFilePath, newXmlFilePath);
                    this.treeList1.FocusedNode.SetValue("tl_Name", text);
                }
            }
            this.treeList1.CloseEditor();
            this.treeList1.FocusedColumn.OptionsColumn.AllowEdit = false;
            this.treeList1.FocusedColumn.OptionsColumn.AllowFocus = false;
            this.setButtonState();
        }
        private void treeList1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TreeListHitInfo treeListHitInfo = this.treeList1.CalcHitInfo(e.Location);
            TreeListNode node = treeListHitInfo.Node;
            this.treeList1.SetFocusedNode(node);
            this.setButtonState();
            if (node == null)
            {
                this.barPlayGroup.Enabled = false;
                this.barStopGroup.Enabled = false;
                this.barDeleteGroup.Enabled = false;
                this.barRenameGroup.Enabled = false;
                this.btn_ExportXml.Enabled = false;
                this.btn_ImportXml.Enabled = true;
            }
            else
            {
                this.barPlayGroup.Enabled = true;
                this.barStopGroup.Enabled = true;
                this.barDeleteGroup.Enabled = true;
                this.barRenameGroup.Enabled = true;
                this.btn_ExportXml.Enabled = true;
                this.btn_ImportXml.Enabled = true;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.Clicks == 1)
            {
                this.popupMenuGroup.ShowPopup(System.Windows.Forms.Cursor.Position);
                if (this.treeList1.FocusedNode == null) return;
                DataTable dt = (System.Data.DataTable)this.treeList1.FocusedNode.GetValue("tl_Object");
                if (dt == null) return;
                if (dt.Rows.Count < 2)
                {
                    this.barPlayGroup.Enabled = false;
                    this.barStopGroup.Enabled = false;
                }
                else
                {
                    this.barPlayGroup.Enabled = true;
                    this.barStopGroup.Enabled = true;
                }
            }
        }
        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            StopCameraTour();
            this.gridControl1.DataSource = null;
            if (this.treeList1.FocusedNode == null)
            {
                return;
            }
            this.setButtonState();
            DataTable dt = (System.Data.DataTable)this.treeList1.FocusedNode.GetValue("tl_Object");
            this.gridControl1.DataSource = dt;
        }
        
        private void gridControl1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LayoutViewHitInfo layoutViewHitInfo = (LayoutViewHitInfo)this.gridControl1.FocusedView.CalcHitInfo(e.Location);
            if (layoutViewHitInfo != null && layoutViewHitInfo.InCard)
            {
                int rowhandle = layoutViewHitInfo.RowHandle;
                DataTable dt = (System.Data.DataTable)this.treeList1.FocusedNode.GetValue("tl_Object");
                System.Data.DataRow dataRow = dt.Rows[rowhandle];
                CameraProperty cp = (CameraProperty)dataRow["Location"];
                IVector3 vector = new Vector3Class();
                vector.Set(cp.X, cp.Y, cp.Z);
                IEulerAngle eulerAngle = new EulerAngleClass();
                eulerAngle.Heading = cp.Heading;
                eulerAngle.Tilt = cp.Tilt;
                eulerAngle.Roll = cp.Roll;
                if (this.d3.InteractMode != gviInteractMode.gviInteractNormal)
                {
                    this.d3.InteractMode = gviInteractMode.gviInteractNormal;
                    DF3DApplication.Application.Workbench.BarPerformClick("Wander3D");
                }
                
                this.d3.Camera.SetCamera(vector, eulerAngle, gviSetCameraFlags.gviSetCameraNoFlags);
            }
        }
        private void layoutView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.treeList1.Nodes.Count == 0)
            {
                return;
            }
            LayoutViewHitInfo layoutViewHitInfo = this.layoutView1.CalcHitInfo(e.Location);
            int rowhandle = layoutViewHitInfo.RowHandle;
            this.setLocationButtonState();
            
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popupMenuLocation.ShowPopup(System.Windows.Forms.Control.MousePosition);
                int[] sr = this.layoutView1.GetSelectedRows();
                if (layoutViewHitInfo != null && layoutViewHitInfo.InCard && (sr == null || sr.Length == 1))
                {
                    this.barEdit.Enabled = true;
                    this.barReplace.Enabled = true;
                    this.barDelete.Enabled = true;
                    this.barInsert.Enabled = true;
                    return;
                }
                if (layoutViewHitInfo != null && layoutViewHitInfo.InCard && sr != null && sr.Length > 1)
                {
                    this.barEdit.Enabled = false;
                    this.barReplace.Enabled = false;
                    this.barDelete.Enabled = true;
                    this.barInsert.Enabled = false;
                    return;
                }
                if (layoutViewHitInfo != null && !layoutViewHitInfo.InCard)
                {
                    this.barEdit.Enabled = false;
                    this.barReplace.Enabled = false;
                    this.barDelete.Enabled = false;
                    this.barInsert.Enabled = true;
                }
            }
        }

        private void UCCertainScene_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(this._tmpPath))
            {
                string[] files = Directory.GetFiles(this._tmpPath, "*.xml");
                foreach (string file in files)
                {
                    string key = file.Substring(file.LastIndexOf("\\") + 1, file.LastIndexOf(".") - file.LastIndexOf("\\") - 1);
                    ImportXml(file, key);
                }
            }
        }
        public void RestoreEnv()
        {
            try
            {
                if (cameraTour != null)
                {
                    cameraTour.Stop();
                    d3.ObjectManager.DeleteObject(cameraTour.Guid);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cameraTour);
                    cameraTour = null;
                }
                foreach (TreeListNode groupNode in this.treeList1.Nodes)
                {
                    string fileName = this._tmpPath + "\\" + groupNode.GetValue("tl_Name").ToString() + ".xml";
                    DataTable dt = groupNode.GetValue("tl_Object") as DataTable;
                    ExportXml(fileName, dt);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCertainScene));
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.tl_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_Object = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.colPicture = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.layoutViewField_colPicture = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colName = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutViewField_colName = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colComment = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_colComment = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colDuration = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colLocation = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colFilePath = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_2 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.item1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.popupMenuLocation = new DevExpress.XtraBars.PopupMenu();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barReplace = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barInsert = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnShowGroup = new DevExpress.XtraBars.BarButtonItem();
            this.btnNewGroup = new DevExpress.XtraBars.BarButtonItem();
            this.btnNewLocation = new DevExpress.XtraBars.BarButtonItem();
            this.btnForwardLocation = new DevExpress.XtraBars.BarButtonItem();
            this.btnBackwardLocation = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barPlayGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barDeleteGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barRenameGroup = new DevExpress.XtraBars.BarButtonItem();
            this.btn_ImportXml = new DevExpress.XtraBars.BarButtonItem();
            this.btn_ExportXml = new DevExpress.XtraBars.BarButtonItem();
            this.barStopGroup = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuGroup = new DevExpress.XtraBars.PopupMenu();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.item1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // treeList1
            // 
            this.treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treeList1.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this.treeList1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeList1.Appearance.GroupButton.BackColor = System.Drawing.Color.Lime;
            this.treeList1.Appearance.GroupButton.Options.UseBackColor = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tl_Name,
            this.tl_Object});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.AllowIndeterminateCheckState = true;
            this.treeList1.OptionsPrint.PrintPageHeader = false;
            this.treeList1.OptionsView.ShowColumns = false;
            this.treeList1.OptionsView.ShowHorzLines = false;
            this.treeList1.OptionsView.ShowIndicator = false;
            this.treeList1.Size = new System.Drawing.Size(275, 178);
            this.treeList1.TabIndex = 0;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            this.treeList1.HiddenEditor += new System.EventHandler(this.treeList1_HiddenEditor);
            this.treeList1.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeList1_CellValueChanged);
            this.treeList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseUp);
            // 
            // tl_Name
            // 
            this.tl_Name.Caption = "名称";
            this.tl_Name.FieldName = "tl_Name";
            this.tl_Name.Name = "tl_Name";
            this.tl_Name.OptionsColumn.AllowEdit = false;
            this.tl_Name.Visible = true;
            this.tl_Name.VisibleIndex = 0;
            // 
            // tl_Object
            // 
            this.tl_Object.Caption = "tl_Object";
            this.tl_Object.FieldName = "tl_Object";
            this.tl_Object.Name = "tl_Object";
            // 
            // gridControl1
            // 
            this.gridControl1.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.layoutView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1,
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(275, 288);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
            this.gridControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseDoubleClick);
            // 
            // layoutView1
            // 
            this.layoutView1.Appearance.FieldValue.Options.UseTextOptions = true;
            this.layoutView1.Appearance.FieldValue.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutView1.Appearance.FieldValue.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutView1.Appearance.FocusedCardCaption.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.layoutView1.Appearance.FocusedCardCaption.Options.UseForeColor = true;
            this.layoutView1.Appearance.SelectedCardCaption.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.layoutView1.Appearance.SelectedCardCaption.Options.UseForeColor = true;
            this.layoutView1.Appearance.SelectionFrame.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.layoutView1.Appearance.SelectionFrame.Options.UseForeColor = true;
            this.layoutView1.CardCaptionFormat = "{3}";
            this.layoutView1.CardHorzInterval = 6;
            this.layoutView1.CardMinSize = new System.Drawing.Size(80, 100);
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.colPicture,
            this.colName,
            this.colComment,
            this.colDuration,
            this.colLocation,
            this.colFilePath});
            this.layoutView1.GridControl = this.gridControl1;
            this.layoutView1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_layoutViewColumn1,
            this.layoutViewField_layoutViewColumn1_1,
            this.layoutViewField_colComment,
            this.layoutViewField_layoutViewColumn1_2,
            this.layoutViewField_colName});
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsBehavior.AllowExpandCollapse = false;
            this.layoutView1.OptionsBehavior.AllowPanCards = false;
            this.layoutView1.OptionsBehavior.Editable = false;
            this.layoutView1.OptionsBehavior.ScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.layoutView1.OptionsCustomization.AllowFilter = false;
            this.layoutView1.OptionsCustomization.AllowSort = false;
            this.layoutView1.OptionsHeaderPanel.EnableCarouselModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableColumnModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableMultiColumnModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableMultiRowModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnablePanButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableRowModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableSingleModeButton = false;
            this.layoutView1.OptionsItemText.TextToControlDistance = 0;
            this.layoutView1.OptionsMultiRecordMode.MultiRowScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
            this.layoutView1.OptionsView.AllowHotTrackFields = false;
            this.layoutView1.OptionsView.CardsAlignment = DevExpress.XtraGrid.Views.Layout.CardsAlignment.Near;
            this.layoutView1.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutView1.OptionsView.ShowCardExpandButton = false;
            this.layoutView1.OptionsView.ShowFieldHints = false;
            this.layoutView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.layoutView1.OptionsView.ShowHeaderPanel = false;
            this.layoutView1.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
            this.layoutView1.TemplateCard = this.layoutViewCard1;
            this.layoutView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.layoutView1_MouseUp);
            // 
            // colPicture
            // 
            this.colPicture.Caption = "图片";
            this.colPicture.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colPicture.FieldName = "Picture";
            this.colPicture.LayoutViewField = this.layoutViewField_colPicture;
            this.colPicture.Name = "colPicture";
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.AppearanceFocused.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.repositoryItemPictureEdit1.AppearanceFocused.Options.UseBorderColor = true;
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // layoutViewField_colPicture
            // 
            this.layoutViewField_colPicture.EditorPreferredWidth = 99;
            this.layoutViewField_colPicture.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutViewField_colPicture.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colPicture.Name = "layoutViewField_colPicture";
            this.layoutViewField_colPicture.Size = new System.Drawing.Size(103, 96);
            this.layoutViewField_colPicture.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_colPicture.TextToControlDistance = 0;
            this.layoutViewField_colPicture.TextVisible = false;
            // 
            // colName
            // 
            this.colName.Caption = "名称";
            this.colName.ColumnEdit = this.repositoryItemTextEdit1;
            this.colName.FieldName = "Name";
            this.colName.LayoutViewField = this.layoutViewField_colName;
            this.colName.Name = "colName";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AppearanceFocused.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.repositoryItemTextEdit1.AppearanceFocused.Options.UseForeColor = true;
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // layoutViewField_colName
            // 
            this.layoutViewField_colName.EditorPreferredWidth = 89;
            this.layoutViewField_colName.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutViewField_colName.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colName.Name = "layoutViewField_colName";
            this.layoutViewField_colName.Size = new System.Drawing.Size(93, 26);
            this.layoutViewField_colName.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_colName.TextToControlDistance = 0;
            this.layoutViewField_colName.TextVisible = false;
            // 
            // colComment
            // 
            this.colComment.Caption = "colComment";
            this.colComment.FieldName = "Comment";
            this.colComment.LayoutViewField = this.layoutViewField_colComment;
            this.colComment.Name = "colComment";
            // 
            // layoutViewField_colComment
            // 
            this.layoutViewField_colComment.EditorPreferredWidth = 10;
            this.layoutViewField_colComment.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colComment.Name = "layoutViewField_colComment";
            this.layoutViewField_colComment.Size = new System.Drawing.Size(93, 26);
            this.layoutViewField_colComment.TextSize = new System.Drawing.Size(65, 20);
            this.layoutViewField_colComment.TextToControlDistance = 0;
            // 
            // colDuration
            // 
            this.colDuration.Caption = "colDuration";
            this.colDuration.FieldName = "Duration";
            this.colDuration.LayoutViewField = this.layoutViewField_layoutViewColumn1;
            this.colDuration.Name = "colDuration";
            // 
            // layoutViewField_layoutViewColumn1
            // 
            this.layoutViewField_layoutViewColumn1.EditorPreferredWidth = 10;
            this.layoutViewField_layoutViewColumn1.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1.Name = "layoutViewField_layoutViewColumn1";
            this.layoutViewField_layoutViewColumn1.Size = new System.Drawing.Size(93, 26);
            this.layoutViewField_layoutViewColumn1.TextSize = new System.Drawing.Size(60, 20);
            this.layoutViewField_layoutViewColumn1.TextToControlDistance = 0;
            // 
            // colLocation
            // 
            this.colLocation.Caption = "colLocation";
            this.colLocation.FieldName = "Location";
            this.colLocation.LayoutViewField = this.layoutViewField_layoutViewColumn1_1;
            this.colLocation.Name = "colLocation";
            // 
            // layoutViewField_layoutViewColumn1_1
            // 
            this.layoutViewField_layoutViewColumn1_1.EditorPreferredWidth = 10;
            this.layoutViewField_layoutViewColumn1_1.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_1.Name = "layoutViewField_layoutViewColumn1_1";
            this.layoutViewField_layoutViewColumn1_1.Size = new System.Drawing.Size(93, 26);
            this.layoutViewField_layoutViewColumn1_1.TextSize = new System.Drawing.Size(65, 20);
            this.layoutViewField_layoutViewColumn1_1.TextToControlDistance = 0;
            // 
            // colFilePath
            // 
            this.colFilePath.Caption = "FilePath";
            this.colFilePath.FieldName = "FilePath";
            this.colFilePath.LayoutViewField = this.layoutViewField_layoutViewColumn1_2;
            this.colFilePath.Name = "colFilePath";
            // 
            // layoutViewField_layoutViewColumn1_2
            // 
            this.layoutViewField_layoutViewColumn1_2.EditorPreferredWidth = 10;
            this.layoutViewField_layoutViewColumn1_2.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_2.Name = "layoutViewField_layoutViewColumn1_2";
            this.layoutViewField_layoutViewColumn1_2.Size = new System.Drawing.Size(93, 26);
            this.layoutViewField_layoutViewColumn1_2.TextSize = new System.Drawing.Size(72, 20);
            this.layoutViewField_layoutViewColumn1_2.TextToControlDistance = 0;
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "TemplateCard";
            this.layoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_colPicture,
            this.item1});
            this.layoutViewCard1.Name = "layoutViewCard1";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 0;
            this.layoutViewCard1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // item1
            // 
            this.item1.AllowHotTrack = false;
            this.item1.CustomizationFormText = "item1";
            this.item1.Location = new System.Drawing.Point(0, 96);
            this.item1.Name = "item1";
            this.item1.Size = new System.Drawing.Size(103, 2);
            this.item1.Text = "item1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 24);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(275, 471);
            this.splitContainerControl1.SplitterPosition = 178;
            this.splitContainerControl1.TabIndex = 14;
            this.splitContainerControl1.Text = "splitContainerControl1";
            this.splitContainerControl1.SplitGroupPanelCollapsed += new DevExpress.XtraEditors.SplitGroupPanelCollapsedEventHandler(this.splitContainerControl1_SplitGroupPanelCollapsed);
            // 
            // popupMenuLocation
            // 
            this.popupMenuLocation.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barReplace),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barInsert)});
            this.popupMenuLocation.Manager = this.barManager1;
            this.popupMenuLocation.Name = "popupMenuLocation";
            // 
            // barEdit
            // 
            this.barEdit.Caption = "编辑";
            this.barEdit.Id = 10;
            this.barEdit.Name = "barEdit";
            this.barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit_ItemClick);
            // 
            // barReplace
            // 
            this.barReplace.Caption = "替换";
            this.barReplace.Id = 11;
            this.barReplace.Name = "barReplace";
            this.barReplace.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReplace_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除";
            this.barDelete.Id = 12;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barInsert
            // 
            this.barInsert.Caption = "插入";
            this.barInsert.Id = 13;
            this.barInsert.Name = "barInsert";
            this.barInsert.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barInsert_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnShowGroup,
            this.btnNewLocation,
            this.btnNewGroup,
            this.btnForwardLocation,
            this.btnBackwardLocation,
            this.barPlayGroup,
            this.barDeleteGroup,
            this.barRenameGroup,
            this.barEdit,
            this.barReplace,
            this.barDelete,
            this.barInsert,
            this.btn_ImportXml,
            this.btn_ExportXml,
            this.barStopGroup});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 18;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(344, 114);
            this.bar2.FloatSize = new System.Drawing.Size(252, 31);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnShowGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNewGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNewLocation),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnForwardLocation),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBackwardLocation)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.Text = "Main menu";
            // 
            // btnShowGroup
            // 
            this.btnShowGroup.Caption = "隐藏组";
            this.btnShowGroup.Glyph = ((System.Drawing.Image)(resources.GetObject("btnShowGroup.Glyph")));
            this.btnShowGroup.Id = 0;
            this.btnShowGroup.Name = "btnShowGroup";
            this.btnShowGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowGroup_ItemClick);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Caption = "新建组";
            this.btnNewGroup.Glyph = ((System.Drawing.Image)(resources.GetObject("btnNewGroup.Glyph")));
            this.btnNewGroup.Id = 2;
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNewGroup_ItemClick);
            // 
            // btnNewLocation
            // 
            this.btnNewLocation.Caption = "新建特定场景";
            this.btnNewLocation.Glyph = ((System.Drawing.Image)(resources.GetObject("btnNewLocation.Glyph")));
            this.btnNewLocation.Id = 1;
            this.btnNewLocation.Name = "btnNewLocation";
            this.btnNewLocation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNewLocation_ItemClick);
            // 
            // btnForwardLocation
            // 
            this.btnForwardLocation.Caption = "特定场景前移";
            this.btnForwardLocation.Glyph = ((System.Drawing.Image)(resources.GetObject("btnForwardLocation.Glyph")));
            this.btnForwardLocation.Id = 3;
            this.btnForwardLocation.Name = "btnForwardLocation";
            this.btnForwardLocation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnForwardLocation_ItemClick);
            // 
            // btnBackwardLocation
            // 
            this.btnBackwardLocation.Caption = "特定场景后移";
            this.btnBackwardLocation.Glyph = ((System.Drawing.Image)(resources.GetObject("btnBackwardLocation.Glyph")));
            this.btnBackwardLocation.Id = 4;
            this.btnBackwardLocation.Name = "btnBackwardLocation";
            this.btnBackwardLocation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBackwardLocation_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(275, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 495);
            this.barDockControlBottom.Size = new System.Drawing.Size(275, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 471);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(275, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 471);
            // 
            // barPlayGroup
            // 
            this.barPlayGroup.Caption = "播放";
            this.barPlayGroup.Id = 7;
            this.barPlayGroup.Name = "barPlayGroup";
            this.barPlayGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPlayGroup_ItemClick);
            // 
            // barDeleteGroup
            // 
            this.barDeleteGroup.Caption = "删除";
            this.barDeleteGroup.Id = 8;
            this.barDeleteGroup.Name = "barDeleteGroup";
            this.barDeleteGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDeleteGroup_ItemClick);
            // 
            // barRenameGroup
            // 
            this.barRenameGroup.Caption = "重命名";
            this.barRenameGroup.Id = 9;
            this.barRenameGroup.Name = "barRenameGroup";
            this.barRenameGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRenameGroup_ItemClick);
            // 
            // btn_ImportXml
            // 
            this.btn_ImportXml.Caption = "导入";
            this.btn_ImportXml.Id = 14;
            this.btn_ImportXml.Name = "btn_ImportXml";
            this.btn_ImportXml.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ImportXml_ItemClick);
            // 
            // btn_ExportXml
            // 
            this.btn_ExportXml.Caption = "导出";
            this.btn_ExportXml.Id = 15;
            this.btn_ExportXml.Name = "btn_ExportXml";
            this.btn_ExportXml.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ExportXml_ItemClick);
            // 
            // barStopGroup
            // 
            this.barStopGroup.Caption = "停止";
            this.barStopGroup.Id = 17;
            this.barStopGroup.Name = "barStopGroup";
            this.barStopGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barStopGroup_ItemClick);
            // 
            // popupMenuGroup
            // 
            this.popupMenuGroup.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barPlayGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStopGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRenameGroup, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDeleteGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.btn_ImportXml, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btn_ExportXml)});
            this.popupMenuGroup.Manager = this.barManager1;
            this.popupMenuGroup.Name = "popupMenuGroup";
            // 
            // UCCertainScene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCCertainScene";
            this.Size = new System.Drawing.Size(275, 495);
            this.Load += new System.EventHandler(this.UCCertainScene_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.item1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGroup)).EndInit();
            this.ResumeLayout(false);

        }



    }
}
