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
    public partial class ClientFamilyMemberDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string lClientId;
        public bool lFromSave;
        GlobalVariables.Operation lOperation;
        ClientFamilyMember loClientFamilyMember;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ClientFamilyMemberDetailUI(string pClientId)
        {
            InitializeComponent();
            lClientId = pClientId;
            lId = "";
            lFromSave = false;
            lOperation = GlobalVariables.Operation.Add;
            loClientFamilyMember = new ClientFamilyMember();
        }
        public ClientFamilyMemberDetailUI(string pClientId, string pId)
        {
            InitializeComponent();
            lClientId = pClientId;
            lId = pId;
            lFromSave = false;
            lOperation = GlobalVariables.Operation.Edit;
            loClientFamilyMember = new ClientFamilyMember();
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
            txtAge.Text = "0";
            txtCellphoneNo.Clear();
            txtBusinessWorkSchool.Clear();
            txtIncome.Text = "0.00";
            txtRemarks.Clear();
            txtName.Focus();
        }
        #endregion "END OF METHODS"

        private void ClientFamilyMemberDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                lFromSave = false;
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach(DataRow _dr in loClientFamilyMember.getAllData("0",lId).Rows)
                    {
                        lId = _dr["Id"].ToString();
                        txtName.Text = _dr["Name"].ToString();
                        cboRelationship.Text = _dr["Relationship"].ToString();
                        txtAge.Text = _dr["Age"].ToString();
                        txtCellphoneNo.Text = _dr["Cellphone No."].ToString();
                        txtBusinessWorkSchool.Text = _dr["Business/Work/School"].ToString();
                        txtIncome.Text = string.Format("{0:n}", decimal.Parse(_dr["Income"].ToString()));
                        txtRemarks.Text = _dr["Remarks"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ClientFamilyMemberDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loClientFamilyMember.Id = lId;
                loClientFamilyMember.ClientId = lClientId;
                loClientFamilyMember.Name = GlobalFunctions.replaceChar(txtName.Text);
                loClientFamilyMember.Relationship = cboRelationship.Text;
                loClientFamilyMember.Age = int.Parse(txtAge.Text);
                loClientFamilyMember.CellphoneNo = GlobalFunctions.replaceChar(txtCellphoneNo.Text);
                loClientFamilyMember.BusinessWorkSchool = GlobalFunctions.replaceChar(txtBusinessWorkSchool.Text);
                loClientFamilyMember.Income = decimal.Parse(txtIncome.Text);
                loClientFamilyMember.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loClientFamilyMember.UserId = GlobalVariables.UserId;

                string _Id = loClientFamilyMember.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Family Member has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
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

        private void txtIncome_Leave(object sender, EventArgs e)
        {
            try
            {
                txtIncome.Text = string.Format("{0:n}", decimal.Parse(txtIncome.Text));
            }
            catch
            {
                txtIncome.Text = "0.00";
            }
        }

        private void txtAge_Leave(object sender, EventArgs e)
        {
            try
            {
                txtAge.Text = string.Format("{0:0}", decimal.Parse(txtAge.Text));
            }
            catch
            {
                txtAge.Text = "0";
            }
        }
    }
}
