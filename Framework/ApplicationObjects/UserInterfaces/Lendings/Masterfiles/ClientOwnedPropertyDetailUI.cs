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
    public partial class ClientOwnedPropertyDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string lClientId;
        public bool lFromSave;
        GlobalVariables.Operation lOperation;
        ClientOwnedProperty loClientOwnedProperty;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ClientOwnedPropertyDetailUI(string pClientId)
        {
            InitializeComponent();
            lClientId = pClientId;
            lId = "";
            lFromSave = false;
            lOperation = GlobalVariables.Operation.Add;
            loClientOwnedProperty = new ClientOwnedProperty();
        }
        public ClientOwnedPropertyDetailUI(string pClientId, string pId)
        {
            InitializeComponent();
            lClientId = pClientId;
            lId = pId;
            lFromSave = false;
            lOperation = GlobalVariables.Operation.Edit;
            loClientOwnedProperty = new ClientOwnedProperty();
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
            cboPropertyType.Text = "";
            txtLocation.Clear();
            txtModel.Clear();
            txtValue.Text = "0.00";
            txtRemarks.Clear();
            cboPropertyType.Focus();
        }
        #endregion "END OF METHODS"

        private void ClientOwnedPropertyDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                lFromSave = false;
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loClientOwnedProperty.getAllData("0", lId).Rows)
                    {
                        lId = _dr["Id"].ToString();
                        cboPropertyType.Text = _dr["Property Type"].ToString();
                        txtLocation.Text = _dr["Location"].ToString();
                        txtModel.Text = _dr["Model"].ToString();
                        txtValue.Text = string.Format("{0:n}", decimal.Parse(_dr["Value"].ToString()));
                        txtRemarks.Text = _dr["Remarks"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ClientOwnedPropertyDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loClientOwnedProperty.Id = lId;
                loClientOwnedProperty.ClientId = lClientId;
                loClientOwnedProperty.PropertyType = cboPropertyType.Text;
                loClientOwnedProperty.Location = GlobalFunctions.replaceChar(txtLocation.Text);
                loClientOwnedProperty.Model = GlobalFunctions.replaceChar(txtModel.Text);
                loClientOwnedProperty.Value = decimal.Parse(txtValue.Text);
                loClientOwnedProperty.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loClientOwnedProperty.UserId = GlobalVariables.UserId;

                string _Id = loClientOwnedProperty.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Owned Property has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
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
