namespace DF2DDataCheck.Frm
{
    partial class frmProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /*private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pro = new System.Windows.Forms.ProgressBar();
            this.lbltext = new System.Windows.Forms.Label();
            this.m_Timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pro
            // 
            this.pro.Location = new System.Drawing.Point(71, 68);
            this.pro.Name = "pro";
            this.pro.Size = new System.Drawing.Size(1086, 82);
            this.pro.TabIndex = 0;
            // 
            // lbltext
            // 
            this.lbltext.AutoSize = true;
            this.lbltext.Location = new System.Drawing.Point(121, 199);
            this.lbltext.Name = "lbltext";
            this.lbltext.Size = new System.Drawing.Size(0, 14);
            this.lbltext.TabIndex = 1;
            // 
            // frmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 542);
            this.Controls.Add(this.lbltext);
            this.Controls.Add(this.pro);
            this.Name = "frmProgress";
            this.Text = "计算中……";
            this.Load += new System.EventHandler(this.frmProgress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }*/

        #endregion

        private System.Windows.Forms.ProgressBar pro;
        private System.Windows.Forms.Label lbltext;
        private System.Windows.Forms.Timer m_Timer;
    }
}