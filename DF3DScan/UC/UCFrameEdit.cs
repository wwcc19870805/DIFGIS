using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.Core;
using DF3DControl.Base;
using DF3DScan.Frm;

namespace DF3DScan.UC
{
    public class UCFrameEdit : XtraUserControl
    {
        private ICameraTour tour;
        private System.Data.DataTable dt;
        private bool isChangedByEditTotal;
        private bool isChangedByEditCell = true;
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFrameEdit));
        private System.ComponentModel.IContainer components;
        private BarManager barManager1;
        private Bar barFrame;
        private BarDockControl barDockControlTop;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarButtonItem barCreate;
        private BarButtonItem barReplace;
        private BarButtonItem barDelete;
        private BarButtonItem barPlayFromNow;
        private BarButtonItem barPlayFromPast;
        private BarButtonItem barStop;
        private BarButtonItem barReturn;
        private LayoutControl layoutControl1;
        private GridControl gridControl1;
        private GridView gridView1;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutControlItem1;
        private SpinEdit txtTotalTime;
        private LayoutControlItem layoutControlItem2;
        private GridColumn col_ID;
        private GridColumn col_Position;
        private GridColumn col_Heading;
        private GridColumn col_Pitch;
        private GridColumn col_Roll;
        private GridColumn col_Duration;
        private GridColumn col_Mode;
        private System.Windows.Forms.ContextMenuStrip contextMenu1;
        private System.Windows.Forms.ToolStripMenuItem contextEdit;
        private System.Windows.Forms.ToolStripMenuItem contextDelete;
        private ImageCollection imageCollection1;
        private AxRenderControl d3;
        public UCFrameEdit(ICameraTour tour)
        {
            this.InitializeComponent();
            d3 = DF3DApplication.Application.Current3DMapControl;
            // 注册播放事件
            d3.RcCameraTourWaypointChanged -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraTourWaypointChangedEventHandler(this.AxRenderControl_RcCameraTourWaypointChanged);
            d3.RcCameraTourWaypointChanged += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraTourWaypointChangedEventHandler(this.AxRenderControl_RcCameraTourWaypointChanged);
            d3.RcCameraFlyFinished -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraFlyFinishedEventHandler(this.AxRenderControl_RcCameraFlyFinished);
            d3.RcCameraFlyFinished += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraFlyFinishedEventHandler(this.AxRenderControl_RcCameraFlyFinished);
            this.txtTotalTime.Properties.MinValue = 0m;
            this.txtTotalTime.Properties.MaxValue = 65535m;
            this.tour = tour;
            this.ConstructTable();
            if (tour.WaypointsNumber == 0)
            {
                this.barCreate.Enabled = true;
                this.barReplace.Enabled = false;
                this.barDelete.Enabled = false;
                this.barPlayFromNow.Enabled = false;
                this.barPlayFromPast.Enabled = false;
                this.barStop.Enabled = false;
            }
            else
            {
                this.barCreate.Enabled = true;
                this.barReplace.Enabled = true;
                this.barDelete.Enabled = true;
                this.barPlayFromNow.Enabled = true;
                this.barPlayFromPast.Enabled = true;
                this.barStop.Enabled = false;
            }
            this.gridView1.Focus();
        }

        // 创建关键帧属性表
        private void ConstructTable()
        {
            this.dt = new System.Data.DataTable();
            System.Data.DataColumn dataColumn = new System.Data.DataColumn("ID", typeof(int));
            System.Data.DataColumn dataColumn2 = new System.Data.DataColumn("Position", typeof(object));
            System.Data.DataColumn dataColumn3 = new System.Data.DataColumn("Heading", typeof(double));
            System.Data.DataColumn dataColumn4 = new System.Data.DataColumn("Pitch", typeof(double));
            System.Data.DataColumn dataColumn5 = new System.Data.DataColumn("Roll", typeof(double));
            System.Data.DataColumn dataColumn6 = new System.Data.DataColumn("FlyTime", typeof(double));
            System.Data.DataColumn dataColumn7 = new System.Data.DataColumn("FlyMode", typeof(string));
            this.dt.Columns.AddRange(new System.Data.DataColumn[]
			{
				dataColumn, 
				dataColumn2, 
				dataColumn3, 
				dataColumn4, 
				dataColumn5, 
				dataColumn6, 
				dataColumn7
			});
            this.gridControl1.DataSource = this.dt;
            RepositoryItemSpinEdit repositoryItemSpinEdit = new RepositoryItemSpinEdit();
            repositoryItemSpinEdit.MinValue = 0m;
            repositoryItemSpinEdit.MaxValue = 79228162514264337593543950335m;
            this.gridView1.Columns[5].ColumnEdit = repositoryItemSpinEdit;
            RepositoryItemComboBox repositoryItemComboBox = new RepositoryItemComboBox();
            repositoryItemComboBox.Items.Add("Smooth");
            repositoryItemComboBox.Items.Add("Bounce");
            repositoryItemComboBox.Items.Add("Linear");
            repositoryItemComboBox.CycleOnDblClick = true;
            repositoryItemComboBox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            repositoryItemComboBox.DoubleClick += new System.EventHandler(this.modeEdit_DoubleClick);
            this.gridView1.Columns[6].ColumnEdit = repositoryItemComboBox;
            if (this.tour.WaypointsNumber > 0)
            {
                for (int i = 0; i < this.tour.WaypointsNumber; i++)
                {
                    IVector3 value;
                    IEulerAngle eulerAngle;
                    double value2;
                    gviCameraTourMode mode;
                    this.tour.GetWaypoint(i, out value, out eulerAngle, out value2, out mode);
                    System.Data.DataRow dataRow = this.dt.NewRow();
                    dataRow[0] = i + 1;
                    dataRow[1] = value;
                    dataRow[2] = eulerAngle.Heading;
                    dataRow[3] = eulerAngle.Tilt;
                    dataRow[4] = eulerAngle.Roll;
                    dataRow[5] = System.Math.Round(value2, 2);
                    dataRow[6] = this.getModeString(mode);
                    this.dt.Rows.Add(dataRow);
                }
                this.txtTotalTime.Value = System.Math.Round((decimal)this.tour.TotalTime, 2);
            }
        }
        // 获取动画导航相机运动轨迹的插值模式
        private string getModeString(gviCameraTourMode mode)
        {
            switch (mode)
            {
                case gviCameraTourMode.gviCameraTourLinear:
                    {
                        return "Linear";
                    }
                case gviCameraTourMode.gviCameraTourSmooth:
                    {
                        return "Smooth";
                    }
                case gviCameraTourMode.gviCameraTourBounce:
                    {
                        return "Bounce";
                    }
                default:
                    {
                        return "";
                    }
            }
        }
        private gviCameraTourMode getModeEnum(string modestring)
        {
            if (modestring != null)
            {
                if (modestring == "Bounce")
                {
                    return gviCameraTourMode.gviCameraTourBounce;
                }
                if (modestring == "Smooth")
                {
                    return gviCameraTourMode.gviCameraTourSmooth;
                }
                if (modestring == "Linear")
                {
                    return gviCameraTourMode.gviCameraTourLinear;
                }
            }
            return gviCameraTourMode.gviCameraTourSmooth;
        }

        private void barReturn_ItemClick(object sender, ItemClickEventArgs e)
        {
            d3.RcCameraTourWaypointChanged -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraTourWaypointChangedEventHandler(this.AxRenderControl_RcCameraTourWaypointChanged);
            d3.RcCameraFlyFinished -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraFlyFinishedEventHandler(this.AxRenderControl_RcCameraFlyFinished);
            (this.Parent as UCCameraTour).ReLoadCameraTourpanel();
            //RightPanelService.ReLoadCameraTourpanel();
        }
        private void barCreate_ItemClick(object sender, ItemClickEventArgs e)
        {
            IVector3 vector;
            IEulerAngle eulerAngle;
            d3.Camera.GetCamera(out vector, out eulerAngle);
            int waypointsNumber = this.tour.WaypointsNumber;
            System.Data.DataRow dataRow = this.dt.NewRow();
            dataRow[1] = vector;
            dataRow[2] = eulerAngle.Heading;
            dataRow[3] = eulerAngle.Tilt;
            dataRow[4] = eulerAngle.Roll;
            dataRow[5] = "5";
            dataRow[6] = "Smooth";
            int[] selectedRows = this.gridView1.GetSelectedRows();
            if (selectedRows.Length == 0 || selectedRows.Length > 1 || (selectedRows.Length == 1 && selectedRows[0] == waypointsNumber - 1))
            {
                this.tour.AddWaypoint(vector, eulerAngle, 5.0, gviCameraTourMode.gviCameraTourSmooth);
                if (this.tour.WaypointsNumber != waypointsNumber + 1)
                {
                    XtraMessageBox.Show("添加动画导航节点错误！","提示");
                    return;
                }
                this.dt.Rows.Add(dataRow);
                this.gridView1.ClearSelection();
                this.gridView1.SelectRow(this.gridView1.RowCount - 1);
            }
            else
            {
                int num = selectedRows[0];
                this.tour.InsertWaypoint(num + 1, vector, eulerAngle, 5.0, gviCameraTourMode.gviCameraTourSmooth);
                if (this.tour.WaypointsNumber != waypointsNumber + 1)
                {
                    XtraMessageBox.Show("插入动画导航节点错误！", "提示");
                    return;
                }
                this.dt.Rows.InsertAt(dataRow, num + 1);
                this.gridView1.ClearSelection();
                this.gridView1.SelectRow(num + 1);
            }
            int num2 = 0;
            foreach (System.Data.DataRow dataRow2 in this.dt.Rows)
            {
                dataRow2[0] = ++num2;
            }
            this.txtTotalTime.Value = System.Math.Round((decimal)this.tour.TotalTime, 2);
            this.barCreate.Enabled = true;
            this.barReplace.Enabled = true;
            this.barDelete.Enabled = true;
            this.barPlayFromNow.Enabled = true;
            this.barPlayFromPast.Enabled = true;
            this.barStop.Enabled = false;
            this.gridView1.Focus();
            //WorkSpaceServices.Instance().NeedSaveProject = true;
        }
        private void barReplace_ItemClick(object sender, ItemClickEventArgs e)
        {
            int[] selectedRows = this.gridView1.GetSelectedRows();
            if (selectedRows.Length == 1 && XtraMessageBox.Show("是否确定替换该动画导航节点！","提示", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                IVector3 vector;
                IEulerAngle eulerAngle;
                d3.Camera.GetCamera(out vector, out eulerAngle);
                this.tour.ModifyWaypoint(selectedRows[0], vector, eulerAngle, 5.0, gviCameraTourMode.gviCameraTourSmooth);
                System.Data.DataRow dataRow = this.dt.Rows[selectedRows[0]];
                dataRow[1] = vector;
                dataRow[2] = eulerAngle.Heading;
                dataRow[3] = eulerAngle.Tilt;
                dataRow[4] = eulerAngle.Roll;
                dataRow[5] = "5";
                dataRow[6] = "Smooth";
                this.txtTotalTime.Value = System.Math.Round((decimal)this.tour.TotalTime, 2);
                this.gridView1.Focus();
                //WorkSpaceServices.Instance().NeedSaveProject = true;
            }
        }
        private void deleteFrame(int[] selectRows)
        {
            if (selectRows.Length > 0)
            {
                if (XtraMessageBox.Show("是否确定删除该动画导航节点！", "提示", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (selectRows.Length == this.dt.Rows.Count)
                    {
                        this.dt.Rows.Clear();
                        while (this.tour.WaypointsNumber != 0)
                        {
                            this.tour.DeleteWaypoint(this.tour.WaypointsNumber - 1);
                        }
                    }
                    else
                    {
                        int num = selectRows[selectRows.Length - 1];
                        if (this.gridView1.RowCount > 1 && num == this.gridView1.RowCount - 1)
                        {
                            this.gridView1.ClearSelection();
                            this.gridView1.SelectRow(num - selectRows.Length);
                        }
                        else
                        {
                            this.gridView1.ClearSelection();
                            this.gridView1.SelectRow(num + 1);
                        }
                        for (int i = 0; i < selectRows.Length; i++)
                        {
                            if (i == 0)
                            {
                                this.dt.Rows.RemoveAt(selectRows[i]);
                                this.tour.DeleteWaypoint(selectRows[i]);
                            }
                            else
                            {
                                this.dt.Rows.RemoveAt(selectRows[i] - i);
                                this.tour.DeleteWaypoint(selectRows[i] - i);
                            }
                        }
                    }
                }
                if (this.dt.Rows.Count > 0)
                {
                    int num2 = 1;
                    foreach (System.Data.DataRow dataRow in this.dt.Rows)
                    {
                        dataRow[0] = num2;
                        num2++;
                    }
                }
                this.txtTotalTime.Value = System.Math.Round((decimal)this.tour.TotalTime, 2);
                if (this.tour.WaypointsNumber == 0)
                {
                    this.barCreate.Enabled = true;
                    this.barReplace.Enabled = false;
                    this.barDelete.Enabled = false;
                    this.barPlayFromNow.Enabled = false;
                    this.barPlayFromPast.Enabled = false;
                    this.barStop.Enabled = false;
                }
                else
                {
                    this.barCreate.Enabled = true;
                    if (this.gridView1.GetSelectedRows().Length > 1)
                    {
                        this.barReplace.Enabled = false;
                    }
                    else
                    {
                        this.barReplace.Enabled = true;
                    }
                    this.barDelete.Enabled = true;
                    this.barPlayFromNow.Enabled = true;
                    this.barPlayFromPast.Enabled = true;
                    this.barStop.Enabled = false;
                }
                //WorkSpaceServices.Instance().NeedSaveProject = true;
            }
        }
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            int[] selectedRows = this.gridView1.GetSelectedRows();
            this.deleteFrame(selectedRows);
        }
        private void barPlayFromNow_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (d3.InteractMode != gviInteractMode.gviInteractNormal)
            {
                d3.InteractMode = gviInteractMode.gviInteractNormal;
                DF3DApplication.Application.Workbench.BarPerformClick("Wander");
            }
            d3.Viewport.ActiveView = 0;
            if (this.tour != null && this.barPlayFromNow.Caption == "播放")            
            {
                if (this.tour.WaypointsNumber == 0)
                {
                    return;
                }
                if (this.tour.WaypointsNumber == 1)
                {
                    IVector3 position;
                    IEulerAngle angle;
                    double num;
                    gviCameraTourMode gviCameraTourMode;
                    this.tour.GetWaypoint(0, out position, out angle, out num, out gviCameraTourMode);
                    d3.Camera.SetCamera(position, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                    return;
                }
                this.barPlayFromNow.ImageIndex = 4;
                this.barPlayFromNow.Caption = "暂停";
                int[] selectedRows = this.gridView1.GetSelectedRows();
                if (selectedRows.Length == 1 || selectedRows.Length == 0)
                {
                    int num2 = 0;
                    if (selectedRows.Length == 1)
                    {
                        num2 = selectedRows[0];
                    }
                    this.gridView1.ClearSelection();
                    if (num2 == this.tour.WaypointsNumber - 1)
                    {
                        this.gridView1.SelectRow(0);
                        this.tour.Index = 0;
                    }
                    else
                    {
                        this.gridView1.SelectRow(num2);
                        this.tour.Index = num2;
                    }
                }
                this.tour.Play();
            }
            else
            {
                if (this.tour != null && this.barPlayFromNow.Caption == "暂停")
                {
                    this.barPlayFromNow.ImageIndex = 3;
                    this.barPlayFromNow.Caption = "播放";
                    this.tour.Pause();
                }
            }
            this.barCreate.Enabled = false;
            this.barReplace.Enabled = false;
            this.barDelete.Enabled = false;
            this.barPlayFromNow.Enabled = true;
            this.barPlayFromPast.Enabled = false;
            this.barStop.Enabled = true;
            this.barReturn.Enabled = false;
            this.txtTotalTime.Enabled = false;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridControl1.Enabled = false;
        }
        private void barPlayFromPast_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (d3.InteractMode != gviInteractMode.gviInteractNormal)
            {
                d3.InteractMode = gviInteractMode.gviInteractNormal;
                DF3DApplication.Application.Workbench.BarPerformClick("Wander");
            }
            d3.Viewport.ActiveView = 0;
            if (this.tour != null && (this.barPlayFromPast.Caption == "前一侦播放") || this.barPlayFromPast.Caption == "继续")
            {
                if (this.tour.WaypointsNumber == 0)
                {
                    return;
                }
                if (this.tour.WaypointsNumber == 1)
                {
                    IVector3 position;
                    IEulerAngle angle;
                    double num;
                    gviCameraTourMode gviCameraTourMode;
                    this.tour.GetWaypoint(0, out position, out angle, out num, out gviCameraTourMode);
                    d3.Camera.SetCamera(position, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                    return;
                }
                this.barPlayFromPast.ImageIndex = 4;
                this.barPlayFromPast.Caption = "暂停";
                int[] selectedRows = this.gridView1.GetSelectedRows();
                if (selectedRows.Length == 1)
                {
                    int num2 = selectedRows[0];
                    this.gridView1.ClearSelection();
                    if (num2 == 0)
                    {
                        this.gridView1.SelectRow(0);
                        this.tour.Index = 0;
                    }
                    else
                    {
                        this.gridView1.SelectRow(num2 - 1);
                        this.tour.Index = num2 - 1;
                    }
                }
                this.tour.Play();
            }
            else
            {
                if (this.tour != null && this.barPlayFromPast.Caption == "暂停")
                {
                    this.barPlayFromPast.ImageIndex = 5;
                    this.barPlayFromPast.Caption = "继续";
                    this.tour.Pause();
                }
            }
            this.barCreate.Enabled = false;
            this.barReplace.Enabled = false;
            this.barDelete.Enabled = false;
            this.barPlayFromNow.Enabled = false;
            this.barPlayFromPast.Enabled = true;
            this.barStop.Enabled = true;
            this.barReturn.Enabled = false;
            this.txtTotalTime.Enabled = false;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridControl1.Enabled = false;
        }
        private void stopTour()
        {
            this.barPlayFromNow.ImageIndex = 3;
            this.barPlayFromNow.Caption = "播放";
            this.barPlayFromPast.ImageIndex = 5;
            this.barPlayFromPast.Caption = "前一侦播放";
            this.tour.Stop();
            this.barCreate.Enabled = true;
            this.barReplace.Enabled = true;
            this.barDelete.Enabled = true;
            this.barPlayFromNow.Enabled = true;
            this.barPlayFromPast.Enabled = true;
            this.barStop.Enabled = false;
            this.barReturn.Enabled = true;
            this.txtTotalTime.Enabled = true;
            this.gridView1.OptionsBehavior.Editable = true;
            this.gridControl1.Enabled = true;
            this.gridView1.Focus();
        }
        private void barStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            d3.Viewport.ActiveView = 0;
            this.stopTour();
        }
        private void modeEdit_DoubleClick(object sender, System.EventArgs e)
        {
            System.Data.DataRow focusedDataRow = this.gridView1.GetFocusedDataRow();
            int focusedRowHandle = this.gridView1.FocusedRowHandle;
            IVector3 position;
            IEulerAngle angle;
            double duration;
            gviCameraTourMode gviCameraTourMode;
            this.tour.GetWaypoint(focusedRowHandle, out position, out angle, out duration, out gviCameraTourMode);
            ComboBoxEdit comboBoxEdit = sender as ComboBoxEdit;
            if (gviCameraTourMode == gviCameraTourMode.gviCameraTourSmooth)
            {
                this.tour.ModifyWaypoint(focusedRowHandle, position, angle, duration, this.getModeEnum("Bounce"));
                focusedDataRow[6] = (comboBoxEdit.EditValue = "Bounce");
            }
            else
            {
                if (gviCameraTourMode == gviCameraTourMode.gviCameraTourBounce)
                {
                    this.tour.ModifyWaypoint(focusedRowHandle, position, angle, duration, this.getModeEnum("Linear"));
                    focusedDataRow[6] = (comboBoxEdit.EditValue = "Linear");
                }
                else
                {
                    if (gviCameraTourMode == gviCameraTourMode.gviCameraTourLinear)
                    {
                        this.tour.ModifyWaypoint(focusedRowHandle, position, angle, duration, this.getModeEnum("Smooth"));
                        focusedDataRow[6] = (comboBoxEdit.EditValue = "Smooth");
                    }
                }
            }
            //WorkSpaceServices.Instance().NeedSaveProject = true;
        }
        private void gridControl1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GridHitInfo gridHitInfo = this.gridView1.CalcHitInfo(e.Location);
            if (gridHitInfo != null && gridHitInfo.HitTest == GridHitTest.EmptyRow)
            {
                this.gridView1.ClearSelection();
                return;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.contextEdit.Enabled = false;
                this.contextDelete.Enabled = false;
                if (gridHitInfo.InColumn && gridHitInfo.Column.FieldName.ToUpper() != "ID")
                {
                    this.contextMenu1.Show(System.Windows.Forms.Control.MousePosition);
                    this.gridView1.FocusedColumn = gridHitInfo.Column;
                    this.contextEdit.Enabled = true;
                    return;
                }
                if (gridHitInfo.InRowCell)
                {
                    this.contextMenu1.Show(System.Windows.Forms.Control.MousePosition);
                    this.contextDelete.Enabled = true;
                }
            }
        }
        private void contextDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                int[] selectedRows = this.gridView1.GetSelectedRows();
                if (selectedRows.Length == 0)
                {
                    XtraMessageBox.Show("没有选择要删除的记录！", "提示");
                }
                else
                {
                    this.deleteFrame(selectedRows);
                }
            }
            catch (System.Exception e2)
            {
            }
        }
        private void contextEdit_Click(object sender, System.EventArgs e)
        {
            try
            {
                int[] selectedRows = this.gridView1.GetSelectedRows();
                if (selectedRows.Length == 0)
                {
                    XtraMessageBox.Show("没有选择要修改的记录！", "提示");
                }
                else
                {
                    string fieldName = this.gridView1.FocusedColumn.FieldName;
                    if (!(fieldName == "ID"))
                    {
                        this.gridView1.FocusedColumn.SortOrder = ColumnSortOrder.None;
                        double maxValue = 65535.0;
                        double num = 0.0;
                        int num2 = 0;
                        int num3 = 0;
                        while (num3 < selectedRows.Length && selectedRows[num3] != this.tour.WaypointsNumber - 1)
                        {
                            IVector3 vector;
                            IEulerAngle eulerAngle;
                            double num4;
                            gviCameraTourMode gviCameraTourMode;
                            this.tour.GetWaypoint(selectedRows[num3], out vector, out eulerAngle, out num4, out gviCameraTourMode);
                            num += num4;
                            num2++;
                            num3++;
                        }
                        if (num2 != 0)
                        {
                            maxValue = (65535.0 - (this.tour.TotalTime - num)) / (double)num2;
                        }
                        AttributeEditDlg attributeEditDlg = new AttributeEditDlg(fieldName, maxValue);
                        if (attributeEditDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string inputValue = attributeEditDlg.InputValue;
                            this.gridView1.BeginDataUpdate();
                            for (int i = 0; i < selectedRows.Length; i++)
                            {
                                System.Data.DataRow dataRow = this.dt.Rows[selectedRows[i]];
                                if (dataRow != null)
                                {
                                    IEulerAngle eulerAngle2 = new EulerAngleClass();
                                    IVector3 position = (IVector3)dataRow[1];
                                    eulerAngle2.Heading = double.Parse(dataRow[2].ToString());
                                    eulerAngle2.Tilt = double.Parse(dataRow[3].ToString());
                                    eulerAngle2.Roll = double.Parse(dataRow[4].ToString());
                                    double num5;
                                    if (fieldName =="FlyTime")
                                    {
                                        num5 = double.Parse(inputValue.ToString());
                                        dataRow[5] = System.Math.Round(num5, 2);
                                    }
                                    else
                                    {
                                        num5 = double.Parse(dataRow[5].ToString());
                                    }
                                    gviCameraTourMode modeEnum;
                                    if (fieldName == "FlyMode")
                                    {
                                        modeEnum = this.getModeEnum(inputValue.ToString());
                                        dataRow[6] = inputValue;
                                    }
                                    else
                                    {
                                        modeEnum = this.getModeEnum(dataRow[6].ToString());
                                    }
                                    this.tour.ModifyWaypoint(selectedRows[i], position, eulerAngle2, num5, modeEnum);
                                }
                            }
                            this.txtTotalTime.Value = System.Math.Round((decimal)this.tour.TotalTime, 2);
                            this.gridView1.EndDataUpdate();
                            //WorkSpaceServices.Instance().NeedSaveProject = true;
                        }
                    }
                }
            }
            catch (System.Exception e2)
            {
            }
        }
        private void gridControl1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                GridHitInfo gridHitInfo = this.gridView1.CalcHitInfo(e.Location);
                if (gridHitInfo != null && gridHitInfo.HitTest == GridHitTest.EmptyRow)
                {
                    this.gridView1.ClearSelection();
                }
                else
                {
                    if (this.gridView1.OptionsBehavior.Editable)
                    {
                        if (d3.InteractMode != gviInteractMode.gviInteractNormal)
                        {
                            d3.InteractMode = gviInteractMode.gviInteractNormal;
                            DF3DApplication.Application.Workbench.BarPerformClick("Wander");
                        }
                        if (d3.Camera.FlyTime != 0.0)
                        {
                            d3.Camera.FlyTime = 0.0;
                        }
                        object focusedRow = this.gridView1.GetFocusedRow();
                        if (focusedRow != null && gridHitInfo.Column.FieldName.ToUpper() == "ID")
                        {
                            System.Data.DataRowView dataRowView = focusedRow as System.Data.DataRowView;
                            if (dataRowView != null && dataRowView.Row != null)
                            {
                                IVector3 position = (IVector3)dataRowView.Row[1];
                                IEulerAngle eulerAngle = new EulerAngleClass();
                                double heading = double.Parse(dataRowView.Row[2].ToString());
                                double tilt = double.Parse(dataRowView.Row[3].ToString());
                                eulerAngle.Heading = heading;
                                eulerAngle.Tilt = tilt;
                                eulerAngle.Roll = 0.0;
                                d3.Camera.SetCamera(position, eulerAngle, gviSetCameraFlags.gviSetCameraNoFlags);
                            }
                        }
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (!this.isChangedByEditCell)
            {
                return;
            }
            int focusedRowHandle = this.gridView1.FocusedRowHandle;
            if (this.dt.Rows.Count <= focusedRowHandle)
            {
                return;
            }
            double totalTime = this.tour.TotalTime;
            System.Data.DataRow dataRow = this.dt.Rows[focusedRowHandle];
            IVector3 position;
            IEulerAngle eulerAngle;
            double num;
            gviCameraTourMode modeEnum;
            this.tour.GetWaypoint(focusedRowHandle, out position, out eulerAngle, out num, out modeEnum);
            position = (IVector3)dataRow[1];
            eulerAngle.Heading = double.Parse(dataRow[2].ToString());
            eulerAngle.Tilt = double.Parse(dataRow[3].ToString());
            eulerAngle.Roll = double.Parse(dataRow[4].ToString());
            double duration = double.Parse(dataRow[5].ToString());
            modeEnum = this.getModeEnum(dataRow[6].ToString());
            this.tour.ModifyWaypoint(focusedRowHandle, position, eulerAngle, duration, modeEnum);
            this.txtTotalTime.Value = System.Math.Round((decimal)this.tour.TotalTime, 2);
            if (this.tour.TotalTime > 65535.0)
            {
                dataRow[5] = System.Math.Round(65535.0 - totalTime + num, 2);
                this.tour.ModifyWaypoint(focusedRowHandle, position, eulerAngle, 65535.0 - totalTime + num, modeEnum);
                this.txtTotalTime.Value = 65535m;
            }
            //WorkSpaceServices.Instance().NeedSaveProject = true;
        }
        private void gridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.gridView1.OptionsBehavior.Editable)
            {
                return;
            }
            int[] selectedRows = this.gridView1.GetSelectedRows();
            if (selectedRows.Length > 1)
            {
                this.barCreate.Enabled = true;
                this.barReplace.Enabled = false;
                this.barDelete.Enabled = true;
                this.barPlayFromNow.Enabled = false;
                this.barPlayFromPast.Enabled = false;
                this.barStop.Enabled = false;
                return;
            }
            if (selectedRows.Length == 1)
            {
                this.barCreate.Enabled = true;
                this.barReplace.Enabled = true;
                this.barDelete.Enabled = true;
                this.barPlayFromNow.Enabled = true;
                this.barPlayFromPast.Enabled = true;
                this.barStop.Enabled = false;
                return;
            }
            this.barCreate.Enabled = true;
            this.barReplace.Enabled = false;
            this.barDelete.Enabled = false;
            this.barPlayFromNow.Enabled = true;
            this.barPlayFromPast.Enabled = false;
            this.barStop.Enabled = false;
        }
        // 播放过程中
        private void AxRenderControl_RcCameraTourWaypointChanged(object sender, _IRenderControlEvents_RcCameraTourWaypointChangedEvent e)
        {
            this.gridView1.ClearSelection();
            this.gridView1.SelectRow(e.index);
        }
        // 播放结束
        private void AxRenderControl_RcCameraFlyFinished(object sender, _IRenderControlEvents_RcCameraFlyFinishedEvent e)
        {
            if (this.gridView1.OptionsBehavior.Editable)
            {
                return;
            }
            this.stopTour();
            this.gridView1.ClearSelection();
            this.gridView1.SelectRow(0);
        }
        private void txtTotalTime_ValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (this.isChangedByEditTotal)
                {
                    if (this.tour.WaypointsNumber != 1)
                    {
                        if (this.tour.TotalTime == 0.0)
                        {
                            double num = 1.0;
                            double.TryParse(this.txtTotalTime.Value.ToString(), out num);
                            double num2 = num / (double)(this.tour.WaypointsNumber - 1);
                            int num3 = 0;
                            System.Collections.IEnumerator enumerator = this.dt.Rows.GetEnumerator();
                            try
                            {
                                while (enumerator.MoveNext())
                                {
                                    System.Data.DataRow dataRow = (System.Data.DataRow)enumerator.Current;
                                    dataRow[5] = System.Math.Round(num2, 2);
                                    IEulerAngle eulerAngle = new EulerAngleClass();
                                    IVector3 position = (IVector3)dataRow[1];
                                    eulerAngle.Heading = double.Parse(dataRow[2].ToString());
                                    eulerAngle.Tilt = double.Parse(dataRow[3].ToString());
                                    eulerAngle.Roll = double.Parse(dataRow[4].ToString());
                                    gviCameraTourMode modeEnum = this.getModeEnum(dataRow[6].ToString());
                                    this.tour.ModifyWaypoint(num3, position, eulerAngle, num2, modeEnum);
                                    num3++;
                                }
                                goto IL_222;
                            }
                            finally
                            {
                                System.IDisposable disposable = enumerator as System.IDisposable;
                                if (disposable != null)
                                {
                                    disposable.Dispose();
                                }
                            }
                        }
                        double totalTime = this.tour.TotalTime;
                        double num4 = 1.0;
                        double.TryParse(this.txtTotalTime.Value.ToString(), out num4);
                        double num5 = num4 / totalTime;
                        int num6 = 0;
                        foreach (System.Data.DataRow dataRow2 in this.dt.Rows)
                        {
                            IVector3 position2;
                            IEulerAngle angle;
                            double num7;
                            gviCameraTourMode mode;
                            this.tour.GetWaypoint(num6, out position2, out angle, out num7, out mode);
                            dataRow2[5] = System.Math.Round(num7 * num5, 2);
                            this.tour.ModifyWaypoint(num6, position2, angle, num7 * num5, mode);
                            num6++;
                        }
                    IL_222:
                        return;
                        //WorkSpaceServices.Instance().NeedSaveProject = true;
                    }
                }
            }
            catch (System.Exception e2)
            {
                this.txtTotalTime.Value = System.Math.Round(decimal.Parse(this.tour.TotalTime.ToString()), 2);
            }
        }
        private void txtTotalTime_Enter(object sender, System.EventArgs e)
        {
            this.gridView1.ClearSelection();
            this.isChangedByEditTotal = true;
            this.isChangedByEditCell = false;
        }
        private void txtTotalTime_Leave(object sender, System.EventArgs e)
        {
            double num = 1.0;
            double.TryParse(this.txtTotalTime.Value.ToString(), out num);
            if (num < 0.0)
            {
                this.txtTotalTime.Focus();
                this.txtTotalTime.Value = 0m;
                return;
            }
            if (num > 65535.0)
            {
                this.txtTotalTime.Focus();
                this.txtTotalTime.Value = 65535m;
                return;
            }
            this.isChangedByEditTotal = false;
            this.isChangedByEditCell = true;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFrameEdit));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barFrame = new DevExpress.XtraBars.Bar();
            this.barCreate = new DevExpress.XtraBars.BarButtonItem();
            this.barReplace = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barPlayFromNow = new DevExpress.XtraBars.BarButtonItem();
            this.barPlayFromPast = new DevExpress.XtraBars.BarButtonItem();
            this.barStop = new DevExpress.XtraBars.BarButtonItem();
            this.barReturn = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtTotalTime = new DevExpress.XtraEditors.SpinEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Position = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Heading = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Pitch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Roll = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Duration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Mode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.contextMenu1 = new System.Windows.Forms.ContextMenuStrip();
            this.contextEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextDelete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.contextMenu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barFrame});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barCreate,
            this.barReplace,
            this.barDelete,
            this.barPlayFromNow,
            this.barPlayFromPast,
            this.barStop,
            this.barReturn});
            this.barManager1.MaxItemId = 7;
            // 
            // barFrame
            // 
            this.barFrame.BarName = "Tools";
            this.barFrame.DockCol = 0;
            this.barFrame.DockRow = 0;
            this.barFrame.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barFrame.FloatLocation = new System.Drawing.Point(282, 97);
            this.barFrame.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCreate),
            new DevExpress.XtraBars.LinkPersistInfo(this.barReplace),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPlayFromNow),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPlayFromPast),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStop),
            new DevExpress.XtraBars.LinkPersistInfo(this.barReturn)});
            this.barFrame.Offset = 5;
            this.barFrame.OptionsBar.AllowQuickCustomization = false;
            this.barFrame.OptionsBar.DrawDragBorder = false;
            this.barFrame.Text = "Tools";
            // 
            // barCreate
            // 
            this.barCreate.Caption = "新建";
            this.barCreate.Id = 0;
            this.barCreate.ImageIndex = 0;
            this.barCreate.Name = "barCreate";
            this.barCreate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCreate_ItemClick);
            // 
            // barReplace
            // 
            this.barReplace.Caption = "替换";
            this.barReplace.Id = 1;
            this.barReplace.ImageIndex = 1;
            this.barReplace.Name = "barReplace";
            this.barReplace.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReplace_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除";
            this.barDelete.Id = 2;
            this.barDelete.ImageIndex = 2;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barPlayFromNow
            // 
            this.barPlayFromNow.Caption = "播放";
            this.barPlayFromNow.Id = 3;
            this.barPlayFromNow.ImageIndex = 3;
            this.barPlayFromNow.Name = "barPlayFromNow";
            this.barPlayFromNow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPlayFromNow_ItemClick);
            // 
            // barPlayFromPast
            // 
            this.barPlayFromPast.Caption = "前一侦播放";
            this.barPlayFromPast.Id = 4;
            this.barPlayFromPast.ImageIndex = 5;
            this.barPlayFromPast.Name = "barPlayFromPast";
            this.barPlayFromPast.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPlayFromPast_ItemClick);
            // 
            // barStop
            // 
            this.barStop.Caption = "停止";
            this.barStop.Id = 5;
            this.barStop.ImageIndex = 6;
            this.barStop.Name = "barStop";
            this.barStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barStop_ItemClick);
            // 
            // barReturn
            // 
            this.barReturn.Caption = "返回";
            this.barReturn.Id = 6;
            this.barReturn.ImageIndex = 7;
            this.barReturn.Name = "barReturn";
            this.barReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReturn_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(343, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 552);
            this.barDockControlBottom.Size = new System.Drawing.Size(343, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 521);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(343, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 521);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "create.png");
            this.imageCollection1.Images.SetKeyName(1, "refresh.png");
            this.imageCollection1.Images.SetKeyName(2, "delete.png");
            this.imageCollection1.Images.SetKeyName(3, "play.png");
            this.imageCollection1.Images.SetKeyName(4, "pause.png");
            this.imageCollection1.Images.SetKeyName(5, "playlastframe.png");
            this.imageCollection1.Images.SetKeyName(6, "stop.png");
            this.imageCollection1.Images.SetKeyName(7, "return.png");
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtTotalTime);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(343, 521);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtTotalTime
            // 
            this.txtTotalTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalTime.Location = new System.Drawing.Point(41, 497);
            this.txtTotalTime.MenuManager = this.barManager1;
            this.txtTotalTime.Name = "txtTotalTime";
            this.txtTotalTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtTotalTime.Size = new System.Drawing.Size(300, 22);
            this.txtTotalTime.StyleController = this.layoutControl1;
            this.txtTotalTime.TabIndex = 1;
            this.txtTotalTime.ValueChanged += new System.EventHandler(this.txtTotalTime_ValueChanged);
            this.txtTotalTime.Enter += new System.EventHandler(this.txtTotalTime_Enter);
            this.txtTotalTime.Leave += new System.EventHandler(this.txtTotalTime_Leave);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(339, 491);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseDoubleClick);
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.Gray;
            this.gridView1.Appearance.Row.Options.UseBackColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_ID,
            this.col_Position,
            this.col_Heading,
            this.col_Pitch,
            this.col_Roll,
            this.col_Duration,
            this.col_Mode});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // col_ID
            // 
            this.col_ID.Caption = "ID";
            this.col_ID.FieldName = "ID";
            this.col_ID.Name = "col_ID";
            this.col_ID.OptionsColumn.AllowEdit = false;
            this.col_ID.Visible = true;
            this.col_ID.VisibleIndex = 0;
            // 
            // col_Position
            // 
            this.col_Position.Caption = "Position";
            this.col_Position.FieldName = "Position";
            this.col_Position.Name = "col_Position";
            // 
            // col_Heading
            // 
            this.col_Heading.Caption = "Heading";
            this.col_Heading.FieldName = "Heading";
            this.col_Heading.Name = "col_Heading";
            // 
            // col_Pitch
            // 
            this.col_Pitch.Caption = "Pitch";
            this.col_Pitch.FieldName = "Pitch";
            this.col_Pitch.Name = "col_Pitch";
            // 
            // col_Roll
            // 
            this.col_Roll.Caption = "Roll";
            this.col_Roll.FieldName = "Roll";
            this.col_Roll.Name = "col_Roll";
            // 
            // col_Duration
            // 
            this.col_Duration.Caption = "飞行时间";
            this.col_Duration.FieldName = "FlyTime";
            this.col_Duration.Name = "col_Duration";
            this.col_Duration.Visible = true;
            this.col_Duration.VisibleIndex = 1;
            // 
            // col_Mode
            // 
            this.col_Mode.Caption = "飞行模式";
            this.col_Mode.FieldName = "FlyMode";
            this.col_Mode.Name = "col_Mode";
            this.col_Mode.Visible = true;
            this.col_Mode.VisibleIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(343, 521);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(343, 495);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtTotalTime;
            this.layoutControlItem2.CustomizationFormText = "总时长";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 495);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(343, 26);
            this.layoutControlItem2.Text = "总时长";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 14);
            // 
            // contextMenu1
            // 
            this.contextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextEdit,
            this.contextDelete});
            this.contextMenu1.Name = "contextMenu1";
            this.contextMenu1.Size = new System.Drawing.Size(101, 48);
            // 
            // contextEdit
            // 
            this.contextEdit.Name = "contextEdit";
            this.contextEdit.Size = new System.Drawing.Size(100, 22);
            this.contextEdit.Text = "修改";
            this.contextEdit.Click += new System.EventHandler(this.contextEdit_Click);
            // 
            // contextDelete
            // 
            this.contextDelete.Name = "contextDelete";
            this.contextDelete.Size = new System.Drawing.Size(100, 22);
            this.contextDelete.Text = "删除";
            this.contextDelete.Click += new System.EventHandler(this.contextDelete_Click);
            // 
            // UCFrameEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCFrameEdit";
            this.Size = new System.Drawing.Size(343, 552);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.contextMenu1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
