using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel;
//using Oracle.DataAccess.Client;

namespace CodeTool.Common.Model
{
    /// <summary>
    /// ���ݿ�����
    /// </summary>
    public enum DatabaseTypes
    {
        [Description("Access")]
        Access,
        [Description("SqlServer")]
        Sql2000,
        [Description("SqlServer")]
        Sql2005,
        [Description("MySql")]
        MySql,
        [Description("Oracle")]
        Oracle,
        [Description("SQLite")]
        SQLite,
        [Description("Ase")]
        Ase,
        [Description("DB2")]
        DB2,
        [Description("pgsql")]
        PostgreSql
 
    }

    /// <summary>
    /// ���ݿ�
    /// </summary>
    public class Database
    {
        [Newtonsoft.Json.JsonIgnore]
        public string TypeDescn
        {
            get
            {
                switch (Type)
                {
                    case DatabaseTypes.Sql2005:
                        return "Sql2005����߰汾";
                    default:
                        return Type.ToString();
                }
            }
        }

        /// <summary>
        /// ����һ�������ݿ�
        /// </summary>
        public Database()
        {
            Tables = new List<Table>();
            Views = new List<Table>();
            StoreProcedures = new List<string>();
            Selects = new List<Table>();
        }

        #region ����
        /// <summary>
        /// ���ݿ����Ӳ���
        /// </summary>
        public string ConnString { get; set; }

        private string name = null;

        /// <summary>
        /// ���ݿ���
        /// </summary>
        public string Name
        {
            get
            {
                if (name != null)
                {
                    return name;
                }
                else
                {
                    switch (Type)
                    {
                        case DatabaseTypes.Access:
                            using (OleDbConnection conn = new OleDbConnection(ConnString))
                            {
                                try
                                {
                                    FileInfo file = new FileInfo(conn.DataSource);
                                    return file.Name.Remove(file.Name.LastIndexOf("."));
                                }
                                catch (Exception)
                                {
                                    return conn.DataSource;
                                }
                            }
                        case DatabaseTypes.Sql2000:
                        case DatabaseTypes.Sql2005:
                            using (SqlConnection conn = new SqlConnection(ConnString))
                            {
                                try
                                {
                                    FileInfo file = new FileInfo(conn.Database);
                                    int start = file.Name.LastIndexOf(".");
                                    if (start > 0)
                                        return file.Name.Remove(start);

                                    return conn.Database;
                                }
                                catch (Exception)
                                {
                                    return conn.Database;
                                }
                            }
                        case DatabaseTypes.MySql:
                            using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(ConnString))
                            {
                                try
                                {
                                    return conn.Database;
                                }
                                catch (Exception)
                                {
                                    return "MySqlDB";
                                }
                            }
                        case DatabaseTypes.Oracle:
                            using (OracleConnection conn = new OracleConnection(ConnString))
                            {
                                try
                                {
                                    return conn.DataSource;
                                }
                                catch (Exception)
                                {
                                    return "OracleDB";
                                }
                            }
                        case DatabaseTypes.SQLite:
                            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection(ConnString))
                            {
                                try
                                {
                                    Regex r = new Regex("Data Source=(?<source>[^;]+);");
                                    Match m = r.Match(conn.ConnectionString);
                                    if (m.Success)
                                    {
                                        string path = m.Groups["source"].Value;
                                        FileInfo fi = new FileInfo(path);
                                        return fi.Name.Substring(0, fi.Name.LastIndexOf('.'));
                                    }
                                    else
                                    {
                                        return "SQLiteDB";
                                    }
                                }
                                catch (Exception)
                                {
                                    return "SQLiteDB";
                                }
                            }
                        case DatabaseTypes.Ase:
                            using (Sybase.Data.AseClient.AseConnection conn = new Sybase.Data.AseClient.AseConnection(ConnString))
                            {
                                try
                                {
                                    return conn.Database;
                                }
                                catch (Exception)
                                {
                                    return "SybaseDB";
                                }
                            }
                        case DatabaseTypes.DB2:
                            using (IBM.Data.DB2.DB2Connection conn = new IBM.Data.DB2.DB2Connection(ConnString))
                            {
                                try
                                {
                                    Regex r = new Regex("Database=(?<Database>[^;]+);");
                                    Match m = r.Match(conn.ConnectionString);
                                    if (m.Success)
                                    {
                                        string result = m.Groups["Database"].Value;
                                        return result;

                                    }
                                    else
                                    {
                                        return "DB2DB";
                                    }
                                }
                                catch (Exception)
                                {
                                    return "DB2DB";
                                }
                            }
                        case DatabaseTypes.PostgreSql:
                            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(ConnString))
                            {
                                try
                                {
                                    return conn.Database;
                                }
                                catch (Exception)
                                {
                                    return "PostgreSqlDB";
                                }
                            }
                        default:
                            return "UnKnownDB";
                    }
                }
            }
            set { name = value; }
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public DatabaseTypes Type { get; set; }

        /// <summary>
        /// ���б�
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public List<Table> Tables { get; set; }

        /// <summary>
        /// ������ͼ��
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public List<Table> Views { get; set; }

        /// <summary>
        /// ���д洢������
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public List<string> StoreProcedures { get; set; }

        /// <summary>
        /// ѡ�еı����ͼ
        /// </summary>
        public List<Table> Selects { get; set; }
        #endregion

        #region ����
        /// <summary>
        /// ���һ����
        /// </summary>
        public void AddTable(Table table)
        {
            table.Database = this;
            table.IsView = false;
            Tables.Add(table);
        }

        /// <summary>
        /// ���һ����ͼ
        /// </summary>
        public void AddView(Table view)
        {
            view.Database = this;
            view.IsView = true;
            Views.Add(view);
        }

        /// <summary>
        /// ת��Ϊ�ַ���
        /// </summary>
        /// <returns>���ر���</returns>
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
