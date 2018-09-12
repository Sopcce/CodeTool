using System;
using System.Drawing;
using System.Windows.Forms;
using CodeTool.Common.Model;
using CodeTool.Config;

//using CodeTool.Forms;

namespace CodeTool.Forms
{
    public partial class DatabaseForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public event Action<Database> OutputCode;
        public event Action<Table> CreateCode;
        public event Action<string> ShowStatus;

        public DatabaseForm()
        {
            InitializeComponent();
        }

        #region ����Ƴ����ݿ�
        private void btnAddDatabase_Click(object sender, EventArgs e)
        {
            ConnectionForm frm = new ConnectionForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Database database = frm.database;
                ShowDatabase(database);
            }
        }

        private void btnRemoveDatabase_Click(object sender, EventArgs e)
        {
            TreeNode node = tvDatabase.SelectedNode;
            if (node == null) { return; }
            while (node.Level > 0)
            {
                node = node.Parent;
            }
            tvDatabase.Nodes.Remove(node);
        }
        #endregion

        #region ���ؽڵ�
        /// <summary>
        /// ��ʾ��һ���ڵ㣺���ݿ�
        /// </summary>
        private void ShowDatabase(Database database)
        {
            TreeNode nodeDB = new TreeNode(
                string.Format("{0}��{1}",
                    database.Name,
                    database.TypeDescn),
                0, 0);
            nodeDB.Tag = database;
            nodeDB.ContextMenuStrip = cmsDB;
            this.tvDatabase.Nodes.Add(nodeDB);

            ShowFolders(database, nodeDB);
            nodeDB.Expand();
        }

        /// <summary>
        /// ��ʾ�ڶ����ڵ㣺����ͼ���洢�����ļ���
        /// </summary>
        private void ShowFolders(Database db, TreeNode nodeRoot)
        {
            //��ӡ����ļ���
            TreeNode nodeTableFolder = new TreeNode("��", 1, 1);
            nodeRoot.Nodes.Add(nodeTableFolder);

            //��ӱ�
            ShowTables(db, nodeTableFolder);
            nodeTableFolder.Expand();

            //��ӡ���ͼ���ļ���
            TreeNode nodeViewFolder = new TreeNode("��ͼ", 1, 1);
            nodeRoot.Nodes.Add(nodeViewFolder);

            //�����ͼ
            ShowViews(db, nodeViewFolder);

            //��ӡ��洢���̡��ļ���
            TreeNode nodeStoreProceduresFolder = new TreeNode("�洢����", 1, 1);
            nodeRoot.Nodes.Add(nodeStoreProceduresFolder);

            //��Ӵ洢����
            ShowStoreProcedures(db, nodeStoreProceduresFolder);
        }

        /// <summary>
        /// ��ʾ�������ڵ㣺��
        /// </summary>
        private void ShowTables(Database database, TreeNode nodeRoot)
        {
            foreach (Table table in database.Tables)
            {
                TreeNode nodeTable = new TreeNode(table.Name, 2, 2);
                nodeTable.Tag = table;
                nodeTable.ContextMenuStrip = cmsTable;
                nodeRoot.Nodes.Add(nodeTable);

                ShowFields(table, nodeTable);
            }
        }

        /// <summary>
        /// ��ʾ�������ڵ㣺��ͼ
        /// </summary>
        private void ShowViews(Database database, TreeNode nodeRoot)
        {
            foreach (Table table in database.Views)
            {
                TreeNode nodeTable = new TreeNode(table.Name, 2, 2);
                nodeTable.Tag = table;
                nodeTable.ContextMenuStrip = cmsView;
                nodeRoot.Nodes.Add(nodeTable);

                ShowFields(table, nodeTable);
            }
        }

        /// <summary>
        /// ��ʾ�������ڵ㣺�洢����
        /// </summary>
        private static void ShowStoreProcedures(Database db, TreeNode nodeRoot)
        {
            foreach (string storeProcedure in db.StoreProcedures)
            {
                nodeRoot.Nodes.Add(new TreeNode(storeProcedure, 5, 5));
            }
        }

        /// <summary>
        /// ��ʾ���ļ��ڵ㣺�ֶ�
        /// </summary>
        private void ShowFields(Table table, TreeNode nodeRoot)
        {
            foreach (Field field in table.Fields)
            {
                string text = string.Format("{0}:{1}{2}{3}", field.Name, field.FieldType,
                    field.IsId ? "[Id]" : "", field.IsKey ? "[key]" : "");
                TreeNode nodeField = new TreeNode(text, 3, 3);
                nodeRoot.Nodes.Add(nodeField);

                //TreeNode nodeField = new TreeNode(field.Name, 3, 3);
                //nodeRoot.Nodes.Add(nodeField);
            }
        }
        #endregion

        #region �Ҽ�
        private void menuSelect_Click(object sender, EventArgs e)
        {
            if (CreateCode != null)
            {
                string dbName = tvDatabase.SelectedNode.Parent.Parent.Text;
                Database db = tvDatabase.SelectedNode.Parent.Parent.Tag as Database;
                Table table = tvDatabase.SelectedNode.Tag as Table;
                if (table != null)
                {
                    SqlSeleteViewForm ssv = new SqlSeleteViewForm(db, table);
                    if ((ssv.sqlTextEditor.Text.Trim().Length != 0) && (ssv.sqlTextEditor.Text[ssv.sqlTextEditor.Text.Length - 1] != '\n'))
                    {
                        ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + "\n";
                    }
                    ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + "select * from " + this.tvDatabase.SelectedNode.Text + "";
                    ssv.Show(MainForm.DockPanel);
                }
                else
                {
                    ShowMessage.Alert("����ѡ��һ�������ͼ��");
                }
            }
        }

        private void menuUpdate_Click(object sender, EventArgs e)
        {
            if (CreateCode != null)
            {
                string dbName = tvDatabase.SelectedNode.Parent.Parent.Text;
                Database db = tvDatabase.SelectedNode.Parent.Parent.Tag as Database;
                Table table = tvDatabase.SelectedNode.Tag as Table;
                if (table != null)
                {
                    SqlSeleteViewForm ssv = new SqlSeleteViewForm(db, table);
                    string str = "\nwhere 1 = 1";
                    string str2 = "update ";
                    str2 = ((str2 + "" + this.tvDatabase.SelectedNode.Text + "") + " \nset ") + this.tvDatabase.SelectedNode.Nodes[0].Text + " = ''";
                    for (int i = 1; i < this.tvDatabase.SelectedNode.Nodes.Count; i++)
                    {
                        str2 = (str2 + ",") + this.tvDatabase.SelectedNode.Nodes[i].Text + "=''";
                    }
                    if ((ssv.sqlTextEditor.Text.Trim().Length != 0) && (ssv.sqlTextEditor.Text[ssv.sqlTextEditor.Text.Length - 1] != '\n'))
                    {
                        ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + "\n";
                    }
                    ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + str2;
                    ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + str;
                    ssv.Show(MainForm.DockPanel);
                }
                else
                {
                    ShowMessage.Alert("����ѡ��һ�������ͼ��");
                }
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            if (CreateCode != null)
            {
                string dbName = tvDatabase.SelectedNode.Parent.Parent.Text;
                Database db = tvDatabase.SelectedNode.Parent.Parent.Tag as Database;
                Table table = tvDatabase.SelectedNode.Tag as Table;
                if (table != null)
                {
                    SqlSeleteViewForm ssv = new SqlSeleteViewForm(db, table);
                    string str = "\nwhere 1 = 1";
                    string str2 = "delete from ";
                    str2 = (str2 + "" + this.tvDatabase.SelectedNode.Text + "") + str;
                    if ((ssv.sqlTextEditor.Text.Trim().Length != 0) && (ssv.sqlTextEditor.Text[ssv.sqlTextEditor.Text.Length - 1] != '\n'))
                    {
                        ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + "\n";
                    }
                    ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + str2;
                    ssv.Show(MainForm.DockPanel);
                }
                else
                {
                    ShowMessage.Alert("����ѡ��һ�������ͼ��");
                }
            }
        }

        private void menuInsert_Click(object sender, EventArgs e)
        {
            if (CreateCode != null)
            {
                string dbName = tvDatabase.SelectedNode.Parent.Parent.Text;
                Database db = tvDatabase.SelectedNode.Parent.Parent.Tag as Database;
                Table table = tvDatabase.SelectedNode.Tag as Table;
                if (table != null)
                {
                    SqlSeleteViewForm ssv = new SqlSeleteViewForm(db, table);
                    string str = "";
                    string str2 = "insert into ";
                    str2 = (str2 + "" + this.tvDatabase.SelectedNode.Text + "") + " (" + this.tvDatabase.SelectedNode.Nodes[0].Text;
                    for (int i = 1; i < this.tvDatabase.SelectedNode.Nodes.Count; i++)
                    {
                        str = str + ",''";
                        str2 = str2 + "," + this.tvDatabase.SelectedNode.Nodes[i].Text;
                    }
                    str2 = (str2 + ") \n" + "values(''") + str + ")";
                    if ((ssv.sqlTextEditor.Text.Trim().Length != 0) && (ssv.sqlTextEditor.Text[ssv.sqlTextEditor.Text.Length - 1] != '\n'))
                    {
                        ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + "\n";
                    }
                    ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + str2;
                    ssv.Show(MainForm.DockPanel);
                }
                else
                {
                    ShowMessage.Alert("����ѡ��һ�������ͼ��");
                }
            }
        }

        private void menuOutput_Click(object sender, EventArgs e)
        {
            OutputSelectedDatabaseCode();
        }

        private void menuCreateCode_Click(object sender, EventArgs e)
        {
            CreateCurrentTableCode();
        }

        private void menuDeleteDatabase_Click(object sender, EventArgs e)
        {
            tvDatabase.Nodes.Remove(tvDatabase.SelectedNode);
        }
        #endregion

        /// <summary>
        /// ��ѡ�еĿ��������������
        /// </summary>
        public void OutputSelectedDatabaseCode()
        {
            if (OutputCode != null)
            {
                Database database = tvDatabase.SelectedNode.Tag as Database;
                if (database != null)
                {
                    OutputCode(database);
                }
                else
                {
                    ShowMessage.Alert("����ѡ��һ�����ݿ⡣");
                }
            }
        }

        /// <summary>
        /// ��ѡ�еı�������ɴ������
        /// </summary>
        public void CreateCurrentTableCode()
        {
            if (CreateCode != null)
            {
                Table table = tvDatabase.SelectedNode.Tag as Table;
                if (table != null)
                {
                    CreateCode(table);
                }
                else
                {
                    ShowMessage.Alert("����ѡ��һ�������ͼ��");
                }
            }
        }

        private void tvDatabase_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode tn = GetMouseNode(tvDatabase, this);
            if (tn != null)
            {
                tvDatabase.SelectedNode = tn;
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

        protected virtual void OnShowStatus(string obj)
        {
            ShowStatus?.Invoke(obj);
        }
    }
}