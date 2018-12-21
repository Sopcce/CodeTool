using System;
using System.Drawing;
using System.Windows.Forms;
using CodeTool.Common.Model;
using CodeTool.Config;
using CodeTool.Forms.Open;
using WeifenLuo.WinFormsUI.Docking;

//using CodeTool.Forms;

namespace CodeTool.Forms
{
    public partial class DatabaseForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public event Action<Database> OutputCode;

        public event Action<Table> CreateCode;

        public event Action<string> ShowStatus;

        public event Action<Database, Table> DataInfo;


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

        #region  V&P&T ���ؽڵ�
        /// <summary>
        /// ��ʾ��һ���ڵ㣺���ݿ�
        /// </summary>
        private void ShowDatabase(Database database)
        {
            //TODO: ICO
            TreeNode nodeDB = new TreeNode(
                string.Format("{0}��{1}",
                    database.Name,
                    database.TypeDescn),
                0, 10);
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
            TreeNode nodeTableFolder = new TreeNode("��", 7, 17);
            nodeRoot.Nodes.Add(nodeTableFolder);


            //��ӱ�
            ShowTables(db, nodeTableFolder);
            nodeTableFolder.Expand();

            //��ӡ���ͼ���ļ���
            TreeNode nodeViewFolder = new TreeNode("��ͼ", 8, 18);
            nodeRoot.Nodes.Add(nodeViewFolder);

            //�����ͼ
            ShowViews(db, nodeViewFolder);

            //��ӡ��洢���̡��ļ���
            TreeNode nodeStoreProceduresFolder = new TreeNode("�洢����", 4, 14);
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
                TreeNode nodeTable = new TreeNode(table.Name, 6, 16);
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
                TreeNode nodeTable = new TreeNode(table.Name, 8, 18);
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
                nodeRoot.Nodes.Add(new TreeNode(storeProcedure, 4, 14));
            }
        }

        /// <summary>
        /// ��ʾ���ļ��ڵ㣺�ֶ�
        /// </summary>
        private void ShowFields(Table table, TreeNode nodeRoot)
        {
            foreach (Field field in table.Fields)
            {
                string text = string.Format("{0} ({1}{2}{3},{4})",
                    field.FieldName,
                    field.IsKey ? "PK " : "",
                    field.FieldType,
                    field.FieldLength == 0 ? "" : $"({ field.FieldLength})",
                    field.AllowNull ? "null" : "not null"
                    );

                TreeNode nodeField = new TreeNode(text, 2, 12);

                nodeField.ForeColor = field.IsKey ? Color.Red : Color.Black;


                nodeRoot.Nodes.Add(nodeField);





            }
        }
        #endregion

        #region  CURG�Ҽ�
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
                    ssv.AddSqlTextEditor();
                    ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + "select * from " + table.Name + "";
                    ssv.Show(MainForm.DockPanel);
                }
                else
                {
                    MessageBoxMessage.Alert("����ѡ��һ�������ͼ��");
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
                    ssv.AddSqlTextEditor();
                    string sql = $"update {table.Name} set ";
                    foreach (Field field in table.Fields)
                    {
                        if (field.IsKey)
                            continue;
                        sql += field.FieldName + "='', ";
                    }
                    sql = sql.TrimEnd(',');
                    sql += " where ";
                    ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + sql;
                    ssv.Show(MainForm.DockPanel);
                }
                else
                {
                    MessageBoxMessage.Alert("����ѡ��һ�������ͼ��");
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
                    ssv.AddSqlTextEditor();
                    ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + @"delete from " + table.Name + "";
                    ssv.Show(MainForm.DockPanel);
                }
                else
                {
                    MessageBoxMessage.Alert("����ѡ��һ�������ͼ��");
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

                    string tempSql = string.Empty;
                    string tempSqlValue = string.Empty;
                    foreach (Field field in table.Fields)
                    {
                        if (field.IsKey)
                            continue;
                        tempSql += field.FieldName + ",";
                        tempSqlValue += "'',";

                    }
                    tempSql = tempSql.TrimEnd(',');
                    tempSqlValue = tempSqlValue.TrimEnd(',');
                    string sql = $"  INSERT INTO {table.Name} ({tempSql}) VALUES({tempSqlValue}) ";

                    ssv.AddSqlTextEditor();
                    ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + sql;
                    ssv.Show(MainForm.DockPanel);
                }
                else
                {
                    MessageBoxMessage.Alert("����ѡ��һ�������ͼ��");
                }
            }
        }
        /// <summary>
        /// ���ɴ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCreateCode_Click(object sender, EventArgs e)
        {
            CreateCurrentTableCode();
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_CreateDataInfo_Click(object sender, EventArgs e)
        {
            if (CreateCode == null)
            {
                MessageBoxMessage.Alert("����ѡ��һ�������ͼ��");
                return;
            }
            // ����������Ϣ����
            string dbName = tvDatabase.SelectedNode.Parent.Parent.Text;
            Database db = tvDatabase.SelectedNode.Parent.Parent.Tag as Database;
            Table table = tvDatabase.SelectedNode.Tag as Table;
            if (db != null)
            {
                DataInfo(db, table);
            }
            else
            {
                MessageBoxMessage.Alert("����ѡ��һ�����ݿ⡣");
            }

        }
        #endregion

        private void menuOutput_Click(object sender, EventArgs e)
        {
            OutputSelectedDatabaseCode();
        }



        private void menuDeleteDatabase_Click(object sender, EventArgs e)
        {
            tvDatabase.Nodes.Remove(tvDatabase.SelectedNode);
        }




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
                    MessageBoxMessage.Alert("����ѡ��һ�����ݿ⡣");
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
                Table table = tvDatabase?.SelectedNode?.Tag as Table;
                if (table != null)
                {
                    CreateCode(table);
                }
                else
                {
                    MessageBoxMessage.Alert("����ѡ��һ�������ͼ��");
                }
            }
        }

        private void tvDatabase_MouseDown(object sender, MouseEventArgs e)
        {
            //�õ�TreeView�����ָ��Ľڵ�,ͬʱ�Ѹýڵ�����Ϊ��ǰѡ�еĽڵ�
            Point pt = this.PointToScreen(tvDatabase.Location);
            Point p = new Point(Control.MousePosition.X - pt.X, Control.MousePosition.Y - pt.Y);
            TreeNode tn = tvDatabase.GetNodeAt(p);
            if (tn != null)
            {
                tvDatabase.SelectedNode = tn;
            }
            //����ϸ��Ϣ�б� 

            //Database db = tvDatabase.SelectedNode.Tag as Database;
            //Table table = tvDatabase.SelectedNode.Tag as Table;
            //DataInfoForm dataInfoForm = new DataInfoForm(db, table);
            //dataInfoForm.Show(MainForm.DockPanel);

            //if (new MainForm().FindDocument("DataInfoForm") != null)
            //{
            //    Form f = new MainForm().FindDocument("DataInfoForm") as Form;
            //    f.Close();

            //}

        }



        protected virtual void OnShowStatus(string obj)
        {
            ShowStatus?.Invoke(obj);
        }

        private void menuRunSql_Click(object sender, EventArgs e)
        {

        }

        private void menuBrowseDB_Click(object sender, EventArgs e)
        {
            if (CreateCode != null)
            {

                Database db = tvDatabase.SelectedNode.Tag as Database;
                Table table = tvDatabase.SelectedNode.Tag as Table;
                if (db != null)
                {
                    //SqlSeleteViewForm ssv = new SqlSeleteViewForm(db, table);
                    //ssv.addSqlTextEditor();
                    //ssv.sqlTextEditor.Text = ssv.sqlTextEditor.Text + "select * from " + table.Name + "";
                    //ssv.Show(MainForm.DockPanel);
                }
                else
                {
                    MessageBoxMessage.Alert("����ѡ��һ�����ݿ⡣");
                }
            }
        }

        private void �½���ѯToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tvDatabase_Click(object sender, EventArgs e)
        {

        }


    }
}