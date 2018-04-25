using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using System.Collections;
using DevExpress.XtraEditors;
using System.IO;
using DFCommon.Class;
using ESRI.ArcGIS.DataSourcesGDB;
using DFWinForms.Class;

namespace DF2DDocumentManage.Class
{
    public class ImportFiles
    {
        private bool m_BImportFile;
        private string m_strTableName;
        private ITable m_pTabDocment;
        private IWorkspace m_pCurWorkspace;
        //文件信息变量
        private string m_strFileName;
        private string m_strFileClass;
        private int m_nFileType;
        private string m_strFileVersion;
        private string m_strDiscription;
        private string m_strMark;
        private string m_strFileType;
        private string m_strFileSaver;
        private string m_strFilePromulgateUnit;
        private string m_strFileFolder;
        private DateTime m_dCreationDate;
        private DateTime m_dSaveDate;
        
        //技改项目信息变量
        private string m_strProName;
        private string m_strProNum;
        private string m_strDesign;
        private string m_strDesigner;
        private string m_nDesignPhase;
        private DateTime m_strProDate;

        //技改项目表字段名
        private string FIELD_PRONAME = "PROJECTNAME";
        private string FIELD_PRONUM = "PROJECTNUM";
        private string FIELD_DESIGN = "DESIGN";
        private string FIELD_DESIGNER = "DESIGNER";
        private string FIELD_DESIGNPHASE = "DESIGNPHASE";
        private string FIELD_PROJECTDATE = "PROJECTDATE";
        private string FIELD_CADNAME = "CADNAME";
        private string FIELD_CADFILE = "OBJFILE";

        //表字段名
        private string FIELD_FILENAME = "FILENAME";
        private string FIELD_FILECLASS = "FileClass";
        private string FIELD_VERSION = "VERSION";
        private string FIELD_PROMULGATEDATE = "PROMULGATEDATE";
        private string FIELD_PROMULGATEUNIT = "PROMULGATEUNIT";
        private string FIELD_FILETYPE = "FILETYPE";
        private string FIELD_DISCRIPTION = "Description";
        private string FIELD_MARK = "MARK";
        private string FIELD_SAVER = "SAVER";
        private string FIELD_SAVEDATE = "SAVEDATE";
        private string FIELD_SAVEYEAR = "SAVEYEAR";
        private string FIELD_OBJFILE = "OBJFILE";

        private ArrayList m_ArrayListFiles;

        public void SetPara(ArrayList arrayListFiles, string fileName, string fileVersion, string fileDiscription,
            string mark, string fileClass, string fileSaver, string promulgateUnit, string fileFolder, DateTime createDate)
        {
            this.m_BImportFile = true;
            this.m_ArrayListFiles = arrayListFiles;
            m_strFileName = fileName;
            m_strFileVersion = fileVersion;
            m_strDiscription = fileDiscription;
            m_strMark = mark;
            m_strFileClass = fileClass;
            m_strFileSaver = fileSaver;
            m_strFilePromulgateUnit = promulgateUnit;
            m_strFileFolder = fileFolder;
            m_dCreationDate = createDate;
            

        }

