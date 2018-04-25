using System;
using Infragistics.Win.UltraWinToolbars ;
/*----------------------------------------------------------------
			// Copyright (C) 2005 中冶集团武汉勘察研究院有限公司
			// 版权所有。 
			//
			// 文件名：EditContextMenu.cs
			// 文件功能描述:绘制、修改时右键弹出的内容菜单
			//
			// 
			// 创建标识：YuanHY 20060329
            // 操作说明：
			// 修改记录：
----------------------------------------------------------------*/
namespace WSGRI.DigitalFactory.DFEditorTool
{
	/// <summary>
	/// EditContextMenu 的摘要说明。
	/// </summary>
	public class EditContextMenu
	{
		public UltraToolbarsManager toolbarsManager = null;
		
		public EditContextMenu()
		{
			toolbarsManager = new UltraToolbarsManager();
		
			this.createDrawContextMenu();
		}

		//绘制内容菜单
		private void createDrawContextMenu()
		{
			ButtonTool btnUndo       = new ButtonTool("btnUndo");
			ButtonTool btnLeftCorner = new ButtonTool("btnLeftCorner");
			ButtonTool btnFixAzim    = new ButtonTool("btnFixAzim");
			ButtonTool btnLengthAzim = new ButtonTool("btnLengthAzim");
			ButtonTool btnSideLength = new ButtonTool("btnSideLength");
			ButtonTool btnFixLength  = new ButtonTool("btnFixLength");
			ButtonTool btnAbsXYZ     = new ButtonTool("btnAbsXYZ");			
			ButtonTool btnRelaXYZ    = new ButtonTool("btnRelaXYZ");
			ButtonTool btnParllel    = new ButtonTool("btnParllel");
			ButtonTool btnRt         = new ButtonTool("btnRt");	
			ButtonTool btnColse      = new ButtonTool("btnColse");
			ButtonTool btnEnd        = new ButtonTool("btnEnd");
			ButtonTool btnESC        = new ButtonTool("btnESC");
		 
			btnUndo.SharedProps.Caption       = "键回退(&U)";
			btnLeftCorner.SharedProps.Caption = "输入左折角(&N)...";
			btnFixAzim.SharedProps.Caption    = "输入方位角(&O)...";
			btnFixLength.SharedProps.Caption  = "输入长度(&D)...";
			btnLengthAzim.SharedProps.Caption = "长度+方位角(&F)..";
			btnSideLength.SharedProps.Caption = "矩形边长(&B)...";
			btnAbsXYZ.SharedProps.Caption     = "绝对坐标(&A)...";
			btnRelaXYZ.SharedProps.Caption    = "相对坐标(&R)...";	
			btnParllel.SharedProps.Caption    = "平行(&P)...";	
			btnRt.SharedProps.Caption         = "直角(&S)...";			
			btnColse.SharedProps.Caption      = "封闭完成(&C)";
			btnEnd.SharedProps.Caption        = "完成(&E)";
			btnESC.SharedProps.Caption        = "取消(ESC)";


			PopupMenuTool drawPopupMenuTool  = new PopupMenuTool("drawPopupMenuTool");
			drawPopupMenuTool.Tools.Add(btnUndo);
			drawPopupMenuTool.Tools.Add(btnLeftCorner);
			drawPopupMenuTool.Tools.Add(btnFixAzim);
			drawPopupMenuTool.Tools.Add(btnFixLength);
			drawPopupMenuTool.Tools.Add(btnLengthAzim);
			drawPopupMenuTool.Tools.Add(btnSideLength);
			drawPopupMenuTool.Tools.Add(btnAbsXYZ);
			drawPopupMenuTool.Tools.Add(btnRelaXYZ);	
			drawPopupMenuTool.Tools.Add(btnParllel);
			drawPopupMenuTool.Tools.Add(btnRt);	
			drawPopupMenuTool.Tools.Add(btnColse);
			drawPopupMenuTool.Tools.Add(btnEnd);
			drawPopupMenuTool.Tools.Add(btnESC);

			PopupMenuTool modifyPopupMenuTool = new PopupMenuTool("modifyPopupMenuTool");
			modifyPopupMenuTool.Tools.Add(btnFixAzim);
			modifyPopupMenuTool.Tools.Add(btnFixLength);
			modifyPopupMenuTool.Tools.Add(btnParllel);
			modifyPopupMenuTool.Tools.Add(btnESC);			

			
			drawPopupMenuTool.Tools["btnLeftCorner"].InstanceProps.IsFirstInGroup = true;
			drawPopupMenuTool.Tools["btnFixAzim"].InstanceProps.IsFirstInGroup    = true;
			drawPopupMenuTool.Tools["btnSideLength"].InstanceProps.IsFirstInGroup = true;
			drawPopupMenuTool.Tools["btnAbsXYZ"].InstanceProps.IsFirstInGroup     = true;
			drawPopupMenuTool.Tools["btnParllel"].InstanceProps.IsFirstInGroup    = true;
			drawPopupMenuTool.Tools["btnColse"].InstanceProps.IsFirstInGroup      = true;

			modifyPopupMenuTool.Tools["btnParllel"].InstanceProps.IsFirstInGroup   = true;
			modifyPopupMenuTool.Tools["btnESC"].InstanceProps.IsFirstInGroup       = true;


			toolbarsManager.Tools.Add(btnUndo);
			toolbarsManager.Tools.Add(btnLeftCorner);
			toolbarsManager.Tools.Add(btnFixAzim);
			toolbarsManager.Tools.Add(btnFixLength);
			toolbarsManager.Tools.Add(btnLengthAzim);
			toolbarsManager.Tools.Add(btnSideLength);
			toolbarsManager.Tools.Add(btnAbsXYZ);
			toolbarsManager.Tools.Add(btnRelaXYZ);
			toolbarsManager.Tools.Add(btnParllel);
			toolbarsManager.Tools.Add(btnRt);
			toolbarsManager.Tools.Add(btnColse);
			toolbarsManager.Tools.Add(btnEnd);
			toolbarsManager.Tools.Add(btnESC);
			toolbarsManager.Tools.Add(drawPopupMenuTool);
			toolbarsManager.Tools.Add(modifyPopupMenuTool);

		}

		public void ActiveEditContextMenu(string menuStr, System.Windows.Forms.Control control)
		{
			toolbarsManager.ShowPopup(menuStr,control);
		}


	}
}
