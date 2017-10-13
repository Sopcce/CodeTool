﻿namespace Sop.DatabaseDesignAssistant
{
    partial class BaseForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.PanelConect = new System.Windows.Forms.Panel();
            this.TxtConnectionString = new System.Windows.Forms.TextBox();
            this.LblConnectionString = new System.Windows.Forms.Label();
            this.GbAuthentication = new System.Windows.Forms.GroupBox();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.TxtUserName = new System.Windows.Forms.TextBox();
            this.LblPassword = new System.Windows.Forms.Label();
            this.LblUserName = new System.Windows.Forms.Label();
            this.RBtnSqlAuthentication = new System.Windows.Forms.RadioButton();
            this.RBtnWinAuthentication = new System.Windows.Forms.RadioButton();
            this.TxtServer = new System.Windows.Forms.TextBox();
            this.LblServer = new System.Windows.Forms.Label();
            this.RBtnProvideConnectionString = new System.Windows.Forms.RadioButton();
            this.RBtnSpecifiyServer = new System.Windows.Forms.RadioButton();
            this.PlSelectDataBase = new System.Windows.Forms.Panel();
            this.LblOperateTarget = new System.Windows.Forms.Label();
            this.ChkOperateView = new System.Windows.Forms.CheckBox();
            this.ChkOperateTable = new System.Windows.Forms.CheckBox();
            this.LblSelectDataBase = new System.Windows.Forms.Label();
            this.LbDataBases = new System.Windows.Forms.ListBox();
            this.PlSelectDataItem = new System.Windows.Forms.Panel();
            this.BtnClear = new System.Windows.Forms.Button();
            this.BtnSelectAll = new System.Windows.Forms.Button();
            this.LblSelectDataBaseItems = new System.Windows.Forms.Label();
            this.ChklSelectDataBaseItems = new System.Windows.Forms.CheckedListBox();
            this.BtnSchemaNext = new System.Windows.Forms.Button();
            this.BtnComplete = new System.Windows.Forms.Button();
            this.BtnPrevious = new System.Windows.Forms.Button();
            this.PlSetOption = new System.Windows.Forms.Panel();
            this.GbSetTargetDatBase = new System.Windows.Forms.GroupBox();
            this.RbtnMsSqlToMySql = new System.Windows.Forms.RadioButton();
            this.LblMySqlConnectionString = new System.Windows.Forms.Label();
            this.TxtMySqlConnectionString = new System.Windows.Forms.TextBox();
            this.RBtnMySqlToMsSql = new System.Windows.Forms.RadioButton();
            this.LblPath = new System.Windows.Forms.Label();
            this.RBtnExportMySql = new System.Windows.Forms.RadioButton();
            this.BtnSelectPath = new System.Windows.Forms.Button();
            this.RBtnSelectPath = new System.Windows.Forms.RadioButton();
            this.TxtPath = new System.Windows.Forms.TextBox();
            this.FbdSetPath = new System.Windows.Forms.FolderBrowserDialog();
            this.PlExecMessage = new System.Windows.Forms.Panel();
            this.TxtExecMessage = new System.Windows.Forms.TextBox();
            this.PanelConect.SuspendLayout();
            this.GbAuthentication.SuspendLayout();
            this.PlSelectDataBase.SuspendLayout();
            this.PlSelectDataItem.SuspendLayout();
            this.PlSetOption.SuspendLayout();
            this.GbSetTargetDatBase.SuspendLayout();
            this.PlExecMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelConect
            // 
            this.PanelConect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelConect.Controls.Add(this.TxtConnectionString);
            this.PanelConect.Controls.Add(this.LblConnectionString);
            this.PanelConect.Controls.Add(this.GbAuthentication);
            this.PanelConect.Controls.Add(this.TxtServer);
            this.PanelConect.Controls.Add(this.LblServer);
            this.PanelConect.Controls.Add(this.RBtnProvideConnectionString);
            this.PanelConect.Controls.Add(this.RBtnSpecifiyServer);
            this.PanelConect.Location = new System.Drawing.Point(15, 20);
            this.PanelConect.Name = "PanelConect";
            this.PanelConect.Size = new System.Drawing.Size(581, 392);
            this.PanelConect.TabIndex = 0;
            this.PanelConect.TabStop = true;
            // 
            // TxtConnectionString
            // 
            this.TxtConnectionString.Enabled = false;
            this.TxtConnectionString.Location = new System.Drawing.Point(46, 310);
            this.TxtConnectionString.Name = "TxtConnectionString";
            this.TxtConnectionString.Size = new System.Drawing.Size(467, 21);
            this.TxtConnectionString.TabIndex = 5;
            this.TxtConnectionString.Text = "Data Source=\'.\';User Id=\'sa\';Password=\'123456\';";
            // 
            // LblConnectionString
            // 
            this.LblConnectionString.AutoSize = true;
            this.LblConnectionString.Location = new System.Drawing.Point(44, 291);
            this.LblConnectionString.Name = "LblConnectionString";
            this.LblConnectionString.Size = new System.Drawing.Size(65, 12);
            this.LblConnectionString.TabIndex = 4;
            this.LblConnectionString.Text = "连接字符串";
            // 
            // GbAuthentication
            // 
            this.GbAuthentication.Controls.Add(this.TxtPassword);
            this.GbAuthentication.Controls.Add(this.TxtUserName);
            this.GbAuthentication.Controls.Add(this.LblPassword);
            this.GbAuthentication.Controls.Add(this.LblUserName);
            this.GbAuthentication.Controls.Add(this.RBtnSqlAuthentication);
            this.GbAuthentication.Controls.Add(this.RBtnWinAuthentication);
            this.GbAuthentication.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GbAuthentication.Location = new System.Drawing.Point(49, 75);
            this.GbAuthentication.Name = "GbAuthentication";
            this.GbAuthentication.Size = new System.Drawing.Size(470, 152);
            this.GbAuthentication.TabIndex = 3;
            this.GbAuthentication.TabStop = false;
            this.GbAuthentication.Text = "身份验证";
            // 
            // TxtPassword
            // 
            this.TxtPassword.Enabled = false;
            this.TxtPassword.Location = new System.Drawing.Point(161, 98);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '●';
            this.TxtPassword.Size = new System.Drawing.Size(297, 21);
            this.TxtPassword.TabIndex = 2;
            // 
            // TxtUserName
            // 
            this.TxtUserName.Enabled = false;
            this.TxtUserName.Location = new System.Drawing.Point(161, 64);
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new System.Drawing.Size(297, 21);
            this.TxtUserName.TabIndex = 2;
            this.TxtUserName.Text = "sa";
            // 
            // LblPassword
            // 
            this.LblPassword.AutoSize = true;
            this.LblPassword.Location = new System.Drawing.Point(36, 103);
            this.LblPassword.Name = "LblPassword";
            this.LblPassword.Size = new System.Drawing.Size(29, 12);
            this.LblPassword.TabIndex = 1;
            this.LblPassword.Text = "密码";
            // 
            // LblUserName
            // 
            this.LblUserName.AutoSize = true;
            this.LblUserName.Location = new System.Drawing.Point(36, 69);
            this.LblUserName.Name = "LblUserName";
            this.LblUserName.Size = new System.Drawing.Size(41, 12);
            this.LblUserName.TabIndex = 1;
            this.LblUserName.Text = "用户名";
            // 
            // RBtnSqlAuthentication
            // 
            this.RBtnSqlAuthentication.AutoSize = true;
            this.RBtnSqlAuthentication.Location = new System.Drawing.Point(18, 42);
            this.RBtnSqlAuthentication.Name = "RBtnSqlAuthentication";
            this.RBtnSqlAuthentication.Size = new System.Drawing.Size(155, 16);
            this.RBtnSqlAuthentication.TabIndex = 0;
            this.RBtnSqlAuthentication.Tag = "Authentication";
            this.RBtnSqlAuthentication.Text = "使用Sql Server身份认证";
            this.RBtnSqlAuthentication.UseVisualStyleBackColor = true;
            this.RBtnSqlAuthentication.CheckedChanged += new System.EventHandler(this.RBtnSqlAuthentication_CheckedChanged);
            // 
            // RBtnWinAuthentication
            // 
            this.RBtnWinAuthentication.AutoSize = true;
            this.RBtnWinAuthentication.Checked = true;
            this.RBtnWinAuthentication.Location = new System.Drawing.Point(18, 20);
            this.RBtnWinAuthentication.Name = "RBtnWinAuthentication";
            this.RBtnWinAuthentication.Size = new System.Drawing.Size(137, 16);
            this.RBtnWinAuthentication.TabIndex = 0;
            this.RBtnWinAuthentication.TabStop = true;
            this.RBtnWinAuthentication.Tag = "Authentication";
            this.RBtnWinAuthentication.Text = "使用windows身份认证";
            this.RBtnWinAuthentication.UseVisualStyleBackColor = true;
            this.RBtnWinAuthentication.CheckedChanged += new System.EventHandler(this.RBtnWinAuthentication_CheckedChanged);
            // 
            // TxtServer
            // 
            this.TxtServer.Location = new System.Drawing.Point(186, 37);
            this.TxtServer.Name = "TxtServer";
            this.TxtServer.Size = new System.Drawing.Size(333, 21);
            this.TxtServer.TabIndex = 2;
            this.TxtServer.Text = ".";
            // 
            // LblServer
            // 
            this.LblServer.AutoSize = true;
            this.LblServer.Location = new System.Drawing.Point(49, 40);
            this.LblServer.Name = "LblServer";
            this.LblServer.Size = new System.Drawing.Size(41, 12);
            this.LblServer.TabIndex = 1;
            this.LblServer.Text = "服务器";
            // 
            // RBtnProvideConnectionString
            // 
            this.RBtnProvideConnectionString.AutoSize = true;
            this.RBtnProvideConnectionString.Location = new System.Drawing.Point(8, 265);
            this.RBtnProvideConnectionString.Name = "RBtnProvideConnectionString";
            this.RBtnProvideConnectionString.Size = new System.Drawing.Size(107, 16);
            this.RBtnProvideConnectionString.TabIndex = 0;
            this.RBtnProvideConnectionString.Tag = "ConnectMode";
            this.RBtnProvideConnectionString.Text = "提供连接字符串";
            this.RBtnProvideConnectionString.UseVisualStyleBackColor = true;
            this.RBtnProvideConnectionString.CheckedChanged += new System.EventHandler(this.RBtnProvideConnectionString_CheckedChanged);
            // 
            // RBtnSpecifiyServer
            // 
            this.RBtnSpecifiyServer.AutoSize = true;
            this.RBtnSpecifiyServer.Checked = true;
            this.RBtnSpecifiyServer.Location = new System.Drawing.Point(8, 12);
            this.RBtnSpecifiyServer.Name = "RBtnSpecifiyServer";
            this.RBtnSpecifiyServer.Size = new System.Drawing.Size(83, 16);
            this.RBtnSpecifiyServer.TabIndex = 0;
            this.RBtnSpecifiyServer.TabStop = true;
            this.RBtnSpecifiyServer.Tag = "ConnectMode";
            this.RBtnSpecifiyServer.Text = "指定服务器";
            this.RBtnSpecifiyServer.UseVisualStyleBackColor = true;
            this.RBtnSpecifiyServer.CheckedChanged += new System.EventHandler(this.RBtnSpecifiyServer_CheckedChanged);
            // 
            // PlSelectDataBase
            // 
            this.PlSelectDataBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlSelectDataBase.Controls.Add(this.LblOperateTarget);
            this.PlSelectDataBase.Controls.Add(this.ChkOperateView);
            this.PlSelectDataBase.Controls.Add(this.ChkOperateTable);
            this.PlSelectDataBase.Controls.Add(this.LblSelectDataBase);
            this.PlSelectDataBase.Controls.Add(this.LbDataBases);
            this.PlSelectDataBase.Location = new System.Drawing.Point(15, 20);
            this.PlSelectDataBase.Name = "PlSelectDataBase";
            this.PlSelectDataBase.Size = new System.Drawing.Size(581, 392);
            this.PlSelectDataBase.TabIndex = 1;
            this.PlSelectDataBase.TabStop = true;
            this.PlSelectDataBase.Visible = false;
            // 
            // LblOperateTarget
            // 
            this.LblOperateTarget.AutoSize = true;
            this.LblOperateTarget.Location = new System.Drawing.Point(37, 357);
            this.LblOperateTarget.Name = "LblOperateTarget";
            this.LblOperateTarget.Size = new System.Drawing.Size(53, 12);
            this.LblOperateTarget.TabIndex = 4;
            this.LblOperateTarget.Text = "操作对象";
            this.LblOperateTarget.Visible = false;
            // 
            // ChkOperateView
            // 
            this.ChkOperateView.AutoSize = true;
            this.ChkOperateView.Enabled = false;
            this.ChkOperateView.Location = new System.Drawing.Point(186, 357);
            this.ChkOperateView.Name = "ChkOperateView";
            this.ChkOperateView.Size = new System.Drawing.Size(48, 16);
            this.ChkOperateView.TabIndex = 3;
            this.ChkOperateView.Text = "视图";
            this.ChkOperateView.UseVisualStyleBackColor = true;
            this.ChkOperateView.Visible = false;
            // 
            // ChkOperateTable
            // 
            this.ChkOperateTable.AutoSize = true;
            this.ChkOperateTable.Enabled = false;
            this.ChkOperateTable.Location = new System.Drawing.Point(126, 356);
            this.ChkOperateTable.Name = "ChkOperateTable";
            this.ChkOperateTable.Size = new System.Drawing.Size(36, 16);
            this.ChkOperateTable.TabIndex = 3;
            this.ChkOperateTable.Text = "表";
            this.ChkOperateTable.UseVisualStyleBackColor = true;
            this.ChkOperateTable.Visible = false;
            // 
            // LblSelectDataBase
            // 
            this.LblSelectDataBase.AutoSize = true;
            this.LblSelectDataBase.Location = new System.Drawing.Point(33, 15);
            this.LblSelectDataBase.Name = "LblSelectDataBase";
            this.LblSelectDataBase.Size = new System.Drawing.Size(65, 12);
            this.LblSelectDataBase.TabIndex = 2;
            this.LblSelectDataBase.Text = "选择数据库";
            // 
            // LbDataBases
            // 
            this.LbDataBases.FormattingEnabled = true;
            this.LbDataBases.ItemHeight = 12;
            this.LbDataBases.Location = new System.Drawing.Point(35, 39);
            this.LbDataBases.Name = "LbDataBases";
            this.LbDataBases.Size = new System.Drawing.Size(508, 304);
            this.LbDataBases.TabIndex = 1;
            this.LbDataBases.Click += new System.EventHandler(this.LbDataBases_Click);
            this.LbDataBases.SelectedValueChanged += new System.EventHandler(this.LbDataBases_SelectedValueChanged);
            // 
            // PlSelectDataItem
            // 
            this.PlSelectDataItem.Controls.Add(this.BtnClear);
            this.PlSelectDataItem.Controls.Add(this.BtnSelectAll);
            this.PlSelectDataItem.Controls.Add(this.LblSelectDataBaseItems);
            this.PlSelectDataItem.Controls.Add(this.ChklSelectDataBaseItems);
            this.PlSelectDataItem.Location = new System.Drawing.Point(15, 20);
            this.PlSelectDataItem.Name = "PlSelectDataItem";
            this.PlSelectDataItem.Size = new System.Drawing.Size(583, 392);
            this.PlSelectDataItem.TabIndex = 3;
            this.PlSelectDataItem.Visible = false;
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(166, 350);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(75, 23);
            this.BtnClear.TabIndex = 3;
            this.BtnClear.Text = "清除";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnSelectAll
            // 
            this.BtnSelectAll.Location = new System.Drawing.Point(54, 350);
            this.BtnSelectAll.Name = "BtnSelectAll";
            this.BtnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.BtnSelectAll.TabIndex = 2;
            this.BtnSelectAll.Text = "全选";
            this.BtnSelectAll.UseVisualStyleBackColor = true;
            this.BtnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // LblSelectDataBaseItems
            // 
            this.LblSelectDataBaseItems.AutoSize = true;
            this.LblSelectDataBaseItems.Location = new System.Drawing.Point(38, 22);
            this.LblSelectDataBaseItems.Name = "LblSelectDataBaseItems";
            this.LblSelectDataBaseItems.Size = new System.Drawing.Size(65, 12);
            this.LblSelectDataBaseItems.TabIndex = 1;
            this.LblSelectDataBaseItems.Text = "选择数据表";
            // 
            // ChklSelectDataBaseItems
            // 
            this.ChklSelectDataBaseItems.CheckOnClick = true;
            this.ChklSelectDataBaseItems.FormattingEnabled = true;
            this.ChklSelectDataBaseItems.Location = new System.Drawing.Point(38, 41);
            this.ChklSelectDataBaseItems.Name = "ChklSelectDataBaseItems";
            this.ChklSelectDataBaseItems.Size = new System.Drawing.Size(508, 292);
            this.ChklSelectDataBaseItems.TabIndex = 0;
            // 
            // BtnSchemaNext
            // 
            this.BtnSchemaNext.Location = new System.Drawing.Point(420, 419);
            this.BtnSchemaNext.Name = "BtnSchemaNext";
            this.BtnSchemaNext.Size = new System.Drawing.Size(75, 23);
            this.BtnSchemaNext.TabIndex = 8;
            this.BtnSchemaNext.Text = "下一步》";
            this.BtnSchemaNext.UseVisualStyleBackColor = true;
            this.BtnSchemaNext.Click += new System.EventHandler(this.BtnSchemaNext_Click);
            // 
            // BtnComplete
            // 
            this.BtnComplete.Location = new System.Drawing.Point(510, 419);
            this.BtnComplete.Name = "BtnComplete";
            this.BtnComplete.Size = new System.Drawing.Size(75, 23);
            this.BtnComplete.TabIndex = 7;
            this.BtnComplete.Text = "取消";
            this.BtnComplete.UseVisualStyleBackColor = true;
            this.BtnComplete.Click += new System.EventHandler(this.BtnComplete_Click);
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Location = new System.Drawing.Point(328, 419);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(75, 23);
            this.BtnPrevious.TabIndex = 8;
            this.BtnPrevious.Text = "《上一步";
            this.BtnPrevious.UseVisualStyleBackColor = true;
            this.BtnPrevious.Visible = false;
            this.BtnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // PlSetOption
            // 
            this.PlSetOption.Controls.Add(this.GbSetTargetDatBase);
            this.PlSetOption.Controls.Add(this.LblPath);
            this.PlSetOption.Controls.Add(this.RBtnExportMySql);
            this.PlSetOption.Controls.Add(this.BtnSelectPath);
            this.PlSetOption.Controls.Add(this.RBtnSelectPath);
            this.PlSetOption.Controls.Add(this.TxtPath);
            this.PlSetOption.Location = new System.Drawing.Point(15, 20);
            this.PlSetOption.Name = "PlSetOption";
            this.PlSetOption.Size = new System.Drawing.Size(581, 392);
            this.PlSetOption.TabIndex = 9;
            this.PlSetOption.Visible = false;
            // 
            // GbSetTargetDatBase
            // 
            this.GbSetTargetDatBase.Controls.Add(this.RbtnMsSqlToMySql);
            this.GbSetTargetDatBase.Controls.Add(this.LblMySqlConnectionString);
            this.GbSetTargetDatBase.Controls.Add(this.TxtMySqlConnectionString);
            this.GbSetTargetDatBase.Controls.Add(this.RBtnMySqlToMsSql);
            this.GbSetTargetDatBase.Location = new System.Drawing.Point(36, 256);
            this.GbSetTargetDatBase.Name = "GbSetTargetDatBase";
            this.GbSetTargetDatBase.Size = new System.Drawing.Size(484, 99);
            this.GbSetTargetDatBase.TabIndex = 9;
            this.GbSetTargetDatBase.TabStop = false;
            // 
            // RbtnMsSqlToMySql
            // 
            this.RbtnMsSqlToMySql.AutoSize = true;
            this.RbtnMsSqlToMySql.Checked = true;
            this.RbtnMsSqlToMySql.Enabled = false;
            this.RbtnMsSqlToMySql.Location = new System.Drawing.Point(91, 61);
            this.RbtnMsSqlToMySql.Name = "RbtnMsSqlToMySql";
            this.RbtnMsSqlToMySql.Size = new System.Drawing.Size(107, 16);
            this.RbtnMsSqlToMySql.TabIndex = 7;
            this.RbtnMsSqlToMySql.TabStop = true;
            this.RbtnMsSqlToMySql.Tag = "SetTarget";
            this.RbtnMsSqlToMySql.Text = "MsSql导入MySql";
            this.RbtnMsSqlToMySql.UseVisualStyleBackColor = true;
            // 
            // LblMySqlConnectionString
            // 
            this.LblMySqlConnectionString.AutoSize = true;
            this.LblMySqlConnectionString.Location = new System.Drawing.Point(12, 28);
            this.LblMySqlConnectionString.Name = "LblMySqlConnectionString";
            this.LblMySqlConnectionString.Size = new System.Drawing.Size(65, 12);
            this.LblMySqlConnectionString.TabIndex = 6;
            this.LblMySqlConnectionString.Text = "连接字符串";
            // 
            // TxtMySqlConnectionString
            // 
            this.TxtMySqlConnectionString.Enabled = false;
            this.TxtMySqlConnectionString.Location = new System.Drawing.Point(91, 23);
            this.TxtMySqlConnectionString.Name = "TxtMySqlConnectionString";
            this.TxtMySqlConnectionString.Size = new System.Drawing.Size(343, 21);
            this.TxtMySqlConnectionString.TabIndex = 4;
            this.TxtMySqlConnectionString.Text = "Database=\'数据库名\';Data Source=\'数据库服务器地址\';User Id=\'数据库用户名\';Password=\'密码\';";
            // 
            // RBtnMySqlToMsSql
            // 
            this.RBtnMySqlToMsSql.AutoSize = true;
            this.RBtnMySqlToMsSql.Enabled = false;
            this.RBtnMySqlToMsSql.Location = new System.Drawing.Point(245, 61);
            this.RBtnMySqlToMsSql.Name = "RBtnMySqlToMsSql";
            this.RBtnMySqlToMsSql.Size = new System.Drawing.Size(107, 16);
            this.RBtnMySqlToMsSql.TabIndex = 8;
            this.RBtnMySqlToMsSql.Tag = "SetTarget";
            this.RBtnMySqlToMsSql.Text = "MySql导入MsSql";
            this.RBtnMySqlToMsSql.UseVisualStyleBackColor = true;
            // 
            // LblPath
            // 
            this.LblPath.AutoSize = true;
            this.LblPath.Location = new System.Drawing.Point(56, 62);
            this.LblPath.Name = "LblPath";
            this.LblPath.Size = new System.Drawing.Size(29, 12);
            this.LblPath.TabIndex = 5;
            this.LblPath.Text = "路径";
            // 
            // RBtnExportMySql
            // 
            this.RBtnExportMySql.AutoSize = true;
            this.RBtnExportMySql.Location = new System.Drawing.Point(40, 234);
            this.RBtnExportMySql.Name = "RBtnExportMySql";
            this.RBtnExportMySql.Size = new System.Drawing.Size(113, 16);
            this.RBtnExportMySql.TabIndex = 3;
            this.RBtnExportMySql.Tag = "SetOption";
            this.RBtnExportMySql.Text = "与MySql数据互导";
            this.RBtnExportMySql.UseVisualStyleBackColor = true;
            this.RBtnExportMySql.CheckedChanged += new System.EventHandler(this.RBtnExportMySql_CheckedChanged);
            // 
            // BtnSelectPath
            // 
            this.BtnSelectPath.Location = new System.Drawing.Point(395, 57);
            this.BtnSelectPath.Name = "BtnSelectPath";
            this.BtnSelectPath.Size = new System.Drawing.Size(40, 23);
            this.BtnSelectPath.TabIndex = 2;
            this.BtnSelectPath.Text = "...";
            this.BtnSelectPath.UseVisualStyleBackColor = true;
            this.BtnSelectPath.Click += new System.EventHandler(this.BtnSelectPath_Click);
            // 
            // RBtnSelectPath
            // 
            this.RBtnSelectPath.AutoSize = true;
            this.RBtnSelectPath.Checked = true;
            this.RBtnSelectPath.Location = new System.Drawing.Point(40, 31);
            this.RBtnSelectPath.Name = "RBtnSelectPath";
            this.RBtnSelectPath.Size = new System.Drawing.Size(119, 16);
            this.RBtnSelectPath.TabIndex = 1;
            this.RBtnSelectPath.TabStop = true;
            this.RBtnSelectPath.Tag = "SetOption";
            this.RBtnSelectPath.Text = "选择脚本生成路径";
            this.RBtnSelectPath.UseVisualStyleBackColor = true;
            this.RBtnSelectPath.CheckedChanged += new System.EventHandler(this.RBtnSelectPath_CheckedChanged);
            // 
            // TxtPath
            // 
            this.TxtPath.Location = new System.Drawing.Point(118, 59);
            this.TxtPath.Name = "TxtPath";
            this.TxtPath.Size = new System.Drawing.Size(270, 21);
            this.TxtPath.TabIndex = 0;
            // 
            // PlExecMessage
            // 
            this.PlExecMessage.Controls.Add(this.TxtExecMessage);
            this.PlExecMessage.Location = new System.Drawing.Point(15, 20);
            this.PlExecMessage.Name = "PlExecMessage";
            this.PlExecMessage.Size = new System.Drawing.Size(581, 392);
            this.PlExecMessage.TabIndex = 10;
            this.PlExecMessage.Visible = false;
            // 
            // TxtExecMessage
            // 
            this.TxtExecMessage.Location = new System.Drawing.Point(22, 22);
            this.TxtExecMessage.Multiline = true;
            this.TxtExecMessage.Name = "TxtExecMessage";
            this.TxtExecMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtExecMessage.Size = new System.Drawing.Size(535, 352);
            this.TxtExecMessage.TabIndex = 0;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 457);
            this.Controls.Add(this.BtnPrevious);
            this.Controls.Add(this.BtnSchemaNext);
            this.Controls.Add(this.BtnComplete);
            this.Controls.Add(this.PanelConect);
            this.Controls.Add(this.PlSelectDataBase);
            this.Controls.Add(this.PlSelectDataItem);
            this.Controls.Add(this.PlSetOption);
            this.Controls.Add(this.PlExecMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(626, 496);
            this.MinimumSize = new System.Drawing.Size(626, 496);
            this.Name = "BaseForm";
            this.Text = "Sop数据库迁移V1.0";
            this.PanelConect.ResumeLayout(false);
            this.PanelConect.PerformLayout();
            this.GbAuthentication.ResumeLayout(false);
            this.GbAuthentication.PerformLayout();
            this.PlSelectDataBase.ResumeLayout(false);
            this.PlSelectDataBase.PerformLayout();
            this.PlSelectDataItem.ResumeLayout(false);
            this.PlSelectDataItem.PerformLayout();
            this.PlSetOption.ResumeLayout(false);
            this.PlSetOption.PerformLayout();
            this.GbSetTargetDatBase.ResumeLayout(false);
            this.GbSetTargetDatBase.PerformLayout();
            this.PlExecMessage.ResumeLayout(false);
            this.PlExecMessage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelConect;
        private System.Windows.Forms.RadioButton RBtnSpecifiyServer;
        private System.Windows.Forms.Label LblServer;
        private System.Windows.Forms.GroupBox GbAuthentication;
        private System.Windows.Forms.TextBox TxtServer;
        private System.Windows.Forms.RadioButton RBtnWinAuthentication;
        private System.Windows.Forms.RadioButton RBtnSqlAuthentication;
        private System.Windows.Forms.Label LblUserName;
        private System.Windows.Forms.TextBox TxtUserName;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Label LblPassword;
        private System.Windows.Forms.RadioButton RBtnProvideConnectionString;
        private System.Windows.Forms.Label LblConnectionString;
        private System.Windows.Forms.TextBox TxtConnectionString;
        private System.Windows.Forms.Button BtnSchemaNext;
        private System.Windows.Forms.Button BtnComplete;
        private System.Windows.Forms.Panel PlSelectDataBase;
        private System.Windows.Forms.Button BtnPrevious;
        private System.Windows.Forms.ListBox LbDataBases;
        private System.Windows.Forms.Label LblSelectDataBase;
        private System.Windows.Forms.Panel PlSelectDataItem;
        private System.Windows.Forms.Label LblSelectDataBaseItems;
        private System.Windows.Forms.CheckedListBox ChklSelectDataBaseItems;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Button BtnSelectAll;
        private System.Windows.Forms.Label LblOperateTarget;
        private System.Windows.Forms.CheckBox ChkOperateTable;
        private System.Windows.Forms.CheckBox ChkOperateView;
        private System.Windows.Forms.Panel PlSetOption;
        private System.Windows.Forms.Label LblPath;
        private System.Windows.Forms.TextBox TxtMySqlConnectionString;
        private System.Windows.Forms.RadioButton RBtnExportMySql;
        private System.Windows.Forms.Button BtnSelectPath;
        private System.Windows.Forms.RadioButton RBtnSelectPath;
        private System.Windows.Forms.TextBox TxtPath;
        private System.Windows.Forms.Label LblMySqlConnectionString;
        private System.Windows.Forms.FolderBrowserDialog FbdSetPath;
        private System.Windows.Forms.Panel PlExecMessage;
        private System.Windows.Forms.TextBox TxtExecMessage;
        private System.Windows.Forms.RadioButton RBtnMySqlToMsSql;
        private System.Windows.Forms.RadioButton RbtnMsSqlToMySql;
        private System.Windows.Forms.GroupBox GbSetTargetDatBase;

    }
}

