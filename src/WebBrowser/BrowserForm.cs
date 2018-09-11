using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AxSHDocVw;
using Microsoft.Win32;
using WeifenLuo.WinFormsUI.Docking;

namespace WebBrowser
{
    public partial class BrowserForm : DockContent
    {
        /// <summary>
        /// 404����ʱҪת���ҳ��
        /// </summary>
        private readonly string ERROR_GOTO = "http://www.baidu.com";

        #region ���캯��

        public BrowserForm()
        {
            int value = GetBrowserVersion();
            SetWebBrowserFeatures(11);

            InitializeComponent();
        }

        public BrowserForm(ContextMenuStrip cms, string url)
        {
            InitializeComponent();
            this.TabPageContextMenuStrip = cms;

            if (false == string.IsNullOrEmpty(url))
            {
                OpenUrl(url);
            }
        }

        #endregion

        #region �¼�

        public delegate void TitleChangeEventHandler(IDockContent dockContent, string title);

        /// <summary>
        /// ������ı�ʱҪִ�е��¼�
        /// </summary>
        public event TitleChangeEventHandler TitleChange;

        #endregion

        #region ����

        /// <summary>
        /// ��ǰ��ҳ�ĵ�
        /// </summary>
        public mshtml.IHTMLDocument2 Document
        {
            get
            {
                mshtml.IHTMLDocument2 doc = AxWebBrowser1.Document as mshtml.IHTMLDocument2;
                return doc;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ����ϵͳ�ӿڲ鿴Դ�ļ�
        /// </summary>
        public void ViewSource()
        {
            try
            {
                BrowseHelper.BrowserWapper.ViewSource(Document);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��һ����վ
        /// </summary>
        /// <param name="url">��վURL</param>
        public void Go(string url)
        {
            Application.DoEvents();
            try
            {
                AxWebBrowser1.Navigate(url);
                cobURL.Text = url;
                labStatus.Text = "���ڴ���վ " + url;
                AxWebBrowser1.Focus();
                Application.DoEvents();
                AddItemToToolStripComboBox(cobURL, url);
            }
            catch (Exception)
            {
            }
        }

        private void AddItemToToolStripComboBox(ToolStripComboBox cob, string item)
        {
            if (cob.Items.Contains(item))
            {
                cob.Items.Remove(item);
            }

            cob.Items.Insert(0, item);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyword">Ҫ�����Ĺؼ���</param>
        public void Search(string keyword)
        {
            ToolStripMenuItem searchEngine = GetSelectedSearchEngine();
            if (searchEngine != null)
            {
                string url = string.Format(searchEngine.Tag.ToString(), System.Web.HttpUtility.UrlEncode(keyword));
                Go(url);
                AddItemToToolStripComboBox(cobSearch, keyword);
            }
        }

        private void AddSearchHistory(string keyword)
        {
            if (cobSearch.Items.Contains(keyword))
            {
                cobSearch.Items.Remove(keyword);
            }
            cobSearch.Items.Insert(0, keyword);
        }

        #endregion

        #region ����

        private void cobSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search(cobSearch.Text.Trim());
            }
        }

        private void changeSearchEngine(ToolStripMenuItem selctedSearchEngine)
        {
            ToolStripItemCollection searchEngines = btnSearch.DropDownItems;
            foreach (ToolStripItem searchEngine in searchEngines)
            {
                if (searchEngine is ToolStripMenuItem)
                {
                    (searchEngine as ToolStripMenuItem).Checked = false;
                }
            }

            selctedSearchEngine.Checked = true;
        }

        private void SearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeSearchEngine(sender as ToolStripMenuItem);
            btnSearch_Click(sender, e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cobSearch.Text.Trim()))
            {
                Search(cobSearch.Text.Trim());
            }
        }

        private void mnuClearSearchHistory_Click(object sender, EventArgs e)
        {
            cobSearch.Items.Clear();
        }

        /// <summary>
        /// ��ȡ��ǰѡ�е��������棬���û�з��ص�һ��������������������棬����Null
        /// </summary>
        private ToolStripMenuItem GetSelectedSearchEngine()
        {
            List<ToolStripMenuItem> searchEngines = GetAllSearchEngines();

            if (searchEngines.Count > 0)
            {
                foreach (ToolStripMenuItem searchEngine in searchEngines)
                {
                    if (searchEngine.Checked)
                    {
                        return searchEngine;
                    }
                }

                return searchEngines[0];
            }

            return null;
        }

        /// <summary>
        /// ��ȡ���е���������
        /// </summary>
        private List<ToolStripMenuItem> GetAllSearchEngines()
        {
            List<ToolStripMenuItem> searchEngines = new List<ToolStripMenuItem>();
            foreach (ToolStripItem item in btnSearch.DropDownItems)
            {
                if (item is ToolStripMenuItem && item.Tag != null && !string.IsNullOrEmpty(item.Tag.ToString()))
                {
                    searchEngines.Add(item as ToolStripMenuItem);
                }
            }
            return searchEngines;
        }

        #endregion

        #region AxWebBrowser

        /// <summary>
        /// �������״̬����ʾ������������
        /// </summary>
        private void AxWebBrowser1_StatusTextChange(object sender, DWebBrowserEvents2_StatusTextChangeEvent e)
        {
            Application.DoEvents();
            labStatus.Text = e.text;
        }

        /// <summary>
        /// ������ơ�ǰ�����͡����ˡ���ť�Ƿ����
        /// </summary>
        private void AxWebBrowser1_CommandStateChange(object sender, DWebBrowserEvents2_CommandStateChangeEvent e)
        {
            Application.DoEvents();
            if (e.command == 1)
            {
                btnGoFoward.Enabled = e.enable;
            }
            else if (e.command == 2)
            {
                btnGoBack.Enabled = e.enable;
            }
        }

        /// <summary>
        /// ���ǰ���Shift���ʱ�����´��ڵ��¼���Ӧ�ô�һ���±�ǩҳ
        /// </summary>
        private void AxWebBrowser1_NewWindow3(object sender, AxSHDocVw.DWebBrowserEvents2_NewWindow3Event e)
        {
            BrowserForm frmBrowser = new BrowserForm();
            frmBrowser.TitleChange = TitleChange;
            e.ppDisp = frmBrowser.AxWebBrowser1.Application;
            if (this.DockPanel != null)
            {
                frmBrowser.Show(this.DockPanel);
            }
            else
            {
                frmBrowser.Show();
            }
        }

        /// <summary>
        /// ������仯ʱ���¼�����ǩҳ��Ӧ����ʾ���⡣
        /// ���������������������ı���Ӧ�ñ仯��
        /// </summary>
        private void AxWebBrowser1_TitleChange(object sender, AxSHDocVw.DWebBrowserEvents2_TitleChangeEvent e)
        {
            Application.DoEvents();
            this.TabText = BrowseHelper.BrowserUtility.CutString(e.text, 20); ;
            this.ToolTipText = e.text;

            if (TitleChange != null)
            {
                TitleChange(this, e.text);
            }
        }

        /// <summary>
        /// �ĵ��������ʱ��URL����ʾ���յĵ�ַ
        /// </summary>
        private void AxWebBrowser1_DownloadComplete(object sender, EventArgs e)
        {
            Application.DoEvents();
            cobURL.Text = AxWebBrowser1.LocationURL;
        }

        /// <summary>
        /// ���صĽ��ȣ�Ӧ����ʾ�ڽ�������
        /// </summary>
        private void AxWebBrowser1_ProgressChange(object sender, AxSHDocVw.DWebBrowserEvents2_ProgressChangeEvent e)
        {
            Application.DoEvents();
            switch (e.progress)
            {
                case -1:
                    break;
                case 0:
                    pbStatus.Visible = false;
                    break;
                default:
                    pbStatus.Visible = true;
                    pbStatus.Maximum = e.progressMax;
                    pbStatus.Value = Math.Min(e.progressMax, e.progress);
                    break;
            }
        }

        private void AxWebBrowser1_NavigateError(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateErrorEvent e)
        {
            Application.DoEvents();
            if (Convert.ToInt32(e.statusCode) == 400)
            {
                labStatus.Text = "��վδ�ҵ�...";
                if (!string.IsNullOrEmpty(ERROR_GOTO))
                {
                    OpenUrl(ERROR_GOTO);
                }
            }
        }

        private void AxWebBrowser1_NewWindow2(object sender, AxSHDocVw.DWebBrowserEvents2_NewWindow2Event e)
        {
            BrowserForm frmBrowser = new BrowserForm();
            e.ppDisp = frmBrowser.AxWebBrowser1.Application;
            if (this.DockPanel != null)
            {
                frmBrowser.Show(this.DockPanel);
            }
            else
            {
                frmBrowser.Show();
            }
        }
        #endregion

        #region Url�ı���

        private void cobURL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((e.Modifiers & Keys.Control) == Keys.Control
                    && (e.Modifiers & Keys.Shift) == Keys.Shift)
                {
                    //������Ctrl��Shift��
                    OpenUrl(string.Format("http://www.{0}.com.cn", cobURL.Text.Trim()));
                }
                else if (e.Modifiers == Keys.Shift)
                {
                    //������Shift��
                    OpenUrl(string.Format("http://www.{0}.net", cobURL.Text.Trim()));
                }
                else if (e.Modifiers == Keys.Control)
                {
                    //������Ctrl��
                    OpenUrl(string.Format("http://www.{0}.com", cobURL.Text.Trim()));
                }
                else
                {
                    OpenUrl(cobURL.Text.Trim());
                }
            }
        }

