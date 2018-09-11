#define OPENURL

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using CodeTool.Common.Model;
using CodeTool.Config;
using WeifenLuo.WinFormsUI.Docking;

namespace CodeTool.Forms
{
    public partial class MainForm : Form
    {
        
       

        public static WeifenLuo.WinFormsUI.Docking.DockPanel DockPanel;

        private DatabaseForm _frmDatabase;
        private TemplateForm _frmTemplate;
        public DebugForm FrmDebug;

        public MainForm()
        {
            InitializeComponent();
            labNewVersion.Tag = SqlConfig.DownloadUrl;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.Text = this.Text + " V" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            DockPanel = this.dockPanel1;

            _frmDatabase = new DatabaseForm();
            _frmDatabase.OutputCode += new Action<Database>(frmDatabase_OutputCode);
            _frmDatabase.CreateCode += new Action<Table>(frmDatabase_CreateCode);
            _frmDatabase.Show(DockPanel);

            _frmTemplate = new TemplateForm();
            _frmTemplate.TemplateChanged += new Action(frmTemplate_TemplateChanged);
            _frmTemplate.Show(DockPanel);

            FrmDebug = new DebugForm();
            FrmDebug.Show(DockPanel);

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();

            OpenUrl(SqlConfig.HomeUrl);
        }

        void frmTemplate_TemplateChanged()
        {
            List<DockContent> contents = new List<DockContent>();
            foreach (DockContent content in dockPanel1.Documents)
            {
                contents.Add(content);
            }

            foreach (DockContent content in contents)
            {
                if (content is ITemplateChanged)
                {
                    ITemplateChanged templateChanged = content as ITemplateChanged;
                    templateChanged.OnTemplateChanged();
                }
            }
        }

        #region �汾���
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            int count = 0;
            while (count < 3)
            {
                try
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(SqlConfig.XmlUrl);
                    e.Result = xml;
                    return;
                }
                catch
                {
                    count++;
                }
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                labNewVersion.Text = "���ӷ�����ʧ��!";
                labNewVersion.LinkColor = Color.Red;
                return;
            }
            //http://www.socansoft.com/downloads/socancode/SocanCode.rar
            XmlDocument xml = e.Result as XmlDocument;
            XmlNode display = xml.SelectSingleNode("DOCUMENT").SelectSingleNode("item").SelectSingleNode("display");
            Version lastVersion = new Version(display.SelectSingleNode("content2").InnerText);


            Version currVersion = Assembly.GetExecutingAssembly().GetName().Version;
            labNewVersion.Tag = display.SelectSingleNode("button").Attributes["buttonlink"].Value;

