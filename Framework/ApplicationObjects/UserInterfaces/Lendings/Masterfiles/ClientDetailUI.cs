using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Lendings;

namespace NSites_V.ApplicationObjects.UserInterfaces.Lendings.Masterfiles
{
    public partial class ClientDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[14];
        LookUpValueUI loLookupValue;
        GlobalVariables.Operation lOperation;
        Client loClient;
        ClientFamilyMember loClientFamilyMember;
        ClientPersonalReference loClientPersonalReference;
        ClientSourceOfIncome loClientSourceOfIncome;
        ClientOwnedProperty loClientOwnedProperty;
        ClientCreditExperience loClientCreditExperience;
        LoanApplication loLoanApplication;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ClientDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loLookupValue = new LookUpValueUI();
            loClient = new Client();
            loClientFamilyMember = new ClientFamilyMember();
            loClientPersonalReference = new ClientPersonalReference();
            loClientSourceOfIncome = new ClientSourceOfIncome();
            loClientOwnedProperty = new ClientOwnedProperty();
            loClientCreditExperience = new ClientCreditExperience();
            loLoanApplication = new LoanApplication();
        }
        public ClientDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loLookupValue = new LookUpValueUI();
            loClient = new Client();
            loClientFamilyMember = new ClientFamilyMember();
            loClientPersonalReference = new ClientPersonalReference();
            loClientSourceOfIncome = new ClientSourceOfIncome();
            loClientOwnedProperty = new ClientOwnedProperty();
            loClientCreditExperience = new ClientCreditExperience();
            loLoanApplication = new LoanApplication();
            lRecords = pRecords;
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
        private void clear()
        {
            lId = "";
            txtLastname.Clear();
            txtFirstname.Clear();
            txtMiddlename.Clear();
            txtNickname.Clear();
            txtCellphoneNo.Clear();
            dtpBirthday.Value = DateTime.Now;
            txtSitioPurokStreet.Clear();
            txtBarangay.Clear();
            txtTownCity.Clear();
            txtProvince.Clear();
            txtYearsOfStay.Text = "0";
            cboHomeType.SelectedValue = 0;
            txtRemarks.Clear();
            dgvFamilyMembers.DataSource = null;
            dgvPersonalReference.DataSource = null;
            dgvSourceOfIncome.DataSource = null;
            dgvOwnedProperties.DataSource = null;
            dgvCreditExperience.DataSource = null;
            dgvLoanHistory.DataSource = null;
            txtLastname.Focus();
        }

        private void getAllList(string pClientId)
        {
            try
            {
                dgvFamilyMembers.DataSource = loClientFamilyMember.getAllData(pClientId,"");
            }
            catch
            {
                dgvFamilyMembers.DataSource = null;
            }

            try
            {
                dgvPersonalReference.DataSource = loClientPersonalReference.getAllData(pClientId, "");
            }
            catch
            {
                dgvPersonalReference.DataSource = null;
            }

            try
            {
                dgvSourceOfIncome.DataSource = loClientSourceOfIncome.getAllData(pClientId, "");
            }
            catch
            {
                dgvSourceOfIncome.DataSource = null;
            }

            try
            {
                dgvOwnedProperties.DataSource = loClientOwnedProperty.getAllData(pClientId, "");
            }
            catch
            {
                dgvOwnedProperties.DataSource = null;
            }

            try
            {
                dgvCreditExperience.DataSource = loClientCreditExperience.getAllData(pClientId, "");
            }
            catch
            {
                dgvCreditExperience.DataSource = null;
            }

            try
            {
                dgvLoanHistory.DataSource = loLoanApplication.getLoanApplicationByClient(pClientId);
            }
            catch
            {
                dgvLoanHistory.DataSource = null;
            }
        }
        #endregion "END OF METHODS"

        private void ClientDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtClientId.Text = lRecords[0];
                    txtLastname.Text = lRecords[1];
                    txtFirstname.Text = lRecords[2];
                    txtMiddlename.Text = lRecords[3];
                    txtNickname.Text = lRecords[4];
                    txtCellphoneNo.Text = lRecords[5];
                    dtpBirthday.Value = DateTime.Parse(lRecords[6]);
                    txtSitioPurokStreet.Text = lRecords[7];
                    txtBarangay.Text = lRecords[8];
                    txtTownCity.Text = lRecords[9];
                    txtProvince.Text = lRecords[10];
                    txtYearsOfStay.Text = lRecords[11];
                    cboHomeType.Text = lRecords[12];
                    txtRemarks.Text = lRecords[13];
                }

                getAllList(txtClientId.Text);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ClientDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loClient.Id = lId;
                loClient.Lastname = GlobalFunctions.replaceChar(txtLastname.Text);
                loClient.Firstname = GlobalFunctions.replaceChar(txtFirstname.Text);
                loClient.Middlename = GlobalFunctions.replaceChar(txtMiddlename.Text);
                loClient.Nickname = GlobalFunctions.replaceChar(txtNickname.Text);
                loClient.CellphoneNo = GlobalFunctions.replaceChar(txtCellphoneNo.Text);
                loClient.Birthday = dtpBirthday.Value;
                loClient.SitioPurokStreet = GlobalFunctions.replaceChar(txtSitioPurokStreet.Text);
                loClient.Barangay = GlobalFunctions.replaceChar(txtBarangay.Text);
                loClient.TownCity = GlobalFunctions.replaceChar(txtTownCity.Text);
                loClient.Province = GlobalFunctions.replaceChar(txtProvince.Text);
                try
                {
                    loClient.YearsOfStay = int.Parse(txtYearsOfStay.Text);
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("Years of Stay must in integer value!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    txtYearsOfStay.Focus();
                    return;
                }
                loClient.HomeType = cboHomeType.Text;
                loClient.Picture = "";
                loClient.ID1 = "";
                loClient.ID2 = "";
                loClient.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loClient.UserId = GlobalVariables.UserId;

                string _Id = loClient.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Client has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtLastname.Text;
                    lRecords[2] = txtFirstname.Text;
                    lRecords[3] = txtMiddlename.Text;
                    lRecords[4] = txtNickname.Text;
                    lRecords[5] = txtCellphoneNo.Text;
                    lRecords[6] = string.Format("{0:mm-dd-yyyy}", dtpBirthday.Value);
                    lRecords[7] = txtSitioPurokStreet.Text;
                    lRecords[8] = txtBarangay.Text;
                    lRecords[9] = txtTownCity.Text;
                    lRecords[10] = txtProvince.Text;
                    lRecords[11] = txtYearsOfStay.Text;
                    lRecords[12] = cboHomeType.Text;
                    lRecords[13] = txtRemarks.Text;
                    object[] _params = { lRecords };
                    if (lOperation == GlobalVariables.Operation.Edit)
                    {
                        ParentList.GetType().GetMethod("updateData").Invoke(ParentList, _params);
                        this.Close();
                    }
                    else
                    {
                        ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                        clear();
                    }
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("Failure to save the record!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSave_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnAddFamilyMember_Click(object sender, EventArgs e)
        {
            ClientFamilyMemberDetailUI loClientFamilyMemberDetail = new ClientFamilyMemberDetailUI(txtClientId.Text);
            loClientFamilyMemberDetail.ShowDialog();
            if (loClientFamilyMemberDetail.lFromSave)
            {
                dgvFamilyMembers.DataSource = loClientFamilyMember.getAllData(txtClientId.Text,"");
            }
        }

        private void dgvFamilyMembers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvFamilyMembers.Columns[e.ColumnIndex].Name == "Id")
                {
                    this.dgvFamilyMembers.Columns[e.ColumnIndex].Visible = false;
                }
                else if (this.dgvFamilyMembers.Columns[e.ColumnIndex].Name == "Age")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvFamilyMembers.Columns[e.ColumnIndex].Name == "Income")
                {
                    if (e.Value != null)
                    {
                        e.Value = String.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch { }
        }

        private void dgvFamilyMembers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditFamilyMember_Click(null, new EventArgs());
        }

        private void btnEditFamilyMember_Click(object sender, EventArgs e)
        {
            ClientFamilyMemberDetailUI loClientFamilyMemberDetail = new ClientFamilyMemberDetailUI(txtClientId.Text, dgvFamilyMembers.CurrentRow.Cells[0].Value.ToString());
            loClientFamilyMemberDetail.ShowDialog();
            if (loClientFamilyMemberDetail.lFromSave)
            {
                dgvFamilyMembers.DataSource = loClientFamilyMember.getAllData(txtClientId.Text, "");
            }
        }

        private void btnRemoveFamilyMember_Click(object sender, EventArgs e)
        {
            DialogResult _dr = new DialogResult();
            MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
            _mb.ShowDialog();
            _dr = _mb.Operation;
            if (_dr == DialogResult.Yes)
            {
                try
                {
                    if (loClientFamilyMember.remove(dgvFamilyMembers.CurrentRow.Cells[0].Value.ToString()))
                    {
                        MessageBoxUI _mb1 = new MessageBoxUI("Family Member has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                        _mb1.ShowDialog();
                        dgvFamilyMembers.DataSource = loClientFamilyMember.getAllData(txtClientId.Text, "");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                    return;
                }
            }
        }

        private void btnAddPersonalReference_Click(object sender, EventArgs e)
        {
            ClientPersonalReferenceDetailUI loClientPersonalReferenceDetail = new ClientPersonalReferenceDetailUI(txtClientId.Text);
            loClientPersonalReferenceDetail.ShowDialog();
            if (loClientPersonalReferenceDetail.lFromSave)
            {
                dgvPersonalReference.DataSource = loClientPersonalReference.getAllData(txtClientId.Text, "");
            }
        }

        private void btnAddSourceOfIncome_Click(object sender, EventArgs e)
        {
            ClientSourceOfIncomeDetailUI loClientSourceOfIncomeDetail = new ClientSourceOfIncomeDetailUI(txtClientId.Text);
            loClientSourceOfIncomeDetail.ShowDialog();
            if (loClientSourceOfIncomeDetail.lFromSave)
            {
                dgvSourceOfIncome.DataSource = loClientSourceOfIncome.getAllData(txtClientId.Text, "");
            }
        }

        private void btnAddOwnedProperty_Click(object sender, EventArgs e)
        {
            ClientOwnedPropertyDetailUI loClientOwnedPropertyDetail = new ClientOwnedPropertyDetailUI(txtClientId.Text);
            loClientOwnedPropertyDetail.ShowDialog();
            if (loClientOwnedPropertyDetail.lFromSave)
            {
                dgvOwnedProperties.DataSource = loClientOwnedProperty.getAllData(txtClientId.Text, "");
            }
        }

        private void btnAddCreditExperience_Click(object sender, EventArgs e)
        {
            ClientCreditExperienceDetailUI loClientCreditExperienceDetail = new ClientCreditExperienceDetailUI(txtClientId.Text);
            loClientCreditExperienceDetail.ShowDialog();
            if (loClientCreditExperienceDetail.lFromSave)
            {
                dgvCreditExperience.DataSource = loClientCreditExperience.getAllData(txtClientId.Text, "");
            }
        }

        private void dgvPersonalReference_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvPersonalReference.Columns[e.ColumnIndex].Name == "Id")
                {
                    this.dgvPersonalReference.Columns[e.ColumnIndex].Visible = false;
                }
            }
            catch { }
        }

        private void dgvSourceOfIncome_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvSourceOfIncome.Columns[e.ColumnIndex].Name == "Id")
                {
                    this.dgvSourceOfIncome.Columns[e.ColumnIndex].Visible = false;
                }
                else if (this.dgvSourceOfIncome.Columns[e.ColumnIndex].Name == "No. of Years")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvSourceOfIncome.Columns[e.ColumnIndex].Name == "Net Income")
                {
                    if (e.Value != null)
                    {
                        e.Value = String.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch { }
        }

        private void dgvOwnedProperties_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvOwnedProperties.Columns[e.ColumnIndex].Name == "Id")
                {
                    this.dgvOwnedProperties.Columns[e.ColumnIndex].Visible = false;
                }
                else if (this.dgvOwnedProperties.Columns[e.ColumnIndex].Name == "Value")
                {
                    if (e.Value != null)
                    {
                        e.Value = String.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch { }
        }

        private void dgvCreditExperience_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvCreditExperience.Columns[e.ColumnIndex].Name == "Id")
                {
                    this.dgvCreditExperience.Columns[e.ColumnIndex].Visible = false;
                }
                else if (this.dgvCreditExperience.Columns[e.ColumnIndex].Name == "Value")
                {
                    if (e.Value != null)
                    {
                        e.Value = String.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch { }
        }

        private void btnEditPersonalReference_Click(object sender, EventArgs e)
        {
            ClientPersonalReferenceDetailUI loClientPersonalReferenceDetail = new ClientPersonalReferenceDetailUI(txtClientId.Text, dgvPersonalReference.CurrentRow.Cells[0].Value.ToString());
            loClientPersonalReferenceDetail.ShowDialog();
            if (loClientPersonalReferenceDetail.lFromSave)
            {
                dgvPersonalReference.DataSource = loClientPersonalReference.getAllData(txtClientId.Text, "");
            }
        }

        private void btnEditSourceOfIncome_Click(object sender, EventArgs e)
        {
            ClientSourceOfIncomeDetailUI loClientSourceOfIncomeDetail = new ClientSourceOfIncomeDetailUI(txtClientId.Text, dgvSourceOfIncome.CurrentRow.Cells[0].Value.ToString());
            loClientSourceOfIncomeDetail.ShowDialog();
            if (loClientSourceOfIncomeDetail.lFromSave)
            {
                dgvSourceOfIncome.DataSource = loClientSourceOfIncome.getAllData(txtClientId.Text, "");
            }
        }

        private void btnEditOwnedProperty_Click(object sender, EventArgs e)
        {
            ClientOwnedPropertyDetailUI loClientOwnedPropertyDetail = new ClientOwnedPropertyDetailUI(txtClientId.Text, dgvOwnedProperties.CurrentRow.Cells[0].Value.ToString());
            loClientOwnedPropertyDetail.ShowDialog();
            if (loClientOwnedPropertyDetail.lFromSave)
            {
                dgvOwnedProperties.DataSource = loClientOwnedProperty.getAllData(txtClientId.Text, "");
            }
        }

        private void btnEditCreditExperience_Click(object sender, EventArgs e)
        {
            ClientCreditExperienceDetailUI loClientCreditExperienceDetail = new ClientCreditExperienceDetailUI(txtClientId.Text, dgvCreditExperience.CurrentRow.Cells[0].Value.ToString());
            loClientCreditExperienceDetail.ShowDialog();
            if (loClientCreditExperienceDetail.lFromSave)
            {
                dgvCreditExperience.DataSource = loClientCreditExperience.getAllData(txtClientId.Text, "");
            }
        }

        private void dgvPersonalReference_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditPersonalReference_Click(null, new EventArgs());
        }

        private void dgvSourceOfIncome_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditSourceOfIncome_Click(null, new EventArgs());
        }

        private void dgvOwnedProperties_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditOwnedProperty_Click(null, new EventArgs());
        }

        private void dgvCreditExperience_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditCreditExperience_Click(null, new EventArgs());
        }

        private void btnRemovePersonalReference_Click(object sender, EventArgs e)
        {
            DialogResult _dr = new DialogResult();
            MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
            _mb.ShowDialog();
            _dr = _mb.Operation;
            if (_dr == DialogResult.Yes)
            {
                try
                {
                    if (loClientPersonalReference.remove(dgvPersonalReference.CurrentRow.Cells[0].Value.ToString()))
                    {
                        MessageBoxUI _mb1 = new MessageBoxUI("Personal Reference has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                        _mb1.ShowDialog();
                        dgvPersonalReference.DataSource = loClientPersonalReference.getAllData(txtClientId.Text, "");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                    return;
                }
            }
        }

        private void btnRemoveSourceOfIncome_Click(object sender, EventArgs e)
        {
            DialogResult _dr = new DialogResult();
            MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
            _mb.ShowDialog();
            _dr = _mb.Operation;
            if (_dr == DialogResult.Yes)
            {
                try
                {
                    if (loClientSourceOfIncome.remove(dgvSourceOfIncome.CurrentRow.Cells[0].Value.ToString()))
                    {
                        MessageBoxUI _mb1 = new MessageBoxUI("Source of Income has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                        _mb1.ShowDialog();
                        dgvSourceOfIncome.DataSource = loClientSourceOfIncome.getAllData(txtClientId.Text, "");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                    return;
                }
            }
        }

        private void btnRemoveOwnedProperty_Click(object sender, EventArgs e)
        {
            DialogResult _dr = new DialogResult();
            MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
            _mb.ShowDialog();
            _dr = _mb.Operation;
            if (_dr == DialogResult.Yes)
            {
                try
                {
                    if (loClientOwnedProperty.remove(dgvOwnedProperties.CurrentRow.Cells[0].Value.ToString()))
                    {
                        MessageBoxUI _mb1 = new MessageBoxUI("Owned Property has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                        _mb1.ShowDialog();
                        dgvOwnedProperties.DataSource = loClientOwnedProperty.getAllData(txtClientId.Text, "");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                    return;
                }
            }
        }

        private void btnRemoveCreditExperience_Click(object sender, EventArgs e)
        {
            DialogResult _dr = new DialogResult();
            MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
            _mb.ShowDialog();
            _dr = _mb.Operation;
            if (_dr == DialogResult.Yes)
            {
                try
                {
                    if (loClientCreditExperience.remove(dgvCreditExperience.CurrentRow.Cells[0].Value.ToString()))
                    {
                        MessageBoxUI _mb1 = new MessageBoxUI("Credit Experience has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                        _mb1.ShowDialog();
                        dgvCreditExperience.DataSource = loClientCreditExperience.getAllData(txtClientId.Text, "");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                    return;
                }
            }
        }

        private void btnViewLoanHistoryDetails_Click(object sender, EventArgs e)
        {

        }

        private void dgvLoanHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Loan Application Id" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Application Status" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Loan Cycle" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Loan Disbursement Id" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Terms" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Payment Frequency" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Prepared By" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Approved By" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Posted By" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Total Days Past Due")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Interest Rate" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Service Fee Rate" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Loan Amount" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Interest Amount" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Service Fee Amount" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Total Amount Due" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Installment Amount Due" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Loan Release Amount" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Payment Amount" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Penalty" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Running Balance" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Past Due Amount" ||
                    this.dgvLoanHistory.Columns[e.ColumnIndex].Name == "Advance Payment")
                {
                    if (e.Value != null)
                    {
                        e.Value = string.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvLoanHistory_CellFormatting");
                em.ShowDialog();
                return;
            }
        }
    }
}
