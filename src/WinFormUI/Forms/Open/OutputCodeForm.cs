using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CodeTool.Common;
using CodeTool.Common.Generator;
using CodeTool.Common.Model;
using CodeTool.Config;
using CodeTool.Forms;

namespace CodeTool
{
    public partial class OutputCodeForm : WeifenLuo.WinFormsUI.Docking.DockContent, ITemplateChanged
    {
        private Database db;
        public event Action<string> ShowReadmeOfTemplate;
        public event Action<string, string, bool> ShowDebug;

        public OutputCodeForm(Database db)
        {
            InitializeComponent();
            selectTableUserControl1.Db = db;

            this.db = db;
            this.TabText = string.Format("������� {0}", db.Name);
        }

        /// <summary>
        /// ���ɵ�·��λ��
        /// </summary>
        private string CreateFilePath
        {
            get { return txtPath.Text.TrimEnd(' ', '\\') + @"\CodeTool����Ĵ���"; }
        }

        private void btnOutputCode_Click(object sender, EventArgs e)
        {
            foreach (Table table in selectTableUserControl1.SelectedTables)
            {
                if (table.Fields.Count == 0)
                {
                    MessageBoxMessage.Alert(string.Format("��{0}�������κ��ֶΣ��޷����ɣ�", table.Name));
                    return;
                }
            }

            if (false == MessageBoxMessage.Confirm("ȷ��Ҫ���������?"))
            {
                return;
            }

            if (Directory.Exists(CreateFilePath))
            {
                if (MessageBoxMessage.Confirm("��Ŀ¼�Ѵ��ڣ��Ƿ�ɾ��?"))
                {
                    try
                    {
                        Directory.Delete(CreateFilePath, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxMessage.Error(ex.ToString());
                    }
                }
            }

            //selectTemplateUserControl1.SaveSetting();
            db.Selects = selectTableUserControl1.SelectedTables;

            //��Settings�ж��������һ���趨��ֵΪ��ǰѡ�е����б�
            List<Setting> settings = selectTemplateUserControl1.Settings;

            //Ŀ¼�п��ܲ����ڣ����û�������κδ������Ŀ¼��������ȴ���Ŀ¼
            if (false == Directory.Exists(CreateFilePath))
            {
                Directory.CreateDirectory(CreateFilePath);
            }

            try
            {
                Template template = new Template();
                template.TemplateFolder = selectTemplateUserControl1.TemplateFolderPath;
                template.Database = db;
                template.Settings = settings;
                template.GenerateFolder = CreateFilePath;
                template.OnProcessChanged += new ProcessChanged(template_OnProcessChanged);
                template.Generate();
                if (ShowDebug != null) ShowDebug(template.DatabaseJson, template.SettingJson, false);
            }
            catch (Exception ex)
            {
                MessageBoxMessage.Error(ex.Message);
            }

            progressBar1.Value = 0;
            if (MessageBoxMessage.Confirm("�ɹ�����,�Ƿ��Ŀ¼?"))
            {
                Process.Start(CreateFilePath);
            }
        }

        void template_OnProcessChanged(int process, int maxProcess)
        {
            if (progressBar1.Maximum != maxProcess)
            {
                if (progressBar1.Value > maxProcess)
                    progressBar1.Value = maxProcess;
                progressBar1.Maximum = maxProcess;
            }
            progressBar1.Value = process;
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = txtPath.Text.Trim();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dlg.SelectedPath;
            }
        }

        private void selectTemplateUserControl1_ShowReadmeOfTemplate(string obj)
        {
            if (ShowReadmeOfTemplate != null)
            {
                ShowReadmeOfTemplate(obj);
            }
        }

        public void OnTemplateChanged()
        {
            selectTemplateUserControl1.LoadTemplates();
        }
    }
}