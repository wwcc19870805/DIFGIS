using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DF2DTool.Class;

namespace DF2DTool.Frm
{
    public partial class DatasetEdit : Form
    {
        private DataSetNames dNames = new DataSetNames();

        private DataSet dataDs = new DataSet("dataDs");	

        public DataSetNames dsNames
        {
            get { return dNames; }
        }
        public DatasetEdit()
        {
            InitializeComponent();
        }

        public DatasetEdit(DataSetNames iName)
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();
            dNames = iName;
        }
    }
}
