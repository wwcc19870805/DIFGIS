/*----------------------------------------------------------------
            // Copyright (C) 2017 中冶集团武汉勘察研究院有限公司
            // 版权所有。 
            //
            // 文件名：BufferDis.cs
            // 文件功能描述：缓冲区距离
            //               
            // 
            // 创建标识：LuoXuan
            //
            // 修改描述：

----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DF2DControl.Base;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DevExpress.XtraEditors;

namespace DF2DEdit.BaseEdit
{
    public class BufferDis : AbstractMap2DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;

            SpinEdit spinEdit = sender as SpinEdit;
            int strDis = Convert.ToInt32(spinEdit.EditValue);
            Class.SelectionEnv.Tolerate = strDis;
        }
    }
}
