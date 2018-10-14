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
    public partial class ClientSourceOfIncomeDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string lClientId;
        public bool lFromSave;
        GlobalVariables.Operation lOperation;
        ClientSourceOfIncome loClientSourceOfIncome;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ClientSourceOfIncomeDetailUI(string pClientId)
        {
            InitializeComponent();
            lClientId = pClientId;
            lId = "";
            lFromSave = false;
            lOperation = GlobalVariables.Operation.Add;
            loClientSourceOfIncome = new ClientSourceOfIncome();
        }
        public ClientSourceOfIncomeDetailUI(string pClientId, string pId)
        {
            InitializeComponent();
            lClientId = pClientId;
            lId = pId;
            lFromSave = false;
            lOperation = GlobalVariables.Operation.Edit;
            loClientSourceOfIncome = new ClientSourceOfIncome();
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
            cboType.Text = "";
            txtOccupation.Clear();
            txtNetIncome.Text = "0.00";
            txtNoOfYears.Text = "0";
            txtEmployer.Clear();
            txtEmployerAddress.Clear();
            txtRemarks.Clear();
            cboType.Focus();
        }
        #endregion "END OF METHODS"

        private void ClientSourceOfIncomeDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                lFromSave = false;
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loClientSourceOfIncome.getAllData("0", lId).Rows)
                    {
                        lId = _dr["Id"].ToString();
                        cboType.Text = _dr["Type"].ToString();
                        txtOccupation.Text = _dr["Occupation"].ToString();
                        txtNetIncome.Text = string.Format("{0:n}", decimal.Parse(_dr["Net Income"].ToString()));
                        txtNoOfYears.Text = _dr["No. of Years"].ToString();
                        txtEmployer.Text = _dr["Employer"].ToString();
                        txtEmployerAddress.Text = _dr["Employer Address"].ToString();
                        txtRemarks.Text = _dr["Remarks"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ClientSourceOfIncomeDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loClientSourceOfIncome.Id = lId;
                loClientSourceOfIncome.ClientId = lClientId;
                loClientSourceOfIncome.Type = cboType.Text;
                loClientSourceOfIncome.Occupation = GlobalFunctions.replaceChar(txtOccupation.Text);
                loClientSourceOfIncome.NetIncome = decimal.Parse(txtNetIncome.Text);
                loClientSourceOfIncome.NoOfYears = int.Parse(txtNoOfYears.Text);
                loClientSourceOfIncome.Employer = GlobalFunctions.replaceChar(txtEmployer.Text);
                loClientSourceOfIncome.EmployerAddress = GlobalFunctions.replaceChar(txtEmployerAddress.Text);
                loClientSourceOfIncome.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loClientSourceOfIncome.UserId = GlobalVariables.UserId;

                string _Id = loClientSourceOfIncome.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Source of Income has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lFromSave = true;
                    this.Close();
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

        private void txtNetIncome_Leave(object sender, EventArgs e)
        {
            try
            {
                txtNetIncome.Text = string.Format("{0:n}", decimal.Parse(txtNetIncome.Text));
            }
            catch
            {
                txtNetIncome.Text = "0.00";
            }
        }

        private void txtNoOfYears_Leave(object sender, EventArgs e)
        {
            try
            {
                txtNoOfYears.Text = string.Format("{0:0}", decimal.Parse(txtNoOfYears.Text));
            }
            catch
            {
                txtNoOfYears.Text = "0";
            }
        }
    }
}
