using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ICSharpCode.Core;
using DF3DControl.Base;
using DF3DScan.Frm;
using DF3DScan.Class;
using DFCommon.Class;
using DFWinForms.Class;

namespace DF3DScan.UC
{
    public class UCAnimationNav : XtraUserControl
    {
        // Fields
        private BarButtonItem barCreate;
        private BarButtonItem barCreateGroupToGroup;
        private BarButtonItem barCreateTourToBlank;
        private BarButtonItem barCreateTourToGroup;
        private BarButtonItem barDelete;
        private BarButtonItem barDeleteGroup;
        private BarButtonItem barDeleteTour;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarButtonItem barEdit;
        private BarButtonItem barEditTour;
        private BarButtonItem barExport;
        private BarButtonItem barExportGroup;
        private BarButtonItem barExportGroupVideo;
        private BarButtonItem barExportTour;
        private BarButtonItem barExportTourVideo;
        private BarButtonItem barExportVideo;
        private BarButtonItem barImport;
        private BarButtonItem barImportToBlank;
        private BarButtonItem barImportToGroup;
        private BarManager barManager1;
        private BarButtonItem barMode;
        private BarButtonItem barNewGroupToBlank;
        private BarButtonItem barPlay;
        private BarButtonItem barPlayGroup;
        private BarButtonItem barPlayTour;
        private BarButtonItem barRenameGroup;
        private BarButtonItem barRenameTour;
        private BarButtonItem barStop;
        private Bar barTour;
        private IContainer components;
        private int currentPlayNodeIndex = -1;
        private FrameProps fp = new FrameProps();
        private ArrayList groupNodeToPlay;
        private ImageList imageList1;
        private bool isAutoRepeat;
        private bool isEmpty = true;
        private int iterater = 1;
        private TreeListNode lastNodeInGroup;
        private TreeListNode node;
        private string nodeName = "";
        private PopupMenu popupMenuBlank;
        private PopupMenu popupMenuGroup;
        private PopupMenu popupMenuTour;
        private ComponentResourceManager resources = new ComponentResourceManager(typeof(UCAnimationNav));
        private TreeListColumn tl_ID;
        private TreeListColumn tl_Name;
        private TreeListColumn tl_Object;
        private TreeListColumn tl_ParentID;
        private TreeListColumn tl_Type;
        private TreeListNode tourNode;
        private TreeList treeList1;
        private IVector3 vec = new Vector3Class();
        private ImageCollection imageCollection1;
        private AxRenderControl d3;
        private string _tmpPath;
        // Methods
        public UCAnimationNav()
        {
            this.InitializeComponent();
            this.barCreate.Enabled = true;
            this.barEdit.Enabled = false;
            this.barDelete.Enabled = false;
            this.barPlay.Enabled = false;
            this.barStop.Enabled = false;
            this.barMode.Enabled = false;
            this.barImport.Enabled = true;
            this.barExport.Enabled = false;
            this.barExportVideo.Enabled = false;
            this.groupNodeToPlay = new ArrayList();
            // 注册播放事件
            d3 = DF3DApplication.Application.Current3DMapControl;

            string localDataPath = SystemInfo.Instance.LocalDataPath;
            this._tmpPath = Path.Combine(localDataPath, "CameraTour");
            if (!Directory.Exists(this._tmpPath))
            {
                Directory.CreateDirectory(this._tmpPath);
            }
            if (File.Exists(this._tmpPath + "\\CameraTour.xml"))
            {
                this.LoadXML2(this._tmpPath + "\\CameraTour.xml", null);
            }

        }
        public void AttachEvent()
        {
            d3.RcCameraFlyFinished -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraFlyFinishedEventHandler(this.AxRenderControl_RcCameraFlyFinished);
            d3.RcCameraFlyFinished += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcCameraFlyFinishedEventHandler(this.AxRenderControl_RcCameraFlyFinished);
        }

        public void RestoreEnv()
        {
            WriteXML2(this._tmpPath + "\\CameraTour.xml", null);
        }

