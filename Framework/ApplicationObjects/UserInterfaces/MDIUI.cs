using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Xml;
using System.Net;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Lendings;
using NSites_V.ApplicationObjects.Classes.Systems;

using NSites_V.ApplicationObjects.UserInterfaces;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Lendings.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Lendings.Transactions;
using NSites_V.ApplicationObjects.UserInterfaces.Lendings.Reports;
//using NSites_V.ApplicationObjects.UserInterfaces.HRISs.Reports;
using NSites_V.ApplicationObjects.UserInterfaces.Systems;
using NSites_V.ApplicationObjects.UserInterfaces.Systems.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Systems.Reports;

namespace NSites_V.ApplicationObjects.UserInterfaces
{
    public partial class MDINSites_VUI : Form
    {
        #region "VARIABLES"
        UserGroup loUserGroup;
        DataView ldvUserGroup;
        DataTable ldtUserGroup;
        SystemConfiguration loSystemConfiguration;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public MDINSites_VUI()
        {
            InitializeComponent();
            loUserGroup = new UserGroup();
            ldtUserGroup = new DataTable();
            loSystemConfiguration = new SystemConfiguration();
        }
        #endregion "END OF CONSTRUCTORS"

        #region "METHODS"
        private void disabledMenuStrip()
        {
            try
            {
                foreach (ToolStripMenuItem item in mnsNSites_V.Items)
                {
                    item.Enabled = false;
                    foreach (ToolStripItem subitem in item.DropDownItems)
                    {
                        if (subitem is ToolStripMenuItem)
                        {
                            subitem.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void enabledMenuStrip()
        {
            try
            {
                ldtUserGroup = loUserGroup.getUserGroupMenuItems();

                GlobalVariables.DVRights = new DataView(loUserGroup.getUserGroupRights());
                ldvUserGroup = new DataView(ldtUserGroup);
                foreach (ToolStripMenuItem item in mnsNSites_V.Items)
                {
                    try
                    {
                        ldvUserGroup.RowFilter = "Menu = '" + item.Name + "'";
                    }
                    catch { }
                    if (ldvUserGroup.Count != 0)
                    {
                        item.Enabled = true;
                        processMenuItems(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void processMenuItems(ToolStripMenuItem pitem)
        {
            try
            {
                if (true)
                {
                    pitem.Enabled = true;
                }

                foreach (ToolStripItem subitem in pitem.DropDownItems)
                {
                    if (subitem is ToolStripMenuItem)
                    {
                        ldvUserGroup.RowFilter = "Item = '" + subitem.Name + "'";
                        if (ldvUserGroup.Count != 0)
                        {
                            subitem.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int displayControlOnTab(Control pControl, TabPage pTabPage)
        {
            try
            {
                // The tabpage.
                Form _FormControl = new Form();
                _FormControl = (Form)pControl;

                // Add to the tab control.
                pTabPage.Text = _FormControl.Text;
                pTabPage.Name = _FormControl.Name;
                tbcNSites_V.TabPages.Add(pTabPage);
                tbcNSites_V.SelectTab(pTabPage);
                _FormControl.TopLevel = false;
                _FormControl.Parent = this;
                _FormControl.Dock = DockStyle.Fill;
                _FormControl.FormBorderStyle = FormBorderStyle.None;
                pTabPage.Controls.Add(_FormControl);
                tbcNSites_V.SelectTab(tbcNSites_V.SelectedIndex);
                _FormControl.Show();
                return tbcNSites_V.SelectedIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void closeTabPage()
        {
            try
            {
                tbcNSites_V.TabPages.RemoveAt(tbcNSites_V.SelectedIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void changeHomeImage()
        {
            try
            {
                try
                {
                    byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ScreenSaverImage);
                    pctScreenSaver.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                    pctScreenSaver.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch
                {
                    pctScreenSaver.BackgroundImage = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getGlobalVariablesData()
        {
            try
            {
                foreach (DataRow _drSystemConfig in loSystemConfiguration.getAllData().Rows)
                {
                    if (_drSystemConfig["Key"].ToString() == "CompanyLogo")
                    {
                        GlobalVariables.CompanyLogo = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ReportLogo")
                    {
                        GlobalVariables.ReportLogo = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "DisplayRecordLimit")
                    {
                        GlobalVariables.DisplayRecordLimit = int.Parse(_drSystemConfig["Value"].ToString());
                    }
                    else if (_drSystemConfig["Key"].ToString() == "EmailAddress")
                    {
                        GlobalVariables.EmailAddress = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "EmailPassword")
                    {
                        GlobalVariables.EmailPassword = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CurrentFinancialYear")
                    {
                        GlobalVariables.CurrentFinancialYear = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CollectionsDebit")
                    {
                        GlobalVariables.CollectionsDebit = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CollectionsCredit")
                    {
                        GlobalVariables.CollectionsCredit = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ReleasesDebit")
                    {
                        GlobalVariables.ReleasesDebit = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ReleasesCredit")
                    {
                        GlobalVariables.ReleasesCredit = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ServiceFeesDebit")
                    {
                        GlobalVariables.ServiceFeesDebit = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ServiceFeesCredit")
                    {
                        GlobalVariables.ServiceFeesCredit = _drSystemConfig["Value"].ToString();
                    }

                    else if (_drSystemConfig["Key"].ToString() == "ScreenSaverImage")
                    {
                        GlobalVariables.ScreenSaverImage = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "MDITabAlignment")
                    {
                        GlobalVariables.TabAlignment = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "BackupMySqlDumpAddress")
                    {
                        GlobalVariables.BackupMySqlDumpAddress = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "RestoreMySqlAddress")
                    {
                        GlobalVariables.RestoreMySqlAddress = _drSystemConfig["Value"].ToString();
                    }
                }

                //byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ReportLogo);
                GlobalVariables.DTCompanyLogo = GlobalFunctions.getReportLogo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion "END OF METHODS"

        #region "EVENTS"
        private void MDIFrameWork_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text += " [" + GlobalVariables.CurrentConnection + "]";
                pnlMenu.BackColor = Color.FromArgb(int.Parse(GlobalVariables.SecondaryColor));
                getGlobalVariablesData();
                try
                {
                    byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ScreenSaverImage);
                    pctScreenSaver.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                    pctScreenSaver.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch { }
                try
                {
                    byte[] hextobyteLogo = GlobalFunctions.HexToBytes(GlobalVariables.CompanyLogo);
                    pctLogo.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyteLogo);
                }
                catch { }
                try
                {
                    switch (GlobalVariables.TabAlignment)
                    {
                        case "Top":
                            tbcNSites_V.Alignment = TabAlignment.Top;
                            break;
                        case "Bottom":
                            tbcNSites_V.Alignment = TabAlignment.Bottom;
                            break;
                        case "Left":
                            tbcNSites_V.Alignment = TabAlignment.Left;
                            break;
                        case "Right":
                            tbcNSites_V.Alignment = TabAlignment.Right;
                            break;
                        default:
                            tbcNSites_V.Alignment = TabAlignment.Top;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                lblUsername.Text = "Welcome!  " + GlobalVariables.Userfullname;
                lblDateTime.Text = DateTime.Now.ToLongDateString();
                lblOwnerName.UseMnemonic = false;
                lblOwnerName.Text = GlobalVariables.CompanyName;
                lblApplicationName.Text = GlobalVariables.ApplicationName;
                if (GlobalVariables.Username != "admin" && GlobalVariables.Username != "technicalsupport")
                {
                    disabledMenuStrip();
                    enabledMenuStrip();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "MDIFrameWork_Load");
                em.ShowDialog();
                Application.Exit();
            }
        }

        private void tsmSystemConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "System Configuration")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                SystemConfigurationUI _SystemConfiguration = new SystemConfigurationUI();
                TabPage _SystemConfigurationTab = new TabPage();
                _SystemConfigurationTab.ImageIndex = 1;
                _SystemConfiguration.ParentList = this;
                displayControlOnTab(_SystemConfiguration, _SystemConfigurationTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSystemConfiguration_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmUser_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "User List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                User _User = new User();
                Type _Type = typeof(User);
                ListFormSystemUI _ListForm = new ListFormSystemUI((object)_User, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 2;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmUser_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmUserGroup_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "User Group List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                UserGroupListUI _UserGroupList = new UserGroupListUI();
                TabPage _UserGroupTab = new TabPage();
                _UserGroupTab.ImageIndex = 3;
                _UserGroupList.ParentList = this;
                displayControlOnTab(_UserGroupList, _UserGroupTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmUserGroup_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmChangeUserPassword_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Change User Password")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ChangeUserPasswordUI _ChangeUserPassword = new ChangeUserPasswordUI();
                TabPage _ChangeUserPasswordTab = new TabPage();
                _ChangeUserPasswordTab.ImageIndex = 4;
                _ChangeUserPassword.ParentList = this;
                displayControlOnTab(_ChangeUserPassword, _ChangeUserPasswordTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmChangeUserPassword_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmLockScreen_Click(object sender, EventArgs e)
        {
            try
            {
                UnlockScreenUI _UnlockScreen = new UnlockScreenUI();
                _UnlockScreen.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmLockScreen_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion "END OF EVENTS"

        private void tsmScreenSaver_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Screen Saver")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ScreenSaverUI _ScreenSaver = new ScreenSaverUI();
                TabPage _ScreenSaverTab = new TabPage();
                _ScreenSaverTab.ImageIndex = 5;
                _ScreenSaver.ParentList = this;
                displayControlOnTab(_ScreenSaver, _ScreenSaverTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmScreenSaver_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmAuditTrail_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Audit Trail")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                AuditTrailUI _AuditTrail = new AuditTrailUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 55;
                _AuditTrail.ParentList = this;
                displayControlOnTab(_AuditTrail, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmAuditTrail_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmTechnicalUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //check technical support username and password
                if (GlobalVariables.Username == "technicalsupport")
                {
                    foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                    {
                        if (_tab.Text == "Technical Update")
                        {
                            tbcNSites_V.SelectedTab = _tab;
                            return;
                        }
                    }

                    TechnicalUpdateUI _TechnicalUpdate = new TechnicalUpdateUI();
                    TabPage _TechnicalUpdateTab = new TabPage();
                    _TechnicalUpdateTab.ImageIndex = 7;
                    _TechnicalUpdate.ParentList = this;
                    displayControlOnTab(_TechnicalUpdate, _TechnicalUpdateTab);
                }
                else
                {
                    MessageBoxUI ms = new MessageBoxUI("Only JC Technical Support can open this Function!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmTechnicalUpdate_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnClearTab_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text != "Home")
                    {
                        tbcNSites_V.TabPages.Remove(_tab);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnClearTab_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSalesConfiguration_Click(object sender, EventArgs e)
        {

        }

        private void tsmClient_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Client List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Client _Client = new Client();
                Type _Type = typeof(Client);
                ListFormLendingUI _ListForm = new ListFormLendingUI((object)_Client, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 8;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmClient_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmArea_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Area List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Area _Area = new Area();
                Type _Type = typeof(Area);
                ListFormLendingUI _ListForm = new ListFormLendingUI((object)_Area, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 8;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmArea_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmBranch_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Branch List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Branch _Branch = new Branch();
                Type _Type = typeof(Branch);
                ListFormLendingUI _ListForm = new ListFormLendingUI((object)_Branch, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 8;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmBranch_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmZone_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Zone List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Zone _Zone = new Zone();
                Type _Type = typeof(Zone);
                ListFormLendingUI _ListForm = new ListFormLendingUI((object)_Zone, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 8;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmZone_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmCollector_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Collector List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Collector _Collector = new Collector();
                Type _Type = typeof(Collector);
                ListFormLendingUI _ListForm = new ListFormLendingUI((object)_Collector, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 8;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCollector_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmProduct_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Product List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Product _Product = new Product();
                Type _Type = typeof(Product);
                ListFormLendingUI _ListForm = new ListFormLendingUI((object)_Product, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 8;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCollector_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmLoanApplication_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Loan Application")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                LoanApplicationUI _LoanApplication = new LoanApplicationUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 35;
                _LoanApplication.ParentList = this;
                displayControlOnTab(_LoanApplication, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmLoanApplication_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmEndOfDay_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "End of Day")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                EndOfDayUI _EndOfDay = new EndOfDayUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 35;
                _EndOfDay.ParentList = this;
                displayControlOnTab(_EndOfDay, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmEndOfDay_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmDownloadDCRS_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Download DCRS")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                DownloadDCRSUI _DownloadDCRS = new DownloadDCRSUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 35;
                _DownloadDCRS.ParentList = this;
                displayControlOnTab(_DownloadDCRS, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmDownloadDCRS_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmUploadCollections_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Upload Collections")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                UploadCollectionsUI _UploadCollections = new UploadCollectionsUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 35;
                _UploadCollections.ParentList = this;
                displayControlOnTab(_UploadCollections, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmUploadCollections_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmBackupRestoreDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Database Backup/Restore")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                DatabaseBackupRestoreUI loDatabaseBackupRestore = new DatabaseBackupRestoreUI();
                loDatabaseBackupRestore.ParentList = this;
                loDatabaseBackupRestore.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmBackupRestoreDatabase_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmMonthlyCollections_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Monthly Collections")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                MonthlyCollectionsUI _MonthlyCollections = new MonthlyCollectionsUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 47;
                _MonthlyCollections.ParentList = this;
                displayControlOnTab(_MonthlyCollections, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmMonthlyCollections_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmMonthlyProjections_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Monthly Projections")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                MonthlyProjectionsUI _MonthlyProjections = new MonthlyProjectionsUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 47;
                _MonthlyProjections.ParentList = this;
                displayControlOnTab(_MonthlyProjections, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmMonthlyProjections_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmPastDueAccounts_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Past Due Accounts")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                PastDueAccountsUI _PastDueAccounts = new PastDueAccountsUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 47;
                _PastDueAccounts.ParentList = this;
                displayControlOnTab(_PastDueAccounts, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmPastDueAccounts_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmClientLedger_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Client Ledger")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ClientLedgerUI _ClientLedger = new ClientLedgerUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 47;
                _ClientLedger.ParentList = this;
                displayControlOnTab(_ClientLedger, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmClientLedger_Click");
                em.ShowDialog();
                return;
            }
        }

    }
}
