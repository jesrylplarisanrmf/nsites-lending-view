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
    public partial class ClientPersonalReferenceDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string lClientId;
        public bool lFromSave;
        GlobalVariables.Operation lOperation;
        ClientPersonalReference loClientPersonalReference;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ClientPersonalReferenceDetailUI(string pClientId)
        {
            InitializeComponent();
            lClientId = pClientId;
            lId = "";
            lFromSave = false;
            lOperation = GlobalVariables.Operation.Add;
            loClientPersonalReference = new ClientPersonalReference();
        }
        public ClientPersonalReferenceDetailUI(string pClientId, string pId)
        {
            InitializeComponent();
            lClientId = pClientId;
            lId = pId;
            lFromSave = false;
            lOperation = GlobalVariables.Operation.Edit;
            loClientPersonalReference = new ClientPersonalReference();
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
            txtName.Clear();
            cboRelationship.Text = "";
            txtCompleteAddress.Text = "0";
            txtSourceOfIncome.Clear();
            txtCellphoneNo.Text = "0.00";
            txtRemarks.Clear();
            txtName.Focus();
        }
        #endregion "END OF METHODS"

        private void ClientPersonalReferenceDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                lFromSave = false;
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loClientPersonalReference.getAllData("0", lId).Rows)
                    {
                        lId = _dr["Id"].ToString();
                        txtName.Text = _dr["Name"].ToString();
                        cboRelationship.Text = _dr["Relationship"].ToString();
                        txtCompleteAddress.Text = _dr["Complete Address"].ToString();
                        txtSourceOfIncome.Text = _dr["Source of Income"].ToString();
                        txtCellphoneNo.Text = _dr["Cellphone No."].ToString();
                        txtRemarks.Text = _dr["Remarks"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ClientPersonalReferenceDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loClientPersonalReference.Id = lId;
                loClientPersonalReference.ClientId = lClientId;
                loClientPersonalReference.Name = GlobalFunctions.replaceChar(txtName.Text);
                loClientPersonalReference.Relationship = cboRelationship.Text;
                loClientPersonalReference.CompleteAddress = GlobalFunctions.replaceChar(txtCompleteAddress.Text);
                loClientPersonalReference.SourceOfIncome = GlobalFunctions.replaceChar(txtSourceOfIncome.Text);
                loClientPersonalReference.CellphoneNo = GlobalFunctions.replaceChar(txtCellphoneNo.Text);
                loClientPersonalReference.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loClientPersonalReference.UserId = GlobalVariables.UserId;

                string _Id = loClientPersonalReference.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Personal Reference has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
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
    }
}