        // 播放结束
        private void AxRenderControl_RcCameraFlyFinished(object sender, _IRenderControlEvents_RcCameraFlyFinishedEvent e)
        {
            try
            {
                if (((this.treeList1.Nodes.Count != 0) && ((e.type != 0) || !this.treeList1.Enabled)) && ((e.type != 1) || !this.treeList1.Enabled))
                {
                    if ((e.type == 2) && !this.treeList1.Enabled)
                    {
                        (this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour).Pause();
                        this.barPlay.ImageIndex = 3;
                        this.barPlay.Caption = "播放";
                    }
                    else if (((e.type == 1) && !this.isAutoRepeat) && (this.groupNodeToPlay.Count == 0))
                    {
                        (this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour).Stop();
                        this.groupNodeToPlay.Clear();
                        this.treeList1.SetFocusedNode(this.lastNodeInGroup);
                        this.stopTour();
                    }
                    else if (((e.type == 1) && !this.isAutoRepeat) && (this.groupNodeToPlay.Count > 0))
                    {
                        TreeListNode node = this.groupNodeToPlay[0] as TreeListNode;
                        ICameraTour caTour = node.GetValue("tl_Object") as ICameraTour;
                        if (caTour.WaypointsNumber == 1)
                        {
                            this.playTourWithOneFrame(caTour);
                        }
                        else
                        {
                            caTour.Index = 0;
                            caTour.Play();
                        }
                        this.treeList1.SetFocusedNode(node);
                        this.groupNodeToPlay.RemoveAt(0);
                    }
                    else if (((e.type == 1) && this.isAutoRepeat) && (this.groupNodeToPlay.Count > 0))
                    {
                        if (this.currentPlayNodeIndex != (this.groupNodeToPlay.Count - 1))
                        {
                            this.currentPlayNodeIndex++;
                        }
                        else
                        {
                            this.currentPlayNodeIndex = 0;
                        }
                        TreeListNode node2 = this.groupNodeToPlay[this.currentPlayNodeIndex] as TreeListNode;
                        ICameraTour tour4 = node2.GetValue("tl_Object") as ICameraTour;
                        if (tour4.WaypointsNumber == 1)
                        {
                            this.playTourWithOneFrame(tour4);
                        }
                        else
                        {
                            tour4.Index = 0;
                            tour4.Play();
                        }
                        this.treeList1.SetFocusedNode(node2);
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        // 【新建】动画导航
        private void barCreate_ItemClick(object sender, ItemClickEventArgs e)
        {
            if ((this.treeList1.FocusedNode == null) || this.treeList1.FocusedNode.GetValue("tl_Type").ToString().Equals("Group"))
            {
                ICameraTour tour = d3.ObjectManager.CreateCameraTour(d3.ProjectTree.RootID);
                this.tourNode = this.treeList1.AppendNode(new object[] { "新建动画导航" + DateTime.Now.ToString(), this.iterater++, 0, "Tour", tour }, this.treeList1.FocusedNode);
                this.tourNode.StateImageIndex = 2;
                (this.Parent as UCCameraTour).AddUCFrameEdit(tour);
                //RightPanelService.AddFrameEditpanel(tour);
                //WorkSpaceServices.Instance().NeedSaveProject = true;
            }
        }
        private void barCreateGroupToGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            string str = this.treeList1.FocusedNode.GetValue("tl_ID").ToString();
            object[] nodeData = new object[5];
            nodeData[0] = "新建动画导航组" + DateTime.Now.ToString();
            nodeData[1] = this.iterater++;
            nodeData[2] = str;
            nodeData[3] = "Group";
            this.tourNode = this.treeList1.AppendNode(nodeData, this.treeList1.FocusedNode);
            this.tourNode.StateImageIndex = 0;
            //WorkSpaceServices.Instance().NeedSaveProject = true;
        }
        private void barCreateTourToBlank_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICameraTour tour = d3.ObjectManager.CreateCameraTour(d3.ProjectTree.RootID);
            this.tourNode = this.treeList1.AppendNode(new object[] { "新建动画导航" + DateTime.Now.ToString(), this.iterater++, 0, "Tour", tour }, (TreeListNode)null);
            this.tourNode.StateImageIndex = 2;
            //WorkSpaceServices.Instance().NeedSaveProject = true;
        }
        private void barCreateTourToGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICameraTour tour = d3.ObjectManager.CreateCameraTour(d3.ProjectTree.RootID);
            string str = this.treeList1.FocusedNode.GetValue("tl_ID").ToString();
            this.tourNode = this.treeList1.AppendNode(new object[] { "新建动画导航" + DateTime.Now.ToString(), this.iterater++, str, "Tour", tour }, this.treeList1.FocusedNode);
            this.tourNode.StateImageIndex = 2;
            //WorkSpaceServices.Instance().NeedSaveProject = true;
        }

        // 【删除】动画导航
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if ((this.treeList1.FocusedNode != null) && (XtraMessageBox.Show("是否确认删除？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                if (this.treeList1.FocusedNode.GetValue("tl_Type").ToString().Equals("Group"))
                {
                    foreach (TreeListNode node in this.treeList1.FocusedNode.Nodes)
                    {
                        if (node.GetValue("tl_Type").ToString().Equals("Tour"))
                        {
                            ICameraTour tour = node.GetValue("tl_Object") as ICameraTour;
                            d3.ObjectManager.DeleteObject(tour.Guid);
                        }
                    }
                    this.treeList1.DeleteNode(this.treeList1.FocusedNode);
                }
                else
                {
                    ICameraTour tour2 = this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour;
                    d3.ObjectManager.DeleteObject(tour2.Guid);
                    this.treeList1.DeleteSelectedNodes();
                }
                if (this.treeList1.Nodes.Count == 0)
                {
                    this.barCreate.Enabled = true;
                    this.barEdit.Enabled = false;
                    this.barDelete.Enabled = false;
                    this.barPlay.Enabled = false;
                    this.barStop.Enabled = false;
                    this.barMode.Enabled = false;
                    this.barImport.Enabled = true;
                    this.barExport.Enabled = false;
                    this.barExportVideo.Enabled = false;
                }
                //WorkSpaceServices.Instance().NeedSaveProject = true;
            }
        }
        private void barDeleteGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if ((this.treeList1.FocusedNode != null) && (XtraMessageBox.Show("是否确认删除？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                foreach (TreeListNode node in this.treeList1.FocusedNode.Nodes)
                {
                    if (node.GetValue("tl_Type").ToString().Equals("Tour"))
                    {
                        ICameraTour tour = node.GetValue("tl_Object") as ICameraTour;
                        d3.ObjectManager.DeleteObject(tour.Guid);
                    }
                }
                this.treeList1.DeleteNode(this.treeList1.FocusedNode);
                if (this.treeList1.Nodes.Count == 0)
                {
                    this.barCreate.Enabled = true;
                    this.barEdit.Enabled = false;
                    this.barDelete.Enabled = false;
                    this.barPlay.Enabled = false;
                    this.barStop.Enabled = false;
                    this.barMode.Enabled = false;
                    this.barImport.Enabled = true;
                    this.barExport.Enabled = false;
                    this.barExportVideo.Enabled = false;
                }
                //WorkSpaceServices.Instance().NeedSaveProject = true;
            }
        }
        private void barDeleteTour_ItemClick(object sender, ItemClickEventArgs e)
        {
            if ((this.treeList1.FocusedNode != null) && (XtraMessageBox.Show("是否确认删除？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                ICameraTour tour = this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour;
                d3.ObjectManager.DeleteObject(tour.Guid);
                this.treeList1.DeleteNode(this.treeList1.FocusedNode);
                if (this.treeList1.Nodes.Count == 0)
                {
                    this.barCreate.Enabled = true;
                    this.barEdit.Enabled = false;
                    this.barDelete.Enabled = false;
                    this.barPlay.Enabled = false;
                    this.barStop.Enabled = false;
                    this.barMode.Enabled = false;
                    this.barImport.Enabled = true;
                    this.barExport.Enabled = false;
                    this.barExportVideo.Enabled = false;
                }
                //WorkSpaceServices.Instance().NeedSaveProject = true;
            }
        }

        // 【编辑】动画导航
        private void barEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode != null)
            {
                ICameraTour tour = this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour;
                (this.Parent as UCCameraTour).AddUCFrameEdit(tour);
                //RightPanelService.AddFrameEditpanel(tour);
            }
        }
        private void barEditTour_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode != null)
            {
                ICameraTour tour = this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour;
                (this.Parent as UCCameraTour).AddUCFrameEdit(tour);
                //RightPanelService.AddFrameEditpanel(tour);
            }
        }

        // 【导出】XML文件
        private void barExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "xml",
                Filter = "XML File(*.xml)|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                WaitDialogForm form = new WaitDialogForm("正在导出...", "请稍后");
                form.Show();
                string fileName = dialog.FileName;
                if (dialog.FileName.LastIndexOf(".xml") == -1)
                {
                    fileName = string.Format("{0}.xml", dialog.FileName);
                }
                if (this.WriteXML2(fileName, this.treeList1.FocusedNode))
                {
                    XtraMessageBox.Show("导出成功！", "提示");
                }
                else
                {
                    XtraMessageBox.Show("导出失败！", "提示");
                }
                form.Close();
            }
        }
        private void barExportGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "xml",
                Filter = "XML File(*.xml)|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                WaitDialogForm form = new WaitDialogForm("正在导出...", "请稍后");
                form.Show();
                string fileName = dialog.FileName;
                if (dialog.FileName.LastIndexOf(".xml") == -1)
                {
                    fileName = string.Format("{0}.xml", dialog.FileName);
                }
                if (this.WriteXML2(fileName, this.treeList1.FocusedNode))
                {
                    XtraMessageBox.Show("导出成功！", "提示");
                }
                else
                {
                    XtraMessageBox.Show("导出失败！", "提示");
                }
                form.Close();
            }
        }
        private void barExportTour_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "xml",
                Filter = "XML File(*.xml)|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                if (dialog.FileName.LastIndexOf(".xml") == -1)
                {
                    fileName = string.Format("{0}.xml", dialog.FileName);
                }
                if (this.WriteXML2(fileName, this.treeList1.FocusedNode))
                {
                    XtraMessageBox.Show("导出成功！", "提示");
                }
                else
                {
                    XtraMessageBox.Show("导出失败！", "提示");
                }
            }
        }

        // 【输出】视频文件
        private void barExportGroupVideo_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ExportGroupVideo();
        }
        private void barExportTourVideo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode != null)
            {
                this.ExportTourVideo();
            }
        }
        private void barExportVideo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if ((this.treeList1.FocusedNode == null) || this.treeList1.FocusedNode.GetValue("tl_Type").ToString().Equals("Group"))
            {
                this.ExportGroupVideo();
            }
            else
            {
                this.ExportTourVideo();
            }
        }

        // 【导入】xml文件
        private void barImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "XML File(*.xml)|*.xml|ASE File(*.ase)|*.ase"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                WaitDialogForm form = new WaitDialogForm("正在导出...", "请稍后");
                form.Show();
                if (this.getFileType(dialog.FileName) == 1)
                {
                    this.LoadXML2(dialog.FileName, this.treeList1.FocusedNode);
                    //WorkSpaceServices.Instance().NeedSaveProject = true;
                }
                else if (this.getFileType(dialog.FileName) == 2)
                {
                    ICameraTour tour = d3.ObjectManager.CreateCameraTour(d3.ProjectTree.RootID);
                    tour.FromAse(dialog.FileName);
                    string[] strArray = dialog.FileName.Split(new char[] { '\\' });
                    string str = strArray[strArray.Length - 1];
                    int length = str.LastIndexOf(".");
                    this.node = this.treeList1.AppendNode(new object[] { str.Substring(0, length), 0, 0, "Tour", tour }, this.treeList1.FocusedNode);
                    this.node.StateImageIndex = 2;
                    //WorkSpaceServices.Instance().NeedSaveProject = true;
                }
                form.Close();
            }
        }
        private void barImportToBlank_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "XML File(*.xml)|*.xml|ASE File(*.ase)|*.ase"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                WaitDialogForm form = new WaitDialogForm("正在导出...", "请稍后");
                form.Show();
                if (this.getFileType(dialog.FileName) == 1)
                {
                    this.LoadXML2(dialog.FileName, null);
                    //WorkSpaceServices.Instance().NeedSaveProject = true;
                }
                else if (this.getFileType(dialog.FileName) == 2)
                {
                    ICameraTour tour = d3.ObjectManager.CreateCameraTour(d3.ProjectTree.RootID);
                    tour.FromAse(dialog.FileName);
                    string[] strArray = dialog.FileName.Split(new char[] { '\\' });
                    string str = strArray[strArray.Length - 1];
                    int length = str.LastIndexOf(".");
                    this.node = this.treeList1.AppendNode(new object[] { str.Substring(0, length), 0, 0, "Tour", tour }, (TreeListNode)null);
                    this.node.StateImageIndex = 2;
                    //WorkSpaceServices.Instance().NeedSaveProject = true;
                }
                form.Close();
            }
        }
        private void barImportToGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "XML File(*.xml)|*.xml|ASE File(*.ase)|*.ase"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                WaitDialogForm form = new WaitDialogForm("正在导出...", "请稍后");
                form.Show();
                if (this.getFileType(dialog.FileName) == 1)
                {
                    this.LoadXML2(dialog.FileName, this.treeList1.FocusedNode);
                    //WorkSpaceServices.Instance().NeedSaveProject = true;
                }
                else if (this.getFileType(dialog.FileName) == 2)
                {
                    ICameraTour tour = d3.ObjectManager.CreateCameraTour(d3.ProjectTree.RootID);
                    tour.FromAse(dialog.FileName);
                    string[] strArray = dialog.FileName.Split(new char[] { '\\' });
                    string str = strArray[strArray.Length - 1];
                    int length = str.LastIndexOf(".");
                    this.node = this.treeList1.AppendNode(new object[] { str.Substring(0, length), 0, 0, "Tour", tour }, this.treeList1.FocusedNode);
                    this.node.StateImageIndex = 2;
                    //WorkSpaceServices.Instance().NeedSaveProject = true;
                }
                form.Close();
            }
        }

        private void barMode_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!this.isAutoRepeat)
            {
                this.isAutoRepeat = true;
                this.barMode.Caption = "循环播放";
                this.barMode.ImageIndex = 10;
            }
            else
            {
                this.isAutoRepeat = false;
                this.barMode.Caption = "单次播放";
                this.barMode.ImageIndex = 6;
            }
        }

        private void barNewGroupToBlank_ItemClick(object sender, ItemClickEventArgs e)
        {
            object[] nodeData = new object[5];
            nodeData[0] = "新建动画导航组" + DateTime.Now.ToString();
            nodeData[1] = this.iterater++;
            nodeData[2] = 0;
            nodeData[3] = "Group";
            this.tourNode = this.treeList1.AppendNode(nodeData, (TreeListNode)null);
            this.tourNode.StateImageIndex = 0;
            //WorkSpaceServices.Instance().NeedSaveProject = true;
        }

        // 【播放】动画导航
        private void barPlay_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (d3.InteractMode != gviInteractMode.gviInteractNormal)
            {
                d3.InteractMode = gviInteractMode.gviInteractNormal;
                DF3DApplication.Application.Workbench.BarPerformClick("Wander");
            }
            d3.Viewport.ActiveView = 0;
            if ((this.treeList1.FocusedNode == null) || this.treeList1.FocusedNode.GetValue("tl_Type").ToString().Equals("Group"))
            {
                if (this.barPlay.Caption == "播放")
                {
                    if (!this.playGroup())
                    {
                        return;
                    }
                    this.barPlay.ImageIndex = 4;
                    this.barPlay.Caption = "暂停";
                }
                else
                {
                    (this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour).Pause();
                    this.barPlay.ImageIndex = 3;
                    this.barPlay.Caption = "播放";
                }
            }
            else
            {
                ICameraTour caTour = this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour;
                if (caTour.WaypointsNumber == 0)
                {
                    return;
                }
                if (caTour.WaypointsNumber == 1)
                {
                    this.playTourWithOneFrame(caTour);
                    return;
                }
                this.lastNodeInGroup = this.treeList1.FocusedNode;
                if (this.barPlay.Caption == "播放")
                {
                    caTour.AutoRepeat = this.isAutoRepeat;
                    caTour.Index = 0;
                    caTour.Play();
                    this.barPlay.ImageIndex = 4;
                    this.barPlay.Caption = "暂停";
                }
                else
                {
                    caTour.Pause();
                    this.barPlay.ImageIndex = 3;
                    this.barPlay.Caption = "播放";
                }
            }
            this.barCreate.Enabled = false;
            this.barEdit.Enabled = false;
            this.barDelete.Enabled = false;
            this.barPlay.Enabled = true;
            this.barStop.Enabled = true;
            this.barMode.Enabled = false;
            this.barImport.Enabled = false;
            this.barExport.Enabled = false;
            this.barExportVideo.Enabled = false;
            this.treeList1.Enabled = false;
        }
        private void barPlayGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (d3.InteractMode != gviInteractMode.gviInteractNormal)
            {
                d3.InteractMode = gviInteractMode.gviInteractNormal;
                DF3DApplication.Application.Workbench.BarPerformClick("Wander");
            }
            d3.Viewport.ActiveView = 0;
            if (this.playGroup())
            {
                this.barPlay.ImageIndex = 4;
                this.barPlay.Caption = "暂停";
                this.barCreate.Enabled = false;
                this.barEdit.Enabled = false;
                this.barDelete.Enabled = false;
                this.barPlay.Enabled = true;
                this.barStop.Enabled = true;
                this.barMode.Enabled = false;
                this.barImport.Enabled = false;
                this.barExport.Enabled = false;
                this.barExportVideo.Enabled = false;
                this.treeList1.Enabled = false;
            }
        }
        private void barPlayTour_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (d3.InteractMode != gviInteractMode.gviInteractNormal)
            {
                d3.InteractMode = gviInteractMode.gviInteractNormal;
                DF3DApplication.Application.Workbench.BarPerformClick("Wander");
            }
            d3.Viewport.ActiveView = 0;
            if (this.treeList1.FocusedNode != null)
            {
                ICameraTour caTour = this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour;
                if (caTour.WaypointsNumber != 0)
                {
                    if (caTour.WaypointsNumber == 1)
                    {
                        this.playTourWithOneFrame(caTour);
                    }
                    else
                    {
                        this.lastNodeInGroup = this.treeList1.FocusedNode;
                        caTour.AutoRepeat = this.isAutoRepeat;
                        caTour.Index = 0;
                        caTour.Play();
                        this.barPlay.ImageIndex = 4;
                        this.barPlay.Caption = "暂停";
                        this.barCreate.Enabled = false;
                        this.barEdit.Enabled = false;
                        this.barDelete.Enabled = false;
                        this.barPlay.Enabled = true;
                        this.barStop.Enabled = true;
                        this.barMode.Enabled = false;
                        this.barImport.Enabled = false;
                        this.barExport.Enabled = false;
                        this.barExportVideo.Enabled = false;
                        this.treeList1.Enabled = false;
                    }
                }
            }
        }

        // 【重命名】动画导航
        private void barRenameGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode != null)
            {
                this.treeList1.FocusedNode.TreeList.Columns.ColumnByFieldName("tl_Name").OptionsColumn.AllowEdit = true;
                this.treeList1.FocusedNode.TreeList.Columns.ColumnByFieldName("tl_Name").OptionsColumn.AllowFocus = true;
                this.treeList1.FocusedColumn = this.treeList1.FocusedNode.TreeList.Columns.ColumnByFieldName("tl_Name");
                this.treeList1.ShowEditor();
            }
        }
        private void barRenameTour_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode != null)
            {
                this.treeList1.FocusedNode.TreeList.Columns.ColumnByFieldName("tl_Name").OptionsColumn.AllowEdit = true;
                this.treeList1.FocusedNode.TreeList.Columns.ColumnByFieldName("tl_Name").OptionsColumn.AllowFocus = true;
                this.treeList1.FocusedColumn = this.treeList1.FocusedNode.TreeList.Columns.ColumnByFieldName("tl_Name");
                this.treeList1.ShowEditor();
            }
        }

        private void barStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            d3.Viewport.ActiveView = 0;
            (this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour).Stop();
            this.groupNodeToPlay.Clear();
            this.stopTour();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        // 输出视频文件
        private void ExportGroupVideo()
        {
            this.groupNodeToPlay.Clear();
            if (this.treeList1.FocusedNode != null)
            {
                this.findAllNode2(this.treeList1.FocusedNode.Nodes);
            }
            else
            {
                this.findAllNode2(this.treeList1.Nodes);
            }
            if (this.groupNodeToPlay.Count != 0)
            {
                try
                {
                    using (ExportVideoDlg dlg = new ExportVideoDlg())
                    {
                        string filePath;
                        int fPS;
                        dlg.ShowDialog();
                        if (dlg.IsNeedExport)
                        {
                            filePath = dlg.FilePath;
                            if (dlg.FPS >= 0)
                            {
                                fPS = dlg.FPS;
                                goto Label_0095;
                            }
                            XtraMessageBox.Show("导出视频错误！", "提示");
                        }
                        return;
                    Label_0095:
                        XtraMessageBox.Show("请注意：导出视频时窗口不能最小化！", "提示");
                        int length = filePath.LastIndexOf(".");
                        string fileName = filePath.Substring(0, length);
                        string fileType = filePath.Substring(length);
                        int vindex = 0;
                        TreeListNode node = this.groupNodeToPlay[0] as TreeListNode;
                        ICameraTour tour = node.GetValue("tl_Object") as ICameraTour;
                        tour.Index = 0;
                        ExportProcess.Instance().AdviseEvent(tour, this.groupNodeToPlay, fileName, vindex, fileType, fPS, this.treeList1, this.lastNodeInGroup);
                        if (!tour.ExportVideo(fileName + "_0" + fileType, fPS))
                        {
                            XtraMessageBox.Show(node.GetValue("tl_Name") + ":" + "导出视频错误！", "提示");
                        }
                        this.treeList1.SetFocusedNode(node);
                        this.groupNodeToPlay.Clear();
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }
        private void ExportTourVideo()
        {
            ICameraTour tour = this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour;
            tour.Index = 0;
            if (tour.WaypointsNumber < 2)
            {
                XtraMessageBox.Show("节点数少于2个，不支持导出视频！", "提示");
            }
            else
            {
                try
                {
                    this.lastNodeInGroup = this.treeList1.FocusedNode;
                    using (ExportVideoDlg dlg = new ExportVideoDlg())
                    {
                        string filePath;
                        int fPS;
                        dlg.ShowDialog();
                        if (dlg.IsNeedExport)
                        {
                            filePath = dlg.FilePath;
                            if (dlg.FPS >= 0)
                            {
                                fPS = dlg.FPS;
                                goto Label_0093;
                            }
                            XtraMessageBox.Show("导出视频错误！", "提示");
                        }
                        return;
                    Label_0093:
                        XtraMessageBox.Show("请注意：导出视频时窗口不能最小化！", "提示");
                        ExportProcess.Instance().AdviseEvent(tour, null, "", -1, "", -1, null, null);
                        bool flag = true;
                        if (dlg.Type == 0)
                        {
                            flag = tour.ExportVideo(filePath, fPS);
                        }
                        else
                        {
                            flag = tour.ExportFrameSequence(filePath, dlg.WidthPx, dlg.HeightPx, fPS);
                        }
                        if (!flag)
                        {
                            XtraMessageBox.Show("导出视频错误！", "提示");
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }

        // 获取所有动画导航节点存储到this.groupNodeToPlay列表中
        private void findAllNode(TreeListNodes nodes)
        {
            this.lastNodeInGroup = nodes.LastNode;
            foreach (TreeListNode node in nodes)
            {
                if (node.HasChildren)
                {
                    this.findAllNode(node.Nodes);
                }
                if (!node.GetValue("tl_Type").ToString().Equals("Group"))
                {
                    ICameraTour tour = node.GetValue("tl_Object") as ICameraTour;
                    if (tour.WaypointsNumber != 0)
                    {
                        this.groupNodeToPlay.Add(node);
                    }
                }
            }
        }
        private void findAllNode2(TreeListNodes nodes)
        {
            this.lastNodeInGroup = nodes.LastNode;
            foreach (TreeListNode node in nodes)
            {
                if (node.HasChildren)
                {
                    this.findAllNode2(node.Nodes);
                }
                if (!node.GetValue("tl_Type").ToString().Equals("Group"))
                {
                    ICameraTour tour = node.GetValue("tl_Object") as ICameraTour;
                    if (tour.WaypointsNumber >= 2)
                    {
                        this.groupNodeToPlay.Add(node);
                    }
                }
            }
        }

        public string GetCurCameraTourTree()
        {
            try
            {
                this.treeList1.Refresh();
                TreeListNode focusedNode = this.treeList1.FocusedNode;
                this.treeList1.FocusedNode = null;
                XmlDocument doc = new XmlDocument();
                XmlNode newChild = doc.CreateElement("UCAnimationNav");
                doc.AppendChild(newChild);
                if (this.treeList1.Nodes.Count > 0)
                {
                    this.WriteCircle(this.treeList1.Nodes[0], newChild, doc);
                }
                this.treeList1.SetFocusedNode(focusedNode);
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = false,
                    Indent = true,
                    Encoding = Encoding.UTF8,
                    ConformanceLevel = ConformanceLevel.Auto
                };
                MemoryStream output = new MemoryStream();
                XmlWriter w = XmlWriter.Create(output, settings);
                doc.Save(w);
                byte[] bytes = output.ToArray();
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        // 获取导入文件类型
        private int getFileType(string filepath)
        {
            if (!string.IsNullOrEmpty(filepath))
            {
                string[] strArray = filepath.Split(new char[] { '.' });
                string str = strArray[strArray.Length - 1];
                if (str.ToUpper() == "XML")
                {
                    return 1;
                }
                if (str.ToUpper() == "ASE")
                {
                    return 2;
                }
            }
            return 0;
        }

        #region 初始化
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCAnimationNav));
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.tl_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_ParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_Type = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_Object = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barTour = new DevExpress.XtraBars.Bar();
            this.barCreate = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barPlay = new DevExpress.XtraBars.BarButtonItem();
            this.barStop = new DevExpress.XtraBars.BarButtonItem();
            this.barMode = new DevExpress.XtraBars.BarButtonItem();
            this.barImport = new DevExpress.XtraBars.BarButtonItem();
            this.barExport = new DevExpress.XtraBars.BarButtonItem();
            this.barExportVideo = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.barNewGroupToBlank = new DevExpress.XtraBars.BarButtonItem();
            this.barCreateTourToBlank = new DevExpress.XtraBars.BarButtonItem();
            this.barImportToBlank = new DevExpress.XtraBars.BarButtonItem();
            this.barCreateGroupToGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barCreateTourToGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barRenameGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barDeleteGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barPlayGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barImportToGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barExportGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barExportGroupVideo = new DevExpress.XtraBars.BarButtonItem();
            this.barEditTour = new DevExpress.XtraBars.BarButtonItem();
            this.barDeleteTour = new DevExpress.XtraBars.BarButtonItem();
            this.barRenameTour = new DevExpress.XtraBars.BarButtonItem();
            this.barPlayTour = new DevExpress.XtraBars.BarButtonItem();
            this.barExportTour = new DevExpress.XtraBars.BarButtonItem();
            this.barExportTourVideo = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuBlank = new DevExpress.XtraBars.PopupMenu();
            this.popupMenuGroup = new DevExpress.XtraBars.PopupMenu();
            this.popupMenuTour = new DevExpress.XtraBars.PopupMenu();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuBlank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTour)).BeginInit();
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
            this.tl_ID,
            this.tl_ParentID,
            this.tl_Type,
            this.tl_Object});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 31);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.AllowIndeterminateCheckState = true;
            this.treeList1.OptionsBehavior.DragNodes = true;
            this.treeList1.OptionsPrint.PrintPageHeader = false;
            this.treeList1.OptionsView.ShowColumns = false;
            this.treeList1.OptionsView.ShowHorzLines = false;
            this.treeList1.OptionsView.ShowIndicator = false;
            this.treeList1.Size = new System.Drawing.Size(297, 521);
            this.treeList1.StateImageList = this.imageList1;
            this.treeList1.TabIndex = 0;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            this.treeList1.HiddenEditor += new System.EventHandler(this.treeList1_HiddenEditor);
            this.treeList1.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeList1_CellValueChanged);
            this.treeList1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeList1_DragDrop);
            this.treeList1.DragOver += new System.Windows.Forms.DragEventHandler(this.treeList1_DragOver);
            this.treeList1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseDoubleClick);
            this.treeList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseUp);
            // 
            // tl_Name
            // 
            this.tl_Name.Caption = "tl_Name";
            this.tl_Name.FieldName = "tl_Name";
            this.tl_Name.MinWidth = 33;
            this.tl_Name.Name = "tl_Name";
            this.tl_Name.OptionsColumn.AllowEdit = false;
            this.tl_Name.Visible = true;
            this.tl_Name.VisibleIndex = 0;
            // 
            // tl_ID
            // 
            this.tl_ID.Caption = "tl_ID";
            this.tl_ID.FieldName = "tl_ID";
            this.tl_ID.Name = "tl_ID";
            // 
            // tl_ParentID
            // 
            this.tl_ParentID.Caption = "tl_ParentID";
            this.tl_ParentID.FieldName = "tl_ParentID";
            this.tl_ParentID.Name = "tl_ParentID";
            // 
            // tl_Type
            // 
            this.tl_Type.Caption = "tl_Type";
            this.tl_Type.FieldName = "tl_Type";
            this.tl_Type.Name = "tl_Type";
            // 
            // tl_Object
            // 
            this.tl_Object.Caption = "tl_Object";
            this.tl_Object.FieldName = "tl_Object";
            this.tl_Object.Name = "tl_Object";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "globe.ico");
            this.imageList1.Images.SetKeyName(1, "xiezai.ico");
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTour});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barCreate,
            this.barEdit,
            this.barDelete,
            this.barPlay,
            this.barStop,
            this.barMode,
            this.barImport,
            this.barExport,
            this.barExportVideo,
            this.barNewGroupToBlank,
            this.barCreateTourToBlank,
            this.barImportToBlank,
            this.barCreateGroupToGroup,
            this.barCreateTourToGroup,
            this.barRenameGroup,
            this.barDeleteGroup,
            this.barPlayGroup,
            this.barImportToGroup,
            this.barExportGroup,
            this.barExportGroupVideo,
            this.barEditTour,
            this.barDeleteTour,
            this.barRenameTour,
            this.barPlayTour,
            this.barExportTour,
            this.barExportTourVideo});
            this.barManager1.MaxItemId = 26;
            // 
            // barTour
            // 
            this.barTour.BarName = "Tools";
            this.barTour.DockCol = 0;
            this.barTour.DockRow = 0;
            this.barTour.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTour.FloatLocation = new System.Drawing.Point(245, 105);
            this.barTour.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCreate),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPlay),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStop),
            new DevExpress.XtraBars.LinkPersistInfo(this.barMode),
            new DevExpress.XtraBars.LinkPersistInfo(this.barImport),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExportVideo)});
            this.barTour.OptionsBar.AllowQuickCustomization = false;
            this.barTour.OptionsBar.DrawDragBorder = false;
            this.barTour.Text = "Tools";
            // 
            // barCreate
            // 
            this.barCreate.Caption = "新建";
            this.barCreate.Id = 0;
            this.barCreate.ImageIndex = 0;
            this.barCreate.Name = "barCreate";
            this.barCreate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCreate_ItemClick);
            // 
            // barEdit
            // 
            this.barEdit.Caption = "编辑";
            this.barEdit.Id = 1;
            this.barEdit.ImageIndex = 1;
            this.barEdit.Name = "barEdit";
            this.barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除";
            this.barDelete.Id = 2;
            this.barDelete.ImageIndex = 2;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barPlay
            // 
            this.barPlay.Caption = "播放";
            this.barPlay.Id = 3;
            this.barPlay.ImageIndex = 3;
            this.barPlay.Name = "barPlay";
            this.barPlay.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPlay_ItemClick);
            // 
            // barStop
            // 
            this.barStop.Caption = "停止";
            this.barStop.Id = 4;
            this.barStop.ImageIndex = 5;
            this.barStop.Name = "barStop";
            this.barStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barStop_ItemClick);
            // 
            // barMode
            // 
            this.barMode.Caption = "单次";
            this.barMode.Id = 5;
            this.barMode.ImageIndex = 6;
            this.barMode.Name = "barMode";
            this.barMode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barMode_ItemClick);
            // 
            // barImport
            // 
            this.barImport.Caption = "导入";
            this.barImport.Id = 6;
            this.barImport.ImageIndex = 7;
            this.barImport.Name = "barImport";
            this.barImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barImport_ItemClick);
            // 
            // barExport
            // 
            this.barExport.Caption = "导出";
            this.barExport.Id = 7;
            this.barExport.ImageIndex = 8;
            this.barExport.Name = "barExport";
            this.barExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExport_ItemClick);
            // 
            // barExportVideo
            // 
            this.barExportVideo.Caption = "输出";
            this.barExportVideo.Id = 8;
            this.barExportVideo.ImageIndex = 9;
            this.barExportVideo.Name = "barExportVideo";
            this.barExportVideo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExportVideo_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(297, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 552);
            this.barDockControlBottom.Size = new System.Drawing.Size(297, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(297, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 521);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "create.png");
            this.imageCollection1.Images.SetKeyName(1, "edit.png");
            this.imageCollection1.Images.SetKeyName(2, "delete.png");
            this.imageCollection1.Images.SetKeyName(3, "play.png");
            this.imageCollection1.Images.SetKeyName(4, "pause.png");
            this.imageCollection1.Images.SetKeyName(5, "stop.png");
            this.imageCollection1.Images.SetKeyName(6, "playonce.png");
            this.imageCollection1.Images.SetKeyName(7, "import.png");
            this.imageCollection1.Images.SetKeyName(8, "export.png");
            this.imageCollection1.Images.SetKeyName(9, "output.png");
            this.imageCollection1.Images.SetKeyName(10, "refresh.png");
            // 
            // barNewGroupToBlank
            // 
            this.barNewGroupToBlank.Caption = "新建动画导航组";
            this.barNewGroupToBlank.Id = 9;
            this.barNewGroupToBlank.Name = "barNewGroupToBlank";
            this.barNewGroupToBlank.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewGroupToBlank_ItemClick);
            // 
            // barCreateTourToBlank
            // 
            this.barCreateTourToBlank.Caption = "新建动画导航";
            this.barCreateTourToBlank.Id = 10;
            this.barCreateTourToBlank.Name = "barCreateTourToBlank";
            this.barCreateTourToBlank.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCreateTourToBlank_ItemClick);
            // 
            // barImportToBlank
            // 
            this.barImportToBlank.Caption = "导入";
            this.barImportToBlank.Id = 11;
            this.barImportToBlank.Name = "barImportToBlank";
            this.barImportToBlank.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barImportToBlank_ItemClick);
            // 
            // barCreateGroupToGroup
            // 
            this.barCreateGroupToGroup.Caption = "新建动画导航组";
            this.barCreateGroupToGroup.Id = 12;
            this.barCreateGroupToGroup.Name = "barCreateGroupToGroup";
            this.barCreateGroupToGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCreateGroupToGroup_ItemClick);
            // 
            // barCreateTourToGroup
            // 
            this.barCreateTourToGroup.Caption = "新建动画导航";
            this.barCreateTourToGroup.Id = 13;
            this.barCreateTourToGroup.Name = "barCreateTourToGroup";
            this.barCreateTourToGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCreateTourToGroup_ItemClick);
            // 
            // barRenameGroup
            // 
            this.barRenameGroup.Caption = "重命名";
            this.barRenameGroup.Id = 14;
            this.barRenameGroup.Name = "barRenameGroup";
            this.barRenameGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRenameGroup_ItemClick);
            // 
            // barDeleteGroup
            // 
            this.barDeleteGroup.Caption = "删除";
            this.barDeleteGroup.Id = 15;
            this.barDeleteGroup.Name = "barDeleteGroup";
            this.barDeleteGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDeleteGroup_ItemClick);
            // 
            // barPlayGroup
            // 
            this.barPlayGroup.Caption = "播放";
            this.barPlayGroup.Id = 16;
            this.barPlayGroup.Name = "barPlayGroup";
            this.barPlayGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPlayGroup_ItemClick);
            // 
            // barImportToGroup
            // 
            this.barImportToGroup.Caption = "导入";
            this.barImportToGroup.Id = 17;
            this.barImportToGroup.Name = "barImportToGroup";
            this.barImportToGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barImportToGroup_ItemClick);
            // 
            // barExportGroup
            // 
            this.barExportGroup.Caption = "导出";
            this.barExportGroup.Id = 18;
            this.barExportGroup.Name = "barExportGroup";
            this.barExportGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExportGroup_ItemClick);
            // 
            // barExportGroupVideo
            // 
            this.barExportGroupVideo.Caption = "输出视频";
            this.barExportGroupVideo.Id = 19;
            this.barExportGroupVideo.Name = "barExportGroupVideo";
            this.barExportGroupVideo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExportGroupVideo_ItemClick);
            // 
            // barEditTour
            // 
            this.barEditTour.Caption = "编辑";
            this.barEditTour.Id = 20;
            this.barEditTour.Name = "barEditTour";
            this.barEditTour.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEditTour_ItemClick);
            // 
            // barDeleteTour
            // 
            this.barDeleteTour.Caption = "删除";
            this.barDeleteTour.Id = 21;
            this.barDeleteTour.Name = "barDeleteTour";
            this.barDeleteTour.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDeleteTour_ItemClick);
            // 
            // barRenameTour
            // 
            this.barRenameTour.Caption = "重命名";
            this.barRenameTour.Id = 22;
            this.barRenameTour.Name = "barRenameTour";
            this.barRenameTour.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRenameTour_ItemClick);
            // 
            // barPlayTour
            // 
            this.barPlayTour.Caption = "播放";
            this.barPlayTour.Id = 23;
            this.barPlayTour.Name = "barPlayTour";
            this.barPlayTour.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPlayTour_ItemClick);
            // 
            // barExportTour
            // 
            this.barExportTour.Caption = "导出";
            this.barExportTour.Id = 24;
            this.barExportTour.Name = "barExportTour";
            this.barExportTour.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExportTour_ItemClick);
            // 
            // barExportTourVideo
            // 
            this.barExportTourVideo.Caption = "输出视频";
            this.barExportTourVideo.Id = 25;
            this.barExportTourVideo.Name = "barExportTourVideo";
            this.barExportTourVideo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExportTourVideo_ItemClick);
            // 
            // popupMenuBlank
            // 
            this.popupMenuBlank.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barNewGroupToBlank),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCreateTourToBlank),
            new DevExpress.XtraBars.LinkPersistInfo(this.barImportToBlank)});
            this.popupMenuBlank.Manager = this.barManager1;
            this.popupMenuBlank.Name = "popupMenuBlank";
            // 
            // popupMenuGroup
            // 
            this.popupMenuGroup.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCreateGroupToGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCreateTourToGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRenameGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDeleteGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPlayGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barImportToGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExportGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExportGroupVideo)});
            this.popupMenuGroup.Manager = this.barManager1;
            this.popupMenuGroup.Name = "popupMenuGroup";
            // 
            // popupMenuTour
            // 
            this.popupMenuTour.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditTour),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDeleteTour),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRenameTour),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPlayTour),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExportTour),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExportTourVideo)});
            this.popupMenuTour.Manager = this.barManager1;
            this.popupMenuTour.Name = "popupMenuTour";
            // 
            // UCAnimationNav
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCAnimationNav";
            this.Size = new System.Drawing.Size(297, 552);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuBlank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTour)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion 

        // 判断动画导航路径节点是否为空
        private void isEmptyFrameTree(TreeListNode treeNode)
        {
            if (this.treeList1.Nodes.Count != 0)
            {
                if (treeNode == null)
                {
                    for (int i = 0; i < this.treeList1.Nodes.Count; i++)
                    {
                        this.isEmptyFrameTree(this.treeList1.Nodes[i]);
                    }
                }
                else if (treeNode.GetValue("tl_Type").ToString().Equals("Group"))
                {
                    for (int j = 0; j < treeNode.Nodes.Count; j++)
                    {
                        this.isEmptyFrameTree(treeNode.Nodes[j]);
                    }
                }
                else if (treeNode.GetValue("tl_Type").ToString().Equals("Tour"))
                {
                    ICameraTour tour = treeNode.GetValue("tl_Object") as ICameraTour;
                    if (tour.WaypointsNumber != 0)
                    {
                        this.isEmpty = false;
                    }
                }
            }
        }

        #region 加载动画导航
        public void LoadCameraTourTree(XmlReader xr, string prjpath)
        {
            if (!xr.IsEmptyElement)
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(prjpath);
                    XmlNodeList childNodes = document.DocumentElement.SelectSingleNode("descendant::UCAnimationNav").ChildNodes;
                    this.LoadXml2TreeList(childNodes, null);
                    this.treeList1.FocusedNode = this.treeList1.Nodes[0];
                    this.UpdateButtonState();
                }
                catch (Exception exception)
                {
                    XtraMessageBox.Show("加载动画导航数据错误！", "提示");
                }
            }
        }
        #endregion

        // 导入XML文件
        private void LoadXML2(string path, TreeListNode targetNode)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNodeList childNodes = document.SelectSingleNode("UCAnimationNav").ChildNodes;
                this.LoadXml2TreeList(childNodes, targetNode);
                this.treeList1.FocusedNode = targetNode;
                this.UpdateButtonState();
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("加载动画导航数据错误！", "提示");
            }
        }
        private void LoadXml2TreeList(XmlNodeList xnl, TreeListNode targetNode)
        {
            if (xnl.Count > 0)
            {
                foreach (XmlNode node in xnl)
                {
                    this.nodeName = node.Attributes["Name"].Value;
                    if (node.Name == "Tour")
                    {
                        ICameraTour tour = d3.ObjectManager.CreateCameraTour(d3.ProjectTree.RootID);
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            IEulerAngle angle;
                            this.fp.clear();
                            this.fp.index = int.Parse(node2.Attributes["index"].Value);
                            double x = double.Parse(node2.Attributes["x"].Value);
                            double y = double.Parse(node2.Attributes["y"].Value);
                            double z = double.Parse(node2.Attributes["z"].Value);
                            this.vec.Set(x, y, z);
                            this.fp.heading = double.Parse(node2.Attributes["heading"].Value);
                            this.fp.tilt = double.Parse(node2.Attributes["tilt"].Value);
                            this.fp.roll = double.Parse(node2.Attributes["roll"].Value);
                            this.fp.duration = double.Parse(node2.Attributes["duration"].Value);
                            string str = node2.Attributes["mode"].Value;
                            if (str != null)
                            {
                                if (!(str == "gviCameraTourBounce"))
                                {
                                    if (str == "gviCameraTourSmooth")
                                    {
                                        goto Label_0205;
                                    }
                                    if (str == "gviCameraTourLinear")
                                    {
                                        goto Label_0213;
                                    }
                                }
                                else
                                {
                                    this.fp.mode = gviCameraTourMode.gviCameraTourBounce;
                                }
                            }
                            goto Label_021F;
                        Label_0205:
                            this.fp.mode = gviCameraTourMode.gviCameraTourSmooth;
                            goto Label_021F;
                        Label_0213:
                            this.fp.mode = gviCameraTourMode.gviCameraTourLinear;
                        Label_021F:
                            angle = new EulerAngleClass();
                            angle.Heading = this.fp.heading;
                            angle.Roll = this.fp.roll;
                            angle.Tilt = this.fp.tilt;
                            tour.AddWaypoint(this.vec, angle, this.fp.duration, this.fp.mode);
                        }
                        this.node = this.treeList1.AppendNode(new object[] { this.nodeName, 0, 0, node.Name, tour }, targetNode);
                        this.node.StateImageIndex = 2;
                        continue;
                    }
                    object[] nodeData = new object[5];
                    nodeData[0] = this.nodeName;
                    nodeData[1] = 0;
                    nodeData[2] = 0;
                    nodeData[3] = node.Name;
                    this.node = this.treeList1.AppendNode(nodeData, targetNode);
                    this.node.StateImageIndex = 0;
                    if (node.HasChildNodes)
                    {
                        this.LoadXml2TreeList(node.ChildNodes, this.node);
                    }
                }
            }
        }

        // 播放动画导航组
        private bool playGroup()
        {
            this.groupNodeToPlay.Clear();
            if (this.treeList1.FocusedNode != null)
            {
                this.findAllNode(this.treeList1.FocusedNode.Nodes);
            }
            else
            {
                this.findAllNode(this.treeList1.Nodes);
            }
            if (this.groupNodeToPlay.Count == 0)
            {
                return false;
            }
            TreeListNode node = this.groupNodeToPlay[0] as TreeListNode;
            ICameraTour caTour = node.GetValue("tl_Object") as ICameraTour;
            if (caTour.WaypointsNumber == 1)
            {
                this.playTourWithOneFrame(caTour);
            }
            else
            {
                caTour.Index = 0;
                caTour.Play();
            }
            this.treeList1.SetFocusedNode(node);
            if (!this.isAutoRepeat)
            {
                this.groupNodeToPlay.RemoveAt(0);
            }
            else
            {
                this.currentPlayNodeIndex = 0;
            }
            return true;
        }

        // 播放动画导航(一帧)
        private void playTourWithOneFrame(ICameraTour caTour)
        {
            double num;
            gviCameraTourMode mode;
            IVector3 vector;
            IEulerAngle angle;
            caTour.GetWaypoint(0, out vector, out angle, out num, out mode);
            d3.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
        }

        public void SetDefault()
        {
            if ((this.treeList1.FocusedNode != null) && (this.treeList1.FocusedNode.GetValue("tl_Object") != null))
            {
                (this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour).Stop();
            }
            this.groupNodeToPlay.Clear();
            this.stopTour();
            this.treeList1.ClearNodes();
            this.barCreate.Enabled = true;
            this.barEdit.Enabled = false;
            this.barDelete.Enabled = false;
            this.barPlay.Enabled = false;
            this.barStop.Enabled = false;
            this.barMode.Enabled = false;
            this.barImport.Enabled = true;
            this.barExport.Enabled = false;
            this.barExportVideo.Enabled = false;
            this.iterater = 1;
            this.currentPlayNodeIndex = -1;
        }

        private void stopTour()
        {
            this.barPlay.ImageIndex = 3;
            this.barPlay.Caption = "播放";
            this.barCreate.Enabled = false;
            this.barEdit.Enabled = true;
            this.barDelete.Enabled = true;
            this.barPlay.Enabled = true;
            this.barStop.Enabled = false;
            this.barMode.Enabled = true;
            this.barImport.Enabled = false;
            this.barExport.Enabled = true;
            this.barExportVideo.Enabled = true;
            this.treeList1.Enabled = true;
            this.treeList1.Focus();
        }

        private void treeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "tl_Name")
            {
                string val = e.Value.ToString();
                val.Trim();
                e.Node.SetValue("tl_Name", val);
            }
        }

        private void treeList1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TreeListNode data = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
                TreeListNode node = this.treeList1.CalcHitInfo(this.treeList1.PointToClient(new Point(e.X, e.Y))).Node;
                string val = node.GetValue("tl_ID").ToString();
                if (!data.Equals(node))
                {
                    data.SetValue("tl_ParentID", val);
                }
                data.SetValue("tl_ID", this.iterater);
                this.iterater++;
                //WorkSpaceServices.Instance().NeedSaveProject = true;
            }
            catch (Exception exception)
            {
            }
        }

        private void treeList1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            e.Data.GetData(typeof(TreeListNode));
            TreeListHitInfo info = this.treeList1.CalcHitInfo(this.treeList1.PointToClient(new Point(e.X, e.Y)));
            if ((info.HitInfoType == HitInfoType.Cell) && !info.Node.GetValue("tl_Type").ToString().Equals("Tour"))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (this.treeList1.Enabled)
            {
                if (e.Node == null)
                {
                    this.barCreate.Enabled = true;
                    this.barEdit.Enabled = false;
                    this.barDelete.Enabled = false;
                    this.barStop.Enabled = false;
                    this.barMode.Enabled = true;
                    this.barImport.Enabled = true;
                    this.UpdateButtonState();
                }
                else if (e.Node.GetValue("tl_Type").ToString().Equals("Group"))
                {
                    this.barCreate.Enabled = true;
                    this.barEdit.Enabled = false;
                    this.barDelete.Enabled = true;
                    this.barStop.Enabled = false;
                    this.barMode.Enabled = true;
                    this.barImport.Enabled = true;
                    this.UpdateButtonState();
                }
                else if (e.Node.GetValue("tl_Type").ToString().Equals("Tour"))
                {
                    this.barCreate.Enabled = false;
                    this.barEdit.Enabled = true;
                    this.barDelete.Enabled = true;
                    this.barStop.Enabled = false;
                    this.barMode.Enabled = true;
                    this.barImport.Enabled = false;
                    this.UpdateButtonState();
                }
            }
        }

        private void treeList1_HiddenEditor(object sender, EventArgs e)
        {
            string str = this.treeList1.FocusedNode.GetValue("tl_Type").ToString();
            string val = this.treeList1.FocusedNode.GetValue("tl_Name").ToString().Trim();
            if (val == string.Empty)
            {
                if (str == "Tour")
                {
                    this.treeList1.FocusedNode.SetValue("tl_Name", "新建动画导航" + DateTime.Now.ToString());
                }
                else
                {
                    this.treeList1.FocusedNode.SetValue("tl_Name", "新建动画导航组" + DateTime.Now.ToString());
                }
            }
            else
            {
                this.treeList1.FocusedNode.SetValue("tl_Name", val);
            }
            this.treeList1.CloseEditor();
            this.treeList1.FocusedColumn.OptionsColumn.AllowEdit = false;
            this.treeList1.FocusedColumn.OptionsColumn.AllowFocus = false;
            //WorkSpaceServices.Instance().NeedSaveProject = true;
        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                TreeListHitInfo info = this.treeList1.CalcHitInfo(e.Location);
                if (((e.Button == MouseButtons.Left) && ((info.HitInfoType == HitInfoType.Cell) && (this.treeList1.FocusedNode != null))) && info.Node.GetValue("tl_Type").ToString().Equals("Tour"))
                {
                    if (d3.InteractMode != gviInteractMode.gviInteractNormal)
                    {
                        d3.InteractMode = gviInteractMode.gviInteractNormal;
                        DF3DApplication.Application.Workbench.BarPerformClick("Wander");
                    }
                    TreeListNode node = info.Node;
                    if (node != null)
                    {
                        this.treeList1.SetFocusedNode(node);
                        this.lastNodeInGroup = node;
                        ICameraTour tour = this.treeList1.FocusedNode.GetValue("tl_Object") as ICameraTour;
                        if (tour.WaypointsNumber == 1)
                        {
                            double num;
                            gviCameraTourMode mode;
                            IVector3 vector;
                            IEulerAngle angle;
                            tour.GetWaypoint(0, out vector, out angle, out num, out mode);
                            d3.Camera.SetCamera(vector, angle, gviSetCameraFlags.gviSetCameraNoFlags);
                        }
                        else if (tour.WaypointsNumber > 1)
                        {
                            tour.AutoRepeat = this.isAutoRepeat;
                            tour.Index = 0;
                            tour.Play();
                            this.barPlay.ImageIndex = 4;
                            this.barPlay.Caption = "暂停";
                            this.barCreate.Enabled = false;
                            this.barEdit.Enabled = false;
                            this.barDelete.Enabled = false;
                            this.barPlay.Enabled = true;
                            this.barStop.Enabled = true;
                            this.barMode.Enabled = false;
                            this.barImport.Enabled = false;
                            this.barExport.Enabled = false;
                            this.barExportVideo.Enabled = false;
                            this.treeList1.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            TreeListHitInfo info = this.treeList1.CalcHitInfo(e.Location);
            TreeListNode node = info.Node;
            if (info.Node == null)
            {
                this.treeList1.SetFocusedNode(null);
            }
            if ((e.Button == MouseButtons.Right) && (e.Clicks == 1))
            {
                this.treeList1.SetFocusedNode(node);
                if (node == null)
                {
                    this.popupMenuBlank.ShowPopup(Cursor.Position);
                }
                else if ((node != null) && node.GetValue("tl_Type").ToString().Equals("Group"))
                {
                    this.popupMenuGroup.ShowPopup(Cursor.Position);
                }
                else if ((node != null) && node.GetValue("tl_Type").ToString().Equals("Tour"))
                {
                    this.popupMenuTour.ShowPopup(Cursor.Position);
                }
            }
        }

        // 更新按钮状态
        public void UpdateButtonState()
        {
            this.isEmpty = true;
            this.isEmptyFrameTree(this.treeList1.FocusedNode);
            if (!this.isEmpty)
            {
                this.barPlay.Enabled = true;
                this.barExport.Enabled = true;
                this.barExportVideo.Enabled = true;
                this.barPlayGroup.Enabled = true;
                this.barExportGroup.Enabled = true;
                this.barExportGroupVideo.Enabled = true;
                this.barPlayTour.Enabled = true;
                this.barExportTour.Enabled = true;
                this.barExportTourVideo.Enabled = true;
            }
            else
            {
                this.barPlay.Enabled = false;
                this.barExport.Enabled = false;
                this.barExportVideo.Enabled = false;
                this.barPlayGroup.Enabled = false;
                this.barExportGroup.Enabled = false;
                this.barExportGroupVideo.Enabled = false;
                this.barPlayTour.Enabled = false;
                this.barExportTour.Enabled = false;
                this.barExportTourVideo.Enabled = false;
            }
        }

        private void WriteCircle(TreeListNode node, XmlNode root, XmlDocument doc)
        {
            XmlElement newChild = doc.CreateElement(node.GetValue("tl_Type").ToString());
            newChild.SetAttribute("Name", node.GetValue("tl_Name").ToString());
            if (node.GetValue("tl_Object") != null)
            {
                ICameraTour tour = node.GetValue("tl_Object") as ICameraTour;
                for (int i = 0; i < tour.WaypointsNumber; i++)
                {
                    IEulerAngle angle;
                    this.fp.clear();
                    this.fp.index = i;
                    tour.GetWaypoint(i, out this.vec, out angle, out this.fp.duration, out this.fp.mode);
                    XmlElement element2 = doc.CreateElement("WayPoint");
                    element2.SetAttribute("index", i.ToString());
                    element2.SetAttribute("x", this.vec.X.ToString());
                    element2.SetAttribute("y", this.vec.Y.ToString());
                    element2.SetAttribute("z", this.vec.Z.ToString());
                    element2.SetAttribute("heading", angle.Heading.ToString());
                    element2.SetAttribute("tilt", angle.Tilt.ToString());
                    element2.SetAttribute("roll", angle.Roll.ToString());
                    element2.SetAttribute("duration", this.fp.duration.ToString());
                    element2.SetAttribute("mode", this.fp.mode.ToString());
                    newChild.AppendChild(element2);
                }
            }
            root.AppendChild(newChild);
            if (node.HasChildren)
            {
                foreach (TreeListNode node2 in node.Nodes)
                {
                    this.WriteCircle(node2, newChild, doc);
                }
            }
            //if (((this.treeList1.FocusedNode == null) && (node.Level == 0)) && (node.NextNode != null))
            //{
            //    this.WriteCircle(node.NextNode, root, doc);
            //}
        }
        private bool WriteXML2(string path, TreeListNode focusNode)
        {
            try
            {
                this.treeList1.Refresh();
                XmlDocument doc = new XmlDocument();
                XmlDeclaration newChild = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(newChild);
                XmlNode node = doc.CreateElement("UCAnimationNav");
                doc.AppendChild(node);
                if (this.treeList1.Nodes.Count > 0)
                {
                    if (focusNode == null)
                    {
                        foreach(TreeListNode node1 in this.treeList1.Nodes)
                            this.WriteCircle(node1, node, doc);
                    }
                    else
                    {
                        this.WriteCircle(focusNode, node, doc);
                    }
                }
                doc.Save(path);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        // Nested Types
        // 关键帧属性
        public struct FrameProps
        {
            public int index;
            public double heading;
            public double tilt;
            public double roll;
            public double duration;
            public gviCameraTourMode mode;
            public void clear()
            {
                this.index = -1;
                this.heading = 0.0;
                this.tilt = 0.0;
                this.roll = 0.0;
                this.duration = 0.0;
                this.mode = gviCameraTourMode.gviCameraTourSmooth;
            }
        }

    }
}
