/*----------------------------------------------------------------
// Copyright (C) 2011 中冶集团武汉勘察研究院有限公司
// 版权所有。 
//
// 文件名：DegreeControl.cs
// 文件功能描述：
//
// 
// 创建标识：Zhangl 20060720
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DF2DEdit.UserControl
{
	/// <summary>
	/// 显示度、分、秒的自定义控件
	/// </summary>
	public class DegreeControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxDegree;
		private System.Windows.Forms.TextBox textBoxMinute;
		private System.Windows.Forms.TextBox textBoxSecond;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DegreeControl()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitComponent 调用后添加任何初始化
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.textBoxDegree = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxMinute = new System.Windows.Forms.TextBox();
			this.textBoxSecond = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBoxDegree
			// 
			this.textBoxDegree.Location = new System.Drawing.Point(0, 0);
			this.textBoxDegree.Name = "textBoxDegree";
			this.textBoxDegree.Size = new System.Drawing.Size(32, 21);
			this.textBoxDegree.TabIndex = 1;
			this.textBoxDegree.Text = "0";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(8, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "°";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(56, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(8, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "′";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(80, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(8, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "″";
			// 
			// textBoxMinute
			// 
			this.textBoxMinute.Location = new System.Drawing.Point(40, 0);
			this.textBoxMinute.Name = "textBoxMinute";
			this.textBoxMinute.Size = new System.Drawing.Size(16, 21);
			this.textBoxMinute.TabIndex = 1;
			this.textBoxMinute.Text = "0";
			// 
			// textBoxSecond
			// 
			this.textBoxSecond.Location = new System.Drawing.Point(64, 0);
			this.textBoxSecond.Name = "textBoxSecond";
			this.textBoxSecond.Size = new System.Drawing.Size(16, 21);
			this.textBoxSecond.TabIndex = 1;
			this.textBoxSecond.Text = "0";
			// 
			// DegreeControl
			// 
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxDegree);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxMinute);
			this.Controls.Add(this.textBoxSecond);
			this.Name = "DegreeControl";
			this.Size = new System.Drawing.Size(88, 24);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 获得当前角度的度
		/// </summary>
		public int Degree
		{
			get
			{
				//try转成数字，然后转成合理？
				try
				{
					return System.Int32.Parse(this.textBoxDegree.Text);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				//如果合理，转换
				try
				{
					this.textBoxDegree.Text = value.ToString();
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// 获得当前角度的分
		/// </summary>
		public int Minute
		{
			get
			{
				//try转成数字，然后转成合理？
				try
				{
					return System.Int32.Parse(this.textBoxMinute.Text);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				//如果合理，转换
				try
				{
					this.textBoxMinute.Text = value.ToString();
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// 获得当前角度的秒
		/// </summary>
		public int Second
		{
			get
			{
				//try转成数字，然后转成合理？
				try
				{
					return System.Int32.Parse(this.textBoxSecond.Text);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				//如果合理，转换
				try
				{
					this.textBoxSecond.Text = value.ToString();
				}
				catch
				{
				}
			}
		}


		/// <summary>
		/// 获得当前角度的值
		/// </summary>
		public double Angle
		{
			set
			{
				this.Degree = (int)Math.Floor(value);
				value -= Math.Floor(value);
				value *= 60;
				this.Minute = (int)Math.Floor(value);
				value -= Math.Floor(value);
				value *= 60;
				this.Second = (int)value;
			}
			get
			{
				return this.Degree+ this.Minute/60.0 + this.Second/3600.0;
			}
		}

		/// <summary>
		/// 验证用户的输入是否正确
		/// </summary>
		/// <returns>验证是否成功</returns>
		public bool Envaluate()
		{
			try
			{
				System.Int32.Parse(this.textBoxDegree.Text);
			}
			catch
			{
				return false;
			}

			try
			{
				System.Int32.Parse(this.textBoxMinute.Text);
			}
			catch
			{
				return false;
			}

			try
			{
				System.Int32.Parse(this.textBoxSecond.Text);
			}
			catch
			{
				return false;
			}

			return true;
		}
	}
}
