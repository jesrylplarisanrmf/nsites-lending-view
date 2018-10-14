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

namespace NSites_V.ApplicationObjects.UserInterfaces.Lendings.Transactions.Details
{
    public partial class LoanApplicationDetailsUI : Form
    {
        #region "VARIABLES"
        LoanApplication loLoanApplication;
        Client loClient;
        Branch loBranch;
        Zone loZone;
        Collector loCollector;
        Product loProduct;
        GlobalVariables.Operation lOperation;
        string lLoanApplicationId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public LoanApplicationDetailsUI()
        {
            InitializeComponent();
            loLoanApplication = new LoanApplication();
            loClient = new Client();
            loBranch = new Branch();
            loZone = new Zone();
            loCollector = new Collector();
            loProduct = new Product();
            lOperation = GlobalVariables.Operation.Add;
            lLoanApplicationId = "";
        }
        public LoanApplicationDetailsUI(string pLoanApplicationId)
        {
            InitializeComponent();
            loLoanApplication = new LoanApplication();
            loClient = new Client();
            loBranch = new Branch();
            loZone = new Zone();
            loCollector = new Collector();
            loProduct = new Product();
            lOperation = GlobalVariables.Operation.Edit;
            lLoanApplicationId = pLoanApplicationId;
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

        }

        private void calculateLoan()
        {
            try
            {
                int _terms = int.Parse(txtTerms.Text);
                decimal _interestRate = decimal.Parse(txtInterestRate.Text);
                decimal _serviceFeeRate = decimal.Parse(txtServiceFeeRate.Text);
                decimal _loanAmount = decimal.Parse(txtLoanAmount.Text);

                txtInterestAmount.Text = string.Format("{0:n}", (_loanAmount * (_interestRate / 100)) * (decimal.Parse(txtTerms.Text)/30));
                txtTotalAmountDue.Text = string.Format("{0:n}", _loanAmount + decimal.Parse(txtInterestAmount.Text));
                
                if (cboPaymentFrequency.Text == "Daily")
                {
                    txtInstallmentAmountDue.Text = string.Format("{0:n}", decimal.Parse(txtTotalAmountDue.Text) / decimal.Parse(txtTerms.Text));
                }

                txtServiceFeeAmount.Text = string.Format("{0:n}", _loanAmount * (_serviceFeeRate / 100));
                txtLoanReleaseAmount.Text = string.Format("{0:n}", _loanAmount - decimal.Parse(txtServiceFeeAmount.Text));
            }
            catch { }
        }

        private void calculateTerms()
        {
            dtpMaturityDate.Value = dtpStartDate.Value.AddDays(double.Parse(txtTerms.Text));
        }

        #endregion "END OF METHODS"

        private void LoanApplicationDetailsUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                cboClient.DataSource = loClient.getClientLists();
                cboClient.ValueMember = "Id";
                cboClient.DisplayMember = "Client Name";
                cboClient.SelectedIndex = -1;

                cboBranch.DataSource = loBranch.getAllData("ViewAll","","");
                cboBranch.ValueMember = "Id";
                cboBranch.DisplayMember = "Description";
                cboBranch.SelectedIndex = -1;

                cboZone.DataSource = loZone.getAllData("ViewAll", "", "");
                cboZone.ValueMember = "Id";
                cboZone.DisplayMember = "Description";
                cboZone.SelectedIndex = -1;

                cboCollector.DataSource = loCollector.getAllData("ViewAll", "", "");
                cboCollector.ValueMember = "Id";
                cboCollector.DisplayMember = "Employee Name";
                cboCollector.SelectedIndex = -1;

                cboProduct.DataSource = loProduct.getAllData("ViewAll", "", "");
                cboProduct.ValueMember = "Id";
                cboProduct.DisplayMember = "Description";
                cboProduct.SelectedIndex = -1;

