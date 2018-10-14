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
    public partial class EndOfDayUI : Form
    {
        LoanEndOfDay loLoanEndOfDay;
        LoanEndOfDayDetail loLoanEndOfDayDetail;
        Branch loBranch;
        Common loCommon;
        SearchesUI loSearches;
        EndOfDayRpt loEndOfDayRpt;
        //PurchaseRequestDetailRpt loPurchaseRequestDetailRpt;
        System.Data.DataTable ldtLoanEOD;
        
        ReportViewerUI loReportViewer;

        public EndOfDayUI()
        {
            InitializeComponent();
            loLoanEndOfDay = new LoanEndOfDay();
            loLoanEndOfDayDetail = new LoanEndOfDayDetail();
            loBranch = new Branch();
            loCommon = new Common();
            ldtLoanEOD = new System.Data.DataTable();
            loEndOfDayRpt = new EndOfDayRpt();
            //loPurchaseRequestDetailRpt = new PurchaseRequestDetailRpt();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        public void refresh(string pBranchId)
        {
            try
            {
                try
                {
                    dgvDetailList.DataSource = null;
                    ldtLoanEOD = loLoanEndOfDay.getLoanEndOfDayByBranch(pBranchId);
                }
                catch
                {
                    ldtLoanEOD = null;
                }
                
                loSearches.lQuery = "";
                GlobalFunctions.refreshGrid(ref dgvList, ldtLoanEOD);
                if (dgvList.Rows.Count > 0)
                {
                    viewDetails();
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
                dgvDetailList.DataSource = null;
                dgvDetailList.DataSource = loLoanEndOfDayDetail.getLoanEndOfDayDetails(dgvList.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                dgvDetailList.DataSource = null;
            }
        }

        private void EndOfDayUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(LoanEndOfDay);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmEndOfDay");
                loSearches.lQuery = "";
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "EndOfDayUI_Load");
                em.ShowDialog();
                return;
            }

            cboBranch.DataSource = loBranch.getAllData("ViewAll", "", "");
            cboBranch.DisplayMember = "Description";
            cboBranch.ValueMember = "Id";
            cboBranch.SelectedIndex = -1;

            cboBranch.SelectedValue = GlobalVariables.CurrentBranchId;
            viewDetails();
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

        private void btnEnd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmEndOfDay", "End"))
                {
                    return;
                }
                EndOfDayDetailUI loEndOfDayDetail = new EndOfDayDetailUI();
                loEndOfDayDetail.ParentList = this;
                loEndOfDayDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnEnd_Click");
                em.ShowDialog();
                return;
            }
        }

        private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                refresh(cboBranch.SelectedValue.ToString());
            }
            catch { }
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Id")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Total Amount Due" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Total Running Balance" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Total Collection" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Total Variance" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Total Loan Release" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Total Service Fee")
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
                if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Id")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Total Amount Due" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Total Running Balance" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Total Collection" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Total Variance" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Total Loan Release" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Total Service Fee")
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvDetailList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmEndOfDay", "Preview"))
                {
                    return;
                }
                if (dgvList.Rows.Count > 0)
                {
                    loEndOfDayRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loEndOfDayRpt.Database.Tables[1].SetDataSource(ldtLoanEOD);
                    loEndOfDayRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loEndOfDayRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loEndOfDayRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loEndOfDayRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loEndOfDayRpt.SetParameterValue("Title", "End of Day");
                    loEndOfDayRpt.SetParameterValue("SubTitle", "End of Day");
                    loEndOfDayRpt.SetParameterValue("Branch", cboBranch.Text);
                    loReportViewer.crystalReportViewer.ReportSource = loEndOfDayRpt;
                    loReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreview_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