        private void btnGo_ButtonClick(object sender, EventArgs e)
        {
            OpenUrl(cobURL.Text.Trim());
        }

        private void btnGo_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            cobURL.Items.Clear();
        }

        private void OpenUrl(string url)
        {
            if (url.ToUpper().IndexOf(":") < 0)
            {
                if (url.IndexOf(".") < 0)
                {
                    Search(url);
                }
                else
                {
                    url = url.Insert(0, "http://");
                    Go(url);
                }
            }
            else
            {
                Go(url);
            }
        }

        #endregion

        #region �������Ӧ

        private void toolStrip1_SizeChanged(object sender, EventArgs e)
        {
            SetWidthOfCobURL();
        }

        private void statusStrip1_SizeChanged(object sender, EventArgs e)
        {
            SetWidthOfLabStatus();
        }

        private void tspbStatus_VisibleChanged(object sender, EventArgs e)
        {
            SetWidthOfLabStatus();
        }

        private void SetWidthOfLabStatus()
        {
            Application.DoEvents();
            int width = 0;
            foreach (ToolStripItem item in statusStrip1.Items)
            {
                if (item != labStatus && item.Visible == true)
                {
                    width += item.Width;
                }
            }
            labStatus.Width = statusStrip1.Width - width - 15 - (pbStatus.Visible ? 2 : 0);
        }

        private void SetWidthOfCobURL()
        {
            Application.DoEvents();
            int width = 0;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item != cobURL)
                {
                    width += item.Width;
                }
            }
            cobURL.Width = toolStrip1.Width - width - 20;
        }

        #endregion

        #region ������

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                AxWebBrowser1.Refresh();
            }
            catch
            {
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            try
            {
                AxWebBrowser1.GoBack();
            }
            catch
            {
            }
        }

        private void btnGoFoward_Click(object sender, EventArgs e)
        {
            try
            {
                AxWebBrowser1.GoForward();
            }
            catch
            {
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                AxWebBrowser1.Stop();
            }
            catch
            {
            }
        }

        private void btnGoHome_Click(object sender, EventArgs e)
        {
            try
            {
                AxWebBrowser1.GoHome();
            }
            catch
            {
            }
        }

        #endregion


        #region ����IE�汾
        /// <summary>  
        /// �޸�ע�����Ϣ�����ݵ�ǰ����  
        ///   
        /// </summary>  
        static void SetWebBrowserFeatures(int ieVersion)
        {
            // don't change the registry if running in-proc inside Visual Studio  
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime)
                return;
            //��ȡ��������  
            var appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            //�õ��������ģʽ��ֵ  
            UInt32 ieMode = GeoEmulationModee(ieVersion);
            var featureControlRegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\";
            //�����������Ӧ�ó���appName����ʲôģʽ��ieMode������  
            Registry.SetValue(featureControlRegKey + "FEATURE_BROWSER_EMULATION",
                appName, ieMode, RegistryValueKind.DWord);
            // enable the features which are "On" for the full Internet Explorer browser  
            //������������ʲô��  
            Registry.SetValue(featureControlRegKey + "FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION",
                appName, 1, RegistryValueKind.DWord);


            //Registry.SetValue(featureControlRegKey + "FEATURE_AJAX_CONNECTIONEVENTS",  
            //    appName, 1, RegistryValueKind.DWord);  


            //Registry.SetValue(featureControlRegKey + "FEATURE_GPU_RENDERING",  
            //    appName, 1, RegistryValueKind.DWord);  


            //Registry.SetValue(featureControlRegKey + "FEATURE_WEBOC_DOCUMENT_ZOOM",  
            //    appName, 1, RegistryValueKind.DWord);  


            //Registry.SetValue(featureControlRegKey + "FEATURE_NINPUT_LEGACYMODE",  
            //    appName, 0, RegistryValueKind.DWord);  
        }
        /// <summary>  
        /// ��ȡ������İ汾  
        /// </summary>  
        /// <returns></returns>  
        static int GetBrowserVersion()
        {
            int browserVersion = 0;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }
            //���С��7  
            if (browserVersion < 7)
            {
                throw new ApplicationException("��֧�ֵ�������汾!");
            }
            return browserVersion;
        }
        /// <summary>  
        /// ͨ���汾�õ������ģʽ��ֵ  
        /// </summary>  
        /// <param name="browserVersion"></param>  
        /// <returns></returns>  
        static UInt32 GeoEmulationModee(int browserVersion)
        {
            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode.   
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.   
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.   
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.                      
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10.  
                    break;
                case 11:
                    mode = 11000; // Internet Explorer 11  
                    break;
            }
            return mode;
        } 
        #endregion

    }
}