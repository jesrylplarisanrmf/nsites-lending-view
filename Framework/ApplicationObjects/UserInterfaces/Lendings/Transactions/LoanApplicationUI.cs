using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
//using Microsoft.Office.Interop.Excel;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Lendings;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Lendings.Transactions.Details;
//using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Reports.TransactionRpt;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Lendings.Transactions
{
    public partial class LoanApplicationUI : Form
    {
        LoanApplication loLoanApplication;
        LoanApplicationDetail loLoanApplicationDetail;
        Branch loBranch;
        Common loCommon;
        SearchesUI loSearches;
        System.Data.DataTable ldtLoanApplication;
        ReportViewerUI loReportViewer;

        public LoanApplicationUI()
        {
            InitializeComponent();
            loLoanApplication = new LoanApplication();
            loLoanApplicationDetail = new LoanApplicationDetail();
            loBranch = new Branch();
            loCommon = new Common();
            ldtLoanApplication = new System.Data.DataTable();
            //loGeneralJournalRpt = new GeneralJournalRpt();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        public void refresh()
        {
            try
            {
                ldtLoanApplication = loLoanApplication.getAllData("ViewAll", "", "");
                GlobalFunctions.refreshGrid(ref dgvList, ldtLoanApplication);
                viewDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addData(string[] pRecordData)
        {
            try
            {
                int n = dgvList.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvList.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvList.CurrentRow.Selected = false;
                dgvList.FirstDisplayedScrollingRowIndex = dgvList.Rows[n].Index;
                dgvList.Rows[n].Selected = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateData(string[] pRecordData)
        {
            try
            {
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvList.CurrentRow.Cells[i].Value = pRecordData[i];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void viewDetails()
        {
            try
            {
                dgvDetailList.DataSource = loLoanApplicationDetail.getLoanApplicationDetails(dgvList.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                dgvDetailList.DataSource = null;
            }
        }

        private void LoanApplicationUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(LoanApplication);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmLoanApplication");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "LoanApplicationUI_Load");
                em.ShowDialog();
                return;
            }

            cboBranch.DataSource = loBranch.getAllData("ViewAll", "", "");
            cboBranch.DisplayMember = "Description";
            cboBranch.ValueMember = "Id";
            cboBranch.SelectedIndex = -1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnClose_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmLoanApplication", "Refresh"))
                {
                    return;
                }
                refresh();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmLoanApplication", "Create"))
                {
                    return;
                }
                LoanApplicationDetailsUI loLoanApplicationDetails = new LoanApplicationDetailsUI();
                loLoanApplicationDetails.ParentList = this;
                loLoanApplicationDetails.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCreate_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmLoanApplication", "Update"))
                {
                    return;
                }

                DataTable _dt = loLoanApplication.getLoanApplicationStatus(dgvList.CurrentRow.Cells[0].Value.ToString());
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _drStatus in _dt.Rows)
                    {
                        if (_drStatus[0].ToString() != "In Process")
                        {
                            MessageBoxUI _mbStatus = new MessageBoxUI("Only IN PROCESS application can be updated!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                            _mbStatus.ShowDialog();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Loan Application Id does not exist!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    return;
                }

                if (dgvList.Rows.Count > 0)
                {
                    LoanApplicationDetailsUI loLoanApplicationDetails = new LoanApplicationDetailsUI(dgvList.CurrentRow.Cells[0].Value.ToString());
                    loLoanApplicationDetails.ParentList = this;
                    loLoanApplicationDetails.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnUpdate_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmLoanApplication", "Remove"))
                {
                    return;
                }

                DataTable _dt = loLoanApplication.getLoanApplicationStatus(dgvList.CurrentRow.Cells[0].Value.ToString());
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _drStatus in _dt.Rows)
                    {
                        if (_drStatus[0].ToString() != "In Process")
                        {
                            MessageBoxUI _mbStatus = new MessageBoxUI("Only IN PROCESS application can be removed!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                            _mbStatus.ShowDialog();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Loan Application Id does not exist!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    return;
                }
                
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this Loan Application record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loLoanApplication.remove(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("Loan Application record has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            refresh();
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRemove_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmLoanApplication", "Preview"))
                {
                    return;
                }
                if (dgvList.Rows.Count > 0)
                {
                    /*
                    loGeneralJournalRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loGeneralJournalRpt.Database.Tables[1].SetDataSource(ldtJournalEntry);
                    loGeneralJournalRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loGeneralJournalRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loGeneralJournalRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loGeneralJournalRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loGeneralJournalRpt.SetParameterValue("Title", "General Journal");
                    loGeneralJournalRpt.SetParameterValue("SubTitle", "General Journal");
                    try
                    {
                        if (loSearches.lAlias == "")
                        {
                            loGeneralJournalRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                        }
                        else
                        {
                            loGeneralJournalRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                        }

                    }
                    catch
                    {
                        loGeneralJournalRpt.SetParameterValue("QueryString", "");
                    }
                    loReportViewer.crystalReportViewer.ReportSource = loGeneralJournalRpt;
                    loReportViewer.ShowDialog();
                    */
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreview_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmLoanApplication", "Search"))
                {
                    return;
                }

                string _DisplayFields = "SELECT la.LoanApplicationId AS `Loan Application Id`, "+
		            "DATE_FORMAT(la.`Date`,'%m-%d-%Y') AS `Date`, "+
		            "la.ApplicationStatus AS `Application Status`, "+
		            "CONCAT(c.Lastname,', ',c.Firstname,' ',SUBSTRING(c.Middlename, 1, 1),'.') AS `Client Name`, "+
		            "b.Description AS Branch,z.Description AS Zone, "+
		            "CONCAT(e.Firstname,' ',SUBSTRING(e.Middlename, 1, 1),'. ',e.Lastname) AS `Collector Name`, "+
		            "p.Description AS Product,la.PaymentFrequency AS `Payment Frequency`, "+
		            "la.Terms,la.InterestRate AS `Interest Rate`,la.ServiceFeeRate AS `Service Fee Rate`, "+
		            "la.LoanAmount AS `Loan Amount`,la.InterestAmount AS `Interest Amount`, "+
		            "la.ServiceFeeAmount AS `Service Fee Amount`,la.TotalAmountDue AS `Total Amount Due`, "+
		            "la.InstallmentAmountDue AS `Installment Amount Due`, "+
		            "preby.Username AS `Prepared By`,appby.Username AS `Approved By`, "+
		            "DATE_FORMAT(la.DateApproved,'%m-%d-%Y') AS `Date Approved`, "+
		            "disby.Username AS `Disapproved By`,DATE_FORMAT(la.DateDisapproved,'%m-%d-%Y') AS `Date Disapproved`, "+
		            "la.DisapprovedReason AS `Disapproved Reason`,la.Remarks "+
		            "FROM loanapplication la "+
		            "LEFT JOIN `client` c "+
		            "ON la.ClientId = c.Id "+
		            "LEFT JOIN branch b "+
		            "ON la.BranchId = b.Id "+
		            "LEFT JOIN zone z "+
		            "ON la.ZoneId = z.Id "+
		            "LEFT JOIN collector col "+
		            "ON la.CollectorId = col.Id "+
		            "LEFT JOIN employee e "+
		            "ON col.EmployeeId = e.Id "+
		            "LEFT JOIN product p "+
		            "ON la.ProductId = p.Id "+
		            "LEFT JOIN `user` preby "+
		            "ON la.PreparedBy = preby.Id "+
		            "LEFT JOIN `user` appby "+
		            "ON la.ApprovedBy = appby.Id "+
                    "LEFT JOIN `user` disby " +
		            "ON la.DisapprovedBy = disby.Id ";
                string _WhereFields = " AND la.`Status` = 'Active' ORDER BY la.LoanApplicationId DESC;";
                loSearches.lAlias = "la.";
                loSearches.ShowDialog();
                if (loSearches.lFromShow)
                {
                    ldtLoanApplication = loCommon.getDataFromSearch(_DisplayFields + loSearches.lQuery + _WhereFields);
                    GlobalFunctions.refreshGrid(ref dgvList, ldtLoanApplication);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSearch_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Loan Application Id" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Application Status" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Loan Cycle" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Loan Disbursement Id" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Terms" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Payment Frequency" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Prepared By" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Approved By" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Posted By" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Total Days Past Due")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Interest Rate" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Service Fee Rate" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Loan Amount" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Interest Amount" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Service Fee Amount" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Total Amount Due" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Installment Amount Due" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Loan Release Amount" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Payment Amount" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Penalty" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Running Balance" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Past Due Amount" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Advance Payment")
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    System.Drawing.Point pt = dgvList.PointToScreen(e.Location);
                    cmsFunction.Show(pt);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_MouseClick");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmLoanApplication", "Approve"))
                {
                    return;
                }

                DataTable _dt = loLoanApplication.getLoanApplicationStatus(dgvList.CurrentRow.Cells[0].Value.ToString());
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _drStatus in _dt.Rows)
                    {
                        if (_drStatus[0].ToString() == "Approved")
                        {
                            MessageBoxUI _mbStatus = new MessageBoxUI("Loan application is already APPROVED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                            _mbStatus.ShowDialog();
                            return;
                        }
                        else if (_drStatus[0].ToString() == "Cancelled")
                        {
                            MessageBoxUI _mbStatus = new MessageBoxUI("You cannot approve a CANCELLED application!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                            _mbStatus.ShowDialog();
                            return;
                        }
                        else if (_drStatus[0].ToString() == "Posted")
                        {
                            MessageBoxUI _mbStatus = new MessageBoxUI("You cannot approve a POSTED application!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                            _mbStatus.ShowDialog();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Loan Application Id does not exist!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    return;
                }

                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue approving this Loan Application record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        try
                        {
                            if (loLoanApplication.approve(dgvList.CurrentRow.Cells[0].Value.ToString()))
                            {
                                MessageBoxUI _mb1 = new MessageBoxUI("Loan Application record has been successfully approved!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                                _mb1.ShowDialog();
                                refresh();
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
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnApprove_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiViewAllRecords_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalFunctions.refreshAll(ref dgvList, ldtLoanApplication);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiViewAllRecords_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh_Click(null, new EventArgs());
        }

        private void tsmiCreate_Click(object sender, EventArgs e)
        {
            btnCreate_Click(null, new EventArgs());
        }

        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void tsmiRemove_Click(object sender, EventArgs e)
        {
            btnRemove_Click(null, new EventArgs());
        }

        private void tsmiApprove_Click(object sender, EventArgs e)
        {
            btnApprove_Click(null, new EventArgs());
        }

        private void tsmiSearch_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, new EventArgs());
        }

        private void tsmiPreview_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, new EventArgs());
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                viewDetails();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_CellClick");
                em.ShowDialog();
                return;
            }
        }

        private void dgvDetailList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Id" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Seq. No.")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Id")
                {
                    if (e.Value != null)
                    {
                        //e.ColumnIndex.visi = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Amount Due" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Installment Amount" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Payment Amount" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Running Balance" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Variance")
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmLoanApplication", "Cancel"))
                {
                    return;
                }
                
                DataTable _dt = loLoanApplication.getLoanApplicationStatus(dgvList.CurrentRow.Cells[0].Value.ToString());
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _drStatus in _dt.Rows)
                    {
                        if (_drStatus[0].ToString() == "In Process")
                        {
                            MessageBoxUI _mbStatus = new MessageBoxUI("Only APPROVED application can be Cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                            _mbStatus.ShowDialog();
                            return;
                        }
                        else if (_drStatus[0].ToString() == "Cancelled")
                        {
                            MessageBoxUI _mbStatus = new MessageBoxUI("Loan application is already CANCELLED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                            _mbStatus.ShowDialog();
                            return;
                        }
                        else if (_drStatus[0].ToString() == "Posted")
                        {
                            MessageBoxUI _mbStatus = new MessageBoxUI("You cannot cancel a POSTED application!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                            _mbStatus.ShowDialog();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Loan Application Id does not exist!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    return;
                }

                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue cancelling this Loan Application?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        LendingCancelReasonUI loLendingCancelReason = new LendingCancelReasonUI();
                        loLendingCancelReason.ShowDialog();
                        if (loLendingCancelReason.lReason == "")
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("You must have a reason in cancelling entry!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            return;
                        }
                        else
                        {
                            if (loLoanApplication.cancel(dgvList.CurrentRow.Cells[0].Value.ToString(), loLendingCancelReason.lReason))
                            {
                                MessageBoxUI _mb1 = new MessageBoxUI("Loan Application record has been successfully cancelled!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                                _mb1.ShowDialog();
                                refresh();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnApprove_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmLoanApplication", "Post"))
                {
                    return;
                }
                string _LoanApplicationId = dgvList.CurrentRow.Cells[0].Value.ToString();
                DataTable _dt = loLoanApplication.getLoanApplicationStatus(_LoanApplicationId);
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _drStatus in _dt.Rows)
                    {
                        if (_drStatus[0].ToString() == "In Process")
                        {
                            MessageBoxUI _mbStatus = new MessageBoxUI("You cannot post an IN PROCESS application!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                            _mbStatus.ShowDialog();
                            return;
                        }
                        else if (_drStatus[0].ToString() == "Cancelled")
                        {
                            MessageBoxUI _mbStatus = new MessageBoxUI("You cannot post a CANCELLED application!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                            _mbStatus.ShowDialog();
                            return;
                        }
                        else if (_drStatus[0].ToString() == "Posted")
                        {
                            MessageBoxUI _mbStatus = new MessageBoxUI("Loan application is already POSTED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                            _mbStatus.ShowDialog();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Loan Application Id does not exist!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    return;
                }

                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue posting this Loan Application record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        try
                        {
                            if (loLoanApplication.post(_LoanApplicationId))
                            {
                                //create loan application detail
                                foreach (DataRow _drHeader in loLoanApplication.getAllData("", _LoanApplicationId, "").Rows)
                                {
                                    string _PaymentFrequency = _drHeader["Payment Frequency"].ToString();
                                    int _Terms = int.Parse(_drHeader["Terms"].ToString());
                                    DateTime _StartDate = DateTime.Parse(_drHeader["Start Date"].ToString());
                                    DateTime _MaturityDate = DateTime.Parse(_drHeader["Maturity Date"].ToString());
                                    decimal _TotalAmountDue = decimal.Parse(_drHeader["Total Amount Due"].ToString());
                                    decimal _InstallmentAmountDue = decimal.Parse(_drHeader["Installment Amount Due"].ToString());
                                    decimal _OriginalTotalAmountDue = decimal.Parse(_drHeader["Total Amount Due"].ToString());

                                    loLoanApplicationDetail.DetailId = "";
                                    loLoanApplicationDetail.LoanApplicationId = _LoanApplicationId;
                                    loLoanApplicationDetail.SeqNo = 0;
                                    loLoanApplicationDetail.Date = _StartDate;
                                    loLoanApplicationDetail.AmountDue = _TotalAmountDue;
                                    loLoanApplicationDetail.InstallmentAmount = _InstallmentAmountDue;
                                    loLoanApplicationDetail.PaymentAmount = 0;
                                    loLoanApplicationDetail.RunningBalance = _OriginalTotalAmountDue;
                                    loLoanApplicationDetail.NewBalance = 0;
                                    loLoanApplicationDetail.Variance = 0;
                                    loLoanApplicationDetail.PastDueReason = "";
                                    loLoanApplicationDetail.Remarks = "";
                                    loLoanApplicationDetail.UserId = GlobalVariables.UserId;
                                    loLoanApplicationDetail.save(GlobalVariables.Operation.Add);

                                    int i = 1;
                                    for (DateTime _startDate = _StartDate.AddDays(1); _startDate <= _MaturityDate; )
                                    {
                                        loLoanApplicationDetail.DetailId = "";
                                        loLoanApplicationDetail.LoanApplicationId = _LoanApplicationId;
                                        loLoanApplicationDetail.SeqNo = i;
                                        loLoanApplicationDetail.Date = _startDate;
                                        loLoanApplicationDetail.AmountDue = _TotalAmountDue - _InstallmentAmountDue;
                                        loLoanApplicationDetail.InstallmentAmount = _InstallmentAmountDue;
                                        loLoanApplicationDetail.PaymentAmount = 0;
                                        loLoanApplicationDetail.RunningBalance = _OriginalTotalAmountDue;
                                        loLoanApplicationDetail.NewBalance = 0;
                                        loLoanApplicationDetail.Variance = 0;
                                        loLoanApplicationDetail.PastDueReason = "";
                                        loLoanApplicationDetail.Remarks = "";
                                        loLoanApplicationDetail.UserId = GlobalVariables.UserId;
                                        loLoanApplicationDetail.save(GlobalVariables.Operation.Add);
                                        _startDate = _startDate.AddDays(1);
                                        _TotalAmountDue = _TotalAmountDue - _InstallmentAmountDue;
                                        i++;
                                    }
                                }

                                //lacking post to accounting system...

                                MessageBoxUI _mb1 = new MessageBoxUI("Loan Application record has been successfully posted!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                                _mb1.ShowDialog();
                                refresh();
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
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnApprove_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
