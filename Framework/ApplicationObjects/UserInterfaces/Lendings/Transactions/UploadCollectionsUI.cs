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

using NSites_V.ApplicationObjects.UserInterfaces.Lendings.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Lendings.Reports.TransactionsRpt;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Lendings.Transactions
{
    public partial class UploadCollectionsUI : Form
    {
        LoanApplication loLoanApplication;
        LoanApplicationDetail loLoanApplicationDetail;
        Branch loBranch;
        Collector loCollector;
        Common loCommon;
        SearchesUI loSearches;
        DailyCollectionAndReleaseSheetRpt loDailyCollectionAndReleaseSheetRpt;
        ReportViewerUI loReportViewer;

        public UploadCollectionsUI()
        {
            InitializeComponent();
            loLoanApplication = new LoanApplication();
            loLoanApplicationDetail = new LoanApplicationDetail();
            loBranch = new Branch();
            loCollector = new Collector();
            loCommon = new Common();
            loDailyCollectionAndReleaseSheetRpt = new DailyCollectionAndReleaseSheetRpt();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void getList()
        {
            try
            {
                dgvCollectionList.Rows.Clear();

                foreach (DataRow _dr in loLoanApplicationDetail.getUploadCollectionList(dtpDate.Value, cboCollector.SelectedValue.ToString()).Rows)
                {
                    int i = dgvCollectionList.Rows.Add();
                    dgvCollectionList.Rows[i].Cells["SeqNo"].Value = i+1;
                    dgvCollectionList.Rows[i].Cells["LoanApplicationDetailId"].Value = _dr["Detail Id"].ToString();
                    dgvCollectionList.Rows[i].Cells["Zone"].Value = _dr["Zone"].ToString();
                    dgvCollectionList.Rows[i].Cells["LoanApplicationNo"].Value = _dr["Loan Application No."].ToString();
                    dgvCollectionList.Rows[i].Cells["ClientName"].Value = _dr["Client Name"].ToString();
                    dgvCollectionList.Rows[i].Cells["LoanAmount"].Value = string.Format("{0:n}", decimal.Parse(_dr["Loan Amount"].ToString()));
                    dgvCollectionList.Rows[i].Cells["AmountDue"].Value = string.Format("{0:n}", decimal.Parse(_dr["Amount Due"].ToString()));
                    dgvCollectionList.Rows[i].Cells["RunningBalance"].Value = string.Format("{0:n}", decimal.Parse(_dr["Running Balance"].ToString()));
                    dgvCollectionList.Rows[i].Cells["InstallmentAmount"].Value = string.Format("{0:n}", decimal.Parse(_dr["Installment Amount"].ToString()));
                    dgvCollectionList.Rows[i].Cells["Payment"].Value = string.Format("{0:n}", decimal.Parse(_dr["Payment Amount"].ToString()));
                    dgvCollectionList.Rows[i].Cells["NewBalance"].Value = string.Format("{0:n}", decimal.Parse(_dr["New Balance"].ToString()));
                    dgvCollectionList.Rows[i].Cells["Variance"].Value = string.Format("{0:n}", decimal.Parse(_dr["Variance"].ToString()));
                    dgvCollectionList.Rows[i].Cells["PastDueReason"].Value = _dr["Past Due Reason"].ToString();
                    dgvCollectionList.Rows[i].Cells["Remarks"].Value = _dr["Remarks"].ToString();
                }
            }
            catch
            {
                dgvCollectionList.Rows.Clear();
            }
        }

        private void UploadCollectionsUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(LoanApplicationDetail);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmDownloadDCRS");
                loSearches.lQuery = "";
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "DownloadDCRSUI_Load");
                em.ShowDialog();
                return;
            }

            cboBranch.DataSource = loBranch.getAllData("ViewAll", "", "");
            cboBranch.DisplayMember = "Description";
            cboBranch.ValueMember = "Id";
            cboBranch.SelectedIndex = -1;

            cboCollector.DataSource = loCollector.getAllData("ViewAll", "", "");
            cboCollector.DisplayMember = "Employee Name";
            cboCollector.ValueMember = "Id";
            cboCollector.SelectedIndex = -1;

            cboBranch.SelectedValue = GlobalVariables.CurrentBranchId;
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

        private void cboCollector_SelectedIndexChanged(object sender, EventArgs e)
        {
            getList();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {

        }

        private void dgvCollectionList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvCollectionList.CurrentRow.DefaultCellStyle.BackColor = Color.PaleGreen;
            dgvCollectionList.CurrentRow.Cells["NewBalance"].Value = string.Format("{0:n}", decimal.Parse(dgvCollectionList.CurrentRow.Cells["RunningBalance"].Value.ToString()) - decimal.Parse(dgvCollectionList.CurrentRow.Cells["Payment"].Value.ToString()));
            dgvCollectionList.CurrentRow.Cells["Variance"].Value = string.Format("{0:n}", decimal.Parse(dgvCollectionList.CurrentRow.Cells["AmountDue"].Value.ToString()) - decimal.Parse(dgvCollectionList.CurrentRow.Cells["NewBalance"].Value.ToString()));
            dgvCollectionList.CurrentRow.Cells["Payment"].Value = string.Format("{0:n}", decimal.Parse(dgvCollectionList.CurrentRow.Cells["Payment"].Value.ToString()));
            //update transaction details
            string _remarks = "";
            string _pastDueReason = "";
            try
            {
                _remarks = dgvCollectionList.CurrentRow.Cells["Remarks"].Value.ToString();
            }
            catch
            {
                _remarks = "";
            }

            try
            {
                _pastDueReason = dgvCollectionList.CurrentRow.Cells["PastDueReason"].Value.ToString();
            }
            catch
            {
                _pastDueReason = "";
            }

            loLoanApplicationDetail.updatePayment(dgvCollectionList.CurrentRow.Cells["LoanApplicationDetailId"].Value.ToString(),
                decimal.Parse(dgvCollectionList.CurrentRow.Cells["Payment"].Value.ToString()),
                decimal.Parse(dgvCollectionList.CurrentRow.Cells["NewBalance"].Value.ToString()),
                decimal.Parse(dgvCollectionList.CurrentRow.Cells["Variance"].Value.ToString()),
                _pastDueReason, _remarks,
                cboCollector.SelectedValue.ToString());
        }
    }
}
