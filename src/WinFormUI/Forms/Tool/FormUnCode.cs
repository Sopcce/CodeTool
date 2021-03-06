﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CodeTool
{
    public partial class FormUnCode : Form
    {
        public FormUnCode()
        {
            InitializeComponent();
        }
        private void btnStringBuilder1_Click(object sender, EventArgs e)
        {
            try
            {
                string result = string.Empty;
                string[] lines = richTextBoxBefore.Lines;
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    result = result + "sb.Append(\" " + line.Replace("\"", string.Format("\\{0}", "\"")) + "\"); \n";
                } 
                richTextBoxAfter.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"错误信息：" + ex.Message);
            }

        }

        private void btnCleanRN_Click(object sender, EventArgs e)
        {
            try
            {
                string text = richTextBoxBefore.Text;
                text = text.Replace(Environment.NewLine, "");
                text = text.Replace("\n", "");
                text = text.Replace("\r", "");
                richTextBoxAfter.Text = text.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：" + ex.Message);
            }
           
        }

        private void btnClearSpChar_Click(object sender, EventArgs e)
        {
            try
            {
                string result = richTextBoxBefore.Text;
                string charValue = textBox_clernstr.Text;
                var charArray = charValue.ToCharArray(0, charValue.Length);
                foreach (var item in charArray)
                {
                    result = result.Replace(item.ToString(), "");
                }
                richTextBoxAfter.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Test" + ex.Message);
            }
        }
    

   

        private void btnStringBuilder_AppendFormat_Click(object sender, EventArgs e)
        {
            try
            {
                string result = string.Empty;
                string[] lines = richTextBoxBefore.Lines;
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    result = result + "sb.AppendFormat(\" " + line.Replace("\"", string.Format("\\{0}", "\"")) + "\"); \n";
                }

                richTextBoxAfter.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：" + ex.Message);
            }
        }
    }
}