            if (lastVersion > currVersion)
            {
                labNewVersion.Text = @"�����°汾 V" + lastVersion + @", �����˴�����";
                labNewVersion.LinkColor = Color.Red;
            }
            else
            {
                labNewVersion.Text = @"��ǰ�汾�Ѿ������°汾";
                labNewVersion.LinkColor = Color.Green;
            }
        }
        #endregion

        #region frmDatabase�¼�
        void frmDatabase_OutputCode(Database db)
        {
            OutputCodeForm frm = new OutputCodeForm(db);
            frm.ShowDebug += new Action<string, string, bool>(frm_ShowDebug);
            frm.Show(MainForm.DockPanel);
        }

        void frmDatabase_CreateCode(Table table)
        {
            CreateCodeForm frm = new CreateCodeForm(table);
            frm.ShowDebug += new Action<string, string, bool>(frm_ShowDebug);
            frm.Show(MainForm.DockPanel);
        }

        void frm_ShowDebug(string dbJson, string setJson, bool show)
        {
            FrmDebug.txtDbJson.Text = dbJson;
            FrmDebug.txtSetJson.Text = setJson;
            FrmDebug.Focus();
            if (show)
                FrmDebug.Show(DockPanel);
        }
        #endregion

        #region ��ǩҳ�ϵ��Ҽ��˵�
        private void mnuClose_Click(object sender, EventArgs e)
        {
            this.dockPanel1.ActiveContent.DockHandler.Close();
        }

        private void mnuCloseOther_Click(object sender, EventArgs e)
        {
            List<DockContent> contents = new List<DockContent>();
            foreach (DockContent content in dockPanel1.Documents)
            {
                contents.Add(content);
            }

            foreach (DockContent content in contents)
            {
                if (content != this.dockPanel1.ActiveContent)
                {
                    content.Close();
                }
            }
        }

        private void mnuCloseAll_Click(object sender, EventArgs e)
        {
            mnuCloseOther_Click(sender, e);
            mnuClose_Click(sender, e);
        }
        #endregion

        #region �˵�
        /// <summary>
        /// ���ɴ���
        /// </summary>
        private void menuCreateCode_Click(object sender, EventArgs e)
        {
            _frmDatabase.CreateCurrentTableCode();
        }

        /// <summary>
        /// �������
        /// </summary>
        private void menuOutputCode_Click(object sender, EventArgs e)
        {
            _frmDatabase.OutputSelectedDatabaseCode();
        }

        /// <summary>
        /// ���ݿ�
        /// </summary>
        private void menufrmDatabase_Click(object sender, EventArgs e)
        {
            _frmDatabase.Show(DockPanel);
        }

        /// <summary>
        /// ģ��
        /// </summary>
        private void menuTemplate_Click(object sender, EventArgs e)
        {
            _frmTemplate.Show(DockPanel);
        }

        /// <summary>
        /// ����
        /// </summary>
        private void menuShowDebug_Click(object sender, EventArgs e)
        {
            FrmDebug.Show(DockPanel);
        }

        /// <summary>
        /// ����תģ��
        /// </summary>
        private void menuCodeToTemplateForm_Click(object sender, EventArgs e)
        {
            CodeToTemplateForm frm = new CodeToTemplateForm();
            frm.Show(DockPanel);
        }

        /// <summary>
        /// ����ȥ�кſ���
        /// </summary>
        private void menuCodeClear_Click(object sender, EventArgs e)
        {
            CodeClearForm frm = new CodeClearForm();
            frm.Show(DockPanel);
        }
        /// <summary>
        /// cSharpת��VBNET
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cSharpת��VBNETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CSharpToVBForm frm = new CSharpToVBForm();
            frm.Show(DockPanel);
        }
        /// <summary>
        /// �ٷ���վ
        /// </summary>
        private void menuWebsite_Click(object sender, EventArgs e)
        {
            OpenUrl(SqlConfig.HomeUrl);
        }

        /// <summary>
        /// ����
        /// </summary>
        private void menuAbout_Click(object sender, EventArgs e)
        {
            AboutForm frm = new AboutForm();
            frm.ShowDialog();
        }
        #endregion

        private void labNewVersion_Click(object sender, EventArgs e)
        {
            var toolStripStatusLabel = sender as ToolStripStatusLabel;
            if (toolStripStatusLabel != null)
            {
                object tag = toolStripStatusLabel.Tag;
                Process.Start(new ProcessStartInfo("IEXPLORE.EXE", tag.ToString()));
            }
        }

        private void dockPanel1_ContentAdded(object sender, DockContentEventArgs e)
        {
            if (e.Content.DockHandler.ShowHint == DockState.Document
                   || e.Content.DockHandler.ShowHint == DockState.Unknown)
            {
                e.Content.DockHandler.TabPageContextMenuStrip = cmsDockPanel;
            }
        }

        /// <summary>
        /// ��һ���ļ�
        /// </summary>
        public static void OpenFile(FileInfo fi)
        {
            foreach (DockContent content in DockPanel.Documents)
            {
                if (content.GetType() == typeof(EditForm))
                {
                    EditForm frm = content as EditForm;
                    if (frm.FileInfo.FullName.Equals(fi.FullName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        content.Activate();
                        return;
                    }
                }
            }

            EditForm frm2 = new EditForm(fi);
            frm2.Show(DockPanel);
        }

        /// <summary>
        /// ��һ��������
        /// </summary>
        public static void OpenUrl(string url)
        {
#if OPENURL
            if (DockPanel.InvokeRequired)
            {
                DockPanel.BeginInvoke(new Action<string>(OpenUrl), new object[] { url });
                return;
            }

            WebBrowser.BrowserForm frm = new WebBrowser.BrowserForm();
            frm.Show(DockPanel);
            frm.Go(url);
#endif
        }


    }
}