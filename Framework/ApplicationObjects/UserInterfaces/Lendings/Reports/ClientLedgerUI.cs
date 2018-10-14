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

namespace NSites_V.ApplicationObjects.UserInterfaces.Lendings.Reports
{
    public partial class ClientLedgerUI : Form
    {
        Client loClient;
        LoanApplication loLoanApplication;
        LoanApplicationDetail loLoanApplicationDetail;

        public ClientLedgerUI()
        {
            InitializeComponent();
            loClient = new Client();
            loLoanApplication = new LoanApplication();
            loLoanApplicationDetail = new LoanApplicationDetail();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void viewClients()
        {
            try
            {
                dgvClientList.DataSource = loClient.getClientNames("ViewAll", "");
                viewClientLoans();
            }
            catch
            {
                dgvClientList.DataSource = null;
            }
        }

        private void viewClientLoans()
        {
            try
            {
                dgvClientLoanList.DataSource = loLoanApplication.getLoanApplicationByClientLedger(dgvClientList.CurrentRow.Cells[0].Value.ToString());
                viewClientLoanDetails();
            }
            catch 
            {
                dgvClientLoanList.DataSource = null;
            }
        }

        private void viewClientLoanDetails()
        {
            try
            {
                dgvClientLoanDetailList.DataSource = loLoanApplicationDetail.getLoanApplicationDetails(dgvClientLoanList.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                dgvClientLoanDetailList.DataSource = null;
            }
        }

        private void ClientLedgerUI_Load(object sender, EventArgs e)
        {

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
                viewClients();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvClientList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvClientList.Columns[e.ColumnIndex].Name == "Id")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvClientList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void dgvClientLoanList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Loan Application Id" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Application Status" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Loan Cycle" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Loan Disbursement Id" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Terms" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Payment Frequency" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Total Days Past Due")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Interest Rate" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Service Fee Rate" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Loan Amount" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Interest Amount" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Service Fee Amount" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Total Amount Due" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Installment Amount Due" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Loan Release Amount" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Payment Amount" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Penalty" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Running Balance" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Past Due Amount" ||
                    this.dgvClientLoanList.Columns[e.ColumnIndex].Name == "Advance Payment")
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvClientLoanList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void dgvClientLoanDetailList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvClientLoanDetailList.Columns[e.ColumnIndex].Name == "Id" ||
                    this.dgvClientLoanDetailList.Columns[e.ColumnIndex].Name == "Seq. No.")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvClientLoanDetailList.Columns[e.ColumnIndex].Name == "Amount Due" ||
                    this.dgvClientLoanDetailList.Columns[e.ColumnIndex].Name == "Installment Amount" ||
                    this.dgvClientLoanDetailList.Columns[e.ColumnIndex].Name == "Payment Amount" ||
                    this.dgvClientLoanDetailList.Columns[e.ColumnIndex].Name == "Running Balance" ||
                    this.dgvClientLoanDetailList.Columns[e.ColumnIndex].Name == "Variance")
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvClientLoanDetailList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }
    }
}
