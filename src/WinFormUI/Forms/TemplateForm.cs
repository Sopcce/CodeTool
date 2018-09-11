using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using CodeTool.Common.Generator;
using CodeTool.Config;
using CodeTool.Forms;

namespace CodeTool
{
    public partial class TemplateForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public event Action TemplateChanged;

        TreeNode rootNode;

        public TemplateForm()
        {
            InitializeComponent();

            LoadRoot();
        }

        private void LoadRoot()
        {
            rootNode = new TreeNode("����ģ��");
            string templatePath = Application.StartupPath + "\\templates";
            if (!Directory.Exists(templatePath))
            {
                Directory.CreateDirectory(templatePath);
            }
            DirectoryInfo templateFolder = new DirectoryInfo(templatePath);
            rootNode.Tag = templateFolder;
            rootNode.ContextMenuStrip = cms;
            rootNode.ImageIndex = 0;
            rootNode.SelectedImageIndex = 0;
            tvTemplates.Nodes.Add(rootNode);
            LoadTempates();
            rootNode.Expand();
        }

        private void LoadTempates()
        {
            rootNode.Nodes.Clear();
            DirectoryInfo rootFolder = rootNode.Tag as DirectoryInfo;
            foreach (DirectoryInfo dir in rootFolder.GetDirectories())
            {
                TreeNode node = new TreeNode(dir.Name);
                node.Tag = dir;
                node.ContextMenuStrip = cms;
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                rootNode.Nodes.Add(node);
                LoadFiles(node);
            }
        }

        private void LoadFiles(TreeNode templateNode)
        {
            templateNode.Nodes.Clear();
            DirectoryInfo templateDir = templateNode.Tag as DirectoryInfo;
            foreach (FileInfo fi in templateDir.GetFiles())
            {
                TreeNode node = new TreeNode(fi.Name);
                node.Tag = fi;
                node.ContextMenuStrip = cms;
                if (fi.Extension.ToLower().Contains("js"))
                {
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 2;
                }
                else
                {
                    node.ImageIndex = 3;
                    node.SelectedImageIndex = 3;
                }
                templateNode.Nodes.Add(node);
            }
        }

        private void tvTemplates_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode tn = GetMouseNode(tvTemplates, this);
            if (tn != null)
            {
                tvTemplates.SelectedNode = tn;
            }
        }

        /// <summary>
        /// �õ�TreeView�����ָ��Ľڵ�,ͬʱ�Ѹýڵ�����Ϊ��ǰѡ�еĽڵ�
        /// </summary>
        private TreeNode GetMouseNode(TreeView tv, Control currentForm)
        {
            Point pt = currentForm.PointToScreen(tv.Location);
            Point p = new Point(Control.MousePosition.X - pt.X, Control.MousePosition.Y - pt.Y);
            TreeNode tn = tv.GetNodeAt(p);
            return tn;
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mnuAddJSFile.Visible = false;
            mnuAddXmlFile.Visible = false;
            mnuAddTemplate.Visible = false;
            mnuDelete.Visible = false;
            mnuEdit.Visible = false;
            mnuRefresh.Visible = false;
            mnuOpenFolder.Visible = false;

            switch (tvTemplates.SelectedNode.Level)
            {
                case 0:
                    mnuAddTemplate.Visible = true;
                    mnuRefresh.Visible = true;
                    mnuOpenFolder.Visible = true;
                    break;
                case 1:
                    mnuAddJSFile.Visible = true;
                    mnuAddXmlFile.Visible = true;
                    mnuDelete.Visible = true;
                    mnuRefresh.Visible = true;
                    mnuOpenFolder.Visible = true;
                    break;
                default:
                    mnuDelete.Visible = true;
                    mnuEdit.Visible = true;
                    break;
            }
        }

        private void mnuAddTemplate_Click(object sender, EventArgs e)
        {
            InputForm frm = new InputForm();
            frm.Title = "������ģ������";
            frm.Value = "��ģ��";
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(frm.Value))
                {
                    ShowMessage.Alert("ģ�����Ʋ���Ϊ�ա�");
                    frm.ShowDialog();
                    return;
                }

                DirectoryInfo rootFolder = rootNode.Tag as DirectoryInfo;
                string path = rootFolder.FullName + "\\" + frm.Value;
                if (Directory.Exists(path))
                {
                    ShowMessage.Alert("ģ���Ѵ��ڡ�");
                    frm.ShowDialog();
                    return;
                }

                IoHelper.CopyFolder(Application.StartupPath + "\\Config\\NewTemplate", path);
                LoadTempates();
                if (TemplateChanged != null)
                    TemplateChanged();
            }
        }

        private void mnuAddJSFile_Click(object sender, EventArgs e)
        {
            AddFile("js");
        }

        private void mnuAddXmlFile_Click(object sender, EventArgs e)
        {
            AddFile("xml");
        }

        private void AddFile(string ext)
        {
            InputForm frm = new InputForm();
            frm.Title = string.Format("�������ļ����� (.{0})", ext);
            frm.Value = "test";
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(frm.Value))
                {
                    ShowMessage.Alert("�ļ����Ʋ���Ϊ�ա�");
                    frm.ShowDialog();
                    return;
                }

                DirectoryInfo rootFolder = tvTemplates.SelectedNode.Tag as DirectoryInfo;
                string path = string.Format("{0}\\{1}.{2}", rootFolder.FullName, frm.Value, ext);
                if (File.Exists(path))
                {
                    ShowMessage.Alert("�ļ��Ѵ��ڡ�");
                    frm.ShowDialog();
                    return;
                }

                IoHelper.WriteFile(path, "");
                LoadFiles(tvTemplates.SelectedNode);
            }
        }

        private void mnuEdit_Click(object sender, EventArgs e)
        {
            FileInfo fi = tvTemplates.SelectedNode.Tag as FileInfo;
            MainForm.OpenFile(fi);
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            object obj = tvTemplates.SelectedNode.Tag;
            switch (tvTemplates.SelectedNode.Level)
            {
                case 1:
                    if (ShowMessage.Confirm("ȷ��Ҫɾ����ģ����") == false)
                        return;
                    DirectoryInfo dir = obj as DirectoryInfo;
                    Directory.Delete(dir.FullName, true);
                    LoadTempates();
                    if (TemplateChanged != null)
                        TemplateChanged();
                    break;
                case 2:
                    if (ShowMessage.Confirm("ȷ��Ҫɾ�����ļ���") == false)
                        return;
                    TreeNode parent = tvTemplates.SelectedNode.Parent;
                    FileInfo fi = obj as FileInfo;
                    File.Delete(fi.FullName);
                    LoadFiles(parent);
                    break;
                default:
                    break;
            }
        }

        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            switch (tvTemplates.SelectedNode.Level)
            {
                case 0:
                    LoadTempates();
                    break;
                case 1:
                    LoadFiles(tvTemplates.SelectedNode);
                    break;
                default:
                    break;
            }
        }

        private void mnuOpenFolder_Click(object sender, EventArgs e)
        {
            object tag = tvTemplates.SelectedNode.Tag;
            if (tag is DirectoryInfo)
            {
                DirectoryInfo dir = tag as DirectoryInfo;
                Process.Start(dir.FullName);
            }
        }

        private void tvTemplates_DoubleClick(object sender, EventArgs e)
        {
            switch (tvTemplates.SelectedNode.Level)
            {
                case 2:
                    FileInfo fi = tvTemplates.SelectedNode.Tag as FileInfo;
                    MainForm.OpenFile(fi);
                    break;
                default:
                    break;
            }
        }
    }
}