                cboPaymentFrequency.Text = "";
                txtTerms.Text = "0";
                txtInterestRate.Text = "0.00";
                txtServiceFeeRate.Text = "0.00";

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loLoanApplication.getAllData("", lLoanApplicationId, "").Rows)
                    {
                        txtLoanApplicationId.Text = _dr["Loan Application Id"].ToString();
                        dtpDate.Value = GlobalFunctions.ConvertToDate(_dr["Date"].ToString());
                        txtApplicationStatus.Text = _dr["Application Status"].ToString();
                        txtLoanCycle.Text = _dr["Loan Cycle"].ToString();
                        cboClient.SelectedValue = _dr["ClientId"].ToString();
                        cboBranch.SelectedValue = _dr["BranchId"].ToString();
                        cboZone.SelectedValue = _dr["ZoneId"].ToString();
                        cboCollector.SelectedValue = _dr["CollectorId"].ToString();
                        cboProduct.SelectedValue = _dr["ProductId"].ToString();
                        cboPaymentFrequency.Text = _dr["Payment Frequency"].ToString();
                        txtTerms.Text = _dr["Terms"].ToString();
                        dtpStartDate.Value = GlobalFunctions.ConvertToDate(_dr["Start Date"].ToString());
                        dtpMaturityDate.Value = GlobalFunctions.ConvertToDate(_dr["Maturity Date"].ToString());
                        txtInterestRate.Text = string.Format("{0:n}",decimal.Parse(_dr["Interest Rate"].ToString()));
                        txtServiceFeeRate.Text = string.Format("{0:n}",decimal.Parse(_dr["Service Fee Rate"].ToString()));
                        txtLoanAmount.Text = string.Format("{0:n}",decimal.Parse(_dr["Loan Amount"].ToString()));
                        txtInterestAmount.Text = string.Format("{0:n}", decimal.Parse(_dr["Interest Amount"].ToString()));
                        txtTotalAmountDue.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Amount Due"].ToString()));
                        txtInstallmentAmountDue.Text = string.Format("{0:n}", decimal.Parse(_dr["Installment Amount Due"].ToString()));
                        txtServiceFeeAmount.Text = string.Format("{0:n}", decimal.Parse(_dr["Service Fee Amount"].ToString()));
                        txtLoanReleaseAmount.Text = string.Format("{0:n}", decimal.Parse(_dr["Loan Release Amount"].ToString()));
                        txtRemarks.Text = _dr["Remarks"].ToString();
                    }
                }
                else
                {
                    txtApplicationStatus.Text = "In Progress";
                    txtLoanApplicationId.Text = "New";
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "LoanApplicationDetailsUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            loLoanApplication.LoanApplicationId = lLoanApplicationId;
            loLoanApplication.Date = dtpDate.Value;
            loLoanApplication.LoanCycle = int.Parse(txtLoanCycle.Text);
            loLoanApplication.ClientId = cboClient.SelectedValue.ToString();
            loLoanApplication.BranchId = cboBranch.SelectedValue.ToString();
            loLoanApplication.ZoneId = cboZone.SelectedValue.ToString();
            loLoanApplication.CollectorId = cboCollector.SelectedValue.ToString();
            loLoanApplication.ProductId = cboProduct.SelectedValue.ToString();
            loLoanApplication.PaymentFrequency = cboPaymentFrequency.Text;
            loLoanApplication.Terms = int.Parse(txtTerms.Text);
            loLoanApplication.StartDate = dtpStartDate.Value;
            loLoanApplication.MaturityDate = dtpMaturityDate.Value;
            loLoanApplication.InterestRate = decimal.Parse(txtInterestRate.Text);
            loLoanApplication.ServiceFeeRate = decimal.Parse(txtServiceFeeRate.Text);
            loLoanApplication.LoanAmount = decimal.Parse(txtLoanAmount.Text);
            loLoanApplication.InterestAmount = decimal.Parse(txtInterestAmount.Text);
            loLoanApplication.TotalAmountDue = decimal.Parse(txtTotalAmountDue.Text);
            loLoanApplication.InstallmentAmountDue = decimal.Parse(txtInstallmentAmountDue.Text);
            loLoanApplication.ServiceFeeAmount = decimal.Parse(txtServiceFeeAmount.Text);
            loLoanApplication.LoanReleaseAmount = decimal.Parse(txtLoanReleaseAmount.Text);
            loLoanApplication.PreparedBy = GlobalVariables.Username;
            loLoanApplication.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
            loLoanApplication.UserId = GlobalVariables.UserId;

            try
            {
                string _LoanApplicationId = loLoanApplication.save(lOperation);
                if (_LoanApplicationId != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Loan Application has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();

                    ParentList.GetType().GetMethod("refresh").Invoke(ParentList, null);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unclosed quotation mark after the character string"))
                {
                    MessageBoxUI _mb = new MessageBoxUI("Do not use this character( ' ).", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI(ex.Message, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
        }

        private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow _dr in loProduct.getAllData("", cboProduct.SelectedValue.ToString(), "").Rows)
                {
                    cboPaymentFrequency.Text = _dr["Payment Frequency"].ToString();
                    txtTerms.Text = _dr["Terms"].ToString();
                    txtInterestRate.Text = string.Format("{0:n}",decimal.Parse(_dr["Interest Rate"].ToString()));
                    txtServiceFeeRate.Text = string.Format("{0:n}", decimal.Parse(_dr["Service Fee Rate"].ToString()));
                }
            }
            catch { }
        }

        private void txtLoanAmount_TextChanged(object sender, EventArgs e)
        {
            calculateLoan();
        }

        private void txtLoanAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                txtLoanAmount.Text = string.Format("{0:n}",decimal.Parse(txtLoanAmount.Text));
            }
            catch
            {
                txtLoanAmount.Text = "0.00";
            }
        }

        private void cboPaymentFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculateLoan();
        }

        private void txtTerms_TextChanged(object sender, EventArgs e)
        {
            calculateLoan();
            calculateTerms();
        }

        private void txtInterestRate_TextChanged(object sender, EventArgs e)
        {
            calculateLoan();
        }

        private void txtServiceFeeRate_TextChanged(object sender, EventArgs e)
        {
            calculateLoan();
        }

        private void txtInterestRate_Leave(object sender, EventArgs e)
        {
            try
            {
                txtInterestRate.Text = string.Format("{0:n}", decimal.Parse(txtInterestRate.Text));
            }
            catch
            {
                txtInterestRate.Text = "0.00";
            }
        }

        private void txtServiceFeeRate_Leave(object sender, EventArgs e)
        {
            try
            {
                txtServiceFeeRate.Text = string.Format("{0:n}", decimal.Parse(txtServiceFeeRate.Text));
            }
            catch
            {
                txtServiceFeeRate.Text = "0.00";
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            calculateTerms();
        }
    }
}
