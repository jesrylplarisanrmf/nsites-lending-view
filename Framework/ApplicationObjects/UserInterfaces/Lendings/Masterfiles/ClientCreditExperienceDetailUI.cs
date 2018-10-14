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
    public partial class ClientCreditExperienceDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string lClientId;
        public bool lFromSave;
        GlobalVariables.Operation lOperation;
        ClientCreditExperience loClientCreditExperience;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ClientCreditExperienceDetailUI(string pClientId)
        {
            InitializeComponent();
            lClientId = pClientId;
            lId = "";
            lFromSave = false;
            lOperation = GlobalVariables.Operation.Add;
            loClientCreditExperience = new ClientCreditExperience();
        }
        public ClientCreditExperienceDetailUI(string pClientId, string pId)
        {
            InitializeComponent();
            lClientId = pClientId;
            lId = pId;
            lFromSave = false;
            lOperation = GlobalVariables.Operation.Edit;
            loClientCreditExperience = new ClientCreditExperience();
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
            txtCreditorName.Clear();
            txtLocation.Clear();
            txtValue.Text = "0.00";
            txtRemarks.Clear();
            txtCreditorName.Focus();
        }
        #endregion "END OF METHODS"

        private void ClientCreditExperienceDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                lFromSave = false;
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loClientCreditExperience.getAllData("0", lId).Rows)
                    {
                        lId = _dr["Id"].ToString();
                        txtCreditorName.Text = _dr["Creditor Name"].ToString();
                        txtLocation.Text = _dr["Location"].ToString();
                        txtValue.Text = string.Format("{0:n}", decimal.Parse(_dr["Value"].ToString()));
                        txtRemarks.Text = _dr["Remarks"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ClientCreditExperienceDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loClientCreditExperience.Id = lId;
                loClientCreditExperience.ClientId = lClientId;
                loClientCreditExperience.CreditorName = GlobalFunctions.replaceChar(txtCreditorName.Text);
                loClientCreditExperience.Location = GlobalFunctions.replaceChar(txtLocation.Text);
                loClientCreditExperience.Value = decimal.Parse(txtValue.Text);
                loClientCreditExperience.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loClientCreditExperience.UserId = GlobalVariables.UserId;

                string _Id = loClientCreditExperience.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Credit Experience has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
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

        private void txtValue_Leave(object sender, EventArgs e)
        {
            try
            {
                txtValue.Text = string.Format("{0:n}", decimal.Parse(txtValue.Text));
            }
            catch
            {
                txtValue.Text = "0.00";
            }
        }
    }
}
