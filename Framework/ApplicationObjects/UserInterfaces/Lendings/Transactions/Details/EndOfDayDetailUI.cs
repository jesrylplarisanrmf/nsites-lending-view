using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Lendings;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Lendings.Transactions.Details
{
    public partial class EndOfDayDetailUI : Form
    {
        Branch loBranch;
        LoanEndOfDay loLoanEndOfDay;
        LoanEndOfDayDetail loLoanEndOfDayDetail;
        LoanApplication loLoanApplication;
        LoanApplicationDetail loLoanApplicationDetail;
        Common loCommon;
        GlobalVariables.Operation lOperation;
        
        public EndOfDayDetailUI()
        {
            InitializeComponent();
            loBranch = new Branch();
            loLoanEndOfDay = new LoanEndOfDay();
            loLoanEndOfDayDetail = new LoanEndOfDayDetail();
            loLoanApplication = new LoanApplication();
            loLoanApplicationDetail = new LoanApplicationDetail();
            loCommon = new Common();
            lOperation = GlobalVariables.Operation.Add;
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void EndOfDayDetailUI_Load(object sender, EventArgs e)
        {
            cboBranch.DataSource = loBranch.getAllData("ViewAll", "", "");
            cboBranch.DisplayMember = "Description";
            cboBranch.ValueMember = "Id";
            cboBranch.SelectedIndex = -1;
        }

        private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvDetailList.Rows.Clear();
                decimal _totalAmountDue = 0;
                decimal _totalRunningBalance = 0;
                decimal _totalCollection = 0;
                decimal _totalVariance = 0;
                decimal _totalLoanRelease = 0;
                decimal _totalServiceFee = 0;

                foreach (DataRow _dr in loLoanApplicationDetail.getEODLoanApplicationDetail(dtpDate.Value, cboBranch.SelectedValue.ToString()).Rows)
                {
                    int i = dgvDetailList.Rows.Add();
                    dgvDetailList.Rows[i].Cells["CollectorId"].Value = _dr["CollectorId"].ToString();
                    dgvDetailList.Rows[i].Cells["CollectorName"].Value = _dr["Collector Name"].ToString();
                    dgvDetailList.Rows[i].Cells["TotalAmountDue"].Value = string.Format("{0:n}", decimal.Parse(_dr["Total Running Balance"].ToString()));
                    dgvDetailList.Rows[i].Cells["TotalRunningBalance"].Value = string.Format("{0:n}", decimal.Parse(_dr["Total New Balance"].ToString()));
                    dgvDetailList.Rows[i].Cells["TotalCollection"].Value = string.Format("{0:n}", decimal.Parse(_dr["Total Collection"].ToString()));
                    dgvDetailList.Rows[i].Cells["TotalVariance"].Value = string.Format("{0:n}", decimal.Parse(_dr["Total Variance"].ToString()));
                    //get Release Amount
                    decimal _LoanAmount = 0;
                    decimal _ServiceFeeAmount = 0;
                    foreach (DataRow _drR in loLoanApplication.getForReleaseSheet(dtpDate.Value, _dr["CollectorId"].ToString()).Rows)
                    { 
                        _LoanAmount += decimal.Parse(_drR["Loan Amount"].ToString());
                        _ServiceFeeAmount += decimal.Parse(_drR["Service Fee Amount"].ToString());
                    }
                    dgvDetailList.Rows[i].Cells["TotalLoanRelease"].Value = string.Format("{0:n}", _LoanAmount);
                    dgvDetailList.Rows[i].Cells["TotalServiceFee"].Value = string.Format("{0:n}", _ServiceFeeAmount);

                    dgvDetailList.Rows[i].Cells["Remarks"].Value = "";

                    _totalAmountDue += decimal.Parse(_dr["Total Running Balance"].ToString());
                    _totalRunningBalance += decimal.Parse(_dr["Total New Balance"].ToString());
                    _totalCollection += decimal.Parse(_dr["Total Collection"].ToString());
                    _totalVariance += decimal.Parse(_dr["Total Variance"].ToString());
                    _totalLoanRelease += _LoanAmount;
                    _totalServiceFee += _ServiceFeeAmount;
                }

                txtTotalAmountDue.Text = string.Format("{0:n}", _totalAmountDue);
                txtTotalRunningBalance.Text = string.Format("{0:n}", _totalRunningBalance);
                txtTotalCollection.Text = string.Format("{0:n}", _totalCollection);
                txtTotalVariance.Text = string.Format("{0:n}", _totalVariance);
                txtTotalLoanRelease.Text = string.Format("{0:n}", _totalLoanRelease);
                txtTotalServiceFee.Text = string.Format("{0:n}", _totalServiceFee);
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetailList.Rows.Count == 0)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Record count must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }
                string _BranchId = "0";
                try
                {
                    _BranchId = cboBranch.SelectedValue.ToString();
                }
                catch
                {
                    _BranchId = "0";
                }
                if (_BranchId == "0")
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("You must select a branch!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }

                DialogResult _dr = new DialogResult();
                MessageBoxUI _mb = new MessageBoxUI("Continue closing this End of Day?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                _mb.ShowDialog();
                _dr = _mb.Operation;
                if (_dr == DialogResult.Yes)
                {
                    loLoanEndOfDay.Id = "";
                    loLoanEndOfDay.Date = dtpDate.Value;
                    loLoanEndOfDay.BranchId = _BranchId;
                    loLoanEndOfDay.TotalAmountDue = decimal.Parse(txtTotalAmountDue.Text);
                    loLoanEndOfDay.TotalRunningBalance = decimal.Parse(txtTotalRunningBalance.Text);
                    loLoanEndOfDay.TotalCollection = decimal.Parse(txtTotalCollection.Text);
                    loLoanEndOfDay.TotalVariance = decimal.Parse(txtTotalVariance.Text);
                    loLoanEndOfDay.TotalLoanRelease = decimal.Parse(txtTotalLoanRelease.Text);
                    loLoanEndOfDay.TotalServiceFee = decimal.Parse(txtTotalServiceFee.Text);
                    loLoanEndOfDay.EndedBy = GlobalVariables.UserId;
                    loLoanEndOfDay.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                    loLoanEndOfDay.UserId = GlobalVariables.UserId;

                    try
                    {
                        string _LoanEODId = loLoanEndOfDay.save(lOperation);
                        if (_LoanEODId != "")
                        {
                            for (int i = 0; i < dgvDetailList.Rows.Count; i++)
                            {
                                try
                                {
                                    loLoanEndOfDayDetail.DetailId = "";
                                    loLoanEndOfDayDetail.LoanEndOfDayId = _LoanEODId;
                                    loLoanEndOfDayDetail.CollectorId = dgvDetailList.Rows[i].Cells["CollectorId"].Value.ToString();
                                    loLoanEndOfDayDetail.TotalAmountDue = decimal.Parse(dgvDetailList.Rows[i].Cells["TotalAmountDue"].Value.ToString());
                                    loLoanEndOfDayDetail.TotalRunningBalance = decimal.Parse(dgvDetailList.Rows[i].Cells["TotalRunningBalance"].Value.ToString());
                                    loLoanEndOfDayDetail.TotalCollection = decimal.Parse(dgvDetailList.Rows[i].Cells["TotalCollection"].Value.ToString());
                                    loLoanEndOfDayDetail.TotalVariance = decimal.Parse(dgvDetailList.Rows[i].Cells["TotalVariance"].Value.ToString());
                                    loLoanEndOfDayDetail.TotalLoanRelease = decimal.Parse(dgvDetailList.Rows[i].Cells["TotalLoanRelease"].Value.ToString());
                                    loLoanEndOfDayDetail.TotalServiceFee = decimal.Parse(dgvDetailList.Rows[i].Cells["TotalServiceFee"].Value.ToString());
                                    
                                    string _remarks = "";
                                    try
                                    {
                                        _remarks = dgvDetailList.Rows[i].Cells["Remarks"].Value.ToString();
                                    }
                                    catch
                                    {
                                        _remarks = "";
                                    }
                                    loLoanEndOfDayDetail.Remarks = _remarks;
                                    loLoanEndOfDayDetail.UserId = GlobalVariables.UserId;
                                    loLoanEndOfDayDetail.save(GlobalVariables.Operation.Add);
                                }
                                catch { }
                            }
                            //update list
                            foreach (DataRow _drList in loLoanApplicationDetail.getEODLoanApplicationDetailList(dtpDate.Value, _BranchId).Rows)
                            {
                                loLoanApplicationDetail.updateEODLoanTransactionDetail(_drList[0].ToString(), _LoanEODId);
                            }
                            
                            #region "Insert to Accounting"
                            
                            string _financialYear = "", _journalEntryId = "";
                            try
                            {
                                foreach (DataRow _drAccounting in loCommon.getCurrentFinancialYear().Rows)
                                {
                                    _financialYear = _drAccounting[0].ToString();
                                }
                                //insert Collections
                                _journalEntryId = loCommon.insertJournalEntryFromOutside(_financialYear, "GJ", "JV", "", dtpDate.Value, "Lending Collection",
                                    0,0,"","","",GlobalVariables.UserId,"");
                                loCommon.insertJournalEntryDetailFromOutside(_journalEntryId, GlobalVariables.CollectionsDebit, decimal.Parse(txtTotalCollection.Text), 0, "", "", "", "", GlobalVariables.UserId);
                                loCommon.insertJournalEntryDetailFromOutside(_journalEntryId, GlobalVariables.CollectionsCredit, 0, decimal.Parse(txtTotalCollection.Text), "", "", "", "", GlobalVariables.UserId);

                                //insert Releases
                                _journalEntryId = loCommon.insertJournalEntryFromOutside(_financialYear, "GJ", "JV", "", dtpDate.Value, "Lending Releases",
                                    0, 0, "", "", "", GlobalVariables.UserId, "");
                                loCommon.insertJournalEntryDetailFromOutside(_journalEntryId, GlobalVariables.ReleasesDebit, decimal.Parse(txtTotalLoanRelease.Text), 0, "", "", "", "", GlobalVariables.UserId);
                                loCommon.insertJournalEntryDetailFromOutside(_journalEntryId, GlobalVariables.ReleasesCredit, 0, decimal.Parse(txtTotalLoanRelease.Text), "", "", "", "", GlobalVariables.UserId);

                                //insert Service Fees
                                _journalEntryId = loCommon.insertJournalEntryFromOutside(_financialYear, "GJ", "JV", "", dtpDate.Value, "Lending Service Fees",
                                    0, 0, "", "", "", GlobalVariables.UserId, "");
                                loCommon.insertJournalEntryDetailFromOutside(_journalEntryId, GlobalVariables.ServiceFeesDebit, decimal.Parse(txtTotalServiceFee.Text), 0, "", "", "", "", GlobalVariables.UserId);
                                loCommon.insertJournalEntryDetailFromOutside(_journalEntryId, GlobalVariables.ServiceFeesCredit, 0, decimal.Parse(txtTotalServiceFee.Text), "", "", "", "", GlobalVariables.UserId);
                            }
                            catch { }
                            
                            #endregion

                            MessageBoxUI _mb2 = new MessageBoxUI("End of Day has been closed successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                            _mb2.showDialog();

                            object[] _params = { _BranchId };
                            ParentList.GetType().GetMethod("refresh").Invoke(ParentList, _params);
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Unclosed quotation mark after the character string"))
                        {
                            MessageBoxUI _mb3 = new MessageBoxUI("Do not use this character( ' ).", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb3.showDialog();
                            return;
                        }
                        else
                        {
                            MessageBoxUI _mb3 = new MessageBoxUI(ex.Message, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb3.showDialog();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSave_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
