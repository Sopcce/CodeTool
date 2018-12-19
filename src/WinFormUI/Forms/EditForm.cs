using System.Windows.Forms;
using System.IO;
using System.Text;
using CodeTool.Common.Generator;

namespace CodeTool
{
    public partial class EditForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        SaveFileDialog dlg = new SaveFileDialog();
        FileInfo fileInfo;

        public EditForm()
        {
            InitializeComponent();
            dlg = new SaveFileDialog();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="caption">����</param>
        /// <param name="text">����</param>
        /// <param name="lang">����:"ASP3/XHTML","BAT","Boo","Coco","C++.NET","C#","HTML",
        /// "Java","JavaScript","PHP","TeX","VBNET","XML","TSQL"</param>
        public EditForm(FileInfo fi)
            : this()
        {
            if (fi.Exists)
            {
                string text = File.ReadAllText(fi.FullName, Encoding.UTF8);
                txtCode.Text = text;
            }
            this.FileInfo = fi;
        }

        public FileInfo FileInfo
        {
            get { return fileInfo; }
            set
            {
                fileInfo = value;
                this.TabText = value.Name;
                labPath.Text = value.FullName;
                txtCode.SetStyleByExt(value.Extension);
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            if (File.Exists(FileInfo.FullName))
            {
                Helper.WriteFile(FileInfo.FullName, txtCode.Text);
                FileInfo = FileInfo;
                return true;
            }
            else
            {
                return SaveAs();
            }
        }

        private void btnSaveAs_Click(object sender, System.EventArgs e)
        {
            SaveAs();
        }

        private bool SaveAs()
        {
            dlg.FileName = FileInfo.Name;
            dlg.DefaultExt = FileInfo.Extension;
            dlg.Filter = string.Format("{0}|*.{0}|�����ļ�(*.*)|*.*", dlg.DefaultExt);
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Helper.WriteFile(dlg.FileName, txtCode.Text);
                FileInfo = new FileInfo(dlg.FileName);
                return true;
            }

            return false;
        }

        private void txtCode_TextChanged(object sender, System.EventArgs e)
        {
            if (FileInfo != null)
                TabText = FileInfo.Name + " *";
        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TabText != FileInfo.Name)
            {
                DialogResult dr = MessageBox.Show("�ļ��Ѹ��ģ��Ƿ񱣴棿", "��ʾ", MessageBoxButtons.YesNoCancel);
                if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (dr == System.Windows.Forms.DialogResult.No)
                    return;

                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    if (Save() == false)
                        e.Cancel = true;
                }
            }
        }

        private void EditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
                Save();
        }
    }
}