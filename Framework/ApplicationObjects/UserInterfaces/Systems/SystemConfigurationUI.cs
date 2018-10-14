using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Generics;
using NSites_V.ApplicationObjects.Classes.Systems;
using NSites_V.ApplicationObjects.Classes.Lendings;

namespace NSites_V.ApplicationObjects.UserInterfaces.Systems
{
    public partial class SystemConfigurationUI : Form
    {
        #region "VARIABLES"
        public GlobalVariables.Operation lOperation;
        SystemConfiguration loSystemConfiguration;
        Hashtable lSystemConfigHash;
        Branch loBranch;
        Common loCommon;
        CryptorEngine loCryptorEngine;
        string lCompanyLogo = "";
        string lBannerImage = "";
        string lReportLogo = "";
        string lScreenSaverImage = "";
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public SystemConfigurationUI()
        {
            InitializeComponent();
            lSystemConfigHash = new Hashtable();
            loSystemConfiguration = new SystemConfiguration();
            loCryptorEngine = new CryptorEngine();
            loBranch = new Branch();
            loCommon = new Common();
        }
        #endregion "END OF CONSTRUCTORS"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        private void loadDataToHash()
        {
            try
            {
                lSystemConfigHash.Clear();
                lSystemConfigHash.Add("ApplicationName", lblApplicationName.Text);
                lSystemConfigHash.Add("VersionNo", lblVersionNo.Text);
                lSystemConfigHash.Add("DevelopedBy", lblDevelopedBy.Text);
                lSystemConfigHash.Add("CompanyName", txtCompanyName.Text);
                lSystemConfigHash.Add("CompanyAddress", txtCompanyAddress.Text);
                lSystemConfigHash.Add("ContactNumber", txtContactNumber.Text);
                lSystemConfigHash.Add("CompanyLogo", lCompanyLogo);
                lSystemConfigHash.Add("BannerImage", lBannerImage);
                lSystemConfigHash.Add("ReportLogo", lReportLogo);
                lSystemConfigHash.Add("PrimaryColor", txtPrimaryColor.Text);
                lSystemConfigHash.Add("SecondaryColor", txtSecondaryColor.Text);
                lSystemConfigHash.Add("FormBackColor", txtFormBackColor.Text);
                lSystemConfigHash.Add("DisplayRecordLimit", nudRecordLimit.Value.ToString());
                
                lSystemConfigHash.Add("TechnicalSupportEmailAddress", txtTechnicalSupportEmailAddress.Text);
                try
                {
                    lSystemConfigHash.Add("CollectionsDebit", cboCollectionsDebit.SelectedValue.ToString());
                }
                catch 
                {
                    lSystemConfigHash.Add("CollectionsDebit", "");
                }
                try
                {
                    lSystemConfigHash.Add("CollectionsCredit", cboCollectionsCredit.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("CollectionsCredit", "");
                }
                try
                {
                    lSystemConfigHash.Add("ReleasesDebit", cboReleasesDebit.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("ReleasesDebit", "");
                }
                try
                {
                    lSystemConfigHash.Add("ReleasesCredit", cboReleasesCredit.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("ReleasesCredit", "");
                }
                try
                {
                    lSystemConfigHash.Add("ServiceFeesDebit", cboServiceFeesDebit.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("ServiceFeesDebit", "");
                }
                try
                {
                    lSystemConfigHash.Add("ServiceFeesCredit", cboServiceFeesCredit.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("ServiceFeesCredit", "");
                }

                lSystemConfigHash.Add("EmailAddress", txtEmailAddress.Text);
                lSystemConfigHash.Add("EmailPassword", txtEmailPassword.Text);
                lSystemConfigHash.Add("ScreenSaverImage", lScreenSaverImage);
                if (rdbBottom.Checked)
                {
                    lSystemConfigHash.Add("MDITabAlignment", "Bottom");
                }
                else if (rdbLeft.Checked)
                {
                    lSystemConfigHash.Add("MDITabAlignment", "Left");
                }
                else if (rdbRight.Checked)
                {
                    lSystemConfigHash.Add("MDITabAlignment", "Right");
                }
                else if (rdbTop.Checked)
                {
                    lSystemConfigHash.Add("MDITabAlignment", "Top");
                }
                else
                {
                    lSystemConfigHash.Add("MDITabAlignment", "Top");
                }

                writeLocalsystemConfig();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void readSystemSettings()
        {
            try
            {
                string line = null;
                char[] splitter1 = { ';' };
                char[] splitter2 = { ':' };
                System.IO.TextReader readFile = new StreamReader(".../Main/text/SystemSettings.txt");
                line = readFile.ReadLine();
                if (line != null)
                {
                    string _StringToWrite = loCryptorEngine.DecryptString(line);
                    string[] data1 = _StringToWrite.Split(splitter1);
                    for (int i = 0; i < data1.Length; i++)
                    {
                        string[] data2 = null;
                        data2 = data1[i].Split(splitter2);
                        if (data2[0].ToString() == "CurrentBranchId")
                        {
                            try
                            {
                                cboCurrentBranch.SelectedValue = data2[1].ToString();
                            }
                            catch { }
                        }
                        else if (data2[0].ToString() == "CurrentConnection")
                        {
                            try
                            {
                                cboCurrentConnection.Text = data2[1].ToString();
                            }
                            catch { }
                        }
                    }
                }
                readFile.Close();
                readFile = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void writeSystemSettings()
        {
            try
            {
                string _currentBranchId = "";
                try
                {
                    _currentBranchId = cboCurrentBranch.SelectedValue.ToString();
                }
                catch { }
                string _StringToWrite = "CurrentBranchId:" + _currentBranchId + 
                    ";CurrentConnection:" + cboCurrentConnection.Text;
                _StringToWrite = loCryptorEngine.EncryptString(_StringToWrite);
                System.IO.TextWriter writeFile = new StreamWriter(".../Main/text/SystemSettings.txt");
                writeFile.WriteLine(_StringToWrite);
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void refresh()
        {
            try
            {
                DataTable _dt = new DataTable();
                _dt = loSystemConfiguration.getAllData();
                foreach (DataRow _dr in _dt.Rows)
                {
                    try
                    {
                        if (_dr["Key"].ToString() == "ApplicationName")
                        {
                            lblApplicationName.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "VersionNo")
                        {
                            lblVersionNo.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "DevelopedBy")
                        {
                            lblDevelopedBy.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CompanyName")
                        {
                            txtCompanyName.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CompanyAddress")
                        {
                            txtCompanyAddress.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "ContactNumber")
                        {
                            txtContactNumber.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CompanyLogo")
                        {
                            try
                            {
                                lCompanyLogo = _dr["Value"].ToString();
                                byte[] hextobyte = GlobalFunctions.HexToBytes(lCompanyLogo);
                                pctCompanyLogo.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                                pctCompanyLogo.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "BannerImage")
                        {
                            try
                            {
                                lBannerImage = _dr["Value"].ToString();
                                byte[] hextobyte = GlobalFunctions.HexToBytes(lBannerImage);
                                pctBannerImage.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                                pctBannerImage.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "ReportLogo")
                        {
                            try
                            {
                                lReportLogo = _dr["Value"].ToString();
                                byte[] hextobyte = GlobalFunctions.HexToBytes(lReportLogo);
                                pctReportLogo.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                                pctReportLogo.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "PrimaryColor")
                        {
                            txtPrimaryColor.Text = _dr["Value"].ToString();
                            txtPrimaryColor.BackColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                            txtPrimaryColor.ForeColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                        }
                        else if (_dr["Key"].ToString() == "SecondaryColor")
                        {
                            txtSecondaryColor.Text = _dr["Value"].ToString();
                            txtSecondaryColor.BackColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                            txtSecondaryColor.ForeColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                        }
                        else if (_dr["Key"].ToString() == "FormBackColor")
                        {
                            txtFormBackColor.Text = _dr["Value"].ToString();
                            txtFormBackColor.BackColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                            txtFormBackColor.ForeColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                        }
                        else if (_dr["Key"].ToString() == "DisplayRecordLimit")
                        {
                            nudRecordLimit.Value = int.Parse(_dr["Value"].ToString());
                        }
                        else if (_dr["Key"].ToString() == "EmailAddress")
                        {
                            txtEmailAddress.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "EmailPassword")
                        {
                            txtEmailPassword.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "TechnicalSupportEmailAddress")
                        {
                            txtTechnicalSupportEmailAddress.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CollectionsDebit")
                        {
                            try
                            {
                                cboCollectionsDebit.SelectedValue = _dr["Value"].ToString();
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "CollectionsCredit")
                        {
                            try
                            {
                                cboCollectionsCredit.SelectedValue = _dr["Value"].ToString();
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "ReleasesDebit")
                        {
                            try
                            {
                                cboReleasesDebit.SelectedValue = _dr["Value"].ToString();
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "ReleasesCredit")
                        {
                            try
                            {
                                cboReleasesCredit.SelectedValue = _dr["Value"].ToString();
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "ServiceFeesDebit")
                        {
                            try
                            {
                                cboServiceFeesDebit.SelectedValue = _dr["Value"].ToString();
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "ServiceFeesCredit")
                        {
                            try
                            {
                                cboServiceFeesCredit.SelectedValue = _dr["Value"].ToString();
                            }
                            catch { }
                        }


                        else if (_dr["Key"].ToString() == "ScreenSaverImage")
                        {
                            lScreenSaverImage = _dr["Value"].ToString();
                            try
                            {
                                byte[] hextobyte = GlobalFunctions.HexToBytes(lScreenSaverImage);
                                pctScreenSaver.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                                pctScreenSaver.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "MDITabAlignment")
                        {
                            if (_dr["Value"].ToString() == "Top")
                            {
                                rdbTop.Checked = true;
                            }
                            else if (_dr["Value"].ToString() == "Bottom")
                            {
                                rdbBottom.Checked = true;
                            }
                            else if (_dr["Value"].ToString() == "Left")
                            {
                                rdbLeft.Checked = true;
                            }
                            else if (_dr["Value"].ToString() == "Right")
                            {
                                rdbRight.Checked = true;
                            }
                            else
                            {
                                rdbTop.Checked = true;
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }

                readLocalSystemConfig();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void writeLocalsystemConfig()
        {
            try
            {
                string _StringToWrite = "BackupPath=" + txtBackupPath.Text;
                _StringToWrite = loCryptorEngine.EncryptString(_StringToWrite);
                System.IO.TextWriter writeFile = new StreamWriter(".../Main/text/localSystemConfig.txt");
                writeFile.WriteLine(_StringToWrite);
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void readLocalSystemConfig()
        {
            string line = null;
            char[] splitter1 = { ';' };
            char[] splitter2 = { '=' };
            System.IO.TextReader readFile = new StreamReader(".../Main/text/localSystemConfig.txt");
            line = readFile.ReadLine();
            if (line != null)
            {
                string _StringToWrite = loCryptorEngine.DecryptString(line);
                string[] data1 = _StringToWrite.Split(splitter1);
                for (int i = 0; i < data1.Length; i++)
                {
                    string[] data2 = null;
                    data2 = data1[i].Split(splitter2);
                    switch (data2[0].ToString())
                    {
                        case "BackupPath": txtBackupPath.Text = data2[1].ToString(); break;
                    }
                }
            }
            readFile.Close();
            readFile = null;
        }
        #endregion "END OF METHODS"

        #region "EVENTS"
        private void SystemConfigurationUI_Load(object sender, EventArgs e)
        {
            try
            {
                cboCurrentBranch.DataSource = loBranch.getAllData("ViewAll", "", "");
                cboCurrentBranch.DisplayMember = "Description";
                cboCurrentBranch.ValueMember = "Id";
                cboCurrentBranch.SelectedIndex = 0;
            }
            catch { }

            try
            {
                foreach(DataRow _dr in loCommon.getCurrentFinancialYear().Rows)
                {
                    txtCurrentFinancialYear.Text = _dr[0].ToString();
                }
            }
            catch { }

            try
            {
                cboCollectionsDebit.DataSource = loCommon.getChartOfAccounts("ViewAll","","");
                cboCollectionsDebit.DisplayMember = "Account Title";
                cboCollectionsDebit.ValueMember = "Id";
                cboCollectionsDebit.SelectedIndex = -1;
            }
            catch { }
            try
            {
                cboCollectionsCredit.DataSource = loCommon.getChartOfAccounts("ViewAll", "", "");
                cboCollectionsCredit.DisplayMember = "Account Title";
                cboCollectionsCredit.ValueMember = "Id";
                cboCollectionsCredit.SelectedIndex = -1;
            }
            catch { }
            try
            {
                cboReleasesDebit.DataSource = loCommon.getChartOfAccounts("ViewAll", "", "");
                cboReleasesDebit.DisplayMember = "Account Title";
                cboReleasesDebit.ValueMember = "Id";
                cboReleasesDebit.SelectedIndex = -1;
            }
            catch { }
            try
            {
                cboReleasesCredit.DataSource = loCommon.getChartOfAccounts("ViewAll", "", "");
                cboReleasesCredit.DisplayMember = "Account Title";
                cboReleasesCredit.ValueMember = "Id";
                cboReleasesCredit.SelectedIndex = -1;
            }
            catch { }
            try
            {
                cboServiceFeesDebit.DataSource = loCommon.getChartOfAccounts("ViewAll", "", "");
                cboServiceFeesDebit.DisplayMember = "Account Title";
                cboServiceFeesDebit.ValueMember = "Id";
                cboServiceFeesDebit.SelectedIndex = -1;
            }
            catch { }
            try
            {
                cboServiceFeesCredit.DataSource = loCommon.getChartOfAccounts("ViewAll", "", "");
                cboServiceFeesCredit.DisplayMember = "Account Title";
                cboServiceFeesCredit.ValueMember = "Id";
                cboServiceFeesCredit.SelectedIndex = -1;
            }
            catch { }

            try
            {
                refresh();
                readSystemSettings();
            }
            catch
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmSystemConfiguration", "Save"))
                {
                    return;
                }

                lOperation = GlobalVariables.Operation.Edit;
                loadDataToHash();
                foreach (DictionaryEntry Hash in lSystemConfigHash)
                {
                    try
                    {
                        loSystemConfiguration.Key = Hash.Key.ToString();
                        loSystemConfiguration.Value = Hash.Value.ToString();
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                    loSystemConfiguration.saveSystemConfiguration(lOperation);

                }
                writeSystemSettings();
                MessageBoxUI _mb = new MessageBoxUI("System Configuration has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                _mb.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSave_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCancel_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnFindScreenSaver_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFD = new OpenFileDialog();
                openFD.InitialDirectory = ".../Main/ScreenSaverImages/";
                openFD.Title = "Insert an Image";
                openFD.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|PNG Images|*.png";
                if (openFD.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBoxUI mb = new MessageBoxUI("Operation Cancelled", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                }
                else
                {
                    string _ChosenFile = openFD.FileName;
                    string _FileName = openFD.SafeFileName;

                    byte[] m_Bitmap = null;

                    FileStream fs = new FileStream(_ChosenFile, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    int length = (int)br.BaseStream.Length;
                    m_Bitmap = new byte[length];
                    m_Bitmap = br.ReadBytes(length);
                    br.Close();
                    fs.Close();

                    lScreenSaverImage = GlobalFunctions.ToHex(m_Bitmap);

                    pctScreenSaver.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(m_Bitmap);
                    pctScreenSaver.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnFindScreenSaver_Click");
                em.ShowDialog();
                return;
            }
        }

        #endregion "END OF EVENTS"

        private void btnFindCompanyLogo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFD = new OpenFileDialog();
                openFD.InitialDirectory = ".../Main/Images/";
                openFD.Title = "Insert an Image";
                openFD.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|PNG Images|*.png";
                if (openFD.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBoxUI mb = new MessageBoxUI("Operation Cancelled", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                }
                else
                {
                    string _ChosenFile = openFD.FileName;
                    string _FileName = openFD.SafeFileName;

                    byte[] m_Bitmap = null;

                    FileStream fs = new FileStream(_ChosenFile, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    int length = (int)br.BaseStream.Length;
                    m_Bitmap = new byte[length];
                    m_Bitmap = br.ReadBytes(length);
                    br.Close();
                    fs.Close();

                    lCompanyLogo = GlobalFunctions.ToHex(m_Bitmap);

                    pctCompanyLogo.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(m_Bitmap);
                    pctCompanyLogo.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnFind_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnFindBannerImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFD = new OpenFileDialog();
                openFD.InitialDirectory = ".../Main/Images/";
                openFD.Title = "Insert an Image";
                openFD.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|PNG Images|*.png";
                if (openFD.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBoxUI mb = new MessageBoxUI("Operation Cancelled", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                }
                else
                {
                    string _ChosenFile = openFD.FileName;
                    string _FileName = openFD.SafeFileName;

                    byte[] m_Bitmap = null;

                    FileStream fs = new FileStream(_ChosenFile, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    int length = (int)br.BaseStream.Length;
                    m_Bitmap = new byte[length];
                    m_Bitmap = br.ReadBytes(length);
                    br.Close();
                    fs.Close();

                    lBannerImage = GlobalFunctions.ToHex(m_Bitmap);

                    pctBannerImage.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(m_Bitmap);
                    pctBannerImage.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnFind_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnFindReportLogo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFD = new OpenFileDialog();
                openFD.InitialDirectory = ".../Main/Images/";
                openFD.Title = "Insert an Image";
                openFD.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|PNG Images|*.png";
                if (openFD.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBoxUI mb = new MessageBoxUI("Operation Cancelled", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                }
                else
                {
                    string _ChosenFile = openFD.FileName;
                    string _FileName = openFD.SafeFileName;

                    byte[] m_Bitmap = null;

                    FileStream fs = new FileStream(_ChosenFile, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    int length = (int)br.BaseStream.Length;
                    m_Bitmap = new byte[length];
                    m_Bitmap = br.ReadBytes(length);
                    br.Close();
                    fs.Close();

                    lReportLogo = GlobalFunctions.ToHex(m_Bitmap);

                    pctReportLogo.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(m_Bitmap);
                    pctReportLogo.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnFind_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnFindPrimaryColor_Click(object sender, EventArgs e)
        {
            ColorDialog clrColorDialog = new ColorDialog();
            if (clrColorDialog.ShowDialog() == DialogResult.OK)
            {
                txtPrimaryColor.Text = clrColorDialog.Color.ToArgb().ToString();
                txtPrimaryColor.BackColor = clrColorDialog.Color;
                txtPrimaryColor.ForeColor = clrColorDialog.Color;
            }
        }

        private void btnFindSecondaryColor_Click(object sender, EventArgs e)
        {
            ColorDialog clrColorDialog = new ColorDialog();
            if (clrColorDialog.ShowDialog() == DialogResult.OK)
            {
                txtSecondaryColor.Text = clrColorDialog.Color.ToArgb().ToString();
                txtSecondaryColor.BackColor = clrColorDialog.Color;
                txtSecondaryColor.ForeColor = clrColorDialog.Color;
            }
        }

        private void btnFindFormBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog clrColorDialog = new ColorDialog();
            if (clrColorDialog.ShowDialog() == DialogResult.OK)
            {
                txtFormBackColor.Text = clrColorDialog.Color.ToArgb().ToString();
                txtFormBackColor.BackColor = clrColorDialog.Color;
                txtFormBackColor.ForeColor = clrColorDialog.Color;
            }
        }
    }
}
