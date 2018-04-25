using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Gvitech.CityMaker.FdeCore;
using DF3DPipeCreateTool.Class;
using DF3DPipeCreateTool.Service;
using DFDataConfig.Class;
using DF3DData.Class;
using DF3DEdit.Service;
using DevExpress.Utils;
using DF3DEdit.Class;
using DF3DEdit.Delegate;

namespace DF3DEdit.UC
{
    public partial class UCEditFacilityStyle : XtraUserControl
    {
        public UCEditFacilityStyle()
        {
            InitializeComponent();

            LoadStyle();
            InitData();
            SelectCollection.Instance().SelectionChangedEvent += new SelectionChangedEventHandler(UCEditFacilityStyle_SelectionChangedEvent);
        }

        void UCEditFacilityStyle_SelectionChangedEvent()
        {
            InitData();
        }

        private void InitData()
        {
            try
            {
                this.cbxFacilityStyle.SelectedIndex = -1;
                this.listStyles.SelectedIndex = -1;
                DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
                if (dffc == null) return;

                int count = SelectCollection.Instance().GetCount(false);
                if (count == 1)
                {
                    HashMap hm = SelectCollection.Instance().GetSelectGeometrys();
                    if (hm != null && hm.Count == 1)
                    {
                        IRowBufferCollection rowBufferCollection = hm[dffc] as IRowBufferCollection;
                        if (rowBufferCollection.Count == 1)
                        {
                            IRowBuffer rowBuffer = rowBufferCollection.Get(0);
                            int index = rowBuffer.FieldIndex("StyleId");
                            if (index != -1 && !rowBuffer.IsNull(index))
                            {
                                string styleId = rowBuffer.GetValue(index).ToString();
                                for (int i = 0; i < this.cbxFacilityStyle.Properties.Items.Count; i++)
                                {
                                    FacStyleClass fsc = this.cbxFacilityStyle.Properties.Items[i] as FacStyleClass;
                                    if (fsc.ObjectId == styleId)
                                    {
                                        this.cbxFacilityStyle.SelectedIndex = i;
                                        this.listStyles.SelectedIndex = i;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadStyle()
        {
            try
            {
                this.imageCollection1.Images.Clear();
                this.listStyles.Items.Clear();
                this.cbxFacilityStyle.Properties.Items.Clear();

                DF3DFeatureClass dffc = CommonUtils.Instance().CurEditLayer;
                if (dffc != null)
                {
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc != null)
                    {
                        FacClassReg reg = FacilityInfoService.GetFacClassRegByFeatureClassID(fc.Guid.ToString());
                        if (reg != null)
                        {
                            List<FacStyleClass> list1 = FacilityInfoService.GetFacStyleByFacClassCode(reg.FacClassCode);
                            if (list1 != null)
                            {
                                foreach (FacStyleClass fsc in list1)
                                {
                                    int imageIndex = this.imageCollection1.Images.Add(fsc.Thumbnail);
                                    ImageComboBoxItem item = new ImageComboBoxItem();
                                    item.Value = fsc;
                                    item.Description = fsc.ToString();
                                    item.ImageIndex = imageIndex;
                                    this.listStyles.Items.Add(item);
                                    this.cbxFacilityStyle.Properties.Items.Add(fsc);
                                }
                                for (int i = 0; i < this.listStyles.Items.Count; i++)
                                {
                                    this.listStyles.Items[i].ImageIndex = i;
                                }
                                this.listStyles.Refresh();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SelectCollection.Instance().FacStyleClassChanged(this.cbxFacilityStyle.SelectedItem as FacStyleClass);
        }

        private void cbxFacilityStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listStyles.SelectedIndex = this.cbxFacilityStyle.SelectedIndex;
        }

        private void listStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxFacilityStyle.SelectedIndex = this.listStyles.SelectedIndex;
        }
    }
}
