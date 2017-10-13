﻿using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sop.DatabaseDesignAssistant
{
    public partial class GenerationForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private static string connectionString;
        /// <summary>
        /// 设置连接信息
        /// </summary>
        private static Step step = Step.SetConnection;
        private static IEnumerable<string> tables;
        /// <summary>
        /// 
        /// </summary>
        private static IEnumerable<string> views;
        private static bool changeDataBase = false;
        private static object selectItem;
        private static string logPath = System.IO.Directory.GetCurrentDirectory() + "\\log.txt";

        public GenerationForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnComplete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出本程序吗？", "关闭窗口", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Dispose();
                this.Close();
            }

        }
        /// <summary>
        /// 上一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            switch (step)
            {
                case Step.Execute:
                    BtnPrevious.Visible = true;
                    BtnSchemaNext.Text = "下一步》";
                    BtnSchemaNext.Enabled = true;
                    PlExecMessage.Visible = false;
                    PlSetOption.Visible = true;
                    step = Step.SetOption;
                    break;
                case Step.SetOption:
                    PlSetOption.Visible = false;
                    PlSelectDataItem.Visible = true;
                    step = Step.SelectTables;
                    break;
                case Step.SelectViews:
                    break;
                case Step.SelectTables:
                    PlSelectDataItem.Visible = false;
                    PlSelectDataBase.Visible = true;
                    step = Step.SelectDataBase;
                    break;
                case Step.SelectDataBase:
                    PlSelectDataBase.Visible = false;
                    PanelConect.Visible = true;
                    step = Step.SetConnection;
                    break;
            }
        }
        /// <summary>
        /// 下一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSchemaNext_Click(object sender, EventArgs e)
        {
            switch (step)
            {
                case Step.SetConnection:
                    if (!ValidateConnectionString())
                        return;
                    string errorMessage = string.Empty;
                    if (!CheckConnection(out errorMessage))
                    {
                        MessageBox.Show(errorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    step = Step.SelectDataBase;
                    PanelConect.Visible = false;
                    PlSelectDataBase.Visible = true;
                    BtnPrevious.Visible = true;
                    LbDataBases.Items.Clear();
                    BindDataBase();
                    break;
                case Step.SelectDataBase:
                    if (LbDataBases.SelectedItem == null)
                    {
                        MessageBox.Show("请选择一个数据库！", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    connectionString = connectionString + "Initial Catalog=" + LbDataBases.SelectedItem + ";";
                    PlSelectDataBase.Visible = false;
                    PlSelectDataItem.Visible = true;
                    if (changeDataBase)
                    {
                        ChklSelectDataBaseItems.Items.Clear();
                        changeDataBase = false;
                        BindTables();
                    }
                    step = Step.SelectTables;
                    break;
                case Step.SelectTables:
                    if (ChklSelectDataBaseItems.CheckedItems.Count == 0)
                    {
                        MessageBox.Show("请选择一个数据表！", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    tables = ChklSelectDataBaseItems.CheckedItems.Cast<string>();
                    //views = ChklSelectDataBaseItems.CheckedItems.Cast<string>();
                    if (tables == null)
                        return;
                    PlSelectDataItem.Visible = false;
                    PlSetOption.Visible = true;
                    step = Step.SetOption;
                    string message = string.Empty;
                    break;
                case Step.SelectViews:
                    break;
                case Step.SetOption:
                    step = Step.Execute;
                    PlSetOption.Visible = false;
                    PlExecMessage.Visible = true;
                    TxtExecMessage.Text = "";
                    if (RBtnSelectPath.Checked)
                    {
                        //if (!CreatePath())
                        // return;
                        CreateScript();
                    }
                    else if (RBtnExportMySql.Checked)
                    {
                        if (RbtnMsSqlToMySql.Checked)
                            MsSqlToMySql();
                        else if (RBtnMySqlToMsSql.Checked)
                            MySqlToMsSql();
                    }
                    break;
                case Step.Execute:
                    this.Close();
                    break;
            }
        }

        #region SetConnectionString

        private void RBtnProvideConnectionString_CheckedChanged(object sender, EventArgs e)
        {
            SetConnectOptions(false);
        }

        private void RBtnSpecifiyServer_CheckedChanged(object sender, EventArgs e)
        {
            SetConnectOptions();
        }

        private void RBtnWinAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            this.TxtUserName.Enabled = false;
            this.TxtPassword.Enabled = false;
        }

        private void RBtnSqlAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            this.TxtUserName.Enabled = true;
            this.TxtPassword.Enabled = true;
        }

        #endregion

        #region SelectDataBase

        private void LbDataBases_Click(object sender, EventArgs e)
        {
            if (LbDataBases.SelectedItem != null)
            {
                ChkOperateTable.Enabled = true;
                ChkOperateView.Enabled = true;
            }
            else
            {
                ChkOperateTable.Enabled = false;
                ChkOperateView.Enabled = false;
            }
        }

        #endregion

        #region SelectDataBaseItem

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            SetChecked(true);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            SetChecked(false);
        }

        #endregion

        #region SetOption

        private void BtnSelectPath_Click(object sender, EventArgs e)
        {
            if (FbdSetPath.ShowDialog() == DialogResult.OK)
                TxtPath.Text = FbdSetPath.SelectedPath;
        }

        #endregion

        #region Help Medth

        /// <summary>
        /// 更新连接设置的启用状态
        /// </summary>
        /// <param name="enable"></param>
        private void SetConnectOptions(bool enable = true)
        {
            this.TxtServer.Enabled = enable;
            this.TxtUserName.Enabled = enable;
            this.TxtPassword.Enabled = enable;
            this.RBtnWinAuthentication.Enabled = enable;
            this.RBtnSqlAuthentication.Enabled = enable;
            this.TxtConnectionString.Enabled = !enable;
        }

        #region 验证连接字符串
        /// <summary>
        /// 验证连接字符串
        /// </summary>
        private bool ValidateConnectionString()
        {
            if (TxtServer.Enabled)
            {
                if (RBtnWinAuthentication.Checked)
                {
                    if (string.IsNullOrEmpty(TxtServer.Text))
                    {
                        MessageBox.Show("给定服务器名称无效", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    connectionString = string.Format("Data Source={0};Integrated Security=True;Pooling=true;", TxtServer.Text.Trim());
                    return true;
                }

                if (string.IsNullOrEmpty(TxtUserName.Text) || string.IsNullOrEmpty(TxtPassword.Text))
                {
                    MessageBox.Show("用户名和密码是必需的", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                connectionString = string.Format("Data Source={0};User ID={1};Password={2};Trusted_Connection=no; Pooling=true;", TxtServer.Text.Trim(), TxtUserName.Text.Trim(), TxtPassword.Text.Trim());

            }
            else if (TxtConnectionString.Enabled)
            {
                if (string.IsNullOrEmpty(TxtConnectionString.Text))
                {
                    MessageBox.Show("给定的连接字符串无效", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                connectionString = TxtConnectionString.Text.Trim();
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 绑定数据库
        /// </summary>
        private void BindDataBase()
        {
            List<string> dataBases = GetDataBases(connectionString);
            if (dataBases != null)
            {
                LbDataBases.Items.AddRange(dataBases.ToArray());
            }
        }

        /// <summary>
        /// 绑定数据表
        /// </summary>
        private void BindTables()
        {
            List<string> tables = GetTables();
            if (tables != null)
            {
                tables.Sort();
                ChklSelectDataBaseItems.Items.AddRange(tables.ToArray());
            }
        }

        private void SetChecked(bool isChecked)
        {
            for (int i = 0; i < ChklSelectDataBaseItems.Items.Count; i++)
            {
                ChklSelectDataBaseItems.SetItemChecked(i, isChecked);
            }
        }

        //private bool CreatePath()
        //{
        //    if (!File.Exists(TxtPath.Text))
        //    {
        //        try
        //        {
        //            File.Create(TxtPath.Text);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        #endregion

        #region SqlDataHelper
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CheckConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
            //using (var da = new PetaPoco.Database(connectionString))
            //{
            //    try
            //    {
            //        da.Connection.Open();
            //    }
            //    catch (Exception ex)
            //    {
            //        errorMessage = ex.Message;
            //        return false;
            //    }
            //    //return true;
            //}
            ;
            


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }

                conn.Close();
                return true;
            }
        }

        /// <summary>
        /// 获取所有数据库
        /// </summary>
        public List<string> GetDataBases(string connectionString)
        {

            string selectDataBases = "SELECT Name FROM Master..SysDatabases where dbid > 4 ORDER BY Name;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand comm = new SqlCommand(selectDataBases, conn);
                conn.Open();

                List<string> dataBases = new List<string>();
                using (SqlDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        dataBases.Add(dr["Name"].ToString());
                    }
                }
                conn.Close();
                return dataBases;
            }
        }

        /// <summary>
        /// 获取数据库中所有表
        /// </summary>
        public List<string> GetTables()
        {
            string selectTables = "select name from sysobjects where xtype='u';";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand comm = new SqlCommand(selectTables, conn);

                conn.Open();
                List<string> tables = new List<string>();
                using (SqlDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        tables.Add(dr["name"].ToString());
                    }
                }
                conn.Close();

                return tables;
            }
        }

        /// <summary>
        /// 生成MySql脚本
        /// </summary>
        public void CreateScript()
        {

            if (tables == null)
                return;

            BtnPrevious.Visible = false;
            BtnSchemaNext.Text = "完成";
            BtnSchemaNext.Enabled = false;
            int count = tables.Count();
            //生成sql
            string path = TxtPath.Text + "\\MySQLText.sql";

            StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                Server server = new Server(new ServerConnection(conn));
                Database db = server.Databases[conn.Database];
                Table table = null;
                int j = 1;
                foreach (var tableName in tables)
                {
                    if (db.Tables.Contains(tableName))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("DROP TABLE IF EXISTS `{0}`;\nCREATE TABLE `{0}` (\n", tableName);

                        table = db.Tables[tableName];
                        long increment = 0;
                        foreach (Column column in table.Columns)
                        {
                            sb.Append(column.Name + " " + ConvertType(column.DataType));

                            sb.Append(column.Nullable && column.DefaultConstraint == null ? " DEFAULT NULL" : " NOT NULL");
                            if (column.DataType.SqlDataType != SqlDataType.Text && column.DataType.SqlDataType != SqlDataType.NText && column.DataType.SqlDataType != SqlDataType.DateTime && column.DataType.SqlDataType != SqlDataType.Bit && column.DataType.SqlDataType != SqlDataType.UniqueIdentifier)
                                sb.Append(column.DefaultConstraint == null ? string.Empty : " DEFAULT " + column.DefaultConstraint.Text.Replace('(', ' ').Replace(')', ' ').Trim());
                            sb.Append(column.Identity ? " AUTO_INCREMENT" : string.Empty);
                            sb.Append(",\n");
                            if (column.Identity && column.IdentityIncrement > 1)
                            {
                                increment = column.IdentityIncrement;
                            }
                        }

                        if (table.Indexes != null)
                        {
                            foreach (Index index in table.Indexes)
                            {
                                string iXName = string.Empty;
                                string strColumnNames = string.Empty;
                                for (int i = 0; i < index.IndexedColumns.Count; i++)
                                {
                                    if (table.Columns[index.IndexedColumns[i].Name].DataType.MaximumLength < 332)
                                    {
                                        strColumnNames += "`" + index.IndexedColumns[i].Name + "`,";
                                        //生成索引名后缀
                                        if (index.Name.StartsWith("IX"))
                                            iXName += index.IndexedColumns[i].Name + "_";
                                    }
                                }

                                strColumnNames = strColumnNames.TrimEnd(',');

                                if (string.IsNullOrEmpty(strColumnNames))
                                    continue;

                                if (index.Name.StartsWith("PK"))
                                {
                                    //处理主键
                                    sb.AppendFormat("PRIMARY KEY ({0})", strColumnNames);
                                }
                                else if (index.Name.StartsWith("IX"))
                                {
                                    //处理索引
                                    sb.AppendFormat("KEY `IX_{1}` ({2})", tableName, iXName.TrimEnd('_'), strColumnNames);
                                }
                                sb.Append(",\n");
                            }
                        }
                        sb.Remove(sb.Length - 2, 2);
                        sb.AppendFormat("\n)ENGINE=innodb{0};\r", increment > 1 ? " AUTO_INCREMENT=" + increment.ToString() : string.Empty);
                        writer.WriteLine(sb.ToString());

                        TxtExecMessage.SelectedText += tableName + "\t ok\r\n";
                        j++;
                        increment = 0;
                    }
                }

                TxtExecMessage.SelectedText = "操作完成。";
                writer.Close();
                conn.Close();

                BtnPrevious.Visible = true;
                BtnSchemaNext.Enabled = true;
            }
        }

        /// <summary>
        /// 转换类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string ConvertType(DataType type)
        {
            switch (type.SqlDataType)
            {
                case SqlDataType.Decimal:
                    return "Decimal(10,2)";
                case SqlDataType.Char:
                    return string.Format("Char({0})", type.MaximumLength);
                case SqlDataType.NChar:
                case SqlDataType.VarChar:
                case SqlDataType.NVarChar:
                    return string.Format("Varchar({0})", type.MaximumLength);
                case SqlDataType.NVarCharMax:
                case SqlDataType.NText:
                    return "mediumtext";
                case SqlDataType.Int:
                    return "Int(11)";
                case SqlDataType.Bit:
                    return "tinyint(1)";
                case SqlDataType.Float:
                    return "Float(7,2)";
                case SqlDataType.UniqueIdentifier:
                    return "char(36)";
                default:
                    return type.SqlDataType.ToString();
            }
        }

        private void MsSqlToMySql()
        {
            if (tables == null)
                return;

            BtnPrevious.Visible = false;
            BtnSchemaNext.Text = "完成";
            BtnSchemaNext.Enabled = false;

            bool error = false;

            if (!File.Exists(logPath))
            {
                File.Create(logPath).Close();
            }

            StreamWriter logWriter = new StreamWriter(logPath, false, Encoding.UTF8);

            //创建一个MySqlConnection对象
            using (MySqlConnection conn = new MySqlConnection(TxtMySqlConnectionString.Text))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                DataTable dt = null;
                conn.Open();
                string dd = string.Empty;
                foreach (var tableName in tables)
                {
                    try
                    {
                        dt = GetDataTable(tableName);
                        if (dt == null || dt.Rows.Count == 0)
                            continue;

                        cmd.CommandText = GenerateMySqlScript(dt, tableName);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        error = true;
                        logWriter.WriteLine(DateTime.Now + " error:" + e.Message + "\r\n");
                    }

                    TxtExecMessage.SelectedText = tableName + "\t ok\r\n";
                }
                conn.Close();
            }

            TxtExecMessage.SelectedText = error ? "操作完成但有异常产生，请到程序根目录的log.txt文件中查看异常" : "操作完成。";

            BtnPrevious.Visible = true;
            BtnSchemaNext.Enabled = true;
            logWriter.Close();
        }

        /// <summary>
        /// 将MySql中的数据导入MsSql
        /// </summary>
        private void MySqlToMsSql()
        {

            if (tables == null)
                return;

            BtnPrevious.Visible = false;
            BtnSchemaNext.Text = "完成";
            BtnSchemaNext.Enabled = false;

            bool error = false;

            if (!File.Exists(logPath))
            {
                File.Create(logPath).Close();
            }

            StreamWriter logWriter = new StreamWriter(logPath, false, Encoding.UTF8);


            //创建一个MySqlConnection对象
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                DataTable dt = null;

                conn.Open();

                Server server = new Server(new ServerConnection(conn));
                Database db = server.Databases[conn.Database];

                foreach (var tableName in tables)
                {
                    try
                    {
                        dt = GetDataTable(tableName);
                        if (dt == null || dt.Rows.Count == 0)
                            continue;


                        cmd.CommandText = GenerateMsSqlScript(dt, tableName, db.Tables[tableName]);

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        error = true;
                        logWriter.WriteLine(DateTime.Now + " error:" + e.Message + "\r\n");
                    }


                    TxtExecMessage.SelectedText = tableName + "\t ok\r\n";
                }
                conn.Close();
            }

            TxtExecMessage.SelectedText = error ? "操作完成但有异常产生，请到程序根目录的log.txt文件中查看异常" : "操作完成。";
            BtnPrevious.Visible = true;
            BtnSchemaNext.Enabled = true;
            logWriter.Close();
        }

        /// <summary>
        /// 组装Insert语句
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GenerateMySqlScript(DataTable dt, string tableName)
        {
            string columnNames = string.Empty;
            StringBuilder sbColumns = new StringBuilder();
            foreach (DataColumn column in dt.Columns)
            {
                sbColumns.Append("`" + column.ColumnName + "`,");
            }

            sbColumns.Remove(sbColumns.Length - 1, 1);

            string insertModel = "INSERT INTO `" + tableName + "`(" + sbColumns.ToString() + ") VALUES ({0});\n";

            StringBuilder sbInsert = new StringBuilder();
            sbInsert.Append("DELETE FROM `" + tableName + "`;");
            StringBuilder columnValue = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    if (item.GetType().Equals(typeof(bool)))
                    {
                        columnValue.Append("'" + ((bool)item ? 1 : 0) + "',");
                    }
                    else
                    {
                        columnValue.Append("'" + item + "',");
                    }
                }
                sbInsert.AppendFormat(insertModel, columnValue.ToString().TrimEnd(','));
                columnValue.Clear();
            }
            return sbInsert.ToString();
        }

        /// <summary>
        /// 组装Insert语句
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GenerateMsSqlScript(DataTable dt, string tableName, Table table)
        {
            string columnNames = string.Empty;

            StringBuilder sbInsert = new StringBuilder();
            sbInsert.Append("DELETE FROM " + tableName + ";");

            StringBuilder sbColumns = new StringBuilder();
            bool isIdentity = false;

            foreach (DataColumn column in dt.Columns)
            {
                if (table.Columns[column.ColumnName].Identity && !isIdentity)
                {
                    sbInsert.Append("SET IDENTITY_INSERT " + tableName + " ON;\n\r");
                    isIdentity = true;
                }
                sbColumns.Append(column.ColumnName + ",");
            }

            sbColumns.Remove(sbColumns.Length - 1, 1);
            string insertModel = "INSERT INTO " + tableName + " (" + sbColumns.ToString() + ") VALUES ({0});\n";

            StringBuilder columnValue = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    columnValue.Append("'" + item + "',");
                }
                sbInsert.AppendFormat(insertModel, columnValue.ToString().TrimEnd(','));
                columnValue.Clear();
            }
            if (isIdentity)
                sbInsert.Append("SET IDENTITY_INSERT " + tableName + " OFF;\n\r");

            return sbInsert.ToString();
        }

        /// <summary>
        /// 从MsSql中获取数据表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string tableName)
        {
            string strSQL = "SELECT * FROM " + tableName;

            DataSet ds = new DataSet();
            if (RbtnMsSqlToMySql.Checked)
            {
                SqlDataAdapter da = new SqlDataAdapter(strSQL, connectionString);
                da.Fill(ds, "TempTable");
                return ds.Tables["TempTable"];
            }
            else
            {
                MySqlDataAdapter da = new MySqlDataAdapter(strSQL, TxtMySqlConnectionString.Text.Trim());
                da.Fill(ds, "TempTable");
                return ds.Tables["TempTable"];
            }
        }

        #endregion

        private void RBtnSelectPath_CheckedChanged(object sender, EventArgs e)
        {
            TxtPath.Enabled = true;
            BtnSelectPath.Enabled = true;
            TxtMySqlConnectionString.Enabled = false;
            RBtnMySqlToMsSql.Enabled = false;
            RbtnMsSqlToMySql.Enabled = false;
        }

        private void RBtnExportMySql_CheckedChanged(object sender, EventArgs e)
        {
            TxtPath.Enabled = true;
            BtnSelectPath.Enabled = false;
            TxtMySqlConnectionString.Enabled = true;
            RBtnMySqlToMsSql.Enabled = true;
            RbtnMsSqlToMySql.Enabled = true;
        }

        private void RBtnMsSql_CheckedChanged(object sender, EventArgs e)
        {
            TxtServer.Text = ".";
            TxtUserName.Text = "sa";
            RBtnWinAuthentication.Enabled = true;
        }

        private void RBtnMySql_CheckedChanged(object sender, EventArgs e)
        {
            RBtnWinAuthentication.Enabled = false;
            RBtnSqlAuthentication.Checked = true;
            TxtServer.Text = "127.0.0.1";
            TxtUserName.Text = "root";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbDataBases_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!LbDataBases.SelectedItem.Equals(selectItem))
            {
                selectItem = LbDataBases.SelectedItem;
                changeDataBase = true;
            }
        }


    }


}