        public void SetPara(ArrayList arrayListFiles, string proName, string proNum, 
            string design,string designer,string designPhase,DateTime proDate)
        {
            this.m_BImportFile = false;
            this.m_ArrayListFiles = arrayListFiles;
            this.m_strProName = proName;
            this.m_strProNum = proNum;
            this.m_strDesign = design;
            this.m_strDesigner = designer;
            this.m_nDesignPhase = designPhase;
            this.m_strProDate = proDate;
          
        }
        private IWorkspace GetWorkSpace()
        {
            IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
            string path = Config.GetConfigValue("2DDocument");
            IWorkspace pWs = pWsF.OpenFromFile(path, 0);
            return pWs;

        }
        public void Import()
        {
            WaitForm.Start("开始上传,请稍后...");
            if (m_BImportFile)
            {
                m_strTableName = "WSGRI_Docment";
                this.ImportFile(m_strTableName);
            }
            else
            {
                m_strTableName = "WSGRI_Map";
                this.ImportProject(m_strTableName);
            }
            WaitForm.Stop();
        }
        private void ImportFile(string strTableName)
        {
            m_dSaveDate = System.DateTime.Now;
            if (this.m_ArrayListFiles == null || this.m_ArrayListFiles.Count == 0)
            { XtraMessageBox.Show("请指定需要导入的文件", "导入文件"); return; }
            if (m_strFileClass == null)
            {
                XtraMessageBox.Show("资料类型不能为空", "导入文件");
                return;
            }
            try
            {
                WaitForm.SetCaption("正在连接数据库,请稍后...");
                m_pCurWorkspace = GetWorkSpace();
                if (m_pCurWorkspace == null) return;
                m_pTabDocment = (m_pCurWorkspace as IFeatureWorkspace).OpenTable(strTableName);
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show("无法打开表" + strTableName + "请确定数据库中是否存在该表", "导入文件");
                WaitForm.Stop();
                return;
            }
            if (m_pTabDocment == null) return;
            IRow pReportRow = null;
            string sFPath = "";
            for (int i = 0; i < m_ArrayListFiles.Count; i++)
            {
                m_strFileName = ((FileInfo)m_ArrayListFiles[i]).Name;
                m_strFileType = m_strFileName.Substring(m_strFileName.LastIndexOf('.'));
                WaitForm.SetCaption("正在上传" + m_strFileName + "请稍后...");
                m_strFileFolder = ((FileInfo)m_ArrayListFiles[i]).Directory.Name;
                if (!IsExist(FIELD_FILENAME, m_strFileName, m_pTabDocment))
                {
                    sFPath = ((FileInfo)m_ArrayListFiles[i]).FullName;
                    m_dCreationDate = ((FileInfo)m_ArrayListFiles[i]).LastWriteTime;

                    IMemoryBlobStream mbs = GetStreamFromFile(sFPath) as IMemoryBlobStream;
                    if (mbs.Size == 0)
                    {
                        XtraMessageBox.Show(Path.GetFileName(sFPath) + " 为空文件", "提示");
                        return;
                    }
                    //获得文件扩展信息
                    //ArrayList aDetailedInfo = null;
                    //aDetailedInfo = GetDetailedFileInfo(sFPath);
                    //向数据库中写入记录
                    pReportRow = m_pTabDocment.CreateRow();
                    pReportRow.set_Value(m_pTabDocment.FindField(FIELD_FILENAME), m_strFileName);
                    pReportRow.set_Value(m_pTabDocment.FindField(FIELD_FILECLASS), m_strFileClass);
                    pReportRow.set_Value(m_pTabDocment.FindField(FIELD_VERSION), m_strFileVersion);
                    pReportRow.set_Value(m_pTabDocment.FindField(FIELD_PROMULGATEDATE), m_strProDate);
                    pReportRow.set_Value(m_pTabDocment.FindField(FIELD_PROMULGATEUNIT), m_strFilePromulgateUnit);
                    pReportRow.set_Value(m_pTabDocment.FindField(FIELD_FILETYPE), m_strFileType);
                    pReportRow.set_Value(m_pTabDocment.FindField(FIELD_DISCRIPTION), m_strDiscription);
                    pReportRow.set_Value(m_pTabDocment.FindField(FIELD_MARK), m_strMark);
                    pReportRow.set_Value(m_pTabDocment.FindField(FIELD_SAVER), m_strFileSaver);
                    pReportRow.set_Value(m_pTabDocment.FindField(FIELD_SAVEDATE), m_dSaveDate);
                    //pReportRow.set_Value(m_pTabDocment.FindField(FIELD_SAVEYEAR), m_dSaveDate.Year);
                    pReportRow.set_Value(m_pTabDocment.FindField(FIELD_OBJFILE), mbs);
                    pReportRow.Store();
                }
                else
                {
                    XtraMessageBox.Show((m_strFileName + "已存在"),"导入文件");
                    WaitForm.Stop();
                    continue;
                }
            }
        }
        private void ImportProject(string strTableName)
        {
            m_dSaveDate = System.DateTime.Now;
            if (this.m_strProName == null)
            { XtraMessageBox.Show("请指定需要导入的文件", "导入文件"); return; }
            try
            {
                WaitForm.SetCaption("正在连接数据库,请稍后...");
                m_pCurWorkspace = GetWorkSpace();
                if (m_pCurWorkspace == null) return;
                m_pTabDocment = (m_pCurWorkspace as IFeatureWorkspace).OpenTable(strTableName);
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show("无法打开表" + strTableName + "请确定数据库中是否存在该表", "导入文件");
                WaitForm.Stop();
                return;
            }        
            if (m_pTabDocment == null) return;
            IRow pReportRow = null;
            string sFPath = "";
            try
            {
                for (int i = 0; i < m_ArrayListFiles.Count; i++)
                {
                    m_strFileName = ((FileInfo)m_ArrayListFiles[i]).Name;
                    WaitForm.SetCaption("正在上传" + m_strFileName + "请稍后...");
                    m_strFileFolder = ((FileInfo)m_ArrayListFiles[i]).Directory.Name;
                    if (!IsExist(FIELD_PRONAME, m_strFileName, m_pTabDocment))
                    {
                        sFPath = ((FileInfo)m_ArrayListFiles[i]).FullName;
                        m_dCreationDate = ((FileInfo)m_ArrayListFiles[i]).LastWriteTime;

                        IMemoryBlobStream mbs = GetStreamFromFile(sFPath) as IMemoryBlobStream;
                        if (mbs.Size == 0)
                        {
                            XtraMessageBox.Show(Path.GetFileName(sFPath) + " 为空文件", "提示");
                            return;
                        }
                        //获得文件扩展信息
                        //ArrayList aDetailedInfo = null;
                        //aDetailedInfo = GetDetailedFileInfo(sFPath);
                        //向数据库中写入记录
                        pReportRow = m_pTabDocment.CreateRow();
                        pReportRow.set_Value(m_pTabDocment.FindField(FIELD_PRONAME), m_strProName);
                        pReportRow.set_Value(m_pTabDocment.FindField(FIELD_PRONUM), m_strProNum);
                        pReportRow.set_Value(m_pTabDocment.FindField(FIELD_DESIGN), m_strDesign);
                        pReportRow.set_Value(m_pTabDocment.FindField(FIELD_DESIGNER), m_strDesigner);
                        pReportRow.set_Value(m_pTabDocment.FindField(FIELD_PROJECTDATE), m_strProDate);
                        pReportRow.set_Value(m_pTabDocment.FindField(FIELD_CADNAME), m_strFileName);
                        pReportRow.set_Value(m_pTabDocment.FindField(FIELD_CADFILE), mbs);
                        pReportRow.set_Value(m_pTabDocment.FindField(FIELD_DESIGNPHASE), m_nDesignPhase);
                        pReportRow.Store();
                    }
                    else
                    {
                        XtraMessageBox.Show(m_strFileName + "已存在","导入文件");
                        WaitForm.Stop();
                        continue;

                    }
                }
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
            }
           

        }

        private bool IsExist(string strField, string strFileName, ITable pTable)
        {
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = strField + "='" + strFileName + "'";
            ICursor cursor = pTable.Search(filter, true);
            IRow row = cursor.NextRow();
            if (row == null) return false;
            else
                return true;
        }

        private IMemoryBlobStream GetStreamFromFile(string fileName)
        {
            MemoryBlobStreamClass fileBlob = new MemoryBlobStreamClass();
            fileBlob.LoadFromFile(fileName);
            return fileBlob;
        }
    }
}
